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

public partial class _Portfolios_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0, codigoResponsavel = -1, idUsuarioLogado = 0, codigoUnidade = -1, codigoMapa = -1, codigoIndicador = -1, codigoUnidadeIndicador = -1, largura;

    string azul = "", verde = "", amarelo = "", vermelho = "", branco = "";

    bool mostrarTodosFechados = false;

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

        if (Request.QueryString["Azul"] != null && Request.QueryString["Azul"].ToString() != "")
            azul = Request.QueryString["Azul"].ToString() == "S" ? ",'Azul'" : "";

        if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
            verde = Request.QueryString["Verde"].ToString() == "S" ? ",'Verde'" : "";

        if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
            amarelo = Request.QueryString["Amarelo"].ToString() == "S" ? ",'Amarelo'" : "";

        if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
            vermelho = Request.QueryString["Vermelho"].ToString() == "S" ? ",'Vermelho'" : "";

        if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
            branco = Request.QueryString["Branco"].ToString() == "S" ? ",'Branco'" : "";

        if (Request.QueryString["Fechados"] != null && Request.QueryString["Fechados"].ToString() != "")
            mostrarTodosFechados = Request.QueryString["Fechados"].ToString() == "S";

        if (Request.QueryString["CR"] != null && Request.QueryString["CR"].ToString() != "")
            codigoResponsavel = int.Parse(Request.QueryString["CR"].ToString());

        if (Request.QueryString["CUN"] != null && Request.QueryString["CUN"].ToString() != "")
            codigoUnidade = int.Parse(Request.QueryString["CUN"].ToString());

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        if (Request.QueryString["CI"] != null && Request.QueryString["CI"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        carregaItens();


        nb01.Width = largura - 10;
    }   

    private void carregaItens()
    {
        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, "complementaDescricaoMetaNoPainelDeMetas");
        string complementaDescricaoMetaNoPainelDeMetas = "N";

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            complementaDescricaoMetaNoPainelDeMetas = dsParam.Tables[0].Rows[0]["complementaDescricaoMetaNoPainelDeMetas"].ToString();

        dsParam = cDados.getDefinicaoUnidade(codigoEntidade);
        string definicaoUnidade = "Unidade de Negócio";
        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            definicaoUnidade = dsParam.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        string where = " AND Desempenho IN('SC'" + azul + verde + amarelo + vermelho + branco + ")";

        if (codigoResponsavel != -1)
            where += " AND CodigoUsuarioResponsavel = " + codigoResponsavel;

        if (codigoIndicador != -1)
            where += " AND CodigoIndicador = " + codigoIndicador;

        string pesquisa = Server.UrlDecode(Request.QueryString["Meta"] + "");

        if(pesquisa != "")
            where += " AND Meta LIKE '%" + pesquisa + "%'";

        DataSet ds = cDados.getListaPainelMetas(codigoEntidade, idUsuarioLogado, "", codigoMapa, codigoUnidade, where);

        nb01.Groups.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bool usuarioIgualResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()) == int.Parse(dr["CodigoUsuarioResponsavel"].ToString());

                bool indicaEntidade = dr["CodigoUnidadeNegocio"].ToString() == codigoEntidade.ToString();

                string nomeUnidade = complementaDescricaoMetaNoPainelDeMetas != "S" || indicaEntidade ? "" : (" - " + definicaoUnidade + ": " + dr["NomeUnidadeNegocio"]);

                string textoGrupo = string.Format("<table><tr><td style='width:30px;'><img src='../../imagens/{0}Menor.gif' /></td><td>{1}</td></tr></table>",
                    dr["Desempenho"].ToString(), dr["Meta"].ToString() + nomeUnidade);

                string urlGrafico = "";
                string imagemMensagem = string.Format("<img id='ind_{0}' src='../../imagens/vazioPequeno.GIF' />", dr["CodigoIndicador"].ToString());
                string link = "";
                string cursor = "";

                if (dr["CodigoUltimaMensagem"].ToString() != "")
                {
                    string nomeImagem = "envelopeNormal";
                    string toolTip = "Mensagem Aberta";


                    if (usuarioIgualResponsavel)
                    {
                        cursor = "cursor: pointer;";
                        link = " onclick='abreMensagensNovas(" + dr["CodigoUltimaMensagem"].ToString() + ", " + dr["CodigoIndicador"].ToString() + ");'";
                    }

                    imagemMensagem = string.Format("<img style='{3}' {4} alt='{1}' id='ind_{2}' src='../../imagens/{0}.png' />", 
                        nomeImagem
                        , toolTip
                        , dr["CodigoIndicador"].ToString()
                        , cursor
                        , link
                        , dr["CodigoUltimaMensagem"].ToString());
                }

                string imagemPolaridade = "";
                string hintPolaridade = "";

                if (dr["Polaridade"].ToString() == "NEG")
                {
                    imagemPolaridade = "<img alt='Polaridade Negativa(Quanto Maior, Pior)' src='../../imagens/botoes/PolaridadeNMaior.png'/>";
                    hintPolaridade = "Polaridade Negativa(Quanto Maior, Pior)";
                }
                else
                {
                    imagemPolaridade = "<img alt='Polaridade Positiva(Quanto Maior, Melhor)' src='../../imagens/botoes/PolaridadePMaior.png'/>";
                    hintPolaridade = "Polaridade Positiva(Quanto Maior, Melhor)";
                }

                string nomeResponsavel = dr["NomeResponsavel"].ToString();

                if (nomeResponsavel.IndexOf(' ') != -1)
                {
                    nomeResponsavel = nomeResponsavel.Substring(0, nomeResponsavel.IndexOf(' ')) + " " + nomeResponsavel.Substring(nomeResponsavel.LastIndexOf(' '));
                }

                string linkResponsavel = string.Format(@"<strong title='Abrir Nova Mensagem para o Responsável' onclick=""abreMensagens({2}, {1}, '{0}')"" style=""FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;"">{3}</strong>"
                    , dr["NomeIndicador"].ToString()
                    , dr["CodigoUsuarioResponsavel"].ToString()
                    , dr["CodigoIndicador"].ToString()
                    , nomeResponsavel);
                                
                if (usuarioIgualResponsavel)
                {
                    linkResponsavel = string.Format(@"<strong title='' {1} style=""FONT-WEIGHT: normal; color:#0E008C; {2}"">{0}</strong>", dr["NomeResponsavel"].ToString(), link, cursor);
                }

                string linkIndicador = string.Format(@"<strong title='Abrir detalhes do indicador' onclick=""abreIndicador({0}, {2})"" style=""FONT-WEIGHT: normal; TEXT-DECORATION: underline; cursor: pointer; color:#0E008C;"">{1}</strong>"
                    , dr["CodigoIndicador"].ToString()
                    , dr["NomeIndicador"].ToString()
                    , dr["CodigoUnidadeNegocio"].ToString());

                NavBarGroup nb;

                    nb = nb01.Groups.Add(textoGrupo, dr["CodigoIndicador"].ToString() + "_" + count);
                    nb01.JSProperties["cp_CodigoIndicador"] = dr["CodigoIndicador"].ToString();
                    nb01.JSProperties["cp_CasasDecimais"] = dr["CasasDecimais"].ToString();
                    nb01.JSProperties["cp_CodigoEntidade"] = dr["CodigoUnidadeNegocio"].ToString();
                
                    codigoUnidadeIndicador = int.Parse(dr["CodigoUnidadeNegocio"].ToString());

                string urlInicial = "";

                nb.Expanded = false;

                string textoItem = string.Format(@"<table style='width:{7}px;'><tr><td>Indicador: {0}</td></tr><tr><td><table><tr><td>Responsável: {3}</td><td style='width:10px'></td><td>Polaridade: </td><td title='{8}'>{5}</td></tr></table></td></tr><tr><td><table style='width:100%;'><tr><td><iframe id=""frm2_{2}"" frameborder=""0"" height=""450px"" scrolling=""no"" link=""{1}"" src=""{6}"" width=""{7}px""></iframe></td></tr></table></td></tr></table>", linkIndicador
                                                            , urlGrafico
                                                            , dr["CodigoIndicador"].ToString() + "_" + count
                                                            , linkResponsavel
                                                            , imagemMensagem
                                                            , imagemPolaridade
                                                            , urlInicial
                                                            , largura - 40
                                                            , hintPolaridade);
                                

                count++;

                NavBarItem nbi = nb.Items.Add(textoItem);
                nbi.Name = dr["CodigoIndicador"].ToString() + ";" + dr["CasasDecimais"].ToString() + ";" + codigoUnidadeIndicador;
                nbi.ClientEnabled = false;
            }
        }
        
        if (count == 0)
        {
            popUpStatusTela.ShowOnPageLoad = true;
        }
    }
    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_Analise"] = "";
        callback.JSProperties["cp_recomendacoes"] = "";
        callback.JSProperties["cp_titulo"] = "Análise";
        callback.JSProperties["cp_Meta"] = "";
        callback.JSProperties["cp_Indicador"] = "";

        string ano = e.Parameter.ToString().Split(';')[0];
        string mes = e.Parameter.ToString().Split(';')[1];
        string codigoIndicador = e.Parameter.ToString().Split(';')[2];
        string codigoUnidadeNegocio = e.Parameter.ToString().Split(';')[3];

        string where = " AND Ano = " + ano + " AND mes = " + mes;

        DataSet dsGrafico = cDados.getPeriodicidadeIndicador(int.Parse(codigoUnidadeNegocio), int.Parse(codigoIndicador), where);

        string comandoSQL = string.Format(@"SELECT iu.Meta, i.NomeIndicador
                                              FROM IndicadorUnidade iu INNER JOIN
	                                               Indicador i ON i.CodigoIndicador = iu.CodigoIndicador
                                             WHERE iu.DataExclusao IS NULL
                                               AND iu.CodigoIndicador = {0}
                                               AND iu.CodigoUnidadeNegocio = {1}", codigoIndicador, codigoUnidadeNegocio);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            callback.JSProperties["cp_Meta"] = ds.Tables[0].Rows[0]["Meta"].ToString();
            callback.JSProperties["cp_Indicador"] = ds.Tables[0].Rows[0]["NomeIndicador"].ToString();
        }

        if (cDados.DataSetOk(dsGrafico) && cDados.DataTableOk(dsGrafico.Tables[0]))
        {
            callback.JSProperties["cp_Analise"] = dsGrafico.Tables[0].Rows[0]["Analise"].ToString();
            callback.JSProperties["cp_recomendacoes"] = dsGrafico.Tables[0].Rows[0]["Recomendacoes"].ToString();
            callback.JSProperties["cp_titulo"] = "Análise - " + dsGrafico.Tables[0].Rows[0]["Periodo"].ToString();

            string nomeResponsavel = dsGrafico.Tables[0].Rows[0]["NomeUsuarioUltimaAlteracao"].ToString();

            if (nomeResponsavel.IndexOf(' ') != -1)
            {
                nomeResponsavel = nomeResponsavel.Substring(0, nomeResponsavel.IndexOf(' ')) + " " + nomeResponsavel.Substring(nomeResponsavel.LastIndexOf(' '));
            }
            
            string ultimaAtualizacao = string.Format("Última atualização realizada por {1} em {0}", dsGrafico.Tables[0].Rows[0]["DataUltimaAlteracaoFormatada"].ToString()
                , nomeResponsavel);

            callback.JSProperties["cp_DataAnalise"] = ultimaAtualizacao;
        }
    }
}
