using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Agil_EquipeProjetoAgil : System.Web.UI.Page
{
    dados cDados;

    public static bool indicaProjetoAgil = false;
    public static int codigoProjeto { get; set; }
    public static int codigoProjetoDoRequestQueryString { get; set; }

    public int codigoEntidadeUsuarioResponsavel;
    public int codigoUsuarioResponsavel;
    public bool podeManterEquipe = false;
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
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        Session["ce"] = codigoEntidadeUsuarioResponsavel;
        codigoProjetoDoRequestQueryString = codigoProjeto = int.Parse(Request.QueryString["cp"] + "");
        SetConnectionStrings();
        configuraTela();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);
        ((GridViewDataTextColumn)gvDados.Columns["NomeRecurso"]).HeaderStyle.Font.Bold = false;
        this.TH(this.TS("EquipeProjetoAgil"));
        podeManterEquipe = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_EqAgil");
        if (!IsPostBack)
        {
            if (!podeManterEquipe)
            {
                try
                {
                    this.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
                }
                catch
                {
                    this.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
                    this.Response.End();
                }
            }
        }

        carregaGrid();

    }

    private void carregaGrid()
    {
        string comandoSQL_projetoagil = string.Format(@"SELECT 
                            rp.CodigoRecursoProjeto, 
                            rp.CodigoRecursoCorporativo, 
                            rp.CodigoTipoPapelRecursoProjeto, 
                            rc.NomeRecurso, 
                            tpr.DescricaoTipoPapelRecurso,
                            rp.PercentualAlocacao
                           FROM Agil_RecursoProjeto AS rp 
                           INNER JOIN vi_RecursoCorporativo AS rc ON rc.CodigoRecursoCorporativo = rp.CodigoRecursoCorporativo 
                           INNER JOIN Agil_TipoPapelRecurso AS tpr ON tpr.CodigoTipoPapelRecurso = rp.CodigoTipoPapelRecursoProjeto 
                           WHERE (rp.CodigoProjeto = {0}) ORDER BY rc.NomeRecurso", codigoProjeto);

        string comandoSQL_no_projetoagil = string.Format(@"SELECT
                            ri.CodigoRecursoIteracao AS CodigoRecursoProjeto, 
                            ri.CodigoRecursoCorporativo, 
                            ri.CodigoTipoPapelRecursoIteracao as CodigoTipoPapelRecursoProjeto, 
                            rc.NomeRecurso, 
                            tpr.DescricaoTipoPapelRecurso,
                            ri.PercentualAlocacao
						   from Agil_RecursoIteracao ri
						   INNER JOIN vi_RecursoCorporativo AS rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo 
                           INNER JOIN Agil_TipoPapelRecurso AS tpr ON tpr.CodigoTipoPapelRecurso = ri.CodigoTipoPapelRecursoIteracao
                           WHERE (ri.CodigoIteracao = {0}) ORDER BY rc.NomeRecurso", codigoProjeto);

        DataSet ds = cDados.getDataSet(indicaProjetoAgil == true ? comandoSQL_projetoagil : comandoSQL_no_projetoagil);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private void SetConnectionStrings()
    {
            sdsRecursoCorporativo.ConnectionString =
            sdsTipoPapelRecurso.ConnectionString = cDados.ConnectionString;
    }


    private void configuraTela()
    {

        string comandoSQL = string.Format(@"
        SELECT IndicaProjetoAgil, IndicaTipoProjeto 
          FROM TipoProjeto
         WHERE CodigoTipoProjeto 
            IN (SELECT CodigoTipoProjeto
                  FROM projeto
                 WHERE CodigoProjeto = {0})", codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            indicaProjetoAgil = (ds.Tables[0].Rows[0]["IndicaProjetoAgil"].ToString().Trim() + "") == "S";
        }

        if (!indicaProjetoAgil)
        {
            string comandoSQL1 = @"SELECT CodigoIteracao FROM Agil_Iteracao WHERE CodigoProjetoIteracao = " + codigoProjeto;
            DataSet ds1 = cDados.getDataSet(comandoSQL1);
            if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                codigoProjeto = int.Parse(ds1.Tables[0].Rows[0]["CodigoIteracao"].ToString());
            }
        }
        btnFechar.ClientVisible = !(Request.QueryString["popup"] != null && Request.QueryString["popup"] == "N");
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if(e.Column.FieldName == "CodigoRecursoCorporativo")
        {
            if (e.VisibleIndex > -1)
            {
                string nomeRecurso = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "NomeRecurso").ToString();

                ASPxComboBox combo = e.Editor as ASPxComboBox;

                combo.ForeColor = System.Drawing.Color.Black;
                combo.BackColor = System.Drawing.Color.Gainsboro;
                combo.Text = nomeRecurso;
                combo.Enabled = false;

            }
            else
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                string comandoSQL = string.Format(@"
BEGIN
	DECLARE @IndicaProjetoAgil as char(1)
	SET @IndicaProjetoAgil = 'S'
	SELECT TOP 1 @IndicaProjetoAgil = IndicaProjetoAgil
		FROM TipoProjeto
	WHERE CodigoTipoProjeto
		IN(SELECT CodigoTipoProjeto
			FROM projeto
			WHERE CodigoProjeto = {0})
	IF(@IndicaProjetoAgil = 'S')
	BEGIN
		SELECT rc.[NomeRecurso], rc.[CodigoRecursoCorporativo]
			FROM [vi_RecursoCorporativo] as rc
		WHERE CodigoEntidade = {1}
			AND TipoRecurso = 1
			AND rc.CodigoRecursoCorporativo NOT IN 
                (   SELECT rp.CodigoRecursoCorporativo
					    FROM Agil_RecursoProjeto rp
						WHERE rp.CodigoProjeto = {0}
						AND rp.CodigoRecursoCorporativo IS NOT NULL
                )
			ORDER BY NomeRecurso
	END
	ELSE
	BEGIN
		SELECT [NomeRecurso], [CodigoRecursoCorporativo]
			FROM [vi_RecursoCorporativo] as rc
		WHERE CodigoEntidade = {1}
			AND TipoRecurso = 1
			AND rc.CodigoRecursoCorporativo IN 
				(	SELECT rp.[CodigoRecursoCorporativo]
						FROM 
							[dbo].[Agil_Iteracao]		AS [itr]

							INNER JOIN [dbo].[LinkProjeto]					AS [lp]		ON 
								(		lp.[CodigoProjetoFilho] = itr.[CodigoProjetoIteracao]
									AND lp.[TipoLink]						= 'PJPJ' )

							INNER JOIN [dbo].[Agil_RecursoProjeto]	AS [rp]		ON 
								(		rp.[CodigoProjeto] = lp.[CodigoProjetoPai]
									AND rp.[CodigoRecursoCorporativo] IS NOT NULL )
						WHERE itr.[CodigoProjetoIteracao] = {0}
				)
			AND rc.CodigoRecursoCorporativo NOT IN 
				(	SELECT ri.CodigoRecursoCorporativo
						FROM Agil_RecursoIteracao ri INNER JOIN [dbo].[Agil_Iteracao] itr ON itr.[CodigoIteracao] = ri.[CodigoIteracao]
						WHERE itr.[CodigoProjetoIteracao] = {0}
							AND ri.CodigoRecursoCorporativo IS NOT NULL
				)
			ORDER BY NomeRecurso
	END
END
", codigoProjetoDoRequestQueryString, Session["ce"]);

                DataSet ds = cDados.getDataSet(comandoSQL);
                combo.DataSource = ds;
                combo.DataBind();
            }
        }

    }

    protected void gvDados_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";


        if (e.AffectedRecords > 0)
        {
            ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.EquipeProjetoAgil_recurso_exclu_do_com_sucesso_;
        }
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        string comandoSQL_agil = string.Format(@"DELETE FROM[Agil_RecursoProjeto] WHERE [CodigoRecursoProjeto] = {0}", getChavePrimaria());
        string comandoSQL_naoagil = string.Format(@"DELETE FROM[Agil_RecursoIteracao] WHERE [CodigoRecursoIteracao] = {0}", getChavePrimaria());

        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";


        try
        {
            int regafetados = 0;
            cDados.execSQL((indicaProjetoAgil == true) ? comandoSQL_agil : comandoSQL_naoagil, ref regafetados);
            
            //sqlDataSource.Delete();
            ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.EquipeProjetoAgil_recurso_exclu_do_com_sucesso_;

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cpErro"] = Resources.traducao.EquipeProjetoAgil_erro_ao_excluir__ + ex.Message;
        }
        e.Cancel = true;
        gvDados.CancelEdit();

    }

    protected void gvDados_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ((ASPxGridView)sender).SettingsText.PopupEditFormCaption = "Adicionar Recurso";
    }

  


    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        DevExpress.Web.MenuItem btnIncluir = (sender as ASPxMenu).Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = "Incluir";


        DevExpress.Web.MenuItem btnImportar = (sender as ASPxMenu).Items.FindByName("btnImportar");
        btnImportar.ToolTip = "Importar Recurso";
        btnImportar.ClientVisible = indicaProjetoAgil;

        (sender as ASPxMenu).ClientSideEvents.ItemClick = @"function(s, e){
            e.processOnServer = false;
            if(e.item.name == 'btnIncluir')
            {
                gvDados.AddNewRow();
            }		                     
	        else if(e.item.name === 'btnImportar')
	        {
                window.top.mostraConfirmacao('Deseja importar os recursos do cronograma para a equipe do projeto?', function(){callbackImportar.PerformCallback();}, null); 
	        }	
        }";
    }

    protected void callbackImportar_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpAlerta"] = "";
        bool retorno = false;
        string comandoSql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjeto int

        SET @in_CodigoProjeto = {0}

        EXECUTE @RC = [dbo].[p_Agil_ImportaRecursosCronograma] 
            @in_CodigoProjeto", codigoProjeto);

        try
        {
            int regAfetados = 0;
            retorno = cDados.execSQL(comandoSql, ref regAfetados);
            if(regAfetados == 0)
            {
                ((ASPxCallback)source).JSProperties["cpAlerta"] = "Não foram encontrados novos recursos a serem importados!";
            }
            else
            {
                if(regAfetados == 1)
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = regAfetados + " Recurso foi importado com sucesso!";
                }
                else if (regAfetados > 1)
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = regAfetados + " Recursos foram importados com sucesso!";
                }
            }
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }

    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }


    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string comandoSQL_projetoagil = string.Format(@"UPDATE [Agil_RecursoProjeto] 
                           SET [CodigoTipoPapelRecursoProjeto] = {0} ,  
                               [PercentualAlocacao] = {1}
                          WHERE [CodigoRecursoProjeto] = {2}", e.NewValues["CodigoTipoPapelRecursoProjeto"].ToString(),
                          e.NewValues["PercentualAlocacao"] == null ? "0" : e.NewValues["PercentualAlocacao"].ToString(),
                          getChavePrimaria());

        string comandoSQL_no_projetoagil = string.Format(@"
                         UPDATE [Agil_RecursoIteracao] 
                           SET [CodigoTipoPapelRecursoIteracao] = {0} ,  
                               [PercentualAlocacao] = {1}
                          WHERE [CodigoRecursoIteracao] = {2}", e.NewValues["CodigoTipoPapelRecursoProjeto"].ToString(),
                          e.NewValues["PercentualAlocacao"] == null ? "0" : e.NewValues["PercentualAlocacao"].ToString(),
                          getChavePrimaria());

        int regAfetados = 0;
        try
        {
            cDados.execSQL(indicaProjetoAgil == true ? comandoSQL_projetoagil : comandoSQL_no_projetoagil, ref regAfetados);
        }
        catch(Exception ex)
        {

        }
        e.Cancel = true;
        gvDados.CancelEdit();

    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string comandosql_projetoagil = string.Format(@"
                              IF NOT EXISTS (SELECT 1 FROM Agil_RecursoProjeto 
                                              WHERE [CodigoRecursoCorporativo] = {0}
                                                AND [CodigoTipoPapelRecursoProjeto] = {1}
                                                AND [CodigoProjeto] = {2}
                                                AND [PercentualAlocacao] = {3})
                               BEGIN
                                      INSERT INTO [Agil_RecursoProjeto] ([CodigoRecursoCorporativo], [CodigoTipoPapelRecursoProjeto], [CodigoProjeto], [PercentualAlocacao]) 
                                                                  VALUES ({0}                      , {1}                            , {2}            , {3})
                               END", e.NewValues["CodigoRecursoCorporativo"].ToString(), 
                               e.NewValues["CodigoTipoPapelRecursoProjeto"].ToString(), codigoProjeto,
                               e.NewValues["PercentualAlocacao"] == null ? "0" : e.NewValues["PercentualAlocacao"].ToString());

        string comandosql_no_projetoagil = string.Format(@"
                            IF NOT EXISTS (SELECT 1 FROM Agil_RecursoIteracao
                                           WHERE [CodigoRecursoCorporativo] = {0}
                                             AND [CodigoTipoPapelRecursoIteracao] = {1}
                                             AND [CodigoIteracao] = {2}
                                             AND [PercentualAlocacao] = {3})
                                BEGIN
                                      INSERT INTO [Agil_RecursoIteracao] ([CodigoRecursoCorporativo], [CodigoTipoPapelRecursoIteracao], [CodigoIteracao], [PercentualAlocacao]) 
                                                                  VALUES ({0}                       ,                              {1},              {2}, {3})
                                END", e.NewValues["CodigoRecursoCorporativo"].ToString(),
                               e.NewValues["CodigoTipoPapelRecursoProjeto"].ToString(), codigoProjeto,
                               e.NewValues["PercentualAlocacao"] == null ? "0" : e.NewValues["PercentualAlocacao"].ToString());

        int regAfetados = 0;
        try
        {
            cDados.execSQL(indicaProjetoAgil == true ? comandosql_projetoagil : comandosql_no_projetoagil, ref regAfetados);
        }
        catch (Exception ex)
        {

        }
        e.Cancel = true;
        gvDados.CancelEdit();

    }
}