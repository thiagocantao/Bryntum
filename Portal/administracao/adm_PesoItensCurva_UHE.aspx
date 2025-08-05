<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_PesoItensCurva_UHE.aspx.cs" Inherits="administracao_adm_PesoItensCurva_UHE" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); height: 26px"
                valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Curva S" ClientInstanceName="lblTitulo">
                            </dxe:ASPxLabel>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px">

        <dxcp:ASPxGridView runat="server"
            ClientInstanceName="gvDados" KeyFieldName="CodigoProjeto;CodigoCategoria"
            AutoGenerateColumns="False" EnableRowsCache="False"
            Width="100%" ID="gvDados"
            EnableViewState="False" OnCustomCallback="gvDados_CustomCallback"
            OnCustomErrorText="gvDados_CustomErrorText">
            <ClientSideEvents BeginCallback="function(s, e) {
comando = e.command;
}"
                CustomButtonClick="function(s, e) {
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

}"
                EndCallback="function(s, e) {
	
if(comando == &quot;CUSTOMCALLBACK&quot;)
{
if(&quot;Incluir&quot; == s.cp_OperacaoOk)
   	{
window.top.mostraMensagem(&quot;Registro incluído com sucesso!&quot;, 'sucesso', false, false, null);
//onEnd_pnCallback();
pcDados.Hide();
gvDados.SetVisible(true);
   	}	
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
	{
		window.top.mostraMensagem(&quot;Registro alterado com sucesso!&quot;, 'sucesso', false, false, null);
}    
else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
{
window.top.mostraMensagem(&quot;Registro excluído com sucesso!&quot;, 'sucesso', false, false, null);
}
else if (s.cp_ErroSalvar != null ||s.cp_ErroSalvar != null != undefined)
{
               window.top.mostraMensagem(s.cp_ErroSalvar, 'erro', true, false, null);
}
   }    

}" />
            <Columns>
                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                    Width="130px" ToolTip="Editar os perfis do usu&#225;rio" VisibleIndex="0">
                    <CustomButtons>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnDetalhesCustom"
                            Text="Mostrar Detalhes">
                            <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
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
                </dxcp:GridViewCommandColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoProjeto"
                    ShowInCustomizationForm="True" Caption="CodigoProjeto" VisibleIndex="1"
                    Visible="False">

                    <FilterCellStyle></FilterCellStyle>
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoCategoria"
                    ShowInCustomizationForm="True" Caption="CodigoCategoria" VisibleIndex="2"
                    Visible="False">
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" ShowInCustomizationForm="True"
                    Caption="Projeto" VisibleIndex="3" SortIndex="0" SortOrder="Ascending">
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="DescricaoCategoria"
                    ShowInCustomizationForm="True" Caption="Categoria" VisibleIndex="4"
                    SortIndex="1" SortOrder="Ascending">
                </dxcp:GridViewDataTextColumn>
                <dxtv:GridViewDataSpinEditColumn FieldName="Peso" VisibleIndex="5"
                    Name="col_Peso">
                    <PropertiesSpinEdit DisplayFormatString="n4" DecimalPlaces="4"
                        NumberFormat="Custom">
                    </PropertiesSpinEdit>
                    <Settings AllowAutoFilter="False" />
                </dxtv:GridViewDataSpinEditColumn>
            </Columns>

            <SettingsBehavior AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>

            <SettingsPager PageSize="100"></SettingsPager>

            <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>

            <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>


        </dxcp:ASPxGridView>

        <dxcp:ASPxPopupControl runat="server"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados"
            HeaderText="Detalhes" ShowCloseButton="False" Width="380px"
            ID="pcDados">

            <ContentStyle>
                <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"
                                        Text="Projeto:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxComboBox ID="ddlProjeto" runat="server"
                                        ClientInstanceName="ddlProjeto"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlCategoria.PerformCallback(s.GetValue());
}" />
                                    </dxtv:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px;">
                                    <dxtv:ASPxLabel ID="ASPxLabel4" runat="server"
                                        Text="Categoria:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxComboBox ID="ddlCategoria" runat="server"
                                        ClientInstanceName="ddlCategoria"
                                        Width="100%" OnCallback="ddlCategoria_Callback">
                                    </dxtv:ASPxComboBox>

                                </td>
                            </tr>
                            <tr style="">
                                <td style="padding-top: 5px;" align="left">
                                    <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"
                                        Text="Peso:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr style="">
                                <td align="left" style="padding-bottom: 5px;">
                                    <dxtv:ASPxSpinEdit ID="spnPeso" runat="server" ClientInstanceName="spnPeso"
                                        Number="0" Width="100%"
                                        DecimalPlaces="4" DisplayFormatString="N4" MaxLength="20">
                                        <SpinButtons ClientVisible="False">
                                        </SpinButtons>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr style="">
                                <td align="right">
                                    <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False"
                                                        ClientInstanceName="btnSalvar" CausesValidation="False" Text="Salvar"
                                                        Width="100px" ID="btnSalvar">

                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;  
  var mensagemErro = validaCamposFormulario(); 
    if(mensagemErro == '')
    {
	   if (window.SalvarCamposFormulario)
    	{
		onClick_btnSalvar();
        	}    	
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }

}" />

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxcp:ASPxButton>

                                                </td>
                                                <td align="right">
                                                    <dxcp:ASPxButton runat="server" AutoPostBack="False"
                                                        ClientInstanceName="btnFechar" Text="Fechar" Width="100px"
                                                        ID="btnFechar">

                                                        <ClientSideEvents Click="function(s, e) {
	   e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();

}" />

                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxcp:ASPxButton>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>

        <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxtv:ASPxHiddenField>
        <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
            PaperKind="A4" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        </dxtv:ASPxGridViewExporter>

    </div>
</asp:Content>
