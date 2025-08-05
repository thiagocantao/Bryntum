<%@ Page Language="C#" ResponseEncoding="utf-8" AutoEventWireup="true" CodeFile="popupEdicaoTarefaEAP.aspx.cs"
    Inherits="_Projetos_DadosProjeto_popupEdicaoTarefaEAP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pacote de Trabalho - Detalhes</title>
    <base target="_self" />
    <script type="text/javascript" language="javascript">

        function buscaNomeBD(objeto) {
            hfGeral.Set("lovCodigoResponsavel", "");
            nome = objeto.GetText();
            if (nome != "") {
                pnCallback.PerformCallback("PesquisarResp");
            }
            else {
                btnSalvar.SetEnabled(true);
            }
        }

        function preparaWhereGetLovNomeValor() {
            var where = new String();
            where = hfGeral.Contains("hfWheregetLov_NomeValor") ? hfGeral.Get("hfWheregetLov_NomeValor") : "dataExclusao is null";
            if (hfGeral.Contains("hfWheregetLov_NomeValor")) {
                if (where.substring(0, 3) == "and") {
                    where = where.substring(4, where.length);
                }
            }
            return where;
        }

        function mostraLov() {
            var where = preparaWhereGetLovNomeValor();
            var retorno = window.showModalDialog('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), '', 'resizable:0; dialogWidth:520px; dialogHeight:465px; status:no; menubar=no;');
            hfGeral.Set("lovMostrarPopPup", "0");
            if (retorno && retorno != "") {
                var aRetorno = retorno.split(';');
                hfGeral.Set("lovCodigoResponsavel", aRetorno[0]);
                txtResponsavel.SetText(aRetorno[1]);
            }
            else {
                txtResponsavel.SetText("");
                hfGeral.Set("lovCodigoResponsavel", "");
            }
            pnCallback.PerformCallback("habilitaSalvar");
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 10px;
        }
        .style2
        {
            height: 10px;
            width: 10px;
        }
    </style>
</head>
<body style="padding-left: 5px">
    <form id="form1" runat="server">
    <div>
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%" HideContentOnCallback="False">
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td align="center">
                                    &nbsp;&nbsp;<dxe:ASPxLabel runat="server" Text="Pacote 1.1" ClientInstanceName="lblPacote"
                                        Font-Bold="True"  ID="lblPacote" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="center" class="style1">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table style="width: 580px" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="padding-bottom: 0px" valign="bottom">
                                                                    <dxe:ASPxLabel runat="server" Text="In&#237;cio:" 
                                                                        ID="ASPxLabel4">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-bottom: 0px" valign="bottom">
                                                                    <dxe:ASPxLabel runat="server" Text="T&#233;rmino:" 
                                                                        ID="ASPxLabel2">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-bottom: 0px" valign="bottom">
                                                                    <dxe:ASPxLabel runat="server" Text="Dura&#231;&#227;o (dias):"
                                                                        ID="ASPxLabel3">
                                                                        <DisabledStyle >
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-bottom: 0px" valign="bottom">
                                                                    <dxe:ASPxLabel runat="server" Text="Trabalho (horas):" 
                                                                        ID="ASPxLabel5">
                                                                        <DisabledStyle >
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-bottom: 0px" valign="bottom">
                                                                    <dxe:ASPxLabel runat="server" Text="Despesa (R$):" 
                                                                        ID="ASPxLabel6">
                                                                        <DisabledStyle >
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 0px">
                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="dteInicio"
                                                                         ID="dteInicio">
                                                                        <CalendarProperties ClearButtonText="Limpar">
                                                                        </CalendarProperties>
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td style="padding-top: 0px">
                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="dteTermino"
                                                                         ID="dteTermino">
                                                                        <CalendarProperties ClearButtonText="Limpar">
                                                                        </CalendarProperties>
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td style="padding-top: 0px">
                                                                    <dxe:ASPxTextBox runat="server" Width="110px" ClientInstanceName="txtDuracao"
                                                                        ID="txtDuracao">
                                                                        <MaskSettings Mask="&lt;0..9999999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                        <ValidationSettings ErrorDisplayMode="None" ErrorText="">
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-top: 0px">
                                                                    <dxe:ASPxTextBox runat="server" Width="110px" ClientInstanceName="txtTrabalho"
                                                                        ID="txtTrabalho">
                                                                        <MaskSettings Mask="&lt;0..9999999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                        <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-top: 0px">
                                                                    <dxe:ASPxTextBox runat="server" Width="119px" ClientInstanceName="txtCusto"
                                                                        ID="txtCusto">
                                                                        <MaskSettings Mask="&lt;0..9999999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                        <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" 
                                                        ID="ASPxLabel7">
                                                        <DisabledStyle >
                                                        </DisabledStyle>
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButtonEdit runat="server" Width="100%" ClientInstanceName="txtResponsavel"
                                                         ID="txtResponsavel">
                                                        <ClientSideEvents ButtonClick="function(s, e) {
	e.processOnServer = false;	
	mostraLov();
}" TextChanged="function(s, e) {
	e.processOnServer = false;	
	buscaNomeBD(s);
}"></ClientSideEvents>
                                                        <Buttons>
                                                            <dxe:EditButton>
                                                            </dxe:EditButton>
                                                        </Buttons>
                                                        <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                        </Paddings>
                                                        <DisabledStyle  ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 16px">
                                                    <dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es:" 
                                                        ID="ASPxLabel8">
                                                        <DisabledStyle >
                                                        </DisabledStyle>
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="txtDicionario"
                                                        ID="txtDicionario" Rows="5">
                                                        <ClientSideEvents Init="function(s, e) {
		return setMaxLength(s.GetInputElement(), 4000);
}"></ClientSideEvents>
                                                        <DisabledStyle  ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater"
                                                        ForeColor="Silver" ID="lblCantCarater">
                                                    </dxe:ASPxLabel>
                                                    &nbsp;<dxe:ASPxLabel runat="server" Text=" de 4000" ClientInstanceName="lblDe250"
                                                         ForeColor="Silver" ID="lblDe250">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    &nbsp;<dxe:ASPxLabel ID="lblResponsavel" runat="server" ClientInstanceName="lblResponsavel"
                                                                        >
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" style="width:100px">
                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                                                         ID="btnSalvar">
                                                                        <ClientSideEvents Click="function(s, e) 
{
    hfGeral.Set(&quot;lovMostrarPopPup&quot;, &quot;0&quot;);
	e.processOnServer = false;	
	if (window.validaCamposFormulario)
	{
    	if(validaCamposFormulario() != &quot;&quot;)
		{
			window.top.mostraMensagem(validaCamposFormulario(), 'atencao', true, false, null);
		}
		else
		{
			pnCallback.PerformCallback(&quot;salvar&quot;);		     
	    }
	}
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td align="right" style="width:100px">
                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px"
                                                                         ID="btnCancelar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal2();
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
                                </td>
                                <td class="style1">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText=""
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                        ShowHeader="False" Width="270px"  ID="pcMensagemGravacao">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="center" style="">
                                            </td>
                                            <td align="center" rowspan="3" style="width: 70px">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                    ID="lblAcaoGravacao">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </dxpc:ASPxPopupControl>
                </dxp:PanelContent>
            </PanelCollection>
            <ClientSideEvents EndCallback="function(s, e) { 
	if (hfGeral.Contains(&quot;lovMostrarPopPup&quot;) &amp;&amp; hfGeral.Get(&quot;lovMostrarPopPup&quot;) == &quot;1&quot; )
	{
		mostraLov();
	}
	if(&quot;SIM&quot; == s.cp_OperacaoOk)
	    mostraPopupMensagemGravacao(&quot;Dados gravados com sucesso!&quot;);
}"></ClientSideEvents>
            <Styles>
            <LoadingDiv Opacity="0" BackColor="Transparent"></LoadingDiv>
            </Styles>
        </dxcp:ASPxCallbackPanel>
    </div>
    </form>
</body>
</html>
