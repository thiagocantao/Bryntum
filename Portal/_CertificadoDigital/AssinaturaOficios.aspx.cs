using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.IO;

public partial class _CertificadoDigital_AssinaturaOficios : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        populaGrid();

        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/AssinaturaOficios.js""></script>"));

        //gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        gvDados.JSProperties["cp_Arquivo"] = "";

        if (string.IsNullOrEmpty(Request.QueryString["CD"]))
            gvDados.Columns.OfType<GridViewCommandColumn>().Single().Visible = false;
    }

    private void populaGrid()
    {

        string comandoSQL = string.Format(@"
                SELECT * FROM f_pbh_GetOficiosAssinar('{0}', {1}, {2})
                ", Request.QueryString["CD"], codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            //((GridViewCommandColumn)gvDados.Columns[0]).CustomButtons[0].Visibility = GridViewCustomButtonVisibility.BrowsableRow;
        }

        gvDados.JSProperties["cp_CD"] = Request.QueryString["CD"];
    }

    #region VARIOS

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        alturaPrincipal -= 420;

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "Of" + Request.QueryString["CD"]);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "Of" + Request.QueryString["CD"], "Assinatura de Ofícios", this);
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {

    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {

    }

    public string getRowCount()
    {
        string retorno = "";
        //int quantidadeLinhas = 0;
        //for (int i = 0; i < gvDados.VisibleRowCount; i++)
        //{
        //    if (!gvDados.IsGroupRow(i))
        //        quantidadeLinhas++;
        //}

        //retorno = quantidadeLinhas + " pendências";

        return retorno;
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        object[] aOficios = ObtemDadosGrid(gvDados, "CodigoOficio", "OficioAssinado").ToArray();
        int visibleRowCount = gvDados.VisibleRowCount;
        e.Properties["cp_RowCount"] = visibleRowCount;
        e.Properties["cp_ArrayOficios"] = aOficios;
    }

    private static IEnumerable<object> ObtemDadosGrid(ASPxGridView grid, params string[] nomesColunas)
    {
        if (grid == null)
            throw new ArgumentNullException("grid", "O parametro não pode ser nulo");
        if (nomesColunas == null)
            throw new ArgumentNullException("nomesColunas", "O parametro não pode ser nulo");
        if (nomesColunas.Length == 0)
            throw new ArgumentException("Deve ser informado o nome de uma ou mais colunas da grid");

        int rowCount = grid.VisibleRowCount;
        for (int i = 0; i < rowCount; i++)
            yield return grid.GetRowValues(i, nomesColunas);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        int codigoWorkflow = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoWorkflow").ToString());
        long codigoInstanciaWf = long.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoInstanciaWf").ToString());

        string comite = Request.QueryString["CD"];

        string nomeArquivo = "ArquivosTemporarios\\" + string.Format("CF{0}_{1}_{2}_{3}.pdf", codigoWorkflow, codigoInstanciaWf, codigoUsuarioResponsavel, DateTime.Now.ToString("yyyyMMddHHmmss"));
        relOficioDemanda rel = new relOficioDemanda(codigoWorkflow, codigoInstanciaWf, codigoUsuarioResponsavel, comite);
        rel.Parameters["pMostraChave"].Value = "N";
        rel.Parameters["pComiteDeliberacao"].Value = comite;
        rel.CreateDocument();
        rel.ExportToPdf(Request.ServerVariables["APPL_PHYSICAL_PATH"] + nomeArquivo);

        string path = Page.MapPath("~/" + nomeArquivo);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "application/octet-stream";
        Response.AppendHeader("content-disposition",
                "attachment; filename=\"" + name + "\"");
        Response.ContentType = type;
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }

    public string getEventoClick(string idEvento)
    {
        return string.Format(@"processaClickBotao(""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"");"
            , idEvento
            , Eval("CodigoWorkflow")
            , Eval("CodigoInstanciaWf")
            , Eval("CodigoEtapaAtual")
            , Eval("OcorrenciaAtual"));
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string acao = e.Parameter;
        long codigoOperacao = 0L;
        switch (acao)
        {
            case "cancelar":
                codigoOperacao = (long)cDados.getInfoSistema("CodigoOperacaoCritica");
                cDados.setInfoSistema("CodigoOperacaoCritica", null);
                cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "Operação cancelada", "Operação cancelada pelo usuário");
                break;
            case "prosseguirSemAssinaturaDigital":
                string codigos = string.Join(",", gvDados.GetSelectedFieldValues("CodigoOficio"));
                codigoOperacao = cDados.RegistraOperacaoCritica("ASSNOFICIOS", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
                cDados.RegistraPassoOperacaoCritica(codigoOperacao, "Escopo do processo", string.Format("Ofícios(sem assinatura digital): {0}", codigos));

                bool resultado = ProssegueOficioSemAssinaturaDigital(codigoOperacao);
                if (resultado)
                {
                    e.Result = "sucesso";
                    cDados.FinalizaOperacaoCritica(codigoOperacao);
                }
                else
                {
                    e.Result = "falha";
                    cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "ErrVerific", e.Result);
                }
                break;
            default:
                break;
        }
    }

    private bool ProssegueOficioSemAssinaturaDigital(long codigoOperacao)
    {
        var linhasSelecionadas = gvDados.GetSelectedFieldValues(
            "CodigoOficio", "CodigoWorkflow", "CodigoInstanciaWf",
            "OcorrenciaAtual", "CodigoEtapaAtual");
        try
        {
            foreach (object[] valores in linhasSelecionadas)
            {
                string usuario = codigoUsuarioResponsavel.ToString();
                string oficio = valores[0].ToString();
                string workflow = valores[1].ToString();
                string instanciaWf = valores[2].ToString();
                string seqEtapa = valores[3].ToString();
                string etapa = valores[4].ToString();

                int regAf = 0;
                string comandoSQLProc = string.Format(@"EXEC p_pbh_AssinaOficio {0}, {1}, {2}, 'S'", workflow, instanciaWf, usuario);
                cDados.RegistraPassoOperacaoCritica(codigoOperacao, "Assina ofício", string.Format("-->Comando:{0}{1}", Environment.NewLine, comandoSQLProc.Replace("'", "''")));

                cDados.execSQL(comandoSQLProc, ref regAf);

                string msgErro;

                cDados.RegistraPassoOperacaoCritica(codigoOperacao, "Processa ação: ", string.Format("Código ofício: {0}", oficio));
                cDados.ProcessaAcaoWorkflow(usuario, workflow, instanciaWf, seqEtapa, etapa, false, out msgErro);
            }
        }
        catch (Exception ex)
        {
            cDados.RegistraPassoOperacaoCritica(codigoOperacao, ex.Message.Replace("'", "''"), string.Format("Código ofício: {0}", codigoOperacao));
            return false;
        }

        return true;
    }

    protected void ASPxButton3_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        DataSet ds = cDados.getParametrosSistema("utilizaAssinaturaDigitalOficio");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string utilizaAssinaturaDigitalOficio = ds.Tables[0].Rows[0]["utilizaAssinaturaDigitalOficio"] as string;
            e.Properties["cp_utilizaAssinaturaDigitalOficio"] = utilizaAssinaturaDigitalOficio ?? "N";
        }
        else
            e.Properties["cp_utilizaAssinaturaDigitalOficio"] = "N";
    }

    protected void ASPxButton3_Load(object sender, EventArgs e)
    {
        var button = (ASPxButton)sender;

        if (string.IsNullOrEmpty(Request.QueryString["CD"]))
        {
            button.ClientVisible = false;
        }
        else
        {
            string comandoSql;

            #region Comando SQL

            comandoSql = string.Format("SELECT [dbo].[f_pbh_VerificaPermissaoAssinaturaOficio]({1}, {2}, '{0}') as Resultado"
                , Request.QueryString["CD"], codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

            #endregion

            DataSet ds = cDados.getDataSet(comandoSql);
            int resultado = Convert.ToInt32(ds.Tables[0].Rows[0]["Resultado"]);

            button.ClientVisible = resultado == 1;
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK" || e.CallbackName == "REFRESH")
            gvDados.Selection.UnselectAll();
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CodigoWorkflow")
        {
            ASPxButton ck = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnPDF") as ASPxButton;
            ck.ClientSideEvents.Click = "function(s,e){gvDados.SetFocusedRowIndex(" + e.VisibleIndex + "); e.processOnServer = true;}";
        }
    }
}