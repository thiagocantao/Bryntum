<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroAditivoContrato.aspx.cs" Inherits="Administracao_CadastroAditivoContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
<link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">

    function validaCamposFormulario() {
        // Esta função tem que retornar uma string.
        // "" se todas as validações estiverem OK
        // "<erro>" indicando o que deve ser corrigido
        mensagemErro_ValidaCamposFormulario = "";
        var numAux = 0;
        var mensagem = "";
               
        if (ddlContrato.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O contrato a ser aditivado deve ser informado.";
        }
        if (ddlTipoInstrumento.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O tipo do instrumento deve ser informado.";
        }
        if (txtNumeroInstrumento.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") A número do instrumento deve ser informado.";
        }
        if (ddlAditivar.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O campo aditivar deve ser informado.";
        }
        if ((ddlAditivar.GetValue() == 'PR' || ddlAditivar.GetValue() == 'PV' || ddlAditivar.GetValue() == 'TM') && ddlDataPrazo.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O novo prazo final deve ser informado.";
        }
        if ((ddlAditivar.GetValue() == 'VL' || ddlAditivar.GetValue() == 'PV') && txtValorAditivo.GetText() == '') {
            numAux++;
            mensagem += "\n" + numAux + ") O valor do aditivo deve ser informado.";
        }
        if (mensagem != "") {
            mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
        }

        return mensagemErro_ValidaCamposFormulario;
    }

    function verificaTipoContrato() {
        if (ddlDataPrazo.cp_RO.toString() != "S") {
            var tipoContrato = ddlAditivar.GetValue();

            if (tipoContrato == 'PR') {
                ddlDataPrazo.SetEnabled(true);
                txtValorAditivo.SetEnabled(false);
                txtValorAditivo.SetText("");
                txtNovoValor.SetText("");
                txtNovoValor.SetEnabled(false);
            } else if (tipoContrato == 'VL') {
                ddlDataPrazo.SetEnabled(false);
                txtValorAditivo.SetEnabled(true);
                ddlDataPrazo.SetValue(null);
                txtNovoValor.SetEnabled(false);

                if (txtValorAditivo.GetValue() == null || parseFloat(txtValorAditivo.GetValue().replace(",", ".")) == 0)
                    txtNovoValor.SetText("");

            } else if (tipoContrato == 'PV') {
                ddlDataPrazo.SetEnabled(true);
                txtValorAditivo.SetEnabled(true);
                txtNovoValor.SetEnabled(false);
                txtValorAditivo.SetText("");

                if (txtValorAditivo.GetValue() == null || parseFloat(txtValorAditivo.GetValue().replace(",", ".")) == 0)
                    txtNovoValor.SetText("");

            } else if (tipoContrato == 'TM') {
                ddlDataPrazo.SetEnabled(true);
                txtNovoValor.SetEnabled(true);
                txtValorAditivo.SetEnabled(false);
                txtValorAditivo.SetText("");
            } else {
                txtValorAditivo.SetEnabled(false);
                txtValorAditivo.SetText("");
                txtNovoValor.SetText("");
                ddlDataPrazo.SetEnabled(false);
                ddlDataPrazo.SetValue(null);
                txtNovoValor.SetEnabled(false);
            }
        }
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

    function onInit_mmObjeto(s, e) {
        try { return setMaxLength(s.GetInputElement(), 2000); }
        catch (e) { }
    }

    function onInit_mmObservacoes(s, e) {
        try { return setMaxLength(s.GetInputElement(), 2000); }
        catch (e) { }
    }

    //----------- Mensagem modificação con sucesso..!!!
    function mostraDivSalvoPublicado(acao) {
        if (acao.toUpperCase().indexOf('SUCESSO'))
            window.top.mostraMensagem(acao, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(acao, 'erro', true, false, null);

        if (callbackSalvar.cp_Status == "1")
            setTimeout('finalizaEdicao();', 1500);
    }

    function finalizaEdicao() {
        pcUsuarioIncluido.Hide();
        window.top.retornoModal = 'S';
        try {
            window.top.fechaModal();
        }
        catch (e)
		    { }

		}

		function getDadosWF() {
		    if (hfWF.Get("CodigoWF") == "-1" && window.parent.callbackWF) {
		        var valorWF = window.parent.callbackWF.cp_CWF;
                var valorCI = window.parent.callbackWF.cp_CI;

                if (valorWF == null || valorWF == "")
                    valorWF = "-1";

                if (valorCI == null || valorCI == "")
                    valorCI = "-1";

                hfWF.Set("CodigoWF", valorWF);
		        hfWF.Set("CodigoCI", valorCI);

		        callbackSalvar.PerformCallback();
		    }
		}


		function montaCampos(s, e) {

		    if (s.cp_CodigoAditivoContrato != null && s.cp_CodigoAditivoContrato != "") {
		        hfWF.Set("CodigoAditivo", s.cp_CodigoAditivoContrato);
		        ddlContrato.SetValue(s.cp_CodigoContrato);
		        ddlTipoInstrumento.SetValue(s.cp_CodigoTipoContratoAditivo);
		        txtNumeroInstrumento.SetText(s.cp_NumeroContratoAditivo);
		        ddlAditivar.SetValue(s.cp_TipoAditivo);
		        verificaTipoContrato();
		        txtNovoValor.SetText(s.cp_NovoValorContrato);
		        ddlDataPrazo.SetText(s.cp_NovaDataVigencia);
		        mmMotivo.SetText(s.cp_DescricaoMotivoAditivo);
		        txtDataInclusao.SetText(s.cp_DataInclusao);
		        txtUsuarioInclusao.SetText(s.cp_UsuarioInclusao);
		        txtDataAprovacao.SetText(s.cp_DataAprovacaoAditivo);
		        txtUsuarioAprovacao.SetText(s.cp_UsuarioAprovacao);
		        txtValorAditivo.SetText(s.cp_ValorAditivo);
		        txtValorDoContrato.SetText(s.cp_ValorContrato);

                if(s.cp_NovaDataVigencia != "")
		            ddlDataPrazo.SetFocus();
		        ddlContrato.SetFocus();
		    }
		}

		function atualizaNovoValor() {
		    var novoValor;

		    if (txtValorAditivo.GetText() != "" && txtValorDoContrato.GetText() != "") {
		        try {
		            var valorAditivo = parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.'));
		            var valorContrato = parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.'));

		            novoValor = valorAditivo + valorContrato;

		        } catch (e) { }
		    }

		    txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
		}

		function gravaInstanciaWf() {
		    try {
		        window.parent.executaCallbackWF();
		    } catch (e) { }
        }

        function eventoPosSalvar(codigoInstancia) {
            try {
                window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
            } catch (e) {
            }
            hfWF.Set("CodigoCI", codigoInstancia);
            callbackInstancia.PerformCallback(codigoInstancia);
        }
</script>
    <title></title>
    <style type="text/css">

        .style5
        {
            width: 5px;
        }
        .style4
        {
            width: 145px;
        }
        .style7
        {
            width: 120px;
        }
        .style10
        {
            width: 110px;
        }
        .style1
        {
            height: 10px;
        }
        .style11
        {
            width: 105px;
        }
        .style13
        {
            width: 110px;
        }
        </style>
</head>
<body style='margin:0px' onload='getDadosWF();'>
    <form id="form1" runat="server">
    <div>
    
        <table border="0" 
            cellpadding="0" cellspacing="0" width="<%=tamanhoTable %>">
        <tbody><tr><td style="padding-top: 10px" class="style5">&nbsp;</td>
            <td ><table border="0" 
                    cellpadding="0" cellspacing="0" 
                    width="100%"><tbody>
                        <tr><td class="style4">
                            <dxe:ASPxLabel runat="server" Text="Aditivar Contrato:" 
                            ClientInstanceName="lblNumeroContrato"  
                            ID="lblNumeroContrato"></dxe:ASPxLabel>





                            </td><td>
                            <dxe:ASPxLabel runat="server" Text="Tipo Instrumento:"  
                            ID="lblNumeroContrato0"></dxe:ASPxLabel>





                            </td><td class="style7">
                                <dxe:ASPxLabel runat="server" Text="Nº do Instrumento:" 
                                    ID="ASPxLabel14"></dxe:ASPxLabel>





                            </td><td class="style11">
                                    <dxe:ASPxLabel runat="server" Text="Aditar:"  
                                        ID="ASPxLabel7"></dxe:ASPxLabel>





                            </td></tr>
                        <tr><td class="style4" style="padding-right: 5px;">
                            <dxe:ASPxComboBox runat="server" Width="100%" 
                                            ClientInstanceName="ddlContrato" 
                                            ID="ddlContrato">

                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	callbackValorContrato.PerformCallback();
}" />

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





 </td><td style="padding-right: 5px;">
                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" 
                                            ClientInstanceName="ddlTipoInstrumento" 
                                            ID="ddlTipoInstrumento">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





 </td><td style="padding-right: 5px;" class="style7">
                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="25" 
                            ClientInstanceName="txtNumeroInstrumento"  
                                ID="txtNumeroInstrumento">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>





 </td><td style="padding-right: 5px;" class="style11">
                                    <dxe:ASPxComboBox runat="server" Width="100%" 
                                            ClientInstanceName="ddlAditivar" 
                                        ID="ddlAditivar">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato();
}"></ClientSideEvents>

                                        <Items>
                                            <dxe:ListEditItem Text="Prazo" Value="PR" />
                                            <dxe:ListEditItem Text="Valor" Value="VL" />
                                            <dxe:ListEditItem Text="Prazo e Valor" Value="PV" />
                                            <dxe:ListEditItem Text="Troca de Material" Value="TM" />
                                        </Items>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





 </td></tr></tbody></table></td><td style="padding-top: 10px" class="style5">&nbsp;</td></tr><tr><td class="style5">
                &nbsp;</td><td>
                <table border="0" 
                    cellpadding="0" cellspacing="0" width="100%">
                <tbody><tr><td >
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Valor Contrato/TEC (R$):" 
                                                         ID="ASPxLabel23"></dxe:ASPxLabel>


                                                </td>
                                                <td class="]">
                                                    <dxe:ASPxLabel runat="server" Text="Valor Aditivo (R$):" 
                                                         ID="ASPxLabel24"></dxe:ASPxLabel>


                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Novo Valor (R$):" 
                                                         ID="ASPxLabel21"></dxe:ASPxLabel>


                                                </td>
                                                <td class="style13">
                                                    <dxe:ASPxLabel runat="server" Text="Prazo Final/TEC:" 
                                                        ID="ASPxLabel22"></dxe:ASPxLabel>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="padding-right: 5px;">
                                                    <dxcp:ASPxCallbackPanel ID="callbackValorContrato" runat="server" 
                                                        ClientInstanceName="callbackValorContrato" 
                                                        >
                                                        <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dxe:ASPxTextBox ID="txtValorDoContrato" runat="server" ClientEnabled="False" 
        ClientInstanceName="txtValorDoContrato" DisplayFormatString="{0:n2}" 
         HorizontalAlign="Right" Width="100%">
        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
        <ValidationSettings ErrorDisplayMode="None">
        </ValidationSettings>
        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
        </DisabledStyle>
    </dxe:ASPxTextBox>
                                                            </dxp:PanelContent>
</PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>


                                                </td>
                                                <td align="left" style="padding-right: 5px;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" 
                                                        DisplayFormatString="{0:n2}" ClientInstanceName="txtValorAditivo" 
                                                         ID="txtValorAditivo">
<ClientSideEvents ValueChanged="function(s, e) {
	atualizaNovoValor();
}"></ClientSideEvents>

<MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None" Display="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                                </td>
                                                <td align="left" style="padding-right: 5px;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" 
                                                        DisplayFormatString="{0:n2}" ClientInstanceName="txtNovoValor" 
                                                        ClientEnabled="False"  ID="txtNovoValor">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                                </td>
                                                <td class="style13">
                                                    <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" 
                                                        EditFormatString="dd/MM/yyyy" Width="100%" DisplayFormatString="dd/MM/yyyy" 
                                                        ClientInstanceName="ddlDataPrazo"  
                                                        ID="ddlDataPrazo">
<CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>


                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>





 </td></tr><tr><td >
                        <dxe:ASPxLabel runat="server" Text="Motivo: &amp;nbsp;" 
                            ClientInstanceName="lblObservacoes"  
                            ID="lblObservacoes" EncodeHtml="False"></dxe:ASPxLabel>





 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterOb" 
                            ForeColor="Silver" ID="lblCantCaraterOb"></dxe:ASPxLabel>





 <dxe:ASPxLabel runat="server" Text="&amp;nbsp;de 2000" ClientInstanceName="lblDe250Ob" 
                            ForeColor="Silver" ID="lblDe250Ob" EncodeHtml="False"></dxe:ASPxLabel>





 </td></tr><tr><td><dxe:ASPxMemo runat="server" Rows="10" Width="100%" 
                            ClientInstanceName="mmMotivo"  
                            ID="mmMotivo">
