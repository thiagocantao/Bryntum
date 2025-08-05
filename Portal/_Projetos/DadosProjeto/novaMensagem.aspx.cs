/*
 31/01/2011 - Alejandro : alteração do método [private void onHearderTela()], adiciono a linha para evitar uso de cache na tela.
 
 
 
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

public partial class _Projetos_DadosProjeto_novaMensagem : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private string resolucaoCliente = "";
    private int codigoEntidade = -1;
    private int codigoProjeto = -1;
    private int alturaPrincipal = 0;

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
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        onHearderTela();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            hfGeral.Set("CodigosDestinatariosSelecionados", "-1");
        }

        if (Request.QueryString["CP"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        populaListBox_UsuariosDisponiveis(codigoProjeto);
        insereGerenteProjetoList();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        object[] options = new object[lbDisponiveis.Items.Count];

        for (int i = 0; i < lbDisponiveis.Items.Count; i++)
            options[i] = lbDisponiveis.Items[i].Value;

        hiddenField["options"] = options;
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
    }

    private void onHearderTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/mensagens.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("mensagens", "_Strings", "ASPxListbox"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Mensagens</title>"));
    }

    private void insereGerenteProjetoList()
    {
        DataSet ds = cDados.getProjetos(" and P.CodigoProjeto = " + codigoProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DevExpress.Web.ListEditItem gestorEncontrado = lbSelecionados.Items.FindByText(ds.Tables[0].Rows[0]["Gerente"].ToString());

            if (gestorEncontrado == null)
            {
                lbSelecionados.Items.Add(new ListEditItem(ds.Tables[0].Rows[0]["Gerente"].ToString(), ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString()));

                DevExpress.Web.ListEditItem itemaRemover = new ListEditItem(ds.Tables[0].Rows[0]["Gerente"].ToString(), ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString());
                if (ds.Tables[0].Rows[0]["Gerente"] != null)
                    if (lbDisponiveis.Items.FindByText(ds.Tables[0].Rows[0]["Gerente"].ToString()) != null)
                        lbDisponiveis.Items.Remove(lbDisponiveis.Items.FindByText(ds.Tables[0].Rows[0]["Gerente"].ToString()));
            }
        }
    }

    private void populaListBox_UsuariosDisponiveis(int codigoProjeto)
    {
        string where = "AND U.CodigoUsuario <> " + idUsuarioLogado;
        DataSet ds = cDados.getUsuariosDisponiveis(codigoProjeto, "PR", where);
        hfCount.Set("QuantidadeSelecionados", ds.Tables[0].Select("disponivel = 'N'").Length);
        //serão preenchidos com os usuarios disponiveis que nao sao destinatarios
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lbDisponiveis.DataSource = ds.Tables[0];
            lbDisponiveis.TextField = "Nome";
            lbDisponiveis.ValueField = "Codigo";
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
        string[] listaDestinatario = null;

        int[] listaUsuariosSelecionados = new int[lbSelecionados.Items.Count];
        DateTime auxDt = DateTime.MinValue;

        if (ckbRespondeMsg.Checked)
            auxDt = dtePrazo.Date;

        //for (int i = 0; i < lbSelecionados.Items.Count; i++)
        //{
        //    listaUsuariosSelecionados[i] = int.Parse(lbSelecionados.Items[i].Value.ToString());
        //}

        if (hfGeral.Contains("CodigosDestinatariosSelecionados"))
            listaDestinatario = hfGeral.Get("CodigosDestinatariosSelecionados").ToString().Split(';');


        bool incluiu = cDados.incluiMensagemProjeto(codigoEntidade, codigoProjeto, idUsuarioLogado
                                                    , txtMensagem.Text.Replace("'",""), auxDt
                                                    , ckbRespondeMsg.Checked, listaDestinatario, "PR"
                                                    , ref msgErro);
        if (!incluiu)
        {
            string script = "<script type='text/javascript' language='javascript'>";
            script += Environment.NewLine + "window.top.mostraMensagem('Erro ao enviar a mensagem', 'erro', true, false, null);";
            script += Environment.NewLine + "window.parent.fechaModal();";
            script += Environment.NewLine + "</script>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "client", script, false);
        }
        else
        {
            string script = "<script type='text/javascript' language='javascript'>";
            script += Environment.NewLine + "window.top.mostraMensagem('A mensagem foi enviada com sucesso!', 'sucesso', false, false, null);";
            script += Environment.NewLine + "window.parent.fechaModal();";
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
