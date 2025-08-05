<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroGrandesDesafios.aspx.cs" Inherits="administracao_CadastroGrandesDesafios" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <!-- table principal -->
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                                height: 26px" valign="middle">
                                <table>
                                    <tr>
                                        <td align="center" style="width: 1px; height: 19px">
                                            <span id="Span2" runat="server"></span>
                                        </td>
                                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                                Text="Cadastro Grandes Desafios" 
                                                ClientInstanceName="lblTitulo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    </td><td></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
<!-- ASPxCALLBACKPANEL principal: pnCallback -->
<dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback" 
                        oncallback="pnCallback_Callback">
                        <PanelCollection>
<dxp:PanelContent runat="server"><!-- ASPxGRidVIEW: gvDados -->
                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoGrandeDesafio" AutoGenerateColumns="False" 
        ID="gvDados" 
        OnAfterPerformCallback="gvDados_AfterPerformCallback" Width="100%" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" 
        OnHtmlRowPrepared="gvDados_HtmlRowPrepared" style="text-align: left">
<ClientSideEvents CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnIncluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
     }
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		pcDados.Show();
     }	
	else if(e.buttonID == 'btnPermissoesCustom')
	{
	    OnGridFocusedRowChangedPopup(gvDados);
	}
}" FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"></ClientSideEvents>
<Columns>
    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#245;es" VisibleIndex="0"
        Width="130px">
        <CustomButtons>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Text="Inserir" Visibility="Invisible">
                <Image AlternateText="Inserir" Url="~/imagens/botoes/incluirReg02.png">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir" 
                Visibility="Invisible">
                <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                <Image AlternateText="Detalhe" Url="~/imagens/botoes/pFormulario.png">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
        </CustomButtons>
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
        <HeaderTemplate>
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""NovoGrandeDesafio();"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>")%>
        </HeaderTemplate>
    </dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataTextColumn Caption="Grande Desafio" 
        FieldName="DescricaoGrandeDesafio" Name="DescricaoGrandeDesafio"
        VisibleIndex="1">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="CodigoGrandeDesafio" 
        FieldName="CodigoGrandeDesafio" Name="CodigoGrandeDesafio" ShowInCustomizationForm="True" 
        Visible="False" VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Meta Período Estratégico" 
        FieldName="MetaPeriodoEstrategico" Name="MetaPeriodoEstrategico"
        VisibleIndex="3">
        <CellStyle Wrap="True">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="CodigoEntidade" 
        FieldName="CodigoEntidade" Name="CodigoEntidade"
        VisibleIndex="4" Visible="False">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="IndicaGrandeDesafioAtivo" 
        ShowInCustomizationForm="True" VisibleIndex="7" Visible="False" 
        Caption="Ativo" Name="IndicaGrandeDesafioAtivo">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFooter="True"></Settings>

<Styles>
<EmptyDataRow BackColor="#EEEEDD" ForeColor="Black"></EmptyDataRow>
</Styles>
                    <Templates>
                        <FooterRow>
<!-- FOOTER DVDADOS -->
<table style="WIDTH: 100%" cellspacing="0" cellpadding="0">
<TBODY><tr>
<td style="BORDER-RIGHT: green 2px solid; BORDER-TOP: green 2px solid; BORDER-LEFT: green 2px solid; WIDTH: 10px; BORDER-BOTTOM: green 2px solid; BACKGROUND-COLOR: #ddffcc">&nbsp;</td>
<td style="PADDING-LEFT: 5px"><%# definelegenda %>. </td></tr></tbody></table>
                        </FooterRow>
                    </Templates>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" 
        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" 
        ID="pcDados" AllowDragging="True" CloseAction="None" 
        Width="950px">
<ContentStyle>
<Paddings Padding="8px"></Paddings>
</ContentStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><!-- table -->
    <table class="headerGrid">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="lblGrandeDesafio" runat="server" 
                                    ClientInstanceName="lblGrandeDesafio"  
                                    Text="Grande Desafio:">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lblCantCarater" runat="server" 
                                    ClientInstanceName="lblCantCarater"  
                                    ForeColor="Silver" Text="0">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lblDe500" runat="server" ClientInstanceName="lblDe500" 
                                     ForeColor="Silver" Text="  de 500">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <!-- ASPxROUNDPANEL -->
                                <dxe:ASPxMemo ID="memGrandeDesafio" runat="server" 
                                    ClientInstanceName="memGrandeDesafio"  
                                    Height="83px" Width="100%">
                                    <ClientSideEvents Init="function(s, e) { try
                {
                return setMaxLength(s.GetInputElement(), 500);
                }
                catch(e)
                {

                }
                }" />
                                    <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                        <Border BorderColor="#E0E0E0" />
                                    </DisabledStyle>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td style="HEIGHT: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Meta do Período Estratégico:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txtMetaPeriodoEstrategico" runat="server" 
                                    ClientInstanceName="txtMetaPeriodoEstrategico" 
                                    Height="16px" MaxLength="100" Width="99%">
                                    <DisabledStyle BackColor="#EBEBEB"  
                                        ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
       <tr>
            <td style="height: 10px">
                <dxe:ASPxCheckBox ID="checkAtivo" runat="server" 
                    ClientInstanceName="checkAtivo"  
                    Text="Grande Desafio Ativo?">
                </dxe:ASPxCheckBox>
            </td>
        </tr>
       <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnSalvar"  
                                    Text="Salvar" Width="100px">
                                    <clientsideevents click="function(s, e) {
	e.processOnServer = false;
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="WIDTH: 10px">
                            </td>
                            <td align="right">
                                <dxe:ASPxButton ID="btnCancelar" runat="server" 
                                    ClientInstanceName="btnCancelar"  
                                    Text="Fechar" Width="100px">
                                    <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
	
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>    
    </dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <!-- ASPxHidDENFIELD hfGeral-->
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	onEnd_pnCallback();
}"></ClientSideEvents>
    <Border BorderStyle="None" />
</dxcp:aspxcallbackpanel>
</td>
</table>
    
</div>
</asp:Content>
