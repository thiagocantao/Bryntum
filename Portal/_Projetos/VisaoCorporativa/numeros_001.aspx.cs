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

public partial class _Projetos_VisaoCorporativa_numeros_001 : System.Web.UI.Page
{
    dados cDados;

    public string styleDisplayReceita = "";
    public string styleDisplayEsforco = "";
    public string styleDisplayDespesa = "";

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

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {            
            if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N")
            {
                styleDisplayReceita = "style='display:none'";
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N")
            {
                styleDisplayDespesa = "style='display:none'";
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresEsforco"].ToString() == "N")
            {
                styleDisplayEsforco = "style='display:none'";
            }
        }

        defineLarguraTela();

        string where = "";

        //if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
        //    where = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        int anoFinanceiro = Request.QueryString["Financeiro"] != null && Request.QueryString["Financeiro"].ToString() != "" ? int.Parse(Request.QueryString["Financeiro"].ToString()) : -1;
        
        DataSet ds = cDados.getNumerosVisaoProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), anoFinanceiro, where);

        if (cDados.DataSetOk(ds))
        {
            //Preenche o label com a quantidade de projetos da instituição
            lblQtdProjetos.Text = ds.Tables[0].Rows[0]["qtdProjetos"].ToString();

            getInfoReceita(ds.Tables[0]);
            getInfoDespesa(ds.Tables[0]);
            getInfoEsforco(ds.Tables[0]);
        }

        lblTituloProjetos.Attributes.Add("onclick", "window.open('../ResumoProjetos.aspx?Status=3', '_top');");
        lblTituloProjetos.Style.Add("cursor", "pointer");

        lblQtdProjetos.Attributes.Add("onclick", "window.open('../ResumoProjetos.aspx?Status=3', '_top');");
        lblQtdProjetos.Style.Add("cursor", "pointer");

