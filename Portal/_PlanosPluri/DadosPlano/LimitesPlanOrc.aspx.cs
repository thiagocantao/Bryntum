using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_DadosPlano_LimitesPlanOrc : System.Web.UI.Page
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

        HeaderOnTela();

        gvDados.Settings.ShowFilterRow = false;
        carregaGrid();
        populaDdlLimite();
        pnUnidadeMedida.SettingsLoadingPanel.Enabled = false;
    }

    protected void populaDdlLimite()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoLimite]
              ,[NomeLimite]
          FROM [Limite]
         WHERE TipoLimite = '{0}'  
           AND getdate() BETWEEN  ISNULL(InicioValidade, CAST('08/08/1900' AS DATE)) AND 
                                            ISNULL(TerminoValidade, CAST('08/08/2800' AS DATE)) 
order by 2", Request.QueryString["TL"]);
        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlLimites.TextField = "NomeLimite";
        ddlLimites.ValueField = "CodigoLimite";
        ddlLimites.DataSource = ds;
        ddlLimites.DataBind();

    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/LimitesPlanoOrc.js""></script>"));
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
SELECT pl.[CodigoPlano]
      ,pl.[CodigoLimite]
      ,l.NomeLimite
      ,tum.SiglaUnidadeMedida 
      ,tum.CodigoUnidadeMedida 
      ,pl.[ValorMinimo]
      ,pl.[ValorMaximo]
  FROM [PlanoLimite] pl
inner join Limite l on l.CodigoLimite = pl.CodigoLimite
inner join TipoUnidadeMedida tum on (tum.CodigoUnidadeMedida = l.CodigoUnidadeMedida)
where l.TipoLimite = '{1}'
  and pl.CodigoPlano = {0}
order by 3 asc", Request.QueryString["CP"], Request.QueryString["TL"]);

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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "LimPlo", "Limites", this);

    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LimPlo");
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string msg = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameters.ToString() == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            msg = "Limite incluído com sucesso!";
        }
        if (e.Parameters.ToString() == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            msg = "Limite alterado com sucesso!";
        }
        if (e.Parameters.ToString() == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            msg = "Limite excluído com sucesso!";
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            gvDados.JSProperties["cp_Erro"] = ""; // 1 indica que foi salvo com sucesso.
            gvDados.JSProperties["cp_Sucesso"] = msg;
        }
        else // alguma coisa deu errado...
        {
            gvDados.JSProperties["cp_Erro"] = mensagemErro_Persistencia; // 1 indica que foi salvo com sucesso.
            gvDados.JSProperties["cp_Sucesso"] = "";
        }
    }

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        int regAfetados = 0;

        string chave = getChavePrimaria();

        string[] chaves = chave.Split('|');
        string codigoplano = chaves[0];
        string codigoLimite = chaves[1];


        string comandoSQL = string.Format(@"
        DELETE FROM [PlanoLimite]
         WHERE CodigoPlano = {0} 
           AND CodigoLimite = {1}", codigoplano, codigoLimite);
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch(Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
        
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";

        int regAfetados = 0;

        string chave = getChavePrimaria();

        string[] chaves = chave.Split('|');
        string codigoplano = chaves[0];
        string codigoLimite = chaves[1];

        string trataValorMinimo = (spnValorMinimo.Value == null) ? "NULL" : spnValorMinimo.Value.ToString().Replace(",",".");
        string trataValorMaximo = (spnValorMaximo.Value == null) ? "NULL" : spnValorMaximo.Value.ToString().Replace(",", ".");


        string comandoSQL = string.Format(@"
declare  @ValorMinimo decimal(25,4)
declare  @ValorMaximo decimal(25,4)
declare  @CodigoPlano int
 declare  @CodigoLimite int
    
set  @ValorMinimo = {0}
set  @ValorMaximo = {1}
set  @CodigoPlano = {2}
 set  @CodigoLimite = {3} 
UPDATE [PlanoLimite]
   SET 
      [ValorMinimo] = @ValorMinimo
      ,[ValorMaximo] = @ValorMaximo
      ,[CodigoLimite] = @CodigoLimite
  WHERE [CodigoPlano] = @CodigoPlano and CodigoLimite = @CodigoLimite", trataValorMinimo, trataValorMaximo, codigoplano, codigoLimite);
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
            if (ex.Message.Contains("PRIMARY KEY"))
            {
                retorno = "Já existe outro registro de Limite com esta mesma descrição! Escolha outro limite.";
            }            
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
 

        string retorno = "";

        int regAfetados = 0;

        string trataValorMinimo = (spnValorMinimo.Value == null) ? "NULL" : spnValorMinimo.Value.ToString().Replace(",", ".");
        string trataValorMaximo = (spnValorMaximo.Value == null) ? "NULL" : spnValorMaximo.Value.ToString().Replace(",", "."); 



        string comandoSQL = string.Format(@"
declare  @ValorMinimo decimal(25,4)
declare  @ValorMaximo decimal(25,4)
declare  @CodigoPlano int
 declare  @CodigoLimite int
    
set  @ValorMinimo = {0}
set  @ValorMaximo = {1}
set  @CodigoPlano = {2}
 set  @CodigoLimite = {3} 
INSERT INTO [PlanoLimite]
           ([CodigoPlano]
           ,[CodigoLimite]
           ,[ValorMinimo]
           ,[ValorMaximo])
     VALUES
           (@CodigoPlano
           ,@CodigoLimite
           ,@ValorMinimo 
           ,@ValorMaximo)", trataValorMinimo, trataValorMaximo, Request.QueryString["CP"], ddlLimites.Value);
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
            if (ex.Message.Contains("PRIMARY KEY"))
            {
                retorno = "Já existe outro registro de Limite com esta mesma descrição! Escolha outro limite.";
            }
        }
        return retorno;
    }

    protected void pnUnidadeMedida_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoLimite = "";
        string TipoOperacao = "";
        string auxiliar = e.Parameter;
        string[] parametros = auxiliar.Split('|');

        codigoLimite = parametros[0];
        TipoOperacao = parametros[1];

        string comandoSQL = string.Format(@"
            SELECT tum.SiglaUnidadeMedida
              FROM Limite l INNER JOIN
			       TipoUnidadeMedida tum ON tum.CodigoUnidadeMedida = l.CodigoUnidadeMedida
             WHERE l.CodigoLimite = {0}", codigoLimite);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtUnidade.Text = ds.Tables[0].Rows[0]["SiglaUnidadeMedida"].ToString();
        }
        else
        {
            txtUnidade.Text = "";
        }

        txtUnidade.ClientEnabled = false;
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

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }
}
