<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WizardDefinicaoLista.aspx.cs" Inherits="_Processos_Visualizacao_WizardDefinicaoLista" %>

<%@ Register Assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .titulo {
            font-weight: bolder;
        }

        .subtitulo {
            font-weight: bold;
            padding-bottom: 10px;
            font-size: 12px;
            color: #a8a8a8;
        }

        .largura100perc {
            width: 100%;
        }

        table.tabela-botoes {
            margin-left: auto;
        }

            table.tabela-botoes td {
                padding: 2.5px 10px;
            }
     .box-text{
         background:#f5f5f5;
         padding:10px;
         border-radius:4px;
         width: 99%;
     }
     .btn{
         text-transform:capitalize !important;
     }
    </style>
    <script type="text/javascript">
        var posicaoAtual = 0;
        var caminhoSelecionado = [];
        var caminhosEtapas = {
            basico: [
                { tabIndex: 0, podeFinalizar: false },
                { tabIndex: 1, podeFinalizar: false },
                { tabIndex: 2, podeFinalizar: true }
            ],
            consultasGrid: [
                { tabIndex: 0, podeFinalizar: false },
                { tabIndex: 1, podeFinalizar: false },
                { tabIndex: 2, podeFinalizar: false },
                { tabIndex: 3, podeFinalizar: true },
                { tabIndex: 4, podeFinalizar: true },
                { tabIndex: 5, podeFinalizar: true }
            ],
            consultasOutros: [
                { tabIndex: 0, podeFinalizar: false },
                { tabIndex: 1, podeFinalizar: false },
                { tabIndex: 2, podeFinalizar: false },
                { tabIndex: 3, podeFinalizar: true },
                { tabIndex: 5, podeFinalizar: true }
            ],
            relatorios: [
                { tabIndex: 0, podeFinalizar: false },
                { tabIndex: 1, podeFinalizar: false },
                { tabIndex: 2, podeFinalizar: false },
                { tabIndex: 7, podeFinalizar: true }
            ],
            dashboads: [
                { tabIndex: 0, podeFinalizar: false },
                { tabIndex: 1, podeFinalizar: false },
                { tabIndex: 2, podeFinalizar: false },
                { tabIndex: 6, podeFinalizar: true }
            ]
        };

        function retrocedeEtapaAssistente() {
            moveEtapaAssistente(-1);
        }

        function avancaEtapaAssistente() {
            if (validaEtapa())
                moveEtapaAssistente(1);
        }

        function finalizarWizard(s, e) {
            if (validaEtapa())
                callback.PerformCallback('finalizarWizard');
        }

        function validaEtapa() {
            var groupName = tabbedGroupPageControl.GetActiveTab().name;
            var validateResult = ASPxClientEdit.ValidateGroup(groupName);
            return validateResult;
        }

        function moveEtapaAssistente(quantidade) {
            posicaoAtual = posicaoAtual + quantidade;
            var selectedTabIndex = caminhoSelecionado[posicaoAtual].tabIndex;
            tabbedGroupPageControl.SetActiveTabIndex(selectedTabIndex);
            defineBotoesHabilitados();
        }

        function defineBotoesHabilitados() {
            btnAnterior.SetEnabled(posicaoAtual > 0);
            btnProximo.SetEnabled(posicaoAtual < caminhoSelecionado.length - 1);
            btnFinalizar.SetEnabled(caminhoSelecionado[posicaoAtual].podeFinalizar);
        }

        function defineCaminhoSelecionado() {
            var value = cmbTipoLista.GetValue();
            switch (value) {
                case 'RELATORIO':
                    caminhoSelecionado = caminhosEtapas.consultasGrid;
                    break;
                case 'OLAP':
                    caminhoSelecionado = caminhosEtapas.consultasOutros;
                    break;
                case 'ARVORE':
                    caminhoSelecionado = caminhosEtapas.consultasOutros;
                    break;
                case 'PROCESSO':
                    caminhoSelecionado = caminhosEtapas.consultasGrid;
                    break;
                case 'DASHBOARD':
                    caminhoSelecionado = caminhosEtapas.dashboads;
                    break;
                case 'REPORT':
                    caminhoSelecionado = caminhosEtapas.relatorios;
                    break;
                default:
                    caminhoSelecionado = caminhosEtapas.basico;
                    break;
            }
            defineBotoesHabilitados();
        }

        function cmbTipoLista_OnSelectedIndexChanged(s, e) {
            defineCaminhoSelecionado();
        }

        function onSaveCommandExecute(s, e) {
            var msgErro = "";
            if (s.IsQueryValid()) {
                var query = s.GetJsonQueryModel().Query;
                if (query.Tables) {
                    if (query.Columns == undefined) {
                        msgErro = "Nenhuma coluna selecionada";
                    }
                }
            }
            else {
                msgErro = "Não foi possível construir um comando válido a partir das informações selecionadas";
            }
            if (msgErro != "") {
                e.handled = true;
                window.top.mostraMensagem(msgErro, 'erro', true, false, null);
            }
        }

        function txtTitulo_ValueChanged(s, e) {
            var value = s.GetValue();
            txtItemMenu.SetValue(value);
            txtItemPermissao.SetValue(value);
        }

        function btnCancelar_Click(s, e) {
            window.top.mostraMensagem(traducao.WizardDefinicaoLista_deseja_realmente_abandonar_assistente_criacao_relatorio, 'Atencao', true, true, window.top.fechaModal);
        }

        function onCallbackComplete(s, e) {
            switch (e.parameter) {
                case 'finalizarWizard':
                    window.top.retornoModal = {
                        gerarColunas: cmbGerarColunas.GetChecked(),
                        tipoLista: cmbTipoLista.GetValue(),
                        codigoSubLista: cmbCodigoSubLista.GetValue()
                    };
                    if (window.top.retornoModal.codigoSubLista == null)
                        window.top.retornoModal.codigoSubLista = -1;
                    window.top.fechaModal();
                    break;
                default:
                    break;
            }
        }

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="largura100perc">
                <tr>
                    <td>
                        <dxcp:ASPxFormLayout ID="formLayout" runat="server" ClientInstanceName="formLayout" NestedControlWidth="100%" Height="200px" UseDefaultPaddings="False">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" />
                            <Items>
                                <dxtv:TabbedLayoutGroup ClientInstanceName="tabbedGroupPageControl">
                                    <Items>
                                        <dxtv:LayoutGroup Caption="grp1" Name="grp1">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <BorderTop BorderStyle="None" />
                                                    <BorderBottom BorderStyle="None" />
                                                    <Border BorderStyle="None" />
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="formLayout_E6" runat="server" CssClass="subtitulo" Text="<%$ Resources:traducao, WizardDefinicaoLista_iniciando_assistente %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<%$ Resources:traducao, WizardDefinicaoLista_nome %>" FieldName="NomeLista">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" MaxLength="255" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_0">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxTextBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<%$ Resources:traducao, WizardDefinicaoLista_titulo %>" FieldName="TituloLista">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxTextBox ID="txtTitulo" runat="server" MaxLength="150" ClientInstanceName="txtTitulo" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                        <ClientSideEvents ValueChanged="txtTitulo_ValueChanged" />
                                                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_0">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxTextBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<%$ Resources:traducao, WizardDefinicaoLista_modulo %>" FieldName="CodigoModuloMenu">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxComboBox ID="formLayout_E2" runat="server" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                        <Items>
                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, WizardDefinicaoLista_modulo_adm%>" Value="ADM" />
                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, WizardDefinicaoLista_modulo_projetos%>" Value="PRJ" />
                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, WizardDefinicaoLista_modulo_espaco_trabalho%>" Value="ESP" />
                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, WizardDefinicaoLista_modulo_estrategia%>" Value="EST" />
                                                                        </Items>
                                                                        <ValidationSettings Display="Dynamic" ValidationGroup="0_0">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxComboBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="Disponível" FieldName="IndicaOpcaoDisponivel" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxCheckBox ID="ASPxFormLayout1_E6" runat="server" Checked="True" CheckState="Checked" Text="<%$ Resources:traducao, WizardDefinicaoLista_disponivel %>" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Width="100px" Theme="MaterialCompact">
                                                                        <ValidationSettings ValidationGroup="0_0">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxCheckBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem FieldName="unbound_TextoExplicativo" ShowCaption="False" CssClass="box-text">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="formLayout_E4" runat="server" Text="<%$ Resources:traducao, bem_vindo_ao_assistente_para_configura__o_de_relat_rios_e_consultas_din_micas %>"
                                                                        Wrap="True">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp2" Name="grp2">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" CssClass="subtitulo" Text="<% $ Resources:traducao, WizardDefinicaoLista_definindo_opcao_menu_permissoes_ordenacao_opcao_menu %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutGroup Caption="<% $ Resources:traducao, WizardDefinicaoLista_menu %>" ColCount="2">
                                                            <Items>
                                                                <dxtv:LayoutItem Caption="<% $ Resources:traducao, WizardDefinicaoLista_grupo_menu %>" FieldName="GrupoMenu" HelpText="<% $ Resources:traducao, WizardDefinicaoLista_opcao_menu_disponibilizado_relatorio_consulta %>">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" MaxLength="50">
                                                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_1">
                                                                                    <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxTextBox>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                                <dxtv:LayoutItem Caption="<% $ Resources:traducao, WizardDefinicaoLista_item_menu %>" FieldName="ItemMenu" HelpText="<% $ Resources:traducao, WizardDefinicaoLista_nome_opcao_aparecer_usuario_final %>">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxTextBox ID="txtItemMenu" ClientInstanceName="txtItemMenu" runat="server" MaxLength="60">
                                                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_1">
                                                                                    <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxTextBox>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                            </Items>
                                                        </dxtv:LayoutGroup>
                                                        <dxtv:LayoutGroup Caption="<% $ Resources:traducao, WizardDefinicaoLista_permissao %>" ColCount="2">
                                                            <Items>
                                                                <dxtv:LayoutItem Caption="<% $ Resources:traducao, WizardDefinicaoLista_grupo_permissao %>" FieldName="GrupoPermissao" HelpText="<% $ Resources:traducao, WizardDefinicaoLista_defina_local_permissao_disponibilizada %>">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxTextBox ID="ASPxFormLayout1_E7" runat="server" MaxLength="50">
                                                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_1">
                                                                                    <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxTextBox>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                                <dxtv:LayoutItem Caption="<% $ Resources:traducao, WizardDefinicaoLista_item_permissao %>" FieldName="ItemPermissao" HelpText="<% $ Resources:traducao, WizardDefinicaoLocal_defina_nome_permissao %>">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxTextBox ID="txtItemPermissao" ClientInstanceName="txtItemPermissao" runat="server" MaxLength="60">
                                                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_1">
                                                                                    <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxTextBox>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                            </Items>
                                                        </dxtv:LayoutGroup>
                                                     <%--<dxtv:LayoutGroup Caption="Ordem" ColCount="2" ClientVisible="false">
                                                            <Items>
                                                                <dxtv:LayoutItem Caption="Ordem do grupo" FieldName="OrdemGrupoMenu" HelpText="Informe a ordem de apresentação do grupo de menu">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxSpinEdit ID="ASPxFormLayout1_E9" runat="server" MaxValue="32767" MinValue="1">
                                                                                <ValidationSettings ValidationGroup="0_1" Display="Dynamic">
                                                                                    <RequiredField ErrorText="*Campo obrigatório" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                                <dxtv:LayoutItem Caption="Ordem do Item" FieldName="OrdemItemGrupoMenu" HelpText="Informe em que ordem a nova opção deve aparecer dentro do grupo de menu">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxSpinEdit ID="ASPxFormLayout1_E10" runat="server" MaxValue="32767" MinValue="1">
                                                                                <ValidationSettings ValidationGroup="0_1" Display="Dynamic">
                                                                                    <RequiredField ErrorText="*Campo obrigatório" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                            </Items>
                                                        </dxtv:LayoutGroup>--%>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp3" Name="grp3">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" CssClass="subtitulo" Text="<% $ Resources:traducao, WizardDefinicaoLista_definindo_relatorio_consulta %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<% $Resources:traducao, WizardDefinicaoLista_selecione %>" HelpText="<% $Resources:traducao, WizardDefinicaoLista_selecione_help_text_URL_utilizada_relatorio_previamente_construido_CDIS %>">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxRadioButtonList ID="rblUrlOuTipo" runat="server" ClientInstanceName="rblUrlOuTipo" ItemSpacing="25px" RepeatDirection="Horizontal" SelectedIndex="0" Width="300px">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == 'URL'){
		txtUrl.SetEnabled(true);
		cmbTipoLista.SetEnabled(false);
		cmbTipoLista.SetSelectedIndex(-1);
	}
	else{
		txtUrl.SetEnabled(false);
		cmbTipoLista.SetEnabled(true);
		cmbTipoLista.SetSelectedIndex(0);
		txtUrl.SetValue(null);
	}
    defineCaminhoSelecionado();
}" />
                                                                        <Items>
                                                                            <dxtv:ListEditItem Selected="True" Text="URL" Value="URL" />
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_escolher_um_modelo %>" Value="Tipo" />
                                                                        </Items>
                                                                        <ValidationSettings ValidationGroup="0_2">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxRadioButtonList>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="URL" FieldName="URL" HelpText="<% $Resources:traducao, WizardDefinicaoLista_URL_help_text_escolha_opcao_disponibilizada %>">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxTextBox ID="txtUrl" runat="server" ClientInstanceName="txtUrl" MaxLength="500">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(rblUrlOuTipo.GetValue() == 'URL'){
		var url = e.value;
		e.isValid =  !(url == null || url == undefined || url == '');
		if(!e.isValid)
			e.errorText = 'Informe a url';
	}
}" />
                                                                        <ValidationSettings Display="Dynamic" ValidationGroup="0_2">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxTextBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<% $Resources:traducao, WizardDefinicaoLista_escolher_um_modelo %>" FieldName="TipoLista" HelpText="<% $Resources:traducao, WizardDefinicaoLista_escolher_um_modelo_help_text_escolha_opcao_disponibilizada %>">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxComboBox ID="cmbTipoLista" runat="server" ClientEnabled="False" ClientInstanceName="cmbTipoLista">
                                                                        <ClientSideEvents SelectedIndexChanged="cmbTipoLista_OnSelectedIndexChanged" />
                                                                        <Items>
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_consulta_simples %>" Value="RELATORIO" />
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_consulta_OLAP %>" Value="OLAP" />
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_consulta_arvore %>" Value="ARVORE" />
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_processo %>" Value="PROCESSO" />
                                                                            <dxtv:ListEditItem Text="Dashboard" Value="DASHBOARD" />
                                                                            <dxtv:ListEditItem Text="<% $Resources:traducao, WizardDefinicaoLista_relatorio %>" Value="REPORT" />
                                                                        </Items>
                                                                        <ValidationSettings ValidationGroup="0_2">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxComboBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp4" Name="grp4">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" CssClass="subtitulo" Text="<% $Resources:traducao, WizardDefinicaoLista_definindo_formato_geral_apresentacao_fonte_dados_relatorio %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>

                                                        <dxtv:LayoutGroup Caption="<% $Resources:traducao, WizardDefinicaoLista_paginacao_itens_pagina %>" ColumnCount="3" ShowCaption="False">
                                                            <Items>
                                                                <dxtv:LayoutItem FieldName="IndicaPaginacao" HelpText="<% $Resources:traducao, WizardDefinicaoLista_indique_relatorio_apresentado_paginacao_ou_nao %>" ShowCaption="False">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxCheckBox ID="ASPxFormLayout1_E14" runat="server" CheckState="Unchecked" Text="<% $Resources:traducao, WizardDefinicaoLista_possui_paginacao %>" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Width="125px">
                                                                                <ValidationSettings ValidationGroup="0_3">
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxCheckBox>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                                <dxtv:LayoutItem Caption="<% $Resources:traducao, WizardDefinicaoLista_itens_por_pagina %>" FieldName="QuantidadeItensPaginacao" HelpText="<% $Resources:traducao, WizardDefinicaoLista_escolhida_opcao_apresentar_paginacao_escolhida_informe_qtd_itens_pagina_valor_10_200_registros_helptext %>">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                            <dxtv:ASPxSpinEdit ID="ASPxFormLayout1_E15" runat="server" MaxValue="200" MinValue="10" Width="125px">
                                                                                <ValidationSettings ValidationGroup="0_3">
                                                                                </ValidationSettings>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </dxtv:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dxtv:LayoutItem>
                                                                                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxHyperLink ID="formLayout_E9" runat="server" Text="<% $Resources:traducao, WizardDefinicaoLista_construir_comando %>" Cursor="pointer">
                                                                        <ClientSideEvents Click="function(s, e) {
	popup.Show();
}" />
                                                                    </dxtv:ASPxHyperLink>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                            </Items>

                                                        </dxtv:LayoutGroup>
                                                        <dxtv:LayoutItem Caption="<% $Resources:traducao, WizardDefinicaoLista_comando %>" FieldName="ComandoSelect" HelpText="<% $Resources:traducao, WizardDefinicaoLista_clique_link_construir_comando_para_elaborar_instrucao_utilizada_trazer_dados_desejados_consulta %>">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxMemo ID="memoComandoSelect" runat="server" ClientInstanceName="memoComandoSelect" Rows="10" Width="100%" Enabled="True" Height="120px" ReadOnly="True">
                                                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_3">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                                                        </ReadOnlyStyle>
                                                                        <DisabledStyle BackColor="#EBEBEB">
                                                                        </DisabledStyle>
                                                                    </dxtv:ASPxMemo>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>

                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp5" Name="grp5">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" CssClass="subtitulo" Text="<% $Resources:traducao, WizardDefinicaoLista_definindo_informacoes_finais_consulta %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem FieldName="IndicaListaZebrada" HelpText="<% $Resources:traducao, WizardDefinicaoLista_marque_opcao_quiser_apresentar_consulta_cores_fundo_alternadas %>" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxCheckBox ID="ASPxFormLayout1_E19" runat="server" CheckState="Unchecked" Text="<% $Resources:traducao, WizardDefinicaoLista_lista_zebrada %>" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Width="175px">
                                                                        <ValidationSettings ValidationGroup="0_4">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxCheckBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem FieldName="IndicaBuscaPalavraChave" HelpText="<% $Resources:traducao, WizardDefinicaoLista_indique_solucao_apresentar_opcao_busca_palavra_chave_qualquer_parte_resultado_apresentado_consulta %>" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxCheckBox ID="ASPxFormLayout1_E20" runat="server" CheckState="Unchecked" Text="<% $Resources:traducao, WizardDefinicaoLista_busca_palavra_chave %>" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Width="175px">
                                                                        <ValidationSettings ValidationGroup="0_4">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxCheckBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="<% $Resources:traducao, WizardDefinicaoLista_sub_lista %>" FieldName="CodigoSubLista" HelpText="<% $Resources:traducao, WizardDefinicaoLista_consulta_devera_apresentar_lista_adicional_registro_indique_nome_consulta %>">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxComboBox ID="cmbCodigoSubLista" ClientInstanceName="cmbCodigoSubLista" runat="server" DataSourceID="dataSourceSubLista" TextField="NomeLista" ValueField="CodigoLista" ValueType="System.Int32" Width="300px">
                                                                        <ValidationSettings ValidationGroup="0_4">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxComboBox>
                                                                    <asp:SqlDataSource ID="dataSourceSubLista" runat="server" SelectCommand=" SELECT rs.* 
   FROM
   (
     SELECT l.CodigoLista,
            l.NomeLista
       FROM Lista AS l
      WHERE l.CodigoEntidade = @CodigoEntidade
        AND l.TipoLista = 'RELATORIO'
        AND l.URL IS NULL
        AND l.CodigoSubLista IS NULL
        AND l.IniciaisListaControladaSistema IS NULL
    UNION
     SELECT NULL, ''
   ) AS rs
  ORDER BY
        rs.NomeLista">
                                                                        <SelectParameters>
                                                                            <asp:SessionParameter DefaultValue="1" Name="CodigoEntidade" SessionField="ce" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp6" Name="grp6">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel10" runat="server" CssClass="subtitulo" Text="<% $Resources:traducao, WizardDefinicaoLista_iniciando_configuracoes_campos_consulta %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem FieldName="unbound_GerarColunas" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxCheckBox ID="cmbGerarColunas" ClientInstanceName="cmbGerarColunas" runat="server" CheckState="Unchecked" Text="<% $Resources:traducao, WizardDefinicaoLista_gerar_colunas_automaticamente %>" Width="225px">
                                                                        <ValidationSettings ValidationGroup="0_5">
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxCheckBox>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="formLayout_E8" runat="server" Text="<% $Resources:traducao, WizardDefinicaoLista_terminou_definir_informacoes_principais_consulta_marcar_opcao_gerar_colunas_automaticamente %>" Wrap="True">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp7" Name="grp7">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" CssClass="subtitulo" Text="Definindo qual painel de bordo será disponibilizado">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="Dashboard" FieldName="IDDashboard">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxComboBox ID="ASPxFormLayout1_E23" runat="server" DataSourceID="dataSourceDashboard" TextField="TituloDashboard" ValueField="IDDashboard" Width="300px">
                                                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_6">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxComboBox>
                                                                    <asp:SqlDataSource ID="dataSourceDashboard" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [IDDashboard], [TituloDashboard] FROM [Dashboard] ORDER BY [TituloDashboard]"></asp:SqlDataSource>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="formLayout_E7" runat="server" Text="<% $Resources:traducao, WizardDefinicaoLista_opcao_permitir_disponibilize_painel_bordo_dashboard_tenha_sido_previamente_elaborado %>" Wrap="True">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                        <dxtv:LayoutGroup Caption="grp8" Name="grp8">
                                            <Items>
                                                <dxtv:LayoutGroup Caption="<%$ Resources:traducao, WizardDefinicaoLista_assistente_config_relatorio %>">
                                                    <GroupBoxStyle>
                                                        <Caption Font-Bold="True" Font-Size="12pt">
                                                        </Caption>
                                                    </GroupBoxStyle>
                                                    <Items>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="ASPxLabel14" runat="server" CssClass="subtitulo" Text="<% $Resources:traducao, WizardDefinicaoLista_definindo_relatorio_disponibilizado %>">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem Caption="Relatório" FieldName="IDRelatorio">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxComboBox ID="ASPxFormLayout1_E24" runat="server" DataSourceID="dataSourceRelatorio" TextField="TituloRelatorio" ValueField="IDRelatorio" Width="300px">
                                                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="0_7">
                                                                            <RequiredField ErrorText="<% $ Resources:traducao, WizardDefinicaoLocal_error_text_campo_obrigatorio %>" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxComboBox>
                                                                    <asp:SqlDataSource ID="dataSourceRelatorio" runat="server" SelectCommand="SELECT [IDRelatorio], [TituloRelatorio] FROM [ModeloRelatorio] WHERE [IniciaisControle] IS NULL ORDER BY [TituloRelatorio]"></asp:SqlDataSource>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                        <dxtv:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dxtv:LayoutItemNestedControlContainer runat="server">
                                                                    <dxtv:ASPxLabel ID="formLayout_E3" runat="server" Text="<% $Resources:traducao, WizardDefinicaoLista_esta_opcao_permitir_disponibilize_relatorio_exportavel_PDF_RTF_previamente_elaborado %>" Wrap="True">
                                                                    </dxtv:ASPxLabel>
                                                                </dxtv:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dxtv:LayoutItem>
                                                    </Items>
                                                </dxtv:LayoutGroup>
                                            </Items>
                                        </dxtv:LayoutGroup>
                                    </Items>
                                </dxtv:TabbedLayoutGroup>
                            </Items>
                            <SettingsItemCaptions Location="Top" />
                            <ClientSideEvents Init="function(s, e) {
	defineCaminhoSelecionado();
	var height = Math.max(0, document.documentElement.clientHeight);
    	height = height - 75;
 	s.SetHeight(height);
}" />
                            <Paddings Padding="0px" />
                        </dxcp:ASPxFormLayout>

                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tabela-botoes">
                            <tr>
                                <td>
                                    <dxcp:ASPxButton ID="btnCancelar" ClientInstanceName="btnCancelar" runat="server" Text="<% $ Resources:traducao, WizardDefinicaoLista_btn_cancelar %>" Width="75px" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssClass="btn">
                                        <ClientSideEvents Click="btnCancelar_Click" />
                                    </dxcp:ASPxButton>
                                </td>
                                <td>
                                    <dxcp:ASPxButton ID="btnAnterior" ClientInstanceName="btnAnterior" runat="server" Text="<% $ Resources:traducao, WizardDefinicaoLista_btn_anterior %>" Width="75px" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssClass="btn">
                                        <ClientSideEvents Click="retrocedeEtapaAssistente" />
                                    </dxcp:ASPxButton>
                                </td>
                                <td>
                                    <dxcp:ASPxButton ID="btnProximo" ClientInstanceName="btnProximo" runat="server" Text="<% $ Resources:traducao, WizardDefinicaoLista_btn_proximo %>" Width="75px" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssClass="btn">
                                        <ClientSideEvents Click="avancaEtapaAssistente" />
                                    </dxcp:ASPxButton>
                                </td>
                                <td>
                                    <dxcp:ASPxButton ID="btnFinalizar" ClientInstanceName="btnFinalizar" runat="server" Text="<% $ Resources:traducao, WizardDefinicaoLista_btn_finalizar %>" Width="75px" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssClass="btn">
                                        <ClientSideEvents Click="finalizarWizard" />
                                    </dxcp:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="dataSourceLista" runat="server" InsertCommand="SET @IniciaisPermissao = 'CDIS' + RIGHT(REPLICATE('0', 6) + CONVERT(VARCHAR(6), ISNULL((SELECT MAX(CONVERT(INT, SUBSTRING(ps.IniciaisPermissao,6,6))) + 1 FROM PermissaoSistema ps WHERE ps.IniciaisPermissao LIKE 'CDIS%'), 1)), 6)

