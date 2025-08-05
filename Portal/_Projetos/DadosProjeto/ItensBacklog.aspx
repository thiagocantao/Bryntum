<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItensBacklog.aspx.cs" Inherits="_Projetos_DadosProjeto_ItensBacklog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Backlog</title>
    <script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
    </script>
    <style type="text/css">
        .style2 {
            width: 100%;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div style="width: 100%">
            <dxcp:ASPxGridView ID="tlDados" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="tlDados" KeyFieldName="CodigoItem" ParentFieldName="CodigoItemSuperior" OnProcessDragNode="tlDados_ProcessDragNode" OnCommandColumnButtonInitialize="tlDados_CommandColumnButtonInitialize" OnFocusedRowChanged="tlDados_FocusedNodeChanged">
                <SettingsBehavior EnableCustomizationWindow="True" />
                <Columns>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoItem" ShowInCustomizationForm="False" Visible="False" VisibleIndex="1">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn Caption="Título do Item" FieldName="TituloItem" ShowInCustomizationForm="True" VisibleIndex="2" SortIndex="0" SortOrder="Ascending" Width="400px">
                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" ShowInFilterControl="Default" />
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="False" Visible="False" VisibleIndex="3">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoItemSuperior" ShowInCustomizationForm="False" Visible="False" VisibleIndex="4">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="DetalheItem" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn Caption="Status" FieldName="DescricaoTipoStatusItem" ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="Sprint" ShowInCustomizationForm="True" VisibleIndex="7" Width="300px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoTipoStatusItem" ShowInCustomizationForm="False" Visible="False" VisibleIndex="8">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn Caption="Classificação" FieldName="DescricaoTipoClassificacaoItem" ShowInCustomizationForm="True" VisibleIndex="9" Width="200px" Visible="False">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoTipoClassificacaoItem" ShowInCustomizationForm="False" Visible="False" VisibleIndex="10">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="11">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="DataInclusao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="12">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoUsuarioUltimaAlteracao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="13">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="DataUltimaAlteracao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="14">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoUsuarioExclusao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="15">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="PercentualConcluido" ShowInCustomizationForm="False" Visible="False" VisibleIndex="16">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoIteracao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="17">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="Importancia" ShowInCustomizationForm="True" VisibleIndex="18" Caption="Importância" Width="100px">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                        <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn Caption="Esforço Previsto (h)" FieldName="EsforcoPrevisto" ShowInCustomizationForm="True" VisibleIndex="20" Width="150px">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="IndicaQuestao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="22">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="IndicaBloqueioItem" ShowInCustomizationForm="False" Visible="False" VisibleIndex="23">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoWorkflow" ShowInCustomizationForm="False" Visible="False" VisibleIndex="24">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoInstanciaWf" ShowInCustomizationForm="False" Visible="False" VisibleIndex="25">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoCronogramaProjetoReferencia" ShowInCustomizationForm="False" Visible="False" VisibleIndex="26">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoTarefaReferencia" ShowInCustomizationForm="False" Visible="False" VisibleIndex="27">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoCliente" ShowInCustomizationForm="False" Visible="False" VisibleIndex="28">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="NomeCliente" ShowInCustomizationForm="False" Visible="False" VisibleIndex="29">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="CodigoTipoTarefaTimeSheet" ShowInCustomizationForm="False" Visible="False" VisibleIndex="30">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="DescricaoTipoTarefaTimeSheet" ShowInCustomizationForm="False" Visible="False" VisibleIndex="31">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn FieldName="ReceitaPrevista" ShowInCustomizationForm="False" Visible="False" VisibleIndex="32">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="110px" FixedStyle="Left">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar">
                                <Image AlternateText="Editar" ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir">
                                <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <CellStyle HorizontalAlign="Center">
                            <Paddings Padding="0px" PaddingRight="5px" />
                        </CellStyle>
                        <HeaderCaptionTemplate>
                            <table align="center" cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td>
                                        <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init1" OnItemClick="menu_ItemClick1">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                    <Items>
                                                        <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                    </Items>
                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                    </Image>
                                                </dxtv:MenuItem>
                                                <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                    <Items>
                                                        <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                            <Image IconID="save_save_16x16">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                        <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                            <Image IconID="actions_reset_16x16">
                                                            </Image>
                                                        </dxtv:MenuItem>
                                                    </Items>
                                                    <Image Url="~/imagens/botoes/layout.png">
                                                    </Image>
                                                </dxtv:MenuItem>
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
                                        </dxtv:ASPxMenu>
                                    </td>
                                </tr>
                            </table>
                        </HeaderCaptionTemplate>
                    </dxwgv:GridViewCommandColumn>
                    <dxw:GridViewDataDateColumn Caption="Data Alvo" FieldName="DataAlvo" Visible="False" VisibleIndex="34">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                        </PropertiesDateEdit>
                    </dxw:GridViewDataDateColumn>
                    <dxuc:GridViewDataTextColumn Caption="Tags" FieldName="TagItem" ShowInCustomizationForm="True" VisibleIndex="35" Width="300px">
                        <Settings AutoFilterCondition="Default" ShowInFilterControl="Default" />
                    </dxuc:GridViewDataTextColumn>
                </Columns>
                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowAutoFilter="True"></SettingsBehavior>
                <SettingsPager PageSize="30">
                </SettingsPager>
                <SettingsText CommandDelete="Excluir" ConfirmDelete="Deseja realmente excluir o item de backlog?" />
                <SettingsPopup>
                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" Height="200px" />
                </SettingsPopup>
                <Styles>
                    <Cell Wrap="True">
                    </Cell>
                </Styles>
                <ClientSideEvents CustomButtonClick="function(s, e) {     
     s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
     if(e.buttonID == &quot;btnEditar&quot;)
     {
	        s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoItem;CodigoItemSuperior;',atualizaItensBacklog);
     }
     if(e.buttonID == &quot;btnExcluir&quot;)
     {
                     s.GetRowValues(s.GetFocusedRowIndex(), &quot;CodigoItem;IndicaSeItemBacklogTemTarefaAssociada;&quot;,excluiItemBacklog);              
     }
}"
                    BeginCallback="function(s, e) {
	comando = e.command;
}"
                    EndCallback="function(s, e) 
{
       //alert(comando);
       if(s.cpErro != &quot;&quot; && s.cpErro != undefined)
       {
              window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
       }
       else
       {
              if(s.cpSucesso != &quot;&quot; && s.cpSucesso != undefined)
             {
                           window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
              } 
       }
 }"
                    ContextMenu="function(s, e) {
        if (e.objectType == &quot;header&quot;)
                                   pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}" Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 25);
}" />
            </dxcp:ASPxGridView>
            <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
                <Items>
                    <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
                    </dxm:MenuItem>
                </Items>
                <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
       tlDados.ShowCustomizationWindow();
}" />
            </dxm:ASPxPopupMenu>
        </div>
        <dxtv:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
      if(s.cp_Erro != '')
      {
               window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
      }
      else
      {
                 tlDados.Refresh();	
                 if(s.cp_Sucesso != '')
                 {
                            window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                 }
       }
}"></ClientSideEvents>
        </dxtv:ASPxCallback>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>
        <dxtv:ASPxGridViewExporter ID="ASPxTreeListExporter1" runat="server" GridViewID="tlDados" OnRenderBrick="ASPxTreeListExporter1_RenderBrick1">
            <Styles>
                <Header Font-Bold="True" Font-Names="roboto">
                </Header>
                <Cell Font-Names="roboto">
                </Cell>
            </Styles>
        </dxtv:ASPxGridViewExporter>
        <asp:SqlDataSource ID="sdsRecursoCorporativo" runat="server" ProviderName="System.Data.SqlClient" SelectCommand=" SELECT rp.CodigoRecursoProjeto, rp.CodigoRecursoCorporativo, rp.CodigoTipoPapelRecursoProjeto, rc.NomeRecurso, tpr.DescricaoTipoPapelRecurso FROM Agil_RecursoProjeto AS rp INNER JOIN vi_RecursoCorporativo AS rc ON rc.CodigoRecursoCorporativo = rp.CodigoRecursoCorporativo INNER JOIN Agil_TipoPapelRecurso AS tpr ON tpr.CodigoTipoPapelRecurso = rp.CodigoTipoPapelRecursoProjeto WHERE (rp.CodigoProjeto = @CodigoProjeto) ORDER BY rc.NomeRecurso">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="CodigoProjeto" QueryStringField="IDProjeto" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxcp:ASPxCallback ID="callbackVerificaSeTemTarefasAssociadas" runat="server" ClientInstanceName="callbackVerificaSeTemTarefasAssociadas" OnCallback="callbackVerificaSeTemTarefasAssociadas_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
