<%@ Page Language="C#" AutoEventWireup="true" CodeFile="desenhaMapaEstrategico.aspx.cs" Inherits="_Estrategias_wizard_desenhaMapaEstrategico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Desenho do Mapa Estratégico</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../estilos/bordas.css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>

    <script type="text/javascript" src="../../scripts/drag.js"></script>

    <script type="text/javascript">
        window.name="modal";
        var selObj = null;
        var novoObjetivoTopo = 0;     // é utiliza ao criar um novo objetivo
        var novoObjetivoEsquerda = 0; // é utiliza ao criar um novo objetivo
        
        document.onmousedown=dragHandler;
    </script>

</head>
<body class="body">
    <form id="form1" runat="server" target="modal">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 998px; border-bottom: silver 1px solid;
            background-color: #f8f8ff;">
            <tr height="26px">
                <td valign="middle" style="height: 26px; padding-left: 10px;">
                    <asp:Label ID="lblTituloTela" runat="server" 
                        Font-Bold="False"
                        Font-Overline="False" Font-Strikeout="False" Text=" Mapa"></asp:Label>
                </td>
                <td align="right" valign="middle">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                    Width="85px" UseSubmitBehavior="False" OnClick="btnSalvar_Click" font-italic="False" >
<ClientSideEvents Click="function(s, e) {
	if ( salvarMapa() )
	{
	    //hfGeral.Set('StatusSalvar', '0');
    	//hfGeral.PerformCallback('Salvar');
        e.processOnServer = true;
    }
}"></ClientSideEvents>
</dxe:ASPxButton>
                            </td>
                            <td>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnFechar" runat="server" Text="Fechar" AutoPostBack="False" ClientInstanceName="btnFechar"
                                    Width="85px" UseSubmitBehavior="False" font-italic="False" >
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    close();
}"></ClientSideEvents>
</dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1002px;">
            <tr>
                <td style="width: 802px">
                    <asp:Panel ID="dvDesktop" runat="server" Style="position: relative; overflow: auto; background-color: #e6e6fa;
                        border: gray 1px dashed; margin: 1px;" Width="800px">
                    </asp:Panel>
                </td>
                <td valign="top" align="left" style="width: 200px">
                    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px; width: 200px">
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="middle" style="border-bottom: gray 1px solid; text-align: center; height: 20px; background-color: gainsboro;" align="center">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Overline="False"
                                    Font-Strikeout="False" Text=" Caixa de Ferramentas"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td style="width: 30px; height: 25px">
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnNovaPerspectiva" runat="server" ClientInstanceName="btnNovaPerspectiva" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Perspectiva" Visible="False">
                                                <Image Url="~/imagens/wizardMapaEstrategico/Perspectiva.png" />
                                                <Paddings Padding="0px" />
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('P');
}" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnNovoObjetivo" runat="server" ClientInstanceName="btnNovoObjetivo" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ImageSpacing="0px" ToolTip="Objetivo">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('O');
}" />
                                                <Image AlternateText="Objetivo" Url="~/imagens/wizardMapaEstrategico/Objetivo.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnNovoTema" runat="server" ClientInstanceName="btnTema" Width="22px" AutoPostBack="False"
                                                UseSubmitBehavior="False" ToolTip="Tema" Height="22px">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('T');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/Tema.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaCima" runat="server" ClientInstanceName="btnSetaCima" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para cima)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SC');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/Up.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaBaixo" runat="server" ClientInstanceName="btnSetaBaixo" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para baixo)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SB');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/Down.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaDir" runat="server" ClientInstanceName="btnSetaDir" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para direita)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SD');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/Right.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaEsq" runat="server" ClientInstanceName="btnSetaEsq" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para esquerda)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SE');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/Left.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaFinaCima" runat="server" ClientInstanceName="btnSetaFinaCima" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para cima)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SFC');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/UpFina.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaFinaBaixo" runat="server" ClientInstanceName="btnSetaFinaBaixo" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para baixo)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SFB');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/DownFina.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaFinaDir" runat="server" ClientInstanceName="btnSetaFinaDir" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para direita)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SFD');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/RightFina.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 30px; height: 25px">
                                            <dxe:ASPxButton ID="btnSetaFinaEsq" runat="server" ClientInstanceName="btnSetaFinaEsq" Width="22px"
                                                AutoPostBack="False" UseSubmitBehavior="False" Height="22px" ToolTip="Causa e efeito (para esquerda)">
                                                <ClientSideEvents Click="function(s, e) {
	insereObjeto('SFE');
}" />
                                                <Image Url="~/imagens/wizardMapaEstrategico/LeftFina.png" />
                                                <Paddings Padding="0px" />
                                                <FocusRectPaddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="height: 20px;">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="middle" style="border-bottom: gray 1px solid; text-align: center; height: 20px; background-color: gainsboro;" align="center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Overline="False"
                                    Font-Strikeout="False" Text=" Propriedades"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                            <td align="center" valign="middle">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr style="height: 20px">
                                        <td style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;" align="center">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Título"></asp:Label></td>
                                        <td style="border-left: gainsboro 1px solid; border-right: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid; width: 125px;">
                                            <dxe:ASPxTextBox ID="txtObjeto" runat="server" ClientInstanceName="txtObjeto" Width="100%"
                                                CssClass="color" ReadOnly="True"></dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;" align="center">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Texto"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxTextBox ID="txtTexto" runat="server" ClientInstanceName="txtTexto" Width="100%" MaxLength="250">
