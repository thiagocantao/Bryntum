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

public partial class espacoTrabalho_DetalhesTS : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string idAtribucao = "";
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CA"] != null)
            idAtribucao = Request.QueryString["CA"].ToString();

        try
        {
            int aux = int.Parse(idAtribucao);
        }
        catch {
            pcDados.TabPages[2].ClientVisible = false;   
        }

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack && !IsCallback)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            if (!IsCallback)
            {
                pcDados.ActiveTabIndex = 0;
                carregaDados(idAtribucao);
            }
        }

        string urlAnexos = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=TC&ID=" + idAtribucao + "&ALT=455";
        pcDados.JSProperties["cp_URLAnexos"] = urlAnexos;

        string urlComentarios = "../Tarefas/ComentariosTS.aspx?CA=" + idAtribucao + "&ALT=392";
        pcDados.JSProperties["cp_URLComentarios"] = urlComentarios;
                
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/DetalhesTS.js""></script>"));
        this.TH(this.TS("DetalhesTS"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 148);
        //mmComentariosAprovador.Height = altura;
    }

    private void carregaDados(string codigoAtribucao)
    {
        if (codigoAtribucao != "")
        {
            DataSet ds = cDados.getTarefasAtualizacaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, " AND CodigoAtribuicao = " + codigoAtribucao);

            if (cDados.DataSetOk(ds))
            {
                txtIndicaMarco.Text = "";
                if (cDados.DataTableOk(ds.Tables[0]))
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    txtProjeto.Text = dr["NomeProjeto"].ToString();
                    txtTarefa.Text = dr["NomeTarefa"].ToString();
                    txtTarefaSuperior.Text = dr["TarefaSuperior"].ToString();
                    txtAprovador.Text = ds.Tables[0].Rows[0]["Aprovador"].ToString();
                    txtInicioPrevisto.Value = dr["InicioPrevisto"];
                    txtTerminoPrevisto.Value = dr["TerminoPrevisto"];
                    txtTabalhoPrevisto.Value = dr["TrabalhoPrevisto"];
                    txtInicio.Value = dr["Inicio"];
                    txtTermino.Value = dr["Termino"];
                    txtTrabalho.Value = dr["Trabalho"];
                    txtIndicaMarco.Text = dr["IndicaTarefaMarco"].ToString();
                    mmAnotacoes.Text = dr["Anotacoes"].ToString(); //Anotacoes"].ToString();
                    
                    if (dr["InicioProjeto"].ToString() != "")
                    {

                        ddlInicioReal.MinDate = DateTime.Parse(dr["InicioProjeto"].ToString()).Date;
                        ddlTerminoReal.MinDate = DateTime.Parse(dr["InicioProjeto"].ToString()).Date;
                    }

                    if (dr["TerminoProjeto"].ToString() != "")
                    {
                        ddlInicioReal.MaxDate = DateTime.Parse(dr["TerminoProjeto"].ToString()).Date;
                        ddlTerminoReal.MaxDate = DateTime.Parse(dr["TerminoProjeto"].ToString()).Date;
                    }
                
                    if (dr["InicioReal"].ToString() != "")
                        ddlInicioReal.Date = Convert.ToDateTime(dr["InicioReal"].ToString());
                    if (dr["TerminoReal"].ToString() != "")
                        ddlTerminoReal.Date = Convert.ToDateTime(dr["TerminoReal"].ToString());

                    txtTrabalhoReal.Value = dr["TrabalhoRealInformado"];

                    if (dr["TrabalhoRestanteInformado"].ToString() == "")
                        txtTrabalhoRestante.Value = dr["Trabalho"];
                    else
                        txtTrabalhoRestante.Value = dr["TrabalhoRestanteInformado"];

                    txtPorcentaje.Value = dr["PercentualConcluido"];
                    mmComentariosRecurso.Text = dr["ComentariosRecurso"].ToString();
                }

                if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
                {
                    ddlInicioReal.ClientEnabled = false;
                    ddlTerminoReal.ClientEnabled = false;
                    txtTrabalhoReal.ClientEnabled = false;
                    txtTrabalhoRestante.ClientEnabled = false;
                    txtPorcentaje.ClientEnabled = false;
                    mmComentariosRecurso.ClientEnabled = false;
                    btnSalvar.ClientVisible = false;
                }

            }
        }
    }
    
    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";

        string msgErro = "";
        string inicioReal = (ddlInicioReal.Date == null || ddlInicioReal.Text == "") ? "NULL" : string.Format("{0:dd/MM/yyyy}", ddlInicioReal.Date);
        string terminoReal = (ddlTerminoReal.Date == null || ddlTerminoReal.Text == "") ? "NULL" : string.Format("{0:dd/MM/yyyy}", ddlTerminoReal.Date);

        bool retorno = cDados.atualizaTarefasUsuarioKanban(int.Parse(idAtribucao), txtTrabalhoRestante.Text, txtTrabalhoReal.Text, inicioReal, terminoReal, mmComentariosRecurso.Text, ref msgErro);

        if (retorno)
        {
            callBack.JSProperties["cp_OK"] = "Atribuição Atualizada com Sucesso!";
            carregaDados(idAtribucao);
        }
        else
        {
            callBack.JSProperties["cp_Erro"] = "Erro ao Atualizar a Atribuição";
        }
    }
}
