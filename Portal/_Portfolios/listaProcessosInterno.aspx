<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listaProcessosInterno.aspx.cs" Inherits="_Portfolios_listaProcessosInterno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>

    <script type="text/javascript">
        function processa(param) {
            hfGeral.Set('param', param);
            __doPostBack('WF', param);
        }

        function MontaCamposFormulario(valores) {
            var codigoInstanciaWf = valores[0];
            var codigoWorkflow = valores[1];
            var nomeProcessoWf = valores[2];

            var url1 = 'Relatorios/popupRelResumoProcessos.aspx?';
            url1 += 'cwf=' + codigoWorkflow;
            url1 += '&ciwf=' + codigoInstanciaWf;
            url1 += '&niwf=' + nomeProcessoWf;
            window.open(url1, 'form', 'resizable=0,width=900px,height=800px,status=no,menubar=no');
        }
        function mostraPopupGraficoProcesso(valores) {
            var CodigoInstanciaWf = valores[0];
            var CodigoWorkflow = valores[1];
            var CodigoFluxo = valores[3];
            var CodigoProjeto = valores[4];

            //var larguraPopup = screen.width;
            var larguraPopup = window.top.innerWidth;

            var url1 = "_Portfolios/GraficoProcessoInterno.aspx?";
            url1 += "CW=" + CodigoWorkflow;
            url1 += "&CF=" + CodigoFluxo;
            url1 += "&CI=" + CodigoInstanciaWf;
            url1 += "&AlturaEtapaPercorrida=" + (window.top.innerHeight - 250).toString();
            url1 += "&Altura=" + (window.top.innerHeight - 250).toString();
            url1 += "&Largura=" + (screen.width - 80).toString();
            url1 += "&Popup=S";
            url1 += "&CP=" + CodigoProjeto;

            var url = window.top.pcModal.cp_Path + url1;
            window.top.showModal2(url, traducao.listaProcessosInterno_gr_fico_do_processo, larguraPopup - 30, window.top.innerHeight - 60, atualizaGrid);

        }

        function mostraPopupFormulario(valores) {
            var IndicaEtapaDisjuncao = valores[6];
            if (IndicaEtapaDisjuncao == 'D') {
                mostraPopupGraficoProcesso(valores);
            }
            else {
                var CodigoInstanciaWf = valores[0];
                var CodigoWorkflow = valores[1];
                var CodigoFluxo = valores[2];
                var CodigoProjeto = valores[3];
                var OcorrenciaAtual = valores[4];
                var CodigoEtapaAtual = valores[5];

                var url1 = "CW=" + CodigoWorkflow;
                url1 += "&CI=" + CodigoInstanciaWf;
                url1 += "&CE=" + CodigoEtapaAtual;
                url1 += "&CS=" + OcorrenciaAtual;
                url1 += "&CF=" + CodigoFluxo;
                url1 += "&CP=" + CodigoProjeto;
                abreFluxo(url1);
            }
        }

        function abreInclusaoFluxo(CodigoFluxo, CodigoProjeto, acessoEtapaInicial, CodigoWorkflow) {

            var url1 = "CW=" + CodigoWorkflow;
            url1 += "&CF=" + CodigoFluxo;
            url1 += "&CP=" + CodigoProjeto;
            url1 += "&AEI=" + acessoEtapaInicial;

            abreFluxo(url1);
        }

        function abreFluxo(parametros) {
            var larguraTelaApp = screen.width <= 400 ? 900 : screen.width;
            var url1 = "./wfEngineInterno.aspx?" + parametros;
            url1 += "&Altura=" + (window.top.innerHeight - 222).toString();
            url1 += "&Largura=" + (larguraTelaApp - 80).toString();
            url1 += "&Popup=S";

            var url = window.top.pcModal.cp_Path + url1;

            window.top.showModal2(url, traducao.listaProcessosInterno_Formul_rio_Din_mico, larguraTelaApp - 30, window.top.innerHeight - 60, atualizaGrid);

        }

        function atualizaGrid(x) {
            var funcDescartarAlteracoesPendentes = function () {
                var frame = window.top.pcModal2.GetContentIFrameWindow().frames["wfFormularios"];
                if (frame) {
                    if (frame.frames['wfTela']) {
                        frame = frame.frames['wfTela'];
                    }
                    frame.existeConteudoCampoAlterado = false;
                }
                window.top.cancelaFechamentoPopUp2 = 'N';
                if (window.top.lpAguardeMasterPage)
                    window.top.lpAguardeMasterPage.Hide();
                window.top.fechaModal2();
            }
            var existeInformacoesPendentes = VerificaExistenciaInformacoesPendentes();
            if (existeInformacoesPendentes) {
                window.top.cancelaFechamentoPopUp2 = 'S';
                var textoMsg = traducao.listaProcessosInterno_existem_altera__es_ainda_n_o_salvas___br___br_ao_pressionar__ok___as_altera__es_n_o_salvas_ser_o_perdidas___br___br_deseja_continuar_;
                window.top.mostraMensagem(textoMsg, 'confirmacao', true, true, funcDescartarAlteracoesPendentes);
            }
            else {
                gvDados.Refresh();
            }
        }

        function VerificaExistenciaInformacoesPendentes() {
            var frame = window.top.pcModal2.GetContentIFrameWindow().frames["wfFormularios"];
            if (frame) {
                if (frame.frames['wfTela']) {
                    frame = frame.frames['wfTela'];
                }
                return frame.existeConteudoCampoAlterado;
            }
            return false;
        }

        function mostraPopupHistorico(valores) {
            var CodigoInstanciaWf = valores[0];
            var CodigoWorkflow = valores[1];

            var url1 = "_Portfolios/historicoProcessoInterno.aspx?";
            url1 += "CW=" + CodigoWorkflow;
            url1 += "&CI=" + CodigoInstanciaWf;

            var url = window.top.pcModal.cp_Path + url1;
            window.top.showModal(url, traducao.listaProcessosInterno_hist_rico, screen.width - 40, window.top.innerHeight - 120, '', atualizaGrid);
        }

        if (window.top.lpAguardeMasterPage)
            window.top.lpAguardeMasterPage.Hide();

    </script>

    <style type="text/css">
        .style1 {
            width: 10px;
            height: 10px;
        }

        .style2 {
            height: 10px;
        }

        .style3 {
            width: 10px;
        }
    </style>