//alert('fez  o performcallback e trouxe o valor: ' +s.cp_ItemBacklogTemTarefaAssociada );	
hfGeral.Set(&quot;ItemBacklogTemTarefaAssociada&quot;, s.cp_ItemBacklogTemTarefaAssociada);
}" />
        </dxcp:ASPxCallback>
        <dxtv:ASPxCallback ID="callbackPopupItemBacklog" runat="server" ClientInstanceName="callbackPopupItemBacklog" OnCallback="callbackPopupItemBacklog_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
      var codigoItem = s.cpCodigoItem;
      var acao = s.cpAcao;
      var codigoProjeto = s.cpCodigoProjeto;
      var descricaoItemSuperior = s.cpDescricaoItemSuperior;
      var codigoItemSuperior = s.cpCodigoItemSuperior;
      var tituloItem = s.cpTituloItem;
     var codigoProjetoAgil = s.cpCodigoProjetoAgil;
      var url = window.top.pcModal.cp_Path +  '_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=' + codigoItem  + &quot;&amp;acao=&quot; + acao + &quot;&amp;IDProjeto=&quot; + codigoProjeto + '&amp;CIS=' + codigoItemSuperior + '&amp;CPA=' + codigoProjeto + '&amp;ALT=' + (screen.height - 270).toString() + &quot;&amp;tela=itensBacklog.aspx&quot;;
      window.top.showModalComFooter(url, (acao == 'incluir')? tituloItem : descricaoItemSuperior , null, null, &nbsp; atualizaGridItensBacklog, null);
}"></ClientSideEvents>
        </dxtv:ASPxCallback>
    </form>
</body>
</html>
