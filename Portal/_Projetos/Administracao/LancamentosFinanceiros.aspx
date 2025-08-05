<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="LancamentosFinanceiros.aspx.cs" Inherits="LancamentosFinanceiros" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div style="padding: 10px">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            Width="100%" OnCallback="pnCallback_Callback">
            <PanelCollection>
                <dxp:PanelContent runat="server">

                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoLancamentoFinanceiro"
                        AutoGenerateColumns="False" Width="100%" Font-Names="Verdana" Font-Size="8pt"
                        ID="gvDados" OnCustomColumnDisplayText="gvDados_CustomColumnDisplayText">
                        <ClientSideEvents
                            CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnEditar&quot;)
     {  
            gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro;IniciaisControleLancamento;', mostraPopupLancamentoFinanceiro);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
        var codigoLancamentoFinanceiro = gvDados.GetRowKey(e.visibleIndex);
        var valores = [codigoLancamentoFinanceiro, 'RO'];
        mostraPopupLancamentoFinanceiro(valores);        
     }	
}
"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td align="center">
                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                    ClientInstanceName="menu"
                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick">
                                                    <Paddings Padding="0px" />
                                                    <Items>
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
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="4" Width="65px"
                                FieldName="IndicaDespesaReceita" Name="colTipo">
                                <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Data Empenho" FieldName="DataEmpenho"
                                Name="colDataEmpenho" ShowInCustomizationForm="True" VisibleIndex="2"
                                Width="125px">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"
                                    ShowFilterRowMenu="True" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Emitente" FieldName="PessoaEmitente" ShowInCustomizationForm="True"
                                VisibleIndex="5" Name="colEmitente" Width="250px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" Name="NomeProjeto"
                                ShowInCustomizationForm="True" VisibleIndex="11" Width="290px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Doc. Fiscal" ShowInCustomizationForm="True"
                                VisibleIndex="9" Width="95px" FieldName="NumeroDocFiscal"
                                Name="colNumeroDocFiscal">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" Width="140px" Caption="Valor Empenhado(R$)"
                                VisibleIndex="8" FieldName="ValorEmpenhado" Name="colValorEmpenhado">
                                <PropertiesTextEdit DisplayFormatString="N2">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False"></Settings>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Previsão"
                                FieldName="DataPrevistaPagamentoRecebimento"
                                Name="colDataPrevistaPagamentoRecebimento" ShowInCustomizationForm="True"
                                VisibleIndex="3" Width="120px">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"
                                    ShowFilterRowMenu="True" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                </CellStyle>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Conta" FieldName="DescricaoConta" ShowInCustomizationForm="True"
                                VisibleIndex="6" Name="colConta" Width="250px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Partícipe" FieldName="NomeParticipe" ShowInCustomizationForm="True"
                                VisibleIndex="7" Name="colFontePagadora" Width="200px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxtv:GridViewDataDateColumn Caption="Vencimento" FieldName="DataVencimento" ShowInCustomizationForm="True" VisibleIndex="1" Width="125px">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxtv:GridViewDataDateColumn>
                            <dxtv:GridViewDataTextColumn Caption="Emissão" FieldName="DataEmissaoDocFiscal" ShowInCustomizationForm="True" VisibleIndex="10">
                            </dxtv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager AlwaysShowPager="True" PageSize="50">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                            ShowGroupPanel="True" HorizontalScrollBarMode="Visible"></Settings>
                        <SettingsCommandButton>
                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                        </SettingsCommandButton>
                        <StylesEditors>
                            <Style Font-Names="Verdana" Font-Size="8pt">
                                        </Style>
                            <Calendar Font-Names="Verdana" Font-Size="8pt">
                            </Calendar>
                            <CalendarDayHeader Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDayHeader>
                            <CalendarWeekNumber Font-Names="Verdana" Font-Size="8pt">
                            </CalendarWeekNumber>
                            <CalendarDay Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDay>
                            <CalendarDayOtherMonth Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDayOtherMonth>
                            <CalendarDaySelected Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDaySelected>
                            <CalendarDayWeekEnd Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDayWeekEnd>
                            <CalendarDayOutOfRange Font-Names="Verdana" Font-Size="8pt">
                            </CalendarDayOutOfRange>
                            <CalendarToday Font-Names="Verdana" Font-Size="8pt">
                            </CalendarToday>
                            <CalendarHeader Font-Names="Verdana" Font-Size="8pt">
                            </CalendarHeader>
                            <CalendarFooter Font-Names="Verdana" Font-Size="8pt">
                            </CalendarFooter>
                            <CalendarButton Font-Names="Verdana" Font-Size="8pt">
                            </CalendarButton>
                        </StylesEditors>
                    </dxwgv:ASPxGridView>
                </dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
    </div>



    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
        <Styles>
            <Header Font-Bold="True" Font-Names="Verdana" Font-Size="8pt">
            </Header>
            <Cell Font-Names="Verdana" Font-Size="8pt">
            </Cell>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
