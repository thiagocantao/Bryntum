<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroAditivo.aspx.cs" Inherits="Administracao_CadastroContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
<link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
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
        var valorAditivo = txtValorAditivo.GetText() != '' ? parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.')) : null;
        var valorContrato = txtValorDoContrato.GetText() != '' ? parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.')) : null;
        var novoValor = txtNovoValor.GetText() != '' ? parseFloat(txtNovoValor.GetValue().toString().replace(',', '.')) : null;
        var text = txtNumeroInstrumento.GetText();

        var contratacao = ddlTipoInstrumento.GetText();

        if (ddlTipoInstrumento.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O tipo do instrumento deve ser informado.";
        }
        if (ddlAditivar.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O campo 'Aditivo de:' deve ser informado.";
        }
        if ((ddlAditivar.GetValue() == 'PR' || ddlAditivar.GetValue() == 'PV') && ddlDataPrazo.GetValue() == null && ddlAditivar.GetValue() != 'TC') {
            numAux++;
            mensagem += "\n" + numAux + ") O novo prazo final deve ser informado.";
        }
        if ((ddlAditivar.GetValue() == 'VL' || ddlAditivar.GetValue() == 'PV') && valorAditivo == 0 && ddlAditivar.GetValue() != 'TC') {
            numAux++;
            mensagem += "\n" + numAux + ") O valor do aditivo deve ser informado.";
        }

        if (valorAditivo + valorContrato < 0) {
            numAux++;
            mensagem += "\n" + numAux + ") Não é possível colocar um valor de aditivo negativo maior que o valor do contrato.";
        }

        if (novoValor + valorContrato < 0) {
            numAux++;
            mensagem += "\n" + numAux + ") Não é possível colocar um novo valor de contrato negativo maior que o valor do contrato.";
        }
        if (text.length > 25) {

            mensagem += "\n O Número do instrumento gerado está acima do tamanho permitido, por favor informe manualmente um número válido com tamanho de 25 caracteres.";
        }

        if (window.parent.ddlTerminoDeVigencia && ddlDataPrazo.GetValue() != null) {
            if (window.parent.ddlTerminoDeVigencia.GetValue() > ddlDataPrazo.GetValue()) {
                numAux++;
                mensagem += "\n" + numAux + ") Não é possível um prazo final/TEC menor que a data de término do contrato.";
            }
        }

        if (mensagem != "") {
            mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
        }

        return mensagemErro_ValidaCamposFormulario;
    }

    function verificaTipoContrato(mudaValor) {
        if (ddlDataPrazo.cp_RO.toString() != "S") {
            var tipoContrato = ddlAditivar.GetValue();

            if (tipoContrato == 'PR') {
                ddlDataPrazo.SetEnabled(true);
                txtValorAditivo.SetEnabled(false);
                if (mudaValor) {
                    txtValorAditivo.SetText("");
                }
                txtNovoValor.SetEnabled(false);
                atualizaNovoValor();
            } else if (tipoContrato == 'VL') {
                ddlDataPrazo.SetEnabled(false);
                txtValorAditivo.SetEnabled(true);
                txtNovoValor.SetEnabled(false);

                if (mudaValor) {
                    ddlDataPrazo.SetValue(null);
                }

                atualizaNovoValor();

            } else if (tipoContrato == 'PV') {
                ddlDataPrazo.SetEnabled(true);
                txtValorAditivo.SetEnabled(true);
                txtNovoValor.SetEnabled(false);
                atualizaNovoValor();

            }else {
                txtValorAditivo.SetEnabled(false);

                if (mudaValor) {
                    txtValorAditivo.SetText("");
                    ddlDataPrazo.SetValue(null);
                    //txtNovoValor.SetText("");
                }
                ddlDataPrazo.SetEnabled(false);
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

    function atualizaNovoValor() {
        var novoValor;

        if (txtValorDoContrato.GetText() != "") {
            try {
                var valorAditivo = parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.'));
                var valorContrato = parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.'));
                if (valorAditivo + valorContrato < 0) {
                    window.top.mostraMensagem('Não é possível colocar um valor de aditivo negativo maior que o valor do contrato', 'Atencao', true, false, null);
                    txtValorAditivo.SetFocus();
                    return;
                }
                else
                    novoValor = valorAditivo + valorContrato;

            } catch (e) { }
        }

        txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
    }

    
    //----------- Mensagem modificação con sucesso..!!!
    function mostraDivSalvoPublicado(acao) {
        if (acao.toUpperCase().indexOf('SUCESSO'))
            window.top.mostraMensagem(acao, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(acao, 'erro', true, false, null);
    }

    function onClick_btnSalvar() {
        if (window.validaCamposFormulario) {
            if (validaCamposFormulario() != "") {
                window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);
                return false;
            }
            else {
                pnCallback.PerformCallback();
            }
        }
    }
    </script>
    <title></title>
    
    <style type="text/css">



        .style5
        {
            width: 5px;
        }
        .style12
        {
            width: 140px;
        }
        .style1
        {
            height: 10px;
        }
        .style7
        {
            width: 120px;
        }
        .style14
        {
            width: 160px;
        }
.dxpcControl
{
	font: 12px Tahoma;
	color: black;
	background-color: white;
	border: 1px solid #8B8B8B;
	width: 200px;
}
.dxpcContent
{
	color: #010000;
	white-space: normal;
	vertical-align: top;
}
.dxpcContentPaddings 
{
	padding: 9px 12px;
}
    </style>
    
</head>
<body style='margin:0px'>
    <form id="form1" runat="server">
    <div>
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="style5" style="padding-top: 10px">
                    &nbsp;</td>
                <td >
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Tipo Instrumento:" 
                                        ID="lblNumeroContrato0"></dxe:ASPxLabel>


                                </td>
                                <td class="linhaEdicaoPeriodo">
                                    <dxe:ASPxLabel runat="server" Text="N&#186; do Instrumento:" 
                                         ID="ASPxLabel14"></dxe:ASPxLabel>


                                </td>
                                <td class="style12">
                                    <dxe:ASPxLabel runat="server" Text="Aditar:" 
                                        ClientInstanceName="lblNumeroContrato"  
                                        ID="ASPxLabel7"></dxe:ASPxLabel>


                                </td>
                                <td class="style14">
                                                    <dxe:ASPxLabel runat="server" Text="Valor Contrato (R$):" 
                                         ID="ASPxLabel23"></dxe:ASPxLabel>


                                </td>
                                <td class="style12">
                                                    <dxe:ASPxLabel runat="server" Text="Valor Aditivo (R$):" 
                                        Width="125px"  ID="ASPxLabel24"></dxe:ASPxLabel>


                                </td>
                                <td class="style12">
                                                    <dxe:ASPxLabel runat="server" Text="Novo Valor (R$):" 
                                        Width="125px"  ID="ASPxLabel21"></dxe:ASPxLabel>


                                </td>
                                <td class="style12">
                                                    <dxe:ASPxLabel runat="server" Text="Prazo Final:" 
                                         ID="ASPxLabel22"></dxe:ASPxLabel>


                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px;">
                                    <dxe:ASPxComboBox runat="server" Width="100%" 
                                        ClientInstanceName="ddlTipoInstrumento"  
                                        ID="ddlTipoInstrumento" ClientEnabled="False">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato(true);
}" ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>


                                </td>
                                <td class="linhaEdicaoPeriodo" style="padding-right: 5px;">
                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="25" 
                                        ClientInstanceName="txtNumeroInstrumento"  
                                        ID="txtNumeroInstrumento" ClientEnabled="False">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                </td>
                                <td class="style12" style="padding-right: 5px;">
                                    <dxe:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlAditivar" 
                                         ID="ddlAditivar">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato(true);
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Prazo" Value="PR"></dxe:ListEditItem>
<dxe:ListEditItem Text="Valor" Value="VL"></dxe:ListEditItem>
<dxe:ListEditItem Text="Prazo e Valor" Value="PV"></dxe:ListEditItem>
</Items>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>


                                </td>
                                <td class="style14" style="padding-right: 5px;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" 
                                        HorizontalAlign="Right" DisplayFormatString="{0:n2}" 
                                        ClientInstanceName="txtValorDoContrato" ClientEnabled="False" 
                                         ID="txtValorDoContrato">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                </td>
                                <td class="style12" style="padding-right: 5px;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" 
                                        HorizontalAlign="Right" DisplayFormatString="{0:n2}" 
                                        ClientInstanceName="txtValorAditivo"  
                                        ID="txtValorAditivo">
<ClientSideEvents LostFocus="function(s, e) {
	atualizaNovoValor();
}" ValueChanged="function(s, e) {
	atualizaNovoValor();
}"></ClientSideEvents>

<MaskSettings Mask="&lt;-9999999999999..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None" Display="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                </td>
                                <td class="style12" style="padding-right: 5px;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" 
                                        HorizontalAlign="Right" DisplayFormatString="{0:n2}" 
                                        ClientInstanceName="txtNovoValor" ClientEnabled="False" 
                                        ID="txtNovoValor">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<MaskSettings Mask="&lt;-9999999999999..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                </td>
                                <td class="style12">
                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" 
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
                </td>
                <td class="style5" style="padding-top: 10px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td >
                                    <dxe:ASPxLabel runat="server" Text="Motivo: &amp;nbsp;" EncodeHtml="False" 
                                        ClientInstanceName="lblObservacoes"  
                                        ID="lblObservacoes"></dxe:ASPxLabel>


                                    <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterOb" 
                                         ForeColor="Silver" ID="lblCantCaraterOb"></dxe:ASPxLabel>


                                    <dxe:ASPxLabel runat="server" Text="&amp;nbsp;de 2000" EncodeHtml="False" 
                                        ClientInstanceName="lblDe250Ob"  
                                        ForeColor="Silver" ID="lblDe250Ob"></dxe:ASPxLabel>


                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo runat="server" Rows="6" Width="100%" 
                                        ClientInstanceName="mmMotivo"  
                                        ID="mmMotivo">
<ClientSideEvents ValueChanged="function(s, e) {
	
}" Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>


                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                        <tr>
                                            <td class="style7">
                                                <dxe:ASPxLabel runat="server" Text="Data Inclus&#227;o:" 
                                                    ID="ASPxLabel15"></dxe:ASPxLabel>


                                            </td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Inclu&#237;do Por:" 
                                                    ID="ASPxLabel16"></dxe:ASPxLabel>


                                            </td>
                                            <td class="style7">
                                                <dxe:ASPxLabel runat="server" Text="Data Aprova&#231;&#227;o:" 
                                                     ID="ASPxLabel17"></dxe:ASPxLabel>


                                            </td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Aprovado Por:" 
                                                    ID="ASPxLabel18"></dxe:ASPxLabel>


                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style7" style="padding-right: 10px;">
                                                <dxe:ASPxTextBox runat="server" Width="100%" 
                                                    DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtDataInclusao" 
                                                    ClientEnabled="False"  ID="txtDataInclusao">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="100%" 
                                                    ClientInstanceName="txtUsuarioInclusao" ClientEnabled="False" 
                                                     ID="txtUsuarioInclusao">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                            </td>
                                            <td class="style7" style="padding-right: 10px;">
                                                <dxe:ASPxTextBox runat="server" Width="100%" 
                                                    DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtDataAprovacao" 
                                                    ClientEnabled="False"  
                                                    ID="txtDataAprovacao">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="100%" 
                                                    ClientInstanceName="txtUsuarioAprovacao" ClientEnabled="False" 
                                                     ID="txtUsuarioAprovacao" 
                                                    style="margin-bottom: 0px">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" 
                                                        ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" 
                                                        ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
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
                <td class="style5">
                    &nbsp;</td>
            </tr>
        </tbody>
    </table>

    </div>

 <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Modal="True" CloseAction="None" 
        ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" 
        ShowCloseButton="False" ShowHeader="False" Width="270px" 
        ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>


























 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    <dxcb:ASPxCallback ID="pnCallback" runat="server" 
        ClientInstanceName="pnCallback" oncallback="pnCallback_Callback">
        <ClientSideEvents Init="function(s, e) {
	verificaTipoContrato(true);
}" EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
    </dxcb:ASPxCallback>

    </form>
</body>
</html>
