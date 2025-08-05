<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_009.aspx.cs" Inherits="grafico_009" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <style type="text/css">
        .style8
        {
            width: 100%;
        }
        .style9
        {
            width: 110px;
        }
        .style11
        {
            width: 55%;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="50%">
                            <dxrp:ASPxRoundPanel ID="pDespesa" runat="server" 
            BackColor="White"  HeaderText="Despesa" 
            Width="100%" EncodeHtml="False" 
            ClientInstanceName="pNumeros" ContentHeight="45px" CornerRadius="1px">
                                <ContentPaddings Padding="0px" PaddingTop="1px" PaddingLeft="2px" />
                                <HeaderStyle BackColor="#EBEBEB" Font-Bold="False"  Height="20px">
                                    <Paddings Padding="2px" />
                                </HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" class="style8">
                                            <tr>
                                                <td class="style11">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; ">
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblCustoPrevisto12" runat="server" 
                                                                    ClientInstanceName="lblCustoPrevisto12"  
                                                                    Text="Previsto Total:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblCustoPrevisto" runat="server" 
                                                                    ClientInstanceName="lblCustoPrevisto"  
                                                                    Text="0">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblCustoPrevistoData12" runat="server" 
                                                                    ClientInstanceName="lblCustoPrevistoData12" 
                                                                    Text="Prev. Até Data:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblCustoPrevistoData" runat="server" 
                                                                    ClientInstanceName="lblCustoPrevistoData"  
                                                                    Text="0">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblCustoReal12" runat="server" 
                                                                    ClientInstanceName="lblCustoReal12"  
                                                                    Text="Real:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblCustoReal" runat="server" 
                                                                    ClientInstanceName="lblCustoReal"  Text="0">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
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
                                    <td width="50%">
                            <dxrp:ASPxRoundPanel ID="pReceita" runat="server" 
            BackColor="White"  HeaderText="Receita" 
            Width="100%" EncodeHtml="False" 
            ClientInstanceName="pNumeros" ContentHeight="45px" CornerRadius="1px">
                                <ContentPaddings Padding="0px" PaddingTop="1px" PaddingLeft="2px" />
                                <HeaderStyle BackColor="#EBEBEB" Font-Bold="False"  Height="20px">
                                    <Paddings Padding="2px" />
                                </HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" class="style8">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; ">
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblReceitaPrevista12" runat="server" 
                                                                    ClientInstanceName="lblReceitaPrevista12"  
                                                                    Text="Prevista Total:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblReceitaPrevista" runat="server" 
                                                                    ClientInstanceName="lblReceitaPrevista"  
                                                                    Text="0">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblReceitaPrevistaData12" runat="server" 
                                                                    ClientInstanceName="lblReceitaPrevistaData12" 
                                                                    Text="Prev. Até Data:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblReceitaPrevistaData" runat="server" 
                                                                    ClientInstanceName="lblReceitaPrevistaData" 
                                                                    Text="0">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style9">
                                                                <dxe:ASPxLabel ID="lblReceitaReal12" runat="server" 
                                                                    ClientInstanceName="lblReceitaReal12"  
                                                                    Text="Real:">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="left">
                                                                <dxe:ASPxLabel ID="lblReceitaReal" runat="server" 
                                                                    ClientInstanceName="lblReceitaReal"  
                                                                    Text="0">
                                                                    <Border BorderStyle="None" />
                                                                </dxe:ASPxLabel>
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
                                </tr>
                            </table>
        &nbsp;</div>
    </form>
</body>
</html>
