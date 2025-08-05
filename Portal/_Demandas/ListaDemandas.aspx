<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="ListaDemandas.aspx.cs" Inherits="_Demandas_ListaDemandas" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script language="javascript" type="text/javascript">
        function grid_ContextMenu(s, e) {
            if (e.objectType == "header")
                pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
        }

        function SalvarConfiguracoesLayout() {
            callback.PerformCallback("save_layout");
        }

        function RestaurarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
            window.top.mostraMensagem('Deseja restaurar as configurações originais do layout da consulta?', 'confirmacao', true, true, function () { funcObj['funcaoClickOK']() });

        }

    </script>
    <style type="text/css">
        .branco {
            background: white;
        }

        .colorido {
            background: #F3B678;
        }

        .style2 {
            height: 20px;
        }

        .style3 {
            height: 10px;
        }

        .style4 {
            width: 10px;
            height: 10px;
        }
    </style>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 28px">
        <tr>
            <td align="left" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" CssClass="titlePage"
                    Text="Demandas de TIC"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style4"></td>
            <td class="style3"></td>
            <td class="style4"></td>
        </tr>
        <tr>
            <td></td>
            <td style="width: 100%">
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoDemanda"
                    AutoGenerateColumns="False" Width="100%"
                    ID="gvDados" EnableRowsCache="False" OnAfterPerformCallback="gvDados_AfterPerformCallback"
                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnProcessColumnAutoFilter="gvDados_ProcessColumnAutoFilter"
                    OnCustomCallback="gvDados_CustomCallback">
                    <%--<SettingsCustomizationWindow Enabled="True" Height="200px" PopupHorizontalAlign="NotSet" PopupVerticalAlign="NotSet" Width="200px" />--%>
                    <ClientSideEvents EndCallback="function(s, e) {
	ddlProjetoAbertura.PerformCallback();
}"
                        ContextMenu="OnContextMenu" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Ações" FieldName="CodigoDemanda" VisibleIndex="0"
                            Width="110px">
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
                                            <%# getBotaoGraficoFluxo() %>
                                        </td>
                                        <td>
                                            <%# getBotaoInteragirFluxo() %>
                                        </td>
                                        <td>
                                            <%# getBotaoEVTFluxo() %>
                                        </td>
                                        <td>
                                            <%# getBotaoVinculo() %>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderCaptionTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <%# getBotoesInsercao() %>
                                                    </td>
                                                    <td>
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
                                                                        <dxtv:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout" Name="btnRestaurarLayout">
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
                                        </td>
                                    </tr>
                                </table>
                            </HeaderCaptionTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nº CGTIC" VisibleIndex="1" Width="130px" FieldName="NumeroCGTIC">
                            <Settings AllowGroup="False" AutoFilterCondition="Contains" AllowAutoFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Título" VisibleIndex="2" Width="420px" FieldName="TituloDemanda">
                            <Settings AutoFilterCondition="Contains" AllowGroup="False" AllowAutoFilter="True"></Settings>
                            <DataItemTemplate>
                                <%# getDescricaoObra()%>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Demanda com Plano de Ação" FieldName="PossuiPlanoAcaoDemanda"
                            VisibleIndex="4" Width="200px">
                            <Settings AllowAutoFilter="True" AllowGroup="True" AutoFilterCondition="Contains" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Situação do Plano de Ação" FieldName="SituacaoPlanoAcaoDemanda"
                            VisibleIndex="5" Width="180px">
                            <Settings AllowAutoFilter="True" AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Órgão Demandante" VisibleIndex="3" Width="140px"
                            FieldName="SiglaOrgaoDemandante">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains"></Settings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Caption="Responsável" Width="200px"
                            FieldName="NomeResponsavel">
                            <Settings AllowGroup="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ofício JUCOF" VisibleIndex="7" Width="160px"
                            FieldName="OficioJucof">
                            <Settings AutoFilterCondition="Contains" AllowGroup="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="8" Width="180px" FieldName="StatusDemanda">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="True" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Etapa Atual" VisibleIndex="9" Width="180px"
                            FieldName="EtapaAtualProcesso">
                            <Settings AllowGroup="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Solicitado" VisibleIndex="10" Width="160px"
                            FieldName="ValorSolicitado">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="False" ShowFilterRowMenu="False" AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Aprovado CGTIC" VisibleIndex="11" Width="160px"
                            FieldName="ValorAprovado">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowGroup="False" ShowFilterRowMenu="False" AllowAutoFilter="False"></Settings>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Executado" FieldName="ValorExecutado"
                            VisibleIndex="12" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" ShowFilterRowMenu="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ano Orçamentário CGTIC" FieldName="Ano" VisibleIndex="13"
                            Width="110px">
                            <Settings AllowAutoFilter="False" AllowGroup="True" AllowHeaderFilter="True" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Solicitado Ano" FieldName="ValorSolicitadoAno"
                            VisibleIndex="14" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" ShowFilterRowMenu="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Aprovado Ano CGTIC" FieldName="ValorAprovadoAno"
                            VisibleIndex="15" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" ShowFilterRowMenu="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Executado Ano" FieldName="ValorExecutadoAno"
                            VisibleIndex="16" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowGroup="False" ShowFilterRowMenu="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Aprovado JUCOF" FieldName="ValorAprovadoJUCOF"
                            VisibleIndex="17" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor Aprovado Ano JUCOF" FieldName="ValorAprovadoAnoJUCOF"
                            VisibleIndex="18" Width="160px">
                            <PropertiesTextEdit DisplayFormatString="{0:c2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ano Orçamentário JUCOF" FieldName="AnoOrcamentoJUCOF"
                            VisibleIndex="19" Width="110px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" EnableCustomizationWindow="True"></SettingsBehavior>
                    <SettingsResizing  ColumnResizeMode="Control"/>
                    <SettingsPager PageSize="20" AlwaysShowPager="True">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        HorizontalScrollBarMode="Visible" ShowFooter="True" ShowHeaderFilterBlankItems="False"></Settings>
                    <SettingsText CommandClearFilter="Limpar filtro" CustomizationWindowCaption="Colunas Disponíveis"></SettingsText>

                    <SettingsPopup>
                        <CustomizationWindow Height="200px" HorizontalAlign="LeftSides" VerticalAlign="TopSides" Width="200px"
                            VerticalOffset="200" />
                    </SettingsPopup>
                    <Styles>
                        <Header Wrap="True">
                        </Header>
                        <%--                        <FilterPopupItemsArea >
                        </FilterPopupItemsArea>--%>
                    </Styles>
                </dxwgv:ASPxGridView>
            </td>
            <td></td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcTipoObras" runat="server" PopupElementID="btnIncluir"
        PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" ShowCloseButton="False"
        ShowHeader="False" ClientInstanceName="pcTipoObras" Width="332px">
        <ContentStyle>
            <Paddings Padding="3px" />
        </ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" id="abtprj" onclick="iniciaAberturaProjeto()">
                        <td colspan="0" valign="middle" class="style2">
                            <table>
                                <tr>
                                    <td style="width: 25px; display: none;">
                                        <dxe:ASPxImage ID="imgOp4" runat="server" ImageUrl="~/imagens/botoes/plano_de_seguranca.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp4" runat="server" ClientInstanceName="lblOp2"
                                            Text="Elaboração do EVT">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer;" class="op" onclick="insereNovaDemanda('TipoProjetoA')"
                        id="ds">
                        <td colspan="0" rowspan="0" valign="middle" class="style2">
                            <table>
                                <tr>
                                    <td style="width: 25px; display: none;">
                                        <dxe:ASPxImage ID="imgOp1" runat="server" ImageUrl="~/imagens/botoes/socioambiental.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp1" runat="server" ClientInstanceName="lblOp1"
                                            Text="Demanda de TIC">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="pps" class="op" onclick="insereNovaDemanda('TipoProjetoB')" onmouseout="this.className='branco';"
                        onmouseover="this.className='colorido';" style="cursor: pointer;">
                        <td class="style2" colspan="0" rowspan="0" valign="middle">
                            <table>
                                <tr>
                                    <td style="width: 25px; display: none;">
                                        <dxe:ASPxImage ID="imgOp2" runat="server" ImageUrl="~/imagens/botoes/plano_de_seguranca.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp2" runat="server" ClientInstanceName="lblOp2"
                                            Text="Plano de Investimento">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr onmouseover="this.className='colorido';" onmouseout="this.className='branco';"
                        style="cursor: pointer; display: none" class="op" onclick="insereNovaDemanda('TipoProjetoC')"
                        id="pnps">
                        <td colspan="0" rowspan="0" valign="middle" class="style2">
                            <table>
                                <tr>
                                    <td style="width: 25px; display: none;">
                                        <dxe:ASPxImage ID="imgOp3" runat="server" ImageUrl="~/imagens/botoes/pdrs.PNG">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="lblOp3" runat="server" ClientInstanceName="lblOp3"
                                            Text="Proposição de Projeto">
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
    <dxcb:ASPxCallback ID="callbackEVT" runat="server" ClientInstanceName="callbackEVT"
        OnCallback="callbackEVT_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(callbackEVT.cp_Url != null &amp;&amp; callbackEVT.cp_Url != '')
		window.location.href = callbackEVT.cp_Url;
}" />
    </dxcb:ASPxCallback>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
        Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
        ExportEmptyDetailGrid="True" PreserveGroupRowStates="False">
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
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu">
        <Items>
            <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
            </dxm:MenuItem>
            <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
            </dxm:MenuItem>
        </Items>
        <ClientSideEvents ItemClick="OnItemClick" />
    </dxm:ASPxPopupMenu>
    <dxpc:ASPxPopupControl ID="pcAbertura" runat="server" ClientInstanceName="pcAbertura"
        HeaderText="Abertura de Projeto" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="700px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lblProjeto" runat="server" ClientInstanceName="lblProjeto"
                                Text="Projeto:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxComboBox ID="ddlProjetoAbertura" runat="server" ClientInstanceName="ddlProjetoAbertura"
                                IncrementalFilteringMode="Contains" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                Text="Fazer Abertura" Width="130px">
                                                <ClientSideEvents Click="function(s, e) {
	if(ddlProjetoAbertura.GetValue()== null)
	{
		window.top.mostraMensagem('Nenhum projeto foi selecionado!', 'atencao', true, false, null); 
	}else
	{	
		callbackEVT.PerformCallback(ddlProjetoAbertura.GetValue() + &quot;;PRJ_TIC_PS&quot;);
	}
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td></td>
                                        <td>
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Cancelar" Width="130px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcAbertura.Hide();
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
</asp:Content>
