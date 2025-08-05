using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;

public partial class _Projetos_Administracao_LicoesAprendidas_popupLicoesAprendidas : System.Web.UI.Page
{
    private dados cDados;
    private int _codigoProjeto;
    private int codigoEntidadeContexto;
    /// <summary>
    /// Código do projeto vinculado a proposta de iniciativa. Caso seja igual a '-1' trata-se de uma nova proposta de iniciativa
    /// </summary>
    public int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            cDados.setInfoSistema("CodigoProjeto", value); // variável de sessão usada no fluxo
            Session["codigoProjeto"] = value;
            _codigoProjeto = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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

        string cp = Request.QueryString["CP"];
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
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

        codigoEntidadeContexto = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaLicoesAprendidas();
        cDados.aplicaEstiloVisual(this.Page);
        gvLicoesAprendidas.Settings.ShowFilterRow = false;
    }

    private void carregaLicoesAprendidas()
    {
        DataSet dsLicaoAprendida = new DataSet();

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidade int
        DECLARE @in_codigoProjeto int
        DECLARE @in_situacaoLicaoAprendida varchar(15)

        SET @in_codigoEntidade = {0}
        SET @in_codigoProjeto = {1}
        SET @in_situacaoLicaoAprendida = 'DISPONIVEL'

         EXECUTE @RC = [dbo].[p_lca_listaLicoesAprendidas] 
         @in_codigoEntidade,@in_codigoProjeto,@in_situacaoLicaoAprendida", codigoEntidadeContexto, CodigoProjeto);
        dsLicaoAprendida = cDados.getDataSet(comandosql);

        gvLicoesAprendidas.DataSource = dsLicaoAprendida;
        gvLicoesAprendidas.DataBind();
    }

    public string retornaEstiloLinha(string seq, bool indicaColunaCheck)
    {        
        string retorno = "";
        string trataColunaCheck = string.Format(@"{0}%;", indicaColunaCheck ? "2" : "12.25");
        int seq1 = 0;
        if (int.TryParse(seq, out seq1) == true && seq1 % 2 == 0)
        {
            retorno = @"style='width: " + trataColunaCheck + " border:solid; background-color:gainsboro; border-width:1px;border-color:gainsboro;font-family:Roboto Regular;font-size:10pt'";
        }
        else
        {
            retorno = @"style='width: " + trataColunaCheck + " border:solid; background-color:white; border-width:1px;border-color:gainsboro;font-family:Roboto Regular;font-size:10pt'";
        }
        return retorno;
    }

    protected void menu_gvLicoesAprendidas_Init(object sender, EventArgs e)
    {

    }

    protected void repeater_Init(object sender, EventArgs e)
    {
        
    }

}