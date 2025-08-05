<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mensagensObjetivoEstrategico.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_mensagensObjetivoEstrategico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
<script type="text/javascript" language="javascript">
    
        var TipoOperacao;
        var myObject = new Object();
        
        function abreTelaNovaMensagem()
        {
            var codOE = hfGeral.Get('hfCodigoObjetivo');
	        var nomeOE = hfGeral.Contains('hfNomeObjetivo') ? hfGeral.Get('hfNomeObjetivo'): "";

            
            myObject.nomeProjeto = nomeOE;

            window.top.showModal("../../Mensagens/EnvioMensagens.aspx?CO=" + codOE + "&TA=OB", "Nova Mensagem - " + nomeOE, 950, 510, "", myObject);
        }
        
        function atualizaGrid()
        {
            gvDados.PerformCallback();
        }
    
</script>
<link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
</head>
<body  class="body">
    <form id="form1" runat="server">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%" HideContentOnCallback="False"  ><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody>
    <tr>
        <td style="width: 5px">
        </td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server"  Text="Mapa Estrat&#233;gico:"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td style="width: 209px">
                                    <asp:Label ID="lblPerspectiva" runat="server" 
                                        Text="Perspectiva:"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td style="width: 220px">
                                    <asp:Label ID="Label6" runat="server"  Text="Tema:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa" 
                                        ReadOnly="True" Width="100%">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td style="width: 220px">
                                    <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                         ReadOnly="True" Width="100%">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td style="width: 220px">
                                    <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema" 
                                        ReadOnly="True" Width="100%">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
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
                        <table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td><asp:Label id="lblObjetivoEstrategico" runat="server" Text="Objetivo Estratégico:" ></asp:Label></td><td 
style="WIDTH: 10px"></td><td style="WIDTH: 280px"><asp:Label id="Label3" runat="server" Text="Responsável:" ></asp:Label></td></tr><tr><td><dxe:ASPxTextBox id="txtObjetivoEstrategico" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                    <ReadOnlyStyle BackColor="#E0E0E0">
                    </ReadOnlyStyle>
                </dxe:ASPxTextBox></td><td style="WIDTH: 10px"></td><td 
style="WIDTH: 280px"><dxe:ASPxTextBox id="txtResponsavel" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                <ReadOnlyStyle BackColor="#E0E0E0">
                                </ReadOnlyStyle>
                            </dxe:ASPxTextBox></td></tr></tbody></table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
            </table>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
     <td style="width: 5px">
     </td>
     <td><dxe:ASPxLabel runat="server" Font-Bold="True"  ID="lblDescricaoObjetivoEstrategico"></dxe:ASPxLabel>
 </td>
        <td>
        </td>
    </tr><tr>
     <td style="width: 5px; height: 5px">
     </td>
     <td style="HEIGHT: 5px"></td>
     <td>
     </td>
 </tr><tr>
     <td style="width: 5px">
     </td>
     <td><dxrp:ASPxRoundPanel runat="server" ShowHeader="False" View="GroupBox" ClientInstanceName="pnBusca" ID="pnBusca"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td style="WIDTH: 216px"><dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus"  ID="lblStatus"></dxe:ASPxLabel>

 </td><td style="WIDTH: 200px"></td><td style="WIDTH: 200px"></td></tr><tr><td style="WIDTH: 216px"><dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="194px" ClientInstanceName="ddlStatus"  ID="ddlStatus">
<ClientSideEvents SelectedIndexChanged="function(s, e) 
{
 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);	
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Todas" Value="Todas" Selected="True"></dxe:ListEditItem>
<dxe:ListEditItem Text="Respondidas" Value="Respondidas"></dxe:ListEditItem>
<dxe:ListEditItem Text="N&#227;o Respondidas" Value="N&#227;o Respondidas"></dxe:ListEditItem>
</Items>
</dxe:ASPxComboBox>

 </td><td style="WIDTH: 200px"><dxe:ASPxCheckBox runat="server" Text="Mensagens que eu enviei" ClientInstanceName="ckbEnviei"  ID="ckbEnviei">
<ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}"></ClientSideEvents>

<BorderLeft BorderColor="Silver"></BorderLeft>

<BorderTop BorderColor="Silver"></BorderTop>

<BorderRight BorderColor="Silver"></BorderRight>

<BorderBottom BorderColor="Silver"></BorderBottom>
</dxe:ASPxCheckBox>

 </td><td style="WIDTH: 200px"><dxe:ASPxCheckBox runat="server" Text="Mensagens que eu respondo" ClientInstanceName="ckbRespondo"  ID="ckbRespondo">
<ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}"></ClientSideEvents>

<BorderLeft BorderColor="Silver"></BorderLeft>

<BorderTop BorderColor="Silver"></BorderTop>

<BorderRight BorderColor="Silver"></BorderRight>

<BorderBottom BorderColor="Silver"></BorderBottom>
</dxe:ASPxCheckBox>

 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>

<BorderLeft BorderColor="White"></BorderLeft>

<BorderTop BorderColor="White"></BorderTop>

<BorderRight BorderColor="White"></BorderRight>

<BorderBottom BorderColor="White"></BorderBottom>
     <ContentPaddings Padding="0px" />
