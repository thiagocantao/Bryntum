<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroContrato.aspx.cs" Inherits="Administracao_CadastroContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        //------------Obtendo data e hora actual
        var dataInicio = new Date(ddlInicioDeVigencia.GetValue());
        var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
        var dataInicioC = Date.parse(dataInicioP);

        var dataTermino = new Date(ddlTerminoDeVigencia.GetValue());
        var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
        var dataTerminoC = Date.parse(dataTerminoP);

        if (txtNumeroContrato.cp_NumeracaoAutomatica == "N" && txtNumeroContrato.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") O número do contrato deve ser informado.";
        }
        if (ddlRazaoSocial.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") A razão social deve ser informada.";
        }
        if (mmObjeto.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") A descrição do objeto deve ser informada.";
        }

        if (txtVigencia.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") A vigência deve ser informada.";
        }

        //        if ((ddlTerminoDeVigencia.GetEnabled() == true) && (ddlTerminoDeVigencia.GetValue() == null)) {
        //            mensagem += "\n" + numAux + ") A data de término deve ser informada.\n";
        //            retorno = false;
        //        }

        if ((ddlAssinatura.GetEnabled() == true) && (ddlAssinatura.GetValue() == null)) {
            mensagem += "\n" + numAux + ") A data de assinatura deve ser informada.\n";
            retorno = false;
        }

        //        if ((ddlInicioDeVigencia.GetValue() != null) && (ddlTerminoDeVigencia.GetValue() != null)) {
        //            if (dataInicioC > dataTerminoC) {
        //                mensagem += "\n" + numAux + ") A data de início não pode ser maior que a data de término!\n";
        //                retorno = false;
        //            }
        //        }
        if (ddlCriterioReajuste.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O critério de reajuste deve ser informado.";
        }
        if (ddlCriterioMedicao.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O critério de medição deve ser informado.";
        }
        if (ddlGestorContrato.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O gestor deve ser informado.";
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
            if (textAreaElement.name.indexOf("mmObservacoes") >= 0)
                lblCantCaraterOb.SetText(text.length);
            else
                lblCantCarater.SetText(text.length);
        }
    }

    function createEventHandler(funcName) {
        return new Function("event", funcName + "(event);");
    }

    function onInit_mmObjeto(s, e) {
        try { return setMaxLength(s.GetInputElement(), 4000); }
        catch (e) { }
    }

    function onInit_mmObservacoes(s, e) {
        try { return setMaxLength(s.GetInputElement(), 500); }
        catch (e) { }
    }

    function novaRazaoSocial() {
        window.top.showModal(ddlRazaoSocial.cp_URLRazao, 'Nova Razão Social', 900, 435, funcaoPosModal, null);
    }

    function funcaoPosModal(valor) {
        if (valor != null && valor != '')
            ddlRazaoSocial.PerformCallback(valor.toString());
    }

    function carregaDadosFornecedor(contatoFornecedor) {
        txtGestorContratada.SetText(contatoFornecedor);
    }

    //----------- Mensagem modificação con sucesso..!!!
    function mostraDivSalvoPublicado(acao) {
        if (acao.toUpperCase().indexOf('SUCESSO'))
            window.top.mostraMensagem(acao, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(acao, 'erro', true, false, null);

        if (callbackSalvar.cp_Status == "1") {
            setTimeout('finalizaEdicao();', 2000);
            if (callbackSalvar.cp_URL != null && callbackSalvar.cp_URL != "") {
                var url = callbackSalvar.cp_URL;
                window.open(url, '_self');
            }
        }
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
</script>
    <title></title>
    <style type="text/css">

        .style5
        {
            width: 5px;
        }
        .style4
        {
            width: 200px;
        }
        .style2
        {
            
        }
        .style3
        {
            
        }
        .style7
        {
            width: 115px;
        }
        .style1
        {
            height: 10px;
        }
        .style11
        {
            width: 95px;
        }
        .style12
        {
            width: 100px;
        }
        .style13
        {
            width: 120px;
        }
        .style15
        {
            width: 220px;
        }
        .style17
        {
            width: 155px;
        }
        .style18
        {
            width: 111px;
        }
        .style20
        {
            width: 100%;
        }
        .style21
        {
            width: 137px;
        }
    </style>
</head>
<body style='margin:0px'>
    <form id="form1" runat="server">
    <div>
    
        <dxtc:ASPxPageControl ID="pcDados" runat="server" ActiveTabIndex="0" 
             Width="100%">
            <TabPages>
                <dxtc:TabPage Name="tbContrato" Text="Contrato">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="style5" style="padding-top: 10px">
                                            &nbsp;</td>
                                        <td >
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="style4">
                                                            <dxe:ASPxLabel ID="lblNumeroContrato" runat="server" 
                                                                ClientInstanceName="lblNumeroContrato"  
                                                                Text="Número do Documento:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 82px">
                                                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                                                                            Text="Razão Social:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <span class="style2" runat="server" id="link"><span class="style3">(</span><a href="#" 
                                                                            onclick="novaRazaoSocial()" tabindex="4"><span class="style3">Incluir Novo</span></a>)</span> </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4" style="padding-right: 10px;">
                                                            <dxe:ASPxTextBox ID="txtNumeroContrato" runat="server" 
                                                                ClientInstanceName="txtNumeroContrato"  
                                                                MaxLength="50" Width="100%">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="ddlRazaoSocial" runat="server" 
                                                                ClientInstanceName="ddlRazaoSocial"  
                                                                IncrementalFilteringMode="Contains" OnCallback="ddlRazaoSocial_Callback" 
                                                                ValueType="System.String" Width="100%">
                                                                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_NovoValor != null &amp;&amp; s.cp_NovoValor != &quot;&quot; &amp;&amp; s.cp_NovoValor != &quot;-1&quot;)
		s.SetValue(s.cp_NovoValor);
}" SelectedIndexChanged="function(s, e) {
	callbackFornecedor.PerformCallback();
}" />
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5" >
                                            &nbsp;</td>
                                        <td >
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                ClientInstanceName="lblNumeroContrato" EncodeHtml="False" 
                                                Text="Objeto: &amp;nbsp;">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel ID="lblCantCarater" runat="server" 
                                                ClientInstanceName="lblCantCarater"  
                                                ForeColor="Silver" Text="0">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250" 
                                                EncodeHtml="False"  ForeColor="Silver" 
                                                Text=" &amp;nbsp;de 4000">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td>
                                            <dxe:ASPxMemo ID="mmObjeto" runat="server" ClientInstanceName="mmObjeto" 
                                                 Rows="4" Width="100%">
                                                <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" ValueChanged="function(s, e) {
	
}" />
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="style18" style="display: none;">
                                                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                                                ClientInstanceName="lblNumeroContrato"  
                                                                                Text="Data OS Externa:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style12" style="display: none">
                                                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                                                ClientInstanceName="lblNumeroContrato"  
                                                                                Text="Data Término:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style21">
                                                                            <dxe:ASPxLabel ID="ASPxLabel20" runat="server" 
                                                                                ClientInstanceName="lblNumeroContrato"  
                                                                                Text="Vigência:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style12">
                                                                            <dxe:ASPxLabel ID="ASPxLabel19" runat="server" 
                                                                                ClientInstanceName="lblNumeroContrato"  
                                                                                Text="Data Assinatura:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style7">
                                                                            <dxe:ASPxLabel ID="lblValorDoContrato" runat="server" 
                                                                                ClientInstanceName="lblValorDoContrato"  
                                                                                Text="Valor do Contrato:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style11">
                                                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                                ClientInstanceName="lblValorDoContrato"  
                                                                                Text="Data-Base:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                    <td ID="tdLabelNumeroInterno2" class="style17" style="padding-right: 5px; padding-left: 5px; ">
                        <dxe:ASPxLabel ID="ASPxLabelNumeroInterno2" runat="server" 
                        Text="DD:">
                    </dxe:ASPxLabel>
                    </td>
                    <td ID="tdLabelNumeroInterno3" class="style17" style="">
                        <dxe:ASPxLabel ID="ASPxLabelNumeroInterno3" runat="server" 
                        Text="RD:">
                    </dxe:ASPxLabel>
                    </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                                                ClientInstanceName="lblNumeroContrato"  
                                                                                Text="Critério de Reajuste:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style18" style="padding-right: 5px; display: none;">
                                                                            <dxe:ASPxDateEdit ID="ddlInicioDeVigencia" runat="server" 
                                                                                ClientInstanceName="ddlInicioDeVigencia" DisplayFormatString="dd/MM/yyyy" 
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" 
                                                                                 UseMaskBehavior="True" Width="100%" 
                                                                                PopupVerticalAlign="WindowCenter">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}" ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td class="style12" style="padding-right: 5px; display: none;">
                                                                            <dxe:ASPxDateEdit ID="ddlTerminoDeVigencia" runat="server" 
                                                                                ClientInstanceName="ddlTerminoDeVigencia" DisplayFormatString="dd/MM/yyyy" 
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" 
                                                                                 UseMaskBehavior="True" Width="100%" 
                                                                                PopupVerticalAlign="WindowCenter">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td style="padding-right: 3px;" class="style21">
                                                                            <table cellpadding="0" cellspacing="0" class="style20">
                                                                                <tr>
                                                                                    <td style="width: 60px; padding-right: 3px;">
                                                                                        <dxe:ASPxSpinEdit ID="txtVigencia" runat="server" 
                                                                                            ClientInstanceName="txtVigencia" MaxValue="9999" MinValue="1" NullText="-" 
                                                                                            Number="0" NumberType="Integer" Width="100%">
                                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                                            </SpinButtons>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxComboBox ID="ddlUnidadeVigencia" runat="server" 
                                                                                            ClientInstanceName="ddlUnidadeVigencia"  
                                                                                            SelectedIndex="1" Width="100%">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="Dias" Value="dd" />
                                                                                                <dxe:ListEditItem Selected="True" Text="Meses" Value="mm" />
                                                                                            </Items>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="style12" style="padding-right: 5px;">
                                                                            <dxe:ASPxDateEdit ID="ddlAssinatura" runat="server" 
                                                                                ClientInstanceName="ddlAssinatura" DisplayFormatString="dd/MM/yyyy" 
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" 
                                                                                 UseMaskBehavior="True" Width="100%" 
                                                                                PopupVerticalAlign="WindowCenter">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td class="style7" style="padding-right: 5px;">
                                                                            <dxe:ASPxTextBox ID="txtValorDoContrato" runat="server" 
                                                                                ClientInstanceName="txtValorDoContrato" DisplayFormatString="{0:n2}" 
                                                                                 HorizontalAlign="Right" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td class="style11" style="padding-right: 5px;">
                                                                            <dxe:ASPxDateEdit ID="ddlDataBase" runat="server" 
                                                                                ClientInstanceName="ddlDataBase" DisplayFormatString="dd/MM/yyyy" 
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" 
                                                                                 UseMaskBehavior="True" Width="100%" 
                                                                                PopupVerticalAlign="WindowCenter">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
 <td style="padding-right: 5px; padding-left: 5px; "class="style17" ID="tdNumeroInterno2">
                <dxe:ASPxTextBox ID="txtnumeroInterno2" 
                    runat="server" ClientInstanceName="txtnumeroInterno2"
                                                            
                    MaxLength="25" Width="100%">
