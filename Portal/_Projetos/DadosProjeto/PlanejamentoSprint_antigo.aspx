<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanejamentoSprint_antigo.aspx.cs" Inherits="_Projetos_DadosProjeto_PlanejamentoSprint_antigo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .style3 {
            width: 150px;
        }

        .style4 {
            width: 190px;
        }

        .style5 {
            width: 130px;
        }

        .style6 {
            height: 7px;
        }

        .style7 {
            width: 100%;
        }

        .style10 {
            height: 11px;
        }

        .style12 {
            width: 113px;
        }

        .style13 {
            width: 100px;
        }

        .style14 {
            width: 115px;
        }

        .auto-style1 {
            height: 36px;
        }
        td.classe-tarefas > a {
            color: #575757;
            font-size: 14px !important;
            font-weight: 300 !important;
        }
        td.classe-tarefas > a:nth-child(1):after {
            content: " tarefa(s)";
        }
    </style>
</head>
<body style="margin: 5px">
    <form id="form1" runat="server">
        <div>

            <dxcp:ASPxCallbackPanel ID="pnCallbackGeral" runat="server"
                ClientInstanceName="pnCallbackGeral" OnCallback="pnCallbackGeral_Callback"
                Width="100%">
                <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg); callbackCusto_Callback();
}" />
                <PanelCollection>
                    <dxcp:PanelContent runat="server">
                        <dxtv:ASPxPageControl ID="pcSprint" runat="server" ActiveTabIndex="1"
                            ClientInstanceName="pcSprint" 
                            Width="100%">
                            <TabPages>
                                <dxtv:TabPage Name="tbPrincipal" Text="Principal">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable"
                                                width="100%">
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="lblSprint" runat="server"
                                                            Text="Sprint:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxTextBox ID="txtSprint" runat="server" ClientInstanceName="txtSprint"
                                                             MaxLength="255" Width="100%">
                                                            <ClientSideEvents Init="function(s, e) {
	txtSprint1.SetText(s.GetText());
	txtSprint2.SetText(s.GetText());
}"
                                                                TextChanged="function(s, e) {
	txtSprint1.SetText(s.GetText());
	txtSprint2.SetText(s.GetText());
}" />
                                                            <ValidationSettings CausesValidation="True" Display="Dynamic"
                                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido"
                                                                ValidationGroup="MKE">
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dxtv:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"
                                                            Text="Objetivos:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxMemo ID="mmObjetivos" runat="server" ClientInstanceName="mmObjetivos"
                                                             Rows="8" Width="100%">
                                                            <ValidationSettings CausesValidation="True" Display="Dynamic"
                                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido"
                                                                ValidationGroup="MKE">
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxMemo>
                                                        <dxtv:ASPxLabel ID="lbl_mmObjetivos" runat="server"
                                                            ClientInstanceName="lbl_mmObjetivos" Font-Bold="True"
                                                            Font-Size="7pt" ForeColor="#999999">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="style3">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"  Text="Início:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel7" runat="server"  Text="Término:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style3" style="padding-right: 10px">
                                                                        <dxtv:ASPxDateEdit ID="ddlInicio" runat="server" ClientInstanceName="ddlInicio"  UseMaskBehavior="True" Width="100%">
                                                                            <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido" ValidationGroup="MKE">
                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="style3" style="padding-right: 10px">
                                                                        <dxtv:ASPxDateEdit ID="ddlTermino" runat="server" ClientInstanceName="ddlTermino"  UseMaskBehavior="True" Width="100%">
                                                                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetValue() &lt; ddlInicio.GetValue())
	{
		e.isValid = false;
		e.errorText = 'A data de término não pode ser menor que a data de início!';
	}
}" />
                                                                            <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido" ValidateOnLeave="False" ValidationGroup="MKE">
                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxDateEdit>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="ASPxLabel9" runat="server"
                                                            Text="Anotações:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxMemo ID="mmAnotacoes" runat="server" ClientInstanceName="mmAnotacoes"
                                                             Rows="8" Width="100%">
                                                            <ValidationSettings CausesValidation="True" Display="Dynamic"
                                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido"
                                                                ValidationGroup="MKE">
                                                                <RequiredField ErrorText="Campo Obrigatório" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxMemo>
                                                        <dxtv:ASPxLabel ID="lbl_mmAnotacoes" runat="server"
                                                            ClientInstanceName="lbl_mmAnotacoes" Font-Bold="True"
                                                            Font-Size="7pt" ForeColor="#999999">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <dxtv:ASPxButton ID="btnSalvarSprint" runat="server" AutoPostBack="False"
                                                            ClientInstanceName="btnSalvarSprint" 
                                                            Text="Salvar" ValidationGroup="MKE" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;	
	
	if(ASPxClientEdit.ValidateGroup('MKE', true) == true)
		callback.PerformCallback();
}" />
                                                            <Paddings Padding="0px" />
                                                        </dxtv:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Name="tbEquipe" Text="Equipe">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable"
                                                width="100%">
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="lblSprint0" runat="server"
                                                            Text="Sprint:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxTextBox ID="txtSprint1" runat="server" ClientEnabled="False"
                                                            ClientInstanceName="txtSprint1" 
                                                            MaxLength="500" Width="100%">
                                                        </dxtv:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxCallbackPanel ID="pnCallback" runat="server"
                                                            ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback" Width="100%">
                                                            <ClientSideEvents EndCallback="function(s, e) 
{
                                                       callbackCusto.PerformCallback();
 callbackDadosEquipe.PerformCallback();

	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	 if(&quot;Incluir&quot; == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado('Recurso incluído com sucesso!');		
	}
  	  else if(&quot;Editar&quot; == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado('Recurso alterado com sucesso!');	
}
    	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
	{
		mostraDivSalvoPublicado('Recurso excluído com sucesso!');
	}
	callbackDadosPlanejamento.PerformCallback();

}" />
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                                    </dxtv:ASPxHiddenField>
                                                                    <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                                                        ClientInstanceName="gvDados" 
                                                                        KeyFieldName="CodigoRecursoIteracao"
                                                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%" OnCustomSummaryCalculate="gvDados_CustomSummaryCalculate" OnSummaryDisplayText="gvDados_SummaryDisplayText">
                                                                        <ClientSideEvents CustomButtonClick="function(s, e) 
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
"
                                                                            FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                                            EndCallback="function(s, e) {
	callbackDadosEquipe.PerformCallback();
}" />
                                                                        <Columns>
                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                                                VisibleIndex="0" Width="100px">
                                                                                <CustomButtons>
                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                                        </Image>
                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                        </Image>
                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                                                        </Image>
                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                </CustomButtons>
                                                                                <HeaderTemplate>
                                                                                    <dxtv:ASPxMenu ID="menu_equipe" runat="server" BackColor="Transparent" ClientInstanceName="menu_equipe" ItemSpacing="5px" OnInit="menu_equipe_Init" OnItemClick="menu_equipe_ItemClick">
                                                                                        <Paddings Padding="0px" />
                                                                                        <Items>
                                                                                            <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
                                                                                            <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                <Items>
                                                                                                    <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                </Items>
                                                                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
                                                                                            <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                                                <Items>
                                                                                                    <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                        <Image IconID="save_save_16x16">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                    <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                        <Image IconID="actions_reset_16x16">
                                                                                                        </Image>
                                                                                                    </dxtv:MenuItem>
                                                                                                </Items>
                                                                                                <Image Url="~/imagens/botoes/layout.png">
                                                                                                </Image>
                                                                                            </dxtv:MenuItem>
                                                                                        </Items>
                                                                                        <ItemStyle Cursor="pointer">
                                                                                            <HoverStyle>
                                                                                                <Border BorderStyle="None" />
                                                                                            </HoverStyle>
                                                                                            <Paddings Padding="0px" />
                                                                                        </ItemStyle>
                                                                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                            <SelectedStyle>
                                                                                                <Border BorderStyle="None" />
                                                                                            </SelectedStyle>
                                                                                        </SubMenuItemStyle>
                                                                                        <Border BorderStyle="None" />
                                                                                    </dxtv:ASPxMenu>
                                                                                </HeaderTemplate>
                                                                            </dxtv:GridViewCommandColumn>
                                                                            <dxtv:GridViewDataTextColumn Caption="Recurso" FieldName="NomeRecurso"
                                                                                Name="NomeRecurso" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                                Width="220px">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn Caption="Papel"
                                                                                FieldName="DescricaoTipoPapelRecurso" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2">
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn Caption="Dedicação" FieldName="PercentualAlocacao"
                                                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                                <PropertiesTextEdit DisplayFormatString="{0:n0}%">
                                                                                </PropertiesTextEdit>
                                                                                <Settings AllowAutoFilter="False" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <CellStyle HorizontalAlign="Right">
                                                                                </CellStyle>
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn Caption="Horas Dedicadas"
                                                                                FieldName="HorasDedicadas" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                Width="120px">
                                                                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                                                </PropertiesTextEdit>
                                                                                <Settings AllowAutoFilter="False" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <CellStyle HorizontalAlign="Right">
                                                                                </CellStyle>
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoTipoPapelRecursoIteracao"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn FieldName="CodigoRecursoCorporativo"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn FieldName="CustoUnitario"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                            </dxtv:GridViewDataTextColumn>
                                                                            <dxtv:GridViewDataTextColumn FieldName="ReceitaUnitaria"
                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                            </dxtv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowSort="False" SelectionStoringMode="PerformanceOptimized" AllowSelectSingleRowOnly="True" />
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" ShowFooter="True" />
                                                                        <TotalSummary>
                                                                            <dxtv:ASPxSummaryItem FieldName="HorasDedicadas" ShowInColumn="Horas Dedicadas" ShowInGroupFooterColumn="Horas Dedicadas" SummaryType="Sum" DisplayFormat="{0:n2}" />
                                                                            <dxtv:ASPxSummaryItem FieldName="PercentualAlocacao" ShowInColumn="Dedicação" ShowInGroupFooterColumn="Dedicação" SummaryType="Custom" DisplayFormat="{0}" />
                                                                        </TotalSummary>
                                                                    </dxtv:ASPxGridView>

                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style10"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        <dxtv:ASPxCallbackPanel ID="callbackDadosEquipe" runat="server" ClientInstanceName="callbackDadosEquipe" OnCallback="callbackDadosEquipe_Callback"     Width="450px">
                                                            <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel23" runat="server"  Text="Total Horas Dedicadas:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel24" runat="server"  Text="Produtividade:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel25" runat="server"  Text="Capacidade da Equipe:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtTotalHorasEquipe" runat="server" ClientInstanceName="txtTotalHorasEquipe" DisplayFormatString="{0:n2}"  MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtFatorProdutividadeEquipe" runat="server" ClientEnabled="False" ClientInstanceName="txtFatorProdutividadeEquipe" DisplayFormatString="{0:n2}"  MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtCapacidade_AbaEquipe" runat="server" ClientEnabled="False" ClientInstanceName="txtCapacidade_AbaEquipe" DisplayFormatString="{0:n2}"  MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                                <dxtv:TabPage Name="tbItens" Text="Itens">
                                    <ContentCollection>
                                        <dxtv:ContentControl runat="server">
                                            <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable">
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxLabel ID="lblSprint1" runat="server"
                                                            Text="Sprint:">
                                                        </dxtv:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxTextBox ID="txtSprint2" runat="server" ClientEnabled="False"
                                                            ClientInstanceName="txtSprint2" 
                                                            MaxLength="500" Width="100%">
                                                        </dxtv:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gvItens" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="gvItens" 
                                                            KeyFieldName="CodigoItem"
                                                            OnCustomButtonInitialize="gvItens_CustomButtonInitialize"
                                                            OnCustomCallback="gvItens_CustomCallback"
                                                            OnHtmlDataCellPrepared="gvItens_HtmlDataCellPrepared" Width="100%">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) 
{
     gvItens.SetFocusedRowIndex(e.visibleIndex);
     
     if(e.buttonID == &quot;btnEditarItem&quot;)
     {
	OnGridItemFocusedRowChanged(s, true);
	pcItens.Show();

     }
     else if(e.buttonID == &quot;btnExcluirItem&quot;)
     {
	if(confirm('Deseja excluir o item?'))
		gvItens.PerformCallback('Excluir');
     }
}
"
                                                                EndCallback="function(s, e) {
	if(s.cp_Msg != '')
	{
		mostraDivSalvoPublicado(s.cp_Msg);
		pcItens.Hide();
		callbackDadosPlanejamento.PerformCallback();
	}
}"
                                                                FocusedRowChanged="function(s, e) {
	OnGridItemFocusedRowChanged(s);
}" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Width="80px">
                                                                    <CustomButtons>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditarItem" Text="Editar">
                                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnExcluirItem" Text="Excluir">
                                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <HeaderTemplate>
                                                                        <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
                                                                            <Paddings Padding="0px" />
                                                                            <Items>
                                                                                <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                    <Items>
                                                                                        <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                        <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                        <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                        <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                    </Items>
                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                                    <Items>
                                                                                        <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                            <Image IconID="save_save_16x16">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                        <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                            <Image IconID="actions_reset_16x16">
                                                                                            </Image>
                                                                                        </dxtv:MenuItem>
                                                                                    </Items>
                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                            </Items>
                                                                            <ItemStyle Cursor="pointer">
                                                                                <HoverStyle>
                                                                                    <Border BorderStyle="None" />
                                                                                </HoverStyle>
                                                                                <Paddings Padding="0px" />
                                                                            </ItemStyle>
                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                <SelectedStyle>
                                                                                    <Border BorderStyle="None" />
                                                                                </SelectedStyle>
                                                                            </SubMenuItemStyle>
                                                                            <Border BorderStyle="None" />
                                                                        </dxtv:ASPxMenu>
                                                                    </HeaderTemplate>
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Item" FieldName="TituloItem"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Importância" FieldName="Importancia"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                                    <Settings AllowAutoFilter="False" />
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Estimativa" FieldName="EsforcoPrevisto"
                                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                    <Settings AllowAutoFilter="False" />
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Complexidade"
                                                                    FieldName="DescricaoComplexidade" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="110px">
                                                                    <Settings AllowAutoFilter="False" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn FieldName="DetalheItem"
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Tarefas" FieldName="QuantidadeTarefa"
                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="120px">
                                                                    <PropertiesTextEdit DisplayFormatString="{0}">
                                                                    </PropertiesTextEdit>
                                                                    <Settings AllowAutoFilter="False" />
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle HorizontalAlign="Right" CssClass="classe-tarefas">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                                                        </dxtv:ASPxGridView>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style10"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxCallbackPanel ID="callbackDadosPlanejamento" runat="server"
                                                            ClientInstanceName="callbackDadosPlanejamento"
                                                            OnCallback="callbackDadosPlanejamento_Callback"
                                                            Width="450px">
                                                            <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                    Text="Capacidade Equipe:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel15" runat="server"
                                                                                    Text="Estimativa:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel16" runat="server"
                                                                                    Text="Saldo:">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtCapacidadeEquipe" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtCapacidadeEquipe" DisplayFormatString="{0:n2}"
                                                                                     MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtEstimativa" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtEstimativa" DisplayFormatString="{0:n2}"
                                                                                     MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxtv:ASPxTextBox ID="txtSaldo" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtSaldo" DisplayFormatString="{0:n2}"
                                                                                    MaxLength="500" Width="130px">
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnPublicar" runat="server" AutoPostBack="False"
                                                                            ClientInstanceName="btnPublicar" 
                                                                            Text="Publicar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(confirm('Ao publicar, os dados não poderão ser alterados. Deseja continuar?'))
