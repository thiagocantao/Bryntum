/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 
 * 01/03/2011 :: Alejandro : -Alteração no método 'carregaComboFontePagadora()', aonde agora indicara o código da
 *                          entidade logada para filtrar as fontes pagadoras correspondientes.
 *                          -Alteração do desenho da grid para obter o padron.
 * 
 * 17/03/2011 :: Alejandro : adiciono el botão de Permissãos para os contratos.
 * 21/03/2011 :: Alejandro : adiciono el control de acesso para o botão de permissões [CT_AdmPrs].
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
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Collections.Generic;

public partial class _Projetos_DadosProjeto_frmContratos : System.Web.UI.Page
{
    dados cDados;
    DataSet dsTreelist;

    private int idProjeto = -1;
    private int idUsuarioResponsavel;
    private int idEntidadeLogado;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    private string dbName;
    private string dbOwner;
    private string captioCIA = "Número do CIA:";

    public string alturaTabelaAnexo;
    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;

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

        if (Request.QueryString["ID"] != null) 
            idProjeto = int.Parse(Request.QueryString["ID"].ToString());
        
        if (cDados.getInfoSistema("ResolucaoCliente") == null) 
            Response.Redirect("~/index.aspx");

        idUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        idEntidadeLogado = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        DataSet dsAux = cDados.getParametrosSistema(idEntidadeLogado, "labelNumeroInternoContrato");

