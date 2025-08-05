using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class _PDA_SelecaoProdutosConsultor : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade;
    int idUsuarioLogado;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);
        string connectionString = cDados.classeDados.getStringConexao();        
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

        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
      
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        carregaGrid();

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
                BEGIN
                    DECLARE @CodigoRecursoCorporativo INT

                    SELECT @CodigoRecursoCorporativo = CodigoRecursoCorporativo
		              FROM RecursoCorporativo
	                 WHERE CodigoUsuario = {0}
                       AND CodigoEntidade = {1}
                
                   SELECT * FROM dbo.f_GetTarefasAtribuidasMinhasEquipes(@CodigoRecursoCorporativo)              

                END 
                ", idUsuarioLogado
                 , codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }
        
    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string comandoSQL = "", codigoAtribuicao = "", codigoEquipe = "";
        
        for (int i = 0; i < gvDados.GetSelectedFieldValues("CodigoAtribuicao").Count; i++)
        {
            codigoAtribuicao = gvDados.GetSelectedFieldValues("CodigoAtribuicao")[i].ToString();
            codigoEquipe = gvDados.GetSelectedFieldValues("CodigoEquipe")[i].ToString();

            comandoSQL += string.Format(@"                
                    EXEC dbo.p_RealizaDelegacaoAtribuicao {0}, {1}, @CodigoRecursoCorporativoDestino
                ", codigoAtribuicao
                 , codigoEquipe);
        } 

        if (comandoSQL != "")
        {
            comandoSQL = string.Format(@"
                BEGIN
                    DECLARE @CodigoRecursoCorporativoDestino INT

                    SELECT @CodigoRecursoCorporativoDestino = CodigoRecursoCorporativo
		              FROM RecursoCorporativo
	                 WHERE CodigoUsuario = {0}
                       AND CodigoEntidade = {2}
                
                   {1}               

                END 
                ", idUsuarioLogado
                 , comandoSQL
                 , codigoEntidade);

            int regAf = 0;

            bool result = cDados.execSQL(comandoSQL, ref regAf);

            carregaGrid();
        }
    }


}