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

public partial class formularios_frame_MenuEsquerdo : System.Web.UI.Page
{
    dados cDados;
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
        nbBarraLateral.Groups.FindByName("grFormularios").Visible = (populaItensFormulario());
        nbBarraLateral.Groups.FindByName("grProcessos").Visible = false;// = (populaItensProcesso());
    }

    private bool populaItensFormulario()
    {
        string comandoSQL = string.Format(
            @"SELECT MF.CodigoModeloFormulario, MF.NomeFormulario, MFTP.PreenchimentoObrigatorio
                FROM ModeloFormularioTipoProjeto MFTP inner join
                     ModeloFormulario MF ON (MFTP.codigoModeloFormulario = MF.codigoModeloFormulario)
               WHERE CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ')
                 AND IndicaModeloPublicado = 'S'
                 AND IndicaControladoSistema = 'N'
               ORDER BY MF.NomeFormulario "
            );

        DataTable dt = cDados.getDataSet(comandoSQL).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            NavBarItem item = new NavBarItem(dr["NomeFormulario"].ToString(), "ID_" + dr["CodigoModeloFormulario"], "", "~/formularios/RenderizaFormularios.aspx?CP=1" + "&CMF=" + dr["CodigoModeloFormulario"] + "&US=" + cDados.getInfoSistema("IDUsuarioLogado") + "&NF=" + dr["NomeFormulario"], "framePrincipal");
            nbBarraLateral.Groups[0].Items.Add(item);
        }
        return dt.Rows.Count > 0;
    }

    private bool populaItensProcesso()
    {
        string comandoSQL = string.Format(
            @"SELECT W.CodigoWorkflow, CAST(NULL AS VARCHAR(50) ) AS NomeWorkflow
                FROM WorkflowTipoProjeto WTP inner join
                     Workflows W ON (WTP.CodigoWorkflow = W.CodigoWorkflow)
               WHERE WTP.CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ')
               ORDER BY 2 "
            );

        DataTable dt = cDados.getDataSet(comandoSQL).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            NavBarItem item = new NavBarItem(dr["NomeWorkflow"].ToString(), "ID_" + dr["CodigoWorkflow"], "", "~/projeto_WorkFlows.aspx?CP=1" + "&US=1" + cDados.getInfoSistema("IDUsuarioLogado") + "&CW=" + dr["CodigoWorkflow"] + "&NW=" + dr["NomeWorkflow"], "framePrincipal");
            nbBarraLateral.Groups[1].Items.Add(item);
        }
        return dt.Rows.Count > 0;
    }

}
