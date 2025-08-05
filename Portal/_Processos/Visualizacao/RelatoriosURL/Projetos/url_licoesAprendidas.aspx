<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="url_licoesAprendidas.aspx.cs" Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_licoesAprendidas"
    Title="Portal da Estratégia" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td></td>
                <td style="height: 10px">
                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                    </dxwgv:ASPxGridViewExporter>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        OnCallback="pnCallback_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="cla"
                                    AutoGenerateColumns="False" Width="100%"
                                    ID="gvDados">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
		OnGridFocusedRowChanged(s,true);
}"
                                        CustomButtonClick="function(s, e) 
{
	if(e.buttonID == &quot;btnFormulario&quot;)
	{			
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
	}
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" VisibleIndex="0"
                                            Caption=" ">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnFormulario" Text="Detalhes do relat&#243;rio">
                                                    <Image Url="~/imagens/botoes/pFormulario.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
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
                                                                            <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                                                <Image IconID="save_save_16x16">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout"
                                                                                Name="btnRestaurarLayout">
                                                                                <Image IconID="actions_reset_16x16">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                        </Items>
                                                                        <Image Url="~/imagens/botoes/layout.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnRelatorio" Text=""
                                                                        ToolTip="Visualizar relatório em PDF das lições aprendidas">
                                                                        <Image Url="~/imagens/botoes/btnPDF.png">
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
                                        <dxwgv:GridViewDataTextColumn FieldName="tipo" Width="140px" Caption="Tipo"
                                            VisibleIndex="1">
                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="assunto" Width="240px"
                                            Caption="Assunto" VisibleIndex="2">
                                            <Settings AllowAutoFilter="True" ShowFilterRowMenu="False" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="projeto" Caption="Projeto"
                                            VisibleIndex="3">
                                            <Settings AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="licao" Caption="Li&#231;&#227;o"
                                            VisibleIndex="4">
                                            <Settings AutoFilterCondition="Contains"></Settings>
                                            <DataItemTemplate>
                                                <%# (Eval("licao").ToString().Length >= 90) ? (Eval("licao").ToString().Substring(0, 90) + "...") : (Eval("licao").ToString()) %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IncluidoPor" Caption="Inclu&#237;do Por"
                                            Visible="False" VisibleIndex="4">
                                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="cla" Caption="Codigo Licao Aprendida" Visible="False"
                                            VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"></Settings>
                                </dxwgv:ASPxGridView>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" PopupVerticalOffset="40" ShowCloseButton="False"
                                    Width="690px" Height="251px" ID="pcDados">
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 189px">
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" Style="cursor: pointer;"
                                                                                ImageUrl="~/imagens/botoes/btnPDF.png" OnClick="ImageButton1_Click"
                                                                                ToolTip="Gerar Relatório em PDF" />
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 189px; padding-top: 5px;">
                                                                            <dxe:ASPxLabel runat="server" Text="Data:" ID="ASPxLabel4">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Inclu&#237;da Por:"
                                                                                ID="ASPxLabel5">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 189px">
                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                DisplayFormatString="dd/MM/yyyy" ReadOnly="True" ClientInstanceName="dteData"
                                                                                ID="dteData">
                                                                                <CalendarProperties ClearButtonText="Limpar">
                                                                                </CalendarProperties>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIncluidaPor"
                                                                                ClientEnabled="False" ID="txtIncluidaPor">
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
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 190px">
                                                                            <dxe:ASPxLabel runat="server" Text="Tipo:" ID="ASPxLabel6">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 475px">
                                                                            <dxe:ASPxLabel runat="server" Text="Assunto:"
                                                                                ID="ASPxLabel7">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 190px">
                                                                            <dxe:ASPxTextBox runat="server" Width="170px" ClientInstanceName="txtTipo" ClientEnabled="False"
                                                                                ID="txtTipo">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="width: 475px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtAssunto" ClientEnabled="False"
                                                                                ID="txtAssunto">
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
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Projeto:"
                                                                ID="ASPxLabel8">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False"
                                                                ID="txtProjeto">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Li&#231;&#227;o:"
                                                                ID="ASPxLabel9">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo runat="server" Rows="9" Width="100%" ClientInstanceName="txtLicao"
                                                                ClientEnabled="False" ID="txtLicao">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" ClientVisible="False"
                                                                                ClientEnabled="False" Text="Salvar" Width="90px"
                                                                                ID="btnSalvar">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px"
                                                                                ID="btnCancelar">
                                                                                <ClientSideEvents Click="function(s, e) 
{
		e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    ShowHeader="False" Width="270px" ID="pcMensagemGravacao">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style=""></td>
                                                        <td align="center" rowspan="3" style="width: 70px">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                ID="lblAcaoGravacao">
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
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