        defineLabels();
    }

    private void defineLabels()
    {
        DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "labelDespesa");

        string labelDespesa = "Despesa";

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["labelDespesa"].ToString() != "")
            labelDespesa = dsParam.Tables[0].Rows[0]["labelDespesa"].ToString();

        lblTituloCustoOrcadoData.Text = labelDespesa + ":";
        lblTituloCustoRealData.Text = labelDespesa + " (Realizado):";

        lblTituloCustoOrcado.Text = labelDespesa + ":";
        lblTituloCustoReal.Text = labelDespesa + " (Realizado):";

    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //ASPxRoundPanel1.Width = 250;
        ASPxRoundPanel1.ContentHeight = (altura - 249);

        lblData.Text = string.Format("Até {0:dd/MM/yyyy}", DateTime.Now.Date);
    }

    private void getInfoReceita(DataTable dt)
    {
        //Preenche o label com a Receita Orçada até a data, formato da moeda configurado no Servidor
        lblReceitaOrcadaData.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["ReceitaOrcadoData"].ToString()));

        //Preenche o label com a Receita Orçada total, formato da moeda configurado no Servidor
        lblReceitaOrcadaTotal.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["ReceitaOrcadoTotal"].ToString()));

        //Preenche o label com a Receita Realizada total, formato da moeda configurado no Servidor
        lblReceitaRealTotal.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["ReceitaRealTotal"].ToString()));

        if (float.Parse(dt.Rows[0]["ReceitaOrcadoData"].ToString()) > 0)
            lblPercentualReceitaData.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["ReceitaRealData"].ToString()) / double.Parse(dt.Rows[0]["ReceitaOrcadoData"].ToString()));
        else
            lblPercentualReceitaData.Text = string.Format("{0:p}", 0);

        if (float.Parse(dt.Rows[0]["ReceitaOrcadoTotal"].ToString()) > 0)
            lblPercentualReceitaTotal.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["ReceitaRealTotal"].ToString()) / double.Parse(dt.Rows[0]["ReceitaOrcadoTotal"].ToString()));
        else
            lblPercentualReceitaTotal.Text = string.Format("{0:p}", 0);

        //Preenche o label com a Receita Realizada até a data (Mesmo valor da receita realizada total), formato da moeda configurado no Servidor
        lblReceitaRealData.Text = lblReceitaRealTotal.Text;
    }

    private void getInfoDespesa(DataTable dt)
    {
        //Preenche o label com o Custo Orçado até a data, formatando para a moeda configurado no Servidor
        lblCustoOrcadoData.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["CustoOrcadoData"].ToString()));

        //Preenche o label com o Custo Orçado total, formatando para a moeda configurado no Servidor
        lblCustoOrcadoTotal.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["CustoOrcadoTotal"].ToString()));

        //Preenche o label com o Custo Realizado total, formatando para a moeda configurado no Servidor
        lblCustoRealTotal.Text = string.Format("{0:c0}", double.Parse(dt.Rows[0]["CustoRealTotal"].ToString()));

        //---------- Percentuais (verifica cada valor para não executar divisões por zero e configura todos os valor para percentual)
        if (float.Parse(dt.Rows[0]["CustoOrcadoData"].ToString()) > 0)
            lblPercentualCustoData.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["CustoRealData"].ToString()) / double.Parse(dt.Rows[0]["CustoOrcadoData"].ToString()));
        else
            lblPercentualCustoData.Text = string.Format("{0:p}", 0);

        if (float.Parse(dt.Rows[0]["CustoOrcadoTotal"].ToString()) > 0)
            lblPercentualCustoTotal.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["CustoRealTotal"].ToString()) / double.Parse(dt.Rows[0]["CustoOrcadoTotal"].ToString()));
        else
            lblPercentualCustoTotal.Text = string.Format("{0:p}", 0);

        //Preenche o label com o Custo Realizado até a data(mesmo valor do custo real total), formato da moeda configurado no Servidor
        lblCustoRealData.Text = lblCustoRealTotal.Text;

    }

    private void getInfoEsforco(DataTable dt)
    {
        //Preenche o label com o Esforço previsto até a data(em horas), formato de números configurado no Servidor
        lblEsforcoPrevData.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoOrcadoData"].ToString()));

        //Preenche o label com o Esforço previsto total(em horas), formato de números configurado no Servidor
        lblEsforcoPrevTotal.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoOrcadoTotal"].ToString()));

        //Preenche o label com o Esforço realizado total(em horas), formato de números configurado no Servidor
        lblEsforcoRealTotal.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoRealTotal"].ToString()));

        //Preenche o label com o Esforço previsto até a data(em horas), formato de números configurado no Servidor
        lblEsforcoPrevData.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoOrcadoData"].ToString()));

        //Preenche o label com o Esforço previsto total(em horas), formato de números configurado no Servidor
        lblEsforcoPrevTotal.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoOrcadoTotal"].ToString()));

        //Preenche o label com o Esforço realizado total(em horas), formato de números configurado no Servidor
        lblEsforcoRealTotal.Text = string.Format("{0:n2} h", double.Parse(dt.Rows[0]["TrabalhoRealTotal"].ToString()));

        if (double.Parse(dt.Rows[0]["TrabalhoOrcadoData"].ToString()) > 0)
            lblPercentualEsforcoData.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["TrabalhoRealData"].ToString()) / double.Parse(dt.Rows[0]["TrabalhoOrcadoData"].ToString()));
        else
            lblPercentualEsforcoData.Text = string.Format("{0:p}", 0);

        if (double.Parse(dt.Rows[0]["TrabalhoOrcadoTotal"].ToString()) > 0)
            lblPercentualEsforcoTotal.Text = string.Format("{0:p}", double.Parse(dt.Rows[0]["TrabalhoRealTotal"].ToString()) / double.Parse(dt.Rows[0]["TrabalhoOrcadoTotal"].ToString()));
        else
            lblPercentualEsforcoTotal.Text = string.Format("{0:p}", 0);

        lblEsforcoRealData.Text = lblEsforcoRealTotal.Text;
    }
}
