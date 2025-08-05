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

public partial class _Portfolios_frameProposta_AdicionarUnidade : System.Web.UI.Page
{
    dados cDados;
    DataSet ds;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoProjetoSelecionado;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoProjetoSelecionado = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            rblCronograma.SelectedIndex = 0;
        }

        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
        }


        if (!IsPostBack)
        {
            getDadosProjetoUnidade(codigoProjetoSelecionado);
            carregarCombos();

            if (((cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S")) ||
                   ((Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S")))
            {
                btnSalvar.ClientVisible = false;
                ddlUnidadeNegocio.ReadOnly = true;
                ddlGerenteProjeto.ReadOnly = true;
                deInicioProposta.ReadOnly = true;
                deTerminoProposta.ReadOnly = true;
                rblCronograma.ReadOnly = true;
                ddlAssoCroExistente.ReadOnly = true;
            }

        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/frameProposta_AdicionarUnidade.js""></script>"));
        this.TH(this.TS("frameProposta_AdicionarUnidade"));
    }

    #region prencher dados

    private void getDadosProjetoUnidade(int idProjeto)
    {
        ds = cDados.getDadosUnidadeProjeto(idProjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            hfGeral.Set("CodigoCategoria", ds.Tables[0].Rows[0]["CodigoCategoria"].ToString());

            txtNomeProjeto.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
            txtNomeCategoria.Text = ds.Tables[0].Rows[0]["DescricaoCategoria"].ToString();

            string dtInicio = ds.Tables[0].Rows[0]["InicioProposta"].ToString();
            if ( "" != dtInicio )
                deInicioProposta.Date = DateTime.Parse(dtInicio);

            string dtTermino = ds.Tables[0].Rows[0]["TerminoProposta"].ToString();
            if ("" != dtTermino)
                deTerminoProposta.Date = DateTime.Parse(dtTermino);

            ddlUnidadeNegocio.Value = ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString();
            ddlGerenteProjeto.Value = ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString();
            if (ds.Tables[0].Rows[0]["CodigoMSProject"] + "" != "")
            {
                ddlAssoCroExistente.Value = ds.Tables[0].Rows[0]["CodigoMSProject"].ToString();
                rblCronograma.SelectedIndex = 0;
            }
            else if (ds.Tables[0].Rows[0]["CodigoMSProject"] + "" == "")
                rblCronograma.SelectedIndex = 1;
            
        }
    }

    #endregion

    #region Combos

    private void carregarCombos()
    {
        DataSet ds;

        //Unidades de negocios
        ds = cDados.getUnidade(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";

            ddlUnidadeNegocio.DataSource = ds.Tables[0];
            ddlUnidadeNegocio.DataBind();
        }

        //Gerente de projeto
        ds = cDados.getUsuarioUnidadeNegocioAtivo("");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlGerenteProjeto.Columns.Clear();
            ListBoxColumn lbc1 = new ListBoxColumn("NomeUsuario", "Nome", 200);
            ListBoxColumn lbc2 = new ListBoxColumn("EMail", "Email", 350);
            ListBoxColumn lbc3 = new ListBoxColumn("StatusUsuario", "Status", 80);

            ddlGerenteProjeto.Columns.Insert(0, lbc1);
            ddlGerenteProjeto.Columns.Insert(1, lbc2);
            ddlGerenteProjeto.Columns.Insert(2, lbc3);


            ddlGerenteProjeto.ValueField = "CodigoUsuario";
            ddlGerenteProjeto.TextField = "NomeUsuario";

            ddlGerenteProjeto.DataSource = ds.Tables[0];
            ddlGerenteProjeto.DataBind();
        }

        //Associar cronograma existente
        ds = cDados.getMSProjet(codigoProjetoSelecionado);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlAssoCroExistente.ValueField = "CodigoMSProject";
            ddlAssoCroExistente.TextField = "NomeProjeto";

            ddlAssoCroExistente.DataSource = ds.Tables[0];
            ddlAssoCroExistente.DataBind();
        }

    }

    #endregion

    #region Varios

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        string codigoMSprojet = "";

        if (rblCronograma.Items[rblCronograma.SelectedIndex].Value.ToString() == "ACE")
            codigoMSprojet = (ddlAssoCroExistente.Value != null) ? ddlAssoCroExistente.Value.ToString() : "NULL";
        else if (rblCronograma.Items[rblCronograma.SelectedIndex].Value.ToString() == "NCB")
            codigoMSprojet = "NULL";
        else if (rblCronograma.Items[rblCronograma.SelectedIndex].Value.ToString() == "NCM")
            codigoMSprojet = "NULL";
        
        // monta as datas no formato M/D/Y
        string dataInicio  = (deInicioProposta.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", deInicioProposta.Date));
        string dataTermino = (deTerminoProposta.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", deTerminoProposta.Date));
        string unidadeNegocio = (ddlUnidadeNegocio.Text.Equals("")) ? "NULL" : ddlUnidadeNegocio.Value.ToString();
        string gerenteProjeto = (ddlGerenteProjeto.Text.Equals("")) ? "NULL" : ddlGerenteProjeto.Value.ToString();

        bool resposta = cDados.atualizaDadosUnidadeProjeto(codigoProjetoSelecionado.ToString(),
                        unidadeNegocio, gerenteProjeto, codigoMSprojet, dataInicio, dataTermino);

        if (!resposta)
        {
            ClientScript.RegisterStartupScript(GetType(), "ok", "verificaSelecao(rblCronograma); window.top.mostraMensagem('Problema ao atualizar cadastro', 'erro', true, false, null);", true);
        }

    }

    #endregion
}
