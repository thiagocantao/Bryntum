<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lovFormulario.aspx.cs" Inherits="lovFormulario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxComboBox ID="cmbLov" runat="server" ClientInstanceName="cmbLov" 
            ValueType="System.String" Width="500px">
        </dx:ASPxComboBox>
        <dx:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar">
            <ClientSideEvents Click="function(s, e) {
	 window.parent.retornoModal = 'OK';
     window.parent.retornoModalTexto = cmbLov.GetText();
     window.parent.retornoModalValor = cmbLov.GetValue();
     window.parent.fechaModal();
}" />
        </dx:ASPxButton>
    
    </div>
    </form>
</body>
</html>
