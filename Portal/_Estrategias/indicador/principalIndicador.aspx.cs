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

public partial class _Estrategias_indicador_principalIndicador : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela;
    public string segundoMenu = "";
    public int codigoIndicador = -1;

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

        if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        defineAlturaTela();
        preparaMenu();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaTela = (alturaPrincipal - 140).ToString() + "px";
    }

    private void preparaMenu()
    {
        string SeparadorOpcoes = "¥"; // alt 190

        try
        {
            string menuCompleto = "";

            menuCompleto += string.Format(@"<a onclick=""javascript:document.getElementById('frameIndicador').src = './resumoIndicador.aspx?COIN={0}';"" href=""#"" class=""LinkSecundario"">Resumo&nbsp;</a>" + SeparadorOpcoes, codigoIndicador);

            menuCompleto += string.Format(@"<a onclick=""javascript:document.getElementById('frameIndicador').src = './composicaoIndicador.aspx?COIN={0}'; "" href=""#"" class=""LinkSecundario"">&nbsp;Composição&nbsp;</a>" + SeparadorOpcoes, codigoIndicador);

            menuCompleto += string.Format(@"<a onclick=""javascript:document.getElementById('frameIndicador').src = './benchmarkingIndicador.aspx?COIN={0}'; "" href=""#"" class=""LinkSecundario"">&nbsp;Benchmarking&nbsp;</a>" + SeparadorOpcoes, codigoIndicador);

            menuCompleto += string.Format(@"<a onclick=""javascript:document.getElementById('frameIndicador').src = './AcoesCorretivas.aspx?COIN={0}'; "" href=""#"" class=""LinkSecundario"">&nbsp;Ações Corretivas&nbsp;</a>" + SeparadorOpcoes, codigoIndicador);

            menuCompleto += string.Format(@"<a onclick=""javascript:document.getElementById('frameIndicador').src = './Issues.aspx?COIN={0}'; "" href=""#"" class=""LinkSecundario"">&nbsp;Issues&nbsp;&nbsp;</a>" + SeparadorOpcoes, codigoIndicador);
            
            if (menuCompleto != "")
                menuCompleto = menuCompleto.Substring(0, menuCompleto.Length - 1);
            
            menuCompleto = menuCompleto.Replace(SeparadorOpcoes, @"<font class =""fonteGeral"" style=""color:#5d7b9d""> | </font>");

            segundoMenu = menuCompleto;
        }
        catch (Exception ex)
        {
            throw new Exception("Controle de acesso a projetos: " + ex.Message);
        }
    }
}
