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

public partial class grafico_003 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;
        
    public int alturaGrafico = 220;

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

        cDados.aplicaEstiloVisual(this);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        bool linkQuestoes = false, linkRiscos = false;


        linkQuestoes = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2");
        linkRiscos = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1");
        

        defineTamanhoObjetos();

        //Função que gera a grade de riscos
        geraGradeRiscos(linkRiscos);

        //Função que gera a grade de questões
        geraGradeQuestoes(linkQuestoes);

        DataSet ds = cDados.getParametrosSistema("labelQuestoes");

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            pQuestoes.HeaderText = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
        }

        cDados.defineAlturaFrames(this, 55);
    }

    //Carrega o grade de riscos
    private void geraGradeRiscos(bool link)
    {
        DataSet dsRiscos = cDados.getGradeRiscosQuestoes(codigoProjeto, 'R', "");

        if (cDados.DataSetOk(dsRiscos) && cDados.DataTableOk(dsRiscos.Tables[0]))
        {
            DataTable dt = dsRiscos.Tables[0];
            hlRisco_1_1.Text = dt.Select("Coluna = 1 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlRisco_1_2.Text = dt.Select("Coluna = 2 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlRisco_1_3.Text = dt.Select("Coluna = 3 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlRisco_2_1.Text = dt.Select("Coluna = 1 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlRisco_2_2.Text = dt.Select("Coluna = 2 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlRisco_2_3.Text = dt.Select("Coluna = 3 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlRisco_3_1.Text = dt.Select("Coluna = 1 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 3")[0]["Quantidade"].ToString() : "0";
            hlRisco_3_2.Text = dt.Select("Coluna = 2 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 3")[0]["Quantidade"].ToString() : "0";
            hlRisco_3_3.Text = dt.Select("Coluna = 3 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 3")[0]["Quantidade"].ToString() : "0";

            if (link)
            {
                hlRisco_1_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_1_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_1_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_2_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_2_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_2_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_3_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_3_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlRisco_3_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=R&Status=Aberta&IDProjeto=" + codigoProjeto : "";

                hlRisco_1_1.Target = "framePrincipal";
                hlRisco_1_2.Target = "framePrincipal";
                hlRisco_1_3.Target = "framePrincipal";
                hlRisco_2_1.Target = "framePrincipal";
                hlRisco_2_2.Target = "framePrincipal";
                hlRisco_2_3.Target = "framePrincipal";
                hlRisco_3_1.Target = "framePrincipal";
                hlRisco_3_2.Target = "framePrincipal";
                hlRisco_3_3.Target = "framePrincipal";
            }
        }
    }

    //Carrega o grade de Questões
    private void geraGradeQuestoes(bool link)
    {
        DataSet dsQuestoes = cDados.getGradeRiscosQuestoes(codigoProjeto, 'Q', "");

        if (cDados.DataSetOk(dsQuestoes) && cDados.DataTableOk(dsQuestoes.Tables[0]))
        {
            DataTable dt = dsQuestoes.Tables[0];
            hlQuestao_1_1.Text = dt.Select("Coluna = 1 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlQuestao_1_2.Text = dt.Select("Coluna = 2 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlQuestao_1_3.Text = dt.Select("Coluna = 3 AND Linha = 1").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 1")[0]["Quantidade"].ToString() : "0";
            hlQuestao_2_1.Text = dt.Select("Coluna = 1 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlQuestao_2_2.Text = dt.Select("Coluna = 2 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlQuestao_2_3.Text = dt.Select("Coluna = 3 AND Linha = 2").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 2")[0]["Quantidade"].ToString() : "0";
            hlQuestao_3_1.Text = dt.Select("Coluna = 1 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 1 AND Linha = 3")[0]["Quantidade"].ToString() : "0";
            hlQuestao_3_2.Text = dt.Select("Coluna = 2 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 2 AND Linha = 3")[0]["Quantidade"].ToString() : "0";
            hlQuestao_3_3.Text = dt.Select("Coluna = 3 AND Linha = 3").Length > 0 ? dt.Select("Coluna = 3 AND Linha = 3")[0]["Quantidade"].ToString() : "0";

            if (link)
            {
                hlQuestao_1_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_1_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_1_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 1").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_2_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_2_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_2_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 2").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_3_1.NavigateUrl = dt.Select("Coluna = 1 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_3_2.NavigateUrl = dt.Select("Coluna = 2 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";
                hlQuestao_3_3.NavigateUrl = dt.Select("Coluna = 3 AND Linha = 3").Length > 0 ? "../riscos.aspx?Publicado=SIM&TT=Q&Status=Aberta&IDProjeto=" + codigoProjeto : "";

                hlQuestao_1_1.Target = "framePrincipal";
                hlQuestao_1_2.Target = "framePrincipal";
                hlQuestao_1_3.Target = "framePrincipal";
                hlQuestao_2_1.Target = "framePrincipal";
                hlQuestao_2_2.Target = "framePrincipal";
                hlQuestao_2_3.Target = "framePrincipal";
                hlQuestao_3_1.Target = "framePrincipal";
                hlQuestao_3_2.Target = "framePrincipal";
                hlQuestao_3_3.Target = "framePrincipal";
            }
        }
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = (((largura) / 2 - 105) / 2) - 5;
        int alturaPaineis = altura - 235;


        pRiscos.Width = new Unit("100%");
        
        pQuestoes.Width = new Unit("100%");

        alturaGrafico = (altura - 304) / 2;
    }
}
