<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .Tabela
        {
            width: 100%;
        }
        .textoComIniciaisMaiuscula {
            text-transform:capitalize
        }
    </style>
    <script type="text/javascript" language="javascript">

        var existeConteudoCampoAlterado = false;

        function btnImprimir_Click(s, e) {
            var codigoProjeto = hfGeral.Get("CodigoProjeto");
            var url = window.top.pcModal.cp_Path + "_Projetos/Administracao/ImpressaoTai.aspx?ModeloTai=tai&CP=" + codigoProjeto;
            window.top.showModal(url, "Impressão TAI", 900, screen.height - 250, "", null);
        }

        function btnSalvar_Click(s, e) {
            var msg = ValidaCampos();
            if (msg == "") {
                callback.PerformCallback("");
                existeConteudoCampoAlterado = false;
            }
            else {
                window.top.mostraMensagem(msg, 'Atencao', true, false, null);
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

            var nomeIniciativa = txtNomeIniciativa.GetValue() != null;
            var lider = cmbLider.GetValue() != null;
            var unidadeGestora = cmbUnidadeGestora.GetValue() != null;
            var dataInicio = deDataInicio.GetValue() != null;
            var dataTermino = deDataTermino.GetValue() != null;
            var focoEstrategico = cmbFocoEstrategico.GetValue() != null;
            var possuiDirecionadores = cmbDirecionador.GetValue() != null;
            var tipoIniciativa = cmbTipoIniciativa.GetValue() != null;
            //            var publicoAlvo = txtPublicoAlvo.GetValue() != null;
            //            var justificativa = txtJustificativa.GetValue() != null;
            var objetivoGeral = txtObjetivoGeral.GetValue() != null;
            //            var escopoIniciativa = txtEscopoIniciativa.GetValue() != null;
            //            var limiteEscopo = txtLimitesEscopo.GetValue() != null;
            //            var premissas = txtPremissas.GetValue() != null;
            //            var restricoes = txtRestricoes.GetValue() != null;
            var possuiResultados = gvResultados.cpMyRowCount > 0;

            return nomeIniciativa &&
                lider &&
                unidadeGestora &&
                dataInicio &&
                dataTermino &&
                focoEstrategico &&
                //                publicoAlvo &&
                //                justificativa &&
                objetivoGeral &&
                //                escopoIniciativa &&
                //                limiteEscopo &&
                //                premissas &&
                //                restricoes &&
                possuiResultados &&
                possuiDirecionadores &&
                tipoIniciativa;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeIniciativa = txtNomeIniciativa.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();
            var codigoFocoEstrategico = cmbFocoEstrategico.GetValue();
            var codigoLider = cmbLider.GetValue();
            var tipoIniciativa = cmbTipoIniciativa.GetValue();

            if (!nomeIniciativa || 0 === nomeIniciativa.length)
                msg += "O campo 'Nome da Iniciativa' deve ser informado.\n";
            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Unidade Gestora' deve ser informado.\n";
            //            if (!codigoLider || codigoLider == null)
            //                msg += "O campo 'Lider' deve ser informado.\n";
            //            if (!codigoFocoEstrategico || codigoFocoEstrategico == null)
            //                msg += "O campo 'Foco Estrategico' deve ser informado.\n";
            if (tipoIniciativa == null) {
                msg += "O campo 'Tipo de Iniciativa' deve ser informado.\n";
            }
            return msg;
        }

        function verificaAvancoWorkflow() {
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'Atencao', true, false, null);
                return false;
            }

            if (!camposPreenchidos) {
                var possuiNomeIniciativa = txtNomeIniciativa.GetValue() != null;
                var possuiLider = cmbLider.GetValue() != null;
                var possuiUnidadeGestora = cmbUnidadeGestora.GetValue() != null;
                var possuiDataInicio = deDataInicio.GetValue() != null;
                var possuiDataTermino = deDataTermino.GetValue() != null;
                var possuiFocoEstrategico = cmbFocoEstrategico.GetValue() != null;
                var possuiDirecionadores = cmbDirecionador.GetValue() != null;
                //                var possuiPublicoAlvo = txtPublicoAlvo.GetValue() != null;
                //                var possuiJustificativa = txtJustificativa.GetValue() != null;
                var possuiObjetivoGeral = txtObjetivoGeral.GetValue() != null;
                //                var possuiEscopoIniciativa = txtEscopoIniciativa.GetValue() != null;
                //                var possuiLimiteEscopo = txtLimitesEscopo.GetValue() != null;
                //                var possuiPremissas = txtPremissas.GetValue() != null;
                //                var possuiRestricoes = txtRestricoes.GetValue() != null;
                var possuiResultados = gvResultados.cpMyRowCount > 0;

                var camposNaoPreenchidos = new Array();
                var cont = 0;
                if (!possuiNomeIniciativa) {
                    camposNaoPreenchidos[cont] = "Nome da Iniciativa";
                    cont++;
                }
                if (!possuiLider) {
                    camposNaoPreenchidos[cont] = "Líder";
                    cont++;
                }
                if (!possuiUnidadeGestora) {
                    camposNaoPreenchidos[cont] = "Unidade Gestora";
                    cont++;
                }
                if (!possuiDataInicio) {
                    camposNaoPreenchidos[cont] = "Data Início";
                    cont++;
                }
                if (!possuiDataTermino) {
                    camposNaoPreenchidos[cont] = "Data Término";
                    cont++;
                }
                if (!possuiFocoEstrategico) {
                    camposNaoPreenchidos[cont] = "Foco Estratégico";
                    cont++;
                }
                if (!possuiDirecionadores) {
                    camposNaoPreenchidos[cont] = "Direcionador";
                    cont++;
                }
                //                if (!possuiPublicoAlvo) {
                //                    camposNaoPreenchidos[cont] = "Público Alvo";
                //                    cont++;
                //                }
                //                if (!possuiJustificativa) {
                //                    camposNaoPreenchidos[cont] = "Justificativa";
                //                    cont++;
                //                }
                if (!possuiObjetivoGeral) {
                    camposNaoPreenchidos[cont] = "Objetivo Geral";
                    cont++;
                }
                //                if (!possuiEscopoIniciativa) {
                //                    camposNaoPreenchidos[cont] = "Escopo da Iniciativa";
                //                    cont++;
                //                }
                //                if (!possuiLimiteEscopo) {
                //                    camposNaoPreenchidos[cont] = "Limites do Escopo";
                //                    cont++;
                //                }
                //                if (!possuiPremissas) {
                //                    camposNaoPreenchidos[cont] = "Premissas";
                //                    cont++;
                //                }
                //                if (!possuiRestricoes) {
                //                    camposNaoPreenchidos[cont] = "Restrições";
                //                    cont++;
                //                }
                if (!possuiResultados) {
                    camposNaoPreenchidos[cont] = "Metas";
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
                        var codigoNovoProjeto = resultadoSplit[0].substring(1);
                        window.location = "./propostaDeIniciativa.aspx?CP=" + codigoNovoProjeto + "&tab=" + activeTabIndex;
                        //if (window.parent && window.parent.parent && window.parent.parent.location.pathname === "/wfEngineInterno.aspx" && window.parent.parent.location.search.indexOf('CP') === -1)
                        //    window.parent.parent.location = window.parent.parent.location.href + '&CP=' + codigoNovoProjeto;
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
                if (labelCount.id == 'labelDescricaoResultado')
                    labelCount.innerText = text.length;
                else
                    labelCount.SetText(text.length + ' de ' + maxLength);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function DefineDescricaoGrandeDesafio(selectedValue) {
            txtGrandesDesafios.SetValue(selectedValue[0]);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" scroll="yes">
    <div style="overflow: auto">
        <table border="0" cellpadding="0" cellspacing="0" class="Tabela">
            <tr>
                <td>
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                         Width="100%" 
                        OnCustomJSProperties="pageControl_CustomJSProperties">
                        <TabPages>
                            <dxtc:TabPage Name="tabIdentificacao" Text="Identificação">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlIdentificacao" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                    <div id="dv01" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table class="Tabela" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                                                                                Text="Nome da Iniciativa">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o nome da iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtNomeIniciativa" runat="server" ClientInstanceName="txtNomeIniciativa"
                                                                                     Width="100%" MaxLength="255">
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
                                                                <td class="AvailableReadOnly">
                                                                    <table class="Tabela" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Font-Bold="True"
                                                                                                Text="Líder">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o nome do responsável pela iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="margin-left: 120px">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Font-Bold="True"
                                                                                                Text="Unidade Gestora">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o nome da unidade responsável pela iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="margin-left: 120px">
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Iniciativa">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxtv:ASPxImage ID="ASPxImage20" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o tipo de iniciativa" Width="18px">
                                                                                            </dxtv:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtLider" runat="server" ClientInstanceName="txtLider"
                                                                                    Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtUnidadeGestora" runat="server" ClientInstanceName="txtUnidadeGestora"
                                                                                     Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtTipoIniciativa" runat="server" ClientInstanceName="txtTipoIniciativa" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="AvailableOnlyEditing">
                                                                    <table class="Tabela" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True"
                                                                                                Text="Líder">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o nome do responsável pela iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td style="margin-left: 120px">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True"
                                                                                                Text="Unidade Gestora">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o nome da unidade responsável pela iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td style="margin-left: 120px">
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel57" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Iniciativa">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxtv:ASPxImage ID="ASPxImage21" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir o tipo de iniciativa" Width="18px">
                                                                                            </dxtv:ASPxImage>
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
                                                                                    TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                                        <dxe:ListBoxColumn Caption="E-Mail" FieldName="EMail" Width="300px" />
                                                                                        <dxe:ListBoxColumn Caption="Status" FieldName="StatusUsuario" Width="90px" />
                                                                                    </Columns>
                                                                                    <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxComboBox ID="cmbUnidadeGestora" runat="server" ClientInstanceName="cmbUnidadeGestora"
                                                                                    DataSourceID="sdsUnidadeGestora"  ValueType="System.Int32"
                                                                                    Width="100%" IncrementalFilteringMode="Contains" TextField="NomeUnidadeNegocio"
                                                                                    TextFormatString="{1}" ValueField="CodigoUnidadeNegocio">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="140px" />
                                                                                        <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio" Width="300px" />
                                                                                    </Columns>
                                                                                    <SettingsLoadingPanel Text="Carregando&amp;hellip;"></SettingsLoadingPanel>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxComboBox ID="cmbTipoIniciativa" runat="server" ClientInstanceName="cmbTipoIniciativa" DataSourceID="sdsTipoIniciativa" Font-Names="Verdana" Font-Size="8pt" TextField="Descricao" ValueField="Codigo" ValueType="System.Int16" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <SettingsLoadingPanel Text="Carregando;" />
                                                                                </dxtv:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="AvailableOnlyEditing">
                                                                    <table class="Tabela" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True"
                                                                                                Text="Data Início">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir a data de início da iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                                                                                Text="Data Término">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir a data prevista para o término da iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Font-Bold="True"
                                                                                                Text="Foco Estratégico">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage16" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecionar o Foco Estratégico ao qual a iniciativa está relacionada"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxDateEdit ID="deDataInicio" runat="server" ClientInstanceName="deDataInicio"
                                                                                     Width="100%" PopupVerticalAlign="WindowCenter">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxDateEdit ID="deDataTermino" runat="server" ClientInstanceName="deDataTermino"
                                                                                     Width="100%" PopupVerticalAlign="WindowCenter">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxComboBox ID="cmbFocoEstrategico" runat="server" ClientInstanceName="cmbFocoEstrategico"
                                                                                    DataSourceID="sdsFocoEstrategico"  TextField="Descricao"
                                                                                    ValueField="Codigo" ValueType="System.Int32" Width="100%" DisplayFormatString="{0}"
                                                                                    TextFormatString="{0}">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbDirecionador.PerformCallback(&quot;&quot;);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxtv:ListBoxColumn FieldName="Descricao" Caption="Foco Estratégico" />
                                                                                    </Columns>
                                                                                    <SettingsLoadingPanel Text="Carregando"></SettingsLoadingPanel>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="AvailableReadOnly">
                                                                    <table class="Tabela" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="25%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel31" runat="server" Font-Bold="True"
                                                                                                Text="Data Início">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage18" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir a data de início da iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel32" runat="server" Font-Bold="True"
                                                                                                Text="Data Término">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage19" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Inserir a data prevista para o término da iniciativa"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="50%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel25" runat="server" Font-Bold="True"
                                                                                                Text="Foco Estratégico">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage12" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecionar o Foco Estratégico ao qual a iniciativa está relacionada"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtDataInicio" runat="server" ClientInstanceName="txtDataInicio"
                                                                                    DisplayFormatString="{0:dd/MM/yyy}"  Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtDataTermino" runat="server" ClientInstanceName="txtDataTermino"
                                                                                    DisplayFormatString="{0:dd/MM/yyy}"  Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtFocoEstrategico" runat="server" ClientInstanceName="txtFocoEstrategico"
                                                                                     Width="100%">
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
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Font-Bold="True"
                                                                                                Text="Direcionador">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage17" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecionar o Direcionador ao qual a iniciativa está relacionada"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtDirecionador" runat="server" ClientInstanceName="txtDirecionador"
                                                                                     Width="100%">
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
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel28" runat="server" Font-Bold="True"
                                                                                                Text="Direcionador">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage15" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecionar o Direcionador ao qual a iniciativa está relacionada"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxComboBox ID="cmbDirecionador" runat="server" ClientInstanceName="cmbDirecionador"
                                                                                    DataSourceID="sdsDirecionador"  TextField="Descricao"
                                                                                    ValueField="Codigo" ValueType="System.Int32" Width="100%" OnCallback="cmbDirecionador_Callback">
                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	cmbDirecionador.SetSelectedIndex(-1);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Direcionador" FieldName="Descricao" Width="100%" />
                                                                                    </Columns>
                                                                                    <SettingsLoadingPanel Text="Carregando"></SettingsLoadingPanel>
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
                                                                            <td>
                                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td style="width: 125px">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True"
                                                                                                Text="Grandes Desafios">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px; width: 40px;">
                                                                                            <dxe:ASPxImage ID="ASPxImage5" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Selecionar o Grande Desafio ao qual a iniciativa está relacionada"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                        <td align="right" style="padding-left: 10px; padding-right: 10px;">
                                                                                            <dxe:ASPxButton ID="btnSelecionaDesafio" runat="server" AutoPostBack="False" Text="..."
                                                                                                Width="25px">
                                                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                                gvGrandesDesafios.PerformCallback();
                                                                                                    pcDados.Show();
}" />
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxMemo ID="txtGrandesDesafios" runat="server" ClientEnabled="False" ClientInstanceName="txtGrandesDesafios"
                                                                                    Rows="5" Width="100%">
                                                                                </dxe:ASPxMemo>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table class="Tabela">
                                                                        <tr>
                                                                            <td width="34%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True"
                                                                                                Text="Valor Estimado da Iniciativa">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage9" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="(CAMPO PREENCHIDO AUTOMATICAMENTE COM A SOMA DOS VALORES ESTIMADOS PARA CADA AÇÃO)"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="33%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td width="33%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtValorEstimado" runat="server" ClientInstanceName="txtValorEstimado"
                                                                                     Width="190px" ClientEnabled="False" DisplayFormatString="c">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
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
                            <dxtc:TabPage Name="tabElementosDeResultado" Text="Elementos de Resultado">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlElementosResultado" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent2" runat="server">
                                                    <div id="dv02" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True"
                                                                                    Text="Valor de Fomento">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda0" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Valor de Fomento" Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxSpinEdit ID="txtValorFomento" runat="server" ClientInstanceName="txtValorFomento" DecimalPlaces="2" DisplayFormatString="c2"  Number="0" Width="190px">
                                                                        <SpinButtons ClientVisible="False">
                                                                        </SpinButtons>
                                                                    </dxtv:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <%--                     <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True"
                                                                                    Text="Público Alvo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountPublicoAlvo" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountPublicoAlvo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda0" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Segmento que se pretende atender com a execução do projeto e em relação ao qual serão avaliados os resultados; descrição dos beneficiários diretos da iniciativa"
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
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="True"
                                                                                    Text="Justificativa">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountJustificativa" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountJustificativa">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Razão de existência da iniciativa; problema que se quer solucionar"
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
                                                                         Rows="5" Width="100%">
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
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Font-Bold="True"
                                                                                    Text="Objetivo Geral">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountObjetivoGeral" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountObjetivoGeral">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Alvo principal de todos as metas da iniciativa; a transformação que se pretende alcançar no público-alvo"
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
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight: 700">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Font-Bold="True"
                                                                                    Text="Metas">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Transformações ou produtos resultantes da execução da iniciativa; quantificação do objetivo"
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
                                                                        Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaRegistro" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated"
                                                                        OnRowInserting="gvResultados_RowInserting" OnRowUpdating="gvResultados_RowUpdating"
                                                                        OnCustomJSProperties="grid_CustomJSProperties">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true" ShowUpdateButton="true"
                                                                                ShowCancelButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvResultados", "Metas")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataMemoColumn Caption="Descrição Meta" FieldName="SetencaResultado"
                                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="900px">
                                                                                <PropertiesMemoEdit Rows="5">
                                                                                    <ClientSideEvents Init="function(s, e) {
	labelDescricaoResultado.innerText  = s.GetText().length;
	return setMaxLength(s.GetInputElement(), 500, labelDescricaoResultado);
}" />
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesMemoEdit>
                                                                                <EditFormSettings Caption="Descrição Meta: (&lt;span id='labelDescricaoResultado'&gt;0&lt;/span&gt; de 500) "
                                                                                    CaptionLocation="Top" ColumnSpan="3" Visible="True" VisibleIndex="0" />
                                                                            </dxwgv:GridViewDataMemoColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Transformação/ Produto" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="2" FieldName="TransformacaoProduto">
                                                                                <PropertiesTextEdit MaxLength="255">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="3" Visible="False" VisibleIndex="0" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Meta: de" FieldName="ValorInicialTransformacao"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                <PropertiesSpinEdit DisplayFormatString="g" DecimalPlaces="2"
                                                                                    MaxValue="9999999999999.99" MinValue="-9999999999999.99">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="1" />
                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Quantidade Total" FieldName="ValorFinalTransformacao"
                                                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                <PropertiesSpinEdit DisplayFormatString="g" DecimalPlaces="2"
                                                                                    MaxValue="9999999999999.99" MinValue="-9999999999999.99">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador" FieldName="CodigoIndicador"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                <PropertiesComboBox DataSourceID="sdsIndicador" IncrementalFilteringMode="Contains"
                                                                                    TextField="NomeIndicador" ValueField="CodigoIndicador" ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Indicador" FieldName="NomeIndicador" Width="100%" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="False" VisibleIndex="4" CaptionLocation="Top" ColumnSpan="3" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome Indicador" FieldName="NomeIndicador"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                                <EditFormSettings Visible="False" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Prazo: até" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="7" FieldName="DataLimitePrevista">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings Visible="False" VisibleIndex="3" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequência" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="8">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                        </SettingsPopup>
                                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                                        <SettingsCommandButton>
                                                                            <CancelButton RenderMode="Image">
                                                                            </CancelButton>
                                                                            <EditButton RenderMode="Image">
                                                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                                </Image>
                                                                            </EditButton>
                                                                            <DeleteButton RenderMode="Image">
                                                                            </DeleteButton>
                                                                        </SettingsCommandButton>
                                                                        <StylesEditors>
                                                                            <Style >
                                                                                
                                                                            </Style>
                                                                        </StylesEditors>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Font-Bold="True"
                                                                                    Text="Escopo da Iniciativa">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountEscopoIniciativa" runat="server"
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountEscopoIniciativa">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Resumo do que é a iniciativa e do que será feito"
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
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Font-Bold="True"
                                                                                    Text="Limites do Escopo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountLimitesEscopo" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountLimitesEscopo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda5" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Definir o que NÃO será feito na iniciativa"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtLimitesEscopo" runat="server" ClientInstanceName="txtLimitesEscopo"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountLimitesEscopo;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel17" runat="server" Font-Bold="True"
                                                                                    Text="Premissas">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountPremissas" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountPremissas">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda6" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="São suposições sobre o ambiente externo, fatos dados como verdadeiros; não podem ser controlados pela iniciativa"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtPremissas" runat="server" ClientInstanceName="txtPremissas"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountPremissas;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Font-Bold="True"
                                                                                    Text="Restrições">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountRestricoes" runat="server" 
                                                                                    ForeColor="Silver" Text="0 de 0" ClientInstanceName="lblCountRestricoes">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda7" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Limitações ao trabalho a ser executado pela equipe da iniciativa; fatores que estão sob o controle do gestor/líder; geralmente limitações de orçamento e prazo"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtRestricoes" runat="server" ClientInstanceName="txtRestricoes"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" Init="function(s, e) {
	var labelCount = lblCountRestricoes;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Font-Bold="True"
                                                                                    Text="Interações">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Interlocutores internos e/ou externos que vão contribuir com produtos e serviços para a iniciativa"
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
                                                                        DataSourceID="sdsParceiros"  Width="100%"
                                                                        OnCommandButtonInitialize="grid_CommandButtonInitialize" KeyFieldName="SequenciaRegistro"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvParceiros", "Parceiros")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome do Parceiro" ShowInCustomizationForm="True" FieldName="NomeParceiro">
                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="0" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Produtos Solicitados" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" FieldName="ProdutoSolicitado">
                                                                                <PropertiesTextEdit MaxLength="500">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data Limite" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3" FieldName="DataPrevistaEntrega">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings VisibleIndex="2" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Interlocutor" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4" FieldName="Interlocutor">
                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings VisibleIndex="3" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="7">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
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
                            <dxtc:TabPage Name="tabElementosOperacionais" Text="Elementos Operacionais" Visible="False">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlElementosOperacionais" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent3" runat="server">
                                                    <div id="dv03" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Font-Bold="True"
                                                                                    Text="Ações">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda9" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Ações específicas a serem executadas para, em conjunto, produzir os resultados da iniciativa"
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
                                                                        DataSourceID="sdsAcoes"  Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaAcao" OnCellEditorInitialize="gvAcoes_CellEditorInitialize"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" OnRowInserting="gvAcoes_RowInserting"
                                                                        OnRowUpdating="gvAcoes_RowUpdating" OnRowDeleted="gvAcoes_RowDeleted">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	txtValorEstimado.SetValue(gvAcoes.cp_ValorTotalEstimadoIniciativa);
    gvAcoesView.PerformCallback();
}" />
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvAcoes", "Ações") %>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome da Ação" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="NomeAcao">
                                                                                <PropertiesTextEdit MaxLength="255">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="2" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Entidade" FieldName="CodigoEntidadeResponsavel"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                                <PropertiesComboBox DataSourceID="sdsEntidadesAcao" IncrementalFilteringMode="Contains"
                                                                                    TextField="NomeEntidade" ValueField="CodigoEntidade" ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Entidade" FieldName="NomeEntidade" Width="100%" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Entidade" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                FieldName="NomeEntidadeResponsavel">
                                                                                <EditFormSettings Caption="Entidade Responsável" ColumnSpan="2" Visible="False" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Responsável" FieldName="CodigoUsuarioResponsavel"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                                <PropertiesComboBox IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                                                    DataSourceID="sdsResponsaveisAcao" EnableCallbackMode="True" TextField="NomeUsuario"
                                                                                    TextFormatString="{0}" ValueField="CodigoUsuario">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Width="200px" Caption="Nome" FieldName="NomeUsuario" />
                                                                                        <dxe:ListBoxColumn Caption="E-Mail" FieldName="EMail" Width="300px" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Responsável" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3" FieldName="NomeUsuarioResponsavel">
                                                                                <EditFormSettings Caption="Pessoa Responsável" ColumnSpan="2" Visible="False" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data de Início" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4" Width="100px" FieldName="Inicio">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data de Término" ShowInCustomizationForm="True"
                                                                                VisibleIndex="5" Width="100px" FieldName="Termino">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor" FieldName="ValorPrevisto" ShowInCustomizationForm="True"
                                                                                VisibleIndex="6" Width="100px">
                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Currency"
                                                                                    MaxValue="9999999999999.99" MinValue="-9999999999999.99">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings Caption="Valor Estimado da Ação" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo da Ação" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="7" FieldName="SequenciaAcao">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Font-Bold="True"
                                                                                    Text="Produtos da Ação">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Bens ou serviços quantificáveis entregues ao final da execução da ação"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvProdutosAcao" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvProdutosAcao" DataSourceID="sdsProdutos"
                                                                        Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        KeyFieldName="SequenciaRegistro" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	gvAcoes.Refresh();
}" />
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvProdutosAcao", "Produtos da Ação")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do Produto" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="DescricaoProduto">
                                                                                <PropertiesTextEdit MaxLength="500">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Qtde" FieldName="Quantidade" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" Width="50px">
                                                                                <PropertiesSpinEdit DisplayFormatString="g" NumberFormat="Custom" DecimalPlaces="4"
                                                                                    MaxValue="9999999999999.9999" MinValue="-9999999999999.9999">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings Caption="Quantidade" CaptionLocation="Top" VisibleIndex="2" />
                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data de Término" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3" Width="100px" FieldName="DataLimitePrevista">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings Caption="Data Limite" VisibleIndex="3" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Ação Vinculada" ShowInCustomizationForm="True"
                                                                                VisibleIndex="5" FieldName="SequenciaAcao" Width="200px" GroupIndex="0" SortIndex="0"
                                                                                SortOrder="Ascending">
                                                                                <PropertiesComboBox ValueType="System.Int32" DataSourceID="sdsAcoes" IncrementalFilteringMode="Contains"
                                                                                    TextField="NomeAcao" ValueField="SequenciaAcao">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Ação" FieldName="NomeAcao" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="0" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AutoExpandAllGroups="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Font-Bold="True"
                                                                                    Text="Marcos Críticos" ClientVisible="False">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Eventos cuja superação significa um avanço significativo na realização da ação"
                                                                                    Width="18px" ClientVisible="False">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvMarcosCriticos" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvMarcosCriticos"  Width="100%"
                                                                        OnCommandButtonInitialize="grid_CommandButtonInitialize" KeyFieldName="SequenciaRegistro"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" ClientVisible="False">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	gvAcoes.Refresh();
}" />
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvMarcosCriticos", "Marcos Críticos") %>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do marco crítico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="NomeMarco">
                                                                                <PropertiesTextEdit MaxLength="255">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data Limite" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" Width="100px" FieldName="DataLimitePrevista">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings Caption="Data Limite Prevista" VisibleIndex="2" CaptionLocation="Top"
                                                                                    ColumnSpan="2" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Ação Vinculada" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3" FieldName="SequenciaAcao" Width="200px" GroupIndex="0" SortIndex="0"
                                                                                SortOrder="Ascending">
                                                                                <PropertiesComboBox ValueType="System.Int32" DataSourceID="sdsAcoes" IncrementalFilteringMode="Contains"
                                                                                    TextField="NomeAcao" ValueField="SequenciaAcao">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Ação" FieldName="NomeAcao" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings ColumnSpan="2" VisibleIndex="0" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AutoExpandAllGroups="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" />
                                                                        <SettingsPopup>
                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
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
                            <dxtc:TabPage Text="Visualizar Elementos Operacionais" Visible="False">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                        <div id="dv04" runat="server">
                                            <dxwgv:ASPxGridView ID="gvAcoesView" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvAcoesView"
                                                DataSourceID="sdsAcoes"  KeyFieldName="SequenciaAcao"
                                                Width="100%" OnDataBound="gvAcoesView_DataBound" OnCustomCallback="gvAcoesView_CustomCallback">
                                                <ClientSideEvents EndCallback="function(s, e) {
	txtValorEstimado.SetValue(gvAcoes.cp_ValorTotalEstimadoIniciativa);
}" />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Visible="False"
                                                        VisibleIndex="0" Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                        <HeaderTemplate>
                                                            <% =ObtemBotaoInclusaoRegistro("gvAcoes", "Ações") %>
                                                        </HeaderTemplate>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Nome da Ação" FieldName="NomeAcao" ShowInCustomizationForm="True"
                                                        VisibleIndex="1">
                                                        <PropertiesTextEdit MaxLength="255">
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Entidade" FieldName="CodigoEntidadeResponsavel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                        <PropertiesComboBox DataSourceID="sdsEntidadesAcao" IncrementalFilteringMode="Contains"
                                                            TextField="NomeEntidade" ValueField="CodigoEntidade" ValueType="System.Int32">
                                                            <Columns>
                                                                <dxe:ListBoxColumn Caption="Entidade" FieldName="NomeEntidade" Width="100%" />
                                                            </Columns>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings CaptionLocation="Top" Visible="True" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Entidade" FieldName="NomeEntidadeResponsavel"
                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                        <EditFormSettings Caption="Entidade Responsável" CaptionLocation="Top" ColumnSpan="2"
                                                            Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Responsável" FieldName="CodigoUsuarioResponsavel"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        <PropertiesComboBox DataSourceID="sdsResponsaveisAcao" EnableCallbackMode="True"
                                                            IncrementalFilteringMode="Contains" TextField="NomeUsuario" TextFormatString="{0}"
                                                            ValueField="CodigoUsuario" ValueType="System.Int32">
                                                            <Columns>
                                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                <dxe:ListBoxColumn Caption="E-Mail" FieldName="EMail" Width="300px" />
                                                            </Columns>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings CaptionLocation="Top" Visible="True" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuarioResponsavel"
                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                        <EditFormSettings Caption="Pessoa Responsável" CaptionLocation="Top" ColumnSpan="2"
                                                            Visible="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Data de Início" FieldName="Inicio" ShowInCustomizationForm="True"
                                                        VisibleIndex="4" Width="100px">
                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesDateEdit>
                                                        <EditFormSettings CaptionLocation="Top" />
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Data de Término" FieldName="Termino" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="100px">
                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesDateEdit>
                                                        <EditFormSettings CaptionLocation="Top" />
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Valor" FieldName="ValorPrevisto" ShowInCustomizationForm="True"
                                                        VisibleIndex="6" Width="100px">
                                                        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" MaxValue="9999999999999.99"
                                                            MinValue="-9999999999999.99" NumberFormat="Currency">
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesSpinEdit>
                                                        <EditFormSettings Caption="Valor Estimado da Ação" CaptionLocation="Top" />
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Codigo da Ação" FieldName="SequenciaAcao"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm" />
                                                <SettingsPopup>
                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                        AllowResize="True" Width="600px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>
                                                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                <SettingsDetail ShowDetailRow="True" />
                                                <Templates>
                                                    <DetailRow>
                                                        <dxwgv:ASPxGridView ID="gvProdutosAcaoView" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvProdutosAcaoView" DataSourceID="sdsProdutosView"
                                                            KeyFieldName="SequenciaRegistro" OnBeforePerformDataSelect="detailGrid_BeforePerformDataSelect"
                                                            Width="100%">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	gvAcoes.Refresh();
}" />
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Visible="False"
                                                                    VisibleIndex="0" Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvProdutosAcaoView", "Produtos da Ação")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Produto" FieldName="DescricaoProduto" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                    <PropertiesTextEdit MaxLength="500">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="1" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Qtde" FieldName="Quantidade" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" Width="50px">
                                                                    <PropertiesSpinEdit DecimalPlaces="4" DisplayFormatString="g" MaxValue="9999999999999.9999"
                                                                        MinValue="-9999999999999.9999" NumberFormat="Custom">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesSpinEdit>
                                                                    <EditFormSettings Caption="Quantidade" CaptionLocation="Top" VisibleIndex="2" />
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                <dxwgv:GridViewDataDateColumn Caption="Data de Término" FieldName="DataLimitePrevista"
                                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesDateEdit>
                                                                    <EditFormSettings Caption="Data Limite" CaptionLocation="Top" VisibleIndex="3" />
                                                                </dxwgv:GridViewDataDateColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="4">
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                    AllowResize="True" Width="600px" />
                                                            </SettingsPopup>
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                        </dxwgv:ASPxGridView>
                                                        <br />
                                                        <dxwgv:ASPxGridView ID="gvMarcosCriticosView" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvMarcosCriticosView" DataSourceID="sdsMarcosCriticosView"
                                                             KeyFieldName="SequenciaRegistro" OnBeforePerformDataSelect="detailGrid_BeforePerformDataSelect"
                                                            Width="100%" Visible="False">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	gvAcoes.Refresh();
}" />
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Visible="False"
                                                                    VisibleIndex="0" Width="50px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvMarcosCriticosView", "Marcos Críticos") %>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Marco Crítico" FieldName="NomeMarco" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                    <PropertiesTextEdit MaxLength="255">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="1" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataDateColumn Caption="Data Limite" FieldName="DataLimitePrevista"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesDateEdit>
                                                                    <EditFormSettings Caption="Data Limite Prevista" CaptionLocation="Top" ColumnSpan="2"
                                                                        VisibleIndex="2" />
                                                                </dxwgv:GridViewDataDateColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="4">
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                    Width="600px" AllowResize="True" />
                                                            </SettingsPopup>
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                        </dxwgv:ASPxGridView>
                                                    </DetailRow>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
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
                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" CssClass="textoComIniciaisMaiuscula"
                                    Text="Salvar" AutoPostBack="False" Width="90px">
                                    <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
	btnImprimir.SetEnabled(true);
	//btnImprimir0.SetEnabled(true);
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="padding-left: 10px">
                                <dxe:ASPxButton ID="btnImprimir" runat="server" ClientInstanceName="btnImprimir" CssClass="textoComIniciaisMaiuscula"
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
            </tr>
        </table>
    </div>
    <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="None"
        HeaderText="Seleção de Grande Desafio" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        Modal="True" ShowCloseButton="False" Width="750px" AllowDragging="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr>
                        <td>
                            <dxwgv:ASPxGridView ID="gvGrandesDesafios" runat="server" AutoGenerateColumns="False"
                                DataSourceID="sdsOpcoesDesafios" Width="100%" KeyFieldName="CodigoGrandeDesafio"
                                ClientInstanceName="gvGrandesDesafios" OnCustomCallback="gvGrandesDesafios_CustomCallback">
                                <ClientSideEvents SelectionChanged="function(s, e) {
	e.processOnServer = false;
}" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                        VisibleIndex="0" Width="45px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Desafio" ShowInCustomizationForm="True" VisibleIndex="1"
                                        FieldName="DescricaoGrandeDesafio">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Codigo" FieldName="CodigoGrandeDesafio" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" />
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-top: 10px">
                            <table>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxButton ID="btnConfirmar" runat="server" AutoPostBack="False" Text="Confirmar"
                                            Width="75px">
                                            <ClientSideEvents Click="function(s, e) {
	pcDados.Hide();
    if(gvGrandesDesafios.GetSelectedRowCount() == 1)
	    gvGrandesDesafios.GetSelectedFieldValues(&quot;DescricaoGrandeDesafio&quot;, DefineDescricaoGrandeDesafio);
	else
		txtGrandesDesafios.SetValue(null);
	existeConteudoCampoAlterado = true;
}" />
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
    <dxpc:ASPxPopupControl ID="ASPxPopupMsgEdicaoTai" runat="server" HeaderText="Mensagem do Sistema"
        Width="383px" ClientInstanceName="ASPxPopupMsgEdicaoTai"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="Tabela">
                    <tr>
                        <td align="center">
                            <dxe:ASPxLabel ID="ASPxLabel55" runat="server" EncodeHtml="False" Font-Bold="True"
                                 Text="Atenção!!! ">
                                <ClientSideEvents Init="function(s, e) {
	if(window.parent.textoItem)
		s.SetText('Atenção!!! &lt;br&gt;&lt;br&gt; Somente irão refletir no projeto as alterações nos campos:&lt;br&gt;&lt;ul&gt;&lt;li style=&quot;text-align:left&quot;&gt;Nome da Iniciativa&lt;br&gt;&lt;li style=&quot;text-align:left&quot;&gt;Líder&lt;br&gt;&lt;li style=&quot;text-align:left&quot;&gt;Unidade Gestora e&lt;br&gt; &lt;li style=&quot;text-align:left&quot;&gt;Objetivo Geral.&lt;/ul&gt;');
}" />
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
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
    <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT ta.[CodigoProjeto]
      ,ta.[NomeIniciativa]
      ,ta.[CodigoGerenteIniciativa]
      ,ta.[NomeGerenteIniciativa]
      ,ta.[CodigoUnidadeNegocio]
      ,ta.[NomeUnidadeNegocio]
      ,ta.[DataInicio]
      ,ta.[DataTermino]
      ,ta.[CR]
      ,CONVERT(INT, ta.[CodigoFocoEstrategico]) AS CodigoFocoEstrategico
      ,ta.[NomeFocoEstrategico]
      ,isnull(ta.[ValorEstimado],0.0) as [ValorEstimado]
      ,ta.[PublicoAlvo]
      ,ta.[Justificativa]
      ,ta.[ObjetivoGeral]
      ,ta.[Escopo]
      ,ta.[LimiteEscopo]
      ,ta.[Premissas]
      ,ta.[Restricoes]
      ,CONVERT(INT,ta.[CodigoOE_Direcionador]) AS CodigoOE_Direcionador
      ,ta.[DescricaoDirecionadorEstrategico]
      ,ta.[CodigoIndicadorDesafio]
      ,ta.[DescricaoDesafio]
      ,isnull(ta.[ValorFomento],0.0) as ValorFomento
      ,tp.TipoProjeto as Descricao
      ,tp.CodigoTipoProjeto as Codigo
  FROM [TermoAbertura] ta
        inner join Projeto pr on (pr.CodigoProjeto = ta.CodigoProjeto)