pnCallbackGeral.PerformCallback();
}" />
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                PaddingRight="0px" PaddingTop="0px" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxtv:ContentControl>
                                    </ContentCollection>
                                </dxtv:TabPage>
                            </TabPages>
                            <TabStyle >
                            </TabStyle>
                            <ContentStyle>
                                <Paddings Padding="10px" />
                            </ContentStyle>
                        </dxtv:ASPxPageControl>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>

        </div>

        <dxtv:ASPxPopupControl ID="pcItens" runat="server" ClientInstanceName="pcItens"
            CloseAction="None"  HeaderText="Detalhes"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="850px">
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
            <HeaderStyle Font-Bold="True" />
            <ContentCollection>
                <dxtv:PopupControlContentControl runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel12" runat="server"
                                    Text="Item:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxTextBox ID="txtTituloItem" runat="server" ClientEnabled="False"
                                    ClientInstanceName="txtTituloItem" 
                                    MaxLength="500" Width="100%">
                                </dxtv:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6"></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style7">
                                    <tr>
                                        <td class="style13">
                                            <dxtv:ASPxLabel ID="ASPxLabel13" runat="server"
                                                Text="Importância:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td class="style14">
                                            <dxtv:ASPxLabel ID="ASPxLabel20" runat="server"
                                                Text="Estimativa:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel21" runat="server"
                                                Text="Complexidade:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style13" style="padding-right: 10px">
                                            <dxtv:ASPxTextBox ID="txtImportancia" runat="server" ClientEnabled="False"
                                                ClientInstanceName="txtImportancia" 
                                                MaxLength="500" Width="100%">
                                            </dxtv:ASPxTextBox>
                                        </td>
                                        <td class="style14" style="padding-right: 10px">
                                            <dxtv:ASPxSpinEdit ID="txtEsforcoPrevisto" runat="server"
                                                AllowMouseWheel="False" ClientInstanceName="txtEsforcoPrevisto"
                                                DisplayFormatString="{0:n0}"
                                                Number="0" Width="100%" NumberType="Integer">
                                                <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxSpinEdit>
                                        </td>
                                        <td>
                                            <dxtv:ASPxComboBox ID="ddlComplexidade" runat="server" ClientEnabled="False" ClientInstanceName="ddlComplexidade"  Width="130px">
                                                <Items>
                                                    <dxtv:ListEditItem Text="Baixa" Value="0" />
                                                    <dxtv:ListEditItem Text="Média" Value="1" />
                                                    <dxtv:ListEditItem Text="Alta" Value="2" />
                                                    <dxtv:ListEditItem Text="Muito Alta" Value="3" />
                                                </Items>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1"></td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel22" runat="server"
                                    Text="Descrição:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxMemo ID="mmDescricaoItem" runat="server" ClientEnabled="False"
                                    ClientInstanceName="mmDescricaoItem" 
                                    Rows="10" Width="100%">
                                    <ValidationSettings CausesValidation="True" Display="Dynamic"
                                        ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido"
                                        ValidationGroup="MKE">
                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxButton ID="btnSalvarItem" runat="server" AutoPostBack="False"
                                                    ClientInstanceName="btnSalvarItem" 
                                                    Text="Salvar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(validaCamposFormularioItem() == '')
   		gvItens.PerformCallback('Editar');
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td>
                                                <dxtv:ASPxButton ID="btnFecharItem" runat="server" AutoPostBack="False"
                                                    ClientInstanceName="btnFecharItem" 
                                                    Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
