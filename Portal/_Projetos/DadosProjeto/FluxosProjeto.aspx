<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FluxosProjeto.aspx.cs" Inherits="AnaliseProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Análise</title>
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

            if (window.TipoOperacao && TipoOperacao == "Incluir" && ddlFluxo.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") " + traducao.FluxosProjeto_o_fluxo_deve_ser_informado_;
            }
            if (Trim(txtNomeOpcao.GetText()) == "") {
                numAux++;
                mensagem += "\n" + numAux + ") " + traducao.FluxosProjeto_o_nome_da_op__o_deve_ser_informado_;
            }
            if (gvStatus.GetSelectedRowCount() == 0) {
                numAux++;
                mensagem += "\n" + numAux + ") " + traducao.FluxosProjeto_pelo_menos_1_status_deve_ser_selecionado_;
            }
            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
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
            rbStatus.SetValue("A");
            ddlFluxo.SetValue(null);
            txtNomeOpcao.SetText("");
            ddlOcorrencia.SetValue("U");

            lblInformacaoAtivacao.SetText("");

            gvStatus.PerformCallback("-1");

            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoFluxo;NomeFluxo;TextoOpcaoFluxo;DataAtivacao;DataDesativacao;TipoOcorrenciaFluxo;UsuarioAtivacao;UsuarioDesativacao;StatusRelacionamento', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {
            desabilitaHabilitaComponentes();

            var codigoFluxo = (values[0] != null ? values[0].toString() : "");
            var nomeFluxo = (values[1] != null ? values[1] : "");
            var nomeOpcao = (values[2] != null ? values[2] : "");
            var dataAtivacao = (values[3] != null ? values[3] : "");
            var dataDesativacao = (values[4] != null ? values[4] : "");
            var ocorrencia = (values[5] != null ? values[5] : "");
            var usuarioAtivacao = (values[6] != null ? values[6] : "");
            var usuarioDesativacao = (values[7] != null ? values[7] : "");
            var statusRelacionamento = (values[8] != null ? values[8] : "");

            rbStatus.SetValue(statusRelacionamento);
            ddlFluxo.SetText(nomeFluxo);
            txtNomeOpcao.SetText(nomeOpcao);
            ddlOcorrencia.SetValue(ocorrencia);

            if (statusRelacionamento == "A")
                lblInformacaoAtivacao.SetText(traducao.FluxosProjeto_ativado_em_ + dataAtivacao + " " + traducao.FluxosProjeto_por_ + usuarioAtivacao);
            else
                lblInformacaoAtivacao.SetText(traducao.FluxosProjeto_desativado_em_ + dataAtivacao + " " + traducao.FluxosProjeto_por_ + usuarioAtivacao);

            gvStatus.PerformCallback(codigoFluxo);
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            rbStatus.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            ddlFluxo.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
            txtNomeOpcao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            ddlOcorrencia.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");           
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
        .style1
        {
            width: 155px;
        }
        .style2
        {
            height: 10px;
        }
        .style4
        {
            width: 285px;
        }
        .style6
        {
            width: 130px;
        }
        .style7
        {
            width: 11px;
        }
        .style8
        {
            width: 11px;
            height: 10px;
        }
        .style9
        {
            width: 10px;
            height: 10px;
        }
        .style10
        {
            width: 10px;
        }
    </style>
