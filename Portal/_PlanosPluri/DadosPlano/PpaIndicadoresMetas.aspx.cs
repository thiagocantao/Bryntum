using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_DadosPlano_PpaIndicadoresMetas : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    public bool podeEditar = false;

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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


        int codigoPlano = -1;

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoPlano = int.Parse(Request.QueryString["CP"].ToString());

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_EdtPlaPlu") && cDados.podeEditarPPA(codigoPlano, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel))
        {
            podeEditar = true;
        }

        sdsPlanoIndicador.ConnectionString = cDados.classeDados.getStringConexao();
        Session["ce"] = codigoEntidadeUsuarioResponsavel;
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);

        carregaGrid();
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
       SELECT CASE WHEN pi.CodigoIndicador IS NULL THEN 'N' ELSE 'S' END AS Selecionado, i.[CodigoIndicador],i.NomeIndicador, iu.Meta
         FROM [Indicador] AS i 
         INNER JOIN [IndicadorUnidade] AS iu ON (iu.CodigoIndicador = i.CodigoIndicador) 
          LEFT JOIN PlanoIndicador pi on pi.CodigoIndicador = i.CodigoIndicador 
         WHERE i.[DataExclusao] is null
           AND iu.CodigoUnidadeNegocio = {0}
           ORDER BY 3", codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();

        if(!IsPostBack )
        {
            gvDados.Selection.UnselectAll();
            int i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Selecionado"].ToString().ToUpper().Trim() == "S")
                    gvDados.Selection.SelectRow(i);
                i++;
            }
        }        
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        int codigoPlano = int.Parse(Request.QueryString["CP"]);
        podeEditar = false;
        if (cDados.podeEditarPPA(codigoPlano, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel))
        {
            podeEditar = true;
        }
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "gvDados.AddNewRow();", false, true, false, "PpaInd", "PPA - Indicadores e Metas", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PpaInd");
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Salvar")
        {
            bool retorno = false;
            int regafetados = 0;
            string comandosql = string.Format(@"DELETE FROM [PlanoIndicador]
      WHERE(CodigoPlano = {0})", Request.QueryString["CP"]);
           //bool retorno= cDados.execSQL(comandosql, ref regafetados);

            List<object> arrayIndicadores = ((ASPxGridView)sender).GetSelectedFieldValues(gvDados.KeyFieldName);
            for (int i = 0; i < arrayIndicadores.Count; i++)
            {

                comandosql += Environment.NewLine + string.Format(@" INSERT INTO [PlanoIndicador]
           ([CodigoPlano]
           ,[CodigoIndicador])
     VALUES
           ({0}
           , {1})", Request.QueryString["CP"], arrayIndicadores[i].ToString());

            }
            retorno = cDados.execSQL(comandosql, ref regafetados);
        }
    }
}
