<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comentarios.aspx.cs" Inherits="_Projetos_Agil_Comentarios" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title></title>
      <style>
          .tabela-comentario {
              width: 100%;
              border: 1px lightgray solid;
              border-radius: 10px;
              margin: 10px 0px;
              padding: 5px;
          }

          .div-btn-add-comment {
              float: right;
              padding: 10px 0px 20px;
          }

          .iniciaisMaiusculas {
              text-transform: capitalize !important
          }

          .indent {
              margin-bottom: 24px
          }
      </style>
      <script type="text/javascript">
          var textSeparator = ";";
          function updateText() {
              var selectedItems = checkListBox.GetSelectedItems();
              checkComboBox.SetText(getSelectedItemsText(selectedItems));
              hfItensSelecionados.PerformCallback('1');
          }
          function synchronizeListBoxValues(dropDown, args) {
              checkListBox.UnselectAll();
              var texts = dropDown.GetText().split(textSeparator);
              var values = getValuesByTexts(texts);
              checkListBox.SelectValues(values);
              updateText();
          }
          function getSelectedItemsText(items) {
              var texts = [];
              for (var i = 0; i < items.length; i++)
                  texts.push(items[i].text);
              return texts.join(textSeparator);
          }
          function getValuesByTexts(texts) {
              var actualValues = [];
              var item;
              for (var i = 0; i < texts.length; i++) {
                  item = checkListBox.FindItemByText(texts[i]);
                  if (item != null)
                      actualValues.push(item.value);
              }
              return actualValues;
          }

          function excluirComentario(codigocomentario, codigoentidade, codigousuario) {
              //alert('passou codigo comentario -> ' + codigocomentario + ' codigoentidade -> ' + codigoentidade + ' usuario -> ' + codigousuario);
              var funcObj = { funcaoClickOK: function (cod) { callbackPanel.PerformCallback(cod); } }
              window.top.mostraConfirmacao("Deseja realmente excluir o comentário?", function () { funcObj['funcaoClickOK'](codigocomentario) }, null);
          }

          function editarComentario(codigocomentario, codigoentidade, codigousuario) {
              var url1 = window.top.pcModal.cp_Path + '_Projetos/Agil/atualizaComentario.aspx?cc=' + codigocomentario + '&ce=' + codigoentidade + '&u=' + codigousuario;
              window.top.showModal2(url1, 'Atualiza Comentário', null, null, funcaoPosModal, null);
          }

          function funcaoPosModal() {
              callbackPanel.PerformCallback('');
          }

      </script>
   </head>
   <body>
      <form id="form1" runat="server">
         <input id="retorno_popup" name="retorno_popup" runat="server" type="hidden" value="" />
          <div margin: 0px 10px;">
         <dxhe:ASPxHtmlEditor ID="htmlComentario" runat="server" Theme="MaterialCompact" Width="100%" ClientInstanceName="htmlComentario" Height="200px">
            <Toolbars>
               <dxhe:HtmlEditorToolbar Name="StandardToolbar1">
                  <Items>
                     <dxhe:ToolbarCutButton AdaptivePriority="2">
                     </dxhe:ToolbarCutButton>
                     <dxhe:ToolbarCopyButton AdaptivePriority="2">
                     </dxhe:ToolbarCopyButton>
                     <dxhe:ToolbarPasteButton AdaptivePriority="2">
                     </dxhe:ToolbarPasteButton>
                     <dxhe:ToolbarPasteFromWordButton AdaptivePriority="2" Visible="False">
                     </dxhe:ToolbarPasteFromWordButton>
                     <dxhe:ToolbarUndoButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarUndoButton>
                     <dxhe:ToolbarRedoButton AdaptivePriority="1">
                     </dxhe:ToolbarRedoButton>
                     <dxhe:ToolbarRemoveFormatButton AdaptivePriority="2" BeginGroup="True" Visible="False">
                     </dxhe:ToolbarRemoveFormatButton>
                     <dxhe:ToolbarSuperscriptButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarSuperscriptButton>
                     <dxhe:ToolbarSubscriptButton AdaptivePriority="1">
                     </dxhe:ToolbarSubscriptButton>
                     <dxhe:ToolbarInsertOrderedListButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarInsertOrderedListButton>
                     <dxhe:ToolbarInsertUnorderedListButton AdaptivePriority="1">
                     </dxhe:ToolbarInsertUnorderedListButton>
                     <dxhe:ToolbarIndentButton AdaptivePriority="2" BeginGroup="True">
                     </dxhe:ToolbarIndentButton>
                     <dxhe:ToolbarOutdentButton AdaptivePriority="2">
                     </dxhe:ToolbarOutdentButton>
                     <dxhe:ToolbarInsertLinkDialogButton AdaptivePriority="1" BeginGroup="True" Visible="False">
                     </dxhe:ToolbarInsertLinkDialogButton>
                     <dxhe:ToolbarUnlinkButton AdaptivePriority="1" Visible="False">
                     </dxhe:ToolbarUnlinkButton>
                     <dxhe:ToolbarInsertImageDialogButton AdaptivePriority="1" Visible="False">
                     </dxhe:ToolbarInsertImageDialogButton>
                     <dxhe:ToolbarTableOperationsDropDownButton AdaptivePriority="2" BeginGroup="True" Visible="False">
                        <Items>
                           <dxhe:ToolbarInsertTableDialogButton BeginGroup="True" Text="Insert Table..." ToolTip="Insert Table...">
                           </dxhe:ToolbarInsertTableDialogButton>
                           <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True">
                           </dxhe:ToolbarTablePropertiesDialogButton>
                           <dxhe:ToolbarTableRowPropertiesDialogButton>
                           </dxhe:ToolbarTableRowPropertiesDialogButton>
                           <dxhe:ToolbarTableColumnPropertiesDialogButton>
                           </dxhe:ToolbarTableColumnPropertiesDialogButton>
                           <dxhe:ToolbarTableCellPropertiesDialogButton>
                           </dxhe:ToolbarTableCellPropertiesDialogButton>
                           <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True">
                           </dxhe:ToolbarInsertTableRowAboveButton>
                           <dxhe:ToolbarInsertTableRowBelowButton>
                           </dxhe:ToolbarInsertTableRowBelowButton>
                           <dxhe:ToolbarInsertTableColumnToLeftButton>
                           </dxhe:ToolbarInsertTableColumnToLeftButton>
                           <dxhe:ToolbarInsertTableColumnToRightButton>
                           </dxhe:ToolbarInsertTableColumnToRightButton>
                           <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                           </dxhe:ToolbarSplitTableCellHorizontallyButton>
                           <dxhe:ToolbarSplitTableCellVerticallyButton>
                           </dxhe:ToolbarSplitTableCellVerticallyButton>
                           <dxhe:ToolbarMergeTableCellRightButton>
                           </dxhe:ToolbarMergeTableCellRightButton>
                           <dxhe:ToolbarMergeTableCellDownButton>
                           </dxhe:ToolbarMergeTableCellDownButton>
                           <dxhe:ToolbarDeleteTableButton BeginGroup="True">
                           </dxhe:ToolbarDeleteTableButton>
                           <dxhe:ToolbarDeleteTableRowButton>
                           </dxhe:ToolbarDeleteTableRowButton>
                           <dxhe:ToolbarDeleteTableColumnButton>
                           </dxhe:ToolbarDeleteTableColumnButton>
                        </Items>
                     </dxhe:ToolbarTableOperationsDropDownButton>
                     <dxhe:ToolbarFindAndReplaceDialogButton AdaptivePriority="2" BeginGroup="True">
                     </dxhe:ToolbarFindAndReplaceDialogButton>
                     <dxhe:ToolbarFullscreenButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarFullscreenButton>
                  </Items>
               </dxhe:HtmlEditorToolbar>
               <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                  <Items>
                     <dxhe:ToolbarParagraphFormattingEdit AdaptivePriority="2" Width="120px">
                        <Items>
                           <dxhe:ToolbarListEditItem Text="Normal" Value="p" />
                           <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1" />
                           <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2" />
                           <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3" />
                           <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4" />
                           <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5" />
                           <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6" />
                           <dxhe:ToolbarListEditItem Text="Address" Value="address" />
                           <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                        </Items>
                     </dxhe:ToolbarParagraphFormattingEdit>
                     <dxhe:ToolbarFontNameEdit AdaptivePriority="2">
                        <Items>
                           <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                           <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                           <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                           <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                           <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                           <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                           <dxhe:ToolbarListEditItem Text="Segoe UI" Value="Segoe UI" />
                        </Items>
                     </dxhe:ToolbarFontNameEdit>
                     <dxhe:ToolbarFontSizeEdit AdaptivePriority="2">
                        <Items>
                           <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                           <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                           <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                           <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                           <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                           <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                           <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                        </Items>
                     </dxhe:ToolbarFontSizeEdit>
                     <dxhe:ToolbarBoldButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarBoldButton>
                     <dxhe:ToolbarItalicButton AdaptivePriority="1">
                     </dxhe:ToolbarItalicButton>
                     <dxhe:ToolbarUnderlineButton AdaptivePriority="1">
                     </dxhe:ToolbarUnderlineButton>
                     <dxhe:ToolbarStrikethroughButton AdaptivePriority="1">
                     </dxhe:ToolbarStrikethroughButton>
                     <dxhe:ToolbarJustifyLeftButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarJustifyLeftButton>
                     <dxhe:ToolbarJustifyCenterButton AdaptivePriority="1">
                     </dxhe:ToolbarJustifyCenterButton>
                     <dxhe:ToolbarJustifyRightButton AdaptivePriority="1">
                     </dxhe:ToolbarJustifyRightButton>
                     <dxhe:ToolbarBackColorButton AdaptivePriority="1" BeginGroup="True">
                     </dxhe:ToolbarBackColorButton>
                     <dxhe:ToolbarFontColorButton AdaptivePriority="1">
                     </dxhe:ToolbarFontColorButton>
                  </Items>
               </dxhe:HtmlEditorToolbar>
            </Toolbars>
            <Settings AllowHtmlView="False" AllowPreview="False">
            </Settings>
            <SettingsHtmlEditing>
               <PasteFiltering Attributes="class"></PasteFiltering>
            </SettingsHtmlEditing>
            <SettingsValidation ErrorText="Nenhum comentário foi informado" ValidationGroup="grp_comment">
               <RequiredField ErrorText="Nenhum comentário foi informado" IsRequired="True" />
            </SettingsValidation>
         </dxhe:ASPxHtmlEditor>
         <table class="div-btn-add-comment" border="0" cellspacing="5">
            <tbody>
               <tr>
                  <td>
                     <asp:Label Font-Names="Verdana" ForeColor="Gray" Font-Size="14px" Font-Bold="true" ID="lbListBox" runat="server" Text="Ao adicionar o comentário, enviar para" />
                  </td>
                  <td>
                     <dxe:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit" Width="385px" runat="server" AnimationType="None" ValidationSettings-ValidateOnLeave="False">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                           <dxe:ASPxListBox runat="server" ID="checkListBox" Width="385" Height="150" FilteringSettings-ShowSearchUI="true" TextField="NomeRecurso" ValueField="EmailRecurso" CallbackPageSize="15" ClientInstanceName="checkListBox" OnCallback="listaEquipe_Callback" EncodeHtml="False">
                              <FilteringSettings ShowSearchUI="true" />
                              <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                           </dxe:ASPxListBox>
                           <table style="width: 100%">
                              <tr>
                                 <td style="padding: 4px">
                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Fechar" style="float: right" CausesValidation="False">
                                       <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                    </dx:ASPxButton>
                                 </td>
                              </tr>
                           </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                     </dxe:ASPxDropDownEdit>
                     <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfItensSelecionados" ID="hfItensSelecionados"
                        OnCustomCallback="hfItensSelecionados_CustomCallback">                                                            
                     </dxhf:ASPxHiddenField>
                  </td>
                  <td>
                    <dxcp:ASPxButton ID="btnSalvarComentario" runat="server" AutoPostBack="False" Text="Adicionar comentário" Theme="MaterialCompact" ClientInstanceName="btnSalvarComentario"  CssClass="iniciaisMaiusculas" ValidationGroup="grp_comment">
                        <ClientSideEvents Click="function(s, e) {
                           if(htmlComentario.GetIsValid())
                           callbackPanel.PerformCallback('ins');
                           }" />
                     </dxcp:ASPxButton>

                  </td>
                  <td id="mostraBotaoFechar" runat="server">
                     <dxcp:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" Text="Cancelar" Theme="MaterialCompact" ClientInstanceName="btnFechar"  CssClass="iniciaisMaiusculas" ValidationGroup="grp_comment">
                        <ClientSideEvents Click="function(s, e) {
                   document.getElementById('retorno_popup').value = null;
                    if(window.top.pcModal2.GetVisible() == true &amp;&amp; window.top.pcModal.GetVisible() == true)
                    {
                       window.top.fechaModal2();
                    }
                    else
                    {
                          if(window.top.pcModal.GetVisible() == true)
                          {
                                 window.top.fechaModal();
                          }
                    }
}" />
                         <Paddings PaddingLeft="5px" />
                     </dxcp:ASPxButton>
                  </td>
               </tr>
            </tbody>
         </table>
         <dxcp:ASPxCallbackPanel ID="callbackPanel" runat="server" Width="100%" ClientInstanceName="callbackPanel" OnCallback="callbackPanel_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
              if(s.cpErro != '')
              {
           window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
              }
              else if(s.cpSucesso != '')
              {
           window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
              } 
              else
             {
                          htmlComentario.SetHtml(null);
                          if(s.cpFecharAoSalvar == 'S')
                          {
                                    var codigo = s.cpCodigo;
                                    document.getElementById('retorno_popup').value = codigo;
                                    if(window.top.pcModal2.GetVisible() == true &amp;&amp; window.top.pcModal.GetVisible() == true)
                                    {
                                              window.top.fechaModal2();
                                     }
                                    else
                                   {
                                           if(window.top.pcModal.GetVisible() == true)
                                          {
                                                  window.top.fechaModal();
                                         }
                                  }
                          }
              }
             s.cpErro = '';
             s.cpSucesso = ''; 
}" />
            <PanelCollection>
               <dxcp:PanelContent runat="server">
                  <asp:Repeater ID="repeater" runat="server" DataSourceID="sdsComentario">
                     <ItemTemplate>
                        <table class="tabela-comentario">
                           <thead>
                              <tr>
                                 <td>
                                    <asp:Label Font-Names="Verdana" ForeColor="Gray" Font-Size="14px" Font-Bold="true" ID="NomeUsuarioLabel" runat="server" Text='<%# Eval("NomeCompletoUsuarioComentario") %>' />
                                    <asp:Label Font-Names="Verdana" ForeColor="Gray" Font-Size="85%" ID="DataInclusaoLabel" runat="server" ToolTip='<%# setlabelDataInclusaoAlteracao(Eval("IndicaComentarioEditado"), Eval("DataInclusao"), Eval("DataUltimaAtualizacao")) %>' Text='<%# setlabelDataInclusaoAlteracao(Eval("IndicaComentarioEditado"), Eval("DataInclusao"), Eval("DataUltimaAtualizacao")) %>' />
                                    <asp:Label Font-Names="Verdana" ForeColor="Gray" Font-Size="85%" ID="Label1" runat="server" Text='<%# Eval("DestinatariosComentario") %>' />
                                 </td>
                              </tr>
                           </thead>
                           <tbody>
                              <tr>
                                 <td>
                                    <dxcp:ASPxLabel EncodeHtml="false" ID="DetalheComentarioLabel" runat="server" Text='<%# Eval("Comentario") %>'></dxcp:ASPxLabel>
                                 </td>
                              </tr>

                               
                           </tbody>
                        </table>
                               <div style="display:flex;flex-direction:row-reverse">
                                       <%# constroiLixeira(Eval("CodigoComentario").ToString(), Eval("CodigoUsuario").ToString())  %>
                                       <%# constroiIconeEdicao(Eval("CodigoComentario").ToString(), Eval("CodigoUsuario").ToString())  %>
                               </div>
                     </ItemTemplate>
                  </asp:Repeater>
               </dxcp:PanelContent>
            </PanelCollection>
         </dxcp:ASPxCallbackPanel>
         <asp:SqlDataSource CancelSelectOnNullParameter="False" ID="sdsComentario" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="
            SET @l_CodigoTipoAssociacaoObjeto			= [dbo].[f_GetCodigoTipoAssociacao](@IniciaisTipoObjeto);
            SET @l_CodigoTipoAssociacaoComentario		= [dbo].[f_GetCodigoTipoAssociacao]('CN')
            SET @l_CodigoTipoLink						= [dbo].[f_GetCodigoTipoLinkObjeto]('AS');
        
            SELECT co.DataInclusao   AS DataInclusao,  
                   co.DetalheComentario  AS Comentario,  
                   u.NomeUsuario   AS NomeUsuarioComentario,  
                   u.IniciaisNomeUsuario AS IniciaisNomeUsuarioComentario,  
                   u.NomeCompletoUsuario AS NomeCompletoUsuarioComentario,  
                   co.DataUltimaAtualizacao AS DataUltimaAtualizacao,
                   CASE WHEN co.DataUltimaAtualizacao IS NOT NULL THEN 'Sim' ELSE 'Não' END AS IndicaComentarioEditado,
                   co.CodigoComentario,
                   u.CodigoUsuario,
                   co.[DestinatariosComentario]
             FROM ComentarioObjeto AS co 
             INNER JOIN LinkObjeto AS lo ON (co.CodigoComentario = lo.CodigoObjetoLink  
                                             AND lo.CodigoTipoObjetoLink = @l_CodigoTipoAssociacaoComentario  
                                             AND lo.CodigoTipoObjeto = @l_CodigoTipoAssociacaoObjeto  
                                             AND lo.CodigoObjeto = @CodigoObjeto) 
             INNER JOIN Usuario AS u ON (u.CodigoUsuario = co.CodigoUsuario)
             ORDER BY 1 DESC"
            
             DeleteCommand="EXECUTE [dbo].[p_brk_ExcluiComentarioObjeto] 
            @CodigoEntidadeContexto
            ,@CodigoUsuarioSistema
            ,@CodigoComentario"
            InsertCommand="EXECUTE[dbo].[p_brk_IncluiComentarioObjeto] 
            @CodigoEntidadeContexto
            ,@CodigoUsuarioSistema
            ,@DetalheComentario
            ,@DestinatariosComentario
            ,@CodigoObjeto
            ,@IniciaisTipoObjeto
            ,null
            ,'S'
             
             IF @BloqueioItem is not null
             begin
             
             declare @codigoitemSuperior as int
             select @codigoitemSuperior = CodigoItemSuperior from Agil_ItemBacklog where codigoitem =   @CodigoObjeto
             
             declare @RC as int 
             UPDATE Agil_ItemBacklog
             SET IndicaBloqueioItem = @BloqueioItem
             WHERE CodigoItem = @CodigoObjeto
             
             UPDATE Agil_ItemBacklog
             SET IndicaBloqueioItem = @BloqueioItem
             WHERE CodigoItemSuperior = @codigoitemSuperior and CodigoItemEspelho = @CodigoObjeto
             
             EXECUTE @RC = [dbo].[p_Agil_SincronizaItensClonados] @CodigoObjeto
             
             end">
            <DeleteParameters>
               <asp:SessionParameter Name="CodigoEntidadeContexto" SessionField="ce" />
               <asp:SessionParameter Name="CodigoUsuarioSistema" SessionField="cu" />
               <asp:SessionParameter Name="CodigoComentario" SessionField="co_del" />
            </DeleteParameters>
            <InsertParameters>
               <asp:SessionParameter Name="CodigoEntidadeContexto" SessionField="ce" />
               <asp:SessionParameter Name="CodigoUsuarioSistema" SessionField="cu" />
               <asp:Parameter Name="DetalheComentario" />
               <asp:Parameter Name="DestinatariosComentario" />
               <asp:QueryStringParameter Name="CodigoObjeto" QueryStringField="co" />
               <asp:QueryStringParameter Name="IniciaisTipoObjeto" QueryStringField="ini" />
               <asp:QueryStringParameter Name="BloqueioItem" QueryStringField="bloqueio" />
            </InsertParameters>
            <SelectParameters>
               <asp:QueryStringParameter Name="CodigoObjeto" QueryStringField="co" DbType="Int64" />
               <asp:QueryStringParameter Name="IniciaisTipoObjeto" QueryStringField="ini" DbType="AnsiStringFixedLength" Size="2" />
               <asp:Parameter Name="l_CodigoTipoAssociacaoObjeto" DbType="Int16" />
               <asp:Parameter Name="l_CodigoTipoAssociacaoComentario" DbType="Int16" />
               <asp:Parameter Name="l_CodigoTipoLink" DbType="Int16" />
            </SelectParameters>
         </asp:SqlDataSource>
         </div>
         <script type="text/javascript">
             var sWidth = Math.max(0, document.documentElement.clientWidth) - 20;
             htmlComentario.SetWidth(sWidth);


             function funcaoAdicionarComentario() {
                 if (htmlComentario.GetIsValid())
                     callbackPanel.PerformCallback('ins');
             }


         </script>        
      </form>
   </body>
</html>