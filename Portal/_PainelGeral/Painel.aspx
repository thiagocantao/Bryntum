<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="Painel.aspx.cs" Inherits="_PainelGeral_Painel" %>

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
                <dxcp:ASPxSplitter ID="ASPxSplitter1" runat="server" FullscreenMode="True" Height="100%" Width="100%">
        <Panes>
            <dxcp:SplitterPane></dxcp:SplitterPane>
            <dxcp:SplitterPane></dxcp:SplitterPane>
        </Panes>
    </dxcp:ASPxSplitter>
            </td>
        </tr>
    </table>
</asp:Content>

