<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" EnableViewState="false" AutoEventWireup="true" CodeFile="visaoCorporativa.aspx.cs" Inherits="_VisaoEPC_visaoCorporativa" Title="Portal da Estratégia" %>
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
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Painel do Contrato EPC"></asp:Label>
                            <dxcb:ASPxCallback ID="callbackVC" runat="server" 
                                ClientInstanceName="callbackVC" oncallback="callbackVC_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('frmVC').src = document.getElementById('frmVC').src;
}" />
                            </dxcb:ASPxCallback>
                        </td>
                         <td align="right" style="width: 98px;" >
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Periodicidade:"></dxe:ASPxLabel>
                        </td>
                         <td align="left" style="width: 100px;" >
                            <dxe:ASPxComboBox ID="ddlPeriodicidade" runat="server" 
                                 ClientInstanceName="ddlPeriodicidade"
                                Width="95px" SelectedIndex="2">
                                <Items>
                                    <dxe:ListEditItem Text="Mensal" Value="M" />                                  
                                    <dxe:ListEditItem Text="Trimestral" Value="T" />
                                    <dxe:ListEditItem Text="Semestral" Value="S" />
                                    <dxe:ListEditItem Selected="True" Text="Anual" Value="A" />
                                </Items>
                             </dxe:ASPxComboBox>
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

