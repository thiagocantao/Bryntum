<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testeConexaoSQL.aspx.cs"
    Inherits="testeConexaoSQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function validaCampos() {
            var txtServidor = document.getElementById("txtServidor");
            var txtBanco = document.getElementById("txtBanco");
            var txtUsuario = document.getElementById("txtUsuario");
            var txtSenha = document.getElementById("txtSenha");

            if (txtServidor.value == "") {
                txtServidor.focus();
                window.top.mostraMensagem("Preencha o nome do servidor", 'Atencao', true, false, null);
                return false;
            }
            if (txtBanco.value == "") {
                txtBanco.focus();
                window.top.mostraMensagem("Preencha o nome do banco", 'Atencao', true, false, null);
                return false;
            }
            if (txtUsuario.value == "") {
                txtUsuario.focus();
                window.top.mostraMensagem("Preencha o nome do usuário", 'Atencao', true, false, null);
                return false;
            }
            if (txtSenha.value == "") {
                txtSenha.focus();
                window.top.mostraMensagem("Preencha a senha", 'Atencao', true, false, null);
                return false;
            }

            mostraAguarde();
            return true;
        }
        function mostraAguarde() {
            
            var lblStringConexao = document.getElementById("lblStringConexao");
            var lblResultado = document.getElementById("lblResultado");

            lblResultado.innerText = "Aguarde...";
        }
    
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 400px">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Nome do servidor:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtServidor" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="nome da instancia:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInstancia" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Número da porta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPorta" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Nome do banco de dados"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBanco" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px">
                    <asp:Label ID="Label5" runat="server" Text="Nome do usuário:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px">
                    <asp:Label ID="Label6" runat="server" Text="Senha:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSenha" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px; text-align: center;" colspan="2">
                    <asp:Button ID="btnGerarStringConexao" runat="server" 
                        Text="Criar a string de conexão" OnClick="btnGerarStringConexao_Click" OnClientClick="return validaCampos()"/>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px; text-align: center;" colspan="2">
                    <asp:Button ID="btnTestarCoxexao" runat="server" Text="Testar a conexão" 
                        onclick="btnTestarCoxexao_Click" OnClientClick="return validaCampos()"/>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 120px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <p>
        <asp:Label ID="lblStringConexao" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
        <asp:Label ID="lblResultado" runat="server" Text="Label" ForeColor="#990000" Font-Bold="True"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