INSERT INTO Lista
           (NomeLista
           ,GrupoMenu
           ,ItemMenu
           ,GrupoPermissao
           ,ItemPermissao
           ,IniciaisPermissao
           ,TituloLista
           ,ComandoSelect
           ,IndicaPaginacao
           ,QuantidadeItensPaginacao
           ,IndicaOpcaoDisponivel
           ,TipoLista
           ,URL
           ,CodigoEntidade
           ,CodigoModuloMenu
           ,IndicaListaZebrada
           ,IniciaisListaControladaSistema
           ,IndicaBuscaPalavraChave
           ,IDDashboard
           ,IDRelatorio
           ,OrdemGrupoMenu
           ,OrdemItemGrupoMenu
           ,CodigoSubLista
           ,DataInclusao
           ,CodigoUsuarioInclusao)
     VALUES
           (@NomeLista
           ,@GrupoMenu
           ,@ItemMenu
           ,@GrupoPermissao
           ,@ItemPermissao
           ,@IniciaisPermissao
           ,@TituloLista
           ,@ComandoSelect
           ,@IndicaPaginacao
           ,@QuantidadeItensPaginacao
           ,@IndicaOpcaoDisponivel
           ,@TipoLista
           ,@URL
           ,@CodigoEntidade
           ,@CodigoModuloMenu
           ,@IndicaListaZebrada
           ,@IniciaisListaControladaSistema
           ,@IndicaBuscaPalavraChave
           ,@IDDashboard
           ,@IDRelatorio
           ,@OrdemGrupoMenu
           ,@OrdemItemGrupoMenu
           ,@CodigoSubLista
           ,GETDATE()
           ,@CodigoUsuario)

    SET @CodigoLista = SCOPE_IDENTITY()

