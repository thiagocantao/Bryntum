<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="metasDesempenho.aspx.cs" Inherits="_Estrategias_wizard_metasDesempenho" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle">&nbsp;
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Atualização das Metas Estratégicas"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <style>
        @media (max-height:768px) {
            .divAlturaFrameAtlResult {
                height: 392px;
            }
        }

        @media (min-height:769px) and (max-height:900px) {
            .divAlturaFrameAtlResult {
                height: 440px;
            }
        }

        @media (min-height:901px) {
            .divAlturaFrameAtlResult {
                height: 550px;
            }
        }
    </style>

    <div id="ConteudoPrincipal">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador"
                        AutoGenerateColumns="False" Width="100%"
                        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"
                            CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnEditarCustom&quot;){
		pcDados.Show();
	}
	else if(e.buttonID == &quot;btnReplicar&quot;){
		pcAtualizarMetas.PerformCallback(s.GetRowKey(e.visibleIndex));
		pcAtualizarMetas.Show();
	}
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" Caption=" " VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Atualizar Metas de Desempenho">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG" Height="20px" ToolTip="Atualizar Metas de Desempenho"
                                            Width="20px">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnReplicar" Text="Replicar meta">
                                        <Image AlternateText="Replicar meta" Height="20px" ToolTip="Replicar meta"
                                            Width="20px" Url="~/imagens/botoes/btnDuplicar.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderTemplate>
                                    <table>
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
                            <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Indicador" VisibleIndex="1" Width="50%">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False"
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" Visible="False"
                                VisibleIndex="4">
                                <Settings AllowHeaderFilter="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="IndicaIndicadorCompartilhado" Name="IndicaIndicadorCompartilhado"
                                Width="100px" Caption="Compartilhado" VisibleIndex="5" Visible="False">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
                                <DataItemTemplate>
                                    <%# (Eval("IndicaIndicadorCompartilhado").ToString() == "S") ? "Sim" : "Não"%>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Width="300px"
                                Caption="Respons&#225;vel" VisibleIndex="6">
                                <Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Meta" Name="Meta" Caption="Meta" Visible="False"
                                VisibleIndex="12">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="FormulaIndicador" Name="FormulaIndicador"
                                Width="35px" Caption=" " VisibleIndex="7" ExportWidth="60">
                                <Settings AllowAutoFilter="False" AllowHeaderFilter="False"
                                    AllowDragDrop="False" AllowGroup="False" AllowSort="False"></Settings>
                                <DataItemTemplate>
                                    <%# Eval("formulaPorExtenso").ToString() != "" ? "<img title='fórmula Ok' alt='' style='border:0px' src='../../imagens/formula.png'/>" : "<font title='fórmula não definida' color='red'><b>?</b></font>"%>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Permissao" Name="Permissao" Caption="Permissao"
                                Visible="False" VisibleIndex="10">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Visible="False"
                                VisibleIndex="11">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoPeriodicidade_PT" Visible="False"
                                VisibleIndex="13">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mapa Estratégico" FieldName="MapaEstrategico"
                                GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                                VisibleIndex="8" Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio"
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" AutoExpandAllGroups="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupPanel="True"
                            ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True"></Settings>
                        <Templates>
                            <FooterRow>
                                <table class="grid-legendas" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td class="grid-legendas-icone">
                                                <img src="../../imagens/formula(14x14).png" />
                                            </td>
                                            <td class="grid-legendas-label grid-legendas-label-icone">
                                                <asp:Label ID="lblVerde" runat="server" Text="<%# Resources.traducao.metasDesempenho_f_rmula_ok %>"
                                                    EnableViewState="False"></asp:Label>
                                            </td>
                                            <td class="grid-legendas-asterisco">
                                                <asp:Label ID="Label1" runat="server" Width="100%"
                                                    Text="?" Font-Bold="True" EnableViewState="False" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td class="grid-legendas-label grid-legendas-label-asterisco">
                                                <asp:Label ID="lblAmarelo" runat="server" Width="100%"
                                                    Text="<%# Resources.traducao.metasDesempenho_f_rmula_n_o_definida %>" EnableViewState="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">



                                <dxcb:ASPxCallback runat="server" ClientInstanceName="pnCallbackMeta" ID="callback"
                                    OnCallback="callback_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	var filaEditar = document.getElementById(&quot;tdEditarMeta&quot;);
	var filaAcao = document.getElementById(&quot;tdAcaoMeta&quot;);

	filaEditar.style.display = &quot;&quot;;
	filaAcao.style.display = &quot;none&quot;;

	txtMeta.SetEnabled(false);
}"></ClientSideEvents>
                                </dxcb:ASPxCallback>

                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None" HeaderText="Atualiza&#231;&#227;o das Metas" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="1200"
            ID="pcDados">
            <ClientSideEvents Closing="function(s, e) {
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}"
                Shown="function(s, e) {	
	tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));

	//Tratar Botoes de edi&#231;&#227;o:
	var fila = document.getElementById('tdAcaoMeta');
	fila.style.display = 'none';

	//by Alejandro : 22/03/2011
	//var permissao = hfGeral.Get('PermissaoLinha');
}"></ClientSideEvents>
            <ContentStyle>
                <Paddings PaddingBottom="8px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0"
                                        ClientInstanceName="tabEdicao"
                                        Width="100%" ID="ASPxPageControl1">
                                        <TabPages>
                                            <dxtc:TabPage Name="tbDados" Text="Dados do Indicador">
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Indicador:"
                                                                            ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIndicador" ClientEnabled="False"
                                                                            ID="txtIndicador">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 275px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Unidade de Medida:"
                                                                                            ID="ASPxLabel2">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Polaridade:"
                                                                                            ID="ASPxLabel3">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 275px">
                                                                                        <dxe:ASPxTextBox runat="server" Width="260px" ClientInstanceName="txtUnidadeMedida"
                                                                                            ClientEnabled="False" ID="txtUnidadeMedida">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPolaridade" ClientEnabled="False"
                                                                                            ID="txtPolaridade">
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
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Descrição da Meta:"
                                                                            ID="ASPxLabel10">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <dxe:ASPxMemo runat="server" Rows="7" Width="100%" ClientInstanceName="txtMeta" ClientEnabled="False"
                                                                                            ID="txtMeta">
                                                                                            <ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="width: 35px" valign="top" align="center">
                                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td id="tdEditarMeta">
                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/editarReg02.PNG" ToolTip="Alterar meta"
                                                                                                            ClientInstanceName="imgEditarMeta" Cursor="pointer" ID="imgEditarMeta">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	var filaEditar = document.getElementById(&quot;tdEditarMeta&quot;);
	var filaAcao = document.getElementById(&quot;tdAcaoMeta&quot;);

	filaEditar.style.display = &quot;none&quot;;
	filaAcao.style.display = &quot;&quot;;

	txtMeta.SetEnabled(true);
}"></ClientSideEvents>
                                                                                                        </dxe:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td id="tdAcaoMeta">
                                                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/PublicarReg02.png" ToolTip="Salvar meta"
                                                                                                                            ClientInstanceName="imgPublicarMeta" Cursor="pointer" ID="imgPublicarMeta">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
	var meta = txtMeta.GetText();

	pnCallbackMeta.PerformCallback();
}"></ClientSideEvents>
                                                                                                                        </dxe:ASPxImage>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/cancelar.PNG" ToolTip="Cancelar"
                                                                                                                            ClientInstanceName="imgCancelarMeta" Cursor="pointer" ID="imgCancelarMeta">
                                                                                                                            <ClientSideEvents Click="function(s, e) {
	OnClick_ImagemCancelar(s, e);
}"></ClientSideEvents>
                                                                                                                        </dxe:ASPxImage>
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
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 275px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Periodicidade:"
                                                                                            ID="ASPxLabel5">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:"
                                                                                            ID="ASPxLabel6">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 275px">
                                                                                        <dxe:ASPxTextBox runat="server" Width="260px" ClientInstanceName="txtPeriodicidade"
                                                                                            ClientEnabled="False" ID="txtPeriodicidade">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtResponsavel"
                                                                                            ClientEnabled="False" ID="txtResponsavel">
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
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="F&#243;rmula:"
                                                                            ID="ASPxLabel7">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Rows="6" Width="100%" ClientInstanceName="txtFormula"
                                                                            ClientEnabled="False" ID="txtFormula">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                            <dxtc:TabPage Name="tbMetas" Text="Atualiza&#231;&#227;o das Metas">
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <iframe class="divAlturaFrameAtlResult" id="frmMetas" frameborder="0" <%--height="600"--%> scrolling="no" src="" width="100%"></iframe>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                        </TabPages>
                                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	document.getElementById(&#39;frmMetas&#39;).src = urlMetas;
}"></ClientSideEvents>
                                        <ContentStyle>
                                            <Paddings PaddingLeft="5px" PaddingRight="5px"></Paddings>
                                        </ContentStyle>
                                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	document.getElementById('frmMetas').src = urlMetas;
}" />
                                    </dxtc:ASPxPageControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                        Text="Fechar" Width="90px" ID="btnFechar">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
