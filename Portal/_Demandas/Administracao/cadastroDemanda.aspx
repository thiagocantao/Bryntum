<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroDemanda.aspx.cs" Inherits="_Demandas_Administracao_cadastroDemanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength    = textAreaElement.maxlength;
            var text         = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            
            if (maxLength != 0 && text.length > maxLength) 
                textAreaElement.value = text.substr(0, maxLength);
            else
                lblCantCarater.SetText(text.length);
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }
        
        function mostraDivSalvoPublicado(acao)
        {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao()
        {
            window.top.fechaModal();
        }
        
        function validaCamposFormulario()
        {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var countMsg = 0;
                
            if(ddlDemandante.GetValue() == null)
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") O demandante deve ser informado. \n";
                
            if(ddlAssunto.GetValue() == null)
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") O assunto deve ser informado. \n";        
                
            if(txtTitulo.GetText() == "")
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") O título deve ser informado. \n";   
                
            if(ddlTipo.GetValue() == null)
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") O tipo de demanda deve ser informado. \n";   
                
            if(ddlUrgencia.GetValue() == null)
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") A urgência deve ser informada. \n";   
                
           if(ddlCanal.GetValue() == null)
                mensagemErro_ValidaCamposFormulario += countMsg++ + ") O canal de atendimento deve ser informado. \n";  
                         
            return mensagemErro_ValidaCamposFormulario;
        }
        
        function salvarDemanda(codigoAcaoWf)
        {
            hfGeral.Set("CodigoAcaoWf", codigoAcaoWf);
            btnSalvar.DoClick();
        }
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 99%">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Assunto:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxComboBox ID="ddlAssunto" runat="server" ClientInstanceName="ddlAssunto"
                         Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                        Text="Título:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox ID="txtTitulo" runat="server" ClientInstanceName="txtTitulo" 
                        Width="100%" MaxLength="250">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                        Text="Demandante:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxComboBox ID="ddlDemandante" runat="server" ClientInstanceName="ddlDemandante"
                         IncrementalFilteringMode="Contains" 
                        TextFormatString="{0}" ValueType="System.String" Width="100%">
                        <Columns>
                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                            <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                        </Columns>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Tipo de Demanda:">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td style="width: 140px">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Urgência:">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td style="width: 221px">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Canal de Atendimento:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo" 
                                    Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                            </td>
                            <td style="width: 140px">
                                <dxe:ASPxComboBox ID="ddlUrgencia" runat="server" ClientInstanceName="ddlUrgencia"
                                     Width="100%" ValueType="System.String">
                                    <Items>
                                        <dxe:ListEditItem Text="Alta" Value="A" />
                                        <dxe:ListEditItem Text="M&#233;dia" Value="M" />
                                        <dxe:ListEditItem Text="Baixa" Value="B" />
                                    </Items>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                            </td>
                            <td style="width: 221px"><dxe:ASPxComboBox ID="ddlCanal" runat="server" ClientInstanceName="ddlCanal"
                                     Width="100%" ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="Alta" Value="A" />
                                    <dxe:ListEditItem Text="M&#233;dia" Value="M" />
                                    <dxe:ListEditItem Text="Baixa" Value="B" />
                                </Items>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                        Text="Detalhes:">
                    </dxe:ASPxLabel>
                    &nbsp;<dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                         ForeColor="Silver" Text="0">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250" 
                        ForeColor="Silver" Text=" de 2000">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtDetalhes" runat="server" ClientInstanceName="txtDetalhes" 
                        Rows="8" Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                        <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}" />
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lblAnexo" runat="server" 
                        Text="Anexo:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxuc:ASPxUploadControl ID="fuAnexo" runat="server" ClientInstanceName="fuAnexo"
                         Width="100%">
                        <ValidationSettings MaxFileSizeErrorText="O tamanho do arquivo n&#227;o deve ultrapassar 5MB.">
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxuc:ASPxUploadControl>
                </td>
            </tr>
            <tr>
                <td style="height: 15px" valign="top"><dxe:ASPxLabel ID="lblMsgAnexo" runat="server"
                        Text="Tamanho máximo 5Mb. Caso deseje enviar mais de um arquivo, compacte-os em um .zip antes de enviá-lo." Font-Italic="True">
                </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" style="width: 200px">
                        <tr>
                            <td align="right">
                                <dxe:ASPxButton ID="btnSalvar" ClientInstanceName="btnSalvar" runat="server" 
                                    Text="Salvar" ValidationGroup="MKE" Width="90px" OnClick="btnSalvar_Click">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	var msgErro = validaCamposFormulario();
	if(msgErro != &quot;&quot;)
	{
		e.processOnServer = false;
		window.top.mostraMensagem(msgErro, 'erro', true, false, null); 
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" 
                                    Text="Fechar" Width="90px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
             HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
            Width="270px">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="center">
                                </td>
                                <td align="center" rowspan="3" style="width: 70px">
                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                        ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                        EncodeHtml="False" >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
