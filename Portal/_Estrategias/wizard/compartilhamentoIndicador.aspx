<%@ Page Language="C#" AutoEventWireup="true" CodeFile="compartilhamentoIndicador.aspx.cs"
    Inherits="_Estrategias_wizard_compartilhamentoIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/ASPxListbox.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body class="body">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);">
        <tr height="26px">
            <td valign="middle" style="height: 26px; padding-left: 10px;">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text=" Mapa"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 10px; height: 10px;">
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <!-- Mapas -->
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Indicadores da Unidade Atual:"
                                >
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxwgv:ASPxGridView ID="gvIndicadores" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="gvIndicadores" KeyFieldName="CodigoIndicador" Width="100%"
                                >
                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsLoadingPanel Text="Carregando&amp;hellip;"></SettingsLoadingPanel>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                                </SettingsEditing>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        AllowResize="True" Width="400px" />
                                </SettingsPopup>
                                <SettingsText PopupEditFormCaption="Indicadores" EmptyDataRow="N&#227;o h&#225; indicador criado nesta unidade.">
                                </SettingsText>
                                <ClientSideEvents CustomButtonClick="function(s, e) {
	e.processOnServer = false;
	if ( 'btnCompartilhar' == e.buttonID)
		pcDados.Show();
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="40px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilhar" Text="Compartilhar">
                                                <Image AlternateText="Compartilhar" Url="~/imagens/compartilhar.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Caption="CodigoIndicador"
                                        Visible="False" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Indicador" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings VerticalScrollBarMode="Visible"></Settings>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" HeaderText="Compartilhamento de Indicadores"
                    Height="80px" ImageFolder="~/App_Themes/Aqua/{0}/" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="721px"
                    CloseAction="None">
                    <ContentStyle VerticalAlign="Top">
                        <Paddings Padding="2px" PaddingLeft="10px" PaddingRight="10px"></Paddings>
                    </ContentStyle>
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnPopup(s,e);
}"></ClientSideEvents>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="Indicador:" 
                                                ID="ASPxLabel1011">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtNomeIndicador"
                                                 ID="txtNomeIndicador">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="center">
                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfUnidades" ID="hfUnidades">
                                                            </dxhf:ASPxHiddenField>
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 310px" align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Entidades Dispon&#237;veis:"
                                                                                ID="ASPxLabel106">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 5px">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="width: 5px">
                                                                        </td>
                                                                        <td style="width: 310px" align="left">
                                                                            <dxe:ASPxLabel runat="server" Text="Entidades Selecionadas:"
                                                                                ID="ASPxLabel107">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" SelectionMode="Multiple"
                                                                                ClientInstanceName="lbItensDisponiveis" EnableClientSideAPI="True" Width="100%"
                                                                                Height="110px"  ID="lbItensDisponiveis" OnCallback="lbItensDisponiveis_Callback">
                                                                                <ItemStyle BackColor="White">
                                                                                    <SelectedStyle BackColor="#FFE4AC">
                                                                                    </SelectedStyle>
                                                                                </ItemStyle>
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" Init="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                                <DisabledStyle ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxListBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll"
                                                                                                ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                Font-Bold="True"  ToolTip="Selecionar todas as unidades"
                                                                                                ID="btnAddAll">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel"
                                                                                                ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                Font-Bold="True"  ToolTip="Selecionar as unidades marcadas"
                                                                                                ID="btnAddSel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel"
                                                                                                ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                Font-Bold="True"  ToolTip="Retirar da sele&#231;&#227;o as unidades marcadas"
                                                                                                ID="btnRemoveSel">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 28px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll"
                                                                                                ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="40px" Height="25px"
                                                                                                Font-Bold="True"  ToolTip="Retirar da sele&#231;&#227;o todas as unidades"
                                                                                                ID="btnRemoveAll">
                                                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" ImageFolder="~/App_Themes/Aqua/{0}/"
                                                                                SelectionMode="Multiple" ClientInstanceName="lbItensSelecionados" EnableClientSideAPI="True"
                                                                                Width="100%" Height="110px"  ID="lbItensSelecionados"
                                                                                OnCallback="lbItensSelecionados_Callback">
                                                                                <ItemStyle BackColor="White">
                                                                                    <SelectedStyle BackColor="#FFE4AC">
                                                                                    </SelectedStyle>
                                                                                </ItemStyle>
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                                <DisabledStyle ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxListBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr style="height: 35px">
                                                        <td align="right">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar"
                                                                CausesValidation="False" Text="Salvar" Width="100px" 
                                                                ID="btnSalvar">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}"></ClientSideEvents>
                                                                <Paddings Padding="0px"></Paddings>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                        <td align="right">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                Text="Fechar" Width="100px"  ID="btnFechar">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                <Paddings Padding="0px"></Paddings>
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
                    <CloseButtonImage Height="16px" Width="17px">
                    </CloseButtonImage>
                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                </dxpc:ASPxPopupControl>
            </td>
            <td style="width: 20px">
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td>
            </td>
            <td style="width: 20px">
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px;">
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="200px" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" CloseAction="None"
                                EnableClientSideAPI="True" HeaderText="Mensagem" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowShadow="False"
                                Width="721px"  ID="pcMensagemGravacao">
                                <HeaderImage Url="~/imagens/alertAmarelho.png">
                                </HeaderImage>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Aten&#231;&#227;o:" ClientInstanceName="lblAtencao"
                                                            Font-Bold="True"  ID="lblAtencao">
                                                        </dxe:ASPxLabel>
                                                        &nbsp;<dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblMensagemError"
                                                             ID="lblMensagemError">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxp:ASPxPanel runat="server" EnableClientSideAPI="True" ClientInstanceName="pnlImpedimentos"
                                                            Width="100%" ID="pnlImpedimentos">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvImpedimentos" AutoGenerateColumns="False"
                                                                                        Width="100%"  ID="gvImpedimentos">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUnidade" Width="40%" Caption="Unidade"
                                                                                                VisibleIndex="0">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="Impedimento" Caption="Impedimento" VisibleIndex="1">
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                        </SettingsPager>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                                                                        Text="Fechar" Width="100px"  ID="ASPxButton1">
                                                                                        <ClientSideEvents Click="function(s, e) {
	pcMensagemGravacao.Hide();
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxp:ASPxPanel runat="server" EnableClientSideAPI="True" ClientInstanceName="pnlMensagemGravacao"
                                            Width="100px" ID="pnlMensagemGravacao">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcOperMsg" HeaderText="Incluir a Entidad Atual"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" ShowHeader="False" Width="270px"
                                ID="pcOperMsg">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="" align="center">
                                                    </td>
                                                    <td style="width: 70px" align="center" rowspan="3">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                            ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);

//	e.processOnServer = false;
//	if (window.onEnd_pnCallback)
//	    onEnd_pnCallback();
	
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td style="width: 20px">
            </td>
        </tr>
    </table>
    <!-- Indicadores -->
    &nbsp;
    </form>
</body>
</html>
