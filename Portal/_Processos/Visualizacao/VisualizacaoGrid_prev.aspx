<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizacaoGrid_prev.aspx.cs"
    Inherits="_Processos_Visualizacao_VisualizacaoGrid_prev" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript" language="javascript">

        function reloadPage() {
            document.location.reload();
        }

        function MontaCamposFormulario(codigoInstanciaWf, codigoWorkflow, nomeProcessoWf) {

            var url1 = '../../_Portfolios/Relatorios/popupRelResumoProcessos.aspx?';
            url1 += 'cwf=' + codigoWorkflow;
            url1 += '&ciwf=' + codigoInstanciaWf;
            url1 += '&niwf=' + nomeProcessoWf;
            window.open(url1, 'form', 'resizable=0,width=900px,height=800px,status=no,menubar=no');
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
            callback.PerformCallback(traducao.VisualizacaoGrid_prev_salvar);
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
                setTimeout(function () {
                    popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
                }, 100);
            //popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
            else {
                window.parent.showModal2(popupMenu.GetItem(0).GetNavigateUrl(), traducao.VisualizacaoGrid_prev_fomulario_dinamico, window.parent.innerWidth - 30, window.parent.innerHeight - 20, atualizaGrid);
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
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <%--<div id="divTitulo" style="margin: 5px 10px 5px 10px; background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            height: 20px;">
            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                Font-Overline="False" Font-Strikeout="False" Text="Título"></asp:Label>
        </div>--%>
            <div id="divConteudo" style="margin: 5px 10px 5px 10px;">
                <div id="divBotoes">
                    <table cellpadding="0" cellspacing="0" id="tbBotoes" runat="server" style="display: block !important;">
                        <tr runat="server">
                            <td runat="server" style="padding: 3px" valign="top">
                                <table>
                                    <tr>
                                        <td style="padding: 3px;">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnItemClick="menu_ItemClick" OnLoad="menu_Load">
                                                <Paddings Padding="0px" />
                                                <Items>
                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar %>">
                                                        <Items>
                                                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar_XLS %>">
                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar_PDF %>">
                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar_RTF %>">
                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar_HTML %>" ClientVisible="False">
                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxtv:MenuItem Text="CSV" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_exportar_CSV %>">
                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                </Image>
                                                            </dxtv:MenuItem>
                                                        </Items>
                                                        <Image Url="~/imagens/botoes/btnDownload.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxtv:MenuItem Name="btnFilterEditor" Text="" ClientVisible="true" ToolTip="" >
                                                         <Image>
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">

                                                        <Items>
                                                            <dxtv:MenuItem Name="btnAbrirConsultas" Text="<% $Resources:traducao, VisualizacaoGrid_prev_abrir %>" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_abrir_consultas_salvas %>">
                                                                <Image IconID="actions_open_16x16">
                                                                </Image>
                                                            </dxtv:MenuItem>
                                                            <dxtv:MenuItem Name="btnSalvarConsultas" Text="<% $Resources:traducao, VisualizacaoGrid_prev_salvar %>" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_salvar_consulta %>">
                                                                <Image IconID="save_save_16x16">
                                                                </Image>
                                                            </dxtv:MenuItem>
                                                            <dxtv:MenuItem Name="btnSalvarComoConsultas" Text="<% $Resources:traducao, VisualizacaoGrid_prev_salvar_como %>" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_salvar_consulta_como %>">
                                                                <Image IconID="save_saveandnew_16x16">
                                                                </Image>
                                                            </dxtv:MenuItem>
                                                            <dxtv:MenuItem Name="btnCerregarOriginalConsultas" Text="<% $Resources:traducao, VisualizacaoGrid_prev_carregar_configuracoes_originais %>" ToolTip="<% $Resources:traducao, VisualizacaoGrid_prev_carregar_configuracoes_originais_consulta %>">
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
                                        <td style="padding: 3px;">
                                            <dxcp:ASPxLabel ID="lblNomeConsulta" runat="server" Text="ASPxLabel">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <% if (indicaPodeIncluirNovoRegistro)
                        {%>
                    <%} %>
                </div>
            
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" DataSourceID="dataSource" Width="100%" ClientInstanceName="gvDados" >
                    <SettingsExport PaperKind="A4">
                    </SettingsExport>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FixedStyle="Left" VisibleIndex="0" ShowInCustomizationForm="False" ExportWidth="5" Name="col_acoes">
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" AllowSort="False" />
                            <DataItemTemplate>
                                <%# ObtemHtmlBotoes() %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents ContextMenu="grid_ContextMenu" Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
    height = height - 80;
 	s.SetHeight(height);
}" />
                    <SettingsBehavior EnableCustomizationWindow="True" AutoExpandAllGroups="True" />
                    <SettingsDetail ExportMode="Expanded" />
                    <SettingsResizing ColumnResizeMode="Control" />
                    <SettingsPager AlwaysShowPager="True">
                        <PageSizeItemSettings Caption="<% $Resources:traducao, VizualizacaoGrid_prev_itens_por_p_gina %>" Visible="True">
                        </PageSizeItemSettings>
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" VerticalScrollBarStyle="Virtual" />
                    <SettingsPopup>
                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                        <HeaderFilter Height="320px" Width="300px" />
                    </SettingsPopup>
                </dxwgv:ASPxGridView>
            </div>
        </div>
        <asp:SqlDataSource ID="dataSource" runat="server" OnSelecting="dataSource_Selecting"  ></asp:SqlDataSource>
        <asp:SqlDataSource ID="dataSourceDetalhe" runat="server" OnSelecting="dataSource_Selecting"></asp:SqlDataSource>
        <dxwgv:ASPxGridViewExporter ID="exporter" runat="server" ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True" LeftMargin="50" RightMargin="50" OnRenderBrick="exporter_RenderBrick" PaperKind="A4">
            <Styles>
                <Default>
                </Default>
                <Cell>
                </Cell>
                <AlternatingRowCell>
                </AlternatingRowCell>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter.indexOf('salvar_como') != -1){
		CarregarConsulta(e.result);
	}
	else if(e.parameter.indexOf('salvar') != -1){
		//Se o retorno indicar que a consulta não foi salva ou por a consulta
		 //carregada ter sido excluãda ou por ter sido carregada as configuraçães
		//originais da consulta, é exibida o popup de salvar como
		if(e.result.toLowerCase() == String(true)){
			 parent.ExibirJanelaSalvarComo();
}
}
}
" />
        </dxcb:ASPxCallback>

        <dxcp:ASPxCallback ID="callbackCriaInstancia" runat="server" ClientInstanceName="callbackCriaInstancia" OnCallback="callbackCriaInstancia_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
                lpLoading.Hide();
	abreFluxo(s.cp_Parametros);
}" />
        </dxcp:ASPxCallback>

        <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu" OnInit="popupMenu_Init">
            <ClientSideEvents ItemClick="function(s, e) {
           
}" />
        </dxm:ASPxPopupMenu>
        <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
            <Items>
                <dxm:MenuItem Name="cmdShowCustomization" Text="<% $Resources:traducao, VizualizacaoGrid_prev_selecionar_campos %>">
                </dxm:MenuItem>
            </Items>
            <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
        gvDados.ShowCustomizationWindow();
}" />
        </dxm:ASPxPopupMenu>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxcp:ASPxLoadingPanel ID="lpLoading" ClientInstanceName="lpLoading" runat="server">
        </dxcp:ASPxLoadingPanel>
    </form>
</body>

</html>
