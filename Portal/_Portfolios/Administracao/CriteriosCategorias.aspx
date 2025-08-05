<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CriteriosCategorias.aspx.cs" Inherits="_Portfolios_Administracao_CriteriosCategorias" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" 
                    Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" 
                    Text="Categorias"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="height: 5px;">
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
                        OnCallback="pnCallback_Callback" ><PanelCollection>
<dxp:PanelContent runat="server"><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoCategoria" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
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
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="col_comando" Width="120px" VisibleIndex="0">

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
<dxwgv:GridViewDataTextColumn FieldName="SiglaCategoria" Name="SiglaCategoria" Width="80px" Caption="Sigla" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoCategoria" Name="col_DescricaoCategoria" Caption="Categoria" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria" Name="col_CodigoCategoria" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Dados da Categoria" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupVerticalOffset="40" ShowCloseButton="False" Width="860px"  ID="pcDados">
<ClientSideEvents PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}" Shown="function(s, e) {
	tab.SetActiveTab(tab.GetTabByName('tbCS'));
}"></ClientSideEvents>

<CloseButtonImage Width="17px"></CloseButtonImage>

<SizeGripImage Width="12px"></SizeGripImage>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%"><tbody><tr><td align=left><dxp:ASPxPanel runat="server" Width="100%" Height="380px" ID="pnFormulario" style="OVERFLOW: auto"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%"><tbody><tr><td><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Categoria:"  ID="ASPxLabel1"></dxe:ASPxLabel>


 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 80px"><dxe:ASPxLabel runat="server" Text="Sigla:" ClientInstanceName="lblSigla"  ID="lblSigla"></dxe:ASPxLabel>


 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtCategoria"  ID="txtCategoria">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" MaxLength="5" ClientInstanceName="txtSigla"  ID="txtSigla">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td></tr></tbody></table></td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tab" TabSpacing="0px" Width="100%"  ID="tab"><TabPages>
<dxtc:TabPage Name="tbCS" Text="Crit&#233;rios de Sele&#231;&#227;o"><ContentCollection>
<dxw:ContentControl runat="server"><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pn_ListBox_Criterios" Width="100%" ID="pn_ListBox_Criterios"  OnCallback="pn_ListBox_Criterios_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><table cellspacing="0" cellpadding="0" width="100%"><tbody><tr><td style="WIDTH: 380px" valign="top"><dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" ClientInstanceName="lbDisponiveis" Width="100%"  ID="lbDisponiveis" >
<ClientSideEvents SelectedIndexChanged="UpdateButtons


" Init="UpdateButtons



"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Width="14px"></ErrorImage>
</ValidationSettings>
</dxe:ASPxListBox>




 </td><td style="WIDTH: 60px" align="center"><table cellspacing="0" cellpadding="0"><tbody><tr><td style="HEIGHT: 28px" valign="middle"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos" ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnADDTodos" >
<ClientSideEvents Click="function(s, e) {
	                lb_moveTodosItens(lbDisponiveis, lbSelecionados);
	                capturaCodigosCriteriosSelecionados();
	                pnCallback_GridCriterios.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnADD" >
<ClientSideEvents Click="function(s, e) {
	                lb_moveItem(lbDisponiveis, lbSelecionados);
	                capturaCodigosCriteriosSelecionados();
	                pnCallback_GridCriterios.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnRMV" >
<ClientSideEvents Click="function(s, e) {
                    lb_moveItem(lbSelecionados, lbDisponiveis);
	                capturaCodigosCriteriosSelecionados();
   	                pnCallback_GridCriterios.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos" ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnRMVTodos" >
<ClientSideEvents Click="function(s, e) {
                    lb_moveTodosItens(lbSelecionados, lbDisponiveis);
	                capturaCodigosCriteriosSelecionados();
	                pnCallback_GridCriterios.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr></tbody></table></td><td style="WIDTH: 380px" valign="top"><dxe:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" Rows="3" ClientInstanceName="lbSelecionados" Width="100%"  ID="lbSelecionados" >
<ClientSideEvents SelectedIndexChanged="UpdateButtons" Init="UpdateButtons


"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Width="14px"></ErrorImage>
</ValidationSettings>
</dxe:ASPxListBox>




 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfCriteriosSelecionados" ID="hfCriteriosSelecionados" ></dxhf:ASPxHiddenField>




 </td></tr></tbody></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallback_GridCriterios" Width="100%" Height="165px" ID="pnCallback_GridCriterios"  OnCallback="pnCallback_GridCriterios_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxwgv:ASPxGridView runat="server" ClientInstanceName="gridMatriz" KeyFieldName="Criterio" AutoGenerateColumns="False" Width="100%"  ID="gridMatriz"  OnHtmlRowPrepared="gridMatriz_HtmlRowPrepared" OnHtmlDataCellPrepared="gridMatriz_HtmlDataCellPrepared">
<ClientSideEvents Init="function(s, e) {
	                    calculaPeso();
                    }"></ClientSideEvents>
<Columns>
<dxwgv:GridViewDataTextColumn FieldName="Criterio" Caption="Criterios" VisibleIndex="0">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Caption="Criterio A" Visible="False" VisibleIndex="1">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="100"></Settings>

<SettingsText EmptyDataRow="Nenhum Crit&#233;rio foi Selecionado!"></SettingsText>

<Styles>
<Header Wrap="True">
<Paddings PaddingLeft="0px" PaddingTop="3px" PaddingRight="0px" PaddingBottom="3px"></Paddings>
</Header>

<FocusedRow BackColor="White" ForeColor="Black"></FocusedRow>

