<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="visaoCorporativa.aspx.cs" Inherits="_VisaoAnalista_visaoCorporativa" Title="Portal da Estratégia" %>
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
    
        
</script>

    <table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Meu Painel de Bordo"></asp:Label></td>
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
                <iframe frameborder="0" name="graficos" scrolling="auto" src="./Graficos/visaoCorporativa_01.aspx"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

