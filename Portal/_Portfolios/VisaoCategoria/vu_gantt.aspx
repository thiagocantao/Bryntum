<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vu_gantt.aspx.cs" Inherits="_Portfolios_VisaoCategoria_vu_gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <script type="text/javascript" language="JavaScript">
		    var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
			chartSample.width = "100%";
			chartSample.height = <%=alturaGrafico %>;
			chartSample.setXMLFile('<%=grafico_xml %>');
			chartSample.write();
			//]]>
		</script>
		<%=nenhumGrafico %>
    </form>
</body>
</html>