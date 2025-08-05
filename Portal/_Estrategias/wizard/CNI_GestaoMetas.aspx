<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CNI_GestaoMetas.aspx.cs" 
         Inherits="_Estrategias_wizard_metasDesempenho"%>
<%@ MasterType VirtualPath="~/novaCdis.master"  %>
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
                                Text="Gestão de Metas"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server">
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" 
            gridviewid="gvDados" onrenderbrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:aspxgridviewexporter>
    <dxwgv:ASPxGridView runat="server" 
        ClientInstanceName="gvDados" KeyFieldName="CodigoIndicador" 
        AutoGenerateColumns="False" Width="99%"  
        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}" CustomButtonClick="function(s, e) {
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
     else if(e.buttonID == &quot;btnAnalises&quot;)
     {	

		var codigoIndicador = s.GetRowKey(e.visibleIndex);
		var url = window.top.pcModal.cp_Path + '_Estrategias/wizard/CNI_AnalisesIndicador.aspx?CI=' + codigoIndicador + '&ALT=' + (screen.height - 300);
		window.top.showModal(url, 'Análises', 1020, screen.height - 240, '', null);
     }	
}"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="90px" Caption=" " 
        VisibleIndex="0"><CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar Meta">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
        <dxwgv:GridViewCommandColumnCustomButton Text="Excluir Meta" ID="btnExcluir">
            <Image Url="~/imagens/botoes/excluirReg02.PNG">
            </Image>
        </dxwgv:GridViewCommandColumnCustomButton>
        <dxwgv:GridViewCommandColumnCustomButton Text="Análises" ID="btnAnalises">
            <Image Url="~/imagens/botoes/btnAnalises.png">
            </Image>
        </dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
    <HeaderTemplate>
        <table><tr><td style="cursor:pointer" title="Exportar para excel">
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/imagens/botoes/btnExcel.png" 
                                        ToolTip="Exportar para excel"  onclick="ImageButton1_Click" />
                                        </td></tr></table>
    </HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Caption="Indicador" 
        VisibleIndex="2" Width="350px" ExportWidth="350">
<Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Tipo" VisibleIndex="3" Caption="Tipo" 
        Width="85px" ExportWidth="90" Name="Tipo">
    <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" 
        AllowAutoFilter="False" FilterMode="DisplayText" />
<Settings FilterMode="DisplayText" AllowAutoFilter="False" AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>
    <DataItemTemplate>
        <%# getTipoMeta() %>
    </DataItemTemplate>
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" 
        Width="180px" Caption="Responsável" VisibleIndex="5" ExportWidth="240">
<Settings AllowAutoFilter="True" AllowHeaderFilter="False"></Settings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Meta" VisibleIndex="1" Caption="Meta" 
        Name="Meta" ExportWidth="400">
    <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
<Settings AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Permissao" Visible="False" 
        VisibleIndex="4" Caption="Permissao" Name="Permissao">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoEstrategia" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="Descricao" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="Comentario" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoMapaEstrategico" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" 
        AutoExpandAllGroups="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" 
        ShowHeaderFilterButton="True" ShowGroupPanel="True"></Settings>

</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Atualiza&#231;&#227;o das Metas" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="900px"  ID="pcDados">
<ClientSideEvents Closing="function(s, e) {	
        tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
}" Shown="function(s, e) {	
	tabEdicao.SetActiveTab(tabEdicao.GetTabByName('tbDados'));
	
}"></ClientSideEvents>

<ContentStyle>
<Paddings PaddingBottom="8px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td>
    <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" 
        ClientInstanceName="tabEdicao" Width="100%" 
        ID="ASPxPageControl1"><TabPages>
<dxtc:TabPage Name="tbDados" Text="Meta"><ContentCollection>
<dxw:ContentControl runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Indicador:"  ID="ASPxLabel4"></dxe:ASPxLabel>


 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIndicador" ClientEnabled="False"  ID="txtIndicador">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td>
    <table>
        <tr>
            <td style="width: 205px">
                &nbsp;</td>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                    Text="Mapa Estratégico:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="width: 205px; padding-right: 5px">
                <dxe:ASPxRadioButtonList ID="rbTipo" runat="server" ClientInstanceName="rbTipo" 
                     ItemSpacing="15px" 
                    RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                    <paddings padding="0px" />
                    
<Paddings Padding="0px"></Paddings>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlAssociacao.PerformCallback();
}"></ClientSideEvents>
                    <Items>
                        <dxe:ListEditItem Selected="True" Text="Macrometa" Value="PSP" />
                        <dxe:ListEditItem Text="Micrometa" Value="OBJ" />
                    </Items>
                </dxe:ASPxRadioButtonList>
            </td>
            <td>
                <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" 
                     OnCallback="ddlAssociacao_Callback" 
                    Width="100%">
                 
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlAssociacao.PerformCallback();
}"></ClientSideEvents>

                    <ItemStyle Wrap="True" />
                    <listboxstyle wrap="True">
                    </listboxstyle>
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>


 </td></tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                Text="Associar a:">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr><td>
        <dxe:ASPxComboBox ID="ddlAssociacao" runat="server" 
            Width="100%" ClientInstanceName="ddlAssociacao" 
            OnCallback="ddlAssociacao_Callback">            
