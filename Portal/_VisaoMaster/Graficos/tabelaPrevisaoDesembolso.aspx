<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tabelaPrevisaoDesembolso.aspx.cs" Inherits="_VisaoMaster_Graficos_tabelaPrevisaoDesembolso" %>

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
                        onhtmlrowprepared="gvDados_HtmlRowPrepared">
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes1" 
                                ShowInColumn="PrevistoMes1" SummaryType="Sum" 
                                ShowInGroupFooterColumn="PrevistoMes1" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes1" 
                                ShowInColumn="RealMes1" SummaryType="Sum" 
                                ShowInGroupFooterColumn="RealMes1" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes2" 
                                ShowInColumn="PrevistoMes2" ShowInGroupFooterColumn="PrevistoMes2" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes2" 
                                ShowInColumn="RealMes2" ShowInGroupFooterColumn="RealMes2" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes3" 
                                ShowInColumn="PrevistoMes3" ShowInGroupFooterColumn="PrevistoMes3" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes3" 
                                ShowInColumn="RealMes3" ShowInGroupFooterColumn="RealMes3" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoTri" 
                                ShowInColumn="PrevistoTri" ShowInGroupFooterColumn="PrevistoTri" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealTri" 
                                ShowInColumn="RealTri" ShowInGroupFooterColumn="RealTri" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                        </TotalSummary>
                        <GroupSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes1" 
                                ShowInGroupFooterColumn="PrevistoMes1" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes1" 
                                ShowInGroupFooterColumn="RealMes1" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes2" 
                                ShowInGroupFooterColumn="PrevistoMes2" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes2" 
                                ShowInGroupFooterColumn="RealMes2" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoMes3" 
                                ShowInGroupFooterColumn="PrevistoMes3" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealMes3" 
                                ShowInGroupFooterColumn="RealMes3" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="PrevistoTri" 
                                ShowInGroupFooterColumn="PrevistoTri" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="RealTri" 
                                ShowInGroupFooterColumn="RealTri" SummaryType="Sum" ValueDisplayFormat="n2" />
                        </GroupSummary>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="CodigoConta" VisibleIndex="0" 
                                FieldName="CodigoConta" Visible="False">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="1" FieldName="Tipo" 
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="Grupo" 
                                VisibleIndex="2" GroupIndex="1" SortIndex="1" SortOrder="Ascending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Item" FieldName="Conta" VisibleIndex="3" 
                                ExportWidth="320">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="PrevistoTri" VisibleIndex="10" 
                                ExportWidth="170" Width="145px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="RealTri" VisibleIndex="11" 
                                ExportWidth="170" Width="145px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="PrevistoMes1" VisibleIndex="4" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="RealMes1" VisibleIndex="5" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="PrevistoMes2" VisibleIndex="6" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="RealMes2" VisibleIndex="7" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="PrevistoMes3" VisibleIndex="8" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn FieldName="RealMes3" VisibleIndex="9" 
                                ExportWidth="170" Width="120px">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewDataSpinEditColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" 
                            VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                            ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowTitlePanel="True" />
                        <Styles>
                            <Header>
                                <Paddings PaddingLeft="2px" PaddingRight="2px" />
                            </Header>
                            <TitlePanel HorizontalAlign="Left">
                            </TitlePanel>
                        </Styles>
                        <Templates>
                            <TitlePanel>
                                    <table>
                                        <tr>
                                            <td style="cursor: pointer">
                                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                                    ImageUrl="~/imagens/botoes/btnExcel.png" OnClick="ImageButton1_Click" 
                                                    ToolTip="Exportar para excel" />
                                            </td>
                                        </tr>
                                    </table>
                            </TitlePanel>
                        </Templates>
                    </dxwgv:ASPxGridView>
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
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" 
            gridviewid="gvDados" onrenderbrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:aspxgridviewexporter>
    </form>
</body>
</html>
