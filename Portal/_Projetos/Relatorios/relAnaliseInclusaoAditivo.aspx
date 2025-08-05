<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseInclusaoAditivo.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseInclusaoAditivo" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%; height: 28px">
        <tr>
            <td style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False"
                    Text="Aditivos Incluídos por Período"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
                    OnCallback="pnCallbackDados_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top: 10px">
                                            <table id="tabelaFiltros" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 424px" align="left">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 57px" valign="middle">
                                                                            <dxe:ASPxLabel runat="server" Text="Per&#237;odo:" 
                                                                                ID="ASPxLabel1">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 120px" valign="middle">
                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" 
                                                                                ID="txtInicio">
                                                                                <CalendarProperties>
                                                                                    <DayHeaderStyle ></DayHeaderStyle>
                                                                                    <DayStyle ></DayStyle>
                                                                                    <DayOtherMonthStyle >
                                                                                    </DayOtherMonthStyle>
                                                                                    <DayWeekendStyle >
                                                                                    </DayWeekendStyle>
                                                                                    <DayOutOfRangeStyle >
                                                                                    </DayOutOfRangeStyle>
                                                                                    <ButtonStyle >
                                                                                    </ButtonStyle>
                                                                                    <HeaderStyle ></HeaderStyle>
                                                                                    <FastNavMonthAreaStyle >
                                                                                    </FastNavMonthAreaStyle>
                                                                                    <FastNavFooterStyle >
                                                                                    </FastNavFooterStyle>
                                                                                </CalendarProperties>
                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                </Paddings>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td style="width: 20px" valign="middle">
                                                                            <dxe:ASPxLabel runat="server" Text="a"  ID="ASPxLabel2">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 120px" valign="middle">
                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" 
                                                                                ID="txtTermino">
                                                                                <CalendarProperties>
                                                                                    <DayHeaderStyle ></DayHeaderStyle>
                                                                                    <DayStyle ></DayStyle>
                                                                                    <DayOtherMonthStyle >
                                                                                    </DayOtherMonthStyle>
                                                                                    <DayWeekendStyle >
                                                                                    </DayWeekendStyle>
                                                                                    <ButtonStyle >
                                                                                    </ButtonStyle>
                                                                                    <HeaderStyle ></HeaderStyle>
                                                                                    <FooterStyle ></FooterStyle>
                                                                                </CalendarProperties>
                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                </Paddings>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td style="width: 90px" valign="middle">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar"
                                                                                ID="btnSelecionar">
                                                                                <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                </Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 40px" valign="middle">
                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp"
                                                                                Cursor="pointer" ID="ImgHelp">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 205px">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"
                                                                                                 ID="ddlExporta">
                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
                                                                                            </dxe:ASPxComboBox>
                                                                                        </td>
                                                                                        <td style="padding-left: 2px">
                                                                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                                                                ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
<SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>
                                                                                                <PanelCollection>
                                                                                                    <dxp:PanelContent ID="PanelContent2" runat="server">
                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                                                            Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                                                                        </dxe:ASPxImage>
                                                                                                    </dxp:PanelContent>
                                                                                                </PanelCollection>
                                                                                            </dxcp:ASPxCallbackPanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" Text="Exportar" 
                                                                                ID="btnExportar" OnClick="btnExportar_Click">
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                                            </dxhf:ASPxHiddenField>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px">
                                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
                                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                            </dxwgv:ASPxGridViewExporter>
                                            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoAditivoContrato"
                                                Width="100%">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoAditivoContrato" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Nº Contrato" FieldName="NumeroContrato" ShowInCustomizationForm="True"
                                                        VisibleIndex="1" Width="120px">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Razão Social" FieldName="RazaoSocial" ShowInCustomizationForm="True"
                                                        VisibleIndex="2" Width="200px">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Objeto" FieldName="Objeto" ShowInCustomizationForm="True"
                                                        VisibleIndex="3">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Data Inclusão" FieldName="DataInclusao" ShowInCustomizationForm="True"
                                                        VisibleIndex="4" Width="85px">
                                                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                        </PropertiesTextEdit>
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Valor Aditivo" FieldName="ValorAditivo" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="100px">
                                                        <PropertiesTextEdit DisplayFormatString="{0:c}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Valor Contrato" FieldName="ValorContrato"
                                                        ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                        <PropertiesTextEdit DisplayFormatString="{0:c}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de Aditivo" FieldName="TipoAditivo"
                                                        ShowInCustomizationForm="True" VisibleIndex="7" Width="150px">
                                                        <PropertiesComboBox ValueType="System.String">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Prazo" Value="Prazo" />
                                                                <dxe:ListEditItem Text="Prazo e Valor" Value="Prazo e Valor" />
                                                                <dxe:ListEditItem Text="Valor" Value="Valor" />
                                                                <dxe:ListEditItem Text="Troca de Material" Value="Troca de Material" />
                                                                <dxe:ListEditItem Text="Termo de Encerramento" Value="Termo de Encerramento" />
                                                            </Items>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Nova Vigência" FieldName="NovaDataVigencia"
                                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="85px">
                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                        </PropertiesDateEdit>
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                                            </dxwgv:ASPxGridView>
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
