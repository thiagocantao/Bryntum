<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoTreeGrid_prev.aspx.cs"
    Inherits="_Processos_Visualizacao_VisualizacaoTreeGrid_prev" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <link href="../../estilos/custom.css" rel="stylesheet" />
        <script type="text/javascript" language="javascript">
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
                var queryString = '?cl=' + obj['cl'] + '&ir=' + obj['ir'] + '&clu=' + obj['clu'] + '&IDProjeto=' + obj['IDProjeto'];

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
                var funcObj = {
                    funcaoClickOK: function() {
                        callback.PerformCallback("restore_layout");
                    }
                }
                window.parent.mostraConfirmacao(Resources.traducao.VisualizacaoTreeGrid_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function() {
                    funcObj['funcaoClickOK']()
                }, null);
            }
        </script>
        <title></title>
        <style type="text/css">

        </style>
    </head>

    <body>
        <form id="form1" runat="server" autocomplete="off">
            <input autocomplete="false" name="hidden" type="text" style="display:none;"/>
            <div>
                <div id="divConteudo" style="margin: 5px 10px 5px 10px;">
                    <div id="divBotoes">
                        <table id="tbBotoesEdicao" runat="server" cellpadding="0" cellspacing="0">
                            <tr runat="server">
                                <td runat="server" style="padding: 3px" valign="top">
                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnItemClick="menu_ItemClick" OnLoad="menu_Load">
                                        <Paddings Padding="0px" />
                                        <Items>
                                            <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_incluir %>">
                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                </Image>
                                            </dxm:MenuItem>
                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar %>">
                                                <Items>
                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar_XLS %>">
                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar_PDF %>">
                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar_RTF %>">
                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar_HTML %>" ClientVisible="False">
                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Text="CSV" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_exportar_CSV %>">
                                                        <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                </Image>
                                            </dxm:MenuItem>
                                             
                                            <dxm:MenuItem Name="preFiltro" Text="" ClientVisible="true" ToolTip="" >
                                                         <Image Url="~/imagens/filtroOrcamentacaoProjeto.png">
                                                        </Image>
                                            </dxm:MenuItem>

                                            <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                <Items>
                                                    <dxtv:MenuItem Name="btnAbrirConsultas" Text="<% $Resources:traducao, VisualizacaoTreeGrid_prev_abrir %>" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_abrir_consultas_salvas %>">
                                                        <Image IconID="actions_open_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Name="btnSalvarConsultas" Text="<% $Resources:traducao, VisualizacaoTreeGrid_prev_salvar %>" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_salvar_consulta %>">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Name="btnSalvarComoConsultas" Text="<% $Resources:traducao, VisualizacaoTreeGrid_prev_salvar_como %>" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_salvar_consulta_como %>">
                                                        <Image IconID="save_saveandnew_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Name="btnCerregarOriginalConsultas" Text="<% $Resources:traducao, VisualizacaoTreeGrid_prev_carregar_configuracoes_originais %>" ToolTip="<% $Resources:traducao, VisualizacaoTreeGrid_prev_carregar_configuracoes_originais_consulta %>">
                                                        <Image IconID="actions_reset_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/layout.png">
                                                </Image>
                                            </dxm:MenuItem>

                                        </Items>
                                        <ItemStyle Cursor="pointer">
                                            <HoverStyle>
                                                <border borderstyle="None" />
                                            </HoverStyle>
                                            <Paddings Padding="0px" />
                                        </ItemStyle>
                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                            <SelectedStyle>
                                                <border borderstyle="None" />
                                            </SelectedStyle>
                                        </SubMenuItemStyle>
                                        <Border BorderStyle="None" />
                                    </dxm:ASPxMenu>
                                </td>
                                <td style="padding: 3px 3px 3px 3px;">
                                    <dxcp:ASPxLabel ID="lblNomeConsulta" runat="server" Text="ASPxLabel" Font-Size="12pt">
                                    </dxcp:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados" DataSourceID="dataSource" OnHtmlDataCellPrepared="tlDados_HtmlDataCellPrepared" Width="100%">
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFilterBar="Auto"/>
                        <SettingsBehavior AutoExpandAllNodes="True" />
                        <SettingsResizing ColumnResizeMode="Control" />
                        <SettingsCustomizationWindow Caption="<% $Resources:traducao, VisualizacaoTreeGrid_prev_seletor_campos %>" Enabled="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" />
                        <SettingsText CustomizationWindowCaption="<% $Resources:traducao, VisualizacaoTreeGrid_prev_seletor_campos %>" />
                        <SettingsSearchPanel Visible="True" EditorNullTextDisplayMode="UnfocusedAndFocused" />

<SettingsPopup>
<HeaderFilter MinHeight="300px" CloseOnEscape="False" MinWidth="300px"></HeaderFilter>
</SettingsPopup>

                        <Styles>
                            <Header Wrap="True">
                            </Header>
                            <Cell Wrap="True">
                            </Cell>
                            <SearchPanel Spacing="0px">
                                <Paddings PaddingBottom="0px" />
                            </SearchPanel>
                        </Styles>
                        <ClientSideEvents ContextMenu="treeList_ContextMenu" Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);    
    height = height - 60;
 	s.SetHeight(height);
}" />
                    </dxwtl:ASPxTreeList>
                </div>
            </div>
            <asp:SqlDataSource ID="dataSource" runat="server" OnSelecting="dataSource_Selecting" ></asp:SqlDataSource>
            <dxwtle:ASPxTreeListExporter ID="exporter" runat="server" OnRenderBrick="exporter_RenderBrick" TreeListID="tlDados">
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