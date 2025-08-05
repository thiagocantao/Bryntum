<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reunioes.aspx.cs" Inherits="Reunioes_reunioes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript">
        var pastaImagens = "../imagens";
    </script>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/reunioes_ASPxListbox.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>
    <title>Reuniões</title>
    <style type="text/css">
        body {
            overflow: hidden;
        }

        div[id=pnCallback_pcDados_pnFormulario_tabControl_Content4Div] div.dxpc-mainDiv {
            height: 400px;
            overflow: auto;
        }

            div[id=pnCallback_pcDados_pnFormulario_tabControl_Content4Div] div.dxpc-mainDiv div[id=pnCallback_pcDados_pnFormulario_tabControl_pnExternoToDoList_gvToDoList_DXPEForm_efnew_DXEFL] {
                font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif !important;
                font-size: 14px !important;
            }

                div[id=pnCallback_pcDados_pnFormulario_tabControl_Content4Div] div.dxpc-mainDiv div[id=pnCallback_pcDados_pnFormulario_tabControl_pnExternoToDoList_gvToDoList_DXPEForm_efnew_DXEFL] input,
                div[id=pnCallback_pcDados_pnFormulario_tabControl_Content4Div] div.dxpc-mainDiv div[id=pnCallback_pcDados_pnFormulario_tabControl_pnExternoToDoList_gvToDoList_DXPEForm_efnew_DXEFL] select,
                div[id=pnCallback_pcDados_pnFormulario_tabControl_Content4Div] div.dxpc-mainDiv div[id=pnCallback_pcDados_pnFormulario_tabControl_pnExternoToDoList_gvToDoList_DXPEForm_efnew_DXEFL] textarea {
                    font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif !important;
                    font-size: 14px !important;
                }

        .divLoading {
        }

        .style1 {
            width: 10px;
            height: 10px;
        }

        .rolagem-tab {
            overflow-y: auto;
            height: 260px;
        }

        #pcDados_pnFormulario_tabControl_pnExternoToDoList_gvToDoList > tbody > tr:nth-child(1) > td:nth-child(1) {
            padding-right: 1.5rem;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <!-- table titulo -->
            <!-- table conteudo principal -->
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="left" style="width: 10px"></td>
                    <td id="tdLista" align="left">
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback" EnableCallbackAnimation="True">
                            <Styles>
                                <LoadingPanel HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divLoading">
                                </LoadingPanel>
                                <LoadingDiv CssClass="divLoading">
                                </LoadingDiv>
                            </Styles>
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <div id="divGrid" style="visibility: hidden">
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoEvento"
                                            AutoGenerateColumns="False" Width="100%"
                                            ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize1"
                                            OnHeaderFilterFillItems="gvDados_HeaderFilterFillItems" OnCustomErrorText="gvDados_CustomErrorText">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                CustomButtonClick="function(s, e) {
    //debugger
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
         s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
         s.GetSelectedFieldValues('CodigoEvento', editaReuniao);
     }
      if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
        

		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;btnExcluirCustom&quot;);
        e.processOnServer = confirm(traducao.reunioes_tem_certeza_que_deseja_excluir_a_reuni_o_);

        if (e.processOnServer)
	    {
            gvDados.SetFocusedRowIndex(e.visibleIndex);

            pnCallback.PerformCallback('Excluir');
		    desabilitaHabilitaComponentes();
         }
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
         s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
         s.GetSelectedFieldValues('CodigoEvento', visualizaReuniao);
     }	
}"
                                                Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="A&#231;&#227;o" Width="100px"
                                                    Caption="A&#231;&#227;o" VisibleIndex="1">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Visibility="Invisible"
                                                            Text="Incluir">
                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                            <Image Url="~/imagens/botoes/pFormulario.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                    <HeaderTemplate>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoResumida" Name="DescricaoResumida"
                                                    Caption="Assunto" VisibleIndex="2" Width="300px">
                                                    <Settings AllowHeaderFilter="False"></Settings>
                                                    <HeaderStyle Wrap="True" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataDateColumn FieldName="InicioPrevisto" Name="Local" Width="230px"
                                                    Caption="In&#237;cio Previsto" VisibleIndex="3">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                    </PropertiesDateEdit>
                                                    <Settings ShowFilterRowMenu="True"></Settings>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataDateColumn FieldName="TerminoPrevisto" Width="230px" Caption="T&#233;rmino Previsto"
                                                    VisibleIndex="4" SortIndex="0" SortOrder="Descending">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                    </PropertiesDateEdit>
                                                    <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataDateColumn FieldName="InicioReal" Name="InicioReal" Width="230px"
                                                    Caption="In&#237;cio Real" VisibleIndex="5">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                    </PropertiesDateEdit>
                                                    <Settings ShowFilterRowMenu="True"></Settings>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataDateColumn FieldName="TerminoReal" Name="TerminoReal" Width="230px"
                                                    Caption="T&#233;rmino Real" VisibleIndex="6" SortIndex="1" SortOrder="Descending">
                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                    </PropertiesDateEdit>
                                                    <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Quando" Name="Quando" Width="200px" Caption="Quando"
                                                    VisibleIndex="25">
                                                    <PropertiesTextEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                    </PropertiesTextEdit>
                                                    <Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelEvento" Name="CodigoResponsavelEvento"
                                                    Visible="False" VisibleIndex="7">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoData" Name="inicioPrevistoData"
                                                    Visible="False" VisibleIndex="8">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoHora" Name="inicioPrevistoHora"
                                                    Visible="False" VisibleIndex="9">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto"
                                                    Visible="False" VisibleIndex="10">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoData" Name="TerminoPrevistoData"
                                                    Visible="False" VisibleIndex="11">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoHora" Name="TerminoPrevistoHora"
                                                    Visible="False" VisibleIndex="12">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="InicioRealData" Name="InicioRealData" Visible="False"
                                                    VisibleIndex="13">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="InicioRealHora" Name="InicioRealHora" Visible="False"
                                                    VisibleIndex="14">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TerminoRealData" Name="TerminoRealData"
                                                    Visible="False" VisibleIndex="15">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TerminoRealHora" Name="TerminoRealHora"
                                                    Visible="False" VisibleIndex="16">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao"
                                                    Visible="False" VisibleIndex="17">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                                    Visible="False" VisibleIndex="18">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="LocalEvento" Name="LocalEvento" Visible="False"
                                                    VisibleIndex="19">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Pauta" Name="Pauta" Visible="False" VisibleIndex="20">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="ResumoEvento" Name="ResumoEvento" Visible="False"
                                                    VisibleIndex="21">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoEvento" Name="CodigoTipoEvento"
                                                    Visible="False" VisibleIndex="22">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                                    Visible="False" VisibleIndex="23">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAtrasada" Visible="False" VisibleIndex="24">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="PermissaoEditarAtaResponsavel" FieldName="PermissaoEditarAtaResponsavel"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="CodigoEvento" FieldName="CodigoEvento" Name="CodigoEvento" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="True" Visible="False">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                                                ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True" HorizontalScrollBarMode="Visible"></Settings>
                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                            <Images>
                                                <HeaderFilter ToolTip="Filtrar">
                                                </HeaderFilter>
                                            </Images>
                                            <Templates>
                                                <FooterRow>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td class="grid-legendas-cor grid-legendas-cor-atrasado"><span></span></td>
                                                                <td class="grid-legendas-label grid-legendas-label-atrasado">
                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblDescricaoAtrasada"
                                                                        Text="<%# Resources.traducao.reunioes_reuni_es_passadas_n_o_finalizadas %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
	verificaEnvioPauta();
	if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.reunioes_reuni_o_exclu_da_com_sucesso_);
	else
	{
		if(tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1)
	    	btnEnviarPauta.SetText(traducao.reunioes_enviar_pauta);
		else
			btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);

		if(&quot;Incluir&quot; == s.cp_OperacaoOk){
			mostraDivSalvoPublicado(traducao.reunioes_reuni_o_salva_com_sucesso_);
            gvDados.SetVisible(true);
			pcDados.Hide();
        }
	                 else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		{
			mostraDivSalvoPublicado(traducao.reunioes_dados_gravados_com_sucesso_);
            gvDados.SetVisible(true);
			pcDados.Hide();
		}
		else if(&quot;EnviarPauta&quot; == s.cp_OperacaoOk)
		{
			mostraDivSalvoPublicado(traducao.reunioes_pauta_enviada_com_sucesso_aos_participantes_);
            gvDados.SetVisible(true);
			pcDados.Hide();
		}
		else if(&quot;EnviarAta&quot; == s.cp_OperacaoOk)
		{
			mostraDivSalvoPublicado(traducao.reunioes_ata_enviada_com_sucesso_aos_participantes_);
            gvDados.SetVisible(true);
			pcDados.Hide();
		}
		else if(&quot;Erro&quot; == s.cp_OperacaoOk)
        {   
            pcMensagemPauta.Hide();
            window.top.mostraMensagem(s.cp_ErroSalvar, 'erro', true, false, null);
        }		
        if(&quot;Erro&quot; != s.cp_OperacaoOk)
		{
	    	pcMensagemPauta.Hide();
			hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
			onClickBarraNavegacao(traducao.reunioes_editar, gvDados, pcDados);
			desabilitaHabilitaComponentes();
		}
	}
	
}"
                                Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>

                    </td>
                    <td align="left"></td>
                </tr>
            </table>

        </div>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcMensagemPauta"
            CloseAction="None" HeaderText="Envio de pauta" PopupAction="None" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="750px" ID="pcMensagemPauta"
            EnableViewState="False" PopupVerticalOffset="-15">
            <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
}"></ClientSideEvents>
            <ContentStyle>
                <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Texto de apresenta&#231;&#227;o:" ID="Label1"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heEncabecadoAta" Width="98%"
                                        Height="130px" ID="heEncabecadoAta">
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
                                        <SettingsDialogs>
                                            <InsertImageDialog>
                                                <SettingsImageSelector>
                                                    <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                </SettingsImageSelector>
                                            </InsertImageDialog>
                                            <InsertLinkDialog>
                                                <SettingsDocumentSelector>
                                                    <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                </SettingsDocumentSelector>
                                            </InsertLinkDialog>
                                        </SettingsDialogs>
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

	var tipoEnvio = '';

	if(tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1){
		if(verificarDadosPreenchidos())
		{
			tipoEnvio = 'EnviarPauta';
			pnCallback.PerformCallback(tipoEnvio);
		}
	}
	else{
		if(verificarDadosAta())
		{
			tipoEnvio = 'EnviarAta';
			pnCallback.PerformCallback(tipoEnvio);
		}
	}
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarEncabecadoPauta"
                                                        Text="Fechar" Width="100px" ID="btnCancelarEncabecadoPauta"
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
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxlp:ASPxLoadingPanel ID="lpCarregando" runat="server" ClientInstanceName="lpCarregando"
            Height="50px" Modal="True" Width="150px">
        </dxlp:ASPxLoadingPanel>
        <dxtv:ASPxPopupControl ID="pcComentarios" runat="server" AllowDragging="True" ClientInstanceName="pcComentarios"
            CloseAction="None" HeaderText="Comentários"
            PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="695px" PopupVerticalOffset="-15">
            <HeaderStyle Font-Bold="True" />
            <ContentCollection>
                <dxtv:PopupControlContentControl runat="server">
                    <dxtv:ASPxCallbackPanel ID="pnGeral" runat="server" ClientInstanceName="pnGeral"
                        OnCallback="pnGeral_Callback" Width="100%">
                        <ClientSideEvents EndCallback="function(s, e) {
	pcComentarios.Show();
}" />
                        <Paddings Padding="0px" />
                        <PanelCollection>
                            <dxtv:PanelContent runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxMemo ID="mmComentario" runat="server" ClientInstanceName="mmComentario"
                                                Rows="10" Width="100%">
                                            </dxtv:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 10px">
                                            <dxtv:ASPxButton ID="btFechar" runat="server" AutoPostBack="False" ClientInstanceName="btFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {pcComentarios.Hide();}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dxtv:PanelContent>
                        </PanelCollection>
                    </dxtv:ASPxCallbackPanel>
                </dxtv:PopupControlContentControl>
            </ContentCollection>
        </dxtv:ASPxPopupControl>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None" HeaderText="Reuni&#227;o" AutoUpdatePosition="True" PopupAction="None" ShowHeader="true"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupHorizontalOffset="-15" ShowCloseButton="False"
            Width="50%" ID="pcDados" Height="425px" PopupVerticalOffset="-15">
            <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
	document.getElementById('frmAnexosReuniao').src = &quot;&quot;;
    LimpaCamposFormulario();
}"
                CloseUp="function(s, e) {
}"></ClientSideEvents>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario" Style="overflow: auto">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl"
                                                    Width="99%" ID="tabControl">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="TabA" Text="<%$ Resources:traducao, reunioes_reuni_o %>">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                                                    <div class="rolagem-tab">
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 33%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_assunto_ %>" ID="lblAssunto" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 33%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_respons_vel_ %>" ID="lblResponsavel" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 33%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_tipo_de_reuni_o_ %>" ID="lblTipoEventos" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-right: 5px">
                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtAssunto"
                                                                                                            ID="txtAssunto">
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                                                <RequiredField ErrorText="<%$ Resources:traducao, reunioes_campo_obrigat_rio %>"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px">
                                                                                                        <dxe:ASPxComboBox ID="ddlResponsavelEvento" runat="server" CallbackPageSize="80"
                                                                                                            ClientInstanceName="ddlResponsavelEvento" DropDownRows="10" DropDownStyle="DropDown"
                                                                                                            EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                            OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                                                            TextField="<%$ Resources:traducao, reunioes_nomeusuario %>" TextFormatString="{0}" ValueField="CodigoUsuario" ValueType="System.Int32"
                                                                                                            Width="100%">
                                                                                                            <Columns>
                                                                                                                <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                                                                <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="320px" />
                                                                                                            </Columns>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE" CausesValidation="True" Display="Dynamic">
                                                                                                                <RequiredField ErrorText="<%$ Resources:traducao, reunioes_campo_obrigat_rio %>" />
                                                                                                                <RequiredField ErrorText="<%$ Resources:traducao, reunioes_campo_obrigat_rio %>"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTipoEvento"
                                                                                                            ID="ddlTipoEvento">
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
                                                                                        <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="In&#237;cio:" ID="lblInicio" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10%"></td>
                                                                                                    <td style="width: 20%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="T&#233;rmino:" ID="lblTermino" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10%"></td>
                                                                                                    <td style="width: 40%">
                                                                                                        <dxe:ASPxLabel runat="server" Text="Local:" ID="lblLocal" Font-Size="11px" Width="100%"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>

                                                                                                    <td style="padding-right: 5px;">
                                                                                                        <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>"
                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlInicioPrevisto"
                                                                                                            ID="ddlInicioPrevisto">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
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
                                                                                                    <td style="padding-right: 10px;">
                                                                                                        <dxe:ASPxTextBox runat="server" CssClass="dx_Hora" ClientInstanceName="txtHoraInicio" ID="txtHoraInicio" Width="100%">
                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 5px;">
                                                                                                        <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>"
                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlTerminoPrevisto"
                                                                                                            ID="ddlTerminoPrevisto">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                                <ButtonStyle>
                                                                                                                    <PressedStyle>
                                                                                                                    </PressedStyle>
                                                                                                                    <HoverStyle>
                                                                                                                    </HoverStyle>
                                                                                                                </ButtonStyle>
                                                                                                            </CalendarProperties>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxDateEdit>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 10px;">
                                                                                                        <dxe:ASPxTextBox runat="server" CssClass="dx_Hora" ClientInstanceName="txtHoraTermino" Width="100%"
                                                                                                            ID="txtHoraTermino">
                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
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
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Pauta:" ID="lblPauta" Font-Size="11px"></dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoPauta" Width="100%" ID="memoPauta" EnableTheming="True" Height="460px">
                                                                                            <StylesToolbars>
                                                                                                <ToolbarItem>
                                                                                                    <Paddings Padding="1px"></Paddings>
                                                                                                </ToolbarItem>
                                                                                            </StylesToolbars>
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
                                                                                                        <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False">
                                                                                                        </dxhe:ToolbarCheckSpellingButton>
                                                                                                        <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                                            <Items>
                                                                                                                <dxhe:ToolbarInsertTableDialogButton BeginGroup="True">
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
                                                                                                                <dxhe:ToolbarListEditItem Text="Normal" Value="p" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Address" Value="address" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                                                            </Items>
                                                                                                        </dxhe:ToolbarParagraphFormattingEdit>
                                                                                                        <dxhe:ToolbarFontNameEdit>
                                                                                                            <Items>
                                                                                                                <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                                                <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                                                <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                                                            </Items>
                                                                                                        </dxhe:ToolbarFontNameEdit>
                                                                                                        <dxhe:ToolbarFontSizeEdit>
                                                                                                            <Items>
                                                                                                                <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                                                                <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                                                                <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                                                                <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                                                                <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                                                                <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                                                                <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7" />
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
                                                                                            <SettingsDialogs>
                                                                                                <InsertImageDialog>
                                                                                                    <SettingsImageSelector>
                                                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                                                    </SettingsImageSelector>
                                                                                                </InsertImageDialog>
                                                                                                <InsertLinkDialog>
                                                                                                    <SettingsDocumentSelector>
                                                                                                        <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                                                                    </SettingsDocumentSelector>
                                                                                                </InsertLinkDialog>
                                                                                            </SettingsDialogs>
                                                                                        </dxhe:ASPxHtmlEditor>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="TabB" Text="Participantes">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True" Width="170px">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                                                    <div class="rolagem-tab">
                                                                        <table cellspacing="0" cellpadding="0" width="98%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 355px">
                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_dispon_veis_ %>" ClientInstanceName="lblDisponiveis" Font-Size="11px"
                                                                                            ID="lblDisponiveis">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td align="center" style="width: 60px"></td>
                                                                                    <td align="left" style="width: 355px">
                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_selecionados_ %>" ClientInstanceName="lblSelecionado" Font-Size="11px"
                                                                                            ID="lblSelecionado">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <dxe:ASPxListBox ID="lbDisponiveis" runat="server" ClientInstanceName="lbDisponiveis"
                                                                                                        EncodeHtml="False" Height="100px" OnCallback="lbDisponiveis_Callback"
                                                                                                        Rows="3" SelectionMode="CheckColumn" Width="100%" CallbackPageSize="10" EnableCallbackMode="True" EnableSelectAll="False">
                                                                                                        <ClientSideEvents SelectedIndexChanged="UpdateButtons" Init="UpdateButtons
