<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ganttPlanoAcao.aspx.cs" Inherits="gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Gantt do Plano de Ação do Tema Estratégico Selecionado</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">    
    
    </script>
</head>
<body style="margin:0px" scroll="no">
    <form id="form1" runat="server">
    <div>
        
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td align="left" style="width: 10px; height: 3px">
                    </td>
                    <td align="left" style="height: 3px">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                    <td align="left">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                            Text="Objetivo Estratégico:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px; height: 5px">
                    </td>
                    <td align="left">
                        <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientInstanceName="txtObjetivoEstrategico"
                             ReadOnly="True" Width="99%">
                            <ReadOnlyStyle BackColor="#E0E0E0">
                            </ReadOnlyStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px; height: 4px">
                    </td>
                    <td align="left" style="height: 4px">
                    </td>
                </tr>
            </table>
            <div id="divChart">                    
		            </div>
          <script type="text/javascript" language="JavaScript">
		    var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
		    chartSample.width = "99%";
			if(<%=alturaGrafico %> == 0)
		        chartSample.height = 0;
		    else
			    chartSample.height = screen.height - 350;
			chartSample.setXMLFile('<%=grafico_xml %>');
			chartSample.write('divChart');
		</script>
		<%=nenhumGrafico %>
       </div>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                        HeaderText="Legenda" Width="170px">
                        <ContentPaddings Padding="1px" />
                        <Border BorderColor="#8B8B8B" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle  />                        
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 20px; height: 17px; background-color:Red">
                                        </td>
                                        <td style="height: 17px">
                                            &nbsp;<dxe:ASPxLabel ID="lblDescricaoPendiente" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                     Text="Tarefas Atrasadas">
                                                </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                        Text="Fechar" Width="75px">
                        <Paddings Padding="0px" />
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>