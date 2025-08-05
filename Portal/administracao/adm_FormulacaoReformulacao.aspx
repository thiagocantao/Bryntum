<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_FormulacaoReformulacao.aspx.cs" Inherits="administracao_FormulacaoReformulacao"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr>
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                &nbsp;
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Reformulação Orçamentária"></asp:Label>
                                    </td>
                                    <td align="right">
                <table>
                    <tr>
                        <td style="padding-right: 10px">
                            <dxe:ASPxButton ID="AspxbuttonLimpaReformulacao" runat="server" 
                                
                                Text="Limpa Reformulação" ClientInstanceName="btnLimpaReformulacao" Width="150px"
                                AutoPostBack="False" onclick="btnLimpaReformulacao_Click" 
                                ClientEnabled="False" ClientVisible="False">
                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = ConfirmaLimpa();
   
}" Init="function(s, e) {
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
            <td align="right" style="width: 147px; padding-right: 10px;" id="tdLblMes">
                <dxe:ASPxLabel ID="lblMes" runat="server"  
                    Text="Mês Referência ZEUS:" ClientInstanceName="lblMes">
                </dxe:ASPxLabel>
            </td>
            <td align="left" style="width: 198px; padding-right: 10px;" id="tdcomboMes">
                <dxe:ASPxComboBox ID="ddlMes" runat="server" ClientInstanceName="ddlMes" EnableCallbackMode="True"
                     OnCallback="ddlMes_Callback" ValueType="System.Int32"
                    Width="193px" Height="18px">
                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Mes);
}" />
                </dxe:ASPxComboBox>
            </td>
                        <td>
                            <dxe:ASPxButton ID="AspxbuttonInicioReformulacao" runat="server"
                                Text="Iniciar Reformulação" ClientInstanceName="btnInicioReformulacao"
                                Style="margin-left: 0px" Width="170px" AutoPostBack="False" 
                                onclick="AspxbuttonInicioReformulacao_Click">
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = ConfirmaReformulacao();
    
}" />
                                <Paddings Padding="0px" />

<Paddings Padding="0px"></Paddings>
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="AspxbuttonTermino" runat="server" 
                                Text="Encerrar Reformulação" Width="170px" AutoPostBack="False" 
                                ClientInstanceName="btnTermino" onclick="AspxbuttonTermino_Click">
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = ConfirmaEncerramento();
   
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="padding-right: 10px">
                            <dxe:ASPxButton ID="AspxbuttonDesbloqueio" runat="server" 
                                Text="Desbloqueio Projetos" ClientInstanceName="btnDesbloqueio" Width="170px"
                                AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
   ConfirmaInicioDesbloqueio();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False" 
                                Width="100%">
                                <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dxe:ASPxLabel ID="lblMostraMensagem" runat="server" 
        ClientInstanceName="lblMostraMensagem" Font-Size="Small">
        <Border BorderStyle="None" />
    </dxe:ASPxLabel>
                                    </dxp:PanelContent>
</PanelCollection>
                            </dxrp:ASPxRoundPanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" AutoGenerateColumns="False"
                                Width="100%"  ID="gvDados" KeyFieldName="linha"
                                OnCustomCallback="gvDados_CustomCallback" ClientVisible="False" 
                                oncelleditorinitialize="gvDados_CellEditorInitialize" 
                                onrowupdating="gvDados_RowUpdating">
<ClientSideEvents CustomButtonClick="function(s, e) {
   
}" EndCallback="function(s, e) {
}"></ClientSideEvents>

<SettingsText Title="Lista de projetos bloqueados para Reformula&#231;&#227;o Or&#231;ament&#225;ria"></SettingsText>
                                <ClientSideEvents />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" VisibleIndex="0" Caption=" " ShowEditButton="true">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Desbloquear" 
                                                Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir" Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes" Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Caption="Codigo Projeto"
                                        VisibleIndex="0" Width="150px" ReadOnly="True" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Caption="Nome Projeto" 
                                        VisibleIndex="1" ReadOnly="True">
                                        <PropertiesTextEdit>
                                            <ClientSideEvents Init="function(s, e) {
	s.SetEnabled(false);
}" />
                                            
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="False" />
                                        <EditCellStyle BackColor="#EBEBEB">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Data Último Bloqueio" 
                                        FieldName="DataUltimoBloqueio" ReadOnly="True" VisibleIndex="2" Width="200px">
                                        <PropertiesDateEdit DisplayFormatString="" AllowMouseWheel="False" 
                                            AllowUserInput="False">
                                            <ClientSideEvents Init="function(s, e) {
	s.SetEnabled(false);
}" />
                                            <DropDownButton Visible="False">
                                            </DropDownButton>
                                        </PropertiesDateEdit>
                                        <EditFormSettings Visible="False" />
                                        <EditCellStyle BackColor="#EBEBEB">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Usuário responsável pelo último bloqueio"
                                        FieldName="NomeUsuario" VisibleIndex="3" ReadOnly="True" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="Usuário Bloqueio" 
                                        FieldName="CodigoUsuario" VisibleIndex="4">
                                        <PropertiesComboBox DataSourceID="sdsResponsaveisAcao" 
                                            EnableCallbackMode="True" IncrementalFilteringMode="Contains" 
                                            TextField="NomeUsuario" ValueField="CodigoUsuario" 
                                            ValueType="System.Int32">
                                            <Columns>
                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                            </Columns>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" />
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Settings VerticalScrollBarMode="Visible" ShowTitlePanel="True"></Settings>
                                <SettingsText Title="Lista de projetos bloqueados para Reformulação Orçamentária" />

                                <StylesEditors>
                                    <ReadOnly BackColor="#EBEBEB"  
                                        ForeColor="Black">
                                    </ReadOnly>
                                    <TextBox >
                                    </TextBox>
                                    <CheckBox >
                                    </CheckBox>
                                </StylesEditors>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDadosGeraArquivo" AutoGenerateColumns="False"
                                Width="100%"  ID="gvDadosGeraArquivo"
                                KeyFieldName="CodigoProjeto">
                                <ClientSideEvents CustomButtonClick="function(s, e) {

}" EndCallback="function(s, e) {
	
	if(s.cp_Limpa != '')
		mostraEscondeElementos(&quot;Tudo&quot;);
}" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Caption="Seleção" VisibleIndex="0"
                                        Width="35px" Visible="False">
                                        <HeaderTemplate>
                                            <input onclick="gvDadosGeraArquivo.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                type="checkbox" />
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Caption="Nome Projeto" VisibleIndex="2"
                                        Width="100%">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                    ShowTitlePanel="True"></Settings>
                                <SettingsText Title="Relação de projetos disponíveis para reformulação" />
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDadosLogReformulacao" AutoGenerateColumns="False"
                                Width="100%"  
                                ID="gvDadosLogReformulacao" ClientVisible="False">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="Data" Caption="Data" VisibleIndex="0" Width="150px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Etapa" Caption="Etapa" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ação" FieldName="Acao" VisibleIndex="2" Width="350px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nome Usuário" FieldName="Usuario" VisibleIndex="3"
                                        Width="350px">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible">
                                </Settings>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>

                            <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" 
                                Text="Aguarde término do procedimento...&amp;hellip;" 
                                ClientInstanceName="LoadingPanel" 
         Modal="True">
                            </dxlp:ASPxLoadingPanel>

    <asp:SqlDataSource ID="sdsResponsaveisAcao" runat="server"></asp:SqlDataSource>
                        </asp:Content>


