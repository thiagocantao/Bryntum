<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaUsuariosMensagem.aspx.cs" Inherits="espacoTrabalho_ListaUsuariosMensagem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--<link href="../estilos/custom.css" rel="stylesheet" /> -->
    <script language="javascript" type="text/javascript">

        function GetSelectionInfo(inputElement) {
            var start, end;
            if (ASPxClientUtils.ie) {
                var range = document.selection.createRange();
                var rangeCopy = range.duplicate();
                range.move('character', -inputElement.value.length);
                range.setEndPoint('EndToStart', rangeCopy);
                start = range.text.length;
                end = start + rangeCopy.text.length;
            } else {
                start = inputElement.selectionStart;
                end = inputElement.selectionEnd;
            }
            return { startPos: start, endPos: end };
        }


        function selecionaTexto() {

            if (txtDestinatarios.GetText() != '') {
                var textoMemo = txtDestinatarios.GetText();

                var inputElement = txtDestinatarios.GetInputElement();
                var selectionInfo = GetSelectionInfo(inputElement);


                txtDestinatarios.SetSelection(selectionInfo.startPos, selectionInfo.endPos, true);


                var indexInicio = selectionInfo.startPos;
                var indexFim = selectionInfo.endPos;
                var indexNovoInicio = 0;
                var indexNovoFim = textoMemo.length - 1;
                var i = 0;
                for (i = indexInicio; i >= 0; i--) {
                    if (textoMemo.charAt(i) == ';') {
                        indexNovoInicio = i + 1;
                        break;
                    }
                }

                for (i = indexFim; i < textoMemo.length; i++) {
                    if (textoMemo.charAt(i) == ';') {
                        indexNovoFim = i + 1;
                        break;
                    }
                }

                txtDestinatarios.SetSelection(indexNovoInicio, indexNovoFim, true);
            }

        }

        function removeEmail(s, e) {

            if (txtDestinatarios.GetText() != '') {
                var textoMemo = txtDestinatarios.GetText();

                var inputElement = txtDestinatarios.GetInputElement();
                var selectionInfo = GetSelectionInfo(inputElement);


                txtDestinatarios.SetSelection(selectionInfo.startPos, selectionInfo.endPos, true);


                var indexInicio = selectionInfo.startPos;
                var indexFim = selectionInfo.endPos;
                var indexNovoInicio = 0;
                var indexNovoFim = textoMemo.length - 1;
                var i = 0;
                for (i = indexInicio; i >= 0; i--) {
                    if (textoMemo.charAt(i) == ';') {
                        indexNovoInicio = i + 1;
                        break;
                    }
                }

                for (i = indexFim; i < textoMemo.length; i++) {
                    if (textoMemo.charAt(i) == ';') {
                        indexNovoFim = i + 1;
                        break;
                    }
                }

                var emails = "";

                if (indexNovoInicio == 0) {
                    emails = txtDestinatarios.GetText().substring(indexNovoFim);
                }
                else if (indexNovoFim == (textoMemo.length)) {
                    emails = txtDestinatarios.GetText().substring(0, indexNovoInicio - 1);
                }
                else {
                    emails = txtDestinatarios.GetText().substring(0, indexNovoInicio) + txtDestinatarios.GetText().substring(indexNovoFim);
                }

                txtDestinatarios.SetText(emails);
            }

        }

    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style7 {
            height: 10px;
        }

        .dxbButton {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <dxe:ASPxLabel runat="server"
                            Text="Destinat&#225;rios:" ClientInstanceName="lblDestinatarios"
                            ID="lblDestinatarios">
                        </dxe:ASPxLabel>

                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxMemo runat="server" Rows="3" Width="100%"
                            ClientInstanceName="txtDestinatarios"
                            ID="txtDestinatarios" ClientEnabled="False">
                            <ClientSideEvents Init="function(s, e) {

	var emailsSelecionados = window.parent.txtDestinatarios.GetText();
	
	if(emailsSelecionados  != '')
	{
		var quantidadeLinhas = gvUsuarios.GetVisibleRowsOnPage();
		var i = 0;

		for(i = 0; i &lt; quantidadeLinhas; i++)
		{
			var email = '&lt;' + gvUsuarios.GetRowKey(i).split('|')[1] + '&gt;';
			if(emailsSelecionados.indexOf(email) != -1)
			{
				gvUsuarios.SelectRow(i, true, null);
			}
		}	
	}

	s.SetText(emailsSelecionados);
}"></ClientSideEvents>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="#333333"></DisabledStyle>
                        </dxe:ASPxMemo>
                    </td>
                </tr>
        </table>
        <div>
            <table cellpadding="0" cellspacing="0" class="style1" style="overflow: hidden;">
                <tr>
                    <td>
                        <dxwgv:ASPxGridView runat="server"
                            ClientInstanceName="gvUsuarios" KeyFieldName="NomeUsuario;EMail"
                            AutoGenerateColumns="False" Width="100%"
                            ID="gvUsuarios" EnableRowsCache="False" EnableViewState="False">
                            <ClientSideEvents SelectionChanged="function(s, e) {
	if (e.isSelected)
   {
		var chave = s.GetRowKey(e.visibleIndex);

		var destinatarios = txtDestinatarios.GetText() + &quot;'&quot; + chave.split('|')[0].replace('&lt;', '').replace('&gt;', '').replace(/\s+$/,'') + &quot;'&lt; &quot; + chave.split('|')[1] + &quot; &gt;;&quot;

		txtDestinatarios.SetText(destinatarios);
	}
    else
    {
		var chave = s.GetRowKey(e.visibleIndex);

		var destinatarios = txtDestinatarios.GetText().replace(&quot;'&quot; + chave.split('|')[0].replace('&lt;', '').replace('&gt;', '').replace(/\s+$/,'') + &quot;'&lt; &quot; + chave.split('|')[1] + &quot; &gt;;&quot;, &quot;&quot;);

		txtDestinatarios.SetText(destinatarios);
	}
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                    ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Width="250px" Caption="Nome"
                                    VisibleIndex="1">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="EMail"
                                    Caption="Email" VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager PageSize="20" Visible="False">
                            </SettingsPager>

                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="180" ShowFilterRow="True"
                                VerticalScrollBarStyle="Virtual"></Settings>

                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>

                            <SettingsLoadingPanel Mode="ShowAsPopup" Text="Carregando&amp;hellip;" />

                            <Styles>
                                <SelectedRow BackColor="Transparent" ForeColor="Black"></SelectedRow>
                            </Styles>
                        </dxwgv:ASPxGridView>

                    </td>
                </tr>
            </table>
        </div>
        <table align="right" cellspacing="0" cellpadding="0" border="0" style="margin-top: 10px;">
            <tbody>
                <tr>
                    <td style="width: 60px">
                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSelecionar" CssClass="dxbButton"
                            Text="Selecionar" Width="90px" ID="btnSelecionar">
                            <ClientSideEvents Click="function(s, e) {	
		            window.parent.txtDestinatarios.SetText(txtDestinatarios.GetText());
                    window.top.fechaModal();
            }"></ClientSideEvents>
                        </dxe:ASPxButton>
                    </td>
                    <td style="width: 10px"></td>
                    <td style="width: 60px">
                        <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" CssClass="dxbButton" Text="Fechar"
                            Width="90px" ID="btnFechar">
                            <ClientSideEvents Click="function(s, e) {	
                    window.top.fechaModal();
            }"></ClientSideEvents>
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
