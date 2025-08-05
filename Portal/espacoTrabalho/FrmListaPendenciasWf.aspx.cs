using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using CDIS;

public partial class espacoTrabalho_PendenciasWorkflow : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["sc"] != null)
        {
            string stringCriptografada = Request.QueryString["sc"].ToString();

            decriptaStringConexao(stringCriptografada);
        }

        if (cDados.getInfoSistema("IDUsuarioLogado") == null)
        {
            codigoUsuarioResponsavel = 0;
            codigoEntidadeUsuarioResponsavel = 0;
        }
        else
        { 
            //Get dado do usuario logado, e do qual entidad ele pertenece.        
            codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        }
    }

    private void decriptaStringConexao(string stringCriptografada)
    {
        string chavePrivada = "", stringPlana;
        DataSet dsChaveWfMobile = cDados.getParametrosSistema(-1, "chaveAutenticacaoWsMobile");
        DateTime datConexao;
        int codigoEntidade = 0, codigoUsuario = 0;

        if (cDados.DataSetOk(dsChaveWfMobile) && cDados.DataTableOk(dsChaveWfMobile.Tables[0]))
        {
            chavePrivada = dsChaveWfMobile.Tables[0].Rows[0]["chaveAutenticacaoWsMobile"] + "";
            stringPlana = Cripto.descriptografar(stringCriptografada, chavePrivada);
            if (string.IsNullOrEmpty(stringPlana) == false)
            {
                string[] valores = stringPlana.Split(';');

                if (valores.Length > 2)
                {
                    if (DateTime.TryParse(valores[1].TrimEnd(' '), out datConexao))
                    {
                        if (datConexao.AddHours(1).CompareTo(DateTime.Now) >= 0)
                        {

                            if (int.TryParse(valores[2], out codigoEntidade))
                            {
                                string strWhere = string.Format(" AND us.[Email] = '{0}' ", valores[0].TrimEnd(' '));
                                DataSet dsUsr = cDados.getDadosResumidosUsuario(strWhere);
                                if (cDados.DataSetOk(dsUsr) && cDados.DataTableOk(dsUsr.Tables[0]))
                                {
                                    codigoUsuario = int.Parse(dsUsr.Tables[0].Rows[0]["CodigoUsuario"].ToString());
                                }
                            }
                        }
                    }

                }
            }
        }

        cDados.setInfoSistema("CodigoEntidade", codigoEntidade);
        cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
        return;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        populaGrid();

        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PendenciasWorkflow.js""></script>"));
        this.TH(this.TS("PendenciasWorkflow", "FrmListaPendenciasWf"));
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void populaGrid()
    {

        string comandoSQL = string.Format(@"
        BEGIN
                DECLARE @CodigoFluxo as int
                DECLARE @IniciaisFluxo as varchar(18)
                DECLARE @isMobile as bit
				
                SET @isMobile = {2}
                SET @CodigoFluxo = NULL
				SET @IniciaisFluxo = NULL

                SELECT @IniciaisFluxo = Valor FROM ParametroConfiguracaoSistema 
                WHERE Parametro = 'iniciaisFluxoTelaPendencia' AND CodigoEntidade = {1}
                
                IF(@isMobile = 1)
                BEGIN
                    SELECT @CodigoFluxo = CodigoFluxo 
                     FROM Fluxos WHERE IniciaisFluxo =  @IniciaisFluxo
                END
               EXEC [dbo].[p_wf_obtemListaInstanciasUsuario] 
                  @in_identificadorUsuario	= '{0}'
                , @in_codigoEntidade		= {1}
                , @in_codigoFluxo			= @CodigoFluxo
                , @in_codigoProjeto 		= NULL
                ,@in_somenteInteracao       = 1
                ,@in_somentePendencia       = 1
                ,@in_palavraChave           = NULL
        END
", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, cDados.isMobileBrowser() == true ? "1" : "0");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            //((GridViewCommandColumn)gvDados.Columns[0]).CustomButtons[0].Visibility = GridViewCustomButtonVisibility.BrowsableRow;
        }

    }

    #region VARIOS
        
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        alturaPrincipal -= 390;         

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LtPFlxUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "LtPFlxUsu", "Pendências de Workflow", this);

        if (!IsPostBack)
        {
            if (Request.QueryString["ATR"] + "" == "S")
            {
                if (gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [IndicaAtraso] = 'Sim'";
                else
                    gvDados.FilterExpression = " [IndicaAtraso] = 'Sim'";
            }
        }
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
        else if (e.RowType == GridViewRowType.Header)
        {
            if (e.Column.Name == "Acao")
                e.Text = Resources.traducao.FrmListaPendenciasWf_possui_pend_ncias_;
        }
    }

    public string getRowCount()
    {
        string retorno = "";
        int quantidadeLinhas = 0;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            if (!gvDados.IsGroupRow(i))
                quantidadeLinhas++;
        }

        if (quantidadeLinhas == 0)
        {
            retorno = Resources.traducao.FrmListaPendenciasWf_nenhuma_pend_ncia;
        }
        else if (quantidadeLinhas == 1)
        {
            retorno = quantidadeLinhas + " " + Resources.traducao.FrmListaPendenciasWf_pend_ncia;
        }
        else if(quantidadeLinhas > 1)
        {
            retorno = quantidadeLinhas + " " + Resources.traducao.FrmListaPendenciasWf_pend_ncias;
        }

        

        return retorno;
    }
}