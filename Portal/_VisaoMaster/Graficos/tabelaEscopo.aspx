<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tabelaEscopo.aspx.cs" Inherits="_VisaoMaster_Graficos_tabelaEscopo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
                         Width="100%" 
                        onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize">
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPrevisto" 
                                SummaryType="Sum" ShowInGroupFooterColumn="ValorPrevisto" 
                                ValueDisplayFormat="{0:n2}" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorContratado" 
                                SummaryType="Sum" ShowInGroupFooterColumn="ValorContratado" 
                                ValueDisplayFormat="{0:n2}" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorDiferenca" 
                                ShowInGroupFooterColumn="ValorDiferenca" SummaryType="Sum" 
                                ValueDisplayFormat="{0:n2}" />
                        </TotalSummary>
                        <GroupSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPrevisto" 
                                ShowInGroupFooterColumn="ValorPrevisto" SummaryType="Sum" 
                                ValueDisplayFormat="{0:n2}" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorContratado" 
                                ShowInGroupFooterColumn="ValorContratado" SummaryType="Sum" 
                                ValueDisplayFormat="{0:n2}" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorDiferenca" 
                                ShowInGroupFooterColumn="ValorDiferenca" SummaryType="Sum" 
                                ValueDisplayFormat="{0:n2}" />
                        </GroupSummary>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Item/Projeto" VisibleIndex="2" 
                                FieldName="Item" ExportWidth="350">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                <FooterTemplate>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                        Text="Total:">
                                    </dxe:ASPxLabel>
                                </FooterTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Prazo Máximo Contratação" FieldName="DataPrevista" 
                                VisibleIndex="4" Width="155px" ExportWidth="175">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                    <Style HorizontalAlign="Center">
                                    </Style>
                                </PropertiesDateEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor Previsto (R$ Mil)" FieldName="ValorPrevisto" 
                                VisibleIndex="5" Width="155px" ExportWidth="175">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    <Style HorizontalAlign="Right">
                                    </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor Contratado (R$ Mil)" VisibleIndex="6" 
                                Width="155px" FieldName="ValorContratado" ExportWidth="175">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    <Style HorizontalAlign="Right">
                                    </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="TipoItem" 
                                Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" 
                                Visible="False" VisibleIndex="8">
                                <Settings AllowAutoFilter="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Área" FieldName="Area" GroupIndex="0" 
                                SortIndex="0" SortOrder="Ascending" VisibleIndex="9" ExportWidth="350">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Contratado?" FieldName="Contratado" 
                                VisibleIndex="3" ExportWidth="110" Width="90px">
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Diferença (R$ Mil)" FieldName="ValorDiferenca" 
                                VisibleIndex="7" Width="155px" ExportWidth="175">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    <Style HorizontalAlign="Right">
                                    </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Item" VisibleIndex="1" Width="50px" 
                                ExportWidth="60" Visible="False">
                                <PropertiesTextEdit>
                                    <Style HorizontalAlign="Center">
                                    </Style>
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" 
                            VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                            ShowFooter="True" ShowTitlePanel="True" />
                        <Styles>
                            <TitlePanel HorizontalAlign="Left">
                            </TitlePanel>
                        </Styles>
                        <Templates>
                            <TitlePanel>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/imagens/botoes/btnExcel.png" OnClick="ImageButton1_Click" 
                                        ToolTip="Exportar para excel" />
                                </HeaderTemplate>
                            </TitlePanel>
                        </Templates>
                    </dxwgv:ASPxGridView>
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" 
            gridviewid="gvDados" onrenderbrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:aspxgridviewexporter>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
