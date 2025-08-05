<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_cfg_campoCalculado.ascx.cs" Inherits="formularios_UC_Formularios_uc_cfg_campoCalculado" %>

<dx:ASPxPopupControl ID="ppDvCampoCalculado" runat="server" ClientInstanceName="ppDvCampoCalculado" OnWindowCallback="ppDvCampoCalculado_WindowCallback" AllowDragging="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="900px">
    <ClientSideEvents EndCallback="function(s, e) {
        ppDvCampoCalculado.Show();	
}" />
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                <tbody>
                    <tr>
                        <td style="width: 500px"><dx:ASPxGridView runat="server" ClientInstanceName="gvd_CAL_CamposCalculaveis"
                                KeyFieldName="CodigoCampo" AutoGenerateColumns="False" 
                                ID="gvd_CAL_CamposCalculaveis" Width="100%" >
                            <SettingsBehavior EnableCustomizationWindow="False" AllowSort="False" />
                            <SettingsResizing  ColumnResizeMode="Control"/>    
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="False" />
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="CodigoCampo" Name="colCodigo" Caption="C&#243;digo"
                                        Visible="False" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Sequencia" Name="colSequencia" Width="45px" Caption="Linha" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NomeCampo" Name="colCampo"
                                        Caption="(A) - Campos" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ValorCampo" Name="colValorCampo" Width="70px"
                                        Caption="(B) - Valor" VisibleIndex="4">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtValorCampo" runat="server" ClientInstanceName="txtValorCampo"
                                                Width="60px" HorizontalAlign="Right">
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords" Visible="False" PageSize="20">
                                </SettingsPager>
                            </dx:ASPxGridView>
                        </td>
                        <td style="width: 25px"></td>
                        <td style="vertical-align: top">
                            <table style="width: 120px; height: 140px;">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel110" runat="server" 
                                            Text="Precisão decimal:">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="ddl_CAL_Precisao" runat="server" ClientInstanceName="ddl_CAL_Precisao"
                                             SelectedIndex="0" Width="100px">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="0" Value="0" />
                                                <dx:ListEditItem Text="1" Value="1" />
                                                <dx:ListEditItem Text="2" Value="2" />
                                                <dx:ListEditItem Text="3" Value="3" />
                                                <dx:ListEditItem Text="4" Value="4" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel111" runat="server" 
                                            Text="Formato:">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="ddl_CAL_Formato" runat="server" ClientInstanceName="ddl_CAL_Formato"
                                             SelectedIndex="0" Width="100px">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Número" Value="N" />
                                                <dx:ListEditItem Text="Moeda" Value="M" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel112" runat="server" 
                                            Text="Agregação:">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="ddl_CAL_Agregacao" runat="server" ClientInstanceName="ddl_CAL_Agregacao"
                                             SelectedIndex="0" Width="100px">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Nenhuma" Value="" />
                                                <dx:ListEditItem Text="Soma" Value="SOM" />
                                                <dx:ListEditItem Text="Média" Value="MED" />
                                                <dx:ListEditItem Text="Máximo" Value="MAX" />
                                                <dx:ListEditItem Text="Mínimo" Value="MIN" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            <table style="width: 251px">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" 
                                                Text="Fórmula:">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txt_CAL_Formula" runat="server" ClientInstanceName="txt_CAL_Formula"
                                                 MaxLength="50" Width="100%" ToolTip="Referenciar os campos no formato Bn onde &quot;n&quot; corresponde ao número da linha do campo na tabela de campos ao lado. Exemplo: (B1+B2)*100">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 65px">
                                                        <dx:ASPxButton ID="btnAvaliarFormula" runat="server" AutoPostBack="False" ClientInstanceName="btnAvaliarFormula"
                                                             Text="Avaliar" Width="60px">
                                                            <ClientSideEvents Click="function(s, e) {
    validarFormulaDigitada();
    }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td style="width: 70px; text-align: right;">
                                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" 
                                                            Text="Resultado:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_CAL_ResultadoFormula" runat="server" ClientInstanceName="txt_CAL_ResultadoFormula"
                                                             ReadOnly="True" Width="110px">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="dvComandosEncerrar">
                <table style="width: 100%">
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="btnSalvarCampoCalculado" runat="server" Text="Ok" Width="90px"  ClientInstanceName="btnSalvarCampoCalculado" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
                                if (txtEtapaOrigemLabelOpcao.GetText()=='')
                                {
                                    window.top.mostraMensagem('Informe o nome da opção.', 'atencao', true, false, null);
                                    return;
                                }
                                salvaCaminhoCondicional();
                                pcDvCaminhoCondicional.Hide();
}" />

                            </dx:ASPxButton>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxButton ID="btnCancelarCampoCalculado" runat="server" Text="Cancelar" Width="90px"  ClientInstanceName="btnCancelarCampoCalculado" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
                                                pcDvCaminhoCondicional.Hide();
}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dxhf:ASPxHiddenField ID="hf" runat="server" ClientInstanceName="hf">
                                        </dxhf:ASPxHiddenField>
