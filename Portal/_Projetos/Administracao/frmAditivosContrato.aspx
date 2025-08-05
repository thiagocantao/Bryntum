<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAditivosContrato.aspx.cs" Inherits="administracao_frmAditivosContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
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
            var valorAditivo = parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.'));
            var valorContrato = parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.'));
            var novoValor = parseFloat(txtNovoValor.GetValue().toString().replace(',', '.'));
            var text = txtNumeroInstrumento.GetText();
            var contratacao = ddlTipoInstrumento.GetText();

            if (ddlTipoInstrumento.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O tipo do instrumento deve ser informado.";
            }
            if (ddlAditivar.GetValue() == null && (contratacao != 'Termo de Encerramento de Contrato' && contratacao != 'TEC')) {
                numAux++;
                mensagem += "\n" + numAux + ") O campo aditar deve ser informado.";
            }
            if (parseFloat(txtNovoValor.GetValue()) == 0 && (contratacao == 'Termo de Encerramento de Contrato' || contratacao == 'TEC')) {
                if (!confirm("ATENÇÃO!!!\n\nConfirma TEC com valor zero? \n")) {
                    numAux++;
                    mensagem += "\n" + numAux + ") O campo novo valor deve ser informado.";
                }
            }
            if ((ddlAditivar.GetValue() == 'PR' || ddlAditivar.GetValue() == 'PV' || (contratacao == 'Termo de Encerramento de Contrato' || contratacao == 'TEC')) && ddlDataPrazo.GetValue() == null && ddlAditivar.GetValue() != 'TC') {
                numAux++;
                mensagem += "\n" + numAux + ") O novo prazo final deve ser informado.";
            }
            if ((ddlAditivar.GetValue() == 'VL' || ddlAditivar.GetValue() == 'PV' && (contratacao != 'Termo de Encerramento de Contrato' && contratacao != 'TEC')) && parseFloat(parseFloat(txtValorAditivo.GetValue().replace('.', '').replace(',', '.')).toFixed(2)) == 0 && ddlAditivar.GetValue() != 'TC') {
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

            //            if (window.parent.ddlTerminoDeVigencia && ddlDataPrazo.GetValue() != null) {
            //                if (window.parent.ddlTerminoDeVigencia.GetValue() > ddlDataPrazo.GetValue()) {
            //                    numAux++;
            //                    mensagem += "\n" + numAux + ") Não é possível um prazo final/TEC menor que a data de término do contrato.";
            //                }
            //            }


            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
            }
            return mensagemErro_ValidaCamposFormulario;
        }

        function verificaTipoContrato(mudaValor) {
            if (ddlDataPrazo.cp_RO.toString() != "S") {

                var tipoContrato = ddlAditivar.GetValue();
                ddlTipoInstrumento.SetEnabled(TipoOperacao == "Incluir");

                var contratacao = ddlTipoInstrumento.GetText();

                if (contratacao == 'Termo de Encerramento de Contrato' || contratacao == 'TEC') {
                    tipoContrato = 'TC';
                    ddlAditivar.SetEnabled(false);
                }
                else
                    ddlAditivar.SetEnabled(TipoOperacao == "Incluir");

                if (tipoContrato == 'PR') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(false);
                    if (mudaValor) {
                        txtValorAditivo.SetText("");
                    }
                    txtNovoValor.SetEnabled(false);
                    atualizaNovoValor();
                } else if (tipoContrato == 'VL') {
                    ddlDataPrazo.SetEnabled(false);
                    txtValorAditivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(false);

                    if (mudaValor) {
                        ddlDataPrazo.SetValue(null);
                    }

                    atualizaNovoValor();

                } else if (tipoContrato == 'PV' || tipoContrato == 'SC') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(false);
                    atualizaNovoValor();

                } else if (tipoContrato == 'TM') {
                    txtValorAditivo.SetEnabled(false);

                    if (mudaValor) {
                        txtValorAditivo.SetText("");
                        ddlDataPrazo.SetValue(null);
                        txtNovoValor.SetText("");
                    }
                    ddlDataPrazo.SetEnabled(false);
                    txtNovoValor.SetEnabled(false);


                } else if (tipoContrato == 'TC') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(false);

                    if (mudaValor) {
                        ddlAditivar.SetValue(null);
                        txtValorAditivo.SetText("");
                        txtNovoValor.SetText("");
                    }
                } else {
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
            ddlTipoInstrumento.SetValue(null);
            txtNumeroInstrumento.SetText(pnCallback.cp_NumeroNovoInstrumento);
            ddlAditivar.SetValue(null);
            verificaTipoContrato(true);
            txtValorAditivo.SetText("");
            //            txtNovoValor.SetText("");
            txtValorDoContrato.SetText(pnCallback.cp_ValorContrato.toString().replace('.', ','));
            txtNovoValor.SetText(pnCallback.cp_ValorContrato.toString().replace('.', ','));
            ddlDataPrazo.SetValue(null);
            mmMotivo.SetText("");
            txtDataInclusao.SetText("");
            txtUsuarioInclusao.SetText("");
            txtDataAprovacao.SetText("");
            txtUsuarioAprovacao.SetText("");
            atualizaNovoValor();
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAditivoContrato;CodigoTipoContratoAditivo;NumeroContratoAditivo;TipoAditivo;NovoValorContrato;NovaDataVigencia;DescricaoMotivoAditivo;DataInclusao;UsuarioInclusao;DataAprovacaoAditivo;UsuarioAprovacao;ValorContrato;ValorAditivo', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {


            var codigoAditivoContrato = (values[0] != null ? values[0] : "");
            var tipoInstrumento = (values[1] != null ? values[1] : null);
            var numeroInstrumento = (values[2] != null ? values[2] : "");
            var aditivar = (values[3] != null ? values[3] : null);
            var novoValor = (values[4] != null ? values[4] : "");
            var dataPrazo = (values[5] != null ? values[5] : null);
            var motivo = (values[6] != null ? values[6] : "");
            var dataInclusao = (values[7] != null ? values[7] : "");
            var usuarioInclusao = (values[8] != null ? values[8] : "");
            var dataAprovacao = (values[9] != null ? values[9] : "");
            var usuarioAprovacao = (values[10] != null ? values[10] : "");
            var valorContrato = (values[11] != null ? values[11] : "");
            var valorAditivo = (values[12] != null ? values[12] : "");

            ddlTipoInstrumento.SetValue(tipoInstrumento);
            txtNumeroInstrumento.SetText(numeroInstrumento);

            if ((tipoInstrumento == 'Termo de Encerramento de Contrato' || tipoInstrumento == 'TEC'))
                ddlAditivar.SetValue(null);
            else
                ddlAditivar.SetValue(aditivar);

            verificaTipoContrato(true);
            txtValorAditivo.SetText(valorAditivo != null ? valorAditivo.toString().replace('.', ',') : "");
            txtValorDoContrato.SetText(valorContrato != null ? valorContrato.toString().replace('.', ',') : "");
            txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
            ddlDataPrazo.SetValue(dataPrazo);
            mmMotivo.SetText(motivo);
            txtDataInclusao.SetText(dataInclusao);
            txtUsuarioInclusao.SetText(usuarioInclusao);
            txtDataAprovacao.SetText(dataAprovacao);
            txtUsuarioAprovacao.SetText(usuarioAprovacao);
            desabilitaHabilitaComponentes();
        }

        function atualizaNovoValor() {
            var novoValor;

            if (txtValorDoContrato.GetText() != "") {
                try {
                    var valorAditivo = parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.'));
                    var valorContrato = parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.'));
                    if (valorAditivo + valorContrato < 0) {
                        window.top.mostraMensagem('Não é possível colocar um valor de aditivo negativo maior que o valor do contrato', 'atencao', true, false, null);
                        txtValorAditivo.SetFocus();
                        return;
                    }
                    else
                        novoValor = valorAditivo + valorContrato;

                } catch (e) { }
            }

            txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            ddlTipoInstrumento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            txtNumeroInstrumento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            ddlAditivar.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
            verificaTipoContrato(false);
            mmMotivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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

        .style12 {
            width: 140px;
        }

        .style13 {
            width: 120px;
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
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
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
                                    
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                        KeyFieldName="CodigoAditivoContrato" AutoGenerateColumns="False"
                                        ID="gvDados"
                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                        Width="100%">
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
" Init="function(s, e) 
{
	 var sHeight = Math.max(0, document.documentElement.clientHeight) -65;
        s.SetHeight(sHeight);
}"></ClientSideEvents>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px" VisibleIndex="0">
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
                                                    <%# botaoNovo()%>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NumeroContratoAditivo" Name="NumeroContratoAditivo"
                                                Caption="Número Instrumento" VisibleIndex="1" Width="140px">
                                                <Settings AutoFilterCondition="Contains" />
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="2"
                                                Caption="Tipo Instrumento"
                                                FieldName="DescricaoTipoContrato">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoAditivo"
                                                ShowInCustomizationForm="True" VisibleIndex="3" Caption="Aditivo de "
                                                Width="110px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ValorAditivo"
                                                ShowInCustomizationForm="True" VisibleIndex="4" Caption="Valor Aditivo"
                                                Width="120px">
                                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NovoValorContrato"
                                                ShowInCustomizationForm="True" VisibleIndex="5"
                                                Caption="Novo Valor Contrato" Width="135px">
                                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NovaDataVigencia"
                                                ShowInCustomizationForm="True" VisibleIndex="6" Caption="Nova Vigência"
                                                Width="100px">
                                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                </PropertiesTextEdit>
                                                <Settings AutoFilterCondition="Contains" />
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataInclusao"
                                                ShowInCustomizationForm="True" VisibleIndex="7" Caption="Data Inclusão"
                                                Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao"
                                                ShowInCustomizationForm="True" VisibleIndex="9" Caption="Usuário Inclusão"
                                                Width="235px" Visible="False">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoContratoAditivo"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoMotivoAditivo"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="12"
                                                Caption="Aprovador" FieldName="UsuarioAprovacao" Width="280px"
                                                Visible="False">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn Caption="Data Aprovação"
                                                FieldName="DataAprovacaoAditivo" ShowInCustomizationForm="True"
                                                VisibleIndex="13" Width="125px" Visible="False">
                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ValorContrato"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Editavel"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Aditivo de" FieldName="TipoAditivo"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoWorkflow"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>

                                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                                        <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                            ShowHeaderFilterBlankItems="False"></Settings>

                                        <SettingsText></SettingsText>
                                        <Styles>
                                            <StatusBar ForeColor="Red">
                                            </StatusBar>
                                        </Styles>
                                        <Templates>
                                            <StatusBar>
                                                <dxe:ASPxLabel ID="lblStatus0" runat="server"
                                                    Text="Não é possível incluir, editar ou excluir aditivos. Este contrato possui um aditivo pendente de aprovação no fluxo. ">
                                                </dxe:ASPxLabel>
                                            </StatusBar>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                    
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) 
{
    try
	{		
		window.parent.parent.gvDados.PerformCallback();
		}catch(e)
	{}
	try
	{		
		window.parent.gvDados.PerformCallback();
		}catch(e)
	{}
	try
	{		
		if(s.cp_ValorContrato + '' != '')
			window.parent.txtValorComAditivo.SetText(s.cp_ValorContrato.toString().replace('.', ','));
	}catch(e)
	{}
	try
	{		
		if(s.cp_TerminoContrato + '' != '')
			window.parent.ddlTerminoComAditivo.SetText(s.cp_TerminoContrato.toString());
	}catch(e)
	{}
		
	try
	{		
		if(s.cp_StatusContrato + '' != '')
			window.parent.ddlSituacao.SetValue(s.cp_StatusContrato.toString());
	}catch(e)
	{}	

	try
	{
		if (ddlDataPrazo.cp_RO.toString() != 'S') {
			window.parent.btnSalvar.SetEnabled(true);
		}
	}catch(e)
	{}	

	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aditivo incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aditivo alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aditivo excluído com sucesso!&quot;);	
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                                        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="755px"
                                        ID="pcDados" ShowHeader="False">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>

                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="style5" style="padding-top: 10px">&nbsp;</td>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblNumeroContrato0" runat="server"
                                                                                    Text="Tipo Instrumento:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td class="linhaEdicaoPeriodo">
                                                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                    Text="Nº do Instrumento:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td class="style12">
                                                                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Aditar:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 5px;">
                                                                                <dxe:ASPxComboBox ID="ddlTipoInstrumento" runat="server"
                                                                                    ClientInstanceName="ddlTipoInstrumento"
                                                                                    ValueType="System.String" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato(true);
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td class="linhaEdicaoPeriodo" style="padding-right: 5px;">
                                                                                <dxe:ASPxTextBox ID="txtNumeroInstrumento" runat="server"
                                                                                    ClientInstanceName="txtNumeroInstrumento"
                                                                                    MaxLength="25" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td class="style12">
                                                                                <dxe:ASPxComboBox ID="ddlAditivar" runat="server"
                                                                                    ClientInstanceName="ddlAditivar"
                                                                                    ValueType="System.String" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato(true);
}" />
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaTipoContrato(true);
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Escopo" Value="SC" />
                                                                                        <dxe:ListEditItem Text="Prazo" Value="PR" />
                                                                                        <dxe:ListEditItem Text="Valor" Value="VL" />
                                                                                        <dxe:ListEditItem Text="Prazo e Valor" Value="PV" />
                                                                                        <dxe:ListEditItem Text="Troca de Material" Value="TM" />
                                                                                    </Items>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td class="style5" style="padding-top: 10px">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style5">&nbsp;</td>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server"
                                                                                                    Text="Valor Contrato/TEC (R$):">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="]">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server"
                                                                                                    Text="Valor Aditivo (R$):" Width="125px">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server"
                                                                                                    Text="Novo Valor (R$):" Width="125px">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="style13">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel22" runat="server"
                                                                                                    Text="Prazo Final/TEC:">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" style="padding-right: 5px;">
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
                                                                                            </td>
                                                                                            <td align="left" style="padding-right: 5px;">
                                                                                                <dxe:ASPxTextBox ID="txtValorAditivo" runat="server"
                                                                                                    ClientInstanceName="txtValorAditivo" DisplayFormatString="{0:n2}"
                                                                                                    HorizontalAlign="Right" Width="100%">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	atualizaNovoValor();
}"
                                                                                                        LostFocus="function(s, e) {
	atualizaNovoValor();
}" />
                                                                                                    <MaskSettings Mask="&lt;-9999999999999..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                                    <ValidationSettings ErrorDisplayMode="None" Display="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td align="left" style="padding-right: 5px;">
                                                                                                <dxe:ASPxTextBox ID="txtNovoValor" runat="server" ClientEnabled="False"
                                                                                                    ClientInstanceName="txtNovoValor" DisplayFormatString="{0:n2}"
                                                                                                    HorizontalAlign="Right" Width="100%">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                    <MaskSettings Mask="&lt;-9999999999999..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td class="style13">
                                                                                                <dxe:ASPxDateEdit ID="ddlDataPrazo" runat="server"
                                                                                                    ClientInstanceName="ddlDataPrazo" DisplayFormatString="dd/MM/yyyy"
                                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                    Width="100%">
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
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblObservacoes" runat="server"
                                                                                    ClientInstanceName="lblObservacoes" EncodeHtml="False"
                                                                                    Text="Motivo: &amp;nbsp;">
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
                                                                                <dxe:ASPxMemo ID="mmMotivo" runat="server" ClientInstanceName="mmMotivo" Width="100%" Height="55px">
                                                                                    <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"
                                                                                        ValueChanged="function(s, e) {
	
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}"
                                                                                        Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxMemo>
                                                                            </td>
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
                                                                                                ClientInstanceName="txtDataInclusao"
                                                                                                Width="100%" DisplayFormatString="{0:dd/MM/yyyy}">
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
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                    <tr>
                                                                                        <td class="style7">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel17" runat="server"
                                                                                                Text="Data Aprovação:">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel18" runat="server"
                                                                                                Text="Aprovado Por:">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="style7" style="padding-right: 10px;">
                                                                                            <dxe:ASPxTextBox ID="txtDataAprovacao" runat="server" ClientEnabled="False"
                                                                                                ClientInstanceName="txtDataAprovacao"
                                                                                                Width="100%" DisplayFormatString="{0:dd/MM/yyyy}">
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
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <table>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                                                    ClientInstanceName="btnSalvar"
                                                                                                    Text="Salvar" Width="100px">
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	atualizaNovoValor();
    if (window.onClick_btnSalvar)
	{
		try
		{
			if (ddlDataPrazo.cp_RO.toString() != 'S') {
				window.parent.btnSalvar.SetEnabled(false);
			}
		}catch(e)
		{}	

	    onClick_btnSalvar();
	}
}" />
                                                                                                    <Paddings Padding="0px" />
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                                </dxe:ASPxButton>
                                                                                            </td>
                                                                                            <td style="width: 10px"></td>
                                                                                            <td>
                                                                                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                                                    ClientInstanceName="btnFechar"
                                                                                                    Text="Fechar" Width="90px">
                                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
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

        </div>
    </form>
</body>
</html>

