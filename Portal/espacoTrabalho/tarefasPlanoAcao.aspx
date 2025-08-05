<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tarefasPlanoAcao.aspx.cs" Inherits="espacoTrabalho_tarefasPlanoAcao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .titulo-tela,
        .titulo-tela > span {
            display: inline-block;
            font-size: 18px !important;
            padding-bottom: 10px;
            padding-left: 7px;
            padding-top: 5px;
        }
        .iniciaisMaiusculas{
            text-transform:capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td>
                            <dxcp:ASPxLabel runat="server" CssClass="titulo-tela" Text="Plano de Ação:"
                                ClientInstanceName="lblStatusTarefa"
                                ID="lblStatusTarefa0">
                            </dxcp:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoPlanoAcao" ClientEnabled="False" ID="txtDescricaoPlanoAcao" MaxLength="250">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxcp:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px">
                            <dxcp:ASPxLabel runat="server" Text="A&#231;&#245;es:" ClientInstanceName="lblAnotacoes" ID="lblAnotacoes"></dxcp:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td1">
                            <dxcp:ASPxPanel runat="server" Width="100%" ID="pAcoes">
                                <PanelCollection>
                                    <dxcp:PanelContent runat="server"></dxcp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxPanel>



                        </td>
                    </tr>
                    <tr>
                        <td id="Td1" align="right" style="padding-top: 10px">
                            <dxcp:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" CssClass="iniciaisMaiusculas"
                                Text="Fechar" Width="100px">
                                <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}" />
                                <Paddings Padding="0px" />
                            </dxcp:ASPxButton>



                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
            <ClientSideEvents Init="function(s, e) {
	if(window.hfGeralToDoList) 
        {
            if(window.hfGeral &amp;&amp; hfGeral.Contains('codigoObjetoAssociado'))
                hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get('codigoObjetoAssociado') );
        }

}" />
        </dxcp:ASPxHiddenField>



    </form>
</body>
</html>
