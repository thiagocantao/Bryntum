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
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;

public partial class administracao_adm_GrupoFluxo : System.Web.UI.Page
{

    #region variáveis globais

    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;


    #endregion

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

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadGrpRec");
        }

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadGdeFlu"))
            podeIncluir = true;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        carregaGvDados();
        if (!IsPostBack && !IsCallback)
        {            
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/custom.css"" rel=""stylesheet"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_GrupoFluxo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "adm_GrupoFluxo", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 320;
    }
    
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = getGruposFluxos(codigoEntidadeUsuarioResponsavel, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

        string IniciaisGrupoFluxoControladoSistema = (gvDados.GetRowValues(e.VisibleIndex, "IniciaisGrupoFluxo") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IniciaisGrupoFluxo").ToString() : "";
        
        
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadGdeFlu"))
            podeEditar = true;

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeEditar)
            {
                e.Enabled = true;
                if (!string.IsNullOrEmpty(IniciaisGrupoFluxoControladoSistema))
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
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
        if (e.Parameter == "Editar")
        {
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

    #endregion

    #region CRUD Banco de dados

    private bool excluiGrupoFluxo(string codigoGrupoFluxo, ref string msgRetorno)
    {
        bool retorno = false;
        int registrosAfetados = 0;

        try
        {
            string comandoSQL = string.Format(@"DELETE FROM {0}.{1}.GrupoFluxo
      WHERE CodigoGrupoFluxo = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoGrupoFluxo);
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            msgRetorno = ex.Message;
            retorno = false;
        }
        return retorno;
    }

    private bool incluiGrupoFluxo(string nomeGrupo, int ordemGrupoRecurso, int codigoEntidadeUsuarioResponsavel, ref string mesgError)
    {
        bool retorno = false;
        string comandoSQL = "";
        int codigoNovoGrupo = -1;
        try
        {
            comandoSQL = string.Format(@"
                BEGIN
                    DECLARE @CodigoNovoGrupo Int, @Nivel Int

                    INSERT INTO {0}.{1}.GrupoFluxo(DescricaoGrupoFluxo,OrdemGrupoMenu,IniciaisGrupoFluxo,CodigoEntidade)
                                            VALUES(              '{2}',           {3},              null,           {4})
                    
                    SET @CodigoNovoGrupo = SCOPE_IDENTITY()

                    SELECT @CodigoNovoGrupo AS CodigoNovoGrupo
                END", cDados.getDbName(), cDados.getDbOwner(), nomeGrupo, ordemGrupoRecurso, codigoEntidadeUsuarioResponsavel);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                codigoNovoGrupo = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            retorno = true;
        }
        catch (Exception ex)
        {
            mesgError = ex.Message + Environment.NewLine + comandoSQL;
            retorno = false;
        }
        return retorno;
    }

    private bool AtualizaGrupoRecurso(string nomeGrupoFluxo, int ordemGrupoFluxo, int codigoGrupoFluxo, ref string mesgError)
    {

        bool retorno = false;
        string comandoSQL = "";
        try
        {
            comandoSQL = string.Format(@"
                    UPDATE {0}.{1}.GrupoFluxo
                    SET DescricaoGrupoFluxo = '{2}'
                       ,OrdemGrupoMenu = {3}
                    WHERE CodigoGrupoFluxo = {4}", cDados.getDbName(), cDados.getDbOwner(), nomeGrupoFluxo, ordemGrupoFluxo, codigoGrupoFluxo);

            int registrosAfetados = 0;
            cDados.execSQL(comandoSQL, ref registrosAfetados);
            retorno = true;
        }
        catch (Exception ex)
        {
            mesgError = ex.Message + Environment.NewLine + comandoSQL;
            retorno = false;
        }
        return retorno;
    }

    private DataSet getGruposFluxos(int codigoEntidadeUsuarioResponsavel, string where)
    {
        string comandoSQL = string.Format(@"
                    SELECT CodigoGrupoFluxo
                          ,DescricaoGrupoFluxo
                          ,OrdemGrupoMenu
                          ,IniciaisGrupoFluxo
                          ,CodigoEntidade
                      FROM {0}.{1}.GrupoFluxo
                     WHERE CodigoEntidade = {2} {3}
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    #endregion

    #region CRUD Persitências

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string codigoGrupoFluxo = getChavePrimaria();
        string msgRetorno = "";
        bool retornoExclusao = false;

        retornoExclusao = excluiGrupoFluxo(codigoGrupoFluxo, ref msgRetorno);

        carregaGvDados();
        return msgRetorno;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {

        string nomeGrupoFluxo = txtDescricaoGrupoFluxo.Text;
        int ordemGrupoFluxo = int.Parse(txtOrdemGrupoFluxo.Text);
        int codigoGrupoFluxo = int.Parse(getChavePrimaria());

        string mesgError = "";

        AtualizaGrupoRecurso(nomeGrupoFluxo, ordemGrupoFluxo, codigoGrupoFluxo, ref mesgError);

        carregaGvDados();
        return mesgError;

    }

    private string persisteInclusaoRegistro()
    {
        string nomeGrupoFluxo = txtDescricaoGrupoFluxo.Text;
        int ordemGrupoRecurso = int.Parse(txtOrdemGrupoFluxo.Text);
        string mensagemRetorno = "";
        try
        {

            bool result = incluiGrupoFluxo(nomeGrupoFluxo, ordemGrupoRecurso, codigoEntidadeUsuarioResponsavel, ref mensagemRetorno);

            if (result == false)
            {
                return mensagemRetorno;
            }

            return mensagemRetorno;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string iniciaisGrupo = e.GetValue("IniciaisGrupoFluxo").ToString();

            if (!String.IsNullOrEmpty(iniciaisGrupo))
            {
                //e.Row.BackColor = Color.FromName("#DDFFCC");
                //e.Row.ForeColor = Color.Black;
                e.Row.ForeColor = Color.FromName("#619340");
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadGrpFlx");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadGrpFlx", lblTituloTela.Text, this);
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
