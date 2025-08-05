<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="SubstituicaoRecursos.aspx.cs" Inherits="administracao_SubstituicaoRecursos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
        <tr>
            <td id="ConteudoPrincipal">
                <table cellpadding="0" cellspacing="0" class="headerGrid1" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%" align="left">
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                            Text="Substituir o Usuário:">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>

                                    <td align="left">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                            Text="Pelo Usuário:">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 50%; padding-right: 10px" align="left">
                                        <dxe:ASPxComboBox runat="server" DropDownStyle="DropDown" EnableCallbackMode="True" ValueType="System.Int32"
                                            TextFormatString="{0}" Width="100%" ClientInstanceName="ddlUsuarioOrigem"
                                            Font-Bold="False" ID="ddlUsuarioOrigem"
                                            OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                            DropDownHeight="380px">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) 
{
     if(s.GetValue() != null)
     {
         pnUsuarioDestino.PerformCallback(s.GetValue());	
          gvDados.PerformCallback();
          verificaSeHabilitaBotaoSubstituir();
     }
}" />
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="NomeUsuario" Width="250px" Caption="Nome"></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="EMail" Width="350px" Caption="Email"></dxe:ListBoxColumn>
                                            </Columns>
                                        </dxe:ASPxComboBox>

                                    </td>
                                    <td align="left">
                                        <dxcp:ASPxCallbackPanel ID="pnUsuarioDestino" runat="server" ClientInstanceName="pnUsuarioDestino" OnCallback="pnUsuarioDestino_Callback" Width="100%">
                                            <ClientSideEvents EndCallback="function(s, e) {
	ddlUsuarioDestino.SetEnabled(true);
}" />
                                            <PanelCollection>
                                                <dxcp:PanelContent runat="server">
                                                    <dxtv:ASPxComboBox ID="ddlUsuarioDestino" runat="server" ClientInstanceName="ddlUsuarioDestino" DropDownHeight="380px" DropDownStyle="DropDown" Font-Bold="False" OnItemRequestedByValue="ddlUsuarioDestino_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlUsuarioDestino_ItemsRequestedByFilterCondition" TextFormatString="{0}" ValueType="System.Int32" Width="100%" ClientEnabled="False">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaSeHabilitaBotaoSubstituir();
}"
                                                            EndCallback="function(s, e) {
	ddlUsuarioDestino.SetEnabled(true);
}" />
                                                        <Columns>
                                                            <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="250px">
                                                            </dxtv:ListBoxColumn>
                                                            <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Width="350px">
                                                            </dxtv:ListBoxColumn>
                                                        </Columns>
                                                    </dxtv:ASPxComboBox>
                                                </dxcp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>





                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px; padding-bottom: 10px">
                             <div id="divGrid" style="visibility:hidden;">
                            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="gvDados"
                                Width="100%" OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                                KeyFieldName="CodigoUnico" OnCustomCallback="gvDados_CustomCallback"
                                OnCommandButtonInitialize="gvDados_CommandButtonInitialize"
                                OnCustomErrorText="gvDados_CustomErrorText">
                                <ClientSideEvents EndCallback="function(s, e) {
                if(s.cp_Sucesso != null &amp;&amp; s.cp_Sucesso != '')
	{
		window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
	}
                else if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	{
		window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
	}
               else 
	{
                              if(s.cp_Erro != null &amp;&amp; s.cp_Erro != '')
                             {		
                                         window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                             }
	}
}"
                                    SelectionChanged="function(s, e) 
{
	verificaSeHabilitaBotaoSubstituir();
}" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"/>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True"
                                        VisibleIndex="0" Width="55px">
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
                                    <dxwgv:GridViewDataTextColumn Caption="Tipo de Objeto" FieldName="NomeAgrupador"
                                        GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="1"
                                        Width="250px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="DescricaoObjeto"
                                        VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoObjetoAux"
                                        VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Tabela" Visible="False"
                                        VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Chave1" Visible="False"
                                        VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Chave2" Visible="False"
                                        VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Chave3" Visible="False"
                                        VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Chave4" Visible="False"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <GroupRowContent>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxCheckBox ID="checkBox" runat="server" />
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="CaptionText" runat="server"
                                                        Text='<%# GetCaptionText(Container) %>' EncodeHtml="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </GroupRowContent>
                                    <TitlePanel>
                                        <dxtv:ASPxLabel ID="lblAjuda" runat="server" ClientInstanceName="lblAjuda" Text="<%$ Resources:traducao, SubstituicaoRecursos___ap_s_escolher_os_usu_rios_de_origem_e_destino__escolher_na_lista_abaixo_as_responsabilidades_do_usu_rio_de_origem_que_ser_o_repassadas_ao_usu_rio_de__destino___em_seguida_acione_o_bot_o_substituir_ %>" Font-Size="9pt ">
                                        </dxtv:ASPxLabel>
                                    </TitlePanel>
                                </Templates>
                                <SettingsBehavior AllowDragDrop="False" />
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" ShowTitlePanel="True" VerticalScrollableHeight="150" />
                            </dxwgv:ASPxGridView>
                                 </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">

                            <dxtv:ASPxButton ID="btSubstituir" runat="server" AutoPostBack="False" ClientEnabled="False" ClientInstanceName="btSubstituir" Height="16px" Style="margin-bottom: 0px" Text="Substituir" Width="90px">
                                <ClientSideEvents Click="function(s, e) {
window.top.mostraMensagem('Confirma a substituição do usuário &quot;' + ddlUsuarioOrigem.GetText() + '&quot; pelo usuário &quot;' + ddlUsuarioDestino.GetText() + '&quot; nos itens selecionados?', 'confirmacao', true, true, substituir);	
}" />
                                <Paddings Padding="0px" />
                            </dxtv:ASPxButton>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="dsResponsavel" runat="server"
        ConnectionString=""
        SelectCommand=""></asp:SqlDataSource>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
        ID="ASPxGridViewExporter1"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default></Default>

            <Header></Header>

            <Cell></Cell>

            <GroupFooter Font-Bold="True"></GroupFooter>

            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido"
        HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False"
        Width="270px" ID="pcUsuarioIncluido">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center"></td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>


























                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>


























                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>

</asp:Content>

