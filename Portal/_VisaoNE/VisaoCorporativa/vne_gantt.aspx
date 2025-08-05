<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vne_gantt.aspx.cs" Inherits="_VisaoNE_VisaoCorporativa_vne_gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
    </head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table>
         <tr>
                <td align="right">
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Município:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-right: 10px">
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                    ValueType="System.String" Width="380px" 
                                    ClientInstanceName="ddlMunicipio"  
                                    ID="ddlMunicipio">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ItemStyle Wrap="True"></ItemStyle>

<ListBoxStyle Wrap="True"></ListBoxStyle>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                            </td>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                    Text="Status:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-right: 10px">
                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="220px" 
                                    ClientInstanceName="ddlStatus"  
                                        ID="ddlStatus">

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" 
                                    Text="Selecionar" Width="90px">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
           <tr>
                <td align="left">
                    <div id="divChart">                    
		            </div>		            
                        <%=nenhumGrafico %>
                    </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                                <dxrp:ASPxRoundPanel ID="Aspxroundpanel1" runat="server" ClientInstanceName="pLegenda"
                                     HeaderText="Legenda" HorizontalAlign="Left"
                                    Width="100%" ShowHeader="False">
                                    <ContentPaddings Padding="5px" />
                                    <HeaderStyle Font-Bold="True">
                                        <Paddings Padding="1px" PaddingLeft="3px" />
                                    </HeaderStyle>
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                            <table cellpadding="0" cellspacing="0" enableviewstate="false">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" 
                                                                ImageUrl="~/imagens/SemCronograma.PNG" Height="16px">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right:10px">
                                                            <asp:Label ID="Label3" runat="server" EnableViewState="False"
                                                                Font-Size="7pt">Projetos Sem Cronograma</asp:Label>
                                                        </td>
                                                        <td id="legVerde" runat="server" style="width: 10px; border: 1px solid #808080;">
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right:10px">
                                                            <asp:Label ID="Label1" runat="server" EnableViewState="False"
                                                                Font-Size="7pt">Satisfatório</asp:Label>
                                                        </td>
                                                        <td id="legAmarelo" runat="server" style="width: 10px; border: 1px solid #808080;">
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right:10px">
                                                            <asp:Label ID="Label2" runat="server" EnableViewState="False"
                                                                Font-Size="7pt">Atenção</asp:Label>
                                                        </td>
                                                        <td id="legVermelho" runat="server" style="width: 10px; border: 1px solid #808080;">
                                                        </td>
                                                        <td style="padding-left: 3px; padding-right:10px">
                                                            <asp:Label ID="Label4" runat="server" EnableViewState="False"
                                                                Font-Size="7pt">Crítico</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>        
    </form>
    <script type="text/javascript" language="JavaScript">
		    var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
			chartSample.width = "100%";
			chartSample.height = <%=alturaGrafico %>;
			chartSample.setXMLFile('<%=grafico_xml %>');
			chartSample.write('divChart');
	</script>
</body>
</html>