<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NovaAnalise.aspx.cs" Inherits="_VisaoMaster_NovaAnalise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function salvaAnalise() {
            callbackSalvar.PerformCallback();
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
                if (textAreaElement.name.indexOf("mmAnalise") >= 0)
                    lblContador1.SetText(text.length);
                else
                    lblContador2.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_memo(s, e) {
            try { return setMaxLength(s.GetInputElement(), 2000); }
            catch (e) { }
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
            if(callbackSalvar.cp_Status == '1')
                window.top.fechaModal();
        }

    </script>
    <style type="text/css">

.dxeBase
{
	font: 12px Tahoma;
}
.dxeTrackBar, 
.dxeIRadioButton, 
.dxeButtonEdit, 
.dxeTextBox, 
.dxeRadioButtonList, 
.dxeCheckBoxList, 
.dxeMemo, 
.dxeListBox, 
.dxeCalendar, 
.dxeColorTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}
.dxeTextBox,
.dxeMemo
{
	background-color: white;
	border: 1px solid #9f9f9f;
}
.dxeTextBoxSys, .dxeMemoSys
{
    border-collapse:separate!important;
}

.dxeMemo td
{
	padding: 0 0 0 1px;
	width: 100%;
}

.dxeMemoEditArea
{
	background-color: white;
	font: 12px Tahoma;
	outline: none;
}

.dxeMemoEditAreaSys
{
	border-width: 0px;
	padding: 0px;
	display:block;
	resize: none;
}

.dxbButton
{
	color: #000000;
	font: normal 12px Tahoma;
	vertical-align: middle;
	border: 1px solid #7F7F7F;
	padding: 1px;
	cursor: pointer;
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
        .style1
        {
            height: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" 
        HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" 
        Width="270px"  ID="pcUsuarioIncluido">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="0" cellpadding="0" 
        width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>




















































 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>




















































 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

        <table cellspacing="0" 
        cellpadding="0" width="100%" border="0"><TBODY><tr><td align=left><dxe:ASPxLabel runat="server" Text="An&#225;lises:"  ID="ASPxLabel1"></dxe:ASPxLabel>



                                    <dxe:ASPxLabel runat="server" Text="0" 
                    ClientInstanceName="lblContador1"  
                    ForeColor="Silver" ID="lblContador1"></dxe:ASPxLabel>


                                    <dxe:ASPxLabel runat="server" Text=" de 2000" 
                    ClientInstanceName="lblDe250Ob"  
                    ForeColor="Silver" ID="lblDe250Ob"></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmAnalise" 
                         ID="mmAnalise" Rows="12">
<ClientSideEvents KeyUp="function(s, e) {
	onInit_memo(s, e);
}" Validation="function(s, e) {
	if(e.value == null)
	{
		e.isValid = false;
		e.errorText = &#39;Campo Obrigat&#243;rio!&#39;;
	}
	else
		e.isValid = true;
}" Init="function(s, e) {
	lblContador1.SetText(s.GetText().length);
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="MKE">
<RequiredField ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
</ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>



 </td></tr><tr><td class="style1"></td></tr><tr><td align=left><dxe:ASPxLabel runat="server" Text="Recomenda&#231;&#245;es:"  ID="ASPxLabel3"></dxe:ASPxLabel>



                                    <dxe:ASPxLabel runat="server" Text="0" 
                    ClientInstanceName="lblContador2"  
                    ForeColor="Silver" ID="lblContador2"></dxe:ASPxLabel>


                                    <dxe:ASPxLabel runat="server" Text=" de 2000" 
                    ClientInstanceName="lblDe250Ob"  
                    ForeColor="Silver" ID="lblDe2"></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmRecomendacoes" 
                         ID="mmRecomendacoes" Rows="12">
<ClientSideEvents KeyUp="function(s, e) {
	onInit_memo(s, e);
}" Init="function(s, e) {
	lblContador2.SetText(s.GetText().length);
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align=right><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td align=left>
                <table cellspacing="0" cellpadding="0" border="0" width="500"><TBODY><tr><td style="HEIGHT: 16px">
                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblDataInclusao" 
                         ForeColor="DimGray" 
                        ID="lblDataInclusao" Text=" "></dxe:ASPxLabel>



 </td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td>
                    <dxe:ASPxLabel runat="server" 
                        ClientInstanceName="lblDataAlteracao"  
                        ForeColor="DimGray" ID="lblDataAlteracao" Text=" "></dxe:ASPxLabel>



 </td></tr></tbody></table></td><td style="WIDTH: 100px" valign="top"><dxe:ASPxButton runat="server" 
                        ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="100%" 
                         ID="btnSalvar" AutoPostBack="False">
<ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('MKE', true))
		salvaAnalise();
}"></ClientSideEvents>
</dxe:ASPxButton>



 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 100px" valign="top"><dxe:ASPxButton runat="server" CommandArgument="btnCancelar" Text="Fechar" Width="100%"  ID="btnCancelar">
<ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}"></ClientSideEvents>
</dxe:ASPxButton>



 </td></tr></tbody></table></td></tr></tbody></table>
    
    </div>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
		if (s.cp_Status == '1') {
            mostraDivSalvoPublicado(traducao.NovaAnalise_an_lise_salva_com_sucesso_);            
        }
        else
            mostraDivSalvoPublicado(traducao.NovaAnalise_erro_ao_salvar_an_lise_);		
}" />
    </dxcb:ASPxCallback>
        <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
