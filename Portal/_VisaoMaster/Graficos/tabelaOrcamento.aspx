<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tabelaOrcamento.aspx.cs" Inherits="_VisaoMaster_Graficos_tabelaOrcamento" %>

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
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev1" 
                                ShowInColumn="Prev1" SummaryType="Sum" 
                                ShowInGroupFooterColumn="Prev1" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real1" 
                                ShowInColumn="Real1" SummaryType="Sum" 
                                ShowInGroupFooterColumn="Real1" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev2" 
                                ShowInColumn="Prev2" ShowInGroupFooterColumn="Prev2" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real2" 
                                ShowInColumn="Real2" ShowInGroupFooterColumn="Real2" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev3" 
                                ShowInColumn="Prev3" ShowInGroupFooterColumn="Prev3" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real3" 
                                ShowInColumn="Real3" ShowInGroupFooterColumn="Real3" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev4" 
                                ShowInColumn="Prev4" ShowInGroupFooterColumn="Prev4" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real4" 
                                ShowInColumn="Real4" ShowInGroupFooterColumn="Real4" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev5" 
                                ShowInColumn="Prev5" ShowInGroupFooterColumn="Prev5" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real5" 
                                ShowInColumn="Real5" ShowInGroupFooterColumn="Real5" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev6" 
                                ShowInColumn="Prev6" ShowInGroupFooterColumn="Prev6" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real6" 
                                ShowInColumn="Real6" ShowInGroupFooterColumn="Real6" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev7" 
                                ShowInColumn="Prev7" ShowInGroupFooterColumn="Prev7" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real7" 
                                ShowInColumn="Real7" ShowInGroupFooterColumn="Real7" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev8" 
                                ShowInColumn="Prev8" ShowInGroupFooterColumn="Prev8" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real8" 
                                ShowInColumn="Real8" ShowInGroupFooterColumn="Real8" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev9" 
                                ShowInColumn="Prev9" ShowInGroupFooterColumn="Prev9" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real9" 
                                ShowInColumn="Real9" ShowInGroupFooterColumn="Real9" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev10" 
                                ShowInColumn="Prev10" ShowInGroupFooterColumn="Prev10" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real10" 
                                ShowInColumn="Real10" ShowInGroupFooterColumn="Real10" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev11" 
                                ShowInColumn="Prev11" ShowInGroupFooterColumn="Prev11" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real11" 
                                ShowInColumn="Real11" ShowInGroupFooterColumn="Real11" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev12" 
                                ShowInColumn="Prev12" ShowInGroupFooterColumn="Prev12" 
                                SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real12" 
                                ShowInColumn="Real12" ShowInGroupFooterColumn="Real12" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                        </TotalSummary>
                        <GroupSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev1" 
                                ShowInGroupFooterColumn="Prev1" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real1" 
                                ShowInGroupFooterColumn="Real1" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev2" 
                                ShowInGroupFooterColumn="Prev2" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real2" 
                                ShowInGroupFooterColumn="Real2" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev3" 
                                ShowInGroupFooterColumn="Prev3" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real3" 
                                ShowInGroupFooterColumn="Real3" SummaryType="Sum" ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev4" 
                                ShowInGroupFooterColumn="Prev4" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real4" 
                                ShowInGroupFooterColumn="Real4" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev5" 
                                ShowInGroupFooterColumn="Prev5" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real5" 
                                ShowInGroupFooterColumn="Real5" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev6" 
                                ShowInGroupFooterColumn="Prev6" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real6" 
                                ShowInGroupFooterColumn="Real6" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev7" 
                                ShowInGroupFooterColumn="Prev7" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real7" 
                                ShowInGroupFooterColumn="Real7" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev8" 
                                ShowInGroupFooterColumn="Prev8" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real8" 
                                ShowInGroupFooterColumn="Real8" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev9" 
                                ShowInGroupFooterColumn="Prev9" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real9" 
                                ShowInGroupFooterColumn="Real9" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev10" 
                                ShowInGroupFooterColumn="Prev10" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real10" 
                                ShowInGroupFooterColumn="Real10" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev11" 
                                ShowInGroupFooterColumn="Prev11" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real11" 
                                ShowInGroupFooterColumn="Real11" SummaryType="Sum" ValueDisplayFormat="n2" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Prev12" 
                                ShowInGroupFooterColumn="Prev12" SummaryType="Sum" 
                                ValueDisplayFormat="n2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Real12" 
                                ShowInGroupFooterColumn="Real12" SummaryType="Sum" ValueDisplayFormat="n2" />
                        </GroupSummary>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="Grupo" 
                                VisibleIndex="2" GroupIndex="1" SortIndex="1" SortOrder="Ascending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Item" FieldName="Conta" VisibleIndex="3" 
                                ExportWidth="450" Width="420px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewBandColumn Caption="Janeiro" Name="Jan" VisibleIndex="4">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev1" Name="Prev1" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real1" Name="Real1" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Fevereiro" Name="Fev" VisibleIndex="5">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev2" Name="Prev2" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real2" Name="Real2" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Março" Name="Mar" VisibleIndex="6">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev3" Name="Prev3" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real3" Name="Real3" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Abril" Name="Abr" VisibleIndex="7">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev4" Name="Prev4" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real4" Name="Real4" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Maio" Name="Mai" VisibleIndex="8">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev5" Name="Prev5" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real5" Name="Real5" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Junho" Name="Jun" VisibleIndex="9">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev6" Name="Prev6" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real6" Name="Real6" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Julho" Name="Jul" VisibleIndex="10">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev7" Name="Prev7" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real7" Name="Real7" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Agosto" Name="Ago" VisibleIndex="11">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev8" Name="Prev8" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real8" Name="Real8" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Setembro" Name="Set" VisibleIndex="12">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev9" Name="Prev9" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real9" Name="Real9" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Outubro" Name="Out" VisibleIndex="13">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev10" Name="Prev10" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real10" Name="Real10" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Novembro" Name="Nov" VisibleIndex="14">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev11" Name="Prev11" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real11" Name="Real11" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Dezembro" Name="Dez" VisibleIndex="15">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" ExportWidth="170" 
                                        FieldName="Prev12" Name="Prev12" VisibleIndex="0" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Realizado" ExportWidth="170" 
                                        FieldName="Real12" Name="Real12" VisibleIndex="1" Width="120px">
                                        <PropertiesTextEdit DisplayFormatString="n2">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True">
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" 
                            VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                            ShowGroupFooter="VisibleAlways" ShowTitlePanel="True" 
                            HorizontalScrollBarMode="Auto" />
                        <Styles>
                            <Header>
                                <Paddings PaddingLeft="2px" PaddingRight="2px" />
                            </Header>
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
