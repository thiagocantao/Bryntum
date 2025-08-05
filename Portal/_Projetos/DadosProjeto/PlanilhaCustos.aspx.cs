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
using DevExpress.Web;
using System.IO;
using DevExpress.XtraPrinting;

public partial class _Projetos_DadosProjeto_PlanilhaCustos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    public string mostraTDProjeto = "";
    private string resolucaoCliente = "";

    int codigoObjeto = -1;
    int codigoWorkflow = -1;
    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    int anoCorrente = -1;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());        
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        //if (!IsPostBack)
        //{            
        cDados.aplicaEstiloVisual(Page);
        //}
        
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoObjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        if (codigoObjeto == -1)
        {
            try
            {
                int codigoInstanciaWf = int.Parse(Request.QueryString["CI"].ToString());
                codigoObjeto = cDados.getCodigoProjetoInstanciaFluxo(codigoWorkflow, codigoInstanciaWf);
                gvDados.JSProperties["cp_CodigoProjeto"] = codigoObjeto;
            }
            catch { }
        }
        
        gvDados.JSProperties["cp_CodigoProjeto"] = codigoObjeto;
        gvDados.JSProperties["cp_CodigoWorkflow"] = codigoWorkflow;
        gvDados.JSProperties["cp_CodigoLB"] = ddlLinhaBase.SelectedIndex == -1 ? Convert.ToInt32(ddlLinhaBase.Value) : -1;


        carregaComboLinhaBase();

        if (ddlLinhaBase.SelectedIndex != -1)
        {
            anoCorrente = cDados.getAnoCorrentePlanilhaCustosProjeto(codigoObjeto, int.Parse(ddlLinhaBase.Value.ToString()));
            gvDados.JSProperties["cp_anoCorrente"] = anoCorrente;
            gvDados.Columns["ValorRequeridoAnoCorrente"].Caption = string.Format("Requerido {0} (R$)", anoCorrente);
            gvDados.Columns["ValorRequeridoAnoSeguinte"].Caption = string.Format("Requerido {0} (R$)", anoCorrente + 1);
            gvDados.Columns["ValorRequeridoPosAnoSeguinte"].Caption = string.Format("Requerido {0} (R$)", anoCorrente + 2);
            gvDados.JSProperties["cp_anoCorrente"] = anoCorrente;
        }

        if ((Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S") || ddlLinhaBase.SelectedIndex == -1 || ddlLinhaBase.Value.ToString() != "-1")
        {
            podeIncluir = false;
            podeEditar = false;
            podeExcluir = false;
        }

        //txtValorRequeridoAnoCorrente.JSProperties["cp_AnoCorrente"] = anoCorrente;
        callback.JSProperties["cp_DescricaoItem"] = "";

        carregaGvDados();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PlanilhaCustos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "PlanilhaCustos", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);
        
        //ALT = 628 & Altura = 628

        int alturaVindaDoFluxo = alturaPrincipal;
        int.TryParse(Request.QueryString["ALT"] + "", out alturaVindaDoFluxo);
        
        if (codigoWorkflow != -1)
            gvDados.Settings.VerticalScrollableHeight = alturaVindaDoFluxo;
        else
            gvDados.Settings.VerticalScrollableHeight = altura + 20;
    }
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        string where = "";

        if (ddlLinhaBase.SelectedIndex != -1)
        {
            DataSet ds = cDados.getPlanilhaCustosProjeto(codigoObjeto, int.Parse(ddlLinhaBase.Value.ToString()), where);

            if ((cDados.DataSetOk(ds)))
            {
                gvDados.DataSource = ds;
                gvDados.DataBind();
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {        
        if (e.ButtonID == "btnEditar")
        {
            if (!podeEditar)
            {               
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }else 
        if (e.ButtonID == "btnExcluir")
        {
            if (!podeExcluir)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion
    
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    
    
    

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoItemOrcamento = int.Parse(getChavePrimaria());

        bool result = cDados.excluiItemPlanilhaCustosProjeto(codigoItemOrcamento, codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #endregion

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    private void carregaComboLinhaBase()
    {
        int index = 0;

        DataSet ds = cDados.getLinhasBasesPlanilhaCustosProjeto(codigoObjeto, "");

        ddlLinhaBase.DataSource = ds;
        ddlLinhaBase.TextField = "VersaoLinhaBase";
        ddlLinhaBase.ValueField = "CodigoLinhaBase";
        ddlLinhaBase.DataBind();
        //StatusAprovacao, DataStatusAprovacao
        if (codigoWorkflow != -1)
        {
            ListEditItem lei = new ListEditItem("Nova Versão", "-1");

            ddlLinhaBase.Items.Insert(0, lei);

            ddlLinhaBase.JSProperties["cp_StatusLB0"] = "*Linha de Base PENDENTE de aprovação";

            index = 1;
        }        

        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (dr["StatusAprovacao"].ToString() == "AP")
            {
                ddlLinhaBase.JSProperties["cp_StatusLB" + index] = string.Format("*Linha de Base APROVADA em {0:dd/MM/yyyy}", dr["DataStatusAprovacao"]);
            }
            else if (dr["StatusAprovacao"].ToString() == "RP")
            {
                ddlLinhaBase.JSProperties["cp_StatusLB" + index] = string.Format("*Linha de Base REPROVADA em {0:dd/MM/yyyy}", dr["DataStatusAprovacao"]);
            }
            else
            {
                ddlLinhaBase.JSProperties["cp_StatusLB" + index] = "*Linha de Base PENDENTE de aprovação";
            }

            index++;
        }

        if (!IsPostBack)
        {
            if (codigoWorkflow != -1)
            {
                //lblStatusLinhaBase.Text = "*Linha de Base PENDENTE de aprovação";
                ddlLinhaBase.SelectedIndex = 0;
            }
            else
            {
                DataRow[] drs = ds.Tables[0].Select("StatusAprovacao = 'AP'");

                if (drs.Length == 0)
                    ddlLinhaBase.SelectedIndex = -1;
                else
                    ddlLinhaBase.Value = drs[0]["CodigoLinhaBase"].ToString();
            }
        }

    }






    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        carregaGvDados();

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "Empenhos_Financeiros_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();

                gvExporter.WriteXls(stream, x);
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

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        e.Text = e.Text.Replace("<br>", "");
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

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter == "Download")
        {
            GeraRelatorio();
        }
        else if (e.Parameter != "")
        {
            string valorUnitario = cDados.getCustoUnitarioGrupoRecurso(int.Parse(e.Parameter));

            callback.JSProperties["cp_CustoUnitario"] = valorUnitario == "" ? "0" : valorUnitario.Replace(".", "").Replace(",", ".");

            string where = " AND item.CodigoGrupoRecurso = " + e.Parameter;
            DataSet ds = cDados.getGruposRecursosPlanilhaCustosProjeto(codigoEntidadeUsuarioResponsavel, where);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                callback.JSProperties["cp_DescricaoItem"] = ds.Tables[0].Rows[0]["DetalheGrupo"].ToString();
            }
        }
    }

    private void GeraRelatorio()
    {
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoObjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        if (codigoObjeto == -1)
        {
            try
            {
                int codigoInstanciaWf = int.Parse(Request.QueryString["CI"].ToString());
                codigoObjeto = cDados.getCodigoProjetoInstanciaFluxo(codigoWorkflow, codigoInstanciaWf);
            }
            catch { }
        }
        
        carregaComboLinhaBase();

        relPlanilhaCustosProjeto rel = new relPlanilhaCustosProjeto();
        rel.pCodigoProjeto.Value = codigoObjeto;
        rel.pLinhaBase.Value = ddlLinhaBase.SelectedIndex == -1 ? Convert.ToInt32(ddlLinhaBase.Value) : -1;
        Session["report"] = rel;
    }

    
}
