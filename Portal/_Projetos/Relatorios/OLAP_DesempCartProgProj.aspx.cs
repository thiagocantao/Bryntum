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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using System.IO;
using DevExpress.Utils;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Globalization;


public partial class _Projetos_Relatorios_OLAP_DesempCartProgProj : System.Web.UI.Page
{
    private dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public string estiloFooter = "dxtlControl dxtlFooter";
    public string displayLaranja = "";

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "relDesCartProgPrj");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxtlControl_" + cssPostfix + " dxtlFooter_" + cssPostfix;

        if (!IsPostBack)
        {
            ddlMes.Value = string.Format("{0:D2}", DateTime.Now.Month);
            txtAno.Text = DateTime.Now.Year.ToString();
        }
        
        populaUnidadeNegocio();
        
        carregaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {            
            hfGeral.Set("tipoArquivo", "XLS");
            cDados.excluiNiveisAbaixo(1);
            Master.geraRastroSite();
            this.Title = cDados.getNomeSistema();
            cDados.insereNivel(1, this);
        }

        DataSet dsParametros = cDados.getParametrosSistema("MostraFisicoLaranja");       

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
           if (dsParametros.Tables[0].Rows[0]["MostraFisicoLaranja"].ToString().Trim() == "N")
                displayLaranja = "display:none;";
        }  

        cDados.configuraPainelBotoesTREELIST(tbBotoesEdicao);
    }

    private void populaUnidadeNegocio()
    {
        DataSet dsUnidade = cDados.getUnidadeNegocioComProjetosAssociados(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsUnidade))
        {
            ddlUnidade.DataSource = dsUnidade.Tables[0];
            ddlUnidade.TextField = "NomeUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.DataBind();

            //ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

            //ddlUnidade.Items.Insert(0, lei);

            if (!IsPostBack)
                ddlUnidade.SelectedIndex = 0;
        }
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/OLAP_DesempCartProgProj.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "OLAP_DesempCartProgProj"));
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        string alturaTabela = (alturaPrincipal - 200) + "px";//a div vai ficar com essa altura
        string larguraTabela = (larguraPrincipal - 10) + "px";

        tlDados.Settings.ScrollableHeight = alturaPrincipal - 230;
    }

    private void carregaGrid()
    {
        DateTime dataSelecionada = DateTime.Now;
        DateTime dataAtual = DateTime.Now;

        if (int.Parse(ddlMes.Value.ToString()) == dataAtual.Month && txtAno.Text == dataAtual.Year.ToString())
            dataSelecionada = dataAtual;
        else
        {
            int diasMes = DateTime.DaysInMonth(int.Parse(txtAno.Text), int.Parse(ddlMes.Value.ToString()));
            DateTime dtCampo = DateTime.Now;
            bool converteu = DateTime.TryParseExact(diasMes + "/" + ddlMes.Value + "/" + txtAno.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

            if (converteu)
                dataSelecionada = dtCampo;
        }

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE  @CodigoEntidade as int
            DECLARE  @CodigoUnidade as int
            DECLARE  @CodigoUsuario AS int
            DECLARE  @DataParam AS DateTime
            
            SET  @CodigoEntidade = {2}
            SET  @CodigoUsuario = {3}
            SET  @CodigoUnidade = {5}
  
            SET  @DataParam = CONVERT(DateTime, '{4:dd/MM/yyyy}', 103)
            
             SELECT Descricao, 
                   (PercentualPrevisto) as PercentualPrevisto, 
                   (PercentualReal) as PercentualReal, 
                   CorStatus, 
                   CodigoItem, 
                   CodigoItemSuperior, 
                   TipoItem, 
                   EstruturaHierarquica,
                   EstruturaHierarquicaSup
             FROM {0}.{1}.f_uhe_DesempenhoCarteiraProgramaProjeto(@CodigoEntidade, @CodigoUsuario, @DataParam, @CodigoUnidade)
            WHERE 1=1 
        END", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, dataSelecionada, ddlUnidade.Value.ToString());
        DataSet ds = cDados.getDataSet(comandoSQL);
        tlDados.DataSource = ds.Tables[0];
        tlDados.DataBind();
        tlDados.ExpandAll();
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "CorStatus" && e.RowKind == DevExpress.Web.ASPxTreeList.TreeListRowKind.Data)
        {
            //J sorriso fonte vcerde ou azul
            //L triste fonte vermelha
            //K neutro fonte amarela
            e.BrickStyle.Font = new Font("Wingdings", 18, FontStyle.Regular);
            e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;

            if (e.Text.Trim() == "Branco")
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else if (e.Text.Trim() == "Azul")
            {

                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Blue;
            }
            else if (e.Text.Trim() == "Verde")
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Text.Trim() == "Vermelho")
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Text.Trim() == "Amarelo")
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Text.Trim() == "Laranja")
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else
            {
                e.Text = " ";
                e.TextValue = " ";
            }

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
        cDados.exportaTreeList(ASPxTreeListExporter1, parameter);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "DesCartProgProj", lblTituloTela.Text, this);
    }

    #endregion
}
