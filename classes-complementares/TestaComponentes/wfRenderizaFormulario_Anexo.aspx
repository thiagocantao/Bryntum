<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormulario_Anexo.aspx.cs"
    Inherits="wfRenderizaFormulario_Anexo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Anexo formulário</title>

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
                        <dx:ASPxRoundPanel ID="pnContainer" runat="server" ClientInstanceName="pnContainer"
                            Width="100%" ShowHeader="False">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel runat="server" Text="Nome:" Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel10">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:FileUpload runat="server" Font-Names="Verdana" Font-Size="8pt" Height="20px"
                                                        Width="100%" ID="fluArquivo"></asp:FileUpload>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel runat="server" Text="Descri&#231;&#227;o:" Font-Names="Verdana" Font-Size="8pt"
                                                        ID="ASPxLabel11">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtDescricaoNovoAnexo"
                                                        Font-Names="Verdana" Font-Size="8pt" ID="txtDescricaoNovoAnexo">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblIncluidoPor" runat="server" ClientInstanceName="lblIncluidoPor"
                                                        Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Gray">
                                                    </dx:ASPxLabel>
                                                    <dx:ASPxLabel ID="lblTamanhoMaximo" runat="server" ClientInstanceName="lblTamanhoMaximo"
                                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Gray">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 35px" valign="bottom" align="right">
                                                    &nbsp;<table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px">
                                                                    <dx:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarNovoAnexo"
                                                                        Text="Salvar" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvarNovoAnexo" OnClick="btnSalvarNovoAnexo_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	mensagem = validaNovoAnexo();
    if (mensagem != &quot;&quot;)
    {
        e.processOnServer = false;
		alert(mensagem);
    }
    else
    {
		e.processOnServer = true;
    }
}"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td style="width: 10px">
                                                                </td>
                                                                <td style="width: 60px">
                                                                    <dx:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharNovoAnexo"
                                                                        Text="Fechar" Font-Names="Verdana" Font-Size="8pt" ID="btnFecharNovoAnexo">
                                                                        <ClientSideEvents Click="function(s, e) {
	window.close();
}"></ClientSideEvents>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
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
        window.returnValue = 'OK'
        window.close();
    }
</script>
</html>
