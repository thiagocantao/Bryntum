<%@ Page Language="C#" AutoEventWireup="true" CodeFile="numeros_004.aspx.cs" Inherits="_VisaoMaster_Graficos_numeros_004" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style87 {
            height: 1px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server" enableviewstate="false">
        <div style="text-align: left">
            <table cellspacing="0" class="headerGrid">
                <tr>
                    <td style="padding-top: 0px">
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Dias para geração da Primeira Turbina" HorizontalAlign="Center" Width="100%"
                            EnableViewState="False">
                            <ContentPaddings Padding="1px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <HeaderContent>
                                <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png"
                                    Repeat="RepeatX" VerticalPosition="bottom" />
                            </HeaderContent>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dx:ASPxGaugeControl ID="gauge" runat="server" BackColor="White"
                                        Height="46px" Width="180px" EnableViewState="False">
                                        <Gauges>
                                            <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#0D8097&quot;/&gt;"
                                                AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#02F0F7&quot;/&gt;"
                                                Bounds="0, 0, 180, 46" DigitCount="6" Name="digitalGauge3"
                                                Padding="10, 20, 10, 20" Text="">
                                                <backgroundlayers>
                                    <dx:DigitalBackgroundLayerComponent AcceptOrder="-1000" 
                                        BottomRight="201.85, 99.9625" Name="digitalBackgroundLayerComponent1" 
                                        ShapeType="Style17" TopLeft="10, 0" ZOrder="1000" />
                                </backgroundlayers>
                                            </dx:DigitalGauge>
                                        </Gauges>
                                        <LayoutPadding All="0" Bottom="0" Left="0" Right="0" Top="0" />
                                    </dx:ASPxGaugeControl>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 0px" class="style87"></td>
                </tr>
                <tr>
                    <td style="padding-top: 0px">
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" BackColor="#FFFFFF"
                            CssPostfix="PlasticBlue" HeaderText="Dias para obtenção da Licença de Operação" HorizontalAlign="Center" Width="100%"
                            EnableViewState="False">
                            <ContentPaddings Padding="1px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <HeaderContent>
                                <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png"
                                    Repeat="RepeatX" VerticalPosition="bottom" />
                            </HeaderContent>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dx:ASPxGaugeControl ID="gauge2" runat="server" BackColor="White"
                                        Height="46px" Width="180px" EnableViewState="False">
                                        <Gauges>
                                            <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#0D8097&quot;/&gt;"
                                                AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#02F0F7&quot;/&gt;"
                                                Bounds="0, 0, 180, 46" DigitCount="6" Name="digitalGauge3"
                                                Padding="10, 20, 10, 20" Text="">
                                                <backgroundlayers>
                                    <dx:DigitalBackgroundLayerComponent AcceptOrder="-1000" 
                                        BottomRight="201.85, 99.9625" Name="digitalBackgroundLayerComponent1" 
                                        ShapeType="Style17" TopLeft="10, 0" ZOrder="1000" />
                                </backgroundlayers>
                                            </dx:DigitalGauge>
                                        </Gauges>
                                        <LayoutPadding All="0" Bottom="0" Left="0" Right="0" Top="0" />
                                    </dx:ASPxGaugeControl>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 0px" class="style87"></td>
                </tr>
                <tr>
                    <td style="padding-top: 0px">
                        <dxrp:ASPxRoundPanel ID="pnUHE" runat="server" BackColor="White" HeaderText="UHE Belo Monte" ShowHeader="False" Width="220px">
                            <ContentPaddings Padding="1px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <HeaderContent>
                                <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png"
                                    Repeat="RepeatX" VerticalPosition="bottom" />
                            </HeaderContent>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                        <tr>
                                            <td>
                                                <dxe:ASPxImage ID="imgUHE" runat="server" Cursor="pointer"
                                                    ImageUrl="~/imagens/ne/imgNE_00.png" Width="290px" Height="111px"
                                                    EnableViewState="False">
                                                    <ClientSideEvents Click="function(s, e) {
	window.parent.parent.abreVisao1();
}"
                                                        Init="function(s, e) {
	if(window.parent.parent.hfPermissoes.Get(&quot;EAP&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                                    <BackgroundImage ImageUrl="~/imagens/ne/FundoUHE.png" />
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style75" style="background-color: #DADADA">
                                                <table>
                                                    <tr>
                                                        <td align="left">&nbsp;</td>
                                                        <td align="center" class="style83"
                                                            style="font-weight: bold;"
                                                            title="Percentual Concluído">
                                                            <dxe:ASPxLabel ID="lblDesempenhoUHE0" runat="server" Font-Bold="True"
                                                                Text="Físico Concluído">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <dxe:ASPxImage ID="imgFisicoUHE" runat="server" ToolTip="Visão Físico"
                                                                ImageUrl="~/imagens/FisicoBranco.png" Cursor="pointer">
                                                                <ClientSideEvents Click="function(s, e) {
	window.parent.parent.abreVisao2();
}"
                                                                    Init="function(s, e) {
	if(window.parent.parent.hfPermissoes.Get(&quot;VisaoGlobal&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td align="center" class="style83"
                                                            style="px; font-weight: bold;"
                                                            title="Percentual Concluído">
                                                            <dxe:ASPxLabel ID="lblDesempenhoUHE" runat="server" Font-Bold="True"
                                                                Text="-">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right">
                                                            <dxe:ASPxImage ID="imgFinanceiroUHE" runat="server"
                                                                ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png"
                                                                Cursor="pointer">
                                                                <ClientSideEvents Click="function(s, e) {
	window.parent.parent.abreVisao4();
}"
                                                                    Init="function(s, e) {
	if(window.parent.parent.hfPermissoes.Get(&quot;VisaoCusto&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 0px" class="style6"></td>
                </tr>
                <tr>
                    <td style="padding-top: 0px">
                        <table cellpadding="0" cellspacing="0" class="style84">
                            <tr>
                                <td align="left">
                                    <dxe:ASPxBinaryImage ID="img001" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="center">
                                    <dxe:ASPxBinaryImage ID="img002" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="right">
                                    <dxe:ASPxBinaryImage ID="img003" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxBinaryImage ID="img004" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="center">
                                    <dxe:ASPxBinaryImage ID="img005" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="right">
                                    <dxe:ASPxBinaryImage ID="img006" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxBinaryImage ID="img007" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="center">
                                    <dxe:ASPxBinaryImage ID="img008" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                                <td align="right">
                                    <dxe:ASPxBinaryImage ID="img009" runat="server" Height="45px" Width="95px" StoreContentBytesInViewState="True"
                                        ViewStateMode="Enabled" EnableViewState="False"
                                        BinaryStorageMode="Session">
                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                        </EmptyImage>
                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxe:ASPxBinaryImage>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
