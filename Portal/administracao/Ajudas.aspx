<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="Ajudas.aspx.cs" Inherits="administracao_Ajudas" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Tópicos de Ajuda" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal">
        <div id="divgvDados">
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoGlossarioAjuda"
                AutoGenerateColumns="False" Width="100%"
                ID="gvDados" DataSourceID="sdsGlossarioAjuda" OnCustomCallback="gvDados_CustomCallback">
                <ClientSideEvents FocusedRowChanged="function(s, e) {
	
//OnGridFocusedRowChanged(s, true);
}"
                    CustomButtonClick="function(s, e) 
{
     
     gvDados.SetFocusedRowIndex(e.visibleIndex);
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
"
                    EndCallback="function(s, e) {
  if(s.cp_Erro == &quot;OK&quot;)
        {
             if(comando == &quot;ADDNEWROW&quot;)
             {
                  mostraDivSalvoPublicado(traducao.Ajudas_dados_inclu_dos_com_sucesso);
             }
             else if(comando == &quot;UPDATEEDIT&quot;)
             {
                  mostraDivSalvoPublicado(traducao.Ajudas_dados_atualizados_com_sucesso);
             }
            else if(comando == &quot;DELETEROW&quot;)
            {
                  mostraDivSalvoPublicado(traducao.Ajudas_dados_exclu_dos_com_sucesso);
            }
            else if(comando == &quot;CUSTOMCALLBACK&quot;)
            {
                   mostraDivSalvoPublicado(s.cp_MensagemSucesso);      
            }
        }
        else if(s.cp_Erro.toString().length &gt; 2)
	 {
             window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
	}
}"
                    BeginCallback="function(s, e) {
	comando = e.command;
}"></ClientSideEvents>
                <Columns>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
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
                    <dxwgv:GridViewDataTextColumn Caption="Funcionalidade" FieldName="TituloFuncionalidade"
                        ShowInCustomizationForm="True" VisibleIndex="2">
                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="CodigoFuncionalidade" FieldName="CodigoFuncionalidade" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="URL" FieldName="URL" VisibleIndex="3">
                    </dxtv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Título" FieldName="TituloGlossarioAjuda" VisibleIndex="4" Width="250px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="DetalhesGlossarioAjuda" FieldName="DetalhesGlossarioAjuda" Visible="False" VisibleIndex="5">
                    </dxtv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
                <SettingsPager Mode="ShowAllRecords" Visible="False">
                </SettingsPager>
                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True"></Settings>
                <SettingsText></SettingsText>

            </dxwgv:ASPxGridView>
        </div>
    </div>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="485px" ID="pcDados" AllowDragging="True">
        <ContentStyle>
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="True"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                            Text="Título:">
                                        </dxe:ASPxLabel>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtTituloAjuda" runat="server" ClientInstanceName="txtTituloAjuda"
                                            MaxLength="250" Width="100%">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px;">
                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Funcionalidade:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <dxtv:ASPxComboBox TextFormatString="{0}" ID="ddlFuncionalidade" runat="server" ClientInstanceName="ddlFuncionalidade" DataSourceID="sdsFuncionalidade" TextField="TituloFuncionalidade" ValueField="CodigoFuncionalidade" ValueType="System.Int32" Width="100%">
                                <Columns>
                                    <dxe:ListBoxColumn Caption="Título" FieldName="TituloFuncionalidade" Width="200px" />
                                    <dxe:ListBoxColumn Caption="URL" FieldName="URL" Width="300px" />
                                </Columns>
                            </dxtv:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-top: 5px;">
                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Detalhes:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <dxhe:ASPxHtmlEditor ID="htmlDetalhesAjuda" runat="server" ClientInstanceName="htmlDetalhesAjuda">
                                <Settings AllowPreview="False" AllowHtmlView="False" />
                            </dxhe:ASPxHtmlEditor>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table class="formulario-botoes">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
  onClick_btnSalvar();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
onClick_btnCancelar();
}" />
                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                    PaddingTop="0px" />
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
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
        ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
        LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
        RightMargin="50">
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
    </dxwgv:ASPxGridViewExporter>
    <asp:SqlDataSource ID="sdsGlossarioAjuda" runat="server" InsertCommand="INSERT INTO [GlossarioAjuda]
           ([TituloGlossarioAjuda]
           ,[DetalhesGlossarioAjuda]
           ,[CodigoFuncionalidade]
           ,[CodigoModeloFormulario]
           ,[CodigoEntidade])
     VALUES
           (@TituloGlossarioAjuda
           ,@DetalhesGlossarioAjuda
           ,@CodigoFuncionalidade
           ,@CodigoModeloFormulario
           ,@CodigoEntidade)"
        SelectCommand="SELECT ga.[CodigoGlossarioAjuda]
      ,ga.[TituloGlossarioAjuda]
      ,ga.[DetalhesGlossarioAjuda]
      ,ga.[CodigoFuncionalidade]
      ,ga.[CodigoModeloFormulario]
      ,ga.[CodigoEntidade]
      ,fp.TituloFuncionalidade
      ,fp.URL
  FROM [GlossarioAjuda] ga
  LEFT JOIN FuncionalidadePortal fp on (fp.CodigoFuncionalidade = ga.CodigoFuncionalidade)
where CodigoEntidade = @CodigoEntidade
        order by 2"
        DeleteCommand="DELETE FROM [GlossarioAjuda]
      WHERE CodigoGlossarioAjuda = @CodigoGlossarioAjuda"
        UpdateCommand="UPDATE [GlossarioAjuda]
   SET [TituloGlossarioAjuda] = @TituloGlossarioAjuda
      ,[DetalhesGlossarioAjuda] = @DetalhesGlossarioAjuda
      ,[CodigoEntidade] = @CodigoEntidade
 WHERE CodigoGlossarioAjuda = @CodigoGlossarioAjuda
       ">
        <DeleteParameters>
            <asp:Parameter Name="CodigoGlossarioAjuda" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="TituloGlossarioAjuda" />
            <asp:Parameter Name="DetalhesGlossarioAjuda" />
            <asp:Parameter Name="CodigoFuncionalidade" />
            <asp:Parameter Name="CodigoModeloFormulario" />
            <asp:Parameter Name="CodigoEntidade" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="TituloGlossarioAjuda" />
            <asp:Parameter Name="DetalhesGlossarioAjuda" />
            <asp:Parameter Name="CodigoEntidade" />
            <asp:Parameter Name="CodigoFuncionalidade" />
            <asp:Parameter Name="CodigoGlossarioAjuda" />

        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsFuncionalidade" runat="server" SelectCommand="SELECT [CodigoFuncionalidade]
      ,[URL]
      ,[TituloFuncionalidade]
  FROM [FuncionalidadePortal] 
where CodigoFuncionalidade not in (select CodigoFuncionalidade from GlossarioAjuda where CodigoEntidade = @CodigoEntidade)
order by 3">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

