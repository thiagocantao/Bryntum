<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master"  AutoEventWireup="true" CodeFile="succ_MenuPrincipal.aspx.cs" Inherits="administracao_succ_MenuPrincipal" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divGrid" style="padding-left:5px;padding-right:15px;padding-top:5px;padding-bottom:5px">
        <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoInstrumentoJuridico" AutoGenerateColumns="False" Width="100%"  ID="gvDados" EnableViewState="False">
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
        <SettingsBehavior  AllowFocusedRow="True" AllowSort="False"></SettingsBehavior>
        <Columns>
            
            <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="80px" Caption="#" VisibleIndex="0" FixedStyle="Left">
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
            </dxcp:GridViewCommandColumn>
            <dxcp:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Codigo" VisibleIndex="1" FieldName="CodigoInstrumentoJuridico" Visible="False">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dxcp:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Instrumento Jurídico" VisibleIndex="3" FieldName="NumeroInstrumentoJuridico" Width="305px">
                <SettingsHeaderFilter>
                    
                </SettingsHeaderFilter>
                <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True" FilterMode="DisplayText"  ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" />
                <HeaderStyle Wrap="True" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Fornecedor" VisibleIndex="4" FieldName="NomeFornecedor" Width="300px">
                <Settings ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="True" ShowInFilterControl="True" AutoFilterCondition="Contains" AllowHeaderFilter="True"/>
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Valor Contrato" VisibleIndex="7" FieldName="ValorInstrumentoJuridico" Width="200px">
                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                </PropertiesTextEdit>
                <Settings AllowAutoFilter="False" />
                <HeaderStyle HorizontalAlign="Right" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataCheckColumn Caption="Tipo" VisibleIndex="8" FieldName="TipoContrato" Width="280px">
                <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"    />
            </dxtv:GridViewDataCheckColumn>
            <dxtv:GridViewDataTextColumn Caption="Natureza" VisibleIndex="9" FieldName="DescricaoNatureza" Width="160px">
                <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Codigo Contrato Portal" FieldName="CodigoContratoPortal" VisibleIndex="2" Visible="False">
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Gestor" FieldName="NomeGestorInstrumentoJuridico" VisibleIndex="10" Width="250px">
                <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True"  />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataDateColumn Caption="Início Vigência" FieldName="DataInicioVigencia" VisibleIndex="5" Width="150px">
                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                </PropertiesDateEdit>
                <Settings ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowInFilterControl="True" />
                <HeaderStyle Wrap="True" />
            </dxtv:GridViewDataDateColumn>
            <dxtv:GridViewDataDateColumn Caption="Término Vigência" FieldName="DataTerminoVigencia" VisibleIndex="6" Width="150px">
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
            <Default >
            </Default>
            <Header Font-Bold="True" >
            </Header>
            <Cell >
            </Cell>
            <GroupFooter Font-Bold="True" >
            </GroupFooter>
            <GroupRow Font-Bold="True" >
            </GroupRow>
            <Title Font-Bold="True" ></Title>
        </Styles>
    </dxcp:ASPxGridViewExporter>

</asp:Content>

