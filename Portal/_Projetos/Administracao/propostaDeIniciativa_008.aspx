<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_008.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_008" meta:resourcekey="PageResource5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .captionForm {
            height: 18px;
        }

        .Tabela {
            width: 100%;
        }

        .auto-style1 {
            height: 27px;
        }

        .textoComIniciaisMaiuscula {
            text-transform: capitalize
        }
    </style>
    <script type="text/javascript" language="javascript">

        var existeConteudoCampoAlterado = false;

        var houveAlteracaoServicoCompartilhado = false;

        //        function mostraNomeGerente(s, e) {
        //            txtGerenteUnidade.SetValue(s.GetItem(s.GetSelectedIndex()).GetColumnText(2));
        //        }  
        function gravaInstanciaWf() {
            try {
                window.parent.executaCallbackWF();
            } catch (e) { }
        }
        function btnImprimir_Click(s, e) {
            var codigoProjeto = hfGeral.Get("CodigoProjeto");
            var codigoFormulario = hfGeral.Get("CodigoFormulario");
            var origemMenuProjeto = hfGeral.Get("OrigemMenuProjeto");
            var url = window.top.pcModal.cp_Path + "_Projetos/Administracao/ImpressaoTai.aspx?ModeloTai=tai008&CP=" + codigoProjeto + "&CF=" + codigoFormulario + "&Origem=" + origemMenuProjeto;
            window.top.showModal(url, "Impressão TAI", 900, screen.height - 250, "", null);
        }

        function maiornum(a, b, c, d) {
            maior = arguments[0];
            for (i = 1; i <= 3; i++) {
                if (arguments[i] > maior)
                    maior = arguments[i];
            }
            return maior;
        }

        function menornum(a, b, c, d) {
            menor = arguments[0];
            for (i = 1; i <= 3; i++) {
                if (arguments[i] < menor)
                    menor = arguments[i];
            }
            return menor;
        }

        function btnSalvar_Click(s, e) {
            var msg = ValidaCampos();
            if (msg == "") {
                callback.PerformCallback("");
                existeConteudoCampoAlterado = false;
            }
            else {
                window.top.mostraMensagem(msg, 'atencao', true, false, null);
            }
        }

        function verificaGravacaoInstanciaWf() {
            try {
                // se a tela estiver dentro de um fluxo
                if (null != window.parent.parent.hfGeralWorkflow) {
                    var codigoInstanciaWf = window.parent.parent.hfGeralWorkflow.Get('CodigoInstanciaWf');

                    // se a instância ainda não estiver registrada;
                    if (-1 == codigoInstanciaWf) {
                        window.parent.executaCallbackWF();
                    }
                }
            } catch (e) { }
        }

        function eventoPosSalvar(codigoInstancia) {
            try {
                window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
            } catch (e) {
            }
        }

        function VerificaCamposObrigatoriosPreenchidos() {
            var nomeProjeto = txtNomeProjeto.GetValue() != null;
            var lider = cmbLider.GetValue() != null;
            var unidadeGestora = cmbUnidadeGestora.GetValue() != null;
            //            var valorProjeto = txtValorProjeto.GetValue() != null;
            //            var justificativa = txtJustificativa.GetValue() != null;
            //            var objetivoGeral = txtObjetivoGeral.GetValue() != null;

            return nomeProjeto && lider && unidadeGestora;
            //                valorProjeto &&
            //                objetivoGeral &&
            //                justificativa;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeProjeto = txtNomeProjeto.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();


            if (!nomeProjeto || 0 === nomeProjeto.length)
                msg += "O campo 'Nome do Projeto/Processo' deve ser informado.\n";
            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Unidade Responsável Pelo Projeto' deve ser informado.\n";

            return msg;
        }

        function verificaAvancoWorkflow() {
            //        debugger
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'atencao', true, false, null);
                return false;
            }

            //if (!camposPreenchidos) {
            var possuiNomeProjeto = txtNomeProjeto.GetValue() != null;
            var possuiLider = cmbLider.GetValue() != null;
            var possuiUnidadeGestora = cmbUnidadeGestora.GetValue() != null;
            var possuiObjetivoGeral = txtObjetivoGeral.GetValue() != null;
            var possuiJustificativa = txtJustificativa.GetValue() != null;
            var possuiValorEstimado = seValorEstimado.GetValue() != null && seValorEstimado.GetValue() != 0;

            var possuiDataInicioProjeto = deDataInicioProjeto.GetValue() != null;
            var possuiDataTerminoProjeto = deDataTerminoProjeto.GetValue() != null;
            var possuiPublicoAlvo = txtPublicoAlvo.GetValue() != null;

            var possuiAcoes = gvAcoes.cpMyRowCount > 0;
            var possuiEntregas = gvEntregas.cpMyRowCount > 0;

            var possuiPremissa = gvPremissa.cpMyRowCount > 0;
            var possuiRestricao = gvRestricao.cpMyRowCount > 0;
            var possuiParceiro = gvParceiro.cpMyRowCount > 0;

            var camposNaoPreenchidos = new Array();
            var cont = 0;

            if (!possuiNomeProjeto) {
                camposNaoPreenchidos[cont] = "Título do Projeto";
                cont++;
            }
            if (!possuiLider) {
                camposNaoPreenchidos[cont] = "Gerente do Projeto";
                cont++;
            }
            if (!possuiUnidadeGestora) {
                camposNaoPreenchidos[cont] = "Unidade Executora";
                cont++;
            }
            if (!possuiObjetivoGeral) {
                camposNaoPreenchidos[cont] = "Objetivo do Projeto";
                cont++;
            }
            if (!possuiJustificativa) {
                camposNaoPreenchidos[cont] = "Justificativa";
                cont++;
            }
            if (!possuiDataInicioProjeto) {
                camposNaoPreenchidos[cont] = "Data Inínio Projeto";
                cont++;
            }
            if (!possuiDataTerminoProjeto) {
                camposNaoPreenchidos[cont] = "Data Término Projeto";
                cont++;
            }
            if (!possuiPublicoAlvo) {
                camposNaoPreenchidos[cont] = "Público Alvo";
                cont++;
            }
            if (!possuiAcoes) {
                camposNaoPreenchidos[cont] = "Ações";
                cont++;
            }
            if (!possuiValorEstimado) {
                camposNaoPreenchidos[cont] = "Valor Estimado do Orçamento";
                cont++;
            }
            if (!possuiEntregas) {
                camposNaoPreenchidos[cont] = "Produtos Intermediários / Entregas";
                cont++;
            }
            if (!possuiPremissa) {
                camposNaoPreenchidos[cont] = "Premissas";
                cont++;
            }
            if (!possuiRestricao) {
                camposNaoPreenchidos[cont] = "Restrições";
                cont++;
            }
            if (!possuiParceiro) {
                camposNaoPreenchidos[cont] = "Parceiros";
                cont++;
            }

            var quantidade = camposNaoPreenchidos.length;
            var nomesCampos = "";
            if (cont == 0)
                return true;
            for (var i = 0; i < quantidade; i++) {
                nomesCampos += "\n" + camposNaoPreenchidos[i];

                if (i == (quantidade - 1))       //Se for o último concatena um '.' (ponto final).
                    nomesCampos += ".";
                else if (i == (quantidade - 2))  //Se for o penúltimo contatena ' e'.
                    nomesCampos += " e";
                else                            //Caso contrário concatena ',' (vírgula).
                    nomesCampos += ",";
            }

            window.top.mostraMensagem("Para prosseguir com o fluxo, é necessário informar os seguintes campos: " + nomesCampos, 'atencao', true, false, null);
            return false;
            //}

            return true;
        }

        function ProcessaResultadoCallback(s, e) {
            var result = e.result;
            var strAuxiliar = result;
            var mensagemErro = "";
            var resultadoSplit = strAuxiliar.split("|");
            if (resultadoSplit[1] != "") {
                mensagemErro = resultadoSplit[1];
            }
            if (mensagemErro != "") {
                window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
            }
            else {
                if (result && result.length && result.length > 0) {

                    window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);

                    if (result.substring(0, 1) == "I") {
                        var activeTabIndex = pageControl.GetActiveTabIndex();
                        window.location = "./propostaDeIniciativa_008.aspx?CP=" + result.substring(1) + "&tab=" + activeTabIndex;
                    }
                }
            }
        }


        function setMaxLength(textAreaElement, length, lblCount) {
            textAreaElement.maxlength = length;
            textAreaElement.LabelCount = lblCount;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var labelCount = textAreaElement.LabelCount;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                labelCount.SetText(text.length + ' de ' + maxLength);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function validaMeta(s, e) {

            if (s.GetValue() == null) {
                e.errorText = 'Campo Obrigatório!';
                e.isValid = false;
                return;
            }

            var criterio = callbackHTMLIndicador.cp_Criterio;
            var agrupamento = callbackHTMLIndicador.cp_Agrupamento;
            var valorMeta = 0;
            var valorTrim1 = valorTrimestre1.GetValue();
            var valorTrim2 = valorTrimestre2.GetValue();
            var valorTrim3 = valorTrimestre3.GetValue();
            var valorTrim4 = valorTrimestre4.GetValue();

            e.isValid = true;

            var casasDecimais = callbackHTMLIndicador.cp_Decimals;

            if (casasDecimais == 0) {
                s.decimalPlaces = 0;
                valorFinalTransformacao.decimalPlaces = 0;
                //valorInicialTransformacao.decimalPlaces = 0;
                valorTrimestre1.decimalPlaces = 0;
                valorTrimestre2.decimalPlaces = 0;
                valorTrimestre3.decimalPlaces = 0;
                valorTrimestre4.decimalPlaces = 0;
                s.numberType = 'i';
                valorFinalTransformacao.numberType = 'i';
                valorTrimestre1.numberType = 'i';
                valorTrimestre2.numberType = 'i';
                valorTrimestre3.numberType = 'i';
                valorTrimestre4.numberType = 'i';

            }
            else {
                s.decimalPlaces = casasDecimais;
                valorFinalTransformacao.decimalPlaces = casasDecimais;
                //valorInicialTransformacao.decimalPlaces = casasDecimais;
                valorTrimestre1.decimalPlaces = casasDecimais;
                valorTrimestre2.decimalPlaces = casasDecimais;
                valorTrimestre3.decimalPlaces = casasDecimais;
                valorTrimestre4.decimalPlaces = casasDecimais;
                s.numberType = 'f';
                valorFinalTransformacao.numberType = 'f';
                //valorInicialTransformacao.numberType = 'f';
                valorTrimestre1.numberType = 'f';
                valorTrimestre2.numberType = 'f';
                valorTrimestre3.numberType = 'f';
                valorTrimestre4.numberType = 'f';
            }

            if (criterio == 'S') {
                if (s.GetValue() != valorTrim4) {
                    e.errorText = 'O valor da meta deve ser igual ao valor do 4º trimestre!';
                    e.isValid = false;
                }
            }
            else {
                if (agrupamento == 'SUM') {
                    valorTotalMeta = valorTrim1 + valorTrim2 + valorTrim3 + valorTrim4;
                    valorTotalDePara = s.GetValue();
                    if (valorTotalDePara != valorTotalMeta) {
                        e.errorText = 'O valor da meta deve ser igual à soma dos trimestres!';
                        e.isValid = false;
                    }
                }
                else if (agrupamento == 'AVG') {
                    valorTotalMeta = (valorTrim1 + valorTrim2 + valorTrim3 + valorTrim4) / 4;

                    if (s.GetValue() != valorTotalMeta) {
                        e.errorText = 'O valor da meta deve ser igual à média dos trimestres!';
                        e.isValid = false;
                    }
                }
                else if (agrupamento == 'MIN') {
                    valorTotalMeta = menornum(valorTrim1, valorTrim2, valorTrim3, valorTrim4);

                    if (s.GetValue() != valorTotalMeta) {
                        e.errorText = 'O valor da meta deve ser igual ao menor valor dos trimestres!';
                        e.isValid = false;
                    }
                }
                else if (agrupamento == 'MAX') {
                    valorTotalMeta = maiornum(valorTrim1, valorTrim2, valorTrim3, valorTrim4);

                    if (s.GetValue() != valorTotalMeta) {
                        e.errorText = 'O valor da meta deve ser igual ao maior valor dos trimestres!';
                        e.isValid = false;
                    }
                }
            }

            //valorInicialTransformacao.Validate();
            valorTrimestre1.Validate();
            valorTrimestre2.Validate();
            valorTrimestre3.Validate();
            valorTrimestre4.Validate();
        }

        function mostraHelp(s, e) {
            e.caption = 'Transformação/Mudança desejada para o indicador de acordo com sua unidade de medida. Exemplo: Aumentar número'
        }

        //        function defineCampoDescricaoResultado() {
        //            var valorMeta = valorFinalTransformacao.GetText();
        //            var nomeIndicador = cbIndicador.GetValue() != null ? cbIndicador.GetText() : "";
        //            var justificativa = txtJustificativa.GetText();

        //            if (valorMeta != "" && nomeIndicador != "" && justificativa != "")
        //                txtDescricaoResultado.SetText(valorMeta + ' ' + nomeIndicador + ' ' + justificativa);
        //            else
        //                txtDescricaoResultado.SetText('');
        //        }

        function MontaCamposFormulario(values) {

            var CodigoAcaoIniciativa = (values[0] != null ? values[0] : "");
            var CodigoProjeto = (values[1] != null ? values[1] : "");
            var NomeAcao = (values[2] != null ? values[2] : "");
            var Responsaveis = (values[3] != null ? values[3] : "");
            var DataInicio = (values[4] != null ? values[4] : "");
            var DataTermino = (values[5] != null ? values[5] : "");
            var ServicoCompartilhado = (values[6] != null ? values[6] : "");
            var ValorPrevisto = (values[7] != null ? values[7] : "");

            txtNomeAcao.SetText(NomeAcao);
            spnValor.SetValue(ValorPrevisto);
            ddlDataInicioAcao.SetValue(DataInicio);
            ddlDataTerminoAcao.SetValue(DataTermino);
            gvResponsaveis.PerformCallback();
            gvServicosCorporativos.PerformCallback();

        }

        function validaCamposFormulario() {
            var mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";
            if (txtNomeAcao.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome da ação deve ser informado.";
            }
            if (ddlDataInicioAcao.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de início da ação deve ser informada.";
            }

            if (ddlDataTerminoAcao.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de término da ação deve ser informada.";
            }

            if (ddlDataInicioAcao.GetValue() > ddlDataTerminoAcao.GetValue()) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de início da ação não pode ser maior que a data de término.";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }
            return mensagemErro_ValidaCamposFormulario;
        }

        function LimpaCamposFormulario() {
            callbackAcoes.PerformCallback("PreparaInclusaoAcao");

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
        <div style="overflow: auto">
            <table border="0" cellpadding="0" cellspacing="0" class="Tabela">
                <tr>
                    <td style="margin-left: 40px">
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                            Width="100%" OnCustomJSProperties="pageControl_CustomJSProperties"
                            meta:resourcekey="pageControlResource1">
                            <TabPages>
                                <dxtc:TabPage Name="tabDescricaoProjeto" Text="Descrição do Projeto">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dxp:ASPxPanel ID="pnlDescricaoProjeto" runat="server" Width="100%" meta:resourcekey="pnlDescricaoProjetoResource1">
                                                <ClientSideEvents Init="function(s, e) {
   s.SetEnabled(s.cp_RO == &quot;N&quot;);
}" />
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server" meta:resourcekey="PanelContentResource1">
                                                        <div id="dv01" runat="server">
                                                            <table width="94%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                                                                                    Text="Nome do Projeto" meta:resourcekey="ASPxLabel1Resource1"
                                                                                                    ClientInstanceName="ASPxLabel1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjudaResource1"
                                                                                                    ToolTip="Identificar o nome do projeto com o nome da unidade e seu ano de vigência">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" ClientInstanceName="txtNomeProjeto"
                                                                                        Width="100%" MaxLength="255" meta:resourcekey="txtNomeProjetoResource1">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="AvailableReadOnly">
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Font-Bold="True"
                                                                                                    Text="Gerente do Projeto" meta:resourcekey="ASPxLabel23Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Principal responsável por acompanhar e gerir a execução do projeto"
                                                                                                    Width="18px" meta:resourcekey="ASPxImage10Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td style="margin-left: 120px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Font-Bold="True"
                                                                                                    Text="Unidade Responsável" meta:resourcekey="ASPxLabel24Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de Negócio que o projeto está associado"
                                                                                                    Width="18px" meta:resourcekey="ASPxImage11Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxTextBox ID="txtLider" runat="server" ClientInstanceName="txtLider"
                                                                                        Width="100%" meta:resourcekey="txtLiderResource1" ClientEnabled="False">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxTextBox ID="txtUnidadeGestora" runat="server" ClientInstanceName="txtUnidadeGestora"
                                                                                        Width="100%" meta:resourcekey="txtUnidadeGestoraResource1"
                                                                                        ClientEnabled="False">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="AvailableOnlyEditing">
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True"
                                                                                                    Text="Gerente do Projeto" meta:resourcekey="ASPxLabel2Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Principal responsável por acompanhar e gerir a execução do projeto"
                                                                                                    Width="18px" meta:resourcekey="ASPxImage1Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td style="margin-left: 120px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True"
                                                                                                    Text="Unidade Responsável" meta:resourcekey="ASPxLabel3Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de Negócio que o projeto está associado"
                                                                                                    Width="18px" meta:resourcekey="ASPxImage2Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxComboBox ID="cmbLider" runat="server" ClientInstanceName="cmbLider" DataSourceID="sdsLider"
                                                                                        ValueType="System.Int32" Width="100%" EnableCallbackMode="True"
                                                                                        IncrementalFilteringMode="Contains" OnItemRequestedByValue="cmbLider_ItemRequestedByValue"
                                                                                        OnItemsRequestedByFilterCondition="cmbLider_ItemsRequestedByFilterCondition"
                                                                                        TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" meta:resourcekey="cmbLiderResource1">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" meta:resourcekey="ListBoxColumnResource1" />
                                                                                            <dxe:ListBoxColumn Caption="E-Mail" FieldName="EMail" Width="300px" meta:resourcekey="ListBoxColumnResource2" />
                                                                                            <dxe:ListBoxColumn Caption="Status" FieldName="StatusUsuario" Width="90px" />
                                                                                        </Columns>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxComboBox ID="cmbUnidadeGestora" runat="server" ClientInstanceName="cmbUnidadeGestora"
                                                                                        DataSourceID="sdsUnidadeGestora" ValueType="System.Int32"
                                                                                        Width="100%" IncrementalFilteringMode="Contains" TextField="NomeUnidadeNegocio"
                                                                                        TextFormatString="{1}" ValueField="CodigoUnidadeNegocio" meta:resourcekey="cmbUnidadeGestoraResource1">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
  }" />
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
  }"></ClientSideEvents>
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="140px" />
                                                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio" Width="300px" />
                                                                                        </Columns>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True"
                                                                                                    meta:resourceKey="ASPxLabel2Resource1" Text="Data Início">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage29" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage1Resource1"
                                                                                                    ToolTip="Indicar a data de início de execução do projeto" Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel57" runat="server" Font-Bold="True"
                                                                                                    meta:resourceKey="ASPxLabel2Resource1" Text="Data Término">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage30" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage1Resource1"
                                                                                                    ToolTip="Indicar a data final de execução do projeto" Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="deDataInicioProjeto" runat="server" ClientInstanceName="deDataInicioProjeto"
                                                                                        DisplayFormatString="d" Width="200px">
                                                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                        </CalendarProperties>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="deDataTerminoProjeto" runat="server" ClientInstanceName="deDataTerminoProjeto"
                                                                                        DisplayFormatString="d" Width="200px">
                                                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                        </CalendarProperties>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True"
                                                                                        Text="Objetivo">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-right: 5px; padding-left: 5px">
                                                                                    <dxe:ASPxLabel ID="lblCountObjetivoGeral" runat="server" ClientInstanceName="lblCountObjetivoGeral"
                                                                                        ForeColor="Silver" Text="0 de 0">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="imgAjuda18" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Finalidade ou propósito para o qual o projeto será desenvolvido"
                                                                                        Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txtObjetivoGeral" runat="server" ClientInstanceName="txtObjetivoGeral"
                                                                            Rows="10" Width="100%" Height="62px">
                                                                            <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                KeyDown="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyPress="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyUp="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                            <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyPress="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyUp="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True"
                                                                                        Text="Justificativa">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-right: 5px; padding-left: 5px">
                                                                                    <dxe:ASPxLabel ID="lblCountJustificativa" runat="server" ClientInstanceName="lblCountJustificativa"
                                                                                        ForeColor="Silver" Text="0 de 0">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descrição da situação (problema ou oportunidade) que justifica o desenvolvimento do projeto"
                                                                                        Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txtJustificativa" runat="server" ClientInstanceName="txtJustificativa"
                                                                            Rows="20" Width="100%" Height="150px">
                                                                            <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                KeyDown="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyPress="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyUp="function(s, e) {
    var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                            <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyPress="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyUp="function(s, e) {
    var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True"
                                                                                        Text="Público-alvo">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-right: 5px; padding-left: 5px">
                                                                                    <dxe:ASPxLabel ID="lblCountPublicoAlvo" runat="server" ClientInstanceName="lblCountPublicoAlvo"
                                                                                        ForeColor="Silver" Text="0 de 0">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="imgAjuda20" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="A quem ou o que o projeto pretende beneficiar"
                                                                                        Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txtPublicoAlvo" runat="server" ClientInstanceName="txtPublicoAlvo"
                                                                            Height="62px" Width="100%">
                                                                            <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 2000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyDown="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 2000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyPress="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 2000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                KeyUp="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 2000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabCronogramaOrcamento" Text="Elementos Operacionais">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dxp:ASPxPanel ID="pnlCronograma" runat="server" Width="100%">
                                                <ClientSideEvents Init="function(s, e) {
	s.SetEnabled(s.cp_RO == &quot;N&quot;);
}" />
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                        <div id="dv3" runat="server">
                                                            <table width="94%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel58" runat="server" Font-Bold="True"
                                                                                                    Text="Ações/Sprint">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda19" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Principais atividades a serem executadas no projeto"
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvAcoes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvAcoes"
                                                                                        DataSourceID="sdsAcoesIniciativa" KeyFieldName="CodigoAcaoIniciativa"
                                                                                        Width="100%" OnCellEditorInitialize="gvAcoes_CellEditorInitialize" OnRowDeleted="grid_RowDeleted"
                                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" OnInitNewRow="gvAcoes_InitNewRow"
                                                                                        OnStartRowEditing="gvAcoes_StartRowEditing" OnCustomJSProperties="grid_CustomJSProperties"
                                                                                        OnRowValidating="gvAcoes_RowValidating" OnCustomCallback="gvAcoes_CustomCallback">
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	gvEquipe.Refresh();
               pnValorEstimado.PerformCallback(checkDesabilitaCampoValorEstimado.GetValue());
	//panelCallbakcValorEstimado.PerformCallback();
}"
                                                                                            DetailRowCollapsing="function(s, e) {
	if(houveAlteracaoServicoCompartilhado){
		houveAlteracaoServicoCompartilhado = false;
		e.cancel = true;
		s.Refresh();
		setTimeout(function(){s.CollapseDetailRow(e.visibleIndex)}, 1500);
	}
}"
                                                                                            BeginCallback="function(s, e) {
	if(e.command == 'REFRESH'){
		houveAlteracaoServicoCompartilhado = false;
	}
}"
                                                                                            CustomButtonClick="function(s, e) {
    s.SetFocusedRowIndex(e.visibleIndex);
    e.processOnServer = false;
    if (e.buttonID == &quot;btnEditar&quot;) 
    {
        TipoOperacao = &quot;Editar&quot;;    
        callbackAcoes.PerformCallback(&quot;PreparaAtualizaAcao&quot;);                                                                 s.GetRowValues(s.GetFocusedRowIndex(),'CodigoAcaoIniciativa;CodigoProjeto;NomeAcao;Responsaveis;DataInicio;DataTermino;ServicoCompartilhado;ValorPrevisto', MontaCamposFormulario); 
pcDados.Show();    
     }
}" />
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                Width="75px" ShowDeleteButton="true">
                                                                                                <CustomButtons>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                                                                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                </CustomButtons>
                                                                                                <HeaderTemplate>
                                                                                                    <% =ObtemBotaoInclusaoRegistro("gvAcoes", "Ações")%>
                                                                                                </HeaderTemplate>
                                                                                            </dxwgv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAcaoIniciativa" ReadOnly="True" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="1">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="2">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Ação/Sprint" FieldName="NomeAcao" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="3">
                                                                                                <PropertiesTextEdit MaxLength="512" Width="100%">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Responsáveis" FieldName="Responsaveis" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="4">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataDateColumn Caption="Data Início" FieldName="DataInicio" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="6" Width="80px">
                                                                                                <PropertiesDateEdit Width="100%">
                                                                                                    <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                                    </CalendarProperties>
                                                                                                    <ValidationSettings Display="Dynamic">
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesDateEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                                            </dxwgv:GridViewDataDateColumn>
                                                                                            <dxwgv:GridViewDataDateColumn Caption="Data Término" FieldName="DataTermino" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="7" Width="95px">
                                                                                                <PropertiesDateEdit Width="100%">
                                                                                                    <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                                                                    </CalendarProperties>
                                                                                                    <ValidationSettings Display="Dynamic">
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesDateEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                                            </dxwgv:GridViewDataDateColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Serviços Corporativos" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="8" FieldName="ServicoCompartilhado">
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" Caption="Serviços Corporativos" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor R$" FieldName="ValorPrevisto" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="9" Width="90px">
                                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="n2" NumberFormat="Custom"
                                                                                                    MaxLength="18" Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Caption="Valor (R$)"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Editar Ação" />
                                                                                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" />
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                Width="600px" AllowResize="true" />
                                                                                        </SettingsPopup>
                                                                                        <Styles>
                                                                                            <DetailCell>
                                                                                                <Paddings Padding="0px" />
                                                                                            </DetailCell>
                                                                                        </Styles>
                                                                                        <Templates>
                                                                                            <DetailRow>
                                                                                                <dx:ASPxDataView ID="dataView" runat="server" Width="100%">
                                                                                                    <ItemTemplate>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxLabel ID="lblGrupoServicoCompartilhado" runat="server" Text='<%# Eval("Key") %>'>
                                                                                                                    </dxe:ASPxLabel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxCheckBoxList ID="cblServicoCompartilhado" runat="server" ValueType="System.Int32"
                                                                                                                        TextField="Descricao" ValueField="Codigo" Width="100%">
                                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {	
	houveAlteracaoServicoCompartilhado = true;
	var value = s.GetItem(e.index).value;
	var parameter = value + &quot;;&quot; + e.isSelected;
	callbackServicosCompartilhados.PerformCallback(parameter);
}" />
                                                                                                                        <Border BorderStyle="None" />
                                                                                                                    </dxe:ASPxCheckBoxList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </ItemTemplate>
                                                                                                    <Paddings Padding="5px" />
                                                                                                    <ItemStyle>
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </ItemStyle>
                                                                                                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                                                                                                </dx:ASPxDataView>
                                                                                            </DetailRow>
                                                                                            <EditForm>
                                                                                                <div <%=dvForm %>>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="right">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel61" runat="server"
                                                                                                                    Text="Ação">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" runat="server"
                                                                                                                    ColumnID="NomeAcao" ReplacementType="EditFormCellEditor" />
                                                                                                            </td>
                                                                                                            <td align="right">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel62" runat="server"
                                                                                                                    Text="Valor (R$)">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" runat="server"
                                                                                                                    ColumnID="ValorPrevisto" ReplacementType="EditFormCellEditor" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="right">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel63" runat="server"
                                                                                                                    Text="Data Início">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement3" runat="server"
                                                                                                                    ColumnID="DataInicio" ReplacementType="EditFormCellEditor" />
                                                                                                            </td>
                                                                                                            <td align="right">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel64" runat="server"
                                                                                                                    Text="Data Término">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement4" runat="server"
                                                                                                                    ColumnID="DataTermino" ReplacementType="EditFormCellEditor" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="4">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel65" runat="server"
                                                                                                                    Font-Bold="true" Text="Responsáveis">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="4" style="margin-left: 40px">
                                                                                                                <dxe:ASPxListBox ID="listBoxResponsaveis" runat="server" ClientInstanceName="listBoxResponsaveis"
                                                                                                                    DataSourceID="sdsResponsaveisAcao" OnDataBound="ASPxListBox1_DataBound" SelectionMode="CheckColumn"
                                                                                                                    TextField="NomeUsuario" Height="160" ValueField="CodigoUsuario" ValueType="System.Int32"
                                                                                                                    Width="100%">
                                                                                                                    <Columns>
                                                                                                                        <dxe:ListBoxColumn FieldName="CodigoUsuario" Visible="False" />
                                                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" />
                                                                                                                        <dxe:ListBoxColumn Caption="E-mail" FieldName="EMail" />
                                                                                                                        <dxe:ListBoxColumn FieldName="Selecionado" Visible="False" />
                                                                                                                    </Columns>
                                                                                                                </dxe:ASPxListBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="4">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                                    Font-Bold="true" Text="Serviços Corporativos">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="4">
                                                                                                                <dxe:ASPxListBox ID="listBoxGrupoServicos" runat="server" ClientInstanceName="listBoxGrupoServicos"
                                                                                                                    DataSourceID="sdsGrupoServico" OnDataBound="ASPxListBox2_DataBound" SelectionMode="CheckColumn"
                                                                                                                    TextField="Grupo" Height="160" ValueField="CodigoServicoCompartilhado" ValueType="System.Int32"
                                                                                                                    Width="100%">
                                                                                                                    <Columns>
                                                                                                                        <dxe:ListBoxColumn FieldName="CodigoServicoCompartilhado" Visible="False" />
                                                                                                                        <dxe:ListBoxColumn Caption="Grupo" FieldName="GrupoServicoCompartilhado" />
                                                                                                                        <dxe:ListBoxColumn Caption="Serviço" FieldName="DescricaoServicoCompartilhado" />
                                                                                                                        <dxe:ListBoxColumn FieldName="Selecionado" Visible="False" />
                                                                                                                    </Columns>
                                                                                                                </dxe:ASPxListBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                                <div style="height: 20px; text-align: right">
                                                                                                    <table style="display: inline-block">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False"
                                                                                                                    Text="Salvar">
                                                                                                                    <ClientSideEvents Click="function(s, e) {
	gvAcoes.UpdateEdit();
}" />
                                                                                                                </dxe:ASPxButton>
                                                                                                            </td>
                                                                                                            <td style="padding-left: 10px">
                                                                                                                <dxe:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="False"
                                                                                                                    Text="Fechar">
                                                                                                                    <ClientSideEvents Click="function(s, e) {
	gvAcoes.CancelEdit();
}" />
                                                                                                                </dxe:ASPxButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </EditForm>
                                                                                        </Templates>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Text="Valor Estimado do Orçamento (R$)">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxtv:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Soma do valor das ações do projeto" Width="18px">
                                                                                                </dxtv:ASPxImage>
                                                                                            </td>
                                                                                            <td style="width: auto">

                                                                                                <dxtv:ASPxCheckBox ID="checkDesabilitaCampoValorEstimado" runat="server" CheckState="Unchecked" ClientInstanceName="checkDesabilitaCampoValorEstimado" Font-Bold="True" Text="Somar Valor Automaticamente?" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	pnValorEstimado.PerformCallback(s.GetValue());
}" />
                                                                                                </dxtv:ASPxCheckBox>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style1">
                                                                                    <dxtv:ASPxCallbackPanel ID="pnValorEstimado" runat="server" ClientInstanceName="pnValorEstimado" OnCallback="pnValorEstimado_Callback" Width="200px">
                                                                                        <PanelCollection>
                                                                                            <dxtv:PanelContent runat="server">
                                                                                                <dxtv:ASPxSpinEdit ID="seValorEstimado" runat="server" ClientInstanceName="seValorEstimado" DecimalPlaces="2" DisplayFormatString="{0:n2}" Height="21px" Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </ReadOnlyStyle>
                                                                                                </dxtv:ASPxSpinEdit>
                                                                                            </dxtv:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dxtv:ASPxCallbackPanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="ASPxLabel19" runat="server" Font-Bold="True" Text="Equipe">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxtv:ASPxImage ID="imgAjuda8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Integrantes do projeto" Width="18px">
                                                                                                </dxtv:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvEquipe" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEquipe"
                                                                                        DataSourceID="sdsEquipe" KeyFieldName="CodigoUsuario"
                                                                                        Width="100%" OnCustomJSProperties="grid_CustomJSProperties">
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                        <SettingsEditing Mode="PopupEditForm">
                                                                                        </SettingsEditing>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeUsuario" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="2">
                                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                                    <Style>
                                                                                                    
                                                                                                </Style>
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" VisibleIndex="1" />
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel59" runat="server" Font-Bold="True"
                                                                                                    Text="Entrega(s) intermediária(s)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage31" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Entregas que ocorrem durante a execução do projeto"
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvEntregas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEntregas"
                                                                                        DataSourceID="sdsProdutoIntermediario" KeyFieldName="CodigoProdutoIntermediario"
                                                                                        Width="100%" OnCustomJSProperties="grid_CustomJSProperties" OnStartRowEditing="gvEntregas_StartRowEditing">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                Width="75px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                                <HeaderTemplate>
                                                                                                    <% =ObtemBotaoInclusaoRegistro("gvEntregas", "Produtos Intermediários / Entregas")%>
                                                                                                </HeaderTemplate>
                                                                                            </dxwgv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProdutoIntermediario" ReadOnly="True"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="2">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Entrega(s) intermediária(s)" FieldName="DescricaoProdutoIntermediario"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                <PropertiesTextEdit MaxLength="512">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Incluir" />
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel45" runat="server" Font-Bold="True"
                                                                                                    Text="Entregas(s) Final(is)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descreva o(s) produto(s) ou serviço(s) que serão entregues ao término do projeto"
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxGridView ID="gvProdutosFinais" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvProdutosFinais" DataSourceID="sdsProdutoFinalResultadosEsperados" KeyFieldName="CodigoProdutoFinal" OnCustomJSProperties="grid_CustomJSProperties" Width="100%" OnStartRowEditing="gvProdutosFinais_StartRowEditing">
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm">
                                                                                        </SettingsEditing>
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                        <SettingsPopup>
                                                                                            <EditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Incluir" />
                                                                                        <Columns>
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="75px">
                                                                                                <HeaderTemplate>
                                                                                                    <% =ObtemBotaoInclusaoRegistro("gvProdutosFinais", "Entregas(s) Final(is)")%>
                                                                                                </HeaderTemplate>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProdutoFinal" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" Caption="CodigoProdutoFinal">
                                                                                                <EditFormSettings Visible="False" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2" Caption="CodigoProjeto">
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewDataTextColumn Caption="Entregas(s) Final(is)" FieldName="DescricaoProdutoFinal" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                <PropertiesTextEdit MaxLength="512">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                            </dxtv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                    </dxtv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage ClientVisible="True" Name="tabInformacoesComplementares" Text="Informações Complementares">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <%--<iframe id="frmInformacoesComplementares" frameborder="0" height="px" scrolling="no"
                                            width="100%" src="../../wfRenderizaFormulario.aspx?CPWF=&amp;CMF=&amp;AT=&amp;WSCR=915&amp;RO=&amp;INIPERM=PR_EditaInfComp">
                                        </iframe>--%>
                                            <div id="dv4" runat="server">
                                                <table class="Tabela">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="Premissas" runat="server" Font-Bold="True"
                                                                            Text="Premissas">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-left: 10px">
                                                                        <dxe:ASPxImage ID="imgAjuda21" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                            Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Eventos/ações definidos no início do planejamento que podem ocorrer ou não, de acordo com os cenários externos/internos, gerando riscos a execução física/financeira do projeto"
                                                                            Width="18px">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxwgv:ASPxGridView ID="gvPremissa" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPremissa"
                                                                DataSourceID="sdsPremissa" KeyFieldName="CodigoPremissa"
                                                                Width="100%" OnCustomJSProperties="grid_CustomJSProperties">
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                        Width="75px" ShowEditButton="true" ShowDeleteButton="true">
                                                                        <HeaderTemplate>
                                                                            <% =ObtemBotaoInclusaoRegistro("gvPremissa", "Premissas")%>
                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoPremissa" ReadOnly="True" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="0">
                                                                        <EditFormSettings Visible="False" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Premissas" FieldName="DescricaoPremissa" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                        <PropertiesTextEdit MaxLength="512">
                                                                        </PropertiesTextEdit>
                                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm" />
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" Width="600px" />
                                                                </SettingsPopup>
                                                                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="Restrições" runat="server" Font-Bold="True"
                                                                            Text="Restrições">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-left: 10px">
                                                                        <dxe:ASPxImage ID="imgAjuda22" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                            Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Ações que mitigam/amenizam os possíveis riscos dos eventos descritos no campo &quot;Premissas&quot;"
                                                                            Width="18px">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxwgv:ASPxGridView ID="gvRestricao" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvRestricao"
                                                                DataSourceID="sdsRestricao" KeyFieldName="CodigoRestricao"
                                                                Width="100%" OnCustomJSProperties="grid_CustomJSProperties">
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                        Width="75px" ShowEditButton="true" ShowDeleteButton="true">
                                                                        <HeaderTemplate>
                                                                            <% =ObtemBotaoInclusaoRegistro("gvRestricao", "Restrições")%>
                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoRestricao" ReadOnly="True" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="0">
                                                                        <EditFormSettings Visible="False" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Restrições" FieldName="DescricaoRestricao"
                                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                                        <PropertiesTextEdit MaxLength="512">
                                                                        </PropertiesTextEdit>
                                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm" />
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" Width="600px" />
                                                                </SettingsPopup>
                                                                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="Parcerias" runat="server" Font-Bold="True"
                                                                            Text="Parcerias">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-left: 10px">
                                                                        <dxe:ASPxImage ID="imgAjuda23" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                            Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Principais parcerias previstas para a realização do projeto"
                                                                            Width="18px">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxwgv:ASPxGridView ID="gvParceiro" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParceiro"
                                                                DataSourceID="sdsParceiro" KeyFieldName="CodigoParceiro"
                                                                Width="100%" OnCustomJSProperties="grid_CustomJSProperties">
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                        Width="75px" ShowEditButton="true" ShowDeleteButton="true">
                                                                        <HeaderTemplate>
                                                                            <% =ObtemBotaoInclusaoRegistro("gvParceiro", "Parceiros")%>
                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoParceiro" ReadOnly="True" ShowInCustomizationForm="True"
                                                                        Visible="False" VisibleIndex="0">
                                                                        <EditFormSettings Visible="False" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Parceiros" FieldName="DescricaoParceiro" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                        <PropertiesTextEdit MaxLength="512">
                                                                        </PropertiesTextEdit>
                                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm" />
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                        AllowResize="True" Width="600px" />
                                                                </SettingsPopup>
                                                                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 0px; padding-right: 10px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                        Text="Salvar" AutoPostBack="False" Width="90px" CssClass="textoComIniciaisMaiuscula">
                                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
	btnImprimir.SetEnabled(true);
	btnImprimir0.SetEnabled(true);
}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxButton ID="btnImprimir" runat="server" ClientInstanceName="btnImprimir"
                                        Text="Imprimir" AutoPostBack="False" ClientEnabled="False" CssClass="textoComIniciaisMaiuscula"
                                        Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	btnImprimir_Click(s, e);
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="sdsDadosFomulario" runat="server"
            SelectCommand="SELECT ta.*, u.NomeUsuario as nomeGerenteUnidade FROM [TermoAbertura04] ta
   inner join UnidadeNegocio un on (un.CodigoUnidadeNegocio = ta.CodigoUnidadeNegocio)
   inner join usuario as u on (un.CodigoUsuarioGerente = u.CodigoUsuario)