<ClientSideEvents EndCallback="function(s, e) {
	//debugger
	s.SetValue(s.cp_Valor != '-1' ? s.cp_Valor : null); 
}"></ClientSideEvents>

            <ItemStyle Wrap="True" />
            <listboxstyle wrap="True">
            </listboxstyle>
        </dxe:ASPxComboBox>


 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td>
    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
        Text="Meta:">
    </dxe:ASPxLabel>
    </td></tr><tr><td>
        <dxe:ASPxMemo ID="txtMeta" runat="server" 
            ClientInstanceName="txtMeta"  Rows="5" 
            Width="100%">
            <clientsideevents keyup="function(s, e) {
	limitaASPxMemo(s, 2000);
}" />

<ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>

            <disabledstyle backcolor="#EBEBEB" forecolor="Black">
            </disabledstyle>
        </dxe:ASPxMemo>
        </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td>
    <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
        Text="Descrição:">
    </dxe:ASPxLabel>
    </td></tr>
    <tr>
        <td>
            <dxe:ASPxMemo ID="txtDescricao" runat="server" 
                ClientInstanceName="txtDescricao"  Rows="4" 
                Width="100%">
                <clientsideevents keyup="function(s, e) {
	limitaASPxMemo(s, 500);
}" />

<ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 500);
}"></ClientSideEvents>

                <disabledstyle backcolor="#EBEBEB" forecolor="Black">
                </disabledstyle>
            </dxe:ASPxMemo>
        </td>
    </tr>
    <tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxe:ASPxLabel runat="server" 
        Text="Comentários:"  ID="ASPxLabel7"></dxe:ASPxLabel>


 </td></tr><tr><td>
        <dxe:ASPxMemo runat="server" Rows="5" Width="100%" 
            ClientInstanceName="txtComentarios" 
            ID="txtComentarios">
            
<ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>


 </td></tr></tbody></table></dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
<dxtc:TabPage Name="tbMetas" Text="Detalhes"><ContentCollection>
<dxw:ContentControl runat="server"><iframe id="frmMetas" frameborder="0" height="393" scrolling="no" src=""
                                    width="100%"></iframe> </dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
            <dxtc:TabPage Name="tbSerie" Text="Série Histórica">
                <contentcollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <iframe id="frmSerie" frameborder="0" height="393" scrolling="no" src=""
                                    width="100%"></iframe>
                    </dxw:ContentControl>
                </contentcollection>
            </dxtc:TabPage>
</TabPages>

<ClientSideEvents ActiveTabChanged="function(s, e) {
    if(e.tab.name == &#39;tbMetas&#39;)
	    document.getElementById(&#39;frmMetas&#39;).src = urlMetas;
    if(e.tab.name == &#39;tbSerie&#39;)
	    document.getElementById(&#39;frmSerie&#39;).src = urlSerie;
}"></ClientSideEvents>

<ContentStyle>
<Paddings PaddingLeft="5px" PaddingRight="5px"></Paddings>
</ContentStyle>
    <ClientSideEvents ActiveTabChanged="function(s, e) {
    if(e.tab.name == 'tbMetas')
	    document.getElementById('frmMetas').src = urlMetas;
    if(e.tab.name == 'tbSerie')
	    document.getElementById('frmSerie').src = urlSerie;
}" />
</dxtc:ASPxPageControl>

 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="right">
    <table>
        <tbody>
            <tr>
                <td>
                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnSalvar"  
                        Text="Salvar" Width="100px">
            
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
                    </dxe:ASPxButton>
                </td>
                <td style="WIDTH: 10px">
                </td>
                <td>
                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnFechar"  
                        Text="Fechar" Width="90px">                        
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                    </dxe:ASPxButton>
                </td>
            </tr>
        </tbody>
    </table>

 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" 
        ClientInstanceName="pcUsuarioIncluido"  
        HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
        Width="270px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" 
                SupportsDisabledAttribute="True">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="center" style="">
                            </td>
                            <td align="center" rowspan="3" style="WIDTH: 70px">
                                <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" 
                                    ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="HEIGHT: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" 
                                    ClientInstanceName="lblAcaoGravacao" >
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
    if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Meta salva com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Meta excluída com sucesso!&quot;);
        OnGridFocusedRowChanged(gvDados, true);
     }
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
 
            </td>
        </tr>
    </table>
</asp:Content>
 



