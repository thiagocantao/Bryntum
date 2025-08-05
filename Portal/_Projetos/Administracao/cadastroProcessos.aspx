<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroProcessos.aspx.cs"
    Inherits="_cadastroProcessos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Inclusão do Processos</title>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 10px;">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 571px; padding-top: 10px;">
                                            <dxe:ASPxLabel runat="server" Text="Processo:" ClientInstanceName="lblNomeProjeto"
                                                 ID="lblNomeProjeto">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                Text="Código:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 571px" valign="top">
                                            <dxe:ASPxTextBox runat="server" Width="99%" ClientInstanceName="txtNomeProjeto" AutoCompleteType="Disabled"
                                                 ID="txtNomeProjeto" MaxLength="255">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 185px" valign="top">
                                                        <dxe:ASPxTextBox ID="txtCodigoReservado" runat="server" ClientInstanceName="txtCodigoReservado"
                                                             Width="210px" MaxLength="20" AutoCompleteType="Disabled">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td valign="top" align="left">
                                                        <dxe:ASPxImage ID="imgAjuda" runat="server" Height="18px" ImageUrl="~/imagens/ajuda.png"
                                                            Width="18px" ClientInstanceName="imgAjuda" Cursor="pointer" ToolTip="Código utilizado para interface com outros sistemas da instituição">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Objetivos:"
                                   >
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxcp:ASPxMemo ID="txtObjetivos" runat="server" ClientInstanceName="txtObjetivos"  Height="120px" Width="780px">
                                </dxcp:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 300px" valign="top" align="left">
                                                <dxe:ASPxLabel runat="server" Text="Unidade:" ClientInstanceName="lblUnidade"
                                                    ID="lblUnidade">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="left">
                                                <dxe:ASPxLabel runat="server" Text="Responsável:" ClientInstanceName="lblGerenteProjeto"
                                                     ID="lblGerenteProjeto">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" style="width: 300px">
                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" 
                                                    Width="290px" ClientInstanceName="ddlUnidadeNegocio" 
                                                    ID="ddlUnidadeNegocio" IncrementalFilteringMode="Contains">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td valign='top' align='left'>
                                                <dxe:ASPxComboBox runat="server" 
                                                    Width="480px" ClientInstanceName="ddlGerenteProjeto" 
                                                    ID="ddlGerenteProjeto" DropDownStyle="DropDown"
                                                    EnableCallbackMode="True" OnItemRequestedByValue="ddlGerenteProjeto_ItemRequestedByValue"
                                                    OnItemsRequestedByFilterCondition="ddlGerenteProjeto_ItemsRequestedByFilterCondition" TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}" Init="function(s, e) {
	ddlGerenteProjeto.SetText(ddlGerenteProjeto.cp_ddlGerenteProjeto);
}"></ClientSideEvents>
                                                            <Columns>
<dxe:ListBoxColumn FieldName="NomeUsuario" Width="250px" Caption="Nome" Name="campo_Nome"></dxe:ListBoxColumn>
                                                                <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Name="campo_Email" Width="300px" />
                                                                <dxtv:ListBoxColumn Caption="Status" FieldName="StatusUsuario" Name="coluna_Status" Width="80px" />
