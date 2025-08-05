<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tramitacao.aspx.cs" Inherits="workflow_Tramitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

a.dxbButton
{
    color: #0d45b7;
    text-decoration: underline;
}
a.dxbButtonSys
{
    border: 0;
    background: none;
    padding: 0;
}
.dxbButton
{
	color: #000000;
	font: 12px Tahoma, Geneva, sans-serif;
	border: 1px solid #7F7F7F;
	padding: 1px;
}
.dxbButtonSys /*Bootstrap correction*/
{
    -webkit-box-sizing: content-box;
    -moz-box-sizing: content-box;
    box-sizing: content-box;
}
.dxbButtonSys
{
	cursor: pointer;
	display: inline-block;
	text-align: center;
	white-space: nowrap;
}
a.dxbButtonSys > span
{
    text-decoration: inherit;
}
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 166px;
        }
        .style5
        {
            height: 5px;
        }
        .style6
        {
            width: 212px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                Width="100%" OnCallback="pnCallback_Callback">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados"
                            KeyFieldName="CodigoModeloFormulario" AutoGenerateColumns="False" Width="100%"
                            ID="gvDados"
                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}"
                                CustomButtonClick="function(s, e) 
{
     LimpaCamposFormulario();
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
	hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
	TipoOperacao = &quot;Incluir&quot;;
	onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);	
OnGridFocusedRowChanged(gvDados, true);
	
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
	onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
      	btnSalvar.SetVisible(false);
	hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
	TipoOperacao = &quot;Consultar&quot;;
