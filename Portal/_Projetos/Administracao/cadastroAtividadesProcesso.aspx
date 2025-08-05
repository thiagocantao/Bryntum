<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroAtividadesProcesso.aspx.cs" Inherits="_Projetos_Administracao_cadastroAtividadesProcesso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                if (textAreaElement.name.indexOf("mmObservacoes") >= 0)
                    lblCantCaraterOb.SetText(text.length);
                else
                    lblCantCarater.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObjeto(s, e) {
            try { return setMaxLength(s.GetInputElement(), 4000); }
            catch (e) { }
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (txtNomeAtividade.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome da atividade deve ser informado.";
            }
            if (ddlResponsavel.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") O nome do responsável pela atividade deve ser informado.";
            }
            if (dataInicio.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de início da atividade deve ser informada.";
            }

            if (dataTermino.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de término da atividade deve ser informada.";
            }

            if (dataInicio.GetValue() > dataTermino.GetValue()) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de início da atividade não pode ser maior que a data de término.";
            }

//            if (ckbIndicaEventoInstitucional.GetChecked()  && (txtLocalEvento.GetText() == "" || mmObjeto.GetText() == ""))
//            {            
//                numAux++;
//                mensagem += "\n" + numAux + ") Para evento institucional o local e detalhes são obrigatórios.";
//            }
            

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
        }
    </script>
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
    
        .style8
        {
            width: 145px;
        }
        .style9
        {
            height: 5px;
        }
        .style10
        {
            height: 10px;
        }
        .style11
        {
            width: 75px;
        }
        .style13
        {
            width: 300px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form2" runat="server">
    <table>
        <tr>
            <td>
            <div style='width:880px; height:470px; overflow:auto'>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 862px">
        <tr>
            <td>
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                        HeaderText="Dados da Atividade" Width="100%">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td class="style11">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Nº Ação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 95px">
                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                Text="Nº Atividade:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                        Text="Nome da atividade:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" 
                                        Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                        meta:resourceKey="ASPxImage10Resource1" ToolTip="Informe nome da atividade." 
                                        Width="18px">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 3px; " class="style11">
                            <dxe:ASPxTextBox ID="txtNumero" runat="server" ClientInstanceName="txtNumero" 
                                Enabled="False"  Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="padding-right: 3px; width: 95px;">
                            <dxe:ASPxSpinEdit ID="txtNumeroAtividade" runat="server" 
                                ClientInstanceName="txtNumero"  
                                MaxValue="9999" MinValue="1" Number="1" NumberType="Integer" Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtNomeAtividade" runat="server" 
                                Width="100%" ClientInstanceName="txtNomeAtividade" 
                                MaxLength="255">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
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
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                    <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Início:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-left: 10px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" 
                                    Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                    meta:resourceKey="ASPxImage10Resource1" ToolTip="Informe data de início." 
                                    Width="18px">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        </table>
                        </td>
                        <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                        Text="Término:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" 
                                        Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                        meta:resourceKey="ASPxImage10Resource1" ToolTip="Informe data de término" 
                                        Width="18px">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                        Text="Responsável:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" 
                                        Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                        meta:resourceKey="ASPxImage10Resource1" ToolTip="Selecione o responsável." 
                                        Width="18px">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td style="display: none;">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="padding-left: 10px">
                                    <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" 
                                        Cursor="pointer" Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                        meta:resourceKey="ASPxImage10Resource1" 
                                        ToolTip="Marque se a atividade não envolve recursos orçamentários" Width="18px">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 95px; padding-right: 3px">
                            <dxe:ASPxDateEdit ID="dataInicio" runat="server" 
                                ClientInstanceName="dataInicio"  
                                Width="100%" DisplayFormatString="dd/MM/yyyy">
                                <CalendarProperties>
                                    <DayHeaderStyle  />
                                    <DayStyle  />
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <ButtonStyle >
                                    </ButtonStyle>
                                </CalendarProperties>
                                <ButtonStyle >
                                </ButtonStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxDateEdit>
                        </td>
                        <td style="width: 95px; padding-right: 3px">
                            <dxe:ASPxDateEdit ID="dataTermino" runat="server" 
                                ClientInstanceName="dataTermino"  
                                Width="100%" DisplayFormatString="dd/MM/yyyy">
                                <CalendarProperties>
                                    <DayHeaderStyle  />
                                    <DayStyle  />
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <ButtonStyle >
                                    </ButtonStyle>
                                </CalendarProperties>
                                <ButtonStyle >
                                </ButtonStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class="style6" style="padding-right: 3px">
                            <dxe:ASPxComboBox ID="ddlResponsavel" runat="server" 
                                ClientInstanceName="ddlResponsavel"  
                                Width="100%" IncrementalFilteringMode="Contains" TextField="NomeUsuario" 
                                TextFormatString="{0}" ValueField="CodigoUsuario">
                                <Columns>
                                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Name="Nome" 
                                        Width="250px" />
                                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Name="Email" 
                                        Width="350px" />
                                </Columns>
                                <ItemStyle  />
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="style13" style="display: none;">
                            <dxe:ASPxCheckBox ID="ckbSemRecursos" runat="server" 
                                CheckState="Unchecked" ClientInstanceName="ckbSemRecursos" 
                                 
                                Text="Não Envolve RECURSOS ORÇAMENTÁRIOS">
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td class="style9">
                </td>
        </tr>
        <tr>
            <td align="right">
                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                     Text="Salvar Dados da Atividade" 
                    Width="175px">
                    <ClientSideEvents Click="function(s, e) {
	var valida = validaCamposFormulario();
	if(valida == '') 	
		gvParceria.PerformCallback('A');
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
            <td class="style10">

        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="Incluir a Entidade Atual" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

                </td>
        </tr>
        <tr>
            <td style="margin-left: 40px">
    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvParceria" KeyFieldName="CodigoParceria" 
                    AutoGenerateColumns="False" Width="100%"  
                    ID="gvParceria" oncustomcallback="gvParceria_CustomCallback" 
                    onrowdeleting="gvParceria_RowDeleting" onrowinserting="gvParceria_RowInserting" 
                    onrowupdating="gvParceria_RowUpdating" 
                    oncelleditorinitialize="gvParceria_CellEditorInitialize">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Retorno == 'S')
		window.top.retornoModal = s.cp_Retorno;

	if(s.cp_CodigoAtividade != null &amp;&amp; s.cp_CodigoAtividade != '')
		hfCodigoAtividade.Set(&quot;CodigoAtividade&quot;, s.cp_CodigoAtividade);

	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);

	if(s.cp_AtualizaGrids == 'S')
	{
		gvProduto.PerformCallback();
		gvMarco.PerformCallback();
	}
}" />
        <Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="90px" 
                VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true">
