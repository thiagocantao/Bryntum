using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using CDIS;

public partial class _VisaoMaster_PlanoAcaoIndicador : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoIndicador;
    private int codigoProjeto;
    private int codigoMeta;
    private ASPxGridView gvToDoList;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
            cDados.setInfoSistema("TipoEdicao", "I");
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoUsuarioResponsavel = int.Parse(hfDadosSessao.Get("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());

        codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        codigoMeta = int.Parse(Request.QueryString["CM"].ToString());

        hfGeral.Set("codigoObjetoAssociado", codigoMeta);
        hfGeral.Set("TipoOperacao", "Editar");

        carregaPlanoAcao();

        cDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void carregaPlanoAcao()
    {
        // inclui as funcionalidades do plano de ação.
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("MT");

        Unit tamanho = new Unit("100%");

        int[] convidados = getParticipantesEvento();

        pnPlanoAcao.Controls.Clear();

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigoProjeto, codigoTipoAssociacao, codigoMeta, tamanho, 385, false, convidados.Length == 0 ? null : convidados, true, "", 900);
        pnPlanoAcao.Controls.Add(myPlanoDeAcao.constroiInterfaceFormulario());
        gvToDoList = myPlanoDeAcao.gvToDoList;        
        gvToDoList.Font.Name = "Verdana";
        gvToDoList.Font.Size = new FontUnit("8pt");
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  '');}";
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