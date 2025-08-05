/*
 16/12/2010: Mudança by Alejandro: 
            Desabilitar o CommandButton 'Excluir', quando a relação entre objetivo-indicador tenha
            uma outra relação con projeto.
            função adicionado: 'gvDados_CommandButtonInitialize()'
            
 */
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
using System.Text;
using DevExpress.Web;
using System.Drawing;

public partial class _Estrategias_wizard_indicadorObjetivo : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int alturaPrincipal = 0;

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        cDados.aplicaEstiloVisual(Page);
        carregaMapasEstrategicos();
        populaGrid();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 230;
    }

    #endregion

    #region COMBOBOX

    private void carregaMapasEstrategicos()
    {
        //DataSet ds = cDados.getMapasEstrategicos(codigoEntidade, "");
        string where = string.Format(@"
                AND IndicaMapaEstrategicoAtivo = 'S' 
                AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Vsl') = 1
                ", dbName, dbOwner, idUsuarioLogado, codigoEntidade); 
        string comandoSQL = cDados.getSelect_MapaEstrategico(codigoEntidade, idUsuarioLogado, where);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlMapa.DataSource = ds;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();

            if (!IsPostBack && ddlMapa.Items.Count > 0)
                ddlMapa.SelectedIndex = 0;
        }
    }

    #endregion

    #region GRID VIEW

    private void populaGrid()
    {
        if (ddlMapa.Items.Count > 0)
        {
            DataSet ds = cDados.getIndicadoresObjetivosUnidade(codigoEntidade, " AND me.CodigoMapaEstrategico = " + ddlMapa.Value);

            if (cDados.DataSetOk(ds))
            {
                gvDados.DataSource = ds.Tables[0];
                gvDados.DataBind();
            }

            string captionGrid = "<table style='width:100%'><tr>";
            captionGrid += "<td align='left'><img src='../../imagens/botoes/incluirReg02.png' alt='Novo' onclick='gvDados.AddNewRow();' style='cursor: pointer;'/></td>";
            captionGrid += string.Format(@"</tr></table>");
            gvDados.SettingsText.Title = captionGrid;
        }
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int registrosAfetados = 0;
        string mensagemErro = ""; 

        string codigoErro = "";
        bool retorno = cDados.excluiIndicadoresOE(int.Parse(e.Keys[0].ToString()), int.Parse(e.Values["CodigoObjetoEstrategia"].ToString()), ref registrosAfetados 
                                                    , ref codigoErro , ref mensagemErro);
        if (registrosAfetados == 0)
        {
            mensagemErro = "Nenhum indicador foi desvinculado do objetivo estratégico nesta ação,/n para maiores informações contate o administrador do sistema.";
        }

        if (mensagemErro != "")
        {
            throw new Exception(mensagemErro);
        }
        else
        {
            populaGrid();
            e.Cancel = true;
            gvDados.CancelEdit();
        }
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (!gvDados.IsEditing)
            return; //retorno

        if (e.Column.Name == "mapa")
        {           
            ASPxTextBox txt = e.Editor as ASPxTextBox;
            txt.Text = ddlMapa.Text;
        }
        else if (e.Column.Name == "objetivo")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            DataSet ds = cDados.getObjetivoEstrategico(null, null, " AND obj.DataExclusao IS NULL AND obj.CodigoMapaEstrategico = " + ddlMapa.Value);

            if (cDados.DataSetOk(ds))
            {
                combo.DataSource = ds;
                combo.TextField = "DescricaoObjetoEstrategia";
                combo.ValueField = "CodigoObjetivoEstrategico";
                combo.DataBind();
            }
        }
        else if (e.Column.Name == "indicador")
        {
            int codigoMapa = -1;

            if (ddlMapa.SelectedIndex != -1)
                codigoMapa = int.Parse(ddlMapa.Value.ToString());

            ASPxComboBox combo = e.Editor as ASPxComboBox;
            DataSet ds = cDados.getIndicadoresNaoAssociados(codigoEntidade, codigoMapa, "");

            if (cDados.DataSetOk(ds))
            {
                combo.DataSource = ds;
                combo.TextField = "NomeIndicador";
                combo.ValueField = "CodigoIndicador";
                combo.DataBind();
            }
        }
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues[1] != null && e.NewValues[0] != null)
        {
            int registrosAfectados = 0;

            bool retorno = cDados.incluiIndicadorOE(    int.Parse(e.NewValues[1].ToString())
                                                    ,   int.Parse(e.NewValues[0].ToString())
                                                    ,   ref registrosAfectados);
            populaGrid();           
        }

        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        
    }

    protected void gvDados_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        string codigoProjeto = gvDados.GetRowValues(e.VisibleIndex, "CodigoProjeto").ToString();
        if (codigoProjeto != "")
        {
            e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            e.Text = "";
        }
    }

    #endregion
}
