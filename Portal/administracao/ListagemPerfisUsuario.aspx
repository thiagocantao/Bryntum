<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListagemPerfisUsuario.aspx.cs" Inherits="administracao_ListagemPerfisUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Lista de Interessados</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <script language="javascript" type="text/javascript"></script>
    <link href="../estilos/custom.css" rel="stylesheet" />

    <style type="text/css">
        @media (max-height: 768px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 200px;
            }
        }

        @media (min-height: 769px) and (max-height: 800px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 200px;
            }
        }

        @media (min-height: 801px) and (max-height: 960px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 300px;
            }
        }

        @media (min-height: 961px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 600px;
            }
        }
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <div id="divGrid" style="visibility: hidden">
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoObjeto;CodigoTipoObjeto" AutoGenerateColumns="False"
                                            Width="100%" ID="gvDados" OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Settings-VerticalScrollableHeight="250">
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
     LimpaCamposFormulario();

     if(e.buttonID == 'btnEditarCustom')		onClickEditarAcesso(s, e);
     else if(e.buttonID == 'btnDetalheCustom')	onClickDetalheAcesso(s, e);
     else if(e.buttonID == 'btnExcluirCustom')	onClickExcluirAcesso(s, e);
}"
                                                Init="function(s, e) {
    var height = Math.max(0, document.documentElement.clientHeight) - 40;
    s.SetHeight(height);
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem SummaryType="Count" FieldName="NomeUsuario" DisplayFormat="Total interessados: {0}"></dxwgv:ASPxSummaryItem>
                                            </TotalSummary>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="A&#231;&#227;o" Width="100px" Caption="A&#231;&#227;o" VisibleIndex="0">

                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                            <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dxwgv:GridViewCommandColumn>


                                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoObjeto" Name="DescricaoObjeto" Caption="Descri&#231;&#227;o" VisibleIndex="1">
                                                    <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="Perfis" Name="Perfis"
                                                    Caption="Perfis" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="HerdaPermissoesObjetoSuperior"
                                                    Name="HerdaPermissoesObjetoSuperior" Width="75px" Caption="Herdadas"
                                                    VisibleIndex="4">
                                                    <Settings AllowAutoFilter="False"></Settings>
                                                    <DataItemTemplate>
                                                        <%# (Eval("HerdaPermissoesObjetoSuperior").ToString().Equals("N") ? "<img title='' alt='' style='border:0px' src='../imagens/botoes/blank.gif'/>" : "<img title='Permissões Herdadas' alt='' style='border:0px' src='../imagens/Perfis/Perfil_Herdada.png'/>")%>
                                                    </DataItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaPermissoesPersonalizadas"
                                                    Name="IndicaPermissoesPersonalizadas" Width="75px" Caption="Personalizadas"
                                                    VisibleIndex="5">
                                                    <Settings AllowAutoFilter="False"></Settings>
                                                    <DataItemTemplate>
                                                        <%# (Eval("IndicaPermissoesPersonalizadas").ToString().Equals("N") ? "<img title='' alt='' style='border:0px' src='../imagens/botoes/blank.gif'/>" : "<img title='Permissões Personalizadas' alt='' style='border:0px' src='../imagens/Perfis/Editado.png'/>")%>
                                                    </DataItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="IniciaisTipoObjeto"
                                                    Name="IniciaisTipoObjeto" Caption="IniciaisTipoObjeto" Visible="False"
                                                    VisibleIndex="3" Width="100px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoObjeto" Name="CodigoTipoObjeto"
                                                    Caption="CodigoTipoObjeto" Visible="False" VisibleIndex="8" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaEdicaoPermitida"
                                                    Name="IndicaEdicaoPermitida" Caption="IndicaEdicaoPermitida" Visible="False"
                                                    VisibleIndex="9" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaExclusaoPermitida"
                                                    Name="IndicaExclusaoPermitida" Caption="IndicaExclusaoPermitida"
                                                    Visible="False" VisibleIndex="6" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="NivelHierarquicoObjeto"
                                                    Name="NivelHierarquicoObjeto" Caption="NivelHierarquicoObjeto" Visible="False"
                                                    VisibleIndex="7" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjeto" Name="CodigoObjeto"
                                                    Caption="CodigoObjeto" Visible="False" VisibleIndex="10" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoPai" Name="CodigoObjetoPai"
                                                    Caption="CodigoObjetoPai" Visible="False" VisibleIndex="11" Width="80px">
                                                </dxwgv:GridViewDataTextColumn>

                                            </Columns>

                                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                            <Settings ShowTitlePanel="True" ShowFilterRow="True" ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="150"></Settings>

                                            <SettingsText></SettingsText>

                                            <Styles>
                                                <Footer></Footer>

                                                <TitlePanel HorizontalAlign="Left"></TitlePanel>
                                            </Styles>

                                            <Templates>
                                                <FooterRow>
                                                    <table class="" cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 20px">
                                                                    <dxe:ASPxImage ID="imgEditadoLenda" runat="server"
                                                                        ImageUrl="~/imagens/Perfis/Editado.png" Height="16px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td style="padding-right: 10px; padding-left: 3px;">
                                                                    <dxe:ASPxLabel ID="lblEditadoLenda" runat="server" Font-Size="7pt" Text="Permissão Personalizada"></dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 20px">
                                                                    <dxe:ASPxImage ID="imgHerdadoLenda" runat="server"
                                                                        ImageUrl="~/imagens/Perfis/Perfil_Herdada.png" Height="16px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                                <td style="padding-right: 10px; padding-left: 3px">
                                                                    <dxe:ASPxLabel ID="lblHerdadoLenda" runat="server" Font-Size="7pt" Text="Permissão Herdada"></dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" style="padding-right: 10px; padding-left: 3px;"><%# "Total de Interessados: " + gvDados.VisibleRowCount %></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhe" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="875px" ID="pcDados">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>

                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe" Width="100%" ID="pnCallbackDetalhe" OnCallback="pnCallbackDetalhe_Callback">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	pcDados.Show();
}"></ClientSideEvents>
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server">
                                                            <table id="tbDetalhe" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Usu&#225;rio:" ClientInstanceName="lblUsuarioDetalhe" ID="lblUsuarioDetalhe"></dxe:ASPxLabel>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox runat="server" ReadOnly="True" ValueType="System.Int32" TextField="NomeUsuario" ValueField="CodigoUsuario" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlInteressado" ClientEnabled="False" ForeColor="Black" ID="ddlInteressado">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e){
    var codigoInteressado = s.GetValue();
    if(codigoInteressado != null)
    {
        if(window.hfGeral &amp;&amp; hfGeral.Contains('CodigoUsuarioPermissao'))
            hfGeral.Set('CodigoUsuarioPermissao', codigoInteressado);
            
        if(lbListaPerfisSelecionados.GetItemCount()&gt;0)
        {
            btnSeleccionarPermissao.SetEnabled(true);
            btnSalvarPerfis.SetEnabled(true);
        }
        else
        {
            btnSeleccionarPermissao.SetEnabled(false);
            btnSalvarPerfis.SetEnabled(false);
        }
    }
}"></ClientSideEvents>
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn FieldName="NomeUsuario" Width="60%" Caption="Nome"></dxe:ListBoxColumn>
                                                                                    <dxe:ListBoxColumn FieldName="Email" Width="40%" Caption="E-mail"></dxe:ListBoxColumn>
                                                                                </Columns>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxComboBox>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-top: 5px">
                                                                            <table id="Table2" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 47%">
                                                                                            <dxe:ASPxLabel runat="server" Text="Perfis dispon&#237;veis:" ClientInstanceName="lblProjetosDisponivel" ID="lblProjetosDisponivel">
                                                                                                <ClientSideEvents Init="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                                            </dxe:ASPxLabel>


                                                                                        </td>
                                                                                        <td valign="top" align="center"></td>
                                                                                        <td style="width: 47%">
                                                                                            <dxe:ASPxLabel runat="server" Text="Perfis selecionados:" ClientInstanceName="lblProjetosSelecionados" ID="lblProjetosSelecionados"></dxe:ASPxLabel>


                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" TextField="DescricaoPerfil" ValueField="CodigoPerfil" ClientInstanceName="lbListaPerfisDisponivel" EnableClientSideAPI="True" Width="100%" ID="lbListaPerfisDisponivel" Height="125px">
                                                                                                <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtons();
}
"
                                                                                                    SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

                                                                                                <ValidationSettings>
                                                                                                    <ErrorImage Width="14px"></ErrorImage>
                                                                                                </ValidationSettings>
                                                                                            </dxe:ASPxListBox>


                                                                                        </td>
                                                                                        <td style="padding-right: 3px; padding-left: 3px" valign="top" align="center">
                                                                                            <table cellspacing="0" cellpadding="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="middle" align="center">
                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos" ClientEnabled="False" Text="&gt;&gt;" Width="40px" ID="btnADDTodos">
                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbListaPerfisDisponivel, lbListaPerfisSelecionados);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                                            </dxe:ASPxButton>


                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding-bottom: 3px; padding-top: 3px" align="center">
                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False" Text="&gt;" Width="40px" ID="btnADD">
                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaPerfisDisponivel, lbListaPerfisSelecionados);
	UpdateButtons();	
}"></ClientSideEvents>
                                                                                                            </dxe:ASPxButton>


                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding-bottom: 3px" align="center">
                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False" Text="&lt;" Width="40px" ID="btnRMV">
                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbListaPerfisSelecionados, lbListaPerfisDisponivel);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                                            </dxe:ASPxButton>


                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos" ClientEnabled="False" Text="&lt;&lt;" Width="40px" ID="btnRMVTodos">
                                                                                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbListaPerfisSelecionados, lbListaPerfisDisponivel);
	UpdateButtons();
}"></ClientSideEvents>
                                                                                                            </dxe:ASPxButton>


                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" TextField="DescricaoPerfil" SelectionMode="CheckColumn" FilteringSettings-ShowSearchUI="true" ValueField="CodigoPerfil" ClientInstanceName="lbListaPerfisSelecionados" EnableClientSideAPI="True" Width="100%" ID="lbListaPerfisSelecionados" Height="125px">
                                                                                                <FilteringSettings ShowSearchUI="False"></FilteringSettings>

                                                                                                <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtons();
}"
                                                                                                    SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

                                                                                                <ValidationSettings>
                                                                                                    <ErrorImage Width="14px"></ErrorImage>
                                                                                                </ValidationSettings>
                                                                                            </dxe:ASPxListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-top: 5px">
                                                                            <dxe:ASPxCheckBox runat="server" Text="Herdar permiss&#245;es do objeto superior?" ClientInstanceName="checkHerdarPermissoes" ID="checkHerdarPermissoes"></dxe:ASPxCheckBox>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-top: 10px" align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSeleccionarPermissao" Text="Personalizar" ID="btnSeleccionarPermissao">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	onClickPersonalizarPermissoes(s,e);
}"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>


                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                                ClientInstanceName="btnSalvarPerfis" Text="Salvar"
                                                                                                ID="btnSalvarPerfis" Width="100px">
                                                                                                <ClientSideEvents Click="function(s, e) {
    var mensagemErro = verificarDadosPreenchidos(); 
    if(mensagemErro == '')
    {
	   if (window.SalvarCamposFormulario)
    	{
			if(window.hfGeral &amp;&amp; hfGeral.Contains(&quot;HerdaPermissoes&quot;))
				hfGeral.Set('HerdaPermissoes', checkHerdarPermissoes.GetChecked() ? 'S' : 'N');
        	if (SalvarCamposFormulario())
	        {
    	        pcDados.Hide();
        	    habilitaModoEdicaoBarraNavegacao(false, gvDados);
            	return true;
	        }
    	}
    	else
	    {
    	    window.top.mostraMensagem('O método não foi implementado!', 'Atencao', true, false, null);
	    }
    }
    else
    {
        window.top.mostraMensagem(mensagemErro, 'Atencao', true, false, null);
        e.processOnServer = false;
    }
}




