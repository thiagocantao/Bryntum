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
using System.Text.RegularExpressions;
using DevExpress.Web;

public partial class _Estrategias_wizard_PermissaoObjetoEstrategia : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;
    private string idUsuarioResponsavel;
    private string idEntidadeUsuario;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

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

        idUsuarioResponsavel = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idEntidadeUsuario = cDados.getInfoSistema("CodigoEntidade").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            hfGeral.Set("iniciaisTO", "");
            hfGeral.Set("idObjeto", "");
            hfGeral.Set("tituloObjeto", "");
            hfGeral.Set("filtrarPor", "");
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }
        else
        {
            string iniciaisTO = hfGeral.Get("iniciaisTO").ToString();

            if (iniciaisTO != "")
                carregarComboFiltraPor(iniciaisTO);
        }

    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 125;
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PermissaoObjetoEstrategia.js""></script>"));
        this.TH(this.TS("PermissaoObjetoEstrategia"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }

    #endregion

    #region COMBOBOX

    protected void ddlFiltarPor_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
       /// string parametro = e.Parameter;
       /// carregarComboFiltraPor();
    }

    private void carregarComboFiltraPor(string iniciaisTO)
    {
        if (iniciaisTO.Equals("MAP"))
        {
            if (ddlFiltarPor.Items.Count <= 0)
            {
                ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
                ddlFiltarPor.Items.Insert(0, lei);
                ddlFiltarPor.SelectedIndex = 0;
                hfGeral.Set("SelecionaPrimeiro", "1");
            }
        }
        else
        {
            bool selecionaPrimeiro =  hfGeral.Contains("SelecionaPrimeiro") && (hfGeral.Get("SelecionaPrimeiro") + "" == "1");
            DataSet ds = cDados.getMapaEstrategico(idEntidadeUsuario, "");

            if (cDados.DataSetOk(ds))
            {
                ddlFiltarPor.DataSource = ds.Tables[0];
                ddlFiltarPor.DataBind();

                if(selecionaPrimeiro)
                    ddlFiltarPor.SelectedIndex = -1;
               // else
               //     ddlFiltarPor.SelectedIndex = 0;
                hfGeral.Set("SelecionaPrimeiro", "0");
            }
        }
    }

    #endregion 

    #region GVDADOS

    private void carregarGvDados(string codigoME, string iniciaisTO, string selectSql)
    {
        DataSet ds = cDados.getObjetosParaDarPermissao(iniciaisTO, idEntidadeUsuario, codigoME, selectSql);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            if (gvDados.VisibleRowCount > 0)
                gvDados.FocusedRowIndex = 0;
        }
    }

    private void carregarGvDadosIndicadores(string codigoME)
    {
        DataSet ds = cDados.getObjetoIndicadorParaDarPermissao(idEntidadeUsuario, codigoME);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            if (gvDados.VisibleRowCount > 0)
                gvDados.FocusedRowIndex = 0;
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string codigoME = e.Parameters;
        string tipoObjeto = hfGeral.Get("iniciaisTO").ToString();

        if (codigoME == null)
        {
            gvDados.DataSource = null;
            gvDados.DataBind();
        }
        else{
            if (tipoObjeto != "IND")
            {
                carregarGvDados(codigoME, hfGeral.Get("iniciaisTO").ToString(), ", oe.* ");
            }
            else
            {
                carregarGvDadosIndicadores(codigoME);
            }
        }

    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        string selectCampos = getSelectSegundoTipoObjeto(hfGeral.Get("iniciaisTO").ToString());
        string tipoObjeto = hfGeral.Get("iniciaisTO").ToString();


        if (ddlFiltarPor.Value != null)
        {
            if (tipoObjeto != "IND")
            {
                carregarGvDados(ddlFiltarPor.Value.ToString(), hfGeral.Get("iniciaisTO").ToString(), selectCampos);
            }
            else
            {
                carregarGvDadosIndicadores(ddlFiltarPor.Value.ToString());
            }
        }
        else
        {
            if (tipoObjeto != "IND")
            {
                carregarGvDados("-1", hfGeral.Get("iniciaisTO").ToString(), selectCampos);
            }
            else
            {
                carregarGvDadosIndicadores("-1");
            }
        }
    }

    private string getSelectSegundoTipoObjeto(string parametro)
    {
        string selectSql = "";

        if (parametro.Equals("MAP"))
            selectSql = @"  , oe.TituloObjetoEstrategia --, oe.DescricaoObjetoEstrategia
                         ";
        else if (parametro.Equals("PSP"))
            selectSql = @"  , oe.TituloObjetoEstrategia --, oe.DescricaoObjetoEstrategia
                         ";
        else if (parametro.Equals("TEM"))
            selectSql = @"--, oe.TituloObjetoEstrategia , oe.DescricaoObjetoEstrategia AS TituloObjetoEstrategia
                         ";
        else if (parametro.Equals("OBJ"))
            selectSql = @"--, oe.TituloObjetoEstrategia , oe.DescricaoObjetoEstrategia AS TituloObjetoEstrategia
                         ";
        return selectSql;
    }

    #endregion

    #region CALLBACK'S

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        string selectCampos = getSelectSegundoTipoObjeto(parametro);

        Regex reg = new Regex("[0-9]"); //Expresión que solo acepta números.
        string iniciaisTO = hfGeral.Get("iniciaisTO").ToString();

        carregarComboFiltraPor(iniciaisTO);

        string aux = (ddlFiltarPor.Value == null ? "" : ddlFiltarPor.Value.ToString());

        if (parametro != "IND")
        {
            if (reg.IsMatch(aux)) //reg.IsMatch(parametro))
                carregarGvDados(aux, iniciaisTO, selectCampos);
            else
                carregarGvDados("-1", iniciaisTO, selectCampos);
        }
        else
        {
            if (reg.IsMatch(aux)) //reg.IsMatch(parametro))
                carregarGvDadosIndicadores(aux);
            else
                carregarGvDadosIndicadores("-1");
        }
    }

    #endregion
}

