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
using DevExpress.Web;


public partial class _VisaoExecutivaProjetos_ListaProjetos_visaoProjetos_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0, largura;

    string nomeProjeto = "", verde = "", amarelo = "", vermelho = "", laranja = "", branco = "";
    string labelDesvioPrazo = "", labelDesvioCusto = "", labelRiscos = "", labelQuestoes = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
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

        if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
            verde = Request.QueryString["Verde"].ToString() == "S" ? ",'Verde'" : "";

        if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
            amarelo = Request.QueryString["Amarelo"].ToString() == "S" ? ",'Amarelo'" : "";

        if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
            vermelho = Request.QueryString["Vermelho"].ToString() == "S" ? ",'Vermelho'" : "";

        if (Request.QueryString["Laranja"] != null && Request.QueryString["Laranja"].ToString() != "")
            laranja = Request.QueryString["Laranja"].ToString() == "S" ? ",'Laranja'" : "";

        if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
            branco = Request.QueryString["Branco"].ToString() == "S" ? ",'Branco'" : "";

        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
            nomeProjeto = Request.QueryString["NomeProjeto"].ToString();

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        carregaItens();

        nb01.Width = largura - 10;
    }

    private void carregaItens()
    {

        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        string where = " AND Desempenho IN('SC'" + verde + amarelo + vermelho + laranja + branco + ")";

        if (nomeProjeto.Trim() != "")
            where += " AND NomeProjeto LIKE '%" + nomeProjeto + "%'";

        DataSet ds = cDados.getProjetosVisaoCorporativaCliente(codigoEntidade, codigoUsuario, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where);

        nb01.Groups.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //encaixar  dr["DataUltimaAlteracao"].ToString()
                bool usuarioIgualResponsavel = dr["CodigoGerenteProjeto"].ToString() != "" && codigoUsuario == int.Parse(dr["CodigoGerenteProjeto"].ToString());
                bool usuarioIgualGerenteUnidade = dr["CodigoGerenteUnidade"].ToString() != "" && codigoUsuario == int.Parse(dr["CodigoGerenteUnidade"].ToString());

                string ultimaAtualizacao = "";
                DataSet dsDados = cDados.getDadosGeraisProjeto(int.Parse(dr["CodigoProjeto"].ToString()), "");
                if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
                {
                    DataTable dt = dsDados.Tables[0];
                    ultimaAtualizacao = dt.Rows[0]["UltimaAtualizacao"] + "" == "" ? "---" : dt.Rows[0]["UltimaAtualizacao"].ToString();

                } 
                string textoGrupo = string.Format(@"<table><tr><td style='width:15px;'><img src='../../imagens/{0}Menor.gif' /></td><td>&nbsp;{1}</td><tr><td></td><td><table><tr><td>{2}</td><td style='width:10px'></td><td>{3}</td><td>{4}</td></tr></table></td></tr></table>",
                dr["Desempenho"].ToString().Trim(), dr["NomeProjeto"].ToString()
               , dr["Concluido"].ToString() == "" ? "" : "<b>Previsto:</b> " + double.Parse(dr["Previsto"].ToString()).ToString("p0")
               , dr["Concluido"].ToString() == "" ? "" : "<b>Concluído:</b> " + double.Parse(dr["Concluido"].ToString()).ToString("p0")
               , ultimaAtualizacao == "" ? "" : "&nbsp&nbsp&nbsp<b>Última Atualização:</b>&nbsp" + ultimaAtualizacao);

                string urlGrafico = "pr_001.aspx?CP=" + dr["CodigoProjeto"];

                string imagemMensagemProjeto = string.Format("<img id='PR_{0}' src='../../imagens/vazioPequeno.GIF' />", dr["CodigoProjeto"].ToString());
                string imagemMensagemUnidade = string.Format("<img id='EN_{0}_{0}' src='../../imagens/vazioPequeno.GIF' />", dr["CodigoUnidade"].ToString(), dr["CodigoProjeto"].ToString());

                if (dr["TipoMensagem"].ToString() != "SM")
                {
                    string nomeImagem = dr["TipoMensagem"].ToString() == "MA" ? "envelopeAtrasado" : "envelopeNormal";
                    string toolTip = dr["TipoMensagem"].ToString() == "MA" ? "Mensagem Aberta e com Resposta Atrasada" : "Mensagem Aberta";

                    string cursor = "";
                    string link = "";

                    if (usuarioIgualResponsavel)
                    {
                        cursor = "cursor: pointer;";
                        link = " onclick='abreMensagensNovas(" + dr["CodigoUltimaMensagem"].ToString() + ", " + dr["CodigoProjeto"].ToString() + ");'";
                    }

                    imagemMensagemProjeto = string.Format("<img id='PR_{2}' style='{3}' {4} alt='{1}' src='../../imagens/{0}.png' />",
                        nomeImagem
                        , toolTip
                        , dr["CodigoProjeto"].ToString()
                        , cursor
                        , link
                        , dr["CodigoUltimaMensagem"].ToString());
                }

                if (dr["TipoMensagemEntidade"].ToString() != "SM")
                {
                    string nomeImagem = dr["TipoMensagemEntidade"].ToString() == "MA" ? "envelopeAtrasado" : "envelopeNormal";
                    string toolTip = dr["TipoMensagemEntidade"].ToString() == "MA" ? "Mensagem Aberta e com Resposta Atrasada" : "Mensagem Aberta";

                    string cursor = "";
                    string link = "";

                    if (usuarioIgualGerenteUnidade)
                    {
                        cursor = "cursor: pointer;";
                        link = " onclick='abreMensagensNovas(" + dr["CodigoUltimaMensagemEntidade"].ToString() + ");'";
                    }

                    imagemMensagemUnidade = string.Format("<img id='EN_{2}_{2}' style='{3}' {4} alt='{1}' src='../../imagens/{0}.png' />",
                        nomeImagem
                        , toolTip
                        , dr["CodigoUnidade"].ToString()
                        , cursor
                        , link
                        , dr["CodigoUnidade"].ToString());
                }



                string nomeGerenteProjeto = getNomeAbreviadoUsuario(dr["GerenteProjeto"].ToString());
                string nomeGerenteUnidade = getNomeAbreviadoUsuario(dr["GerenteUnidade"].ToString());
                string nomeUnidade = dr["NomeUnidadeNegocio"].ToString();

                string linkGerenteProjeto = dr["CodigoGerenteProjeto"].ToString() == "" ? "" : "Gerente do Projeto: " + getLinkUsuario(usuarioIgualResponsavel, dr["CodigoGerenteProjeto"].ToString(), nomeGerenteProjeto, dr["CodigoProjeto"].ToString(), dr["NomeProjeto"].ToString(), "PR", "-1");
                string linkGerenteUnidade = dr["CodigoGerenteUnidade"].ToString() == "" ? "" : "Gerente: " + getLinkUsuario(usuarioIgualGerenteUnidade, dr["CodigoGerenteUnidade"].ToString(), nomeGerenteUnidade, dr["CodigoUnidade"].ToString(), dr["NomeProjeto"].ToString(), "EN", dr["CodigoUnidade"].ToString());

                NavBarGroup nb;

                nb = nb01.Groups.Add(textoGrupo, dr["CodigoProjeto"].ToString() + "_" + count);


                string urlInicial = "";

                nb.Expanded = false;

                montaCampos(int.Parse(dr["CodigoProjeto"].ToString()));

                string imagensStatus = string.Format(@"<table><tr><td style='width:25px' align='center'><img alt='Status Físico' src='../../imagens/Fisico{0}.png' /></td><td>{4}&nbsp;&nbsp;</td><td style='width:25px' align='center'><img alt='Status Financeiro' src='../../imagens/Financeiro{1}.png' /></td><td>{5}&nbsp;&nbsp;</td><td style='width:25px' align='center'><img alt='Status Riscos' src='../../imagens/Risco{2}.png' /></td><td><strong style='FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;' onclick='abreRiscosQuestoes(""RIS"", {8}, ""{9}"")'>{6}</strong>&nbsp;&nbsp;</td><td style='width:25px' align='center'><img alt='Status Questões' src='../../imagens/Questao{2}.png' /></td><td><strong style='FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;' onclick='abreRiscosQuestoes(""QUE"", {8}, ""{9}"")'>{7}</strong>&nbsp;&nbsp;</td></tr></table>"

                    , dr["Fisico"].ToString().Trim()
                    , dr["Financeiro"].ToString().Trim()
                    , dr["Risco"].ToString().Trim()
                    , dr["Questao"].ToString().Trim()
                    , labelDesvioPrazo
                    , labelDesvioCusto
                    , labelRiscos
                    , labelQuestoes
                    , dr["CodigoProjeto"].ToString()
                    , dr["NomeProjeto"].ToString().Replace("'", ""));

                int nivel = 2;

                if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                    nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString()) + 1;

                string linkResumoProjeto = string.Format(@"<strong title='Visualizar Detalhes do Projeto' onclick=""window.top.gotoURL('_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}&NomeProjeto={1}&NivelNavegacao={2}', '_top');"" style=""FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;"">Visualizar Detalhes do Projeto</strong>", dr["CodigoProjeto"].ToString()
                                    , dr["NomeProjeto"].ToString().Replace("'", "")
                                    , nivel); //<a href='../../DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" +  + "&NomeProjeto=" + dr["NomeProjeto"].ToString() + "'>&nbsp;Visualizar Detalhes do Projeto</a>";

                string textoItem = string.Format(@"<table style='width:100%;'><tr><td>{9}</td></tr><tr><td><table><tr><td><table><tr><td>{3}</td><td>{4}</td></tr></table></td><td></td><td style='width:50px'></td><td></td></tr><tr><td><table><tr><td>Unidade: {6}&nbsp;</td><td>{7}</td><td>{8}</td></tr></table></td><td></td><td style='width:50px'></td><td></td></tr></table></td></tr><tr><td><table style='width:100%;'><tr><td>{10}</td></tr><tr><td>&nbsp;</td></tr><tr><td><iframe id=""frm2_{2}"" frameborder=""0"" height=""280px"" scrolling=""no"" link=""{1}"" src=""{5}"" width=""{11}""></iframe></td></tr></table></td></tr></table>", dr["NomeProjeto"].ToString()
                                                            , urlGrafico
                                                            , dr["CodigoProjeto"].ToString() + "_" + count
                                                            , linkGerenteProjeto
                                                            , imagemMensagemProjeto
                                                            , urlInicial
                                                            , nomeUnidade
                                                            , linkGerenteUnidade
                                                            , imagemMensagemUnidade
                                                            , imagensStatus
                                                            , linkResumoProjeto
                                                            , largura - 70);


                count++;

                NavBarItem nbi = nb.Items.Add(textoItem);

                nbi.ClientEnabled = false;
            }
        }

        if (count == 0)
        {
            popUpStatusTela.ShowOnPageLoad = true;
        }
    }

    private string getLinkUsuario(bool usarLink, string codigoUsuario, string nomeUsuario, string codigoProjero, string nomeProjeto, string iniciais, string codigoUnidade)
    {
        if (usarLink)
        {
            return string.Format(@"<strong title='' style=""FONT-WEIGHT: normal; color:#0E008C;"">{0}</strong>", nomeUsuario);
        }
        else
        {
            return string.Format(@"<strong title='Abrir Nova Mensagem para o Gerente' onclick=""abreMensagens({2}, {1}, '{0}', '{4}', {5})"" style=""FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;"">{3}</strong>"
                    , nomeProjeto
                    , codigoUsuario
                    , codigoProjero
                    , nomeUsuario
                    , iniciais
                    , codigoUnidade);
        }
    }

    private string getNomeAbreviadoUsuario(string nomeCompleto)
    {
        if (nomeCompleto.IndexOf(' ') != -1)
        {
            return nomeCompleto.Substring(0, nomeCompleto.IndexOf(' ')) + " " + nomeCompleto.Substring(nomeCompleto.LastIndexOf(' '));
        }
        else
        {
            return nomeCompleto;
        }
    }

    private void montaCampos(int codigoProjeto)
    {
        DataSet dsRiscosQuestoes = cDados.getNumerosProjetoExecutivo(codigoProjeto, "");

        if (cDados.DataSetOk(dsRiscosQuestoes) && cDados.DataTableOk(dsRiscosQuestoes.Tables[0]))
        {
            float riscos = 0, questoes = 0, financeiro = 0, fisico = 0;

            riscos = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["Riscos"].ToString());
            questoes = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["Questoes"].ToString());
            fisico = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["DesvioFisico"].ToString());
            financeiro = float.Parse(dsRiscosQuestoes.Tables[0].Rows[0]["DesvioFinanceiro"].ToString());

            labelDesvioPrazo = "No prazo";

            if (fisico < 0)
                labelDesvioPrazo = string.Format("Atrasado em {0:p1}", (fisico * -1));
            else if (fisico > 0)
                labelDesvioPrazo = string.Format("Adiantado em {0:p1}", fisico);


            labelDesvioCusto = string.Format("Desvio {0:p2}", financeiro);

            string definicaoQuestao = "Questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
            string labelNenhuma = "Nenhuma";
            string genero = "F";
            DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
            {
                definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
            }

            definicaoQuestaoPlural = definicaoQuestaoPlural + string.Format(@" ativ{0}s", genero == "M" ? "o" : "a");
            definicaoQuestaoSingular = definicaoQuestaoSingular + string.Format(@" ativ{0}", genero == "M" ? "o" : "a");
            labelNenhuma = genero == "M" ? "Nenhum" : "Nenhuma";


            labelRiscos = "Nenhum risco ativo";
            labelQuestoes = labelNenhuma + " " + definicaoQuestaoSingular;

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
        }
    }
}
