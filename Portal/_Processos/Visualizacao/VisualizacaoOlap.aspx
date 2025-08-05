<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoOlap.aspx.cs"
    Inherits="_Processos_Visualizacao_VisualizacaoOlap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function resizePivotGrid() {
            debugger;
            var windowHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
            var pivotGrid = document.getElementById('pgDados');
            var offsetTop = pivotGrid.getBoundingClientRect().top;
            var offsetBottom = windowHeight - offsetTop;

            // Defina a altura do PivotGrid com base na altura disponível na janela do navegador
            pivotGrid.style.height = (offsetBottom) + "px";
        }

        // Chame a função de redimensionamento quando a página for carregada e sempre que a janela for redimensionada
        window.onload = resizePivotGrid;
        window.onresize = resizePivotGrid;

        function SalvarConsulta() {
            callback.PerformCallback("salvar");
        }

        function reloadPage() {
            document.location.reload();
        }

        function SalvarConsultaComo(nomeConsulta) {
            callback.PerformCallback("salvar_como;" + nomeConsulta);
        }

        function CarregarConsulta(codigoListaUsuario) {
            var obj = {};
            var variaveis = location.search.substr(1).split('&')
            for (var i = 0; i < variaveis.length; i++) {
                var keyValue = variaveis[i].split('=');
                if (keyValue.length != 2) continue;

                obj[keyValue[0]] = keyValue[1];
            }
            obj['clu'] = codigoListaUsuario;
            var queryString = '?cl=' + obj['cl'] + '&ir=' + obj['ir'] + '&clu=' + obj['clu'];

            var href = location.href;
            location.replace(href.substring(0, href.indexOf('?')) + queryString);
        }

        function SalvarConfiguracoesLayout() {
            //if (confirm('Deseja salvar as alterações realizadas no layout da consulta?'))
            callback.PerformCallback("save_layout");
        }

        function RestaurarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
            window.parent.mostraConfirmacao(Resources.traducao.VisualizacaoOlap_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
        }

    </script>
    <title></title>
    <style type="text/css">
        .dxmMenu {
            font: 12px Tahoma, Geneva, sans-serif;
            color: black;
            background-color: #F0F0F0;
            border: 1px solid #A8A8A8;
            padding: 2px;
        }

        .dxmMenuItemWithImage {
            white-space: nowrap;
        }

        .dxmMenuItemWithImage {
            padding: 4px 8px 5px;
        }

        .dxmMenuItemSpacing {
            width: 2px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div>
            <%--<div id="divTitulo" style="margin: 5px 10px 5px 10px; background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            height: 20px;">
            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                Font-Overline="False" Font-Strikeout="False" Text="Título"></asp:Label>
        </div>--%>
        </div>
        <table runat="server" id="tbBotoes" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr id="Tr1" runat="server">
                <td id="Td1" runat="server" valign="top">
                    <table cellpadding="0" cellspacing="0" style="height: 22px; width: 100%;">
                        <tr>
                            <td style="padding: 0px 2px;">
                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnLoad="menu_Load" Theme="MaterialCompact" Width="100%" ShowAsToolbar="True" SeparatorWidth="0px">
                                    <Paddings Padding="0px" />
                                    <Items>
                                        <dxtv:MenuItem Name="titulo" Text="">
                                            <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="100%" Cursor="default">
                                                <HoverStyle BackColor="Transparent">
                                                </HoverStyle>
                                                <Paddings PaddingLeft="40px" />
                                            </ItemStyle>
                                        </dxtv:MenuItem>
                                        <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                            <Image IconID="iconbuilder_actions_addcircled_svg_dark_16x16">
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxtv:MenuItem Name="btnFilterEditor" Text="" ClientVisible="true" ToolTip="">
                                            <Image Url="~/imagens/filtroOrcamentacaoProjeto.png">
                                            </Image>
                                        </dxtv:MenuItem>
                                        <dxm:MenuItem Name="btnLayout" Text="Layout" ClientVisible="false" ToolTip="Layout">
                                            <Items>
                                                <dxtv:MenuItem Name="btnAbrirConsultas" Text="Abrir" ToolTip="Abrir consultas salvas">
                                                    <Image IconID="actions_open_svg_dark_16x16">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem Name="btnSalvarConsultas" Text="Salvar" ToolTip="Salvar consulta">
                                                    <Image IconID="save_save_svg_dark_16x16">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem Name="btnSalvarComoConsultas" Text="Salvar como" ToolTip="Salvar consulta como">
                                                    <Image IconID="save_saveas_svg_dark_16x16">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem Name="btnCerregarOriginalConsultas" Text="Carregar configurações originais"
                                                    ToolTip="Carrega as configurações originais da consulta">
                                                    <Image IconID="dashboards_undo_svg_dark_16x16">
                                                    </Image>
                                                </dxtv:MenuItem>
                                            </Items>
                                            <Image IconID="setup_pagesetup_svg_dark_16x16">
                                            </Image>
                                            <ItemStyle Font-Bold="True" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="btnExportar" Text="Exportar" ToolTip="Exportar">
                                            <Items>
                                                <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                    <Image IconID="export_exporttoxls_16x16gray">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                    <Image IconID="export_exporttopdf_16x16gray">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                    <Image IconID="export_exporttortf_16x16gray">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                    <Image IconID="export_exporttohtml_16x16gray">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxtv:MenuItem Name="btnCSV" Text="CSV" ToolTip="Exportar para CSV">
                                                    <Image IconID="export_exporttocsv_16x16gray">
                                                    </Image>
                                                </dxtv:MenuItem>
                                            </Items>
                                            <Image IconID="iconbuilder_actions_arrow2down_svg_dark_16x16">
                                            </Image>
                                        </dxm:MenuItem>
                                    </Items>
                                    <Border BorderStyle="None" />
                                </dxm:ASPxMenu>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="padding-right:5px; width:100%; padding-left: 5px;">
            <dxwpg:ASPxPivotGrid ID="pgDados" runat="server" ClientIDMode="AutoID" ClientInstanceName="pgDados"
                DataSourceID="dataSource" Width="99%" OnCustomCellDisplayText="pgDados_CustomCellDisplayText" OnCustomFilterPopupItems="pgDados_CustomFilterPopupItems"
                OnCustomSummary="pgDados_CustomSummary" OnHtmlFieldValuePrepared="pgDados_HtmlFieldValuePrepared" OnHtmlCellPrepared="pgDados_HtmlCellPrepared" Theme="MaterialCompact" OnInit="pgDados_Init">
                <OptionsView DataHeadersDisplayMode="Popup" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                <OptionsCustomization AllowPrefilter="False" CustomizationWindowHeight="275" CustomizationWindowWidth="200" />
                <OptionsLoadingPanel>
                    <Style>
                    
                </Style>
                </OptionsLoadingPanel>
                <OptionsFilter ShowOnlyAvailableItems="True" />
            </dxwpg:ASPxPivotGrid>
        </div>

        <asp:SqlDataSource ID="dataSource" runat="server" OnSelecting="dataSource_Selecting"></asp:SqlDataSource>
        <dxpgwx:ASPxPivotGridExporter ID="exporter" runat="server" OnCustomExportCell="exporter_CustomExportCell"
            ASPxPivotGridID="pgDados" OnCustomExportFieldValue="exporter_CustomExportFieldValue"
            OnCustomExportHeader="exporter_CustomExportHeader">
            <OptionsPrint>
                <PageSettings Landscape="True" PaperKind="A4" Margins="50, 50, 100, 100" />
            </OptionsPrint>
        </dxpgwx:ASPxPivotGridExporter>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter.indexOf('salvar_como') != -1){
		CarregarConsulta(e.result);
	}
	else if(e.parameter.indexOf('salvar') != -1){
		//Se o retorno indicar que a consulta não foi salva ou por a consulta
		 //carregada ter sido excluída ou por ter sido carregada as configurações
		//originais da consulta, é exibida o popup de salvar como
		if(e.result.toLowerCase() == String(true)){
			 parent.ExibirJanelaSalvarComo();
}
}
}
" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
