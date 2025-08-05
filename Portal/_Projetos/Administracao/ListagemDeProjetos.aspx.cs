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

public partial class _Projetos_Administracao_ListagemDeProjetos : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //if (!IsPostBack)
        //{
        cDados.aplicaEstiloVisual(Page);
        //}
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ((GridViewDataTextColumn)gvDados.Columns["SiglaUnidadeNegocio"]).Caption = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        carregaGvDados();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ListagemDeProjetos.js""></script>"));
        this.TH(this.TS("ListagemDeProjetos"));
    }

    private void carregaGvDados()
    {
        DataSet ds = cDados.getProjetosToProgramas(codigoEntidade, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }
}
