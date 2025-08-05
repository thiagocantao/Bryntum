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

public partial class administracao_adm_GrupoRecurso : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadGrpRec");
        }

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadGrpRec"))
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
        
        cDados.aplicaEstiloVisual(Page);


        cDados.setaTamanhoMaximoMemo(txtDetalhe, 250, lblContadorMemoDetalhe);
        if (!IsPostBack && !IsCallback)
        {
            populaTipoRecurso();
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
                 
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_GrupoRecurso.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "adm_GrupoRecurso", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);
      
        gvDados.Settings.VerticalScrollableHeight = altura - 310;
    }

    #endregion

    #region COMBOBOX

    protected void ddlGrupoRecursoSuperior_Callback(object sender, CallbackEventArgsBase e)
    {
        // os parâmetros devem: [CodigoTipoRecurso];[CodigoGrupoRecurso];[CodigoGrupoSuperior]
        string[] parametros = e.Parameter.ToString().Split(';');
        int codigoTipoRecurso, codigoRecurso;

        if (parametros.Length > 2)
        {
            int.TryParse(parametros[0], out codigoTipoRecurso);
            int.TryParse(parametros[1], out codigoRecurso);
            populaComboSuperior(codigoTipoRecurso, codigoRecurso);
            ddlGrupoRecursoSuperior.ClientEnabled = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            ddlGrupoRecursoSuperior.JSProperties["cpCodigoGrupoSuperior"] = parametros[2];

            ddlGrupoRecursoSuperior.JSProperties["cp_TemFilho"] = "N";

            if (cDados.existeFilhosDoGrupoRecurso(codigoRecurso))
            {
                ddlTipoRecurso.JSProperties["cp_TemFilho"] = "S";
            }           
        }
    }

    protected void ddlTipoRecurso_Callback(object sender, CallbackEventArgsBase e)
    {
        // os parâmetros devem: [codigoTipoRecurso] [codigoGrupoRecurso]
        string[] parametro = e.Parameter.ToString().Split(';');
        int codigoGrupoRecurso;
        int codigoTipoRecurso;

        ddlTipoRecurso.JSProperties["cp_TemFilho"] = "N";
        int.TryParse(parametro[0], out codigoTipoRecurso);
        ddlTipoRecurso.JSProperties["cp_codigoTipoRecurso"] = codigoTipoRecurso.ToString();

        if (parametro.Length > 0)
        {
            int.TryParse(parametro[1], out codigoGrupoRecurso);
            if (cDados.existeFilhosDoGrupoRecurso(codigoGrupoRecurso))
            {
                ddlTipoRecurso.JSProperties["cp_TemFilho"] = "S";
            }
        }
    }

    private void populaComboSuperior(int codigoTipoRecurso, int codigoGrupoRecurso)
    {
        string where = string.Format(@" AND gr.[CodigoTipoRecurso] = {2} AND gr.[CodigoGrupoRecurso] != {3} 
                                        AND {0}.{1}.f_EhGrupoRecursoDescendente(gr.[CodigoGrupoRecurso], {3}) = 0 "
            , cDados.getDbName(), cDados.getDbOwner(), codigoTipoRecurso, codigoGrupoRecurso);
        DataSet ds = cDados.getGruposRecursos(codigoEntidadeUsuarioResponsavel, where);
        if (cDados.DataSetOk(ds))
        {
            ddlGrupoRecursoSuperior.DataSource = ds.Tables[0];
            ddlGrupoRecursoSuperior.TextField = "DescricaoGrupo";
            ddlGrupoRecursoSuperior.ValueField = "CodigoGrupoRecurso";
            ddlGrupoRecursoSuperior.DataBind();
        }
        else
        {
            ddlGrupoRecursoSuperior.DataSource = null;
            ddlGrupoRecursoSuperior.DataBind();
        }
        //Adiciono um novo elemento ao combo.
        ddlGrupoRecursoSuperior.Items.Insert(0, new ListEditItem(Resources.traducao.nenhum, -1));
    }

    private void populaTipoRecurso()
    {
        DataSet ds = cDados.getTiposRecursos("");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlTipoRecurso.DataSource = ds.Tables[0];
            ddlTipoRecurso.DataBind();
        }
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getGruposRecursos(codigoEntidadeUsuarioResponsavel, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        if (!IsPostBack)
            gvDados.FocusedRowIndex = 0;
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadGrpRec"))
            podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadGrpRec"))
            podeExcluir = true;

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

        pnCallback.JSProperties["cp_sucesso"] = "";
        pnCallback.JSProperties["cp_erro"] = "";


        if (e.Parameter == "Incluir")
        {
            pnCallback.JSProperties["cp_sucesso"] = Resources.traducao.adm_GrupoRecurso_grupo_inclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {

            pnCallback.JSProperties["cp_sucesso"] = Resources.traducao.adm_GrupoRecurso_dados_gravados_com_sucesso_;
            mensagemErro_Persistencia = persisteEdicaoRegistro();

        }
        if (e.Parameter == "Excluir")
        {
            pnCallback.JSProperties["cp_sucesso"] = Resources.traducao.adm_GrupoRecurso_grupo_exclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            /*
         A instrução DELETE conflitou com a restrição do REFERENCE "FK_RecursoCorporativo_GrupoRecurso". O conflito ocorreu no bando de dados "desenv_PortalEstrategia", tabela "dbo.RecursoCorporativo", column 'CodigoGrupoRecurso'. 
A instrução foi finalizada.
    
         */
            if (mensagemErro_Persistencia.Contains("REFERENCE") == true)
            {
                mensagemErro_Persistencia = Resources.traducao.adm_GrupoRecurso_este_grupo_n_o_pode_ser_exclu_do_pois_h__recursos_corporativos_associados_a_ele;
            }
   

        }
        pnCallback.JSProperties["cp_erro"] = mensagemErro_Persistencia;
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string nomeGrupo;
        string detalhe;
        int? codigoGrupoSuperior;
        int tipoRecursoGrupo;
        string unidadeMedida = string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ?
            "NULL" : String.Format("'{0}'", txtUnidadeMedida.Text.Replace("'", "''"));
        string valorHora = string.IsNullOrWhiteSpace(txtValorHora.Text) ?
             "NULL" : txtValorHora.Text.Replace(",", ".");
        string valorUso = string.IsNullOrWhiteSpace(txtValorUso.Text) ?
             "NULL" : txtValorUso.Text.Replace(",", ".");

        DecodificaValoresTela(out nomeGrupo, out detalhe, out codigoGrupoSuperior, out tipoRecursoGrupo);
        try
        {
            int idNovoGrupo;
            string mesgError = "";
            if (cDados.existeGrupoRecursoComMesmoNomeNaEntidade(nomeGrupo, codigoEntidadeUsuarioResponsavel, -1, "I", ref mesgError))
            {
                return Resources.traducao.adm_GrupoRecurso_n_o___permitido_incluir_um_grupo_de_recurso_com_mesmo_nome_na_entidade;
            }
            else
            {
                bool result = cDados.incluiGrupoRecurso(nomeGrupo, codigoGrupoSuperior, detalhe, codigoEntidadeUsuarioResponsavel, tipoRecursoGrupo, unidadeMedida, valorHora, valorUso, out idNovoGrupo, ref mesgError);

                if (result == false)
                {
                    //if (mesgError.Contains("UQ_Projeto_NomeProjeto"))
                    //    return "Nome do Projeto já Existe!";
                    //else
                    return mesgError;
                }
                else
                {
                    carregaGvDados();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(idNovoGrupo);
                    gvDados.ClientVisible = false;
                    return "";
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private void DecodificaValoresTela(out string nomeGrupo, out string detalhe, out int? codigoGrupoSuperior, out int tipoRecursoGrupo)
    {
        nomeGrupo = txtNome.Text.Replace("'", "''");
        codigoGrupoSuperior = null;
        detalhe = null;

        if (txtDetalhe.Text.Length > 0)
            detalhe = txtDetalhe.Text.Replace("'", "''");

        if (hfGeral.Contains("CodigoGrupoSuperior"))
        {
            string strCod = hfGeral.Get("CodigoGrupoSuperior").ToString();
            int iCod;
            if (int.TryParse(strCod, out iCod) && (iCod > 0))
                codigoGrupoSuperior = iCod;
        }

        // tipoRecursoGrupo tem que ter valor
        tipoRecursoGrupo = int.Parse(ddlTipoRecurso.SelectedItem.Value.ToString());
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string mesgError = "";
        int codigoGrupo = int.Parse(getChavePrimaria());

        string nomeGrupo;
        string detalhe;
        int? codigoGrupoSuperior;
        int tipoRecursoGrupo;
        string unidadeMedida = string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ?
            "NULL" : String.Format("'{0}'", txtUnidadeMedida.Text.Replace("'", "''"));
        string valorHora = string.IsNullOrWhiteSpace(txtValorHora.Text) ?
             "NULL" : txtValorHora.Text.Replace(",", ".");
        string valorUso = string.IsNullOrWhiteSpace(txtValorUso.Text) ?
             "NULL" : txtValorUso.Text.Replace(",", ".");
        string erroExiste = "";
        DecodificaValoresTela(out nomeGrupo, out detalhe, out codigoGrupoSuperior, out tipoRecursoGrupo);

        if (cDados.existeGrupoRecursoComMesmoNomeNaEntidade(nomeGrupo, codigoEntidadeUsuarioResponsavel, codigoGrupo, "E", ref erroExiste))
        {
            return erroExiste;
        }
        else
        {

            cDados.AtualizaGrupoRecurso(codigoGrupo, nomeGrupo, codigoGrupoSuperior, detalhe, tipoRecursoGrupo, unidadeMedida, valorHora, valorUso, ref mesgError);

            carregaGvDados();
            return mesgError;
        }



    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";
        bool retornoExclusao = false;
        string where = " and gr.GrupoRecursoSuperior = " + chave;
        DataSet ds = cDados.getGruposRecursos(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            msgRetorno = Resources.traducao.adm_GrupoRecurso_este_grupo_n_o_pode_ser_exclu_do_pois_h__recursos_corporativos_associados_a_ele;
        }
        else
        {
            retornoExclusao = cDados.excluiGrupoRecurso(chave, codigoEntidadeUsuarioResponsavel.ToString(), ref msgRetorno);
        }
        carregaGvDados();
        return msgRetorno;
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadGrpRec");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadGrpRec", lblTituloTela.Text, this);
    }

    #endregion
}
