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

public partial class grafico_010 : System.Web.UI.Page
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

        cDados.defineAlturaFrames(this, 65);
    }
  
    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        
        int larguraPaineis = ((largura) / 2 - 105); 
    }

    private void getRiscosProblemas()
    {
        string labelQuestao = "Questão", labelQuestoesAtivas = " Questões Ativas", labelQuestaoAtiva = " Questão Ativa";
        string labelQuestoesEliminadas = " Questões Eliminadas", labelQuestaoEliminada = " Questão Eliminada";
        string labelQuestoes = "Questões";
        string generoQuestao = "F";
        DataSet dsDados = cDados.getQuantidadeRiscosProblemasProjeto(codigoProjeto, "");
        DataSet dsQuestoes = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if (cDados.DataSetOk(dsQuestoes) && cDados.DataTableOk(dsQuestoes.Tables[0]))
        {
            labelQuestao = dsQuestoes.Tables[0].Rows[0]["labelQuestao"].ToString();
            labelQuestoes = dsQuestoes.Tables[0].Rows[0]["labelQuestoes"].ToString();
            generoQuestao = dsQuestoes.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("en"))
            {
                labelQuestoesAtivas = string.Format(@"Active {0}", labelQuestoes);
                labelQuestaoAtiva = string.Format(@"Active {0}", labelQuestao); //" Questão Ativa";
                labelQuestoesEliminadas = string.Format(@"Solved {0}", labelQuestoes); // Questões Eliminadas";
                labelQuestaoEliminada = string.Format(@"Solved {0}", labelQuestao); // " Questão Eliminada";
            }
            else
            {
                labelQuestoesAtivas = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");
                labelQuestaoAtiva = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); //" Questão Ativa";
                labelQuestoesEliminadas = string.Format(@" {0} Eliminad{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); // Questões Eliminadas";
                labelQuestaoEliminada = string.Format(@" {0} Eliminad{1}", labelQuestao, generoQuestao == "M" ? "o" : "a");// " Questão Eliminada";
            }
        }


        
        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao");

    

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int riscosAtivos, riscosEliminados, problemasAtivos, problemasEliminados;

            riscosAtivos = int.Parse(dt.Rows[0]["RiscosAtivos"].ToString());
            riscosEliminados = int.Parse(dt.Rows[0]["RiscosEliminados"].ToString());
            problemasAtivos = int.Parse(dt.Rows[0]["ProblemasAtivos"].ToString());
            problemasEliminados = int.Parse(dt.Rows[0]["ProblemasEliminados"].ToString());
            
            hlRiscosAtivos.Text = (riscosAtivos > 0) ? Resources.traducao.riscos_ativos +  " (" + riscosAtivos + ")" : Resources.traducao.riscos_ativos + " (0)";

            hlRiscosEliminados.Text = Resources.traducao.riscos_eliminados + " (" + riscosEliminados + ")";

            hlProblemasAtivos.Text = labelQuestoesAtivas + " (" + problemasAtivos + ")";

            hlProblemasEliminados.Text = labelQuestoesEliminadas + " (" + problemasEliminados + ")";
                       
            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1"))
            {
                if (riscosAtivos > 0)
                    hlRiscosAtivos.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&IDProjeto=" + codigoProjeto;
                
                if (riscosEliminados > 0)
                    hlRiscosEliminados.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=R&Status=Eliminado&IDProjeto=" + codigoProjeto;               
            }
            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2"))
            {
                if (problemasAtivos > 0)
                    hlProblemasAtivos.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&IDProjeto=" + codigoProjeto;
                
                if (problemasEliminados > 0)
                    hlProblemasEliminados.NavigateUrl = "../riscos.aspx?Publicado=SIM&TT=Q&Status=Eliminado&IDProjeto=" + codigoProjeto;
            }
        }
        else
        {
            hlRiscosAtivos.Text = Resources.traducao.riscos_ativos + " (0)";
            hlRiscosEliminados.Text = Resources.traducao.riscos_eliminados + " (0)";
            hlProblemasAtivos.Text = labelQuestoesAtivas + " (0)";
            hlProblemasEliminados.Text = labelQuestoesEliminadas + " (0)";
        }
    }
}
