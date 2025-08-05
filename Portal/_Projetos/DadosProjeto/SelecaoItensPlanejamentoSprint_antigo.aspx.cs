using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_DadosProjeto_SelecaoItensPlanejamentoSprint_antigo : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade;
    int codigoProjeto;
    bool indicaPublicado;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);
        string connectionString = cDados.classeDados.getStringConexao();
        
        codigoProjeto = int.Parse(Request.QueryString["CP"]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
      
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        carregaGrid();
        verificaPublicacao();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT ib.CodigoItem,
               ib.TituloItem,
               ib.DetalheItem,
               ib.Importancia,
               ib.EsforcoPrevisto,
               CASE WHEN ib.Complexidade = 0 THEN 'Baixa'
                        WHEN ib.Complexidade = 1 THEN 'Média'
                        WHEN ib.Complexidade = 2 THEN 'Alta'
                        WHEN ib.Complexidade = 3 THEN 'Muito Alta' ELSE '' END AS DescricaoComplexidade
          FROM Agil_ItemBacklog AS ib INNER JOIN
               Agil_TipoStatusItemBacklog AS tsi ON (tsi.CodigoTipoStatusItem = ib.CodigoTipoStatusItem) INNER JOIN
               LinkProjeto AS lp ON (lp.CodigoProjetoFilho = {0}
                                 AND lp.CodigoProjetoPai = ib.CodigoProjeto)                                         
         WHERE ib.CodigoItemSuperior IS NULL
           AND ib.IndicaItemNaoPlanejado = 'N'
           AND (tsi.IniciaisTipoStatusItemControladoSistema = ('AG_IMPL'))
           AND ib.CodigoIteracao IS NULL
         ORDER BY Importancia DESC, TituloItem
            ", codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void gvDados_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            string comandoSQL = string.Format(@"
            UPDATE Agil_ItemBacklog 
               SET EsforcoPrevisto = {1}
             WHERE CodigoItem = {0}"
            , e.UpdateValues[i].Keys[0]
            , e.UpdateValues[i].NewValues[0].ToString().Replace(",", "."));

            int regAf = 0;

            bool result = cDados.execSQL(comandoSQL, ref regAf);
        }

        carregaGrid();
        e.Handled = true;
        
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string comandoSQL = "";

        /*
         		    SELECT @CodigoStatusItem = CodigoTipoStatusItem
		      FROM Agil_TipoStatusItemBacklog
		     WHERE IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI'
		     
			UPDATE Agil_ItemBacklog
			   SET IndicaBloqueioItem = 'S',
			       CodigoTipoStatusItem = @CodigoStatusItem
			 WHERE CodigoIteracao = @ins_CodigoIteracao
         
         */

        foreach (object item in gvDados.GetSelectedFieldValues("CodigoItem"))
        {
            if (!indicaPublicado)
            {

                comandoSQL += string.Format(@"                
                    UPDATE Agil_ItemBacklog SET CodigoIteracao = @CodigoIteracao,
                      CodigoTipoStatusItem = (SELECT CodigoTipoStatusItem
		                                       FROM Agil_TipoStatusItemBacklog
		                                      WHERE IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI')
                     WHERE CodigoItem = {0}
                ", item);
            }
            else
            {
                comandoSQL += string.Format(@"                
                    UPDATE Agil_ItemBacklog SET CodigoIteracao = @CodigoIteracao,
                      CodigoTipoStatusItem = (SELECT CodigoTipoStatusItem
		                                       FROM Agil_TipoStatusItemBacklog
		                                      WHERE IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI'),
                      IndicaItemNaoPlanejado = 'S'
                     WHERE CodigoItem = {0}
                ", item);

            }
            
        }

        if (comandoSQL != "")
        {
            comandoSQL = string.Format(@"
                BEGIN
                    DECLARE @CodigoIteracao Int
	
	                SELECT @CodigoIteracao = CodigoIteracao
		                FROM Agil_Iteracao
	                    WHERE CodigoProjetoIteracao = {0}
                
                   {1}


END 
                ", codigoProjeto
                 , comandoSQL);

            int regAf = 0;

            bool result = cDados.execSQL(comandoSQL, ref regAf);

            carregaGrid();
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "DetalheItem" && e.CellValue != null && e.CellValue.ToString().Length > 280)
        {
            e.Cell.ToolTip = e.CellValue.ToString();
            e.Cell.Text = e.CellValue.ToString().Substring(0, 279) + "...";           
        }
    }

    private void verificaPublicacao()
    {
        string comandoSQL = string.Format(@"
            SELECT a.DataPublicacaoPlanejamento
              FROM Agil_Iteracao AS a
             WHERE a.CodigoProjetoIteracao = {0}", Request.QueryString["CP"]);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            indicaPublicado = dr["DataPublicacaoPlanejamento"].ToString() != "";
        }
        //indicaPublicado = false;
    }
}