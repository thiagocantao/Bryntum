<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="contaDesembolsoFinanceiro.aspx.cs" Inherits="administracao_contaDesembolsoFinanceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle">
                            <dxe:ASPxLabel id="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Desembolso Financeiro"></dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td style="height: 10px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent ID="PanelContent1" runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoConta" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAfterPerformCallback="gvDados_AfterPerformCallback1" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
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
}
"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
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
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoConta" Name="CodigoConta" 
        Caption="Codigo Conta" VisibleIndex="7" Visible="False">
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Conta" FieldName="DescricaoConta" 
        ShowInCustomizationForm="True" VisibleIndex="1">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Financiável" 
        FieldName="IndicaInvestimentoFinanciavel" Name="IndicaInvestimentoFinanciavel" 
        ShowInCustomizationForm="True" VisibleIndex="4" Width="75px">
        <DataItemTemplate>
            <%# (Eval("IndicaInvestimentoFinanciavel").ToString() == "S" ? "Sim" : "Não") %>
        </DataItemTemplate>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="GrupoConta" 
        ShowInCustomizationForm="True" VisibleIndex="3">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigo Entidade" 
        FieldName="CodigoEntidade" Name="CodigoEntidade" ShowInCustomizationForm="True" 
        VisibleIndex="5" Visible="False">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigo Categoria" 
        FieldName="CodigoCategoria" Name="CodigoCategoria" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Área" 
        FieldName="DescricaoCategoria" ShowInCustomizationForm="True" 
        VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="620px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
    <table>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                    Text="Descrição:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
                <dxe:ASPxTextBox ID="txtNomeConta" runat="server" 
                    ClientInstanceName="txtNomeConta"  
                    Width="100%">
                    <DisabledStyle BackColor="#EBEBEB"  
                        ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                    Text="Grupo:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
                <dxe:ASPxTextBox ID="txtNomeGrupo" runat="server" 
                    ClientInstanceName="txtNomeGrupo"  
                    Width="100%">
                    <DisabledStyle BackColor="#EBEBEB"  
                        ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td style="width: 311px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 311px">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Área:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 311px">
                            <dxe:ASPxComboBox ID="ddlCategoria" runat="server" 
                                ClientInstanceName="ddlCategoria"  
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB"  
                                    ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxCheckBox ID="ckbFinanciavel" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckbFinanciavel"  
                                Text="Investimento Financiável?">
                                <DisabledStyle BackColor="#EBEBEB"  
                                    ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></TBODY></table>
            </td>
        </tr>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>

























 </td></tr></TBODY></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta de desembolso financeiro incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta de desembolso financeiro alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Conta de desembolso financeiro excluída com sucesso!&quot;);
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>

