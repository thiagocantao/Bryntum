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
using System.Drawing;

public partial class _Portfolios_Administracao_CriteriosCategorias : System.Web.UI.Page
{
    dados cDados;
    private string whereUpdateDelete;
    private int CodigoEntidade = 0;
    private int idUsuarioLogado;
    private int alturaPrincipal = 0;

    private char delimitador = '¥';

    int contador = 0;
    int contadorRiscos = 0;
   public  bool podeIncluir = false;
    DataSet dsValoresCriterios = new DataSet();
    DataSet dsValoresRiscos = new DataSet();

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
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_CadCtgPrj");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        cDados.aplicaEstiloVisual(Page);
        
        if (!IsPostBack)
        {            
            defineAlturaTela();
            populaGrid();
        }
        
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;
        
       
        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_CadCtgPrj"))
        {
            podeIncluir = true;
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Cadastro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/categoriasNova.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "Cadastro", "categoriasNova"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Categorias</title>"));
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
       int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 225;
        gvDados.Width = new Unit((largura - 10) + "px");
    }

    /*private void MenuUsuarioLogado()
    {
        BarraNavegacao1.MostrarInclusao = false;
        BarraNavegacao1.MostrarEdicao = false;
        BarraNavegacao1.MostrarExclusao = false;

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "ADMPMTPTF"))
        {
            BarraNavegacao1.MostrarInclusao = true;
            BarraNavegacao1.MostrarEdicao = true;
            BarraNavegacao1.MostrarExclusao = true;
        }
    }*/

    #endregion

    #region Popula Objetos

    private void populaGrid()
    {
        DataSet ds = cDados.getCategoriasEntidade(CodigoEntidade, "");
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

        string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
        captionGrid += string.Format(@"</tr></table>");
        gvDados.SettingsText.Title = captionGrid;
    }

    private void populaListaBox_CriteriosSelecaoDisponivel(int codigoCategoria)
    {
        DataSet ds = cDados.getCriteriosSelecaoUnidade_Disponivel(codigoCategoria, CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            lbDisponiveis.DataSource = ds.Tables[0];
            lbDisponiveis.TextField = "DescricaoCriterioSelecao";
            lbDisponiveis.ValueField = "CodigoCriterioSelecao";
            lbDisponiveis.DataBind();
        }
    }

    private void populaListaBox_CriteriosSelecaoSelecionado(int codigoCategoria)
    {
        DataSet ds = cDados.getCriteriosSelecaoUnidade_Selecionado(codigoCategoria, CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            lbSelecionados.DataSource = ds.Tables[0];
            lbSelecionados.TextField = "DescricaoCriterioSelecao";
            lbSelecionados.ValueField = "CodigoCriterioSelecao";
            lbSelecionados.DataBind();

            // Os codigos dos critérios selecionados serão armazenados na variável abaixo e enviados por HiddenField ao cliente
            string CodigosCriteriosSelecionados = "";
            string TextosCriteriosSelecionados = "";
            for (int i = 0; i < lbSelecionados.Items.Count; i++)
            {
                CodigosCriteriosSelecionados += lbSelecionados.Items[i].Value.ToString() + delimitador;
                TextosCriteriosSelecionados += lbSelecionados.Items[i].Text + delimitador;
            }
            hfCriteriosSelecionados.Set("CodigosCriteriosSelecionados", CodigosCriteriosSelecionados);
            hfCriteriosSelecionados.Set("TextosCriteriosSelecionados", TextosCriteriosSelecionados);
        }
    }

    private void populaListaBox_RiscoDisponivel(int codigoCategoria)
    {
        DataSet ds = cDados.getRiscosPadroesUnidade_Disponivel(codigoCategoria, CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            lbDisponiveisRiscos.DataSource = ds.Tables[0];
            lbDisponiveisRiscos.TextField = "DescricaoRiscoPadrao";
            lbDisponiveisRiscos.ValueField = "CodigoRiscoPadrao";
            lbDisponiveisRiscos.DataBind();
        }
    }

