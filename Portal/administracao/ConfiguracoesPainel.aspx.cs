using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_ConfiguracoesPainel : System.Web.UI.Page
{
    dados cDados;
    string iniciaisPainel = "";
    int codigoUsuarioResponsavel, codigoEntidade;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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

        iniciaisPainel = Request.QueryString["INI"] + "";
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaCombos();

        if (!IsPostBack)
            carregaInformacoesPainel();
    }

    private void carregaCombos()
    {
        DataSet ds = cDados.getGraficosDisponiveisPainel(codigoEntidade, iniciaisPainel);

        carregaComboPosicao(ds, 1);
        carregaComboPosicao(ds, 2);
        carregaComboPosicao(ds, 3);
        carregaComboPosicao(ds, 4);
        carregaComboPosicao(ds, 5);
        carregaComboPosicao(ds, 6);
    }

    private void carregaComboPosicao(DataSet ds, int numeroPosicao)
    {
        ASPxComboBox cbGrafico = Page.FindControl("ddlPos" + numeroPosicao) as ASPxComboBox;
        cbGrafico.DataSource = ds;
        cbGrafico.TextField = "TituloGrafico";
        cbGrafico.ValueField = "CodigoGrafico";
        cbGrafico.DataBind();

        ListEditItem lei = new ListEditItem("Não Exibir", "-1");
        cbGrafico.Items.Insert(0, lei);
    }

    private void carregaInformacoesPainel()
    {
        DataSet ds = cDados.getPainelUsuario(codigoEntidade, codigoUsuarioResponsavel, iniciaisPainel);

        setaValoresPosicao(ds, 1);
        setaValoresPosicao(ds, 2);
        setaValoresPosicao(ds, 3);
        setaValoresPosicao(ds, 4);
        setaValoresPosicao(ds, 5);
        setaValoresPosicao(ds, 6);

    }

    private void setaValoresPosicao(DataSet ds, int numeroPosicao)
    {
        ASPxComboBox cbGrafico = Page.FindControl("ddlPos" + numeroPosicao) as ASPxComboBox;
        ASPxComboBox cbLinhas = Page.FindControl("ddlLinhas" + numeroPosicao) as ASPxComboBox;
        ASPxComboBox cbColunas = Page.FindControl("ddlColunas" + numeroPosicao) as ASPxComboBox;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow[] drs = ds.Tables[0].Select("Posicao = " + numeroPosicao);            

            if (drs.Length > 0)
            {
                cbGrafico.Value = drs[0]["CodigoGrafico"].ToString();
                cbLinhas.Value = drs[0]["RowSpan"].ToString();
                cbColunas.Value = drs[0]["ColSpan"].ToString();
            }
            else
            {
                cbGrafico.Value = "-1";
                cbLinhas.Value = "1";
                cbColunas.Value = "1";
            }
        }
        else
        {
            cbGrafico.Value = "-1";
            cbLinhas.Value = "1";
            cbColunas.Value = "1";
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = string.Format(@"DELETE FROM {0}.{1}.GraficosUsuario 
                                                  WHERE CodigoUsuario = {2} 
                                                    AND CodigoGrafico IN(SELECT g.CodigoGrafico 
                                                                           FROM {0}.{1}.Graficos g 
                                                                          WHERE IniciaisPainel = '{3}')
                                            ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, iniciaisPainel);

        if (e.Parameter != "Default")
        {
            comandoSQL += getInsertGrafico(1);
            comandoSQL += getInsertGrafico(2);
            comandoSQL += getInsertGrafico(3);
            comandoSQL += getInsertGrafico(4);
            comandoSQL += getInsertGrafico(5);
            comandoSQL += getInsertGrafico(6);
        }

        int regAf = 0;

        cDados.execSQL(comandoSQL, ref regAf);
    }

    private string getInsertGrafico(int numeroPosicao)
    {
        string comandoInsert = "";
        ASPxComboBox cbGrafico = Page.FindControl("ddlPos" + numeroPosicao) as ASPxComboBox;
        ASPxComboBox cbLinhas = Page.FindControl("ddlLinhas" + numeroPosicao) as ASPxComboBox;
        ASPxComboBox cbColunas = Page.FindControl("ddlColunas" + numeroPosicao) as ASPxComboBox;

        if (cbGrafico.Value != null && cbGrafico.Value.ToString() != "-1")
        {
            comandoInsert = string.Format(@"INSERT INTO {0}.{1}.GraficosUsuario(CodigoGrafico, CodigoUsuario, Posicao, ColSpan, RowSpan)
                                                                         VALUES({2}, {3}, {4}, {5}, {6});
                                            ", cDados.getDbName()
                                             , cDados.getDbOwner()
                                             , cbGrafico.Value
                                             , codigoUsuarioResponsavel
                                             , numeroPosicao
                                             , cbColunas.Value
                                             , cbLinhas.Value);
        }

        return comandoInsert;
    }
}