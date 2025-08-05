<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaContratosEstendidos.aspx.cs" Inherits="_Projetos_Administracao_ListaContratosEstendidos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato"
            AutoGenerateColumns="False" Width="100%"
            ID="gvDados" EnableViewState="False" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCustomCallback="gvDados_CustomCallback"
            EnableRowsCache="False" EnableTheming="True">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg!= null &amp;&amp; s.cp_Msg != &quot;&quot;)
		mostraDivSalvoPublicado(s.cp_Msg);
	
	txtComentarioEncerramento.SetText(&quot;&quot;);
	pcEncerramento.Hide();
}"></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewDataTextColumn ToolTip="Nº Parcela" VisibleIndex="0" Width="130px"
                    ExportWidth="1" Caption="nº parcela">
                    <DataItemTemplate>
                        <%# getBotoes() %>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout" Name="btnRestaurarLayout">
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
                                            <td>
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/botoes/btnPDF.png"
                                                    ToolTip="Imprime relatório" Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
         e.processOnServer = false;
	     onClick_btnSalvar_MSR();
}" />
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Width="35px" Caption=" " VisibleIndex="1">
                    <DataItemTemplate>
                        <%# Eval("QuantidadeAditivos") + "" != "" && int.Parse(Eval("QuantidadeAditivos").ToString()) > 0 ? "<img src='../../imagens/botoes/aditado.png' alt='Este contrato possui " + Eval("QuantidadeAditivos").ToString() + " aditivo(s).' />" : "&nbsp;"%>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nº Contrato SAP" VisibleIndex="2" FieldName="NumeroContratoSAP"
                    Name="NumeroContratoSAP" Width="130px">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NumeroContrato" Width="130px" Caption="Nº Contrato"
                    VisibleIndex="3" Name="NumeroContrato">
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle>
                    </FilterCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="GestorContrato" Width="250px" VisibleIndex="7"
                    Caption="Responsável">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nome Município" FieldName="NomeMunicipio"
                    VisibleIndex="8" Width="250px">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Razão Social" FieldName="NomePessoa" VisibleIndex="9"
                    Width="210px">
                    <Settings AutoFilterCondition="Contains" />
                    <FilterCellStyle>
                    </FilterCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Data Assinatura" FieldName="DataAssinatura"
                    Name="DataAssinatura" VisibleIndex="10" Width="180px">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                            <ButtonStyle>
                            </ButtonStyle>
                            <HeaderStyle />
                            <FooterStyle />
                            <FastNavStyle>
                            </FastNavStyle>
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
                            <ReadOnlyStyle>
                            </ReadOnlyStyle>
                            <FocusedStyle>
                            </FocusedStyle>
                            <InvalidStyle>
                            </InvalidStyle>
                            <Style>
                                            
                                        </Style>
                        </CalendarProperties>
                    </PropertiesDateEdit>
                    <Settings AutoFilterCondition="GreaterOrEqual" ShowFilterRowMenu="True" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="DataInicio" Name="DataInicio" Width="180px"
                    Caption="Data OS Externa" VisibleIndex="11">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                        EditFormat="Custom">
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
                            <ButtonStyle>
                            </ButtonStyle>
                            <HeaderStyle />
                            <FooterStyle />
                            <FastNavStyle>
                            </FastNavStyle>
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
                            <ReadOnlyStyle>
                            </ReadOnlyStyle>
                            <FocusedStyle>
                            </FocusedStyle>
                            <InvalidStyle>
                            </InvalidStyle>
                            <Style>
                                            
                                        </Style>
                        </CalendarProperties>
                    </PropertiesDateEdit>
                    <Settings ShowFilterRowMenu="True" AutoFilterCondition="GreaterOrEqual"></Settings>
                    <FilterCellStyle>
                    </FilterCellStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="DataTermino" Name="DataTermino" Width="180px"
                    Caption="Data T&#233;rmino" VisibleIndex="12">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                        EditFormat="Custom">
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
                            <ButtonStyle>
                            </ButtonStyle>
                            <HeaderStyle />
                            <FooterStyle />
                            <FastNavStyle>
                            </FastNavStyle>
                            <FastNavMonthAreaStyle>
                            </FastNavMonthAreaStyle>
                            <FastNavYearAreaStyle>
                            </FastNavYearAreaStyle>
                            <FastNavMonthStyle>
                            </FastNavMonthStyle>
                            <FastNavYearStyle>
                                <SelectedStyle>
                                </SelectedStyle>
                                <HoverStyle>
                                </HoverStyle>
                            </FastNavYearStyle>
                            <FastNavFooterStyle>
                            </FastNavFooterStyle>
                            <ReadOnlyStyle>
                            </ReadOnlyStyle>
                            <FocusedStyle>
                            </FocusedStyle>
                            <InvalidStyle>
                            </InvalidStyle>
                            <Style>
                                            
                                        </Style>
                        </CalendarProperties>
                    </PropertiesDateEdit>
                    <Settings ShowFilterRowMenu="True" AutoFilterCondition="LessOrEqual"></Settings>
                    <FilterCellStyle>
                    </FilterCellStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SituacaoContrato" Width="75px" Caption="Situa&#231;&#227;o"
                    VisibleIndex="13">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="DescricaoObjetoContrato" Name="DescricaoObjetoContrato"
                    Width="595px" Caption="Objeto" VisibleIndex="14">
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle>
                    </FilterCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ValorContratoOriginal" Width="180px" Caption="Valor Contrato"
                    VisibleIndex="26">
                    <PropertiesTextEdit DisplayFormatString="{0:c2}">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False"></Settings>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ValorContrato" Width="180px" Caption="Valor com Aditivo"
                    VisibleIndex="27">
                    <PropertiesTextEdit DisplayFormatString="{0:c2}">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False"></Settings>
                    <DataItemTemplate>
                        <%# getValorAditadoGrid(Eval("ValorContrato").ToString(), Eval("ValorContratoOriginal").ToString())%>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Name="SaldoContratual" Width="180px" Caption="Saldo Contratual"
                    VisibleIndex="29">
                    <PropertiesTextEdit DisplayFormatString="{0:c2}">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False"></Settings>
                    <DataItemTemplate>
                        <%# getSaldoContratual()%>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="AnoTermino" Width="110px" Caption="Ano T&#233;rmino"
                    VisibleIndex="38">
                    <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle HorizontalAlign="Center">
                    </FilterCellStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PodeExcluirContrato" Visible="False" VisibleIndex="44">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Visible="False" VisibleIndex="43">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ContratoPossuiVinculo" Visible="False" VisibleIndex="41">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CNPJ/CPF" FieldName="CNPJ_CPF" VisibleIndex="40"
                    Width="150px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="IndicaPessoaFisicaJuridica" Visible="False"
                    VisibleIndex="42">
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Unidade de Negócio" FieldName="UnidadeNegocioContrato" VisibleIndex="4" Width="200px">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Projeto Vinculado" FieldName="PossuiProjeto" VisibleIndex="5" Width="135px">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Código do Projeto" FieldName="CodigoProjeto" VisibleIndex="6" Width="130px">
                </dxtv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
            <SettingsPager PageSize="80">
            </SettingsPager>
            <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True"
                ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupedColumns="True" HorizontalScrollBarMode="Visible"></Settings>
            <SettingsText></SettingsText>
            <Styles>
                <CommandColumnItem>
                    <Paddings PaddingLeft="2px" PaddingRight="2px"></Paddings>
                </CommandColumnItem>
                <Header>
                </Header>
                <FocusedGroupRow>
                </FocusedGroupRow>
                <FilterRow>
                </FilterRow>
                <Cell>
                </Cell>
                <HeaderPanel>
                </HeaderPanel>
                <FilterCell>
                </FilterCell>
                <FilterBar>
                </FilterBar>
                <FilterBarExpressionCell>
                </FilterBarExpressionCell>
                <HeaderFilterItem>
                </HeaderFilterItem>
                <FilterRowMenu>
                </FilterRowMenu>
                <FilterRowMenuItem>
                </FilterRowMenuItem>
            </Styles>
            <StylesFilterControl>
                <Value>
                </Value>
            </StylesFilterControl>
            <StylesPopup>
                <FilterBuilder>
                    <Header></Header>
                    <MainArea></MainArea>
                </FilterBuilder>
            </StylesPopup>
            <Templates>
                <FooterRow>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 20px">
                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/botoes/aditado.png">
                                    </dxe:ASPxImage>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server"
                                            Text="Contratos aditados">
                                        </dxe:ASPxLabel>
                                    </td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </FooterRow>
            </Templates>
            <Paddings PaddingTop="10px" PaddingLeft="10px" PaddingRight="10px" />
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False"
            ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
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
        <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            Modal="True" CloseAction="None" ClientInstanceName="pcEncerramento" HeaderText="Encerramento do Contrato"
            ShowCloseButton="False" Width="550px" ID="pcEncerramento">
            <ClientSideEvents Shown="function(s, e) {
	txtComentarioEncerramento.SetFocus();
}"></ClientSideEvents>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Coment&#225;rios:" ClientInstanceName="lblComentarios"
                                    ID="lblComentarios">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ClientInstanceName="txtComentarioEncerramento"
                                    ID="txtComentarioEncerramento">
                                    <ClientSideEvents Init="function(s, e) {
	onInit_mmEncerramento(s, e);
}"></ClientSideEvents>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" style="padding-right: 10px; padding-top: 5px">
                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnEncerrar" Text="Encerrar" Width="95px"
                                                    ID="btnEncerrar">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	gvDados.PerformCallback(&#39;C&#39;);
}"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 100px; padding-top: 5px">
                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelarEncerramento" Text="Cancelar"
                                                    Width="95px" ID="btnCancelarEncerramento">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    txtComentarioEncerramento.SetText(&quot;&quot;);
	pcEncerramento.Hide();
}"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
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
        <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes"
            ShowCloseButton="False" ID="pcDados" Width="1000px"
            Modal="True">
            <ClientSideEvents CloseUp="function(s, e) {
	
	
}"
                Shown="function(s, e) {
	if(TipoEdicao == 'I')
		abreNovo();
}"></ClientSideEvents>
            <ContentStyle>
                <Paddings Padding="2px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <iframe id="frmDetalhes" frameborder="0" height="<%=alturaFrames %>px" scrolling="no"
                        src="" width="100%"></iframe>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
            <Styles>
                <Header Font-Bold="True">
                </Header>
                <Cell>
                </Cell>
                <Footer Font-Bold="True">
                </Footer>
                <GroupFooter Font-Bold="True">
                </GroupFooter>
                <GroupRow Font-Bold="True">
                </GroupRow>
                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </div>
</asp:Content>
