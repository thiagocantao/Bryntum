using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_chaveAcessoExternoAPI : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    protected void Page_Init(object sender, EventArgs e)
    {
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

        this.TH(this.TS("chaveAcessoExternoAPI"));

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(false, "Cadastro De Chave Acesso Externo", "CADCHA", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        Header.Controls.Add(cDados.getLiteral(@" <script src=""../scripts/chaveAcessoExternoAPI.js""></script>"));

        string select = string.Format(@" select CodigoUsuario, NomeUsuario, EMail, IndicaUsuarioCredenciado, Client_ID
                                           from dbo.f_brk_GetUsuariosEntidadeCredenciamentoAcessoViaAPI ({0}, {1}) order by 2 asc", codigoEntidadeLogada, codigoUsuarioLogado);
        DataSet dsGrid = cDados.getDataSet(select);
        if (cDados.DataSetOk(dsGrid))
        {
            gvDados.DataSource = dsGrid;
            gvDados.DataBind();
        }
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;


    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0)
        {

            string indicaUsuarioCredenciado = gvDados.GetRowValues(e.VisibleIndex, "IndicaUsuarioCredenciado") + ""; // linha alterada
 
            if (e.ButtonID.Equals("btnChaveAcesso"))
            {
                if (indicaUsuarioCredenciado.ToUpper().Trim() == "S")
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    //e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnSelecionarOpcoes"))
            {
                if (indicaUsuarioCredenciado.ToUpper().Trim() == "S")
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    //e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
        }
    }

    public string getCheckBox()
    {
        string retorno = "";
        string indicaChecado = Eval("IndicaUsuarioCredenciado").ToString().ToUpper().Trim() == "N" ? "" : "checked='CHECKED'";

        string client_id = Eval("Client_ID").ToString();
        retorno = @"<input type='checkbox' id='check_" + Eval("CodigoUsuario").ToString() + "'" + " onclick='credenciarUsuario(" + Eval("CodigoUsuario") + "," + @"""" + client_id + @"""" + ", this.checked)' " + indicaChecado + " />";

        return retorno;
    }

    protected void callbackCredencia_Callback(object source, CallbackEventArgs e)
    {
        var codigoUsuarioSelecionado = e.Parameter.Split('|')[0];
        var client_id = e.Parameter.Split('|')[1];

        string client_secret = "";
        int client_secret_ComHashCode = -1;

        string comando = "exec dbo.p_geraGUID";
        DataSet ds = cDados.getDataSet(comando);
        client_secret = ds.Tables[0].Rows[0][0].ToString();
        client_secret_ComHashCode = client_secret.GetHashCode();

        string comandoSQLCredencia = string.Format(@"[dbo].[p_credenciaUsuarioAcessoViaAPI]
         @codigoEntidade = {0}
        , @codigoUsuarioCredenciado   = {1}
        , @usuarioCredenciadorAcesso  = {2}
        , @client_secret              = {3}", codigoEntidadeLogada, codigoUsuarioSelecionado, codigoUsuarioLogado, client_secret_ComHashCode);

        string retorno_proc_Client_ID = "";
        DataSet ds1 = cDados.getDataSet(comandoSQLCredencia);
        retorno_proc_Client_ID = ds1.Tables[0].Rows[0][0].ToString();

        ((ASPxCallback)source).JSProperties["cpRetornoClientID"] = retorno_proc_Client_ID;
        ((ASPxCallback)source).JSProperties["cpRetornoClientSecret"] = client_secret;
        ((ASPxCallback)source).JSProperties["cpCodigoUsuarioSelecionado"] = codigoUsuarioSelecionado;
    }

    protected void callbackDescredencia_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        string comandoSQL = string.Format(@"
        DELETE FROM CredencialAcessoExterno where CodigoUsuario = {0};
         UPDATE [UsuarioFederacaoIdentidade] 
            SET IndicaAssociacaoAtiva = 'N', 
                DataUltimaDesassociacao = GETDATE() 
          WHERE CodigoUsuario = {0} 
            AND SiglaFederacaoIdentidade = 'brisk' ", e.Parameter);
        int regAfetados = -1;
        bool retorno = false;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if(retorno == true)
            {
                if(regAfetados > 0)
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Usuário descredenciado com sucesso!";
                }
                else
                {
                    ((ASPxCallback)source).JSProperties["cpErro"] = "Nenhum usuário foi descredenciado!";
                }
            }
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }


}