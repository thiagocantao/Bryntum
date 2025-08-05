<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="espacoTrabalho_dashboard" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <script type="text/javascript" language="JavaScript"> 
    var refreshinterval=600; 
    var starttime; 
    var nowtime; 
    var reloadseconds=0; 
    var secondssinceloaded=0; 
    
    function starttime() 
    { 
        starttime=new Date(); 
        starttime=starttime.getTime(); 
        countdown(); 
    } 
 
    function countdown() 
    { 
        nowtime= new Date(); 
        nowtime=nowtime.getTime(); 
        secondssinceloaded=(nowtime-starttime)/1000; 
        reloadseconds=Math.round(refreshinterval-secondssinceloaded); 
        if (refreshinterval>=secondssinceloaded) 
        { 
            var timer=setTimeout("countdown()",1000);         
            
        } 
        else 
        { 
            clearTimeout(timer); 
            window.location.reload(true); 
        } 
    }
    window.onload = starttime; 
    
    function verificaVisao(itemSelecionado)
    {
        if(itemSelecionado == 0)
        {
            document.getElementById('tdInstitucional1').style.display = "inline-table";
            document.getElementById('tdInstitucional2').style.display = "inline-table";
            document.getElementById('tdInstitucional3').style.display = "inline-table";
            document.getElementById('tdInstitucional4').style.display = "inline-table";
            document.getElementById('tdEstagio1').style.display = "none";
            document.getElementById('tdEstagio2').style.display = "none";
            document.getElementById('tdOlap1').style.display = "none";
            document.getElementById('tdOlap2').style.display = "none";
            document.getElementById('tdOlap3').style.display = "none";
            document.getElementById('tdOlap4').style.display = "none";
            
            document.getElementById('frmVC').src = './dashboard/dashboard_02.aspx';
        }
        else
        {
            document.getElementById('tdInstitucional1').style.display = "none";
            document.getElementById('tdInstitucional2').style.display = "none";
            document.getElementById('tdInstitucional3').style.display = "none";
            document.getElementById('tdInstitucional4').style.display = "none";
            
            
            if(itemSelecionado == 1)
            {
                document.getElementById('frmVC').src = './Estagio/visaoEstagios_01.aspx';
                document.getElementById('tdEstagio1').style.display = "inline-table";
                document.getElementById('tdEstagio2').style.display = "inline-table";
                document.getElementById('tdOlap1').style.display = "none";
                document.getElementById('tdOlap2').style.display = "none";
                document.getElementById('tdOlap3').style.display = "none";
                document.getElementById('tdOlap4').style.display = "none";
            
            }
            else
            {
                document.getElementById('frmVC').src = './Estagio/visaoOLAPEstagios.aspx';
                document.getElementById('tdEstagio1').style.display = "none";
                document.getElementById('tdEstagio2').style.display = "none";
                document.getElementById('tdOlap1').style.display = "inline-table";
                document.getElementById('tdOlap2').style.display = "inline-table";
                document.getElementById('tdOlap3').style.display = "inline-table";
                document.getElementById('tdOlap4').style.display = "inline-table";
            }
        }
    }
    
</script>

    <table>
        <tr>
            <td>
                <dxcb:ASPxCallback ID="callBackVC" runat="server" ClientInstanceName="callBackVC"
                    OnCallback="callBackVC_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('frmVC').src = document.getElementById('frmVC').src;
}" />
                </dxcb:ASPxCallback>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <table>
                                <tr>
                                    <td style="padding-right: 10px">
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Painel de Bordo Institucional"></asp:Label>
                                    </td>
                                    <td>
                            <dxe:ASPxComboBox ID="ddlOpcaoVisao" runat="server" ClientInstanceName="ddlOpcaoVisao"
                                 SelectedIndex="0" ShowImageInEditBox="True"
                                ValueType="System.String" Width="178px">
                                <Items>
                                    <dxe:ListEditItem ImageUrl="~/imagens/graficos.PNG" Selected="True" Text="Painel Institucional"
                                        Value="0" />
                                    <dxe:ListEditItem ImageUrl="~/imagens/graficos.PNG" Text="Painel de Est&#225;gios" Value="1" />
                                    <dxe:ListEditItem ImageUrl="~/imagens/olap.PNG" Text="An&#225;lise de Est&#225;gios"
                                        Value="2" />
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaVisao(s.GetValue());
}" />
                            </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 60px;display: none;" valign="middle" id="tdOlap1">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Período:"></dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 115px;display: none;" valign="middle" id="tdOlap2">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" AllowNull="False" ClientInstanceName="dteInicio"
                                    Font-Overline="False" Width="105px" AllowUserInput="False" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="center" style="width: 24px;display: none;" id="tdOlap3">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="a" Width="10px">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 115px;display: none;" valign="middle" id="tdOlap4">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" AllowNull="False" ClientInstanceName="dteFim"
                                     Font-Strikeout="False" Width="105px" AllowUserInput="False" DisplayFormatString="dd/MM/yyyy">
                                    <ClientSideEvents ButtonClick="function(s, e) {

}" ValueChanged="function(s, e) {
}" />
                                </dxe:ASPxDateEdit>
                            </td>
                        <td id="tdEstagio1" align="right" style="width: 70px; display: none;">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Mês/Ano:">
                            </dxe:ASPxLabel>
                        </td>
                        <td id="tdEstagio2" align="left" style="width: 95px; display: none;">
                            <dxe:ASPxComboBox ID="ddlAnoMes" runat="server" ClientInstanceName="ddlAnoMes"
                                Width="84px" OnCallback="ddlMes_Callback" ValueType="System.String">
                                <ClientSideEvents EndCallback="function(s, e) {
	ddlMes.SetSelectedIndex(s.cp_index);
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="right" style="width: 80px" id="tdInstitucional1">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Orçamento:">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left" style="width: 280px" id="tdInstitucional2">
                            <dxe:ASPxComboBox ID="ddlOrcamento" runat="server" ClientInstanceName="ddlStatus"
                                 Width="270px" ValueType="System.String">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMes.PerformCallback();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="right" style="width: 30px" id="tdInstitucional3">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Mês:">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left" style="width: 65px" id="tdInstitucional4">
                            <dxe:ASPxComboBox ID="ddlMes" runat="server" ClientInstanceName="ddlMes"
                                Width="65px" OnCallback="ddlMes_Callback" ValueType="System.String">
                                <ClientSideEvents EndCallback="function(s, e) {
	ddlMes.SetSelectedIndex(s.cp_index);
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="left">
                        </td>
                        <td align="left" style="width: 80px">
                            <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                Text="Selecionar">
                                <Paddings Padding="0px" />
                                <ClientSideEvents Click="function(s, e) 
{
	if(dteInicio.GetValue() &gt; dteFim.GetValue())
		window.top.mostraMensagem(&quot;Data de Início não pode ser superior à Data de Término do Período!&quot;, 'Atencao', true, false, null);
	else
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <iframe frameborder="0" name="graficos" scrolling="no" src="Dashboard/dashboard_02.aspx"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>