WHERE ta.[CodigoProjeto] = @CodigoProjeto"
            OnSelected="sdsDadosFomulario_Selected">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsLider" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsUnidadeGestora" runat="server" SelectCommand=" 
    SELECT 
un.CodigoUnidadeNegocio,
        un.NomeUnidadeNegocio,
        un.SiglaUnidadeNegocio,
        u.NomeUsuario
   FROM UnidadeNegocio un
        inner join usuario as u on (un.CodigoUsuarioGerente = u.CodigoUsuario)
  WHERE un.DataExclusao IS NULL 
    AND un.IndicaUnidadeNegocioAtiva = 'S' 
    AND un.CodigoEntidade = @CodigoEntidade
  ORDER BY 
        un.SiglaUnidadeNegocio ">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsEquipe" runat="server" DeleteCommand="DELETE FROM [tai04_ParceirosIniciativa]
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro"
            InsertCommand="  INSERT INTO [tai04_ParceirosIniciativa]
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[CodigoUsuario]
           ,[NomeUsuario]
           ,[Email]
           ,[NumeroTelefone])
      SELECT @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai04_ParceirosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoUsuario
           ,@NomeUsuario
           ,@Email
           ,@NumeroTelefone"
            SelectCommand=" SELECT DISTINCT
        CodigoUsuario,
        NomeUsuario
   FROM Usuario AS u INNER JOIN 
        tai04_ResponsavelAcao AS ra ON ra.CodigoUsuarioResponsavel = u.CodigoUsuario INNER JOIN
        tai04_AcoesIniciativa AS ai ON ai.CodigoAcaoIniciativa = ra.CodigoAcaoIniciativa
  WHERE ai.CodigoProjeto = @CodigoProjeto"
            UpdateCommand="UPDATE [tai04_ParceirosIniciativa]
   SET [CodigoUsuario] = @CodigoUsuario
      ,[NomeUsuario] = @NomeUsuario
      ,[Email] = @Email
      ,[NumeroTelefone] = @NumeroTelefone
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro">
            <DeleteParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="SequenciaRegistro" />
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="CodigoUsuario" />
                <asp:Parameter Name="NomeUsuario" />
                <asp:Parameter Name="Email" />
                <asp:Parameter Name="NumeroTelefone" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            </SelectParameters>
            <UpdateParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="CodigoUsuario" />
                <asp:Parameter Name="NomeUsuario" />
                <asp:Parameter Name="Email" />
                <asp:Parameter Name="NumeroTelefone" />
                <asp:Parameter Name="SequenciaRegistro" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsResponsaveisAcao" runat="server" SelectCommand=" SELECT DISTINCT 
        us.CodigoUsuario, 
        us.NomeUsuario, 
        us.EMail,
        CASE WHEN ra.CodigoAcaoIniciativa IS NULL THEN 'N' ELSE 'S' END AS Selecionado,
        CASE WHEN ra.CodigoAcaoIniciativa IS NULL THEN  '1' ELSE   '0' END AS ColunaAgrupamento
   FROM Usuario us INNER JOIN 
        UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario LEFT JOIN
        tai04_ResponsavelAcao ra ON ra.CodigoUsuarioResponsavel = us.CodigoUsuario AND ra.CodigoAcaoIniciativa = @CodigoAcao
  WHERE uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'
        AND uun.CodigoUnidadeNegocio = @CodigoEntidade
        AND us.DataExclusao IS NULL
	    AND EXISTS( SELECT 1 
	                  FROM RecursoCorporativo AS rc 
	                 WHERE rc.[CodigoEntidade] = @CodigoEntidade 
	                   AND rc.[CodigoUsuario] = us.CodigoUsuario)
  ORDER BY us.NomeUsuario">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
                <asp:SessionParameter Name="CodigoAcao" SessionField="codigoAcao" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [tai04_ResponsavelAcao] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa

DELETE FROM [tai04_ServicoCompartilhadoIniciativa] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa

DELETE FROM [tai04_AcoesIniciativa] WHERE [CodigoAcaoIniciativa] = @CodigoAcaoIniciativa"
            InsertCommand="INSERT INTO [tai04_AcoesIniciativa] ([CodigoProjeto], [NomeAcao], [DataInicio], [DataTermino], [ValorPrevisto]) VALUES (@CodigoProjeto, @NomeAcao, @DataInicio, @DataTermino, @ValorPrevisto)"
            SelectCommand="SELECT *, dbo.f_getServicoCompartilhadoAcaoTai08(CodigoAcaoIniciativa) AS ServicoCompartilhado, dbo.f_getResponsaveisAcaoTAI08(CodigoAcaoIniciativa) AS Responsaveis FROM [tai04_AcoesIniciativa] WHERE ([CodigoProjeto] = @CodigoProjeto) ORDER BY [NomeAcao]"
            UpdateCommand="UPDATE [tai04_AcoesIniciativa] SET [NomeAcao] = @NomeAcao, [DataInicio] = @DataInicio, [DataTermino] = @DataTermino, [ValorPrevisto] = @ValorPrevisto WHERE [CodigoAcaoIniciativa] = @CodigoAcaoIniciativa"
            ID="sdsAcoesIniciativa" OnInserting="sdsAcoesIniciativa_Inserting">
            <DeleteParameters>
                <asp:Parameter Name="CodigoAcaoIniciativa" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="NomeAcao" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoUsuarioResponsavel" Type="Int32"></asp:Parameter>
                <asp:Parameter Name="NomeUsuarioResponsavel" Type="String"></asp:Parameter>
                <asp:Parameter Name="DataInicio" Type="DateTime"></asp:Parameter>
                <asp:Parameter Name="DataTermino" Type="DateTime"></asp:Parameter>
                <asp:Parameter Name="ValorPrevisto" Type="Decimal"></asp:Parameter>
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter SessionField="codigoProjeto" Name="CodigoProjeto" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="NomeAcao" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoUsuarioResponsavel" Type="Int32"></asp:Parameter>
                <asp:Parameter Name="NomeUsuarioResponsavel" Type="String"></asp:Parameter>
                <asp:Parameter Name="DataInicio" Type="DateTime"></asp:Parameter>
                <asp:Parameter Name="DataTermino" Type="DateTime"></asp:Parameter>
                <asp:Parameter Name="ValorPrevisto" Type="Decimal"></asp:Parameter>
                <asp:Parameter Name="CodigoAcaoIniciativa" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [tai04_ProdutoIntermediario] WHERE [CodigoProdutoIntermediario] = @CodigoProdutoIntermediario"
            InsertCommand="INSERT INTO [tai04_ProdutoIntermediario] ([CodigoProjeto], [DescricaoProdutoIntermediario]) VALUES (@CodigoProjeto, @DescricaoProdutoIntermediario)"
            SelectCommand="SELECT * FROM [tai04_ProdutoIntermediario] WHERE ([CodigoProjeto] = @CodigoProjeto) ORDER BY [DescricaoProdutoIntermediario]"
            UpdateCommand="UPDATE [tai04_ProdutoIntermediario] SET [DescricaoProdutoIntermediario] = @DescricaoProdutoIntermediario WHERE [CodigoProdutoIntermediario] = @CodigoProdutoIntermediario"
            ID="sdsProdutoIntermediario">
            <DeleteParameters>
                <asp:Parameter Name="CodigoProdutoIntermediario" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="DescricaoProdutoIntermediario" Type="String"></asp:Parameter>
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter SessionField="codigoProjeto" Name="CodigoProjeto" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DescricaoProdutoIntermediario" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoProdutoIntermediario" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsGrupoServico" runat="server" SelectCommand="SELECT sc.CodigoServicoCompartilhado,
        sc.DescricaoServicoCompartilhado,
        sc.GrupoServicoCompartilhado,
        (CASE WHEN (sci.CodigoServicoCompartilhado IS NULL) THEN 'N' ELSE 'S' END) AS Selecionado,
        (CASE WHEN (sci.CodigoServicoCompartilhado IS NULL) THEN '1' ELSE '0' END) AS ColunaAgrupamento
   FROM tai04_ServicoCompartilhado AS sc LEFT JOIN
        tai04_ServicoCompartilhadoIniciativa AS sci ON sci.CodigoServicoCompartilhado = sc.CodigoServicoCompartilhado
                                            AND sci.CodigoAcaoIniciativa = @CodigoAcao
  WHERE sc.DataDesativacao IS NULL
        ">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoAcao" SessionField="codigoAcao" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsProdutoFinalResultadosEsperados" runat="server" DeleteCommand="DELETE FROM [dbo].[tai04_ProdutoFinal] WHERE CodigoProdutoFinal = @CodigoProdutoFinal"
            InsertCommand="INSERT INTO [dbo].[tai04_ProdutoFinal] ([CodigoProjeto],[DescricaoProdutoFinal])
        VALUES (@CodigoProjeto, @DescricaoProdutoFinal)"
            SelectCommand="SELECT CodigoProdutoFinal, CodigoProjeto, DescricaoProdutoFinal FROM [tai04_ProdutoFinal] WHERE CodigoProjeto = @CodigoProjeto"
            UpdateCommand="UPDATE [tai04_ProdutoFinal] SET [DescricaoProdutoFinal] = @DescricaoProdutoFinal
                        WHERE CodigoProdutoFinal = @CodigoProdutoFinal 
                          AND [CodigoProjeto] = @CodigoProjeto ">
            <DeleteParameters>
                <asp:Parameter Name="CodigoProdutoFinal" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="DescricaoProdutoFinal" Type="String"></asp:Parameter>
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter SessionField="codigoProjeto" Name="CodigoProjeto" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DescricaoProdutoFinal" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoProdutoFinal" Type="Int32"></asp:Parameter>
                <asp:Parameter Name="CodigoProjeto" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [tai04_Premissa] WHERE [CodigoPremissa] = @CodigoPremissa"
            InsertCommand="INSERT INTO [tai04_Premissa] ([CodigoProjeto], [DescricaoPremissa]) VALUES (@CodigoProjeto, @DescricaoPremissa)"
            SelectCommand="SELECT * FROM [tai04_Premissa] WHERE ([CodigoProjeto] = @CodigoProjeto)"
            UpdateCommand="UPDATE [tai04_Premissa] SET [DescricaoPremissa] = @DescricaoPremissa WHERE [CodigoPremissa] = @CodigoPremissa"
            ID="sdsPremissa">
            <DeleteParameters>
                <asp:Parameter Name="CodigoPremissa" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="DescricaoPremissa" Type="String"></asp:Parameter>
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter SessionField="codigoProjeto" Name="CodigoProjeto" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DescricaoPremissa" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoPremissa" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource runat="server" DeleteCommand="DELETE FROM [tai04_Restricao] WHERE [CodigoRestricao] = @CodigoRestricao"
            InsertCommand="INSERT INTO [tai04_Restricao] ([CodigoProjeto], [DescricaoRestricao]) VALUES (@CodigoProjeto, @DescricaoRestricao)"
            SelectCommand="SELECT * FROM [tai04_Restricao] WHERE ([CodigoProjeto] = @CodigoProjeto)"
            UpdateCommand="UPDATE [tai04_Restricao] SET [DescricaoRestricao] = @DescricaoRestricao WHERE [CodigoRestricao] = @CodigoRestricao"
            ID="sdsRestricao">
            <DeleteParameters>
                <asp:Parameter Name="CodigoRestricao" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="DescricaoRestricao" Type="String"></asp:Parameter>
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter SessionField="codigoProjeto" Name="CodigoProjeto" Type="Int32"></asp:SessionParameter>
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DescricaoRestricao" Type="String"></asp:Parameter>
                <asp:Parameter Name="CodigoRestricao" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsParceiro" runat="server" DeleteCommand="DELETE FROM [tai04_Parceiro] WHERE [CodigoParceiro] = @CodigoParceiro"
            InsertCommand="INSERT INTO [tai04_Parceiro] ([CodigoProjeto], [DescricaoParceiro]) VALUES (@CodigoProjeto, @DescricaoParceiro)"
            SelectCommand="SELECT * FROM [tai04_Parceiro] WHERE ([CodigoProjeto] = @CodigoProjeto)"
            UpdateCommand="UPDATE [tai04_Parceiro] SET [DescricaoParceiro] = @DescricaoParceiro WHERE [CodigoParceiro] = @CodigoParceiro">
            <DeleteParameters>
                <asp:Parameter Name="CodigoParceiro" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
                <asp:Parameter Name="DescricaoParceiro" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="DescricaoParceiro" Type="String" />
                <asp:Parameter Name="CodigoParceiro" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if (s.cp_gravouNovaIni == &quot;1&quot;)
	    gravaInstanciaWf();
	ProcessaResultadoCallback(s, e);
