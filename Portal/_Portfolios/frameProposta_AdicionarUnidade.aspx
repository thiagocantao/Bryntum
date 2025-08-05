<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_AdicionarUnidade.aspx.cs" Inherits="_Portfolios_frameProposta_AdicionarUnidade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;
        function conteudoCampoAlterado() 
        {
            if (  ( rblCronograma.readOnly == undefined ) || ( rblCronograma.readOnly == false) )
                existeConteudoCampoAlterado = true;
        }
    </script> 
    

</head>

<body  onload="verificaSelecao(rblCronograma);">
    <form id="form1" runat="server">
    <div>
    
<table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
<td></td>
                <td>
                    <dxe:aspxlabel id="lblNomeProjeto" runat="server" clientinstancename="lblNomeProjeto"
                         text="Projeto:"></dxe:aspxlabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td>
                    <dxe:aspxtextbox id="txtNomeProjeto" runat="server" 
                                     width="100%" clientenabled="False" clientinstancename="txtNomeProjeto" 
                                     backcolor="#EBEBEB">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                    </dxe:aspxtextbox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td style="height: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>

                <td>
                    <table>
                        <tr>
                            <td align="left" valign="top" style="width: 400px">
                                <dxe:aspxlabel id="lblUnidade" runat="server" clientinstancename="lblUnidade"
                                    text="Unidade:"></dxe:aspxlabel>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 220px">
                                <dxe:aspxlabel id="lblGerenteProjeto" runat="server" clientinstancename="lblGerenteProjeto"
                                     text="Gerente do Projeto:"></dxe:aspxlabel>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" style="width: 120px" valign="top">
                                <dxe:aspxlabel id="lblDataInicio" runat="server" clientinstancename="lblDataInicio"
                                     text="Inicio:"></dxe:aspxlabel>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top">
                                <dxe:aspxlabel id="lblDataTermino" runat="server" clientinstancename="lblDataTermino"
                                     text="Término:"></dxe:aspxlabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <dxe:aspxcombobox id="ddlUnidadeNegocio" runat="server" clientinstancename="ddlUnidadeNegocio"
                                      width="100%" ValueType="System.String">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxcombobox>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top">
                                <dxe:aspxcombobox id="ddlGerenteProjeto" runat="server" clientinstancename="ddlGerenteProjeto"
                                      width="100%" ValueType="System.String">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxcombobox>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top">
                                <dxe:aspxdateedit id="deInicioProposta" runat="server" clientinstancename="deInicioProposta" editformat="Custom" editformatstring="dd/MM/yyyy"  Width="100%">
<ClientSideEvents ValueChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxdateedit>
                            </td>
                            <td align="left" valign="top">
                            </td>
                            <td align="left" valign="top">
                                <dxe:aspxdateedit id="deTerminoProposta" runat="server" clientinstancename="deTerminoProposta" editformat="Custom" editformatstring="dd/MM/yyyy"  Width="120px">
<ClientSideEvents ValueChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxdateedit>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td style="width: 10px; height: 10px;"></td>
                <td style="height: 10px">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
<td></td>
                <td>
                    <dxe:aspxlabel id="lblNomeCategoria" runat="server" clientinstancename="lblNomeCategoria"
                        text="Categoria:" ></dxe:aspxlabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td>
                    <dxe:aspxtextbox id="txtNomeCategoria" runat="server" 
                        width="100%" clientenabled="False" clientinstancename="txtNomeCategoria" backcolor="#EBEBEB">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxtextbox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td style="height: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td>
                    <dxe:ASPxRadioButtonList ID="rblCronograma" runat="server"
                        ItemSpacing="10px" RepeatDirection="Horizontal" ClientInstanceName="rblCronograma" Width="400px">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	
    conteudoCampoAlterado();
	verificaSelecao(s);	
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Associar Cronograma Existente" Value="ACE"></dxe:ListEditItem>
</Items>
</dxe:ASPxRadioButtonList>
                    <dxe:aspxradiobuttonlist id="ASPxRadioButtonList1" runat="server" clientinstancename="rblCronograma"
                         itemspacing="10px" repeatdirection="Horizontal"
                        visible="False">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	
    conteudoCampoAlterado();
	verificaSelecao(s);	
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Associar Cronograma Existente" Value="ACE"></dxe:ListEditItem>
<dxe:ListEditItem Text="Novo Cronograma em Branco" Value="NCB"></dxe:ListEditItem>
<dxe:ListEditItem Text="Novo Cronograma a Partir de Modelo" Value="NCM"></dxe:ListEditItem>
</Items>
</dxe:aspxradiobuttonlist>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td style="height: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td>
                    <table>
                        <tr>
                            <td id="CE1" style="width: 450px;">
                                <dxe:aspxlabel id="lblAsCroExistente" runat="server" clientinstancename="lblAsCroExistente"
                                     text="Associar Cronograma Existente:"></dxe:aspxlabel>
                            </td>
                            <td>
                            </td>
                            <td id="NC1">
                                <dxe:aspxlabel id="lblNoCroBranco" runat="server" clientinstancename="lblNoCroBranco"
                                     text="Novo Cronograma em Branco:"></dxe:aspxlabel>
                            </td>
                            <td>
                            </td>
                            <td id="CM1">
                                <dxe:aspxlabel id="lblNoCroModelo" runat="server" clientinstancename="lblNoCroModelo"
                                     text="Novo Cronograma de Modelo:"></dxe:aspxlabel>
                            </td>
                        </tr>
                        <tr>
                            <td id="CE2">
                                <dxe:aspxcombobox id="ddlAssoCroExistente" runat="server" clientinstancename="ddlAssoCroExistente"
                                     width="400px" ValueType="System.String">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxcombobox>
                            </td>
                            <td>
                            </td>
                            <td id="NC2">
                                <dxe:aspxcombobox id="ddlNoCroBranco" runat="server" clientinstancename="ddlNoCroBranco"
                                     width="100%" ValueType="System.String">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxcombobox>
                            </td>
                            <td>
                            </td>
                            <td id="CM2">
                                <dxe:aspxcombobox id="ddlNoCroModelo" runat="server" clientinstancename="ddlNoCroModelo"
                                     width="100%" ValueType="System.String">
<ClientSideEvents BeginCallback="function(s, e) {
	conteudoCampoAlterado();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:aspxcombobox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
<td></td>
                <td style="height: 10px">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
<td></td>
                <td align="left">
                                <dxe:aspxbutton id="btnSalvar" runat="server" clientinstancename="btnSalvar"
                                    text="Salvar" width="90px" onclick="btnSalvar_Click" AutoPostBack="False">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents Click="function(s, e) {
	e.processOnServer = validaCampos();
}"></ClientSideEvents>
</dxe:aspxbutton>
                    <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>
                </td>
                <td align="left">
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
