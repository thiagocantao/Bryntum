using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class ElementosEstrategia : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    private string resolucaoCliente = "";

    private int codigoProjeto = -1;

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

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            //cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
        }

        carregaGvDados();

        carregaComboFatores();
        carregaComboTemas();
        carregaComboObjetivos();
        carregaComboAcoes();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        if (Request.QueryString["Altura"] != null)
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString());
        }
        else
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 415;
        }
        
        pcDados.Width = largura - 300;
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @codigoMapaEstrategicoDefault INT
                   ,@codigoTipoObjetoFatorChave INT
                   ,@codigoTipoObjetoTemaPrioritario INT

            SELECT @codigoMapaEstrategicoDefault = CAST(Valor AS Int) 
              FROM {0}.{1}.ParametroConfiguracaoSistema
             WHERE Parametro = 'CodigoMapaDefault'
               AND CodigoEntidade = {2}

            SELECT @codigoTipoObjetoFatorChave = CodigoTipoObjetoEstrategia
              FROM {0}.{1}.TipoObjetoEstrategia
             WHERE IniciaisTipoObjeto = 'PSP'

            SELECT @codigoTipoObjetoTemaPrioritario = CodigoTipoObjetoEstrategia
              FROM {0}.{1}.TipoObjetoEstrategia
             WHERE IniciaisTipoObjeto = 'TEM'          

            SELECT oe3.CodigoObjetoEstrategia as CodigoFatorChave,
                   oe3.TituloObjetoEstrategia as FatorChave,
                   oe2.CodigoObjetoEstrategia as CodigoTemaPrioritario,
                   oe2.TituloObjetoEstrategia as TemaPrioritario,       
                   oe1.CodigoObjetoEstrategia as CodigoObjetivoEstrategico,
                   oe1.DescricaoObjetoEstrategia as ObjetivoEstrategico,
                   aSug.CodigoAcaoSugerida as CodigoAcaoTransformadora,
                   aSug.DescricaoAcao as AcaoTransformadora
            FROM {0}.{1}.ProjetoAcoesSugeridas paSug INNER JOIN
                 {0}.{1}.AcoesSugeridas aSug ON ( aSug.CodigoAcaoSugerida = paSug.CodigoAcaoSugerido ) INNER JOIN
                 {0}.{1}.ObjetoEstrategia oe1 ON ( oe1.CodigoObjetoEstrategia = paSug.CodigoObjetivoEstrategico ) INNER JOIN
                 {0}.{1}.ObjetoEstrategia oe2 ON ( oe2.CodigoObjetoEstrategia = oe1.CodigoObjetoEstrategiaSuperior ) INNER JOIN
                 {0}.{1}.ObjetoEstrategia oe3 ON ( oe3.CodigoObjetoEstrategia = oe2.CodigoObjetoEstrategiaSuperior )
            WHERE oe1.CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
            AND oe2.CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
            AND oe3.CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
            AND oe2.CodigoTipoObjetoEstrategia = @codigoTipoObjetoTemaPrioritario
            AND oe3.CodigoTipoObjetoEstrategia = @codigoTipoObjetoFatorChave
            AND paSug.CodigoProjeto = {3}
        END"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
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

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
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
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoObjetivo = int.Parse(ddlObjetivo.Value.ToString());
        int codigoAcao = int.Parse(ddlAcao.Value.ToString());
        int regAf = 0;

        string comandoSQL = string.Format(@"    IF NOT EXISTS(SELECT 1 FROM {0}.{1}.ProjetoObjetivoEstrategico WHERE CodigoProjeto = {2} AND CodigoObjetivoEstrategico = {3})
                                                    INSERT INTO {0}.{1}.ProjetoObjetivoEstrategico(CodigoProjeto, CodigoObjetivoEstrategico, IndicaObjetivoEstrategicoPrincipal, Prioridade)
                                                                                        VALUES({2}, {3}, 'N', 'A')

                                                INSERT INTO {0}.{1}.ProjetoAcoesSugeridas( CodigoProjeto, CodigoObjetivoEstrategico, CodigoAcaoSugerido )
                                                                               VALUES( {2}, {3}, {4})", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoObjetivo, codigoAcao);

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int regAf = 0;

        string codigoObjetivo = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjetivoEstrategico").ToString();
        string codigoAcao = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAcaoTransformadora").ToString();

        string comandoSQL = string.Format(@"DELETE FROM {0}.{1}.ProjetoAcoesSugeridas 
                                             WHERE CodigoProjeto = {2} 
                                               AND CodigoObjetivoEstrategico = {3} 
                                               AND CodigoAcaoSugerido = {4}
                                            ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoObjetivo, codigoAcao);

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #endregion

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    #region ComboBox

    private void carregaComboFatores()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @codigoMapaEstrategicoDefault INT
                   ,@codigoTipoObjetoFatorChave INT

            SELECT @codigoMapaEstrategicoDefault = CAST(Valor AS Int) 
              FROM {0}.{1}.ParametroConfiguracaoSistema
             WHERE Parametro = 'CodigoMapaDefault'
               AND CodigoEntidade = {2}

            SELECT @codigoTipoObjetoFatorChave = CodigoTipoObjetoEstrategia
              FROM {0}.{1}.TipoObjetoEstrategia
             WHERE IniciaisTipoObjeto = 'PSP'     

           SELECT CodigoObjetoEstrategia AS Codigo, TituloObjetoEstrategia AS Descricao
             FROM ObjetoEstrategia
            WHERE CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
              AND CodigoTipoObjetoEstrategia = @codigoTipoObjetoFatorChave
            ORDER BY TituloObjetoEstrategia

        END"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            ddlChave.DataSource = ds;
            ddlChave.TextField = "Descricao";
            ddlChave.ValueField = "Codigo";
            ddlChave.DataBind();
        }
    }

    private void carregaComboTemas()
    {
        string codigoFator = ddlChave.SelectedIndex == -1 ? "-1" : ddlChave.Value.ToString();

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @codigoMapaEstrategicoDefault INT
                   ,@codigoTipoObjetoTemaPrioritario INT

            SELECT @codigoMapaEstrategicoDefault = CAST(Valor AS Int) 
              FROM {0}.{1}.ParametroConfiguracaoSistema
             WHERE Parametro = 'CodigoMapaDefault'
               AND CodigoEntidade = {2}

            SELECT @codigoTipoObjetoTemaPrioritario = CodigoTipoObjetoEstrategia
              FROM {0}.{1}.TipoObjetoEstrategia
             WHERE IniciaisTipoObjeto = 'TEM'    

           SELECT CodigoObjetoEstrategia AS Codigo, TituloObjetoEstrategia AS Descricao
             FROM ObjetoEstrategia
            WHERE CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
              AND CodigoTipoObjetoEstrategia = @codigoTipoObjetoTemaPrioritario
              AND CodigoObjetoEstrategiaSuperior = {3}
            ORDER BY TituloObjetoEstrategia

        END"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoFator);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            ddlTema.DataSource = ds;
            ddlTema.TextField = "Descricao";
            ddlTema.ValueField = "Codigo";
            ddlTema.DataBind();
        }
    }

    private void carregaComboObjetivos()
    {
        string codigoTema = ddlTema.SelectedIndex == -1 ? "-1" : ddlTema.Value.ToString();

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @codigoMapaEstrategicoDefault INT
                   ,@codigoTipoObjetoObjetivo INT

            SELECT @codigoMapaEstrategicoDefault = CAST(Valor AS Int) 
              FROM {0}.{1}.ParametroConfiguracaoSistema
             WHERE Parametro = 'CodigoMapaDefault'
               AND CodigoEntidade = {2}        

            SELECT @codigoTipoObjetoObjetivo = CodigoTipoObjetoEstrategia
              FROM TipoObjetoEstrategia
             WHERE IniciaisTipoObjeto = 'OBJ'   

           SELECT CodigoObjetoEstrategia AS Codigo, DescricaoObjetoEstrategia AS Descricao
             FROM ObjetoEstrategia
            WHERE CodigoMapaEstrategico = @codigoMapaEstrategicoDefault
              AND CodigoTipoObjetoEstrategia = @codigoTipoObjetoObjetivo
              AND CodigoObjetoEstrategiaSuperior = {3}
            ORDER BY TituloObjetoEstrategia

        END"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoTema);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            ddlObjetivo.DataSource = ds;
            ddlObjetivo.TextField = "Descricao";
            ddlObjetivo.ValueField = "Codigo";
            ddlObjetivo.DataBind();
        }
    }

    private void carregaComboAcoes()
    {

        string codigoObjetivo = ddlObjetivo.SelectedIndex == -1 ? "-1" : ddlObjetivo.Value.ToString();

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @codigoMapaEstrategicoDefault INT
                   ,@codigoTipoObjetoFatorChave INT
                   ,@codigoTipoObjetoTemaPrioritario INT
                   ,@codigoTipoObjetoObjetivo INT            

           SELECT a.CodigoAcaoSugerida AS Codigo, a.DescricaoAcao AS Descricao
             FROM AcoesSugeridas a
            WHERE CodigoEntidade = {2}
              AND CodigoTipoAssociacao = [dbo].[f_GetCodigoTipoAssociacao] ('OB')
              AND CodigoObjetoAssociado = {3}
              AND a.CodigoAcaoSugerida NOT IN(SELECT pas.CodigoAcaoSugerido 
                                                 FROM {0}.{1}.ProjetoAcoesSugeridas pas 
                                                WHERE a.CodigoAcaoSugerida = pas.CodigoAcaoSugerido
                                                  AND pas.CodigoObjetivoEstrategico = {3}
                                                  AND pas.CodigoProjeto = {4})
            ORDER BY DescricaoAcao     

        END"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoObjetivo, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            ddlAcao.DataSource = ds;
            ddlAcao.TextField = "Descricao";
            ddlAcao.ValueField = "Codigo";
            ddlAcao.DataBind();
        }
    }

    #endregion
    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.Style.Add("height", "30px");
    }
}
