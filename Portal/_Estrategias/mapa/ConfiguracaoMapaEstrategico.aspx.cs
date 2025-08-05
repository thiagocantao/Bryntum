using System;
using System.Web;
using DevExpress.Web;
using System.Web.Hosting;
using Img = System.Drawing.Image;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Web.Script.Serialization;

public partial class _Estrategias_mapa_ConfiguracaoMapaEstrategico : System.Web.UI.Page
{
    #region Fields

    int codigoMapa;
    int codigoUsuario;
    int codigoEntidade;
    string caminhoFisicoArquivosTemp;

    dados cDados;

    #endregion

    #region Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
        codigoMapa = int.Parse(Request.QueryString["cm"]);
        codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));

        caminhoFisicoArquivosTemp = HttpUtility.JavaScriptStringEncode(
            string.Format("{0}ArquivosTemporarios", HostingEnvironment.ApplicationPhysicalPath));

    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string result = string.Empty;
        string[] parameters = (e.Parameter ?? string.Empty).Split(';');
        string acao = parameters[0].ToLower();
        string codigoFatorChave;
        switch (acao)
        {
            #region carregamapa
            case "carregamapa":
                string innerHtml = ObtemConteudoHtml();
                string nomeImagemMapa = SalvaImagemMapa(codigoMapa, caminhoFisicoArquivosTemp);
                if (!string.IsNullOrEmpty(nomeImagemMapa))
                {
                    Img img = Img.FromFile(string.Format("{0}\\\\{1}",
                        caminhoFisicoArquivosTemp, nomeImagemMapa));
                    result = new JavaScriptSerializer().Serialize(new
                    {
                        NomeImagemMapa = nomeImagemMapa,
                        AlturaImagem = img.Height,
                        LarguraImagem = img.Width,
                        InnerHtml = innerHtml
                    });
                }
                break;
            #endregion
            #region reposiciona e redimensiona
            case "reposiciona":
            case "redimensiona":
                if (parameters.Length != 6)
                {
                    string msgErro = string.Format(
                        "Os parâmetros para a ação {0} não foram corretamente especificados.", acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                SalvaAlteracoesFatorChave(id: parameters[1].Substring(2),
                    width: parameters[2],
                    height: parameters[3],
                    left: parameters[4],
                    top: parameters[5]);
                break;
            #endregion
            #region exclui
            case "exclui":
                if (parameters.Length != 2)
                {
                    string msgErro = string.Format(
                        "Os parâmetros para a ação {0} não foram corretamente especificados.", acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = parameters[1].Substring(2);
                ExcluiFatorChave(codigoFatorChave);
                result = codigoFatorChave;
                break;
            #endregion
            #region definenovofc
            case "definenovofc":
                if (parameters.Length != 6)
                {
                    string msgErro = string.Format(
                        "Os parâmetros para a ação {0} não foram corretamente especificados.", acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = IncluiFatorChave(width: parameters[2],
                    height: parameters[3],
                    left: parameters[4],
                    top: parameters[5]);
                result = codigoFatorChave;
                break;
            #endregion
            #region definetitulofc
            case "definetitulofc":
                if (parameters.Length != 3)
                {
                    string msgErro = string.Format(
                        "Os parâmetros para a ação {0} não foram corretamente especificados.", acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = parameters[1].Substring(2);
                string titulo = parameters[2];
                AlteraTituloFatorChave(codigoFatorChave, titulo: titulo);
                result = titulo;
                break;
            #endregion
        }
        e.Result = result;
    }

    protected void upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        UploadedFile file = e.UploadedFile;
        if ((file != null && file.IsValid))
        {
            //TODO:
            //Fazer verificações de extensão, tamanho e resolução da imagem
            string appVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
            string appPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;
            string caminhoFisicoArquivosTemp = string.Format("{0}ArquivosTemporarios\\", appPhysicalPath);
            string nomeImagemMapa = string.Format("Mapa_{0:yyMMddhhmmss}.jpg", DateTime.Now);
            string caminhoFiscoArquivo = string.Format("{0}{1}", caminhoFisicoArquivosTemp, nomeImagemMapa);
            string caminhoAbsolutoArquivo = string.Format("{0}{1}/ArquivosTemporarios/{2}",
                HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
                appVirtualPath,
                nomeImagemMapa);
            file.SaveAs(caminhoFiscoArquivo, true);
            e.CallbackData = HttpUtility.JavaScriptStringEncode(caminhoAbsolutoArquivo);

            AtualizaImagemMapa(file);
        }
    }

    #endregion

    #region Methods

    private static string SalvaImagemMapa(int codigoMapa, string caminhoFisicoArquivosTemp)
    {
        string nomeImagemMapa = string.Format("Mapa_{0:yyMMddhhmmss}.jpg", DateTime.Now);
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
    SELECT me.ImagemMapa 
      FROM MapaEstrategico AS me 
     WHERE CodigoMapaEstrategico = {0}", codigoMapa);

        #endregion

        dados cDados = CdadosUtil.GetCdados(null);
        DataSet ds = cDados.getDataSet(comandoSql);

        string caminhoArquivo = string.Format(@"{0}\{1}", caminhoFisicoArquivosTemp, nomeImagemMapa);
        using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write))
        {
            object value = ds.Tables[0].Rows[0]["ImagemMapa"];
            if (Convert.IsDBNull(value))
                return string.Empty;

            byte[] imagem = (byte[])value;
            fs.Write(imagem, 0, imagem.Length);

        }

        return nomeImagemMapa;
    }

    private void AtualizaImagemMapa(UploadedFile file)
    {
        using (Img img = Img.FromStream(file.FileContent))
        {
            if (cDados == null)
            {
                cDados = CdadosUtil.GetCdados(null);
                codigoMapa = int.Parse(Request.QueryString["cm"]);
                codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
                codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
            }
            string strConn = cDados.classeDados.getStringConexao();
            string comandoSql = string.Format(@"
UPDATE [MapaEstrategico]
   SET [ImagemMapa] = @ImagemMapa
 WHERE [CodigoMapaEstrategico] = @CodigoMapaEstrategico
 
UPDATE [ObjetoEstrategia]
   SET [AlturaObjetoEstrategia] = @Altura
      ,[LarguraObjetoEstrategia] = @Largura
      ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuario
      ,[DataUltimaAlteracao] = GETDATE()
 WHERE [CodigoMapaEstrategico] = @CodigoMapaEstrategico
   AND [CodigoTipoObjetoEstrategia] = (SELECT toe.[CodigoTipoObjetoEstrategia] 
                                         FROM [TipoObjetoEstrategia] AS toe 
                                        WHERE toe.[IniciaisTipoObjeto] = 'MAP')");
            SqlConnection conexao = new SqlConnection(strConn);
            SqlCommand comando = new SqlCommand()
            {
                Connection = conexao,
                CommandType = CommandType.Text,
                CommandText = comandoSql
            };
            comando.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            comando.Parameters.Add(new SqlParameter("@CodigoMapaEstrategico", codigoMapa));
            comando.Parameters.Add(new SqlParameter("@CodigoUsuario", codigoUsuario));
            comando.Parameters.Add(new SqlParameter("@Largura", img.Width));
            comando.Parameters.Add(new SqlParameter("@Altura", img.Height));
            comando.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@ImagemMapa",
                SqlDbType = SqlDbType.Image,
                Direction = ParameterDirection.Input,
                SourceColumn = "ImagemMapa",
                Value = file.FileBytes
            });

            conexao.Open();
            comando.ExecuteNonQuery();
        }
    }

    private string ObtemConteudoHtml()
    {
        StringBuilder sbRetorno = new StringBuilder();
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
        SELECT oe.CodigoObjetoEstrategia,
                oe.TituloObjetoEstrategia,
                oe.AlturaObjetoEstrategia,
                oe.LarguraObjetoEstrategia,
                oe.TopoObjetoEstrategia,
                oe.EsquerdaObjetoEstrategia,
                dbo.f_GetCorObjetivo(me.CodigoUnidadeNegocio, oe.CodigoObjetoEstrategia, {1}, {2}) AS CorIndicador
           FROM ObjetoEstrategia AS oe INNER JOIN
                MapaEstrategico AS me ON me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
          WHERE oe.CodigoMapaEstrategico = {0}
            AND oe.CodigoTipoObjetoEstrategia  = (SELECT [CodigoTipoObjetoEstrategia] FROM [TipoObjetoEstrategia] WHERE [IniciaisTipoObjeto] = 'PSP')
            AND oe.DataExclusao IS NULL", codigoMapa, DateTime.Today.Year, DateTime.Today.Month);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            object codigoObjetoEstrategia = dr["CodigoObjetoEstrategia"];
            object tituloObjetoEstrategia = dr["TituloObjetoEstrategia"];
            object alturaObjetoEstrategia = dr["AlturaObjetoEstrategia"];
            object larguraObjetoEstrategia = dr["LarguraObjetoEstrategia"];
            object topoObjetoEstrategia = dr["TopoObjetoEstrategia"];
            object esquerdaObjetoEstrategia = dr["EsquerdaObjetoEstrategia"];
            object corIndicador = dr["CorIndicador"];
            string divFatorChave = ObterDivFatorChave(codigoObjetoEstrategia,
                                       alturaObjetoEstrategia,
                                       larguraObjetoEstrategia,
                                       topoObjetoEstrategia,
                                       esquerdaObjetoEstrategia,
                                       corIndicador,
                                       tituloObjetoEstrategia);
            sbRetorno.Append(divFatorChave);
        }
        return sbRetorno.ToString();
    }

    private static string ObterDivFatorChave(object codigoObjetoEstrategia,
        object alturaObjetoEstrategia,
        object larguraObjetoEstrategia,
        object topoObjetoEstrategia,
        object esquerdaObjetoEstrategia,
        object corIndicador,
        object tituloObjetoEstrategia)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        corIndicador = cultureInfo.TextInfo.ToTitleCase(
            corIndicador.ToString().ToLower().Trim());
        StringBuilder divFatorChave = new StringBuilder();
        divFatorChave.AppendFormat(@"<div id='fc{0}' class='divFatorChave' style='top: {1}px; left: {2}px; width: {3}px; height: {4}px;'>",
            codigoObjetoEstrategia, topoObjetoEstrategia, esquerdaObjetoEstrategia, larguraObjetoEstrategia, alturaObjetoEstrategia);
        divFatorChave.AppendFormat("<span class='spanTitulo'>{0}</span>", tituloObjetoEstrategia);
        divFatorChave.Append("</div>");
        return divFatorChave.ToString();
    }

    private void SalvaAlteracoesFatorChave(string id, string width, string height, string left, string top)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @AlturaObjetoEstrategia smallint,
        @LarguraObjetoEstrategia smallint,
        @TopoObjetoEstrategia smallint,
        @EsquerdaObjetoEstrategia smallint,
        @CodigoUsuarioUltimaAlteracao int,
        @CodigoObjetoEstrategia int
        
    SET @AlturaObjetoEstrategia = {0}
    SET @LarguraObjetoEstrategia = {1}
    SET @TopoObjetoEstrategia = {2}
    SET @EsquerdaObjetoEstrategia = {3}
    SET @CodigoUsuarioUltimaAlteracao = {4}
    SET @CodigoObjetoEstrategia = {5}
    

UPDATE [ObjetoEstrategia]
   SET [AlturaObjetoEstrategia] = @AlturaObjetoEstrategia
      ,[LarguraObjetoEstrategia] = @LarguraObjetoEstrategia
      ,[TopoObjetoEstrategia] = @TopoObjetoEstrategia
      ,[EsquerdaObjetoEstrategia] = @EsquerdaObjetoEstrategia
      ,[DataUltimaAlteracao] = GETDATE()
      ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao
 WHERE [CodigoObjetoEstrategia] = @CodigoObjetoEstrategia"
            , height
            , width
            , top
            , left
            , codigoUsuario
            , id);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private string IncluiFatorChave(string width, string height, string left, string top)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @AlturaObjetoEstrategia smallint,
        @LarguraObjetoEstrategia smallint,
        @TopoObjetoEstrategia smallint,
        @EsquerdaObjetoEstrategia smallint,
        @CodigoUsuarioInclusao int,
        @CodigoObjetoEstrategia int,
        @CodigoMapa int,
        @CodigoVersaoMapaEstrategico tinyint,
        @TituloObjetoEstrategia varchar(100),
        @CodigoTipoObjetoEstrategia tinyint
        
    SET @AlturaObjetoEstrategia = {0}
    SET @LarguraObjetoEstrategia = {1}
    SET @TopoObjetoEstrategia = {2}
    SET @EsquerdaObjetoEstrategia = {3}
    SET @CodigoUsuarioInclusao = {4}
    SET @CodigoMapa = {5}
    SET @CodigoVersaoMapaEstrategico = 1
    SET @TituloObjetoEstrategia = 'Fator Chave'
    
 SELECT @CodigoTipoObjetoEstrategia = [CodigoTipoObjetoEstrategia]
   FROM [TipoObjetoEstrategia]
  WHERE [IniciaisTipoObjeto] = 'PSP'
    

