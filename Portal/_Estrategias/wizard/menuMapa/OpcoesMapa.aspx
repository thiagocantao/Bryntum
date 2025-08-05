<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpcoesMapa.aspx.cs" Inherits="_Estrategias_wizard_menuMapa_OpcoesMapa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
         <link href="../../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Opcões Mapa</title>
</head>
<body style="margin: 0px; margin-top:10px">
    <form id="form1" runat="server">
    <div>
<!-- TABLE CONTEUDO -->
<table border="0" cellpadding="0" class="classeTableMenu" cellspacing="0" style="height:<%=alturaTabela %>; width: 192px">
<tr>
<td style="padding-right: 7px; padding-left: 7px; width: 184px; padding-top: 5px"valign="top">
    <dxnb:aspxnavbar id="nvbMenuMapa" runat="server" autocollapse="True" clientinstancename="nvbMenuMapa"
         width="100%">
<ItemStyle ></ItemStyle>
<Groups>
<dxnb:NavBarGroup Name="gpMenuMapa" Text="Principal">
<HeaderStyle Wrap="True"></HeaderStyle>

<ItemStyle ></ItemStyle>
</dxnb:NavBarGroup>
</Groups>

<Paddings PaddingLeft="5px" PaddingRight="5px"></Paddings>
</dxnb:aspxnavbar>
</td>
</tr>
</table>

        <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral">
        </dxhf:aspxhiddenfield>
            </div>
    </form>
</body>
</html>
