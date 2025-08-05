<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ganttPlanoAcao.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_ganttPlanoAcao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base target="_self" />
    <title><asp:Literal runat="server" Text="<%$ Resources:traducao, ganttPlanoAcao_gantt_do_plano_de_a__o_do_objetivo_estrat_gico_selecionado %>" /></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">    
    
    </script>
    <style type="text/css">


        .style1
        {
            width: 10px;
        }
    </style>
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
                    <td align="left" style="padding-left: 10px">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                            Text="Objetivo Estratégico:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px; height: 5px">
                    </td>
                    <td align="left" style="padding-right: 10px; padding-left: 10px;">
                        <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientInstanceName="txtObjetivoEstrategico"
                             ReadOnly="True" Width="100%">
                            <ReadOnlyStyle BackColor="#E0E0E0">
                            </ReadOnlyStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px; height: 5px">
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px;">
                    </td>
                    <td id="tdGraficoGantt" align="left">
                    <div id="divChart">                    
		            </div>
                                  <script type="text/javascript" language="JavaScript">
		    var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
		    chartSample.width = "100%";
			if(<%=alturaGrafico %> == 0)
		        chartSample.height = 0;
		    else
			    chartSample.height = screen.height - 370;
			chartSample.setXMLFile('<%=grafico_xml %>');
			chartSample.write('divChart');
		</script> 
		<%=nenhumGrafico %></td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px; height: 4px">
                    </td>
                    <td align="left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxrp:ASPxRoundPanel ID="Aspxroundpanel2" runat="server" ClientInstanceName="pLegenda"
                                    HeaderText="Legenda" HorizontalAlign="Left"
                                    Width="100%" ShowHeader="False">
                                    <ContentPaddings Padding="5px" />
                                    <HeaderStyle Font-Bold="True">
                                        <Paddings Padding="1px" PaddingLeft="3px" />
                                    </HeaderStyle>
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table cellpadding="0" cellspacing="0" enableviewstate="false">
                                                <tbody>
                                                    <tr>
                                                        <td id="legVerde" runat="server" 
                                                            style="width: 10px; border: 1px solid #808080; background-color: #FF0000;">
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right:10px">
                                                            <asp:Label ID="Label6" runat="server" EnableViewState="False" 
                                                                Font-Size="7pt">Atrasadas</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </dxrp:ASPxRoundPanel>
                            </td>
                            <td class="style1">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10px;">
                    </td>
                    <td align="right" style="padding-right: 10px; padding-top: 10px;">
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                        Text="Fechar" Width="90px">
                        <Paddings Padding="0px" />
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
       </div>
    </form>
</body>
</html>