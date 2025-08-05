using Cdis.gantt;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_ganttDesktop : System.Web.UI.Page
{
    dados cDados;

    public int codigoProjeto = -1;
    public int codigoUsuario = -1;
    public int codigoEntidade = -1;
    public int controleLocal = -1;
    public int controleVindoDoTasques = -1;


    public string alturaGrafico = "", larguraGrafico = "", nenhumGrafico = "";
    public string nomeProjeto = "";

    public string estiloFooter = "dxtlFooter";
    public int versaoLinhaBase = -1;
    CdisGanttHelper cdisGanttHelper;
    string mindate;
    string maxdate;
    protected void Page_Init(object sender, EventArgs e)
    {

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Request.UserHostAddress;
        listaParametrosDados["NomeUsuario"] = "Suporte BriskPPM";


        //codigoEntidade = int.Parse(Request.QueryString["CE"].ToString());
        //codigoUsuario = int.Parse(Request.QueryString["U"].ToString());
        //codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        //controleLocal = Math.Abs(ObtemCodigoHash(((codigoUsuario * codigoProjeto * codigoEntidade) + (codigoEntidade - codigoUsuario)) + "CDIS"));

        //bool r = int.TryParse(Request.QueryString["controle"] != null ? Request.QueryString["controle"].ToString() : "-1", out controleVindoDoTasques);
        //if (controleLocal != controleVindoDoTasques)
        //{
        //    try
        //    {

        //        Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
        //    }
        //    catch
        //    {
        //        Response.RedirectLocation = VirtualPathUtility.ToAbsolute("~/") + "erros/SemAcessoNoMaster.aspx";
        //        Response.End();
        //    }
        //}
        //cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
        //cDados.setInfoSistema("CodigoEntidade", codigoEntidade);
        //cDados.setInfoSistema("IDEstiloVisual", "Office2003Blue");
        //try
        //{
        //    if (cDados.getInfoSistema("IDUsuarioLogado") == null)
        //        Response.Redirect("~/erros/erroInatividade.aspx");
        //}
        //catch
        //{
        //    Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
        //    Response.End();
        //}
    }

    public static int ObtemCodigoHash(string str)
    {
        int valorRetorno = 0;
        int acumulador = 0;
        MD5 md5Hasher = MD5.Create();
        char[] caracteres = str.ToCharArray();

        foreach (char caracter in caracteres)
        {
            acumulador += caracter * Array.IndexOf(caracteres, caracter) + caracteres.Length;
        }

        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        valorRetorno = int.Parse(sBuilder.ToString(0, 8), System.Globalization.NumberStyles.HexNumber) ^ (int.Parse(sBuilder.ToString(8, 8), System.Globalization.NumberStyles.HexNumber) / acumulador) ^ int.MaxValue;
        return (valorRetorno % 2 == 0) ? valorRetorno : -valorRetorno;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //this.TH(this.TS("Cronograma_gantt"));
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);


        //hfCodigoProjeto.Set("CodigoProjeto", codigoProjeto);
        //hfCodigoProjeto.Set("NomeProjeto", nomeProjeto);

        Header.Controls.Add(getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/basicBryNTum/basic_tasques.js""></script>"));
        this.TH(this.TS("geral", "basic"));

        //Função que gera o gráfico
        geraGrafico();


    }

    public Literal getLiteral(string texto)
    {
        int indexJS = texto.ToLower().IndexOf(".js");

        if (indexJS != -1 && texto.Trim().Contains("<script"))
        {
            string param = "?V=" + getStringDataHoraCompleta();
            texto = texto.Insert(indexJS + 3, param);
        }

        Literal myLiteral = new Literal();
        myLiteral.Text = texto;
        return myLiteral;
    }

    public string getStringDataHoraCompleta()
    {
        return DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
    }

    //Função para geração do gráfico Gantt - Bryntum
    private void geraGrafico()
    {
        bool removerIdentacao = true;

        versaoLinhaBase = -1;

        bool possuiCronograma = geraGraficoJsonTaskData(removerIdentacao);
        geraDependenciasJSON(possuiCronograma);

        if (!possuiCronograma)
        {
            nenhumGrafico = getGanttVazio("500");

            alturaGrafico = "0";
        }


    }

    public string getGanttVazio(string altura)
    {
        return string.Format(@"<div style=""height:{0}px; width:100%; font-family:Verdana; font-size:8pt; text-align: center;""><span>" + Resources.traducao.dados_ainda_n_o_h__cronograma_planejado_para_ser_exibido + @"</span></div>", altura);

    }

    private bool geraGraficoJsonTaskData(bool removerIdentacao)
    {
        var percentualConcluido = new int?();
        var data = new DateTime?();
        //atribui o valor do caminho do JSON a ser carregado
        string caminhoJSON = "../ArquivosTemporarios/" + Request.QueryString["NomeGantt"] + ".json";

        mindate = Request.QueryString["mindate"];
        maxdate = Request.QueryString["maxdate"];

        string DataInicio = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, -2)", DateTime.Parse(mindate));
        string DataTermino = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, 2)", DateTime.Parse(maxdate));

        string scripts = @"<script type=""text/javascript"">var urlJSON = """ + caminhoJSON + @""";
                                                                                 var dataInicio = " + DataInicio + @";
                                                                                 var dataTermino = " + DataTermino + @";
                                                </script>";
        Literal literal = new Literal();
        literal.Text = scripts;

        Header.Controls.Add(literal);


        return true;
    }
    // Gera as tarefas filhas
    private void geraDependenciasJSON(bool possuiCronograma)
    {
        //cria  a variável para armazenar o JSON_BryNTum
        string caminhoJSON = "../ArquivosTemporarios/" + Request.QueryString["NomeDependencias"];
        string scripts = @"<script type=""text/javascript"">var urlJSONDep = """ + caminhoJSON + @""";
                                                </script>";
        Literal literal = new Literal();
        literal.Text = scripts;

        Header.Controls.Add(literal);

    }






}
