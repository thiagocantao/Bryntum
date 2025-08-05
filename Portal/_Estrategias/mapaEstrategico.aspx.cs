/*
24/06/2010 - by Alejandro
            Alteração agregando tipo de visualização do mapa estrategico (Mapa Iluminado, Em Arvore, Lista).
*/
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing; //para identar o xml

public partial class _Estrategias_mapaEstrategico : System.Web.UI.Page
{
    private bool usarDetalheObjetigoAntigo = false; // variável a ser eliminada após terminada a nova tela de detalhe de objetivos

    dados cDados;
    Panel pnUltimaPerspectivaInserida = new Panel(); // alterado 04/12/2010 by alejandro null;
    public int alturaDivArvore = 0; //para indicar o height de la div que contem a visualização "Em Arvore"
    public int alturaDivMapa = 0;   //para indicar o height de la div que contem a visualização "Mapa Iluminado"
    private int codigoUsuarioLogado;
    private int alturaPrincipal = 0;
    private int codigoEntidadeLogada = 0, codigoUnidadeMapa = 0, codigoEntidadeMapa = 0;
    public string xmlMapaEstrategico = ""; //variavel temporal, teste de gerar xml do mapa estratégico.
    private string descricaoMenuMapaEstrategico = "Mapa Estratégico";
    public string alturaTela = "";
    //parâmetros : Flash View
    public string webServicePath = "";  // caminho do web service
    public string codigoMapa = "";
    public string nomeMapa = "";
    public string codigoUsuario = "";
    public string codigoEntidade = "";
    public string alturaObject = "";
    public string largoObject = "";

    public string estiloFooter = "dxtlControl dxtlFooter";

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

        codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();

        codigoUsuarioLogado = int.Parse(codigoUsuario);
        codigoEntidadeLogada = int.Parse(codigoEntidade);


        if (!IsPostBack)
        {
            bool bPodeAcessar;

            bPodeAcessar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "ME_Vsl");

