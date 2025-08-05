<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tema_ToDoList.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_ObjetivoEstrategico_ToDoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title></title>
    <script type="text/javascript" language="javascript">
        function abreGanttOE(codigoOE, codigoPA) {
            if (codigoPA != "-1")
                window.top.showModal('ganttPlanoAcao.aspx?CT=' + codigoOE + '&CPA=' + codigoPA, "Gantt do Plano de Ação do Tema Selecionado", 1020, screen.height - 240, "", null);
            else
                window.top.showModal('ganttPlanoAcao.aspx?CT=' + codigoOE, "Gantt do Plano de Ação do Tema Selecionado", 1020, screen.height - 240, "", null);
        }
        
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server"  Text="Mapa Estratégico:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 209px">
                                        <asp:Label ID="lblPerspectiva" runat="server" 
                                            Text="Perspectiva:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 220px">
                                        <asp:Label ID="Label6" runat="server"  Text="Tema:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa"
                                            ReadOnly="True" Width="100%">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 220px">
                                        <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                             ReadOnly="True" Width="100%">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 220px">
                                        <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema"
                                            ReadOnly="True" Width="100%">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" >
                            <dxe:ASPxImage ID="imgGantt" runat="server" Cursor="pointer" ImageUrl="~/imagens/ganttBotao.PNG"
                                ToolTip="Gráfico Gantt">
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                OnCallback="pnCallback_Callback" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <!-- ASPxGRIDVIEW: gvDados -->
                                        <!-- PANEL CONTROL : pcDados -->
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoToDoList"
                                            AutoGenerateColumns="False" Width="100%" 
                                            ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {
    OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
	onClick_CustomButtomGvDados(s, e);
}"></ClientSideEvents>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="acao" Width="130px" Caption="A&#231;&#227;o"
                                                    VisibleIndex="0">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Visibility="Invisible"
                                                            Text="Incluir">
                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnGanttCustom" Text="Gantt">
                                                            <Image Url="~/imagens/botoes/botaoGantt.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                            <Image Url="~/imagens/botoes/pFormulario.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                    <HeaderTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; 
                                                            ">
                                                            <tr>
                                                                <td style="width: 15%">
                                                                    <%# (podeIncluir==true) ? "<img src='../../imagens/botoes/incluirReg02.png' onclick='Click_NovaAcaoSugerida();' title='Novo' alt='' />" : "<img src='../../imagens/botoes/incluirRegDes.png' alt=''  />" %>
                                                                </td>
                                                                <td style="width: 70%">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoToDoList" Name="CodigoToDoList" Caption="CodigoToDoList"
                                                    Visible="False" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="NomeToDoList" Name="NomeToDoList" Caption="Descri&#231;&#227;o"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Width="210px"
                                                    Caption="Respons&#225;vel" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavelToDoList" Name="CodigoUsuarioResponsavelToDoList"
                                                    Caption="Responsavel" Visible="False" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Porcentagem" Name="Porcentagem" Width="40px"
                                                    Caption="%" VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Status" Name="Status" Width="70px" Caption="Status"
                                                    VisibleIndex="4">
                                                    <PropertiesTextEdit DisplayFormatString="&lt;img style='border:0px' alt='' src='../../imagens/{0}.gif'/&gt;">
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings ShowHeaderFilterButton="True" ShowHeaderFilterBlankItems="False" ShowFooter="True"
                                                VerticalScrollBarMode="Visible"></Settings>
                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                            <StylesPopup>
                                                <HeaderFilter>
                                                    <Content CssClass="janelaFiltro">
                                                    </Content>
                                                </HeaderFilter>
                                            </StylesPopup>
                                            <Styles>
                                                <HeaderPanel CssClass="janelaFiltro">
                                                </HeaderPanel>
                                                <FilterCell >
                                                </FilterCell>
                                                <FilterBar >
                                                </FilterBar>
                                                <HeaderFilterItem>
                                                    <HoverStyle BackColor="Fuchsia">
                                                    </HoverStyle>
                                                </HeaderFilterItem>
                                            </Styles>
                                            <Templates>
                                                <FooterRow>
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Satisfatório" ClientInstanceName="lblDescricaoConcluido"
                                                                         ID="lblDescricaoConcluido">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/verdeMenor.gif" ID="ASPxImage2">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Atenção" ClientInstanceName="lblDescricaoPendiente"
                                                                         ID="lblDescricaoPendiente">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/amareloMenor.gif" ID="ASPxImage3">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Crítico" ClientInstanceName="lblDescricaoAtrazadas"
                                                                         ID="lblDescricaoAtrazadas">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/vermelhoMenor.gif" ID="ASPxImage4">
                                                                    </dxe:ASPxImage>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
                                        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                            CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="774px" Height="145px"
                                             ID="pcDados">
                                            <ClientSideEvents Shown="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
                                            <ContentStyle>
                                                <Paddings Padding="5px"></Paddings>
                                            </ContentStyle>
                                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                                            <ContentCollection>
                                                <dxpc:PopupControlContentControl runat="server">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Plano de A&#231;&#227;o:" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                                         ID="lblCodigoUsuarioResponsavel">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 50px" valign="middle" align="center">
                                                                                    <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus"
                                                                                        ID="lblStatus">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 10px" valign="top">
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoPlanoAcao"
                                                                                        ClientEnabled="False"  ID="txtDescricaoPlanoAcao" MaxLength="250">
                                                                                        <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                                                            <Border BorderColor="Silver"></Border>
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td valign="middle" align="center">
                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Branco.gif" ClientInstanceName="imgStatus"
                                                                                        ID="imgStatus">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ClientInstanceName="lblStatusTarefa"
                                                                                         ID="lblStatusTarefa">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <dxe:ASPxLabel runat="server" Text="% Conclu&#237;do:" ClientInstanceName="lblPorcentajeConcluido"
                                                                                         ID="lblPorcentajeConcluido">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 10px" valign="top">
                                                                                    <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                                                        TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavel"
                                                                                        ID="ddlResponsavel">
                                                                                        <Columns>
                                                                                            <dxe:ListBoxColumn FieldName="NomeUsuario" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                                            <dxe:ListBoxColumn FieldName="EMail" Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                                        </Columns>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtPorcentajeConcluido"
                                                                                        ClientEnabled="False"  ID="txtPorcentajeConcluido">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="A&#231;&#245;es:" ClientInstanceName="lblAnotacoes"
                                                                         ID="lblAnotacoes">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="Td1">
                                                                    <dxp:ASPxPanel runat="server" Width="100%" ID="pAcoes">
                                                                        <PanelCollection>
                                                                            <dxp:PanelContent runat="server">
                                                                            </dxp:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxp:ASPxPanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 10px" align="right">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                                                                        Width="100px"  ID="btnSalvar">
                                                                                        <ClientSideEvents Click="function(s, e) {
	Click_Salvar(s,e);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td style="padding-left: 10px">
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px"
                                                                                         ID="btnFechar">
                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();

	pnCallback.PerformCallback('FecharPopup'); // somente para remover Session[&quot;_CodigoToDoList_&quot;]
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
                                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                    </dxhf:ASPxHiddenField>
                                                </dxpc:PopupControlContentControl>
                                            </ContentCollection>
                                        </dxpc:ASPxPopupControl>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
   onEnd_pnCallback();
}"></ClientSideEvents>
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
