<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_00.aspx.cs" Inherits="_VisaoMaster_Graficos_visaoCorporativa_00" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript">
        window.parent.idPaginaAtual = 2;
        window.parent.imgAnterior.SetVisible(true);
        window.parent.imgProximo.SetVisible(true);

        window.parent.imgAnterior.SetEnabled(true);
        window.parent.imgProximo.SetEnabled(true);
        
        window.parent.urlAnterior = '1'
        window.parent.urlProximo = '3'

        window.parent.lblTitulo.SetText('Visão Global UHE Belo Monte');
        window.parent.imgFotos.SetVisible(false);
        window.parent.imgGrafico.SetVisible(false);
        window.parent.btnDownLoad.SetVisible(false);
        window.parent.btnEAP.SetVisible(false);
        window.parent.verificaLabelImagensVisivel();
        

        function mostrarNumeros(mostrar) {
            if (mostrar) {
                document.getElementById('tdNumeros').style.display = 'block';
                imgMostrarNumeros.SetVisible(false);

            }
            else {
                document.getElementById('tdNumeros').style.display = 'none';
                imgMostrarNumeros.SetVisible(true);
            }
        }

        function abreFotosSitio(iniciais, nomeSitio) {
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/GaleriaFotos.aspx?NumeroFotos=999&CR=' + iniciais, "Fotos - " + nomeSitio, 565, 490, "", null);
        }

        function getAjuda() {
            return hfAjuda.Get("TextoAjuda");
        }

    </script> 

    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />   
    <style type="text/css">

        .style1
        {
            width: 100%;
        }

        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td valign="top" align="center" style="width: <%=larguraTela %>">
                    <table>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=alturaFrame1 %>" scrolling="no" src="<%=grafico1 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                 <table cellpadding="0" cellspacing="0" class="style1">
                     <tr>
                         <td>
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td align="center">
                                         <dxe:ASPxBinaryImage ID="img001" runat="server" Height="90px" Width="140px" 
                                             BinaryStorageMode="Session" StoreContentBytesInViewState="True" 
                                             ViewStateMode="Enabled">
                                             <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                             </EmptyImage>
                                             <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                         </dxe:ASPxBinaryImage>
                                     </td>
                                 </tr>
                                 </table>
                         </td>
                         <td align="center">
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td align="center">
                                         <dxe:ASPxBinaryImage ID="img002" runat="server" Height="90px" Width="140px" 
                                             BinaryStorageMode="Session" StoreContentBytesInViewState="True" 
                                             ViewStateMode="Enabled">
                                             <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                             </EmptyImage>
                                             <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                         </dxe:ASPxBinaryImage>
                                     </td>
                                 </tr>
                                 </table>
                         </td>
                         <td align="center">
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td align="center">
                                         <dxe:ASPxBinaryImage ID="img003" runat="server" Height="90px" Width="140px" 
                                             BinaryStorageMode="Session" StoreContentBytesInViewState="True" 
                                             ViewStateMode="Enabled">
                                             <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                             </EmptyImage>
                                             <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                         </dxe:ASPxBinaryImage>
                                     </td>
                                 </tr>
                                 </table>
                         </td>
                         <td align="center">
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td align="center">
                                         <dxe:ASPxBinaryImage ID="img004" runat="server" Height="90px" Width="140px" 
                                             BinaryStorageMode="Session" StoreContentBytesInViewState="True" 
                                             ViewStateMode="Enabled">
                                             <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                             </EmptyImage>
                                             <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                         </dxe:ASPxBinaryImage>
                                     </td>
                                 </tr>
                                 </table>
                         </td>
                         <td align="center">
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td align="center">
                                         <dxe:ASPxBinaryImage ID="img005" runat="server" Height="90px" Width="140px" 
                                             BinaryStorageMode="Session" StoreContentBytesInViewState="True" 
                                             ViewStateMode="Enabled">
                                             <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                             </EmptyImage>
                                             <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                         </dxe:ASPxBinaryImage>
                                     </td>
                                 </tr>
                                 </table>
                         </td>
                     </tr>
                     <tr>
                         <td align="center">
                                                <dxe:ASPxLabel runat="server" Text="Sítio Pimental" 
                                        Font-Bold="False"  ID="lblLeganda" 
                                        Font-Italic="True" Width="115px"></dxe:ASPxLabel>

                         </td>
                         <td align="center">
                                                <dxe:ASPxLabel runat="server" Text="Sítio Belo Monte" 
                                        Font-Bold="False"  ID="lblLeganda0" 
                                        Font-Italic="True" Width="115px"></dxe:ASPxLabel>

                         </td>
                         <td align="center">
                                                <dxe:ASPxLabel runat="server" Text="Sítio Infraestrutura" 
                                        Font-Bold="False"  ID="lblLeganda1" 
                                        Font-Italic="True" Width="115px"></dxe:ASPxLabel>

                         </td>
                         <td align="center">
                                                <dxe:ASPxLabel runat="server" Text="Sítio Canais de Derivação, Transposição e Enchimento" 
                                        Font-Bold="False"  ID="lblLeganda3" 
                                        Font-Italic="True" Width="160px"></dxe:ASPxLabel>

                         </td>
                         <td align="center">
                                                <dxe:ASPxLabel runat="server" Text="Sítio Diques" 
                                        Font-Bold="False"  ID="lblLeganda2" 
                                        Font-Italic="True" Width="115px"></dxe:ASPxLabel>

                         </td>
                     </tr>
                 </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="width:420px">
                    <table>
                        <tr>
                            <td align="center">
                    <iframe id="tdNumeros" frameborder="0" height="<%=alturaNumeros %>" scrolling="no" src="numeros_006.aspx?CR=UHE_Principal"
                        width="100%"></iframe>
                                <dxe:ASPxImage ID="imgMostrarNumeros" runat="server" ClientInstanceName="imgMostrarNumeros"
                                    ClientVisible="False" CssClass="mao" ImageUrl="~/imagens/mostrarNumeros.png"
                                    ToolTip="Mostrar Números dos Contratos">
                                    <ClientSideEvents Click="function(s, e) {
	mostrarNumeros(true);
}" />
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <dxhf:ASPxHiddenField ID="hfAjuda" runat="server" ClientInstanceName="hfAjuda">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