OnGridFocusedRowChanged(gvDados, true);
	pcDados.Show();
     }
    else if(e.buttonID == &quot;btnHistorico&quot;)
    {
	gvHistorico.PerformCallback();
    }	
}
"></ClientSideEvents>
                            <Columns>
                                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                    Width="120px" Caption=" " VisibleIndex="0">
                                    <CustomButtons>
                                        <dxcp:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                        </dxcp:GridViewCommandColumnCustomButton>
                                        <dxcp:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                        </dxcp:GridViewCommandColumnCustomButton>
                                        <dxcp:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                            <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                        </dxcp:GridViewCommandColumnCustomButton>
                                        <dxtv:GridViewCommandColumnCustomButton ID="btnHistorico" Text="Histórico">
                                            <Image AlternateText="Histórico" ToolTip="Histórico"
                                                Url="~/imagens/botoes/btnHistorico.png">
                                            </Image>
                                        </dxtv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>

                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                </dxcp:GridViewCommandColumn>
                                <dxcp:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Formulário"
                                    VisibleIndex="1" FieldName="TituloFormulario">
                                </dxcp:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="2" Width="160px"
                                    FieldName="DescricaoStatus">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="3"
                                    Width="180px" FieldName="Responsavel">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataDateColumn Caption="Solicitação" VisibleIndex="4"
                                    Width="90px" FieldName="DataSolicitacaoTramitacao">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <ExportCellStyle HorizontalAlign="Center">
                                    </ExportCellStyle>
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataDateColumn Caption="Prazo" VisibleIndex="5" Width="90px"
                                    FieldName="DataPrevistaConclusao">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <ExportCellStyle HorizontalAlign="Center">
                                    </ExportCellStyle>
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataTextColumn FieldName="AssuntoMensagemNotificacao"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn FieldName="TextoMensagemNotificacao"
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                </dxtv:GridViewDataTextColumn>
                            </Columns>

                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                            <Settings VerticalScrollBarMode="Visible"></Settings>

                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>

                            <Templates>
                                <FooterRow>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido" ID="lblDescricaoConcluido"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="width: 10px; background-color: green"></td>
                                                <td style="width: 10px" align="center">|</td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada" ID="lblDescricaoAtrasada"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="width: 10px; background-color: red"></td>
                                                <td style="width: 10px" align="center">|</td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes" ID="lblDescricaoAnotacoes"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; border-right-width: 0px" src="../imagens/anotacao.gif" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </FooterRow>
                            </Templates>
                        </dxcp:ASPxGridView>



                        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" CloseAction="None"
                            ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False"
                            Width="790px" ID="pcDados">
                            <ClientSideEvents Closing="function(s, e) {
	pcBloqueio.Hide();
}" />
                            <ContentStyle>
                                <Paddings Padding="5px"></Paddings>
                            </ContentStyle>

                            <HeaderStyle Font-Bold="True">
                                <Paddings Padding="2px" PaddingLeft="10px" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dxcp:PopupControlContentControl runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                Text="Assunto: *">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                        <td class="style6">
                                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                Text="Responsável: *">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                        <td class="style3">
                                                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                Text="Prazo para Preenchimento: *">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 10px">
                                                            <dxtv:ASPxTextBox ID="txtAssunto" runat="server"
                                                                ClientInstanceName="txtAssunto"
                                                                MaxLength="500" Width="100%">
                                                            </dxtv:ASPxTextBox>
                                                        </td>
                                                        <td class="style6" style="padding-right: 10px">
                                                            <dxtv:ASPxComboBox ID="ddlResponsavel" runat="server"
                                                                ClientInstanceName="ddlResponsavel" DropDownStyle="DropDown"
                                                                EnableCallbackMode="True"
                                                                OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario"
                                                                Width="100%">
                                                                <ClientSideEvents Init="function(s, e) {
	
}"
                                                                    SelectedIndexChanged="function(s, e) {
	
}" />
                                                                <Columns>
                                                                    <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="200px" />
                                                                    <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Width="300px" />
                                                                </Columns>
                                                                <SettingsLoadingPanel Text="Carregando;" />
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxComboBox>
                                                        </td>
                                                        <td class="style3">
                                                            <dxtv:ASPxDateEdit ID="ddlPrazo" runat="server" ClientInstanceName="ddlPrazo"
                                                                Width="100%">
                                                                <ClientSideEvents Init="function(s, e) {
	s.SetMinDate(new&nbsp;Date() ) ;
}" />
                                                            </dxtv:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"
                                                    Text="Mensagem: *">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxMemo ID="mmObjeto" runat="server" ClientInstanceName="mmObjeto"
                                                    Rows="6" Width="100%">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxtv:ASPxMemo>
                                                <dxtv:ASPxLabel ID="lbl_mmObjeto" runat="server"
                                                    ClientInstanceName="lbl_mmObjeto" Font-Bold="True"
                                                    Font-Size="7pt" ForeColor="#999999">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxGridView ID="gvFormulariosBloqueados" runat="server"
                                                    AutoGenerateColumns="False" ClientInstanceName="gvFormulariosBloqueados"
                                                    OnCustomCallback="gvFormulariosBloqueados_CustomCallback" Width="100%"
                                                    KeyFieldName="CodigoModeloFormulario">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
 if(confirm('Deseja desbloquear o formulário?'))
    gvFormulariosBloqueados.PerformCallback('Excluir');
}
"
                                                        EndCallback="function(s, e) {
	pcBloqueio.Hide();   
}" />
                                                    <Columns>
                                                        <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="50px">
                                                            <CustomButtons>
                                                                <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                    </Image>
                                                                </dxtv:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle>
                                                                <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                                            </HeaderStyle>
                                                            <HeaderTemplate>
                                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""ddlForms.PerformCallback();"" style=""cursor: pointer;""/>")%>
                                                            </HeaderTemplate>
                                                        </dxtv:GridViewCommandColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Formulário"
                                                            ShowInCustomizationForm="True" VisibleIndex="2"
                                                            FieldName="TituloFormulario">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dxtv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                    </SettingsPager>
                                                    <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible"
                                                        VerticalScrollableHeight="90" />
                                                    <SettingsText Title="Bloqueio de Formulários" />
                                                    <Styles>
                                                        <TitlePanel>
                                                            <Paddings Padding="1px" />
                                                        </TitlePanel>
                                                    </Styles>
                                                </dxtv:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnSalvar"
                                                                    Text="Salvar" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;	
	
	if(hfGeral.Get('CIWF') == '-1')
		gravaInstanciaWf();
	else
	{
    		if (window.onClick_btnSalvar)
	    		onClick_btnSalvar();
	}
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnFechar"
                                                                    Text="Fechar" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dxcp:PopupControlContentControl>
                            </ContentCollection>
                        </dxcp:ASPxPopupControl>

                        <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>

                        <dxcp:ASPxHiddenField runat="server"
                            ClientInstanceName="hfGeral" ID="hfGeral">
                        </dxcp:ASPxHiddenField>

                        <dxtv:ASPxPopupControl ID="pcBloqueio" runat="server"
                            ClientInstanceName="pcBloqueio"
                            HeaderText="Bloqueio" Width="550px" CloseAction="None"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            ShowCloseButton="False">
                            <ContentCollection>
                                <dxtv:PopupControlContentControl runat="server">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"
                                                    Text="Formulário:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxComboBox ID="ddlForms" runat="server" ClientInstanceName="ddlForms"
                                                    Width="100%">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	pcBloqueio.Show();
}" />
                                                </dxtv:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxButton ID="btnSalvarBloqueio" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnSalvarBloqueio"
                                                                    Text="Salvar" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
