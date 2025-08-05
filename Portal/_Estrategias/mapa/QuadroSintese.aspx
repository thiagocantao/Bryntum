<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuadroSintese.aspx.cs" Inherits="_Estrategias_mapa_Macrometa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 24px;
            width: 115px;
        }
    </style>
    </head>
<body style="overflow-x:hidden; margin:0px; margin-top:0px">
    <form id="form1" runat="server">    
        <table cellpadding="0" cellspacing="0" width="100%">                      
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
            
                <dxwtl:aspxtreelist EnableViewState="False" id="tlMapaEstrategico" runat="server" autogeneratecolumns="False"
                    clientinstancename="tlMapaEstrategico"  keyfieldname="Codigo"
                    parentfieldname="CodigoPai" width="100%">
                    <Settings VerticalScrollBarMode="Visible" />
<Styles>
<Cell VerticalAlign="Middle"></Cell>
    <Header>
        <BorderLeft BorderStyle="None" />
        <BorderTop BorderStyle="None" />
        <BorderRight BorderStyle="None" />
    </Header>
</Styles>
<Columns>
<dxwtl:TreeListTextColumn FieldName="Descricao" Name="Descricao" 
        Caption="Descri&#231;&#227;o" VisibleIndex="0"><DataCellTemplate>
 <%# getDescricao() %>
</DataCellTemplate>
</dxwtl:TreeListTextColumn>
</Columns>
                    <Border BorderStyle="None" />
</dxwtl:aspxtreelist>
<span id="spanLegenda" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="<%=estiloFooter %>" align="left" >
                            <dxp:aspxpanel id="paLegenda" runat="server" clientinstancename="paLegenda" width="100%">
<Border BorderColor="Gray"></Border>

<Paddings PaddingLeft="10px" PaddingTop="3px"></Paddings>
<PanelCollection>
<dxp:PanelContent ID="PanelContent1" runat="server"><table cellspacing="0" cellpadding="0"><tbody><tr><td style="WIDTH: 25px; HEIGHT: 24px"><IMG alt="" src="../../imagens/verde.gif" /></td><td style="WIDTH: 75px; HEIGHT: 24px"><SPAN><asp:Label runat="server" Text="Satisfat&#243;rio" ID="Label10" EnableViewState="False"></asp:Label>
 </SPAN></td><td style="WIDTH: 25px; HEIGHT: 24px"><IMG alt="" src="../../imagens/amarelo.gif" /></td><td style="WIDTH: 55px; HEIGHT: 24px"><SPAN><asp:Label runat="server" Text="Aten&#231;&#227;o" ID="Label11" EnableViewState="False"></asp:Label>
 </SPAN></td><td style="WIDTH: 25px; HEIGHT: 24px"><IMG style="MARGIN-TOP: 0px" alt="" src="../../imagens/vermelho.gif" /></td><td style="WIDTH: 45px; HEIGHT: 24px"><SPAN><asp:Label runat="server" Text="Cr&#237;tico" ID="Label12" EnableViewState="False"></asp:Label>
 </SPAN></td><td style="WIDTH: 25px; HEIGHT: 24px"><IMG style="MARGIN-TOP: 0px" alt="" src="../../imagens/Branco.gif" /></td><td style="WIDTH: 100px; HEIGHT: 24px"><asp:Label runat="server" Text="Sem informa&#231;&#227;o" ID="Label13" EnableViewState="False"></asp:Label>
 </td><td style="WIDTH: 27px; HEIGHT: 24px" align="center"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/TemaCombo.png" ID="Image2" EnableViewState="False"></asp:Image>
 </td><td style="WIDTH: 50px; HEIGHT: 24px"><asp:Label runat="server" Text="Tema" Font-Size="7pt" ID="Label1" EnableViewState="False"></asp:Label>
 </td><td style="WIDTH: 27px; HEIGHT: 24px" align="center"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/Objetivo.png" ID="Image1" EnableViewState="False"></asp:Image>
 </td><td style="WIDTH: 55px; HEIGHT: 24px"><asp:Label runat="server" Text="Objetivos" ID="Label14" EnableViewState="False"></asp:Label>
 </td><td style="WIDTH: 29px; HEIGHT: 24px" align="center"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/indicador.png" ID="Image4" EnableViewState="False"></asp:Image>
 </td><td style="WIDTH: 53px; HEIGHT: 24px"><asp:Label runat="server" Text="Indicador" Font-Size="7pt" ID="Label15" EnableViewState="False"></asp:Label>
 </td><td style="WIDTH: 27px; HEIGHT: 24px" align="center"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/Acao.png" ID="Image3" EnableViewState="False"></asp:Image>
 </td><td class="style1"><asp:Label runat="server" Text="Ação Transformadora" ID="Label2" EnableViewState="False"></asp:Label>
 </td><td style="WIDTH: 29px; HEIGHT: 24px" align="center"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/projeto.png" ID="Image5" EnableViewState="False"></asp:Image>
 </td><td style="WIDTH: 53px; HEIGHT: 24px"><asp:Label runat="server" Text="Projeto" Font-Size="7pt" ID="Label3" EnableViewState="False"></asp:Label>
 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxp:aspxpanel>
                        </td>
                    </tr>
                </table>
            </tr>
        </table>
    </form>
</body>
</html>