</Columns>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>


                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <dxp:ASPxPanel ID="pnVersaoProject" runat="server" ClientInstanceName="pnVersaoProject"
                                    Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 355px">
                                                            <dxrp:ASPxRoundPanel ID="rdQuemAtualizaTarefas" runat="server" ClientInstanceName="rdQuemAtualizaTarefas"
                                                                 HeaderText="Atualiza&#231;&#227;o das Tarefas"
                                                                Width="340px">
                                                                <ContentPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                    PaddingTop="5px" />
                                                                <ContentPaddings Padding="0px" PaddingLeft="0px" PaddingTop="5px" PaddingRight="0px"
                                                                    PaddingBottom="0px"></ContentPaddings>
                                                                <HeaderStyle Font-Bold="False"  Height="10px">
                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                        PaddingTop="0px" />
                                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                    </Paddings>
                                                                </HeaderStyle>
                                                                <PanelCollection>
                                                                    <dxp:PanelContent runat="server">
                                                                        <dxe:ASPxRadioButtonList ID="rbQuemAtualiza" runat="server" ClientInstanceName="rbQuemAtualiza"
                                                                             Height="10px" ItemSpacing="5px" RepeatDirection="Horizontal"
                                                                            Width="100%">
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                PaddingTop="0px" />
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaQuemAtualiza(s.GetValue());
}" />
                                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                            </Paddings>
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaQuemAtualiza(s.GetValue());
}"></ClientSideEvents>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Gerente do Projeto" Value="N" />
                                                                                <dxe:ListEditItem Text="Recursos" Value="S" />
                                                                            </Items>
                                                                            <Border BorderStyle="None" />
                                                                            <Border BorderStyle="None"></Border>
                                                                            <DisabledStyle ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxRadioButtonList>
                                                                    </dxp:PanelContent>
                                                                </PanelCollection>
                                                            </dxrp:ASPxRoundPanel>
                                                        </td>
                                                        <td>
                                                            <dxrp:ASPxRoundPanel runat="server" HeaderText="Aprova&#231;&#227;o das Horas Trabalhadas"
                                                                Width="305px" ClientInstanceName="rdQuemAprovaTarefas" 
                                                                ID="rdQuemAprovaTarefas">
                                                                <ContentPaddings Padding="0px" PaddingLeft="0px" PaddingTop="5px" PaddingRight="0px"
                                                                    PaddingBottom="0px"></ContentPaddings>
                                                                <HeaderStyle Height="10px" Font-Bold="False" >
                                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                    </Paddings>
                                                                </HeaderStyle>
                                                                <PanelCollection>
                                                                    <dxp:PanelContent runat="server">
                                                                        <dxe:ASPxRadioButtonList runat="server" ItemSpacing="5px" RepeatDirection="Horizontal"
                                                                            ClientInstanceName="rbTipoAprovacao" Width="100%" Height="10px"
                                                                            ID="rbTipoAprovacao">
                                                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                            </Paddings>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Respons&#225;vel" Value="GP" />
                                                                                <dxe:ListEditItem Text="Gerente do Recurso" Value="GR" />
                                                                            </Items>
                                                                            <Border BorderStyle="None"></Border>
                                                                            <DisabledStyle ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxRadioButtonList>
                                                                    </dxp:PanelContent>
                                                                </PanelCollection>
                                                            </dxrp:ASPxRoundPanel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-top: 5px;">
                                    <tr>
                                        <td align="right" style="padding-right: 0px; width: 92px">
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="90px"  ID="btnSalvar"
                                                OnClick="btnSalvar_Click">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = validaCampos();
}"></ClientSideEvents>
                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td align="right" style="padding-right: 0px; width: 100px">
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnNovo"
                                                Text="Novo" Width="90px"  ID="btnNovo" OnClick="btnNovo_Click">
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td align="right" style="padding-right: 0px; width: 100px">
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px"  ID="btnFechar"
                                                OnClick="btnNovo_Click">
                                                <Paddings Padding="0px" />
                                                <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="padding-left: 5px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcAjuda" runat="server" AllowDragging="True" ClientInstanceName="pcAjuda"
        CloseAction="CloseButton" Font-Size="10pt" HeaderText="Ajuda"
        PopupElementID="imgAjuda" PopupHorizontalAlign="LeftSides" PopupHorizontalOffset="-275"
        PopupVerticalAlign="Below" PopupVerticalOffset="5" Width="293px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                Código utilizado para interface com outros sistemas da instituição.</dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle Font-Bold="True" />
    </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <asp:SqlDataSource runat="server" ID="dsResponsavel"></asp:SqlDataSource>
    </form>
</body>
</html>
