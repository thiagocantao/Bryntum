<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="SENAR_Contratos.aspx.cs" Inherits="administracao_CadastroRamosAtividades" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeHLC, .dxeHC, .dxeHFC
{
    display: none;
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxgvFilterRow
{
	background-color: #E7E7E7;
}
.dxgvCommandColumn
{
	padding: 2px;
}
.dxgvFocusedRow
{
	background-color: #8D8D8D;
	color: White;
}
.dxgvFooter
{
	background-color: #D7D7D7;
	white-space: nowrap;
}
        .auto-style1 {
            width: 100%;
        }
        .auto-style12 {
            height: 5px;
        }
        .auto-style17 {
            width: 275px;
        }
        .auto-style18 {
            width: 158px;
        }
        .auto-style3 {
            width: 139px;
        }
        .auto-style21 {
            width: 130px;
        }
        .auto-style22 {
            width: 132px;
        }
        .auto-style23 {
            width: 141px;
        }
    </style>
</head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
            </td>
            <td style="height: 10px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoPessoaFornecedor" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAfterPerformCallback="gvDados_AfterPerformCallback1" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
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
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="NomeFornecedor" ShowInCustomizationForm="True" Caption="Fornecedor" VisibleIndex="1">
</dxcp:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoPessoa" 
        Caption="Tipo" VisibleIndex="2" Width="120px">
    <PropertiesTextEdit DisplayFormatString="{0:n0}">
    </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Documento" FieldName="NumeroDocPessoaFormatado" ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
        <PropertiesTextEdit DisplayFormatString="c2">
        </PropertiesTextEdit>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="NomeContato" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Contato">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Telefone" FieldName="TelefonePessoa" ShowInCustomizationForm="True" VisibleIndex="5" Width="120px">
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="NumeroProcesso" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="NumeroDocPessoa" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="TipoPessoa" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="OutrasInformacoes" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="Email" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
    </dxtv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True"></Settings>

<SettingsText ></SettingsText>
     <TotalSummary>
         <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="Valor" ShowInColumn="Valor" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
     </TotalSummary>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="820px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td style="height: 15px">
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Fornecedor:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style17">&nbsp;</td>
                        <td class="auto-style18">
                            <dxtv:ASPxLabel ID="lblTipoPessoa1" runat="server"  Text="CNPJ:" ClientInstanceName="lblTipoPessoa">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px">
                            <dxtv:ASPxTextBox ID="txtNomePessoa" runat="server" ClientInstanceName="txtNomePessoa"  MaxLength="150" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                        <td class="auto-style17" style="padding-right: 10px">
                            <dxtv:ASPxRadioButtonList ID="rbTipoPessoa" runat="server" ClientInstanceName="rbTipoPessoa"  ItemSpacing="10px" RepeatDirection="Horizontal" SelectedIndex="1" Width="100%">
                                <Paddings Padding="0px" />
                                <ClientSideEvents Init="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" SelectedIndexChanged="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" />
                                <Items>
                                    <dxtv:ListEditItem Text="Pessoa Física" Value="F" />
                                    <dxtv:ListEditItem Selected="True" Text="Pessoa Jurídica" Value="J" />
                                </Items>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxRadioButtonList>
                        </td>
                        <td class="auto-style18">
                            <dxtv:ASPxTextBox ID="txtCPF" runat="server" ClientInstanceName="txtCPF" ClientVisible="False"  Width="100%">
                                <MaskSettings IncludeLiterals="None" Mask="999,999,999-99" PromptChar=" " />
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                            <dxtv:ASPxTextBox ID="txtCNPJ" runat="server" ClientInstanceName="txtCNPJ"  Width="100%">
                                <MaskSettings IncludeLiterals="None" Mask="99,999,999/9999-99" PromptChar=" " />
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 210px">
                            <dxtv:ASPxLabel ID="ASPxLabel9" runat="server"  Text="Nome do Contato:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="linhaEdicaoPeriodo" style="width: 125px">
                            <dxtv:ASPxLabel ID="ASPxLabel10" runat="server"  Text="Telefone:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel8" runat="server"  Text="Email:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 210px">
                            <dxtv:ASPxTextBox ID="txtNomeContato" runat="server" ClientInstanceName="txtNomeContato"  MaxLength="150" Width="200px">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                        <td class="linhaEdicaoPeriodo" style="width: 125px">
                            <dxtv:ASPxTextBox ID="txtTelefone" runat="server" ClientInstanceName="txtTelefone"  Width="115px">
                                <MaskSettings Mask="(99) 99999-9999" PromptChar=" " />
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                        <td>
                            <dxtv:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail"  MaxLength="150" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxLabel ID="ASPxLabel11" runat="server"  Text="Número do Processo:">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxTextBox ID="txtNumeroProcesso" runat="server" ClientInstanceName="txtNumeroProcesso"  MaxLength="30" Width="150px">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxtv:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxLabel ID="ASPxLabel7" runat="server"  Text="Outras Informações:">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxMemo ID="txtInformacoesContato" runat="server" ClientInstanceName="txtInformacoesContato"  Rows="3" Width="100%">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxtv:ASPxMemo>
                <dxtv:ASPxLabel ID="lblContadorInformacoesContato" runat="server" ClientInstanceName="lblContadorInformacoesContato" Font-Bold="True"  ForeColor="#999999">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxGridView ID="gvContratos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvContratos"  KeyFieldName="CodigoContrato" OnBatchUpdate="gvContratos_BatchUpdate" Width="100%" OnCommandButtonInitialize="gvContratos_CommandButtonInitialize">
                    <ClientSideEvents BatchEditStartEditing="onStartEdit" EndCallback="function(s, e) {
	if(s.cp_msg !=  null &amp;&amp; s.cp_msg != '')
	{
		if(s.cp_status == 'ok')
        		{	
                    if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro incluído com sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro alterado com sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro excluído com sucesso!&quot;, 'sucesso', false, false, null);
			gvDados.PerformCallback();
			
			if (window.onClick_btnCancelar)
       				onClick_btnCancelar();

}
        		else
            			window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
	}
}
" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="Batch">
                        <BatchEditSettings ShowConfirmOnLosingChanges="False" StartEditAction="DblClick" />
                    </SettingsEditing>
                    <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="135" VerticalScrollBarMode="Visible" />
                    <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                    <SettingsCommandButton>
                        <UpdateButton>
                            <Image Url="~/imagens/botoes/salvar.png">
                            </Image>
                        </UpdateButton>
                        <CancelButton>
                            <Image Url="~/imagens/botoes/cancelar.png">
                            </Image>
                        </CancelButton>
                        <EditButton>
                            <Image Url="~/imagens/botoes/editarReg02.png">
                            </Image>
                        </EditButton>
                        <DeleteButton>
                            <Image Url="~/imagens/botoes/excluirReg02.png">
                            </Image>
                        </DeleteButton>
                    </SettingsCommandButton>
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="780px" />
                        <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                    </SettingsPopup>
                    <SettingsText EmptyDataRow="Nenhum perfil cadastrado" PopupEditFormCaption="Contratos" Title="Contratos" />
                    <Columns>
                        <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" Ação" ShowCancelButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dxtv:GridViewCommandColumn>
                        <dxtv:GridViewDataTextColumn Caption="Número do Contrato" FieldName="NumeroContrato" ShowInCustomizationForm="True" VisibleIndex="1">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataDateColumn Caption="Início de Vigência" FieldName="DataInicioVigencia" ShowInCustomizationForm="True" VisibleIndex="2">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxtv:GridViewDataDateColumn>
                        <dxtv:GridViewDataDateColumn Caption="Término de Vigência" FieldName="DataTerminoVigencia" ShowInCustomizationForm="True" VisibleIndex="3">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxtv:GridViewDataDateColumn>
                        <dxtv:GridViewDataSpinEditColumn Caption="Valor Global" FieldName="ValorGlobalContrato" ShowInCustomizationForm="True" VisibleIndex="4">
                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxtv:GridViewDataSpinEditColumn>
                    </Columns>
                </dxtv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style12"></td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
  if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></tbody></table>
            </td>
        </tr>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
    <dxtv:ASPxPopupControl ID="pcDadosContrato" runat="server" ClientInstanceName="pcDadosContrato" CloseAction="None"  HeaderText="Detalhes" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="620px">
        <ClientSideEvents Shown="onShown" />
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <HeaderStyle Font-Bold="True" />
        <ContentCollection>
            <dxtv:PopupControlContentControl runat="server">
                <table>
                    <tr>
                        <td>
                            <table cellspacing="0" class="auto-style1">
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel31" runat="server"  Text="Número do Contrato:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td class="auto-style22">
                                        <dxtv:ASPxLabel ID="ASPxLabel30" runat="server"  Text="Início de Vigência:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td class="auto-style21">
                                        <dxtv:ASPxLabel ID="ASPxLabel32" runat="server"  Text="Término de Vigência:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td class="auto-style23">
                                        <dxtv:ASPxLabel ID="ASPxLabel33" runat="server"  Text="Valor Global:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxtv:ASPxTextBox ID="txtNumeroContrato" runat="server" ClientInstanceName="txtNumeroContrato"  MaxLength="30" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                    <td class="auto-style22" style="padding-right: 10px">
                                        <dxtv:ASPxDateEdit ID="deInicio" runat="server" ClientInstanceName="deInicio"  Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxDateEdit>
                                    </td>
                                    <td class="auto-style21" style="padding-right: 10px">
                                        <dxtv:ASPxDateEdit ID="deTermino" runat="server" ClientInstanceName="deTermino"  Width="100%">
                                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetValue() &lt; deInicio.GetValue())
	{
		e.isValid = false;
		e.errorText = 'A data de término na pode ser menor que a data de início da vigência!';
	}
}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxDateEdit>
                                    </td>
                                    <td class="auto-style23">
                                        <dxtv:ASPxSpinEdit ID="txtValorGlobal" runat="server" ClientInstanceName="txtValorGlobal" DecimalPlaces="2"  Width="100%">
                                            <SpinButtons ClientVisible="False">
                                            </SpinButtons>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxSpinEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxButton ID="btnSalvar0" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar2"  Text="Salvar" ValidationGroup="MKE2" Width="100px">
                                                <clientsideevents click="function(s, e) {
	e.processOnServer = false;
if(ASPxClientEdit.ValidateGroup('MKE2', true))
    		onAcceptClick();
}" />
                                                <paddings padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td style="WIDTH: 10px"></td>
                                        <td>
                                            <dxtv:ASPxButton ID="btnFechar0" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"  Text="Fechar" Width="90px">
                                                <clientsideevents click="function(s, e) {
	e.processOnServer = false;
   onCloseButtonClick();
}" />
                                                <paddings padding="0px" paddingbottom="0px" paddingleft="0px" paddingright="0px" paddingtop="0px" />
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
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro incluído com sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro alterado com sucesso!&quot;, 'sucesso', false, false, null);
    	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro excluído com sucesso!&quot;, 'sucesso', false, false, null);

}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
</div>
    </form>
</body>
</html>
