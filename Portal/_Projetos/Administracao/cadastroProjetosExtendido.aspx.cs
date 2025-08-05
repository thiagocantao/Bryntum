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
using System.Drawing;

public partial class _cadastroProjetosExtendido : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public bool podeEditarGrids = false;

    int codigoProjeto = -1;
    bool somenteLeitura = false;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        if (!IsPostBack && !IsCallback)
            hfGeral.Set("CodigoProjeto", "-1");

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/cadastroProjetos.js""></script>"));
        this.TH(this.TS("cadastroProjetos"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            hfGeral.Set("CodigoProjeto", Request.QueryString["IDProjeto"].ToString());
        }

        if (Request.QueryString["PopUP"] != null && Request.QueryString["PopUP"].ToString() == "S")
        {
            btnCancelar.ClientVisible = true;
        }
        else
        {
            defineAlturaTela();
        }

        codigoProjeto = hfGeral.Get("CodigoProjeto").ToString() != "" ? int.Parse(hfGeral.Get("CodigoProjeto").ToString()) : -1;

        string sWhere = string.Format(@" 
                AND EXISTS( SELECT 1 FROM {0}.{1}.RecursoCorporativo AS rc WHERE rc.[CodigoEntidade] = {2} and rc.[CodigoTipoRecurso] = 1 and rc.[CodigoUsuario] = us.CodigoUsuario AND rc.[DataDesativacaoRecurso] IS NULL ) ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);

        carregaComboUsuarios(ddlGerenteProjeto, "");
        carregaComboUsuarios(ddlResponsavelEtapa, sWhere);
        carregaComboEntidadesDisponiveis();
        carregaComboObjetivosDisponiveis();

        if (codigoProjeto != -1)
        {
            if (!IsPostBack)
                carregaDadosProjeto();

            // se veio código do projeto na URL, trata-se de consulta à tela ou alteração
            if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
                podeEditarGrids = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltCaract");
            else
                podeEditarGrids = true; // se não veio código do projeto na URL, é por que trata-se de uma inclusão. Permitimos então a edição da grid.

            somenteLeitura = !podeEditarGrids;

            carregaGridObjetivos();
            carregaGridEntidades();
            carregaGridEtapas();
        }

        DataSet ds = cDados.getParametrosSistema("labelGerente");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string labelGerente = ds.Tables[0].Rows[0]["labelGerente"] + "" != "" ? ds.Tables[0].Rows[0]["labelGerente"] + "" : "Gerente";

            lblGerenteProjeto.Text = labelGerente + ":";
        }

        cDados.aplicaEstiloVisual(Page);

        gvEntidades.JSProperties["cp_Msg"] = "";
        gvEntidades.JSProperties["cp_AtualizaCombo"] = "N";
        gvObjetivos.JSProperties["cp_Msg"] = "";
        gvObjetivos.JSProperties["cp_AtualizaCombo"] = "N";
        gvEtapas.JSProperties["cp_Msg"] = "";
        gvEtapas.JSProperties["cp_LimpaCampos"] = "N";

        if (somenteLeitura)
            desabilitaCampos();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        int alturaGrid = (alturaPrincipal - 555);

        if (alturaGrid > 100)
            gvEtapas.Settings.VerticalScrollableHeight = alturaGrid;
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgGravacao = "";
        callbackSalvar.JSProperties["cp_NovoCodigoProjeto"] = "";
        string statusGravacao = "0";
        bool resposta;

        string nomeProjeto = txtNomeProjeto.Text;
        string escopo = txtEscopo.Text;
        string resultados = txtResultados.Text;
        string gerente = ddlGerenteProjeto.SelectedIndex == -1 ? "NULL" : ddlGerenteProjeto.Value.ToString();
        string dataInicio = ddlInicio.Text == "" ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date);
        string dataTermino = ddlTermino.Text == "" ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date);
        string valorOrcamento = txtValorOrcamento.Value.ToString();
        string moeda = ddlMoeda.Value.ToString();

        if (codigoProjeto == -1)
        {
            if (cDados.verificaExistenciaNomeTabelaProjeto(codigoEntidadeUsuarioResponsavel, txtNomeProjeto.Text, ""))
            {
                msgGravacao = "Já existe um projeto cadastrado com este nome. <br>Altere o nome do projeto e tente incluir novamente!";
            }
            else
            {
                int novoCodigoProjeto = 0;               

                resposta = cDados.incluiDadosProjetoExtendido(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, nomeProjeto, escopo, resultados, gerente, dataInicio, dataTermino
                    , valorOrcamento, moeda, out novoCodigoProjeto, out msgGravacao);

                if (resposta)
                {
                    statusGravacao = "1";
                    msgGravacao = "Projeto incluído com sucesso!";
                    callbackSalvar.JSProperties["cp_NovoCodigoProjeto"] = novoCodigoProjeto;
                }
            }
        }
        else
        {
            if (cDados.verificaExistenciaNomeTabelaProjeto(codigoEntidadeUsuarioResponsavel, txtNomeProjeto.Text, " AND p.CodigoProjeto <> " + codigoProjeto))
            {
                msgGravacao = "Já existe um projeto cadastrado com este nome. <br>Altere o nome do projeto e tente salvar novamente!";
            }
            else
            {
                resposta = cDados.atualizaDadosProjetoExtendido(codigoProjeto, codigoUsuarioResponsavel, nomeProjeto, escopo, resultados, gerente, dataInicio, dataTermino
                    , valorOrcamento, moeda, out msgGravacao);

                if (resposta)
                {
                    statusGravacao = "1";
                    msgGravacao = "Projeto alterado com sucesso!";
                }
            }
        }

        callbackSalvar.JSProperties["cp_Status"] = statusGravacao;
        callbackSalvar.JSProperties["cp_Msg"] = msgGravacao;
    }

    private void carregaComboUsuarios(ASPxComboBox combo,string where)
    {
        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            combo.ValueField = "CodigoUsuario";
            combo.TextField = "NomeUsuario";

            combo.DataSource = ds.Tables[0];
            combo.DataBind();
        }
    }
    
    private void carregaComboEntidadesDisponiveis()
    {
        DataSet ds = cDados.getParceirosDisponiveisProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            ddlNovaEntidade.ValueField = "CodigoUsuario";
            ddlNovaEntidade.TextField = "NomeUsuario";

            ddlNovaEntidade.DataSource = ds.Tables[0];
            ddlNovaEntidade.DataBind();
        }
    }

    private void carregaComboObjetivosDisponiveis()
    {
        DataSet ds = cDados.getObjetivosDisponiveisProjeto(codigoProjeto, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            ddlNovoObjetivo.ValueField = "CodigoObjetoEstrategia";
            ddlNovoObjetivo.TextField = "DescricaoObjetoEstrategia";

            ddlNovoObjetivo.DataSource = ds.Tables[0];
            ddlNovoObjetivo.DataBind();
        }
    }

    private void carregaDadosProjeto()
    {
        DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataRow dr = dsDados.Tables[0].Rows[0];

            txtNomeProjeto.Text = dr["NomeProjeto"].ToString();
            txtEscopo.Text = dr["DescricaoProposta"].ToString();
            txtResultados.Text = dr["Memo1"].ToString();
            if (dr["CodigoGerente"].ToString() != "")
                ddlGerenteProjeto.Value = int.Parse(dr["CodigoGerente"].ToString());
            if (dr["InicioProposta"].ToString() != "")
                ddlInicio.Date = DateTime.Parse(dr["InicioProposta"].ToString());
            if (dr["TerminoProposta"].ToString() != "")
                ddlTermino.Date = DateTime.Parse(dr["TerminoProposta"].ToString());
            ddlMoeda.Value = dr["texto1"].ToString();
            txtValorOrcamento.Text = dr["Valor1"].ToString();


        }
    }

    private void desabilitaCampos()
    {
        txtNomeProjeto.ClientEnabled = false;
        txtEscopo.ClientEnabled = false;
        txtResultados.ClientEnabled = false;
        ddlGerenteProjeto.ClientEnabled = false;
        ddlInicio.ClientEnabled = false;
        ddlTermino.ClientEnabled = false;
        txtValorOrcamento.ClientEnabled = false;
        ddlMoeda.ClientEnabled = false;
        podeEditarGrids = false;
        btnSalvar.ClientVisible = false;
    }

    private void carregaGridObjetivos()
    {
        DataSet ds = cDados.getObjetivosAssociadosProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvObjetivos.DataSource = ds;
            gvObjetivos.DataBind();
        }
    }

    private void carregaGridEntidades()
    {
        DataSet ds = cDados.getParceirosAssociadosProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvEntidades.DataSource = ds;
            gvEntidades.DataBind();
        }
    }

    private void carregaGridEtapas()
    {
        DataSet ds = cDados.getEtapasProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvEtapas.DataSource = ds;
            gvEtapas.DataBind();
        }
        
        txtSequenciaEtapa.MaskSettings.Mask = "<1.." + (gvEtapas.VisibleRowCount + 1) + ">";
        gvEtapas.JSProperties["cp_ProximoSequencia"] = (gvEtapas.VisibleRowCount + 1).ToString();
    }

    protected void gvEntidades_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        bool resultado = false;
        string msgRetorno = "";

        if (e.Parameters.ToString() == "I")
        {
            resultado = cDados.insereAssociacaoParceiroProjeto(codigoProjeto, int.Parse(ddlNovaEntidade.Value.ToString()), codigoUsuarioResponsavel, ref msgRetorno);
        }
        else if (e.Parameters.ToString() != "")
        {
            int codigoParceiro = int.Parse(e.Parameters.ToString());

            resultado = cDados.excluiAssociacaoParceiroProjeto(codigoProjeto, codigoParceiro, codigoUsuarioResponsavel, ref msgRetorno);

            if (resultado)
                msgRetorno = "Associação excluída com sucesso!";
        }

        if (resultado)
        {
            carregaGridEntidades();
            gvEntidades.JSProperties["cp_AtualizaCombo"] = "S";
        }

        gvEntidades.JSProperties["cp_Msg"] = msgRetorno;
    }

    protected void gvObjetivos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        bool resultado = false;
        string msgRetorno = "";

        if (e.Parameters.ToString() == "I")
        {
            resultado = cDados.insereAssociacaoObjetivoProjeto(codigoProjeto, int.Parse(ddlNovoObjetivo.Value.ToString()), ckObjPrincipal.Checked ? 'S' : 'N', ref msgRetorno);
        }
        else if (e.Parameters.ToString() != "")
        {
            int codigoObjetivo = int.Parse(e.Parameters.ToString());

            resultado = cDados.excluiAssociacaoObjetivoProjeto(codigoProjeto, codigoObjetivo, ref msgRetorno);

            if (resultado)
                msgRetorno = "Associação excluída com sucesso!";
        }

        if (resultado)
        {
            carregaGridObjetivos();
            gvObjetivos.JSProperties["cp_AtualizaCombo"] = "S";
        }

        gvObjetivos.JSProperties["cp_Msg"] = msgRetorno;
    }

    protected void gvEtapas_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        bool resultado = false;
        string msgRetorno = "";

        if (e.Parameters.ToString() == "-1")
        {
            resultado = cDados.insereTarefaProjeto(codigoProjeto, int.Parse(txtSequenciaEtapa.Text), txtNomeEtapa.Text, ddlInicioEtapa.Date, ddlTerminoEtapa.Date
                , Convert.ToInt32(sePercentConcluido.Value), int.Parse(ddlResponsavelEtapa.Value.ToString()), codigoUsuarioResponsavel, ref msgRetorno);

            if(resultado)
                gvEtapas.JSProperties["cp_LimpaCampos"] = "S";
        }
        else if (e.Parameters.ToString() != "")
        {
            if (e.Parameters.ToString().Substring(0, 1) == "X")
            {
                int codigoEtapa = int.Parse(e.Parameters.ToString().Substring(1));

                resultado = cDados.excluiTarefaProjeto(codigoProjeto, codigoEtapa, codigoUsuarioResponsavel, ref msgRetorno);

                if (resultado)
                    msgRetorno = "Etapa excluída com sucesso!";
            }
            else
            {
                int codigoEtapa = int.Parse(e.Parameters.ToString());

                resultado = cDados.atualizaTarefaProjeto(codigoProjeto, codigoEtapa, int.Parse(txtSequenciaEtapa.Text), txtNomeEtapa.Text, ddlInicioEtapa.Date, ddlTerminoEtapa.Date
                , Convert.ToInt32(sePercentConcluido.Value), int.Parse(ddlResponsavelEtapa.Value.ToString()), codigoUsuarioResponsavel, ref msgRetorno);

                if (resultado)
                    msgRetorno = "Etapa alterada com sucesso!";
            }
        }

        if (resultado)
        {
            carregaGridEtapas();
        }

        gvEtapas.JSProperties["cp_Msg"] = msgRetorno;
    }

    protected void gvObjetivos_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            string indicaPrincipal = gvObjetivos.GetRowValues(e.VisibleIndex, "IndicaObjetivoEstrategicoPrincipal").ToString();

            if (indicaPrincipal == "S")
                e.Row.ForeColor = Color.FromName("#008000");
        }
    }

    public string getBotoesEdicaoTarefas(string codigoEtapa, string numeroSequencia, string nomeEtapa, string dataInicio, string dataTermino, string codigoResponsavel, string percentConcluido)
    {
        string retorno = "";

        string btnExcluir = (podeEditarGrids) ? string.Format("<img alt='Excluir' src='../../imagens/botoes/excluirReg02.PNG' style='cursor: pointer;' onclick='excluiEtapa({0})' />", codigoEtapa)
                                                                                                : string.Format("<img alt='' src='../../imagens/botoes/excluirRegDes.PNG' style='cursor: default;' />");

        try
        {
            dataInicio = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dataInicio));
            dataTermino = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dataTermino));
        }
        catch { }

        string btnEditar = (podeEditarGrids) ? string.Format(@"<img alt='Editar' src='../../imagens/botoes/editarReg02.PNG' style='cursor: pointer;' onclick=""editaEtapa({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6})"" />", codigoEtapa, numeroSequencia, nomeEtapa, dataInicio, dataTermino, codigoResponsavel, percentConcluido)
                                                                                                : string.Format("<img alt='' src='../../imagens/botoes/editarRegDes.PNG' style='cursor: default;' />");

        retorno = string.Format(@"<table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td style=""padding-right:5px;"">
                                            {0}
                                        </td>
                                        <td>
                                            {1}
                                        </td>
                                    </tr>
                                 </table>", btnEditar, btnExcluir);

        return retorno;
    }
}
