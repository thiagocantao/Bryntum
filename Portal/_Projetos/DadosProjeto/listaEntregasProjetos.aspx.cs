using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.XtraPrinting;
using System.Collections.Specialized;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_listaEntregasProjetos : System.Web.UI.Page
{
    string where = "";
    string resolucaoCliente = "";
    dados cDados;
    int idUsuarioLogado = 0;
    int CodigoEntidade = 0;
    int codigoProjeto = 0;
    public bool exportaOLAPTodosFormatos = false;
    string nomeProjeto = "";

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_PrjRelLicApr");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;
        if (!IsPostBack && !IsCallback)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            //populaOpcoesExportacao();
            hfGeral.Set("tipoArquivo", "XLS");
        }
        if (int.TryParse(Request.QueryString["IDProjeto"], out codigoProjeto) == true)
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"]);
            hfGeral.Set("CodigoProjeto", codigoProjeto);
            nomeProjeto = cDados.getNomeProjeto(codigoProjeto, "");
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        string formatacao = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1046{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}";
        formatacao += "\\viewkind4\\uc1\\pard\\qc\\f0\\fs32 Lista de Entregas do Projeto: " + nomeProjeto + "\\fs20\\par";
        formatacao += "}";
        //ASPxGridViewExporter1.PageHeader = formatacao;
        ASPxGridViewExporter1.ReportHeader = formatacao;
        //ASPxGridViewExporter1.ReportFooter = formatacao;


        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            //cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
    }

    private string getChavePrimaria()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private ListDictionary getDadosFormulario()
    {

        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoRiscoPadrao", dteData.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("UsuarioInclusao", txtIncluidaPor.Text);
        oDadosFormulario.Add("NomeProjeto", txtProjeto.Text);
        oDadosFormulario.Add("TipoLicao", txtTipo.Text);
        oDadosFormulario.Add("AssuntoLicao", txtAssunto.Text);
        return oDadosFormulario;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 120;
    }

    private void HeaderOnTela()
    {

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/listaEntregasProjetos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "listaEntregasProjetos"));

    }

    private void populaGrid()
    {
        DataSet ds1 = new DataSet();
        string comandoSQL = string.Format(
        @"SELECT p.CodigoProjeto, 
                 tc.NomeTarefa, 
                 tc.TerminoLB, 
                 tc.TerminoReal,
				 CASE WHEN tc.TerminoLB is null THEN ''
					  WHEN tc.TerminoReal is not null THEN 'Concluída'
					  WHEN tc.TerminoReal is null  and tc.TerminoLB < GETDATE() THEN 'Atrasada'
					  WHEN tc.TerminoReal is null  and tc.TerminoLB >= GETDATE() THEN 'Planejada'
				 END AS Situacao     
			FROM {0}.{1}.Projeto p INNER JOIN
				 {0}.{1}.CronogramaProjeto cp ON ( cp.CodigoProjeto = p.CodigoProjeto ) INNER JOIN
				 {0}.{1}.TarefaCronogramaProjeto tc ON ( tc.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto ) INNER JOIN
				 {0}.{1}.TipoTarefaCronograma ttc ON ( ttc.CodigoTipoTarefaCronograma = tc.CodigoTipoTarefaCronograma )
			WHERE tc.DataExclusao IS NULL
			AND ttc.codigoEntidade = p.CodigoEntidade
			AND ttc.IniciaisTipoControladoSistema = 'ENTREGA'
			AND p.CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
        ds1 = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds1.Tables[0];
        gvDados.DataBind();

        geraFiltroRelatorio();
    }

    private void geraFiltroRelatorio()
    {
        if (gvDados.FilterExpression != "")
        {
            ASPxFilterControl ASPxFilterControl1 = new ASPxFilterControl();
            ASPxFilterControl1.FilterExpression = gvDados.FilterExpression;
            where = "AND " + ASPxFilterControl1.GetFilterExpressionForMsSql();
        }

        gvDados.JSProperties["cp_Where"] = where;
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";


        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";

        if (e.Parameter == "PDF")
        {
            nomeArquivo = "~/imagens/botoes/imprimir.png";

        }
        if (e.Parameter == "XLS")//excel.PNG
        {
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";
        }
        btnImprimir.ImageUrl = nomeArquivo;
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado;

            string nomeArquivo1 = "", app = "", erro = "";

            try
            {


                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    //nomeArquivo1 = "listaEntregas_" + dataHora + ".pdf";
                    //PdfExportOptions p = new PdfExportOptions();
                    //p.DocumentOptions.Author = "CDIS Informática";
                    //ASPxGridViewExporter1.WritePdfToResponse(p);
                    //app = "application/pdf";

                    nomeArquivo1 = "listaEntregas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();

                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo1 = "\"" + nomeArquivo1 + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo1);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }

            }
            else
            {

                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        geraFiltroRelatorio();
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }
}