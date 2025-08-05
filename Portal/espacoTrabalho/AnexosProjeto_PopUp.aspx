<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnexosProjeto_PopUp.aspx.cs"
    Inherits="_Compartilhada_AnexosProjeto_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/ASPxListbox.js"></script>
    <%-- <link href="../estilos/custom.css" rel="stylesheet" />--%>
    <script type="text/javascript" language="javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        window.name = "modal";

        var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
    
        function getSomatorioDasAlturasDosComponentes() {
            return (document.documentElement.clientHeight - document.getElementById('tabelaConteudo').offsetHeight + 50);
        }

        function desabilitarBotaoSemArquivo() {
            $("#btnCheckIn").addClass("dxbDisabled_MaterialCompact");
        }

        $(document).ready(function () {
            desabilitarBotaoSemArquivo()
        });

        function habilitarBtnCheckIn() {
            console.log("fluArquivo:", fluArquivo);
            $("#btnCheckIn").removeClass("dxbDisabled_MaterialCompact");
            $("#fluArquivo_ClearBox0Img").click(function () {
                $("#btnCheckIn").addClass("dxbDisabled_MaterialCompact");
            });
        }

        function validaNovoAnexo() {
            var mensagemErro_ValidaCamposFormulario = "";
            return mensagemErro_ValidaCamposFormulario;
        }

        function UpdateButtons() {
            try {

                btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
                btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0);
                btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
                btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "" && TipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
                capturaCodigosInteressados();
            } catch (e) { }
        }

        function habilitaBotoesListBoxes() {
            btnAddAll.SetEnabled(lbItensDisponiveis.GetItemCount() > 0);
            btnAddSel.SetEnabled(lbItensDisponiveis.GetSelectedItem() != null);

            btnRemoveAll.SetEnabled(lbItensSelecionados.GetItemCount() > 0);
            btnRemoveSel.SetEnabled(lbItensSelecionados.GetSelectedItem() != null);
        }
    </script>
    <style type="text/css">
        .btn {
            text-transform: capitalize !important;
        }

        .auto-style1 {
            width: 150px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server" target="modal">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" id="tabelaConteudo" cellpadding="0" border="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="padding-bottom: 5px">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="auto-style1">
                                                    <dxe:ASPxLabel runat="server" Text="Destino do anexo:" ClientInstanceName="lblOpcaoPastaOrigem"
                                                        ID="lblOpcaoPastaOrigem">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Nome da pasta de destino"
                                                        ID="lblPastaDestino">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="padding-top: 10px">
                                                    <dxe:ASPxCheckBox ID="ckLink" runat="server" CheckState="Unchecked"
                                                        Text="Disponibilizar o link deste documento para uso em outras bibliotecas">
                                                    </dxe:ASPxCheckBox>
                                                    <dxe:ASPxCheckBox ID="ckPastaPublica" runat="server" CheckState="Unchecked"
                                                        Text="Disponibilizar pasta e todos seus arquivos para acesso de todas as pessoas da organização">
                                                    </dxe:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 5px">
                                        <dxe:ASPxLabel runat="server" Text="Nome:" ID="ASPxLabel10">
                                        </dxe:ASPxLabel>
                                        <dxcp:ASPxUploadControl
                                            ID="fluArquivo"
                                            runat="server"
                                            OnFileUploadComplete="fluArquivo_FileUploadComplete"
                                            AutoStartUpload="false"
                                            ClientInstanceName="fluArquivo"
                                            onchange="habilitarBtnCheckIn()"
                                            Theme="MaterialCompact" FileUploadMode="OnPageLoad" UploadMode="Advanced" Width="100%">
                                        </dxcp:ASPxUploadControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 5px">
                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNomePasta"
                                            ID="txtNomePasta">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px">
                                        <dxe:ASPxLabel runat="server" Text="Palavras Chaves:" ID="ASPxLabel12">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="4000" ClientInstanceName="txtPalavraChave"
                                            ID="txtPalavraChave">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px">
                                        <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o:"
                                            ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxMemo ID="txtDescricaoNovoAnexo" runat="server" ClientInstanceName="txtDescricaoNovoAnexo" Width="100%" Height="100px" EnableClientSideAPI="True" AutoResizeWithContainer="True">
                                        </dxe:ASPxMemo>
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_MaterialCompact">
                                            <tr>
                                                <td align="left">
                                                    <dxe:ASPxLabel ID="lblContadorMemoNovoAnexo" runat="server"
                                                        ClientInstanceName="lblContadorMemoNovoAnexo" Font-Bold="True"
                                                        ForeColor="#999999">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right">
                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblIncluidoPor" Font-Bold="False"
                                                        ForeColor="Gray" ID="lblIncluidoPor">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblCheckoutPor" Font-Bold="False"
                                                        ForeColor="Gray" ID="lblCheckoutPor" Visible="False">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblTamanhoMaximo"
                                                        ForeColor="Gray" ID="lblTamanhoMaximo">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" align="right">
                                        
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            
        </div>

        <table border="0" style="width: 100%; position: absolute; bottom: 0px;">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnCheckOutForEdit" Text="Obter arquivo e bloquear edi&#231;&#227;o"
                                                            Width="250px" ID="btnCheckOutForEdit" Visible="False" AutoPostBack="False" CssClass="btn">
                                                            <ClientSideEvents Click="function(s, e) {
callbackCheckoutArquivo.PerformCallback();
}"></ClientSideEvents>
                                                        </dxe:ASPxButton>
                                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnCheckIn" Text="Devolver arquivo e liberar edi&#231;&#227;o"
                                                            Width="250px" ID="btnCheckIn" Visible="False"
                                                            AutoPostBack="False"
                                                            CssClass="btn">
                                                            <ClientSideEvents Click="function(s, e) {                                                                      
                                                                    fluArquivo.Upload();
                                                                    setTimeout(function(){ window.top.fechaModal3(); }, 2000);                                                                     
                                                               }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="paddings: 5px">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 60px">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarNovoAnexo"
                                                                            Text="Salvar" ID="btnSalvarNovoAnexo" OnClick="btnSalvarNovoAnexo_Click"
                                                                            Width="90px" CssClass="btn">
                                                                            <ClientSideEvents Click="function(s, e) {
	mensagem = validaNovoAnexo();
    if (mensagem != &quot;&quot;)
    {
        e.processOnServer = false;
    }
    else
    {
		pnLoading.Show();
		e.processOnServer = true;
    }
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 60px; padding-left: 5px;">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharNovoAnexo"
                                                                            Text="Fechar" ID="btnFecharNovoAnexo" Width="90px" CssClass="btn">
                                                                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal3();
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

        <dxlp:ASPxLoadingPanel ID="pnLoading" runat="server" ClientInstanceName="pnLoading">
        </dxlp:ASPxLoadingPanel>
        <dxcp:ASPxCallback ID="callbackCheckoutArquivo" runat="server" ClientInstanceName="callbackCheckoutArquivo" OnCallback="callbackCheckoutArquivo_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
          var url = &quot;../../_Processos/Visualizacao/ExportacaoDadosBiblioteca.aspx?exportType=&quot; + s.cpTipo + &quot;&amp;bInline=False&amp;fileName=&quot; + s.cpFileName;
          window.location = url ;
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
