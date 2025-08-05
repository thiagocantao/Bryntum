<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_029.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
   
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel HorizontalAlign="Center" ID="pDesempenhoFisico" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto" ClientInstanceName="pc"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%" >
            <ContentPaddings Padding="1px" PaddingTop="2px" />
            <Border BorderStyle="None" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                ForeColor="#404040">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table cellpadding="0" cellspacing="0" class="dxic-fileManager">
                        <tr>
                            <td>
                                <div id="chartdiv1" align="center">
                                </div>                                
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>            
            <ClientSideEvents Init="OnInit" />
            <HeaderTemplate>
                <table>
                    <tbody>
                        <tr>
                            <td align="left" class="style1">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Curva S Física">
                                </dxe:ASPxLabel>
                                &nbsp;</td>                            
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', '<%=grafico_swfZoom %>', '<%=grafico_xmlzoom %>', '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
       </div>
    </form>
</body>
</html>