<ClientSideEvents ValueChanged="function(s, e) {
	
}" />

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td>

  <td class="style17"  style="padding-right: 5px; " ID="tdNumeroInterno3">
                <dxe:ASPxTextBox ID="txtnumeroInterno3" 
                    runat="server" ClientInstanceName="txtnumeroInterno3"
                                                            
                    MaxLength="25" Width="100%">
<ClientSideEvents ValueChanged="function(s, e) {
	
}" />

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="ddlCriterioReajuste" runat="server" 
                                                                                ClientInstanceName="ddlCriterioReajuste"  
                                                                                ValueType="System.String" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="style15">
                                                                            &nbsp;<dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                                                                Text="Critério de Medição:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                                                                Text="Gestor:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style4">
                                                                            <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                                                                Text="Gestor da Contratada:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td width="170">
                                                                            <dxe:ASPxLabel ID="ASPxLabel18" runat="server" 
                                                                                Text="Nº de Trabalhadores Diretos:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="style13">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
<td style="padding-right: 10px; " class="style15">
                                                                            <dxe:ASPxComboBox ID="ddlCriterioMedicao" runat="server" 
                                                                                ClientInstanceName="ddlCriterioMedicao"  
                                                                                ValueType="System.String" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>

                                                                        <td style="padding-right: 10px; ">
                                                                            <dxe:ASPxComboBox ID="ddlGestorContrato" runat="server" 
                                                                                ClientInstanceName="ddlGestorContrato"  
                                                                                IncrementalFilteringMode="Contains" TextField="ColunaNome" 
                                                                                ValueField="ColunaValor" ValueType="System.String" Width="100%">
                                                                                <ClientSideEvents Init="function(s, e) {
	ddlGestorContrato.SetText(s.cp_ddlGestorContrato);
}" />
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn Caption="Nome" Width="300px" />
                                                                                    <dxe:ListBoxColumn Caption="Email" Width="200px" />
                                                                                </Columns>
                                                                                <ValidationSettings 
                                                                                    ErrorDisplayMode="None" ErrorText="*">
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td style="padding-right: 10px; " class="style4">
                                                                            <dxe:ASPxTextBox ID="txtGestorContratada" runat="server" BackColor="#EBEBEB" 
                                                                                ClientInstanceName="txtGestorContratada"  
                                                                                ReadOnly="True" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td  width="170">
                                                                            <dxe:ASPxTextBox ID="txtNumeroTrabalhadores" runat="server" 
                                                                                ClientInstanceName="txtNumeroTrabalhadores" DisplayFormatString="{0:n0}" 
                                                                                 HorizontalAlign="Right" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <MaskSettings Mask="&lt;0..999999999&gt;" />
                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td align="center" class="style13">
                                                                            <dxe:ASPxHyperLink ID="linkEditarCronograma" runat="server" 
                                                                                ClientInstanceName="linkEditarCronograma" ClientVisible="False" 
                                                                                 Text="Editar Cronograma">
                                                                            </dxe:ASPxHyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            <dxe:ASPxLabel ID="lblObservacoes" runat="server" 
                                                                ClientInstanceName="lblObservacoes" EncodeHtml="False" 
                                                                Text="Observações: &amp;nbsp;">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="lblCantCaraterOb" runat="server" 
                                                                ClientInstanceName="lblCantCaraterOb"  
                                                                ForeColor="Silver" Text="0">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="lblDe250Ob" runat="server" ClientInstanceName="lblDe250Ob" 
                                                                EncodeHtml="False"  ForeColor="Silver" 
                                                                Text="  &amp;nbsp;de 500">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo ID="mmObservacoes" runat="server" 
                                                                ClientInstanceName="mmObservacoes"  
                                                                Rows="4" Width="100%" >
                                                                <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}" ValueChanged="function(s, e) {
	
}" />
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            &nbsp;</td>
                                                                        <td align="right" style="width: 100px; ">
                                                                            <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" 
                                                                                 Text="Salvar" Width="95px" 
                                                                                Height="22px">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var msgErro = validaCamposFormulario();

	if(msgErro != &quot;&quot;)
		window.top.mostraMensagem(msgErro, 'Atencao', true, false, null);
	else
		callbackSalvar.PerformCallback();
}" />
                                                                                <Paddings Padding="0px" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td align="right" style="width: 95px; padding-left: 5px; ">
                                                                            <dxe:ASPxButton ID="btnCancelar" runat="server" 
                                                                                ClientInstanceName="btnCancelar" CommandArgument="btnCancelar" 
                                                                                 Text="Cancelar" Width="95px">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false; 
}" />
                                                                                <Paddings Padding="0px" />
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
                                    </tr>
                                </tbody>
                            </table>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="tbAnexos" Text="Anexos">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="tbComentarios" Text="Comentários">
                    <ContentCollection>
                        <dxw:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            <ClientSideEvents ActiveTabChanged="function(s, e) {
	if(e.tab.index == 2)
		document.getElementById('frComentarios').src = document.getElementById('frComentarios').src;
}" />
            <ContentStyle>
                <Paddings PaddingBottom="4px" PaddingTop="4px" />
            </ContentStyle>
        </dxtc:ASPxPageControl>

    </div>

 <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackSalvar" 
        ID="callbackSalvar" OnCallback="callbackSalvar_Callback">
     <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MsgStatus != null &amp;&amp; s.cp_MsgStatus != &quot;&quot;)
		mostraDivSalvoPublicado(s.cp_MsgStatus);	
}" />
    </dxcb:ASPxCallback>

 <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackFornecedor" 
        ID="callbackFornecedor" OnCallback="callbackFornecedor_Callback">
<ClientSideEvents EndCallback="function(s, e) {
	carregaDadosFornecedor(s.cp_contatoFornecedor);
}"></ClientSideEvents>
</dxcb:ASPxCallback>

    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="Incluir a Entidade Atual" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    </form>
</body>
</html>