</head>
<body class="body" enableviewstate="false">
    <form id="form1" runat="server" enableviewstate="false">
    <!-- TABLA CONTEUDO -->
    <table>
        <tr>
            <td class="style8">
            </td>
            <td class="style2">
            </td>
            <td class="style9">
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoFluxo"
                                AutoGenerateColumns="False" Width="100%" 
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
                                OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
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
"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Fluxo" ShowInCustomizationForm="True"
                                        VisibleIndex="1" FieldName="NomeFluxo" Width="600px">
                                        <Settings AutoFilterCondition="Contains" />
                                        <Settings AutoFilterCondition="Contains"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Name="Opcao" Caption="Opção"
                                        VisibleIndex="2" Width="300px" FieldName="TextoOpcaoFluxo">
                                        <Settings AutoFilterCondition="Contains" />
                                        <Settings AutoFilterCondition="Contains"></Settings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn 
                                        ShowInCustomizationForm="True" VisibleIndex="3" Caption="Status" 
                                        Width="120px" FieldName="NomeStatusRelacionamento">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TextoOpcaoFluxo" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DataAtivacao" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DataDesativacao" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TipoOcorrenciaFluxo" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="UsuarioAtivacao" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="UsuarioDesativacao" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="StatusRelacionamento" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible"></Settings>
                                <SettingsText ></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="800px"  ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                        <tr>
                                                            <td class="style1" align="center" style="padding-right: 10px">
                                                                <asp:Panel ID="Panel1" runat="server" BackColor="White" GroupingText="Status">
                                                                    <dxe:ASPxRadioButtonList ID="rbStatus" runat="server" 
                                                                         RepeatDirection="Horizontal" 
                                                                        SelectedIndex="0" Width="100%" ClientInstanceName="rbStatus">
                                                                        <Paddings Padding="0px" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Selected="True" Text="Ativo" Value="A" />
                                                                            <dxe:ListEditItem Text="Inativo" Value="D" />
                                                                        </Items>
                                                                        <Border BorderStyle="None" />
                                                                        <DisabledStyle ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxRadioButtonList>
                                                                </asp:Panel>
                                                            </td>
                                                            <td valign="bottom" style="padding-bottom: 3px">
                                                                <dxe:ASPxLabel ID="lblInformacaoAtivacao" runat="server" 
                                                                    ClientInstanceName="lblInformacaoAtivacao" >
                                                                </dxe:ASPxLabel>
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
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                    Text="Fluxo:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td class="style4">
                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                    Text="Opção do Menu:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td class="style6">
                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                    Text="Ocorrência do Fluxo:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxComboBox ID="ddlFluxo" runat="server" ClientInstanceName="ddlFluxo" 
                                                                     IncrementalFilteringMode="Contains" 
                                                                    TextField="NomeFluxo" TextFormatString="{0}" ValueField="CodigoFluxo" 
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn Caption="Fluxo" FieldName="NomeFluxo" Width="200px" />
                                                                        <dxe:ListBoxColumn Caption="Descrição" FieldName="Descricao" Width="600px" />
                                                                    </Columns>
                                                                    <ItemStyle Wrap="True" />
                                                                    <ListBoxStyle Wrap="True">
                                                                    </ListBoxStyle>
                                                                    <ValidationSettings ErrorDisplayMode="None" ErrorText="*">
                                                                    </ValidationSettings>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td class="style4" style="padding-right: 10px">
                                                                <dxe:ASPxTextBox ID="txtNomeOpcao" runat="server" 
                                                                    ClientInstanceName="txtNomeOpcao"  
                                                                    MaxLength="40" Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td class="style6">
                                                                <dxe:ASPxComboBox ID="ddlOcorrencia" runat="server" 
                                                                    ClientInstanceName="ddlOcorrencia"  
                                                                    SelectedIndex="0" Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <Items>
                                                                        <dxe:ListEditItem Selected="True" Text="1 Vez" Value="U" />
                                                                        <dxe:ListEditItem Text="N Vezes" Value="N" />
                                                                    </Items>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
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
                                                    <dxwgv:ASPxGridView ID="gvStatus" runat="server" AutoGenerateColumns="False" 
                                                        ClientInstanceName="gvStatus"  
                                                        KeyFieldName="CodigoStatus" Width="100%" 
                                                        OnCustomCallback="gvStatus_CustomCallback">
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" 
                                                                ShowSelectCheckbox="True" VisibleIndex="0" Width="50px" 
                                                                Name="colSelection">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <HeaderTemplate>
                                                                        <dxe:ASPxCheckBox ID="ckTodos" runat="server" CheckState="Unchecked" 
                                                                            ClientInstanceName="ckTodos" Layout="Flow" Text=" " TextAlign="Left" 
                                                                            ToolTip="Marcar/Desmarcar Todos">
                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	gvStatus.SelectAllRowsOnPage(s.GetChecked());
}" Init="function(s, e) {
	s.SetChecked(gvStatus.cp_Todos == 'S');
}" />
                                                                        </dxe:ASPxCheckBox>
                                            
                                                                    </HeaderTemplate>
                                                            </dxwgv:GridViewCommandColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Descricao" 
                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowSort="False" />
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <Settings VerticalScrollBarMode="Visible" />
                                                        <SettingsText  />
                                                    </dxwgv:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellspacing="0" cellpadding="2" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="100px"  ID="btnSalvar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                        Text="Fechar" Width="90px"  ID="btnFechar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                        </Paddings>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center">
                                                    </td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                            ID="lblAcaoGravacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(traducao.FluxosProjeto_incluir == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado(traducao.FluxosProjeto_fluxo_associado_com_sucesso_);
		window.parent.callbackMenu.PerformCallback();
	}
    else if(traducao.FluxosProjeto_editar == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado(traducao.FluxosProjeto_associa__o_alterada_com_sucesso_);
		window.parent.callbackMenu.PerformCallback();
	}
    else if(traducao.FluxosProjeto_excluir == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado(traducao.FluxosProjeto_associa__o_exclu_da_com_sucesso_);
		window.parent.callbackMenu.PerformCallback();
	}
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td class="style10">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
