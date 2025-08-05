<%@ Page Language="C#" AutoEventWireup="true" CodeFile="succ_Projetos.aspx.cs" Inherits="_Projetos_DadosProjeto_succ_Projetos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
   </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divGrid" style="padding-left: 5px; padding-right: 15px; padding-top: 5px; padding-bottom: 5px">
                <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoInstrumentoJuridico" AutoGenerateColumns="False" Width="100%" ID="gvDados" EnableViewState="False">
                    <ClientSideEvents
                        CustomButtonClick="function(s, e) 
{
      if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {
           s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoInstrumentoJuridico;NumeroInstrumentoJuridico', abrePopupInstrumentosJuridicos);
     }
      else if(e.buttonID == 'btnPermissoesCustom')
      {
                s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoContratoPortal', abrePopupPermissoes);
       }
}"></ClientSideEvents>
                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarStyle="Virtual" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible"></Settings>
                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False"></SettingsBehavior>
                    <Columns>
                        <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="80px" Caption="#" VisibleIndex="0">
                            <CustomButtons>
                                <dxcp:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                    <Image Url="~/imagens/botoes/pFormulario.PNG">
                                    </Image>
                                </dxcp:GridViewCommandColumnCustomButton>
                                <dxcp:GridViewCommandColumnCustomButton ID="btnPermissoesCustom" Text="Visualizar Interessados">
                                    <Image Url="~/imagens/Perfis/Perfil_Permissoes.png">
                                    </Image>
                                </dxcp:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
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
                                                        <border borderstyle="None" />
                                                    </HoverStyle>
                                                    <Paddings Padding="0px" />
                                                </ItemStyle>
                                                <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                    <SelectedStyle>
                                                        <border borderstyle="None" />
                                                    </SelectedStyle>
                                                </SubMenuItemStyle>
                                                <Border BorderStyle="None" />
                                            </dxm:ASPxMenu>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </dxcp:GridViewCommandColumn>
                        <dxcp:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Codigo" VisibleIndex="1" FieldName="CodigoInstrumentoJuridico" Visible="False">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxcp:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Instrumento Jurídico" VisibleIndex="3" FieldName="NumeroInstrumentoJuridico" Width="175px">
                            <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True" FilterMode="DisplayText"  ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" />
                            <HeaderStyle Wrap="True" />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Fornecedor" VisibleIndex="4" FieldName="NomeFornecedor" Width="250px">
                            <Settings ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="True" ShowInFilterControl="True" AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Valor Contrato" VisibleIndex="7" FieldName="ValorInstrumentoJuridico" Width="170px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="8" FieldName="TipoContrato" Width="250px">
                            <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Natureza" VisibleIndex="9" FieldName="DescricaoNatureza" Width="160px">
                            <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Codigo Contrato Portal" FieldName="CodigoContratoPortal" VisibleIndex="2" Visible="False">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Gestor" FieldName="NomeGestorInstrumentoJuridico" VisibleIndex="10" Width="250px">
                            <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataDateColumn Caption="Início Vigência" FieldName="DataInicioVigencia" VisibleIndex="5" Width="130px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesDateEdit>
                            <Settings ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowInFilterControl="True" />
                            <HeaderStyle Wrap="True" />
                        </dxtv:GridViewDataDateColumn>
                        <dxtv:GridViewDataDateColumn Caption="Término Vigência" FieldName="DataTerminoVigencia" VisibleIndex="6" Width="130px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesDateEdit>
                            <Settings ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowInFilterControl="True" />
                            <HeaderStyle Wrap="True" />
                        </dxtv:GridViewDataDateColumn>
                    </Columns>
                    <Styles>
                        <CommandColumnItem>
                            <Paddings PaddingLeft="2px" PaddingRight="2px"></Paddings>
                        </CommandColumnItem>
                    </Styles>
                </dxcp:ASPxGridView>
            </div>

            <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
            </dxcp:ASPxHiddenField>
            <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
                <Styles>
                    <Default>
                    </Default>
                    <Header Font-Bold="True">
                    </Header>
                    <Cell>
                    </Cell>
                    <GroupFooter Font-Bold="True">
                    </GroupFooter>
                    <GroupRow Font-Bold="True">
                    </GroupRow>
                    <Title Font-Bold="True"></Title>
                </Styles>
            </dxcp:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
