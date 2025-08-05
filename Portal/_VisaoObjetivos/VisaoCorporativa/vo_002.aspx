<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vo_002.aspx.cs" Inherits="_VisaoObjetivos_VisaoCorporativa_vo_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function abreCompromissos(statusParam)
        {
            if(statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_Agenda.aspx', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_Agenda.aspx', '_top');
        }
        
        function atualizaTela(lParam)
        {
            window.location.reload();
        }
        
        function abreMensagensNovas(statusParam)
        {
            window.top.showModal("../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S", 'Mensagens', 1000, 600, atualizaTela, null);          
        }
        
        function abreRespostas(statusParam)
        {
                 if(statusParam == "A")
                    window.top.showModal("../_VisaoExecutiva/MensagensEnviadasNaoRespondidasExecutivo.aspx?Status=A", 'Mensagens', 960, 550, '', null);       
                else
                    window.top.showModal("../_VisaoExecutiva/MensagensEnviadasNaoRespondidasExecutivo.aspx", 'Mensagens', 960, 550, '', null);                  
        }
        
    </script>
</head>
<body class="body" style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Desempenho"
            Height="215px" Width="210px" ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False"  BackColor="#EBEBEB" Height="23px">
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
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/calendarioDash.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkCompromissos" runat="server" 
                                    Text="Compromissos" EncodeHtml="False" ForeColor="Black">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/MensagemNova.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkMensagensRecebidas" runat="server"
                                    Text="Mensagens Pendentes" EncodeHtml="False" ForeColor="Black">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                    </table>
                    
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td align=left><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Comunicação">
                            </dxe:ASPxLabel>  </td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
