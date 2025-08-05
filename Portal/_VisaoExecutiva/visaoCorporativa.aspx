<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" EnableViewState="false"  AutoEventWireup="true" CodeFile="visaoCorporativa.aspx.cs" Inherits="_VisaoExecutiva_visaoCorporativa" Title="Portal da Estratégia" %>
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
    
    function verificaVisao()
    {
        var tipoVisao = ddlOpcaoVisao.GetValue();
        
	    if(tipoVisao == "0")
	    {
		    document.getElementById('frmVC').src = './VisaoCorporativa/visaoCorporativa_01.aspx';	
	    }
	    else
	    {
		    if(tipoVisao == "1")
		    {
                document.getElementById('frmVC').src = '../_Public/Gantt/paginas/projetometa/Default.aspx';
                    //'./VisaoCorporativa/vc_gantt.aspx';
		    }
		    else
		    {
		        if(tipoVisao == "2")
		        {
		            document.getElementById('frmVC').src = '../_Portfolios/VisaoMetas/visaoMetas_01.aspx';	
		        }
		        else
		        {
		            document.getElementById('frmVC').src = './VisaoDemandas/visaoDemandas_01.aspx';
		        }
		    }
	    }	
    }
    
</script>

    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Names="Verdana" Font-Overline="False" Font-Size="12pt" Font-Strikeout="False"
                                Text="Meu Painel de Bordo"></asp:Label></td>
                        <td style="width: 5px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <iframe frameborder="0" name="graficos" scrolling="no" src="<%=telaVisao %>"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

