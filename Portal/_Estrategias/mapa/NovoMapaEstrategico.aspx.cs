using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.Services;
using System.IO;
using System.Web;
using System.Web.Hosting;
using DevExpress.Web;
using System.Web.Script.Serialization;
using Img = System.Drawing.Image;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Drawing;

public partial class _Estrategias_mapa_NovoMapaEstrategico : System.Web.UI.Page
{
	#region Fields

	dados cDados;

	int codigoMapa;
	int codigoEntidade;
	int codigoUsuario;
    //private string montaNomeImagemParametro;
    public string estiloFooter = "dxtlControl dxtlFooter";
    private string caminhoFisicoAplicacao = "";
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
		codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
		codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
		Session["ce"] = codigoEntidade;

		sdsDataSource.ConnectionString = cDados.classeDados.getStringConexao();

		string caminhoFisicoArquivosTemp = HttpUtility.JavaScriptStringEncode(
			string.Format("{0}ArquivosTemporarios", Request.ServerVariables["APPL_PHYSICAL_PATH"]));

        caminhoFisicoAplicacao = HttpUtility.JavaScriptStringEncode(Request.ServerVariables["APPL_PHYSICAL_PATH"]);

        hfGeral.Set("caminhoFisicoArquivosTemp", caminhoFisicoArquivosTemp);
        hfGeral.Set("caminhoFisicoAplicacao", caminhoFisicoAplicacao);

        codigoMapa = int.Parse(Request.QueryString["cm"]);
		hfGeral.Set("cm", codigoMapa);

