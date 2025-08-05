<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_028.aspx.cs" Inherits="grafico_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    
    <style type="text/css">

        .style4
        {
            width: 100%;
        }
        .style10
        {
            height: 10px;
        }
        .style11
        {
            width: 10px;
        }
        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel HorizontalAlign="Center" ID="pDesempenhoFisico" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%" ClientInstanceName="pc">
            <ContentPaddings Padding="1px" PaddingTop="2px" />
            <Border BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                ForeColor="#404040" Height="27px">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table>
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
                            <%# getIconeAlertaCronograma() %>
                            <td align="left">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Desempenho Físico - Última Linha de Base Salva">
                                </dxe:ASPxLabel>
                                &nbsp;</td>
                            <td align="right">
                                <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', 'Desempenho Físico - Última Linha de Base Salva', 'Flashs/AngularGauge.swf', '<%=grafico1_xmlzoom %>', '0')"
                                    src="../../../imagens/zoom.PNG" style="cursor: pointer" /></td>
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
    </form>
</body>
</html>
