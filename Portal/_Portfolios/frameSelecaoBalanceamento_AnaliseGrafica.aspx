<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_AnaliseGrafica.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_AnaliseGrafica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../scripts/abrirZoom.js" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        function atualizaDados() {
            btnSelecionar.DoClick()
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 5px;
        }
    </style>
</head>
<body style="margin-top: 0">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="right">
                        <table cellpadding="0" cellspacing="0" style="padding-top: 5px">
                            <tr>

                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                        Text="Categoria:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 100px; padding-right: 10px;" align="left">

                                    <dxe:ASPxComboBox ID="ddlCategoria" runat="server" ClientInstanceName="ddlCategoria" Width="130px" IncrementalFilteringMode="Contains" TextFormatString="{0}" ValueType="System.String">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaCategoria" Width="100px" />
                                            <dxe:ListBoxColumn Caption="Categoria" FieldName="DescricaoCategoria" Width="150px" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                        Text="Ano:">
                                    </dxe:ASPxLabel>
                                </td>
                                    <td style="width: 70px" align="left">
                                        <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlEixoX" Width="85px" ValueType="System.String">
                                        </dxe:ASPxComboBox>
                                    </td>
           


                               <td style="width: 60px; padding-left: 10px;" align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                        Text="Análise:">
                                    </dxe:ASPxLabel>
                                </td>
                                    <td style="width: 210px" align="left">
                                        <dxe:ASPxComboBox ID="ddlAnalise" runat="server" ClientInstanceName="ddlEixoX" Width="200px" ValueType="System.String" SelectedIndex="0">
                                            <Items>
                                                <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, comparar_cen_rios %>" Value="0" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_1 %>" Value="1" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_2 %>" Value="2" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_3 %>" Value="3" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_4 %>" Value="4" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_5 %>" Value="5" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_6 %>" Value="6" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_7 %>" Value="7" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_8 %>" Value="8" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, analisar_cen_rio_9 %>" Value="9" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                               


                                <td style="width: 50px;" align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Eixo X:">
                                    </dxe:ASPxLabel>
                                </td>
                                    <td style="width: 160px" align="left">
                                        <dxe:ASPxComboBox ID="ddlEixoX" runat="server" ClientInstanceName="ddlEixoX" Width="145px" ValueType="System.String">
                                        </dxe:ASPxComboBox>
                                    </td>
                                <td style="width: 50px; padding-left: 10px;" align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                        Text="Eixo Y:">
                                    </dxe:ASPxLabel>
                                </td>
                                    <td style="width: 130px" align="left">
                                        <dxe:ASPxComboBox ID="ddlEixoY" runat="server" ClientInstanceName="ddlEixoY" Width="100%">
                                        </dxe:ASPxComboBox>
                                    </td>
                                
                                <td style="width: 110px; padding-left: 5px;" align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                        Text="Tamanho Bolha:" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                    <td style="width: 140px" align="left">
                                        <dxe:ASPxComboBox ID="ddlTamanhoBolha" runat="server" ClientInstanceName="ddlTamanhoBolha"
                                            SelectedIndex="0" ValueType="System.String"
                                            Width="125px">
                                            <Items>
                                                <dxe:ListEditItem Selected="True" Text="Despesa Total" Value="Despesa" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, receita_total %>" Value="Receita" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, total_import_ncia %>" Value="ValorCriterio" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, total_complexidade %>" Value="ValorRisco" />
                                                <dxe:ListEditItem Text="<%$ Resources:traducao, recurso %>" Value="Trabalho" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                 

                              <td style="padding-left: 10px; padding-right: 10px;">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                        Text="Selecionar" Width="90px" ClientInstanceName="btnSelecionar">
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                               
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <div id="chartdiv" align="center">
                                                </div>
                                                <script type="text/javascript">
                                                    getGrafico('<%=grafico_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                                                </script>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <img align="right" alt="Visualizar gráfico em modo ampliado" style="cursor: pointer;"
                                                    onclick="javascript:f_zoomGrafico('../zoom.aspx', 'Análise Gráfica','Flashs/Bubble.swf', '<%=grafico_xmlzoom %>', '0')"
                                                    src="../imagens/zoom.PNG" />
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 545px" valign="top">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView ID="gvReceita" runat="server" AutoGenerateColumns="False" Width="530px" Font-Size="11px" OnCustomCallback="gvReceita_CustomCallback">
                                                    <Templates>
                                                        <TitlePanel>
                                                            			<table cellpadding="0" cellspacing="0" style="width: 420px; margin-top: 20px;" align="right">
                                                                <tr>
                                                                    <td align="right">
                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:traducao, recursos_financeiros_dispon_veis_ %>">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 115px">
                                                                        <dxe:ASPxTextBox ID="txtRFDisponiveis" runat="server" Width="110px" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" ClientInstanceName="txtRFDisponiveis" DisplayFormatString="{0:n2}">
                                                                            <ValidationSettings ErrorDisplayMode="None">
                                                                            </ValidationSettings>
                                                                            <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 80px">
                                                                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:traducao, calcular %>" Width="65px" AutoPostBack="False" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                                                                            <Paddings Padding="0px" />
                                                                            <ClientSideEvents Click="function(s, e) {
	gvReceita.PerformCallback(txtRFDisponiveis.GetValue());
}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </TitlePanel>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" FieldName="Descricao"
                                                            VisibleIndex="0" Width="130px" FixedStyle="Left">
                                                            <HeaderStyle Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 1" FieldName="Cenario1" VisibleIndex="1"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 2" FieldName="Cenario2" VisibleIndex="2"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 3" FieldName="Cenario3" VisibleIndex="3"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 4" FieldName="Cenario4" VisibleIndex="4"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 5" FieldName="Cenario5" VisibleIndex="5"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 6" FieldName="Cenario6" VisibleIndex="6"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 7" FieldName="Cenario7" VisibleIndex="7"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 8" FieldName="Cenario8" VisibleIndex="8"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 9" FieldName="Cenario9" VisibleIndex="9"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="90" VerticalScrollBarMode="Visible" ShowTitlePanel="True" />
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="display: none">
                                                <dxwgv:ASPxGridView ID="gvDespesa" runat="server" AutoGenerateColumns="False" Width="530px" OnCustomCallback="gvDespesa_CustomCallback" Font-Size="11px" ClientVisible="False">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" FieldName="Descricao"
                                                            VisibleIndex="0" Width="130px" FixedStyle="Left">
                                                            <BatchEditModifiedCellStyle Font-Size="11px">
                                                            </BatchEditModifiedCellStyle>
                                                            <HeaderStyle Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 1" FieldName="Cenario1" VisibleIndex="1"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 2" FieldName="Cenario2" VisibleIndex="2"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 3" FieldName="Cenario3" VisibleIndex="3"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 4" FieldName="Cenario4" VisibleIndex="4"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 5" FieldName="Cenario5" VisibleIndex="5"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 6" FieldName="Cenario6" VisibleIndex="6"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 7" FieldName="Cenario7" VisibleIndex="7"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 8" FieldName="Cenario8" VisibleIndex="8"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cen&#225;rio 9" FieldName="Cenario9" VisibleIndex="9"
                                                            Width="110px">
                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                            </PropertiesTextEdit>
                                                            <HeaderStyle HorizontalAlign="Right" Font-Size="12px" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles>
                                                        <TitlePanel CssClass="tituloGrid" HorizontalAlign="Left">
                                                            <Paddings Padding="0px" />
                                                        </TitlePanel>
                                                    </Styles>
                                                    <Settings ShowTitlePanel="True" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="90" VerticalScrollBarMode="Visible" />
                                                    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
