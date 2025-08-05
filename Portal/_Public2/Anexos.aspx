<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anexos.aspx.cs" Inherits="teste" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

<script language="javascript" type="text/javascript">


</script>

    <style type="text/css">
        .escondetxt
        {
            display:none;
        }
        .style1
        {
            width: 100%;
        }

        .barraAnexo {
            background-color: #f2f2f2 !important;
            border-top: 1px solid #cccccc !important;
            border-bottom: none !important;
            color: #555555 !important;
            font-size: 14px !important;
            height: 45px !important;
        }

        html .dxeHyperlink {
        
            color: #0d45b7;
            text-decoration: none;
        }

        html a.dxeHyperlink:visited {
            color: #555555;
        }
}
        </style>

</head>
<body class="body" style="margin:0px">
    <form id="form1" runat="server">
<div>
        

        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 60px">
            <tr>
                <td style="height: 90px; border-top: 2px solid #95c477;">
                    <img src="../espacoCliente/LogoPortal.png" / style="height:40px; margin:5px 5px 5px 45px" ></td>
                <td align="right">
                    <dxe:ASPxImage ID="imgLogoLD" runat="server" ClientVisible="false"
                        ImageUrl="~/espacoCliente/LogoLD.png">
                    </dxe:ASPxImage>
                </td>
            </tr>
            </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="barraAnexo">
                    <table cellspacing="0" class="style1">
                        <tr>
                            <td style="padding-left: 30px;">
                                &nbsp;
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server"  NavigateUrl="~/login.aspx" Text="Voltar" Theme="MaterialCompact" />
                            </td>
                            <td align="right">

                            <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server" 
                                ClientInstanceName="txtPesquisa"  
                                NullText="Pesquisar por palavra chave..." Width="350px" Height="25px" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" Theme="MaterialCompact">
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
                    </table>
                </td>
            </tr>
        </table>
        

        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable" 
            width="100%">
            <tr>
                <td style="padding: 5px">

                    <dx:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	 fmArquivos.SetHeight(window.innerHeight - 145);
}" />
                        <PanelCollection>
<dx:PanelContent runat="server">
    <dxtv:ASPxFileManager ID="fmArquivos" runat="server" ClientInstanceName="fmArquivos" DataSourceID="SqlDataSource1"  meta:resourceKey="fmArquivosResource1" 
        OnCustomFileInfoDisplayText="fmArquivos_CustomFileInfoDisplayText" OnFileDownloading="fmArquivos_FileDownloading" Width="100%" Theme="MaterialCompact">
        <Styles>
            <FolderContainer Width="300px">
            </FolderContainer>
        </Styles>
        <Settings InitialFolder="Raiz" />
        <SettingsFileList View="Details">
            <DetailsViewSettings>
                <Columns>
                    <dxtv:FileManagerDetailsColumn Caption=" " FileInfoType="Thumbnail" VisibleIndex="0">
                    </dxtv:FileManagerDetailsColumn>
                    <dxtv:FileManagerDetailsColumn Caption="Nome" VisibleIndex="1">
                    </dxtv:FileManagerDetailsColumn>
                    <dxtv:FileManagerDetailsColumn Caption="Data de Inclusão" FileInfoType="LastWriteTime" VisibleIndex="2">
                    </dxtv:FileManagerDetailsColumn>
                </Columns>
            </DetailsViewSettings>
        </SettingsFileList>
        <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" AllowMove="True" AllowRename="True" />
        <SettingsToolbar ShowFilterBox="False">
        </SettingsToolbar>
        <SettingsUpload Enabled="False">
            <AdvancedModeSettings DropZoneText=" " EnableMultiSelect="True">
            </AdvancedModeSettings>
        </SettingsUpload>
        <SettingsDataSource FileBinaryContentFieldName="ConteudoAnexo" IsFolderFieldName="IndicaPasta" KeyFieldName="CodigoAnexo" LastWriteTimeFieldName="DataInclusao" NameFieldName="Nome" ParentKeyFieldName="CodigoPastaSuperior" />
    </dxtv:ASPxFileManager>
                            </dx:PanelContent>