        cDados.aplicaEstiloVisual(this);
	}

	protected void callback_Callback(object source, CallbackEventArgs e)
	{
		//exportaGestaoEstrategica
		if (e.Parameter.Contains("export"))
		{
			MemoryStream stream = new MemoryStream();
			PdfExportOptions options = new PdfExportOptions();
			XtraReport rel;
			XtraReport relCapa;
			if (e.Parameter == "exportaGestaoEstrategica")
			{
				rel = new relGestaoEstrategica();
				((relGestaoEstrategica)rel).CodigoMapa.Value = codigoMapa;
				relCapa = new relProjetosEstrategicosCapa("Relatório de Gestão Estratégica");
			}
			else if (e.Parameter == "exportaProjetosEstrategicos")
			{

				rel = new relProjetosEstrategicos(codigoMapa, codigoUsuario, DateTime.Today.Year);
				//rel.Parameters["pathArquivo"].Value = montaNomeImagemParametro;
				relCapa = new relProjetosEstrategicosCapa("Boletim de Projetos Estratégicos");
			}
			else
			{
				throw new Exception("Modelo de relatório não identificado.");
			}
			relCapa.CreateDocument();
			rel.CreateDocument();

			rel.Pages.Insert(0, relCapa.Pages.First);
			PageWatermark watermark = new PageWatermark()
			{
				Image = relCapa.Watermark.Image,
				ImageAlign = relCapa.Watermark.ImageAlign,
				ImageTiling = false,
				ImageViewMode = ImageViewMode.Stretch,
				ShowBehind = relCapa.Watermark.ShowBehind
			};
			rel.Pages.First.AssignWatermark(watermark);
			rel.ExportToPdf(stream, options);
			Session["exportStream"] = stream;
		}
		else
		{
			string[] parameters = e.Parameter.Split(';');
			string codigoMapa = parameters[0];
			string caminhoFisicoArquivosTemp = parameters[1];
			int larguraResolucaoCliente = int.Parse(parameters[2]);
			int alturaResolucaoCliente = int.Parse(parameters[3]);
			e.Result = ObterDadosFatoresChave(codigoMapa, caminhoFisicoArquivosTemp, larguraResolucaoCliente, alturaResolucaoCliente, caminhoFisicoAplicacao);
		}
	}

    #endregion

    #region Methods

    private static string ObterDivFatorChave(int codigoObjetoEstrategia,
        short alturaObjetoEstrategia,
        short larguraObjetoEstrategia,
        short topoObjetoEstrategia,
        short esquerdaObjetoEstrategia,
        string corIndicador,
        bool permissao)
    {
        string cursor = permissao ? "" : "cursor: default;";
        string perm = permissao ? "1" : "0";
        corIndicador = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(corIndicador.ToLower().Trim());
        StringBuilder divFatorChave = new StringBuilder();
        divFatorChave.AppendFormat(@"<div id='fc{0}' class='divFatorChave' style='top: {1}px; left: {2}px; width: {3}px; height: {4}px; {5}' perm='{6}'>",
            codigoObjetoEstrategia, topoObjetoEstrategia, esquerdaObjetoEstrategia, larguraObjetoEstrategia, alturaObjetoEstrategia, cursor, perm);
        divFatorChave.AppendFormat("<div id='in{0}' class='divIndicador{1}' {2}></div>", codigoObjetoEstrategia, corIndicador,
            permissao ? "" : "style='cursor:default;'");
        divFatorChave.Append("</div>");
        return divFatorChave.ToString();
    }

    private static string SalvaImagemMapa(int codigoMapa, string caminhoFisicoArquivosTemp, int width, int height)
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

        string caminhoArquivo = string.Format(@"{0}\{1}",
            caminhoFisicoArquivosTemp, nomeImagemMapa);
        object value = ds.Tables[0].Rows[0]["ImagemMapa"];
        if (Convert.IsDBNull(value))
        {
            string msgErro = HttpContext.Current.Server.HtmlEncode(
                "Não foi definida a imagem do mapa.");
            throw new Exception(msgErro);
        }
        byte[] imagem = (byte[])value;
        using (MemoryStream ms = new MemoryStream(imagem))
        {
            ms.Position = 0;
            Img imgOginal = Img.FromStream(ms);
            Bitmap imgRedimensionada = new Bitmap(imgOginal, width, height);
            imgRedimensionada.Save(caminhoArquivo);
        }
        /*using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write))
		{
			object value = ds.Tables[0].Rows[0]["ImagemMapa"];
			if (Convert.IsDBNull(value))
			{
				string msgErro = HttpContext.Current.Server.HtmlEncode(
					"Não foi definida a imagem do mapa.");
				throw new Exception(msgErro);
			}

			byte[] imagem = (byte[])value;
			fs.Write(imagem, 0, imagem.Length);
		}*/

        return nomeImagemMapa;
    }

    public static Img ObtemImagemMapa(int codigoMapa, ref bool indicaFaltaDeImagem, string caminhoArquivoNoImage = "")
    {
        Img imgOginal;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
    SELECT me.ImagemMapa 
      FROM MapaEstrategico AS me 
     WHERE CodigoMapaEstrategico = {0}", codigoMapa);

        #endregion

        dados cDados = CdadosUtil.GetCdados(null);
        DataSet ds = cDados.getDataSet(comandoSql);
        object value = ds.Tables[0].Rows[0]["ImagemMapa"];
        if (Convert.IsDBNull(value))
        {
            //string msgErro = HttpContext.Current.Server.HtmlEncode(
            //    "Não foi definida a imagem do mapa.");
            indicaFaltaDeImagem = true;
            imgOginal = Img.FromFile(caminhoArquivoNoImage);
            //throw new Exception(msgErro);
        }
        else
        {
            byte[] imagem = (byte[])value;
            using (MemoryStream ms = new MemoryStream(imagem))
            {
                ms.Position = 0;
                imgOginal = Img.FromStream(ms);
            }
        }        
        return imgOginal;
    }

    private static Bitmap ObtemImagemMapaRedimensionada(int larguraResolucaoCliente, int alturaResolucaoCliente, Img imgOrinalMapa, bool indicaNaotemImagem = false)
    {
        int larguraOrig = imgOrinalMapa.Width;
        int alturaOrig = imgOrinalMapa.Height;
        int larguraRedimensionada;
        int alturaRedimensionada;
        float relacaoLarguraAltura = (float)larguraOrig / (float)alturaOrig;
        if ((larguraResolucaoCliente / relacaoLarguraAltura) < alturaResolucaoCliente)
        {
            larguraRedimensionada = larguraResolucaoCliente;
            alturaRedimensionada = (int)(larguraResolucaoCliente / relacaoLarguraAltura);
        }
        else
        {
            larguraRedimensionada = (int)(alturaResolucaoCliente * relacaoLarguraAltura);
            alturaRedimensionada = alturaResolucaoCliente;
        }
        Bitmap imgRedimensionada = new Bitmap(
            imgOrinalMapa, larguraRedimensionada, alturaRedimensionada);

        Bitmap imgOriginalBitMap = new Bitmap(imgOrinalMapa, larguraOrig, alturaOrig);

        return indicaNaotemImagem == true ? imgOriginalBitMap : imgRedimensionada;
    }

    public static string ObterDadosFatoresChave(string codigoMapa, string caminhoFisicoArquivosTemp, int larguraResolucaoCliente, int alturaResolucaoCliente, string caminhoFisicoAplicacao)
    {
        string nomeImagemMapa =
            string.Format("Mapa_{0:yyMMddhhmmss}.jpg", DateTime.Now);
        string caminhoCompletoImagem =
            string.Format(@"{0}\\{1}", caminhoFisicoArquivosTemp, nomeImagemMapa);
        bool indicaFaltaDeImagem = false;
        Img imgOrinalMapa = ObtemImagemMapa(int.Parse(codigoMapa), ref indicaFaltaDeImagem, caminhoFisicoAplicacao + "imagens\\img-no-image-n.png");
        Bitmap imgRedimensionada = ObtemImagemMapaRedimensionada(
            larguraResolucaoCliente, alturaResolucaoCliente, imgOrinalMapa, indicaFaltaDeImagem);
        float fatorRedimensionamentoImagem =
           (float)imgRedimensionada.Height / (float)imgOrinalMapa.Height;

        string conteudoHtml = ObtemConteudoHtml(codigoMapa, fatorRedimensionamentoImagem);
        imgRedimensionada.Save(caminhoCompletoImagem);
        string result = new JavaScriptSerializer().Serialize(new
        {
            NomeImagemMapa = nomeImagemMapa,
            AlturaImagem = imgRedimensionada.Height,
            LarguraImagem = imgRedimensionada.Width,
            InnerHtml = conteudoHtml
        });

        return result;
    }

    private static string ObtemConteudoHtml(string codigoMapa, float fatorRedimensionamento)
    {
        StringBuilder sbRetorno = new StringBuilder();
        string comandoSql;
        dados cDados = CdadosUtil.GetCdados(null);
        string codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        string codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();

        #region Comando SQL

        comandoSql = string.Format(@"
        SELECT oe.CodigoObjetoEstrategia,
                oe.TituloObjetoEstrategia,
                oe.AlturaObjetoEstrategia,
                oe.LarguraObjetoEstrategia,
                oe.TopoObjetoEstrategia,
                oe.EsquerdaObjetoEstrategia,
                dbo.f_GetCorObjetivo(me.CodigoUnidadeNegocio, oe.CodigoObjetoEstrategia, {1}, {2}) AS CorIndicador,
                dbo.f_VerificaAcessoConcedido({3}, {4}, oe.CodigoObjetoEstrategia, NULL, 'OB', 0, NULL, 'OB_CnsDtl') AS Permissao
           FROM ObjetoEstrategia AS oe INNER JOIN
                MapaEstrategico AS me ON me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
          WHERE oe.CodigoMapaEstrategico = {0}
            AND oe.CodigoTipoObjetoEstrategia  = (SELECT [CodigoTipoObjetoEstrategia] FROM [TipoObjetoEstrategia] WHERE [IniciaisTipoObjeto] = 'OBJ')
            AND oe.DataExclusao IS NULL", codigoMapa, DateTime.Today.Year, DateTime.Today.Month, codigoUsuario, codigoEntidade);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            int codigoObjetoEstrategia = dr.Field<int>("CodigoObjetoEstrategia");
            short alturaObjetoEstrategia = dr.Field<short>("AlturaObjetoEstrategia");
            short larguraObjetoEstrategia = dr.Field<short>("LarguraObjetoEstrategia");
            short topoObjetoEstrategia = dr.Field<short>("TopoObjetoEstrategia");
            short esquerdaObjetoEstrategia = dr.Field<short>("EsquerdaObjetoEstrategia");
            string corIndicador = dr["CorIndicador"].ToString();
            string tituloObjetoEstrategia = dr["TituloObjetoEstrategia"].ToString();
            bool permissao = dr.Field<bool>("Permissao");
            alturaObjetoEstrategia = (short)(alturaObjetoEstrategia * fatorRedimensionamento);
            larguraObjetoEstrategia = (short)(larguraObjetoEstrategia * fatorRedimensionamento);
            topoObjetoEstrategia = (short)(topoObjetoEstrategia * fatorRedimensionamento);
            esquerdaObjetoEstrategia = (short)(esquerdaObjetoEstrategia * fatorRedimensionamento);
            string divFatorChave = ObterDivFatorChave(codigoObjetoEstrategia,
                                       alturaObjetoEstrategia,
                                       larguraObjetoEstrategia,
                                       topoObjetoEstrategia,
                                       esquerdaObjetoEstrategia,
                                       corIndicador,
                                       permissao);
            sbRetorno.AppendLine(divFatorChave);
        }
        string strRetorno = sbRetorno.ToString();
        return strRetorno;
    }

    #endregion
}