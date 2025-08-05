<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaUsuariosMensagem.aspx.cs" Inherits="espacoTrabalho_ListaUsuariosMensagem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        .style1
        {
            width: 100%;
        }
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
	color: Black;
}

.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px 5px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxeBase
{
	font: 12px Tahoma;
}
.dxICheckBox 
{
    cursor: default;
	margin: auto;
	display: inline-block;
	vertical-align: middle;
}
.dxWeb_edtCheckBoxUnchecked {
	background-position: -41px -99px;
}

.dxWeb_edtCheckBoxChecked,
.dxWeb_edtCheckBoxUnchecked,
.dxWeb_edtCheckBoxGrayed,
.dxWeb_edtCheckBoxCheckedDisabled,
.dxWeb_edtCheckBoxUncheckedDisabled,
.dxWeb_edtCheckBoxGrayedDisabled {
	
    background-repeat: no-repeat;
    background-color: transparent;
    width: 15px;
    height: 15px;
}
.dxgvFocusedRow
{
	background-color: #8D8D8D;
	color: White;
}
.dxgvCommandColumn
{
	padding: 2px;
}

.dxgvPagerTopPanel,
.dxgvPagerBottomPanel
{
	padding-top: 4px;
	padding-bottom: 4px;
}

.dxpControl
{
	font: 12px Tahoma;
	color: black;
}
.dxpSummary
{
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 1px 4px 0px;
}
.dxpDisabled
{
	color: #acacac;
	border-color: #808080;
	cursor: default;
}
.dxpDisabledButton
{
	text-decoration: none;
}
.dxpButton
{
	color: #394EA2;
	text-decoration: underline;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}

.dxWeb_pPrevDisabled {
    background-position: -105px -25px;
    width: 16px;
    height: 17px;
}

.dxWeb_pPopOut,
.dxWeb_pPopOutDisabled,
.dxWeb_pAll,
.dxWeb_pAllDisabled,
.dxWeb_pPrev,
.dxWeb_pPrevDisabled,
.dxWeb_pNext,
.dxWeb_pNextDisabled,
.dxWeb_pLast,
.dxWeb_pLastDisabled,
.dxWeb_pFirst,
.dxWeb_pFirstDisabled {
    display: inline;
}

.dxWeb_rpHeaderTopLeftCorner,
.dxWeb_rpHeaderTopRightCorner,
.dxWeb_rpBottomLeftCorner,
.dxWeb_rpBottomRightCorner,
.dxWeb_rpTopLeftCorner,
.dxWeb_rpTopRightCorner,
.dxWeb_rpGroupBoxBottomLeftCorner,
.dxWeb_rpGroupBoxBottomRightCorner,
.dxWeb_rpGroupBoxTopLeftCorner,
.dxWeb_rpGroupBoxTopRightCorner,
.dxWeb_mHorizontalPopOut,
.dxWeb_mVerticalPopOut,
.dxWeb_mVerticalPopOutRtl,
.dxWeb_mSubMenuItem,
.dxWeb_mSubMenuItemChecked,
.dxWeb_mScrollUp,
.dxWeb_mScrollDown,
.dxWeb_tcScrollLeft,
.dxWeb_tcScrollRight,
.dxWeb_tcScrollLeftHover,
.dxWeb_tcScrollRightHover,
.dxWeb_tcScrollLeftPressed,
.dxWeb_tcScrollRightPressed,
.dxWeb_tcScrollLeftDisabled,
.dxWeb_tcScrollRightDisabled,
.dxWeb_nbCollapse,
.dxWeb_nbExpand,
.dxWeb_splVSeparator,
.dxWeb_splVSeparatorHover,
.dxWeb_splHSeparator,
.dxWeb_splHSeparatorHover,
.dxWeb_splVCollapseBackwardButton,
.dxWeb_splVCollapseBackwardButtonHover,
.dxWeb_splHCollapseBackwardButton,
.dxWeb_splHCollapseBackwardButtonHover,
.dxWeb_splVCollapseForwardButton,
.dxWeb_splVCollapseForwardButtonHover,
.dxWeb_splHCollapseForwardButton,
.dxWeb_splHCollapseForwardButtonHover,
.dxWeb_pcCloseButton,
.dxWeb_pcSizeGrip,
.dxWeb_pcSizeGripRtl,
.dxWeb_pPopOut,
.dxWeb_pPopOutDisabled,
.dxWeb_pAll,
.dxWeb_pAllDisabled,
.dxWeb_pPrev,
.dxWeb_pPrevDisabled,
.dxWeb_pNext,
.dxWeb_pNextDisabled,
.dxWeb_pLast,
.dxWeb_pLastDisabled,
.dxWeb_pFirst,
.dxWeb_pFirstDisabled,
.dxWeb_tvColBtn,
.dxWeb_tvColBtnRtl,
.dxWeb_tvExpBtn,
.dxWeb_tvExpBtnRtl,
.dxWeb_fmFolder,
.dxWeb_fmFolderLocked,
.dxWeb_fmCreateButton,
.dxWeb_fmMoveButton,
.dxWeb_fmRenameButton,
.dxWeb_fmDeleteButton,
.dxWeb_fmRefreshButton,
.dxWeb_fmDwnlButton,
.dxWeb_fmCreateButtonDisabled,
.dxWeb_fmMoveButtonDisabled,
.dxWeb_fmRenameButtonDisabled,
.dxWeb_fmDeleteButtonDisabled,
.dxWeb_fmRefreshButtonDisabled,
.dxWeb_fmDwnlButtonDisabled,
.dxWeb_ucClearButton {
    background-repeat: no-repeat;
    background-color: transparent;
    display:block;
}

