<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_027.aspx.cs" Inherits="grafico_027" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <style type="text/css">
.tdRotate
{
  -ms-transform:rotate(-90deg); /* IE 9 */
  -moz-transform:rotate(-90deg); /* Firefox */
  -webkit-transform:rotate(-90deg); /* Safari and Chrome */
  -o-transform:rotate(-90deg); /* Opera */
  padding:0px;
  margin:0px;
  width:5px;
}
</style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table>
                <tr>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pRiscos" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue"  HeaderText="Riscos"
                            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
                            <ContentPaddings Padding="1px" PaddingTop="2px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" />
                            <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" 
                                ForeColor="#404040" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle">
                                <Paddings Padding="1px" PaddingLeft="3px" PaddingTop="0px" />
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellpadding="0" cellspacing="0" width="95%" >
                                        <tr>
                                            <td rowspan="3" class="tdRotate">Impacto</td>
                                            <td style="background-color: #ffff6f; vertical-align: middle; text-align: center;" id="tdRisco_3_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_3_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ff484a; vertical-align: middle; text-align: center;" id="tdRisco_3_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_3_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #CC0000; vertical-align: middle; text-align: center;" id="tdRisco_3_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_3_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #8cff8c; vertical-align: middle; text-align: center;" id="tdRisco_2_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_2_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #FFFF00; vertical-align: middle; text-align: center;" id="tdRisco_2_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_2_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ff484a; vertical-align: middle; text-align: center;" id="tdRisco_2_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_2_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #66FF33; vertical-align: middle; text-align: center;" id="tdRisco_1_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_1_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #8cff8c; vertical-align: middle; text-align: center;" id="tdRisco_1_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_1_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ffff6f; vertical-align: middle; text-align: center;" id="tdRisco_1_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlRisco_1_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="3" >
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="False"
                                                    Text="Probabilidade">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                    <td style="width: 5px; background-color: #EBEBEB;"></td>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pQuestoes" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue"  HeaderText="Questões"
                            ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="100%">
                            <ContentPaddings Padding="1px" PaddingTop="2px" />
                            <Border BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" />
                            <HeaderStyle BackColor="#EBEBEB" Font-Bold="True" 
                                ForeColor="#404040" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle">
                                <BorderLeft BorderStyle="None" />
                                <BorderRight BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellpadding="0" cellspacing="0" width="95%" >
                                        <tr>
                                            <td rowspan="3" class="tdRotate">Prioridade</td>
                                            <td style="background-color: #ffff6f; vertical-align: middle; text-align: center;" id="tdQuestao_3_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_3_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ff484a; vertical-align: middle; text-align: center;" id="tdQuestao_3_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_3_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #CC0000; vertical-align: middle; text-align: center;" id="tdQuestao_3_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_3_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #8cff8c; vertical-align: middle; text-align: center;" id="tdQuestao_2_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_2_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #FFFF00; vertical-align: middle; text-align: center;" id="tdQuestao_2_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_2_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ff484a; vertical-align: middle; text-align: center;" id="tdQuestao_2_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_2_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #66FF33; vertical-align: middle; text-align: center;" id="tdQuestao_1_1" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_1_1" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #8cff8c; vertical-align: middle; text-align: center;" id="tdQuestao_1_2" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_1_2" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="background-color: #ffff6f; vertical-align: middle; text-align: center;" id="tdQuestao_1_3" runat="server">
                                                <dxtv:ASPxHyperLink ID="hlQuestao_1_3" runat="server" ForeColor="#000000" Font-Bold="true" Target="_parent" Text="">
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="3" >
                                                <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Font-Bold="False"
                                                    Text="Urg&#234;ncia">
                                                </dxe:ASPxLabel>
                                            </td>
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
