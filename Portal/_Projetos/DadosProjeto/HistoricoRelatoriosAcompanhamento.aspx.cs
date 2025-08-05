using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using DevExpress.XtraPrinting;

public partial class _Projetos_DadosProjeto_HistoricoRelatoriosAcompanhamento : System.Web.UI.Page
{
    #region Fields

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    bool podeConsultar;
    bool podeEditar;
    bool podeExcluir;
    bool podeIncluir;
    bool podePublicar;

    dados cDados;
    int codigoProjeto;


    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {
            sdsRelatorioAcompanhamento.ConnectionString =
            _connectionString = value;
        }
    }


    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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

        ConnectionString = cDados.classeDados.getStringConexao();
        codigoProjeto = int.Parse(Request.QueryString["CP"]);
        // dados do usuario logado e da entidade logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok
        //Define variável de sessão que será utilizado na manipulação de dados da tela
        Session["CodigoUsuario"] = codigoUsuarioResponsavel;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        DefineAlturaTela(resolucaoCliente);
        DefinePermissoes();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(2);
            cDados.insereNivel(2, this);
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        e.Result = "-1";
        string parameter = e.Parameter;
        if (parameter == "Novo")
        {
            int insertedRows = sdsRelatorioAcompanhamento.Insert();
            if (insertedRows > 0)
            {
                int codigoRelatorio = ObtemCodigoNovoRelatorio();
                e.Result = codigoRelatorio.ToString();
            }
        }
        else if (parameter.Contains("Limpar"))
        {
            int posicaoSeparador = parameter.IndexOf(';');
            int codigoRelatorio = int.Parse(parameter.Substring(posicaoSeparador + 1));
            LimpaRelatorioPendente(codigoRelatorio);
        }
        else if (parameter.Contains("Download"))
        {
            int posicaoSeparador = parameter.IndexOf(';');
            int codigoRelatorio = int.Parse(parameter.Substring(posicaoSeparador + 1));
            GeraRelatorio(codigoRelatorio);
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        object dataPublicacao = gvDados.GetRowValues(e.VisibleIndex, "DataPublicacao");
        switch (e.ButtonID)
        {
            case "btnDownload":
                if (!podeConsultar)
                {
                    e.Image.Url = @"~/imagens/menuExportacao/iconoPDFDes.PNG";
                    e.Enabled = false;
                }
                break;
            case "btnVisualizar":
                if (!podeConsultar)
                {
                    e.Image.Url = @"~/imagens/botoes/pFormulario.PNG";
                    e.Enabled = false;
                }
                break;
            case "btnEditar":
                if (!podeEditar)
                {
                    e.Image.Url = @"~/imagens/botoes/editarRegDes.PNG";
                    e.Enabled = false;
                }
                else if (!Convert.IsDBNull(dataPublicacao) && dataPublicacao != null)
                {
                    e.Image.Url = @"~/imagens/botoes/editarRegDes.PNG";
                    e.Text = "Não é possível editar um relatório publicado.";
                    e.Enabled = false;
                }
                break;
            case "btnExcluir":
                if (!podeExcluir)
                {
                    e.Image.Url = @"~/imagens/botoes/excluirRegDes.PNG";
                    e.Enabled = false;
                }
                else if (!Convert.IsDBNull(dataPublicacao) && dataPublicacao != null)
                {
                    e.Image.Url = @"~/imagens/botoes/excluirRegDes.PNG";
                    e.Text = "Não é possível excluir um relatório publicado.";
                    e.Enabled = false;
                }
                break;
            case "btnPublicar":
                if (!podePublicar)
                {
                    e.Image.Url = @"~/imagens/botoes/PublicarRegDes.png";
                    e.Enabled = false;
                }
                else if (!Convert.IsDBNull(dataPublicacao) && dataPublicacao != null)
                {
                    e.Image.Url = @"~/imagens/botoes/PublicarRegDes.PNG";
                    e.Text = "O relatório já se encontra publicado.";
                    e.Enabled = false;
                }
                break;
        }
    }

    protected void gvDados_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        int codigoRelatorio = 
            (int)gvDados.GetRowValues(e.VisibleIndex, "CodigoRelatorio");
        switch (e.ButtonID)
        {
            case "btnPublicar":
                sdsRelatorioAcompanhamento.UpdateParameters["CodigoRelatorio"].DefaultValue =
                    codigoRelatorio.ToString();
                int updatedRows = sdsRelatorioAcompanhamento.Update();
                if (updatedRows > 0)
                    gvDados.DataBind();
                break;
        }
    }

    private void GeraRelatorio(int codigoRelatorio)
    {
        relRelatorioAcompanhamento rel =
            new relRelatorioAcompanhamento(codigoRelatorio);
        Session["report"] = rel;
    }

    #endregion

    #region Methods

    protected string ObtemHtmlBtnAdicionar()
    {
        string botao = (podeIncluir) ?
@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""AdicionarNovo();"" style=""cursor: pointer;""/>" :
@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>";
        string html = string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", botao);

        return html;
    }

    private void DefineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 205);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 140;
    }

    private void DefinePermissoes()
    {
        podeConsultar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsRel");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltRel");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_ExcRel");
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_IncRel");
        podePublicar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_PubRel");
    }

    private void DefineStringConexao()
    {
        string connectionString = cDados.classeDados.getStringConexao();

        sdsRelatorioAcompanhamento.ConnectionString = connectionString;
    }

    private int ObtemCodigoNovoRelatorio()
    {
        string comandoSql = string.Format(@"
                     SELECT MAX(CodigoRelatorio) AS CodigoRelatorio
                       FROM {0}.{1}.pbh_RelatorioAcompanhamento
                      WHERE CodigoProjeto={2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSql);
        int codigoRelatorio = (int)ds.Tables[0].Rows[0]["CodigoRelatorio"];
        return codigoRelatorio;
    }

    private void LimpaRelatorioPendente(int codigoRelatorio)
    {
        string comandoSql = string.Format(@"
                DECLARE @CodigoRelatorio int
                    SET @CodigoRelatorio = {2}
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoRiscos]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoProblemas]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoIndicadores]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoEntregas]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoDestinatarios]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamentoAquisicoes]
                      WHERE CodigoRelatorio = @CodigoRelatorio
            
                DELETE FROM {0}.{1}.[pbh_RelatorioAcompanhamento]
                      WHERE CodigoRelatorio = @CodigoRelatorio"
            , cDados.getDbName(), cDados.getDbOwner(), codigoRelatorio);

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    #endregion
}