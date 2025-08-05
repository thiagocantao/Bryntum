<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManutencaoPlanoPeriodo.aspx.cs" Inherits="_PlanosPluri_DadosPlano_ManutencaoPlanoPeriodo" %>

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
        
        <div style="padding-left: 5px; padding-right: 5px; padding-top: 5px">

            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPlano;Ano" AutoGenerateColumns="False" Width="100%"  ID="gvDados" DataSourceID="sdsGrid" OnRowDeleting="gvDados_RowDeleting" OnRowInserting="gvDados_RowInserting" OnRowUpdating="gvDados_RowUpdating" OnCommandButtonInitialize="gvDados_CommandButtonInitialize">
                <SettingsPopup>
                    <EditForm AllowResize="True" CloseOnEscape="False" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                </SettingsPopup>
                <Columns>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="100px" ShowDeleteButton="True" ShowEditButton="True">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td align="center">
                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                            ClientInstanceName="menu"
                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                            OnInit="menu_Init">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                    <Items>
                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                            ClientVisible="False">
                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                    </Items>
                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                    <Items>
                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
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
                                        </dxm:ASPxMenu>
                                    </td>
                                </tr>
                            </table>

                        </HeaderTemplate>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CodigoPlano" VisibleIndex="1" Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn FieldName="Ano" ShowInCustomizationForm="True" VisibleIndex="2" Width="130px">
                        <PropertiesSpinEdit DisplayFormatString="g" MaxLength="4" MaxValue="2999" MinValue="1901" NumberType="Integer">
                            <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                            </SpinButtons>
                            <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text" ErrorText="*" SetFocusOnError="True">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                        <EditFormSettings Caption="Ano" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="0" />
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataCheckColumn Caption="Ativo" FieldName="IndicaAnoAtivo" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextGrayed="Todos" DisplayTextUnchecked="Não" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                        </PropertiesCheckEdit>
                        <EditFormSettings Caption="Ativo?" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="False" VisibleIndex="1" />
                    </dxtv:GridViewDataCheckColumn>
                    <dxtv:GridViewDataCheckColumn Caption="Meta Editável" FieldName="IndicaMetaEditavel" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextGrayed="Todos" DisplayTextUnchecked="Não" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                        </PropertiesCheckEdit>
                        <EditFormSettings Caption="Meta Editável?" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="False" VisibleIndex="2" />
                    </dxtv:GridViewDataCheckColumn>
                    <dxtv:GridViewDataCheckColumn Caption="Resultado Editável" FieldName="IndicaResultadoEditavel" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextGrayed="Todos" DisplayTextUnchecked="Não" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                        </PropertiesCheckEdit>
                        <EditFormSettings Caption="Resultado Editável" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="False" VisibleIndex="4" />
                    </dxtv:GridViewDataCheckColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Periodicidade Cenário de Recursos" FieldName="TipoDetalheCenario" ShowInCustomizationForm="True" VisibleIndex="9" Visible="False">
                        <PropertiesComboBox>
                            <Items>
                                <dxtv:ListEditItem Text="Anual" Value="A" />
                                <dxtv:ListEditItem Text="Por Período" Value="P" />
                            </Items>
                            <ValidationSettings CausesValidation="True" Display="None" ErrorDisplayMode="Text" ErrorText="*">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Periodicidade Cenário de Recursos" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="6" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Periodicidade Orçamento" FieldName="TipoDetalheOrcamento" ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                        <PropertiesComboBox>
                            <Items>
                                <dxtv:ListEditItem Text="Anual" Value="A" />
                                <dxtv:ListEditItem Text="Por Período" Value="P" />
                            </Items>
                            <ValidationSettings CausesValidation="True" Display="None" ErrorDisplayMode="Text" ErrorText="*">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Periodicidade Orçamento" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="7" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Cenário de Recursos Editável?" FieldName="IndicaCenarioEditavel" VisibleIndex="5">
                        <PropertiesComboBox EnableFocusedStyle="False">
                            <Items>
                                <dxtv:ListEditItem Text="Sim" Value="S" />
                                <dxtv:ListEditItem Text="Não" Value="N" />
                            </Items>
                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="*">
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Cenário de Recursos Editável?" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="3" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Orçamento Editável?" FieldName="IndicaOrcamentoEditavel" VisibleIndex="8">
                        <PropertiesComboBox EnableFocusedStyle="False">
                            <Items>
                                <dxtv:ListEditItem Text="Sim" Value="S" />
                                <dxtv:ListEditItem Text="Não" Value="N" />
                            </Items>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Orçamento Editável?" CaptionLocation="Top" ColumnSpan="1" RowSpan="1" Visible="True" VisibleIndex="5" />
                    </dxtv:GridViewDataComboBoxColumn>
                </Columns>

                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>

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

}" />

                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm">
                </SettingsEditing>

                <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>

                <SettingsText ConfirmDelete="Deseja Excluir o registro?"></SettingsText>
            </dxwgv:ASPxGridView>

            
            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="" align="center"></td>
                                    <td style="width: 70px" align="center" rowspan="3">
                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao" ></dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <asp:SqlDataSource ID="sdsGrid" runat="server" DeleteCommand="DELETE FROM [PlanoPeriodo]
      WHERE CodigoPlano = @CodigoPlano 
