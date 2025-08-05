<%@ Page Language="C#" AutoEventWireup="true" CodeFile="novaMensagemMetas.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_novaMensagemObjetivoEstrategico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Mensagens</title>
        <script type="text/javascript" language="javascript">
        function validaMensagem()
        {

            var retorno = "";
            if(txtMensagem.GetText() == "")
            {
                retorno = "Mensagem deve ser informada";
            }
            if(dtePrazo.GetText() == "")
            {
                retorno = "Prazo deve ser informado";
            }
            else
            {
                if(dtePrazo.GetValue() != null)
                {
                    var dataAtual 	 = new Date();
	                var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	                var dataHoje 	 = Date.parse(meuDataAtual);

	                var dataPrazo = new Date(dtePrazo.GetValue());
                    
                    if(dataPrazo < dataHoje)
                    {
                         retorno = "Prazo não deve ser anterior a data atual";
                    }
                }  
            }            
            
            return retorno;
        }
            
        //para funcionar o label mostrador de quantos caracteres faltam para terminar o texto
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
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }
            
        </script>
    <base target="_self" />
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="height: 30px; background-color: whitesmoke">
                    &nbsp;
                    <dxe:ASPxLabel ID="lblDescricaoObjetivoEstrategico" runat="server" ClientInstanceName="lblNomeProjeto"
                        Font-Bold="True" >
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
        <table>
            <tr>
                <td style="width: 10px; height: 10px">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblMsg" runat="server" ClientInstanceName="lblMsg"
                        Text="Mensagem:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientInstanceName="txtMensagem"
                        Width="665px"  rows="12">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    lblCantCarater.SetText(s.GetText().length);
}" KeyPress="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    lblCantCarater.SetText(s.GetText().length);
}" KeyUp="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    lblCantCarater.SetText(s.GetText().length);
}"></ClientSideEvents>
</dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                         ForeColor="Silver" Text="0">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                        ForeColor="Silver" Text=" de 2000">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                                <dxe:ASPxLabel ID="lblPrazoResposta" runat="server" 
                                    Text="Prazo Para Resposta:">
                                </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td align="right">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left">
                                <dxe:ASPxDateEdit ID="dtePrazo" runat="server" ClientInstanceName="dtePrazo" EditFormatString="dd/MM/yyyy"  Width="193px" EditFormat="Custom" EnableClientSideAPI="True" popupverticalalign="NotSet">
                                    <CalendarProperties>
                                        <Style  />
                                        <DayHeaderStyle  />
                                        <WeekNumberStyle >
                                        </WeekNumberStyle>
                                        <DayStyle  />
                                        <DaySelectedStyle >
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <TodayStyle >
                                        </TodayStyle>
                                        <ButtonStyle >
                                            <HoverStyle >
                                            </HoverStyle>
                                        </ButtonStyle>
                                        <HeaderStyle  />
                                        <FooterStyle  />
                                        <FastNavStyle >
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle >
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle >
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle >
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle >
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle >
                                        </FastNavFooterStyle>
                                        <ReadOnlyStyle >
                                        </ReadOnlyStyle>
                                        <FocusedStyle >
                                        </FocusedStyle>
                                        <InvalidStyle >
                                        </InvalidStyle>
                                        <Style >
                                        </Style>
                                    </CalendarProperties>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="right" style="width: 100px">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Height="5px"
                                    Text="Salvar" Width="90px" OnClick="btnSalvar_Click" >
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
    var valida = validaMensagem(); 	
	if(valida == &quot;&quot;)
	{
        e.processOnServer = true;
	}
	else
	{
		window.top.mostraMensagem(valida, 'atencao', true, false, null);
		e.processOnServer = false;
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right">
                            </td>
                            <td style="width: 100px">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                    Height="1px" Text="Cancelar" Width="90px" >
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	window.top.fechaModal();
	try
	{
	     this.parent.src = 'editaMensagens.aspx';
	     this.parent.framePrincipal.carregaGrid(); 
	}
	catch(e)
	{
	    
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    var oMyObject = window.top.myObject;
       
    var nomeProjeto= oMyObject.nomeProjeto;
    lblNomeProjeto.SetText(nomeProjeto);
</script>

