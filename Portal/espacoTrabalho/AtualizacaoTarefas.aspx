<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="AtualizacaoTarefas.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_TimeSheet" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Atualização de Tarefas">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="gvDados" EnableRowsCache="False"
                    KeyFieldName="CodigoTarefa" Width="100%"
                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                    OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                    OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	gvDados.SetFocusedRowIndex(e.visibleIndex);
	if(e.buttonID == &quot;btnDetalhes&quot;)
	    {
			abreDados('D', e.visibleIndex);	
     	}
	else if(e.buttonID == &quot;btnDelegar&quot;)
	{
		ddlRecurso.PerformCallback();
	}
     
}"
                        EndCallback="function(s, e) {
	pcDelegar.Hide();
	if(s.cp_Msg != '')
                {
		window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
                }
                else if(s.cp_Erro != '')
                   {
                               window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                   }

}" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " FixedStyle="Left" VisibleIndex="0"
                            Width="100px">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhes" Text="Editar Detalhes da Atribui&#231;&#227;o">
                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnStatus">
                                    <Image Url="~/imagens/vazioPequeno.GIF">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxtv:GridViewCommandColumnCustomButton ID="btnDelegar" Text="Delegar">
                                    <Image AlternateText="Delegar" ToolTip="Delegar"
                                        Url="~/imagens/botoes/btnDelegar.png">
                                    </Image>
                                </dxtv:GridViewCommandColumnCustomButton>
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
                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Obter Tarefas Atribuídas às Minhas Equipes">
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
                            <FooterTemplate>
                                <strong>Total</strong>
                            </FooterTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Vinculada a" FieldName="NomeProjeto"
                            GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="1"
                            Width="350px">
                            <Settings AllowGroup="True" AllowHeaderFilter="True"
                                AutoFilterCondition="Contains" />
                            <GroupRowTemplate>
                                <%#getNomeGrupo() %>
                            </GroupRowTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa"
                            VisibleIndex="3" Width="370px">
                            <Settings AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="Inicio"
                            VisibleIndex="4" Width="110px">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="Termino"
                            VisibleIndex="5" Width="110px">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Trab. Restante(h)" VisibleIndex="9"
                            Width="130px" FieldName="TrabalhoRestante">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% Concluído" FieldName="PercConcluido"
                            VisibleIndex="10" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:n0}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Atrasada" FieldName="IndicaAtrasada"
                            VisibleIndex="11" Width="90px">
                            <Settings AutoFilterCondition="Contains" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Concluída" FieldName="IndicaConcluida"
                            VisibleIndex="12" Width="90px">
                            <Settings AutoFilterCondition="Contains" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IndicaCritica"
                            VisibleIndex="13" Caption="Crítica" Width="90px">
                            <Settings AutoFilterCondition="Contains" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IndicaAnexo" Visible="False"
                            VisibleIndex="16" ShowInCustomizationForm="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StatusAprovacao" Visible="False"
                            VisibleIndex="17" ShowInCustomizationForm="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TipoTarefa" Visible="False"
                            VisibleIndex="15" ShowInCustomizationForm="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoAtribuicao" Visible="False"
                            VisibleIndex="14" ShowInCustomizationForm="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Tarefa Superior"
                            FieldName="TarefaSuperior" GroupIndex="1" SortIndex="1" SortOrder="Ascending"
                            VisibleIndex="2" Width="350px">
                        </dxtv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Trabalho(h)" VisibleIndex="7"
                            Width="130px" FieldName="Trabalho">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Trabalho Real(h)" VisibleIndex="8"
                            Width="130px" FieldName="TrabalhoRealTotal">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AutoExpandAllGroups="True" EnableCustomizationWindow="True" AllowFocusedRow="True" />
                    <SettingsResizing  ColumnResizeMode="Control"/>
                    <Settings ShowFooter="True" VerticalScrollBarMode="Auto"
                        ShowGroupPanel="True" HorizontalScrollBarMode="Visible"
                        ShowHeaderFilterBlankItems="False" />
                    <SettingsPopup>
                        <CustomizationWindow Height="300px" HorizontalAlign="Center"
                            VerticalAlign="WindowCenter" Width="300px" />
                    </SettingsPopup>
                    <SettingsContextMenu Enabled="True" EnableRowMenu="False">
                        <ColumnMenuItemVisibility ShowSearchPanel="False" SortAscending="False"
                            SortDescending="False" />
                    </SettingsContextMenu>
                    <Styles>
                        <GroupRow Font-Bold="True">
                        </GroupRow>
                    </Styles>
                    <Templates>
                        <FooterRow>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server"
                                            ImageUrl="~/imagens/botoes/tarefasPPLenda.png">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="padding-right: 5px; padding-left: 3px">
                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="False"
                                            Text="Envio Pendente">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                            ImageUrl="~/imagens/botoes/tarefasPALenda.png">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="padding-right: 5px; padding-left: 3px">
                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="False"
                                            Text="Pendente Aprovação">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="ASPxImage3" runat="server"
                                            ImageUrl="~/imagens/botoes/salvarLenda.png">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="padding-right: 5px; padding-left: 3px">
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="False"
                                            Text="Aprovado">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="ASPxImage4" runat="server"
                                            ImageUrl="~/imagens/botoes/tarefaRecusadaLenda.png">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="padding-left: 3px; padding-right: 5px;">
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="False"
                                            Text="Reprovado">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="padding-left: 8px">
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="False"
                                            Font-Underline="True"
                                            Text="Sublinhado: trabalho real maior que trabalho previsto">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </FooterRow>
                    </Templates>
                    <Border BorderStyle="Solid" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px"
                align="right">
                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                    Text="Publicar" Width="100px">
                    <Paddings Padding="0px"></Paddings>

                    <ClientSideEvents Click="function(s, e) {
window.top.mostraMensagem(&quot;Deseja Enviar as Tarefas para Aprovação?&quot;, 'confirmacao', true, true,enviaTarefasParaAprovacao);
}"></ClientSideEvents>
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
           if(s.cp_OK != '')
           {
                    window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                    gvDados.PerformCallback();
           }
           else if (s.cp_Erro != '')
          {
                  window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          }
}" />
    </dxcb:ASPxCallback>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
        ID="ASPxGridViewExporter1" OnRenderBrick="gvExporter_RenderBrick">
        <Styles>
            <Default></Default>

            <Header></Header>

            <Cell></Cell>

            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>

    <dxcp:ASPxPopupControl ID="pcDelegar" runat="server"
        ClientInstanceName="pcDelegar" CloseAction="None"
        HeaderText="Delegar Tarefa" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="450px">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table>
                    <tr>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"
                                Text="Recurso:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxComboBox ID="ddlRecurso" runat="server"
                                ClientInstanceName="ddlRecurso"
                                TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario"
                                Width="100%">
                                <ClientSideEvents EndCallback="function(s, e) {
	pcDelegar.Show();
}"
                                    SelectedIndexChanged="function(s, e) {
}" />
                                <ItemStyle />
                            </dxtv:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxButton ID="btnDelegarTarefa" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnDelegarTarefa"
                                                Text="Salvar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
   	 if (ddlRecurso.GetValue() != null)
	    gvDados.PerformCallback('DLG');
	else
	    window.top.mostraMensagem('O responsável deve ser informado!', 'Atencao', true, false, null);
}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
   	pcDelegar.Hide();
}" />
                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                    PaddingRight="0px" PaddingTop="0px" />
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

</asp:Content>

