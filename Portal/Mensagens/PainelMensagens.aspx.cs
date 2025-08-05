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
    public string definicaoEntidade = "Entidade";

    protected void Page_Init(object sender, EventArgs e)
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
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object s, EventArgs e) {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PainelMensagens.js""></script>"));
        this.TH(this.TS("PainelMensagens"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));

        tipoAssociacaoParametro = cDados.getCodigoTipoAssociacao("MG");

        if (!IsPostBack) {
            MailTree.SelectedNode = MailTree.Nodes[0].Nodes.FindByName("E");
            hfGeral.Set("CodigoPastaSelecionada", MailTree.SelectedNode.Name);
        }

        MailGrid.JSProperties["cp_NaoLidas"] = "";

        carregaComboCategorias();
        carregaPastasUsuario();
        carregaGrid();

       
           
        ((GridViewDataColumn)MailGrid.Columns["NomeUsuario"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataColumn)MailGrid.Columns["Assunto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataColumn)MailGrid.Columns["DataInclusao"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataColumn)MailGrid.Columns["DescricaoCategoria"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;




        if (!IsPostBack)
        {
            FocusFirstMessageRow();
        }
        
        MailTree.Nodes[0].Text = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
               
        defineLarguraTela();

        DataSet ds = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ckTodasEntidades.Text = "Mostrar Minhas Mensagens de Todas as " + ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();

        }
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        MailSplitter.Height = new System.Web.UI.WebControls.Unit((altura - 220).ToString() + "px");
        MailSplitter.GetPaneByName("GridPane").Size = ((int)((altura - 220) / 100) * 70);
        MailSplitter.GetPaneByName("MessagePane").Size = ((int)((altura - 220) / 100) * 30);
        MailGrid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        MailGrid.Settings.VerticalScrollableHeight = 500;
        btnAltaPrioridade.Attributes.Add("onclick", "verificaMarcacaoAlta()");
        //btnBaixaPrioridade.Attributes.Add("onclick", "verificaMarcacaoBaixa()");

        DataSet dsEntidade = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(dsEntidade) && cDados.DataTableOk(dsEntidade.Tables[0]))
        {
            definicaoEntidade = dsEntidade.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }
        
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        LayoutSplitter.Height = altura - 120;
    }

    protected void MailPanel_Callback(object sender, CallbackEventArgsBase e) {

        string prioridade = "NULL";

        if (btnAltaPrioridade.Checked)
        { 
            prioridade = "'A'";
        }
        else
        {
            prioridade = "'B'";
        }


        //else if (btnBaixaPrioridade.Checked)
        //    prioridade = "'B'";

        string assunto = txtAssunto.Text;

        if (e.Parameter == "MV")
        {
            string codigoPastaDestino = hfGeral.Get("PastaDestino").ToString();
            string codigoMensagem = hfGeral.Get("CodigoMensagemMover").ToString();
            string indicaTipoMensagem = MailGrid.GetRowValues(MailGrid.FocusedRowIndex, "IndicaTipoMensagem") + "";

            atualizaPastaMensagemUsuario(codigoUsuarioResponsavel, codigoMensagem, codigoPastaDestino, indicaTipoMensagem);
        }
        else if (e.Parameter == "CT")
        {
            string codigoCategoria = hfGeral.Get("Categoria").ToString();
            string codigoMensagem = hfGeral.Get("CodigoMensagemMover").ToString();

            cDados.atualizaCategoriaMensagem(int.Parse(codigoMensagem), int.Parse(codigoCategoria));
        }
        else if (e.Parameter == "compose" || e.Parameter == "fwd")
        {
            if (txtDestinatarios.Text.Trim() != "")
            {
                string emails = "";

                foreach (Match item in Regex.Matches(txtDestinatarios.Text, @"< [^>]* >"))
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

                    bool retorno = cDados.incluiMensagem(codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, assunto, MailEditor.Html, DateTime.MinValue, false, prioridade, int.Parse(ddlCategoria.Value.ToString()), arrayUsuarios, "EN", ref msg);
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

    public bool atualizaPastaMensagemUsuario(int codigoUsuario, string codigoMensagem, string codigoPastaDestino, string indicaTipoMensagem)
    {
        try
        {
            string comandoSQL = "";

            if (codigoPastaDestino == "E" || codigoPastaDestino == "S")
            {
                comandoSQL = string.Format(@"
                                BEGIN
	                            DECLARE @codigoTipoAssociacao Int

                                SELECT  @codigoTipoAssociacao =  CodigoTipoAssociacao 
                                FROM    {0}.{1}.TipoAssociacao 
                                WHERE   IniciaisTipoAssociacao = 'MG'

                                UPDATE {0}.{1}.CaixaMensagem SET CodigoPasta = NULL
                                 WHERE CodigoObjetoAssociado = {2}
                                   AND CodigoUsuario = {3}
                                   AND CodigoTipoAssociacao = @codigoTipoAssociacao
                                   AND IndicaTipoMensagem = '{5}'
                                END
                                ", cDados.getDbName(), cDados.getDbOwner(), codigoMensagem, codigoUsuario, codigoPastaDestino, indicaTipoMensagem);
            }
            else
            {
                comandoSQL = string.Format(@"
                                BEGIN
	                            DECLARE @codigoTipoAssociacao Int

                                SELECT  @codigoTipoAssociacao =  CodigoTipoAssociacao 
                                FROM    {0}.{1}.TipoAssociacao 
                                WHERE   IniciaisTipoAssociacao = 'MG'

                                UPDATE {0}.{1}.CaixaMensagem SET CodigoPasta = {4}
                                 WHERE CodigoObjetoAssociado = {2}
                                   AND CodigoUsuario = {3}
                                   AND CodigoTipoAssociacao = @codigoTipoAssociacao
                                   AND IndicaTipoMensagem = '{5}'
                                END
                                ", cDados.getDbName(), cDados.getDbOwner(), codigoMensagem, codigoUsuario, codigoPastaDestino, indicaTipoMensagem);

            }
            int registrosAfetados = 0;
            cDados.execSQL(comandoSQL, ref registrosAfetados);
            return true;

        }
        catch
        {
            return false;
        }
    }

    protected void MailMessagePanel_Callback(object sender, CallbackEventArgsBase e) {
       
    }

    protected void MailEditorPopup_WindowCallback(object sender, PopupWindowCallbackArgs e) {
        var html = "";

        var key = Convert.ToInt32(e.Parameter);
        if (key > -1)
        {
             string format = "<br><br><br>"
                + "----- " + Resources.traducao.PainelMensagens_mensagem_original + " -----<br>"
                + Resources.traducao.PainelMensagens_assunto + ": {0}<br>"
                + Resources.traducao.PainelMensagens_de + ": {1}<br>"
                + Resources.traducao.PainelMensagens_data + ": {2:g}<br>{3}";

            html += FormatMessageCore(format);


            if (hfDestinatarios.Get("Tipo").ToString() != "fwd")
            {
                txtAssunto.ClientEnabled = false;
                txtAssunto.Text = FormatMessageCore(Resources.traducao.PainelMensagens_res + ": {0}");
                string codigoCategoria = FormatMessageCore("{5}");
                ddlCategoria.Value = codigoCategoria == "" ? "-1" : codigoCategoria;
                MailEditorPopup.Height = 320;
                carregaDestinatariosMensagem(key);
            }
            else
            {
                txtAssunto.ClientEnabled = false;
                txtAssunto.Text = FormatMessageCore(Resources.traducao.PainelMensagens_enc + ": {0}");
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
                if(!usuariosDestinatarios.Contains("'<" + email + ">;"))
                    usuariosDestinatarios += "'" + nomeUsuario + "'< " + email + " >;";
            }
        }

        hfDestinatarios.Set("Destinatarios", codigosDestinatarios);
        txtDestinatarios.Text = usuariosDestinatarios;
    }

    protected void MailGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e) {
        //if(e.Column.FieldName == "Assunto" && (bool)e.GetFieldValue("IsReply"))
        //    e.DisplayText = "Re: " + HttpUtility.HtmlEncode(e.Value);
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
            return FormatMessageCore(getDescricaoObjeto() + "<br><h3>{0}</h3><div>Enviada em: {2:g}</div><div>Para: " + (usuariosDestinatarios) + "</div><hr />{3}");
        }

        return FormatMessageCore(getDescricaoObjeto() + "<br><h3>{0}</h3><div>Enviada em: {2:g}</div><div>De: {1}</div><div>Para: " + (usuariosDestinatarios) + "</div><hr />{3}");
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
        //    descricao = string.Format("<a style='font-size:10pt;' href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?NivelNavegacao=1&IDProjeto={0}&NomeProjeto={1}' target='_top'>{1}</a>", codigoObjeto, nomeObjeto);
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

        string codigoPastaSelecionada = hfGeral.Get("CodigoPastaSelecionada").ToString();
        string tipoPasta = getTipoPasta(codigoPastaSelecionada);
        string where = "";

        MailGrid.JSProperties["cp_TipoPastaSelecionada"] = tipoPasta;

        if (codigoPastaSelecionada != "")
        {
            if (codigoPastaSelecionada == "E" || codigoPastaSelecionada == "S")
                where = " AND cm.IndicaTipoMensagem = '" + codigoPastaSelecionada + "' AND cm.CodigoPasta IS NULL";
            else
                where = " AND cm.CodigoPasta = " + codigoPastaSelecionada;

            if(ckTodasEntidades.Checked == false)
                where += " AND m.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;

            ds1 = cDados.getItensPainelMensagensUsuario(codigoUsuarioResponsavel,tipoPasta, where);

            var searchText = FindSearchBox().Text.Replace("'", "&#39;");
            

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

            

            if (tipoPasta == "S")
                MailGrid.Columns["NomeUsuario"].Visible = false;
            else
                MailGrid.Columns["NomeUsuario"].Visible = true;
        }
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        MailGrid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        MailGrid.Settings.VerticalScrollableHeight = 500;
        MailSplitter.GetPaneByName("GridPane").Size = ((int)((altura - 220) / 100) * 70);
        MailSplitter.GetPaneByName("MessagePane").Size = ((int)((altura - 220) / 100) * 30);
        MailSplitter.Height = new System.Web.UI.WebControls.Unit((altura - 220).ToString() + "px");

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

    ASPxButtonEdit FindSearchBox() {
        return MailMenu.Items.FindByName("SearchBoxItem").FindControl("SearchBox") as ASPxButtonEdit;
    }

    private string getTipoPasta(string codigoPasta)
    {
        if (codigoPasta == "E" || codigoPasta == "S")
            return codigoPasta;
        else if (codigoPasta != "")
        {
            DataSet ds = getPastasMensagens(codigoUsuarioResponsavel, " AND CodigoPasta = " + codigoPasta);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                return ds.Tables[0].Rows[0]["IndicaEntradaSaida"].ToString();
        }

        return "";
    }

    public DataSet getPastasMensagens(int codUsuario, string where)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"
            SELECT CodigoPasta, NomePasta, IndicaEntradaSaida, 'S' AS PodeExcluir
              FROM {0}.{1}.PastaMensagemUsuario
             WHERE CodigoUsuario = {2}
               {3}
        ", cDados.getDbName(), cDados.getDbOwner(), codUsuario, where);

        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }
    private void carregaPastasUsuario()
    {
        DataSet ds = getPastasMensagens(codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            popupMenuMover.Items.FindByName("mover").Items.Clear();

            DataRow[] drsEntrada = ds.Tables[0].Select("IndicaEntradaSaida = 'E'");

            MailTree.Nodes.FindByName("E").Nodes.Clear();

            MenuItem menuItemEntrada = popupMenuMover.Items.FindByName("mover").Items.Add(Resources.traducao.PainelMensagens_caixa_de_entrada, "E");
            menuItemEntrada.Image.SpriteProperties.CssClass = "Sprite_Inbox";
            menuItemEntrada.Image.Width = 21;

            foreach (DataRow dr in drsEntrada)
            {
                TreeViewNode tvn = new TreeViewNode(dr["NomePasta"].ToString(), dr["CodigoPasta"].ToString() + ";" + dr["PodeExcluir"].ToString());
                tvn.Image.SpriteProperties.CssClass = "Sprite_ASP";
                tvn.Image.SpriteProperties.SelectedCssClass = "Sprite_Announcements";
                tvn.Image.Width = new System.Web.UI.WebControls.Unit("22px");
                tvn.Image.Height = new System.Web.UI.WebControls.Unit("22px");

                MailTree.Nodes.FindByName("E").Nodes.Add(tvn);
                MenuItem menu = menuItemEntrada.Items.Add(dr["NomePasta"].ToString(), dr["CodigoPasta"].ToString());
                menu.Image.SpriteProperties.CssClass = "Sprite_Announcements";
                menu.Image.Width = 21;
            }

            DataRow[] drsSaida = ds.Tables[0].Select("IndicaEntradaSaida = 'S'");

            MailTree.Nodes.FindByName("S").Nodes.Clear();

            MenuItem menuItemSaida = popupMenuMover.Items.FindByName("mover").Items.Add(Resources.traducao.PainelMensagens_itens_enviados, "S");
            menuItemSaida.Image.SpriteProperties.CssClass = "Sprite_SentItems";
            menuItemSaida.Image.Width = 26;

            foreach (DataRow dr in drsSaida)
            {
                TreeViewNode tvn = new TreeViewNode(dr["NomePasta"].ToString(), dr["CodigoPasta"].ToString() + ";" + dr["PodeExcluir"].ToString());
                tvn.Image.SpriteProperties.CssClass = "Sprite_ASP";
                tvn.Image.SpriteProperties.SelectedCssClass = "Sprite_Announcements";
                tvn.Image.Width = new System.Web.UI.WebControls.Unit("22px");
                tvn.Image.Height = new System.Web.UI.WebControls.Unit("22px");

                MailTree.Nodes.FindByName("S").Nodes.Add(tvn);
                MenuItem menuSaida = menuItemSaida.Items.Add(dr["NomePasta"].ToString(), dr["CodigoPasta"].ToString());
                menuSaida.Image.SpriteProperties.CssClass = "Sprite_Announcements";
                menuSaida.Image.Width = 21;
            }
        }
    }

    protected void pnMenu_Callback(object sender, CallbackEventArgsBase e)
    {
        pnMenu.JSProperties["cp_SelecionaPasta"] = "";
        pnMenu.JSProperties["cp_MensagemExclusao"] = "";
        if (e.Parameter != "")
        {
            string nomeDaPasta = txtNomePasta.Text.Replace("'", "'+char(39)+'");
            if (e.Parameter == "E" || e.Parameter == "S")
            {
                incluiPastaMensagensUsuario(codigoUsuarioResponsavel, nomeDaPasta, e.Parameter);
                MailTree.SelectedNode = MailTree.Nodes[0].Nodes.FindByName(e.Parameter);
                pnMenu.JSProperties["cp_SelecionaPasta"] = e.Parameter;
            }
            else if (!e.Parameter.Contains('X'))
            {
                cDados.atualizaPastaMensagensUsuario(codigoUsuarioResponsavel, nomeDaPasta, e.Parameter);
                MailTree.SelectedNode = null;
                pnMenu.JSProperties["cp_SelecionaPasta"] = "-1";
            }
            else if (e.Parameter.Contains('X'))
            {
                bool podeExcluir = verificaSePodeExcluirAPasta(codigoUsuarioResponsavel, e.Parameter.Replace("X", ""));

                if (podeExcluir == true)
                {
                    pnMenu.JSProperties["cp_PodeExcluir"] = true.ToString();
                    pnMenu.JSProperties["cp_MensagemExclusao"] = "A pasta foi excluída com sucesso!";
                    cDados.excluiPastaMensagensUsuario(codigoUsuarioResponsavel, e.Parameter.Replace("X", ""));
                    MailTree.SelectedNode = null;
                    pnMenu.JSProperties["cp_SelecionaPasta"] = "-1";
                }
                else
                {
                    pnMenu.JSProperties["cp_PodeExcluir"] = false.ToString();
                    pnMenu.JSProperties["cp_MensagemExclusao"] = Resources.traducao.PainelMensagens_a_pasta_n_o_foi_exclu_da__pois_h__mensagens_dentro_dela_;
                }
                
            }
            carregaPastasUsuario();
        }
    }

    public bool incluiPastaMensagensUsuario(int codigoUsuario, string nomePasta, string indicaEntradaSaida)
    {
        try
        {
            string comandoSQL = string.Format(@"
                                INSERT INTO {0}.{1}.PastaMensagemUsuario(NomePasta, IndicaEntradaSaida, CodigoUsuario)
                                                                  VALUES('{2}', '{3}', {4})
                                ", cDados.getDbName(), cDados.getDbOwner(), nomePasta, indicaEntradaSaida, codigoUsuario);

            int registrosAfetados = 0;
            cDados.execSQL(comandoSQL, ref registrosAfetados);
            return true;

        }
        catch
        {
            return false;
        }
    }
    private bool verificaSePodeExcluirAPasta(int codigoUsuarioResponsavel, string codigoPastaSelecionada)
    {
        DataSet ds1 = new DataSet();
        bool retorno = false;
        string tipoPasta = getTipoPasta(codigoPastaSelecionada);
        string where = "";

   
        if (codigoPastaSelecionada != "")
        {
            if (codigoPastaSelecionada == "E" || codigoPastaSelecionada == "S")
                where = " AND cm.IndicaTipoMensagem = '" + codigoPastaSelecionada + "' AND cm.CodigoPasta IS NULL";
            else
                where = " AND cm.CodigoPasta = " + codigoPastaSelecionada;

            if (ckTodasEntidades.Checked == false)
                where += " AND m.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;

            ds1 = cDados.getItensPainelMensagensUsuario(codigoUsuarioResponsavel, tipoPasta, where);
            
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

                retorno = dt.Rows.Count == 0;
                //MailGrid.DataSource = dt;

            }
            else
            {
                //MailGrid.DataSource = ds1;

                retorno = ds1.Tables[0].Rows.Count == 0;
            }           
        }
        return retorno;
    }

    protected void MailGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string indicaLida = MailGrid.GetRowValues(e.VisibleIndex, "Lida").ToString();
            string tipoMensagem = MailGrid.GetRowValues(MailGrid.FocusedRowIndex, "IndicaTipoMensagem") + "";
            e.Row.Style.Clear();

            if (indicaLida == "N" && tipoMensagem == "E")
            {                
                MailGrid.JSProperties["cp_NaoLidas"] += (e.KeyValue.ToString() + ";");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Style.Add("font-weight", "bold");
                }
            }
        }
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

    protected void ckTodasEntidades_CheckedChanged(object sender, EventArgs e)
    {
        MailGrid.ExpandAll();
    }

    protected void MailGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Prioridade")
        {
            if (e.CellValue.ToString() == "A")
            {
                e.Cell.Text = "<img alt='Alta Prioridade' src='../imagens/PrioridadeAlta.PNG' />";
                e.Cell.ToolTip = "Alta Prioridade";
            }
            else if (e.CellValue.ToString() == "B")
            {
                e.Cell.Text = "<img alt='Baixa Prioridade' src='../imagens/PrioridadeBaixa.PNG' />";
                e.Cell.ToolTip = Resources.traducao.PainelMensagens_baixa_prioridade;
            }
            else
                e.Cell.Text = "<img alt='Baixa Prioridade' src='../imagens/PrioridadeBaixa.PNG' />";

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

        MenuItem menuItemCategorizar = popupMenuMover.Items.FindByName("categorizar");

        menuItemCategorizar.Items.Clear();

        menuItemCategorizar.Items.Add(Resources.traducao.PainelMensagens_sem_categoria, "-1");

        foreach (DataRow dr in dsCategorias.Tables[0].Rows)
        {
            menuItemCategorizar.Items.Add(dr["DescricaoCategoria"].ToString(), dr["CodigoCategoria"].ToString());
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PnlMsgUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "PnlMsgUsu", lblTituloTela.Text, this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"< [^>]* >", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void ckTodasEntidades_Init(object sender, EventArgs e)
    {
        
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        DataSet dsAux = cDados.getEntidadesUsuario(codigoUsuarioResponsavel, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioResponsavel.ToString());
        int QtdeEntidades = dsAux.Tables[0].Rows.Count;
        ((ASPxCheckBox)sender).Visible = (QtdeEntidades > 1);
       
    }
}
