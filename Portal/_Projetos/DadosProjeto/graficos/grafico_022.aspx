<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico_022.aspx.cs" Inherits="_Projetos_DadosProjeto_graficos_grafico_022" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

.dxrpControl .dxrpCI
{
	display:block;
}
.dxrpControl .dxrpTE,
.dxrpControl .dxrpNHTE,
.dxrpControlGB .dxrpNHTE
{
	border-top: 1px solid #8B8B8B;
}
.dxrpControl .dxrpLE,
.dxrpControl .dxrpRE,
.dxrpControl .dxrpBE,
.dxrpControl .dxrpNHTE
{
    background-image: none;
	background-color: #F7F7F7;
}
.dxrpControl .dxrpLE,
.dxrpControl .dxrpHLE,
.dxrpControlGB .dxrpLE,
.dxrpControlGB .dxrpHLE
{
	border-left: 1px solid #8B8B8B;
}
.dxrpControl td.dxrp,
.dxrpControlGB td.dxrp
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: #000000;
}
.dxrpControl .dxrpcontent
{
    background-image: none;
    background-color: #F7F7F7;
}
.dxrpControl .dxrpcontent,
.dxrpControlGB .dxrpcontent
{
	vertical-align: top;
}
.dxrpControl .dxrpRE,
.dxrpControl .dxrpHRE,
.dxrpControlGB .dxrpRE
{
	border-right: 1px solid #8B8B8B;
}
.dxrpControl .dxrpBE,
.dxrpControlGB .dxrpBE
{
	border-bottom: 1px solid #8B8B8B;
}
    </style>

</head>
<body style="margin:0px">
    <form id="form1" runat="server">
   
    
                            <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: 1px; border-color: #CCCCCC; width:100%; height: 65px;">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxImage ID="imgStatusProjeto" runat="server" 
                                                                    ImageUrl="~/imagens/bullets/branco.png" Height="24px">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblCorProjeto" runat="server" 
                                                                   >
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxLabel ID="lblTituloGer" runat="server" 
                                                                    Text="Gerente:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblGerente" runat="server">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Status:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblStatus" runat="server">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Atualização:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblAtualizacao" runat="server">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  width="25%">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 3px">
                                                                <dxe:ASPxImage ID="imgCronograma" runat="server" 
                                                                    ImageUrl="~/imagens/FisicoBranco.png">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="lblCronograma" runat="server" Target="_parent">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center"  width="25%">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 3px">
                                                                <dxe:ASPxImage ID="imgFinanceiro" runat="server" 
                                                                    ImageUrl="~/imagens/FinanceiroBranco.png">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="lblFinanceiro" runat="server" Target="_parent">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center"  width="25%">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 3px">
                                                                <dxe:ASPxImage ID="imgRisco" runat="server" 
                                                                    ImageUrl="~/imagens/RiscoBranco.png">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="lblRisco" runat="server" Target="_parent">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right"  width="25%">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 3px">
                                                                <dxe:ASPxImage ID="imgQuestao" runat="server" 
                                                                    ImageUrl="~/imagens/QuestaoBranco.png">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="lblQuestao" runat="server" Target="_parent">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
    
    
    </form>
</body>
</html>
