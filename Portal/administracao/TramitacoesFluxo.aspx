<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TramitacoesFluxo.aspx.cs" Inherits="administracao_TramitacoesFluxo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Resize textarea {
            resize: both;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var label;

        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function redimensionaFrameTramitacao() {
            var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
            document.getElementById("pnCallback_ASPxPageControl1_divGestao").style.height = sHeight + "px";
            document.getElementById("pnCallback_ASPxPageControl1_divGestao").style.overflow = "auto";

            document.getElementById("pnCallback_ASPxPageControl1_divComentarios").style.height = sHeight + "px";
            document.getElementById("pnCallback_ASPxPageControl1_divComentarios").style.overflow = "auto";
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (TipoOperacao == 'Parecer' || TipoOperacao == "GravarParecer")
                return "";

            if (ddlUsuario.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O usuário deve ser informado.";
            }

            if (ddlDataPrevista.GetText() == "" || ddlDataPrevista.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data prevista deve ser informada.";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
        }
        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        // **************************************************************************************
        // - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
        // **************************************************************************************

        function LimpaCamposFormulario() {
            txtStatus.SetText("Pendente de Envio");
            ddlUsuario.SetValue(null);
            ddlDataEnvio.SetValue(null);
            ddlDataPrevista.SetValue(null);
            ddlDataParecer.SetValue(null);
            txtComentarios.SetText(gvDados.cp_TextPadrao);
            txtParecer.SetText("");
            lblCantCarater.SetText(txtComentarios.GetText().length);
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoTramitacaoEtapaFluxo;UsuarioParecer;DataSolicitacaoParecer;ComentarioSolicitacaoParecer;DataPrevistaParecer;DataParecer;Parecer;StatusParecer', MontaCamposFormulario);
        }

        function MontaCamposFormulario(values) {
            desabilitaHabilitaComponentes();

            var codigoTramitacaoEtapaFluxo = (values[0] != null ? values[0] : "");
            var usuarioParecer = values[1];
            var dataSolicitacaoParecer = values[2];
            var comentarioSolicitacaoParecer = values[3];
            var dataPrevistaParecer = values[4];
            var dataParecer = values[5];
            var parecer = values[6];
            var statusParecer = values[7];

            txtStatus.SetText(statusParecer);
            ddlDataEnvio.SetValue(dataSolicitacaoParecer);
            ddlDataPrevista.SetValue(dataPrevistaParecer);
            ddlDataParecer.SetValue(dataParecer);
            txtComentarios.SetText(comentarioSolicitacaoParecer);
            txtParecer.SetText(parecer);
            lblCantCarater.SetText(txtComentarios.GetText().length);

            ddlUsuario.SetText(usuarioParecer);
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            ddlUsuario.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
            ddlDataPrevista.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            txtComentarios.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
        }
        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            onClick_btnCancelar();
        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                label.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObjeto(s, e, length) {
            try {
                return setMaxLength(s.GetInputElement(), length);
            }
            catch (e) { }
        }

        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
            if (codigoAcao == 0) {
                if (Trim(txtParecerUsuario.GetText()) == "") {
                    window.top.mostraMensagem("Informe e grave o comentário antes de enviar a tramitação!", 'Atencao', true, false, null);
                    return false;
                } else if (pnCallback.cp_EnviarParecer != "S") {
                    window.top.mostraMensagem("Grave o comentário antes de enviar a tramitação!", 'Atencao', true, false, null);
                    return false;
                }
            }

            return true;
        }

        function abreAnexos(s, e) {
            var codigoParecer = s.GetRowKey(e.visibleIndex);
            var urlAnexos = window.top.pcModal.cp_Path + "espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&RO=S&TA=PT&ID=" + codigoParecer + "&ALT=405";

            window.top.showModal(urlAnexos, 'Anexos', 800, 485, '', null);
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .style2 {
            width: 100%;
        }

        .style4 {
            width: 300px;
            padding-right: 5px;
        }

        .style5 {
            width: 110px;
        }

        .style6 {
            width: 10px;
            height: 10px;
        }

        .style7 {
            width: 215px;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server"
            ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback1" Width="100%">
            <ClientSideEvents EndCallback="function(s, e) 
{
                
    if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Usuário incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Usuário alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Usuário excluído com sucesso!&quot;);
	else if(&quot;Parecer&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Solicitações enviadas com sucesso!&quot;);
    else if(&quot;GravarParecer&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Comentário registrado com sucesso!&quot;);

    redimensionaFrameTramitacao();
 }" />
            <PanelCollection>
                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                    <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
                        Width="100%">
                        <TabPages>
                            <dxtc:TabPage Name="tbGestao" Text="Tramitações">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div runat="server" id="divGestao">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>

                                                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvDados"
                                                            KeyFieldName="CodigoTramitacaoEtapaFluxo"
                                                            OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                                            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%">

                                                            <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                                                            <ClientSideEvents CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }
     else if(e.buttonID == &quot;btnAnexos&quot;)
     {
	abreAnexos(s, e);
     }
	
     
}
"
                                                                FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" />

                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <Settings VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />

                                                            <SettingsCommandButton>
                                                                <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                            </SettingsCommandButton>

                                                            <SettingsPopup>
                                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                            </SettingsPopup>

                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Width="35px" ShowSelectCheckbox="True" Caption=" ">
                                                                    <HeaderTemplate>
                                                                        <input onclick="gvDados.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                                            type="checkbox" />

                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1" Width="115px">
                                                                    <CustomButtons>
                                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                            </Image>
                                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                            </Image>
                                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                                            <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                                            </Image>
                                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnAnexos" Text="Visualizar Anexos">
                                                                            <Image ToolTip="Visualizar Anexos" Url="~/imagens/anexar.png">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <HeaderTemplate>
                                                                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Usuário" FieldName="UsuarioParecer"
                                                                    Name="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                    <Settings AutoFilterCondition="Contains"></Settings>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Data Envio"
                                                                    FieldName="DataSolicitacaoParecer" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Width="95px">
                                                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                                    </PropertiesTextEdit>
                                                                    <Settings AllowAutoFilter="False" />

                                                                    <Settings AllowAutoFilter="False"></Settings>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Data Comentário" FieldName="DataParecer"
                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="115px">
                                                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                                    </PropertiesTextEdit>
                                                                    <Settings AllowAutoFilter="False" />

                                                                    <Settings AllowAutoFilter="False"></Settings>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusParecer"
                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="200px">
                                                                    <Settings AllowAutoFilter="False"></Settings>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioParecer"
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="ComentarioSolicitacaoParecer"
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="DataPrevistaParecer"
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="Parecer"
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxButton ID="btnSolicitarParecer" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnSolicitarParecer"
                                                                        Text="Enviar Solicitações" Width="140px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(gvDados.GetSelectedRowCount() == 0)
		window.top.mostraMensagem(&quot;Nenhum usuário foi selecionado!&quot;, 'Atencao', true, false, null);
	else
	{
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Parecer&quot;);
		TipoOperacao = &quot;Parecer&quot;;

	    if (window.onClick_btnSalvar)
		    onClick_btnSalvar();
	}
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxtv:ASPxButton ID="btnImprimir" runat="server" AutoPostBack="False"
                                                                        ClientInstanceName="btnImprimir"
                                                                        Text="Imprimir" Width="140px">
                                                                        <ClientSideEvents Click="function(s, e) {
