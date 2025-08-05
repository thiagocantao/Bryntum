<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_OlapRecursosTabela.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_OlapCriterios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function atualizaDados() {
            pnCallback.PerformCallback();
        }

        function alteraVisao() {
            if (document.getElementById('tdGrafico').style.display == 'block') {
                document.getElementById('tdOlap').style.display = 'block';
                document.getElementById('tdGrafico').style.display = 'none';
                document.getElementById('tdFiltroRecurso1').style.display = "none";
                document.getElementById('tdFiltroRecurso2').style.display = "none";
                imgModoVisao.SetImageUrl('../imagens/graficos.PNG');
                imgModoVisao.mainElement.title = 'Mostrar Gráfico de Disponibilidade dos Recursos';
            }
            else {
                defineCaminhoGrafico();
                imgModoVisao.SetImageUrl('../imagens/olap.PNG');
                imgModoVisao.mainElement.title = 'Mostrar Olap de Análise de Recursos';
                document.getElementById('tdOlap').style.display = 'none';
                document.getElementById('tdGrafico').style.display = 'block';
                document.getElementById('tdFiltroRecurso1').style.display = "block";
                document.getElementById('tdFiltroRecurso2').style.display = "block";
            }
        }

        function defineCaminhoGrafico() {
            if (document.getElementById('framePrincipal') != null)
                document.getElementById('framePrincipal').src = 'frameProposta_GraficoRH.aspx?DefineAltura=S&CRH=' + ddlRecurso.GetValue() + '&Cenario=' + ddlCenario.GetValue();
        }
    </script>
</head>
<body style="margin-top: 0px; overflow: hidden">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td id='tdOlap'>
                        <div style="overflow: auto; height: <%=alturaTela %>; width: 100%">
                                                                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                        <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                            style="width: 99%">
                                            <tr runat="server">
                                                <td runat="server" style="padding: 3px" valign="top">
                                                    <table cellpadding="0" cellspacing="0" style="height: 22px">
                                                        <tr>
                                                            <td style="padding-right: 10px;">
                                                                <dxm:ASPxMenu ID="menu123" runat="server" BackColor="Transparent"
                                                                    ClientInstanceName="menu123" ItemSpacing="5px" OnInit="menu_Init"
                                                                    OnItemClick="menu_ItemClick">
                                                                    <Paddings Padding="0px" />
                                                                    <Items>
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
                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">

                                        <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid" Width="100%" OnCustomCallback="grid_CustomCallback" OnCustomCellStyle="grid_CustomCellStyle">
                                            <Fields>
                                                <dxwpg:PivotGridField ID="cGrupo" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="RowArea" AreaIndex="0" Caption="Grupo" FieldName="Grupo">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cProjeto" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="RowArea" AreaIndex="1" Caption="Projeto" FieldName="NomeProjeto">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cAno" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea"
                                                    AreaIndex="0" Caption="Ano" FieldName="Ano">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cMes" AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea"
                                                    AreaIndex="1" Caption="M&#234;s" FieldName="Mes" Visible="False">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cPeriodo" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                    Area="ColumnArea" AreaIndex="1" Caption="Per&#237;odo" FieldName="Periodo" Visible="False">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cAlocacao" AllowedAreas="DataArea" Area="DataArea" AreaIndex="0"
                                                    Caption="Aloca&#231;&#227;o (h)" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Custom"
                                                    FieldName="Alocacao" ValueFormat-FormatString="{0:n1}" ValueFormat-FormatType="Custom">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cCapacidade" AllowedAreas="DataArea" Area="DataArea" AreaIndex="1"
                                                    Caption="Capacidade (h)" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Custom"
                                                    FieldName="Capacidade">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cProposta" AllowedAreas="DataArea" Area="DataArea" AreaIndex="2"
                                                    Caption="Proposta (h)" CellFormat-FormatString="{0:n1}" CellFormat-FormatType="Custom"
                                                    FieldName="PropostaAlocacao">
                                                </dxwpg:PivotGridField>
                                                <dxwpg:PivotGridField ID="cDisponibilidade" AllowedAreas="DataArea" Area="DataArea"
                                                    AreaIndex="3" Caption="Disponibilidade (h)" CellFormat-FormatString="{0:n1}"
                                                    CellFormat-FormatType="Custom" FieldName="Disponibilidade">
                                                </dxwpg:PivotGridField>
                                            </Fields>
                                            <OptionsView HorizontalScrollBarMode="Visible" />
                                            <OptionsCustomization CustomizationWindowHeight="100" />
                                            <OptionsPager EllipsisMode="None">
                                            </OptionsPager>
                                        </dxwpg:ASPxPivotGrid>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
	defineCaminhoGrafico();
}" />
                            </dxcp:ASPxCallbackPanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id='tdGrafico' style="display: none">
                        <iframe name="framePrincipal" src="frameProposta_GraficoRH.aspx?DefineAltura=S&CRH=-1" width="100%" scrolling="no"
                            frameborder="0" style="height: <%=alturaTela %>"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"
            ASPxPivotGridID="grid">
        </dxpgwx:ASPxPivotGridExporter>
    </form>
</body>
</html>
