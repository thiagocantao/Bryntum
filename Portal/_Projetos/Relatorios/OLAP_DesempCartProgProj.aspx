<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="OLAP_DesempCartProgProj.aspx.cs" Inherits="_Projetos_Relatorios_OLAP_DesempCartProgProj" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url('../../imagens/titulo/back_Titulo_Desktop.gif');">
            <tr style="height: 26px;">
                <td align="left">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="padding-left: 5px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Desempenho dos Programas"></asp:Label>
                            </td>
                            <td style="width: 55px; padding-right: 1px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                    Text="Unidade:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 300px; padding-right: 1px;" align="right">
                                <dxe:ASPxComboBox ID="ddlUnidade" runat="server"
                                    ClientInstanceName="ddlUnidade"
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                        <RequiredField ErrorText="Informe a unidade" IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="width: 35px; padding-right: 1px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                    Text="Mês:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 65px;">
                                <dxe:ASPxComboBox ID="ddlMes" runat="server" DropDownRows="12"
                                    Width="100%">
                                    <Items>
                                        <dxe:ListEditItem Text="Jan" Value="01" />
                                        <dxe:ListEditItem Text="Fev" Value="02" />
                                        <dxe:ListEditItem Text="Mar" Value="03" />
                                        <dxe:ListEditItem Text="Abr" Value="04" />
                                        <dxe:ListEditItem Text="Mai" Value="05" />
                                        <dxe:ListEditItem Text="Jun" Value="06" />
                                        <dxe:ListEditItem Text="Jul" Value="07" />
                                        <dxe:ListEditItem Text="Ago" Value="08" />
                                        <dxe:ListEditItem Text="Set" Value="09" />
                                        <dxe:ListEditItem Text="Out" Value="10" />
                                        <dxe:ListEditItem Text="Nov" Value="11" />
                                        <dxe:ListEditItem Text="Dez" Value="12" />
                                    </Items>
                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                        <RequiredField ErrorText="Informe o mês" IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="width: 35px; padding-right: 1px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 60px; padding-right: 10px;">
                                <dxe:ASPxSpinEdit ID="txtAno" runat="server" AllowMouseWheel="False"
                                    MaxLength="4" MaxValue="2199" MinValue="1900" NumberType="Integer" Width="100%">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                        <RequiredField ErrorText="Informe o ano" IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td style="width: 90px; padding-right: 10px;">
                                <dxe:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False"
                                    Text="Selecionar" ValidationGroup="MKE"
                                    Width="100%">
                                    <ClientSideEvents Click="function(s, e) {	
	if(ASPxClientEdit.ValidateGroup('MKE', true))
		tlDados.PerformCallback();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" Width="100%" ClientInstanceName="pnCallbackDados">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px; padding-top: 10px;">
                                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                            <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server"
                                                TreeListID="tlDados" OnRenderBrick="ASPxTreeListExporter1_RenderBrick">
                                                <Styles>
                                                    <Header>
                                                    </Header>
                                                    <Cell>
                                                    </Cell>
                                                </Styles>
                                            </dxwtle:ASPxTreeListExporter>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="tbBotoesEdicao" runat="server" cellpadding="0" cellspacing="0"
                                                            width="100%">
                                                            <tr runat="server">
                                                                <td runat="server" valign="middle">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
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
                                                                            <td>
                                                                                <dxe:ASPxImage ID="imgCurvaS" runat="server" ClientInstanceName="imgCurvaS"
                                                                                    Cursor="Pointer" ImageUrl="~/imagens/botoes/btnCurvaS.png"
                                                                                    ToolTip="Visualizar Curva S">
                                                                                    <ClientSideEvents Click="function(s, e) {
	OnGridFocusedRowChanged(tlDados);
}" />
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados"
                                                            KeyFieldName="EstruturaHierarquica" ParentFieldName="EstruturaHierarquicaSup"
                                                            Width="100%">
                                                            <Columns>
                                                                <dxwtl:TreeListTextColumn Caption="Descrição" FieldName="Descricao" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0">
                                                                    <CellStyle Wrap="True">
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="(%) Previsto" FieldName="PercentualPrevisto"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="90px">
                                                                    <PropertiesSpinEdit DisplayFormatString="{0:p0}" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="(%) Real" FieldName="PercentualReal" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="90px">
                                                                    <PropertiesSpinEdit DisplayFormatString="{0:p0}" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                                <dxwtl:TreeListTextColumn Caption="Desempenho" FieldName="CorStatus" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" Width="95px">
                                                                    <DataCellTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <%# "<img alt='' style='border:0px' src='" + (Eval("CorStatus").ToString() != "" ? "../../imagens/" + Eval("CorStatus").ToString().Trim() + ".GIF" : "../../imagens/vazioPequeno.GIF") + "'/>"%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </DataCellTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn FieldName="CodigoItem" ShowInCustomizationForm="True" VisibleIndex="6"
                                                                    Visible="False">
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn FieldName="CodigoItemSuperior" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="7">
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn FieldName="EstruturaHierarquica" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="8">
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn FieldName="EstruturaHierarquicaSup" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="9">
                                                                    <CellStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </CellStyle>
                                                                </dxwtl:TreeListTextColumn>
                                                            </Columns>
                                                            <Settings SuppressOuterGridLines="True" VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowFocusedNode="True" />
                                                            <Styles>
                                                                <AlternatingNode BackColor="#EBEBEB" Enabled="True">
                                                                </AlternatingNode>
                                                            </Styles>
                                                        </dxwtl:ASPxTreeList>
                                                        <table class="<%=estiloFooter %>" cellspacing="0" cellpadding="0" width="100%"
                                                            style="border-top: none;">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="middle" style="padding: 3px;">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <img src="../../imagens/verdeMenor.gif" width="16" />
                                                                                </td>
                                                                                <td>
                                                                                    <span style="">
                                                                                        <asp:Label ID="lblVerde" runat="server" EnableViewState="False"
                                                                                            Text="Satisfatório"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                                <td>
                                                                                    <img src="../../imagens/amareloMenor.gif" width="16" />
                                                                                </td>
                                                                                <td>
                                                                                    <span style="">
                                                                                        <asp:Label ID="lblAmarelo" runat="server" EnableViewState="False"
                                                                                            Text="Atenção"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                                <td>
                                                                                    <img src="../../imagens/vermelhoMenor.gif" width="16" />
                                                                                </td>
                                                                                <td>
                                                                                    <span style="">
                                                                                        <asp:Label ID="lblVermelho" runat="server" EnableViewState="False"
                                                                                            Text="Crítico"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                                <td>
                                                                                    <img src="../../imagens/brancoMenor.gif" width="16" />
                                                                                </td>
                                                                                <td>
                                                                                    <span style="">
                                                                                        <asp:Label ID="lblBranco" runat="server" EnableViewState="False"
                                                                                            Text="Sem informação"></asp:Label>
                                                                                    </span>
                                                                                </td>
                                                                                <td style="<%=displayLaranja %>;">
                                                                                    <img src="../../imagens/laranjaMenor.gif" width="16" />
                                                                                </td>
                                                                                <td style="<%=displayLaranja %>; padding-right: 10px; padding-left: 3px;">
                                                                                    <span>
                                                                                        <asp:Label ID="lblLaranja" runat="server" EnableViewState="False"
                                                                                            Text="Finalizando"></asp:Label>
                                                                                    </span>
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
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