</PanelCollection>
                    </dx:ASPxCallbackPanel>
        <script language="javascript" type="text/javascript">
            fmArquivos.SetHeight(window.innerHeight - 155);
        </script>
                </td>
            </tr>
            </table>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        SelectCommand="                   BEGIN
                     DECLARE @CodigoEntidadeMaster Int
                     
                     SELECT TOP 1 @CodigoEntidadeMaster = un.CodigoEntidade
                       FROM UnidadeNegocio AS un
                      WHERE un.CodigoEntidade = un.CodigoUnidadeNegocio
                        AND un.DataExclusao IS NULL
                        AND un.IndicaUnidadeNegocioAtiva = 'S'
                        AND un.CodigoUnidadeNegocioSuperior IS NULL
                     
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
                                   (CodigoAnexo 
                         ,Descricao 
                         ,DataInclusao 
                         ,CodigoUsuarioInclusao 
                         ,Nome 
                         ,CodigoEntidade 
                         ,CodigoPastaSuperior 
                         ,IndicaPasta 
                         ,IndicaControladoSistema 
                         ,NomeUsuario 
                         ,NomePastaSuperior 
                         ,DataCheckOut 
                         ,CodigoUsuarioCheckOut 
                         ,DataCheckIn 
                         ,NomeUsuarioCheckout 
                         ,ConteudoAnexo)
                      SELECT -1, 'Raiz', null, -1, 'Raiz', -1, null, 1, 'S', null, null, null, null, null, null, null
                      
                      INSERT INTO @tb
                        (CodigoAnexo 
                         ,Descricao 
                         ,DataInclusao 
                         ,CodigoUsuarioInclusao 
                         ,Nome 
                         ,CodigoEntidade 
                         ,CodigoPastaSuperior 
                         ,IndicaPasta 
                         ,IndicaControladoSistema 
                         ,NomeUsuario 
                         ,NomePastaSuperior 
                         ,DataCheckOut 
                         ,CodigoUsuarioCheckOut 
                         ,DataCheckIn 
                         ,NomeUsuarioCheckout 
                         ,ConteudoAnexo)
                        SELECT a.CodigoAnexo, a.DescricaoAnexo, convert(varchar(20), a.DataInclusao, 113) as DataInclusao, 
                               a.CodigoUsuarioInclusao, a.Nome, a.CodigoEntidade, 
                               CASE WHEN aa.IndicaLinkCompartilhado = 'S' THEN ISNULL(aa.CodigoPastaLink, -1) ELSE ISNULL(a.CodigoPastaSuperior, -1) END , CASE WHEN a.IndicaPasta = 'S' THEN 1 ELSE 0 END AS IndicaPasta, 
                               a.IndicaControladoSistema, U.NomeUsuario, PSUP.Nome AS NomePastaSuperior,
                               a.dataCheckOut, a.codigoUsuarioCheckOut, a.dataCheckIn, UCK.NomeUsuario as nomeUsuarioCheckout, null
                          FROM Anexo AS a INNER JOIN
                               AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo ) INNER JOIN
                               Usuario AS U ON a.CodigoUsuarioInclusao = U.CodigoUsuario LEFT OUTER JOIN
                               Anexo AS PSUP ON a.CodigoPastaSuperior = PSUP.CodigoAnexo LEFT OUTER JOIN
                               Usuario AS UCK ON a.codigoUsuarioCheckOut = UCK.CodigoUsuario LEFT JOIN
                               ConteudoAnexo ca ON ca.codigoSequencialAnexo = a.CodigoAnexo
                         WHERE a.dataExclusao is null
                           AND a.IndicaPasta = 'S'
                           AND a.IndicaAnexoPublicoExterno = 'S'
                           AND a.CodigoEntidade = @CodigoEntidadeMaster
                           AND aa.CodigoObjetoAssociado = @CodigoEntidadeMaster
                           AND aa.CodigoTipoAssociacao = dbo.f_GetCodigoTipoAssociacao('EN')
                      
                      INSERT INTO @tb
                         (CodigoAnexo 
                         ,Descricao 
                         ,DataInclusao 
                         ,CodigoUsuarioInclusao 
                         ,Nome 
                         ,CodigoEntidade 
                         ,CodigoPastaSuperior 
                         ,IndicaPasta 
                         ,IndicaControladoSistema 
                         ,NomeUsuario 
                         ,NomePastaSuperior 
                         ,DataCheckOut 
                         ,CodigoUsuarioCheckOut 
                         ,DataCheckIn 
                         ,NomeUsuarioCheckout 
                         ,ConteudoAnexo)
                        SELECT a.CodigoAnexo, a.DescricaoAnexo, convert(varchar(20), a.DataInclusao, 113) as DataInclusao, 
                               a.CodigoUsuarioInclusao, a.Nome, a.CodigoEntidade, 
                               CASE WHEN aa.IndicaLinkCompartilhado = 'S' THEN ISNULL(aa.CodigoPastaLink, -1) ELSE ISNULL(a.CodigoPastaSuperior, -1) END , CASE WHEN a.IndicaPasta = 'S' THEN 1 ELSE 0 END AS IndicaPasta, 
                               a.IndicaControladoSistema, U.NomeUsuario, PSUP.Nome AS NomePastaSuperior,
                               a.dataCheckOut, a.codigoUsuarioCheckOut, a.dataCheckIn, UCK.NomeUsuario as nomeUsuarioCheckout, null
                          FROM Anexo AS a INNER JOIN
                               AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo ) INNER JOIN
                               Usuario AS U ON a.CodigoUsuarioInclusao = U.CodigoUsuario LEFT OUTER JOIN
                               Anexo AS PSUP ON a.CodigoPastaSuperior = PSUP.CodigoAnexo LEFT OUTER JOIN
                               Usuario AS UCK ON a.codigoUsuarioCheckOut = UCK.CodigoUsuario LEFT JOIN
                               ConteudoAnexo ca ON ca.codigoSequencialAnexo = a.CodigoAnexo
                         WHERE a.dataExclusao is null    
                           AND a.IndicaPasta = 'S'                       
                           AND a.CodigoEntidade = @CodigoEntidadeMaster
                           AND a.CodigoPastaSuperior IN (SELECT CodigoAnexo FROM @tb)
                           AND a.CodigoAnexo NOT IN (SELECT CodigoAnexo FROM @tb)
                           AND aa.CodigoObjetoAssociado = @CodigoEntidadeMaster
                           AND aa.CodigoTipoAssociacao = dbo.f_GetCodigoTipoAssociacao('EN')
                      
                     
                     INSERT INTO @tb
                         (CodigoAnexo 
                         ,Descricao 
                         ,DataInclusao 
                         ,CodigoUsuarioInclusao 
                         ,Nome 
                         ,CodigoEntidade 
                         ,CodigoPastaSuperior 
                         ,IndicaPasta 
                         ,IndicaControladoSistema 
                         ,NomeUsuario 
                         ,NomePastaSuperior 
                         ,DataCheckOut 
                         ,CodigoUsuarioCheckOut 
                         ,DataCheckIn 
                         ,NomeUsuarioCheckout 
                         ,ConteudoAnexo)
                        SELECT a.CodigoAnexo, a.DescricaoAnexo, convert(varchar(20), a.DataInclusao, 113) as DataInclusao, 
                               a.CodigoUsuarioInclusao, a.Nome, a.CodigoEntidade, 
                               CASE WHEN aa.IndicaLinkCompartilhado = 'S' THEN ISNULL(aa.CodigoPastaLink, -1) ELSE ISNULL(a.CodigoPastaSuperior, -1) END , CASE WHEN a.IndicaPasta = 'S' THEN 1 ELSE 0 END AS IndicaPasta, 
                               a.IndicaControladoSistema, U.NomeUsuario, PSUP.Nome AS NomePastaSuperior,
                               a.dataCheckOut, a.codigoUsuarioCheckOut, a.dataCheckIn, UCK.NomeUsuario as nomeUsuarioCheckout, null
                          FROM Anexo AS a INNER JOIN
                               AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo ) INNER JOIN
                               Usuario AS U ON a.CodigoUsuarioInclusao = U.CodigoUsuario LEFT OUTER JOIN
                               Anexo AS PSUP ON a.CodigoPastaSuperior = PSUP.CodigoAnexo LEFT OUTER JOIN
                               Usuario AS UCK ON a.codigoUsuarioCheckOut = UCK.CodigoUsuario LEFT JOIN
                               ConteudoAnexo ca ON ca.codigoSequencialAnexo = a.CodigoAnexo
                         WHERE a.dataExclusao is null    
                           AND a.IndicaPasta = 'N'    
                           AND a.CodigoEntidade = @CodigoEntidadeMaster                       
                           AND a.CodigoPastaSuperior IN (SELECT CodigoAnexo FROM @tb)     
                           AND a.CodigoAnexo NOT IN (SELECT CodigoAnexo FROM @tb) 
                           AND aa.CodigoObjetoAssociado = @CodigoEntidadeMaster
                           AND aa.CodigoTipoAssociacao = dbo.f_GetCodigoTipoAssociacao('EN')
                      

                               UPDATE @tb
                                  SET CodigoPastaSuperior = -1
                                WHERE CodigoPastaSuperior NOT IN (SELECT CodigoAnexo FROM @tb)
                                

                      
                        SELECT * FROM @tb     
                   
                 END                   
">
    </asp:SqlDataSource>

        </div>
</form>
</body>
</html>
