<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormulario_Anexo.aspx.cs"
    Inherits="wfRenderizaFormulario_Anexo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Anexo formulário</title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>

    <script type="text/javascript" language="javascript">
    window.name="modal";
    var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
    function validaNovoAnexo()
    {
        var mensagemErro_ValidaCamposFormulario = "";
        if (document.getElementById("pnContainer_fluArquivo") != null)
        {
            if (document.getElementById("pnContainer_fluArquivo").getAttribute("value") == "")
            {
                mensagemErro_ValidaCamposFormulario = "Selecione um Arquivo para ser anexado";
            }
        }
        return mensagemErro_ValidaCamposFormulario;
    }

    </script>

</head>
<body>
    <form id="form1" runat="server" target="modal">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 10px; height: 5px">
                    </td>
                    <td>
                    </td>
                    <td style="width: 10px; height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pnContainer" runat="server" ClientInstanceName="pnContainer"
                            Width="100%" ShowHeader="False">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Nome:"  ID="ASPxLabel10">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:FileUpload runat="server"  Height="20px"
                                                        Width="100%" ID="fluArquivo"></asp:FileUpload>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o:" 
                                                        ID="ASPxLabel11">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtDescricaoNovoAnexo"
                                                         ID="txtDescricaoNovoAnexo">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblIncluidoPor" runat="server" ClientInstanceName="lblIncluidoPor"
                                                        Font-Bold="False"  ForeColor="Gray">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblTamanhoMaximo" runat="server" ClientInstanceName="lblTamanhoMaximo"
                                                         ForeColor="Gray">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 35px" valign="bottom" align="right">
                                                    &nbsp;<table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px">
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarNovoAnexo"
                                                                        Text="Salvar"  ID="btnSalvarNovoAnexo" OnClick="btnSalvarNovoAnexo_Click" Width="85px">
                                                                        <ClientSideEvents Click="function(s, e) {
	mensagem = validaNovoAnexo();
    if (mensagem != &quot;&quot;)
    {
        e.processOnServer = false;
		window.top.mostraMensagem(mensagem, 'Atencao', true, false, null);
    }
    else
    {
		e.processOnServer = true;
    }
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 60px">
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharNovoAnexo"
                                                                        Text="Fechar"  ID="btnFecharNovoAnexo" Width="85px">
                                                                        <ClientSideEvents Click="function(s, e) {
	
	if(window.parent.fechaModal)
	    window.parent.fechaModal();
	else
	    window.close();
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfGeral" runat="server" />
    </form>
</body>
<script type="text/javascript" language="javascript">
    if (document.getElementById('hfGeral').value=='OK')
    {
        window.returnValue = 'OK';
        if (window.parent.fechaModal) {
            window.parent.retornoModal = 'OK';
            window.parent.fechaModal();
        }
        else
            window.close();
    }
</script>
</html>