/*
 window.top.mostraMensagem('Salvo com sucesso', 'sucesso', false, false, null);
	verificaGravacaoInstanciaWf(); 
	ProcessaResultadoCallback(s, e);
*/
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackHTMLIndicador" runat="server" ClientInstanceName="callbackHTMLIndicador"
            OnCallback="callbackHTMLIndicador_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    htmlIndicador.SetHtml(s.cp_HTML);
	//valorInicialTransformacao.Validate(); 	
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackAcoes" runat="server" ClientInstanceName="callbackAcoes" OnCallback="callbackAcoes_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_erro != '')
                {      
                          window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);
                }
                if(s.cp_operacao == 'AtualizaAcao')
                          {
                                       window.top.mostraMensagem('Dados atualizados com sucesso!',  'sucesso', false, false, null);
                                      pcDados.Hide();
                                      gvAcoes.PerformCallback();
                                      pnValorEstimado.PerformCallback(checkDesabilitaCampoValorEstimado.GetValue());
                          }
                
                if(s.cp_operacao == 'PreparaInclusaoAcao')
                  {
                        txtNomeAcao.SetText(&quot;&quot;);
                        spnValor.SetValue(null);
                        ddlDataInicioAcao.SetValue(null);
                        ddlDataTerminoAcao.SetValue(null);
                        gvResponsaveis.PerformCallback();
                        gvServicosCorporativos.PerformCallback();
                        pcDados.Show();
                  }
              
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackServicosCompartilhados" runat="server" ClientInstanceName="callbackServicosCompartilhados"
            OnCallback="callbackServicosCompartilhados_Callback">
        </dxcb:ASPxCallback>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxpc:ASPxPopupControl ID="ASPxPopupMsgEdicaoTai" runat="server" HeaderText="Mensagem do Sistema"
            Width="383px" ClientInstanceName="ASPxPopupMsgEdicaoTai"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellpadding="0" cellspacing="0" class="Tabela">
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel ID="ASPxLabel55" runat="server" EncodeHtml="False" Font-Bold="True"
                                    Text="Atenção!!! ">
                                    <ClientSideEvents Init="function(s, e) {
	if(window.parent.textoItem)
		s.SetText('Atenção!!! &lt;br&gt;&lt;br&gt; Somente irão refletir no projeto as alterações nos campos:&lt;br&gt;&lt;ul&gt;&lt;li style=&quot;text-align:left&quot;&gt;Nome do Projeto/Processo&lt;br&gt;&lt;li style=&quot;text-align:left&quot;&gt;Gerente do Projeto&lt;br&gt;&lt;li style=&quot;text-align:left&quot;&gt;Unidade Responsável Pelo Projeto e&lt;br&gt; &lt;li style=&quot;text-align:left&quot;&gt;Objetivo do Projeto.&lt;/ul&gt;');
}" />
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Fechar" AutoPostBack="False"
                                    Width="80px">
                                    <ClientSideEvents Click="function(s, e) {
	ASPxPopupMsgEdicaoTai.Hide();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="ASPxPopupMsg" runat="server" HeaderText="Mensagem do Sistema"
            Width="383px" ClientInstanceName="ASPxPopupMsg"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellpadding="0" cellspacing="0" class="Tabela">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Overline="False"
                                    Style="text-align: center" Text="Não é possível efetuar a operação pois não existem orçamentos liberados para lançamento."
                                    Width="349px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Fechar" AutoPostBack="False"
                                    Width="80px">
                                    <ClientSideEvents Click="function(s, e) {
	ASPxPopupMsg.Hide();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="740px" ID="pcDados">

            <ClientSideEvents CloseUp="function(s, e) {
	gvResponsaveis.ClearFilter();