lpLoading.Show();              
cbExportacao.PerformCallback();
}" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tbComentarios" Text="Comentários" Enabled="False">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div runat="server" id="divComentarios">
                                            <table cellpadding="0" cellspacing="0" class="style2">
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="ASPxLabel10" runat="server" Text="Solicitação:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxMemo ID="txtComentarioSolicitacaoParecer" runat="server" ClientInstanceName="txtComentarioSolicitacaoParecer" Height="133px" ReadOnly="True" Width="100%">
                                                            <ClientSideEvents GotFocus="function(s, e) {
	label = lblCantParecer;
}"
                                                                Init="function(s, e) {
	onInit_mmObjeto(s, e, 4000);
}" />
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                            <tr>
                                                                <td class="style7" valign="bottom">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" Text="Comentário:">
                                                                    </dxtv:ASPxLabel>
                                                                    <dxtv:ASPxLabel ID="lblCantParecer0" runat="server" ClientInstanceName="lblCantParecer" ForeColor="Silver" Text="0">
                                                                        <ClientSideEvents Init="function(s, e) {
	s.SetText(txtParecerUsuario.GetText().length);
}" />
                                                                    </dxtv:ASPxLabel>
                                                                    <dxtv:ASPxLabel ID="lblDe252" runat="server" ClientInstanceName="lblDe250" EncodeHtml="False" ForeColor="Silver" Text="&amp;nbsp;de 4000">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                                <td align="right" style="padding-bottom: 2px; padding-top: 5px;">
                                                                    <dxtv:ASPxImage ID="btnAnexo" runat="server" Cursor="pointer" ImageUrl="~/imagens/anexar.png" ToolTip="Anexar arquivos ao parecer">
                                                                    </dxtv:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <dxtv:ASPxMemo ID="txtParecerUsuario" runat="server" ClientInstanceName="txtParecerUsuario" Height="133px" Rows="16" Width="100%">
                                                            <ClientSideEvents GotFocus="function(s, e) {
	label = lblCantParecer;
}"
                                                                Init="function(s, e) {
	onInit_mmObjeto(s, e, 4000);
}" />
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btnSalvarParecer" runat="server" AutoPostBack="False"
                                                            ClientInstanceName="btnSalvarParecer" Text="Salvar" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if(txtParecerUsuario.GetText() == &quot;&quot;)
	{
		window.top.mostraMensagem(&quot;Campo comentário é obrigatório.&quot;, 'Atencao', true, false, null);
	}
    else
    {		
    	hfGeral.Set(&quot;TipoOperacao&quot;, &quot;GravarParecer&quot;);
		TipoOperacao = &quot;GravarParecer&quot;;

		if (window.onClick_btnSalvar)
	    	onClick_btnSalvar();
    }

}" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                    </dxtc:ASPxPageControl>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <dxtv:ASPxCallback ID="cbExportacao" runat="server"
                        ClientInstanceName="cbExportacao" OnCallback="cbExportacao_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
