<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_005.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_005" meta:resourcekey="PageResource5" %>

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
            if (pageControl.cpReadOnly)
                return true;

            var nomeProjeto = txtNomeProjeto.GetValue() != null;
            var lider = cmbLider.GetValue() != null;
            var unidadeGestora = cmbUnidadeGestora.GetValue() != null;
            var dataInicio = deDataInicio.GetValue() != null;
            var dataTermino = deDataTermino.GetValue() != null;
            var valorProjeto = txtValorProjeto.GetValue() != null;
            var valorContrapartida = txtValorContrapartida.GetValue() != null;
            var fontesFinanciamento = txtFontesFinanciamento.GetValue() != null;
            var objetivoGeral = txtObjetivoGeral.GetValue() != null;
            var justificativa = txtJustificativa.GetValue() != null;
            var escopoIniciativa = txtEscopoIniciativa.GetValue() != null;

            return nomeProjeto &&
                lider &&
                unidadeGestora &&
                dataInicio &&
                dataTermino &&
                valorProjeto &&
                valorContrapartida &&
                fontesFinanciamento &&
                objetivoGeral &&
                justificativa &&
                escopoIniciativa;
        }

        function ValidaCampos() {
            var msg = ""
            var nomeProjeto = txtNomeProjeto.GetText();
            var codigoUnidadeGestora = cmbUnidadeGestora.GetValue();

            if (!nomeProjeto || 0 === nomeProjeto.length)
                msg += "O campo 'Título do Projeto' deve ser informado.\n";
            if (!codigoUnidadeGestora || codigoUnidadeGestora == null)
                msg += "O campo 'Unidade Executora' deve ser informado.\n";

            return msg;
        }

        function verificaAvancoWorkflow() {
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'atencao', true, false, null);
                return false;
            }

            if (!camposPreenchidos) {
                var possuiNomeProjeto = txtNomeProjeto.GetValue() != null;
                var possuiLider = cmbLider.GetValue() != null;
                var possuiUnidadeGestora = cmbUnidadeGestora.GetValue() != null;
                var possuiDataInicio = deDataInicio.GetValue() != null;
                var possuiDataTermino = deDataTermino.GetValue() != null;
                var possuiValorProjeto = txtValorProjeto.GetValue() != null;
                var possuiValorContrapartida = txtValorContrapartida.GetValue() != null;
                var possuiFontesFinanciamento = txtFontesFinanciamento.GetValue() != null;
                var possuiObjetivoGeral = txtObjetivoGeral.GetValue() != null;
                var possuiJustificativa = txtJustificativa.GetValue() != null;
                var possuiEscopoIniciativa = txtEscopoIniciativa.GetValue() != null;

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
                if (!possuiDataInicio) {
                    camposNaoPreenchidos[cont] = "Data de Início";
                    cont++;
                }
                if (!possuiDataTermino) {
                    camposNaoPreenchidos[cont] = "Data de Término";
                    cont++;
                }
                if (!possuiValorProjeto) {
                    camposNaoPreenchidos[cont] = "Valor do Projeto";
                    cont++;
                }
                if (!possuiValorContrapartida) {
                    camposNaoPreenchidos[cont] = "Valor das Contrapartidas";
                    cont++;
                }
                if (!possuiFontesFinanciamento) {
                    camposNaoPreenchidos[cont] = "Fontes de Financiamento";
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
                if (!possuiEscopoIniciativa) {
                    camposNaoPreenchidos[cont] = "Escopo";
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

                window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);

                if (result.substring(0, 1) == "I") {
                    var activeTabIndex = pageControl.GetActiveTabIndex();
                    window.location = "./propostaDeIniciativa_005.aspx?CP=" + result.substring(1) + "&tab=" + activeTabIndex;
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
                        Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvar0Resource1" Width="100px">
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
                <td style="margin-left: 40px">
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
                                                                                                Text="Título do projeto" meta:resourcekey="ASPxLabel1Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="imgAjudaResource1"
                                                                                                ToolTip="Nome do projeto ou investimento, correspondendo à numeração recebida no INFOPLAN">
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
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="É o encarregado individual de planejar e programar as tarefas e a gestão cotidiana da execução do projeto."
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
                                                                                                Text="Unidade Executora" meta:resourcekey="ASPxLabel24Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de negócio ou corporativa à qual o projeto ou investimento está associado."
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
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="É o encarregado individual de planejar e programar as tarefas e a gestão cotidiana da execução do projeto."
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
                                                                                                Text="Unidade Executora" meta:resourcekey="ASPxLabel3Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Unidade de negócio ou corporativa à qual o projeto ou investimento está associado."
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
                                                                                    TextFormatString="{1}" ValueField="CodigoUnidadeNegocio" meta:resourcekey="cmbUnidadeGestoraResource1">
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
                                                                    <table class="Tabela">
                                                                        <tr>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Font-Bold="True"
                                                                                                Text="Início" meta:resourcekey="ASPxLabel7Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage24" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Data prevista para início da execução do projeto."
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
                                                                                            <dxe:ASPxLabel ID="ASPxLabel39" runat="server" Font-Bold="True"
                                                                                                Text="Término" meta:resourcekey="ASPxLabel8Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage25" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage7Resource1"
                                                                                                ToolTip="Data prevista para término da execução do projeto.">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel40" runat="server" Font-Bold="True"
                                                                                                Text="Valor do Projeto" meta:resourcekey="ASPxLabel10Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage26" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage9Resource1"
                                                                                                ToolTip="Valor total para execução do projeto  (em R$).">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="25%">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel ID="ASPxLabel41" runat="server" Font-Bold="True"
                                                                                                Text="Valor das Contrapartidas" meta:resourcekey="ASPxLabel13Resource1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage27" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" meta:resourcekey="ASPxImage12Resource1"
                                                                                                ToolTip="Valor referente às contrapartidas para execução do projeto  (em R$).">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxDateEdit ID="deDataInicio" runat="server" ClientInstanceName="deDataInicio"
                                                                                     meta:resourceKey="deDataInicioResource1"
                                                                                    PopupVerticalAlign="WindowCenter" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxDateEdit ID="deDataTermino" runat="server" ClientInstanceName="deDataTermino"
                                                                                     meta:resourceKey="deDataTerminoResource1"
                                                                                    PopupVerticalAlign="WindowCenter" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtValorProjeto" runat="server" ClientInstanceName="txtValorProjeto"
                                                                                    Width="100%" DisplayFormatString="{0:n2}" HorizontalAlign="Right">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtValorContrapartida" runat="server" ClientInstanceName="txtValorContrapartida"
                                                                                    Width="170px" DisplayFormatString="{0:n2}" HorizontalAlign="Right">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                </dxe:ASPxTextBox>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True"
                                                                                    Text="Fontes de Financiamento">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountFontesFinanciamento" runat="server" ClientInstanceName="lblCountFontesFinanciamento"
                                                                                     ForeColor="Silver" Text="0 de 0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda17" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="De onde virão os recursos: recursos próprios, parceiros (relacionar nomes)."
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtFontesFinanciamento" runat="server" ClientInstanceName="txtFontesFinanciamento"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountFontesFinanciamento;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxMemo>
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
                                                                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True"
                                                                                                meta:resourceKey="ASPxLabel2Resource1" Text="Linha de Financiamento">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                                Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage1Resource1"
                                                                                                Width="18px">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxComboBox ID="cmbLinhaFinanciamento" runat="server" ClientInstanceName="cmbLinhaFinanciamento"
                                                                                    EnableCallbackMode="True"  IncrementalFilteringMode="Contains"
                                                                                    meta:resourceKey="cmbLiderResource1" Width="350px" SelectedIndex="0">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Selected="True" Text="Desenvolvimento ou melhoria de produto/serviço"
                                                                                            Value="DM" />
                                                                                        <dxe:ListEditItem Text="Modernização física" Value="MF" />
                                                                                        <dxe:ListEditItem Text="Modernização Tecnológica" Value="MT" />
                                                                                        <dxe:ListEditItem Text="Modernização da Gestão" Value="MG" />
                                                                                        <dxe:ListEditItem Text="Desenvolvimento de Competências" Value="DC" />
                                                                                        <dxe:ListEditItem Text="Estimulo a produção" Value="EP" />
                                                                                        <dxe:ListEditItem Text="Feiras e eventos" Value="FE" />
                                                                                        <dxe:ListEditItem Text="Comunicação" Value="CO" />
                                                                                        <dxe:ListEditItem Text="Outros" Value="OU" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabElementosResultado" Text="Elementos de Resultado">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlElementosResultado" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <div id="dv02" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                                                                    Text="Setor Industrial">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountSetorIndustrial" runat="server" ClientInstanceName="lblCountSetorIndustrial"
                                                                                     ForeColor="Silver" Text="0 de 0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txtSetorIndustrial" runat="server" ClientInstanceName="txtSetorIndustrial"
                                                                         Rows="5" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountSetorIndustrial;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel42" runat="server" Font-Bold="True"
                                                                                    Text="Cliente">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountPublicoAlvo" runat="server" ClientInstanceName="lblCountPublicoAlvo"
                                                                                     ForeColor="Silver" Text="0 de 0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda13" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Público-alvo do projeto.">
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
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountPublicoAlvo;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Font-Bold="True"
                                                                                    Text="Parceiros">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvParceiros" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParceiros"
                                                                        DataSourceID="sdsParceiros"  KeyFieldName="SequenciaRegistro"
                                                                        OnCommandButtonInitialize="grid_CommandButtonInitialize" OnRowInserted="grid_RowInserted"
                                                                        OnRowUpdated="grid_RowUpdated" Width="100%">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvParceiros", "Parceiros")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome do Parceiro" FieldName="NomeParceiro"
                                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="0" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                                <PropertiesTextEdit MaxLength="150">
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" VisibleIndex="2" ColumnSpan="2" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Interlocutor" FieldName="NomeInterlocutor"
                                                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                                <PropertiesTextEdit MaxLength="100">
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" VisibleIndex="1" ColumnSpan="2" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Telefone" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                Width="100px" FieldName="NumeroTelefone">
                                                                                <PropertiesTextEdit MaxLength="20" Width="100px">
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" VisibleIndex="3" />
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel47" runat="server" Font-Bold="True"
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
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Descrever o motivo que impulsionou a geração do projeto e a sua importância."
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
                                                                         Rows="10" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountObjetivoGeral;
	var maxLength = 500;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight: 700">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel44" runat="server" Font-Bold="True"
                                                                                    Text="Resultados">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda16" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
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
                                                                        KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        OnCustomJSProperties="grid_CustomJSProperties" OnRowInserted="grid_RowInserted"
                                                                        OnRowInserting="gvResultados_RowInserting" OnRowUpdated="grid_RowUpdated" OnRowUpdating="gvResultados_RowUpdating"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvResultados", "Resultados")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do Resultado" FieldName="SetencaResultado"
                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Transformação/ Produto" FieldName="TransformacaoProduto"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="2" Width="100%">
                                                                                <PropertiesTextEdit MaxLength="255">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="3" Visible="True" VisibleIndex="1" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Meta: de" FieldName="ValorInicialTransformacao"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g" MaxValue="9999999999999.99"
                                                                                    MinValue="-9999999999999.99" NumberFormat="Custom">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
                                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                                            <dxwgv:GridViewDataSpinEditColumn Caption="para" FieldName="ValorFinalTransformacao"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                                <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="g" MaxValue="9999999999999.99"
                                                                                    MinValue="-9999999999999.99" NumberFormat="Custom">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="3" />
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
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="3" Visible="True" VisibleIndex="0" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Nome Indicador" FieldName="NomeIndicador"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Prazo: até" FieldName="DataLimitePrevista"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="4" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequência" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="7">
                                                                                <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel48" runat="server" Font-Bold="True"
                                                                                    Text="Justificativa">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountJustificativa" runat="server" ClientInstanceName="lblCountJustificativa"
                                                                                     ForeColor="Silver" Text="0 de 0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda19" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
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
                                                                         Rows="20" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountJustificativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel45" runat="server" Font-Bold="True"
                                                                                    Text="Escopo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                                <dxe:ASPxLabel ID="lblCountEscopoIniciativa" runat="server" ClientInstanceName="lblCountEscopoIniciativa"
                                                                                     ForeColor="Silver" Text="0 de 0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Maneira como se define os limites de um projeto. Caracteriza-se pela definição do trabalho que será realizado, estabelecendo os produtos de cada etapa e as atividades necessárias para sua execução e, também, o que &quot;não será feito&quot; dentro da abrangência, eliminando qualquer expectativa não prevista dos clientes e stakeholders."
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
                                                                         Rows="20" Width="100%">
                                                                        <ClientSideEvents Init="function(s, e) {
	var labelCount = lblCountEscopoIniciativa;
	var maxLength = 8000;
	labelCount.SetText(s.GetText().length + ' de ' + maxLength);
	return setMaxLength(s.GetInputElement(), maxLength, labelCount);
}" ValueChanged="function(s, e) {
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
                            <dxtc:TabPage Name="tabAlinhamentoEstrategico" Text="Alinhamento Estratégico">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlAlinhamentoEstrategico" runat="server" Width="100%" meta:resourcekey="pnlAlinhamentoEstrategicoResource1">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" meta:resourcekey="PanelContentResource2">
                                                    <div id="dv03" runat="server">
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
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Objetivo estratégico em que o projeto se enquadra e o indicador atrelado ao objetivo estratégico. Pode ser mais de um."
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
                                                                                Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                <HeaderTemplate>
                                                                                    <% =ObtemBotaoInclusaoRegistro("gvEstrategico", "Plano Estratégico")%>
                                                                                </HeaderTemplate>
                                                                            </dxwgv:GridViewCommandColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Objetivo Estratégico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" FieldName="DescricaoObjetoEstrategia">
                                                                                <EditFormSettings Visible="False" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Indicador Estratégico" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" FieldName="Meta">
                                                                                <EditFormSettings Visible="False" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="3" FieldName="CodigoObjetoEstrategia">
                                                                                <PropertiesComboBox DataSourceID="sdsObjetivoEstrategico" TextField="Descricao" ValueField="Codigo"
                                                                                    ValueType="System.Int32" TextFormatString="{1} ({0})" IncrementalFilteringMode="Contains">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="TituloMapaEstrategico" Width="200px" Caption="Mapa Estratégico" />
                                                                                        <dxe:ListBoxColumn Caption="Objetivo Estratégico" FieldName="Objetivo" Width="100%" />
                                                                                    </Columns>
                                                                                    <ItemStyle Wrap="True"></ItemStyle>
                                                                                    <ListBoxStyle Wrap="True">
                                                                                    </ListBoxStyle>
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	cmbMetaRelacionada.PerformCallback(&quot;&quot;);
}" />
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                        <RequiredField ErrorText="Informe um valor válido para o campo" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Indicador Estratégico" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4" FieldName="CodigoIndicador">
                                                                                <PropertiesComboBox DataSourceID="sdsIndicadorEstrategico" TextField="Meta" ValueField="CodigoIndicador"
                                                                                    ValueType="System.Int32" ClientInstanceName="cmbMetaRelacionada" IncrementalFilteringMode="Contains">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Meta" Width="100%" Caption="Indicador" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetSelectedIndex(-1);
}" />
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings Visible="True" CaptionLocation="Top" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo" ShowInCustomizationForm="True" Visible="False"
                                                                                VisibleIndex="5" FieldName="SequenciaObjetivo">
                                                                                <EditFormSettings Visible="False" />
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
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
                            <dxtc:TabPage Name="tabElementosOperacionais" Text="Elementos Operacionais">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlElementosOperacionais" runat="server" Width="100%" ScrollBars="Auto">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                    <div id="dv04" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True"
                                                                                    Text="Ações">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage8" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
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
                                                                        DataSourceID="sdsAcoes"  KeyFieldName="SequenciaAcao"
                                                                        OnCellEditorInitialize="gvAcoes_CellEditorInitialize" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        OnRowDeleted="gvAcoes_RowDeleted" OnRowInserted="grid_RowInserted" OnRowInserting="gvAcoes_RowInserting"
                                                                        OnRowUpdated="grid_RowUpdated" OnRowUpdating="gvAcoes_RowUpdating" Width="100%">
                                                                        <Columns>
                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
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
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="0" />
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
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" ColumnSpan="2" VisibleIndex="1" />
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
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="2" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataDateColumn Caption="Data de Término" FieldName="Termino" ShowInCustomizationForm="True"
                                                                                VisibleIndex="5" Width="100px">
                                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesDateEdit>
                                                                                <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="3" />
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Codigo da Ação" FieldName="SequenciaAcao"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
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
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True"
                                                                                    Text="Marcos Críticos">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="ASPxImage9" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                                    Height="18px" ImageUrl="~/imagens/ajuda.png" ToolTip="Eventos cuja superação significa um avanço significativo na realização da ação"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView ID="gvMarcosCriticos" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvMarcosCriticos" DataSourceID="sdsMarcosCriticos"
                                                                        KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                                        OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" Width="100%">
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
                                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição do marco crítico" FieldName="NomeMarco"
                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
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
                                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Ação Vinculada" FieldName="SequenciaAcao"
                                                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                                                <PropertiesComboBox DataSourceID="sdsAcoes" IncrementalFilteringMode="Contains" TextField="NomeAcao"
                                                                                    ValueField="SequenciaAcao" ValueType="System.Int32">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Ação" FieldName="NomeAcao" />
                                                                                    </Columns>
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesComboBox>
                                                                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="0" />
                                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                                            <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                                Visible="False" VisibleIndex="4">
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
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabEquipeComunicacao" Text="Equipe e Comunicação">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div id="dv05" runat="server">
                                            <table width="94%">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Font-Bold="True"
                                                                        Text="Equipe">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="imgAjuda9" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvEquipe" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEquipe"
                                                            DataSourceID="sdsEquipe"  KeyFieldName="SequenciaRegistro"
                                                            OnCommandButtonInitialize="grid_CommandButtonInitialize" OnRowInserted="grid_RowInserted"
                                                            OnRowUpdated="grid_RowUpdated" Width="100%">
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvEquipe", "Equipe")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Executor" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" FieldName="IndicaExecutor" Width="150px">
                                                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="DR - Unidade Operacional" Value="DR" />
                                                                            <dxe:ListEditItem Text="UO - Unidade Operacional" Value="UO" />
                                                                            <dxe:ListEditItem Text="EP - Empresa" Value="EP" />
                                                                            <dxe:ListEditItem Text="OP - Outro Parceiro" Value="OP" />
                                                                        </Items>
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Nome" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                    FieldName="Nome">
                                                                    <PropertiesTextEdit MaxLength="100">
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Cargo" FieldName="IndicaCargo" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Width="150px">
                                                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Advogado" Value="Adv" />
                                                                            <dxe:ListEditItem Text="Analista" Value="Anl" />
                                                                            <dxe:ListEditItem Text="Arquiteto" Value="Arq" />
                                                                            <dxe:ListEditItem Text="Assessor" Value="Ass" />
                                                                            <dxe:ListEditItem Text="Assistente" Value="Ast" />
                                                                            <dxe:ListEditItem Text="Auditor" Value="Aud" />
                                                                            <dxe:ListEditItem Text="Auxiliar" Value="Aux" />
                                                                            <dxe:ListEditItem Text="Consultor" Value="Cns" />
                                                                            <dxe:ListEditItem Text="Coordenador" Value="Crd" />
                                                                            <dxe:ListEditItem Text="Designer" Value="Dsg" />
                                                                            <dxe:ListEditItem Text="Diretor de Faculdade" Value="DF" />
                                                                            <dxe:ListEditItem Text="Diretor Escolar" Value="DE" />
                                                                            <dxe:ListEditItem Text="Enfermeiro" Value="Enf" />
                                                                            <dxe:ListEditItem Text="Engenheiro" Value="Eng" />
                                                                            <dxe:ListEditItem Text="Ergonomista" Value="Erg" />
                                                                            <dxe:ListEditItem Text="Especialista" Value="Esp" />
                                                                            <dxe:ListEditItem Text="Estatístico" Value="Est" />
                                                                            <dxe:ListEditItem Text="Examinador" Value="Exm" />
                                                                            <dxe:ListEditItem Text="Farmacêutico" Value="Far" />
                                                                            <dxe:ListEditItem Text="Fisioterapeuta" Value="Fis" />
                                                                            <dxe:ListEditItem Text="Gerente" Value="Ger" />
                                                                            <dxe:ListEditItem Text="Gerente de Negócio" Value="GN" />
                                                                            <dxe:ListEditItem Text="Gerente de Unidade" Value="GU" />
                                                                            <dxe:ListEditItem Text="Higienista" Value="Hig" />
                                                                            <dxe:ListEditItem Text="Instrutor" Value="Ins" />
                                                                            <dxe:ListEditItem Text="Líder de Suporte ao Negócio" Value="LSN" />
                                                                            <dxe:ListEditItem Text="Líder Técnico" Value="LT" />
                                                                            <dxe:ListEditItem Text="Médico" Value="Med" />
                                                                            <dxe:ListEditItem Text="Motorista" Value="Mot" />
                                                                            <dxe:ListEditItem Text="Nutricionista" Value="Nut" />
                                                                            <dxe:ListEditItem Text="Pedagogo" Value="Ped" />
                                                                            <dxe:ListEditItem Text="Produtor" Value="Prd" />
                                                                            <dxe:ListEditItem Text="Professor" Value="Pro" />
                                                                            <dxe:ListEditItem Text="Programador Multimidia" Value="Prg" />
                                                                            <dxe:ListEditItem Text="Psicólogo " Value="Psc" />
                                                                            <dxe:ListEditItem Text="Psicopedagogo" Value="Psg" />
                                                                            <dxe:ListEditItem Text="Roteirista" Value="Rot" />
                                                                            <dxe:ListEditItem Text="Superintendente" Value="Sup" />
                                                                            <dxe:ListEditItem Text="Técnico" Value="Tec" />
                                                                            <dxe:ListEditItem Text="Vice Diretor Escolar" Value="VDE" />
                                                                            <dxe:ListEditItem Text="Webdesigner" Value="Wds" />
                                                                        </Items>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Financiador" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" FieldName="IndicaFinanciador" Width="180px">
                                                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="DN - Departamento Regional" Value="DR" />
                                                                            <dxe:ListEditItem Text="DR - Unidade Operacional" Value="UO" />
                                                                            <dxe:ListEditItem Text="EP - Empresa" Value="EP" />
                                                                            <dxe:ListEditItem Text="OP - Outro Parceiro" Value="OP" />
                                                                        </Items>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Valor Hora" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" FieldName="ValorHora" Width="75px">
                                                                    <PropertiesTextEdit DisplayFormatString="{0:c}">
                                                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="$&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                                                        <Style HorizontalAlign="Right">
                                                                            
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="False" VisibleIndex="6" FieldName="SequenciaRegistro"
                                                                    Visible="False">
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
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" PopupEditFormCaption="Integrante da Equipe" />
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Font-Bold="True"
                                                                        Text="Plano e Comunicação">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="imgAjuda11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvPlanoComunicacao" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvPlanoComunicacao" DataSourceID="sdsPlanoComunicacao"
                                                            KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                            OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" Width="100%">
                                                            <Columns>
                                                                <dxwgv:GridViewDataTextColumn Caption="Parte interessada" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" FieldName="ParteInteressada" Width="150px">
                                                                    <PropertiesTextEdit MaxLength="100">
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Informação" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1" FieldName="Informacao">
                                                                    <PropertiesTextEdit MaxLength="100">
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" FieldName="Responsavel" Width="150px">
                                                                    <PropertiesTextEdit MaxLength="100">
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Meio" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                    FieldName="Meio" Width="150px">
                                                                    <PropertiesTextEdit MaxLength="50">
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Frequência" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" FieldName="Frequencia" Width="75px">
                                                                    <PropertiesTextEdit MaxLength="50">
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvPlanoComunicacao", "Plano e Comunicação")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="6">
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
                            <dxtc:TabPage Name="tabRiscos" Text="Riscos e Aquisições">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div id="dv06" runat="server">
                                            <table width="94%">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="True"
                                                                        Text="Risco">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage6" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvRiscos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvRiscos"
                                                             KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                            OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" Width="100%"
                                                            DataSourceID="sdsRiscos" OnInitNewRow="gvRiscos_InitNewRow">
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvRiscos", "Risco")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Risco" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                    FieldName="Risco">
                                                                    <PropertiesTextEdit MaxLength="250">
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" VisibleIndex="0" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataMemoColumn Caption="Descrição" FieldName="Descricao" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="4" RowSpan="2" VisibleIndex="2" />
                                                                </dxwgv:GridViewDataMemoColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Estratégia" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" FieldName="Estrategia" Width="75px">
                                                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Explorar" Value="EX" />
                                                                            <dxe:ListEditItem Text="Compartilhar" Value="CO" />
                                                                            <dxe:ListEditItem Text="Melhorar" Value="ME" />
                                                                            <dxe:ListEditItem Text="Aceitar" Value="AC" />
                                                                            <dxe:ListEditItem Text="Evitar" Value="EV" />
                                                                            <dxe:ListEditItem Text="Mitigar" Value="MI" />
                                                                            <dxe:ListEditItem Text="Transferir" Value="TR" />
                                                                        </Items>
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" VisibleIndex="1" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Probabilidade de ocorrência" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" FieldName="Probabilidade" Width="75px">
                                                                    <PropertiesComboBox ValueType="System.Byte" IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="10 %" Value="10" />
                                                                            <dxe:ListEditItem Text="25 %" Value="25" />
                                                                            <dxe:ListEditItem Text="50 %" Value="50" />
                                                                            <dxe:ListEditItem Text="75 %" Value="75" />
                                                                        </Items>
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="3" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Impacto no projeto" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" FieldName="Impacto" Width="75px">
                                                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Muito baixo" Value="MB" />
                                                                            <dxe:ListEditItem Text="Baixo" Value="BA" />
                                                                            <dxe:ListEditItem Text="Moderado" Value="MO" />
                                                                            <dxe:ListEditItem Text="Alto" Value="AL" />
                                                                            <dxe:ListEditItem Text="Muito Alto" Value="MA" />
                                                                        </Items>
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="4" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                <dxwgv:GridViewBandColumn Caption="Área de impacto" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6">
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataCheckColumn Caption="Financeiro" ShowInCustomizationForm="True"
                                                                            VisibleIndex="0" FieldName="IndicaImpactoFinanceiro">
                                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                            </PropertiesCheckEdit>
                                                                            <EditFormSettings CaptionLocation="Near" VisibleIndex="5" />
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn Caption="Prazo" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                            FieldName="IndicaImpactoPrazo">
                                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                            </PropertiesCheckEdit>
                                                                            <EditFormSettings CaptionLocation="Near" VisibleIndex="6" />
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn Caption="Qualidade" ShowInCustomizationForm="True"
                                                                            VisibleIndex="2" FieldName="IndicaImpactoQualidade">
                                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                            </PropertiesCheckEdit>
                                                                            <EditFormSettings CaptionLocation="Near" VisibleIndex="7" />
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                        <dxwgv:GridViewDataCheckColumn Caption="Escopo" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                            FieldName="IndicaImpactoEscopo">
                                                                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                            </PropertiesCheckEdit>
                                                                            <EditFormSettings CaptionLocation="Near" VisibleIndex="8" />
                                                                        </dxwgv:GridViewDataCheckColumn>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dxwgv:GridViewBandColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="4" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                    AllowResize="True" Width="600px" />
                                                            </SettingsPopup>
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                            <Styles>
                                                                <Header Wrap="True">
                                                                </Header>
                                                            </Styles>
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="True"
                                                                        Text="Aquisições">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage7" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvAquisicoes" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvAquisicoes" DataSourceID="sdsAquisicoes"
                                                            KeyFieldName="SequenciaRegistro" OnCommandButtonInitialize="grid_CommandButtonInitialize"
                                                            OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" Width="100%">
                                                            <Columns>
                                                                <dxwgv:GridViewDataDateColumn Caption="Data prevista" FieldName="DataPrevista" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" Width="80px">
                                                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                    </PropertiesDateEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataDateColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Contratação/Aquisição" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1" FieldName="Item">
                                                                    <PropertiesTextEdit MaxLength="250">
                                                                        <ValidationSettings Display="Dynamic">
                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Valor previsto" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" FieldName="ValorPrevisto" Width="105px">
                                                                    <PropertiesTextEdit MaxLength="100" DisplayFormatString="{0:C}">
                                                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="$ &lt;0..999999999g&gt;.&lt;00..99&gt;" />
                                                                        <Style HorizontalAlign="Right">
                                                                            
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataDateColumn Caption="Data realizada" FieldName="DataReal" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="80px">
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                                    </PropertiesDateEdit>
                                                                </dxwgv:GridViewDataDateColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Valor Realizado" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" FieldName="ValorReal" Width="105px">
                                                                    <PropertiesTextEdit MaxLength="50" DisplayFormatString="{0:C}">
                                                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="$ &lt;0..999999999g&gt;.&lt;00..99&gt;" />
                                                                        <Style HorizontalAlign="Right">
                                                                            
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6" FieldName="Reponsavel">
                                                                    <PropertiesTextEdit MaxLength="100">
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <% =ObtemBotaoInclusaoRegistro("gvAquisicoes", "Aquisições")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
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
                            <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div id="dv07" runat="server">
                                            <iframe frameborder="0" name="frmBiblioteca" scrolling="no" src="../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=TA&ID=<%=CodigoProjeto %>&RO=<%=IndicaNovoProjeto? "S": "N" %>"
                                                marginheight="0" style="margin: 0; height: 700px;" width="94%" id="frmBiblioteca">
                                            </iframe>
                                        </div>
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
                        Text="Salvar" AutoPostBack="False" meta:resourcekey="btnSalvarResource1" Width="100px">
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
    <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT * FROM [TermoAbertura03] WHERE [CodigoProjeto] = @CodigoProjeto"
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
    <asp:SqlDataSource ID="sdsEstrategico" runat="server" SelectCommand="SELECT [SequenciaObjetivo]
      ,[CodigoObjetoEstrategia]
      ,[DescricaoObjetoEstrategia]
      ,[CodigoIndicador]
      ,[Meta]
  FROM [tai03_ObjetivosEstrategicos]
  WHERE  [CodigoProjeto] = @CodigoProjeto
   ORDER BY DescricaoObjetoEstrategia" InsertCommand="INSERT INTO tai03_ObjetivosEstrategicos
           ([CodigoProjeto]
      ,[SequenciaObjetivo]
      ,[CodigoObjetoEstrategia]
      ,[DescricaoObjetoEstrategia]
      ,[CodigoIndicador]
      ,[Meta])
     SELECT @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaObjetivo),0) + 1 FROM tai03_ObjetivosEstrategicos WHERE CodigoProjeto = @CodigoProjeto )
           ,@CodigoObjetoEstrategia
           ,@DescricaoObjetoEstrategia
           ,@CodigoIndicador
           ,@Meta" UpdateCommand="UPDATE [tai03_ObjetivosEstrategicos]
   SET [CodigoObjetoEstrategia] = @CodigoObjetoEstrategia
      ,[DescricaoObjetoEstrategia] = @DescricaoObjetoEstrategia
      ,[CodigoIndicador] = @CodigoIndicador
      ,[Meta] = @Meta
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaObjetivo = @SequenciaObjetivo
" DeleteCommand="DELETE FROM tai03_ObjetivosEstrategicos
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
        oe.DescricaoObjetoEstrategia + ' (' + me.TituloMapaEstrategico + ')' AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior,
        oes.TituloObjetoEstrategia DescricaoSuperior,
        oe.DescricaoObjetoEstrategia AS Objetivo,
        me.TituloMapaEstrategico AS TituloMapaEstrategico
   FROM ObjetoEstrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio =me.CodigoUnidadeNegocio 
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = @CodigoEntidade
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL
    AND ( oe.CodigoobjetoEstrategia = @CodigoObjetivoAtual OR NOT EXISTS (SELECT 1 FROM tai03_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoObjetoEstrategia = oe.CodigoobjetoEstrategia ) )
  ORDER BY 2">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoObjetivoAtual" />
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
    AND (i.[CodigoIndicador] = @CodigoIndicadorAtual OR NOT EXISTS (SELECT 1 FROM tai03_ObjetivosEstrategicos t WHERE t.CodigoProjeto = @CodigoProjeto AND t.CodigoIndicador = i.CodigoIndicador AND t.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico) )
  ORDER BY 2" OnFiltering="sqlDataSource_Filtering" FilterExpression="CodigoSuperior = {0}">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="codigoEntidade" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="CodigoIndicadorAtual" />
        </SelectParameters>
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
    <asp:SqlDataSource ID="sdsResultados" runat="server" SelectCommand="SELECT * FROM dbo.tai03_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY SetencaResultado"
        DeleteCommand="DELETE FROM tai03_ResultadosIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro] = @SequenciaRegistro" InsertCommand="INSERT INTO tai03_ResultadosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[TransformacaoProduto]
           ,[CodigoIndicador]
           ,[NomeIndicador]
           ,[ValorInicialTransformacao]
           ,[ValorFinalTransformacao]
           ,[DataLimitePrevista]
           ,[SetencaResultado])
        SELECT
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_ResultadosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@TransformacaoProduto
           ,@CodigoIndicador
           ,@NomeIndicador
           ,@ValorInicialTransformacao
           ,@ValorFinalTransformacao
           ,@DataLimitePrevista
           ,@SetencaResultado" UpdateCommand="UPDATE tai03_ResultadosIniciativa
   SET [TransformacaoProduto] = @TransformacaoProduto
      ,[CodigoIndicador] = @CodigoIndicador
      ,[NomeIndicador] = @NomeIndicador
      ,[ValorInicialTransformacao] = @ValorInicialTransformacao
      ,[ValorFinalTransformacao] = @ValorFinalTransformacao
      ,[DataLimitePrevista] = @DataLimitePrevista
      ,[SetencaResultado] = @SetencaResultado
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
            <asp:Parameter Name="ValorInicialTransformacao" />
            <asp:Parameter Name="ValorFinalTransformacao" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SetencaResultado" />
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
            <asp:Parameter Name="ValorInicialTransformacao" />
            <asp:Parameter Name="ValorFinalTransformacao" />
            <asp:Parameter Name="DataLimitePrevista" />
            <asp:Parameter Name="SetencaResultado" />
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
    <asp:SqlDataSource ID="sdsParceiros" runat="server" DeleteCommand="DELETE FROM [tai03_ParceirosIniciativa]
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="  INSERT INTO [tai03_ParceirosIniciativa]
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeParceiro]
           ,[NomeInterlocutor]
           ,[Email]
           ,[NumeroTelefone])
      SELECT @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_ParceirosIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeParceiro
           ,@NomeInterlocutor
           ,@Email
           ,@NumeroTelefone" SelectCommand="SELECT [SequenciaRegistro]
      ,[NomeParceiro]
      ,[NomeInterlocutor]
      ,[Email]
      ,[NumeroTelefone]
  FROM [tai03_ParceirosIniciativa]
