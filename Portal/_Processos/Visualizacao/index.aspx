<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="_Processos_Visualizacao_index" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>

        #div-menu-hamburguer{
            float: left;
        }

        .menu {
            background: #35B86B;
            border-radius: 50%;
            width: 30px;
            height: 30px;
            position: fixed;
            top: 80px;
            left: 7px;
            cursor: pointer;
        }

        .label-titulo {
            position: fixed;
            top: 83px;
            left: 30px;
        }

        .hamburguer {
            position: relative;
            display: block;
            background: #FFF;
            width: 10px;
            height: 2px;
            top: 14px;
            left: 10px;
            transition: .5s ease-in-out;
        }

            .hamburguer:before,
            .hamburguer:after {
                background: #FFF;
                content: '';
                display: block;
                width: 100%;
                height: 100%;
                position: absolute;
                transition: .5s ease-in-out;
            }

            .hamburguer:before {
                top: -5px;
            }

            .hamburguer:after {
                bottom: -5px;
            }

        input {
            display: none;
        }

            input:checked ~ label .hamburguer {
                transform: rotate(45deg);
            }

                input:checked ~ label .hamburguer:before {
                    transform: rotate(90deg);
                    top: 0;
                }

                input:checked ~ label .hamburguer:after {
                    transform: rotate(90deg);
                    bottom: 0;
                }

        .tabela {
            width: 100%;
        }

            .tabela .coluna {
                width: 25%;
            }

        .img-modulos {
            min-width: 75px;
            padding: 10px 0px;
        }

            .img-modulos figcaption {
                padding-top: 5px;
                text-align: center;
            }

            .img-modulos.selected {
                background-color: #35B86B;
            }

            .img-modulos img {
                display: block;
                margin-left: auto;
                margin-right: auto;
                width: 30px;
                height: 30px;
                cursor: pointer;
                transition: transform .2s;
            }

                .img-modulos img:hover {
                    transform: scale(1.5);
                }
    </style>

    <script>
        var botoes;
        function OnChange(cb) {
            if (cb.checked)
                pcMenuRelatorios.Show();
            else
                pcMenuRelatorios.Hide();
        }

        function SelecionarModulo(el, modulo) {
            var selected = document.getElementsByClassName('selected');
            for (var i = 0; i < selected.length; i++) {
                selected[i].classList.remove("selected");
            }
            el.parentElement.classList.toggle("selected");
            cardViewRelatorios.PerformCallback(modulo);
        }

        function ObtemUrlItem(codLista, tipoLista, titulo, enderecoRelatorio) {
            if (enderecoRelatorio) {
                if (enderecoRelatorio.startsWith('~/'))
                    enderecoRelatorio = enderecoRelatorio.substring(1);
                return enderecoRelatorio;
            }

            switch (tipoLista.toUpperCase()) {
                case "FORMULARIO":
                    return "";
                case "OLAP":
                    return "VisualizacaoOlap.aspx?cl=" + codLista + "&titulo=" + titulo;
                case "PROCESSO":
                    return "VisualizacaoGrid.aspx?cl=" + codLista + "&titulo=" + titulo;
                case "RELATORIO":
                    return "VisualizacaoGrid.aspx?ir=S&cl=" + codLista + "&titulo=" + titulo;
                case "ARVORE":
                    return "VisualizacaoTreeGrid.aspx?cl=" + codLista + "&titulo=" + titulo;
                case "DASHBOARD":
                    return "VisualizacaoDashboard.aspx?cl=" + codLista + "&titulo=" + titulo;
                case "REPORT":
                    return "VisualizacaoRelatorio.aspx?cl=" + codLista + "&titulo=" + titulo;
                default:
                    return "";
            }
        }

        function OnCardFocusing(s, e) {
            if (e.visibleIndex === -1) return;
            
            var callbackFunc = function (values) {
                
                var framePrincipal = window.frames['framePrincipal'];
                var codigoLista = values[0];
                var tipoLista = values[1];
                var titulo = values[2];
                var enderecoRelatorio = values[3];
                var url = ObtemUrlItem(codigoLista, tipoLista, titulo, enderecoRelatorio);
                var codigoListaQueryString = getUrlParameter('cl', framePrincipal.location.search);
                /*if (codigoListaQueryString != codigoLista)*/
                framePrincipal.location.replace(url);
                pcMenuRelatorios.Hide();
            }
            s.GetCardValues(e.visibleIndex, 'CodigoLista;TipoLista;TituloLista;URL', callbackFunc);
        }

        function getUrlParameter(name, queryString) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(queryString);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        };

        function OnCardClick(s, e) {
            
            if (e.visibleIndex === s.GetFocusedCardIndex()) {
                s.SetFocusedCardIndex(-1);
            }
            lpAtualizando.Show();
            s.GetCardValues(e.visibleIndex, 'TituloLista', function (titulo) { lblTituloTela.SetText(titulo); });
        }

        function OnCardViewInit(s, e) {
            var obj = document.querySelector('figure.img-modulos > img:first-child');
            if (obj)
                SelecionarModulo(obj, 'ESP');
        }

        function OnFrameLoad() {
            EscondeLoadingPanelSeVisibel();
        }

        function EscondeLoadingPanelSeVisibel() {
            if (window.lpAtualizando && lpAtualizando != undefined && lpAtualizando.IsVisible())
                lpAtualizando.Hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; display: none;">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px;">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div-menu-hamburguer">
        <input id="menu-hamburguer" type="checkbox" onchange="OnChange(this)">
        <label for="menu-hamburguer">
            <div class="menu" style="background-color: transparent;">
                <i class="fas fa-arrow-left fa" style="color:darkgray"></i>
                <!--
                <span class="hamburguer"></span>
                -->
            </div>
                <dxe:ASPxLabel CssClass="label-titulo" ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela" Text="Gestão dinâmica de processos e relatórios" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                </dxe:ASPxLabel>
        </label>
    </div>
    <iframe onload="OnFrameLoad()" name="framePrincipal" scrolling="yes" style="height: <%=alturaTabela %>; width: 100%; margin: 15px 0px 0px 0px; border:none;" id="frmBoletim"></iframe>

    <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback">
    </dxcp:ASPxCallback>
    <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [ListaCampoUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario

DELETE FROM [ListaUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
        InsertCommand="INSERT INTO [ListaUsuario] 
([NomeListaUsuario], [CodigoUsuario], [CodigoLista], [IndicaListaPadrao]) 
VALUES 
(@NomeListaUsuario, @CodigoUsuario, @CodigoLista, (SELECT (CASE WHEN EXISTS(SELECT 1 FROM [ListaUsuario] WHERE [CodigoUsuario] = @CodigoUsuario AND [CodigoLista] = @CodigoLista) THEN 'N' ELSE 'S' END)))"
        SelectCommand="SELECT [NomeListaUsuario], [IndicaListaPadrao], [CodigoListaUsuario] FROM [ListaUsuario] WHERE (([CodigoUsuario] = @CodigoUsuario) AND ([CodigoLista] = @CodigoLista)) ORDER BY [NomeListaUsuario]"
        UpdateCommand="IF @IndicaListaPadrao = &#39;S&#39;
BEGIN
    UPDATE [ListaUsuario] 
            SET [IndicaListaPadrao] = &#39;N&#39; 
     WHERE [CodigoUsuario] = @CodigoUsuario
          AND [CodigoLista] = @CodigoLista
END

    UPDATE [ListaUsuario] 
            SET [NomeListaUsuario] = @NomeListaUsuario, 
                   [IndicaListaPadrao] = @IndicaListaPadrao 
     WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
        ID="sdsConsultas">
        <DeleteParameters>
            <asp:Parameter Name="CodigoListaUsuario" Type="Int64"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NomeListaUsuario" Type="String"></asp:Parameter>
            <asp:SessionParameter Name="CodigoUsuario" SessionField="codUsuario" Type="Int32" />
            <asp:SessionParameter Name="CodigoLista" SessionField="codLista" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter SessionField="codUsuario" Name="CodigoUsuario" Type="Int32"></asp:SessionParameter>
            <asp:SessionParameter SessionField="codLista" Name="CodigoLista" Type="Int32"></asp:SessionParameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="NomeListaUsuario"></asp:Parameter>
            <asp:Parameter Name="IndicaListaPadrao"></asp:Parameter>
            <asp:Parameter Name="CodigoListaUsuario"></asp:Parameter>
            <asp:SessionParameter SessionField="codUsuario" Name="CodigoUsuario"></asp:SessionParameter>
            <asp:SessionParameter SessionField="codLista" Name="CodigoLista"></asp:SessionParameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <dxlp:ASPxLoadingPanel ID="lpAtualizando" runat="server" ClientInstanceName="lpAtualizando" Modal="True" Text="Carregando" HorizontalAlign="Center" VerticalAlign="Middle">
    </dxlp:ASPxLoadingPanel>
    <dxcp:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" ClientInstanceName="popup" CloseAction="CloseButton" CloseAnimationType="Fade" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" OnWindowCallback="popup_WindowCallback">
        <Windows>
            <dxtv:PopupWindow HeaderText="Consultas" Name="winGerenciarConsultas" Width="600px" Modal="True">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dxtv:ASPxGridView ID="gvConsultas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvConsultas" DataSourceID="sdsConsultas" KeyFieldName="CodigoListaUsuario" Width="100%" OnCellEditorInitialize="gvConsultas_CellEditorInitialize" OnCommandButtonInitialize="gvConsultas_CommandButtonInitialize">
                                        <Columns>
                                            <dxtv:GridViewDataTextColumn Caption="Lista" FieldName="NomeListaUsuario" ShowInCustomizationForm="True" VisibleIndex="2" Width="230px">
                                                <PropertiesTextEdit MaxLength="255">
                                                </PropertiesTextEdit>
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoListaUsuario" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                <EditFormSettings Visible="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataCheckColumn Caption="Padrão" FieldName="IndicaListaPadrao" ShowInCustomizationForm="True" VisibleIndex="3" Width="130px">
                                                <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                </PropertiesCheckEdit>
                                            </dxtv:GridViewDataCheckColumn>
                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption="   " ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="130px">
                                            </dxtv:GridViewCommandColumn>
                                            <dxtv:GridViewCommandColumn Caption="  " ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                                            </dxtv:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                                        <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command
}" EndCallback="function(s, e) {
        if(comando == 'DELETEROW')
   {
        callbackBotoes.PerformCallback(comando);  
   }
}" />
                                        <SettingsEditing Mode="Inline">
                                        </SettingsEditing>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                        <SettingsText ConfirmDelete="Deseja excluir a consulta?" />
                                        <SettingsCommandButton>
                                            <UpdateButton RenderMode="Image" Text="Salvar">
                                                <Image AlternateText="Salvar" ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                </Image>
                                            </UpdateButton>
                                            <CancelButton RenderMode="Image" Text="Cancelar">
                                                <Image AlternateText="Cancelar" ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                </Image>
                                            </CancelButton>
                                            <EditButton RenderMode="Image" Text="Editar">
                                                <Image AlternateText="Editar" ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </EditButton>
                                            <DeleteButton RenderMode="Image" Text="Excluir">
                                                <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                    </dxtv:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxCallbackPanel ID="callbackBotoes" runat="server" ClientInstanceName="callbackBotoes" Width="100%" OnCallback="callbackBotoes_Callback">
                                        <PanelCollection>
                                            <dxtv:PanelContent runat="server">
                                                <table style="margin: 10px 0 5px auto;">
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxButton ID="btnConfirmar_SelecaoConsultas" runat="server" ClientInstanceName="btnConfirmar_SelecaoConsultas" Text="Confirmar" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    var keys = gvConsultas.GetSelectedKeysOnPage();
   	 if(keys.length &gt; 0){
        		CarregarConsulta(keys[0]);
	   	 popup.HideWindow(winGerenciarConsultas);
   	 }
	else{
		//window.top.mostraMensagem(traducao.index_nenhuma_consulta_foi_selecionada, 'atencao', true, false, null);
        CarregarConsulta(null);
        popup.HideWindow(winGerenciarConsultas);
	}
}" />
                                                            </dxtv:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxtv:ASPxButton ID="btnCancelar_SelecaoConsultas" runat="server" ClientInstanceName="btnCancelar_SelecaoConsultas" Text="Cancelar" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(winGerenciarConsultas);
}" />
                                                            </dxtv:ASPxButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxtv:PanelContent>
                                        </PanelCollection>
                                    </dxtv:ASPxCallbackPanel>
                                </td>
                            </tr>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
            <dxtv:PopupWindow HeaderText="Salvar como" Name="winSalvarComo" Width="400px" Modal="True">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table>
                            <tr>
                                <td>

                                    <dxtv:ASPxTextBox ID="txtNomeConsulta" runat="server" ClientInstanceName="txtNomeConsulta" Width="350px">
                                    </dxtv:ASPxTextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="margin: 10px 0 5px auto;">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxButton ID="btnConfirmar_SalvarComo" runat="server" ClientInstanceName="btnConfirmar_SalvarComo" Text="Confirmar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	var nomeConsulta = txtNomeConsulta.GetText().replace(/'/g, &quot;''&quot;);
	if(nomeConsulta == null || nomeConsulta.trim() == ''){
		window.top.mostraMensagem(traducao.index_informe_o_nome_da_consluta, 'atencao', true, false, null);
	}
	else{
		SalvarConsultaComo(nomeConsulta);
		popup.HideWindow(winSalvarComo);
	}
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td>
                                                <dxtv:ASPxButton ID="btnCancelar_SalvarComo" runat="server" ClientInstanceName="btnCancelar_SalvarComo" Text="Cancelar" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	popup.HideWindow(winSalvarComo);
}" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
        </Windows>
        <ClientSideEvents Init="function(s, e) {	
        winGerenciarConsultas = popup.GetWindowByName('winGerenciarConsultas');
        winSalvarComo = popup.GetWindowByName('winSalvarComo');
}" />
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <dxcp:ASPxPopupControl ID="pcMenuRelatorios" runat="server" ClientInstanceName="pcMenuRelatorios" CloseAction="CloseButton" CloseAnimationType="Fade" PopupVerticalOffset="75" Width="100%" HeaderText="Selecione o relatório" ShowCloseButton="False" ShowOnPageLoad="True"
        CloseOnEscape="True" ScrollBars="Auto">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <dxtv:ASPxPanel ID="pnlModulos" runat="server" Width="100%">
                    <PanelCollection>
                        <dxtv:PanelContent runat="server">
                            <table class="tabela">
                                <tr>
                                    <td class="coluna">
                                        <figure class="img-modulos">
                                            <img alt="" src="../../imagens/principal/EspacoTrabalho_Menu.png" onclick="SelecionarModulo(this, 'ESP')" />
                                            <figcaption>
                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espaço de trabalho">
                                                </dxtv:ASPxLabel>
                                            </figcaption>
                                        </figure>
                                    </td>
                                    <td class="coluna">
                                        <figure class="img-modulos">
                                            <img alt="" src="../../imagens/principal/Estrategia_Menu.png" onclick="SelecionarModulo(this, 'EST')" />
                                            <figcaption>
                                                <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" Font-Size="10pt" Text="Estratégia">
                                                </dxtv:ASPxLabel>
                                            </figcaption>
                                        </figure>
                                    </td>
                                    <td class="coluna">
                                        <figure class="img-modulos">
                                            <img alt="" src="../../imagens/principal/Projetos_Menu.png" onclick="SelecionarModulo(this, 'PRJ')" />
                                            <figcaption>
                                                <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="True" Font-Size="10pt" Text="Projetos">
                                                </dxtv:ASPxLabel>
                                            </figcaption>
                                        </figure>
                                    </td>
                                    <td class="coluna">
                                        <figure class="img-modulos">
                                            <img alt="" src="../../imagens/principal/Administracao_Menu.png" onclick="SelecionarModulo(this, 'ADM')" />
                                            <figcaption>
                                                <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="True" Font-Size="10pt" Text="Administração">
                                                </dxtv:ASPxLabel>
                                            </figcaption>
                                        </figure>
                                    </td>
                                </tr>
                            </table>
                        </dxtv:PanelContent>
                    </PanelCollection>
                </dxtv:ASPxPanel>
                <dxcp:ASPxCardView ID="cardViewRelatorios" ClientInstanceName="cardViewRelatorios" runat="server" AutoGenerateColumns="False" DataSourceID="sdsListaConsultas" KeyFieldName="CodigoLista" Width="100%" OnCustomCallback="cardViewRelatorios_CustomCallback">
                    <ClientSideEvents Init="OnCardViewInit" CardFocusing="OnCardFocusing" CardClick="OnCardClick" />
                    <Settings LayoutMode="Breakpoints" />
                    <SettingsAdaptivity>
                        <BreakpointsLayoutSettings CardsPerRow="6">
                            <Breakpoints>
                                <dx:CardViewBreakpoint DeviceSize="XLarge" CardsPerRow="5" />
                                <dx:CardViewBreakpoint DeviceSize="Large" CardsPerRow="4" />
                                <dx:CardViewBreakpoint DeviceSize="Medium" CardsPerRow="3" />
                                <dx:CardViewBreakpoint DeviceSize="Small" CardsPerRow="2" />
                                <dx:CardViewBreakpoint DeviceSize="Custom" MaxWidth="600" CardsPerRow="1" />
                            </Breakpoints>
                        </BreakpointsLayoutSettings>
                    </SettingsAdaptivity>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedCard="True" />
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                    <SettingsSearchPanel Visible="True" />
                    <SettingsExport ExportSelectedCardsOnly="False"></SettingsExport>

                    <Templates>
                        <Card>
                            <table cellpadding="20" style="width: 100%; cursor: pointer; min-height: 135px;">
                                <tr>
                                    <td style="text-align: center;">
                                        <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt" Text='<%# string.Format("{0} - {1}", Eval("GrupoMenu"), Eval("TituloLista")) %>'>
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify; text-justify: inter-word;">
                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Size="7pt" Text='<%# Eval("DescricaoLista") %>'>
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr style="height: 20px; width: 100%;">
                                    <td style="width: 100%; text-align: center;">
                                        <div class="btn btn-light rounded-0">
                                            <i class="fas fa-search"></i>Ver detalhes
                                        </div>
                                        <!--
                                        <img src="../../imagens/botoes/verDetalhe.png" style="border: 1px solid black;" />
                                        -->
                                    </td>
                                </tr>
                            </table>
                        </Card>
                    </Templates>

                    <Columns>
                        <dxtv:CardViewTextColumn FieldName="CodigoLista" ReadOnly="True" ShowInCustomizationForm="True" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="NomeLista" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="GrupoMenu" ShowInCustomizationForm="True" VisibleIndex="1">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="ItemMenu" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="GrupoPermissao" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="ItemPermissao" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="IniciaisPermissao" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="TituloLista" ShowInCustomizationForm="True" VisibleIndex="6">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="ComandoSelect" ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="IndicaPaginacao" ShowInCustomizationForm="True" VisibleIndex="8" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="QuantidadeItensPaginacao" ShowInCustomizationForm="True" VisibleIndex="9" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="IndicaOpcaoDisponivel" ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="TipoLista" ShowInCustomizationForm="True" VisibleIndex="11" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="URL" ShowInCustomizationForm="True" VisibleIndex="12" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="CodigoEntidade" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="CodigoModuloMenu" ShowInCustomizationForm="True" VisibleIndex="14" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="OrdemGrupoMenu" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="OrdemItemGrupoMenu" ShowInCustomizationForm="True" VisibleIndex="16" Visible="False">
                        </dxtv:CardViewTextColumn>
                        <dxtv:CardViewTextColumn FieldName="DescricaoLista" ShowInCustomizationForm="True" VisibleIndex="17">
                        </dxtv:CardViewTextColumn>
                    </Columns>
                    <CardLayoutProperties>
                        <Items>
                            <dxtv:CardViewColumnLayoutItem Caption="Grupo Menu" ColSpan="1" ColumnName="GrupoMenu" ShowCaption="False">
                            </dxtv:CardViewColumnLayoutItem>
                            <dxtv:CardViewColumnLayoutItem Caption="Titulo Lista" ColSpan="1" ColumnName="TituloLista" ShowCaption="False">
                            </dxtv:CardViewColumnLayoutItem>
                            <dxtv:CardViewColumnLayoutItem Caption="Descricao Lista" ColSpan="1" ColumnName="DescricaoLista" ShowCaption="False">
                            </dxtv:CardViewColumnLayoutItem>
                            <dxtv:CardViewCommandLayoutItem ColSpan="1">
                            </dxtv:CardViewCommandLayoutItem>
                        </Items>
                    </CardLayoutProperties>
                    <Styles>
                        <Card Height="150px" VerticalAlign="Middle">
                        </Card>
                    </Styles>

                    <StylesExport>
                        <Card BorderSize="1" BorderSides="All"></Card>

                        <Group BorderSize="1" BorderSides="All"></Group>

                        <TabbedGroup BorderSize="1" BorderSides="All"></TabbedGroup>

                        <Tab BorderSize="1"></Tab>
                    </StylesExport>
                </dxcp:ASPxCardView>
                <asp:SqlDataSource ID="sdsListaConsultas" runat="server" SelectCommand=" SELECT [CodigoLista]
       ,[NomeLista]
       ,[GrupoMenu]
       ,[ItemMenu]
       ,[GrupoPermissao]
       ,[ItemPermissao]
       ,[IniciaisPermissao]
       ,[TituloLista]
       ,[ComandoSelect]
       ,[IndicaPaginacao]
       ,[QuantidadeItensPaginacao]
       ,[IndicaOpcaoDisponivel]
       ,[TipoLista]
       ,[URL]
       ,[CodigoEntidade]
       ,[CodigoModuloMenu]
       ,[OrdemGrupoMenu]
       ,[OrdemItemGrupoMenu]
       ,[DescricaoLista]
   FROM [Lista] AS [l]
  WHERE IndicaOpcaoDisponivel = 'S'
    AND l.CodigoEntidade = @CodigoEntidade
    AND (@IndicaProcesso = 1 AND l.TipoLista LIKE ('PROCESSO') OR l.CodigoModuloMenu = @CodigoModuloMenu AND @IndicaProcesso = 0 AND l.TipoLista NOT LIKE ('PROCESSO'))
    AND (dbo.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoEntidade, null, 'EN', 0, null, l.IniciaisPermissao) = 1)
  ORDER BY
        l.GrupoMenu,
        l.TituloLista"
                    ConnectionString="Data Source=10.61.35.3; Initial Catalog=brisk_dev_br; User ID=sacdis; Password=qualiton;">
                    <SelectParameters>
                        <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
                        <asp:SessionParameter Name="CodigoUsuario" SessionField="cu" />
                        <asp:SessionParameter DbType="Boolean" DefaultValue="" Name="IndicaProcesso" SessionField="ip" />
                        <asp:SessionParameter Name="CodigoModuloMenu" SessionField="cmm" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Init="function(s, e) {
    var documentElement = document.documentElement;
    var width = Math.max(0, documentElement.clientWidth);
    var height = Math.max(0, documentElement.clientHeight - 75);
    s.SetSize(width,height);
}"
            CloseUp="function(s, e) {
