<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="IntegracaoSMD.aspx.cs" Inherits="administracao_IntegracaoSMD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="padding-left: 10px" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Integração SMD">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="padding: 10px 10px 0px 10px">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                            Text="Ano Competência:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                            Text="Arquivo CSV:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px; height: 40px;" valign="top">
                                        <dxe:ASPxSpinEdit ID="txtAno" runat="server" ClientInstanceName="txtAno"
                                            Height="21px" MaxValue="9999" MinValue="1900" Number="0" NumberType="Integer"
                                            Width="110px">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td style="padding-right: 10px; height: 40px;" valign="top">
                                        <dxuc:ASPxUploadControl ID="fluArquivo" runat="server" ClientInstanceName="fluArquivo"
                                             Width="380px">
                                            <ValidationSettings AllowedFileExtensions=".csv" NotAllowedFileExtensionErrorText="Extensão de arquivo não permitida. Favor carregar um arquivo com extensão .csv">
                                            </ValidationSettings>
                                            <BrowseButton Text="Selecionar...">
                                            </BrowseButton>
                                        </dxuc:ASPxUploadControl>
                                    </td>
                                    <td style="padding-right: 10px; height: 40px;" valign="top">
                                        <dxe:ASPxButton ID="btnImportar" runat="server" 
                                            Text="Ler Arquivo" Width="115px" OnClick="btnImportar_Click">
                                            <ClientSideEvents Click="function(s, e) {
	lpCarregando.Show();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="height: 40px;" valign="top">
                                        <dxe:ASPxButton ID="btnSalvar" runat="server" 
                                            Text="Salvar" Width="115px" ClientEnabled="False" AutoPostBack="False" ClientInstanceName="btnSalvar">
                                            <ClientSideEvents Click="function(s, e) {
	
	callback.PerformCallback();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40px;" valign="top">
                                        <dxe:ASPxButton ID="btnDesfazer" runat="server" 
                                            Text="Lista de Cargas" Width="115px" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
	pcCargas.Show();
	//window.top.showModal('./DesfazerCargaSMD.aspx', 'Desfazer Cargas Realizadas', 1000, 390, '', null);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding: 0px 10px 0px 10px">
                <dxwgv:ASPxGridView ID="gvDados" runat="server" 
                    Width="100%" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" />
                </dxwgv:ASPxGridView>
                <dxhf:ASPxHiddenField ID="hfComandoSQL" runat="server" ClientInstanceName="hfComandoSQL">
                </dxhf:ASPxHiddenField>
                <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                    <ClientSideEvents BeginCallback="function(s, e) {
	lpCarregando.Show();
}" EndCallback="function(s, e) {
	lpCarregando.Hide();
	btnSalvar.SetEnabled(s.cp_Status != '1');
                if(s.cp_Status == '1')
                {
                           window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
	           gvCargas.PerformCallback();
                }
                else if (s.cp_Status == &quot;0&quot;)
               {
                          window.top.mostraMensagem(s.cp_MSG, 'erro', true, false, null);
               }
}" />
                </dxcb:ASPxCallback>
                <dxlp:ASPxLoadingPanel ID="lpCarregando" runat="server" ClientInstanceName="lpCarregando"
                     Modal="True" Text="Carregando&amp;hellip;">
                </dxlp:ASPxLoadingPanel>
                <dxpc:ASPxPopupControl ID="pcCargas" runat="server" ClientInstanceName="pcCargas"
                     HeaderText="Lista de Cargas Realizadas"
                    Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowCloseButton="False" Width="1000px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-bottom: 10px">
                                        <dxwgv:ASPxGridView ID="gvCargas" runat="server" AutoGenerateColumns="False"
                                            KeyFieldName="CodigoCarga" OnRowDeleting="gvDados_RowDeleting"
                                            Width="100%">
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
                        var codigoCarga = s.GetRowKey(e.visibleIndex);
	window.top.showModal('./LogCargaSMD.aspx?CC=' + codigoCarga, 'Log da Carga', 1000, 390, '', null);
}" />
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                    VisibleIndex="0" Width="60px" ShowDeleteButton="true">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnLOG" Text="Visualizar Log">
                                                            <Image ToolTip="Visualizar Log" Url="~/imagens/botoes/btnLOG.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Ano/Mes Ref." FieldName="AnoMesRef" ShowInCustomizationForm="True"
                                                    VisibleIndex="1" Width="100px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Nome do Arquivo" FieldName="NomeArquivoPlanilha"
                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Data/Hora Início Carga" FieldName="DataHoraInicioCarga"
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="155px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}">
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Linhas Planilha" FieldName="QtdLinhasPlanilha"
                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Linhas Importadas" FieldName="QtdLinhasImportadas"
                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Competência" FieldName="Competencia" ShowInCustomizationForm="True"
                                                    VisibleIndex="3" Width="150px">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" />
                                            <SettingsText ConfirmDelete="Deseja desfazer a carga selecionada?" />
                                            <SettingsCommandButton>
                                                <DeleteButton RenderMode="Image" Text="Desfazer Carga">
                                                    <Image AlternateText="Excluir" ToolTip="Desfazer Carga" Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </DeleteButton>
                                            </SettingsCommandButton>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                            Text="Fechar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	pcCargas.Hide();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
