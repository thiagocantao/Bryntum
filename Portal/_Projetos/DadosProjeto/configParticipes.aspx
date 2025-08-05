<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configParticipes.aspx.cs" Inherits="_Projetos_DadosProjeto_configParticipes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 10px"></td>
                    <td>
                        <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoPessoa" ClientInstanceName="gvDados" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                            <ClientSideEvents CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		TipoOperacao = &quot;Editar&quot;;
                                 hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
                                onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
                                onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
	               TipoOperacao = &quot;Consultar&quot;;
                                hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);	
                               OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);		
		pcDados.Show();
     }	
}
"
                                FocusedRowChanged="function(s, e) {
//OnGridFocusedRowChanged(s, true);
}"
                                BeginCallback="function(s, e) {
	comando = e.command;
}"
                                EndCallback="function(s, e) {
             
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
                        else
                        {
                                     if(s.cp_erro != &quot;&quot;)
                                     {
                                                  window.top.mostraMensagem(erro , 'erro', true, false, null);
                                      }
                        }
               }
	
}"></ClientSideEvents>

                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                            <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible"></Settings>

                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                            <SettingsCommandButton>
                                <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                            </SettingsCommandButton>
                            <Columns>
                                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="100px" VisibleIndex="0">
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
                                    </CustomButtons>
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="center">
                                                    <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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

                                    </HeaderTemplate>
                                </dxcp:GridViewCommandColumn>
                                <dxcp:GridViewDataTextColumn FieldName="CodigoPessoa" ShowInCustomizationForm="True" Name="col_CodigoPessoa" Caption="CodigoPessoa" VisibleIndex="1" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dxcp:GridViewDataTextColumn>
                                <dxcp:GridViewDataTextColumn FieldName="DataInclusao" ShowInCustomizationForm="True" Name="col_DataInclusao" Caption="DataInclusao" VisibleIndex="7" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dxcp:GridViewDataTextColumn>
                                <dxcp:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" ShowInCustomizationForm="True" Name="col_CodigoUsuarioInclusao" Caption="CodigoUsuarioInclusao" VisibleIndex="8" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dxcp:GridViewDataTextColumn>
                                <dxcp:GridViewDataTextColumn FieldName="DataUltimaAlteracao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9" Caption="DataUltimaAlteracao" Name="col_DataUltimaAlteracao"></dxcp:GridViewDataTextColumn>
                                <dxcp:GridViewDataTextColumn FieldName="CodigoUsuarioUltimaAlteracao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" Caption="CodigoUsuarioUltimaAlteracao" Name="col_CodigoUsuarioUltimaAlteracao"></dxcp:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Partícipe" FieldName="NomePessoa" Name="col_NomePessoa" VisibleIndex="2">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="CodigoPapelParticipe" FieldName="CodigoPapelParticipe" Visible="False" VisibleIndex="3">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Papel" FieldName="DescricaoPapelParticipe" VisibleIndex="4" Width="170px">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Status" FieldName="IndicaParticipeAtivo" Name="col_IndicaParticipeAtivo" VisibleIndex="6" Width="110px">
                                    <PropertiesComboBox EnableFocusedStyle="False">
                                        <Items>
                                            <dxtv:ListEditItem Text="Ativo" Value="S" />
                                            <dxtv:ListEditItem Text="Inativo" Value="N" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataTextColumn Caption="% Limite" FieldName="PercentualLimiteUsoRecurso" Name="col_PercentualLimiteUsoRecurso" ToolTip="% limite de comprometimento dos recursos usado para alerta" VisibleIndex="5" Width="80px">
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                        </dxcp:ASPxGridView>

                        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="550px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados">
                            <ContentStyle>
                                <Paddings Padding="8px"></Paddings>
                            </ContentStyle>

                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                            <ContentCollection>
                                <dxcp:PopupControlContentControl runat="server">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Partícipe:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <dxtv:ASPxCallbackPanel ID="pnDdlParticipe" runat="server" ClientInstanceName="pnDdlParticipe" Width="100%" OnCallback="pnDdlParticipe_Callback">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Ativo == 'S')
                {
                              ddlParticipe.SetEnabled(true);
                }
                else
                {
                             ddlParticipe.SetEnabled(false);
                }
}" />
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxtv:ASPxComboBox ID="ddlParticipe" runat="server" ClientInstanceName="ddlParticipe" Font-Names="Verdana" Font-Size="8pt" Width="100%" ClientEnabled="False">
                                                                <Items>
                                                                    <dxtv:ListEditItem Text="Sim" Value="S" />
                                                                    <dxtv:ListEditItem Text="Não" Value="N" />
                                                                </Items>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxComboBox>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Papel:">
                                                            </dxtv:ASPxLabel>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxComboBox ID="ddlPapel" runat="server" ClientInstanceName="ddlPapel" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Text="% Limite">
                                                </dxtv:ASPxLabel>
                                                :</td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <dxtv:ASPxSpinEdit ID="spinLimite" runat="server" ClientInstanceName="spinLimite" MaxValue="100" NumberType="Integer" ToolTip="&quot;% limite de comprometimento dos recursos usado para alerta" Width="100%">
                                                                </dxtv:ASPxSpinEdit>
                                                            </td>
                                                            <td style="width: 10px">&nbsp;</td>
                                                            <td>
                                                                <dxtv:ASPxCheckBox ID="ckbAtivo" runat="server" Checked="True" CheckState="Checked" ClientInstanceName="ckbAtivo" Font-Names="Verdana" Font-Size="8pt" Text="Partícipe Ativo no Projeto?" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right">
                                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                                        <Paddings Padding="0px"></Paddings>
                                                                    </dxcp:ASPxButton>

                                                                </td>
                                                                <td align="right" style="padding-left: 10px">
                                                                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnFechar">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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

                        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

                        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="false" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                            <Styles>
                                <Default Font-Names="Verdana" Font-Size="8pt"></Default>

                                <Header Font-Names="Verdana" Font-Size="9pt"></Header>

                                <Cell Font-Names="Verdana" Font-Size="8pt"></Cell>

                                <GroupFooter Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></GroupFooter>

                                <Title Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></Title>
                            </Styles>
                        </dxcp:ASPxGridViewExporter>

                    </td>
                    <td style="width: 10px"></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
