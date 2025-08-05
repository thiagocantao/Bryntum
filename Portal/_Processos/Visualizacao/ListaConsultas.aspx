<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaConsultas.aspx.cs" Inherits="_Processos_Visualizacao_ListaConsultas" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 5px;">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Gestão" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width:100%">
        <tr>
            <td style="padding-left: 5px">
                <dxwgv:ASPxGridView ID="gvDados" ClientInstanceName="gvDados" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsLista" KeyFieldName="CodigoLista" OnInitNewRow="gvDados_InitNewRow"
                    Width="100%" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                    OnDataBound="gvDados_DataBound" OnDetailRowGetButtonVisibility="gvDados_DetailRowGetButtonVisibility"
                    OnRowUpdating="gvDados_RowUpdating" OnRowValidating="gvDados_RowValidating" OnCustomErrorText="gvDados_CustomErrorText">
                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
	if(e.buttonID == &quot;btnAtualizarCampos&quot;)
                {
		e.processOnServer = false;
                                 if(confirm('Deseja atualizar os campos?'))
                                 {
		       loadingPanel.Show();	
                                      callbackAtualizaCampos.PerformCallback(s.GetRowKey(e.visibleIndex));
		}
	}
	if(e.buttonID == &quot;btnPopupFilter&quot;)
                {
		e.processOnServer = false;
	                  s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoLista;TipoLista', function(valores) 
                                      {
                        
                                              var CodigoLista = valores[0];
                                               var TipoLista =  valores[1];
                                               var sUrl = window.parent.pcModalComFooter.cp_Path + '_Processos/Visualizacao/FilterEditorPopup.aspx?cl=' + CodigoLista;
                    sUrl += '&amp;ctx=cfg';
                    window.parent.showModalComFooter(sUrl , traducao.ListaConsultas_filtro_dos_dados, 300, 400, '', null);
                });	
		
	}

}"
                        DetailRowCollapsing="function(s, e) {
	s.SetFocusedRowIndex(e.visibleIndex);
}"
                        DetailRowExpanding="function(s, e) {
	s.SetFocusedRowIndex(e.visibleIndex);
}" BeginCallback="function(s, e) {
	//alert(e.command);
comando = e.command;

}" EndCallback="function(s, e) {
       if(comando == 'UPDATEEDIT')
       {
                 if(s.cpErro != '')
                 {
                            window.parent.mostraMensagem(s.cpErro, 'erro', true, false, null);
                  }
                  else
                 {
                              if(s.cpSucesso != '')
                              {
                           
                                           window.parent.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000);
                               }
                 }
       }
}" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="120px" ShowEditButton="true" ShowDeleteButton="true" FixedStyle="Left">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnAtualizarCampos" Text="Atualizar campos">
                                    <Image AlternateText="Atualizar campos" Height="20px" ToolTip="Atualizar campos"
                                        Url="~/imagens/atualizar.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxtv:GridViewCommandColumnCustomButton ID="btnPopupFilter" Image-ToolTip="Filtro Master">
                                    <Image Url="~/imagens/filtroOrcamentacaoProjeto.png">
                                    </Image>
                                </dxtv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderTemplate>
                                <img alt="Novo Registro" id="btnIncluir" src="../../imagens/botoes/incluirReg02.png" runat="server"
                                    onclick="window.parent.showModal('WizardDefinicaoLista.aspx', '', screen.width - 30, window.parent.innerHeight - 180, executarAposWizardLista);" title="<%$ Resources:traducao, novo_registro %>" style="cursor: pointer" />
                            </HeaderTemplate>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoLista" ReadOnly="True" ShowInCustomizationForm="False"
                            Visible="False" VisibleIndex="1">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeLista" VisibleIndex="2" Width="410px">
                            <PropertiesTextEdit ClientInstanceName="txtNomeLista" MaxLength="255">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="0" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Grupo de menu" FieldName="GrupoMenu" Visible="False"
                            VisibleIndex="3">
                            <PropertiesTextEdit ClientInstanceName="txtGrupoMenu" MaxLength="50">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="6" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Item de menu" FieldName="ItemMenu" Visible="False"
                            VisibleIndex="4">
                            <PropertiesTextEdit ClientInstanceName="txtItemMenu" MaxLength="60">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="7" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Grupo de permissão" FieldName="GrupoPermissao"
                            Visible="False" VisibleIndex="5">
                            <PropertiesTextEdit ClientInstanceName="txtGrupoPermissao" MaxLength="50">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="8" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Item de permissão" FieldName="ItemPermissao"
                            Visible="False" VisibleIndex="6">
                            <PropertiesTextEdit ClientInstanceName="txtItemPermissao" MaxLength="60">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="9" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Iniciais de permissão" FieldName="IniciaisPermissao"
                            Visible="False" VisibleIndex="7">
                            <PropertiesTextEdit ClientInstanceName="txtIniciaisPermissao" MaxLength="10">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="8" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Título" FieldName="TituloLista" VisibleIndex="8" Width="250px">
                            <PropertiesTextEdit ClientInstanceName="txtTitulo" MaxLength="150">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="1" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn Caption="Comando SELECT" FieldName="ComandoSelect"
                            Visible="False" VisibleIndex="9" CellRowSpan="4">
                            <PropertiesMemoEdit ClientInstanceName="memoComandoSelect" Rows="5" Style-CssClass="resizable" Height="300px">
                                <Style>
                                    
                                </Style>
                            </PropertiesMemoEdit>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="4" Visible="True" VisibleIndex="15" RowSpan="4" />
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewDataCheckColumn Caption="Paginação?" FieldName="IndicaPaginacao" Visible="False"
                            VisibleIndex="10">
                            <PropertiesCheckEdit ClientInstanceName="cbIndicaPaginacao" ValueChecked="S" ValueType="System.String"
                                ValueUnchecked="N">
                            </PropertiesCheckEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="10" Caption="Paginação?" />
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="Itens por página" FieldName="QuantidadeItensPaginacao"
                            Visible="False" VisibleIndex="11">
                            <PropertiesSpinEdit ClientInstanceName="seQtdeItensPagina" DisplayFormatString="g"
                                MinValue="1" NumberType="Integer" MaxValue="65000">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </PropertiesSpinEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="11" />
                        </dxwgv:GridViewDataSpinEditColumn>
                        <dxwgv:GridViewDataCheckColumn Caption="Opção disponível?" FieldName="IndicaOpcaoDisponivel"
                            VisibleIndex="13" Width="180px">
                            <PropertiesCheckEdit ClientInstanceName="cbIndicaOpcaoDisponivel" ValueChecked="S"
                                ValueType="System.String" ValueUnchecked="N">
                            </PropertiesCheckEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="4" Caption="Opção disponível?" />
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="Tipo" FieldName="TipoLista" VisibleIndex="14"
                            Width="185px">
                            <PropertiesComboBox ClientInstanceName="cmbTipoLista" Native="True">
                                <Items>
                                    <dxtv:ListEditItem Text="Consulta simples" Value="RELATORIO" />
                                    <dxtv:ListEditItem Text="Consulta OLAP" Value="OLAP" />
                                    <dxtv:ListEditItem Text="Consulta em árvore" Value="ARVORE" />
                                    <dxtv:ListEditItem Text="Processo" Value="PROCESSO" />
                                    <dxtv:ListEditItem Text="Dashboard" Value="DASHBOARD" />
                                    <dxtv:ListEditItem Text="Relatório" Value="REPORT" />
                                </Items>
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="URL" FieldName="URL" Visible="False" VisibleIndex="15">
                            <PropertiesTextEdit ClientInstanceName="txtUrl">
                            </PropertiesTextEdit>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="16" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" ShowInCustomizationForm="False"
                            Visible="False" VisibleIndex="19">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="Módulo" FieldName="CodigoModuloMenu" VisibleIndex="20"
                            Width="285px">
                            <PropertiesComboBox ClientInstanceName="cmbModuloMenu" Native="True">
                                <Items>
                                    <dxe:ListEditItem Text="Administração" Value="ADM" />
                                    <dxe:ListEditItem Text="Projetos" Value="PRJ" />
                                    <dxe:ListEditItem Text="Espaço de Trabalho" Value="ESP" />
                                    <dxe:ListEditItem Text="Estratégia" Value="EST" />
                                </Items>
                            </PropertiesComboBox>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="3" />
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataCheckColumn Caption="Lista zebrada?" FieldName="IndicaListaZebrada"
                            Visible="False" VisibleIndex="21">
                            <PropertiesCheckEdit ClientInstanceName="cbIndicaListaZebrada" ValueChecked="S" ValueType="System.String"
                                ValueUnchecked="N">
                            </PropertiesCheckEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="5" />
                        </dxwgv:GridViewDataCheckColumn>
                        <dxtv:GridViewDataCheckColumn Caption="Busca por palavra chave?" FieldName="IndicaBuscaPalavraChave"
                            Visible="False" VisibleIndex="12">
                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                            </PropertiesCheckEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="14" Caption="Busca por palavra chave?" />
                        </dxtv:GridViewDataCheckColumn>
                        <dxtv:GridViewDataComboBoxColumn Caption="Dashboard" FieldName="IDDashboard"
                            Visible="False" VisibleIndex="16">
                            <PropertiesComboBox DataSourceID="sdsDashboards" TextField="TituloDashboard"
                                ValueField="IDDashboard" ValueType="System.Guid">
                            </PropertiesComboBox>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True"
                                VisibleIndex="17" />
                        </dxtv:GridViewDataComboBoxColumn>
                        <dxtv:GridViewDataComboBoxColumn Caption="Relatorio" FieldName="IDRelatorio" Visible="False" VisibleIndex="17">
                            <PropertiesComboBox DataSourceID="sdsRelatorios" TextField="TituloRelatorio" ValueField="IDRelatorio" ValueType="System.Guid">
                            </PropertiesComboBox>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="18" />
                        </dxtv:GridViewDataComboBoxColumn>
                        <dxtv:GridViewDataSpinEditColumn Caption="Ordem do grupo" FieldName="OrdemGrupoMenu" Visible="False" VisibleIndex="22">
                            <PropertiesSpinEdit DisplayFormatString="g" MaxValue="65000" MinValue="1" NumberType="Integer">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </PropertiesSpinEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="12" />
                        </dxtv:GridViewDataSpinEditColumn>
                        <dxtv:GridViewDataSpinEditColumn Caption="Ordem do item" FieldName="OrdemItemGrupoMenu" Visible="False" VisibleIndex="23">
                            <PropertiesSpinEdit DisplayFormatString="g" MaxValue="65000" MinValue="1" NumberType="Integer">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </PropertiesSpinEdit>
                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="13" />
                        </dxtv:GridViewDataSpinEditColumn>
                        <dxtv:GridViewDataComboBoxColumn Caption="Sub-Lista" FieldName="CodigoSubLista" Visible="False" VisibleIndex="18">
                            <PropertiesComboBox DataSourceID="sdsSubLista" TextField="NomeLista" ValueField="CodigoLista" ValueType="System.Int32">
                            </PropertiesComboBox>
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="19" />
                        </dxtv:GridViewDataComboBoxColumn>
                        <dxtv:GridViewDataTextColumn VisibleIndex="24">
                        </dxtv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing EditFormColumnCount="4" Mode="PopupEditForm" />
                    <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                    <SettingsText ConfirmDelete="Desejsa excluir o registro?" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                            Width="960px" AllowResize="True" Height="450px" MinHeight="250px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>
                    <EditFormLayoutProperties ColCount="4" ColumnCount="4" EncodeHtml="False" ShowItemCaptionColon="False">
                        <Items>
                            <dxtv:GridViewTabbedLayoutGroup ColSpan="4" ColumnSpan="4">
                                <Items>
                                    <dxtv:GridViewLayoutGroup Caption="Geral" ColCount="4" ColSpan="4" ColumnCount="4" ColumnSpan="4" Width="100%">
                                        <Items>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="TituloLista" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="TipoLista" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="CodigoModuloMenu">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem Caption="Opção disponível?" ColSpan="1" ColumnName="IndicaOpcaoDisponivel">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="IndicaListaZebrada">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="GrupoMenu">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="ItemMenu">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="GrupoPermissao">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="ItemPermissao">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem Caption="Paginação?" ColSpan="1" ColumnName="IndicaPaginacao">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="QuantidadeItensPaginacao">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="OrdemGrupoMenu">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="OrdemItemGrupoMenu">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="CodigoSubLista" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="URL" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="IDRelatorio" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="2" ColumnName="IDDashboard" ColumnSpan="2">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                            <dxtv:GridViewColumnLayoutItem Caption="Busca por palavra chave?" ColSpan="1" ColumnName="IndicaBuscaPalavraChave">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                        </Items>
                                    </dxtv:GridViewLayoutGroup>
                                    <dxtv:GridViewLayoutGroup Caption="Consulta de banco de dados" ColSpan="4" ColumnSpan="4" Width="100%">
                                        <Items>
                                            <dxtv:GridViewColumnLayoutItem ColSpan="1" ColumnName="ComandoSelect" ShowCaption="False">
                                                <CaptionSettings Location="Top" />
                                            </dxtv:GridViewColumnLayoutItem>
                                        </Items>
                                    </dxtv:GridViewLayoutGroup>
                                </Items>
                            </dxtv:GridViewTabbedLayoutGroup>
                            <dxtv:EditModeCommandLayoutItem ColSpan="4" HorizontalAlign="Right" ColumnSpan="4">
                            </dxtv:EditModeCommandLayoutItem>
                        </Items>
                        <SettingsAdaptivity>
                            <GridSettings StretchLastItem="True">
                                <Breakpoints>
                                    <dxtv:LayoutBreakpoint ColumnCount="1" MaxWidth="0" Name="bp0" />
                                </Breakpoints>
                            </GridSettings>
                        </SettingsAdaptivity>
                    </EditFormLayoutProperties>
                    <StylesEditors>
                        <Style>
                            
                        </Style>
                    </StylesEditors>
                    <SettingsAdaptivity>
                        <AdaptiveDetailLayoutProperties ColCount="4" ColumnCount="4">
                            <SettingsAdaptivity>
                                <GridSettings>
                                    <Breakpoints>
                                        <dxtv:LayoutBreakpoint ColumnCount="1" MaxWidth="0" Name="bp0" />
                                    </Breakpoints>
                                </GridSettings>
                            </SettingsAdaptivity>
                        </AdaptiveDetailLayoutProperties>
                    </SettingsAdaptivity>
                    <Templates>
                        <DetailRow>
                            <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" Width="100%"
                                ClientInstanceName="pageControl" OnInit="pageControl_Init" EnableCallBacks="True">
                                <TabPages>
                                    <dxtc:TabPage Name="tabCampos" Text="Campos">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dxwgv:ASPxGridView ID="gvDetalhe" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDetalhe"
                                                    DataSourceID="sdsCamposLista" KeyFieldName="CodigoCampo"
                                                    OnInit="gvDetalhe_Init"
                                                    Width="90%" OnInitNewRow="gvDetalhe_InitNewRow" OnBeforePerformDataSelect="grid_BeforePerformDataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                            Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                            <HeaderTemplate>
                                                                <img alt="Novo Registro" id="btnIncluir" src="../../imagens/botoes/incluirReg02.png" runat="server"
                                                                    onclick="gvDetalhe.AddNewRow();" title="<%$ Resources:traducao, novo_registro %>" style="cursor: pointer" />
                                                            </HeaderTemplate>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoCampo" ReadOnly="True" ShowInCustomizationForm="False"
                                                            Visible="False" VisibleIndex="10">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoLista" ShowInCustomizationForm="False"
                                                            Visible="False" VisibleIndex="11">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Campo" FieldName="NomeCampo" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                            <PropertiesTextEdit MaxLength="255">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Título" FieldName="TituloCampo" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                            <PropertiesTextEdit MaxLength="255">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="1" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Ordem" FieldName="OrdemCampo" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="60px">
                                                            <PropertiesSpinEdit DisplayFormatString="g">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesSpinEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="3" />
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Ordem de agrupamento" FieldName="OrdemAgrupamentoCampo"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                            <PropertiesSpinEdit DisplayFormatString="g">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesSpinEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="9" />
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de campo" FieldName="TipoCampo" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="110px">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Numérico" Value="NUM" />
                                                                    <dxe:ListEditItem Text="Texto" Value="TXT" />
                                                                    <dxe:ListEditItem Text="Data" Value="DAT" />
                                                                    <dxe:ListEditItem Text="VAR" Value="VAR" />
                                                                    <dxe:ListEditItem Text="Monetário" Value="MON" />
                                                                    <dxe:ListEditItem Text="Percentual" Value="PER" />
                                                                    <dxe:ListEditItem Text="Bullet" Value="BLT" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Formato" FieldName="Formato" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="13">
                                                            <PropertiesTextEdit MaxLength="50">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="16" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Área de filtro?" FieldName="IndicaAreaFiltro"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="10" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de filtro" FieldName="TipoFiltro"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Nenhum" Value="N" />
                                                                    <dxe:ListEditItem Text="Editável" Value="E" />
                                                                    <dxe:ListEditItem Text="Combo" Value="C" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="11" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Permite Agrupamento?" FieldName="IndicaAgrupamento"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="8" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de totalizador" FieldName="TipoTotalizador"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Nenhum" Value="NENHUM" />
                                                                    <dxe:ListEditItem Text="Contar" Value="CONTAR" />
                                                                    <dxe:ListEditItem Text="Soma" Value="SOMA" />
                                                                    <dxe:ListEditItem Text="Média" Value="MEDIA" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="18" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Área de dados?" FieldName="IndicaAreaDado"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="12" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Área de colunas?" FieldName="IndicaAreaColuna"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="13" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Área de linhas?" FieldName="IndicaAreaLinha"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="14" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Área default" FieldName="AreaDefault"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Linha" Value="L" />
                                                                    <dxe:ListEditItem Text="Coluna" Value="C" />
                                                                    <dxe:ListEditItem Text="Dados" Value="D" />
                                                                    <dxe:ListEditItem Text="Filtro" Value="F" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="15" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Campo visível?" FieldName="IndicaCampoVisivel"
                                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="110px">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="5" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Campo de controle?" FieldName="IndicaCampoControle"
                                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="150px">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="6" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Iniciais controle" FieldName="IniciaisCampoControlado"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                                            <PropertiesTextEdit MaxLength="2">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="6" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn Caption="Link para o projeto?" FieldName="IndicaLink"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="20" />
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Alinhamento" FieldName="AlinhamentoCampo"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Esquerda" Value="E" />
                                                                    <dxe:ListEditItem Text="Direita" Value="D" />
                                                                    <dxe:ListEditItem Text="Centro" Value="C" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="17" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Hierarquia" FieldName="IndicaCampoHierarquia"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                                            <PropertiesComboBox EnableFocusedStyle="False">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Nenhum" Value="N" />
                                                                    <dxe:ListEditItem Text="Chave primária" Value="P" />
                                                                    <dxe:ListEditItem Text="Codigo superior" Value="S" />
                                                                </Items>
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="7" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn FieldName="LarguraColuna" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="9">
                                                            <PropertiesSpinEdit DisplayFormatString="g" MaxValue="65000" MinValue="10" NumberType="Integer">
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </PropertiesSpinEdit>
                                                            <EditFormSettings Caption="Largura da coluna" CaptionLocation="Top" Visible="True"
                                                                VisibleIndex="19" />
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Título Coluna Agrupadora" FieldName="TituloColunaAgrupadora"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                            <PropertiesTextEdit MaxLength="255">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True"
                                                                VisibleIndex="22" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataCheckColumn Caption="Coluna fixa?"
                                                            FieldName="IndicaColunaFixa" ShowInCustomizationForm="True" Visible="False"
                                                            VisibleIndex="7">
                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String"
                                                                ValueUnchecked="N">
                                                            </PropertiesCheckEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="21" />
                                                        </dxtv:GridViewDataCheckColumn>
                                                    </Columns>
                                                    <Settings VerticalScrollableHeight="135" VerticalScrollBarMode="Visible" />
                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                    <SettingsEditing EditFormColumnCount="4" Mode="PopupEditForm" />
                                                    <SettingsText ConfirmDelete="Desejsa excluir o registro?" />
                                                    <SettingsPopup>
                                                        <EditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                            Width="800px" />
                                                        <HeaderFilter MinHeight="140px">
                                                        </HeaderFilter>
                                                    </SettingsPopup>
                                                    <StylesEditors>
                                                        <Style>
                                                            
                                                        </Style>
                                                    </StylesEditors>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Name="tabFluxos" Text="Fluxos" ClientVisible="False">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dxwgv:ASPxGridView ID="gvDetalheFluxos" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="gvDetalheFluxos" DataSourceID="sdsFluxosLista" KeyFieldName="CodigoLista;CodigoFluxo"
                                                    Width="90%" OnInit="gvDetalheFluxos_Init" OnCellEditorInitialize="gvDetalheFluxos_CellEditorInitialize" OnBeforePerformDataSelect="grid_BeforePerformDataSelect">
                                                    <ClientSideEvents BeginCallback="function(s, e) {
	if(e.command == &quot;STARTEDIT&quot;){
		//s.GetEditor(&quot;CodigoFluxo&quot;).SetEnabled(false);
	}
}"
                                                        EndCallback="function(s, e) {
	//s.GetEditor(&quot;CodigoFluxo&quot;).SetEnabled(true);
}" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                            <HeaderTemplate>
                                                                <img alt="Novo Registro" id="btnIncluir" src="../../imagens/botoes/incluirReg02.png" runat="server"
                                                                    onclick="gvDetalheFluxos.AddNewRow();" title="<%$ Resources:traducao, novo_registro %>" style="cursor: pointer" />
                                                            </HeaderTemplate>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoLista" ReadOnly="True" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Visible="False">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Fluxo" FieldName="CodigoFluxo" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                            <PropertiesComboBox ClientInstanceName="cmbFluxos" DataSourceID="sdsFluxos" IncrementalFilteringMode="Contains"
                                                                TextField="NomeFluxo" ValueField="CodigoFluxo" ValueType="System.Int32">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" />
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="TituloMenu" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Caption="Título do menu">
                                                            <PropertiesTextEdit MaxLength="100">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings CaptionLocation="Top" Visible="True" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings VerticalScrollableHeight="135" VerticalScrollBarMode="Visible" />
                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                    <SettingsEditing Mode="PopupEditForm" />
                                                    <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                    <SettingsPopup>
                                                        <EditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                            Width="600px" />
                                                        <HeaderFilter MinHeight="140px">
                                                        </HeaderFilter>
                                                    </SettingsPopup>
                                                    <StylesEditors>
                                                        <Style>
                                                            
                                                        </Style>
                                                    </StylesEditors>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtv:TabPage Name="tabDetalhe" Text="Detalhe" ClientVisible="False">
                                        <ContentCollection>
                                            <dxtv:ContentControl runat="server">
                                                <dxtv:ASPxGridView ID="gvDetalheLink" ClientInstanceName="gvDetalheLink" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="sdsCamposLink" KeyFieldName="CodigoCampoLista" OnBeforePerformDataSelect="grid_BeforePerformDataSelect">
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <SettingsDataSecurity AllowDelete="False" />
                                                    <SettingsPopup>
                                                        <HeaderFilter MinHeight="140px">
                                                        </HeaderFilter>
                                                    </SettingsPopup>
                                                    <Columns>
                                                        <dxtv:GridViewDataTextColumn FieldName="CodigoCampoLista" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                            <EditFormSettings Visible="False" />
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Campo Lista" FieldName="NomeCampoLista" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <EditFormSettings Visible="False" />
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataComboBoxColumn Caption="Campo Sub-Lista" FieldName="CodigoCampoSubLista" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <PropertiesComboBox DataSourceID="sdsCamposSubLista" TextField="NomeCampo" ValueField="CodigoCampo" ValueType="System.Int32">
                                                                <ValidationSettings Display="Dynamic">
                                                                    <RequiredField ErrorText="*Campo Obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dxtv:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                </dxtv:ASPxGridView>
                                            </dxtv:ContentControl>
                                        </ContentCollection>
                                    </dxtv:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsLista" runat="server" DeleteCommand="DELETE FROM [ListaCampoUsuario] WHERE [CodigoCampo] IN (SELECT [CodigoCampo] FROM [ListaCampo] WHERE [CodigoLista] = @CodigoLista)
