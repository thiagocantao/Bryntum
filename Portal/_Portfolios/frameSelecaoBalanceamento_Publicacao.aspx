<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_Publicacao.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_Publicacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var valorCheck = '';
        function atualizaDados() {
            gvProjetos.PerformCallback('Atualizar');
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 5px;
            width: 10px;
        }

        .style2 {
            width: 10px;
        }
    </style>
</head>
<body class="body" style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10px; height: 5px"></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" id="tbBotoes" runat="server"
                            style="width: 100%;">
                            <tr runat="server">
                                <td runat="server" style="padding: 2px" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
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
                                                        <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                    </td>
                    <td align="right">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 70px; display: none;" align="left">
                                    <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno" Width="65px">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="width: 140px" align="left">
                                    <dxe:ASPxComboBox ID="ddlCenario" runat="server" ClientInstanceName="ddlPortfolio"
                                        Width="135px" SelectedIndex="0">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvProjetos.PerformCallback('Atualizar');
}" />
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Cen&#225;rio 1" Value="1" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 2" Value="2" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 3" Value="3" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 4" Value="4" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 5" Value="5" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 6" Value="6" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 7" Value="7" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 8" Value="8" />
                                            <dxe:ListEditItem Text="Cen&#225;rio 9" Value="9" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="width: 100px; display: none;" align="left">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                        Text="Selecionar" AutoPostBack="False" Width="90px">
                                        <Paddings Padding="0px" />
                                        <ClientSideEvents Click="function(s, e) {
	gvProjetos.PerformCallback('Atualizar');
}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td align="left">
                                    <dxe:ASPxButton ID="btnPublicar" runat="server"
                                        Text="Publicar" OnClick="btnPublicar_Click" Width="90px">
                                        <Paddings Padding="0px" />
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = confirm(&quot;Ao fazer a publica&#231;&#227;o, somente os projetos do cen&#225;rio escolhido ser&#227;o acompanhados no portf&#243;lio. Deseja realmente fazer a publica&#231;&#227;o do portf&#243;lio?&quot;);
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" class="style2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="width: 10px"></td>
                    <td>
                        <div id="divGrid" style="visibility:hidden">
                        <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" Width="100%" OnCustomCallback="gvProjetos_CustomCallback" OnAfterPerformCallback="gvProjetos_AfterPerformCallback">
                            <ClientSideEvents 
                                Init="function(s, e) {
    var height = Math.max(0, document.documentElement.clientHeight) - 45;
    s.SetHeight(height);
    document.getElementById('divGrid').style.visibility = '';
}" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" />
                            <Settings ShowGroupPanel="False" VerticalScrollBarMode="Visible" ShowFooter="True" ShowFilterRow="True" HorizontalScrollBarMode="Visible" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="RK" FieldName="Ranking" VisibleIndex="0" Width="40px" FixedStyle="Left">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("Ranking") + "" != "" ? Eval("Ranking") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="Categoria" VisibleIndex="1"
                                    Width="270px" FixedStyle="Left">
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("Categoria") + "" != "" ? Eval("Categoria") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="DesempenhoProjeto" VisibleIndex="2"
                                    Width="40px" FixedStyle="Left" Name="DesempenhoProjeto">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' border='0' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="3"
                                    Width="270px" FixedStyle="Left">
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("NomeProjeto") + "" != "" ? Eval("NomeProjeto") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Import&#226;ncia" FieldName="ScoreCriterios"
                                    Name="pontuacao" VisibleIndex="4" Width="180px">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("ScoreCriterios") + "" != "" ? Eval("ScoreCriterios") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Complexidade" FieldName="ScoreRiscos" VisibleIndex="5"
                                    Width="195px">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("ScoreRiscos") + "" != "" ? Eval("ScoreRiscos") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Despesa (R$)" FieldName="Custo" VisibleIndex="6"
                                    Width="150px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("Custo") + "" != "" ? string.Format("{0:n0}", float.Parse(Eval("Custo").ToString())) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Receita (R$)" FieldName="Receita" VisibleIndex="7"
                                    Width="150px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("Receita") + "" != "" ? string.Format("{0:n0}", float.Parse(Eval("Receita").ToString())) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Recursos (h)" FieldName="RH" VisibleIndex="8" Width="150px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("RH") + "" != "" ? string.Format("{0:n0}", double.Parse(Eval("RH").ToString())) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusProjeto"
                                    VisibleIndex="9" Width="140px">
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("DescricaoStatusProjeto") + "" != "" ? Eval("DescricaoStatusProjeto").ToString() : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <FooterRow>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="background-color: #619340; border-radius: 50%; height: 16px; width: 16px;">&nbsp;</td>
                                            <td style="padding-left: 3px; padding-right: 10px; font-size: 12px; color: #575757;">
                                                <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server"
                                                    ClientInstanceName="lblDescricaoNaoAtiva" Font-Bold="False"
                                                    Text="<%$ Resources:traducao, frameSelecaoBalanceamento_Publicacao_projetos_n_o_associados_ao_portf_lio_ %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                       </div>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1"
                            runat="server" GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                            <Styles>
                                <Default>
                                </Default>
                                <Header>
                                </Header>
                                <Cell>
                                </Cell>
                                <GroupFooter Font-Bold="True">
                                </GroupFooter>
                                <Title Font-Bold="True"></Title>
                            </Styles>
                        </dxwgv:ASPxGridViewExporter>
                    </td>
                    <td class="style2">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
