<%@ Page Language="C#" AutoEventWireup="true" CodeFile="numeros_001.aspx.cs" Inherits="_VisaoNE_VisaoExecutiva_numeros_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    </head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: left">
        <table cellspacing="1" class="headerGrid">
            <tr>
                <td>
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                        HeaderText="Números das Obras (Todos os Anos)" Width="100%" 
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="0px" PaddingTop="2px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False" Font-Italic="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
                <Paddings PaddingBottom="5px" PaddingTop="2px" PaddingLeft="3px" 
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
                                <table style="width:100%">
                                    <tr>
                                        <td align="left" style="border-top: #c6c6c6 1px solid; padding-left: 8px; color: white;width:100%;
                                            border-bottom: #c6c6c6 1px solid; height: 13px; background-color: #ebebeb">
                                            <strong><span>
                                            <asp:Label ID="lblTituloTotal0" runat="server" Font-Bold="False" 
                                                 ForeColor="#404040" 
                                                Text="Visão Quantitativa"></asp:Label>
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
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 1px">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" BackColor="White"
                        HeaderText="Números dos Contratos (Todos os Anos)" Width="100%">
            <ContentPaddings Padding="0px" PaddingTop="2px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False" Font-Italic="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
                <Paddings PaddingBottom="5px" PaddingTop="2px" PaddingLeft="3px" 
                PaddingRight="3px" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/260627823/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table style="width:100%">
                        <tr>
                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px;">
                                <table style="width:100%">
                                    <tr>
                                        <td align="left" style="border-top: #c6c6c6 1px solid; padding-left: 8px; color: white;
                                            border-bottom: #c6c6c6 1px solid; height: 13px; background-color: #ebebeb">
                                            <strong><span>
                                            <asp:Label ID="lblTituloTotal2" runat="server" Font-Bold="False" 
                                                 ForeColor="#404040" 
                                                Text="Visão Quantitativa"></asp:Label>
                                            </span></strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                <span id="spanContratos_T1" runat="server"></span></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                <table style="width:100%">
                                    <tr>
                                        <td align="left" style="border-top: #c6c6c6 1px solid; padding-left: 8px; color: white;
                                            border-bottom: #c6c6c6 1px solid; height: 13px; background-color: #ebebeb">
                                            <strong><span>
                                            <asp:Label ID="lblTituloTotal3" runat="server" Font-Bold="False" 
                                                 ForeColor="#404040" 
                                                Text="Visão Financeira"></asp:Label>
                                            </span></strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                <span id="spanContratos_T2" runat="server"></span></td>
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
