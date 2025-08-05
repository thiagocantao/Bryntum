<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_SimulacaoGantt.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_Simulacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />        
    <base target="_self" />
    <title>Gantt de Projetos</title>

    <!--Ext and ux styles -->
    <link href="../scripts/basicBryNTum/ext-all.css" rel="stylesheet"
        type="text/css" />
    
    <!--Scheduler styles-->
    <link href="../scripts/basicBryNTum/sch-gantt-all.css" rel="stylesheet" type="text/css" />
    <!--Implementation specific styles-->
    <link href="../scripts/basicBryNTum/basic.css" rel="stylesheet" type="text/css" />
    <!--Ext lib and UX components-->
    <script src="../scripts/basicBryNTum/ext-all.js" type="text/javascript"></script>
    <script src="../scripts/basicBryNTum/gnt-all-debug.js?v=2" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    
    <script type="text/javascript" language="javascript">
        var statusVC = 1;
        
        function atualizaDados()
        {            
            btnSelecionar.DoClick()
        }
    </script>
    <style type="text/css">
       .sch-ganttpanel-showbaseline .sch-gantt-milestone-baseline {
    display: none;
}
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

.sch-dependency-line, .sch-dependency-arrow {
     border-color: #95c477; 
}

  .x-tree-node-text,  .x-grid-cell-inner {
      font-size:14px;
      }

      .azul .x-tree-node-text, .azul .x-grid-cell-inner {
        color:#0000FF;
      }s

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
<body style="margin-top:0; overflow:auto">
    <form id="form1" runat="server">
    <div style="width:100%">
        <table style="width:100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td align="right" style="width: 70px; height: 5px">
                            </td>
                            <td style="width: 95px; height: 5px">
                            </td>
                            <td style="width: 85px; height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="border-right: gainsboro 1px solid;
                                    border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                    <tr>
                                        <td style="width: 30px" valign="bottom">
                                <dxe:ASPxImage ID="imgVisao" runat="server" ImageUrl="~/imagens/graficos.PNG" ToolTip="Mostrar Gráficos">
                                    <ClientSideEvents Click="function(s, e) 
{
	window.location.href = './frameSelecaoBalanceamento_Simulacao.aspx';
}" />
                                </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 95px">
                                            <dxe:ASPxLabel ID="lblGantt" runat="server" 
                                                Text="Mostrar Gráficos">
                                                <ClientSideEvents Click="function(s, e) 
{
	window.location.href = './frameSelecaoBalanceamento_Simulacao.aspx';
}" />
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="width: 70px">
                                <dxe:ASPxLabel ID="lblCenario" runat="server" 
                                    Text="Cenário:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 95px">
                                <dxe:ASPxComboBox ID="ddlCenario" runat="server" ClientInstanceName="ddlCenario"
                                     SelectedIndex="0" ValueType="System.String"
                                    Width="90px">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Cen&#225;rio 1" Value="1" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 2" Value="2" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 3" Value="3" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 4" Value="4" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 5" Value="5" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 6" Value="6" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 7" Value="7" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 8" Value="8" />
                                        <dxe:ListEditItem Text="Cen&#225;rio 9" Value="9" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="width: 85px">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                    Text="Selecionar" OnClick="btnSelecionar_Click">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 6px">
                </td>
            </tr>
            <tr>
                <td id="tdGraficoGantt" align="left">
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
        </table>
    </div>
    </form>
</body>
</html>
