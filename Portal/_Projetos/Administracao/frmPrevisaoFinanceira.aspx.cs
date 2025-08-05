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

public partial class administracao_frmPrevisaoFinanceira : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string textoStatus = "";

    int codigoContrato = -1;
    public string somenteLeitura = "";
    public bool mostraMsg = false;

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

        string IniciaisTipoAssociacao = "CT";

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            
        }

        if (Request.QueryString["CC"] != null)
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();
      
        //somenteLeitura = "S";
        if (somenteLeitura == "S" )
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
        }
        else
        {

            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncPrv");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcPrv");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltPrv");

            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }

        //if (!IsPostBack)
        cDados.aplicaEstiloVisual(Page);
       
    
            carregaGvDados();
     

        getValorAtualContrato(); 

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        DataSet ds = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtNumeroContrato.Text = ds.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim() == "" ? ((ds.Tables[0].Rows[0]["StatusContrato"].ToString().ToUpper().Trim() == "A") ? "Ativo" : "Inativo") : ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim();
            txtInicioVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataInicio"].ToString()).ToString("dd/MM/yyyy");
            txtTerminoVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataTermino"].ToString()).ToString("dd/MM/yyyy");

        }
    }


    private void getValorAtualContrato()
    {
        DataSet ds = cDados.getInformacoesContrato(codigoContrato);

        DataSet ds1 = cDados.getSomaPrevisaoFinanceira(codigoContrato);
        float valorContrato = 0;
        float valorParcelas = 0;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            valorContrato = ds.Tables[0].Rows[0]["ValorContrato"] + "" != "" ? float.Parse(ds.Tables[0].Rows[0]["ValorContrato"].ToString()) : 0;
            pnCallback.JSProperties["cp_ValorContrato"] = ds.Tables[0].Rows[0]["ValorContrato"].ToString();
            pnCallback.JSProperties["cp_TerminoContrato"] = ds.Tables[0].Rows[0]["DataTermino"].ToString();
            pnCallback.JSProperties["cp_StatusContrato"] = ds.Tables[0].Rows[0]["StatusContrato"].ToString();


            if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                valorParcelas = ds1.Tables[0].Rows[0]["Soma"] + "" != "" ? float.Parse(ds1.Tables[0].Rows[0]["Soma"].ToString()) : 0;
                
            }
        }

        if (valorContrato < valorParcelas)
            textoStatus = "Atenção! A soma dos valores de previsão é maior que o valor do contrato!";
        else if (valorContrato > valorParcelas)
            textoStatus = "Atenção! A soma dos valores de previsão é menor que o valor do contrato!";
        else
            textoStatus = "";
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

    }
       
    #endregion

   
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getInformacoesContrato(codigoContrato);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["StatusContrato"].ToString() == "I")
            {
                podeIncluir = false;
            }
        }

        DataSet dsPrevisao = cDados.getPrevisaoFinanceira(" AND CodigoContrato = " + codigoContrato);

        if ((cDados.DataSetOk(dsPrevisao)))
        {
            gvDados.DataSource = dsPrevisao;
            gvDados.DataBind();
        }        
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir )
            {
                e.Enabled = true;
            }
            else if (podeExcluir )
            {
                e.Enabled = true;
            }
            else
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

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
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

        getValorAtualContrato();
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

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msgStatusGravacao = "";
        string valorPrevisao = txtValorPrevisao.Text;
        string dataPrevisao = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevisao.Date);
        string observacoes = mmObservacao.Text;

        bool result = cDados.incluirPrevisaoFinanceiraContratoObra(codigoContrato, valorPrevisao, dataPrevisao, observacoes, codigoUsuarioResponsavel,  ref msgStatusGravacao);

        if (result == false)
            return msgStatusGravacao;
        else
        {
           
            carregaGvDados();
            return "";
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoPrevisao = int.Parse(getChavePrimaria());

        string msgStatusGravacao = "";

        string valorPrevisao = txtValorPrevisao.Text;
        string dataPrevisao = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevisao.Date);
        string observacoes = mmObservacao.Text;

        bool result = cDados.atualizaPrevisaoFinanceiraContratoObra(codigoPrevisao, codigoContrato, valorPrevisao, dataPrevisao, observacoes,  ref msgStatusGravacao);
        
        if (result == false)
            return msgStatusGravacao;
        else
        {
            carregaGvDados();
            return "";
        }    
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoPrevisao = int.Parse(getChavePrimaria());
        string msgStatusGravacao = "";

        bool result = cDados.excluiPrevisaoFinanceiraContratoObra(codigoPrevisao, codigoUsuarioResponsavel, ref msgStatusGravacao); ;

        if (result == false)
            return msgStatusGravacao;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
    }


    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "ObservacoesPrevisaoFinanceira")
        {
            if (e.CellValue.ToString().Length > 125)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 125) + "...";
            }
        }
    }

    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.eventoClickMenu((source as ASPxMenu), parameter, gvDados, "CadRecCorp");
    }

    protected void menu_ItemClick1(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "CadPrevFin");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "CadPrevFin", "Previsão financeira", this);
    }
}