pcItens.Hide();
}" />
                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                        PaddingRight="0px" PaddingTop="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxtv:PopupControlContentControl>
            </ContentCollection>
        </dxtv:ASPxPopupControl>
        <dxtv:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="None"  HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="580px">
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
            <HeaderStyle Font-Bold="True" />
            <ContentCollection>
                <dxtv:PopupControlContentControl runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Recurso:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxComboBox ID="ddlRecurso" runat="server" ClientInstanceName="ddlRecurso"  TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
callbackCusto.PerformCallback();	
}" />
                                    <ItemStyle  />
                                </dxtv:ASPxComboBox>
                                <dxtv:ASPxTextBox ID="txtRecurso" runat="server" ClientEnabled="False" ClientInstanceName="txtRecurso" ClientVisible="False"  MaxLength="500" Width="100%">
                                </dxtv:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6"></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style7">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel10" runat="server"  Text="Papel:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxComboBox ID="ddlPapelRecurso" runat="server" ClientInstanceName="ddlPapelRecurso"  TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" Width="100%">
                                                <ItemStyle  />
                                            </dxtv:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style7">
                                    <tr>
                                        <td class="style12">
                                            <dxtv:ASPxLabel ID="ASPxLabel11" runat="server"  Text="% Dedicação:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td class="style12">
                                            <dxtv:ASPxLabel ID="ASPxLabel19" runat="server"  Text="Horas Dedicadas:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel17" runat="server"  Text="Custo por Hora:" Width="125px">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxtv:ASPxLabel ID="lblReceita" runat="server"  Text="Receita por Hora:" Width="125px" ClientInstanceName="lblReceita">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style12" style="padding-right: 10px">
                                            <dxtv:ASPxTextBox ID="txtPercentualDedicacao" runat="server" ClientInstanceName="txtPercentualDedicacao" DisplayFormatString="{0:n0}"  Width="100%">
                                                <ClientSideEvents ValueChanged="function(s, e) { calculaHoras();}" />
                                                <MaskSettings Mask="&lt;0..100&gt;" />
                                            </dxtv:ASPxTextBox>
                                        </td>
                                        <td class="style12" style="padding-right: 10px">
                                            <dxtv:ASPxTextBox ID="txtHorasDedicadas" runat="server" ClientEnabled="False" ClientInstanceName="txtHorasDedicadas" DisplayFormatString="{0:n2}"  Width="100%">
                                            </dxtv:ASPxTextBox>
                                        </td>
                                        <td style="padding-right: 10px">
                                            <dxtv:ASPxTextBox ID="txtCusto" runat="server" ClientEnabled="False" ClientInstanceName="txtCusto" DisplayFormatString="{0:n2}"  Width="100%">
                                                <MaskSettings IncludeLiterals="DecimalSymbol" />
                                            </dxtv:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dxtv:ASPxSpinEdit ID="txtReceita" runat="server" AllowMouseWheel="False" ClientInstanceName="txtReceita" DecimalPlaces="2" DisplayFormatString="{0:n2}"  Number="0" Width="100%">
                                                <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"  Text="Salvar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
 
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar(); 
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td></td>
                                            <td>
                                                <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"  Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxtv:PopupControlContentControl>
            </ContentCollection>
        </dxtv:ASPxPopupControl>

        <dxcp:ASPxCallback ID="callback" runat="server"
            ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	//
	mostraDivSalvoPublicado(s.cp_Msg);
	pnCallback.PerformCallback();
}" />
        </dxcp:ASPxCallback>

        <dxcp:ASPxCallback ID="callbackCusto" runat="server"
            ClientInstanceName="callbackCusto" OnCallback="callbackCusto_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
         var possuiHoras = (s.cp_HorasDedicadas != null &amp;&amp; s.cp_HorasDedicadas != '');
         var possuiPercentual = (txtPercentualDedicacao.GetValue() != null);
         if (TipoOperacao == 'Editar') 
         {
                 if (valorPercentualAntigo != 0)
                 {
                           txtHorasDedicadas.SetValue(txtPercentualDedicacao.GetValue() * txtHorasDedicadas.GetValue() / valorPercentualAntigo);
                 }                         
                else
               {
                         txtHorasDedicadas.SetValue(0);
               }
                valorPercentualAntigo = txtPercentualDedicacao.GetValue();
          }
          else 
         {
                txtCusto.SetValue(s.cp_CustoUnitario);  
                if (possuiHoras &amp;&amp; possuiPercentual)
                {
                      txtHorasDedicadas.SetValue(parseFloat(s.cp_HorasDedicadas) * txtPercentualDedicacao.GetValue() / 100);
         
                }            
                else
                {
                        txtHorasDedicadas.SetValue(null);
                }
          }
}" />
        </dxcp:ASPxCallback>

    </form>
</body>
</html>
