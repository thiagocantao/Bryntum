<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResumoProjetoTipo1.aspx.cs" Inherits="_Projetos_DadosProjeto_ResumoProjetoTipo1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style="margin:5px;">
    <form id="form1" runat="server">
    <div>
    
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td >
                <iframe id="frm1" frameborder="0" height="70" name="frm1" scrolling="no" src="graficos/grafico_022.aspx?IDProjeto=<%=codigoProjeto %>" 
                    width="100%"></iframe>
            </td>
        </tr>
        <tr>
            <td >
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td style="padding-right: 2px">
                <iframe id="I1" frameborder="0" name="I1" height="<%=alturaFrames %>" scrolling="no" src="<%=grafico1 %>" 
                    width="100%"></iframe>
                            </td>
                        <td style="padding-left: 2px">
                <iframe id="I2" frameborder="0" name="I2" height="<%=alturaFrames %>" scrolling="no" src="<%=grafico2 %>" 
                    width="100%"></iframe>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <iframe id="I3" frameborder="0" name="I3" height="<%=alturaFrames %>" scrolling="no" src="<%=grafico3 %>" 
                    width="100%"></iframe>
                </td>
        </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
