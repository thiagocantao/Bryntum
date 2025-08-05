<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormulario.aspx.cs"
    Inherits="wfRenderizaFormulario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="estilos/custom.css" rel="stylesheet" />
    <style type="text/css">
        #tdVersao {
            color: #575757;
        }

        .dxeTextBoxSys.dxeTextBox_MaterialCompact.dxeReadOnly_MaterialCompact.dxeTextBoxDefaultWidthSys.dxeDisabled_MaterialCompact,
        .dxeTextBoxSys.dxeTextBox_MaterialCompact.dxeReadOnly_MaterialCompact.dxeTextBoxDefaultWidthSys.dxeDisabled_MaterialCompact .dxeEditArea_MaterialCompact.dxeDisabled_MaterialCompact,
        .dxeButtonEditSys.dxeButtonEdit_MaterialCompact.dxeReadOnly_MaterialCompact.dxeDisabled_MaterialCompact,
        .dxeButtonEditSys.dxeButtonEdit_MaterialCompact.dxeReadOnly_MaterialCompact.dxeDisabled_MaterialCompact .dxeEditArea_MaterialCompact.dxeEditAreaSys.dxeDisabled_MaterialCompact {
            background-color: #f2f2f2 !important;
            color: #444444 !important;
        }
    </style>
    <title>Untitled Page</title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;
        var retornoModal = null;
        var retornoModalTexto = null;
        var retornoModalValor = null;
        var componenteLOV = null;

        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, objParam) {
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;

            myObject = objParam;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();
        }

        function fechaModal() {
            pcModal.Hide();
        }

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
            retornoModalTexto = null;
            retornoModalValor = null;
        }

        function mostrarLov(s, e, codigoLista) {
            componenteLOV = s;
            var valor = s.GetValue() != null ? s.GetValue() : "";
            var texto = s.GetText();
            showModal("lovFormulario.aspx?CL=" + codigoLista + "&Value=" + valor + '&Text=' + texto + '&' + callbackWF.cp_QS, "Lista de valores", 680, 200, null);
        }

        function atribuiResultadoLov() {
            componenteLOV.ClearItems();
            componenteLOV.AddItem(retornoModalTexto, retornoModalValor);
            componenteLOV.SetSelectedIndex(0);
        }


        function conteudoCampoAlterado() {
            existeConteudoCampoAlterado = true;
        }

        function executaCallbackWF() {
            if (window.parent.hfGeralWorkflow) {
                // copia o 'CodigoInstanciaWf' da frame Fluxo para a frame do formulário atual
                var CodigoInstanciaWf = window.parent.hfGeralWorkflow.Get('CodigoInstanciaWf');
                if (CodigoInstanciaWf.toString() == "-1")
                    callbackWF.PerformCallback();
            }
        }

        function fechaTelaPosSalvar() {
            if (callbackWF.cp_FechaModal == 'S')
                window.top.fechaModal();
        }

        function executaFuncaoTelaPai(codigoProjeto, codigoForm) {
            if (window.parent.funcaoPosSalvarFormulario)
                window.parent.funcaoPosSalvarFormulario(codigoProjeto, codigoForm);
        }

        function abreCrono(param) {
            if (hfSessao.Get('AcessaCrono') == 'S')
                window.open(hfSessao.Get('UrlCrono'), 'framePrincipal');
            else {
                window.top.mostraMensagem(traducao.wfRenderizaFormulario_acesso_negado_para_editar_o_cronograma_, 'Atencao', true, false, null);
            }
        }

        function mudaVersao(urlFormulario) {
            lpLoading.Show();
            window.open(urlFormulario, '_self');
        }

        function abreVersoes() {
            pcVersoes.Show();
        }

        function abreAssinaturas() {
            document.body.style.cursor = 'wait';
            callbackVerificacaoAssinatura.PerformCallback('');
            //pcAssinaturas.Show();
        }

        function ImprimeFormAssinado(param) {
            var codigoFormulario = -1;
            var parametros = param.split("&");
            for (var i = 0; i < parametros.length; i++) {
                var p = parametros[i].toLowerCase();
                if (p.substring(0, 3) == "cf=") {
                    codigoFormulario = parseInt(p.replace("cf=", ""));
                    break;
                }
            }
            document.body.style.cursor = 'wait';
            callbackVerificacaoAssinatura.PerformCallback(codigoFormulario);
        }

        function desbloqueiaFormulario() {
            if (callbackVerificacaoAssinatura.cp_Desbloqueio == 'S') {
                window.top.callbackDesbloqueio.PerformCallback(callbackVerificacaoAssinatura.cp_CodigoFormulario);

                setTimeout(function () {
                    aguarde();
                }, 1000);
            }
        }

        function aguarde() {

        }
    </script>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .buttonWithoutImage .dxbButton {
            background-image: none;
        }
    </style>
