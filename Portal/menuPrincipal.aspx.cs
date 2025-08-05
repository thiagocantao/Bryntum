using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class menuPrincipal : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;

    public string mostrarOpAgenda;
    public bool montarMenuPortfolio;
    public bool montarMenuProjeto;
    public bool montarMenuDemandas;
    public bool montarMenuEstrategia;
    public bool montarMenuOrcamento;
    public bool montarMenuAdministracao;
    public bool montarMenuEspacoTrabalho;
    public bool mostrarMenuProcessos;
    public string mostrarAcaoFavoritos = "none";
    public string imagemFundoCentro = "", imagemFundoEsquerdo = "", imagemFundoTopo = "";

    string lblEspacoTrabalho = "";
    public string posicaoMenu = "center";

    string iniciaisMenuAcessado = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        iniciaisMenuAcessado = Request.QueryString["IN"].ToString();
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        DataSet dsMenu = cDados.getMenuAcessoUsuario(codigoEntidadeLogada, codigoUsuarioLogado, "");

        criaMenuPrincipal(dsMenu.Tables[0]);
        cDados.aplicaEstiloVisual(this);
    }

    private void criaMenuPrincipal(DataTable dtMenu)
    {

        DataRow[] dr = dtMenu.Select("Iniciais = '" + iniciaisMenuAcessado + "'");

        if (dr.Length == 0)
        {
            lblEspacoTrabalho = "";
            return;
        }

        int codigoObjetoMenu = int.Parse(dr[0]["CodigoObjetoMenu"].ToString());

        dr = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoMenu);

        constroiSubMenus(dr, dtMenu, ref lblEspacoTrabalho);
    }

    private void constroiSubMenus(DataRow[] dr, DataTable dtMenu, ref string lblMenu)
    {

        for (int i = 0; i < dr.Length; i++)
        {
            int codigoObjetoSubMenu = int.Parse(dr[i]["CodigoObjetoMenu"].ToString());

            DataRow[] drSubMenu = dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoSubMenu, "OrdemObjeto");

            NavBarGroup subMenu = new NavBarGroup();

            if (i == 0)
            {
                subMenu = nav001.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 1)
            {
                subMenu = nav002.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 2)
            {
                subMenu = nav003.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 3)
            {
                subMenu = dr.Length == 4 ? nav002.Groups.Add(dr[i]["NomeMenu"].ToString()) : nav001.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 4)
            {
                subMenu = dr.Length == 5 ? nav003.Groups.Add(dr[i]["NomeMenu"].ToString()) : nav002.Groups.Add(dr[i]["NomeMenu"].ToString());
            }
            else if (i == 5)
            {
                subMenu = nav003.Groups.Add(dr[i]["NomeMenu"].ToString());
            }

            foreach (DataRow drItem in drSubMenu)
            {
                string urlMenu = drItem["URLObjetoMenu"].ToString();

                if (drItem["Iniciais"].ToString() == "PNL_GE")
                {
                    DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidadeLogada.ToString(), codigoUsuarioLogado.ToString());

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        urlMenu = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                    }
                }
                else if (drItem["Iniciais"].ToString() == "EMSOS ") // emissão de ordem de serviço
                {
                    // busca o código do workflow para criar uma nova Demanda
                    int codigoFluxo, codigoWorkflow;

                    cDados.getCodigoWfAtualFluxoEmissaoOS(codigoEntidadeLogada, out codigoFluxo, out codigoWorkflow);

                    if ((codigoFluxo != 0) && (codigoWorkflow != 0))
                        urlMenu = "~/wfEngine.aspx?CF=" + codigoFluxo.ToString() + "&CW=" + codigoWorkflow.ToString();
                }

                subMenu.Items.Add(drItem["NomeMenu"].ToString(), null, null, urlMenu.Replace("¥ENT", codigoEntidadeLogada.ToString()));
            }
        }
    }
}