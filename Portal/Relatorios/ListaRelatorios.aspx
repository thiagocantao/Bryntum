<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaRelatorios.aspx.cs" Inherits="Relatorios_ListaRelatorios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function onCustomButtonClick(s, e) {
            if (e.buttonID == 'design') {
                var key = gvRelatorios.GetRowKey(e.visibleIndex);
                window.location = 'DesenhoRelatorio.aspx?id=' + key;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dxcp:ASPxGridView ID="gvRelatorios" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvRelatorios" DataSourceID="dataSource" KeyFieldName="IDRelatorio" OnRowInserting="gvRelatorios_RowInserting" Width="100%">
            <ClientSideEvents CustomButtonClick="onCustomButtonClick" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
            </SettingsEditing>
            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" />
            <SettingsCommandButton>
                <NewButton>
                    <Image ToolTip="Novo" Url="~/imagens/botoes/novoReg.PNG">
                    </Image>
                </NewButton>
                <UpdateButton>
                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                    </Image>
                </UpdateButton>
                <CancelButton>
                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                    </Image>
                </CancelButton>
                <EditButton>
                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                    </Image>
                </EditButton>
                <DeleteButton>
                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                    </Image>
                </DeleteButton>
            </SettingsCommandButton>
            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="400px" />
            </SettingsPopup>
            <Columns>
                <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="100px" ButtonRenderMode="Image">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton Text="Design" ID="design">
                            <Image IconID="miscellaneous_design_16x16" ToolTip="Design">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxtv:GridViewCommandColumn>
                <dxtv:GridViewDataTextColumn FieldName="IDRelatorio" ReadOnly="True" Visible="False" VisibleIndex="1">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="TituloRelatorio" VisibleIndex="2" Caption="Relatório">
                    <PropertiesTextEdit MaxLength="250">
                        <ValidationSettings Display="Dynamic">
                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                    <EditFormSettings CaptionLocation="Top" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="IniciaisControle" Visible="False" VisibleIndex="3">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataComboBoxColumn FieldName="TipoAssociacao" Visible="False" VisibleIndex="4">
                    <PropertiesComboBox>
                        <Items>
                            <dxtv:ListEditItem Text="PR" Value="PR" />
                            <dxtv:ListEditItem Text="EN" Value="EN" />
                            <dxtv:ListEditItem Text="IN" Value="IN" />
                            <dxtv:ListEditItem Text="RD" Value="RD" />
                            <dxtv:ListEditItem Text="RE" Value="RE" />
                            <dxtv:ListEditItem Text="OB" Value="OB" />
                        </Items>
                    </PropertiesComboBox>
                    <EditFormSettings CaptionLocation="Top" />
                </dxtv:GridViewDataComboBoxColumn>
            </Columns>
        </dxcp:ASPxGridView>
        <asp:SqlDataSource ID="dataSource" runat="server" DeleteCommand="DELETE FROM [ModeloRelatorio] WHERE [IDRelatorio] = @IDRelatorio" InsertCommand="INSERT INTO [ModeloRelatorio] ([IDRelatorio], [TituloRelatorio], [TipoAssociacao]) VALUES (@IDRelatorio, @TituloRelatorio, 'RD')" SelectCommand="SELECT [IDRelatorio], [TituloRelatorio], [IniciaisControle], [TipoAssociacao] FROM [ModeloRelatorio] ORDER BY [TituloRelatorio]" UpdateCommand="UPDATE [ModeloRelatorio] SET [TituloRelatorio] = @TituloRelatorio WHERE [IDRelatorio] = @IDRelatorio">
            <DeleteParameters>
                <asp:Parameter Name="IDRelatorio" Type="Object" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="IDRelatorio" />
                <asp:Parameter Name="TituloRelatorio" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="TituloRelatorio" />
                <asp:Parameter Name="IDRelatorio" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
