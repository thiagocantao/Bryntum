<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="visaoMetas_01.aspx.cs" Inherits="_VisaoObjetivos_VisaoCorporativa_visaoMetas_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
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
                                    <DisabledStyle ForeColor="Black">
                                    </DisabledStyle>
                                </dxnb:ASPxNavBar>
                            </td>
                        </tr>
                    </table>
                    <dxpc:ASPxPopupControl ID="popUpStatusTela" runat="server" CloseAction="CloseButton"
                        HeaderText=" " Height="27px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ShowHeader="False" Width="300px">
                        <ContentStyle HorizontalAlign="Center">
                            <Paddings PaddingTop="20px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Nenhum objetivo a ser apresentado." Font-Bold="False" Font-Italic="False">
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
