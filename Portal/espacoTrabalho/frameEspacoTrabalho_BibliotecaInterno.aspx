<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_BibliotecaInterno.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_BibliotecaInterno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%= System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-language" content="<%# System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        var clicouEmSelecionarTodos = false;
        function verificaBotoes(s, e) {

            var btnComent = s.GetContextMenuItemByCommandName('btnComentarios');
            var btnVers = s.GetContextMenuItemByCommandName('btnVersoes');
            var btnPerm = s.GetContextMenuItemByCommandName('btnPermissao');
            var btnLink2 = s.GetToolbarItemByCommandName('btnLink2');

            var mostraComentarios = ((e.activeAreaName == "Folders" && s.currentFolderId != '') || s.GetSelectedItems().length > 0) && e.activeAreaName != "None";
            var mostraVersao = e.activeAreaName != "Folders" && s.GetSelectedItems().length > 0 && e.activeAreaName != "None" && !s.GetSelectedItems()[0].isFolder;
            var mostraPerrmissao = ((e.activeAreaName == "Folders" && s.currentFolderId != 'o:-1') || (s.GetSelectedItems().length > 0 && s.GetSelectedItems()[0].isFolder)) && e.activeAreaName != "None";
            var mostraLink = e.activeAreaName != "Folders" && s.GetSelectedItems().length > 0 && e.activeAreaName != "None" && !s.GetSelectedItems()[0].isFolder;

            mostraComentarios = mostraComentarios && s.allowRename;
            if (btnComent != null)
                btnComent.SetVisible(mostraComentarios);
            if (btnVers != null)
                btnVers.SetVisible(mostraVersao);
            if (btnPerm != null)
                btnPerm.SetVisible(mostraPerrmissao);
            if (btnLink2 != null)
                btnLink2.SetVisible(mostraLink);
            //if (btnLink != null) {
            //    btnLink.SetEnabled(mostraLink);
            //    if(mostraLink)
            //        btnLink.SetImageUrl('../imagens/anexo/link.png');
            //    else
            //        btnLink.SetImageUrl('../imagens/anexo/linkDes.png');
            //}

            var botaoUpload = document.querySelector('#pnCallback_fmArquivos_Splitter_UploadButton');
            if (botaoUpload !== null) {
                botaoUpload.innerHTML = 'Enviar';
            }

            //console.log(botaoUpload);
        }


        function abrePopUp(s) {
            var codigoPastaSuperior = "";
            var codigoPastaAtual = "";
            //alert(s.cp_CN);
            if (s.cp_CN == 'btnComentarios')
                window.top.showModal3(window.top.pcModal.cp_Path + "espacoTrabalho/AnexosProjeto_PopUp.aspx?OL=S&TA=" + s.cp_TA + "&ID=" + s.cp_COA + "&IDOP=" + s.cp_COP + "&O=" + s.cp_O + "&CPS=&CPA=&MO=Editar&CA=" + s.cp_CA, traducao.frameEspacoTrabalho_BibliotecaInterno_anexos, null, null, '', null);
            else if (s.cp_CN == 'btnVersoes')
                window.top.showModal3(window.top.pcModal.cp_Path + 'espacoTrabalho/frameEspacoTrabalho_BibliotecaListaVersoes.aspx?CA=' + s.cp_CA, traducao.frameEspacoTrabalho_BibliotecaInterno_vers_es, null, null, '', null);
            else if (s.cp_CN == 'btnLink')
                window.top.showModal3(window.top.pcModal.cp_Path + "espacoTrabalho/AnexosCompartilhamento.aspx?TA=" + s.cp_TA + "&ID=" + s.cp_COA + "&O=Arquivo&CPS=&CPA=" + s.cp_CA, traducao.frameEspacoTrabalho_BibliotecaInterno_anexos, screen.width - 60, 490, executaPosPopUp, null);
            else if (s.cp_CN == 'btnPermissao')
                window.top.showModal3(window.top.pcModal.cp_Path + "espacoTrabalho/PermissoesPastaAnexo.aspx?TA=" + s.cp_TA + "&ID=" + s.cp_COA + "&IDOP=" + s.cp_COP + "&O=N&CPS=&CPA=&MO=Editar&CA=" + s.cp_CA + "&Nome=" + s.cp_Nome, traducao.frameEspacoTrabalho_BibliotecaInterno_permiss_es_da_pasta, 380, 405, executaPosPopUp, null);
            else if (s.cp_CN == 'btnLink2')
                window.top.showModal3(window.top.pcModal.cp_Path + "espacoTrabalho/Anexo_Link.aspx?CA=" + s.cp_CA, traducao.frameEspacoTrabalho_BibliotecaInterno_compartilhar_anexo, 800, 490, executaPosPopUp, null);
        }

        function executaPosPopUp(lParam) {
            fmArquivos.PerformCallback();
        }

        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 45;
            fmArquivos.SetHeight(height);
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }

    </script>

    <style type="text/css">
        .escondetxt {
            display: none;
        }
    </style>

