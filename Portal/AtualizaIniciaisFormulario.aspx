<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AtualizaIniciaisFormulario.aspx.cs" Inherits="UltimoComandoSQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 165px;
        }
        .style3
        {
            height: 10px;
        }
        .style4
        {
            width: 440px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var iniciaisFrm = '';
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td class="style4">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Modelo Formulário:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style2">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Senha:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" >
                            <dxe:ASPxComboBox ID="ddlModelo" runat="server" 
                                Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	txtIniciaisForm.SetText('');
	gvCampos.PerformCallback('A');
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="150px" 
                                ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Selecionar" 
                                onclick="Button1_Click"  
                                Width="90px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                </td>
        </tr>
    </table>
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                        Text="Iniciais Modelo Formulário:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxe:ASPxTextBox ID="txtIniciaisForm" runat="server" 
                        ClientInstanceName="txtIniciaisForm"  
                        MaxLength="24" Width="170px">
                        <ClientSideEvents TextChanged="function(s, e) {
	if(confirm('Deseja alterar as iniciais do modelo de formulário para &quot;' + s.GetText() + '&quot;?'))
		callbackForm.PerformCallback();
	else
		s.SetText(iniciaisFrm);
}" GotFocus="function(s, e) {
	iniciaisFrm = s.GetText();
}" />
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxwgv:ASPxGridView ID="gvCampos" runat="server" AutoGenerateColumns="False" 
                        ClientInstanceName="gvCampos"  
                        KeyFieldName="CodigoCampo" oncustomcallback="gvCampos_CustomCallback" 
                        onhtmldatacellprepared="gvCampos_HtmlDataCellPrepared" Width="100%">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Campo" FieldName="NomeCampo" 
                                ShowInCustomizationForm="True" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Iniciais" 
                                FieldName="IniciaisCampoControladoSistema" ShowInCustomizationForm="True" 
                                VisibleIndex="1" Width="120px">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox ID="txtIniciaisCampo" runat="server" BackColor="#CDEEFE" 
                                        ClientInstanceName="txtIniciaisCampo"  
                                        Width="100%">
                                        <FocusedStyle BackColor="#99FFCC">
                                        </FocusedStyle>
                                        <Border BorderStyle="None" />
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                                <CellStyle BackColor="#E4E4E4">
                                    <Paddings Padding="0px" />
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollableHeight="350" VerticalScrollBarMode="Visible" />
                    </dxwgv:ASPxGridView>
                    <dxcb:ASPxCallback ID="callbackForm" runat="server" 
                        ClientInstanceName="callbackForm" oncallback="callbackForm_Callback">
                    </dxcb:ASPxCallback>
                    <dxcb:ASPxCallback ID="callbackCampo" runat="server" 
                        ClientInstanceName="callbackCampo" oncallback="callbackCampo_Callback">
                    </dxcb:ASPxCallback>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
