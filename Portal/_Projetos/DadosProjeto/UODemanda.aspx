<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UODemanda.aspx.cs" Inherits="_Projetos_DadosProjeto_UODemanda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divGrid" style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
            <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUO" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomCallback="gvDados_CustomCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                <ClientSideEvents FocusedRowChanged="function(s, e) {
    // OnGridFocusedRowChanged(s, true);
}"
                    CustomButtonClick="function(s, e) 
{      
           //debugger   
            btnSalvar.SetVisible(true); 
           // s.SetFocusedRowIndex(e.visibleIndex);
    	e.processOnServer = false;
	if (e.buttonID == &quot;btnEditar&quot;)
              {
	          TipoOperacao = &quot;Editar&quot;;
	           hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
	           //OnGridFocusedRowChanged(gvDados);
	           onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
	}
                else if(e.buttonID == &quot;btnExcluir&quot;)
               {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
               }
                else if(e.buttonID == 'btnDetalhesCustom')
               {	
		btnSalvar.SetVisible(false);
                              TipoOperacao = 'Consultar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
                                OnGridFocusedRowChanged(gvDados,true);
                                pcDados.Show();
               }
   }"
                    BeginCallback="function(s, e) {
	command = e.command;
}"
                    EndCallback="function(s, e) {
	if(command == &quot;CUSTOMCALLBACK&quot;)
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
                        else
                        {
                                     if(s.cp_erro == &quot;&quot;)
                                     {
                                                  window.top.mostraMensagem(erro , 'erro', true, false, null);
                                      }
                        }
               }
}"></ClientSideEvents>
                <SettingsPager PageSize="100">
                </SettingsPager>
                <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                <SettingsCommandButton>
                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                </SettingsCommandButton>

                <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                <Columns>
                    <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="False" Width="160px" VisibleIndex="0">
                        <CustomButtons>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                </Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                </Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                        </CustomButtons>
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
                    <dxcp:GridViewDataTextColumn FieldName="CodigoUO" Width="250px" Caption="Código U.O." VisibleIndex="1">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="NumeroProtocolo" Caption="Processo Vinculado" VisibleIndex="2">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="DataInclusao" VisibleIndex="3" Visible="False" Caption="DataInclusao">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Caption="CodigoUsuarioInclusao" Visible="False" VisibleIndex="4">
                    </dxcp:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="DataUltimaAlteracao" FieldName="DataUltimaAlteracao" Visible="False" VisibleIndex="5">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoUsuarioUltimaAlteracao" FieldName="CodigoUsuarioUltimaAlteracao" Visible="False" VisibleIndex="6">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="DataExclusao" FieldName="DataExclusao" Visible="False" VisibleIndex="7">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoUsuarioExclusao" FieldName="CodigoUsuarioExclusao" Visible="False" VisibleIndex="8">
                    </dxtv:GridViewDataTextColumn>
                </Columns>
            </dxcp:ASPxGridView>
        </div>


        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="220px" ID="pcDados" AllowResize="True">

            <ContentStyle>
                <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxcp:ASPxLabel runat="server" Text="Unidades Orçamentárias:"></dxcp:ASPxLabel>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    <dxtv:ASPxTextBox ID="spnUO" runat="server" ClientInstanceName="spnUO" MaxLength="4" Width="100%">
                                                        <MaskSettings Mask="&lt;0000&gt;" />
                                                        <ValidationSettings CausesValidation="True" Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        </ValidationSettings>
                                                    </dxtv:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="">
                                <td align="right">
                                    <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr style="height: 35px">
                                                <td align="right">
                                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar" CausesValidation="False" Text="Salvar" Width="100px" ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e) 
{
         e.processOnServer = false;
         if (window.onClick_btnSalvar)
        {
                  //debugger
                  var valido = validaCamposFormulario();
                  if(valido == &quot;&quot;){
                          onClick_btnSalvar();
                 }
                 else
                {
                     window.top.mostraMensagem(valido, 'atencao', true, false, null);
                }                    
        }
}"></ClientSideEvents>

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxcp:ASPxButton>

                                                </td>
                                                <td align="right">
                                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {
     e.processOnServer = false;
     if (window.onClick_btnCancelar)
    {
               onClick_btnCancelar();
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
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50" Landscape="True" ExportEmptyDetailGrid="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" MaxColumnWidth="700" PaperKind="A4">
            <Styles>
                <Default></Default>

                <Header></Header>

                <Cell></Cell>

                <Footer></Footer>

                <GroupFooter></GroupFooter>

                <GroupRow></GroupRow>

                <Title></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>
    </form>
</body>
</html>
