<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroProjetosExtendido.aspx.cs" Inherits="_cadastroProjetosExtendido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<base target="_self" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Novo Projeto</title>
    <script type="text/javascript" language="javascript">
        var codigoEtapaEdicao = -1;

        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
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
            if (callbackSalvar.cp_NovoCodigoProjeto != null && callbackSalvar.cp_NovoCodigoProjeto != '') {
                hfGeral.Set("CodigoProjeto", callbackSalvar.cp_NovoCodigoProjeto);
                pnCallback.PerformCallback();                
            }
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            var numAux = 0;
            var mensagem = "";

            //------------Obtendo data e hora actual
            var dataInicio = new Date(ddlInicio.GetValue());
            var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
            var dataInicioC = Date.parse(dataInicioP);

            var dataTermino = new Date(ddlTermino.GetValue());
            var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
            var dataTerminoC = Date.parse(dataTerminoP);

            if (Trim(txtNomeProjeto.GetText()) == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome do projeto deve ser informado.";
            }
            if ((ddlInicio.GetValue() != null) && (ddlTermino.GetValue() != null)) {
                if (dataInicioC > dataTerminoC) {
                    numAux++;
                    mensagem += "\n" + numAux + ") A data de início do projeto não pode ser maior que a data de término!";
                    retorno = false;
                }
            }
            if (ddlMoeda.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A moeda deve ser informada.";
            }
            if (mensagem != "") {
                window.top.mostraMensagem("Alguns dados são de preenchimento obrigatório:\n\n" + mensagem, 'atencao', true, false, null);
                return false;
            }

            return true;
        }

        function validaCamposFormularioEtapas() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            var numAux = 0;
            var mensagem = "";

            //------------Obtendo data e hora actual
            var dataInicio = new Date(ddlInicioEtapa.GetValue());
            var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
            var dataInicioC = Date.parse(dataInicioP);

            var dataTermino = new Date(ddlTerminoEtapa.GetValue());
            var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
            var dataTerminoC = Date.parse(dataTerminoP);

            if (Trim(txtNomeEtapa.GetText()) == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome da etapa deve ser informado.";
            }
            if (ddlInicioEtapa.GetValue() == null || ddlInicioEtapa.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") A data de início da tarefa deve ser informada.";
            }
            if (ddlTerminoEtapa.GetValue() == null || ddlTerminoEtapa.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") A data de término da tarefa deve ser informada.";
            }
            if ((sePercentConcluido.GetValue() == null) || (sePercentConcluido.GetText() == "")) {
                numAux++;
                mensagem += "\n" + numAux + ") O percentual concluído da tarefa deve ser informado.";
            }
            if ((ddlInicioEtapa.GetValue() != null) && (ddlTerminoEtapa.GetValue() != null)) {
                if (dataInicioC > dataTerminoC) {
                    numAux++;
                    mensagem += "\n" + numAux + ") A data de início da etapa não pode ser maior que a data de término!";
                    retorno = false;
                }
            }
            if (ddlResponsavelEtapa.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O responsável pela tarefa deve ser informado.";
            }
            if (mensagem != "") {
                window.top.mostraMensagem("Alguns dados são de preenchimento obrigatório:\n\n" + mensagem, 'atencao', true, false, null);
                return false;
            }

            return true;
        }

        function incluiNovaEntidade() {
            pcEntidades.Show();
        }

        function incluiNovoObjetivo() {
            pcObjetivos.Show();
        }

        function incluiNovaEtapa() {

            limpaCamposEtapas();
            pcEtapas.Show();
        }

        function limpaCamposEtapas() {
            codigoEtapaEdicao = -1;

            txtNomeEtapa.SetText('');
            ddlInicioEtapa.SetValue(null);
            ddlTerminoEtapa.SetValue(null);
            ddlResponsavelEtapa.SetValue(null);
            txtSequenciaEtapa.SetText(gvEtapas.cp_ProximoSequencia);
            sePercentConcluido.SetValue(null);
        }

        function editaEtapa(codigoEtapa, numeroSequencia, nomeEtapa, dataInicio, dataTermino, codigoResponsavel, percentConcluido) {

            codigoEtapaEdicao = codigoEtapa;
            txtSequenciaEtapa.SetText(numeroSequencia);
            txtNomeEtapa.SetText(nomeEtapa);
            ddlInicioEtapa.SetText(dataInicio);
            ddlTerminoEtapa.SetText(dataTermino);
            ddlResponsavelEtapa.SetValue(codigoResponsavel);
            sePercentConcluido.SetValue(percentConcluido);       

            pcEtapas.Show();
        }

        function excluiEntidade(codigoEntidade) {
            gvEntidades.PerformCallback(codigoEntidade);
        }

        function excluiObjetivo(codigoObjetivo) {
            gvObjetivos.PerformCallback(codigoObjetivo);
        }

        function excluiEtapa(codigoEtapa) {
            gvEtapas.PerformCallback('X' + codigoEtapa);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 10px;
            height: 5px;
        }
        .style2
        {
            height: 5px;
        }
        .style3
        {
            width: 100%;
        }
        .style6
        {
            width: 100px;
        }
        .style7
        {
            width: 200px;
        }
        .style8
        {
            width: 9px;
        }
        .style12
        {
            width: 10px;
        }
        .style13
        {
            height: 3px;
        }
        .style14
        {
            width: 10px;
            height: 4px;
        }
        .style15
        {
            height: 4px;
        }
        .style16
        {
            height: 10px;
        }
        .style20
        {
            width: 130px;
        }
        .style22
        {
            width: 75px;
        }
        .style23
        {
            width: 160px;
        }
        .style24
        {
            height: 2px;
        }
    </style>
