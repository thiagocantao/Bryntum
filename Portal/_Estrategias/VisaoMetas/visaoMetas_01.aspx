<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="visaoMetas_01.aspx.cs" Inherits="_Portfolios_visaoCorporativa_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>   
    <script type="text/javascript" language="javascript">
        var retornoJanela = 'N';
        var myObject = new Object();
        var novoCodigoMsg = -1;
        var codigoIndicadorModal;
        
        function abreMensagens(codigoIndicador, codigoResponsavel, nomeIndicador)
        {            
            myObject.nomeProjeto = nomeIndicador;

            codigoIndicadorModal = codigoIndicador;

            window.top.showModal("../../Mensagens/EnvioMensagens.aspx?CR=" + codigoResponsavel + "&CO=" + codigoIndicador + "&TA=IN", "Nova Mensagem - " + nomeIndicador, 950, 480, "", myObject);
        }
        
        function atualizaIconeMsg(lParam)
        {
            if(lParam == 'S')
            {
                document.getElementById('ind_' + codigoIndicadorModal).src = '../../imagens/envelopeNormal.png'; 
            }
        }
        
        function mostraMsg(lParam)
        {
            if(lParam == 'S')
            {
                document.getElementById('ind_' + codigoIndicadorModal).style.display = 'none'; 
            }
        }
        
        function abreMensagensNovas(codigoMensagem, codigoIndicador)
        {
            novoCodigoMsg = codigoMensagem;
            codigoIndicadorModal = codigoIndicador;
            window.top.showModal("../../Mensagens/MensagensPainelBordo.aspx?EsconderBotaoNovo=S&TA=IN&CO=" + codigoIndicador, 'Mensagens', 1010, 600, "", null);   
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 50%" valign="top">
                                <dxnb:ASPxNavBar ID="nb01" runat="server" EncodeHtml="False"
                        GroupSpacing="4px" Width="100%" Font-Bold="False">
                                    <GroupContentStyle>
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                    </GroupContentStyle>
                                    <ItemStyle BackColor="White" >
                                    <Paddings Padding="1px" />
                                    </ItemStyle>
                                    <Paddings Padding="5px" />
                                    <GroupHeaderStyle Wrap="True">
                                        <Paddings Padding="2px" PaddingLeft="10px" />
                                    </GroupHeaderStyle>
                                    <GroupDataFields NameField="NomeIndicador" />
                                    <ClientSideEvents ExpandedChanged="function(s, e) {
	if(e.group.GetExpanded())
	{
		var casasDecimais = e.group.items[0].name.split(';')[1];
		var codigoIndicador = e.group.items[0].name.split(';')[0];
		document.getElementById('frm2_' + e.group.name).src = &quot;mt_001.aspx?CI=&quot; + codigoIndicador + &quot;&amp;UN=&quot; + s.cp_CodigoEntidade + &quot;&amp;CD=&quot; + casasDecimais;
	}
}" />
                                    <DisabledStyle ForeColor="Black">
                                    </DisabledStyle>
                                </dxnb:ASPxNavBar>
                            </td>
                            <td valign="top">
                                <dxnb:ASPxNavBar ID="nb02" runat="server" EncodeHtml="False"
                        GroupSpacing="4px" Width="100%">
                                    <GroupContentStyle>
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                    </GroupContentStyle>
                                    <ItemStyle BackColor="White" >
                                    <Paddings Padding="1px" />
                                    </ItemStyle>
                                    <Paddings Padding="5px" />
                                    <GroupHeaderStyle Wrap="True" Font-Bold="False">
                                        <Paddings Padding="2px" PaddingLeft="10px" />
                                    </GroupHeaderStyle>
                                    <GroupDataFields NameField="NomeIndicador" />
                                    <ClientSideEvents ExpandedChanged="function(s, e) {
	if(e.group.GetExpanded())
	{
		var casasDecimais = e.group.items[0].name.split(';')[1];
		var codigoIndicador = e.group.items[0].name.split(';')[0];
		document.getElementById('frm2_' + e.group.name).src = &quot;mt_001.aspx?CI=&quot; + codigoIndicador + &quot;&amp;UN=&quot; + s.cp_CodigoEntidade + &quot;&amp;CD=&quot; + casasDecimais;
	}
}" />
                                    <DisabledStyle ForeColor="Black">
                                    </DisabledStyle>
                                </dxnb:ASPxNavBar>
                            </td>
                        </tr>
                    </table>
                    <dxpc:ASPxPopupControl ID="popUpStatusTela" runat="server" CloseAction="CloseButton"
                        HeaderText=" " Height="27px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ShowHeader="False" Width="272px">
                        <ContentStyle>
                            <Paddings PaddingTop="20px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Nenhuma informa&#231;&#227;o a ser apresentada." Font-Bold="False" Font-Italic="False">
                                </dxe:ASPxLabel>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </dxpc:ASPxPopupControl>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        
    </div>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None"  HeaderText="Análises"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="900px">
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
            <ClientSideEvents Closing="function(s, e) {
	
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table id="pnCallback_grid_DXPEForm_DXEFT" border="0" cellpadding="0" cellspacing="0"
                        style=" width: 100%;  border-collapse: collapse">
                        <tbody>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                        Text="Meta:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxTextBox ID="txtMeta" runat="server" ClientEnabled="False" ClientInstanceName="txtMeta"
                                         Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                        Text="Indicador:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicador"
                                         Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                        Text="An&#225;lise:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientInstanceName="txtAnalise"
                                        Width="100%" ClientEnabled="False" Rows="7">
                                        <ClientSideEvents KeyUp="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="1">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                        Text="Recomenda&#231;&#245;es:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxMemo ID="txtRecomendacoes" runat="server" ClientInstanceName="txtRecomendacoes"
                                         Width="100%" ClientEnabled="False" Rows="7">
                                        <ClientSideEvents KeyUp="function(s, e) {
	
}" />
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" rowspan="1">
                                    <dxe:ASPxLabel ID="lblUltimaAtualizacao" runat="server" ClientInstanceName="lblUltimaAtualizacaoAnalise"
                                        Font-Italic="True" >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" rowspan="2">
                                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                         Text="Fechar" Width="100px">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcDados.Hide();
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle Font-Bold="True" />
        </dxpc:ASPxPopupControl>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	 txtIndicador.SetText(s.cp_Indicador);
	 txtMeta.SetText(s.cp_Meta);
	 txtRecomendacoes.SetText(s.cp_recomendacoes);
     txtAnalise.SetText(s.cp_Analise);
	 lblUltimaAtualizacaoAnalise.SetText(s.cp_DataAnalise);
	 pcDados.SetHeaderText(s.cp_titulo);
     pcDados.Show();
	
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
