/*
 08/12//2010: Mudança by Alejandro: 
            Foi implementado a inserção de Metas.
 15/12//2010: Mudança by Alejandro: 
            Foi mudado la propiedade "Rows" (do componente [txtMeta]) con el
            valor 2.
            
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
using System.Text;
using DevExpress.Web;
using System.Drawing;


public partial class _Estrategias_wizard_metasDesempenho : System.Web.UI.Page
{
    dados cDados;

    private string resolucaoCliente = ""; 
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int alturaPrincipal = 0, larguraPrincipal = 0;

    public bool podeEditar = false;

    protected void Page_Init(object sender, EventArgs e)
    {
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(pnCallback);      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HearderOnTela();
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente); 

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        if(!ddlAssociacao.IsCallback)
            carregaComboAssociacao();

        carregaComboMapas();

        populaGrid();

        if (!IsPostBack)
        {

            hfGeral.Set("PermissaoLinha", "N");
            hfGeral.Set("CodigoIndicador", "-1");

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "CNIMET", "EST", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 140;

        pcDados.Width = larguraPrincipal - 80;
    }

    private void HearderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/CNI_GestaoMetas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CNI_GestaoMetas", "_Strings"));
    }

    private void MenuUsuarioLogado()
    {
        gvDados.Columns[0].Visible = false;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTMETA"))
            gvDados.Columns[0].Visible = true;
    }

    #endregion

    #region GRIDVIEW

    #region gvDados

    private void populaGrid()
    {
        #region COMANDOS
        string select = string.Format(@", CAST(CASE WHEN EXISTS 
				(
					SELECT TOP 1 1 
						FROM {0}.{1}.[IndicadorUnidade]	AS [iu]
							INNER JOIN {0}.{1}.[UnidadeNegocio]	AS [un]	ON 
								(			un.[CodigoUnidadeNegocio] = iu.[CodigoUnidadeNegocio]
									AND un.[CodigoEntidade]			= {3}  AND un.IndicaUnidadeNegocioAtiva = 'S' AND un.DataExclusao IS NULL)
						WHERE
									iu.[CodigoIndicador]		= i.[CodigoIndicador]
							AND {0}.{1}.f_VerificaAcessoConcedido({2},  {3}, iu.[CodigoIndicador], NULL, 'IN', iu.[CodigoUnidadeNegocio], NULL, 'IN_DefMta') = 1
				) THEN 1 ELSE 0 END AS Bit) AS [Permissoes] 
            ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);
        string where = string.Format(@" AND EXISTS 
				(
					SELECT TOP 1 1 
						FROM {0}.{1}.[IndicadorUnidade]	AS [iu]
							INNER JOIN {0}.{1}.[UnidadeNegocio]	AS [un]	ON 
								(			un.[CodigoUnidadeNegocio] = iu.[CodigoUnidadeNegocio]
									AND un.[CodigoEntidade]			= {3}  AND un.IndicaUnidadeNegocioAtiva = 'S' AND un.DataExclusao IS NULL)
						WHERE
									iu.[CodigoIndicador]		= i.[CodigoIndicador]
							AND {0}.{1}.f_VerificaAcessoConcedido({2},  {3}, iu.[CodigoIndicador], NULL, 'IN', iu.[CodigoUnidadeNegocio], NULL, 'IN_CnsMta') = 1
				) 
            ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);
        #endregion

        DataSet ds = cDados.getIndicadoresGestaoMetas(idUsuarioLogado, codigoEntidade, select, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
            
        }
    }
    
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Permissoes") != null)
        {
            //int permissao = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            podeEditar = (bool)gvDados.GetRowValues(e.VisibleIndex, "Permissoes"); //(permissao & 2) > 0;

            if (e.ButtonID.Equals("btnEditar"))
            {
                if (podeEditar)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Text = "Detalhe da Meta";
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/pFormulario.png";
                }
            }
            else if (e.ButtonID.Equals("btnAnalises"))
            {
                string codigoObjetoEstrategia = gvDados.GetRowValues(e.VisibleIndex, "CodigoObjetoEstrategia") + "";

                if (codigoObjetoEstrategia == "")
                {
                    e.Text = "";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/btnAnalisesDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnExcluir"))
            {
                string codigoObjetoEstrategia = gvDados.GetRowValues(e.VisibleIndex, "CodigoObjetoEstrategia") + "";

                if (codigoObjetoEstrategia == "" || !podeEditar)
                {
                    e.Text = "";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
        }
    }

    #endregion

    
    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
                
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoIndicador = int.Parse(getChavePrimaria());
        string objetoAssociado = ddlAssociacao.Value == null ? "NULL" : ddlAssociacao.Value.ToString();
        string meta = txtMeta.Text.Replace("'", "''");
        string descricao = txtDescricao.Text.Replace("'", "''");
        string comentarios = txtComentarios.Text.Replace("'", "''");
        int codigoObjetoEstrategia = ddlAssociacao.Value == null ? -1 : int.Parse(ddlAssociacao.Value.ToString());
        string iniciaisTipoAssociacao = (rbTipo.Value + "") == "PSP" ? "PP" : "OB";

        bool result = cDados.atualizaGestaoMetas(codigoIndicador, codigoEntidade, idUsuarioLogado, codigoObjetoEstrategia, meta, descricao, comentarios, iniciaisTipoAssociacao);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            populaGrid();
            carregaComboAssociacao();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoIndicador = int.Parse(getChavePrimaria());
        string objetoAssociado = ddlAssociacao.Value == null ? "NULL" : ddlAssociacao.Value.ToString();
        int codigoObjetoEstrategia = ddlAssociacao.Value == null ? -1 : int.Parse(ddlAssociacao.Value.ToString());
        string iniciaisTipoAssociacao = (rbTipo.Value + "") == "PSP" ? "PP" : "OB";

        bool result = cDados.excluiGestaoMetas(codigoIndicador, codigoEntidade, idUsuarioLogado, codigoObjetoEstrategia, iniciaisTipoAssociacao);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            populaGrid();
            carregaComboAssociacao();
            //gvDados.JSProperties["cp_IndexLinha"] = gvDados.FocusedRowIndex;
            return "";
        }
    }

    #endregion

    public string getTipoMeta()
    {
        string tipoMeta = "";
        string siglaTipo = Eval("Tipo").ToString();

        if (siglaTipo == "PSP")
            tipoMeta = "Macrometa";
        else if (siglaTipo == "OBJ")
            tipoMeta = "Micrometa";

        return tipoMeta;
    }

    private void carregaComboAssociacao()
    {
        string iniciais = rbTipo.Value == null ? "-1" : rbTipo.Value.ToString();
        string codigoMapa = ddlMapa.Value == null ? "-1" : ddlMapa.Value.ToString();
        int codigoIndicador = iniciais == "OBJ" ? -1 : int.Parse(getChavePrimaria());

        DataSet ds = cDados.getObjetosAssociacaoMetas(codigoMapa, iniciais, codigoIndicador, "");

        if (cDados.DataSetOk(ds))
        {
            ddlAssociacao.DataSource = ds;
            ddlAssociacao.TextField = "Descricao";
            ddlAssociacao.ValueField = "CodigoObjetoEstrategia";
            ddlAssociacao.DataBind();
        }
    }

    private void carregaComboMapas()
    {
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidade.ToString(), idUsuarioLogado, "");

        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();
        }
    }

    protected void ddlAssociacao_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaComboAssociacao();

        if (e.Parameter != "" && e.Parameter != "-1")
        {
            ddlAssociacao.JSProperties["cp_Valor"] = e.Parameter;
        }
        else
        {
            ddlAssociacao.JSProperties["cp_Valor"] = "-1";
        }

        
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Meta")
        {
            if (e.CellValue.ToString().Length > 120)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 120) + "...";
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        Font fonte = new Font("Verdana", 9);
        e.BrickStyle.Font = fonte;

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

        if (e.Column.Name == "Tipo")
        {
            if (e.Text == "OBJ")
                e.TextValue = "Micrometa";
            else if (e.Text == "PSP")
                e.TextValue = "Macrometa";
        }
    }
}