DELETE FROM [ListaCampo] WHERE [CodigoLista] = @CodigoLista
DELETE FROM [Lista] WHERE [CodigoLista] = @CodigoLista"
        InsertCommand="SET @IniciaisPermissao = 'CDIS' + RIGHT(REPLICATE('0', 6) + CONVERT(VARCHAR(6), ISNULL((SELECT MAX(CONVERT(INT, SUBSTRING(ps.IniciaisPermissao,6,6))) + 1 FROM PermissaoSistema ps WHERE ps.IniciaisPermissao LIKE 'CDIS%'), 1)), 6)

INSERT INTO [Lista] ([NomeLista], [GrupoMenu], [ItemMenu], [GrupoPermissao], [ItemPermissao], [TituloLista], [ComandoSelect], [IndicaPaginacao], [QuantidadeItensPaginacao], [IndicaOpcaoDisponivel], [TipoLista], [URL], [CodigoEntidade], [CodigoModuloMenu], [IndicaListaZebrada], [IniciaisPermissao], [IndicaBuscaPalavraChave], [IDDashboard], [IDRelatorio],[CodigoSubLista], [OrdemGrupoMenu], [OrdemItemGrupoMenu]) VALUES (@NomeLista, @GrupoMenu, @ItemMenu, @GrupoPermissao, @ItemPermissao, @TituloLista, @ComandoSelect, @IndicaPaginacao, @QuantidadeItensPaginacao, @IndicaOpcaoDisponivel, @TipoLista, @URL, @CodigoEntidade, @CodigoModuloMenu, @IndicaListaZebrada, @IniciaisPermissao, @IndicaBuscaPalavraChave, @IDDashboard, @IDRelatorio,@CodigoSubLista, @OrdemGrupoMenu, @OrdemItemGrupoMenu)

