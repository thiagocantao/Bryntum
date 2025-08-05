<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaMensagens.aspx.cs" Inherits="_Projetos_DadosProjeto_editaMensagens" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
<script type="text/javascript" language="javascript">
    
        var TipoOperacao;
        
   function abreTelaNovaMensagem()
    {
        var codProj = window.parent.hfGeral.Get('hfCodigoProjeto');
	    var nomeProj = window.parent.hfGeral.Get('hfNomeProjeto');

        var myObject = new Object();
        
        myObject.nomeProjeto = nomeProj; 

        window.top.showModal("novaMensagem.aspx?CP=" + codProj, "Nova Mensagem", 720, 450, funcaoPosModal, myObject);
    }
   
    function funcaoPosModal()
    {
        gvDados.PerformCallback();
    }
   
    
</script>
<link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
</head>
<body  class="body">
    <form id="form1" runat="server">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="WIDTH: 10px; HEIGHT: 10px"></td><td style="HEIGHT: 10px"></td></tr><tr><td style="WIDTH: 10px"></td><td>
    <dxe:ASPxLabel ID="lblNomeProjeto" runat="server" Font-Bold="True" 
        >
    </dxe:ASPxLabel>
    </td></tr><tr><td style="WIDTH: 10px; height: 5px;"></td><td style="HEIGHT: 5px">
 </td></tr><tr><td style="WIDTH: 10px; "></td><td>
        <dxrp:ASPxRoundPanel ID="pnBusca" runat="server" ClientInstanceName="pnBusca" 
            ShowHeader="False" View="GroupBox">
            <contentpaddings padding="0px" />
            <panelcollection>
                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <table border="0" cellpadding="0" cellspacing="0" style="WIDTH: 100%">
                        <tbody>
                            <tr>
                                <td style="WIDTH: 216px">
                                    <dxe:ASPxLabel ID="lblStatus" runat="server" ClientInstanceName="lblStatus" 
                                         Text="Status:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="WIDTH: 200px">
                                </td>
                                <td style="WIDTH: 200px">
                                </td>
                                <td style="width: 200px">
                                </td>
                            </tr>
                            <tr>
                                <td style="WIDTH: 216px">
                                    <dxe:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus" 
                                         SelectedIndex="0" Width="194px">
                                        <clientsideevents selectedindexchanged="function(s, e) 
{
 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);	
}" />
                                        <Items>
                                            <dxe:ListEditItem Selected="True" Text="Todas" Value="Todas" />
                                            <dxe:ListEditItem Text="Respondidas" Value="Respondidas" />
                                            <dxe:ListEditItem Text="Não Respondidas" Value="Não Respondidas" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="WIDTH: 200px">
                                    <dxe:ASPxCheckBox ID="ckbEnviei" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckbEnviei"  
                                        Text="Mensagens que eu enviei">
                                        <clientsideevents checkedchanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}" />
                                        <borderleft bordercolor="Silver" />
                                        <bordertop bordercolor="Silver" />
                                        <borderright bordercolor="Silver" />
                                        <borderbottom bordercolor="Silver" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="WIDTH: 200px">
                                    <dxe:ASPxCheckBox ID="ckbRespondo" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckbRespondo"  
                                        Text="Mensagens que eu respondo">
                                        <clientsideevents checkedchanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}" />
                                        <borderleft bordercolor="Silver" />
                                        <bordertop bordercolor="Silver" />
                                        <borderright bordercolor="Silver" />
                                        <borderbottom bordercolor="Silver" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="width: 200px">
                                    <dxe:ASPxCheckBox ID="ckbRespostaNecessaria" runat="server" 
                                        CheckState="Unchecked" ClientInstanceName="ckbRespostaNecessaria" 
                                         Text="Resposta necessária">
                                        <clientsideevents checkedchanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}" />
                                        <borderleft bordercolor="Silver" />
                                        <bordertop bordercolor="Silver" />
                                        <borderright bordercolor="Silver" />
                                        <borderbottom bordercolor="Silver" />
                                    </dxe:ASPxCheckBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </panelcollection>
            <borderleft bordercolor="White" />
            <bordertop bordercolor="White" />
            <borderright bordercolor="White" />
            <borderbottom bordercolor="White" />
        </dxrp:ASPxRoundPanel>
        </td></tr><tr><td style="WIDTH: 10px; height: 10px;"></td>
        <td style="HEIGHT: 10px">
 </td></tr><tr><td style="WIDTH: 10px; "></td><td>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" 
            ClientInstanceName="pcDados"  
            HeaderText="Detalhes" Modal="True" PopupAction="None" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            ShowCloseButton="False" Width="700px">
            <clientsideevents closing="function(s, e) {

}" popup="function(s, e) 
{
	OnGridFocusedRowChanged(gvDados);
}" />
            <contentcollection>
                <dxpc:PopupControlContentControl runat="server" 
                    SupportsDisabledAttribute="True">
                    <dxp:ASPxPanel ID="pnFormulario" runat="server" 
                        ClientInstanceName="pnFormulario" Width="100%">
                        <panelcollection>
                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dxcp:ASPxCallbackPanel ID="pnBotaoResponder" runat="server" 
                                    ClientInstanceName="pnBotaoResponder" OnCallback="pnBotaoResponder_Callback" 
                                    Width="100%">
                                    <panelcollection>
                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="lblMsg" runat="server" ClientInstanceName="lblMsg" 
                                                                 Text="Mensagem:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientInstanceName="txtMensagem" 
                                                                EnableClientSideAPI="True"  Height="70px" 
                                                                Width="665px">
                                                                <clientsideevents init="function(s, e) 
{ 
	
}" keyup="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" />
                                                                <readonlystyle backcolor="WhiteSmoke">
                                                                </readonlystyle>
                                                                <disabledstyle backcolor="WhiteSmoke" forecolor="Black">
                                                                </disabledstyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblMsg" 
                                                                 Text="Resposta:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo ID="txtResposta" runat="server" ClientInstanceName="txtResposta" 
                                                                EnableClientSideAPI="True"  Height="70px" 
                                                                Width="665px">
                                                                <clientsideevents init="function(s, e) 
{
	
}" keypress="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" />
                                                                <disabledstyle backcolor="WhiteSmoke" forecolor="Black">
                                                                </disabledstyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btnResponder" runat="server" 
                                                                                ClientInstanceName="btnResponder"  
                                                                                Height="5px" Text="Responder" Width="90px">
                                                                                <clientsideevents checkedchanged="function(s, e) 
{

}" click="function(s, e) 
{
    e.processOnServer = false; 

	if(txtResposta.GetText() == &quot;&quot;)
	{
		window.top.mostraMensagem(&quot;Resposta da mensagem deve ser informada&quot;, 'atencao', true, false, null);
	}
	else
	{
		pnCallback.PerformCallback(&quot;Responder&quot;);
	}
}" />
                                                                                <paddings paddingbottom="0px" paddingleft="0px" paddingright="0px" 
                                                                                    paddingtop="0px" />
                                                                            </dxe:ASPxButton>
                                                                            <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" 
                                                                                 Height="5px" Text="Salvar" Width="90px">
                                                                                <clientsideevents click="function(s, e) 
{
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                <paddings paddingbottom="0px" paddingleft="0px" paddingright="0px" 
                                                                                    paddingtop="0px" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="WIDTH: 10px">
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" 
                                                                                 Height="1px" Text="Fechar" Width="90px">
                                                                                <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                <paddings paddingbottom="0px" paddingleft="0px" paddingright="0px" 
                                                                                    paddingtop="0px" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </panelcollection>
                                </dxcp:ASPxCallbackPanel>
                            </dxp:PanelContent>
                        </panelcollection>
                    </dxp:ASPxPanel>
                </dxpc:PopupControlContentControl>
            </contentcollection>
        </dxpc:ASPxPopupControl>
        </td></tr><tr><td style="WIDTH: 10px"></td><td>
            <div id="divGrid" style="visibility:hidden">
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
            ClientInstanceName="gvDados"  
            KeyFieldName="CodigoMensagem" 
            OnAfterPerformCallback="gvDados_AfterPerformCallback" 
            OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="98%">
            <clientsideevents custombuttonclick="function(s, e) 
 {
	if(e.buttonID == &quot;btnExcluirCustom&quot;)
    {
		TipoOperacao = &quot;Excluir&quot;;        
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
    
    }
    if(e.buttonID == &quot;btnEditarCustom&quot;)
    {
        TipoOperacao = &quot;Editar&quot;;        
	    onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);       
    }
    if(e.buttonID == &quot;btnDetalhesCustom&quot;)
    {
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();    
    }
 }" focusedrowchanged="function(s, e) 
{
	OnGridFocusedRowChanged(s);
}" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"
/>
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="Ação" 
                    Name="col_CustomButtons" ShowInCustomizationForm="True" VisibleIndex="0" 
                    Width="100px">
                    <custombuttons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                            <image url="~/imagens/botoes/excluirReg02.PNG">
                            </image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                            <image alternatetext="Editar" url="~/imagens/botoes/editarReg02.PNG">
                            </image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                            <image url="~/imagens/botoes/pFormulario.png">
                            </image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                    </custombuttons>
                    <cellstyle horizontalalign="Center">
                    </cellstyle>
                    <HeaderTemplate>
                        <%# (IncluiMsg) ? "<img src='../../imagens/botoes/incluirReg02.png' style='cursor: pointer' onclick='abreTelaNovaMensagem();gvDados.PerformCallback();' />" : "<img style='cursor:default' src='../../imagens/botoes/incluirRegDes.png' />"%>
                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Codigo Mensagem" 
                    FieldName="CodigoMensagem" Name="col_CodMensagem" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoUsuarioInclusao" 
                    FieldName="CodigoUsuarioInclusao" Name="col_CodigoUsuarioInclusao" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mensagem" FieldName="Mensagem" 
                    Name="col_Mensagem" ShowInCustomizationForm="True" 
                    ToolTip="Mensagem que você recebeu de alguem" VisibleIndex="1">
                    <dataitemtemplate>
                        <span ID="spnMensagem">
                        <%# Eval("Mensagem").ToString().Length > 100 ? Eval("Mensagem").ToString().Substring(0, 99) + "..." : Eval("Mensagem")%>
                        </span>
                    </dataitemtemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Resposta" FieldName="Resposta" 
                    Name="col_Resposta" ShowInCustomizationForm="True" Visible="False" 
                    VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Respondida Em" FieldName="DataResposta" 
                    Name="col_RespondidaEm" ShowInCustomizationForm="True" VisibleIndex="3" 
                    Width="120px">
                    <propertiestextedit displayformatstring="{0: dd/MM/yyyy HH:mm}"></propertiestextedit>
                    <HeaderStyle HorizontalAlign="Center" />
                    <cellstyle horizontalalign="Center">
                    </cellstyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Data Inclusao" FieldName="DataInclusao" 
                    Name="col_DataInclusao" ShowInCustomizationForm="True" Visible="False" 
                    VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Indica Resposta Necessaria" 
                    FieldName="IndicaRespostaNecessaria" Name="col_IndicaRespostaNecessaria" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Codigo Objeto Associado" 
                    FieldName="CodigoObjetoAssociado" Name="col_CodigoObjetoAssociado" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Data Limite Resposta" 
                    FieldName="DataLimiteResposta" Name="col_DataLimiteResposta" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Respondida Por" 
                    FieldName="NomeUsuarioResposta" Name="col_RespondidaPor" 
                    ShowInCustomizationForm="True" VisibleIndex="5" Width="180px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Enviada Em" FieldName="DataInclusao" 
                    Name="col_EnviadaEm" ShowInCustomizationForm="True" VisibleIndex="2" 
                    Width="110px">
                    <propertiestextedit displayformatstring="{0: dd/MM/yyyy HH:mm}"></propertiestextedit>
                    <HeaderStyle HorizontalAlign="Center" />
                    <cellstyle horizontalalign="Center">
                    </cellstyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Enviada Por" FieldName="UsuarioInclusao" 
                    Name="col_EnviadaPor" ShowInCustomizationForm="True" VisibleIndex="4" 
                    Width="180px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nome Projeto" FieldName="NomeProjeto" 
                    Name="col_NomeProjeto" ShowInCustomizationForm="True" Visible="False" 
                    VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Pode Excluir Mensagem" 
                    FieldName="ExcluiMensagem" Name="col_podeExcluirMensagem" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="pode Editar Mensagem" 
                    FieldName="EditaMensagem" Name="col_podeEditarMensagem" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="pode Editar Resposta" 
                    FieldName="EditaResposta" Name="col_podeEditarResposta" 
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <settingsbehavior allowfocusedrow="True" />
            <settingspager mode="ShowAllRecords" visible="False">
            </settingspager>
            <settings VerticalScrollBarMode="Visible" />
            <settingstext grouppanel="Arraste um cabeçalho da coluna aqui para agrupar por essa coluna" />
            <paddings paddingbottom="0px" paddingleft="0px" paddingright="0px" 
                paddingtop="0px" />
        </dxwgv:ASPxGridView>
            </div>
        
 </td></tr><tr><td style="WIDTH: 10px"></td><td>
 </td></tr></tbody></table><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hiddenField" ID="hiddenField"></dxhf:ASPxHiddenField>
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
        &nbsp;
    </form>
</body>
</html>
