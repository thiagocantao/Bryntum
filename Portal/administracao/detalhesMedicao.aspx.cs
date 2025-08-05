using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using System.Runtime.Serialization;
using DevExpress.Utils;

public partial class detalhesMedicao : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    private int codigoMedicao = -1;
    private bool podeEditar = true;
    private string codigoContrato = "NULL";
    public string valorGlosa, valorAdiantamento, valorFaturamento;
    public string titulo = "";
    DataSet ds;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
            desabilitaCampos();

        if (Request.QueryString["CodigoMedicao"] != null && Request.QueryString["CodigoMedicao"].ToString() != "")
        {
            codigoMedicao = int.Parse(Request.QueryString["CodigoMedicao"].ToString());
        }

        setCodigoContrato();

        btnImprimir.JSProperties["cp_CodigoContrato"] = codigoContrato;
        btnImprimir.JSProperties["cp_CodigoMedicao"] = codigoMedicao;
        
        if (!gvValores.IsCallback)
            ds = cDados.getListaItensMedicao(codigoMedicao, "");

        if(!IsPostBack)
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            carregaGvMedicao();
            carregaGridValores();

        }
        else if (gvMedicao.IsCallback)
            carregaGvMedicao();

        gvValores.Settings.ShowFilterRow = false;
        gvValores.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvValores.SettingsBehavior.AllowDragDrop = false;
        gvValores.SettingsBehavior.AllowSort = false;

        gvMedicao.Settings.ShowFilterRow = false;
        gvMedicao.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvMedicao.SettingsBehavior.AllowDragDrop = false;
        gvMedicao.SettingsBehavior.AllowSort = false;

        //gvMedicao.Settings.ShowFilterRow = false;
        ////chamar de dentro da função carregagvmedicao
        //gvMedicao.SettingsPager.PageSize = 50;
        //gvMedicao.SettingsPager.PageSizeItemSettings.ShowAllItem = true;
        //gvMedicao.SettingsPager.PageSizeItemSettings.Visible = true;
       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        this.TH(this.TS("_Strings"));
    }

    private void setCodigoContrato()
    {
        string comandoSQL = string.Format(@"
     SELECT m.CodigoContrato, m.ComentarioMedicao, m.CodigoStatusMedicao, c.NumeroContrato,
       CASE WHEN m.MesMedicao = 1 THEN 'Jan/'
            WHEN m.MesMedicao = 2 THEN 'Fev/'
            WHEN m.MesMedicao = 3 THEN 'Mar/'
            WHEN m.MesMedicao = 4 THEN 'Abr/'
            WHEN m.MesMedicao = 5 THEN 'Mai/'
            WHEN m.MesMedicao = 6 THEN 'Jun/'
            WHEN m.MesMedicao = 7 THEN 'Jul/'
            WHEN m.MesMedicao = 8 THEN 'Ago/'
            WHEN m.MesMedicao = 9 THEN 'Set/'
            WHEN m.MesMedicao = 10 THEN 'Out/'
            WHEN m.MesMedicao = 11 THEN 'Nov/'
            WHEN m.MesMedicao = 12 THEN 'Dez/'
       END + CAST(m.AnoMedicao AS VARCHAR) AS MesAno, MesMedicao, AnoMedicao
     FROM {0}.{1}.Medicao  m INNER JOIN
          {0}.{1}.Contrato c ON ( c.CodigoContrato = m.CodigoContrato 
                                      and c.tipoPessoa = 'F' )  LEFT JOIN
		{0}.{1}.Pessoa AS p ON (p.CodigoPessoa = c.CodigoPessoaContratada)  LEFT JOIN 
        {0}.{1}.[PessoaEntidade] AS [pe] ON (
			pe.[CodigoPessoa] = c.[CodigoPessoaContratada]
			AND pe.codigoEntidade = c.codigoEntidade
            --AND pe.IndicaFornecedor = 'S'
			)  
    WHERE m.CodigoMedicao = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoMedicao);

        DataSet dsContrato = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(dsContrato) && cDados.DataTableOk(dsContrato.Tables[0]))
        {
            codigoContrato = dsContrato.Tables[0].Rows[0]["CodigoContrato"].ToString();

            if (!IsPostBack)
                txtComentarioGeral.Text = dsContrato.Tables[0].Rows[0]["ComentarioMedicao"].ToString();

            string codigoStatusMedicao = dsContrato.Tables[0].Rows[0]["CodigoStatusMedicao"].ToString();
            
            if (codigoStatusMedicao != "1" && codigoStatusMedicao != "2")
                desabilitaCampos();

            rdValores.JSProperties["cp_Titulo"] = "Medição - " + dsContrato.Tables[0].Rows[0]["MesAno"].ToString() + " - " + dsContrato.Tables[0].Rows[0]["NumeroContrato"].ToString();
        }
    }

    private void desabilitaCampos()
    {
        podeEditar = false;
        btnRemeter.ClientVisible = false;
        txtComentarioGeral.ClientEnabled = false;
        txtDescricao.ClientEnabled = false;
        mmObservacoes.ClientEnabled = false;
        gvMedicao.JSProperties["cp_RO"] = "S";

    }

    private void carregaGvMedicao()
    {

        if (cDados.DataSetOk(ds))
        {
            gvMedicao.DataSource = ds;
            gvMedicao.DataBind();
            
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = (altura - 450);

        gvMedicao.Settings.VerticalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;

        gvMedicao.Settings.VerticalScrollableHeight = alturaPrincipal;
    }

    protected void gvMedicao_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        string podeEditarCampo = gvMedicao.GetRowValues(e.VisibleIndex, "RegistroEditavel") + "";
                
        if (podeEditarCampo == "N")
        {
            e.Cell.BackColor = Color.FromName("#EBEBEB");
            e.Cell.Font.Bold = true;
        }
        else if (e.DataColumn.FieldName == "DescricaoItem")
        {
            e.Cell.Style.Add("padding-left", "20px");
        }
        
        if (e.DataColumn.FieldName == "QuantidadeMedidaMes")
        {
            ASPxSpinEdit spin = gvMedicao.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtQuantidadeMedidaMes") as ASPxSpinEdit;

            spin.ClientInstanceName = "txtQuantidadeMedidaMes_" + e.VisibleIndex;

            spin.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            spin.ReadOnlyStyle.ForeColor = Color.Black;            

            if (podeEditarCampo == "N" || podeEditar == false)
            {
                spin.ReadOnly = true;
                e.Cell.Style.Add("background-color", "#EBEBEB");
                spin.Font.Bold = true;
                spin.FocusedStyle.BackColor = Color.FromName("#EBEBEB");
                return;
            }

            string custoUnitario = (gvMedicao.GetRowValues(e.VisibleIndex, "ValorUnitarioItem") + "").Replace(",", ".");

            string comandoJS = string.Format(" executaCalculoCampo(s, {0}, txtQuantidadeMedidaAteMes_{1}, txtValorMedidoMes_{1}, txtValorTotalAteMes_{1}); "
                , (custoUnitario == "" ? "0" : custoUnitario)
               , e.VisibleIndex);

            string quantidadePrevista = gvMedicao.GetRowValues(e.VisibleIndex, "QuantidadePrevistaTotal").ToString();
            string quantidadeAcumulada = gvMedicao.GetRowValues(e.VisibleIndex, "QuantidadeMedidaAteMes").ToString();

            quantidadePrevista = (quantidadePrevista == "" ? "0" : quantidadePrevista.Replace(",", "."));
            quantidadeAcumulada = (quantidadeAcumulada == "" ? "0" : quantidadeAcumulada.Replace(",", "."));

            comandoJS += @" 
                            if(s.GetValue() > (" + quantidadePrevista + " - " + quantidadeAcumulada + ")) { e.isValid = false; e.errorText = 'O valor máximo permitido é ' + (" + quantidadePrevista + " - " + quantidadeAcumulada + "); podeSalvar = false;} else{podeSalvar = true;}";

            spin.ClientSideEvents.Validation = @"function(s, e) { 
                                                                " + comandoJS + " }";

            spin.ClientSideEvents.Validation = @"function(s, e) { " + comandoJS + " }";

            spin.ClientSideEvents.GotFocus = @"function(s, e) { valorAtual = s.GetValue() == null ? 0 : s.GetValue(); s.SetValue(s.GetValue() == 0 ? null : s.GetValue()); document.getElementById('" + e.Cell.ClientID + @"').style.backgroundColor = '#CCFFCC'; 
                                                               }";

            spin.ClientSideEvents.LostFocus = @"function(s, e) { s.SetValue(s.GetValue() == null ? 0 : s.GetValue()); document.getElementById('" + e.Cell.ClientID + "').style.backgroundColor = '#E1EAFF';}";

            string comandoFocus = "";

            comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 38)
                                                      navegaSetas('C');", (e.VisibleIndex - 1));

            comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 40)
                                                        navegaSetas('B');", (e.VisibleIndex - 1)
                                                                                        , e.VisibleIndex + 1);

            spin.ClientSideEvents.KeyDown = @"function(s, e) { " + comandoFocus + " }";
        }
        else if (e.DataColumn.FieldName == "QuantidadeMedidaAteMes")
        {
            ASPxSpinEdit spin = gvMedicao.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtQuantidadeMedidaAteMes") as ASPxSpinEdit;

            spin.ClientInstanceName = "txtQuantidadeMedidaAteMes_" + e.VisibleIndex;

            if (podeEditarCampo == "N")
            {
                spin.BackColor = Color.FromName("#EBEBEB");
                spin.ForeColor = Color.Black;     
                spin.Font.Bold = true;
            }
        }
        else if (e.DataColumn.FieldName == "ValorMedidoMes")
        {
            ASPxSpinEdit spin = gvMedicao.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtValorMedidoMes") as ASPxSpinEdit;

            spin.ClientInstanceName = "txtValorMedidoMes_" + e.VisibleIndex;

            if (podeEditarCampo == "N")
            {
                spin.BackColor = Color.FromName("#EBEBEB");
                spin.ForeColor = Color.Black;
                spin.Font.Bold = true;
            }
        }
        else if (e.DataColumn.FieldName == "ValorTotalAteMes")
        {
            ASPxSpinEdit spin = gvMedicao.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtValorTotalAteMes") as ASPxSpinEdit;

            spin.ClientInstanceName = "txtValorTotalAteMes_" + e.VisibleIndex;

            if (podeEditarCampo == "N")
            {
                spin.BackColor = Color.FromName("#EBEBEB");
                spin.ForeColor = Color.Black;
                spin.Font.Bold = true;
            }
        }
    }

    public double getSomaColuna(string nomeColuna)
    {
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            object sumObject = ds.Tables[0].Compute("Sum(" + nomeColuna + ")", "RegistroEditavel = 'S'");

            double valorRetorno = 0;
            try
            {
                valorRetorno = double.Parse(sumObject.ToString());
            }
            catch { }

            return valorRetorno;
        }
        else
        {
            return 0;
        }
    }
    
    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoUpdate = "";
        if (e.Parameter == "AP")
        {
            comandoUpdate = string.Format(@" UPDATE {0}.{1}.Medicao SET CodigoStatusMedicao = 2
                                               WHERE CodigoMedicao = {2}"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao);

            callbackSalvar.JSProperties["cp_Fechar"] = "S";

        }else if (e.Parameter == "MG")
        {
            comandoUpdate = string.Format(@" UPDATE {0}.{1}.Medicao SET ComentarioMedicao = '{3}'
                                               WHERE CodigoMedicao = {2}"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao
                        , txtComentarioGeral.Text.Replace("'", "''"));
        
        }else if (e.Parameter != "")
        {

            comandoUpdate = string.Format(@" UPDATE {0}.{1}.ItemMedicao SET ComentarioItemMedicao = '{4}'
                                               WHERE CodigoMedicao = {2}
                                                 AND CodigoItemMedicaoContrato = {3}"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao
                        , e.Parameter
                        , mmObservacoes.Text.Replace("'", "''"));
        }

        if (comandoUpdate != "")
        {
            int regAf = 0;

            try
            {
                cDados.execSQL(comandoUpdate, ref regAf);
            }
            catch
            {

            }
        }
    }

    private void carregaGridValores()
    {

        DataSet dsValores = cDados.getValoresAcrescimosRetencoesContrato(codigoMedicao, codigoContrato);

        if (cDados.DataSetOk(dsValores) && cDados.DataTableOk(dsValores.Tables[0]))
        {
            if(IsPostBack)
                ds = cDados.getListaItensMedicao(codigoMedicao, "");

            DataTable dtValores = dsValores.Tables[0];

            object sumObject = ds.Tables[0].Compute("Sum(ValorMedidoMes)", "RegistroEditavel = 'S'");

            double valorTotalMedidoMes = 0, valorMontanteFaturamentoDireto = 0;
            try
            {
                valorTotalMedidoMes = double.Parse(sumObject.ToString());
            }
            catch { }

            double valorAtualCalculo = valorTotalMedidoMes;
            valorMontanteFaturamentoDireto = valorTotalMedidoMes;
            DataTable dtRetornoValores = new DataTable();

            dtRetornoValores.Columns.Add("Descricao");
            dtRetornoValores.Columns.Add("Aliquota");
            dtRetornoValores.Columns.Add("Valor");
            dtRetornoValores.Columns.Add("ValorResultante");

            foreach (DataRow dr in dtValores.Rows)
            {
                string codigoAcessorio = dr["CodigoAcessorioContrato"].ToString();
                string descricao = dr["DescricaoAcessorio"].ToString();
                string tipo = dr["Tipo"].ToString();
                string incideSobreQualValor = dr["IncideSobreQualValor"].ToString();
                double aliquota = 0;
                double valor = 0;

                DataRow novaLinha = dtRetornoValores.NewRow();

                if (dr["Aliquota"].ToString() != "")
                {
                    aliquota = dr["Aliquota"].ToString() == "" ? 0 : double.Parse(dr["Aliquota"].ToString());
                    novaLinha["Aliquota"] = string.Format("{0:n4}", aliquota);
                    
                    if(tipo.Trim() != "")
                        valor = incideSobreQualValor != "VM" ? (valorMontanteFaturamentoDireto * aliquota / 100) : (valorTotalMedidoMes * aliquota / 100);
                    else
                        valor = dr["Valor"].ToString() == "" ? 0 : double.Parse(dr["Valor"].ToString());
                }
                else
                {
                    valor = dr["Valor"].ToString() == "" ? 0 : double.Parse(dr["Valor"].ToString());
                    novaLinha["Aliquota"] = "";                                      
                } 

                if (tipo.Trim() == "Desconto")
                {
                    descricao = descricao + " (-)";
                    valorAtualCalculo -= valor;
                }
                else if (tipo.Trim() != "=")
                {
                    descricao = descricao + " (+)";
                    valorAtualCalculo += valor;
                }

                ASPxSpinEdit txtAdiantamento = rdValores.FindControl("txtAdiantamento") as ASPxSpinEdit;
                ASPxSpinEdit txtFaturamento = rdValores.FindControl("txtFaturamento") as ASPxSpinEdit;
                ASPxSpinEdit txtGlosa = rdValores.FindControl("txtGlosa") as ASPxSpinEdit;

                if (dr["DescricaoAcessorio"].ToString().Trim() == "Faturamento Direto")
                {
                    valorMontanteFaturamentoDireto = valorAtualCalculo;
                    valorFaturamento = valor.ToString();
                }
                else if (dr["DescricaoAcessorio"].ToString().Trim() == "Desconto de Adiantamento")
                {
                    valorAdiantamento = valor.ToString();
                }
                else if (dr["DescricaoAcessorio"].ToString().Trim() == "Glosa")
                {
                    valorGlosa = valor.ToString();
                }

                txtAdiantamento.Text = valorAdiantamento;
                txtFaturamento.Text = valorFaturamento;
                txtGlosa.Text = valorGlosa;

                novaLinha["Descricao"] = descricao;
                novaLinha["Valor"] = string.Format("{0:n2}", valor);
                novaLinha["ValorResultante"] = string.Format("{0:n2}", tipo.Trim() == "=" ? valor : valorAtualCalculo);

                string comandoSQL = string.Format(@"IF EXISTS (SELECT 1 FROM {0}.{1}.RetornoValoresAcessoriosMedicao WHERE CodigoMedicao = {2} AND CodigoAcessorioContrato = {3})
                                                    BEGIN
                                                        UPDATE RetornoValoresAcessoriosMedicao SET ValorAcessorio = {4} WHERE CodigoMedicao = {2} AND CodigoAcessorioContrato = {3}
                                                    END ELSE
                                                        INSERT INTO RetornoValoresAcessoriosMedicao(CodigoMedicao, CodigoAcessorioContrato, ValorAcessorio)
                                                                                             VALUES({2}, {3}, {4})
                                                        ", cDados.getDbName(), cDados.getDbOwner(), codigoMedicao, codigoAcessorio, valor.ToString().Replace(",", "."));

                int regAf = 0;

                cDados.execSQL(comandoSQL, ref regAf);

                dtRetornoValores.Rows.Add(novaLinha);
            }

            gvValores.DataSource = dtRetornoValores;
            gvValores.DataBind();

        }
    }

    protected void gvValores_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string comandoUpdate = "";

        string valorReajuste = "null";

        DevExpress.Web.ASPxGridView gv = (DevExpress.Web.ASPxGridView)sender;        

        if (e.Parameters == "")
        {
            for (int i = 0; i < gvMedicao.VisibleRowCount; i++)
            {
                ASPxSpinEdit spin = gvMedicao.FindRowCellTemplateControl(i, null, "txtQuantidadeMedidaMes") as ASPxSpinEdit;

                if (spin != null && spin.IsValid)
                {
                    comandoUpdate += string.Format(" UPDATE {0}.{1}.ItemMedicao SET QuantidadeMedidaMes = {4} WHERE CodigoMedicao = {2} AND CodigoItemMedicaoContrato = {3} \n"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao
                        , gvMedicao.GetRowValues(i, gvMedicao.KeyFieldName)
                        , spin.Value == null ? "0" : spin.Value.ToString().Replace(",", "."));
                }
            }

            if (comandoUpdate != "")
            {
                int regAf = 0;

                try
                {
                    cDados.execSQL(comandoUpdate, ref regAf);
                }
                catch
                {

                }
            }

            //carregaGvMedicao();
            carregaGridValores();

            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                object[] rowValues = (object[])gv.GetRowValues(i, "Descricao", "Valor");
                string descricao = (rowValues[0] as string) ?? string.Empty;
                if (descricao.StartsWith("Reajuste"))
                {
                    valorReajuste = (rowValues[1] as string) ?? "null";
                    break;
                }
            }

            comandoUpdate = string.Format(@" UPDATE {0}.{1}.Medicao SET ValorReajuste = {3}
                                               WHERE CodigoMedicao = {2}"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao
                        , valorReajuste.Replace(".", "").Replace(',', '.'));


            if (comandoUpdate != "")
            {
                int regAf = 0;

                try
                {
                    cDados.execSQL(comandoUpdate, ref regAf);
                }
                catch
                {

                }
            }
        }
        else
        {
            ASPxSpinEdit txtAdiantamento = rdValores.FindControl("txtAdiantamento") as ASPxSpinEdit;
            ASPxSpinEdit txtFaturamento = rdValores.FindControl("txtFaturamento") as ASPxSpinEdit;
            ASPxSpinEdit txtGlosa = rdValores.FindControl("txtGlosa") as ASPxSpinEdit;

            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                object[] rowValues = (object[])gv.GetRowValues(i, "Descricao", "Valor");
                string descricao = (rowValues[0] as string) ?? string.Empty;
                if (descricao.StartsWith("Reajuste"))
                {
                    valorReajuste = (rowValues[1] as string) ?? "null";
                    break;
                }
            }

            comandoUpdate = string.Format(@" UPDATE {0}.{1}.Medicao SET ValorAdiantamento = {3}
                                                                        ,ValorFaturamentoDireto = {4}
                                                                        ,ValorGlosa = {5}
                                                                        ,ValorReajuste = {6}
                                               WHERE CodigoMedicao = {2}"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoMedicao
                        , e.Parameters == "AD" ? (txtAdiantamento.Value == null ? "0" : txtAdiantamento.Value.ToString().Replace(",", ".")) : "ValorAdiantamento"
                        , e.Parameters == "FA" ? (txtFaturamento.Value == null ? "0" : txtFaturamento.Value.ToString().Replace(",", ".")) : "ValorFaturamentoDireto"
                        , e.Parameters == "GL" ? (txtGlosa.Value == null ? "0" : txtGlosa.Value.ToString().Replace(",", ".")) : "ValorGlosa"
                        , valorReajuste.Replace(".","").Replace(',','.'));

            if (comandoUpdate != "")
            {
                int regAf = 0;

                try
                {
                    cDados.execSQL(comandoUpdate, ref regAf);
                }
                catch
                {

                }
            }
        }
        
        carregaGvMedicao();
        carregaGridValores();
    }

    protected void gvMedicao_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnComentarios")
        {
            string podeEditarCampo = gvMedicao.GetRowValues(e.VisibleIndex, "RegistroEditavel") + "";

            if (podeEditarCampo == "N")
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }            
        }
    }

}