using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_GerenciamentoFluxos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

        }

        carregaComboFluxos();
        carregaComboVersoes();
        populaGrids();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        cDados.aplicaEstiloVisual(Page);

        gvEtapas.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvConectores.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/GerenciamentoFluxos.js""></script>"));
        this.TH(this.TS("GerenciamentoFluxos"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        int alturaGrids = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaGrids = (altura - 260) / 2;

        gvEtapas.Settings.VerticalScrollableHeight = alturaGrids;
        gvConectores.Settings.VerticalScrollableHeight = alturaGrids;
    }
    #endregion

    #region Combos

    private void carregaComboFluxos()
    {
        string comandoSQL = string.Format(@"
          SELECT DISTINCT f.CodigoFluxo, f.NomeFluxo, f.Descricao  -- (f.NomeFluxo, f.Descricao visíveis e com filtro)
            FROM Fluxos f INNER JOIN
                 Workflows w ON (w.CodigoFluxo = f.CodigoFluxo)
            WHERE (f.DataDesativacao IS NULL OR  f.DataReativacao > ISNULL(f.DataDesativacao, CAST('01/01/1900' AS DATETIME)))
            AND w.DataPublicacao IS NOT NULL
            AND f.CodigoEntidade = {0}
            ORDER BY f.NomeFluxo", codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            ddlFluxo.DataSource = ds;
            ddlFluxo.TextField = "NomeFluxo";
            ddlFluxo.ValueField = "CodigoFluxo";
            ddlFluxo.DataBind();
        }
    }

    private void carregaComboVersoes()
    {
        int codigoFluxo = ddlFluxo.SelectedIndex > -1 ? int.Parse(ddlFluxo.Value.ToString()) : -1;

        string comandoSQL = string.Format(@"
          SELECT w.CodigoWorkflow, f.CodigoFluxo, 
                 CASE WHEN ISNUMERIC(w.VersaoWorkflow) = 1 THEN 'v.'+REPLICATE(' ',3-LEN(RTRIM(LTRIM(w.VersaoWorkflow))))+RTRIM(LTRIM(w.VersaoWorkflow)) ELSE RTRIM(LTRIM(w.VersaoWorkflow)) END as VersaoWf,
                 w.DescricaoVersao, w.Observacao    -- (VersaoWf, w.DescricaoVersao, w.Observacao - não precisa filtro)
           FROM Fluxos f INNER JOIN
                Workflows w ON (w.CodigoFluxo = f.CodigoFluxo)
          WHERE (f.DataDesativacao IS NULL OR  f.DataReativacao > ISNULL(f.DataDesativacao, CAST('01/01/1900' AS DATETIME)))
            AND w.DataPublicacao IS NOT NULL
            AND f.CodigoEntidade = {0}
            AND f.CodigoFluxo = {1}
            ORDER BY 2, 3", codigoEntidadeUsuarioResponsavel, codigoFluxo);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlVersao.DataSource = ds;
            ddlVersao.TextField = "VersaoWf";
            ddlVersao.ValueField = "CodigoWorkflow";
            ddlVersao.DataBind();
        }
    }

    #endregion

    #region Grids

    private void populaGrids()
    {
        if (ddlVersao.SelectedIndex > -1)
        {
            int codigoWf = int.Parse(ddlVersao.Value.ToString());

            string comandoSQL = string.Format(@"
          EXEC dbo.p_wf_GetEtapasConectoresWorkflow {0}", codigoWf);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds))
            {
                gvEtapas.DataSource = ds.Tables[1];
                gvEtapas.DataBind();

                if (gvEtapas.FocusedRowIndex > -1)
                {
                    string etapa = gvEtapas.GetRowValues(gvEtapas.FocusedRowIndex, "id").ToString();
                    string where = "idEtapa = " + etapa + " OR idEtapaDestino = " + etapa;
                    DataView dv = new DataView(ds.Tables[3], where, "", DataViewRowState.CurrentRows);
                    gvConectores.DataSource = dv;
                }
                else
                    gvConectores.DataSource = ds.Tables[3];

                gvConectores.DataBind();
            }
        }
        else
        {
            gvEtapas.DataSource = null;
            gvEtapas.DataBind();

            gvConectores.DataSource = null;
            gvConectores.DataBind();
        }
    }

    protected void gvConectores_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if ((e.DataColumn.FieldName == "textoNotificacaoAcao" || e.DataColumn.FieldName == "textoNotificacaoAcompanhamento") && e.CellValue.ToString().Length > 60)
        {
            e.Cell.ToolTip = e.CellValue.ToString();
            e.Cell.Text = e.CellValue.ToString().Substring(0, 60) + "...";
        }
    }

    public string getEtapasConector()
    {
        string retorno = "";

        retorno = string.Format(@"<table cellspacing=""0"" style=""font - family: verdana; font - size: 8pt; "">
                                                                                 <tr>
                                                                                     <td style = ""padding-right: 5px"" > Conector </td>
                                                                                      <td style = ""padding-right: 2px"" >{0}</td>
                                                                                       <td style = ""padding-right: 2px"" >
                                                                                            <img alt = """" class=""auto-style2"" height=""15px"" src=""../imagens/setaDireita.png"" width=""15px"" /></td>
                                                                                <td>{1}</td>
                                                                            </tr>
                                                                        </table>", Eval("idEtapa"), Eval("idEtapaDestino"));

        return retorno;
    }

    #endregion
}