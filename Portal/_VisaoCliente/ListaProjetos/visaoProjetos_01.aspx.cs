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

public partial class _VisaoCliente_ListaProjetos_visaoProjetos_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0;

    string nomeProjeto = "", verde = "", amarelo = "", vermelho = "", branco = "";

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
                
        if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
            verde = Request.QueryString["Verde"].ToString() == "S" ? ",'Verde'" : "";

        if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
            amarelo = Request.QueryString["Amarelo"].ToString() == "S" ? ",'Amarelo'" : "";

        if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
            vermelho = Request.QueryString["Vermelho"].ToString() == "S" ? ",'Vermelho'" : "";

        if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
            branco = Request.QueryString["Branco"].ToString() == "S" ? ",'Branco'" : "";

        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
            nomeProjeto = Request.QueryString["NomeProjeto"].ToString();

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        carregaItens();
    }

    private void carregaItens()
    {
        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        string where = " AND Desempenho IN('SC'" + verde + amarelo + vermelho + branco + ")";

        if (nomeProjeto.Trim() != "")
            where += " AND NomeProjeto LIKE '%" + nomeProjeto + "%'";

        DataSet ds = cDados.getProjetosVisaoCorporativaCliente(codigoEntidade, codigoUsuario, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where);

        nb01.Groups.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bool usuarioIgualResponsavel = dr["CodigoGerenteProjeto"].ToString() != "" && codigoUsuario == int.Parse(dr["CodigoGerenteProjeto"].ToString());
                bool usuarioIgualGerenteUnidade = dr["CodigoGerenteUnidade"].ToString() != "" && codigoUsuario == int.Parse(dr["CodigoGerenteUnidade"].ToString());

                string textoGrupo = string.Format(@"<table><tr><td style='width:15px;'><img src='../../imagens/{0}Menor.gif' /></td><td>&nbsp;{1}</td><tr><td></td><td><table><tr><td>{2}</td><td style='width:10px'></td><td>{3}</td></tr></table></td></tr></table>",
                    dr["Desempenho"].ToString().Trim(), dr["NomeProjeto"].ToString()
                    , dr["Concluido"].ToString() == "" ? "" : "<b>Previsto:</b> " + double.Parse(dr["Previsto"].ToString()).ToString("p0")
                    , dr["Concluido"].ToString() == "" ? "" : "<b>Concluído:</b> " + double.Parse(dr["Concluido"].ToString()).ToString("p0"));

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
                        link = " onclick='abreMensagensNovas(" + dr["CodigoUltimaMensagem"].ToString() + ");'";
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

                string textoItem = string.Format(@"<table style='width:100%;'><tr><td><table><tr><td><table><tr><td>{3}</td><td>{4}</td></tr></table></td><td></td><td style='width:50px'></td><td></td></tr><tr><td><table><tr><td>Unidade: {6}&nbsp;</td><td>{7}</td><td>{8}</td></tr></table></td><td></td><td style='width:50px'></td><td></td></tr></table></td></tr><tr><td><table style='width:100%;'><tr><td><iframe id=""frm2_{2}"" frameborder=""0"" height=""300px"" scrolling=""no"" link=""{1}"" src=""{5}"" width=""100%""></iframe></td></tr></table></td></tr></table>", dr["NomeProjeto"].ToString()
                                                            , urlGrafico
                                                            , dr["CodigoProjeto"].ToString() + "_" + count
                                                            , linkGerenteProjeto
                                                            , imagemMensagemProjeto
                                                            , urlInicial
                                                            , nomeUnidade
                                                            , linkGerenteUnidade
                                                            , imagemMensagemUnidade);
                                

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
}
