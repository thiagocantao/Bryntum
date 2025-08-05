using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_popup_adm_AssociacaoDeCR : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private int codigoProjeto;

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
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        bool ret = int.TryParse(Request.QueryString["CP"] + "", out codigoProjeto);
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);

        populaCombo();
        if (!IsPostBack)
        {
            populaDisponiveis();
            populaSelecionados();
            //btnSalvar.ClientEnabled = (crDisponiveis.Items.Count() > 0 || crSelecionados.Items.Count() > 0);
        }
    }

    private void populaDisponiveis()
    {
        string comandoSQL = string.Format(@"select CR, CodigoCR, CodigoReservadoCR, NomeCR from [dbo].[f_Sescoop_ListaCRsDisponiveis]( {0}, {1})", codigoEntidadeLogada, comboAno.Value == null ? "null" : comboAno.Value);
        DataSet ds = cDados.getDataSet(comandoSQL);
        crDisponiveis.TextField = "CR";
        crDisponiveis.ValueField = "CodigoCR";
        crDisponiveis.DataSource = ds.Tables[0];
        crDisponiveis.DataBind();

    }


    private void populaSelecionados()
    {
        string comandoSQL = string.Format(@"select CR, CodigoCR, CodigoReservadoCR, NomeCR from [dbo].[f_Sescoop_GetCRsProjeto]({0}, {1})", codigoProjeto, comboAno.Value == null ? "null" : comboAno.Value.ToString());
        DataSet ds = cDados.getDataSet(comandoSQL);
        crSelecionados.TextField = "CR";
        crSelecionados.ValueField = "CodigoCR";
        crSelecionados.DataSource = ds.Tables[0];
        crSelecionados.DataBind();

    }

    private void populaCombo()
    {
        string comandoSQL = string.Format(@"select Ano, IndicaSelecionado from [dbo].[f_Sescoop_GetAnosCRs]({0})", codigoEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);

        DataRow[] dr = ds.Tables[0].Select("IndicaSelecionado = 'S'");


        var anoSelecionado = "0";

        if (dr.Length > 0)
        {
            anoSelecionado = dr[0]["Ano"].ToString();
        }

        comboAno.TextField = "Ano";
        comboAno.ValueField = "Ano";
        comboAno.DataSource = ds;
        comboAno.DataBind();

        if (!IsPostBack)
        {
            ListEditItem li = comboAno.Items.FindByValue(anoSelecionado);
            comboAno.SelectedIndex = li == null ? -1 : li.Index;
        }
    }

    protected void callbackDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        populaDisponiveis();
    }
    protected void callbackSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        populaSelecionados();
    }
    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";


        if (hfGeral.Contains("Sel_$") == false)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = "Não há nenhum CR disponível para o ano selecionado";
            return;
        }

        string listacrs = hfGeral.Get("Sel_$").ToString();

        string listacrs_pontovirgula = "";

        string[] arrayNomeValor = listacrs.Split('¢');
        for (int i = 0; i < arrayNomeValor.Length; i++)
        {
            string[] arrayCodigoSegundaCasa = arrayNomeValor[i].Split('$');
            listacrs_pontovirgula += arrayCodigoSegundaCasa.Length > 1 ? arrayCodigoSegundaCasa[1] + ";" : ";";
        }
        listacrs_pontovirgula = listacrs_pontovirgula.Substring(0, listacrs_pontovirgula.Length - 1);
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @CodigoProjeto int
        DECLARE @Ano smallint
        DECLARE @listaCRs varchar(max)

        SET @CodigoProjeto = {0}
        SET @Ano = {1}
        SET @listaCRs = '{2}'

        EXECUTE @RC = [dbo].[p_Sescoop_gravaCRsProjeto] 
           @CodigoProjeto
          ,@Ano
          ,@listaCRs
         ", codigoProjeto, comboAno.Value, listacrs_pontovirgula);

        int quantidade = 0;
        try
        {
            bool ret = cDados.execSQL(comandoSQL, ref quantidade);
            if (ret == true)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Associação realizada com sucesso!";
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }
}