<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResumoProjeto.aspx.cs" Inherits="_Projetos_DadosProjeto_ResumoProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
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

        .auto-style2 {
            height: 4px;
        }

        .auto-style3 {
            height: 2px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div id="divStatusProjeto" style="display: flex; flex-direction: column; flex-wrap: nowrap; width: 100%; overflow: auto">
            <div style="display: flex; flex-direction: row; width: 100%">
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <table align="center" cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #CCCCCC; height: 80px; width: 100%;">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 2px; padding-left: 3px;">
                                                        <dxcp:ASPxLabel ID="lblTituloUnidadeNegocio" runat="server" Text="UN:">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                    <td style="padding-right: 15px;">
                                                        <dxcp:ASPxLabel ID="lblUnidadeNegocio" runat="server" Text="--">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxcp:ASPxLabel ID="lblTituloGer" runat="server" Text="Gerente:">
                                                                    </dxcp:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxcp:ASPxLabel ID="lblGerente" runat="server" Text="--">
                                                                    </dxcp:ASPxLabel>
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
                        </tr>
                        <tr>
                            <td align="left">
                                <table>
                                    <tr>
                                        <td align="left" style="padding-right: 2px; padding-left: 3px;">
                                            <dxcp:ASPxLabel ID="lblTituloInicio" runat="server" Text="Início:">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td align="left" style="padding-right: 15px;">
                                            <dxcp:ASPxLabel ID="lblInicio" runat="server" Text="--">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <dxcp:ASPxLabel ID="lblTituloTermino" runat="server" Text="Término:">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td align="left" style="padding-right: 15px;">
                                            <dxcp:ASPxLabel ID="lblTermino" runat="server" Text="--">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <dxcp:ASPxLabel ID="lblAtualizacao" runat="server" Text="--/--/----">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr runat="server" id="trRanking">
                            <td align="left">
                                <table>
                                    <tr>
                                        <td style="padding-right: 2px">
                                            <dxcp:ASPxLabel ID="lblTituloRanking" runat="server" Text="Ranking:">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td style="padding-right: 10px">
                                            <dxcp:ASPxLabel ID="lblRanking" runat="server" Text="--">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td style="padding-right: 2px">
                                            <dxcp:ASPxLabel ID="lblTituloCarteira" runat="server" Text="Carteira Principal:">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel ID="lblCarteira" runat="server" Text="--">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="display: flex; flex-grow: 1">
                    <iframe id='frm01' frameborder="0" scrolling="no" height="90px" width="100%"
                        src="./graficos/<%=grafico1 %>.aspx?IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                </div>
            </div>
            <div style="display: flex; flex-direction: row">
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <iframe id='frm02' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                        style="width: 100%"
                        src="./graficos/<%=grafico2 %>.aspx?FRM=frm02&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                </div>
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <iframe id='frm03' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                        style="width: 100%"
                        src="./graficos/<%=grafico3 %>.aspx?FRM=frm03&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                </div>
            </div>
            <div style="display: flex; flex-direction: row">
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <iframe id='frm04' frameborder="0" scrolling="no" style="height: 65px; width: 100%"
                        src="./graficos/<%=grafico4 %>.aspx?FRM=frm04&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                </div>
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <iframe id='frm05' frameborder="0" scrolling="no" style="height: 65px; width: 100%"
                        src="./graficos/<%=grafico5 %>.aspx?FRM=frm05&IDProjeto=<%=codigoProjeto %>&TipoTela=<%=tipoTela %><%=paramFin %>"></iframe>
                </div>
            </div>
            <div style="display: flex; flex-direction: row">
                <div style="display: flex; flex-grow: 1; padding: 3px">
                    <iframe id='frm06' frameborder="0" scrolling="no" width="100%" onload="this.style.height = this.contentWindow.document.body.scrollHeight + 'px';"
                        src="<%=urlGrafico06 %>"></iframe>
                </div>
            </div>
        </div>
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
                            <td align="right">
                                <table celpaddin="0" cellspacing="0" class="formulario-botoes">
                                    <tr>
                                        <td>
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
    </form>
    <script type="text/javascript" language="javascript">
        window.addEventListener('DOMContentLoaded', function () {
            var divStatusProjeto = document.getElementById('divStatusProjeto');
            var sHeight = Math.max(0, document.documentElement.clientHeight) - 10;
            divStatusProjeto.style.height = sHeight + "px";
        });
    </script>
</body>
</html>
