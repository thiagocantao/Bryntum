<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opcoes.aspx.cs" Inherits="_Estrategias_wizard_opcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var myObject = new Object();
        function getTelaInicial()
        {
            //window.parent.telaInicialOE = nvbMenuProjeto.cp_TelaInicial;
            var url = nvbMenuProjeto.cp_TelaInicial;
	        parent.telaInicialOE = url;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 22px;
        }
    </style>
</head>
<body style="margin: 0px;" onload='getTelaInicial()'>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);">
            <tr height="26px">
                <td valign="middle" style="height: 26px;">
                    <table>
                        <tr>
                            <td style="padding-left: 10px; display: none;" class="style1">
                                <dxe:ASPxImage ID="imgMensagens" runat="server" ClientInstanceName="imgMensagens"
                                    ImageUrl="~/imagens/questao.gif" ToolTip="Incluir uma nova mensagem" Width="14px" Cursor="pointer">
                                    <ClientSideEvents Click="function(s, e) 
{
	var codOE = hfGeral.Get('hfCodigoObjetivo');
	var nomeOE = hfGeral.Contains('hfNomeObjetivo') ? hfGeral.Get('hfNomeObjetivo'): &quot;&quot;;
     
     myObject.nomeProjeto = nomeOE; 

    window.top.showModal(&quot;novaMensagemTema.aspx?TipoReuniao=E&amp;MOD=EST&amp;IOB=OB&amp;COE=&quot; + codOE, 'Nova Mensagem', 710, 430, '', myObject);

}" />
                                </dxe:ASPxImage>
                            </td>
                            <td style="padding-left: 10px">
                    <asp:Label ID="Label1" runat="server" Font-Overline="False"
                        Font-Strikeout="False" Text="Tema" Font-Bold="True"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; height:<%=alturaTabela %>">
            <tr>
                <td style="width: 7px" valign="top">
                </td>
                <td valign="top" >
                    <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server" AutoCollapse="True"
                        ClientInstanceName="nvbMenuProjeto"  Width="100%"><Groups>
<dxnb:NavBarGroup Name="Moe" Text="Principal"><Items>
<dxnb:NavBarItem Name="Det" Text="Detalhes"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Tdl" Text="Planos de A&#231;&#245;es" Visible="False"></dxnb:NavBarItem>
</Items>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="Com" Text="Comunica&#231;&#227;o" Visible="False"><Items>
<dxnb:NavBarItem Name="Ane" Text="Anexos"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Men" Text="Mensagens"></dxnb:NavBarItem>
</Items>
</dxnb:NavBarGroup>
</Groups>

<ClientSideEvents ItemClick="function(s, e) 
{
	parent.lblTitulo.SetText(e.item.GetText());
}" Init="function(s, e) {	
	parent.lblTitulo.SetText(s.cp_NomeTelaInicial);
	parent.document.getElementById('oe_desktop').src = s.cp_TelaInicial;
}"></ClientSideEvents>

<Paddings PaddingLeft="5px" PaddingTop="5px" PaddingRight="5px"></Paddings>
</dxnb:ASPxNavBar>
                </td>
                <td style="width: 7px" valign="top">
                </td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
