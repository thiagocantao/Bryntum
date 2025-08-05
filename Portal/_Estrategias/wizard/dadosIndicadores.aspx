<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="dadosIndicadores.aspx.cs" Inherits="_Estrategias_wizard_dadosIndicadores" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 10px"></td>
            <td style="padding-top: 5px">



                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback" Font-Names="Verdana" Font-Size="8pt">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <div id="divGrid" style="visibility: hidden">
                                <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoDado" AutoGenerateColumns="False" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                        CustomButtonClick="function(s, e) 
{
	onClick_CustomButtom(s, e);
}"
                                        Init="function(s, e) 
                                                    {
                                                    AdjustSize();
                                                    document.getElementById(&quot;divGrid&quot;).style.visibility = &quot;&quot;;
                                                    }"></ClientSideEvents>

                                    <Templates>
                                        <FooterRow>
                                            <table cellpadding="0" cellspacing="0"
                                                style="font-family: verdana; font-size: 8pt">
                                                <tr>
                                                    <td style="border: 1px solid #808080; width: 10px; background-color: #ddffcc">&nbsp;
                                                    </td>
                                                    <td style="padding-left: 3px; padding-right: 10px; font-family: verdana; font-size: 8pt">
                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                            ClientInstanceName="lblDescricaoNaoAtiva" Font-Bold="False"
                                                            Font-Names="Verdana" Font-Size="7pt" Text="<%$ Resources:traducao, dado_de_indicador_criado_em_outra_entidade %>">
                                                        </dxe:ASPxLabel>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>

                                        </FooterRow>
                                    </Templates>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>

                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSort="False" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                                    <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                    <Columns>
                                        <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="130px" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                </dxcp:GridViewCommandColumnCustomButton>
                                                <dxcp:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                </dxcp:GridViewCommandColumnCustomButton>
                                                <dxcp:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                                </dxcp:GridViewCommandColumnCustomButton>
                                                <dxcp:GridViewCommandColumnCustomButton ID="btnDadoCompartilhadoCustom" Text="<%$ Resources:traducao,editar_dado_compartilhado %>">
                                                    <Image Url="~/imagens/codigoReservado.png"></Image>
                                                </dxcp:GridViewCommandColumnCustomButton>
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
                                        </dxcp:GridViewCommandColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="DescricaoDado" ShowInCustomizationForm="True" Caption="Dado" VisibleIndex="1"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CodigoDado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="GlossarioDado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CodigoUnidadeMedida" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CasasDecimais" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="ValorMinimo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="ValorMaximo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CodigoFuncaoAgrupamentoDado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8"></dxcp:GridViewDataTextColumn>
                                        <dxcp:GridViewDataTextColumn FieldName="CodigoReservado" ShowInCustomizationForm="True" Caption="Codigo Reservado" Visible="False" VisibleIndex="10"></dxcp:GridViewDataTextColumn>
                                    </Columns>
                                </dxcp:ASPxGridView>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>

                    <ClientSideEvents EndCallback="function(s, e) {
     
     if(s.cp_erro != '')
     {
              window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null, 3000);              
     }
     else
    {
              if(s.cp_sucesso != '')
             {
                      gvDados.Refresh();
                        window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null, 3000);
             }
    }
  if (window.onClick_btnCancelar)
  {
     onClick_btnCancelar();
  }
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>

            </td>
        </tr>
    </table>
    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
    </table>
    <!-- POPUPCONTROL: pcDados -->
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
        GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
    <dxpc:ASPxPopupControl runat="server" AllowDragging="True"
        ClientInstanceName="pcAjuda" CloseAction="CloseButton" HeaderText="Ajuda"
        Modal="True" PopupElementID="imgAjuda" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" Width="293px" Font-Names="Verdana"
        Font-Size="8pt" ID="pcAjuda">
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                Código utilizado para interface com outros sistemas da instituição.
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl runat="server" AllowDragging="True"
        ClientInstanceName="pcAjudaNovoResp" CloseAction="CloseButton"
        HeaderText="Ajuda" Modal="True" PopupElementID="imgAjudaDadoComp"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        Width="293px" Font-Names="Verdana" Font-Size="8pt" ID="pcAjudaNovoResp">
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                Código utilizado para interface com outros sistemas da instituição.
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="720px" Height="385px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados" PopupVerticalOffset="10">
        <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>

        <ContentStyle>
            <Paddings PaddingTop="5px" PaddingBottom="5px"></Paddings>
        </ContentStyle>

        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table id="tbtbtb" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <table style="width: 720px" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 176px">
                                                <dxcp:ASPxLabel runat="server" Text="C&#243;digo Reservado:" ClientInstanceName="lblDado" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="ASPxLabel1"></dxcp:ASPxLabel>




                                                <dxcp:ASPxLabel runat="server" Text="*" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" ID="ASPxLabel8"></dxcp:ASPxLabel>




                                            </td>
                                            <td>
                                                <dxcp:ASPxLabel runat="server" Text="Dado:" ClientInstanceName="lblDado" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblDado"></dxcp:ASPxLabel>




                                                <dxtv:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#006600" Text="*">
                                                </dxtv:ASPxLabel>




                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="20" ClientInstanceName="txtCodigoReservado" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="txtCodigoReservado">
                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                </dxcp:ASPxTextBox>




                                                            </td>
                                                            <td valign="top" align="center" width="20">
                                                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o" ClientInstanceName="imgAjuda" Cursor="pointer" ID="imgAjuda" Visible="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	pcAjuda.Show();
}"></ClientSideEvents>
                                                                </dxcp:ASPxImage>




                                                            </td>
                                                            <td style="width: 10px" valign="top" align="center"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="padding-left: 0px" valign="top">
                                                <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="99" ClientInstanceName="txtDado" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="txtDado">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxTextBox>




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
                                <table cellspacing="0" cellpadding="0" width="720" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 166px">
                                                <dxcp:ASPxLabel runat="server" Text="Unidade de Medida:" ClientInstanceName="lblUnidadeDeMedida" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblUnidadeDeMedida"></dxcp:ASPxLabel>




                                                <dxtv:ASPxLabel ID="ASPxLabel11" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#006600" Text="*">
                                                </dxtv:ASPxLabel>




                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 166px">
                                                <dxcp:ASPxLabel runat="server" Text="Casas Decimais:" ClientInstanceName="lblCasasDecimais" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblCasasDecimais"></dxcp:ASPxLabel>




                                                <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#006600" Text="*">
                                                </dxtv:ASPxLabel>




                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <dxcp:ASPxLabel runat="server" Text="Agrupamento do Dado:" ClientInstanceName="lblAgrupamentoDoDado" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblAgrupamentoDoDado"></dxcp:ASPxLabel>




                                                <dxtv:ASPxLabel ID="ASPxLabel13" runat="server" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#006600" Text="*">
                                                </dxtv:ASPxLabel>




                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="cmbUnidadeDeMedida" Font-Names="Verdana" Font-Size="8pt" ID="cmbUnidadeDeMedida">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxComboBox>




                                            </td>
                                            <td></td>
                                            <td>
                                                <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="cmbCasasDecimais" Font-Names="Verdana" Font-Size="8pt" ID="cmbCasasDecimais">
                                                    <Items>
                                                        <dxcp:ListEditItem Text="0 (Zero)" Value="0"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="1 (Uma)" Value="1"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="2 (Duas)" Value="2"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="3 (Tr&#234;s)" Value="3"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="4 (Quatro)" Value="4"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="5 (Cinco)" Value="5"></dxcp:ListEditItem>
                                                        <dxcp:ListEditItem Text="6 (Seis)" Value="6"></dxcp:ListEditItem>
                                                    </Items>

                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxComboBox>




                                            </td>
                                            <td></td>
                                            <td>
                                                <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="cmbAgrupamentoDoDado" Font-Names="Verdana" Font-Size="8pt" ID="cmbAgrupamentoDoDado">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxComboBox>




                                            </td>
                                            <td valign="top" align="center">
                                                <img style="display: none; cursor: pointer" title="Mensagem para ajuda!" src="../../imagens/ajuda.png" /></td>
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
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 220px; height: 16px">
                                                <dxcp:ASPxLabel runat="server" Text="Valor M&#237;nimo:" ClientInstanceName="lblValorMinimo" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblValorMinimo"></dxcp:ASPxLabel>




                                            </td>
                                            <td style="width: 10px; height: 16px"></td>
                                            <td style="width: 220px; height: 16px">
                                                <dxcp:ASPxLabel runat="server" Text="Valor M&#225;ximo:" ClientInstanceName="lblValorMaximo" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="lblValorMaximo"></dxcp:ASPxLabel>




                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 220px">
                                                <dxcp:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n6}" ClientInstanceName="txtValorMinimo" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="txtValorMinimo">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxTextBox>




                                            </td>
                                            <td></td>
                                            <td style="width: 220px">
                                                <dxcp:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n6}" ClientInstanceName="txtValorMaximo" Font-Names="Verdana" Font-Size="8pt" Font-Strikeout="False" ID="txtValorMaximo">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxTextBox>




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
                                <dxcp:ASPxLabel runat="server" Text="Gloss&#225;rio:" ClientInstanceName="lblGlossario" Font-Names="Verdana" Font-Size="8pt" ID="lblGlossario"></dxcp:ASPxLabel>




                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heGlossario" Width="720px" Height="215px" Font-Names="Verdana" Font-Size="8pt" ID="heGlossario">
                                    <Toolbars>
                                        <dxhe:HtmlEditorToolbar>
                                            <Items>
                                                <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                                <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                                <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
                                                <dxhe:ToolbarPasteFromWordButton></dxhe:ToolbarPasteFromWordButton>
                                                <dxhe:ToolbarUndoButton BeginGroup="True"></dxhe:ToolbarUndoButton>
                                                <dxhe:ToolbarRedoButton></dxhe:ToolbarRedoButton>
                                                <dxhe:ToolbarRemoveFormatButton BeginGroup="True"></dxhe:ToolbarRemoveFormatButton>
                                                <dxhe:ToolbarSuperscriptButton BeginGroup="True"></dxhe:ToolbarSuperscriptButton>
                                                <dxhe:ToolbarSubscriptButton></dxhe:ToolbarSubscriptButton>
                                                <dxhe:ToolbarInsertOrderedListButton BeginGroup="True"></dxhe:ToolbarInsertOrderedListButton>
                                                <dxhe:ToolbarInsertUnorderedListButton></dxhe:ToolbarInsertUnorderedListButton>
                                                <dxhe:ToolbarIndentButton BeginGroup="True"></dxhe:ToolbarIndentButton>
                                                <dxhe:ToolbarOutdentButton></dxhe:ToolbarOutdentButton>
                                                <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True"></dxhe:ToolbarInsertLinkDialogButton>
                                                <dxhe:ToolbarUnlinkButton></dxhe:ToolbarUnlinkButton>
                                                <dxhe:ToolbarInsertImageDialogButton></dxhe:ToolbarInsertImageDialogButton>
                                                <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False"></dxhe:ToolbarCheckSpellingButton>
                                                <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                    <Items>
                                                        <dxhe:ToolbarInsertTableDialogButton Text="Insert Table..." ToolTip="Insert Table..." BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
                                                        <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True"></dxhe:ToolbarTablePropertiesDialogButton>
                                                        <dxhe:ToolbarTableRowPropertiesDialogButton></dxhe:ToolbarTableRowPropertiesDialogButton>
                                                        <dxhe:ToolbarTableColumnPropertiesDialogButton></dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                        <dxhe:ToolbarTableCellPropertiesDialogButton></dxhe:ToolbarTableCellPropertiesDialogButton>
                                                        <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True"></dxhe:ToolbarInsertTableRowAboveButton>
                                                        <dxhe:ToolbarInsertTableRowBelowButton></dxhe:ToolbarInsertTableRowBelowButton>
                                                        <dxhe:ToolbarInsertTableColumnToLeftButton></dxhe:ToolbarInsertTableColumnToLeftButton>
                                                        <dxhe:ToolbarInsertTableColumnToRightButton></dxhe:ToolbarInsertTableColumnToRightButton>
                                                        <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True"></dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                        <dxhe:ToolbarSplitTableCellVerticallyButton></dxhe:ToolbarSplitTableCellVerticallyButton>
                                                        <dxhe:ToolbarMergeTableCellRightButton></dxhe:ToolbarMergeTableCellRightButton>
                                                        <dxhe:ToolbarMergeTableCellDownButton></dxhe:ToolbarMergeTableCellDownButton>
                                                        <dxhe:ToolbarDeleteTableButton BeginGroup="True"></dxhe:ToolbarDeleteTableButton>
                                                        <dxhe:ToolbarDeleteTableRowButton></dxhe:ToolbarDeleteTableRowButton>
                                                        <dxhe:ToolbarDeleteTableColumnButton></dxhe:ToolbarDeleteTableColumnButton>
                                                        <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                                    </Items>
                                                </dxhe:ToolbarTableOperationsDropDownButton>
                                                <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                            </Items>
                                        </dxhe:HtmlEditorToolbar>
                                        <dxhe:HtmlEditorToolbar>
                                            <Items>
                                                <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                    <Items>
                                                        <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                    </Items>
                                                </dxhe:ToolbarParagraphFormattingEdit>
                                                <dxhe:ToolbarFontNameEdit>
                                                    <Items>
                                                        <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                    </Items>
                                                </dxhe:ToolbarFontNameEdit>
                                                <dxhe:ToolbarFontSizeEdit>
                                                    <Items>
                                                        <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                        <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                    </Items>
                                                </dxhe:ToolbarFontSizeEdit>
                                                <dxhe:ToolbarBoldButton BeginGroup="True"></dxhe:ToolbarBoldButton>
                                                <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                            </Items>
                                        </dxhe:HtmlEditorToolbar>
                                    </Toolbars>

                                    <Settings AllowHtmlView="False"></Settings>

                                    <SettingsHtmlEditing>
                                        <PasteFiltering Attributes="class"></PasteFiltering>
                                    </SettingsHtmlEditing>
                                </dxhe:ASPxHtmlEditor>




                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px; padding-top: 3px">
                                <dxcp:ASPxLabel runat="server" Text="(*) - C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o." ClientInstanceName="lblAjuda" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Size="7pt" ForeColor="#404040" ID="ASPxLabel9"></dxcp:ASPxLabel>




                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvar">
                                                    <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnSalvar)
	                {
	                    onClick_btnSalvar();
	                }
                }"></ClientSideEvents>

                                                    <Paddings Padding="0px"></Paddings>
                                                </dxcp:ASPxButton>




                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnCancelar)
	                {
                       onClick_btnCancelar();
	                   if(gvDados.GetVisibleRowsOnPage() == 0)
		                {
			                desabilitaBarraNavegacao(true);
		                }
		                else
		                {
			                desabilitaBarraNavegacao(false);
		                }
	                }
                	
                }"></ClientSideEvents>

                                                    <Paddings Padding="0px"></Paddings>
                                                </dxcp:ASPxButton>




                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>

    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDadoCompartilhado" HeaderText="Edi&#231;&#227;o de Dado Compartilhado" ShowCloseButton="False" Width="600px" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ID="pcDadoCompartilhado">
        <ClientSideEvents Closing="function(s, e) {
	txtCodigoReservadoDadoComp.SetText(&quot;&quot;);
}"></ClientSideEvents>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackResponsavel" Width="100%" ID="pnCallbackResponsavel" OnCallback="pnCallbackResponsavel_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	onEnd_CallbackResponsavel(s, e);
}"></ClientSideEvents>
                                    <PanelCollection>
                                        <dxcp:PanelContent runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 140px">
                                                            <dxcp:ASPxLabel runat="server" Text="C&#243;digo Reservado:" ClientInstanceName="lblCodigoResponsavelNovoResp" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ID="lblCodigoResponsavelNovoResp"></dxcp:ASPxLabel>







                                                            <dxcp:ASPxLabel runat="server" Text="*" ClientInstanceName="lblAjuda" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" ID="ASPxLabel2"></dxcp:ASPxLabel>







                                                        </td>
                                                        <td style="width: 25px" valign="top" align="center"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 140px">
                                                            <dxcp:ASPxTextBox runat="server" Width="130px" ClientInstanceName="txtCodigoReservadoDadoComp" ID="txtCodigoReservadoDadoComp"></dxcp:ASPxTextBox>







                                                        </td>
                                                        <td style="width: 25px" valign="top" align="center">
                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o" Height="18px" ClientInstanceName="imgAjudaDadoComp" Cursor="pointer" ID="imgAjudaDadoComp" Visible="False">
                                                                <ClientSideEvents Click="function(s, e) {
	                    pcAjuda.Show();
                    }"></ClientSideEvents>
                                                            </dxcp:ASPxImage>







                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 140px; height: 34px">
                                                            <dxcp:ASPxTextBox runat="server" Width="130px" ClientInstanceName="txtCodigoDadoCompartilhado" ClientVisible="False" ID="txtCodigoDadoCompartilhado"></dxcp:ASPxTextBox>







                                                        </td>
                                                        <td style="width: 25px; height: 34px" valign="top" align="center"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxcp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>


                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px; padding-top: 3px">
                                <dxcp:ASPxLabel runat="server" Text="(*) - C&#243;digo utilizado para interface com outros sistemas da institui&#231;&#227;o." ClientInstanceName="lblAjuda" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Size="7pt" ForeColor="#404040" ID="ASPxLabel3"></dxcp:ASPxLabel>


                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 90px">
                                                <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvarResponsavel" Text="Salvar" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvarResponsavel">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pnCallbackResponsavel.PerformCallback(&quot;salvar&quot;);
}"></ClientSideEvents>

                                                    <Paddings Padding="0px"></Paddings>
                                                </dxcp:ASPxButton>


                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 90px">
                                                <dxcp:ASPxButton runat="server" ClientInstanceName="btnFecharResponsavel" Text="Fechar" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="btnFecharResponsavel">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDadoCompartilhado.Hide();
}"></ClientSideEvents>

                                                    <Paddings Padding="0px"></Paddings>
                                                </dxcp:ASPxButton>


                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>

</asp:Content>