<HeaderTemplate>
                 <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvParceria.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="Área da Parceria" FieldName="Area" 
                VisibleIndex="0">
                <PropertiesComboBox>
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="Elemento da Parceria" VisibleIndex="1" 
                FieldName="Elemento">
                <PropertiesTextEdit MaxLength="500">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

        <SettingsEditing Mode="Inline" />

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="135"></Settings>
</dxwgv:ASPxGridView>

                            </td>
        </tr>
        <tr>
            <td class="style10">
                </td>
        </tr>
        <tr>
            <td>
    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvProduto" KeyFieldName="CodigoMeta" 
                    AutoGenerateColumns="False" Width="100%"  
                    ID="gvProduto" onrowdeleting="gvProduto_RowDeleting" 
                    onrowinserting="gvProduto_RowInserting" onrowupdating="gvProduto_RowUpdating">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Retorno == 'S')
		window.top.retornoModal = s.cp_Retorno;
}" />
        <Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="90px" 
                VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true">
<HeaderTemplate>
              <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvProduto.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="Produto / Resultado" VisibleIndex="1" 
                FieldName="Meta">
                <PropertiesTextEdit MaxLength="500">
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

        <SettingsEditing Mode="Inline" EditFormColumnCount="1" />

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="135"></Settings>
</dxwgv:ASPxGridView>

                            </td>
        </tr>
        <tr>
            <td class="style10">
                </td>
        </tr>
        <tr>
            <td style="display: none">
    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvMarco" KeyFieldName="CodigoMarco" 
                    AutoGenerateColumns="False" Width="100%"  
                    ID="gvMarco" onrowdeleting="gvMarco_RowDeleting" 
                    onrowinserting="gvMarco_RowInserting" onrowupdating="gvMarco_RowUpdating">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Retorno == 'S')
		window.top.retornoModal = s.cp_Retorno;
}" />
        <Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="90px" 
                VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true">
<HeaderTemplate>
                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvMarco.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="Marco do Projeto" VisibleIndex="1" 
                FieldName="Marco">
                <PropertiesTextEdit MaxLength="255">
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings Caption="Descrição:" CaptionLocation="Top" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataDateColumn Caption="Data" VisibleIndex="2" Width="120px" 
                FieldName="Data">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" 
                    EditFormatString="dd/MM/yyyy" >
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <EditFormSettings Caption="Data:" CaptionLocation="Top" />
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataDateColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<SettingsEditing Mode="Inline" />

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="135"></Settings>
</dxwgv:ASPxGridView>

                            </td>
        </tr>
        </table>
        </div>
            </td>
        </tr>
        <tr>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                    <dxe:ASPxButton ID="btnFechar" runat="server" 
                        Text="Fechar" Width="100px" ClientInstanceName="btnFechar" 
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>

            </td>
        </tr>
    </table>

    <dxhf:ASPxHiddenField ID="hfCodigoAtividade" runat="server" 
        ClientInstanceName="hfCodigoAtividade">
    </dxhf:ASPxHiddenField>

    </form>
</body>
</html>
