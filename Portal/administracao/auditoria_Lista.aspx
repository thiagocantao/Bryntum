<%@ Page MasterPageFile="~/novaCdis.master" Language="C#" AutoEventWireup="true" CodeFile="auditoria_Lista.aspx.cs" Inherits="administracao_auditoria" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 10px;
        }

        .style3 {
            width: 10px;
            height: 5px;
        }
    </style>
    <table cellspacing="1" class="style1">
        <tr>
            <td class="style3">
                </td>
            <td style="padding-top: 10px; padding-bottom: 5px">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Tabela:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="padding-left: 5px; width: 175px;">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Operação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxLabel id="lblTitulo0" runat="server" Font-Bold="False"
                                Text="Palavra-chave:"></dxe:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 230px">
                <dxe:ASPxComboBox ID="ddlTabela" runat="server" ClientInstanceName="ddlTabela" 
                    ValueType="System.String"  
                                Width="100%">
                </dxe:ASPxComboBox>
                        </td>
                        <td style="padding-left: 5px; width: 175px">
                <dxe:ASPxComboBox ID="ddlOperacao" runat="server" ClientInstanceName="ddlOperacao" 
                                 SelectedIndex="0" 
                                Width="100%">
                    <Items>
                        <dxe:ListEditItem Selected="True" Text="Atualização" Value="U" />
                        <dxe:ListEditItem Text="Inclusão" Value="I" />
                        <dxe:ListEditItem Text="Exclusão" Value="E" />
                    </Items>
                </dxe:ASPxComboBox>
                        </td>
                        <td style="padding-left: 5px">
                            <dxe:ASPxTextBox ID="txtFiltroAlteracao" runat="server" 
                                Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="width: 100px; padding-left: 5px">
                            <dxe:ASPxButton ID="ASPxButton3" runat="server" 
                                Text="Buscar" Width="100%">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
    
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1">
<Styles>
<Default ></Default>

<Header ></Header>

<Cell ></Cell>

<GroupFooter Font-Bold="True" ></GroupFooter>

<Title Font-Bold="True" ></Title>
</Styles>
</dxwgv:ASPxGridViewExporter>

        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
             Width="100%" KeyFieldName="ID" 
                    onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize">
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" 
                    Width="40px" Caption=" ">
                    <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                        AllowHeaderFilter="False" AllowSort="False" />
                    <DataItemTemplate>
                        <%# getBotaoVisualizar()%>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderTemplate>
                        <table>
    <tr>
        <td align="center">
            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" 
                                                            ClientInstanceName="menu" 
                ItemSpacing="5px" onitemclick="menu_ItemClick" 
                                                            oninit="menu_Init">
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
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DATA_OPERACAO" 
                    VisibleIndex="1" Width="115px">
                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesDateEdit>
                    <Settings AllowHeaderFilter="False" AutoFilterCondition="Equals" 
                        ShowFilterRowMenu="True" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataTextColumn FieldName="TABELA" VisibleIndex="2" 
                    Caption="Nome da Tabela" Width="200px">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="TipoOperacao" VisibleIndex="3" 
                    Caption="Operação Realizada" Width="140px">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="USUARIO" 
                    VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="200" AlwaysShowPager="True">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" 
                ShowGroupPanel="True" />
        </dxwgv:ASPxGridView>
    
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        </table>
    </asp:Content>
