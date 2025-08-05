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
using CDIS;

public partial class testaPlanoAcao : System.Web.UI.Page
{
    private ASPxGridView gvToDoList;
    dados cDados = new dados();
    int codigoEntidadeUsuarioResponsavel = 1;
    int codigoUsuarioLogado = 371;
    int idProjeto = 487;
    int codigoTipoAssociacao = 11;
    int codigoObjetoAssociado = 171;

    protected void Page_Load(object sender, EventArgs e)
    {

        Unit tamanho = new Unit("720px");
        bool somenteLeitura = false;
        int[] convidados = getParticipantesEvento();

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidadeUsuarioResponsavel, codigoUsuarioLogado, idProjeto, codigoTipoAssociacao, codigoObjetoAssociado, tamanho, 189, somenteLeitura, convidados.Length == 0 ? null : convidados,true, "");
        pcAbas.TabPages.FindByName("tabPageToDoList").Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario());
        gvToDoList = myPlanoDeAcao.gvToDoList;

        if (!IsCallback)
            gvToDoList.DataBind();
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), "");

        int[] convidados = new int[dsConvidados.Tables[0].Rows.Count];

        if (cDados.DataSetOk(dsConvidados))
        {
            int i = 0;
            foreach (DataRow dr in dsConvidados.Tables[0].Rows)
            {
                convidados[i] = int.Parse(dr["CodigoUsuario"].ToString());
                i++;
            }
        }

        return convidados;
    }
}
