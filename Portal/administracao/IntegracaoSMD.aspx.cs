using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Hosting;
using DevExpress.Web;
using System.Text;

public partial class administracao_IntegracaoSMD : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    //array utilizado somente para nomear a tabela criada do SMD no evento do botão btnLerExcel_Click
    string[] meses = new string[12] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

    public string nomeTabelaTemp;
    public int ano_global;
    public int mes_global;
    public string codControleCarga;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        DataSet dsdata = cDados.getDataSet("select getdate()");
        if (dsdata != null && dsdata.Tables[0] != null)
        {
            DateTime data = DateTime.Parse(dsdata.Tables[0].Rows[0][0].ToString());
            ano_global = data.Year;
            mes_global = data.Month;
        }

        nomeTabelaTemp = "";

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        cDados.aplicaEstiloVisual(Page);

        //gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        btnImportar.Paddings.PaddingTop = 1;
        btnImportar.Paddings.PaddingBottom = 1;
        btnSalvar.Paddings.PaddingTop = 1;
        btnSalvar.Paddings.PaddingBottom = 1;

        if (!IsPostBack)
            txtAno.Number = DateTime.Now.Year;

        carregaDados();
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 305;
    }

    #region Importação CSV

    public void SaveStreamToFile(Stream stream, string filename)
    {
        using (Stream destination = File.Create(filename))
            Write(stream, destination);
    }

    public void Write(Stream from, Stream to)
    {
        for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
            to.WriteByte((byte)a);
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        //gvDados.Columns.Clear();

        string mensagem = preparaImportacao();

        if (mensagem != "")
        {
            gvDados.Columns.Clear();
            gvDados.DataSource = null;
            gvDados.DataBind();
            btnSalvar.ClientEnabled = false;

            string script = (mensagem == "EF" ? "" : "window.top.mostraMensagem('" + mensagem + "', 'atencao', true, false, null);") + "lpCarregando.Hide();";

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "arquivo",
                script,
                true);
        }
    }

    private DataTable GetDataTabletFromCSVFile(string csv_file_path, ref bool verificaColunas, bool primeiraCargaCSV)
    {
        DataTable csvData = new DataTable();
        DataSet ds = new DataSet();

        if (primeiraCargaCSV)
        {
            ds = cDados.getDataSet(@"select ColunaDimensaoPlanilha
                                        from [SMD_Dados_Dimensoes_Origem]
                                        union 
                                        select ColunaIndicador
                                        from [SMD_Dados_Principais_Origem]
                                        union 
                                        select ColunaTempo
                                        from [SMD_Dados_Principais_Origem]
                                        union 
                                        select ColunaUnidadeNegocio
                                        from [SMD_Dados_Principais_Origem]
                                        union 
                                        select ColunaQuantidade
                                        from [SMD_Dados_Principais_Origem]
                                        union 
                                        select ColunaControle
                                        from [SMD_Dados_Principais_Origem]
                                          ");

            if (cDados.DataTableOk(ds.Tables[0]))
                verificaColunas = true;
        }

        string colunasInsert = "";
            StringBuilder comandoInsert = new StringBuilder();

        if (File.Exists(csv_file_path))
        {
            StreamReader readData = new StreamReader(csv_file_path, System.Text.Encoding.GetEncoding("UTF-8"), true);
            string readLine = String.Empty;

            bool planilhaVazia = true, encontrouCabecalho = false;
            int count = 0;
            do
            {
                 readLine = readData.ReadLine();

                 if (readLine.Replace(";", "") != "")
                 {
                     planilhaVazia = false;
                     encontrouCabecalho = true;
                 }
                 else if (count == 100)
                     encontrouCabecalho = true;

                 count++;

            } while (encontrouCabecalho == false);


            if (planilhaVazia)
                return csvData;

            int qtdColunas = 0;
            string[] newValue2 = readLine.Split(new char[] { ';' });

            bool achoColuna = false;
                        
            if (primeiraCargaCSV)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    achoColuna = false;

                    for (int i = 0; i < newValue2.Length; i++)
                    {
                        if (dr["ColunaDimensaoPlanilha"].ToString() == newValue2[i].TrimEnd().TrimStart())
                        {
                            achoColuna = true;
                            break;
                        }
                    }

                    if (!achoColuna)
                    {
                        verificaColunas = false;
                        return csvData;
                    }
                }
            }

            for (int i = 0; i < newValue2.Length; i++)
            {

                csvData.Columns.Add(newValue2[i].TrimEnd().TrimStart(), typeof(string));
                qtdColunas++;
            }

            do
            {
                readLine = readData.ReadLine();
                if (readLine == null)
                {
                    break;
                }
                else
                {
                    string[] newValue = readLine.Split(new char[] { ';' });

                    try
                    {
                        DataRow myDataRow = csvData.NewRow();
                        colunasInsert = "";
                        for (int j = 0; j < qtdColunas; j++)
                        {
                            myDataRow[j] = newValue[j] + "";
                            colunasInsert += ",'" + newValue[j].Replace("'", "''") + "'";
                        }

                        if (colunasInsert != "")
                            comandoInsert.AppendLine("INSERT INTO @nomeTabelaTemp VALUES(" + colunasInsert.Substring(1) + ", null, null)");

                        csvData.Rows.Add(myDataRow);
                    }
                    catch { }
                }


            } while (true);

            if (primeiraCargaCSV)
            {
                hfComandoSQL.Set("InsertValues", comandoInsert.ToString());
            }
        }
        else
        {
            verificaColunas = false;
        }

        return csvData;
    }

    private string preparaImportacao()
    {
        string sqlAux;
        string anoMes = ano_global + ((mes_global.ToString().Length > 1 ? "" : "0") + mes_global.ToString());
        
        // VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
        if (fluArquivo.UploadedFiles.Length > 0)
        {
            string nomeNovoAnexo = Path.GetFileName(fluArquivo.UploadedFiles[0].FileName);
            int tamanhoImagem = (int)fluArquivo.UploadedFiles[0].ContentLength;

            // VERIFICA A EXTENSÃO DO ARQUIVO.
            string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
            if (extensao != ".csv")
                return "EF";

            //RECEBE O ARQUIVO COLOCANDO-O NA MEMÓRIA
            Stream imagem = fluArquivo.UploadedFiles[0].FileContent;

            string nomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios\\arquivoImportacaoSMD_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + extensao;

            SaveStreamToFile(imagem, nomeArquivo);
            //gvDados.Columns.Clear();

            bool verificaColunas = true;
            
            DataTable dtdados = GetDataTabletFromCSVFile(nomeArquivo, ref verificaColunas, true);

            hfComandoSQL.Set("NomeArquivo", nomeArquivo);

            if (verificaColunas == false)
                return "Arquivo .csv no formato incorreto!";

            bool usaCabecalhosParaCriarTabelasSistema = true;

            if (dtdados.Rows[0][0].ToString().TrimStart().TrimEnd().Contains("Cod"))
            {
                usaCabecalhosParaCriarTabelasSistema = false;
            }

            gvDados.DataSource = dtdados;
            gvDados.DataBind();

            // Insert na tabela de controle das cargas -------------------------------------------------------------------------------------------
            hfComandoSQL.Set("Comando1",  string.Format(@"DECLARE @codRegInserido  int;

                                                       INSERT INTO ControleCargaPlanilhaIndicadores
                                                        ( AnoMesRef, NomeArquivoPlanilha, NomeTabelaDadosBrutos, QtdLinhasPlanilha)
                                                       VALUES
                                                        ( {0}      ,'{1}'               ,                 '{2}', {3});
                                                        
                                                       SET @codRegInserido = SCOPE_IDENTITY();
                                                                                                            
                                                        INSERT INTO SMD_Import_Dados_Principais  
	                                                        SELECT @codRegInserido, {0}, ColunaIndicador, ColunaTempo, ColunaUnidadeNegocio, ColunaQuantidade, ColunaControle 
		                                                        FROM SMD_Dados_Principais_Origem 


                                                        INSERT INTO SMD_Import_Dados_Dimensoes   
	                                                        SELECT @codRegInserido, {0}, ColunaDimensaoPlanilha, NomeColunaTabelaFato
		                                                        FROM SMD_Dados_Dimensoes_Origem  
                                                        
                                                       SELECT @codRegInserido;
                                                      ", anoMes, fluArquivo.UploadedFiles[0].FileName, nomeTabelaTemp, dtdados.Rows.Count
                                                    ));

            // Atualiza na [ControleCargaPlanilhaIndicadores] o nome da tabela com os dados brutos da planilha
            hfComandoSQL.Set("Comando2",  string.Format(@"UPDATE ControleCargaPlanilhaIndicadores
                                                                SET nomeTabelaDadosBrutos = '@nomeTabelaTemp'
                                                                WHERE codigoCarga = @CodigoCargaParam"));
            sqlAux = "";

            if (usaCabecalhosParaCriarTabelasSistema == true)
            {
                foreach (GridViewDataTextColumn col in gvDados.Columns)
                {
                    sqlAux = sqlAux + "[" + col.FieldName.TrimEnd().TrimStart() + "]   " + "VARCHAR(512)," + Environment.NewLine;
                }

            }
            else
            {
                for (int i = 0; i < dtdados.Columns.Count; i++)
                {
                    string strAux = "F" + (i + 1);
                    sqlAux = sqlAux + "[" + ((dtdados.Rows[0][i].ToString().TrimEnd().TrimStart().Length) > 0 ? dtdados.Rows[0][i].ToString().TrimEnd().TrimStart() : strAux) + "]   " + "VARCHAR(512)," + Environment.NewLine;
                }

            }

            hfComandoSQL.Set("Comando3", sqlAux);
            
            btnSalvar.ClientEnabled = true;
            

            return "";
        }
        else
        {
            return "O arquivo não foi informado";
        }
    }

    #endregion

    private void salvaInformacoes()
    {
        try
        {

            string sqlCreateTable, sqlAux;
            string anoMes = ano_global + ((mes_global.ToString().Length > 1 ? "" : "0") + mes_global.ToString());

            // Insert na tabela de controle das cargas -------------------------------------------------------------------------------------------
            string cmdInsertControle = hfComandoSQL.Get("Comando1").ToString();

            DataSet dsTabControle = cDados.getDataSet(cmdInsertControle);
            codControleCarga = dsTabControle.Tables[0].Rows[0][0].ToString();
            // Monta o nome da tabela que conterá os dados brutos da planilha a ser importada,
            // concatenando o CodigoCarga (coluna autoincrementado da tabela [ControleCargaPlanilhaIndicadores]).
            nomeTabelaTemp = "SMD_PLAN_" + ano_global + meses[mes_global - 1];
            nomeTabelaTemp = nomeTabelaTemp + "_" + codControleCarga;

            // Atualiza na [ControleCargaPlanilhaIndicadores] o nome da tabela com os dados brutos da planilha
            string cmdUpdateAjustaNomeTabela = hfComandoSQL.Get("Comando2").ToString().Replace("@CodigoCargaParam", codControleCarga).Replace("@nomeTabelaTemp", nomeTabelaTemp);


            int regAf = 0;

            cDados.execSQL(cmdUpdateAjustaNomeTabela, ref regAf);

            // ------------------------------------------------------------------------------------------------------------------------------------

            sqlCreateTable = @"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[dbo].[" + nomeTabelaTemp + "]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1) " + Environment.NewLine +
                               "DROP TABLE [dbo].[" + nomeTabelaTemp + "]; " + Environment.NewLine +
                               " CREATE TABLE [" + nomeTabelaTemp + "] ( " + Environment.NewLine;
            sqlAux = hfComandoSQL.Get("Comando3").ToString();


            sqlCreateTable = sqlCreateTable + sqlAux + " DataHoraCarga  DATETIME, NumeroControleInterno     INT ) ";

            cDados.execSQL(sqlCreateTable, ref regAf);

            cDados.execSQL(hfComandoSQL.Get("InsertValues").ToString().Replace("@nomeTabelaTemp", nomeTabelaTemp), ref regAf);

            cDados.execSQL(@"EXEC dbo.[p_SMD_SalvaDadosPlanilhaTabelaFato] " + anoMes + ", " + txtAno.Number + ", " + codControleCarga, ref regAf);

            callback.JSProperties["cp_MSG"] = "Informações salvas com sucesso!";
            callback.JSProperties["cp_Status"] = "1";
        }
        catch (Exception ex)
        {
            callback.JSProperties["cp_MSG"] = "Erro ao salvar as informações! " + ex.Message;
            callback.JSProperties["cp_Status"] = "0";
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        salvaInformacoes();
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        bool verificaColunas = true;

        DataTable dtdados = GetDataTabletFromCSVFile(hfComandoSQL.Get("NomeArquivo").ToString(), ref verificaColunas, false);
               
        gvDados.DataSource = dtdados;
        gvDados.DataBind();
    }

    private void carregaDados()
    {
        string comandoSQL = @"
                   SELECT ccp.CodigoCarga, 
                    (SELECT TOP 1 TipoControle FROM SMD_TBF_Indicadores i where i.codigoCarga = ccp.codigoCarga) as Competencia,
                    ccp.AnoMesRef, ccp.NomeArquivoPlanilha, ccp.DataHoraInicioCarga, ccp.QtdLinhasPlanilha, ccp.QtdLinhasImportadas
                    FROM ControleCargaPlanilhaIndicadores ccp
                   ORDER BY ccp.CodigoCarga DESC";

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvCargas.DataSource = ds;
        gvCargas.DataBind();
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string comandoSQL = string.Format(@"EXEC {0}.{1}.p_SMD_DesfazCarga {2}", cDados.getDbName(), cDados.getDbOwner(), e.Keys[0]);

        try
        {
            int regAf = 0;
            cDados.execSQL(comandoSQL, ref regAf);
            e.Cancel = true;
            carregaDados();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao desfazer a carga! " + Environment.NewLine + ex.Message);
        }
    }
}