using System;
using System.Data;
using System.Web;
using System.Globalization;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class espacoTrabalho_DetalhesTS : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string idAtribucao = "";

    private string bloqueio = "N";

    protected void Page_Init(object sender, EventArgs e)
    {
        var culture = CultureInfo.CurrentCulture;

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        if (Request.QueryString["CA"] != null)
            idAtribucao = Request.QueryString["CA"].ToString();
        if (!IsPostBack)
        {
            carregaGrid();
        }
    }

    protected void Page_PreInit()
    {
        gvDados.EnableTheming = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("geral", "DetalhesTS", "espacoTrabalho_DetalhesTS", "CDIS", "kanban"));
        try
        {
            int aux = int.Parse(idAtribucao);
        }
        catch
        {
            pcDados.TabPages[4].ClientVisible = false;
        }

        if (Request.QueryString["B"] != null) // Bloqueio (Nâo pode editar)
        {
            bloqueio = Request.QueryString["B"].ToString();
        }

        if (bloqueio != "S")
        {
            bloqueio = "N";
        }

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack && !IsCallback)
        {
            DataTable dtDadosInformados = new DataTable();
            DataColumn dcCodigoAtribuicao = new DataColumn("CodigoAtribuicao", typeof(int));
            DataColumn dcUnidadeAtribuicaoRealInformado = new DataColumn("UnidadeAtribuicaoRealInformado", typeof(decimal));
            DataColumn dcUnidadeAtribuicaoRestanteInformado = new DataColumn("UnidadeAtribuicaoRestanteInformado", typeof(decimal));
            DataColumn dcCustoUnitarioRealInformado = new DataColumn("CustoUnitarioRealInformado", typeof(decimal));
            DataColumn dcCustoUnitarioRestanteInformado = new DataColumn("CustoUnitarioRestanteInformado", typeof(decimal));
            DataColumn dcCustoRealInformado = new DataColumn("CustoRealInformado", typeof(decimal));
            DataColumn dcCustoRestanteInformado = new DataColumn("CustoRestanteInformado", typeof(decimal));
            DataColumn dcReceitaRealInformado = new DataColumn("ReceitaRealInformado", typeof(decimal));
            DataColumn dcReceitaRestanteInformado = new DataColumn("ReceitaRestanteInformado", typeof(decimal));

            dtDadosInformados.Columns.Add(dcCodigoAtribuicao);
            dtDadosInformados.Columns.Add(dcUnidadeAtribuicaoRealInformado);
            dtDadosInformados.Columns.Add(dcUnidadeAtribuicaoRestanteInformado);
            dtDadosInformados.Columns.Add(dcCustoUnitarioRealInformado);
            dtDadosInformados.Columns.Add(dcCustoUnitarioRestanteInformado);
            dtDadosInformados.Columns.Add(dcCustoRealInformado);
            dtDadosInformados.Columns.Add(dcCustoRestanteInformado);
            dtDadosInformados.Columns.Add(dcReceitaRealInformado);
            dtDadosInformados.Columns.Add(dcReceitaRestanteInformado);
            setDtDadosInformados(dtDadosInformados);
            if (!IsCallback)
            {
                pcDados.ActiveTabIndex = 0;
                carregaDados(idAtribucao);

            }
        }
        string urlAnexos = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=AT&ID=" + idAtribucao + "&ALT=392";
        string urlOutrosCustos = "../espacoTrabalho/abaVisibilidadeOutrosCustos.aspx?ID=" + idAtribucao;
        pcDados.JSProperties["cp_URLAnexos"] = urlAnexos;
        pcDados.JSProperties["cp_URLOutrosCustos"] = urlOutrosCustos;
        carregaGridPredecessoras(idAtribucao);
        carregaGridSucessoras(idAtribucao);
        setVisibilidadeOutrosCustos();
        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvDados.Settings.ShowFilterRow = false;
        gvDados.SettingsBehavior.AllowSort = false;
        gvDados.SettingsBehavior.AllowAutoFilter = false;
        gvDados.SettingsBehavior.AllowHeaderFilter = false;
        gvDados.SettingsBehavior.AllowGroup = false;
        gvDados.SettingsBehavior.AllowSelectSingleRowOnly = true;
        gvDados.SettingsBehavior.AllowFocusedRow = true;
        gvDados.SettingsBehavior.AllowFixedGroups = true;
       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/espacoTrabalho_DetalhesTS.js""></script>"));

        string popupTituloApontamentoOutrosCustos = (cDados.DataSetOk(cDados.getParametrosSistema(codigoEntidadeLogada, "popupTituloApontamentoOutrosCustos")) &&
  cDados.DataTableOk(cDados.getParametrosSistema("popupTituloApontamentoOutrosCustos").Tables[0])) ?
  cDados.getParametrosSistema("popupTituloApontamentoOutrosCustos").Tables[0].Rows[0]["popupTituloApontamentoOutrosCustos"].ToString() : pcDados.TabPages[5].Text;
        hfGeral.Set("popupTituloApontamentoOutrosCustos", popupTituloApontamentoOutrosCustos);
    }

    protected void carregaGrid()
    {
        string comandoSQL = string.Format(@"
            SELECT    [CodigoAtribuicao]
                    , [CodigoRecursoProjeto]
                    , [NomeRecurso]
                    , [UnidadeMedidaRecurso]
                    , [UnidadeAtribuicaoLB]
                    , [CustoUnitarioLB]
                    , [CustoLB]
                    , [UnidadeAtribuicao]
                    , [CustoUnitario]
                    , [Custo]
                    , [UnidadeAtribuicaoReal]
                    , [CustoUnitarioReal]
                    , [CustoReal]
                    , [UnidadeAtribuicaoRealInformado]
                    , [CustoUnitarioRealInformado]
                    , [CustoRealInformado]
                    , [UnidadeAtribuicaoRestanteInformado]
                    , [CustoUnitarioRestanteInformado]
                    , [CustoRestanteInformado]
                    , [CodigoTipoRecurso]
                    , CAST(0 AS bit) AS [IndicaLinhaAlterada]
             FROM [dbo].[f_art_GetOutrosCustosAtribuicao] ({0}, {1},{2})", codigoEntidadeLogada, codigoUsuarioLogado, idAtribucao);
       DataSet dsGrid = cDados.getDataSet(comandoSQL);
        setDsGrid(dsGrid);
        if (cDados.DataSetOk(getDsGrid()))
        {
            gvDados.DataSource = getDsGrid();            
            gvDados.DataBind();
        }
    }

    private void carregaDados(string codigoAtribucao)
    {
        string codigoAprovador;

        if (codigoAtribucao != "")
        {
            DataSet ds = cDados.getDetalhesTS(codigoAtribucao);
            string tipoAtualizacao = "";
            //bool temRegistros = false;

            if (cDados.DataSetOk(ds))
            {
                txtIndicaMarco.Text = "";
                if (cDados.DataTableOk(ds.Tables[0]))
                {
                    //string.Format("{0:dd/MM/yyyy}", txtDe.Date);
                    //Cargar Datos Aba Detalhes
                    txtProjeto.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
                    txtTarefa.Text = ds.Tables[0].Rows[0]["NomeTarefa"].ToString();
                    txtTarefaSuperior.Text = ds.Tables[0].Rows[0]["TarefaSuperior"].ToString();

                    txtProjetoComentarios.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
                    txtTarefaComentarios.Text = ds.Tables[0].Rows[0]["NomeTarefa"].ToString();
                    txtTarefaSuperiorComentarios.Text = ds.Tables[0].Rows[0]["TarefaSuperior"].ToString();

                    txtAprovador.Text = ds.Tables[0].Rows[0]["Aprovador"].ToString();

                    preencheCamposTxtInicioTerminoTrabalhoPrevisto(ds.Tables[0].Rows[0]);

                    txtInicio.Text = ds.Tables[0].Rows[0]["Inicio"] + "" == "" ? "" : ((DateTime)ds.Tables[0].Rows[0]["Inicio"]).ToShortDateString();
                    txtTermino.Text = ds.Tables[0].Rows[0]["Termino"] + "" == "" ? "" : ((DateTime)ds.Tables[0].Rows[0]["Termino"]).ToShortDateString();
                    txtTrabalho.Text = ds.Tables[0].Rows[0]["Trabalho"].ToString();
                    txtIndicaMarco.Text = ds.Tables[0].Rows[0]["IndicaMarco"].ToString();

                    tipoAtualizacao = ds.Tables[0].Rows[0]["TipoAtualizacao"].ToString();
                    mmAnotacoes.Text = ds.Tables[0].Rows[0]["AnotacoesCodigoCronogramaProjeto"].ToString(); //Anotacoes"].ToString();

                    if (ds.Tables[0].Rows[0]["StatusAprovacao"].ToString() != "")
                    {
                        lblStatusAprovacao.Text = ds.Tables[0].Rows[0]["StatusAprovacao"].ToString();
                        txtAprovadorTarefa.Text = ds.Tables[0].Rows[0]["Aprovador"].ToString();
                        txtDataStatus.Text = ((DateTime)ds.Tables[0].Rows[0]["DataStatusAprovacao"]).ToShortDateString();
                    }

                    if (ds.Tables[0].Rows[0]["SiglaStatusAprovacao"].ToString() == "AP")
                    {
                        codigoAprovador = ds.Tables[0].Rows[0]["CodigoAprovador"].ToString();
                        if (codigoAprovador == codigoUsuarioLogado.ToString())
                        {
                            bloqueio = "N";
                        }
                        else
                        {
                            if (double.Parse(ds.Tables[1].Rows[0]["PercentualConcluido"].ToString()) == 100)
                            {
                                bloqueio = "S";
                            }
                        }
                    }
                    if (ds.Tables[0].Rows[0]["SiglaStatusAprovacao"].ToString() == "PA")
                    {
                        bloqueio = "N";
                    }
                    else if ((ds.Tables[0].Rows[0]["SiglaStatusAprovacao"].ToString() == "EA") || (ds.Tables[0].Rows[0]["SiglaStatusAprovacao"].ToString() == "ER"))
                    {
                        bloqueio = "S";
                    }

                    if (ds.Tables[0].Rows[0]["InicioProjeto"].ToString() != "")
                    {

                        ddlInicioReal.MinDate = DateTime.Parse(ds.Tables[0].Rows[0]["InicioProjeto"].ToString()).Date;
                        ddlTerminoReal.MinDate = DateTime.Parse(ds.Tables[0].Rows[0]["InicioProjeto"].ToString()).Date;
                    }

                    if (ds.Tables[0].Rows[0]["TerminoProjeto"].ToString() != "")
                    {
                        ddlInicioReal.MaxDate = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoProjeto"].ToString()).Date;
                        ddlTerminoReal.MaxDate = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoProjeto"].ToString()).Date;
                    }
                }
                if (cDados.DataTableOk(ds.Tables[1]))
                {
                    if (ds.Tables[1].Rows[0]["DataInicioReal"].ToString() != "")
                        ddlInicioReal.Date = Convert.ToDateTime(ds.Tables[1].Rows[0]["DataInicioReal"].ToString());
                    if (ds.Tables[1].Rows[0]["DataTerminoReal"].ToString() != "")
                        ddlTerminoReal.Date = Convert.ToDateTime(ds.Tables[1].Rows[0]["DataTerminoReal"].ToString());

                    txtTrabalhoReal.Text = ds.Tables[1].Rows[0]["TrabalhoRealInformado"].ToString() == "" ? "0" : string.Format("{0:n2}", ds.Tables[1].Rows[0]["TrabalhoRealInformado"]);
                    txtTrabalhoRestante.Text = ds.Tables[1].Rows[0]["TrabalhoRestanteInformado"].ToString() == "" ? "0" : string.Format("{0:n2}", ds.Tables[1].Rows[0]["TrabalhoRestanteInformado"]);
                    txtPorcentaje.Text = ds.Tables[1].Rows[0]["PercentualConcluido"].ToString();
                    mmComentariosRecurso.Text = ds.Tables[1].Rows[0]["ComentariosRecurso"].ToString();
                    mmComentariosAprovador.Text = ds.Tables[1].Rows[0]["ComentariosAprovador"].ToString();
                }
                if ((tipoAtualizacao != "PC") || (bloqueio == "S"))
                {
                    //btnSalvar.Visible = false;
                    ddlInicioReal.ClientEnabled = false;
                    ddlTerminoReal.ClientEnabled = false;
                    txtTrabalhoReal.ClientEnabled = false;
                    txtPorcentaje.ClientEnabled = false;
                    txtTrabalhoRestante.ClientEnabled = false;
                }
                else
                {
                    // desabilitando 'trabalhoReal' e 'restante' no caso de tarefa marco. 
                    if (txtIndicaMarco.Text.Equals("S", StringComparison.OrdinalIgnoreCase))
                    {
                        txtTrabalhoReal.ClientEnabled = false;
                        txtTrabalhoRestante.ClientEnabled = false;
                    }
                }
            }
        }
    }

    private void preencheCamposTxtInicioTerminoTrabalhoPrevisto(DataRow dr1)
    {
        string valor_labelProgramacaoCurvaSFisica = "";

        DataSet ds_apresentaReprogramacaoAusenciaLB = cDados.getValorParametroConfiguracaoSistema("apresentaReprogramacaoAusenciaLB", codigoEntidadeLogada.ToString(), "");
        DataSet ds_labelProgramacaoCurvaSFisica = cDados.getValorParametroConfiguracaoSistema("labelProgramacaoCurvaSFisica", codigoEntidadeLogada.ToString(), "");
        valor_labelProgramacaoCurvaSFisica = ds_labelProgramacaoCurvaSFisica.Tables[0].Rows[0][0].ToString();

        /*
         Se tarefa tarefa possuir LB, a tela continua como está. [OK]
             Se a tela não possuir mas não existir o parâmetro "apresentaReprogramacaoAusenciaLB" ou o seu valor não for "S", a tela continua como está. [OK]
             No caso da tarefa não possuir LB e o parâmetro "apresentaReprogramacaoAusenciaLB" contiver o valor "S", fazer as seguintes alterações:
                mudar os labels do popup para: "Início Planejado", "Término Planejado", "Trabalho Planejado"   => o termo planejado deve ser substituído pelo valor do parâmetro "labelProgramacaoCurvaSFisica";
                Apresentar nos campos os valores "Início", "Término" e "Trabalho" em vez de "InicioLB", "TerminoLB" e "TrabalhoLB";
         */

        txtInicioPrevisto.Text = dr1["InicioPrevisto"] + "" == "" ? "" : ((DateTime)dr1["InicioPrevisto"]).ToShortDateString();
        txtTerminoPrevisto.Text = dr1["TerminoPrevisto"] + "" == "" ? "" : ((DateTime)dr1["TerminoPrevisto"]).ToShortDateString();
        txtTabalhoPrevisto.Text = dr1["TrabalhoPrevisto"].ToString();

        if (dr1["InicioPrevisto"].ToString() == ""
            && cDados.DataSetOk(ds_apresentaReprogramacaoAusenciaLB)
            && cDados.DataTableOk(ds_apresentaReprogramacaoAusenciaLB.Tables[0])
            && ds_apresentaReprogramacaoAusenciaLB.Tables[0].Rows[0][0].ToString().Trim().ToUpper() == "S")
        {
            txtInicioPrevisto.Text = dr1["Inicio"] + "" == "" ? "" : ((DateTime)dr1["Inicio"]).ToShortDateString();
            txtTerminoPrevisto.Text = dr1["Termino"] + "" == "" ? "" : ((DateTime)dr1["Termino"]).ToShortDateString();
            txtTabalhoPrevisto.Text = dr1["Trabalho"] + "" == "" ? "" : dr1["Trabalho"].ToString();

            lblInicioPrevisto.Text = "Início " + valor_labelProgramacaoCurvaSFisica;
            lblTerminoPrevisto.Text = "Término " + valor_labelProgramacaoCurvaSFisica;
            lblTrabalhoPrevisto.Text = "Trabalho " + valor_labelProgramacaoCurvaSFisica;
        }
    }


    private void carregaGridPredecessoras(string codigoAtribucao)
    {
        DataSet ds = cDados.getPredecessorasAtribuicao(codigoAtribucao);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvPredecessoras.DataSource = ds;
            gvPredecessoras.DataBind();
        }
    }

    private void carregaGridSucessoras(string codigoAtribucao)
    {
        DataSet ds = cDados.getSucessorasAtribuicao(codigoAtribucao);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvSucessoras.DataSource = ds;
            gvSucessoras.DataBind();
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";


        string msgErro = "";
        string inicioReal = (ddlInicioReal.Date == null || ddlInicioReal.Text == "") ? "NULL" : string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", ddlInicioReal.Date);
        string terminoReal = (ddlTerminoReal.Date == null || ddlTerminoReal.Text == "") ? "NULL" : string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", ddlTerminoReal.Date);

        bool retorno = cDados.atualizaTarefa_PC_TS(codigoUsuarioLogado, codigoEntidadeLogada, int.Parse(idAtribucao), txtTrabalhoRestante.Text, txtTrabalhoReal.Text, inicioReal, terminoReal, mmComentariosRecurso.Text, true,   getDsGrid().Tables[0], ref msgErro);

        if (retorno)
        {
            resetaDtDadosInformados();
            resetaDsGrid();
            callBack.JSProperties["cp_OK"] = Resources.traducao.DetalhesTS_atribui__o_atualizada_com_sucesso_;
            carregaDados(idAtribucao);
        }
        else
        {
            callBack.JSProperties["cp_Erro"] = Resources.traducao.DetalhesTS_erro_ao_atualizar_a_atribui__o;
        }
    }

    private void setVisibilidadeOutrosCustos()
    {
        bool retorno = false;
        string comando = string.Format(@"
        SELECT [dbo].[f_art_GetVisibilidadeOutrosCustos] ({0},{1},{2})", codigoEntidadeLogada, codigoUsuarioLogado, idAtribucao);

        DataSet ds = cDados.getDataSet(comando);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = (bool)ds.Tables[0].Rows[0][0];
        }

        bool datasetDataTableOK = (cDados.DataSetOk(cDados.getParametrosSistema(codigoEntidadeLogada, "popupLabelAbaOutrosCustos")) &&
                          cDados.DataTableOk(cDados.getParametrosSistema(codigoEntidadeLogada, "popupLabelAbaOutrosCustos").Tables[0]));

        bool parametroEmbranco = true;
        string valorParametro = "";

        if (datasetDataTableOK == true)
        {
            parametroEmbranco = cDados.getParametrosSistema(codigoEntidadeLogada, "popupLabelAbaOutrosCustos").Tables[0].Rows[0]["popupLabelAbaOutrosCustos"].ToString().Trim() == "";
            valorParametro = cDados.getParametrosSistema(codigoEntidadeLogada, "popupLabelAbaOutrosCustos").Tables[0].Rows[0]["popupLabelAbaOutrosCustos"].ToString();
        }

        string labelAba = (datasetDataTableOK && !parametroEmbranco) ? valorParametro : pcDados.TabPages[5].Text;


        pcDados.TabPages[5].Visible = retorno;     
        pcDados.TabPages[5].Text = labelAba;
    }

    protected void limpaCampos()
    {
        txtRecurso.Text = "";
        txtUnidadeMedida.Text = "";


        txtQuantidadeLinhaBase.Text = "";
        spnValorUnitarioLinhaBase.Text = "";
        spnValorTotalLinhaBase.Text = "";

        txtQuantidadeUltimoPlanejamento.Text = "";
        spnValorUnitarioUltimoPlanejamento.Text = "";
        spnValorTotalUltimoPlanejamento.Text = "";

        txtQuantidadeRealizado.Text = "";
        spnValorUnitarioRealizado.Text = "";
        spnValorTotalRealizado.Text = "";

        txtQuantidadeRestante.Text = "";
        spnValorUnitarioRestante.Text = "";
        spnValorTotalRestante.Text = "";


        txtRecursoFinanceiro.Text = "";
        txtUnidadeMedidaFinanceiro.Text = "";


        spnLinhaBaseFinanceiro.Value = "";
        spnUltimoPlanejamentoFinanceiro.Value = "";

        spnRealizadoFinanceiro.Value = "";
        spnRestanteFinanceiro.Value = "";

    }


    protected void callbackAplicar_Callback(object source, CallbackEventArgs e)
    {
        var tipoRecurso = e.Parameter;

        ((ASPxCallback)source).JSProperties["cpOK"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        try
        {
            DataTable dt1 = getDtDadosInformados();
            
            DataRow dr = dt1.NewRow();
            if(tipoRecurso == "Financeiro")
            {
                dr["UnidadeAtribuicaoRealInformado"] = 0.0;
                dr["UnidadeAtribuicaoRestanteInformado"] = 0.0;
                dr["CustoUnitarioRealInformado"] = 0.0;
                dr["CustoUnitarioRestanteInformado"] = 0.0;
                dr["CustoRealInformado"] =  spnRealizadoFinanceiro.Value == null ? decimal.Parse("0.0") : spnRealizadoFinanceiro.Value;
                dr["CustoRestanteInformado"] = spnRestanteFinanceiro.Value == null ? decimal.Parse("0.0") : spnRestanteFinanceiro.Value;
            }
            else
            {
                dr["UnidadeAtribuicaoRealInformado"] = txtQuantidadeRealizado.Text == "" ? decimal.Parse("0.0") : decimal.Parse(txtQuantidadeRealizado.Text);
                dr["UnidadeAtribuicaoRestanteInformado"] = txtQuantidadeRestante.Text == "" ? decimal.Parse("0.0") : decimal.Parse(txtQuantidadeRestante.Text);
                dr["CustoUnitarioRealInformado"] = spnValorUnitarioRealizado.Value == null ? decimal.Parse("0.0") : spnValorUnitarioRealizado.Value;
                dr["CustoUnitarioRestanteInformado"] = spnValorUnitarioRestante.Value == null ? decimal.Parse("0.0") : spnValorUnitarioRestante.Value;
                dr["CustoRealInformado"] = spnValorTotalRealizado.Value == null ? decimal.Parse("0.0") : spnValorTotalRealizado.Value;
                dr["CustoRestanteInformado"] = spnValorTotalRestante.Value == null ? decimal.Parse("0.0") : spnValorTotalRestante.Value;
            }
            
            dr["CodigoAtribuicao"] = hfGeral.Get("CodigoAtribuicaoSelecionado");

            dt1.Rows.Add(dr);
            setDtDadosInformados(dt1);
            DataRow[] dtLinhaSelecionadaDagrid = getDsGrid().Tables[0].Select("CodigoAtribuicao = " + hfGeral.Get("CodigoAtribuicaoSelecionado"));

            dtLinhaSelecionadaDagrid[0]["UnidadeAtribuicaoReal"] = dr["UnidadeAtribuicaoRealInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoUnitarioReal"] = dr["CustoUnitarioRealInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoReal"] = dr["CustoRealInformado"];

            dtLinhaSelecionadaDagrid[0]["UnidadeAtribuicaoRealInformado"] = dr["UnidadeAtribuicaoRealInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoUnitarioRealInformado"] = dr["CustoUnitarioRealInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoRealInformado"] = dr["CustoRealInformado"];

            dtLinhaSelecionadaDagrid[0]["UnidadeAtribuicaoRestanteInformado"] = dr["UnidadeAtribuicaoRestanteInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoUnitarioRestanteInformado"] = dr["CustoUnitarioRestanteInformado"];
            dtLinhaSelecionadaDagrid[0]["CustoRestanteInformado"] = dr["CustoRestanteInformado"];

            dtLinhaSelecionadaDagrid[0]["IndicaLinhaAlterada"] = 1;


            ((ASPxCallback)source).JSProperties["cpOK"] = "Dados aplicados com sucesso!";
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = "Erro: " + ex.Message;
        }
    }

    protected void gvDados_DataBinding(object sender, EventArgs e)
    {
        if (cDados.DataSetOk(getDsGrid()))
        {
            gvDados.DataSource = getDsGrid();            
        }
    }

    protected void callbackMontaCamposFormulario_Callback(object source, CallbackEventArgs e)
    {

        var retornos = e.Parameter.Split('|');
        var codigoAtribuicao = retornos[0];
        var codigoTipoRecurso = retornos[1];
        var nomeRecurso = retornos[2];
        var unidadeMedidaRecurso = retornos[3];
        var unidadeAtribuicao = retornos[4];
        var custoUnitario = retornos[5];
        var custo = retornos[6];
        var unidadeAtribuicaoReal = retornos[7];
        var custoUnitarioReal = retornos[8];
        var custoReal = retornos[9];

        
        if (hfValoresInformados.Contains(codigoAtribuicao + ".CodigoTipoRecurso") == false)
        {
            ((ASPxCallback)source).JSProperties["cpCodigoAtribuicao"] = codigoAtribuicao;
            ((ASPxCallback)source).JSProperties["cpCodigoTipoRecurso"] = codigoTipoRecurso;
            ((ASPxCallback)source).JSProperties["cpNomeRecurso"] = nomeRecurso;
            ((ASPxCallback)source).JSProperties["cpUnidadeMedidaRecurso"] = unidadeMedidaRecurso;
            ((ASPxCallback)source).JSProperties["cpUnidadeAtribuicao"] = unidadeAtribuicao;
            ((ASPxCallback)source).JSProperties["cpCustoUnitario"] = custoUnitario;
            ((ASPxCallback)source).JSProperties["cpCusto"] = custo;
            ((ASPxCallback)source).JSProperties["cpUnidadeAtribuicaoReal"] = unidadeAtribuicaoReal;
            ((ASPxCallback)source).JSProperties["cpCustoUnitarioReal"] = custoUnitarioReal;
            ((ASPxCallback)source).JSProperties["cpCustoReal"] = custoReal;
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //e.Cell.CssClass += " border";
        e.Cell.BackColor = Color.FromName("#FFFFFF");
       // e.Cell.BorderColor = Color.Black;

        /*
           Toda vez que a linha processada for do tipo de dados financeiro, então as colunas 
           UnidadeAtribuicao, CustoUnitario, UnidadeAtribuicaoReal, CustoUnitarioReal ficam em branco
         */
        if (int.Parse(e.GetValue("CodigoTipoRecurso").ToString()) == 3 && 
            (e.DataColumn.FieldName.Equals("UnidadeAtribuicao") ||
                e.DataColumn.FieldName.Equals("CustoUnitario") ||
                e.DataColumn.FieldName.Equals("UnidadeAtribuicaoReal") ||
                e.DataColumn.FieldName.Equals("CustoUnitarioReal")))
        {
            e.Cell.Text = "";
            e.Cell.BackColor = Color.FromName("#D8D8D8");
        }
    }

    protected void gvDados_HtmlCommandCellPrepared(object sender, ASPxGridViewTableCommandCellEventArgs e)
    {
        e.Cell.CssClass += " border";
        e.Cell.BackColor = Color.FromName("#FFFFFF");
    }

    protected void setDtDadosInformados(DataTable dt)
    {
        Session["dtDadosInformados"] = dt;
    }

    protected DataTable getDtDadosInformados()
    {
        DataTable dtRetorno = new DataTable();
        dtRetorno  = ((DataTable)Session["dtDadosInformados"]);
        return dtRetorno;
    }

    protected void resetaDtDadosInformados()
    {
        Session["dtDadosInformados"] = null;
    }

    protected void setDsGrid(DataSet ds)
    {
        Session["dsGrid"] = ds;
    }

    protected DataSet getDsGrid()
    {
        DataSet dsRetorno = new DataSet();
        dsRetorno = ((DataSet)Session["dsGrid"]);
        return dsRetorno;
    }
    protected void resetaDsGrid()
    {
        Session["dsGrid"] = null;
    }
}
