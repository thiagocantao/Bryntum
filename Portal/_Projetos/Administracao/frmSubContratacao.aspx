<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSubContratacao.aspx.cs"
    Inherits="_Projetos_Administracao_frmSubContratacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function atualizaGrid() {
            pcDados.Close();
        }

        function incluirObrigacao() {
            hfGeral.Set('TipoOperacao', 'Incluir');
            comboTipoPessoa.SetValue(null);
            txtCpfCnpj.SetText('');
            comboClassificacao.SetValue(null)
            txtRazaoSocial.SetText('');
            pcDados.Show();
        }

        function montaCamposFormulario(valores) {
            var TipoPessoa = (valores[0] == null) ? "" : valores[0].toString();
            var NumeroCNPJCPF = (valores[1] == null) ? "" : valores[1].toString();
            var RazaoSocial = (valores[2] == null) ? "" : valores[2].toString();
            var codigoContrato = (valores[3] == null) ? "" : valores[3].toString();
            var Classificacao = (valores[4] == null) ? "" : valores[4].toString();


            comboTipoPessoa.SetValue(TipoPessoa);
            txtCpfCnpj.SetText(NumeroCNPJCPF);
            txtCpfCnpj.SetEnabled(true);
            comboClassificacao.SetValue(Classificacao)
            txtRazaoSocial.SetText(RazaoSocial);
            pcDados.Show();
        }

        function ValidaCamposFormulario() {
            var mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (comboTipoPessoa.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O tipo de pessoa deve ser informado.";
            }
            if (validaCNPJCPF(txtCpfCnpj.GetText().replaceAll('.', '').replaceAll('_', '').replaceAll('/', '').replaceAll('-', '')) == false) {
                numAux++;
                mensagem += "\n" + numAux + ") Por favor, informe um número de CPF/CNPJ válido";
            }
            if (comboClassificacao.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A classificação deve ser informada.";
            }

            if (txtRazaoSocial.GetText() == '') {
                numAux++;
                mensagem += "\n" + numAux + ") A razão social deve ser informada.";
            }
            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
        }

        function validaCNPJCPF(numero) {
            if (numero == null)
                return false;
            if (numero.length < 11)
                return false;
            if (numero.length == 11)
                return valida_cpf(numero);
            else if (numero.length >= 14)
                return valida_cnpj(numero)
            else
                return false;

        }


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

    </script>
