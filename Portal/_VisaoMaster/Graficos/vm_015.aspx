<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_015.aspx.cs" Inherits="_VisaoMaster_Graficos_vm_015" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 27px;
        }
        .style3
        {
            border: 1px solid #808080;
            height: 25px;
            
            
        }
        .style4
        {
            border: 1px solid #808080;
            height: 25px;
            
            
            color: #FFFFFF;
        }
        .style5
        {
            border: 1px solid #808080;
            
            
        }
    </style>
    <script language="javascript" type="text/javascript">
        function abreTabela(nomeSitio, numeroUnidade, corTarefas, statusTarefa) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Marcos Físicos - ' + nomeSitio;
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaIndicadores.aspx?Altura=' + (altura - 100) + '&NS=' + nomeSitio + '&NUG=' + numeroUnidade + '&ST=' + corTarefas + '&TT=' + statusTarefa, titulo, largura, altura, '', null);
        }

        function abreTabelaDS(nomeSitio, numeroUnidade, corTarefas, statusTarefa) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Marcos Físicos - ' + nomeSitio;
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaIndicadores.aspx?Altura=' + (altura - 100) + '&NS=DS&NUG=' + numeroUnidade + '&ST=' + corTarefas + '&TT=' + statusTarefa, titulo, largura, altura, '', null);
        }

        function abreGantt(nomeSitio, unidadeGeradora) {
            altura = screen.height - 240;
            largura = screen.width - 60;
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/gantt.aspx?Altura=' + (altura - 60) + '&Largura=' + (largura - 20) + '&NS=' + nomeSitio + '&NUG=' + unidadeGeradora, 'Gantt', largura, altura, '', null);


        }

        function abreEscopo(codigoArea, nomeArea) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Acompanhamento do Escopo' + (nomeArea != '' ? (' - ' + nomeArea) : '');

            if (pn3.cp_MostraEscopoUHEArvore == 'S')
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/treeListEscopo.aspx?Altura=' + (altura - 65) + '&CA=' + codigoArea, titulo, largura, altura, '', null);
            else
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaEscopo.aspx?Altura=' + (altura - 100) + '&CA=' + codigoArea, titulo, largura, altura, '', null);
        }

        function abreCusto(codigoArea, nomeArea) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Acompanhamento do Orçamento' + (nomeArea != '' ? (' - ' + nomeArea) : '');
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaOrcamento.aspx?Altura=' + (altura - 100) + '&CA=' + codigoArea, titulo, largura, altura, '', null);
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="pn1" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                        CssPostfix="PlasticBlue" HeaderText="Desempenho" Height="215px" Width="210px"
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" EnableViewState="False">
                        <ContentPaddings Padding="1px" PaddingBottom="2px" />
                        <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                        <ContentPaddings Padding="5px"></ContentPaddings>
                        <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                            <BorderLeft BorderStyle="None"></BorderLeft>
                            <BorderRight BorderStyle="None"></BorderRight>
                            <BorderBottom BorderStyle="None"></BorderBottom>
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                VerticalPosition="bottom" HorizontalPosition="left" />
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                HorizontalPosition="left" VerticalPosition="bottom"></BackgroundImage>
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table cellspacing='0' cellpadding='0'>
                                <tbody>
                                    <tr>
                                        <td align='left'>
                                            <%=tituloPainel1 %>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
                    </dxrp:ASPxRoundPanel>
                </td>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="pn2" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                        CssPostfix="PlasticBlue" HeaderText="Desempenho" Height="215px" Width="210px"
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" EnableViewState="False">
                        <ContentPaddings Padding="1px" PaddingBottom="2px" />
                        <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                        <ContentPaddings Padding="5px"></ContentPaddings>
                        <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                            <BorderLeft BorderStyle="None"></BorderLeft>
                            <BorderRight BorderStyle="None"></BorderRight>
                            <BorderBottom BorderStyle="None"></BorderBottom>
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                VerticalPosition="bottom" HorizontalPosition="left" />
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                HorizontalPosition="left" VerticalPosition="bottom"></BackgroundImage>
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent2" runat="server">
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table cellspacing='0' cellpadding='0'>
                                <tbody>
                                    <tr>
                                        <td align='left'>
                                            <%=tituloPainel2 %>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
                    </dxrp:ASPxRoundPanel>
                </td>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="pn3" runat="server" BackColor="#FFFFFF" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                        CssPostfix="PlasticBlue" HeaderText="Desempenho" Height="215px" Width="210px"
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" EnableViewState="False">
                        <ContentPaddings Padding="1px" PaddingBottom="2px" />
                        <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                        <ContentPaddings Padding="5px"></ContentPaddings>
                        <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                            <BorderLeft BorderStyle="None"></BorderLeft>
                            <BorderRight BorderStyle="None"></BorderRight>
                            <BorderBottom BorderStyle="None"></BorderBottom>
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                VerticalPosition="bottom" HorizontalPosition="left" />
                            <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                                HorizontalPosition="left" VerticalPosition="bottom"></BackgroundImage>
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent3" runat="server">
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table cellspacing='0' cellpadding='0'>
                                <tbody>
                                    <tr>
                                        <td align='left'>
                                            <%=tituloPainel3 %>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
