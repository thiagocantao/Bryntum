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
using System.Drawing;

public partial class grafico_011 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;

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

        defineTamanhoObjetos();

        getRiscosProblemas();

        cDados.defineAlturaFrames(this, 55);
    }
  
    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        
        int larguraPaineis = ((largura) / 2 - 105);
    }

    private void getRiscosProblemas()
    {
        string labelQuestao = Resources.traducao.grafico_011_quest_o, labelQuestoesAtivas = " " + Resources.traducao.grafico_011_quest_es_ativas, labelQuestaoAtiva = " " + Resources.traducao.grafico_011_quest_o_ativa;
        string labelQuestoesEliminadas = " " + Resources.traducao.grafico_011_quest_es_eliminadas, labelQuestaoEliminada = " " + Resources.traducao.grafico_011_quest_o_eliminada;
        string labelNenhuma = Resources.traducao.grafico_011_nenhuma;
        string labelQuestoes = Resources.traducao.grafico_011_quest_es;
        string generoQuestao = "F";
        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
        {
            labelQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
            labelQuestoes = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
            generoQuestao = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";

            labelQuestoesAtivas = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");
            labelQuestaoAtiva = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); //" Questão Ativa";
            labelQuestoesEliminadas = string.Format(@" {0} Eliminad{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); // Questões Eliminadas";
            labelQuestaoEliminada = string.Format(@" {0} Eliminad{1}", labelQuestao, generoQuestao == "M" ? "o" : "a");// " Questão Eliminada";
            labelNenhuma = generoQuestao == "M" ? Resources.traducao.grafico_011_nenhum : "Nenhuma";
        }
        

        DataSet dsDados = cDados.getQuantidadeRiscosProblemasProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int riscosAtivos, problemasAtivos;

            riscosAtivos = int.Parse(dt.Rows[0]["RiscosAtivos"].ToString());
            problemasAtivos = int.Parse(dt.Rows[0]["ProblemasAtivos"].ToString());

            hlRiscosAtivos.Text = (riscosAtivos > 0) ? Resources.traducao.grafico_011_riscos_ativos__ + riscosAtivos + ")" : Resources.traducao.grafico_011_riscos_ativos__0_;

            hlProblemasAtivos.Text = (problemasAtivos > 0) ? labelQuestoesAtivas + " (" + problemasAtivos + ")" : labelQuestoesAtivas + " (0)";


            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1"))
            {
                if (riscosAtivos > 0)
                    hlRiscosAtivos.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&IDProjeto=" + codigoProjeto;
            }
            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2"))
            {
                if (problemasAtivos > 0)
                    hlProblemasAtivos.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&IDProjeto=" + codigoProjeto;
            }

        }
        else
        {
            hlRiscosAtivos.Text = "Riscos Ativos (0)";
            hlProblemasAtivos.Text = labelQuestoesAtivas + " (0)";
        }
    }
}
