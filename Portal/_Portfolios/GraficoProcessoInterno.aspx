<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GraficoProcessoInterno.aspx.cs" Inherits="_Portfolios_GraficoProcessoInterno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   


    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <!-- CARGANDO SCRIPT'S -->
    <script type="text/javascript" language="Javascript" src="../scripts/PowerChartsJS/FusionCharts.js"></script>
    <script type="text/javascript" language="javascript">
        function reziseDiv() {
            __wf_chartObj.render("divFlash"); 
        }
    </script>
     <style type="text/css">
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
    </head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="<%=larguraPopup %>">
            <!-- titulo da tela (uso do ASPxlabel) -->
            <tr>
                <td>
                </td>
                <td style="height: 26px; padding-bottom: 3px; padding-top: 3px;">
                    <dxe:ASPxLabel ID="lblTituloTela" runat="server" Text="ASPxLabel" ClientInstanceName="lblTituloTela"
                        Font-Bold="False" >
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblTituloFluxo" runat="server" ClientInstanceName="lblTituloFluxo"
                        Font-Bold="True" ForeColor="#404040"></dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <!-- Espacio Separador -->
            <!-- Grafico WORKFLOW -->
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
               
                    <dxp:aspxpanel id="pnFlash" runat="server" clientinstancename="pnFlash" width="100%"><PanelCollection>
<dxp:PanelContent runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                            <td>
                            <div  id="divFlash"></div>
                            </td>
                    </tr>
                    <tr>
                    <td>
                    <table cellspacing="1" style="width:100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" style="border-top: lightgrey 1px solid; margin-top: 3px;">
                                    <tr>
                                        <td id="caminoPrecorrido" style="width: 10px;">
                                            <dxtv:ASPxLabel ID="lblCorPercorrido" runat="server" BackColor="#FFC080" ClientInstanceName="lblCorPercorrido" ForeColor="#FFC080" Text="P" Width="100%">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 3px;"></td>
                                        <td>
                                            <dxtv:ASPxLabel ID="lblCaminhoPercorrido" runat="server" ClientInstanceName="lblCaminhoPercorrido"  Text="Caminho Percorrido">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td id="EtapaAtual" style="width: 10px;">
                                            <dxtv:ASPxLabel ID="lblCorEtapaAtual" runat="server" BackColor="#80FF80" ClientInstanceName="lblCorEtapaAtual" ForeColor="Lime" Text="E" Width="100%">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 3px;"></td>
                                        <td>
                                            <dxtv:ASPxLabel ID="lblEtapaAtual" runat="server" ClientInstanceName="lblEtapaAtual"  Text="Etapa Atual">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td id="CaminhoNaoPrecorrido" style="width: 10px;">
                                            <dxtv:ASPxLabel ID="lblCorNaoPercorrido" runat="server" BackColor="Blue" ClientInstanceName="lblCorNaoPercorrido" ForeColor="Blue" Text="C" Width="100%">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 3px;"></td>
                                        <td>
                                            <dxtv:ASPxLabel ID="lblCaminhoNaoPercorrido" runat="server" ClientInstanceName="lblCaminhoNaoPercorrido"  Text="Caminho não percorrido">
                                            </dxtv:ASPxLabel>
                                        </td>                                       
                                    </tr>
                                </table>
                            </td>
                            <td align="right" id="Tdflash" style="display:none">
                                <dxtv:ASPxLabel ID="lblFlsh" runat="server" Font-Bold="True" Font-Italic="True"  ForeColor="Red" Text="MsgFlash">
                                </dxtv:ASPxLabel>
                            </td>
                             <td align="right">
                                 <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" CssClass="btn_inicialMaiuscula" Text="Fechar" Width="100px">
                                     <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}" />
                                 </dxtv:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                    </dxp:PanelContent>
</PanelCollection>
</dxp:aspxpanel>
                    
                    
        <dxhf:ASPxHiddenField ID="hfXML" runat="server" ClientInstanceName="hfXML">
        </dxhf:ASPxHiddenField>
                </td>
                <td valign="top">
                </td>
            </tr>
        </table>

        <script type="text/javascript">
        // __wf_chartObj -> variável definida no script WorkflowCharts.js
        //var div = $('divFlash');
        //if( null != div )
            //div.height = screen.availHeight;
            
            renderizaFlash();
            __wf_chartObj = new FusionCharts("../flashs/DragNode.swf", "nodeChart", "100%", "100%", "0", "1");
            __wf_chartObj.setTransparent(true);
            var flash = document.getElementById('Tdflash');

            if (FusionCharts.getCurrentRenderer() == "javascript")
                flash.style.display = "";

            setXmlComponente(); // atualiza o xml objeto chart
            __wf_chartObj.render("divFlash");
           // reziseDiv();
        </script>

    </form>
</body>
</html>
