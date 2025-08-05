using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_CadastroFaixasTolerancia : System.Web.UI.Page
{
    #region Fields

    private dados cDados;
    private Int32 idUsuarioLogado;
    private Int32 codigoEntidade;
    private Int32 alturaPrincipal;
    private String resolucaoCliente;
    private String dbName;
    private String dbOwner;

    #endregion

    #region Event Handlers

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

        dsDados.ConnectionString = cDados.classeDados.getStringConexao();
        dsStatus.ConnectionString = cDados.classeDados.getStringConexao();      

        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        setPermissoesTela();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_CadFxaTlr");

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        cDados.aplicaEstiloVisual(Page);
        gvDados.DataBind();
        //carregaGvDados();               
        this.Title = cDados.getNomeSistema();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadFaixTol");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "CadFaixTol", lblTituloTela.Text, this);
    }

    #endregion

    #endregion

    #region Methods

    private void setPermissoesTela()
    {

    }

    private void carregaGvDados()
    {
        String comandoSql = String.Format(@"
 SELECT p.CodigoParametro,
		p.TipoIndicador,
		CASE p.TipoIndicador
			WHEN 'FIS' THEN 'Físico'
			WHEN 'FIN' THEN 'Financeiro'
		END AS DescricaoIndicador,
		p.CodigoTipoStatus,
		p.ValorInicial,
		p.ValorFinal,
        tsa.TipoStatus
   FROM {0}.{1}.ParametroIndicadores p INNER JOIN
        {0}.{1}.TipoStatusAnalise tsa ON tsa.CodigoTipoStatus = p.CodigoTipoStatus
  WHERE p.TipoIndicador IN ('FIS', 'FIN')
  ORDER BY
        p.TipoIndicador,
        p.ValorInicial", dbName, dbOwner);

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        //Calcula a altura da tela
        int largura1 = 0;
        int altura1 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);
        
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 125;
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Cadastro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroFaixasTolerancia.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CadastroFaixasTolerancia", "Cadastro"));
    } 

    #endregion


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
}