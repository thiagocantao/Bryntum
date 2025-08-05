<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoGrid.aspx.cs"
    Inherits="_Processos_Visualizacao_VisualizacaoGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" language="javascript">
        function MontaCamposFormulario(codigoInstanciaWf, codigoWorkflow, nomeProcessoWf) {

            var url1 = '../../_Portfolios/Relatorios/popupRelResumoProcessos.aspx?';
            url1 += 'cwf=' + codigoWorkflow;
            url1 += '&ciwf=' + codigoInstanciaWf;
            url1 += '&niwf=' + nomeProcessoWf;
            window.open(url1, 'form', 'resizable=0,width=900px,height=800px,status=no,menubar=no');
        }

        function reloadPage() {
            document.location.reload();
        }

        function mostraPopupGraficoProcesso(CodigoInstanciaWf, CodigoWorkflow, CodigoFluxo, CodigoProjeto) {

            var url1 = "_Portfolios/GraficoProcessoInterno.aspx?";
            url1 += "CW=" + CodigoWorkflow;
            url1 += "&CF=" + CodigoFluxo;
            url1 += "&CI=" + CodigoInstanciaWf;
            url1 += "&CP=" + CodigoProjeto;
            url1 += "&AlturaEtapaPercorrida=" + (window.parent.innerHeight - 250).toString();
            url1 += "&Altura=" + (window.parent.innerHeight - 250).toString();
            url1 += "&Largura=" + (window.parent.innerWidth - 80).toString();
            url1 += "&Popup=S";

            var url = window.parent.pcModal.cp_Path + url1;
            window.parent.showModal2(url, traducao.VisualizacaoGrid_gr_fico_do_processo, window.parent.innerWidth - 30, window.parent.innerHeight - 20, atualizaGrid);
        }

        function mostraPopupFormulario(CodigoInstanciaWf, CodigoWorkflow, CodigoFluxo, CodigoProjeto, OcorrenciaAtual, CodigoEtapaAtual) {

            var url1 = "CW=" + CodigoWorkflow;
            url1 += "&CI=" + CodigoInstanciaWf;
            url1 += "&CE=" + CodigoEtapaAtual;
            url1 += "&CS=" + OcorrenciaAtual;
            url1 += "&CF=" + CodigoFluxo;
            url1 += "&CP=" + CodigoProjeto;
            abreFluxo(url1);
        }

        function abreInclusaoFluxo(CodigoFluxo, CodigoProjeto, acessoEtapaInicial, CodigoWorkflow) {

            var url1 = "CW=" + CodigoWorkflow;
            url1 += "&CF=" + CodigoFluxo;
            url1 += "&CP=" + CodigoProjeto;
            url1 += "&AEI=" + acessoEtapaInicial;

            abreFluxo(url1);
        }

        function abreFluxo(parametros) {

            var url1 = "./wfEngineInterno.aspx?" + parametros;
            url1 += "&Altura=" + (window.parent.innerHeight - 250).toString();
            url1 += "&Largura=" + (window.parent.innerWidth - 80).toString();
            url1 += "&Popup=S";

            var url = window.parent.pcModal.cp_Path + url1;
            window.parent.showModal2(url, traducao.VisualizacaoGrid_formul_rio_din_mico, window.parent.innerWidth - 30, window.parent.innerHeight - 60, atualizaGrid);

        }

        function atualizaGrid(x) {
            if (window.parent.lpAguardeMasterPage)
                window.parent.lpAguardeMasterPage.Hide();

            //gvDados.PerformCallback();
            gvDados.Refresh();
        }

        function mostraPopupHistorico(valores) {
            var CodigoInstanciaWf = valores[0];
            var CodigoWorkflow = valores[1];

            var url1 = "_Portfolios/historicoProcessoInterno.aspx?";
            url1 += "CW=" + CodigoWorkflow;
            url1 += "&CI=" + CodigoInstanciaWf;

            var url = window.parent.pcModal.cp_Path + url1;
            window.parent.showModal(url, traducao.VisualizacaoGrid_hist_rico, window.parent.innerWidth - 40, screen.height - 300, '', atualizaGrid);
        }

        function SalvarConsulta() {
            callback.PerformCallback("Salvar");
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

        function ShowMenu(s, e) {
            if (popupMenu.GetItemCount() > 1)
                setTimeout(function () { popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent)); }, 100);
            //popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
            else {
                window.parent.showModal2(popupMenu.GetItem(0).GetNavigateUrl(), 'Formulário Dinâmico', window.parent.innerWidth - 30, window.parent.innerHeight - 20, atualizaGrid);
                e.processOnServer = false;
            }
        }

        function grid_ContextMenu(s, e) {
            if (e.objectType == "header")
                pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
        }

        function abreNovoFluxoEVT(codigoProjeto, iniciaisFluxo) {
            if (confirm(traducao.VisualizacaoGrid_deseja_iniciar_o_fluxo_de_abertura_do_projeto_)) {
                callback.PerformCallback(codigoProjeto + ";" + iniciaisFluxo);
            }
        }
        
        //function OnToolbarItemClick(s, e) {
        //    if (e.toolbarName === 'MainToolbar') {
        //        var indicaPossuiListaUsuario = gvDados.cp_indica_possui_lista_usuario;
        //        e.processOnServer = false;
        //        if (e.item.name == 'incluir') {
        //            if (e.item.GetItemCount() === 0)
        //                abreFluxo(gvDados.cp_navigate_url);
        //        }
        //        else if (e.item.name.indexOf('cf_') === 0) {
        //            var codigoFluxo = parseInt(e.item.name.substring(3));
        //            callbackCriaInstancia.PerformCallback(codigoFluxo);
        //        }
        //        else if (e.item.name == 'abrir') {
        //            parent.ExibeConsultaSalvas(gvDados.cp_codigo_lista, gvDados.cp_codigo_usuario_logado);
        //        }
        //        else if (e.item.name == 'salvar') {
        //            if (indicaPossuiListaUsuario) {
        //                SalvarConsulta();
        //            }
        //            else {
        //                parent.ExibirJanelaSalvarComo();
        //            }
        //        }
        //        else if (e.item.name == 'salvar_como') {
        //            parent.ExibirJanelaSalvarComo();
        //        }
        //        else if (e.item.name == 'carregar_configuracoes_originais') {
        //            CarregarConsulta(0);
        //        }
        //    }
        //}

        function OnFocusedRowChanged(s, e) {
            if (gvDados.cp_mostra_fab) {
                var rowIndex = gvDados.GetFocusedRowIndex();
                if (rowIndex === -1)
                    fabAcoesGrid.SetActionContext('');
                else
                    gvDados.GetValuesOnCustomCallback(rowIndex, OnGetValuesOnCustomCallback);
            }
        }

        var CI, CW, CF, CP, CS, CE;
        function OnGetValuesOnCustomCallback(values) {
            if (values) {
                CI = values.CI;
                CW = values.CW;
                CF = values.CF;
                CP = values.CP;
                CS = values.CS;
                CE = values.CE;
                var actionName = values.ActionName;
                var expand = false;
                if (actionName)
                    expand = true;
                else
                    actionName = '';
                fabAcoesGrid.SetActionContext(actionName, expand);
            }
        }

        function OnGridInit(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight);
            height = height - 5;
            s.SetHeight(height);
            //document.getElementById('divGrid').style.visibility = '';
            s.SetVisible(true);
        }

        function OnActionItemClick(s, e) {
            switch (e.actionName) {
                case 'grafico':
                    mostraPopupGraficoProcesso(CI, CW, CF, CP);
                    break;
                case 'interagir':
                    mostraPopupFormulario(CI, CW, CF, CP, CS, CE);
                    break;
                default:
                    return;
            }
            gvDados.SetFocusedRowIndex(-1);
        }

        function OnActionCollapsing(s, e) {
            if (e.collapseReason === "CollapseButton") {
                gvDados.SetFocusedRowIndex(-1);
            }
        }
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div>
            <div id="divConteudo" style="margin: 2px;">

                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientVisible="False"
                    DataSourceID="dataSource" Width="100%" ClientInstanceName="gvDados" Font-Bold="False" Font-Names="Verdana" Font-Size="9pt" OnCustomJSProperties="gvDados_CustomJSProperties" OnCustomDataCallback="gvDados_CustomDataCallback" KeyboardSupport="True" OnExportRenderBrick="gvDados_ExportRenderBrick">
                    <SettingsExport PaperKind="A4" EnableClientSideExportAPI="True" ExportEmptyDetailGrid="True" Landscape="True" LeftMargin="50" RightMargin="50" ExcelExportMode="WYSIWYG">
                    </SettingsExport>
                    <Columns>
                    </Columns>
                    <ClientSideEvents ContextMenu="grid_ContextMenu" Init="OnGridInit" ToolbarItemClick="OnToolbarItemClick" FocusedRowChanged="OnFocusedRowChanged" />
                    <SettingsBehavior EnableCustomizationWindow="True" AutoExpandAllGroups="True" AllowFocusedRow="True" />
                    <SettingsDetail ExportMode="Expanded" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowHideDataCellsByColumnMinWidth="True" AllowOnlyOneAdaptiveDetailExpanded="True">
                    </SettingsAdaptivity>
                    <SettingsPager AlwaysShowPager="True">
                        <PageSizeItemSettings Caption="Itens por página:" Visible="True">
                        </PageSizeItemSettings>
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" VerticalScrollBarStyle="Virtual" />
                    <SettingsResizing ColumnResizeMode="NextColumn" />
                    <SettingsPopup>
                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                        <HeaderFilter Height="320px" Width="300px" />
                    </SettingsPopup>
                    <Toolbars>
                        <dxtv:GridViewToolbar ItemAlign="Right" Name="MainToolbar">
                            <Items>
                                <dxtv:GridViewToolbarItem DisplayMode="Text" Text="" Name="titulo">
                                    <ItemStyle Font-Bold="True" Width="100%" Cursor="default">
                                        <HoverStyle BackColor="Transparent">
                                        </HoverStyle>
                                        <Paddings PaddingLeft="40px" />
                                    </ItemStyle>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem Name="incluir" Text="Incluir">
                                    <Image IconID="iconbuilder_actions_addcircled_svg_dark_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                 <dxtv:GridViewToolbarItem Name="preFiltro" Visible="true" Text="">
                                    <Image Url="~/imagens/filtroOrcamentacaoProjeto.png">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem Name="layout" Text="Layout">
                                    <Items>
                                        <dxtv:GridViewToolbarItem Name="abrir" Text="Abrir">
                                            <Image IconID="actions_open_svg_dark_16x16">
                                            </Image>
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Name="salvar" Text="Salvar">
                                            <Image IconID="save_save_svg_dark_16x16">
                                            </Image>
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Name="salvar_como" Text="Salvar como">
                                            <Image IconID="save_saveas_svg_dark_16x16">
                                            </Image>
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Name="carregar_configuracoes_originais" Text="Carregar configurações originais">
                                            <Image IconID="dashboards_undo_svg_dark_16x16">
                                            </Image>
                                        </dxtv:GridViewToolbarItem>
                                    </Items>
                                    <Image IconID="setup_pagesetup_svg_dark_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem Text="Exportar" Name="exportar">
                                    <Items>
                                        <dxtv:GridViewToolbarItem Command="ExportToXlsx" Text="Xlsx" Name="exportar_xlsx">
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Command="ExportToPdf" Text="Pdf" Name="exportar_pdf">
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Command="ExportToRtf" Text="Rtf" Name="exportar_rtf">
                                        </dxtv:GridViewToolbarItem>
                                        <dxtv:GridViewToolbarItem Command="ExportToCsv" Name="exportar_csv" Text="Csv">
                                        </dxtv:GridViewToolbarItem>
                                    </Items>
                                    <Image IconID="iconbuilder_actions_arrow2down_svg_dark_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem Command="ShowSearchPanel">
                                </dxtv:GridViewToolbarItem>
                            </Items>
                        </dxtv:GridViewToolbar>
                    </Toolbars>
                    <Styles>
                        <Header Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                        </Header>
                        <Cell Font-Names="Verdana" Font-Size="10pt">
                        </Cell>
                        <GroupPanel Wrap="False">
                            <Paddings PaddingBottom="8px" PaddingTop="4px" />
                        </GroupPanel>
                        <SearchPanel>
                            <Paddings Padding="0px" />
                        </SearchPanel>
                    </Styles>
                    <StylesToolbar>
                        <Style BackColor="Transparent" Border-BorderStyle="None" SeparatorWidth="0px">
                            <Paddings Padding="0px" />
                        </Style>
                        <Item Border-BorderStyle="None">
                        </Item>
                    </StylesToolbar>
                </dxwgv:ASPxGridView>

                <dxcp:ASPxFloatingActionButton ID="fabAcoesGrid" runat="server" ContainerElementID="gvDados" ClientInstanceName="fabAcoesGrid">
                    <ClientSideEvents ActionItemClick="OnActionItemClick" ActionCollapsing="OnActionCollapsing" />
                    <Items>
                        <dx:FABActionGroup ContextName="opcoes_fluxo_interagir" Text="Opções de Fluxo">
                            <Items>
                                <dx:FABActionItem ActionName="grafico" Text="Gráfico">
                                    <Image IconID="iconbuilder_business_diagram_svg_16x16">
                                    </Image>
                                </dx:FABActionItem>
                                <dx:FABActionItem ActionName="interagir" Text="Interagir">
                                    <Image IconID="iconbuilder_actions_rating_svg_16x16">
                                    </Image>
                                </dx:FABActionItem>
                            </Items>
                        </dx:FABActionGroup>
                        <dx:FABActionGroup ContextName="opcoes_fluxo_visualizar" Text="Opções de Fluxo">
                            <Items>
                                <dx:FABActionItem ActionName="grafico" Text="Gráfico">
                                    <Image IconID="iconbuilder_business_diagram_svg_16x16">
                                    </Image>
                                </dx:FABActionItem>
                                <dx:FABActionItem ActionName="interagir" Text="Visualizar">
                                    <Image IconID="iconbuilder_actions_zoom_svg_16x16">
                                    </Image>
                                </dx:FABActionItem>
                            </Items>
                        </dx:FABActionGroup>
                        <dx:FABActionGroup ContextName="opcoes_fluxo" Text="Opções de Fluxo">
                            <Items>
                                <dx:FABActionItem ActionName="grafico" Text="Gráfico">
                                    <Image IconID="iconbuilder_business_diagram_svg_16x16">
                                    </Image>
                                </dx:FABActionItem>
                            </Items>
                        </dx:FABActionGroup>
                    </Items>
                </dxcp:ASPxFloatingActionButton>
            </div>
        </div>
        <asp:SqlDataSource ID="dataSource" runat="server" OnSelecting="dataSource_Selecting"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dataSourceDetalhe" runat="server" OnSelecting="dataSource_Selecting"></asp:SqlDataSource>
        <dxwgv:ASPxGridViewExporter ID="exporter" runat="server" ExportEmptyDetailGrid="True" Landscape="True" LeftMargin="50" RightMargin="50" OnRenderBrick="exporter_RenderBrick"
            PaperKind="A4">
        </dxwgv:ASPxGridViewExporter>
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

        <dxcp:ASPxCallback ID="callbackCriaInstancia" runat="server"
            ClientInstanceName="callbackCriaInstancia"
            OnCallback="callbackCriaInstancia_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	abreFluxo(s.cp_Parametros);
}" />
        </dxcp:ASPxCallback>

        <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu" OnInit="popupMenu_Init">
            <ClientSideEvents ItemClick="function(s, e) {
           
}" />
        </dxm:ASPxPopupMenu>
        <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
            <Items>
                <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
                </dxm:MenuItem>
            </Items>
            <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
        gvDados.ShowCustomizationWindow();
}" />
        </dxm:ASPxPopupMenu>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
