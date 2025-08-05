<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssociacaoTarefasAlerta.aspx.cs" Inherits="_Projetos_DadosProjeto_novaMensagem" UICulture="pt-BR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<!-- <base target="_self" /> -->
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Tarefas</title>
   
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
                    <dxhf:ASPxHiddenField ID="hiddenField" runat="server" ClientInstanceName="hiddenField">
                    </dxhf:ASPxHiddenField>
        <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>
    
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 457px" valign="top">
                    <dxe:ASPxLabel ID="lblMsg" runat="server" ClientInstanceName="lblMsg"
                        Text="Tarefa:">
                    </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                    <dxcp:ASPxTextBox ID="txtTarefa" runat="server" ClientInstanceName="txtTarefa" 
                        ClientVisible="False"  ReadOnly="True" 
                        Width="100%">
                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                        </ReadOnlyStyle>
                    </dxcp:ASPxTextBox>
                    <dxe:ASPxComboBox ID="ddlTarefas" runat="server" EnableCallbackMode="True" DropDownStyle="DropDown" 
                        ClientInstanceName="ddlTarefas"  TextFormatString="{0}"
                        IncrementalFilteringMode="Contains" ValueType="System.Int32" Width="100%" 
                        TextField="NomeTarefa" ValueField="CodigoTarefa" 
                        onitemrequestedbyvalue="ddlTarefas_ItemRequestedByValue1" 
                        onitemsrequestedbyfiltercondition="ddlTarefas_ItemsRequestedByFilterCondition1">
                        <ItemStyle Wrap="True" />
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                        <Columns>
                        <dxe:ListBoxColumn Caption="Tarefa" FieldName="NomeTarefa" Width="300px" />
                        <dxe:ListBoxColumn Caption="Tarefa Superior" FieldName="TarefaSuperior" Width="200px" />
                    </Columns>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td>
                    <dxrp:ASPxRoundPanel ID="rdpDestinatarios" runat="server" 
                        ClientInstanceName="rdpDestinatarios" EnableClientSideAPI="True" 
                        
                        HeaderText="Destinatários" Width="665px">
                        <HeaderStyle Height="7px" />
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                    <table cellpadding="0" cellspacing="0" ID="Table2" width="100%">
                        <tbody>
                            <tr>
                                <td style="WIDTH: 350px">
                                    <dxe:ASPxLabel ID="lblProjetosDisponivel" runat="server" 
                                         ClientInstanceName="lblProjetosDisponivel" 
                                         Text="Disponíveis:">
                                        <clientsideevents init="function(s, e) {
	UpdateButtons();
}" />
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="center" valign="top">
                                </td>
                                <td style="WIDTH: 350px">
                                    <dxe:ASPxLabel ID="lblProjetosSelecionados" runat="server" 
                                         ClientInstanceName="lblProjetosSelecionados" 
                                         Text="Selecionados:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <dxe:ASPxListBox ID="lbListaDisponiveis" runat="server" 
                                         ClientInstanceName="lbListaDisponiveis" 
                                        EnableClientSideAPI="True" EncodeHtml="False" 
                                        Height="200px" Rows="8" TextField="Descricao" FilteringSettings-ShowSearchUI="true" 
                                        ValueField="Codigo" Width="100%" SelectionMode="Multiple">
                                        <clientsideevents endcallback="function(s, e) {
	UpdateButtons();
}
" selectedindexchanged="function(s, e) {
	UpdateButtons();
}" />
                                        <validationsettings>
                                            <errorimage width="14px">
                                            </errorimage>
                                        </validationsettings>
                                    </dxe:ASPxListBox>
                                </td>
                                <td align="center" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px" 
                                    valign="middle">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <dxe:ASPxButton ID="btnADDTodos0" runat="server"  
                                                        AutoPostBack="False" ClientEnabled="False" ClientInstanceName="btnADDTodos" 
                                                        Text="&gt;&gt;" Width="40px">
                                                        <clientsideevents click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbListaDisponiveis, lbListaSelecionados);
	UpdateButtons();
}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="PADDING-BOTTOM: 3px; PADDING-TOP: 3px">
                                                    <dxe:ASPxButton ID="btnADD0" runat="server"  
                                                        AutoPostBack="False" ClientEnabled="False" ClientInstanceName="btnADD" 
                                                        Text="&gt;" Width="40px">
                                                        <clientsideevents click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaDisponiveis, lbListaSelecionados);
	UpdateButtons();	
}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="PADDING-BOTTOM: 3px">
                                                    <dxe:ASPxButton ID="btnRMV0" runat="server"  
                                                        AutoPostBack="False" ClientEnabled="False" ClientInstanceName="btnRMV" 
                                                        Text="&lt;" Width="40px">
                                                        <clientsideevents click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaSelecionados, lbListaDisponiveis);
	UpdateButtons();
}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <dxe:ASPxButton ID="btnRMVTodos0" runat="server"  
                                                        AutoPostBack="False" ClientEnabled="False" ClientInstanceName="btnRMVTodos" 
                                                        Text="&lt;&lt;" Width="40px">
                                                        <clientsideevents click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbListaSelecionados, lbListaDisponiveis);
	UpdateButtons();
}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td valign="top">
                                    <dxe:ASPxListBox ID="lbListaSelecionados" runat="server" 
                                         ClientInstanceName="lbListaSelecionados" 
                                        EnableClientSideAPI="True" EncodeHtml="False" 
                                        Height="200px" Rows="8" TextField="Descricao" FilteringSettings-ShowSearchUI="true" 
                                        ValueField="Codigo" Width="100%" SelectionMode="Multiple">
                                        <clientsideevents endcallback="function(s, e) {
	UpdateButtons();
}" selectedindexchanged="function(s, e) {
	UpdateButtons();
}" />
                                        <validationsettings>
                                            <errorimage width="14px">
                                            </errorimage>
                                        </validationsettings>
                                    </dxe:ASPxListBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td align="right">
                    <table class="formulario-botoes">
                        <tr>
                            <td class="formulario-botao">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Height="5px"
                                    Text="Salvar" Width="90px"  
                                    AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) 
{
var retorno =     validaCampos();
if(retorno == true)
{
		cbSalvar.PerformCallback();
} 
}" />
                                    <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" 
                                        PaddingBottom="0px">
                                    </Paddings>
                                </dxe:ASPxButton>
                            </td>
                            <td class="formulario-botao">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                    Height="1px" Text="Fechar" Width="90px" >
                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	window.parent.fechaModal();
}" />
                                    <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" 
                                        PaddingBottom="0px">
                                    </Paddings>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:SqlDataSource ID="dsTarefas" runat="server"></asp:SqlDataSource>
    <dxcp:ASPxCallback ID="cbSalvar" runat="server" ClientInstanceName="cbSalvar" 
        oncallback="cbSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) 
{
var erro  = s.cp_ErroSalvar;
if(erro == &quot;OK&quot;)
{
window.top.mostraMensagem(&quot;Dados alterados com sucesso!&quot;, 'sucesso', false, false, null);
window.parent.fechaModal();
}
else if (erro.length &gt; 0 )
{
 window.top.mostraMensagem(&quot;Erro : &quot; + erro, 'erro', true, false, null);
}
else
{
window.top.mostraMensagem(&quot;Dados alterados com sucesso!&quot;, 'sucesso', false, false, null);
window.parent.fechaModal();
}
}" />
    </dxcp:ASPxCallback>
    </form>
</body>
</html>