</head>
<body style="margin-left: 5px; margin-right: 5px; margin-bottom: 0px; margin-top: 0px;" onbeforeunload="desbloqueiaFormulario();">
    <div id="divAlturaFormulario" style="overflow: auto">
        <form id="form1" runat="server">
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="style1"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnExportarGrid" runat="server">
                                <table cellpadding="0" cellspacing="0" id="tbBotoes" runat="server"
                                    style="width: 100%">
                                    <tr runat="server">
                                        <td runat="server" style="padding: 3px" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent"
                                                            ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                            OnItemClick="menu_ItemClick">
                                                            <Paddings Padding="0px" />
                                                            <Items>
                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                            ToolTip="Exportar para HTML">
                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image IconID="actions_reset_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                            </Items>
                                                            <ItemStyle Cursor="pointer">
                                                                <HoverStyle>
                                                                    <border borderstyle="None" />
                                                                </HoverStyle>
                                                                <Paddings Padding="0px" />
                                                            </ItemStyle>
                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                <SelectedStyle>
                                                                    <border borderstyle="None" />
                                                                </SelectedStyle>
                                                            </SubMenuItemStyle>
                                                            <Border BorderStyle="None" />
                                                        </dxm:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvGridPrincipal" ID="ASPxGridViewExporter1"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                <Styles>
                                    <Default></Default>

                                    <Header></Header>

                                    <Cell></Cell>

                                    <GroupFooter Font-Bold="True"></GroupFooter>

                                    <Title Font-Bold="True"></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>

                            <dxhf:ASPxHiddenField ID="hfSessao" runat="server"
                                ClientInstanceName="hfSessao">
                            </dxhf:ASPxHiddenField>
                        </td>
                    </tr>
                </table>
            </div>
            <dxpc:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal"
                HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                        <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow: auto; padding: 0px; margin: 0px; height: 100%;"></iframe>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents PopUp="function(s, e) {
                            window.document.getElementById('frmModal').dialogArguments = myObject;
	                        document.getElementById('frmModal').src = urlModal;
                        }"
                    Closing="function(s, e) {
                            if(retornoModal != null)
                                atribuiResultadoLov();
                             resetaModal();
                        }" />
                <ContentStyle>
                    <Paddings Padding="5px" />
                </ContentStyle>
            </dxpc:ASPxPopupControl>
            <dxcp:ASPxPopupControl ID="pcVersoes" runat="server"
                ClientInstanceName="pcVersoes" CloseAction="CloseButton"
                HeaderText="Versões" PopupElementID="tbVersao"
                Width="600px" MaxHeight="350px" ScrollBars="Auto" MinHeight="200px" AllowDragging="True" AllowResize="True" OnCustomJSProperties="pcVersoes_CustomJSProperties">
                <ClientSideEvents EndCallback="function(s, e) {
var tdVersao = document.querySelector('#tdVersao');
if(tdVersao)
tdVersao.innerText = pcVersoes.cp_TextoVersao;
}" />
                <ContentCollection>
                    <dxcp:PopupControlContentControl ID="PopupControlContentControl1" runat="server"></dxcp:PopupControlContentControl>
                </ContentCollection>
            </dxcp:ASPxPopupControl>
            <dxcp:ASPxPopupControl ID="pcAssinaturas" runat="server"
                ClientInstanceName="pcAssinaturas" CloseAction="CloseButton"
                HeaderText="Assinaturas" PopupElementID="tdAssinatura"
                Width="600px" MaxHeight="350px" ScrollBars="Auto" MinHeight="200px" PopupAction="None">
                <ContentCollection>
                    <dxcp:PopupControlContentControl ID="PopupControlContentControl9" runat="server"></dxcp:PopupControlContentControl>
                </ContentCollection>
            </dxcp:ASPxPopupControl>
            <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
            </dxcp:ASPxLoadingPanel>
        </form>
    </div>
    <dxcb:ASPxCallback ID="callbackWF" runat="server" ClientInstanceName="callbackWF"
        OnCallback="callbackWF_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	try
	{
		wfTela.eventoPosSalvar(s.cp_CodigoInstanciaWf);
	}catch(e){}
}" />
    </dxcb:ASPxCallback>



    <dxcb:ASPxCallback ID="callbackReload" runat="server" ClientInstanceName="callbackReload"
        OnCallback="callbackReload_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	
	try
	{
		wfTela.eventoPosSalvar(s.cp_CodigoInstanciaWf);
	}catch(e){}
	if(s.cp_URL != null &amp;&amp; s.cp_URL != '')
		window.parent.location = s.cp_URL;
}"
            BeginCallback="function(s, e) {
	window.top.lpAguardeMasterPage.Show();
}" />
    </dxcb:ASPxCallback>
    <dxcp:ASPxCallback ID="callbackVerificacaoAssinatura" runat="server" ClientInstanceName="callbackVerificacaoAssinatura" OnCallback="callbackVerificacaoAssinatura_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
            if(e.result){
                window.top.mostraMensagem(e.result, 'Atencao', true, false, null);
            }
            else{
                if(e.parameter == ''){
                    pcAssinaturas.Show();
                }
                else{
                    var url = './_CertificadoDigital/DownloadFormularioAssinado.aspx?cf=' + e.parameter;
                    window.location = url;
                }
            }
            document.body.style.cursor = 'default';
}" />
    </dxcp:ASPxCallback>

<%--   <script language="javascript" type="text/javascript">
       var height = Math.max(0, document.documentElement.clientHeight) - 1;
       document.getElementById("divAlturaFormulario").style.height =  height +  "px";
   </script>--%>

</body>







</html>
