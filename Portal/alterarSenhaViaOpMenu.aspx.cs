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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class alterarSenhaViaOpMenu : System.Web.UI.Page
{
    public string alturaTabela;
    private string ResolucaoCliente;
    dados cDados;
    int codigoUsuario;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

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

        ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack && !IsCallback)
        {
            DataSet dsUsuario = cDados.getDadosResumidosUsuario(" AND us.codigoUsuario = " + codigoUsuario);
            if (cDados.DataSetOk(dsUsuario) && cDados.DataTableOk(dsUsuario.Tables[0]))
                lblUsuario.Text = dsUsuario.Tables[0].Rows[0]["EMail"].ToString();
        }

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "CVCLIEN", "ENT", -1, Resources.traducao.alterarSenhaViaOpMenu_adicionar_aos_favoritos);
            painelLogin.HeaderText = lblTituloTela.Text;
        }


        string tituloPaginasWEB = System.Configuration.ConfigurationManager.AppSettings["nomeSistema"].ToString();

        alturaTabela = getAlturaTela();
        txtSenhaAtual.Focus();

        Page.Title = tituloPaginasWEB;

        lblLicenciado.Text = Resources.traducao.alterarSenhaViaOpMenu_licenciado_para_ + System.Configuration.ConfigurationManager.AppSettings["nomeEmpresa"].ToString();
    }

    private string getAlturaTela()
    {
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 125).ToString();
    }




    protected void pnCallbackErro_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string msg = "";
        lblErro.ForeColor = Color.Red;
        pnCallbackErro.JSProperties["cp_Msg"] = "";
        pnCallbackErro.JSProperties["cp_status"] = "";

        if (lblUsuario.Text != "" && txtSenhaAtual.Text != "" && txtNovaSenha.Text.Trim() != "")
        {
            if (txtNovaSenha.Text == txtConfNovaSenha.Text)
            {
                int senhaCriptografa;
                /*tenta autenticar o usuário da rede no banco de dados*/
                string nomeUsuario = "";
                string IDEstiloVisual = "";
                string usuarioDeveAlterarSenha = "";

                DataSet dsUsuario = cDados.getDadosResumidosUsuario(" AND us.codigoUsuario = " + codigoUsuario);
                string usuario = dsUsuario.Tables[0].Rows[0]["EMail"].ToString();

                // Tenta autenticar gerando a senha pelo metodo do .NET Framework - GetHashCode()
                senhaCriptografa = txtSenhaAtual.Text.GetHashCode();
                int codigoUsuarioLocal = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

                // se não conseguiu autenticar, tenta pelo metodo onde a senha é gerada pelo metodo desenvolvido pela CDIS - ObtemCodigoHash()
                if (codigoUsuarioLocal == 0)
                {
                    senhaCriptografa = cDados.ObtemCodigoHash(txtSenhaAtual.Text);
                    codigoUsuarioLocal = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
                }


                // se é um usuário válido...
                if (codigoUsuarioLocal > 0)
                {
                    senhaCriptografa = cDados.ObtemCodigoHash(txtNovaSenha.Text);
                    if (!cDados.setNovaSenha(codigoUsuarioLocal, senhaCriptografa, "", ref msg))
                        lblErro.Text = msg;
                    else
                    {
                        cDados.setInfoSistema("IDUsuarioLogado", null);
                        //cDados.alerta(this, "Senha alterada com sucesso!");
                        pnCallbackErro.JSProperties["cp_Msg"] = Resources.traducao.alterarSenhaViaOpMenu_senha_alterada_com_sucesso_;
                        pnCallbackErro.JSProperties["cp_status"] = "ok";
                        //Response.Redirect("login.aspx?RC=" + ResolucaoCliente);
                    }
                }
                else
                {
                    lblErro.Text = Resources.traducao.alterarSenhaViaOpMenu_nome_de_usu_rio_ou_senha_inv_lidos_;
                }
            }
            else
            {
                lblErro.Text = Resources.traducao.alterarSenhaViaOpMenu_as_senhas_n_o_coincidem;
            }
        }
        else
        {
            string aviso = "";
            if (lblUsuario.Text == "" && txtSenhaAtual.Text == "")
            {
                aviso = Resources.traducao.alterarSenhaViaOpMenu_nome_de_usu_rio_e_senha_devem_ser_informados;
            }
            else if (lblUsuario.Text == "")
            {
                aviso = Resources.traducao.alterarSenhaViaOpMenu_nome_de_usu_rio_deve_ser_informado;
            }
            else
            {
                aviso = Resources.traducao.alterarSenhaViaOpMenu_senha_deve_ser_informada;
            }

            lblErro.Text = aviso;
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx?RC=" + ResolucaoCliente);
    }
}