left join TipoProjeto tp on (tp.CodigoTipoProjeto = pr.CodigoTipoProjeto)
WHERE (ta.CodigoProjeto = @CodigoProjeto)" OnSelected="sdsDadosFomulario_Selected">
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
    AND CodigoEntidade &lt;&gt; CodigoUnidadeNegocio
  ORDER BY 
        SiglaUnidadeNegocio">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsFocoEstrategico" runat="server" SelectCommand=" SELECT oes.CodigoobjetoEstrategia AS Codigo,  
        me.TituloMapaEstrategico AS Mapa, 
        oes.TituloObjetoEstrategia AS Descricao
   FROM ObjetoEstrategia oes INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oes.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
      AND oes.CodigoTipoObjetoEstrategia = (select codigotipoobjetoEstrategia from tipoobjetoestrategia where [IniciaisTipoObjeto] = 'PSP')
    AND oeS.DataExclusao IS NULL
    AND un.[DataExclusao] IS NULL
ORDER BY 2,3">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsTipoIniciativa" runat="server" SelectCommand="SELECT tp.[CodigoTipoProjeto] AS [Codigo]
   , tp.[TipoProjeto]    AS [Descricao]
FROM [dbo].[TipoProjeto] tp 
WHERE (tp.[IndicaTipoProjeto] in ('PRJ', 'PRC')
AND CHARINDEX('diret', tp.[IniciaisTipoControladoSistema]) &gt; 0)
OR (tp.[IndicaTipoProjeto]='PRG' AND tp.[IndicaProjetoAgil]='S')"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDirecionador" runat="server" SelectCommand="SELECT oe.*, oe.CodigoobjetoEstrategia AS Codigo, 
      isnull(oe.TituloObjetoEstrategia, 'Título do direcionador não informado') +  ' - ' + isnull(oe.DescricaoObjetoEstrategia, 'Descrição do direcionador não informado') AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on (me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico  and me.CodigoMapaEstrategico = oes.CodigoMapaEstrategico) INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S' 
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio 
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = (select codigotipoobjetoEstrategia from tipoobjetoestrategia where [IniciaisTipoObjeto] = 'TEM')
    AND oe.DataExclusao IS NULL
    AND un.[DataExclusao] IS NULL
    AND oe.CodigoObjetoEstrategiaSuperior = @CodigoObjetoEstrategiaSuperior 