EXECUTE p_IncluiPermissao
   @GrupoPermissao
  ,@ItemPermissao
  ,@IniciaisPermissao
  ,'EN'
  ,5"
        SelectCommand="SELECT * FROM [Lista] WHERE ([CodigoEntidade] = @CodigoEntidade) ORDER BY [OrdemGrupoMenu], [OrdemItemGrupoMenu], [NomeLista], [TipoLista]"
        UpdateCommand="UPDATE [Lista] SET [NomeLista] = @NomeLista, [GrupoMenu] = @GrupoMenu, [ItemMenu] = @ItemMenu, [GrupoPermissao] = @GrupoPermissao, [ItemPermissao] = @ItemPermissao, [TituloLista] = @TituloLista, [ComandoSelect] = @ComandoSelect, [IndicaPaginacao] = @IndicaPaginacao, [QuantidadeItensPaginacao] = @QuantidadeItensPaginacao, [IndicaOpcaoDisponivel] = @IndicaOpcaoDisponivel, [TipoLista] = @TipoLista, [URL] = @URL, [CodigoEntidade] = @CodigoEntidade, [CodigoModuloMenu] = @CodigoModuloMenu, [IndicaListaZebrada] = @IndicaListaZebrada, [IndicaBuscaPalavraChave] = @IndicaBuscaPalavraChave, [IDDashboard] = @IDDashboard, [IDRelatorio] = @IDRelatorio, [CodigoSubLista]=@CodigoSubLista, [OrdemGrupoMenu] = @OrdemGrupoMenu, [OrdemItemGrupoMenu] = @OrdemItemGrupoMenu WHERE [CodigoLista] = @CodigoLista

 UPDATE PermissaoSistema
    SET DescricaoItemPermissao = @GrupoPermissao,
        DescricaoAcaoPermissao = @ItemPermissao
  WHERE dbo.f_GetIniciaisTipoAssociacao(CodigoTipoAssociacao) = 'EN'
    AND IniciaisPermissao = @IniciaisPermissao">
        <DeleteParameters>
            <asp:Parameter Name="CodigoLista" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NomeLista" Type="String" />
            <asp:Parameter Name="GrupoMenu" Type="String" />
            <asp:Parameter Name="ItemMenu" Type="String" />
            <asp:Parameter Name="GrupoPermissao" Type="String" />
            <asp:Parameter Name="ItemPermissao" Type="String" />
            <asp:Parameter Name="TituloLista" Type="String" />
            <asp:Parameter Name="ComandoSelect" Type="String" />
            <asp:Parameter Name="IndicaPaginacao" Type="String" />
            <asp:Parameter Name="QuantidadeItensPaginacao" Type="Int16" />
            <asp:Parameter Name="IndicaOpcaoDisponivel" Type="String" />
            <asp:Parameter Name="TipoLista" Type="String" />
            <asp:Parameter Name="URL" Type="String" />
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" Type="Int32" />
            <asp:Parameter Name="CodigoModuloMenu" Type="String" />
            <asp:Parameter Name="IndicaListaZebrada" Type="String" />
            <asp:Parameter Name="IniciaisPermissao" />
            <asp:Parameter Name="IndicaBuscaPalavraChave" />
            <asp:Parameter Name="IDDashboard" />
            <asp:Parameter Name="IDRelatorio" />
            <asp:Parameter Name="OrdemGrupoMenu" />
            <asp:Parameter Name="OrdemItemGrupoMenu" />
            <asp:Parameter Name="CodigoSubLista" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="NomeLista" Type="String" />
            <asp:Parameter Name="GrupoMenu" Type="String" />
            <asp:Parameter Name="ItemMenu" Type="String" />
            <asp:Parameter Name="GrupoPermissao" Type="String" />
            <asp:Parameter Name="ItemPermissao" Type="String" />
            <asp:Parameter Name="TituloLista" Type="String" />
            <asp:Parameter Name="ComandoSelect" Type="String" />
            <asp:Parameter Name="IndicaPaginacao" Type="String" />
            <asp:Parameter Name="QuantidadeItensPaginacao" Type="Int16" />
            <asp:Parameter Name="IndicaOpcaoDisponivel" Type="String" />
            <asp:Parameter Name="TipoLista" Type="String" />
            <asp:Parameter Name="URL" Type="String" />
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" Type="Int32" />
            <asp:Parameter Name="CodigoModuloMenu" Type="String" />
            <asp:Parameter Name="IndicaListaZebrada" Type="String" />
            <asp:Parameter Name="CodigoLista" Type="Int32" />
            <asp:Parameter Name="IniciaisPermissao" />
            <asp:Parameter Name="IndicaBuscaPalavraChave" />
            <asp:Parameter Name="IDDashboard" />
            <asp:Parameter Name="IDRelatorio" />
            <asp:Parameter Name="OrdemGrupoMenu" />
            <asp:Parameter Name="OrdemItemGrupoMenu" />
            <asp:Parameter Name="CodigoSubLista" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCamposLista" runat="server" DeleteCommand="DELETE FROM [ListaCampoUsuario] WHERE [CodigoCampo] = @CodigoCampo
