<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroMenusEstrategia.aspx.cs" Inherits="_Estrategias_CadastroMenusEstrategia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Menus de Estratégias">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal">
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
            ClientInstanceName="gvDados"
            KeyFieldName="CodigoMenu"
            OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
            <ClientSideEvents CustomButtonClick="function(s, e) 
{
     //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	                 
                                btnSalvar.SetVisible(false);
		OnGridFocusedRowChanged(gvDados, true);
		
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"
                BeginCallback="function(s, e) {
	linhaSelecionada = e.visibleIndex;
}"
                EndCallback="function(s, e) {
     var erro = Trim(s.cp_Erro);
     var sucesso = Trim(s.cp_Sucesso);
     if(erro != &quot;&quot; )
     {
          window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          s.cp_Erro = &quot;&quot;;
          s.cp_Sucesso = &quot;&quot;;
     }
     else if(sucesso != &quot;&quot;)
     {
          window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
     }
        s.cp_Erro = &quot;&quot;;
          s.cp_Sucesso = &quot;&quot;;
gvDados.SetFocusedRowIndex(e.visibleIndex);
}" />
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                    VisibleIndex="0" Width="100px">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                            <Image Url="~/imagens/botoes/pFormulario.PNG">
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
                <dxwgv:GridViewDataTextColumn Caption="CodigoMenu" FieldName="CodigoMenu" Visible="False"
                    VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Grupo"
                    FieldName="DescricaoGrupoMenu"
                    VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoItemMenu"
                    FieldName="CodigoItemMenu" Visible="False"
                    VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Opção" FieldName="NomeItemMenu" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Ordem" FieldName="SequenciaItemGrupoMenu" VisibleIndex="5" Width="120px">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="IniciaisObjeto" FieldName="IniciaisObjeto" Visible="False" VisibleIndex="6">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoEntidade" FieldName="CodigoEntidade" Visible="False" VisibleIndex="8">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataComboBoxColumn Caption="Disponível" FieldName="IndicaMenuDisponivel" VisibleIndex="7" Width="120px">
                    <PropertiesComboBox>
                        <Items>
                            <dxtv:ListEditItem Selected="True" Text="Sim" Value="S" />
                            <dxtv:ListEditItem Text="Não" Value="N" />
                            <dxtv:ListEditItem Text="Todos" Value="" />
                        </Items>
                    </PropertiesComboBox>
                </dxtv:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
            <SettingsPager Mode="ShowAllRecords" Visible="False">
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" ShowGroupPanel="True" />
        </dxwgv:ASPxGridView>
    </div>
    <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server"
        ClientInstanceName="pcUsuarioIncluido"
        HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
        Width="270px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server"
                SupportsDisabledAttribute="True">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="center"></td>
                            <td align="center" rowspan="3" style="width: 70px">
                                <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar"
                                    ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server"
                                    ClientInstanceName="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1"
        runat="server" GridViewID="gvDados"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default>
            </Default>
            <Header>
            </Header>
            <Cell>
            </Cell>
            <GroupFooter Font-Bold="True">
            </GroupFooter>
            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>

    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="698px" ID="pcDados" Height="136px">
        <ContentStyle>
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>

        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Grupo:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Ordem:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Disponível:">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo" runat="server" ClientInstanceName="txtGrupo" MaxLength="100" Width="100%">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                    <td style="width: 100px; padding-right: 5px">
                                        <dxtv:ASPxSpinEdit ID="spnOrdem" runat="server" ClientInstanceName="spnOrdem" Number="0" Width="100%" MaxLength="2" MaxValue="99" NumberType="Integer">
                                            <SpinButtons ClientVisible="False">
                                            </SpinButtons>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxSpinEdit>
                                    </td>
                                    <td style="width: 100px">
                                        <dxtv:ASPxComboBox ID="ddlIndicaDisponivel" runat="server" ClientInstanceName="ddlIndicaDisponivel" Width="100%">
                                            <Items>
                                                <dxtv:ListEditItem Text="Sim" Value="S" />
                                                <dxtv:ListEditItem Text="Não" Value="N" />
                                            </Items>
                                        </dxtv:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Título:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxTextBox ID="txtTituloMenu" runat="server" ClientInstanceName="txtTituloMenu" MaxLength="100" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px;">
                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="Opção de Menu:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxComboBox ID="ddlOpcao" runat="server" ClientInstanceName="ddlOpcao" Width="100%">
                            </dxtv:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0" class="formulario-botoes">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" ID="btnSalvar">
                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                                <Paddings Padding="0px"></Paddings>
                                            </dxcp:ASPxButton>

                                        </td>
                                        <td>
                                            <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                            </dxcp:ASPxButton>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <dxtv:ASPxHiddenField ID="hfGeral" ClientInstanceName="hfGeral" runat="server">
    </dxtv:ASPxHiddenField>
</asp:Content>




