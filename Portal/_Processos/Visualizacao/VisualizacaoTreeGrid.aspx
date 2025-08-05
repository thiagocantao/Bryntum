<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoTreeGrid.aspx.cs"
    Inherits="_Processos_Visualizacao_VisualizacaoTreeGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">

        function reloadPage() {
            
            document.location.reload();
        }

        function SalvarConsulta() {
            callback.PerformCallback("salvar");
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

        function treeList_ContextMenu(s, e) {
            if (e.objectType == "Header")
                pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
        }

        function SalvarConfiguracoesLayout() {
            //if (confirm('Deseja salvar as alterações realizadas no layout da consulta?'))
            callback.PerformCallback("save_layout");
        }

        function RestaurarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
            window.parent.mostraConfirmacao(Resources.traducao.VisualizacaoTreeGrid_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
        }

    </script>
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div>
            <div id="divConteudo" style="margin: 2px;">

                <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientVisible="False" ClientInstanceName="tlDados"
                    DataSourceID="dataSource" OnHtmlDataCellPrepared="tlDados_HtmlDataCellPrepared"
                    Width="100%" OnCustomJSProperties="tlDados_CustomJSProperties" OnExportRenderBrick="tlDados_ExportRenderBrick">
                    <Toolbars>
                        <dxwtle:TreeListToolbar ItemAlign="Right" Name="MainToolbar">
                            <Items>
                                <dxwtle:TreeListToolbarItem DisplayMode="Text" Text="" Name="titulo">
                                    <ItemStyle Font-Bold="True" Width="100%" Cursor="default">
                                        <HoverStyle BackColor="Transparent">
                                        </HoverStyle>
                                        <Paddings PaddingLeft="30px" />
                                    </ItemStyle>
                                </dxwtle:TreeListToolbarItem>
                                <dxwtle:TreeListToolbarItem Name="preFiltro" Visible="true" Text="Pré Filtro TreeGrid">
                                    <Image Url="~/imagens/filtroOrcamentacaoProjeto.png">
                                    </Image>
                                </dxwtle:TreeListToolbarItem>
                                <dxwtle:TreeListToolbarItem Name="layout" Text="Layout">
                                    <Items>
                                        <dxwtle:TreeListToolbarItem Name="abrir" Text="Abrir">
                                            <Image  IconID="actions_open_svg_dark_16x16">
                                            </Image>
                                        </dxwtle:TreeListToolbarItem>
                                        <dxwtle:TreeListToolbarItem Name="salvar" Text="Salvar">
                                            <Image IconID="save_save_svg_dark_16x16">
                                            </Image>
                                        </dxwtle:TreeListToolbarItem>
                                        <dxwtle:TreeListToolbarItem Name="salvar_como" Text="Salvar como">
                                            <Image IconID="save_saveas_svg_dark_16x16">
                                            </Image>
                                        </dxwtle:TreeListToolbarItem>
                                        <dxwtle:TreeListToolbarItem Name="carregar_configuracoes_originais" Text="Carregar configurações originais">
                                            <Image IconID="dashboards_undo_svg_dark_16x16">
                                            </Image>
                                        </dxwtle:TreeListToolbarItem>
                                    </Items>
                                    <Image IconID="setup_pagesetup_svg_dark_16x16">
                                    </Image>
                                </dxwtle:TreeListToolbarItem>
                                <dxwtle:TreeListToolbarItem Text="Exportar" Name="exportar">
                                    <Items>
                                        <dxwtle:TreeListToolbarItem Command="ExportToXlsx" Text="Xlsx" Name="exportar_xlsx">
                                        </dxwtle:TreeListToolbarItem>
                                        <dxwtle:TreeListToolbarItem Command="ExportToPdf" Text="Pdf" Name="exportar_pdf">
                                        </dxwtle:TreeListToolbarItem>
                                        <dxwtle:TreeListToolbarItem Command="ExportToRtf" Text="Rtf" Name="exportar_rtf">
                                        </dxwtle:TreeListToolbarItem>
                                    </Items>
                                    <Image IconID="iconbuilder_actions_arrow2down_svg_dark_16x16">
                                    </Image>
                                </dxwtle:TreeListToolbarItem>
                            </Items>
                        </dxwtle:TreeListToolbar>
                    </Toolbars>
                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                    <SettingsBehavior AutoExpandAllNodes="True" />
                    <SettingsResizing ColumnResizeMode="Control" />
                    <SettingsCustomizationWindow Caption="Seletor de campos" Enabled="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" />
                    <SettingsText CustomizationWindowCaption="Seletor de campos" />
                    <SettingsExport EnableClientSideExportAPI="True">
                        <PageSettings Landscape="True" PaperKind="A4">
                            <Margins Left="50" Right="50" />
                        </PageSettings>
                    </SettingsExport>
                    <Styles>
                        <Header Wrap="True">
                        </Header>
                        <Cell Wrap="True">
                        </Cell>
                    </Styles>
                    <StylesToolbar>
                        <Style Border-BorderStyle="None">
                            <Paddings Padding="0px" />
                        </Style>
                        <Item Border-BorderStyle="None">
                        </Item>
                    </StylesToolbar>
                    <ClientSideEvents ContextMenu="treeList_ContextMenu" Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);    
    height = height - 15;
 	s.SetHeight(height);
    s.SetVisible(true);
}"
                        ToolbarItemClick="function(s, e){ 
	if(e.toolbarName === 'MainToolbar'){
		var indicaPossuiListaUsuario = s.cp_indica_possui_lista_usuario;
		e.processOnServer = false;
		if(e.item.name.indexOf('cf_') === 0){
            var codigoFluxo = parseInt(e.item.name.substring(3));
            callbackCriaInstancia.PerformCallback(codigoFluxo);
        }
		else if(e.item.name == 'abrir'){
			parent.ExibeConsultaSalvas(s.cp_codigo_lista, s.cp_codigo_usuario_logado);
		}
		else if(e.item.name == 'salvar'){
			if(indicaPossuiListaUsuario){
				SalvarConsulta();
			}
			else{
			parent.ExibirJanelaSalvarComo();
			}
		}
		else if(e.item.name == 'salvar_como'){
			parent.ExibirJanelaSalvarComo();
		}
		else if(e.item.name == 'carregar_configuracoes_originais'){
			CarregarConsulta(0);
		}	
        else if(e.item.name == 'preFiltro'){
            window.parent.showModalComFooter('FilterEditorPopup.aspx?cl='+s.cp_codigo_lista+'&clu='+s.cp_codigo_lista_usuario, 'Pré Filtro', 300, 400, reloadPage, null);
         }
	}
}" />
                </dxwtl:ASPxTreeList>
            </div>
        </div>
        <asp:SqlDataSource ID="dataSource" runat="server" OnSelecting="dataSource_Selecting"></asp:SqlDataSource>
        <dxwtle:ASPxTreeListExporter ID="exporter" runat="server" OnRenderBrick="exporter_RenderBrick"
            TreeListID="tlDados">
            <Settings>
                <PageSettings Landscape="True" PaperKind="A4">
                    <Margins Bottom="50" Left="50" Right="50" Top="50" />
                </PageSettings>
            </Settings>
            <Styles>
                <Default>
                </Default>
            </Styles>
        </dxwtle:ASPxTreeListExporter>
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
}" />
        </dxcb:ASPxCallback>
        <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
            <Items>
                <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
                </dxm:MenuItem>
            </Items>
            <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization'){
        tlDados.ShowCustomizationWindow();
}
}" />
        </dxm:ASPxPopupMenu>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
