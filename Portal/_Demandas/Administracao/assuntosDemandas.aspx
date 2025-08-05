<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="assuntosDemandas.aspx.cs" Inherits="_Demandas_Administracao_assuntosDemandas" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master"  %>
<asp:Content id="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="height: 19px; padding-left: 10px;">
                            <dxe:ASPxLabel id="lblTituloTela" runat="server" Font-Bold="True" 
                                Text="Assuntos de Demandas" 
                                ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="padding-right: 5px; padding-left: 5px; padding-top: 5px">
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    oncallback="pnCallback_Callback" width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAssuntoDemanda" AutoGenerateColumns="False" Width="100%"  ID="gvDados">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	                    OnGridFocusedRowChanged(s);
                    }" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == 'btnNovo')
     {
		TipoOperacao = 'Incluir';
		desabilitaHabilitaComponentes();
      	onClickBarraNavegacao('Incluir', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Incluir');		
     }
     else if(e.buttonID == 'btnEditar')
     {
		TipoOperacao = 'Editar';
		desabilitaHabilitaComponentes();
		onClickBarraNavegacao('Editar', gvDados, pcDados);
		hfGeral.Set('TipoOperacao', 'Editar');
		
     }
     else if(e.buttonID == 'btnExcluir')
     {
		onClickBarraNavegacao('Excluir', gvDados, pcDados);
     }
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
		TipoOperacao = 'Consultar';
		desabilitaHabilitaComponentes();
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set('TipoOperacao', 'Consultar');		
		pcDados.Show();
     }	
	else if(e.buttonID == 'btnCompartilharCustom')
	{
		OnGridFocusedRowChangedPopup(gvDados);
	}
}
"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="130px" VisibleIndex="0">

<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilharCustom" Text="Mostrar Interessados">
<Image ToolTip="Mostrar Interessados" Url="~/imagens/Perfis/Perfil_Permissoes.png"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
<HeaderTemplate>
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Novo Registro"" onclick=""onClick_CustomIncluir();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir Novo Registro"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoAssuntoDemanda" Name="col_Assunto" Caption="Assunto" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoGerente" Visible="False" VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoFluxo" Visible="False" VisibleIndex="3">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoProjetoAssociado" Visible="False"
        VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="Comentario" Visible="False" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsText GroupPanel="Arraste o cabe&#231;alho da coluna que deseja agrupar" ></SettingsText>



<Templates><FooterRow>
<!-- FOOTER DVDADOS -->
</FooterRow>
</Templates>
</dxwgv:ASPxGridView>
 <!-- PCDADOS --><dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px"  ID="pcDados">

<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Assunto:" ClientInstanceName="lblNome"  ID="lblNome"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtNome"  ID="txtNome">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>

 </td></tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:"  ID="ASPxLabel1">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxComboBox ID="ddlGerenteProjeto" runat="server" ClientInstanceName="ddlResponsavel"
                 IncrementalFilteringMode="Contains" 
                TextFormatString="{0}" ValueType="System.String" Width="100%">
                <Columns>
                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                </Columns>
                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                </DisabledStyle>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel runat="server" Text="Fluxo Associado:"  ID="ASPxLabel2">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxComboBox ID="ddlFluxo" runat="server" ClientInstanceName="ddlFluxo" 
                ValueType="System.String" Width="100%">
                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                </DisabledStyle>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel runat="server" Text="Projeto Associado:"  ID="ASPxLabel3">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxComboBox ID="ddlProjetos" runat="server" ClientInstanceName="ddlProjeto"
                 TabIndex="7" TextField="NomeProjeto" ValueField="CodigoProjeto"
                ValueType="System.String" Width="100%">
                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                </DisabledStyle>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel runat="server" Text="Coment&#225;rios:" ClientInstanceName="lblNome"  ID="ASPxLabel4">
            </dxe:ASPxLabel>
            &nbsp;<dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                 ForeColor="Silver" Text="0">
            </dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250" 
                ForeColor="Silver" Text=" de 2000">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxMemo ID="txtObservacoes" runat="server" ClientInstanceName="txtObservacoes"
                 Rows="5" Width="100%">
                <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}" />
                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                </DisabledStyle>
            </dxe:ASPxMemo>
        </td>
    </tr>
    <tr><td style="height: 10px">
     &nbsp;</td></tr><tr><td align=right><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td style="PADDING-RIGHT: 5px"><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	if (window.onClick_btnSalvar)
	      onClick_btnSalvar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td></tr></tbody></table></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <!-- FIM DE PCDADOS -->
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" EncodeHtml="False" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>



 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
          onEnd_pnCallback();
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
        </tr>
    </table>
</asp:Content>

