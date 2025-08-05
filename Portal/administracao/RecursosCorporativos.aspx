<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="RecursosCorporativos.aspx.cs" Inherits="administracao_RecursosCorporativos"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                             <div id="divGrid" style="visibility:hidden;padding-top:5px">
                                 <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
                            </dxtv:ASPxLoadingPanel>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoRecursoCorporativo"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                    CustomButtonClick="function(s, e) {
	gvDados.SetFocusedRowIndex(e.visibleIndex);
btnSalvar1.SetVisible(true);
     if(e.buttonID == 'btnNovo')
     {
		TipoOperacao = 'Incluir';
		hfGeral.Set('TipoOperacao', TipoOperacao);
        onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
	 }
     else if(e.buttonID == 'btnEditar')
     {
        lpLoading.Show();
		TipoOperacao = 'Editar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
        onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
	 }
     else if(e.buttonID == 'btnExcluir')
     {
		onClickBarraNavegacao('Excluir', gvDados, pcDados);
     }
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
        lpLoading.Show();
		TipoOperacao = 'Consultar';
		hfGeral.Set('TipoOperacao', TipoOperacao);

        OnGridFocusedRowChanged(gvDados,true);
		btnSalvar1.SetVisible(false);
		pcDados.Show();
     }
	else if(e.buttonID == 'btnCalendario')
	{
		onBtnCalendario_Click(gvDados, e);
	}	
	else if(e.buttonID == 'btnInteressados')
	{
		OnGridFocusedRowChangedPopup(gvDados);
	}
}" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="180px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnCalendario" Text="Alterar Calend&#225;rio">
                                                <Image Url="~/imagens/Calendario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnInteressados" Text="Administrar Recursos da Equipe">
                                                <Image ToolTip="Administrar Recursos da Equipe" Url="~/imagens/Perfis/Perfil_Permissoes.png">
                                                </Image>
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
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeRecursoCorporativo" Name="NomeRecursoCorporativo"
                                        Caption="Recurso" VisibleIndex="1" Width="200px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoGrupo" Name="DescricaoGrupo" Caption="Grupo"
                                        VisibleIndex="2" Width="250px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="estaAtivo" Name="estaAtivo" Width="60px"
                                        Caption="Ativo" Visible="False" VisibleIndex="6">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaRecursoAtivo"
                                        Name="IndicaRecursoAtivo" VisibleIndex="7" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoCalendario" Name="CodigoCalendario"
                                        Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoRecurso" Visible="False"
                                        VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoGrupoRecurso" Caption="Codigo Grupo Recurso"
                                        Visible="False" VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioDisponibilidadeRecurso" Caption="In&#237;cio Disponibilidade Recurso"
                                        Visible="False" VisibleIndex="11">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoDisponibilidadeRecurso" Caption="T&#233;rmino Disponibilidade Recurso"
                                        Visible="False" VisibleIndex="12">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CustoHora" Caption="Despesa Hora" Visible="False"
                                        VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CustoUso" Caption="Despesa Uso" Visible="False"
                                        VisibleIndex="14">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Caption="Codigo Unidade Negocio"
                                        Visible="False" VisibleIndex="15">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Anotacoes" Caption="Anota&#231;&#245;es"
                                        Visible="False" VisibleIndex="16">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="UnidadeMedidaRecurso" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Unidade Negócio" FieldName="NomeUnidadeNegocio"
                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="250px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="DescricaoTipoRecurso" ShowInCustomizationForm="True"
                                        VisibleIndex="4" Width="100px">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataComboBoxColumn Caption="Genérico"
                                        FieldName="IndicaRecursoGenerico" Name="IndicaRecursoGenerico"
                                        ShowInCustomizationForm="True" VisibleIndex="17" Width="150px">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxtv:ListEditItem Text="Todos" Value="" />
                                                <dxtv:ListEditItem Text="Sim" Value="Sim" />
                                                <dxtv:ListEditItem Text="Não" Value="Não" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxtv:GridViewDataComboBoxColumn>
                                    <dxtv:GridViewDataComboBoxColumn Caption="Equipe" FieldName="IndicaEquipe"
                                        Name="IndicaEquipe" ShowInCustomizationForm="True" VisibleIndex="18"
                                        Width="100px">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxtv:ListEditItem Text="Todos" Value="" />
                                                <dxtv:ListEditItem Text="Sim" Value="Sim" />
                                                <dxtv:ListEditItem Text="Não" Value="Não" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxtv:GridViewDataComboBoxColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                <SettingsPager PageSize="100">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupedColumns="True"
                                    ShowFilterRow="True"></Settings>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-inativo">
                                                        <span></span>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-inativo">
                                                        <dxe:ASPxLabel runat="server" Text="<%# Resources.traducao.RecursosCorporativos_recursos_inativos %>" ClientInstanceName="lblDescricaoNaoAtiva"
                                                            Font-Bold="False" ID="lblDescricaoNaoAtiva">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                            </dxwgv:ASPxGridView>
                                 </div>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px"
                                ID="pcDados">
                                <ClientSideEvents BeginCallback="function(s, e) {
	lpLoading.Show();
}" EndCallback="function(s, e) {
	lpLoading.Hide();
}" />
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td width="200">
                                                                        <dxe:ASPxLabel runat="server" Text="Tipo:" ID="ASPxLabel1">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 40px"></td>
                                                                    <td style="width: 40px"></td>
                                                                    <td style="width: 40px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="DescricaoTipoRecurso"
                                                                            ValueField="CodigoTipoRecurso" Width="100%" ClientInstanceName="ddlTipoRecurso"
                                                                            ID="ddlTipoRecurso">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) 
{
    //debugger;
	pn_ddlGrupo.PerformCallback(s.GetValue() + '|Incluir');
    txtResp.SetText('');
	//if('Pessoa' == ddlTipoRecurso.GetText())
	if('1' == ddlTipoRecurso.GetValue())
	{
        txtValorHora.SetEnabled(true);
        txtValorUso.SetEnabled(true);
    	ddlDisponibilidadeInicio.SetEnabled(true);
        ddlDisponibilidadeTermino.SetEnabled(true);
		ddlResponsavel.SetVisible(true);
        txtResp.SetVisible(false);
		txtUnidadeMedida.SetEnabled(false);
		txtUnidadeMedida.SetText('Hrs');
		lblObservacoes.SetText('Conhecimentos, Habilidades e Competências:');
        chkGenerico.SetEnabled(true);
        chkEquipe.SetEnabled(true);
        chkGenerico.SetChecked(false);
        chkEquipe.SetChecked(false);
	}
	//else if('Financeiro' == ddlTipoRecurso.GetText())
	else if('3' == ddlTipoRecurso.GetValue())
    {
        txtValorHora.SetEnabled(false);
        txtValorUso.SetEnabled(false);
		ddlDisponibilidadeInicio.SetEnabled(false);
        ddlDisponibilidadeTermino.SetEnabled(false);
        ddlResponsavel.SetVisible(false);
        txtResp.SetVisible(true);
        txtResp.SetEnabled(true);
		txtUnidadeMedida.SetEnabled(false);
		txtUnidadeMedida.SetText('R$');
		txtValorHora.SetText('');
		txtValorUso.SetText('');
		ddlDisponibilidadeInicio.SetText('');
		ddlDisponibilidadeTermino.SetText('');
		lblObservacoes.SetText('Observações:');
        chkGenerico.SetEnabled(false);
        chkEquipe.SetEnabled(false);
        chkGenerico.SetChecked(false);
        chkEquipe.SetChecked(false);
    }
    else //Equipamento
    {
        txtValorHora.SetEnabled(true);
        txtValorUso.SetEnabled(true);
    	ddlDisponibilidadeInicio.SetEnabled(true);
        ddlDisponibilidadeTermino.SetEnabled(true);
        ddlResponsavel.SetVisible(false);
        txtResp.SetVisible(true);
        txtResp.SetEnabled(true);
		txtUnidadeMedida.SetEnabled(true);
		txtUnidadeMedida.SetText('');
		lblObservacoes.SetText('Observações:');
        chkGenerico.SetEnabled(true);
        chkEquipe.SetEnabled(false);
        chkGenerico.SetChecked(false);
        chkEquipe.SetChecked(false);
	}
}"></ClientSideEvents>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxe:ASPxCheckBox runat="server" Text="Ativo" ClientInstanceName="chkAtivo"
                                                                            ID="chkAtivo">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxe:ASPxCheckBox runat="server" Text="Genérico" ClientInstanceName="chkGenerico"
                                                                            ID="chkGenerico">
                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
 trataSelecaoGenerico();
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxe:ASPxCheckBox runat="server" Text="Equipe" ClientInstanceName="chkEquipe"
                                                                            ID="chkEquipe">
                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
