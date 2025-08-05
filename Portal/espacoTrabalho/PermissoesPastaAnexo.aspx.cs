using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class espacoTrabalho_PermissoesPastaAnexo : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;

    string IniciaisTipoAssociacao;
    int IDObjetoAssociado, IDObjetoPai, codigoPasta = -1;

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;
        IDObjetoPai = (Request.QueryString["IDOP"] != null && Request.QueryString["IDOP"].ToString() != "") ? int.Parse(Request.QueryString["IDOP"].ToString()) : 0;
        codigoPasta = (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "") ? int.Parse(Request.QueryString["CA"].ToString()) : -1;

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        lblNomePasta.Text = Request.QueryString["Nome"] + "";

        carregaGrupos();
        cDados.aplicaEstiloVisual(this);
        if (!IsPostBack)
            carregaPermissoes();
    }

    private void carregaGrupos()
    {
        string comandoSQL = string.Format(@"
        SELECT * FROM dbo.f_pax_GetGruposPermissaoPasta({0}, '{1}', {2});"
        , IDObjetoAssociado
        , IniciaisTipoAssociacao
        , codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            lbGrupos.DataSource = ds;
            lbGrupos.TextField = "DescricaoPerfil_PT";
            lbGrupos.ValueField = "CodigoPerfil";
            lbGrupos.DataBind();
        }

        if (!IsPostBack && lbGrupos.Items.Count > 0)
            lbGrupos.SelectedIndex = 0;
    }

    private void carregaPermissoes()
    {
        if(lbGrupos.SelectedIndex > -1)
        {
            string comandoSQL = string.Format(@"
            select * from dbo.f_pax_GetPermissoesAnexoPerfil({0}, {1}, '{2}', {3}, {4}, {5})", codigoEntidadeUsuarioResponsavel, IDObjetoAssociado, IniciaisTipoAssociacao, IDObjetoPai, codigoPasta, lbGrupos.Value);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string consultar = ds.Tables[0].Rows[0]["ValorAcessoConsultar"].ToString();
                string incluir = ds.Tables[0].Rows[0]["ValorAcessoIncluir"].ToString();
                string alterar = ds.Tables[0].Rows[0]["ValorAcessoAlterar"].ToString();
                string excluir = ds.Tables[0].Rows[0]["ValorAcessoExcluir"].ToString();

                ckNegarC.Checked = consultar == "2" || consultar == "6";
                ckNegarE.Checked = excluir == "2" || excluir == "6";
                ckNegarI.Checked = incluir == "2" || incluir == "6";
                ckNegarM.Checked = alterar == "2" || alterar == "6";

                ckPermitirC.Checked = consultar == "1" || consultar == "5";
                ckPermitirE.Checked = excluir == "1" || excluir == "5";
                ckPermitirI.Checked = incluir == "1" || incluir == "5";
                ckPermitirM.Checked = alterar == "1" || alterar == "5";
            }
            else
            {
                ckNegarC.Checked = false;
                ckNegarE.Checked = false;
                ckNegarI.Checked = false;
                ckNegarM.Checked = false;

                ckPermitirC.Checked = false;
                ckPermitirE.Checked = false;
                ckPermitirI.Checked = false;
                ckPermitirM.Checked = false;
            }
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (lbGrupos.SelectedIndex > -1)
        {
            string valorConsultar = "0";
            string valorIncluir = "0";
            string valorEditar = "0";
            string valorExcluir = "0";

            if (ckPermitirC.Checked)
                valorConsultar = "1";
            else if (ckNegarC.Checked)
                valorConsultar = "2";

            if (ckPermitirI.Checked)
                valorIncluir = "1";
            else if (ckNegarI.Checked)
                valorIncluir = "2";

            if (ckPermitirM.Checked)
                valorEditar = "1";
            else if (ckNegarM.Checked)
                valorEditar = "2";

            if (ckPermitirE.Checked)
                valorExcluir = "1";
            else if (ckNegarE.Checked)
                valorExcluir = "2";

            string comandoSQL = string.Format(@"
        EXEC dbo.p_pax_alteraAcessoAnexo {0}, {1}, {2}, {3}, {4}, {5}, {6}"
            , codigoPasta
            , lbGrupos.Value
            , valorConsultar
            , valorIncluir
            , valorEditar
            , valorExcluir
            , codigoUsuarioResponsavel);

            int regAf = 0;

            cDados.execSQL(comandoSQL, ref regAf);
        }
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaPermissoes();
    }
}