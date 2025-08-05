<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup_adm_AssociacaoDeCR.aspx.cs" Inherits="administracao_popup_adm_AssociacaoDeCR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .iniciaisMaiusculas {
            text-transform: capitalize !important
        }

        .auto-style1 {
            width: 100%;
        }
    </style>
    <script src="../scripts/adm_AssociacaoDeCR.js"></script>
    <script src="../scripts/ASPxListbox.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                <tbody>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td style="width: 20px">
                                        <dxe:ASPxLabel runat="server" Text="Ano:"
                                            ID="ASPxLabel1011">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxcp:ASPxComboBox ID="comboAno" runat="server" ClientInstanceName="comboAno">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	crDisponiveis.PerformCallback();
       crSelecionados.PerformCallback();
e.processOnServer = false;
}" />
                                        </dxcp:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 47%">
                                            <dxe:ASPxLabel runat="server" Text="CR's Disponíveis:"
                                                ID="ASPxLabel106">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 6%"></td>
                                        <td style="width: 47%">
                                            <dxe:ASPxLabel runat="server" Text="CR's Selecionados:"
                                                ID="ASPxLabel107">
                                            </dxe:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" ClientInstanceName="crDisponiveis" SelectionMode="CheckColumn"
                                                EnableClientSideAPI="True" Width="100%" Height="150px"
                                                ID="crDisponiveis" EnableCallbackMode="True" OnCallback="callbackDisponiveis_Callback" >
                                                <ItemStyle Wrap="True"></ItemStyle>
                                                <SettingsLoadingPanel Text="Aguarde..."></SettingsLoadingPanel>
                                                <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" EditorNullTextDisplayMode="Unfocused" UseCompactView="False" />
                                                <ClientSideEvents EndCallback="function(s, e) {
	                                                                            setListBoxItemsInMemory(s,'Disp_');
                                                                                
                                                                                }"
                                                    SelectedIndexChanged="function(s, e) {
	                                                                        habilitaBotoesListBoxes();
                                                                            }"
                                                    Init="function(s, e) {
                                                            var sHeight = Math.max(0, document.documentElement.clientHeight) - 110;
                                                            s.SetHeight(sHeight);
                                                            }">

                                                </ClientSideEvents>
                                                <ValidationSettings>
                                                    <ErrorImage Width="14px">
                                                    </ErrorImage>
                                                </ValidationSettings>
                                            </dxe:ASPxListBox>
                                        </td>
                                        <td align="center">
                                            <table cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-bottom: 5px;">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll"
                                                                ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="55px"
                                                                ToolTip="Selecionar todos os CRs" ID="btnAddAll">
                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(crDisponiveis, crSelecionados);
	setListBoxItemsInMemory(crDisponiveis,'Disp_');
	setListBoxItemsInMemory(crSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-bottom: 5px;">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel"
                                                                ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="55px"
                                                                ToolTip="Selecionar os CRs marcados" ID="btnAddSel">
                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(crDisponiveis, crSelecionados);
	setListBoxItemsInMemory(crDisponiveis,'Disp_');
	setListBoxItemsInMemory( crSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-bottom: 5px;">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel"
                                                                ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="55px"
                                                                ToolTip="Retirar da sele&#231;&#227;o os CRs marcados" ID="btnRemoveSel">
                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(crSelecionados, crDisponiveis);
	setListBoxItemsInMemory( crDisponiveis,'Disp_');
	setListBoxItemsInMemory( crSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 28px">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll"
                                                                ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="55px"
                                                                ToolTip="Retirar da sele&#231;&#227;o todos os CRs" ID="btnRemoveAll">
                                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(crSelecionados, crDisponiveis);
	setListBoxItemsInMemory(crDisponiveis,'Disp_');
	setListBoxItemsInMemory(crSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" ClientInstanceName="crSelecionados"
                                                EnableClientSideAPI="True" Width="100%" Height="150px"
                                                ID="crSelecionados" OnCallback="callbackSelecionados_Callback" SelectionMode="CheckColumn" SelectAllText="Selecionar Tudo">
                                                <ItemStyle Wrap="True"></ItemStyle>
                                                <SettingsLoadingPanel Text="Aguarde..."></SettingsLoadingPanel>
                                                <FilteringSettings EditorNullText="Digite o texto para filtrar" ShowSearchUI="True" />
                                                <ClientSideEvents EndCallback="function(s, e) {
setListBoxItemsInMemory(s,'Sel_');
setListBoxItemsInMemory(s,'InDB_');
habilitaBotoesListBoxes();
}"
                                                    SelectedIndexChanged="function(s, e) {
habilitaBotoesListBoxes();
}"
                                                    Init="function(s, e) {
    var sHeight = Math.max(0, document.documentElement.clientHeight) - 110;
    s.SetHeight(sHeight);

}"></ClientSideEvents>
                                                <ValidationSettings>
                                                    <ErrorImage Width="14px">
                                                    </ErrorImage>
                                                </ValidationSettings>
                                            </dxe:ASPxListBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-right: 10px; padding-top: 5px;">
                                            <dxe:ASPxButton runat="server" ClientEnabled="true" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar"
                                                CausesValidation="False" Text="Salvar" Width="90px" ID="btnSalvar" CssClass="iniciaisMaiusculas">
                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
callbackSalvar.PerformCallback();
}"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="padding-top: 5px">
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px" ID="btnFechar" CssClass="iniciaisMaiusculas">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
   window.top.fechaModal();
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
        </div>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cpErro != '')
                {
                          //function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
                         window.top.mostraMensagem(s.cpErro, 'erro', true, false, null, null);                
                }
               else
               {
                        if(s.cpSucesso != '')
                       {
                              window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, atualizaGrid, 2500); 
                              window.top.fechaModal();
                       }
               }
}" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
