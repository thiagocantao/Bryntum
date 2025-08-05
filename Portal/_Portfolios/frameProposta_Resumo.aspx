<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_Resumo.aspx.cs"
    Inherits="_Portfolios_frameProposta_Resumo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resumo da proposta</title>

    
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>

</head>
<body style="margin-left: 0px; margin-top: 0px">
    <form id="form1" runat="server">
        <div style="padding-top: 3px; padding-left: 0px;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 840px">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblTitulo" runat="server" Text="Título:" Font-Bold="False"
                           >
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txtTitulo" runat="server" Width="95%" ClientInstanceName="txtTitulo"
                            >
                            <DisabledStyle ForeColor="#404040">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblDetalhes" runat="server" Text="Detalhes:" Font-Bold="False"
                           >
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxMemo ID="txtDetalhes" runat="server" Rows="5" Width="95%"
                           >
                            <DisabledStyle ForeColor="#404040">
                            </DisabledStyle>
                        </dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                            <tr>
                                <td style="width: 399px">
                                    <dxe:ASPxLabel ID="lblStatus" runat="server" Text="Status:" Font-Bold="False"
                                       >
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lblCategoria" runat="server" Text="Categoria:" Font-Bold="False"
                                        >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 399px">
                                    <dxe:ASPxComboBox ID="cmbStatus" Width="380px" runat="server" >
                                        <DisabledStyle ForeColor="#404040">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmbCategoria" Width="395px" runat="server" >
                                        <DisabledStyle ForeColor="#404040">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 399px; height: 10px">
                                    <dxe:ASPxLabel ID="lblGerente" runat="server" Text="Gerente:" Font-Bold="False"
                                       >
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="height: 10px">
                                    <dxe:ASPxLabel ID="lblUnidade" runat="server" Text="Unidade:" Font-Bold="False"
                                       >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 399px">
                                    <dxe:ASPxComboBox ID="cmbGerente" Width="380px" runat="server" >
                                        <DisabledStyle ForeColor="#404040">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmbUnidade" Width="395px" runat="server" >
                                        <DisabledStyle ForeColor="#404040">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 13px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 143px">
                                    <dxe:ASPxButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" Text="Salvar"
                                        Width="95px" >
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = validaCampos();
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
