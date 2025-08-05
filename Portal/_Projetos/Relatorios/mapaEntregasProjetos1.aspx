<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="mapaEntregasProjetos1.aspx.cs" Inherits="_Projetos_Relatorios_mapaEntregasProjetos1"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%; height: 26px">
        <tr>
            <td style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Mapa de Entregas"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
                    OnCallback="pnCallbackDados_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="Div1" runat="server"
                                                style="padding-left: 10px; padding-right: 10px; padding-top: 5px;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 57px" valign="middle">
                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                                    Text="Período:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 120px" valign="middle">
                                                                                <dxe:ASPxDateEdit ID="txtInicio" runat="server"
                                                                                    DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                                    EditFormatString="dd/MM/yyyy"
                                                                                    Width="110px">
                                                                                    <CalendarProperties>
                                                                                        <DayHeaderStyle />
                                                                                        <DayStyle />
                                                                                        <DayOtherMonthStyle>
                                                                                        </DayOtherMonthStyle>
                                                                                        <DayWeekendStyle>
                                                                                        </DayWeekendStyle>
                                                                                        <DayOutOfRangeStyle>
                                                                                        </DayOutOfRangeStyle>
                                                                                        <ButtonStyle>
                                                                                        </ButtonStyle>
                                                                                        <HeaderStyle />
                                                                                        <FastNavMonthAreaStyle>
                                                                                        </FastNavMonthAreaStyle>
                                                                                        <FastNavFooterStyle>
                                                                                        </FastNavFooterStyle>
                                                                                    </CalendarProperties>
                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="width: 20px" valign="middle">
                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                    Text="a">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 120px" valign="middle">
                                                                                <dxe:ASPxDateEdit ID="txtTermino" runat="server"
                                                                                    DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                                    EditFormatString="dd/MM/yyyy"
                                                                                    Width="110px">
                                                                                    <CalendarProperties>
                                                                                        <DayHeaderStyle />
                                                                                        <DayStyle />
                                                                                        <DayOtherMonthStyle>
                                                                                        </DayOtherMonthStyle>
                                                                                        <DayWeekendStyle>
                                                                                        </DayWeekendStyle>
                                                                                        <ButtonStyle>
                                                                                        </ButtonStyle>
                                                                                        <HeaderStyle />
                                                                                        <FooterStyle />
                                                                                    </CalendarProperties>
                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="width: 90px" valign="middle">
                                                                                <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                                                                    Text="Selecionar">
                                                                                    <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback();
}" />
                                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td valign="middle">&nbsp;</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                                    GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                                </dxwgv:ASPxGridViewExporter>
                                                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="gvDados"
                                                    KeyFieldName="CodigoProjeto;NomeTarefa"
                                                    OnAfterPerformCallback="gvDados_AfterPerformCallback"
                                                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                                    Width="100%">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
	//if(e.buttonID == &quot;btnFormulario&quot;)
	//{			
	//	onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
	//}
}"
                                                        FocusedRowChanged="function(s, e) {
		//OnGridFocusedRowChanged(s,true);
}" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" "
                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="40px">
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
                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto"
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Entrega" FieldName="NomeTarefa"
                                                            ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <PropertiesTextEdit Width="200px">
                                                            </PropertiesTextEdit>
                                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                            <CellStyle Wrap="True">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Data Pactuada" FieldName="TerminoLB"
                                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px">
                                                                <CalendarProperties>
                                                                    <DayHeaderStyle />
                                                                    <DayStyle />
                                                                    <DaySelectedStyle>
                                                                    </DaySelectedStyle>
                                                                    <DayOtherMonthStyle>
                                                                    </DayOtherMonthStyle>
                                                                    <DayWeekendStyle>
                                                                    </DayWeekendStyle>
                                                                    <DayOutOfRangeStyle>
                                                                    </DayOutOfRangeStyle>
                                                                    <ButtonStyle>
                                                                        <PressedStyle>
                                                                        </PressedStyle>
                                                                        <HoverStyle>
                                                                        </HoverStyle>
                                                                    </ButtonStyle>
                                                                    <FastNavStyle>
                                                                    </FastNavStyle>
                                                                    <FastNavMonthAreaStyle>
                                                                    </FastNavMonthAreaStyle>
                                                                    <FastNavYearAreaStyle>
                                                                    </FastNavYearAreaStyle>
                                                                    <FastNavMonthStyle>
                                                                        <HoverStyle>
                                                                        </HoverStyle>
                                                                    </FastNavMonthStyle>
                                                                    <FastNavYearStyle>
                                                                        <SelectedStyle>
                                                                        </SelectedStyle>
                                                                        <HoverStyle>
                                                                        </HoverStyle>
                                                                    </FastNavYearStyle>
                                                                    <FastNavFooterStyle>
                                                                    </FastNavFooterStyle>
                                                                    <Style>
                                                                    </Style>
                                                                </CalendarProperties>
                                                            </PropertiesDateEdit>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Data Real" FieldName="TerminoReal"
                                                            ShowInCustomizationForm="True" VisibleIndex="7" Width="120px">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px">
                                                                <CalendarProperties ClearButtonText="Limpar">
                                                                    <DayHeaderStyle />
                                                                    <DayStyle />
                                                                    <DaySelectedStyle>
                                                                    </DaySelectedStyle>
                                                                    <DayOtherMonthStyle>
                                                                    </DayOtherMonthStyle>
                                                                    <DayWeekendStyle>
                                                                    </DayWeekendStyle>
                                                                    <DayOutOfRangeStyle>
                                                                    </DayOutOfRangeStyle>
                                                                    <ButtonStyle>
                                                                        <PressedStyle>
                                                                        </PressedStyle>
                                                                        <HoverStyle>
                                                                        </HoverStyle>
                                                                    </ButtonStyle>
                                                                    <HeaderStyle />
                                                                    <FooterStyle />
                                                                    <FastNavStyle>
                                                                    </FastNavStyle>
                                                                    <FastNavMonthAreaStyle>
                                                                    </FastNavMonthAreaStyle>
                                                                    <FastNavYearAreaStyle>
                                                                    </FastNavYearAreaStyle>
                                                                    <FastNavMonthStyle>
                                                                        <SelectedStyle>
                                                                        </SelectedStyle>
                                                                        <HoverStyle>
                                                                        </HoverStyle>
                                                                    </FastNavMonthStyle>
                                                                    <FastNavYearStyle>
                                                                        <SelectedStyle>
                                                                        </SelectedStyle>
                                                                    </FastNavYearStyle>
                                                                    <FastNavFooterStyle>
                                                                    </FastNavFooterStyle>
                                                                    <FocusedStyle>
                                                                    </FocusedStyle>
                                                                    <InvalidStyle>
                                                                    </InvalidStyle>
                                                                    <Style>
                                                                    </Style>
                                                                </CalendarProperties>
                                                            </PropertiesDateEdit>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Situação" FieldName="Situacao"
                                                            ShowInCustomizationForm="True" VisibleIndex="8" Width="140px">
                                                            <PropertiesTextEdit Width="200px">
                                                            </PropertiesTextEdit>
                                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                            <CellStyle>
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Unidade de Negócio" FieldName="NomeUnidadeNegocio"
                                                            GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0"
                                                            SortOrder="Ascending" VisibleIndex="2" Width="250px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto"
                                                            GroupIndex="2" ShowInCustomizationForm="True" SortIndex="2"
                                                            SortOrder="Ascending" VisibleIndex="4">
                                                            <HeaderStyle Wrap="True" />
                                                            <CellStyle Wrap="True">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Programa" FieldName="NomePrograma" GroupIndex="1" ShowInCustomizationForm="True" SortIndex="1" SortOrder="Ascending" VisibleIndex="3">
                                                        </dxtv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm" />
                                                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"
                                                        ShowGroupPanel="True" />
                                                </dxwgv:ASPxGridView>
                                            </div>
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
