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

public partial class et_alteraSenha : System.Web.UI.Page
{
    public string alturaTabela;
    private string ResolucaoCliente;
    dados cDados;
    public string linhaDataNascimento = "none";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();

        string tituloPaginasWEB = System.Configuration.ConfigurationManager.AppSettings["nomeSistema"].ToString();

        alturaTabela = getAlturaTela();
        txtUsuario.Focus();

        Page.Title = tituloPaginasWEB;

        lblLicenciado.Text = Resources.traducao.et_alteraSenha_licenciado_para__ + System.Configuration.ConfigurationManager.AppSettings["nomeEmpresa"].ToString();
    }

    private string getAlturaTela()
    {
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 125).ToString();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx?RC=" + ResolucaoCliente);
    }

    protected void pnCallbackErro_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string msg = "";
        lblErro.ForeColor = Color.Red;
        cDados.setInfoSistema("IDUsuarioLogado", null);

        if (txtUsuario.Text != "" && txtSenhaAtual.Text != "")
        {
            int senhaCriptografa;
            /*tenta autenticar o usuário da rede no banco de dados*/
            string nomeUsuario = "";
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";
            int codigoUsuario;// = cDados.getAutenticacaoUsuario(txtUsuario.Text, txtSenhaAtual.Text.GetHashCode(), "AS", out nomeUsuario, out IDEstiloVisual);

            // Retira os caracteres utilizados em SQL Injection
            txtUsuario.Text = cDados.retiraCaracteresSqlInjection(txtUsuario.Text);

            // Tenta autenticar gerando a senha pelo metodo do .NET Framework - GetHashCode()
            senhaCriptografa = txtSenhaAtual.Text.GetHashCode();
            codigoUsuario = cDados.getAutenticacaoUsuario(txtUsuario.Text, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            // se não conseguiu autenticar, tenta pelo metodo onde a senha é gerada pelo metodo desenvolvido pela CDIS - ObtemCodigoHash()
            if (codigoUsuario == 0)
            {
                senhaCriptografa = cDados.ObtemCodigoHash(txtSenhaAtual.Text);
                codigoUsuario = cDados.getAutenticacaoUsuario(txtUsuario.Text, senhaCriptografa, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
            }


            // se é um usuário válido...
            if (codigoUsuario > 0)
            {
                
                senhaCriptografa = cDados.ObtemCodigoHash(txtNovaSenha.Text);
                if (!cDados.setNovaSenha(codigoUsuario, senhaCriptografa, "", ref msg))
                    lblErro.Text = msg;
                else
                {
                    

                    lblErro.ForeColor = Color.Green;
                    lblErro.Text = Resources.traducao.et_alteraSenha_senha_alterada_com_sucesso_;
                    
                }
            }
            else
            {
                lblErro.Text = Resources.traducao.et_alteraSenha_nome_de_usu_rio_ou_senha_inv_lidos_;
            }
        }
        else
        {
            string aviso = "";
            if (txtUsuario.Text == "" && txtSenhaAtual.Text == "")
            {
                aviso = Resources.traducao.et_alteraSenha_nome_de_usu_rio_e_senha_devem_ser_informados;
            }
            else if (txtUsuario.Text == "")
            {
                aviso = Resources.traducao.et_alteraSenha_nome_de_usu_rio_deve_ser_informado;
            }
            else
            {
                aviso = Resources.traducao.et_alteraSenha_senha_deve_ser_informada;
            }

            lblErro.Text = aviso;
        }
    }
}
