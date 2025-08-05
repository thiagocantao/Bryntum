<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_Resumo.aspx.cs"
    Inherits="_Portfolios_frameSelecaoBalanceamento_Resumo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var valorCheck = '';
        function atualizaDados() {
            gvProjetos.PerformCallback('Atualizar');
        }

        function abreMatrizCategoria(codigoCategoria) {
            window.top.showModalComFooter(window.top.pcModalComFooter.cp_Path + '_Portfolios/Administracao/matrizCategoria.aspx?PopUp=S&CC=' + codigoCategoria + '&alt=875&larg=1020', "Matriz", null, null, funcaoPosModal, null);
        }

        function funcaoPosModal() {
            gvProjetos.PerformCallback('Atualizar');
        }

        function abreTelaCriterios(codigoProjeto) {
            window.top.showModalComFooter(window.top.pcModalComFooter.cp_Path + '_Portfolios/frameProposta_Criterios.aspx?PopUp=S&CP=' + codigoProjeto, "", null, null, funcaoPosModal, null);
        }

        function abreTelaFluxoCaixa(codigoProjeto, custoOuReceita) {
            window.top.showModal(window.top.pcModal.cp_Path + '_Portfolios/frameProposta_FluxoCaixa.aspx?PopUp=S&AT=220&RO=S&CP=' + codigoProjeto + '&ES=' + custoOuReceita, "", 1020, 430, "", null);
        }

        function abreTelaRecursos(codigoProjeto) {
            window.top.showModal(window.top.pcModal.cp_Path + '_Portfolios/frameProposta_RH.aspx?PopUp=S&RO=S&CP=' + codigoProjeto, "", 1030, 550, "", null);
        }

        function abreTelaDemandaRelacionada(codigoProjeto) {
            window.top.showModal(window.top.pcModal.cp_Path + '_Portfolios/frameProposta_DemandaRelacionada.aspx?PopUp=S&RO=S&CP=' + codigoProjeto, "", 1000, 430, "", null);
        }

        function OnContextMenu(s, e) {
            if (e.objectType == 'header') {
                colName = s.GetColumn(e.index).fieldName;
                headerMenu.GetItemByName('HideColumn').SetEnabled((colName == null || colName == 'CodigoDemanda' ? false : true));
                headerMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
            }
        }

        function OnItemClick(s, e) {
            if (e.item.name == 'HideColumn') {
                gvProjetos.PerformCallback(colName);
                colName = null;
            }
            else {
                if (gvProjetos.IsCustomizationWindowVisible())
                    gvProjetos.HideCustomizationWindow();
                else
                    gvProjetos.ShowCustomizationWindow();
            }
        }

        function grid_ContextMenu(s, e) {
            if (e.objectType == "header")
                pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
        }

        function SalvarConfiguracoesLayout() {
            callback.PerformCallback("save_layout");
        }

        function RestaurarConfiguracoesLayout() {
            var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
            window.top.mostraConfirmacao('Deseja restaurar as configurações originais do layout da consulta?', function () { funcObj['funcaoClickOK']() }, null);
        }

        function clickCB(param) {
            gvProjetos.PerformCallback(param);
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 90px;
        }

        .style2 {
            width: 70px;
        }

        .style4 {
            width: 70px;
            height: 21px;
        }

        .style5 {
            width: 90px;
        }

        .style6 {
            width: 10px;
            height: 5px;
        }

        .style7 {
            height: 5px;
        }

        .style8 {
            height: 5px;
        }

        .style9 {
            width: 10px;
        }
    </style>
