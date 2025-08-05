<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdicaoPlano.aspx.cs" Inherits="_PlanosPluri_EdicaoPlano" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" language="javascript">

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            if (callbackSalvar.cp_Status == 'OK') {
                window.top.fechaModal2();
            }
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellspacing="0" class="auto-style1">
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Nome do Plano Plurianual:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxcp:ASPxTextBox ID="txtPlano" runat="server" ClientInstanceName="txtPlano"  MaxLength="250" Width="100%">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                        </ValidationSettings>
                    </dxcp:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server"  Text="Portfólio Associado:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxcp:ASPxComboBox ID="ddlPorfolios" runat="server" ClientInstanceName="ddlPorfolios"  ValueType="System.String" Width="100%">
                    </dxcp:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel3" runat="server"  Text="Plano Plurianual Consolidador:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 15px">
                    <dxcp:ASPxComboBox ID="ddlPlano" runat="server" ClientInstanceName="ddlPlano"  ValueType="System.String" Width="100%">
                    </dxcp:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60px">
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                                    Text="Salvar"  ID="btnSalvar"
                                                                                    Width="90px">
                                                                                    <ClientSideEvents Click="function(s, e) {
callbackSalvar.PerformCallback();	
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td style="width: 60px">
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                    Text="Fechar"  ID="btnFechar" Width="90px">
                                                                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
            </tr>
        </table>
    
    </div>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
        </dxcp:ASPxCallback>

 <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxcp:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxcp:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxcp:ASPxLabel>


























 </td></tr></tbody></table></dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>

    </form>
</body>
</html>
