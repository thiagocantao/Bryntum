<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_TimeSheet.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_TimeSheet"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Atualização de Tarefas">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td id="ConteudoPrincipal">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td valign="bottom" style="padding-right: 10px; width: 20%;">
                                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel3a" runat="server"
                                            HeaderText="Seleção de Projetos" View="GroupBox">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="display: none;">
                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                        Text="Tipo de Atualização:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="display: none;">
                                                                    <dxe:ASPxComboBox ID="ddlTipoAtualizacao" runat="server"
                                                                        SelectedIndex="0" ValueType="System.String">
                                                                        <Items>
                                                                            <dxe:ListEditItem Selected="True" Text="Todos" Value="TD" />
                                                                            <dxe:ListEditItem Text="Timesheet" Value="TS" />
                                                                            <dxe:ListEditItem Text="Percentual Conclu&#237;do" Value="PC" />
                                                                        </Items>
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                                                                        <ItemStyle />
                                                                        <ListBoxStyle>
                                                                        </ListBoxStyle>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                        Text="Projeto:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-top: 3px;">
                                                                    <dxe:ASPxComboBox ID="ddlProjeto" runat="server"
                                                                        ValueType="System.String" Width="300px" IncrementalFilteringMode="Contains">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}"></ClientSideEvents>
                                                                        <ItemStyle />
                                                                        <ListBoxStyle>
                                                                        </ListBoxStyle>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                            <Border BorderWidth="1px" BorderColor="#8B8B8B" BorderStyle="Solid" />
                                            <HeaderStyle BackColor="White" />
                                        </dxrp:ASPxRoundPanel>
                                    </td>
                                    <td valign="bottom" style="display: none; width: 20%;">
                                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server"
                                            HeaderText="Período de Apontamento" View="GroupBox" BackColor="White">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                    Text="De:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                    Text="At&#233;:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="txtDe" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                    EditFormatString="dd/MM/yyyy"
                                                                    ClientInstanceName="txtDe" AllowUserInput="False">
                                                                    <ClientSideEvents DateChanged="function(s, e) {
	
	if(gvDados.cp_Inicio != s.GetText().toString())
	{				
		var dataAtual = new Date();
		var dataDe = new Date(s.GetValue());
		var dataAte = new Date(txtFim.GetValue());
		txtFim.SetMinDate(s.GetValue());
		
		if(dataDe &gt; dataAte)
			txtFim.SetValue(dataDe);

		dataDe.setDate(dataDe.getDate() + 6);
	
	
		if(dataDe &gt; dataAtual)
			txtFim.SetMaxDate(dataAtual);
		else
			txtFim.SetMaxDate(dataDe);
	
	
		if(dataDe &lt; dataAte)
			txtFim.SetValue(dataDe);

		if(dataAte &gt; dataAtual)
			txtFim.SetValue(dataAtual);

		gvDados.cp_Inicio = s.GetValue();

		gvDados.PerformCallback();
	}
}" />
                                                                    <CalendarProperties HighlightToday="False" ShowClearButton="False" ShowTodayButton="False">
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
                                                                        <Style>
                                                                
                                                            </Style>
                                                                    </CalendarProperties>
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="txtFim" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                    EditFormatString="dd/MM/yyyy"
                                                                    ClientInstanceName="txtFim" AllowUserInput="False">
                                                                    <CalendarProperties HighlightToday="False" ShowClearButton="False" ShowTodayButton="False">
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
                                                                        <Style>
                                                                
                                                            </Style>
                                                                    </CalendarProperties>
                                                                    <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Termino != s.GetText().toString())
	{
		gvDados.cp_Termino = s.GetValue();
		gvDados.PerformCallback();
	}
}" />
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                            <Border BorderWidth="1px" BorderColor="#8B8B8B" BorderStyle="Solid" />
                                            <HeaderStyle BackColor="White" />
                                        </dxrp:ASPxRoundPanel>
                                    </td>
                                    <td valign="bottom" style="width: 50%;">
                                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server"
                                            HeaderText="Filtro de Tarefas" View="GroupBox">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                    Text="T&#233;rmino Previsto At&#233;:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="txtTerminoPrevistoAte" runat="server" DisplayFormatString="dd/MM/yyyy"
                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Previsto != s.GetText().toString())
	{
		gvDados.PerformCallback();
	}
}" />
                                                                    <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
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
                                                                        <FocusedStyle>
                                                                        </FocusedStyle>
                                                                        <Style>
                                                                
                                                            </Style>
                                                                    </CalendarProperties>
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxCheckBox ID="checkNaoConcluidas" runat="server" Checked="True"
                                                                    Text="Somente N&#227;o Conclu&#237;das" ValueChecked="S" ValueType="System.Char"
                                                                    ValueUnchecked="N">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxCheckBox ID="checkAtrasadas" runat="server" CheckState="Unchecked"
                                                                    Text="Somente Atrasadas" ValueChecked="S" ValueType="System.Char"
                                                                    ValueUnchecked="N">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                            <Border BorderWidth="1px" BorderColor="#8B8B8B" BorderStyle="Solid" />
                                            <HeaderStyle BackColor="White" />
                                        </dxrp:ASPxRoundPanel>
                                    </td>
                                    <td align="center" valign="middle" style="padding: 5px; width: 30px;">
                                        <dxe:ASPxButton ID="imgAtualiza" runat="server" BackColor="White" Cursor="pointer"
                                            ImagePosition="Bottom" ImageSpacing="0px"
                                            ToolTip="Atualizar" AutoPostBack="False" ClientInstanceName="imgAtualiza" Width="25px">
                                            <PressedStyle BackColor="White">
                                                <border borderstyle="None" />
                                            </PressedStyle>
                                            <Image Url="~/imagens/atualizar.PNG">
                                            </Image>
                                            <Paddings Padding="0px" PaddingBottom="0px" />
                                            <ClientSideEvents Click="function(s, e) {
	gvDados.PerformCallback();
}" />
                                            <HoverStyle BackColor="White" CssClass="a">
                                                <BackgroundImage Repeat="NoRepeat" />
                                                <border borderstyle="None" />
                                            </HoverStyle>
                                            <FocusRectPaddings Padding="0px" />
                                            <FocusRectBorder BorderStyle="None" />
                                            <Border BorderStyle="None" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td align="center" valign="middle" style="padding: 5px; width: 30px;">
                                        <dxe:ASPxButton ID="ASPxButton2" runat="server" BackColor="White" Cursor="pointer"
                                            ImagePosition="Bottom" ImageSpacing="0px" Width="22px"
                                            OnClick="btnExcel_Click" ToolTip="Exportar para o Excel">
                                            <PressedStyle BackColor="White" CssClass="a">
                                                <border borderstyle="None" />
                                            </PressedStyle>
                                            <Image Url="~/imagens/botoes/btnExcel.png">
                                            </Image>
                                            <Paddings Padding="0px" PaddingBottom="0px" />
                                            <FocusRectPaddings Padding="0px" />
                                            <FocusRectBorder BorderStyle="None" />
                                            <Border BorderStyle="None" />
                                            <HoverStyle BackColor="White" CssClass="corBotao">
                                                <border borderstyle="None" />
                                            </HoverStyle>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td align="center" valign="middle" style="padding: 5px; width: 30px;">
                                        <dxe:ASPxButton ID="btnAtualizarAgenda" runat="server" BackColor="White" Cursor="pointer"
                                            ImagePosition="Bottom" ImageSpacing="0px"
                                            OnClick="btnAtualizarAgenda_Click" ToolTip="Atualizar minha agenda">
                                            <PressedStyle BackColor="White" CssClass="a">
                                                <border borderstyle="None" />
                                            </PressedStyle>
                                            <Image IconID="scheduling_switchtimescalesto_16x16">
                                            </Image>
                                            <Paddings Padding="0px" PaddingBottom="0px" />
                                            <FocusRectPaddings Padding="0px" />
                                            <FocusRectBorder BorderStyle="None" />
                                            <Border BorderStyle="None" />
                                            <HoverStyle BackColor="White" CssClass="corBotao">
                                                <border borderstyle="None" />
                                            </HoverStyle>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px;">
                            <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                                Width="100%" AutoGenerateColumns="False" KeyFieldName="CodigoAtribuicao"
                                OnCommandButtonInitialize="gvDados_CommandButtonInitialize" OnCellEditorInitialize="gvDados_CellEditorInitialize"
                                OnRowUpdating="gvDados_RowUpdating" OnCustomCallback="gvDados_CustomCallback"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize" PreviewFieldName="CodigoAtribuicao"
                                OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage7" runat="server" ImageUrl="~/imagens/botoes/tarefasPPLenda.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_envio_pendente %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage8" runat="server" ImageUrl="~/imagens/botoes/tarefasPALenda.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_pendente_aprova__o %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage9" runat="server" ImageUrl="~/imagens/botoes/salvarLenda.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_aprovado %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage10" runat="server" ImageUrl="~/imagens/botoes/tarefaRecusadaLenda.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_reprovado %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage11" runat="server" ImageUrl="~/imagens/tarefaCritica.gif">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_cr_tica %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage12" runat="server" ImageUrl="~/imagens/possuiAnexo.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_possui_anexo %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-asterisco">
                                                        <span>*</span>
                                                    </td>
                                                    <td class="grid-legendas-label">
                                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server"
                                                            Font-Underline="True" Text="<%# Resources.traducao.frameEspacoTrabalho_TimeSheet_sublinhado__trabalho_real_maior_que_trabalho_previsto %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </FooterRow>
                                </Templates>
                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
                                <Styles>
                                    <FocusedRow BackColor="Transparent" ForeColor="Black">
                                    </FocusedRow>
                                    <Cell Wrap="False">
                                    </Cell>
                                </Styles>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <TotalSummary>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                    <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n0}"></dxwgv:ASPxSummaryItem>
                                </TotalSummary>
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                                <SettingsText></SettingsText>
                                <ClientSideEvents CustomButtonClick="function(s, e) 
{
	if(e.buttonID == &quot;btnDetalhes&quot;)
	 {
                             s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                             s.GetSelectedFieldValues('CodigoAtribuicao;TipoTarefa;', abreDatelhes);
     	}
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " FixedStyle="Left" VisibleIndex="0"
                                        ShowEditButton="false" Width="90px">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhes" Text="Editar Detalhes da Atribui&#231;&#227;o">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnStatus">
                                                <Image Url="~/imagens/vazioPequeno.GIF">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <FooterTemplate>
                                            <strong><asp:Literal runat="server" Text="<%$ Resources:traducao, frameEspacoTrabalho_TimeSheet_total %>" /></strong>
                                        </FooterTemplate>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" FieldName="Descricao"
                                        FixedStyle="Left" ReadOnly="True" VisibleIndex="1" Width="100%">
                                        <DataItemTemplate>
                                            <%# verificaDescricaoTarefa(Eval("Nivel").ToString(), Eval("Descricao").ToString(), Eval("IndicaAtrasada").ToString(), Eval("IndicaCritica").ToString(), Eval("TipoTarefa").ToString())%>
                                        </DataItemTemplate>
                                        <CellStyle Wrap="True">
                                            <Paddings PaddingRight="10px" />
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="In&#237;cio" FieldName="Inicio" ReadOnly="True"
                                        VisibleIndex="2" Width="90px">
                                        <PropertiesDateEdit AllowMouseWheel="False" AllowUserInput="False" DisplayFormatString="dd/MM/yyyy"
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            <DropDownButton Enabled="False" Visible="False">
                                            </DropDownButton>
                                        </PropertiesDateEdit>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="T&#233;rmino" FieldName="Termino" ReadOnly="True"
                                        VisibleIndex="3" Width="90px">
                                        <PropertiesDateEdit AllowMouseWheel="False" AllowUserInput="False" DisplayFormatInEditMode="True"
                                            DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            <DropDownButton Enabled="False" Visible="False">
                                            </DropDownButton>
                                        </PropertiesDateEdit>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Trab. Restante (h)" FieldName="TrabalhoRestante"
                                        Name="TrabalhoRestante" VisibleIndex="4" Width="135px">
                                        <PropertiesTextEdit>
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Trabalho Real (h)" FieldName="TrabalhoRealTotal"
                                        Name="TrabalhoRealTotal" ReadOnly="True" Visible="False" VisibleIndex="5" Width="120px">
                                        <PropertiesTextEdit>
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Conclu&#237;do" FieldName="PercConcluido"
                                        Name="PercConcluido" ReadOnly="True" VisibleIndex="6" Width="90px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}%">
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="In&#237;cio Real" FieldName="InicioReal" Name="InicioReal"
                                        ReadOnly="True" Visible="False" VisibleIndex="8" Width="90px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="T&#233;rmino Real" FieldName="TerminoReal"
                                        Name="TerminoReal" ReadOnly="True" Visible="False" VisibleIndex="9" Width="90px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="1" Name="dia1" VisibleIndex="7" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="2" Name="dia2" VisibleIndex="10" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="3" Name="dia3" VisibleIndex="11" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="4" Name="dia4" VisibleIndex="12" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="5" Name="dia5" VisibleIndex="13" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="6" Name="dia6" VisibleIndex="14" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="7" Name="dia7" VisibleIndex="15" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..24&gt;.&lt;0..9&gt;" />
                                            <ValidationSettings ErrorDisplayMode="None">
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Nivel" Name="Nivel" Visible="False" VisibleIndex="18" Width="90px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TipoAtualizacao" Name="TipoAtualizacao"
                                        Visible="False" VisibleIndex="19">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="StatusAprovacao" Name="StatusAprovacao"
                                        Visible="False" VisibleIndex="20">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaAtrasada" Visible="False" VisibleIndex="17">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaCritica" Visible="False" VisibleIndex="21">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TipoTarefa" Visible="False" VisibleIndex="22">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TrabalhoPrevisto" Visible="False" VisibleIndex="23">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IndicaAnexo" Visible="False" VisibleIndex="16">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"></Settings>
                                <StylesEditors>
                                    <ReadOnly BackColor="Transparent">
                                        <border borderstyle="None"></border>
                                    </ReadOnly>

                                    <ReadOnly BackColor="Transparent">
                                        <border borderstyle="None"></border>
                                    </ReadOnly>
                                    <TextBox BackColor="Transparent">
                                    </TextBox>
                                </StylesEditors>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table class="formulario-botoes">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                                Text="Publicar" Width="100px">
                                                <Paddings Padding="0px"></Paddings>
                                                <ClientSideEvents Click="function(s, e) {
window.top.mostraMensagem(traducao.frameEspacoTrabalho_TimeSheet_deseja_enviar_as_tarefas_para_aprova__o_,  'confirmacao',  true,  true, enviaTarefasParaAprovacao);
}"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_OK != '')
                {
                         window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                         gvDados.PerformCallback();
                }
                 else if(s.cp_Erro != '')
                {
                          window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                }
}" />
    </dxcb:ASPxCallback>
    <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" Landscape="True"
        LeftMargin="20" MaxColumnWidth="400" OnRenderBrick="gvExporter_RenderBrick" PaperKind="A4"
        RightMargin="20">
        <Styles>
            <Header>
            </Header>
            <Cell>
            </Cell>
            <GroupRow>
            </GroupRow>
            <AlternatingRowCell>
            </AlternatingRowCell>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
