<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gantt.aspx.cs" Inherits="_VisaoMaster_Graficos_gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">    
     
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 10px;
        }
.dxbButton
{
	color: #000000;
	font: normal 12px Tahoma;
	vertical-align: middle;
	border: 1px solid #7F7F7F;
	padding: 1px;
	cursor: pointer;
}
        .style4
        {
            width: 105px;
        }
        .style5
        {
            width: 150px;
        }
        .style6
        {
            width: 56px;
        }
        .style7
        {
            width: 120px;
        }
    </style>
    </head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td style="padding-right: 10px">
                            <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server" 
                                ClientInstanceName="txtPesquisa"  
                                NullText="Pesquisar por palavra chave..." Width="100%">
                                <Buttons>
                                    <dxe:EditButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Search" 
                                                HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                        </Image>
                                    </dxe:EditButton>
                                </Buttons>
                                <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                            </dxe:ASPxButtonEdit>
                                </td>
                                <td class="style6">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                        Text="Término:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td class="style4" style="padding-right: 10px">
                                    <dxe:ASPxDateEdit ID="ddlTermino" runat="server" 
                                        Width="100%">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td class="style5">
                                    <dxe:ASPxCheckBox ID="ckConcluidas" runat="server" 
                                        Text="Apenas Concluídas">
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td class="style7">
                                    <dxe:ASPxButton ID="ASPxButton2" runat="server" 
                                        Text="Selecionar" Width="110px">
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td>
                                     <div id="divChart">                    
		                
		            </div>		            
                        <%=nenhumGrafico %></td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>                   
                    </td>
                </tr>
                </table>
          
       </div>
    </form>
    <script type="text/javascript" language="JavaScript">
		                var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
		                chartSample.width = <%=larguraGrafico %>;
			            chartSample.height = <%=alturaGrafico %>;
			            chartSample.setXMLFile('<%=grafico_xml %>');
			            chartSample.write('divChart');
		            </script>
</body>
</html>