<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="propostas.aspx.cs" Inherits="espacoTrabalho_propostas" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Cadastro de riscos de projetos"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/Navegacao/btnBordaEsquerda.PNG">
                            </dxe:ASPxImage>
                        </td>
                        <td id="primeiro" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgPrimeiro" runat="server" ImageUrl="~/imagens/Navegacao/btnPrimeiro.PNG"
                                ToolTip="Primeiro" ClientInstanceName="imgPrimeiro">
                                <ClientSideEvents Click="function(s, e) {
	mudaLinhaSelecionada(grid, 0, false);
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="anterior" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgAnterior" runat="server" ImageUrl="~/imagens/Navegacao/btnAnterior.PNG"
                                ToolTip="Anterior" ClientInstanceName="imgAnterior">
                                <ClientSideEvents Click="function(s, e) {
	mudaLinhaSelecionada(grid, grid.GetFocusedRowIndex() - 1, false);
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="proximo" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgProximo" runat="server" ImageUrl="~/imagens/Navegacao/btnProximo.PNG"
                                ToolTip="Próximo" ClientInstanceName="imgProximo">
                                <ClientSideEvents Click="function(s, e) {
	mudaLinhaSelecionada(grid, grid.GetFocusedRowIndex() + 1, false);
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="ultimo" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgUltimo" runat="server" ImageUrl="~/imagens/Navegacao/btnUltimo.PNG"
                                ToolTip="Último" ClientInstanceName="imgUltimo">
                                <ClientSideEvents Click="function(s, e) {
	mudaLinhaSelecionada(grid, grid.visibleStartIndex + grid.GetVisibleRowsOnPage() - 1, false);
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="novo" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgNovo" runat="server" ImageUrl="~/imagens/Navegacao/btnInserir.PNG"
                                ToolTip="Inserir" ClientInstanceName="imgNovo">
                                <ClientSideEvents Click="function(s, e) {
	habilitaEdicao(tabControl, 'NOVO', false); painelCallback.PerformCallback('inserir');
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="editar" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgEdicao" runat="server" ImageUrl="~/imagens/Navegacao/btnEditar.PNG"
                                ToolTip="Editar" ClientInstanceName="imgEdicao">
                                <ClientSideEvents Click="function(s, e) {
	habilitaEdicao(tabControl, 'EDICAO', false); painelCallback.PerformCallback('editar');
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="excluir" align="center" style="cursor: pointer">
                            <dxe:ASPxImage ID="imgExclusao" runat="server" ImageUrl="~/imagens/Navegacao/btnExcluir.PNG"
                                ToolTip="Excluir" ClientInstanceName="imgExclusao">
                                <ClientSideEvents Click="function(s, e) {
	confirm('Deseja realmente excluir o registro?')== true ? painelCallBackGrid.PerformCallback('Excluir'):false
}" />
                            </dxe:ASPxImage>
                        </td>
                        <td id="tbBotoesEdicao" align="center" style="cursor: pointer">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxImage ID="imgSalvar" runat="server" ImageUrl="~/imagens/Navegacao/btnSalvar.PNG"
                                            ToolTip="Salvar" ClientInstanceName="imgSalvar">
                                            <ClientSideEvents Click="function(s, e) {
	painelCallBackGrid.PerformCallback('Salvar');
	desabilitaEdicao(tabControl);
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="imgCancelar" runat="server" ImageUrl="~/imagens/Navegacao/btnCancelar.PNG"
                                            ToolTip="Cancelar" ClientInstanceName="imgCancelar">
                                            <ClientSideEvents Click="function(s, e) {
	painelCallback.PerformCallback('desabilitar');	
	desabilitaEdicao(tabControl);
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dxe:ASPxImage ID="ASPxImage11" runat="server" ImageUrl="~/imagens/Navegacao/btnBordaDireita.PNG">
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxtc:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="1" Width="98%"
                    CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass"
                    ImageFolder="~/App_Themes/Glass/{0}/" TabSpacing="0px" ClientInstanceName="tabControl">
                    <TabPages>
                        <dxtc:TabPage Name="TabC" Text="Consulta">
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <dxcp:ASPxCallbackPanel ID="painelCallBackGrid" runat="server" ClientInstanceName="painelCallBackGrid"
                                        OnCallback="painelCallBackGrid_Callback">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxwgv:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                                                    Width="100%"  KeyFieldName="CodigoRiscoPadrao">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="T&#237;tulo da Proposta" FieldName="NomeProjeto"
                                                            VisibleIndex="0">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" FieldName="DescricaoProposta"
                                                            Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	
}" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <Styles>
                                                        <FocusedRow BackColor="LightGray" ForeColor="Black">
                                                        </FocusedRow>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="Edi&#231;&#227;o" Name="TabE">
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <dxcp:ASPxCallbackPanel ID="painelCallback" runat="server" ClientInstanceName="painelCallback"
                                        OnCallback="painelCallback_Callback">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                Text="T&#237;tulo da Proposta:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTitulo" runat="server" MaxLength="250" Width="99%"
                                                                ClientInstanceName="txtTitulo">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                Text="Descri&#231;&#227;o:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao"
                                                                Rows="4" Width="99%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxwgv:ASPxGridView ID="gridObjetivos" runat="server" AutoGenerateColumns="False"
                                                                ClientInstanceName="gridObjetivos"  KeyFieldName="CodigoObjetoEstrategia"
                                                                OnCancelRowEditing="gridObjetivos_CancelRowEditing" OnRowDeleting="gridObjetivos_RowDeleting"
                                                                OnRowInserting="gridObjetivos_RowInserting" OnRowUpdating="gridObjetivos_RowUpdating"
                                                                Width="99%">
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#245;es" Name="Descricao"
                                                                        ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0" Width="50px">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle VerticalAlign="Middle">
                                                                        </CellStyle>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Objetivo Estrat&#233;gico" VisibleIndex="1">
                                                                        <PropertiesTextEdit ClientInstanceName="Descricao" MaxLength="250" Width="600px">
                                                                            <ClientSideEvents Init="function(s, e) {
	s.SetFocus();
}" KeyPress="function(s, e) {
	if(e.htmlEvent.keyCode == 13)
	{
		aspxGVUpdateEdit('ASPxPageControl1_gridAmeacas');
	}	
}" />
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="Valor Invalido" ErrorTextPosition="Bottom"
                                                                                SetFocusOnError="True">
                                                                                <RequiredField ErrorText="Campo Obrigat&#243;rio" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </PropertiesTextEdit>
                                                                        <EditFormSettings Caption="Objetivo Estrat&#233;gico:" CaptionLocation="Top" />
                                                                        <EditCellStyle >
                                                                        </EditCellStyle>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <CellStyle Wrap="True">
                                                                        </CellStyle>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataCheckColumn Caption="Principal" VisibleIndex="2" Width="60px">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                </Columns>
                                                                <SettingsBehavior ConfirmDelete="True" />
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                    <AllButton Text="All">
                                                                        <Image Height="19px" Width="27px" />
                                                                    </AllButton>
                                                                    <FirstPageButton>
                                                                        <Image Height="19px" Width="23px" />
                                                                    </FirstPageButton>
                                                                    <LastPageButton>
                                                                        <Image Height="19px" Width="23px" />
                                                                    </LastPageButton>
                                                                    <NextPageButton>
                                                                        <Image Height="19px" Width="19px" />
                                                                    </NextPageButton>
                                                                    <PrevPageButton>
                                                                        <Image Height="19px" Width="19px" />
                                                                    </PrevPageButton>
                                                                    <Summary Text="{0} de {1}" />
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm" />
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" />
                                                                </SettingsPopup>
                                                                <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="135" />
                                                                <SettingsText ConfirmDelete="Confirma exclus&#227;o?" PopupEditFormCaption="Cadastro de Objetivos Estrat&#233;gicos" />
                                                                <SettingsLoadingPanel Text="" />
                                                                <Images>
                                                                    <CollapsedButton Height="15px" Width="15px" />
                                                                    <ExpandedButton Height="15px" Width="15px" />
                                                                    <DetailCollapsedButton Height="15px" Width="15px" />
                                                                    <DetailExpandedButton Height="15px" Width="15px" />
                                                                    <HeaderFilter Height="19px" Width="19px" />
                                                                    <HeaderActiveFilter Height="19px" Width="19px" />
                                                                    <HeaderSortDown Height="5px" Width="7px" />
                                                                    <HeaderSortUp Height="5px" Width="7px" />
                                                                    <FilterRowButton Height="13px" Width="13px" />
                                                                    <WindowResizer Height="13px" Width="13px" />
                                                                </Images>
                                                                <ImagesEditors>
                                                                    <CalendarFastNavPrevYear Height="19px" Width="19px" />
                                                                    <CalendarFastNavNextYear Height="19px" Width="19px" />
                                                                    <DropDownEditDropDown Height="7px" Width="9px" />
                                                                    <SpinEditIncrement Height="6px" Width="7px" />
                                                                    <SpinEditDecrement Height="7px" Width="7px" />
                                                                    <SpinEditLargeIncrement Height="9px" Width="7px" />
                                                                    <SpinEditLargeDecrement Height="9px" Width="7px" />
                                                                </ImagesEditors>
                                                                <Styles>
                                                                    <SelectedRow BackColor="#F7F7F7">
                                                                    </SelectedRow>
                                                                    <FocusedRow BackColor="LightGray" ForeColor="Black">
                                                                    </FocusedRow>
                                                                    <EditFormDisplayRow >
                                                                    </EditFormDisplayRow>
                                                                    <EditForm >
                                                                    </EditForm>
                                                                    <TitlePanel BackColor="White" Font-Bold="True" 
                                                                        ForeColor="Black">
                                                                    </TitlePanel>
                                                                </Styles>
                                                                <StylesPopup>
                                                                <EditForm>
                                                                <MainArea Wrap="True"></MainArea>
                                                                </EditForm>
                                                                </StylesPopup>
                                                                <StylesEditors>
                                                                    <ProgressBar Height="25px">
                                                                    </ProgressBar>
                                                                </StylesEditors>
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ContentStyle>
                        <Border BorderColor="#4986A2" />
                    </ContentStyle>
                    <Paddings PaddingLeft="0px" />
                </dxtc:ASPxPageControl>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
