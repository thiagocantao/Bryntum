<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroAvisos.aspx.cs" Inherits="administracao_CadastroAvisos" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">

    <table>
        <tr>
            <td colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height:26px">
                        <td valign="middle">
                            &nbsp;
                            <dxe:ASPxLabel ID="lblTitulo" runat="server" Font-Bold="True"
                                Text="Avisos">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>

            </td>
            <td colspan="1">
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
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
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAviso" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="grid_AfterPerformCallback">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
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
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="110px" VisibleIndex="0">

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
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir');lblCantCarater.SetText('0');"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoAviso" Visible="False" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Assunto" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Aviso" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataInicio" Width="80px" Caption="In&#237;cio" VisibleIndex="2">
<PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataTermino" Width="80px" Caption="T&#233;rmino" VisibleIndex="3">
<PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Width="80px" Caption="Inclus&#227;o" VisibleIndex="4">
<PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Visible="False" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoEntidade" Visible="False" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="tipoDestinatario" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes do Aviso" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="12" ShowCloseButton="False" Width="665px" Height="100px"  ID="pcDados">
<ClientSideEvents PopUp="function(s, e) 
{
	desabilitaHabilitaComponentes();
}
"></ClientSideEvents>

<ContentStyle>
<Paddings PaddingBottom="4px"></Paddings>
</ContentStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxp:ASPxPanel runat="server" ClientInstanceName="pnFormulario" Width="100%" ID="pnFormulario"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 660px" id="tbGeral" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxLabel runat="server" Text="Assunto:"  ID="ASPxLabel1" >
<DisabledStyle ></DisabledStyle>
</dxe:ASPxLabel>


 </td></tr></tbody></table><dxe:ASPxTextBox runat="server" Width="660px" MaxLength="100" ClientInstanceName="txtAssunto"  ID="txtAssunto" >
<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td></tr><tr><td style="HEIGHT: 8px"></td></tr><tr><td><table style="WIDTH: 660px" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="WIDTH: 150px">&nbsp;<dxe:ASPxLabel runat="server" Text="Data de In&#237;cio:"  ID="ASPxLabel3" ></dxe:ASPxLabel>


 </td><td style="WIDTH: 150px"><dxe:ASPxLabel runat="server" Text="Data de T&#233;rmino:"  ID="ASPxLabel2" ></dxe:ASPxLabel>


 </td><td><dxe:ASPxLabel runat="server" Text="Tipo de Destinat&#225;rio:"  ID="ASPxLabel4" ></dxe:ASPxLabel>


 </td></tr><tr><td><dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="135px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="dteInicio"  ID="dteInicio" >
<CalendarProperties ShowClearButton="False" ShowTodayButton="False"></CalendarProperties>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>


 </td><td><dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="135px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="dteTermino"  ID="dteTermino" >
<CalendarProperties ShowClearButton="False" ShowTodayButton="False"></CalendarProperties>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>


 </td><td><dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="360px" ClientInstanceName="ddlTipoDestinatario"  ID="ddlTipoDestinatario" >
<ClientSideEvents SelectedIndexChanged="function(s, e) {

pnDestinatario.PerformCallback(&quot;popula&quot;);
if(ddlTipoDestinatario.GetValue() ==null || ddlTipoDestinatario.GetValue()==&quot;TD&quot;)
{
document.getElementById('pnDestinatario').style.height=&quot;0px&quot;;
document.getElementById('txtAviso').style.height=&quot;247px&quot;;

}
else
{
document.getElementById('pnDestinatario').style.height=&quot;155px&quot;;
document.getElementById('txtAviso').style.height=&quot;95px&quot;;

}
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Text="Todos" Value="TD" Selected="True"></dxe:ListEditItem>
<dxe:ListEditItem Text="Unidade de neg&#243;cios" Value="UN"></dxe:ListEditItem>
<dxe:ListEditItem Text="Projetos" Value="PR"></dxe:ListEditItem>
<dxe:ListEditItem Text="Usu&#225;rios" Value="US"></dxe:ListEditItem>
</Items>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>


 </td></tr></tbody></table></td></tr><tr><td style="HEIGHT: 8px"></td></tr><tr><td><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnDestinatario" Height="155px" ID="pnDestinatario"  OnCallback="pnDestinatario_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxp:ASPxPanel runat="server" ClientInstanceName="rdPainelDestinatarios" ID="rdPainelDestinatarios" ><PanelCollection>
<dxp:PanelContent runat="server"><dxrp:ASPxRoundPanel runat="server" HeaderText="" ShowHeader="False" View="GroupBox" Width="656px" ClientInstanceName="rdPainel"  ID="rdPainel" ><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 634px" id="tbAvisos" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="WIDTH: 297px" valign="top"><dxe:ASPxLabel runat="server" Text="Dispon&#237;veis:" ClientInstanceName="lblDisponiveis"  ID="lblDisponiveis" ></dxe:ASPxLabel>





 </td><td style="WIDTH: 55px" align="center"></td><td valign="top"><dxe:ASPxLabel runat="server" Text="Selecionados:" ClientInstanceName="lblSelecionados"  ID="lblSelecionados" >
