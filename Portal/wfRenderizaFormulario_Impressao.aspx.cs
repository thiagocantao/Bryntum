using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.UI;

public partial class wfRenderizaFormulario_Impressao : System.Web.UI.Page
{
	private int codigoFormulario;
	private int codigoProjeto;
    private int codigoEntidade;
    private int codigoUsuario;
    dados cDados;
	private string resolucaoCliente = "";
	private int alturaPrincipal;

	protected void Page_Init(object sender, EventArgs e)
	{
		DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
		System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

		listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
		listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

		cDados = CdadosUtil.GetCdados(listaParametrosDados);
		try
		{
			if (cDados.getInfoSistema("IDUsuarioLogado") == null)
				Response.Redirect("~/erros/erroInatividade.aspx");
		}
		catch
		{
			Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
			Response.End();
        }
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        this.Title = cDados.getNomeSistema();
	}


    protected void Page_Load(object sender, EventArgs e)
	{
        if (!IsPostBack)
            hfSessao.Set("orientacao", "retrato");
        if (Request.QueryString["CF"] != null)
        {
            codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());

            if (codigoFormulario != -1)
                hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);
        }

		if (Request.QueryString["CP"] != null && Request.QueryString["CP"] != "")
			codigoProjeto = int.Parse(Request.QueryString["CP"]);

        DefineFormatosExportacao();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
		defineAlturaTela(resolucaoCliente);
		if (codigoFormulario > 0)
		{
			bool oFormularioEAssinado = false;
			string comandSQLVerificaFormularioAssinado = string.Format("SELECT {0}.{1}.[f_VerificaFormularioAssinado] ({2})", cDados.getDbName(), cDados.getDbOwner(), codigoFormulario);
			DataSet dsVerificaFormularioAssinado = cDados.getDataSet(comandSQLVerificaFormularioAssinado);
			if (cDados.DataSetOk(dsVerificaFormularioAssinado) && cDados.DataTableOk(dsVerificaFormularioAssinado.Tables[0]))
			{
				oFormularioEAssinado = (dsVerificaFormularioAssinado.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "s");
			}
            if (oFormularioEAssinado)
            {
                Response.Redirect("_CertificadoDigital/DownloadFormularioAssinado.aspx?cf=" + codigoFormulario, true);
                return;
            }
            else
            {
                XtraReport rel = ObtemRelatorio();
                if (hfSessao.Get("orientacao").ToString() == "paisagem")
                {
                    rel.Landscape = true;
                }
                else
                {
                    rel.Landscape = false;
                }
                ReportViewer1.Report = rel;
			}
		}

        ReportViewer1.Width = new Unit("100%");
        // se escolheu o formato "xml" e clicou no botão "salvar"
        if (!String.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "xml")
		{
			cDados = CdadosUtil.GetCdados(null);
			Object codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado");
			String comandoSql = String.Format("exec [dbo].[p_GetDadosFormularios] {0}, null, '{1}'", codigoUsuario, codigoFormulario);
			DataSet dsTemp = cDados.getDataSet(comandoSql);

			string nomeArquivoEmDisco = "RelImpressaoFormulario_" + codigoUsuario + codigoFormulario + ".xml";

			// salva o arquivo em disco
			string caminhoFisicoArquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivoEmDisco;
			dsTemp.WriteXml(caminhoFisicoArquivo, XmlWriteMode.WriteSchema);

			// faz o download
			string caminhoVirtualArquivo = "~/ArquivosTemporarios/" + nomeArquivoEmDisco;
			cDados.ForceDownloadFile(caminhoVirtualArquivo, Page, Response, true, false);
        }
        cDados.aplicaEstiloVisual(this);
    }

    private byte[] ObtemLayoutRelatorio()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoFormulario INT,
        @CodigoUsuario INT
    SET @CodigoFormulario = {0}
    SET @CodigoUsuario = {1}

 SELECT ISNULL(ru.ConteudoRelatorio, mr.ConteudoRelatorio) AS ConteudoRelatorio
   FROM Formulario as f INNER JOIN
        ModeloFormulario as mf ON mf.CodigoModeloFormulario = f.CodigoModeloFormulario LEFT JOIN
        ModeloRelatorio as mr ON mr.IDRelatorio = mf.IDRelatorio LEFT JOIN
        RelatorioUsuario as ru on ru.IDRelatorio = mr.IDRelatorio AND
                                  ru.CodigoUsuario = @CodigoUsuario
  WHERE f.CodigoFormulario = @CodigoFormulario", codigoFormulario, codigoUsuario);

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count == 0)
            return null;

        return dt.Rows[0]["ConteudoRelatorio"] as byte[];
    }

    private XtraReport ObtemRelatorio()
    {
        byte[] reportLayout = ObtemLayoutRelatorio();
        if (reportLayout == null || reportLayout.Length == 0)
            return new rel_ImpressaoFormularios(codigoFormulario);
        
        using (var stream = new MemoryStream(reportLayout))
        {
            //XtraReport rel = XtraReport.FromStream(stream, true);
            CdisReport rel = new CdisReport();
            rel.LoadLayout(stream);
            rel.Parameters["pUrlLogo"].Value = ObtemUrlLogoEntidade();
            rel.Parameters["pCodigoFormulario"].Value = codigoFormulario;
            return rel;
        }
    }

    private string ObtemUrlLogoEntidade()
    {
        DataTable dt = cDados.getLogoEntidade(codigoEntidade, "").Tables[0];
        byte[] byteArrayIn = dt.Rows[0].Field<byte[]>("LogoUnidadeNegocio");
        string caminhoArquivoLogoEntidade = string.Format("~/ArquivosTemporarios/{0}",
            Path.ChangeExtension(Path.GetRandomFileName(), "jpg"));
        using (var ms = new MemoryStream(byteArrayIn))
        {
            var img = System.Drawing.Image.FromStream(ms);
            img.Save(Server.MapPath(caminhoArquivoLogoEntidade));
        }

        return caminhoArquivoLogoEntidade;
    }

    private void DefineFormatosExportacao()
    {
        var ds = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
        //var exportaRelatorioFormatoPPT = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoPPT"] as string) == "S";
        //var exportaRelatorioFormatoDOC = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoDOC"] as string) == "S";
        var exportaOLAPTodosFormatos = (ds.Tables[0].Rows[0]["exportaOLAPTodosFormatos"] as string) == "S";

        var item = (ReportToolbarComboBox)ReportToolbar1.Items.Find(i => i.ItemKind == ReportToolbarItemKind.SaveFormat);
        if (!exportaOLAPTodosFormatos)
        {
            for (int i = item.Elements.Count - 1; i >= 0; i--)
            {
                var element = item.Elements[i];
                string valor = element.Value ?? string.Empty;
                if (!exportaOLAPTodosFormatos && !(valor.ToLower().StartsWith("pdf")
                    || valor.ToLower().StartsWith("rtf")))
                {
                    item.Elements.Remove(element);
                }
            }
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
	{   // Calcula a altura da tela
		int largura = 0;
		int altura = 0;

		cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = (altura - 380);
        //divRelatorio.Style.Add("height", (alturaPrincipal) + "px");
        divRelatorio.Style.Add("overflow", "auto");
        divToolBox.Style.Add("overflow", "auto");
        divToolBox.Style.Add("height", "45px");

	}
}
