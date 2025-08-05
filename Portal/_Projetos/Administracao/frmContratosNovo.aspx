<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContratosNovo.aspx.cs"
    Inherits="_Projetos_DadosProjeto_frmContratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Contratos</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>

</head>
<body class="body" style="margin: 0px">
    <form id="form1" runat="server">
        <div style="padding: 5px;">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                    <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" OnRenderBrick="gvExporter_RenderBrick">
                                        <Styles>
                                            <Default>
                                            </Default>
                                            <Header Font-Bold="True">
                                            </Header>
                                            <Cell>
                                            </Cell>
                                            <GroupFooter Font-Bold="True">
                                            </GroupFooter>
                                            <GroupRow Font-Bold="True">
                                            </GroupRow>
                                            <Title Font-Bold="True"></Title>
                                        </Styles>
                                    </dxwgv:ASPxGridViewExporter>
                                    <div id="divGrid" style="visibility: visible">
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato"
                                            AutoGenerateColumns="False"
                                            ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" EnableViewState="False"
                                            OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCustomErrorText="gvDados_CustomErrorText" OnCustomCallback="gvDados_CustomCallback" Width="100%">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(window.pcDados &amp;&amp; pcDados.GetVisible())
		OnGridFocusedRowChanged(s);
}"
                                                CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
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
	 else if(e.buttonID == 'btnPermissoesCustom')	{OnGridFocusedRowChangedPopup(gvDados);}
}"
                                                Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>

                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                            </SettingsCommandButton>

                                            <SettingsPopup>
                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>

                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="N&#186; Parcela" ToolTip="N&#186; Parcela"
                                                    VisibleIndex="0" Width="150px">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                            <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissoesCustom" Text="Visualizar Interessados">
                                                            <Image Url="~/imagens/Perfis/Perfil_Permissoes.png">
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
                                                <dxwgv:GridViewDataTextColumn Caption="Número do Contrato" FieldName="NumeroContrato"
                                                    Name="NumeroContrato" VisibleIndex="1" Width="280px">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Fornecedor/Cliente" FieldName="Fornecedor"
                                                    Name="Fornecedor" VisibleIndex="3" Width="200px">
                                                    <Settings AutoFilterCondition="Contains" AllowHeaderFilter="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Unidade Gestora" FieldName="NomeUnidadeNegocio"
                                                    Name="NomeUnidadeNegocio" VisibleIndex="11" Width="150px" Visible="False">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" FieldName="NomeUsuario"
                                                    Name="NomeUsuario" VisibleIndex="13" Width="150px" Visible="False">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Modalidade Aquisi&#231;&#227;o" FieldName="DescricaoTipoAquisicao"
                                                    Name="DescricaoTipoAquisicao" VisibleIndex="14" Width="150px" Visible="False">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataDateColumn Caption="Inicio Vig&#234;ncia" FieldName="DataInicio"
                                                    Name="DataInicio" VisibleIndex="15" Width="130px" Visible="False">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>">
                                                    </PropertiesDateEdit>
                                                    <Settings AutoFilterCondition="GreaterOrEqual" ShowFilterRowMenu="True" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataDateColumn Caption="Término da Vigência" FieldName="DataTermino"
                                                    Name="DataTermino" VisibleIndex="10" Width="200px">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>">
                                                    </PropertiesDateEdit>
                                                    <Settings AutoFilterCondition="LessOrEqual" ShowFilterRowMenu="True" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Objeto" FieldName="DescricaoObjetoContrato"
                                                    Name="DescricaoObjetoContrato" VisibleIndex="5" Width="260px">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Situa&#231;&#227;o" FieldName="SituacaoContrato"
                                                    VisibleIndex="23" Width="75px" Visible="False">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Situa&#231;&#227;o" FieldName="StatusContrato"
                                                    Name="StatusContrato" Visible="False" VisibleIndex="12" Width="75px">
                                                    <DataItemTemplate>
                                                        <%# (Eval("StatusContrato").ToString() == "A") ? "Ativo" : "Inativo"%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoContrato" FieldName="CodigoContrato"
                                                    Name="CodigoContrato" Visible="False" VisibleIndex="17">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoUsuarioResponsavel" FieldName="CodigoUsuarioResponsavel"
                                                    Name="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="22">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoTipoAquisicao" FieldName="CodigoTipoAquisicao"
                                                    Name="CodigoTipoAquisicao" Visible="False" VisibleIndex="18">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoUnidadeNegocio" FieldName="CodigoUnidadeNegocio"
                                                    Name="CodigoUnidadeNegocio" Visible="False" VisibleIndex="19">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Observacao" FieldName="Observacao" Name="Observacao"
                                                    Visible="False" VisibleIndex="20">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="NumeroContratoSAP" FieldName="NumeroContratoSAP"
                                                    Name="NumeroContratoSAP" Visible="False" VisibleIndex="21">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" Name="CodigoProjeto"
                                                    Visible="False" VisibleIndex="16">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Permissoes" Name="Permissoes" Visible="False"
                                                    VisibleIndex="27">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoFonteRecursosFinanceiros" Visible="False"
                                                    VisibleIndex="28">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoContrato" Visible="False" VisibleIndex="29">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="CodigoPessoaContratada" FieldName="CodigoPessoaContratada"
                                                    ShowInCustomizationForm="True" VisibleIndex="24" Visible="False">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="TipoPessoaContrato" FieldName="TipoPessoa"
                                                    ShowInCustomizationForm="True" VisibleIndex="25" Visible="False">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="TemParcelas" FieldName="TemParcelas" ShowInCustomizationForm="True"
                                                    VisibleIndex="26" Visible="False">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataComboBoxColumn Caption="Revisão Prévia" FieldName="IndicaRevisaoPrevia" ShowInCustomizationForm="True" VisibleIndex="30" Visible="False">
                                                    <PropertiesComboBox>
                                                        <Items>
                                                            <dxtv:ListEditItem Text="Sim" Value="S" />
                                                            <dxtv:ListEditItem Text="Não" Value="N" />
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxtv:GridViewDataComboBoxColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Tipo" FieldName="DescricaoTipoContrato" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="4" Width="300px">
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Status" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="DescricaoStatusComplementarContrato">
                                                    <Settings AllowHeaderFilter="True" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor total" FieldName="ValorContrato" Name="ValorContrato" ShowInCustomizationForm="True" VisibleIndex="6" Width="150px">
                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                        <SpinButtons ClientVisible="False">
                                                        </SpinButtons>
                                                    </PropertiesSpinEdit>
                                                    <Settings ShowFilterRowMenu="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxtv:GridViewDataSpinEditColumn>
                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor pago" FieldName="ValorPago" Width="150px" ShowInCustomizationForm="True" VisibleIndex="8">
                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                        <SpinButtons ClientVisible="False">
                                                        </SpinButtons>
                                                    </PropertiesSpinEdit>
                                                    <Settings ShowFilterRowMenu="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxtv:GridViewDataSpinEditColumn>
                                                <dxtv:GridViewDataSpinEditColumn Caption="Saldo" ShowInCustomizationForm="True" VisibleIndex="9" Width="150px" FieldName="Saldo">
                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                        <SpinButtons ClientVisible="False">
                                                        </SpinButtons>
                                                    </PropertiesSpinEdit>
                                                    <Settings ShowFilterRowMenu="True" />
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </dxtv:GridViewDataSpinEditColumn>
                                                <dxtv:GridViewDataTextColumn Caption="CodigoStatusComplementarContrato" FieldName="CodigoStatusComplementarContrato" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="ValorContratoOriginal" FieldName="ValorContratoOriginal" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True"
                                                HorizontalScrollBarMode="Visible" ShowGroupedColumns="True" VerticalScrollableHeight="50" VerticalScrollBarMode="Visible"></Settings>
                                            <SettingsText></SettingsText>
                                            <Styles>
                                                <CommandColumnItem>
                                                    <Paddings PaddingLeft="2px" PaddingRight="2px" />
                                                </CommandColumnItem>
                                            </Styles>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ShowCloseButton="False" PopupVerticalOffset="-20" ID="pcDados" AllowDragging="True" ShowHeader="False" Width="800px">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                                <dxtc:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl"
                                                    Width="100%">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="tabP" Text="Principal">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div id="divAbaPrincipal" style="overflow-y: scroll;">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxe:ASPxLabel ID="lblNumeroContrato" runat="server" ClientInstanceName="lblNumeroContrato"
                                                                                                            Text="Número do Contrato:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxe:ASPxLabel ID="lblTipoContrato" runat="server" ClientInstanceName="lblTipoContrato"
                                                                                                            Text="Tipo de Contrato:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 20%; display: none">
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                                            Text="Situa&#231;&#227;o:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 20%;">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel15" runat="server" Text="Status:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" ClientInstanceName="lblNumeroContrato" Text="Início de Vigência:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblNumeroContrato" Text="Término de Vigência:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-right: 5px; width: 20%">
                                                                                                        <dxe:ASPxTextBox ID="txtNumeroContrato" runat="server" ClientInstanceName="txtNumeroContrato"
                                                                                                            MaxLength="50" TabIndex="1" Width="100%">
                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px; width: 20%">
                                                                                                        <dxe:ASPxComboBox ID="ddlTipoContrato" runat="server" ClientInstanceName="ddlTipoContrato"
                                                                                                            TabIndex="2" TextField="DescricaoTipoContrato"
                                                                                                            ValueField="CodigoTipoContrato" ValueType="System.Int32" Width="100%">
                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Ativo" Value="0" />
                                                                                                                <dxe:ListEditItem Text="N&#227;o Ativo" Value="0" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px; display: none; width: 20%">
                                                                                                        <dxe:ASPxComboBox ID="ddlSituacao" runat="server" ClientInstanceName="ddlSituacao"
                                                                                                            TabIndex="3" ValueType="System.String" Width="100%">
                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Ativo" Value="A" />
                                                                                                                <dxe:ListEditItem Text="N&#227;o Ativo" Value="I" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px; width: 20%">
                                                                                                        <dxtv:ASPxComboBox ID="ddlStatusComplementarContrato" runat="server" ClientInstanceName="ddlStatusComplementarContrato" TabIndex="4" Width="100%">
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px; width: 20%">
                                                                                                        <dxtv:ASPxDateEdit ID="ddlInicioDeVigencia" runat="server" ClientInstanceName="ddlInicioDeVigencia" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EncodeHtml="False" PopupVerticalAlign="TopSides" TabIndex="5" UseMaskBehavior="True" Width="100%">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                            </CalendarProperties>
                                                                                                            <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}"
                                                                                                                ValueChanged="function(s, e) {
	
}" />
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxDateEdit>
                                                                                                    </td>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxtv:ASPxDateEdit ID="ddlTerminoDeVigencia" runat="server" ClientInstanceName="ddlTerminoDeVigencia" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EncodeHtml="False" PopupVerticalAlign="TopSides" TabIndex="6" UseMaskBehavior="True" Width="100%">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                            </CalendarProperties>
                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxDateEdit>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 40%; padding-right: 5px;">
                                                                                                        <dxtv:ASPxLabel ID="lblTipoEmitente" runat="server" ClientInstanceName="lblTipoEmitente" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Emitente:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 60%">
                                                                                                        <dxe:ASPxLabel ID="lblClienteFor" ClientInstanceName="lblClienteFor" runat="server"
                                                                                                            Text="Emitente:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 40%; padding-right: 5px;">
                                                                                                        <dxe:ASPxComboBox runat="server" ClientInstanceName="rbClienteFor"
                                                                                                            ID="rbClienteFor" Width="100%" SelectedIndex="1" TabIndex="7">
                                                                                                            <Paddings Padding="0px"></Paddings>
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    alteraLabelCliFor();
    ddlRazaoSocial.PerformCallback();
}" />
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Cliente" Value="C"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="Fornecedor" Value="F" Selected="True"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="Concedente de Convênio" Value="D"></dxe:ListEditItem>
                                                                                                            </Items>
                                                                                                            <DisabledStyle ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td id="td10" style="width: 60%">
                                                                                                        <dxe:ASPxComboBox ID="ddlRazaoSocial" runat="server" ClientInstanceName="ddlRazaoSocial"
                                                                                                            Height="21px" IncrementalFilteringMode="Contains"
                                                                                                            TextFormatString="{0}" Width="100%" TabIndex="8">
                                                                                                            <Columns>
                                                                                                                <dxe:ListBoxColumn Caption="Razão Social" FieldName="NomePessoa" Name="NomePessoa"
                                                                                                                    Width="300px" />
                                                                                                                <dxe:ListBoxColumn Caption="Nome Fantasia" FieldName="NomeFantasia" Name="NomeFantasia"
                                                                                                                    Width="260px" />
                                                                                                            </Columns>
                                                                                                            <ItemStyle Wrap="True" />
                                                                                                            <ListBoxStyle Wrap="True">
                                                                                                            </ListBoxStyle>
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
                                                                                    <td style="padding-top: 5px">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblNumeroContrato"
                                                                                            Text="Objeto:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="mmObjeto" runat="server" ClientInstanceName="mmObjeto"
                                                                                            Rows="3" TabIndex="9" Width="100%">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                        <dxe:ASPxLabel ID="lbl_mmObjeto" runat="server" ClientInstanceName="lbl_mmObjeto"
                                                                                            Font-Bold="True" ForeColor="#999999">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px">
                                                                                        <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblNumeroContrato" Text="Unidade Gestora:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" ClientInstanceName="lblNumeroContrato" Text="Responsável pela Gestão:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 5px">
                                                                                                    <dxtv:ASPxComboBox ID="ddlUnidadeGestora" runat="server" ClientInstanceName="ddlUnidadeGestora" TabIndex="10" TextField="NomeUnidadeNegocio" ValueField="CodigoUnidadeNegocio" Width="100%">
                                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel" EnableCallbackMode="True" NullValueItemDisplayText="{0}" OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition" TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" Width="100%" TabIndex="11">
                                                                                                        <Columns>
                                                                                                            <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px">
                                                                                                            </dxtv:ListBoxColumn>
                                                                                                            <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px">
                                                                                                            </dxtv:ListBoxColumn>
                                                                                                        </Columns>
                                                                                                        <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorText="*">
                                                                                                            <RequiredField IsRequired="True" />
                                                                                                        </ValidationSettings>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 25%">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Modalidade de Aquisição:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 25%">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" ClientInstanceName="lblNumeroContrato" Text="Fonte Pagadora:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="width: 25%">&nbsp;</td>
                                                                                                                    <td style="width: 25%">
                                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lblNumeroContrato" Text="Projeto:">
                                                                                                                        </dxtv:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 25%; padding-right: 5px;">
                                                                                                                        <dxtv:ASPxComboBox ID="ddlModalidadAquisicao" runat="server" ClientInstanceName="ddlModalidadAquisicao" TabIndex="12" TextField="DescricaoTipoAquisicao" ValueField="CodigoTipoAquisicao" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 25%; padding-right: 5px;">
                                                                                                                        <dxtv:ASPxComboBox ID="ddlFontePagadora" runat="server" ClientInstanceName="ddlFontePagadora" TabIndex="13" TextField="NomeFonte" ValueField="CodigoFonteRecursosFinanceiros" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 25%">
                                                                                                                        <dxtv:ASPxCheckBox ID="cbIndicaRevisaoPrevia" runat="server" CheckState="Unchecked" ClientInstanceName="cbIndicaRevisaoPrevia" Text="Revisão Prévia?" ValueChecked="S" ValueGrayed="N" ValueType="System.String" ValueUnchecked="N" TabIndex="14">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxCheckBox>
                                                                                                                    </td>
                                                                                                                    <td id="td_ConteudoRevisaoPrevia" style="width: 25%">
                                                                                                                        <dxtv:ASPxComboBox ID="ddlProjetos" runat="server" ClientInstanceName="ddlProjetos" TabIndex="15" TextField="NomeProjeto" ValueField="CodigoProjeto" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxtv:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                            <tbody>
                                                                                                                <tr style="display: none">
                                                                                                                    <td style="width: 150px">
                                                                                                                        <dxe:ASPxLabel ID="lblNomeCIA" runat="server" ClientInstanceName="lblNomeCIA">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>&nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr style="display: none">
                                                                                                                    <td style="padding-right: 10px">
                                                                                                                        <dxe:ASPxTextBox ID="txtSAP" runat="server" ClientInstanceName="txtSAP"
                                                                                                                            MaxLength="50" TabIndex="16" Width="100%">
                                                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>

                                                                                                                    <td>&nbsp;</td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <div id="divSaldoContrato">
                                                                                                            <dx:ASPxLoadingPanel ID="lpSaldoContrato" runat="server" ClientInstanceName="lpSaldoContrato" ContainerElementID="divSaldoContrato" Modal="False" ><Image Width="20px" Height="20px" />
                                                                                                            </dx:ASPxLoadingPanel>
                                                                                                            <dxtv:ASPxCallbackPanel ID="pnSaldo" runat="server" ClientInstanceName="pnSaldo" OnCallback="pnSaldo_Callback" Width="100%"  >
                                                                                                                <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="Calculando..." />
                                                                                                                <ClientSideEvents BeginCallback="function(s,e){lpSaldoContrato.Show();
                                                                                                                                                        }" />
                                                                                                                <ClientSideEvents EndCallback="function(s, e) {
                                                                                                                                                cbAvisos.PerformCallback(s.cpAviso);
                                                                                                                                                lpSaldoContrato.Hide();
                                                                                                                                                }" />
                                                                                                                <PanelCollection>
                                                                                                                    <dxtv:PanelContent runat="server">
                                                                                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <dxtv:ASPxLabel ID="lblValorDoContrato" runat="server" ClientInstanceName="lblValorDoContrato" Text="Valor do Contrato:">
                                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                                </td>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <dxtv:ASPxLabel ID="lblValorComRepactacao" runat="server" ClientInstanceName="lblValorComRepactacao" Text="Valor com repactuação:">
                                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                                </td>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <dxtv:ASPxLabel ID="lblPagoAcumulado" runat="server" ClientInstanceName="lblPagoAcumulado" Text="Pago Acumulado:">
                                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                                </td>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <dxtv:ASPxLabel ID="lblPrevistoAcumulado" runat="server" ClientInstanceName="lblPrevistoAcumulado" Text="Previsto Acumulado:">
                                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                                </td>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <dxtv:ASPxLabel ID="lblSaldo" runat="server" ClientInstanceName="lblSaldo" Text="Saldo:">
                                                                                                                                    </dxtv:ASPxLabel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td style="padding-right: 5px; width: 20%;">
                                                                                                                                    <dxtv:ASPxSpinEdit ID="txtValorDoContrato" runat="server" ClientInstanceName="txtValorDoContrato" DisplayFormatString="c2" HorizontalAlign="Right" TabIndex="17" Width="100%" AllowMouseWheel="False" EnableClientSideAPI="True" Increment="0">
                                                                                                                                        <SpinButtons ClientVisible="False" Enabled="false" ShowIncrementButtons="False">
                                                                                                                                        </SpinButtons>

                                                                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                                                                                                            var valorSaldo = s.GetValue() -  spnPagoAcumulado.GetValue() - spnPrevistoAcumulado.GetValue();
                                                                                                                                            spnSaldo.SetValue(valorSaldo);
	
                                                                                                                                            }" />
                                                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </ReadOnlyStyle>
                                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                                                </td>
                                                                                                                                <td style="padding-right: 5px; width: 20%;">
                                                                                                                                    <dxtv:ASPxSpinEdit ID="spnValorComAditivo" runat="server" ClientInstanceName="spnValorComAditivo" DisplayFormatString="c2" Number="0" ReadOnly="True" Width="100%" TabIndex="18" HorizontalAlign="Right">
                                                                                                                                        <SpinButtons ClientVisible="False">
                                                                                                                                        </SpinButtons>
                                                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </ReadOnlyStyle>
                                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                                                </td>
                                                                                                                                <td style="padding-right: 5px; width: 20%;">
                                                                                                                                    <dxtv:ASPxSpinEdit ID="spnPagoAcumulado" runat="server" ClientInstanceName="spnPagoAcumulado" DisplayFormatString="c2" Number="0" Width="100%" ReadOnly="True" TabIndex="19" HorizontalAlign="Right">
                                                                                                                                        <SpinButtons ClientVisible="False">
                                                                                                                                        </SpinButtons>
                                                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </ReadOnlyStyle>
                                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                                                </td>
                                                                                                                                <td style="padding-right: 5px; width: 20%;">
                                                                                                                                    <dxtv:ASPxSpinEdit ID="spnPrevistoAcumulado" runat="server" ClientInstanceName="spnPrevistoAcumulado" DisplayFormatString="c2" Number="0" Width="100%" ReadOnly="True" TabIndex="20" HorizontalAlign="Right">
                                                                                                                                        <SpinButtons ClientVisible="False">
                                                                                                                                        </SpinButtons>
                                                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </ReadOnlyStyle>
                                                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </DisabledStyle>
                                                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                                                </td>
                                                                                                                                <td style="width: 20%">
                                                                                                                                    <table>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <dxtv:ASPxSpinEdit ID="spnSaldo" runat="server" ClientInstanceName="spnSaldo" DisplayFormatString="c2" Number="0" Width="100%" ReadOnly="True" TabIndex="21" HorizontalAlign="Right">
                                                                                                                                                    <SpinButtons ClientVisible="False">
                                                                                                                                                    </SpinButtons>
                                                                                                                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                                    </ReadOnlyStyle>
                                                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                                    </DisabledStyle>
                                                                                                                                                </dxtv:ASPxSpinEdit>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <dxe:ASPxImage runat="server" ImageAlign="TextTop"
                                                                                                                                                    ClientInstanceName="imgStatusSincronizacao" ID="imgStatusSincronizacao" ToolTip="Parcela originária de integração com sistemas externos. (Desatualizada)" EnableClientSideAPI="true">
                                                                                                                                                </dxe:ASPxImage>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </dxtv:PanelContent>
                                                                                                                </PanelCollection>
                                                                                                            </dxtv:ASPxCallbackPanel>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <dxtv:ASPxLabel ID="lblObservacoes" runat="server" ClientInstanceName="lblObservacoes" Text="Observações:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxMemo ID="mmObservacoes" runat="server" ClientInstanceName="mmObservacoes"
                                                                                                            Rows="3" TabIndex="22" Width="100%">
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxMemo>
                                                                                                        <dxe:ASPxLabel ID="lbl_mmObservacoes" runat="server" ClientInstanceName="lbl_mmObservacoes"
                                                                                                            Font-Bold="True" ForeColor="#999999">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        <dxcb:ASPxCallback ID="callbackResponsavel" runat="server" ClientInstanceName="callbackResponsavel">
                                                                                            <ClientSideEvents EndCallback="function(s, e) 
{
	//onEnd_CallbackResponsavel(s, e);
}" />
                                                                                        </dxcb:ASPxCallback>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtv:TabPage Name="tabItens" Text="Itens">
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <iframe id="frmItens" frameborder="0" height="276" scrolling="no" src="" width="800"></iframe>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                        <dxtc:TabPage Name="tabF" Text="Financeiro">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <iframe id="frmParcelas" frameborder="0" height="276" scrolling="no" src="" width="800"></iframe>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtv:TabPage Name="tabRepactuacoes" Text="Repactuações">
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <iframe id="frmAditivos" frameborder="0" height="276" scrolling="no" src="" width="800"></iframe>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                        <dxtv:TabPage Name="tabLinks" Text="Links">
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <iframe id="frmLinks" frameborder="0" height="276" scrolling="no" src="" width="800"></iframe>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                        <dxtc:TabPage Name="tabA" Text="Anexos">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <iframe id="frmAnexos" frameborder="0" height="276" scrolling="no" src="" width="800px"></iframe>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)	
}"
                                                        ActiveTabChanged="function(s, e) {
          if(e.tab.index == 0)
          {
            pnSaldo.PerformCallback(codigoContratoGlobal);
          }
          if(e.tab.index == 1)
          {
	   if(atualizarURLItens != 'N')
	  {
	              atualizarURLItens = 'N';
	             document.getElementById('frmItens').src = frmItensContrato;
	  }
         }
         if(e.tab.index == 2)
        {
	 if(atualizarURLParcelas != 'N')
	 {
	          atualizarURLParcelas = 'N';
	          document.getElementById('frmParcelas').src = frmParcelasContrato;
	 }
        }
        else if(e.tab.index == 3)
       {
                if(atualizarURLAditivos != 'N')
               {
                          atualizarURLAditivos = 'N';                             
                         document.getElementById('frmAditivos').src = frmAditivosContrato;
               }
      }

        else if(e.tab.index == 4)
       {
                if(atualizarURLLinks != 'N')
               {
                          atualizarURLLinks = 'N';                             
                         document.getElementById('frmLinks').src = frmLinksContrato;
               }
      }

     else if(e.tab.index == 5)
     {
               if(atualizarURLAnexos != 'N')
               {
	              atualizarURLAnexos = 'N';		    
	             document.getElementById('frmAnexos').src = frmAnexosContrato;
	}
      }

}" />
                                                    <ContentStyle>
                                                        <Paddings Padding="3px" />
                                                    </ContentStyle>
                                                </dxtc:ASPxPageControl>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" style="padding-right: 10px; padding-top: 5px">
                                                                <dxtv:ASPxCallbackPanel ID="cbAvisos" runat="server" ClientInstanceName="cbAvisos" OnCallback="cbAvisos_Callback" Width="100%">
                                                                    <PanelCollection>
                                                                        <dxtv:PanelContent runat="server">
                                                                            <dxtv:ASPxLabel ID="lblAvisos" runat="server" ClientInstanceName="lblAvisos" Font-Bold="True" ForeColor="Red" Width="100%" Wrap="True">
                                                                            </dxtv:ASPxLabel>
                                                                        </dxtv:PanelContent>
                                                                    </PanelCollection>
                                                                </dxtv:ASPxCallbackPanel>
                                                            </td>
                                                            <td align="right" style="padding-right: 10px; padding-top: 5px; width: 120px;">
                                                                <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" TabIndex="23" Text="Salvar" Width="95px">
                                                                    <ClientSideEvents Click="function(s, e) {
	                                                                    e.processOnServer = false;

                                                                        if ( validaCamposFormulario() != '')
                                                                        {
                                                                            window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
                                                                            return;
                                                                        }
                                                                            
                                                                        if (txtValorDoContrato.GetValue() == 0 && TipoOperacao == 'Incluir') {
                                                                            var funcObj = { funcaoClickOK: function () { 
                                                                                                                           window.top.lpAguardeMasterPage.Show();
                                                                                                                            if (SalvarCamposFormulario()) {
                                                                                                                                pcDados.Hide();
                                                                                                                                return true;
                                                                                                                            }
                                                                                                                        } 
                                                                                           }
                                                                            var funcObjNOK = { funcaoClickNOK: function () { return; } }
                                                                            window.top.mostraConfirmacao('Confirma inclusão de contrato com valor zero?', function () { funcObj['funcaoClickOK']() } , function () { funcObjNOK['funcaoClickNOK']() });
                                                                        }else{
                                                                            window.top.lpAguardeMasterPage.Show();
                                                                            if (SalvarCamposFormulario()) {
                                                                                pcDados.Hide();
                                                                                return true;
                                                                            }
	                                                                    }
                                                                    }" />
                                                                    <Paddings Padding="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                            <td align="right" style="width: 100px; padding-top: 5px">
                                                                <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                                                    CommandArgument="btnCancelar" TabIndex="24"
                                                                    Text="Fechar" Width="95px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
	{
       onClick_btnCancelar();
        refreshGridContratos();
		hfGeral.Set(&quot;TipoOperacaoParcelas&quot;, &quot;&quot;);
	}
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <ClientSideEvents CloseUp="function(s, e) {
    LimpaCamposFormulario();
	tabControl.SetActiveTabIndex(0);
}"
                                            PopUp="function(s, e) {
    var sWidth;
    var sHeight;
    sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    sHeight = Math.max(0, document.documentElement.clientHeight) - 40;
    s.SetSize(sWidth, sHeight);
    s.UpdatePosition();

    var sHeight1 = Math.max(0, document.documentElement.clientHeight) - 125;
    var sWidth1 = Math.max(0, document.documentElement.clientWidth) - 100;

    document.getElementById('frmParcelas').style.height = sHeight1 + 'px';
    document.getElementById('frmParcelas').style.width = sWidth1 + 'px';

    document.getElementById('divAbaPrincipal').style.height = sHeight1 + 'px';
    document.getElementById('divAbaPrincipal').style.width = sWidth1 + 'px';

    document.getElementById('frmAditivos').style.height = sHeight1 + 'px';
    document.getElementById('frmAditivos').style.width = sWidth1 + 'px';

    document.getElementById('frmAnexos').style.height = sHeight1 + 'px';
    document.getElementById('frmAnexos').style.width = sWidth1 + 'px';

    document.getElementById('frmItens').style.height = sHeight1 + 'px';
    document.getElementById('frmItens').style.width = sWidth1 + 'px';
                
    document.getElementById('frmLinks').style.height = sHeight1 + 'px';
    document.getElementById('frmLinks').style.width = sWidth1 + 'px';

    imgStatusSincronizacao.SetVisible(false);

}" />
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" AllowDragging="true" HeaderText="Incluir a Entidad Atual"
                                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="true"
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
       window.top.lpAguardeMasterPage.Hide();                                
       var sucesso = (s.cp_sucesso == undefined) ?  '' : s.cp_sucesso;     
       var erro = (s.cp_erro == undefined) ?  '' : s.cp_erro;     
       var tipoOperacao = s.cp_tipoOperacao;
       if(erro != '')
       {
                  if(erro.search(&quot;DELETE&quot;) != -1 &amp;&amp; erro.search(&quot;ParcelaContrato&quot;) != -1)
                  {
                           window.top.mostraMensagem(traducao.frmContratosNovo_n_o___poss_vel_excluir__pois_este_contrato_possui_parcelas_associadas_, 'erro', true, false, null);
                  }
                 else
                 {
                           window.top.mostraMensagem(erro, 'erro', true, false, null);
                  }                   
       }
        else
       {
                if(sucesso != '')
               {
                           window.top.mostraMensagem(sucesso, 'sucesso', false, false, null, 3500);
                          if(tipoOperacao == &quot;Editar&quot;)
                         {
                                  if (window.onEnd_pnCallback)
	                        onEnd_pnCallback();
                         }
                         if(tipoOperacao == &quot;Incluir&quot;)
                         {
		TipoOperacao = &quot;Editar&quot;;
                                hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
                               hfGeral.Set(&quot;ListaDeParcelas&quot;, &quot;&quot;);
                               if (window.onEnd_pnCallback)
	                        onEnd_pnCallback();

                         }                           
               }
       }
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
        </div>
        &nbsp;<asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    </form>
</body>
</html>
