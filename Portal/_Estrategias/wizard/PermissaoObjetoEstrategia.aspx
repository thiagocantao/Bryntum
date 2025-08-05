<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissaoObjetoEstrategia.aspx.cs"
    Inherits="_Estrategias_wizard_PermissaoObjetoEstrategia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body class="body">
    <form id="form1" runat="server">
    <!-- Table Principal -->
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr height="26px">
            <td valign="middle" style="height: 26px; padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text="Permissão de Objetos"></asp:Label>
            </td>
        </tr>
    </table>
    <!-- Table Conteudo -->
    <table>
        <tr>
            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Listar:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxComboBox ID="ddlListarPor" runat="server" ClientInstanceName="ddlListarPor"
                                 ValueType="System.String" Width="450px">
                                <Items>
                                    <dxe:ListEditItem Text="Mapas" Value="MAP"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Perspectivas" Value="PSP"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Temas" Value="TEM"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Objetivos Estrat&#233;gicos" Value="OBJ"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Indicadores" Value="IND"></dxe:ListEditItem>
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	onSelected_IndexDllListarPor(s, e);
}"></ClientSideEvents>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                OnCallback="pnCallback_Callback" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Filtrar por:" 
                                                            ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="TituloMapaEstrategico"
                                                            ValueField="CodigoMapaEstrategico" Width="450px" ClientInstanceName="ddlFiltarPor"
                                                             ID="ddlFiltarPor" OnCallback="ddlFiltarPor_Callback">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	//s.SetValue(null);
	//gvDados.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	onSelected_dllFiltarPor(s, e);
}"></ClientSideEvents>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top: 10px; height: 249px">
                                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoObjeto"
                                                            AutoGenerateColumns="False" Width="100%" 
                                                            ID="gvDados" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
	onClick_CustomButtomGvDado(s, e);
}"></ClientSideEvents>
                                                            <TotalSummary>
                                                                <dxwgv:ASPxSummaryItem SummaryType="Count" FieldName="Objeto" DisplayFormat="Total de Objetos: {0}"
                                                                    ShowInColumn="Objeto"></dxwgv:ASPxSummaryItem>
                                                            </TotalSummary>
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="35px" VisibleIndex="0">
                                                                    <CustomButtons>
                                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomCompartilhar" Text="Compartilhar">
                                                                            <Image Url="~/imagens/compartilhar.PNG">
                                                                            </Image>
                                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjeto" Name="CodigoObjeto" Caption="CodigoObjeto"
                                                                    Visible="False" VisibleIndex="1">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="NomeObjeto" Name="NomeObjeto" Caption="Objeto"
                                                                    VisibleIndex="1">
                                                                    <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" AutoFilterCondition="Contains">
                                                                    </Settings>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoPai" Name="CodigoObjetoPai"
                                                                    Caption="CodigoObjetoPai" Visible="False" VisibleIndex="3">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="NomeObjetoPai" Name="NomeObjetoPai" Width="300px"
                                                                    Caption="Objeto Pai" VisibleIndex="2">
                                                                    <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn FieldName="DoMapaEstrategico" Name="DoMapaEstrategico"
                                                                    Caption="DoMapaEstrategico" Visible="False" VisibleIndex="3">
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3">
                                                            </SettingsEditing>
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                    AllowResize="True" Width="400px" />
                                                            </SettingsPopup>
                                                            <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible">
                                                            </Settings>
                                                            <SettingsLoadingPanel Text=""></SettingsLoadingPanel>
                                                            <Styles>
                                                                <Footer Font-Bold="False" Font-Size="9px">
                                                                </Footer>
                                                            </Styles>
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
