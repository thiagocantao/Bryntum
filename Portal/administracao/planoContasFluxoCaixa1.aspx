<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="planoContasFluxoCaixa1.aspx.cs" Inherits="planoContasFluxoCaixa1" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="left">
                <table cellpadding="0" cellspacing="0"
                    style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif); height: 26px;"
                    border="0">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="8pt" Text="Centro de Custos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
        <PanelCollection>
            <dxp:PanelContent runat="server">

                <table cellpadding="0" cellspacing="0"
                    style="width: 100%; padding-left: 5px; padding-right: 5px;" border="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <table cellpadding="0" cellspacing="0" enableviewstate="false"
                                style="width: 100%">
                                <tr>
                                    <td style="width: 5px"></td>
                                    <td style="padding-right: 10px">
                                        <table id="tbBotoesEdicao0" runat="server" cellpadding="0" cellspacing="0"
                                            width="100%">
                                            <tr runat="server">
                                                <td runat="server" style="padding: 3px;" valign="middle" width="80px">
                                                    <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent"
                                                        ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                        OnItemClick="menu_ItemClick">
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
                                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                                        ToolTip="Exportar para HTML">
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
                                                            <dxm:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
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
                                                <td runat="server" style="padding: 3px;" valign="middle">
                                                    <dxe:ASPxTextBox ID="txtFiltro" runat="server" ClientInstanceName="txtFiltro"
                                                        Font-Names="Verdana" Font-Size="8pt" MaxLength="30" Width="350px">
                                                        <ClientSideEvents KeyDown="function(s, e) {
	novaDescricao();
}" />
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxwtl:ASPxTreeList ID="tlCentroCusto" runat="server"
                                            AutoGenerateColumns="False" ClientInstanceName="tlCentroCusto"
                                            Font-Names="Verdana" Font-Size="8pt" KeyFieldName="CodigoConta"
                                            OnCustomCallback="tlCentroCustos_CustomCallback"
                                            OnHtmlRowPrepared="tlCentroCustos_HtmlRowPrepared"
                                            ParentFieldName="CodigoContaSuperior" Width="100%">
                                            <Columns>
                                                <dxwtl:TreeListTextColumn Caption="CodigoConta" FieldName="CodigoConta"
                                                    Name="CodigoConta" ShowInCustomizationForm="True" Visible="False"
                                                    VisibleIndex="0">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="Descrição" FieldName="DescricaoConta"
                                                    Name="DescricaoConta" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <DataCellTemplate>
                                                        <%# getDescricaoObjetosLista()%>
                                                    </DataCellTemplate>

                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="Departamento" FieldName="Departamento"
                                                    Name="Departamento" ShowInCustomizationForm="True" Visible="False"
                                                    VisibleIndex="2">
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="Diretoria" FieldName="Diretoria"
                                                    Name="Diretoria" ShowInCustomizationForm="True" Visible="False"
                                                    VisibleIndex="2">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="Tipo da Conta" FieldName="TipoConta"
                                                    Name="TipoConta" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="IndicaContaAnalitica"
                                                    FieldName="IndicaContaAnalitica" Name="IndicaContaAnalitica"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="Código Reservado"
                                                    FieldName="CodigoReservadoGrupoConta" Name="CodigoReservadoGrupoConta"
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="CodigoContaSuperior"
                                                    FieldName="CodigoContaSuperior" Name="CodigoContaSuperior"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6" Width="70px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwtl:TreeListTextColumn>
                                            </Columns>
                                            <Settings SuppressOuterGridLines="True" VerticalScrollBarMode="Visible" />
                                            <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                            <SettingsPager PageSize="30">
                                            </SettingsPager>
                                            <Styles>
                                                <Cell Wrap="True">
                                                </Cell>
                                            </Styles>
                                            <ClientSideEvents FocusedNodeChanged="function(s, e) {
	OnFocusedNodeChanged(s);
}" />
                                        </dxwtl:ASPxTreeList>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td class="" style="height: 25px">
                            <table cellpadding="0" cellspacing="0"
                                style="width: 100%; height: 15px;">
                                <tbody>
                                    <tr>
                                        <td style="border-right: green 2px solid; border-top: green 2px solid; border-left: green 2px solid; width: 10px; border-bottom: green 2px solid; background-color: #ddffcc"></td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Font-Names="Verdana"
                                                Font-Size="8pt" Text="Contas Sintéticas" Font-Bold="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" Font-Names="Verdana" Font-Size="8pt" ID="pcUsuarioIncluido">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="font-size: 8pt; font-family: Verdana" align="center"></td>
                                        <td style="width: 70px" align="center" rowspan="3">
                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" Font-Names="Verdana" Font-Size="8pt" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>





                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="hfOrdena" runat="server"
                    ClientInstanceName="hfOrdena">
                </dxhf:ASPxHiddenField>
                <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server">
                </dxwtle:ASPxTreeListExporter>
                <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" Font-Names="Verdana" Font-Size="8pt" HeaderText="Detalhes" Height="184px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="489px">
                    <ClientSideEvents Shown="function(s, e) {
		ordenarItems();
}" />
                    <ContentStyle>
                        <Paddings Padding="5px" />
                        <Paddings Padding="5px"></Paddings>
                    </ContentStyle>
                    <HeaderStyle Font-Bold="True" />
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                            SupportsDisabledAttribute="True">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Names="Verdana"
                                                                    Font-Size="8pt" Text="Tipo:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxRadioButtonList ID="rblTipoConta" runat="server"
                                                                    ClientInstanceName="rblTipoConta" Font-Names="Verdana" Font-Size="8pt"
                                                                    RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                                                    <Paddings Padding="0px" />
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
   //debugger
	ddlContaSuperior.PerformCallback();
}" />
                                                                    <Paddings Padding="0px"></Paddings>

                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
   //debugger
	ddlContaSuperior.PerformCallback();
}"></ClientSideEvents>
                                                                    <Items>
                                                                        <dxe:ListEditItem Selected="True" Text="Diretoria" Value="DI" />
                                                                        <dxe:ListEditItem Text="Departamento" Value="DP" />
                                                                        <dxe:ListEditItem Text="Conta" Value="CT" />
                                                                    </Items>
                                                                </dxe:ASPxRadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Verdana"
                                                                    Font-Size="8pt" Text="Descrição:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txtDescricaoConta" runat="server"
                                                                    ClientInstanceName="txtDescricaoConta" Font-Names="Verdana" Font-Size="8pt"
                                                                    MaxLength="70" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Verdana"
                                                                    Font-Size="8pt" Text="Codigo Reservado:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txtCodigoReservado" runat="server"
                                                                    ClientInstanceName="txtCodigoReservado" Font-Names="Verdana" Font-Size="8pt"
                                                                    MaxLength="30" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td class="style14">
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Names="Verdana"
                                                        Font-Size="8pt" Text="Centro de Custos Superior:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style14">
                                                    <dxe:ASPxComboBox ID="ddlContaSuperior" runat="server"
                                                        ClientInstanceName="ddlContaSuperior" Font-Names="Verdana" Font-Size="8pt"
                                                        OnCallback="ddlContaSuperior_Callback" Width="100%">
                                                        <ClientSideEvents EndCallback="function(s, e) {
   if(ddlContaSuperior.cpIndiceSelecionado)
	{
		ddlContaSuperior.SetSelectedIndex(ddlContaSuperior.cpIndiceSelecionado);		
	}
    ordenarItems();
}" />
                                                        <Paddings Padding="0px" />
                                                        <ClientSideEvents EndCallback="function(s, e) {
	//debugger
   if(ddlContaSuperior.cpIndiceSelecionado)
	{
		ddlContaSuperior.SetSelectedIndex(ddlContaSuperior.cpIndiceSelecionado);		
	}
}"></ClientSideEvents>

                                                        <Paddings Padding="0px"></Paddings>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                            ClientInstanceName="btnSalvar" Font-Names="Verdana" Font-Size="8pt"
                                                            Text="Salvar" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	if(window.onClick_btnSalvar)
	{
		onClick_btnSalvar();
	}
}" />
                                                            <Paddings Padding="0px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if(window.onClick_btnSalvar)
	{
		onClick_btnSalvar();
	}
}"></ClientSideEvents>

                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td align="right">
                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                            ClientInstanceName="btnFechar" Font-Names="Verdana" Font-Size="8pt"
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
            </dxp:PanelContent>
        </PanelCollection>

        <ClientSideEvents EndCallback="function(s, e) 
{
	
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Centro de custos incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Centro de custos alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Centro de custos excluído com sucesso!&quot;);
}"></ClientSideEvents>
    </dxcp:ASPxCallbackPanel>
</asp:Content>
