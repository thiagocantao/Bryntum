<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="planoInvestimento.aspx.cs" Inherits="administracao_planoInvestimento" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Administração De Planos De Investimentos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td></td>
            <td style="height: 10px"></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPlanoInvestimento"
                                AutoGenerateColumns="False" Width="100%"
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
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
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                                            Name="btnRestaurarLayout">
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
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoPlanoInvestimento" Name="PlanoInvestimento"
                                        Caption="CodigoPlanoInvestimento" VisibleIndex="7" Visible="False">
                                        <Settings AutoFilterCondition="Contains" AllowAutoFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Plano" FieldName="DescricaoPlanoInvestimento"
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ano" FieldName="Ano" ShowInCustomizationForm="True"
                                        VisibleIndex="2" Width="90px" ExportWidth="60">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="DataInicioInclusaoProjeto"
                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="120px"
                                        ExportWidth="200">
                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                        </PropertiesDateEdit>
                                        <Settings ShowFilterRowMenu="True" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="DataFinalInclusaoProjetos"
                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="120px"
                                        ExportWidth="200">
                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                        </PropertiesDateEdit>
                                        <Settings ShowFilterRowMenu="True" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="CodigoStatusPlanoInvestimento" FieldName="CodigoStatusPlanoInvestimento"
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                        <Settings AllowAutoFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusPlanoInvestimento"
                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="220px"
                                        ExportWidth="300">
                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>
                                <SettingsText></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="620px" ID="pcDados">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                        Text="Plano de Investimento:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtDescricaoPlanoInvestimento" runat="server" ClientInstanceName="txtDescricaoPlanoInvestimento"
                                                        MaxLength="100" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 70px">
                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                    Text="Ano:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 105px">
                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                    Text="Início:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 105px">
                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                    Text="Final:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                    Text="Status:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 70px; padding-right: 3px">
                                                                <dxe:ASPxSpinEdit ID="txtAno" runat="server" ClientInstanceName="txtAno"
                                                                    Height="21px" MaxLength="4" Number="0" NumberType="Integer" Width="100%">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                            <td style="width: 105px; padding-right: 3px">
                                                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" ClientInstanceName="dteInicio"
                                                                    Width="100%" DisplayFormatString="dd/MM/yyyy"
                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                                    <CalendarProperties>
                                                                        <DayHeaderStyle />
                                                                        <WeekNumberStyle>
                                                                        </WeekNumberStyle>
                                                                        <DayStyle />
                                                                        <DaySelectedStyle>
                                                                        </DaySelectedStyle>
                                                                        <DayOtherMonthStyle>
                                                                        </DayOtherMonthStyle>
                                                                        <DayWeekendStyle>
                                                                        </DayWeekendStyle>
                                                                        <DayOutOfRangeStyle>
                                                                        </DayOutOfRangeStyle>
                                                                        <TodayStyle>
                                                                        </TodayStyle>
                                                                        <HeaderStyle />
                                                                        <FooterStyle />
                                                                        <FastNavMonthAreaStyle>
                                                                        </FastNavMonthAreaStyle>
                                                                        <FastNavYearAreaStyle>
                                                                        </FastNavYearAreaStyle>
                                                                        <FastNavMonthStyle>
                                                                        </FastNavMonthStyle>
                                                                        <FastNavYearStyle>
                                                                        </FastNavYearStyle>
                                                                        <FastNavFooterStyle>
                                                                        </FastNavFooterStyle>
                                                                        <Style>
                                                                        </Style>
                                                                    </CalendarProperties>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td style="width: 105px; padding-right: 3px">
                                                                <dxe:ASPxDateEdit ID="dteFinal" runat="server" ClientInstanceName="dteFinal"
                                                                    Width="100%"
                                                                    DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                    EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                                    <CalendarProperties>
                                                                        <DayHeaderStyle />
                                                                        <WeekNumberStyle>
                                                                        </WeekNumberStyle>
                                                                        <DayStyle />
                                                                        <DaySelectedStyle>
                                                                        </DaySelectedStyle>
                                                                        <DayOtherMonthStyle>
                                                                        </DayOtherMonthStyle>
                                                                        <DayWeekendStyle>
                                                                        </DayWeekendStyle>
                                                                        <DayOutOfRangeStyle>
                                                                        </DayOutOfRangeStyle>
                                                                        <TodayStyle>
                                                                        </TodayStyle>
                                                                        <HeaderStyle />
                                                                        <FooterStyle />
                                                                        <FastNavMonthAreaStyle>
                                                                        </FastNavMonthAreaStyle>
                                                                        <FastNavYearAreaStyle>
                                                                        </FastNavYearAreaStyle>
                                                                        <FastNavMonthStyle>
                                                                        </FastNavMonthStyle>
                                                                        <FastNavYearStyle>
                                                                        </FastNavYearStyle>
                                                                        <FastNavFooterStyle>
                                                                        </FastNavFooterStyle>
                                                                        <Style>
                                                                        </Style>
                                                                    </CalendarProperties>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus"
                                                                    Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 15px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                        Text="Salvar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                        Text="Fechar" Width="90px" ID="btnFechar">
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
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                ExportEmptyDetailGrid="True" GridViewID="gvDados" LeftMargin="50"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4"
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
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center"></td>
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
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Plano de Investimento incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Plano de Investimento alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Plano de Investimento excluído com sucesso!&quot;);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
