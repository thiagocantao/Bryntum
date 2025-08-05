<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sobre.aspx.cs" Inherits="sobre" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="estilos/custom_frame.css" rel="stylesheet" />
    <style type="text/css">
        .style2
        {
            height: 5px;
            width: 5px;
        }
        .style3
        {
            width: 5px;
        }
        .style5
        {
            width: 180px;
        }
        .tituloSobre{
            font-size:0.9rem;
            font-family:Roboto, Arial, sans-serif;
            font-weight:600;
            color: #484848;
            border-bottom: 1px solid #eee;
            padding-bottom: 5px;
        }

         .subTituloSobre{
            padding:5px 0px !important;
            font-size:0.85rem !important;
            font-family:Arial, sans-serif !important;
            font-weight:normal !important;
            color: #484848 !important;
         }

        .subTituloSobre span{
            padding:5px 0px !important;
            font-size:0.85rem !important;
            font-family:Arial, sans-serif !important;
            font-weight:normal !important;
            color: #484848 !important;
        }
    </style>
</head>
<body scroll="no" data-pagina="Sobre">
    <form id="form1" runat="server" enableviewstate="false">
    <table cellpadding="0" cellspacing="0" id="conteudoSobre" width="100%">
        <tr>
            <td class="style5">
                <img src="imagens/Sobre/barraLateralSobre.PNG" />
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td class="style3">
                        </td>
                        <td align="left" valign="top">
                            <table>
                                <tr>
                                    <td style="height: 15px">
                                        <span id="nomeProjeto" class="tituloSobre" runat="server" >BRISK</span></td>
                                </tr>
                                <tr>
                                    <td style="height: 3px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 13px">
                                        <span id="spnCopyRight" class="subTituloSobre" runat="server" style="">.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 3px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="subTituloSobre">
                                            <%= Resources.traducao.todos_os_direitos_reservados_%>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subTituloSobre">
                                        <dxtv:ASPxLabel ID="lblAtualizacao" runat="server" EncodeHtml="False" >
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #cccccc; height: 77px; padding-left: 0.25rem; padding-top: 0.5rem;" valign="top">
                                        <span style="">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNomeCliente" class="subTituloSobre" runat="server" ></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   </form>
</body>

</html>
