<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanoObjetivoEstrategico.aspx.cs" Inherits="_PlanosPluri_DadosPlano_PlanoObjetivoEstrategico" %>

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
</head>
<body>
    <form id="form1" runat="server">
<div>
    
        <div style="padding-left: 5px; padding-right: 5px; padding-top: 5px">
            <table>
                <tr>
                    <td>
            <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"  KeyFieldName="CodigoPlano;CodigoObjetoEstrategia;CodigoObjetivoEstrategicoPlanoConsolidador" Width="100%" OnCellEditorInitialize="gvDados_CellEditorInitialize" OnCustomErrorText="gvDados_CustomErrorText" OnRowDeleting="gvDados_RowDeleting" OnRowInserting="gvDados_RowInserting">
                <SettingsPopup>
                    <EditForm AllowResize="True" CloseOnEscape="False" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                </SettingsPopup>
                <Columns>
                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="100px" ShowDeleteButton="True">
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
                    <dxtv:GridViewDataTextColumn Caption="CodigoObjetoEstrategia" FieldName="CodigoObjetoEstrategia" Visible="False" VisibleIndex="2">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico" FieldName="ObjetivoEstrategico" VisibleIndex="4">
                        <PropertiesComboBox TextField="ObjetivoEstrategico" ValueField="CodigoObjetoEstrategia">
                            <ValidationSettings ErrorText="*">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <Settings AutoFilterCondition="Contains" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico do Plano Consolidador" FieldName="ObjetivoEstrategicoPlanoConsolidador" VisibleIndex="5">
                        <PropertiesComboBox TextField="ObjetivoEstrategicoPlanoConsolidador" ValueField="CodigoObjetivoEstrategicoPlanoConsolidador">
                            <ValidationSettings ErrorText="*">
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <Settings AutoFilterCondition="Contains" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoObjetivoEstrategicoPlanoConsolidador" FieldName="CodigoObjetivoEstrategicoPlanoConsolidador" Visible="False" VisibleIndex="3">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="CodigoPlano" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoObjetivoEstrategicoPlanoConsolidador" FieldName="CodigoObjetivoEstrategicoPlanoConsolidador" Visible="False" VisibleIndex="6">
                    </dxtv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True" />
                <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
if(s.cp_Erros != null  &amp;&amp;  s.cp_Erros != &quot;&quot; &amp;&amp;  s.cp_Erros != undefined)
{
window.top.mostraMensagem(s.cp_Erros, 'erro', true, false, null);
   s.cp_Erros = '';
}
else
{
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
                gvDados.SetHeight(window.innerHeight - 30);
            </script>
        </div>
    </div>
    </form>
</body>
</html>
