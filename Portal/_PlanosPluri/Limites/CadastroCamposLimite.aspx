<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroCamposLimite.aspx.cs" Inherits="_PlanosPluri_Limites_CadastroCamposLimite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text="Cadastro de Campos de Limites de Planos"
                    EnableViewState="False"></asp:Label>
            </td>
            <td align="left" valign="middle"></td>
        </tr>
    </table>
    <dxcp:ASPxGridView ID="gvCampoLimite" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvCampoLimite" DataSourceID="dataSource" KeyFieldName="CodigoCampo" OnRowValidating="gvCampoLimite_RowValidating" Width="100%">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
        </SettingsEditing>
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="400px" />
        </SettingsPopup>
        <EditFormLayoutProperties>
            <Items>
                <dxtv:GridViewColumnLayoutItem ColumnName="NomeCampo">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="SiglaCampo">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="FormulaCampo" HelpText="Instrução SQL para trazer o valor do campo">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dxtv:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="60px">
            </dxtv:GridViewCommandColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoCampo" ReadOnly="True" Visible="False" VisibleIndex="1">
                <EditFormSettings Visible="False" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Campo" FieldName="NomeCampo" VisibleIndex="2">
                <PropertiesTextEdit MaxLength="150">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings CaptionLocation="Top" VisibleIndex="0" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Sigla" FieldName="SiglaCampo" VisibleIndex="3">
                <PropertiesTextEdit MaxLength="25">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings CaptionLocation="Top" VisibleIndex="1" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataMemoColumn Caption="Fórmula" FieldName="FormulaCampo" Visible="False" VisibleIndex="4">
                <PropertiesMemoEdit Rows="5" MaxLength="8000">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesMemoEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
            </dxtv:GridViewDataMemoColumn>
        </Columns>
    </dxcp:ASPxGridView>
    <asp:SqlDataSource ID="dataSource" runat="server" DeleteCommand="DELETE FROM [CampoLimite] WHERE [CodigoCampo] = @CodigoCampo" InsertCommand="INSERT INTO [CampoLimite] ([NomeCampo], [SiglaCampo], [FormulaCampo]) VALUES (@NomeCampo, @SiglaCampo, @FormulaCampo)" SelectCommand="SELECT * FROM [CampoLimite] ORDER BY [NomeCampo]" UpdateCommand="UPDATE [CampoLimite] SET [NomeCampo] = @NomeCampo, [SiglaCampo] = @SiglaCampo, [FormulaCampo] = @FormulaCampo WHERE [CodigoCampo] = @CodigoCampo">
        <DeleteParameters>
            <asp:Parameter Name="CodigoCampo" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NomeCampo" Type="String" />
            <asp:Parameter Name="SiglaCampo" Type="String" />
            <asp:Parameter Name="FormulaCampo" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="NomeCampo" Type="String" />
            <asp:Parameter Name="SiglaCampo" Type="String" />
            <asp:Parameter Name="FormulaCampo" Type="String" />
            <asp:Parameter Name="CodigoCampo" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

