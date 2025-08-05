<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoomGantt.aspx.cs" Inherits="ZoomGantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="./scripts/AnyChart.js" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="divChart">                    
		            </div>
		            
    </div>
    </form>
    <script type="text/javascript" language="JavaScript">
        var chartSample = new AnyChart('./flashs/AnyGantt.swf');
        chartSample.width = '100%';
        chartSample.height = window.top.myObject.param2;
        chartSample.setXMLFile(window.top.myObject.param1);
        chartSample.write('divChart');

                        
		            </script>
</body>
</html>
