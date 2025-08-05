<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroOrcTematicos.aspx.cs" Inherits="administracao_CadastroOrcTematicos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table cellpadding="0" cellspacing="0" class="auto-style1">
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Orçamentos Temáticos" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="ConteudoPrincipal">
        <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoOrcamentoTematico" ClientInstanceName="gvDados" Width="100%" ID="gvDados" Style="text-align: left" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
            <ClientSideEvents CustomButtonClick="function(s, e) {
     //gvDados.SetFocusedRowIndex(e.visibleIndex);
     btnSalvar1.SetVisible(true);    
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
            TipoOperacao = &quot;Editar&quot;;
             hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
            onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
             onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {
             OnGridFocusedRowChanged(gvDados, true);
             btnSalvar1.SetVisible(false);
             hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
             pcDados.Show();
     }	
}"
                FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"
                BeginCallback="function(s, e) {
	comando = e.command;
}"
                EndCallback="function(s, e) {
            //debugger
               if(comando == &quot;CUSTOMCALLBACK&quot;)
               {
                        var sucesso = s.cp_sucesso;
                        var erro = s.cp_erro;
                        if(s.cp_erro == &quot;&quot;)
                        {
                                     window.top.mostraMensagem(sucesso , 'sucesso', false, false, null);
                                     if (window.onClick_btnCancelar)
                                     {
       	                                onClick_btnCancelar();
                                     }
                        }
                        else if(erro != &quot;&quot;)
                        {
                                   window.top.mostraMensagem(erro , 'erro', true, false, null);                                      
                        }
               }
}"></ClientSideEvents>

            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

            <Settings ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
            <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
            <SettingsCommandButton>
                <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
            </SettingsCommandButton>
            <Columns>
                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="130px" Caption="A&#231;&#245;es" VisibleIndex="0">
                    <CustomButtons>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                            <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                            <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                            <Image AlternateText="Detalhe" Url="~/imagens/botoes/pFormulario.png"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                    </CustomButtons>

                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <CellStyle HorizontalAlign="Center"></CellStyle>
                    <HeaderTemplate>
                        <table>
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
                </dxcp:GridViewCommandColumn>
                <dxcp:GridViewDataTextColumn FieldName="DescricaoOrcamentoTematico" ShowInCustomizationForm="True" Name="col_DescricaoOrcamentoTematico" Caption="Descri&#231;&#227;o" VisibleIndex="3">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                    <CellStyle Wrap="True"></CellStyle>
                </dxcp:GridViewDataTextColumn>
            </Columns>

            <Styles>
                <EmptyDataRow BackColor="#EEEEDD" ForeColor="Black"></EmptyDataRow>
            </Styles>
        </dxcp:ASPxGridView>
    </div>


    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="960px" ID="pcDados">
        <ContentStyle>
            <Paddings Padding="8px"></Paddings>
        </ContentStyle>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <!-- table -->
                <table width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" Text="Título:" ClientInstanceName="lblTituloOrcamento" ID="lblTituloOrcamento"></dxcp:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <!-- ASPxROUNDPANEL -->
                                            <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtDescricaoOrcamento" ID="txtDescricaoOrcamento">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                            </dxcp:ASPxTextBox>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="Dotacao" ClientInstanceName="gvDotacoes" Width="100%" ID="gvDotacoes" OnCustomCallback="gvDotacoes_CustomCallback">
                                <SettingsPager PageSize="100">
                                </SettingsPager>

                                <Settings VerticalScrollableHeight="275" VerticalScrollBarMode="Auto"></Settings>

                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>

                                <SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>
                                <Columns>
                                    <dxcp:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" Width="40px" Caption=" " VisibleIndex="0"></dxcp:GridViewCommandColumn>
                                    <dxcp:GridViewDataTextColumn FieldName="Dotacao" ShowInCustomizationForm="True" Caption="Dotação" VisibleIndex="2">
                                        <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                                    </dxcp:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption=" " FieldName="ColunaAgrupamento" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Descending" VisibleIndex="1">
                                    </dxtv:GridViewDataTextColumn>
                                    <dxcp:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3"></dxcp:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <GroupRowContent>
                                        <%# Container.GroupText == "1" ? "Selecionados" : "Disponíveis" %>
                                    </GroupRowContent>
                                </Templates>
                            </dxcp:ASPxGridView>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table class="formulario-botoes" cellpadding="0" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px" ID="btnSalvar">
                                                <ClientSideEvents Click="function(s, e) {
         if (window.onClick_btnSalvar)
	    onClick_btnSalvar();	
}"></ClientSideEvents>
                                            </dxcp:ASPxButton>

                                        </td>
                                        <td>
                                            <dxcp:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px" ID="btnCancelar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
	
}"></ClientSideEvents>
                                            </dxcp:ASPxButton>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>

    <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="false" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default></Default>

            <Header></Header>

            <Cell></Cell>

            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxcp:ASPxGridViewExporter>

    <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>
</asp:Content>

