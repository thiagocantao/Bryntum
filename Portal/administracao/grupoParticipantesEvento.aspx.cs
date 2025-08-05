using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Data.Filtering;

public partial class administracao_grupoParticipantesEvento : System.Web.UI.Page
{
		#region Fields (8) 

    private int alturaPrincipal = 0;
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuarioResponsavel;
    public bool podeEditar = true;
    public bool podeExcluir = true;
    public bool podeIncluir = true;
    private string resolucaoCliente = "";
    string moduloSistema = "";
    string idProjeto = ""; 
		#endregion Fields 

		#region Methods (4) 

		// Protected Methods (4) 

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {

    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

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
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AdmParticipEven");
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "INCGRP");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EXCGRP");
        if (Request.QueryString["MOD"] != null) //Ok
        {
            moduloSistema = Request.QueryString["MOD"].ToString();
            hfGeral.Set("moduloSistema", moduloSistema);
        }
        if (Request.QueryString["idProjeto"] != null)
        {
            idProjeto = Request.QueryString["idProjeto"];
        }

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
            cDados.aplicaEstiloVisual(Page);
            gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }
        carregaGvDados("");

        if (!IsPostBack)
        {
            
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") + "" != "Incluir")
            carregaListaGridParticipantes(getChavePrimaria());
        else
            carregaListaGridParticipantes("-1");

        gvParticipantesEventos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

		#endregion Methods 



    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/grupoParticipantesEvento.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("grupoParticipantesEvento", "ASPxListbox", "_Strings", "barraNavegacao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 285;
    }
    #endregion

    #region GRID

    private void carregaGvDados(string where)
    {
        string comandoSQL = string.Format(@"SELECT CodigoGrupoParticipantes
                                                 ,DescricaoGrupoParticipantes
                                                 ,CodigoEntidade
                                            FROM {0}.{1}.GrupoParticipantesEvento
                                            WHERE CodigoEntidade = {2}  {3}
                                            order by DescricaoGrupoParticipantes", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, where);


        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados("");
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
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }


    private bool incluiGrupoParticipanteEvento(string nomeGrupo, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @ultimoGrupoInserido AS int 
            INSERT INTO {0}.{1}.GrupoParticipantesEvento
                                (CodigoEntidade,DescricaoGrupoParticipantes)
                          VALUES(          {2} ,'{3}')
            SET @ultimoGrupoInserido = scope_identity()
            SELECT  @ultimoGrupoInserido
        END", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, nomeGrupo);


        string[] arrayParticipantesSelecionados = new string[gvParticipantesEventos.GetSelectedFieldValues(gvParticipantesEventos.KeyFieldName).Count];

        for (int i = 0; i < arrayParticipantesSelecionados.Length; i++)
        {
            arrayParticipantesSelecionados[i] = gvParticipantesEventos.GetSelectedFieldValues(gvParticipantesEventos.KeyFieldName)[i].ToString();
        }

        try
        {
           int retornoUltimoGrupo = -1;
           DataSet dsUltimoGrupo = cDados.getDataSet(comandoSQL);
           if (cDados.DataSetOk(dsUltimoGrupo) && cDados.DataTableOk(dsUltimoGrupo.Tables[0]))
           {
               retornoUltimoGrupo = int.Parse(dsUltimoGrupo.Tables[0].Rows[0][0].ToString());
           }

           retorno = cDados.incluiParticipantesGruposSelecionados(arrayParticipantesSelecionados, retornoUltimoGrupo.ToString());
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;
    }
    
    private bool excluiGrupoParticipanteEvento(int CodigoGrupoParticipante, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = string.Format(@"
      DELETE FROM {0}.{1}.UsuarioGrupoParticipantesEvento WHERE CodigoGrupoParticipantes = {2}  
      DELETE FROM {0}.{1}.GrupoParticipantesEvento
      WHERE CodigoGrupoParticipantes = {2} and CodigoEntidade = {3}", cDados.getDbName(), cDados.getDbOwner(), CodigoGrupoParticipante, codigoEntidadeUsuarioResponsavel);

        int registrosAfetados = 0;

        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;
    }

    private bool atualizaGrupoParticipanteEvento(int codigoGrupoParticipantes, string descricaoGrupoParticipantes, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = string.Format(@"
       UPDATE {0}.{1}.GrupoParticipantesEvento
          SET DescricaoGrupoParticipantes = '{4}'
        WHERE CodigoEntidade = {2} AND 
              CodigoGrupoParticipantes = {3}", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoGrupoParticipantes, descricaoGrupoParticipantes);
        int registrosAfetados = 0;

        string[] arrayParticipantesSelecionados = new string[gvParticipantesEventos.GetSelectedFieldValues(gvParticipantesEventos.KeyFieldName).Count];

        for (int i = 0; i < arrayParticipantesSelecionados.Length; i++)
        {
            arrayParticipantesSelecionados[i] = gvParticipantesEventos.GetSelectedFieldValues(gvParticipantesEventos.KeyFieldName)[i].ToString();
        }
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
            retorno = cDados.incluiParticipantesGruposSelecionados(arrayParticipantesSelecionados, codigoGrupoParticipantes.ToString());
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;
    }
    
    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
 
        string nomeGrupo = txtNomeGrupo.Text;

        string mensagemErro = "";
        bool result = incluiGrupoParticipanteEvento(nomeGrupo, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados("");
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoGrupoParticipante = int.Parse(getChavePrimaria());

        string nomeGrupoParticipante = txtNomeGrupo.Text;
        string mensagemErro = "";


        bool result = atualizaGrupoParticipanteEvento(codigoGrupoParticipante, nomeGrupoParticipante, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados("");
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoGrupoParticipante = int.Parse(getChavePrimaria());
        
        string mensagemErro = "";

        bool result = excluiGrupoParticipanteEvento(codigoGrupoParticipante, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados("");
            return "";
        }

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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadGrpPartEven");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadGrpPartEven", lblTituloTela.Text, this);
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

    protected void gvParticipantesEventos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != "")
        {
            gvParticipantesEventos.ExpandAll();
            //carregaListaProjetos(e.Parameters);
            selecionaParticipantes();
        }
    }


    private void selecionaParticipantes()
    {
        gvParticipantesEventos.Selection.UnselectAll();

        for (int i = 0; i < gvParticipantesEventos.VisibleRowCount; i++)
        {
            if (gvParticipantesEventos.GetRowValues(i, "Selecionado").ToString() == "S")
                gvParticipantesEventos.Selection.SelectRow(i);
        }
    }
    
    private void carregaListaGridParticipantes(string codigoGrupo)
    {
        string comandoSQL = string.Format(@"                
     select * from (   
     SELECT u.NomeUsuario
               ,u.CodigoUsuario, 'N' AS Selecionado, 1 AS ColunaAgrupamento
          FROM {0}.{1}.Usuario u 
    INNER JOIN {0}.{1}.UsuarioUnidadeNegocio uun ON ( uun.CodigoUsuario = u.CodigoUsuario )
                    WHERE u.CodigoUsuario NOT IN ((SELECT CodigoUsuarioParticipante
                                              FROM {0}.{1}.UsuarioGrupoParticipantesEvento WHERE CodigoGrupoParticipantes =  {3}))
                    AND u.DataExclusao IS NULL
                    AND uun.CodigoUnidadeNegocio = {2}
                    AND uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'

         UNION                    
       SELECT u.NomeUsuario
               ,u.CodigoUsuario, 'S' as Selecionado, 0 AS ColunaAgrupamento
          FROM {0}.{1}.Usuario u 
    INNER JOIN {0}.{1}.UsuarioUnidadeNegocio uun ON ( uun.CodigoUsuario = u.CodigoUsuario )
                    WHERE u.CodigoUsuario IN (SELECT CodigoUsuarioParticipante
                                              FROM {0}.{1}.UsuarioGrupoParticipantesEvento where CodigoGrupoParticipantes = {3})
                    --AND u.CodigoUsuario IN (SELECT * FROM {0}.{1}.f_GetPossiveisConvidadosReuniao('{4}', 'PR', -1, {2}))
                    AND u.DataExclusao IS NULL
                    AND uun.CodigoUnidadeNegocio = {2}
                    AND uun.IndicaUsuarioAtivoUnidadeNegocio = 'S') as a
       ORDER BY 1", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoGrupo, "PRJ");

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvParticipantesEventos.DataSource = ds;
        gvParticipantesEventos.DataBind();
        //return ds;
    }
}