</head>
<body style="margin: 0px;" >
    <form id="form1" runat="server">        
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody>
                    <tr>
                        <td class="style13">
                        </td>
                        <td class="style13">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
    <tr>
        <td>
        </td>
        <td>
            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                HeaderText="Resumo" Width="100%">
                <ContentPaddings PaddingBottom="3px" PaddingLeft="4px" PaddingRight="4px" 
                    PaddingTop="3px" />
                <HeaderStyle>
                <Paddings PaddingBottom="2px" PaddingTop="1px" />
                </HeaderStyle>
                <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <table cellpadding="0" cellspacing="0" class="style3">
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblNomeProjeto" runat="server" 
                    ClientInstanceName="lblNomeProjeto"  
                    Text="Projeto:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" AutoCompleteType="Disabled" 
                    ClientInstanceName="txtNomeProjeto"  
                    MaxLength="255" Width="100%">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style3">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Escopo:" Width="150px">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Resultados Esperados:" Width="140px">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td  width="50%">
                            <dxe:ASPxMemo ID="txtEscopo" runat="server" ClientInstanceName="txtEscopo" 
                                 Rows="5" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                        <td width="50%">
                            <dxe:ASPxMemo ID="txtResultados" runat="server" 
                                ClientInstanceName="txtResultados"  
                                Rows="5" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style3">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lblGerenteProjeto" runat="server" 
                                ClientInstanceName="lblGerenteProjeto"  
                                Text="Responsável (Gerente):">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style6">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                Text="Data Início:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style6">
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                Text="Data Término:">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="60px">
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                Text="Moeda:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style23">
                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                Text="Orçamento:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxComboBox ID="ddlGerenteProjeto" runat="server" 
                                ClientInstanceName="ddlGerenteProjeto"  
                                IncrementalFilteringMode="Contains"  
                                TextFormatString="{0}" ValueType="System.String" Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}" />
                                <Columns>
                                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                                </Columns>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="style6" >
                            <dxe:ASPxDateEdit ID="ddlInicio" runat="server" 
                                Width="100%" ClientInstanceName="ddlInicio" 
                                UseMaskBehavior="True">
                                <ClientSideEvents Init="function(s, e) {
	if(s.GetValue() != null)
	{
		ddlInicioEtapa.SetMinDate(s.GetValue());
		ddlTerminoEtapa.SetMinDate(s.GetValue());
	}
}" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class="style6" >
                            <dxe:ASPxDateEdit ID="ddlTermino" runat="server" 
                                Width="100%" ClientInstanceName="ddlTermino" 
                                UseMaskBehavior="True">
                                <ClientSideEvents Init="function(s, e) {
	if(s.GetValue() != null)
	{
		ddlInicioEtapa.SetMaxDate(s.GetValue());
		ddlTerminoEtapa.SetMaxDate(s.GetValue());
	}
}" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="ddlMoeda" runat="server" ClientInstanceName="ddlMoeda" 
                                SelectedIndex="0" ValueType="System.String" Width="100%">
                                <Items>
                                    <dxe:ListEditItem Selected="True" Text="R$" Value="R$" />
                                    <dxe:ListEditItem Text="US$" Value="US$" />
                                </Items>
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="style23">
                            <dxe:ASPxTextBox ID="txtValorOrcamento" runat="server" 
                                ClientInstanceName="txtValorOrcamento" DisplayFormatString="{0:n2}" 
                                 HorizontalAlign="Right" Width="100%">
                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style24">
            </td>
        </tr>
        <tr>
            <td align="right">
                <dxe:ASPxButton ID="btnSalvar" runat="server" 
                    Text="Salvar" Width="100px" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) {
	if(validaCamposFormulario())
		callbackSalvar.PerformCallback();
}" />
                    <Paddings Padding="0px" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
                    </dxp:PanelContent>
