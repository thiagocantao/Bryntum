/*
 OBSERVAÇÕES
 
 23/11/2010 - Mudanças: Foi anterado a função [private void populaListBox_UsuariosDisponiveis(int codigoObjetivoEstrategico)]
              altero a linha [DataSet ds = cDados.getPossiveisDestinatariosMensagem(iniciaisObjeto, codigoObjetivoEstrategico, idEntidadeLogada, "");]
                 antes tinha [getParticipantesEventos("EST", iniciaisObjeto, codigoProjeto.ToString(), codigoEntidade.ToString(), "");]
 
 
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

public partial class _Estrategias_objetivoEstrategico_novaMensagemObjetivoEstrategico : System.Web.UI.Page
{
    dados cDados;
    private int codigoIndicador = -1, codigoResponsavel = -1;
    private int idEntidadeLogada = -1;
    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private string resolucaoCliente = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            DateTime dataDefault = DateTime.Now.AddDays(1);

            dtePrazo.Date = dataDefault;
        }

        if (Request.QueryString["CI"] != null && Request.QueryString["CI"].ToString() + "" != "")
        {
            codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        }

        if (Request.QueryString["CRESP"] != null && Request.QueryString["CRESP"].ToString() + "" != "")
        {
            codigoResponsavel = int.Parse(Request.QueryString["CRESP"].ToString());
        }

        Header.Controls.Add(cDados.getLiteral(@"<title>Mensagens</title>"));

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);        
    }
        
    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);       
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        
        string msgErro = "";
        int[] listaUsuariosSelecionados = new int[1];
        DateTime auxDt = DateTime.MinValue;
        
        auxDt = dtePrazo.Date;

        listaUsuariosSelecionados[0] = codigoResponsavel;
       
        //cDados.incluiMensagemProjeto(
        
        bool incluiu = cDados.incluiMensagemObjeto(idEntidadeLogada, codigoIndicador, idUsuarioLogado, "", txtMensagem.Text.Replace("'", ""), auxDt, true, "", listaUsuariosSelecionados, "IN", ref msgErro);
        
        if (!incluiu)
        {
            string script = "<script type='text/javascript' language='javascript'>";
            script += Environment.NewLine + "window.top.mostraMensagem('Erro ao enviar a mensagem', 'erro', true, false, null);";
            script += Environment.NewLine + "window.top.fechaModal();";
            script += Environment.NewLine + "</script>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "client", script, false);
        }
        else
        {
            string script = "<script type='text/javascript' language='javascript'>";
            script += Environment.NewLine + "window.top.mostraMensagem('A mensagem foi enviada com sucesso!', 'sucesso', false, false, null);";
            script += Environment.NewLine + "window.top.retornoModal = 'S';";
            script += Environment.NewLine + "window.top.fechaModal();";
            script += Environment.NewLine + "</script>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "client", script, false);
        }
    }

    protected void pnData_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "false")
        {
            dtePrazo.ClientEnabled = false;
        }
        else
        {
            dtePrazo.ClientEnabled = true;
        }
    }
}