gvServicosCorporativos.ClearFilter();
}" />

            <ContentStyle>
                <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <div id="divpopup" runat="server">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td>

                                        <table cellpadding="0" cellspacing="0" class="formulario-colunas">
                                            <tr>
                                                <td style="width: 50%">
                                                    <dxtv:ASPxLabel ID="lblNomeAcao" runat="server" ClientInstanceName="lblNomeAcao" Text="Ação/Sprint:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblNomeAcao0" runat="server" ClientInstanceName="lblNomeAcao" Text="Valor (R$):">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="width: 120px">
                                                    <dxtv:ASPxLabel ID="lblNomeAcao1" runat="server" ClientInstanceName="lblNomeAcao" Text="Data Início:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="width: 120px">
                                                    <dxtv:ASPxLabel ID="lblNomeAcao2" runat="server" ClientInstanceName="lblNomeAcao" Text="Data Término:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxTextBox ID="txtNomeAcao" runat="server" ClientInstanceName="txtNomeAcao" Width="100%" MaxLength="512">
                                                        <ValidationSettings ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxSpinEdit ID="spnValor" runat="server" ClientInstanceName="spnValor" Number="0" Width="100%" DisplayFormatString="c2">
                                                        <SpinButtons ClientVisible="False">
                                                        </SpinButtons>
                                                    </dxtv:ASPxSpinEdit>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxDateEdit ID="ddlDataInicioAcao" runat="server" ClientInstanceName="ddlDataInicioAcao" DisplayFormatString="d" Width="100%">
                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                        </CalendarProperties>
                                                    </dxtv:ASPxDateEdit>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxDateEdit ID="ddlDataTerminoAcao" runat="server" ClientInstanceName="ddlDataTerminoAcao" DisplayFormatString="d" Width="100%">
                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                        </CalendarProperties>
                                                    </dxtv:ASPxDateEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="lblNomeAcao3" runat="server" ClientInstanceName="lblNomeAcao" Text="Responsáveis" Font-Bold="True">
                                        </dxtv:ASPxLabel>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gvResponsaveis" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvResponsaveis" KeyFieldName="CodigoUsuario" Width="100%" DataSourceID="sdsResponsaveisAcao" OnCustomCallback="gvResponsaveis_CustomCallback">
                                                            <Templates>
                                                                <GroupRowContent>
                                                                    <%# Container.GroupText == "0" ? "Selecionados" : "Disponíveis" %>
                                                                </GroupRowContent>
                                                            </Templates>
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <Settings VerticalScrollableHeight="100" VerticalScrollBarMode="Auto" />
                                                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn Caption=" " SelectAllCheckboxMode="AllPages" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="40px">
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Nome" FieldName="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4" Caption="Selecionado">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption=" " FieldName="ColunaAgrupamento" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="E-Mail" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="EMail">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxtv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="lblNomeAcao4" runat="server" ClientInstanceName="lblNomeAcao" Text="Serviços Corporativos" Font-Bold="True">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gvServicosCorporativos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvServicosCorporativos" KeyFieldName="CodigoServicoCompartilhado" Width="100%" DataSourceID="sdsGrupoServico" OnCustomCallback="gvResponsaveis_CustomCallback">
                                                            <Templates>
                                                                <GroupRowContent>
                                                                    <%# Container.GroupText == "0" ? "Selecionados" : "Disponíveis" %>
                                                                </GroupRowContent>
                                                            </Templates>
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <Settings VerticalScrollableHeight="100" VerticalScrollBarMode="Auto" ShowGroupPanel="False" />
                                                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn Caption=" " SelectAllCheckboxMode="AllPages" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="40px">
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Grupo" FieldName="GrupoServicoCompartilhado" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption=" " FieldName="ColunaAgrupamento" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Serviço" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="DescricaoServicoCompartilhado">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxtv:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </td>
                                </tr>
                                <tr style="">
                                    <td align="right"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0" width="100%" class="formulario-colunas">
                        <tbody>
                            <tr style="height: 35px">
                                <td align="left" width="310">&nbsp;</td>
                                
                                <td align="right">
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvarAcao" CausesValidation="False" Text="Salvar" Width="100px" ID="btnSalvarAcao">
                                        <ClientSideEvents Click="function(s, e) 
{
    var mensagemErro = validaCamposFormulario();
    if(mensagemErro == '')
    {
        //callbackAcoes.PerformCallback('PreparaAtualizaAcao');   
        callbackAcoes.PerformCallback('AtualizaAcao');
    }
    else
    {
       window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
    }

}"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                                <td align="right" width="90px">
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharAcao" Text="Fechar" Width="100px" ID="btnFecharAcao">
                                        <ClientSideEvents Click="function(s, e) {
pcDados.Hide();
}"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>

    </form>
</body>
</html>
