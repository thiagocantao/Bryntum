using DevExpress.Utils;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Agil_CadastroRaias : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = -1;
    public bool podeManterRaias = false;
    public int codigoUsuarioResponsavel = -1;
    public int codigoEntidadeUsuarioResponsavel = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);
        dataSource.ConnectionString = cDados.ConnectionString;
        int.TryParse(Request.QueryString["CP"] + "", out codigoProjeto);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ((GridViewDataTextColumn)grid.Columns["NomeRaia"]).HeaderStyle.Font.Bold = false;
        podeManterRaias = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_CadRaias");
        if (!IsPostBack)
        {
            if (!podeManterRaias)
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
    }

    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.VisibleIndex > 0 && e.Column.FieldName == "PercentualConcluido")
        {
            var editor = e.Editor as ASPxSpinEdit;
            if (editor == null) return;

            int visibleIndex = e.VisibleIndex;
            var raiaBloqueada = VerificaRaiaBloqueada(visibleIndex);
            var raiaPossuiItens = VerificaRaiaPossuiItensAssociados(visibleIndex);

            editor.ClientEnabled = !(raiaBloqueada || raiaPossuiItens);
        }
    }

    protected void grid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType != ColumnCommandButtonType.Delete) return;

        bool raiaBloqueada = VerificaRaiaBloqueada(e.VisibleIndex);
        e.Visible = !raiaBloqueada;
        if (e.Visible)
        {
            bool possuiItensAssociados = VerificaRaiaPossuiItensAssociados(e.VisibleIndex);
            if (possuiItensAssociados)
            {
                e.Enabled = false;
                e.Image.ToolTip = "A raia possui itens associados a ela.";
                e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
            }
        }
    }

    protected void grid_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            var visibleIndex = e.VisibleIndex;
            var paraCima = e.ButtonID == "btnPraCima";
            var visivel = VerificaBotaoAlteracaoPosicaoVisivel(visibleIndex, paraCima);
            e.Visible = visivel ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }

    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        var parameters = e.Parameters.Split('|');

        string sql;

        #region Comando SQL

        sql = string.Format(@"
  UPDATE Agil_RaiasIteracao
     SET SequenciaApresentacaoRaia = SequenciaApresentacaoRaia -1
   WHERE CodigoRaia = {0}

  UPDATE Agil_RaiasIteracao
     SET SequenciaApresentacaoRaia = SequenciaApresentacaoRaia + 1
   WHERE CodigoRaia = {1}", args: parameters);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(sql, ref registrosAfetados);

        grid.DataBind();
    }

    private bool VerificaBotaoAlteracaoPosicaoVisivel(int visibleIndex, bool paraCima)
    {
        if (VerificaRaiaBloqueada(visibleIndex)) return false;

        var fieldNames = new[] { "SequenciaApresentacaoRaia", "PercentualConcluido" };
        var values = (object[])grid.GetRowValues(visibleIndex, fieldNames);
        var compareValues = paraCima ?
            (object[])grid.GetRowValues(visibleIndex - 1, fieldNames) :
            (object[])grid.GetRowValues(visibleIndex + 1, fieldNames);

        var sequenciaApresentacao = (byte)values[0];
        var percentualConcluido = (byte)values[1];
        if(compareValues[0] != null && compareValues[1] != null)
        {
            var sequenciaApresentacaoComparacao = (byte)compareValues[0];
            var percentualConcluidoComparacao = (byte)compareValues[1];
            var limiteValor = paraCima ? byte.MinValue : byte.MaxValue;

            if (percentualConcluido != percentualConcluidoComparacao) return false;

            if (sequenciaApresentacaoComparacao == limiteValor) return false;
        }
        else
        {
            return false;
        }
        return true;
    }

    bool VerificaRaiaBloqueada(int visibleIndex)
    {
        var sequenciaApresentacaoRaia = (byte)grid.GetRowValues(visibleIndex, "SequenciaApresentacaoRaia");
        return (sequenciaApresentacaoRaia == byte.MinValue || sequenciaApresentacaoRaia == byte.MaxValue);
    }

    bool VerificaRaiaPossuiItensAssociados(int visibleIndex)
    {
        return (bool)grid.GetRowValues(visibleIndex, "PossuiItensAssociados");
    }

    protected void dataSource_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        string nomeraia = (e.Command.Parameters["@NomeRaia"].Value != null) ? e.Command.Parameters["@NomeRaia"].Value.ToString() : "";
        string comandoSQL = string.Format(@"
        DECLARE @CodigoIteracao as int
        SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = {0}
         SELECT ri.*, 
        CONVERT(bit, CASE WHEN EXISTS (SELECT 1 FROM Agil_ItemBacklog AS ib WHERE ib.CodigoRaia = ri.CodigoRaia) THEN 1 ELSE 0 END) AS PossuiItensAssociados
   FROM [dbo].[f_Agil_GetRaiasIteracao](@CodigoIteracao) AS ri
where ri.NomeRaia = '{1}'
  ORDER BY 
        SequenciaApresentacaoRaia, 
        PercentualConcluido", codigoProjeto, nomeraia);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            e.Cancel = true;
        }
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }

    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        int linhasAfetadas = e.AffectedRecords;
        ((ASPxGridView)(sender)).JSProperties["cpErro"] = "";
        ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = "";

        if (e.AffectedRecords == 0)
        {
            ((ASPxGridView)(sender)).JSProperties["cpErro"] = Resources.traducao.CadastroRaias_n_o___permitida_inclus_o_de_raias_com_mesmo_nome__a_opera__o_n_o_foi_realizada_;

        }
        else
        {
            ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = Resources.traducao.CadastroRaias_raia_inclu_da_com_sucesso_;
        }
    }

    protected void grid_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        int linhasAfetadas = e.AffectedRecords;
        ((ASPxGridView)(sender)).JSProperties["cpErro"] = "";
        ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = "";

        if (e.AffectedRecords == 0)
        {
            ((ASPxGridView)(sender)).JSProperties["cpErro"] = Resources.traducao.CadastroRaias_n_o___possivel_aterar_o_nome_de_uma_raia_para_um_nome_de_raia_j__existente__nenhuma_opera__o_foi_realizada_;

        }
        else
        {
            ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = Resources.traducao.CadastroRaias_raia_atualizada_com_sucesso_;
        }
    }

    protected void dataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        string nomeraia = (e.Command.Parameters["@NomeRaia"].Value != null) ? e.Command.Parameters["@NomeRaia"].Value.ToString() : "";
        string codigoRaia = (e.Command.Parameters["@CodigoRaia"].Value != null) ? e.Command.Parameters["@CodigoRaia"].Value.ToString() : "";

        string comandoSQL = string.Format(@"
        DECLARE @CodigoIteracao as int
        SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = {0}
         SELECT ri.*, 
        CONVERT(bit, CASE WHEN EXISTS (SELECT 1 FROM Agil_ItemBacklog AS ib WHERE ib.CodigoRaia = ri.CodigoRaia) THEN 1 ELSE 0 END) AS PossuiItensAssociados
   FROM [dbo].[f_Agil_GetRaiasIteracao](@CodigoIteracao) AS ri
where ri.NomeRaia = '{1}' and ri.CodigoRaia <> {2}
  ORDER BY 
        SequenciaApresentacaoRaia, 
        PercentualConcluido", codigoProjeto, nomeraia, codigoRaia);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            e.Cancel = true;
        }
    }

    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        int linhasAfetadas = e.AffectedRecords;
        ((ASPxGridView)(sender)).JSProperties["cpErro"] = "";
        ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = "";

        if (e.AffectedRecords == 0)
        {
            ((ASPxGridView)(sender)).JSProperties["cpErro"] = "Problema: Nenhuma raia foi excluída.";

        }
        else
        {
            ((ASPxGridView)(sender)).JSProperties["cpSucesso"] = "Raia excluída com sucesso!";
        }
    }
}