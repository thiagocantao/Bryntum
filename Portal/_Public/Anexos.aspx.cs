using System;
using System.Data;
using System.Web;
using DevExpress.Web;

public partial class teste : System.Web.UI.Page
{
    dados cDados;
    string ResolucaoCliente;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;
    int IDObjetoAssociado, IDObjetoPai;
    string IniciaisTipoAssociacao = "";

    public string estiloFooter = "dxsplPane dxfm-toolbar";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        cDados = CdadosUtil.GetCdados(null);
        string connectionString = cDados.classeDados.getStringConexao();

        SqlDataSource1.ConnectionString = connectionString;
        cDados.aplicaEstiloVisual(this, "MaterialCompact");
    }

    protected void Page_Load(object sender, EventArgs e)
    {       
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));

        if (txtPesquisa.Text != "")
        {
            SqlDataSource1.SelectCommand =
                string.Format(@"
                                   BEGIN
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
                      SELECT -1, '{1}', null, -1, '{1}', -1, null, 1, 'S', null, null, null, null, null, null, null
                      
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
                               -1 , CASE WHEN a.IndicaPasta = 'S' THEN 1 ELSE 0 END AS IndicaPasta, 
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
                           AND (a.Nome LIKE '%{0}%' OR a.DescricaoAnexo LIKE '%{0}%' OR a.PalavraChave LIKE '%{0}%')   

                               UPDATE @tb
                                  SET CodigoPastaSuperior = -1
                                WHERE CodigoPastaSuperior NOT IN (SELECT CodigoAnexo FROM @tb)
                                

                      
                        SELECT * FROM @tb WHERE IndicaPasta = 0 OR CodigoAnexo = -1
    
                   
                     END", txtPesquisa.Text.Replace("'", "''"), Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_resultado_da_pesquisa);
            fmArquivos.Settings.RootFolder = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_resultado_da_pesquisa;
           
           // fmArquivos.DataBind();
        }else
        {
            fmArquivos.Settings.RootFolder = "Raiz";// Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_raiz;
        }
    }

    protected void fmArquivos_FileDownloading(object source, DevExpress.Web.FileManagerFileDownloadingEventArgs e)
    {        
        string comandoSQL = string.Format(@"SELECT CodigoAnexo FROM {0}.{1}.Anexo WHERE CONVERT(varchar(30), DataInclusao, 120) = CONVERT(varchar(30), CONVERT(DateTime, '{2:dd/MM/yyyy HH:mm:ss}', 103), 120) AND Nome = '{3}'", cDados.getDbName()
            , cDados.getDbOwner()
            , e.File.LastWriteTime
            , e.File.Name);
        
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string CodigoAnexo = ds.Tables[0].Rows[0]["CodigoAnexo"].ToString();
            cDados.download(int.Parse(CodigoAnexo), null, Page, Response, Request, true);
        }
    }

    protected void fmArquivos_CustomFileInfoDisplayText(object source, FileManagerCustomFileInfoDisplayTextEventArgs e)
    {
        if (e.FileInfoType == FileInfoType.Size)
        {
            string comandoSQL = string.Format(@"SELECT  ca.codigoSequencialAnexo,  ca.Anexo
					   FROM Anexo a 
					   INNER JOIN AnexoVersao av on (av.codigoAnexo = a.CodigoAnexo)
					   LEFT JOIN ConteudoAnexo ca on (ca.codigoSequencialAnexo = av.codigoSequencialAnexo)					   
					   WHERE a.CodigoAnexo = {0} ", e.File.Id);
            DataSet dstemp = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(dstemp) && cDados.DataTableOk(dstemp.Tables[0]))
            {
                ASPxBinaryImage image1 = new ASPxBinaryImage();
                if (dstemp.Tables[0].Rows[0]["Anexo"].ToString() == "")
                {
                    e.DisplayText = "0B - não encontrado";
                    return;
                }
                image1.ContentBytes = (byte[])dstemp.Tables[0].Rows[0]["Anexo"];
                long tamanho = image1.ContentBytes.Length;
                e.DisplayText = tamanho.ToString();

                if (tamanho <= 1023)
                {
                    e.DisplayText = tamanho.ToString() + "B";
                }
                if (tamanho > 1024 && tamanho <= (1024 * 1024))
                {
                    e.DisplayText = (tamanho / 1024).ToString() + "KB";
                }
                if (tamanho > (1024 * 1024) && tamanho <= (1024 * 1024 * 1024))
                {
                    e.DisplayText = (tamanho / (1024 * 1024)).ToString() + "MB";
                }
            }
        }

    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
       
    }
}