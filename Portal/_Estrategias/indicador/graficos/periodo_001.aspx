<%@ Page Language="C#" AutoEventWireup="true" CodeFile="periodo_001.aspx.cs" Inherits="_Estrategias_indicador_graficos_periodo_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: left">
        <dxwgv:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
             Width="99%">
            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
            <Styles>
                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                </Header>
            </Styles>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Images ImageFolder="~/App_Themes/Glass/{0}/">
                <CollapsedButton Height="12px" Width="11px" />
                <DetailCollapsedButton Height="9px" Width="9px" />
                <HeaderFilter Height="18px" Url="~/App_Themes/Glass/GridView/gvHeaderFilter.png"
                    Width="18px" />
                <HeaderActiveFilter Height="18px" Url="~/App_Themes/Glass/GridView/gvHeaderFilterActive.png"
                    Width="18px" />
                <FilterRowButton Height="13px" Width="13px" />
                <CustomizationWindowClose Height="17px" Width="17px" />
                <PopupEditFormWindowClose Height="17px" Width="17px" />
                <FilterBuilderClose Height="17px" Width="17px" />
            </Images>
            <SettingsText  />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" VisibleIndex="0"
                    Width="35%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Meta" FieldName="ValorMeta" VisibleIndex="1"
                    Width="25%">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Right" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Realizado" FieldName="ValorRealizado" VisibleIndex="2"
                    Width="25%">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False" NullDisplayText="-"
                        NullText="-">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Right" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Desempenho" Visible="False" VisibleIndex="3"
                    Width="75px">
                    <HeaderStyle HorizontalAlign="Right" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="CorIndicador" VisibleIndex="3"
                    Width="15%">
                    <DataItemTemplate>
                        <img alt='' src='../../../imagens/<%# Eval("CorIndicador") %>.gif' />
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Tend&#234;ncia" Visible="False" VisibleIndex="5"
                    Width="55px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="145" />
            <StylesEditors>
                <ProgressBar Height="25px">
                </ProgressBar>
            </StylesEditors>
        </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
