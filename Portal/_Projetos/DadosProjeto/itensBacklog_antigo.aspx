<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItensBacklog_antigo.aspx.cs" Inherits="_Projetos_DadosProjeto_ItensBacklog_antigo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Alertas</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
    </script>
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 377px;
        }
        .auto-style1 {
            width: 150px;
        }
        .auto-style3 {
            width: 326px;
        }
        .auto-style4 {
            width: 89px;
        }
        .auto-style5 {
            width: 125px;
        }
        .auto-style6 {
            width: 140px;
        }
        </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10px; height: 10px"></td>
                    <td style="height: 10px"></td>
                    <td style="width: 10px; height: 10px;"></td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <!-- PANELCALLBACK: pnCallback -->
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- ASPxGRIDVIEW: gvDados -->
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                        KeyFieldName="CodigoItem" AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados"
                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnCustomErrorText="gvDados_CustomErrorText">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) {
//debugger    
gvDados.SetFocusedRowIndex(e.visibleIndex);

    var funcObj;

     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Get(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
        window.top.mostraMensagem('Deseja realmente excluir a tarefa e o histórico de movimentações?', 'confirmacao', true, true, function () { funcObj['funcaoClickOK']() });                
        funcObj = { funcaoClickOK: function () { pnCallback.PerformCallback('Excluir'); } }
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		pcDados.Show();
     }	
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="90px"
                                                Caption=" " VisibleIndex="0" FixedStyle="Left">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                        <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>

                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                <HeaderTemplate>
                                                    <table align="center" cellpadding="0" cellspacing="0" class="style2">
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                    ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                                    OnItemClick="menu_ItemClick">
                                                                    <Paddings Padding="0px" />
                                                                    <Items>
                                                                        <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                            <Items>
                                                                                <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                    <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                    <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                    <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                                    ToolTip="Exportar para HTML">
                                                                                    <Image Url="~/imagens/menuExportacao/html.png">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                            </Items>
                                                                            <Image Url="~/imagens/botoes/btnDownload.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                            <Items>
                                                                                <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                    <Image IconID="save_save_16x16">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                                <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                    <Image IconID="actions_reset_16x16">
                                                                                    </Image>
                                                                                </dxtv:MenuItem>
                                                                            </Items>
                                                                            <Image Url="~/imagens/botoes/layout.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
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
                                                                </dxtv:ASPxMenu>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Título do Item"
                                                ShowInCustomizationForm="True" VisibleIndex="2" FieldName="TituloItem"
                                                Width="100%">
                                                <Settings AllowAutoFilter="True" AllowGroup="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoItem"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoItemSuperior"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Detalhe" FieldName="DetalheItem"
                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="300px"
                                                Visible="False">
                                                <Settings AllowGroup="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="DescricaoTipoStatusItem"
                                                ShowInCustomizationForm="True" Caption="Status"
                                                Width="150px">
                                                <Settings AllowGroup="True" AllowHeaderFilter="True" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="DescricaoTipoClassificacaoItem"
                                                ShowInCustomizationForm="True" VisibleIndex="9" Caption="Classificação"
                                                Width="170px" Name="DescricaoTipoClassificacaoItem">
                                                <Settings AllowAutoFilter="True" AllowGroup="True" AllowHeaderFilter="True" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="DataInclusao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoUsuarioUltimaAlteracao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="DataUltimaAlteracao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoUsuarioExclusao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="PercentualConcluido"
                                                ShowInCustomizationForm="True" VisibleIndex="16" Visible="False">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoIteracao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="Importancia"
                                                ShowInCustomizationForm="True" VisibleIndex="18" Caption="Importância"
                                                Width="110px">
                                                <Settings AllowAutoFilter="False" AllowGroup="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="Complexidade"
                                                ShowInCustomizationForm="True" VisibleIndex="19" Visible="False">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="EsforcoPrevisto"
                                                ShowInCustomizationForm="True" VisibleIndex="20"
                                                Caption="Estimativa" Width="130px">
                                                <Settings AllowAutoFilter="False" AllowGroup="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="IndicaQuestao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="IndicaBloqueioItem"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoWorkflow"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoInstanciaWf"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoCronogramaProjetoReferencia"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoTarefaReferencia"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="27">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="CodigoTipoStatusItem"
                                                FieldName="CodigoTipoStatusItem" ShowInCustomizationForm="True" Visible="False"
                                                VisibleIndex="8">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="CodigoTipoClassificacaoItem"
                                                FieldName="CodigoTipoClassificacaoItem" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="10">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoCliente" ShowInCustomizationForm="True" Visible="False" VisibleIndex="28">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="NomeCliente" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoTipoTarefaTimeSheet" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="DescricaoTipoTarefaTimeSheet" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="ReceitaPrevista" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataSpinEditColumn Caption="Complexidade" FieldName="Complexidade" ShowInCustomizationForm="True" VisibleIndex="21" Width="150px" Visible="False">
                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                </PropertiesSpinEdit>
                                                <Settings AllowGroup="True" />
                                            </dxtv:GridViewDataSpinEditColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Sprint" FieldName="Sprint" ShowInCustomizationForm="True" VisibleIndex="7" Width="150px">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Complexidade" FieldName="DescricaoComplexidade" ShowInCustomizationForm="True" VisibleIndex="33">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="TituloStatusItem" FieldName="TituloStatusItem" ShowInCustomizationForm="True" Visible="False" VisibleIndex="34">
                                            </dxtv:GridViewDataTextColumn>
                                        </Columns>

                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                            HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />

                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Templates>
                                            <FooterRow>
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido" ID="lblDescricaoConcluido"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td style="width: 10px; background-color: green"></td>
                                                            <td style="width: 10px" align="center">|</td>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada" ID="lblDescricaoAtrasada"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td style="width: 10px; background-color: red"></td>
                                                            <td style="width: 10px" align="center">|</td>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes" ID="lblDescricaoAnotacoes"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; border-right-width: 0px" src="../../imagens/anotacao.gif" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                    <!-- PANEL CONTROL : pcDados -->
                                    <dxpc:ASPxPopupControl runat="server"
                                        AllowDragging="True" ClientInstanceName="pcDados" CloseAction="CloseButton" PopupVerticalOffset="-30"
                                        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ShowCloseButton="True" Width="850px"
                                        Height="175px" ID="pcDados">
                                                                                                        <ClientSideEvents CloseButtonClick="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <dxtv:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="0"
                                                    ClientInstanceName="tabControl"
                                                    Width="100%">
                                                    <TabPages>
                                                        <dxtv:TabPage Name="tabP" Text="Principal">
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="padding-top: 5px">
                                                                                                    <dxtv:ASPxLabel ID="lblTitulo6" runat="server" ClientInstanceName="lblTitulo"
                                                                                                        Text="Título:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxTextBox ID="txtTituloItem" runat="server"
                                                                                                        ClientInstanceName="txtTituloItem"
                                                                                                        MaxLength="150" Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB"
                                                                                                            ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px">
                                                                                    <dxtv:ASPxLabel ID="lblTitulo7" runat="server" ClientInstanceName="lblTitulo"
                                                                                        Text="Detalhes:">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxtv:ASPxMemo ID="txtDetalheItem" runat="server" ClientInstanceName="txtDetalheItem"
                                                                                         Width="100%">
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxtv:ASPxMemo>
                                                                                    <dxtv:ASPxLabel ID="lblContadorMemoDescricao" runat="server"
                                                                                        ClientInstanceName="lblContadorMemoDescricao" Font-Bold="True"
                                                                                        ForeColor="#999999">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px; padding-bottom: 5px;">
                                                                                    <table cellpadding="0" cellspacing="0" class="style2">
                                                                                        <tr>
                                                                                            <td style="width: 125px">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo8" runat="server" ClientInstanceName="lblTitulo"
                                                                                                    Text="Estimativa:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td runat="server" id="tdTituloReceita" class="auto-style5">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo16" runat="server" ClientInstanceName="lblTitulo"
                                                                                                    Text="Receita Prevista:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 170px">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo9" runat="server" ClientInstanceName="lblTitulo" Text="Complexidade:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="auto-style6">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo14" runat="server" ClientInstanceName="lblTitulo" Text="Importância:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="auto-style4">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo15" runat="server" ClientInstanceName="lblTitulo" Text="% Concluído:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td id="tdStatus2" runat="server">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo11" runat="server" ClientInstanceName="lblTitulo" Text="Status:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-right: 5px" class="auto-style5">
                                                                                                <dxtv:ASPxSpinEdit ID="txtEsforco" runat="server"
                                                                                                    ClientInstanceName="txtEsforco" DecimalPlaces="2" DisplayFormatString="n2"
                                                                                                    Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td runat="server" id="tdReceita" style="padding-right: 5px" class="auto-style5">
                                                                                                <dxtv:ASPxSpinEdit ID="spnReceitaPrevista" runat="server" ClientInstanceName="spnReceitaPrevista" DecimalPlaces="2" DisplayFormatString="n2" Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td style="padding-right: 5px">
                                                                                                <dxtv:ASPxComboBox ID="ddlComplexidade" runat="server" ClientInstanceName="ddlComplexidade" Width="100%">
                                                                                                    <Items>
                                                                                                        <dxtv:ListEditItem Text="<%$ Resources:traducao, mnuopt_prioridade_baixa %>" Value="0" />
                                                                                                        <dxtv:ListEditItem Text="<%$ Resources:traducao, mnuopt_prioridade_media %>" Value="1" />
                                                                                                        <dxtv:ListEditItem Text="<%$ Resources:traducao, mnuopt_prioridade_alta %>" Value="2" />
                                                                                                        <dxtv:ListEditItem Text="<%$ Resources:traducao, mnuopt_prioridade_muito_alta %>" Value="3" />
                                                                                                    </Items>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                            <td class="auto-style6" style="padding-right: 5px">
                                                                                                <dxtv:ASPxSpinEdit ID="txtImportancia" runat="server" ClientInstanceName="txtImportancia" DisplayFormatString="n0" NumberType="Integer" Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td class="auto-style4">
                                                                                                <dxtv:ASPxSpinEdit ID="txtPercentualConcluido" runat="server" ClientEnabled="False" ClientInstanceName="txtPercentualConcluido" DecimalPlaces="2" DisplayFormatString="n0" Width="100%">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    </ValidationSettings>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td id="tdStatus1" runat="server" style="padding-left: 5px">
                                                                                                <dxtv:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus" Width="100%">
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-bottom: 5px;" runat="server" id="tdInfoSecundarias">
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td class="style3">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo17" runat="server" ClientInstanceName="lblTitulo" Text="Cliente:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="auto-style3">
                                                                                                <dxtv:ASPxLabel ID="lblTitulo18" runat="server" ClientInstanceName="lblTitulo" Text="Tipo de Atividade:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="lblTitulo12" runat="server" ClientInstanceName="lblTitulo"
                                                                                                    Text="Classificação:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-right: 5px" class="style3">
                                                                                                <dxtv:ASPxComboBox ID="ddlCliente" runat="server" ClientInstanceName="ddlCliente" Width="100%">
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                            <td class="auto-style3" style="padding-right: 5px">
                                                                                                <dxtv:ASPxComboBox ID="ddlTipoAtividade" runat="server" ClientInstanceName="ddlTipoAtividade" Width="100%">
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                            <td style="width: 155px">
                                                                                                <dxtv:ASPxComboBox ID="ddlClassificacao" runat="server"
                                                                                                    ClientInstanceName="ddlClassificacao"
                                                                                                    Width="100%">
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxComboBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td style="padding-right: 5px">
                                                                                                <dxtv:ASPxLabel ID="lblItemNaoPlanejado" runat="server"
                                                                                                    ClientInstanceName="lblItemNaoPlanejado" Font-Italic="True">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                <dxtv:ASPxCallbackPanel ID="pnDescricaoSprint" runat="server"
                                                                                                    ClientInstanceName="pnDescricaoSprint" OnCallback="pnDescricaoSprint_Callback"
                                                                                                    Width="500px">
                                                                                                    <SettingsLoadingPanel Enabled="False" ShowImage="False" />
                                                                                                    <PanelCollection>
                                                                                                        <dxtv:PanelContent runat="server">
                                                                                                            <dxtv:ASPxLabel ID="lblDescricaoSprint" runat="server"
                                                                                                                ClientInstanceName="lblDescricaoSprint" Font-Italic="True">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </dxtv:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                </dxtv:ASPxCallbackPanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                                                                        Text="Salvar" ValidationGroup="MKE"
                                                                                                        Width="100px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	//debugger
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
	}
	else
		return false;
    
}" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                                                                                        Text="Fechar" Width="100px">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                        <dxtv:TabPage Name="tabA" Text="Anexos">
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <iframe id="frmAnexos" frameborder="0" height="<%=alturaFrameAnexos %>" scrolling="no" src=""
                                                                        width="800px"></iframe>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanged="function(s, e) {
	if(e.tab.index == 1)
	{
		if(atualizarURLAnexos != 'N')
	    {
	        		atualizarURLAnexos = 'N';		    
			document.getElementById('frmAnexos').src = urlFrmAnexosContrato + '&ALT=' + (parseInt(gvDados.cp_AlturaFrameAnexos, 10) - 20);
		}
	}
}"
                                                        ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)	
}" />
                                                    <ContentStyle>
                                                        <Paddings Padding="3px" />
                                                    </ContentStyle>
                                                </dxtv:ASPxPageControl>
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

                                                <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                                    GridViewID="gvDados">
                                                </dxtv:ASPxGridViewExporter>

                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                </dxp:PanelContent>
                            </PanelCollection>

                            <SettingsLoadingPanel Enabled="False" ShowImage="False" />

                            <ClientSideEvents EndCallback="function(s, e) {
var msgApp = 	s.cp_MSG;
               if(s.cp_MSG == 'OK')
               {
		msgApp = 'Dados atualizados com sucesso!';
		window.top.mostraMensagem(msgApp, 'sucesso', false, false, null);
		posSalvarComSucesso();
                }
                else
                {
		window.top.mostraMensagem(s.cp_MSG, 'erro', true, false, null);
                }
                
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td style="width: 5px"></td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido"
            HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="True" ShowHeader="False"
            Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
