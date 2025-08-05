<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAnaliseIndicadores2.aspx.cs" Inherits="_relAnaliseIndicadores2"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script language="javascript" type="text/javascript">

        function SalvarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("save_layout"); } }
            window.top.mostraConfirmacao(traducao.relAnaliseIndicadores2_deseja_salvar_as_altera__es_realizadas_no_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
        }

        function RestaurarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
            window.top.mostraConfirmacao(traducao.relAnaliseIndicadores2_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
        }

    </script>
<%--    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Análise de Indicadores"></asp:Label>
                        </td>
                        <td align="right" style="display: none; width: 8px; height: 26px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>

        <table>
        <tr>
            <td>
                <span style="color: #aaaaaa !important; font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif; font-size: 18px; font-weight: 200 !important; display: inline-block !important; padding: 10px 10px 10px 10px !important;">
                    <asp:Literal runat="server" Text="<%$ Resources:traducao, relAnaliseIndicadores2_an_lise_de_indicadores %>" />
                </span>
            </td>
        </tr>
    </table>



    <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
        Width="100%" OnCallback="pnCallbackDados_Callback">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top: 10px">
                                <table id="tabelaFiltro" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 153px" align="center">
                                                                <dxe:ASPxCheckBox runat="server" Text="Valores Acumulados" ClientInstanceName="checkAcumulado"
                                                                    Width="147px" Height="25px"  ID="checkAcumulado"
                                                                    Checked="True" CheckState="Checked">
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" SelectedIndex="0"
                                                                    ClientInstanceName="rbOpcao" Width="120px" Height="25px"
                                                                    ID="rbOpcao">
                                                                    <Paddings Padding="0px"></Paddings>
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == &quot;U&quot;)
	{
		ddlUnidade.SetVisible(true);
		ddlIndicador.SetVisible(false);
        ddlMapa.SetVisible(false);
	}
	else if(s.GetValue() == &quot;I&quot;)
	{
		ddlUnidade.SetVisible(false);
		ddlIndicador.SetVisible(true);
        ddlMapa.SetVisible(false);
	}
    else
	{
		ddlUnidade.SetVisible(false);
		ddlIndicador.SetVisible(false);
        ddlMapa.SetVisible(true);
	}	
}"></ClientSideEvents>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Mapa" Value="M" Selected="True" />
                                                                        <dxe:ListEditItem Text="Unidade" Value="U"></dxe:ListEditItem>
                                                                        <dxe:ListEditItem Text="Indicador" Value="I"></dxe:ListEditItem>
                                                                    </Items>
                                                                </dxe:ASPxRadioButtonList>
                                                            </td>
                                                            <td style="width: 5px" align="left">
                                                            </td>
                                                            <td style="width: 256px" align="left">
                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="250px" Height="25px"
                                                                    ClientInstanceName="ddlIndicador" ClientVisible="False"
                                                                    ID="ddlIndicador">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                                </dxe:ASPxComboBox>
                                                                <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" ClientVisible="False"
                                                                     Height="25px" Width="250px">
                                                                </dxe:ASPxComboBox>
                                                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                    TextFormatString="{0}" Width="250px" Height="25px" ClientInstanceName="ddlUnidade"
                                                                     ID="ddlUnidades" ClientVisible="False">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Valor);
}"></ClientSideEvents>
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="200px" Caption="Sigla">
                                                                        </dxe:ListBoxColumn>
                                                                        <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="350px" Caption="Unidade">
                                                                        </dxe:ListBoxColumn>
                                                                    </Columns>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="Desempenho:" 
                                                                    ID="ASPxLabel3">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="Ano:"  ID="ASPxLabel40">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="right">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="display: none; width: 8px; height: 61px" align="left">
                                                            </td>
                                                            <td style="width: 91px" align="left">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="84px"
                                                                    Height="25px"  ID="btnSelecionar">
                                                                    <ClientSideEvents Click="function(s, e) {
	pnCallbackDados.PerformCallback(&#39;A&#39;);
}"></ClientSideEvents>
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                            </td>
                                            <td align="right">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <div id="divBotoes" style="padding-bottom: 5px; padding-top: 5px;">
                                                                    <span style="margin: auto; padding-right: 10px">
                                                                        <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                                            ToolTip="Exportar" Cursor="pointer" Height="16px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                            Width="16px">
                                                                            <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback(&quot;export&quot;);
}" />
                                                                        </dxe:ASPxImage>
                                                                    </span><span style="margin: auto; padding-right: 10px">
                                                                        <dxe:ASPxImage ID="imgSalvaConfiguracoes" runat="server" ClientInstanceName="imgSalvaConfiguracoes"
                                                                            ToolTip="Salvar layout da consulta" Cursor="pointer" Height="14px" ImageUrl="~/imagens/salvar.png"
                                                                            Width="14px">
                                                                            <ClientSideEvents Click="SalvarConfiguracoesLayout" />
                                                                        </dxe:ASPxImage>
                                                                    </span><span style="margin: auto; padding-right: 10px">
                                                                        <dxe:ASPxImage ID="imgRestaurarLayout" runat="server" ClientInstanceName="imgRestaurarLayout"
                                                                            ToolTip="Restaurar layout" Cursor="pointer" Height="16px" ImageUrl="~/imagens/restaurar.png"
                                                                            Width="16px">
                                                                            <ClientSideEvents Click="RestaurarConfiguracoesLayout" />
                                                                        </dxe:ASPxImage>
                                                                    </span>
                                                                </div>
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
                                <div id="Div2" runat="server" style="overflow: auto;">
                                    <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgDadosIndicador" 
                                        Width="98%" ID="pvgDadosIndicador" OnCustomCellDisplayText="pvgDadosIndicador_CustomCellDisplayText"
                                        OnCustomFieldSort="pvgDadosIndicador_CustomFieldSort" OnCustomSummary="pvgDadosIndicador_CustomSummary"
                                        OnFieldVisibleChanged="pvgDadosIndicador_FieldVisibleChanged" EnableRowsCache="False"
                                        EnableViewState="False" Height="100%" OnHtmlCellPrepared="pvgDadosIndicador_HtmlCellPrepared" OnHtmlFieldValuePrepared="pvgDadosIndicador_HtmlFieldValuePrepared">
                                        <BorderLeft BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></BorderLeft>
                                        <BorderTop BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></BorderTop>
                                        <BorderRight BorderStyle="Solid" BorderWidth="1px"></BorderRight>
                                        <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></BorderBottom>
                                        <Fields>
                                        </Fields>
                                        <OptionsView ShowColumnGrandTotals="False" ShowRowGrandTotals="False" 
                                            HorizontalScrollBarMode="Visible" RowTotalsLocation="Tree">
                                        </OptionsView>
                                        <OptionsPager Position="Bottom">
                                            <PageSizeItemSettings Caption="Itens/página:" Visible="True">
                                            </PageSizeItemSettings>
                                        </OptionsPager>
                                        <Border BorderStyle="None"></Border>
                                        <BorderLeft BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderTop BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderRight BorderStyle="Solid" BorderWidth="1px" />
                                        <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </dxwpg:ASPxPivotGrid>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>
    <dxpgwx:ASPxPivotGridExporter ID="exporter" runat="server" OnCustomExportCell="exporter_CustomExportCell"
        ASPxPivotGridID="pvgDadosIndicador" OnCustomExportFieldValue="exporter_CustomExportFieldValue"
        OnCustomExportHeader="exporter_CustomExportHeader">
        <OptionsPrint>
            <PageSettings Landscape="True" PaperKind="A4" />
        </OptionsPrint>
    </dxpgwx:ASPxPivotGridExporter>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter == &quot;export&quot;)
		window.location = &quot;../../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=xls&amp;bInline=False&quot;;
	else if(e.parameter == &quot;save_layout&quot;)
        window.top.mostraMensagem(traducao.relAnaliseIndicadores2_o_layout_da_consulta_foi_salvo_com_sucesso, 'sucesso', false, false, null);
	else if(e.parameter == &quot;restore_layout&quot;){
        window.top.mostraMensagem(traducao.relAnaliseIndicadores2_o_layout_foi_restaurado_com_sucesso_, 'sucesso', false, false, null);
        window.location.reload(true);
    }
}" />
    </dxcb:ASPxCallback>
</asp:Content>
