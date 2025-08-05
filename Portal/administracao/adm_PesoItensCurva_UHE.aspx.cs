using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_adm_PesoItensCurva_UHE : System.Web.UI.Page
{

    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;

    private string dbName;
    private string dbOwner;


    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool emailAlterado = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);


        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        hfGeral.Set("CodigoEntidadeAtual", CodigoEntidade); // p/ controle de edição dos usuários listados
        hfGeral.Set("idUsuarioLogado", idUsuarioLogado);

        podeIncluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_Cad");
        podeEditar = podeIncluir;
        podeExcluir = podeIncluir;

        this.Title = cDados.getNomeSistema();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        cDados.aplicaEstiloVisual(Page);
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        carregaGrid();
        carregaComboProjeto();
        //carregaComboCategoria(-1);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    
    }
    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.VerticalScrollableHeight = altura - 230;
    }
    private void carregaComboProjeto()
    {
        string comandoSQL = string.Format(@"SELECT codigoProjeto, nomeProjeto FROM Projeto
                                 WHERE dataExclusao IS NULL
                                 AND codigoStatusProjeto = 3
                                 ORDER BY nomeProjeto");
      DataSet ds=  cDados.getDataSet(comandoSQL);

        ddlProjeto.ValueField = "CodigoProjeto";
        ddlProjeto.TextField = "NomeProjeto";
        ddlProjeto.DataSource = ds.Tables[0];
        ddlProjeto.DataBind();

    }

    private void carregaComboCategoria(int codigoProjeto)
    {
        string comandoSQL = string.Format(@"SELECT CodigoCategoria, DescricaoCategoria FROM {0}.{1}.Categoria
                                             WHERE CodigoCategoria NOT IN(SELECT CodigoCategoria 
                                                                            FROM {0}.{1}.UHE_PesoItensCurvaS 
                                                                           WHERE CodigoProjeto = {2})
                                            ORDER BY descricaoCategoria", dbName, dbOwner, codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlCategoria.ValueField = "CodigoCategoria";
        ddlCategoria.TextField = "DescricaoCategoria";
        ddlCategoria.DataSource = ds.Tables[0];
        ddlCategoria.DataBind();
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT 
            pics.CodigoCategoria, 
            pics.CodigoProjeto, 
            pics.Peso, 
            p.NomeProjeto, 
            ca.DescricaoCategoria 
          FROM {0}.{1}.UHE_PesoItensCurvaS pics
    INNER JOIN {0}.{1}.Projeto p on (p.CodigoProjeto = pics.CodigoProjeto)
    INNER JOIN {0}.{1}.Categoria ca on (ca.CodigoCategoria = pics.CodigoCategoria)",dbName, dbOwner);


        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();


    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_PesoItensCurva_UHE.js""></script>"));
    }
    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";


        ((ASPxGridView)sender).JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "OK") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            ((ASPxGridView)sender).JSProperties["cp_OperacaoOk"] = e.Parameters;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            ((ASPxGridView)sender).JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
        }

        carregaGrid();
        
    }

    private string getChavePrimaria()
    {
        string retorno = "";
        if (gvDados.FocusedRowIndex >= 0)
        {
            retorno = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(retorno);
        }
        else
        {
            retorno = "";
        }
        return retorno;
    }

    private string persisteExclusaoRegistro()
    {
        string mensagemErro = "";
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();

        string codigoComposto = getChavePrimaria();

        string[] chaves = codigoComposto.Split('|');
        string chaveCodigoProjeto = chaves[0];
        string chaveCodigoCategoria = chaves[1];

        string comandosql = string.Format(@"
       DELETE FROM {0}.{1}.[UHE_PesoItensCurvaS]
      WHERE CodigoProjeto = {2} and CodigoCategoria = {3}", cDados.getDbName(), cDados.getDbOwner(), chaveCodigoProjeto, chaveCodigoCategoria);

        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }

    private string persisteEdicaoRegistro()
    {
        string mensagemErro = "";
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();

        string codigoComposto = getChavePrimaria();

        string[] chaves = codigoComposto.Split('|');
        string chaveCodigoProjeto = chaves[0];
        string chaveCodigoCategoria = chaves[1];


        decimal peso = (spnPeso.Value == null) ? (decimal)0.0 : (decimal)spnPeso.Value;
        int codigoProjetoSelecionado = int.Parse(ddlProjeto.Value.ToString());
        int codigoCategoriaSelecionada = int.Parse(ddlCategoria.Value.ToString());


        string comandosql = string.Format(@"
       UPDATE {0}.{1}.[UHE_PesoItensCurvaS]
       SET [CodigoProjeto] = {2}
          ,[CodigoCategoria] = {3}
          ,[Peso] = {4}
     WHERE CodigoProjeto = {5} and CodigoCategoria = {6}", dbName, dbOwner, codigoProjetoSelecionado, codigoCategoriaSelecionada, peso.ToString().Replace(',', '.'), chaveCodigoProjeto, chaveCodigoCategoria);
        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }

    private string persisteInclusaoRegistro()
    {
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();

        decimal peso = (spnPeso.Value == null) ? (decimal)0.0 : (decimal)spnPeso.Value;
        int codigoProjetoSelecionado = int.Parse(ddlProjeto.Value.ToString());
        int codigoCategoriaSelecionada = int.Parse(ddlCategoria.Value.ToString());

        string comandosql = string.Format(@"INSERT INTO {0}.{1}.[UHE_PesoItensCurvaS]
           ([CodigoProjeto]           ,[CodigoCategoria]           ,[Peso])
     VALUES({2}                       ,{3}                         ,{4})", cDados.getDbName(), cDados.getDbOwner()
           , codigoProjetoSelecionado, codigoCategoriaSelecionada,  peso.ToString().Replace(',', '.'));
        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        string mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }
    protected void gvDados_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PesIteCurvUHE", lblTituloTela.Text, this);

    }
    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PesIteCurvUHE");
    }
    protected void ddlCategoria_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        int resultado = -1;
        if (int.TryParse(parametro, out resultado) == true)
        {
            carregaComboCategoria(int.Parse(parametro));
        }
        else
        {
            carregaComboCategoria(resultado);
        }        
    }
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data && e.Text != "")
        {
            if (e.Column.Name == "col_Peso")
            {

                e.TextValueFormatString = "n4";

                e.Text = string.Format("{0:n4}", e.TextValue);
            }
        }
    }
}