<ClientSideEvents ValueChanged="function(s, e) {
	
}" Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>





 </td></tr><tr><td class="style1">





 </td></tr><tr><td>
                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                            <tr>
                                <td class="style7">
                                    <dxe:ASPxLabel runat="server" Text="Data Inclusão:" 
                                             ID="ASPxLabel15"></dxe:ASPxLabel>





                                </td>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Incluído Por:" 
                                             ID="ASPxLabel16"></dxe:ASPxLabel>





                                </td>
                            </tr>
                            <tr>
                                <td class="style7" style="padding-right: 10px;">
                                    <dxe:ASPxTextBox ID="txtDataInclusao" runat="server" ClientEnabled="False" 
                                        ClientInstanceName="txtDataInclusao"  
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





 </td></tr><tr><td class="style1">





 </td></tr><tr><td>
                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                            <tr>
                                <td class="style7">
                                    <dxe:ASPxLabel runat="server" Text="Data Aprovação:" 
                                             ID="ASPxLabel17"></dxe:ASPxLabel>





                                </td>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Aprovado Por:" 
                                             ID="ASPxLabel18"></dxe:ASPxLabel>





                                </td>
                            </tr>
                            <tr>
                                <td class="style7" style="padding-right: 10px;">
                                    <dxe:ASPxTextBox ID="txtDataAprovacao" runat="server" ClientEnabled="False" 
                                        ClientInstanceName="txtDataAprovacao"  
                                        Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtUsuarioAprovacao" runat="server" ClientEnabled="False" 
                                        ClientInstanceName="txtUsuarioAprovacao"  
                                        Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>





 </td></tr><tr><td class="style1">





 </td></tr><tr><td>



 <table border="0" cellpadding="0" cellspacing="0" width="100%"><tbody><tr>
         <td align="right">
             &nbsp;</td>
         <td align="right" style="width: 100px; ">
             <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" 
                 Width="95px"  
                 ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var msgErro = validaCamposFormulario();

	if(msgErro != &quot;&quot;)
		window.top.mostraMensagem(msgErro, 'Atencao', true, false, null);
	else
		callbackSalvar.PerformCallback('S');
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td><td align="right" style="width: 95px; padding-left: 5px; <%=mostrarBotaoCancelar %>">
             <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" 
                 CommandArgument="btnCancelar" Text="Cancelar" Width="95px" 
                 ID="btnCancelar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false; 
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td></tr></tbody></table>





 </td></tr></tbody></table></td><td class="style5">
                    &nbsp;</td></tr></tbody></table>

    </div>

 <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackSalvar" 
        ID="callbackSalvar" OnCallback="callbackSalvar_Callback">
     <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MsgStatus != &quot;&quot;)
	{
		mostraDivSalvoPublicado(s.cp_MsgStatus);
	}	
	montaCampos(s,e);

	if (s.cp_Status == &quot;2&quot;) 
		gravaInstanciaWf();
}" Init="function(s, e) {
	montaCampos(s,e);
}" />
    </dxcb:ASPxCallback>

 <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackInstancia" 
        ID="callbackInstancia" OnCallback="callbackInstancia_Callback">
     <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_URL != &quot;&quot;)
		window.location.href = s.cp_URL;
}" />
    </dxcb:ASPxCallback>

        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="Incluir a Entidade Atual" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    <dxhf:ASPxHiddenField ID="hfWF" runat="server" ClientInstanceName="hfWF">
    </dxhf:ASPxHiddenField>

    </form>
</body>
</html>
