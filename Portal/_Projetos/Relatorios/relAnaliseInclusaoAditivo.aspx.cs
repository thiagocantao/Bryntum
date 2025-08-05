using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using DevExpress.Web;
using System.IO;

public partial class _Projetos_Relatorios_relAnaliseInclusaoAditivo : System.Web.UI.Page
{
    private dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public string alturaTabela;
    public string larguraTabela;
    public bool exportaOLAPTodosFormatos = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            //TODO: Verificar permissão
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_PrjRelIncAdt");
        }

        this.Title = cDados.getNomeSistema();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            txtInicio.Value = DateTime.Now.AddDays(-5);
            txtTermino.Value = DateTime.Now.AddDays(5);
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            //DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            //if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])))
            //    exportaOLAPTodosFormatos = dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].Equals("S");
            populaOpcoesExportacao();

            cDados.aplicaEstiloVisual(Page);
        }

        CDIS_GridLocalizer.Activate();

        carregaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            GridViewDataComboBoxColumn col = (GridViewDataComboBoxColumn)gvDados.Columns["TipoAditivo"];
            ListEditItem item = col.PropertiesComboBox.Items.Add("", null);
            col.PropertiesComboBox.Items.Move(item.Index, 0);

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 215) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 5) + "px";
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 250;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = String.Format("{0:dd-MM-yyy HH_mm_ss}_{1}", DateTime.Now, codigoUsuarioResponsavel);

            string nomeArquivo = string.Empty, app = string.Empty, erro = string.Empty;


            nomeArquivo = String.Format("mapaEntrega_{0}.xls", dataHora);
            try
            {
                ASPxGridViewExporter1.WriteXls(stream);
            }
            catch
            {
                erro = "S";
            }
            app = "application/ms-excel";


            if (string.IsNullOrEmpty(erro))
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
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

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();

        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";


        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);


            ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            ddlExporta.Items.Add(liHTML);

            ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            ddlExporta.Items.Add(liRTF);

            ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            ddlExporta.Items.Add(liCSV);

        }
        ddlExporta.SelectedIndex = 0;
    }

    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }

    private void carregaGrid()
    {
        string comandoSql = string.Format(@"
 SELECT DISTINCT 
        ac.CodigoAditivoContrato,
		c.NumeroContrato,
		p.NomePessoa AS RazaoSocial,
		c.DescricaoObjetoContrato AS Objeto,
		ac.DataInclusao,
		IsNull(ac.ValorAditivo,c.ValorContrato - c.ValorContratoOriginal) AS ValorAditivo,
		c.ValorContrato,
        CASE ac.TipoAditivo 
            WHEN 'VL' THEN 'Valor'
            WHEN 'PV' THEN 'Prazo e Valor'
            WHEN 'PR' THEN 'Prazo'
            WHEN 'TM' THEN 'Troca de Material'
            WHEN 'TC' THEN 'Termo de Encerramento'
        END AS TipoAditivo,
        ac.NovaDataVigencia
   FROM {0}.{1}.Contrato AS c INNER JOIN
		{0}.{1}.AditivoContrato AS ac ON (ac.CodigoContrato = c.CodigoContrato) INNER JOIN
		{0}.{1}.Pessoa AS p ON (p.CodigoPessoa = c.CodigoPessoaContratada)INNER JOIN 
        {0}.{1}.[PessoaEntidade] AS [pe] ON (
			pe.[CodigoPessoa] = c.[CodigoPessoaContratada]
			AND pe.codigoEntidade = {2}
            --AND pe.IndicaFornecedor = 'S'
			)
  WHERE ac.DataExclusao IS NULL
	AND c.CodigoEntidade = {2}
    and c.tipoPessoa = 'F'
	--AND ac.TipoAditivo IN ('VL', 'PV', 'PR', 'TM')
	AND IsNull(ac.ValorAditivo,c.ValorContrato - c.ValorContratoOriginal) <> 0"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);

        if (!string.IsNullOrEmpty(txtInicio.Text))
            comandoSql += String.Format(" AND ac.DataInclusao >= CONVERT(DateTime, '{0}', 103)", txtInicio.Text);
        if (!string.IsNullOrEmpty(txtTermino.Text))
            comandoSql += String.Format(" AND ac.DataInclusao <= CONVERT(DateTime, '{0}', 103)", txtTermino.Text);

        DataSet ds = cDados.getDataSet(comandoSql);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }

    }

    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaGrid();
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";

        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
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
