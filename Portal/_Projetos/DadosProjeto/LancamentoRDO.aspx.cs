using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Globalization;
using System.Drawing;

public partial class _Projetos_DadosProjeto_LancamentoRDO : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = 0;
    int codigoUsuario = 0;
    int codigoEntidade = 0;
    DataTable dtb;
    ASPxDateEdit dte = new ASPxDateEdit();
    int codigoRDO = -1;
    bool podeIncluir = false;
    bool podeEditar = false;

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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        dte = gvCabecalho.FindTitleTemplateControl("deData") as ASPxDateEdit;

        dte.AutoPostBack = true;


        dte.MaxDate = DateTime.Now;

        if (!IsPostBack && !IsCallback)
        {
            string tipoAssociacao = cDados.getIniciaisTipoAssociacaoProjeto(codigoProjeto);
            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", tipoAssociacao, 0, "null", "PR_RelRDO");
            dte.Date = DateTime.Now;
            hfGeral.Set("Data", dte.Text);
        }
        else
        {
            dte.Text = hfGeral.Get("Data").ToString();
        }


        definePoliticasDeAcessoComPrazo();



        setCodigoRDO();
        carregaDadosRDO();
        defineTamanhoObjetos();

        gvCabecalho.JSProperties["cp_UrlOcorrencias"] = cDados.getPathSistema() + "_Projetos/DadosProjeto/OcorrenciasRdo.aspx?DataRdo=" + Server.UrlEncode(hfGeral.Get("Data").ToString()) + "&CodigoProjeto=" + codigoProjeto;
        gvCabecalho.JSProperties["cp_UrlPDFRDO"] = cDados.getPathSistema() + "_Projetos/Relatorios/rel_RDO.aspx?CodigoRdo=" + codigoRDO + "&CodigoProjeto=" + codigoProjeto;

        callbackSalvarCabecalho.JSProperties["cp_UrlPDFRDO"] = cDados.getPathSistema() + "_Projetos/Relatorios/rel_RDO.aspx?CodigoRdo=" + codigoRDO + "&CodigoProjeto=" + codigoProjeto + "&DataRdo=" + dte.Text;


        dtb = new DataTable();
        dtb = CriaDataTable();

        this.gvDados.DataSource = dtb;
        this.gvDados.DataBind();
        gvDados.ExpandAll();


        //comentar este if em caso de mudança pra session
        //if (!IsPostBack && !IsCallback)
        //{
        dte.ClientSideEvents.Init = @"function(s,e){
                                              lpCarregando.Hide(); 
                                              window.parent.document.getElementById('frm001').src = './CadastroRDO.aspx?CP=" + codigoProjeto + @"&Data=' + convertDate(s.GetDate());
                                              window.parent.document.getElementById('frm003').src = './ComentarioRDO.aspx?CP=" + codigoProjeto + @"&DataRdo=' + convertDate(s.GetDate()) + '&Altura=" + (gvDados.Settings.VerticalScrollableHeight + 300) + "';}";
        dte.ClientSideEvents.DateChanged = @"function(s,e){
                                                    hfGeral.Set('Data', convertDate(s.GetDate()));
                                                    lpCarregando.Show();
                                                    window.parent.document.getElementById('frm001').src = './CadastroRDO.aspx?CP=" + codigoProjeto + @"&Data=' + convertDate(s.GetDate());
                                                    window.parent.document.getElementById('frm003').src = './ComentarioRDO.aspx?CP=" + codigoProjeto + @"&DataRdo=' + convertDate(s.GetDate()) + '&Altura=" + (gvDados.Settings.VerticalScrollableHeight + 300) + @"';
                                                }";
        //}

    }


    private void definePoliticasDeAcessoComPrazo()
    {
        int diasPrazoInclusaoEdicaoRDO;

        DataSet ds = cDados.getParametrosSistema("PrazoInclusaoEdicaoRDO");
        if (cDados.DataSetOk(ds))
        {
            /*se de hoje até o dte tiverem se passado mais que ->diasPrazoInclusaoEdicaoRDO<- dias então*/
            diasPrazoInclusaoEdicaoRDO = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            DateTime dataAux = DateTime.Now.AddDays(-diasPrazoInclusaoEdicaoRDO);

            if ((DateTime)dte.Value <= DateTime.Now &&
                (DateTime)dte.Value >= dataAux)
            {
                //pode incluir sem precisar de permissao especifica
                podeIncluir = true;
                podeEditar = true;
            }
            else
            {
                //PRECISA DE permissao especifica
                podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");
                podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");
            }


        }

    }

    private void setCodigoRDO()
    {
        dte = gvCabecalho.FindTitleTemplateControl("deData") as ASPxDateEdit;
        string comandoSQL = string.Format(@"SELECT ISNULL(CodigoRdo, -1) AS CodigoRdo FROM {0}.{1}.Rdo_DadosColetados WHERE DataRdo = CONVERT(DateTime, '{2:dd/MM/yyyy}', 103) AND CodigoProjeto = {3}", cDados.getDbName(), cDados.getDbOwner(), dte.Date, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoRDO = int.Parse(ds.Tables[0].Rows[0]["CodigoRdo"].ToString());
        
        
    }

    private void insereRDO()
    {
        string comandoSQL = string.Format(@"
                    DECLARE @CodigoRDO Int
                    INSERT INTO {0}.{1}.Rdo_DadosColetados (DataRdo, CodigoProjeto, DataInclusao, CodigoUsuarioInclusao)
				               VALUES(CONVERT(DateTime, '{2}', 103),           {3},    getdate(), {4});
                    SET @CodigoRDO = SCOPE_IDENTITY();", cDados.getDbName(), cDados.getDbOwner(), dte.Date, codigoProjeto, codigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoRDO = int.Parse(ds.Tables[0].Rows[0]["CodigoRDO"].ToString());
    }

    private void carregaDadosRDO()
    {
        string comandoSQL = string.Format(@"
 DECLARE @HorasParalisadasAcumulado DECIMAL(20,8)
     SET @HorasParalisadasAcumulado = [dbo].[f_acumuladoHorasParalisadasRDO]('{3:dd/MM/yyyy}',{4})

            SELECT [CodigoRdo]
                  ,[DataRdo]
                  ,[CodigoProjeto]
                  ,[ME_Dia_Bom]
                  ,[ME_Dia_Precipitacao]
                  ,[ME_Noite_Bom]
                  ,[ME_Noite_Precipitacao]
                  ,[ME_Impraticavel]
                  ,[MD_Dia_Bom]
                  ,[MD_Dia_Precipitacao]
                  ,[MD_Noite_Bom]
                  ,[MD_Noite_Precipitacao]
                  ,[MD_Impraticavel]
                  ,[HorasParalisadas]
                  ,[FrenteServico]
                  ,[EncarregadoGeral]
                  ,[CodigoConstrutora]
              FROM [{0}].[{1}].[Rdo_DadosColetados]
             WHERE CodigoRdo = {2}

            SELECT @HorasParalisadasAcumulado AS HorasParalisadasAcumulado"
            , cDados.getDbName(), cDados.getDbOwner(), codigoRDO, dte.Date, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ASPxCheckBox cb_DiaBomME = gvCabecalho.FindEmptyDataRowTemplateControl("ckBomDiaME") as ASPxCheckBox;
        ASPxCheckBox cb_NoiteBomME = gvCabecalho.FindEmptyDataRowTemplateControl("ckBomNoiteME") as ASPxCheckBox;
        ASPxCheckBox cb_DiaBomMD = gvCabecalho.FindEmptyDataRowTemplateControl("ckBomDiaMD") as ASPxCheckBox;
        ASPxCheckBox cb_NoiteBomMD = gvCabecalho.FindEmptyDataRowTemplateControl("ckBomNoiteMD") as ASPxCheckBox;

        ASPxSpinEdit spinPrecipitacaoDiaME = gvCabecalho.FindEmptyDataRowTemplateControl("txtPrecipitacaoDiaME") as ASPxSpinEdit;
        ASPxSpinEdit spinPrecipitacaoNoiteME = gvCabecalho.FindEmptyDataRowTemplateControl("txtPrecipitacaoNoiteME") as ASPxSpinEdit;
        ASPxSpinEdit spinPrecipitacaoDiaMD = gvCabecalho.FindEmptyDataRowTemplateControl("txtPrecipitacaoDiaMD") as ASPxSpinEdit;
        ASPxSpinEdit spinPrecipitacaoNoiteMD = gvCabecalho.FindEmptyDataRowTemplateControl("txtPrecipitacaoNoiteMD") as ASPxSpinEdit;

        ASPxTextBox txtImpraticavelME = gvCabecalho.FindEmptyDataRowTemplateControl("txtImpraticavelME") as ASPxTextBox;
        ASPxTextBox txtImpraticavelMD = gvCabecalho.FindEmptyDataRowTemplateControl("txtImpraticavelMD") as ASPxTextBox;

        ASPxSpinEdit spinHorasParalisadasDia = gvCabecalho.FindEmptyDataRowTemplateControl("txtHorasParalisadasDia") as ASPxSpinEdit;
        ASPxSpinEdit spinHorasParalisadasAcumulado = gvCabecalho.FindEmptyDataRowTemplateControl("txtHorasParalisadasAcumulado") as ASPxSpinEdit;

        ASPxMemo memoFrenteServico = gvCabecalho.FindEmptyDataRowTemplateControl("txtFrenteServico") as ASPxMemo;
        ASPxMemo memoEncarregadoGeral = gvCabecalho.FindEmptyDataRowTemplateControl("txtEncarregadoGeral") as ASPxMemo;

        ASPxTextBox txtCodigo = gvCabecalho.FindEmptyDataRowTemplateControl("txtCodigo") as ASPxTextBox;


        cb_DiaBomME.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        cb_DiaBomME.DisabledStyle.ForeColor = Color.FromName("Black");

        cb_NoiteBomME.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        cb_NoiteBomME.DisabledStyle.ForeColor = Color.FromName("Black");

        cb_DiaBomMD.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        cb_DiaBomMD.DisabledStyle.ForeColor = Color.FromName("Black");

        cb_NoiteBomMD.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        cb_NoiteBomMD.DisabledStyle.ForeColor = Color.FromName("Black");


        spinPrecipitacaoDiaME.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinPrecipitacaoDiaME.DisabledStyle.ForeColor = Color.FromName("Black");


        spinPrecipitacaoNoiteME.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinPrecipitacaoNoiteME.DisabledStyle.ForeColor = Color.FromName("Black");


        spinPrecipitacaoDiaMD.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinPrecipitacaoDiaMD.DisabledStyle.ForeColor = Color.FromName("Black");

        spinPrecipitacaoNoiteMD.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinPrecipitacaoNoiteMD.DisabledStyle.ForeColor = Color.FromName("Black");

        txtImpraticavelME.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        txtImpraticavelME.DisabledStyle.ForeColor = Color.FromName("Black");

        txtImpraticavelMD.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        txtImpraticavelMD.DisabledStyle.ForeColor = Color.FromName("Black");

        spinHorasParalisadasDia.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinHorasParalisadasDia.DisabledStyle.ForeColor = Color.FromName("Black");

        spinHorasParalisadasAcumulado.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        spinHorasParalisadasAcumulado.DisabledStyle.ForeColor = Color.FromName("Black");


        memoFrenteServico.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        memoFrenteServico.DisabledStyle.ForeColor = Color.FromName("Black");
        
        memoEncarregadoGeral.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        memoEncarregadoGeral.DisabledStyle.ForeColor = Color.FromName("Black");

        txtCodigo.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        txtCodigo.DisabledStyle.ForeColor = Color.FromName("Black");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            cb_DiaBomME.Value = dr["ME_Dia_Bom"];
            cb_NoiteBomME.Value = dr["ME_Noite_Bom"];
            cb_DiaBomMD.Value = dr["MD_Dia_Bom"];
            cb_NoiteBomMD.Value = dr["MD_Noite_Bom"];

            spinPrecipitacaoDiaME.Value = dr["ME_Dia_Precipitacao"];
            spinPrecipitacaoNoiteME.Value = dr["ME_Noite_Precipitacao"];
            spinPrecipitacaoDiaMD.Value = dr["MD_Dia_Precipitacao"];
            spinPrecipitacaoNoiteMD.Value = dr["MD_Noite_Precipitacao"];

            txtImpraticavelME.Text = dr["ME_Impraticavel"].ToString();
            txtImpraticavelMD.Text = dr["MD_Impraticavel"].ToString();

            spinHorasParalisadasDia.Value = dr["HorasParalisadas"];
            spinHorasParalisadasAcumulado.Value = ds.Tables[1].Rows[0]["HorasParalisadasAcumulado"];

            memoFrenteServico.Text = dr["FrenteServico"].ToString();
            memoEncarregadoGeral.Text = dr["EncarregadoGeral"].ToString();

            txtCodigo.Text = dr["CodigoConstrutora"].ToString();
        }
        else
        {
            cb_DiaBomME.Value = "N";
            cb_NoiteBomME.Value = "N";
            cb_DiaBomMD.Value = "N";
            cb_NoiteBomMD.Value = "N";

            spinPrecipitacaoDiaME.Value = null;
            spinPrecipitacaoNoiteME.Value = null;
            spinPrecipitacaoDiaMD.Value = null;
            spinPrecipitacaoNoiteMD.Value = null;

            txtImpraticavelME.Text = "";
            txtImpraticavelMD.Text = "";

            spinHorasParalisadasDia.Value = null;
            spinHorasParalisadasAcumulado.Value = ds.Tables[1].Rows[0]["HorasParalisadasAcumulado"];

            memoFrenteServico.Text = "";
            memoEncarregadoGeral.Text = "";

            txtCodigo.Text = "";
        }

        cb_DiaBomME.ClientEnabled = podeEditar;
        cb_NoiteBomME.ClientEnabled = podeEditar;
        cb_DiaBomMD.ClientEnabled = podeEditar;
        cb_NoiteBomMD.ClientEnabled = podeEditar;

        spinPrecipitacaoDiaME.ClientEnabled = podeEditar;
        spinPrecipitacaoNoiteME.ClientEnabled = podeEditar;
        spinPrecipitacaoDiaMD.ClientEnabled = podeEditar;
        spinPrecipitacaoNoiteMD.ClientEnabled = podeEditar;

        txtImpraticavelME.ClientEnabled = podeEditar;
        txtImpraticavelMD.ClientEnabled = podeEditar;

        spinHorasParalisadasDia.ClientEnabled = podeEditar;
        spinHorasParalisadasAcumulado.ClientEnabled = podeEditar;

        memoFrenteServico.ClientEnabled = podeEditar;
        memoEncarregadoGeral.ClientEnabled = podeEditar;

        txtCodigo.ClientEnabled = podeEditar;
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        gvDados.Settings.VerticalScrollableHeight = (altura - 530);

    }

    private DataTable CriaDataTable()
    {
        string comandoSQL = string.Format(@"
                     SELECT ri.CodigoItem, ri.DescricaoItem, ai.TipoItem, ai.NumeroOrdem AS NumeroItem
			               ,ai.NumeroOrdem AS NumeroTipo, qi.Quantidade
			               ,CASE WHEN ai.CategoriaItem = 'EQP' THEN 'EQUIPAMENTOS MOBILIZADOS'
						         WHEN ai.CategoriaItem = 'MOB' THEN 'MÃO DE OBRA UTILIZADA'
						         ELSE '' END AS Categoria
                      FROM {0}.{1}.Rdo_Itens ri INNER JOIN
                           {0}.{1}.Rdo_AgrupamentoItens ai ON ai.CodigoAgrupamento = ri.CodigoAgrupamento LEFT JOIN
                           {0}.{1}.Rdo_QuantidadeItens qi ON (qi.CodigoItem = ri.CodigoItem AND qi.CodigoRdo = {3})
                     WHERE ri.DataExclusao IS NULL
	                   AND ri.CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoRDO);

        DataSet ds = cDados.getDataSet(comandoSQL);

        return ds.Tables[0];
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
        {
            e.Row.BackColor = gvDados.GetRowLevel(e.VisibleIndex) == 0 ? Color.FromName("#EBEBEB") : Color.FromName("#EBEBEB");
            e.Row.ForeColor = Color.Black;
            if (gvDados.GetRowLevel(e.VisibleIndex) == 1 && e.GetValue("TipoItem").ToString().Trim() == "")
            {
                e.Row.Cells[0].Text = "";
                //e.Row.Cells[0].Visible = false;
                e.Row.Style.Add("padding", "0px");
                e.Row.Height = 0;
                e.Row.Cells[0].Style.Add("display", "none");
                e.Row.Style.Add("display", "none");
            }
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            if (codigoRDO == -1)
            {
                insereRDO();
            }

            setCodigoRDO();

            string codigoItem = e.Parameter.Split(';')[0];
            string valor = e.Parameter.Split(';')[1].Replace(',', '.');

            int regAf = 0;

            string comandoSQL = string.Format(@"IF EXISTS(SELECT 1 FROM {0}.{1}.Rdo_QuantidadeItens 
                                                  WHERE CodigoRdo = {2} 
                                                    AND CodigoItem = {3} )
                                            BEGIN
                                                 UPDATE {0}.{1}.Rdo_QuantidadeItens SET Quantidade = {4}
                                                  WHERE CodigoRdo = {2} 
                                                    AND CodigoItem = {3}
                                            END
                                        ELSE
                                            BEGIN
                                                INSERT INTO {0}.{1}.Rdo_QuantidadeItens
                                                       (CodigoRdo
                                                       ,CodigoItem
                                                       ,Quantidade)
                                                 VALUES
                                                       ({2}
                                                       ,{3}
                                                       ,{4})
                                                END", cDados.getDbName(), cDados.getDbOwner(), codigoRDO, codigoItem, valor == "" ? "NULL" : valor);
            
            callbackSalvarCabecalho.JSProperties["cp_UrlPDFRDO"] = cDados.getPathSistema() + "_Projetos/Relatorios/rel_RDO.aspx?CodigoRdo=" + codigoRDO + "&CodigoProjeto=" + codigoProjeto;

            cDados.execSQL(comandoSQL, ref regAf);
        }

    }

    protected void callbackSalvarCabecalho_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            if (codigoRDO == -1)
            {
                insereRDO();
            }

            setCodigoRDO();
            callbackSalvarCabecalho.JSProperties["cp_UrlPDFRDO"] = cDados.getPathSistema() + "_Projetos/Relatorios/rel_RDO.aspx?CodigoRdo=" + codigoRDO + "&CodigoProjeto=" + codigoProjeto + "&DataRdo=" + dte.Text;

            string campo = e.Parameter.Split(';')[0];
            string valor = e.Parameter.Split(';')[1] == "null" ? "NULL" : ("'" + e.Parameter.Split(';')[1].Replace("'", "''") + "'");

            if (campo.Contains("Precipitacao") || campo == "HorasParalisadas")
                valor = valor.Replace(',', '.');
            else if (campo == "ME_Dia_Bom" || campo == "ME_Noite_Bom")
            {
                if (valor == "S")
                {
                    campo = "ME_Impraticavel = NULL, " + campo;
                }
            }
            else if (campo == "MD_Dia_Bom" || campo == "MD_Noite_Bom")
            {
                if (valor == "S")
                {
                    campo = "MD_Impraticavel = NULL, " + campo;
                }
            }

            int regAf = 0;

            string comandoSQL = string.Format(@"UPDATE {0}.{1}.Rdo_DadosColetados SET {2} = {3}, DataUltimaAlteracao = getdate(), CodigoUsuarioAlteracao = {5} WHERE CodigoRdo = {4}", cDados.getDbName(), cDados.getDbOwner(), campo, valor, codigoRDO, codigoUsuario);

            cDados.execSQL(comandoSQL, ref regAf);
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Quantidade")
        {
            ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txt" + e.DataColumn.Name) as ASPxSpinEdit;

            spin.Value = gvDados.GetRowValues(e.VisibleIndex, "Quantidade");

            e.Cell.BackColor = Color.FromName("#E1EAFF");

            spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Name;

            string codigoItem = gvDados.GetRowValues(e.VisibleIndex, "CodigoItem") + "";

            spin.ClientSideEvents.ValueChanged = @"function(s, e) { callbackSalvar.PerformCallback('" + codigoItem + ";' + s.GetValue());}";

            spin.ClientSideEvents.GotFocus = @"function(s, e) { gvDados.SetFocusedRowIndex(" + e.VisibleIndex + ");}";


            string comandoFocus = string.Format(@"if(e.htmlEvent.keyCode == 38)
                                                      navegaSetas('C');", (e.VisibleIndex - 1));

            comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 40)
                                                        navegaSetas('B');", (e.VisibleIndex - 1)
                                                                                        , e.VisibleIndex + 1);

            spin.ClientSideEvents.KeyDown = @"function(s, e) { " + comandoFocus + " }";
            spin.ClientSideEvents.KeyPress = @"function(s, e) { if(e.htmlEvent.keyCode == 13)
                                                                    navegaSetas('B'); }";
            spin.ClientEnabled = podeEditar;
        }
    }

    protected void gvCabecalho_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Cells[0].Style.Add("padding", "0px");
        e.Row.Cells[0].Style.Add("margin", "0px");
        e.Row.Cells[0].Style.Add("border", "1px");
    }

    protected void deData_CalendarDayCellPrepared(object sender, CalendarDayCellPreparedEventArgs e)
    {
        string comandoSql = string.Format(@"SELECT dbo.f_DiaRdoLancado('{1:dd/MM/yyyy}', {0})"
            , codigoProjeto, e.Date);
        bool lancado = (bool)cDados.getDataSet(comandoSql).Tables[0].Rows[0][0];
        if (lancado)
        {
            e.Cell.Font.Bold = true;
            e.Cell.ForeColor = Color.Blue;
        }
        else
        {
            e.Cell.Font.Bold = false;
            e.Cell.ForeColor = Color.Black;
        }
        //if (e.IsWeekend)
        //    e.Cell.BackColor = Color.FromName("#EBEBEB");
        //else
        //{
        //    if (e.Date.Day % 2 == 0)
        //    {
        //        e.Cell.BackColor = Color.LightGreen;
        //    }
        //    else
        //    {
        //        e.Cell.BackColor = Color.LightYellow;
        //    }
        //}
    }
    protected void gvCabecalho_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (e.Parameters != "")
        //{
        //    DateTime dtCampo = new DateTime();
        //    bool converteu = DateTime.TryParseExact(e.Parameters, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

        //    if (converteu)
        //    {
        //        dte.Date = dtCampo;
        //        setCodigoRDO();
        //        carregaDadosRDO();
        //    }
        //}
    }
    protected void deData_ValueChanged(object sender, EventArgs e)
    {
        definePoliticasDeAcessoComPrazo();
        callbackSalvarCabecalho.JSProperties["cp_UrlPDFRDO"] = cDados.getPathSistema() + "_Projetos/Relatorios/rel_RDO.aspx?CodigoRdo=" + codigoRDO + "&CodigoProjeto=" + codigoProjeto + "&DataRdo=" + dte.Text;
    }
}