<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroCalendarioMedicao.aspx.cs" Inherits="administracao_CadastroCalendarioMedicao" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Calendários de Medições">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCalendarioMedicao"
                                AutoGenerateColumns="False" Width="100%" 
                                ID="gvDados" DataSourceID="dsDados" OnRowDeleting="gvDados_RowDeleting" OnInitNewRow="gvDados_InitNewRow"
                                OnRowUpdating="gvDados_RowUpdating">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="75px" VisibleIndex="0" Caption=" "
                                        ShowEditButton="true" ShowDeleteButton="true">
                                        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvDados.AddNewRow();"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoCalendarioMedicao" VisibleIndex="1"
                                        Visible="False">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ano da Medição" FieldName="Ano" ShowInCustomizationForm="True"
                                        VisibleIndex="0" Name="txtAno">
                                        <PropertiesTextEdit>
                                            <MaskSettings ErrorText="Informe um valor válido" Mask="&lt;2000..2020&gt;" />
                                            <ClientSideEvents Init="function(s, e) {
	var podeEditar = gvDados.IsNewRowEditing();
	s.enabled = podeEditar;
	s.readOnly = !podeEditar;
}" />
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Informe um valor válido para o campo" IsRequired="True" />
                                            </ValidationSettings>
                                            <Style >
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="Ano da Medição:" CaptionLocation="Top" Visible="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="Mês da Medição" FieldName="Mes" ShowInCustomizationForm="True"
                                        VisibleIndex="2" Name="cmbMes">
                                        <PropertiesComboBox ValueType="System.Int32">
                                            <Items>
                                                <dxe:ListEditItem Text="Janeiro" Value="1" />
                                                <dxe:ListEditItem Text="Fevereiro" Value="2" />
                                                <dxe:ListEditItem Text="Março" Value="3" />
                                                <dxe:ListEditItem Text="Abril" Value="4" />
                                                <dxe:ListEditItem Text="Maio" Value="5" />
                                                <dxe:ListEditItem Text="Junho" Value="6" />
                                                <dxe:ListEditItem Text="Julho" Value="7" />
                                                <dxe:ListEditItem Text="Agosto" Value="8" />
                                                <dxe:ListEditItem Text="Setembro" Value="9" />
                                                <dxe:ListEditItem Text="Outubro" Value="10" />
                                                <dxe:ListEditItem Text="Novembro" Value="11" />
                                                <dxe:ListEditItem Text="Dezembro" Value="12" />
                                            </Items>
                                            <ClientSideEvents Init="function(s, e) {
	var podeEditar = gvDados.IsNewRowEditing();
	s.enabled = podeEditar;
	s.readOnly = !podeEditar;
}" />
                                            <ItemStyle  />
                                            <ListBoxStyle >
                                            </ListBoxStyle>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Selecione um mes" IsRequired="True" />
                                            </ValidationSettings>
                                            <Style >
                                                
                                            </Style>
                                        </PropertiesComboBox>
                                        <EditFormSettings Caption="Mês da Medição:" CaptionLocation="Top" Visible="True" />
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Início do Processo" FieldName="DataInicioPrevistoProcesso"
                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="deInicioProcesso">
                                            <CalendarProperties>
                                                <DayHeaderStyle  />
                                                <WeekNumberStyle >
                                                </WeekNumberStyle>
                                                <DayStyle  />
                                                <DaySelectedStyle >
                                                </DaySelectedStyle>
                                                <DayOtherMonthStyle >
                                                </DayOtherMonthStyle>
                                                <DayWeekendStyle >
                                                </DayWeekendStyle>
                                                <DayOutOfRangeStyle >
                                                </DayOutOfRangeStyle>
                                                <TodayStyle >
                                                </TodayStyle>
                                                <ButtonStyle >
                                                </ButtonStyle>
                                                <HeaderStyle  />
                                                <FooterStyle  />
                                                <FastNavStyle >
                                                </FastNavStyle>
                                                <FastNavMonthAreaStyle >
                                                </FastNavMonthAreaStyle>
                                                <FastNavYearAreaStyle >
                                                </FastNavYearAreaStyle>
                                                <FastNavMonthStyle >
                                                </FastNavMonthStyle>
                                                <FastNavYearStyle >
                                                </FastNavYearStyle>
                                                <FastNavFooterStyle >
                                                </FastNavFooterStyle>
                                                <ReadOnlyStyle >
                                                </ReadOnlyStyle>
                                                <Style >
                                                    
                                                </Style>
                                            </CalendarProperties>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Selecione uma data" IsRequired="True" />
                                            </ValidationSettings>
                                            <Style >
                                                
                                            </Style>
                                        </PropertiesDateEdit>
                                        <EditFormSettings Caption="Início do Processo:" CaptionLocation="Top" Visible="True" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Término do Processo" FieldName="DataTerminoPrevistoProcesso"
                                        ShowInCustomizationForm="True" VisibleIndex="4">
                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="deTerminoProcesso">
                                            <CalendarProperties>
                                                <DayHeaderStyle  />
                                                <WeekNumberStyle >
                                                </WeekNumberStyle>
                                                <DayStyle  />
                                                <DaySelectedStyle >
                                                </DaySelectedStyle>
                                                <DayOtherMonthStyle >
                                                </DayOtherMonthStyle>
                                                <DayWeekendStyle >
                                                </DayWeekendStyle>
                                                <DayOutOfRangeStyle >
                                                </DayOutOfRangeStyle>
                                                <TodayStyle >
                                                </TodayStyle>
                                                <ButtonStyle >
                                                </ButtonStyle>
                                                <HeaderStyle  />
                                                <FooterStyle  />
                                                <FastNavStyle >
                                                </FastNavStyle>
                                                <FastNavMonthAreaStyle >
                                                </FastNavMonthAreaStyle>
                                                <FastNavYearAreaStyle >
                                                </FastNavYearAreaStyle>
                                                <FastNavMonthStyle >
                                                </FastNavMonthStyle>
                                                <FastNavYearStyle >
                                                </FastNavYearStyle>
                                                <FastNavFooterStyle >
                                                </FastNavFooterStyle>
                                                <Style >
                                                    
                                                </Style>
                                            </CalendarProperties>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Selecione uma data" IsRequired="True" />
                                            </ValidationSettings>
                                            <Style >
                                                
                                            </Style>
                                        </PropertiesDateEdit>
                                        <EditFormSettings Caption="Término do Processo:" CaptionLocation="Top" Visible="True" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PodeExcluir" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="5">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True">
                                </SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm"/>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        AllowResize="True" Width="400px" />
                                </SettingsPopup>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>
                                <SettingsText ConfirmDelete="Deseja remover o registro?"></SettingsText>
                            </dxwgv:ASPxGridView>
                            <asp:SqlDataSource ID="dsDados" runat="server" DeleteCommand="DELETE FROM [CalendarioMedicao]
      WHERE [CodigoCalendarioMedicao] = @CodigoCalendarioMedicao" InsertCommand="INSERT INTO [CalendarioMedicao]
           ([Ano]
           ,[Mes]
           ,[DataInicioPrevistoProcesso]
           ,[DataTerminoPrevistoProcesso])
     VALUES
           (@Ano
           ,@Mes
           ,@DataInicioPrevistoProcesso
           ,@DataTerminoPrevistoProcesso)" SelectCommand=" SELECT cm.CodigoCalendarioMedicao,
				cm.Ano,
				cm.Mes,
				cm.DataInicioPrevistoProcesso,
				cm.DataTerminoPrevistoProcesso,
				CASE WHEN EXISTS(SELECT 1 FROM Medicao m WHERE m.AnoMedicao = cm.Ano AND m.MesMedicao = cm.Mes)
					THEN 'N'
					ELSE 'S'
				END PodeExcluir
	 FROM CalendarioMedicao cm
	ORDER BY
				cm.Ano,
				cm.Mes" UpdateCommand="UPDATE [CalendarioMedicao]
   SET [Ano] = @Ano
      ,[Mes] = @Mes
      ,[DataInicioPrevistoProcesso] = @DataInicioPrevistoProcesso
      ,[DataTerminoPrevistoProcesso] = @DataTerminoPrevistoProcesso
 WHERE [CodigoCalendarioMedicao] = @CodigoCalendarioMedicao">
                                <DeleteParameters>
                                    <asp:Parameter Name="CodigoCalendarioMedicao" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Ano" />
                                    <asp:Parameter Name="Mes" />
                                    <asp:Parameter Name="DataInicioPrevistoProcesso" />
                                    <asp:Parameter Name="DataTerminoPrevistoProcesso" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Ano" />
                                    <asp:Parameter Name="Mes" />
                                    <asp:Parameter Name="DataInicioPrevistoProcesso" />
                                    <asp:Parameter Name="DataTerminoPrevistoProcesso" />
                                    <asp:Parameter Name="CodigoCalendarioMedicao" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
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
                                                        &nbsp;
                                                    </td>
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
		mostraDivSalvoPublicado(&quot;Calendário de medição incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Calendário de medição alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Calendário de medição excluído com sucesso!&quot;);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