ORDER BY 2" InsertCommand="INSERT INTO tai_DirecionadoresIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[CodigoOE_Direcionador]
           ,[NomeOE_Direcionador])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_DirecionadoresIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoOE_Direcionador
           ,@NomeOE_Direcionador)" UpdateCommand="UPDATE tai_DirecionadoresIniciativa
   SET [CodigoOE_Direcionador] = @CodigoOE_Direcionador
      ,[NomeOE_Direcionador] = @NomeOE_Direcionador
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro
" DeleteCommand="DELETE FROM tai_DirecionadoresIniciativa
 WHERE CodigoProjeto = @CodigoProjeto
     AND SequenciaRegistro = @SequenciaRegistro" FilterExpression="CodigoSuperior = {0}"
        OnFiltering="sqlDataSource_Filtering">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="pageControl$pnlIdentificacao$cmbFocoEstrategico"
                Name="CodigoSuperior" PropertyName="Value" />
        </FilterParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="CodigoOE_Direcionador" />
            <asp:Parameter Name="NomeOE_Direcionador" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:ControlParameter ControlID="pageControl$pnlIdentificacao$cmbFocoEstrategico"
                DefaultValue="-1" Name="CodigoObjetoEstrategiaSuperior" PropertyName="Value" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoOE_Direcionador" />
            <asp:Parameter Name="NomeOE_Direcionador" />
            <asp:Parameter Name="SequenciaRegistro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsOpcoesDirecionador" runat="server" SelectCommand=" SELECT oe.CodigoobjetoEstrategia AS Codigo, 
        oe.DescricaoObjetoEstrategia AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior,
        oes.TituloObjetoEstrategia DescricaoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on (oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior 
                                 and oes.CodigoTipoObjetoEstrategia = 11  )INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico  INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL

union 
 SELECT oe.CodigoobjetoEstrategia AS Codigo, 
        oe.DescricaoObjetoEstrategia AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior,
        oes.TituloObjetoEstrategia DescricaoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on (oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior 
                                 and oes.CodigoTipoObjetoEstrategia = 13  )INNER JOIN 
        ObjetoEstrategia oes2 on oes2.CodigoObjetoEstrategia = oes.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL
  ORDER BY 4,2
">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsGrandesDesafios" runat="server" DeleteCommand=" DELETE FROM tai_DesafiosIniciativa
  WHERE [CodigoProjeto] = @CodigoProjeto
    AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai_DesafiosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[CodigoDesafio]
           ,[DescricaoDesafio])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_DesafiosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoDesafio
           ,@DescricaoDesafio) " SelectCommand="SELECT oe.CodigoobjetoEstrategia AS Codigo, 
       ( CASE WHEN LEN(oe.TituloObjetoEstrategia) &lt; 1 THEN 'Título do Grande Desafio não informado' ELSE oe.TituloObjetoEstrategia END +  ' - ' +
	  CASE WHEN LEN(oe.DescricaoObjetoEstrategia) &lt; 1 THEN 'Descrição do Grande Desafio não informado' ELSE oe.DescricaoObjetoEstrategia END
       )  AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = (select codigotipoobjetoEstrategia from tipoobjetoestrategia where [IniciaisTipoObjeto] = 'OBJ')
    AND oe.DataExclusao IS NULL
    AND un.[DataExclusao] IS NULL
    AND oe.CodigoObjetoEstrategiaSuperior = @CodigoObjetoEstrategiaSuperior
