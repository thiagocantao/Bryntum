using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_DadosPlano_PlanoObjetivoEstrategico : System.Web.UI.Page
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

        int codigoPlano = int.Parse(Request.QueryString["CP"]);
        podeEditar = false;
        if (cDados.podeEditarPPA(codigoPlano, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel))
        {
            podeEditar = true;
        }

        Session["ce"] = codigoEntidadeUsuarioResponsavel;
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
        gvDados.Settings.ShowFilterRow = false;
        carregaGrid();
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
       SELECT poe.CodigoPlano, oe.CodigoObjetoEstrategia, oe.DescricaoObjetoEstrategia AS ObjetivoEstrategico,
      oe.DescricaoObjetoEstrategia AS ObjetivoEstrategico_txt,
       oeSup.CodigoObjetoEstrategia as CodigoObjetivoEstrategicoPlanoConsolidador, oeSup.DescricaoObjetoEstrategia AS ObjetivoEstrategicoPlanoConsolidador,
oeSup.DescricaoObjetoEstrategia AS ObjetivoEstrategicoPlanoConsolidador_txt
  FROM PlanoObjetivoEstrategico AS poe INNER JOIN
       ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = poe.CodigoObjetivoEstrategico) LEFT JOIN
       ObjetoEstrategia AS oeSup ON (oeSup.CodigoObjetoEstrategia = poe.CodigoObjetivoEstrategicoSuperior)
 WHERE poe.CodigoPlano = {0}
   AND oe.DataExclusao IS NULL
   AND oeSup.DataExclusao IS NULL    
 ORDER BY oe.DescricaoObjetoEstrategia", Request.QueryString["CP"]);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "gvDados.AddNewRow();", true, true, false, "PpaObj", "PPA - Planos e Objetivos Estratégicos", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PpaObj");
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

 

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ObjetivoEstrategico")
        {
            string comandoSQL = string.Format(@"
                SELECT oe.CodigoObjetoEstrategia AS CodigoObjetivo,
       oe.DescricaoObjetoEstrategia AS TituloObjetivo
  FROM ObjetoEstrategia AS oe INNER JOIN
       TipoObjetoEstrategia AS toe ON (oe.CodigoTipoObjetoEstrategia = toe.CodigoTipoObjetoEstrategia
                                   AND toe.IniciaisTipoObjeto = 'OBJ') INNER JOIN
       MapaEstrategico AS me ON (me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
                             AND me.CodigoUnidadeNegocio = {0})                            
 WHERE oe.DataExclusao IS NULL
 ORDER BY oe.DescricaoObjetoEstrategia
   ", codigoEntidadeUsuarioResponsavel);
           
            DataSet dsObjetivos = cDados.getDataSet(comandoSQL);
            (e.Editor as ASPxComboBox).DataSource = dsObjetivos;
            (e.Editor as ASPxComboBox).TextField = "TituloObjetivo";
            (e.Editor as ASPxComboBox).ValueField = "CodigoObjetivo";
            (e.Editor as ASPxComboBox).DataBind();

        }
        else if(e.Column.FieldName == "ObjetivoEstrategicoPlanoConsolidador")
        {
            string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @CodigoUnidadePlanoConsolidador INT
                DECLARE @CodigoPlanoConsolidador INT
                DECLARE @CodigoPlano INT

                SET @CodigoPlano = {0}            

                SELECT @CodigoPlanoConsolidador = pln.CodigoPlanoSuperior
                  FROM Plano AS pln
                 WHERE pln.CodigoPlano = @CodigoPlano-- Substituir pelo código do plano atual!
   
               SELECT @CodigoUnidadePlanoConsolidador = plnSup.CodigoUnidadeNegocio
                  FROM Plano AS plnSup
                 WHERE plnSup.CodigoPlano = @CodigoPlanoConsolidador

                SELECT oe.CodigoObjetoEstrategia AS CodigoObjetivo,
                       oe.DescricaoObjetoEstrategia AS TituloObjetivo
                  FROM ObjetoEstrategia AS oe 
                  INNER JOIN TipoObjetoEstrategia AS toe ON(oe.CodigoTipoObjetoEstrategia = toe.CodigoTipoObjetoEstrategia
                                               AND toe.IniciaisTipoObjeto = 'OBJ') INNER JOIN
                       MapaEstrategico AS me ON (me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
                                         AND me.CodigoUnidadeNegocio = @CodigoUnidadePlanoConsolidador)                            
                 WHERE oe.DataExclusao IS NULL
                 ORDER BY oe.DescricaoObjetoEstrategia
           END", Request.QueryString["CP"]);
            DataSet dsObjetivos = cDados.getDataSet(comandoSQL);
            (e.Editor as ASPxComboBox).DataSource = dsObjetivos;
            (e.Editor as ASPxComboBox).TextField = "TituloObjetivo";
            (e.Editor as ASPxComboBox).ValueField = "CodigoObjetivo";
            (e.Editor as ASPxComboBox).DataBind();

        }
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_Erros"] = "";
        
        //CodigoPlano;CodigoObjetoEstrategia;CodigoObjetivoEstrategicoPlanoConsolidador
        int regAfetados = 0;
        bool retorno;
        string CodigoPlano = Request.QueryString["CP"];
        string ObjetivoEstrategico = e.NewValues["ObjetivoEstrategico"].ToString();
        string ObjetivoEstrategicoPlanoConsolidador = (e.NewValues["ObjetivoEstrategicoPlanoConsolidador"] == null) ? "NULL" : e.NewValues["ObjetivoEstrategicoPlanoConsolidador"].ToString();

        string comandoSQL = string.Format(@"
declare @CodigoPlano int
           ,@CodigoObjetivoEstrategico int
           ,@CodigoObjetivoEstrategicoSuperior int

set @CodigoPlano = {0}
set @CodigoObjetivoEstrategico = {1}
set @CodigoObjetivoEstrategicoSuperior = {2}

INSERT INTO [PlanoObjetivoEstrategico]
           ([CodigoPlano]
           ,[CodigoObjetivoEstrategico]
           ,[CodigoObjetivoEstrategicoSuperior])
     VALUES
           (@CodigoPlano
           ,@CodigoObjetivoEstrategico
           ,@CodigoObjetivoEstrategicoSuperior)", CodigoPlano, ObjetivoEstrategico, ObjetivoEstrategicoPlanoConsolidador);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);

        }
        catch(Exception ex)
        {
            string mensagem = ex.Message;
            if (mensagem.Contains("PRIMARY KEY"))
            {
                mensagem = "Este registro já está cadastrado.";
            }


            ((ASPxGridView)sender).JSProperties["cp_Erros"] = mensagem;
        }

        carregaGrid();
        e.Cancel = true;
        gvDados.CancelEdit();

    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_Erros"] = "";
        int regAfetados = 0;
        bool retorno;
        string CodigoPlano = e.Keys[0].ToString();
        string CodigoObjetoEstrategia = e.Keys[1].ToString();
        string CodigoObjetivoEstrategicoPlanoConsolidador = string.IsNullOrEmpty(e.Keys[2].ToString()) ? "NULL" : e.Keys[2].ToString();

        string mensagem = "";

        string comandoSQL = string.Format(@"
        declare @CodigoPlano int
           ,@CodigoObjetivoEstrategico int
           ,@CodigoObjetivoEstrategicoSuperior int

        set @CodigoPlano = {0}
        set @CodigoObjetivoEstrategico = {1}
        --set @CodigoObjetivoEstrategicoSuperior = 

        DELETE FROM [PlanoObjetivoEstrategico]
         WHERE CodigoPlano = @CodigoPlano
            AND CodigoObjetivoEstrategico = @CodigoObjetivoEstrategico", CodigoPlano, CodigoObjetoEstrategia);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (regAfetados == 0)
            {
                mensagem = "Nenhum registro foi excluído";
                ((ASPxGridView)sender).JSProperties["cp_Erros"] = mensagem;
            }
        }
        catch(Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_Erros"] = ex.Message;
        }
        
        
        carregaGrid();
        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }

}
