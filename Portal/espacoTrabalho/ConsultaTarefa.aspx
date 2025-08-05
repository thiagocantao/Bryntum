<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaTarefa.aspx.cs" Inherits="espacoTrabalho_ComentariosTarefa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <title>Detalhes da Tarefa</title>
    <style type="text/css">
        .style1
        {
            height: 10px;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                        Text="Projeto:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtProjeto" runat="server" ClientEnabled="False"
                        Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                        Text="Tarefa Superior:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtTarefaSuperior" runat="server" ClientEnabled="False"
                        Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                        Text="Tarefa:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtTarefa" runat="server" ClientEnabled="False"
                        Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                        Text="Recurso:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtRecurso" runat="server" ClientEnabled="False"
                        Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="style2">
                        <tr>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                        Text="Início Previsto:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                        Text="Término Previsto:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                        Text="Início Real:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                        Text="Término Real:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                        Text="Trabalho Previsto:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                        Text="Trabalho Real:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                        Text="Trabalho Restante:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                        Text="Concluído:" Width="115px">
                    </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                    <dxe:ASPxTextBox ID="txtInicioPrevisto" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="dd/MM/yyyy">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtTerminoPrevisto" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="dd/MM/yyyy">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtInicioReal" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="dd/MM/yyyy">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtTerminoReal" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="dd/MM/yyyy">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtTrabalhoPrevisto" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="{0:n1}">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtTrabalhoReal" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="{0:n1}">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtTrabalhoRestante" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="{0:n1}">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td>
                    <dxe:ASPxTextBox ID="txtPercentualConcluido" runat="server" ClientEnabled="False"
                        Width="100%" DisplayFormatString="{0:n2}%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                        Text="Anotações:">
                    </dxe:ASPxLabel>
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    <dxe:ASPxMemo ID="txtAnotacoes" runat="server" ClientEnabled="False"
                        Rows="8" Width="100%" 
                        ClientInstanceName="txtAnotacoes">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Comentários do Recurso:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtComentariosRecurso" runat="server" ClientEnabled="False"
                        Rows="8" Width="100%" 
                        ClientInstanceName="txtComentariosRecurso">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                    Text="Fechar" Width="90px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxe:ASPxButton>
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
