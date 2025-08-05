<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LimitesPlanOrc.aspx.cs" Inherits="_PlanosPluri_DadosPlano_LimitesPlanOrc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script type="text/javascript">
            
        function excluir() {
            gvDados.PerformCallback('Excluir');
        }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
<div>
        <div style="padding-left: 5px; padding-right: 5px; padding-top: 5px">
            <table>
                <tr>
                    <td>
            <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"  KeyFieldName="CodigoPlano;CodigoLimite" Width="100%" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                <SettingsPopup>
                    <EditForm AllowResize="True" CloseOnEscape="False" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                </SettingsPopup>

                <Columns>
                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="100px">
                        <HeaderTemplate>
                            <table>
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
                                                <Border BorderStyle="None" />
                                            </HoverStyle>
                                            <Paddings Padding="0px" />
                                            </ItemStyle>
                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                <SelectedStyle>
                                                    <Border BorderStyle="None" />
                                                </SelectedStyle>
                                            </SubMenuItemStyle>
                                            <Border BorderStyle="None" />
                                        </dxtv:ASPxMenu>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoLimite" FieldName="CodigoLimite" Visible="False" VisibleIndex="2">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" VisibleIndex="4" Caption="Unidade">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Valor Mínimo" FieldName="ValorMinimo" VisibleIndex="6">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoPlano" FieldName="CodigoPlano" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Valor Máximo" FieldName="ValorMaximo" VisibleIndex="7">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Descrição" FieldName="NomeLimite" VisibleIndex="3">
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoUnidadeMedida" FieldName="CodigoUnidadeMedida" Visible="False" VisibleIndex="5">
                    </dxtv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True" />
                <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
if(comando == &quot;CUSTOMCALLBACK&quot;)
{
     if(s.cp_Erro != &quot;&quot;)
     {
          window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
      }
      else
      {
               window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
if (window.onClick_btnCancelar)
       	         onClick_btnCancelar();

       }
 }    
}" CustomButtonClick="function(s, e) {
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == 'btnNovo')
     {
        hfGeral.Set('NomeArquivo', '');
        onClickBarraNavegacao('Incluir', gvDados, pcDados);
        TipoOperacao = 'Incluir';
		hfGeral.Set('TipoOperacao', TipoOperacao);
     }
     if(e.buttonID == 'btnEditar')
     {
        hfGeral.Set('NomeArquivo', '');
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		TipoOperacao = 'Editar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
     }
     else if(e.buttonID == 'btnExcluir')
     {
	var textoMsg = 'Deseja realmente excluir o registro?';
window.top.mostraMensagem(textoMsg, 'confirmacao', true, true, excluir);
    }
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
        hfGeral.Set('NomeArquivo', '');
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		TipoOperacao = 'Consultar';
		hfGeral.Set('TipoOperacao', TipoOperacao);
		pcDados.Show();
     }
}" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsText ConfirmDelete="Deseja Excluir o registro?" />
            </dxtv:ASPxGridView>
                                                    </td>
                </tr>
                </table>
            <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxtv:ASPxHiddenField>
            <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" PaperKind="A4">
                <Styles>
                    <Default >
                    </Default>
                    <Header >
                    </Header>
                    <Cell >
                    </Cell>
                    <GroupFooter Font-Bold="True" >
                    </GroupFooter>
                    <Title Font-Bold="True" ></Title>
                </Styles>
            </dxtv:ASPxGridViewExporter>

 <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="620px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxcp:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td>
                <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Nome do Limite:">
                </dxtv:ASPxLabel>

            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxComboBox ID="ddlLimites" runat="server" ClientInstanceName="ddlLimites" Width="100%">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnUnidadeMedida.PerformCallback(s.GetValue() + '|' + TipoOperacao);
}" />
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxtv:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td >
                <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"  Text="Unidade do Limite:">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxCallbackPanel ID="pnUnidadeMedida" runat="server" ClientInstanceName="pnUnidadeMedida"     OnCallback="pnUnidadeMedida_Callback"     Width="100%">
                    <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                    <PanelCollection>
                        <dxtv:PanelContent runat="server">
                            <dxtv:ASPxTextBox ID="txtUnidade" runat="server" ClientEnabled="False" ClientInstanceName="txtUnidade" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </dxtv:PanelContent>
                    </PanelCollection>
                </dxtv:ASPxCallbackPanel>
            </td>
        </tr>
        <tr>
            <td >
                <table>
                    <tr>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server"  Text="Valor Mínimo">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"  Text="Valor Máximo">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxSpinEdit ID="spnValorMinimo" runat="server" ClientInstanceName="spnValorMinimo" Number="0" Width="100%" DecimalPlaces="2">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                        <td>
                            <dxtv:ASPxSpinEdit ID="spnValorMaximo" runat="server" ClientInstanceName="spnValorMaximo" Number="0" Width="100%" DecimalPlaces="2">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px;" align="right">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"  Text="Salvar" Width="90px">
                                    <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                    <paddings padding="0px" />
                                </dxtv:ASPxButton>
                            </td>
                            <td style="WIDTH: 10px"></td>
                            <td>
                                <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"  Text="Fechar" Width="90px">
                                    <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                    <paddings padding="0px" paddingbottom="0px" paddingleft="0px" paddingright="0px" paddingtop="0px" />
                                </dxtv:ASPxButton>
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

            <script type="text/javascript" language="javascript">
                gvDados.SetHeight(window.innerHeight - (0.1 * window.innerHeight));
            </script>
        </div>
    </div>
    </form>
</body>
</html>