DELETE FROM [ListaCampo] WHERE [CodigoCampo] = @CodigoCampo"
        InsertCommand="INSERT INTO [ListaCampo] ([CodigoLista], [NomeCampo], [TituloCampo], [OrdemCampo], [OrdemAgrupamentoCampo], [TipoCampo], [Formato], [IndicaAreaFiltro], [TipoFiltro], [IndicaAgrupamento], [TipoTotalizador], [IndicaAreaDado], [IndicaAreaColuna], [IndicaAreaLinha], [AreaDefault], [IndicaCampoVisivel], [IndicaCampoControle], [IniciaisCampoControlado], [IndicaLink], [AlinhamentoCampo], [IndicaCampoHierarquia], [LarguraColuna], [TituloColunaAgrupadora], [IndicaColunaFixa]) VALUES (@CodigoLista, @NomeCampo, @TituloCampo, @OrdemCampo, @OrdemAgrupamentoCampo, @TipoCampo, @Formato, @IndicaAreaFiltro, @TipoFiltro, @IndicaAgrupamento, @TipoTotalizador, @IndicaAreaDado, @IndicaAreaColuna, @IndicaAreaLinha, @AreaDefault, @IndicaCampoVisivel, @IndicaCampoControle, @IniciaisCampoControlado, @IndicaLink, @AlinhamentoCampo, @IndicaCampoHierarquia, @LarguraColuna, @TituloColunaAgrupadora, @IndicaColunaFixa)

