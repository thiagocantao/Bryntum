<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_006.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_006" meta:resourcekey="PageResource5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tabela
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var existeConteudoCampoAlterado = false;

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
            //var lider = cmbLider.GetValue() != null;
            var unidadeGestora = cmbUnidadeGestora.GetValue() != null;
            var dataInicio = deDataInicio.GetValue() != null;
            var dataTermino = deDataTermino.GetValue() != null;
            //var objetivoGeral = txtObjetivoGeral.GetValue() != null;
            var podeSalvar = hfGeral.Get("PodeSalvarResultados") + "" != "N";
            var podeSalvarAtividades = hfGeral.Get("PodeSalvarAtividade") + "" != "N";

            return nomeProjeto &&
                unidadeGestora &&
                dataInicio &&
                dataTermino &&
                podeSalvar &&
                podeSalvarAtividades;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeProjeto = txtNomeProjeto.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();

            if (!nomeProjeto || 0 === nomeProjeto.length)
                msg += "O campo 'Nome do Processo' deve ser informado.\n";
            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Área Responsável' deve ser informado.\n";
            if (!cbFundecoop.GetChecked() && !cbRecursoProprio.GetChecked())
                msg += "Deve ser informada ao menos uma opção para o campo 'Fonte de Recurso'.\n";
//            if (dataInicio.GetValue() == null) {
//                numAux++;
//                mensagem += "\n" + numAux + ") A data de início da atividade deve ser informada.";
//            }