"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>


                                                                                        </td>
                                                                                        <td style="padding-left: 10px">
                                                                                            <dxe:ASPxButton runat="server"
                                                                                                CommandArgument="btnCancelar" Text="Fechar" Width="100px"
                                                                                                ID="btnCancelar" CssClass="btn">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_Cancelar)
       onClick_Cancelar();
}"></ClientSideEvents>
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

                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcPermissoes"
                                        CloseAction="None" HeaderText="Permiss&#245;es" Modal="True"
                                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle"
                                        ShowCloseButton="False" Width="950px" PopupVerticalOffset="20"
                                        ID="pcPermissoes" AllowDragging="True" Height="450px" ShowFooter="True" PopupAlignCorrection="Disabled">
                                        <ClientSideEvents PopUp="function(s, e) {
    var largura = Math.max(0, document.documentElement.clientWidth) - 100;
    var altura = Math.max(0, document.documentElement.clientHeight) - 155;

    s.SetWidth(largura);
    s.SetHeight(altura);

    s.UpdatePosition();
	
pnCallbackPermissoes.PerformCallback();
}"></ClientSideEvents>

                                        <ContentStyle>
                                            <Paddings PaddingLeft="5px" PaddingTop="5px" PaddingRight="5px" PaddingBottom="5px"></Paddings>
                                        </ContentStyle>

                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <FooterTemplate>
                                            <div style="float:right">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" ID="btnSalvar" Theme="MaterialCompact">
                                                                                    <ClientSideEvents Click="function(s, e){
	if (window.SalvarCamposFormulario)
    {
		TipoOperacao += 'Permissao';
        if (SalvarCamposFormulario())
	    {
    		pcPermissoes.Hide();
        	habilitaModoEdicaoBarraNavegacao(false, gvDados);
            return true;
		}
	}
    else
	{
    	window.top.mostraMensagem('O método não foi implementado!', 'atencao', true, false, null);
	}
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>

                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                    CommandArgument="btnVoltar" Text="Fechar" Width="100px"
                                                                                    ID="btnVoltar" Theme="MaterialCompact">
                                                                                    <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    pcPermissoes.Hide();
    
    gvPermissoes.PerformCallback('VOLTAR');
    
    //if(window.hfGeral &amp;&amp; hfGeral.Contains('CodigoUsuarioPermissao'))
    //{
        //lbListaPerfisDisponivel.PerformCallback(hfGeral.Get('CodigoUsuarioPermissao'));
        //lbListaPerfisSelecionados.PerformCallback(hfGeral.Get('CodigoUsuarioPermissao'));
    //}
        
    //callbackGeral.PerformCallback('CerrarSession');
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                            </div>
                                            
                                        </FooterTemplate>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackPermissoes" ID="pnCallbackPermissoes" OnCallback="pnCallbackPermissoes_Callback" Width="100%">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
    var group = nbAssociacao.GetActiveGroup();
    if (group != null)
    {
        var item = group.GetItem(0);
        if (item != null)
        {
	        var textoItem = item.name;
	        nbAssociacao.SetSelectedItem(item);

            CallBackGvPermissoes(getIniciaisAssociacaoFromTextoMenu(textoItem));
        } // if (item != null)
    } // if (group != null)
}"></ClientSideEvents>
                                                                    <PanelCollection>
                                                                        <dxp:PanelContent runat="server">
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="padding-right: 5px; width: 180px" valign="top">
                                                                                            <div id="divAssociacao" style="height: 290px;overflow-y: auto;">
                                                                                                <dxnb:ASPxNavBar runat="server" AllowSelectItem="True" AutoCollapse="True" ClientInstanceName="nbAssociacao" Width="100%" ID="nbAssociacao">
                                                                                                    <ClientSideEvents ItemClick="function(s, e) {
	onClick_MenuPermissao(s, e);
}"></ClientSideEvents>
                                                                                                    <Groups>
                                                                                                        <dxnb:NavBarGroup Name="gpAssociacao" Text="Tipo Associac&#227;o"></dxnb:NavBarGroup>
                                                                                                    </Groups>

                                                                                                    <Paddings PaddingTop="0px"></Paddings>

                                                                                                    <ItemStyle Wrap="True"></ItemStyle>
                                                                                                </dxnb:ASPxNavBar>
                                                                                            </div>



                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <!-- GRIDVIEW gvPermissoes -->

                                                                                            <dxwgv:ASPxGridView runat="server"
                                                                                                ClientInstanceName="gvPermissoes" KeyFieldName="CodigoPermissao"
                                                                                                AutoGenerateColumns="False" Width="100%"
                                                                                                ID="gvPermissoes" OnCustomCallback="gvPermissoes_CustomCallback"
                                                                                                OnHtmlRowPrepared="gvPermissoes_HtmlRowPrepared"
                                                                                                OnHtmlRowCreated="gvPermissoes_HtmlRowCreated"
                                                                                                OnAfterPerformCallback="gvPermissoes_AfterPerformCallback">
                                                                                                <ClientSideEvents EndCallback="function(s, e) {
	//if(s.cp_RecarregaGvPermissao == &quot;OK&quot;)
	//	gvPermissoes.PerformCallback('ATL');
}" Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 195;
s.SetHeight(sHeight);
}"></ClientSideEvents>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                                                                        VisibleIndex="0" Width="40px">
                                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="imagemIcone" Name="imagemIcone"
                                                                                                        Width="30px" Caption="Inf" VisibleIndex="1">
                                                                                                        <Settings AllowAutoFilter="False" />
                                                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                                                        <DataItemTemplate>
                                                                                                            <%#  (Eval("imagemIcono") != null &&  Eval("imagemIcono").ToString() != "")   ? string.Format("<img title='' alt='' style='border:0px' src='../imagens/Perfis/{0}'/>", Eval("imagemIcono").ToString()) : "<img title='' alt='' style='border:0px' src='../imagens/botoes/blank.gif'/>"%>
                                                                                                        </DataItemTemplate>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoItemPermissao" GroupIndex="0"
                                                                                                        SortIndex="0" SortOrder="Ascending" Name="DescricaoItemPermissao" Caption=" "
                                                                                                        VisibleIndex="2">
                                                                                                        <Settings AllowDragDrop="False" AllowGroup="True" AllowAutoFilter="False"></Settings>

                                                                                                        <GroupFooterCellStyle Font-Italic="True"></GroupFooterCellStyle>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcaoPermissao" ReadOnly="True"
                                                                                                        Name="DescricaoAcaoPermissao" Caption="Permiss&#245;es" VisibleIndex="3">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="Concedido" Name="Concedido" Width="80px"
                                                                                                        Caption="Conceder" VisibleIndex="4">
                                                                                                        <PropertiesCheckEdit ClientInstanceName="chkConceder"></PropertiesCheckEdit>
                                                                                                        <Settings AllowAutoFilter="False" />

                                                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                                                        <DataItemTemplate>
                                                                                                            <%# getCheckBox("CheckConcedido", "Concedido", "C")%>
                                                                                                        </DataItemTemplate>

                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="Delegavel" Name="Delegavel" Width="80px"
                                                                                                        Caption="Extens&#237;vel" VisibleIndex="5">
                                                                                                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="N&#227;o" ClientInstanceName="chkExtensivel"></PropertiesCheckEdit>
                                                                                                        <Settings AllowAutoFilter="False" />

                                                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                                                        <DataItemTemplate>
                                                                                                            <%# getCheckBox("CheckDelegavel", "Delegavel", "D")%>
                                                                                                        </DataItemTemplate>

                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="Negado" Name="Negado" Width="80px"
                                                                                                        Caption="Negar" VisibleIndex="6">
                                                                                                        <PropertiesCheckEdit ClientInstanceName="chkNegar"></PropertiesCheckEdit>
                                                                                                        <Settings AllowAutoFilter="False" />

                                                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                                                        <DataItemTemplate>
                                                                                                            <%# getCheckBox("CheckNegado", "Negado", "N")%>
                                                                                                        </DataItemTemplate>

                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="Incondicional" Name="Incondicional"
                                                                                                        Width="100px" Caption="Incondicional" VisibleIndex="8">
                                                                                                        <PropertiesCheckEdit ClientInstanceName="chkIncondicional" EnableClientSideAPI="True"></PropertiesCheckEdit>
                                                                                                        <Settings AllowAutoFilter="False" />

                                                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                                                        <DataItemTemplate>
                                                                                                            <%# getCheckBox("CheckIncondicional", "Incondicional", "I")%>
                                                                                                        </DataItemTemplate>

                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="Herdada" Caption="Herdadas" Visible="False"
                                                                                                        VisibleIndex="10">
                                                                                                        <PropertiesTextEdit ClientInstanceName="txtHerdada"></PropertiesTextEdit>

                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="FlagsOrigem" Caption="FlagsOrigem"
                                                                                                        Visible="False" VisibleIndex="11">
                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="Outorgavel" Name="Outorgavel"
                                                                                                        Caption="Outorgavel" Visible="False" VisibleIndex="9">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoPermissao" Name="CodigoPermissao"
                                                                                                        Caption="CodigoPermissao" Visible="False" VisibleIndex="7">
                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ReadOnly" Name="ReadOnly" Visible="False"
                                                                                                        VisibleIndex="12">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="imagemReadOnly" Name="imagemReadOnly"
                                                                                                        Visible="False" VisibleIndex="13">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="IncondicionalBloqueado"
                                                                                                        Name="IncondicionalBloqueado" Caption="IncondicionalBloqueado" Visible="False"
                                                                                                        VisibleIndex="14">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>

                                                                                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                                                                <Settings ShowTitlePanel="True" ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="170"></Settings>

                                                                                                <SettingsText></SettingsText>

                                                                                                <Styles>
                                                                                                    <Disabled BackColor="#EBEBEB" ForeColor="Black"></Disabled>
                                                                                                </Styles>

                                                                                                <Templates>
                                                                                                    <FooterRow>
                                                                                                        <table>
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxImage ID="ASPxImage1" runat="server"
                                                                                                                            ImageUrl="~/imagens/Perfis/Editado.png">
                                                                                                                        </dxe:ASPxImage>
                                                                                                                    </td>
                                                                                                                    <td style="padding-right: 10px; padding-left: 3px">
                                                                                                                        <dxe:ASPxLabel ID="lblAjudaPersonalizada" runat="server"
                                                                                                                            Font-Size="7pt" Text="Permissão personalizada">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td style="border-right: green 2px solid; border-top: green 2px solid; border-left: green 2px solid; width: 10px; border-bottom: green 2px solid; background-color: #ddffcc; border-width: 1px; border-color: #808080;">&nbsp;</td>
                                                                                                                    <td style="padding-right: 10px; padding-left: 3px">
                                                                                                                        <dxe:ASPxLabel ID="lblAjudaBloqueada" runat="server"
                                                                                                                            Font-Size="7pt" Text="Permissão não editável">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxImage ID="ASPxImage3" runat="server"
                                                                                                                            ImageUrl="~/imagens/Perfis/Perfil_Herdada.png">
                                                                                                                        </dxe:ASPxImage>
                                                                                                                    </td>
                                                                                                                    <td style="padding-right: 10px; padding-left: 3px">
                                                                                                                        <dxe:ASPxLabel ID="lblAjudaHerdada" runat="server"
                                                                                                                            Font-Size="7pt" Text="Permissão herdada">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </FooterRow>
                                                                                                </Templates>
                                                                                            </dxwgv:ASPxGridView>


                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 10px" align="right">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="padding-left: 5px; padding-bottom: 5px">
                                                                                <table class="auto-style1">
                                                                                    <tr>
                                                                                        <td style="width:33.33%" align="left">
                                                                                            <dxtv:ASPxLabel ID="lblInteressado" runat="server" ClientInstanceName="lblInteressado" Font-Bold="False" ForeColor="#404040" Text="Interessado: ">
                                                                                            </dxtv:ASPxLabel>
                                                                                            <dxtv:ASPxLabel ID="lblCaptionInteressado" runat="server" ClientInstanceName="lblCaptionInteressado" Font-Bold="True" ForeColor="#404040">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width:33.33%" align="middle">
                                                                                            <dxtv:ASPxLabel ID="lblPerfis" runat="server" ClientInstanceName="lblPerfis" Font-Bold="False" ForeColor="#404040" Text="Perfis: ">
                                                                                            </dxtv:ASPxLabel>
                                                                                            <dxtv:ASPxLabel ID="lblCaptionPerfil" runat="server" ClientInstanceName="lblCaptionPerfil" Font-Bold="True" ForeColor="#404040">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                        <td align="right" style="width:33.33%">
                                                                                            <dxtv:ASPxLabel ID="lblHerda" runat="server" ClientInstanceName="lblHerda" Font-Bold="False" ForeColor="#404040" Text="Herdar permissões do objeto superior?:">
                                                                                            </dxtv:ASPxLabel>
                                                                                            <dxtv:ASPxLabel ID="lblCaptionHerda" runat="server" ClientInstanceName="lblCaptionHerda" Font-Bold="True" ForeColor="#404040">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackGeral" ID="callbackGeral" OnCallback="callbackGeral_Callback"></dxcb:ASPxCallback>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                    <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackConceder" ID="callbackConceder" OnCallback="callbackConceder_Callback">
                                        <ClientSideEvents EndCallback="function(s, e) {
	gvPermissoes.PerformCallback('ATL');
}"></ClientSideEvents>
                                    </dxcb:ASPxCallback>
                                </dxp:PanelContent>
                            </PanelCollection>

                            <ClientSideEvents EndCallback="function(s, e) {
    if (hfGeral.Get('StatusSalvar') == '1')
    {
        if (TipoOperacao == 'Incluir')
        {
            TipoOperacao = 'Editar';
            hfGeral.Set('TipoOperacao', TipoOperacao);
        }

	    if('Incluir' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao('Perfil incluído com sucesso!');
	    else if('Editar' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao('Perfil alterado com sucesso!');
		else if('Excluir' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao('Perfil excluido com sucesso!');
	    else if('IncluirPermissao' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao('Permissão incluído com sucesso!');
	    else if('EditarPermissao' == s.cp_OperacaoOk)
    	    mostraPopupMensagemGravacao('Permissão alterado com sucesso!');
    
	    callbackGeral.PerformCallback('CerrarSession');
		onClick_Cancelar(); //onClick_btnCancelar();
    }
    else if (hfGeral.Get('StatusSalvar') == '0')
    {
        mensagemErro = hfGeral.Get('ErroSalvar');
        
        if (TipoOperacao == 'Excluir')
        {
            // se existe um tratamento de erro especifico da opçao que está sendo executada
            if (window.trataMensagemErro)
                mensagemErro = window.trataMensagemErro(TipoOperacao, mensagemErro);
            else // caso contrário, usa o tratamento padrão
            {
                // se for erro de Chave Estrangeira (FK)
                if (mensagemErro.indexOf('REFERENCE') &gt;= 0 )
                    mensagemErro = 'O registro não pode ser excluído pois está sendo utilizado por outro';
            }
        }
        window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
    }
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px;"
                        align="right">
                        <dxe:ASPxButton runat="server" AutoPostBack="False"
                            CommandArgument="btnFecharModal" Text="Fechar" Width="100px"
                            ID="btnFecharModal">
                            <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>
                        </dxe:ASPxButton>



                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
