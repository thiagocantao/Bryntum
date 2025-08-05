<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ganttDesktop.aspx.cs" Inherits="_Projetos_DadosProjeto_ganttDesktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--Ext and ux styles -->
    <link href="../scripts/basicBryNTum/ext-all.css" rel="stylesheet" type="text/css" />
    <link href="../Bootstrap/css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../estilos/custom.css" rel="stylesheet" type="text/css" />

    <!--Scheduler styles-->
    <link href="../scripts/basicBryNTum/sch-gantt-all.css" rel="stylesheet" type="text/css" />
    <!--Implementation specific styles-->
    <link href="../scripts/basicBryNTum/basic.css?v=2" rel="stylesheet" type="text/css" />
    <link href="../scripts/basicBryNTum/examples.css" rel="stylesheet" type="text/css" />
    <!--Ext lib and UX components-->
    <script src="../scripts/basicBryNTum/ext-all.js?v=0" type="text/javascript"></script>
    <script src="../scripts/basicBryNTum/gnt-all-debug.js?ver=3.0.12" type="text/javascript"></script>
    <script src="../scripts/basicBryNTum/Gantt.js" type="text/javascript"></script>
    <link href="../scripts/basicBryNTum/sch-gantt-all.css" rel="stylesheet" />
    <style type="text/css">
        /*3188c0ff*/


        .sch-ganttpanel-showbaseline .sch-gantt-milestone-baseline {
            display: none !important;
        }


        .sch-dependency-line, .sch-dependency-arrow {
            border-color: #95c477 !important;
        }

        .x-tree-node-text, .x-grid-cell-inner {
            font-size: 14px !important;
        }

        .adiantada .x-tree-node-text, .adiantada .x-grid-cell-inner {
            color: #0000FF !important;
        }

        .atrasada .x-tree-node-text, .atrasada .x-grid-cell-inner {
            color: #FF0000 !important;
        }

        .marco .x-tree-node-text {
            font-style: italic !important;
        }

        .x-tree-node-text {
            text-transform: none !important;
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

        .iconzoom-out:before {
            content: "\f010";
            font-family: FontAwesome;
            font-style: normal;
            font-weight: normal;
            text-decoration: inherit;
            font-size: 18px;
        }
    </style>


    <%--    <script type="text/javascript">
            loadingPanel.Show();
    </script>--%>
</head>
<body style="margin: 0px" onresize="App.Gantt.refresh();">
    <form id="form1" runat="server">

        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td align="left">
                        <div id="basicGantt" style="width: 100%;">
                        </div>
                        <%=nenhumGrafico %>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table id="tbLegenda" runat="server" cellpadding="0" cellspacing="0"
                            style="width: 100%; background-color: #E5E5E5; height: 25px;">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style4">
                                        <tr>
                                            <td style="padding-left: 10px">
                                                <table cellpadding="0" cellspacing="0" style="height: 15px">
                                                    <tr>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <span>
                                                                <dxcp:ASPxLabel ID="lblNum" ClientInstanceName="lblNum" runat="server" Text="Atrasadas" ForeColor="Red">
                                                                </dxcp:ASPxLabel>
                                                            </span>

                                                        </td>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <dxcp:ASPxLabel ID="Label4" ClientInstanceName="Label4" runat="server" Text="Adiantadas" ForeColor="Blue">
                                                            </dxcp:ASPxLabel>

                                                        </td>
                                                        <td style="border: 1px solid #808080; background-color: #7342d7" width="10px">&nbsp;</td>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <dxcp:ASPxLabel ID="Label3" ClientInstanceName="Label4" runat="server" Text="Críticas">
                                                            </dxcp:ASPxLabel>

                                                        </td>
                                                        <td style="border: 1px solid #808080; background-color: #bc9987;" width="10px">&nbsp;</td>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <dxcp:ASPxLabel ID="Label5" ClientInstanceName="Label5" runat="server" Text="Linha de base">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="border: 1px solid #808080; background-color: forestgreen;" width="10px">&nbsp;</td>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <dxcp:ASPxLabel ID="ASPxLabel8" ClientInstanceName="Label5" runat="server" Text="Concluído">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right: 10px">
                                                            <span>
                                                                <dxcp:ASPxLabel ID="Label1" ClientInstanceName="Label1" runat="server" Font-Italic="True" Text="Marcos em itálico">
                                                                </dxcp:ASPxLabel>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
