<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resumoIndicador2.aspx.cs"
    Inherits="_Estrategias_indicador_resumoIndicador2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>

    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>

    <title>Untitled Page</title>

    <script type="text/javascript" language="javascript">
        function abreGrafico(tela) {
            document.getElementById('graficoIndicador').src = tela;
            periodoIndicador.location.reload();
        }
        function alteraUnidade(codigoUnidade) {
            pcUnidades.Hide();
            ddlUnidade.SetValue(codigoUnidade);
            callback.PerformCallback('A');
        }

    </script>

    <style type="text/css">
        .auto-style1 {
            height: 5px;
        }

        .Resize textarea {
            resize: both;
        }

        .titulo-tela,
        .titulo-tela > span {
            display: inline-block;
            font-size: 18px !important;
            padding-bottom: 10px;
            padding-left: 7px;
            padding-top: 5px;
        }
    </style>

</head>
<body class="body">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr style="height: 26px">
                    <td valign="middle">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="lblTitulo" runat="server" CssClass="titulo-tela" ClientInstanceName="lblTituloSelecao" Font-Bold="True"
                                        Text="Detalhes">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" style="width: 90px">
                                    <dxe:ASPxLabel ID="lblUnidade" runat="server"
                                        Text="Entidade:" ClientInstanceName="lblUnidade">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 110px">
                                    <dxe:ASPxComboBox ID="ddlUnidade" runat="server"
                                        Width="100%" ClientInstanceName="ddlUnidade" ValueType="System.Int32" IncrementalFilteringMode="Contains">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Sigla da Entidade" Width="150px" />
                                            <dxe:ListBoxColumn Caption="Nome da Entidade" Width="250px" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="right" style="width: 60px">
                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                        Text="Período:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 100px">
                                    <dxe:ASPxComboBox ID="ddlPeriodo" runat="server"
                                        Width="200px" ClientInstanceName="ddlPeriodo" EnableCallbackMode="True" ValueType="System.String">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="right" style="width: 95px; display: none;">
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                        Text="Desempenho:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 95px; display: none;">
                                    <dxe:ASPxComboBox ID="ddlDesempenho" runat="server" ClientInstanceName="ddlDesempenho"
                                        SelectedIndex="0" ValueType="System.String"
                                        Width="90px">
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Acumulado" Value="A" />
                                            <dxe:ListEditItem Text="Status" Value="S" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="center" style="width: 100px">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                        Text="Selecionar" AutoPostBack="False">
                                        <Paddings Padding="0px"></Paddings>

                                        <ClientSideEvents Click="function(s, e) {
	if(ddlUnidade.GetValue() == s.cp_UNL)
		parent.lblEntidadeDiferente.SetText(&quot;&quot;);
	else
		parent.lblEntidadeDiferente.SetText(traducao.resumoIndicador2__voc__est__visualizando_as_informa__es_da_unidade__ + ddlUnidade.GetText());
	callback.PerformCallback('A');
	e.processOnServer = false;
}"
                                            Init="function(s, e) {
	if(ddlUnidade.GetValue() == s.cp_UNL)
		parent.lblEntidadeDiferente.SetText(&quot;&quot;);
	else
		parent.lblEntidadeDiferente.SetText(traducao.resumoIndicador2__voc__est__visualizando_as_informa__es_da_unidade__ + ddlUnidade.GetText());
}"></ClientSideEvents>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientVisible="False" Cursor="pointer"
                            ImageUrl="~/imagens/novoAnexo.png" ToolTip="Mais detalhes...">
                            <ClientSideEvents Click="function(s, e) {