        if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]))
            captioCIA = dsAux.Tables[0].Rows[0]["labelNumeroInternoContrato"].ToString() + ":";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeardeOnTela();

        if (!IsPostBack)
        {
            //bool bPodeAcessarTela;
            ///// se houver algum contrato que o usuário pode consultar
            //bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioResponsavel, idEntidadeLogado, "CT", "CT_Cns");

            ///// se ainda não foi determinado que pode acessar, 
            ///// verifica se há alguma unidade em que o usuário possa incluir contratos
            //if (bPodeAcessarTela == false)
            //    bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioResponsavel, idEntidadeLogado, "UN", "UN_IncCtt");

            ///// se ainda não foi determinado que pode acessar, 
            ///// verifica se há alguma unidade em que o usuário possa consultar contratos, mesmo que não exista nenhum
            //if (bPodeAcessarTela == false)
            //    bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioResponsavel, idEntidadeLogado, "UN", "CT_Cns");

            //// se não puder, redireciona para a página sem acesso
            //if (bPodeAcessarTela == false)
            //    cDados.RedirecionaParaTelaSemAcesso(this);
            //Inicializa los Hidden Field.
            hfGeral.Set("CodigoResponsavel", "");
            hfGeral.Set("CodigoContrato", "-1");
            hfGeral.Set("ListaDeParcelas", "");
            hfGeral.Set("TipoOperacaoParcelas", "");
            hfGeral.Set("TipoOperacaoEmParcela", "");
            hfGeral.Set("EstadoNumParcela", "");
            hfGeral.Set("CantUnidadesGestora", "");
            
            string where = string.Format(@" DataExclusao IS NULL AND CodigoUsuario IN(SELECT uun.CodigoUsuario FROM {0}.{1}.UsuarioUnidadeNegocio AS uun 
								                                  WHERE Uun.CodigoUnidadeNegocio = {2} AND uun.[IndicaUsuarioAtivoUnidadeNegocio] = 'S')", cDados.getDbName(), cDados.getDbOwner(), idEntidadeLogado.ToString());
            // conteúdo usado na tela para listar os usuários
            hfGeral.Set("ComandoWhere", where);
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);

            //carregar os comboBox            
            tabControl.ActiveTabIndex = 0;

            lblNomeCIA.Text = captioCIA;
        }

        carregaComboAquisicao(idEntidadeLogado);
        podeIncluir = carregaComboUnidadeGestora(string.Format(@" AND CodigoEntidade = {0} AND IndicaUnidadeNegocioAtiva = 'S' AND DataExclusao IS NULL", idEntidadeLogado)) > 0;
        carregaComboFontePagadora();
        carregaComboProjetos();
        carregaComboTipoContrato("");
        
        //podeIncluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, idEntidadeLogada, " ???? ");
        //podeEditar = cDados.VerificaPermissaoUsuario(idUsuarioResponsavel, idEntidadeLogado, "CT_Alt");
        //podeExcluir = cDados.VerificaPermissaoUsuario(idUsuarioResponsavel, idEntidadeLogado, "CT_Exc");

        if (!IsPostBack)
            carregaGvDados(idEntidadeLogado);
        
        gvParcelas.FilterExpression = "[TipoRegistro] <> 'EX'";

        if (Request.QueryString["DiasVencimento"] != null && Request.QueryString["DiasVencimento"].ToString() != "")
        {
            int diasVencimento = int.Parse(Request.QueryString["DiasVencimento"].ToString());
            DateTime dataVencimento = DateTime.Now.AddDays(diasVencimento);

            gvDados.FilterExpression = string.Format(" [DataTermino] <= #{0:yyyy-MM-dd}# AND [DataTermino] <> ''", dataVencimento);
            //gvDados.Columns[""].
        }

        if (Request.QueryString["ApenasMeusContratos"] != null && Request.QueryString["ApenasMeusContratos"].ToString() != "")
        {
            DataSet dsFiltro = cDados.getUsuarios(" AND u.CodigoUsuario = " + idUsuarioResponsavel);

            if (cDados.DataSetOk(dsFiltro) && cDados.DataTableOk(dsFiltro.Tables[0]))
            {
                string nomeUsuario = dsFiltro.Tables[0].Rows[0]["NomeUsuario"].ToString();

                if(gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [NomeUsuario] = '" + nomeUsuario + "'";
                else
                    gvDados.FilterExpression = " [NomeUsuario] = '" + nomeUsuario + "'";
            }
        }

        populaTreeListArquivos();        
    }

    #region GRIDVIEW

    private void carregaGvDados(int idEntidadeLogado)
    {
        string strWhere = "";

        if (idProjeto != -1)
            strWhere = " AND proj.CodigoProjeto = " + idProjeto;

        //Só pega contratos com fornecedores
        DataSet ds = cDados.getContratosAquisicoes(idEntidadeLogado, idUsuarioResponsavel, strWhere + " AND cont.TipoPessoa = 'F' ");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "DescricaoObjetoContrato")
        {
            if (e.CellValue.ToString().Length > 60)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 60) + "...";
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > 0)
        {
            int permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            podeEditar = (permissoes & 2) > 0;
            podeExcluir = (permissoes & 4) > 0;
            podePermissao = (permissoes & 8) > 0;


            if (e.ButtonID == "btnEditarCustom")
            {
                if (podeEditar)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Text = "";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            if (e.ButtonID == "btnExcluirCustom")
            {
                if (!podeExcluir)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Text = "";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            if (e.ButtonID == "btnPermissoesCustom")
            {
                if (podePermissao)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Text = "Permissões";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                }
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            string usuarioAtivo = e.GetValue("StatusContrato").ToString();

            if (usuarioAtivo == "I")
            {
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
            }
        }
    }

    private void carregaGvParcelas(string codigoContrato)
    {
        DataSet ds = cDados.getParcelaContrato(codigoContrato);
        
        if (cDados.DataSetOk(ds))
        {
            gvParcelas.DataSource = ds;
            gvParcelas.DataBind();

            
            Session["dtParcela"] = ds.Tables[0];

            obtemListaParcelas(ds.Tables[0]);
        }

    }

    private void obtemListaParcelas(DataTable dt)
    {
        string parcelas = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["TipoRegistro"].ToString() != "EX")
                parcelas += dr["NumeroAditivoContrato"].ToString() + dr["NumeroParcela"].ToString() + ";";
        }
        if (parcelas.Length > 0)
        {
            parcelas = parcelas.Remove(parcelas.Length - 1);
            gvParcelas.JSProperties["cp_parcelas"] = parcelas;
        }
        else
        {
            gvParcelas.JSProperties["cp_parcelas"] = "";
        }
    }

    private void vaciarSessionGvParcela()
    {
        if (Session["dtParcela"] != null) Session.Remove("dtParcela"); 
    }

    protected void gvParcelas_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvParcelas(hfGeral.Get("CodigoContrato").ToString());

        //Preparando a columna "#" da grid, caso qeu seja so consulta, a columna de ediçã ficara invisivel para o
        //usuario.
        //bool somenteLeitura = true;
        //if (hfGeral.Contains("TipoOperacao") && (hfGeral.Get("TipoOperacao").ToString() == "Incluir" || hfGeral.Get("TipoOperacao").ToString() == "Editar"))
        //    somenteLeitura = false;

        //gvParcelas.Columns[0].Visible = !somenteLeitura;

        //recargar a grid.
        string parametro = e.Parameters;

        if (parametro.Equals("CARREGAR"))
        {
            string codigoContrato = hfGeral.Get("CodigoContrato").ToString();
            carregaGvParcelas(codigoContrato);
        }
    }

    protected void gvParcelas_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "HistoricoParcela")
        {
            if (e.CellValue.ToString().Length > 20)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 20) + "...";
            }
        }
    }

    protected void gvParcelas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        e.Editor.ClientEnabled = true;
        e.Editor.DisabledStyle.ForeColor = Color.Black;
        e.Editor.DisabledStyle.BackColor = Color.White;

        string modoDetalhe = hfGeral.Get("TipoOperacaoParcelas").ToString();

        //si nao ta no modo edicao e nao ta null o conteudo do banco de dados.
        //if (e.KeyValue)
        if ((!gvParcelas.IsNewRowEditing))
        {
            if (e.Column.Name == "NumeroParcela")
            {
                e.Editor.ClientEnabled = false;
                e.Editor.DisabledStyle.ForeColor = Color.Black;
                e.Editor.DisabledStyle.BackColor = Color.Gainsboro;
            }
            if (e.Column.Name == "NumeroAditivo")
            {
                e.Editor.ClientEnabled = false;
                e.Editor.DisabledStyle.ForeColor = Color.Black;
                e.Editor.DisabledStyle.BackColor = Color.Gainsboro;
            }
            gvParcelas.JSProperties["cp_parcelas"] = "";
            gvParcelas.JSProperties["cp_tipoOperacaoEmParcela"] = "Editar";
        }
        else
            gvParcelas.JSProperties["cp_tipoOperacaoEmParcela"] = "Inserir";

        if (e.Column.Name == "DataVencimento")
        {
            ((ASPxDateEdit)e.Editor).MinDate = ddlInicioDeVigencia.Date;
        }
        if (e.Column.Name == "DataPagamento")
        {
            ((ASPxDateEdit)e.Editor).MaxDate = DateTime.Now;
        }

        if (modoDetalhe.Equals("Consultar") || modoDetalhe.Equals("ConsultarEdicao"))
        {
            e.Editor.ClientEnabled = false;
            e.Editor.DisabledStyle.ForeColor = Color.Black;
            e.Editor.DisabledStyle.BackColor = Color.Gainsboro;
        }
    }

    protected void gvParcelas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string modoDetalhe = hfGeral.Get("TipoOperacaoParcelas").ToString();
        hfGeral.Set("EstadoNumParcela", "");
        if (modoDetalhe.Equals("Consultar"))
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            {
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/editarRegDes.png";
            }
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
            {
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/excluirRegDes.png";
            }

        }
    }

    protected void gvParcelas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = obtemDataTableGridParcelas();

        DataRow dr = dt.NewRow();

        if (e.NewValues["NumeroParcela"] != null)
        {
            dr["CodigoContrato"] = "-1";
            dr["NumeroParcela"] = e.NewValues["NumeroParcela"];
            dr["NumeroAditivoContrato"] = e.NewValues["NumeroAditivoContrato"];
            dr["ValorPrevisto"] = e.NewValues["ValorPrevisto"];
            dr["DataVencimento"] = e.NewValues["DataVencimento"] != null ? e.NewValues["DataVencimento"] : System.DBNull.Value;
            dr["DataPagamento"] = e.NewValues["DataPagamento"] != null ? e.NewValues["DataPagamento"] : System.DBNull.Value;
            dr["ValorPago"] = Double.Parse(e.NewValues["ValorPago"].ToString()) != 0 ? e.NewValues["ValorPago"] : System.DBNull.Value;
            dr["HistoricoParcela"] = e.NewValues["HistoricoParcela"] != null ? e.NewValues["HistoricoParcela"] : System.DBNull.Value;
            dr["TipoRegistro"] = "NI";
        }

        dt.Rows.Add(dr);

        Session["dtParcela"] = dt;

        if (cDados.DataTableOk(dt))
        {
            gvParcelas.DataSource = dt;
            gvParcelas.DataBind();
            gvParcelas.FocusedRowIndex = gvParcelas.FindVisibleIndexByKeyValue(e.NewValues["CodigoContrato"]);
            obtemListaParcelas(dt);
            gvParcelas.JSProperties["cp_Edicao"] = "OK";
        }

        e.Cancel = true;
        gvParcelas.CancelEdit();
    }

    protected void gvParcelas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = obtemDataTableGridParcelas();

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["CodigoContrato"].ToString() == e.Keys["CodigoContrato"].ToString())
                    && (dr["NumeroAditivoContrato"].ToString() == e.Keys["NumeroAditivoContrato"].ToString())
                    && (dr["NumeroParcela"].ToString() == e.Keys["NumeroParcela"].ToString()))
                {
                    dr["NumeroAditivoContrato"] = e.NewValues["NumeroAditivoContrato"];
                    dr["NumeroParcela"] = e.NewValues["NumeroParcela"];
                    dr["ValorPrevisto"] = e.NewValues["ValorPrevisto"];
                    dr["DataVencimento"] = e.NewValues["DataVencimento"] != null ? e.NewValues["DataVencimento"] : System.DBNull.Value;
                    dr["DataPagamento"] = e.NewValues["DataPagamento"] != null ? e.NewValues["DataPagamento"] : System.DBNull.Value;
                    dr["ValorPago"] = Double.Parse(e.NewValues["ValorPago"].ToString()) != 0 ? e.NewValues["ValorPago"] : System.DBNull.Value;
                    dr["HistoricoParcela"] = e.NewValues["HistoricoParcela"] != null ? e.NewValues["HistoricoParcela"] : System.DBNull.Value;

                    if (e.Keys["CodigoContrato"].ToString().Equals("-1"))
                        dr["TipoRegistro"] = "NA";
                    else
                        dr["TipoRegistro"] = "EA";

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)


            Session["dtParcela"] = dt;

            gvParcelas.DataSource = dt;
            gvParcelas.DataBind();
            obtemListaParcelas(dt);
            gvParcelas.JSProperties["cp_Edicao"] = "OK";
        }  // if (null != dt)

        e.Cancel = true;
        gvParcelas.CancelEdit();
    }

    protected void gvParcelas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dt = obtemDataTableGridParcelas();

        foreach (DataRow dr in dt.Rows)
        {
            if ((dr["CodigoContrato"].ToString() == e.Keys["CodigoContrato"].ToString())
                && (dr["NumeroAditivoContrato"].ToString() == e.Keys["NumeroAditivoContrato"].ToString())
                && (dr["NumeroParcela"].ToString() == e.Keys["NumeroParcela"].ToString()))
            {
                if (e.Keys["CodigoContrato"].ToString().Equals("-1"))
                    dr.Delete();
                else
                    dr["TipoRegistro"] = "EX";

                dt.AcceptChanges();
                gvParcelas.JSProperties["cp_Edicao"] = "OK";
                break;
            }
        }

        gvParcelas.DataSource = dt;
        gvParcelas.DataBind();
        obtemListaParcelas((DataTable) gvParcelas.DataSource);

        e.Cancel = true;
        gvParcelas.CancelEdit();
    }

    private DataTable obtemDataTableGridParcelas()
    {
        DataTable dt = null;
        if (null != Session["dtParcela"])
            dt = (DataTable)Session["dtParcela"];
        else
        {
            carregaGvParcelas("-1");
            dt = (DataTable)Session["dtParcela"];
        }

        return dt;
    }

    #endregion

    #region COMBOBOX'S

    private void carregaComboAquisicao(int idEntidadLogada)
    {
        DataSet ds = cDados.getTiposAquisicao(idEntidadeLogado.ToString());

        if (cDados.DataSetOk(ds))
        {
            ddlModalidadAquisicao.DataSource = ds.Tables[0];
            ddlModalidadAquisicao.DataBind();

            if (!IsPostBack && ddlModalidadAquisicao.Items.Count > 0)
                ddlModalidadAquisicao.SelectedIndex = 0;
        }
    }

    private int carregaComboUnidadeGestora(string where)
    {
        int qtdUnidades = 0;
        where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({3}, {2}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncCtt')=1
                    ", dbName, dbOwner, idEntidadeLogado, idUsuarioResponsavel);
        DataSet ds = cDados.getUnidade(where);

        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeGestora.DataSource = ds.Tables[0];
            ddlUnidadeGestora.DataBind();
        }

        qtdUnidades = ddlUnidadeGestora.Items.Count;

        //saver quantas unidades tem. So para saver si tem 1, colocar como opção default.
        hfGeral.Set("CantUnidadesGestora", qtdUnidades);

        if (!IsPostBack && ddlUnidadeGestora.Items.Count > 0)
        {
            if (qtdUnidades == 1)
                ddlUnidadeGestora.SelectedIndex = 1;
            else
                ddlUnidadeGestora.SelectedIndex = 0;
        }

        return qtdUnidades;
    }

    /// <summary>
    /// filtro where, saver o alias das tabelas:: FROM {0}.{1}.FonteRecursosFinanceiros AS frf
    /// </summary>
    private void carregaComboFontePagadora()
    {
        string where = "where frf.CodigoEntidade = " + idEntidadeLogado;
        DataSet ds = cDados.getFontePagadora(where);

        if (cDados.DataSetOk(ds))
        {
            ddlFontePagadora.DataSource = ds.Tables[0];
            ddlFontePagadora.DataBind();

            if (!IsPostBack && ddlFontePagadora.Items.Count > 0)
                ddlFontePagadora.SelectedIndex = 0;
        }
    }

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND Projeto.CodigoEntidade = {0} ORDER BY Projeto.NomeProjeto", idEntidadeLogado);

        if (idProjeto != -1)
            where = " AND Projeto.CodigoProjeto = " + idProjeto + where;

        DataSet ds = cDados.getPropostas(idUsuarioResponsavel, idEntidadeLogado, where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

        }

        if (idProjeto == -1)
        {
            ListEditItem sinProjeto = new ListEditItem(Resources.traducao.nenhum, "0");
            ddlProjetos.Items.Insert(0, sinProjeto);
        }

        if (!IsPostBack && ddlProjetos.Items.Count > 0)
            ddlProjetos.SelectedIndex = 0;
    }

    //todo: Pendiente la consulta do combo TipoContrato.
    private void carregaComboTipoContrato(string where)
    {
        DataSet ds = cDados.getTipoContrato(idEntidadeLogado, where);

        if (cDados.DataSetOk(ds))
        {
            ddlTipoContrato.DataSource = ds.Tables[0];
            ddlTipoContrato.DataBind();
        }

        ListEditItem sinTipoContrato = new ListEditItem("Selecionar...", "0");
        ddlTipoContrato.Items.Insert(0, sinTipoContrato);

        if (!IsPostBack && ddlTipoContrato.Items.Count > 0)
            ddlTipoContrato.SelectedIndex = 0;
    }

    #endregion

    #region VARIOS

    private void HeardeOnTela()
    {
        ASPxWebControl.RegisterBaseScript(Page);

        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var __isEdicao = false;</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Contratos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Anexos_Contratos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "Contratos", "Anexos_Contratos"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        gvDados.Settings.VerticalScrollableHeight = altura - 100;
        pnAnexosContratos.Height = 293;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        carregaGvDados(idEntidadeLogado);

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + cDados.getInfoSistema("IDUsuarioLogado").ToString();
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "Contratos_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();

                gvExporter.WriteXls(stream, x);
                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    #endregion

    #region CALLBACK'S

    protected void callbackResponsavel_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        //so coloca na sessão se realmente precisar abrir o popup.
        Session["Pesquisa"] = txtResponsavel.Text;
        callbackResponsavel.JSProperties["cp_Pesquisa"] = "1";
        hfGeral.Set("StatusSalvar", "-1"); // -1 indicar que o retorno não está relacionado com persistência
        hfGeral.Set("lovMostrarPopPup", "0"); // 0 indica que não precisa abrir popup de pesquisa
        callbackResponsavel.JSProperties["cp_lovMostrarPopPup"] = "0";

        string nomeUsuario = "";
        string codigoUsuario = "";
        string where = " AND ";
        where = where + hfGeral.Get("ComandoWhere").ToString();
        cDados.getLov_NomeValor("usuario", "CodigoUsuario", "NomeUsuario", txtResponsavel.Text, false, where, "NomeUsuario", out codigoUsuario, out nomeUsuario);

        // se encontrou um único registro
        if (nomeUsuario != "")
        {
            txtResponsavel.Text = nomeUsuario;
            hfGeral.Set("CodigoResponsavel", codigoUsuario);
            callbackResponsavel.JSProperties["cp_Codigo"] = codigoUsuario;
            callbackResponsavel.JSProperties["cp_Nome"] = nomeUsuario;
        }
        else // mostrar popup
        {
            hfGeral.Set("lovMostrarPopPup", "1"); // 1 indica que precisa abrir popup de pesquisa
            hfGeral.Set("CodigoResponsavel", "");

            callbackResponsavel.JSProperties["cp_Codigo"] = "";
            callbackResponsavel.JSProperties["cp_Nome"] = "";
        }
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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

    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.Equals("CerrarSession"))
            vaciarSessionGvParcela();
    }

    protected void pnCallback1_Disposed(object sender, EventArgs e)
    {
        tabControl.EnableViewState = true;
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela
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
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";
        int novoCodigoContrato = -1;

        try
        {
            //recoleção de dados.
            string numeroContrato = txtNumeroContrato.Text;
            //string codigoProjeto = idProjeto.ToString();
            string codigoTipoContrato = ddlTipoContrato.Value != null ? ddlTipoContrato.Value.ToString() : "-1";
            string codigoTipoAquisicao = ddlModalidadAquisicao.Value != null ? ddlModalidadAquisicao.Value.ToString() : "";
            string fornecedor = txtFornecedor.Text;
            string descricaoObjetoContrato = mmObjeto.Text;
            string codigoFonteRecursosFinancieros = ddlFontePagadora.Value != null ? ddlFontePagadora.Value.ToString() : "";
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "";
            DateTime dtInicio = ddlInicioDeVigencia.Date;
            DateTime dtTermino = ddlTerminoDeVigencia.Date;
            string codigoUsuarioResponsavel = hfGeral.Get("CodigoResponsavel").ToString();
            string codigoUsuarioInclusao = idUsuarioResponsavel.ToString();
            string statusContrato = ddlSituacao.Value != null ? ddlSituacao.Value.ToString() : "";
            string observacoes = mmObservacoes.Text;
            string numeroContratoSAP = txtSAP.Text;
            string codigoProjeto = ddlProjetos.Value != null ? (int.Parse(ddlProjetos.Value.ToString()) != 0 ? ddlProjetos.Value.ToString() : "NULL") : "NULL";
            string valorContrato = txtValorDoContrato.Text.Replace(".", "").Replace(",", ".");

            //Insere a Unidade Negocio com NivelHierarquico como 0 e EstruturaHierarquica com 0 pois será feito um "scope_identity()" para poder montar o Nivel e a Estrutura
            cDados.incluirContratoAquisicoes(idEntidadeLogado, numeroContrato, codigoProjeto, codigoTipoAquisicao
                                               , fornecedor, descricaoObjetoContrato, codigoFonteRecursosFinancieros
                                               , codigoUnidadeNegocio, dtInicio, dtTermino
                                               , codigoUsuarioResponsavel, codigoUsuarioInclusao, statusContrato
                                               , observacoes, numeroContratoSAP, codigoTipoContrato
                                               , valorContrato, null, "-1", "N", "NULL", ref novoCodigoContrato);

            hfGeral.Set("CodigoContrato", novoCodigoContrato.ToString());
            carregaGvDados(idEntidadeLogado);
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoContrato);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = "Houve um erro ao salvar o registro. Entre em contato com o administrador do sistema.\nMensagem Servidor:\n" + ex.Message;
        }
        return msg;
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";

        try
        {
            //recoleção de dados.
            string codigoContratoAquisicao = getChavePrimaria();
            string numeroContrato = txtNumeroContrato.Text;
            //string codigoProjeto = idProjeto.ToString();
            string codigoTipoContrato = ddlTipoContrato.Value != null ? ddlTipoContrato.Value.ToString() : "-1";
            string codigoTipoAquisicao = ddlModalidadAquisicao.Value != null ? ddlModalidadAquisicao.Value.ToString() : "";
            string fornecedor = txtFornecedor.Text;
            string descricaoObjetoContrato = mmObjeto.Text;
            string codigoFonteRecursosFinancieros = ddlFontePagadora.Value != null ? ddlFontePagadora.Value.ToString() : "";
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "";
            DateTime dtInicio = ddlInicioDeVigencia.Date;
            DateTime dtTermino = ddlTerminoDeVigencia.Date;
            string codigoUsuarioResponsavel = hfGeral.Get("CodigoResponsavel").ToString();
            string codigoUsuarioAlteracao = idUsuarioResponsavel.ToString();
            string statusContrato = ddlSituacao.Value != null ? ddlSituacao.Value.ToString() : "";
            string observacoes = mmObservacoes.Text;
            string numeroContratoSAP = txtSAP.Text;
            string sqlParcelas = montaSQLParcelas();
            string codigoProjeto = ddlProjetos.Value != null ? (int.Parse(ddlProjetos.Value.ToString()) != 0 ? ddlProjetos.Value.ToString() : "NULL") : "NULL";
            string valorContrato = txtValorDoContrato.Text.Replace(".","").Replace(",",".");

            cDados.atualizaContratoAquisicoes(numeroContrato, codigoTipoAquisicao, fornecedor, descricaoObjetoContrato
                                                , codigoFonteRecursosFinancieros, codigoUnidadeNegocio, dtInicio, dtTermino
                                                , codigoUsuarioResponsavel, codigoUsuarioAlteracao, statusContrato
                                                , codigoContratoAquisicao, observacoes, numeroContratoSAP, codigoProjeto
                                                , codigoTipoContrato, valorContrato, sqlParcelas, "NULL", "-1", "N", "NULL");

            carregaGvDados(idEntidadeLogado);
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoContratoAquisicao);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = "Houve um erro ao salvar o registro. Entre em contato com o administrador do sistema.\nMensagem Servidor:\n" + ex.Message;
        }
        return msg;
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        string msg = "";
        string chave = getChavePrimaria();

        try
        {
            cDados.excluiContratoAquisicoes(chave, cDados.getCodigoTipoAssociacao("CT"), ref msg);
            carregaGvDados(idEntidadeLogado);
        }
        catch
        {
            gvDados.ClientVisible = false;
            msg = "Houve um erro ao excluir o registro. Entre em contato com o administrador do sistema.";
        }

        return msg;
    }

    private string montaSQLParcelas()
    {
        DataTable dt = obtemDataTableGridParcelas();
        //string retorno = "";
       //string codigoUsuario = hfGeral.Get("CodigoUsuarioPermissao").ToString();
        string strInsert = string.Empty;
        string strUpdate = string.Empty;
        string strDelete = string.Empty;
        string strVencto = string.Empty;
        string strPagto = string.Empty;

        DateTime dtVencimento = new DateTime();
        DateTime dtPagamento = new DateTime();
        
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["DataVencimento"].ToString() == "")
                dtVencimento = DateTime.MinValue;
            else
                dtVencimento = (DateTime)dr["DataVencimento"];

            if (dr["DataPagamento"].ToString() == "")
                dtPagamento = DateTime.MinValue;
            else 
                dtPagamento = (DateTime)dr["DataPagamento"];

            strVencto = dtVencimento == DateTime.MinValue ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dtVencimento);
            strPagto = dtPagamento == DateTime.MinValue ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dtPagamento);

            if (dr["CodigoContrato"].ToString().Equals("-1"))
            {
                strInsert += string.Format(@"
                                INSERT INTO {0}.{1}.ParcelaContrato(CodigoContrato, NumeroParcela, ValorPrevisto
                                                                    , DataVencimento, DataPagamento, ValorPago
                                                                    , HistoricoParcela, DataInclusao, CodigoUsuarioInclusao
                                                                    , NumeroAditivoContrato)
                                VALUES(@CodigoContrato, {2}, {3}, {4}, {5}, {6}, {7}, GETDATE(), @CodigoUsuario, {8});

                                ", dbName, dbOwner
                                 , dr["NumeroParcela"].ToString()
                                 , dr["ValorPrevisto"].ToString().Replace(",", ".")
                                 , strVencto
                                 , strPagto
                                 , dr["ValorPago"].ToString() != "" ? "'" + dr["ValorPago"].ToString().Replace(",", ".") + "'" : "NULL"
                                 , dr["HistoricoParcela"].ToString() != "" ? "'" + dr["HistoricoParcela"].ToString().Replace("'","''") + "'" : "NULL"
                                 , dr["NumeroAditivoContrato"].ToString()
                                 );
            }
            else if (dr["TipoRegistro"].ToString().Equals("EA"))
            {
                strUpdate += string.Format(@"
                                UPDATE {0}.{1}.ParcelaContrato
                                SET
                                      ValorPrevisto     = {3}
                                    , DataVencimento    = {4}
                                    , DataPagamento     = {5}
                                    , ValorPago         = {6}
                                    , HistoricoParcela  = {7}
                                    , DataUltimaAlteracao           = GETDATE()
                                    , CodigoUsuarioUltimaAlteracao  =  @CodigoUsuario

                                WHERE   CodigoContrato          = @CodigoContrato
                                  AND   NumeroParcela           = {2}
                                  AND   NumeroAditivoContrato   = {8};
                                ", dbName, dbOwner
                                 , dr["NumeroParcela"].ToString()
                                 , dr["ValorPrevisto"].ToString().Replace(",",".")
                                 , strVencto
                                 , strPagto
                                 , dr["ValorPago"].ToString() != "" ? dr["ValorPago"].ToString().Replace(",", ".")      : "NULL"
                                 , dr["HistoricoParcela"].ToString() != "" ? "'" + dr["HistoricoParcela"].ToString().Replace("'", "''") + "'" : "NULL"
                                 , dr["NumeroAditivoContrato"].ToString()
                                 );

            }
            else if (dr["TipoRegistro"].ToString().Equals("EX"))
            {
                strDelete += string.Format(@"
                                DELETE FROM {0}.{1}.ParcelaContrato
                                WHERE   CodigoContrato          = @CodigoContrato
                                  AND   NumeroParcela           = {2}
                                  AND   NumeroAditivoContrato   = {3};

                                ", dbName, dbOwner
                                 , dr["NumeroParcela"].ToString()
                                 , dr["NumeroAditivoContrato"].ToString());
            }
        }
        return strDelete + strInsert + strUpdate;
    }

    #endregion

    #region ANDRE

    protected void pnCallback1_Callback(object sender, CallbackEventArgsBase e)
    {
        hfAnexos.Set("hfErro", "");
        if (e.Parameter == "excluir")
        {
            int codigoAnexo = int.Parse(hfAnexos.Contains("CodigoAnexo") == true ? hfAnexos.Get("CodigoAnexo").ToString() : "-1");

            int codigoPastaSup = int.Parse(hfAnexos.Contains("CodigoPastaSuperior") == true ? hfAnexos.Get("CodigoPastaSuperior").ToString() : "-1");

            string indicaPasta = hfAnexos.Contains("IndicaPasta") == true ? hfAnexos.Get("IndicaPasta").ToString() : "N";

            string erro = "";
            if (indicaPasta != "")
            {
                bool retorno = cDados.excluiAnexoProjeto(char.Parse(indicaPasta), codigoAnexo, idUsuarioResponsavel, idEntidadeLogado, int.Parse(getChavePrimaria()), cDados.getCodigoTipoAssociacao("CT"), ref erro);
                if (!retorno)
                {
                    hfAnexos.Set("hfErro", erro);
                }
                else
                {
                    hfAnexos.Set("CodigoAnexo", -1);
                    hfAnexos.Set("CodigoPastaSuperior", -1);
                    hfAnexos.Set("IndicaPasta", "");
                }
            }
            populaTreeListArquivos();
        }
        if (e.Parameter == "Listar")
        {
            populaTreeListArquivos();
        }
        tabControl.EnableViewState = false;
        spnControlesAnexo.Visible = (hfGeral.Get("TipoOperacao").ToString() != "Consultar");
    }

    private void populaTreeListArquivos()
    {
        int erro = -1;
        hfAnexos.Set("IDObjetoAssociado", getChavePrimaria());
        int objAssociado = int.TryParse(getChavePrimaria(), out erro) ? int.Parse(getChavePrimaria()) : erro;

        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("CT");

        dsTreelist = cDados.getAnexos(objAssociado, codigoTipoAssociacao, idEntidadeLogado);
        if ((cDados.DataSetOk(dsTreelist)))
        {
            tlAnexos.DataSource = dsTreelist.Tables[0];
            tlAnexos.DataBind();
        }

    }

    protected string GetIconUrl(TreeListDataCellTemplateContainer container)
    {
        //ASPxImage imgDownLoad = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "imgDownload") as ASPxImage;
        ASPxButton btnDownLoad = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "btnDownLoad") as ASPxButton;
        string nomeIcone = "pasta.gif";
        string IndicaPasta = container.GetValue("IndicaPasta").ToString();
        if (IndicaPasta == "N")
        {
            btnDownLoad.ClientInstanceName = "Btn_CodigoAnexo_" + container.GetValue("CodigoAnexo").ToString();
            if (btnDownLoad != null)
                btnDownLoad.Visible = true;
            nomeIcone = "arquivo.gif";
            string NomeArquivo = container.GetValue("Nome").ToString();
        }
        else
            if (btnDownLoad != null)
                btnDownLoad.Visible = false;

        return string.Format("~/imagens/anexo/{0}", nomeIcone);
    }

    protected string GetToolTip(TreeListDataCellTemplateContainer container)
    {
        string toolTip = "Incluído em: " + container.GetValue("DataInclusao").ToString() + Environment.NewLine + "por: " + container.GetValue("NomeUsuario").ToString() + Environment.NewLine + "==================================" + Environment.NewLine + container.GetValue("DescricaoAnexo").ToString();
        return toolTip;
    }

    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        string CodigoAnexo = (sender as ASPxButton).ClientInstanceName.Substring(16);
        cDados.download(int.Parse(CodigoAnexo), null, Page, Response, Request, true);

    }

    #endregion

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados(idEntidadeLogado);
    }
    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
