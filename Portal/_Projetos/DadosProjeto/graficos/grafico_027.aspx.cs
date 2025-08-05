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

public partial class grafico_027 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;
    public int alturaGrafico = 220;
    private bool podeConsultarRiscos = false;
    private bool podeConsultarQuestoes = false;

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
        
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        podeConsultarRiscos = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1");
        podeConsultarQuestoes = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2");

        defineTamanhoObjetos();

        //Função que gera a grade de riscos
        geraGradeRiscos();

        //Função que gera a grade de questões
        geraGradeQuestoes();

        DataSet ds = cDados.getParametrosSistema("labelQuestoes");

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            pQuestoes.HeaderText = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
        }

        //cDados.defineAlturaFrames(this, 55);
    }

    //Carrega o grade de riscos
    private void geraGradeRiscos()
    {
        DataSet dsRiscos = cDados.getGradeRiscosQuestoes(codigoProjeto, 'R', "");

        if (cDados.DataSetOk(dsRiscos))
        {
            DataTable dt = dsRiscos.Tables[0];
            if (dt.Select("Linha = 1 AND Coluna = 1").Length > 0)
            {
                hlRisco_1_1.Text = dt.Select("Linha = 1 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_1_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=1&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 1 AND Coluna = 2").Length > 0)
            {
                hlRisco_1_2.Text = dt.Select("Linha = 1 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_1_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=1&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 1 AND Coluna = 3").Length > 0)
            {
                hlRisco_1_3.Text = dt.Select("Linha = 1 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_1_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=1&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }

            if (dt.Select("Linha = 2 AND Coluna = 1").Length > 0)
            {
                hlRisco_2_1.Text = dt.Select("Linha = 2 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_2_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=2&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 2 AND Coluna = 2").Length > 0)
            {
                hlRisco_2_2.Text = dt.Select("Linha = 2 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_2_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=2&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 2 AND Coluna = 3").Length > 0)
            {
                hlRisco_2_3.Text = dt.Select("Linha = 2 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_2_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=2&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }

            if (dt.Select("Linha = 3 AND Coluna = 1").Length > 0)
            {
                hlRisco_3_1.Text = dt.Select("Linha = 3 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_3_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=3&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 3 AND Coluna = 2").Length > 0)
            {
                hlRisco_3_2.Text = dt.Select("Linha = 3 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_3_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=3&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 3 AND Coluna = 3").Length > 0)
            {
                hlRisco_3_3.Text = dt.Select("Linha = 3 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarRiscos)
                {
                    hlRisco_3_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&GIU=3&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }
        }
    }

    //Carrega o grade de Questões
    private void geraGradeQuestoes()
    {
        DataSet dsQuestoes = cDados.getGradeRiscosQuestoes(codigoProjeto, 'Q', "");

        if (cDados.DataSetOk(dsQuestoes))
        {
            DataTable dt = dsQuestoes.Tables[0];
            if (dt.Select("Linha = 1 AND Coluna = 1").Length > 0)
            {
                hlQuestao_1_1.Text = dt.Select("Linha = 1 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_1_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=1&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 1 AND Coluna = 2").Length > 0)
            {
                hlQuestao_1_2.Text = dt.Select("Linha = 1 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_1_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=1&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 1 AND Coluna = 3").Length > 0)
            {
                hlQuestao_1_3.Text = dt.Select("Linha = 1 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_1_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=1&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }

            if (dt.Select("Linha = 2 AND Coluna = 1").Length > 0)
            {
                hlQuestao_2_1.Text = dt.Select("Linha = 2 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_2_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=2&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 2 AND Coluna = 2").Length > 0)
            {
                hlQuestao_2_2.Text = dt.Select("Linha = 2 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_2_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=2&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 2 AND Coluna = 3").Length > 0)
            {
                hlQuestao_2_3.Text = dt.Select("Linha = 2 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_2_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=2&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }

            if (dt.Select("Linha = 3 AND Coluna = 1").Length > 0)
            {
                hlQuestao_3_1.Text = dt.Select("Linha = 3 AND Coluna = 1")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_3_1.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=3&GPP=1&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 3 AND Coluna = 2").Length > 0)
            {
                hlQuestao_3_2.Text = dt.Select("Linha = 3 AND Coluna = 2")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_3_2.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=3&GPP=2&IDProjeto=" + codigoProjeto;
                }
            }
            if (dt.Select("Linha = 3 AND Coluna = 3").Length > 0)
            {
                hlQuestao_3_3.Text = dt.Select("Linha = 3 AND Coluna = 3")[0]["Quantidade"].ToString();
                if (podeConsultarQuestoes)
                {
                    hlQuestao_3_3.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&GIU=3&GPP=3&IDProjeto=" + codigoProjeto;
                }
            }
        }
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = (((largura) / 2 - 105) / 2)+2;
        int alturaPaineis = (altura - 307) / 2;

        int larguracelula = (int)(larguraPaineis / 3.3)+3;
        int alturacelula = (int)(alturaPaineis/ 3.5);

        Unit untLarguraCel = new Unit(larguracelula);
        Unit untAlturaCel = new Unit(alturacelula);
        
        pRiscos.Height = new Unit(alturaPaineis);
        pQuestoes.Height = new Unit(alturaPaineis);

        alturaGrafico = (altura - 307) / 2;

        tdRisco_1_1.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_1_2.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_1_3.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_2_1.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_2_2.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_2_3.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_3_1.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_3_2.Style.Add("width", larguracelula.ToString() + "px");
        tdRisco_3_3.Style.Add("width", larguracelula.ToString() + "px");

        tdRisco_1_1.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_1_2.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_1_3.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_2_1.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_2_2.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_2_3.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_3_1.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_3_2.Style.Add("height", alturacelula.ToString() + "px");
        tdRisco_3_3.Style.Add("height", alturacelula.ToString() + "px");

        tdQuestao_1_1.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_1_2.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_1_3.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_2_1.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_2_2.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_2_3.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_3_1.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_3_2.Style.Add("width", larguracelula.ToString() + "px");
        tdQuestao_3_3.Style.Add("width", larguracelula.ToString() + "px");

        tdQuestao_1_1.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_1_2.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_1_3.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_2_1.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_2_2.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_2_3.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_3_1.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_3_2.Style.Add("height", alturacelula.ToString() + "px");
        tdQuestao_3_3.Style.Add("height", alturacelula.ToString() + "px");
    }
}
