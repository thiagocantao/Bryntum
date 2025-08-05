<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" EnableViewState="false" AutoEventWireup="true" CodeFile="visaoCorporativa.aspx.cs" Inherits="_VisaoNE_VisaoExecutiva_visaoCorporativa" Title="Portal da Estratégia" %>
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

    <table style="width:100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Painel Executivo de Obras do Entorno"></asp:Label>
                            <dxcb:ASPxCallback ID="callbackVC" runat="server" 
                                ClientInstanceName="callbackVC" oncallback="callbackVC_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('frmVC').src = document.getElementById('frmVC').src;
}" />
                            </dxcb:ASPxCallback>
                        </td>
                         <td align="right" style="width: 135px;padding-right:5px" >
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" width="100%"
                                Text="Ano de Entrega:"></dxe:ASPxLabel>
                        </td>
                        <td align="left" style="width: 100px;padding-right:5px">
                            <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno"
                                Width="100%"></dxe:ASPxComboBox>
                        </td>
                        <td align="left" style="width: 80px;" >
                            <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                Text="Selecionar">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents Click="function(s, e) 
{
	callbackVC.PerformCallback('AtualizarVC');
}"></ClientSideEvents>
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
                <iframe frameborder="0" name="graficos" scrolling="auto" src="./visaoCorporativa_01.aspx"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

