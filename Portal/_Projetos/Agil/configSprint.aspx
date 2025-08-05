<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configSprint.aspx.cs" Inherits="_Projetos_Agil_configSprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="padding-top: 5px; width: 450px;">
                                        <dxcp:ASPxLabel runat="server" Text="Local:" ClientInstanceName="lblTitulo"
                                            ID="lblTitulo17">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td style="padding-top: 5px; width: 70px;">
                                        <dxcp:ASPxLabel runat="server" Text="In&#237;cio:" ClientInstanceName="lblTitulo"
                                             ID="lblTitulo21">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td style="padding-top: 5px; width: 70px;">
                                        <dxcp:ASPxLabel runat="server" Text="T&#233;rmino:" ClientInstanceName="lblTitulo"
                                             ID="lblTitulo22">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td style="padding-top: 5px; width: 170px;">
                                        <dxcp:ASPxLabel runat="server" Text="Tipo de Gráfico:" ClientInstanceName="lblTitulo"
                                             ID="lblTitulo23">
                                        </dxcp:ASPxLabel>
                                    </td>
                                    <td style="padding-top: 5px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 5px;">
                                        <dxcp:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtLocal"
                                            ID="txtLocal">
                                        </dxcp:ASPxTextBox>
                                    </td>
                                    <td style="padding-right: 5px; width: 70px;">
                                        <dxcp:ASPxTextBox runat="server" Width="100%" 
                                            ClientInstanceName="txtHoraInicioReal"
                                            ID="txtHoraInicioReal">
                                            <MaskSettings Mask="HH:mm" />
                                            <ValidationSettings Display="None">
                                            </ValidationSettings>
                                        </dxcp:ASPxTextBox>
                                    </td>
                                    <td style="padding-right: 5px; width: 70px;">
                                        <dxcp:ASPxTextBox runat="server" Width="100%" 
                                            ClientInstanceName="txtHoraTerminoReal"
                                            ID="txtHoraTerminoReal">
                                            <MaskSettings Mask="HH:mm" />
                                            <ValidationSettings Display="None">
                                            </ValidationSettings>
                                        </dxcp:ASPxTextBox>
                                    </td>
                                    <td style="padding-right: 5px;">
                                        <dxcp:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblTipoGrafico"
                                            Width="100%"  ID="rblTipoGrafico">
                                            <Paddings Padding="0px"></Paddings>
                                            <Items>
                                                <dxcp:ListEditItem Text="BurnDown" Value="D"></dxcp:ListEditItem>
                                                <dxcp:ListEditItem Text="BurnUp" Value="U"></dxcp:ListEditItem>
                                            </Items>
                                        </dxcp:ASPxRadioButtonList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" >
                                        <dxcp:ASPxCheckBox runat="server" CheckState="Checked" 
                            Checked="True" Text="Itens n&#227;o planejados influenciam no desempenho?"
                                            ValueType="System.String" ValueChecked="S" 
                            ValueUnchecked="N" ClientInstanceName="ckbItensNaoPlanejados" 
                             ID="ckbItensNaoPlanejados">
                                        </dxcp:ASPxCheckBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                            Width="100px"  ID="btnSalvar" 
                                            AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
                var mensagemErro = validaCamposFormulario();
	if(mensagemErro == &quot;&quot;)
               {
                       cbSalvar.PerformCallback();
               }
               else
               {
                      window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
               }
    
}"></ClientSideEvents>
                                        </dxcp:ASPxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
    </div>
    <dxcb:ASPxCallback ID="cbSalvar" runat="server" ClientInstanceName="cbSalvar" OnCallback="cbSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	e.processOnServer = false;
	var erro = s.cp_MensagemErro;
                var sucesso = s.cp_MensagemSucesso;
	if(erro != &quot;&quot;)
	{
                             window.top.mostraMensagem( erro, 'erro', true, false, null);
	}
                 else
                {
                            window.top.mostraMensagem( sucesso, 'sucesso', false, false, null);
                }
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
