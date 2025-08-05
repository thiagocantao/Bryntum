<%@ Page Language="C#" AutoEventWireup="true" CodeFile="propostaDeIniciativa_003.aspx.cs"
    Inherits="_Projetos_Administracao_propostaDeIniciativa_003" meta:resourcekey="PageResource5" %>

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
                    window.location = "./propostaDeIniciativa_003.aspx?CP=" + result.substring(1) + "&tab=" + activeTabIndex;
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
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="3" ClientInstanceName="pageControl"
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
                                                                <td>
                                                                    &nbsp;
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
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
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
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
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
                                                                        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm"/>
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
                                        <dxp:ASPxPanel ID="pnlAlinhamentoEstrategico" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
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
                                                                                Width="50px" ShowEditButton="true" ShowDeleteButton="true">
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
                                                <dxp:PanelContent runat="server">
                                                    <div id="dv04" runat="server">
                                                        <table width="94%">
                                                            <tr>
                                                                <td>
                                                                    <table>
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
                                                                        <SettingsEditing Mode="PopupEditForm"/>
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
                                                                                <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Font-Bold="True"
                                                                                    Text="Marcos Críticos">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
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
        oe.DescricaoObjetoEstrategia AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior,
        oes.TituloObjetoEstrategia DescricaoSuperior
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
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
    verificaGravacaoInstanciaWf(); 
	ProcessaResultadoCallback(s, e);
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
