<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensIndicador.aspx.cs" Inherits="_Estrategias_indicador_MensagensIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);width: 100%" id="tbPrincipal">
        <tr style="height:26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTitulo" runat="server" ClientInstanceName="lblTitulo"
                    Font-Bold="True"  Text="Mensagens"></dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <!-- TABLE DADOS -->
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 5px; padding-left: 5px; padding-top: 5px">
                    <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                        oncallback="pnCallback_Callback" width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Mapa Estrat&#233;gico:"  ID="ASPxLabel10"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="99%" ClientEnabled="False"  ID="txtMapa">
<DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr><tr><td style="PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Objetivo Estrat&#233;gico:"  ID="ASPxLabel1"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="99%" ClientEnabled="False"  ID="txtObjetivoEstrategico">
<DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr><tr><td style="PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Indicador:"  ID="ASPxLabel2"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="99%" ClientEnabled="False"  ID="txtIndicador">
<DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr><tr><td style="PADDING-TOP: 5px"><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus"  ID="lblStatus"></dxe:ASPxLabel>
 </td><td></td><td></td></tr><tr><td style="PADDING-RIGHT: 5px"><dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="194px" ClientInstanceName="ddlStatus"  ID="ddlStatus">
<ClientSideEvents SelectedIndexChanged="function(s, e){
	e.processOnServer = false;
	pnCallback.PerformCallback(&quot;CarregarGrid&quot;);
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Todas" Value="Todas" Selected="True"></dxe:ListEditItem>
<dxe:ListEditItem Text="Respondidas" Value="Respondidas"></dxe:ListEditItem>
<dxe:ListEditItem Text="N&#227;o Respondidas" Value="N&#227;o Respondidas"></dxe:ListEditItem>
</Items>
</dxe:ASPxComboBox>
 </td><td style="PADDING-RIGHT: 5px"><dxe:ASPxCheckBox runat="server" Text="Mensagens que eu enviei" ClientInstanceName="ckbEnviei"  ID="ckbEnviei"  >
<ClientSideEvents CheckedChanged="function(s, e) {
	e.processOnServer = false;
	pnCallback.PerformCallback(&quot;CarregarGrid&quot;);
}"></ClientSideEvents>

<BorderLeft BorderColor="Silver"></BorderLeft>

<BorderTop BorderColor="Silver"></BorderTop>

<BorderRight BorderColor="Silver"></BorderRight>

<BorderBottom BorderColor="Silver"></BorderBottom>
</dxe:ASPxCheckBox>
 </td><td ><dxe:ASPxCheckBox runat="server" Text="Mensagens que eu respondo" ClientInstanceName="ckbRespondo"  ID="ckbRespondo"  >
<ClientSideEvents CheckedChanged="function(s, e) {
	e.processOnServer = false;
	pnCallback.PerformCallback(&quot;CarregarGrid&quot;);
}"></ClientSideEvents>

<BorderLeft BorderColor="Silver"></BorderLeft>

<BorderTop BorderColor="Silver"></BorderTop>

<BorderRight BorderColor="Silver"></BorderRight>

<BorderBottom BorderColor="Silver"></BorderBottom>
</dxe:ASPxCheckBox>
 </td></tr></tbody></table></td></tr><tr><td style="PADDING-TOP: 5px"><!-- Grid gvDados --><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoMensagem" AutoGenerateColumns="False" Width="100%"  ID="gvDados"  OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
<ClientSideEvents FocusedRowChanged="function(s, e) 
{
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e){
	if(e.buttonID == &quot;btnExcluirCustom&quot;)
    {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);

		TipoOperacao = &quot;Excluir&quot;;
		hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
    }
    if(e.buttonID == &quot;btnEditarCustom&quot;)
    {
	    onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);

        TipoOperacao = &quot;Editar&quot;;
		hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
    }
    if(e.buttonID == &quot;btnDetalhesCustom&quot;)
    {
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);

		TipoOperacao = &quot;Consultar&quot;;
		hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
		
		pcDados.Show();
    }
 }"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="col_CustomButtons" Width="100px" Caption="A&#231;&#227;o" VisibleIndex="0">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
<Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.png"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>