"></ClientSideEvents>
                                                                                                        <ItemStyle>
                                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                                            </SelectedStyle>
                                                                                                        </ItemStyle>
                                                                                                        <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                                        <FilteringSettings ShowSearchUI="True" />
                                                                                                        <ClientSideEvents Init="UpdateButtons
"
                                                                                                            SelectedIndexChanged="UpdateButtons" />
                                                                                                        <ValidationSettings>
                                                                                                            <ErrorImage Height="14px" Width="14px">
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
                                                                                                <td style="height: 5px">
                                                                                                    <dxe:ASPxLabel ID="lblGrupos0" runat="server" Font-Size="11px"
                                                                                                        Text="Grupos:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                                                                                        <ClientSideEvents EndCallback="function(s, e) {
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
}"
                                                                                                            BeginCallback="function(s, e) {
	mostraDivAtualizando('Atualizando...'); 
}" />
                                                                                                        <ClientSideEvents BeginCallback="function(s, e) {
	mostraDivAtualizando(&#39;Atualizando...&#39;); 
}"
                                                                                                            EndCallback="function(s, e) {
    var delimitador = &quot;&#165;&quot;;
  	var listaCodigos = s.cp_ListaCodigos;
  	var arrayItens = listaCodigos.split(&#39;;&#39;);

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
    setTimeout(&#39;fechaTelaEdicao();&#39;, 10);
}"></ClientSideEvents>
                                                                                                    </dxcb:ASPxCallback>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxListBox ID="lbGrupos" runat="server" ClientInstanceName="lbGrupos" EnableCallbackMode="True"
                                                                                                        EnableClientSideAPI="True" EncodeHtml="False"
                                                                                                        Rows="3" SelectionMode="CheckColumn" Width="100%">
                                                                                                        <ItemStyle>
                                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                                            </SelectedStyle>
                                                                                                        </ItemStyle>
                                                                                                        <FilteringSettings ShowSearchUI="True" />
                                                                                                        <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	callback.PerformCallback(s.GetValue());
}" />
                                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	callback.PerformCallback(s.GetValue());
}"></ClientSideEvents>
                                                                                                        <ValidationSettings>
                                                                                                            <ErrorImage Height="14px" Width="14px">
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
                                                                                        </table>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <table cellpadding="0" cellspacing="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="height: 28px" valign="middle">
                                                                                                        <dxe:ASPxButton ID="btnADDTodos" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                            ClientInstanceName="btnADDTodos" EncodeHtml="False" Font-Bold="True" Height="25px" Text="&gt;&gt;" Width="45px">
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();    
	capturaCodigosInteressados();



}" />
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
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
                                                                                                    <td style="height: 28px">
                                                                                                        <dxe:ASPxButton ID="btnADD" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                            ClientInstanceName="btnADD" EncodeHtml="False" Font-Bold="True" Height="25px" Text="&gt;" Width="45px">
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();	
	capturaCodigosInteressados();
}" />
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
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
                                                                                                    <td style="height: 28px">
                                                                                                        <dxe:ASPxButton ID="btnRMV" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                            ClientInstanceName="btnRMV" EncodeHtml="False" Font-Bold="True" Height="25px" Text="&lt;" Width="45px">
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();	
}" />
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
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
                                                                                                    <td style="height: 28px">
                                                                                                        <dxe:ASPxButton ID="btnRMVTodos" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                            ClientInstanceName="btnRMVTodos" EncodeHtml="False" Font-Bold="True" Height="25px" Text="&lt;&lt;" Width="45px">
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveTodosItens(lbSelecionados, lbDisponiveis);
    UpdateButtons();
	capturaCodigosInteressados();
}" />
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <ClientSideEvents Click="function(s, e) 
{
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
                                                                                    <td align="left">
                                                                                        <dxe:ASPxListBox ID="lbSelecionados" runat="server" ClientInstanceName="lbSelecionados"
                                                                                            EnableSynchronization="True" EncodeHtml="False"
                                                                                            Height="100px" OnCallback="lbSelecionados_Callback" Rows="4" SelectionMode="CheckColumn"
                                                                                            Width="100%">
                                                                                            <ItemStyle>
                                                                                                <SelectedStyle BackColor="#FFE4AC">
                                                                                                </SelectedStyle>
                                                                                            </ItemStyle>
                                                                                            <FilteringSettings ShowSearchUI="True" />
                                                                                            <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                                                            <ClientSideEvents Init="UpdateButtons" SelectedIndexChanged="UpdateButtons" EndCallback="function(s, e) {	
    btnSalvar.SetEnabled(true);
	verificaEnvioPauta();	

    capturaCodigosInteressados();	        
    
	lpCarregando.Hide();
}" />
                                                                                            <ClientSideEvents EndCallback="function(s, e) {	
    btnSalvar.SetEnabled(true);
	verificaEnvioPauta();	

    capturaCodigosInteressados();	        
    
	lpCarregando.Hide();
}"
                                                                                                SelectedIndexChanged="UpdateButtons" Init="UpdateButtons"></ClientSideEvents>
                                                                                            <ValidationSettings>
                                                                                                <ErrorImage Height="14px" Width="14px">
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
                                                                                    <td valign="top"></td>
                                                                                    <td style="width: 60px" align="center"></td>
                                                                                    <td align="center">
                                                                                        <dxhf:ASPxHiddenField ID="hfRiscosSelecionados" runat="server" ClientInstanceName="hfRiscosSelecionados">
                                                                                        </dxhf:ASPxHiddenField>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="TabC" Text="Ata">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True" Width="95px">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                                                    <div class="rolagem-tab">
                                                                        <table cellspacing="0" cellpadding="0" width="98%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="position: inherit">
                                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 110px">
                                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_in_cio_real_ %>" ID="lblInicioReal" Font-Size="11px"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 10px"></td>
                                                                                                    <td style="width: 110px">
                                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_t_rmino_real_ %>" ID="lblTerminoReal" Font-Size="11px"></dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 55px"></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">
                                                                                                        <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>"
                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlInicioReal"
                                                                                                            ID="ddlInicioReal">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                            </CalendarProperties>
                                                                                                            <ClientSideEvents DateChanged="function(s, e) {
                ddlTerminoReal.SetDate(s.GetValue());
  	calendar = ddlTerminoReal.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxDateEdit>
                                                                                                    </td>
                                                                                                    <td style="padding-right: 10px; padding-left: 10px;">
                                                                                                        <dxe:ASPxTextBox runat="server" CssClass="dx_Hora" ClientInstanceName="txtHoraInicioAta"
                                                                                                            ID="txtHoraInicioAta">
                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td style="width: 140px">
                                                                                                        <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>"
                                                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlTerminoReal"
                                                                                                            ID="ddlTerminoReal">
                                                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                            </CalendarProperties>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxDateEdit>
                                                                                                    </td>
                                                                                                    <td style="width: 55px; padding-right: 10px; padding-left: 10px;">
                                                                                                        <dxe:ASPxTextBox runat="server" CssClass="dx_Hora" ClientInstanceName="txtHoraTerminoAta"
                                                                                                            ID="txtHoraTerminoAta">
                                                                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxImage ID="btnImprimir" runat="server" ClientInstanceName="btnImprimir" ImageUrl="~/imagens/botoes/btnPDF.png"
                                                                                                            Cursor="pointer" ToolTip="<%$ Resources:traducao, reunioes_relat_rio_de_ata_de_reuni_o %>">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	if(verificarDadosAta())
	{
abrejanelaImpressaoReuniao(1000, 760);		

    }
}" />
                                                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	if(verificarDadosAta())
	{
       var codigoEvento = hfGeral.Get(&quot;CodigoEventoAtual&quot;);
	   var codigoProjeto = hfGeral.Get(&quot;CodigoProjetoAtual&quot;);
       var moduloSistema = hfGeral.Get(&quot;moduloSistema&quot;);			
       abrejanelaImpressaoReuniao(codigoProjeto, codigoEvento, moduloSistema);
    }
}"></ClientSideEvents>
                                                                                                        </dxe:ASPxImage>
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
                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_resumo_da_reuni_o_ %>" ID="lblAta" Font-Size="11px"></dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoAta" Width="100%" ID="memoAta">
                                                                                            <StylesToolbars>
                                                                                                <ToolbarItem>
                                                                                                    <Paddings Padding="1px"></Paddings>
                                                                                                </ToolbarItem>
                                                                                            </StylesToolbars>
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
                                                                                                        <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False">
                                                                                                        </dxhe:ToolbarCheckSpellingButton>
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
                                                                                            <SettingsDialogs>
                                                                                                <InsertImageDialog>
                                                                                                    <SettingsImageSelector>
                                                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                                                    </SettingsImageSelector>
                                                                                                </InsertImageDialog>
                                                                                                <InsertLinkDialog>
                                                                                                    <SettingsDocumentSelector>
                                                                                                        <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                                                                    </SettingsDocumentSelector>
                                                                                                </InsertLinkDialog>
                                                                                            </SettingsDialogs>
                                                                                        </dxhe:ASPxHtmlEditor>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtv:TabPage Name="tabPendencias" Text="Pendências da reunião anterior" ToolTip="Pendencias da reunião anterior">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True" Width="300px">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxtv:ContentControl runat="server">
                                                                    <div class="rolagem-tab">
                                                                        <table style="width: 98%" cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Status:"
                                                                                            ID="ASPxLabel2" Font-Size="11px">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="332px" ID="ddlStatusPendencia" SelectedIndex="0">
                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvPendencias.PerformCallback();
}"></ClientSideEvents>
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="Todos" Value="-1" Selected="True"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Text="Atrasada" Value="Atrasada"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Text="Em Execução" Value="Em_Execução" />
                                                                                                <dxe:ListEditItem Text="Futura" Value="Futura" />
                                                                                                <dxe:ListEditItem Text="Início atrasado" Value="Início atrasado" />
                                                                                            </Items>
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPendencias" KeyFieldName="CodigoTarefa"
                                                                                            AutoGenerateColumns="False" Width="100%"
                                                                                            ID="gvPendencias" OnCustomButtonInitialize="gvPendencias_CustomButtonInitialize"
                                                                                            KeyboardSupport="True" OnCustomColumnDisplayText="gvPendencias_CustomColumnDisplayText"
                                                                                            OnCustomCallback="gvPendencias_CustomCallback" OnHtmlDataCellPrepared="gvPendencias_HtmlDataCellPrepared"
                                                                                            OnAfterPerformCallback="gvPendencias_AfterPerformCallback">
                                                                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	gvPendencias.SetFocusedRowIndex(e.visibleIndex);
	e.processOnServer = false;
    if (e.buttonID == &quot;btnComentarios&quot;)
    {
       gvMedicao.GetRowValues(gvPendencias.GetFocusedRowIndex(), 'CodigoMedicao;Contrato;Mes_Ano;Fornecedor;ObjetoContrato;ValorContrato;DataInicio;DataTermino;DataBaseReajuste;CodigoContrato;ValorTotalMedicao;ValorMedidoAteMes;', MontaCampos );
       gvHistorico.PerformCallback(e.visibleIndex);       
    }	
}" />
                                                                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	gvPendencias.SetFocusedRowIndex(e.visibleIndex);
	e.processOnServer = false;
    if (e.buttonID == &quot;btnComentarios&quot;)
    {
//       pcComentarios.Show();
       pnGeral.PerformCallback(e.visibleIndex);       
    }	
}"></ClientSideEvents>
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoTarefa" Caption="<%$ Resources:traducao, reunioes_tarefa %>" VisibleIndex="3"
                                                                                                    Width="260px">
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Width="220px" Caption="<%$ Resources:traducao, reunioes_respons_vel %>"
                                                                                                    VisibleIndex="4">
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataDateColumn Caption="<%$ Resources:traducao, reunioes_t_rmino_previsto %>" FieldName="TerminoPrevisto"
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="125px">
                                                                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                                                                    </PropertiesDateEdit>
                                                                                                    <Settings ShowFilterRowMenu="True" />
                                                                                                    <Settings ShowFilterRowMenu="True"></Settings>
                                                                                                </dxwgv:GridViewDataDateColumn>
                                                                                                <dxwgv:GridViewDataDateColumn Caption="<%$ Resources:traducao, reunioes_t_rmino_real %>" FieldName="TerminoReal" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="7" Width="115px">
                                                                                                    <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>">
                                                                                                    </PropertiesDateEdit>
                                                                                                    <Settings ShowFilterRowMenu="True" />
                                                                                                    <Settings ShowFilterRowMenu="True"></Settings>
                                                                                                </dxwgv:GridViewDataDateColumn>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                                    Width="40px" Caption=" ">
                                                                                                    <CustomButtons>
                                                                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnComentarios" Text="Comentários">
                                                                                                            <Image Url="~/imagens/botoes/pFormulario.png">
                                                                                                            </Image>
                                                                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                                                                    </CustomButtons>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="CodigoTarefa" FieldName="CodigoTarefa" ShowInCustomizationForm="True"
                                                                                                    Visible="False" VisibleIndex="9">
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="<%$ Resources:traducao, reunioes_anotacoes %>" FieldName="Anotacoes" ShowInCustomizationForm="True"
                                                                                                    Visible="False" VisibleIndex="8">
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="<%$ Resources:traducao, reunioes_reuni_o %>" FieldName="DescricaoReuniao" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="5" Width="200px">
                                                                                                    <CellStyle Wrap="True">
                                                                                                    </CellStyle>
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxtv:GridViewDataImageColumn Caption=" " FieldName="Estagio" Name="Estagio" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="0" Width="50px">
                                                                                                    <PropertiesImage DisplayFormatString="&lt;img {0}&gt;">
                                                                                                    </PropertiesImage>
                                                                                                    <HeaderTemplate>
                                                                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                            <tr>
                                                                                                                <td align="center">
                                                                                                                    <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent" ClientInstanceName="menu2"
                                                                                                                        ItemSpacing="5px" OnItemClick="menu2_ItemClick" OnInit="menu2_Init">
                                                                                                                        <Paddings Padding="0px" />
                                                                                                                        <Items>
                                                                                                                            <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, reunioes_incluir %>">
                                                                                                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                                                </Image>
                                                                                                                            </dxm:MenuItem>
                                                                                                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<%$ Resources:traducao, reunioes_exportar %>">
                                                                                                                                <Items>
                                                                                                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_xls %>">
                                                                                                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                                                        </Image>
                                                                                                                                    </dxm:MenuItem>
                                                                                                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_pdf %>">
                                                                                                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                                                        </Image>
                                                                                                                                    </dxm:MenuItem>
                                                                                                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_rtf %>">
                                                                                                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                                                        </Image>
                                                                                                                                    </dxm:MenuItem>
                                                                                                                                    <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_html %>" ClientVisible="False">
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
                                                                                                                                    <dxm:MenuItem Text="Salvar" ToolTip="<%$ Resources:traducao, reunioes_salvar_layout %>">
                                                                                                                                        <Image IconID="save_save_16x16">
                                                                                                                                        </Image>
                                                                                                                                    </dxm:MenuItem>
                                                                                                                                    <dxm:MenuItem Text="Restaurar" ToolTip="<%$ Resources:traducao, reunioes_restaurar_layout %>">
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
                                                                                                </dxtv:GridViewDataImageColumn>
                                                                                            </Columns>
                                                                                            <SettingsResizing ColumnResizeMode="Control" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <Settings VerticalScrollBarMode="Visible" ShowFooter="True" ShowGroupPanel="True"
                                                                                                HorizontalScrollBarMode="Visible" VerticalScrollableHeight="120"></Settings>
                                                                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                                                            <Styles>
                                                                                                <Footer>
                                                                                                    <Paddings Padding="0px" />
                                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                                </Footer>
                                                                                            </Styles>
                                                                                            <Templates>
                                                                                                <FooterRow>
                                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table cellpadding="0" cellspacing="0">
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxImage ID="ASPxImage4" runat="server" Height="16px" ImageUrl="~/imagens/Verde.gif">
                                                                                                                            </dxe:ASPxImage>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                                                Text="Em execução">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxImage ID="ASPxImage8" runat="server" Height="16px" ImageUrl="~/imagens/amarelo.gif">
                                                                                                                            </dxe:ASPxImage>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                                                Text="Início atrasado">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxImage ID="ASPxImage5" runat="server" Height="16px" ImageUrl="~/imagens/Vermelho.gif">
                                                                                                                            </dxe:ASPxImage>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                                                                Text="Atrasada">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <dxe:ASPxImage ID="ASPxImage6" runat="server" Height="16px" ImageUrl="~/imagens/Relogio.png">
                                                                                                                            </dxe:ASPxImage>
                                                                                                                        </td>
                                                                                                                        <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                                                                                Text="Futura">
                                                                                                                            </dxe:ASPxLabel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                            <td align="right">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                                                    Text="<%$ Resources:traducao, reunioes__pend_ncias_da_reuni_o_anterior %>" Font-Italic="True">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </FooterRow>
                                                                                            </Templates>
                                                                                        </dxwgv:ASPxGridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </dxtv:ContentControl>
                                                            </ContentCollection>
                                                        </dxtv:TabPage>
                                                        <dxtc:TabPage Name="TabD" Text="Plano de A&#231;&#227;o">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True" Width="200px">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="ContentControl4" runat="server">
                                                                    <div runat="server" class="rolagem-tab" id="Content4Div">
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                                                            <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>
                                                            <TabStyle Wrap="True" Width="150px">
                                                            </TabStyle>
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                                                    <div class="rolagem-tab">
                                                                        <iframe id="frmAnexosReuniao" frameborder="0" scrolling="yes"
                                                                            height="350px" width="98%"></iframe>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanged="function(s, e) {
                                                                                
	if(s.GetActiveTab().index == 0 || s.GetActiveTab().index == 1)
	{
		btnEnviarPauta.SetText(traducao.reunioes_enviar_pauta);
		if(s.GetActiveTab().index == 1)
			UpdateButtons();
		
	}
                else if(e.tab.index == 3)
{
                             btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);
                             gvPendencias.PerformCallback();
}
	else if(e.tab.index == 5)
	{
		btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);
		if(atualizarURLAnexos != 'N')
	    {
	        atualizarURLAnexos = 'N';		    
			document.getElementById('frmAnexosReuniao').src = frmAnexos;
		}
	}
	else
		btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);

	verificaEnvioPauta();
}"
                                                        ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e);
}"></ClientSideEvents>
                                                    <TabStyle Width="150px">
                                                    </TabStyle>
                                                </dxtc:ASPxPageControl>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td class="fomulario-botao">
                                                    <dxe:ASPxButton ID="btnEnviarPauta" runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarPauta"
                                                        ClientVisible="True" Text="Enviar Pauta"
                                                        ValidationGroup="MKE" Width="115px">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
                window.top.mostraConfirmacao(traducao.reunioes_a_reuni_o_ser__salva__deseja_continuar_, confirmaClickEnviarPauta, null);
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td class="formulario-botao">
                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                        Text="Salvar" ValidationGroup="MKE" Width="115px">
                                                        <ClientSideEvents Click="function(s, e) {
	//debugger
    e.processOnServer = false;
	capturaCodigosInteressados();
	if(verificarDadosPreenchidos())
    {
	  //if (window.onClick_btnSalvar)
	  //  onClick_btnSalvar();
       hfGeral.Set(&quot;StatusSalvar&quot;,&quot;0&quot;);
	  pnCallback.PerformCallback(TipoOperacao);
   } 
   else
		return false;
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td class="formulario-botao">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar2"
                                                        Text="Fechar" Width="115px" ID="btnFechar2">
                                                        <ClientSideEvents Click="function(s, e) {	
                                                            e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
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

        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
            OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
   //debugger
   verificaEnvioPauta();
	if(&quot;Excluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(traducao.reunioes_reuni_o_exclu_da_com_sucesso_);
        
	}
    else
	{
		if(tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1)
	    	btnEnviarPauta.SetText(traducao.reunioes_enviar_pauta);
		else
			btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);

		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
        {
			mostraDivSalvoPublicado(traducao.reunioes_reuni_o_salva_com_sucesso_);
            
        }
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		{
	        mostraDivSalvoPublicado(traducao.reunioes_dados_gravados_com_sucesso_);
           
        }
        else if(&quot;EnviarPauta&quot; == s.cp_OperacaoOk)
       {
			mostraDivSalvoPublicado(traducao.reunioes_pauta_enviada_com_sucesso_aos_participantes);
            
	   }
       else if(&quot;EnviarAta&quot; == s.cp_OperacaoOk)
       {
			mostraDivSalvoPublicado(traducao.reunioes_ata_enviada_com_sucesso_aos_participantes_);
            
       }		
       else if(&quot;Erro&quot; == s.cp_OperacaoOk)
       {
			window.top.mostraMensagem(hfGeral.Get(&quot;ErroSalvar&quot;), 'erro', true, false, null);
            
       }		
       if(&quot;Erro&quot; != s.cp_OperacaoOk)
	   {
	    	pcMensagemPauta.Hide();
			hfGeral.Set(&quot;TipoOperacao&quot;, traducao.reunioes_editar);
			onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
			//desabilitaHabilitaComponentes();
	   }
   }

}" />
        </dxcb:ASPxCallback>
        <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <GroupFooter Font-Bold="True">
                </GroupFooter>
                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter_pendencia" runat="server" GridViewID="gvPendencias"
            OnRenderBrick="ASPxGridViewExporter_pendencia_RenderBrick" PaperKind="A4">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <GroupFooter Font-Bold="True">
                </GroupFooter>
                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
