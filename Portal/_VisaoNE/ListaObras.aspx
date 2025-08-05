<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaObras.aspx.cs" Inherits="_VisaoNE_ListaObras" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .branco
        {
            background: white;
        }
        .colorido
        {
            background: #F3B678;
        }
        .style2
        {
            width: 100%;
        }
    </style>
<%--    <table border="0" cellpadding="0" cellspacing="0" style="
        width: 100%; height: 28px">
        <tr>
            <td align="left" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Lista de Obras e Serviços">

                </asp:Label>
            </td>
        </tr>
    </table>--%>


    <table style="width:100%;">
        <tr>
            <td align="right">
            </td>
            <td align="right">
                <table cellpadding="0" cellspacing="0" class="style2">
                    <tr>
                        <td align="left">
                            <span>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                                OnClick="ImageButton1_Click" ToolTip="Exportar para excel" />
                                        </td>
                                        <td>
                                            <span>
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/salvar.png" ToolTip="Salvar layout da consulta"
                                                    Width="14px" Height="14px" ClientInstanceName="imgSalvaConfiguracoes" Cursor="pointer"
                                                    ID="imgSalvaConfiguracoes">
                                                    <ClientSideEvents Click="SalvarConfiguracoesLayout"></ClientSideEvents>
                                                </dxe:ASPxImage>
                                            </span>
                                        </td>
                                        <td>
                                            <span>
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/restaurar.png" ToolTip="Restaurar layout"
                                                    Width="16px" Height="16px" ClientInstanceName="imgRestaurarLayout" Cursor="pointer"
                                                    ID="imgRestaurarLayout">
                                                    <ClientSideEvents Click="RestaurarConfiguracoesLayout"></ClientSideEvents>
                                                </dxe:ASPxImage>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </span>
                        </td>
                        <td align="right">
                            <dxe:ASPxImage ID="imgGantt" runat="server" ClientInstanceName="imgGantt" Cursor="Pointer" ClientVisible="false"
                                ImageUrl="~/imagens/botoes/btnGantt.png" ToolTip="Visualizar Gantt">
                                <ClientSideEvents Click="function(s, e) {
	window.top.showModal('visaoCorporativa/vne_gantt.aspx', traducao.ListaObras_gantt_de_obras, screen.width - 60, screen.height - 260, '', null);
}" />
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoObra"
                    AutoGenerateColumns="False" Width="100%" 
                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                    OnProcessColumnAutoFilter="gvDados_ProcessColumnAutoFilter" OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	abreEdicaoObras(1, '')
}" ContextMenu="OnContextMenu"></ClientSideEvents>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Ações" FieldName="CodigoObra" Name="CodigoObra"
                            VisibleIndex="0" Width="95px">
                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" AllowDragDrop="False"
                                AllowGroup="False" AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False"
                                ShowInFilterControl="False" />
                            <Settings AllowDragDrop="False" AllowAutoFilterTextInputTimer="False" AllowAutoFilter="False"
                                ShowFilterRowMenu="False" AllowHeaderFilter="False" ShowInFilterControl="False"
                                AllowSort="False" AllowGroup="False"></Settings>
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%# getBotaoGraficoFluxo(Eval("EtapaContratacao").ToString(), Eval("CodigoWorkflow").ToString(), Eval("CodigoInstanciaWf").ToString(), Eval("CodigoFluxo").ToString(), Eval("CodigoProjeto").ToString()) %>
                                        </td>
                                        <td>
                                            <%# getBotaoInteragirFluxo(Eval("EtapaContratacao").ToString(), Eval("CodigoWorkflow").ToString(), Eval("CodigoInstanciaWf").ToString(), Eval("CodigoFluxo").ToString(), Eval("CodigoProjeto").ToString(), Eval("CodigoEtapaInicial").ToString(), Eval("CodigoEtapaAtual").ToString(), Eval("OcorrenciaAtual").ToString(), Eval("CodigoStatus").ToString(), Eval("TipoProjeto").ToString())%>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <HeaderStyle  HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderCaptionTemplate>
                                <%# getBotoesInsercao() %>
                            </HeaderCaptionTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Munic&#237;pio" FieldName="Municipio" Name="Municipio"
                            VisibleIndex="1" Width="165px">
                            <Settings AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Obra/Serviço" FieldName="Projeto" VisibleIndex="2"
                            Width="300px" Name="Projeto">
                            <Settings AutoFilterCondition="Contains" AllowGroup="True"></Settings>
                            <DataItemTemplate>
                                <%# getDescricaoObra()%>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="3"
                            Width="145px" Name="Status">
                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" AllowGroup="True" AutoFilterCondition="Contains">
                            </Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="EtapaContratacao" VisibleIndex="4" Caption="Etapa do Fluxo"
                            Width="150px" Name="EtapaContratacao">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Número da OS Interna" FieldName="NumeroOS"
                            VisibleIndex="5" Width="150px" Name="NumeroOS">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Contrato" FieldName="ValorContrato"
                            VisibleIndex="6" Width="160px" Visible="False" Name="ValorContrato">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Pago" FieldName="ValorPago" VisibleIndex="7"
                            Width="160px" Visible="False" Name="ValorPago">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Previsto Pagamento" FieldName="PrevistoPagamento"
                            VisibleIndex="8" Width="160px" Visible="False" Name="PrevistoPagamento">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Saldo" FieldName="Saldo" VisibleIndex="9"
                            Width="160px" Visible="False" Name="Saldo">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% Financeiro" FieldName="PercentualFinanceiro"
                            VisibleIndex="10" Width="90px" Visible="False" Name="PercentualFinanceiro">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" ShowFilterRowMenu="True"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% F&#237;sico" FieldName="PercentualFisico"
                            VisibleIndex="11" Width="75px" Name="PercentualFisico">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="True" ShowFilterRowMenu="True"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Segmento" FieldName="Segmento" VisibleIndex="12"
                            Width="130px" Visible="False" Name="Segmento">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término Pactuado" FieldName="Termino" VisibleIndex="13"
                            Width="125px" Name="Termino">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" AutoFilterCondition="LessOrEqual" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término Cronograma" FieldName="TerminoCronograma"
                            VisibleIndex="14" Width="140px" Name="TerminoCronograma">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" AutoFilterCondition="GreaterOrEqual" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ano Término Pactuado" FieldName="AnoTermino"
                            VisibleIndex="15" Width="160px" Name="AnoTermino">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Número do Contrato" FieldName="NumeroContrato"
                            VisibleIndex="16" Width="155px" Name="NumeroContrato">
                            <Settings AllowGroup="False" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="In&#237;cio Vigência Contrato" FieldName="InicioVigenciaContrato"
                            VisibleIndex="17" Width="175px" Visible="False" Name="InicioVigenciaContrato">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término Vigência Contrato" FieldName="TerminoVigenciaContrato"
                            VisibleIndex="18" Width="175px" Visible="False" Name="TerminoVigenciaContrato">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Fornecedor" FieldName="Fornecedor" VisibleIndex="19"
                            Width="300px" Visible="False" Name="Fornecedor">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Obra/Serviço" FieldName="TipoObra"
                            VisibleIndex="20" Width="160px" Name="TipoObra">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Quantidade Obras" FieldName="QuantidadeObras"
                            VisibleIndex="22" Width="115px" Visible="False" Name="QuantidadeObras">
                            <PropertiesTextEdit DisplayFormatString="{0:n0}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Indica Obra / Serviço" FieldName="IndicaObraServico"
                            VisibleIndex="25" Width="140px" Visible="False" Name="IndicaObraServico">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Processo em Contratação" FieldName="TipoContratacao"
                            VisibleIndex="26" Width="210px" Visible="False" Name="TipoContratacao">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Última Atualização Cronograma" FieldName="UltimaAtualizacaoCronograma"
                            VisibleIndex="27" Width="185px" Visible="False" Name="UltimaAtualizacaoCronograma">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Último Upload de Foto" FieldName="UltimaAtualizacaoFoto"
                            VisibleIndex="28" Width="175px" Visible="False" Name="UltimaAtualizacaoFoto">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Último Comentário Fiscalização" FieldName="UltimaAnaliseFiscalizacao"
                            VisibleIndex="29" Width="185px" Visible="False" Name="UltimaAnaliseFiscalizacao">
                            <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:dd/MM/yyyy}"
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" ShowFilterRowMenu="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DesempenhoFisico" VisibleIndex="30" Caption="Estágio de Execução do Projeto"
                            Width="210px" Visible="False" Name="DesempenhoFisico">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SituacaoAtualContrato" VisibleIndex="31"
                            Caption="Situação Atual Contrato" Width="210px" Visible="False" Name="SituacaoAtualContrato">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Posso Interagir?" FieldName="ProcessoComigo"
                            VisibleIndex="21" Width="100px" Name="ProcessoComigo">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Minha Gerência?" FieldName="ProcessoMinhaGerencia"
                            VisibleIndex="23" Width="100px" Name="ProcessoMinhaGerencia">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" AllowGroup="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Requisição de Compra SAP" FieldName="RequisicaoSAP"
                            VisibleIndex="24" Width="170px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" AllowFocusedRow="True"
                        EnableCustomizationWindow="true"></SettingsBehavior>
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        HorizontalScrollBarMode="Visible" ShowFooter="True" ShowHeaderFilterBlankItems="False">
                    </Settings>
                    <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                    <SettingsPopup>
                        <CustomizationWindow Height="200px" HorizontalAlign="NotSet" VerticalAlign="NotSet"
                            Width="200px" />
                    </SettingsPopup>
                </dxwgv:ASPxGridView>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcTipoObras" runat="server" PopupElementID="btnIncluir"
        PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" ShowCloseButton="False"
        ShowHeader="False" ClientInstanceName="pcTipoObras" Width="250px">
        <ContentStyle>
            <Paddings Padding="3px" />
        </ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" onclick="insereNovaObra('soci')" id="soci">
                        <td colspan="0" rowspan="0" valign="middle">
                            <table>
                                <tr>
                                    <td style="width: 25px">
                                        <dxe:ASPxImage ID="imgOp1" runat="server" ImageUrl="~/imagens/botoes/socioambiental.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp1" runat="server" ClientInstanceName="lblOp1"
                                            Text="PBA">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" id="plan" onclick="insereNovaObra('plan')">
                        <td colspan="0" rowspan="0" valign="middle">
                            <table>
                                <tr>
                                    <td style="width: 25px">
                                        <dxe:ASPxImage ID="imgOp2" runat="server" ImageUrl="~/imagens/botoes/plano_de_seguranca.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp2" runat="server" ClientInstanceName="lblOp2"
                                            Text="Plano de Segurança Pública">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" onclick="insereNovaObra('pdrs')" id="pdrs">
                        <td colspan="0" rowspan="0" valign="middle">
                            <table>
                                <tr>
                                    <td style="width: 25px">
                                        <dxe:ASPxImage ID="imgOp3" runat="server" ImageUrl="~/imagens/botoes/pdrs.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp3" runat="server" ClientInstanceName="lblOp3"
                                            Text="PDRS">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" onclick="insereNovaObra('indi')" id="indi">
                        <td colspan="0" rowspan="0" valign="middle">
                            <table>
                                <tr>
                                    <td style="width: 25px">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" Height="16px" ImageUrl="~/imagens/geral/apache.PNG"
                                            Width="16px">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp4" runat="server" ClientInstanceName="lblOp4"
                                            Text="Indígena">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <Border BorderColor="#EBEBEB" BorderStyle="Solid" BorderWidth="1px" />
    </dxpc:ASPxPopupControl>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {	
		gvDados.PerformCallback('R');    
}" />
    </dxcb:ASPxCallback>
    <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu"
        >
        <Items>
            <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
            </dxm:MenuItem>
            <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
            </dxm:MenuItem>
        </Items>
        <ClientSideEvents ItemClick="OnItemClick" />
    </dxm:ASPxPopupMenu>
                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                    Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                </dxwgv:ASPxGridViewExporter>
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                </dxhf:ASPxHiddenField>
            </asp:Content>
