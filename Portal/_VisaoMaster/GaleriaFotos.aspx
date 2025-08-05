<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GaleriaFotos.aspx.cs" Inherits="_VisaoMaster_GaleriaFotos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        var numeroImagem = 0;

        function proximaFoto() {
            numeroImagem++;
            if (numeroImagem <= pnFotos.cp_NumeroFotos) {
                pnFotos.PerformCallback(numeroImagem);
            }
        }

        function fotoAnterior() {
            numeroImagem--;
            if (numeroImagem >= 0) {
                pnFotos.PerformCallback(numeroImagem);
            }
        }

        function verificaNumeroFoto() {

            if (0 == pnFotos.cp_NumeroFotos) {
                document.getElementById('imgProxima').style.display = "none";
                document.getElementById('imgAnterior').style.display = "none";
                return;
            }

            if (numeroImagem == pnFotos.cp_NumeroFotos)
                document.getElementById('imgProxima').style.display = "none";

            if (numeroImagem == 0)
                document.getElementById('imgAnterior').style.display = "none";
        }

        function loadFotos() {
            numeroImagem = pnFotos.cp_NumeroImagem;
            verificaNumeroFoto();
        }
    </script>
    <title></title>
    <style type="text/css">
        .style2 {
            width: 10px;
        }

        .style3 {
            width: 10px;
            height: 10px;
        }

        .style4 {
            height: 10px;
        }

        .style5 {
            width: 10px;
            height: 5px;
        }

        .style6 {
            height: 5px;
        }

        #form1 {
            text-align: left;
        }
    </style>
</head>
<body style="margin: 0px" onload="loadFotos()">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" align="center" width="100%">
            <tr>
                <td class="style2">&nbsp;</td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnFotos" runat="server" Width="100%"
                        ClientInstanceName="pnFotos" OnCallback="pnFoto_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	txtDescricaoFoto.SetText(s.cp_DescricaoFotos);               
    verificaNumeroFoto();
}"
                            Init="function(s, e) {
	verificaNumeroFoto();
}" />
                        <PanelCollection>
                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <img title="Foto Anterior" src="../imagens/fotoAnterior.png" onmouseover="this.style.opacity = 0.8; this.style.filter = 'alpha(opacity=80)'" onmouseout="this.style.opacity = 0.2; this.style.filter = 'alpha(opacity=20)'"
                                    id="imgAnterior" style="position: absolute; top: 0px; left: 10px; cursor: pointer; opacity: 0.2; filter: alpha(opacity=20); padding-top: 149px; padding-bottom: 149px;" onclick="fotoAnterior()" />
                                <dxe:ASPxBinaryImage ID="imgZoom" runat="server" Height="360px" Width="100%">
                                    <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                    </EmptyImage>
                                    <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                </dxe:ASPxBinaryImage>
                                <img title="Próxima Foto" src="../imagens/proximaFoto.png" onmouseover="this.style.opacity = 0.8; this.style.filter = 'alpha(opacity=80)'" onmouseout="this.style.opacity = 0.2; this.style.filter = 'alpha(opacity=20)'"
                                    style="position: absolute; top: 0px; left: 480px; cursor: pointer; opacity: 0.2; filter: alpha(opacity=20); padding-top: 149px; padding-bottom: 149px;" id="imgProxima" onclick="proximaFoto()" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td class="style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="style5"></td>
                <td class="style6"></td>
                <td class="style5"></td>
            </tr>
            <tr>
                <td class="style2">&nbsp;</td>
                <td>
                    <dx:ASPxLabel runat="server" Text="Descrição:" ID="Label28"></dx:ASPxLabel>


                </td>
                <td class="style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">&nbsp;</td>
                <td>
                    <dxe:ASPxMemo runat="server" Rows="4" Width="100%"
                        ClientEnabled="False"
                        ID="txtDescricaoFoto" ClientInstanceName="txtDescricaoFoto">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                    </dxe:ASPxMemo>


                </td>
                <td class="style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="style3"></td>
                <td class="style4"></td>
                <td class="style3"></td>
            </tr>
            <tr>
                <td class="style2">&nbsp;</td>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharGeral"
                        Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
                <td class="style2">&nbsp;</td>
            </tr>
        </table>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
