<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="RelatorioSOE_senar.aspx.cs" Inherits="_Projetos_DadosProjeto_RelatorioSOE_senar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relatório SOE</title>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .style2 {
            width: 100%;
        }

        .style7 {
            height: 5px;
        }

        .style8 {
            height: 21px;
        }

        .style9 {
            height: 11px;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <table style="width:100%">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="padding-left: 5px" valign="bottom">
                                <dxcp:ASPxLabel ID="ASPxLabel3" runat="server" Text="Início:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td valign="bottom">
                                <dxcp:ASPxLabel ID="ASPxLabel4" runat="server" Text="Término:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td valign="bottom">
                                <dxcp:ASPxLabel ID="ASPxLabel5" runat="server" Text="Origem:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td valign="bottom">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px; padding-right: 5px; width: 120px">
                                <dxcp:ASPxDateEdit ID="dtInicio" runat="server" ClientInstanceName="dtInicio" Width="100%">
                                </dxcp:ASPxDateEdit>
                            </td>
                            <td style="width: 120px; padding-right: 5px">
                                <dxcp:ASPxDateEdit ID="dtTermino" runat="server" ClientInstanceName="dtTermino" Width="100%">
                                </dxcp:ASPxDateEdit>
                            </td>
                            <td>
                                <dxcp:ASPxComboBox ID="ddlOrigem" runat="server" ClientInstanceName="ddlOrigem" Width="100%">
                                </dxcp:ASPxComboBox>
                            </td>
                            <td style="width: 100px">
                                <dxcp:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar" ClientInstanceName="btnSelecionar" AutoPostBack="False" Width="100%">
                                    <ClientSideEvents Click="function(s, e) {
               e.processOnServer = false;
               var mensagemErro = validaCamposFormulario(); 
                if(mensagemErro == &quot;&quot;)
               {
                        gvDados.PerformCallback();
               }
               else
               {
                       window.top.mostraMensagem(mensagemErro, 'atencao', true, false, null);
               }
}" />
                                </dxcp:ASPxButton>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-right: 10px">
                    <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="IdItem" AutoGenerateColumns="False" ID="gvDados" OnCustomCallback="gvDados_CustomCallback" Width="100%">
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <Settings ShowGroupPanel="True" ShowGroupButtons="False" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                        <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True"></SettingsBehavior>
                        <SettingsPopup>
                            <EditForm Width="600px" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" AllowResize="True" Modal="True"></EditForm>
                        </SettingsPopup>
                        <SettingsLoadingPanel Text="" ShowImage="False"></SettingsLoadingPanel>
                        <Columns>
                            <dxcp:GridViewDataTextColumn FieldName="IdItem" VisibleIndex="1" Caption="Item Nº" Width="70px">
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="Fornecedor" VisibleIndex="3" Caption="Nome do fornecedor/firma/consultor ou beneficiário" Width="370px">
                            </dxcp:GridViewDataTextColumn>
                            <dxcp:GridViewDataTextColumn FieldName="TipoGasto" VisibleIndex="4" Caption="Tipo de gasto" Width="200px">
                            </dxcp:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="NumeroContrato" VisibleIndex="5" Caption="Número do Contrato" Width="120px">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="RevisaoPrevia" VisibleIndex="6" Caption="Contrato Submetido a Revisão Prévia (sim/não)" Width="225px">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="MoedaContrato" VisibleIndex="7" Caption="Moeda do Contrato" Width="100px">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="ValorTotalContrato" VisibleIndex="8" Caption="Valor Total do Contrato" Width="170px">
                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="ValorPagoAcumulado" VisibleIndex="9" Caption="Valor pago acumulado do contrato" Width="190px">
                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="NumeroNF" VisibleIndex="10" Caption="Número da Fatura, Nota Fiscal ou Recibo" Width="150px">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="DataPagamento" VisibleIndex="11" Caption="Data do Pagamento" Width="170px">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="ValorPagamento" VisibleIndex="12" Caption="Valor total do pagamento incluído nesse SOE" Width="180px">
                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="PercentualBIRD" VisibleIndex="13" Caption="% Financiado pelo  BIRD" Width="220px">
                                <PropertiesTextEdit DisplayFormatString="{0:p2}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="ValorFinanciadoBanco" VisibleIndex="14" Caption="Valor Financiado pelo Banco Mundial (10x11)" Width="120px">
                                <PropertiesTextEdit DisplayFormatString="{0:c2}">
                                </PropertiesTextEdit>
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn FieldName="Observacoes" VisibleIndex="15" Caption="Observações" Width="100px">
                            </dxtv:GridViewDataTextColumn>
                            <dxtv:GridViewCommandColumn VisibleIndex="0" Width="30px">
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
                            </dxtv:GridViewCommandColumn>
                            <dxtv:GridViewDataTextColumn Caption="Origem" FieldName="Origem" VisibleIndex="2" Width="100px">
                                <Settings AllowAutoFilterTextInputTimer="False" AllowHeaderFilter="True"  ShowFilterRowMenu="False" />
                            </dxtv:GridViewDataTextColumn>
                        </Columns>
                        <TotalSummary>
                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorPagamento" ShowInColumn="Valor total do pagamento incluído nesse SOE" ShowInGroupFooterColumn="Valor total do pagamento incluído nesse SOE" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                            <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="ValorFinanciadoBanco" ShowInColumn="Valor Financiado pelo Banco Mundial (10x11)" ShowInGroupFooterColumn="Valor Financiado pelo Banco Mundial (10x11)" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
                        </TotalSummary>
                        <Styles>
                            <Header Wrap="True">
                            </Header>
                        </Styles>
                    </dxcp:ASPxGridView>
                </td>
            </tr>
        </table>

        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50" Landscape="True" ExportEmptyDetailGrid="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default></Default>

                <Header></Header>

                <Cell></Cell>

                <Footer></Footer>

                <GroupFooter></GroupFooter>

                <GroupRow></GroupRow>

                <Title></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>

    </form>
</body>
</html>



