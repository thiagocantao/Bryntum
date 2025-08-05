<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" EnableViewState="false" AutoEventWireup="true" CodeFile="visaoCorporativa.aspx.cs" Inherits="_Portfolios_visaoCorporativa" Title="Portal da Estratégia" %>
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
            <td align="right">
                <dxcb:ASPxCallback ID="callBackVC" runat="server" ClientInstanceName="callBackVC"
                    OnCallback="callBackVC_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	verificaVisao();
}" />
                </dxcb:ASPxCallback>
            </td>
        </tr>
        <tr>
            
            <td align="right">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td align="left" style="width: 8px">
                        </td>
                    
                        <td align="left" style="width: 140px">
                            <asp:Label ID="lblTitulo" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Monitor de Portfólio"></asp:Label></td>
                        <td align="left">
                            <dxe:ASPxComboBox ID="ddlOpcaoVisao" runat="server" ClientInstanceName="ddlOpcaoVisao"
                                 ValueType="System.String"
                                Width="123px" ShowImageInEditBox="True" SelectedIndex="0">
                                <Items>
                                    <dxe:ListEditItem ImageUrl="~/imagens/graficos.PNG" Text="Gr&#225;ficos"
                                        Value="0" Selected="True" />
                                    <dxe:ListEditItem ImageUrl="~/imagens/botoes/btnGantt.png" Text="Gantt" Value="1" />
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaVisao();
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="left" style="width: 220px">
                            <dxe:ASPxRadioButtonList ID="rbTipoVisao" runat="server"
                                RepeatDirection="Horizontal" SelectedIndex="0" ClientInstanceName="rbTipoVisao">
                                <Paddings Padding="0px" />
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {	
	verificaVisao();
}" />
                                <Items>
                                    <dxe:ListEditItem Text="Geral" Value="G" />
                                    <dxe:ListEditItem Text="Unidade" Value="U" />
                                    <dxe:ListEditItem Text="Categoria" Value="C" />
                                </Items>
                                <Border BorderStyle="None" />
                            </dxe:ASPxRadioButtonList>
                        </td>
                        <td align="right" style="width: 55px">
                            <dxe:ASPxLabel ID="lblFiltro" runat="server" 
                                Text="Portfólio:" ClientInstanceName="lblFiltro">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left" style="width: 290px">
                            <dxe:ASPxComboBox ID="ddlUnidade" runat="server" 
                                Width="285px" ClientInstanceName="ddlUnidade" ClientVisible="False" ValueType="System.String">
                            </dxe:ASPxComboBox><dxe:ASPxComboBox ID="ddlCategoria" runat="server" 
                                Width="285px" ClientInstanceName="ddlCategoria" ClientVisible="False" ValueType="System.String">
                            </dxe:ASPxComboBox>
                            <dxe:ASPxComboBox ID="ddlGeral" runat="server" 
                                Width="285px" ClientInstanceName="ddlGeral" ValueType="System.String">
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
                <iframe frameborder="0" name="graficos" scrolling="vertical" src="./VisaoCorporativa/visaoCorporativa_01.aspx"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
            </td>
        </tr>
    </table>
    <script type='text/javascript' language='Javascript'>
                                 verificaVisao();
                              </script>
</asp:Content>

