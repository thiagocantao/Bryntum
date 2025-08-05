<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComentariosTarefa.aspx.cs" Inherits="espacoTrabalho_ComentariosTarefa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <base target="_self" />
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <title>Comentários da Tarefa</title>
    <style type="text/css">
        .style1 {
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div runat="server" id="divAlturaPopup" style="overflow-y: scroll; width: 100%;">
            <table class="formulario" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr class="formulario-linha">
                    <td class="formulario-label">
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                            Text="Projeto:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td>
                        <dxe:ASPxTextBox ID="txtProjeto" runat="server" ClientEnabled="False" Width="100%">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td class="formulario-label">
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                            Text="Tarefa:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td>
                        <dxe:ASPxTextBox ID="txtTarefa" runat="server" ClientEnabled="False" Width="100%">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td class="formulario-label">
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                            Text="Recurso:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td>
                        <dxe:ASPxTextBox ID="txtRecurso" runat="server" ClientEnabled="False" Width="100%">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td class="formulario-label">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                            Text="Comentários do Recurso:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td>
                        <dxe:ASPxMemo ID="txtComentariosRecurso" runat="server" ClientEnabled="False" Rows="8" Width="100%"
                            ClientInstanceName="txtComentariosRecurso">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxMemo>
                        <dxe:ASPxLabel ID="lblContadorComentarioRecurso" runat="server"
                            ClientInstanceName="lblContadorComentarioRecurso" Font-Bold="True" ForeColor="#999999">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td class="formulario-label">
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                            Text="Comentários do Aprovador:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr class="formulario-linha">
                    <td>
                        <dxe:ASPxMemo ID="txtComentariosAprovador" runat="server"
                            Rows="8" Width="100%" ClientInstanceName="txtComentariosAprovador">
                        </dxe:ASPxMemo>
                        <dxe:ASPxLabel ID="lblContadorComentarioAprovador" runat="server"
                            ClientInstanceName="lblContadorComentarioAprovador" Font-Bold="True" ForeColor="#999999">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>

        </div>
        <table align="right" class="formulario-botoes" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" Text="Salvar" Width="90px">
                        <Paddings Padding="0px" />
                        <ClientSideEvents Click="function(s, e) {
	callBack.PerformCallback();
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Fechar" Width="90px">
                        <Paddings Padding="0px" />
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    if(s.cp_OK != '')
    {
                     window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
    }
    else
    {
                     window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
    }
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
