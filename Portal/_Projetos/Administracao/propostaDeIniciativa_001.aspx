<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_001.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_001" meta:resourcekey="PageResource5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tabela
        {
            width: 100%;
        }
         .style5
        {
            width: 176px;
        }
        .style6
        {
            height: 10px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var atualizaTab = 'S';
        var existeConteudoCampoAlterado = false;

        function btnSalvar_Click(s, e) {
            var msg = ValidaCampos();
            if (msg == "") {
                callback.PerformCallback("");
                existeConteudoCampoAlterado = false;
                btnImprimir.SetEnabled(true);
                btnImprimir0.SetEnabled(true);
            }
            else {
                window.top.mostraMensagem(msg, 'atencao', true, false, null);
            }
        }

        function gravaInstanciaWf() {
            try {
                window.parent.executaCallbackWF();
            } catch (e) { }
        }

        function eventoPosSalvar(codigoInstancia) {
            try {
                window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
            } catch (e) {
            }
        }

        function VerificaCamposObrigatoriosPreenchidos() {



            if (pageControl.cpReadOnly)
                return true;

            var nomeProjeto = txtNomeProjeto.GetValue() != null;
            var lider = cmbLider.GetValue() != null;
            var unidadeGestora = cmbUnidadeGestora.GetValue() != null;
            var dataInicio = deDataInicio.GetValue() != null;
            var dataTermino = deDataTermino.GetValue() != null;
            var objetivoGeral = txtObjetivoGeral.GetValue() != null;
            var podeSalvar = (hfGeral.Get("PodeSalvarResultados") + "") != "N";
            var podeSalvarAtividades = (hfGeral.Get("PodeSalvarAtividade") + "") != "N";
            var podeSalvarAcao = (hfGeral.Get("PodeSalvarAcao") + "") != "N";
            var podeSalvarMetas = (hfGeral.Get("PodeSalvarMetas") + "") != "N";
            var podeSalvarCronograma = (hfGeral.Get("podeSalvarCronograma") + "") != "N";
            var anoOrcamento = cmbAnoOrcamento.GetValue() != null;

            return nomeProjeto &&
                lider &&
                unidadeGestora &&
                dataInicio &&
                dataTermino &&
                objetivoGeral &&
                podeSalvar &&
                podeSalvarAtividades &&
                podeSalvarMetas &&
                podeSalvarCronograma &&
                podeSalvarAcao &&
                anoOrcamento;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeProjeto = txtNomeProjeto.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();
            var anoOrcamento = cmbAnoOrcamento.GetValue();
            if (!nomeProjeto || 0 == nomeProjeto.length)
                msg += "O campo 'Nome do projeto' deve ser informado.\n";
            if (nomeProjeto.length < 10)
                msg += "O campo 'Nome do projeto' deve ser informado com no mínimo 10 caracteres.\n";

            if (!anoOrcamento || 0 == anoOrcamento.length)
                msg += "O campo 'Ano do Orçamento' deve ser informado.\n";

            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Área Responsável' deve ser informado.\n";
            if (!cbFundecoop.GetChecked() && !cbRecursoProprio.GetChecked())
                msg += "Deve ser informada ao menos uma opção para o campo 'Fonte de Recurso'.\n";
            if (deDataInicio.GetValue() > deDataTermino.GetValue()) {
                msg += "A data de início não pode ser maior que a data de término.\n";
            }

            if ((txtPublicoAlvo.GetValue() != null || txtPublicoAlvo.GetText() != "") && txtPublicoAlvo.GetText().length < 10) {
                msg += "Público Alvo deve ser informado com no mínimo 10 caracteres.\n";
            }
            if ((txtBeneficiarios.GetValue() != null || txtBeneficiarios.GetText() != "") && txtBeneficiarios.GetText().length < 10) {
                msg += "Beneficiário deve ser informado com no mínimo 10 caracteres.\n";
            }
            if ((txtJustificativa.GetValue() != null || txtJustificativa.GetText() != "") && txtJustificativa.GetText().length < 10) {
                msg += "Justificativa deve ser informado com no mínimo 10 caracteres.\n";
            }
            if ((txtResultadosContinuidade.GetValue() != null || txtResultadosContinuidade.GetText() != "") && txtResultadosContinuidade.GetText().length < 10) {
                msg += "Resultados continuidade deve ser informado com no mínimo 10 caracteres.\n";
            }
            if ((txtObjetivoGeral.GetValue() != null || txtObjetivoGeral.GetText() != "") && txtObjetivoGeral.GetText().length < 10) {
                msg += "Objetivo Geral deve ser informado com no mínimo 10 caracteres.\n";
            }


            return msg;
        }

        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
            if (hfGeral.Get("PodePassarFluxo") == "N") {
                window.top.mostraMensagem("Somente o usuário que inicializou o processo poderá passar para a próxima etapa do fluxo.", 'atencao', true, false, null);
                return false;
            }

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'atencao', true, false, null);
                return false;
            }
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();
            if (!camposPreenchidos) {
                var possuiNomeProjeto = txtNomeProjeto.GetValue() != null;
                var possuiLider = cmbLider.GetValue() != null;
                var possuiUnidadeGestora = cmbUnidadeGestora.GetValue() != null;
                var possuiDataInicio = deDataInicio.GetValue() != null;
                var possuiDataTermino = deDataTermino.GetValue() != null;
                var possuiObjetivoGeral = txtObjetivoGeral.GetValue() != null;
                var podeSalvar = hfGeral.Get("PodeSalvarResultados") + "" != "N";
                var podeSalvarAtividades = hfGeral.Get("PodeSalvarAtividade") + "" != "N";
                var podeSalvarAcao = hfGeral.Get("PodeSalvarAcao") + "" != "N";
                var podeSalvarMetas = hfGeral.Get("PodeSalvarMetas") + "" != "N";
                var podeSalvarCronograma = hfGeral.Get("podeSalvarCronograma") + "" != "N";
                var possuiAnoOrcamento = cmbAnoOrcamento.GetValue() != null;
                var camposNaoPreenchidos = new Array();
                var cont = 0;

                if (!possuiNomeProjeto) {
                    camposNaoPreenchidos[cont] = "Nome do Projeto";
                    cont++;
                }
                if (!possuiAnoOrcamento) {
                    camposNaoPreenchidos[cont] = "Ano do Orçamento";
                    cont++;
                }
                if (!possuiLider) {
                    camposNaoPreenchidos[cont] = "Coordenador do Projeto";
                    cont++;
                }
                if (!possuiUnidadeGestora) {
                    camposNaoPreenchidos[cont] = "Área Responsável";
                    cont++;
                }
                if (!possuiDataInicio) {
                    camposNaoPreenchidos[cont] = "Data de Início";
                    cont++;
                }
                if (!possuiDataTermino) {
                    camposNaoPreenchidos[cont] = "Data de Término";
                    cont++;
                }
                if (!possuiObjetivoGeral) {
                    camposNaoPreenchidos[cont] = "Objetivo Geral do Projeto";
                    cont++;
                }
                if (!podeSalvar) {
                    camposNaoPreenchidos[cont] = "Não foram cadastrados resultados esperados ou existem resultados esperados sem indicador relacionado"
                    cont++;
                }
                if (!podeSalvarAtividades) {
                    camposNaoPreenchidos[cont] = "Existem atividades sem produtos relacionados"
                    cont++;
                }

                if (!podeSalvarAcao) {
                    camposNaoPreenchidos[cont] = "Existem ações sem atividades relacionados"
                    cont++;
                }
                if (!podeSalvarMetas) {
                    camposNaoPreenchidos[cont] = "Existem ações sem metas relacionados"
                    cont++;
                }
                if (!podeSalvarCronograma) {
                    camposNaoPreenchidos[cont] = "No cronograma orçamentário, os registros devem possuir conta com valor diferente de zero e memória de cálculo informada."
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
            hfGeral.Set("CodigoAcao", codigoAcao);
            if (callbackConflitos.cpExecutaVerificacao && gvDados.cp_CodigoProjeto != -1) {
                var acoes = hfGeral.Get("Acoes");
                for (var i = 0; i < acoes.length; i++) {
                    if (acoes[i] == codigoAcao) {
                        callbackConflitos.PerformCallback("S");
                        return false;
                    }
                }
            }
            if (callbackConflitos.cpMudaStatusProposta) {
                callbackConflitos.PerformCallback("N");
            }
            return true;
        }

        function ProcessaResultadoCallback(s, e) {
            var result = e.result;
            if (result && result.length && result.length > 0) {

                window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);

                if (result.substring(0, 1) == "I") {
                    var codigoNovoProjeto = result.substring(1);
                    var codigoWorkflow = hfGeral.Get("CodigoWorkflow");
                    var codigoEtapaWf = hfGeral.Get("CodigoEtapaWf");
                    var codigoInstanciaWf = hfGeral.Get("CodigoInstanciaWf");
                    var activeTabIndex = pageControl.GetActiveTabIndex();
                    window.location = "./propostaDeIniciativa_001.aspx?CP=" + codigoNovoProjeto + "&CWF=" + codigoWorkflow + "&CEWF=" + codigoEtapaWf + "&CIWF=" + codigoInstanciaWf + "&tab=" + activeTabIndex;
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

        function incluiAtividade(codigoAcao, numeroAcao, faseTAI) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAtividades.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + "&NA=" + numeroAcao + "&FaseTAI=" + faseTAI, 'Nova Atividade', 900, 500, funcaoPosModal, null);
        }

        function editaAtividade(codigoAtividade, codigoAcao) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAtividades.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + '&CodigoAtividade=' + codigoAtividade, 'Edição da Atividade', 900, 500, funcaoPosModal, null);
        }

        function excluiAtividade(codigoAtividade) {
            if (confirm("ATENÇÃO!!!\n\nEste processo também vai excluir todos os Produtos, Marcos Parcerias e Cronograma Orçamentário relacionados com a atividade selecionada!\n\nConfirma a exclusão da atividade selecionada? \n"))
                callbackAtividade.PerformCallback(codigoAtividade);
        }

        function incluiAcao(faseTAI) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;
            var sequencia = gvDados.cp_Sequencia;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAcoes.aspx?CA=-1&CP=' + codigoProjeto + '&SQ=' + sequencia + "&FaseTAI=" + faseTAI, 'Nova Ação', 850, 500, funcaoPosModal, null);
        }

        function editaAcao(codigoAcao) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;
            var sequencia = gvDados.cp_Sequencia;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAcoes.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + '&SQ=' + sequencia, 'Edição da Ação', 850, 500, funcaoPosModal, null);
        }

        function excluiAcao(codigoAcao) {
            if (confirm("ATENÇÃO!!!\n\nEste processo também vai excluir todas as Metas, Atividades, Produtos, Marcos, Parcerias e Cronograma Orçamentário relacionados com a ação selecionada!\n\nConfirma a exclusão da ação selecionada? \n"))
                callbackPlanoTrabalho.PerformCallback(codigoAcao);
        }

        function funcaoPosModal(valor) {
            if (valor == 'S')
                gvDados.PerformCallback();
        }

        function ExibeTelaConflitosAgenda() {
            var altura = screen.height - 240;
            var codigoEntidade = hfGeral.Get("CodigoEntidade");
            var codigoProjeto = hfGeral.Get("CodigoProjeto");
            var url = null;

            url = window.top.pcModal.cp_Path + '_Projetos/Administracao/ConflitosAgenda.aspx?CE=' + codigoEntidade + '&CP=' + codigoProjeto + '&AL=' + altura;

            window.top.showModal(url, "Conflitos de Agenda", 1020, altura, "", null);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
    <div style="overflow: auto">
        &nbsp;<table border="0" cellpadding="0" cellspacing="0" class="Tabela">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td  width="100px">
                                <dxe:ASPxButton ID="btnImprimir" runat="server" 
                                    Text="Imprimir" AutoPostBack="False" meta:resourcekey="btnSalvar0Resource1" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
    //codigo da tela de proposta de iniciativa 4    
    var codigoProjeto = hfGeral.Get(&quot;CodigoProjeto&quot;);
    var url = window.top.pcModal.cp_Path + &quot;_Projetos/Administracao/ImpressaoTai.aspx?ModeloTai=tai001&amp;CP=&quot; + codigoProjeto;
    window.top.showModal(url, &quot;Impressão TAI&quot;, 1100, screen.height - 250, &quot;&quot;, null);
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar0" runat="server" 
                                    Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvar0Resource1" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);

}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                         Width="100%" OnCustomJSProperties="pageControl_CustomJSProperties"
                        meta:resourcekey="pageControlResource1">
                        <TabPages>
                            <dxtc:TabPage Name="tabDescricaoProjeto" Text="Descrição do Projeto">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlDescricaoProjeto" runat="server" Width="100%" meta:resourcekey="pnlDescricaoProjetoResource1">
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
                                                                                                Text="Nome do Projeto" meta:resourcekey="ASPxLabel1Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjudaResource1"
                                                                                                ToolTip="Informe o nome do projeto">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="style5">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                                                                                Text="Ano do Orçamento" meta:resourcekey="ASPxLabel1Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjudaResource1"
                                                                                                ToolTip="Informe o ano a ser utilizado para seleção de contas no Cronograma Orçamentário">
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
                                                                            <td style="padding-right: 10px" class="style5">
                                                                                <dxe:ASPxComboBox ID="cmbAnoOrcamento" runat="server" ClientInstanceName="cmbAnoOrcamento"
                                                                                    EnableCallbackMode="True" TextField="DescricaoMovimentoOrcamento" ValueField="Ano"
                                                                                    ValueType="System.Int32" Width="100%" >
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Código" FieldName="CodigoMovimentoOrcamento" Visible="False" />
                                                                                        <dxe:ListBoxColumn Caption="Descrição" FieldName="DescricaoMovimentoOrcamento" />
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
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="True"
                                                                                                Text="Tipo de Projeto" meta:resourcekey="ASPxLabel7Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage6" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecione o Tipo de Projeto"
                                                                                                Width="18px" meta:resourcekey="ASPxImage6Resource1">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="True"
                                                                                                Text="Fonte de Recurso" meta:resourcekey="ASPxLabel8Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage7" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecione a Fonte de recurso"
                                                                                                Width="18px" meta:resourcekey="ASPxImage7Resource1">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True"
                                                                                                Text="Data Início" meta:resourcekey="ASPxLabel10Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage9" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage9Resource1"
                                                                                                ToolTip="Informe data de início">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Font-Bold="True"
                                                                                                Text="Data Término" meta:resourcekey="ASPxLabel13Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage12" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage12Resource1"
                                                                                                ToolTip="Informe data de término">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid #000000;">
                                                                                <dxe:ASPxRadioButtonList ID="rblTipoProjeto" runat="server" ClientInstanceName="rblTipoProjeto"
                                                                                     RepeatColumns="2" Width="100%" SelectedIndex="0"
                                                                                    meta:resourcekey="rblTipoProjetoResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Finalísticos" Value="FI" Selected="True" meta:resourcekey="ListEditItemResource1" />
                                                                                        <dxe:ListEditItem Text="Administração e Apoio" Value="AA" meta:resourcekey="ListEditItemResource2" />
                                                                                    </Items>
                                                                                    <Border BorderStyle="None" />
                                                                                    <Border BorderStyle="None"></Border>
                                                                                </dxe:ASPxRadioButtonList>
                                                                            </td>
                                                                            <td style="border: 1px solid #000000;">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxCheckBox ID="cbFundecoop" runat="server" 
                                                                                                Text="FUNDECOOP" ClientInstanceName="cbFundecoop" meta:resourcekey="cbFundecoopResource1">
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                            </dxe:ASPxCheckBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxCheckBox ID="cbRecursoProprio" runat="server" 
                                                                                                Text="Recurso Próprio" ClientInstanceName="cbRecursoProprio" meta:resourcekey="cbRecursoProprioResource1">
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                            </dxe:ASPxCheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxDateEdit ID="deDataInicio" runat="server" ClientInstanceName="deDataInicio"
                                                                                     Width="100%" PopupVerticalAlign="WindowCenter"
                                                                                    meta:resourcekey="deDataInicioResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxDateEdit ID="deDataTermino" runat="server" ClientInstanceName="deDataTermino"
                                                                                     Width="100%" PopupVerticalAlign="WindowCenter"
                                                                                    meta:resourcekey="deDataTerminoResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                </dxe:ASPxDateEdit>
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
                                                                                                Text="Coordenador do Projeto" meta:resourcekey="ASPxLabel23Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Nome do Coordenador" Width="18px"
                                                                                                meta:resourcekey="ASPxImage10Resource1">
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
                                                                                                Text="Área Responsável" meta:resourcekey="ASPxLabel24Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Área responsável" Width="18px"
                                                                                                meta:resourcekey="ASPxImage11Resource1">
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
                                                                                                Text="Coordenador do Projeto" meta:resourcekey="ASPxLabel2Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecione coordenador do projeto"
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
                                                                                                Text="Área Responsável" meta:resourcekey="ASPxLabel3Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecione Área Responsável"
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
                                                                                    DataSourceID="sdsUnidadeGestora"  ValueType="System.Int32"
                                                                                    Width="100%" IncrementalFilteringMode="Contains" TextField="NomeUnidadeNegocio"
                                                                                    TextFormatString="{0}; {1}" ValueField="CodigoUnidadeNegocio" meta:resourcekey="cmbUnidadeGestoraResource1">
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
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel33" runat="server" Font-Bold="True"
                                                                                                Text="Público-Alvo" meta:resourcekey="ASPxLabel33Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-right: 5px; padding-left: 5px">
                                                                                            <dxe:ASPxLabel ID="lblCountPublicoAlvo" runat="server" ClientInstanceName="lblCountPublicoAlvo"
                                                                                                 ForeColor="Silver" Text="0 de 0" meta:resourcekey="lblCountPublicoAlvoResource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage20" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage20Resource1"
                                                                                                ToolTip="Informe o público alvo">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="margin-left: 120px">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel34" runat="server" Font-Bold="True"
                                                                                                Text="Beneficiários" meta:resourcekey="ASPxLabel34Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-right: 5px; padding-left: 5px">
                                                                                            <dxe:ASPxLabel ID="lblCountBeneficiarios" runat="server" ClientInstanceName="lblCountBeneficiarios"
                                                                                                 ForeColor="Silver" Text="0 de 0" meta:resourcekey="lblCountBeneficiariosResource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage21" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage21Resource1"
                                                                                                ToolTip="Informe os beneficiários">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxMemo ID="txtPublicoAlvo" runat="server" ClientInstanceName="txtPublicoAlvo"
                                                                                     Rows="5" Width="100%" meta:resourcekey="txtPublicoAlvoResource1">
                                                                                    <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                </dxe:ASPxMemo>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxMemo ID="txtBeneficiarios" runat="server" ClientInstanceName="txtBeneficiarios"
                                                                                     Rows="5" Width="100%" meta:resourcekey="txtBeneficiariosResource1">
                                                                                    <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountBeneficiarios;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountBeneficiarios;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
                                                                                </dxe:ASPxMemo>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Font-Bold="True"
                                                                                    Text="Equipe de Apoio" meta:resourcekey="ASPxLabel36Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage22" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Informe Equipe de Apoio"
                                                                                    Width="18px" meta:resourcekey="ASPxImage22Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvEquipeApoio" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvEquipeApoio" DataSourceID="sdsEquipeApoio"
                                                                        KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" Width="100%"
                                                                        meta:resourcekey="gvEquipeApoioResource1">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvEquipeApoio", "Equipe de Apoio")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Colaborador" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="NomeColaborador" meta:resourcekey="GridViewDataTextColumnResource1">
                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Informe um valor válido para o campo." IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                                <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="2" meta:resourcekey="GridViewDataTextColumnResource2">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />
                                                                        </SettingsPopup>
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True"
                                                                                    Text="Projeto:" meta:resourcekey="ASPxLabel11Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda0" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjuda0Resource1"
                                                                                    ToolTip="Informe se o projeto é novo ou se é uma continuação de outro projeto">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxRadioButtonList ID="rblSituacaoProjeto" runat="server" ClientInstanceName="rblSituacaoProjeto"
                                                                                     ItemSpacing="25px" RepeatColumns="2" SelectedIndex="0"
                                                                                    meta:resourcekey="rblSituacaoProjetoResource1">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Novo" Value="NO" Selected="True" meta:resourcekey="ListEditItemResource3" />
                                                                                        <dxe:ListEditItem Text="Continuidade" Value="CO" meta:resourcekey="ListEditItemResource4" />
                                                                                    </Items>
                                                                                </dxe:ASPxRadioButtonList>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel35" runat="server" Font-Bold="True"
                                                                                    Text="Em caso de continuidade, descreva os últimos resultados"
                                                                                    meta:resourcekey="ASPxLabel35Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountResultadosContinuidade" runat="server" ClientInstanceName="lblCountResultadosContinuidade"
                                                                                     ForeColor="Silver" Text="0 de 0" meta:resourcekey="lblCountResultadosContinuidadeResource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda12" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descreva sucintamente - tópicos - os principais resultados do exercício anterior - positivos e/ou negativos."
                                                                                    Width="18px" meta:resourcekey="imgAjuda12Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtResultadosContinuidade" runat="server" ClientInstanceName="txtResultadosContinuidade"
                                                                         Rows="5" Width="100%" meta:resourcekey="txtResultadosContinuidadeResource1">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountResultadosContinuidade;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountResultadosContinuidade;
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="True"
                                                                                    Text="Justificativa" meta:resourcekey="ASPxLabel12Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountJustificativa" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountJustificativa" meta:resourcekey="lblCountJustificativaResource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Elementos que explicam a apresentação do projeto; a importância e a necessidade de sua execução."
                                                                                    Width="18px" meta:resourcekey="imgAjuda1Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtJustificativa" runat="server" ClientInstanceName="txtJustificativa"
                                                                         Rows="20" Width="100%" meta:resourcekey="txtJustificativaResource1">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + &#39; de &#39; + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}"></ClientSideEvents>
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
                            <dxtc:TabPage Name="tabAlinhamentoEstrategico" Text="Alinhamento Estratégico">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlAlinhamentoEstrategico" runat="server" Width="100%" meta:resourcekey="pnlAlinhamentoEstrategicoResource1">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" meta:resourcekey="PanelContentResource2">
                                                    <div id="dv02" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True"
                                                                                    Text="Objetivos Estratégicos / Indicadores" meta:resourcekey="ASPxLabel6Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage5" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Informe objetivos estratégicos e seus indicadores"
                                                                                    Width="18px" meta:resourcekey="ASPxImage5Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvEstrategico" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvEstrategico" DataSourceID="sdsEstrategico"
                                                                        Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaObjetivo" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated"
                                                                        OnRowInserting="gvEstrategico_RowInserting" OnRowUpdating="gvEstrategico_RowUpdating"
                                                                        OnCellEditorInitialize="gvEstrategico_CellEditorInitialize" meta:resourcekey="gvEstrategicoResource1">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvEstrategico", "Plano Estratégico")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Objetivo Estratégico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="DescricaoObjetoEstrategia">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Indicador Estratégico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" FieldName="Meta">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="3" FieldName="CodigoObjetoEstrategia">
                                                                                <PropertiesComboBox DataSourceID="sdsObjetivoEstrategico" TextField="Descricao" ValueField="Codigo"
                                                                                    ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Descricao" Width="100%" Caption="Descrição" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbMetaRelacionada.PerformCallback(&quot;&quot;);
}" />
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                        <RequiredField ErrorText="Informe um valor válido para o campo" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador Estratégico" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4" FieldName="CodigoIndicador">
                                                                                <PropertiesComboBox DataSourceID="sdsIndicadorEstrategico" TextField="Meta" ValueField="CodigoIndicador"
                                                                                    ValueType="System.Int32" ClientInstanceName="cmbMetaRelacionada">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Meta" Width="100%" Caption="Indicador" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetSelectedIndex(-1);
}" />
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo" ShowInCustomizationForm="True" Visible="False"
                                                                                VisibleIndex="5" FieldName="SequenciaObjetivo">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />
                                                                        </SettingsPopup>
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True"
                                                                                    Text="Linhas de Ação" meta:resourcekey="ASPxLabel9Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Informe Linhas de Ação"
                                                                                    Width="18px" meta:resourcekey="ASPxImage8Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvLinhaAcao" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvLinhaAcao"
                                                                        DataSourceID="sdsLinhaAcao"  Width="100%"
                                                                        OnCommandButtonInitialize="grid_CommandButtonInitialize" KeyFieldName="SequenciaRegistro"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" OnRowInserting="gvLinhaAcao_RowInserting"
                                                                        OnRowUpdating="gvLinhaAcao_RowUpdating" OnCellEditorInitialize="gvLinhaAcao_CellEditorInitialize"
                                                                        PreviewFieldName="SequenciaRegistro" meta:resourcekey="gvLinhaAcaoResource1">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	gvEstrategico.Refresh();
}" />
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	gvEstrategico.Refresh();
}"></ClientSideEvents>
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvLinhaAcao", "Linha de Ação")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="CodigoObjetoEstrategia">
                                                                                <PropertiesComboBox DataSourceID="sdsObjetivoEstrategico_LinhaAtuacao" TextField="Descricao"
                                                                                    ValueField="Codigo" ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Descricao" Width="100%" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbLinhaAcao.PerformCallback(&quot;&quot;);
}" />
                                                                                    <ValidationSettings>
                                                                                        <RequiredField IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Linha de Ação" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" FieldName="DescricaoAcao">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Linha de Ação" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="2" FieldName="CodigoAcaoSugerida">
                                                                                <PropertiesComboBox DataSourceID="sdsOpcoesLinhaAtuacao" ValueType="System.Int32"
                                                                                    TextField="DescricaoAcao" ValueField="CodigoAcaoSugerida" ClientInstanceName="cmbLinhaAcao">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="DescricaoAcao" Width="100%" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetSelectedIndex(-1);
}" />
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Informe um valor válido para o campo" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo" ShowInCustomizationForm="True" Visible="False"
                                                                                VisibleIndex="3" FieldName="SequenciaRegistro">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="SequenciaObjetivo" FieldName="SequenciaObjetivo"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />
                                                                        </SettingsPopup>
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="60px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Font-Bold="True"
                                                                                    Text="Área de Atuação" meta:resourcekey="ASPxLabel37Resource1">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px" width="18px">
                                                                                <dxe:ASPxImage ID="ASPxImage23" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Áreas de atuação do Sescoop. Indicar apenas um."
                                                                                    Width="18px" meta:resourcekey="ASPxImage23Resource1">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxRadioButtonList ID="rblAreaAtuacao" runat="server" ClientInstanceName="rblAreaAtuacao"
                                                                                     RepeatColumns="2" Width="100%" SelectedIndex="0"
                                                                                    meta:resourcekey="rblAreaAtuacaoResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}"></ClientSideEvents>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Formação e Capacitação Profissional" Value="FO" Selected="True"
                                                                                            meta:resourcekey="ListEditItemResource5" />
                                                                                        <dxe:ListEditItem Text="Monitoramento e Desenvolvimento de Cooperativas" Value="MO"
                                                                                            meta:resourcekey="ListEditItemResource6" />
                                                                                        <dxe:ListEditItem Text="Gestão Interna do Sistema" Value="GE" meta:resourcekey="ListEditItemResource7" />
                                                                                        <dxe:ListEditItem Text="Promoção Social" Value="PR" meta:resourcekey="ListEditItemResource8" />
                                                                                    </Items>
                                                                                </dxe:ASPxRadioButtonList>
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
                            <dxtc:TabPage Name="tabQuadroLogico" Text="Quadro Lógico">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlQuadroLogico" runat="server" Width="100%" meta:resourcekey="pnlQuadroLogicoResource1">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" meta:resourcekey="PanelContentResource3">
                                                    <div id="dv03" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True"
                                                                                    Text="Objetivo Geral do Projeto">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountObjetivoGeral" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountObjetivoGeral">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Informe Objetivo Geral do projeto"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="1">
                                                                    <dxe:ASPxMemo ID="txtObjetivoGeral" runat="server" ClientInstanceName="txtObjetivoGeral"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Font-Bold="True"
                                                                                    Text="Indicador de Desempenho">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Indicadores de desempenho do projeto">
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
                                                                        Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaRegistro" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated"
                                                                        OnRowInserting="gvResultados_RowInserting" OnRowUpdating="gvResultados_RowUpdating"
                                                                        OnCustomJSProperties="grid_CustomJSProperties">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="70px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvResultados", "Indicadores de Desempenho")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Indicador de Desempenho" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="SetencaResultado">
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador" FieldName="CodigoIndicador"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                <PropertiesComboBox DataSourceID="sdsIndicadorDesempenho" IncrementalFilteringMode="Contains"
                                                                                    TextField="NomeIndicador" ValueField="CodigoIndicador" ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Indicador" FieldName="NomeIndicador" Width="100%" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" VisibleIndex="4" CaptionLocation="Top" ColumnSpan="3" />
                                                                                <EditFormSettings ColumnSpan="3" Visible="True" VisibleIndex="4" CaptionLocation="Top">
                                                                                </EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome Indicador" FieldName="NomeIndicador"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequência" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="7">
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top" />
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />
                                                                        </SettingsPopup>
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight: 700">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Font-Bold="True"
                                                                                    Text="Resultados Esperados">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Informe os resultados esperados.">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvResultadosEsperados" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvResultadosEsperados" DataSourceID="sdsResultadosEsperados"
                                                                         Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaRegistro" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated"
                                                                        OnCustomJSProperties="grid_CustomJSProperties" OnRowInserting="gvResultadosEsperados_RowInserting"
                                                                        OnRowUpdating="gvResultadosEsperados_RowUpdating" OnHtmlRowCreated="gvResultadosEsperados_HtmlRowCreated">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set(&quot;PodeSalvarResultados&quot;, s.cp_PodeSalvar);
}"></ClientSideEvents>
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="70px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvResultadosEsperados", "Resultados Esperados")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do Resultado" FieldName="DescricaoResultado"
                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                <PropertiesTextEdit MaxLength="500">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                                                                                <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador Operacional" FieldName="CodigoIndicador"
                                                                                ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                                                                <PropertiesComboBox MaxLength="500" DataSourceID="sdsIndicadorOperacional" ValueType="System.Int32"
                                                                                    TextField="NomeIndicador" ValueField="CodigoIndicador" IncrementalFilteringMode="Contains">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                                                                                <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoIndicadorOperacional" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3" Caption="Descrição Indicador Operacional">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4">
                                                                                <EditFormSettings Visible="False" />
                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm"/>
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
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabPlanoTrabalho" Text="Plano de Trabalho">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlPlanoTrabalho" runat="server" Width="100%" meta:resourcekey="pnlPlanoTrabalhoResource1">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent1" runat="server" meta:resourcekey="PanelContentResource4">
                                                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                                                         KeyFieldName="Codigo" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                                        OnHtmlRowPrepared="gvDados_HtmlRowPrepared" Width="100%">
                                                        <ClientSideEvents EndCallback="function(s, e) {
	atualizaTab = 'S';
    hfGeral.Set(&quot;PodeSalvarAtividade&quot;, s.cp_PodeSalvar);
    hfGeral.Set(&quot;PodeSalvarMetas&quot;, s.cp_PodeSalvarMetas);	
	hfGeral.Set(&quot;PodeSalvarAcao&quot;, s.cp_PodeSalvarAcao);
	callbackCronogramaOrcamentario.PerformCallback();
	try
			{
                
				//document.getElementById('frmCronograma').src = document.getElementById('frmCronograma').src;
			}
			catch(e){}
}" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataTextColumn FixedStyle="Left" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                Width="90px">
                                                                <DataItemTemplate>
                                                                    <%# getBotoesLinha() %>
                                                                </DataItemTemplate>
                                                                <GroupFooterCellStyle Wrap="True">
                                                                </GroupFooterCellStyle>
                                                                <HeaderTemplate>
                                                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluirAcao) ? string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Ação"" onclick=""incluiAcao('{0}')"" style=""cursor: pointer;""/>", faseTAI) : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Clique em 'Salvar' para poder registrar as informações de [Plano de Trabalho]"" style=""cursor: default;""/>")%>
                                                                </HeaderTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Nº" FieldName="Numero" FixedStyle="Left" Name="Numero"
                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="45px">
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Ações / Atividades" FieldName="Descricao"
                                                                FixedStyle="Left" Name="Descricao" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                Width="280px">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Início" FieldName="Inicio" Name="Inicio" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="90px">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Término" FieldName="Termino" Name="Termino"
                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="90px">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Evento Institucional" FieldName="Institucional"
                                                                Name="Institucional" ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="Responsavel" Name="Responsavel"
                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                                                                <PropertiesTextEdit>
                                                                    <Style Wrap="False">
                                                                        
                                                                    </Style>
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                                <HeaderStyle Wrap="True" />
                                                                <GroupFooterCellStyle Wrap="True">
                                                                </GroupFooterCellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Meta da Ação/Produto" FieldName="Codigo" Name="Metas"
                                                                ShowInCustomizationForm="True" VisibleIndex="7" Width="250px">
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption=" - " FieldName="Codigo" Name="Parcerias" ShowInCustomizationForm="True"
                                                                VisibleIndex="8" Width="400px">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Marcos" FieldName="Codigo" Name="Marcos" ShowInCustomizationForm="True"
                                                                VisibleIndex="9" Width="280px">
                                                                <Settings AllowAutoFilter="False" />
                                                                <Settings AllowAutoFilter="False"></Settings>
                                                                <HeaderStyle>
                                                                    <Paddings Padding="0px" />
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </HeaderStyle>
                                                                <CellStyle HorizontalAlign="Left">
                                                                    <Paddings Padding="0px" />
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="False" />
                                                        <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <Settings ShowHeaderFilterBlankItems="False" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                                                        <SettingsText />
                                                        <Settings ShowHeaderFilterBlankItems="False" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible">
                                                        </Settings>
                                                        <Styles>
                                                            <Header>
                                                                <Border BorderColor="Gray" />
                                                                <Border BorderColor="Gray"></Border>
                                                            </Header>
                                                            <Cell>
                                                                <Border BorderColor="Gray" />
                                                                <Border BorderColor="Gray"></Border>
                                                            </Cell>
                                                        </Styles>
                                                    </dxwgv:ASPxGridView>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabCronograma" Text="Cronograma Orçamentário">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	if(e.tab.index == 4 &amp;&amp; atualizaTab == 'S')
	{
		document.getElementById('frmCronograma').src = document.getElementById('frmCronograma').src;
		atualizaTab = 'N';
	}
}" />
                    </dxtc:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 20px; padding-right: 10px;">
                    <table>
                        <tr>
                            <td  width="100px">
                                <dxe:ASPxButton ID="btnImprimir0" runat="server" 
                                    Text="Imprimir" AutoPostBack="False" meta:resourcekey="btnSalvar0Resource1" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
    //codigo da tela de proposta de iniciativa 4    
    var codigoProjeto = hfGeral.Get(&quot;CodigoProjeto&quot;);
    var url = window.top.pcModal.cp_Path + &quot;_Projetos/Administracao/ImpressaoTai.aspx?ModeloTai=tai001&amp;CP=&quot; + codigoProjeto;
    window.top.showModal(url, &quot;Impressão TAI&quot;, 1100, screen.height - 250, &quot;&quot;, null);
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                    Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvarResource1"
                                    Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <dxpc:ASPxPopupControl ID="pcIniciarFluxo" runat="server" ClientInstanceName="pcIniciarFluxo"
        CloseAction="None" EncodeHtml="False"  HeaderText="&amp;nbsp;"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="411px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="Tabela">
                    <tr>
                        <td align="center">
                            <dxe:ASPxLabel ID="ASPxLabel38" runat="server" 
                                Text="Deseja bloquear este projeto e iniciar o processo de reformulação do mesmo ?">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btnSim" runat="server" AutoPostBack="False"
                                            Text="Sim" Width="80px">
                                            <ClientSideEvents Click="function(s, e) {
	callbackIniciarFluxo.PerformCallback();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="padding-left: 5px">
                                        <dxe:ASPxButton ID="btnNao" runat="server" AutoPostBack="False"
                                            Text="Não" Width="80px">
                                            <ClientSideEvents Click="function(s, e) {
	window.open(s.cp_URLDirecionamento, '_self');
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT * FROM [TermoAbertura02] WHERE [CodigoProjeto] = @CodigoProjeto"
        OnSelected="sdsDadosFomulario_Selected">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsLider" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsUnidadeGestora" runat="server" SelectCommand=" SELECT CodigoUnidadeNegocio,
        NomeUnidadeNegocio,
        SiglaUnidadeNegocio
   FROM UnidadeNegocio
  WHERE DataExclusao IS NULL 
    AND IndicaUnidadeNegocioAtiva = 'S' 
    AND CodigoEntidade = @CodigoEntidade
  ORDER BY 
        SiglaUnidadeNegocio">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsEquipeApoio" runat="server" DeleteCommand="DELETE FROM  [tai02_EquipeApoio]
             WHERE [CodigoProjeto] = @CodigoProjeto
                  AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO [tai02_EquipeApoio]
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeColaborador])
     VALUES
           (@CodigoProjeto
           ,(SELECT  ISNULL(MAX(SequenciaRegistro), 0) + 1 FROM [tai02_EquipeApoio] WHERE CodigoProjeto = @CodigoProjeto)
           ,@NomeColaborador)" SelectCommand="SELECT SequenciaRegistro, NomeColaborador
  FROM [tai02_EquipeApoio] 
