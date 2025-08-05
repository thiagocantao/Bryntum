<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinanceiroDetalhes.aspx.cs"
    Inherits="_Projetos_DadosProjeto_FinanceiroDetalhes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function ExibeDetalheValorMes(parametro) {
            var par = parametro.toString().split(';');
            hfGeral.Set("CodigoProjeto", par[0]);
            hfGeral.Set("Mes", par[1]);
            hfGeral.Set("Tipo", par[2]);
            hfGeral.Set("DespesaReceita", par[3]);
            pnGridDetalheMes.SetVisible(true);
            //gvDetalhesMes.SetVisible(true);
            gvDetalhesMes.PerformCallback(parametro);

            //lblTipoDetalhe.SetText(par[2] + " - " + par[3] + " - " + par[1]);
            lblTipoDetalhe.SetText(par[4]);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="btnExcel1" runat="server" Height="23px" ImageUrl="~/imagens/botoes/btnExcel.png"
                        OnClick="btnExcel_Click" ToolTip="Exportar para excel" Width="23px" />
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblAnoReferencia" runat="server" ClientInstanceName="lblAnoReferencia"
                        Text="Ano de referência:">
                    </dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmbAnoReferencia" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cmbAnoReferencia_SelectedIndexChanged" Width="80px">
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
        
        <dxwgv:ASPxGridView ID="gvDados" runat="server" Width="100%" AutoGenerateColumns="False"
             OnCustomColumnDisplayText="gvDados_CustomColumnDisplayText"
            OnCustomUnboundColumnData="gvDados_CustomUnboundColumnData" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
            Style="float: right">
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="TipoValor" VisibleIndex="0"
                    FixedStyle="Left" Width="200px" Name="TipoValor">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="AnoAnterior" Caption="Anos Anteriores" VisibleIndex="1"
                    Name="Anteriores">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}"/>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Mes_01" Caption="Jan"
                    VisibleIndex="2" Name="M_01">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Fev" FieldName="Mes_02" VisibleIndex="3" Name="M_02">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mar" FieldName="Mes_03" VisibleIndex="4" Name="M_03">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Abr" FieldName="Mes_04" VisibleIndex="5" Name="M_04">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mai" FieldName="Mes_05" VisibleIndex="6" Name="M_05">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Jun" FieldName="Mes_06" VisibleIndex="7" Name="M_06">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Jul" FieldName="Mes_07" VisibleIndex="8" Name="M_07">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Ago" FieldName="Mes_08" VisibleIndex="9" Name="M_08">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Set" FieldName="Mes_09" VisibleIndex="10"
                    Name="M_09">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Out" FieldName="Mes_10" VisibleIndex="11"
                    Name="M_10">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nov" FieldName="Mes_11" VisibleIndex="12"
                    Name="M_11">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Dez" FieldName="Mes_12" VisibleIndex="13"
                    Name="M_12">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Total Ano" FieldName="Total" VisibleIndex="14"
                    Name="TotalAno" UnboundType="Decimal">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="AnoPosterior" VisibleIndex="15"
                    Caption="Anos Posteriores" Name="Posteriores">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="DespesaReceita" 
                    GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="16"
                    Name="DespesaReceita">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" AutoExpandAllGroups="True" />
            <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords">
            </SettingsPager>
            <Settings GroupFormat="{1} {2}" HorizontalScrollBarMode="Visible" />
            <Styles>
                <GroupRow Font-Bold="True" HorizontalAlign="Left">
                </GroupRow>
            </Styles>
        </dxwgv:ASPxGridView>
        <p>
            &nbsp;
        </p>
        <dxp:ASPxPanel ID="pnGridDetalheMes" runat="server" ClientInstanceName="pnGridDetalheMes"
            ClientVisible="false">
            <PanelCollection>
                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <asp:ImageButton ID="btnExcel2" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                        OnClick="btnExcel_Click" ToolTip="Exportar para excel" />
                    <dxe:ASPxLabel ID="lblTipoDetalhe" runat="server" ClientInstanceName="lblTipoDetalhe">
                    </dxe:ASPxLabel>
                    <dxwgv:ASPxGridView ID="gvDetalhesMes" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="gvDetalhesMes" ClientVisible="true" OnAfterPerformCallback="gvDetalhesMes_AfterPerformCallback"
                        Width="100%">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Cód. Unidade" FieldName="codigoUnidade" ShowInCustomizationForm="True"
                                VisibleIndex="0" Width="70px" Name="codigoUnidade">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="nomeUnidade" ShowInCustomizationForm="True"
                                VisibleIndex="1" Width="120px" Name="nomeUnidade">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Código CR" FieldName="codigoCR" ShowInCustomizationForm="True"
                                VisibleIndex="3" Width="65px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="CR" FieldName="nomeCR" ShowInCustomizationForm="True"
                                VisibleIndex="4" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Cód. Grupo" FieldName="codigoGrupo" ShowInCustomizationForm="True"
                                VisibleIndex="5" Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="nomeGrupo" ShowInCustomizationForm="True"
                                VisibleIndex="6" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Cód. Conta" FieldName="codigoConta" ShowInCustomizationForm="True"
                                VisibleIndex="7" Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Conta" FieldName="nomeConta" ShowInCustomizationForm="True"
                                VisibleIndex="8" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Lançamento" FieldName="lancamento" ShowInCustomizationForm="True"
                                VisibleIndex="9" Width="300px" Name="lancamento">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor" FieldName="valor" ShowInCustomizationForm="True"
                                VisibleIndex="10" Width="80px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}"/>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Data" FieldName="dataLancamento" ShowInCustomizationForm="True"
                                VisibleIndex="11" Width="80px" Name="dataLancamento">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="50">
                        </SettingsPager>
                        <Settings ShowGroupPanel="false" />
                    </dxwgv:ASPxGridView>
                </dxp:PanelContent>
            </PanelCollection>
        </dxp:ASPxPanel>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server">
    </dxhf:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" 
        onrenderbrick="gvExporter_RenderBrick">
        <Styles>
            <Cell >
            </Cell>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
