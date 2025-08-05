<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCadastroPessoa.aspx.cs"
    Inherits="frmCadastroPessoa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pessoa</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function valida_cpf(cpf) {
            var numeros, digitos, soma, i, resultado, digitos_iguais;
            digitos_iguais = 1;
            if (cpf.length < 11)
                return false;
            for (i = 0; i < cpf.length - 1; i++)
                if (cpf.charAt(i) != cpf.charAt(i + 1)) {
                    digitos_iguais = 0;
                    break;
                }
            if (!digitos_iguais) {
                numeros = cpf.substring(0, 9);
                digitos = cpf.substring(9);
                soma = 0;
                for (i = 10; i > 1; i--)
                    soma += numeros.charAt(10 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
                    return false;
                numeros = cpf.substring(0, 10);
                soma = 0;
                for (i = 11; i > 1; i--)
                    soma += numeros.charAt(11 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1))
                    return false;
                return true;
            }
            else
                return false;
        }

        function valida_cnpj(cnpj) {
            var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
            digitos_iguais = 1;
            if (cnpj.length < 14 && cnpj.length < 15)
                return false;
            for (i = 0; i < cnpj.length - 1; i++)
                if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                    digitos_iguais = 0;
                    break;
                }
            if (!digitos_iguais) {
                tamanho = cnpj.length - 2
                numeros = cnpj.substring(0, tamanho);
                digitos = cnpj.substring(tamanho);
                soma = 0;
                pos = tamanho - 7;
                for (i = tamanho; i >= 1; i--) {
                    soma += numeros.charAt(tamanho - i) * pos--;
                    if (pos < 2)
                        pos = 9;
                }
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
                    return false;
                tamanho = tamanho + 1;
                numeros = cnpj.substring(0, tamanho);
                soma = 0;
                pos = tamanho - 7;
                for (i = tamanho; i >= 1; i--) {
                    soma += numeros.charAt(tamanho - i) * pos--;
                    if (pos < 2)
                        pos = 9;
                }
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1))
                    return false;
                return true;
            }
            else
                return false;
        }


        function alteraTipoPessoa(valor) {
            if (valor == 'J') {
                lblTipoPessoa.SetText('CNPJ:');
                txtCNPJ.SetVisible(true);
                txtCPF.SetVisible(false);
            }
            else {
                lblTipoPessoa.SetText('CPF:');
                txtCNPJ.SetVisible(false);
                txtCPF.SetVisible(true);
            }
        }

        function validaCampos() {
            var numAux = 0;
            var mensagem = "";

            if (txtNomeFantasia.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome fantasia deve ser informado.";
            }
            if (rbTipoPessoa.GetValue() == "J" && txtCNPJ.GetText() != "" && txtCNPJ.GetValue() != null) {
                if (valida_cnpj(txtCNPJ.GetValue()) == false) {
                    numAux++;
                    mensagem += "\n" + numAux + ") O CNPJ informado é inválido.";
                }
            }
            if (rbTipoPessoa.GetValue() == "F" && txtCPF.GetText() != "" && txtCPF.GetValue() != null) {
                if (valida_cpf(txtCPF.GetValue()) == false) {
                    numAux++;
                    mensagem += "\n" + numAux + ") O CPF informado é inválido.";
                }
            }
            if (ddlMunicipio.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O município deve ser informado.";
            }
            if (txtEmail.GetText() != "") {
                if (validateEmail(txtEmail.GetText()) == false) {
                    numAux++;
                    mensagem += "\n" + numAux + ") O email informado é inválido.";
                }
            }
            if (mensagem != "") {
                window.top.mostraMensagem("Alguns dados são de preenchimento obrigatório:\n\n" + mensagem, 'atencao', true, false, null);
                return false;
            }

            return true;
        }

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
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
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_memo(s, e, qtdCaracteres) {
            try { return setMaxLength(s.GetInputElement(), qtdCaracteres); }
            catch (e) { }
        }

        function validateEmail(elementValue) {
            var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            return emailPattern.test(elementValue);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 220px;
        }
        .style2
        {
            width: 160px;
        }
        .style3
        {
            width: 250px;
        }
        .style4
        {
            height: 10px;
        }
        .style5
        {
            width: 125px;
        }
        .headerGrid
        {
            width: 100%;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
        <table style="width:100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Razão Social:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 0px">
                                <dxe:ASPxTextBox ID="txtNomePessoa" runat="server" ClientInstanceName="txtNomePessoa"
                                     MaxLength="150" Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="tdEsconder1">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel runat="server" Text="Nome Fantasia:" 
                        ID="ASPxLabel11">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtNomeFantasia"
                         ID="txtNomeFantasia">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td id="tdEsconder2">
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td style="vertical-align: bottom; width:33.33%">
                                &nbsp;
                            </td>
                            <td style="vertical-align: bottom; width:33.33%">
                                <dxe:ASPxLabel ID="lblTipoPessoa" runat="server" 
                                    Text="CNPJ:" ClientInstanceName="lblTipoPessoa">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: bottom; width:33.33%">
                                <dxe:ASPxLabel ID="lblTipoPessoa0" runat="server" 
                                    Text="Ramo de Atividade:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <dxe:ASPxRadioButtonList ID="rbTipoPessoa" runat="server" 
                                    RepeatDirection="Horizontal" SelectedIndex="1" ItemSpacing="20px" Width="100%">
                                    <Items>
                                        <dxe:ListEditItem Text="Pessoa F&#237;sica" Value="F" />
                                        <dxe:ListEditItem Selected="True" Text="Pessoa Jur&#237;dica" Value="J" />
                                    </Items>
                                    <ClientSideEvents Init="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" SelectedIndexChanged="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" />
                                    <Paddings Padding="0px" />
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td style="vertical-align: top">
                                <dxe:ASPxTextBox ID="txtCPF" runat="server" ClientInstanceName="txtCPF" ClientVisible="False"
                                     Width="100%">
                                    <ValidationSettings ErrorDisplayMode="None">
                                    </ValidationSettings>
                                    <MaskSettings IncludeLiterals="None" Mask="999,999,999-99" PromptChar=" " />
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txtCNPJ" runat="server" ClientInstanceName="txtCNPJ"
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="None">
                                    </ValidationSettings>
                                    <MaskSettings IncludeLiterals="None" Mask="99,999,999/9999-99" PromptChar=" " />
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                            <td style="vertical-align: top">
                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlRamoAtividade"
                                     ID="ddlRamoAtividade">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>
                                    <Items>
                                        <dxe:ListEditItem Text="Construção" Value="C" />
                                        <dxe:ListEditItem Text="Reforma" Value="R" />
                                        <dxe:ListEditItem Text="Ampliação" Value="A" />
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
                <td id="tdEsconder3">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                        Text="Endereço:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtEnderecoPessoa" runat="server" ClientInstanceName="txtEnderecoPessoa"
                         Rows="2" Width="100%">
                        <ClientSideEvents Init="function (s, e) {
    onInit_memo(s, e, 250);
}" />
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td id="tdEsconder4">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                        <tr>
                            <td style="width: 75px">
                                <dxe:ASPxLabel runat="server" Text="UF:"  ID="ASPxLabel12">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Munic&#237;pio:" 
                                    ID="ASPxLabel4">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 75px; padding-right: 10px">
                                <dxe:ASPxComboBox runat="server" TextField="SiglaUF" ValueField="SiglaUF" Width="100%"
                                    ClientInstanceName="ddlUF"  ID="ddlUF">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMunicipio.PerformCallback();
}"></ClientSideEvents>
                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" TextFormatString="{1} - {0}"
                                    Width="100%" ClientInstanceName="ddlMunicipio" 
                                    ID="ddlMunicipio">
                                    <Columns>
                                        <dxe:ListBoxColumn FieldName="SiglaUF" Width="50px" Caption="UF"></dxe:ListBoxColumn>
                                        <dxe:ListBoxColumn FieldName="NomeMunicipio" Width="810px" Caption="Munic&#237;pio">
                                        </dxe:ListBoxColumn>
                                    </Columns>
                                    <ItemStyle Wrap="True"></ItemStyle>
                                    <ListBoxStyle Wrap="True">
                                    </ListBoxStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" id="tdEsconder5">
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td style="vertical-align: bottom; width:33.33%">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                    Text="Nome do Contato:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: bottom; width:33.33%">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Telefone:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: bottom; width:33.33%">
                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                    Text="Email:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <dxe:ASPxTextBox ID="txtNomeContato" runat="server" ClientInstanceName="txtNomeContato"
                                     MaxLength="150" Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                            <td style="vertical-align: top">
                                <dxe:ASPxTextBox ID="txtTelefone" runat="server" ClientInstanceName="txtTelefone"
                                     Width="100%">
                                    <ValidationSettings ErrorDisplayMode="None" Display="None">
                                    </ValidationSettings>
                                    <MaskSettings Mask="(99) 9999-9999" PromptChar=" " />
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                            <td style="vertical-align: top">
                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtEmail"
                                     ID="txtEmail">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="tdEsconder6">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                        Text="Informações do Contato:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtInformacoesContato" runat="server" ClientInstanceName="txtInformacoesContato"
                         Rows="3" Width="100%">
                        <ClientSideEvents Init="function (s, e) {
    onInit_memo(s, e, 250);
}" />
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td id="tdEsconder7">
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                        Text="Comentários:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtComentarios" runat="server" ClientInstanceName="txtComentarios"
                         Rows="4" Width="100%">
                        <ClientSideEvents Init="function (s, e) {
    onInit_memo(s, e, 2000);
}" />
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td align="right">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                    Text="Salvar" Width="95px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	if(validaCampos())
	{
		callbackSalvar.PerformCallback();
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right" style="width: 104px">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False"
                                    Text="Fechar" Width="95px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
        OnCallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MsgErro != null && s.cp_MsgErro != '')
	    mostraDivSalvoPublicado(s.cp_MsgErro);
	else
	{
	    mostraDivSalvoPublicado(traducao.frmCadastroPessoa_raz_o_social_inclu_da_com_sucesso_);
	    window.top.retornoModal = s.cp_CodigoPessoa;
	    window.top.fechaModal();
	}
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
         HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
        Width="270px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td style="text-align: center">
                            </td>
                            <td rowspan="3" style="width: 70px; text-align: center">
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
                            <td style="text-align: center">
                                <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                    >
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
