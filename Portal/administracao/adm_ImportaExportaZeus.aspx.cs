using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.IO;
using DevExpress.XtraPrinting;

public partial class administracao_ImportaExportaZeus : System.Web.UI.Page
{

    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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


        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_IntegZeus");
        }


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);



        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 80;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_ImportaExportaZeus.js""></script>"));
    }

    #endregion

    #region COMBOBOX


    #endregion

    #region DVGRID

    private void populaGrid()
    {
        DataSet ds = cDados.getArquivoExportacaoZeus();
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void populaGridGeraArquivo()
    {
        DataSet ds = cDados.getDadosGeraArquivoCronogramaOrcamentario();
        if (cDados.DataSetOk(ds))
        {
            gvDadosGeraArquivo.DataSource = ds.Tables[0];
            gvDadosGeraArquivo.DataBind();
        }
    }


    #endregion

    #region CALLBACK's


    #endregion

    #region BANCO DE DADOS


    #endregion



    protected void grava(StreamWriter s, string linha)
    {
        if (s != null)
            s.WriteLine(linha);
    }

    protected string getDiretorioIntegracaoZeus()
    {
        string raiz = "";
        DataSet dsParametros = cDados.getParametrosSistema(CodigoEntidade, "diretorioIntegracaoZeus");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            raiz = dsParametros.Tables[0].Rows[0]["diretorioIntegracaoZeus"].ToString();
            if (!raiz.Equals("") && !raiz.Substring(raiz.Length - 1, 1).Equals("\\"))
            {
                raiz = raiz + '\\';
            }
        }
        return raiz;
    }




    protected void moveArquivo(string origem, string destino, string dir)
    {
        int qtd = 0, qtd2 = 0;
        string primeiraVez = "S";

        if (File.Exists(destino))
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            FileInfo[] rgFiles = di.GetFiles(destino.Replace(dir, ""));
            while (rgFiles.Length != 0)
            {
                foreach (FileInfo fi in rgFiles)
                {
                    qtd += 1;
                }

                if (primeiraVez == "S")
                {
                    primeiraVez = "N";
                    destino = destino.Replace(dir, dir + "(" + qtd + ")");

                }
                else
                {
                    qtd2 = qtd - 1;
                    destino = destino.Replace(dir + "(" + qtd2 + ")", dir + "(" + qtd + ")");
                }
                rgFiles = di.GetFiles(destino.Replace(dir, ""));
            }
        }
        System.IO.File.Move(origem, destino);
    }

    protected void btnGerarArquivo_Click(object sender, EventArgs e)
    {
        string podeGerarArquivo = hfGeral.Get("podeGerarArquivo").ToString();
        StreamWriter swriterExporta = null, swriterLogProcesso = null; ;
        if (podeGerarArquivo.Equals("N"))
        {

            string script = @"<script type='text/Javascript' language='Javascript'>
                                   window.top.mostraMensagem('Geração de Arquivo cancelada pelo usuário.', 'atencao', true, false, null);</script>";

            ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
        }
        else
        {
            populaGridGeraArquivo();

            using (MemoryStream stream = new MemoryStream())
            {
                string nomeArquivo = "", erro = "", linha = "", nomeProjeto = "", raiz = "", codigoAcao = "", NomeAtividade = "";
                try
                {
                    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
                    raiz = getDiretorioIntegracaoZeus();
                    if (raiz == "")
                    {
                        throw new Exception(@"O parâmetro que identifica o Diretório onde serão armazenados os arquivos gerados para integração com o ZEUS não foi informado." +
                                            @"\n\nVerifique por favor na tela de parâmetros o grupo Outras Configurações!");
                    }
                    raiz = raiz + "CronogramaOrcamentario\\";
                    //Se o diretório não existir...
                    if (!Directory.Exists(raiz))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(raiz);
                    }
                    string folder = @raiz + dataHora; //nome do diretorio a ser criado
                    //Se o diretório não existir...
                    if (!Directory.Exists(folder))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folder);
                    }

                    swriterLogProcesso = new StreamWriter(@folder + "\\CronogramaOrcamentario_" + dataHora + ".log");
                    grava(swriterLogProcesso, "Inicio do processo de geração do arquivo Cronograma Orçamentário " + DateTime.Now.ToString());
                    for (int i = 0; i < gvDadosGeraArquivo.VisibleRowCount; i++)
                    {
                        DataRowView dt = (DataRowView)gvDadosGeraArquivo.GetRow(i);
                        
                        if (nomeProjeto != dt["NomeProjeto"].ToString())
                        {
                            if (swriterExporta != null)
                                swriterExporta.Close();

                            nomeArquivo = @folder + "\\" + dt["NomeProjeto"].ToString() + "_" + dataHora + ".csv";
                            //System.Text.Encoding encoding = new System.Text.UTF32Encoding();
                            swriterExporta = new StreamWriter(nomeArquivo, false, System.Text.Encoding.GetEncoding("UTF-8"));
                            nomeProjeto = dt["NomeProjeto"].ToString();
                            ASPxMemo1.Text = ASPxMemo1.Text + (nomeArquivo + "\n");

                            grava(swriterLogProcesso, nomeArquivo + " -> Processado - OK\n");
                            decimal totalRP = Convert.IsDBNull ( dt["totalRP"]) ? 0 : (decimal)dt["totalRP"] ;
                            decimal totalFU = Convert.IsDBNull ( dt["totalFundecop"]) ? 0 : (decimal)dt["totalFundecop"] ;

                            decimal valorTotal =  totalRP + totalFU ; 
                            linha = "SESCOOP - Cronograma Orçamentário;;;;;;;;;;;;\n" + ";;;;;;;;;;;;\n" +
                                    //"Cronograma Orçamentário;;;;;;;;;;;;\n" +
                                    //";;;;;;;;;;;;" +
                                    "Projeto: " + dt["NomeProjeto"].ToString().Trim() + ";;;;;Total UN: ;" + dt["totalRP"].ToString().Trim() + ";;Total FUNDECOOP: ;"
                                    + dt["totalFundecop"].ToString().Trim() + ";;Total GERAL: ;" + dt["totalGeral"].ToString().Trim();// valorTotal;
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (codigoAcao != dt["CodigoAcao"].ToString())
                        {
                            codigoAcao = dt["CodigoAcao"].ToString();
                            linha = "  \nAÇÃO: " + dt["NomeAcao"].ToString().Trim() + ";;;;;;Fonte de recursos: " + dt["FonteRecurso"].ToString().Trim() +
                                    ";;;;;Total AÇÃO:  ;" + dt["totalAcao"].ToString().Trim() + "";
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (dt["NomeAtividade"].ToString() != "" && NomeAtividade != dt["NomeAtividade"].ToString())
                        {
                            NomeAtividade = dt["NomeAtividade"].ToString();
                            linha = "    ATIVIDADE: " + dt["NomeAtividade"].ToString() + ";;;;;;;;;;;Total Atividade:  ;"+ dt["totalAtividade"].ToString().Trim() + "" ;
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (dt["ContaOrcamentaria"].ToString() != "")
                        {
                            linha = "      Conta Orçamentária  ;;Quantidade   ; Valor Unitário;Valor Total;;Memória de Cálculo  ;;;;;;";
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                            linha = "      " + dt["ContaOrcamentaria"].ToString() + ";;" + dt["Quantidade"].ToString() + ";" + dt["ValorUnitario"].ToString() +
                                    ";" + dt["ValorTotal"].ToString() + ";;\"" + dt["MemoriaCalculo"].ToString() + "\";;;;;;\n" +
                                    ";Janeiro  ;Fevereiro  ;Março  ;Abril  ;Maio  ;Junho  ;Julho  ;Agosto  ;Setembro  ;Outubro  ;Novembro  ;Dezembro\n ";
                            // linha = string.Format("{0};;{1};{2};{3};;\"{4}\";;;;;;", dt["ContaOrcamentaria"], dt["Quantidade"], dt["ValorUnitario"], dt["ValorTotal"], dt["MemoriaCalculo"]);
                            linha = linha + string.Format(";{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}\n",
                                dt["Janeiro"].ToString(),
                                dt["Fevereiro"].ToString(),
                                dt["Marco"].ToString(),
                                dt["Abril"].ToString(),
                                dt["Maio"].ToString(),
                                dt["Junho"].ToString(),
                                dt["Julho"].ToString(),
                                dt["Agosto"].ToString(),
                                dt["Setembro"].ToString(),
                                dt["Outubro"].ToString(),
                                dt["Novembro"].ToString(),
                                dt["Dezembro"].ToString()
                                );
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }
                        
                        //linha = dt["CodigoUnidade"].ToString().Trim() + ";" +
                        //        dt["NomeUnidade"].ToString().Trim() + ";" +
                        //        dt["SiglaUnidade"].ToString().Trim() + ";" +
                        //        dt["NomeProjeto"].ToString().Trim() + ";" +
                        //        dt["Nomeacao"].ToString().Trim() + ";" +
                        //        dt["ContaOrcamentaria"].ToString().Trim() + ";" +
                        //        dt["Mes"].ToString().Trim() + ";" +
                        //        dt["Valororcado"].ToString().Trim().Substring(0, dt["Valororcado"].ToString().Length - 2) + ";" +
                        //        dt["Codigoacaoportal"].ToString().Trim();

                        //if (swriterExporta != null)
                        //    swriterExporta.WriteLine(linha);

                    }

                    if (swriterExporta != null)
                        swriterExporta.Close();
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    grava(swriterLogProcesso, nomeArquivo + " -> Processado - com erros " + erro + "\n");
                }
                finally
                {

                    grava(swriterLogProcesso, "Término do processo de geração do arquivo Cronograma Orçamentário " + DateTime.Now.ToString());
                    if (swriterLogProcesso != null)
                        swriterLogProcesso.Close();

                    if (swriterExporta != null)
                        swriterExporta.Close();
                }

                if (erro == "")
                {

                    string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Arquivo gerado com sucesso.', 'sucesso', false, false, null);                                 
                                 </script>";
                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                    ASPxLabel41.Text = "Relação dos arquivos gerados.";

                }
                else
                {

                    string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. \n" + erro.Replace("'", "#") + "', 'erro', true, false, null);</script>";

                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                }
            }
        }

    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string podeExportar = hfGeral.Get("podeExportar").ToString();
        StreamWriter swriterExporta = null, swriterLogProcesso = null; ;
        if (podeExportar.Equals("N"))
        {

            string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Exportação cancelada pelo usuário.', 'atencao', true, false, null);</script>";

            ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
        }
        else
        {
            populaGrid();
            using (MemoryStream stream = new MemoryStream())
            {
                string nomeArquivo = "", erro = "", linha = "", nomeUnidade = "", raiz = "", raizLog = "";
                try
                {
                    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
                    raiz = getDiretorioIntegracaoZeus();
                    if (raiz == "")
                    {
                        throw new Exception(@"O parâmetro que identifica o Diretório onde serão armazenados os arquivos gerados para integração com o ZEUS não foi informado." + 
                                             @"\n\nVerifique por favor na tela de parâmetros o grupo Outras Configurações!");
                    }
                    raiz = raiz + "Exportar\\";

                    raizLog = raiz + "Processado\\";
                    //Se o diretório não existir...
                    if (!Directory.Exists(raizLog))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(raizLog);
                    }
                    //Se o diretório não existir...
                    if (!Directory.Exists(raiz))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(raiz);
                    }
                    string folder = @raizLog + dataHora; //nome do diretorio a ser criado
                    //Se o diretório não existir...
                    if (!Directory.Exists(folder))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folder);
                    }

                    swriterLogProcesso = new StreamWriter(@folder + "\\LogExportacao_" + dataHora + ".log");
                    grava(swriterLogProcesso, "Inicio do processo de exportação dos arquivos do ZEUS " + DateTime.Now.ToString());
                    for (int i = 0; i < gvDados.VisibleRowCount; i++)
                    {
                        DataRowView dt = (DataRowView)gvDados.GetRow(i);

                        if (nomeUnidade != dt["NomeUnidade"].ToString())
                        {
                            if (swriterExporta != null)
                                swriterExporta.Close();

                            nomeArquivo = @raiz + "F1-" + dt["NomeUnidade"].ToString() + "_" + dataHora + ".txt";
                            swriterExporta = new StreamWriter(nomeArquivo);
                            nomeUnidade = dt["NomeUnidade"].ToString();
                            ASPxMemo1.Text = ASPxMemo1.Text + (nomeArquivo + "\n");

                            grava(swriterLogProcesso, nomeArquivo + " -> Processado - OK\n");

                        }


                        linha = dt["CodigoUnidade"].ToString().Trim() + ";" +
                                dt["NomeUnidade"].ToString().Trim() + ";" +
                                dt["SiglaUnidade"].ToString().Trim() + ";" +
                                dt["NomeProjeto"].ToString().Trim() + ";" +
                                dt["Nomeacao"].ToString().Trim() + ";" +
                                dt["ContaOrcamentaria"].ToString().Trim() + ";" +
                                dt["Mes"].ToString().Trim() + ";" +
                                dt["Valororcado"].ToString().Trim().Substring(0, dt["Valororcado"].ToString().Length - 2) + ";" +
                                dt["Codigoacaoportal"].ToString().Trim();

                        if (swriterExporta != null)
                            swriterExporta.WriteLine(linha);

                    }

                    if (swriterExporta != null)
                        swriterExporta.Close();
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                    grava(swriterLogProcesso, nomeArquivo + " -> Processado - com erros " + erro + "\n");
                }
                finally
                {

                    grava(swriterLogProcesso, "Término do processo de exportação dos arquivos do ZEUS " + DateTime.Now.ToString());
                    if (swriterLogProcesso != null)
                        swriterLogProcesso.Close();

                    if (swriterExporta != null)
                        swriterExporta.Close();
                }

                if (erro == "")
                {

                    string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Arquivo gerado com sucesso.', 'sucesso', false, false, null);                                 
                                 </script>";
                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                    ASPxLabel41.Text = "Relação dos arquivos gerados.";

                }
                else
                {

                    string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. \n" + erro.Replace("'", "#") + "', 'erro', true, false, null);</script>";

                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                }
            }
        }

    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        string podeImportar = hfGeral.Get("podeImportar").ToString();
        if (podeImportar.Equals("N"))
        {

            string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Importação cancelada pelo usuário.', 'atencao', true, false, null);</script>";

            ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
        }
        else
        {
            string nomeArquivo = "", nomeArquivoLog = "", erro = "", nomeUnidade = "", raiz = "", raizLog = "", arquivoMover = "", nomeArquivoErro = "";
            StreamWriter swriterLogImporta = null, swriterLogProcesso = null;
            int linha = 0, qtdArquivos = 0;
            try
            {
                raiz = getDiretorioIntegracaoZeus();
                if (raiz == "")
                {
                    throw new Exception(@"O parâmetro que identifica o Diretório onde serão armazenados os arquivos gerados para integração com o ZEUS não foi informado." +
                                        @"\n\nVerifique por favor na tela de parâmetros o grupo Outras Configurações!");
                }
                raiz = raiz +  "Importar\\";

                raizLog = raiz + "Processado\\";
                //Se o diretório não existir...
                if (!Directory.Exists(raizLog))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(raizLog);
                }
                //Se o diretório não existir...
                if (!Directory.Exists(raiz))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(raiz);
                }
                string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
                string folder = @raizLog + dataHora; //nome do diretorio a ser criado
                //Se o diretório não existir...
                if (!Directory.Exists(folder))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(folder);
                }


                DirectoryInfo diretorio = new DirectoryInfo(@raiz);
                FileInfo[] arquivos = diretorio.GetFiles("F2*.txt");

                Array.Sort(arquivos, delegate(FileInfo a, FileInfo b) { return DateTime.Compare(a.CreationTime, b.CreationTime); });
                ASPxMemo1.Text = @"";
                foreach (FileInfo arquivo in arquivos)
                {
                    qtdArquivos += 1;
                    nomeArquivo = @raiz + arquivo.Name;
                    nomeArquivoLog = @folder + '\\' + arquivo.Name;
                    arquivoMover = @folder + '\\' + arquivo.Name.Replace("F2", "F3");
                    nomeArquivoErro = nomeArquivo.Replace("F2", "F9");
                    nomeArquivoErro = nomeArquivoErro.Replace(".txt", ".erro");
                    swriterLogProcesso = new StreamWriter(@folder + "\\LogImportacao_" + dataHora + ".log");
                    grava(swriterLogProcesso, "Inicio do processo de importação dos arquivos do ZEUS " + DateTime.Now.ToString());
                    if (nomeArquivo != nomeUnidade)
                    {
                        if (swriterLogImporta != null)
                            swriterLogImporta.Close();

                        nomeUnidade = nomeArquivo;
                        nomeArquivoLog = nomeArquivoLog.Replace(".txt", "_P_") + dataHora + ".log";
                        swriterLogImporta = new StreamWriter(nomeArquivoLog);
                    }


                    System.IO.StreamReader sr;
                    string linhaAtual, podeMoverArquivo;
                    podeMoverArquivo = "S";
                    // Verifica se o Arquivo não Existe
                    if (!System.IO.File.Exists(nomeArquivo))
                    {
                        throw (new System.IO.FileNotFoundException("Não foi Possível Localizar o Arquivo Especificado"));
                    }

                    string vCodigoCompara = "";
                    // Inicializa o StreamReader


                    using (sr = new System.IO.StreamReader(nomeArquivo))
                    {
                        linha = 1;

                        while (!sr.EndOfStream)
                        {
                            // Recupera a Linha
                            linhaAtual = sr.ReadLine();
                            // Processar a Linha AQUI!!!
                            string vCodigoProjeto, vCodigoCr;
                            if (linhaAtual.Split(';').Length != 10)
                            {
                                podeMoverArquivo = "N";
                                grava(swriterLogImporta, linhaAtual + " ==> linha [" + linha + "] não está no formato válido");
                            }
                            else
                            {

                                string[] result = linhaAtual.Split(';');
                                string[] result2 = result[8].Split('-');
                                if (result[8].Split('-').Length != 3)
                                {
                                    podeMoverArquivo = "N";
                                    grava(swriterLogImporta, linhaAtual + " ==> linha [" + linha + "] não está no formato válido");
                                }
                                else
                                {
                                    vCodigoProjeto = result2[0].ToString();
                                    vCodigoCr = result[9].ToString();
                                    if (vCodigoCompara != vCodigoProjeto + '-' + vCodigoCr)
                                    {
                                        try
                                        {
                                            if (cDados.incluirProjetoIntegraCR(vCodigoProjeto, vCodigoCr))
                                            {
                                                grava(swriterLogImporta, linhaAtual + " ==> linha [" + linha + "] OK");
                                            }
                                        }
                                        catch
                                        {
                                            podeMoverArquivo = "N";
                                            break;
                                        }
                                        vCodigoCompara = vCodigoProjeto + '-' + vCodigoCr;
                                    }
                                }
                            }
                            linha += 1;
                        }
                    }
                    if (podeMoverArquivo.Equals("S"))
                    {
                        grava(swriterLogProcesso, nomeArquivo + " - " + arquivo.CreationTime + "  -> Processado - OK\n");
                        ASPxMemo1.Text = ASPxMemo1.Text + (nomeArquivo + " - " + arquivo.CreationTime + "  -> Processado - OK\n");
                        moveArquivo(@nomeArquivo, @arquivoMover, @folder);
                    }
                    else
                    {
                        grava(swriterLogProcesso, nomeArquivo + " - " + arquivo.CreationTime + " -> Processado - Com Erros\n");
                        ASPxMemo1.Text = ASPxMemo1.Text + (nomeArquivo + " - " + arquivo.CreationTime + " -> Processado - Com Erros, verifique os erros no arquivo: " + nomeArquivoLog + "/n");
                        moveArquivo(nomeArquivo, nomeArquivoErro, @raiz);
                    }


                }


                if (erro == "")
                {
                    if (qtdArquivos > 0)
                    {
                        string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Foram processados [" + qtdArquivos + "] arquivos.', 'sucesso', true, false, null);</script>"; 
                        ClientScript.RegisterClientScriptBlock(GetType(), "client", script);

                    }
                    else
                    {
                        string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Não existem arquivos para serem importados!', 'atencao', true, false, null);</script>";
                        ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                    }

                    ASPxLabel41.Text = "Relação dos arquivos processados.";
                }
                else
                {
                    string erroFormatado = erro.Replace("'", " ");
                    erroFormatado = erroFormatado.Replace("\"", " ");
                    erroFormatado = erroFormatado.Replace("\r", " ");
                    erroFormatado = erroFormatado.Replace("\n", " ");
                    erroFormatado = @"Erro ao importar os dados. " + erroFormatado;
                    moveArquivo(nomeArquivo, nomeArquivoErro, @raiz);
                    string script = @"<script type='text/Javascript' language='Javascript'> window.top.mostraMensagem('" + erroFormatado + "', 'erro', true, false, null);</script>";
                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            finally
            {
                grava(swriterLogProcesso, "Término do processo de importação dos arquivos do ZEUS " + DateTime.Now.ToString());
                if (swriterLogProcesso != null)
                    swriterLogProcesso.Close();

                if (swriterLogImporta != null)
                    swriterLogImporta.Close();
            }

        }
    }




}
