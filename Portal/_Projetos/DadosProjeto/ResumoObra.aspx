<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResumoObra.aspx.cs" Inherits="_Projetos_DadosProjeto_ResumoProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Portfólio - Dados da Obra</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .dxeTextBox,
        .dxeMemo {
            background-color: white;
            border: 1px solid #9f9f9f;
        }

        .dxeTrackBar,
        .dxeIRadioButton,
        .dxeButtonEdit,
        .dxeTextBox,
        .dxeRadioButtonList,
        .dxeCheckBoxList,
        .dxeMemo,
        .dxeListBox,
        .dxeCalendar,
        .dxeColorTable {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

        .dxeTextBoxSys, .dxeMemoSys {
            border-collapse: separate !important;
        }

        .dxeTextBox .dxeEditArea {
            background-color: white;
        }

        .dxeEditArea {
            font: 12px Tahoma;
            border: 1px solid #A0A0A0;
        }

        .dxeEditAreaSys {
            width: 100%;
            background-position: 0 0; /*iOS Safari*/
        }

        .dxeEditAreaSys, .dxeEditAreaNotStrechSys {
            border: 0px !important;
            padding: 0px;
        }

        .style4 {
            width: 10px;
        }

        .style5 {
            height: 5px;
            width: 10px;
        }

        .style8 {
            width: 105px;
        }

        .style11 {
            width: 10px;
            height: 10px;
        }

        .style12 {
            height: 5px;
        }

        .style13 {
            width: 160px;
        }

        .style18 {
            height: 4px;
            width: 10px;
        }

        .style19 {
            height: 4px;
        }

        .style20 {
            width: 95px;
        }

        .style21 {
            width: 115px;
        }

        .dxpcControl {
            font: 12px Tahoma, Geneva, sans-serif;
            color: black;
            background-color: white;
            border: 1px solid #8B8B8B;
            width: 200px;
        }

        .dxpcHeader {
            color: #404040;
            background-color: #DCDCDC;
            border-bottom: 1px solid #C9C9C9;
        }

        .dxpcHBCell {
            padding: 1px 1px 1px 2px;
        }

        .dxpcContent {
            color: #010000;
            white-space: normal;
            vertical-align: top;
        }

        .dxpcContentPaddings {
            padding: 9px 12px;
        }

        .style10 {
            height: 10px;
        }

        .style22 {
            width: 100%;
            color: #404040;
            white-space: nowrap;
        }
    </style>
</head>
<body scroll='auto' style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="style5"></td>
                    <td class="style12"></td>
                    <td class="style5"></td>
                </tr>
                <tr>
                    <td class="style4"></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Projeto:"></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:Label ID="Label2" runat="server"
                                        Text="Início Previsto:"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label3" runat="server"
                                        Text="Término Previsto:"></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:Label ID="Label4" runat="server"
                                        Text="Início Real:"></asp:Label>
                                </td>
                                <td class="style21">
                                    <asp:Label ID="Label5" runat="server"
                                        Text="Término Reprogr.:"></asp:Label>
                                </td>
                                <td class="style21">
                                    <asp:Label ID="lblAtualizacao" runat="server"
                                        Text="Última Atualização:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxTextBox ID="txtProjeto" runat="server"
                                        Width="100%" ClientEnabled="False">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style20">
                                    <dxe:ASPxTextBox ID="txtInicioPrevisto" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style8">
                                    <dxe:ASPxTextBox ID="txtTerminoPrevisto" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style20">
                                    <dxe:ASPxTextBox ID="txtInicioReal" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style21">
                                    <dxe:ASPxTextBox ID="txtTerminoReprogramado" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style21">
                                    <dxe:ASPxTextBox ID="txtAtualizacao" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style4"></td>
                </tr>
                <tr>
                    <td class="style11"></td>
                    <td class="style12"></td>
                    <td class="style11"></td>
                </tr>
                <tr>
                    <td class="style5"></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server"
                                        Text="Município:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server"
                                        Text="Contratada:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtMunicipio" runat="server"
                                        Width="100%" ClientEnabled="False">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtContratada" runat="server"
                                        Width="100%" ClientEnabled="False">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style5"></td>
                </tr>
                <tr>
                    <td class="style11"></td>
                    <td class="style12"></td>
                    <td class="style11"></td>
                </tr>
                <tr>
                    <td class="style4"></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server"
                                        Text="Número Contrato:"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label19" runat="server"
                                        Text="Início Vigência:"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label20" runat="server"
                                        Text="Término Vigência:"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="Label21" runat="server"
                                        Text="Valor Contratado:"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="lblPrevistoParcela" runat="server"
                                        Text="Valor Medido:"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="Label23" runat="server"
                                        Text="Saldo Contratual:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtNumeroContrato" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style8" style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtInicioVigencia" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style8" style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtTerminoVigencia" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style13" style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtValorContratado" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="c2">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style13" style="padding-right: 10px">
                                    <dxe:ASPxTextBox ID="txtValorMedido" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="c2">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style13">
                                    <dxe:ASPxTextBox ID="txtSaldoContratual" runat="server"
                                        Width="100%" ClientEnabled="False"
                                        DisplayFormatString="c2">
                                        <ValidationSettings Display="None" ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style4"></td>
                </tr>
                <tr>
                    <td class="style11"></td>
                    <td class="style12"></td>
                    <td class="style11"></td>
                </tr>
                <tr>
                    <td class="style4"></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="padding-right: 1px" valign="top">
                                    <iframe id='frm01' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                                        width="100%"
                                        src="<%=telaEspaco1 %>"
                                        name="frm01"></iframe>
                                </td>
                                <td style="padding-left: 1px" valign="top">
                                    <iframe id='frm02' frameborder="0" scrolling="no" height="<%=alturaGraficos %>"
                                        width="100%"
                                        src="<%=telaEspaco2 %>"
                                        name="frm02"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style4"></td>
                </tr>
                <tr>
                    <td class="style18"></td>
                    <td class="style19"></td>
                    <td class="style18"></td>
                </tr>
                <tr>
                    <td class="style4"></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td width="50%">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxBinaryImage ID="img001" runat="server" Height="121px" Width="180px">
                                                                <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                                                </EmptyImage>
                                                                <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                                            </dxe:ASPxBinaryImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblFoto1" runat="server"
                                                                Text="Foto 1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxBinaryImage ID="img002" runat="server" Height="121px" Width="180px">
                                                                <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                                                </EmptyImage>
                                                                <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                                            </dxe:ASPxBinaryImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblFoto2" runat="server"
                                                                Text="Foto 2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxBinaryImage ID="img003" runat="server" Height="121px" Width="180px">
                                                                <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                                                </EmptyImage>
                                                                <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                                            </dxe:ASPxBinaryImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblFoto3" runat="server"
                                                                Text="Foto 3"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td align="left" valign="top" style="padding-left: 5px">
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label27" runat="server"
                                                                Text="Último Comentário da Fiscalização:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxhe:ASPxHtmlEditor ID="htmlUltimoComentario" runat="server"
                                                                ActiveView="Preview" Height="110px" Width="100%">
                                                                <Styles>
                                                                    <ContentArea>
                                                                        <Paddings Padding="0px" />
                                                                    </ContentArea>
                                                                </Styles>
                                                                <Settings AllowContextMenu="False" AllowDesignView="False"
                                                                    AllowHtmlView="False" AllowInsertDirectImageUrls="False" />
                                                            </dxhe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblUltimoComentario" runat="server"></asp:Label>
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
                    <td class="style4"></td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="pcEventosAtlPrj" runat="server" ClientInstanceName="pcEventosAtlPrj"
                HeaderText="Atualizações"
                Width="800px" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table cellspacing="1" width="100%">
                            <tr>
                                <td>
                                    <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                        style="width: 100%">
                                        <tr runat="server">
                                            <td runat="server" style="padding: 3px" valign="top">
                                                <table>
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
                                    <table cellspacing="1" width="100%">
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