<Cell CssClass="linhas">
<Paddings Padding="0px"></Paddings>
</Cell>
</Styles>
</dxwgv:ASPxGridView>





 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfValores" ID="hfValores" ></dxhf:ASPxHiddenField>





 </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>




 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>



 </dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
<dxtc:TabPage Name="tbRP" Text="Riscos Padr&#245;es"><ContentCollection>
<dxw:ContentControl runat="server"><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pn_ListBox_Riscos" Width="100%" ID="pn_ListBox_Riscos"  OnCallback="pn_ListBox_Riscos_Callback">
<ClientSideEvents EndCallback="function(s, e) {
	calculaPeso();
    calculaPesoRiscos();
}"></ClientSideEvents>
<PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><table cellspacing="0" cellpadding="0" width="100%"><tbody><tr><td style="WIDTH: 380px" valign="top"><dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3"  ClientInstanceName="lbDisponiveisRiscos" Width="100%" Height="105px"  ID="lbDisponiveisRiscos" >
<ItemStyle BackColor="White">
<SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
</ItemStyle>

<ClientSideEvents SelectedIndexChanged="UpdateButtons" Init="UpdateButtons"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>




 </td><td style="WIDTH: 60px" align="center"><table cellspacing="0" cellpadding="0"><tbody><tr><td style="HEIGHT: 28px" valign="middle"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodosRiscos" ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnADDTodosRiscos" >
<ClientSideEvents Click="function(s, e) {
	                lb_moveTodosItens(lbDisponiveisRiscos,lbSelecionadosRiscos);
	                capturaCodigosRiscosSelecionados();
	                pnCallback_GridRiscos.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDRiscos" ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnADDRiscos" >
<ClientSideEvents Click="function(s, e) {
	                lb_moveItem(lbDisponiveisRiscos, lbSelecionadosRiscos);
	                capturaCodigosRiscosSelecionados();
	                pnCallback_GridRiscos.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVRiscos" ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnRMVRiscos" >
<ClientSideEvents Click="function(s, e) {
	                lb_moveItem(lbSelecionadosRiscos, lbDisponiveisRiscos);
	                capturaCodigosRiscosSelecionados();
	                pnCallback_GridRiscos.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr><tr><td style="HEIGHT: 28px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodosRiscos" ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="40px" Height="25px" Font-Bold="True"  ID="btnRMVTodosRiscos" >
<ClientSideEvents Click="function(s, e) {
                    lb_moveTodosItens(lbSelecionadosRiscos, lbDisponiveisRiscos);
	                capturaCodigosRiscosSelecionados();
	                pnCallback_GridRiscos.PerformCallback();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>




 </td></tr></tbody></table></td><td style="WIDTH: 380px" valign="top"><dxe:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" Rows="4"  ClientInstanceName="lbSelecionadosRiscos" Width="100%" Height="105px"  ID="lbSelecionadosRiscos" >
<ItemStyle BackColor="White">
<SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
</ItemStyle>

<ClientSideEvents SelectedIndexChanged="UpdateButtons" Init="UpdateButtons"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>




 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfRiscosSelecionados" ID="hfRiscosSelecionados" ></dxhf:ASPxHiddenField>




 </td></tr></tbody></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallback_GridRiscos" Width="100%" Height="165px" ID="pnCallback_GridRiscos"  OnCallback="pnCallback_GridRiscos_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxwgv:ASPxGridView runat="server" ClientInstanceName="gridMatrizRiscos" KeyFieldName="Riscos" AutoGenerateColumns="False" Width="100%"  ID="gridMatrizRiscos"  OnHtmlRowPrepared="gridMatrizRiscos_HtmlRowPrepared" OnHtmlDataCellPrepared="gridMatrizRiscos_HtmlDataCellPrepared">
<ClientSideEvents EndCallback="function(s, e) {	
	                    calculaPesoRiscos();
                    }" Init="function(s, e) {
	                    calculaPesoRiscos();
                    }"></ClientSideEvents>
<Columns>
<dxwgv:GridViewDataTextColumn FieldName="Riscos" Caption="Riscos" VisibleIndex="0">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Caption="Risco A" Visible="False" VisibleIndex="1">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="100"></Settings>

<SettingsText EmptyDataRow="Nenhum Risco foi Selecionado!"></SettingsText>

<Styles>
<Header Wrap="True">
<Paddings PaddingLeft="0px" PaddingTop="3px" PaddingRight="0px" PaddingBottom="3px"></Paddings>
</Header>

<FocusedRow BackColor="White" ForeColor="Black"></FocusedRow>

<Cell CssClass="linhas">
<Paddings Padding="0px"></Paddings>
</Cell>
</Styles>
</dxwgv:ASPxGridView>





 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfValoresRiscos" ID="hfValoresRiscos" ></dxhf:ASPxHiddenField>





 </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>




 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>



 </dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
</TabPages>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingBottom="1px"></Paddings>

<ContentStyle>
<Paddings Padding="3px" PaddingTop="3px" PaddingBottom="2px"></Paddings>

<Border BorderColor="#4986A2"></Border>
</ContentStyle>
</dxtc:ASPxPageControl>


 </td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxp:ASPxPanel>

 </td></tr><tr><td align=right><table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0"><tbody><TR style="HEIGHT: 35px"><td style="WIDTH: 90px"><dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    if (window.onClick_btnSalvar)
	                    onClick_btnSalvar();
                }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 90px" align=right><dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%"  ID="btnFechar">
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
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <script type='text/javascript'>ajustaVisualComRegistroGrid()</script>
</asp:Content>