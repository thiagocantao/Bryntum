/*
 31/01/2011 - Alejandro : alteração do método [private void onHearderTela()], adiciono a linha para evitar uso de cache na tela.
 
 
 
 */
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

public partial class _Projetos_DadosProjeto_novaMensagem : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private string resolucaoCliente = "";
    private int codigoEntidade = -1;
    private int codigoProjeto = -1;
    private int alturaPrincipal = 0;
    private int codigoAlertaTarefa = -1;
    private int codigoAlerta = -1;
    private string tipoEdicao = "";
    private string nomeTarefa = "";

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dsTarefas.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        onHearderTela();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            lbListaDisponiveis.SelectionMode = ListEditSelectionMode.CheckColumn;
            lbListaSelecionados.SelectionMode = ListEditSelectionMode.CheckColumn;
            hfGeral.Set("CodigosDestinatariosSelecionados", "-1");
        }

        if (Request.QueryString["CP"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        if (Request.QueryString["CAT"] != null)
        {
            codigoAlertaTarefa = int.Parse(Request.QueryString["CAT"].ToString());
        }

        if (Request.QueryString["CA"] != null)
        {
            codigoAlerta = int.Parse(Request.QueryString["CA"].ToString());
        }
        if (Request.QueryString["NT"] != null)
        {
            nomeTarefa = Request.QueryString["NT"].ToString();
        }
        ddlTarefas.JSProperties["cp_RO"] = "N";
        
        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            desabilitaCampos();
            ddlTarefas.JSProperties["cp_RO"] = "S";
        }

        ddlTarefas.ToolTip = cDados.getTextoTraduzido("AssociacaoTarefasAlerta_tipos_de_tarefas_listadas", null);

        if (Request.QueryString["TE"] != null && Request.QueryString["TE"].ToString() + "" != "")
        {
            tipoEdicao = Request.QueryString["TE"].ToString();
        }

        carregaComboTarefas();

        if (!IsPostBack)
        {
            carregaListaAssociados();
            carregaListaDisponiveis();
            
        }

        

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (codigoAlertaTarefa != -1 && !IsPostBack)
            carregaCampos();
        
        //DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
    }

    private void onHearderTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AssociacaoTarefasAlerta.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("AssociacaoTarefasAlerta", "_Strings", "ASPxListbox"));
    }    
    
    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);        
    }

    private void desabilitaCampos()
    {
        ddlTarefas.ClientEnabled = false;
        btnADD0.ClientEnabled = false;
        btnADDTodos0.ClientEnabled = false;
        btnRMV0.ClientEnabled = false;
        btnRMVTodos0.ClientEnabled = false;
        btnSalvar.ClientVisible = false;
        lbListaDisponiveis.ClientEnabled = false;
        lbListaSelecionados.ClientEnabled = false;
    }

    private void carregaCampos()
    {
        DataSet ds = cDados.getAlertaTarefa(codigoAlertaTarefa, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlTarefas.Value = ds.Tables[0].Rows[0]["CodigoTarefa"].ToString();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        
    }

    private void carregaListaDisponiveis()
    {
        DataSet ds = cDados.getUsuariosDisponiveisAlerta(codigoAlertaTarefa, codigoProjeto, "");

        lbListaDisponiveis.DataSource = ds.Tables[0];
        lbListaDisponiveis.DataBind();

        if (lbListaDisponiveis.Items.Count > 0)
        {
            lbListaDisponiveis.SelectedIndex = -1;
            btnADDTodos0.ClientEnabled = true;
        }
    }

    private void carregaListaAssociados()
    {
        DataSet ds = cDados.getUsuariosAssociadosAlerta(codigoAlertaTarefa, codigoProjeto, "");

        lbListaSelecionados.DataSource = ds.Tables[0];
        lbListaSelecionados.DataBind();

        if (lbListaSelecionados.Items.Count > 0)
        {
            lbListaSelecionados.SelectedIndex = -1;
            btnRMVTodos0.ClientEnabled = true;
        }
    }

    private void carregaComboTarefas()
    {
        DataSet ds = getTarefasDisponiveisAlertasCronograma_Local(codigoAlertaTarefa, codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            ddlTarefas.DataSource = ds;
            ddlTarefas.DataBind();
        }
        if (tipoEdicao == "I")
        {
            ddlTarefas.ClientVisible = true;
            txtTarefa.ClientVisible = false;
        }
        else
        {
            txtTarefa.ClientVisible = true;
            ddlTarefas.ClientVisible = false;
            txtTarefa.Text = nomeTarefa;
        }
    }


    public DataSet getTarefasDisponiveisAlertasCronograma_Local(int codigoAlertaTarefa, int codigoProjeto, string where)
    {
        string comandoSQL = string.Format(
                  @"SELECT tcp.CodigoTarefa, 
                            CONVERT(varchar,tcp.SequenciaTarefaCronograma,1) + ' - ' + tcp.NomeTarefa as NomeTarefa,                       
                           tcps.NomeTarefa as TarefaSuperior 
                      FROM {0}.{1}.TarefaCronogramaProjeto tcp
                 left JOIN {0}.{1}.TarefaCronogramaProjeto tcps on (tcps.CodigoTarefa = tcp.CodigoTarefaSuperior
                                                                and tcps.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto)
                INNER JOIN {0}.{1}.CronogramaProjeto cp ON (cp.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto
															                    AND cp.CodigoProjeto = {3})                
                     WHERE tcp.IndicaTarefaResumo = 'N'
                       AND tcp.DataExclusao IS NULL
                       AND tcp.PercentualFisicoConcluido < 100
                       AND tcp.CodigoTarefa NOT IN(SELECT at.CodigoTarefa 
													 FROM {0}.{1}.AlertaTarefa at
                     inner join CronogramaProjeto cp on (cp.CodigoCronogramaProjeto = at.CodigoCronogramaProjeto)
                     inner join Projeto p on (cp.CodigoProjeto = p.CodigoProjeto)
                     										WHERE cp.CodigoProjeto = {3} and at.CodigoAlerta = {2})
                      {4}
                     ORDER BY tcp.SequenciaTarefaCronograma
               ", cDados.getDbName(),cDados.getDbOwner(), codigoAlerta, codigoProjeto, where);
        return cDados.getDataSet(comandoSQL);
    }

    protected void ddlTarefas_ItemRequestedByValue1(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;

            string comandoSQL = string.Format(
                 @"SELECT 
                      tcp.CodigoTarefa, 
                      tcps.NomeTarefa as TarefaSuperior,
                      CONVERT(varchar,tcp.SequenciaTarefaCronograma,1) + ' - ' + tcp.NomeTarefa as NomeTarefa                       
                      FROM {0}.{1}.TarefaCronogramaProjeto tcp 
                 LEFT JOIN {0}.{1}.TarefaCronogramaProjeto tcps on (tcps.CodigoTarefa = tcp.CodigoTarefaSuperior and
                                                                    tcps.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto)
                INNER JOIN {0}.{1}.CronogramaProjeto cp ON (cp.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto
															                    AND cp.CodigoProjeto = @CodigoProjeto)

                     WHERE tcp.IndicaTarefaResumo = 'N'
                       AND tcp.DataExclusao IS NULL
                       AND tcp.PercentualFisicoConcluido < 100
                       AND tcp.CodigoTarefa NOT IN(SELECT at.CodigoTarefa 
													 FROM {0}.{1}.AlertaTarefa at
                     inner join CronogramaProjeto cp on (cp.CodigoCronogramaProjeto = at.CodigoCronogramaProjeto)
                     inner join Projeto p on (cp.CodigoProjeto = p.CodigoProjeto)
                     										WHERE cp.CodigoProjeto = {3} and at.CodigoAlerta = {2})
                     ORDER BY tcp.SequenciaTarefaCronograma
               ", cDados.getDbName(), cDados.getDbOwner(), codigoAlerta, codigoProjeto, "");


            dsTarefas.SelectCommand = comandoSQL;

            dsTarefas.SelectParameters.Clear();

            dsTarefas.SelectParameters.Add("CodigoProjeto", TypeCode.Int32, e.Value.ToString());

            dsTarefas.SelectParameters.Add("CodigoAlerta", TypeCode.Int32, codigoAlerta.ToString());

            comboBox.DataSource = dsTarefas;
            comboBox.DataBind();
        }
    }
    protected void ddlTarefas_ItemsRequestedByFilterCondition1(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {       
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL2 =
            string.Format(@"SELECT ROW_NUMBER() over (order by tcps.NomeTarefa) as rn,  
                                   tcp.CodigoTarefa, 
                                   tcps.NomeTarefa as TarefaSuperior, 
                                   CONVERT(varchar,tcp.SequenciaTarefaCronograma,1) + ' - ' + tcp.NomeTarefa as NomeTarefa
                              FROM {0}.{1}.TarefaCronogramaProjeto tcp 
                         left JOIN {0}.{1}.TarefaCronogramaProjeto tcps on (tcps.CodigoTarefa = tcp.CodigoTarefaSuperior 
                                                                            and tcps.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto) 
                        INNER JOIN {0}.{1}.CronogramaProjeto cp ON (cp.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto
															                    AND cp.CodigoProjeto = {3})

                             WHERE tcp.IndicaTarefaResumo = 'N'
                               AND tcp.PercentualFisicoConcluido < 100
                               AND tcp.NomeTarefa like '%{2}%'
                               AND tcp.DataExclusao IS NULL
                               AND tcp.CodigoTarefa NOT IN(SELECT at.CodigoTarefa 
													 FROM {0}.{1}.AlertaTarefa at
                     inner join CronogramaProjeto cp on (cp.CodigoCronogramaProjeto = at.CodigoCronogramaProjeto)
                     inner join Projeto p on (cp.CodigoProjeto = p.CodigoProjeto)
                     										WHERE cp.CodigoProjeto = {3} and at.CodigoAlerta = {4})
", cDados.getDbName(), cDados.getDbOwner(), e.Filter, codigoProjeto, codigoAlerta);

        dsTarefas.SelectCommand = comandoSQL2;
        dsTarefas.SelectParameters.Clear();
        //dsTarefas.SelectParameters.Add("CodigoAlertaTarefa", TypeCode.Int32, codigoAlertaTarefa.ToString());

        cDados.populaComboVirtualGeral(dsTarefas, comandoSQL2, comboBox, e.BeginIndex, e.EndIndex);
         
    }
    
    
    protected void cbSalvar_Callback(object source, CallbackEventArgs e)
    {
        cbSalvar.JSProperties["cp_ErroSalvar"] = "";
        
        string msgErro = "";
        string[] listaDestinatario = null;

        if (hfGeral.Contains("CodigosDestinatariosSelecionados"))
            listaDestinatario = hfGeral.Get("CodigosDestinatariosSelecionados").ToString().Split(';');

        if (codigoAlertaTarefa == -1)
        {
            bool incluiu = cDados.incluiTarefaAlerta(codigoAlerta, int.Parse(ddlTarefas.Value.ToString()), codigoProjeto, listaDestinatario, ref msgErro);
            cbSalvar.JSProperties["cp_ErroSalvar"] = msgErro;
           
        }
        else
        {
            bool alterou = cDados.atualizaTarefaAlerta(codigoAlertaTarefa, int.Parse(ddlTarefas.Value.ToString()), listaDestinatario, ref msgErro);
            cbSalvar.JSProperties["cp_ErroSalvar"] = msgErro;
        }
    }
}
