<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tabelaCusto.aspx.cs" Inherits="_VisaoMaster_Graficos_tabelaCusto" %>

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
                        onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize" 
                        oncustomcolumnsort="gvDados_CustomColumnSort">
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPrevisto" 
                                SummaryType="Sum" ShowInGroupFooterColumn="ValorPrevisto" 
                                ValueDisplayFormat="{0:n2}" ShowInColumn="ValorPrevisto" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorReal" 
                                SummaryType="Sum" ShowInGroupFooterColumn="ValorReal" 
                                ValueDisplayFormat="{0:n2}" ShowInColumn="ValorReal" />
                        </TotalSummary>
                        <GroupSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorPrevisto" 
                                ShowInGroupFooterColumn="ValorPrevisto" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorReal" 
                                ShowInGroupFooterColumn="ValorReal" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                        </GroupSummary>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Descrição" VisibleIndex="2" 
                                FieldName="Conta" ExportWidth="350" GroupIndex="1" SortIndex="1" 
                                SortOrder="Ascending">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" 
                                    AllowSort="False" />
                                <FooterTemplate>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                        Text="Total:">
                                    </dxe:ASPxLabel>
                                </FooterTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor Previsto (R$ Mil)" FieldName="ValorPrevisto" 
                                VisibleIndex="5" ExportWidth="175">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    <Style HorizontalAlign="Right">
                                    </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor Real (R$ Mil)" VisibleIndex="6" 
                                FieldName="ValorReal" ExportWidth="175">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    <Style HorizontalAlign="Right">
                                    </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="Grupo" 
                                VisibleIndex="0" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Período" FieldName="DescricaoPeriodo" 
                                VisibleIndex="3" ExportWidth="150">
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" 
                            VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                            ShowFooter="True" ShowTitlePanel="True" 
                            ShowGroupFooter="VisibleAlways" />
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
