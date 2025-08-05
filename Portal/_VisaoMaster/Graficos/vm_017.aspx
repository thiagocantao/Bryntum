<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_017.aspx.cs" Inherits="_VisaoMaster_Graficos_vm_017" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function abreTabela(codigoArea, statusRegistro, tipoItens) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Itens ' + tipoItens + ' - Acompanhamento do Escopo';

            if (ASPxRoundPanel1.cp_MostraEscopoUHEArvore == 'S')
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/treeListEscopo.aspx?Altura=' + (altura - 65) + '&CA=' + codigoArea + '&ST=' + statusRegistro, titulo, largura, altura, '', null);
            else
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaEscopo.aspx?Altura=' + (altura - 100) + '&CA=' + codigoArea + '&ST=' + statusRegistro, titulo, largura, altura, '', null);
        }

        function abreEscopo(codigoArea, nomeArea) {
            largura = screen.width - 60;
            altura = screen.height - 260;
            var titulo = 'Acompanhamento do Escopo' + (nomeArea != '' ? (' - ' + nomeArea) : '');

            if (ASPxRoundPanel1.cp_MostraEscopoUHEArvore == 'S')
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/treeListEscopo.aspx?Altura=' + (altura - 65) + '&CA=' + codigoArea, titulo, largura, altura, '', null);
            else
                window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/tabelaEscopo.aspx?Altura=' + (altura - 100) + '&CA=' + codigoArea, titulo, largura, altura, '', null);
        }

    </script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
            CssPostfix="PlasticBlue" HeaderText="Desempenho"
            Height="215px" Width="210px" ImageFolder="~/App_Themes/PlasticBlue/{0}/" 
            ClientInstanceName="ASPxRoundPanel1" EnableViewState="False">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB">
                <Paddings Padding="0px" PaddingBottom="2px" />
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
                    <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <div id="chartdiv" align="center">
                    </div>

                    <script type="text/javascript">
                        getGrafico('<%=grafico_swf %>', "grafico001", '<%=larguraGrafico %>', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                    </script></td>
            </tr>
            <tr>
                <td align="left" valign="top" >
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
                         
                        Text="Fonte: Gerência de Informações Gerenciais">
                    </dxe:ASPxLabel>
                </td>
            </tr>
        </table> 
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%;" cellspacing="0" cellpadding="0"><TBODY><tr><td align=left><%=tituloPainel %></td><td align=right><IMG style="CURSOR: pointer" 
                    onclick="javascript:f_zoomGrafico('../zoom.aspx', '', 'Flashs/Pie2D.swf', '<%=grafico_xmlzoom %>', '0')" 
                    alt="Visualizar gráfico em modo ampliado" src="../../imagens/zoom.PNG" 
                    align=right 
                    /></td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
               
        </div>
    </form>
</body>
</html>