//            if (dataTermino.GetValue() == null) {
//                numAux++;
//                mensagem += "\n" + numAux + ") A data de término da atividade deve ser informada.";
//            }

            if (deDataInicio.GetValue() > deDataTermino.GetValue()) {
                msg += "A data de início da atividade não pode ser maior que a data de término.";
            }
            return msg;
        }

        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
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
                var camposNaoPreenchidos = new Array();
                var cont = 0;

                if (!possuiNomeProjeto) {
                    camposNaoPreenchidos[cont] = "Nome do Projeto";
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
                    camposNaoPreenchidos[cont] = "Existem resultados esperados sem indicador relacionado"
                    cont++;
                }

                if (!podeSalvarAtividades) {
                    camposNaoPreenchidos[cont] = "Existem atividades sem produtos relacionados"
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
//            if (callbackConflitos.cpExecutaVerificacao && gvDados.cp_CodigoProjeto != -1) {
//                var acoes = hfGeral.Get("Acoes");
//                for (var i = 0; i < acoes.length; i++) {
//                    if (acoes[i] == codigoAcao) {
//                        callbackConflitos.PerformCallback("S");
//                        return false;
//                    }
//                }
//            }

//            if (callbackConflitos.cpMudaStatusProposta) {
//                callbackConflitos.PerformCallback("N");
//            }
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
                    window.location = "./propostaDeIniciativa_006.aspx?CP=" + codigoNovoProjeto + "&CWF=" + codigoWorkflow + "&CEWF=" + codigoEtapaWf + "&CIWF=" + codigoInstanciaWf + "&tab=" + activeTabIndex;
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

        function incluiAtividade(codigoAcao, numeroAcao) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAtividadesProcesso.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + "&NA=" + numeroAcao, 'Nova Atividade', 900, 500, funcaoPosModal, null);
        }

        function editaAtividade(codigoAtividade, codigoAcao) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAtividadesProcesso.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + '&CodigoAtividade=' + codigoAtividade, 'Edição da Atividade', 900, 500, funcaoPosModal, null);
        }

        function excluiAtividade(codigoAtividade) {
            if (confirm("ATENÇÃO!!!\n\nEste processo também vai excluir todos os Produtos e Parcerias relacionados com a atividade selecionada!\n\nConfirma a exclusão da atividade selecionada? \n"))
                callbackAtividade.PerformCallback(codigoAtividade);
        }

        function incluiAcao() {
            var codigoProjeto = gvDados.cp_CodigoProjeto;
            var sequencia = gvDados.cp_Sequencia;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAcoesProcesso.aspx?CA=-1&CP=' + codigoProjeto + '&SQ=' + sequencia, 'Nova Ação', 800, 300, funcaoPosModal, null);
        }

        function editaAcao(codigoAcao) {
            var codigoProjeto = gvDados.cp_CodigoProjeto;
            var sequencia = gvDados.cp_Sequencia;

            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroAcoesProcesso.aspx?CA=' + codigoAcao + '&CP=' + codigoProjeto + '&SQ=' + sequencia, 'Edição da Ação', 800, 300, funcaoPosModal, null);
        }

        function excluiAcao(codigoAcao) {
            if (confirm("Deseja excluir a ação selecionada?"))
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
            if (window.top.document.URL.indexOf("listaProcessos.aspx") > -1)
                url = '../_Projetos/Administracao/ConflitosAgenda.aspx?CE=' + codigoEntidade + '&CP=' + codigoProjeto + '&AL=' + altura;
            else
                url = './_Projetos/Administracao/ConflitosAgenda.aspx?CE=' + codigoEntidade + '&CP=' + codigoProjeto + '&AL=' + altura;
            window.top.showModal(url, "Conflitos de Agenda", 1020, altura, "", null);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
    <div style="overflow: auto">
        <table border="0" cellpadding="0" cellspacing="0" class="Tabela">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dxe:ASPxButton ID="btnSalvar0" runat="server" 
                        Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvar0Resource1">
                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                         Width="100%" OnCustomJSProperties="pageControl_CustomJSProperties"
                        meta:resourcekey="pageControlResource1">
                        <TabPages>
                            <dxtc:TabPage Name="tabDescricaoProjeto" Text="Descrição do Processo" 
                                meta:resourcekey="TabPageResource1">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True" meta:resourcekey="ContentControlResource1">
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
                                                                                                Text="Nome do Processo" 
                                                                                                meta:resourcekey="ASPxLabel1Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" 
                                                                                                meta:resourcekey="imgAjudaResource1" ToolTip="Informe o nome do processo">
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
                                                                                </dxe:ASPxTextBox>
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
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" 
                                                                                                meta:resourcekey="ASPxImage9Resource1" ToolTip="Informe data de início">
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
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" 
                                                                                                meta:resourcekey="ASPxImage12Resource1" ToolTip="Informe data de término">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid #000000;">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxCheckBox ID="cbFundecoop" runat="server" 
                                                                                                Text="FUNDECOOP" ClientInstanceName="cbFundecoop" meta:resourcekey="cbFundecoopResource1">
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                            </dxe:ASPxCheckBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxCheckBox ID="cbRecursoProprio" runat="server" 
                                                                                                Text="Recurso Próprio" ClientInstanceName="cbRecursoProprio" meta:resourcekey="cbRecursoProprioResource1">
                                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
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
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxDateEdit ID="deDataTermino" runat="server" ClientInstanceName="deDataTermino"
                                                                                     Width="100%" PopupVerticalAlign="WindowCenter"
                                                                                    meta:resourcekey="deDataTerminoResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
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
                                                                                                Text="Objetivo do Processo" 
                                                                                                meta:resourcekey="ASPxLabel23Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                                                                                ToolTip="Nome do Coordenador" Width="18px"
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
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Área responsável"
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
                                                                            <td width="50%" style="display: none;">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True"
                                                                                                Text="Objetivo do Processo" 
                                                                                                meta:resourcekey="ASPxLabel2Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                                                                                ToolTip="Selecione coordenador do projeto" Width="18px"
                                                                                                meta:resourcekey="ASPxImage1Resource1">
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
                                                                            <td style="padding-right: 10px; display: none;">
                                                                                <dxe:ASPxComboBox ID="cmbLider" runat="server" ClientInstanceName="cmbLider" DataSourceID="sdsLider"
                                                                                     ValueType="System.Int32" Width="100%" EnableCallbackMode="True"
                                                                                    IncrementalFilteringMode="Contains"  OnItemRequestedByValue="cmbLider_ItemRequestedByValue"
                                                                                    OnItemsRequestedByFilterCondition="cmbLider_ItemsRequestedByFilterCondition"
                                                                                    TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" meta:resourcekey="cmbLiderResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" meta:resourcekey="ListBoxColumnResource1" />
                                                                                        <dxe:ListBoxColumn Caption="E-Mail" FieldName="EMail" Width="300px" meta:resourcekey="ListBoxColumnResource2" />
                                                                                    </Columns>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxComboBox ID="cmbUnidadeGestora" runat="server" ClientInstanceName="cmbUnidadeGestora"
                                                                                    DataSourceID="sdsUnidadeGestora"  ValueType="System.Int32"
                                                                                    Width="100%" IncrementalFilteringMode="Contains" 
                                                                                    TextField="NomeUnidadeNegocio" TextFormatString="{1}" ValueField="CodigoUnidadeNegocio"
                                                                                    meta:resourcekey="cmbUnidadeGestoraResource1">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="140px"
                                                                                            meta:resourcekey="ListBoxColumnResource3" />
                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio" Width="300px" meta:resourcekey="ListBoxColumnResource4" />
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
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Elementos que explicam a apresentação do processo a importância e a necessidade de sua execução."
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
                                                                         Rows="20" Width="100%" 
                                                                        meta:resourcekey="txtJustificativaResource1" Height="150px">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
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
	
    hfGeral.Set(&quot;PodeSalvarAtividade&quot;, s.cp_PodeSalvar);	
	try
			{
				document.getElementById('frmCronograma').src = document.getElementById('frmCronograma').src;
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
                                                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluirAcao) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Ação"" onclick=""incluiAcao()"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Clique em 'Salvar' para poder registrar as informações de [Plano de Trabalho]"" style=""cursor: default;""/>")%>
                                                                </HeaderTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Nº" FieldName="Numero" FixedStyle="Left" Name="Numero"
                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="45px">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Ações / Atividades" FieldName="Descricao"
                                                                FixedStyle="Left" Name="Descricao" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                Width="280px">
                                                                <Settings AllowAutoFilter="False" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Início" FieldName="Inicio" Name="Inicio" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="90px">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="False" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Término" FieldName="Termino" Name="Termino"
                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="90px">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="False" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="Responsavel" Name="Responsavel"
                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                                                                <CellStyle HorizontalAlign="Left">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Produto / Resultado" FieldName="Codigo" Name="Metas"
                                                                ShowInCustomizationForm="True" VisibleIndex="7" Width="250px">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption=" - " FieldName="Codigo" Name="Parcerias" ShowInCustomizationForm="True"
                                                                VisibleIndex="8" Width="400px">
                                                                <Settings AllowAutoFilter="False" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="False" />
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <Settings ShowHeaderFilterBlankItems="False" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                                                        <SettingsText  />
                                                        <Styles>
                                                            <Header>
                                                                <Border BorderColor="Gray" />
                                                            </Header>
                                                            <Cell>
                                                                <Border BorderColor="Gray" />
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
                    </dxtc:ASPxPageControl>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 20px">
                    &nbsp;
                </td>
                <td align="right" style="padding-top: 20px; padding-right: 10px;">
                    <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                        Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvarResource1">
                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT * FROM [TermoAbertura06] WHERE [CodigoProjeto] = @CodigoProjeto"
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
    --AND NOT EXISTS (SELECT 1 FROM Tai06_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoObjetoEstrategia = oe.CodigoobjetoEstrategia )
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
    AND NOT EXISTS (SELECT 1 FROM Tai06_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoIndicador = i.CodigoIndicador AND t.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico)
  ORDER BY 2" DeleteCommand="DELETE FROM tai_DesafiosIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" OnFiltering="sqlDataSource_Filtering"
        FilterExpression="CodigoSuperior = {0}">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsObjetivoEstrategico_LinhaAtuacao" runat="server" SelectCommand=" SELECT DISTINCT
        oe.CodigoobjetoEstrategia AS Codigo, 
        oe.DescricaoObjetoEstrategia AS Descricao
   FROM ObjetoEstrategia oe INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio INNER JOIN 
        Tai06_ObjetivosEstrategicos t ON t.CodigoObjetoEstrategia = oe.CodigoobjetoEstrategia             
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
    AND NOT EXISTS(SELECT 1 FROM Tai06_LinhaDeAcao l WHERE l.CodigoAcaoSugerida = a.CodigoAcaoSugerida AND l.CodigoProjeto = @CodigoProjeto)
  ORDER BY
        a.[DescricaoAcao]" OnFiltering="sqlDataSource_Filtering">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
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
    <asp:SqlDataSource ID="sdsMarcosCriticos" runat="server" SelectCommand="SELECT * FROM Tai06_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeMarco"
        DeleteCommand="DELETE FROM Tai06_MarcosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO Tai06_MarcosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeMarco]
           ,[DataLimitePrevista]
           ,[CodigoAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM Tai06_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeMarco
           ,@DataLimitePrevista
           ,@CodigoAcao)" UpdateCommand="UPDATE Tai06_MarcosAcoesIniciativa
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
    if (s.cp_Msg != &quot;&quot;)
	{
	    if(s.cp_Status == '1')
            window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);

		if(s.cp_Status == '1')
			gvDados.PerformCallback();
	}
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackAtividade" runat="server" ClientInstanceName="callbackAtividade"
        OnCallback="callbackAtividade_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
    if (s.cp_Msg != &quot;&quot;)
	{
	    if(s.cp_Status == '1')
            window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);

		if(s.cp_Status == '1')
		{				
			gvDados.PerformCallback();
		}
	}
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackConflitos" runat="server" OnCustomJSProperties="callbackConflitos_CustomJSProperties"
        ClientInstanceName="callbackConflitos" OnCallback="callbackConflitos_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cpPossuiConflitos)
		ExibeTelaConflitosAgenda();
	else{
		callbackConflitos.cpExecutaVerificacao = false;
        var codigoAcao = hfGeral.Get(&quot;CodigoAcao&quot;);
        var nomeBotao = &quot;_ID&quot; + codigoAcao;
        window.parent.parent.document.getElementById(nomeBotao).click();
    }
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
