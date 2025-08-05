<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configPeriodicidadeProjeto.aspx.cs" Inherits="_Projetos_DadosProjeto_configPeriodicidadeProjeto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1 {
            width: 160px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                                <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="Ano" ClientInstanceName="gvDados" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback">
<ClientSideEvents CustomButtonClick="function(s, e) 
{
  gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
" FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
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
                                     if(s.cp_erro == &quot;&quot;)
                                     {
                                                  window.top.mostraMensagem(erro , 'erro', true, false, null);
                                      }
                        }
               }

}"></ClientSideEvents>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings ShowGroupPanel="True" VerticalScrollBarMode="Auto"></Settings>

<SettingsBehavior AllowFocusedRow="True" AllowSort="False" AllowSelectSingleRowOnly="True"></SettingsBehavior>

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
<Columns>
<dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="100px" VisibleIndex="0"><CustomButtons>
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
<dxcp:GridViewDataCheckColumn FieldName="IndicaMetaEditavel" ShowInCustomizationForm="True" Name="col_IndicaMetaEditavel" Caption="Meta Edit&#225;vel?" VisibleIndex="4">
<PropertiesCheckEdit ValueType="System.String" ValueChecked="S" ValueUnchecked="N" DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o"></PropertiesCheckEdit>

<HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxcp:GridViewDataCheckColumn>
<dxcp:GridViewDataCheckColumn FieldName="IndicaResultadoEditavel" ShowInCustomizationForm="True" Name="col_IndicaResultadoEditavel" Caption="Resultado Edit&#225;vel?" VisibleIndex="5">
<PropertiesCheckEdit ValueType="System.String" ValueChecked="S" ValueUnchecked="N" DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o"></PropertiesCheckEdit>

<HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxcp:GridViewDataCheckColumn>
<dxcp:GridViewDataCheckColumn FieldName="IndicaAnoAtivo" ShowInCustomizationForm="True" Name="col_IndicaAnoAtivo" Caption="Ano Ativo?" VisibleIndex="2">
<PropertiesCheckEdit ValueType="System.String" ValueChecked="S" ValueUnchecked="N" DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o"></PropertiesCheckEdit>

<HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxcp:GridViewDataCheckColumn>
<dxcp:GridViewDataSpinEditColumn FieldName="Ano" ShowInCustomizationForm="True" Name="col_Ano" Caption="Ano" VisibleIndex="1" Width="100px">
<PropertiesSpinEdit DisplayFormatString="g" MaxValue="3000" NumberType="Integer" DisplayFormatInEditMode="True">
<SpinButtons ShowIncrementButtons="False" Enabled="False" ClientVisible="False"></SpinButtons>
</PropertiesSpinEdit>

<HeaderStyle HorizontalAlign="Center" Font-Bold="False"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxcp:GridViewDataSpinEditColumn>
<dxcp:GridViewDataTextColumn FieldName="CodigoPeriodicidadeValoresFinanceiros" ShowInCustomizationForm="True" Caption="CodigoPeriodicidadeValoresFinanceiros" Visible="False" VisibleIndex="6"></dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="DescricaoPeriodicidade_PT" ShowInCustomizationForm="True" Caption="Periodicidade" VisibleIndex="7"></dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataCheckColumn FieldName="IndicaAnoPeriodoEditavel" ShowInCustomizationForm="True" Name="col_IndicaAnoPeriodoEditavel" Caption="Ano Per&#237;odo Edit&#225;vel?" VisibleIndex="3">
<PropertiesCheckEdit ValueType="System.String" ValueChecked="S" ValueUnchecked="N" DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o"></PropertiesCheckEdit>
</dxcp:GridViewDataCheckColumn>
</Columns>
</dxcp:ASPxGridView>

                                <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="774px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados" AllowDragging="True" AllowResize="True">
<ContentStyle>
<Paddings Padding="8px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxcp:PopupControlContentControl runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left" style="width: 75px">
                                                                            <dxcp:ASPxLabel runat="server" Text="Ano:" ClientInstanceName="lblAno" Font-Names="Verdana" Font-Size="8pt" ID="lblAno"></dxcp:ASPxLabel>

                                                                        </td>
                                                                       
                                                                        <td align="left">&nbsp;</td>
                                                                        
                                                                        <td align="left">&nbsp;</td>
                                                                        <td align="left">&nbsp;</td>
                                                                        <td align="left" class="auto-style1">&nbsp;</td>
                                                                        <td align="left">
                                                                            <dxcp:ASPxLabel runat="server" Text="Periodicidade:" Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel1"></dxcp:ASPxLabel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 75px">
                                                                            <dxcp:ASPxSpinEdit runat="server" MaxValue="9999" MinValue="1" NumberType="Integer" Number="0" Width="100%" ClientInstanceName="spAno" ID="spAno" MaxLength="4">
<SpinButtons ClientVisible="False"></SpinButtons>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
</dxcp:ASPxSpinEdit>

                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <dxcp:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Ano Ativo?" ValueType="System.String" ValueChecked="S" ValueUnchecked="N" ClientInstanceName="ckbAnoAtivo" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="ckbAnoAtivo">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxcp:ASPxCheckBox>

                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <dxcp:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Ano Edit&#225;vel?" ValueType="System.String" ValueChecked="S" ValueUnchecked="N" ClientInstanceName="ckbAnoPeriodoEditavel" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="ckbAnoPeriodoEditavel">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxcp:ASPxCheckBox>

                                                                        </td>
                                                                        <td>
                                                                            <dxcp:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Meta Edit&#225;vel?" ValueType="System.String" ValueChecked="S" ValueUnchecked="N" ClientInstanceName="ckbMetaEditavel" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="ckbMetaEditavel">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxcp:ASPxCheckBox>

                                                                        </td>
                                                                        <td>
                                                                            <dxcp:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Resultado Edit&#225;vel?" ValueType="System.String" ValueChecked="S" ValueUnchecked="N" ClientInstanceName="ckbResultadoEditavel" Width="150px" Font-Names="Verdana" Font-Size="8pt" ID="ckbResultadoEditavel">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxcp:ASPxCheckBox>

                                                                        </td>
                                                                        <td>
                                                                            <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlTipoPeriodicidade" Font-Names="Verdana" Font-Size="8pt" ID="ddlTipoPeriodicidade">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxcp:ASPxComboBox>

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

    </form>
</body>
</html>
