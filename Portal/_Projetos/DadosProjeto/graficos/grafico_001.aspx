<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_001.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>fusioncharts</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/fusioncharts.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts/js/themes/fusioncharts.theme.fusion.js"></script>
    <style type="text/css">
        .style4 {
            width: 100%;
        }

        .style10 {
            height: 10px;
        }

        .style11 {
            width: 10px;
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
                    <table>
                        <tr>
                            <td>
        <div id="chartdiv"></div>
    </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table>
                    <tbody>
                        <tr>                            
                            <%# getIconeAlertaCronograma() %>
                            <td align="left">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="<%$ Resources:traducao, desempenho_f_sico____ltima_linha_de_base_salva%>">
                                </dxe:ASPxLabel>
                                &nbsp;</td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoomChart.aspx', '<asp:Literal runat="server" Text="<%$ Resources:traducao, desempenho_f_sico____ltima_linha_de_base_salva%>" />', 'angulargauge', 'grafico1')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        <dxpc:ASPxPopupControl ID="popupAlerta" runat="server" ClientInstanceName="popUpAlerta"
                        Width="458px"  
            HeaderText="Atenção" PopupElementID="imgAlerta" PopupHorizontalOffset="10" 
            PopupVerticalOffset="-60">
                        <ContentStyle HorizontalAlign="Left">
                        </ContentStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <table cellpadding="0" cellspacing="0" class="style4">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblAlerta" runat="server" EncodeHtml="False" 
                                                >
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxButton ID="btnEditarCronograma2" runat="server" 
                                                OnClick="btnEditarCronograma_Click" Text="Editar Cronograma" 
                                                Width="130px">
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcDesbloqueio" runat="server" ClientInstanceName="pcDesbloqueio" 
         
        HeaderText="Informação" 
        Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="1" class="style4">
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblDesbloqueio" runat="server" 
                    ClientInstanceName="lblDesbloqueio" >
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td align="right">
                            <dxe:ASPxButton ID="btnDesbloquearCrono" runat="server" 
                                Text="Sim" Width="80px" 
                                OnClick="btnDesbloquearCrono_Click">
                                <ClientSideEvents Click="function(s, e) {
	pcDesbloqueio.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td class="style11">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton4" runat="server" 
                                Text="Não" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	pcDesbloqueio.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcInformacao" runat="server" ClientInstanceName="pcInformacao" 
         
        HeaderText="Informação" 
        Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="1" class="style4">
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblInformacao" runat="server" 
                    ClientInstanceName="lblInformacao" >
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td align="right">
                            <dxe:ASPxButton ID="btnAbrirCronoBloqueado" runat="server" 
                                Text="Sim" Width="80px" 
                                OnClick="btnEditarCronograma_Click">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td class="style11">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton3" runat="server" 
                                Text="Não" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	pcInformacao.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
                </div>
        <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
        var sWidth = Math.max(0, document.documentElement.clientWidth) - 25;
        getGraficoZoom('angulargauge', sWidth, sHeight, '<%=grafico1_jsonzoom %>', 'chartdiv')
        </script>
</form>
</body>

</html>
