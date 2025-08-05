<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relPendenciaAtualizacaoIndicadores.aspx.cs" Inherits="_Estrategias_Relatorios_relPendenciaAtualizacaoIndicadores" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table>
        <tr style="height:26px">
            <td valign="middle" style="height: 10px">
                <table>
                    <tr style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%;
                        height: 26px">
                        <td align="left" valign="middle">
                            &nbsp;
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Relatório de Pendência de Atualização de Indicadores"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="1" style="width: 7px" valign="top">
                        </td>
                        <td colspan="1" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 7px" valign="top">
                        </td>
                        <td colspan="1" valign="top">
                <table>
                    <tr>
                        <td style="width: 205px">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                             ValueType="System.String">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td style="padding-left: 2px">
                                        <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                            Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" 
                                             Width="23px">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                        Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px">
                                                    </dxe:ASPxImage>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                OnClick="btnExcel_Click" Text="Exportar">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxhf:ASPxHiddenField>
                <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" 
                                PaperKind="A4" Landscape="True" LeftMargin="20" RightMargin="20" 
                                MaxColumnWidth="400" onrenderbrick="gvExporter_RenderBrick">
                    <Styles>
                        <Header >
                        </Header>
                        <Cell >
                        </Cell>
                        <GroupRow >
                        </GroupRow>
                        <AlternatingRowCell >
                        </AlternatingRowCell>
                    </Styles>
                </dxwgv:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 7px" valign="top">
                        </td>
                        <td colspan="1" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="width: 7px;" valign="top">
                        </td>
                        <td colspan="1" valign="top">
                            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                                 KeyFieldName="DataVencimento"
                                Width="100%" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                <SettingsBehavior AllowFocusedRow="True" />
                                <SettingsPager AlwaysShowPager="True">
                                </SettingsPager>
                                <SettingsText  GroupPanel="Arraste o cabe&#231;alho da coluna que deseja agrupar" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="NomeUnidadeNegocio" VisibleIndex="1"
                                        Width="24%">
                                        <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" FieldName="NomeUsuario"
                                        VisibleIndex="2" Width="21%">
                                        <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ano" FieldName="Ano" VisibleIndex="3" Width="6%">
                                        <Settings AutoFilterCondition="Equals" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Mes" VisibleIndex="4"
                                        Width="9%">
                                        <Settings AutoFilterCondition="Equals" />
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Vencimento" FieldName="DataVencimento" VisibleIndex="5"
                                        Width="11%">
                                        <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy">
                                        </PropertiesTextEdit>
                                        <Settings AutoFilterCondition="Contains" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Indicador" FieldName="NomeIndicador" GroupIndex="0"
                                        SortIndex="0" SortOrder="Ascending" VisibleIndex="0" Width="29%">
                                        <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFilterRow="True" ShowGroupedColumns="True" ShowGroupPanel="True" HorizontalScrollBarMode="Visible"
                                    VerticalScrollBarMode="Visible" />
                                <Styles>
                                    <FocusedRow >
                                    </FocusedRow>
                                    <FilterRow >
                                    </FilterRow>
                                    <FilterCell >
                                    </FilterCell>
                                    <FilterBarExpressionCell >
                                    </FilterBarExpressionCell>
                                    <FilterRowMenuItem >
                                    </FilterRowMenuItem>
                                </Styles>
                                <StylesFilterControl>
                                    <Value >
                                    </Value>
                                </StylesFilterControl>
                                <SettingsDetail ExportIndex="-1" ExportMode="Expanded" />
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
      var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
       if(isIE8)
       {
            document.forms[0].style.overflow = "hidden";
       }
       else
       {
            document.forms[0].style.position = "relative";
            document.forms[0].style.overflow = "hidden";       
       }

    </script>
</asp:Content>

