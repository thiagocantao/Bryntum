<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_GrupoRecurso.aspx.cs" Inherits="administracao_adm_GrupoRecurso" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px;">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Grupos de Recursos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
                                LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                                RightMargin="50">
                                <Styles>
                                    <Default>
                                    </Default>
                                    <Header>
                                    </Header>
                                    <Cell>
                                    </Cell>
                                    <Footer>
                                    </Footer>
                                    <GroupFooter>
                                    </GroupFooter>
                                    <GroupRow>
                                    </GroupRow>
                                    <Title></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                KeyFieldName="CodigoGrupoRecurso" AutoGenerateColumns="False" Width="100%"
                                ID="gvDados"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) 
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
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="180px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                            ClientInstanceName="menu"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                            OnInit="menu_Init">
                                                            <Paddings Padding="0px" />
                                                            <Items>
                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                            ClientVisible="False">
                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image IconID="actions_reset_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                            </Items>
                                                            <ItemStyle Cursor="pointer">
                                                                <HoverStyle>
                                                                    <border borderstyle="None" />
                                                                </HoverStyle>
                                                                <Paddings Padding="0px" />
                                                            </ItemStyle>
                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                <SelectedStyle>
                                                                    <border borderstyle="None" />
                                                                </SelectedStyle>
                                                            </SubMenuItemStyle>
                                                            <Border BorderStyle="None" />
                                                        </dxm:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>

                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoRecurso" Name="CodigoGrupoRecurso" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoGrupo" Name="DescricaoGrupo" Caption="Descri&#231;&#227;o" VisibleIndex="2">
                                        <Settings AllowGroup="True" AllowAutoFilter="True"
                                            AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DetalheGrupo" Name="DetalheGrupo" Caption="Detalhe" VisibleIndex="3">
                                        <Settings AllowGroup="True" AllowAutoFilter="True"
                                            AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="GrupoRecursoSuperior" Name="GrupoRecursoSuperior" Visible="False" VisibleIndex="4">
                                        <HeaderStyle Wrap="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NivelGrupo" Name="NivelGrupo" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Name="CodigoEntidade" Visible="False" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoRecurso"
                                        Name="CodigoTipoRecurso" Caption="CodigoTipoRecurso" Visible="False"
                                        VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CustoHora"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CustoUso"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="UnidadeMedida"
                                        ShowInCustomizationForm="True" VisibleIndex="9" Caption="Unidade"
                                        Width="150px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Grupo Superior"
                                        FieldName="DescricaoGrupoSuperior" ShowInCustomizationForm="True"
                                        VisibleIndex="11" Width="220px">
                                        <Settings AllowGroup="True" AllowAutoFilter="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="DescricaoTipoRecurso"
                                        ShowInCustomizationForm="True" VisibleIndex="12" Width="150px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

                                <SettingsPager PageSize="100"></SettingsPager>

                                <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True" ShowFilterRow="True" VerticalScrollableHeight="150"></Settings>

                                <SettingsText></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px" ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>

                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Nome:" ClientInstanceName="lblNome" ID="lblNome"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNome" ID="txtNome">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                        </dxe:ASPxTextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Tipo de Recurso:" ClientInstanceName="lblNome" ID="ASPxLabel2"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="DescricaoTipoRecurso" ValueField="CodigoTipoRecurso" Width="100%" ClientInstanceName="ddlTipoRecurso" ID="ddlTipoRecurso" OnCallback="ddlTipoRecurso_Callback">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_TemFilho == &quot;S&quot;)
		ddlTipoRecurso.SetEnabled(false);
	s.SetValue(s.cp_codigoTipoRecurso);
}"
                                                                SelectedIndexChanged="function(s, e) {
	if('Pessoa' == ddlTipoRecurso.GetText())
	{
        txtValorHora.SetEnabled(true);
        txtValorUso.SetEnabled(true);
		txtUnidadeMedida.SetEnabled(false);
		txtUnidadeMedida.SetText('Hrs');
	}
	else if('Financeiro' == ddlTipoRecurso.GetText())
    {
        txtValorHora.SetEnabled(false);
        txtValorUso.SetEnabled(false);
		txtUnidadeMedida.SetEnabled(false);
		txtUnidadeMedida.SetText('R$');
		txtValorHora.SetText('');
		txtValorUso.SetText('');
    }
    else
    {
        txtValorHora.SetEnabled(true);
        txtValorUso.SetEnabled(true);
		txtUnidadeMedida.SetEnabled(true);
		txtUnidadeMedida.SetText('');
	}

	var codigoGrupoRecurso, codigoTipoRecurso, codigoGrupoSuperior;
	codigoTipoRecurso = parseInt(ddlTipoRecurso.GetValue());
	if (codigoTipoRecurso.toString() == &quot;NaN&quot;)
		codigoTipoRecurso = -1;

	codigoGrupoRecurso = txtNome.cpCodigoGrupoRecurso;
	if (codigoGrupoRecurso.toString() == &quot;NaN&quot;)
		codigoGrupoRecurso = -1;

	codigoGrupoSuperior = -1;

	ddlGrupoRecursoSuperior.PerformCallback(codigoTipoRecurso + &quot;;&quot; + codigoGrupoRecurso +&quot;;&quot; + codigoGrupoSuperior)
}"></ClientSideEvents>

                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                        </dxe:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                        <dxe:ASPxLabel runat="server" Text="Grupo Superior:" ClientInstanceName="lblGerente" ID="lblGerente"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" EnableCallbackMode="True" ValueType="System.Int32" Width="100%" ClientInstanceName="ddlGrupoRecursoSuperior" EnableClientSideAPI="True" ID="ddlGrupoRecursoSuperior" OnCallback="ddlGrupoRecursoSuperior_Callback">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_TemFilho == 'S')
		ddlTipoRecurso.SetEnabled(false);	

	if( ddlGrupoRecursoSuperior.cpCodigoGrupoSuperior )
	{
		var codigo = parseInt(ddlGrupoRecursoSuperior.cpCodigoGrupoSuperior);
		if (codigo.toString() == -1)
			ddlGrupoRecursoSuperior.SetSelectedIndex(0);
		else
			ddlGrupoRecursoSuperior.SetValue(codigo);
	}
	else
		ddlGrupoRecursoSuperior.SetSelectedIndex(0);
}"></ClientSideEvents>

                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                        </dxe:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-bottom: 10px">
                                                        <dxrp:ASPxRoundPanel ID="rdValor" runat="server" ClientInstanceName="rdValor"
                                                            HeaderText="Valor (R$)" Width="400px">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                        Text="Unidade de Medida:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">&nbsp;</td>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                        Text="Unitário:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px"></td>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server"
                                                                                        Text="Uso:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxTextBox ID="txtUnidadeMedida" runat="server"
                                                                                        ClientInstanceName="txtUnidadeMedida"
                                                                                        Width="120px" MaxLength="20">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td style="width: 10px">&nbsp;</td>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxSpinEdit ID="txtValorHora" runat="server"
                                                                                        ClientInstanceName="txtValorHora" DecimalPlaces="2" DisplayFormatString="n2"
                                                                                        Width="120px">
                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                        </SpinButtons>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td style="width: 10px"></td>
                                                                                <td style="width: 120px">
                                                                                    <dxe:ASPxSpinEdit ID="txtValorUso" runat="server"
                                                                                        ClientInstanceName="txtValorUso" DecimalPlaces="2" DisplayFormatString="n2"
                                                                                        Width="120px">
                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                        </SpinButtons>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxrp:ASPxRoundPanel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblObservacoes" runat="server"
                                                            ClientInstanceName="lblObservacoes"
                                                            Text="Detalhe:">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtDetalhe" runat="server" ClientInstanceName="txtDetalhe"
                                                            Rows="5" Width="100%">
                                                            <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"
                                                                KeyUp="function(s, e) {
	//limitaASPxMemo(s, 250);
}" />

                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                        <dxe:ASPxLabel ID="lblContadorMemoDetalhe" runat="server"
                                                            ClientInstanceName="lblContadorMemoDetalhe" Font-Bold="True"
                                                            ForeColor="#999999">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                            ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	// registra a informação na propriedade do campo para uso durante a gravação
	var codGrpSup = parseInt(ddlGrupoRecursoSuperior.GetValue());
	if ( (codGrpSup.toString() == &quot;NaN&quot;) || (codGrpSup == -1) )
	    hfGeral.Set('CodigoGrupoSuperior', '');
	else 
	    hfGeral.Set('CodigoGrupoSuperior', codGrpSup);

	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                            <Paddings Padding="0px" />
                                                                            <ClientSideEvents Click="function(s, e) {
	// registra a informa&#231;&#227;o na propriedade do campo para uso durante a grava&#231;&#227;o
	var codGrpSup = parseInt(ddlGrupoRecursoSuperior.GetValue());
	if ( (codGrpSup.toString() == &quot;NaN&quot;) || (codGrpSup == -1) )
	    hfGeral.Set(&#39;CodigoGrupoSuperior&#39;, &#39;&#39;);
	else 
	    hfGeral.Set(&#39;CodigoGrupoSuperior&#39;, codGrpSup);

	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                            ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                PaddingRight="0px" PaddingTop="0px" />
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
       if(s.cp_erro == &quot;&quot;)
       {
             if(s.cp_sucesso != &quot;&quot;)
             {
                    window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null, 4000);
             }       
        }
        else
        {
              if(s.cp_erro != &quot;&quot;)
              {
                      window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);
               }
        }
        s.cp_sucesso = &quot;&quot;;
       s.cp_erro = &quot;&quot;;
        onClick_btnCancelar();
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>

    </table>
</asp:Content>
