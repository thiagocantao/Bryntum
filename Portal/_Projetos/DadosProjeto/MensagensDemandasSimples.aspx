<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensDemandasSimples.aspx.cs"
    Inherits="MensagensDemandasSimples" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">

        var TipoOperacao;

        function abreTelaNovaMensagem() {
            var codProj = hfGeral.Get('CodigoProjeto');
            var nomeProj = hfGeral.Get('NomeProjeto');

            var myObject = new Object();

            myObject.nomeProjeto = nomeProj;

            window.top.showModal("../../Mensagens/EnvioMensagens.aspx?CO=" + codProj + "&TA=DS", "Nova Mensagem - " + nomeProj, 950, 510, "", myObject);
        }

        function funcaoPosModal() {
            gvDados.PerformCallback();
        }
   
    
    </script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Demanda Simples</title>
</head>
<body class="body">
    <form id="form1" runat="server">
    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
        OnCallback="pnCallback_Callback" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxe:ASPxLabel runat="server" Font-Bold="True" 
                                    ID="lblNomeProjeto">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10px; height: 5px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxrp:ASPxRoundPanel runat="server" ShowHeader="False" View="GroupBox" ClientInstanceName="pnBusca"
                                    ID="pnBusca">
                                    <ContentPaddings Padding="0px"></ContentPaddings>
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent2" runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 216px">
                                                            <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus"
                                                                ID="lblStatus">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 200px">
                                                        </td>
                                                        <td style="width: 200px">
                                                        </td>
                                                        <td style="width: 200px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 216px">
                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="194px"
                                                                ClientInstanceName="ddlStatus"  ID="ddlStatus">
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
                                                        </td>
                                                        <td style="width: 200px">
                                                            <dxe:ASPxCheckBox runat="server" Text="Mensagens que eu enviei" ClientInstanceName="ckbEnviei"
                                                                 ID="ckbEnviei">
                                                                <ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}"></ClientSideEvents>
                                                                <BorderLeft BorderColor="Silver"></BorderLeft>
                                                                <BorderTop BorderColor="Silver"></BorderTop>
                                                                <BorderRight BorderColor="Silver"></BorderRight>
                                                                <BorderBottom BorderColor="Silver"></BorderBottom>
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <dxe:ASPxCheckBox runat="server" Text="Mensagens que eu respondo" ClientInstanceName="ckbRespondo"
                                                                 ID="ckbRespondo">
                                                                <ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}"></ClientSideEvents>
                                                                <BorderLeft BorderColor="Silver"></BorderLeft>
                                                                <BorderTop BorderColor="Silver"></BorderTop>
                                                                <BorderRight BorderColor="Silver"></BorderRight>
                                                                <BorderBottom BorderColor="Silver"></BorderBottom>
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <dxe:ASPxCheckBox runat="server" Text="Resposta necess&#225;ria" ClientInstanceName="ckbRespostaNecessaria"
                                                                 ID="ckbRespostaNecessaria">
                                                                <ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}" />
                                                                <BorderLeft BorderColor="Silver" />
                                                                <BorderTop BorderColor="Silver" />
                                                                <BorderRight BorderColor="Silver" />
                                                                <BorderBottom BorderColor="Silver" />
                                                                <ClientSideEvents CheckedChanged="function(s, e) {
	 	e.processOnServer = false;     
	pnCallback.PerformCallback(&quot;carregaGrid&quot;);
}"></ClientSideEvents>
                                                                <BorderLeft BorderColor="Silver"></BorderLeft>
                                                                <BorderTop BorderColor="Silver"></BorderTop>
                                                                <BorderRight BorderColor="Silver"></BorderRight>
                                                                <BorderBottom BorderColor="Silver"></BorderBottom>
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <BorderLeft BorderColor="White"></BorderLeft>
                                    <BorderTop BorderColor="White"></BorderTop>
                                    <BorderRight BorderColor="White"></BorderRight>
                                    <BorderBottom BorderColor="White"></BorderBottom>
                                </dxrp:ASPxRoundPanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10px; height: 10px">
                            </td>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    HeaderText="Detalhes" Modal="True" PopupAction="None" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px"
                                    ID="pcDados">
                                    <ClientSideEvents Closing="function(s, e) {

}" PopUp="function(s, e) 
{
	OnGridFocusedRowChanged(gvDados);
}"></ClientSideEvents>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <dxp:ASPxPanel runat="server" ClientInstanceName="pnFormulario" Width="100%" ID="pnFormulario">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent3" runat="server">
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnBotaoResponder" Width="100%"
                                                            ID="pnBotaoResponder" OnCallback="pnBotaoResponder_Callback">
                                                            <PanelCollection>
                                                                <dxp:PanelContent ID="PanelContent4" runat="server">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Mensagem:" ClientInstanceName="lblMsg"
                                                                                        ID="lblMsg">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo runat="server" Height="70px" Width="665px" ClientInstanceName="txtMensagem"
                                                                                        EnableClientSideAPI="True"  ID="txtMensagem">
                                                                                        <ClientSideEvents KeyUp="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" Init="function(s, e) 
{ 
	
}"></ClientSideEvents>
                                                                                        <ReadOnlyStyle BackColor="WhiteSmoke">
                                                                                        </ReadOnlyStyle>
                                                                                        <DisabledStyle BackColor="WhiteSmoke" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Resposta:" ClientInstanceName="lblMsg"
                                                                                        ID="ASPxLabel2">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo runat="server" Height="70px" Width="665px" ClientInstanceName="txtResposta"
                                                                                        EnableClientSideAPI="True"  ID="txtResposta">
                                                                                        <ClientSideEvents KeyPress="function(s, e) 
{
	limitaASPxMemo(s, 2000);
}" Init="function(s, e) 
{
	
}"></ClientSideEvents>
                                                                                        <DisabledStyle BackColor="WhiteSmoke" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnResponder" Text="Responder"
                                                                                                        Width="90px" Height="5px"  ID="btnResponder">
                                                                                                        <ClientSideEvents Click="function(s, e) 
{
    e.processOnServer = false; 

	if(txtResposta.GetText() == '')
	{
		window.top.mostraMensagem('Resposta da mensagem deve ser informada', 'atencao', true, false, null);
	}
	else
	{
		pnCallback.PerformCallback('Responder');
	}
}" CheckedChanged="function(s, e) 
{

}"></ClientSideEvents>
                                                                                                        <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                        </Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                                                                                        Height="5px"  ID="btnSalvar">
                                                                                                        <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                                        <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                        </Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"
                                                                                                        Height="1px"  ID="btnFechar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                                        <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                        </Paddings>
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
                                                            </PanelCollection>
                                                        </dxcp:ASPxCallbackPanel>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoMensagem"
                                    AutoGenerateColumns="False" Width="100%" 
                                    ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
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
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="col_CustomButtons" Width="100px"
                                            Caption="A&#231;&#227;o" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                    <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.png">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <HeaderTemplate>
                                                <%# (IncluiMsg) ? "<img src='../../imagens/botoes/incluirReg02.png' style='cursor: pointer' onclick='abreTelaNovaMensagem();gvDados.PerformCallback();' />" : "<img style='cursor:default' src='../../imagens/botoes/incluirRegDes.png' />"%>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoMensagem" Name="col_CodMensagem" Caption="Codigo Mensagem"
                                            Visible="False" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Name="col_CodigoUsuarioInclusao"
                                            Caption="CodigoUsuarioInclusao" Visible="False" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Mensagem" Name="col_Mensagem" Caption="Mensagem"
                                            ToolTip="Mensagem que voc&#234; recebeu de alguem" VisibleIndex="1">
                                            <DataItemTemplate>
                                                <span id="spnMensagem">
                                                    <%# Eval("Mensagem").ToString().Length > 100 ? Eval("Mensagem").ToString().Substring(0, 99) + "..." : Eval("Mensagem")%>
                                                </span>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Resposta" Name="col_Resposta" Caption="Resposta"
                                            Visible="False" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataResposta" Name="col_RespondidaEm" Width="105px"
                                            Caption="Respondida Em" VisibleIndex="3">
                                            <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Name="col_DataInclusao" Caption="Data Inclusao"
                                            Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicaRespostaNecessaria" Name="col_IndicaRespostaNecessaria"
                                            Caption="Indica Resposta Necessaria" Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="col_CodigoObjetoAssociado"
                                            Caption="Codigo Objeto Associado" Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataLimiteResposta" Name="col_DataLimiteResposta"
                                            Caption="Data Limite Resposta" Visible="False" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResposta" Name="col_RespondidaPor"
                                            Width="150px" Caption="Respondida Por" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Name="col_EnviadaEm" Width="105px"
                                            Caption="Enviada Em" VisibleIndex="2">
                                            <PropertiesTextEdit DisplayFormatString="{0: dd/MM/yyyy HH:mm}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao" Name="col_EnviadaPor" Width="150px"
                                            Caption="Enviada Por" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Name="col_NomeProjeto" Caption="Nome Projeto"
                                            Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ExcluiMensagem" Name="col_podeExcluirMensagem"
                                            Caption="Pode Excluir Mensagem" Visible="False" VisibleIndex="10">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="EditaMensagem" Name="col_podeEditarMensagem"
                                            Caption="pode Editar Mensagem" Visible="False" VisibleIndex="10">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="EditaResposta" Name="col_podeEditarResposta"
                                            Caption="pode Editar Resposta" Visible="False" VisibleIndex="10">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible"></Settings>
                                    <SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna">
                                    </SettingsText>
                                    <Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                    </Paddings>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hiddenField" ID="hiddenField">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                </dxhf:ASPxHiddenField>
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
