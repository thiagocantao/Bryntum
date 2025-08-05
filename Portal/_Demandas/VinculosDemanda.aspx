<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VinculosDemanda.aspx.cs" Inherits="_Demandas_VinculosDemanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
.dxpcControl
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: black;
	background-color: white;
	border: 1px solid #8B8B8B;
	width: 200px;
}
.dxpcContent
{
	color: #010000;
	white-space: normal;
	vertical-align: top;
}
.dxpcContentPaddings 
{
	padding: 9px 12px;
}
        .style2
        {
            width: 165px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Demanda:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxe:ASPxTextBox ID="txtNomeDemanda" runat="server" ClientEnabled="False" 
                         Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Vínculo PDTI:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxe:ASPxComboBox ID="ddlVinculoPDTI" runat="server" DropDownHeight="140px" 
                        DropDownWidth="900px"  
                        IncrementalFilteringMode="Contains" TextFormatString="{1}" Width="100%">
                        <Columns>
                            <dxe:ListBoxColumn Caption="Plano" FieldName="Plano" Width="400px" />
                            <dxe:ListBoxColumn Caption="Projeto" FieldName="Projeto" Width="400px" />
                        </Columns>
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Vínculo LOA:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxe:ASPxComboBox ID="ddlVinculoLOA" runat="server" DropDownHeight="110px" 
                        DropDownWidth="900px"  
                        IncrementalFilteringMode="Contains" PopupVerticalAlign="Above" 
                        TextFormatString="{1}" Width="100%">
                        <Columns>
                            <dxe:ListBoxColumn Caption="Ano" FieldName="AnoOrcamento" Width="60px" />
                            <dxe:ListBoxColumn Caption="Projeto" FieldName="Projeto" Width="800px" />
                        </Columns>
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Vínculo Projeto de TIC:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxe:ASPxComboBox ID="ddlVinculoPojetoTIC" runat="server" 
                        DropDownHeight="140px" DropDownWidth="900px" IncrementalFilteringMode="Contains" PopupVerticalAlign="Above" 
                        TextFormatString="{0}" Width="100%">
                        <Columns>
                            <dxe:ListBoxColumn Caption="Projeto" FieldName="Projeto" Width="600px" />
                            <dxe:ListBoxColumn Caption="Status" FieldName="Status" Width="250px" />
                        </Columns>
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 15px">
                    <table>
                        <tr>
                            <td class="style2">
                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Valor Solicitado (Total):">
                    </dxe:ASPxLabel>
                            </td>
                            <td class="style2">
                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Valor Aprovado (Total):">
                    </dxe:ASPxLabel>
                            </td>
                            <td class="style2">
                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Valor Solicitado (Ano):">
                    </dxe:ASPxLabel>
                            </td>
                            <td class="style2">
                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Valor Aprovado (Ano):">
                    </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" style="padding-right: 10px">
                            <dxe:ASPxSpinEdit runat="server" DecimalPlaces="2" AllowMouseWheel="False" 
                                    Width="100%" DisplayFormatString="n2" 
                                    ClientInstanceName="txtValorSolicitadoTotal" ID="txtValorSolicitadoTotal">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
</dxe:ASPxSpinEdit>

                            </td>
                            <td class="style2" style="padding-right: 10px">
                            <dxe:ASPxSpinEdit runat="server" DecimalPlaces="2" AllowMouseWheel="False" 
                                    Width="100%" DisplayFormatString="n2" 
                                    ClientInstanceName="txtValorAprovadoTotal"  
                                    ID="txtValorAprovadoTotal">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
</dxe:ASPxSpinEdit>

                            </td>
                            <td class="style2" style="padding-right: 10px">
                            <dxe:ASPxSpinEdit runat="server" DecimalPlaces="2" AllowMouseWheel="False" 
                                    Width="100%" DisplayFormatString="n2" 
                                    ClientInstanceName="txtValorSolicitadoAno"  
                                    ID="txtValorSolicitadoAno">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
</dxe:ASPxSpinEdit>

                            </td>
                            <td class="style2" style="padding-right: 10px">
                            <dxe:ASPxSpinEdit runat="server" DecimalPlaces="2" AllowMouseWheel="False" 
                                    Width="100%" DisplayFormatString="n2" ClientInstanceName="txtValorAprovadoAno" 
                                     ID="txtValorAprovadoAno">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
</dxe:ASPxSpinEdit>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                                                    <table cellspacing="0" cellpadding="0" 
                        border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px">
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" 
                                                                        ClientInstanceName="btnSalvarNovoAnexo" Text="Salvar" ID="btnSalvarNovoAnexo" Width="90px">
<ClientSideEvents Click="function(s, e) {
	callbackSalvar.PerformCallback();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px" />
</dxe:ASPxButton>

                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 60px">
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" 
                                                                        ClientInstanceName="btnFecharNovoAnexo" Text="Fechar"  ID="btnFecharNovoAnexo" Width="90px">
<ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}"></ClientSideEvents>
                                                                        <Paddings Padding="0px" />
</dxe:ASPxButton>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
            </tr>
        </table>
    
    </div>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
    </dxcb:ASPxCallback>

 <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" 
        HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" 
        Width="320px"  ID="pcUsuarioIncluido">
     <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>


























 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    </form>
</body>
</html>
