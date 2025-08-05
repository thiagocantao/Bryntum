<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="index.aspx.cs"
    Inherits="_Estrategias_objetivoEstrategico_index" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[
var telaInicialOE = "";
function defineTI()
{ 
    //document.getElementById('oe_desktop').src = telaInicialOE; 
}

// ]]>
</script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: <%=alturaTabela %>;">
        <tr>
            <td style="border-right: #e4e3e4 1px solid; width: 192px" valign="top">
                <iframe id="oe_menu" src="opcoes.aspx?CM=<%=codigoMapa %>&COE=<%=codigoObjetivoEstrategico %>&UN=<%=codigoUnidadeSelecionada %>&UNM=<%=codigoUnidadeMapa %>" width="100%" height="<%=alturaTabela %>"
                    scrolling="no" frameborder="0" name="oe_menu"></iframe>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 100%">
                                <tr style="height:26px">
                                    <td valign="middle">
                                        &nbsp; &nbsp;<dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo"
                                            Font-Bold="True"  Text="Detalhes">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" style="width: 150px" valign="middle">
                                        <dxe:ASPxLabel ID="lblEntidade" runat="server" 
                                            Text="Entidade:" ClientInstanceName="lblEntidade">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" style="width: 120px">
                                        <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                             Width="100%" ValueType="System.Int32" TextFormatString="{0}" IncrementalFilteringMode="Contains">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	document.getElementById('oe_menu').src='opcoes.aspx?COE=' + s.cp_COE + '&amp;UN=' + s.GetValue() + '&amp;UNM=' + s.cp_UNM;
	
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*Voc&#234; est&#225; Visualizando as Informa&#231;&#245;es da Entidade: ' + s.GetText());
}" Init="function(s, e) {
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*Voc&#234; est&#225; Visualizando as Informa&#231;&#245;es da Entidade: ' + s.GetText());
}"></ClientSideEvents>
<Columns>
<dxe:ListBoxColumn Name="siglaUnidade" Width="90px" Caption="Sigla Unidade"></dxe:ListBoxColumn>
<dxe:ListBoxColumn Name="nomeUnidade" Width="350px" Caption="Nome Unidade"></dxe:ListBoxColumn>
</Columns>
</dxe:ASPxComboBox>
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
                <iframe id="oe_desktop"  width="100%"
                    height="<%=alturaTabela %>" scrolling="no" frameborder="0" name="oe_desktop" >
                </iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 192px" valign="top">
            </td>
            <td valign="top">
                &nbsp;<dxe:ASPxLabel ID="lblEntidadeDiferente" runat="server" ClientInstanceName="lblEntidadeDiferente" Font-Bold="True" Font-Italic="True" 
                    Font-Size="7pt" ForeColor="Red">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>
