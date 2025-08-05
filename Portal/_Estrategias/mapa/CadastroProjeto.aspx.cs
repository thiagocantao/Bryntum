using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Estrategias_mapa_CadastroProjeto : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = -1;
    int altura = 640;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            altura = int.Parse(Request.QueryString["Altura"].ToString()) - 80;
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "" && int.Parse(Request.QueryString["CP"].ToString()) > 0)
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }
        else if (Request.QueryString["CF"] != null && Request.QueryString["CF"].ToString() != "" && int.Parse(Request.QueryString["CF"].ToString()) != 0)
        {
            string comandoSQL = string.Format(@"SELECT CodigoProject 
                                                  FROM {0}.{1}.FormularioProjeto 
                                                 WHERE CodigoFormulario = {2}", cDados.getDbName(), cDados.getDbOwner(), Request.QueryString["CF"].ToString());

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                codigoProjeto = int.Parse(ds.Tables[0].Rows[0]["CodigoProject"].ToString());
        }

        incluiFrameFormulario();

        cDados.aplicaEstiloVisual(Page);

        if (codigoProjeto == -1)
        {
            pcProjeto.TabPages[1].ClientEnabled = false;
        }
        else
        {
            pcProjeto.TabPages[1].ClientEnabled = true;
            incluiFrameAcoes();
        }
                
        if (Request.QueryString["ChamadaViaProjeto"] != null && Request.QueryString["ChamadaViaProjeto"].ToString() == "S")
        {
            btnFechar.ClientVisible = false;
            pcProjeto.ActiveTabIndex = 0;
        }
    }

    private void incluiFrameFormulario()
    {
        string paramProjeto = "";

        paramProjeto = "&CPWF=" + codigoProjeto + "&AT=" + (altura - 50);

        if (Request.QueryString["WSCR"] != null && Request.QueryString["WSCR"].ToString() != "")
        {
            paramProjeto += "&WSCR=" + (int.Parse(Request.QueryString["WSCR"].ToString()) - 80);
        }

        Literal controle;

        pcProjeto.TabPages[0].Controls.Clear();

        string frm = string.Format(@"<iframe id=""frmFormulario"" name=""frmFormulario"" frameborder=""0"" height=""{1}px"" 
                                        src=""../../wfRenderizaFormulario.aspx?INIMF=PE_CNI&FechaModalPosSalvar=N{0}""
                                            width=""100%""></iframe>", paramProjeto, altura);

        controle = cDados.getLiteral(frm);

        pcProjeto.TabPages[0].Controls.Add(controle);
    }

    private void incluiFrameAcoes()
    {
        Literal controle;

        pcProjeto.TabPages[1].Controls.Clear();

        string frm = string.Format(@"<iframe id=""frmAcoes"" name=""frmAcoes"" frameborder=""0"" height=""{1}px"" 
                                        src=""../../_Projetos/Administracao/ElementosEstrategia.aspx?CP={0}&Altura={2}""
                                            width=""100%""></iframe>", codigoProjeto, altura, altura - 110);

        controle = cDados.getLiteral(frm);

        pcProjeto.TabPages[1].Controls.Add(controle);

        pcProjeto.ActiveTabIndex = 1;
    }
}