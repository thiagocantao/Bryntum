using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page {

    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;
    private int tipoAssociacaoParametro;
    public string definicaoEntidade = Resources.traducao.entidade;
    int codigoObjetoAssociado = -1;
    string iniciaisAssociacao = "EN";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object s, EventArgs e) {

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PainelMensagens.js""></script>"));
        this.TH(this.TS("PainelMensagens"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));

        tipoAssociacaoParametro = cDados.getCodigoTipoAssociacao("MG");

        if(!IsPostBack) {
            
            hfGeral.Set("CodigoPastaSelecionada", "E");
        }

        MailGrid.JSProperties["cp_NaoLidas"] = "";

        if (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "")
            iniciaisAssociacao = Request.QueryString["TA"].ToString();

        if (Request.QueryString["CO"] != null && Request.QueryString["CO"].ToString() != "")
        {
            codigoObjetoAssociado = int.Parse(Request.QueryString["CO"].ToString());

            if (!IsPostBack)
                CkNaoLidos().Checked = iniciaisAssociacao == "CO";

            CkNaoLidos().ClientVisible = false;
            MailGrid.Columns["TipoAssociacao"].Visible = false;
            MailGrid.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
        }

        


        carregaComboCategorias();
        carregaGrid();

        if (!IsPostBack)
        {
            FocusFirstMessageRow();
        }

        //btnAltaPrioridade.Attributes.Add("onclick", "verificaMarcacaoAlta()");
        //btnBaixaPrioridade.Attributes.Add("onclick", "verificaMarcacaoBaixa()");

        DataSet dsEntidade = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(dsEntidade) && cDados.DataTableOk(dsEntidade.Tables[0]))
        {
            definicaoEntidade = dsEntidade.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        //Pega a Resolução do cliente
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["EsconderBotaoNovo"] != null && Request.QueryString["EsconderBotaoNovo"].ToString() == "S")
        {
            MailMenu.Items.FindByName("compose").ClientVisible = false;
            MailGrid.Settings.VerticalScrollableHeight = alturaTela - 405;
        }
        else
        {

            MailGrid.Settings.VerticalScrollableHeight = alturaTela - 405;
        }

        if (Request.QueryString["MostrarTitulo"] == null || Request.QueryString["MOstrarTitulo"] + "" == "N")
            tbTitulo.Style.Add("display", "none");

        MailGrid.Settings.ShowFilterRow = false;
        MailGrid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }
    
    protected void MailPanel_Callback(object sender, CallbackEventArgsBase e) {

        string prioridade = "NULL";

        if (btnAltaPrioridade.Checked)
            prioridade = "'A'";
       // else if (btnBaixaPrioridade.Checked)
       //     prioridade = "'B'";

        string assunto = txtAssunto.Text;

        if (e.Parameter == "compose" || e.Parameter == "fwd")
        {
            if (txtDestinatarios.Text.Trim() != "")
            {
                string emails = "";

                foreach (Match item in Regex.Matches(txtDestinatarios.Text.Trim(), @"< [^>]* >"))
                {                    
                    emails += "'" + item.Value + "',";
                }

                DataSet dsUsuarios = cDados.getUsuarios(" AND EMail IN(" + emails.Replace("< ", "").Replace(" >", "") + "'NI')");

                if (cDados.DataSetOk(dsUsuarios) && cDados.DataTableOk(dsUsuarios.Tables[0]))
                { 
                    int[] arrayUsuarios = new int[dsUsuarios.Tables[0].Rows.Count];

                    for (int i = 0; i < arrayUsuarios.Length; i++)
                        arrayUsuarios[i] = int.Parse(dsUsuarios.Tables[0].Rows[i]["CodigoUsuario"].ToString());

                    string msg = "";

                    bool retorno = cDados.incluiMensagem(codigoEntidadeUsuarioResponsavel, codigoObjetoAssociado == -1 ? codigoEntidadeUsuarioResponsavel : codigoObjetoAssociado, codigoUsuarioResponsavel, assunto, MailEditor.Html, DateTime.MinValue, false, prioridade, int.Parse(ddlCategoria.Value.ToString()), arrayUsuarios, iniciaisAssociacao, ref msg);
                }
            }
        }
        else if (e.Parameter.Contains("reply"))
        {
            string codigoMensagem = MailGrid.GetRowValues(MailGrid.FocusedRowIndex, MailGrid.KeyFieldName).ToString();

            string msg = "";

            string arrayUsuarios = hfDestinatarios.Get("Destinatarios").ToString();
            bool retorno = false;

            if(arrayUsuarios != "")
                retorno = cDados.incluiResposta(int.Parse(codigoMensagem), codigoUsuarioResponsavel, assunto, MailEditor.Html, prioridade, int.Parse(ddlCategoria.Value.ToString()), arrayUsuarios.Split(';'), ref msg);
        }

        carregaGrid();

        MailGrid.ExpandAll();
        FocusFirstMessageRow();
    }
    
    protected void MailEditorPopup_WindowCallback(object sender, PopupWindowCallbackArgs e) {
        var html = "";

        var key = Convert.ToInt32(e.Parameter);
        if (key > -1)
        {
    

             string format = "<br><br><br>"
                + "----- " +  Resources.traducao.MensagensPainelBordo_mensagem_original +  " -----<br>"
                + Resources.traducao.MensagensPainelBordo_assunto + ": {0}<br>"
                + Resources.traducao.MensagensPainelBordo_de+ ": {1}<br>"
                + Resources.traducao.MensagensPainelBordo_data + ": {2:g}<br>{3}";

            html += FormatMessageCore(format);


            if (hfDestinatarios.Get("Tipo").ToString() != "fwd")
            {
                txtAssunto.ClientEnabled = false;
                txtAssunto.Text = FormatMessageCore("Res: {0}");
                string codigoCategoria = FormatMessageCore("{5}");
                ddlCategoria.Value = codigoCategoria == "" ? "-1" : codigoCategoria;
                MailEditorPopup.Height = 320;
                carregaDestinatariosMensagem(key);
            }
            else
            {
                txtAssunto.ClientEnabled = false;
                txtAssunto.Text = FormatMessageCore("Enc: {0}");
                string codigoCategoria = FormatMessageCore("{5}");
                ddlCategoria.Value = codigoCategoria == "" ? "-1" : codigoCategoria;
            }
        }

        MailEditor.Html = html;
    }

    private void carregaDestinatariosMensagem(int codigoMensagem)
    {
        DataSet dsDest = cDados.getDestinatariosMensagens(codigoMensagem, "");

        string codigosDestinatarios = "";
        string usuariosDestinatarios = "";

        if (hfDestinatarios.Get("Tipo").ToString() == "replyAll")
        {
            if (cDados.DataSetOk(dsDest) && cDados.DataTableOk(dsDest.Tables[0]))
            {
                foreach (DataRow dr in dsDest.Tables[0].Rows)
                {
                    string codigoUsuario = dr["CodigoUsuario"].ToString();
                    string nomeUsuario = dr["NomeUsuario"].ToString();
                    string email = dr["EMail"].ToString();

                    codigosDestinatarios += codigoUsuario + ";";
                    if (!usuariosDestinatarios.Contains("'< " + email + " >;"))
                        usuariosDestinatarios += "'" + nomeUsuario + "'< " + email + " >; ";
                }
            }
        }
        else if (hfDestinatarios.Get("Tipo").ToString() == "reply")
        {
            DataRow[] drs = dsDest.Tables[0].Select("IndicaResponsavel = 'S'");

            if (drs.Length > 0)
            {
                string codigoUsuario = drs[0]["CodigoUsuario"].ToString();
                string nomeUsuario = drs[0]["NomeUsuario"].ToString();
                string email = drs[0]["EMail"].ToString();
                codigosDestinatarios += codigoUsuario + ";";
                if(!usuariosDestinatarios.Contains("'< " + email + " >;"))
                    usuariosDestinatarios += "'" + nomeUsuario + "'< " + email + " >;";
            }
        }

        hfDestinatarios.Set("Destinatarios", codigosDestinatarios);
        txtDestinatarios.Text = usuariosDestinatarios;
    }
    
    protected void MailGrid_CustomGroupDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e) {
        if(e.Column.FieldName == "Assunto")
            e.DisplayText = HttpUtility.HtmlEncode(e.Value);
    }

    protected string FormatCurrentMessage()
    {

        if (MailGrid.IsGroupRow(MailGrid.FocusedRowIndex) || MailGrid.FocusedRowIndex == -1)
            return "";

        string tipoMensagem = MailGrid.GetRowValues(MailGrid.FocusedRowIndex, "IndicaTipoMensagem") + "";


        int codigoMensagem = int.Parse(MailGrid.GetRowValues(MailGrid.FocusedRowIndex, "CodigoMensagem") + "");
        DataSet dsDest = cDados.getDestinatariosMensagens(codigoMensagem, "");

        string usuariosDestinatarios = "";

        if (cDados.DataSetOk(dsDest) && cDados.DataTableOk(dsDest.Tables[0]))
        {
            DataRow[] drs = dsDest.Tables[0].Select("IndicaResponsavel = 'N'");

            foreach (DataRow dr in drs)
            {
                string nomeUsuario = dr["NomeUsuario"].ToString();
                string email = dr["EMail"].ToString();

                usuariosDestinatarios += "'" + nomeUsuario + "'< " + email + " >; ";
            }
        }




        if (tipoMensagem == "S")
        {
            return FormatMessageCore(getDescricaoObjeto() + "<br><h3>{0}</h3><div>Enviada em: {2:g}</div><div>Para: " + (usuariosDestinatarios) + "</div>" + "<hr />{3}");
        }

        return FormatMessageCore(getDescricaoObjeto() + "<br><h3>{0}</h3><div>Enviada em: {2:g}</div><div>De: {1}</div><div>Para: " + (usuariosDestinatarios) + "</div>" + "<hr />{3}");
    }

    private string getDescricaoObjeto()
    {

        var index = MailGrid.FocusedRowIndex;
        string descricao = "";

        string codigoObjeto = MailGrid.GetRowValues(index, "CodigoObjetoAssociado").ToString();
        string nomeObjeto = MailGrid.GetRowValues(index, "NomeObjeto").ToString();
        string tipoAssociacao = MailGrid.GetRowValues(index, "IniciaisTipoAssociacao").ToString();

        if (tipoAssociacao == "PR")
        {
            descricao = string.Format("<a style='font-size:10pt;font-weight:bold' href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?NivelNavegacao=1&IDProjeto={0}&NomeProjeto={1}' target='_top'>{1}</a>", codigoObjeto, nomeObjeto);
        }            
        else
        {
            descricao = "<a style='font-size:10pt;font-weight:bold'>" + nomeObjeto + "</a>";
        }
        //else if (tipoAssociacao == "IN")
        //{
        //    descricao = string.Format("<a style='font-size:10pt;' href='../_Estrategias/indicador/index.aspx?NivelNavegacao=2&COIN={0}' target='_top'>{1}</a>", codigoObjeto, nomeObjeto);
        //}
        //else if (tipoAssociacao == "OB")
        //{
        //    descricao = string.Format("<a style='font-size:10pt;' href='../_Estrategias/objetivoestrategico/indexResumoObjetivo.aspx?COE={0}&UNM={2}' target='_top'>{1}</a>", codigoObjeto, nomeObjeto, codigoEntidadeUsuarioResponsavel);
        //}
        //else
        //{
        //    descricao = "<a style='font-size:10pt;font-weight:bold'>" + nomeObjeto + "</a>";
        //}


        return descricao;
    }

    string FormatMessageCore(string format)
    {
        var index = MailGrid.FocusedRowIndex;
        if (index > -1)
        {
            return String.Format(format, MailGrid.GetRowValues(index, "Assunto"), MailGrid.GetRowValues(index, "NomeUsuario"), MailGrid.GetRowValues(index, "DataInclusao"), MailGrid.GetRowValues(index, "Mensagem"), MailGrid.GetRowValues(index, "NomeObjeto"), MailGrid.GetRowValues(index, "CodigoCategoria"));
        }
        else
            return "";
        
    }

    private void carregaGrid()
    {
        DataSet ds1 = new DataSet();
                
        string where = "";

        MailGrid.JSProperties["cp_TipoPastaSelecionada"] = "E";

        
        int codigoUsuarioMensagem = codigoUsuarioResponsavel;

        if (codigoObjetoAssociado != -1 && iniciaisAssociacao != "CO")
        {
            if (CkNaoLidos().Checked)
                where = " AND md.DataLeitura IS NULL";

            ds1 = getMensagensObjeto(codigoEntidadeUsuarioResponsavel, codigoObjetoAssociado, iniciaisAssociacao, "");
        }
        else
        {
            if (CkNaoLidos().Checked)
                where = " AND md.DataLeitura IS NULL AND cm.IndicaTipoMensagem = 'E' AND cm.CodigoPasta IS NULL AND m.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;
            else
                where = "AND cm.IndicaTipoMensagem = 'E' AND cm.CodigoPasta IS NULL AND m.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;

            if(codigoObjetoAssociado != -1 && iniciaisAssociacao == "CO")
            {
                where += " AND m.CodigoObjetoAssociado = " + codigoObjetoAssociado + " AND m.CodigoTipoAssociacao = (SELECT [CodigoTipoAssociacao] FROM [TipoAssociacao] WHERE [IniciaisTipoAssociacao] = 'CO')";
            }

            ds1 = cDados.getItensPainelMensagensUsuario(codigoUsuarioMensagem, "E", where);
        }
        var searchText = FindSearchBox().Text;
        

        if (!String.IsNullOrEmpty(searchText))
        {
            string comandoSQL = string.Format(@"  SELECT md.CodigoMensagem 
	                                                    FROM {0}.{1}.MensagemDestinatario md INNER JOIN 
			                                                 {0}.{1}.Usuario u ON u.CodigoUsuario = md.CodigoDestinatario
                                                     WHERE u.NomeUsuario LIKE '%{2}%'
                                                        OR u.EMail LIKE '%{2}%'", cDados.getDbName(), cDados.getDbOwner(), searchText);

            DataSet dsFind = cDados.getDataSet(comandoSQL);

            string codigosMensagens = "-1";

            if (cDados.DataSetOk(dsFind) && cDados.DataTableOk(dsFind.Tables[0]))
            {
                foreach (DataRow dr in dsFind.Tables[0].Rows)
                {
                    codigosMensagens += "," + dr["CodigoMensagem"];
                }
            }

            DataRow[] drs = ds1.Tables[0].Select("Mensagem Like '%" + searchText + "%' OR Assunto Like '%" + searchText + "%'" + " OR NomeUsuario Like '%" + searchText + "%' OR CodigoMensagem IN(" + codigosMensagens + ") OR DescricaoCategoria Like '%" + searchText + "%' OR NomeObjeto Like '%" + searchText + "%'" + "");

            DataTable dt = ds1.Tables[0].Clone();

            foreach (DataRow dr in drs)
                dt.ImportRow(dr);

            MailGrid.DataSource = dt;
        }
        else
        {
            MailGrid.DataSource = ds1;
        }

        MailGrid.DataBind();

        MailGrid.Columns["NomeUsuario"].Visible = true;
    }

    public DataSet getMensagensObjeto(int codigoEntidade, int codigoObjetoAssociado, string iniciaisAssociacao, string where)
    {
        string comandoSQL = "";

        comandoSQL = string.Format(@"
            BEGIN
	            DECLARE @codigoTipoAssociacao Int

                SELECT  @codigoTipoAssociacao =  CodigoTipoAssociacao 
                FROM    {0}.{1}.TipoAssociacao 
                WHERE   IniciaisTipoAssociacao = 'MG'
            			
	            SELECT m.CodigoMensagem, m.Mensagem, m.DataInclusao, m.DataLimiteResposta, u.NomeUsuario, 'E' AS IndicaTipoMensagem
                     , m.Assunto, m.Mensagem, 
                      ISNULL((SELECT CASE WHEN md.DataLeitura IS NULL THEN 'N' ELSE 'S' END 
                         FROM {0}.{1}.MensagemDestinatario md 
                        WHERE md.CodigoMensagem = m.CodigoMensagem
                          AND md.CodigoDestinatario = {6}), 'S') AS Lida, m.Prioridade
                     , cat.DescricaoCategoria, m.CodigoObjetoAssociado, ta.IniciaisTipoAssociacao, m.CodigoCategoria
                     , {0}.{1}.f_GetDescricaoOrigemAssociacaoObjeto(m.CodigoEntidade, m.CodigoTipoAssociacao,null, m.CodigoObjetoAssociado,0,null) AS NomeObjeto
                     , CASE WHEN ta.IniciaisTipoAssociacao = 'DC' OR ta.IniciaisTipoAssociacao = 'DS' OR ta.IniciaisTipoAssociacao = 'PC' THEN 'PR' ELSE ta.IniciaisTipoAssociacao END AS TipoAssociacao
	              FROM {0}.{1}.Mensagem m INNER JOIN
		               {0}.{1}.Usuario u ON u.CodigoUsuario = m.CodigoUsuarioInclusao INNER JOIN
	                   {0}.{1}.TipoAssociacao ta ON ta.CodigoTipoAssociacao = m.CodigoTipoAssociacao LEFT JOIN
	                   {0}.{1}.CategoriaMensagem cat ON cat.CodigoCategoria = m.CodigoCategoria
	             WHERE m.CodigoEntidade = {2} 
                   AND ta.IniciaisTipoAssociacao = '{3}' 
                   AND m.CodigoObjetoAssociado = {4}    
                   {5}   	
            END
            ", cDados.getDbName()
             , cDados.getDbOwner()
             , codigoEntidade
             , iniciaisAssociacao
             , codigoObjetoAssociado
             , where
             , cDados.getInfoSistema("IDUsuarioLogado"));

        return cDados.getDataSet(comandoSQL);
    }
    void FocusFirstMessageRow() {
        var index = FindFirstMessageRowIndex();
        if (index > -1)
        {
            MailGrid.FocusedRowIndex = index;            
        }
    }

    int FindFirstMessageRowIndex() {
        for(var i = 0; i < MailGrid.VisibleRowCount; i++) {
            if(!MailGrid.IsGroupRow(i))
                return i;
        }
        return -1;
    }

    ASPxButtonEdit FindSearchBox()
    {
        return MailMenu.Items.FindByName("SearchBoxItem").FindControl("SearchBox") as ASPxButtonEdit;
    }

    ASPxCheckBox CkNaoLidos()
    {
        return MailMenu.Items.FindByName("ckMostrarApenasNaoLidosItem").FindControl("ckMostrarApenasNaoLidos") as ASPxCheckBox;
    }
    
    protected void MailGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {        
        if (e.Parameters.IndexOf("LT") > -1)
        {
            try
            {
                int codigoMensagem = int.Parse(e.Parameters.Replace("LT", ""));

                cDados.atualizaMensagemLida(codigoMensagem, codigoMensagem, codigoUsuarioResponsavel);

                carregaGrid();

                MailGrid.FocusedRowIndex = MailGrid.FindVisibleIndexByKeyValue(codigoMensagem.ToString());
                MailGrid.ExpandAll();

                MailGrid.JSProperties["cp_Atualizar"] = "N";
            }
            catch { }
        }
    }    
    
    protected void MailGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Prioridade")
        {
            if (e.CellValue.ToString() == "A")
            {
                e.Cell.Text = "<img alt='Alta Prioridade' src='../imagens/PrioridadeAlta.PNG' />";
                e.Cell.ToolTip = Resources.traducao.MensagensPainelBordo_alta_prioridade;
            }
            else if (e.CellValue.ToString() == "B")
            {
                e.Cell.Text = "<img alt='Baixa Prioridade' src='../imagens/PrioridadeBaixa.PNG' />";
                e.Cell.ToolTip = Resources.traducao.MensagensPainelBordo_baixa_prioridade;
            }
            else
                e.Cell.Text = "&nbsp;";
        }
    }

    private void carregaComboCategorias()
    {
        DataSet dsCategorias = cDados.getCategoriasMensagem(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsCategorias))
        {
            ddlCategoria.DataSource = dsCategorias;
            ddlCategoria.TextField = "DescricaoCategoria";
            ddlCategoria.ValueField = "CodigoCategoria";
            ddlCategoria.DataBind();
        }

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlCategoria.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlCategoria.SelectedIndex = 0;
    }

    protected void MailGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string indicaLida = MailGrid.GetRowValues(e.VisibleIndex, "Lida").ToString();

            if (indicaLida == "N")
            {
                MailGrid.JSProperties["cp_NaoLidas"] += (e.KeyValue.ToString() + ";");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Style.Add("font-weight", "bold");
                }
            }
        }
    }
}
