<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="StatusReport.aspx.cs" Inherits="_Projetos_Administracao_StatusReport" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="height: 26px; padding-left: 10px;" valign="middle">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text="Modelos de Relatórios de Status"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal">
        <table>
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback" HideContentOnCallback="False">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoModeloStatusReport"
                                    AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                        CustomButtonClick="function(s, e) 
{
	onClick_CustomButtomGrid(s, e);
}
"></ClientSideEvents>

                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilhar" Text="Associar modelo selecionado a projetos">
                                                    <Image Url="~/imagens/compartilhar.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
                                                                <Paddings Padding="0px" />
                                                                <Items>
                                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                        </Items>
                                                                        <Image Url="~/imagens/botoes/btnDownload.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                        <Items>
                                                                            <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                <Image IconID="save_save_16x16">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                <Image IconID="actions_reset_16x16">
                                                                                </Image>
                                                                            </dxm:MenuItem>
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
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoModeloStatusReport" Caption="Descri&#231;&#227;o"
                                            VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoPeriodicidade_PT" Width="150px"
                                            Caption="Periodicidade" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ToleranciaPeriodicidade" Name="ToleranciaPeriodicidade"
                                            Width="120px" Caption="Espera (d)" VisibleIndex="3">
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AllowAutoFilter="False"></Settings>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoModeloStatusReport" Name="CodigoModeloStatusReport"
                                            Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPeriodicidade" Name="CodigoPeriodicidade"
                                            Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasAtrasadas" Name="ListaTarefasAtrasadas"
                                            Visible="False" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasConcluidas" Name="ListaTarefasConcluidas"
                                            Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasFuturas" Name="ListaTarefasFuturas"
                                            Visible="False" VisibleIndex="8">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosConcluidos" Name="ListaMarcosConcluidos"
                                            Visible="False" VisibleIndex="9">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosAtrasados" Name="ListaMarcosAtrasados"
                                            Visible="False" VisibleIndex="10">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosFuturos" Name="ListaMarcosFuturos"
                                            Visible="False" VisibleIndex="11">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoFisico" Name="GraficoDesempenhoFisico"
                                            Visible="False" VisibleIndex="12">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaRH" Name="ListaRH" Visible="False"
                                            VisibleIndex="13">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoCusto" Name="GraficoDesempenhoCusto"
                                            Visible="False" VisibleIndex="14">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaContasCusto" Name="ListaContasCusto"
                                            Visible="False" VisibleIndex="15">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoReceita" Name="GraficoDesempenhoReceita"
                                            Visible="False" VisibleIndex="16">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaContasReceita" Name="ListaContasReceita"
                                            Visible="False" VisibleIndex="17">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="AnaliseValorAgregado" Name="AnaliseValorAgregado"
                                            Visible="False" VisibleIndex="18">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaRiscosAtivos" Name="ListaRiscosAtivos"
                                            Visible="False" VisibleIndex="19">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaRiscosEliminados" Name="ListaRiscosEliminados"
                                            Visible="False" VisibleIndex="20">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaQuestoesAtivas" Name="ListaQuestoesAtivas"
                                            Visible="False" VisibleIndex="21">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaQuestoesResolvidas" Name="ListaQuestoesResolvidas"
                                            Visible="False" VisibleIndex="22">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaMetasResultados" Name="ListaMetasResultados"
                                            Visible="False" VisibleIndex="23">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaPendenciasToDoList" Name="ListaPendenciasToDoList"
                                            Visible="False" VisibleIndex="24">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioGeral" Name="ComentarioGeral"
                                            Visible="False" VisibleIndex="25">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioFisico" Name="ComentarioFisico"
                                            Visible="False" VisibleIndex="26">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioFinanceiro" Name="ComentarioFinanceiro"
                                            Visible="False" VisibleIndex="27">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioRisco" Name="ComentarioRisco"
                                            Visible="False" VisibleIndex="28">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioQuestao" Name="ComentarioQuestao"
                                            Visible="False" VisibleIndex="29">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioMeta" Name="ComentarioMeta" Visible="False"
                                            VisibleIndex="30">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaContratos" Name="ListaContratos" Visible="False"
                                            VisibleIndex="31">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComentarioPlanoAcao" Name="ComentarioPlanoAcao"
                                            Visible="False" VisibleIndex="32">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ListaEntregas" ShowInCustomizationForm="True"
                                            Visible="False" VisibleIndex="33" Name="ListaEntregas">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IniciaisModeloControladoSistema" ShowInCustomizationForm="True"
                                            Visible="False" VisibleIndex="34">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl ID="pcNovoStatusReport" runat="server" ClientInstanceName="pcNovoStatusReport"
                                    PopupElementID="gvDados" PopupHorizontalAlign="LeftSides"
                                    PopupVerticalAlign="TopSides" ShowCloseButton="False" ShowHeader="False" Width="200px"
                                    PopupHorizontalOffset="25" PopupVerticalOffset="20" PopupAction="RightMouseClick">
                                    <ContentStyle>
                                        <Paddings Padding="0px" />
                                    </ContentStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <table width="100%">
                                                <tr id="sr_padrao" class="op" onclick="AbreFormularioNovoSR('padrao');">
                                                    <td style="padding: 5px; cursor: pointer">Modelo Padrão</td>
                                                </tr>
                                                <tr id="sr_padrao_novo" class="op" onclick="AbreFormularioNovoSR('padraonovo');">
                                                    <td style="padding: 5px; cursor: pointer">Modelo Simplificado</td>
                                                </tr>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                    <Border BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl ID="pcCompartilharModelos" runat="server" AllowDragging="True"
                                    CloseAction="CloseButton" EnableViewState="False"
                                    HeaderText="Associação de Modelos de Relatórios a Objetos" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" Width="600px" ClientInstanceName="pcCompartilharModelos">
                                    <ClientSideEvents Closing="function(s, e) {
	gvDados.SetVisible(true);
}"
                                        Shown="function(s, e) {
	gvDados.SetVisible(false);
}" />
                                    <ClientSideEvents Closing="function(s, e) {
	gvDados.SetVisible(true);
}"
                                        Shown="function(s, e) {
	gvDados.SetVisible(false);
}"></ClientSideEvents>
                                    <ContentStyle>
                                        <Paddings Padding="5px" />
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True" />
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="lblNomeRelatorio0" runat="server"
                                                                Text="Nome do Relatório:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtNomeModeloSR" runat="server" BackColor="#EBEBEB"
                                                                MaxLength="30" ReadOnly="True" Width="100%" ClientInstanceName="txtNomeModeloSR">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <dxwgv:ASPxGridView ID="gvAssociacaoModelos" runat="server" AutoGenerateColumns="False"
                                                                ClientInstanceName="gvAssociacaoModelos"
                                                                KeyFieldName="CodigoObjeto" OnCustomCallback="gvAssociacaoModelos_CustomCallback"
                                                                OnRowDeleting="gvAssociacaoModelos_RowDeleting" Width="100%">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	gvAssociacaoModelos_CustomButtonClick(s,e);
}"></ClientSideEvents>
                                                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                                                                </SettingsEditing>
                                                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                                                <SettingsText ConfirmDelete="Deseja excluir a associa&#231;&#227;o ao objeto?" PopupEditFormCaption="Novo Objeto"></SettingsText>
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	gvAssociacaoModelos_CustomButtonClick(s,e);
}" />
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                <Settings VerticalScrollBarMode="Visible" />
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />

                                                                <SettingsCommandButton>
                                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                                </SettingsCommandButton>

                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" Width="400px" />
                                                                </SettingsPopup>
                                                                <SettingsText ConfirmDelete="Deseja excluir a associação ao objeto?" PopupEditFormCaption="Novo Objeto" />
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="True"
                                                                        VisibleIndex="0" Width="40px">
                                                                        <CustomButtons>
                                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirAssociacaoModelo" Text="Excluir">
                                                                                <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                </Image>
                                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                        <HeaderTemplate>
                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <img id="btnIncluir" alt="Incluir" src="../../imagens/botoes/incluirReg02.png" style="cursor: pointer;"
                                                                                            onclick="pcNovoObjeto.Show();" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoModeloStatusReport" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="1">
                                                                        <EditFormSettings Visible="False" />
                                                                        <EditFormSettings Visible="False"></EditFormSettings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoObjeto" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="2">
                                                                        <PropertiesComboBox TextField="NomeObjeto" ValueField="CodigoObjeto" ValueType="System.String">
                                                                            <Columns>
                                                                                <dxe:ListBoxColumn Caption="Objeto" FieldName="NomeObjeto" />
                                                                            </Columns>
                                                                        </PropertiesComboBox>
                                                                        <EditFormSettings Caption="Objeto:" Visible="True" VisibleIndex="1" />
                                                                        <EditFormSettings Visible="True" VisibleIndex="1" Caption="Objeto:"></EditFormSettings>
                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Tipo" FieldName="CodigoTipoAssociacaoObjeto"
                                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                        <PropertiesComboBox ValueType="System.String">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Projeto" Value="PR" />
                                                                                <dxe:ListEditItem Text="Unidade de Negócio" Value="UN" />
                                                                                <dxe:ListEditItem Text="Entidade" Value="EN" />
                                                                                <dxe:ListEditItem Text="Carteira de Projeto" Value="CP" />
                                                                            </Items>
                                                                        </PropertiesComboBox>
                                                                        <EditFormSettings Caption="Tipo:" Visible="True" VisibleIndex="0" />
                                                                        <EditFormSettings Visible="True" VisibleIndex="0" Caption="Tipo:"></EditFormSettings>
                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Objeto" FieldName="DescricaoObjeto" ShowInCustomizationForm="True"
                                                                        SortIndex="0" SortOrder="Ascending" VisibleIndex="4">
                                                                        <CellStyle HorizontalAlign="Left">
                                                                        </CellStyle>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <dxe:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                Text="Fechar" Width="100px">
                                                                <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    pcCompartilharModelos.Hide();
                }" />
                                                                <Paddings Padding="0px" />
                                                                <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    pcCompartilharModelos.Hide();
                }"></ClientSideEvents>
                                                                <Paddings Padding="0px"></Paddings>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxpc:ASPxPopupControl ID="pcNovoObjeto" runat="server" ClientInstanceName="pcNovoObjeto"
                                                PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below"
                                                ShowCloseButton="False" ShowHeader="False" PopupElementID="txtNomeModeloSR" Width="120px"
                                                PopupVerticalOffset="40" PopupAction="None">
                                                <ContentStyle>
                                                    <Paddings Padding="0px" />
                                                    <Paddings Padding="0px"></Paddings>
                                                </ContentStyle>
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl runat="server">
                                                        <table width="100%">
                                                            <tr id="pr" class="op" onclick="mostrarAssociacaoModelosSR('pr', 'Projeto');">
                                                                <td style="padding: 5px; cursor: pointer">
                                                                    <dxe:ASPxLabel ID="lblProjeto" runat="server" Text="Projeto"
                                                                        ClientInstanceName="lblProjeto">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr id="en" class="op" onclick="mostrarAssociacaoModelosSR('en', 'Entidade');">
                                                                <td style="padding: 5px; cursor: pointer">
                                                                    <dxe:ASPxLabel ID="lblEntidade" runat="server" Text="Entidade"
                                                                        ClientInstanceName="lblEntidade">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr id="un" class="op" onclick="mostrarAssociacaoModelosSR('un', 'Unidade');">
                                                                <td style="padding: 5px; cursor: pointer">
                                                                    <dxe:ASPxLabel ID="lblUnidade" runat="server" Text="Unidade"
                                                                        ClientInstanceName="lblUnidade">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr id="cp" class="op" onclick="mostrarAssociacaoModelosSR('cp', lblCarteira.GetText());">
                                                                <td style="padding: 5px; cursor: pointer">
                                                                    <dxe:ASPxLabel ID="lblCarteira" runat="server" Text="Carteira"
                                                                        ClientInstanceName="lblCarteira">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dxpc:PopupControlContentControl>
                                                </ContentCollection>
                                                <Border BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" />
                                                <Border BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dxpc:ASPxPopupControl>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcAssociacao"
                                    CloseAction="None" HeaderText="Associação de Modelos de Relatórios" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="970px"
                                    ID="pcAssociacao">
                                    <ClientSideEvents CloseUp="function(s, e) {
	gvAssociacaoModelos.PerformCallback();
}" />
                                    <ClientSideEvents CloseUp="function(s, e) {
	gvAssociacaoModelos.PerformCallback();
}"></ClientSideEvents>
                                    <ContentStyle>
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <SettingsLoadingPanel ShowImage="true" />
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <dxe:ASPxLabel runat="server" Text="Nome do Relat&#243;rio:"
                                                                            ID="lblNomeRelatorio">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 50%">
                                                                        <dxe:ASPxLabel runat="server" Text="Tipo do Objeto:"
                                                                            ID="lblTipoObjeto">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtNomeModelo_Associacao"
                                                                            BackColor="#EBEBEB" ID="txtNomeModelo_Associacao">
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 50%">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtTipoObjeto"
                                                                            BackColor="#EBEBEB" ID="txtTipoObjeto">
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Objetos Disponíveis:"
                                                                                ID="ASPxLabel106">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 70px"></td>
                                                                        <td align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Objetos Selecionados:"
                                                                                ID="ASPxLabel107">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="10" SelectionMode="CheckColumn" SelectAllText="Selecionar Tudo"
                                                                                ToolTip="Digite Control  + clique com o botão esquerdo do mouse para selecionar vários ítens..."
                                                                                ClientInstanceName="lbItensDisponiveis"
                                                                                EnableClientSideAPI="True" Width="440px" Height="260px"
                                                                                ID="lbItensDisponiveis" OnCallback="lbItensDisponiveis_Callback">

                                                                                <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" EditorNullTextDisplayMode="Unfocused" UseCompactView="False" />

                                                                                <ItemStyle Wrap="True"></ItemStyle>

                                                                                <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}"
                                                                                    SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>

                                                                                <DisabledStyle ForeColor="Black">
                                                                                </DisabledStyle>

                                                                            </dxe:ASPxListBox>
                                                                        </td>
                                                                        <td style="width: 70px" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll"
                                                                                                ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                Font-Bold="True" ToolTip="Selecionar todas as unidades"
                                                                                                ID="btnAddAll">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel"
                                                                                                ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                Font-Bold="True" ToolTip="Selecionar as unidades marcadas"
                                                                                                ID="btnAddSel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel"
                                                                                                ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                Font-Bold="True" ToolTip="Retirar da sele&#231;&#227;o as unidades marcadas"
                                                                                                ID="btnRemoveSel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll"
                                                                                                ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="60px" Height="25px"
                                                                                                Font-Bold="True" ToolTip="Retirar da sele&#231;&#227;o todas as unidades"
                                                                                                ID="btnRemoveAll">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="10" SelectionMode="CheckColumn"
                                                                                ClientInstanceName="lbItensSelecionados" SelectAllText="Selecionar Tudo"
                                                                                ToolTip="Digite Control  + clique com o botão esquerdo do mouse para selecionar vários ítens"
                                                                                EnableClientSideAPI="True" Width="440px" Height="260px"
                                                                                ID="lbItensSelecionados" OnCallback="lbItensSelecionados_Callback">

                                                                                <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" EditorNullTextDisplayMode="Unfocused" UseCompactView="False" />

                                                                                <ItemStyle Wrap="True"></ItemStyle>

                                                                                <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
}"
                                                                                    SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                                <DisabledStyle ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxListBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfObjetos" ID="hfObjetos">
                                                                <ClientSideEvents EndCallback="function(s, e) {
	hfProjetos_onEndCallback();
}"></ClientSideEvents>
                                                            </dxhf:ASPxHiddenField>
                                                            <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr style="height: 35px">
                                                                        <td style="padding: 5px;">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvarCompartilhar"
                                                                                CausesValidation="False" Text="Salvar" Width="100px"
                                                                                ID="btnSalvarCompartilhar">
                                                                                <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	onClick_btnSalvarCompartilhar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>

                                                                        <td align="right">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                Text="Fechar" Width="100px" ID="ASPxButton2">
                                                                                <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    pcAssociacao.Hide();
                }"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackMensagem" Width="640px"
                                    ID="pnCallbackMensagem" OnCallback="pnCallbackMensagem_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent2" runat="server">
                                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" CloseAction="None"
                                                EnableClientSideAPI="True" HeaderText="Mensagem" Modal="True" PopupAction="MouseOver"
                                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                                ShowShadow="False" Width="630px" ID="pcMensagemGravacao">
                                                <HeaderImage Url="~/imagens/alertAmarelho.png">
                                                </HeaderImage>
                                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Aten&#231;&#227;o:" ClientInstanceName="lblAtencao"
                                                                            Font-Bold="True" ID="lblAtencao">
                                                                        </dxe:ASPxLabel>
                                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblGravacao2"
                                                                            ID="lblGravacao2">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxp:ASPxPanel runat="server" EnableClientSideAPI="True" ClientInstanceName="pnlImpedimentos"
                                                                            Width="100%" ID="pnlImpedimentos">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent ID="PanelContent3" runat="server">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvImpedimentos" AutoGenerateColumns="False"
                                                                                                        Width="100%" ID="gvImpedimentos">

                                                                                                        <SettingsCommandButton>
                                                                                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                                                                        </SettingsCommandButton>
                                                                                                        <Columns>
                                                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Width="40%" Caption="Projeto"
                                                                                                                VisibleIndex="0">
                                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                                            <dxwgv:GridViewDataTextColumn FieldName="Impedimento" Caption="Impedimento" VisibleIndex="1">
                                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                                        </Columns>
                                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                                        </SettingsPager>
                                                                                                    </dxwgv:ASPxGridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 10px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="lblAlertaFechar"
                                                                                                        CausesValidation="False" Text="Fechar" Width="100px"
                                                                                                        ID="lblAlertaFechar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	pcMensagemGravacao.Hide();
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxp:ASPxPanel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxpc:PopupControlContentControl>
                                                </ContentCollection>
                                            </dxpc:ASPxPopupControl>
                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                                <Styles>
                                                    <Default>
                                                    </Default>
                                                    <Header>
                                                    </Header>
                                                    <Cell>
                                                    </Cell>
                                                    <GroupFooter Font-Bold="True">
                                                    </GroupFooter>
                                                    <Title Font-Bold="True"></Title>
                                                </Styles>
                                            </dxwgv:ASPxGridViewExporter>
                                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcOperMsg" HeaderText="Incluir a Entidade Atual"
                                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                ShowCloseButton="False" ShowHeader="False" Width="270px"
                                                ID="pcOperMsg">
                                                <ContentCollection>
                                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="" align="center"></td>
                                                                    <td style="width: 70px" align="center" rowspan="3">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                            ID="lblAcaoGravacao">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxpc:PopupControlContentControl>
                                                </ContentCollection>
                                            </dxpc:ASPxPopupControl>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px"
                                    ID="pcDados">
                                    <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();
}"></ClientSideEvents>
                                    <ContentStyle>
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr style="height: 50px" valign="top">
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_nome_do_relat_rio %>" />
                                                                                            :
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td style="width: 80px">
                                                                                            <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_espera__d_ %>" />
                                                                                            :
                                                                                        </td>
                                                                                        <td style="width: 20px"></td>
                                                                                        <td></td>
                                                                                        <td style="width: 100px">
                                                                                            <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_periodicidade %>" />:
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxTextBox runat="server" Width="95%" MaxLength="60" ClientInstanceName="txtNomeNovoModeloRelatorio"
                                                                                                ID="txtNomeNovoModeloRelatorio">
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td style="width: 80px;">
                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="3" ClientInstanceName="txtEspera"
                                                                                                ID="txtEspera">
                                                                                                <MaskSettings Mask="&lt;0..359&gt;" IncludeLiterals="None"></MaskSettings>
                                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                                </ValidationSettings>
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                        <td style="width: 20px; padding: 0px 10px;" valign="top" align="center">
                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="Quantidade de dias a aguardar para a gera&#231;&#227;o do relat&#243;rio ap&#243;s o fim do per&#237;odo"
                                                                                                Width="18px" Height="18px" ClientInstanceName="imgAjuda" Cursor="pointer" ID="imgAjuda">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td>
                                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlPeriodicidade"
                                                                                                ID="ddlPeriodicidade">
                                                                                            </dxe:ASPxComboBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="CheckBox">
                                                                        <td>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_f_sico %>" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Tarefas Atrasadas" ClientInstanceName="ceTarefasAtrasadas"
                                                                                                                            ID="ceTarefasAtrasadas">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Marcos Atrasados" ClientInstanceName="ceMarcosAtrasados"
                                                                                                                            ID="ceMarcosAtrasados">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Comentar F&#237;sico" ClientInstanceName="ceComentarFisico"
                                                                                                                            ID="ceComentarFisico">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Tarefas Conclu&#237;das no Per&#237;odo" ClientInstanceName="ceTarefasConcluidasPeriodo"
                                                                                                                            ID="ceTarefasConcluidasPeriodo">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Marcos Conclu&#237;dos" ClientInstanceName="ceMarcosConcluidos"
                                                                                                                            ID="ceMarcosConcluidos">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Tarefas para Pr&#243;ximo Per&#237;odo" ClientInstanceName="ceTarefasProximoPeriodo"
                                                                                                                            ID="ceTarefasProximoPeriodo">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Marcos para o Pr&#243;ximo Per&#237;odo" ClientInstanceName="ceMarcosProximoPeriodo"
                                                                                                                            ID="ceMarcosProximoPeriodo">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Lista de Desempenho dos Recursos" ClientInstanceName="ceListaDesempenhoRecursos"
                                                                                                                            ID="ceListaDesempenhoRecursos" ClientVisible="False">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox ID="ceListaEntregas" runat="server" CheckState="Unchecked" ClientInstanceName="ceListaEntregas"
                                                                                                                            ClientVisible="False" Text="Entregas">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_riscos %>" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Riscos Ativos" ClientInstanceName="ceRiscosAtivos"
                                                                                                                            ID="ceRiscosAtivos">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Riscos Eliminados" ClientInstanceName="ceRiscosEliminados"
                                                                                                                            ID="ceRiscosEliminados">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Comentar Riscos" ClientInstanceName="ceComentarRiscos"
                                                                                                                            ID="ceComentarRiscos">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <b>
                                                                                            <dxe:ASPxLabel runat="server" Text="Quest&#245;es"
                                                                                                ID="lblQuestoes">
                                                                                            </dxe:ASPxLabel>
                                                                                        </b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Quest&#245;es Ativas" ClientInstanceName="ceQuestoesAtivas"
                                                                                                                            ID="ceQuestoesAtivas">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Quest&#245;es Resolvidas" ClientInstanceName="ceQuestoesResolvidas"
                                                                                                                            ID="ceQuestoesResolvidas">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Comentar Quest&#245;es" ClientInstanceName="ceComentarQuestoes"
                                                                                                                            ID="ceComentarQuestoes">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblFinanceiro" runat="server" ClientInstanceName="lblFinanceiro"
                                                                                            Text="Financeiro">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Detalhes da Despesa" ClientInstanceName="ceDetalhesCusto"
                                                                                                                            ID="ceDetalhesCusto">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Detalhes da Receita" ClientInstanceName="ceDetalhesReceita"
                                                                                                                            ID="ceDetalhesReceita">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Comentar Financeiro" ClientInstanceName="ceComentarFinanceiro"
                                                                                                                            ID="ceComentarFinanceiro">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_metas %>" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Metas e Resultados" ClientInstanceName="ceMetasResultados"
                                                                                                                            ID="ceMetasResultados">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%"></td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Comentar Metas e Resultados" ClientInstanceName="ceComentarMetasResultados"
                                                                                                                            ID="ceComentarMetasResultados">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, StatusReport_outros_t_picos %>" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="border: 1px solid #C0C0C0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox ID="ceContratos" runat="server" CheckState="Unchecked" ClientInstanceName="ceContratos"
                                                                                                                            Text="Contratos">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox ID="ceAnaliseValorAgregado" runat="server" CheckState="Unchecked"
                                                                                                                            ClientInstanceName="ceAnaliseValorAgregado"
                                                                                                                            Text="Análise do Valor Agregado">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 33%">
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Análise Crítica" ClientInstanceName="ceComentarioGeral"
                                                                                                                            ID="ceComentarioGeral">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxCheckBox ID="ceInformacoesCusto" runat="server" CheckState="Unchecked" ClientInstanceName="ceInformacoesCusto" Text="Informações de Custo">
                                                                                                                        </dxtv:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox ID="ceListaPendencias" runat="server" CheckState="Unchecked" ClientInstanceName="ceListaPendencias"
                                                                                                                            Text="Lista de Pendências">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxCheckBox runat="server" Text="Plano de Ação" ClientInstanceName="cePlanoAcaoGeral"
                                                                                                                            ID="cePlanoAcaoGeral">
                                                                                                                        </dxe:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 90px; height: 37px">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%"
                                                                                ID="btnSalvar">
                                                                                <ClientSideEvents Click="function(s, e) {	
	            e.processOnServer = false;
                if (window.onClick_btnSalvar)
	                onClick_btnSalvar();
            }"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 10px; height: 37px" align="right"></td>
                                                                        <td style="width: 90px; height: 37px" align="right">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%"
                                                                                ID="btnFechar">
                                                                                <ClientSideEvents Click="function(s, e) {
	            e.processOnServer = false;
                if (window.onClick_btnCancelar)
                   onClick_btnCancelar();
            }"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <!-- ASPxPOPUPCONTROL : Dados salvos -->
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="popupNovoDado" HeaderText="Novo Dado"
                                    Modal="True" PopupElementID="popupDado" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                    ShowCloseButton="False" Width="600px" Height="124px"
                                    ID="popupNovoDado">
                                    <ClientSideEvents Shown="function(s, e) {
        txtNome.SetText(&quot;&quot;);
    }"></ClientSideEvents>
                                    <ContentStyle>
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 127px">
                                                                            <dxe:ASPxLabel runat="server" Text="C&#243;digo:" ClientInstanceName="lblNome"
                                                                                ID="ASPxLabel1">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Nome do Dado:" ClientInstanceName="lblNome"
                                                                                ID="lblNome">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 127px">
                                                                            <dxe:ASPxTextBox runat="server" Width="116px" ClientInstanceName="txtCodigoDado"
                                                                                ID="txtCodigoDado">
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNome"
                                                                                ID="txtNome">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="PPR">
                                                                                    <ErrorImage Height="14px">
                                                                                    </ErrorImage>
                                                                                    <RequiredField IsRequired="True" ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <DisabledStyle ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPP"
                                                                                Text="Salvar" ValidationGroup="PPR" Width="100px"
                                                                                ID="btnSalvarNovoDado">
                                                                                <ClientSideEvents Click="function(s, e) {
                    if(txtNome.GetValue() != &quot;&quot;) 
                    {	
                        insereNovoDado();
                        popupNovoDado.Hide();
                     }
                }"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td></td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelaPP"
                                                                                Text="Fechar" Width="100px" ID="btnCancelaNovoDado">
                                                                                <ClientSideEvents Click="function(s, e) { popupNovoDado.Hide(); }"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>

        <asp:SqlDataSource ID="dsDado" runat="server" SelectCommand="SELECT d.[CodigoDado], d.[DescricaoDado] FROM [DadoOperacional] d&#13;&#10;WHERE d.CodigoDado NOT IN (select CodigoDado from f_GetDadosIndicadorOperacional(@CodigoIndicador)) ORDER BY [DescricaoDado]">
            <SelectParameters>
                <asp:Parameter Name="CodigoUnidade" />
                <asp:Parameter Name="CodigoIndicador" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsDadosIndicador" runat="server" SelectCommand="select * from f_GetDadosIndicadorOperacional(@CodigoIndicador)">
            <SelectParameters>
                <asp:Parameter Name="CodigoIndicador" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsFuncao" runat="server" SelectCommand="SELECT CodigoFuncao, NomeFuncao FROM TipoFuncaoDado UNION SELECT 0 AS Expr1, ' ' AS Expr2 ORDER BY NomeFuncao"></asp:SqlDataSource>
    </div>
</asp:Content>
