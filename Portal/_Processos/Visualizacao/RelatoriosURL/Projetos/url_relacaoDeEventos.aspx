<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relacaoDeEventos.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_relacaoDeEventos"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 5px; padding-right: 5px;">

        <tr>
            <td>
                <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	//document.getElementById('indicador_menu').src = document.getElementById('indicador_menu').src;
	//document.getElementById('indicador_desktop').src = document.getElementById('indicador_desktop').src;
	//pcAlterarIndicador.Hide();
}" />
                </dxcb:ASPxCallback>
                <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" OnRenderBrick="gvExporter_RenderBrick1"
                    PaperKind="A4" PreserveGroupRowStates="True">
                    <Styles>
                        <Default BackColor="#BBCFE7"  ForeColor="Black">
                        </Default>
                        <Header BackColor="#4F81BD" Font-Bold="True" Font-Size="10pt"
                            ForeColor="White" HorizontalAlign="Left">
                        </Header>
                        <Cell >
                        </Cell>
                        <AlternatingRowCell BackColor="#D8E4F2" Enabled="True" ForeColor="Black">
                        </AlternatingRowCell>
                    </Styles>
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                Text="Tipo de Evento:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Unidade:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 355px">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Projeto:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                Text="Início:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 83px">
                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                Text="Final:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; padding-right: 3px;">
                            <dxe:ASPxComboBox ID="ddlTipoEvento" runat="server" ClientInstanceName="ddlTipoEvento"
                                 Width="100%" 
                                IncrementalFilteringMode="Contains">
                                <Paddings Padding="0px" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 120px; padding-right: 3px;">
                            <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                 Width="100%" IncrementalFilteringMode="Contains">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
pcUsuarioIncluido.Show();
ddlProjeto.SetEnabled(true);	
ddlProjeto.PerformCallback();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 355px; padding-right: 3px;">
                            <dxe:ASPxComboBox ID="ddlProjeto" runat="server" ClientInstanceName="ddlProjeto"
                                 Width="100%" OnCallback="ddlProjeto_Callback"
                                ClientEnabled="False" IncrementalFilteringMode="Contains">
                                <ClientSideEvents EndCallback="function(s, e) {
	pcUsuarioIncluido.Hide();
}" />
                                <Paddings Padding="0px" />
                                <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 95px; padding-right: 3px;">
                            <dxe:ASPxDateEdit ID="dteInicio" runat="server" ClientInstanceName="dteInicio"
                                Width="100%">
                                <CalendarProperties>
                                    <DayHeaderStyle  />
                                    <DayStyle  />
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <ButtonStyle >
                                    </ButtonStyle>
                                    <HeaderStyle  />
                                </CalendarProperties>
                                <ButtonStyle>
                                    <Paddings Padding="0px" />
                                </ButtonStyle>
                                <Paddings Padding="0px" />
                            </dxe:ASPxDateEdit>
                        </td>
                        <td style="width: 95px; padding-right: 3px;">
                            <dxe:ASPxDateEdit ID="dteTermino" runat="server" ClientInstanceName="dteTermino"
                                 Width="100%">
                                <CalendarProperties>
                                    <DayStyle  />
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <ButtonStyle >
                                    </ButtonStyle>
                                    <HeaderStyle  />
                                </CalendarProperties>
                                <ButtonStyle>
                                    <Paddings Padding="0px" />
                                </ButtonStyle>
                                <Paddings Padding="0px" />
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>
                            <dxe:ASPxCheckBox ID="ckbParticipo" runat="server" ClientInstanceName="ckbParticipo"
                                 Text="Eventos que Participo">
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                Text="Usuário:">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="width: 95px">
                        </td>
                        <td style="width: 95px">
                            &nbsp;
                        </td>
                        <td style="width: 102px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 529px; padding-right: 3px;">
                            <dxe:ASPxComboBox ID="ddlUsuario" runat="server" ClientInstanceName="ddlUsuario"
                                 Width="100%" IncrementalFilteringMode="Contains">
                                <Paddings Padding="0px" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td style="width: 95px; padding-right: 3px">
                            <dxe:ASPxButton ID="btnGeraRelatorio" runat="server" 
                                Text="Selecionar" Width="95px" ClientInstanceName="btnGeraRelatorio" Height="5px">
                                <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;	
	if(validaData() == '')	
	{
		gvDados.PerformCallback();
	}
	else
	{
		window.top.mostraMensagem(validaData(), 'atencao', true, false, null);
	}		
}" />
                                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="width: 95px">
                            <table>
                                <tr>
                                    <td style="width: 95px">
                                        <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                            >
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td style="padding-left: 2px">
                                        <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                            Height="22px" OnCallback="pnImage_Callback"  
                                            Width="23px">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                        Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px">
                                                    </dxe:ASPxImage>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 102px">
                            <dxe:ASPxButton ID="btnExportar" runat="server" 
                                Text="Exportar" Width="95px" ClientInstanceName="btnExportar" Height="5px" AutoPostBack="False"
                                OnClick="btnExportar_Click">
                                <ClientSideEvents Click="function(s, e) {
	if(validaData() == '')	
		gvDados.PerformCallback();
	else
		window.top.mostraMensagem(validaData(), 'atencao', true, false, null);
}" />
                                <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="_CodigoTarefa"
                    AutoGenerateColumns="False" Width="100%" 
                    ID="gvDados" OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
		//OnGridFocusedRowChanged(s,true);
}" CustomButtonClick="function(s, e) 
{
	if(e.buttonID == &quot;btnFormulario&quot;)
	{			
		//onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
	}
}"></ClientSideEvents>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusProjeto" VisibleIndex="5"
                            Width="160px" Visible="False">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="SiglaUnidadeNegocio" VisibleIndex="0">
                            <Settings AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="1">
                            <Settings AllowAutoFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Evento" FieldName="Evento" VisibleIndex="2">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="Inicio" VisibleIndex="3"
                            Width="110px">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Equals" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="Termino" VisibleIndex="4"
                            Width="110px">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Equals" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="_CodigoProjeto" FieldName="_CodigoProjeto"
                            VisibleIndex="8" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="_CodigoCronogramaProjeto" FieldName="_CodigoCronogramaProjeto"
                            Visible="False" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="_CodigoTarefa" FieldName="_CodigoTarefa" Visible="False"
                            VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="_CodigoUnidadeNegocio" FieldName="_CodigoUnidadeNegocio"
                            Visible="False" VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="%Concluído" FieldName="PercentualConcluido"
                            VisibleIndex="7" Width="90px" Visible="False">
                            <PropertiesSpinEdit DisplayFormatString="g">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </PropertiesSpinEdit>
                            <Settings AllowAutoFilter="True" />
                        </dxwgv:GridViewDataSpinEditColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Evento" FieldName="DescricaoTipoTarefaCronograma"
                            VisibleIndex="6" Name="colunaIndicaEventoInstitucional">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AutoExpandAllGroups="True">
                    </SettingsBehavior>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" ShowGroupPanel="True">
                    </Settings>
                    <Styles>
                        <AlternatingRow BackColor="#D8E4F2" Enabled="True">
                        </AlternatingRow>
                        <Cell BackColor="#BBCFE7">
                            <Border BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                        </Cell>
                    </Styles>
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False"
                    ShowHeader="False" Width="200px"  ID="pcUsuarioIncluido">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                Font-Size="10pt" Text="Aguarde, carregando...">
                            </dxe:ASPxLabel>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
