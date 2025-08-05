<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_GanttRecursos.aspx.cs" Inherits="frameEspacoTrabalho_GanttRecursos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle">
                                &nbsp; &nbsp; &nbsp;
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                    Text="Meu Gantt">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="left">
                            </td>
                            <td style="width: 100px" align="left">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="Tipo:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 280px">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Ação:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px" align="left">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Início:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 105px">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Término:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 120px">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                    Text="Status das Tarefas:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 110px">
                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                    Text="Aprovação:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	selectFile();

}" />
                                </dxcb:ASPxCallback>
                                &nbsp;</td>
                            <td align="left" style="width: 100px">
                                <dxe:ASPxComboBox ID="ddlTipo" runat="server" 
                                    SelectedIndex="0" ValueType="System.String" Width="91px">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Todos" Value="TD" />
                                        <dxe:ListEditItem Text="Projeto" Value="PR" />
                                        <dxe:ListEditItem Text="To Do List" Value="TL" />
                                        <dxe:ListEditItem Text="Demanda" Value="DE" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="width: 280px">
                                <dxe:ASPxComboBox ID="ddlProjeto" runat="server" 
                                    Width="272px">
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="width: 105px">
                                <dxe:ASPxDateEdit ID="ddlInicio" runat="server" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy"  Width="96px" ClientInstanceName="ddlInicio">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 105px">
                                <dxe:ASPxDateEdit ID="ddlTermino" runat="server" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy"  Width="97px" ClientInstanceName="ddlTermino">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 120px">
                                <dxe:ASPxComboBox ID="ddlStatus" runat="server" 
                                    SelectedIndex="0" ValueType="System.String" Width="112px">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Todas" Value="T" />
                                        <dxe:ListEditItem Text="Atrasadas" Value="A" />
                                        <dxe:ListEditItem Text="Futuras" Value="F" />
                                        <dxe:ListEditItem Text="Conclu&#237;das" Value="C" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="width: 110px">
                                <dxe:ASPxComboBox ID="ddlAprovacao" runat="server" 
                                    SelectedIndex="0" ValueType="System.String" Width="104px">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Todas" Value="T" />
                                        <dxe:ListEditItem Text="Aprovadas" Value="A" />
                                        <dxe:ListEditItem Text="Reprovadas" Value="R" />
                                        <dxe:ListEditItem Text="Pendentes" Value="P" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="center" style="width: 95px">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                                    Text="Selecionar" AutoPostBack="False">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) {
	if(ddlInicio.GetValue() != null &amp;&amp; ddlTermino.GetValue() != null &amp;&amp; ddlInicio.GetValue() &gt; ddlTermino.GetValue())
	{
		window.top.mostraMensagem('Data de Início não pode ser maior que a Data de Término!', 'Atencao', true, false, null);
	}
	else
	{
		callback.PerformCallback();
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tdGrafico">
                <td>
                    <script type="text/javascript" language="JavaScript">
		                var chartSample = new AnyChart('./../flashs/AnyGantt.swf');
			            chartSample.width = "100%";
			            chartSample.height = <%=alturaGrafico %>;
			            chartSample.setXMLFile('<%=grafico_xml %>');
			            chartSample.write();
			            
			            selectFile = function() {
				                                var filePath = callback.cp_XML;
				                                if(callback.cp_Altura == "0")
				                                {
				                                    document.getElementById('tdGrafico').style.display = "none";
				                                    document.getElementById('tdMsg').style.display = "block";
				                                }
				                                else
				                                {
				                                    document.getElementById('tdGrafico').style.display = "block";
				                                    document.getElementById('tdMsg').style.display = "none";
				                                }
				                                chartSample.setLoading("Carregando...");
				                                chartSample.setXMLFile(filePath);
				                                }

			            //]]>
		            </script>		         
                </td>
            </tr>
            <tr id="tdMsg" style="display:none">
            <td><%=nenhumGrafico %></td>
            </tr>
        </table>
    </div>
   </asp:Content>