using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class erros_erro : System.Web.UI.Page
{
    dados cDados;

    string mensagemOriginal;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);

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

        var exception = ObtemUlticaExcecaoOcorrida();
        var dadosSessao = ObtemDadosSessaoSerializados();
        var identificadorOcorrencia = cDados.InsereRegistroOcorrencia(exception, dadosSessao);

        linkVoltar.Text = Resources.traducao.erro_voltar;
        linkVoltar.ToolTip = Resources.traducao.erro_clique_para_voltar;
        lblComunicacaoErro.Text = string.Format(Resources.traducao.erro_ComunicacaoErro, identificadorOcorrencia);
    }

    private Exception ObtemUlticaExcecaoOcorrida()
    {
        var lastError = Server.GetLastError();
        if (lastError == null)
        {
            var callbackErrorMessage = ASPxWebControl.GetCallbackErrorMessage();
            lastError =
                HttpContext.Current.Application["exception"] as Exception ??
                new Exception(callbackErrorMessage);
        }
        var exception = lastError.InnerException ?? lastError;
        return exception;
    }

    private string ObtemDadosSessaoSerializados()
    {
        OrderedDictionary dadosSessao;
        try
        {
            dadosSessao = Session["infoSistema"] as OrderedDictionary;
        }
        catch
        {
            dadosSessao = new OrderedDictionary();
        }

        if (dadosSessao == null) return string.Empty;

        var dicionary = new Dictionary<string, object>();
        var validKeys = dadosSessao.Keys.OfType<string>()
            .Where(k => k.IndexOf("menu", StringComparison.CurrentCultureIgnoreCase) == decimal.MinusOne);

        foreach (var key in validKeys)
            dicionary.Add(key, dadosSessao[key]);

        dicionary.Add("UserAgent", Request.UserAgent);
        dicionary.Add("AbsoluteUri", Request.Url.AbsoluteUri);
        dicionary.Add("Platform", Request.Browser.Platform);
        dicionary.Add("Browser", string.Format("{0} {1}", Request.Browser.Browser, Request.Browser.Version));

        return new JavaScriptSerializer().Serialize(dicionary);
    }
}
