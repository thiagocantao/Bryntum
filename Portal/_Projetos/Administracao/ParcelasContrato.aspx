<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ParcelasContrato.aspx.cs" Inherits="_Projetos_Administracao_ParcelasContrato"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server"
                                Text="Atualização - Parcelas"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td id="ConteudoPrincipal">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    OnCallback="pnCallback_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gveExportar"
                                OnRenderBrick="gveExportar_RenderBrick">
                            </dxwgv:ASPxGridViewExporter>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato;NumeroAditivoContrato"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"
                                    CustomButtonClick="function(s, e) {
	if((e.buttonID == 'btnEditarCustom'))
    {	
          if(s.cp_utilizaConvenio == &quot;N&quot; || s.cp_utilizaConvenio == undefined)
          {
               try
               {
	                 hfGeral.Set('TipoOperacao', 'Editar');
	                 onClickBarraNavegacao('Editar', gvDados, pcDados);
               }
               catch(e){}
           }
           else
           {
               s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro', mostraPopup);
           }
	}	
}"></ClientSideEvents>
                                <TotalSummary>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" FieldName="ValorPrevisto" DisplayFormat="{0:n2}"
                                        ShowInColumn="ValorPrevisto" ShowInGroupFooterColumn="ValorPrevisto" Tag="Tot.:"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" FieldName="ValorPago" DisplayFormat="{0:n2}"
                                        ShowInColumn="ValorPago" ShowInGroupFooterColumn="ValorPago" Tag="Tat.:"></dxwgv:ASPxSummaryItem>
                                </TotalSummary>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" CellStyle-HorizontalAlign="Center" FixedStyle="Left" Width="50px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <HeaderTemplate>
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imgExport" runat="server" ImageUrl="~/imagens/excel.PNG" ToolTip="Exportar para Excel"
                                                                Width="22px" OnClick="imgExport_Click"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Name="CodigoContrato" Caption="CodigoContrato"
                                        Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NumeroContrato" FixedStyle="Left" Name="NumeroContrato"
                                        Caption="N&#186; Contrato" VisibleIndex="2" Width="120px">
                                        <FooterTemplate>
                                            <dxe:ASPxLabel ID="lblTotales" runat="server" ClientInstanceName="lblTotales" Text="TOTAL:">
                                            </dxe:ASPxLabel>
                                        </FooterTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NumeroAditivoContrato" FixedStyle="Left"
                                        Name="NumeroAditivoContrato" Width="80px" Caption="N&#186; Aditivo" VisibleIndex="3">
                                        <PropertiesTextEdit MaxLength="3" ClientInstanceName="txtNumeroAditivo">
                                            <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings VisibleIndex="1" CaptionLocation="Top" Caption="N&#186; Aditivo:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Fornecedor" FixedStyle="Left" Name="Fornecedor"
                                        Caption="Fornecedor" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Objeto" Name="Objeto" Caption="Objeto"
                                        VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Width="160px"
                                        Caption="Responsavel" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NumeroParcela" Name="NumeroParcela" Width="120px"
                                        Caption="N&#186; Parcela" VisibleIndex="7">
                                        <PropertiesTextEdit MaxLength="3" ClientInstanceName="txtNumeroParcela">
                                            <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings VisibleIndex="2" Caption="N&#186; Parcela:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="DataVencimento" Name="DataVencimento" Caption="Data de Vencimento"
                                        VisibleIndex="8" Width="145px">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio!"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesDateEdit>
                                        <EditFormSettings VisibleIndex="4" CaptionLocation="Top" Caption="Data de Vencimento:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ValorPrevisto" Name="ValorPrevisto" Caption="Valor  Previsto"
                                        VisibleIndex="9" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings VisibleIndex="3" CaptionLocation="Top" Caption="Valor  Previsto:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="DataPagamento" Name="DataPagamento" Caption="Data de Pagamento"
                                        VisibleIndex="10" Width="125px">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlDataPagamento">
                                            <ValidationSettings ValidationGroup="VGG">
                                            </ValidationSettings>
                                        </PropertiesDateEdit>
                                        <Settings AutoFilterCondition="LessOrEqual"></Settings>
                                        <EditFormSettings VisibleIndex="6" CaptionLocation="Top" Caption="Data de Pagamento:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ValorPago" Name="ValorPago" Caption="Valor Pago"
                                        VisibleIndex="11" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}" ClientInstanceName="txtValorPagoGvParcela">
                                            <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>
                                            <ValidationSettings ValidationGroup="VGG">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings VisibleIndex="5" CaptionLocation="Top" Caption="Valor Pago:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SituacaoParcela" Name="SituacaoParcela"
                                        Caption="Situa&#231;&#227;o" VisibleIndex="12" Width="90px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn FieldName="HistoricoParcela" Name="HistoricoParcela"
                                        Caption="Hist&#243;rico" Visible="False" VisibleIndex="13">
                                        <PropertiesMemoEdit Rows="2">
                                        </PropertiesMemoEdit>
                                        <EditFormSettings ColumnSpan="2" VisibleIndex="7" CaptionLocation="Top" Caption="Hist&#243;rico:"></EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Name="CodigoContrato" Caption="CondigoContrato"
                                        Visible="False" VisibleIndex="14">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption="CodigoLancamentoFinanceiro" FieldName="CodigoLancamentoFinanceiro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                    </dxtv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm">
                                </SettingsEditing>
                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        AllowResize="True" Width="350px" VerticalOffset="-40" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowHeaderFilterBlankItems="False"
                                    ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                    VerticalScrollableHeight="227" ShowGroupedColumns="True" ShowStatusBar="Visible"></Settings>
                                <SettingsText Title="Parcelas associadas" ConfirmDelete="Retirar a Parcela para este contrato?"
                                    PopupEditFormCaption="Parcela do Contrato"></SettingsText>
                                <StylesPopup>
                                    <EditForm>
                                        <Header Font-Bold="True">
                                        </Header>
                                        <MainArea Font-Bold="False"></MainArea>
                                    </EditForm>
                                </StylesPopup>
                                <Styles>
                                    <Header Font-Bold="False">
                                    </Header>
                                    <HeaderPanel Font-Bold="False">
                                    </HeaderPanel>
                                    <TitlePanel Font-Bold="True">
                                    </TitlePanel>
                                </Styles>
                                <Templates>
                                    <StatusBar>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-cor grid-legendas-cor-atrasado">
                                                        <span></span>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-atrasado">
                                                        <asp:Label runat="server" Text="<%# Resources.traducao.ParcelasContrato_atrasadas %>"
                                                            ID="Label1" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </StatusBar>
                                </Templates>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Atualiza&#231;&#227;o das Parcelas" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table class="formulario" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <table class="formulario-colunas" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="N&#186; Contrato:"
                                                                            ID="lblNumContrato" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox runat="server" Width="200px" ClientInstanceName="txtNumeroContrato"
                                                                            ClientEnabled="False" ID="txtNumeroContrato">
                                                                            <MaskSettings PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#FFE0C0" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <table class="formulario-colunas" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Fornecedor:"
                                                                            ID="lblFornecedor" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtNomeFornecedor"
                                                                            ClientEnabled="False" ID="txtNomeFornecedor">
                                                                            <MaskSettings PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#FFE0C0" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <table class="formulario-colunas" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Objeto:" ID="lblObjeto"
                                                                            EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Height="100px" Width="100%" ClientInstanceName="mmObjeto"
                                                                            ClientEnabled="False" ID="mmObjeto">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	var texto = s.GetText();
	if(texto.length &gt; 500)
	{
		s.SetText(texto.substring(0,500));
	}
}"
                                                                                Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 500);
}"></ClientSideEvents>
                                                                            <DisabledStyle BackColor="#FFE0C0" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <table class="formulario-colunas" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="N&#186; Aditivo:"
                                                                            ID="lblNumAditivo" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="N&#186; Parcela:"
                                                                            ID="lblNumParcela" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Valor Previsto:"
                                                                            ID="lblValorPrevisto" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Data Vencimento:"
                                                                            ID="lblDataVencimento" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox runat="server" Width="80px" ClientInstanceName="txtAditivo" ClientEnabled="False"
                                                                            ID="txtAditivo">
                                                                            <MaskSettings Mask="&lt;0..999&gt;" PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox runat="server" Width="80px" ClientInstanceName="txtParcela" ClientEnabled="False"
                                                                            ID="txtParcela">
                                                                            <MaskSettings Mask="&lt;0..999&gt;" PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox runat="server" Width="170px" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorPrevisto"
                                                                            ClientEnabled="False" ID="txtValorPrevisto">
                                                                            <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit runat="server" Width="120px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="deDataVencimento"
                                                                            ClientEnabled="False" ID="deDataVencimento">
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <table class="formulario-colunas" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Valor Pago:"
                                                                            ID="lblValorPago" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                    <td class="formulario-label">
                                                                        <asp:Label runat="server" Text="Data de Pagamento:"
                                                                            ID="lblDataPagamento" EnableViewState="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox runat="server" Width="165px" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorPago"
                                                                            ID="txtValorPago">
                                                                            <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" PromptChar=" "></MaskSettings>
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit runat="server" Width="120px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="deDataPagamento"
                                                                            ID="deDataPagamento">
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td class="formulario-label">
                                                        <asp:Label runat="server" Text="Hist&#243;rico:"
                                                            ID="lblHistorico" EnableViewState="False"></asp:Label>
                                                        <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater" ForeColor="Silver" ID="lblCantCarater">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500" ForeColor="Silver" ID="lblDe500">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td>
                                                        <dxe:ASPxMemo runat="server" Height="75px" Width="100%" ClientInstanceName="mmHistorico"
                                                            ID="mmHistorico">
                                                            <ClientSideEvents KeyPress="function(s, e) {
	var texto = s.GetText();
	if(texto.length &gt; 500)
	{
		s.SetText(texto.substring(0,500));
	}
}"
                                                                Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 500);
}"></ClientSideEvents>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr class="formulario-linha">
                                                    <td align="right">
                                                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="100px" ID="btnSalvar">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;

	var valor = txtValorPago.GetText();
	var dataPagamento = deDataPagamento.GetText();
	var isOk = true;

	if(valor == null) isOk = false;
	else if(valor == &quot;&quot;) isOk = false;
	else if(valor == &quot;0,00&quot;) isOk = false;
	if(dataPagamento == null ) isOk = false;
	else if(dataPagamento == &quot;&quot; ) isOk = false;

	if(isOk)
	{
    	if (window.onClick_btnSalvar)
	    	onClick_btnSalvar();
	}
	else
	{
		window.top.mostraMensagem('Valor e Data de Pagamento tem que ser informado!', 'atencao', true, false, null);
	}
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="100px" ID="btnFechar">
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
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDadoIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px" ID="pcDadoIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="center"></td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao">
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
                    <ClientSideEvents EndCallback="function(s, e) {
    if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
