/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 
 * 01/03/2011 :: Alejandro : -Alteração no método 'carregaComboFontePagadora()', aonde agora indicara o código da
 *                          entidade logada para filtrar as fontes pagadoras correspondientes.
 *                          -Alteração do desenho da grid para obter o padron.
 * 
 * 17/03/2011 :: Alejandro : adiciono el botão de Permissãos para os contratos.
 * 21/03/2011 :: Alejandro : adiciono el control de acesso para o botão de permissões [CT_AdmPrs].
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
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Collections.Generic;

public partial class _Projetos_DadosProjeto_Contratos : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela;
    public string urlContratos = "";
    
    private int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        defineLarguraTela();
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   

        urlContratos = "frmContratosNovo.aspx?" + Request.QueryString.ToString();

        DataSet dsAux = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "UtilizaContratosExtendidos");

        if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]) && dsAux.Tables[0].Rows[0]["UtilizaContratosExtendidos"].ToString() == "S")
            urlContratos = "frmContratosExtendido.aspx?" + Request.QueryString.ToString();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Contratos", "CONTRA", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
        
        this.Title = cDados.getNomeSistema();

        //ASPxPanel p = Master.FindControl("pnMaster") as ASPxPanel;
        //if (p != null)
        //{
        //    p.ClientVisible = false;
        //}  
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 145).ToString() + "px";
    }
}
