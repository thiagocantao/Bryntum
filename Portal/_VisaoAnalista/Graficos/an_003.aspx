<%@ Page Language="C#" AutoEventWireup="true" CodeFile="an_003.aspx.cs" Inherits="_VisaoAnalista_Graficos_an_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link href="../../estilos/custom_frame.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function abreTarefas(statusParam)
        {
            if (statusParam == "A")
               window.top.gotoURL('./taskboard/TaskboardWrap.aspx?Atrasadas=S', '_top');
            else
                window.top.gotoURL('./taskboard/TaskboardWrap.aspx', '_top');
        }
        
        function abreIndicadores(statusParam)
        {
            if(statusParam == "A")
                window.top.gotoURL('_Estrategias/wizard/atualizacaoResultados.aspx?DiasVencimento=10&Atrasados=S&Filtro=NomeUsuario', '_top');
            else
                window.top.gotoURL('_Estrategias/wizard/atualizacaoResultados.aspx?DiasVencimento=10&Filtro=NomeUsuario', '_top');                
        }
        
        function abreContratos(statusParam)
        {
            if(statusParam == "A")
                window.top.gotoURL('_Projetos/Administracao/ParcelasContrato.aspx?Atrasados=S&Filtro=NomeUsuario', '_top');
            else
                window.top.gotoURL('_Projetos/Administracao/ParcelasContrato.aspx?Vencendo=S&Filtro=NomeUsuario', '_top');            
        }
        
        function abreToDoList(statusParam)
        {
            if(statusParam == "A")
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MinhaTarefas.aspx?Estagio=Atrasada', '_top');
            else
                window.top.gotoURL('espacoTrabalho/frameEspacoTrabalho_MinhaTarefas.aspx?Estagio=', '_top');            
        }
        </script>

    
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#FFFFFF" HeaderText="Desempenho" Width="325px" CornerRadius="1px">
            <ContentPaddings Padding="1px" />
            <HeaderStyle Font-Bold="False"  
                BackColor="#EBEBEB" Height="31px">
            </HeaderStyle>
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
                        </tr>
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/AtualizarIndicador.PNG">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkIndicadores" runat="server"  Text="Indicadores" ForeColor="Black" EncodeHtml="False">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50px">
                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/contrato_Pagamento.png">
                                </dxe:ASPxImage>
                            </td>
                            <td align="left">
                                <dxe:ASPxHyperLink ID="lkContratos" runat="server"  Text="Contratos" ForeColor="Black" EncodeHtml="False">
                                </dxe:ASPxHyperLink>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td align=left><dxe:ASPxLabel id="ASPxLabel1" runat="server" Text="Atualizações">
                            </dxe:ASPxLabel>  </td></tr></tbody></table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
