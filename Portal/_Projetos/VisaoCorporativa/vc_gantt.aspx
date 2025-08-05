<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_gantt.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_vc_gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />        
    <base target="_self" />
    <title>Gantt de Projetos</title>

    <!--Ext and ux styles -->
    <link href="../../scripts/basicBryNTum/ext-all.css" rel="stylesheet"
        type="text/css" />
    
    <!--Scheduler styles-->
    <link href="../../scripts/basicBryNTum/sch-gantt-all.css" rel="stylesheet" type="text/css" />
    <!--Implementation specific styles-->
    <link href="../../scripts/basicBryNTum/basic.css" rel="stylesheet" type="text/css" />
    <!--Ext lib and UX components-->
    <script src="../../scripts/basicBryNTum/ext-all.js" type="text/javascript"></script>
    <script src="../../scripts/basicBryNTum/gnt-all-debug.js?v=2" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    
    
    <style type="text/css">
        .iconzoom-out:before {
    content: "\f010";
    font-family: FontAwesome;
    font-style: normal;
    font-weight: normal;
    text-decoration: inherit;
    font-size: 18px;
}

.iconzoom-to-fit:before {
    content: "\f0b2";
    font-family: FontAwesome;
    font-style: normal;
    font-weight: normal;
    text-decoration: inherit;
    font-size: 18px;
}

.iconzoom-in:before {
    content: "\f00e";
    font-family: FontAwesome;
    font-style: normal;
    font-weight: normal;
    text-decoration: inherit;
    font-size: 18px;
}

.icon-fullscreen:before {
    content: "\f108";
    font-family: FontAwesome;
    font-style: normal;
    font-weight: normal;
    text-decoration: inherit;
    font-size: 18px;
}

       .sch-ganttpanel-showbaseline .sch-gantt-milestone-baseline {
    display: none;
}


.sch-dependency-line, .sch-dependency-arrow {
     border-color: #95c477; 
}

  .x-tree-node-text,  .x-grid-cell-inner {
      font-size:14px;
      }

      .azul .x-tree-node-text, .azul .x-grid-cell-inner {
        color:#0000FF;
      }

      .vermelho .x-tree-node-text, .vermelho .x-grid-cell-inner {
        color:#FF0000;
      }

      .amarelo .x-tree-node-text, .amarelo .x-grid-cell-inner {
        color:#c6bc08;
      }

      .verde .x-grid-cell-inner {
        background-color:#49a517;
      }

      .marco .x-tree-node-text {
        font-style:italic;
      }





.x-tree-node-text {
     text-transform: none; 
}
    

        .style1
        {
            width: 10px;
        }
    </style>
</head>
<body style="margin: 0px; overflow:hidden">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td id="tdGraficoGantt" align="left" style="padding:10px;">
                    <div  runat="server" id="dvGrafico" style="width:100%">
                        <div id="basicGantt" style="width:100%;">
        <dxcp:ASPxLoadingPanel ID="LoadingPanel" runat="server" 
        ClientInstanceName="loadingPanel" 
        Text="Aguardando assinatura&amp;hellip;"  Modal="True">
        </dxcp:ASPxLoadingPanel>
                    </div>
                    </div>
		<%=nenhumGrafico %></td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 10px">
                            </td>
                            <td>
                                &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>        
    </form>
</body>
</html>