INSERT INTO [ListaCampoUsuario]([CodigoCampo],[CodigoListaUsuario],[OrdemCampo],[OrdemAgrupamentoCampo],[AreaDefault],[IndicaCampoVisivel], [LarguraColuna])
SELECT SCOPE_Identity(),lcu.[CodigoListaUsuario],@OrdemCampo,@OrdemAgrupamentoCampo,@AreaDefault,'N', @LarguraColuna
  FROM [ListaCampoUsuario] AS lcu
 WHERE [CodigoCampo] IN (SELECT [CodigoCampo] FROM ListaCampo AS lc WHERE lc.CodigoLista = @CodigoLista)
 GROUP BY lcu.[CodigoListaUsuario]
"
        SelectCommand="SELECT * FROM [ListaCampo] WHERE ([CodigoLista] = @CodigoLista) ORDER BY [OrdemCampo],[TituloCampo]"
        UpdateCommand="UPDATE [ListaCampo] SET [CodigoLista] = @CodigoLista, [NomeCampo] = @NomeCampo, [TituloCampo] = @TituloCampo, [OrdemCampo] = @OrdemCampo, [OrdemAgrupamentoCampo] = @OrdemAgrupamentoCampo, [TipoCampo] = @TipoCampo, [Formato] = @Formato, [IndicaAreaFiltro] = @IndicaAreaFiltro, [TipoFiltro] = @TipoFiltro, [IndicaAgrupamento] = @IndicaAgrupamento, [TipoTotalizador] = @TipoTotalizador, [IndicaAreaDado] = @IndicaAreaDado, [IndicaAreaColuna] = @IndicaAreaColuna, [IndicaAreaLinha] = @IndicaAreaLinha, [AreaDefault] = @AreaDefault, [IndicaCampoVisivel] = @IndicaCampoVisivel, [IndicaCampoControle] = @IndicaCampoControle, [IniciaisCampoControlado] = @IniciaisCampoControlado, [IndicaLink] = @IndicaLink, [AlinhamentoCampo] = @AlinhamentoCampo, [IndicaCampoHierarquia] = @IndicaCampoHierarquia, [LarguraColuna] = @LarguraColuna, [TituloColunaAgrupadora] = @TituloColunaAgrupadora, [IndicaColunaFixa] = @IndicaColunaFixa WHERE [CodigoCampo] = @CodigoCampo">
        <DeleteParameters>
            <asp:Parameter Name="CodigoCampo" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
            <asp:Parameter Name="NomeCampo" Type="String" />
            <asp:Parameter Name="TituloCampo" Type="String" />
            <asp:Parameter Name="OrdemCampo" Type="Int16" />
            <asp:Parameter Name="OrdemAgrupamentoCampo" Type="Int16" />
            <asp:Parameter Name="TipoCampo" Type="String" />
            <asp:Parameter Name="Formato" Type="String" />
            <asp:Parameter Name="IndicaAreaFiltro" Type="String" />
            <asp:Parameter Name="TipoFiltro" Type="String" />
            <asp:Parameter Name="IndicaAgrupamento" Type="String" />
            <asp:Parameter Name="TipoTotalizador" Type="String" />
            <asp:Parameter Name="IndicaAreaDado" Type="String" />
            <asp:Parameter Name="IndicaAreaColuna" Type="String" />
            <asp:Parameter Name="IndicaAreaLinha" Type="String" />
            <asp:Parameter Name="AreaDefault" Type="String" />
            <asp:Parameter Name="IndicaCampoVisivel" Type="String" />
            <asp:Parameter Name="IndicaCampoControle" Type="String" />
            <asp:Parameter Name="IniciaisCampoControlado" Type="String" />
            <asp:Parameter Name="IndicaLink" Type="String" />
            <asp:Parameter Name="AlinhamentoCampo" Type="String" />
            <asp:Parameter Name="IndicaCampoHierarquia" Type="String" />
            <asp:Parameter Name="LarguraColuna" />
            <asp:Parameter Name="TituloColunaAgrupadora" />
            <asp:Parameter Name="IndicaColunaFixa" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
            <asp:Parameter Name="NomeCampo" Type="String" />
            <asp:Parameter Name="TituloCampo" Type="String" />
            <asp:Parameter Name="OrdemCampo" Type="Int16" />
            <asp:Parameter Name="OrdemAgrupamentoCampo" Type="Int16" />
            <asp:Parameter Name="TipoCampo" Type="String" />
            <asp:Parameter Name="Formato" Type="String" />
            <asp:Parameter Name="IndicaAreaFiltro" Type="String" />
            <asp:Parameter Name="TipoFiltro" Type="String" />
            <asp:Parameter Name="IndicaAgrupamento" Type="String" />
            <asp:Parameter Name="TipoTotalizador" Type="String" />
            <asp:Parameter Name="IndicaAreaDado" Type="String" />
            <asp:Parameter Name="IndicaAreaColuna" Type="String" />
            <asp:Parameter Name="IndicaAreaLinha" Type="String" />
            <asp:Parameter Name="AreaDefault" Type="String" />
            <asp:Parameter Name="IndicaCampoVisivel" Type="String" />
            <asp:Parameter Name="IndicaCampoControle" Type="String" />
            <asp:Parameter Name="IniciaisCampoControlado" Type="String" />
            <asp:Parameter Name="IndicaLink" Type="String" />
            <asp:Parameter Name="AlinhamentoCampo" Type="String" />
            <asp:Parameter Name="IndicaCampoHierarquia" Type="String" />
            <asp:Parameter Name="CodigoCampo" Type="Int32" />
            <asp:Parameter Name="LarguraColuna" />
            <asp:Parameter Name="TituloColunaAgrupadora" />
            <asp:Parameter Name="IndicaColunaFixa" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsFluxosLista" runat="server" DeleteCommand="DELETE FROM [ListaFluxo] WHERE [CodigoLista] = @CodigoLista AND [CodigoFluxo] = @CodigoFluxo"
        InsertCommand="INSERT INTO [ListaFluxo] ([CodigoLista], [CodigoFluxo], [TituloMenu]) VALUES (@CodigoLista, @CodigoFluxo, @TituloMenu)"
        SelectCommand="SELECT * FROM [ListaFluxo] WHERE ([CodigoLista] = @CodigoLista) ORDER BY [TituloMenu]"
        UpdateCommand="UPDATE [ListaFluxo] SET [TituloMenu] = @TituloMenu WHERE [CodigoLista] = @CodigoLista AND [CodigoFluxo] = @CodigoFluxo">
        <DeleteParameters>
            <asp:Parameter Name="CodigoLista" Type="Int32" />
            <asp:Parameter Name="CodigoFluxo" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
            <asp:Parameter Name="CodigoFluxo" Type="Int32" />
            <asp:Parameter Name="TituloMenu" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="TituloMenu" Type="String" />
            <asp:Parameter Name="CodigoLista" Type="Int32" />
            <asp:Parameter Name="CodigoFluxo" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <dxcp:ASPxCallback ID="callbackAtualizaCampos" runat="server" ClientInstanceName="callbackAtualizaCampos" OnCallback="callbackAtualizaCampos_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
            loadingPanel.Hide();
            if(s.cpErro != '')
         {
                     window.parent.mostraMensagem(s.cpErro, 'erro', true, false, null);
         }
         else
         {
                      gvDados.Refresh();                    
                      if(s.cpSucesso != '')
                    {
            
                                   window.parent.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3500);         
                    }
         }	
                
}" />
    </dxcp:ASPxCallback>
    <asp:SqlDataSource ID="sdsFluxos" runat="server" SelectCommand="SELECT [CodigoFluxo], [NomeFluxo] FROM [Fluxos] WHERE ([CodigoEntidade] = @CodigoEntidade) ORDER BY [NomeFluxo]">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDashboards" runat="server" SelectCommand=" SELECT IDDashboard,
        TituloDashboard
   FROM Dashboard
  WHERE TipoAssociacao = 'RD'"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsRelatorios" runat="server" SelectCommand=" SELECT IDRelatorio,
        TituloRelatorio
   FROM ModeloRelatorio AS mr
  WHERE TipoAssociacao = 'RD'"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsSubLista" runat="server" SelectCommand=" SELECT rs.* 
   FROM
   (
     SELECT l.CodigoLista,
            l.NomeLista
       FROM Lista AS l
      WHERE l.CodigoEntidade = @CodigoEntidade
        AND l.CodigoLista &lt;&gt; @CodigoLista
        AND l.TipoLista = 'RELATORIO'
        AND l.URL IS NULL
        AND l.CodigoSubLista IS NULL
        AND l.IniciaisListaControladaSistema IS NULL
    UNION
     SELECT NULL, ''
   ) AS rs
  ORDER BY
        rs.NomeLista">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" Type="Int32" />
            <asp:SessionParameter Name="CodigoLista" SessionField="cl" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCamposLink" runat="server" SelectCommand=" SELECT lc.CodigoCampo AS CodigoCampoLista,
        lc.NomeCampo AS NomeCampoLista,
        lcl.CodigoCampoSubLista
   FROM ListaCampo AS lc LEFT JOIN
        ListaCampoLink AS lcl ON lc.CodigoCampo = lcl.CodigoCampoLista
  WHERE lc.CodigoLista = @CodigoLista
    AND lc.IndicaCampoHierarquia = &#39;P&#39;"
        OldValuesParameterFormatString="old_{0}" ConflictDetection="CompareAllValues" UpdateCommand="DELETE FROM ListaCampoLink
      WHERE CodigoCampoLista  = @old_CodigoCampoLista
        AND CodigoCampoSubLista = @old_CodigoCampoSubLista

