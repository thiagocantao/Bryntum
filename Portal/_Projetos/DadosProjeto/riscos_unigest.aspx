<%@ Page Language="C#" AutoEventWireup="true" CodeFile="riscos_unigest.aspx.cs" Inherits="_Projetos_DadosProjeto_riscos_unigest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript">
        var pastaImagens = "../../imagens";
        var mostrarRelatorioRiscoSelecionado = false;


        var retornoPopUp = null;

        function onEndLocal_pnCallback() {
            onEnd_pnCallback();
        }

        function mostraRelatorioRiscoSelecionado(titulo) {
            cbRelatorioPDF.PerformCallback("export");
            //window.top.showModal('../Relatorios/RiscoQuestaoSelecionada.aspx?CRQ='+hfGeral.Get("CodigoRiscoQuestao"), titulo, screen.width - 60, screen.height - 260, '', null);
        }
        function abreRiscoQuestao() {

            btnRelatorioRisco.SetClientVisible(false);
            pcAbas.SetActiveTab(pcAbas.GetTab(0));
            onClickBarraNavegacao("Incluir", gvDados, pcDados);
            hfGeral.Set("TipoOperacao", "Incluir");
            desabilitaHabilitaComponentes();
        }

        function callbackPopupComentarios(comentario) {
            btnAcao_pcComentarioAcao.SetVisible(false);
            btnCancelar_pcComentarioAcao.SetVisible(true);
            btnCancelar.SetText("Fechar");
            mmComentarioAcao.SetText(comentario);
            mmComentarioAcao.SetEnabled(false);
            pcComentarioAcao.Show();
        }

    </script>
    <script type="text/javascript" language="javascript" src="../../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/_Strings.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style2 {
            width: 25px;
        }

        .style3 {
            width: 10px;
            height: 10px;
        }

        .style4 {
            height: 10px;
        }

        .style5 {
            width: 100px;
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="style3"></td>
                    <td class="style4">
                        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                            <Styles>
                                <Default Font-Names="Verdana" Font-Size="8pt">
                                </Default>
                                <Header Font-Names="Verdana" Font-Size="9pt">
                                </Header>
                                <Cell Font-Names="Verdana" Font-Size="8pt">
                                </Cell>
                                <GroupFooter Font-Bold="True" Font-Names="Verdana" Font-Size="8pt">
                                </GroupFooter>
                                <Title Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></Title>
                            </Styles>
                        </dxwgv:ASPxGridViewExporter>
                    </td>
                    <td class="style3"></td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            OnCallback="pnCallback_Callback" Width="100%" TabIndex="1">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- ASPxGRIDVIEW: gvDados -->
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoRiscoQuestao"
                                        AutoGenerateColumns="False" Width="100%" Font-Names="Verdana" Font-Size="8pt"
                                        ID="gvDados" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnCustomGroupDisplayText="gvDados_CustomGroupDisplayText" OnBeforeGetCallbackResult="gvDados_BeforeGetCallbackResult" OnDataBound="gvDados_DataBound" OnPreRender="gvDados_PreRender" OnCustomJSProperties="gvDados_CustomJSProperties">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
    //OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) {
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
        TipoOperacao = 'Consultar';
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
        desabilitaHabilitaComponentes();
		pcDados.Show();
     }
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="acao" Width="105px" Caption="A&#231;&#227;o"
                                                VisibleIndex="0">
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
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
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
                                            <dxwgv:GridViewDataTextColumn FieldName="CorRiscoQuestao" Name="CorRiscoQuestao"
                                                Width="60px" Caption="Grau" VisibleIndex="1">
                                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif'/&gt;"
                                                    EncodeHtml="False">
                                                </PropertiesTextEdit>
                                                <Settings AllowHeaderFilter="False" AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoRiscoQuestao" Name="DescricaoRiscoQuestao"
                                                Caption="Risco" VisibleIndex="2">
                                                <Settings AllowAutoFilterTextInputTimer="False" AllowAutoFilter="False" ShowFilterRowMenu="False"
                                                    AllowHeaderFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoStatusRiscoQuestao" Name="colStatus"
                                                Width="90px" Caption="Status" VisibleIndex="4">
                                                <Settings AllowHeaderFilter="False" />
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Name="Responsavel"
                                                Width="180px" Caption="Respons&#225;vel" VisibleIndex="5">
                                                <Settings AllowHeaderFilter="False" />
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ImpactoUrgencia" Name="col_ImpactoUrgencia"
                                                Caption="Impacto Urgencia" Visible="False" VisibleIndex="6">
                                                <Settings AllowHeaderFilter="False" />
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ProbabilidadePrioridade" Name="col_ProbabilidadePrioridade"
                                                Caption="Probabilidade Prioridade" Visible="False" VisibleIndex="7">
                                                <Settings AllowHeaderFilter="False" />
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataLimiteResolucao" Name="DataLimiteResolucao"
                                                Width="115px" Caption="Limite Elimina&#231;&#227;o" VisibleIndex="8">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="False" AllowHeaderFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataEliminacaoCancelamento" Name="DataEliminacaoCancelamento"
                                                Width="110px" Caption="Data Elimina&#231;&#227;o" VisibleIndex="9">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="False" AllowHeaderFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="Publicado" Name="DataPublicacao" Width="100px"
                                                Caption="Publicado" Visible="False" VisibleIndex="10">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel"
                                                Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="11">
                                                <Settings AllowHeaderFilter="False"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DetalheRiscoQuestao" Name="col_DetalheRisco"
                                                Caption="Detalhe" Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataStatusRiscoQuestao" Name="col_DataStatusRiscoQuestao"
                                                Caption="Data Status Risco Questao" Visible="False" VisibleIndex="13">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataPublicacao" Name="col_DataPublicacao"
                                                Caption="Data Publicacao" Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoRiscoQuestao" Name="col_DescricaoTipoRiscoQuestao"
                                                Caption="Descricao Tipo Risco Questao" Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Severidade" Name="col_Severidade" Caption="Severidade"
                                                Visible="False" VisibleIndex="16">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="IncluidoEmPor" Name="col_IncluidoEmPor"
                                                Caption="Incluido Em Por" Visible="False" VisibleIndex="17">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="PublicaoEmPor" Name="col_PublicaoEmPor"
                                                Caption="Publicao Em Por" Visible="False" VisibleIndex="18">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ConsequenciaRiscoQuestao" Name="col_ConsequenciaRiscoQuestao"
                                                Caption="Consequencia Risco Questao" Visible="False" VisibleIndex="19">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="TratamentoRiscoQuestao" Name="col_TratamentoRiscoQuestao"
                                                Caption="Tratamento Risco Questao" Visible="False" VisibleIndex="20">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Nome Programa/Projeto" FieldName="NomeProjeto"
                                                ShowInCustomizationForm="False" VisibleIndex="21" GroupIndex="0" SortIndex="0"
                                                SortOrder="Ascending" Visible="False">
                                                <Settings AllowDragDrop="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="IndicaPrograma" FieldName="IndicaPrograma"
                                                ShowInCustomizationForm="True" VisibleIndex="22" Visible="False">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Polaridade" FieldName="Polaridade" Name="colPolaridade"
                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="80px" Visible="False">
                                                <Settings AllowHeaderFilter="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoTipoRespostaRisco" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CustoRiscoQuestao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn FieldName="CodigoRiscoQuestaoSuperior" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Descricao Risco Questão Superior" FieldName="DescricaoRiscoQuestaoSuperior" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="TipoVariacaoCusto" FieldName="TipoVariacaoCusto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="27">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="ValorVariacaoCusto" FieldName="ValorVariacaoCusto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="28">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="CodigoMetaAfetada" FieldName="CodigoMetaAfetada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="DescricaoMetaAfetada" FieldName="DescricaoMetaAfetada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                            </dxtv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowHeaderFilterBlankItems="False" ShowGroupPanel="True" ShowFooter="True"
                                            VerticalScrollBarMode="Visible" GroupFormat="{1} {2}"></Settings>

                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                        </SettingsCommandButton>

                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Templates>
                                            <FooterRow>
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <img src="../../imagens/verdeMenor.gif" />
                                                            </td>
                                                            <td style="padding-right: 10px; padding-left: 5px">
                                                                <asp:Label ID="lblVerde" runat="server" Font-Size="7pt" Font-Names="Verdana" Text="Satisfatório"
                                                                    EnableViewState="False"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <img src="../../imagens/amareloMenor.gif" />
                                                            </td>
                                                            <td style="padding-right: 10px; padding-left: 5px">
                                                                <asp:Label ID="lblAmarelo" runat="server" Font-Size="7pt" Font-Names="Verdana" Text="Atenção"
                                                                    EnableViewState="False"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <img src="../../imagens/vermelhoMenor.gif" />
                                                            </td>
                                                            <td style="padding-right: 10px; padding-left: 5px">
                                                                <asp:Label ID="lblVermelho0" runat="server" Font-Size="7pt" Font-Names="Verdana"
                                                                    Text="Crítico" EnableViewState="False"></asp:Label>
                                                            </td>
                                                            <td style="padding-right: 5px; padding-left: 5px">
                                                                <asp:Label ID="lblMsgEmExecucao" runat="server" EnableViewState="False" Font-Bold="True"
                                                                    Font-Names="Verdana" Font-Size="7pt" Text="Somente são apresentadas informações de projetos em execução"
                                                                    Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </Templates>
                                        <TotalSummary>
                                            <dxtv:ASPxSummaryItem FieldName="DescricaoRiscoQuestao" ShowInColumn="DescricaoRiscoQuestao" SummaryType="Count" ValueDisplayFormat="{0} Item(ns)" Visible="False" />
                                        </TotalSummary>
                                    </dxwgv:ASPxGridView>
                                    <!-- PANEL CONTROL : pcDados -->
                                    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                        CloseAction="CloseButton" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupVerticalOffset="-13" ShowCloseButton="False"
                                        Width="774px" Height="135px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados">
                                        <ClientSideEvents CloseButtonClick="function(s, e) {
	if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"
                                            PopUp="function(s, e) {
    verificaVisibilidadeBotoes();
}"
                                            Shown="function(s, e) {
	//desabilitaHabilitaComponentes();
	btnSalvar.Focus();
}"
                                            CloseUp="function(s, e) {
	ddlResponsavel.SetValue(null);
	ddlResponsavel.SetText(&quot;&quot;);	
	ddlResponsavel.PerformCallback(); 
}"></ClientSideEvents>
                                        <ContentStyle>
                                            <Paddings PaddingBottom="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcAbas"
                                                    Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="pcAbas">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="tabPageRisco" Text="Risco">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                        <tr>
                                                                                            <td style="width: 50%">
                                                                                                <dxtv:ASPxLabel ID="lblRiscoQuestao" runat="server" ClientInstanceName="lblRiscoQuestao" Font-Names="Verdana" Font-Size="8pt" Text="Risco:">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="padding-left: 5px" id="tdRisco1" runat="server">
                                                                                                <dxtv:ASPxLabel ID="lblRiscoAssociado" runat="server" ClientInstanceName="lblRiscoAssociado" Font-Names="Verdana" Font-Size="8pt" Text="Risco (superior):">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 50%">
                                                                                                <dxtv:ASPxTextBox ID="txtRisco" runat="server" ClientInstanceName="txtRisco" Font-Names="Verdana" Font-Size="8pt" MaxLength="250" Width="100%">
                                                                                                    <DisabledStyle BackColor="#EBEBEB" Border-BorderColor="Silver" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                            <td style="padding-left: 5px" id="tdRisco2" runat="server">
                                                                                                <dxtv:ASPxCallbackPanel ID="pnRiscoAssociadoSuperior" runat="server" ClientInstanceName="pnRiscoAssociadoSuperior" OnCallback="pnRiscoAssociadoSuperior_Callback" Width="100%">
                                                                                                    <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                                                                                    <PanelCollection>
                                                                                                        <dxtv:PanelContent runat="server">
                                                                                                            <dxtv:ASPxComboBox ID="ddlRiscoAssociado" runat="server" ClientInstanceName="ddlRiscoAssociado" DisplayFormatString="{0}" Font-Names="Verdana" Font-Size="8pt" TextFormatString="{0}" ValueType="System.Int32" Width="100%">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxComboBox>
                                                                                                        </dxtv:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                    <Border BorderStyle="None" BorderWidth="0px" />
                                                                                                </dxtv:ASPxCallbackPanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px">
                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblAnotacoes" Font-Names="Verdana" Font-Size="8pt" Text="Descrição:">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxMemo ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao" Font-Names="Verdana" Font-Size="8pt" Height="45px" Width="100%">
                                                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                                                </ValidationSettings>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxMemo>
                                                                                                            <dxtv:ASPxLabel ID="lblContadorMemoDescricao" runat="server" ClientInstanceName="lblContadorMemoDescricao" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px;">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 80px">
                                                                                                    <dxtv:ASPxLabel ID="lblTipo" runat="server" ClientInstanceName="lblTipo" Font-Names="Verdana" Font-Size="8pt" Text="Tipo:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>

                                                                                                <td style="width: 100px">
                                                                                                    <dxtv:ASPxLabel ID="lblProbabilidadeUrgencia" runat="server" ClientInstanceName="lblProbabilidadeUrgencia" Font-Names="Verdana" Font-Size="8pt" Text="Probabilidade:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="width: 120px">
                                                                                                    <dxtv:ASPxLabel ID="lblImpactoPrioridade" runat="server" ClientInstanceName="lblImpactoPrioridade" Font-Names="Verdana" Font-Size="8pt" Text="Impacto:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td align="center" class="style2">&nbsp; </td>
                                                                                                <td align="left" id="tdRisco3" runat="server" style="width: 150px">
                                                                                                    <dxtv:ASPxLabel ID="lblTipoRespostaRisco" runat="server" ClientInstanceName="lblTipoRespostaRisco" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Resposta:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td align="left" id="tdRisco4" runat="server">
                                                                                                    <dxtv:ASPxLabel ID="lblCustoRisco" runat="server" ClientInstanceName="lblCustoRisco" Font-Names="Verdana" Font-Size="8pt" Text="Custo:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-right: 5px">
                                                                                                    <dxtv:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo" DisplayFormatString="{0}" Font-Names="Verdana" Font-Size="8pt" TextFormatString="{0}" Width="100%">
                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	atualizaImagemSeveridade();
}" />
                                                                                                        <Columns>
                                                                                                            <dxtv:ListBoxColumn Caption="Tipo" FieldName="DescricaoTipoRiscoQuestao" Width="100%" />
                                                                                                            <dxtv:ListBoxColumn Caption="Polaridade" FieldName="PolaridadeExtenso" Width="0%" />
                                                                                                        </Columns>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td style="padding-right: 5px">
                                                                                                    <dxtv:ASPxComboBox ID="ddlProbabilidade" runat="server" ClientInstanceName="ddlProbabilidade" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	atualizaImagemSeveridade();
}" />
                                                                                                        <Items>
                                                                                                            <dxtv:ListEditItem Text="Alta" Value="A" />
                                                                                                            <dxtv:ListEditItem Text="Média" Value="M" />
                                                                                                            <dxtv:ListEditItem Text="Baixa" Value="B" />
                                                                                                        </Items>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxComboBox ID="ddlImpacto" runat="server" ClientInstanceName="ddlImpacto" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	atualizaImagemSeveridade();
}" />
                                                                                                        <Items>
                                                                                                            <dxtv:ListEditItem Text="Alto" Value="A" />
                                                                                                            <dxtv:ListEditItem Text="Médio" Value="M" />
                                                                                                            <dxtv:ListEditItem Text="Baixo" Value="B" />
                                                                                                        </Items>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td align="left" class="style2" valign="top">
                                                                                                    <dxtv:ASPxImage ID="imgSeveridade" runat="server" ClientInstanceName="imgSeveridade" ImageUrl="~/imagens/Branco.gif">
                                                                                                    </dxtv:ASPxImage>
                                                                                                </td>
                                                                                                <td align="left" valign="top" id="tdRisco5" runat="server" style="padding-right: 5px">
                                                                                                    <dxtv:ASPxComboBox ID="ddlTipoRespostaRisco" runat="server" ClientInstanceName="ddlTipoRespostaRisco" DataSourceID="sdsTipoRespostaRisco" DisplayFormatString="{0}" Font-Names="Verdana" Font-Size="8pt" TextField="DescricaoRespostaRisco" TextFormatString="{0}" ValueField="CodigoTipoRespostaRisco" Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxComboBox>
                                                                                                </td>
                                                                                                <td align="left" valign="top" id="tdRisco6" runat="server">
                                                                                                    <dxtv:ASPxSpinEdit ID="spnCusto" runat="server" ClientInstanceName="spnCusto" DecimalPlaces="2" DisplayFormatString="c" EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" Number="0" Width="100%">
                                                                                                        <SpinButtons ClientVisible="False">
                                                                                                        </SpinButtons>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px;">
                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                        <tr>
                                                                                            <td style="width: 160px">
                                                                                                <dxtv:ASPxCheckBox ID="checkAfetaMeta" runat="server" CheckState="Unchecked" ClientInstanceName="checkAfetaMeta" Text="Afeta a Meta?" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Font-Names="Verdana" Font-Size="8pt">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                //debugger
	ddlMetas.SetVisible(s.GetChecked());
                if(s.GetChecked() == false)
                {
                       ddlMetas.SetValue(null);    
                }
                lblMetasAtivasProjeto.SetVisible(s.GetChecked());
}" />
                                                                                                </dxtv:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                                    <tr>
                                                                                                        <td style="width: 150px">
                                                                                                            <dxtv:ASPxLabel ID="lblMetasAtivasProjeto" runat="server" ClientInstanceName="lblMetasAtivasProjeto" Font-Names="Verdana" Font-Size="8pt" Text="Meta afetada do projeto:" ClientVisible="False">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxComboBox ID="ddlMetas" runat="server" ClientInstanceName="ddlMetas" ClientVisible="False" Width="100%" TextFormatString="{0}">
                                                                                                                <Columns>
                                                                                                                    <dxtv:ListBoxColumn FieldName="MetaDescritiva">
                                                                                                                    </dxtv:ListBoxColumn>
                                                                                                                    <dxtv:ListBoxColumn FieldName="NomeIndicador">
                                                                                                                    </dxtv:ListBoxColumn>
                                                                                                                </Columns>
                                                                                                            </dxtv:ASPxComboBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px;">
                                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                        <tr>
                                                                                            <td style="width: 160px">
                                                                                                <dxtv:ASPxCheckBox ID="checkAfetaOrcamento" runat="server" CheckState="Unchecked" ClientInstanceName="checkAfetaOrcamento" Text="Afeta o Orçamento?" Font-Names="Verdana" Font-Size="8pt">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
     lblTipoOrcamento.SetVisible(s.GetChecked());
     ddlTipoOrcamento.SetVisible(s.GetChecked());
     lblVariacaoOrcamento.SetVisible(s.GetChecked());
     spnValorOrcamento.SetVisible(s.GetChecked());
     if(s.GetChecked() == false)
     { 
            ddlTipoOrcamento.SetValue(null);
            spnValorOrcamento.SetValue(null);
     }
}" />
                                                                                                </dxtv:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>

                                                                                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                                    <tr>
                                                                                                        <td style="width: 150px">
                                                                                                            <dxtv:ASPxLabel ID="lblTipoOrcamento" runat="server" ClientInstanceName="lblTipoOrcamento" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Variação:" ClientVisible="False">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td style="width: 160px">
                                                                                                            <dxtv:ASPxComboBox ID="ddlTipoOrcamento" runat="server" ClientInstanceName="ddlTipoOrcamento" Font-Names="Verdana" Font-Size="8pt" Width="100%" ClientVisible="False">
                                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	atualizaImagemSeveridade();
}" />
                                                                                                                <Items>
                                                                                                                    <dxtv:ListEditItem Text="Acréscimo" Value="A" />
                                                                                                                    <dxtv:ListEditItem Text="Decréscimo" Value="D" />
                                                                                                                    <dxtv:ListEditItem Text="Remanejamento" Value="R" />
                                                                                                                </Items>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td style="width: 70px; padding-left: 5px;">
                                                                                                            <dxtv:ASPxLabel ID="lblVariacaoOrcamento" runat="server" ClientInstanceName="lblVariacaoOrcamento" Font-Names="Verdana" Font-Size="8pt" Text="Variação:" ClientVisible="False">
                                                                                                            </dxtv:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxSpinEdit ID="spnValorOrcamento" runat="server" ClientInstanceName="spnValorOrcamento" DecimalPlaces="2" DisplayFormatString="c" EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" Number="0" Width="100%" ClientVisible="False">
                                                                                                                <SpinButtons ClientVisible="False">
                                                                                                                </SpinButtons>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxtv:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px;">
                                                                                    <dxtv:ASPxLabel ID="ASPxLabel14" runat="server" ClientInstanceName="lblCodigoUsuarioResponsavel" Font-Names="Verdana" Font-Size="8pt" Text="Responsável:">
                                                                                    </dxtv:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                                        Width="100%" ClientInstanceName="ddlResponsavel" Font-Names="Verdana" Font-Size="8pt"
                                                                                        ID="ddlResponsavel" TextField="NomeUsuario" ValueField="CodigoUsuario" EnableCallbackMode="True"
                                                                                        OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition">
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                                            <dxe:ListBoxColumn Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                                        </Columns>
                                                                                        <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                                                                            ErrorText="*">
                                                                                            <RequiredField IsRequired="True" />
                                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 355px">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Inclu&#237;do em/por:" ClientInstanceName="lblOrigemTarefa"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel12">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="width: 10px"></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Publicado em/por:" ClientInstanceName="lblDescricaoTarefa"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel13">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIncluidoEmPor"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="txtIncluidoEmPor" ClientEnabled="False">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPublicadoEmPor"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="txtPublicadoEmPor" ClientEnabled="False">
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
                                                                                <td style="padding-top: 5px;" align="right">
                                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>

                                                                                                    <dxtv:ASPxImage ID="btnRelatorioRisco" runat="server" ClientInstanceName="btnRelatorioRisco" EnableClientSideAPI="True" ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="Gerar Relatório em PDF">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    mostraRelatorioRiscoSelecionado(s.cp_labelRiscoQuestao);
}" />
                                                                                                    </dxtv:ASPxImage>

                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnTransforma" runat="server" ClientInstanceName="btnTransforma" EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" TabIndex="1" Text="Transformar em Problema" Width="180px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    hfGeral.Set('TipoOperacao', 'Transformar');
    if (window.onClick_btnTransforma)
	    onClick_btnTransforma();    
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                                <td style="width: 10px"></td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" TabIndex="1" Text="Salvar" ValidationGroup="MKE" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();    
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                                <td style="width: 10px"></td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Font-Names="Verdana" Font-Size="8pt" Text="Fechar" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
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
                                                        <dxtc:TabPage Name="tabPageTratamento" Text="Tratamento">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <asp:Panel runat="server" Width="100%" ID="pnMemosTratamento">
                                                                        <table style="width: 100%" id="tblMemosTratamento" cellspacing="0" cellpadding="0"
                                                                            border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="height: 16px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Consequências:" ClientInstanceName="lblOrigemTarefa"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel6">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Height="87px" Width="100%" ClientInstanceName="txtConsequencia"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="txtConsequencia">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                        <dxe:ASPxLabel ID="lblContadorMemoConsequencia" runat="server" ClientInstanceName="lblContadorMemoConsequencia"
                                                                                            Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 5px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Estratégia para Tratamento:" ClientInstanceName="lblDescricaoTarefa"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel15">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Height="87px" Width="100%" ClientInstanceName="txtEstrategiaTratamento"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="txtEstrategiaTratamento">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                        <dxe:ASPxLabel ID="lblContadorMemoEstrategiaTratamento" runat="server" ClientInstanceName="lblContadorMemoEstrategiaTratamento"
                                                                                            Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Limite Elimina&#231;&#227;o:" ClientInstanceName="lblTerminoReal"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="lblLimiteEliminacaoResolucao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px"></td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblEsforcoReal"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel10">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px"></td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Elimina&#231;&#227;o/Canc.:" ClientInstanceName="lblStatusTarefa"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="lblEliminacaoResolucaoCancelamento">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit runat="server" Width="100%" ClientInstanceName="ddeLimiteEliminacao"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="ddeLimiteEliminacao">
                                                                                        <ValidationSettings ValidationGroup="MKE">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                        <CalendarProperties>
                                                                                            <DayHeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayWeekendStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayWeekendStyle>
                                                                                            <ButtonStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                        </CalendarProperties>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtStatus" Font-Names="Verdana"
                                                                                        Font-Size="8pt" ID="txtStatus" ClientEnabled="False">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDataElimincaoCancelado"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="txtDataElimincaoCancelado" ClientEnabled="False">
                                                                                        <MaskSettings IncludeLiterals="None"></MaskSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="style5"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEliminar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="Eliminar" Width="90px" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" ToolTip="Eliminar risco" ID="btnEliminar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	btnAcao_pcComentarioAcao.SetText(s.GetText());
	onBtnAcaoEliminar_Click();
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td style="width: 10px" align="right"></td>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="Cancelar" Width="90px" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" ToolTip="Cancelar risco" ID="btnCancelar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	btnAcao_pcComentarioAcao.SetText(s.GetText());
	onBtnAcaoCancelar_Click();
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td style="width: 10px" align="right"></td>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar2" EnableClientSideAPI="True"
                                                                                                        Text="Salvar" ValidationGroup="MKE" Width="90px" Font-Names="Verdana" Font-Size="8pt"
                                                                                                        TabIndex="1" ID="btnSalvar2">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();    
}"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td style="width: 10px" align="right"></td>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar2" Text="Fechar" Width="90px"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="btnFechar2">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100px"></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageComentario" Text="Coment&#225;rios">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    &nbsp;<table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvComentarios1" KeyFieldName="CodigoComentario"
                                                                                        AutoGenerateColumns="False" Width="100%" Font-Names="Verdana" Font-Size="8pt" EditFormLayoutProperties-EncodeHtml="false"
                                                                                        ID="gvComentarios1" OnRowUpdating="gvComentarios_RowUpdating" OnRowDeleting="gvComentarios_RowDeleting"
                                                                                        OnRowInserting="gvComentarios_RowInserting" OnCustomCallback="gvComentarios_CustomCallback"
                                                                                        OnAfterPerformCallback="gvComentarios1_AfterPerformCallback" OnCustomErrorText="gvComentarios1_CustomErrorText">

                                                                                        <EditFormLayoutProperties EncodeHtml="False"></EditFormLayoutProperties>
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="90px" VisibleIndex="0"
                                                                                                ShowEditButton="true" ShowDeleteButton="true" Name="colunaControlesOriginal" Visible="False">
                                                                                                <HeaderTemplate>
                                                                                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvComentarios1.AddNewRow();"" style=""cursor: pointer;""/>")%>
                                                                                                </HeaderTemplate>
                                                                                            </dxwgv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataMemoColumn FieldName="DescricaoComentario" Name="DescricaoComentario"
                                                                                                Caption="Coment&#225;rio" VisibleIndex="2">
                                                                                                <PropertiesMemoEdit Rows="10" ClientInstanceName="txtComentario">
                                                                                                    <ClientSideEvents Init="function(s, e) {
	document.getElementById('spC').innerText = s.GetText().length;
	return setMaxLength(s.GetInputElement(), 2000);
}"
                                                                                                        KeyDown="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    document.getElementById('spC').innerText = s.GetText().length;
}"
                                                                                                        KeyPress="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    document.getElementById('spC').innerText = s.GetText().length;
}"
                                                                                                        KeyUp="function(s, e) {
	if (e.htmlEvent.keyCode === 86 &amp;&amp; e.htmlEvent.ctrlKey)
    {
		var textoCompleto = s.GetText();
		var textoCompletoLimitado = textoCompleto.substr(0,2000);
        s.SetText(textoCompletoLimitado);
    }
    document.getElementById('spC').innerText = s.GetText().length;
}" />
                                                                                                    <Style Font-Names="Verdana" Font-Size="8pt"></Style>
                                                                                                </PropertiesMemoEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Caption="Comentários: (&lt;span id='spC'&gt;0&lt;/span&gt; de 2000) "></EditFormSettings>
                                                                                                <DataItemTemplate>
                                                                                                    <%# (Eval("DescricaoComentario").ToString().Length > 60) ? Eval("DescricaoComentario").ToString().Substring(0, 60) + "..." : Eval("DescricaoComentario").ToString() %>
                                                                                                </DataItemTemplate>
                                                                                            </dxwgv:GridViewDataMemoColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" ReadOnly="True" Name="NomeUsuario"
                                                                                                Caption="Respons&#225;vel" VisibleIndex="3">
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="DataComentario" ReadOnly="True" Name="DataComentario"
                                                                                                Width="90px" Caption="Data" VisibleIndex="4">
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="False" VisibleIndex="1" Width="90px" Name="colunaControlesComentario" Visible="False">
                                                                                                <CustomButtons>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnFormularioCustomComentario">
                                                                                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                </CustomButtons>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"
                                                                                            ConfirmDelete="True"></SettingsBehavior>
                                                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
    if(e.buttonID == 'btnFormularioCustomComentario')
    {	
          s.GetRowValues(s.GetFocusedRowIndex(), 'DescricaoComentario', callbackPopupComentarios);
    }
}" />
                                                                                        <SettingsPager Mode="ShowAllRecords">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                                                                                        </SettingsEditing>

                                                                                        <SettingsCommandButton>
                                                                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                                                        </SettingsCommandButton>

                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <Settings ShowGroupButtons="False" VerticalScrollBarMode="Visible" VerticalScrollableHeight="213"></Settings>
                                                                                        <SettingsText PopupEditFormCaption="Coment&#225;rio"></SettingsText>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 5px" align="right"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar3" Text="Fechar" Width="90px"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="btnFechar3">
                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageToDoList" Text="Plano de A&#231;&#227;o">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table id="htmltablePlanoAcao" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td id="linha1TabPlanoAcao"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar4" Text="Fechar" Width="90px"
                                                                                        Font-Names="Verdana" Font-Size="8pt" ID="btnFechar4">
                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageAnexo" Text="Anexos">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <iframe id="frmAnexos" frameborder="0" height="255px" scrolling="no" src="" width="100%"></iframe>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align='right'>
                                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar5" Text="Fechar" Width="90px"
                                                                                    Font-Names="Verdana" Font-Size="8pt" ID="btnFechar5">
                                                                                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)
}"></ClientSideEvents>
                                                </dxtc:ASPxPageControl>
                                                &nbsp;
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfAcao" ID="hfAcao">
                                    </dxhf:ASPxHiddenField>
                                    <dxcb:ASPxCallback ID="cbRelatorioPDF" runat="server" ClientInstanceName="cbRelatorioPDF"
                                        OnCallback="cbRelatorioPDF_Callback">
                                        <ClientSideEvents EndCallback="function(s, e) {
	window.location = &quot;../../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=false&quot;;
}" />
                                    </dxcb:ASPxCallback>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {

		onEndLocal_pnCallback();
		atualizaImagemSeveridade();

		// se o callback foi em virtude de uma ação de eliminação ou cancelamento do risco
		var acao = hfAcao.Get(&quot;Acao&quot;);
		if ( (null != acao) &amp;&amp; (&quot;&quot; != acao) )
			onPnCallBack_EndCallback_Acao();

    	if(pnCallback.cp_FechaEdicao == &quot;S&quot;)
      		pcDados.Hide();
	
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                        <dxpc:ASPxPopupControl ID="pcComentarioAcao" runat="server" ClientInstanceName="pcComentarioAcao"
                            Width="520px" HeaderText="Comentário Para a Ação" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Font-Names="Verdana"
                            Font-Size="8pt">
                            <ClientSideEvents Shown="function(s, e) {
	// seta o foco para o campo de coment&#225;rio;
	mmComentarioAcao.Focus();
}"></ClientSideEvents>
                            <ContentCollection>
                                <dxpc:PopupControlContentControl runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>&nbsp;<dxe:ASPxLabel runat="server" ClientInstanceName="lblComentarioAcao" EnableClientSideAPI="True"
                                                    Font-Names="Verdana" Font-Size="8pt" ID="lblComentarioAcao">
                                                </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo runat="server" Height="100px" Width="100%" ClientInstanceName="mmComentarioAcao"
                                                        EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" ID="mmComentarioAcao">
                                                        <ValidationSettings ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblContadorMemoComentarioAcao" runat="server" ClientInstanceName="lblContadorMemoComentarioAcao"
                                                        Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAcao_pcComentarioAcao"
                                                                                        EnableClientSideAPI="True" Text="xxx" Width="120px" Font-Names="Verdana" Font-Size="8pt"
                                                                                        ID="btnAcao_pcComentarioAcao">
                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	onBtnAcao_pcComentarioAcao_Click();
}"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td style="width: 10px"></td>
                                                                                <td align="right" style="padding-left: 10px">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar_pcComentarioAcao"
                                                                                        EnableClientSideAPI="True" Text="Fechar" Width="120px" Font-Names="Verdana" Font-Size="8pt"
                                                                                        ID="btnCancelar_pcComentarioAcao">
                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcComentarioAcao.Hide();	
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
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                            <ContentStyle>
                                <Paddings Padding="5px" />
                            </ContentStyle>
                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                        </dxpc:ASPxPopupControl>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsTipoRespostaRisco" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [CodigoTipoRespostaRisco]
      ,[DescricaoRespostaRisco]
  FROM [TipoRespostaRisco]

"></asp:SqlDataSource>
    </form>
</body>
</html>