if(ddlForms.GetValue() != null)	
		gvFormulariosBloqueados.PerformCallback('Atualizar');
	else
		window.top.mostraMensagem('Escolha um formulário para bloquear!', 'Atencao', true, false, null);
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <dxtv:ASPxButton ID="btnFecharBloqueio" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnFecharBloqueio"
                                                                    Text="Fechar" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
pcBloqueio.Hide();   
}" />
                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dxtv:PopupControlContentControl>
                            </ContentCollection>
                        </dxtv:ASPxPopupControl>

                        <dxtv:ASPxPopupControl ID="pcHistorico" runat="server"
                            ClientInstanceName="pcHistorico" CloseAction="None"
                            HeaderText="Histórico" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="800px">
                            <ContentCollection>
                                <dxtv:PopupControlContentControl runat="server">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxGridView ID="gvHistorico" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="gvHistorico"
                                                    KeyFieldName="CodigoTramitacaoEtapaFluxo" Width="100%"
                                                    OnCustomCallback="gvHistorico_CustomCallback">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	pcHistorico.Show();
}" />
                                                    <Columns>
                                                        <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="2" Width="200px"
                                                            FieldName="DescricaoStatus">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="1"
                                                            FieldName="Responsavel">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataDateColumn Caption="Solicitação" VisibleIndex="3"
                                                            Width="90px" FieldName="DataSolicitacaoTramitacao">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <ExportCellStyle HorizontalAlign="Center">
                                                            </ExportCellStyle>
                                                        </dxtv:GridViewDataDateColumn>
                                                        <dxtv:GridViewDataDateColumn Caption="Prazo" VisibleIndex="4" Width="90px"
                                                            FieldName="DataPrevistaConclusao">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <ExportCellStyle HorizontalAlign="Center">
                                                            </ExportCellStyle>
                                                        </dxtv:GridViewDataDateColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollBarMode="Visible"
                                                        VerticalScrollableHeight="180" />
                                                    <Styles>
                                                        <Header>
                                                        </Header>
                                                        <Footer>
                                                            <Paddings Padding="0px" />
                                                        </Footer>
                                                    </Styles>
                                                </dxtv:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <dxtv:ASPxButton ID="btnFecharBloqueio0" runat="server" AutoPostBack="False"
                                                                    ClientInstanceName="btnFecharBloqueio"
                                                                    Text="Fechar" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
pcHistorico.Hide();   
}" />
                                                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px"
                                                                        PaddingRight="0px" PaddingTop="0px" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dxtv:PopupControlContentControl>
                            </ContentCollection>
                        </dxtv:ASPxPopupControl>

                    </dxp:PanelContent>
                </PanelCollection>

                <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
	{          
		mostraDivSalvoPublicado(&quot;Tramitação enviada com sucesso!&quot;);         
	}
    	else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tramitação enviada com sucesso!&quot;);
    	else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tramitação excluída com sucesso!&quot;);
}"></ClientSideEvents>
            </dxcp:ASPxCallbackPanel>
        </div>
    </form>
</body>
</html>
