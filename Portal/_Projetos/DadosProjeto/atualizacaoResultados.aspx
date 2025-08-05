<%@ Page Language="C#" AutoEventWireup="true" CodeFile="atualizacaoResultados.aspx.cs" Inherits="atualizacaoResultados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Atualização dos Resulados</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <style type="text/css">
        .style1 {
            height: 14px;
        }

        .style12 {
            width: 110px;
        }

        .style13 {
            width: 130px;
        }

        .style14 {
            width: 10px;
            height: 10px;
        }

        .style15 {
            height: 10px;
        }
    </style>
</head>
<body class="body" enableviewstate="false">
    <form id="form1" runat="server" enableviewstate="false">
        <table>
            <tr>
                <td class="style14"></td>
                <td class="style15"></td>
                <td class="style14"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                        LeftMargin="50" RightMargin="50"
                        Landscape="True" ID="ASPxGridViewExporter1"
                        ExportEmptyDetailGrid="True" PreserveGroupRowStates="False"
                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                    
                    <div id="divGrid" style="visibility:hidden">
                        <dxwgv:ASPxGridView ID="gvDados" runat="server"
                        AutoGenerateColumns="False" ClientInstanceName="gvDados"
                        KeyFieldName="CodigoMetaOperacional" Width="100%"
                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                        <SettingsBehavior AllowFocusedRow="True" AllowDragDrop="False" AutoExpandAllGroups="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, false);
}"
                            CustomButtonClick="function(s, e) {
	OnClick_CustomEditarGvDado(s, e);
}"
                            Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}" ></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="35px" Caption=" " VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="imgEditar" Text="Atualizar Resultados de Desempenho">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
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
                            <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Width="400px"
                                Caption="Indicador" VisibleIndex="2">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False"
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False"
                                VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" Visible="False"
                                VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Meta" Name="Meta" Caption="Meta"
                                VisibleIndex="6">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo de Indicador"
                                FieldName="DescTipoIndicador" GroupIndex="0" Name="TipoIndicador" SortIndex="0"
                                SortOrder="Ascending" VisibleIndex="1" Width="150px">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Fonte" FieldName="FonteIndicador"
                                VisibleIndex="7" Visible="False">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>

                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowGroupPanel="True"
                            ShowHeaderFilterBlankItems="False"></Settings>
                    </dxwgv:ASPxGridView>
                    </div>
                    
                    <dxpc:ASPxPopupControl ID="pcDados" runat="server"
                        HeaderText="Detalhes" ClientInstanceName="pcDados" CloseAction="None"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ShowCloseButton="False" AllowDragging="True"
                        Width="850px">
                        <ContentStyle>
                            <Paddings PaddingBottom="8px"></Paddings>
                        </ContentStyle>

                        <ClientSideEvents Closing="function(s, e) {	
        LimpaCamposFormulario();
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}"
                            Shown="function(s, e) {	
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}"></ClientSideEvents>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="1"
                                                    ClientInstanceName="tabEdicao"
                                                    ID="ASPxPageControl1" Width="100%">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="tbDados" Text="Metas">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table style="width: 100%" cellspacing="0"
                                                                        cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel4"></dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIndicador" ClientEnabled="False" ID="txtIndicador">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                        Text="Meta:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtMeta" runat="server" ClientEnabled="False"
                                                                                        ClientInstanceName="txtMeta" Rows="3"
                                                                                        Width="100%">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px">&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="style13" style="width: 25%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                                        Text="Meta Numérica:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td class="style13" style="width: 25%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                                        Text="Unidade de Medida:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td class="style12" style="width: 25%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server"
                                                                                                        Text="Agrupamento:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="width: 25%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                                                                                        Text="Polaridade:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style13" style="padding-right: 10px;">
                                                                                                    <dxe:ASPxTextBox ID="txtValorMeta" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtValorMeta" DisplayFormatString="{0:n2}"
                                                                                                        HorizontalAlign="Right" Width="100%">
                                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                                        <Paddings PaddingLeft="0px" />
                                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                                        <Paddings PaddingLeft="0px"></Paddings>

                                                                                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                                                        </ValidationSettings>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="style13" style="padding-right: 10px;">
                                                                                                    <dxe:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtUnidadeMedida"
                                                                                                        Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="style12" style="padding-right: 10px;">
                                                                                                    <dxe:ASPxTextBox ID="txtAgrupamento" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtAgrupamento"
                                                                                                        Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="txtPolaridade" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtPolaridade"
                                                                                                        Width="100%">
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
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 25%; margin-left: 80px; padding-right: 10px;">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Periodicidade:" ID="ASPxLabel5"></dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px; width: 25%;">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel16" runat="server"
                                                                                                        Text="Fonte:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                    <td style="height: 10px">
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                            Text="Responsável:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 10px;">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPeriodicidade" ClientEnabled="False" ID="txtPeriodicidade">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px;">
                                                                                                    <dxe:ASPxTextBox ID="txtFonte" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtFonte"
                                                                                                        Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td style="height: 10px">
                                                                                                    <dxe:ASPxTextBox ID="txtResponsavel" runat="server" ClientEnabled="False"
                                                                                                        ClientInstanceName="txtResponsavel"
                                                                                                        Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabResultados" Text="Resultados">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 235px" id="tdPeriodo">
                                                                                                    <dxrp:ASPxRoundPanel runat="server" HeaderText="Per&#237;odo" View="GroupBox" Width="225px" ID="ASPxRoundPanel1">
                                                                                                        <ContentPaddings Padding="1px"></ContentPaddings>
                                                                                                        <PanelCollection>
                                                                                                            <dxp:PanelContent runat="server">
                                                                                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackPeriodos" Width="211px" ID="pnCallbackPeriodos" OnCallback="pnCallbackPeriodos_Callback">
                                                                                                                    <ClientSideEvents EndCallback="function(s, e) {
	gvResultados.PerformCallback();
}"></ClientSideEvents>
                                                                                                                    <PanelCollection>
                                                                                                                        <dxp:PanelContent runat="server">
                                                                                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 110px">
                                                                                                                                            <dxe:ASPxLabel runat="server" Text="In&#237;cio:" ID="ASPxLabel1"></dxe:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxLabel runat="server" Text="T&#233;rmino:" ID="ASPxLabel9"></dxe:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 110px">
                                                                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" AllowUserInput="False" Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtDe" ID="txtDe">
                                                                                                                                                <CalendarProperties ShowClearButton="False" ShowTodayButton="False" HighlightToday="False"></CalendarProperties>
                                                                                                                                                <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Inicio != s.GetText().toString())
	{				
		var dataAtual = new Date();
		var dataDe = new Date(s.GetValue());
		var dataAte = new Date(txtFim.GetValue());
		var qtdDias = parseInt(gvDados.cp_dias);
		txtFim.SetMinDate(s.GetValue());

		if(dataDe &gt; dataAte)
			txtFim.SetValue(dataDe.getDate() + qtdDias);

		dataDe.setDate(dataDe.getDate() + qtdDias);		
	
		if(dataDe &lt; dataAte)
			txtFim.SetValue(dataDe);		

		gvDados.cp_Inicio = s.GetValue();
		
		

		gvResultados.PerformCallback();
	}
}"></ClientSideEvents>
                                                                                                                                            </dxe:ASPxDateEdit>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" AllowUserInput="False" Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtFim" ID="txtFim">
                                                                                                                                                <CalendarProperties ShowClearButton="False" ShowTodayButton="False" HighlightToday="False"></CalendarProperties>
                                                                                                                                                <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Termino != s.GetText().toString())
	{
		gvDados.cp_Termino = s.GetValue();
		gvResultados.PerformCallback();
	}
}"></ClientSideEvents>
                                                                                                                                            </dxe:ASPxDateEdit>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                        </dxp:PanelContent>
                                                                                                                    </PanelCollection>
                                                                                                                </dxcp:ASPxCallbackPanel>
                                                                                                            </dxp:PanelContent>
                                                                                                        </PanelCollection>
                                                                                                    </dxrp:ASPxRoundPanel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel8"></dxe:ASPxLabel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxTextBox runat="server" Width="99%" ClientInstanceName="txtIndicadorDado" ClientEnabled="False" ID="txtIndicadorDado">
                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                                    </dxe:ASPxTextBox>
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
                                                                            <tr>
                                                                                <td style="height: 5px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                                                                                        ClientInstanceName="gvResultados" KeyFieldName="_CodigoMeta"
                                                                                        OnCellEditorInitialize="gvResultados_CellEditorInitialize" OnHtmlDataCellPrepared="gvResultados_HtmlDataCellPrepared"
                                                                                        OnRowUpdating="gvResultados_RowUpdating"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="35px" ShowEditButton="true">
                                                                                            </dxwgv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" ReadOnly="True"
                                                                                                VisibleIndex="1">
                                                                                                <PropertiesTextEdit>
                                                                                                    <ReadOnlyStyle BackColor="Transparent">
                                                                                                    </ReadOnlyStyle>
                                                                                                </PropertiesTextEdit>
                                                                                                <EditFormSettings Visible="False" />
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Resultado" FieldName="Valor" VisibleIndex="2"
                                                                                                Width="210px">
                                                                                                <PropertiesTextEdit Width="150px">
                                                                                                </PropertiesTextEdit>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="Editavel" Visible="False" VisibleIndex="3">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="_Mes" Visible="False" VisibleIndex="4">
                                                                                                <EditFormSettings Visible="True" />
                                                                                                <EditFormSettings Visible="True"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="_Ano" Visible="False" VisibleIndex="5">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                                                                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
                                                                                        <SettingsPager Mode="ShowAllRecords">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="Inline" />
                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="100" />
                                                                                        <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                                                                            EmptyHeaders="Resultados" />
                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                                                                            VerticalScrollableHeight="180"></Settings>
                                                                                        <SettingsText EmptyHeaders="Resultados" EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."></SettingsText>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                    </TabPages>
                                                </dxtc:ASPxPageControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDados.Hide();
}"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                    </dxpc:ASPxPopupControl>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxpc:ASPxPopupControl ID="pcMensagemVarios" runat="server" ClientInstanceName="pcMensagemVarios"
                        HeaderText="Mensagem do Sistema!" Modal="True"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                        Width="440px">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%;" align="center">
                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemVarios" ID="lblMensagemVarios"></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAceitarMensagem" Text="Aceitar" Width="90px" ID="btnAceitarMensagem">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcMensagemVarios.Hide();
}"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                    </dxpc:ASPxPopupControl>
                </td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
