<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ListaProjetosMapa.aspx.cs" Inherits="_Estrategias_mapa_FatorChave" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <style type="text/css">
    .groupRow
    {
        white-space: normal;
    }
    </style>
    <script language="javascript" type="text/javascript">
    function grid_ContextMenu(s, e) {
        if (e.objectType == "header")
            pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
    }

    function SalvarConfiguracoesLayout() {
        callback.PerformCallback("save_layout");
    }

    function RestaurarConfiguracoesLayout() {
        var funcObj = { funcaoClickOK: function(s, e){ callback.PerformCallback("restore_layout"); } }
        window.top.mostraConfirmacao('Deseja restaurar as configurações originais do layout da consulta?', function(){funcObj['funcaoClickOK'](s, e)}, null);
                  
    }

    </script>
<table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                 Font-Overline="False" Font-Strikeout="False"
                                Text="Lista de Projetos"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    <table>
        <tr>
            <td align="right" 
                style="padding-right: 10px; padding-top: 5px; padding-bottom: 5px">
                <table>
                    <tr>
                        <td align="left" style="padding-left: 10px">
                            <span __designer:mapid="c91">
                                                                        <table cellpadding="0" 
                                cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                                                                        ImageUrl="~/imagens/botoes/btnExcel.png" onclick="ImageButton1_Click" 
                                                                                        ToolTip="Exportar para excel"  />
                                                                                </td>
                                                                                <td>
                                                                                    <span __designer:mapid="c91">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/salvar.png" 
                                                                                        ToolTip="Salvar layout da consulta" Width="14px" Height="14px" 
                                                                                        ClientInstanceName="imgSalvaConfiguracoes" Cursor="pointer" 
                                                                                        ID="imgSalvaConfiguracoes">
<ClientSideEvents Click="SalvarConfiguracoesLayout"></ClientSideEvents>
</dxe:ASPxImage>

                                                                    </span>
                                                                                </td>
                                                                                <td>
                                                                                    <span __designer:mapid="c91">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/restaurar.png" ToolTip="Restaurar layout" 
                                                                                        Width="16px" Height="16px" ClientInstanceName="imgRestaurarLayout" 
                                                                                        Cursor="pointer" ID="imgRestaurarLayout">
<ClientSideEvents Click="RestaurarConfiguracoesLayout"></ClientSideEvents>
</dxe:ASPxImage>

                                                                    </span>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxImage ID="imgIncluirProjeto" runat="server" 
                                                                                        ClientInstanceName="imgIncluirProjeto" Cursor="pointer" Height="18px" 
                                                                                        ImageUrl="~/imagens/botoes/novoReg.PNG" ToolTip="Incluir novo projeto">
                                                                                        <ClientSideEvents Click="function(s, e) {
	incluiNovoProjeto();
}" />
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                            </table>

                                                                    </span>
                        </td>
                        <td align="right">
                <table>
                    <tr>
                        <td style="padding-right: 2px">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Mapa: " 
                           >
                        </dxe:ASPxLabel>
                        </td>
                        <td style="padding-right: 10px">
                        <dxe:ASPxComboBox ID="ddlMapa" ClientInstanceName="ddlMapa" runat="server" 
                                IncrementalFilteringMode="Contains" Width="420px">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                        </dxe:ASPxComboBox>
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server" 
                                ClientInstanceName="txtPesquisa"  
                                NullText="Pesquisar por palavra chave..." Width="250px">
                                <ClientSideEvents ButtonClick="function(s, e) {
	gvDados.PerformCallback();
}" />
                                <Buttons>
                                    <dxe:EditButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Search" 
                                                HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                        </Image>
                                    </dxe:EditButton>
                                </Buttons>
                                <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                            </dxe:ASPxButtonEdit>
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-right: 10px;">
                                <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados" AutoGenerateColumns="False" 
                                     Width="100%" 
                                    oncustomcallback="gvDados_CustomCallback">
                                    <ClientSideEvents ContextMenu="grid_ContextMenu" />
                                    <Columns>                                        
<dxwgv:GridViewDataTextColumn FieldName="FatorChave" Caption="Fator Chave" VisibleIndex="1" GroupIndex="0" 
                                            SortIndex="0" SortOrder="Ascending" Width="310px" ExportWidth="350">
    <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" 
        AllowHeaderFilter="False" />
</dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tema" VisibleIndex="2" 
                                            Width="310px" FieldName="Tema" GroupIndex="1" SortIndex="1" 
                                            SortOrder="Ascending" ExportWidth="350">
                                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" 
                                                AllowHeaderFilter="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Objetivo Estratégico" VisibleIndex="3" 
                                            Width="310px" FieldName="ObjetivoEstrategico" GroupIndex="2" SortIndex="2" 
                                            SortOrder="Ascending" ExportWidth="350">
                                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" 
                                                AllowHeaderFilter="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" 
                                            VisibleIndex="0" Width="480px" ExportWidth="410">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
                                                AutoFilterCondition="Contains" />
                                            <DataItemTemplate>
                                                 <%# getDescricao() %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Ação Transformadora" 
                                            FieldName="AcaoTransformadora" GroupIndex="3" SortIndex="3" 
                                            SortOrder="Ascending" VisibleIndex="4" Width="400px" ExportWidth="450">
                                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" 
                                                AllowHeaderFilter="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Unidade de Negócio" FieldName="Unidade" 
                                            VisibleIndex="7" Width="280px" ExportWidth="300">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
                                                AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Gerente do Projeto" 
                                            FieldName="GerenteProjeto" VisibleIndex="5" 
                                            Width="220px">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
                                                AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Gerente da Unidade" ExportWidth="220" 
                                            FieldName="GerenteUnidade" ShowInCustomizationForm="True" VisibleIndex="6" 
                                            Width="220px">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
                                                AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption=" Status do Projeto" 
                                            FieldName="StatusProjeto" VisibleIndex="8" Width="180px">
                                            <Settings AllowAutoFilter="False" AllowHeaderFilter="True" 
                                                FilterMode="DisplayText" />
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior EnableCustomizationWindow="True" />
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings 
                                        VerticalScrollBarMode="Auto" ShowFooter="True" 
                                        VerticalScrollableHeight="135" HorizontalScrollBarMode="Auto" 
                                        ShowGroupPanel="True" ShowFilterRow="True" />                                   
                                    <SettingsPopup>
                                        <CustomizationWindow HorizontalAlign="WindowCenter" 
                                            VerticalAlign="WindowCenter" Width="220px" />
                                    </SettingsPopup>
                                    <Styles>
                                        <GroupRow BackColor="#EBEBEB" Wrap="True" CssClass="groupRow">
                                        </GroupRow>
                                        <Row Wrap="True">
                                        </Row>
                                        <Cell Wrap="True">
                                        </Cell>
                                        <GroupPanel Wrap="True">
                                        </GroupPanel>
                                    </Styles>
                                </dxwgv:ASPxGridView>
                                                <dxhf:ASPxHiddenField runat="server" 
                                    ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1" 
                                    OnRenderBrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:ASPxGridViewExporter>

    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
	gvDados.PerformCallback('R');
}" />
    </dxcb:ASPxCallback>
    <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
        <Items>
            <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
            </dxm:MenuItem>
        </Items>
        <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
        gvDados.ShowCustomizationWindow();
}" />
    </dxm:ASPxPopupMenu>
                            </td>
        </tr>
    </table>
</asp:Content>
