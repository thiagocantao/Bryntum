using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.IO;

public partial class _Projetos_Administracao_cronogramaOrcamentario : System.Web.UI.Page
{
    dados cDados;
    ASPxGridView gvCampos_;
    DataSet dsValoresAcoes = new DataSet();

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string nomeProjeto;

    private string resolucaoCliente = "";

    int codigoProjeto = -1;
    public string somenteLeitura = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString());
        }
        AspxbuttonGerarArquivoCSV.Visible = false ;
        AspxbuttonGerarArquivoXLS.Visible = false;

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "mostraBotaoGeraCSV", "mostraBotaoGeraXLS");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            AspxbuttonGerarArquivoCSV.Visible = dsParametros.Tables[0].Rows[0]["mostraBotaoGeraCSV"].ToString() == "N" ? false : true;
            AspxbuttonGerarArquivoXLS.Visible = dsParametros.Tables[0].Rows[0]["mostraBotaoGeraXLS"].ToString() == "N" ? false : true;
        }


        carregaGrid();
    }



    private void carregaGrid()
    {
        gvDados.JSProperties["cp_Msg"] = "";
        dsValoresAcoes = cDados.getValoresAcoesProjeto(codigoProjeto, "");
        nomeProjeto = cDados.getNomeProjeto(codigoProjeto, "").ToString();

        DataSet ds = cDados.getCronogramaOrcamentario(codigoProjeto, "");

        gvDados.DataSource = ds;
        gvDados.DataBind();
        
    }

    public string getBotoesAtividade()
    {
        string descricao = "";

        bool podeIncluir = Eval("FonteRecurso").ToString() != "SR" && Eval("IndicaSemRecurso").ToString() != "S" && somenteLeitura != "S";

        descricao = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Conta"" onclick=""incluiLinha({0}, {1})"" style=""cursor: pointer;""/>", Eval("CodigoAtividade"), codigoProjeto) : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>");

        return descricao;
    }

    public string getDescricaoAcao()
    {
        string descricao = "";

        string fonteRecursos = "";

        string valorAcao = "";

        if (cDados != null && cDados.DataSetOk(dsValoresAcoes))
        {
            DataRow[] dr = dsValoresAcoes.Tables[0].Select("CodigoAcao = " + Eval("CodigoAcao"));

            if (dr.Length > 0)
                valorAcao = string.Format("{0:n2}", dr[0]["Valor"]);
        }

        switch (Eval("FonteRecurso").ToString())
        {
            case "SR": fonteRecursos = "Sem Recursos";
                break;
            case "FU": fonteRecursos = "Fonte de Recursos: FUNDECOP";
                break;
            case "RP": fonteRecursos = "Fonte de Recursos: Unidade Nacional";
                break;
            default: fonteRecursos = "";
                break;
        }

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid"" >
                                        <tr>
                                            <td>Ação: {0} - {1}</td>
                                            <td style='width:180px'>{2}</td>
                                            <td align='right' style='width:130px'>{3}</td>
                                        </tr>
                                    </table>", Eval("NumeroAcao"), Eval("NomeAcao"), fonteRecursos, valorAcao);

        return descricao;
    }

    public string getDescricaoAtividade()
    {
        string descricao = "";

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid"">
                                        <tr>
                                            <td>Atividade: {0}.{1} - {2}</td>
                                        </tr>
                                    </table>", Eval("NumeroAcao"), Eval("NumeroAtividade"), Eval("NomeAtividade"));

        return descricao;
    }

    public string getTotalProjeto()
    {
        string descricao = "";
        
        object totalUN = null;
        object totalFdc = null;
        object totalProjeto = null;

        if (cDados != null && cDados.DataSetOk(dsValoresAcoes))
        {
            totalUN = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "FonteRecurso = 'RP'");
            totalFdc = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "FonteRecurso = 'FU'");
            totalProjeto = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "");
        }

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid""  >
                                        <tr>
                                            <td>Projeto: {0}</td>
                                            <td align='right' style='width:200px'>Total UN: {1:n2}</td>
                                            <td align='right' style='width:200px'>Total FDC: {2:n2}</td>
                                            <td align='right' style='width:220px'>Total Projeto: {3:n2}</td>
                                        </tr>
                                    </table>", nomeProjeto, totalUN, totalFdc, totalProjeto);

        return descricao;
    }

    private void carregaGvContas(int codigoAtividade)
    {
        DataSet ds = cDados.getContasAcoesProjeto(codigoAtividade, codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvCampos_.DataSource = ds;
            gvCampos_.DataBind();
        }
    }

    protected void gvContas_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MemoriaCalculo" && e.CellValue != null && e.CellValue.ToString().Length > 60)
        {
            //string texto = e.CellValue.ToString().Replace("\"", "&quot;").Replace(Environment.NewLine, "<br>");

            //texto = texto.Replace("\n", "<BR>");

            //e.Cell.Attributes["onmouseover"] = "getToolTip(\"" + texto + "\")";// e.CellValue.ToString();
            ////e.Cell. e.CellValue.ToString();
            //e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";

            //e.Cell.ToolTip = e.CellValue.ToString();
            //e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";
            string texto = e.CellValue.ToString().Replace("\"", "&quot;").Replace(Environment.NewLine, "<br>");

            texto = texto.Replace("\n", "<BR>");

            e.Cell.Attributes["onmouseover"] = "getToolTip(\"" + texto + "\")";// e.CellValue.ToString();
            e.Cell.Attributes["onmouseout"] = "escondeToolTip()";// e.CellValue.ToString();
            e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";

        }
    }


    protected void gvDados_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            preparaGvContas(e.VisibleIndex);
        }
    }

    protected void gvDados_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        string possuiContas = gvDados.GetRowValues(e.VisibleIndex, "PossuiContas").ToString();
        string codigoAtividade = gvDados.GetRowValues(e.VisibleIndex, "CodigoAtividade").ToString();

        if (possuiContas == "S")
            e.ButtonState = GridViewDetailRowButtonState.Visible;
        else
        {
            gvDados.DetailRows.CollapseRow(e.VisibleIndex);
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
        }
    }

    public string getBotoesContas()
    {
        string imgEdicao = "", imgExclusao = "";

        if (somenteLeitura != "S")
            imgEdicao = string.Format(@"<img alt='Editar Conta' onclick='editaConta({0}, {1}, {2})' src='../../imagens/botoes/editarReg02.PNG' style='cursor:pointer' />", Eval("SeqPlanoContas"), Eval("CodigoAcao"), codigoProjeto);
        else
            imgEdicao = string.Format(@"<img src='../../imagens/botoes/editarRegDes.png' />");

        if (somenteLeitura != "S")
            imgExclusao = string.Format(@"<img alt='Excluir Conta' onclick='excluiConta({0}, {1})' src='../../imagens/botoes/excluirReg02.PNG' style='cursor:pointer' />", Eval("SeqPlanoContas"), Eval("CodigoAcao"));
        else
            imgExclusao = string.Format(@"<img src='../../imagens/botoes/excluirRegDes.png' />");

        return string.Format("<table><tr><td>{0}</td><td>{1}</td></tr></table>", imgEdicao, imgExclusao);
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() == "ATL")
        {
            gvDados.DetailRows.ExpandRowByKey(int.Parse(hfGeral.Get("CodigoAtividade").ToString()));   
            preparaGvContas(gvDados.FindVisibleIndexByKeyValue(int.Parse(hfGeral.Get("CodigoAtividade").ToString())));
        }
        else
            if (e.Parameters.ToString() != "")
            {
                string codigoConta = e.Parameters.ToString().Split(';')[0];
                string codigoAtividade = e.Parameters.ToString().Split(';')[1];

                bool resultado = cDados.excluiContaAtividade(int.Parse(codigoConta), int.Parse(codigoAtividade), codigoProjeto);

                if (resultado)
                {
                    
                    carregaGrid(); 
                    preparaGvContas(gvDados.FindVisibleIndexByKeyValue(int.Parse(hfGeral.Get("CodigoAtividade").ToString())));

                    if (gvCampos_ != null && gvCampos_.VisibleRowCount > 0)
                        gvDados.DetailRows.ExpandRowByKey(int.Parse(hfGeral.Get("CodigoAtividade").ToString()));
                    else
                    {
                        gvDados.DetailRows.ExpandRowByKey(-1);
                    }

                    gvDados.JSProperties["cp_Msg"] = "Conta removida do cronograma orçamentário com sucesso!";

                }
                else
                {
                    gvDados.JSProperties["cp_Msg"] = "Erro ao remover a conta do cronograma orçamentário!";
                }
            }
    }

    private void preparaGvContas(int indexMaster)
    {
        gvCampos_ = gvDados.FindDetailRowTemplateControl(indexMaster, "gvContas") as ASPxGridView;

        if (gvCampos_ != null)
        {
            cDados.aplicaEstiloVisual(gvCampos_);

            object objCodigo = gvCampos_.GetMasterRowKeyValue();

            if (objCodigo != null)
            {
                carregaGvContas(int.Parse(objCodigo.ToString()));
                gvCampos_.DataBind();
            }
        }
    }

    protected void btnGerarArquivocsv_Click(object sender, EventArgs e)
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
                    DateTime date1 = DateTime.Now;
                    DateTime dateOnly = date1.Date;

                    raiz = getDiretorioIntegracaoZeus() ;
                    if (raiz == "")
                    {
                        throw new Exception(@"O parâmetro que identifica o Diretório onde serão armazenados os arquivos gerados para integração com o ZEUS não foi informado." +
                                            @"\n\nVerifique por favor na tela de parâmetros o grupo Outras Configurações!");
                    }
                    raiz = raiz + "CronogramaOrcamentario\\";
                    string folder = @raiz + dateOnly.ToString("dd-MM-yyyy"); //nome do diretorio a ser criado
                    string folderlog = @folder + "\\log";
                    //Se o diretório não existir...
                    if (!Directory.Exists(folder))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folder);
                    }
                    if (!Directory.Exists(folderlog))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folderlog);
                    }

                    swriterLogProcesso = new StreamWriter(@folderlog + "\\CronogramaOrcamentario_" + dataHora + ".log");

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
                            swriterExporta = new System.IO.StreamWriter(nomeArquivo, false, System.Text.Encoding.GetEncoding("UTF-8"));
                            nomeProjeto = dt["NomeProjeto"].ToString();

                            grava(swriterLogProcesso, nomeArquivo + " -> Processado - OK\n");
                            decimal totalRP = Convert.IsDBNull(dt["totalRP"]) ? 0 : (decimal)dt["totalRP"];
                            decimal totalFU = Convert.IsDBNull(dt["totalFundecop"]) ? 0 : (decimal)dt["totalFundecop"];

                            decimal valorTotal = totalRP + totalFU;
                            linha = "SESCOOP - Cronograma Orçamentário;;;;;;;;;;;;\n" + ";;;;;;;;;;;;\n" +
                                //"Cronograma Orçamentário;;;;;;;;;;;;\n" +
                                //";;;;;;;;;;;;" +
                                    "Projeto: " + dt["NomeProjeto"].ToString().Trim() + ";;;;;Total UN: ;" + dt["totalRP"].ToString().Trim() + ";;Total FUNDECOOP: ;"
                                    + dt["totalFundecop"].ToString().Trim() + ";;Total GERAL: ;" +dt["totalProjeto"].ToString().Trim();//+ valorTotal; //
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (codigoAcao != dt["CodigoPai"].ToString() && dt["CodigoPai"].ToString() == dt["CodigoAcao"].ToString())
                        {
                            codigoAcao = dt["CodigoPai"].ToString();
                            linha = "  \nAÇÃO: " + dt["NomeAcao"].ToString().Trim() + ";;;;;;Fonte de recursos: " + dt["FonteRecurso"].ToString().Trim() +
                                    ";;;;;Total AÇÃO:  ;" + dt["totalAcao"].ToString().Trim() + "";
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (dt["NomeAtividade"].ToString() != "" && NomeAtividade != dt["NomeAtividade"].ToString() && dt["CodigoPai"].ToString() != dt["CodigoAcao"].ToString())
                        {
                            NomeAtividade = dt["NomeAtividade"].ToString();
                            linha = "    ATIVIDADE: " + dt["NomeAtividade"].ToString() + ";;;;;;;;;;;Total Atividade:  ;" + dt["totalAtividade"].ToString().Trim() + "";
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

    protected void btnGerarArquivoxls_Click(object sender, EventArgs e)
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
                    DateTime date1 =  DateTime.Now;
                    DateTime dateOnly = date1.Date;
                 

                    raiz = getDiretorioIntegracaoZeus() + "CronogramaOrcamentario\\";

                    string folder = @raiz + dateOnly.ToString("dd-MM-yyyy"); //nome do diretorio a ser criado
                    string folderlog = @folder + "\\log";
                    //Se o diretório não existir...
                    if (!Directory.Exists(folder))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folder);
                    }
                    if (!Directory.Exists(folderlog))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(folderlog);
                    }

                    swriterLogProcesso = new StreamWriter(@folderlog + "\\CronogramaOrcamentario_" + dataHora + ".log");
                    grava(swriterLogProcesso, "Inicio do processo de geração do arquivo Cronograma Orçamentário " + DateTime.Now.ToString());
                    for (int i = 0; i < gvDadosGeraArquivo.VisibleRowCount; i++)
                    {
                        DataRowView dt = (DataRowView)gvDadosGeraArquivo.GetRow(i);

                        if (nomeProjeto != dt["NomeProjeto"].ToString())
                        {
                            if (swriterExporta != null)
                                swriterExporta.Close();

                            nomeArquivo = @folder + "\\" + dt["NomeProjeto"].ToString() + "_" + dataHora + ".xls";
                            //System.Text.Encoding encoding = new System.Text.UTF32Encoding();
                            swriterExporta = new System.IO.StreamWriter(nomeArquivo, false, System.Text.Encoding.GetEncoding("UTF-8"));
                            nomeProjeto = dt["NomeProjeto"].ToString();

                            grava(swriterLogProcesso, nomeArquivo + " -> Processado - OK\n");
                            decimal totalRP = Convert.IsDBNull(dt["totalRP"]) ? 0 : (decimal)dt["totalRP"];
                            decimal totalFU = Convert.IsDBNull(dt["totalFundecop"]) ? 0 : (decimal)dt["totalFundecop"];

                            decimal valorTotal = totalRP + totalFU;

                            linha = string.Format(@"
                                                    <table border=""1"" >
                                                        <tr>
                                                        <td width=100; border=""""> </td> 
                                                        <td width=100; border=""""> </td><td width=100> </td><td width=100> </td><td width=100> </td>
                                                        <td width=100> </td><td width=100> </td><td width=100> </td><td width=100> </td>
                                                        <td width=100> </td><td width=100> </td><td width=100> </td><td width=100> </td>
                                                        </tr>
                                                        <tr><strong><td colspan=""13"";  align=""center""; bgcolor=""#B5B5B5""> SESCOOP - Cronograma Orçamentário </td></strong></tr>
                                                        <tr>
                                                          <td colspan=""7""; ></td>
                                                          <td colspan=""6""; align=""center"";bgcolor=""#CFCFCF"" >Totais</td>
                                                      </tr>
                                                        <tr>
                                                          <td align=""right"";bgcolor=""#CFCFCF"">Projeto:</td><td colspan=""6""; >{0}</td>
                                                          <td align=""right"";bgcolor=""#CFCFCF"" >UN:</td><td>{1} </td>
                                                          <td align=""right"";bgcolor=""#CFCFCF"" >FUNDECOOP:</td><td>{2} </td>
                                                          <td align=""right"";bgcolor=""#CFCFCF"" >GERAL:</td><td>{3}</td> 
                                                      </tr> ", 
                                                      dt["NomeProjeto"].ToString().Trim() , 
                                                      dt["totalRP"].ToString().Trim() ,
                                                      dt["totalFundecop"].ToString().Trim(),
                                                      dt["totalProjeto"].ToString().Trim()
                                                      );

                            if (swriterExporta != null)
                            {
                                linha = @"<style>td { mso-number-format:""\#\,\#\#0\.00"";} </style>" + linha;
                                swriterExporta.WriteLine(linha);
                            }

                        }

                        if (codigoAcao != dt["CodigoPai"].ToString() && dt["CodigoPai"].ToString() == dt["CodigoAcao"].ToString())
                        {
                            codigoAcao = dt["CodigoPai"].ToString();
                            linha = string.Format(@"<tr></tr><tr>
                                                     <td colspan=""7"";bgcolor=""#B5B5B5"">AÇÃO:&nbsp;&nbsp;{0}</td>
                                                     <td colspan=""2""; align=""right""; bgcolor=""#B5B5B5"">Fonte de recursos:&nbsp;&nbsp;{1} </td>
                                                     <td colspan=""2""; align=""right""; bgcolor=""#B5B5B5"">Total AÇÃO:</td>
                                                     <td colspan=""2""; align=""right""; bgcolor=""#B5B5B5"">{2}</td> </tr>    
                                                      ",                                
                                                    dt["NomeAcao"].ToString().Trim(),
                                                    dt["FonteRecurso"].ToString().Trim(),
                                                    dt["totalAcao"].ToString().Trim() );
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (dt["NomeAtividade"].ToString() != "" && NomeAtividade != dt["NomeAtividade"].ToString() && dt["CodigoPai"].ToString() != dt["CodigoAcao"].ToString())
                        {
                            NomeAtividade = dt["NomeAtividade"].ToString();
                            linha = string.Format(@"<tr></tr><tr><td colspan=""9""; bgcolor=""#CFCFCF"">&nbsp;&nbsp;&nbsp;ATIVIDADE:&nbsp;&nbsp;{0} </td>
                                                     <td colspan=""2""; align=""right""; bgcolor=""#CFCFCF"">Total Atividade:</td>
                                                     <td colspan=""2""; align=""right""; bgcolor=""#CFCFCF"">{1} </td></tr>
                                                      ",
                                                    dt["NomeAtividade"].ToString(),
                                                    dt["totalAtividade"].ToString().Trim() );
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }

                        if (dt["ContaOrcamentaria"].ToString() != "")
                        {
                            linha = @"<tr><td colspan=""3"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Conta Orçamentária  </td>
                                          <td> Quantidade </td><td>Valor Unitário</td><td>Valor Total</td><td colspan=""7"">Memória de Cálculo </td>";
                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);
                            
                            linha = string.Format(@"<tr><td colspan=""3"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{12}
                                     <td> {13} </td><td>{14}</td><td>{15}</td><td colspan=""7"">{16}</td></tr> 
                                     <tr align=""center""><td rowspan=""2""></td><td>Janeiro</td><td>Fevereiro</td><td>Março</td><td>Abril</td><td>Maio</td><td>Junho</td><td>Julho</td>
                                                  <td>Agosto</td><td>Setembro</td><td>Outubro</td><td>Novembro</td><td>Dezembro</td></tr><tr align=""right""> 
                                                   <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>
                                                   {8}</td><td>{9}</td><td>{10}</td><td>{11}</td></tr><tr></tr>",
                                dt["Janeiro"].ToString().Trim()+"",
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
                                dt["Dezembro"].ToString(),

                                dt["ContaOrcamentaria"].ToString() , //12
                                dt["Quantidade"].ToString() , 
                                dt["ValorUnitario"].ToString() ,
                                dt["ValorTotal"].ToString() ,
                                dt["MemoriaCalculo"].ToString() 
                                );

                            if (swriterExporta != null)
                                swriterExporta.WriteLine(linha);

                        }
                    }

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
                    {
                        swriterExporta.WriteLine("</table>");
                        swriterExporta.Close();
                    }
                }

                if (erro == "")
                {

                    string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Arquivo gerado com sucesso.', 'sucesso', false, false, null);                                 
                                 </script>";
                    ClientScript.RegisterClientScriptBlock(GetType(), "client", script);

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


    protected void grava(StreamWriter s, string linha)
    {
        if (s != null)
            s.WriteLine(linha);
    }

    protected string getDiretorioIntegracaoZeus()
    {
        string raiz = "";
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "diretorioIntegracaoZeus");
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


    private void populaGridGeraArquivo()
    {
        dsValoresAcoes = cDados.getValoresAcoesProjeto(codigoProjeto, "");
        nomeProjeto = cDados.getNomeProjeto(codigoProjeto, "").ToString();

        
        DataSet ds = cDados.getDadosGeraArquivoCronogramaOrcamentario(codigoProjeto, "");
        if (cDados.DataSetOk(ds))
        {
            gvDadosGeraArquivo.DataSource = ds.Tables[0];
            gvDadosGeraArquivo.DataBind();
        }
    }

}