</PanelCollection>
            </dxrp:ASPxRoundPanel>
</td>
        <td class="style12">
            &nbsp;</td></tr>
    <tr>
        <td class="style14">
        </td>
        <td class="style15">
</td>
        <td class="style14">
            </td></tr>
    <tr>
        <td class="style1">
        </td>
        <td>
            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" 
                ClientInstanceName="pnCallback" Width="100%"  
                >
                <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <table cellpadding="0" cellspacing="0" class="style3">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style3">
                    <tr>
                        <td  width="50%">
                            <dxwgv:ASPxGridView ID="gvEntidades" runat="server" AutoGenerateColumns="False" 
                                ClientInstanceName="gvEntidades"  
                                KeyFieldName="CodigoUsuario" OnCustomCallback="gvEntidades_CustomCallback" 
                                Width="100%" EnableRowsCache="False" EnableViewState="False">
                                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	
	if(s.cp_AtualizaCombo == 'S')
	{
		ddlNovaEntidade.SetValue(null);
		ddlNovaEntidade.PerformCallback();
	}
}" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption=" " ShowInCustomizationForm="True" 
                                        VisibleIndex="0" Width="35px">
                                        <DataItemTemplate>
                                            <%# (podeEditarGrids) ? string.Format("<img alt='Excluir' src='../../imagens/botoes/excluirReg02.PNG' style='cursor: pointer;' onclick='excluiEntidade({0});' />", Eval("CodigoUsuario"))
                                                                                                : string.Format("<img alt='' src='../../imagens/botoes/excluirRegDes.PNG' style='cursor: default;' />") %>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" >
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                        </HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                            <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                        </CellStyle>
                                        <HeaderTemplate>
                                            <%# (podeEditarGrids) ? "<img alt='Incluir' src='../../imagens/botoes/incluirReg02.PNG' style='cursor: pointer;' onclick='incluiNovaEntidade();' />" : "&nbsp;"%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Entidade" FieldName="NomeUsuario" 
                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="91" />
                                <Styles>
                                    <CommandColumn>
                                        <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                    </CommandColumn>
                                </Styles>
                            </dxwgv:ASPxGridView>
                        </td>
                        <td width="50%">
                            <dxwgv:ASPxGridView ID="gvObjetivos" runat="server" AutoGenerateColumns="False" 
                                ClientInstanceName="gvObjetivos"  
                                KeyFieldName="CodigoObjetoEstrategia" 
                                OnCustomCallback="gvObjetivos_CustomCallback" Width="100%" 
                                OnHtmlRowPrepared="gvObjetivos_HtmlRowPrepared" EnableRowsCache="False" 
                                EnableViewState="False">
                                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	
	if(s.cp_AtualizaCombo == 'S')
	{
		ddlNovoObjetivo.SetValue(null);
		ddlNovoObjetivo.PerformCallback();
	}
}" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption=" " ShowInCustomizationForm="True" 
                                        VisibleIndex="0" Width="35px">
                                        <DataItemTemplate>
                                            <%# (podeEditarGrids) ? string.Format("<img alt='Excluir' src='../../imagens/botoes/excluirReg02.PNG' style='cursor: pointer;' onclick='excluiObjetivo({0});' />", Eval("CodigoObjetoEstrategia"))
                                                                                                : string.Format("<img alt='' src='../../imagens/botoes/excluirRegDes.PNG' style='cursor: default;' />") %>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" >
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                        </HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                            <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                        </CellStyle>
                                        <HeaderTemplate>
                                            <%# (podeEditarGrids) ? "<img alt='Incluir' src='../../imagens/botoes/incluirReg02.PNG' style='cursor: pointer;' onclick='incluiNovoObjetivo();' />" : "&nbsp;"%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Objetivo Estratégico Relacionado" 
                                        FieldName="DescricaoObjetoEstrategia" ShowInCustomizationForm="True" 
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaObjetivoEstrategicoPrincipal" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="65" 
                                    ShowFooter="True" />
                                <Styles>
                                    <Footer>
                                        <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                    </Footer>
                                    <CommandColumn>
                                        <Paddings PaddingBottom="0px" PaddingTop="0px" />
                                    </CommandColumn>
                                </Styles>
                                <Templates>
                                    <FooterRow>
                                        <table cellpadding="0" cellspacing="0" class="style3">
                                            <tr>
                                                <td class="style8" style="background-color: #008000">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;&nbsp;<dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                        Font-Size="7pt" Text="Objetivo Estratégico Principal">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="gvEtapas" runat="server" AutoGenerateColumns="False" 
                    ClientInstanceName="gvEtapas"  
                    KeyFieldName="e.Parameters.ToString().Substring(0, 1)" Width="100%" 
                    OnCustomCallback="gvEtapas_CustomCallback" EnableRowsCache="False" 
                    EnableViewState="False">
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	

	if(s.cp_LimpaCampos == 'S')
	{
		limpaCamposEtapas();		
	}	
	else
	{
		pcEtapas.Hide();
	}
	pnSequencia.PerformCallback();
	
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption=" " ShowInCustomizationForm="True" 
                            VisibleIndex="0" Width="65px">
                            <DataItemTemplate>
                                <%#  getBotoesEdicaoTarefas(Eval("CodigoTarefa").ToString(), Eval("Sequencia").ToString(), Eval("NomeTarefa").ToString(), Eval("Inicio").ToString(), Eval("Termino").ToString(), Eval("CodigoResponsavel").ToString(), Eval("PercentualFisicoConcluido").ToString())%>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" >
                            <Paddings PaddingBottom="1px" PaddingTop="1px" />
                            </HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderTemplate>
                                <%# (podeEditarGrids) ? "<img alt='Incluir' src='../../imagens/botoes/incluirReg02.PNG' style='cursor: pointer;' onclick='incluiNovaEtapa();' />" : "&nbsp;"%>
                            </HeaderTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Sequencia" 
                            ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Macroetapas do Projeto" 
                            FieldName="NomeTarefa" ShowInCustomizationForm="True" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% Concluído" 
                            ShowInCustomizationForm="True" VisibleIndex="3" 
                            FieldName="PercentualFisicoConcluido" Width="80px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Início" ShowInCustomizationForm="True" 
                            VisibleIndex="3" Width="90px" FieldName="Inicio">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Término" ShowInCustomizationForm="True" 
                            VisibleIndex="4" Width="90px" FieldName="Termino">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Responsável" 
                            ShowInCustomizationForm="True" VisibleIndex="5" Width="220px" 
                            FieldName="NomeResponsavel">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="100" />
                    <Styles>
                        <CommandColumn>
                            <Paddings PaddingBottom="0px" PaddingTop="0px" />
                        </CommandColumn>
                    </Styles>
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="pcEntidades" runat="server" 
                    ClientInstanceName="pcEntidades" CloseAction="None" 
                    HeaderText="Associar Entidade ao Projeto" Modal="True" 
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                    ShowCloseButton="False" Width="450px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellpadding="0" cellspacing="0" class="style3">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="lblEntidade" runat="server" 
                                            Text="Entidade:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlNovaEntidade" runat="server" 
                                            ClientInstanceName="ddlNovaEntidade"  
                                            ValueType="System.String" Width="100%">
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style3">
                                            <tr>
                                                <td align="right">
                                                    <dxe:ASPxButton ID="btnSalvarEntidade" runat="server" AutoPostBack="False" 
                                                         Text="Salvar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	if(ddlNovaEntidade.GetValue() == null)
		window.top.mostraMensagem('Escolha uma entidade para continuar', 'atencao', true, false, null);
	else
		gvEntidades.PerformCallback('I');
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td align="right" class="style6">
                                                    <dxe:ASPxButton ID="btnFecharEntidade" runat="server" AutoPostBack="False" 
                                                         Text="Fechar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	pcEntidades.Hide();
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
                <dxpc:ASPxPopupControl ID="pcObjetivos" runat="server" 
                    ClientInstanceName="pcObjetivos" CloseAction="None" 
                    HeaderText="Associar Objetivo Estratégico ao Projeto" 
                    Modal="True" PopupHorizontalAlign="WindowCenter" 
                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="690px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellpadding="0" cellspacing="0" class="style3">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="lblEntidade0" runat="server" 
                                            Text="Objetivo Estratégico:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style3">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlNovoObjetivo" runat="server" 
                                                        ClientInstanceName="ddlNovoObjetivo"  
                                                        ValueType="System.String" Width="100%">
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="style7">
                                                    <dxe:ASPxCheckBox ID="ckObjPrincipal" runat="server" 
                                                        ClientInstanceName="ckObjPrincipal"  
                                                        Text="Objetivo Estratégico Principal">
                                                    </dxe:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style3">
                                            <tr>
                                                <td align="right">
                                                    <dxe:ASPxButton ID="btnSalvarObjetivo" runat="server" AutoPostBack="False" 
                                                         Text="Salvar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	if(ddlNovoObjetivo.GetValue() == null)
		window.top.mostraMensagem('Escolha um objetivo estratégico para continuar', 'atencao', true, false, null);
	else
		gvObjetivos.PerformCallback('I');
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td align="right" class="style6">
                                                    <dxe:ASPxButton ID="btnFecharObjetivo" runat="server" AutoPostBack="False" 
                                                         Text="Fechar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	pcObjetivos.Hide();
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
                <dxpc:ASPxPopupControl ID="pcEtapas" runat="server" 
                    ClientInstanceName="pcEtapas" CloseAction="None" 
                    HeaderText="Macroetapa do Projeto" Modal="True" 
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                    ShowCloseButton="False" Width="800px">
                    <ClientSideEvents Shown="function(s, e) {
	ddlInicioEtapa.SetFocus();
    ddlTerminoEtapa.SetFocus();
	txtNomeEtapa.SetFocus();

	if(ddlInicio.GetValue() != null &amp;&amp; ddlTermino.GetValue() != null)
		lblInformacaoEtapa.SetText(&quot;As datas de início e término da etapa devem estar entre &quot; + ddlInicio.GetText() + &quot; e &quot; + ddlTermino.GetText() + &quot;.&quot;);
	else if(ddlInicio.GetValue() != null)
		lblInformacaoEtapa.SetText(&quot;As datas de início e término da etapa devem ser posteriores a &quot; + ddlInicio.GetText() + &quot;.&quot;);
	else if(ddlTermino.GetValue() != null)
		lblInformacaoEtapa.SetText(&quot;As datas de início e término da etapa devem ser anteriores a &quot; + ddlTermino.GetText() + &quot;.&quot;);
	else
		lblInformacaoEtapa.SetText(&quot;&quot;);
}" />
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellpadding="0" cellspacing="0" class="style3">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td class="style22">
                                                    <dxe:ASPxLabel ID="lblEntidade2" runat="server" 
                                                        Text="Sequência:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblEntidade1" runat="server" 
                                                        Text="Macroetapa:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style22" >
                                                    <dxcp:ASPxCallbackPanel ID="pnSequencia" runat="server" 
                                                        ClientInstanceName="pnSequencia"  
                                                        >
                                                        <ClientSideEvents EndCallback="function(s, e) {
	if(gvEtapas.cp_LimpaCampos == 'S')
	{
		txtSequenciaEtapa.SetText(gvEtapas.cp_ProximoSequencia);
		txtNomeEtapa.SetFocus();
	}
}" />
                                                        <PanelCollection>
                                                            <dxp:PanelContent runat="server">
                                                                <dxe:ASPxTextBox ID="txtSequenciaEtapa" runat="server" 
                                                                    ClientInstanceName="txtSequenciaEtapa"  
                                                                    HorizontalAlign="Right" Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <MaskSettings Mask="&lt;0..999999&gt;" />
                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                    </ValidationSettings>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtNomeEtapa" runat="server" AutoCompleteType="Disabled" 
                                                        ClientInstanceName="txtNomeEtapa"  
                                                        MaxLength="255" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                            <tr>
                                                <td class="style20">
                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                        Text="Data Início:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="style20">
                                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                        Text="Data Término:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td width="80">
                                                    <dxe:ASPxLabel ID="lblPercentConcluido" runat="server" 
                                                        Text="% Concluído:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblGerenteProjeto1" runat="server" 
                                                        Text="Responsável:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style20" >
                                                    <dxe:ASPxDateEdit ID="ddlInicioEtapa" runat="server" 
                                                        ClientInstanceName="ddlInicioEtapa"  
                                                        UseMaskBehavior="True" Width="100%">
                                                        <ValidationSettings CausesValidation="True" ErrorDisplayMode="None" 
                                                            SetFocusOnError="True">
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td class="style20" >
                                                    <dxe:ASPxDateEdit ID="ddlTerminoEtapa" runat="server" 
                                                        ClientInstanceName="ddlTerminoEtapa"  
                                                        UseMaskBehavior="True" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td  width="80px">
                                                    
                                                    <dxe:ASPxSpinEdit ID="sePercentConcluido" runat="server" 
                                                        DisplayFormatString="{0:n0}"  Height="21px" 
                                                        MaxValue="100" Number="0" NumberType="Integer" Width="80px" 
                                                        ClientInstanceName="sePercentConcluido">
                                                        <ClientSideEvents NumberChanged="function(s, e) {
	var valorMin = 0;
	var valorMax = 100;
	var valor = sePercentConcluido.GetNumber();
	if(valor &lt; valorMin)
		sePercentConcluido.SetNumber(valorMin);
	else if(valor &gt; valorMax)
		sePercentConcluido.SetNumber(valorMax);
}" />
                                                        <ValidationSettings Display="None" ErrorDisplayMode="None" 
                                                            ValidateOnLeave="False" ErrorText=" ">
                                                        </ValidationSettings>
                                                    </dxe:ASPxSpinEdit>
                                                    
                                                </td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlResponsavelEtapa" runat="server" 
                                                        ClientInstanceName="ddlResponsavelEtapa"  
                                                        IncrementalFilteringMode="Contains"  
                                                        TextFormatString="{0}" ValueType="System.String" Width="100%">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}" />
                                                        <Columns>
                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                            <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                                                        </Columns>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style3">
                                            <tr>
                                                <td align="left">
                                                    <dxe:ASPxLabel ID="lblInformacaoEtapa" runat="server" Font-Italic="True" 
                                                         
                                                        
                                                        Text="*As datas de início e término da etapa devem estar entre o início e o término do projeto." 
                                                        ClientInstanceName="lblInformacaoEtapa">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right">
                                                    <dxe:ASPxButton ID="btnSalvarEtapas" runat="server" AutoPostBack="False" 
                                                         Text="Salvar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	if(validaCamposFormularioEtapas())
		gvEtapas.PerformCallback(codigoEtapaEdicao);
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td align="right" class="style6">
                                                    <dxe:ASPxButton ID="btnFecharEtapas" runat="server" AutoPostBack="False" 
                                                         Text="Fechar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	pcEtapas.Hide();
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
            </td>
        </tr>
    </table>
                    </dxp:PanelContent>
</PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
        <td class="style12">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
        </td>
        <td class="style2">
        </td>
        <td class="style1">
        </td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;</td>
        <td align=right>
                    <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False"
                        Text="Fechar" Width="90px" ClientVisible="False">
                        <Paddings Padding="0px" />
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
 </td>
        <td align=right class="style12">
                    &nbsp;</td></tr></tbody></table>
            </td>
        </tr>
    </table>
        
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);	

	if(ddlInicio.GetValue() != null)
	{
		ddlInicioEtapa.SetMinDate(ddlInicio.GetValue());
		ddlTerminoEtapa.SetMinDate(ddlInicio.GetValue());
	}
	
	if(ddlTermino.GetValue() != null)
	{
		ddlInicioEtapa.SetMaxDate(ddlTermino.GetValue());
		ddlTerminoEtapa.SetMaxDate(ddlTermino.GetValue());
	}
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
        HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" 
        ShowHeader="False"  
        ID="pcUsuarioIncluido" Width="350px">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellspacing="0" cellpadding="0" border="0" width="100%"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>




















































 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>




















































 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>

</form>
</body>
</html>
    
