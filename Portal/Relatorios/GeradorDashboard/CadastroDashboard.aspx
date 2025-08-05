<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroDashboard.aspx.cs" Inherits="Relatorios_GeradorDashboard_CadastroDashboard" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="AreaTrabalho" runat="server">
    <div style="margin: 5px">
       <script type="text/javascript" language="javascript" src="../../scripts/CadastroDashboard.js"></script>
        <dx:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" KeyFieldName="IDDashboard" Width="100%" OnCustomCallback="gvDados_CustomCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
            <ClientSideEvents CustomButtonClick="function(s, e) 
{
          if(e.buttonID == &quot;btnEditar&quot;)
          {
                   TipoOperacao = 'Editar';  
                   s.GetRowValues(s.GetFocusedRowIndex(), 'TituloDashboard;Descricao;IniciaisControle;TipoAssociacao;', montaCamposFormulario);	
          }
          if(e.buttonID == &quot;btnAcessarDashboard&quot;)
          {
                  acessarDashboard(s.GetRowKey(s.GetFocusedRowIndex()));
          }
           if(e.buttonID == &quot;btnExcluir&quot;)
          {
                    excluirDashboard(s.GetRowKey(s.GetFocusedRowIndex()));
          }
}"
                BeginCallback="function(s, e) {
	comando = e.command;
}"
                EndCallback="function(s, e) {
        if(comando ==  &quot;CUSTOMCALLBACK&quot;)
        {
                 if(s.cpErro != '')
                 {
                           alert(s.cpErro);
                 }
                 else if(s.cpSucesso != '')
                 {
                           alert(s.cpSucesso);
                  }
                  s.Refresh();
                    if(s.cpGuidRetorno != '')
                    {
             acessarDashboard(s.cpGuidRetorno);
                    }
                  pcDados.Hide();
        }
	
}" />
            <SettingsPager PageSize="100"></SettingsPager>
            <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"></Settings>
            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowDragDrop="False" AllowSelectByRowClick="True"></SettingsBehavior>
            <Columns>
                <dx:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="180px">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="btnAcessarDashboard" Text="Acessar">
                            <Image Url="~/imagens/botoes/acessardashboard.png"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <dx:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
                                        <Paddings Padding="0px" />
                                        <Items>
                                            <dx:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                </Image>
                                            </dx:MenuItem>
                                            <dx:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                <Items>
                                                    <dx:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                        </Image>
                                                    </dx:MenuItem>
                                                    <dx:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                        </Image>
                                                    </dx:MenuItem>
                                                    <dx:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                        </Image>
                                                    </dx:MenuItem>
                                                    <dx:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                        </Image>
                                                    </dx:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                </Image>
                                            </dx:MenuItem>
                                            <dx:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                <Items>
                                                    <dx:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dx:MenuItem>
                                                    <dx:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                        <Image IconID="actions_reset_16x16">
                                                        </Image>
                                                    </dx:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/layout.png">
                                                </Image>
                                            </dx:MenuItem>
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
                                    </dx:ASPxMenu>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="Título do Painel" FieldName="TituloDashboard" Name="col_TituloDashboard" ShowInCustomizationForm="True" VisibleIndex="1">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descrição" VisibleIndex="2" FieldName="Descricao" Name="col_Descricao"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Iniciais Controle" Visible="False" VisibleIndex="3" FieldName="IniciaisControle" Name="col_IniciaisControle"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo" VisibleIndex="4" FieldName="TipoAssociacao" Name="col_TipoAssocacao"></dx:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Hash" FieldName="IDDashboard" VisibleIndex="5">
                </dxtv:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
    <dx:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" AllowResize="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <table cellpadding="0" cellspacing="0" class="dxeBinImgCPnlSys">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Título do Painel:">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="txtTituloDashboard" runat="server" ClientInstanceName="txtTituloDashboard" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Descrição:">
                            </dx:ASPxLabel>
                            <dx:ASPxMemo ID="memoDescricao" runat="server" ClientInstanceName="memoDescricao" Height="170px" Width="439px">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Iniciais Controle:">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="txtIniciaisControle" runat="server" ClientInstanceName="txtIniciaisControle" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Tipo Associação:">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxComboBox ID="comboTipoAssociacao" runat="server" ClientInstanceName="comboTipoAssociacao" Width="100%">
                                <Items>
                                    <dx:ListEditItem Text="Detalhes de Projeto" Value="PR" />
                                    <dx:ListEditItem Text="Detalhes de Indicador" Value="IN" />
                                    <dx:ListEditItem Text="Menu de Relatórios Dinâmicos" Value="RD" />
                                    <dx:ListEditItem Text="Reuniões" Value="RE" />
                                    <dx:ListEditItem Text="Detalhes de Objetivo Estratégico" Value="OB" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-top: 5px">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
        if(validaCamposFormulario() == &quot;&quot;)
        {
               gvDados.PerformCallback(&quot;Salvar|&quot; + TipoOperacao);
        }
        else
        {
               alert(validaCamposFormulario());    
        }
}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dx:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
      pcDados.Hide();
}" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>

