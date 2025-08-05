<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnaliseProjeto.aspx.cs" Inherits="AnaliseProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Análise</title>
    <script type="text/javascript" language="javascript">
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            return mensagemErro_ValidaCamposFormulario;
        }
        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        // **************************************************************************************
        // - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
        // **************************************************************************************

        function LimpaCamposFormulario() {
            htmlAnalise.SetHtml("");

            desabilitaHabilitaComponentes();
        }

        function MontaAnalise(valor) {
            var antes = htmlAnalise.GetHtml();
            htmlAnalise.SetHtml(antes + '<br>' + valor)
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            var linha = grid.GetFocusedRowIndex() == -1 ? 0 : grid.GetFocusedRowIndex();
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(linha, 'CodigoAnalisePerformance;Analise;', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(valores) {
            var values = valores[0];
            desabilitaHabilitaComponentes();

            var codigoAnalise = (values[0] != null ? values[0] : "");
            var analise = (values[1] != null ? values[1] : "");
            var maxLength = 8000;
            //            if (values[1] == null)
            //                lblCount.SetText('0 de ' + maxLength);
            //            else
            //                lblCount.SetText(values[1].length + ' de ' + maxLength);

            htmlAnalise.SetHtml(analise);
            pcDados.Show();
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            htmlAnalise.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            //            if ((window.TipoOperacao && TipoOperacao == "Consultar") || gvDados.cp_ExisteAnaliseOrcamento == "N") {
            //                document.getElementById('tdAnalise').style.display = 'none';
            //                document.getElementById('tdAnalise1').style.display = 'none';
            //                htmlAnalise.SetWidth('1000px');

            //            } else {
            //                document.getElementById('tdAnalise').style.display = 'block';
            //                document.getElementById('tdAnalise1').style.display = 'block';
            //                htmlAnalise.SetWidth('500px');
            //            }

        }
        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            onClick_btnCancelar();
        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            //            else
            //                lblCount.SetText(text.length + ' de ' + maxLength);
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObjeto(s, e) {
            try {
                var maxLength = 8000;
                //lblCount.SetText(s.GetText().length + ' de ' + maxLength);
                return setMaxLength(s.GetInputElement(), maxLength);
            }
            catch (e) { }
        }

        var execucaoSalvar;
        var textoFocus = '';

        function salvarAutomatico() {

            execucaoSalvar = setTimeout(function () { if (textoFocus != htmlAnalise.GetHtml()) { gvDados.PerformCallback('S'); } else salvarAutomatico(); }, 30000);
        }

        function cancelaSalvarAutomatico() {

            clearTimeout(execucaoSalvar);
        }

        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 20;
            gvDados.SetHeight(height);
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
    </script>
</head>
<body class="body" enableviewstate="false" style="margin: 0px">
    <form id="form1" runat="server" enableviewstate="false">
        <!-- TABLA CONTEUDO -->
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td></td>
                <td style="height: 10px"></td>
                <td style="width: 10px"></td>
            </tr>
            <tr>
                <td style="width: 10px"></td>
                <td id="tdpnCallback">
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                    ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
                                    LeftMargin="50" RightMargin="50"
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
                                <div id="divGrid" style="visibility: hidden">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAnalisePerformance"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                        OnCustomCallback="gvDados_CustomCallback">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) 
{
     //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
               btnSalvar.SetVisible(true); 
                s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                 s.GetSelectedFieldValues('CodigoAnalisePerformance;Analise;', MontaCamposFormulario);
                 hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
	             TipoOperacao = &quot;Editar&quot;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		
                 s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                 s.GetSelectedFieldValues('CodigoAnalisePerformance;Analise;', MontaCamposFormulario);
                 hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
	             TipoOperacao = &quot;Consultar&quot;
                 btnSalvar.SetVisible(false);
     }	
}
"
                                            EndCallback="function(s, e) {
	if(s.cp_Salvar == 'S')
	{	
        hfGeral.Set(&quot;TipoOperacao&quot;, s.cp_Operacao);
		TipoOperacao = s.cp_Operacao;
		textoFocus = htmlAnalise.GetHtml();
        if(s.cp_Msg != '')
            window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);

    	salvarAutomatico();
	}
}"   Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                                            <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DataAnalisePerformance" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Width="25%" ExportWidth="250">
                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="colResponsavel" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="37.5%" UnboundExpression="IsNull(UsuarioAlteracao, UsuarioInclusao)"
                                                UnboundType="String" ExportWidth="240">
                                                <Settings AutoFilterCondition="Contains" />
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Analise" Name="Analise" Caption="Análise"
                                                VisibleIndex="3" ExportWidth="350" Width="37.5%">
                                                <Settings AutoFilterCondition="Contains" />
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAnalisePerformance" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSort="false" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                            ShowGroupPanel="True"></Settings>
                                        <SettingsText></SettingsText>
                                    </dxwgv:ASPxGridView>
                                </div>
                                <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                    HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False">
                                    <ContentStyle>
                                        <Paddings Padding="5px" />
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True" />
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td id="tdAnalise">&nbsp;<dxe:ASPxLabel ID="lblAnalise" runat="server" ClientInstanceName="lblAnalise"
                                                                                ClientVisible="False" Text="Histório de Análises Orçamento:">
                                                                            </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td id="tdHtmlAnalise">
                                                                                <dxe:ASPxLabel runat="server" Text="Análise Crítica: " ClientInstanceName="lblComentariosGerais"
                                                                                    ID="lblComentariosGerais">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="tdAnalise1" valign="top" style="padding-right: 10px">
                                                                                <dxwgv:ASPxGridView ID="gvAnalises" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvAnalises"
                                                                                    KeyFieldName="CausasProblemas" Style="margin-top: 0px"
                                                                                    Width="550px" ClientVisible="False">
                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
     
	 var key = s.GetRowKey(e.visibleIndex);
        
     MontaAnalise(key);
}" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                            Width="30px" Caption=" ">
                                                                                            <CustomButtons>
                                                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnSelecao" Text="Selecionar">
                                                                                                    <Image Url="~/imagens/botoes/aditado.png">
                                                                                                    </Image>
                                                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                                            </CustomButtons>
                                                                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewCommandColumn>
                                                                                        <dxwgv:GridViewDataTextColumn Caption="Nome CR" FieldName="NomeCR" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="1">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn Caption="Período" FieldName="periodo" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="0" Width="60px">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn Caption="Análise Crítica" FieldName="CausasProblemas"
                                                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowSort="False" />
                                                                                    <SettingsPager AlwaysShowPager="True" Position="TopAndBottom" Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                                                    <Settings VerticalScrollableHeight="375" VerticalScrollBarMode="Visible" />
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                            <td id="tdHtmlAnalise1">
                                                                                <dxhe:ASPxHtmlEditor ID="htmlAnalise" runat="server" ClientInstanceName="htmlAnalise"
                                                                                    Height="300px" Width="600px">
                                                                                    <ClientSideEvents GotFocus="function(s, e) {
    cancelaSalvarAutomatico();
	textoFocus = s.GetHtml();
    salvarAutomatico();
	
}" />
                                                                                    <Toolbars>
                                                                                        <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
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
                                                                                                <dxhe:ToolbarInsertImageDialogButton>
                                                                                                </dxhe:ToolbarInsertImageDialogButton>
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
                                                                                                <dxhe:ToolbarFullscreenButton BeginGroup="True">
                                                                                                </dxhe:ToolbarFullscreenButton>
                                                                                            </Items>
                                                                                        </dxhe:HtmlEditorToolbar>
                                                                                        <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                                                                                            <Items>
                                                                                                <dxhe:ToolbarParagraphFormattingEdit Visible="False" Width="120px">
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
                                                                                                <dxhe:ToolbarFontNameEdit Visible="False">
                                                                                                    <Items>
                                                                                                        <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                                                        <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                                                        <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                                                        <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                                                        <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                                                        <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                                                    </Items>
                                                                                                </dxhe:ToolbarFontNameEdit>
                                                                                                <dxhe:ToolbarFontSizeEdit Visible="False">
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
                                                                                                <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                                                                </dxhe:ToolbarBackColorButton>
                                                                                                <dxhe:ToolbarFontColorButton>
                                                                                                </dxhe:ToolbarFontColorButton>
                                                                                            </Items>
                                                                                        </dxhe:HtmlEditorToolbar>
                                                                                    </Toolbars>
                                                                                    <Settings AllowHtmlView="False" AllowInsertDirectImageUrls="False" AllowPreview="False" />
                                                                                    <SettingsLoadingPanel Text="Carregando" />

                                                                                    <SettingsHtmlEditing>
                                                                                        <PasteFiltering Attributes="class"></PasteFiltering>
                                                                                    </SettingsHtmlEditing>

                                                                                    <SettingsValidation ErrorText="O conteúdo HTML é inválido">
                                                                                    </SettingsValidation>
                                                                                    <SettingsDialogs>
                                                                                        <InsertImageDialog ShowStyleSettingsSection="False" ShowInsertFromWebSection="False" ShowMoreOptionsButton="False" ShowSaveFileToServerButton="False">
                                                                                            <SettingsImageUpload AdvancedUploadModeTemporaryFolder="~/ArquivosTemporarios/" UploadFolder="~/userimagens/">
                                                                                                <ValidationSettings GeneralErrorText="Upload do arquivo falhou devido a um erro externo" InvalidUrlErrorText="URL em formato inválido ou um arquivo não pode ser encontrado usando essa URL" MaxFileSizeErrorText="O tamanho do arquivo excede o tamanho máximo permitido, que é {0} bytes" MultiSelectionErrorText="Atenção! {0} arquivos são inválidos e não serão carregados. Possível razão: eles excedem o tamanho de arquivo permitido ({1}), as extensões não são permitidos, ou os nomes dos arquivos contém caracteres inválidos. Estes arquivos foram removidos da seleção: {2}" NotAllowedFileExtensionErrorText="Essa extensão de arquivo não é permitida">
                                                                                                </ValidationSettings>
                                                                                            </SettingsImageUpload>
                                                                                            <SettingsImageSelector>
                                                                                                <CommonSettings RootFolder="~/userimagens/" UseAppRelativePath="false" />
                                                                                                <UploadSettings Enabled="True">
                                                                                                    <AdvancedModeSettings TemporaryFolder="~\ArquivosTemporarios\">
                                                                                                    </AdvancedModeSettings>
                                                                                                </UploadSettings>
                                                                                                <BreadcrumbsSettings Visible="False" />
                                                                                            </SettingsImageSelector>
                                                                                        </InsertImageDialog>
                                                                                    </SettingsDialogs>
                                                                                </dxhe:ASPxHtmlEditor>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-top: 10px">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                            Text="Salvar" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	cancelaSalvarAutomatico();
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                            <Paddings Padding="0px" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                            Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	cancelaSalvarAutomatico();
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                PaddingTop="0px" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server"
                                    ClientInstanceName="hfDadosSessao">
                                </dxhf:ASPxHiddenField>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) 
{
              gvDados.Refresh();	
               if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Análise incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Análise alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Análise excluída com sucesso!&quot;);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td style="width: 10px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
