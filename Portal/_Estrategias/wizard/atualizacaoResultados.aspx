<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="atualizacaoResultados.aspx.cs" Inherits="_Estrategia_wizard_atualizacaoResultados" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Atualização de Resultados de Metas Estratégicas"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td id="ConteudoPrincipal">
                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                </dxhf:ASPxHiddenField>

                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                    ID="ASPxGridViewExporter1"
                    OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                    <Styles>

                        <Default></Default>

                        <Header></Header>

                        <Cell></Cell>

                        <GroupFooter Font-Bold="True"></GroupFooter>

                        <Title Font-Bold="True"></Title>
                    </Styles>
                </dxwgv:ASPxGridViewExporter>

                <dxwgv:ASPxGridView ID="gvDados" runat="server"
                    AutoGenerateColumns="False" ClientInstanceName="gvDados"
                    KeyFieldName="CodigoIndicador" Width="100%"
                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                    <Templates>
                        <FooterRow>
                            <table class="grid-legendas" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td class="grid-legendas-icone">
                                            <img src="../../imagens/formula(14x14).png" height="16" /></td>
                                        <td class="grid-legendas-label grid-legendas-label-icone">
                                            <asp:Label ID="lblVerde" runat="server" Text="<%# Resources.traducao.atualizacaoResultados_f_rmula_ok %>" EnableViewState="False"></asp:Label></td>
                                        <td class="grid-legendas-asterisco">
                                            <asp:Label ID="Label1" runat="server" Width="100%" Text="?" Font-Bold="True" EnableViewState="False" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td class="grid-legendas-label grid-legendas-label-icone">
                                            <asp:Label ID="lblAmarelo" runat="server" Text="<%# Resources.traducao.atualizacaoResultados_f_rmula_n_o_definida %>" EnableViewState="False"></asp:Label></td>
                                        <td class="grid-legendas-cor grid-legendas-cor-atrasado"><span></span></td>
                                        <td class="grid-legendas-label grid-legendas-label-atrasado">
                                            <asp:Label ID="Label2" runat="server" Text="<%# Resources.traducao.atualizacaoResultados_atrasados %>" EnableViewState="False"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </FooterRow>
                    </Templates>

                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" AutoExpandAllGroups="True"></SettingsBehavior>

                    <Styles>
                        <FilterCell></FilterCell>
                    </Styles>

                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(window.pcDados &amp;&amp; pcDados.GetVisible())
		OnGridFocusedRowChanged(s, true);
}"
                        CustomButtonClick="function(s, e) {
	OnGridFocusedRowChanged(gvDados, true);
    pcDados.Show();
}"
                        Init="function(s, e) {
	
}"></ClientSideEvents>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="35px" Caption=" " VisibleIndex="0">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Atualizar Resultados de Desempenho">
                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
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
                        <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Indicador" VisibleIndex="1" Width="480px">
                            <Settings AllowHeaderFilter="False" />
                            <DataItemTemplate>
                                <%# Eval("Atrasado").ToString() == "Sim" ? "<a style='Color:Red;'>" + Eval("NomeIndicador") + "</a>" : Eval("NomeIndicador").ToString() %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" Visible="False"
                            VisibleIndex="4">
                            <Settings AllowHeaderFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IndicaIndicadorCompartilhado"
                            Name="IndicaIndicadorCompartilhado" Width="100px" Caption="Compartilhado"
                            VisibleIndex="5" Visible="False">
                            <Settings AllowHeaderFilter="False" />
                            <DataItemTemplate>
                                <%# (Eval("IndicaIndicadorCompartilhado").ToString() == "S") ? "Sim" : "Não"%>
                            </DataItemTemplate>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Caption="Responsável" VisibleIndex="6">
                            <Settings AllowHeaderFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="Atualizacao" Name="Atualizacao"
                            Width="200px" Caption="Próxima Atualização"
                            VisibleIndex="8">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom" DisplayFormatInEditMode="True"></PropertiesDateEdit>

                            <Settings AutoFilterCondition="LessOrEqual" AllowHeaderFilter="False"></Settings>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Atrasado" Name="Atrasado" Width="260px"
                            Caption="Atrasado" VisibleIndex="9">
                            <Settings AllowHeaderFilter="False" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="formulaPorExtenso"
                            Name="formulaPorExtenso" Width="35px" Caption=" " VisibleIndex="10">
                            <Settings AllowAutoFilter="False" AllowHeaderFilter="False" AllowDragDrop="False"
                                AllowGroup="False" AllowSort="False"></Settings>
                            <DataItemTemplate>
                                <%# Eval("formulaPorExtenso").ToString() != "" ? "<img title='fórmula Ok' alt='' style='border:0px' src='../../imagens/formula.png'/>" : "<font title='fórmula não definida' color='red'><b>?</b></font>"%>
                            </DataItemTemplate>

                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Permissoes" Name="Permissoes"
                            Caption="Permissoes" Visible="False" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoPeriodicidade_PT" Visible="False"
                            VisibleIndex="13">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Visible="False"
                            VisibleIndex="14">
                            <Settings AllowHeaderFilter="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Mapa Estratégico"
                            FieldName="MapaEstrategico" GroupIndex="1" SortIndex="1" SortOrder="Ascending"
                            VisibleIndex="11" Width="100px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="<%$ Resources:traducao,respons_vel_atualiza__o %>"
                            FieldName="NomeUsuarioAtualizacao" Name="NomeUsuarioAtualizacao"
                            VisibleIndex="7">
                            <Settings AllowHeaderFilter="False" />
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Visible="False" VisibleIndex="15">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Unidade de Negócio" FieldName="NomeUnidadeNegocio" GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="16" Width="20px">
                        </dxtv:GridViewDataTextColumn>
                    </Columns>

                    <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                        ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True"></Settings>

                    <StylesEditors>
                        <TextBox></TextBox>
                    </StylesEditors>
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="pcDados" runat="server" HeaderText="Atualização dos Resultados" ClientInstanceName="pcDados" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" AllowDragging="True" Width="900px">
                    <ContentStyle>
                        <Paddings PaddingBottom="8px"></Paddings>
                    </ContentStyle>

                    <ClientSideEvents Closing="function(s, e) {	
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
       LimpaCampos();
}"
                        Shown="function(s, e) {			
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}"></ClientSideEvents>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0"
                                                ClientInstanceName="tabEdicao" Width="100%"
                                                ID="ASPxPageControl1">
                                                <TabPages>
                                                    <dxtc:TabPage Name="tbDados" Text="Dados do Indicador">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <div id="divDadosIndicador" runat="server">
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel4"></dxe:ASPxLabel>




                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIndicador" ClientEnabled="False" ID="txtIndicador">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxe:ASPxTextBox>




                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 275px">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Unidade de Medida:" ID="ASPxLabel2"></dxe:ASPxLabel>




                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Polaridade:" ID="ASPxLabel3"></dxe:ASPxLabel>




                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 275px">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="260px" ClientInstanceName="txtUnidadeMedida" ClientEnabled="False" ID="txtUnidadeMedida">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>




                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPolaridade" ClientEnabled="False" ID="txtPolaridade">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
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
                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 275px">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Periodicidade:" ID="ASPxLabel5"></dxe:ASPxLabel>




                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ID="ASPxLabel6"></dxe:ASPxLabel>




                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 275px">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="260px" ClientInstanceName="txtPeriodicidade" ClientEnabled="False" ID="txtPeriodicidade">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>




                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtResponsavel" ClientEnabled="False" ID="txtResponsavel">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
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
                                                                                    <dxe:ASPxLabel runat="server" Text="F&#243;rmula:" ID="ASPxLabel7"></dxe:ASPxLabel>




                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo runat="server" Rows="10" Width="100%"
                                                                                        ClientInstanceName="txtFormula" ClientEnabled="False"
                                                                                        ID="txtFormula">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    </dxe:ASPxMemo>




                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="titlePanel"></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabResultados" Text="Atualiza&#231;&#227;o dos Resultados">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <div id="divTabResultados" runat="server">
                                                                    <iframe id="frmMetas" src="" frameborder="0" runat="server" width="100%"></iframe>
                                                                </div>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                                <ClientSideEvents ActiveTabChanged="function(s, e) {
	document.getElementById('frmMetas').src = urlMetas;
}" />
                                            </dxtc:ASPxPageControl>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
                  pcDados.Hide();

   	document.getElementById('frmMetas').src = window.top.pcModal.cp_Path + &quot;branco.htm&quot;
    }"></ClientSideEvents>

                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>

                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                </dxpc:ASPxPopupControl>

            </td>
        </tr>
    </table>
</asp:Content>