</head>
<body style="margin: 0px; overflow: auto;">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnNovoFluxo" ImageSpacing="0px" HorizontalAlign="Left" Text="Novo Fluxo" Width="145px" TabIndex="2" ID="btnNovoFluxo" Visible="False">
                                    <Image AlternateText="Novo Fluxo" Url="~/imagens/botoes/incluirReg02.png"></Image>
                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>
                                 <div id="divGrid" style="visibility:hidden">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                    KeyFieldName="CodigoInstanciaWf;CodigoWorkflow;CodigoFluxo;CodigoProjeto;OcorrenciaAtual;CodigoEtapaAtual;IndicaEtapaDisjuncao" AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados"
                                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                    OnBeforeColumnSortingGrouping="gvDados_BeforeColumnSortingGrouping"
                                    OnCustomCallback="gvDados_CustomCallback"
                                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnBeforeGetCallbackResult="gvDados_BeforeGetCallbackResult" OnDataBound="gvDados_DataBound" OnPreRender="gvDados_PreRender">
                                    <ClientSideEvents CustomButtonClick="function(s, e) {
  s.cpCallbackMessage = &quot;&quot;;
   var funcObj = { funcaoOK: function(s,e){ s.PerformCallback(e.buttonID); } }
&nbsp;&nbsp; s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
	if (&quot;btnReverter&quot; == e.buttonID)
	{
		e.processOnServer = false;
window.top.mostraMensagem(traducao.listaProcessosInterno_ao_reverter_um_processo__ele_voltar__para_a_etapa_anterior_e_se_alguma_informa__o_foi_digitada_na_etapa_atual__ela_ser__perdida__continuar_, 'confirmacao', true, true, function(){funcObj['funcaoOK'](s,e)});
	}
	else if (&quot;btnCancelar&quot; == e.buttonID)
	{
		e.processOnServer = false;
window.top.mostraMensagem(traducao.listaProcessosInterno_n_o_h__como_reverter_o_cancelamento_de_um_processo__voc__deseja_realmente_cancelar_este_processo_ , 'confirmacao', true, true, function(){funcObj['funcaoOK'](s,e)});	
}
	else if(&quot;btnResumoProcesso&quot; == e.buttonID)
	{
		s.PerformCallback(e.buttonID); 
	}
	else if(&quot;btnGrafico&quot; == e.buttonID)
	{
		e.processOnServer = false;
        
	    mostraPopupGraficoProcesso(s.GetRowKey(e.visibleIndex).split('|'));
	}
	else if(&quot;btnInteragir&quot; == e.buttonID)
	{	
                e.processOnServer = false;	 
                mostraPopupFormulario(s.GetRowKey(e.visibleIndex).split('|'));
	}
                else if(&quot;btnHistorico&quot; == e.buttonID)
                {
                               e.processOnServer = false;                              
                               mostraPopupHistorico(s.GetRowKey(e.visibleIndex).split('|'));
                }
	else
		s.PerformCallback(e.buttonID); 
}"
                                        EndCallback="function(s, e) {
               if(s.cpCallbackMessage == &quot;export&quot;)
               {
		window.location =        &quot;../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=False&quot;;
                }    
               else if ( (s.cpCallbackMessage) &amp;&amp; (s.cpCallbackMessage.length&gt;0) )
	{
       		if(s.cp_status == &quot;ok&quot;)
			window.top.mostraMensagem(s.cpCallbackMessage, 'sucesso', false, false, null);
		else
			window.top.mostraMensagem(s.cpCallbackMessage, 'erro', true, false, null);
		s.cpCallbackMessage = &quot;&quot;;
	}
}"
                                        Init="function(s, e) {
	
    var height = Math.max(0, document.documentElement.clientHeight) - 15;
    s.SetHeight(height);
    document.getElementById('divGrid').style.visibility = '';
    window.top.lpAguardeMasterPage.Hide();

}"></ClientSideEvents>

                                    <SettingsPager PageSize="100" AlwaysShowPager="True"></SettingsPager>
                                    <Settings ShowHeaderFilterBlankItems="false"
                                        ShowGroupPanel="True"
                                        ShowFilterRow="True"
                                        HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"></Settings>
                                    <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True"
                                        AutoExpandAllGroups="True" AllowSelectByRowClick="True" FilterRowMode="OnClick"></SettingsBehavior>
                                   <SettingsResizing  ColumnResizeMode="Control"/>
                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>
                                    <SettingsPopup>
                                        <HeaderFilter Height="350px" Width="200px" />
                                    </SettingsPopup>
                                    <StylesEditors>
                                        <ButtonEdit></ButtonEdit>
                                    </StylesEditors>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#245;es" Name="A&#231;&#245;es"
                                            VisibleIndex="0" Width="190px">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnResumoProcesso"
                                                    Text="Imprimir Resumo de Tramitação">
                                                    <Image
                                                        Url="~/imagens/botoes/btnPDF.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnHistorico"
                                                    Text="Visualizar Histórico">
                                                    <Image Url="~/imagens/botoes/btnHistorico.png" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnGrafico"
                                                    Text="Visualizar Fluxo Graficamente">
                                                    <Image Url="~/imagens/botoes/fluxos.PNG" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCancelar" Text="Cancelar">
                                                    <Image
                                                        Url="~/imagens/botoes/excluirReg02.PNG" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnReverter" Text="Reverter">
                                                    <Image AlternateText="Reverter" Url="~/imagens/botoes/retornar.png" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnInteragir" Text="Interagir">
                                                    <Image AlternateText="Interagir" Url="~/imagens/botoes/interagir.png" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnUltimaEtapa"
                                                    Text="Visualizar Etapa Atual">
                                                    <Image Url="~/imagens/botoes/pFormulario.png" />
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <HeaderCaptionTemplate>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td align="center">
                                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                ClientInstanceName="menu"
                                                                ItemSpacing="0px" OnItemClick="menu_ItemClick"
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
                                            </HeaderCaptionTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Fluxo" FieldName="NomeFluxo" Name="colTipoFluxo"
                                            VisibleIndex="2" Width="200px" Visible="False">
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="V." FieldName="VersaoFluxo" Name="VersaoFluxo"
                                            ToolTip="Vers&#227;o" VisibleIndex="3" Width="40px">
                                            <Settings AllowHeaderFilter="False" AllowAutoFilter="False" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Nome do Fluxo" FieldName="NomeInstanciaWf"
                                            Name="colNomeFluxo" VisibleIndex="4" Width="350px">
                                            <PropertiesTextEdit DisplayFormatString="{0}">
                                            </PropertiesTextEdit>
                                            <Settings AllowHeaderFilter="False" AllowAutoFilter="True"
                                                AutoFilterCondition="Contains" ShowFilterRowMenu="False" />
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Etapa Anterior"
                                            FieldName="NomeEtapaAnterior" ShowInCustomizationForm="True" VisibleIndex="8"
                                            Width="180px">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False"
                                                AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="DataInicioInstancia"
                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="175px">
                                            <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_hora_csharp %>">
                                            </PropertiesDateEdit>
                                            <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Etapa Atual" FieldName="NomeEtapaAtual" VisibleIndex="7"
                                            Width="100px">
                                            <Settings AllowHeaderFilter="False" AllowAutoFilter="True"
                                                AutoFilterCondition="Contains" />
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" Name="colStatus"
                                            VisibleIndex="13" Width="50px" GroupIndex="0" SortIndex="0"
                                            SortOrder="Ascending">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" />
                                            <FilterCellStyle>
                                                <Paddings Padding="0px" />
                                            </FilterCellStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoWorkflow" Visible="False"
                                            VisibleIndex="9">
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoInstanciaWf" Visible="False"
                                            VisibleIndex="10">
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="OcorrenciaAtual" Visible="False"
                                            VisibleIndex="11">
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoEtapaAtual" Visible="False"
                                            VisibleIndex="12">
                                            <HeaderStyle />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Término"
                                            FieldName="DataTerminoInstancia" ShowInCustomizationForm="True"
                                            VisibleIndex="14" Width="175px">
                                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                            </PropertiesDateEdit>
                                            <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Solicitante" FieldName="UsuarioCriacaoInstancia"
                                            VisibleIndex="15" Width="200px">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="UltimaOcorrencia" Name="UltimaOcorrencia"
                                            UnboundExpression="1" Visible="False" VisibleIndex="18">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUltimaEtapa" Name="CodigoUltimaEtapa"
                                            UnboundExpression="1" Visible="False" VisibleIndex="19">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="IndicaSubFluxoAtual"
                                            FieldName="IndicaSubFluxoAtual" Name="IndicaSubFluxoAtual"
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="codigoEtapaAnterior"
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Nº Protocolo" FieldName="NumeroProtocolo" ShowInCustomizationForm="True" VisibleIndex="1" Width="120px">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Posso Interagir?" FieldName="IndicaInteracaoEtapaAtual" ShowInCustomizationForm="True" VisibleIndex="16" Width="200px">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                            <SettingsHeaderFilter Mode="CheckedList">
                                            </SettingsHeaderFilter>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Unidade de negócio" FieldName="NomeUnidadeNegocio" ShowInCustomizationForm="True" VisibleIndex="5" Width="250px">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True"
                                                AutoFilterCondition="Contains" />
                                            <SettingsHeaderFilter Mode="CheckedList">
                                            </SettingsHeaderFilter>
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Atraso?" FieldName="IndicaAtraso" ShowInCustomizationForm="True" VisibleIndex="23">
                                            <Settings AllowHeaderFilter="True" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="NumeroProtocolo" SummaryType="Count" ShowInColumn="NumeroProtocolo" Visible="false" /> 
                                    </TotalSummary>
                                    <Styles>
                                        <Header></Header>
                                        <EmptyDataRow></EmptyDataRow>
                                        <GroupPanel></GroupPanel>
                                        <HeaderPanel></HeaderPanel>
                                        <CommandColumn HorizontalAlign="Center">
                                        </CommandColumn>
                                        <CommandColumnItem>
                                            <Paddings Padding="0px" />
                                        </CommandColumnItem>
                                        <HeaderFilterItem>
                                        </HeaderFilterItem>
                                    </Styles>
                                </dxwgv:ASPxGridView>
                            </div>
                                     </dxp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>

        <dxcp:ASPxCallback ID="callbackCriaInstancia" runat="server"
            ClientInstanceName="callbackCriaInstancia"
            OnCallback="callbackCriaInstancia_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	abreFluxo(s.cp_Parametros);
}" />
        </dxcp:ASPxCallback>

        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
            ID="ASPxGridViewExporter1"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default></Default>
                <Header></Header>
                <Cell></Cell>
                <GroupFooter></GroupFooter>
                <Title></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>

    </form>
</body>
</html>
