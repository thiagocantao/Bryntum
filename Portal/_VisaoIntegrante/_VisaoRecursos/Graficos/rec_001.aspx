<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rec_001.aspx.cs" Inherits="_VisaoAnalista_Graficos_an_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function abreTarefasAtualizacao(statusParam)
        {
            if(statusParam == "A")
               window.top.gotoURL('Tarefas/Atualizacao.aspx?Atrasadas=S', '_top');
            else
                window.top.gotoURL('Tarefas/Atualizacao.aspx', '_top');
        }

        function abreTarefasAprovacao() {
            window.top.gotoURL("Tarefas/Aprovacao.aspx", '_top');
        }

        function abreRiscos(statusParam) {
            if (statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=R', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=R', '_top');
        }

        function abreIssues(statusParam) {
            if (statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?Cor=Vermelho&TipoTela=Q', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MeusRiscosProblemas.aspx?TipoTela=Q', '_top');
        }

        function abreMensagensNovas(statusParam) {

            window.top.showModal("../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S", 'Mensagens', 1000, 600, atualizaTela, null);
        }

        function atualizaTela(lParam) {
            window.location.reload();
        }
        
    </script>
    <style type="text/css">
        .style1
        {
            width: 50px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
            CssPostfix="PlasticBlue" HeaderText="Pendências" Width="100%" 
            ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  
                BackColor="#EBEBEB">
                <Paddings PaddingBottom="6px" PaddingTop="4px" />
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height:100%">
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/tarefas.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkTarefas" runat="server"  Text="Tarefas" ForeColor="Black" EncodeHtml="False">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td align="left" class="style1">
                                <dxe:ASPxImage ID="ASPxImage6" runat="server" 
                                    ImageUrl="~/imagens/tarefasAprovacao.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkTarefasAprovacao" runat="server" EncodeHtml="False" 
                                     ForeColor="Black" Text="Tarefas">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/riscos.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkRiscos" runat="server" EncodeHtml="False" 
                                     ForeColor="Black" Text="Riscos">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td align="left" class="style1">
                                <dxe:ASPxImage ID="ASPxImage5" runat="server" ImageUrl="~/imagens/issue.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkIssues" runat="server" EncodeHtml="False" 
                                     ForeColor="Black" Text="Issues">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" 
                                    ImageUrl="~/imagens/MensagemNova.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkMensagensRecebidas" runat="server" EncodeHtml="False" 
                                     ForeColor="Black" 
                                    Text="Mensagens Pendentes">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td align="left" class="style1">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td align=left><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Pendências">
                            </dxe:ASPxLabel>  </td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
