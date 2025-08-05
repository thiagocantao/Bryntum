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

public partial class avisoDesconexao : System.Web.UI.Page
{
    public string caminho;
    public string target;
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        string usuarioRede = User.Identity.Name.ToString();

        //usuarioRede = "Sistema-cni\\t-ecantao";

        if (usuarioRede == "")
        {
            caminho = "../login.aspx";
        }
        else
        {

            /*tenta autenticar o usuário da rede no banco de dados*/
            string nomeUsuario = "";
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";

            int codigoUsuario = cDados.getAutenticacaoUsuario(usuarioRede, null, "AI", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            if (codigoUsuario > 0)
                caminho = "../po_autentica.aspx";
            else
                caminho = "../login.aspx";
        }

        target = "_top";

        this.TH(this.TS("avisoDesconexao"));
        string mensagem = "";
        switch (Request.QueryString["T"] + "")
        {
            //licenseIssue_sce
            case "li":
                mensagem = Resources.traducao.avisoDesconexao_todas_as_licen_as_contratadas_para_a_sua_entidade_padr_o_j__est_o_em_uso_no_momento__tente_novamente_mais_tarde_ou_entre_em_contato_com_o_administrador_do_sistema;
                break;

            //disconnectByDoubleConnection
            case "ddc":
                mensagem = Resources.traducao.avisoDesconexao_um_acesso_com_este_usu_rio_foi_feito_em_outro_dispositivo__esta_sess_o_foi_desconectada;
                break;

            //disconnectRequested
            case "dr":
                mensagem = Resources.traducao.avisoDesconexao_esta_sess_o_foi_desconectada;
                break;
            default:
                break;
        }

        objetoMensagemErroInatividade.Text = mensagem;

        HeaderOnTela();
    }

    private void HeaderOnTela()
    {

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<style>
        .box-erro{
            display:flex;
            justify-content:center;
            margin:3rem 0rem;
            padding: 1rem;
            color: #004085;
            background-color: #cce5ff;
           border: 1px solid #a4cffb;
            border-radius: 6px;
            font-family: Verdana, sans-serif;
            font-size:16px;
            line-height: 180%;
        }

        .box-erro a{
           color: #ffffff;
            background: #7bbafb;
            padding: 8px;
            border-radius: 6px;
            text-decoration: none;
            font-size: 14px;
        }

    </style>"));
        //this.TH(this.TS("barraNavegacao", "RecursosCorporativos"));



        // Esconde os controles da MasterPage com base na página atual
        if (this.Master != null)
        {
            HtmlGenericControl menu = (HtmlGenericControl)this.Master.FindControl("menuPrincipalSuperior");
            if (menu != null)
            {
                menu.Visible = false;
            }
            menu = (HtmlGenericControl)this.Master.FindControl("rastroPrincipal");
            menu.Visible = false;

        }
    }

}
