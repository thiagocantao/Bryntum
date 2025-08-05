<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MelhorEstimativa.aspx.cs" Inherits="_Portfolios_frameProposta_FluxoCaixa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() 
        {
            existeConteudoCampoAlterado = true;
        }
    </script> 

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 90px;
        }
    </style>

</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 10px;">
                </td>
                <td style="height: 10px">
                </td>
                <td style="width: 10px; height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                        KeyFieldName="Codigo" Width="100%" 
                        OnRowUpdating="grid_RowUpdating" OnHtmlRowCreated="grid_HtmlRowCreated" 
                        oncelleditorinitialize="grid_CellEditorInitialize" 
                        onsummarydisplaytext="grid_SummaryDisplayText">
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" 
                            ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="True" 
                            ShowGroupFooter="VisibleAlways" />
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                        <Styles>
                            <GroupRow BackColor="#EBEBEB" Font-Bold="True">
                            </GroupRow>
                            <Footer Font-Bold="True">
                            </Footer>
                        </Styles>
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
