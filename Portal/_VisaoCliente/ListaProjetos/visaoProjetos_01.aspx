<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="visaoProjetos_01.aspx.cs" Inherits="_VisaoCliente_ListaProjetos_visaoProjetos_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>  
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script> 
    <script type="text/javascript" language="javascript">
        var myObject = new Object();
        var retornoJanela = 'N';
        var NcodigoUnidade, NcodigoProjeto, Niniciais, NcodigoMensagem;
                
        function abreMensagens(codigoProjeto, codigoResponsavel, nomeProjeto, iniciais, codigoUnidade)
        {
            NcodigoUnidade = codigoUnidade;
            NcodigoProjeto = codigoProjeto;
            Niniciais = iniciais;
            
            myObject.nomeProjeto = nomeProjeto; 
            
            window.top.showModal("../ListaProjetos/novaMensagemProjetos.aspx?INI=" + iniciais + "&CP=" + codigoProjeto + "&CRESP=" + codigoResponsavel, 'Mensagens', 710, 230, mudaIconeEnvelope, myObject); 
            
        }
        
        function mudaIconeEnvelope(retornoJanela)
        {
            if(retornoJanela == 'S')
            {
                if(Niniciais == "EN")
                        document.getElementById('EN_' + NcodigoUnidade + '_' + NcodigoProjeto).src = '../../imagens/envelopeNormal.png'; 
                    else
                        document.getElementById(Niniciais + '_' + NcodigoProjeto).src = '../../imagens/envelopeNormal.png'; 
            }
        }
        
        function mostraMsg(retornoJanela)
        {
            if(retornoJanela == 'S')
            {
                document.getElementById('msg_' + NcodigoMensagem).style.display = 'none'; 
            }
        }
        
        function abreMensagensNovas(codigoMensagem)
        {
            NcodigoMensagem = codigoMensagem;
            
             window.top.showModal("../../_VisaoExecutiva/MensagensExecutivo.aspx?CM=" + codigoMensagem, 'Mensagens', 960, 500, mostraMsg, null);            
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 2px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 2px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 49%" valign="top">
                                <dxnb:ASPxNavBar ID="nb01" runat="server" EncodeHtml="False"
                        GroupSpacing="4px" Width="100%" Font-Bold="False">
                                    <GroupContentStyle>
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                    </GroupContentStyle>
                                    <ItemStyle BackColor="White" />
                                    <GroupHeaderStyle Wrap="True">
                                        <Paddings Padding="2px" PaddingLeft="10px" />
                                    </GroupHeaderStyle>
                                    <GroupDataFields NameField="NomeIndicador" />
                                    <ClientSideEvents ExpandedChanged="function(s, e) {
	if(e.group.GetExpanded())
	{
		document.getElementById('frm2_' + e.group.name).src = document.getElementById('frm2_' + e.group.name).link;
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
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
