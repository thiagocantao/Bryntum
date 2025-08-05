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

public partial class grafico_008 : System.Web.UI.Page
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

        getPendencias();
        getRiscosProblemas();

        cDados.defineAlturaFrames(this, 55);
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int larguraPaineis = ((largura) / 2 - 105);

        pNumeros.Width = new Unit("100%");
    }

    private void getPendencias()
    {
        string definicaoToDoList = "To Do List";

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
            definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

        DataSet dsDados = cDados.getPendenciasProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int cronograma = 0, toDoList = 0, entregas = 0;

            cronograma = int.Parse(dt.Rows[0]["AtrasosCronograma"].ToString());
            toDoList = int.Parse(dt.Rows[0]["AtrasosToDoList"].ToString());
            entregas = int.Parse(dt.Rows[0]["EntregasAtrasadas"].ToString());

            hlCronograma.Text = "Atrasos no Cronograma (" + cronograma + ")";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (" + toDoList + ")";

            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + " (" + entregas + ")";
            }
            else
            {
                hlEntregas.Text = "Entregas Atrasadas (" + entregas + ")";
            }


            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsCrn"))
            {
                hlCronograma.NavigateUrl = "../Cronograma_gantt.aspx?Atrasadas=S&IDProjeto=" + codigoProjeto;
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlCronograma.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }


            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsTdl"))
                hlToDoList.NavigateUrl = "../TarefasToDoList.aspx?Estagio=Atrasada&IDProjeto=" + codigoProjeto;

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsMsg"))
            {
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlEntregas.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?ApenasMarcos=S&Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }
                


            if (cronograma > 0)
                hlCronograma.ForeColor = Color.Red;
            if (toDoList > 0)
                hlToDoList.ForeColor = Color.Red;
            if (entregas > 0)
                hlEntregas.ForeColor = Color.Red;
        }
        else
        {
            hlCronograma.Text = "Atrasos no Cronograma (0)";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (0)";

            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + " (0)";
            }
            else
            {
                hlEntregas.Text = "Entregas Atrasadas (0)";
            }
        }
    }

    private void getRiscosProblemas()
    {
        string labelQuestao = "Questão", labelQuestoesAtivas = " Questões Ativas", labelQuestaoAtiva = " Questão Ativa";
        string labelQuestoesEliminadas = " Questões Eliminadas", labelQuestaoEliminada = " Questão Eliminada";
        string labelNenhuma = "Nenhuma";
        string labelQuestoes = "Questões";
        string generoQuestao = "F";
        DataSet dsDados = cDados.getQuantidadeRiscosProblemasProjeto(codigoProjeto, "");
        DataSet dsQuestoes = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if (cDados.DataSetOk(dsQuestoes) && cDados.DataTableOk(dsQuestoes.Tables[0]))
        {
            labelQuestao = dsQuestoes.Tables[0].Rows[0]["labelQuestao"].ToString();
            labelQuestoes = dsQuestoes.Tables[0].Rows[0]["labelQuestoes"].ToString();
            generoQuestao = dsQuestoes.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();


            labelQuestoesAtivas = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");
            labelQuestaoAtiva = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); //" Questão Ativa";
            labelQuestoesEliminadas = string.Format(@" {0} Eliminad{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); // Questões Eliminadas";
            labelQuestaoEliminada = string.Format(@" {0} Eliminad{1}", labelQuestao, generoQuestao == "M" ? "o" : "a");// " Questão Eliminada";
            labelNenhuma = generoQuestao == "M" ? "Nenhum" : "Nenhuma";
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

            hlRiscosAtivos.Text = (riscosAtivos > 0) ? "Riscos Ativos (" + riscosAtivos + ")" : "Riscos Ativos (0)";

            hlRiscosEliminados.Text = "Riscos Eliminados (" + riscosEliminados + ")";

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
            hlRiscosAtivos.Text = "Riscos Ativos (0)";
            hlRiscosEliminados.Text = "Riscos Eliminados (0)";
            hlProblemasAtivos.Text = labelQuestoesAtivas + " (0)";
            hlProblemasEliminados.Text = labelQuestoesEliminadas + " (0)";
        }
    }
}
