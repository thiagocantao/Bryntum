using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDIS;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using System.Collections.Specialized;

public partial class Relatorios_GeradorDashboard_EditorDashboard: BasePageBrisk
{
    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        frameEditorDashboard.Style.Add("height", (TelaAltura - 125).ToString() + "px");
        //frameEditorDashboard.Style.Add("width", (TelaLargura).ToString() + "px");
        frameEditorDashboard.Src = "frame_EditorDashboard.aspx?id=" + Request.QueryString["id"];
    }
}