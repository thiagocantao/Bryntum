<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
    &nbsp;<dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server">
        </dx:ASPxHiddenField>
    </div>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="200px">
        </dx:ASPxPanel>
        <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server">
        </dx:ASPxUploadControl>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <TotalSummary>
                <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="valor" 
                    ShowInColumn="valor" ShowInGroupFooterColumn="valor" SummaryType="Sum" 
                    ValueDisplayFormat="{0:N2}" />
            </TotalSummary>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="valor" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowVerticalScrollBar="True" ShowFooter="True" />
        </dx:ASPxGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:desenv_PortfolioConnectionString %>" 
        SelectCommand="SELECT * FROM [_teste]"></asp:SqlDataSource>
        <dx:ASPxPageControl id="ASPxPageControl1" runat="server">
        </dx:ASPxPageControl>
        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
        </dx:ASPxCallbackPanel>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server">
        </dx:ASPxPopupControl>
        <dx:ASPxTreeList ID="ASPxTreeList1" runat="server">
        </dx:ASPxTreeList>
    </form>
</body>
</html>
