using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_popupAdicionaItemSprint : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int codigoProjetoAgil;
    private int codigoProjetoIteracao;
    private int CodigoUsuarioLogado;
    private int CodigoEntidade;
    private int codigoIteracao;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;
    public int alturaFrameAnexos = 372;
    public int alturaTelaUrl = 0;





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

        CodigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void headerOnTela()
    {
       // Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupAssociaItensBacklog.js""></script>"));
    }


}