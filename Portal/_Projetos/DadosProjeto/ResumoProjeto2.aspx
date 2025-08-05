<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResumoProjeto2.aspx.cs" Inherits="_Projetos_DadosProjeto_ResumoProjeto2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Portfólio - Dados do Projeto</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 5px;
        }

        .style4 {
            width: 100%;
        }

        .dxeBase {
            font: 12px Tahoma, Geneva, sans-serif;
        }

        .style10 {
            height: 10px;
        }

        .style11 {
            width: 10px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div style="width: 100%">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td align="center" valign="top" width="50%"></td>
                                            <td align="center" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" style="padding-right: 2px">
                                                <dxrp:ASPxRoundPanel ID="pDados" runat="server" BackColor="White"
                                                    HeaderText="Desempenho Físico do Projeto"
                                                    ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%"
                                                    EncodeHtml="False" ShowHeader="False" ContentHeight="60px">
                                                    <ContentPaddings Padding="1px" PaddingTop="2px" />
                                                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                    <HeaderStyle BackColor="White" Font-Bold="False" ForeColor="#404040">
                                                        <BorderLeft BorderStyle="None" />
                                                        <BorderRight BorderStyle="None" />
                                                        <BorderBottom BorderStyle="None" />
                                                    </HeaderStyle>
                                                    <HeaderContent>
                                                        <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1031923202/HeaderContent.png"
                                                            Repeat="RepeatX" VerticalPosition="bottom" />
                                                    </HeaderContent>
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server">
                                                            <table align="center" cellpadding="0" cellspacing="0"
                                                                style="width: 100%; height: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 25px">
                                                                                                <asp:Label ID="lblTituloUnidadeNegocio" runat="server" Text="UN:"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 85px">
                                                                                                <asp:Label ID="lblUnidadeNegocio" runat="server" EnableViewState="False">--</asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblTituloGer" runat="server" Text="Gerente:"></asp:Label>
                                                                                                            &nbsp;</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblGerente" runat="server" EnableViewState="False">--</asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td style="width: 140px; text-align: right; padding-right: 5px;"><strong>
                                                                                                <asp:Label ID="lblAtualizacao" runat="server" Font-Bold="False" Text="--/--/----"></asp:Label>
                                                                                            </strong></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="left" style="width: 40px">
                                                                                    <asp:Label ID="lblTituloInicio" runat="server" Text="In&#237;cio:"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 95px">
                                                                                    <asp:Label ID="lblInicio" runat="server" EnableViewState="False">--</asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 60px">
                                                                                    <asp:Label ID="lblTituloInicio0" runat="server" Text="Início LB:"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 80px">
                                                                                    <asp:Label ID="lblInicioLB" runat="server" EnableViewState="False">--</asp:Label>
                                                                                </td>
                                                                                <td align="left">&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style10"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="left" style="width: 55px"><strong>
                                                                                    <asp:Label ID="lblTituloTermino" runat="server" Font-Bold="False" Text="Término:"></asp:Label>
                                                                                </strong></td>
                                                                                <td align="left" style="width: 80px">
                                                                                    <asp:Label ID="lblTermino" runat="server" EnableViewState="False">--</asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 75px"><strong>
                                                                                    <asp:Label ID="lblTituloTermino0" runat="server" Font-Bold="False" Text="Término LB:"></asp:Label>
                                                                                </strong></td>
                                                                                <td align="left" style="width: 80px">
                                                                                    <asp:Label ID="lblTerminoLB" runat="server" EnableViewState="False">--</asp:Label>
                                                                                </td>
                                                                                <td align="left">&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="display: none; width: 411px;">
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 45px">
                                                                                    <asp:Label ID="lblTituloAtraso" runat="server" Text="Atraso:"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 85px">
                                                                                    <asp:Label ID="lblAtraso" runat="server" EnableViewState="False">---</asp:Label>
                                                                                </td>
                                                                                <td style="width: 99px">
                                                                                    <asp:Label ID="lblTituloCusto" runat="server" Text="Despesa ao Concl.:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCusto" runat="server" EnableViewState="False">----</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxrp:ASPxRoundPanel>
                                            </td>
                                            <td align="center" valign="top" style="padding-left: 2px">
                                                <iframe id='frm01' frameborder="0" scrolling="no" height="67" width="100%"
                                                    src="./graficos/<%=grafico1 %>.aspx?IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top"></td>
                                            <td align="center" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" style="padding-right: 2px">
                                                <iframe id='frm02' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                                                    style="width: 100%"
                                                    src="./graficos/<%=grafico2 %>.aspx?FRM=frm02&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                                            </td>
                                            <td align="center" valign="top" style="padding-left: 2px">
                                                <iframe id='frm03' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                                                    style="width: 100%"
                                                    src="./graficos/<%=grafico3 %>.aspx?FRM=frm03&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top"></td>
                                            <td align="center" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="bottom" style="padding-right: 2px">
                                                <iframe id='frm04' frameborder="0" scrolling="no" height="<%=alturaGraficos %>" style="width: 100%"
                                                    src="./graficos/<%=grafico4 %>.aspx?FRM=frm04&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                                            </td>
                                            <td align="center" style="padding-left: 2px">
                                                <iframe id='frm05' frameborder="0" scrolling="no" height="<%=alturaGraficos %>" style="width: 100%"
                                                    src="./graficos/<%=grafico5 %>.aspx?FRM=frm05&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style2"></td>
                </tr>
                <tr>
                    <td align="center" style="padding-left: 5px; padding-right: 5px">
                        <iframe id='frm06' frameborder="0" scrolling="no" height="1" width="100%"
                            src="<%=urlGrafico06 %>"></iframe>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="pcEventosAtlPrj" runat="server" ClientInstanceName="pcEventosAtlPrj"
                HeaderText="Atualizações"
                Width="800px" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table cellspacing="1" class="style4">
                            <tr>
                                <td class="style10">
                                    <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                        style="width: 100%">
                                        <tr runat="server">
                                            <td runat="server" style="padding: 3px" valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
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
                                    <dxwgv:ASPxGridView ID="gvEventosAtlPrj" runat="server"
                                        AutoGenerateColumns="False" ClientInstanceName="gvEventosAtlPrj"
                                        KeyFieldName="IDItemAtualizacao"
                                        Width="100%">
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Caption="ID Item" FieldName="IDItemAtualizacao"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Item"
                                                FieldName="DescricaoItemAtualizacao" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="115px">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Evento"
                                                FieldName="DescricaoUltimaAtualizacaoItem" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Data"
                                                FieldName="DataUltimaAtualizacaoItem" ShowInCustomizationForm="True"
                                                VisibleIndex="2" SortIndex="0" SortOrder="Descending" Width="140px">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Usuário"
                                                FieldName="IDUsuarioUltimaAtualizacao" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="200px">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="250" />
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="1" class="style4">
                                        <tr>
                                            <td align="right">&nbsp;</td>
                                            <td class="style11">&nbsp;</td>
                                            <td align="right">
                                                <dxe:ASPxButton ID="btnFecharEventosAtlPrj" runat="server"
                                                    Text="Fechar" Width="90px" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
	pcEventosAtlPrj.Hide();
}" />
                                                    <Paddings Padding="0px" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>

            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvEventosAtlPrj"
                ID="ASPxGridViewExporter1"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default></Default>

                    <Header></Header>

                    <Cell></Cell>

                    <GroupFooter Font-Bold="True"></GroupFooter>

                    <Title Font-Bold="True"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>

        </div>
    </form>
</body>
</html>
