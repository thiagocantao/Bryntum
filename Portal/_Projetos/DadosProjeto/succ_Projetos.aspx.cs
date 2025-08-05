using DevExpress.Web;
using System;
using System.Data;
using System.Web;

public partial class _Projetos_DadosProjeto_succ_Projetos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string resolucaoCliente = "";

    private int codigoProjeto = 0;

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["IDProjeto"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["IDProjeto"] + "") && !string.IsNullOrEmpty(Request.QueryString["IDProjeto"] + ""))
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
         

        defineAlturaTela();
        populaGrid();
        HeaderOnTela();
        cDados.aplicaEstiloVisual(this.Page);
    }
    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/succ_MenuPrincipal.js""></script>"));
        this.TH(this.TS("_Strings", "succ_MenuPrincipal"));
    }
    private void defineAlturaTela()
    {
        int largura = 0;
        int altura = 0;

        //altura dos componentes de altura equivale a  31,91% da altura vinda da resolução da tela.
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        int alturaComponentes = (int)((31.91 / 100) * altura);
        int alturaGrid = altura - alturaComponentes;
        gvDados.Settings.VerticalScrollableHeight = alturaGrid - 95;

    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoContratoPortal,
               CodigoInstrumentoJuridico,
               NumeroInstrumentoJuridico,
               NomeFornecedor,
               DataInicioVigencia,
               DataTerminoVigencia,
               ValorInstrumentoJuridico,
               TipoContrato,
               DescricaoNatureza,
               NomeGestorInstrumentoJuridico,
               CodigoProjeto 
          FROM [f_pbh_IJ_GetInstrumentosJuridicos] ({0})", codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();

    }


    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "succMenuProj", "SUCC - Projetos", this);

    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "succMenuProj");
    }
}