/*
    14/01/2011 : by Alejandro : alteração a tela inicial, segundo o acceso que tenha. componente que define
                                a variavel publica: 'public string telaInicial'.

 */
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

public partial class _Projetos_Administracao_index : System.Web.UI.Page
{
    dados cDados;
    public string telaInicial = "programasDoProjetos.aspx?Tit=Pogramas";
    public string alturaTabela;
    private string idUsuarioLogado;
    private string idEntidadeLogada;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idEntidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        alturaTabela = getAturaTela() + "px";

        if (!cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSPMTPRJ"))
            if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSCTT"))
                telaInicial = "Contratos.aspx?Tit=Contratos";
    }

    private string getAturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 125).ToString();
    }
}
