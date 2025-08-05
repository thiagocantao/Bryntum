<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lovFormulario.aspx.cs" Inherits="lovFormulario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 99%;
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
    
        <dxe:ASPxComboBox ID="cmbLov" runat="server" ClientInstanceName="cmbLov" 
            ValueType="System.String" Width="100%" 
            IncrementalFilteringMode="Contains"  
                        DropDownHeight="150px" DropDownStyle="DropDown" EnableCallbackMode="True" 
                        onitemrequestedbyvalue="ddlListaPre_ItemRequestedByValue" 
                        onitemsrequestedbyfiltercondition="ddlListaPre_ItemsRequestedByFilterCondition">
        </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
            </tr>
            <tr>
                <td align="right">
        <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar" 
                        Width="100px">
            <ClientSideEvents Click="function(s, e) {
   if(cmbLov.GetText() == '' || cmbLov.FindItemByText(cmbLov.GetText()) != null)
  {
	 window.parent.retornoModal = 'OK';
     	window.parent.retornoModalTexto = cmbLov.GetText();
     	window.parent.retornoModalValor = cmbLov.GetValue();
     	window.parent.fechaModal();
 }
else
{
	window.top.mostraMensagem('O valor selecionado não consta na lista! Selecione um item válido!', 'erro', true, false, null);
}
}
" />
            <Paddings Padding="0px" />
        </dxe:ASPxButton>
    
                </td>
            </tr>
        </table>
    
    </div>    
    <asp:SqlDataSource ID="dsResponsavel" runat="server" 
        ConnectionString="" 
        SelectCommand="">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    </form>
</body>
</html>
