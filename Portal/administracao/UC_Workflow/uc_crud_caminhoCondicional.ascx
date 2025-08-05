<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_crud_caminhoCondicional.ascx.cs" Inherits="administracao_UC_Workflow_uc_crud_caminhoCondicional" %>
<dx:ASPxPopupControl ID="pcDvCaminhoCondicional" runat="server" Width="700px" ClientInstanceName="pcDvCaminhoCondicional" AllowDragging="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Caminhos condicionais">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <dx:ASPxPanel ID="dvSelecaoEtapa" runat="server" Width="100%" ClientInstanceName="dvSelecaoEtapa" ClientIDMode="Static">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>Etapa de origem:</td>
                                <td rowspan="2" style="width: 120px; vertical-align: bottom;">
                                    <dx:ASPxButton ID="btnConfirmarEtapa" runat="server" ClientInstanceName="btnConfirmarEtapa" AutoPostBack="false" Text="Confirmar etapa" >
                                        <ClientSideEvents Click="function(s, e) {
                                            
                                            preparaTela_CaminhoCondicional_ParaUtilizarEtapaSelecionada();	
}" />
                                    </dx:ASPxButton>
                                </td>
                                <td rowspan="2" style="width: 90px; vertical-align: bottom;">
                                    <dx:ASPxButton ID="btnCancelarConfirmacao" runat="server" ClientInstanceName="btnCancelarConfirmacao" Text="Cancelar" Width="90px" >
                                        <ClientSideEvents Click="function(s, e) {
                                    e.processOnServer = false;
	pcDvCaminhoCondicional.SetVisible(false);
}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="cmbEtapaOrigemDvCaminhoCondicional" runat="server" ValueType="System.String" ClientInstanceName="cmbEtapaOrigemDvCaminhoCondicional" Width="440px"></dx:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
            <dxcp:ASPxPanel ID="dvGridCondicoes" ClientInstanceName="dvGridCondicoes" runat="server" Width="100%" ClientIDMode="Static">
                <PanelCollection>
                    <dxcp:PanelContent runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 250px">Etapa de origem:</td>
                                <td>Opção:</td>
                                <td style="width: 200px">Etapa de caminho alternativo:</td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcp:ASPxTextBox ID="txtEtapaOrigemDvCaminhoCondicional" runat="server" ReadOnly="true" Width="250px"></dxcp:ASPxTextBox>
                                </td>
                                <td>
                                    <dxcp:ASPxTextBox ID="txtEtapaOrigemLabelOpcao" ClientInstanceName="txtEtapaOrigemLabelOpcao" runat="server" Width="100%"></dxcp:ASPxTextBox>
                                </td>
                                <td >
                                    <dxcp:ASPxComboBox ID="cmbEtapaDestinoPadrao" ClientInstanceName="cmbEtapaDestinoPadrao" runat="server" ValueType="System.String" Width="200px"></dxcp:ASPxComboBox>
                                </td>
                            </tr>
                        </table>

                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_Condicoes" KeyFieldName="CodigoExpressao"
                            AutoGenerateColumns="False" Width="100%" Font-Size="8pt" ID="gv_Condicoes" OnCustomCallback="gv_Condicoes_CustomCallback">
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" A&#231;&#227;o" VisibleIndex="0"
                                    Width="80px">
                                    <CustomButtons>
                                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                            </Image>
                                        </dxtv:GridViewCommandColumnCustomButton>
                                        <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir">
                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                            </Image>
                                        </dxtv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="CodigoExpressao" Name="colCodigoExpressao" VisibleIndex="1" Width="40px">
                                    <PropertiesTextEdit Width="230px">
                                    </PropertiesTextEdit>
                                    <EditCellStyle HorizontalAlign="Left">
                                    </EditCellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="CodigoEtapaDestino" FieldName="CodigoEtapaDestino" Name="colCodigoEtapaDestino" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="ExpressaoAvaliada" FieldName="ExpressaoAvaliada" Name="colExpressaoAvaliada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Expressão condicional" FieldName="ExpressaoExtenso" Name="colExpressaoExtenso" VisibleIndex="2">
                                    <PropertiesTextEdit Width="150px">
                                    </PropertiesTextEdit>
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Etapa destino" FieldName="NomeEtapaDestino" Name="colNomeEtapaDestino" VisibleIndex="3" Width="200px">
                                    <PropertiesTextEdit Width="80px">
                                    </PropertiesTextEdit>
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                            <ClientSideEvents CustomButtonClick="function(s, e) {
                                var CodigoExpressao = s.GetRowKey(e.visibleIndex);
                                if (e.buttonID == 'btnEditar'){
                                    preparaTelaParaEditarCondicaoExistente(CodigoExpressao)
                                }
                                else if (e.buttonID == 'btnExcluir')
                                    excluiRegistro(CodigoExpressao);
}" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="PopupEditForm">
                            </SettingsEditing>
                            <SettingsPopup>
                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                    Width="350px" />
                                <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                            </SettingsPopup>
                            <Settings ShowTitlePanel="True" ShowGroupButtons="False" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="144" ShowStatusBar="Hidden"></Settings>
                            <SettingsText PopupEditFormCaption="Pessoas com Acesso &#224; Etapa" EmptyDataRow="Nenhum perfil cadastrado" Title="Condições"></SettingsText>
                            <SettingsCommandButton>
                                <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                    <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                </DeleteButton>
                                <EditButton Image-Url="~/imagens/botoes/editarReg02.png">
                                    <Image Url="~/imagens/botoes/editarReg02.png"></Image>
                                </EditButton>
                                <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                    <Image Url="~/imagens/botoes/salvar.png"></Image>
                                </UpdateButton>
                                <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                    <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                </CancelButton>
                            </SettingsCommandButton>
                        </dxwgv:ASPxGridView>
                        <div id="dvComandosEncerrar">
                            <table style="width: 100%">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="width: 100px">
                                        <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" Width="90px"  ClientInstanceName="btnOk" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
                                if (txtEtapaOrigemLabelOpcao.GetText()=='')
                                {
                                    window.top.mostraMensagem('Informe o nome da opção.', 'atencao', true, false, null);
                                    return;
                                }
                                else if (cmbEtapaDestinoPadrao.GetText()=='')
                                {
                                    window.top.mostraMensagem('Informe o nome da etapa de caminho alternativo.', 'atencao', true, false, null);
                                    return;
                                }
                                else if (arrayRegistrosGridExpressoes.length == 0)
                                {
                                    window.top.mostraMensagem('Inclua no mínimo uma condição que direcione o fluxo para uma etapa diferente da etapa do caminho alternativo (' + cmbEtapaDestinoPadrao.GetText() + ').', 'atencao', true, false, null);
                                    return;
                                }

                                if (salvaCaminhoCondicional())
                                    pcDvCaminhoCondicional.Hide();
}" />

                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width: 90px">
                                        <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" Width="90px"  ClientInstanceName="btnCancelar" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
                                                pcDvCaminhoCondicional.Hide();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="pcDesignerCondicoes" runat="server" Width="700px" ClientInstanceName="pcDesignerCondicoes" AllowDragging="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Expressão de caminho condicional">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <dxcp:ASPxMemo ID="txtEdidorCondicao" ClientInstanceName="txtEdidorCondicao" runat="server" Height="71px" Width="100%"></dxcp:ASPxMemo>
            <table style="width: 100%;display:none;">
                <tr>
                    <td>
                        <img src="../../imagens/Operadores/adicao.png" style="cursor: pointer" onclick="incluiCampoEditorCondicao(0, '+', 'OPER');" /></td>
                    <td>
                        <img src="../../imagens/Operadores/subtracao.png" style="cursor: pointer" onclick="incluiCampoEditorCondicao(0, '-', 'OPER');" /></td>
                    <td>
                        <img src="../../imagens/Operadores/multiplicacao.png" style="cursor: pointer" onclick="incluiCampoEditorCondicao(0, '*', 'OPER');" /></td>
                    <td>
                        <img src="../../imagens/Operadores/divisao.png" style="cursor: pointer" onclick="incluiCampoEditorCondicao(0, '/', 'OPER');" /></td>
                    <td>|</td>
                    <td>
                        <img src="../../imagens/Operadores/adicao.png" /></td>
                    <td>
                        <img src="../../imagens/Operadores/subtracao.png" /></td>
                    <td>
                        <img src="../../imagens/Operadores/multiplicacao.png" /></td>
                    <td>
                        <img src="../../imagens/Operadores/divisao.png" /></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>Formulário</td>
                    <td>Campos</td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxListBox ID="lstFormularios" ClientInstanceName="lstFormularios" runat="server" ValueType="System.String" Width="330px" OnCallback="lstFormularios_Callback" EnableSynchronization="True">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                lstCampos.PerformCallback(s.GetSelectedItem().value);
}"
                                EndCallback="function(s, e) {
    constroiDeParaCodigoCampo(s.cpDeParaCodigoCampo);
}" />
                        </dx:ASPxListBox>
                    </td>
                    <td>
                        <dx:ASPxListBox ID="lstCampos" ClientInstanceName="lstCampos" runat="server" ValueType="System.String" Width="330px" OnCallback="lstCampos_Callback" EnableSynchronization="True">
                            <ClientSideEvents ItemDoubleClick="function(s, e) {
	incluiCampoEditorCondicao(s.GetSelectedItem().value, s.GetSelectedItem().text, 'CAMPO');
}" />
                        </dx:ASPxListBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>Etapa destino:</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxComboBox ID="cmbEtapaDestinoDvCaminhoCondicional" ClientInstanceName="cmbEtapaDestinoDvCaminhoCondicional" runat="server" ValueType="System.String" Width="470px"></dxcp:ASPxComboBox>
                    </td>
                    <td style="width: 90px">
                        <dxcp:ASPxButton ID="btnSalvarCondicao" runat="server" Text="Salvar" Width="90px"  ClientInstanceName="btnCancelarCondicao" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                        // se não selecionou a etapa, sai sem fazer nada
                                        if (cmbEtapaDestinoDvCaminhoCondicional.GetSelectedIndex() == -1)
                                        {
                                            window.top.mostraMensagem('Selecione a etapa de destino.', 'atencao', true, false, null);
                                            return false;
                                        }

                                        if (!validaExpressaoCondicional())
                                            window.top.mostraMensagem('Expressão condicional não é válida!', 'atencao', true, false, null);
                                        else
                                            salvaExpressaoCondicional();
}" />
                        </dxcp:ASPxButton>
                    </td>
                    <td style="width: 90px">
                        <dxcp:ASPxButton ID="btnCancelarCondicao" runat="server" Text="Cancelar" Width="90px"  ClientInstanceName="btnCancelarCondicao" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                pcDesignerCondicoes.Hide();
}" />
                        </dxcp:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dxcp:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hf">
</dxcp:ASPxHiddenField>


<dxcp:ASPxCallback ID="callbackForms" runat="server" OnCallback="callbackForms_Callback">
    <ClientSideEvents EndCallback="function(s, e) {
	var valores = s.cp_Valores.split(';');
       
	for(i = 0; i &lt; valores.length; i++)
	{
		if(valores[i] != '')
	     lstFormularios.RemoveItem(lstFormularios.FindItemByValue(valores[i]).index);
	}
}" />
</dxcp:ASPxCallback>