pcDados.Hide();
document.getElementById('frmMetas').src = window.top.pcModal.cp_Path + &quot;branco.htm&quot;
gvDados.PerformCallback();
   }"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
            GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
        <dxpc:ASPxPopupControl ID="pcAtualizarMetas" runat="server" ClientInstanceName="pcAtualizarMetas"
            CloseAction="None" HeaderText="Replicar Metas"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
            Width="600px" Modal="True"
            OnWindowCallback="pcAtualizarMetas_WindowCallback">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 475px; padding-right: 10px">
                                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="Unidade:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                Text="Ano:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <dxe:ASPxComboBox ID="ddlUnidades" runat="server" ClientInstanceName="ddlUnidades"
                                                Width="100%">
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlAnos" runat="server" ClientInstanceName="ddlAnos"
                                                Width="100%">
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top: 10px">
                                <table>
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Salvar"
                                                AutoPostBack="False" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	var ano = ddlAnos.GetText();
	var unidade = ddlUnidades.GetText();
	var mensagem = &quot;&quot;;
	if(ano == &quot;Todos&quot;)
                {
                            mensagem = traducao.metasDesempenho_deseja_replicar_as_metas_da_unidade; + unidade + traducao.metasDesempenho_para_as_demais_unidades_da_entidade_que_estejam_relacionadas_ao_indicador;
               }		
	else
               {
		mensagem = traducao.metasDesempenho_deseja_replicar_as_metas_no_ano_de + ano + traducao.metasDesempenho_da_unidade + unidade +                     
                                traducao.metasDesempenho_para_as_demais_unidades_da_entidade_que_estejam_relacionadas_ao_indicador;
               }
	
    var funcObj = { funcaoClickOK: function(s, e){ callbackReplicarMetas.PerformCallback();
		                                           pcAtualizarMetas.Hide(); } }
	window.top.mostraConfirmacao(mensagem, function(){funcObj['funcaoClickOK'](s, e)}, function(){funcObj['funcaoClickCancelar'](s, e)});                                                                        
                                                                        
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Fechar"
                                                AutoPostBack="False" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	pcAtualizarMetas.Hide();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <dxcb:ASPxCallback ID="callbackReplicarMetas" runat="server"
                                    ClientInstanceName="callbackReplicarMetas"
                                    OnCallback="callbackReplicarMetas_Callback">
                                </dxcb:ASPxCallback>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText=""
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
            ShowHeader="False" Width="270px" ID="pcMensagemGravacao">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
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
    </div>
</asp:Content>
