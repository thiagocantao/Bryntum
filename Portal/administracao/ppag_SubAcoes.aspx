<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ppag_SubAcoes.aspx.cs" Inherits="administracao_ppag_SubAcoes" %>

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
                                            Text="Sub Ações">
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
                                    KeyFieldName="CodigoSubAcao" AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback">
                                    <ClientSideEvents
                                        CustomButtonClick="function(s, e) 
{
      btnSalvar1.SetVisible(true);
     if(e.buttonID == 'btnEditar')
     {
		onClickBarraNavegacao('Editar', s, pcDados);
		hfGeral.Set('TipoOperacao', 'Editar');
		TipoOperacao = 'Editar';
     }
     else if(e.buttonID == 'btnExcluir')
     {
		onClickBarraNavegacao('Excluir', s, pcDados);
     }
     else if(e.buttonID == 'btnDetalhesCustom')
     {		
                                OnGridFocusedRowChanged(s, true);		
		hfGeral.Set('TipoOperacao', 'Consultar');
		TipoOperacao = 'Consultar';
		pcDados.Show();
                               btnSalvar1.SetVisible(false);
     }	
}
"
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
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoSubAcao" Caption="CodigoSubAcao" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Caption="CodigoEntidade" VisibleIndex="4" Visible="False"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoAcao" Caption="CodigoAcao" VisibleIndex="2" Width="120px" Visible="False"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroSubAcao" Caption="Número" VisibleIndex="6" Width="110px"></dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="SubAção" FieldName="NomeSubAcao" VisibleIndex="7">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="DataExclusao" FieldName="DataExclusao" Visible="False" VisibleIndex="8">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Ação" FieldName="NomeAcao" GroupIndex="1" SortIndex="1" SortOrder="Ascending" VisibleIndex="5">
                                            <Settings AllowAutoFilter="True" />
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="CodigoPrograma" Visible="False" VisibleIndex="9">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Programa" FieldName="NomePrograma" GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                                            <Settings AllowAutoFilter="True" />
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="590px" Height="98px" ID="pcDados">
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
                                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Programa:">
                                            </dxtv:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxCallbackPanel ID="pncb_programa" runat="server" ClientInstanceName="pncb_programa" Width="100%">
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxComboBox ID="ddlPrograma" runat="server" ClientInstanceName="ddlPrograma" TextField="NomeAcao" ValueField="CodigoAcao" ValueType="System.Int32" Width="100%">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	var codigo = s.GetValue();
                pncb_acao.PerformCallback(codigo);
}" />
                                                            <Paddings Padding="0px" />
                                                        </dxtv:ASPxComboBox>
                                                    </dxtv:PanelContent>
                                                </PanelCollection>
                                            </dxtv:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Ação:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxCallbackPanel ID="pncb_acao" runat="server" ClientInstanceName="pncb_acao" OnCallback="pncb_acao_Callback" Width="100%">
                                                <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxComboBox ID="ddlAcao" runat="server" ClientInstanceName="ddlAcao" TextField="NomeAcao" ValueField="CodigoAcao" ValueType="System.Int32" Width="100%">
                                                            <Paddings Padding="0px" />
                                                        </dxtv:ASPxComboBox>
                                                    </dxtv:PanelContent>
                                                </PanelCollection>
                                            </dxtv:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Sub-Ação:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxTextBox ID="txtSubAcao" runat="server" ClientInstanceName="txtSubAcao" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px" align="right">
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px" ID="btnSalvar">
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

