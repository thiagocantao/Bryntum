<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoMetas_01.aspx.cs" Inherits="_Portfolios_VisaoMetas_visaoMetas_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            width: 100px;
        }
        .style4
        {
            width: 10px;
            height: 10px;
        }
        .lk
        {
            font-weight: normal;
            text-decoration: underline;
            cursor: pointer;
            color: #0E008C;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" enableviewstate="false" runat="server">
    <table class="formulario" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
            </td>
            <td>
                <table class="formulario-colunas" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="formulario-linha">
                        <td class="formulario-label">
                            <dxe:ASPxLabel runat="server" Text="Programa/Projeto:" 
                                ID="ASPxLabel6" EnableViewState="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="formulario-label">
                            <dxe:ASPxLabel runat="server" Text="Tipo de Indicador:" 
                                ID="ASPxLabel8" EnableViewState="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="formulario-label">
                            <dxe:ASPxLabel runat="server" Text="Indicador:" 
                                ID="ASPxLabel7" EnableViewState="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td>
                            <dxe:ASPxComboBox ID="ddlProjeto" runat="server" ClientInstanceName="ddlProjeto"
                                 Width="100%" IncrementalFilteringMode="Contains"
                                ImageUrlField="ImagemTipoProjeto">
                            </dxe:ASPxComboBox>
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="ddlTipoIndicador" runat="server" ClientInstanceName="ddlTipoIndicador"
                                 Width="100%" IncrementalFilteringMode="Contains"
                                SelectedIndex="0">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlIndicador.PerformCallback();
}" />
                                <Items>
                                    <dxe:ListEditItem Selected="True" Text="Todos" Value="-1" />
                                    <dxe:ListEditItem Text="Desempenho" Value="'D'" />
                                    <dxe:ListEditItem Text="Operacional" Value="'O'" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="headerGrid" style="padding-right: 20px">
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlIndicador" runat="server" ClientInstanceName="ddlIndicador"
                                             Width="100%" IncrementalFilteringMode="Contains">
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td class="style3">
                                        <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                                            Text="Selecionar" Width="100px" AutoPostBack="False" EnableViewState="False"
                                            ClientInstanceName="btnSelecionar">
                                            <ClientSideEvents Click="function(s, e) {
	pnCallback.PerformCallback();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td >
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" 
                                ClientInstanceName="pnCallback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent1" runat="server">
                                        <div id="divListaMetas" runat="server">
                                            <table cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td style="width: 49%" valign="top">
                                                        <dxnb:ASPxNavBar ID="nb01" runat="server" EncodeHtml="False"
                                                            GroupSpacing="4px" EnableViewState="False" Width="98%">
                                                            <ClientSideEvents ExpandedChanged="function(s, e) {
	if(e.group.GetExpanded())
	{		
		document.getElementById('frm2_' + e.group.name).src = hfUrls.Get(&quot;url_&quot; + e.group.name); ;
	}
}" />
                                                            <Paddings PaddingLeft="0px" PaddingRight="5px" />
                                                            <GroupHeaderStyle Wrap="True">
                                                                <Paddings Padding="2px" PaddingLeft="10px" />
                                                            </GroupHeaderStyle>
                                                            <GroupContentStyle>
                                                                <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                                            </GroupContentStyle>
                                                            <ItemStyle BackColor="White" />
                                                            <GroupDataFields NameField="NomeProjeto" />
                                                            <DisabledStyle ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxnb:ASPxNavBar>
                                                    </td>
                                                    <td style="width: 49%" valign="top">
                                                        <dxnb:ASPxNavBar ID="nb02" runat="server" EncodeHtml="False"
                                                            GroupSpacing="4px" EnableViewState="False" Width="98%">
                                                            <ClientSideEvents ExpandedChanged="function(s, e) {

	if(e.group.GetExpanded())
	{		
		document.getElementById('frm2_' + e.group.name).src = hfUrls.Get(&quot;url_&quot; + e.group.name); ;
	}
}" />
                                                            <Paddings PaddingLeft="5px" PaddingRight="0px" />
                                                            <GroupHeaderStyle Wrap="True">
                                                                <Paddings Padding="2px" PaddingLeft="10px" />
                                                            </GroupHeaderStyle>
                                                            <GroupContentStyle Wrap="True">
                                                                <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                                            </GroupContentStyle>
                                                            <ItemStyle BackColor="White" />
                                                            <GroupDataFields NameField="NomeProjeto" />
                                                            <DisabledStyle ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxnb:ASPxNavBar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>
                            <dxpc:ASPxPopupControl ID="popUpStatusTela" runat="server" CloseAction="CloseButton"
                                HeaderText=" " Height="27px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowHeader="False" Width="272px">
                                <ContentStyle>
                                    <Paddings PaddingTop="20px" />
                                </ContentStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                            Text="Nenhuma informa&#231;&#227;o a ser apresentada." Font-Bold="False" Font-Italic="False">
                                        </dxe:ASPxLabel>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfUrls" ID="hfUrls">
                            </dxhf:ASPxHiddenField>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
        CloseAction="None"  HeaderText="Análises"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="900px">
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>
        <ClientSideEvents Closing="function(s, e) {
	
}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table id="pnCallback_grid_DXPEForm_DXEFT" border="0" cellpadding="0" cellspacing="0"
                    style=" width: 100%;  border-collapse: collapse">
                    <tbody>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Meta:" EnableViewState="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxTextBox ID="txtMeta" runat="server" ClientEnabled="False" ClientInstanceName="txtMeta"
                                     Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Indicador:" EnableViewState="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicador"
                                     Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="An&#225;lise:" EnableViewState="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="1">
                                <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientInstanceName="txtAnalise"
                                    Width="100%" ClientEnabled="False" Rows="7">
                                    <ClientSideEvents KeyUp="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="1" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" rowspan="1">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Recomenda&#231;&#245;es:" EnableViewState="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="1">
                                <dxe:ASPxMemo ID="txtRecomendacoes" runat="server" ClientInstanceName="txtRecomendacoes"
                                     Width="100%" ClientEnabled="False" Rows="7">
                                    <ClientSideEvents KeyUp="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" rowspan="1">
                                <dxe:ASPxLabel ID="lblUltimaAtualizacao" runat="server" ClientInstanceName="lblUltimaAtualizacaoAnalise"
                                    Font-Italic="True" >
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="1" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" rowspan="2">
                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                     Text="Fechar" Width="100px" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcDados.Hide();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle Font-Bold="True" />
    </dxpc:ASPxPopupControl>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	 txtIndicador.SetText(s.cp_Indicador);
	 txtMeta.SetText(s.cp_Meta);
	 txtRecomendacoes.SetText(s.cp_recomendacoes);
     txtAnalise.SetText(s.cp_Analise);
	 lblUltimaAtualizacaoAnalise.SetText(s.cp_DataAnalise);
	 pcDados.SetHeaderText(s.cp_titulo);
     pcDados.Show();
	
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
