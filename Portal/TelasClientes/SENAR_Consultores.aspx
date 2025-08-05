<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SENAR_Consultores.aspx.cs" Inherits="SENAR_Consultores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 80px;
        }

        .auto-style4 {
            height: 15px;
        }

        .auto-style7 {
            width: 120px;
        }

        .auto-style8 {
            width: 126px;
        }
    </style>

</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td></td>
                    <td style="height: 10px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                        KeyFieldName="CodigoConsultor" AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados"
                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                        <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>

                                                    <table>
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                        <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                            <Items>
                                                                                <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                    <Image IconID="save_save_16x16">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                    <Image IconID="actions_reset_16x16">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                            </Items>
                                                                            <Image Url="~/imagens/botoes/layout.png">
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
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeConsultor" Name="Nome"
                                                Caption="Nome" VisibleIndex="3">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="CPF" FieldName="CPF" ShowInCustomizationForm="True" VisibleIndex="4" Width="110px">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True" VisibleIndex="5" Width="300px">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Telefone" FieldName="Telefone" ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Função" FieldName="Funcao" ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Regional" FieldName="SiglaUF" ShowInCustomizationForm="True" VisibleIndex="1" Width="70px">
                                            </dxtv:GridViewDataTextColumn>
                                        </Columns>

                                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                                        <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>

                                        <SettingsText></SettingsText>
                                    </dxwgv:ASPxGridView>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="800px" ID="pcDados">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>

                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="1" class="auto-style1">
                                                                <tr>
                                                                    <td class="auto-style2">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Regional:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="Função:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style2" style="padding-right: 10px">
                                                                        <dxtv:ASPxComboBox ID="ddlUF" runat="server" ClientInstanceName="ddlUF" Width="100%">
                                                                            <Items>
                                                                                <dxtv:ListEditItem Text="BA" Value="BA" />
                                                                                <dxtv:ListEditItem Text="DF" Value="DF" />
                                                                                <dxtv:ListEditItem Text="GO" Value="GO" />
                                                                                <dxtv:ListEditItem Text="MA" Value="MA" />
                                                                                <dxtv:ListEditItem Text="MG" Value="MG" />
                                                                                <dxtv:ListEditItem Text="MS" Value="MS" />
                                                                                <dxtv:ListEditItem Text="PI" Value="PI" />
                                                                                <dxtv:ListEditItem Text="TO" Value="TO" />
                                                                            </Items>
                                                                            <ItemStyle Wrap="True" />
                                                                            <ListBoxStyle Wrap="True">
                                                                            </ListBoxStyle>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="txtFuncao" runat="server" ClientInstanceName="txtFuncao" MaxLength="100" Width="100%">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 15px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="1" class="auto-style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Nome Completo:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style7">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="CPF:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style8">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Telefone:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtNome" runat="server" ClientInstanceName="txtNome" MaxLength="150" Width="100%">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td class="auto-style7" style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtCPF" runat="server" ClientInstanceName="txtCPF" Width="100%">
                                                                            <MaskSettings Mask="999,999,999-99" PromptChar=" " />
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td class="auto-style8">
                                                                        <dxtv:ASPxTextBox ID="txtTelefone" runat="server" ClientInstanceName="txtTelefone" Width="100%">
                                                                            <MaskSettings Mask="(99) 99999-9999" PromptChar=" " />
                                                                            <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Email:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" MaxLength="150" Width="100%">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 15px">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                <Paddings Padding="0px" />
                                                                            </dxtv:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True" LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick" RightMargin="50">
                                        <Styles>
                                            <Default>
                                            </Default>
                                            <Header>
                                            </Header>
                                            <Cell>
                                            </Cell>
                                            <Footer>
                                            </Footer>
                                            <GroupFooter>
                                            </GroupFooter>
                                            <GroupRow>
                                            </GroupRow>
                                            <Title></Title>
                                        </Styles>
                                    </dxtv:ASPxGridViewExporter>
                                    <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                    </dxtv:ASPxHiddenField>
                                </dxp:PanelContent>
                            </PanelCollection>

                            <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Consultor incluído com sucesso!&quot;, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Consultor alterado com sucesso!&quot;, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Consultor excluído com sucesso!&quot;, 'sucesso', false, false, null);
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td></td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
