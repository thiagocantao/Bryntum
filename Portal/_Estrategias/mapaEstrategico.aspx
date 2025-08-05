<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="mapaEstrategico.aspx.cs" Inherits="_Estrategias_mapaEstrategico" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" EnableViewState="false" ContentPlaceHolderID="AreaTrabalho"
    runat="Server">
    <script type="text/javascript" language="JavaScript">
        var refreshinterval = 600;
        var starttime;
        var nowtime;
        var reloadseconds = 0;
        var secondssinceloaded = 0;

        function starttime() {
            starttime = new Date();
            starttime = starttime.getTime();
            countdown();
        }

        function countdown() {
            nowtime = new Date();
            nowtime = nowtime.getTime();
            secondssinceloaded = (nowtime - starttime) / 1000;
            reloadseconds = Math.round(refreshinterval - secondssinceloaded);
            if (refreshinterval >= secondssinceloaded) {
                var timer = setTimeout("countdown()", 1000);

            }
            else {
                clearTimeout(timer);
                window.location.reload(true);
            }
        }

        window.onload = starttime;
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="height: 26px">
            <td align="center" style="width: 40px" valign="middle">
                <dxe:ASPxImage ID="imgReuniao" runat="server" ImageUrl="~/imagens/reuniao.png" ToolTip="<%$ Resources:traducao, mapaEstrategico_reuni_es_de_an_lise_estrat_gica_do_mapa_selecionado %>"
                    ClientInstanceName="imgReuniao">
                    <ClientSideEvents Click="function(s, e) {
	window.top.gotoURL('Reunioes/reuniaoTecnicaPlanejamento.aspx?TipoReuniao=E&amp;MOD=EST&amp;IOB=ME&amp;COB=' + ddlMapa.GetValue(), '_self');
}"></ClientSideEvents>
                </dxe:ASPxImage>
            </td>
            <td valign="middle">
                <dxe:ASPxLabel ID="lblTituloTela" ClientInstanceName="lblTituloTela" runat="server"
                    Font-Bold="True" Font-Overline="False" Font-Strikeout="False">
                </dxe:ASPxLabel>
                <dxe:ASPxButton ID="btnTempXmlMapa" runat="server" ClientInstanceName="btnTempXmlMapa"
                    Text="Mapa -> XML" Visible="False" Width="98px">
                    <Paddings Padding="0px" />
                </dxe:ASPxButton>
            </td>
            <td align="right" style="width: 85px; padding-right: 5px">
                <dxe:ASPxLabel ID="lblVisualizacao" runat="server" ClientInstanceName="lblVisualizacao"
                    Text="Visualização:">
                </dxe:ASPxLabel>
            </td>
            <td align="right" style="width: 160px">
                <dxe:ASPxComboBox ID="ddlVisualizacao" runat="server" ClientInstanceName="ddlVisualizacao"
                    SelectedIndex="0" Width="100%">
                    <Items>
                        <dxe:ListEditItem Text="Mapa Iluminado" Value="MI" Selected="True"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="Em &#193;rvore" Value="EA"></dxe:ListEditItem>
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	OcultarFilas(s);
                    }"
                        Init="function(s, e) {
	OcultarFilas(s);
}"></ClientSideEvents>
                </dxe:ASPxComboBox>
            </td>
            <td align="right" style="width: 10px"></td>
            <td align="right" style="width: 115px; padding-right: 5px" id="tdMapa01">
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                    Text="Mapa Estratégico:">
                </dxe:ASPxLabel>
            </td>
            <td align="right" style="width: 320px" id="tdMapa02">
                <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa"
                    Width="320px" ValueType="System.Int32" ImageUrlField="imagemMapaUnidade"
                    ShowImageInEditBox="True" AutoPostBack="True">
                </dxe:ASPxComboBox>
            </td>
            <td id="tdAjuda" align="left" style="width: 25px">&nbsp;
                <dxcp:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" ImageUrl="~/imagens/ajuda.png"
                    ToolTip="<%$ Resources:traducao, legenda_da_lista_de_mapas%>">
                    <ClientSideEvents Click="function(s, e) {
	popUpAjuda.Show();
}" />
                </dxcp:ASPxImage>
            </td>
            <td align="right" style="width: 8px; display: none;">
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                    Text="Desempenho:">
                </dxe:ASPxLabel>
            </td>
            <td align="left" style="width: 8px; display: none;">
                <dxe:ASPxComboBox ID="ddlDesempenho" runat="server" ClientInstanceName="ddlDesempenho"
                    Width="90px" SelectedIndex="0" ValueType="System.String">
                    <Items>
                        <dxe:ListEditItem Selected="True" Text="Acumulado" Value="A" />
                        <dxe:ListEditItem Text="Status" Value="S" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>
            <td align="left" style="width: 8px; display: none;"></td>
            <td align="right" style="width: 8px; display: none;">
                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                    Text="Ano:">
                </dxe:ASPxLabel>
            </td>
            <td align="left" style="width: 8px; display: none;">
                <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno"
                    ValueType="System.String" Width="60px">
                    <ClientSideEvents EndCallback="function(s, e) {
	ddlMes.PerformCallback();
}"
                        SelectedIndexChanged="function(s, e) {
	ddlMes.PerformCallback();
}" />
                </dxe:ASPxComboBox>
            </td>
            <td align="left" style="width: 8px; display: none;"></td>
            <td align="right" style="width: 8px; display: none;">
                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                    Text="Mês:">
                </dxe:ASPxLabel>
            </td>
            <td align="left" style="width: 8px; display: none;">
                <dxe:ASPxComboBox ID="ddlMes" runat="server" ClientInstanceName="ddlMes" EnableCallbackMode="True"
                    OnCallback="ddlMes_Callback" ValueType="System.Int32"
                    Width="50px">
                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Mes);
}" />
                </dxe:ASPxComboBox>
            </td>
            <td align="left" style="width: 8px; display: none;"></td>
            <td style="width: 5px"></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 802px; height: 3px;"></td>
            <td style="width: 2px; height: 3px;"></td>
            <td valign="top" style="height: 3px"></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td id="tdEmArvore" style="padding-right: 10px; padding-left: 10px; display: none;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="padding-top: 10px;">
                            <table runat="server" id="tbBotoesEdicao" cellpadding="0" cellspacing="0" width="100%">
                                <tr runat="server">
                                    <td runat="server" style="padding: 3px;" valign="middle">
                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                            <dxwtl:ASPxTreeList EnableViewState="False" ID="tlMapaEstrategico" runat="server"
                                AutoGenerateColumns="False" ClientInstanceName="tlMapaEstrategico"
                                KeyFieldName="Codigo" ParentFieldName="CodigoPai" Width="100%">
                                <Settings VerticalScrollBarMode="Visible" />
                                <Styles>
                                    <Cell VerticalAlign="Middle">
                                    </Cell>
                                </Styles>
                                <Columns>
                                    <dxwtl:TreeListTextColumn FieldName="Descricao" Name="Descricao" Width="90%" Caption="Descri&#231;&#227;o"
                                        VisibleIndex="0">
                                        <DataCellTemplate>
                                            <%# (("OBJ" == Eval("TipoObjeto").ToString()) ?
              "<table><tr><td><img src='../imagens/mapaEstrategico/Objetivo.png' /></td><td>" + getLinkIndicadorObjetivo(Eval("TipoObjeto").ToString(), Eval("Codigo").ToString(), Eval("CodigoPai").ToString(), Eval("Descricao").ToString(), (bool)Eval("Permissao")) + "</td></table>"
         :
       ("TEM" == Eval("TipoObjeto").ToString()) ?
                     "<table><tr><td><img src='../imagens/mapaEstrategico/TemaCombo.png' /></td><td>" + getLinkIndicadorObjetivo(Eval("TipoObjeto").ToString(), Eval("Codigo").ToString(), Eval("CodigoPai").ToString(), Eval("Descricao").ToString(), (bool)Eval("Permissao")) + "</td></table>"
         :
         ("IND" == Eval("TipoObjeto").ToString()) ?
                                "<table><tr><td><img src='../imagens/mapaEstrategico/indicador.png' /></td><td>" + getLinkIndicadorObjetivo(Eval("TipoObjeto").ToString(), Eval("Codigo").ToString(), Eval("CodigoPai").ToString(), Eval("Descricao").ToString(), (bool)Eval("Permissao")) + "</td></table>"
            :
              ("INI" == Eval("TipoObjeto").ToString()) ?
                                          "<table><tr><td><img src='../imagens/mapaEstrategico/projeto.PNG' /></td><td>" + getLinkIndicadorObjetivo(Eval("TipoObjeto").ToString(), Eval("Codigo").ToString(), Eval("CodigoPai").ToString(), Eval("Descricao").ToString(), (bool)Eval("Permissao")) + "</td></table>"
                 :
                         getLinkIndicadorObjetivo(Eval("TipoObjeto").ToString(), Eval("Codigo").ToString(), Eval("CodigoPai").ToString(), Eval("Descricao").ToString(), (bool)Eval("Permissao")))%>
                                        </DataCellTemplate>
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="Cor" Width="5%" Caption="Status" VisibleIndex="1">
                                        <DataCellTemplate>
                                            <%# Eval("Cor").ToString() != "" ? "<img src='../imagens/" + Eval("Cor").ToString().Trim() + ".gif' />" : "" %>
                                        </DataCellTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwtl:TreeListTextColumn>
                                </Columns>
                            </dxwtl:ASPxTreeList>
                            <span id="spanLegenda" runat="server"></span>
                            <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server" OnRenderBrick="ASPxTreeListExporter1_RenderBrick">
                                <Styles>
                                    <Default>
                                    </Default>
                                    <Header Font-Bold="True">
                                    </Header>
                                    <Cell HorizontalAlign="Left">
                                    </Cell>
                                </Styles>
                            </dxwtle:ASPxTreeListExporter>
                        </td>
                    </tr>
                    <tr>
                        <td class="<%=estiloFooter %>" style="padding: 3px">
                            <table cellspacing="0" cellpadding="0" class="grid-legendas">
                                <tbody>
                                    <tr>
                                        <td>
                                            <img alt="" src="../imagens/azulMenor.gif" height="16" />
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <span>
                                                <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_meta_superada %>"
                                                    ID="Label2" EnableViewState="False"></asp:Label>
                                            </span>
                                        </td>
                                        <td>
                                            <img alt="" src="../imagens/verdeMenor.gif" height="16" />
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <span>
                                                <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_satisfat_rio %>"
                                                    ID="Label10" EnableViewState="False"></asp:Label>
                                            </span>
                                        </td>
                                        <td>
                                            <img alt="" src="../imagens/amareloMenor.gif" height="16" />
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <span>
                                                <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_aten__o %>"
                                                    ID="Label11" EnableViewState="False"></asp:Label>
                                            </span>
                                        </td>
                                        <td>
                                            <img alt="" src="../imagens/vermelhoMenor.gif" height="16" />
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <span style="">
                                                <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_cr_tico %>"
                                                    ID="Label12" EnableViewState="False"></asp:Label>
                                            </span>
                                        </td>
                                        <td>
                                            <img style="margin-top: 0px" alt="" src="../imagens/BrancoMenor.gif" height="16" />
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_sem_informa__o %>"
                                                ID="Label13" EnableViewState="False"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/Objetivo.png" ID="Image1"
                                                EnableViewState="False" Height="16px"></asp:Image>
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_objetivos %>" ID="Label14"
                                                EnableViewState="False"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/indicador.png" ID="Image4"
                                                EnableViewState="False" Height="16px"></asp:Image>
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_indicador %>" ID="Label15" EnableViewState="False"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Image runat="server" ImageUrl="~/imagens/projeto.png" ID="Image3"
                                                EnableViewState="False" Height="16px"></asp:Image>
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_projeto %>" ID="Label3" EnableViewState="False"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/TemaCombo.png" ID="Image2"
                                                EnableViewState="False" Height="16px"></asp:Image>
                                        </td>
                                        <td style="padding-left: 3px; padding-right: 10px;">
                                            <asp:Label runat="server" Text="<%$ Resources:traducao, mapaEstrategico_tema %>" ID="Label1" EnableViewState="False"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <!-- TD FLASH VIEW MAPA ESTRATÉGICO -->

            <td id="tdMapaFlashView" align="center" valign="top">
                <asp:Panel ID="pnMapaFlash" runat="server">
                    <div style="height: <%= alturaDivMapa %>px; overflow: auto; position: relative">
                        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                            <dxtv:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/img-no-image-n.png" EnableTheming="False">
                            </dxtv:ASPxImage>
                        </div>
                    </div>
                </asp:Panel>
            </td>

            <!-- FIM TD FLASH VIEW MAPA ESTRATÉGICO -->
        </tr>
        <tr>
            <td id="tdMapaCarregado">
                <iframe name="frameMapaCarregado" id="frameMapaCarregado" style="width: 100%; border-style: none;"></iframe>
            </td>
        </tr>        
        <tr>
            <td valign="bottom">
                <dxpc:ASPxPopupControl ID="popUpAjuda" runat="server" ClientInstanceName="popUpAjuda"
                    Font-Bold="True" Font-Italic="False" HeaderText="Legenda"
                    PopupElementID="tdAjuda" Width="388px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 25px">
                                                        <img alt="" src="../imagens/MapaEntidade.png" />
                                                    </td>
                                                    <td style="width: 140px">
                                                        <span style="">
                                                            <asp:Label ID="Label8" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, mapas_da_entidade_atual %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td style="width: 25px">
                                                        <img alt="" src="../imagens/MapaOutraEntidade.png" />
                                                    </td>
                                                    <td style="width: 140px">
                                                        <span style="">
                                                            <asp:Label ID="Label9" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, mapas_de_outras_entidades %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <ContentStyle>
                        <Paddings Padding="2px" PaddingLeft="5px" PaddingTop="5px" />
                    </ContentStyle>
                </dxpc:ASPxPopupControl>
                <dxe:ASPxLabel ID="lblEntidadeDiferente" runat="server" ClientInstanceName="lblEntidadeDiferente"
                    Font-Bold="True" Font-Italic="True" ForeColor="Red">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
</asp:Content>
