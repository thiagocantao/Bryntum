<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="RelatorioSenarDocs.aspx.cs" Inherits="Relatorios_OutrosRelatorios_RelatorioSenarDocs" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .dxtlHeader_MaterialCompact {
            white-space: nowrap;
            padding: 13px 10px 11px;
            border: 1px solid #DFDFDF;
            /*border-width: 0 0 1px 0  !important;*/
            background-color: white;
            color: black;
            overflow: hidden;
            font-weight: normal;
            text-align: left;
            font: 14px 'Roboto Medium', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif;
            font-size: 1em;
        }
    </style>

    <script>
        function DefineBotaoSelecionarHabilitado() {

            btnSelecionar.SetEnabled(
                cmbProcessos.GetValue() != null &&
                deInicioPeriodo.GetValue() != null &&
                deTerminoPeriodo.GetValue() != null &&
                cmbNiveisDetalhes.GetValue() != null);
            lpLoading.Hide();
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div style="display: flex; flex-direction: column; padding: 5px;">
        <div style="display: flex; flex-wrap: wrap; padding: 0px 0px 10px 0px">
            <div style="flex-grow: 0; padding-top: 3px; padding-right: 3px">
                <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Processo:" Font-Names="Verdana" Font-Size="8pt"></dxcp:ASPxLabel>
            </div>
            <div style="flex-grow: 2">
                <dxcp:ASPxComboBox ID="cmbProcessos" runat="server" ValueType="System.Int32" ClientInstanceName="cmbProcessos" DataSourceID="sdsProcessos" Width="100%" Font-Names="Verdana" Font-Size="8pt" TextField="DescricaoTipoProcessoIntegracao" ValueField="CodigoTipoProcessoIntegracao">
                    <ClientSideEvents ValueChanged="function(s, e) {
DefineBotaoSelecionarHabilitado();
                            lpLoading.Show();
	cmbNiveisDetalhes.PerformCallback();
}"
                        Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxComboBox>
            </div>
            <div style="padding-top: 3px; padding-right: 5px; padding-left: 5px">
                <dxcp:ASPxLabel ID="ASPxLabel4" runat="server" Text="Período:" Font-Names="Verdana" Font-Size="8pt"></dxcp:ASPxLabel>
            </div>
            <div style="flex-grow: 1">
                <dxcp:ASPxDateEdit ID="deInicioPeriodo" runat="server" EditFormat="DateTime" Width="100%">
                    <ClientSideEvents Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}"
                        ValueChanged="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxDateEdit>
            </div>
            <div style="padding-top: 3px; flex-grow: 0; padding-right: 5px; padding-left: 5px">
                <dxcp:ASPxLabel ID="ASPxLabel5" runat="server" Text="a" Font-Names="Verdana" Font-Size="8pt" Width="100%"></dxcp:ASPxLabel>
            </div>
            <div style="flex-grow: 2">
                <dxcp:ASPxDateEdit ID="deTerminoPeriodo" runat="server" EditFormat="DateTime" Width="100%">
                    <ClientSideEvents Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}"
                        ValueChanged="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxDateEdit>
            </div>
            <div>
                <dxcp:ASPxCheckBox ID="cbSomenteAlteracoes" runat="server" Text="Somente alterações" CheckState="Checked" Font-Names="Verdana" Font-Size="8pt" ValueChecked="S" ValueType="System.Char" ValueUnchecked="N" TextSpacing="0px">
                    <ClientSideEvents Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}"
                        ValueChanged="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxCheckBox>
            </div>
            <div style="flex-grow: 0; padding-right: 5px">
                <dxcp:ASPxCheckBox ID="cbSomenteFalhas" runat="server" Text="Somente falhas" Font-Names="Verdana" Font-Size="8pt" ValueChecked="S" ValueGrayed="N" ValueType="System.Char" ValueUnchecked="N" CheckState="Unchecked">
                    <ClientSideEvents Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}"
                        ValueChanged="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxCheckBox>
            </div>
            <div style="padding-top: 4px; padding-right: 5px">
                <dxcp:ASPxLabel ID="ASPxLabel3" runat="server" Text="Nível de detalhes:" Font-Names="Verdana" Font-Size="8pt" Width="100%"></dxcp:ASPxLabel>
            </div>
            <div style="flex-grow: 2; padding-right: 5px">
                <dxcp:ASPxComboBox ID="cmbNiveisDetalhes" runat="server" ValueType="System.Int32" ClientInstanceName="cmbNiveisDetalhes" Width="100%" Font-Names="Verdana" Font-Size="8pt" OnCallback="cmbNiveisDetalhes_Callback" TextField="DescricaoNivelDetalhe" ValueField="NivelDetalhe">
                    <ClientSideEvents Init="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}"
                        ValueChanged="function(s, e) {
	DefineBotaoSelecionarHabilitado();
}" />
                </dxcp:ASPxComboBox>
            </div>
            <div>
                <dxcp:ASPxButton ID="btnSelecionar" ClientInstanceName="btnSelecionar" Height="36px" runat="server" AutoPostBack="False" Text="Selecionar">
                    <ClientSideEvents Click="function(s, e) {
	treeList.PerformCallback();
}" />
                </dxcp:ASPxButton>
            </div>
        </div>
        <div id="divBotoes">
            <table id="tbBotoesEdicao" runat="server" cellpadding="0" cellspacing="0">
                <tr runat="server">
                    <td runat="server" style="padding: 3px" valign="top">
                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnItemClick="menu_ItemClick" OnLoad="menu_Load">
                            <Paddings Padding="0px" />
                            <Items>
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
        </div>
        <div>
            <dxwtl:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="treeList" DataSourceID="sdsRegistrosIntegracao" Font-Names="Verdana" Font-Size="8pt" KeyFieldName="CodigoTransacao" OnCustomCallback="treeList_CustomCallback" ParentFieldName="CodigoTransacaoSuperior" OnHtmlRowPrepared="treeList_HtmlRowPrepared">
                <Columns>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Contains" Caption="Contexto" ShowInFilterControl="Default" VisibleIndex="0" FieldName="DescricaoContextoTransacao" Width="20%">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Processamento" ShowInFilterControl="Default" VisibleIndex="3" AllowAutoFilter="False" AllowHeaderFilter="True" FieldName="TextoResumoProcessamento" Width="30%">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Mensagem" ShowInFilterControl="Default" VisibleIndex="4" AllowAutoFilter="False" FieldName="MensagemFalhaProcessamento" Width="30%">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListDateTimeColumn AutoFilterCondition="Default" Caption="Início" ShowInFilterControl="Default" VisibleIndex="1" AllowAutoFilter="False" FieldName="DataInicioTransacao" Width="10%">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                    </dxwtle:TreeListDateTimeColumn>
                    <dxwtle:TreeListDateTimeColumn AllowAutoFilter="False" AutoFilterCondition="Default" Caption="Término" FieldName="DataTerminoTransacao" ShowInFilterControl="Default" VisibleIndex="2" Width="10%">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                    </dxwtle:TreeListDateTimeColumn>
                </Columns>
                <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                <SettingsResizing ColumnResizeMode="Control" />
                <SettingsPopup>
                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <ClientSideEvents Init="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight) - 175;