</head>
<body class="body" style="margin: 0; padding: <%= paddingTela %>;">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable" style="margin: 0; padding: 10px 0px 0px 0px" width="100%">
            <tr>
                <td align="right" style="padding-bottom: 3px">

                    <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
                    </dxcp:ASPxLoadingPanel>

                    <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server"
                        ClientInstanceName="txtPesquisa"
                        NullText="Pesquisar por palavra chave..." Width="350px" Height="25px" Theme="MaterialCompact" ClientVisible="False">
                        <ClientSideEvents ButtonClick="function(s, e) {
                pnCallback.PerformCallback();
	
}" />
                        <Buttons>
                            <dxe:EditButton>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Search"
                                        HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                </Image>
                            </dxe:EditButton>
                        </Buttons>
                        <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                    </dxe:ASPxButtonEdit>
                </td>
            </tr>
            <tr>
                <td>

                    <dx:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
                        <SettingsLoadingPanel Enabled="False" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div id="divGrid" style="visibility:visible">
                                    <dxtv:ASPxFileManager ID="fmArquivos" runat="server" ClientInstanceName="fmArquivos" DataSourceID="SqlDataSource1" meta:resourceKey="fmArquivosResource1" OnCustomFileInfoDisplayText="fmArquivos_CustomFileInfoDisplayText" OnCustomThumbnail="fmArquivos_CustomThumbnail" OnFileDownloading="fmArquivos_FileDownloading" OnItemDeleting="fmArquivos_ItemDeleting" Width="100%" OnCustomCallback="fmArquivos_CustomCallback" Theme="MaterialCompact" OnCustomJSProperties="fmArquivos_CustomJSProperties" OnDetailsViewCustomColumnDisplayText="fmArquivos_DetailsViewCustomColumnDisplayText">
                                        <Styles>
                                            <FolderContainer Width="40%">
                                            </FolderContainer>
                                        </Styles>
                                        <ClientSideEvents CustomCommand="function(s, e) 
{
            if (e.commandName == &quot;selectAll&quot;) 
           {
                    if(clicouEmSelecionarTodos == false)
                    {
                          clicouEmSelecionarTodos = true;                        
                    }
                    else
                    {
                          clicouEmSelecionarTodos = false;
                    }

                     var items = s.GetItems();
                     for (var i = 0; i &lt; items.length; i++)
                          items[i].SetSelected(clicouEmSelecionarTodos);
                      
            }
            else
            {
                     lpLoading.Show();
                                            callbackArquivo.PerformCallback(e.commandName + '|' + s.GetActiveAreaName());
            }
}"
                                            ToolbarUpdating="function(s, e) {
verificaBotoes(s, e);	
}"
                                            FilesUploading="function(s, e) {
   lpLoading.Show(); 
   var&nbsp;i;
    var strFiles = '';
    for&nbsp;(i =&nbsp;0; i &lt; e.fileNames.length; i++) {
&nbsp;&nbsp;      strFiles += e.fileNames[i] +&nbsp;&quot;|&quot;;
    }
           hfCodigoAnexo.Set('NomeArquivoUPL', strFiles);
}"
                                            SelectedFileOpened="function(s, e) {
e.file.Download();
e.processOnServer = false;
}"
                                            EndCallback="function(s, e) {
