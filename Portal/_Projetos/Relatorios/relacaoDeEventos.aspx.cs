using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;
using System.Drawing;

public partial class _Projetos_Relatorios_relacaoDeEventos : System.Web.UI.Page
{
    dados cDados;
    private string where1;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    private bool exportaOLAPTodosFormatos = false;

    protected void Page_Init(object sender, EventArgs e)
    {
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
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_AcsRelEven");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_AcsRelEven"))
            podeIncluir = true;


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        //if (!IsPostBack)
        cDados.aplicaEstiloVisual(Page);

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        where1 = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        //populaModuloSistema();
        populaGrid();
        if (!IsPostBack)
        {
            populaTipoEvento();
            populaUnidade();
            ddlProjeto_Callback(new object(), new DevExpress.Web.CallbackEventArgsBase(""));
            populaUsuarios();
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        populaOpcoesExportacao();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/relacaoDeEventos.js""></script>"));
        this.TH(this.TS("relacaoDeEventos"));
    }

    protected void populaTipoEvento()
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoTipoTarefaCronograma, 
                     DescricaoTipoTarefaCronograma 
                FROM {0}.{1}.TipoTarefaCronograma
               WHERE codigoEntidade = {2} AND 
                     IniciaisTipoControladoSistema = 'EVENTO'"
            , cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlTipoEvento.TextField = "DescricaoTipoTarefaCronograma";
            ddlTipoEvento.ValueField = "CodigoTipoTarefaCronograma";
            ddlTipoEvento.DataSource = ds.Tables[0];
            ddlTipoEvento.DataBind();
            ddlTipoEvento.Items.Insert(0, new DevExpress.Web.ListEditItem("Todos", "null"));

            ddlTipoEvento.SelectedIndex = 0;
        }
    }

    protected void populaUnidade()
    {
        //cDados = new dados();
        string where = string.Format(@" AND CodigoEntidade = {0} 
                                        AND CodigoUnidadeNegocio IN 
                                            (SELECT CodigoUnidadeNegocio 
                                               FROM Projeto 
                                              WHERE CodigoEntidade = {0} 
                                                AND dataexclusao is null)", CodigoEntidade);

        DataSet ds = cDados.getUnidade(where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.DataSource = ds.Tables[0];
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new DevExpress.Web.ListEditItem("Todos", "null"));

            ddlUnidade.SelectedIndex = 0;

        }
    }

    protected void populaUsuarios()
    {

        //cDados = new dados();
        string where1 = "";

        DataSet ds = cDados.getUsuariosAtivosEntidade(CodigoEntidade, where1);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUsuario.TextField = "NomeUsuario";
            ddlUsuario.ValueField = "CodigoUsuario";
            ddlUsuario.DataSource = ds.Tables[0];
            ddlUsuario.DataBind();
        }

        ddlUsuario.Items.Insert(0, new DevExpress.Web.ListEditItem("Todos", "null"));
        ddlUsuario.SelectedIndex = 0;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 265;
    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        int in_CodigoEntidade = CodigoEntidade;
        int in_CodigoUsuario = idUsuarioLogado;
        int in_CodigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());
        string in_CodigoTipoEvento = (ddlTipoEvento.Value != null) ? ddlTipoEvento.Value.ToString() : "null";
        string in_CodigoProjeto = (ddlProjeto.Value != null) ? ddlProjeto.Value.ToString() : "null";
        string in_CodigoUnidadeNegocio = (ddlUnidade.Value != null) ? ddlUnidade.Value.ToString() : "null";
        string in_CodigoUsuarioParticipante = (ddlUsuario.Value != null) ? ddlUsuario.Value.ToString() : "null";
        string in_DataInicio = (dteInicio.Value != null) ? "'" + dteInicio.Value.ToString() + "'" : "null";
        string in_DataTermino = (dteTermino.Value != null) ? "'" + dteTermino.Value.ToString() + "'" : "null";
        string in_IndicaEventosQueParticipo = (ckbParticipo.Checked) ? "S" : "N";
        DataSet ds = cDados.getEventosTarefaCronograma(in_CodigoEntidade, in_CodigoUsuario, in_CodigoCarteira, in_CodigoProjeto, in_CodigoUnidadeNegocio, in_CodigoUsuarioParticipante, in_DataInicio, in_DataTermino, in_IndicaEventosQueParticipo, in_CodigoTipoEvento);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
        {
            if (gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName) != null)
            {
                return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
            }
            else
            {
                return "-1";
            }
        }            
        else
            return "-1";
    }

    protected void ddlProjeto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string where = " and un.SiglaUnidadeNegocio = '" + ddlUnidade.Text + "'";
        DataSet ds = cDados.getListaProjetos(CodigoEntidade, idUsuarioLogado, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), -1, where);
        ddlProjeto.DataSource = ds.Tables[0];
        ddlProjeto.ValueField = "CodigoProjeto";
        ddlProjeto.TextField = "Descricao";
        ddlProjeto.DataBind();

        ddlProjeto.Items.Insert(0, new DevExpress.Web.ListEditItem("Todos", "null"));
        ddlProjeto.SelectedIndex = 0;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "relRelacaoDeEventos_" + dataHora + ".xls";
                gvExporter.WriteXlsToResponse(nomeArquivo, new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
               

                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";

            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {

                nomeArquivo = "\"" + nomeArquivo + "\"";
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

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        populaGrid();
    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();

        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";


        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;
        //if (exportaOLAPTodosFormatos)
        //{
        //    ddlExporta.ClientEnabled = true;
        //    ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));

        //    ListEditItem liPDF = new ListEditItem("PDF", "PDF");
        //    liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
        //    ddlExporta.Items.Add(liPDF);


        //    ListEditItem liHTML = new ListEditItem("HTML", "HTML");
        //    liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
        //    ddlExporta.Items.Add(liHTML);

        //    ListEditItem liRTF = new ListEditItem("RTF", "RTF");
        //    liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
        //    ddlExporta.Items.Add(liRTF);

        //    ListEditItem liCSV = new ListEditItem("CSV", "CSV");
        //    liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
        //    ddlExporta.Items.Add(liCSV);

        //}
        ddlExporta.SelectedIndex = 0;
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

    protected void gvExporter_RenderBrick1(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            e.BrickStyle.BorderStyle = DevExpress.XtraPrinting.BrickBorderStyle.Center;
            e.BrickStyle.BorderColor = Color.White;
            e.BrickStyle.BorderWidth = 1.0F;
           
        }
        if (e.RowType == DevExpress.Web.GridViewRowType.Header)
        {
            e.BrickStyle.BorderWidth = 3.0F;
            e.BrickStyle.BorderColor = Color.White;

        }

    }

}
