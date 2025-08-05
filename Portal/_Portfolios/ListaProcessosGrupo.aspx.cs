/*
 OBSERVAÇÕES
 
 06/01/2011 - MUDANÇA by ALEJANDRO.
 Control do menú:   Tempo Escopo / Editar EAP
                    Control de acceso, segundo si tem o não permiso para editar o cronograma.
                    Alteração no método-> private void carregaMenuTempoEscopo(DataSet ds){...}
 */
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

public partial class _Projetos_DadosProjeto_indexResumoProjeto : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;
    private int codigoEntidadeLogada;
    private int codigoUsuarioResponsavel;
    public string alturaTabela;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);

        dbOwner = cDados.getDbOwner();
        dbName = cDados.getDbName();

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

        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {
            carregaMenuLateral();
        }
        cDados.aplicaEstiloVisual(this);
        cDados.aplicaEstiloVisual(nvbMenuFluxos);
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "FLUXOS", "PROC", -1, Resources.traducao.ListaProcessosGrupo_adicionar_a_lista_aos_favoritos);
        }


        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        sp_Tela.Height = alturaTela - 200;
        return (alturaTela - 200).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        sp_Tela.Panes[1].Size = new Unit(largura - 165);
        return largura - 140;
    }

    #region Menu Lateral

    private void carregaMenuLateral()
    {
        int count = 0;
        DataSet dsGrupos = cDados.getGruposFluxos(codigoEntidadeLogada);

        nvbMenuFluxos.Groups.Clear();
        bool mostraPrimeiroFluxo = true;

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeLogada, "MostrarPrimeiroGrupoFluxo");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            mostraPrimeiroFluxo = dsParam.Tables[0].Rows[0]["MostrarPrimeiroGrupoFluxo"] + "" == "S";

        if ((cDados.DataSetOk(dsGrupos)) && (cDados.DataTableOk(dsGrupos.Tables[0])))
        {
            DataRow[] drGrupos = dsGrupos.Tables[0].Select("", "OrdemGrupoMenu");

            foreach (DataRow drG in drGrupos)
            {
                NavBarGroup nbg = new NavBarGroup(drG["DescricaoGrupoFluxo"].ToString(), drG["IniciaisGrupoFluxo"].ToString());
                nvbMenuFluxos.Groups.Add(nbg);

                DataSet dsFluxos = cDados.getListaFluxosGrupo(codigoEntidadeLogada, int.Parse(drG["CodigoGrupoFluxo"].ToString()));

                foreach (DataRow drI in dsFluxos.Tables[0].Rows)
                {
                    string alturaFrameForms = getAlturaTela();
                    int largura = getLarguraTela();
                    string larguraFrameForms = largura.ToString();
                    string idItem = "op_" + drI["CodigoFluxo"];
                    string urlItem = "./listaProcessosInterno.aspx?CF=" + drI["CodigoFluxo"] + "&NomeFluxo=" + Server.UrlEncode(drI["NomeFluxo"].ToString()) + "&TL=CHI";

                    NavBarItem nbi = nbg.Items.Add(drI["NomeFluxo"].ToString(), idItem, "", urlItem, "framePrincipal");
                    nbi.ToolTip = drI["Descricao"].ToString();

                    if (count == 0)
                    {
                        sp_Tela.Panes[1].ContentUrl = mostraPrimeiroFluxo ? urlItem : "../branco.htm";
                    }

                    count++;
                }

                if (nbg.Items.Count == 0)
                    nbg.ClientVisible = false;
            }
        }

        nvbMenuFluxos.AutoCollapse = true;
    }

    #endregion   
}