</head>
<body style="margin: 5px; margin-top: 7px">
    <form id="form1" runat="server">
        <div>
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
            <dxwgv:ASPxGridView ID="gvSub" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvSub"
                KeyFieldName="CodigoContrato;NumeroCNPJCPF"
                OnCommandButtonInitialize="gvSub_CommandButtonInitialize" OnRowDeleting="gvSub_RowDeleting"
                Width="100%" OnCustomCallback="gvSub_CustomCallback">
                <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                    AllowSort="False" ConfirmDelete="True" />
                <StylesPopup>
                    <EditForm>
                        <Header Font-Bold="True">
                        </Header>
                        <MainArea Font-Bold="False">
                        </MainArea>
                    </EditForm>
                </StylesPopup>
                <Styles>
                    <Header Font-Bold="False">
                    </Header>
                    <HeaderPanel Font-Bold="False">
                    </HeaderPanel>
                    <TitlePanel Font-Bold="True">
                    </TitlePanel>
                </Styles>
                <SettingsPager Mode="ShowAllRecords" Visible="False">
                </SettingsPager>
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3" />
                <SettingsPopup>
                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                        AllowResize="True" Width="600px" VerticalOffset="-40" />
                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"></CustomizationWindow>

                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <SettingsText ConfirmDelete="Confirma a exclusão do registro?" PopupEditFormCaption="Sub Contratada"
                    Title="Sub Contratada" />
                <ClientSideEvents
                    Init="function(s, e) {
		 var sHeight = Math.max(0, document.documentElement.clientHeight) - 75;
        s.SetHeight(sHeight);

}" CustomButtonClick="function(s, e) {
       s.SetFocusedRowIndex(e.visibleIndex);
hfGeral.Set('TipoOperacao', 'Editar');       
 if(e.buttonID == &quot;btnEditar&quot;)
        {
                  s.GetRowValues(e.visibleIndex, 'TipoPessoa;NumeroCNPJCPF;RazaoSocial;CodigoContrato;Classificacao', montaCamposFormulario);
        }
}" />
                <Columns>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="80px" ShowDeleteButton="true">
                        <CustomButtons>
                            <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxtv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <HeaderTemplate>
                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img style=""cursor: pointer"" onclick=""incluirObrigacao();"" src=""../../imagens/botoes/incluirReg02.png"" alt=""Adicionar Obrigação.""/>" : "")%>
                        </HeaderTemplate>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Razão Social / Nome" FieldName="RazaoSocial"
                        Name="RazaoSocial" VisibleIndex="3">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RequiredField ErrorText="Informe Razão Social" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Caption="Razão Social" CaptionLocation="Top" VisibleIndex="2" ColumnSpan="3" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Tipo Pessoa" FieldName="TipoPessoa" Name="TipoPessoa"
                        VisibleIndex="1" Width="100px">
                        <PropertiesComboBox ClientInstanceName="TipoPessoa" AnimationType="None">
                            <ClientSideEvents ValueChanged="function(s, e) {
	//gvSub.PerformCallback(s.GetValue());
}" />
                            <Items>
                                <dxe:ListEditItem Text="Física" Value="F" Selected="True" />
                                <dxe:ListEditItem Text="Jurídica" Value="J" />
                            </Items>
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Tipo Pessoa" CaptionLocation="Top" Visible="True" VisibleIndex="1" />
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CNPJ / CPF" FieldName="NumeroCNPJCPF" VisibleIndex="2"
                        Name="CNPJ" Width="150px">
                        <PropertiesTextEdit ClientInstanceName="NumeroCNPJCPF">
                            <ClientSideEvents Validation="function(s, e) { 
    if (TipoPessoa.GetValue() == &quot;F&quot;)
        e.errorText = &quot;Informe um CPF válido&quot;;

    if (TipoPessoa.GetValue() == &quot;J&quot;)
        e.errorText = &quot;Informe um CNPJ válido&quot;;
      
    e.isValid = validaCNPJCPF(s.GetValue());
}" />
                            <MaskSettings IncludeLiterals="None" Mask="00,000,000/0000-00" ErrorText="Erro de validação" />
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
                            <ValidationSettings ErrorText="Informe CNPJ válido" SetFocusOnError="True" Display="Dynamic">
                                <RequiredField ErrorText="Informe CNPJ/CPF" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Caption="CNPJ / CPF" CaptionLocation="Top" Visible="True" VisibleIndex="1" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Código do Contrato" FieldName="CodigoContrato"
                        Name="CodigoContrato" ShowInCustomizationForm="False" Visible="False" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Classificação" FieldName="Classificacao"
                        Name="Classificacao" VisibleIndex="10" Width="150px">
                        <PropertiesComboBox ClientInstanceName="txtClassificação" MaxLength="50" Width="300px">
                            <ClientSideEvents Validation="function(s, e) {
	//onValidation_NumeroAditivo(s, e);
}" />
                            <Items>
                                <dxe:ListEditItem Text="Parceira" Value="Parceira" />
                                <dxe:ListEditItem Text="Sub Contratada" Value="Sub Contratada" />
                            </Items>
                            <ValidationSettings>
                                <RequiredField ErrorText="Informe a classificação" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings Caption="Classificação:" CaptionLocation="Top" VisibleIndex="1"
                            Visible="True" />
                        <EditCellStyle>
                        </EditCellStyle>
                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                    </dxwgv:GridViewDataComboBoxColumn>
                </Columns>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="135" />
                <StylesEditors>
                    <Style></Style>
                </StylesEditors>
                <Border BorderStyle="Solid" />
            </dxwgv:ASPxGridView>
            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxhf:ASPxHiddenField>
        </div>
        <dxcp:ASPxPopupControl ID="pcDados" runat="server" Height="100px" Width="485px" ClientInstanceName="pcDados" AllowDragging="True" AllowResize="True" CloseAction="None" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False">
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel30" runat="server" Text="Tipo Pessoa:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td>

                                            <dxtv:ASPxCallbackPanel ID="callbackLabelCnpjCpf" runat="server" ClientInstanceName="callbackLabelCnpjCpf" Width="100%" OnCallback="callbackLabelCnpjCpf_Callback">
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxLabel ID="labelCnpjCpf" runat="server" Text="CNPJ/CPF: *" ClientInstanceName="labelCnpjCpf">
                                                        </dxtv:ASPxLabel>
                                                    </dxtv:PanelContent>
                                                </PanelCollection>
                                            </dxtv:ASPxCallbackPanel>
                                        </td>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel32" runat="server" Text="Classificação: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 5px">
                                            <dxtv:ASPxComboBox ID="comboTipoPessoa" runat="server" ClientInstanceName="comboTipoPessoa">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	        callbackLabelCnpjCpf.PerformCallback(s.GetValue());
        callbackCnpjCpf.PerformCallback(s.GetValue());

}" />
                                                <Items>
                                                    <dxtv:ListEditItem Text="Física" Value="F" />
                                                    <dxtv:ListEditItem Text="Jurídica" Value="J" />
                                                </Items>
                                            </dxtv:ASPxComboBox>
                                        </td>
                                        <td style="padding-right: 5px">
                                            <dxtv:ASPxCallbackPanel ID="callbackCnpjCpf" runat="server" ClientInstanceName="callbackCnpjCpf" Width="100%" OnCallback="callbackCnpjCpf_Callback">
                                                <ClientSideEvents EndCallback="function(s, e) {
	txtCpfCnpj.SetEnabled(true);
}" />
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxTextBox ID="txtCpfCnpj" runat="server" ClientInstanceName="txtCpfCnpj" Width="170px" ClientEnabled="False">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxTextBox>
                                                    </dxtv:PanelContent>
                                                </PanelCollection>
                                            </dxtv:ASPxCallbackPanel>
                                        </td>
                                        <td>
                                            <dxtv:ASPxComboBox ID="comboClassificacao" runat="server" ClientInstanceName="comboClassificacao">
                                                <Items>
                                                    <dxtv:ListEditItem Text="Parceira" Value="Parceira" />
                                                    <dxtv:ListEditItem Text="Sub Contratada" Value="Sub Contratada" />
                                                </Items>
                                            </dxtv:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px">
                                <dxtv:ASPxLabel ID="ASPxLabel33" runat="server" Text="Razão Social: *">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxTextBox ID="txtRazaoSocial" runat="server" ClientInstanceName="txtRazaoSocial" Width="100%">
                                </dxtv:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top: 5px">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 5px">
                                            <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
      var msgValidacao = ValidaCamposFormulario();
      if(msgValidacao == '')
      {
                  callbackSalvar.PerformCallback();
      }
      else
      {
                  window.top.mostraMensagem(msgValidacao, 'erro', true, false, null, null);
       }
}" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td>
                                            <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
                txtCpfCnpj.SetEnabled(false);
	pcDados.Hide();
}" />
                                            </dxtv:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
      if(s.cpErro !== '')
      {
               window.top.mostraMensagem(s.cpErro, 'erro', true, false, null, null);
      }
      else if(s.cpSucesso !== '')
     {
         window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, atualizaGrid, 3000);
     }
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
