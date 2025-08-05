<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="url_mapaEntregasProjetos1.aspx.cs" Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_mapaEntregasProjetos1"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">


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
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%"
                                                        style="padding-bottom: 5px">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 57px" valign="middle">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Verdana"
                                                                                        Font-Size="8pt" Text="Período:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 120px" valign="middle">
                                                                                    <dxe:ASPxDateEdit ID="txtInicio" runat="server"
                                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                                        EditFormatString="dd/MM/yyyy" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Width="110px">
                                                                                        <CalendarProperties>
                                                                                            <DayHeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayOtherMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayOtherMonthStyle>
                                                                                            <DayWeekendStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayWeekendStyle>
                                                                                            <DayOutOfRangeStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayOutOfRangeStyle>
                                                                                            <ButtonStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <FastNavMonthAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </FastNavMonthAreaStyle>
                                                                                            <FastNavFooterStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </FastNavFooterStyle>
                                                                                        </CalendarProperties>
                                                                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                            PaddingRight="0px" PaddingTop="0px" />
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td style="width: 20px" valign="middle">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Names="Verdana"
                                                                                        Font-Size="8pt" Text="a">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 120px" valign="middle">
                                                                                    <dxe:ASPxDateEdit ID="txtTermino" runat="server"
                                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                                        EditFormatString="dd/MM/yyyy" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Width="110px">
                                                                                        <CalendarProperties>
                                                                                            <DayHeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <DayOtherMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayOtherMonthStyle>
                                                                                            <DayWeekendStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </DayWeekendStyle>
                                                                                            <ButtonStyle Font-Names="Verdana" Font-Size="8pt">
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                            <FooterStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                                        </CalendarProperties>
                                                                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                                            PaddingRight="0px" PaddingTop="0px" />
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td style="width: 90px" valign="middle">
                                                                                    <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                                                                        Font-Names="Verdana" Font-Size="8pt" Text="Selecionar">
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
                                                        ClientInstanceName="gvDados" Font-Names="Verdana" Font-Size="8pt"
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
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
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
                                                                <CellStyle Font-Names="Verdana" Font-Size="8pt" Wrap="True">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="Data Pactuada" FieldName="TerminoLB"
                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px">
                                                                    <CalendarProperties>
                                                                        <DayHeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <DayStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <DaySelectedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DaySelectedStyle>
                                                                        <DayOtherMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayOtherMonthStyle>
                                                                        <DayWeekendStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayWeekendStyle>
                                                                        <DayOutOfRangeStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayOutOfRangeStyle>
                                                                        <ButtonStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <PressedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </PressedStyle>
                                                                            <HoverStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </HoverStyle>
                                                                        </ButtonStyle>
                                                                        <FastNavStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavStyle>
                                                                        <FastNavMonthAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavMonthAreaStyle>
                                                                        <FastNavYearAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavYearAreaStyle>
                                                                        <FastNavMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <HoverStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </HoverStyle>
                                                                        </FastNavMonthStyle>
                                                                        <FastNavYearStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <SelectedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </SelectedStyle>
                                                                            <HoverStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </HoverStyle>
                                                                        </FastNavYearStyle>
                                                                        <FastNavFooterStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavFooterStyle>
                                                                        <Style Font-Names="Verdana" Font-Size="8pt">
                                                                    </Style>
                                                                    </CalendarProperties>
                                                                </PropertiesDateEdit>
                                                                <CellStyle Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="Data Real" FieldName="TerminoReal"
                                                                ShowInCustomizationForm="True" VisibleIndex="7" Width="120px">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px">
                                                                    <CalendarProperties ClearButtonText="Limpar">
                                                                        <DayHeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <DayStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <DaySelectedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DaySelectedStyle>
                                                                        <DayOtherMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayOtherMonthStyle>
                                                                        <DayWeekendStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayWeekendStyle>
                                                                        <DayOutOfRangeStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </DayOutOfRangeStyle>
                                                                        <ButtonStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <PressedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </PressedStyle>
                                                                            <HoverStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </HoverStyle>
                                                                        </ButtonStyle>
                                                                        <HeaderStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <FooterStyle Font-Names="Verdana" Font-Size="8pt" />
                                                                        <FastNavStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavStyle>
                                                                        <FastNavMonthAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavMonthAreaStyle>
                                                                        <FastNavYearAreaStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavYearAreaStyle>
                                                                        <FastNavMonthStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <SelectedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </SelectedStyle>
                                                                            <HoverStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </HoverStyle>
                                                                        </FastNavMonthStyle>
                                                                        <FastNavYearStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            <SelectedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                            </SelectedStyle>
                                                                        </FastNavYearStyle>
                                                                        <FastNavFooterStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FastNavFooterStyle>
                                                                        <FocusedStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </FocusedStyle>
                                                                        <InvalidStyle Font-Names="Verdana" Font-Size="8pt">
                                                                        </InvalidStyle>
                                                                        <Style Font-Names="Verdana" Font-Size="8pt">
                                                                    </Style>
                                                                    </CalendarProperties>
                                                                </PropertiesDateEdit>
                                                                <CellStyle Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Situação" FieldName="Situacao"
                                                                ShowInCustomizationForm="True" VisibleIndex="8" Width="140px">
                                                                <PropertiesTextEdit Width="200px">
                                                                </PropertiesTextEdit>
                                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                <CellStyle Font-Names="Verdana" Font-Size="8pt">
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

    </form>
</body>
</html>
