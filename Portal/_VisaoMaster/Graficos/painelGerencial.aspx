<%@ Page Language="C#" AutoEventWireup="true" CodeFile="painelGerencial.aspx.cs" Inherits="_VisaoMaster_Graficos_painelGerencial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />  
    <script language="javascript" type="text/javascript">
        function abreVisao001() {
            window.parent.abreVisao2();
        }

        function abreVisao002() {
            window.parent.abreVisao5();
        }

        function abreVisao003() {
            window.parent.abreVisao6();
        }

        function abreVisao004() {
            window.parent.abreVisao7();
        }

        function abreVisao005() {
            window.parent.abreVisao8();
        }

        function abreVisao006() {
            window.parent.abreVisao9();
        }

        function abreVisao007() {
            window.parent.abreVisao10();

        }

        function abreVisao008() {
            window.parent.abreVisao4();
        }

        function abreVisao009() {
            window.parent.abreVisao2();
        }

        function loadPagina() {
            window.parent.document.getElementById('tdArea').style.display = '';
            window.parent.lblTitulo.SetText('Painel de Gestão da UHE Belo Monte');            
        }

        window.parent.imgFotos.SetVisible(false);
        window.parent.imgGrafico.SetVisible(false);
        window.parent.btnDownLoad.SetVisible(false);
        window.parent.btnEAP.SetVisible(false);

        window.parent.idPaginaAtual = 1;
        window.parent.verificaLabelImagensVisivel();
    </script> 
    <style type="text/css">
        .style72
        {
            width: 100%;
            height: 100%;
            margin-bottom: 0px;
        }
        .style75
        {
            height: 25px;
        }
        .style82
        {
            width: 120px;
        }
        .style83
        {
            height: 22px;
        }
        .style84
        {
            height: 21px;
        }
        </style>
