<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_002.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_vc_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Desempenho"
            Height="215px" Width="210px" ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div id="chartdiv" align="center">
                    </div>

                    <script type="text/javascript">
                          getGrafico('<%=grafico_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                    </script>

                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td align=left><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Desempenho">
                            </dxe:ASPxLabel>  </td><td align=right><IMG style="CURSOR: pointer" 
                    onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/Pie2D.swf', '<%=grafico_xmlzoom %>', '0')" 
                    alt="Visualizar gráfico em modo ampliado" src="../../imagens/zoom.PNG" 
                    align=right 
                    /></td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
