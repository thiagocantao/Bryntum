<%@ Page Language="C#" AutoEventWireup="true" CodeFile="visaoCorporativa_01.aspx.cs" Inherits="_VisaoObjetivos_VisaoCorporativa_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />   
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
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico2 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <iframe frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no" src="<%=grafico3 %>"
                                    width="<%=larguraTela %>"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width: <%=larguraTela %>"><iframe frameborder="0" height="<%=alturaTela %>" scrolling="auto" src="<%=grafico1 %>"
                                    width="<%=larguraGrid %>"></iframe>
                </td>               
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
