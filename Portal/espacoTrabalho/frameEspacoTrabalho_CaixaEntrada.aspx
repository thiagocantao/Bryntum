<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/novaCdis.master"
    CodeFile="frameEspacoTrabalho_CaixaEntrada.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_CaixaEntrada" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript">
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle">
                &nbsp; &nbsp;&nbsp;
                <dxe:ASPxLabel ID="lblCaixaDeMensagem" runat="server" Font-Bold="True"
                    Text="Caixa de Mensagens">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <div>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        OnCallback="pnCallback_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tcBandeixa"
                                    Width="100%"  ID="tcBandeixa">
                                    <TabPages>
                                        <dxtc:TabPage Name="tab_Entrada" Text="Entrada">
                                            <ActiveTabImage AlternateText="Entrada" Height="20px" Width="40px">
                                            </ActiveTabImage>
                                            <TabImage Height="15px" Width="35px">
                                            </TabImage>
                                            <ActiveTabStyle >
                                            </ActiveTabStyle>
                                            <TabStyle >
                                                <HoverStyle >
                                                </HoverStyle>
                                            </TabStyle>
                                            <ContentCollection>
                                                <dxw:ContentControl runat="server">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Entradas (Preciso me Posicionar)" Font-Bold="True"
                                                                        Font-Italic="True"  ID="lblCaixaEntrada">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 906px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 317px">
                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvCaixaEntrada" KeyFieldName="CodigoCaixaMensagem"
                                                                        AutoGenerateColumns="False" Width="100%" 
                                                                        ID="gvCaixaEntrada" OnCustomCallback="gvCaixaEntrada_CustomCallback" OnHtmlRowPrepared="gvCaixaEntrada_HtmlRowPrepared">
                                                                        <ClientSideEvents FocusedRowChanged="function(s, e) 
{
}"></ClientSideEvents>
                                                                        <Columns>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="imagemLeitura" Width="30px" Caption=" "
                                                                                Visible="False" VisibleIndex="0">
                                                                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/foruns/{0}' style='border-width:0px;' /&gt;">
                                                                                </PropertiesTextEdit>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Assunto" Caption="Assunto" VisibleIndex="0">
                                                                                <PropertiesTextEdit ClientInstanceName="txtAssunto">
                                                                                </PropertiesTextEdit>
                                                                                <HeaderStyle ></HeaderStyle>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Descricao" Name="colDescricao" Caption="Descri&#231;&#227;o"
                                                                                VisibleIndex="1">
                                                                                <PropertiesTextEdit ClientInstanceName="txtDescricao">
                                                                                </PropertiesTextEdit>
                                                                                <DataItemTemplate>
                                                                                    <span id="Span1" onclick="linkLerMensagem(<%# Eval("CodigoCaixaMensagem")%>, <%#Eval("CodigoTipoAssociacao") %>, <%#Eval("CodigoObjetoAssociado") %>, '<%#Eval("CodigoProjeto")%>' , '<%#Eval("codigoMensagem")%>' , 'S');"
                                                                                        style="cursor: pointer; text-decoration: underline; color: <%# Eval("AcaoAtrasada").ToString() == "True" ? "Red" : "Blue"%>;">
                                                                                        <%# Eval("Descricao")%></span>
                                                                                </DataItemTemplate>
                                                                                <HeaderStyle ></HeaderStyle>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn FieldName="DataInclusao" Width="90px" Caption="Desde"
                                                                                VisibleIndex="2">
                                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="calDesde">
                                                                                </PropertiesDateEdit>
                                                                                <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataDateColumn FieldName="DataPrevistaAcaoMensagem" Width="90px" Caption="Prazo"
                                                                                VisibleIndex="3">
                                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="calPrazo">
                                                                                </PropertiesDateEdit>
                                                                                <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCaixaMensagem" Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Caption="Codigo Objeto Associado"
                                                                                Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Resposta" Name="col_Resposta" Caption="Resposta"
                                                                                Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Name="col_CodigoProjeto" Caption="Codigo Projeto" Visible="False"
                                                                                VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                                                                            VerticalScrollableHeight="250"></Settings>
                                                                        <SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna">
                                                                        </SettingsText>
                                                                        <Styles>
                                                                            <GroupPanel >
                                                                            </GroupPanel>
                                                                        </Styles>
                                                                        <Templates>
                                                                            <FooterRow>
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="margin-left: 5px; width: 10px; background-color: black">
                                                                                            </td>
                                                                                            <td style="padding-right: 10px; padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                                                    Text="Mensagem não lida">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 10px; background-color: red">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                                    Text="Atraso">
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
                                                </dxw:ContentControl>
                                            </ContentCollection>
                                        </dxtc:TabPage>
                                        <dxtc:TabPage Name="tab_Saida" Text="Sa&#237;da">
                                            <ActiveTabImage AlternateText="Saida" Height="20px" Width="40px">
                                            </ActiveTabImage>
                                            <TabImage Height="15px" Width="35px">
                                            </TabImage>
                                            <ActiveTabStyle >
                                            </ActiveTabStyle>
                                            <TabStyle Font-Overline="False">
                                                <HoverStyle >
                                                </HoverStyle>
                                            </TabStyle>
                                            <ContentCollection>
                                                <dxw:ContentControl runat="server">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Sa&#237;da (Aguardo Posicionamento)" Font-Bold="True"
                                                                        Font-Italic="True"  ID="lblCaixaSaida">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvCaixaSaida" KeyFieldName="CodigoCaixaMensagem"
                                                                        AutoGenerateColumns="False" Width="100%" 
                                                                        ID="gvCaixaSaida" OnCustomCallback="gvCaixaSaida_CustomCallback" OnHtmlRowPrepared="gvCaixaSaida_HtmlRowPrepared"
                                                                        OnHtmlDataCellPrepared="gvCaixaMensagens_HtmlDataCellPrepared">
                                                                        <Columns>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Assunto" Caption="Assunto" VisibleIndex="0">
                                                                                <PropertiesTextEdit ClientInstanceName="txtAssunto">
                                                                                </PropertiesTextEdit>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Descricao" Caption="Descri&#231;&#227;o"
                                                                                VisibleIndex="1">
                                                                                <PropertiesTextEdit DisplayFormatString="&lt;a href ='#'&gt;{0}&lt;/a&gt;" ClientInstanceName="txtDescricao">
                                                                                </PropertiesTextEdit>
                                                                                <DataItemTemplate>
                                                                                    <span id="Span1" onclick="linkLerMensagem(<%# Eval("CodigoCaixaMensagem")%>, <%#Eval("CodigoTipoAssociacao") %>, <%#Eval("CodigoObjetoAssociado") %>, '<%#Eval("CodigoProjeto")%>' ,  '<%#Eval("codigoMensagem")%>' ,  'S');"
                                                                                        style="cursor: pointer; text-decoration: underline; color: <%# Eval("AcaoAtrasada").ToString() == "True" ? "Red" : "Blue"%>;">
                                                                                        <%# Eval("Descricao")%></span>
                                                                                </DataItemTemplate>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataDateColumn FieldName="DataInclusao" Width="90px" Caption="Desde"
                                                                                VisibleIndex="2">
                                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="calDesde">
                                                                                </PropertiesDateEdit>
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataDateColumn FieldName="DataPrevistaAcaoMensagem" Width="90px" Caption="Prazo"
                                                                                VisibleIndex="3">
                                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="calPrazo">
                                                                                </PropertiesDateEdit>
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dxwgv:GridViewDataDateColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCaixaMessagem" Visible="False" VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="DataPrimeiroAcessoMensagem" Visible="False"
                                                                                VisibleIndex="4">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                        <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"></Settings>
                                                                        <SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna">
                                                                        </SettingsText>
                                                                        <Styles>
                                                                            <Header >
                                                                            </Header>
                                                                            <EmptyDataRow >
                                                                            </EmptyDataRow>
                                                                            <Cell >
                                                                            </Cell>
                                                                            <GroupPanel >
                                                                            </GroupPanel>
                                                                        </Styles>
                                                                        <Templates>
                                                                            <FooterRow>
                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="margin-left: 5px; width: 10px; background-color: black">
                                                                                            </td>
                                                                                            <td style="padding-right: 10px; padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                                                    Text="Mensagem não lida">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 10px; background-color: red">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                                    Text="Atraso">
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
                                                </dxw:ContentControl>
                                            </ContentCollection>
                                        </dxtc:TabPage>
                                    </TabPages>
                                    <Paddings PaddingLeft="5px"></Paddings>
                                    <TabStyle >
                                    </TabStyle>
                                </dxtc:ASPxPageControl>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                &nbsp;
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
                    executaUrlWfEngine();
		//gvCaixaEntrada.PerformCallback();
                    }"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                    <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                         HeaderText="Detalhes" Modal="True" PopupAction="None"
                        PopupElementID="imgPopup" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ShowCloseButton="False" Width="700px">
                        <ContentStyle>
                            <Paddings Padding="5px"></Paddings>
                        </ContentStyle>
                        <ClientSideEvents Closing="function(s, e) {
	txtMensagem.SetText(&quot;&quot;);
	txtResposta.SetText(&quot;&quot;);
	pcControl.SetActiveTab(pcControl.GetTab(0));
}" Init="function(s, e) {
	pcControl.SetActiveTab(pcControl.GetTab(0));
}"></ClientSideEvents>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnPcDados" ID="pnPcDados"
                                    OnCallback="pnPcDados_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	var text = document.getElementById('txtMensagem');
	if( s.cp_Acao == 'preencherDados')
	{
		if( s.cp_Responder == 'N')
		{
			//text.style.height = 180 + 'px';
			//var auxMensagem = txtMensagem.GetText();
			//txtMensagem.SetHeight(180);
			//txtMensagem.SetText('Hola');
			txtResposta.SetClientVisible(false);
		    btnResponder.SetClientVisible(false);
			document.getElementById('tdResposta').style.display = 'none';
		}
		//if( s.cp_YaRespondido == 'S')
		//{
		//	txtMensagem.SetEnabled(false);
        //	txtResposta.SetEnabled(false);
		//}
		//else
		//{
		//if( s.cp_PodeResponder == 'S')
		//	txtMensagem.SetEnabled(false);
        //	txtResposta.SetEnabled(false);
		//}
		pcDados.Show();
	}
	else if( s.cp_Acao == 'Fechar')
	{
		pcDados.Hide();
		gvCaixaEntrada.PerformCallback();
		gvCaixaSaida.PerformCallback();
	}
	else if( s.cp_Acao == 'Responder')
	{
		pcDados.Hide();
		gvCaixaEntrada.PerformCallback();
		gvCaixaSaida.PerformCallback();
	}
}"></ClientSideEvents>
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcControl"
                                                Width="100%"  ID="pcControl">
                                                <TabPages>
                                                    <dxtc:TabPage Name="tabMen" Text="Mensagem">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="height: 30px">
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 80px">
                                                                                                <dxe:ASPxLabel runat="server" Text="Mensagem:" ClientInstanceName="lblMsg"
                                                                                                    ID="lblMsg">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                &nbsp;<dxe:ASPxLabel runat="server" EncodeHtml="False" ClientInstanceName="lblDAtosMensagem"
                                                                                                     ForeColor="Gray" ID="lblDAtosMensagem">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxMemo runat="server" Height="70px" Width="100%" ClientInstanceName="txtMensagem"
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
                                                                            <td style="padding-bottom: 10px; padding-top: 5px" id="tdResposta">
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel runat="server" Text="Resposta:" ClientInstanceName="lblMsg"
                                                                                                    ID="ASPxLabel2">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxMemo runat="server" Rows="5" Width="100%" ClientInstanceName="txtResposta"
                                                                                                    EnableClientSideAPI="True"  ID="txtResposta">
                                                                                                    <ClientSideEvents KeyUp="function(s, e) {
		limitaASPxMemo(s, 2000);	
}"></ClientSideEvents>
                                                                                                    <DisabledStyle BackColor="WhiteSmoke" Font-Overline="False" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxMemo>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnResponder"
                                                                                    Text="Responder" Width="100px" Height="5px" 
                                                                                    ID="btnResponder">
                                                                                    <ClientSideEvents Click="function(s, e){
	if(txtResposta.GetText()==&quot;&quot;)
	{
		e.processOnServer = false;		
		window.top.mostraMensagem(&quot;O campo resposta da mensagem deve ser informado&quot;, 'atencao', true, false, null);
	}
	else
	{
		hfGeral.Set(&quot;txtResposta&quot;, txtResposta.GetText());
		pnPcDados.PerformCallback(&quot;Responder&quot;);
		//pnCallback.PerformCallback();
		//pcDados.Hide();
	}
}" CheckedChanged="function(s, e) 
{

}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabDes" Text="Destinat&#225;rios">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvListaDestinatarios" KeyFieldName="NomeUsuario"
                                                                                    AutoGenerateColumns="False" Width="100%" 
                                                                                    ID="gvListaDestinatarios" OnCustomCallback="gvCaixaEntrada_CustomCallback" OnHtmlRowPrepared="gvCaixaEntrada_HtmlRowPrepared">
                                                                                    <ClientSideEvents FocusedRowChanged="function(s, e) 
{
}"></ClientSideEvents>
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Caption="Nome" VisibleIndex="0">
                                                                                            <PropertiesTextEdit ClientInstanceName="txtAssunto">
                                                                                            </PropertiesTextEdit>
                                                                                            <HeaderStyle ></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataDateColumn FieldName="DataLeitura" Width="100px" Caption="Data Lectura/Resposta"
                                                                                            VisibleIndex="2">
                                                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="calDesde">
                                                                                            </PropertiesDateEdit>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True" >
                                                                                            </HeaderStyle>
                                                                                            <CellStyle HorizontalAlign="Center">
                                                                                            </CellStyle>
                                                                                        </dxwgv:GridViewDataDateColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                    </SettingsPager>
                                                                                    <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="164">
                                                                                    </Settings>
                                                                                    <SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna">
                                                                                    </SettingsText>
                                                                                    <Styles>
                                                                                        <GroupPanel >
                                                                                        </GroupPanel>
                                                                                    </Styles>
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                            </dxtc:ASPxPageControl>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-top: 5px" align="right">
                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                Text="Fechar" Width="100px" Height="1px" 
                                                                ID="btnFechar">
                                                                <ClientSideEvents Click="function(s, e) 
{
	//pcDados.Hide();
	pnPcDados.PerformCallback(&quot;Fechar&quot;);		
	//pnCallback.PerformCallback();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                    </dxpc:ASPxPopupControl>
                </div>
                <%--       <script type="text/javascript" language="javascript">
       var isIE8 = (window.navigator.userAgent.indexOf("MSIE 8.0") > 0);
       if(isIE8)
       {
            document.forms[0].style.overflowY = "hidden";
       }
       else
       {
            document.forms[0].style.position = "relative";
            document.forms[0].style.overflowY = "hidden";       
       }

    </script>--%>
            </td>
        </tr>
    </table>
</asp:Content>
