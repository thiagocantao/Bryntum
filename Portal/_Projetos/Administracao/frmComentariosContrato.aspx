<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmComentariosContrato.aspx.cs" Inherits="administracao_frmAditivosContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (Trim(txtComentario.GetText()) == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O campo comentário deve ser informado.";
            }
            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
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
                lblCantCaraterOb.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_txtComentario(s, e) {
            try { return setMaxLength(s.GetInputElement(), 2000); }
            catch (e) { }
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
            txtComentario.SetText("");
            txtDataInclusao.SetText("");
            txtUsuarioInclusao.SetText("");
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoComentarioContrato;Comentario;DataInclusao;UsuarioInclusao;', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {
            var codigoComentarioContrato = (values[0] != null ? values[0] : "");
            var comentario = (values[1] != null ? values[1] : "");
            var dataInclusao = (values[2] != null ? values[2] : "");
            var usuarioInclusao = (values[3] != null ? values[3] : "");

            txtComentario.SetText(comentario);
            txtDataInclusao.SetText(dataInclusao);
            txtUsuarioInclusao.SetText(usuarioInclusao);
            desabilitaHabilitaComponentes();
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            txtComentario.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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

    </script>
    <style type="text/css">
        .style5 {
            width: 5px;
        }

        .style7 {
            width: 120px;
        }

        .style1 {
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                Width="100%" OnCallback="pnCallback_Callback">
                <PanelCollection>
                    <dxp:PanelContent runat="server">


                        <table class="headerGrid">
                            <tr>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel25" runat="server" Text="Nº Contrato:">
                                    </dxtv:ASPxLabel>
                                </td>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" Text="Tipo de Contrato:">
                                    </dxtv:ASPxLabel>
                                </td>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel28" runat="server" Text="Status:">
                                    </dxtv:ASPxLabel>
                                </td>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel27" runat="server" Text="Início da Vigência:">
                                    </dxtv:ASPxLabel>
                                </td>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel26" runat="server" Text="Término da Vigência:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtNumeroContrato" runat="server" ClientInstanceName="txtNumeroContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxTextBox>
                                </td>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtTipoContrato" runat="server" ClientInstanceName="txtTipoContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxTextBox>
                                </td>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtStatusContrato" runat="server" ClientInstanceName="txtStatusContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxTextBox>
                                </td>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtInicioVigencia" runat="server" ClientInstanceName="txtInicioVigencia" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxTextBox>
                                </td>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtTerminoVigencia" runat="server" ClientInstanceName="txtTerminoVigencia" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                            KeyFieldName="CodigoComentarioContrato" AutoGenerateColumns="False"
                            ID="gvDados"
                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                            OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                            OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" Width="100%">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}"
                                CustomButtonClick="function(s, e) 
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
}
"
                                Init="function(s, e) {
	  var sHeight = Math.max(0, document.documentElement.clientHeight) - 65;
        s.SetHeight(sHeight);
}"></ClientSideEvents>

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                            <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <HeaderTemplate>
                                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>")%>
                                    </HeaderTemplate>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataDateColumn Caption="Data Inclusão" FieldName="DataInclusao"
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="125px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Equals" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataDateColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao"
                                    ShowInCustomizationForm="True" VisibleIndex="2" Caption="Usuário Inclusão"
                                    Width="190px">
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Comentario" Name="Comentario"
                                    Caption="Comentário" VisibleIndex="3">
                                    <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                            <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                            <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                ShowHeaderFilterBlankItems="False"></Settings>

                            <SettingsText></SettingsText>
                        </dxwgv:ASPxGridView>
                        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                            CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="620px"
                            ID="pcDados">
                            <ContentStyle>
                                <Paddings Padding="5px"></Paddings>
                            </ContentStyle>

                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                            <ContentCollection>
                                <dxpc:PopupControlContentControl runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="style5">&nbsp;</td>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lblObservacoes" runat="server"
                                                                        ClientInstanceName="lblObservacoes" EncodeHtml="False"
                                                                        Text="Comentário: &amp;nbsp;">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxLabel ID="lblCantCaraterOb" runat="server"
                                                                        ClientInstanceName="lblCantCaraterOb"
                                                                        ForeColor="Silver" Text="0">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxLabel ID="lblDe250Ob" runat="server" ClientInstanceName="lblDe250Ob"
                                                                        EncodeHtml="False" ForeColor="Silver"
                                                                        Text="&amp;nbsp;de 2000">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtComentario" runat="server"
                                                                        ClientInstanceName="txtComentario"
                                                                        Rows="10" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	onInit_txtComentario(s, e);
}" />
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                        <tr>
                                                                            <td class="style7">
                                                                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                                                                    Text="Data Inclusão:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server"
                                                                                    Text="Incluído Por:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style7" style="padding-right: 10px;">
                                                                                <dxe:ASPxTextBox ID="txtDataInclusao" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtDataInclusao" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                    Width="100%">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txtUsuarioInclusao" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtUsuarioInclusao"
                                                                                    Width="100%">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnSalvar"
                                                                                        Text="Salvar" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                        <Paddings Padding="0px" />
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
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="style5">&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                        </dxpc:ASPxPopupControl>
                        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido"
                            HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                            Width="270px" ID="pcUsuarioIncluido"
                            CloseAction="None" Modal="True">
                            <ContentCollection>
                                <dxpc:PopupControlContentControl runat="server">
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
                    </dxp:PanelContent>
                </PanelCollection>

                <ClientSideEvents EndCallback="function(s, e) 
{	
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Comentário incluído com sucesso!&quot;);
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Comentário alterado com sucesso!&quot;);
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Comentário excluído com sucesso!&quot;);	
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
}"></ClientSideEvents>
            </dxcp:ASPxCallbackPanel>
        </div>
    </form>
</body>
</html>