WHERE CodigoProjeto = @CodigoProjeto
ORDER BY NomeColaborador" UpdateCommand="UPDATE [tai02_EquipeApoio]
   SET [NomeColaborador] = @NomeColaborador
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeColaborador" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="NomeColaborador" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsEstrategico" runat="server" SelectCommand="SELECT [SequenciaObjetivo]
      ,[CodigoObjetoEstrategia]
      ,[DescricaoObjetoEstrategia]
      ,[CodigoIndicador]
      ,[Meta]
  FROM [tai02_ObjetivosEstrategicos]
  WHERE  [CodigoProjeto] = @CodigoProjeto
   ORDER BY SequenciaObjetivo" InsertCommand="INSERT INTO tai02_ObjetivosEstrategicos
           ([CodigoProjeto]
      ,[SequenciaObjetivo]
      ,[CodigoObjetoEstrategia]
      ,[DescricaoObjetoEstrategia]
      ,[CodigoIndicador]
      ,[Meta])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaObjetivo),0) + 1 FROM tai02_ObjetivosEstrategicos WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoObjetoEstrategia
           ,@DescricaoObjetoEstrategia
           ,@CodigoIndicador
           ,@Meta)" UpdateCommand="UPDATE [tai02_ObjetivosEstrategicos]
   SET [CodigoObjetoEstrategia] = @CodigoObjetoEstrategia
      ,[DescricaoObjetoEstrategia] = @DescricaoObjetoEstrategia
      ,[CodigoIndicador] = @CodigoIndicador
      ,[Meta] = @Meta
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaObjetivo = @SequenciaObjetivo
" DeleteCommand="DELETE FROM tai02_ObjetivosEstrategicos
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaObjetivo = @SequenciaObjetivo">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaObjetivo" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoObjetoEstrategia" />
            <asp:Parameter Name="DescricaoObjetoEstrategia" />
            <asp:Parameter Name="CodigoIndicador" />
            <asp:Parameter Name="Meta" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaObjetivo" />
            <asp:Parameter Name="CodigoObjetoEstrategia" />
            <asp:Parameter Name="DescricaoObjetoEstrategia" />
            <asp:Parameter Name="CodigoIndicador" />
            <asp:Parameter Name="Meta" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsObjetivoEstrategico" runat="server" SelectCommand=" SELECT Convert(INT,oe.CodigoobjetoEstrategia) AS Codigo, 
        oe.DescricaoObjetoEstrategia AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior,
        oes.TituloObjetoEstrategia DescricaoSuperior
   FROM ObjetoEstrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio =me.CodigoUnidadeNegocio 
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL
    --AND NOT EXISTS (SELECT 1 FROM tai02_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoObjetoEstrategia = oe.CodigoobjetoEstrategia )
  ORDER BY 2">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsIndicadorOperacional" runat="server" SelectCommand=" select x.NomeIndicador, 
        x.CodigoIndicador from (
  SELECT 'Não se aplica' NomeIndicador, 
        -1 CodigoIndicador
  union

 SELECT indO.NomeIndicador, 
        indO.CodigoIndicador 
   FROM IndicadorOperacional indO
  WHERE indO.CodigoEntidade = @CodigoEntidade
    AND indO.DataExclusao IS NULL
    AND indO.TipoIndicador = 'O' 
) x 
  ORDER BY x.NomeIndicador">
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
    AND NOT EXISTS (SELECT 1 FROM tai02_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoIndicador = i.CodigoIndicador AND t.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico)
  ORDER BY 2" DeleteCommand="DELETE FROM tai_DesafiosIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" OnFiltering="sqlDataSource_Filtering"
        FilterExpression="CodigoSuperior = {0}" OnSelecting="sdsIndicadorEstrategico_Selecting">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsLinhaAcao" runat="server" DeleteCommand=" DELETE FROM tai02_LinhaDeAcao
  WHERE [CodigoProjeto] = @CodigoProjeto
    AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO [tai02_LinhaDeAcao]
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[CodigoAcaoSugerida]
           ,[DescricaoAcao]
           ,[SequenciaObjetivo])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM [tai02_LinhaDeAcao] WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoAcaoSugerida
           ,@DescricaoAcao
           ,(SELECT TOP 1 SequenciaObjetivo FROM tai02_ObjetivosEstrategicos WHERE CodigoProjeto = @CodigoProjeto AND CodigoObjetoEstrategia = @CodigoObjetoEstrategia))"
        SelectCommand="SELECT l.[SequenciaRegistro]
      ,l.[CodigoAcaoSugerida]
      ,l.[DescricaoAcao]
      ,l.[SequenciaObjetivo]
      ,o.CodigoObjetoEstrategia
  FROM [tai02_LinhaDeAcao] l INNER JOIN
       [tai02_ObjetivosEstrategicos] o ON o.CodigoProjeto = l.CodigoProjeto AND
                                          o.SequenciaObjetivo = l.SequenciaObjetivo
  WHERE l.[CodigoProjeto] = @CodigoProjeto
   ORDER BY  l.[SequenciaRegistro]" UpdateCommand="UPDATE [tai02_LinhaDeAcao]
   SET [CodigoAcaoSugerida] = @CodigoAcaoSugerida
      ,[DescricaoAcao] = @DescricaoAcao
      ,[SequenciaObjetivo] = @SequenciaObjetivo
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro
">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoAcaoSugerida" />
            <asp:Parameter Name="DescricaoAcao" />
            <asp:Parameter Name="CodigoObjetoEstrategia" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoAcaoSugerida" />
            <asp:Parameter Name="DescricaoAcao" />
            <asp:Parameter Name="SequenciaObjetivo" />
            <asp:Parameter Name="SequenciaRegistro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsObjetivoEstrategico_LinhaAtuacao" runat="server" SelectCommand=" SELECT DISTINCT
        oe.CodigoobjetoEstrategia AS Codigo, 
        oe.DescricaoObjetoEstrategia AS Descricao
   FROM ObjetoEstrategia oe INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio INNER JOIN 
        tai02_ObjetivosEstrategicos t ON t.CodigoObjetoEstrategia = oe.CodigoobjetoEstrategia             
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL
    AND t.CodigoProjeto = @CodigoProjeto
  ORDER BY 2">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsOpcoesLinhaAtuacao" runat="server" FilterExpression="CodigoSuperior = {0}"
        SelectCommand=" SELECT a.[CodigoAcaoSugerida], 
        a.[DescricaoAcao],
        a.[CodigoObjetoAssociado] AS CodigoSuperior
   FROM [AcoesSugeridas]       AS [a]
  WHERE a.[CodigoTipoAssociacao] = dbo.f_GetCodigoTipoAssociacao('OB')
    AND a.[DataDesativacao]                  IS NULL
    AND a.CodigoEntidade = @CodigoEntidade
    AND NOT EXISTS(SELECT 1 FROM tai02_LinhaDeAcao l WHERE l.CodigoAcaoSugerida = a.CodigoAcaoSugerida AND l.CodigoProjeto = @CodigoProjeto)
  ORDER BY
        a.[DescricaoAcao]" OnFiltering="sqlDataSource_Filtering" OnSelecting="sdsOpcoesLinhaAtuacao_Selecting">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsResultados" runat="server" SelectCommand="SELECT * FROM dbo.tai02_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY SetencaResultado"
        DeleteCommand="DELETE FROM tai02_ResultadosIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai02_ResultadosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[CodigoIndicador]
           ,[NomeIndicador]
           ,[SetencaResultado])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai02_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoIndicador
           ,@NomeIndicador
           ,@SetencaResultado)" UpdateCommand="UPDATE tai02_ResultadosIniciativa
   SET [CodigoIndicador] = @CodigoIndicador
      ,[NomeIndicador] = @NomeIndicador
      ,[SetencaResultado] = @SetencaResultado
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoIndicador" />
            <asp:Parameter Name="NomeIndicador" />
            <asp:Parameter Name="SetencaResultado" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="CodigoIndicador" />
            <asp:Parameter Name="NomeIndicador" />
            <asp:Parameter Name="SetencaResultado" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsIndicadorDesempenho" runat="server" SelectCommand=" SELECT indO.NomeIndicador, 
        indO.CodigoIndicador 
   FROM IndicadorOperacional indO
  WHERE indO.CodigoEntidade = @CodigoEntidade
    AND indO.DataExclusao IS NULL
    AND indO.TipoIndicador = 'D' 
  ORDER BY indO.NomeIndicador">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsResultadosEsperados" runat="server" DeleteCommand="DELETE FROM tai02_ResultadosEsperados
 WHERE [CodigoProjeto] = @CodigoProjeto
      AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai02_ResultadosEsperados
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[DescricaoResultado]
           , [CodigoIndicador]
           ,[DescricaoIndicadorOperacional])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai02_ResultadosEsperados WHERE CodigoProjeto = @CodigoProjeto )
           ,@DescricaoResultado
          , @CodigoIndicador
           ,@DescricaoIndicadorOperacional)" SelectCommand="SELECT * FROM tai02_ResultadosEsperados WHERE CodigoProjeto = @CodigoProjeto"
        UpdateCommand="UPDATE tai02_ResultadosEsperados
   SET [DescricaoResultado] = @DescricaoResultado
      ,[DescricaoIndicadorOperacional] = @DescricaoIndicadorOperacional
     ,[CodigoIndicador] = @CodigoIndicador
 WHERE [CodigoProjeto] = @CodigoProjeto
      AND [SequenciaRegistro] = @SequenciaRegistro
      ">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="DescricaoResultado" />
            <asp:Parameter Name="DescricaoIndicadorOperacional" />
            <asp:Parameter Name="CodigoIndicador" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="DescricaoResultado" />
            <asp:Parameter Name="DescricaoIndicadorOperacional" />
            <asp:Parameter Name="CodigoIndicador" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsMarcosCriticos" runat="server" SelectCommand="SELECT * FROM tai02_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeMarco"
        DeleteCommand="DELETE FROM tai02_MarcosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai02_MarcosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeMarco]
           ,[DataLimitePrevista]
           ,[CodigoAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai02_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeMarco
           ,@DataLimitePrevista
           ,@CodigoAcao)" UpdateCommand="UPDATE tai02_MarcosAcoesIniciativa
   SET [NomeMarco] = @NomeMarco
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[CodigoAcao] = @CodigoAcao
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeMarco" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="CodigoAcao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="NomeMarco" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="CodigoAcao" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsResponsaveisAcao" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsEntidadesAcao" runat="server" SelectCommand=" SELECT un.CodigoUnidadeNegocio AS CodigoEntidade, 
        un.NomeUnidadeNegocio AS NomeEntidade
   FROM UnidadeNegocio un
  WHERE un.DataExclusao IS NULL
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND un.SiglaUF = 'BR'
ORDER BY 2"></asp:SqlDataSource>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
    if (s.cp_gravouNovaIni == &quot;1&quot;)
	    gravaInstanciaWf();
	ProcessaResultadoCallback(s, e);
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackPlanoTrabalho" runat="server" ClientInstanceName="callbackPlanoTrabalho"
        OnCallback="callbackPlanoTrabalho_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_Msg != &quot;&quot;)
        {
                  window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
        }
        else if(s.cp_Erro != &quot;&quot;)
       {
                 window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
       }
       else if(s.cp_Sucesso != &quot;&quot;)
       {
                 if(s.cp_Status == '1')
                 {
                          window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                          gvDados.PerformCallback();
                 }
       }
}

" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackAtividade" runat="server" ClientInstanceName="callbackAtividade"
        OnCallback="callbackAtividade_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_Msg != &quot;&quot;)
        {
                  window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
        }
        else if(s.cp_Erro != &quot;&quot;)
       {
                 window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
       }
       else if(s.cp_Sucesso != &quot;&quot;)
       {
                 if(s.cp_Status == '1')
                 {
                          window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                          gvDados.PerformCallback();
                 }
       }    
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackConflitos" runat="server" OnCustomJSProperties="callbackConflitos_CustomJSProperties"
        ClientInstanceName="callbackConflitos" OnCallback="callbackConflitos_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
    if(e.parameter == &quot;N&quot;) return;

	if(s.cpPossuiConflitos)
		ExibeTelaConflitosAgenda();
	else if(s.cpExecutaVerificacao){
		callbackConflitos.cpExecutaVerificacao = false;
        var codigoAcao = hfGeral.Get(&quot;CodigoAcao&quot;);
        var nomeBotao = &quot;_ID&quot; + codigoAcao;
        var botao = window.parent.parent.eval(nomeBotao);
        botao.DoClick();
    }
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackCronogramaOrcamentario" runat="server" ClientInstanceName="callbackCronogramaOrcamentario">
        <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set('podeSalvarCronograma', s.cp_PodeSalvar);
	cmbAnoOrcamento.SetEnabled(s.cp_PodeEditarCronograma);
}" Init="function(s, e) {
	hfGeral.Set('podeSalvarCronograma', s.cp_PodeSalvar);
}"></ClientSideEvents>
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackIniciarFluxo" runat="server" ClientInstanceName="callbackIniciarFluxo"
        OnCallback="callbackIniciarFluxo_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	pcIniciarFluxo.Hide();
	gravaInstanciaWf();
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
