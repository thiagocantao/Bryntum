<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaIndicadores.aspx.cs" Inherits="_VisaoMaster_ListaIndicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mapa de Indicadores</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script language="javascript" type="text/javascript">
        window.moveTo(0, 0);
        if (document.all) {
            top.window.resizeTo(screen.availWidth, screen.availHeight);
        }
        else {
            if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }

                try {
                    top.window.resizeTo(screen.availWidth, screen.availHeight);
                } catch (e) {
                }
            }
        }

            var colName;
            function OnItemClick(s, e) {
                if (e.item.name == 'HideColumn') {
                    gvDados.PerformCallback(colName);
                    colName = null;
                }
                else {
                    if (gvDados.IsCustomizationWindowVisible())
                        gvDados.HideCustomizationWindow();
                    else
                        gvDados.ShowCustomizationWindow();
                }
            }

            function OnContextMenu(s, e) {
                if (e.objectType == 'header') {
                    colName = s.GetColumn(e.index).fieldName;
                    headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'NomeIndicador' || colName == 'CodigoIndicador' || colName == '' ? false : true));
                    headerMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
                }
            }

            function abreGraficoResultados(codigoIndicador, codigoProjeto, codigoMeta) {
                window.showModal('./Graficos/vm_020.aspx?CI=' + codigoIndicador + '&CP=' + codigoProjeto + '&CM=' + codigoMeta + '&altura=500', 'Resultados', 920, 500, '', null);
            }

            function abreHistoricoAnalises(codigoIndicador, codigoProjeto, codigoMeta) {
                window.showModal('./HistoricoAnalises.aspx?CI=' + codigoIndicador + '&CP=' + codigoProjeto + '&CM=' + codigoMeta + '&altura=580', 'Histórico de Análises', 920, 580, '', null);
            }

            function abreEdicaoAnalises(codigoIndicador, codigoProjeto, codigoMeta, ano, mes) {
                window.showModal('./HistoricoAnalises.aspx?CI=' + codigoIndicador + '&CP=' + codigoProjeto + '&CM=' + codigoMeta + '&Ano=' + ano + '&Mes=' + mes + '&altura=550', 'Análise', (screen.availWidth - 50), 550, posModal, null);
            }

            function abrePlanoAcao(codigoIndicador, codigoProjeto, codigoMeta) {
                window.showModal('./PlanoAcaoIndicador.aspx?CI=' + codigoIndicador + '&CP=' + codigoProjeto + '&CM=' + codigoMeta + '&altura=550', 'Plano de Ação', 950, 550, '', null);
            }

            function posModal(lParam) {
                gvDados.PerformCallback();
            }

            var myObject = null;
            var posExecutar = null;
            var urlModal = "";
            var cancelaFechamentoPopUp = 'N';
            var retornoModal = null;
            
            function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
                if (parseInt(sHeight) < 535)
                    sHeight = parseInt(sHeight) + 20;

                myObject = objParam;
                posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;
                objFrmModal = document.getElementById('frmModal');

                pcModal.SetWidth(sWidth);
                objFrmModal.style.width = "100%";
                objFrmModal.style.height = sHeight + "px";
                urlModal = sUrl;
                //setTimeout ('alteraUrlModal();', 0);            
                pcModal.SetHeaderText(sHeaderTitulo);
                pcModal.Show();

            }

            function fechaModal() {
                pcModal.Hide();
            }

            function resetaModal() {
                objFrmModal = document.getElementById('frmModal');
                posExecutar = null;
                objFrmModal.src = "";
                pcModal.SetHeaderText("");
                retornoModal = null;
            }
    </script>
    <style type="text/css">
        .style1
        {
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="headerGrid">
            <tr>
                <td>
    
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
            ClientInstanceName="gvDados"  
            KeyFieldName="CodigoIndicador" Width="100%" oncustomcallback="gvDados_CustomCallback" 
                        onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize" 
                        onhtmldatacellprepared="gvDados_HtmlDataCellPrepared" 
                        EnableRowsCache="False" EnableViewState="False">
            <ClientSideEvents ContextMenu="OnContextMenu" Init="function(s, e) {
                var height = 500;
                var ua = navigator.userAgent.toLowerCase();
                
                if (ua.indexOf('chrome') != -1) {
                    height = Math.max(0, screen.availHeight);            
                    height = height - 145;
                }
                else {
                    height = Math.max(0, document.documentElement.clientHeight);            
                    height = height - 85;
           
                }

                gvDados.SetHeight(height);
	
}" />
            <SettingsText CustomizationWindowCaption="Colunas Disponíveis" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CodigoIndicador" ShowInCustomizationForm="False" 
                    VisibleIndex="0" Width="105px" Name="CodigoIndicador">
                    <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                        AllowHeaderFilter="False" AllowSort="False" />
                        <DataItemTemplate>
                            <%# getBotoes() %>
                    </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                        <HeaderTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                OnClick="ImageButton1_Click" ToolTip="Exportar para excel" />
                        </HeaderTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="Grupo" GroupIndex="0" 
                    ShowInCustomizationForm="False" SortIndex="0" SortOrder="Ascending" 
                    VisibleIndex="1" Width="350px">
                    <Settings AllowAutoFilter="False" AllowDragDrop="False" 
                        AllowHeaderFilter="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataCheckColumn Caption="Prioritário" ExportWidth="100" 
                    FieldName="IndicadorMetaOperacionalPrioritaria" VisibleIndex="2" 
                    Width="75px" Visible="False">
                    <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="Não" 
                        NullDisplayText="Não" ValueChecked="S" ValueType="System.String" 
                        ValueUnchecked="N">                        
                    </PropertiesCheckEdit>
                    <Settings FilterMode="DisplayText" />
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ck_Marcado" runat="server" 
                            ClientInstanceName="ck_Marcado" ValueChecked="S" ValueType="System.String" 
                            ValueUnchecked="N">
                        </dxe:ASPxCheckBox>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataCheckColumn>
                <dxwgv:GridViewDataTextColumn Caption="Descrição do Indicador" VisibleIndex="3" 
                    Width="350px" FieldName="NomeIndicador" ExportWidth="400">
                    <Settings 
                        AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Sítio" VisibleIndex="4" Width="120px" 
                    FieldName="NomeSitio" ExportWidth="150">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Resultado" VisibleIndex="5" 
                    Width="125px" FieldName="Resultado" ExportWidth="130">
                    <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                        AllowSort="False" />
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="6" Width="40px" 
                    FieldName="StatusIndicador" ExportWidth="60" Name="StatusIndicador">
                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' /&gt;">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="True" 
                        AllowSort="False" FilterMode="DisplayText" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Avaliado" VisibleIndex="7" Width="115px" 
                    FieldName="Avaliado" ExportWidth="150">
                    <Settings AllowAutoFilter="True" AllowGroup="True" AllowHeaderFilter="False" 
                        AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Gestor Responsável" VisibleIndex="8" 
                    Width="135px" FieldName="NomeGestor" ExportWidth="250">
                    <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Periodicidade" 
                    VisibleIndex="9" Width="90px" FieldName="PeriodicidadeAtualizacao" 
                    ExportWidth="130">
                    <Settings AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Métrica" Visible="False" 
                    VisibleIndex="10" Width="350px" FieldName="Metrica" ExportWidth="350" 
                    Name="Metrica">
                    <PropertiesTextEdit EncodeHtml="False">
                    </PropertiesTextEdit>
                    <Settings AllowGroup="False" AllowHeaderFilter="False" AllowSort="False" 
                        AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Origem do Dado" Visible="False" 
                    VisibleIndex="11" Width="200px" FieldName="OrigemDado" ExportWidth="200">
                    <Settings AllowGroup="False" AllowSort="False" AutoFilterCondition="Contains" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Ano Análise" Visible="False" 
                    VisibleIndex="12" Width="90px" FieldName="AnoResultado">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mês Análise" FieldName="MesResultado" 
                    Visible="False" VisibleIndex="13" Width="90px">
                    <PropertiesTextEdit DisplayFormatString="{0:D2}">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Última Análise" 
                    FieldName="UltimaAnalise" Visible="False" VisibleIndex="14" Width="350px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Última Recomendação" 
                    FieldName="UltimaRecomendacao" Visible="False" VisibleIndex="15" 
                    Width="350px">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AutoExpandAllGroups="True" AllowFocusedRow="True" EnableCustomizationWindow="true" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" 
                HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" 
                ShowHeaderFilterBlankItems="False" />
            <SettingsPopup>
                <CustomizationWindow Height="300px" HorizontalAlign="NotSet" 
                    VerticalAlign="NotSet" Width="190px" />
            </SettingsPopup>
            <Styles>
                <Header Wrap="True">
                </Header>
                <HeaderPanel HorizontalAlign="Left">
                </HeaderPanel>
            </Styles>
            <StylesPopup>
                <CustomizationWindow>
                    <MainArea HorizontalAlign="Left">
                    </MainArea>
                    <PopupControl HorizontalAlign="Left">
                    </PopupControl>
                    <Content HorizontalAlign="Left">
                    </Content>
                    <Header HorizontalAlign="Left">
                    </Header>
                </CustomizationWindow>
            </StylesPopup>
        </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <dxe:ASPxLabel ID="lblDataReferencia" runat="server" Font-Italic="True">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                                    <dxe:ASPxButton ID="btnFechar2" runat="server" 
                        AutoPostBack="False" ClientInstanceName="btnFechar2"
                                         Text="Fechar" 
                        Width="100px">
                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.close();
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
        </dxhf:ASPxHiddenField>
    <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu" 
            >
            <Items>
                <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
                </dxm:MenuItem>
                <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
                </dxm:MenuItem>
            </Items>
         <ClientSideEvents ItemClick="OnItemClick"/>
        </dxm:ASPxPopupMenu>
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" 
            gridviewid="gvDados" onrenderbrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:aspxgridviewexporter>
        <dxcb:ASPxCallback ID="callbackSalvarMarcacao" runat="server" 
            ClientInstanceName="callbackSalvarMarcacao">
        </dxcb:ASPxCallback>
        <dxpc:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal"
            HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0" src="" style="overflow:auto; padding:0px; margin:0px;"></iframe></dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents PopUp="function(s, e) {
    window.document.getElementById('frmModal').dialogArguments = myObject;
	document.getElementById('frmModal').src = urlModal;
}" Closing="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
            
			if(cancelaFechamentoPopUp == 'S')
				e.cancel = true;
    		else
            	resetaModal();
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
