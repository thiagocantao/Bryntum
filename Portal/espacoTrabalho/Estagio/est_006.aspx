<%@ Page Language="C#" AutoEventWireup="true" CodeFile="est_006.aspx.cs" Inherits="_Projetos_VisaoCorporativa_vc_004" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
             OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="Descricao" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="TCEs" FieldName="Coluna01" VisibleIndex="1">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Contratos" FieldName="Coluna02" VisibleIndex="2">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Convenios" FieldName="Coluna03" VisibleIndex="3">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings VerticalScrollBarMode="Visible" />
        </dxwgv:ASPxGridView>
        &nbsp;</div>
    </form>
</body>
</html>