<ClientSideEvents TextChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;T&quot;);	
}"></ClientSideEvents>
</dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;" align="center">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Fundo"></asp:Label></td>
                                        <td style="border-left: gainsboro 1px solid; border-right: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxColorEdit ID="txtCorFundo" runat="server" ClientInstanceName="txtCorFundo" Width="100%">
<ClientSideEvents ColorChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;CFu&quot;);
}"></ClientSideEvents>
</dxe:ASPxColorEdit>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Borda"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxColorEdit ID="txtCorBorda" runat="server" ClientInstanceName="txtCorBorda" Width="100%">
<ClientSideEvents ColorChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;CBo&quot;);
}"></ClientSideEvents>
</dxe:ASPxColorEdit>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Fonte"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxColorEdit ID="txtCorFonte" runat="server" ClientInstanceName="txtCorFonte" Width="100%">
<ClientSideEvents ColorChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;CFo&quot;);
}"></ClientSideEvents>
</dxe:ASPxColorEdit>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;" align="center">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Esquerda"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxTextBox ID="txtEsquerda" runat="server" ClientInstanceName="txtEsquerda" Width="100%"
                                                MaxLength="3">
<ClientSideEvents TextChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;E&quot;);
}"></ClientSideEvents>
</dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Superior"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxTextBox ID="txtSuperior" runat="server" ClientInstanceName="txtSuperior" Width="100%"
                                                MaxLength="3">
<ClientSideEvents TextChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;S&quot;);
}"></ClientSideEvents>
</dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Largura"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxTextBox ID="txtLargura" runat="server" ClientInstanceName="txtLargura" Width="100%"
                                                MaxLength="3">
<ClientSideEvents TextChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;L&quot;);
}"></ClientSideEvents>
</dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" style="border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Overline="False"
                                                Font-Strikeout="False" Text="Altura"></asp:Label></td>
                                        <td style="border-right: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; border-top: gainsboro 1px solid;">
                                            <dxe:ASPxTextBox ID="txtAltura" runat="server" ClientInstanceName="txtAltura" Width="100%"
                                                MaxLength="3">
<ClientSideEvents TextChanged="function(s, e) {
	ajustaPropriedadeObjetoSelecionado(s.GetText(), &quot;A&quot;);
}"></ClientSideEvents>
</dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" colspan="2" valign="middle">
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td align="center" colspan="2" valign="middle">
                                            <dxe:ASPxButton ID="btnExcluirObjeto" runat="server" Text="Excluir Objeto Selecionado" ClientInstanceName="btnExcluirObjeto"
                                                AutoPostBack="False" UseSubmitBehavior="False" font-italic="False" >
<ClientSideEvents Click="function(s, e) {
	removeElemento('botaoDel');
}"></ClientSideEvents>
</dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral" OnCustomCallback="hfGeral_CustomCallback">
            <ClientSideEvents EndCallback="function(s, e) {
	onEnd_hfCallback();
}" />
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