.dxpCurrentPageNumber
{
	font-weight: bold;
	text-decoration: none;
	padding: 1px 3px 0px;
	white-space: nowrap;
}
.dxpPageNumber
{
	color: #394EA2;
	text-decoration: underline;
	text-align: center;
	vertical-align: middle;
	padding: 1px 5px 0px;
}

.dxWeb_pNextDisabled {
    background-position: -81px -25px;
    width: 16px;
    height: 17px;
}

        .style7
        {
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
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

		var destinatarios = txtDestinatarios.GetText() + &quot;'&quot; + chave.split('|')[0].replace('&lt;', '').replace('&gt;', '').replace(/\s+$/,'') + &quot;'&lt;&quot; + chave.split('|')[1] + &quot;&gt;;&quot;

		txtDestinatarios.SetText(destinatarios);
	}
    else
    {
		var chave = s.GetRowKey(e.visibleIndex);

		var destinatarios = txtDestinatarios.GetText().replace(&quot;'&quot; + chave.split('|')[0].replace('&lt;', '').replace('&gt;', '').replace(/\s+$/,'') + &quot;'&lt;&quot; + chave.split('|')[1] + &quot;&gt;;&quot;, &quot;&quot;);

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

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" ShowFilterRow="True"
                                                                        VerticalScrollBarStyle="Virtual"></Settings>

<SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>

                                                                    <SettingsLoadingPanel Mode="ShowAsPopup" Text="Carregando&amp;hellip;" />

<Styles>
<SelectedRow BackColor="Transparent" ForeColor="Black"></SelectedRow>
</Styles>
</dxwgv:ASPxGridView>

                                                            </td>
            </tr>
            <tr>
                <td class="style7">
                </td>
            </tr>
            <tr>
                <td>
                                                                <dxe:ASPxLabel runat="server" 
                        Text="Destinat&#225;rios:" ClientInstanceName="lblDestinatarios" 
                         ID="lblDestinatarios"></dxe:ASPxLabel>

                                                            </td>
            </tr>
            <tr>
                <td>
                                                                <dxe:ASPxMemo runat="server" Rows="5" Width="100%" 
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
            <tr>
                <td class="style7">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="WIDTH: 60px">
    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSelecionar" 
                            Text="Selecionar" Width="90px"  
                            ID="btnSelecionar">
<ClientSideEvents Click="function(s, e) {	
		window.parent.txtDestinatarios.SetText(txtDestinatarios.GetText());
        window.top.fechaModal();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 60px">
    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" 
                            Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {	
        window.top.fechaModal();
}"></ClientSideEvents>
</dxe:ASPxButton>

 </td></tr></TBODY></table></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