trataSelecaoEquipe();
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Nome:" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                            TextField="NomeUsuario" ValueField="CodigoUsuario" TextFormatString="{0}" MaxLength="100"
                                                            Width="100%" ClientInstanceName="ddlResponsavel"
                                                            ID="ddlResponsavel">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="NomeUsuario" Caption="Nome"></dxe:ListBoxColumn>
                                                                <dxe:ListBoxColumn FieldName="EMail" Caption="E-Mail"></dxe:ListBoxColumn>
                                                            </Columns>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxComboBox>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtResp"
                                                            ClientVisible="False" ID="txtResp">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 420px">
                                                                        <dxe:ASPxLabel runat="server" Text="Grupo:"
                                                                            ID="ASPxLabel3">
                                                                        </dxe:ASPxLabel>

                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Unidade Neg&#243;cio:"
                                                                            ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                         <dxtv:ASPxCallbackPanel ID="pn_ddlGrupo" runat="server" ClientInstanceName="pn_ddlGrupo" Width="100%" OnCallback="pn_ddlGrupo_Callback">
                                                                             <ClientSideEvents EndCallback="function(s, e) {
                 hfCodigoGrupo.Set('hfCodigoGrupo', s.cp_Grupo);
	ddlGrupo.SetValue(s.cp_Grupo);
}" />
                                                                            <PanelCollection>
                                                                                <dxtv:PanelContent runat="server">
                                                                                      <dxtv:ASPxHiddenField ID="hfCodigoGrupo" runat="server" ClientInstanceName="hfCodigoGrupo">
                                                                                      </dxtv:ASPxHiddenField>
                                                                                      <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="DescricaoGrupo"
                                                                            ValueField="CodigoGrupoRecurso" Width="100%" ClientInstanceName="ddlGrupo"
                                                                            ID="ddlGrupo">
                                                                          
                                                                                          <ClientSideEvents SelectedIndexChanged="function(s, e) {
    hfCodigoGrupo.Set('hfCodigoGrupo', s.GetValue());
}" />
                                                                          
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                                </dxtv:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxtv:ASPxCallbackPanel>
                                                                      
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="padding-left: 5px;">
                                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                            TextFormatString="{1}" Width="100%" ClientInstanceName="ddlUnidadeNegocio"
                                                                            ID="ddlUnidadeNegocio">
                                                                            <Columns>
                                                                                <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="100px" Caption="Sigla">
                                                                                </dxe:ListBoxColumn>
                                                                                <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="300px" Caption="Unidade">
                                                                                </dxe:ListBoxColumn>
                                                                            </Columns>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 420px; padding-right: 5px;">
                                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Valor (R$)" Width="100%" ClientInstanceName="rdValor"
                                                                            ID="rdValor">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent runat="server">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 120px">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                                        Text="Unidade de Medida:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                                <td style="width: 120px">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                        Text="Unitário:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td style="width: 120px">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Uso:" ID="ASPxLabel11">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 120px">
                                                                                                    <dxe:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientInstanceName="txtUnidadeMedida"
                                                                                                        Width="120px" MaxLength="20">
                                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                        </ValidationSettings>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                                <td style="width: 120px; padding-right: 5px;">
                                                                                                    <dxe:ASPxSpinEdit ID="txtValorHora" runat="server" ClientInstanceName="txtValorHora"
                                                                                                        Width="120px" DecimalPlaces="2" DisplayFormatString="n2">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                        </ValidationSettings>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td style="width: 120px">
                                                                                                    <dxe:ASPxSpinEdit runat="server" Width="120px" ClientInstanceName="txtValorUso"
                                                                                                        ID="txtValorUso" DecimalPlaces="2" DisplayFormatString="n2">
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
                                                                    <td></td>
                                                                    <td>
                                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Disponibilidade" Width="100%" ClientInstanceName="rdDisponibilidade"
                                                                            ID="rdDisponibilidade">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent runat="server">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="In&#237;cio:"
                                                                                                        ID="ASPxLabel8">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="T&#233;rmino:"
                                                                                                        ID="ASPxLabel9">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                        Width="120px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlDisponibilidadeInicio"
                                                                                                        ID="ddlDisponibilidadeInicio">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                        Width="120px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlDisponibilidadeTermino"
                                                                                                        ID="ddlDisponibilidadeTermino">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxrp:ASPxRoundPanel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Conhecimentos, Habilidades e Competências:"
                                                            ID="lblObservacoes" ClientInstanceName="lblObservacoes">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo runat="server" Rows="5" Width="100%" ClientInstanceName="memoAnotacoes"
                                                            ID="memoAnotacoes">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                        <dxe:ASPxLabel ID="lblContadorMemo" runat="server"
                                                            ClientInstanceName="lblContadorMemo" Font-Bold="True"
                                                            Font-Size="7pt" ForeColor="#999999">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table id="Table1" class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1"
                                                                            Text="Salvar" Width="90px"
                                                                            ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
	                    if(verificarDadosPreenchidos())
		                    if (window.onClick_btnSalvar)
	    	                    onClick_btnSalvar();
	                    else
		                    return false;
                    }"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px"
                                                                            ID="btnFechar">
                                                                            <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
                        if (window.onClick_btnCancelar)
                           onClick_btnCancelar();
                    }"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
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
                            <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackLimpaSessao" ID="callbackLimpaSessao"
                                OnCallback="callback_Callback">
                            </dxcb:ASPxCallback>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_WhereLov != null &amp;&amp; s.cp_WhereLov != '')
		hfGeral.Set('hfWheregetLov_NomeValor', s.cp_WhereLov);	

	if(s.cp_lovCodigoResponsavel != '-1')
	{
        hfGeral.Set('lovMostrarPopPup', s.cp_lovMostrarPopPup);
		hfGeral.Set('lovCodigoResponsavel', s.cp_lovCodigoResponsavel);
	}
	onEndLocal_pnCallback();	
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                                Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                                ExportEmptyDetailGrid="True" PreserveGroupRowStates="False">
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
</asp:Content>