s.AdjustControl();
if(fmArquivos.cpErro !== '')
{
window.top.mostraMensagem(s.cpErro, 'Atencao', true, false, null);
fmArquivos.cpErro = &quot;&quot;;
}
}"  Init="function(s, e) {
    AdjustSize();
    s.AdjustControl();
    document.getElementById('divGrid').style.visibility = '';
}" FileUploaded="function(s, e) {
lpLoading.Hide();	
      hfCodigoAnexo.Set('NomeArquivoUPL','');
}" CallbackError="function(s, e) {
	e.handled = true;
}" ErrorAlertDisplaying="function(s, e) {
e.showAlert = false;
lpLoading.Hide();
window.top.mostraMensagem(e.errorText, 'erro', true, false, null, null, null, null);
}" ErrorOccurred="function(s, e) {
}" />
                                        <Settings InitialFolder="Raiz" RootFolder="Raiz"/>
                                        <SettingsFileList View="Details">
                                            <DetailsViewSettings>
                                                <Columns>
                                                    <dxtv:FileManagerDetailsColumn Caption=" " FileInfoType="Thumbnail" VisibleIndex="0">
                                                    </dxtv:FileManagerDetailsColumn>
                                                    <dxtv:FileManagerDetailsColumn Caption="Nome" VisibleIndex="1">
                                                    </dxtv:FileManagerDetailsColumn>
                                                    <dxtv:FileManagerDetailsColumn Caption="Data de Inclusão" FileInfoType="LastWriteTime" VisibleIndex="2">
                                                    </dxtv:FileManagerDetailsColumn>
                                                    <dxtv:FileManagerDetailsColumn Caption="Size" FileInfoType="Size" Visible="False">
                                                    </dxtv:FileManagerDetailsColumn>
                                                    <dxtv:FileManagerDetailsCustomColumn Caption="Usuário" Visible="true" VisibleIndex="3" Name="colNomeUsuarioInclusao" MinWidth="180">
                                                    </dxtv:FileManagerDetailsCustomColumn>
                                                </Columns>
                                            </DetailsViewSettings>
                                        </SettingsFileList>
                                        <SettingsAdaptivity Enabled="true"/>
                                        <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" AllowMove="True" AllowRename="True" />
                                        <SettingsFiltering FilterBoxMode="Subfolders" FilteredFileListView="Standard" />
                                        <SettingsToolbar>
                                            <Items>
                                                <dxtv:FileManagerToolbarRefreshButton ToolTip="Atualizar">
                                                </dxtv:FileManagerToolbarRefreshButton>
                                                <dxtv:FileManagerToolbarDownloadButton ToolTip="Baixar">
                                                </dxtv:FileManagerToolbarDownloadButton>
                                                <dxtv:FileManagerToolbarCustomButton BeginGroup="True" CommandName="btnLink">
                                                    <Image ToolTip="Incluir Link para um Arquivo" Url="~/imagens/anexo/link.png" />
                                                </dxtv:FileManagerToolbarCustomButton>
                                                <dxtv:FileManagerToolbarCustomButton CommandName="selectAll" ToolTip="Marcar/Desmarcar Todos">
                                                     <Image Url="../imagens/selectAll.png" />
                                                 </dxtv:FileManagerToolbarCustomButton>
                                            </Items>
                                        </SettingsToolbar>
                                        <SettingsContextMenu>
                                            <Items>
                                                <dxtv:FileManagerToolbarCreateButton ToolTip="Criar (F7)">
                                                </dxtv:FileManagerToolbarCreateButton>
                                                <dxtv:FileManagerToolbarRenameButton ToolTip="Renomear (F2)">
                                                </dxtv:FileManagerToolbarRenameButton>
                                                <dxtv:FileManagerToolbarMoveButton ToolTip="Mover (F6)">
                                                </dxtv:FileManagerToolbarMoveButton>
                                                <dxtv:FileManagerToolbarDeleteButton ToolTip="Excluir (Del)">
                                                </dxtv:FileManagerToolbarDeleteButton>
                                                <dxtv:FileManagerToolbarCustomButton CommandName="btnComentarios" Text="Editar Detalhes" ToolTip="Editar Detalhes">
                                                    <Image ToolTip="Editar Comentários" Url="~/imagens/anexo/ComentariosAnexo.png" />
                                                </dxtv:FileManagerToolbarCustomButton>
                                                <dxtv:FileManagerToolbarRefreshButton ToolTip="Atualizar">
                                                </dxtv:FileManagerToolbarRefreshButton>
                                                <dxtv:FileManagerToolbarDownloadButton ToolTip="Baixar">
                                                </dxtv:FileManagerToolbarDownloadButton>
                                                <dxtv:FileManagerToolbarCustomButton CommandName="btnVersoes" Text="Visualizar Versões" ToolTip="Visualizar Versões">
                                                    <Image ToolTip="Visualizar Versões" Url="~/imagens/anexo/versaoAnexo.png" />
                                                </dxtv:FileManagerToolbarCustomButton>
                                                <dxtv:FileManagerToolbarCustomButton BeginGroup="True" CommandName="btnPermissao" Text="Definir Permissões" ToolTip="Definir Permissões">
                                                    <Image ToolTip="Visualizar Versões" Url="~/imagens/anexo/permissao.png" />
                                                </dxtv:FileManagerToolbarCustomButton>
                                                <dxtv:FileManagerToolbarCustomButton BeginGroup="True" CommandName="btnLink2" Text="Compartilhar" ToolTip="Compartilhar">
                                                    <Image ToolTip="Compartilhar" Url="~/imagens/anexo/link.png" />
                                                </dxtv:FileManagerToolbarCustomButton>
                                            </Items>
                                        </SettingsContextMenu>
                                        <SettingsUpload NullText="">
                                            <AdvancedModeSettings DropZoneText=" " EnableMultiSelect="True" TemporaryFolder="~\ArquivosTemporarios\">
                                            </AdvancedModeSettings>
                                        </SettingsUpload>
                                        <Images>
                                            <UploadButton AlternateText="">
                                            </UploadButton>
                                        </Images>
                                        <SettingsDataSource FileBinaryContentFieldName="ConteudoAnexo" IsFolderFieldName="IndicaPasta" KeyFieldName="CodigoAnexo" LastWriteTimeFieldName="DataInclusao" NameFieldName="Nome" ParentKeyFieldName="CodigoPastaSuperior" />
                                    </dxtv:ASPxFileManager>
                                </div>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                    <script language="javascript" type="text/javascript">
                        /*
                        if (window.innerHeight > 150) {
                            if (fmArquivos.cp_MostraBotaoFechar == 'S')
                                fmArquivos.SetHeight(window.innerHeight - 100);
                            else
                                fmArquivos.SetHeight(window.innerHeight - 95);
                        }
                        else {
                            fmArquivos.SetHeight(fmArquivos.cp_Altura);
                        }
                        */
                        if (fmArquivos.cp_ALT != null) {
                            fmArquivos.SetHeight(fmArquivos.cp_Altura);
                        }
                        else {
                            if (fmArquivos.cp_Popup != null) {
                                if (fmArquivos.cp_PopupOffset != null) {
                                    fmArquivos.SetHeight(window.innerHeight - parseInt(fmArquivos.cp_PopupOffset, 10))
                                }
                                else {
                                    fmArquivos.SetHeight(window.innerHeight - 80);
                                }
                            }
                            else {
                                fmArquivos.SetHeight(window.innerHeight - 40);
                            }
                        }
                    </script>
                </td>
            </tr>
            <tr>
                <td>
                    <span id="spnLegenda" runat="server"></span></td>
            </tr>
            <tr runat="server" id="td_btnFechar">
                <td>
                    <table cellspacing="0" cellpadding="0" style="padding-top: 10px;" width="100%">
                        <tr>
                            <td align="right">
                                <table cellspacing="0" class="dxflInternalEditorTable" width="100%">
                                    <tr>
                                        <td align="left">
                                            <dx:ASPxLabel ID="lblExtensoesPermitidas" runat="server" Font-Italic="True">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <table class="formulario-botoes" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btnFechar" runat="server"
                                                            Text="Fechar" Width="90px" ClientVisible="False"
                                                            meta:resourcekey="btnFecharResource1" Theme="MaterialCompact">
                                                            <ClientSideEvents Click="function(s, e) {