            if (bPodeAcessar == false)
                bPodeAcessar = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "ME", "ME_Vsl");

            if (bPodeAcessar == false)
                bPodeAcessar = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "UN", "ME_Vsl");

            if( bPodeAcessar == false)
                cDados.RedirecionaParaTelaSemAcesso(this);

        }

        ddlVisualizacao.Items[0].Text = Resources.traducao.op__o_vis_o_mapa_iluminado;
        ddlVisualizacao.Items[1].Text = Resources.traducao.op__o_vis_o_mapa_em__rvore;
        imgReuniao.ClientVisible = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "RE_Cns");

        webServicePath = getWebServicePath();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("mapaEstrategico"));

        pnMapaFlash.Visible = false;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        imgReuniao.Style.Add("cursor", "pointer");
        //carregaComboAnos();
        //carregaComboMeses();
        carregaComboMapas();  // carrega e já 'seleciona' o mapa.

        if (ddlMapa.SelectedIndex != -1)
        {
            DataSet dsUnidadeMapa = cDados.getMapasEstrategicos(null, " AND Mapa.CodigoMapaEstrategico = " + ddlMapa.Value);

            if (cDados.DataSetOk(dsUnidadeMapa) && cDados.DataTableOk(dsUnidadeMapa.Tables[0]))
            {
                codigoUnidadeMapa = int.Parse(dsUnidadeMapa.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
                codigoEntidadeMapa = int.Parse(dsUnidadeMapa.Tables[0].Rows[0]["CodigoEntidade"].ToString());
				string indicaMapaCarregado = dsUnidadeMapa.Tables[0].Rows[0]["IndicaMapaCarregado"].ToString();
				if (indicaMapaCarregado == "S")
				{
					var itemMapaCarregado = ddlVisualizacao.Items.FindByValue("MC");
					var itemMapaIluminado = ddlVisualizacao.Items.FindByValue("MI");
					if (itemMapaCarregado == null)
					{
						itemMapaCarregado = new ListEditItem(Resources.traducao.op__o_vis_o_mapa_iluminado, "MC");
						ddlVisualizacao.Items.Insert(0, itemMapaCarregado);
					}
					ddlVisualizacao.SelectedItem = itemMapaCarregado;
					ddlVisualizacao.Items.Remove(itemMapaIluminado);
				}
				else
				{
                    pnMapaFlash.Visible = true;
                    var itemMapaCarregado = ddlVisualizacao.Items.FindByValue("MC");
					var itemMapaIluminado = ddlVisualizacao.Items.FindByValue("MI");
                    //ddlVisualizacao.Items.Remove(itemMapaCarregado);
                    ddlVisualizacao.Value = "EA";
				}
            }
                        
            if (codigoEntidadeLogada != codigoEntidadeMapa)
            {
                DataSet dsUnidade = cDados.getUnidadeNegocio(" AND un.CodigoUnidadeNegocio = " + codigoEntidadeMapa);
                if(cDados.DataSetOk(dsUnidade) && cDados.DataTableOk(dsUnidade.Tables[0]))
                    lblEntidadeDiferente.Text = Resources.traducao.voc__est__visualizando_as_informa__es_da_entidade + " " + dsUnidade.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
            }
            else
            {
                lblEntidadeDiferente.Text = "";
            }

            //if (codigoUnidadeMapa != 0)
            //    codigoEntidadeLogada = codigoUnidadeMapa;

            cDados.aplicaEstiloVisual(this);

        }

        // defina o código do mapa que deve ser apresentado aqui.
        //int codigoMapaEstrategico = getCodigoMapaAtivo(codigoEntidade);

        //if (codigoMapaEstrategico != -1)
        carregaMapaEstrategicoArvore();
        //ddlVisualizacao.SelectedIndex = 0;

        if (ddlMapa.Items.Count > 0)
        {
            cDados.setInfoSistema("CodigoMapa", ddlMapa.Value.ToString());
        }
        else
        {
            imgReuniao.Enabled = false;
            imgReuniao.Style.Add("Cursor", "default");
            imgReuniao.ImageUrl = "~/imagens/reuniaoDes.png";
        }


        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "MAPA", "EST", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        codigoUsuario = codigoUsuarioLogado.ToString();
       //gvDados.Templates.FooterRow
        //spanLegenda.Controls.Add(gvDados.Templates.FooterRow);
        //gvDados.Templates.FooterRow.InstantiateIn(spanLegenda);

        cDados.configuraPainelBotoesTREELIST(tbBotoesEdicao);
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css""/>"));
        Header.Controls.Add(cDados.getLiteral(@"<link rel=""stylesheet"" type=""text/css"" href=""../estilos/bordas.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/visualizacaoMapaEstrategico.js""></script>"));
        this.TH(this.TS("visualizacaoMapaEstrategico"));
      

        //Obter a descrição para o menu do Mapa Estratégico.
        DataSet ds = cDados.getValorParametroConfiguracaoSistema("nomePainelEstrategico",codigoEntidadeLogada.ToString(), "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            descricaoMenuMapaEstrategico = ds.Tables[0].Rows[0]["Valor"].ToString();
        lblTituloTela.Text = descricaoMenuMapaEstrategico;
        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxtlControl_" + cssPostfix + " dxtlFooter_" + cssPostfix;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 140);
        if (altura > 0)
        {
            alturaDivArvore = altura - 55;
            alturaDivMapa = altura - 18;
            alturaObject = altura + 360 + "px";
            largoObject = altura + 425 + "px";
            
        }

        tlMapaEstrategico.Settings.ScrollableHeight = alturaPrincipal - 315;
    }

    public string getLinkIndicadorObjetivo(string tipoObjeto, string codigo, string codigoPai, string descricao, bool permissao)
    {
        if (permissao == false)
            return descricao;

        if (tipoObjeto == "OBJ")
        {
            if (usarDetalheObjetigoAntigo)
                return string.Format(@"<a href='detalhesObjetivoEstrategico.aspx?COE={0}' target='_parent'>{1}</a>", codigo, descricao);
            else
                return string.Format(@"<a href='objetivoestrategico/indexResumoObjetivo.aspx?COE={0}&UNM={2}&CM={3}' target='_parent'>{1}</a>", codigo, descricao, codigoUnidadeMapa, ddlMapa.Value + "");
        }
        else if (tipoObjeto == "IND")
        {
            if (codigo.IndexOf(".") != -1 && !codigo.Equals(""))
                codigo = codigo.Substring(1, codigo.IndexOf('.') - 1);
            return string.Format(@"<a href='indicador/index.aspx?NivelNavegacao=1&COIN={0}&COE={1}&CM={3}&UNM={4}&UN={5}' target='_parent'>{2}</a>", codigo, codigoPai, descricao, ddlMapa.Value + "", codigoUnidadeMapa, codigoUnidadeMapa);
        }
        else if (tipoObjeto == "INI")
        {
            codigo = codigo.IndexOf(".") == -1 ? codigo : codigo.Substring(1, codigo.IndexOf('.') - 1);
            return string.Format(@"<a href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?NivelNavegacao=1&IDProjeto={0}&NomeProjeto={1}' target='_parent'>{1}</a>", codigo, descricao);
        }
        else if (tipoObjeto == "TEM")
        {            
             return string.Format(@"<a href='tema/index.aspx?CT={0}&UNM={2}&CM={3}' target='_parent'>{1}</a>", codigo, descricao, codigoUnidadeMapa, ddlMapa.Value + "");
        }
        else
        {
            return descricao;
        }
    }

    private string getWebServicePath()
    {
        return cDados.getPathSistema() + "wsPortal.asmx?WSDL";
    }

    #endregion

    private int getCodigoMapaAtivo(int codigoEntidade)
    {
        int codigoMapa = -1;

        DataSet dsMapa = cDados.getMapasEstrategicos(codigoEntidade, " AND IndicaMapaEstrategicoAtivo = 'S'");

        if (cDados.DataSetOk(dsMapa) && cDados.DataTableOk(dsMapa.Tables[0]))
            codigoMapa = int.Parse(dsMapa.Tables[0].Rows[0]["CodigoMapaEstrategico"].ToString());

        return codigoMapa;
    }

    private void carregaMapaEstrategicoArvore()
    {

        int mesesAnteriores = -1;

        DateTime dataMapa = DateTime.Now.AddMonths(mesesAnteriores);

        //----------------------------------------------------[Em Arvore]
        DataSet ds = cDados.getTreeMapaEstrategico((ddlMapa.Value != null) ? ddlMapa.Value.ToString() : "-1", codigoEntidadeLogada.ToString(), codigoUsuarioLogado, "");

        if (cDados.DataSetOk(ds))
        {
            tlMapaEstrategico.DataSource = ds.Tables[0];
            tlMapaEstrategico.DataBind();
            tlMapaEstrategico.ExpandAll();
        }

    }
    
    #region COMBOBOX

    private void carregaComboAnos()
    {
        //if (ddlUnidade.SelectedIndex != -1)
        //{
        DataSet dsAnos = cDados.getAnosAtivosUnidade(1, " AND pe.Ano <= YEAR(GETDATE())");

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAno.DataSource = dsAnos;
            ddlAno.TextField = "Ano";
            ddlAno.ValueField = "Ano";
            ddlAno.DataBind();
        }

        if (!IsPostBack)
        {
            selecionaAno();
        }
        //}
    }

    private void carregaComboMeses()
    {
        ddlMes.Items.Clear();
        if (ddlAno.SelectedIndex != -1)
        {
            int meses = 12;

            if (ddlAno.Value.ToString() == DateTime.Now.Year.ToString())
            {
                meses = DateTime.Now.Month - 1;
            }

            for (int i = 1; i <= meses; i++)
            {
                string mes = string.Format("{0:MMM}", DateTime.Parse(i + "/" + i + "/2010")).ToUpper();

                ListEditItem lei = new ListEditItem(mes, i);

                ddlMes.Items.Insert(i - 1, lei);
            }

            if (!IsPostBack)
                selecionaMes();
        }
    }

    private void selecionaAno()
    {
        if (ddlAno.Items.Count > 0)
        {
            if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                ddlAno.Value = DateTime.Now.Year.ToString();
            else
                ddlAno.SelectedIndex = 0;
        }
    }

    private void selecionaMes()
    {
        if (ddlAno.Items.Count > 0)
        {
            if (ddlAno.Value.ToString() == DateTime.Now.Year.ToString())
            {
                ddlMes.Value = (DateTime.Now.Month - 1);
            }
            else
            {
                ddlMes.SelectedIndex = 0;
            }
            ddlMes.JSProperties["cp_Mes"] = ddlMes.Value;
        }
    }

    protected void ddlMes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        selecionaMes();
    }

	private void carregaComboMapas()
	{
		string where = "AND ((IndicaMapaCarregado IS NULL) OR (IndicaMapaCarregado <> 'S') OR (IndicaMapaCarregado = 'S' AND ImagemMapa IS NOT NULL))";
		DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeLogada.ToString(), codigoUsuarioLogado, where);

		if (cDados.DataSetOk(dsMapas))
		{
			ddlMapa.DataSource = dsMapas;
			ddlMapa.TextField = "TituloMapaEstrategico";
			ddlMapa.ValueField = "CodigoMapaEstrategico";
			ddlMapa.DataBind();
		}

		if (!IsPostBack && ddlMapa.Items.Count > 0)
		{
            bool carregaMapaEstrategicoInicial = false;
            if ((cDados.getInfoSistema("CodigoMapaEstrategicoInicial") != null) && (cDados.getInfoSistema("CodigoMapaEstrategicoInicial").ToString() != ""))
            {
                if (ddlMapa.Items.FindByValue(int.Parse(cDados.getInfoSistema("CodigoMapaEstrategicoInicial").ToString())) != null)
                {
                    carregaMapaEstrategicoInicial = true;
                }
            }
            if (carregaMapaEstrategicoInicial)
            {
    			ddlMapa.Value = int.Parse(cDados.getInfoSistema("CodigoMapaEstrategicoInicial").ToString());
	    	}
		    else
			{

				DataSet dsParametro = cDados.getMapaDefaultUsuario(codigoUsuarioLogado, "");

				if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
				{
					string auxCodMapa = dsParametro.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"] + "";

					if (auxCodMapa != "" && ddlMapa.Items.FindByValue(int.Parse(auxCodMapa)) != null)
						ddlMapa.Value = int.Parse(auxCodMapa);
					else
						ddlMapa.SelectedIndex = 0;
				}
				else
				{
					ddlMapa.SelectedIndex = 0;
				}
			}
			codigoMapa = ddlMapa.Value.ToString();
			cDados.setInfoSistema("CodigoMapa", codigoMapa);

		}
		else
		{
			codigoMapa = (ddlMapa.Value != null) ? ddlMapa.Value.ToString() : "-1";
		}
	}

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.exportaTreeList(ASPxTreeListExporter1, parameter);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstMapaEst", "Lista Mapa Estratégico", this);
    }

    #endregion
    protected void ASPxTreeListExporter1_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.TextValue != null)
        {
            if (e.Column.FieldName == "Cor")
            {
                e.BrickStyle.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;


                if (e.TextValue.ToString().Trim().Contains("Vermelho"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Red;
                }
                else if (e.TextValue.ToString().Trim().Contains("Amarelo"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Yellow;
                }
                else if (e.TextValue.ToString().Trim().Contains("Verde"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Green;
                }
                else if (e.TextValue.ToString().Trim().Contains("Azul"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Blue;
                }
                else if (e.TextValue.ToString().Trim().Contains("Branco"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.WhiteSmoke;
                }
                else if (e.TextValue.ToString().Trim().Contains("Laranja"))
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Orange;
                }

            }
        }        
    }
}