</dxrp:ASPxRoundPanel>
 </td>
         <td>
         </td>
     </tr><tr>
     <td style="width: 5px; height: 10px">
     </td>
     <td style="HEIGHT: 10px"></td>
     <td style="height: 10px">
     </td>
 </tr><tr>
     <td style="width: 5px">
     </td>
     <td><dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px"  ID="pcDados">
<ClientSideEvents Closing="function(s, e) {

}" PopUp="function(s, e) 
{
	OnGridFocusedRowChanged(gvDados);
}"></ClientSideEvents>
<ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"><dxp:ASPxPanel runat="server" ClientInstanceName="pnFormulario" Width="100%" ID="pnFormulario"><PanelCollection>
<dxp:PanelContent runat="server"><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnBotaoResponder" Width="100%" ID="pnBotaoResponder"  OnCallback="pnBotaoResponder_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Mensagem:" ClientInstanceName="lblMsg"  ID="lblMsg" ></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="70px" Width="665px" ClientInstanceName="txtMensagem" EnableClientSideAPI="True"  ID="txtMensagem" >
<ClientSideEvents KeyUp="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" Init="function(s, e) 
{ 
	
}"></ClientSideEvents>

<ReadOnlyStyle BackColor="WhiteSmoke"></ReadOnlyStyle>

<DisabledStyle BackColor="WhiteSmoke" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Resposta:" ClientInstanceName="lblMsg"  ID="ASPxLabel2" ></dxe:ASPxLabel>



 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="70px" Width="665px" ClientInstanceName="txtResposta" EnableClientSideAPI="True"  ID="txtResposta" >
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
</td>
         <td>
         </td>
     </tr><tr>
    <td style="width: 5px">
    </td>
    <td><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoMensagem" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback">
<ClientSideEvents FocusedRowChanged="function(s, e) 
{
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
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
 }"></ClientSideEvents>
<Columns>
    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#227;o" Name="col_CustomButtons"
        VisibleIndex="0" Width="100px">
        <CustomButtons>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                <Image Url="~/imagens/botoes/excluirReg02.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG" />
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                <Image Url="~/imagens/botoes/pFormulario.png" />
            </dxwgv:GridViewCommandColumnCustomButton>
        </CustomButtons>
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
        <HeaderTemplate>
            <%# (IncluiMsg) ? "<img src='../../imagens/botoes/incluirReg02.png' style='cursor: pointer' onclick='abreTelaNovaMensagem();' />" : "<img style='cursor:default' src='../../imagens/botoes/incluirRegDes.png' />"%>
        </HeaderTemplate>
    </dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigo Mensagem" FieldName="CodigoMensagem"
        Name="col_CodMensagem" Visible="False" VisibleIndex="0">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="CodigoUsuarioInclusao" FieldName="CodigoUsuarioInclusao"
        Name="col_CodigoUsuarioInclusao" Visible="False" VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Mensagem" FieldName="Mensagem" Name="col_Mensagem"
        ToolTip="Mensagem que voc&#234; recebeu de alguem" VisibleIndex="1">
        <DataItemTemplate>
                    <span id="spnMensagem"><%# Eval("Mensagem").ToString().Length > 100 ? Eval("Mensagem").ToString().Substring(0, 99) + "..." : Eval("Mensagem")%>
                    </span>
        </DataItemTemplate>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Resposta" FieldName="Resposta" Name="col_Resposta"
        Visible="False" VisibleIndex="3">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Respondida Em" FieldName="DataResposta" Name="col_RespondidaEm"
        VisibleIndex="3" Width="90px">
        <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}">
        </PropertiesTextEdit>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Data Inclus&#227;o" FieldName="DataInclusao"
        Name="col_DataInclusao" Visible="False" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Indica Resposta Necessaria" FieldName="IndicaRespostaNecessaria"
        Name="col_IndicaRespostaNecessaria" Visible="False" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigo Objeto Associado" FieldName="CodigoObjetoAssociado"
        Name="col_CodigoObjetoAssociado" Visible="False" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Data Limite Resposta" FieldName="DataLimiteResposta"
        Name="col_DataLimiteResposta" Visible="False" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Respondida Por" FieldName="NomeUsuarioResposta"
        Name="col_RespondidaPor" VisibleIndex="5" Width="140px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Enviada Em" FieldName="DataInclusao" Name="col_EnviadaEm"
        VisibleIndex="2" Width="90px">
        <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}">
        </PropertiesTextEdit>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Enviada Por" FieldName="UsuarioInclusao" Name="col_EnviadaPor"
        VisibleIndex="4" Width="140px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Nome Projeto" FieldName="NomeProjeto" Name="col_NomeProjeto"
        Visible="False" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Pode Excluir Mensagem" FieldName="ExcluiMensagem"
        Name="col_podeExcluirMensagem" Visible="False" VisibleIndex="10">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="pode Editar Mensagem" FieldName="EditaMensagem"
        Name="col_podeEditarMensagem" Visible="False" VisibleIndex="10">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="pode Editar Resposta" FieldName="EditaResposta"
        Name="col_podeEditarResposta" Visible="False" VisibleIndex="10">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna" ></SettingsText>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxwgv:ASPxGridView>
</td>
    <td>
    </td>
</tr><tr>
    <td style="width: 5px">
    </td>
    <td></td>
    <td>
    </td>
</tr></tbody></table> <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hiddenField" ID="hiddenField"></dxhf:ASPxHiddenField>
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
    </form>
</body>
</html>

