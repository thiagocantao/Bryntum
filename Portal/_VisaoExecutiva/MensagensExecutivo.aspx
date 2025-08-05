<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensExecutivo.aspx.cs" Inherits="_VisaoExecutiva_MensagensExecutivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self" />
    <title>Mensagens</title>
    <script type="text/javascript" language="javascript">
        function OnGridFocusedRowChanged(grid) 
        {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Mensagem;Assunto', MontaCampos);
        }
        
        function MontaCampos(values)
        {
            txtMensagem.SetText(values[0]);
            txtResposta.SetText("");
            txtAssunto.SetText(values[1]);
        }
        
        function abreLeitura()
        {
            pcDados.Show();
            txtResposta.SetEnabled(false);
            btnSalvar.SetVisible(false);
        }
        
        function abreEdicao()
        {
            pcDados.Show();
            txtResposta.SetEnabled(true);
            btnSalvar.SetVisible(true);
        }        
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvMensagens" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="gvMensagens"  KeyFieldName="CodigoMensagem" OnHtmlDataCellPrepared="gvMensagens_HtmlDataCellPrepared" OnCustomButtonInitialize="gvMensagens_CustomButtonInitialize" Width="100%">
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?"  />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="9%">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnLer" Text="Ler">
                                        <Image Url="~/imagens/botoes/lerMensagem.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnResponder" Text="Responder">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mensagem" FieldName="Mensagem" Name="colMensagem"
                                VisibleIndex="0" Width="32%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Autor" FieldName="NomeUsuario" VisibleIndex="1"
                                Width="20%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Data Envio" FieldName="DataInclusao" VisibleIndex="2"
                                Width="10%">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Limite Resposta" FieldName="DataLimiteResposta"
                                VisibleIndex="3" Width="11%">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Resposta Necess&#225;ria" FieldName="RespostaNecessaria"
                                VisibleIndex="4" Width="15%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Assunto" Visible="False" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
	if(e.buttonID == 'btnLer')
		abreLeitura();	
	else
		abreEdicao();
}" />
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharGeral"
                                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                </td>
            </tr>
            </table>
    
    </div>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None"  HeaderText="Mensagem"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
            Width="880px" Modal="True">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                            Text="Assunto:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txtAssunto" runat="server" ClientEnabled="False" ClientInstanceName="txtAssunto"
                             Width="100%">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Mensagem:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientEnabled="False" Rows="8" Width="100%" ClientInstanceName="txtMensagem" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                        Text="Resposta:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtResposta" runat="server" 
                        Rows="8" Width="100%" ClientInstanceName="txtResposta">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr style="height: 35px">
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                         Text="Salvar" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	if(txtResposta.GetText() == &quot;&quot;)
	{
		window.top.mostraMensagem('Digite uma resposta v&#225;lida!', 'atencao', true, false, null);
	}
	else
	{		
		callback.PerformCallback('R');
	}
}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td align="right" style="width: 100px">
                                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                         Text="Fechar" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcDados.Hide();
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
            <ClientSideEvents Closing="function(s, e) {
	btnFecharGeral.SetEnabled(true);
	gvMensagens.PerformCallback();
}" Shown="function(s, e) {
	//OnGridFocusedRowChanged(gvMensagens);
	btnFecharGeral.SetEnabled(false);
	callback.PerformCallback('L');
}" />
        </dxpc:ASPxPopupControl>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MSG != &quot;&quot;)
	{
		if(s.cp_status == 'ok')
            window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(s.cp_MSG, 'erro', true, false, null);
		pcDados.Hide();
		window.top.retornoModal = 'S';		
	}
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