and  Ano = @Ano
"
                InsertCommand="INSERT INTO [PlanoPeriodo]
           ([CodigoPlano]
           ,[Ano]
           ,[IndicaCenarioEditavel]
           ,[IndicaOrcamentoEditavel]
           ,[TipoDetalheCenario]
           ,[TipoDetalheOrcamento])
     VALUES
           (@CodigoPlano
           ,@Ano
           ,@IndicaCenarioEditavel
           ,@IndicaOrcamentoEditavel
           ,@TipoDetalheCenario
           ,@TipoDetalheOrcamento)"
                SelectCommand="SELECT [CodigoPlano]
      ,[Ano]
      ,[IndicaAnoAtivo]
      ,[IndicaMetaEditavel]
      ,[IndicaCenarioEditavel]
      ,[IndicaResultadoEditavel]
      ,[IndicaOrcamentoEditavel]
      ,[TipoDetalheMetaResultado]
      ,[TipoDetalheCenario]
      ,[TipoDetalheOrcamento]
  FROM [PlanoPeriodo] where CodigoPlano = @CodigoPlano"
                UpdateCommand="UPDATE [PlanoPeriodo]
   SET [IndicaCenarioEditavel] = @IndicaCenarioEditavel
      ,[IndicaOrcamentoEditavel] = @IndicaOrcamentoEditavel
      ,[TipoDetalheCenario] = @TipoDetalheCenario
      ,[TipoDetalheOrcamento] = @TipoDetalheOrcamento
 WHERE CodigoPlano = @CodigoPlano and Ano = @Ano">
                <DeleteParameters>
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                    <asp:Parameter Name="Ano" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                    <asp:Parameter Name="Ano" />
                    <asp:Parameter Name="IndicaCenarioEditavel" />
                    <asp:Parameter Name="IndicaOrcamentoEditavel" />
                    <asp:Parameter Name="TipoDetalheCenario" />
                    <asp:Parameter Name="TipoDetalheOrcamento" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Ano" />
                    <asp:Parameter Name="IndicaCenarioEditavel" />
                    <asp:Parameter Name="IndicaOrcamentoEditavel" />
                    <asp:Parameter Name="TipoDetalheCenario" />
                    <asp:Parameter Name="TipoDetalheOrcamento" />
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="CodigoPlano" QueryStringField="CP" />
                </SelectParameters>
            </asp:SqlDataSource>
            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4">
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
            </dxwgv:ASPxGridViewExporter>
            <script type="text/javascript" language="javascript">
                gvDados.SetHeight(window.innerHeight - 30);
            </script>
        </div>
    </form>
</body>
</html>
