/*
 OBSERVAÇÕES
 
 MODIFICAÇÕES
 - 17/02/2011 : - Usuario cadastrado como 'Super Usuario' não pode ser excluido, lançar um mensagem falando que deve alterar
                antes de efetuar a exclusão.
                - Usuario utilizado como 'Recurso Corporativo'. não pode ser excluido.
 
                se crearon variaveis para tratar as situações indicadas ao intentar excluir o usuario;
                no método [protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e){...}] se 
                crio a sigente variaveis:
                                            pnCallback.JSProperties["cp_EsSuperUsuario"] = "";
                                            pnCallback.JSProperties["cp_EsRecursoCorporativo"] = "";
                Aonde ficara salvo 'OK' caso que cumpla a condição o usuario (condição de ser o não SuperoUsuario o RecursoCorporativo).
                Se trato istas variaveis no cliente em [function onEnd_pnCallbackLocal(s, e){...} (CadastroUsuariosNovo.js)] ande
                personaliça a saida do mensagem para o usuario.
 * 
 * 16/03/2011 :: Alejandro  : - Tratamento de acceso a tela [US_Cns].
 * 25/03/2011 :: Alejandro  : - Inclusão do comboBox para seleção de tipo de perfil inicial.
 * 29/03/2011 :: Géter      : - CORREÇÃO DE ERRO!!! 
 *                              Na inclusão de um usuário de outra entidade, a tela não permitia selecionar o perfil 
 *                              e nem gravava o registro exigindo que se selecionasse.
 *                            - Usando uma única verificação de permissão para ganho de performance já que as 3 operações
 *                              (alterar, incluir e excluir) estão baseadas na mesma permissão [US_Cad])
 * 06/05/2011 :: Antônio    : - Inclusão da variável "novaSenhaUsuarioCriptografada" no método "persisteInclusaoRegistro".
 *                              A senha passou a ser criptografada pelo metodo criado pela CDIS.
 * 01/11/2011 :: Eduardo    : - Tradução da grid através da classe CDIS_GridLocalizer ao invés da classe 
 *                              implementada neste arquivo que foi deletada.
 *                              
 * 22/10/2014 :: Amauri     : - Permitir alteração no e-mail.
 * 
 * 04/11/2014 :: Antonio    : - Inclusão do tipo de autenticaçao "AP - Autenticação própria (LDAP/AD/Outros)"
 * 
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
using System.Drawing;
using DevExpress.Web;
using System.Security.Principal;
using System.Collections.Generic;
using DevExpress.Utils.Localization.Internal;
using DevExpress.Web.Localization;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraPrinting;

public partial class administracao_CadastroUsuariosNovo : System.Web.UI.Page
{
    dados cDados;
    DataSet dsEmail;

    static bool editaCPFUsuario;

    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private int CodigoEntidade;

    private string dbName;
    private string dbOwner;

    private string nomeUnidadeParaEmail;

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool podeCopiarPermissoes = false;
    public bool emailAlterado = false;

    public string usuarioDestino = "";
    public string usuarioOrigem = "";
    public static DataTable dtImpedimentosGlobal;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);        
            
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.
        nomeUnidadeParaEmail = getDadosUnidade(CodigoEntidade.ToString());

        hfGeral.Set("CodigoEntidadeAtual", CodigoEntidade); // p/ controle de edição dos usuários listados
        hfGeral.Set("idUsuarioLogado", idUsuarioLogado);

        dsDados.ConnectionString = cDados.classeDados.getStringConexao();
        podeCopiarPermissoes = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_CopPer");
        podeIncluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_Cad");
        podeEditar = podeIncluir;
        podeExcluir = podeIncluir;

        //if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_Cad"))
        //    podeIncluir = true;
        //if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_Cad"))
        //    podeEditar = true;
        //if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "US_Cad"))
        //    podeExcluir = true;

        this.Title = cDados.getNomeSistema();


        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        HeaderOnTela();
        this.TH(this.TS("CadastroUsuariosNovo", "ASPxListbox", "barraNavegacao"));


        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {

            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "US_Cns");

            setLabelTela();

            hfGeral.Set("TipoOperacao", "");
            CDIS_GridLocalizer.Activate();
        }

        populaGrid();
        pnCallback.JSProperties["cp_Mostrar"] = "N";

        cDados.setaTamanhoMaximoMemo(memObservacoes, 250, lblContadorMemo);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Usuários", "CADUSU", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);

            populaCmbPerfil();
            populaCmbTipoAutentica();
        }

        ddlUsuarioOrigem.Columns[0].FieldName = "NomeUsuario";
        ddlUsuarioOrigem.Columns[1].FieldName = "EMail";
        ddlUsuarioOrigem.TextFormatString = "{0}";

        gvImpedimentos.DataSource = dtImpedimentosGlobal;
        gvImpedimentos.DataBind();

    }

    #region GRID

    private void populaGrid()
    {
        string where = " AND us.DataExclusao IS NULL ";
        dsDados.SelectCommand = cDados.getSelect_Usuario(CodigoEntidade, where);
        gvDados.DataBind();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        //if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "ALTUSU"))
        //    podeEditar = true;
        //if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EXCUSU"))
        //    podeExcluir = true;

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        if (e.ButtonID == "btnCopiaPermissoes")
        {
            if (!podeCopiarPermissoes)
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            string usuarioAtivo = e.GetValue("IndicaUsuarioAtivoUnidadeNegocio").ToString();

            if (usuarioAtivo == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    protected void gvImpedimentos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();
        if (!pnCallback.JSProperties.ContainsKey("cp_corUN"))
            pnCallback.JSProperties["cp_corUN"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corPR"))
            pnCallback.JSProperties["cp_corPR"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corIN"))
            pnCallback.JSProperties["cp_corIN"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corIO"))
            pnCallback.JSProperties["cp_corIO"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corDE"))
            pnCallback.JSProperties["cp_corDE"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corTD"))
            pnCallback.JSProperties["cp_corTD"] = "";

        if (e.RowType == GridViewRowType.Data)
        {
            string usuarioAtivo = e.GetValue("codImpedimento").ToString();

            if (usuarioAtivo == "UN")
            {
                int ri = Int32.Parse("98", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("FB", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("98", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corUN"] = "S";
            }
            if (usuarioAtivo == "PR")
            {
                int ri = Int32.Parse("B0", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("C4", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("DE", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corPR"] = "S";
            }
            if (usuarioAtivo == "IN" || usuarioAtivo == "IO")
            {
                int ri = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corIN"] = "S";
            }
            //if (usuarioAtivo == "IO")
            //{
            //    int ri = Int32.Parse("D8", System.Globalization.NumberStyles.HexNumber);
            //    int gi = Int32.Parse("BF", System.Globalization.NumberStyles.HexNumber);
            //    int bi = Int32.Parse("D8", System.Globalization.NumberStyles.HexNumber);

            //    color = Color.FromArgb(ri, gi, bi);
            //    e.Row.BackColor = color;

            //    pnCallback.JSProperties["cp_corIO"] = "S";
            //}
            if (usuarioAtivo == "DE")
            {
                int ri = Int32.Parse("EE", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("E8", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("AA", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corDE"] = "S";
            }
            if (usuarioAtivo == "TD")
            {
                int ri = Int32.Parse("CC", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("CC", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("00", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corTD"] = "S";
            }
        }

    }

    #endregion

    #region COMBOBOX

    private void populaCmbPerfil()
    {
        DataSet ds = cDados.getPerfisDisponivelUsuario(CodigoEntidade, "EN", CodigoEntidade, 0);

        if (cDados.DataSetOk(ds))
        {
            cmbPerfil.DataSource = ds.Tables[0];
            cmbPerfil.TextField = "DescricaoPerfil";
            cmbPerfil.ValueField = "CodigoPerfil";

            cmbPerfil.DataBind();
        }
    }

    private void populaCmbTipoAutentica()
    {
        DataSet ds = cDados.getTipoAutenticacaoSistema();
        if (cDados.DataSetOk(ds))
        {
            cmbTipoAutentica.DataSource = ds.Tables[0];
            cmbTipoAutentica.TextField = "NomeTipoAutenticacao";
            cmbTipoAutentica.ValueField = "CodigoTipoAutenticacao";
            cmbTipoAutentica.DataBind();
        }
    }



    #endregion

    #region VARIOS

    private string geraSenha()
    {
        string numero = "";
        Random objeto = new Random();
        for (int i = 0; i < 6; i++)
            numero += objeto.Next(0, 9).ToString();
        return numero;
    }

    private void habilitaCampos()
    {
        string situacaoEmail = hfGeral.Get("emailVerificadoUsuarioNovo").ToString();
        txtEmail.ClientEnabled = false;

        if ("EXOE" == situacaoEmail || "ELOE" == situacaoEmail)
        {
            txtNomeUsuario.ClientEnabled = false;
            txtCPF.ClientEnabled = false;
            txtLogin.ClientEnabled = false;
            txtTelefoneContato1.ClientEnabled = false;
            txtTelefoneContato2.ClientEnabled = false;
            cmbTipoAutentica.ClientEnabled = false;
            memObservacoes.ClientEnabled = false;
        }
        if ("EXNE" == situacaoEmail || "I" == situacaoEmail || "ELNE" == situacaoEmail)
        {
            txtNomeUsuario.ClientEnabled = true;
            txtCPF.ClientEnabled = true;
            txtLogin.ClientEnabled = true;
            txtTelefoneContato1.ClientEnabled = true;
            txtTelefoneContato2.ClientEnabled = true;
            cmbTipoAutentica.ClientEnabled = true;
            memObservacoes.ClientEnabled = true;
        }
    }


    private ListDictionary getDadosFormulario()
    {
        //NomeUsuario;EMail;TelefoneContato1;TelefoneContato2;ContaWindows;TipoAutenticacao;
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("NomeUsuario", txtNomeUsuario.Text);
        oDadosFormulario.Add("CPF", txtCPF.Text);
        oDadosFormulario.Add("EMail", txtEmail.Text);
        oDadosFormulario.Add("TelefoneContato1", txtTelefoneContato1.Text);
        oDadosFormulario.Add("TelefoneContato2", txtTelefoneContato2.Text);
        oDadosFormulario.Add("ContaWindows", txtLogin.Text);
        oDadosFormulario.Add("TipoAutenticacao", cmbTipoAutentica.TextField.ToString());
        //oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
        return oDadosFormulario;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui estilo desta tela
        Header.Controls.Add(cDados.getLiteral(@"
                <style type='text/css'>
                    .dragclass{
                        position : relative;
                        cursor : move;
                    }

                    #divSalvoPublicado{
                        position: absolute; /*Define a posição absoluta da pop-up*/
                        top: 100; /*Distancia da margem superior da página */
                        left: 200; /*Distancia da margem esquerda da página */
                        width: 300px; /*Largura da pop-up*/
                        height: 40px; /*Altura da pop-up*/
                        padding: 2px 2px 2px 2px; /*Margem interna da pop-up*/
                        border-color: #C2C2C2; 
                        border-width: 1px; /*Largura da borda da pop-up*/
                        border-style: solid; /*Estilo da borda da pop-up*/
                        background: #E5E5E5; /*Cor de fundo da pop-up*/
                        color: #5C5C5C; /*Cor do texto da pop-up*/
                        display: none; /* Estilo da pop-up*/
                    }

                    a:hover {
                        text-decoration:none;

                    } /* background-color e necessario para o IE6 */

			        #msg{
				        position: absolute;
				        background: #f0f0f0;
				        top: 20%;
				        left: 33%;
				        width: 300px;
				        height: 300px;
				        border:groove 1px #000;
			        }
                </style>"));
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroUsuariosNovo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "CadastroUsuariosNovo", "ASPxListbox"));

    }

    private string getDadosUnidade(string codigoUnidade)
    {
        string retorno = "";
        DataSet ds = cDados.getUnidade(" AND CodigoUnidadeNegocio = " + codigoUnidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString() + " (" + ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString() + ")";
        }

        return retorno;
    }

    private void setLabelTela()
    {
        DataSet ds = cDados.getDefinicaoEntidade(CodigoEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string definicaoEntidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            lblDescricaoCorUnidade.Text = definicaoEntidade + " relacionadas ao usuario.";
            lblMensagemOutraEntidade.Text = "*@#@Usuário cadastrado em outra " + definicaoEntidade;
            lblMsgIncluirEntidadAtual.Text = Resources.traducao.CadastroUsuariosNovo_aten__o__este_email___de_um_usu_rio_de_outra_entidade__deseja_incluir_o_usu_rio_nesta_ + definicaoEntidade + Resources.traducao.CadastroUsuariosNovo__tamb_m_;
        }
        //Existência de Unidades relacionadas ao usuario.
    }

    #endregion

    #region VERIFICAÇÃO-EMAIL
    /*
    * Neste escopo se achan a diferentes funções qeu fazen uso ao cadastrar um novo usuario.
    * 
    * No cadastro do usuario, o primer dato a prencher sera o E-Mail. Se verificara si ele
    * existe, ou não, ficando como resultado a diferentes situacaoEmail(*). Segundo ele, 
    * o cadastro sera de uma forma ou outra.
    * 
    * 
   (*) situacaoEmail :'I'    -> Inexistentehttp://localhost:42388/administracao/CadastroContratoExterno.aspx
    *                 'EXNE' -> EXcluido agora Neste Entidade.
    *                 'ATNE' -> ATivo Nesta Entidade.
    *                 'ATOE' -> ATivo Outra Entidade.
     *                'ALTE' -> Alteração de Email 
    */
    protected void hfConfirmacaoEmailUsuario_CustomCallback1(object sender, CallbackEventArgsBase e)
    {
        hfConfirmacaoEmailUsuario.Set("confirmacaoEmailInvalido", true);        
        hfNovoEmailUsuario_CustomCallback1(sender, e);
    }
        protected void hfNovoEmailUsuario_CustomCallback1(object sender, CallbackEventArgsBase e)
    {
        if (hfGeral.Get("TipoOperacao").ToString() == "Editar")
            hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "ALTE");
        else
            hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "I");
        hfNovoEmailUsuario.Set("codigoUsuarioExluido", "");
        //bool entidadeEmail = false;
        string exclusaoEmail = "";
        dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, CodigoEntidade.ToString());
        if (cDados.DataSetOk(dsEmail))
        {
            if (dsEmail.Tables[0].Rows.Count > 0)
            {
                hfNovoEmailUsuario.Set("codigoUsuarioExcluido", dsEmail.Tables[0].Rows[0]["CodigoUsuario"].ToString());
                exclusaoEmail = dsEmail.Tables[0].Rows[0]["DataExclusao"].ToString();

                if ("" != exclusaoEmail)
                {
                    hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "EXNE");
                }
                else
                {
                    hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "ATOE");

                    foreach (DataRow drEntidade in dsEmail.Tables[0].Rows)
                    {
                        string unidadeNegocioEmail = drEntidade["CodigoUnidadeNegocio"].ToString();
                        if (unidadeNegocioEmail == CodigoEntidade.ToString())
                        {
                            hfNovoEmailUsuario.Set("emailVerificadoUsuarioNovo", "ATNE");
                            break;
                        }
                    }
                } // end if ("" != exclusaoEmail)
            } // end if (dsEmail.Tables[0].Rows.Count > 0)
        } // end if (cDados.DataSetOk(dsEmail))
    }

    protected void pnCallbackFormulario_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;

        pnCallbackFormulario.JSProperties["cp_SituacaoEmail"] = parametro;

        if ("ativarUsuario" == parametro)
        {
            cargarCamposDesdeEmail();
        }

        if ("adicionarUnidade" == parametro)
        {
            cargarCamposDesdeEmail();
        }

        if ("nuevoUsuario" == parametro)
        {
            cargarCamposVacios();
        }

        if ("alterarUsuario" == parametro)
        {
            cmbPerfil.ClientVisible = false;
            lblPerfil.ClientVisible = false;
            enabledFormulario(true);
        }
    }

    private void cargarCamposDesdeEmail()
    {

        string situacaoEmail = hfNovoEmailUsuario.Get("emailVerificadoUsuarioNovo").ToString();
        bool estado = true;

        dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, CodigoEntidade.ToString());
        txtNomeUsuario.Text = dsEmail.Tables[0].Rows[0]["NomeUsuario"].ToString();
        txtCPF.Text = dsEmail.Tables[0].Rows[0]["CPF"].ToString();
        txtEmail.Text = dsEmail.Tables[0].Rows[0]["EMail"].ToString();
        txtTelefoneContato1.Text = dsEmail.Tables[0].Rows[0]["TelefoneContato1"].ToString();
        txtTelefoneContato2.Text = dsEmail.Tables[0].Rows[0]["TelefoneContato2"].ToString();
        txtLogin.Text = dsEmail.Tables[0].Rows[0]["ContaWindows"].ToString();
        cmbTipoAutentica.Value = dsEmail.Tables[0].Rows[0]["TipoAutenticacao"].ToString();

        ckbAtivo.Checked = true;

        memObservacoes.Text = dsEmail.Tables[0].Rows[0]["Observacoes"].ToString();

        if ("EXOE" == situacaoEmail || "ATOE" == situacaoEmail)
            estado = false;

        enabledFormulario(estado);
    }

    private void cargarCamposVacios()
    {
        string situacaoEmail = hfNovoEmailUsuario.Get("emailVerificadoUsuarioNovo").ToString();

        dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, CodigoEntidade.ToString());
        txtNomeUsuario.Text = "";
        txtEmail.Text = "";
        txtCPF.Text = "";
        txtTelefoneContato1.Text = "";
        txtTelefoneContato2.Text = "";
        txtLogin.Text = "";
        cmbTipoAutentica.ValueField = "AI";
        memObservacoes.Text = "";
        ckbAtivo.Checked = true;
        cmbPerfil.Text = "";
        enabledFormulario(true);
        //txtEmail.ClientEnabled = false;
    }

    private void enabledFormulario(bool estado)
    {
        txtNomeUsuario.ClientEnabled = estado;
        txtCPF.ClientEnabled = estado;
        txtTelefoneContato1.ClientEnabled = estado;
        txtTelefoneContato2.ClientEnabled = estado;
        txtLogin.ClientEnabled = estado;
        cmbTipoAutentica.ClientEnabled = estado;
        cmbPerfil.ClientEnabled = true; // o combo para escolher fica sempre habilita já que o Alejandro criou 
        // um procedimento específico para habilitar componente quando o email existe.
        memObservacoes.ClientEnabled = estado;
    }

    #endregion

    #region GERA EMAIL USUARIO

    protected void callbackSenha_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackSenha.JSProperties["cp_MSG"] = "";
        callbackSenha.JSProperties["cp_Erro"] = "";

        int regAf = 0;
        string msgStatus = "", assunto = "", emailDestinatario = "", mensagem = "", nomeDestinatario = "";
        string nomeSistema = "Portal da Estratégia";
        int codigoUsuario = int.Parse(getChavePrimaria());
        string novaSenhaUsuario = geraSenha();
        string urlSistema = "";
        string tipoAutenticacao = "";
        string textoPadraoSistema = "";
        string query = "";

        bool resultado = cDados.atualizaSenhaUsuario(codigoUsuario, idUsuarioLogado, novaSenhaUsuario, ref regAf, ref msgStatus);

        if (resultado)
        {
            assunto = Resources.traducao.CadastroUsuariosNovo_altera__o_de_senha;
            emailDestinatario = gvDados.GetRowValues(gvDados.FocusedRowIndex, "EMail").ToString();
            nomeDestinatario = gvDados.GetRowValues(gvDados.FocusedRowIndex, "NomeUsuario").ToString();
            tipoAutenticacao = gvDados.GetRowValues(gvDados.FocusedRowIndex, "TipoAutenticacao").ToString();

            DataSet dsParam = cDados.getParametrosSistema(CodigoEntidade, "tituloPaginasWEB", "urlAplicacao_AcessoInternet", "urlAplicacao_AcessoExterno");

            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                nomeSistema = dsParam.Tables[0].Rows[0]["tituloPaginasWEB"] + "";
                urlSistema = dsParam.Tables[0].Rows[0]["urlAplicacao_AcessoInternet"] + "";

                if (tipoAutenticacao == "AS" && dsParam.Tables[0].Rows[0]["urlAplicacao_AcessoExterno"] + "" != "")
                {
                    urlSistema = dsParam.Tables[0].Rows[0]["urlAplicacao_AcessoExterno"] + "";
                }
            }


            query = "Select Texto from textoPadraoSistema t where t.IniciaisTexto = 'TX_EmailNovaSenhaUs' AND CodigoEntidade = " + CodigoEntidade;
            textoPadraoSistema = "";
            DataSet ds = cDados.getDataSet(query);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                textoPadraoSistema = ds.Tables[0].Rows[0]["Texto"].ToString();
            }

            if (textoPadraoSistema == "")
            {
                if (textoPadraoSistema == "")
                {
                    textoPadraoSistema = string.Format(
                      @"<fieldset><div style='font-family: Verdana; font-size: 9pt'><p><b>" + Resources.traducao.CadastroUsuariosNovo_prezado_a_ + " @b_NomeUsuario, " +
                            "</b></p><p></p><p>"+ Resources.traducao.CadastroUsuariosNovo_sua_senha_do + " <u>@nomeSistema</u> "+ Resources.traducao.CadastroUsuariosNovo_foi_redefinida_pelo_administrador_do_sistema_par + "<b style='color: blue; font-size: 9pt'> "+
                            "@SenhaUsuario</b>.</p><p>" + Resources.traducao.CadastroUsuariosNovo_para_entrar_no_sistema__acesse_o_endere_o + " <a href='@linkSistema'><i>@nomeSistema</i></a><br /><br /><br /><i> " +
                            "@nomeSistema.</i></p><p></p><p><b>" + Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail +".</b><br /></p></div></fieldset>");

                }

            }

            mensagem = textoPadraoSistema
                .Replace("@b_NomeUsuario", nomeDestinatario)
                .Replace("@linkSistema", urlSistema)
                .Replace("@nomeSistema", nomeSistema)
                .Replace("@NomeUnidade", nomeUnidadeParaEmail)
                .Replace("@EmailDestino", emailDestinatario)
                .Replace("@SenhaUsuario", novaSenhaUsuario);

            int retornoStatus = 0;

            string emailEnviado = cDados.enviarEmail(assunto, emailDestinatario, "", mensagem, "", "", ref retornoStatus);
            if (retornoStatus == 0)
            {
                callbackSenha.JSProperties["cp_Erro"] = emailEnviado;
            }
            else
            {
                callbackSenha.JSProperties["cp_MSG"] = Resources.traducao.CadastroUsuariosNovo_senha_alterada__ + emailEnviado;
            }
        }
        else
            callbackSenha.JSProperties["cp_Erro"] = Resources.traducao.CadastroUsuariosNovo_erro_ao_alterar_a_senha__ + msgStatus;
    }

    /// <summary>
    /// Gerar e-mail para o novo usuário. 
    /// Onde informar os dados necessários para acesso ao sistema bem-sucedido.
    /// </summary>
    /// <param name="emailDestino">Email do usuario cadastrado.</param>
    /// <param name="integracao">Modo de cadastro del usuario, Integrado o Sistema.</param>
    /// <param name="nomeDestinatario">Nome do usuario cadastrado.</param>
    /// <param name="logimDestinatario">Logim utilizado pelo usuario para acceder ao sistema.</param>
    /// <param name="senhaDestinatario">Senha seleccionada ao momento de cadastrar o usuario.</param>
    private void geraEmailUsuarioSistema(string emailDestino, string integracao, string nomeDestinatario
                                       , string logimDestinatario, string senhaDestinatario, string tipoAtualiza, string tipo)
    {
        string enviaEmail = string.Empty;
        DataSet dsParam = cDados.getParametrosSistema(CodigoEntidade, "enviaEmailCadastroUsuario");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            enviaEmail = dsParam.Tables[0].Rows[0]["enviaEmailCadastroUsuario"] + "";
        }

        if (enviaEmail.ToUpper().Equals("N"))
            return;

        string mensagem = "";
        string assunto = Resources.traducao.CadastroUsuariosNovo_usuario_cadastrado;
        string urlSistema = getParametroUrl("urlAplicacao_AcessoInternet");
        string nomeSistema = getParametroUrl("tituloPaginasWEB");
        string textoPadraoSistema = "";
        string query = "";

        if (integracao == "AS" && getParametroUrl("urlAplicacao_AcessoExterno") != "")
        {
            urlSistema = getParametroUrl("urlAplicacao_AcessoExterno");
        }

        if (integracao == "AS" || tipoAtualiza == "AU")
            query = "Select Texto from textoPadraoSistema t where t.IniciaisTexto = 'TX_EmailNovoUsAS' AND CodigoEntidade = " + CodigoEntidade;
        else if (integracao == "AI")
            query = "Select Texto from textoPadraoSistema t where t.IniciaisTexto = 'TX_EmailNovoUsAI' AND CodigoEntidade = " + CodigoEntidade;
        else if (integracao == "AP")
            query = "Select Texto from textoPadraoSistema t where t.IniciaisTexto = 'TX_EmailNovoUsAP' AND CodigoEntidade = " + CodigoEntidade;

        textoPadraoSistema = "";

        if (query != "")
        {
            DataSet ds = cDados.getDataSet(query);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                textoPadraoSistema = ds.Tables[0].Rows[0]["Texto"].ToString();
        }
        if (textoPadraoSistema == "")
        {

            if (integracao == "AS" || tipoAtualiza == "AU")
                textoPadraoSistema = string.Format(
                    @"<fieldset><p></p><p><b>" + Resources.traducao.CadastroUsuariosNovo_prezado_a_ + "</b><br /><i>@b_NomeUsuario,</i></p><p></p><p>" +
                        Resources.traducao.CadastroUsuariosNovo_foi_criado_para_voc__um_acesso_ao_sistema + " < u>@nomeSistema</u>. "+ Resources.traducao.CadastroUsuariosNovo_para_entrar_no_sistema__acesse_o_endere_o +"</p><p> " +
                        "</p><table><tr><td><b>" + Resources.traducao.CadastroUsuariosNovo_unidade + ":</b></td><td>@NomeUnidade</td></tr><tr><td><b>Link :</b></td><td><a href='@linkSistema'>" + 
                        " <i>@nomeSistema</i></a></td></tr><tr><td><b>" + Resources.traducao.CadastroUsuariosNovo_usuario + ":</b></td><td>@EmailDestino</td></tr><tr><td><b> " + Resources.traducao.CadastroUsuariosNovo_senha +":</b></td> " +
                        " <td> @SenhaUsuario</td></tr></table><p></p><p><br />" + Resources.traducao.CadastroUsuariosNovo_administra__o_do  + "< br />@nomeSistema</p><p></p><p><b>" +
                        Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail + ".</b></p><p></p></fieldset>");
            else if (integracao == "AI")
                textoPadraoSistema = string.Format(
                    @"<fieldset><p></p><p><b>" + Resources.traducao.CadastroUsuariosNovo_prezado_a_ + "</b><br /><i>@b_NomeUsuario,</i></p><p></p><p>" +
                        Resources.traducao.CadastroUsuariosNovo_foi_criado_para_voc__um_acesso_ao_sistema + "< u>@nomeSistema</u>. " +
                        Resources.traducao.CadastroUsuariosNovo_seguem_credenciais_para_acesso_ao_sistema +":</p><p></p><table><tr><td><b>" +
                        Resources.traducao.CadastroUsuariosNovo_unidade + ":</b></td><td>@NomeUnidade</td></tr><tr><td><b>" + Resources.traducao.CadastroUsuariosNovo_endere_o + ":</b></td><td>" + 
                        "<a href='@linkSistema'><i>@nomeSistema</i></a></td></tr><tr><td><b>" + Resources.traducao.CadastroUsuariosNovo_usuario + ":</b></td>" + 
                        "<td>@EmailDestino</td></tr></table><p></p><p><br />" + Resources.traducao.CadastroUsuariosNovo_administra__o_do  + "<br />@nomeSistema.</p>" + 
                        "<p></p><p><b>"+ Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail + ".</b></p><p></p></fieldset>");
            else if (integracao == "AP")
                textoPadraoSistema = string.Format(
                    @"<fieldset><p></p><p><b>"+ Resources.traducao.CadastroUsuariosNovo_prezado_a_  + "</b><br /><i>@b_NomeUsuario,</i></p><p></p><p>" +
                        Resources.traducao.CadastroUsuariosNovo_foi_criado_para_voc__um_acesso_ao_sistema + "<u>@nomeSistema</u>." +
                        Resources.traducao.CadastroUsuariosNovo_seguem_credenciais_para_acesso_ao_sistema +":</p><p></p><table><tr><td><b>"+
                        Resources.traducao.CadastroUsuariosNovo_unidade + ":</b></td><td>@NomeUnidade</td></tr><tr><td><b>"+ Resources.traducao.CadastroUsuariosNovo_endere_o+":</b></td><td>" +
                        "<a href='@linkSistema'><i>@nomeSistema</i></a></td></tr><tr><td><b>" + Resources.traducao.CadastroUsuariosNovo_usuario + ":</b></td>" +
                        "<td>@EmailDestino</td></tr></table><p></p><p><br />" + Resources.traducao.CadastroUsuariosNovo_administra__o_do  + "< br />@nomeSistema.</p>" + 
                        "<p></p><p><b>"+ Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail + ".</b></p><p></p></fieldset>");
        }


        if (tipo != "nuevoUsuario" && tipo != "alterarUsuario" && tipo != "emailAlterado")
        {
            textoPadraoSistema = string.Format(
                                @"<fieldset><p></p><p><b>" + Resources.traducao.CadastroUsuariosNovo_prezado_a_ + "</b><br /><i>@b_NomeUsuario,</i></p><p></p><p>" +
                       Resources.traducao.CadastroUsuariosNovo_foi_criado_para_voc__um_acesso_ao_sistema +"< u>@nomeSistema</u>." +
                       "<table><tr> <td> <b>" + Resources.traducao.CadastroUsuariosNovo_unidade + ":</b> </td> "+
                       "<td>@NomeUnidade</td>" + "" +
                       "</tr>" +
                       "<tr><td><b>Link :</b></td>" + 
                       "<td><a href='@linkSistema'><i>@nomeSistema</i></a></td>" + 
                       "</tr>" + 
                       "<tr>" +
                       "<td><b>" + Resources.traducao.CadastroUsuariosNovo_seu_usu_rio_e_senha_n_o_foram_alterados + ".</b></td><td></td>" + 
                       "</tr>" + 
                       "</table><p></p><p><br />" + Resources.traducao.CadastroUsuariosNovo_administra__o_do +"< br />@nomeSistema</p><p></p><p><b>"+
                     Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail +".</b></p><p></p></fieldset>");
        }

        if (tipo == "emailAlterado")
        {
            assunto = Resources.traducao.CadastroUsuariosNovo_altera__o_de_e_mail_usu_rio_cadastrado;
            textoPadraoSistema = string.Format(
                                @"<fieldset><p></p><p><b>" + Resources.traducao.CadastroUsuariosNovo_prezado_a_ + "</b><br /><i>@b_NomeUsuario,</i></p><p></p><p>" +
                                Resources.traducao.CadastroUsuariosNovo_seu_acesso_ao_sistema + "< u>@nomeSistema</u> foi alterado." +
                                "<table><tr> <td> <b>" + Resources.traducao.CadastroUsuariosNovo_unidade +":</b> </td>" +  
                                "<td>@NomeUnidade</td>" + 
                                "</tr>" + 
                                "<tr><td><b>Link :</b></td>" +
                                "<td><a href='@linkSistema'><i>@nomeSistema</i></a></td>" +
                                "</tr>" +
                                "<tr><td><b>"+ Resources.traducao.CadastroUsuariosNovo_usuario +":</b></td><td>@EmailDestino</td></tr>" +
                                "<tr>" + 
                                "<td><b>" + Resources.traducao.CadastroUsuariosNovo_endere_o + ".</b></td><td></td>" +
                                "</tr>" +
                                "</table><p></p><p><br />" + Resources.traducao.CadastroUsuariosNovo_administra__o_do + "<br />@nomeSistema</p><p></p><p><b>" +
                                Resources.traducao.CadastroUsuariosNovo_ps__por_favor__n_o_responda_esse_e_mail + ".</b></p><p></p></fieldset>");
        }
        mensagem = textoPadraoSistema
            .Replace("@b_NomeUsuario", nomeDestinatario)
            .Replace("@linkSistema", urlSistema)
            .Replace("@nomeSistema", nomeSistema)
            .Replace("@NomeUnidade", nomeUnidadeParaEmail)
            .Replace("@EmailDestino", emailDestino)
            .Replace("@SenhaUsuario", senhaDestinatario);

        int retornoStatus = 0;

        string emailEnviado = cDados.enviarEmail(assunto, emailDestino, "", mensagem, "", "", ref retornoStatus);
    }

    /// <summary>
    /// Obtem dados da configuração del sistema atual.
    /// </summary>
    /// <param name="Parametro">Parâmetro ao procurar.</param>
    /// <returns>Valor setado al parâmetro procurado.</returns>
    private String getParametroUrl(string Parametro)
    {
        string retorno = "";
        DataSet ds = cDados.getParametrosSistema(CodigoEntidade, Parametro);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            retorno = ds.Tables[0].Rows[0][Parametro].ToString();

        return retorno;
    }

    #endregion

    #region CALLBACK'S

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        pnCallback.JSProperties["cp_EsSuperUsuario"] = "";
        pnCallback.JSProperties["cp_EsRecursoCorporativo"] = "";
        pnCallback.JSProperties["cp_LegendasEnvolvidas"] = "";
        pnCallback.JSProperties["cp_IndicaCpfInvalido"] = "N";
        pnCallback.JSProperties["cp_HabilitaSimNao"] = "N";


        bool isSuperUsuarioEntidadAtual = false;
        bool isRecursoCorporativo = false;

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "IncluirEntidadeAtual")
        {
            mensagemErro_Persistencia = persisteInclusaoEntidadeAtual();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro(ref isSuperUsuarioEntidadAtual, ref isRecursoCorporativo);
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro(ref isSuperUsuarioEntidadAtual, ref isRecursoCorporativo);
            if (isSuperUsuarioEntidadAtual)
                pnCallback.JSProperties["cp_EsSuperUsuario"] = "OK";
            if (isRecursoCorporativo)
                pnCallback.JSProperties["cp_EsRecursoCorporativo"] = "OK";
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    #endregion

    #region BANCO DE DADOS.

    private DataTable DataTableGridImpedimento()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("NomeUnidade", Type.GetType("System.String"));
        NewColumn.Caption = "Unidade";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("codImpedimento", Type.GetType("System.String"));
        NewColumn.Caption = "codImpedimento";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        return dtResult;
    }

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private string persisteInclusaoEntidadeAtual()
    {
        string msg = "";
        string parametroSQL = "";

        try
        {
            if (hfGeral.Contains("CodigoUsuario"))
            {
                int codigoUsuario = -1;
                if (int.TryParse(hfGeral.Get("CodigoUsuario").ToString(), out codigoUsuario))
                {
                    parametroSQL = string.Format("{0}, {1}, {2}", codigoUsuario, CodigoEntidade, (ckbAtivo.Checked ? "S" : "N"));
                    DataSet ds = cDados.incluiUsuarioNovoEntidadeAtual(parametroSQL);
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        populaGrid();
                        pnCallback.JSProperties["cp_Mostrar"] = "N";
                        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
                        gvDados.ClientVisible = false;
                    } // if (cDados.DataSetOk(ds) && ...
                    else
                        return "Erro durante a gravação dos dados. Código do erro: 1005.";

                } // if (int.TryParse(hfGeral.Get("CodigoUsuario") ...
                else
                    return "Erro durante a gravação dos dados. Código do erro: 1006.";
            } // if (int.TryParse(hfGeral.Get(
            else
                return "Erro durante a gravação dos dados. Código do erro: 1007.";
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            return ex.Message;
        }
        return msg;
    }

    /// <summary>
    /// Método responsável pela Inclusão do registro
    /// </summary>
    /// <returns>
    /// Retorna uma string vacia ("") em caso que ao inserir o novo usuário tenha exito.
    /// Retorna uma string ("[mensagem de erro]") em caso que por algum motivo não posso inserir o novo usuário.
    /// </returns>
    private string persisteInclusaoRegistro()
    {
        int retorno = 0;

        //1ra mudança 2/11/2010: o codigo usuario obtengo da hiddenField, que antigamente feiz a pesquiça da existencia do
        //                       usuario.
        int codigoUsuario = -1;
        //int existenciaUsuario = 0;
        string perfisSeleccionado = cmbPerfil.Value.ToString();
        string tipoConfirmacao = hfNovoEmailUsuario.Get("tipoConfirmacao").ToString();
        string tipoEmailVarificado = hfNovoEmailUsuario.Get("emailVerificadoUsuarioNovo").ToString();
        string tipoAtualiza = "";
        string novaSenhaUsuario = "";
        string mensagem = "";
        string cpf = txtCPF.Text;

        try
        {
            //codigoUsuario = -1;
            if (hfNovoEmailUsuario.Contains("codigoUsuarioExcluido"))
                codigoUsuario = int.Parse(hfNovoEmailUsuario.Get("codigoUsuarioExcluido").ToString());

            /*---altero----*/
            //existenciaUsuario = verificaExistenciaUsuario(out codigoUsuario);
            //se não existir o email
            //if (existenciaUsuario == 0)
            //{

            novaSenhaUsuario = geraSenha();
            string novaSenhaUsuarioCriptografada = cDados.ObtemCodigoHash(novaSenhaUsuario).ToString();

            if ("ativarUsuario" == tipoConfirmacao)
            {
                //codigoUsuario = int.Parse(hfNovoEmailUsuario.Get("codigoUsuarioExcluido").ToString());
                if ("EXOE" == tipoEmailVarificado)
                    tipoAtualiza = "OE";
                else if ("EXNE" == tipoEmailVarificado)
                    tipoAtualiza = "NE";

                cDados.atualizaEntidadResponsavel(codigoUsuario, CodigoEntidade, txtNomeUsuario.Text.Replace("'", "''"), cpf,
                                                  txtLogin.Text.Replace("'", "''"), cmbTipoAutentica.Value.ToString(),
                                                  txtEmail.Text, txtTelefoneContato1.Text, txtTelefoneContato2.Text,
                                                  memObservacoes.Text.Replace("'", "''"), ckbAtivo.Checked ? "S" : "N",
                                                  idUsuarioLogado.ToString(), tipoAtualiza, novaSenhaUsuarioCriptografada);
            }
            else if ("adicionarUnidade" == tipoConfirmacao)
            {
                //codigoUsuario = int.Parse(hfNovoEmailUsuario.Get("codigoUsuarioExcluido").ToString());
                tipoAtualiza = "AU";
                cDados.atualizaEntidadResponsavel(codigoUsuario, CodigoEntidade, txtNomeUsuario.Text.Replace("'", "''"), cpf,
                              txtLogin.Text.Replace("'", "''"), cmbTipoAutentica.Value.ToString(),
                              txtEmail.Text, txtTelefoneContato1.Text, txtTelefoneContato2.Text,
                              memObservacoes.Text.Replace("'", "''"), ckbAtivo.Checked ? "S" : "N",
                              idUsuarioLogado.ToString(), tipoAtualiza, novaSenhaUsuarioCriptografada);
            }
            else if ("nuevoUsuario" == tipoConfirmacao)
            {
                //Verifico que a conta window nao esteja ja em uso por um outro usuario. 
                //(Como usuario ainda não possiu [codigoUsuario], tendra como parámetro o '-1').
                //verificaExistenciaContaWindows(-1, true);
                if (txtCPF.Value != null)
                {
                    string wherex = string.Format(@" and us.CPF = '{0}'", txtCPF.Value);
                    verificaExistenciaUsuario(-1, wherex, Resources.traducao.CadastroUsuariosNovo_usu_rio_com_o_cpf_j__existe, true);
                }                
 
                DataSet ds = cDados.incluiUsuarioNovo(txtNomeUsuario.Text.Replace("'", "''"),
                                                      txtLogin.Text.Replace("'", "''"),
                                                      cmbTipoAutentica.Value.ToString(),
                                                      novaSenhaUsuarioCriptografada,
                                                      txtEmail.Text.Replace("'", "''"),
                                                      txtTelefoneContato1.Text, txtTelefoneContato2.Text,
                                                      memObservacoes.Text.Replace("'", "''"),
                                                      ckbAtivo.Checked ? "S" : "N", idUsuarioLogado, CodigoEntidade, cpf);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    retorno = int.Parse(ds.Tables[0].Rows[0]["Retorno"].ToString());
                    codigoUsuario = int.Parse(ds.Tables[0].Rows[0]["CodigoUsuario"].ToString());

                }// if (cDados.DataSetOk(ds) && ...
            }

            cDados.incluirInteressadoObjeto(CodigoEntidade, "NULL", "'EN'", codigoUsuario, false, idUsuarioLogado, 0, perfisSeleccionado + ";", CodigoEntidade, ref mensagem);
            geraEmailUsuarioSistema(txtEmail.Text, cmbTipoAutentica.SelectedItem.Value.ToString(), txtNomeUsuario.Text, txtLogin.Text, novaSenhaUsuario, tipoAtualiza, tipoConfirmacao);

            populaGrid();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            mensagem = ex.Message;
        }

        return mensagem;
    }

    private string persisteEdicaoRegistro(ref bool isSuperUsuarioEntidadLogada, ref bool isRecursoCorporativo) // Método responsável pela Atualização do registro
    {
        //int retorno         = 0;
        int codigoUsuario = 0;
        //string autenticacaoLoad = hfGeral.Get("AutenticacaoLoad").ToString();
        string novaSenhaUsuario = geraSenha();
        string tipoAutentica = cmbTipoAutentica.SelectedItem.Value.ToString();
        bool podeInativarUsuario = true;
        string mensagem = "";
        string cpf = txtCPF.Text;
        if (tipoAutentica == "AS")
            novaSenhaUsuario = "";

        try
        {
            codigoUsuario = int.Parse(getChavePrimaria());
            string estadoNoBanco = getEstadoDoUsuarioNoBanco(codigoUsuario);

            // se a edição não na entidade responsável, muda apenas o status
            if (conflitoEntidadesResponsaveis(codigoUsuario))
            {
                if (estadoNoBanco.Equals("S") && !ckbAtivo.Checked)
                    verificarPodeInativarUsuario(codigoUsuario, ref podeInativarUsuario, ref isSuperUsuarioEntidadLogada, ref isRecursoCorporativo);
                if (isSuperUsuarioEntidadLogada)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {

                        throw new Exception("Este usuário tem perfil de super-usuário e devido a isto o mesmo não poderá ser INATIVADO e possui pendências vinculadas a ele no sistema " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        throw new Exception("Não há pendências para este usuário, porém ele consta como um super-usuário e não será possível INATIVÁ-LO");
                    }
                 }
                if (isRecursoCorporativo)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {

                        throw new Exception("Este usuário está configurado como Recurso Corporativo no sistema e possui as pendências abaixo " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        pnCallback.JSProperties["cp_HabilitaSimNao"] = "S";
                        throw new Exception("Não há pendências para este usuário, porém ele consta como um recurso corporativo ativo, você deseja realmente INATIVAR este recurso?");
                    }
                }
                if (podeInativarUsuario)
                {
                    MudaStatusUsuario(codigoUsuario);
                    populaGrid();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
                    gvDados.ClientVisible = false;
                }
                else
                    throw new Exception(Resources.traducao.CadastroUsuariosNovo_exist_ncia_de_item_s_relacionados_ao_usuario__n_o_pode_desativar_);
            }
            else
            {


                if (estadoNoBanco.Equals("S") && !ckbAtivo.Checked)
                    verificarPodeInativarUsuario(codigoUsuario, ref podeInativarUsuario, ref isSuperUsuarioEntidadLogada, ref isRecursoCorporativo);
                if (isSuperUsuarioEntidadLogada)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {
                        throw new Exception("Este usuário tem perfil de super-usuário e devido a isto o mesmo não poderá ser INATIVADO e possui pendências vinculadas a ele no sistema " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        throw new Exception("Não há pendências para este usuário, porém ele consta como um super-usuário e não será possível INATIVÁ-LO");
                    }
                }
                if (isRecursoCorporativo)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {

                        throw new Exception("Este usuário está configurado como Recurso Corporativo no sistema e possui as pendências abaixo " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        pnCallback.JSProperties["cp_HabilitaSimNao"] = "S";
                        throw new Exception("Não há pendências para este usuário, porém ele consta como um recurso corporativo ativo, você deseja realmente INATIVAR este recurso?");
                    }
                }
                if (podeInativarUsuario)
                {

                    verificaExistenciaEmail(codigoUsuario, true);
                    verificaExistenciaContaWindows(codigoUsuario, true);
                    if (txtCPF.Value != null)
                    {
                        string wherex = string.Format(@" and us.CPF = '{0}'", txtCPF.Value);
                        verificaExistenciaUsuario(codigoUsuario, wherex, Resources.traducao.CadastroUsuariosNovo_usu_rio_com_o_cpf_j__existe_, true);
                    } 
                    DataSet ds = cDados.atualizaUsuarioNovo(txtNomeUsuario.Text
                                                            , txtLogin.Text
                                                            , tipoAutentica
                                                            , txtEmail.Text
                                                            , txtTelefoneContato1.Text, txtTelefoneContato2.Text
                                                            , memObservacoes.Text
                                                            , ckbAtivo.Checked ? "S" : "N", idUsuarioLogado
                                                            , CodigoEntidade, int.Parse(getChavePrimaria()), cpf);

                    if (hfGeral.Get("emailAnterior").ToString() != txtEmail.Text)
                    {
                        geraEmailUsuarioSistema(txtEmail.Text, cmbTipoAutentica.SelectedItem.Value.ToString(), txtNomeUsuario.Text, txtLogin.Text, novaSenhaUsuario, "XX", "emailAlterado");
                    }

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        //if (autenticacaoLoad.Equals(cmbTipoAutentica.SelectedItem.Value.ToString()))
                        //    geraEmailUsuarioSistema(txtEmail.Text, cmbTipoAutentica.SelectedItem.Value.ToString(), txtNomeUsuario.Text, txtLogin.Text, novaSenhaUsuario, "");
                        populaGrid();
                        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
                        gvDados.ClientVisible = false;
                    }
                }
                else
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {
                        throw new Exception("Este usuário está Ativo no sistema e possui as pendências abaixo " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                }
            }
            mensagem = "";
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            mensagem = ex.Message;
        }

        return mensagem;
    }

    private string persisteExclusaoRegistro(ref bool isSuperUsuarioEntidadLogada, ref bool isRecursoCorporativo) // Método responsável pela Exclusão do registro
    {
        string msgRetorno = "";
        bool usuarioEntidadAtual = false;
        bool podeExcluirUsuario = false;
        bool podeInativarUsuario = false;
        //bool isSuperUsuarioEntidadAtual = false;
        //bool isRecursoCorporativo = false;

        try
        {
            string codigoUsuario = getChavePrimaria();
            verificarOrigemEntidadUsuario(int.Parse(codigoUsuario), ref usuarioEntidadAtual);

            if (usuarioEntidadAtual)
            {
                verificarPodeExcluirUsuarioEntidadAtual(int.Parse(codigoUsuario), ref podeExcluirUsuario, ref isSuperUsuarioEntidadLogada, ref isRecursoCorporativo);
                if (isSuperUsuarioEntidadLogada)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {

                        throw new Exception("Este usuário tem perfil de super-usuário e devido a isto o mesmo não poderá ser EXCLUÍDO e possui pendências vinculadas a ele no sistema " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        throw new Exception("Não há pendências para este usuário, porém ele consta como um super-usuário e não será possível EXCLUÍ-LO");
                    }
                }
                else if (isRecursoCorporativo)
                {
                    if (gvImpedimentos.VisibleRowCount > 0)
                    {

                        throw new Exception("Este usuário está configurado como Recurso Corporativo no sistema e possui as pendências abaixo " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        if (podeExcluirUsuario)
                        {
                            cDados.excluiUsuarioSistema(codigoUsuario, idUsuarioLogado.ToString(), CodigoEntidade, "EA", ref msgRetorno); //cDados.incluiUsuarioNovoEntidadeAtual(parametroSQL);
                        }
                        else
                        {
                            throw new Exception("Não há pendências para este usuário, porém ele consta como um recurso corporativo ativo, você deseja realmente EXCLUIR este recurso?");
                        }
                        
                    }
                }
                else
                {


                    if (gvImpedimentos.VisibleRowCount > 0)
                    {
                        throw new Exception("Este usuário está Ativo no sistema e possui as pendências abaixo " +
                            "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                    }
                    else
                    {
                        if (podeExcluirUsuario)
                        {
                            cDados.excluiUsuarioSistema(codigoUsuario, idUsuarioLogado.ToString(), CodigoEntidade, "EA", ref msgRetorno); //cDados.incluiUsuarioNovoEntidadeAtual(parametroSQL);
                        }
                    }
                }
            }
            else
            {
                verificarPodeExcluirUsuarioOutraEntidade(int.Parse(codigoUsuario), ref podeExcluirUsuario);
                if (podeExcluirUsuario)
                {
                    cDados.excluiUsuarioSistema(codigoUsuario, idUsuarioLogado.ToString(), CodigoEntidade, "OE", ref msgRetorno); //cDados.incluiUsuarioNovoEntidadeAtual(parametroSQL);
                }
                else
                {
                    verificarPodeInativarUsuario(int.Parse(codigoUsuario), ref podeInativarUsuario, ref isSuperUsuarioEntidadLogada, ref isRecursoCorporativo);
                    if (isSuperUsuarioEntidadLogada)
                    {
                        if (gvImpedimentos.VisibleRowCount > 0)
                        {

                            throw new Exception("Este usuário tem perfil de super-usuário e devido a isto o mesmo não poderá ser EXCLUÍDO e possui pendências vinculadas a ele no sistema " +
                                "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                        }
                        else
                        {
                            throw new Exception("Não há pendências para este usuário, porém ele consta como um super-usuário e não será possível EXCLUÍ-LO");
                        }
                    }
                    else if (isRecursoCorporativo)
                    {
                        if (gvImpedimentos.VisibleRowCount > 0)
                        {

                            throw new Exception("Este usuário está configurado como Recurso Corporativo no sistema e possui as pendências abaixo " +
                                "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                        }
                        else
                        {
                            throw new Exception("Não há pendências para este usuário, porém ele consta como um recurso corporativo ativo, você deseja realmente EXCLUIR este recurso?");
                        }
                    }
                    else
                    {
                        if (gvImpedimentos.VisibleRowCount > 0)
                        {
                            throw new Exception("Este usuário está Ativo no sistema e possui as pendências abaixo " +
                                "você pode atribuir essas pendências para outro(s) usuário(s) utilizando a funcionalidade de: " + " <a href='SubstituicaoRecursos.aspx' target='_blank'>" + Resources.traducao.CadastroUsuariosNovo_substitui__o_de_usu_rios + "</a>");
                        }
                    }
                }
            }
            populaGrid();
        }
        catch (Exception ex)
        {
            msgRetorno = ex.Message;
        }

        return msgRetorno;
    }

    private int verificaExistenciaUsuario(out int codigoUsuario)
    {
        int existe = 0;
        int entidade;

        codigoUsuario = -1;

        string where = string.Format(" AND us.[EMail] = '{0}' ", txtEmail.Text.Replace("'", "''"));
        DataSet ds = cDados.getDadosResumidosUsuario(where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            existe = 1;

            // atribui o código à variável de retorno;
            int.TryParse(ds.Tables[0].Rows[0]["CodigoUsuario"].ToString(), out codigoUsuario);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (int.TryParse(row["CodigoUnidadeNegocio"].ToString(), out entidade))
                {
                    if (entidade == CodigoEntidade)
                    {
                        existe = 2;
                        break;
                    } // if (entidade == CodigoEntidade)
                } // if ( int.TryParse(row["CodigoUnidadeNegocio"], out entidade) )
            } // foreach (DataRow row in ds.Tables[0])
        } // ( cDados.DataSetOk(ds) && ...

        return existe;
    }

    private string getEstadoDoUsuarioNoBanco(int codigoUsuario)
    {
        string retorno = "";
        DataSet ds = cDados.getEstadoUsuarioNaUnidadeNoBanco(codigoUsuario, CodigoEntidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            retorno = ds.Tables[0].Rows[0][0].ToString();

        return retorno;
    }

    private void MudaStatusUsuario(int codigoUsuario)
    {
        int registrosAfetados = 0;
        string comandoSQL = string.Format(
        @"UPDATE {0}.{1}.[UsuarioUnidadeNegocio] 
            SET     [IndicaUsuarioAtivoUnidadeNegocio] = '{4}' 
            WHERE [CodigoUsuario] = {2} AND [CodigoUnidadeNegocio] = {3} "
                    , dbName, dbOwner, codigoUsuario, CodigoEntidade, ckbAtivo.Checked ? 'S' : 'N');

        cDados.execSQL(comandoSQL, ref registrosAfetados);
    }

    /// <summary>
    /// Função que retorna um valor inteiro.
    /// 0 : em caso qeu não exista usuário com a mesma Conta de windows.
    /// Nº: em caso que exista usuário com a mesma conta de windows, esse número representa seu codigo de usuário. 
    /// </summary>
    /// <param name="codigoUsuario">Conta de usuário que se tenta cadastradar.</param>
    /// <param name="bRaiseError">Valor booleano para esforçar a salidad de mensagem de erro.</param>
    /// <returns>Número entero.</returns>
    private int verificaExistenciaContaWindows(int codigoUsuario, bool bRaiseError)
    {
        int codigoUsuario2 = 0;
        string tipoAutenticacao = cmbTipoAutentica.Value.ToString();

        // ACG em 24/11/2014 - Incluúido o tipo "AP" com tratamento igual ao tipo "AI"
        if ("AI" == tipoAutenticacao || "AP" == tipoAutenticacao)
        {
            string contaWin = txtLogin.Text.Replace("'", "''").Trim();
            string where = string.Format(" AND us.[ContaWindows] = '{0}' ", contaWin);
            string mensagem = Resources.traducao.CadastroUsuariosNovo_conta_do_sistema_operacional_j__existente__n_o___poss_vel_gravar_os_dados_usando_esta_conta_;
            codigoUsuario2 = verificaExistenciaUsuario(codigoUsuario, where, mensagem, bRaiseError);
        }
        return codigoUsuario2;
    }

    private int verificaExistenciaEmail(int codigoUsuario, bool bRaiseError)
    {
        int codigoUsuario2 = 0;

        string where = string.Format(" AND us.[EMail] = '{0}' ", txtEmail.Text.Replace("'", "''"));
        string mensagem = Resources.traducao.CadastroUsuariosNovo_email_j__existente__n_o___poss_vel_gravar_os_dados_usando_este_email_;
        codigoUsuario2 = verificaExistenciaUsuario(codigoUsuario, where, mensagem, bRaiseError);

        return codigoUsuario2;
    }

    /// <summary>
    /// Verifica se existe na base de dados um usuário com o critério informado no parâmetro <paramref name="where"/>
    /// </summary>
    /// <remarks>
    /// Em caso positivo, a função gera uma exceção caso o parâmetro <paramref name="bRaiseError"/> seja true.
    /// Se o parâmetro <paramref name="bRaiseError"/> for false, é devolvido o código do usuário encontrado ou 
    /// zero caso não tenha sido encontrado usuário algum.
    /// </remarks>
    /// <param name="codigoUsuario"></param>
    /// <param name="where">string conteudo de uma consulta SQL (compativle ao SQL SERVER).</param>
    /// <param name="mensagem">string conteudo do mensagem caso qeu tenha algúm erro.</param>
    /// <param name="bRaiseError"></param>
    /// <returns></returns>
    private int verificaExistenciaUsuario(int codigoUsuario, string where, string mensagem, bool bRaiseError)
    {
        // não é permitido passar o valor zero.
        // quando for inclusão, deve ser passado o valor -1;
        if (codigoUsuario == 0)
            throw new Exception(Resources.traducao.CadastroUsuariosNovo_erro_ao_gravar_os_dados__falha_interna_aplica__o___c_digo_4_);

        int codigoUsuario2 = 0;
        DataSet ds = cDados.getDadosResumidosUsuario(where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoUsuario"].ToString(), out codigoUsuario2);

        if (bRaiseError && (codigoUsuario2 != 0) && (codigoUsuario != codigoUsuario2))
        {
            gvImpedimentos.DataSource = null;
            gvImpedimentos.DataBind();
            dtImpedimentosGlobal = null;
            pnCallback.JSProperties["cp_IndicaCpfInvalido"] = "S";
            throw new Exception(mensagem);
        }

        return codigoUsuario2;
    }

    /// <summary>
    /// Verifica se a entidade que o usuário logado está permite a ele alterar o usuário em questão.
    /// </summary>
    /// <param name="codigoUsuario"></param>
    /// <exception cref="Exception"></exception>
    private bool conflitoEntidadesResponsaveis(int codigoUsuario)
    {
        DataSet dsEntidResp = cDados.getEntidadeResponsavelUsuario(codigoUsuario);

        bool determinouEntidadedResp;
        int codigoEntidadeResponsavel = -1;

        bool bConflito = false;

        if (cDados.DataSetOk(dsEntidResp) && cDados.DataTableOk(dsEntidResp.Tables[0]))
        {
            object codEntidade = dsEntidResp.Tables[0].Rows[0]["CodigoUnidadeNegocio"];
            determinouEntidadedResp = int.TryParse(codEntidade.ToString(), out codigoEntidadeResponsavel);
        }
        else
            determinouEntidadedResp = false;

        if (determinouEntidadedResp)
        {
            if (codigoEntidadeResponsavel != CodigoEntidade)
                bConflito = true;
        }
        else
            throw new Exception(Resources.traducao.CadastroUsuariosNovo_erro_ao_salvar_os_dados__n_o_foi_poss_vel_determinar_a_unidade_respons_vel_pelo_usu_rio_);

        return bConflito;
    }

    private void verificarOrigemEntidadUsuario(int codigoUsuario, ref bool usuarioEntidadAtual)
    {
        int codigoEntidadResponsavel = -1;
        usuarioEntidadAtual = false;
        DataSet ds = cDados.getCodigoUnidadeNegocioResponsavelDoUsuario(codigoUsuario);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoEntidadResponsavel = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            if (codigoEntidadResponsavel == CodigoEntidade)
                usuarioEntidadAtual = true;
        }
    }

    private void verificarPodeExcluirUsuarioEntidadAtual(int codigoUsuario
                                                        , ref bool podeExcluirUsuario
                                                        , ref bool isSuperUsuarioEntidadAtual
                                                        , ref bool isRecursoCorporativo)
    {
        DataSet ds = cDados.getPodeExcluirUsuarioEntidadAtual(codigoUsuario, CodigoEntidade);
        DataTable dt = DataTableGridImpedimento();
        DataRow newRow;
        podeExcluirUsuario = true;

        string legendasEnvolvidas = "";

        if (cDados.DataSetOk(ds))
        {
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeUnidadeNegocio"].ToString();
                    newRow["codImpedimento"] = "UN";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|UN" : "UN";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[1]))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PR";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|PR" : "PR";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[2]))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IN";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|IN" : "IN";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[3]))
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IO";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|IO" : "IO";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[4]))
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "DE";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|DE" : "DE";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[5]))
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PC";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|PC" : "PC";
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[6]))
            {
                isSuperUsuarioEntidadAtual = true;
            }
            if (cDados.DataTableOk(ds.Tables[7]))
            {
                isRecursoCorporativo = true;
            }

            if (cDados.DataTableOk(ds.Tables[8]))
            {
                foreach (DataRow dr in ds.Tables[8].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["DescricaoTarefa"].ToString();
                    newRow["codImpedimento"] = "TD";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|TD" : "TD";
                podeExcluirUsuario = false;
            }

            pnCallback.JSProperties["cp_LegendasEnvolvidas"] = legendasEnvolvidas;

            gvImpedimentos.DataSource = dt;
            gvImpedimentos.DataBind();
            dtImpedimentosGlobal = dt;
        }
    }

    private void verificarPodeExcluirUsuarioOutraEntidade(int codigoUsuario, ref bool podeExcluirUsuario)
    {
        DataSet ds = cDados.getPodeExcluirUsuarioOutraEntidad(codigoUsuario, CodigoEntidade);
        DataTable dt = DataTableGridImpedimento();
        DataRow newRow;
        podeExcluirUsuario = true;

        if (cDados.DataSetOk(ds))
        {
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeUnidadeNegocio"].ToString();
                    newRow["codImpedimento"] = "UN";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[1]))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PR";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[2]))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IN";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[3]))
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IO";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[4]))
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "DE";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[5]))
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PC";
                    dt.Rows.Add(newRow);
                }
                podeExcluirUsuario = false;
            }

            gvImpedimentos.DataSource = dt;
            gvImpedimentos.DataBind();
            dtImpedimentosGlobal = dt;
        }
    }

    private void verificarPodeInativarUsuario(int codigoUsuario, ref bool podeInativarUsuario
                                                        , ref bool isSuperUsuarioEntidadAtual
                                                        , ref bool isRecursoCorporativo)
    {
        //getPodeDesativarUsuario
        DataSet ds = cDados.getPodeDesativarUsuario(codigoUsuario, CodigoEntidade);
        DataTable dt = DataTableGridImpedimento();
        DataRow newRow;
        podeInativarUsuario = true;        
        string legendasEnvolvidas = "";
        if (cDados.DataSetOk(ds))
        {
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeUnidadeNegocio"].ToString();
                    newRow["codImpedimento"] = "UN";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += "UN";
                podeInativarUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[1]))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PR";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|PR" : "PR";
                podeInativarUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[2]))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IN";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|IN" : "IN";
                podeInativarUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[3]))
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeIndicador"].ToString();
                    newRow["codImpedimento"] = "IO";                    
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|IO" : "IO";
                podeInativarUsuario = false;
            }
            if (cDados.DataTableOk(ds.Tables[4]))
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "DE";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|DE" : "DE";
                podeInativarUsuario = false;
            }

            if (cDados.DataTableOk(ds.Tables[5]))
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["DescricaoTarefa"].ToString();
                    newRow["codImpedimento"] = "TD";
                    dt.Rows.Add(newRow);
                }
                legendasEnvolvidas += (legendasEnvolvidas.Length >= 2) ? "|TD" : "TD";
                podeInativarUsuario = false;
            }

            if (cDados.DataTableOk(ds.Tables[6]))
            {
                isSuperUsuarioEntidadAtual = true;
            }
            if (cDados.DataTableOk(ds.Tables[7]))
            {
                isRecursoCorporativo = true;
            }
            gvImpedimentos.DataSource = dt;
            gvImpedimentos.DataBind();
            dtImpedimentosGlobal = dt;
            pnCallback.JSProperties["cp_LegendasEnvolvidas"] = legendasEnvolvidas;
        }
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
        else if(e.RowType == GridViewRowType.Data)
        {
            if (e.Column.Caption == "CPF")
            {
                long cpf = 0;
                if (long.TryParse(e.TextValue.ToString(), out cpf) == true)
                {
                    e.TextValue = e.TextValue.ToString().Insert(3, ".");
                    e.TextValue = e.TextValue.ToString().Insert(7, ".");
                    e.TextValue = e.TextValue.ToString().Insert(11, "-");                   
                }
            }
        }
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        DevExpress.Web.MenuItem btnIncluir = (sender as ASPxMenu).Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = verificaLicencaAtiva() == false ? Resources.traducao.CadastroUsuariosNovo_n_o_h__licen_as_dispon_veis_para_incluir_novos_usu_rios : Resources.traducao.CadastroUsuariosNovo_incluir;

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), verificaLicencaAtiva() && podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadUsu", "Usuários", this);
    }

    private bool verificaLicencaAtiva()
    {
        int saldo = 0;
        string comandoSQL1 = string.Format(@"SELECT [dbo].[f_getQuantidadeSaldoUsuariosInclusao] ({0})", CodigoEntidade);
        DataSet dsUsrCadastrados = cDados.getDataSet(comandoSQL1);
        if (cDados.DataSetOk(dsUsrCadastrados) && cDados.DataTableOk(dsUsrCadastrados.Tables[0]))
        {
            saldo = int.Parse(dsUsrCadastrados.Tables[0].Rows[0][0].ToString());
        }
        return saldo > 0;
    }

    #endregion
    //protected void ddlUsuarioOrigem_Callback(object sender, CallbackEventArgsBase e)
    //{
    //    populaComboUsuarioOrigem();
    //}


    //    protected void populaComboUsuarioOrigem()
    //    {
    //        usuarioOrigem = "-1";
    //        string comandoSQL = string.Format(@"
    //                select * from 
    //                   (SELECT -1 AS codigoUsuario
    //	                ,'            ------ SELECIONE ----- 'AS nomeUsuario
    //                union 
    //                SELECT u.codigoUsuario, u.nomeUsuario
    //                    FROM Usuario AS u
    //                    INNER JOIN UsuarioUnidadeNegocio AS uun ON (
    //		                    u.CodigoUsuario = uun.CodigoUsuario
    //		                    AND uun.CodigoUnidadeNegocio = {2}
    //		                    )
    //                    INNER JOIN [UnidadeNegocio] AS [un] ON (
    //		                    un.[CodigoUnidadeNegocio] = uun.[CodigoUnidadeNegocio]
    //		                    AND un.[DataExclusao] IS NULL
    //		                    )
    //                    WHERE u.DataExclusao IS NULL
    //	                    AND uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'
    //                        and u.codigoUsuario != {3}
    //                ) lista
    //                order by lista.nomeUsuario     
    //                          ", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade, hfStatusCopiaPermissoes.Contains("CodigoUsuarioDestino") ? hfStatusCopiaPermissoes.Get("CodigoUsuarioDestino") : -1);
    //        DataSet ds = cDados.getDataSet(comandoSQL);

    //        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
    //        {
    //            if (ds.Tables[0].Rows.Count > 1)
    //            {
    //                ddlUsuarioOrigem.DataSource = ds.Tables[0];
    //                ddlUsuarioOrigem.ValueField = "codigoUsuario";
    //                ddlUsuarioOrigem.TextField = "nomeUsuario";
    //                ddlUsuarioOrigem.DataBind();
    //            }
    //            else
    //            {
    //                ddlUsuarioOrigem.Visible = false;
    //            }
    //        }
    //    }
    protected void hfStatusCopiaPermissoes_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistroCopiaPermissao();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatusCopiaPermissoes.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia.ToLower().Contains("suc") == true) // não deu erro durante o processo de persistência
        {
            hfStatusCopiaPermissoes.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            ddlUsuarioOrigem.SelectedIndex = -1;

        }
        else // alguma coisa deu errado...
            hfStatusCopiaPermissoes.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }

    private string persisteEdicaoRegistroCopiaPermissao()
    {
        try
        {
            usuarioDestino = hfStatusCopiaPermissoes.Get("CodigoUsuarioDestino").ToString();
            usuarioOrigem = hfStatusCopiaPermissoes.Get("CodigoUsuarioOrigem").ToString();

            string msgErro = salvaRegistroCopiaPermissao("E", int.Parse(usuarioOrigem), int.Parse(usuarioDestino));

            populaGrid();

            return msgErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string salvaRegistroCopiaPermissao(string modo, int CodigoUsuarioOrigem, int CodigoUsuarioDestino)
    {


        //chamar procedure  p_adm_clonaPermissoesUsuario
        string comandoSQL = string.Format(
                            @"BEGIN
                            DECLARE @retorno varchar(2048)

                            EXEC [dbo].[p_adm_clonaPermissoesUsuario] {0}, {1}, {2}, {3},  @retorno output 

                            SELECT  @retorno

                          END", CodigoUsuarioDestino, CodigoUsuarioOrigem, CodigoEntidade, idUsuarioLogado);
        DataSet ds = cDados.getDataSet(comandoSQL);
        string retorno = ds.Tables[0].Rows[0][0].ToString();
        return retorno;
    }

    protected void ddlUsuarioOrigem_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(CodigoEntidade);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }

    protected void ddlUsuarioOrigem_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string codigoUsuario = hfStatusCopiaPermissoes.Contains("CodigoUsuarioDestino") ? hfStatusCopiaPermissoes.Get("CodigoUsuarioDestino").ToString() : "-9";

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidade, e.Filter, "AND us.CodigoUsuario != " + int.Parse(codigoUsuario));

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }


    protected void ddlUsuarioOrigem_Callback(object sender, CallbackEventArgsBase e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)sender;

        string codigoUsuario = hfStatusCopiaPermissoes.Contains("CodigoUsuarioDestino") ? hfStatusCopiaPermissoes.Get("CodigoUsuarioDestino").ToString() : "-9";

        string where =
               string.Format(@"AND us.CodigoUsuario != {0}", int.Parse(codigoUsuario));

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidade, "", where);

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, 0, 99);
    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }

    protected void pnCPF_Callback(object sender, CallbackEventArgsBase e)
    {
        int codigoUsuario = -1;
        editaCPFUsuario = false;

        string[] listaParametros = e.Parameter.Split('|');

        if (int.TryParse(listaParametros[0], out codigoUsuario) == true)
        {
            string comandoSQL = string.Format(@"
            IF EXISTS(SELECT 1 FROM {0}.{1}.FormularioAssinatura fa 
                         WHERE fa.CodigoUsuario =  {2})
            BEGIN
                SELECT 1
            END 
            ELSE
            BEGIN
                SELECT 0 
            END", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario);
            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                editaCPFUsuario = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "0";
            }
        }

        int codigoEntidadeUsuarioSelecionado = 0;
        bool usuarioEdeOutraEntidade = false;
        if (int.TryParse(listaParametros[2], out codigoEntidadeUsuarioSelecionado) == true)
        {
            usuarioEdeOutraEntidade = CodigoEntidade != codigoEntidadeUsuarioSelecionado;
        }


        ((ASPxCallbackPanel)sender).JSProperties["cp_PodeEditarCPF"] = (editaCPFUsuario == true && listaParametros[1] != "Consultar") ? (usuarioEdeOutraEntidade == true) ? "N" : "S" : "N";
        //txtCPF.ClientEnabled = (editaCPFUsuario == true) && listaParametros[1].ToLower().Trim() != "consultar";

    }
    protected void pnHelp_Callback(object sender, CallbackEventArgsBase e)
    {
        //values[8] + "|" + estado + "|" + values[9]
        string[] listaParametros = e.Parameter.Split('|');
        int codigoEntidadeUsuarioSelecionado = 0;
        
        bool usuarioEdeOutraEntidade = false;
        if (listaParametros.Length > 2 && int.TryParse(listaParametros[2], out codigoEntidadeUsuarioSelecionado) == true)
        {
            usuarioEdeOutraEntidade = CodigoEntidade != codigoEntidadeUsuarioSelecionado;
        }

        if (usuarioEdeOutraEntidade == true)
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_Visivel"] = "N";
        }
        else
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_Visivel"] = (editaCPFUsuario == false && listaParametros[1] == "Editar") ? "S" : "N";        
        }
        
    }
    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CPF")
        {
            long cpf = 0;
            if (long.TryParse(e.CellValue.ToString(), out cpf) == true)
            {
                cpf = long.Parse(e.CellValue.ToString());
                string CPFFormatado = String.Format(@"{0:000\.000\.000\-00}", cpf); //Formatar de Long para cpf
                e.Cell.Text = CPFFormatado;
            }            
        }
    }

    protected void imgStatusAtivacao_Init(object sender, EventArgs e)
    {
        ((ASPxImage)sender).ToolTip = Resources.traducao.CadastroUsuariosNovo_gest_o_de_licen_as_de_uso;
        //((ASPxImage)sender).Visible = verificaLicencaAtiva false;

        DataSet ds = cDados.getParametrosSistema("QuantidadeLicencasCliente");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["QuantidadeLicencasCliente"].ToString()))
                ((ASPxImage)sender).Visible = true;
            else
            {
                ((ASPxImage)sender).Visible = true;
            }
        }
    }

    protected void menu_impedimento_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu),false, "", false, true, false, "CadUsu", "Usuários", this);
    }

    protected void menu_impedimento_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gridViewExporterImpedimento, "CadUsu");
    }



    protected void callbackTelaImpedimento_Callback1(object sender, CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = "";

        try
        {
            DataSet ds = cDados.atualizaUsuarioNovo(txtNomeUsuario.Text
                                                            , txtLogin.Text
                                                            , cmbTipoAutentica.SelectedItem.Value.ToString()
                                                            , txtEmail.Text
                                                            , txtTelefoneContato1.Text, txtTelefoneContato2.Text
                                                            , memObservacoes.Text
                                                            , ckbAtivo.Checked ? "S" : "N", idUsuarioLogado
                                                            , CodigoEntidade, int.Parse(getChavePrimaria()), txtCPF.Text);

            string codigoUsuario = getChavePrimaria();

            string comandoSQL = string.Format(@"
            UPDATE [dbo].[RecursoCorporativo]
               SET [IndicaRecursoAtivo] = 'N'
            WHERE CodigoUsuario = {0}", codigoUsuario);
            int registrosAfetados = 0;
            bool retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
            if (retorno == true)
            {
                ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "Usuário desativado com sucesso!";
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = ex.Message;
        }
    }

    protected void callbackVerificaSeExisteCPF_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_atencao"] = "";
        string[] parametros = e.Parameter.Split('|'); 
        string cpfDigitado = parametros[0];
        string codigoUsuarioEditado = parametros[1];
        string comandoSQL = string.Format(@"
        SELECT count(*) AS quantidade 
         FROM Usuario 
        WHERE CPF = '{0}' 
          AND CodigoUsuario <> {1}", cpfDigitado.Replace(".", "").Replace("-", ""),
          codigoUsuarioEditado);

        DataSet ds = cDados.getDataSet(comandoSQL);
        int quantidade = int.Parse(ds.Tables[0].Rows[0]["quantidade"].ToString());
        if (quantidade > 0)
        {
            ((ASPxCallback)source).JSProperties["cp_atencao"] = "O CPF informado já existe.";
        }

    }
}