</head>
<body style="margin:0px;vertical-align:middle" onload="loadPagina();">
    <form id="form1" runat="server" >
    <table cellpadding="0" cellspacing="0" style="width:100%">
            <tr style="height:<%=alturaTela %>; ">
                <td valign="middle" align="center" >  
        <table cellpadding="0" cellspacing="0" style="width:100%" >
            <tr>
                <td>  
    <div align="center" >  
        <table cellpadding="0" cellspacing="0" class="headerGrid">
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center" colspan="3">
        <dxrp:ASPxRoundPanel ID="pnUHE" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="UHE Belo Monte" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" ShowHeader="False">
            <ContentPaddings Padding="1px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" >
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
                    <table cellpadding="0" cellspacing="0" class="headerGrid" ID="tbPrincipal">
                        <tr>
                            <td>
                                <dxe:ASPxImage ID="imgUHE" runat="server" Cursor="pointer" 
                                    ImageUrl="~/imagens/ne/imgNE_00.png">
                                    <ClientSideEvents Click="function(s, e) {
	abreVisao001();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;VisaoGlobal&quot;) == 'N')
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
                                        <td align="left">
                                            &nbsp;</td>
                                        <td align="center" class="style82" 
                                            style="font-weight: bold; " 
                                            title="Percentual Concluído">
                                            <dxe:ASPxLabel ID="lblDesempenhoUHE0" runat="server" Font-Bold="True" 
                                                 Text="Físico Concluído">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <dxe:ASPxImage ID="imgFisicoUHE" runat="server" ToolTip="Visão Físico" 
                                                ImageUrl="~/imagens/FisicoBranco.png" Cursor="pointer">
                                                <ClientSideEvents Click="function(s, e) {
	 abreVisao009();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;VisaoGlobal&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="center" class="style82" 
                                            style="px; font-weight: bold; " title="Percentual Concluído">
                                            <dxe:ASPxLabel ID="lblDesempenhoUHE" runat="server" Font-Bold="True" 
                                                 Text="-">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <dxe:ASPxImage ID="imgFinanceiroUHE" runat="server" 
                                                ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png" 
                                                Cursor="pointer">
                                                <ClientSideEvents Click="function(s, e) {
	abreVisao008();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;VisaoCusto&quot;) == 'N')
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
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style83">
                    </td>
                <td class="style83">
                    </td>
                <td class="style83" style="padding: 0px">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666; ">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666; ">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td class="style83" style="padding: 0px">
                    &nbsp;</td>
                <td class="style83">
                    </td>
            </tr>
            <tr>
                <td class="titlePanel">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666;">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td class="titlePanel">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td class="titlePanel">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td class="titlePanel">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td class="titlePanel">
                    <table cellpadding="0" cellspacing="0" class="style72">
                        <tr>
                            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #666666; border-top-style: solid; border-top-width: 2px; border-top-color: #666666;">
                                &nbsp;</td>
                            <td style="border-left-style: solid; border-left-width: 1px; border-left-color: #666666;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
            </tr>
            <tr>
                <td align="center">
        <dxrp:ASPxRoundPanel ID="pnPimental" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Sítio Pimental" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Center">
            <ContentPaddings Padding="0px" PaddingBottom="0px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                Height="40px" VerticalAlign="Middle" 
                HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="1px" />
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
                                    <dxe:ASPxImage ID="imgPimental" runat="server" Cursor="pointer" Height="115px" 
                                        ImageUrl="~/imagens/ne/imgNE_02.jpg" Width="260px">
                                        <ClientSideEvents Click="function(s, e) {
	abreVisao002();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;Pimental&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td class="style75" style="background-color: #DADADA">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="center" style="px; font-weight: bold; " 
                                                class="style82" title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoUHE1" runat="server" Font-Bold="True" 
                                                     Text="Físico Concluído">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="imgFisicoPimental" runat="server" 
                                                    ToolTip="Visão Físico" ImageUrl="~/imagens/FisicoBranco.png">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoPimental" runat="server" Font-Bold="True" 
                                                     Text="-">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxImage ID="imgFinanceiroPimental" runat="server" 
                                                    ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png">
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
                <td align="center">
        <dxrp:ASPxRoundPanel ID="pnBeloMonte" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Sítio Belo Monte" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Center">
            <ContentPaddings Padding="0px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                Height="40px" VerticalAlign="Middle" 
                HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="1px" />
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
                                    <dxe:ASPxImage ID="imgBeloMonte" runat="server" Cursor="pointer" Height="115px" 
                                        ImageUrl="~/imagens/ne/imgNE_03.jpg" Width="260px">
                                        <ClientSideEvents Click="function(s, e) {
	abreVisao003();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;BeloMonte&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td class="style75" style="background-color: #DADADA">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " 
                                                title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoUHE2" runat="server" Font-Bold="True" 
                                                     Text="Físico Concluído">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="imgFisicoBeloMonte" runat="server" 
                                                    ToolTip="Visão Físico" ImageUrl="~/imagens/FisicoBranco.png">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoBeloMonte" runat="server" Font-Bold="True" 
                                                     Text="-">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxImage ID="imgFinanceiroBeloMonte" runat="server" 
                                                    ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png">
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
                <td align="center">
        <dxrp:ASPxRoundPanel ID="pnInfra" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Sítio Infraestrutura" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Center">
            <ContentPaddings Padding="0px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                Height="40px" VerticalAlign="Middle" 
                HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="1px" />
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
                                    <dxe:ASPxImage ID="imgInfra" runat="server" Cursor="pointer" Height="115px" 
                                        ImageUrl="~/imagens/ne/imgNE_04.jpg" Width="260px">
                                        <ClientSideEvents Click="function(s, e) {
	abreVisao004();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;Infra&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td class="style75" style="background-color: #DADADA">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " 
                                                title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoUHE3" runat="server" Font-Bold="True" 
                                                     Text="Físico Concluído">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="imgFisicoInfra" runat="server" ToolTip="Visão Físico" 
                                                    ImageUrl="~/imagens/FisicoBranco.png">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoInfra" runat="server" Font-Bold="True" 
                                                     Text="-">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxImage ID="imgFinanceiroInfra" runat="server" 
                                                    ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png">
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
                <td align="center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel8" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Canais de Derivação, Transposição&lt;br&gt; e Enchimento" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Center" 
                        EncodeHtml="False">
            <ContentPaddings Padding="0px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                Height="40px" VerticalAlign="Middle" 
                HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="1px" />
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
                                    <dxe:ASPxImage ID="imgDerivacao" runat="server" Cursor="pointer" Height="115px" 
                                        ImageUrl="~/imagens/ne/imgNE_05.jpg" Width="260px">
                                        <ClientSideEvents Click="function(s, e) {
	abreVisao005();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;Derivacao&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td class="style75" style="background-color: #DADADA">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " 
                                                title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoUHE4" runat="server" Font-Bold="True" 
                                                     Text="Físico Concluído">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="imgFisicoDerivacao" runat="server" 
                                                    ToolTip="Visão Físico" ImageUrl="~/imagens/FisicoBranco.png">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoDerivacao" runat="server" Font-Bold="True" 
                                                     Text="-">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxImage ID="imgFinanceiroDerivacao" runat="server" 
                                                    ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png">
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
                <td align="center">
        <dxrp:ASPxRoundPanel ID="pnDiques" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
            CssPostfix="PlasticBlue" HeaderText="Sítio Diques " 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Center">
            <ContentPaddings Padding="0px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                Height="40px" VerticalAlign="Middle" 
                HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="1px" />
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
                                    <dxe:ASPxImage ID="imgDiques" runat="server" Cursor="pointer" Height="115px" 
                                        ImageUrl="~/imagens/ne/imgNE_07.jpg" Width="260px">
                                        <ClientSideEvents Click="function(s, e) {
	abreVisao006();
}" Init="function(s, e) {
	if(window.parent.hfPermissoes.Get(&quot;Reservatorios&quot;) == 'N')
		s.mainElement.style.cursor = '';
}" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td class="style75" style="background-color: #DADADA">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " 
                                                title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoUHE6" runat="server" Font-Bold="True" 
                                                     Text="Físico Concluído">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxImage ID="imgFisicoDiques" runat="server" ToolTip="Visão Físico" 
                                                    ImageUrl="~/imagens/FisicoBranco.png">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td align="center" class="style82" 
                                                style="px; font-weight: bold; " title="Percentual Concluído">
                                                <dxe:ASPxLabel ID="lblDesempenhoDiques" runat="server" Font-Bold="True" 
                                                     Text="-">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <dxe:ASPxImage ID="imgFinanceiroDiques" runat="server" 
                                                    ToolTip="Visão Orçamento" ImageUrl="~/imagens/FinanceiroBranco.png">
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
            </table>
    </div>
     </td>
            </tr>
            <tr>
            <td class="style84" align="center">
                <dxe:ASPxLabel ID="lblInformacoes" runat="server" Font-Bold="True" 
                    Font-Italic="True" >
                </dxe:ASPxLabel>
                </td>
            </tr>
        </table>    
        </td></tr>
        <tr><td style="background-color: #DADADA" class="style83" height="23" 
                valign="middle">
            <table cellpadding="0" cellspacing="0" style="height: 22">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="padding-left: 10px">
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="imgFisicoPimental0" ImageUrl="~/imagens/FisicoVermelho.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Físico Crítico" 
                                        Font-Bold="False"  ID="lblLeganda" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="ASPxImage1" ImageUrl="~/imagens/FisicoAmarelo.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Físico em Atenção" 
                                        Font-Bold="False"  ID="ASPxLabel1" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="ASPxImage2" ImageUrl="~/imagens/FisicoVerde.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Físico Satisfatório" 
                                        Font-Bold="False"  ID="ASPxLabel2" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="ASPxImage3" ImageUrl="~/imagens/FinanceiroVermelho.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Econômico Crítico" 
                                        Font-Bold="False"  ID="ASPxLabel3" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="heigth: 20px">
                            <tr>
                                <td>
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="ASPxImage4" ImageUrl="~/imagens/FinanceiroAmarelo.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Econômico  em Atenção" 
                                        Font-Bold="False"  ID="ASPxLabel4" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="heigth: 100%">
                            <tr>
                                <td>
                                                <dxe:ASPxImage runat="server" ToolTip="Desempenho F&#237;sico" 
                                        ID="ASPxImage5" ImageUrl="~/imagens/FinanceiroVerde.png" Width="20px"></dxe:ASPxImage>

                                            </td>
                                <td style="padding-left: 5px; padding-right: 20px; padding-bottom: 5px;" 
                                    valign="bottom">
                                                <dxe:ASPxLabel runat="server" Text="Econômico  Satisfatório" 
                                        Font-Bold="False"  ID="ASPxLabel5" 
                                        Font-Italic="True"></dxe:ASPxLabel>

                                            </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td></tr></table>
    <dxcp:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CloseAction="None" 
         HeaderText="Informação" 
        PopupAction="None" PopupHorizontalAlign="LeftSides" 
        PopupHorizontalOffset="5" PopupVerticalAlign="TopSides" PopupVerticalOffset="10" 
        ShowOnPageLoad="True" Width="350px" ShowHeader="False">
        <HeaderImage Url="~/imagens/ajuda.png">
        </HeaderImage>
        <ContentStyle >
            <Paddings Padding="3px" />
        </ContentStyle>
        <ContentCollection>
<dxcp:PopupControlContentControl runat="server">
    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" 
        
        
        Text="Para cálculo da Curva S Geral é considerado, além dos sítios em exibição, o avanço do Projeto Executivo" 
        Font-Italic="True">
    </dxtv:ASPxLabel>
            </dxcp:PopupControlContentControl>
</ContentCollection>
    </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
