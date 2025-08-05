/* OBSERVAÇÕES
 * Author: Alejandro Fuentes 
 * Date: 18 / 11 / 2009
 * 
 * MUDANÇA
 * 
 * 10/05/2010 :: ~ :  Addição do control Menu 'BarraNavegacao1', função: MenuUsuarioLogado().
 * 21/03/2011 :: Alejandro : - Adiccionar o controle do botão de Permissão [EN_AdmPrs].
 * 05/04/2011 :: Alejandro : - O dado indicado como [CodigoReservado] não e obrigatório, quando não seja especificado
 *                             salvar no banco de dados como 'NULL'.
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
using System.IO;
using System.Drawing;
using System.Globalization;

public partial class administracao_cadastroEntidades : System.Web.UI.Page
{
    public dados cDados;
    DataSet dsEmail; //Testar Email do usuario Administrador.

    private int idUsuarioLogado = 0;
    private int alturaPrincipal = 0;
    private int codigoEntidade = 0;
    private int idObjetoPai = 0;

    private string resolucaoCliente = "";
    private string dbName;
    private string dbOwner;
    public string definicaoEntidade = "";
    public string definicaoEntidadePlural = "";


    private string emailDoAdministrador = "";

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;

    public byte[] vetorBytes;

    string dataAtual = "";

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_Cad");
        }

        setPermissoesTela();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!hfGeral.Contains("definicaoEntidadePlural"))
        {
            hfGeral.Set("definicaoEntidadePlural", Resources.traducao.entidades);
        }

        if (!hfGeral.Contains("definicaoEntidade"))
        {
            hfGeral.Set("definicaoEntidade", Resources.traducao.entidade);
        }

        gvDados.JSProperties["cp_StatusSalvar"] = "-1";

        //Seteo tela
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        populaUF("");
        populaGrid();
        cDados.aplicaEstiloVisual(Page);
        //pnCallback.SettingsLoadingPanel.Enabled = false;
        //pnCallback.SettingsLoadingPanel.ShowImage = false;

        //pnLogo.SettingsLoadingPanel.Enabled = false;
        //pnLogo.SettingsLoadingPanel.ShowImage = false;

        //pnCallbackFormulario.SettingsLoadingPanel.Enabled = false;
        //pnCallbackFormulario.SettingsLoadingPanel.ShowImage = false;
        

        if (!IsPostBack)
        {
            hfGeral.Set("DataAtual", DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado);
            hfGeral.Set("NomeArquivo", "");
            hfGeral.Set("TipoOperacao", "");            

            if (!IsCallback)
            {                
                DataSet ds = cDados.getDefinicaoEntidade(codigoEntidade);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    definicaoEntidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
                    definicaoEntidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
                    hfGeral.Set("definicaoEntidade", definicaoEntidade);
                    hfGeral.Set("definicaoEntidadePlural", definicaoEntidadePlural);
                }

                if (definicaoEntidade != "") 
                    setarDefinicaoEntidade(definicaoEntidade);

                hfGeral.Set("codigoEntidadeLogada", codigoEntidade);
            }

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        dataAtual = hfGeral.Get("DataAtual").ToString(); 
        this.Title = cDados.getNomeSistema();

        gvDados.JSProperties["cp_OperacaoOk"] = "";
        gvDados.JSProperties["cp_StatusSalvar"] = "";
        //gvDados.Columns["sdfdf"]
        cDados.setaTamanhoMaximoMemo(memObservacoes, 250, lblContadorMemo);
    }

    #region GRID

    private void populaGrid()
    {
        bool temRegistros = false;
        string where = string.Format(@"
                AND e.CodigoUnidadeNegocio = e.CodigoEntidade 
                AND (e.CodigoUnidadeNegocioSuperior = {0} OR e.CodigoEntidade = {0})

                --AND (e.CodigoUnidadeNegocioSuperior = {0} OR e.CodigoUnidadeNegocio = {0})
                --AND e.CodigoEntidade = {0}", codigoEntidade);

        DataSet ds = cDados.getEntidades(where);

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        if (temRegistros = gvDados.VisibleRowCount <= 0)
        {
            string trataDefinicao = hfGeral.Contains("definicaoEntidadePlural") ? hfGeral.Get("definicaoEntidadePlural").ToString() : Resources.traducao.entidade;
            gvDados.SettingsText.EmptyDataRow = "Não há " + trataDefinicao + " Disponíveis...!";
        }
            
    }

    protected void grid_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        
    }

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        
        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeAtivo = e.GetValue("IndicaUnidadeNegocioAtiva").ToString();

            if (unidadeAtivo == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        int idEntidadLinha = -1;
        if (gvDados.GetRowValues(e.VisibleIndex, "CodigoUnidadeNegocio") != null)
        idEntidadLinha = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "CodigoUnidadeNegocio").ToString());

        bool acessaGoogleMaps = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoEntidade, "EN", "EN_GoogleMaps");

        if (e.ButtonID == "btnEditar")
        {
            if (!podeEditar)
            {
                e.Text = "Edição";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        else if (e.ButtonID == "btnExcluir")
        {
            if (!podeExcluir || (idEntidadLinha == codigoEntidade))
            {
                e.Text = "Excluir";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID == "btnCompartilharCustom")
        {
            if (idEntidadLinha == codigoEntidade)
            {
                int permissoes = 0;
                DataSet ds = cDados.getDataSet(string.Format(@"
                            SELECT {0}.{1}.f_VerificaAcessoConcedido({2} , {3}, {3}, NULL, 'EN', 0, NULL, 'EN_AdmPrs') * 8	AS [Permissoes]
                            ", dbName, dbOwner, idUsuarioLogado, codigoEntidade));
                permissoes = int.Parse(ds.Tables[0].Rows[0]["Permissoes"].ToString());
                podePermissao = (permissoes & 8) > 0;

                if (!podePermissao)
                {
                    e.Text = "Permissões";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                }
            }
            else
            {
                e.Text = "Permissões";
                e.Enabled = false;
                e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
            }
        }
        else if (e.ButtonID == "btnGoogleMaps" && acessaGoogleMaps == true && e.CellType == GridViewTableCommandCellType.Data)
        {
            if (acessaGoogleMaps)
            {
                bool exibeBotaoGoogleMaps = false;
                DataSet ds = cDados.getParametrosSistema(codigoEntidade, "visualizaIndGoogleMaps");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    exibeBotaoGoogleMaps = (ds.Tables[0].Rows[0]["visualizaIndGoogleMaps"].ToString() == "S");
                }
                if (exibeBotaoGoogleMaps == true)
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }

    }

    #endregion

    #region COMBOBOX

    private void populaUF(string whereuf)
    {
        DataSet dsUF = cDados.getUF(whereuf);
        if (cDados.DataSetOk(dsUF) && cDados.DataTableOk(dsUF.Tables[0]))
        {
            comboUF.DataSource = cDados.getUF(whereuf);
            comboUF.ValueField = "SiglaUF";
            comboUF.TextField = "ComboUF"; // "SiglaUF";
            comboUF.DataBind();
        }
    }

    #endregion

    #region CALLBACK

    protected void painelCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        
    }

    public static byte[] StrToByteArray(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        return encoding.GetBytes(str);
    }

    protected Byte[] StreamToByteArray(Stream stream)
    {
        Int32 streamLength = (Int32)stream.Length;
        Byte[] byteArray = new Byte[streamLength];
        stream.Read(byteArray, 0, streamLength);
        stream.Close();

        return byteArray;
    }


    protected void pnLogo_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();
        
        if (parametro == "Limpar")
        {
            imageLogo.ContentBytes = null;
        }
        else
        {
            if (parametro == "SE")
            {
                string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                StreamReader img = new StreamReader(pathImg);
                Byte[] stream = StreamToByteArray(img.BaseStream);
                imageLogo.ContentBytes = stream;
            }
            else
            {
                if (hfGeral.Contains("CodigoUnidade"))
                {
                    DataSet dsLogo = cDados.getLogoEntidade(int.Parse(hfGeral.Get("CodigoUnidade").ToString()), "");
                    if (cDados.DataSetOk(dsLogo) && cDados.DataTableOk(dsLogo.Tables[0]))
                    {
                        try
                        {
                            imageLogo.ContentBytes = (byte[])dsLogo.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                        }
                        catch { }
                    }
                }
            }
        }
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        
    }

    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Setea o Encabecado da página
        string comando = string.Format(@"<script type='text/javascript'>logoVisible('none');</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);

        //Header.Controls.Add(cDados.getLiteral(@"<script type='text/javascript' language='javascript' >logoVisible('none');</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Cadastro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/cadastroEntidades.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "Cadastro", "cadastroEntidades"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=true&libraries=places""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places""></script>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 170;
    }

    protected void LogoUpload_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string dataHora = dataAtual + e.UploadedFile.FileName;
        byte[] imagem = e.UploadedFile.FileBytes;

        e.CallbackData = e.UploadedFile.FileName;

        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + e.UploadedFile.FileName;
        FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
        fs.Write(imagem, 0, imagem.Length);
        fs.Close(); 
    }

    private void setarDefinicaoEntidade(string definicaoEntidade)
    {
        //lblNomeEntidade.Text = "Nome da " + definicaoEntidade;
        hfGeral.Set("definicaoEntidade", definicaoEntidade);
        ckbEntidadAtiva.Text = definicaoEntidade + " Ativa?";
        rpAdministrador.HeaderText = "Administrador da " + definicaoEntidade + ":";
        LogoUpload.NullText = "Escolher LogoMarca da " + definicaoEntidade + ":";
        gvDados.Columns["NomeUnidadeNegocio"].Caption = definicaoEntidade;
        
        pcEntidadActual.HeaderText = string.Format(@Resources.traducao.cadastroEntidades_incluir_usu_rio_na__0__atual_, definicaoEntidade);
        lblNomeEntidade.Text = "Nome da " + definicaoEntidade + ":";
        lblMsgIncluirEntidadAtual.Text = string.Format(@Resources.traducao.cadastroEntidades_aten__o__este_email___de_um_usu_rio_de_outra__0___deseja_incluir_o_usu_rio_nesta__0__tamb_m_, definicaoEntidade);
        pcDados.HeaderText = "Detalhes da " + definicaoEntidade;
    }

    private void setPermissoesTela()
    {
        string podeIncluirEntidade = cDados.getPodeIncluirExcluir(codigoEntidade.ToString());
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoEntidade, codigoEntidade, idObjetoPai, "EN", "EN_Cad", "EN_IncEnt");

        if (cDados.DataSetOk(ds))
        {
            if (podeIncluirEntidade.Equals("SIM"))
                podeIncluir = int.Parse(ds.Tables[0].Rows[0]["EN_IncEnt"].ToString()) > 0;
            podeEditar = int.Parse(ds.Tables[0].Rows[0]["EN_Cad"].ToString()) > 0;
            podeExcluir = int.Parse(ds.Tables[0].Rows[0]["EN_Cad"].ToString()) > 0;
        }
    }

    #endregion

    #region Registros

    private void executaExclusao()
    {

    }

    #endregion

    #region BANCOS DE DADOS

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro.
    {
        int regAfetados = 0;
        string msg = "";
        string codigoUnidade = "";
        DataSet ds;

        //obtem Dados del formulario
        string nomeUsuario = txtNome.Text.Replace("'", "''");
        string senhaUsuario = cDados.ObtemCodigoHash("12345678").ToString();
        string emailUsuario = txtEmail.Text.Replace("'", "''");
        string fonoUsuario = txtFone.Text;
        string nomeEntidade = txtEntidade.Text.Replace("'", "''");
        string siglaEntidade = txtSigla.Text.Replace("'", "''");
        string ufEntidade = comboUF.Value != null ? "'" + comboUF.Value.ToString() + "'" : "NULL";
        string ativaEntidade = ckbEntidadAtiva.Checked ? "S" : "N";
        string obsEntidade = memObservacoes.Text.Replace("'", "''");
        string codigoReservado = txtCodigoReservado.Text.Replace("'", "''");

        try
        {
            DataSet dsEntidades = cDados.getEntidades(string.Format(" AND (e.SiglaUnidadeNegocio = '{0}' OR e.NomeUnidadeNegocio = '{1}' OR (e.CodigoReservado <> '' AND e.CodigoReservado = '{2}'))", siglaEntidade, nomeEntidade, codigoReservado));

            if (dsEntidades.Tables[0].Rows.Count > 0)
                return Resources.traducao.cadastroEntidades_j__existe_uma_entidade_com_esse_nome__sigla_ou_c_digo_reservado_;

            //1ro) vejo que o usuario existe...
            int? CodigoUsuarioexisteEmail = cDados.existeEmailUsuarioID(txtEmail.Text);
            if (CodigoUsuarioexisteEmail.Value == 0)
                CodigoUsuarioexisteEmail = null;

            //3do) chamo as funcoes que corresponda... no 9no parametro faiz diferencia sim o usuario existe ao inserir ele.
            ds = cDados.incluiEntidade(nomeUsuario, senhaUsuario, emailUsuario, 
                                       fonoUsuario, idUsuarioLogado, codigoEntidade,
                                       nomeEntidade, siglaEntidade, CodigoUsuarioexisteEmail, 
                                       ufEntidade, ativaEntidade, obsEntidade, codigoEntidade, 
                                       emailDoAdministrador,codigoReservado, ref regAfetados, ref msg);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUnidade = ds.Tables[0].Rows[0]["codigoUnidade"].ToString();

                //--------------------------Cargo a imagen da LogoMarca
                if (hfGeral.Get("NomeArquivo").ToString() != "")
                {
                    string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                    StreamReader img = new StreamReader(pathImg);
                    Byte[] stream = StreamToByteArray(img.BaseStream);

                    ////verifica o tamanho da imagem
                    int tamanhoImagem = stream.Length;

                    byte[] imagemBinario = stream;

                    //pra cargar imagen no banco, se faz uso do pasaje de parametros, de ese jeito, permite
                    //indicar o tipo de dado qeu se esta pasando.
                    cDados.atualizaLogoUnidade(int.Parse(codigoUnidade), imagemBinario);
                }
                populaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoEntidade);
                gvDados.ClientVisible = false;
            }
            msg = Resources.traducao.cadastroEntidades_entidade_salva_com_sucesso_;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            return ex.Message;
        }
        return msg;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro.
    {
        int regAfetados = 0;

        string msg = "";
        string senha = "12345678";
        string codigoUnidade = getChavePrimaria();

        DataSet ds;

        //obtem Dados del formulario
        string nomeUsuario = txtNome.Text.Replace("'", "''");
        string senhaUsuario = cDados.ObtemCodigoHash(senha).ToString();
        string emailUsuario = txtEmail.Text.Replace("'", "''");
        string fonoUsuario = txtFone.Text;
        int codigoUnidadeSuperior = int.Parse(hfGeral.Get("hfCodigoUnidadeNegocio").ToString());
        string nomeEntidade = txtEntidade.Text.Replace("'", "''");
        string siglaEntidade = txtSigla.Text.Replace("'", "''");
        string ufEntidade = (comboUF.Value == null) ? "NULL" : "'" + comboUF.Value.ToString() + "'";
        string ativaEntidade = ckbEntidadAtiva.Checked ? "S" : "N";
        string obsEntidade = memObservacoes.Text.Replace("'", "''");
        string codigoReservado = txtCodigoReservado.Text.Replace("'", "''");

        try
        {
            //1ro) vejo que o usuario existe...
            int? CodigoUsuarioexisteEmail = cDados.existeEmailUsuarioID(txtEmail.Text);
            if (CodigoUsuarioexisteEmail.Value == 0)
                CodigoUsuarioexisteEmail = null;

            //3do) chamo as funcoes que corresponda... no 9no parametro faiz diferencia sim o usuario existe ao inserir ele.
            ds = cDados.atualizaEntidade(nomeUsuario, senhaUsuario, emailUsuario, fonoUsuario, idUsuarioLogado,
                                         codigoUnidadeSuperior, nomeEntidade, siglaEntidade, CodigoUsuarioexisteEmail,
                                         ufEntidade, ativaEntidade, obsEntidade, codigoReservado, ref regAfetados, ref msg);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUnidade = ds.Tables[0].Rows[0]["codigoUnidade"].ToString();

                //--------------------------Cargo a imagen da LogoMarca

                //Stream imagem = LogoUpload.UploadedFiles[0].PostedFile.InputStream;
                if (hfGeral.Get("NomeArquivo").ToString() != "")
                {
                    string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                    StreamReader img = new StreamReader(pathImg);
                    Byte[] stream = StreamToByteArray(img.BaseStream);

                    ////verifica o tamanho da imagem
                    int tamanhoImagem = stream.Length;

                    byte[] imagemBinario = stream;

                    //pra cargar imagen no banco, se faz uso do pasaje de parametros, de ese jeito, permite
                    //indicar o tipo de dado qeu se esta pasando.
                    cDados.atualizaLogoUnidade(int.Parse(codigoUnidade), imagemBinario);
                }
                populaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUnidade);
                gvDados.ClientVisible = false;
            }
            msg = Resources.traducao.cadastroEntidades_dados_gravados_com_sucesso;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            return ex.Message;
        }
        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro.
    {
        string msg = "";
        string chave = getChavePrimaria();

        int regAfetados = 0;

        cDados.getPodeExcluirSimUsuario(int.Parse(chave), ref regAfetados);

        if (regAfetados == 0)
        {
            cDados.excluiEntidade(int.Parse(chave), idUsuarioLogado, ref regAfetados, ref msg);

            msg = msg.Replace("unidades", hfGeral.Contains("definicaoEntidadePlural") ? hfGeral.Get("definicaoEntidadePlural").ToString() : "Entidades");
            msg = msg.Replace("unidade", hfGeral.Contains("definicaoEntidade") ? hfGeral.Get("definicaoEntidade").ToString() : "Entidade");

            populaGrid();
        }
        else
        {
            msg = Resources.traducao.cadastroEntidades_esta_unidade_possui_usu_rios_ativos__n_o_ser__poss_vel_exclu__la_;
            msg = msg.Replace("unidade", hfGeral.Contains("definicaoEntidade") ? hfGeral.Get("definicaoEntidade").ToString() : "Entidade");
        }

        return msg;
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
   (*) situacaoEmail : 'NO' -> NOVO, email inexistente no banco de dados.
    *                  'EX' -> EXcluido.
    *                  'OE' -> ATivo Outra Entidade.    
    *                  'NE' -> ATivo Nesta Entidade.
    */

    protected void hfEmailAdministrador_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();
        string exclusaoEmail = "";
        string codigoUnidadeEmail = "";

        hfEmailAdministrador.Set("emailAdministrador", "NO");

        if ("verificar" == parametro)
        {
            dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, codigoEntidade.ToString());

            if (cDados.DataSetOk(dsEmail))
            {
                if (dsEmail.Tables[0].Rows.Count > 0)
                {
                    codigoUnidadeEmail = dsEmail.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString();
                    exclusaoEmail = dsEmail.Tables[0].Rows[0]["DataExclusao"].ToString();

                    if ("" != exclusaoEmail)
                        hfEmailAdministrador.Set("emailAdministrador", "EX");
                    else
                    {
                        hfEmailAdministrador.Set("emailAdministrador", "OE");
                        foreach (DataRow drEntidade in dsEmail.Tables[0].Rows)
                        {
                            string unidadeNegocioEmail = drEntidade["CodigoUnidadeNegocio"].ToString();
                            if (codigoUnidadeEmail == codigoEntidade.ToString())
                            {
                                hfEmailAdministrador.Set("emailAdministrador", "NE");
                                break;
                            }
                        }
                    }
                }// end if (dsEmail.Tables[0].Rows.Count > 0)
            }// end if (cDados.DataSetOk(dsEmail))
        }
        else if ("verificarEditar" == parametro)
        {
            string codigoEntidadEditar = hfGeral.Get("hfCodigoUnidadeNegocio").ToString();
            dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, codigoEntidadEditar);
            
            if (cDados.DataSetOk(dsEmail))
            {
                if (dsEmail.Tables[0].Rows.Count > 0)
                {
                    codigoUnidadeEmail = dsEmail.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString();

                    if ("" != codigoUnidadeEmail)
                        hfEmailAdministrador.Set("emailAdministrador", "NE");
                }// end if (dsEmail.Tables[0].Rows.Count > 0)
            }// end if (cDados.DataSetOk(dsEmail))
        }
    }
    
    protected void pnCallbackFormulario_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;

        if ("EX" == parametro)
            cargarCamposDesdeEmail();
        else if( "OE" == parametro)
            cargarCamposDesdeEmail();
        else if ("NO" == parametro)
            cargarCamposVacios();
        else if ("NE" == parametro)
            cargarCamposDesdeEmail();
    }

    private void cargarCamposDesdeEmail()
    {
        dsEmail = cDados.getVerificarEmailUsuarioCadastro(txtEmail.Text, codigoEntidade.ToString());

        txtNome.Text = dsEmail.Tables[0].Rows[0]["NomeUsuario"].ToString();
        txtFone.Text = dsEmail.Tables[0].Rows[0]["TelefoneContato1"].ToString();
    }

    private void cargarCamposVacios()
    {
        txtNome.Text = "";
        txtFone.Text = "";
    }

    #endregion

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    { 
        if (hfEmailAdministrador.Contains("emailAdministrador"))
            emailDoAdministrador = hfEmailAdministrador.Get("emailAdministrador").ToString();

        string mensagemErro_Persistencia = "";
        if (e.Parameters.ToString() == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters.ToString() == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameters.ToString() == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            gvDados.JSProperties["cp_StatusSalvar"] = "1"; // 1 indica que foi salvo com sucesso.
            gvDados.JSProperties["cp_OperacaoOk"] = e.Parameters.ToString();
        }
        else // alguma coisa deu errado...
        {
            gvDados.JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
            gvDados.JSProperties["cp_StatusSalvar"] = "0"; // 1 indica que foi salvo com sucesso.
        }        
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CatPrj", "Cadastro de " + definicaoEntidadePlural, this);
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

        if (e.Column != null && e.Column.Name == "IndicaUnidadeNegocioAtiva")
        {
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Text.Trim() == "S")
            {
                e.Text = "Sim";
                e.TextValue = "Sim";
            }
            else if (e.Text.Trim() == "N")
            {
                e.Text = "Não";
                e.TextValue = "Não";
            }
        }
        if (e.Column != null && e.Column.Name == "SiglaUF")
        {
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        }
        
    }
    protected void callback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string[] strParametro = e.Parameter.Split(';');

        bool retorno = false;

        int codigoUnidadeNegocio = int.Parse(strParametro[0]);
        string latitude = strParametro[1];
        string longitude = strParametro[2];

        string mensagemErro = "";
        try
        {
            string comandoSQL = string.Format(@"
            UPDATE {0}.{1}.UnidadeNegocio
               SET Latitude = {2}
                  ,Longitude = {3}
             WHERE CodigoUnidadeNegocio = {4}", cDados.getDbName(), cDados.getDbOwner(), latitude, longitude, codigoUnidadeNegocio);
            int registrosAfetados = 0;
            retorno = cDados.classeDados.execSQL(comandoSQL, ref registrosAfetados);

        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }

        callback1.JSProperties.Add("cp_Sucesso", (retorno == true) ? "S" : "N");
        callback1.JSProperties.Add("cp_Erro", mensagemErro);
    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
    public string SetIniciaisMaiusculas(string myString)
    {
        string retorno = "";
        // Creates a TextInfo based on the "en-US" culture.
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        retorno = myTI.ToTitleCase(myString);
        return retorno;
    }
}
