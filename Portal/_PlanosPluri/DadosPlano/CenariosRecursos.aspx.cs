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

public partial class _Estrategias_wizard_editaResultados : System.Web.UI.Page
{
    private int codigoPlano = -1;

    dados cDados;
    DataTable dtResultados;
    DataTable dtGrid = new DataTable();

    private int idUsuarioLogado;
    private int codigoEntidade;

    public bool podeEditar = true;

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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        cDados.aplicaEstiloVisual(gvResultados);

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoPlano = int.Parse(Request.QueryString["CP"].ToString());

        //podeEditar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", 0, "null", "IN_RegRes"); 
        
        carregaGridResultados(gvResultados);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AtualizacaoResultados.js""></script>"));
        podeEditar = cDados.podeEditarPPA(codigoPlano, codigoEntidade, idUsuarioLogado);

        gvResultados.Settings.ShowFilterRow = false;
        gvResultados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvResultados.SettingsBehavior.AllowSort = false;
        gvResultados.SettingsEditing.Mode = podeEditar ? GridViewEditingMode.Batch : GridViewEditingMode.PopupEditForm;
        gvResultados.Settings.ShowFooter = true;
    }

    #region Grid Resultados

    private void carregaGridResultados(ASPxGridView grid)
    {
        string chave = codigoPlano.ToString();

        grid.Columns.Clear();

        grid.AutoGenerateColumns = true;

        dtGrid = getResultados(int.Parse(chave));

        grid.DataSource = dtGrid;

        grid.DataBind();

        if (grid.Columns.Count > 0)
        {
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 150;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)grid.Columns[0]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 350;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = new Unit("100%");


            string[] fieldNames = new string[grid.Columns.Count - 1];

            for (int i = 1; i < grid.Columns.Count; i++)
            {
                fieldNames[i - 1] = ((GridViewDataTextColumn)grid.Columns[i]).FieldName;
                ((GridViewDataTextColumn)grid.Columns[i]).Visible = false;
            }

            for (int i = 0; i < fieldNames.Length; i++)
            {
                int indexColuna = i + 1;

                GridViewDataSpinEditColumn coluna = new GridViewDataSpinEditColumn();
                coluna.FieldName = fieldNames[i];

                grid.Columns.Insert(i + 1, coluna);

                grid.Columns[indexColuna].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 105;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "N2";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Left;
            }

            grid.Columns["CodigoConta"].Visible = false;

            grid.DataBind();
        }
    }

    private DataTable getResultados(int codigoPlano)
    {
        string comandoSQL = string.Format(@"
        SELECT * FROM dbo.f_pln_GetValoresCenario({0}, '{1}')
        ", codigoPlano, Request.QueryString["Tipo"]);

        dtResultados = cDados.getDataSet(comandoSQL).Tables[0];

        gvResultados.TotalSummary.Clear();

        if (cDados.DataTableOk(dtResultados))
        {
            DataTable dtNovosResultados = new DataTable();

            int ano = 0, mes = 0;

            dtNovosResultados.Columns.Add("DescricaoConta");

            ASPxSummaryItem total = new ASPxSummaryItem();
            total.ShowInColumn = "DescricaoConta";
            total.FieldName = "DescricaoConta";
            total.SummaryType = DevExpress.Data.SummaryItemType.None;
            total.DisplayFormat = "TOTAL";
            gvResultados.TotalSummary.Add(total);

            foreach (DataRow dr in dtResultados.Rows)
            {
                if (ano != int.Parse(dr["Ano"].ToString()) || mes != int.Parse(dr["Mes"].ToString()))
                {
                    dtNovosResultados.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                    ano = int.Parse(dr["Ano"].ToString());
                    mes = int.Parse(dr["Mes"].ToString());

                    ASPxSummaryItem somatorio = new ASPxSummaryItem();
                    somatorio.ShowInColumn = dr["Periodo"].ToString();
                    somatorio.FieldName = dr["Periodo"].ToString();
                    somatorio.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    somatorio.DisplayFormat = "N2";
                    gvResultados.TotalSummary.Add(somatorio);
                }
            }

            dtNovosResultados.Columns.Add("CodigoConta");

            comandoSQL = string.Format(@"
        SELECT DISTINCT CodigoConta, DescricaoConta FROM dbo.f_pln_GetValoresCenario({0}, '{1}')
        ", codigoPlano, Request.QueryString["Tipo"]);

            DataSet dsListaDados = cDados.getDataSet(comandoSQL); ;

            foreach (DataRow dr in dsListaDados.Tables[0].Rows)
            {
                DataRow drNovoResultado = getLinha(dtResultados, dtNovosResultados, int.Parse(dr["CodigoConta"].ToString()));

                if (drNovoResultado != null)
                    dtNovosResultados.Rows.Add(drNovoResultado);
            }
            return dtNovosResultados;
        }
        else
        {
            return null;
        }
    }

    private DataRow getLinha(DataTable dt, DataTable dtNova, int codigo)
    {
        int i = 1;

        if (dt.Select("CodigoConta = " + codigo).Length == 0)
            return null;

        DataRow drLinha = dtNova.NewRow();

        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse(dr["CodigoConta"].ToString()) == codigo)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["DescricaoConta"];
                }
                if (dr["Valor"].ToString() != "")
                    drLinha[i] = dr["Valor"].ToString();
                i++;
            }
        }

        drLinha[i] = codigo;

        return drLinha;
    }

    protected void gvResultados_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            DataRow[] drs = dtResultados.Select("CodigoConta = " + e.UpdateValues[i].Keys[0]);

            for (int j = 0; j < e.UpdateValues[i].NewValues.Count; j++)
            {
                object[] keys = new object[e.UpdateValues[i].NewValues.Keys.Count];
                e.UpdateValues[i].NewValues.Keys.CopyTo(keys, 0);
                string fieldName = keys[j].ToString();
                if (fieldName != "DescricaoConta")
                {
                    int codigoConta = int.Parse(e.UpdateValues[i].Keys[0].ToString());
                    DataRow[] dr = dtResultados.Select("Periodo = '" + fieldName + "' AND CodigoConta = " + codigoConta);


                    int mes = int.Parse(dr[0]["Mes"].ToString());
                    int ano = int.Parse(dr[0]["Ano"].ToString());

                    string valor = (e.UpdateValues[i].NewValues[j] != null && e.UpdateValues[i].NewValues[j].ToString() != "") ? e.UpdateValues[i].NewValues[j].ToString() : "NULL";

                    if(ano != -1 && mes != -1)
                        atualizaConta(codigoConta, mes, ano, valor);

                }
            }
        }

        carregaGridResultados(gvResultados);

        e.Handled = true;
    }

    public bool atualizaConta(int codigoConta, int mes, int ano, string valor)
    {
        string comandoSQL = "";
        int regAfetados = 0;

        try
        {
            comandoSQL = string.Format(
            @"BEGIN
	                DECLARE @CodigoConta int,
			                @Mes int,
			                @Ano int,
			                @Valor Decimal(18,4),
			                @PossuiRegistro int,
                            @CodigoPlano int
                	
	                SET @CodigoConta = {2}
	                SET @Mes = {3}
	                Set @Ano = {4}
	                Set @Valor = {5}
                    SET @CodigoPlano = {6}
                		
	                SELECT @PossuiRegistro = COUNT(1) 
	                  FROM {0}.{1}.PlanoCenarioFinanceiro
	                 WHERE CodigoConta = @CodigoConta
	                   AND Mes = @Mes
	                   AND Ano = @Ano
                       AND CodigoPlano = @CodigoPlano
                	
	                IF @PossuiRegistro = 0
		                INSERT INTO {0}.{1}.PlanoCenarioFinanceiro(CodigoPlano, CodigoConta, Ano, Mes, Valor) VALUES
										                (@CodigoPlano, @CodigoConta, @Ano, @Mes, @Valor)
	                ELSE
		                UPDATE {0}.{1}.PlanoCenarioFinanceiro SET Valor = @Valor
		                  WHERE CodigoConta = @CodigoConta
	                       AND Mes = @Mes
	                       AND Ano = @Ano
                           AND CodigoPlano = @CodigoPlano
                								
                END", cDados.getDbName(), cDados.getDbOwner(), codigoConta, mes, ano, valor.Replace(',', '.'), codigoPlano);

            cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void gvResultados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Index >= 1 && e.VisibleIndex > -1)
        {
            if (dtResultados != null)
            {
                string codigoDado = gvResultados.GetRowValues(e.VisibleIndex, "CodigoConta").ToString();

                DataRow[] dr = dtResultados.Select("CodigoConta = " + codigoDado + " AND Periodo = '" + e.DataColumn.FieldName + "'");

                hfGeral.Set(e.DataColumn.FieldName + "_" + e.VisibleIndex, !podeEditar ? "N" : dr[0]["Editavel"].ToString());

                if (dr[0]["Editavel"].ToString() != "S")
                {
                    e.Cell.BackColor = Color.FromName("#EBEBEB");
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.Font.Bold = true;
                    e.Cell.ToolTip = Resources.traducao.per_odo_n_o_edit_vel;
                }
                else if (podeEditar)
                {
                    e.Cell.BackColor = Color.White;
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = Resources.traducao.clique_para_editar;
                }
            }
        }

    }

    #endregion
}
