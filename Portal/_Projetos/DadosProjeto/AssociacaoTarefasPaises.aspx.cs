using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_AssociacaoTarefasPaises : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int codigoProjeto = -1;
    private string codigoCronogramaProjeto = "";

    private string resolucaoCliente;

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

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

        definirVariaveisGlobais();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
        HeaderOnTela();

        carregaGvDados();
              
        defineAlturaTela(resolucaoCliente);

        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") + "" == "Editar")
            carregaListaPaises(getChavePrimaria());
        else
            carregaListaPaises("-1");
        gvPaises.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void carregaListaPaises(string codigoTarefa)
    {

        string comandoSQL = string.Format(@"
        SELECT P.[CodigoPais],
               P.[NomePais],
              
    CASE WHEN  (SELECT tcPais.CodigoTarefa FROM TarefaCronogramaPais tcPais
                WHERE tcPais.CodigoCronogramaProjeto = '{0}' 
                  AND tcPais.CodigoPais = P.[CodigoPais] 
                  AND tcPais.CodigoTarefa = {1}) IS NULL THEN 'N' ELSE 'S' END AS Selecionado,
   
    CASE WHEN  (SELECT tcPais.CodigoTarefa FROM TarefaCronogramaPais tcPais
                WHERE tcPais.CodigoCronogramaProjeto = '{0}' 
                  AND tcPais.CodigoPais = P.[CodigoPais] 
                  AND tcPais.CodigoTarefa = {1}) IS NULL THEN '1' ELSE '0' END AS ColunaAgrupamento
                 FROM [dbo].[Pais] P ",codigoCronogramaProjeto ,codigoTarefa);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvPaises.DataSource = ds;
            gvPaises.DataBind();
        }
        
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AssociacaoTarefasPaises.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "AssociacaoTarefasPaises"));
    }

    private void definirVariaveisGlobais()
    {
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        this.Title = cDados.getNomeSistema();

        int.TryParse(Request.QueryString["IDProjeto"], out codigoProjeto);

        string comandoSQL = string.Format(@"SELECT CodigoCronogramaProjeto 
                                            FROM CronogramaProjeto  
                                           WHERE CodigoProjeto = {0}", codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }
    }

    
    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
          select gtc.SequenciaTarefaCronograma,
                 gtc.CodigoTarefa, 
                 gtc.NomeTarefa,
                 (SELECT COUNT(*) FROM TarefaCronogramaPais tcpp
				                  WHERE tcpp.CodigoCronogramaProjeto = '{0}' 
                                    AND tcpp.CodigoTarefa = gtc.CodigoTarefa ) as Quantidade 
                  FROM [dbo].[f_tasq_GetTarefasCronograma] ('{0}') as gtc WHERE gtc.IndicaTarefaResumo = 'N' ", codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 190;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";


        if (e.Parameters == "Editar")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = Resources.traducao.AssociacaoTarefasPaises_tarefa_associada_com_sucesso_;
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (mensagemErro_Persistencia != "")
        {// alguma coisa deu errado...
            ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
            if (e.Parameters != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }
    
    private string persisteEdicaoRegistro()
    {
        string[] arrayCodigosChecados = getArrayCamposChecadosDaGrid(gvPaises, "CodigoPais");

        string mensagemErro = "";
        string comandoSQL = "";

        comandoSQL = string.Format(@" DELETE FROM  [dbo].[TarefaCronogramaPais]
                                       WHERE [CodigoCronogramaProjeto] = '{0}' 
                                         AND [CodigoTarefa] = {1} {2}", codigoCronogramaProjeto, getChavePrimaria(), Environment.NewLine);

        foreach(string item in arrayCodigosChecados)
        {
            comandoSQL += string.Format(@" INSERT INTO [dbo].[TarefaCronogramaPais]
                                          ([CodigoCronogramaProjeto],[CodigoTarefa],[CodigoPais])
                                          VALUES('{0}', {1}, {2}) {3}", codigoCronogramaProjeto, getChavePrimaria(), item, Environment.NewLine);
        }


        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = ds.Tables[0].Rows[0][0].ToString().Trim().ToLower();

            if (mensagemErro == "ok")
            {
                mensagemErro = "";
            }
        }
        return mensagemErro;
    }

    private string[] getArrayCamposChecadosDaGrid(ASPxGridView GridX, string colunaChavePrimaria)
    {
        string[] arrayCodigosChecados = new string[GridX.GetSelectedFieldValues(colunaChavePrimaria).Count];

        for (int i = 0; i < arrayCodigosChecados.Length; i++)
        {
            arrayCodigosChecados[i] = GridX.GetSelectedFieldValues(colunaChavePrimaria)[i].ToString();
        }
        return arrayCodigosChecados;
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", false, true, false, "ASSOCIAPAIS", "Associaçãao Tarefas e Países", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ASSOCIAPAIS");
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
        carregaGvDados();
    }

    protected void gvPaises_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        ((ASPxGridView)sender).ExpandAll();
        ((ASPxGridView)sender).Selection.UnselectAll();

        for (int i = 0; i < ((ASPxGridView)sender).VisibleRowCount; i++)
        {
            if (((ASPxGridView)sender).GetRowValues(i, "Selecionado").ToString() == "S")
                ((ASPxGridView)sender).Selection.SelectRow(i);
        }

    }
}