using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_agil_HistoricoItem : System.Web.UI.Page
{
    public int CodigoItem = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        CodigoItem = int.Parse(Request.QueryString["CI"].ToString());
        hfGeral.Set("CodigoItem", CodigoItem);
    }

}