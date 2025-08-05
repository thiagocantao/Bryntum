<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsolidacaoAneelEletrobras.aspx.cs"
    Inherits="_VisaoNE_ConsolidacaoAneelEletrobras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function ExibeMensagem() {
            if (hfGeral.Contains("ResultadoOpercao")) {
                var msg = hfGeral.Get("ResultadoOpercao");
                hfGeral.Remove("ResultadoOpercao");
                window.top.mostraMensagem(msg, 'Atencao', true, false, null);
            }
            cursor_clear();
        }

        // Changes the cursor to an hourglass
        function cursor_wait() {
            document.body.style.cursor = 'wait';
        }

        // Returns the cursor to the default pointer
        function cursor_clear() {
            document.body.style.cursor = 'default';
        }
    </script>
</head>
<body onload="ExibeMensagem();">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif); width: 100%">
                <tr>
                    <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px"
                        valign="middle">
                        <table>
                            <tr>
                                <td align="center" style="width: 1px; height: 19px">
                                    <span id="Span2" runat="server"></span>
                                </td>
                                <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                                    <dxe:ASPxLabel ID="lblTitulo" runat="server" Font-Bold="True"
                                        Font-Size="10pt" Text="Consolidação">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td></td>
                    <td style="height: 10px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
                            Width="100%">
                            <TabPages>
                                <dxtc:TabPage Name="tabAneel" Text="ANEEL">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <dxhe:ASPxHtmlEditor ID="htmlAneel" runat="server" Width="100%" ActiveView="Preview"
                                                            ClientEnabled="False" ClientInstanceName="htmlAneel">
                                                            <Settings AllowDesignView="False" AllowHtmlView="False" />
                                                            <SettingsDialogs>
                                                                <InsertImageDialog>
                                                                    <SettingsImageSelector>
                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                    </SettingsImageSelector>
                                                                </InsertImageDialog>
                                                            </SettingsDialogs>
                                                            <SettingsText PreviewTab="Visualizar" />

                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-top: 25px">
                                                        <dxe:ASPxButton ID="btnGerarAneel" runat="server"
                                                            ClientInstanceName="btnGerarAneel"
                                                            OnClick="btnGerar_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if(s.GetText()==&quot;Gerar&quot;)
		cursor_wait();
}" />
                                                            <Image Height="16px" Url="~/imagens/botoes/rtf.png" Width="16px">
                                                            </Image>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabEletrobras" Text="ELETROBRAS">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <dxhe:ASPxHtmlEditor ID="htmlEletrobras" runat="server" ActiveView="Preview" ClientEnabled="False"
                                                            ClientInstanceName="htmlEletrobras" Width="100%">
                                                            <Settings AllowDesignView="False" AllowHtmlView="False" />
                                                            <SettingsDialogs>
                                                                <InsertImageDialog>
                                                                    <SettingsImageSelector>
                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                    </SettingsImageSelector>
                                                                </InsertImageDialog>
                                                            </SettingsDialogs>
                                                            <SettingsText PreviewTab="Visualizar" />
                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-top: 25px">
                                                        <dxe:ASPxButton ID="btnGerarEletrobras" runat="server" ClientInstanceName="btnGerarEletrobras"
                                                            OnClick="btnGerar_Click">
                                                            <ClientSideEvents Click="function(s, e) {
  	if(s.GetText()==&quot;Gerar&quot;)
		cursor_wait();
}" />
                                                            <Image Height="16px" Url="~/imagens/botoes/rtf.PNG" Width="16px">
                                                            </Image>
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>&nbsp;
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
