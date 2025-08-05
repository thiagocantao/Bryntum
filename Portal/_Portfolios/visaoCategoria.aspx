<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="visaoCategoria.aspx.cs" Inherits="_Portfolios_visaoCategoria" Title="Portal da Estratégia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table>
        <tr>
            <td align="right">
                <dxcb:ASPxCallback ID="callBackVC" runat="server" ClientInstanceName="callBackVC"
                    OnCallback="callBackVC_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	if(statusVC != 1)
	{
	    
		    document.getElementById('frmVC').src = './VisaoCategoria/vcu_gantt.aspx';
	}
	else
	{
		document.getElementById('frmVC').src = './VisaoCategoria/visaoCategoria_01.aspx';
	}
}" />
                </dxcb:ASPxCallback>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr>
                        <td align="left" style="width: 8px">
                        </td>
                        <td align="left" style="width: 140px">
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Monitor de Portfólio"></asp:Label></td>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxImage ID="imgVisao" runat="server" ClientInstanceName="imgVisao" ImageUrl="~/imagens/botoes/btnGantt.png"
                                            ToolTip="Visualizar Gantt">
                                            <ClientSideEvents Click="function(s, e) 
{
	if(statusVC == 1)
	{
		document.getElementById('frmVC').src = './VisaoCategoria/vcu_gantt.aspx';
		statusVC = 0;
		s.SetImageUrl('../imagens/graficos.PNG');
		s.mainElement.title = 'Mostrar Vis&#227;o Corporativa';
		lblGantt.SetText('Mostrar Gr&#225;ficos');		
	}
	else
	{
		document.getElementById('frmVC').src = './VisaoCategoria/visaoCategoria_01.aspx';
		statusVC = 1;
		s.SetImageUrl('../imagens/botoes/btnGantt.png');
		s.mainElement.title = 'Mostrar Gantt';
		lblGantt.SetText('Mostrar Gantt');
	}
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="width: 220px">
                            <dxe:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server"
                                RepeatDirection="Horizontal" SelectedIndex="2">
                                <Paddings Padding="0px" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == &quot;G&quot;)
	{
		window.top.gotoURL('_Portfolios/visaoCorporativa.aspx', '_self');
	}
	else
	{	
		if(s.GetValue() == &quot;U&quot;)
		{
			window.top.gotoURL('_Portfolios/visaoUnidade.aspx', '_self');
		}
	}
}" />
                                <Items>
                                    <dxe:ListEditItem Text="Geral" Value="G" />
                                    <dxe:ListEditItem Text="Unidade" Value="U" />
                                    <dxe:ListEditItem Text="Categoria" Value="C" />
                                </Items>
                                <Border BorderStyle="None" />
                            </dxe:ASPxRadioButtonList>
                        </td>
                        <td align="right" style="width: 64px">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Categoria:">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left" style="width: 290px">
                            <dxe:ASPxComboBox ID="ddlCategoria" runat="server" ClientInstanceName="ddlCategoria"
                                EnableSynchronization="True"  Width="285px">
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="left" style="width: 80px">
                            <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                Text="Selecionar">
                                <Paddings Padding="0px" />
                                <ClientSideEvents Click="function(s, e) 
{
	callBackVC.PerformCallback('AtualizarVC');
}" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="width: 5px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 3px">
            </td>
        </tr>
        <tr>
            <td>
                <iframe frameborder="0" name="graficos" scrolling="no" src="./VisaoCategoria/visaoCategoria_01.aspx"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

