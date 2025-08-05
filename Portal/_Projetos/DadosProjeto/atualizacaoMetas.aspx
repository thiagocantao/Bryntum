<%@ Page Language="C#" AutoEventWireup="true" CodeFile="atualizacaoMetas.aspx.cs"
    Inherits="atualizacaoMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Atualização das Metas</title>
    <script type="text/javascript" language="javascript">
        function excluiMeta() {
            gvDados.PerformCallback('X');
        }
    </script>
    <style type="text/css">
        .style3 {
            height: 10px;
        }

        .style4 {
            width: 100%;
        }

        .style9 {
            width: 100px;
        }

        .style10 {
            width: 170px;
        }

        .style11 {
            width: 120px;
        }

        .style12 {
            height: 15px;
        }

        .style13 {
            width: 175px;
        }

        .style14 {
            width: 200px;
        }

        .style15 {
            width: 10px;
            height: 10px;
        }
    </style>
</head>
<body class="body" enableviewstate="false">
    <form id="form1" runat="server" enableviewstate="false">
        <table>
            <tr>
                <td class="style15"></td>
                <td class="style3"></td>
                <td class="style15"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <div id="divGrid" style="visibility: hidden">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoMetaOperacional"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados" OnCustomCallback="gvDados_CustomCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"
                                            CustomButtonClick="function(s, e) {
	if(e.buttonID == 'imgEditar')
		OnClick_CustomEditarGvDado(s, e);
	else if(e.buttonID == 'imgExcluir')
	{	
		var func = function(existeDependencia){
			if(existeDependencia == 'S')
				window.top.mostraMensagem(traducao.atualizacaoMetas_existem_metas_desdobradas_e_ou_resultados_associados___meta_em_quest_o__a_exclus_o_deste_registro_ocasionar__em_perda_do_hist_rico_associado__confirma_a_exclus_o_, 'confirmacao', true, true, excluiMeta);
			else
				window.top.mostraMensagem(traducao.atualizacaoMetas_deseja_excluir_a_meta_do_projeto_, 'confirmacao', true, true, excluiMeta);
};
		 
		s.GetRowValues(e.visibleIndex, 'ExisteDependencia', func);
	}
}"
                                            EndCallback="function(s, e) {
	
	if(s.cp_AtualizaCampos == 'S')
		OnClick_CustomEditarGvDado(s, e);
	else if(s.cp_Msg != '')
    {
        window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null, 3000);                                
    }
         

	ddlIndicador.PerformCallback();
}"
                                            Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="75px" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="imgEditar" Text="Registrar Meta Para o Indicador">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="imgExcluir" Text="Excluir Indicador do Projeto">
                                                        <Image ToolTip="Excluir Indicador do Projeto" Url="~/imagens/botoes/excluirReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="center">
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
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Width="400px" Caption="Indicador"
                                                VisibleIndex="1">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False" VisibleIndex="3">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" Visible="False" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Meta" Name="Meta" Caption="Meta" VisibleIndex="5">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="MetaNumerica" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="10">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="EditaPeriodicidade" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoPeriodicidade" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Tipo de Indicador" FieldName="DescTipoIndicador"
                                                GroupIndex="0" Name="DescTipoIndicador" ShowInCustomizationForm="True" SortIndex="0"
                                                SortOrder="Ascending" VisibleIndex="6">
                                                <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains"
                                                    AllowDragDrop="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Fonte" FieldName="FonteIndicador" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="9">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="UsuarioAtualizacao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="7">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavelAtualizacao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="8">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataDateColumn Caption="Início (Vigência)" FieldName="DataInicioValidadeMeta"
                                                ShowInCustomizationForm="True" VisibleIndex="13" Width="130px">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                                <Settings ShowFilterRowMenu="True" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxtv:GridViewDataDateColumn>
                                            <dxtv:GridViewDataDateColumn Caption="Término (Vigência)" FieldName="DataTerminoValidadeMeta"
                                                ShowInCustomizationForm="True" VisibleIndex="14" Width="130px">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                                <Settings ShowFilterRowMenu="True" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxtv:GridViewDataDateColumn>
                                            <dxtv:GridViewDataComboBoxColumn Caption="Acompanha Meta?" FieldName="IndicaAcompanhaMetaVigencia"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="15" Width="120px">
                                                <PropertiesComboBox EnableFocusedStyle="False">
                                                    <Items>
                                                        <dxtv:ListEditItem Text="Sim" Value="S" />
                                                        <dxtv:ListEditItem Text="Não" Value="N" />
                                                        <dxtv:ListEditItem Text="Todos" Value="" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dxtv:GridViewDataComboBoxColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowGroupPanel="True"
                                            ShowHeaderFilterBlankItems="False"></Settings>

                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                        </SettingsCommandButton>

                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    </dxwgv:ASPxGridView>
                                </div>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="false"
                                    ID="pcDados" Width="900px">
                                    <ClientSideEvents Closing="function(s, e) {	
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}"
                                        Shown="function(s, e) {	
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
		if(tipoEdicao != 'I')
			OnGridFocusedRowChanged(gvDados, true); 
}"></ClientSideEvents>
                                    <ContentStyle>
                                        <Paddings PaddingBottom="4px" PaddingTop="4px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <div style="height: auto; overflow-y: auto;">
                                                <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabEdicao"
                                                                    ID="tabEdicao" Width="100%">
                                                                    <TabPages>
                                                                        <dxtc:TabPage Name="tbDados" Text="Meta">
                                                                            <ContentCollection>
                                                                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                                                                    <div id="divTabInfoMeta" style="min-height:200px">
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server"
                                                                                                                                    Text="Indicador:">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>


                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicador"
                                                                                                                                    Width="100%">
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxe:ASPxTextBox>
                                                                                                                                <dxe:ASPxComboBox ID="ddlIndicador" runat="server" ClientInstanceName="ddlIndicador"
                                                                                                                                    IncrementalFilteringMode="Contains" TextField="NomeIndicador"
                                                                                                                                    ValueField="CodigoIndicador" Width="100%">
                                                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	callbackIndicador.PerformCallback('I');
}"
                                                                                                                                        TextChanged="function(s, e) {
	callbackIndicador.PerformCallback('I');
}" />
                                                                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	callbackIndicador.PerformCallback(&#39;I&#39;);
}"
                                                                                                                                        TextChanged="function(s, e) {
	callbackIndicador.PerformCallback(&#39;I&#39;);
}"></ClientSideEvents>
                                                                                                                                </dxe:ASPxComboBox>
                                                                                                                            </td>


                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxtv:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                                        Text="Descrição da Meta:">
                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <dxe:ASPxMemo ID="txtMeta" runat="server" ClientInstanceName="txtMeta"
                                                                                                                        Rows="2" Width="100%">
                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                        </DisabledStyle>
                                                                                                                    </dxe:ASPxMemo>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <table width="100%">
                                                                                                                        <tr>
                                                                                                                            <td style="width: 125px">
                                                                                                                                <dxtv:ASPxLabel ID="lblInicioVigencia" runat="server"
                                                                                                                                    Text="Início Vigência:">
                                                                                                                                </dxtv:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td style="width: 125px">
                                                                                                                                <dxtv:ASPxLabel ID="lblInicioVigencia0" runat="server"
                                                                                                                                    Text="Término Vigência:">
                                                                                                                                </dxtv:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td>&nbsp;
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="width: 125px; padding-right: 10px">
                                                                                                                                <dxtv:ASPxDateEdit ID="ddlInicioVigencia" runat="server" ClientInstanceName="ddlInicioVigencia"
                                                                                                                                    DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}"></ClientSideEvents>
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxtv:ASPxDateEdit>
                                                                                                                            </td>
                                                                                                                            <td style="width: 125px; padding-right: 10px">
                                                                                                                                <dxtv:ASPxDateEdit ID="ddlTerminoVigencia" runat="server" ClientInstanceName="ddlTerminoVigencia"
                                                                                                                                    DisplayFormatString="{0:dd/MM/yyyy}" Width="100%">
                                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	verificaVigencia();
}"></ClientSideEvents>
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxtv:ASPxDateEdit>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <dxtv:ASPxCheckBox ID="cbVigencia" runat="server" CheckState="Unchecked" ClientInstanceName="cbVigencia"
                                                                                                                                    Text="Acompanhar as metas do indicador somente no período de vigência"
                                                                                                                                    ValueChecked="S" ValueType="System.String" ValueUnchecked="N" ClientEnabled="False">
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxtv:ASPxCheckBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="right">
                                                                                                                    <table cellpadding="0" cellspacing="0" class="style4">
                                                                                                                        <tr>
                                                                                                                            <td align="left" class="style11" style="padding-right: 10px">
                                                                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                                                                    Text="Meta Numérica:">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td align="left" class="style13" style="padding-right: 10px">
                                                                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                                                                    Text="Periodicidade:">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td align="left">
                                                                                                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                                                                    Text="Fonte:">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>
                                                                                                                            <td align="left" class="style14">
                                                                                                                                <dxe:ASPxLabel ID="lblResponsavelIndicador0" runat="server"
                                                                                                                                    Text="Responsável pela Atualização:">
                                                                                                                                </dxe:ASPxLabel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td align="left" class="style11" style="padding-right: 10px">
                                                                                                                                <dxe:ASPxTextBox ID="txtValorMeta" runat="server" ClientEnabled="False" ClientInstanceName="txtValorMeta"
                                                                                                                                    DisplayFormatString="{0:n2}" HorizontalAlign="Right"
                                                                                                                                    Width="100%">
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
                                                                                                                            <td id="tdPeriodicidade" align="left" class="style13" style="padding-right: 10px">
                                                                                                                                <dxe:ASPxComboBox ID="ddlPeriodicidadeCalculo" runat="server" ClientInstanceName="ddlPeriodicidadeCalculo"
                                                                                                                                    ValueType="System.Int32" Width="100%">
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxe:ASPxComboBox>
                                                                                                                            </td>
                                                                                                                            <td id="tdFonte2" align="left" style="padding-right: 10px">
                                                                                                                                <dxe:ASPxTextBox ID="txtFonte" runat="server" ClientInstanceName="txtFonte"
                                                                                                                                    MaxLength="59" Width="100%">
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxe:ASPxTextBox>
                                                                                                                            </td>
                                                                                                                            <td id="tdFonte" align="left" class="style14">
                                                                                                                                <dxe:ASPxComboBox ID="ddlResponsavelResultado" runat="server" ClientInstanceName="ddlResponsavelResultado"
                                                                                                                                    EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                                                    OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition" ValueType="System.Int32" Width="100%">
                                                                                                                                    <Columns>
                                                                                                                                        <dxe:ListBoxColumn Caption="Nome" Width="300px" />
                                                                                                                                        <dxe:ListBoxColumn Caption="Email" Width="200px" />
                                                                                                                                    </Columns>
                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                    </DisabledStyle>
                                                                                                                                </dxe:ASPxComboBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <dxrp:ASPxRoundPanel ID="rpEntradaSistema" runat="server" ClientInstanceName="rpEntradaSistema"
                                                                                                            HeaderText="Indicador Associado" ClientVisible="false" Width="100%">
                                                                                                            <HeaderStyle Font-Bold="False">
                                                                                                                <BorderBottom BorderStyle="None" />
                                                                                                                <BorderBottom BorderStyle="None"></BorderBottom>
                                                                                                            </HeaderStyle>
                                                                                                            <PanelCollection>
                                                                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                                </dxp:PanelContent>
                                                                                                            </PanelCollection>
                                                                                                        </dxrp:ASPxRoundPanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px;">
                                                                                                        <table width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td class="style11">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                                                            Text="Unidade de Medida:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td class="style9">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel13" runat="server"
                                                                                                                            Text="Agrupamento:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td class="style10">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                                                            Text="Polaridade:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                                            Text="Responsável pelo Indicador:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="style11" style="padding-right: 10px;">
                                                                                                                        <dxtv:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientEnabled="False" ClientInstanceName="txtUnidadeMedida"
                                                                                                                            Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td class="style9" style="padding-right: 10px;">
                                                                                                                        <dxtv:ASPxTextBox ID="txtAgrupamento" runat="server" ClientEnabled="False" ClientInstanceName="txtAgrupamento"
                                                                                                                            Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td class="style10" style="padding-right: 10px;">
                                                                                                                        <dxtv:ASPxTextBox ID="txtPolaridade" runat="server" ClientEnabled="False" ClientInstanceName="txtPolaridade"
                                                                                                                            Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxtv:ASPxTextBox ID="txtResponsavel" runat="server" ClientEnabled="False" ClientInstanceName="txtResponsavel"
                                                                                                                            Width="100%">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </dxw:ContentControl>
                                                                            </ContentCollection>
                                                                        </dxtc:TabPage>
                                                                        <dxtc:TabPage Name="tbMetas" Text="Desdobramento das Metas">
                                                                            <ContentCollection>
                                                                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                                                                    <div id="divTabDesdobrMeta" style="min-height:200px">
                                                                                        <iframe id="frmMetas" frameborder="0" height="293" scrolling="no" src="" style="width: 100%"></iframe>
                                                                                    </div>
                                                                                </dxw:ContentControl>
                                                                            </ContentCollection>
                                                                        </dxtc:TabPage>
                                                                    </TabPages>
                                                                    <ClientSideEvents ActiveTabChanged="function(s, e) {
	document.getElementById('frmMetas').src = urlMetas;
                if(e.tab.name == 'tbMetas')
                {
                            btnSalvar.SetVisible(false);
               }
               else
              {
                           btnSalvar.SetVisible(true);
              }
}"></ClientSideEvents>
                                                                    <ContentStyle>
                                                                        <Paddings PaddingLeft="5px" PaddingRight="5px" PaddingBottom="4px" PaddingTop="4px"></Paddings>
                                                                    </ContentStyle>
                                                                </dxtc:ASPxPageControl>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <table align="right">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                            Text="Salvar" Width="90px">
                                                            <ClientSideEvents Click="function(s, e) {
	if(validaCampos())
		gvDados.PerformCallback(tipoEdicao);
}" />
                                                            <Paddings Padding="0px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if(validaCampos())
		gvDados.PerformCallback(tipoEdicao);
}"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                            Text="Fechar" Width="90px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	OnClick_ImagemCancelar(s,e);
    pcDados.Hide();
	gvDados.PerformCallback();
}" />
                                                            <Paddings Padding="0px" />
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	OnClick_ImagemCancelar(s,e);
    pcDados.Hide();
	gvDados.PerformCallback();
}"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                        Landscape="True" ID="ASPxGridViewExporter1" ExportEmptyDetailGrid="True" PreserveGroupRowStates="False"
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
                    <dxcb:ASPxCallback ID="callbackIndicador" runat="server" ClientInstanceName="callbackIndicador"
                        OnCallback="callbackIndicador_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {	
		preencheCamposIndicador(s);
}" />
                    </dxcb:ASPxCallback>
                    <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
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
                            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%;" align="center">
                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblMensagemVarios"
                                                    ID="lblMensagemVarios" EncodeHtml="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAceitarMensagem"
                                                    Text="Fechar" Width="90px" ID="btnAceitarMensagem">
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
    <script type="text/javascript">
        var popup_detalheMeta_divHeight = Math.max(0, document.documentElement.clientHeight) - 184;
        document.getElementById("divTabInfoMeta").style.maxHeight = popup_detalheMeta_divHeight + "px";
        document.getElementById("divTabInfoMeta").style.overflow = "auto";
        document.getElementById("divTabDesdobrMeta").style.maxHeight = popup_detalheMeta_divHeight + "px";
    </script>
</body>
</html>
