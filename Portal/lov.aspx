<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lov.aspx.cs" Inherits="lov" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self" />
    <title>Lista de Valores</title>
</head>
<body onunload="atualizaTelaPai()">
    <form id="form1" runat="server" target="lov">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px">
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblColunaPesquisar" runat="server" Text="Nome:" ClientInstanceName="lblColunaPesquisar" >
                    </dxe:ASPxLabel>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="txtNome" runat="server" ClientInstanceName="txtNome" Width="100%" >
<ClientSideEvents ButtonClick="function(s, e) {
    var pesquisarPor = txtNome.GetText();
    txtNome.SetText(trim(pesquisarPor));
    if (s.GetText()!=&quot;&quot;)
		gvResultado.PerformCallback(&quot;Pesquisar;&quot;+s.GetText());
}" KeyUp="function(s, e) {

	if(e.htmlEvent.keyCode == 13)
    {
	    var pesquisarPor = txtNome.GetText();
    	txtNome.SetText(trim(pesquisarPor));
	    if (s.GetText()!=&quot;&quot;)
			gvResultado.PerformCallback(&quot;Pesquisar;&quot;+s.GetText());
    }
}"></ClientSideEvents>
<Buttons>
<dxe:EditButton></dxe:EditButton>
</Buttons>
</dxe:ASPxButtonEdit>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td style="height: 10px">
                </td>
                <td style="width: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Resultado" >
                    </dxe:ASPxLabel>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvResultado" runat="server" ClientInstanceName="gvResultado" OnCustomCallback="gvResultado_CustomCallback" KeyFieldName="ColunaValor"
                        Width="100%" AutoGenerateColumns="False" >
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Valor" FieldName="ColunaValor" Name="ColunaValor" Visible="False"
                                VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="ColunaNome" Name="ColunaNome" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                        <ClientSideEvents RowDblClick="function(s, e) {
	fecharPopUp(&quot;1&quot;);
}" />
                    </dxwgv:ASPxGridView>
                    <table>
                        <tr>
                            <td align="right" style="height: 35px" valign="bottom">
                                <table>
                                    <tr>
                                        <td >
                                            <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False" ClientInstanceName="btnSelecionar"
                                                Text="Selecionar" >
                                                <ClientSideEvents Click="function(s, e) {
	fecharPopUp(&quot;1&quot;);
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="width: 84px">
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar"  Width="85px">
                                                <ClientSideEvents Click="function(s, e) {
	fecharPopUp(&quot;&quot;);
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