ORDER BY 2" UpdateCommand=" UPDATE tai_DesafiosIniciativa
    SET [CodigoDesafio] = @CodigoDesafio, 
        [DescricaoDesafio] = @DescricaoDesafio
  WHERE [CodigoProjeto] = @CodigoProjeto
       AND [SequenciaRegistro] = @SequenciaRegistro
">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoDesafio" />
            <asp:Parameter Name="DescricaoDesafio" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" DefaultValue="-1" />
            <asp:ControlParameter ControlID="pageControl$pnlIdentificacao$cmbDirecionador" DefaultValue="-1" Name="CodigoObjetoEstrategiaSuperior" PropertyName="Value" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="CodigoDesafio" />
            <asp:Parameter Name="DescricaoDesafio" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsOpcoesDesafios" runat="server" SelectCommand="SELECT oe.CodigoobjetoEstrategia AS CodigoGrandeDesafio,
       ( CASE WHEN LEN(oe.TituloObjetoEstrategia) < 1 THEN 'Título do Grande Desafio não informado' ELSE oe.TituloObjetoEstrategia END +  ' - ' +
   CASE WHEN LEN(oe.DescricaoObjetoEstrategia) < 1 THEN 'Descrição do Grande Desafio não informado' ELSE oe.DescricaoObjetoEstrategia END
       )  AS DescricaoGrandeDesafio,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on (me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico and me.CodigoMapaEstrategico = oes.CodigoMapaEstrategico) INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = (select codigotipoobjetoEstrategia from tipoobjetoestrategia where [IniciaisTipoObjeto] = 'OBJ')
    AND oe.DataExclusao IS NULL
    AND un.[DataExclusao] IS NULL
    AND oe.CodigoObjetoEstrategiaSuperior = @CodigoObjetoEstrategiaSuperior
