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

public partial class _VisaoMaster_Graficos_vm_015 : System.Web.UI.Page
{
    dados cDados;

    public string tituloPainel1 = "", tituloPainel2 = "", tituloPainel3 = "";

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();
        carregaPaineis();

        string mostraArvore = "N";

        DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "MostraEscopoUHEArvore");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            mostraArvore = dsParam.Tables[0].Rows[0]["MostraEscopoUHEArvore"].ToString();
        }

        pn3.JSProperties["cp_MostraEscopoUHEArvore"] = mostraArvore;
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            pn1.Width = (largura / 3 - 5);
            pn1.ContentHeight = altura - 25;

            pn2.Width = (largura / 3 - 5);
            pn2.ContentHeight = altura - 25;

            pn3.Width = (largura / 3 - 5);
            pn3.ContentHeight = altura - 25;
        }
    }

    private void carregaPaineis()
    {
        string tablePainel1 = "", tablePainel2 = "", tablePainel3 = "";
        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();
        string nomeArea = Request.QueryString["NA"] == null ? "" : Request.QueryString["NA"].ToString();

        int unidadeGeradora = 1;

        #region tabela 1



        string codigoReservado = "St_Pimental";
        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        string funcao = "if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "');";

        tablePainel1 = getTabelaMarcos("Pimental", ref tituloPainel1, unidadeGeradora, funcao);

        string infoTabela1 = string.Format(@"<table cellpadding='0' cellspacing='0' style='height:100%' class='style1'>
                                                <tr><td>{0}</td></tr>
                                                <tr><td onclick=""{1}"" title='Clique aqui para visualizar a métrica e última atualização' style=""cursor:pointer;font-family: Verdana; font-size: 7pt; font-style: italic"" align=""left"">Fonte: Superintendência de Planejamento</td></tr>
                                             </table>", tablePainel1, funcao);

        Literal controle1 = cDados.getLiteral(infoTabela1);

        pn1.Controls.Add(controle1);

        #endregion

        #region tabela 2

        codigoReservado = "St_BeloMonte";
        funcao = "if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "');";
        tablePainel2 = getTabelaMarcos("Belo Monte", ref tituloPainel2, unidadeGeradora, funcao);

        

        string infoTabela2 = string.Format(@"<table cellpadding='0' cellspacing='0' style='height:100%' class='style1'>
                                                <tr><td>{0}</td></tr>
                                                <tr><td onclick=""{1}"" title='Clique aqui para visualizar a métrica e última atualização' style=""cursor:pointer;font-family: Verdana; font-size: 7pt; font-style: italic"" align=""left"">Fonte: Superintendência de Planejamento</td></tr>
                                             </table>", tablePainel2, funcao);

        Literal controle2 = cDados.getLiteral(infoTabela2);

        pn2.Controls.Add(controle2);

        #endregion

        #region tabela 3

        codigoReservado = "EAP_DS";
        funcao = "if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('EAP_DS;" + codigoReservado + "');";
        tablePainel3 = getTabelaMarcosSocioambiental("Obtenção da LO - Licença de Operação", ref tituloPainel3, unidadeGeradora, funcao);



        string infoTabela3 = string.Format(@"<table cellpadding='0' cellspacing='0' style='height:100%' class='style1'>
                                                <tr><td>{0}</td></tr>
                                                <tr><td onclick=""{1}"" title='Clique aqui para visualizar a métrica e última atualização' style=""cursor:pointer;font-family: Verdana; font-size: 7pt; font-style: italic"" align=""left"">Fonte: PMO Socioambiental</td></tr>
                                             </table>", tablePainel3, funcao);

        Literal controle3 = cDados.getLiteral(infoTabela3);

        pn3.Controls.Add(controle3);

        #endregion
    }

    private string getTabelaMarcos(string nomeSitio, ref string titulo, int unidadeGeradora, string funcaoClick)
    {
        string tabelaRetorno = "";

        string toolTip = string.Format(@"Objetivo/descrição - Identificar a quantidade e a criticidade dos Marcos Físicos para a geração da 1º turbina de {0}. Ao clicar no semáforo é possível visualizar o detalhamento dos Marcos Físicos."
            , nomeSitio);

        DataSet ds = cDados.getTabelasPainelPresidencia(nomeSitio, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), unidadeGeradora, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow linha1 = ds.Tables[0].Rows[0];

            titulo = string.Format(@"<table title='{3}' cellpadding='0' cellspacing='0' class='style1'><tr><td style='width:18px' align='left'><img style=""cursor:pointer"" alt='Desempenho' src='../../imagens/{0}Menor.gif' onclick=""{4}"" /></td><td align='left'><a href='#' style='color: #5d7b9d; text-decoration:underline' onclick='abreTabela(""{1}"", {2}, """", """")'>{1} - {2}ª Unidade</a></td></tr></table>"
                , linha1["CorStatusUnidade"]
                , nomeSitio
                , unidadeGeradora
                , toolTip, funcaoClick);

            tabelaRetorno = string.Format(@"<table cellpadding='0' cellspacing='0' class='style1'>
                                        <tr style='height:20px; background-color:#7DABDD'>
                                            <td align='center' class='style5' style='width:50%;'>Marcos Físicos Concluídos</td><td align='center' class='style3'>Marcos Físicos a Concluir</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Verde', 'Sim')""
                                                style=""background-image: url('../../imagens/VerdeGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {0}</td>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Verde', 'Não')""
                                                style=""background-image: url('../../imagens/VerdeGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {1}</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Amarelo', 'Sim')""
                                                style=""background-image: url('../../imagens/AmareloGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {2}</td>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Amarelo', 'Não')""
                                                style=""background-image: url('../../imagens/AmareloGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {3}</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style4' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Vermelho', 'Sim')""
                                                style=""background-image: url('../../imagens/VermelhoGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {4}</td>
                                            <td align='center' class='style4' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabela('{6}', {7}, 'Vermelho', 'Não')""
                                                style=""background-image: url('../../imagens/VermelhoGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {5}</td>
                                        </tr>
                                        </table>
                                            ", linha1["ConcluidosVerde"]
                                                 , linha1["AConcluirVerde"]
                                                 , linha1["ConcluidosAmarelo"]
                                                 , linha1["AConcluirAmarelo"]
                                                 , linha1["ConcluidosVermelho"]
                                                 , linha1["AConcluirVermelho"]
                                                 , nomeSitio
                                                 , unidadeGeradora);
            
        }

        return tabelaRetorno;
    }

    private string getTabelaMarcosSocioambiental(string nomeSitio, ref string titulo, int unidadeGeradora, string funcaoClick)
    {
        string tabelaRetorno = "";

        string toolTip = string.Format(@"Objetivo/descrição - Identificar a quantidade e a criticidade dos Marcos Físicos para a geração da 1º turbina de {0}. Ao clicar no semáforo é possível visualizar o detalhamento dos Marcos Físicos."
            , nomeSitio);

        DataSet ds = cDados.getTabelasPainelPresidenciaDS(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow linha1 = ds.Tables[0].Rows[0];

            titulo = string.Format(@"<table title='{3}' cellpadding='0' cellspacing='0' class='style1'><tr><td style='width:18px' align='left'><img style=""cursor:pointer"" alt='Desempenho' src='../../imagens/{0}Menor.gif' onclick=""{4}"" /></td><td align='left'><a href='#' style='color: #5d7b9d; text-decoration:underline' onclick='abreTabelaDS(""{1}"", {2}, """", """")'>{1}</a></td></tr></table>"
                , linha1["CorStatusUnidade"]
                , nomeSitio
                , unidadeGeradora
                , toolTip, funcaoClick);

            tabelaRetorno = string.Format(@"<table cellpadding='0' cellspacing='0' class='style1'>
                                        <tr style='height:20px; background-color:#7DABDD'>
                                            <td align='center' class='style5' style='width:50%;'>Marcos Físicos Concluídos</td><td align='center' class='style3'>Marcos Físicos a Concluir</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Verde', 'Sim')""
                                                style=""background-image: url('../../imagens/VerdeGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {0}</td>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Verde', 'Não')""
                                                style=""background-image: url('../../imagens/VerdeGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {1}</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Amarelo', 'Sim')""
                                                style=""background-image: url('../../imagens/AmareloGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {2}</td>
                                            <td align='center' class='style3' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Amarelo', 'Não')""
                                                style=""background-image: url('../../imagens/AmareloGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {3}</td>
                                        </tr>
                                        <tr>
                                            <td align='center' class='style4' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Vermelho', 'Sim')""
                                                style=""background-image: url('../../imagens/VermelhoGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {4}</td>
                                            <td align='center' class='style4' valign='center' onmouseover=""this.style.backgroundColor='#CCE6FF'"" onmouseout=""this.style.backgroundColor='white'"" onclick=""abreTabelaDS('{6}', {7}, 'Vermelho', 'Não')""
                                                style=""background-image: url('../../imagens/VermelhoGrande.png'); background-repeat: no-repeat; background-position: center center; padding-bottom:1px; cursor:pointer"">
                                                {5}</td>
                                        </tr>
                                        </table>
                                            ", linha1["ConcluidosVerde"]
                                                 , linha1["AConcluirVerde"]
                                                 , linha1["ConcluidosAmarelo"]
                                                 , linha1["AConcluirAmarelo"]
                                                 , linha1["ConcluidosVermelho"]
                                                 , linha1["AConcluirVermelho"]
                                                 , nomeSitio
                                                 , unidadeGeradora);

        }

        return tabelaRetorno;
    }
}