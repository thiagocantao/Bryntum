<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_026.aspx.cs" Inherits="grafico_008" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 45px;
        }
        .style4
        {
            height: 11px;
        }
        .auto-style1 {
            height: 10px;
        }
        .auto-style2 {
            width: 2px;
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="pNumeros" runat="server" BackColor="White"  HeaderText=""
            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="420px" ContentHeight="43px">
            <ContentPaddings Padding="0px" PaddingTop="1px" PaddingLeft="2px" />
            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="White" Font-Bold="False" 
                ForeColor="#404040" Height="23px">
                <Paddings Padding="2px" />
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
                    <table>
                        <tr>
                            <td style="width: 43%">
                                <table>
                                    <tr>
                                        <td align="left" colspan="3">
                                            <dxtv:ASPxHyperLink ID="hlCronograma" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="0 Atrasos no Cronograma">
                                            </dxtv:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 10px"></td>
                                        <td align="left"></td>
                                        <td align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <dxtv:ASPxHyperLink ID="hlEntregas" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="0 Entregas Atrasadas">
                                            </dxtv:ASPxHyperLink>
                                        </td>
                                        <td align="left" style="width: 2px"></td>
                                        <td align="left">
                                            <dxtv:ASPxHyperLink ID="hlToDoList" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="0 Atrasos no To Do List">
                                            </dxtv:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 3px"></td>
                                        <td align="left" style="width: 2px; height: 3px"></td>
                                        <td align="left" style="height: 3px"></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 57%">
                                <table>
                                    <tr>
                                        <td align="left" style="width: 47%">
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td class="style2">
                                                        <dxtv:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/riscos.png">
                                                        </dxtv:ASPxImage>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="height: 16px">
                                                                    <dxtv:ASPxHyperLink ID="hlRiscosAtivos" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="Riscos Ativos">
                                                                    </dxtv:ASPxHyperLink>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 7px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 16px">
                                                                    <dxtv:ASPxHyperLink ID="hlRiscosEliminados" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="Riscos Eliminados">
                                                                    </dxtv:ASPxHyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" style="width: 53%">
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td class="style2">
                                                        <dxtv:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/issue.png">
                                                        </dxtv:ASPxImage>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="height: 16px">
                                                                    <dxtv:ASPxHyperLink ID="hlProblemasAtivos" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="Problemas Ativos">
                                                                    </dxtv:ASPxHyperLink>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 7px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 15px">
                                                                    <dxtv:ASPxHyperLink ID="hlProblemasEliminados" runat="server"  ForeColor="#5D7B9D" Target="_parent" Text="Problemas Eliminados">
                                                                    </dxtv:ASPxHyperLink>
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
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
