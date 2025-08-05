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
using System.Text;

public partial class _Portfolios_framePropostas : System.Web.UI.Page
{
    public string alturaTabela;
    public string parametrosURL;
    dados cDados;

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

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/framePropostas.js""></script>"));
        this.TH(this.TS("framePropostas"));

        alturaTabela = getAlturaTela()+"px";

        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoProjeto;

        if (Request.QueryString["CP"] != null && Request.QueryString["DesabilitarBotoes"] + "" == "S")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            cDados.setInfoSistema("CodigoProjeto", codigoProjeto);
            cDados.setInfoSistema("DesabilitarBotoes", "S");
        }
        else
        {
            codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
            cDados.setInfoSistema("DesabilitarBotoes", "N");
        }
        

        DataSet ds = cDados.getPropostas(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, " AND Projeto.CodigoProjeto = " + codigoProjeto);

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            lblTituloProposta.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 135).ToString();
    }
}