WHERE CodigoProjeto = @CodigoProjeto" UpdateCommand="UPDATE [tai03_ParceirosIniciativa]
   SET [NomeParceiro] = @NomeParceiro
      ,[NomeInterlocutor] = @NomeInterlocutor
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
            <asp:Parameter Name="NomeParceiro" />
            <asp:Parameter Name="NomeInterlocutor" />
            <asp:Parameter Name="Email" />
            <asp:Parameter Name="NumeroTelefone" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeParceiro" />
            <asp:Parameter Name="NomeInterlocutor" />
            <asp:Parameter Name="Email" />
            <asp:Parameter Name="NumeroTelefone" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsEquipe" runat="server" SelectCommand="SELECT * FROM tai03_EquipeIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY Nome"
        DeleteCommand="DELETE FROM tai03_EquipeIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaRegistro]= @SequenciaRegistro" InsertCommand="INSERT INTO tai03_EquipeIniciativa
([CodigoProjeto], [SequenciaRegistro], [Nome], [IndicaExecutor], [IndicaCargo], [IndicaFinanciador], [ValorHora])
SELECT @CodigoProjeto,
(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_EquipeIniciativa WHERE CodigoProjeto = @CodigoProjeto),
@Nome,
@IndicaExecutor,
@IndicaCargo,
@IndicaFinanciador,
@ValorHora" UpdateCommand="UPDATE tai03_EquipeIniciativa
   SET [Nome] = @Nome
      ,[IndicaExecutor] = @IndicaExecutor
      ,[IndicaCargo] = @IndicaCargo
      ,[IndicaFinanciador] = @IndicaFinanciador
      ,[ValorHora] = @ValorHora
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="Nome" />
            <asp:Parameter Name="IndicaExecutor" />
            <asp:Parameter Name="IndicaCargo" />
            <asp:Parameter Name="IndicaFinanciador" />
            <asp:Parameter Name="ValorHora" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
            <asp:Parameter Name="Nome" />
            <asp:Parameter Name="IndicaExecutor" />
            <asp:Parameter Name="IndicaCargo" />
            <asp:Parameter Name="IndicaFinanciador" />
            <asp:Parameter Name="ValorHora" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPlanoComunicacao" runat="server" SelectCommand="SELECT * FROM tai03_PlanoComunicacao WHERE CodigoProjeto = @CodigoProjeto ORDER BY Informacao"
        DeleteCommand="DELETE FROM tai03_PlanoComunicacao
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai03_PlanoComunicacao
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[ParteInteressada]
           ,[Informacao]
           ,[Responsavel]
           ,[Meio]
           ,[Frequencia])
     SELECT 
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_PlanoComunicacao WHERE CodigoProjeto = @CodigoProjeto )
           ,@ParteInteressada
           ,@Informacao
           ,@Responsavel
           ,@Meio
           ,@Frequencia" UpdateCommand="UPDATE tai03_PlanoComunicacao
   SET [ParteInteressada] = @ParteInteressada
      ,[Informacao] = @Informacao
      ,[Responsavel] = @Responsavel
      ,[Meio] = @Meio
      ,[Frequencia] = @Frequencia
 WHERE CodigoProjeto = @CodigoProjeto
   AND SequenciaRegistro = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="ParteInteressada" />
            <asp:Parameter Name="Informacao" />
            <asp:Parameter Name="Responsavel" />
            <asp:Parameter Name="Meio" />
            <asp:Parameter Name="Frequencia" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="ParteInteressada" />
            <asp:Parameter Name="Informacao" />
            <asp:Parameter Name="Responsavel" />
            <asp:Parameter Name="Meio" />
            <asp:Parameter Name="Frequencia" />
            <asp:Parameter Name="SequenciaRegistro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAcoes" runat="server" SelectCommand="SELECT * FROM tai03_AcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeAcao"
        DeleteCommand="DELETE FROM tai03_AcoesIniciativa
      WHERE [CodigoProjeto] = @CodigoProjeto
        AND [SequenciaAcao] = @SequenciaAcao" InsertCommand="INSERT INTO tai03_AcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaAcao]
           ,[NomeAcao]
           ,[CodigoUsuarioResponsavel]
           ,[NomeUsuarioResponsavel]
           ,[Inicio]
           ,[Termino])
     SELECT 
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaAcao),0) + 1 FROM tai03_AcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeAcao
           ,@CodigoUsuarioResponsavel
           ,@NomeUsuarioResponsavel
           ,@Inicio
           ,@Termino" UpdateCommand="UPDATE tai03_AcoesIniciativa
   SET [NomeAcao] = @NomeAcao
      ,[CodigoUsuarioResponsavel] = @CodigoUsuarioResponsavel
      ,[NomeUsuarioResponsavel] = @NomeUsuarioResponsavel
      ,[Inicio] = @Inicio
      ,[Termino] = @Termino
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaAcao] = @SequenciaAcao">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaAcao" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="NomeAcao" />
            <asp:Parameter Name="CodigoUsuarioResponsavel" />
            <asp:Parameter Name="NomeUsuarioResponsavel" />
            <asp:Parameter Name="Inicio" />
            <asp:Parameter Name="Termino" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaAcao" />
            <asp:Parameter Name="NomeAcao" />
            <asp:Parameter Name="CodigoUsuarioResponsavel" />
            <asp:Parameter Name="NomeUsuarioResponsavel" />
            <asp:Parameter Name="Inicio" />
            <asp:Parameter Name="Termino" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsMarcosCriticos" runat="server" SelectCommand="SELECT * FROM tai03_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY NomeMarco"
        DeleteCommand="DELETE FROM tai03_MarcosAcoesIniciativa
      WHERE CodigoProjeto = @CodigoProjeto
        AND SequenciaRegistro = @SequenciaRegistro" InsertCommand="INSERT INTO tai03_MarcosAcoesIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[NomeMarco]
           ,[DataLimitePrevista]
           ,[SequenciaAcao])
     SELECT 
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_MarcosAcoesIniciativa WHERE CodigoProjeto = @CodigoProjeto )
           ,@NomeMarco
           ,@DataLimitePrevista
           ,@SequenciaAcao" UpdateCommand="UPDATE tai03_MarcosAcoesIniciativa
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
    <asp:SqlDataSource ID="sdsResponsaveisAcao" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsRiscos" runat="server" DeleteCommand="DELETE FROM tai03_RiscosIniciativa WHERE CodigoProjeto = @CodigoProjeto AND SequenciaRegistro = @SequenciaRegistro"
        InsertCommand="INSERT INTO tai03_RiscosIniciativa
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[Risco]
           ,[Descricao]
           ,[Estrategia]
           ,[Probabilidade]
           ,[Impacto]
           ,[IndicaImpactoFinanceiro]
           ,[IndicaImpactoPrazo]
           ,[IndicaImpactoQualidade]
           ,[IndicaImpactoEscopo])
     SELECT
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM tai03_RiscosIniciativa WHERE CodigoProjeto = @CodigoProjeto)
           ,@Risco
           ,@Descricao
           ,@Estrategia
           ,@Probabilidade
           ,@Impacto
           ,@IndicaImpactoFinanceiro
           ,@IndicaImpactoPrazo
           ,@IndicaImpactoQualidade
           ,@IndicaImpactoEscopo" SelectCommand="SELECT * FROM tai03_RiscosIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY Risco"
        UpdateCommand="UPDATE [tai03_RiscosIniciativa]
   SET [Risco] = @Risco
      ,[Descricao] = @Descricao
      ,[Estrategia] = @Estrategia
      ,[Probabilidade] = @Probabilidade
      ,[Impacto] = @Impacto
      ,[IndicaImpactoFinanceiro] = @IndicaImpactoFinanceiro
      ,[IndicaImpactoPrazo] = @IndicaImpactoPrazo
      ,[IndicaImpactoQualidade] = @IndicaImpactoQualidade
      ,[IndicaImpactoEscopo] = @IndicaImpactoEscopo
 WHERE [CodigoProjeto] = @CodigoProjeto
   AND [SequenciaRegistro] = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter DefaultValue="" Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter DefaultValue="" Name="Risco" />
            <asp:Parameter Name="Descricao" />
            <asp:Parameter Name="Estrategia" />
            <asp:Parameter Name="Probabilidade" />
            <asp:Parameter Name="Impacto" />
            <asp:Parameter Name="IndicaImpactoFinanceiro" />
            <asp:Parameter Name="IndicaImpactoPrazo" />
            <asp:Parameter Name="IndicaImpactoQualidade" />
            <asp:Parameter Name="IndicaImpactoEscopo" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="Risco" />
            <asp:Parameter Name="Descricao" />
            <asp:Parameter Name="Estrategia" />
            <asp:Parameter Name="Probabilidade" />
            <asp:Parameter Name="Impacto" />
            <asp:Parameter Name="IndicaImpactoFinanceiro" />
            <asp:Parameter Name="IndicaImpactoPrazo" />
            <asp:Parameter Name="IndicaImpactoQualidade" />
            <asp:Parameter Name="IndicaImpactoEscopo" />
            <asp:Parameter Name="SequenciaRegistro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAquisicoes" runat="server" DeleteCommand="DELETE FROM tai03_AquisicoesIniciativa WHERE CodigoProjeto = @CodigoProjeto AND SequenciaRegistro = @SequenciaRegistro"
        InsertCommand="INSERT INTO [tai03_AquisicoesIniciativa]
           ([CodigoProjeto]
           ,[SequenciaRegistro]
           ,[Item]
           ,[DataPrevista]
           ,[ValorPrevisto]
           ,[DataReal]
           ,[ValorReal]
           ,[Reponsavel])
     SELECT
           @CodigoProjeto
           ,(SELECT ISNULL(MAX(SequenciaRegistro),0) + 1 FROM [tai03_AquisicoesIniciativa] WHERE CodigoProjeto = @CodigoProjeto)
           ,@Item
           ,@DataPrevista
           ,@ValorPrevisto
           ,@DataReal
           ,@ValorReal
           ,@Reponsavel" SelectCommand="SELECT * FROM tai03_AquisicoesIniciativa WHERE CodigoProjeto = @CodigoProjeto ORDER BY Item"
        UpdateCommand="UPDATE dbo.[tai03_AquisicoesIniciativa]
   SET [Item] = @Item
      ,[DataPrevista] = @DataPrevista
      ,[ValorPrevisto] = @ValorPrevisto
      ,[DataReal] = @DataReal
      ,[ValorReal] = @ValorReal
      ,[Reponsavel] = @Reponsavel
 WHERE CodigoProjeto = @CodigoProjeto 
   AND SequenciaRegistro = @SequenciaRegistro">
        <DeleteParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="SequenciaRegistro" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="Item" />
            <asp:Parameter Name="DataPrevista" />
            <asp:Parameter Name="ValorPrevisto" />
            <asp:Parameter Name="DataReal" />
            <asp:Parameter Name="ValorReal" />
            <asp:Parameter Name="Reponsavel" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
            <asp:Parameter Name="Item" />
            <asp:Parameter Name="DataPrevista" />
            <asp:Parameter Name="ValorPrevisto" />
            <asp:Parameter Name="DataReal" />
            <asp:Parameter Name="ValorReal" />
            <asp:Parameter Name="Reponsavel" />
            <asp:Parameter Name="SequenciaRegistro" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
    verificaGravacaoInstanciaWf(); 
	ProcessaResultadoCallback(s, e);
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