EXECUTE p_IncluiPermissao
   @GrupoPermissao
  ,@ItemPermissao
  ,@IniciaisPermissao
  ,'EN'
  ,5"
            OnInserted="dataSourceLista_Inserted" OnInserting="dataSourceLista_Inserting">
            <InsertParameters>
                <asp:Parameter Name="NomeLista" />
                <asp:Parameter Name="GrupoMenu" />
                <asp:Parameter Name="ItemMenu" />
                <asp:Parameter Name="GrupoPermissao" />
                <asp:Parameter Name="ItemPermissao" />
                <asp:Parameter Name="IniciaisPermissao" />
                <asp:Parameter Name="TituloLista" />
                <asp:Parameter Name="ComandoSelect" />
                <asp:Parameter Name="IndicaPaginacao" />
                <asp:Parameter Name="QuantidadeItensPaginacao" />
                <asp:Parameter Name="IndicaOpcaoDisponivel" />
                <asp:Parameter Name="TipoLista" />
                <asp:Parameter Name="URL" />
                <asp:Parameter Name="CodigoUsuario" />
                <asp:Parameter Name="CodigoEntidade" />
                <asp:Parameter Name="CodigoModuloMenu" />
                <asp:Parameter Name="IndicaListaZebrada" />
                <asp:Parameter Name="IniciaisListaControladaSistema" />
                <asp:Parameter Name="IndicaBuscaPalavraChave" />
                <asp:Parameter Name="IDDashboard" />
                <asp:Parameter Name="IDRelatorio" />
                <asp:Parameter Name="OrdemGrupoMenu" />
                <asp:Parameter Name="OrdemItemGrupoMenu" />
                <asp:Parameter Name="CodigoSubLista" />
                <asp:Parameter DefaultValue="0" Direction="Output" Name="CodigoLista" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
<dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	        if(s.cpErro != '')
            {
                  //function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
                  window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
            }
            else 
            {
                  callbackCompletaAcao.PerformCallback('finalizarWizard');
            }
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxPopupControl ID="popup" runat="server" CloseAction="CloseButton" Maximized="True" Modal="True" ClientInstanceName="popup" HeaderText="<% $Resources:traducao, WizardDefinicaoLista_construir_comando %>" Width="500px">
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <dx:ASPxQueryBuilder ID="queryBuilder" runat="server" ClientInstanceName="queryBuilder" OnSaveQuery="queryBuilder_SaveQuery">
                        <ClientSideEvents EndCallback="function(s, e) {
	memoComandoSelect.SetText(s.cpComandoSelect);
    popup.Hide();
}"
                            SaveCommandExecute="onSaveCommandExecute" />
                    </dx:ASPxQueryBuilder>
                    <dxtv:ASPxCallback ID="callbackCompletaAcao" runat="server" ClientInstanceName="callbackCompletaAcao" OnCallback="callbackCompletaAcao_Callback">
                        <ClientSideEvents CallbackComplete="onCallbackComplete" />
                    </dxtv:ASPxCallback>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
