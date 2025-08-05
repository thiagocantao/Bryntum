<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ppag_Programas.aspx.cs" Inherits="administracao_ppag_Programas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
                    <tr>
                        <td align="left" style="height: 26px"
                            valign="middle">
                            <table>
                                <tr>
                                    <td align="center" style="width: 1px; height: 19px">
                                        <span id="Span2" runat="server"></span>
                                    </td>
                                    <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                                        <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                            Text="Programas">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                    KeyFieldName="CodigoPrograma" AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                    <ClientSideEvents
                                        CustomButtonClick="function(s, e) 
{
                                                    
btnSalvar1.SetVisible(true);    
 if(e.buttonID == 'btnEditar')
     {
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Editar');
		TipoOperacao = 'Editar';
     }
     else if(e.buttonID == 'btnExcluir')
     {
		onClickBarraNavegacao('Excluir', gvDados, pcDados);
     }
     else if(e.buttonID == 'btnDetalhesCustom')
     {		
                                OnGridFocusedRowChanged(gvDados, true);		
		hfGeral.Set('TipoOperacao', 'Consultar');
		TipoOperacao = 'Consultar';
		pcDados.Show();
                               btnSalvar1.SetVisible(false);
     }	
}
"
                                        BeginCallback="function(s, e) {
	comando = e.command;
}"
                                        EndCallback="function(s, e) {
       var erro = s.cp_Erro;
       var sucesso = s.cp_Sucesso;
       if(s.cp_Erro != '')
       {
               window.top.mostraMensagem(erro , 'erro', true, false, null);
       }
       else if(s.cp_Sucesso != '')
       {
             	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
     
                window.top.mostraMensagem(sucesso , 'sucesso', false, false, null);
       }
       s.cp_Erro = '';
       s.cp_Sucesso = '';
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderTemplate>
                                                <table>
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
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoPrograma" Caption="CodigoPrograma" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Caption="CodigoEntidade" VisibleIndex="2" Visible="False"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroPrograma" Caption="Número" VisibleIndex="3" Width="120px"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomePrograma" Caption="Programa" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="DataExclusao" FieldName="DataExclusao" Visible="False" VisibleIndex="5">
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowSelectByRowClick="True"></SettingsBehavior>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings VerticalScrollBarMode="Visible"></Settings>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="587px" Height="98px" ID="pcDados">
                    <ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>

                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="Número:" ID="ASPxLabel1"></dxe:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxSpinEdit ID="spnNumero" runat="server" ClientInstanceName="spnNumero" Number="0" Width="100%">
                                                <SpinButtons ClientVisible="False">
                                                </SpinButtons>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxSpinEdit>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="Programa:" ID="ASPxLabel3"></dxe:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxTextBox ID="txtPrograma" runat="server" ClientInstanceName="txtPrograma" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px" align="right">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px" align="right">
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px" ID="btnSalvar" EnableClientSideAPI="True">
                                                                <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>

                                                        </td>
                                                        <td align="right">
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px" ID="btnCancelar">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>

                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                    GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                    <Styles>
                        <Default>
                        </Default>
                        <Header>
                        </Header>
                        <Cell>
                        </Cell>
                        <GroupFooter Font-Bold="True">
                        </GroupFooter>
                        <Title Font-Bold="True"></Title>
                    </Styles>
                </dxwgv:ASPxGridViewExporter>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>

