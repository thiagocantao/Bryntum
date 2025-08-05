<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AprovacoesTarefas.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AprovacoesTarefas"
    Title="Portal da Estratégia" %>



<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

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

    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td id="ConteudoPrincipal">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left">
                            <table border="0" cellpadding="0" cellspacing="0" id="tbBotoes" runat="server">
                                <tr>
                                    <td>
                                        <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" ClientInstanceName="menu"
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
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0" style="margin-bottom: 10px; margin-top: 10px;">
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
                                        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                            Text="Aplicar " Width="100px">
                                            <Paddings Padding="0px"></Paddings>
                                            <ClientSideEvents Click="function(s, e) {
	var msgStatus = &quot;&quot;;

	if(ddlAcao.GetValue() == &quot;EA&quot;)
		msgStatus = traducao.frameEspacoTrabalho_AprovacoesTarefas_deseja_marcar_as_tarefas_selecionadas_para_aprova__231___227_o_;
	else
	{
		if(ddlAcao.GetValue() == &quot;ER&quot;)
			msgStatus = traducao.frameEspacoTrabalho_AprovacoesTarefas_deseja_marcar_as_tarefas_selecionadas_para_reprova__231___227_o_;
		else
			msgStatus = traducao.frameEspacoTrabalho_AprovacoesTarefas_deseja_marcar_as_tarefas_selecionadas_para_pendentes_;
	}

	if(confirm(msgStatus))
      {
        lpLoad.Show();
		callBack.PerformCallback('A');
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
	if(confirm(&quot;Deseja Publicar as Tarefas?&quot;)){
        lpLoad.Show();
		callBack.PerformCallback('P');
        }
}"></ClientSideEvents>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                                Width="100%" KeyFieldName="CodigoAtribuicao" PreviewFieldName="CodigoAtribuicao"
                                OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback"
                                OnAfterPerformCallback="gvDados_AfterPerformCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                <Templates>
                                    <FooterRow>
                                        <table class="grid-legendas" cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/botoes/tarefasPALenda.png"
                                                            Height="16px">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_pendente_aprova__o %>" Font-Bold="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/botoes/tarefasEA_pequeno.PNG"
                                                            Height="16px">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_aprova__o %>" Font-Bold="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="grid-legendas-icone">
                                                        <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/botoes/tarefasER_pequeno.PNG"
                                                            Height="16px">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                            Text="<%# Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_reprova__o %>" Font-Bold="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </FooterRow>
                                </Templates>
                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
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
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback" OnCustomJSProperties="callBack_CustomJSProperties">
        <ClientSideEvents EndCallback="function(s, e) {
                lpLoad.Hide();	
               if(s.cp_OK != '')
              {
                          window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                          gvDados.PerformCallback();
              }
             else if(s.cp_Erro != '')
              {
                          window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
               }
               else if(s.cp_MSG != '')
               {
                           window.top.mostraMensagem(s.cp_MSG, 'Atencao', true, false, null);
                }
            var existemInformacoesASeremPublicadas = s.cp_ExistemInformacoesASeremPublicadas == 'S';
            btnPublicar.SetEnabled(existemInformacoesASeremPublicadas);
            if(!existemInformacoesASeremPublicadas)
                btnPublicar.GetMainElement().title = traducao.frameEspacoTrabalho_AprovacoesTarefas_n_o_existem_informa__es_a_serem_publicadas;
}" />
    </dxcb:ASPxCallback>
    <dxcp:ASPxLoadingPanel ID="lpLoad" runat="server" ClientInstanceName="lpLoad" Text="Aguarde...&amp;hellip;">
    </dxcp:ASPxLoadingPanel>
    <dxcp:ASPxPopupControl ID="pcOutrosCustosEmAprovacao" runat="server" ClientInstanceName="pcOutrosCustosEmAprovacao" ShowHeader="False" ShowFooter="True" Width="750px" AllowDragging="True" CloseAction="None" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ClientSideEvents PopUp="function(s, e) {
var largura = Math.max(0, document.documentElement.clientWidth) - 100;
    var altura = Math.max(0, document.documentElement.clientHeight) - 155;

    s.SetWidth(largura);
    s.SetHeight(altura);	   
s.UpdatePosition();
}" />
        <FooterTemplate>
            <div style="float: right;">
                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharPcOutrosCustos" Text="Fechar" Width="90px" ID="btnFecharPcOutrosCustos" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s, e) {
