<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroAcoes.aspx.cs" Inherits="_Projetos_Administracao_cadastroAcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript">
        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (txtNumero.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O número da ação deve ser informado.";
            }
            if (txtNome.GetText().length < 10) {
                numAux++;
                mensagem += "\n" + numAux + ") O campo 'Nome da ação' deve ser informado com no mínimo 10 caracteres.";
            }
            if (txtNome.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome da ação deve ser informado.";
            }
            if (ddlResponsavel.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O nome do responsável pela ação deve ser informado.";
            }

            if (ddlFonteRecursos.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A fonte de recursos deve ser informada.";
            }
            else if (ddlFonteRecursos.GetValue() == "SR") {
                if (!confirm("A fonte de recursos da ação está marcada como ''Sem Recursos'' se houver registro no orçamento para esta ação os mesmos serão excluidos!\n\nConfirma a operação?")) {
                    numAux++;
                    mensagem += "\n" + numAux + ") Operação cancelada pelo usuário.";
                }

            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
        }
    </script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style7
        {
            height: 13px;
            width: 300px;
        }
        .style10
        {
            width: 978px;
        }
        .style11
        {
            width: 2479px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td style="padding-bottom: 10px">
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                        HeaderText="Dados da Ação" Width="100%">
                        <ContentPaddings Padding="5px" />
                        <HeaderStyle>
                            <Paddings Padding="4px" PaddingTop="1px" />
                        </HeaderStyle>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td style="width: 75px">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                            Text="Nº da Ação:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="style10">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                        Text="Nome da Ação:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage10Resource1"
                                                                        ToolTip="Informe nome da ação." Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                                        Text="Código Reservado:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage10Resource1"
                                                                        ToolTip="Código CR (Zeus)" Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px; width: 75px;">
                                                        <dxe:ASPxSpinEdit ID="txtNumero" runat="server" Number="1" NumberType="Integer" Width="100%"
                                                            ClientInstanceName="txtNumero"  MaxValue="9999"
                                                            MinValue="1">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td class="style10" style="padding-right: 5px;">
                                                        <dxe:ASPxTextBox ID="txtNome" runat="server" 
                                                            Width="100%" ClientInstanceName="txtNome" MaxLength="255">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="style7" style="width: 200px;">
                                                        <dxe:ASPxTextBox ID="txtCodigoReservado" runat="server" ClientInstanceName="txtCodigoReservado"
                                                             MaxLength="20" Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td scope="height:5px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                        Text="Responsável:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage10Resource1"
                                                                        ToolTip="Informe nome do responsável." Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                                        Text="Fonte de Recursos:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                                                        Height="18px" ImageUrl="~/imagens/ajuda.png" meta:resourceKey="ASPxImage10Resource1"
                                                                        ToolTip="Selecione a fonte de Recursos." Width="18px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel"
                                                             Width="100%" TextField="NomeUsuario" TextFormatString="{0}"
                                                            ValueField="CodigoUsuario" IncrementalFilteringMode="Contains">
                                                            <Columns>
                                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Name="Nome" Width="250px" />
                                                                <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Name="Email" Width="350px" />
                                                            </Columns>
                                                            <ItemStyle  />
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td class="style7">
                                                        <dxe:ASPxComboBox ID="ddlFonteRecursos" runat="server" ClientInstanceName="ddlFonteRecursos"
                                                             SelectedIndex="0" Width="100%">
                                                            <Items>
                                                                <dxe:ListEditItem Selected="True" Text="Sem Recursos" Value="SR" />
                                                                <dxe:ListEditItem Text="Fundecoop" Value="FU" />
                                                                <dxe:ListEditItem Text="Unidade Nacional" Value="RP" />
                                                            </Items>
                                                            <ItemStyle  />
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td scope="height:5px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" 
                                                Text="Salvar Dados da Ação" Width="165px" AutoPostBack="False" Style="margin-left: 0px">
                                                <ClientSideEvents Click="function(s, e) {
	var valida = validaCamposFormulario();
	if(valida == '') 	
		gvMetasAcao.PerformCallback('A');
    else
      window.top.mostraMensagem(valida, 'atencao', true, false, null);
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
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" 
                        HeaderText="Metas da Ação" Width="100%">
                        <ContentPaddings Padding="5px" />
                        <HeaderStyle>
                            <Paddings Padding="4px" PaddingTop="1px" />
                        </HeaderStyle>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                <dxwgv:ASPxGridView ID="gvMetasAcao" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMetasAcao"
                                     KeyFieldName="CodigoMeta" OnCustomCallback="gvMetasAcao_CustomCallback"
                                    OnRowDeleting="gvMetasAcao_RowDeleting" OnRowInserting="gvMetasAcao_RowInserting"
                                    OnRowUpdating="gvMetasAcao_RowUpdating" Width="100%">
                                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Retorno == 'S')
		window.top.retornoModal = s.cp_Retorno;

	if(s.cp_CodigoAcao != null &amp;&amp; s.cp_CodigoAcao != '')
		hfCodigoAcao.Set(&quot;CodigoAcao&quot;, s.cp_CodigoAcao);

	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);
}" />
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="True"
                                            VisibleIndex="0" Width="80px" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                                            ShowUpdateButton="true">
                                            <HeaderTemplate>
                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvMetasAcao.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Meta" FieldName="Meta" Name="Meta" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                            <PropertiesTextEdit MaxLength="500" Width="100%">
                                                <ClientSideEvents KeyUp="function(s, e) {

}" />
                                                <ValidationSettings CausesValidation="True">
                                                    <RequiredField ErrorText="Campo Obrigatório!" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                            <EditFormSettings Caption="Meta:" CaptionLocation="Top" VisibleIndex="0" />
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                            AllowResize="True" Width="720px" />
                                    </SettingsPopup>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="185" />
                                    <SettingsText ConfirmDelete="Deseja realmente excluir o registro?" PopupEditFormCaption="Metas" />
                                    <Styles>
                                        <Header>
                                            <Paddings Padding="2px" />
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <Style >
                                            
                                        </Style>
                                    </StylesEditors>
                                </dxwgv:ASPxGridView>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="btnFechar" runat="server" 
                        Text="Fechar" Width="100px" ClientInstanceName="btnFechar" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <dxhf:ASPxHiddenField ID="hfCodigoAcao" runat="server" ClientInstanceName="hfCodigoAcao">
    </dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidade Atual"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        ShowHeader="False" Width="420px"  ID="pcUsuarioIncluido">
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
                                    ID="lblAcaoGravacao" EncodeHtml="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