    private void populaListaBox_RiscoSelecionado(int codigoCategoria)
    {
        DataSet ds = cDados.getRiscosPadroesUnidade_Selecionado(codigoCategoria, CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            lbSelecionadosRiscos.DataSource = ds.Tables[0];
            lbSelecionadosRiscos.TextField = "DescricaoRiscoPadrao";
            lbSelecionadosRiscos.ValueField = "CodigoRiscoPadrao";
            lbSelecionadosRiscos.DataBind();

            // Os codigos dos critérios selecionados serão armazenados na variável abaixo e enviados por HiddenField ao cliente
            string CodigosRiscosSelecionados = "";
            string TextosRiscosSelecionados = "";
            for (int i = 0; i < lbSelecionadosRiscos.Items.Count; i++)
            {
                CodigosRiscosSelecionados += lbSelecionadosRiscos.Items[i].Value.ToString() + delimitador;
                TextosRiscosSelecionados += lbSelecionadosRiscos.Items[i].Text + delimitador;
            }
            hfRiscosSelecionados.Set("CodigosRiscosSelecionados", CodigosRiscosSelecionados);
            hfRiscosSelecionados.Set("TextosRiscosSelecionados", TextosRiscosSelecionados);
        }
    }

    private void carregaGridCriterios()
    {
        contador = 0;
        if (!gridMatrizRiscos.IsEditing)
        {
            int codigoCategoria = 0;

            string[] aCodigosCriteriosSelecionados = hfCriteriosSelecionados.Get("CodigosCriteriosSelecionados").ToString().Split(delimitador);
            string[] aTextosCriteriosSelecionados = hfCriteriosSelecionados.Get("TextosCriteriosSelecionados").ToString().Split(delimitador);

            DataTable dt = new DataTable();

            dt.Columns.Add("Criterio");
            dt.Columns.Add("Peso");

            for (int i = 0; i < aCodigosCriteriosSelecionados.Length - 1; i++)
            {
                dt.Rows.Add(aTextosCriteriosSelecionados[i], " ");
            }

            gridMatriz.DataSource = dt;

            for (int i = 1; i <= aCodigosCriteriosSelecionados.Length - 1; i++)
            {
                GridViewDataColumn coluna = new GridViewDataColumn();
                coluna.Assign(gridMatriz.Columns[1]);
                coluna.VisibleIndex = i + 1;
                gridMatriz.Columns.Add(coluna);
            }

            gridMatriz.DataBind();

            for (int i = 1; i <= aCodigosCriteriosSelecionados.Length - 1; i++)
            {
                gridMatriz.Columns[i].Visible = true;
                gridMatriz.Columns[i].Caption = aTextosCriteriosSelecionados[i - 1];
            }

            gridMatriz.Columns[gridMatriz.Columns.Count - 1].Visible = true;
            gridMatriz.Columns[gridMatriz.Columns.Count - 1].Caption = "Peso";
            gridMatriz.Columns[0].Caption = "Critérios";


            // O dataset abaixo será utilizado apenas no evento gridMatriz_HtmlDataCellPrepared
            string chave = getChavePrimaria();
            if (chave != "")
            {
                codigoCategoria = int.Parse(chave);
                dsValoresCriterios = cDados.getValoresCategoriasUnidade(codigoCategoria, "");
            }
        }
    }

    private void carregaGridRiscos()
    {
        contadorRiscos = 0;
        if (!gridMatrizRiscos.IsEditing)
        {
            int codigoCategoria = 0;

            string[] aCodigosRiscosSelecionados = hfRiscosSelecionados.Get("CodigosRiscosSelecionados").ToString().Split(delimitador);
            string[] aTextosRiscosSelecionados = hfRiscosSelecionados.Get("TextosRiscosSelecionados").ToString().Split(delimitador);

            DataTable dt = new DataTable();

            dt.Columns.Add("Riscos");
            dt.Columns.Add("Peso");

            for (int i = 0; i < aCodigosRiscosSelecionados.Length - 1; i++)
            {
                dt.Rows.Add(aTextosRiscosSelecionados[i], " ");
            }

            gridMatrizRiscos.DataSource = dt;

            for (int i = 1; i <= aCodigosRiscosSelecionados.Length - 1; i++)
            {
                GridViewDataColumn coluna = new GridViewDataColumn();
                coluna.Assign(gridMatrizRiscos.Columns[1]);
                coluna.VisibleIndex = i + 1;
                gridMatrizRiscos.Columns.Add(coluna);
            }

            gridMatrizRiscos.DataBind();

            for (int i = 1; i <= aCodigosRiscosSelecionados.Length - 1; i++)
            {
                gridMatrizRiscos.Columns[i].Visible = true;
                gridMatrizRiscos.Columns[i].Caption = aTextosRiscosSelecionados[i - 1];
            }

            gridMatrizRiscos.Columns[gridMatrizRiscos.Columns.Count - 1].Visible = true;
            gridMatrizRiscos.Columns[gridMatrizRiscos.Columns.Count - 1].Caption = "Peso";
            gridMatrizRiscos.Columns[0].Caption = "Riscos";

            // O dataset abaixo será utilizado apenas no evento gridMatriz_HtmlDataCellPrepared
            string chave = getChavePrimaria();
            if (chave != "")
            {
                codigoCategoria = int.Parse(chave);
                dsValoresRiscos = cDados.getValoresRiscosUnidade(codigoCategoria, "");
            }
        }
    }

