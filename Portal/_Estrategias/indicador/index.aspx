<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="index.aspx.cs"
    Inherits="_Estrategias_indicador_index" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var telaInicialOE = "";
        function defineTI() {
            //document.getElementById('indicador_desktop').src = telaInicialOE; 
        }

        // ]]>
        function atualizaMenu() {
            document.getElementById('indicador_menu').src = document.getElementById('indicador_menu').src;
        }
    </script>
    <dx:ASPxSplitter ID="sp_Tela" runat="server" ClientInstanceName="sp_Tela"
        FullscreenMode="True" Height="100%" Width="100%" SeparatorSize="10px">
        <Panes>
            <dx:SplitterPane MaxSize="320px" MinSize="100px" ScrollBars="Auto"
                Size="265px" ShowCollapseBackwardButton="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxcp:ASPxCallbackPanel ID="callbackMenu" runat="server"
                            ClientInstanceName="callbackMenu">
                            <SettingsLoadingPanel ShowImage="False"></SettingsLoadingPanel>

                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <iframe id="indicador_menu" frameborder="0" height="<%=alturaTabela %>" name="indicador_menu" scrolling="no"
                                        src="opcoes.aspx?COIN=&amp;UNM=&amp;CM=" width="100%"></iframe>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ContentUrlIFrameName="indicador_desktop"
                ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxp:ASPxPanel ID="ASPxPanel1" runat="server">
                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                </dxp:PanelContent>
                            </PanelCollection>
                            <BorderLeft BorderStyle="None" />
                            <BorderTop BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </dxp:ASPxPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <ClientSideEvents 
            PaneContentUrlLoaded="function(s, e) {
	pnCarregarClick.Hide();
}" Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 90;
                s.SetHeight(sHeight);
}" />
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
            </Pane>
        </Styles>
        <Images>
            <HorizontalSeparatorButton Height="8px" Width="8px">
            </HorizontalSeparatorButton>
            <VerticalCollapseBackwardButton Url="~/imagens/colapse.png"
                UrlHottracked="~/imagens/colapse.png">
            </VerticalCollapseBackwardButton>
            <VerticalCollapseForwardButton Url="~/imagens/expand.png"
                UrlHottracked="~/imagens/expand.png">
            </VerticalCollapseForwardButton>
            <HorizontalCollapseBackwardButton Height="8px" Width="8px">
            </HorizontalCollapseBackwardButton>
            <HorizontalCollapseForwardButton Height="8px" Width="8px">
            </HorizontalCollapseForwardButton>
        </Images>
    </dx:ASPxSplitter>
    <dxe:ASPxLabel ID="lblEntidadeDiferente" runat="server" ClientInstanceName="lblEntidadeDiferente"
        Font-Bold="True" Font-Italic="True" ForeColor="Red">
    </dxe:ASPxLabel>
    <dxpc:ASPxPopupControl ID="pcAlterarIndicador" runat="server" ClientInstanceName="pcAlterarIndicador"
        HeaderText="Alterar indicador a ser analisado"
        Height="100px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        Width="720px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table style="width: 100%" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Objetivo Estrat&#233;gico:" ID="ASPxLabel1"></dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox runat="server" ValueType="System.String"
                                    Width="100%" ClientInstanceName="ddlObjetivo" ID="ddlObjetivo">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlIndicador.PerformCallback();
}"></ClientSideEvents>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel2"></dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox runat="server" ValueType="System.String"
                                    Width="100%" ClientInstanceName="ddlIndicador" ID="ddlIndicador" OnCallback="ddlIndicador_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.GetValue() == null || s.GetValue() == &quot;-1&quot;)
		btnSelecionar.SetEnabled(false);
	else
	{
		btnSelecionar.SetEnabled(true);
		s.SetValue(s.cp_codigoIndicador);
	}
}"></ClientSideEvents>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 10px">
                                            <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnSelecionar"
                                                Text="Selecionar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	var variavel = ddlObjetivo.GetValue() + &quot;;&quot; + ddlIndicador.GetValue();
	callback.PerformCallback(variavel);
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	pcAlterarIndicador.Hide();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>

        <HeaderStyle Font-Bold="True"></HeaderStyle>
    </dxpc:ASPxPopupControl>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('indicador_menu').src = document.getElementById('indicador_menu').src;
	sp_Tela.GetPane(1).SetContentUrl(sp_Tela.GetPane(1).GetContentUrl(), false);
	pcAlterarIndicador.Hide();
}" />
    </dxcb:ASPxCallback>
    <dxlp:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server"
        ClientInstanceName="pnCarregarClick" HorizontalAlign="Center"
        HorizontalOffset="40" Text="" VerticalAlign="Middle">
        <Image Url="~/imagens/carregando.gif">
        </Image>
    </dxlp:ASPxLoadingPanel>
    <dxe:ASPxLabel runat="server" Text="Indicador"
        ID="lblTituloTela" ClientVisible="False">
    </dxe:ASPxLabel>

</asp:Content>