pcOutrosCustosEmAprovacao.Hide();
                    }"></ClientSideEvents>

                                    <Paddings Padding="0px"></Paddings>
                                </dxcp:ASPxButton>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </FooterTemplate>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvOutrosCustosEmAprovacao" KeyFieldName="CodigoAtribuicao"
                    AutoGenerateColumns="False" Width="100%"
                    ID="gvOutrosCustosEmAprovacao" EnableTheming="False" OnCustomCallback="gvOutrosCustosEmAprovacao_CustomCallback" Font-Names="Verdana" Font-Size="7pt" OnHtmlDataCellPrepared="gvOutrosCustosEmAprovacao_HtmlDataCellPrepared">
                    <ClientSideEvents Init="function(s, e) {
	    var height =window.top.getClientHeight(-200);
                    s.SetHeight(height);
}"
                        BeginCallback="function(s, e) {
	comando = e.command;
}"
                        EndCallback="function(s, e) {
if(comando == 'CUSTOMCALLBACK')
{
          pcOutrosCustosEmAprovacao.Show();
          pcOutrosCustosEmAprovacao.UpdatePosition();
    lpLoad.Hide();
}
}"></ClientSideEvents>

                    <SettingsPopup>
                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>

                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoAtribuicao" VisibleIndex="0" ShowInCustomizationForm="True" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="UnidadeAtribuicaoRealInformado"
                            VisibleIndex="4" ShowInCustomizationForm="True" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CustoUnitarioRealInformado" VisibleIndex="7" ShowInCustomizationForm="True" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn FieldName="CustoRealInformado" ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewBandColumn Caption="Previsto" ShowInCustomizationForm="True" VisibleIndex="12">
                            <HeaderStyle BackColor="#E1DFCD" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" HorizontalAlign="Center" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn Caption="Valor Total" FieldName="CustoPrevisto" ShowInCustomizationForm="True" VisibleIndex="2" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoPrevisto" ShowInCustomizationForm="True" VisibleIndex="0" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="n2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="CustoUnitarioPrevisto" ShowInCustomizationForm="True" VisibleIndex="1" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxtv:GridViewBandColumn>
                        <dxtv:GridViewBandColumn Caption="Realizado" ShowInCustomizationForm="True" VisibleIndex="13">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoRealInformado" ShowInCustomizationForm="True" VisibleIndex="0" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="n2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="CustoUnitarioRealInformado" ShowInCustomizationForm="True" VisibleIndex="1" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor Total" FieldName="CustoRealInformado" ShowInCustomizationForm="True" VisibleIndex="2" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxtv:GridViewBandColumn>
                        <dxtv:GridViewBandColumn Caption="Restante" ShowInCustomizationForm="True" VisibleIndex="14">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="CustoUnitarioRestanteInformado" ShowInCustomizationForm="True" VisibleIndex="1" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor Total" FieldName="CustoRestanteInformado" ShowInCustomizationForm="True" VisibleIndex="2" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="c2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoRestanteInformado" ShowInCustomizationForm="True" VisibleIndex="0" Width="9%">
                                    <PropertiesTextEdit DisplayFormatString="n2">
                                    </PropertiesTextEdit>
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxtv:GridViewBandColumn>
                        <dxtv:GridViewBandColumn Caption=" " Name="bandRecursoUnidade" ShowInCustomizationForm="True" VisibleIndex="1">
                            <Columns>
                                <dxtv:GridViewDataTextColumn Caption="Recurso" FieldName="NomeRecurso" ShowInCustomizationForm="True" VisibleIndex="0">
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Und." FieldName="UnidadeMedidaRecurso" ShowInCustomizationForm="True" VisibleIndex="1" Width="3%">
                                    <FilterCellStyle BackColor="#E1E1E1">
                                    </FilterCellStyle>
                                    <HeaderStyle BackColor="#E1E1E1" />
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxtv:GridViewBandColumn>
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" AllowSort="False"></SettingsBehavior>
                    <Templates>
                        <TitlePanel>
                            <div style="float: left">
                                <dxtv:ASPxLabel ID="ASPxLabel11" runat="server" Text="Outros custos informados para a aprovação" Width="100%" Font-Size="15pt" Font-Names="Verdana" ForeColor="Black">
                                </dxtv:ASPxLabel>
                            </div>

                        </TitlePanel>
                    </Templates>
                    <SettingsPager PageSize="100" Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" ShowHeaderFilterListBoxSearchUI="False" ShowTitlePanel="True"></Settings>
                    <Border BorderStyle="Solid" />
                </dxwgv:ASPxGridView>

            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
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




