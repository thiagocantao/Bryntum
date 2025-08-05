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

public partial class _Estrategias_mapa_ConfiguracaoNovoMapaEstrategico : System.Web.UI.Page
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

		carregaComboTemasPerspectivas(codigoMapa);       
        Master.geraRastroSite();

        lblSemImagem.Text = @Resources.traducao.ConfiguracaoNovoMapaEstrategico_clique_em_selecionar_imagem_do_mapa_para_escolher_o_desenho_do_mapa_estrat_gico_a_ser_utilizado_para_acompanhamento___;

        menuMapa.Items[0].ToolTip = String.Format(" " + Resources.traducao.ConfiguracaoNovoMapaEstrategico_resolu__o_recomendada + ": 800x600px{0}"+ Resources.traducao.ConfiguracaoNovoMapaEstrategico_formato_permitido + ": .JPG", Environment.NewLine);

        //Carrega todas as traduções da página ConfiguracaoNovoMapaEstrategico para o JavaScript
        this.TH(this.TS("ConfiguracaoNovoMapaEstrategico"));
    }
       
    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_MostraLP"] = "";
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int largura = 0;
        int altura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        ((ASPxCallback)source).JSProperties["cpAltura"] =  altura - 190;

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

                callback.JSProperties["cp_MostraLP"] = nomeImagemMapa == "" ? "S" : "N"; 
                
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
                if (parameters.Length != 7)
                {
                    string msgErro = string.Format(
                          Resources.traducao.ConfiguracaoNovoMapaEstrategico_os_par_metros_para_a_a__o + " { 0} " + Resources.traducao.ConfiguracaoNovoMapaEstrategico_n_o_foram_corretamente_especificados, acao);
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
                          Resources.traducao.ConfiguracaoNovoMapaEstrategico_os_par_metros_para_a_a__o + " { 0} " + Resources.traducao.ConfiguracaoNovoMapaEstrategico_n_o_foram_corretamente_especificados, acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = parameters[1].Substring(2);
                ExcluiFatorChave(codigoFatorChave);
                result = codigoFatorChave;
                break;
            #endregion
            #region definenovofc
            case "definenovofc":
                if (parameters.Length != 7)
                {
                    string msgErro = string.Format(
                          Resources.traducao.ConfiguracaoNovoMapaEstrategico_os_par_metros_para_a_a__o + " { 0} " + Resources.traducao.ConfiguracaoNovoMapaEstrategico_n_o_foram_corretamente_especificados, acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = IncluiFatorChave(width: parameters[2],
                    height: parameters[3],
                    left: parameters[4],
                    top: parameters[5],
					codigoSuperior: parameters[6]);
                result = codigoFatorChave;
                break;
            #endregion
            #region definetitulofc
            case "definetitulofc":
                if (parameters.Length != 3)
                {
                    string msgErro = string.Format(
                       Resources.traducao.ConfiguracaoNovoMapaEstrategico_os_par_metros_para_a_a__o + " { 0} " + Resources.traducao.ConfiguracaoNovoMapaEstrategico_n_o_foram_corretamente_especificados, acao);
                    throw new ArgumentException(HttpContext.Current.Server.HtmlEncode(msgErro));
                }
                codigoFatorChave = parameters[1].Substring(2);
                string titulo = parameters[2];
                AlteraTituloFatorChave(codigoFatorChave, titulo);
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
				oe.CodigoObjetoEstrategiaSuperior,
                dbo.f_GetCorObjetivo(me.CodigoUnidadeNegocio, oe.CodigoObjetoEstrategia, {1}, {2}) AS CorIndicador
           FROM ObjetoEstrategia AS oe INNER JOIN
                MapaEstrategico AS me ON me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
          WHERE oe.CodigoMapaEstrategico = {0}
            AND oe.CodigoTipoObjetoEstrategia  = (SELECT [CodigoTipoObjetoEstrategia] FROM [TipoObjetoEstrategia] WHERE [IniciaisTipoObjeto] = 'OBJ')
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
			object codigoSuperior = dr["CodigoObjetoEstrategiaSuperior"];

			string divFatorChave = ObterDivFatorChave(codigoObjetoEstrategia,
									   alturaObjetoEstrategia,
									   larguraObjetoEstrategia,
									   topoObjetoEstrategia,
									   esquerdaObjetoEstrategia,
									   corIndicador,
									   tituloObjetoEstrategia,
									   codigoSuperior);
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
		object tituloObjetoEstrategia, 
		object codigoSuperior)
	{
		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		corIndicador = cultureInfo.TextInfo.ToTitleCase(
			corIndicador.ToString().ToLower().Trim());
		StringBuilder divFatorChave = new StringBuilder();
		divFatorChave.AppendFormat(@"<div id='fc{0}' class='divFatorChave' cp_CodigoSuperior='{5}' style='top: {1}px; left: {2}px; width: {3}px; height: {4}px;'>",
			codigoObjetoEstrategia, topoObjetoEstrategia, esquerdaObjetoEstrategia, larguraObjetoEstrategia, alturaObjetoEstrategia, codigoSuperior);
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

    private string IncluiFatorChave(string width, string height, string left, string top, string codigoSuperior)
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
        @CodigoTipoObjetoEstrategia tinyint,
		@CodigoObjetoEstrategiaSuperior int
        
    SET @AlturaObjetoEstrategia = {0}
    SET @LarguraObjetoEstrategia = {1}
    SET @TopoObjetoEstrategia = {2}
    SET @EsquerdaObjetoEstrategia = {3}
    SET @CodigoUsuarioInclusao = {4}
    SET @CodigoMapa = {5}
    SET @CodigoVersaoMapaEstrategico = 1
    SET @TituloObjetoEstrategia = '{7}'
	SET @CodigoObjetoEstrategiaSuperior = {6}
    
 SELECT @CodigoTipoObjetoEstrategia = [CodigoTipoObjetoEstrategia]
   FROM [TipoObjetoEstrategia]
  WHERE [IniciaisTipoObjeto] = 'OBJ'
    

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
		   ,[DescricaoObjetoEstrategia]
           ,[CodigoTipoObjetoEstrategia]
           ,[OrdemObjeto]
		   ,[CodigoObjetoEstrategiaSuperior])
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
		   ,@TituloObjetoEstrategia
           ,@CodigoTipoObjetoEstrategia
           ,1
		   ,@CodigoObjetoEstrategiaSuperior)
 
 SET @CodigoObjetoEstrategia = SCOPE_IDENTITY()
SELECT 'CodigoObjetoEstrategia' = @CodigoObjetoEstrategia

                    --Em seguida à gravação dos dados, deve ser acionada a proc [p_ProcessaMatrizAcessos] 
                    --para que a matriz de acesso do usuário logado seja refeita para o mapa cujos dados estão sendo gravadas;
                    
                    DECLARE @RC int
                    DECLARE @in_iniciaisTipoObjeto char(2)
                    DECLARE @in_codigoObjetoPai bigint
                    DECLARE @in_codigoEntidade int
                    DECLARE @in_codigoPermissao smallint
                    DECLARE @in_codigoUsuario int
                    DECLARE @in_codigoPerfil int
                    DECLARE @in_codigoObjeto INT

                    SET @in_iniciaisTipoObjeto =  'OB'       
                    SET @in_codigoObjeto = @CodigoObjetoEstrategia
                    SET @in_codigoObjetoPai = 0
                    SET @in_codigoEntidade = {8}
                    SET @in_codigoPermissao = NULL
                    SET @in_codigoUsuario = {4}
                    SET @in_codigoPerfil = NULL

                    EXECUTE @RC = [dbo].[p_ProcessaMatrizAcessos] 
                        @in_iniciaisTipoObjeto
                        ,@in_codigoObjeto
                        ,@in_codigoObjetoPai
                        ,@in_codigoEntidade
                        ,@in_codigoPermissao
                        ,@in_codigoUsuario
                        ,@in_codigoPerfil

                   --Em seguida ao acionamento da proc do item anterior, deve ser acionada outra proc, 
                   --a [p_incluiJobProcessaMatrizAcessos] para que seja incluído um job que referá a matriz de permissão do mapa para todos os usuários;

                    EXECUTE @RC = [dbo].[p_incluiJobProcessaMatrizAcessos] 
                       @in_iniciaisTipoObjeto
                      ,@in_codigoObjeto
                      ,@in_codigoObjetoPai
                      ,@in_codigoEntidade
                      ,@in_codigoPermissao
                      ,NULL
                      ,@in_codigoPerfil
                      ,NULL"
            , height
            , width
            , top
            , left
            , codigoUsuario
            , codigoMapa
			, codigoSuperior
			, txtTituloFatorChave.Text, codigoEntidade);

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
	  ,[DescricaoObjetoEstrategia] = @TituloObjetoEstrategia
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

	private void carregaComboTemasPerspectivas(int codigoMapa)
	{
		string where = string.Format(@" 
            AND me.CodigoMapaEstrategico            = {0}
            AND toe.[IniciaisTipoObjeto]            IN('TEM', 'PSP')"
			, codigoMapa);

		DataSet dsObjetos = cDados.getObjetosEstrategicos(codigoUsuario, codigoEntidade, where
			, "ORDER BY [DescricaoObjeto]", false, "");

		if (cDados.DataSetOk(dsObjetos))
		{
			ddlTema.DataSource = dsObjetos;
			ddlTema.TextField = "DescricaoObjeto";
			ddlTema.ValueField = "CodigoObjetoEstrategia";
			//ddlTema.ImageUrlField = "urlImagemObjetoCombo";
			ddlTema.DataBind();
		}
	}

    #endregion
}