    #endregion

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        try
        {
            salvaRegistro("I", -1);

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            salvaRegistro("E", int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            string msg = "";
            int regAfetados = 0;

            int codCategoria = int.Parse(chave);
            cDados.excluiCategoria(codCategoria, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), ref msg, ref regAfetados);
            populaGrid();
            return msg;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void salvaRegistro(string modo, int codCategoria )
    {
        string msg = "";
        bool retorno = false;
        int regAfetados = 0;

        string[] aCodigosCriteriosSelecionados = hfCriteriosSelecionados.Get("CodigosCriteriosSelecionados").ToString().Split(delimitador);
        string[] aCodigosRiscosSelecionados = hfRiscosSelecionados.Get("CodigosRiscosSelecionados").ToString().Split(delimitador);

        int[] codigosCriterios = new int[aCodigosCriteriosSelecionados.Length - 1]; //new int[lbSelecionados.Items.Count];
        int[] valoresCriterios = new int[(int)Math.Pow(aCodigosCriteriosSelecionados.Length - 1, 2)];
        int[] codigosRiscos = new int[aCodigosRiscosSelecionados.Length - 1]; //new int[lbSelecionadosRiscos.Items.Count];
        int[] valoresRiscos = new int[(int)Math.Pow(aCodigosRiscosSelecionados.Length - 1, 2)]; //new int[lbSelecionadosRiscos.Items.Count * lbSelecionadosRiscos.Items.Count];

        int count = 0;


        for (int i = 0; i < aCodigosCriteriosSelecionados.Length - 1; i++)
        {
            codigosCriterios[i] = int.Parse(aCodigosCriteriosSelecionados[i].ToString());

            DataRow dr = gridMatriz.GetDataRow(i);

            for (int j = 0; j < aCodigosCriteriosSelecionados.Length - 1; j++)
            {
                valoresCriterios[count] = int.Parse(hfValores.Get("Criterio_" + i + "_" + (j + 1)).ToString());
                count++;
            }
        }

        count = 0;

        for (int i = 0; i < aCodigosRiscosSelecionados.Length - 1; i++)
        {
            codigosRiscos[i] = int.Parse(aCodigosRiscosSelecionados[i].ToString());

            DataRow dr = gridMatrizRiscos.GetDataRow(i);

            for (int j = 0; j < aCodigosRiscosSelecionados.Length - 1; j++)
            {
                valoresRiscos[count] = int.Parse(hfValoresRiscos.Get("Risco_" + i + "_" + (j + 1)).ToString());
                count++;
            }
        }        

        if (modo == "I")
        {
            retorno = cDados.incluiCriteriosCategoria(txtCategoria.Text, txtSigla.Text, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), CodigoEntidade, codigosCriterios, valoresCriterios, codigosRiscos, valoresRiscos, ref msg, ref regAfetados);

            if (!retorno)
            {
                cDados.alerta(this, msg);
            }
        }
        else if (modo == "E")
        {
            retorno = cDados.atualizaCriteriosCategoria(codCategoria, txtCategoria.Text, txtSigla.Text, codigosCriterios, valoresCriterios, codigosRiscos, valoresRiscos, ref msg, ref regAfetados);

            if (!retorno)
            {
                cDados.alerta(this, msg);
            }
        }
        populaGrid();
        gvDados.FocusedRowIndex = 0;
    }

    #region Callbacks

