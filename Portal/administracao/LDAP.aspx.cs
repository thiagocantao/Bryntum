using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections.Specialized;
using System.Data;

public partial class administracao_LDAP : System.Web.UI.Page
{
    dados cDados;
    private bool podeAdministrar = false;

    private string nomeTabelaDb = "ParametroConfiguracaoSistema";
    private string whereUpdateDelete;
    private string resolucaoCliente = "";

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int alturaPrincipal = 0;



    string[] NumerosCardinais = new string[] {"Primeiro",
"Segundo",
"Terceiro",
"Quarto",
"Quinto",
"Sexto",
"Sétimo",
"Oitavo",
"Nono",
"Décimo",
"décimo primeiro",
"décimo segundo",
"décimo terceiro",
"Décimo quarto",
"Décimo quinto",
"Décimo sexto",
"Décimo sétimo",
"Décimo oitavo",
"Décimo nono",
"Vigésimo",
"Vigésimo primeiro",
"Vigésimo segundo",
"Vigésimo terceiro",
"Vigésimo quarto",
"Vigésimo quinto",
"Vigésimo sexto",
"Vigésimo sétimo",
"Vigésimo oitavo",
"Vigésimo nono",
"Trigésimo",
"Trigésimo primeiro",
"Trigésimo segundo",
"Trigésimo terceiro",
"Trigésimo quarto",
"Trigésimo quinto",
"Trigésimo sexto",
"Trigésimo sétimo",
"Trigésimo oitavo",
"Trigésimo nono",
"Quadragésimo",
"Quadragésimo primeiro",
"Quadragésimo segundo",
"Quadragésimo terceiro",
"Quadragésimo quarto",
"Quadragésimo quinto",
"Quadragésimo sexto",
"Quadragésimo sétimo",
"Quadragésimo oitavo",
"Quadragésimo nono",
"Quinquagésimo",
"Quinquagésimo primeiro",
"Quinquagésimo segundo",
"Quinquagésimo terceiro",
"Quinquagésimo quarto",
"Quinquagésimo quinto",
"Quinquagésimo sexto",
"Quinquagésimo sétimo",
"Quinquagésimo oitavo",
"Quinquagésimo nono",
"Sexagésimo",
"Sexagésimo primeiro",
"Sexagésimo segundo",
"Sexagésimo terceiro",
"Sexagésimo quarto",
"Sexagésimo quinto",
"Sexagésimo sexto",
"Sexagésimo sétimo",
"Sexagésimo oitavo",
"Sexagésimo nono",
"Septuagésimo",
"Septuagésimo primeiro",
"Septuagésimo segundo",
"Septuagésimo terceiro",
"Septuagésimo quarto",
"Septuagésimo quinto",
"Septuagésimo sexto",
"Septuagésimo sétimo",
"Septuagésimo oitavo",
"Septuagésimo nono",
"Octogésimo",
"Octogésimo primeiro",
"Octogésimo segundo",
"Octogésimo terceiro",
"Octogésimo quarto",
"Octogésimo quinto",
"Octogésimo sexto",
"Octogésimo sétimo",
"Octogésimo oitavo",
"Octogésimo nono",
"Nonagésimo",
"Nonagésimo primeiro",
"Nonagésimo segundo",
"Nonagésimo terceiro",
"Nonagésimo quarto",
"Nonagésimo quinto",
"Nonagésimo sexto",
"Nonagésimo sétimo",
"Nonagésimo oitavo",
"Nonagésimo nono" };


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
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/LDAP.js"" ></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js"" ></script>"));       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js"" ></script>"));
        this.TH(this.TS("barraNavegacao", "LDAP", "_Strings"));

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "SI_CnsPmt");
            cDados.aplicaEstiloVisual(Page);
        }

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        //dbo.f_GetPermissoesUsuario(CodigoObjeto, IniciaisObjeto, CodigoObjetoPai, CodigoEntidade, CodigoUsuarioInteressado, IniciaisTipoPermissao, CodigoUsuarioOtorgante, ListaPerfis, HerdaPermissao)
        podeAdministrar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "SI_AltPmt");
        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        populaGrid();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        string comandoSQL = string.Format(@"
SELECT CodigoParametro
                      ,Parametro
                      ,Valor
                      ,CASE WHEN TipoDadoParametro = 'BOL' THEN CASE WHEN Valor = 'S' THEN 'Sim' ELSE 'Não' END
                            WHEN TipoDadoParametro = 'LOG' THEN CASE WHEN Valor = '1' THEN 'Sim' ELSE 'Não' END
                        ELSE Valor END AS DescricaoValor
                      ,DescricaoParametro_PT
                      ,DescricaoParametro_EN
                      ,DescricaoParametro_ES
                      ,TipoDadoParametro
                      ,ValorMinimo
                      ,ValorMaximo
                      ,CodigoConjuntoOpcaoParametro
                      ,GrupoParametro
                      ,IndicaControladoSistema
                  FROM {0}.{1}.ParametroConfiguracaoSistema
                 WHERE  CodigoEntidade = {2}
                    and GrupoParametro = 'LDAP/AD' and Parametro like 'LDAP_AD_DN_%' ", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 200;
    }

    private ListDictionary getDadosFormulario(string Modo)
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        string cardinalUtilizado = "";
        if (Modo == "Incluir")
        {
            int sequencial = 0;

            string comandoSQL = string.Format(@"
          SELECT cast(isnull(count(*),0) as int) as qtd FROM {0}.{1}.ParametroConfiguracaoSistema
           WHERE GrupoParametro = 'LDAP/AD' 
             AND CodigoEntidade = {2} 
             AND Parametro like 'LDAP_AD_DN_%'", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                sequencial = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                cardinalUtilizado = NumerosCardinais[sequencial];
            }

            string str_sequencial = ((sequencial + 1).ToString().Length == 1) ? "0" + (sequencial + 1).ToString() : (sequencial + 1).ToString();


            //Define o yyy complemento do Distinct Name de pesquisa no LDAP

            string descricaoParametro = string.Format(@"Define o {0} complemento do Distinct Name de pesquisa no LDAP", cardinalUtilizado);

            oDadosFormulario.Add("Parametro", "LDAP_AD_DN_" + str_sequencial);
            oDadosFormulario.Add("Valor", txtValorTXT.Text);

            oDadosFormulario.Add("DescricaoParametro_PT", memoDescricao.Text);
            oDadosFormulario.Add("DescricaoParametro_EN", memoDescricao.Text);
            oDadosFormulario.Add("DescricaoParametro_ES", memoDescricao.Text);

            oDadosFormulario.Add("IndicaControladoSistema", "N");

            oDadosFormulario.Add("GrupoParametro", "LDAP/AD");
            oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);

        }
        else
        {

            oDadosFormulario.Add("Valor", txtValorTXT.Text);

            oDadosFormulario.Add("DescricaoParametro_PT", memoDescricao.Text);
            oDadosFormulario.Add("DescricaoParametro_EN", memoDescricao.Text);
            oDadosFormulario.Add("DescricaoParametro_ES", memoDescricao.Text);

        }
        
        
        
        return oDadosFormulario;
    }

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario("Editar");

            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteInclusaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario("Incluir");
            int ultimoRegistroIncluido = -1;
            ultimoRegistroIncluido = cDados.insert(nomeTabelaDb, oDadosFormulario, true);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion


    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string IndicaControladoSistema = (gvDados.GetRowValues(e.VisibleIndex, "IndicaControladoSistema") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString() : "";
        if (e.ButtonID == "btnEditar")
        {

            if (!podeAdministrar)
            {
                e.Enabled = false;
                e.Text = "Edição não disponível";
                e.Image.Url = "~/imagens/botoes/editarReg02.png";
            }
            else
            {
                if (IndicaControladoSistema == "S")
                {
                    e.Image.Url = "~/imagens/botoes/pFormulario.png";
                    e.Image.ToolTip = "Visualizar";
                }
                else
                {
                    e.Image.Url = "~/imagens/botoes/editarReg02.png";
                    e.Image.ToolTip = "Alterar";
                }
            }
        }
    }


    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ConfigGerSis");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        string script_incluir = @"onClickBarraNavegacao(""Incluir"", " + gvDados.ClientInstanceName + ", pcDados);";
	    script_incluir += @"hfGeral.Set(""TipoOperacao"", ""Incluir"");";
	    script_incluir +=  @"TipoOperacao = ""Incluir"";";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, script_incluir, true, true, false, "ConfigGerSis", lblTituloTela.Text, this);
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
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.ErrorTextKind == GridErrorTextKind.General)
        {
            e.ErrorText = e.Exception.Message;
        }
        else
        {
            e.ErrorText = "Erro de validação: " + e.ErrorText;
        }
    }
    protected void pnCallbackPopup_Callback(object sender, CallbackEventArgsBase e)
    {
        txtValorTXT.Text = "";
        txtValorTXT.Enabled = true;
        int sequencial = 0;
        string cardinalUtilizado = "";
        string comandoSQL = string.Format(@"
          SELECT cast(isnull(count(*),0) as int) as qtd FROM {0}.{1}.ParametroConfiguracaoSistema
           WHERE GrupoParametro = 'LDAP/AD' 
             AND CodigoEntidade = {2} 
             AND Parametro like 'LDAP_AD_DN_%'", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            sequencial = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            cardinalUtilizado = NumerosCardinais[sequencial];
        }

        string str_sequencial = (sequencial.ToString().Length == 1) ? "0" + sequencial.ToString() : sequencial.ToString();


        //Define o yyy complemento do Distinct Name de pesquisa no LDAP

        string descricaoParametro = string.Format(@"Define o {0} complemento do Distinct Name de pesquisa no LDAP", cardinalUtilizado);

        memoDescricao.Text = descricaoParametro;
    }
}