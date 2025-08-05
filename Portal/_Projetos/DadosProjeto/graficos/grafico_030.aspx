<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_030.aspx.cs" Inherits="grafico_030" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    </head>
<body style="margin:0px">
    <form id="form1" runat="server">
        <div runat="server" id="dvDados">
            <table>
                <tr>
                    <td style = "width:50%;">
                        <dxrp:ASPxRoundPanel ID="pEntregas" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue"  HeaderText="Entregas"
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
                                    <%=conteudoPainelEntregas %>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                    <td style="width: 5px; background-color: #EBEBEB;"></td>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pDesemp" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                            CssPostfix="PlasticBlue"  HeaderText="Desempenho"
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
                                    <%=conteudoPainelDesemp %>
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
