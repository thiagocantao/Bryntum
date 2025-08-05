using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System.Linq;
using DevExpress.XtraPrinting;
using System.Collections;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class _Projetos_DadosProjeto_HistoricoMedicaoApontamentosCustosTarefas : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;

    private int codigoUsuarioLogado = 0;
    private int codigoEntidadeContexto = 0;
    private int codigoProjeto;
    private string iniciaisTipoObjeto;

    private string resolucaoCliente = "";

    public bool podeIncluir = true;
    public bool podeEditar = false;
    public bool podeExcluir = false;


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeContexto = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        codigoProjeto = int.Parse(Request.QueryString["IDProjeto"]);
        iniciaisTipoObjeto = "PR";

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioLogado, codigoEntidadeContexto, codigoProjeto, "NULL", "PR", 0, "NULL", "PR_CnsBltMdcApt");
        }

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeContexto,
            codigoProjeto, "null", "PR", 0, "null", "PR_IncBltMdcApt");

        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeContexto,
            codigoProjeto, "null", "PR", 0, "null", "PR_ExcBltMdcApt");

        if (iniciaisTipoObjeto == "PR")
        {
            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);            
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hfGeral.Set("existeAnaliseOrcamento", "N");
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok


        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        //dsDado.SelectParameters[0].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
        cDados.aplicaEstiloVisual(Page);//Ok

        carregaGvDados();               //Ok
        
        if ((!IsPostBack) && (!IsCallback))
        {
            populaCombos();
        }

    }

    #region GRID's

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        DataSet ds = cDados.getHistoricoMedicaoApontamento(codigoEntidadeContexto, codigoUsuarioLogado, codigoProjeto);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    #endregion

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        if (Request.QueryString["Altura"] != null)
        {
            int altura = int.Parse(Request.QueryString["Altura"].ToString());
            gvDados.Settings.VerticalScrollableHeight = altura - 110;
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/HistoricoMedicaoApontamentosCustoTarefas .js""></script>"));
        this.TH(this.TS("barraNavegacao", "HistoricoStatusReport", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoBoletim").ToString();
        return codigoDado;
    }

    private string getReferencia()
    {
        string mes = string.Empty;
        string sequencial = string.Empty;
        string ano = string.Empty;
        if (gvDados.FocusedRowIndex != -1)
            mes = gvDados.GetRowValues(gvDados.FocusedRowIndex, "MesPeriodoBoletim").ToString();
            sequencial = gvDados.GetRowValues(gvDados.FocusedRowIndex, "SequenciaBoletimProjeto").ToString();
            ano = gvDados.GetRowValues(gvDados.FocusedRowIndex, "AnoPeriodoBoletim").ToString();

        return sequencial + "_" + mes + "_" + ano;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        switch (e.Parameter)
        {
            case "GerarRelatorio":
                mensagemErro_Persistencia = persisteGeracaoRelatorio();
                break;
            case "Excluir":
                mensagemErro_Persistencia = persisteExclusaoRelatorio();
                break;
            case "Visualizar":
                mensagemErro_Persistencia = Download();
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

    }

    private string persisteExclusaoRelatorio()
    {
        string msg = string.Empty;
        try
        {
            int CodigoBoletim = int.Parse(getChavePrimaria());
            cDados.ExcluiHistoricoMedicaoApontamento(CodigoBoletim, codigoUsuarioLogado, codigoProjeto);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteGeracaoRelatorio()
    {
        int retornoProc = 0;
        long idMedicaoApontamento = 0;
        string msg = "";
        try
        {
            int mesSelecionado = (int)ddlMes.Value;
            int anoSelecionado = Int32.Parse(txtAno.Value.ToString());
            cDados.GeraHistoricoMedicaoApontamento(mesSelecionado, anoSelecionado, codigoProjeto, codigoUsuarioLogado, codigoEntidadeContexto, out retornoProc, out idMedicaoApontamento);

            //Se deu tudo certo então vamos gerar o PDF
            if(retornoProc == 0)
            {
                if(idMedicaoApontamento > 0)
                {
                    msg = criaPdfHistoricoApontamento(idMedicaoApontamento);
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string Download()
    {
        string msg = string.Empty;
        try
        {
            int codigoMedicao = int.Parse(getChavePrimaria());
            string referencia = getReferencia();
            string nomeArquivo = "BoletimHistoricoMedicao"+ referencia +".pdf";
            string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivo;
            string arquivo2 = "~/ArquivosTemporarios/" + nomeArquivo;
            FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
            byte[] imagem = cDados.getConteudoPdfMedicaoHistorico(codigoMedicao);
            if (imagem == null)
            {
                msg = criaPdfHistoricoApontamento(codigoMedicao);
                if(msg == string.Empty)
                {
                    imagem = cDados.getConteudoPdfMedicaoHistorico(codigoMedicao);

                    if(imagem == null)
                    {
                        msg = "Não foi possível obter o PDF selecionado.";
                        return msg;
                    }
                }
                
            }

            fs.Write(imagem, 0, imagem.Length);
            fs.Close();

            ForceDownloadFile(arquivo2, true);

            return msg;
        }catch(Exception e)
        {
            msg = e.Message;
            return msg;
        }
    }

    private void ForceDownloadFile(string fname, bool forceDownload)
    {
        //ASPxWebControl.RedirectOnCallback(fname);
        string path = MapPath(fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "application/octet-stream";
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        Response.ContentType = type;
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }

    private string criaPdfHistoricoApontamento(long idMedicaoApontamento)
    {
        string msg = "";
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                rel_HistoricoMedicaoApontamentosTarefa relBoletim = new rel_HistoricoMedicaoApontamentosTarefa();
                relBoletim.pCodigoBoletimMedicao.Value = idMedicaoApontamento;

                relBoletim.CreateDocument();

                PdfExportOptions op = new PdfExportOptions();
                relBoletim.PrintingSystem.ExportToPdf(stream, op);
                byte[] arquivo = stream.GetBuffer();

                cDados.registraPdfHistoricoMedicaoApontamento(idMedicaoApontamento, arquivo);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    #endregion

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        try
        {
            Download();
        }
        catch (Exception ex) 
        { 
            throw ex;
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            var statusExcluido = e.GetValue("IndicaBoletimExcluido");

            if (statusExcluido.ToString() == "Sim")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    protected void Button_Load(object sender, EventArgs e)
    {
        ASPxButton btn = (ASPxButton)sender;
        GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;

        int indexLinha = container.VisibleIndex;

        var registroExcluido = gvDados.GetRowValues(indexLinha, "IndicaBoletimExcluido");

        if (btn.ID == "btnExcluir")
        {
                btn.Visible = podeExcluir;
            if (btn.Visible && registroExcluido.ToString() == "Não")
            {
                btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnExcluir_Click(" + indexLinha + ");}";
            }
            else {
                btn.Enabled = false;
                btn.ToolTip = "Boletim excluído";
                btn.ImageUrl = "~/imagens/botoes/excluirRegDes.PNG";
            }
        }
        if (btn.ID == "btnDownLoad")
        {
            if (registroExcluido.ToString() == "Não")
            {
                //btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnVisualizar_Click(" + indexLinha + ");}";
                btn.Enabled = true;
                btn.ToolTip = "Visualizar boletim";
                btn.ImageUrl = "~/imagens/botoes/btnPDF.PNG";
            }
            else
            {
                btn.Enabled = false;
                btn.ToolTip = "Boletim excluído";
                btn.ImageUrl = "~/imagens/botoes/btnPDFDes.PNG";
            }
  
        }
    }

    private void populaCombos()
    {
        ddlMes.Items.Add("Janeiro", 1);
        ddlMes.Items.Add("Fevereiro", 2);
        ddlMes.Items.Add("Março", 3);
        ddlMes.Items.Add("Abril", 4);
        ddlMes.Items.Add("Maio", 5);
        ddlMes.Items.Add("Junho", 6);
        ddlMes.Items.Add("Julho", 7);
        ddlMes.Items.Add("Agosto", 8);
        ddlMes.Items.Add("Setembro", 9);
        ddlMes.Items.Add("Outubro", 10);
        ddlMes.Items.Add("Novembro", 11);
        ddlMes.Items.Add("Dezembro", 12);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "HisSttRepPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcSelecaoModeloStatusReport);TipoOperacao = 'Incluir';", true, true, false, "HisSttRepPrj", "Histórico Status Report", this);
    }

    #endregion
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

    protected void txtAno_Load(object sender, EventArgs e)
    {
        if ((!IsPostBack) && (!IsCallback)) 
        { 
            DateTime dataAtual = DateTime.Now;

            txtAno.Value = dataAtual.Year.ToString();
        }
    }
}
