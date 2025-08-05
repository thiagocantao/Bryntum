using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using DevExpress.Web;
using System.Drawing;
using System.Data.SqlClient;
using DevExpress.Web.ASPxTreeList;

public partial class _Projetos_Administracao_frmItensMedicao : System.Web.UI.Page
{
    #region Fields

    dados cDados;

    protected int codigoContrato_imc;
    protected int? codigoPrograma_imc;
    protected int? codigoProjeto_imc;
    protected bool indicaContratoAssociadoProjeto;
    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public decimal valorTotalContrato = 0;

    #endregion

    #region Event Handlers

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

        DefineConnectionString();
        codigoContrato_imc = int.Parse(Request.QueryString["CC"]);
        
        cDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DefineVariaveis();
        DefineInformacoesVisiveis();
        tabControl.TabPages[2].ClientEnabled = codigoProjeto_imc.HasValue;

        /*if (tabControl.TabPages[0].IsActive)
            treeListItensMedicao.ExpandAll();
        else*/
        if (tabControl.TabPages[1].IsActive)
            gvDadosPrecoGlobal.ExpandAll();
        else if (tabControl.TabPages[2].IsActive)
            gvDadosImportacaoCronograma.ExpandAll();

        if (!IsPostBack)
            CorrigeInconsistenciasDados();
        AtualizaValorTotalItens();
    }

    protected void cmbProjeto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            ASPxComboBox combo = (ASPxComboBox)sender;
            codigoProjeto_imc = (int)combo.Value;
        }
        catch (Exception)
        {
            codigoProjeto_imc = null;
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string param = e.Parameter;
        int indiceSeparador = param.IndexOf(';');
        string operacao = param.Substring(0, indiceSeparador);
        string codigo = param.Substring(indiceSeparador + 1);

        switch (operacao.ToUpper())
        {
            case "NOVOITEM":
                e.Result = IncluiNovoItemFilho(codigo);
                break;
            case "NOVOITEMPAI":
                e.Result = IncluiNovoItemPai();
                break;
            case "EDITAITEMPAI":
                SalvaItemPai(codigo);
                break;
            case "INITFORMEDICAOITEMPAI":
                e.Result = InitFormEdicaoItemPai(codigo);
                break;
            case "IMPORTAITENSMEDICAOCRONOGRAMA":
                int insertedRowsCount = sdsImportacaoCronograma.Insert();
                e.Result = insertedRowsCount.ToString();
                break;
            case "SALVAITENSMEDICAOCRONOGRAMA":
                int updatedRowsCount = sdsImportacaoCronograma.Update();
                e.Result = updatedRowsCount.ToString();
                break;
            case "CANCELAIMPORTACAOITENSMEDICAOCRONOGRAMA":
                CorrigeInconsistenciasDados();
                break;
        }
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName.Equals("NumeroOrdem"))
        {
            ASPxGridView grid = (ASPxGridView)sender;
            bool novaLinha = grid.IsNewRowEditing;
            int ordem = novaLinha ? grid.VisibleRowCount + 1 :
                (int)grid.GetRowValues(e.VisibleIndex, "NumeroOrdem");
            int ordemMaxima = novaLinha ? grid.VisibleRowCount + 1 :
                (int)grid.GetRowValues(e.VisibleIndex, "NumeroOrdemMaximo");
            ASPxSpinEdit spinEdit = (ASPxSpinEdit)e.Editor;
            spinEdit.MinValue = 1;
            spinEdit.MaxValue = ordemMaxima;
            if (ordem == -1 || novaLinha)
                spinEdit.Value = ordemMaxima;
        }
    }

    protected void treeListItensMedicao_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
    {
        if (e.Column.FieldName.Equals("NumeroOrdem"))
        {
            ASPxTreeList treeList = (ASPxTreeList)sender;
            TreeListNode node = treeList.FocusedNode;
            bool novaLinha = treeList.IsNewNodeEditing;
            int numeroFilhos = ((node == null) ? treeListItensMedicao.Nodes.Count : node.ChildNodes.Count) + 1;
            int ordem = novaLinha ? numeroFilhos : (int)e.Value;
            int ordemMaxima = novaLinha ? numeroFilhos : (int)node["NumeroOrdemMaximo"];
            ASPxSpinEdit spinEdit = (ASPxSpinEdit)e.Editor;
            spinEdit.MinValue = 1;
            spinEdit.MaxValue = ordemMaxima;
            if (ordem == -1 || novaLinha)
                spinEdit.Value = ordemMaxima;
        }
    }

    protected void gvDadosImportacaoCronograma_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            string indicaImportacaoConfirmada = gvDadosImportacaoCronograma.GetRowValues(
                e.VisibleIndex, "IndicaImportacaoConfirmada").ToString().Trim();
            switch (indicaImportacaoConfirmada)
            {
                case "S":
                case "P":
                    e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";
                    e.Text = "Excluir";
                    break;
                case "XP":
                case "XS":
                    e.Image.Url = "~/imagens/botoes/incluirReg02.png";
                    e.Text = "Incluir";
                    break;
                default:
                    break;
            }
        }
    }

    protected void gvDadosImportacaoCronograma_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string indicaRegistroExcluido =
                e.GetValue("IndicaImportacaoConfirmada").ToString();

            if (indicaRegistroExcluido.Contains("X"))
            {
                e.Row.Font.Strikeout = true;
                e.Row.BackColor = Color.Gainsboro;
            }
        }
    }

    protected void gvResponsaveisMedicao_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        bool novaLinha = grid.IsNewRowEditing;
        string fieldName = e.Column.FieldName;
        if (fieldName.Equals("NumeroOrdemAssinatura"))
        {
            int ordem;
            int ordemMaxima;
            if (novaLinha)
            {
                ordem = grid.VisibleRowCount + 1;
                ordemMaxima = grid.VisibleRowCount + 1;
            }
            else
            {
                ordem = Convert.IsDBNull(e.Value) || e.Value == null ?
                            -1 : Convert.ToInt32(e.Value);
                ordemMaxima = grid.VisibleRowCount;
            }
            ASPxSpinEdit spinEdit = (ASPxSpinEdit)e.Editor;
            spinEdit.MinValue = 1;
            spinEdit.MaxValue = ordemMaxima;
            if (ordem == -1 || novaLinha)
                spinEdit.Value = ordemMaxima;
        }
        else if (fieldName.Equals("CodigoUsuario"))
        {
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            combo.ClientEnabled = novaLinha;
            SqlDataSource dataSource = sdsUsuarios;
            int codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
            string where = "--AND us.CodigoUsuario NOT IN (SELECT [CodigoUsuario] FROM [AssinaturasMedicaoContrato] WHERE CodigoContrato = " + codigoContrato_imc.ToString() + ")";
            string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade, "", where);
            cDados.populaComboVirtual(dataSource, comandoSQL, combo, 0, 150);
        }
    }

    void combo_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        SqlDataSource dataSource = sdsUsuarios;
        int codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        string where = "AND us.CodigoUsuario NOT IN (SELECT amc.[CodigoUsuario] FROM amc.[AssinaturasMedicaoContrato] amc WHERE CodigoContrato = " + codigoContrato_imc.ToString() + ")";
        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade, e.Filter, where);

        cDados.populaComboVirtual(dataSource, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    void combo_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (string.IsNullOrEmpty(sdsUsuarios.ConnectionString))
            sdsUsuarios.ConnectionString = cDados.classeDados.getStringConexao();
        if (e.Value != null)
        {
            long value;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlDataSource dataSource = sdsUsuarios;
            int codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
            dataSource.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidade);

            dataSource.SelectParameters.Clear();
            dataSource.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());

            comboBox.DataBind();
        }
    }

    protected void gvResponsaveisMedicao_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int novaOrdem = Convert.ToInt32(e.NewValues["NumeroOrdemAssinatura"]);
        int ordemAntiga = Convert.ToInt32(e.OldValues["NumeroOrdemAssinatura"]);
        if (novaOrdem != ordemAntiga)
        {
            string comandoSql = string.Empty;

            #region novaOrdem < ordemAntiga

            if (novaOrdem < ordemAntiga)
            {
                comandoSql = string.Format(@"
DECLARE @CodigoContrato INT,
		@NumeroOrdemAssinatura TINYINT,
		@NumeroOrdemAssinaturaAntiga TINYINT
		
	SET @CodigoContrato = {0}
	SET @NumeroOrdemAssinatura = {1}
	SET @NumeroOrdemAssinaturaAntiga = {2}

UPDATE [AssinaturasMedicaoContrato]
   SET [NumeroOrdemAssinatura] = [NumeroOrdemAssinatura] + 1
 WHERE [CodigoContrato] = @CodigoContrato
   AND [NumeroOrdemAssinatura] >= @NumeroOrdemAssinatura
   AND [NumeroOrdemAssinatura] < @NumeroOrdemAssinaturaAntiga"
                    , codigoContrato_imc, novaOrdem, ordemAntiga);
            }

            #endregion

            #region novaOrdem > ordemAntiga

            else
            {
                comandoSql = string.Format(@"
DECLARE @CodigoContrato INT,
		@NumeroOrdemAssinatura TINYINT,
		@NumeroOrdemAssinaturaAntiga TINYINT
		
	SET @CodigoContrato = {0}
	SET @NumeroOrdemAssinatura = {1}
	SET @NumeroOrdemAssinaturaAntiga = {2}

UPDATE [AssinaturasMedicaoContrato]
   SET [NumeroOrdemAssinatura] = [NumeroOrdemAssinatura] -1
 WHERE [CodigoContrato] = @CodigoContrato
   AND [NumeroOrdemAssinatura] <= @NumeroOrdemAssinatura
   AND [NumeroOrdemAssinatura] > @NumeroOrdemAssinaturaAntiga"
                            , codigoContrato_imc, novaOrdem, ordemAntiga);
            }

            #endregion

            dados cDados = CdadosUtil.GetCdados(null);
            int registrosAfetados = 0;
            cDados.execSQL(comandoSql, ref registrosAfetados);
        }
    }

    protected void gvResponsaveisMedicao_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int novaOrdem = Convert.ToInt32(e.NewValues["NumeroOrdemAssinatura"]);
        string comandoSql = string.Format(@"
DECLARE @CodigoContrato INT,
		@NumeroOrdemAssinatura TINYINT
		
	SET @CodigoContrato = {0}
	SET @NumeroOrdemAssinatura = {1}

UPDATE [AssinaturasMedicaoContrato]
   SET [NumeroOrdemAssinatura] = [NumeroOrdemAssinatura] + 1
 WHERE [CodigoContrato] = @CodigoContrato
   AND [NumeroOrdemAssinatura] >= @NumeroOrdemAssinatura"
                    , codigoContrato_imc, novaOrdem);

        dados cDados = CdadosUtil.GetCdados(null);
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    protected void gvResponsaveisMedicao_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        int ordem = Convert.ToInt32(e.Values["NumeroOrdemAssinatura"]);
        string comandoSql = string.Format(@"
DECLARE @CodigoContrato INT,
		@NumeroOrdemAssinatura TINYINT
		
	SET @CodigoContrato = {0}
	SET @NumeroOrdemAssinatura = {1}

UPDATE [AssinaturasMedicaoContrato]
   SET [NumeroOrdemAssinatura] = [NumeroOrdemAssinatura] - 1
 WHERE [CodigoContrato] = @CodigoContrato
   AND [NumeroOrdemAssinatura] > @NumeroOrdemAssinatura"
                    , codigoContrato_imc, ordem);

        dados cDados = CdadosUtil.GetCdados(null);
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    protected void sdsResponsaveisMedicao_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (!e.Command.Parameters.Contains("@CodigoContrato"))
        {
            SqlParameter param = new SqlParameter("@CodigoContrato", codigoContrato_imc);
            e.Command.Parameters.Add(param);
        }
    }

    protected void treeListItensMedicao_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
    {
        if (e.ButtonType == TreeListCommandColumnButtonType.Custom && e.CustomButtonIndex == 2)
        {
            TreeListNode node = treeListItensMedicao.FindNodeByKeyValue(e.NodeKey);
            if (node.ChildNodes.Count > 0)
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
    }

    #endregion

    #region Methods

    private void DefineVariaveis()
    {
        string nomeBanco = cDados.getDbName();
        string owner = cDados.getDbOwner();
        #region Comando SQL
        string comandoSql = string.Format(@"
 SELECT c.NumeroContrato,
        p.CodigoProjeto,  
        p.NomeProjeto,
        p.IndicaPrograma,
        c.ValorContrato
   FROM {0}.{1}.Contrato c LEFT JOIN
        {0}.{1}.Projeto p ON c.CodigoProjeto = p.CodigoProjeto LEFT JOIN
		{0}.{1}.Pessoa AS pes ON (pes.CodigoPessoa = c.CodigoPessoaContratada)  LEFT JOIN 
        {0}.{1}.[PessoaEntidade] AS [pe] ON (
			pe.[CodigoPessoa] = c.[CodigoPessoaContratada]
			AND pe.codigoEntidade = c.codigoEntidade
            --AND pe.IndicaFornecedor = 'S'
			)
  WHERE c.CodigoContrato = {2}
    and c.tipoPessoa = 'F'"
    , nomeBanco, owner, codigoContrato_imc);
        #endregion
        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        string numeroContrato = (string)dr["NumeroContrato"];
        object codigoProjetoContrato = dr["CodigoProjeto"];
        string nomeProjeto = dr["NomeProjeto"].ToString();
        string indicaPrograma = dr["IndicaPrograma"].ToString();
        Session["CodigoPrograma"] = -1;
        Session["CodigoProjeto"] = -1;
        Session["CodigoEntidade"] = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        indicaContratoAssociadoProjeto = !Convert.IsDBNull(codigoProjetoContrato);
        valorTotalContrato = Convert.ToDecimal(dr["ValorContrato"]);
        tabControl.JSProperties["cp_ValorContrato"] = valorTotalContrato;
        lblValorTotalContrato.Text = string.Format("{0:c2}", valorTotalContrato);
        if (indicaContratoAssociadoProjeto)
        {
            if (indicaPrograma.Equals("S"))
            {
                codigoPrograma_imc = (int)codigoProjetoContrato;
                Session["CodigoPrograma"] = codigoProjetoContrato;
                if (IsPostBack && cmbProjeto.Value != null)
                {
                    int codigoProjetoDoPrograma = (int)cmbProjeto.Value;
                    Session["CodigoProjeto"] = codigoProjetoDoPrograma;
                    codigoProjeto_imc = codigoProjetoDoPrograma;
                }
                else
                {
                    Session["CodigoProjeto"] = -1;
                    codigoProjeto_imc = null;
                }
                lblNomePrograma.Text = nomeProjeto;
            }
            else
            {
                codigoPrograma_imc = null;
                codigoProjeto_imc = (int)codigoProjetoContrato;
                Session["CodigoProjeto"] = codigoProjetoContrato;
                lblNomeProjeto.Text = nomeProjeto;
            }
        }
        else
        {
            if (IsPostBack && cmbProjetosEntidade.Value != null)
            {
                codigoPrograma_imc = null;
                int codigoProjetoDaEntidade = (int)cmbProjetosEntidade.Value;
                Session["CodigoProjeto"] = codigoProjetoDaEntidade;
                codigoProjeto_imc = codigoProjetoDaEntidade;
            }
        }
    }

    private void AtualizaValorTotalItens()
    {
        string nomeBanco = cDados.getDbName();
        string owner = cDados.getDbOwner();

        #region Comando SQL
        string comandoSql = string.Format(@"
DECLARE @CodigoContrato INT = {2},
        @CodigoPrograma INT = {3},
        @CodigoProjeto INT = {4}

 SELECT SUM(imc.QuantidadePrevistaTotal * imc.ValorUnitarioItem) AS ValorTotalItens
   FROM {0}.{1}.ItemMedicaoContrato imc
  WHERE imc.CodigoContrato = @CodigoContrato
    AND ISNULL(imc.CodigoPrograma, -1) = @CodigoPrograma
    AND ISNULL(imc.CodigoProjeto, -1) = @CodigoProjeto
    AND imc.DataExclusaoItem IS NULL
    AND (imc.IndicaImportacaoConfirmada IS NULL OR imc.IndicaImportacaoConfirmada = 'S')"
    , nomeBanco
    , owner
    , codigoContrato_imc
    , codigoPrograma_imc.HasValue ? codigoPrograma_imc.Value : -1
    , codigoProjeto_imc.HasValue ? codigoProjeto_imc.Value : -1);
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        lblValorTotalItens.Text = string.Format("{0:c2}", dr["ValorTotalItens"]);
        string valorTotalItens = "0" + dr["ValorTotalItens"] + "";//Bug 3747: [F021644][NE]Erro ao abrir "Itens de Medição" nos "Contratos" de um projeto
        tabControl.JSProperties["cp_ValorTotalItens"] = Convert.ToDecimal(valorTotalItens);
    }

    private void DefineConnectionString()
    {
        string connectionString = cDados.classeDados.getStringConexao();
        sdsItemMedicaoContrato.ConnectionString = connectionString;
        sdsItemMedicaoContratoPrecoGlobal.ConnectionString = connectionString;
        sdsProjetosDoPrograma.ConnectionString = connectionString;
        sdsProjetosEntidade.ConnectionString = connectionString;
        sdsImportacaoCronograma.ConnectionString = connectionString;
        sdsResponsaveisMedicao.ConnectionString = connectionString;
        sdsUsuarios.ConnectionString = connectionString;
    }

    private void SalvaItemPai(string codigoItemPai)
    {
        #region Consulta SQL

        string comandoSql = string.Format(@"
DECLARE @CodigoContrato INT
DECLARE @CodigoPrograma INT
DECLARE @CodigoProjeto INT
DECLARE @CodigoItemMedicaoContrato INT
DECLARE @Original_NumeroOrdem INT
DECLARE @NumeroOrdem INT
DECLARE @DescricaoItem VARCHAR(255)

    SET @CodigoContrato = {5}
    SET @CodigoPrograma = {6}
    SET @CodigoProjeto = {7}
    SET @CodigoItemMedicaoContrato = {2}
    SET @NumeroOrdem = {4}
    SET @DescricaoItem = '{3}'

 SELECT @Original_NumeroOrdem = NumeroOrdem
   FROM ItemMedicaoContrato
  WHERE CodigoItemMedicaoContrato = @CodigoItemMedicaoContrato

IF(@Original_NumeroOrdem > @NumeroOrdem)
BEGIN
 UPDATE ItemMedicaoContrato
    SET NumeroOrdem = NumeroOrdem + 1
  WHERE CodigoItemPai = 0
    AND [CodigoContrato] = @CodigoContrato
    AND (CodigoPrograma = @CodigoPrograma OR (CodigoPrograma IS NULL AND @CodigoPrograma = -1))
    AND (CodigoProjeto = @CodigoProjeto OR (CodigoProjeto IS NULL AND @CodigoProjeto = -1))
    AND CodigoItemMedicaoContrato <> @CodigoItemMedicaoContrato
    AND NumeroOrdem >= @NumeroOrdem
    AND NumeroOrdem < @Original_NumeroOrdem
    AND DataExclusaoItem IS NULL
END
ELSE IF(@Original_NumeroOrdem < @NumeroOrdem)
BEGIN
 UPDATE ItemMedicaoContrato
    SET NumeroOrdem = NumeroOrdem - 1
  WHERE CodigoItemPai = 0
    AND [CodigoContrato] = @CodigoContrato
    AND (CodigoPrograma = @CodigoPrograma OR (CodigoPrograma IS NULL AND @CodigoPrograma = -1))
    AND (CodigoProjeto = @CodigoProjeto OR (CodigoProjeto IS NULL AND @CodigoProjeto = -1))
    AND CodigoItemMedicaoContrato <> @CodigoItemMedicaoContrato
    AND NumeroOrdem <= @NumeroOrdem
    AND NumeroOrdem > @Original_NumeroOrdem
    AND DataExclusaoItem IS NULL
    AND [CodigoAtribuicao] IS NULL
END

 UPDATE ItemMedicaoContrato
    SET DescricaoItem = @DescricaoItem,
        NumeroOrdem = @NumeroOrdem
  WHERE CodigoItemMedicaoContrato = @CodigoItemMedicaoContrato"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , codigoItemPai
            , txtDescricaoItemPai.Text.Replace("'", "''")
            , seOrdem.Value
            , codigoContrato_imc
            , Session["CodigoPrograma"]
            , Session["CodigoProjeto"]);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private string AjustaSelectCommand(string selectCommand)
    {
        int indexOfOrderBy = selectCommand.IndexOf("ORDER BY");
        if (indexOfOrderBy == -1)
            indexOfOrderBy = selectCommand.Length;
        string comandoSql = selectCommand.Substring(0, indexOfOrderBy);
        string strOrderBy = selectCommand.Substring(indexOfOrderBy);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(comandoSql);
        if (codigoPrograma_imc.HasValue)
            sb.AppendLine(string.Format("   AND (imc.CodigoPrograma = {0})", codigoPrograma_imc.Value));
        else
            sb.AppendLine("   AND (imc.CodigoPrograma IS NULL)");
        if (codigoProjeto_imc.HasValue)
            sb.AppendLine(string.Format("   AND (imc.CodigoProjeto = {0})", codigoProjeto_imc.Value));
        else
            sb.AppendLine("   AND (imc.CodigoProjeto IS NULL)");
        sb.AppendLine(strOrderBy);

        return sb.ToString();
    }

    private void CorrigeInconsistenciasDados()
    {
        #region Comando SQL
        string comandoSql = string.Format(@"
 DELETE 
   FROM ItemMedicaoContrato 
  WHERE NumeroOrdem = -1

 DELETE 
   FROM ItemMedicaoContrato
  WHERE CodigoItemPai = 0 
    AND CodigoItemMedicaoContrato NOT IN(SELECT DISTINCT
                                                CodigoItemPai 
                                           FROM ItemMedicaoContrato 
                                          WHERE CodigoItemPai <> 0)
 DELETE 
   FROM ItemMedicaoContrato 
  WHERE IndicaImportacaoConfirmada IN ('P', 'XP')
  
 UPDATE ItemMedicaoContrato 
    SET IndicaImportacaoConfirmada = 'S'
  WHERE IndicaImportacaoConfirmada = 'XS'
 ");
        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private string InitFormEdicaoItemPai(string codigoItemPai)
    {
        string resultado = string.Empty;
        string strCodPrograma = codigoPrograma_imc.HasValue ? codigoPrograma_imc.ToString() : "NULL";
        string strCodProjeto = codigoProjeto_imc.HasValue ? codigoProjeto_imc.ToString() : "NULL";

        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @CodigoItemPai INT
    SET @CodigoItemPai = {2}

DECLARE @CodigoContrato INT
    SET @CodigoContrato = {3}

DECLARE @CodigoPrograma INT
    SET @CodigoPrograma = {4}

DECLARE @CodigoProjeto INT
    SET @CodigoProjeto = {5}

DECLARE @NumeroOrdemMaximo INT
 SELECT @NumeroOrdemMaximo = COUNT(*)
   FROM {0}.{1}.ItemMedicaoContrato 
  WHERE CodigoItemPai = 0
    AND DataExclusaoItem IS NULL
    AND CodigoContrato = @CodigoContrato
    AND (CodigoPrograma = @CodigoPrograma OR @CodigoPrograma IS NULL)
    AND (CodigoProjeto = @CodigoProjeto OR @CodigoProjeto IS NULL)
    AND (CodigoAtribuicao IS NULL)
    
IF(@CodigoItemPai = -1)
 SELECT NULL AS DescricaoItem,
        @NumeroOrdemMaximo + 1 AS NumeroOrdem,
        @NumeroOrdemMaximo + 1 AS NumeroOrdemMaximo
ELSE
 SELECT DescricaoItem, 
        NumeroOrdem,
        @NumeroOrdemMaximo AS NumeroOrdemMaximo
   FROM {0}.{1}.ItemMedicaoContrato
  WHERE CodigoItemMedicaoContrato = @CodigoItemPai"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , codigoItemPai
            , codigoContrato_imc
            , strCodPrograma
            , strCodProjeto);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        resultado = string.Format("{0};{1};{2}",
                        dr["DescricaoItem"],
                        dr["NumeroOrdem"],
                        dr["NumeroOrdemMaximo"]);
        return resultado;
    }

    private string IncluiNovoItemFilho(string codigoItemPai)
    {
        string codPrograma = codigoPrograma_imc.HasValue ?
            codigoPrograma_imc.Value.ToString() : "NULL";
        string codProjeto = codigoProjeto_imc.HasValue ?
            codigoProjeto_imc.Value.ToString() : "NULL";

        #region Comando SQL
        string comandoSql = string.Format(@"
        DECLARE @CodigoItem INT

         INSERT INTO {0}.{1}.ItemMedicaoContrato
                    (CodigoContrato
                    ,CodigoPrograma
                    ,CodigoProjeto
                    ,DescricaoItem
                    ,CodigoItemPai
                    ,NumeroOrdem
                    ,IndicaItemPrecoGlobal)
              VALUES
                    ({2}
                    ,{3}
                    ,{4}
                    ,''
                    ,{5}
                    ,-1
                    ,'N')

              SET @CodigoItem = @@IDENTITY
 
           SELECT @CodigoItem AS CodigoItem,
                  (COUNT(*) + 1) AS NumeroOrdemSugerido                  
             FROM {0}.{1}.ItemMedicaoContrato 
            WHERE CodigoItemPai = {5}
              AND DataExclusaoItem IS NULL",
                                cDados.getDbName(),
                                cDados.getDbOwner(),
                                codigoContrato_imc,
                                codPrograma,
                                codProjeto,
                                codigoItemPai);
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        return dr["CodigoItem"].ToString();
    }

    private string IncluiNovoItemPai()
    {
        string comandoSql;
        string descricaoItemPai = txtDescricaoItemPai.Text;
        string ordemItemPai = seOrdem.Text;
        string codPrograma = codigoPrograma_imc.HasValue ?
            codigoPrograma_imc.Value.ToString() : "NULL";
        string codProjeto = codigoProjeto_imc.HasValue ?
            codigoProjeto_imc.Value.ToString() : "NULL";

        #region Comando SQL
        comandoSql = string.Format(@"
        DECLARE @CodigoItemPai INT
        DECLARE @CodigoItem INT
        DECLARE @CodigoContrato INT
            SET @CodigoContrato = {7}
        DECLARE @CodigoPrograma INT
            SET @CodigoPrograma = {8}
        DECLARE @CodigoProjeto INT
            SET @CodigoProjeto = {9}

         INSERT INTO {0}.{1}.ItemMedicaoContrato
                    (CodigoContrato
                    ,CodigoPrograma
                    ,CodigoProjeto
                    ,DescricaoItem
                    ,NumeroOrdem
                    ,CodigoItemPai
                    ,IndicaItemPrecoGlobal)
              VALUES
                    ({2}
                    ,{3}
                    ,{4}
                    ,'{5}'
                    ,{6}
                    ,0
                    ,'N')

              SET @CodigoItemPai = @@IDENTITY

        UPDATE [ItemMedicaoContrato]
           SET [NumeroOrdem] = [NumeroOrdem] + 1
         WHERE CodigoItemMedicaoContrato <> @CodigoItemPai
           AND [CodigoItemPai] = 0
           AND [CodigoContrato] = @CodigoContrato
           AND (CodigoPrograma = @CodigoPrograma OR (CodigoPrograma IS NULL AND @CodigoPrograma = -1))
           AND (CodigoProjeto = @CodigoProjeto OR (CodigoProjeto IS NULL AND @CodigoProjeto = -1))
           AND [NumeroOrdem] >= {6}
           AND [DataExclusaoItem] IS NULL
           AND [CodigoAtribuicao] IS NULL

         INSERT INTO {0}.{1}.ItemMedicaoContrato
                    (CodigoContrato
                    ,CodigoPrograma
                    ,CodigoProjeto
                    ,DescricaoItem
                    ,CodigoItemPai
                    ,NumeroOrdem
                    ,IndicaItemPrecoGlobal)
              VALUES
                    ({2}
                    ,{3}
                    ,{4}
                    ,''
                    ,@CodigoItemPai
                    ,-1
                    ,'N')

              SET @CodigoItem = @@IDENTITY
 
           SELECT @CodigoItem AS CodigoItem"
           , cDados.getDbName()
           , cDados.getDbOwner()
           , codigoContrato_imc
           , codPrograma
           , codProjeto
           , descricaoItemPai.Replace("'", "''")
           , ordemItemPai
           , codigoContrato_imc
           , Session["CodigoPrograma"]
           , Session["CodigoProjeto"]);
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];

        return dr["CodigoItem"].ToString();
    }

    private void DefineInformacoesVisiveis()
    {
        bool mostrarInfoPrograma = false;
        bool mostrarInfoProjeto = false;
        if (indicaContratoAssociadoProjeto)
        {
            if (codigoPrograma_imc.HasValue && codigoPrograma_imc.Value != -1)
                mostrarInfoPrograma = true;
            else if (codigoProjeto_imc.HasValue && codigoProjeto_imc.Value != -1)
                mostrarInfoProjeto = true; 
        }
        tdInformacoesPrograma.Style.Add(HtmlTextWriterStyle.Display,
            mostrarInfoPrograma ? string.Empty : "none");
        tdInformacoesProjeto.Style.Add(HtmlTextWriterStyle.Display,
            mostrarInfoProjeto ? string.Empty : "none");
        tdInformacoesEntidade.Style.Add(HtmlTextWriterStyle.Display,
            !(mostrarInfoPrograma || mostrarInfoProjeto) ? string.Empty : "none");
    }

    protected string ObtemBtnIncluir(int codigoItemPai)
    {
        string imagemHabilitada = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo Item"" onclick=""NovoItem({0});"" style=""cursor: pointer;""/>", codigoItemPai);
        string imagemDesabilitada = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        string conteudoHtml = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>",
            (podeIncluir) ? imagemHabilitada : imagemDesabilitada);
        return conteudoHtml;
    }

    protected string MontaLinkItemPai(int codigoItemPai, string descricaoItemPai)
    {
        string linkItemPai = string.Format("<a hfer=\"#\" onclick=\"InitFormEdicaoItemPai({0}); \" style=\"cursor: pointer;\">{1}</a>"
            , codigoItemPai, descricaoItemPai);
        return linkItemPai;
    }

    protected string ObtemBtnIncluirItemPai()
    {
        string imagemHabilitada = @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Atividade"" onclick=""NovoItemPai();"" style=""cursor: pointer;""/>";
        string imagemDesabilitada = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        string conteudoHtml = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>",
            (podeIncluir) ? imagemHabilitada : imagemDesabilitada);
        return conteudoHtml;
    }

    protected string ObtemBtnIncluirItemGlobal()
    {
        string imagemHabilitada = @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Atividade"" onclick=""gvDadosPrecoGlobal.AddNewRow();"" style=""cursor: pointer;""/>";
        string imagemDesabilitada = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        string conteudoHtml = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>",
            (podeIncluir) ? imagemHabilitada : imagemDesabilitada);
        return conteudoHtml;
    }
    #endregion
}