document.getElementById('menu-hamburguer').checked = false;
EscondeLoadingPanelSeVisibel();
}" />
    </dxcp:ASPxPopupControl>
    <!--
    <script src="../../scripts/jquery.ultima.js" type="text/javascript"></script>
    -->
    <script type="text/javascript">
        var winGerenciarConsultas = null;
        var winSalvarComo = null;

        function ExibeConsultaSalvas(codigoLista, codigoUsuario) {
            var parametro = codigoUsuario + ';' + codigoLista;
            popup.ShowWindow(winGerenciarConsultas);
            popup.PerformWindowCallback(winGerenciarConsultas, parametro);
        }

        function ExibirJanelaSalvarComo() {
            txtNomeConsulta.SetText('');
            popup.ShowWindow(winSalvarComo);
        }

        function SalvarConsultaComo(nomeConsulta) {
            var framePrincipal = window.frames['framePrincipal'];
            if (framePrincipal.SalvarConsultaComo)
                framePrincipal.SalvarConsultaComo(nomeConsulta);
        }

        function CarregarConsulta(codigoListaUsuario) {
            var framePrincipal = window.frames['framePrincipal'];
            if (framePrincipal.CarregarConsulta)
                framePrincipal.CarregarConsulta(codigoListaUsuario);
        }

        if (window.lpAguardeMasterPage)
            window.lpAguardeMasterPage.Hide();
    </script>
</asp:Content>
