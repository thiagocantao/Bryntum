<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auditoria.aspx.cs" Inherits="administracao_auditoria" %>

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
            width: 10px;
        }
        .style3
        {
            width: 10px;
            height: 10px;
        }
        .style4
        {
            height: 10px;
        }
        .style5
        {
            width: 290px;
        }
        .style6
        {
            width: 185px;
        }
        .style7
        {
            width: 127px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <table cellspacing="1" class="style1">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                <table cellspacing="1" class="style1">
                    <tr>
                        <td class="style7">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Tipo de Operação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style5">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Nome da Tabela:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Usuário Responsável:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style6">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="IP da Máquina:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtTipoOperacao" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="style5" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtTabela" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtUsuario" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="style6">
                            <dxe:ASPxTextBox ID="txtMaquina" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style4">
                </td>
            <td class="style3">
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
    
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
             Width="100%">
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="NomeCampo" VisibleIndex="0" 
                    Width="220px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NovoValor" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="AntigoValor" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" />
        </dxwgv:ASPxGridView>
    
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style4">
                </td>
            <td class="style3">
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
