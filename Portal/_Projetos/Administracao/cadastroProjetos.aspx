<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroProjetos.aspx.cs" Inherits="_cadastroProjetos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <base target="_self" />
    <title>Novo Projeto</title>
    <script type="text/javascript" language="javascript">
        var possuiAlteracoes = 'N';
        var mudaRecarregaForm = 'S';
        function verificaQuemAtualiza(valor) {
            var atualizaProjeto = hfGeral.Get("AtualizaProjeto");
            rbTipoAprovacao.SetEnabled(true);
        }

        function verificaAlteracaoCampos() {
            var possuiInformacoesNaoSalvas = false;
            indexTab = pcProjeto.GetActiveTabIndex() == 0 ? 1 : 0;
            nomeTab = pcProjeto.GetTab(indexTab).GetText();

            if (indexTab == 0) {

            } else {
                possuiAlteracoes = frmCaracterizacao.existeConteudoCampoAlterado ? 'S' : 'N';
            }

            if (possuiAlteracoes == 'S') {
                var txtMsg = traducao.cadastroProjetos_existem_informa__es_alteradas_e_n_o_salvas_na_aba__ + nomeTab + "'" + traducao.cadastroProjetos___as_altera__es_somente_ser_o_salvas_ap_s_selecionar_o_bot_o_salvar_desta_aba_;
                window.top.mostraMensagem(txtMsg, 'atencao', true, false, null);
            }

        }
    </script>
    <style type="text/css">
        .style1 {
            width: 225px;
        }

        .style4 {
            height: 10px;
            width: 10px;
        }

        .style5 {
            width: 10px;
        }

        .style6 {
            height: 10px;
        }

        .style7 {
        }

        .style10 {
            width: auto;
        }
        .auto-style1 {
            height: 18px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;overflow:auto">
            <tr>
                <td>
                    <%--                    <dxtc:ASPxPageControl ID="pcProjeto" runat="server" ActiveTabIndex="0"
                        ClientInstanceName="pcProjeto"
                        Width="100%" Style="margin-top: 0px">
                        <TabPages>
                            <dxtc:TabPage Name="tbConfiguracoes" Text="Configurações">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">--%>
                                        <table class="formulario" border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table <%--class="formulario-colunas"--%> width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lblNomeProjeto" runat="server"
                                                                        ClientInstanceName="lblNomeProjeto"
                                                                        Text="Projeto: *">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lbTipoProjeto" runat="server"
                                                                        Text="Tipo do projeto:" ClientInstanceName="lbTipoProjeto">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lblCodigo" runat="server"
                                                                        Text="Código:" ClientInstanceName="lbTipoProjeto">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" AutoCompleteType="Disabled"
                                                                        ClientInstanceName="txtNomeProjeto"
                                                                        MaxLength="255" Width="100%">
                                                                        <ClientSideEvents TextChanged="function(s, e) {
	possuiAlteracoes = 'S';
}" />
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td valign="top">
                                                                    <dxe:ASPxComboBox ID="ddlTipoProjeto" runat="server"
                                                                        ClientInstanceName="ddlTipoProjeto"
                                                                        IncrementalFilteringMode="Contains"
                                                                        ValueType="System.Int32" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}"
                                                                            Init="function(s, e) {
}" />
                                                                        <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td valign="top">
                                                                    <table class="formulario-colunas" style="margin-top:-1.0%; width: 100%;">
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxTextBox ID="txtCodigoReservado" runat="server"
                                                                                    AutoCompleteType="Disabled" ClientInstanceName="txtCodigoReservado"
                                                                                    MaxLength="20" Width="100%">
                                                                                    <ClientSideEvents TextChanged="function(s, e) {
	possuiAlteracoes = 'S';
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td valign="top" style="width: 10px">
                                                                                <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda"
                                                                                    Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png"
                                                                                    ToolTip="Código utilizado para interface com outros sistemas da instituição"
                                                                                    Width="18px">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                            Text="Objetivos:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtObjetivos" runat="server"
                                                            ClientInstanceName="txtObjetivos"
                                                            Rows="3" Width="100%">
                                                            <ClientSideEvents TextChanged="function(s, e) {
	possuiAlteracoes = 'S';
}" Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 360);
}" />
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table class="formulario-colunas" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="auto-style1">
                                                                                    <dxe:ASPxLabel ID="lblNomeCategoria" runat="server"
                                                                                        ClientInstanceName="lblNomeCategoria"
                                                                                        Text="Categoria:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="auto-style1">
                                                                                    <dxe:ASPxLabel ID="lblUnidade" runat="server" ClientInstanceName="lblUnidade"
                                                                                        Text="Unidade: *">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="auto-style1">
                                                                                    <dxe:ASPxLabel ID="lblGerenteProjeto" runat="server"
                                                                                        ClientInstanceName="lblGerenteProjeto"
                                                                                        Text="Gerente do Projeto: *">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="auto-style1">
                                                                                    <dxtv:ASPxLabel runat="server" Text="Carteira Principal:">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 150px;">
                                                                                    <dxe:ASPxComboBox ID="ddlCategoria" runat="server"
                                                                                        ClientInstanceName="ddlCategoria"
                                                                                        IncrementalFilteringMode="Contains"
                                                                                        TextFormatString="{1}" ValueType="System.String" Width="100%" DropDownRows="4">
                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}" />
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaCategoria" Width="120px" />
                                                                                            <dxe:ListBoxColumn Caption="Descrição" FieldName="DescricaoCategoria"
                                                                                                Width="250px" />
                                                                                        </Columns>
                                                                                        <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox ID="ddlUnidadeNegocio" runat="server"
                                                                                        ClientInstanceName="ddlUnidadeNegocio"
                                                                                        IncrementalFilteringMode="Contains"
                                                                                        TextFormatString="{1}" ValueType="System.Int32" Width="100%" DropDownHeight="150px">
                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}" />
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio"
                                                                                                Width="140px" />
                                                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio"
                                                                                                Width="300px" />
                                                                                        </Columns>

                                                                                        <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>

                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox ID="ddlGerenteProjeto" runat="server"
                                                                                        ClientInstanceName="ddlGerenteProjeto"
                                                                                        TextFormatString="{0}" Width="100%"
                                                                                        DropDownStyle="DropDown" EnableCallbackMode="True"  IncrementalFilteringMode="Contains"
                                                                                        OnItemRequestedByValue="ddlGerenteProjeto_ItemRequestedByValue"
                                                                                        OnItemsRequestedByFilterCondition="ddlGerenteProjeto_ItemsRequestedByFilterCondition"
                                                                                        TextField="NomeUsuario" ValueField="CodigoUsuario" CallbackPageSize="80" DropDownRows="10" DropDownHeight="150px">
                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}"
                                                                                            Init="function(s, e) {
	ddlGerenteProjeto.SetText(ddlGerenteProjeto.cp_ddlGerenteProjeto);
}" />
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="220px" />
                                                                                            <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="100px" />
                                                                                        </Columns>

                                                                                        <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>

                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 150px;">
                                                                                    <dxtv:ASPxComboBox ID="ddlCarteiraPrincipal" runat="server" CallbackPageSize="80" ClientInstanceName="ddlCarteiraPrincipal" DropDownRows="10" DropDownStyle="DropDown" EnableCallbackMode="True" TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" Width="100%">
                                                                                        <SettingsLoadingPanel Text="Carregando;" />
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxtv:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <%--<td align="right" style="width: 35px; display:" valign="bottom">
                                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" Cursor="Pointer" 
                                                                    ImageUrl="~/imagens/relStatus.PNG" ToolTip="Associar Status Reports">
                                                                    <clientsideevents click="function(s, e) {
	pcRelatorio.Show();
}" />
                                                                </dxe:ASPxImage>
                                                            </td>--%>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="spnAssociaCronograma" runat="server">
                                                            <table id="tbAssociaCronograma" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="lblAsCroExistente" runat="server"
                                                                            ClientInstanceName="lblAsCroExistente"
                                                                            Text="Associar Cronograma Existente:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="ddlAssoCroExistente" runat="server"
                                                                            ClientInstanceName="ddlAssoCroExistente"
                                                                            ValueType="System.String" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td runat="server" id="td_UnidadeAtendimento">
                                                        <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="lblUnidadeAtendimento" runat="server"
                                                                        ClientInstanceName="lblUnidadeAtendimento"
                                                                        Text="Unidade Atendimento:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="ddlUnidadeAtendimento" runat="server"
                                                                        ClientInstanceName="ddlUnidadeAtendimento"
                                                                        ValueType="System.Int32" Width="100%" IncrementalFilteringMode="Contains"
                                                                        TextFormatString="{1}">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
	possuiAlteracoes = 'S';
}" />
                                                                        <Columns>
                                                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio"
                                                                                Width="140px" />
                                                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUnidadeNegocio"
                                                                                Width="300px" />
                                                                        </Columns>

                                                                        <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>

                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxp:ASPxPanel ID="pnVersaoProject" runat="server"
                                                            ClientInstanceName="pnVersaoProject" Width="100%">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 340px">
                                                                                    <dxrp:ASPxRoundPanel ID="rdQuemAtualizaTarefas" runat="server"
                                                                                        ClientInstanceName="rdQuemAtualizaTarefas"
                                                                                        HeaderText="Atualização das Tarefas" Width="335px">
                                                                                        <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                                                        <PanelCollection>
                                                                                            <dxp:PanelContent runat="server">
                                                                                                <dxe:ASPxRadioButtonList ID="rbQuemAtualiza" runat="server"
                                                                                                    ClientInstanceName="rbQuemAtualiza"
                                                                                                    Height="10px" ItemSpacing="5px" RepeatDirection="Horizontal" Width="100%">
                                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaQuemAtualiza(s.GetValue());
	possuiAlteracoes = 'S';
}" />
                                                                                                    <Items>
                                                                                                        <dxe:ListEditItem Text="Gerente do Projeto" Value="N" />
                                                                                                        <dxe:ListEditItem Text="Recursos" Value="S" />
                                                                                                    </Items>
                                                                                                    <Border BorderStyle="None"></Border>
                                                                                                    <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxRadioButtonList>
                                                                                            </dxp:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dxrp:ASPxRoundPanel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxrp:ASPxRoundPanel ID="rdQuemAprovaTarefas" runat="server"
                                                                                        ClientInstanceName="rdQuemAprovaTarefas"
                                                                                        HeaderText="Aprovação das Horas Trabalhadas" Width="335px">
                                                                                        <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                                                        <PanelCollection>
                                                                                            <dxp:PanelContent runat="server">
                                                                                                <dxe:ASPxRadioButtonList ID="rbTipoAprovacao" runat="server"
                                                                                                    ClientInstanceName="rbTipoAprovacao"
                                                                                                    Height="10px" ItemSpacing="5px" RepeatDirection="Horizontal" Width="100%">
                                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	possuiAlteracoes = 'S';
}" />
                                                                                                    <Items>
                                                                                                        <dxe:ListEditItem Text="Gerente do Projeto" Value="GP" />
                                                                                                        <dxe:ListEditItem Text="Gerente do Recurso" Value="GR" />
                                                                                                    </Items>
                                                                                                    <Border BorderStyle="None"></Border>
                                                                                                    <DisabledStyle ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxRadioButtonList>
                                                                                            </dxp:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dxrp:ASPxRoundPanel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    <%--                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>--%><%-- <dxtc:TabPage Name="tbCaracterizacao" Text="Caracterização"
                                ClientVisible="False">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	verificaAlteracaoCampos();
}"
                            ActiveTabChanging="function(s, e) {
	if(mudaRecarregaForm == 'S')
	{
		document.getElementById('frmCaracterizacao').src = 	pcProjeto.cp_Url;
		mudaRecarregaForm = 'N';
	}
}" />
                        <Paddings Padding="10px" />
                    </dxtc:ASPxPageControl>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                        <tr>
                            <td align="right">
                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                    ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                    ID="btnSalvar">
                                    <ClientSideEvents Click="function(s, e) {
          var retorno = validaCampos(); 
         if(retorno == true)
         {
                 ASPxLoadingPanel.Show();
                                        callbackValida.PerformCallback();
         }
}"></ClientSideEvents>

                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>

                            </td>
                            <td align="right" style="padding-left: 5px;">
                                <dxe:ASPxButton runat="server"
                                    AutoPostBack="False" Text="Fechar" Width="90px"
                                    ID="btnCancelar">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}"></ClientSideEvents>

                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>

                            </td>
                            <td align="right" style="padding-right: 0px; padding-left: 5px;">
                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                    ClientInstanceName="btnNovo" ClientVisible="False" Text="Novo" Width="90px"
                                    ID="btnNovo" OnClick="btnNovo_Click">
                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>

                            </td>
                            <td style="padding-left: 0px" valign="top" align="right">
                                <dxe:ASPxHyperLink runat="server" Text="Editar Cronograma"
                                    ClientInstanceName="linkEditarCronograma" ClientVisible="False"
                                    ID="linkEditarCronograma">
                                </dxe:ASPxHyperLink>

                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>

        <dxpc:ASPxPopupControl ID="pcRelatorio" runat="server"
            HeaderText="Associar Status Reports" Width="705px" ClientInstanceName="pcRelatorio" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dxwgv:ASPxGridView ID="gvRelatorios" runat="server" AutoGenerateColumns="False"
                                    ClientInstanceName="gvRelatorios" KeyFieldName="CodigoModeloStatusReport"
                                    Width="100%">
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowSelectCheckbox="True"
                                            VisibleIndex="0" Width="50px">
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Status Report" FieldName="DescricaoModeloStatusReport"
                                            VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Periodicidade" FieldName="DescricaoPeriodicidade_PT"
                                            VisibleIndex="2" Width="120px">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="220" />
                                    <SettingsText />
                                    <Paddings Padding="0px" />
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                    Text="Fechar" Width="85px">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.mostraMensagem(traducao.cadastroProjetos_as_informa__231___245_es_ser__227_o_gravadas_quando_o_projeto_for_salvo_, 'atencao', true, false, null);
	pcRelatorio.Hide();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="pcAjuda" runat="server" AllowDragging="True" ClientInstanceName="pcAjuda"
            CloseAction="CloseButton" HeaderText="Ajuda"
            PopupElementID="imgAjuda" PopupHorizontalAlign="LeftSides" PopupHorizontalOffset="-275"
            PopupVerticalAlign="Below" PopupVerticalOffset="5" Width="293px">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <span class="style7"><asp:Literal runat="server" Text="<%$ Resources:traducao, cadastroProjetos_c_digo_utilizado_para_interface_com_outros_sistemas_da_institui__o_ %>" /></span>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle Font-Bold="True" />
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>

        <dxcp:ASPxCallback ID="callbackSalvar" runat="server"  ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e)
{
                ASPxLoadingPanel.Hide();
	            if(s.cp_erro != &quot;&quot;)
                {
                         window.top.mostraMensagem(s.cp_erro,'erro', true, false, null);
                }
                else
               {
                        if(s.cp_indicaPopup == 'S')
                        {
                              btnSalvar.SetEnabled(false);
                        }                        
                        window.top.mostraMensagem(s.cp_sucesso,'sucesso', false, false, null);
                        if(s.cp_redirectURLProjeto != '')
                       {
                            //pnLoading.Show();
                           setTimeout(function () {window.top.location.href = s.cp_redirectURLProjeto;}, 3000);
                      }
               }
                cp_erro = '';
                cp_sucesso = '';
           

}" />
        </dxcp:ASPxCallback>

        <dxcp:ASPxCallback ID="callbackValida" runat="server"  ClientInstanceName="callbackValida" OnCallback="callbackValida_Callback">
            <ClientSideEvents EndCallback="function(s, e)
{               
	            debugger
                if(s.cp_erro != &quot;&quot;)
                {
                          ASPxLoadingPanel.Hide();
                         window.top.mostraMensagem(s.cp_erro,'erro', true, false, null);
                }
                else
               {
                          callbackSalvar.PerformCallback();
               }
                cp_erro = '';
                cp_sucesso = '';
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxLoadingPanel ID="ASPxLoadingPanel" ClientInstanceName="ASPxLoadingPanel" runat="server"></dxcp:ASPxLoadingPanel>
    </form>
</body>
</html>

