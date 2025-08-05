<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupSprint.aspx.cs" Inherits="_Projetos_DadosProjeto_popupSprint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        body {
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 84vh">
                <tbody>
                    <tr class="formulario-linha">
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="formulario-label">
                                            <dxtv:ASPxLabel ID="lblTitulo6" runat="server" ClientInstanceName="lblTitulo"
                                                Text="Título: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxTextBox ID="txtTitulo" runat="server" ClientInstanceName="txtTitulo"
                                                MaxLength="150" Width="100%">
                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </ReadOnlyStyle>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td>
                            <table class="style1" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="formulario-label">
                                        <dxtv:ASPxLabel ID="lblTitulo16" runat="server" ClientInstanceName="lblTitulo"
                                            Text="Início: *" Width="120">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td class="formulario-label">
                                        <dxtv:ASPxLabel ID="lblTitulo17" runat="server" ClientInstanceName="lblTitulo"
                                            Text="Término: *" Width="120">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td class="formulario-label">
                                        <dxtv:ASPxLabel ID="lblTitulo11" runat="server" ClientInstanceName="lblTitulo"
                                            Text="Status:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 5px">
                                        <dxtv:ASPxDateEdit ID="dtInicio" runat="server" ClientInstanceName="dtInicio" Width="100%">
                                            <ClientSideEvents ValueChanged="function(s, e) {
		dtTermino.SetDate(null);
		if(s.GetDate() != null)
        {      
            var data = s.GetDate();
            dtTermino.SetMinDate(data);
        }
	
}" />
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxtv:ASPxDateEdit>
                                    </td>
                                    <td style="padding-right: 5px">
                                        <dxtv:ASPxDateEdit ID="dtTermino" runat="server" ClientInstanceName="dtTermino" Width="100%">
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxtv:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtStatus" runat="server" ClientInstanceName="txtStatus"
                                            MaxLength="150" Width="100%" ClientEnabled="False">
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td valign="bottom">
                            <dxtv:ASPxLabel ID="lblTitulo18" runat="server" ClientInstanceName="lblTitulo"
                                Text="Responsável: *">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td>
                            <dxtv:ASPxCallbackPanel ID="pnResponsavelScrumMaster" runat="server" Width="100%" ClientInstanceName="pnResponsavelScrumMaster" OnCallback="pnResponsavelScrumMaster_Callback">
                                <PanelCollection>
                                    <dxtv:PanelContent runat="server">
                                        <dxtv:ASPxComboBox ID="ddlProjectOwner" runat="server" ClientInstanceName="ddlProjectOwner"
                                            Width="100%">
                                            <ItemStyle Wrap="True" />
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxComboBox>
                                    </dxtv:PanelContent>
                                </PanelCollection>
                            </dxtv:ASPxCallbackPanel>

                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td style="padding-top: 10px" id="tdCopiaRaiasRecursos" runat="server">
                            <table class="style1">
                                <tr>
                                    <td style="width: 50%">
                                        <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Sprint para modelo das raias: *">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <dxcp:ASPxComboBox ID="ddlEscolheModeloRaia" runat="server" ClientInstanceName="ddlEscolheModeloRaia" ValueType="System.String" Width="100%">
                                        </dxcp:ASPxComboBox>
                                    </span></td>
                                    <td><span>
                                        <dxtv:ASPxCheckBox ID="ckbCopiarRecursos" runat="server" CheckState="Checked" ClientInstanceName="ckbCopiarRecursos" Text="Copiar os recursos do projeto para este sprint" Width="100%">
                                        </dxtv:ASPxCheckBox>
                                    </span></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td style="padding-top: 10px">
                            <dxtv:ASPxLabel ID="lblTitulo7" runat="server" ClientInstanceName="lblTitulo" Text="Objetivos:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td style="height: 100%">
                            <dxtv:ASPxMemo ID="txtObjetivos" runat="server" ClientInstanceName="txtObjetivos" Width="100%" Height="94%">
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxMemo>
                            <dxtv:ASPxLabel ID="lblContadorMemoDescricao" runat="server" ClientInstanceName="lblContadorMemoDescricao"
                                Font-Bold="True" ForeColor="#999999">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
        if(s.cpErro != '')
        {
                   window.top.mostraMensagem(s.cpErro,'erro', true, false, null);
        }
        else
       {
                   if(s.cpSucesso != '')
                   {
                              window.top.mostraMensagem(s.cpSucesso,'sucesso', false, false, null);
                             window.top.fechaModalComFooter();
                   }
       }
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
