﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Portfolios_frameProposta_Resumo : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = 0;
    int idUsuarioLogado = 0;

    DataSet ds;

    int registrosAfetados = 0;
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

        codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {

            carregaComboStatus("");
            carregaComboCategoria("");
            carregaComboUnidade("");
            carregaComboGerente("");

            carregaCampos();
        }
        if (((cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S")) ||
               ((Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S")))
        {
            desabilitaBotoes(true);
        }
        else
        {
            desabilitaBotoes(false);
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PropostaResumo.js""></script>"));
        this.TH(this.TS("PropostaResumo"));
    }

    public void carregaCampos()
    {
        ds = cDados.getProposta("", codigoProjeto);

        txtTitulo.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
        txtDetalhes.Text = ds.Tables[0].Rows[0]["DescricaoProposta"].ToString();

        cmbStatus.Value = ds.Tables[0].Rows[0]["CodigoStatusProjeto"].ToString();
        cmbCategoria.Value = ds.Tables[0].Rows[0]["CodigoCategoria"].ToString();
        cmbUnidade.Value = ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString();
        cmbGerente.Value = ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString();

    }

    public void carregaComboStatus(string where)
    {
        ds = cDados.getStatus(where);

        cmbStatus.ValueField = "CodigoStatus";
        cmbStatus.TextField = "DescricaoStatus";

        cmbStatus.DataSource = ds.Tables[0];
        cmbStatus.DataBind();
    }

    public void carregaComboCategoria(string where)
    {
        ds = cDados.getCategoria(where);

        cmbCategoria.ValueField = "CodigoCategoria";
        cmbCategoria.TextField = "DescricaoCategoria";

        cmbCategoria.DataSource = ds.Tables[0];
        cmbCategoria.DataBind();

    }

    public void carregaComboUnidade(string where)
    {
        ds = cDados.getUnidade(where);

        cmbUnidade.ValueField = "CodigoUnidadeNegocio";
        cmbUnidade.TextField = "NomeUnidadeNegocio";

        cmbUnidade.DataSource = ds.Tables[0];
        cmbUnidade.DataBind();

    }

    public void carregaComboGerente(string where)
    {
        ds = cDados.getUsuarios(where);

        cmbGerente.ValueField = "CodigoUsuario";
        cmbGerente.TextField = "NomeUsuario";

        cmbGerente.DataSource = ds.Tables[0];
        cmbGerente.DataBind();

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        bool resposta = cDados.atualizaProposta(txtTitulo.Text, txtDetalhes.Text, idUsuarioLogado, int.Parse(cmbCategoria.Value.ToString()), int.Parse(cmbStatus.Value.ToString()), int.Parse(cmbGerente.Value.ToString()), int.Parse(cmbUnidade.Value.ToString()), codigoProjeto, ref registrosAfetados);

        if (!resposta)
        {
            ClientScript.RegisterStartupScript(GetType(), "ok", "window.top.mostraMensagem('Problema ao atualizar cadastro', 'erro', true, false, null);", true);
        }
    }

    private void desabilitaBotoes(bool desabilita)
    {
        txtTitulo.ClientEnabled = !desabilita;
        txtDetalhes.ClientEnabled = !desabilita;
        cmbStatus.ClientEnabled = !desabilita;
        cmbCategoria.ClientEnabled = !desabilita;
        cmbGerente.ClientEnabled = !desabilita;
        cmbUnidade.ClientEnabled = !desabilita;
        btnSalvar.ClientVisible = !desabilita;
    }
}
