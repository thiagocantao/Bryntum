<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame_MenuEsquerdo.aspx.cs" Inherits="formularios_frame_MenuEsquerdo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <title>Untitled Page</title>
</head>
<body style="margin:1px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 145px">
            <tr>
                <td align="center">
                    &nbsp;<dxnb:ASPxNavBar ID="nbBarraLateral" runat="server" AutoCollapse="True" ClientInstanceName="nbBarraLateral"
                        CssFilePath="~/App_Themes/Blue/{0}/styles.css" CssPostfix="Blue" GroupSpacing="2px"
                        ImageFolder="~/App_Themes/Blue/{0}/" Width="144px">
                        <CollapseImage Url="~/App_Themes/Blue/Web/nbCollapse.gif" />
                        <Groups>
                            <dxnb:NavBarGroup Expanded="False" Name="grFormularios" Text="Formul&#225;rios">
                            </dxnb:NavBarGroup>
                            <dxnb:NavBarGroup Name="grProcessos" Text="Processos">
                            </dxnb:NavBarGroup>
                        </Groups>
                        <ExpandImage Url="~/App_Themes/Blue/Web/nbExpand.gif" />
                    </dxnb:ASPxNavBar>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
