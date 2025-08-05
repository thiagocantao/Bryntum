<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_cfg_validacaoCampo.ascx.cs" Inherits="formularios_UC_Formularios_uc_cfg_validacaoCampo" %>
<dxcp:ASPxPopupControl ID="ppDvEditorExpressaoValidacao" runat="server" ClientInstanceName="ppDvEditorExpressaoValidacao" CloseAction="CloseButton" HeaderText="Expressão para validação do campo" Modal="True" OnWindowCallback="ppDvEditorExpressaoValidacao_WindowCallback" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="900px" AllowDragging="True">
    <ClientSideEvents EndCallback="function(s, e) {
        hfStatus.Set('ServidorDeveIgnorarEstaAcao', 0);
        mostraEditorExpressao(s.cpCodigoNomeCampoSelecionado);
}" />
    <ContentCollection>
        <dxcp:PopupControlContentControl runat="server">
            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                <tr>
                    <td style="width: 40%">
                        <dxtv:ASPxGridView ID="gvCamposDisponiveis" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvCamposDisponiveis" Width="100%">
                            <ClientSideEvents RowDblClick="function(s, e) {
	selecionaCampoClicado(e.visibleIndex);
}" />
                            <SettingsPager Visible="False" Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Visible" />
                            <SettingsBehavior AllowSort="False" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn Caption="(A)" FieldName="Sequencia" Name="colSequencia" ShowInCustomizationForm="True" VisibleIndex="0" Width="50px">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="ID" FieldName="CodigoCampo" Name="colCodigoCampo" ShowInCustomizationForm="True" Visible="false" VisibleIndex="1" Width="50px">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Nome do campo" FieldName="NomeCampo" Name="colNomeCampo" ShowInCustomizationForm="True" VisibleIndex="2">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Tipo" FieldName="CodigoTipoCampo" Name="colCodigoTipoCampo" ShowInCustomizationForm="True" VisibleIndex="3" Width="50px">
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxtv:ASPxGridView>
                    </td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td style="vertical-align: top">
                        <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                            <tr>
                                <td>Campo selecionado:</td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcp:ASPxTextBox ID="txtCampoSelecionado" ClientInstanceName="txtCampoSelecionado" runat="server" ReadOnly="true" Width="100%"></dxcp:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Expressão:</td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxMemo ID="txtEdidorExpressao" runat="server" ClientInstanceName="txtEdidorExpressao" Height="71px" MaxLength="2000" Width="100%">
                                    </dxtv:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Mensagem:</td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcp:ASPxTextBox ID="txtMensagemValidacao" ClientInstanceName="txtMensagemValidacao" runat="server" MaxLength="200" Width="100%"></dxcp:ASPxTextBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 100px">
                        <dxtv:ASPxButton ID="btnSalvarExpressao" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarExpressao"  Text="Ok" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
                                if (salvaExpressaoCampo())
                                    ;//ppDvEditorExpressaoValidacao.Hide();
}" />
                        </dxtv:ASPxButton>
                    </td>
                    <td style="width: 90px">
                        <dxtv:ASPxButton ID="btnCancelarExpressao" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarExpressao"  Text="Cancelar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
                                                ppDvEditorExpressaoValidacao.Hide();
}" />
                        </dxtv:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxcp:ASPxHiddenField ID="hfCamposDisponiveis" ClientInstanceName="hfCamposDisponiveis" runat="server" OnCustomCallback="hfCamposDisponiveis_CustomCallback">
                <ClientSideEvents EndCallback="function(s, e) {
                    ppDvEditorExpressaoValidacao.Hide();
                    loadPanel.Hide();	
}" />
            </dxcp:ASPxHiddenField>
            <dxtv:ASPxLoadingPanel ID="loadPanel" runat="server" ClientInstanceName="loadPanel" Text="Aguarde..." Theme="Default" Width="147px" Modal="True">
            </dxtv:ASPxLoadingPanel>
        </dxcp:PopupControlContentControl>
    </ContentCollection>
</dxcp:ASPxPopupControl>

