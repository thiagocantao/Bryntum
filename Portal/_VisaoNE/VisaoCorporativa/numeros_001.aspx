<%@ Page Language="C#" AutoEventWireup="true" CodeFile="numeros_001.aspx.cs" Inherits="_Projetos_VisaoCorporativa_numeros_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3 {
            height: 20px;
        }

        .style4 {
            height: 2px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: left">
            <table cellspacing="1" style="width:100%">
                <tr>
                    <td>
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
                            HeaderText="Visão Quantitativa (Todos os Anos)" Width="100%">
                            <ContentPaddings Padding="0px" PaddingTop="2px" />
                            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" BackColor="#EBEBEB">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                                <Paddings PaddingBottom="4px" PaddingTop="2px" PaddingLeft="3px"
                                    PaddingRight="3px" />
                            </HeaderStyle>
                            <HeaderContent>
                                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/260627823/HeaderContent.png" Repeat="RepeatX"
                                    VerticalPosition="bottom" HorizontalPosition="left" />
                            </HeaderContent>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td align="left" style="border-top: #c6c6c6 1px solid; padding-left: 8px; color: white; border-bottom: #c6c6c6 1px solid; background-color: #ebebeb"
                                                            class="style3">
                                                            <strong><span>
                                                                <asp:Label ID="lblTituloTotal0" runat="server" Font-Bold="False"
                                                                    ForeColor="#404040"
                                                                    Text="Todos os Segmentos"></asp:Label>
                                                            </span></strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                                <span id="spanObras_T1" runat="server"></span></td>
                                        </tr>
                                        <tr>
                                            <td height="5" style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td align="left" style="border-top: #c6c6c6 1px solid; padding-left: 8px; color: white; border-bottom: #c6c6c6 1px solid; background-color: #ebebeb"
                                                            class="style3">
                                                            <strong><span>
                                                                <asp:Label ID="lblTituloTotal1" runat="server" Font-Bold="False"
                                                                    ForeColor="#404040"
                                                                    Text="Por Segmento"></asp:Label>
                                                            </span></strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                                <span id="spanObras_T2" runat="server"></span></td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 2px">
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" BackColor="White"
                            HeaderText="Visão Financeira (Todos os Anos)" Width="100%">
                            <ContentPaddings Padding="0px" PaddingTop="2px" />
                            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" BackColor="#EBEBEB">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                                <Paddings PaddingBottom="4px" PaddingTop="2px" PaddingLeft="3px"
                                    PaddingRight="3px" />
                            </HeaderStyle>
                            <HeaderContent>
                                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/260627823/HeaderContent.png" Repeat="RepeatX"
                                    VerticalPosition="bottom" HorizontalPosition="left" />
                            </HeaderContent>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table>
                                        <tr>
                                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                                <span id="spanContratos_T1" runat="server"></span></td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
