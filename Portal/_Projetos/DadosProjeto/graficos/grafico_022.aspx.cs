using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_DadosProjeto_graficos_grafico_022 : System.Web.UI.Page
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
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        montaCampos();
        getLabelsBancoDados();
        lblCronograma.NavigateUrl = "../Cronograma_gantt.aspx?IDProjeto=" + codigoProjeto;
        DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblCronograma.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?IDProjeto=" + codigoProjeto;
        }
        lblFinanceiro.NavigateUrl = "../FinanceiroCurvaS.aspx?idProjeto=" + codigoProjeto;
        lblRisco.NavigateUrl = "../riscos.aspx?Status=Ativo&TT=R&idProjeto=" + codigoProjeto;
        lblQuestao.NavigateUrl = "../riscos.aspx?Status=Aberta&TT=Q&idProjeto=" + codigoProjeto;
    }

    private void montaCampos()
    {
        string comandoSQL = string.Format(@"
SELECT p.CodigoProjeto,
       p.NomeProjeto,
       rp.CorGeral,
       gp.NomeUsuario,
       p.DataUltimaAlteracao,
       s.DescricaoStatus,
       rp.PercentualRealizacao,
	   {0}.{1}.f_GetCorFisico(p.CodigoProjeto) AS Fisico,
	   {0}.{1}.f_GetCorFinanceiro(p.CodigoProjeto) AS Financeiro,
	   {0}.{1}.f_GetCorRisco(p.CodigoProjeto) AS Risco,
	   {0}.{1}.f_GetCorQuestao(p.CodigoProjeto) AS Questao
  FROM {0}.{1}.Projeto p INNER JOIN
	   {0}.{1}.ResumoProjeto rp ON rp.CodigoProjeto = p.CodigoProjeto LEFT JOIN 
	   {0}.{1}.Usuario gp ON gp.CodigoUsuario = p.CodigoGerenteProjeto INNER JOIN
	   {0}.{1}.Status s ON s.CodigoStatus = p.CodigoStatusProjeto
 WHERE p.CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet dsProjeto = cDados.getDataSet(comandoSQL);

        if (cDados.DataTableOk(dsProjeto.Tables[0]))
        {
            DataRow dr = dsProjeto.Tables[0].Rows[0];

            imgStatusProjeto.ImageUrl = string.Format("~/imagens/bullets/{0}.png", dr["CorGeral"].ToString().Trim());
            imgStatusProjeto.ToolTip = getLabelCorProjeto(dr["CorGeral"].ToString().Trim());
            lblCorProjeto.Text = string.Format("{0:p0}", dr["PercentualRealizacao"]);
            lblGerente.Text = dr["NomeUsuario"].ToString();
            lblStatus.Text = dr["DescricaoStatus"].ToString();
            lblAtualizacao.Text = string.Format("{0:dd/MM/yyyy}", dr["DataUltimaAlteracao"]);

            if (lblCorProjeto.Text.Trim() != "")
                lblCorProjeto.Text += " Realizado";

            imgCronograma.ImageUrl = string.Format("~/imagens/Fisico{0}.png", dr["Fisico"].ToString().Trim());
            imgFinanceiro.ImageUrl = string.Format("~/imagens/Financeiro{0}.png", dr["Financeiro"].ToString().Trim());
            imgRisco.ImageUrl = string.Format("~/imagens/Risco{0}.png", dr["Risco"].ToString().Trim());
            imgQuestao.ImageUrl = string.Format("~/imagens/Questao{0}.png", dr["Questao"].ToString().Trim());
        }

        #region label imagens
        
        DataSet dsRiscosQuestoes = cDados.getNumerosProjetoExecutivo(codigoProjeto, "");

        if (cDados.DataSetOk(dsRiscosQuestoes) && cDados.DataTableOk(dsRiscosQuestoes.Tables[0]))
        {
            float riscos = 0, questoes = 0, financeiro = 0, fisico = 0;

            riscos = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["Riscos"].ToString());
            questoes = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["Questoes"].ToString());
            fisico = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["DesvioFisico"].ToString());
            financeiro = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["DesvioFinanceiro"].ToString());

            string labelDesvioPrazo = "No prazo";

            if (fisico < 0)
                labelDesvioPrazo = string.Format("Atrasado em {0:p1}", (fisico * -1));
            else if (fisico > 0)
                labelDesvioPrazo = string.Format("Adiantado em {0:p1}", fisico);


            string labelDesvioCusto = string.Format("Desvio {0:p2}", financeiro);

            string definicaoQuestao = "questão", definicaoQuestaoPlural = "questões", definicaoQuestaoSingular = "questão";
            string labelNenhuma = "Nenhuma";
            string genero = "F";
            DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
            {
                definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
            }

            definicaoQuestaoPlural = definicaoQuestaoPlural.ToLower() + string.Format(@" ativ{0}s", genero == "M" ? "o" : "a");
            definicaoQuestaoSingular = definicaoQuestaoSingular.ToLower() + string.Format(@" ativ{0}", genero == "M" ? "o" : "a");
            labelNenhuma = genero == "M" ? "Nenhum" : "Nenhuma";


            string labelRiscos = "Nenhum risco ativo";
            string labelQuestoes = labelNenhuma + " " + definicaoQuestaoSingular;

            if (riscos == 1)
            {
                labelRiscos = "1 risco ativo";
            }
            else if (riscos > 1)
            {
                labelRiscos = riscos + " riscos ativos";
            }

            if (questoes == 1)
            {
                labelQuestoes = "1 " + definicaoQuestaoSingular;
            }
            else if (questoes > 1)
            {
                labelQuestoes = questoes + " " + definicaoQuestaoPlural;
            }

            lblCronograma.Text = labelDesvioPrazo;
            lblFinanceiro.Text = labelDesvioCusto;
            lblRisco.Text = labelRiscos;
            lblQuestao.Text = labelQuestoes;
        }

        #endregion
    }

    private string getLabelCorProjeto(string cor)
    {
        switch (cor.ToLower())
        {
            case "verde": return "Satisfatório";
            case "amarelo": return "Em Atenção";
            case "vermelho": return "Crítico";
            case "laranja": return "Finalizando";
            case "azul": return "Encerrado";
            default: return "Sem Informação";
        }
    }

    private void getLabelsBancoDados()
    {
        DataSet ds = cDados.getParametrosSistema("labelGerente");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string labelGerente = ds.Tables[0].Rows[0]["labelGerente"] + "" != "" ? ds.Tables[0].Rows[0]["labelGerente"] + "" : "Gerente";

            lblTituloGer.Text = labelGerente + ":";
        }
    }
}