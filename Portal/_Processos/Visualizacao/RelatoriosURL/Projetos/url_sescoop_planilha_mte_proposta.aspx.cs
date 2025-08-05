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
using DevExpress.Web;
using System.Web.Hosting;
using System.IO;
using DevExpress.XtraReports.Web.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Linq;
using DevExpress.Spreadsheet;
using DevExpress.Web.ASPxSpreadsheet;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_sescoop_planilha_mte_proposta : System.Web.UI.Page
{
    dados cDados;
    string montaNomeArquivo = "";
    string montaNomeImagemParametro;
    string dataImpressao = "";
    public string alturaDivGrid = "";
    public string larguraDivGrid = "";
    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioResponsavel = 0;
    int codUnidade = -1;

    DataRow[] receitas;
    DataRow[] despesas;
    DataRow[] despesas122;
    DataRow[] despesas333;
    DataRow[] despesas444;

    string erro = string.Empty;
    int ano = 0;
    
    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //commit para marcelo adquirir a tela
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

        codUnidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsCallback)
            {
                CarregaComboAnos();
                //CarregaComboPlanilhas();

                if ( Request.QueryString.Count > 0 )
                {
                    Carregacomboperiodo();
                    this.hPlanilha.Value = "EXE";
                    lblDescricao.Text = "Período:";
                    
                }
                else
                {
                    Carregacomboetapa();
                    this.hPlanilha.Value = "ORC";
                    lblDescricao.Text = "Etapa de Previsão Orçamentária:";
                    AspSpreadsheet.Document.Options.Save.CurrentFileName = "Plano Orçamentário";
                }
            }

        }

        cDados.aplicaEstiloVisual(Page);
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
        }

        defineAlturaTela();

    }

    private void Carregacomboetapa()
    {
        try
        {
            DataSet dsEtapas = cDados.getStatusPrevisaoFluxoCaixaMTE();

            if (cDados.DataSetOk(dsEtapas))
            {
                cmbEtapas.DataSource = dsEtapas;
                cmbEtapas.TextField = "Descricao";
                cmbEtapas.ValueField = "Codigo";
                cmbEtapas.DataBind();

                cmbPeriodo.Visible = false;
                cmbEtapas.Visible = true;
            }
        }
        catch (Exception ex)
        {
            erro = "Erro ao carregar combo com os anos. Erro: " + ex.Message;
        }
        
    }

    private void Carregacomboperiodo()
    {
         
        cmbPeriodo.Visible = true;
        cmbEtapas.Visible = false;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 230);

        AspSpreadsheet.Height = alturaPrincipal;
    }

    private bool CarregaArquivoPlanilha(string planilha)
    {
        try
        {
            byte[] documentContentAsByteArray = GetByteArrayFromCustomDocumentStorage(planilha);
            var uniqueDocumentId = Guid.NewGuid().ToString();
            if (documentContentAsByteArray != null)
            {
                AspSpreadsheet.Open(uniqueDocumentId, DocumentFormat.Xlsx, () => documentContentAsByteArray);
                return true;
            }
        }
        catch (Exception ex)
        {
            erro = "Falha ao abrir o arquivo de template. Erro: " + ex.Message;
            return false;
        }
        return false;
    }

    private byte[] GetByteArrayFromCustomDocumentStorage(string planilha)
    {
        try
        {
            DataSet dsx = cDados.getInformacoesPlanilhaMte(codUnidade, codigoUsuarioResponsavel, planilha);
            if (cDados.DataSetOk(dsx) && cDados.DataTableOk(dsx.Tables[0]))
            {
                string CodigoAnexo = dsx.Tables[0].Rows[0]["CodigoAnexo"].ToString();
                int? codigoSequencialAnexo = Convert.ToInt32(dsx.Tables[0].Rows[0]["CodigoSequencialAnexo"]);
                string IndicaDestinoGravacaoAnexo = dsx.Tables[0].Rows[0]["IndicaDestinoGravacaoAnexo"].ToString();

                string NomeArquivo = "";
                byte[] arraybinary = cDados.getConteudoAnexo(int.Parse(CodigoAnexo), codigoSequencialAnexo, ref NomeArquivo, IndicaDestinoGravacaoAnexo);
                return arraybinary;
            }

            return null;

        }
        catch (Exception ex)
        {
            erro = "Falha na leitura do arquivo. Erro: " + ex.Message;
            return null;
        }
    }

    private void AtualizaDadosPlanilha(string planilha)
    {
        if (!AtualizaReceitasDepesas(AspSpreadsheet.Document, "Anexo_I_-_RECEITAS", planilha)) return;
        if (!AtualizaReceitasDepesas(AspSpreadsheet.Document, "Anexo_I_-_DESPESAS", planilha)) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal122")) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas122")) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos122")) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal333")) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas333")) return;
        if (!AtualizaAnexo(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos333")) return;

        if (planilha == "EXE")
        {
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal122Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal122Executado", planilha, "executado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas122Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas122Executado", planilha, "executado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos122Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos122Executado", planilha, "executado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal333Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "pessoal333Executado", planilha, "executado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas333Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "outrasDespesas333Executado", planilha, "executado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos333Reformulado", planilha, "reformulado")) return;
            if (!AtualizaAnexoReformuladoOuExecutado(AspSpreadsheet.Document, "Anexo_III_-_DET", "investimentos333Executado", planilha, "executado")) return;

            AtualizaAnoPlanilhaExecutadoEFormulado(AspSpreadsheet.Document);
            AtualizaReceitasIncorporadasPlanilhaExecutada(AspSpreadsheet.Document);
        }
        else
        {
            AtualizaAnoPlanilhaOrcamentaria(AspSpreadsheet.Document);
            AtualizaReceitasIncorporadasPlanilhaOrcamentaria(AspSpreadsheet.Document);
        }
    }

    private void AtualizaReceitasIncorporadasPlanilhaOrcamentaria(IWorkbook document)
    {
        try
        {
            var worksheet = document.Worksheets["Anexo_I_-_RECEITAS"];

            //Atualiza o receitas incorporadas planilha orçamentária
            var celula = worksheet.Range["ReceitasIncorporadas"];
            var nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(despesas444[0]["VALOR_0"].ToString());


            celula = worksheet.Range["IdentificacaoFiltroAdicionalDados"];
            nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(cmbEtapas.SelectedItem.Text);

        }
        catch (Exception ex)
        {
            erro += "Erro ao atualizar ano na planilha. Erro:" + ex.Message;
        }
    }

    private void AtualizaReceitasIncorporadasPlanilhaExecutada(IWorkbook document)
    {
        try
        {
            var worksheet = document.Worksheets["Anexo_I_-_RECEITAS"];
            
            //Atualiza o receitas incorporadas planilha executada
            var celula = worksheet.Range["ReceitasIncorporadasOriginal"];
            var nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(despesas444[0]["VALOR_0"].ToString());

            celula = worksheet.Range["ReceitasIncorporadasReformulado"];
            nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(despesas444[0]["VALOR_1"].ToString());

            celula = worksheet.Range["ReceitasIncorporadasExecutado"];
            nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(despesas444[0]["VALOR_2"].ToString());

            celula = worksheet.Range["IdentificacaoFiltroAdicionalDados"];
            nomeCelula = celula.GetReferenceA1();
            if (celula != null) worksheet.Cells[nomeCelula].SetValueFromText(cmbPeriodo.SelectedItem.Text);


        }
        catch (Exception ex)
        {
            erro += "Erro ao atualizar ano na planilha. Erro:" + ex.Message;
        }
    }

    private bool AtualizaReceitasDepesas(IWorkbook document, String item, string planilha)
    {
        try
        {
            var worksheet = GetWorksheet(document, item);
            if (worksheet == null)
            {
                erro = "Erro no template da planilha. Item " + item + " não encontrado!";
                return false;
            }

            string colunaInicioCodigos = string.Empty;
            string colunaInicioValores = string.Empty;
            string linhaInicialValores = string.Empty;
            string colunaInicioValoresReformulados = string.Empty;
            string colunaInicioValoresExecutados = string.Empty;
            //string celulaReceitasIncorporadasOriginal = string.Empty;
            //string celulaReceitasIncorporadasReformulado = string.Empty;
            //string celulaReceitasIncorporadasExecutado = string.Empty;

            if (item == "Anexo_I_-_RECEITAS")
            {
                //Captura os marcadores do sheet Receitas
                var celulaInicioCodigos = worksheet.Range["codigoReceita"];
                string[] aux = celulaInicioCodigos.GetReferenceA1().Split('$');
                colunaInicioCodigos = aux[1];

                //Captura os marcadores do sheet Receitas
                var celulaInicioValores = worksheet.Range["valorReceita"];
                string[] aux2 = celulaInicioValores.GetReferenceA1().Split('$');
                colunaInicioValores = aux2[1];
                linhaInicialValores = aux2[2];

                if (planilha == "EXE")
                {
                    //Captura novos marcadores reformulados e executados
                    var celulaInicioValoresReformulados = worksheet.Range["valorReceitaReformulado"];
                    string[] aux3 = celulaInicioValoresReformulados.GetReferenceA1().Split('$');
                    colunaInicioValoresReformulados = aux3[1];

                    var celulaInicioValoresExecutados = worksheet.Range["valorReceitaExecutado"];
                    string[] aux4 = celulaInicioValoresExecutados.GetReferenceA1().Split('$');
                    colunaInicioValoresExecutados = aux4[1];

                    //var celularReceitasIncorporadasOriginal = worksheet.Range["ReceitasIncorporadasOriginal"];
                    //string[] aux5 = celularReceitasIncorporadasOriginal.GetReferenceA1().Split('$');
                    //celulaReceitasIncorporadasOriginal = aux5[1];

                    //var refCelulaReceitasIncorporadasReformulado = worksheet.Range["ReceitasIncorporadasReformulado"];
                    //string[] aux6 = refCelulaReceitasIncorporadasReformulado.GetReferenceA1().Split('$');
                    //celulaReceitasIncorporadasReformulado = aux6[1];

                    //var celularReceitasIncorporadasOriginal = worksheet.Range["ReceitasIncorporadasExecutado"];
                    //string[] aux5 = celularReceitasIncorporadasOriginal.GetReferenceA1().Split('$');
                    //celulaReceitasIncorporadasExecutado = aux5[1];

                }
            }

            if (item == "Anexo_I_-_DESPESAS")
            {
                //Captura os marcadores do sheet Despesas
                var celulaInicioCodigos = worksheet.Range["codigoDespesa"];
                string[] aux = celulaInicioCodigos.GetReferenceA1().Split('$');
                colunaInicioCodigos = aux[1];

                //Captura os marcadores do sheet Despesas
                var celulaInicioValores = worksheet.Range["valorDespesa"];
                string[] aux2 = celulaInicioValores.GetReferenceA1().Split('$');
                colunaInicioValores = aux2[1];
                linhaInicialValores = aux2[2];

                if (planilha == "EXE")
                {
                    //Captura novos marcadores reformulados e executados
                    var celulaInicioValoresReformulados = worksheet.Range["valorDespesaReformulado"];
                    string[] aux3 = celulaInicioValoresReformulados.GetReferenceA1().Split('$');
                    colunaInicioValoresReformulados = aux3[1];

                    var celulaInicioValoresExecutados = worksheet.Range["valorDespesaExecutado"];
                    string[] aux4 = celulaInicioValoresExecutados.GetReferenceA1().Split('$');
                    colunaInicioValoresExecutados = aux4[1];
                }
            }

            int contador = Convert.ToInt32(linhaInicialValores);
            int contadorAux = 1;

            //Percorre as linhas
            foreach (var linha in worksheet.Columns[colunaInicioCodigos])
            {
                if (linha.Value.TextValue == "TOTAL DE RECEITAS" || linha.Value.TextValue == "TOTAL DE DESPESAS")
                    break;

                if (contadorAux == contador)
                {
                    foreach (var coluna in worksheet.Rows[contador.ToString()])
                    {
                        var nomeCelula = coluna.GetReferenceA1();

                        if (nomeCelula.Contains(colunaInicioValores))
                        {
                            var valorCelula = coluna.Value.IsNumeric ? coluna.Value.NumericValue.ToString() : coluna.Value.TextValue;

                            if (!coluna.HasFormula && valorCelula != null && linha.Value != null)
                            {
                                var novoValor = BuscaValorPeloCodigo(linha.Value.ToString(), item);
                                if (novoValor != null)
                                    worksheet.Cells[nomeCelula].SetValueFromText(novoValor);
                            }
                        }
                        if (planilha == "EXE")
                        {
                            if (nomeCelula.Contains(colunaInicioValoresReformulados))
                            {
                                var valorCelula = coluna.Value.IsNumeric ? coluna.Value.NumericValue.ToString() : coluna.Value.TextValue;

                                if (!coluna.HasFormula && valorCelula != null && linha.Value != null)
                                {
                                    var novoValor = BuscaValorReformuladoOuExecutadoPeloCodigo(linha.Value.ToString(), item, "reformulado");
                                    if (novoValor != null)
                                        worksheet.Cells[nomeCelula].SetValueFromText(novoValor);
                                }
                            }

                            if (nomeCelula.Contains(colunaInicioValoresExecutados))
                            {
                                var valorCelula = coluna.Value.IsNumeric ? coluna.Value.NumericValue.ToString() : coluna.Value.TextValue;

                                if (!coluna.HasFormula && valorCelula != null && linha.Value != null)
                                {
                                    var novoValor = BuscaValorReformuladoOuExecutadoPeloCodigo(linha.Value.ToString(), item, "executado");
                                    if (novoValor != null)
                                        worksheet.Cells[nomeCelula].SetValueFromText(novoValor);
                                }
                                break;
                            }
                        }
                    }
                    contador++;
                    contadorAux = contador;
                }
                else { contadorAux++; }
            }
            document.History.Clear();

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }

    private bool AtualizaAnexo(IWorkbook document, string item, string celulaNomeada)
    {
        try
        {
            var worksheet = GetWorksheet(document, item);
            if (worksheet == null)
            {
                erro = "Erro no template da planilha. Item " + item + " não encontrado!";
                return false;
            }

            var celulaPessoal122 = worksheet.Range[celulaNomeada];
            var nomeCelula = celulaPessoal122.GetRangeWithRelativeReference().GetReferenceA1();
            var valorNovo = BuscaValorPeloCodigo(celulaNomeada, item);

            if (valorNovo != null)
            {
                worksheet.Cells[nomeCelula].SetValueFromText(valorNovo);
            }

            document.History.Clear();

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }

    private bool AtualizaAnexoReformuladoOuExecutado(IWorkbook document, string item, string celulaNomeada, string planilha, string opcao)
    {
        try
        {
            var worksheet = GetWorksheet(document, item);
            if (worksheet == null)
            {
                erro = "Erro no template da planilha. Item " + item + " não encontrado!";
                return false;
            }

            var celulaPessoal122 = worksheet.Range[celulaNomeada];
            var nomeCelula = celulaPessoal122.GetRangeWithRelativeReference().GetReferenceA1();

            var valorNovo = BuscaValorReformuladoOuExecutadoPeloCodigo(celulaNomeada, item, opcao);

            if (valorNovo != null)
            {
                worksheet.Cells[nomeCelula].SetValueFromText(valorNovo);
            }

            document.History.Clear();

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }
    private void AtualizaAnoNoWorkSheet(Worksheet worksheet, string celulaNomeada)
    {
        try
        {
            //Atualiza o ano na planilha
            var celulaAno = worksheet.Range[celulaNomeada];
            var nomeCelula = celulaAno.GetReferenceA1();
            if (celulaAno != null) worksheet.Cells[nomeCelula].SetValueFromText(ano.ToString());
        }
        catch (Exception ex)
        {
            erro += "Erro ao atualizar ano no worksheet. Erro: " + ex.Message;
        }
    }

    protected Worksheet GetWorksheet(IWorkbook document, string item)
    {
        if (document == null)
            return null;
        if (document.Worksheets.Contains(item))
            return document.Worksheets[item];
        return null;
    }

    private void AtualizaAnoPlanilhaExecutadoEFormulado(IWorkbook document)
    {
        var listaWorksheets = document.Worksheets;
        try
        {
            foreach (Worksheet ws in listaWorksheets)
            {
                switch (ws.Name)
                {
                    case "Anexo_I_-_RECEITAS":
                        AtualizaAnoNoWorkSheet(ws, "anoReceitas");
                        break;
                    case "Anexo_I_-_DESPESAS":
                        AtualizaAnoNoWorkSheet(ws, "anoDespesas");
                        break;
                    case "Anexo_II":
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoII122");
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoII333");
                        break;
                    case "Anexo_III_-_DET":
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIII122");
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIII333");
                        break;
                    case "Anexo_IV":
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIVReceitas");
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIVDespesas");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            erro += "Erro ao atualizar ano na planilha. Erro:" + ex.Message;
        }
    }

    private void AtualizaAnoPlanilhaOrcamentaria(IWorkbook document)
    {
        var listaWorksheets = document.Worksheets;

        try
        {
            foreach (Worksheet ws in listaWorksheets)
            {
                switch (ws.Name)
                {
                    case "Anexo_I_-_RECEITAS":
                        AtualizaAnoNoWorkSheet(ws, "anoReceitas");
                        break;
                    case "Anexo_I_-_DESPESAS":
                        AtualizaAnoNoWorkSheet(ws, "anoDespesas");
                        break;
                    case "Anexo_II":
                        AtualizaAnoNoWorkSheet(ws, "anoAnexo122");
                        AtualizaAnoNoWorkSheet(ws, "anoAnexo333");
                        break;
                    case "Anexo_IV":
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIVReceitas");
                        AtualizaAnoNoWorkSheet(ws, "anoAnexoIVDespesas");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            erro += "Erro ao atualizar ano na planilha. Erro:" + ex.Message;
        }
    }
    private bool CarregaDadosPlanilha(int ano, int codigoEntidade, string planilha)
    {
        try
        {
            DataSet ds = null   ;
            if (planilha.Equals("ORC"))
                            ds = cDados.getDadosPlanilhaProposta(ano, codigoEntidade, planilha, int.Parse( this.cmbEtapas.SelectedItem.Value.ToString() ), null);
            else
                            ds = cDados.getDadosPlanilhaProposta(ano, codigoEntidade, planilha, null, int.Parse(this.cmbPeriodo.SelectedItem.Value.ToString()));
            //Entidade que traz registros 352 e 428 para os anos 2021 e 2022

            if (cDados.DataSetOk(ds))
            {
                receitas = ds.Tables[0].Select();
                despesas = ds.Tables[1].Select();
                despesas122 = ds.Tables[2].Select();
                despesas333 = ds.Tables[3].Select();
                
                despesas444 = ds.Tables[4].Select();
            }

            if (ds.Tables[0].Rows.Count == 0 &&
                ds.Tables[1].Rows.Count == 0 &&
                ds.Tables[2].Rows.Count == 0 &&
                ds.Tables[3].Rows.Count == 0 &&
                ds.Tables[4].Rows.Count == 0)
            {
                erro = "Nenhum registro encontrado para o ano selecionado!";
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
            throw ex;
        }
    }

    private string BuscaValorPeloCodigo(string codigo, string sheet)
    {
        try
        {
            //Faz a busca no sheet de receitas
            if (sheet == "Anexo_I_-_RECEITAS")
            {
                foreach (var item in from DataRow item in receitas
                                     where item["CONTA"].ToString() == codigo
                                     select item)
                {
                    return item["VALOR_0"].ToString();
                }
            }

            //Faz a busca no sheet de Despesas
            if (sheet == "Anexo_I_-_DESPESAS")
            {
                foreach (var item in from DataRow item in despesas
                                     where item["CONTA"].ToString() == codigo
                                     select item)
                {
                    return item["VALOR_0"].ToString();
                }
            }

            //Faz a busca no sheet de Anexo Dep
            if (sheet == "Anexo_III_-_DET")
            {
                string descricao = string.Empty;

                switch (codigo)
                {
                    case "pessoal122":
                    case "pessoal333":
                        descricao = "PESSOAL E ENCARGOS SOCIAIS";
                        break;
                    case "outrasDespesas122":
                    case "outrasDespesas333":
                        descricao = "OUTRAS DESPESAS CORRENTES";
                        break;
                    case "investimentos122":
                    case "investimentos333":
                        descricao = "INVESTIMENTOS";
                        break;
                }

                if (codigo == "pessoal122" || codigo == "outrasDespesas122" || codigo == "investimentos122")
                {
                    foreach (var item in from DataRow item in despesas122
                                         where item["DESC_DESPESA"].ToString() == descricao
                                         select item)
                    {
                        return item["VALOR_0"].ToString();
                    }
                }
                else
                {
                    foreach (var item in from DataRow item in despesas333
                                         where item["DESC_DESPESA"].ToString() == descricao
                                         select item)
                    {
                        return item["VALOR_0"].ToString();
                    }
                }
            }
            return null;

        }
        catch (Exception ex)
        {
            erro = "Erro: " + ex.Message;
            throw ex;
        }
    }
    private string BuscaValorReformuladoOuExecutadoPeloCodigo(string codigo, string sheet, string opcao)
    {
        try
        {
            //Faz a busca no sheet de receitas
            if (sheet == "Anexo_I_-_RECEITAS")
            {
                foreach (var item in from DataRow item in receitas
                                     where item["CONTA"].ToString() == codigo
                                     select item)
                {
                    if (opcao == "reformulado")
                    {
                        return item["VALOR_1"].ToString();
                    }
                    else
                    {
                        return item["VALOR_2"].ToString();
                    }
                }
            }

            //Faz a busca no sheet de Despesas
            if (sheet == "Anexo_I_-_DESPESAS")
            {
                foreach (var item in from DataRow item in despesas
                                     where item["CONTA"].ToString() == codigo
                                     select item)
                {
                    if (opcao == "reformulado")
                    {
                        return item["VALOR_1"].ToString();
                    }
                    else
                    {
                        return item["VALOR_2"].ToString();
                    }
                }
            }
            //Faz a busca no sheet de Anexo Dep
            if (sheet == "Anexo_III_-_DET")
            {
                string descricao = string.Empty;

                switch (codigo)
                {
                    case "pessoal122Reformulado":
                    case "pessoal333Reformulado":
                    case "pessoal122Executado":
                    case "pessoal333Executado":
                        descricao = "PESSOAL E ENCARGOS SOCIAIS";
                        break;
                    case "outrasDespesas122Reformulado":
                    case "outrasDespesas333Reformulado":
                    case "outrasDespesas122Executado":
                    case "outrasDespesas333Executado":
                        descricao = "OUTRAS DESPESAS CORRENTES";
                        break;
                    case "investimentos122Reformulado":
                    case "investimentos333Reformulado":
                    case "investimentos122Executado":
                    case "investimentos333Executado":
                        descricao = "INVESTIMENTOS";
                        break;
                }

                if (codigo == "pessoal122Reformulado" || codigo == "outrasDespesas122Reformulado" || codigo == "investimentos122Reformulado"
                    || codigo == "pessoal122Executado" || codigo == "outrasDespesas122Executado" || codigo == "investimentos122Executado")
                {
                    foreach (var item in from DataRow item in despesas122
                                         where item["DESC_DESPESA"].ToString() == descricao
                                         select item)
                    {
                        if (codigo.Contains("Reformulado"))
                        {
                            return item["VALOR_1"].ToString();
                        }
                        else
                        {
                            return item["VALOR_2"].ToString();
                        }
                    }
                }
                else
                {
                    foreach (var item in from DataRow item in despesas333
                                         where item["DESC_DESPESA"].ToString() == descricao
                                         select item)
                    {
                        if (codigo.Contains("Reformulado"))
                        {
                            return item["VALOR_1"].ToString();
                        }
                        else
                        {
                            return item["VALOR_2"].ToString();
                        }
                    }
                }
            }
            return null;

        }
        catch (Exception ex)
        {
            erro = "Erro: " + ex.Message;
            throw ex;
        }
    }

    private void CarregaComboAnos()
    {
        try
        {
            DataSet dsAnos = cDados.getAnosOrcamento(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel);

            if (cDados.DataSetOk(dsAnos))
            {
                cmbAnos.DataSource = dsAnos;
                cmbAnos.TextField = "Ano";
                cmbAnos.ValueField = "Ano";
                cmbAnos.DataBind();
            }
        }
        catch (Exception ex)
        {
            erro = "Erro ao carregar combo com os anos. Erro: " + ex.Message;
        }
    }

    //private void CarregaComboPlanilhas()
    //{
    //    try
    //    {
    //        cmbPlanilha.Items.Add("Planejamento Orçamentário", "ORC");
    //        cmbPlanilha.Items.Add("Execução Orçamentária", "EXE");
    //    }
    //    catch (Exception ex)
    //    {
    //        erro = "Erro ao carregar combo com as planilhas. Erro: " + ex.Message;
    //    }
    //}

    protected void cbSalvar_Callback1(object sender, CallbackEventArgsBase e)
    {
        cbSalvar.JSProperties["cp_ErroSalvar"] = "";

        ano = (int)(int.Parse(cmbAnos.Value != null ? cmbAnos.Value.ToString() : "-1"));

        // planilha = cmbPlanilha.Value != null ? cmbPlanilha.Value.ToString() : "-1";

        bool carregaPlanilha = CarregaDadosPlanilha(ano, codUnidade, this.hPlanilha.Value);
        if (carregaPlanilha)
        {
            if (CarregaArquivoPlanilha(this.hPlanilha.Value))
            {
                AtualizaDadosPlanilha(this.hPlanilha.Value);

                if (erro == "")
                    AspSpreadsheet.Visible = true;
            }
        }
        else
        {
            erro = "Erro ao carregar dados da planilha. Mensagem original do erro : " + erro;
        }

        if (erro != string.Empty) cbSalvar.JSProperties["cp_ErroSalvar"] = erro;

    }
}
