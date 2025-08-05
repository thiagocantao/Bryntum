<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoUnidade_01.aspx.cs" Inherits="_Portfolios_VisaoCategoria_visaoUnidade_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
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
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vu_001.aspx"
                                    width="<%=larguraTabela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vu_002.aspx"
                                    width="<%=larguraTabela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px">
                </td>
                <td align="center" valign="top"  style="width: <%=larguraTabela %>">
                    <table>
                        <tr>
                            <td>
                    <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vu_004.aspx"
                        width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="vu_005.aspx" width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px">
                </td>
                <td>
                                <iframe src="vu_003.aspx" frameBorder="0" width="<%=larguraTela %>" 
                                    scrolling=no height="<%=alturaTela %>"></iframe>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