INSERT INTO ListaCampoLink
           (CodigoCampoLista
           ,CodigoCampoSubLista)
     VALUES
           (@CodigoCampoLista
           ,@CodigoCampoSubLista)">
        <SelectParameters>
            <asp:SessionParameter SessionField="cl" Name="CodigoLista"></asp:SessionParameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="old_CodigoCampoLista" />
            <asp:Parameter Name="old_CodigoCampoSubLista" />
            <asp:Parameter Name="CodigoCampoLista" />
            <asp:Parameter Name="CodigoCampoSubLista" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCamposSubLista" runat="server" SelectCommand="SELECT CodigoCampo, NomeCampo FROM [ListaCampo] WHERE ([CodigoLista] = @CodigoSubLista) ORDER BY [TituloCampo]">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoSubLista" SessionField="csl" />
        </SelectParameters>
    </asp:SqlDataSource>
    <dxpc:ASPxPopupControl ID="popup" runat="server" HeaderText="Informe a senha de acesso"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px"
        ClientInstanceName="popup" CloseAction="None" Modal="True" ShowCloseButton="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table style="width: 100%;">
                    <tr>
                        <td width="100%">
                            <dxe:ASPxTextBox ID="txtSenha" runat="server" ClientInstanceName="txtSenha" Password="True"
                                Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="75px">
                            <dxe:ASPxButton ID="btnValidarSenha" runat="server" Text="Validar" ClientInstanceName="btnValidarSenha"
                                Width="75px">
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dxe:ASPxLabel ID="lblMensagem" runat="server" ClientVisible="False"
                                ForeColor="Red" Text="* A senha informada não é válida">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
            <dxcp:ASPxLoadingPanel ID="loadingPanel" runat="server" ClientInstanceName="loadingPanel" Text="Processando..."  Modal="True">
        </dxcp:ASPxLoadingPanel>

    <script type="text/javascript">
        var comando;
        function executarAposWizardLista(retorno) {
            if (retorno) {
                var funcAssistenteFinalizado = function () {
                    gvDados.Refresh();
                    
                    window.parent.mostraMensagem('Assistente de relatório finalizado com sucesso!', 'sucesso', false, false, null);
                }

                if (retorno.gerarColunas) {
                    var func = function (ret) {
                        if (ret.grid != null) {
                            ret.grid.CancelEdit();
                            funcAssistenteFinalizado();
                        }
                        
                    };
                    
                    window.parent.showModal2('WizardDefinicaoListaCampos.aspx?tipoLista=' + retorno.tipoLista + '&csl=' + retorno.codigoSubLista, 'Assistente para configuração de campos do relatórios', screen.width - 30, window.parent.innerHeight - 60, func);
                }
                else {
                    funcAssistenteFinalizado();
                }
            }
        }


    </script>
</asp:Content>
