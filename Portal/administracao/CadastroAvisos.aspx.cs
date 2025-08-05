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
using System.Collections.Specialized;

public partial class administracao_CadastroAvisos : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int codigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

   public bool podeIncluir = false;
    public bool podeEditar = false;
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        
        if (!IsPostBack && !IsCallback)
        {
            cDados.aplicaEstiloVisual(this);
            //MenuUsuarioLogado();
        }

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        carregaGrid();
        defineAlturaTela(resolucaoCliente);
        object[] options = new object[lbDisponiveis.Items.Count];

        for (int i = 0; i < lbDisponiveis.Items.Count; i++)
            options[i] = lbDisponiveis.Items[i].Value;

        hiddenField["options"] = options;
        lbDisponiveis.Style.Add("width", "295px");
        lbSelecionados.Style.Add("width", "276px");

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "INCAVS"))
            podeIncluir = true;
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
    }

    #region GRID

    private void carregaGrid()
    {
        string where = " AND a.CodigoEntidade = '" + codigoEntidade + "'";
        DataSet ds = getAvisos(where);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

        }
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "INCAVS"))
        {
            string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
            captionGrid += string.Format(@"<td align=""left""><img src=""../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
            captionGrid += string.Format(@"</tr></table>");
            gvDados.SettingsText.Title = captionGrid;
        }
    }
    public DataSet getAvisos(string where)
    {
      string  comandoSQL = string.Format(
            @"SELECT 
                A.CodigoAviso,  
                A.Assunto, 
                A.Aviso, 
                A.DataInicio, 
                A.DataTermino, 
                A.DataInclusao ,
                A.CodigoUsuarioInclusao,
                A.CodigoEntidade,
                (SELECT TOP(1) TipoDestinatario 
                   FROM {0}.{1}.AvisoDestinatario 
                  WHERE CodigoAviso = A.CodigoAviso) as tipoDestinatario 
                FROM {0}.{1}.Aviso A
               WHERE 1=1 {2}
               ORDER BY A.DataInicio, A.Assunto", cDados.getDbName(),cDados.getDbOwner(), where);
        return cDados.getDataSet(comandoSQL);
    }


    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 150);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 60;
    }

    //private void MenuUsuarioLogado()
    //{
    //    BarraNavegacao1.MostrarInclusao = false;
    //    BarraNavegacao1.MostrarEdicao = false;
    //    BarraNavegacao1.MostrarExclusao = false;

    //    if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "INCAVS")) 
    //        BarraNavegacao1.MostrarInclusao = true;
    //    if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTAVS")) 
    //        BarraNavegacao1.MostrarEdicao = true;
    //    if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EXCAVS"))
    //        BarraNavegacao1.MostrarExclusao = true;
    //}

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroAvisos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));

        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Avisos</title>"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    private string getChavePrimaria()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAviso").ToString();
    }

    protected void grid_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
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
    }

    // Método responsável por obter os valores que estão preenchidos no formulário
    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        //      
        //

        
        //  oDadosFormulario.Add("DescricaoRiscoPadrao", txtRisco.Text);
        oDadosFormulario.Add("Assunto", txtAssunto.Text);
        oDadosFormulario.Add("Aviso", txtAviso.Text);
        oDadosFormulario.Add("DataInicio", dteInicio.Value.ToString());
        oDadosFormulario.Add("DataTermino", dteTermino.Value.ToString());
        oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("CodigoEntidade", codigoEntidade);
        return oDadosFormulario;
    }
   
    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        // Lê as informações disponíveis no formulário
        string msgErro = "";
        bool retorno = false;

        int[] vetDestinatarios = new int[lbSelecionados.Items.Count];
        for (int i = 0; i < vetDestinatarios.Length; i++)
        {
            vetDestinatarios[i] = int.Parse(lbSelecionados.Items[i].Value.ToString());
        }
        //dteInicio e dteTermino sao configurados sua propriedade displayformatstring e editformatstring para dd/MM/yyyy
        retorno = incluiAviso(txtAssunto.Text.Replace("'",""), txtAviso.Text.Replace("'",""),
             dteInicio.Value.ToString(),
             dteTermino.Value.ToString(),
             idUsuarioLogado,
             codigoEntidade,
             ddlTipoDestinatario.SelectedItem.Value.ToString(), vetDestinatarios, ref msgErro);
        hfCount.Set("QuantidadeSelecionados", 0);
        carregaGrid();

        return msgErro;
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        // Lê as informações disponíveis no formulário
        string msgErro = "";
        bool retorno = false;

        int[] vetDestinatarios = new int[lbSelecionados.Items.Count];
        for (int i = 0; i < vetDestinatarios.Length; i++)
        {
            vetDestinatarios[i] = int.Parse(lbSelecionados.Items[i].Value.ToString());
        }
        int codigoAviso = int.Parse(hfGeral.Get("CodigoAviso").ToString());
        // a configuração displayFormatString e editFormatString me garante que virá sempre o formato dd/MM/yyyy
        //dos componentes dteInicio e dteTermino
        retorno = cDados.atualizaAviso(txtAssunto.Text.Replace("'", ""), txtAviso.Text.Replace("'", ""),
             dteInicio.Value.ToString(),
             dteTermino.Value.ToString(),
             codigoEntidade,
             ddlTipoDestinatario.SelectedItem.Value.ToString(), vetDestinatarios, codigoAviso, ref msgErro);
        hfCount.Set("QuantidadeSelecionados", 0);
        carregaGrid();

        return msgErro;
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        // busca a chave primaria
        string chave = getChavePrimaria();
        cDados.delete("AvisoDestinatario", "CodigoAviso = " + chave);
        cDados.delete("AvisoLido", "CodigoAviso = " + chave);
        cDados.delete("Aviso", "CodigoAviso = " + chave);
        
        carregaGrid();

        return "";
    }

    #endregion

    protected void pnDestinatario_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "popula" && ddlTipoDestinatario.SelectedItem != null)
        {
            if (ddlTipoDestinatario.SelectedItem.Value.ToString() != "TD")
            {
                rdPainelDestinatarios.ClientVisible = true;
                popular(ddlTipoDestinatario.SelectedItem.Value.ToString());
            }
            else
            {
                rdPainelDestinatarios.ClientVisible = false;
                //popular(ddlTipoDestinatario.SelectedItem.Value.ToString());
            }
        }

    }

    private void popular(string tipoDestinatario)
    {
        DataSet ds = null;

        //hfGeral.Set("TipoOperacao", TipoOperacao);
         int codAviso = -1;
        if (hfGeral.Contains("CodigoAviso") && int.TryParse(hfGeral.Get("CodigoAviso").ToString(),out codAviso) == true)
        {
            codAviso = int.Parse(hfGeral.Get("CodigoAviso").ToString());
        }
         
        string tipoOp = hfGeral.Contains("TipoOperacao") == true ? hfGeral.Get("TipoOperacao").ToString() : "";
        if (tipoDestinatario == "UN")
        {
            //popula o conjunto de listboxes com todas as unidades que fazem parte da
            //entidade que o usuario atual ta logado
            //rdPainelDestinatarios.HeaderText = "Unidades destinatárias";
            ds = getUnidadesDestinatarias(codigoEntidade, (tipoOp == "Incluir") ? -1 : codAviso, tipoDestinatario);
        }
        if (tipoDestinatario == "US")
        {
            //popula o conjunto de listboxes com todas as usuarios que fazem parte da
            //entidade que o usuario atual ta logado
            //rdPainelDestinatarios.HeaderText = "Usuários destinatários";
            ds = getUsuariosDestinatarios(codigoEntidade, (tipoOp == "Incluir") ? -1 : codAviso, tipoDestinatario);
        }
        if (tipoDestinatario == "PR")
        {
            //rdPainelDestinatarios.HeaderText = "Projetos destinatáros";
            ds = getProjetosDestinatarios(codigoEntidade, (tipoOp == "Incluir") ? -1 : codAviso, tipoDestinatario);
            //popula o conjunto de listboxes com todas as projetos que fazem parte da
            //entidade que o usuario atual ta logado
        }
        if (tipoDestinatario == "TD")
        {
            ds = cDados.getUsuarios("");
            //esconde o painel de seleção de destinatarios
        }
        popularGeral(ds, codigoEntidade, tipoDestinatario);

    }

    public DataSet getUsuariosDestinatarios(int codigoEntidade, int codigoAviso, string tipoAviso)
    {
       string comandoSQL = string.Format(
       @"SELECT  u.CodigoUsuario as Codigo,
                     u.NomeUsuario as Descricao,
                     'S' AS Destinatario
               FROM {0}.{1}.Usuario u
         INNER JOIN {0}.{1}.UsuarioUnidadeNegocio uun on(u.CodigoUsuario = uun.CodigoUsuario)
         INNER JOIN {0}.{1}.UnidadeNegocio un on(un.CodigoUnidadeNegocio = uun.CodigoUnidadeNegocio)
              WHERE (u.[DataExclusao] IS NULL and un.CodigoEntidade = {2}) AND 
                     u.CodigoUsuario IN(SELECT CodigoDestinatario 
                                          FROM {0}.{1}.AvisoDestinatario
                                         WHERE (CodigoAviso = {3} and TipoDestinatario = '{4}'))
            UNION
            SELECT  u.CodigoUsuario as Codigo,
                     u.NomeUsuario as Descricao,
                     'N' AS Destinatario
               FROM {0}.{1}.Usuario u
         INNER JOIN {0}.{1}.UsuarioUnidadeNegocio uun on(u.CodigoUsuario = uun.CodigoUsuario)
         INNER JOIN {0}.{1}.UnidadeNegocio un on(un.CodigoUnidadeNegocio = uun.CodigoUnidadeNegocio)
              WHERE (u.[DataExclusao] IS NULL and un.CodigoEntidade = {2}) AND 
                     u.CodigoUsuario NOT IN(SELECT CodigoDestinatario 
                                          FROM {0}.{1}.AvisoDestinatario
                                         WHERE (CodigoAviso = {3} and TipoDestinatario = '{4}'))
            ORDER BY Destinatario DESC,Descricao asc", cDados.getDbName() , cDados.getDbOwner(), codigoEntidade, codigoAviso, tipoAviso);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    public DataSet getProjetosDestinatarios(int codigoEntidade, int codigoAviso, string tipoAviso)
    {
        string comandoSQL = string.Format(@"SELECT P.NomeProjeto as Descricao, P.CodigoProjeto as Codigo,
                            'S' AS Destinatario
                       FROM {0}.{1}.Projeto AS P 
                 INNER JOIN {0}.{1}.UnidadeNegocio un on(P.CodigoUnidadeNegocio = un.CodigoUnidadeNegocio) 
                      WHERE P.CodigoEntidade = {2} and P.DataExclusao  is null and
                            P.CodigoProjeto IN(SELECT CodigoDestinatario 
                                                 FROM {0}.{1}.AvisoDestinatario
                                                WHERE (CodigoAviso = {3} AND 
                                                       TipoDestinatario = '{4}'))
                      UNION
                     SELECT P.NomeProjeto as Descricao, P.CodigoProjeto as Codigo,
                            'N' AS Destinatario
                       FROM {0}.{1}.Projeto AS P 
                 INNER JOIN {0}.{1}.UnidadeNegocio un on(P.CodigoUnidadeNegocio = un.CodigoUnidadeNegocio) 
                      WHERE P.CodigoEntidade = {2}  and P.DataExclusao  is null and
                            P.CodigoProjeto NOT IN(SELECT CodigoDestinatario 
                                                     FROM {0}.{1}.AvisoDestinatario
                                                    WHERE (CodigoAviso = {3} AND 
                                                           TipoDestinatario = '{4}'))
                   ORDER BY Destinatario DESC,Descricao asc" , cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, codigoAviso, tipoAviso);

        return cDados.getDataSet(comandoSQL);
    }

    public bool incluiAviso(string assunto, string aviso, string dataInicio, string dataTermino, int codUsuarioInclusao, int codigoEntidade, string tipoDestinatario, int[] codigoDestinatario, ref string msgErro)
    {
        int regAfetados = 0;
        string incluiAvisos = "";
        if (tipoDestinatario == "TD")
        {
            incluiAvisos = string.Format(
            @"INSERT INTO {0}.{1}.AvisoDestinatario(CodigoAviso,TipoDestinatario,CodigoDestinatario)
                                             SELECT @novoCodigo,           '{2}', CodigoUsuario from {0}.{1}.Usuario
            ", cDados.getDbName(), cDados.getDbOwner(), tipoDestinatario);
        }
        else
        {
            for (int i = 0; i < codigoDestinatario.Length; i++)
            {
                incluiAvisos += string.Format(
                @"INSERT INTO {0}.{1}.[AvisoDestinatario] ([CodigoAviso],[TipoDestinatario],[CodigoDestinatario])
                                                VALUES(@novoCodigo, '{2}' ,{3})", cDados.getDbName(), cDados.getDbOwner(), tipoDestinatario, codigoDestinatario[i]);
            }
        }



        string comandoSQL = string.Format(
            @"BEGIN
                DECLARE @novoCodigo as int
                INSERT INTO {0}.{1}.[Aviso]([Assunto],[Aviso],                [DataInicio],               [DataTermino],[DataInclusao],[CodigoUsuarioInclusao],[CodigoEntidade])
                                     VALUES(    '{2}',  '{3}', convert(datetime,'{4}',103), convert(datetime,'{5}',103),     getDate(),                    {6},{7})
                SELECT @novoCodigo = scope_identity()
                {8}
                
              END"
            , cDados.getDbName(), cDados.getDbOwner(), assunto, aviso, dataInicio, dataTermino, codUsuarioInclusao, codigoEntidade, incluiAvisos);
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            msgErro = ex.Message;
            return false;
        }


    }


    public DataSet getUnidadesDestinatarias(int codigoEntidade, int codigoAviso, string tipoAviso)
    {
        string comandoSQL = string.Format(
                              @"SELECT
                un.CodigoUnidadeNegocio as Codigo,
                un.NomeUnidadeNegocio as Descricao,
                'S' AS Destinatario
            FROM {0}.{1}.UnidadeNegocio un
              WHERE (un.IndicaUnidadeNegocioAtiva = 'S'
               AND (un.CodigoEntidade = {2} OR un.CodigoUnidadeNegocio = {2})
               AND un.DataExclusao is null)
                AND  un.CodigoUnidadeNegocio IN(SELECT CodigoDestinatario 
                                                              FROM {0}.{1}.AvisoDestinatario
                                                             WHERE (CodigoAviso = {3} AND TipoDestinatario = '{4}'))


                            UNION
                                SELECT
                un.CodigoUnidadeNegocio as Codigo,
                un.NomeUnidadeNegocio as Descricao,
                'n' AS Destinatario
            FROM {0}.{1}.UnidadeNegocio un
              WHERE (un.IndicaUnidadeNegocioAtiva = 'S'
               AND (un.CodigoEntidade = {2} OR un.CodigoUnidadeNegocio = {2})
               AND un.DataExclusao is null)
              AND un.CodigoUnidadeNegocio NOT IN(SELECT CodigoDestinatario 
                                                              FROM {0}.{1}.AvisoDestinatario
                                                             WHERE (CodigoAviso = {3} AND TipoDestinatario = '{4}'))


                            ORDER BY Destinatario DESC,Descricao asc"
                              , cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, codigoAviso, tipoAviso);
        return cDados.getDataSet(comandoSQL);
    }

    private void popularGeral(DataSet ds, int codigoEntidade, string tipoDestinatario)
    {
        int cod = -1;
        if (hfGeral.Contains("CodigoAviso") && int.TryParse(hfGeral.Get("CodigoAviso").ToString(), out cod) == true)
        {
            cod = int.Parse(hfGeral.Get("CodigoAviso").ToString());
        }

        hfCount.Set("QuantidadeSelecionados", 0);
        //DataSet ds = cDados.getParticipantesComite(cod, "");
        //pega os participantes e o náo participantes do comite
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //pega somente a quantidade total de lbSelecionados da direita
            hfCount.Set("QuantidadeSelecionados", ds.Tables[0].Select("Destinatario = 'S'").Length);

            //a principio preenche o lbDisponivel com todos os usuarios
            //sendo ele participante ou nao
            //preenche com todos os usuários
            lbDisponiveis.DataSource = ds.Tables[0];
            lbDisponiveis.TextField = "Descricao";
            lbDisponiveis.ValueField = "Codigo";
            lbDisponiveis.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTAVS"))
            podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EXCAVS"))
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
}