window.top.fechaModal();
}" />
                                                            <Paddings Padding="0px" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            SelectCommand="BEGIN

	                DECLARE @tb AS Table(CodigoAnexo Int
											                ,Descricao Varchar(4000)
											                ,DataInclusao DateTime
											                ,CodigoUsuarioInclusao Int
											                ,Nome VarChar(255)
											                ,CodigoEntidade Int
											                ,CodigoPastaSuperior Int
											                ,IndicaPasta Bit
											                ,IndicaControladoSistema Char(1)
											                ,NomeUsuario VarChar(255)
											                ,NomePastaSuperior VarChar(255)
											                ,DataCheckOut DateTime
											                ,CodigoUsuarioCheckOut Int
											                ,DataCheckIn DateTime
											                ,NomeUsuarioCheckout VarChar(255)
											                ,ConteudoAnexo VARBINARY(MAX))

	                    INSERT INTO @tb
 SELECT CodigoAnexo,
        DescricaoAnexo,
        DataInclusao,
        CodigoUsuarioInclusao,
        Nome,
        CodigoEntidade,
        CodigoPastaSuperior,
        IndicaPasta,
        IndicaControladoSistema,
        NomeUsuario,
        NomePastaSuperior,
        DataCheckOut,
        CodigoUsuarioCheckOut,
        DataCheckIn,
        NomeUsuarioCheckout,
        null 
  FROM [dbo].[f_brk_getAnexos] (
   @CodigoEntidadeContexto
  ,@CodigoUsuarioSistema
  ,@CodigoObjetoAssociado
  ,@IniciaisTipoObjetoAssociado
  ,@PalavraChave) f
  UNION
  SELECT -1, @NomePastaRaiz, null, -1, @NomePastaRaiz, -1, null, 1, 'S', null, null, null, null, null, null, null

                    SELECT * FROM @tb             
    
                   
                     END"
            DeleteCommand="DELETE FROM Anexo WHERE CodigoAnexo = -1"
            InsertCommand="INSERT INTO Anexo (DescricaoAnexo, DataInclusao, CodigoUsuarioInclusao, Nome, CodigoEntidade, CodigoPastaSuperior, IndicaPasta) 
                                  VALUES ('', GetDate(), @CodigoUsuarioInclusao, @Nome, @CodigoEntidade, @CodigoPastaSuperior, @IndicaPasta)"
            UpdateCommand="UPDATE Anexo SET Nome = @Nome,  CodigoPastaSuperior = @CodigoPastaSuperior WHERE CodigoAnexo = @CodigoAnexo"
            OnUpdating="SqlDataSource1_Updating"
            OnInserting="SqlDataSource1_Inserting"
            OnDeleting="SqlDataSource1_Deleting" OnSelecting="SqlDataSource1_Selecting">
            <DeleteParameters>
                <asp:Parameter Name="CodigoAnexo" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="CodigoPastaSuperior" Type="Int32" />
                <asp:Parameter Name="CodigoAnexo" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="CodigoUsuarioInclusao" Type="Int32" />
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="CodigoEntidade" Type="Int32" />
                <asp:Parameter Name="CodigoPastaSuperior" Type="Int32" />
                <asp:Parameter Name="IndicaPasta" DbType="Boolean" />
                <asp:Parameter Name="ConteudoAnexo" DbType="Binary" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter Name="CodigoObjetoAssociado" Type="Int64" />
                <asp:Parameter Name="CodigoTipoAssociacao" Type="Int32" />
                <asp:Parameter Name="CodigoEntidadeContexto" Type="Int32" />
                <asp:Parameter Name="CodigoUsuarioSistema" Type="Int32" />
                <asp:Parameter Name="IniciaisTipoObjetoAssociado" Type="String" />
                <asp:Parameter Name="PalavraChave" Type="String" />
                <asp:Parameter Name="NomePastaRaiz" />
            </SelectParameters>
        </asp:SqlDataSource>

        <dxhf:ASPxHiddenField ID="hfCodigoAnexo" runat="server"
            ClientInstanceName="hfCodigoAnexo">
        </dxhf:ASPxHiddenField>

        <dx:ASPxCallback ID="callbackArquivo" runat="server"
            ClientInstanceName="callbackArquivo" OnCallback="callbackArquivo_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
                lpLoading.Hide();
abrePopUp(s);
}" />
        </dx:ASPxCallback>

        <dxpc:ASPxPopupControl ID="pcModalInterno" runat="server" ClientInstanceName="pcModalInterno"
            HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents PopUp="function(s, e) {
pcModalInterno.GetContentIFrame().dialogArguments = myObject;
}"
                Closing="function(s, e) {
        var retorno = '';
            
        if(retornoModal != null)
        {   
            retorno = retornoModal;
        }
            
        if(posExecutar != null)
            posExecutar(retorno);
            
		if(cancelaFechamentoPopUp == 'S')
			e.cancel = true;
    	else
            resetaModal();
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
