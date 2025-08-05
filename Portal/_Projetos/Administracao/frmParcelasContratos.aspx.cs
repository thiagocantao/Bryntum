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
using System.Globalization;

public partial class _Projetos_Administracao_frmParcelasContratos : System.Web.UI.Page
{
    dados cDados;

    int codigoContrato = -1;
    public string somenteLeitura = "";
    DateTime dtInicioVigencia = DateTime.Now;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool geraLancamentoFinanceiro = false;
    string IniciaisTipoAssociacao = "CT";
    public bool utilizaConvenio = false;

    protected void Page_Init(object sender, EventArgs e)
    {

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        try
        {
            System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

            listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
            listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

            cDados = CdadosUtil.GetCdados(listaParametrosDados);

            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }


        dsConta.ConnectionString = cDados.classeDados.getStringConexao();
        dsProjetos.ConnectionString = cDados.classeDados.getStringConexao();
        DataSet ds = cDados.getParametrosSistema("utilizaConvenio");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            utilizaConvenio = (ds.Tables[0].Rows[0][0].ToString() == "S");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string swhere1 = "";


        if (Request.QueryString["CC"] != null)
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvParcelas.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 175;
        }

        if (Request.QueryString["IVG"] != null && Request.QueryString["IVG"].ToString() != "")
        {
            string format = "dd/MM/yyyy";
            DateTime.TryParseExact(Request.QueryString["IVG"].ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicioVigencia);
            gvParcelas.JSProperties["cp_InicioVigencia"] = Request.QueryString["IVG"].ToString();
        }

        hfGeral.Set("TipoOperacaoEmParcela", somenteLeitura == "S" ? "Consultar" : "Editar");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        Session["sfCodigoEntidade"] = codigoEntidadeUsuarioResponsavel;

        //if (!IsPostBack)
        //    gvParcelas.FilterExpression = "[TipoRegistro] <> 'EX'";

        cDados.aplicaEstiloVisual(this);

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelPrevistoParcelaContrato", "labelVencimentoParcelaContrato", "parcelaGeraLancamentoFinanceiro");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() != "")
            {
                ((GridViewDataTextColumn)gvParcelas.Columns["ValorPrevisto"]).Caption = dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString();
                ((GridViewDataTextColumn)gvParcelas.Columns["ValorPrevisto"]).EditFormSettings.Caption = dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() + ":";
            }

            if (dsParametros.Tables[0].Rows[0]["labelVencimentoParcelaContrato"].ToString() != "")
            {
                ((GridViewDataDateColumn)gvParcelas.Columns["DataVencimento"]).Caption = dsParametros.Tables[0].Rows[0]["labelVencimentoParcelaContrato"].ToString();
                ((GridViewDataDateColumn)gvParcelas.Columns["DataVencimento"]).EditFormSettings.Caption = dsParametros.Tables[0].Rows[0]["labelVencimentoParcelaContrato"].ToString() + ":";
            }

