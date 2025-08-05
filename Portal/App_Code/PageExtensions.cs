using System.Web.UI;

/// <summary>
/// Descrição resumida de PageExtensions
/// </summary>
public static class PageExtensions
{
    public static string T(this System.Web.UI.Page page, string chave)
    {
        var cDados = CdadosUtil.GetCdados(null);
        return cDados.getTextoTraduzido(chave, string.Empty);
    }

    public static string TS(this System.Web.UI.Page page, params string[] grupos)
    {
        var cDados = CdadosUtil.GetCdados(null);
        return cDados.getJsonExpressoesTraduzidas(grupos);
    }

    public static void TH(this System.Web.UI.Page page, string json)
    {
        page.Header.Controls.AddAt(0, new LiteralControl(string.Format(@"<script>

        if (idioma == null)
        {{
            var idioma = '{1}';
        }}

        if (traducao == null)
        {{
	        var traducao = {{}};
        }}

        function agrupaTraducao(jsonString)
        {{
	        var json = JSON.parse(jsonString);
	        for (var key in json)
	        {{
 		        traducao[key] = json[key].toString();
 	        }}
        }}

        agrupaTraducao('{0}');
        </script>", json, System.Globalization.CultureInfo.CurrentCulture.Name)));
    }

    public static string T(this System.Web.UI.MasterPage page, string chave)
    {
        var cDados = CdadosUtil.GetCdados(null);
        return cDados.getTextoTraduzido(chave, string.Empty);
    }

    public static string TS(this System.Web.UI.MasterPage page, params string[] grupos)
    {
        var cDados = CdadosUtil.GetCdados(null);
        return cDados.getJsonExpressoesTraduzidas(grupos);
    }

    public static void TH(this System.Web.UI.MasterPage page, string json)
    {
        Control controle;

        controle = page.GetType().Name == "bootstrap_bootstrap_master" ?
            page.FindControl("cabecalhoMaster") :
            page.FindControl("HeaderContent");


        controle.Controls.AddAt(0, new LiteralControl(string.Format(@"<script>

        if (idioma == null)
        {{
            var idioma = '{1}';
        }}

        if (traducao == null)
        {{
	        var traducao = {{}};
        }}

        function agrupaTraducao(jsonString)
        {{
	        var json = JSON.parse(jsonString);
	        for (var key in json)
	        {{
 		        traducao[key] = json[key].toString();
 	        }}
        }}

        agrupaTraducao('{0}');
        </script>", json, System.Globalization.CultureInfo.CurrentCulture.Name)));
    }
}