</head>
<body class="body" style="margin: 0">
    <form id="form1" runat="server">
    
            <table style="width: 100%">

                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" id="tbBotoes" style="width: 100%" runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxm:ASPxMenu ID="menu0" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                    ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                                <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                <td></td>
                                <td>
                                    <table align="right">
                                        <tr>
                                            <td align="left">&nbsp;
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                    Text="Ano:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="style4">
                                                <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno"
                                                    Width="90px">
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td class="style5">
                                                <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                                    Text="Selecionar" AutoPostBack="False" Width="100%">
                                                    <Paddings Padding="0px" />
                                                    <ClientSideEvents Click="function(s, e) {
	gvProjetos.PerformCallback('Atualizar');
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>


                                </td>


                            </tr>
                        </table>
                    </td>

                </tr>

                <tr>

                    <td>
                         <div id="divGrid" style="visibility:hidden">
                        <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnCustomCallback="gvProjetos_CustomCallback" OnAfterPerformCallback="gvProjetos_AfterPerformCallback"
                            ClientInstanceName="gvProjetos">
                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" CustomizationWindowCaption="Adicionar/Remover Colunas"></SettingsText>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="RK" FieldName="Ranking" VisibleIndex="1" Width="30px"
                                    FixedStyle="Left">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("Ranking") + "" != "" ? Eval("Ranking") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="Categoria" VisibleIndex="2"
                                    Width="250px" FixedStyle="Left">
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    <DataItemTemplate>
                                       <%# constroiLinkAbreCategoria( Eval("IndicaItalico").ToString() ,  Eval("Categoria").ToString(), Eval("_CodigoCategoria").ToString() )%>
                                     </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="DesempenhoProjeto" VisibleIndex="3" Width="40px"
                                    FixedStyle="Left" Name="col_DesempenhoProjeto" ToolTip="Desempenho" Caption=" ">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' border='0' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" Name="NomeProjeto"
                                    VisibleIndex="4" Width="500px" FixedStyle="Left">
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("NomeProjeto") + "" != "" ? ((Eval("CodigoStatusProjeto").ToString() == "3" || Eval("CodigoStatusProjeto").ToString() == "6") ? ("<a target='_top' href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + Eval("_CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "'>" + Eval("NomeProjeto") + "</a>") : Eval("NomeProjeto")) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <Settings AutoFilterCondition="Contains" SortMode="Value" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Dependência" FieldName="Dependencia"
                                    Name="Dependencia" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("Dependencia") + "" != "" && Eval("Dependencia").ToString() == "SIM" ? ("<a href='#' style='color:" + ((Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000") + "' onclick='abreTelaDemandaRelacionada(" + Eval("_CodigoProjeto") + ");' >" + Eval("Dependencia") + "</a>") : Eval("Dependencia")%></span>
                                    </DataItemTemplate>
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" SortMode="Value" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Import&#226;ncia" FieldName="ScoreCriterios"
                                    Name="pontuacao" VisibleIndex="6" Width="250px">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("ScoreCriterios") + "" != "" ? ("<a href='#' style='color:" + ((Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000") + "' onclick='abreTelaCriterios(" + Eval("_CodigoProjeto") + ");' >" + Eval("ScoreCriterios") + "</a>") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Complexidade" FieldName="ScoreRiscos" VisibleIndex="7"
                                    Width="150px">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("ScoreRiscos") + "" != "" ? ("<a href='#' style='color:" + ((Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000") + "' onclick='abreTelaCriterios(" + Eval("_CodigoProjeto") + ");' >" + Eval("ScoreRiscos") + "</a>") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Despesa (R$)" FieldName="Custo" VisibleIndex="8"
                                    Width="110px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("Custo") + "" != "" ? ( string.Format("{0:n0}", double.Parse(Eval("Custo").ToString()))) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Receita (R$)" FieldName="Receita" VisibleIndex="9"
                                    Width="110px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("Receita") + "" != "" ? ( string.Format("{0:n0}", double.Parse(Eval("Receita").ToString()))) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Recursos (h)" FieldName="RH" VisibleIndex="10"
                                    Width="90px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n0}" EncodeHtml="False">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("RH") + "" != "" ? ( string.Format("{0:n0}", double.Parse(Eval("RH").ToString()))) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusProjeto"
                                    VisibleIndex="11" Width="130px">
                                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("DescricaoStatusProjeto") + "" != "" ? Eval("DescricaoStatusProjeto").ToString() : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 1" VisibleIndex="12" FieldName="IndicaCenario1"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario1" ToolTip="<%$ Resources:traducao, cen_rio_1 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check1", Eval("IndicaCenario1") + "", "1;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 2" VisibleIndex="13" FieldName="IndicaCenario2"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario2" ToolTip="<%$ Resources:traducao, cen_rio_2 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check2", Eval("IndicaCenario2") + "", "2;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 3" VisibleIndex="14" FieldName="IndicaCenario3"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario3" ToolTip="<%$ Resources:traducao, cen_rio_3 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check3", Eval("IndicaCenario3") + "", "3;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 4" VisibleIndex="15" FieldName="IndicaCenario4"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario4" ToolTip="<%$ Resources:traducao, cen_rio_4 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check4", Eval("IndicaCenario4") + "", "4;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 5" VisibleIndex="16" FieldName="IndicaCenario5"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario5" ToolTip="<%$ Resources:traducao, cen_rio_5 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check5", Eval("IndicaCenario5") + "", "5;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 6" VisibleIndex="17" FieldName="IndicaCenario6"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario6" ToolTip="<%$ Resources:traducao, cen_rio_6 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check6", Eval("IndicaCenario6") + "", "6;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 7" VisibleIndex="18" FieldName="IndicaCenario7"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario7" ToolTip="<%$ Resources:traducao, cen_rio_7 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check7", Eval("IndicaCenario7") + "", "7;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 8" VisibleIndex="19" FieldName="IndicaCenario8"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario8" ToolTip="<%$ Resources:traducao, cen_rio_8 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check8", Eval("IndicaCenario8") + "", "8;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Cen. 9" VisibleIndex="20" FieldName="IndicaCenario9"
                                    Width="35px" ExportWidth="10" Name="IndicaCenario9" ToolTip="<%$ Resources:traducao, cen_rio_9 %>">
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <%# getCenarioMarcado("check9", Eval("IndicaCenario9") + "", "9;", Eval("_CodigoProjeto") + "")%>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_CodigoCategoria" VisibleIndex="21" Visible="False"
                                    ShowInCustomizationForm="False">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_CodigoProjeto" VisibleIndex="22" Visible="False"
                                    ShowInCustomizationForm="False">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="% Concluído" FieldName="PercentualConcluido"
                                    Visible="False" VisibleIndex="23" Width="110px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <DataItemTemplate>
                                        <span style='color: <%# (Eval("IndicaItalico") + "" != "" && Eval("IndicaItalico").ToString() == "S") ? "#339900" : "#000000" %>'>
                                            <%# Eval("PercentualConcluido") + "" != "" ? string.Format("{0:n2}%", double.Parse(Eval("PercentualConcluido").ToString())) : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Gerente do Projeto" FieldName="NomeGerenteProjeto"
                                    Visible="False" VisibleIndex="24" Width="200px">
                                    <Settings AutoFilterCondition="Contains" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico")+ "" == "S" ? "#339900" : "#000000" %>'>
                                            <%# Eval("NomeGerenteProjeto") + "" != "" ? Eval("NomeGerenteProjeto") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Unidade de Negócio" Visible="False" VisibleIndex="25"
                                    FieldName="NomeUnidadeNegocio" Width="250px">
                                    <Settings AutoFilterCondition="Contains" />
                                    <DataItemTemplate>
                                        <span style='color: <%# Eval("IndicaItalico")+"" == "S" ? "#339900" : "#000000" %>'>
                                            <%# Eval("NomeUnidadeNegocio") + "" != "" ? Eval("NomeUnidadeNegocio") : "&nbsp;"%></span>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords" PageSize="9999999">
                            </SettingsPager>
                            <Settings ShowGroupPanel="False" VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                HorizontalScrollBarMode="Visible" ShowFooter="True"></Settings>
                            <ClientSideEvents Init="function(s, e) {
	    var height = Math.max(0, document.documentElement.clientHeight) - 60;
    s.SetHeight(height);
                                document.getElementById('divGrid').style.visibility = '';
}"
                                ContextMenu="OnContextMenu" />
                            <SettingsBehavior EnableCustomizationWindow="true" />
                            <SettingsPopup>
                                <CustomizationWindow Height="200px" Width="200px" HorizontalAlign="NotSet" VerticalAlign="NotSet" />
                            </SettingsPopup>
                            <SettingsCookies StoreColumnsVisiblePosition="False" />
                            <Templates>
                                <EmptyDataRow>
                                    <table>
                                        <tr>
                                            <td style="width: 10px; background-color: #339900; border: 1px solid #808080">&nbsp;
                                            </td>
                                            <td style="padding-left: 3px; padding-right: 10px; font-size: 12px; color: #575757;">
                                                <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                    Font-Bold="False" Text="<%$ Resources:traducao, projetos_n_o_associados_ao_portf_lio_ %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataRow>
                                <FooterRow>
                                    <table width="100%">
                                        <tr>
                                            <td style="background-color: #619340; border-radius: 50%; height: 16px; width: 16px;">&nbsp;
                                            </td>
                                            <td style="padding-left: 3px; padding-right: 10px">
                                                <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                    Font-Bold="False" Text="<%$ Resources:traducao, projetos_n_o_associados_ao_portf_lio_ %>">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                </div>
                    </td>
                    <td class="style9"></td>
                </tr>
            </table>

        <dxm:ASPxPopupMenu ID="headerMenu" runat="server" ClientInstanceName="headerMenu">
            <Items>
                <dxm:MenuItem Text="Ocultar Coluna" Name="HideColumn">
                </dxm:MenuItem>
                <dxm:MenuItem Text="Mostrar/Ocultar Colunas Disponíveis" Name="ShowHideList">
                </dxm:MenuItem>
            </Items>
            <ClientSideEvents ItemClick="OnItemClick" />
        </dxm:ASPxPopupMenu>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
    </form>
</body>
</html>
