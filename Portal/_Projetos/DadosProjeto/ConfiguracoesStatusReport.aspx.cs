using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections.Generic;
using System.Text;

public partial class _Projetos_DadosProjeto_ConfiguracoesStatusReport : System.Web.UI.Page
{
    public Unit alturaListBox = new Unit("250px");

    protected class ListaDeProjetos
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;

        public ListaDeProjetos()
        {
            ListaDeCodigos = new List<int>();
            ListaDeNomes = new List<string>();
        }
        public void Clear()
        {
            ListaDeCodigos.Clear();
            ListaDeNomes.Clear();
        }

        /// <summary>
        /// Adiciona um item na lista de unidades
        /// </summary>
        /// <param name="codigoUnidade">Código da unidade a adicionar</param>
        /// <param name="descricaoUnidade">Descrição da unidade a adicionar</param>
        public void Add(int codigoProjeto, string descricaoProjeto)
        {
            ListaDeCodigos.Add(codigoProjeto);
            ListaDeNomes.Add(descricaoProjeto);
        }

        public string GetDescricaoProjeto(int codigoProjeto)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoProjeto);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoProjeto)
        {
            return ListaDeCodigos.Contains(codigoProjeto);
        }

    }

    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;
    int idObjeto;
    string iniciaisTipoObjeto;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeEditar = true;

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

        idObjeto = int.Parse(Request.QueryString["idObjeto"].ToString());
        iniciaisTipoObjeto = Request.QueryString["tp"];

        if (iniciaisTipoObjeto == "PR")
            cDados.verificaPermissaoProjetoInativo(idObjeto, ref podeEditar, ref podeEditar, ref podeEditar);

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        HeaderOnTela();
        
        defineAlturaTela(resolucaoCliente);

        cDados.aplicaEstiloVisual(Page);//Ok
        lbItensDisponiveis.SelectionMode = ListEditSelectionMode.CheckColumn;
        lbItensSelecionados.SelectionMode = ListEditSelectionMode.CheckColumn;
       
        carregaGvDados();               //Ok

        int codModeloStatusReport = -1;
        bool ret = int.TryParse(getChavePrimaria(), out codModeloStatusReport);
        populaListaBox_ProjetosDisponiveis(codModeloStatusReport);
        populaListaBox_ProjetosSelecionados(codModeloStatusReport);

    }



    #region GRID's

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        DataSet ds = cDados.getModeloStatusReportPorObjeto(idObjeto, iniciaisTipoObjeto);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //carregaGvDados();
    }

    #endregion

    #endregion

    #region LISTBOX

    private void populaListaBox_ProjetosDisponiveis(int codigoModeloStatusReport)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
 SELECT U.CodigoUsuario, U.NomeUsuario
   FROM {0}.{1}.Usuario AS U INNER JOIN
        {0}.{1}.UsuarioUnidadeNegocio AS UUN ON U.CodigoUsuario = UUN.CodigoUsuario
  WHERE U.DataExclusao IS NULL
	AND UUN.IndicaUsuarioAtivoUnidadeNegocio = 'S'
	AND UUN.CodigoUnidadeNegocio = {2}
	AND NOT EXISTS(
		SELECT 1
		  FROM {0}.{1}.DestinatarioModeloStatusReport DMSR
		 WHERE CodigoUsuarioDestinatario = U.CodigoUsuario
		   AND DMSR.CodigoModeloStatusReport = {3}
           AND DMSR.CodigoObjeto = {4}
           AND DMSR.CodigoTipoAssociacaoObjeto = {0}.{1}.f_GetCodigoTipoAssociacao('{5}'))
  ORDER BY U.NomeUsuario
        ", dbName, dbOwner, codigoEntidadeUsuarioResponsavel, codigoModeloStatusReport, idObjeto, iniciaisTipoObjeto);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensDisponiveis.DataSource = dt;
            lbItensDisponiveis.TextField = "NomeUsuario";
            lbItensDisponiveis.ValueField = "CodigoUsuario";
            lbItensDisponiveis.DataBind();
        }

        for (int i = 0; i < lbItensDisponiveis.Items.Count; i++)
        {
            int resto;
            Math.DivRem(i, 2, out resto);
            if (resto == 0)
                lbItensDisponiveis.Items[i].ImageUrl = "~/~/imagens/brancoMenor.gif";
        }
    }

    private void populaListaBox_ProjetosSelecionados(int codigoModeloStatusReport)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
 SELECT U.CodigoUsuario, U.NomeUsuario
   FROM {0}.{1}.DestinatarioModeloStatusReport DMSR INNER JOIN
		{0}.{1}.Usuario U ON U.CodigoUsuario = DMSR.CodigoUsuarioDestinatario INNER JOIN
		{0}.{1}.UsuarioUnidadeNegocio UUN ON UUN.CodigoUsuario = U.CodigoUsuario
  WHERE U.DataExclusao IS NULL
	AND UUN.IndicaUsuarioAtivoUnidadeNegocio = 'S'
	AND DMSR.CodigoModeloStatusReport = {2}
	AND UUN.CodigoUnidadeNegocio = {3}
    AND DMSR.CodigoObjeto = {4}
    AND DMSR.CodigoTipoAssociacaoObjeto = {0}.{1}.f_GetCodigoTipoAssociacao('{5}')
  ORDER BY U.NomeUsuario"
            , dbName, dbOwner, codigoModeloStatusReport, codigoEntidadeUsuarioResponsavel, idObjeto, iniciaisTipoObjeto);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensSelecionados.DataSource = dt;
            lbItensSelecionados.TextField = "NomeUsuario";
            lbItensSelecionados.ValueField = "CodigoUsuario";
            lbItensSelecionados.DataBind();
        }
    }


    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 150;
        }

        if (alturaPrincipal <= 768)
        {
            alturaListBox = new Unit("250px");
        }
        else if ((alturaPrincipal >= 769) && (alturaPrincipal <= 800))
        {
            alturaListBox = new Unit("300px");
        }
        else if ((alturaPrincipal >= 801) && (alturaPrincipal <= 960))
        {
            alturaListBox = new Unit("350px");
        }
        else if (alturaPrincipal >= 961)
        {
            alturaListBox = new Unit("450px");
        }
        lbItensDisponiveis.Height = alturaListBox;
        lbItensSelecionados.Height = alturaListBox;

    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ConfiguracoesStatusReport.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ConfiguracoesStatusReport", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoModeloStatusReport").ToString();
        return codigoDado;
    }

    private string persisteInclusaoCompartilhar()
    {
        try
        {
            StringBuilder comandoSQL = new StringBuilder();
            int registrosAfetados = 0;
            int codigoModeloStatusReport = int.Parse(getChavePrimaria());

            comandoSQL.Append(string.Format(@"
 DELETE FROM {0}.{1}.DestinatarioModeloStatusReport
       WHERE CodigoModeloStatusReport = {2}
         AND CodigoObjeto = {3}
         AND CodigoTipoAssociacaoObjeto = {0}.{1}.f_GetCodigoTipoAssociacao('{4}')"
                , dbName, dbOwner, codigoModeloStatusReport, idObjeto, iniciaisTipoObjeto));

          string codes =  hfGeral.Get("CodigosSelecionados").ToString();
            string[] arrayCodes = codes.Split('$');

            foreach (string item in arrayCodes)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    comandoSQL.Append(string.Format(@"
                    INSERT INTO {0}.{1}.DestinatarioModeloStatusReport
                                (CodigoModeloStatusReport
                                ,CodigoTipoAssociacaoObjeto
                                ,CodigoObjeto
                                ,CodigoUsuarioDestinatario)
                         VALUES
                               ({2}
                               ,{0}.{1}.f_GetCodigoTipoAssociacao('{3}')
                               ,{4}
                               ,{5})", dbName, dbOwner, codigoModeloStatusReport, iniciaisTipoObjeto, idObjeto, item));
                }
            }
            cDados.execSQL(comandoSQL.ToString(), ref registrosAfetados);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    private DataTable DataTableGridImpedimento()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("NomeProjeto", Type.GetType("System.String"));
        NewColumn.Caption = "Projeto";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("Impedimento", Type.GetType("System.String"));
        NewColumn.Caption = "Impedimento";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        return dtResult;
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnCompartilhar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/compartilharDes.png";
            }
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string[] parametros = e.Parameter.Split(';');
        bool marcado = bool.Parse(parametros[0]);
        int codigoModeloStatusReport = int.Parse(parametros[1]);
        string comandoSql;
        if (marcado)
        {
            comandoSql = string.Format(@"
INSERT INTO [GraficoReceitaNoRAP]
           ([CodigoModeloStatusReport]
           ,[CodigoObjeto]
           ,[CodigoTipoAssociacaoObjeto]
           ,[DataAtivacao]
           ,[DataDesativacao])
     VALUES
           ({2}
           ,{0}
           ,dbo.f_GetCodigoTipoAssociacao('{1}')
           ,GETDATE()
           ,NULL)"
                , idObjeto
                , iniciaisTipoObjeto
                , codigoModeloStatusReport);
        }
        else
        {
            comandoSql = string.Format(@"
UPDATE [GraficoReceitaNoRAP]
   SET [DataDesativacao] = GETDATE()
 WHERE [CodigoModeloStatusReport] = {2}
   AND [CodigoObjeto] = {0}
   AND [CodigoTipoAssociacaoObjeto] = dbo.f_GetCodigoTipoAssociacao('{1}')
   AND [DataDesativacao] IS NULL"
                , idObjeto
                , iniciaisTipoObjeto
                , codigoModeloStatusReport);
        }
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ConfSttRepPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "ConfSttRepPrj", "Configuração do Status Report", this);
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

    protected void callback1_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_Erro"] = "";
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Compartilhar")
        {
            ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Status Report configurado com sucesso!";
            mensagemErro_Persistencia = persisteInclusaoCompartilhar();
        }
        if (mensagemErro_Persistencia != "") // não deu erro durante o processo de persistência
        {
            ((ASPxCallback)source).JSProperties["cp_Erro"] = "Erro: " + mensagemErro_Persistencia;
        }
    }

    protected void callback_lbItensDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        populaListaBox_ProjetosDisponiveis(int.Parse(e.Parameter));
          
    }

    protected void callback_lblItensSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        populaListaBox_ProjetosSelecionados(int.Parse(e.Parameter));
    }
}
