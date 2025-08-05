<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="mapaEstrategico.aspx.cs" Inherits="_Estrategias_wizard_mapaEstrategico" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- Painel arrendondado lista de projetos -->
    <div <%--enableviewstate="false"--%>>
    </div>
    <script type="text/javascript">
        function desenhaMapaEstrategico(CME) {
            gvMapa.GetRowValues(gvMapa.GetFocusedRowIndex(), 'CodigoMapaEstrategico;TituloMapaEstrategico', PrepararLinkMapaEstrategicoFlash);
        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td id="ConteudoPrincipal">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <!-- Mapas -->
                        <tr>
                            <td>
                                <!-- GvMAPAS -->
                                <div id="divGrid" style="visibility: hidden; padding-top:10px">
                                <dxwgv:ASPxGridView ID="gvMapa" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMapa"
                                    DataSourceID="dsMapa" KeyFieldName="CodigoMapaEstrategico" OnCustomCallback="gvMapa_CustomCallback"
                                    OnRowInserting="gvMapa_RowInserting" OnRowUpdating="gvMapa_RowUpdating" OnRowDeleting="gvMapa_RowDeleting"
                                    Width="100%" OnCellEditorInitialize="gvMapa_CellEditorInitialize"
                                    OnHtmlRowPrepared="gvMapa_HtmlRowPrepared" OnCommandButtonInitialize="gvMapa_CommandButtonInitialize"
                                    OnCustomButtonInitialize="gvMapa_CustomButtonInitialize" OnAutoFilterCellEditorInitialize="gvMapa_AutoFilterCellEditorInitialize" OnRowValidating="gvMapa_RowValidating">
                                    <Templates>
                                        <FooterRow>
                                            <table cellspacing="0" cellpadding="0" class="grid-legendas">
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-inativo"><span></span></td>
                                                    <td class="grid-legendas-label grid-legendas-label-inativo">
                                                        <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                            Font-Bold="False" Text="<%# Resources.traducao.mapaEstrategico_mapas_estrat_gicos_inativos %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                    <StylesPopup>
                                        <EditForm>
                                            <Header Font-Bold="True">
                                            </Header>
                                        </EditForm>
                                    </StylesPopup>
                                    <Styles>
                                        <HeaderPanel Font-Bold="False">
                                        </HeaderPanel>
                                    </Styles>
                                    <SettingsLoadingPanel Text="Carregando&amp;hellip;"></SettingsLoadingPanel>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3">
                                    </SettingsEditing>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                            AllowResize="True" Width="600px" />
                                    </SettingsPopup>
                                    <SettingsText Title="<%$ Resources:traducao, mapaEstrategico_mapas_associados___unidade %>" PopupEditFormCaption="<%$ Resources:traducao, mapaEstrategico_cadastro_de_mapa_estrat_gico %>"></SettingsText>
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"
                                        CustomButtonClick="function(s, e) {
	onClick_CustomButtomGvDados(s, e);
}"
                                        BeginCallback="function(s, e) {
	comando = e.command;
}"
                                        EndCallback="function(s, e) {
     if(comando == &quot;UPDATEEDIT&quot;)
     {
           if(s.cpErro != '')
           {
                    window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
           }
           else
          {
                    if(s.cpSucesso != '')
                   {
                           window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
                   }
           }
      }
}" Init="function(s, e) {
                                        AdjustSize();
                                        document.getElementById(&quot;divGrid&quot;).style.visibility = &quot;&quot;;
                                        
                                       }"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="140px" VisibleIndex="0" ShowEditButton="true"
                                            ShowDeleteButton="true">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDisenhoMapa" Text="<%$ Resources:traducao, mapaEstrategico_editar_desenho %>">
                                                    <Image Url="~/imagens/mapaEstrategico/DisenhoMapa.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilhar" Text="<%$ Resources:traducao, mapaEstrategico_compartilhar %>">
                                                    <Image Url="~/imagens/compartilhar.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissoes" Text="<%$ Resources:traducao, mapaEstrategico_alterar_permiss_es %>">
                                                    <Image Url="~/imagens/Perfis/Perfil_Permissoes.png" ToolTip="<%$ Resources:traducao, mapaEstrategico_alterar_permiss_es %>">
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
                                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, mapaEstrategico_incluir %>">
                                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<%$ Resources:traducao, mapaEstrategico_exportar %>">
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, mapaEstrategico_exportar_para_xls %>">
                                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, mapaEstrategico_exportar_para_pdf %>">
                                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, mapaEstrategico_exportar_para_rtf %>">
                                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, mapaEstrategico_exportar_para_html %>" ClientVisible="False">
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
                                                                            <dxm:MenuItem Text="<%$ Resources:traducao, mapaEstrategico_salvar %>" ToolTip="<%$ Resources:traducao, mapaEstrategico_salvar_layout %>">
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
                                        <dxwgv:GridViewDataTextColumn FieldName="TituloMapaEstrategico" Name="TituloMapaEstrategico"
                                            Caption="T&#237;tulo Mapa" VisibleIndex="1">
                                            <PropertiesTextEdit DisplayFormatString="{0}">
                                                <Style>
                                                
                                            </Style>
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TituloMapaEstrategico" Width="430px" Caption="T&#237;tulo"
                                            Visible="False" VisibleIndex="2">
                                            <PropertiesTextEdit MaxLength="100" DisplayFormatString="{0}">
                                                <ClientSideEvents Validation="function(s, e) {
	e.isValid = false;
	var option = s.GetText();
	var str = option.replace(/^(\s|\&amp;nbsp;)*|(\s|\&amp;nbsp;)*$/g,&quot;&quot;);

	if(str != '')
		e.isValid = true
	else
		e.errorText = traducao.mapaEstrategico_campo_obrigat__243_rio__;
}"></ClientSideEvents>
                                                <ValidationSettings ErrorDisplayMode="None">
                                                    <RequiredField ErrorText="* Campo Obrigatório" IsRequired="True" />
                                                </ValidationSettings>
                                                <Style>
                                                
                                            </Style>
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <EditFormSettings ColumnSpan="3" Visible="True" VisibleIndex="0" CaptionLocation="Top"
                                                Caption="T&#237;tulo:"></EditFormSettings>
                                            <CellStyle>
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoUnidadeNegocio" Name="CodigoUnidadeNegocio"
                                            Visible="False" VisibleIndex="3" Caption="<%$ Resources:traducao, mapaEstrategico_entidade_unidade %>">
                                            <PropertiesComboBox ValueType="System.Int32">
                                                <ClientSideEvents Validation="function(s, e) {
	e.isValid = false;
	var option = s.GetValue();

	if(option != null &amp;&amp; option != '')
		e.isValid = true
	else
		e.errorText = traducao.mapaEstrategico_campo_obrigat__243_rio__;
}"></ClientSideEvents>
                                                <ItemStyle />
                                                <ValidationSettings ErrorDisplayMode="None">
                                                </ValidationSettings>
                                                <Style>
                                                
                                            </Style>
                                            </PropertiesComboBox>
                                            <Settings AllowAutoFilter="False" />
                                            <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="1" CaptionLocation="Top"
                                                Caption="<%$ Resources:traducao, mapaEstrategico_entidade_unidade %>"></EditFormSettings>
                                            <CellStyle>
                                            </CellStyle>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="IndicaMapaEstrategicoAtivo" Width="75px"
                                            Caption="Ativo" Visible="False" VisibleIndex="4">
                                            <PropertiesComboBox ValueType="System.String">
                                                <ClientSideEvents Validation="function(s, e) {
	e.isValid = false;
	var option = s.GetValue();

	if(option == 'S' || option == 'N')
		e.isValid = true
	else
		e.errorText = traducao.mapaEstrategico_campo_obrigat__243_rio__;
}"></ClientSideEvents>
                                                <Items>
                                                    <dxe:ListEditItem Text="<%$ Resources:traducao,sim %>" Value="S"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="<%$ Resources:traducao,n_o %>" Value="N"></dxe:ListEditItem>
                                                </Items>
                                                <ItemStyle />
                                                <ListBoxStyle>
                                                </ListBoxStyle>
                                                <ValidationSettings ErrorDisplayMode="None">
                                                    <RequiredField ErrorText="<%$ Resources:traducao, mapaEstrategico_campo_obrigat_rio__ %>"></RequiredField>
                                                </ValidationSettings>
                                                <Style>
                                                
                                            </Style>
                                            </PropertiesComboBox>
                                            <Settings AllowAutoFilter="False" />
                                            <EditFormSettings Visible="True" VisibleIndex="2" CaptionLocation="Top" Caption="<%$ Resources:traducao, mapaEstrategico_ativo_ %>"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="VersaoMapaEstrategico" Width="80px" Caption="<%$ Resources:traducao, mapaEstrategico_vers_o %>"
                                            VisibleIndex="5">
                                            <Settings AllowAutoFilter="False" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            <CellStyle>
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="DataInicioVersaoMapaEstrategico" Width="120px"
                                            Caption="<%$ Resources:traducao, mapaEstrategico_in_cio %>" VisibleIndex="6">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <Settings ShowFilterRowMenu="True" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="DataTerminoVersaoMapaEstrategico" Width="120px"
                                            Caption="<%$ Resources:traducao, mapaEstrategico_t_rmino %>" VisibleIndex="7">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                            </PropertiesDateEdit>
                                            <Settings ShowFilterRowMenu="True" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="<%$ Resources:traducao, mapaEstrategico_ativo %>" FieldName="TextoIndicaMapaEstrategicoAtivo"
                                            GroupIndex="0" SortIndex="0" SortOrder="Descending" VisibleIndex="8" Width="75px">
                                            <PropertiesTextEdit>
                                                <Style>
                                                
                                            </Style>
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <EditFormSettings CaptionLocation="None" Visible="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                        ShowGroupPanel="True"></Settings>
                                </dxwgv:ASPxGridView>
                                </div>
                                <!-- FIM GVMAPA -->
                                <!-- exemplo gvMAPAS -->
                                <!-- FIM exemplo GVMAPAS -->
                                <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" HeaderText="<%$ Resources:traducao, mapaEstrategico_permiss_es_de_acesso_ao_mapa_estrat_gico %>"
                                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                    ShowCloseButton="False" CloseAction="None"
                                    Font-Bold="True" Width="820px">
                                    <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnPopup(s,e);
}"></ClientSideEvents>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Mapa:" Font-Bold="False"
                                                                ID="ASPxLabel1011">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblNomeMapa" Font-Bold="True" Font-Italic="True"
                                                                Font-Underline="False" ForeColor="DimGray"
                                                                ID="lblNomeMapa">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ReadOnly="True" Visible="False"
                                                                ClientInstanceName="txtNomeMapa" ID="txtNomeMapa">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvEntidades" KeyFieldName="CodigoUnidadeNegocio"
                                                                AutoGenerateColumns="False" Width="100%"
                                                                ID="gvEntidades" OnRowDeleting="gvEntidades_RowDeleting" OnRowUpdating="gvEntidades_RowUpdating"
                                                                OnCustomCallback="gvEntidades_CustomCallback" OnRowInserting="gvEntidades_RowInserting"
                                                                OnCellEditorInitialize="gvEntidades_CellEditorInitialize">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	     if(e.buttonID == &quot;btnNovo&quot;)
     {

     }

}"></ClientSideEvents>
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="80px" ShowDeleteButton="true">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <HeaderTemplate>
                                                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""gvEntidades.AddNewRow();"" style=""cursor: pointer;""</td></tr></table>", hfGeral.Get("definicaoEntidade").ToString())%>
                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Entidade" FieldName="CodigoUnidadeNegocio"
                                                                        Name="colUnidade" Visible="False" VisibleIndex="2">
                                                                        <PropertiesComboBox ClientInstanceName="ddlColUnidade" ValueType="System.Int32" Width="710px">
                                                                            <ItemStyle>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </ItemStyle>
                                                                            <ListBoxStyle>
                                                                            </ListBoxStyle>
                                                                            <DropDownButton ToolTip="<%$ Resources:traducao, mapaEstrategico_selecione_a_entidade_a_ter_acesso_ao_mapa_em_quest_o_ %>">
                                                                            </DropDownButton>
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                                <RequiredField ErrorText="<%$ Resources:traducao, mapaEstrategico_favor_escolher_a_unidade_a_ter_acesso_ao_mapa_em_quest_o_ %>"
                                                                                    IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <Style></Style>
                                                                        </PropertiesComboBox>
                                                                        <EditFormSettings Caption="Entidade:" CaptionLocation="Top" Visible="True"
                                                                            VisibleIndex="0" />
                                                                        <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Top" Caption="<%$ Resources:traducao, mapaEstrategico_entidade_ %>"></EditFormSettings>
                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Entidade" FieldName="NomeUnidadeNegocio" VisibleIndex="1">
                                                                        <PropertiesTextEdit Width="180px">
                                                                            <Style></Style>
                                                                        </PropertiesTextEdit>
                                                                        <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="<%$ Resources:traducao, mapaEstrategico_registronovo %>" FieldName="RegistroNovo" Visible="False"
                                                                        VisibleIndex="3">
                                                                        <EditFormSettings Visible="False" />
                                                                        <EditFormSettings Visible="False"></EditFormSettings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"
                                                                    ConfirmDelete="True"></SettingsBehavior>
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="EditForm" EditFormColumnCount="3">
                                                                </SettingsEditing>
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" HorizontalOffset="30" />
                                                                </SettingsPopup>
                                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="250"></Settings>
                                                                <SettingsText Title="<%$ Resources:traducao, mapaEstrategico_outras_entidades_com_acesso_ao_mapa %>" ConfirmDelete="<%$ Resources:traducao, mapaEstrategico_retirar_o_acesso_ao_mapa_para_esta_unidade %>"
                                                                    PopupEditFormCaption="Entidade" EmptyDataRow="<%$ Resources:traducao, mapaEstrategico_nenhuma_outra_entidade_est__com_acesso_a_este_mapa %>"
                                                                    CommandNew="<%$ Resources:traducao, mapaEstrategico_novo %>" CommandCancel="<%$ Resources:traducao, mapaEstrategico_cancelar %>" CommandUpdate="<%$ Resources:traducao, mapaEstrategico_salvar %>"></SettingsText>
                                                                <Styles>
                                                                    <TitlePanel Font-Bold="True">
                                                                    </TitlePanel>
                                                                </Styles>
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="padding-right: 18px;">
                                                            <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar"
                                                                                CausesValidation="False" Text="<%$ Resources:traducao, mapaEstrategico_salvar %>" Width="90px"
                                                                                ID="btnSalvar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="padding-left: 10px" align="right">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                Text="Fechar" Width="90px" ID="btnFechar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
                                <dxpc:ASPxPopupControl ID="pcDetalheMapa" runat="server"
                                    HeaderText="Detalhe" Modal="True" Width="720px" ClientInstanceName="pcDetalheMapa"
                                    CloseAction="None" ShowCloseButton="False" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, mapaEstrategico_entidade_ %>" ClientInstanceName="lblUnidadeNegocio"
                                                                                ID="lblUnidadeNegocio">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="width: 65px">
                                                                            <dxe:ASPxLabel runat="server" Text="Ativo:"
                                                                                ID="ASPxLabel3">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.Int32"
                                                                                Width="100%" ClientInstanceName="ddlUnidadeNegocio"
                                                                                ID="ddlUnidadeNegocio">
                                                                                <ClientSideEvents EndCallback="function(s, e) {

}"
                                                                                    SelectedIndexChanged="function(s, e) {
	//s.PerformCallback(s.GetValue());
	callBack.PerformCallback('ddl'+s.GetValue());
}"></ClientSideEvents>
                                                                                <ItemStyle />
                                                                                <ListBoxStyle>
                                                                                </ListBoxStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="100%"
                                                                                ClientInstanceName="ddlMapaAtivo" ID="ddlMapaAtivo">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="<%$ Resources:traducao, mapaEstrategico_sim %>" Value="S" Selected="True"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="<%$ Resources:traducao, mapaEstrategico_n_o %>" Value="N"></dxe:ListEditItem>
                                                                                </Items>
                                                                                <ItemStyle />
                                                                            </dxe:ASPxComboBox>
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
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="T&#237;tulo:"
                                                                ID="ASPxLabel2">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtMapaTitulo"
                                                                ID="txtMapaTitulo">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="padding-right: 10px">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvarMapa"
                                                                                CausesValidation="False" Text="<%$ Resources:traducao, mapaEstrategico_salvar %>" Width="90px"
                                                                                ID="btnSalvarMapa">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarMapa)
		onClick_btnSalvarMapa();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td align="right">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharMapa"
                                                                                Text="<%$ Resources:traducao, mapaEstrategico_fechar %>" Width="90px" ID="btnFecharMapa">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDetalheMapa.Hide();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl ID="pcMensagemGravacao" runat="server" ClientInstanceName="pcMensagemGravacao"
                                    HeaderText="" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                                    ShowShadow="False" Width="270px">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
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
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    Width="200px" OnCallback="pnCallback_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <ClientSideEvents EndCallback="function(s, e) {
	e.processOnServer = false;
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
	
}"></ClientSideEvents>
                                </dxcp:ASPxCallbackPanel>
                                <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	gvMapa.PerformCallback('ddl'+ddlUnidadeNegocio.GetValue());
}" />
                                </dxcb:ASPxCallback>
                                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
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
                                <asp:SqlDataSource ID="dsMapa"
                                    runat="server" SelectCommand="SELECT Mapa.CodigoMapaEstrategico, Mapa.TituloMapaEstrategico, Mapa.IndicaMapaEstrategicoAtivo, Ver.DataInicioVersaoMapaEstrategico, Ver.VersaoMapaEstrategico, Ver.DataTerminoVersaoMapaEstrategico, Mapa.CodigoUnidadeNegocio, Mapa.IndicaMapaCarregado FROM MapaEstrategico AS Mapa INNER JOIN VersaoMapaEstrategico AS Ver ON Mapa.CodigoMapaEstrategico = Ver.CodigoMapaEstrategico WHERE (Mapa.CodigoUnidadeNegocio = 1) AND 1=2"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <!-- Mapas -->
</asp:Content>
