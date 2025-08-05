<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_007.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_007" meta:resourcekey="PageResource5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .captionForm {
            height: 18px;
        }

        .Tabela {
            width: 100%;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var existeConteudoCampoAlterado = false;

        //        function mostraNomeGerente(s, e) {
        //            txtGerenteUnidade.SetValue(s.GetItem(s.GetSelectedIndex()).GetColumnText(2));
        //        }  

        function btnImprimir_Click(s, e) {
            var codigoProjeto = hfGeral.Get("CodigoProjeto");
            var codigoFormulario = hfGeral.Get("CodigoFormulario");
            var origemMenuProjeto = hfGeral.Get("OrigemMenuProjeto");
            var url = window.top.pcModal.cp_Path + "_Projetos/Administracao/ImpressaoTai.aspx?ModeloTai=tai007&CP=" + codigoProjeto + "&CF=" + codigoFormulario + "&Origem=" + origemMenuProjeto;
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
            var valorProjeto = txtValorProjeto.GetValue() != null;
            var justificativa = txtJustificativa.GetValue() != null;
            var objetivoGeral = txtObjetivoGeral.GetValue() != null;

            return nomeProjeto &&
                lider &&
                unidadeGestora &&
                valorProjeto &&
                objetivoGeral &&
                justificativa;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeProjeto = txtNomeProjeto.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();

            if (!nomeProjeto || 0 === nomeProjeto.length)
                msg += "O campo 'Nome do Projeto/Processo' deve ser informado.\n";
            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Unidade Responsável Pelo Projeto' deve ser informado.\n";
            if (btnSalvar0.cp_ReadOnly != 'S' && frmInformacoesComplementares.validaCamposObrigatorios && !frmInformacoesComplementares.validaCamposObrigatorios()) {
                msg += "Existem campos obrigatórios que não foram preenchidos na aba 'Informações Complementares'.\n";
                pageControl.SetActiveTab(pageControl.GetTabByName('tabInformacoesComplementares'));
            }


            return msg;
        }

        function verificaAvancoWorkflow() {
            //        debugger
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'atencao', true, false, null);
                return false;
            }

            if (!camposPreenchidos) {
                var possuiNomeProjeto = txtNomeProjeto.GetValue() != null;
                var possuiLider = cmbLider.GetValue() != null;
                var possuiUnidadeGestora = cmbUnidadeGestora.GetValue() != null;
                var possuiValorProjeto = txtValorProjeto.GetValue() != null;
                var possuiObjetivoGeral = txtObjetivoGeral.GetValue() != null;
                var possuiJustificativa = txtJustificativa.GetValue() != null;

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
                if (!possuiValorProjeto) {
                    camposNaoPreenchidos[cont] = "Estimativa Orçamento";
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

                var quantidade = camposNaoPreenchidos.length;
                var nomesCampos = "";
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
            }

            return true;
        }

        function ProcessaResultadoCallback(s, e) {
            var result = e.result;

            if (result && result.length && result.length > 0) {
                if (!frmInformacoesComplementares.pnExterno_btnSalvar_1)
                    window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);
                if (result.substring(0, 1) == "I") {
                    var activeTabIndex = pageControl.GetActiveTabIndex();
                    window.location = "./propostaDeIniciativa_007.aspx?CP=" + result.substring(1) + "&tab=" + activeTabIndex;
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

        function defineCampoDescricaoResultado() {
            var valorMeta = valorFinalTransformacao.GetText();
            var nomeIndicador = cbIndicador.GetValue() != null ? cbIndicador.GetText() : "";
            var justificativa = txtJustificativa.GetText();

            if (valorMeta != "" && nomeIndicador != "" && justificativa != "")
                txtDescricaoResultado.SetText(valorMeta + ' ' + nomeIndicador + ' ' + justificativa);
            else
                txtDescricaoResultado.SetText('');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
        <div style="overflow: auto">
            <table border="0" cellpadding="0" cellspacing="0" class="Tabela">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btnSalvar0" runat="server"
                                        Text="Salvar" AutoPostBack="False" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
	btnImprimir.SetEnabled(true);
	btnImprimir0.SetEnabled(true);
}"
                                            Init="function(s, e) {
	if (frmInformacoesComplementares.pnExterno_btnImpressao_1)
		frmInformacoesComplementares.pnExterno_btnImpressao_1.style.display = 'none';
	if (frmInformacoesComplementares.pnExterno_btnSalvar_1)
		frmInformacoesComplementares.pnExterno_btnSalvar_1.style.display = 'none';
}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxButton ID="btnImprimir0" runat="server" ClientInstanceName="btnImprimir0"
                                        Text="Imprimir" AutoPostBack="False" ClientEnabled="False"
                                        Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	btnImprimir_Click(s, e);
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
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
                                                            <table width="94%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                                                                                    Text="Nome do Projeto/Processo" meta:resourcekey="ASPxLabel1Resource1"
                                                                                                    ClientInstanceName="ASPxLabel1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjudaResource1"
                                                                                                    ToolTip="Ao colocar o nome do projeto procure identificá-lo com o Ano.">
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
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Font-Bold="True"
                                                                                                    Text="Gerente do Projeto" meta:resourcekey="ASPxLabel23Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="É o responsável por planejar e programar as tarefas e gestão cotidiana da execução do projeto"
                                                                                                    Width="18px" meta:resourcekey="ASPxImage10Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="margin-left: 120px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Font-Bold="True"
                                                                                                    Text="Unidade Responsável Pelo Projeto" meta:resourcekey="ASPxLabel24Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de Negócio à qual o projeto ou investimento está associado."
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
                                                                                        Width="100%" meta:resourcekey="txtLiderResource1">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td style="padding-right: 10px">
                                                                                    <dxe:ASPxTextBox ID="txtUnidadeGestora" runat="server" ClientInstanceName="txtUnidadeGestora"
                                                                                        Width="100%" meta:resourcekey="txtUnidadeGestoraResource1">
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
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True"
                                                                                                    Text="Gerente do Projeto" meta:resourcekey="ASPxLabel2Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="É o responsável por planejar e programar as tarefas e gestão cotidiana da execução do projeto."
                                                                                                    Width="18px" meta:resourcekey="ASPxImage1Resource1">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="margin-left: 120px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True"
                                                                                                    Text="Unidade Responsável Pelo Projeto" meta:resourcekey="ASPxLabel3Resource1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de Negócio à qual o projeto ou investimento está associado."
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
  }"
                                                                                            SelectedIndexChanged="function(s, e) {
	var value = s.GetValue();
	callbackNomeGerente.PerformCallback(value);
}" />
                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	var value = s.GetValue();
	callbackNomeGerente.PerformCallback(value);
}"
                                                                                            ValueChanged="function(s, e) {
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
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="True"
                                                                                        meta:resourceKey="ASPxLabel2Resource1" Text="Gerente da Unidade Responsável Pelo Projeto">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="ASPxImage29" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage1Resource1"
                                                                                        ToolTip="Gerente da Unidade Responsável Pelo Projeto" Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxcp:ASPxCallbackPanel ID="callbackNomeGerente" runat="server" ClientInstanceName="callbackNomeGerente"
                                                                            OnCallback="callbackNomeGerente_Callback" Width="100%">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                    <dxe:ASPxTextBox ID="txtGerenteUnidade" runat="server" ClientInstanceName="txtGerenteUnidade"
                                                                                        meta:resourceKey="txtLiderResource1" Width="100%"
                                                                                        ReadOnly="True">
                                                                                    </dxe:ASPxTextBox>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxcp:ASPxCallbackPanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Font-Bold="True"
                                                                                        Text="Equipe">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="imgAjuda8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Integrantes do projeto."
                                                                                        Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxwgv:ASPxGridView ID="gvParceiros" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParceiros"
                                                                            DataSourceID="sdsParceiros" KeyFieldName="SequenciaRegistro"
                                                                            OnCellEditorInitialize="gvParceiros_CellEditorInitialize" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                            OnRowInserted="grid_RowInserted" OnRowInserting="gvParceiros_RowInserting" OnRowUpdated="grid_RowUpdated"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                    Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                    <HeaderTemplate>
                                                                                        <% =ObtemBotaoInclusaoRegistro("gvParceiros", "Parceiros")%>
                                                                                    </HeaderTemplate>
                                                                                </dxwgv:GridViewCommandColumn>
                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Nome Usuário" FieldName="CodigoUsuario"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                                                                    <PropertiesComboBox MaxLength="100" TextField="NomeUsuario" ValueField="CodigoUsuario"
                                                                                        DropDownStyle="DropDown" EnableCallbackMode="True" IncrementalFilteringMode="Contains">
                                                                                        <ItemStyle>
                                                                                            <Paddings Padding="0px" />
                                                                                        </ItemStyle>
                                                                                        <ListBoxStyle>
                                                                                        </ListBoxStyle>
                                                                                        <ValidationSettings>
                                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                        </ValidationSettings>
                                                                                        <Style>
                                                                                        
                                                                                    </Style>
                                                                                    </PropertiesComboBox>
                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="0" Visible="True" />
                                                                                    <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="0" CaptionLocation="Top"></EditFormSettings>
                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True"
                                                                                    Visible="False" VisibleIndex="3">
                                                                                    <EditFormSettings Visible="False" />
                                                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Nome Usuário" FieldName="NomeUsuario" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2">
                                                                                    <PropertiesTextEdit MaxLength="100">
                                                                                        <Style>
                                                                                        
                                                                                    </Style>
                                                                                    </PropertiesTextEdit>
                                                                                    <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="1" />
                                                                                    <EditFormSettings Visible="False" VisibleIndex="1" CaptionLocation="Top"></EditFormSettings>
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Telefone" FieldName="NumeroTelefone" ShowInCustomizationForm="True"
                                                                                    Visible="False" VisibleIndex="4" Width="100px">
                                                                                    <PropertiesTextEdit MaxLength="20" Width="100px">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="3" />
                                                                                    <EditFormSettings Visible="False" VisibleIndex="3" CaptionLocation="Top"></EditFormSettings>
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                    Visible="False" VisibleIndex="7">
                                                                                    <PropertiesTextEdit MaxLength="150">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="False" VisibleIndex="2" />
                                                                                    <EditFormSettings ColumnSpan="2" Visible="False" VisibleIndex="2" CaptionLocation="Top"></EditFormSettings>
                                                                                </dxwgv:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
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
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True"
                                                                                        Text="Objetivo do Projeto">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-right: 5px; padding-left: 5px">
                                                                                    <dxe:ASPxLabel ID="lblCountObjetivoGeral" runat="server" ClientInstanceName="lblCountObjetivoGeral"
                                                                                        ForeColor="Silver" Text="0 de 0">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="imgAjuda18" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Indique a situação ou cenário desejado pós implementação do projeto. "
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
                                                                        <table>
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
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descreva o motivo ou a razão da iniciativa que impulsionou a geração do projeto e a sua importância. "
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
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True"
                                                                                        Text="Valor Estimado do Orçamento (R$)">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxImage ID="ASPxImage5" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Valor Estimado do Orçamento (R$)"
                                                                                        Width="18px">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit ID="seValorEstimado" runat="server" ClientInstanceName="seValorEstimado"
                                                                            DisplayFormatString="{0:n2}" Height="21px"
                                                                            Number="0">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                        </dxe:ASPxSpinEdit>
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
                                                            <table width="94%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="Tabela">
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="True"
                                                                                                    Text="Cronograma Básico">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="lblCountCronograma0" runat="server" ClientInstanceName="lblCountCronograma"
                                                                                                    ForeColor="Silver" Text="0 de 0">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda23" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descrição genérica das principais atividades do projeto"
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtCronogramaBasico" runat="server" ClientInstanceName="txtCronogramaBasico"
                                                                                        Height="130px" Width="100%">
                                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            KeyDown="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                                        <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            Init="function(s, e) {
	var labelCount = lblCountCronograma;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel45" runat="server" Font-Bold="True"
                                                                                                    Text="Produto Final / Resultados Esperados">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="lblCountEscopoIniciativa" runat="server" ClientInstanceName="lblCountEscopoIniciativa"
                                                                                                    ForeColor="Silver" Text="0 de 0">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Definição do trabalho que será realizado, descrevendo os produtos de cada etapa e/ou aquilo que será entregue ao término do projeto."
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtEscopoIniciativa" runat="server" ClientInstanceName="txtEscopoIniciativa"
                                                                                        Rows="20" Width="100%" Height="150px">
                                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            KeyDown="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                                        <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            Init="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="font-weight: 700">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel44" runat="server" Font-Bold="True"
                                                                                                    Text="Metas">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="imgAjuda16" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Indique metas físicas, financeiras, de qualidade ou desempenho para medir o alcance dos resultados do projeto. Quantificação do objetivo."
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                                                                                        ClientInstanceName="gvResultados" DataSourceID="sdsResultados"
                                                                                        KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                                        OnCustomJSProperties="grid_CustomJSProperties" OnRowInserted="grid_RowInserted"
                                                                                        OnRowInserting="gvResultados_RowInserting" OnRowUpdated="grid_RowUpdated" OnRowUpdating="gvResultados_RowUpdating"
                                                                                        Width="100%" OnCellEditorInitialize="gvResultados_CellEditorInitialize" OnRowDeleted="gvResultados_RowDeleted">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                Width="90px" Visible="False" Name="colComandos" ShowEditButton="true" ShowDeleteButton="true">
                                                                                                <HeaderTemplate>
                                                                                                    <% =ObtemBotaoInclusaoRegistro("gvResultados", "Resultados")%>
                                                                                                </HeaderTemplate>
                                                                                            </dxwgv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do Resultado" FieldName="SetencaResultado"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                                                                                <PropertiesTextEdit Width="100%" ClientInstanceName="txtDescricaoResultado">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" Caption="Meta:" ColumnSpan="8"
                                                                                                    VisibleIndex="10" />
                                                                                                <EditFormSettings ColumnSpan="8" Visible="True" VisibleIndex="10" CaptionLocation="Top"
                                                                                                    Caption="Meta:"></EditFormSettings>
                                                                                                <EditFormCaptionStyle Font-Bold="True">
                                                                                                </EditFormCaptionStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Especificação/Detalhamento do indicador" FieldName="TransformacaoProduto"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="2" Width="100%">
                                                                                                <PropertiesTextEdit MaxLength="255" Width="100%" ClientInstanceName="txtJustificativa">
                                                                                                    <ClientSideEvents TextChanged="function(s, e) {
	defineCampoDescricaoResultado();
}" />
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="3" Visible="True" VisibleIndex="2"
                                                                                                    Caption="Especificação/Detalhamento do indicador:" />
                                                                                                <EditFormSettings ColumnSpan="3" Visible="True" VisibleIndex="2" CaptionLocation="Top"
                                                                                                    Caption="Especifica&#231;&#227;o/Detalhamento do indicador:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Meta: de" FieldName="ValorInicialTransformacao"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g" MaxValue="9999999999999.99"
                                                                                                    MinValue="-9999999999999.99" ClientInstanceName="valorInicialTransformacao" Width="110px">
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="3" Caption="Meta(De):" />
                                                                                                <EditFormSettings Visible="False" VisibleIndex="3" CaptionLocation="Top" Caption="Meta(De):"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="para" FieldName="ValorFinalTransformacao"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4" Name="ValorFinalTransformacao">
                                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g" MaxValue="9999999999999.99"
                                                                                                    MinValue="-9999999999999.99" NumberFormat="Custom" ClientInstanceName="valorFinalTransformacao"
                                                                                                    Width="100%">
                                                                                                    <ClientSideEvents Validation="function(s, e) {
	validaMeta(s, e);
}"
                                                                                                        ValueChanged="function(s, e) {
	defineCampoDescricaoResultado();
}" />
                                                                                                    <ValidationSettings ErrorText="O valor da meta deve ser igual à soma dos trimestres">
                                                                                                        <ErrorFrameStyle>
                                                                                                            <border bordercolor="Red" borderstyle="Solid" borderwidth="1px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="0" Caption="Valor a ser atingido:"
                                                                                                    ColumnSpan="2" />
                                                                                                <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="0" CaptionLocation="Top"
                                                                                                    Caption="Valor a ser atingido:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador" FieldName="CodigoIndicador"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                                <PropertiesComboBox DataSourceID="sdsIndicador" IncrementalFilteringMode="Contains"
                                                                                                    TextField="NomeIndicador" ValueField="CodigoIndicador" ValueType="System.Int32"
                                                                                                    Width="100%" ClientInstanceName="cbIndicador">
                                                                                                    <Columns>
                                                                                                        <dxe:ListBoxColumn Caption="Indicador" FieldName="NomeIndicador" Width="100%" />
                                                                                                    </Columns>
                                                                                                    <ClientSideEvents Init="function(s, e) {
	if(s.GetValue() != null)
		callbackHTMLIndicador.PerformCallback(s.GetValue());
}"
                                                                                                        SelectedIndexChanged="function(s, e) {
	defineCampoDescricaoResultado();
	callbackHTMLIndicador.PerformCallback(s.GetValue());
}" />
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesComboBox>
                                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="3" Visible="True" VisibleIndex="1"
                                                                                                    Caption="Indicador: &lt;strong title=''&gt;&lt;img alt='' src=&quot;../../imagens/vazio.png&quot;/&gt;&lt;/strong&gt;" />
                                                                                                <EditFormSettings ColumnSpan="3" Visible="True" VisibleIndex="1" CaptionLocation="Top"
                                                                                                    Caption="Indicador: &lt;strong title=&#39;&#39;&gt;&lt;img alt=&#39;&#39; src=&quot;../../imagens/vazio.png&quot;/&gt;&lt;/strong&gt;"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome Indicador" FieldName="NomeIndicador"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataDateColumn Caption="Prazo: até" FieldName="DataLimitePrevista"
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter"
                                                                                                    Width="90px">
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesDateEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="5" Caption="Prazo(Até):" />
                                                                                                <EditFormSettings Visible="False" VisibleIndex="5" CaptionLocation="Top" Caption="Prazo(At&#233;):"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataDateColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequência" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="7">
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor 1º trimestre" FieldName="ValorTrimestre1"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                                <PropertiesSpinEdit ClientInstanceName="valorTrimestre1" DisplayFormatInEditMode="True"
                                                                                                    DisplayFormatString="g" Width="100%" DecimalPlaces="2">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	valorFinalTransformacao.Validate();
}" />
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="5" Caption="Valor 1º Trim:"
                                                                                                    ColumnSpan="2" />
                                                                                                <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="5" CaptionLocation="Top"
                                                                                                    Caption="Valor 1&#186; Trim:"></EditFormSettings>
                                                                                                <CellStyle HorizontalAlign="Right">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor 2º trimestre" FieldName="ValorTrimestre2"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                                <PropertiesSpinEdit ClientInstanceName="valorTrimestre2" DisplayFormatInEditMode="True"
                                                                                                    DisplayFormatString="g" Width="100%" DecimalPlaces="2">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	valorFinalTransformacao.Validate();
}" />
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="6" Caption="Valor 2º Trim:"
                                                                                                    ColumnSpan="2" />
                                                                                                <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="6" CaptionLocation="Top"
                                                                                                    Caption="Valor 2&#186; Trim:"></EditFormSettings>
                                                                                                <CellStyle HorizontalAlign="Right">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor 3º trimestre" FieldName="ValorTrimestre3"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                                <PropertiesSpinEdit ClientInstanceName="valorTrimestre3" DisplayFormatInEditMode="True"
                                                                                                    DisplayFormatString="g" Width="100%" DecimalPlaces="2">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	valorFinalTransformacao.Validate();
}" />
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="7" Caption="Valor 3º Trim:"
                                                                                                    ColumnSpan="2" />
                                                                                                <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="7" CaptionLocation="Top"
                                                                                                    Caption="Valor 3&#186; Trim:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor 4º trimestre" FieldName="ValorTrimestre4"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="11">
                                                                                                <PropertiesSpinEdit ClientInstanceName="valorTrimestre4" DisplayFormatInEditMode="True"
                                                                                                    DisplayFormatString="g" Width="100%" DecimalPlaces="2">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	valorFinalTransformacao.Validate();
}" />
                                                                                                </PropertiesSpinEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="8" Caption="Valor 4º Trim:"
                                                                                                    ColumnSpan="2" />
                                                                                                <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="8" CaptionLocation="Top"
                                                                                                    Caption="Valor 4&#186; Trim:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="14">
                                                                                                <PropertiesTextEdit Width="100%">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings Caption=" Glossário do Indicador:" CaptionLocation="Top" ColumnSpan="9"
                                                                                                    Visible="True" VisibleIndex="11" RowSpan="3" />
                                                                                                <EditFormSettings ColumnSpan="9" RowSpan="3" Visible="True" VisibleIndex="11" CaptionLocation="Top"
                                                                                                    Caption=" Gloss&#225;rio do Indicador:"></EditFormSettings>
                                                                                                <EditItemTemplate>
                                                                                                    <dxe:ASPxLabel ID="lblDescInd" runat="server" ClientInstanceName="lblDescInd" EncodeHtml="False">
                                                                                                    </dxe:ASPxLabel>
                                                                                                    <dxhe:ASPxHtmlEditor ID="htmlIndicador" runat="server" ForeColor="Gray" ClientInstanceName="htmlIndicador"
                                                                                                        Width="100%" ActiveView="Preview" Height="110px">
                                                                                                        <Settings AllowDesignView="False" AllowHtmlView="False" />
                                                                                                        <SettingsDialogs>
                                                                                                            <InsertImageDialog>
                                                                                                                <SettingsImageSelector>
                                                                                                                    <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                                                                </SettingsImageSelector>
                                                                                                            </InsertImageDialog>
                                                                                                            <InsertLinkDialog>
                                                                                                                <SettingsDocumentSelector>
                                                                                                                    <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                                                                                </SettingsDocumentSelector>
                                                                                                            </InsertLinkDialog>
                                                                                                        </SettingsDialogs>

                                                                                                    </dxhe:ASPxHtmlEditor>
                                                                                                </EditItemTemplate>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Pode Excluir" FieldName="podeexcluir" Name="podeexcluir"
                                                                                                ShowInCustomizationForm="False" Visible="False" VisibleIndex="13">
                                                                                                <EditFormSettings Visible="False" />
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Fonte" FieldName="FonteIndicador" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="12" Visible="False">
                                                                                                <PropertiesTextEdit Width="100%">
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings Caption="Fonte:" CaptionLocation="Top" ColumnSpan="8" Visible="True"
                                                                                                    VisibleIndex="9" />
                                                                                                <EditFormSettings ColumnSpan="8" Visible="True" VisibleIndex="9" CaptionLocation="Top"
                                                                                                    Caption="Fonte:"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing EditFormColumnCount="8" Mode="PopupEditForm" />
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="670px" />
                                                                                        </SettingsPopup>
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Formulário de Edição da Meta" />
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="8">
                                                                                        </SettingsEditing>
                                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Formul&#225;rio de Edi&#231;&#227;o da Meta"></SettingsText>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="670px" />
                                                                                        </SettingsPopup>
                                                                                        <Styles>
                                                                                            <EditFormColumnCaption VerticalAlign="Bottom"
                                                                                                CssClass="captionForm">
                                                                                                <Paddings Padding="0px" />
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </EditFormColumnCaption>
                                                                                        </Styles>
                                                                                        <StylesEditors>
                                                                                            <Style>
                                                                                            
                                                                                        </Style>
                                                                                        </StylesEditors>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="25%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel49" runat="server" Font-Bold="True"
                                                                                                    meta:resourceKey="ASPxLabel10Resource1" Text="Detalhamento do orçamento">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; padding-right: 5px;">
                                                                                                <dxe:ASPxLabel ID="lblCountEstimativa" runat="server" ClientInstanceName="lblCountEstimativa"
                                                                                                    ForeColor="Silver" Text="0 de 0">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage28" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage9Resource1"
                                                                                                    ToolTip="Detalhamento do orçamento" Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtValorProjeto" runat="server" ClientInstanceName="txtValorProjeto"
                                                                                        Height="130px" Width="100%">
                                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            KeyDown="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                                        <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            Init="function(s, e) {
	var labelCount = lblCountEstimativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                                                                                    Text="Serviços Compartilhados">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="lblCountServicosCompartilhados" runat="server" ClientInstanceName="lblCountServicosCompartilhados"
                                                                                                    ForeColor="Silver" Text="0 de 0">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px">
                                                                                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Necessidades das áreas compartilhadas (ACTI, ACRH e DIRCOM) para execução desse projeto."
                                                                                                    Width="18px">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtServicosCompartilhados" runat="server" ClientInstanceName="txtServicosCompartilhados"
                                                                                        Height="150px" Width="100%">
                                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            KeyDown="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                                        <ClientSideEvents KeyDown="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyPress="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            KeyUp="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"
                                                                                            ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"
                                                                                            Init="function(s, e) {
	var labelCount = lblCountServicosCompartilhados;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxMemo>
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
                                            <iframe id="frmInformacoesComplementares" frameborder="0" height="<%= alturaTela%>px"
                                                scrolling="no" width="100%" src="../../wfRenderizaFormulario.aspx?CPWF=<%= CodigoProjeto%>&CMF=<%= codigoModeloFormulario%>&AT=<%= alturaTela%>&WSCR=915&RO=<%= readOnlyInf%>&INIPERM=PR_EditaInfComp"></iframe>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 20px">&nbsp;
                    </td>
                    <td align="right" style="padding-top: 20px; padding-right: 10px;">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                        Text="Salvar" AutoPostBack="False" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
	btnImprimir.SetEnabled(true);
	btnImprimir0.SetEnabled(true);
}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxButton ID="btnImprimir" runat="server" ClientInstanceName="btnImprimir"
                                        Text="Imprimir" AutoPostBack="False" ClientEnabled="False"
                                        Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	btnImprimir_Click(s, e);
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT ta.*, u.NomeUsuario as nomeGerenteUnidade FROM [TermoAbertura04] ta
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
        <asp:SqlDataSource ID="sdsIndicadorEstrategico" runat="server" SelectCommand=" SELECT i.[CodigoIndicador]
      , i.[NomeIndicador]  as Meta
      , ioe.[CodigoObjetivoEstrategico] AS CodigoSuperior
   FROM [dbo].[IndicadorUnidade] AS [iu] INNER JOIN 
        [dbo].[Indicador] AS [i] ON (i.[CodigoIndicador]     = iu.[CodigoIndicador]
                                 AND i.[DataExclusao]         IS NULL ) INNER JOIN 
        [dbo].[IndicadorObjetivoEstrategico]  AS [ioe]    ON (ioe.CodigoIndicador    = i.[CodigoIndicador]   )
  WHERE iu.[DataExclusao] IS NULL
    AND iu.[CodigoUnidadeNegocio]   = @CodigoEntidade
    AND (i.[CodigoIndicador] = @CodigoIndicadorAtual OR NOT EXISTS (SELECT 1 FROM tai04_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoIndicador = i.CodigoIndicador AND t.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico) )
  ORDER BY 2"
            OnFiltering="sqlDataSource_Filtering" FilterExpression="CodigoSuperior = {0}">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="CodigoIndicadorAtual" />
            </SelectParameters>
        </asp:SqlDataSource>
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
        <asp:SqlDataSource ID="sdsIndicador" runat="server" SelectCommand=" SELECT indO.NomeIndicador, 
        indO.CodigoIndicador 
   FROM IndicadorOperacional indO
  WHERE indO.CodigoEntidade = @CodigoEntidade
    AND indO.DataExclusao IS NULL 
  ORDER BY indO.NomeIndicador">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsResultados" runat="server" SelectCommand="
        SELECT *,
        (select COUNT(*) from ResultadoMetaOperacional rmo 
         where rmo.CodigoMetaOperacional = ri.CodigoMetaOperacional) podeexcluir
          FROM dbo.tai04_ResultadosIniciativa ri 
         WHERE CodigoProjeto = @CodigoProjeto 
         ORDER BY SetencaResultado"
            DeleteCommand="DELETE FROM tai04_ResultadosIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro] = @SequenciaRegistro"
            InsertCommand="INSERT INTO tai04_ResultadosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[TransformacaoProduto]
           ,[CodigoIndicador]
           ,[NomeIndicador]
           ,[ValorInicialTransformacao]
           ,[ValorFinalTransformacao]
           ,[DataLimitePrevista]
           ,[SetencaResultado]
           ,[ValorTrimestre1]
           ,[ValorTrimestre2]
           ,[ValorTrimestre3]
           ,[ValorTrimestre4]
           ,[FonteIndicador] )
        SELECT
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai04_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@TransformacaoProduto
           ,@CodigoIndicador
           ,@NomeIndicador
           ,0
           ,@ValorFinalTransformacao
           ,@DataLimitePrevista
           ,@SetencaResultado
          , @ValorTrimestre1
          , @ValorTrimestre2
          , @ValorTrimestre3
          , @ValorTrimestre4
          , @FonteIndicador"
            UpdateCommand="UPDATE tai04_ResultadosIniciativa
   SET [TransformacaoProduto] = @TransformacaoProduto
      ,[CodigoIndicador] = @CodigoIndicador
      ,[NomeIndicador] = @NomeIndicador
      ,[ValorInicialTransformacao] = 0
      ,[ValorFinalTransformacao] = @ValorFinalTransformacao
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SetencaResultado] = @SetencaResultado
      ,[ValorTrimestre1] = @ValorTrimestre1
       ,[ValorTrimestre2] = @ValorTrimestre2
       ,[ValorTrimestre3] = @ValorTrimestre3
       ,[ValorTrimestre4] = @ValorTrimestre4
       ,[FonteIndicador] = @FonteIndicador
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
            <DeleteParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="SequenciaRegistro" />
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="TransformacaoProduto" />
                <asp:Parameter Name="CodigoIndicador" />
                <asp:Parameter Name="NomeIndicador" />
                <asp:Parameter Name="ValorFinalTransformacao" />
                <asp:Parameter Name="DataLimitePrevista" />
                <asp:Parameter Name="SetencaResultado" />
                <asp:Parameter Name="ValorTrimestre1" />
                <asp:Parameter Name="ValorTrimestre2" />
                <asp:Parameter Name="ValorTrimestre3" />
                <asp:Parameter Name="ValorTrimestre4" />
                <asp:Parameter Name="FonteIndicador" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            </SelectParameters>
            <UpdateParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
                <asp:Parameter Name="SequenciaRegistro" />
                <asp:Parameter Name="TransformacaoProduto" />
                <asp:Parameter Name="CodigoIndicador" />
                <asp:Parameter Name="NomeIndicador" />
                <asp:Parameter Name="ValorFinalTransformacao" />
                <asp:Parameter Name="DataLimitePrevista" />
                <asp:Parameter Name="SetencaResultado" />
                <asp:Parameter Name="ValorTrimestre1" />
                <asp:Parameter Name="ValorTrimestre2" />
                <asp:Parameter Name="ValorTrimestre3" />
                <asp:Parameter Name="ValorTrimestre4" />
                <asp:Parameter Name="FonteIndicador" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsIndicadorOperacional" runat="server" SelectCommand=" SELECT indO.NomeIndicador, 
        indO.CodigoIndicador 
   FROM IndicadorOperacional indO
  WHERE indO.CodigoEntidade = @CodigoEntidade
    AND indO.DataExclusao IS NULL 
  ORDER BY indO.NomeIndicador">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsUsuariosParceiros" runat="server" SelectCommand=" SELECT us.NomeUsuario, 
        us.CodigoUsuario
   FROM usuario us  inner join 
        UsuarioUnidadeNegocio as unn on (us.CodigoUsuario = unn.CodigoUsuario)
  WHERE 
     unn.CodigoUnidadeNegocio = @CodigoEntidade
    AND us.DataExclusao IS NULL 
  ORDER BY us.NomeUsuario">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsParceiros" runat="server" DeleteCommand="DELETE FROM [tai04_ParceirosIniciativa]
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
            SelectCommand="SELECT [SequenciaRegistro]
      ,[CodigoUsuario]
      ,[NomeUsuario]
      ,[Email]
      ,[NumeroTelefone]
  FROM [tai04_ParceirosIniciativa]
WHERE CodigoProjeto = @CodigoProjeto"
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
        <asp:SqlDataSource ID="sdsResponsaveisAcao" runat="server"></asp:SqlDataSource>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	verificaGravacaoInstanciaWf(); 
	ProcessaResultadoCallback(s, e);
}"
                EndCallback="function(s, e) {
    if(btnSalvar0.cp_ReadOnly != 'S' &amp;&amp; frmInformacoesComplementares.pnExterno_btnSalvar_1)
		frmInformacoesComplementares.pnExterno_btnSalvar_1.click();
}" />
        </dxcb:ASPxCallback>
        <asp:SqlDataSource ID="dsEquipe" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
        <dxcb:ASPxCallback ID="callbackHTMLIndicador" runat="server" ClientInstanceName="callbackHTMLIndicador"
            OnCallback="callbackHTMLIndicador_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    htmlIndicador.SetHtml(s.cp_HTML);
	//valorInicialTransformacao.Validate(); 	
}" />
        </dxcb:ASPxCallback>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
