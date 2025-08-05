<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComentarioRDO.aspx.cs" Inherits="_Projetos_DadosProjeto_ComentarioRDO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td align="center" class="style2" style="border: 1px solid #808080; background-color: #FFFFEC;">
                    <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                        Font-Size="10pt" ClientInstanceName="lblTituloTela">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxhe:ASPxHtmlEditor ID="htmlComentarios" runat="server" ClientInstanceName="htmlComentarios"
                         Width="100%">
                        <Toolbars>
                            <dxhe:HtmlEditorToolbar Name="StandardToolbar2">
                                <Items>
                                    <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                        <Items>
                                            <dxhe:ToolbarListEditItem Text="Normal" Value="p" />
                                            <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                            <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                            <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                            <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                            <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                            <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                            <dxhe:ToolbarListEditItem Text="Address" Value="address" />
                                            <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                        </Items>
                                    </dxhe:ToolbarParagraphFormattingEdit>
                                    <dxhe:ToolbarFontNameEdit Width="150px">
                                        <Items>
                                            <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                            <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                            <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                            <dxhe:ToolbarListEditItem Text="Arial" Value="Arial" />
                                            <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                            <dxhe:ToolbarListEditItem Text="Courier" Value="Courier" />
                                        </Items>
                                    </dxhe:ToolbarFontNameEdit>
                                    <dxhe:ToolbarFontSizeEdit>
                                        <Items>
                                            <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                            <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                            <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                            <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                            <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                            <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                            <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                        </Items>
                                    </dxhe:ToolbarFontSizeEdit>
                                    <dxhe:ToolbarBoldButton BeginGroup="True">
                                    </dxhe:ToolbarBoldButton>
                                    <dxhe:ToolbarItalicButton>
                                    </dxhe:ToolbarItalicButton>
                                    <dxhe:ToolbarUnderlineButton>
                                    </dxhe:ToolbarUnderlineButton>
                                    <dxhe:ToolbarStrikethroughButton>
                                    </dxhe:ToolbarStrikethroughButton>
                                    <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                    </dxhe:ToolbarJustifyLeftButton>
                                    <dxhe:ToolbarJustifyCenterButton>
                                    </dxhe:ToolbarJustifyCenterButton>
                                    <dxhe:ToolbarJustifyRightButton>
                                    </dxhe:ToolbarJustifyRightButton>
                                    <dxhe:ToolbarBackColorButton BeginGroup="True">
                                    </dxhe:ToolbarBackColorButton>
                                    <dxhe:ToolbarFontColorButton>
                                    </dxhe:ToolbarFontColorButton>
                                    <dxhe:ToolbarFullscreenButton>
                                    </dxhe:ToolbarFullscreenButton>
                                </Items>
                            </dxhe:HtmlEditorToolbar>
                        </Toolbars>
                        <Settings AllowHtmlView="False" AllowInsertDirectImageUrls="False" AllowPreview="False" />
                    </dxhe:ASPxHtmlEditor>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
                        OnCallback="callbackSalvar_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg == '')
	{
		window.top.mostraMensagem('Comentário salvo com sucesso!', 'sucesso', false, false, null); 
		window.top.fechaModal();
	}
	else
	{
		window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null); 
	}
}" />
                    </dxcb:ASPxCallback>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                        Text="Salvar" Width="100px"  ID="btnSalvar">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    callbackSalvar.PerformCallback();
}"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
