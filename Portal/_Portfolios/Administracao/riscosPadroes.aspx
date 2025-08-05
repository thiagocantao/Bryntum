<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="riscosPadroes.aspx.cs" Inherits="_Portfolios_Administracao_riscosPadroes" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Riscos Padrões"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="height: 10px">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoRiscoPadrao" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
	gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
	 }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
	 }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="120px" VisibleIndex="0">

<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
<HeaderTemplate>
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoRiscoPadrao" Visible="False" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoRiscoPadrao" Caption="Descri&#231;&#227;o do Risco" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataExclusao" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioExclusao" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="IndicaControladoSistema" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoImpactoAlto" Name="DescricaoImpactoAlto" Caption="DescricaoImpactoAlto" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoImpactoMedio" Name="DescricaoImpactoMedio" Caption="DescricaoImpactoMedio" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoImpactoBaixo" Name="DescricaoImpactoBaixo" Caption="DescricaoImpactoBaixo" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoProbabilidadeAlta" Name="DescricaoProbabilidadeAlta" Caption="DescricaoProbabilidadeAlta" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoProbabilidadeMedia" Name="DescricaoProbabilidadeMedia" Caption="DescricaoProbabilidadeMedia" Visible="False" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoProbabilidadeBaixa" Name="DescricaoProbabilidadeBaixa" Caption="DescricaoProbabilidadeBaixa" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>

<Templates><FooterRow>
<table cellpadding="0" cellspacing="0"  width="100%">
    <tr>
            <td align=right>
            <dxe:ASPxLabel ID="lblDescricaoTotal" runat="server" ClientInstanceName="lblDescricaoTotal"
                Font-Bold="False"  Text="Total de Riscos: ">
            </dxe:ASPxLabel><%# gvDados.VisibleRowCount%></td>
    </tr>
</table>
</FooterRow>
</Templates>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px" Font-Bold="False"  ID="pcDados">
<ClientSideEvents CloseUp="function(s, e) {
	lblCantCaraterImpactoAlto.SetText('0');
	lblCantCaraterImpactoMedio.SetText('0');
	lblCantCaraterImpactoBaixo.SetText('0');
	lblCantCaraterProbabilidadeAlta.SetText('0');
	lblCantCaraterProbabilidadeMedia.SetText('0');
	lblCantCaraterProbabilidadeBaixa.SetText('0');
}" PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td align=left><dxe:ASPxLabel runat="server" Text="Risco:"  ID="ASPxLabel1"></dxe:ASPxLabel>

 </td></tr><tr><td align=left><dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtRisco"  ID="txtRisco">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>

 </td></tr><tr><td style="PADDING-TOP: 10px; BORDER-BOTTOM: darkgray 1px solid" align=left><dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o Impactos:" Font-Bold="True"  ID="ASPxLabel2"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="Informar as situa&#231;&#245;es que ser&#227;o consideradas para cada grau de impacto." ForeColor="#404040" ID="ASPxLabel4"></dxe:ASPxLabel>

</td></tr><tr><td align=left><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Alto:" ClientInstanceName="lblImpactoAlta"  ID="lblImpactoAlta"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterImpactoAlto"  ForeColor="Silver" ID="lblCantCaraterImpactoAlto"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="lblDe500"></dxe:ASPxLabel>

 </td><td style="PADDING-LEFT: 5px; PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="M&#233;dio:" ClientInstanceName="lblImpactoMedia"  ID="lblImpactoMedia"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterImpactoMedio"  ForeColor="Silver" ID="lblCantCaraterImpactoMedio"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="ASPxLabel5"></dxe:ASPxLabel>

 </td><td style="PADDING-LEFT: 5px; PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Baixo:" ClientInstanceName="lblImpactoMedia"  ID="lblImpactoBaixa"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterImpactoBaixo"  ForeColor="Silver" ID="lblCantCaraterImpactoBaixo"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="ASPxLabel7"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoImpactoAlto"  ID="mmDescricaoImpactoAlto">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td><td style="PADDING-LEFT: 5px"><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoImpactoMedio"  ID="mmDescricaoImpactoMedio">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td><td style="PADDING-LEFT: 5px"><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoImpactoBaixo"  ID="mmDescricaoImpactoBaixo">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td></tr></tbody></table></td></tr><tr><td style="PADDING-TOP: 10px; BORDER-BOTTOM: darkgray 1px solid"><dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o Probabilidades:" Font-Bold="True"  ID="ASPxLabel3"></dxe:ASPxLabel>

&nbsp;<dxe:ASPxLabel runat="server" Text="Informar as situa&#231;&#245;es que ser&#227;o consideradas para cada chance de probabilidade." ForeColor="#404040" ID="ASPxLabel6"></dxe:ASPxLabel>

</td></tr><tr><td><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Alta:" ClientInstanceName="lblProbabilidadeAlta"  ID="lblProbabilidadeAlta"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterProbabilidadeAlta"  ForeColor="Silver" ID="lblCantCaraterProbabilidadeAlta"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="ASPxLabel9"></dxe:ASPxLabel>

 </td><td style="PADDING-LEFT: 5px; PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="M&#233;dia:" ClientInstanceName="lblProbabilidadeMedia"  ID="lblProbabilidadeMedia"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterProbabilidadeMedia"  ForeColor="Silver" ID="lblCantCaraterProbabilidadeMedia"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="ASPxLabel11"></dxe:ASPxLabel>

 </td><td style="PADDING-LEFT: 5px; PADDING-TOP: 5px"><dxe:ASPxLabel runat="server" Text="Baixa:" ClientInstanceName="lblProbabilidadeBaixa"  ID="lblProbabilidadeBaixa"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterProbabilidadeBaixa"  ForeColor="Silver" ID="lblCantCaraterProbabilidadeBaixa"></dxe:ASPxLabel>

 <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe500"  ForeColor="Silver" ID="ASPxLabel13"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoProbabilidadeAlta"  ID="mmDescricaoProbabilidadeAlta">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td><td style="PADDING-LEFT: 5px"><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoProbabilidadeMedia"  ID="mmDescricaoProbabilidadeMedia">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td><td style="PADDING-LEFT: 5px"><dxe:ASPxMemo runat="server" Height="71px" Width="200px" ClientInstanceName="mmDescricaoProbabilidadeBaixa"  ID="mmDescricaoProbabilidadeBaixa">
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 250);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

 </td></tr></tbody></table></td></tr><tr><td align=right><table id="tblSalvarFechar" cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><TR style="HEIGHT: 35px"><td style="WIDTH: 540px" align=right><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td><td style="PADDING-LEFT: 10px" align=right><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td></tr></tbody></table></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcMensagemGravacao"><ContentCollection>
<dxpc:PopupControlContentControl runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="center" style="">
                                                    </td>
                                                    <td align="center" rowspan="3" style="width: 70px">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>








                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>








                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
			mostraPopupMensagemGravacao(&quot;Risco padr&#227;o inclu&#237;do com sucesso!&quot;);
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			mostraPopupMensagemGravacao(&quot;Risco padr&#227;o alterado com sucesso!&quot;);	
	}
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
 
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>