lpLoading.Hide();	
window.location = '../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=false';
}" />
                    </dxtv:ASPxCallback>
                    <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server"
                        ClientInstanceName="lpLoading"
                        Height="63px" Width="144px">
                    </dxtv:ASPxLoadingPanel>
                </dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido"
            HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False"
            Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="" align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados"
            CloseAction="None" HeaderText="Detalhes"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="750px" AllowDragging="True">
            <ContentStyle>
                <Paddings Padding="5px" />
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server"
                    SupportsDisabledAttribute="True">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style2">
                                    <tr>
                                        <td class="style4">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                Text="Status:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                Text="Usuário:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">
                                            <dxe:ASPxTextBox ID="txtStatus" runat="server" ClientEnabled="False"
                                                ClientInstanceName="txtStatus"
                                                MaxLength="50" Style="margin-bottom: 0px" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlUsuario" runat="server"
                                                ClientInstanceName="ddlUsuario"
                                                IncrementalFilteringMode="Contains" Width="100%">
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="Nome" Width="300px" />
                                                    <dxe:ListBoxColumn Caption="Email" Width="200px" />
                                                </Columns>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorText="*">
                                                    <RequiredField IsRequired="True" />
                                                    <RequiredField IsRequired="True"></RequiredField>
                                                </ValidationSettings>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style2">
                                    <tr>
                                        <td class="style5" valign="top">
                                            <table cellpadding="0" cellspacing="0" class="style2">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                            Text="Data Prevista:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="ddlDataPrevista" runat="server"
                                                            ClientInstanceName="ddlDataPrevista" DisplayFormatString="dd/MM/yyyy"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                            UseMaskBehavior="True" Width="100%">
                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                            </CalendarProperties>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />

                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                            Text="Data Envio:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="ddlDataEnvio" runat="server" ClientEnabled="False"
                                                            ClientInstanceName="ddlDataEnvio" DisplayFormatString="dd/MM/yyyy"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                            UseMaskBehavior="True" Width="100%">
                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                            </CalendarProperties>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />

                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                            Text="Data Comentário:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="ddlDataParecer" runat="server" ClientEnabled="False"
                                                            ClientInstanceName="ddlDataParecer" DisplayFormatString="dd/MM/yyyy"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                            UseMaskBehavior="True" Width="100%">
                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                            </CalendarProperties>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />

                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="padding-left: 5px;">
                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                            Text="Solicitação:">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel ID="lblCantCarater" runat="server"
                                                            ClientInstanceName="lblCantCarater"
                                                            ForeColor="Silver" Text="0">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                                                            EncodeHtml="False" ForeColor="Silver"
                                                            Text="&amp;nbsp;de 4000">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 5px;">
                                                        <dxe:ASPxMemo ID="txtComentarios" runat="server" CssClass="Resize"
                                                            ClientInstanceName="txtComentarios"
                                                            Rows="4" Width="100%">
                                                            <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e, 4000);
}"
                                                                GotFocus="function(s, e) {
	label = lblCantCarater;
}" />
                                                            <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e, 4000, lblCantCarater);
}"></ClientSideEvents>

                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 5px;">
                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                            Text="Comentário:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 5px;">
                                                        <dxe:ASPxMemo ID="txtParecer" runat="server" ClientEnabled="False"
                                                            ClientInstanceName="txtParecer" Rows="4" Height="100px"
                                                            Width="100%">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                    ClientInstanceName="btnSalvar"
                                                    Text="Salvar" Width="100px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                    <Paddings Padding="0px" />
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                    ClientInstanceName="btnFechar"
                                                    Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                        PaddingRight="0px" PaddingTop="0px" />
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

    </form>
    <script type="text/javascript">
        redimensionaFrameTramitacao();
    </script>
</body>
</html>

