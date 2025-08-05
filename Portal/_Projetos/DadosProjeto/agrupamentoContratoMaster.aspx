<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="agrupamentoContratoMaster.aspx.cs" Inherits="_Projetos_DadosProjeto_agrupamentoContratoMaster" %>
   <%@ MasterType VirtualPath="~/novaCdis.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Atualização de Contratos de Grande Porte" 
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="width: 350px; height: 5px;">
                &nbsp;
            </td>
            <td style="width: 350px; height: 5px;">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="CCBM:" 
                    Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="IMPSA:"
                    Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxe:ASPxSpinEdit ID="txtCCBM" runat="server" ClientInstanceName="txtCCBM" 
                     Height="21px" Number="0" Width="100%" 
                    DecimalPlaces="2" DisplayFormatString="c2" NullText="0">
                    <SpinButtons ShowIncrementButtons="False">
                    </SpinButtons>
                </dxe:ASPxSpinEdit>
            </td>
            <td>
                <dxe:ASPxSpinEdit ID="txtIMPSA" runat="server" ClientInstanceName="txtIMPSA" 
                     Height="21px" Number="0" Width="100%" 
                    DecimalPlaces="2" DisplayFormatString="c2" NullText="0">
                    <SpinButtons ShowIncrementButtons="False">
                    </SpinButtons>
                </dxe:ASPxSpinEdit>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td >
                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="EPBM:" 
                    Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
            <td >
                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="ELM:" 
                    Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxe:ASPxSpinEdit ID="txtEPBM" runat="server" ClientInstanceName="txtEPBM" 
                     Height="21px" Number="0" Width="100%" 
                    DecimalPlaces="2" DisplayFormatString="c2" NullText="0">
                    <SpinButtons ShowIncrementButtons="False">
                    </SpinButtons>
                </dxe:ASPxSpinEdit>
            </td>
            <td>
                <dxe:ASPxSpinEdit ID="txtELM" runat="server" ClientInstanceName="txtELM" 
                     Height="21px" Number="0" Width="100%" 
                    DecimalPlaces="2" DisplayFormatString="c2" NullText="0">
                    <SpinButtons ShowIncrementButtons="False">
                    </SpinButtons>
                </dxe:ASPxSpinEdit>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td align="right" >
                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Text="Salvar"
                     AutoPostBack="False" Width="110px">
                    <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback();		
}
" />
<ClientSideEvents Click="function(s, e) {
	callback.PerformCallback();
		
}
"></ClientSideEvents>
                </dxe:ASPxButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
 mensagem = s.cp_MensagemErro; 
 posSalvarComSucesso(mensagem);
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center">
                            </td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>