INSERT INTO [ObjetoEstrategia] 
           ([AlturaObjetoEstrategia]
           ,[LarguraObjetoEstrategia]
           ,[TopoObjetoEstrategia]
           ,[EsquerdaObjetoEstrategia]
           ,[DataInclusao]
           ,[CodigoUsuarioInclusao]
           ,[CodigoMapaEstrategico]
           ,[CodigoVersaoMapaEstrategico]
           ,[TituloObjetoEstrategia]
           ,[CodigoTipoObjetoEstrategia]
           ,[OrdemObjeto])
    VALUES
           (@AlturaObjetoEstrategia
           ,@LarguraObjetoEstrategia
           ,@TopoObjetoEstrategia
           ,@EsquerdaObjetoEstrategia
           ,GETDATE()
           ,@CodigoUsuarioInclusao
           ,@CodigoMapa
           ,@CodigoVersaoMapaEstrategico
           ,@TituloObjetoEstrategia
           ,@CodigoTipoObjetoEstrategia
           ,1)
 
 SELECT 'CodigoObjetoEstrategia' = SCOPE_IDENTITY()"
            , height
            , width
            , top
            , left
            , codigoUsuario
            , codigoMapa);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);

        return ds.Tables[0].Rows[0]["CodigoObjetoEstrategia"].ToString();
    }

    private void ExcluiFatorChave(string id)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoUsuarioExclusao int,
        @CodigoObjetoEstrategia int
        
    SET @CodigoUsuarioExclusao = {0}
    SET @CodigoObjetoEstrategia = {1}    

UPDATE [ObjetoEstrategia]
   SET [DataExclusao] = GETDATE()
      ,[CodigoUsuarioExclusao] = @CodigoUsuarioExclusao
 WHERE [CodigoObjetoEstrategia] = @CodigoObjetoEstrategia"
            , codigoUsuario
            , id);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private void AlteraTituloFatorChave(string id, string titulo)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @TituloObjetoEstrategia varchar(100),
        @CodigoUsuarioUltimaAlteracao int,
        @CodigoObjetoEstrategia int
        
    SET @TituloObjetoEstrategia = '{0}'
    SET @CodigoUsuarioUltimaAlteracao = {1}
    SET @CodigoObjetoEstrategia = {2}    

UPDATE [ObjetoEstrategia]
   SET [TituloObjetoEstrategia] = @TituloObjetoEstrategia
      ,[DataUltimaAlteracao] = GETDATE()
      ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao
 WHERE [CodigoObjetoEstrategia] = @CodigoObjetoEstrategia"
            , titulo
            , codigoUsuario
            , id);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    #endregion
}