<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ConfiguracaoSistema.aspx.cs" Inherits="administracao_ConfiguracaoSistema" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div id="ConteudoPrincipal" style="padding-top: 10px">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            Width="100%" OnCallback="pnCallback_Callback">
            <Paddings Padding="0px" />
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div id="divGrid" style="visibility:hidden">
                        <dxwgv:ASPxGridView runat="server"
                        ClientInstanceName="gvDados" KeyFieldName="CodigoParametro"
                        AutoGenerateColumns="False" Width="100%"
                        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                            CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
	 }
}" Init="function(s, e)
{
     var sHeight = Math.max(0, document.documentElement.clientHeight) - 110;
     s.SetHeight(sHeight);
     document.getElementById('divGrid').style.visibility = 'visible';
}"></ClientSideEvents>

                        <SettingsCommandButton>
                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                        </SettingsCommandButton>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="50px" VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
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
                            <dxwgv:GridViewDataTextColumn FieldName="GrupoParametro" GroupIndex="0"
                                SortIndex="0" SortOrder="Ascending" Width="200px" Caption="Grupo"
                                VisibleIndex="1">
                                <Settings AllowGroup="True" AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoParametro_PT"
                                Caption="Descrição" VisibleIndex="2">
                                <Settings AllowGroup="False" AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoValor" Caption="Valor"
                                VisibleIndex="5">
                                <Settings AllowGroup="False"></Settings>
                                <DataItemTemplate>
                                    <%# (Eval("DescricaoValor").ToString().Length == 7 && Eval("DescricaoValor").ToString().IndexOf("#") == 0) ? getTratamentoCores(Eval("DescricaoValor").ToString()) : Eval("DescricaoValor").ToString()%>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TipoDadoParametro"
                                Name="TipoDadoParametro" Width="200px" Caption="Tipo" Visible="False"
                                VisibleIndex="3">
                                <Settings AllowGroup="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Valor" Visible="False" VisibleIndex="4">
                                <Settings AllowGroup="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxtv:GridViewDataTextColumn Caption="Parâmetro" FieldName="Parametro" ShowInCustomizationForm="False" Visible="False" VisibleIndex="6">
                            </dxtv:GridViewDataTextColumn>
                        </Columns>

                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                        <Templates>
                            <FooterRow>
                                <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="grid-legendas-cor grid-legendas-cor-controlado-sistema"><span></span></td>
                                            <td class="grid-legendas-label grid-legendas-label-controlado-sistema">
                                                <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                    Text="Controlados Pelo Sistema">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>

                        <SettingsPager>
                            <PageSizeItemSettings Position="Left">
                            </PageSizeItemSettings>
                        </SettingsPager>

                        <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                            ShowFilterRow="True" ShowFooter="True"></Settings>

                        <Styles>
                            <GroupRow HorizontalAlign="Left">
                            </GroupRow>
                            <Cell HorizontalAlign="Left">
                            </Cell>
                            <GroupPanel Font-Names="Verdana" Font-Size="8pt"></GroupPanel>
                        </Styles>
                    </dxwgv:ASPxGridView>
                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                    </div>
                </dxp:PanelContent>
            </PanelCollection>

            <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
        </dxcp:ASPxCallbackPanel>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados">
            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True" HorizontalAlign="Left"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <dxp:ASPxPanel runat="server" Width="100%" ID="pnFormulario2" Style="overflow: auto">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table style="width: 710px" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td><span style="font-size: 8pt; font-family: Verdana">
                                                                <asp:Label runat="server" Text="<%$ Resources:traducao,valor %>" ID="lblValor"></asp:Label>


                                                            </span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtValorTXT" Font-Names="Verdana" Font-Size="8pt" ID="txtValorTXT">
                                                                    <ValidationSettings>

                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>

                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                </dxe:ASPxTextBox>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="60px" ClientInstanceName="txtValorINT" Font-Names="Verdana" Font-Size="8pt" ID="txtValorINT">
                                                                    <MaskSettings Mask="&lt;0..100000000&gt;"></MaskSettings>

                                                                    <ValidationSettings>

                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>

                                                                    <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                                </dxe:ASPxTextBox>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" ShowShadow="False" AutoResizeWithContainer="True" ClientInstanceName="ddlValorMES" Font-Names="Verdana" Font-Size="8pt" ID="ddlValorMES">


                                                                    <SettingsLoadingPanel Text=""></SettingsLoadingPanel>


                                                                </dxe:ASPxComboBox>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxRadioButtonList runat="server" ItemSpacing="15px" RepeatDirection="Horizontal" ClientInstanceName="rbValorBOL" Width="129px" Font-Names="Verdana" Font-Size="8pt" ID="rbValorBOL">
                                                                    <Paddings Padding="0px"></Paddings>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                                        <dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
                                                                    </Items>
                                                                </dxe:ASPxRadioButtonList>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxColorEdit runat="server" ShowShadow="False" ClientInstanceName="ddlCOR" Font-Names="Verdana" Font-Size="8pt" ID="ddlCOR">
                                                                    <ClientSideEvents Init="OnInit" />
                                                                    <ClientSideEvents Init="OnInit"></ClientSideEvents>

                                                                </dxe:ASPxColorEdit>


                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>

                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label runat="server" Text="<%$ Resources:traducao,descri__o %>" ID="lblDescricao"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxMemo runat="server" Rows="5" Width="100%" ClientInstanceName="memoDescricao" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" ID="memoDescricao">

                                        <DisabledStyle ForeColor="#404040"></DisabledStyle>
                                    </dxe:ASPxMemo>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr style="height: 35px">
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>

                                                </td>
                                                <td style="width: 10px"></td>
                                                <td align="right">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnFechar">
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
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        



        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
            GridViewID="gvDados" PaperKind="A4"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Header Font-Bold="True" Font-Names="Verdana" Font-Size="9pt">
                </Header>
                <Cell Font-Names="Verdana" Font-Size="8pt">
                </Cell>
                <GroupRow Font-Bold="True" Font-Names="Verdana" Font-Size="9pt">
                </GroupRow>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </div>
</asp:Content>