<DisabledStyle ></DisabledStyle>
</dxe:ASPxLabel>





 </td></tr><tr><td style="WIDTH: 297px; HEIGHT: 120px" valign="top"><dxe:ASPxListBox runat="server" Rows="4" SelectionMode="Multiple" ClientInstanceName="lbDisponiveis" EnableClientSideAPI="True" Width="295px" EnableTheming="True"  ID="lbDisponiveis" >
<ItemStyle Wrap="True" ></ItemStyle>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
 ajustaLarguras();
}" Init="function(s, e) {
		UpdateButtons();
        ajustaLarguras();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>





 </td><td style="WIDTH: 55px" valign="top" align="center"><table style="HEIGHT: 110px" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td valign="top" align="center"><dxe:ASPxButton runat="server" ClientInstanceName="btnADDTodos" Text="&gt;&gt;" Width="40px" Height="10px"  ToolTip="Selecionar Todos" ID="btnADDTodos" >
<ClientSideEvents Click="function(s, e) 
{
	lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





 </td></tr><tr><td valign="top" align="center"><dxe:ASPxButton runat="server" ClientInstanceName="btnADD" Text="&gt;" Width="40px" Height="10px"  ToolTip="Selecionar" ID="btnADD" >
<ClientSideEvents Click="function(s, e) 
{
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





 </td></tr><tr><td valign="top" align="center"><dxe:ASPxButton runat="server" ClientInstanceName="btnRMV" Text="&lt;" Width="40px" Height="10px"  ToolTip="Remover" ID="btnRMV" >
<ClientSideEvents Click="function(s, e) 
{
	lb_moveItem(lbSelecionados,lbDisponiveis);
	UpdateButtons();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





 </td></tr><tr><td valign="top" align="center"><dxe:ASPxButton runat="server" ClientInstanceName="btnRMVTodos" Text="&lt;&lt;" Width="40px" Height="10px"  ToolTip="Remover Todos" ID="btnRMVTodos" >
<ClientSideEvents Click="function(s, e) 
{
	lb_moveTodosItens(lbSelecionados,lbDisponiveis);
	UpdateButtons(); 
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





 </td></tr></tbody></table></td><td style="HEIGHT: 120px" valign="top"><dxe:ASPxListBox runat="server" Rows="4" SelectionMode="Multiple" ClientInstanceName="lbSelecionados" EnableClientSideAPI="True" Width="276px" EnableTheming="True"  ID="lbSelecionados" >
<ItemStyle Wrap="True" ></ItemStyle>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
		UpdateButtons();
 ajustaLarguras();
}" Init="function(s, e) 
{
	iniciaGridSelecionados();
	ajustaLarguras();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>





 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxrp:ASPxRoundPanel>




 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hiddenField" ID="hiddenField" ></dxhf:ASPxHiddenField>




 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfCount" ID="hfCount" ></dxhf:ASPxHiddenField>




 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfOrdena" ID="hfOrdena" ></dxhf:ASPxHiddenField>




 </dxp:PanelContent>
</PanelCollection>
</dxp:ASPxPanel>



 </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>


 </td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Aviso:"  ID="ASPxLabel5" ></dxe:ASPxLabel>


 </td></tr><tr><td style="HEIGHT: 90px"><dxe:ASPxMemo runat="server" Height="85px" Native="True" Width="656px" ClientInstanceName="txtAviso" EnableClientSideAPI="True"  ID="txtAviso" >
<ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>


 </td></tr></tbody></table><dxe:ASPxLabel runat="server" Text=" 0" ClientInstanceName="lblCantCarater"  ForeColor="Silver" ID="lblCantCarater" ></dxe:ASPxLabel>


 <dxe:ASPxLabel runat="server" Text="de 2000" ClientInstanceName="lblDe2000"  ForeColor="Silver" ID="lblDe2000" ></dxe:ASPxLabel>


 </dxp:PanelContent>
</PanelCollection>
</dxp:ASPxPanel>

 &nbsp; </td></tr><tr><td style="HEIGHT: 8px"></td></tr><tr><td align=right><table style="WIDTH: 200px" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="WIDTH: 90px"><dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
		e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 90px"><dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}
"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></tbody></table></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcMensagemGravacao"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table border="0" cellpadding="0" cellspacing="0" width="100%"><tbody><tr><td align="center" style=""></td><td align="center" rowspan="3" style="width: 70px"><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>





































 </td></tr><tr><td style="height: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>





































 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	{
		if(&quot;Incluir&quot; == s.cp_OperacaoOk)
			mostraPopupMensagemGravacao(&quot;Aviso inclu&#237;do com sucesso!&quot;);
	    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
			mostraPopupMensagemGravacao(&quot;Aviso editado com sucesso!&quot;);	    
//onEnd_pnCallback();
	}
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

