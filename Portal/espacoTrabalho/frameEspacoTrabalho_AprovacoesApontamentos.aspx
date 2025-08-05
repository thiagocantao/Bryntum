<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AprovacoesApontamentos.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AprovacoesApontamentos"
    Title="Portal da Estratégia" %>



<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function (event) {
            window.top.SetBotaoSalvarVisivel(false);
        });
    </script>
    <style type="text/css">
        .border {
            border-bottom-color: black !important;
            /*border-left-color:black !important;*/
            border-top-color: black !important;
            border-right-color: black !important;
        }

        html .dxgvControl_MaterialCompact .dxgvTable_MaterialCompact .dxgvFocusedRow_MaterialCompact, .dxgvControl_MaterialCompact .dxgvTable_MaterialCompact .dxgvFocusedRow_MaterialCompact.dxgvDataRowHover_MaterialCompact {
            color: #000 !important;
        }
    </style>

    <table cellpadding="0" cellspacing="0" style="width: 100%" id="tbPrincipal">
        <tr>
            <td id="ConteudoPrincipal">
                <table cellpadding="0" cellspacing="0" style="width: 100%" id="tbCabecalhoPagina">
                    <tr>
                        <td align="left">
                            <table border="0" cellpadding="0" cellspacing="0" id="tbBotoes" runat="server">
                                <tr>
                                    <td>

                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" ClientInstanceName="menu0"
                                                        ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                            <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                                                <td>
                                                    <img title="Histórico de apontamentos" class="dx-vam" src="../imagens/colunas.png" alt="Meus apontamentos que aprovei" style="cursor: pointer" onclick="abreTelaMeusApontamentosAprovados();" id="gvDados_DXCBtn3Img">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0" style="margin-bottom: 10px; margin-top: 10px;" id="tabelaOpcoes">
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlAcao" runat="server"
                                            SelectedIndex="0" Width="357px" ClientInstanceName="ddlAcao">
                                            <Items>
                                                <dxe:ListEditItem Text="Aprovar Sele&#231;&#227;o" Value="EA" Selected="True"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Reprovar Sele&#231;&#227;o" Value="ER"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Deixar Sele&#231;&#227;o Pendente" Value="PA"></dxe:ListEditItem>
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td style="width: 110px" align="right">
                                        <dxe:ASPxButton ID="btnAplicar" runat="server" AutoPostBack="False" ClientInstanceName="btnAplicar"
                                            Text="Aplicar " Width="100px">
                                            <Paddings Padding="0px"></Paddings>

                                            <ClientSideEvents Click="function(s, e) {
	                                                        var msgStatus = &quot;&quot;;

	                                                        if(ddlAcao.GetValue() == &quot;EA&quot;)
		                                                        msgStatus = 'Deseja marcar os apontamentos selecionados para Aprovação?';
	                                                        else
	                                                        {
		                                                        if(ddlAcao.GetValue() == &quot;ER&quot;)
			                                                        msgStatus = 'Deseja marcar os apontamentos selecionados para Reprovação?';
		                                                        else
			                                                        msgStatus = 'Deseja marcar os apontamentos selecionados para Pendentes?';
	                                                        }
                                                            if(validaSelecaoGrid()){
                                                                var funcObj = { funcaoClickOK: function () { 
		                                                                                                    callBack.PerformCallback('A');
                                                                                                            } 
                                                                                }
                                                                var funcObjNOK = { funcaoClickNOK: function () { return; } }
                                                                window.top.mostraConfirmacao(msgStatus, function () { funcObj['funcaoClickOK']() } , function () { funcObjNOK['funcaoClickNOK']() });
                                                            }else{
                                                                window.top.mostraMensagem('Nenhum apontamento selecionado', 'atencao', true, false, null);
                                                                return;
                                                            }
                                                        }"></ClientSideEvents>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td style="width: 100px" align="right">
                                        <dxe:ASPxButton ID="btnPublicar" runat="server" AutoPostBack="False" ClientInstanceName="btnPublicar"
                                            Text="Publicar" Width="100px" ClientEnabled="False">
                                            <Paddings Padding="0px"></Paddings>
                                            <ClientSideEvents Click="function(s, e) {
	                                                        var funcObj = { funcaoClickOK: function () { 
		                                                                        callBack.PerformCallback('P');
                                                                            } 
                                                                        }
                                                            var funcObjNOK = { funcaoClickNOK: function () { return; } }
                                                            window.top.mostraConfirmacao(&quot;Deseja Publicar os Apontamentos?&quot;, function () { funcObj['funcaoClickOK']() } , function () { funcObjNOK['funcaoClickNOK']() });
                                                        }"></ClientSideEvents>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxcp:ASPxCallbackPanel ID="callBack" runat="server" ClientInstanceName="callBack" OnCustomJSProperties="callBack_CustomJSProperties"
                    Width="100%" OnCallback="callBack_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">

                            <table cellpadding="0" cellspacing="0" style="width: 100%;" id="tbGrid">
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                                            Width="100%" KeyFieldName="CodigoApontamentoAtribuicao" PreviewFieldName="CodigoApontamentoAtribuicao"
                                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback"
                                            OnAfterPerformCallback="gvDados_AfterPerformCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                            <Templates>
                                                <FooterRow>
                                                    <table class="grid-legendas" cellspacing="0" cellpadding="0" id="tbFooter">
                                                        <tbody id="corpoTbFooter">
                                                            <tr>
                                                                <td class="grid-legendas-icone">
                                                                    <dxe:ASPxImage ID="imgPendenteAprovacao" runat="server" ImageUrl="~/imagens/botoes/tarefasPALenda.png"
                                                                        Height="16px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                                    <dxe:ASPxLabel ID="lblPendenteAprovacao" runat="server"
                                                                        Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_pendente_aprova__o %>" Font-Bold="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="grid-legendas-icone">
                                                                    <dxe:ASPxImage ID="imgEmAprovacao" runat="server" ImageUrl="~/imagens/botoes/tarefasEA_pequeno.PNG"
                                                                        Height="16px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                                    <dxe:ASPxLabel ID="lblEmAprovacao" runat="server"
                                                                        Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_aprova__o %>" Font-Bold="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="grid-legendas-icone">
                                                                    <dxe:ASPxImage ID="imgPendenteReprovacao" runat="server" ImageUrl="~/imagens/botoes/tarefasER_pequeno.PNG"
                                                                        Height="16px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td class="grid-legendas-label grid-legendas-label-icone">
                                                                    <dxe:ASPxLabel ID="lblPendenteReprovacao" runat="server"
                                                                        Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_reprova__o %>" Font-Bold="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" SelectionStoringMode="DataIntegrityOptimized"></SettingsBehavior>
                                            <Styles>
                                                <FocusedRow ForeColor="Black">
                                                </FocusedRow>
                                                <CommandColumnItem VerticalAlign="Middle">
                                                    <Paddings Padding="3px" />
                                                </CommandColumnItem>
                                            </Styles>
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
     if(e.buttonID == &quot;btnComentarios&quot;)
     {
             abreComentarios(e.visibleIndex);	
     }
     else if(e.buttonID == &quot;btnAnexos&quot;)
     {
             abreAnexos(e.visibleIndex);	
     }
}"
                                                Init="function(s, e) {
	                                            var altura = window.top.getClientHeight(-175);
                                                            s.SetHeight(altura);
                                            }"></ClientSideEvents>
                                            <SettingsPopup>
                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>

                                            <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
                                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                                ShowFilterRow="True"></Settings>
                                            <StylesEditors>
                                                <ReadOnlyStyle Border-BorderStyle="None" BackColor="Transparent"></ReadOnlyStyle>

                                                <ReadOnly BackColor="Transparent">
                                                    <border borderstyle="None"></border>
                                                </ReadOnly>
                                                <TextBox BackColor="Transparent">
                                                </TextBox>
                                            </StylesEditors>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents BeginCallback="function(s,e){lpLoad.Show();}" />
                    <ClientSideEvents EndCallback="function(s, e) {
                        lpLoad.Hide();	
                        if(s.cp_OK != '')
                        {
                            window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                            //gvDados.PerformCallback();
                           
                        }
                        else if(s.cp_Erro != '')
                        {
                            window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                        }
                        else if(s.cp_MSG != '')
                        {
                            window.top.mostraMensagem(s.cp_MSG, 'Atencao', true, false, null);
                        }
                        var existemInformacoesASeremPublicadas = (s.cp_ExistemInformacoesASeremPublicadas == 'S') ? true: false;
                        btnPublicar.SetEnabled(existemInformacoesASeremPublicadas);
                        if(!existemInformacoesASeremPublicadas)
                            btnPublicar.GetMainElement().title = traducao.frameEspacoTrabalho_AprovacoesTarefas_n_o_existem_informa__es_a_serem_publicadas;
                         gvDados.UnselectAllRowsOnPage();
                        }" />
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    <dxcp:ASPxLoadingPanel ID="lpLoad" runat="server" ClientInstanceName="lpLoad" Text="Aguarde...&amp;hellip;">
    </dxcp:ASPxLoadingPanel>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <GroupFooter Font-Bold="True">
            </GroupFooter>
            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#gvDados_emptyheader').text() == 'Arraste uma coluna aqui...') {
                $('#gvDados_emptyheader').html('');
                $('#gvDados_DXEmptyRow > td').eq(1).remove();
            }
        });
    </script>
</asp:Content>