            if (dsParametros.Tables[0].Rows[0]["parcelaGeraLancamentoFinanceiro"].ToString() != "")
            {
                geraLancamentoFinanceiro = dsParametros.Tables[0].Rows[0]["parcelaGeraLancamentoFinanceiro"].ToString() == "S";
            }
        }

        // se tipo pessoa foi informado na chamada da página e parâmetro gera lançamento financeiro = sim, popula o combo de conta contábil de acordo com tipo da pessoa
        // caso contrário as informações de conta são inativadas
        if (Request.QueryString["TP"] != null && geraLancamentoFinanceiro)
        {
            gvParcelas.JSProperties["cp_TP"] = Request.QueryString["TP"].ToString();
            string depesaReceita = Request.QueryString["TP"].ToString();

            switch (depesaReceita)
            {
                case "F":
                    depesaReceita = "S";
                    break;
                case "C":
                    depesaReceita = "E";
                    break;
                default:
                    depesaReceita = "";
                    break;
            }
            swhere1 = depesaReceita;

        }
        else
        {
            gvParcelas.Columns["CodigoConta"].Visible = false;

            ((GridViewDataComboBoxColumn)gvParcelas.Columns["CodigoConta"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        Session["sfEntradaSaida"] = swhere1;
        //somenteLeitura = "S";
        if (somenteLeitura == "S")
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
        }
        else
        {
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncPar");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcPar");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltPar");

            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }
        defineConfiguracoesContratoPrograma();
        carregaGvParcelas();

        gvParcelas.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvParcelas.JSProperties["cp_SomenteLeitura"] = somenteLeitura;

        gvParcelas.JSProperties["cp_utilizaConvenio"] = utilizaConvenio == true ? "S" : "N";

        DataSet ds1 = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            txtNumeroContrato.Text = ds1.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds1.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = (ds1.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString());
            txtInicioVigencia.Text = string.IsNullOrWhiteSpace(ds1.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : ((DateTime)ds1.Tables[0].Rows[0]["DataInicio"]).ToString("d");
            txtTerminoVigencia.Text = string.IsNullOrWhiteSpace(ds1.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : ((DateTime)ds1.Tables[0].Rows[0]["DataTermino"]).ToString("d");
            //txtInicioVigencia.Text = ds1.Tables[0].Rows[0]["DataInicio"] == null ? "" : DateTime.Parse(ds1.Tables[0].Rows[0]["DataInicio"].ToString()).ToString("dd/MM/yyyy");
            //txtTerminoVigencia.Text = ds1.Tables[0].Rows[0]["DataTermino"] == null ? "" : DateTime.Parse(ds1.Tables[0].Rows[0]["DataTermino"].ToString()).ToString("dd/MM/yyyy");
        }
    }

    private void defineConfiguracoesContratoPrograma()
    {
        bool colunaVisivel = false;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoContrato INT
    SET @CodigoContrato = {0}
  
 SELECT p.CodigoProjeto AS CodigoPrograma
   FROM Contrato AS c INNER JOIN
        Projeto AS p ON p.CodigoProjeto = c.CodigoProjeto
  WHERE c.CodigoContrato = @CodigoContrato
    AND p.IndicaPrograma = 'S'", codigoContrato);

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            var valor = dt.Rows[0]["CodigoPrograma"];
            if (valor is int)
            {
                Session["codProg"] = valor;
                colunaVisivel = true;
            }
        }

        gvParcelas.Columns["CodigoProjetoParcela"].Visible = colunaVisivel;
    }

    private void carregaGvParcelas()
    {
        DataSet ds = cDados.getParcelaContrato(codigoContrato.ToString());

        if (cDados.DataSetOk(ds))
        {
            gvParcelas.DataSource = ds;
            gvParcelas.DataBind();

            string parcelas = "";

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TipoRegistro"].ToString() != "EX")
                    parcelas += dr["NumeroAditivoContrato"].ToString() + dr["NumeroParcela"].ToString() + ";";
            }
            if (parcelas.Length > 0)
            {
                parcelas = parcelas.Remove(parcelas.Length - 1);
                hfGeral.Set("ListaDeParcelas", parcelas);
            }
            else
            {
                hfGeral.Set("ListaDeParcelas", "");
            }
        }

    }

    protected void gvParcelas_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "HistoricoParcela")
        {
            if (e.CellValue.ToString().Length > 30)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 30) + "...";
            }
        }
    }

    protected void gvParcelas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        e.Editor.ClientEnabled = true;
        e.Editor.DisabledStyle.ForeColor = Color.Black;
        e.Editor.DisabledStyle.BackColor = Color.White;

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
        }

        if (e.Column.Name == "DataVencimento")
        {
            ((ASPxDateEdit)e.Editor).MinDate = dtInicioVigencia;
        }
        if (e.Column.Name == "DataPagamento")
        {
            ((ASPxDateEdit)e.Editor).MaxDate = DateTime.Now;
        }
    }

    protected void gvParcelas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string codigoMedicao = gvParcelas.GetRowValues(e.VisibleIndex, "CodigoMedicao") + "";

        if (somenteLeitura == "S" || codigoMedicao != "")
        {
            podeIncluir = false;
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
        else
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
            {
                if (podeIncluir)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/incluirRegDes.png";
                }
            }
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
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
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
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

    protected void gvParcelas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["NumeroParcela"] != null)
        {
            string numeroParcela = e.NewValues["NumeroParcela"].ToString();
            string numeroAditivoContrato = e.NewValues["NumeroAditivoContrato"].ToString();
            string valorPrevisto = e.NewValues["ValorPrevisto"].ToString().Replace(',', '.');
            string dataVencimento = e.NewValues["DataVencimento"] != null ? e.NewValues["DataVencimento"].ToString() : "";
            string dataPagamento = e.NewValues["DataPagamento"] != null ? e.NewValues["DataPagamento"].ToString() : "";
            string valorPago = e.NewValues["ValorPago"] != null ? e.NewValues["ValorPago"].ToString().Replace(',', '.') : "";
            string historicoParcela = e.NewValues["HistoricoParcela"] != null ? e.NewValues["HistoricoParcela"].ToString() : "";

            string strVencto = dataVencimento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataVencimento);
            string strPagto = dataPagamento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataPagamento);

            //Customizações 09/2012
            string numeroNF = e.NewValues["NumeroNF"] != null ? string.Format("'{0}'", e.NewValues["NumeroNF"]) : "NULL";
            string valorretencao = e.NewValues["ValorRetencao"] != null ? e.NewValues["ValorRetencao"].ToString().Replace(',', '.') : "";
            string valorMulta = e.NewValues["ValorMultas"] != null ? e.NewValues["ValorMultas"].ToString().Replace(',', '.') : "";
            string codigoConta = e.NewValues["CodigoConta"] != null ? e.NewValues["CodigoConta"].ToString().Replace(',', '.') : "";
            string codigoProjetoParcela = e.NewValues["CodigoProjetoParcela"] != null ? e.NewValues["CodigoProjetoParcela"].ToString().Replace(',', '.') : "";
            string mensagemErro = "";
            //cDados.incluiParcelaContratoAquisicoes(codigoContrato, numeroParcela, valorPrevisto, strVencto, strPagto, valorPago,
            //    historicoParcela, numeroAditivoContrato, codigoUsuarioResponsavel, numeroNF, valorretencao, valorMulta, codigoConta, codigoProjetoParcela, ref mensagemErro);

            carregaGvParcelas();
        }

        e.Cancel = true;
        gvParcelas.CancelEdit();
    }

    protected void gvParcelas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["NumeroParcela"] != null)
        {
            string numeroParcela = e.NewValues["NumeroParcela"].ToString();
            string numeroAditivoContrato = e.NewValues["NumeroAditivoContrato"].ToString();
            string valorPrevisto = e.NewValues["ValorPrevisto"].ToString();
            string dataVencimento = e.NewValues["DataVencimento"] != null ? e.NewValues["DataVencimento"].ToString() : "";
            string dataPagamento = e.NewValues["DataPagamento"] != null ? e.NewValues["DataPagamento"].ToString() : "";
            string valorPago = e.NewValues["ValorPago"] != null ? e.NewValues["ValorPago"].ToString() : "";
            string historicoParcela = e.NewValues["HistoricoParcela"] != null ? e.NewValues["HistoricoParcela"].ToString() : "";

            string strVencto = dataVencimento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataVencimento);
            string strPagto = dataPagamento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataPagamento);

            //Customizações 09/2012
            string numeroNF = e.NewValues["NumeroNF"] != null ? string.Format("'{0}'", e.NewValues["NumeroNF"]) : "NULL";
            string valorretencao = e.NewValues["ValorRetencao"] != null ? e.NewValues["ValorRetencao"].ToString() : "";
            string valorMulta = e.NewValues["ValorMultas"] != null ? e.NewValues["ValorMultas"].ToString() : "";
            string codigoConta = e.NewValues["CodigoConta"] != null ? e.NewValues["CodigoConta"].ToString().Replace(',', '.') : "";
            string codigoProjetoParcela = e.NewValues["CodigoProjetoParcela"] != null ? e.NewValues["CodigoProjetoParcela"].ToString().Replace(',', '.') : "";
            string mensagemErro = "";
            //cDados.atualizaParcelaContratoAquisicoes(codigoContrato, numeroParcela, valorPrevisto, strVencto, strPagto, valorPago,
            //    historicoParcela, numeroAditivoContrato, codigoUsuarioResponsavel, numeroNF, valorretencao, valorMulta, codigoConta, codigoProjetoParcela, ref mensagemErro);

            carregaGvParcelas();
        }



        e.Cancel = true;
        gvParcelas.CancelEdit();
    }

    protected void gvParcelas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string numeroParcela = e.Keys["NumeroParcela"].ToString();
        string numeroAditivoContrato = e.Keys["NumeroAditivoContrato"].ToString();

        if (!utilizaConvenio || VerificaPermiteExclusaoParcelaContrato(numeroParcela, numeroAditivoContrato))
        {
            cDados.excluiParcelaContratoAquisicoes(codigoContrato, numeroParcela, numeroAditivoContrato);

            carregaGvParcelas();

            ((ASPxGridView)sender).JSProperties["cpCodigoContrato"] = codigoContrato;
        }
        else
            throw new Exception("Não é possível excluir a parcela.");
        gvParcelas.CancelEdit();
        e.Cancel = true;
    }

    private bool VerificaPermiteExclusaoParcelaContrato(string numeroAditivo, string numeroParcela)
    {
        var permiteExclusao = false;
        var comandoSql = string.Format("SELECT dbo.f_gestconv_podeExcluirParcelaContrato({0}, {1}, {2}, {3})"
            , codigoContrato, numeroAditivo, numeroParcela, codigoUsuarioResponsavel);
        var ds = cDados.getDataSet(comandoSql);
        permiteExclusao = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

        return permiteExclusao;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ParcContra");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        string urlPopupContrato = "";
        if (utilizaConvenio == false)
        {
            urlPopupContrato = @"_Projetos/Administracao/popupParcelaContrato.aspx?";
            urlPopupContrato += "na=-1";
            urlPopupContrato += "&np=-1";
            urlPopupContrato += "&cpp=-1";
            urlPopupContrato += string.Format(@"&cc={0}", codigoContrato);
            urlPopupContrato += "&IVG=" + Request.QueryString["IVG"].ToString();
            urlPopupContrato += "&RO=N";
            urlPopupContrato += "&TP=" + Request.QueryString["TP"];
            urlPopupContrato += "&tipo=PAR";
        }
        else
        {
            urlPopupContrato = @"_Projetos/Administracao/LancamentosFinanceirosConvenio.aspx?";
            urlPopupContrato += string.Format(@"cc={0}", codigoContrato);
            urlPopupContrato += "&clf=-1";
            urlPopupContrato += "&tipo=PAR";
        }

        string script_inclusao_nova_parcela = string.Format(@"
        var sHeight = Math.max(0, document.documentElement.clientHeight) + 70;
        var sWidth = Math.max(0, document.documentElement.clientWidth) + 70;   
        
        var url = window.top.pcModal.cp_Path + '{0}';
        url += '&ALT=' + (sHeight - 0);     
        window.top.showModal2(url, 'Editar detalhes da parcela', sWidth, sHeight, atualizaGrid, null);", urlPopupContrato);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, script_inclusao_nova_parcela, true, true, false, "ParcContra", "Parcelas Contrato", this);
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
    protected void gvParcelas_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }

    protected void gvParcelas_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvParcelas();
    }

    protected void gvParcelas_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar == false || somenteLeitura.Trim().ToUpper() == "S")
            {
                e.Image.Url = "~/imagens/botoes/pFormulario.PNG";
            }
        }
        else if(e.ButtonID == "btnCloud")
        {
            /*
             -> Se a parcela estiver atualizada, manter a mensagem atual:
                "Parcela originária de integraçao com sistemas externos"

             -> Se a parcela estiver DESatualizada, a mensagem será a seguinte:
                "Parcela originária de integraçao com sistemas externos. Última atualização em dd/MM/aaaa HH:mm" {usar o campo [xxx] retornado pela function f_GetParcelaContrato()}
            */
            var IndicaTipoIntegracao = (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "IndicaTipoIntegracao") == null) ? "" : ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "IndicaTipoIntegracao").ToString();
            var dataAtualizacao = (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "DataUltimaIntegracao") == null) ? "" : ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "DataUltimaIntegracao").ToString();
            e.Visible = (IndicaTipoIntegracao.ToString() == "") ? DevExpress.Utils.DefaultBoolean.False : DevExpress.Utils.DefaultBoolean.True;
            if (e.Visible == DevExpress.Utils.DefaultBoolean.True)
            {
                string IndicaDadoIntegracaoAtualizado = (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "IndicaDadoIntegracaoAtualizado") == null) ? "N" : ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "IndicaDadoIntegracaoAtualizado").ToString();
                if (IndicaDadoIntegracaoAtualizado == "N")
                {
                    e.Image.Url = "~/imagens/botoes/cloudError1.png";
                    if (!string.IsNullOrEmpty(dataAtualizacao))
                    {
                        DateTime dtAtualizacao = DateTime.Parse(dataAtualizacao);
                        string dataformatada = dtAtualizacao.ToString("dd/MM/yyyy HH:mm");
                        e.Image.ToolTip = string.Format(@"Parcela originária de integração com sistemas externos. Última atualização em {0}.", dataformatada);
                    }
                    else
                    {
                        e.Image.ToolTip = string.Format(@"Parcela originária de integração com sistemas externos. Última data de sincronização indeterminada!");
                    }
                }
                else
                {
                    e.Image.Url = "~/imagens/botoes/cloud2.png";
                    e.Image.ToolTip = "Parcela originária de integração com sistemas externos.";
                }
            }
        }
    }
}