ORDER BY 2"
        DeleteCommand="DELETE FROM tai_DesafiosIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" FilterExpression="CodigoSuperior = {0}">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="pageControl$pnlIdentificacao$cmbDirecionador" Name="CodigoSuperios" PropertyName="Value" />
        </FilterParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:ControlParameter ControlID="pageControl$pnlIdentificacao$cmbDirecionador" DefaultValue="-1" Name="CodigoObjetoEstrategiaSuperior" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsResultados" runat="server" SelectCommand="SELECT * FROM dbo.tai_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY SetencaResultado"
        DeleteCommand="DELETE FROM tai_ResultadosIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai_ResultadosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[SetencaResultado]
           ,[ValorFinalTransformacao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto)
           ,@SetencaResultado ,
@ValorFinalTransformacao)" UpdateCommand="UPDATE tai_ResultadosIniciativa
   SET [ValorFinalTransformacao]  = @ValorFinalTransformacao
      ,[SetencaResultado] = @SetencaResultado
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SetencaResultado" />
            <asp:Parameter Name="ValorFinalTransformacao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="ValorFinalTransformacao" />
            <asp:Parameter Name="SetencaResultado" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsParceiros" runat="server" SelectCommand="SELECT * FROM tai_ParceirosIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeParceiro"
        DeleteCommand="DELETE FROM tai_ParceirosIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai_ParceirosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeParceiro]
           ,[ProdutoSolicitado]
           ,[DataPrevistaEntrega]
           ,[Interlocutor]
           ,[GrauInfluencia]
           ,[TipoParceiro])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_ParceirosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeParceiro
           ,@ProdutoSolicitado
           ,@DataPrevistaEntrega
           ,@Interlocutor
           ,@GrauInfluencia
           ,@TipoParceiro)" UpdateCommand="UPDATE tai_ParceirosIniciativa
   SET [NomeParceiro] = @NomeParceiro
      ,[ProdutoSolicitado] = @ProdutoSolicitado
      ,[DataPrevistaEntrega] = @DataPrevistaEntrega
      ,[Interlocutor] = @Interlocutor
      ,[GrauInfluencia] = @GrauInfluencia
      ,[TipoParceiro] = @TipoParceiro
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter DefaultValue="" Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeParceiro" />
            <asp:Parameter Name="ProdutoSolicitado" />
            <asp:Parameter Name="DataPrevistaEntrega" />
            <asp:Parameter Name="Interlocutor" />
            <asp:Parameter Name="GrauInfluencia" />
            <asp:Parameter Name="TipoParceiro" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="NomeParceiro" />
            <asp:Parameter Name="ProdutoSolicitado" />
            <asp:Parameter Name="DataPrevistaEntrega" />
            <asp:Parameter Name="Interlocutor" />
            <asp:Parameter Name="GrauInfluencia" />
            <asp:Parameter Name="TipoParceiro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAcoes" runat="server" SelectCommand="SELECT * FROM tai_AcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeAcao"
        DeleteCommand="DELETE FROM tai_AcoesIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaAcao] = @SequenciaAcao" InsertCommand="INSERT INTO tai_AcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaAcao]
           ,[NomeAcao]
           ,[CodigoEntidadeResponsavel]
           ,[NomeEntidadeResponsavel]
           ,[CodigoUsuarioResponsavel]
           ,[NomeUsuarioResponsavel]
           ,[Inicio]
           ,[Termino]
           ,[ValorPrevisto])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaAcao),0) + 1 FROM tai_AcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeAcao
           ,@CodigoEntidadeResponsavel
           ,@NomeEntidadeResponsavel
           ,@CodigoUsuarioResponsavel
           ,@NomeUsuarioResponsavel
           ,@Inicio
           ,@Termino
           ,@ValorPrevisto)" UpdateCommand="UPDATE tai_AcoesIniciativa
   SET [NomeAcao] = @NomeAcao
      ,[CodigoEntidadeResponsavel] = @CodigoEntidadeResponsavel
      ,[NomeEntidadeResponsavel] = @NomeEntidadeResponsavel
      ,[CodigoUsuarioResponsavel] = @CodigoUsuarioResponsavel
      ,[NomeUsuarioResponsavel] = @NomeUsuarioResponsavel
      ,[Inicio] = @Inicio
      ,[Termino] = @Termino
      ,[ValorPrevisto] = @ValorPrevisto
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaAcao] = @SequenciaAcao">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaAcao" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeAcao" />
            <asp:Parameter Name="CodigoEntidadeResponsavel" />
            <asp:Parameter Name="NomeEntidadeResponsavel" />
            <asp:Parameter Name="CodigoUsuarioResponsavel" />
            <asp:Parameter Name="NomeUsuarioResponsavel" />
            <asp:Parameter Name="Inicio" />
            <asp:Parameter Name="Termino" />
            <asp:Parameter Name="ValorPrevisto" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaAcao" />
            <asp:Parameter Name="NomeAcao" />
            <asp:Parameter Name="CodigoEntidadeResponsavel" />
            <asp:Parameter Name="NomeEntidadeResponsavel" />
            <asp:Parameter Name="CodigoUsuarioResponsavel" />
            <asp:Parameter Name="NomeUsuarioResponsavel" />
            <asp:Parameter Name="Inicio" />
            <asp:Parameter Name="Termino" />
            <asp:Parameter Name="ValorPrevisto" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProdutos" runat="server" SelectCommand="SELECT * FROM tai_ProdutosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY DescricaoProduto"
        DeleteCommand="DELETE FROM tai_ProdutosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai_ProdutosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[DescricaoProduto]
           ,[Quantidade]
           ,[DataLimitePrevista]
           ,[SequenciaAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_ProdutosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@DescricaoProduto
           ,@Quantidade
           ,@DataLimitePrevista
           ,@SequenciaAcao)" UpdateCommand="UPDATE tai_ProdutosAcoesIniciativa
   SET [DescricaoProduto] = @DescricaoProduto
      ,[Quantidade] = @Quantidade
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SequenciaAcao] = @SequenciaAcao
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="DescricaoProduto" />
            <asp:Parameter Name="Quantidade" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
            <asp:Parameter Name="DescricaoProduto" />
            <asp:Parameter Name="Quantidade" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProdutosView" runat="server" SelectCommand="SELECT * FROM tai_ProdutosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto AND SequenciaAcao = @SequenciaAcao ORDER BY DescricaoProduto"
        DeleteCommand="DELETE FROM tai_ProdutosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai_ProdutosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[DescricaoProduto]
           ,[Quantidade]
           ,[DataLimitePrevista]
           ,[SequenciaAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_ProdutosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@DescricaoProduto
           ,@Quantidade
           ,@DataLimitePrevista
           ,@SequenciaAcao)" UpdateCommand="UPDATE tai_ProdutosAcoesIniciativa
   SET [DescricaoProduto] = @DescricaoProduto
      ,[Quantidade] = @Quantidade
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SequenciaAcao] = @SequenciaAcao
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="DescricaoProduto" />
            <asp:Parameter Name="Quantidade" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:SessionParameter Name="SequenciaAcao" SessionField="SequenciaAcao" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
            <asp:Parameter Name="DescricaoProduto" />
            <asp:Parameter Name="Quantidade" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsMarcosCriticos" runat="server" SelectCommand="SELECT * FROM tai_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeMarco"
        DeleteCommand="DELETE FROM tai_MarcosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai_MarcosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeMarco]
           ,[DataLimitePrevista]
           ,[SequenciaAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeMarco
           ,@DataLimitePrevista
           ,@SequenciaAcao)" UpdateCommand="UPDATE tai_MarcosAcoesIniciativa
   SET [NomeMarco] = @NomeMarco
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SequenciaAcao] = @SequenciaAcao
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
            <asp:Parameter Name="SequenciaAcao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="NomeMarco" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsMarcosCriticosView" runat="server" SelectCommand="SELECT * FROM tai_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto AND SequenciaAcao = @SequenciaAcao ORDER BY NomeMarco"
        DeleteCommand="DELETE FROM tai_MarcosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai_MarcosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeMarco]
           ,[DataLimitePrevista]
           ,[SequenciaAcao])
     VALUES
           (@CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeMarco
           ,@DataLimitePrevista
           ,@SequenciaAcao)" UpdateCommand="UPDATE tai_MarcosAcoesIniciativa
   SET [NomeMarco] = @NomeMarco
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SequenciaAcao] = @SequenciaAcao
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
            <asp:Parameter Name="SequenciaAcao" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:SessionParameter Name="SequenciaAcao" SessionField="SequenciaAcao" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="NomeMarco" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SequenciaAcao" />
        </UpdateParameters>
    </asp:SqlDataSource>
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
    </form>
</body>
</html>