<CellStyle HorizontalAlign="Right"></CellStyle>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoMensagem" Name="col_CodMensagem" Caption="Codigo Mensagem" Visible="False" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Name="col_CodigoUsuarioInclusao" Caption="CodigoUsuarioInclusao" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Mensagem" Name="col_Mensagem" Caption="Mensagem" ToolTip="Mensagem que voc&#234; recebeu de alguem" VisibleIndex="1"><DataItemTemplate>
                    <span id="spnMensagem"><%# Eval("Mensagem").ToString().Length > 100 ? Eval("Mensagem").ToString().Substring(0, 99) + "..." : Eval("Mensagem")%>
                    </span>
        
</DataItemTemplate>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Resposta" Name="col_Resposta" Caption="Resposta" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataResposta" Name="col_RespondidaEm" Width="90px" Caption="Respondida Em" VisibleIndex="3">
<PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}"></PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Name="col_DataInclusao" Caption="Data Inclus&#227;o" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="IndicaRespostaNecessaria" Name="col_IndicaRespostaNecessaria" Caption="Indica Resposta Necessaria" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="col_CodigoObjetoAssociado" Caption="Codigo Objeto Associado" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataLimiteResposta" Name="col_DataLimiteResposta" Caption="Data Limite Resposta" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResposta" Name="col_RespondidaPor" Width="140px" Caption="Respondida Por" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Name="col_EnviadaEm" Width="90px" Caption="Enviada Em" VisibleIndex="2">
<PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}"></PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao" Name="col_EnviadaPor" Width="140px" Caption="Enviada Por" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Name="col_NomeProjeto" Caption="Nome Projeto" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="ExcluiMensagem" Name="col_podeExcluirMensagem" Caption="Pode Excluir Mensagem" Visible="False" VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="EditaMensagem" Name="col_podeEditarMensagem" Caption="pode Editar Mensagem" Visible="False" VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="EditaResposta" Name="col_podeEditarResposta" Caption="pode Editar Resposta" Visible="False" VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna" ></SettingsText>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxwgv:ASPxGridView>
 </td></tr></tbody></table><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral" ></dxhf:ASPxHiddenField>
 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px"  ID="pcDados" >
<ClientSideEvents PopUp="function(s, e){
	OnGridFocusedRowChanged(gvDados);
}"></ClientSideEvents>

<ContentStyle>
<Paddings Padding="3px" PaddingTop="10px" PaddingRight="10px"></Paddings>
</ContentStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><dxp:ASPxPanel runat="server" ClientInstanceName="pnFormulario" Width="100%" ID="pnFormulario" ><PanelCollection>
<dxp:PanelContent runat="server"><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnBotaoResponder" Width="100%" ID="pnBotaoResponder"  OnCallback="pnBotaoResponder_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Mensagem:" ClientInstanceName="lblMsg"  ID="lblMsg" ></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="70px" Width="100%" ClientInstanceName="txtMensagem" EnableClientSideAPI="True"  ID="txtMensagem" >
<ClientSideEvents KeyUp="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" Init="function(s, e) 
{ 
	
}"></ClientSideEvents>

<ReadOnlyStyle BackColor="WhiteSmoke"></ReadOnlyStyle>

<DisabledStyle BackColor="WhiteSmoke" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Resposta:" ClientInstanceName="lblMsg"  ID="ASPxLabel8" ></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="70px" Width="100%" ClientInstanceName="txtResposta" EnableClientSideAPI="True"  ID="txtResposta" >
<ClientSideEvents KeyPress="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" Init="function(s, e) 
{
	
}"></ClientSideEvents>

<DisabledStyle BackColor="WhiteSmoke" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align=right><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><dxe:ASPxButton runat="server" ClientInstanceName="btnResponder" Text="Responder" Width="90px" Height="5px"  ID="btnResponder" >
<ClientSideEvents Click="function(s, e) 
{
    e.processOnServer = false; 

	if(txtResposta.GetText() == &quot;&quot;)
	{
		window.top.mostraMensagem('Resposta da mensagem deve ser informada', 'atencao', true, false, null);	
	}
	else
	{
		pnCallback.PerformCallback(&quot;Responder&quot;);
	}
}" CheckedChanged="function(s, e) 
{

}"></ClientSideEvents>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>



 <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px" Height="5px"  ID="btnSalvar" >
<ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>



 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" Height="1px"  ID="btnFechar" >
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>



 </td></tr></tbody></table></td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>


 </dxp:PanelContent>
</PanelCollection>
</dxp:ASPxPanel>

 </dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
		if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
gvDados.PerformCallback();
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
