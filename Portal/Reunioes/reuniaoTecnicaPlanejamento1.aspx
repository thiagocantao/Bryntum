<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="reuniaoTecnicaPlanejamento1.aspx.cs" Inherits="Reunioes_reuniaoTecnicaPlanejamento1"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                    Font-Bold="True"  
                    EnableViewState="False">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
                <dxe:ASPxLabel ID="lblUnidade" runat="server" 
                    Text="Unidade:" EnableViewState="False">
                </dxe:ASPxLabel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
                <dxe:ASPxTextBox ID="txtUnidade" runat="server" ClientEnabled="False"
                    Width="99%" EnableViewState="False">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                    <Paddings Padding="0px" />
                </dxe:ASPxTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
            <td style="height: 10px">
            </td>
            <td style="height: 14px">
            </td>
        </tr>
        <tr>
            <td align="left">
            </td>
            <td id="tdLista" align="left">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback" EnableViewState="False">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoEvento"
                                AutoGenerateColumns="False" Width="100%" 
                                ID="gvDados" EnableViewState="False" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnPlan&quot;)
     {
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		desabilitaHabilitaComponentes();
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		pcDados.Show();
     }
	 else if(e.buttonID == &quot;btnReal&quot;)
     {	
		somenteLeituraExecucao = 'N';
		s.GetRowValues(e.visibleIndex, &quot;CodigoEvento;&quot;, abreExecucao)
     }
	 else if(e.buttonID == &quot;btnDetalheReal&quot;)
     {	
		somenteLeituraExecucao = 'S';
		s.GetRowValues(e.visibleIndex, &quot;CodigoEvento;&quot;, abreExecucao)
     }	
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#227;o" Name="A&#231;&#227;o"
                                        VisibleIndex="0" Width="135px">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnPlan" Text="Editar Planejamento">
                                                <Image Url="~/imagens/planejamentoReuniao.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnReal" Text="Editar Realiza&#231;&#227;o">
                                                <Image Url="~/imagens/realizacaoReuniao.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheReal" Text="Detalhe">
                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <HeaderTemplate>
                                            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""novaReuniao();"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Assunto" FieldName="DescricaoResumida" Name="DescricaoResumida"
                                        VisibleIndex="1">
                                        <Settings AllowAutoFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="InicioPrevisto" Name="Local"
                                        VisibleIndex="2" Width="130px">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                        </PropertiesDateEdit>
                                        <Settings AllowAutoFilter="False" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quando" FieldName="Quando" Name="Quando" VisibleIndex="3"
                                        Width="135px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                        </PropertiesTextEdit>
                                        <Settings AllowAutoFilter="True" />
                                        <FilterCellStyle >
                                            <Paddings Padding="0px" />
                                        </FilterCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelEvento" Name="CodigoResponsavelEvento"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoData" Name="inicioPrevistoData"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoHora" Name="inicioPrevistoHora"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoData" Name="TerminoPrevistoData"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoHora" Name="TerminoPrevistoHora"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioReal" Name="InicioReal" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioRealData" Name="InicioRealData" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InicioRealHora" Name="InicioRealHora" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoReal" Name="TerminoReal" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoRealData" Name="TerminoRealData"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TerminoRealHora" Name="TerminoRealHora"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="LocalEvento" Name="LocalEvento" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Pauta" Name="Pauta" Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ResumoEvento" Name="ResumoEvento" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoEvento" Name="CodigoTipoEvento"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                        Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible">
                                </Settings>
                                <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                CloseAction="None" HeaderText="Reuni&#227;o da Unidade" PopupAction="None" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="790px" ID="pcDados"
                                EnableViewState="False">
                                <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
}"></ClientSideEvents>
                                <ContentStyle>
                                    <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px">
                                    </Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True" ></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario" Style="overflow: auto">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl"
                                                                        Width="99%"  ID="tabControl">
                                                                        <TabPages>
                                                                            <dxtc:TabPage Name="TabA" Text="Reuni&#227;o">
                                                                                <ContentCollection>
                                                                                    <dxw:ContentControl runat="server">
                                                                                        <table cellspacing="0" cellpadding="0" width="98%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 384px">
                                                                                                                        <asp:Label runat="server" Text="Assunto:" ID="lblAssunto"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label runat="server" Text="Respons&#225;vel:" ID="lblResponsavel"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 384px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtAssunto"
                                                                                                                             ID="txtAssunto">
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                                                <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                                                                            Width="100%" ClientInstanceName="ddlResponsavelEvento" 
                                                                                                                            ID="ddlResponsavelEvento">
                                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//lbDisponiveis.PerformCallback();
}"></ClientSideEvents>
                                                                                                                            <Columns>
                                                                                                                                <dxe:ListBoxColumn Caption="Nome"></dxe:ListBoxColumn>
                                                                                                                                <dxe:ListBoxColumn Caption="Email"></dxe:ListBoxColumn>
                                                                                                                            </Columns>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                                                <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                                            </ValidationSettings>
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
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="0" cellpadding="0">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 384px">
                                                                                                                        <asp:Label runat="server" Text="Tipo de Reuni&#227;o:" ID="lblTipoEventos"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="width: 10px; height: 10px">
                                                                                                                    </td>
                                                                                                                    <td style="width: 100px">
                                                                                                                        <asp:Label runat="server" Text="In&#237;cio:" ID="lblInicio"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="width: 99px">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td style="width: 100px">
                                                                                                                        <asp:Label runat="server" Text="T&#233;rmino:" ID="lblTermino"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 384px">
                                                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTipoEvento"
                                                                                                                             ID="ddlTipoEvento">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="height: 10px">
                                                                                                                    </td>
                                                                                                                    <td style="width: 100px">
                                                                                                                        <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicioPrevisto"
                                                                                                                             ID="ddlInicioPrevisto">
                                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                                                                                <DayHeaderStyle ></DayHeaderStyle>
                                                                                                                                <DayStyle ></DayStyle>
                                                                                                                                <DayWeekendStyle >
                                                                                                                                </DayWeekendStyle>
                                                                                                                                <Style ></Style>
                                                                                                                            </CalendarProperties>
                                                                                                                            <ClientSideEvents DateChanged="function(s, e) {
	ddlTerminoPrevisto.SetDate(s.GetValue());
	calendar = ddlTerminoPrevisto.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxDateEdit>
                                                                                                                    </td>
                                                                                                                    <td style="width: 99px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="50px" ClientInstanceName="txtHoraInicio"
                                                                                                                            ID="txtHoraInicio">
                                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="height: 10px">
                                                                                                                    </td>
                                                                                                                    <td style="width: 100px">
                                                                                                                        <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTerminoPrevisto"
                                                                                                                             ID="ddlTerminoPrevisto">
                                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                                                <DayHeaderStyle ></DayHeaderStyle>
                                                                                                                                <DayStyle ></DayStyle>
                                                                                                                                <DaySelectedStyle >
                                                                                                                                </DaySelectedStyle>
                                                                                                                                <DayOtherMonthStyle >
                                                                                                                                </DayOtherMonthStyle>
                                                                                                                                <DayWeekendStyle >
                                                                                                                                </DayWeekendStyle>
                                                                                                                                <DayOutOfRangeStyle >
                                                                                                                                </DayOutOfRangeStyle>
                                                                                                                                <ButtonStyle >
                                                                                                                                </ButtonStyle>
                                                                                                                                <HeaderStyle ></HeaderStyle>
                                                                                                                            </CalendarProperties>
                                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxDateEdit>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="50px" ClientInstanceName="txtHoraTermino"
                                                                                                                             ID="txtHoraTermino">
                                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                                            </ValidationSettings>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" Text="Local:" ID="lblLocal"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="memoLocal"
                                                                                                             ID="memoLocal">
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                                <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label runat="server" Text="Pauta:" ID="lblPauta"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoPauta" Width="775px"
                                                                                                            Height="180px"  ID="memoPauta">
                                                                                                            <Styles>
                                                                                                                <ContentArea>
                                                                                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                                    </Paddings>
                                                                                                                </ContentArea>
                                                                                                            </Styles>
                                                                                                            <Toolbars>
                                                                                                                <dxhe:HtmlEditorToolbar>
                                                                                                                    <Items>
                                                                                                                        <dxhe:ToolbarCutButton>
                                                                                                                        </dxhe:ToolbarCutButton>
                                                                                                                        <dxhe:ToolbarCopyButton>
                                                                                                                        </dxhe:ToolbarCopyButton>
                                                                                                                        <dxhe:ToolbarPasteButton>
                                                                                                                        </dxhe:ToolbarPasteButton>
                                                                                                                        <dxhe:ToolbarPasteFromWordButton>
                                                                                                                        </dxhe:ToolbarPasteFromWordButton>
                                                                                                                        <dxhe:ToolbarUndoButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarUndoButton>
                                                                                                                        <dxhe:ToolbarRedoButton>
                                                                                                                        </dxhe:ToolbarRedoButton>
                                                                                                                        <dxhe:ToolbarRemoveFormatButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarRemoveFormatButton>
                                                                                                                        <dxhe:ToolbarSuperscriptButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarSuperscriptButton>
                                                                                                                        <dxhe:ToolbarSubscriptButton>
                                                                                                                        </dxhe:ToolbarSubscriptButton>
                                                                                                                        <dxhe:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarInsertOrderedListButton>
                                                                                                                        <dxhe:ToolbarInsertUnorderedListButton>
                                                                                                                        </dxhe:ToolbarInsertUnorderedListButton>
                                                                                                                        <dxhe:ToolbarIndentButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarIndentButton>
                                                                                                                        <dxhe:ToolbarOutdentButton>
                                                                                                                        </dxhe:ToolbarOutdentButton>
                                                                                                                        <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarInsertLinkDialogButton>
                                                                                                                        <dxhe:ToolbarUnlinkButton>
                                                                                                                        </dxhe:ToolbarUnlinkButton>
                                                                                                                        <dxhe:ToolbarInsertImageDialogButton Visible="false">
                                                                                                                        </dxhe:ToolbarInsertImageDialogButton>
                                                                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                                                            <Items>
                                                                                                                                <dxhe:ToolbarInsertTableDialogButton ViewStyle="ImageAndText" BeginGroup="True">
                                                                                                                                </dxhe:ToolbarInsertTableDialogButton>
                                                                                                                                <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                                                                                </dxhe:ToolbarTablePropertiesDialogButton>
                                                                                                                                <dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                                                                </dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                                                                <dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                                                                </dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                                                                <dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                                                                </dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                                                                <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                                                                                </dxhe:ToolbarInsertTableRowAboveButton>
                                                                                                                                <dxhe:ToolbarInsertTableRowBelowButton>
                                                                                                                                </dxhe:ToolbarInsertTableRowBelowButton>
                                                                                                                                <dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                                                                </dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                                                                <dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                                                                </dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                                                                <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                                                                                </dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                                                                <dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                                                                </dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                                                                <dxhe:ToolbarMergeTableCellRightButton>
                                                                                                                                </dxhe:ToolbarMergeTableCellRightButton>
                                                                                                                                <dxhe:ToolbarMergeTableCellDownButton>
                                                                                                                                </dxhe:ToolbarMergeTableCellDownButton>
                                                                                                                                <dxhe:ToolbarDeleteTableButton BeginGroup="True">
                                                                                                                                </dxhe:ToolbarDeleteTableButton>
                                                                                                                                <dxhe:ToolbarDeleteTableRowButton>
                                                                                                                                </dxhe:ToolbarDeleteTableRowButton>
                                                                                                                                <dxhe:ToolbarDeleteTableColumnButton>
                                                                                                                                </dxhe:ToolbarDeleteTableColumnButton>
                                                                                                                            </Items>
                                                                                                                        </dxhe:ToolbarTableOperationsDropDownButton>
                                                                                                                        <dxhe:ToolbarFullscreenButton>
                                                                                                                        </dxhe:ToolbarFullscreenButton>
                                                                                                                    </Items>
                                                                                                                </dxhe:HtmlEditorToolbar>
                                                                                                                <dxhe:HtmlEditorToolbar>
                                                                                                                    <Items>
                                                                                                                        <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                                                            <Items>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                                                            </Items>
                                                                                                                        </dxhe:ToolbarParagraphFormattingEdit>
                                                                                                                        <dxhe:ToolbarFontNameEdit>
                                                                                                                            <Items>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                                                            </Items>
                                                                                                                        </dxhe:ToolbarFontNameEdit>
                                                                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                                                                            <Items>
                                                                                                                                <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                                                                <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                                                            </Items>
                                                                                                                        </dxhe:ToolbarFontSizeEdit>
                                                                                                                        <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarBoldButton>
                                                                                                                        <dxhe:ToolbarItalicButton>
                                                                                                                        </dxhe:ToolbarItalicButton>
                                                                                                                        <dxhe:ToolbarUnderlineButton>
                                                                                                                        </dxhe:ToolbarUnderlineButton>
                                                                                                                        <dxhe:ToolbarStrikethroughButton>
                                                                                                                        </dxhe:ToolbarStrikethroughButton>
                                                                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarJustifyLeftButton>
                                                                                                                        <dxhe:ToolbarJustifyCenterButton>
                                                                                                                        </dxhe:ToolbarJustifyCenterButton>
                                                                                                                        <dxhe:ToolbarJustifyRightButton>
                                                                                                                        </dxhe:ToolbarJustifyRightButton>
                                                                                                                        <dxhe:ToolbarJustifyFullButton>
                                                                                                                        </dxhe:ToolbarJustifyFullButton>
                                                                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                                                                        </dxhe:ToolbarBackColorButton>
                                                                                                                        <dxhe:ToolbarFontColorButton>
                                                                                                                        </dxhe:ToolbarFontColorButton>
                                                                                                                    </Items>
                                                                                                                </dxhe:HtmlEditorToolbar>
                                                                                                            </Toolbars>
                                                                                                            <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                                                                        </dxhe:ASPxHtmlEditor>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </dxw:ContentControl>
                                                                                </ContentCollection>
                                                                            </dxtc:TabPage>
                                                                            <dxtc:TabPage Name="TabB" Text="Participantes">
                                                                                <ContentCollection>
                                                                                    <dxw:ContentControl runat="server">
                                                                                        &nbsp;<table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 340px">
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblSelecionado"
                                                                                                             Text="Disponíveis:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                    </td>
                                                                                                    <td style="width: 340px">
                                                                                                        <dxe:ASPxLabel runat="server" Text="Selecionados:" ClientInstanceName="lblSelecionado"
                                                                                                             ID="lblSelecionado">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxListBox ID="lbDisponiveis" runat="server" ClientInstanceName="lbDisponiveis"
                                                                                                                        EnableClientSideAPI="True" EncodeHtml="False" 
                                                                                                                        Height="120px" OnCallback="lbDisponiveis_Callback"
                                                                                                                        Rows="10" SelectionMode="Multiple" Width="100%">
                                                                                                                        <ItemStyle BackColor="Transparent">
                                                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                                                            </SelectedStyle>
                                                                                                                            <Paddings Padding="0px" />
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </ItemStyle>

<SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}" />
                                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                                                                        <ValidationSettings>
                                                                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px">
                                                                                                                            </ErrorImage>
                                                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                                                            </ErrorFrameStyle>
                                                                                                                        </ValidationSettings>
                                                                                                                        <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                                                                        </DisabledStyle>
                                                                                                                    </dxe:ASPxListBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" ClientInstanceName="lblSelecionado"
                                                                                                                         Text="Grupos:">
                                                                                                                    </dxe:ASPxLabel>
                                                                                                                    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	//debugger;
    var delimitador = &quot;¥&quot;;
  	var listaCodigos = s.cp_ListaCodigos;
  	arrayItens = new Array();
  	arrayItens = listaCodigos.split(';');
    
    
    for (var i = 0; i &lt; lbSelecionados.GetItemCount(); i++)
    {
        var objetoArray = lbSelecionados.GetItem(i).text + delimitador + lbSelecionados.GetItem(i).value;
        if (buscaNoArray(arrayItens, objetoArray) == false)
        {
             arrayItens.push(objetoArray);
        }		
	}
    lbSelecionados.BeginUpdate();
    lbSelecionados.ClearItems();
	for (i = 0; i &lt; arrayItens.length; i++)
    {

        temp = arrayItens[i].split(delimitador);
        if((temp[0] != null) &amp;&amp; (temp[1] != null))
        {
           lbSelecionados.AddItem(temp[0], temp[1]);
           var item = lbDisponiveis.FindItemByValue(temp[1]);
           if(item != null)
           {
               //debugger;
              lbDisponiveis.BeginUpdate(); 
              lbDisponiveis.RemoveItem(item.index);
              lbDisponiveis.EndUpdate();
           }
        }
    }
    lbSelecionados.EndUpdate();
}" />
                                                                                                                        <ClientSideEvents EndCallback="function(s, e) 
{
	var delimitador = &quot;¥&quot;;
  	var listaCodigos = s.cp_ListaCodigos;
  	var arrayItens = listaCodigos.split(';');

    //arrayItens.sort();
    var array3 = new Array();

    for (i = 0; i &lt; arrayItens.length; i++)
    {
        temp = arrayItens[i].split(delimitador);
        if((temp[0] != null) &amp;&amp; (temp[1] != null))
        {
           array3.push(temp[1]);
        }
    }
    //lbDisponiveis.BeginUpdate(); 
    lbDisponiveis.UnselectAll();
    lbDisponiveis.SelectValues(array3);
    //lbDisponiveis.EndUpdate();    

    UpdateButtons();    
    setTimeout('fechaTelaEdicao();', 10);
}" BeginCallback="function(s, e) {
	mostraDivAtualizando('Atualizando...'); 
}"></ClientSideEvents>
                                                                                                                    </dxcb:ASPxCallback>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxListBox ID="lbGrupos" runat="server" ClientInstanceName="lbGrupos" EnableClientSideAPI="True"
                                                                                                                        EncodeHtml="False"  Height="120px" ImageFolder="~/App_Themes/Aqua/{0}/"
                                                                                                                         Rows="10" SelectionMode="Multiple"
                                                                                                                        Width="100%">
                                                                                                                        <ItemStyle BackColor="Transparent">
                                                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                                                            </SelectedStyle>
                                                                                                                            <Paddings Padding="0px" />
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </ItemStyle>

<SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	callback.PerformCallback(s.GetValue());
}" />
                                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
    callback.PerformCallback(s.GetValue());
}"></ClientSideEvents>
                                                                                                                        <ValidationSettings>
                                                                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px">
                                                                                                                            </ErrorImage>
                                                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                                                            </ErrorFrameStyle>
                                                                                                                        </ValidationSettings>
                                                                                                                        <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                                                                        </DisabledStyle>
                                                                                                                    </dxe:ASPxListBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td style="width: 60px" align="center">
                                                                                                        <table cellspacing="0" cellpadding="0">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 41px; height: 28px" valign="middle">
                                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos"
                                                                                                                            ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                                            Font-Bold="True"  ID="btnADDTodos">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </dxe:ASPxButton>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 41px; height: 28px">
                                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False"
                                                                                                                            Text="&gt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"
                                                                                                                            ID="btnADD">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </dxe:ASPxButton>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 41px; height: 28px">
                                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False"
                                                                                                                            Text="&lt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"
                                                                                                                            ID="btnRMV">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </dxe:ASPxButton>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 41px; height: 28px">
                                                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos"
                                                                                                                            ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                                            Font-Bold="True"  ID="btnRMVTodos">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>
                                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                                        </dxe:ASPxButton>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <dxe:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" Rows="10"
                                                                                                             SelectionMode="Multiple" ImageFolder="~/App_Themes/Aqua/{0}/"
                                                                                                            ClientInstanceName="lbSelecionados" EnableClientSideAPI="True" Width="100%" Height="120px"
                                                                                                             ID="lbSelecionados" OnCallback="lbSelecionados_Callback">
                                                                                                            <ItemStyle BackColor="Transparent">
                                                                                                                <SelectedStyle BackColor="#FFE4AC">
                                                                                                                </SelectedStyle>
                                                                                                                <Paddings Padding="0px" />
                                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                                            </ItemStyle>

<SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                                                            <ValidationSettings>
                                                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                                                </ErrorImage>
                                                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                                                </ErrorFrameStyle>
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxListBox>
                                                                                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfRiscosSelecionados" ID="hfRiscosSelecionados">
                                                                                                        </dxhf:ASPxHiddenField>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </dxw:ContentControl>
                                                                                </ContentCollection>
                                                                            </dxtc:TabPage>
                                                                            <dxtc:TabPage Name="tabProjetos" Text="Projetos">
                                                                                <ContentCollection>
                                                                                    <dxw:ContentControl runat="server">
                                                                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvProjetos" KeyFieldName="Codigo"
                                                                                            AutoGenerateColumns="False" Width="100%" 
                                                                                            ID="gvProjetos" OnAfterPerformCallback="gvProjetos_AfterPerformCallback" OnCustomCallback="gvProjetos_CustomCallback">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" Width="5%" Caption=" " VisibleIndex="0">
                                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                    <HeaderTemplate>
                                                                                                        <input type="checkbox" onclick="gvProjetos.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos" />
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn FieldName="Desempenho" Width="5%" Caption=" " VisibleIndex="1">
                                                                                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' /&gt;">
                                                                                                    </PropertiesTextEdit>
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn FieldName="Descricao" Width="90%" Caption="Projetos"
                                                                                                    VisibleIndex="2">
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="380"></Settings>
                                                                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                                                            <Styles>
                                                                                                <SelectedRow BackColor="Transparent" ForeColor="Black">
                                                                                                </SelectedRow>
                                                                                            </Styles>
                                                                                        </dxwgv:ASPxGridView>
                                                                                    </dxw:ContentControl>
                                                                                </ContentCollection>
                                                                            </dxtc:TabPage>
                                                                        </TabPages>
                                                                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	// se for a aba 'Participantes', atualiza a disponibilidade dos botões de 'seleção'
    if(s.GetActiveTab().index == 1)
    {	    
    	UpdateButtons();
    }

}"></ClientSideEvents>
                                                                        <ContentStyle>
                                                                            <Paddings PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px">
                                                                            </Paddings>
                                                                        </ContentStyle>
                                                                    </dxtc:ASPxPageControl>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 90px">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" ValidationGroup="MKE" Width="100%" Height="20px"
                                                                            ID="btnSalvar" EnableViewState="False">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	capturaCodigosInteressados();
	if(verificarDadosPreenchidos())
		if (window.onClick_btnSalvar)
	    	onClick_btnSalvar();
	else
		return false;
}"></ClientSideEvents>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarPauta"
                                                                            ClientVisible="False" Text="Enviar Pauta" ValidationGroup="MKE" Width="100px"
                                                                             ID="btnEnviarPauta" EnableViewState="False">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	var podeEditar = btnEnviarPauta.cp_EditaMensagem;

	if(podeEditar == 'N')
	{
		if (confirm('A reuni&#227;o ser&#225; Salva, Deseja continuar?'))
		{
			capturaCodigosInteressados();
			if(verificarDadosPreenchidos())
			{
				tipoEnvio = &quot;EnviarPauta&quot;;
				pnCallback.PerformCallback(tipoEnvio);
			}
		
		}
	}
	else
	{
		pcMensagemPauta.Show();
	}
}"></ClientSideEvents>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 90px">
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar2"
                                                                            Text="Fechar" Width="100%"  ID="btnFechar2"
                                                                            EnableViewState="False">
                                                                            <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
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
                            <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcMensagemPauta"
                                CloseAction="None" HeaderText="Envio de pauta" PopupAction="None" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px" ID="pcMensagemPauta"
                                EnableViewState="False">
                                <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
}"></ClientSideEvents>
                                <ContentStyle>
                                    <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px">
                                    </Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True" ></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" Text="Texto de apresenta&#231;&#227;o:"
                                                                            ID="Label1"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heEncabecadoAta" Width="98%"
                                                                            Height="120px"  ID="heEncabecadoAta">
                                                                            <Toolbars>
                                                                                <dxhe:HtmlEditorToolbar>
                                                                                    <Items>
                                                                                        <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                            <Items>
                                                                                                <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                            </Items>
                                                                                        </dxhe:ToolbarParagraphFormattingEdit>
                                                                                        <dxhe:ToolbarFontNameEdit>
                                                                                            <Items>
                                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                            </Items>
                                                                                        </dxhe:ToolbarFontNameEdit>
                                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                                            <Items>
                                                                                                <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                                <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                            </Items>
                                                                                        </dxhe:ToolbarFontSizeEdit>
                                                                                        <dxhe:ToolbarBoldButton BeginGroup="True">
                                                                                        </dxhe:ToolbarBoldButton>
                                                                                        <dxhe:ToolbarItalicButton>
                                                                                        </dxhe:ToolbarItalicButton>
                                                                                        <dxhe:ToolbarUnderlineButton>
                                                                                        </dxhe:ToolbarUnderlineButton>
                                                                                        <dxhe:ToolbarStrikethroughButton>
                                                                                        </dxhe:ToolbarStrikethroughButton>
                                                                                        <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                                                        </dxhe:ToolbarJustifyLeftButton>
                                                                                        <dxhe:ToolbarJustifyCenterButton>
                                                                                        </dxhe:ToolbarJustifyCenterButton>
                                                                                        <dxhe:ToolbarJustifyRightButton>
                                                                                        </dxhe:ToolbarJustifyRightButton>
                                                                                        <dxhe:ToolbarJustifyFullButton>
                                                                                        </dxhe:ToolbarJustifyFullButton>
                                                                                        <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                                        </dxhe:ToolbarBackColorButton>
                                                                                        <dxhe:ToolbarFontColorButton>
                                                                                        </dxhe:ToolbarFontColorButton>
                                                                                    </Items>
                                                                                </dxhe:HtmlEditorToolbar>
                                                                            </Toolbars>
                                                                            <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                                        </dxhe:ASPxHtmlEditor>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 10px" align="right">
                                                                        <table cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarEncabecadoPauta"
                                                                                            Text="Enviar" ValidationGroup="MKE" Width="100px" 
                                                                                            ID="btnEnviarEncabecadoPauta" EnableViewState="False">
                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;

		capturaCodigosInteressados();

		if(verificarDadosPreenchidos())
		{
			tipoEnvio = &quot;EnviarPauta&quot;;
			pnCallback.PerformCallback(tipoEnvio);
		}
		
}"></ClientSideEvents>
                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 90px">
                                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarEncabecadoPauta"
                                                                                            Text="Cancelar" Width="100%"  ID="btnCancelarEncabecadoPauta"
                                                                                            EnableViewState="False">
                                                                                            <ClientSideEvents Click="function(s, e) {	
	pcMensagemPauta.Hide();
}"></ClientSideEvents>
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
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
                    <ClientSideEvents EndCallback="function(s, e) {	
	

	if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Reunião excluída com sucesso!&quot;, 'sucesso', false, false, null);
	else
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		{
			window.top.mostraMensagem('Deseja enviar a pauta da reunião aos participantes?', 'confirmacao', true, true, enviaPautaDeReuniao);
            //window.top.mostraMensagem(&quot;Reunião salva com sucesso!&quot;, 'sucesso', false, false, null);
			
		}	
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Dados gravados com sucesso!&quot;, 'sucesso', false, false, null);	
		else if(&quot;EnviarPauta&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Pauta enviada com sucesso aos participantes!&quot;, 'sucesso', false, false, null);	
		else if(&quot;EnviarAta&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(&quot;Ata enviada com sucesso aos participantes!&quot;, 'sucesso', false, false, null);	
		else if(&quot;Erro&quot; == s.cp_OperacaoOk)
			window.top.mostraMensagem(hfGeral.Get(&quot;ErroSalvar&quot;), 'erro', true, false, null);	 
		
	}
	if (window.onEnd_pnCallback &amp;&amp; &quot;Erro&quot; != s.cp_OperacaoOk)
	    onEnd_pnCallback();	
	
	//btnNovaReuniao.SetVisible(true);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left">
            </td>
            <td align="left">
                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True"
                    Text="Para editar a realização da reunião é necessário que o seu navegador aceite a abertura de POP-UPs. ">
                </dxe:ASPxLabel>
            </td>
            <td align="left">
            </td>
        </tr>
    </table>
</asp:Content>