s.SetHeight(sHeight);
}" />
            </dxwtl:ASPxTreeList>
        </div>
    </div>
    <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
    </dxcp:ASPxLoadingPanel>
    <dxwtle:ASPxTreeListExporter ID="exporter" runat="server" TreeListID="treeList">
        <Settings AutoWidth="True" ExpandAllNodes="True" ExportAllPages="True" ShowTreeButtons="True" SplitDataCellAcrossPages="True">
            <PageSettings Landscape="True" PaperKind="A4">
                <Margins Bottom="50" Left="50" Right="50" Top="50" />
            </PageSettings>
        </Settings>
        <Styles>
            <Default>
            </Default>
        </Styles>
    </dxwtle:ASPxTreeListExporter>
    <asp:SqlDataSource ID="sdsRegistrosIntegracao" runat="server" OnSelected="sdsRegistrosIntegracao_Selected" OnSelecting="sdsRegistrosIntegracao_Selecting" ProviderName="System.Data.SqlClient" SelectCommand="p_itg_getRegistrosIntegracao" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="in_codigoEntidadeContexto" SessionField="ce" Type="Int32" />
            <asp:SessionParameter Name="in_codigoUsuarioSistema" SessionField="cu" Type="Int32" />
            <asp:ControlParameter ControlID="cmbProcessos" Name="in_codigoTipoOperacao" PropertyName="Value" Type="Int16" />
            <asp:ControlParameter ControlID="cmbNiveisDetalhes" Name="in_nivelDetalheInformacao" PropertyName="Value" Type="Int16" />
            <asp:ControlParameter ControlID="deInicioPeriodo" Name="in_dataInicioPeriodo" PropertyName="Value" Type="DateTime" />
            <asp:ControlParameter ControlID="deTerminoPeriodo" Name="in_dataTerminoPeriodo" PropertyName="Value" Type="DateTime" />
            <asp:ControlParameter ControlID="cbSomenteAlteracoes" Name="in_indicaSomenteAlteracoes" PropertyName="Value" Type="String" />
            <asp:ControlParameter ControlID="cbSomenteFalhas" Name="in_indicaSomenteFalhas" PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProcessos" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="p_itg_getProcessosIntegracao" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="in_codigoEntidadeContexto" SessionField="ce" Type="Int32" />
            <asp:SessionParameter DefaultValue="" Name="in_codigoUsuarioSistema" SessionField="cu" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsNiveis" runat="server" OnSelected="sdsNiveis_Selected" OnSelecting="sdsNiveis_Selecting" ProviderName="System.Data.SqlClient" SelectCommand="p_itg_getNivelDetalheProcesso" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="in_codigoEntidadeContexto" SessionField="ce" Type="Int32" />
            <asp:SessionParameter Name="in_codigoUsuarioSistema" SessionField="cu" Type="Int32" />
            <asp:ControlParameter ControlID="cmbProcessos" Name="in_codigoTipoProcessoIntegracao" PropertyName="Value" Type="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
