using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Data;

public partial class _Projetos_Administracao_InterdependiaProjetos : System.Web.UI.Page
{
    #region Fields

    private dados cDados;
    private Int32 idProjeto;
    private Int32 idUsuarioLogado;
    private Int32 codigoEntidade;
    private Int32 alturaPrincipal;
    public String TooltipbtnIncluir = "IncluirOKOK";
    public String nomeProjeto;

    private String resolucaoCliente;
    private String dbName;
    private String dbOwner;

    protected Boolean podeIncluir;
    protected Boolean podeAlterarInterdependencias;

    private int quantidadeProjetosPai = -1;

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        dsProjetosDependenciaFilho.ConnectionString = cDados.classeDados.getStringConexao();
        dsProjetosDependenciaPai.ConnectionString = cDados.classeDados.getStringConexao();
        dsProjetosDisponiveisFilho.ConnectionString = cDados.classeDados.getStringConexao();
        dsProjetosDisponiveisPai.ConnectionString = cDados.classeDados.getStringConexao();

        idProjeto = Convert.ToInt32(Request.QueryString["IDProjeto"]);
        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        nomeProjeto = cDados.getNomeProjeto(idProjeto, "");
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        setPermissoesTela();
        podeAlterarInterdependencias = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade,
            idProjeto, "null", "PR", 0, "null", "PR_AltInterdep");
        podeIncluir = podeAlterarInterdependencias;

        bool podeExcluir = false;

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeIncluir, ref podeAlterarInterdependencias, ref podeExcluir);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, idProjeto, "NULL", "PR", 0, "NULL", "PR_CnsInterdep");

        }
        this.Title = cDados.getNomeSistema();

        gvProjetosDependenciaFilho.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvProjetosDependenciaPai.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            if (podeAlterarInterdependencias)
            {
                ASPxGridView grid = (ASPxGridView)sender;
                String tipoLink = grid.GetRowValues(e.VisibleIndex, "TipoLink").ToString();
                if (tipoLink.Trim().Equals("PP"))
                {
                    e.Enabled = false;
                    e.Image.Url = "../../imagens/botoes/excluirRegDes.png";
                }
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/excluirRegDes.png";
            }
        }
    }

    #endregion

    #region Methods

    private void setPermissoesTela()
    {

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        //Calcula a altura da tela
        int largura1 = 0;
        int altura1 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 70);
        if (altura > 0)
        {
            gvProjetosDependenciaFilho.Settings.VerticalScrollableHeight = (altura / 2);
            gvProjetosDependenciaPai.Settings.VerticalScrollableHeight = (altura / 2);
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Cadastro.js""></script>"));
        //Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/InterdependiaProjetos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "InterdependiaProjetos", "Cadastro"));
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporterProjetosDependenciaFilho, "InterProj");
    }

    protected void menu_ItemClick1(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporterProjetosDependenciaPai, "InterProj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir && (quantidadeProjetosPai < 1), "gvProjetosDependenciaFilho.AddNewRow(); TipoOperacao = 'Incluir';", true, true, false, "InterProj", "Interdependências", this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gvProjetosDependenciaPai.AddNewRow(); TipoOperacao = 'Incluir';", true, true, false, "InterProj", "Interdependências", this);
    }

    #endregion
    
    protected void gvExporterProjetosDependenciaFilho_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        Font fonteWingdings = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
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
        if (e.Column == gvProjetosDependenciaFilho.Columns["Desempenho"] && (e.Value != null))
        {
            e.BrickStyle.Font = fonteWingdings;
            e.Text = "l";
            e.TextValue = "l";
            if (e.Value.ToString().Equals("Vermelho"))
            {
                e.BrickStyle.ForeColor = Color.Red;
            }
            if (e.Value.ToString().Equals("Amarelo"))
            {
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            if (e.Value.ToString().Equals("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            if (e.Value.ToString().Equals("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            if (e.Value.ToString().Equals("Laranja"))
            {
                e.BrickStyle.ForeColor = Color.Orange;
            }
            if (e.Value.ToString().Equals("Branco"))
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }


    protected void gvExporterProjetosDependenciaPai_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        Font fonteWingdings = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);

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

        if (e.Column == gvProjetosDependenciaPai.Columns["Desempenho"] && (e.Value != null))
        {
            e.BrickStyle.Font = fonteWingdings;
            e.Text = "l";
            e.TextValue = "l";
            if (e.Value.ToString().Equals("Vermelho"))
            {
                e.BrickStyle.ForeColor = Color.Red;
            }
            if (e.Value.ToString().Equals("Amarelo"))
            {
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            if (e.Value.ToString().Equals("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            if (e.Value.ToString().Equals("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            if (e.Value.ToString().Equals("Laranja"))
            {
                e.BrickStyle.ForeColor = Color.Orange;
            }
            if (e.Value.ToString().Equals("Branco"))
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }

    protected void enviaEmail(string codigoProjeto, string codigoProjetoDependencia, string tipoLink)
    {
        string nomeGerenteProjetoPai = "";
        string emailGerenteProjetoPai = "";
        string nomeProjetoPai = "";
        string nomeGerenteProjetoFilho = "";
        string emailGerenteProjetoFilho = "";
        string nomeProjetoFilho = "";

        string comandoSQL;

        DataSet dsGerente;

        string assunto;
        string mensagem;
        int retornoStatus;

        // Projeto Pai
        comandoSQL = string.Format(@"
            SELECT p.NomeProjeto, u.NomeUsuario AS NomeGerenteProjeto, u.EMail AS EmailGerenteProjeto
            FROM Projeto p
            INNER JOIN Usuario u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
            WHERE p.CodigoProjeto = {0}", codigoProjeto);
        dsGerente = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsGerente) && cDados.DataTableOk(dsGerente.Tables[0]))
        {
            nomeGerenteProjetoPai = dsGerente.Tables[0].Rows[0]["NomeGerenteProjeto"].ToString();
            emailGerenteProjetoPai = dsGerente.Tables[0].Rows[0]["EmailGerenteProjeto"].ToString();
            nomeProjetoPai = dsGerente.Tables[0].Rows[0]["NomeProjeto"].ToString();
        }

        // Projeto Filho
        comandoSQL = string.Format(@"
            SELECT p.NomeProjeto, u.NomeUsuario AS NomeGerenteProjeto, u.EMail AS EmailGerenteProjeto
            FROM Projeto p
            INNER JOIN Usuario u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
            WHERE p.CodigoProjeto = {0}", codigoProjetoDependencia);
        dsGerente = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsGerente) && cDados.DataTableOk(dsGerente.Tables[0]))
        {
            nomeGerenteProjetoFilho = dsGerente.Tables[0].Rows[0]["NomeGerenteProjeto"].ToString();
            emailGerenteProjetoFilho = dsGerente.Tables[0].Rows[0]["EmailGerenteProjeto"].ToString();
            nomeProjetoFilho = dsGerente.Tables[0].Rows[0]["NomeProjeto"].ToString();
        }

        // Projeto Pai
        if (emailGerenteProjetoPai.Trim() != "")
        {
            assunto = string.Format(@"O projeto ""{0}"" foi relacionado a outro projeto via Interdependência", nomeProjetoPai);
            mensagem = string.Format(@"<p>Prezado(a) {0},</p>
            <p>O projeto ""{1}"" no qual você é responsável acaba de se se tornar dependente do projeto ""{3}"", cujo responsável é {2}.</p>",
                nomeGerenteProjetoPai, nomeProjetoPai, nomeGerenteProjetoFilho, nomeProjetoFilho);
            retornoStatus = 0;
            cDados.enviarEmail(assunto, emailGerenteProjetoPai, string.Empty, mensagem, string.Empty, string.Empty, ref retornoStatus);
        }

        // Projeto Filho
        if (emailGerenteProjetoFilho.Trim() != "")
        {
            assunto = string.Format(@"O projeto ""{0}"" foi relacionado a outro projeto via Interdependência", nomeProjetoFilho);
            mensagem = string.Format(@"<p>Prezado(a) {0},</p>
                <p>O projeto ""{1}"" no qual você é responsável acaba de se se tornar parte do projeto ""{3}"", cujo responsável é {2}.</p>",
                nomeGerenteProjetoFilho, nomeProjetoFilho, nomeGerenteProjetoPai, nomeProjetoPai);
            retornoStatus = 0;
            cDados.enviarEmail(assunto, emailGerenteProjetoFilho, string.Empty, mensagem, string.Empty, string.Empty, ref retornoStatus);
        }
    }

    protected void dsProjetosDependenciaFilho_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        string codigoProjetoPai = e.Command.Parameters["@CodigoProjetoDependencia"].Value.ToString();
        string codigoProjetoFilho = e.Command.Parameters["@CodigoProjeto"].Value.ToString();
        string tipoLink = e.Command.Parameters["@TipoLink"].Value.ToString();
        enviaEmail(codigoProjetoPai, codigoProjetoFilho, tipoLink);

    }

    protected void dsProjetosDependenciaPai_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        string codigoProjetoPai = e.Command.Parameters["@CodigoProjeto"].Value.ToString();
        string codigoProjetoFilho = e.Command.Parameters["@CodigoProjetoDependencia"].Value.ToString();
        string tipoLink = e.Command.Parameters["@TipoLink"].Value.ToString();
        enviaEmail(codigoProjetoPai, codigoProjetoFilho, tipoLink);
    }

    protected void dsProjetosDependenciaFilho_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        quantidadeProjetosPai = e.AffectedRows;
    }

    //Valida Inserção do Projeto FILHO se não há projetos PAI criados.
    protected void dsProjetosDependenciaPai_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        string codigoProjetoPai = (e.Command.Parameters["@CodigoProjetoDependencia"].Value != null) ? e.Command.Parameters["@CodigoProjetoDependencia"].Value.ToString() : "";
        string comandoSQL = string.Format(@"
                                            SELECT * FROM LinkProjeto
                                            where CodigoProjetoPai = {0}", codigoProjetoPai);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            e.Cancel = true;
            gvProjetosDependenciaPai.JSProperties["cp_MensagemErro"] = "1";
        }
    }

    //Valida Inserção do Projeto Pai se não ha projetos filhos criados.
    protected void dsProjetosDependenciaFilho_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        string codigoProjetoPai = (e.Command.Parameters["@CodigoProjeto"].Value != null) ? e.Command.Parameters["@CodigoProjeto"].Value.ToString() : "";
        string comandoSQL = string.Format(@"
                                            SELECT * FROM LinkProjeto
                                            where CodigoProjetoPai = {0}", codigoProjetoPai);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            e.Cancel = true;
            gvProjetosDependenciaFilho.JSProperties["cp_MensagemErro"] = "007";
        }
    }
}