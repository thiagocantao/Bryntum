/*
 OBSERVAÇÕES
 
 23/11/2010 - Mudanças: Foi anterado a função [private void populaListBox_UsuariosDisponiveis(int codigoObjetivoEstrategico)]
              altero a linha [DataSet ds = cDados.getPossiveisDestinatariosMensagem(iniciaisObjeto, codigoObjetivoEstrategico, idEntidadeLogada, "");]
                 antes tinha [getParticipantesEventos("EST", iniciaisObjeto, codigoProjeto.ToString(), codigoEntidade.ToString(), "");]
 31/01/2011 - Mudanças: Limitar ao select que popula a lista de disponivel sim o usuario que ta redigindo o mensagem.
                Altero o método [private void populaListBox_UsuariosDisponiveis(int codigoObjetivoEstrategico)].          
 
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
    private int codigoObjetivoEstrategico = -1;
    private int idEntidadeLogada = -1;
    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private string resolucaoCliente = "";
    private string iniciaisObjeto = "";

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
        onHeaderTela();
        
        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() + "" != "")
        {
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
            //codigoObjetivoEstrategico = 444;  // todo: alterar codigo do projeto para codigo do objetivo e buscar pessoas ligadas ao objetivo ao preencher os listBox de pessoas
        }

        /*"mensagensObjetivoEstrategico.aspx?TipoReuniao=E&MOD=EST&IOB=ME&COE=" + codigoObjetivoEstrategico + "&TA=OB";*/
        if (Request.QueryString["IOB"] != null && Request.QueryString["IOB"].ToString() + "" != "")
        {
            iniciaisObjeto = Request.QueryString["IOB"].ToString();
            //iniciaisObjeto = "OB";
        }

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        populaListBox_UsuariosDisponiveis(codigoObjetivoEstrategico);
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        object[] options = new object[lbDisponiveis.Items.Count];

        for (int i = 0; i < lbDisponiveis.Items.Count; i++)
            options[i] = lbDisponiveis.Items[i].Value;

        hiddenField["options"] = options;
    }

    private void onHeaderTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/mensagens.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "mensagens", "_Strings", "ASPxListbox"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Mensagens</title>"));
    }

    private void populaListBox_UsuariosDisponiveis(int codigoObjetivoEstrategico)
    {
        string where = "AND usu.CodigoUsuario <> " + idUsuarioLogado;
        DataSet ds = cDados.getPossiveisDestinatariosMensagem(iniciaisObjeto, codigoObjetivoEstrategico, idEntidadeLogada, where); //getParticipantesEventos("EST", iniciaisObjeto, codigoProjeto.ToString(), codigoEntidade.ToString(), "");
        //hfCount.Set("QuantidadeSelecionados", ds.Tables[0].Select("disponivel = 'N'").Length);
        //serão preenchidos com os usuarios disponiveis que nao sao destinatarios

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lbDisponiveis.DataSource = ds.Tables[0];
            lbDisponiveis.TextField = "NomeUsuario";
            lbDisponiveis.ValueField = "CodigoUsuario";
            lbDisponiveis.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        /*if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 80;*/
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        //para salvar a nova mensagens deve-se cadastrar a mensaggem na tabela:
        //primeiro cadastra na tabela mensagem com os dados da mensagem depois
        //na tabel mensagemdestinatario fazer um for na lista de usuários selecionados para aquela mensagem

        //cDados.inclui
        string msgErro = "";
        int[] listaUsuariosSelecionados = new int[lbSelecionados.Items.Count];
        DateTime auxDt = DateTime.MinValue;

        if (ckbRespondeMsg.Checked)
            auxDt = dtePrazo.Date;

        for (int i = 0; i < lbSelecionados.Items.Count; i++)
        {
            listaUsuariosSelecionados[i] = int.Parse(lbSelecionados.Items[i].Value.ToString());
        }
       
        //cDados.incluiMensagemProjeto(
        
        bool incluiu = cDados.incluiMensagemObjeto(idEntidadeLogada, codigoObjetivoEstrategico, idUsuarioLogado, "", txtMensagem.Text.Replace("'", ""), auxDt, ckbRespondeMsg.Checked, "", listaUsuariosSelecionados, iniciaisObjeto, ref msgErro);
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