//	tbMaisDados
	mostrado=0;
	elem = document.getElementById('tbMaisDados');
	if(elem.style.display=='block') mostrado = 1;
	elem.style.display='none';
	if(mostrado!=1)elem.style.display = 'block';
}"></ClientSideEvents>
                        </dxe:ASPxImage>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 5px" valign="top"></td>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="display: none;">
                                            <td id="tdObjetivoMapa" runat="server">
                                                <table cellspacing="0" class="headerGrid">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                Text="Mapa Estratégico:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientEnabled="False" Width="100%">
                                                                <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                Text="Objetivo Estratégico:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientEnabled="False"
                                                                Width="100%">
                                                                <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                    Text="Indicador:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" style="width: 99%">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False" Width="99%">
                                                                <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <span id="spanGlossario" style="width: 10px">
                                                                <dxe:ASPxImage ID="imgGlossario" runat="server" Cursor="pointer"
                                                                    ImageUrl="~/imagens/mapaEstrategico/imgGlossarioIndicador.png" ToolTip="Glossário do Indicador"
                                                                    ClientInstanceName="imgGlossario">
                                                                </dxe:ASPxImage>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table id="tbMaisDados" border="0" cellpadding="0" cellspacing="0" width="99%">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                            Text="Responsável:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtResponsavel" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td style="width: 117px">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                                        Text="Unidade de Medida:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                        Text="Polaridade:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 15px"></td>
                                                                                <td style="width: 85px">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                        Text="Periodicidade:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px; width: 117px;">
                                                                                    <dxe:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientEnabled="False" Width="100%">
                                                                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td style="padding-right: 1px">
                                                                                    <dxe:ASPxTextBox ID="txtPolaridade" runat="server" ClientEnabled="False" Width="100%">
                                                                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td style="padding-right: 5px; width: 15px" valign="top">
                                                                                    <dxe:ASPxImage ID="imgPolaridade" runat="server" ClientInstanceName="imgPolaridadNeg">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                                <td style="width: 85px">
                                                                                    <dxe:ASPxTextBox ID="txtPeriodicidade" runat="server" ClientEnabled="False" Width="100%">
                                                                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                            Text="Fórmula:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="memoFormula" runat="server" ClientEnabled="False" Rows="2" Width="100%">
                                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 415px" valign="top">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <iframe id="graficoIndicador" src="<%=graficoInicial %>" width="100%" height="<%=alturaGrafico %>"
                                                    scrolling="no" frameborder="0" name="graficoIndicador"></iframe>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px; padding-left: 5px">
                        <iframe id="periodoIndicador" src="<%=telaPeriodo %>" width="100%"
                            scrolling="no" frameborder="0" name="periodoIndicador"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	abreGrafico(s.cp_TelaGrafico);
	window.parent.atualizaMenu();// = window.parent.indicador_menu.src;
}" />
        </dxcb:ASPxCallback>
        <dxpc:ASPxPopupControl ID="pcUnidades" runat="server" ClientInstanceName="pcUnidades"
            HeaderText="Selecione a Unidade" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <dxwgv:ASPxGridView ID="gvUnidades" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvUnidades"
                        KeyFieldName="CodigoUnidadeNegocio" OnCustomCallback="gvUnidades_CustomCallback"
                        Width="780px">
                        <ClientSideEvents EndCallback="function(s, e) {
	pcUnidades.Show();
}" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" VisibleIndex="0"
                                Width="13%">
                                <DataItemTemplate>
                                    <%# "<a href='#' onclick='alteraUnidade(" + Eval("CodigoUnidadeNegocio") + ")' style='cursor: pointer'>" + Eval("SiglaUnidadeNegocio") + "</a>"%>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeUnidadeNegocio" VisibleIndex="1"
                                Width="54%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Meta" FieldName="Meta" VisibleIndex="2" Width="14%">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Resultado" FieldName="Realizado" VisibleIndex="3"
                                Width="14%">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CorUF" VisibleIndex="4" Width="5%">
                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' /&gt;">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" />
                        <SettingsLoadingPanel ImagePosition="Top" />
                        <Images SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css">
                            <LoadingPanelOnStatusBar Url="~/App_Themes/Aqua/GridView/gvLoadingOnStatusBar.gif">
                            </LoadingPanelOnStatusBar>
                            <LoadingPanel Url="~/App_Themes/Aqua/GridView/Loading.gif">
                            </LoadingPanel>
                        </Images>
                        <ImagesEditors>
                            <DropDownEditDropDown>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                            </DropDownEditDropDown>
                            <SpinEditIncrement>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                    PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                            </SpinEditIncrement>
                            <SpinEditDecrement>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                    PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                            </SpinEditDecrement>
                            <SpinEditLargeIncrement>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                    PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                            </SpinEditLargeIncrement>
                            <SpinEditLargeDecrement>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                    PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                            </SpinEditLargeDecrement>
                        </ImagesEditors>
                        <ImagesFilterControl>
                            <LoadingPanel Url="~/App_Themes/Aqua/Editors/Loading.gif">
                            </LoadingPanel>
                        </ImagesFilterControl>
                        <Styles>
                            <LoadingPanel ImageSpacing="8px">
                            </LoadingPanel>
                        </Styles>
                        <StylesEditors>
                            <CalendarHeader Spacing="1px">
                            </CalendarHeader>
                            <ProgressBar Height="25px">
                            </ProgressBar>
                        </StylesEditors>
                    </dxwgv:ASPxGridView>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle HorizontalAlign="Left" />
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="pcGlossarioIndicador" runat="server"
            ClientInstanceName="pcGlossarioIndicador" AllowDragging="True"
            AllowResize="True" CloseAction="CloseButton" HeaderText="Glossário do Indicador"
            PopupElementID="spanGlossario" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <dxhe:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" ActiveView="Preview">
                        <Settings AllowDesignView="False" AllowHtmlView="False" />
                    </dxhe:ASPxHtmlEditor>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
<%
    if (!IsPostBack)
    {
%>
<script type="text/javascript">
    window.parent.atualizaMenu();
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 275;
    document.getElementById("periodoIndicador").style.height = sHeight + "px";
</script>
<%
    }
%>
</html>
