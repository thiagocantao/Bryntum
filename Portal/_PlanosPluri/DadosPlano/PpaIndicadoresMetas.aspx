<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PpaIndicadoresMetas.aspx.cs" Inherits="_PlanosPluri_DadosPlano_PpaIndicadoresMetas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var comando;
        function mostraMensagemOperacao(acao) {
            lblAcaoGravacao.SetText(acao);
            pcUsuarioIncluido.Show();
            setTimeout('fechaTelaEdicao();', 2500);
        }

        function fechaTelaEdicao() {
            pcUsuarioIncluido.Hide();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div style="padding-left: 5px; padding-right: 5px; padding-top: 5px">
            <table cellpadding="0" cellspacing="0" class="auto-style1">
                <tr>
                    <td>
            <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"  KeyFieldName="CodigoIndicador" Width="100%" OnCustomCallback="gvDados_CustomCallback">
                <SettingsPopup>
                    <EditForm AllowResize="True" CloseOnEscape="False" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Height="500px" />
                </SettingsPopup>
                <Columns>
                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="70px" SelectAllCheckboxMode="Page" ShowSelectCheckbox="True">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td align="center">
                                        <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                    <Items>
                                                        <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                    </Items>
                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                    <Items>
                                                        <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                            <Image IconID="save_save_16x16">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                            <Image IconID="actions_reset_16x16">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                    </Items>
                                                    <Image Url="~/imagens/botoes/layout.png">
                                                    </Image>
                                                </dxtv:MenuItem>
                                            </Items>
                                            <ItemStyle Cursor="pointer">
                                            <HoverStyle>
                                                <Border BorderStyle="None" />
                                            </HoverStyle>
                                            <Paddings Padding="0px" />
                                            </ItemStyle>
                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                <SelectedStyle>
                                                    <Border BorderStyle="None" />
                                                </SelectedStyle>
                                            </SubMenuItemStyle>
                                            <Border BorderStyle="None" />
                                        </dxtv:ASPxMenu>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoIndicador" FieldName="CodigoIndicador" Visible="False" VisibleIndex="3">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Indicador Associado" FieldName="NomeIndicador" VisibleIndex="4">
                        <PropertiesTextEdit DisplayFormatString="g" MaxLength="4">
                            <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text" ErrorText="*" SetFocusOnError="True">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Caption="CodigoPlano" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="False" VisibleIndex="0" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Meta Associada" FieldName="Meta" VisibleIndex="5">
                        <PropertiesTextEdit EnableFocusedStyle="False" Height="180px">
                        </PropertiesTextEdit>
                        <EditFormSettings Caption="Prioridade Local:" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="1" />
                    </dxtv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True" />
                <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
if(comando == &quot;UPDATEEDIT&quot;)
{
mostraMensagemOperacao(&quot;Dados atualizados com sucesso!&quot;);
}
else if (comando == &quot;DELETEROW&quot;)
{
mostraMensagemOperacao(&quot;Dados excluídos com sucesso!&quot;);
}
else if (comando == &quot;CUSTOMCALLBACK&quot;)
{
mostraMensagemOperacao(&quot;Dados atualizados com sucesso!&quot;);
}

}" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsText ConfirmDelete="Deseja Excluir o registro?" />
            </dxtv:ASPxGridView>
                                                    </td>
                </tr>
                <tr>
                    <td>
                                                    <table align="right" cellpadding="0" style="padding-top: 5px; padding-right: 5px">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"  Text="Salvar" Width="120px">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    gvDados.PerformCallback(&quot;Salvar&quot;);
}" />
                                        </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                    </td>
                </tr>
            </table>
            <dxtv:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"  HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="center" style=""></td>
                                    <td align="center" rowspan="3" style="width: 70px">
                                        <dxtv:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                        </dxtv:ASPxImage>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dxtv:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao" >
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:ASPxPopupControl>
            <asp:SqlDataSource ID="sdsPlanoIndicador" runat="server" DeleteCommand="DELETE FROM [PlanoIndicador]
      WHERE (CodigoPlano = @CodigoPlano)

" InsertCommand="INSERT INTO [PlanoIndicador]
           ([CodigoPlano]
           ,[CodigoIndicador])
     VALUES
           (@CodigoPlano
           ,@CodigoIndicador)">
                <DeleteParameters>
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                    <asp:Parameter Name="CodigoIndicador" />
                </InsertParameters>
            </asp:SqlDataSource>
            <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxtv:ASPxHiddenField>
            <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4">
                <Styles>
                    <Default >
                    </Default>
                    <Header >
                    </Header>
                    <Cell >
                    </Cell>
                    <GroupFooter Font-Bold="True" >
                    </GroupFooter>
                    <Title Font-Bold="True" ></Title>
                </Styles>
            </dxtv:ASPxGridViewExporter>
            <script type="text/javascript" language="javascript">
                gvDados.SetHeight(window.innerHeight - 60);
            </script>
        </div>
    </div>
    </form>
</body>
</html>
