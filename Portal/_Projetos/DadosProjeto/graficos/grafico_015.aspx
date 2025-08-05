<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_015.aspx.cs" Inherits="_Projetos_VisaoCorporativa_vc_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 210px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho"
            Width="100%" CornerRadius="1px">
            <ContentPaddings Padding="0px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB" Height="27px">
            <Paddings PaddingBottom="2px" PaddingTop="2px" />
            </HeaderStyle>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div id="chartdiv" align="center">
                    </div>

                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table>
                    <tbody>
                        <tr>
                            <td align="left" style="margin: 0px; width:55px">
                                Previsão:
                                </td>
                                <td align="left" class="style1">
                                 <dxe:ASPxComboBox ID="ddlPrevisao" runat="server" AutoPostBack="true"
                                 ClientInstanceName="ddlPrevisao"
                                Width="210px">
                             </dxe:ASPxComboBox>
                        
                                </td>
                                <td align="left" style="margin: 0px; width:55px; padding-left:10px">
                                Periodicidade:
                                </td>
                                <td align="left">
                                <dxe:ASPxComboBox ID="ddlPeriodicidade" runat="server" AutoPostBack="true"
                                 ClientInstanceName="ddlPeriodicidade"
                                Width="95px" SelectedIndex="2">
                                <Items>
                                    <dxe:ListEditItem Text="Mensal" Value="M" />                                    
                                    <dxe:ListEditItem Text="Trimestral" Value="T" />
                                    <dxe:ListEditItem Text="Semestral" Value="S" />
                                    <dxe:ListEditItem Selected="True" Text="Anual" Value="A" />
                                </Items>
                             </dxe:ASPxComboBox>
                        
                                </td>
                            <td align="right" style="margin: 0px; width:50px; padding-left:10px"">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/MSLine.swf', '<%=grafico_xmlzoom %>', '0')"
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
