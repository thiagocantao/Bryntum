<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_008.aspx.cs" Inherits="_Projetos_VisaoCorporativa_vc_002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    
        <div id="chartdiv" align="center" style="width:100%; height:100%;">
                    </div>

                    <script type="text/javascript">
                          getGrafico('<%=grafico_swf %>', "grafico001", '100%', '100%', '<%=grafico_xml %>', 'chartdiv');
                    </script>
    </form>
</body>
</html>
