<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formulario_Anexo.aspx.cs" Inherits="formulario_Anexo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        window.name="anexo";
    </script>  
    
      <script type="text/javascript">
        var uploadedFilesFlag;
        function OnBtnUploadClick(s, e){
            lblArquivo.SetText("");
            if(uploadControl.GetText() != ""){
                hf.Set("nomeArquivoOriginal", uploadControl.GetText());
                uploadedFilesFlag = null;
                lblCompleteMessage.SetVisible(false);
                pbUpload.SetPosition(0);
                uploadControl.Upload();
                btnUpload.SetEnabled(false);
                pnlProgress.SetVisible(true);
            }
        }
        
        function OnUploadProgressChanged(s, e){
            pbUpload.SetPosition(e.progress);
        }
        
        function OnFileUploadComplete(s, e){
            uploadedFilesFlag = e.isValid;
            lblArquivo.SetText(hf.Get("nomeArquivoOriginal"));
        }
        
        function OnFilesUploadComplete(s, e){
            if(uploadedFilesFlag){
                btnCancel.SetVisible(false);
                btnUpload.SetEnabled(true);
                pbUpload.SetPosition(100);
                lblCompleteMessage.SetVisible(true);
            }
            else{
                btnUpload.SetEnabled(true);
                pnlProgress.SetVisible(false);
            }
        }
        
        function OnBtnCancelClick(s, e){
            uploadControl.Cancel();
            btnUpload.SetEnabled(true);
            pnlProgress.SetVisible(false);
        }
        
        function OnUploadControlTextChanged(s, e){
            btnUpload.SetEnabled(s.GetText() != "");
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server" target="anexo">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Arquivo:"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxUploadControl ID="uc" runat="server" ClientInstanceName="uploadControl" Width="99%" OnFileUploadComplete="uc_FileUploadComplete">
                        <ClientSideEvents FilesUploadComplete="OnFilesUploadComplete" FileUploadComplete="OnFileUploadComplete"
                            TextChanged="OnUploadControlTextChanged" UploadingProgressChanged="OnUploadProgressChanged" />
                    </dx:ASPxUploadControl>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 120px" valign="top">
                    <dx:ASPxButton ID="btnUpload" runat="server" AutoPostBack="False" ClientInstanceName="btnUpload"
                        Text="Transferir arquivo" ToolTip="Enviar o arquivo para uma área temporária no servidor WEB">
                        <ClientSideEvents Click="OnBtnUploadClick" />
                    </dx:ASPxButton>
                            </td>
                            <td>
                    <dx:ASPxPanel ID="pnlProgress" runat="server" ClientInstanceName="pnlProgress" ClientVisible="False"
                        Width="100%">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" class="progressTable" style="width: 100%">
                                    <tr>
                                        <td class="progressBarCell" style="width: 252px">
                                            <dx:ASPxProgressBar ID="pbUpload" runat="server" ClientInstanceName="pbUpload" Height="23px"
                                                Width="250px">
                                            </dx:ASPxProgressBar>
                                        </td>
                                        <td align="center" style="width: 80px">
                                            <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" ClientInstanceName="btnCancel"
                                                Text="Cancelar" ToolTip="Interromper a transfer&#234;ncia">
                                                <ClientSideEvents Click="OnBtnCancelClick" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td valign="top">
                                            <dx:ASPxLabel ID="lblCompleteMessage" runat="server" ClientInstanceName="lblCompleteMessage"
                                                ClientVisible="False" Text="Transfer&#234;ncia completa">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="progressBarCell" colspan="3" style="height: 16px">
                                            <dx:ASPxLabel ID="lblArquivo" runat="server" ClientInstanceName="lblArquivo" Text="Nome do arquivo">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Descrição:"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="71px" Width="99%">
                    </dx:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 130px">
                        <tr>
                            <td align="center">
                                <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" ToolTip="Salva o arquivo da área temporária no banco de dados" AutoPostBack="False" ClientInstanceName="btnSalvar">
                                </dx:ASPxButton>
                            </td>
                            <td style="width: 5px">
                            </td>
                            <td align="right">
                                <dx:ASPxButton ID="btnFechar" runat="server" Text="Fechar" ToolTip="Fecha a tela e ignora todas as informações" ClientInstanceName="btnFechar">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
        <dx:aspxhiddenfield id="hf" runat="server" clientinstancename="hf"></dx:aspxhiddenfield>
    </form>
</body>
</html>
