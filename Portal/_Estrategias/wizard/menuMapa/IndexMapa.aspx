<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="IndexMapa.aspx.cs" Inherits="_Estrategias_wizard_menuMapa_IndexMapa" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
        function atualizaMenu() {
            document.getElementById('mapa_menu').src = document.getElementById('mapa_menu').src;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela" Font-Bold="True"
                    Text="Riscos / Problemas">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table border="0" id="frameMaster" cellpadding="0" cellspacing="0" width="100%" style="width: 100%;">
        <tr>
            <td style="width: 192px" valign="top">
                <iframe id="mapa_menu" src="OpcoesMapa.aspx?Titulo=<%=nomeTela %>" width="100%" height="<%=alturaTabela %>"
                    scrolling="no" frameborder="0" name="mapa_menu"></iframe>
            </td>
            <td valign="top" style="border-left: #e4e3e4 1px solid;">
                <iframe id="mapa_desktop" src="<%=telaInicial %>" width="100%" height="<%=alturaTabela %>" scrolling="auto" frameborder="0" name="mapa_desktop"></iframe>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="box-sizing: border-box; border-collapse: collapse; margin-top: 10px !important; color: rgb(33, 37, 41); font-family: -apple-system, BlinkMacSystemFont, &quot; segoe ui&quot; , roboto, &quot; helvetica neue&quot; , arial, sans-serif, &quot; apple color emoji&quot; , &quot; segoe ui emoji&quot; , &quot; segoe ui symbol&quot; , &quot; noto color emoji&quot; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; width: 1386px;">
        <tbody style="box-sizing: border-box;">
            <tr style="box-sizing: border-box; height: 26px;">
                <td style="box-sizing: border-box; padding-left: 10px;" valign="middle"></td>
            </tr>
        </tbody>
    </table>
    <br class="Apple-interchange-newline">
   <script type="text/javascript">
        window.onresize = function (event) {
            //alert($(window).height());
            //alert($("body").innerHeight());
            //alert(window.innerHeight);
            //this.alert('soma');

            //alert($("#menuPrincipal").height() + ' | ' + $("#rastroPrincipal").height() + ' | ' + $("#lblTituloTela").height() );

            //this.alert('subtração');
            //alert($("body").innerHeight());

            alert('oi');
            this.alert('oi');

            //soma o tamanho da tela e subtrai pelos itens do menu, totalizando o tamanho da tela.
            var TamanhoFrameTela = 'height:' + ( $("body").innerHeight() - ($("#menuPrincipalUsuario").height() + $("#rastroPrincipal").height() + $("#lblTituloTela").height())) + 'px';

            alert(teste);

            $('#frameMaster').removeAttr("style");  
            $('#frameMaster').attr({ 'style': TamanhoFrameTela });

            //$('#mapa_desktop').removeAttr("style");
            //$('#mapa_desktop').attr({
            //                          'style': teste
            //});

            //$('#mapa_menu').removeAttr("style");
            //$('#mapa_menu').attr({
            //                          'style': teste
            //});

            //$('#ConteudoPrincipalFrame').removeAttr("style");
            //$('#ConteudoPrincipalFrame').attr({
            //                          'style': teste
            //});
        };
</script>
</asp:Content>

