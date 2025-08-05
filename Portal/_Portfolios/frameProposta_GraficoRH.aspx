<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_GraficoRH.aspx.cs" Inherits="_Portfolios_frameProposta_RH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Disponibilidade dos Recursos</title>
     <script type="text/javascript" src="../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <style type="text/css">

.dxbButton
{
	color: #000000;
	font: normal 12px Tahoma, Geneva, sans-serif;
	vertical-align: middle;
	border: 1px solid #7F7F7F;
	padding: 1px;
	cursor: pointer;
}
        .style1
        {
            border-width: 0px;
            padding-left: 8px;
            padding-right: 8px;
            padding-top: 3px;
            padding-bottom: 4px;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="center" style="height: 10px">
                </td>
            </tr>
               <tr>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="pnRH" runat="server" BackColor="#FFFFFF" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" 
                         HeaderText="Disponibilidade dos Recursos" ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
                        <ContentPaddings Padding="1px" />
                        
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <div id="chartdiv" align="center">
                                </div>

                                <script type="text/javascript">
                                       getGrafico('<%=grafico_swf %>', "grafico001", '820px', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                                </script>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
               <tr>
                <td align="right">
                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css"
                        Width="90px" ID="btnFechar" Visible="False">
<ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.parent.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
