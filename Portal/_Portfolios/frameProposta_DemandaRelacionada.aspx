<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_DemandaRelacionada.aspx.cs" Inherits="_Portfolios_frameProposta_DemandaRelacionada" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
 
    <style type="text/css">
        .style1
        {
            width: 10px;
            height: 6px;
        }
    </style>
 
    </head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" 100%"" width="100%" >
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="grid"
                        Width="100%" OnRowUpdating="grid_RowUpdating" 
                        OnHtmlRowCreated="grid_HtmlRowCreated" AutoGenerateColumns="False">
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Visible" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Demanda" FieldName="Demanda" 
                                Name="Demanda" VisibleIndex="0" ShowInCustomizationForm="True">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" Name="Status" 
                                VisibleIndex="1" ShowInCustomizationForm="True" Width="160px">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px;">
                </td>
                <td align="right">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" 
                                    ClientInstanceName="btnCancelar"  
                                    Text="Fechar" Width="100px" AutoPostBack="False" ClientVisible="False">
                                    <ClientSideEvents CheckedChanged="
" Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxe:ASPxButton>
 
                </td>

            <td align="right">
                &nbsp;</td>
        
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
