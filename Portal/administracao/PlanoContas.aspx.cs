using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_PlanoContas : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string alturaTabela = "";
    public bool passouNoHtmlRowPrepared = false;


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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadPlnCta1");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);


        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        cDados.aplicaEstiloVisual(Page);

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        carregaGvDados();
        carregaDllContaSuperior();
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void carregaGvDados()
    {
        DataSet ds = getPlanoContasFluxoCaixa(codigoEntidadeUsuarioResponsavel, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private void carregaDllContaSuperior()
    {

        DataSet ds = getDllPlanoContasSuperior("");
        ListEditItem itemNenhum = new ListEditItem(Resources.traducao.nenhum, "-1");
        ddlContaSuperior.Items.Clear();
        if ((cDados.DataSetOk(ds)))
        {
            ddlContaSuperior.TextField = "DescricaoConta";
            ddlContaSuperior.ValueField = "CodigoConta";
            ddlContaSuperior.DataSource = ds;
            ddlContaSuperior.DataBind();
            ddlContaSuperior.Items.Insert(0, itemNenhum);

            if (ddlContaSuperior.SelectedIndex == -1)
            {
                ddlContaSuperior.SelectedIndex = 0;
            }
        }

    }

    private DataSet getDllPlanoContasSuperior(string where)
    {
        string comandoSQL1 = string.Format(
        @" SELECT [CodigoConta]
                 ,[DescricaoConta]                
            FROM {0}.{1}.[PlanoContasFluxoCaixa] 
where CodigoEntidade = {2} and CodigoContaSuperior is null
order by DescricaoConta", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);
        return cDados.getDataSet(comandoSQL1);
    }


    public DataSet getPlanoContasFluxoCaixa(int codigoEntidade, string where)
    {

        string comandoSQL1 = string.Format(
        @" SELECT pc.[CodigoConta]
                 ,pc.[DescricaoConta]
                 ,pc.[EntradaSaida]
                 ,pc.[CodigoContaSuperior]
                 ,pcs.[DescricaoConta] as DescricaoContaSuperior
                 ,pc.[TipoConta]
                 ,pc.[CodigoEntidade]
                 ,pc.[IndicaContaAnalitica]
                 ,pc.[CodigoReservadoGrupoConta]
            FROM {0}.{1}.[PlanoContasFluxoCaixa] pc left JOIN
                 {0}.{1}.PlanoContasFluxoCaixa pcs ON pcs.CodigoConta = pc.CodigoContaSuperior
where pc.CodigoEntidade = {2}
order by pcs.DescricaoConta, pc.DescricaoConta", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);
        return cDados.getDataSet(comandoSQL1);

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 235);

        gvDados.Settings.VerticalScrollableHeight = altura - 265;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/planoContas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "planoContas", "_Strings"));
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            //carregaUnidadeSuperior("IncluirNovo");
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            //carregaUnidadeSuperior("");
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

        carregaGvDados();
    }

    private string persisteExclusaoRegistro()
    {
        string mensagemErro = "";
        string comandoSQL = "";
        string CodigoConta = getChavePrimaria();

        comandoSQL = string.Format(@" DELETE FROM {0}.{1}.[PlanoContasFluxoCaixa]
      WHERE CodigoConta = {2}", cDados.getDbName(), cDados.getDbOwner(), CodigoConta);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + comandoSQL + cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = ds.Tables[0].Rows[0][0].ToString();
            if (mensagemErro == "OK")
            {
                mensagemErro = "";
            }
        }
        return mensagemErro;
    }

    private string persisteEdicaoRegistro()
    {
        string mensagemErro = "";
        string comandoSQL = "";

        string DescricaoConta = txtDescricaoConta.Text.Replace("'", "'+char(39)+'");
        string EntradaSaida = rblEntradaSaida.Value.ToString();
        string CodigoContaSuperior = (ddlContaSuperior.Value != null) ? ddlContaSuperior.Value.ToString() : "-1";
        string TipoConta = ddlTipoConta.Value.ToString();
        string CodigoEntidade = codigoEntidadeUsuarioResponsavel.ToString();
        string IndicaContaAnalitica = ckbContaAnalitica.Value.ToString();
        string CodigoReservadoGrupoConta = txtCodigoReservado.Text;
        string CodigoConta = getChavePrimaria();

        comandoSQL = string.Format(@"

UPDATE {0}.{1}.[PlanoContasFluxoCaixa]
   SET [DescricaoConta] = '{2}'
      ,[EntradaSaida] = '{3}'
      ,[CodigoContaSuperior] = {4}
      ,[TipoConta] = '{5}'
      ,[CodigoEntidade] = {6}
      ,[IndicaContaAnalitica] = '{7}'
      ,[CodigoReservadoGrupoConta] = '{8}'
 WHERE CodigoConta = {9}
", cDados.getDbName(), cDados.getDbOwner(), DescricaoConta, EntradaSaida, CodigoContaSuperior, TipoConta, CodigoEntidade, IndicaContaAnalitica, CodigoReservadoGrupoConta, CodigoConta);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + comandoSQL + cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = ds.Tables[0].Rows[0][0].ToString();
            if (mensagemErro == "OK")
            {
                mensagemErro = "";
            }
        }
        return mensagemErro;
    }

    private string persisteInclusaoRegistro()
    {
        string mensagemErro = "";
        string comandoSQL = "";

        string DescricaoConta = txtDescricaoConta.Text.Replace("'", "'+char(39)+'");
        string EntradaSaida = rblEntradaSaida.Value.ToString();
        string CodigoContaSuperior = (ddlContaSuperior.Value != null) ? ddlContaSuperior.Value.ToString() : "-1";
        string TipoConta = ddlTipoConta.Value.ToString();
        string CodigoEntidade = codigoEntidadeUsuarioResponsavel.ToString();
        string IndicaContaAnalitica = ckbContaAnalitica.Value.ToString();
        string CodigoReservadoGrupoConta = txtCodigoReservado.Text;


        comandoSQL = string.Format(@"
INSERT INTO {0}.{1}.[PlanoContasFluxoCaixa]
           ([DescricaoConta], [EntradaSaida], [CodigoContaSuperior], [TipoConta],[CodigoEntidade], [IndicaContaAnalitica], [CodigoReservadoGrupoConta])
     VALUES(           '{2}',          '{3}',                   {4},       '{5}',            {6} ,                  '{7}', '{8}')
",cDados.getDbName(), cDados.getDbOwner(), DescricaoConta, EntradaSaida, CodigoContaSuperior, TipoConta, CodigoEntidade, IndicaContaAnalitica, CodigoReservadoGrupoConta);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + comandoSQL + cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = ds.Tables[0].Rows[0][0].ToString();
            if (mensagemErro == "OK")
            {
                mensagemErro = "";
            }
        }
        return mensagemErro;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClick_CustomIncluirConta();", true, true, false, "EN_CadPlnCta1", "Centro de Custo", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "EN_CadPlnCta1");
    }

    #endregion



    #region BANCO DE DADOS.

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }
    #endregion
    protected void gvDados_AfterPerformCallback1(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    protected void ddlTipoConta_Callback(object sender, CallbackEventArgsBase e)
    {

    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
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

    protected void callbackDll_Callback(object sender, CallbackEventArgsBase e)
    {
        ListEditItem liRC = new ListEditItem("Receita", "RC");
        ListEditItem liIV = new ListEditItem("Investimento", "IV");
        ListEditItem liCF = new ListEditItem("Custo Fixo", "CF");
        ListEditItem liCV = new ListEditItem("Custo Variável", "CV");

        ddlTipoConta.Items.Clear();

        int indice = int.Parse(e.Parameter);

        if (indice == 0)
        {
            ddlTipoConta.Items.Add(liRC);
        }
        else
        {
            ddlTipoConta.Items.Add(liIV);
            ddlTipoConta.Items.Add(liCF);
            ddlTipoConta.Items.Add(liCV);
        }
    }
}