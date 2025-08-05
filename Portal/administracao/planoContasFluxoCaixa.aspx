<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="planoContasFluxoCaixa.aspx.cs" Inherits="planoContasFluxoCaixa" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
            <script type="text/javascript" language="javascript"" src="../scripts/planoContasFluxoCaixa.js"></script>
        <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>
    
    
    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <table cellpadding="0" cellspacing="0"
                    style="width: 100%; padding-left: 5px; padding-right: 15px;" border="0">
                    <tr>
                        <td style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px">
                            <table style="width:100%">
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtFiltro" runat="server" ClientInstanceName="txtFiltro" NullText="Filtro pela descrição da conta" MaxLength="30" Width="100%">
                                            <ClientSideEvents KeyDown="function(s, e) {
	novaDescricao();
}" />
                                            <NullTextStyle Font-Italic="True" ForeColor="Silver">
                                            </NullTextStyle>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                    <td align="right" style="width: 170px; padding-left: 5px;padding-right:20px">
                                        <dxtv:ASPxRadioButtonList ID="rbDespesaReceita" runat="server" ClientInstanceName="rbDespesaReceita" ItemSpacing="25px" RepeatDirection="Horizontal" SelectedIndex="1" Width="100%">
                                            <Paddings Padding="0px" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	tlCentroCustos.PerformCallback(s.GetValue());
                pnTipoConta.PerformCallback(s.GetValue());
}" />
                                            <Items>
                                                <dxtv:ListEditItem Text="Receita" Value="E" />
                                                <dxtv:ListEditItem Selected="True" Text="Despesa" Value="S" />
                                            </Items>
                                        </dxtv:ASPxRadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px; padding-left: 10px">
                            <dxwtle:ASPxTreeList ID="tlCentroCustos" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlCentroCustos" KeyFieldName="CodigoConta" OnCustomCallback="tlCentroCustos_CustomCallback" OnHtmlRowPrepared="tlCentroCustos_HtmlRowPrepared" OnProcessDragNode="tlCentroCustos_ProcessDragNode" ParentFieldName="CodigoContaSuperior" Width="100%" OnHtmlDataCellPrepared="tlCentroCustos_HtmlDataCellPrepared">
                                <Columns>
                                    <dxwtle:TreeListTextColumn Caption="CodigoConta" FieldName="CodigoConta" Name="CodigoConta" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="Descrição" FieldName="DescricaoConta" Name="DescricaoConta" ShowInCustomizationForm="True" VisibleIndex="2" AllowSort="True">
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="Departamento" FieldName="Departamento" Name="Departamento" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="Diretoria" FieldName="Diretoria" Name="Diretoria" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="Tipo da Conta" FieldName="TipoConta" Name="TipoConta" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="Código Reservado" FieldName="CodigoReservadoGrupoConta" Name="CodigoReservadoGrupoConta" ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListTextColumn Caption="CodigoContaSuperior" FieldName="CodigoContaSuperior" Name="CodigoContaSuperior" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8" Width="70px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                    <dxwtle:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="70px">
                                        <CustomButtons>
                                            <dxwtle:TreeListCommandColumnCustomButton ID="btnEditar"  Image-AlternateText="Editar" Image-ToolTip="Editar">
                                                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwtle:TreeListCommandColumnCustomButton>
                                            <dxwtle:TreeListCommandColumnCustomButton ID="btnExcluir" Image-AlternateText="Excluir" Image-ToolTip="Excluir">
                                                <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwtle:TreeListCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderCaptionTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
                                                            <Paddings Padding="0px" />
                                                            <Items>
                                                                <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxtv:MenuItem>
                                                                <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                    <Items>
                                                                        <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxtv:MenuItem>
                                                                <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                        <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image IconID="actions_reset_16x16">
                                                                            </Image>
                                                                        </dxtv:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                    </Image>
                                                                </dxtv:MenuItem>
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
                                                        </dxtv:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderCaptionTemplate>
                                    </dxwtle:TreeListCommandColumn>
                                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Conta Analítica" FieldName="IndicaContaAnalitica" Name="IndicaContaAnalitica" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="7" Width="100px">
                                        <PropertiesTextEdit EnableFocusedStyle="False">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwtle:TreeListTextColumn>
                                </Columns>
                                <Settings VerticalScrollBarMode="Visible" />
                                <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                <SettingsResizing ColumnResizeMode="Control" />
                                <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>

                                <SettingsEditing AllowNodeDragDrop="True" />

                                <SettingsPopupEditForm VerticalOffset="-1"></SettingsPopupEditForm>

                                <SettingsPopup>
                                    <EditForm VerticalOffset="-1"></EditForm>
                                </SettingsPopup>

                                <Styles>
                                    <Cell Wrap="True">
                                    </Cell>
                                </Styles>
                                <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnEditar&quot;)
               {
                             abrePopUp('Conta','Editar');
               }
              if(e.buttonID == &quot;btnExcluir&quot;)
              {
                             abrePopUp('Conta','Excluir');             
              }
}"
                                    FocusedNodeChanged="function(s, e) {
	OnFocusedNodeChanged(s);
}"
                                    BeginCallback="function(s, e) {
	comando = e.command;
}"
                                    EndCallback="function(s, e) {
          if(comando == 'MoveNode')
          {
                   if(s.cpMensagemErro != '')
                  {
                              window.top.mostraMensagem(s.cpMensagemErro, 'erro', true, false, null);
                  }
          }
}" />
                            </dxwtle:ASPxTreeList>
                            <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Os registros marcados em fundo cinza são agrupadores."></dxcp:ASPxLabel>
                        </td>
                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>

        <ClientSideEvents EndCallback="function(s, e) 
       {
                         if(s.cp_Erro != &quot;&quot;)
                          {
                                     window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);       
                           }                                
                           else
                         {
                                            if(s.cp_Sucesso != &quot;&quot;) 
                                           {
                                                       window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                                                       if(window.onClick_btnCancelar)
                                                            onClick_btnCancelar();
                                            }                             

                        }
}"></ClientSideEvents>
    </dxcp:ASPxCallbackPanel>

    <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" Height="184px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="489px">
        <ContentStyle>
            <Paddings Padding="5px" />
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="True" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server"
                SupportsDisabledAttribute="True">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td <%--class="auto-style2"--%>>
                            <table>
                                <tr>
                                    <td style="padding: 0;">
                                        <table>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                        Text="Tipo:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxtv:ASPxCallbackPanel ID="pnTipoConta" runat="server" ClientInstanceName="pnTipoConta" OnCallback="pnTipoConta_Callback">
                                                        <PanelCollection>
                                                            <dxtv:PanelContent runat="server">
                                                                <dxtv:ASPxComboBox ID="cbTipoConta" runat="server" ClientInstanceName="cbTipoConta" Width="100%">
                                                                </dxtv:ASPxComboBox>
                                                            </dxtv:PanelContent>
                                                        </PanelCollection>
                                                    </dxtv:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                        Text="Descrição:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxe:ASPxTextBox ID="txtDescricaoConta" runat="server"
                                                        ClientInstanceName="txtDescricaoConta"
                                                        MaxLength="70" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 5px;" class="auto-style3">
                                                    <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Codigo Reservado:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxe:ASPxTextBox ID="txtCodigoReservado" runat="server"
                                                        ClientInstanceName="txtCodigoReservado"
                                                        MaxLength="30" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">
                                                    <dxtv:ASPxCheckBox ID="checkIndicaContaAnalitica" runat="server" CheckState="Unchecked" ClientInstanceName="checkIndicaContaAnalitica" Text="Indica Conta Analítica" ValueChecked="S" ValueType="System.String" ValueUnchecked="N" Width="100%">
                                                    </dxtv:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td <%--class="auto-style2"--%> align="right">
                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td class="formulario-botao">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="100px">
                                                <ClientSideEvents Click="function(s, e) {
	if(window.onClick_btnSalvar)
	{
                                                    onClick_btnSalvar();
	}
}" />
                                                <Paddings Padding="0px" />


                                            </dxe:ASPxButton>
                                        </td>
                                        <td class="formulario-botao">
                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {
	//e.processOnServer = false;
	//abrePopUp('Conta', 'Cancelar');
    if(window.onClick_btnCancelar)
     onClick_btnCancelar();

}" />
                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                    PaddingRight="0px" PaddingTop="0px" />
                                                <ClientSideEvents Click="function(s, e) {
	//e.processOnServer = false;
	//abrePopUp(&#39;Conta&#39;, &#39;Cancelar&#39;);
    if(window.onClick_btnCancelar)
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
    <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server" TreeListID="tlCentroCustos" OnRenderBrick="ASPxTreeListExporter1_RenderBrick">
        <Settings>
            <PageSettings PaperKind="A4">
            </PageSettings>
        </Settings>
        <Styles>
            <Default Wrap="True">
                <Paddings Padding="0px" />
            </Default>
            <Cell>
            </Cell>
        </Styles>
    </dxwtle:ASPxTreeListExporter>

    <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style3 {
            width: 471px;
        }
    </style>
</asp:Content>

