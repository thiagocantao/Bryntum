<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mensagens.aspx.cs" Inherits="_VisaoExecutiva_MensagensExecutivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self" />
    <title>Mensagens</title>
    <script type="text/javascript" language="javascript">
        var tipoEdicao = "";
        
        function OnGridFocusedRowChanged(grid) 
        {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Mensagem;DataLimiteResposta;', MontaCampos);
        }
        
        function MontaCampos(values)
        {
            txtMensagem.SetText(values[0]);   
            dtePrazo.SetValue(values[1]);         
        } 
        
        function abreEdicao()
        {
            tipoEdicao = "E";
            pcDados.Show();           
            btnSalvar.SetVisible(true);
        }
       
               
        function abreNovaMensagem()
        {
            txtMensagem.SetText("");
            dtePrazo.SetValue(null);  
            tipoEdicao = "I";
            btnSalvar.SetVisible(true);  
            pcDados.Show();           
                
        }
        
        function validaMensagem()
        {

            var retorno = "";
            if(txtMensagem.GetText() == "")
            {
                retorno = "Mensagem deve ser informada";
            }
            if(dtePrazo.GetText() == "")
            {
                retorno = "Prazo deve ser informado";
            }
            else
            {
                if(dtePrazo.GetValue() != null)
                {
                    var dataAtual 	 = new Date();
	                var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	                var dataHoje 	 = Date.parse(meuDataAtual);

	                var dataPrazo = new Date(dtePrazo.GetValue());
                    
                    if(dataPrazo < dataHoje)
                    {
                         retorno = "Prazo não deve ser anterior a data atual";
                    }
                }  
            }            
            
            return retorno;
        }
        
        //para funcionar o label mostrador de quantos caracteres faltam para terminar o texto
        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength) 
                textAreaElement.value = text.substr(0, maxLength);
            else
                lblCantCarater.SetText(text.length);
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
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
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="6%">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnResponder" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderTemplate>
                                    <%# "<img src='../../imagens/botoes/incluirReg02.png' style='cursor: pointer' onclick='abreNovaMensagem();' />"%>
                                </HeaderTemplate>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Mensagem" FieldName="Mensagem" Name="colMensagem"
                                VisibleIndex="0" Width="50%">
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
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{	
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
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Mensagem:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientInstanceName="txtMensagem"
                        Rows="15" Width="100%">
                        <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}" />
                    </dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                         ForeColor="Silver" Text="0">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                        ForeColor="Silver" Text=" de 2000">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
                <tr>
                    <td align="left">
                        <dxe:ASPxLabel ID="lblPrazoResposta" runat="server" 
                            Text="Prazo Para Resposta:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            <tr>
                <td>
                    <table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr style="height: 35px">
                                <td align="left">
                                    <dxe:ASPxDateEdit ID="dtePrazo" runat="server" ClientInstanceName="dtePrazo" EditFormat="Custom"
                                        EditFormatString="dd/MM/yyyy" EnableClientSideAPI="True"
                                        PopupVerticalAlign="NotSet" Width="130px">
                                    </dxe:ASPxDateEdit>
                                    &nbsp;</td>
                                <td align="right">
                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                         Text="Salvar" Width="90px">
                                        <ClientSideEvents Click="function(s, e) {
	
	e.processOnServer = false;
    var valida = validaMensagem(); 	
	if(valida == &quot;&quot;)
	{
        callback.PerformCallback(tipoEdicao);
	}
	else
	{
		window.top.mostraMensagem(valida, 'atencao', true, false, null);
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
	btnFecharGeral.SetEnabled(false);
	callback.PerformCallback('L');
}" />
        </dxpc:ASPxPopupControl>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MSG != '')
	{
		if(s.cp_status == 'ok')
			window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
		else
			window.top.mostraMensagem(s.cp_MSG, 'erro', true, false, null);
		pcDados.Hide();
        if(window.parent.retornoModal)
          window.parent.retornoModal = 'S';
        else
            window.top.retornoModal = 'S';
		
		gvMensagens.PerformCallback();
	}
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