    protected void pn_ListBox_Criterios_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 3)
        {
            string comando = e.Parameter.Substring(0, 3).ToUpper();
            if (comando == "POP")
            {
                string codigoCategoria = e.Parameter.Substring(4);
                populaListaBox_CriteriosSelecaoDisponivel(int.Parse(codigoCategoria));
                populaListaBox_CriteriosSelecaoSelecionado(int.Parse(codigoCategoria));

                btnADDTodos.ClientEnabled = lbDisponiveis.Items.Count > 0;
                btnADD.ClientEnabled = lbDisponiveis.Items.Count > 0;

                btnRMV.ClientEnabled = lbSelecionados.Items.Count > 0;
                btnRMVTodos.ClientEnabled = lbSelecionados.Items.Count > 0;

                carregaGridCriterios();
            }
        }
    }

    protected void pn_ListBox_Riscos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 3)
        {
            string comando = e.Parameter.Substring(0, 3).ToUpper();
            if (comando == "POP")
            {
                string codigoCategoria = e.Parameter.Substring(4);
                populaListaBox_RiscoDisponivel(int.Parse(codigoCategoria));
                populaListaBox_RiscoSelecionado(int.Parse(codigoCategoria));

                btnADDTodosRiscos.ClientEnabled = lbDisponiveisRiscos.Items.Count > 0;
                btnADDRiscos.ClientEnabled = lbDisponiveisRiscos.Items.Count > 0;

                btnRMVRiscos.ClientEnabled = lbSelecionadosRiscos.Items.Count > 0;
                btnRMVTodosRiscos.ClientEnabled = lbSelecionadosRiscos.Items.Count > 0;

                carregaGridRiscos();
            }
        }
    }

    protected void pnCallback_GridCriterios_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaGridCriterios();
    }

    protected void pnCallback_GridRiscos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaGridRiscos();
    }

    #endregion

    #region Eventos HTML...Prepared

    protected void gridMatriz_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.Style.Add("height", "28px");
        e.Cell.Attributes.Add("id", "cell_" + e.VisibleIndex + "_" + e.DataColumn.Index);
        e.Cell.HorizontalAlign = HorizontalAlign.Center;

        // para a primeira coluna (nome do critério) não faz nada
        if (e.DataColumn.Index == 0)
            return;

        // para a última coluna (Peso) apenas coloca um espaço em branco 
        if (e.DataColumn.Index == gridMatriz.Columns.Count - 1)
        {
            e.Cell.Text = " ";
            return;
        }

        // inicializa a variável com "1", mas este valor poderá ser lido do banco conforme a regra "qtdRegistrosGrid == dsValoresCriterios.Tables[0].Rows.Count"
        string conteudoCelula = "1";

        // obtem a quantidade de registro apresentados na grid
        int qtdRegistrosGrid = (gridMatriz.Columns.Count - 2) * (gridMatriz.Columns.Count - 2);

        // se a quantidade de registros da grid é igual a quantidade de registro do banco, o valor da variável conteudoCelula será lido do banco
        if(cDados.DataSetOk(dsValoresCriterios))
            if (qtdRegistrosGrid == dsValoresCriterios.Tables[0].Rows.Count)
                conteudoCelula = (dsValoresCriterios.Tables[0].Rows[contador]["ValorRelacaoCriterioDePara"] + "" == "True") ? "1" : "0";

        // Se a celula atual é uma "célula Superior", tem que ter um textbox para a entrada de dados.
        if (e.DataColumn.Index - 1 > e.VisibleIndex)
        {
            e.Cell.Text = conteudoCelula;
            ASPxTextBox txt = new ASPxTextBox();
            txt.ID = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.HorizontalAlign = HorizontalAlign.Center;
            txt.Border.BorderStyle = BorderStyle.None;
            txt.Width = 35;
            txt.Height = 28;
            txt.MaskSettings.Mask = "<0..1>";
            txt.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            txt.Text = conteudoCelula;
            txt.ClientSideEvents.LostFocus = "function(s, e) {alteraValor(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ");}";
            txt.ClientSideEvents.KeyUp = "function(s, e) {if(e.htmlEvent.keyCode == 13){alteraValor(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ")};}";
            e.Cell.Controls.Add(txt);
        }

        // se a célula atual é a central, o valor sempre será "1".
        else if (e.DataColumn.Index - 1 == e.VisibleIndex)
            e.Cell.Text = "1";

        // a célula atual é uma "célula inferior", e seu valor virá do banco ou será o inverso da celula superior
        else
        {
            if (cDados.DataSetOk(dsValoresCriterios) && cDados.DataTableOk(dsValoresCriterios.Tables[0]))
                e.Cell.Text = (qtdRegistrosGrid == dsValoresCriterios.Tables[0].Rows.Count) ? conteudoCelula : "0";
            else
                e.Cell.Text = "0";
        }

        // envia o valor da celula atual para o cliente via hiddenField
        hfValores.Set("Criterio_" + e.VisibleIndex + "_" + e.DataColumn.Index, e.Cell.Text);

        contador++;

        // ajusta a cor de fundo da célula
        if (e.DataColumn.Index - 1 == e.VisibleIndex)
            e.Cell.BackColor = Color.FromName("#E0E0E0");
        else if (e.DataColumn.Index - 1 < e.VisibleIndex)
            e.Cell.BackColor = Color.FromName("#EBEBEB");
    }

    protected void gridMatrizRiscos_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.Style.Add("height", "28px");
        e.Cell.Attributes.Add("id", "cellRiscos_" + e.VisibleIndex + "_" + e.DataColumn.Index);
        e.Cell.HorizontalAlign = HorizontalAlign.Center;

        // para a primeira coluna (nome do risco) não faz nada
        if (e.DataColumn.Index == 0)
            return;

        // para a última coluna (Peso) apenas coloca um espaço em branco 
        if (e.DataColumn.Index == gridMatrizRiscos.Columns.Count - 1)
        {
            e.Cell.Text = " ";
            return;
        }

        // inicializa a variável com "1", mas este valor poderá ser lido do banco conforme a regra "qtdRegistrosGrid == dsValoresRiscos.Tables[0].Rows.Count"
        string conteudoCelula = "1";

        // obtem a quantidade de registro apresentados na grid
        int qtdRegistrosGrid = (gridMatrizRiscos.Columns.Count - 2) * (gridMatrizRiscos.Columns.Count - 2);

        // se a quantidade de registros da grid é igual a quantidade de registro do banco, o valor da variável conteudoCelula será lido do banco
        if (cDados.DataSetOk(dsValoresRiscos))
            if (qtdRegistrosGrid == dsValoresRiscos.Tables[0].Rows.Count)
                conteudoCelula = (dsValoresRiscos.Tables[0].Rows[contadorRiscos]["ValorRelacaoRiscoPadraoDePara"] + "" == "True") ? "1" : "0";

        // Se a celula atual é uma "célula Superior", tem que ter um textbox para a entrada de dados.
        if (e.DataColumn.Index - 1 > e.VisibleIndex)
        {
            e.Cell.Text = conteudoCelula;
            ASPxTextBox txt = new ASPxTextBox();
            txt.ID = "txtRiscos_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.ClientInstanceName = "txtRiscos_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.HorizontalAlign = HorizontalAlign.Center;
            txt.Border.BorderStyle = BorderStyle.None;
            txt.Width = 35;
            txt.Height = 28;
            txt.MaskSettings.Mask = "<0..1>";
            txt.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            txt.Text = conteudoCelula;
            txt.ClientSideEvents.LostFocus = "function(s, e) {alteraValorRiscos(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ");}";
            txt.ClientSideEvents.KeyUp = "function(s, e) {if(e.htmlEvent.keyCode == 13){alteraValorRiscos(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ")};}";
            e.Cell.Controls.Add(txt);
        }

        // se a célula atual é a central, o valor sempre será "1".
        else if (e.DataColumn.Index - 1 == e.VisibleIndex)
            e.Cell.Text = "1";

        // a célula atual é uma "célula inferior", e seu valor virá do banco ou será o inverso da celula superior
        else
            e.Cell.Text = (qtdRegistrosGrid == dsValoresRiscos.Tables[0].Rows.Count) ? conteudoCelula : "0";

        // envia o valor da celula atual para o cliente via hiddenField
        hfValoresRiscos.Set("Risco_" + e.VisibleIndex + "_" + e.DataColumn.Index, e.Cell.Text);

        contadorRiscos++;

        // ajusta a cor de fundo da célula
        if (e.DataColumn.Index - 1 == e.VisibleIndex)
            e.Cell.BackColor = Color.FromName("#E0E0E0");
        else if (e.DataColumn.Index - 1 < e.VisibleIndex)
            e.Cell.BackColor = Color.FromName("#EBEBEB");

    }

    protected void gridMatriz_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }
    }

    protected void gridMatrizRiscos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }
    }

    #endregion
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }
}
