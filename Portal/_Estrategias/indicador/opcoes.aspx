<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opcoes.aspx.cs" Inherits="_Estrategias_indicador_opcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
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
            width: 15px;
        }

                html #sp_Tela_0_CC{
	        width:217px !important;

        }
    </style>


</head>
<body style="margin: 0px;" onload='getTelaInicial()'>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" 
            style="border-right: #e4e3e4 1px solid; width:100%; height:<%=alturaTabela %>">
            <tr>
                <td valign="top" >
                    <dxnb:ASPxNavBar ID="nvbMenuProjeto" runat="server" AutoCollapse="True"
                        ClientInstanceName="nvbMenuProjeto"  
                        Width="100%" CssClass="titleDetails"><Groups>
<dxnb:NavBarGroup Name="Ind" Text="Principal"><Items>
<dxnb:NavBarItem Name="Res" Text="Detalhes"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Ben" Text="Benchmarking" Visible="False"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Ana" Text="An&#225;lises"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Tdl" Text="Plano de A&#231;&#227;o"></dxnb:NavBarItem>
    <dxtv:NavBarItem Name="Atm" Text="Atualização de Metas">
    </dxtv:NavBarItem>
    <dxtv:NavBarItem Name="Atr" Text="Atualização de Resultados">
    </dxtv:NavBarItem>
</Items>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="Com" Text="Comunica&#231;&#227;o"><Items>
<dxnb:NavBarItem Name="Anex" Target="indicador_desktop" Text="Anexos"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Men" Target="indicador_desktop" Text="Mensagens"></dxnb:NavBarItem>
</Items>
</dxnb:NavBarGroup>
<dxnb:NavBarGroup Expanded="False" Name="Con" Text="Relat&#243;rios"><Items>
<dxnb:NavBarItem Name="Ddi" Text="Dados do Indicador"></dxnb:NavBarItem>
<dxnb:NavBarItem Name="Udi" Text="Unidades do Indicador"></dxnb:NavBarItem>
</Items>
</dxnb:NavBarGroup>
</Groups>

<ClientSideEvents ItemClick="function(s, e) 
{
    window.parent.pnCarregarClick.Show();
}" ExpandedChanged="function(s, e) 
{

}" HeaderClick="function(s, e) 
{

}"></ClientSideEvents>

<Paddings PaddingLeft="5px" PaddingTop="5px" PaddingRight="5px"></Paddings>
</dxnb:ASPxNavBar>
                    <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
