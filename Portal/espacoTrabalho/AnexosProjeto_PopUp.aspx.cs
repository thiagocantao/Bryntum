using System;
using System.Data;
using System.Web;
using System.IO;
using DevExpress.Web;
using System.Collections.Generic;

public partial class _Compartilhada_AnexosProjeto_PopUp : System.Web.UI.Page
{

    protected class ListaDeUnidades
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeUnidades()
        {
            ListaDeCodigos = new List<int>();
            ListaDeNomes = new List<string>();
        }
        public void Clear()
        {
            ListaDeCodigos.Clear();
            ListaDeNomes.Clear();
        }

        /// <summary>
        /// Adiciona um item na lista de unidades
        /// </summary>
        /// <param name="codigoUnidade">Código da unidade a adicionar</param>
        /// <param name="descricaoUnidade">Descrição da unidade a adicionar</param>
        public void Add(int codigoUnidade, string descricaoUnidade)
        {
            ListaDeCodigos.Add(codigoUnidade);
            ListaDeNomes.Add(descricaoUnidade);
        }

        public string GetDescricaoUnidade(int codigoUnidade)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoUnidade);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoUnidade)
        {
            return ListaDeCodigos.Contains(codigoUnidade);
        }

    }

    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;

    string IniciaisTipoAssociacao;
    int IDObjetoAssociado, IDObjetoPai;
    char Origem;
    string ModoOperacao;

    private Int64 tamanhoMaximoArquivoUpload;

    private int? CodigoPastaDestino;
    int CodigoAnexo;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // PARAMETROS:
        // TA = Tipo Associacao, possíveis valores: PR(Projetos)
        // ID = Código Identificador do objeto que receberá a associação, por exemplo o código d o projeto.
        // O = Origem, possíveis valores: Pasta ou Arquivo
        // CPS = Código Pasta Superior
        // CPA = Código Pasta Atual;
        // MO = Modo Operação, possíveis valores: Incluir, Editar
        //CA = CodigoAnexo

        IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;
        IDObjetoPai = (Request.QueryString["IDOP"] != null && Request.QueryString["IDOP"].ToString() != "") ? int.Parse(Request.QueryString["IDOP"].ToString()) : 0;
        Origem = Request.QueryString["O"].ToString()[0];
        int CodigoPastaSuperior = (Request.QueryString["CPS"] != null && Request.QueryString["CPS"].ToString() != "" ? int.Parse(Request.QueryString["CPS"].ToString()) : -1);
        int CodigoPastaAtual = (Request.QueryString["CPA"] != null && Request.QueryString["CPA"].ToString() != "" ? int.Parse(Request.QueryString["CPA"].ToString()) : -1);
        CodigoAnexo = (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" ? int.Parse(Request.QueryString["CA"].ToString()) : -1);

        ModoOperacao = Request.QueryString["MO"].ToString();


        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        // busca o tamanho máximo permitido para upload no parametro
        tamanhoMaximoArquivoUpload = 2; // inicialmente é 2 megas
        DataSet ds = cDados.getParametrosSistema("tamanhoMaximoArquivoAnexoEmMegaBytes");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString() != "")
            tamanhoMaximoArquivoUpload = int.Parse(ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString());
        lblTamanhoMaximo.Text = Resources.traducao.AnexosProjeto_PopUp_tamanho_m_ximo_do_arquivo_em_megabytes_ + string.Format("{0}MB", tamanhoMaximoArquivoUpload);
        // Se  CodigoPastaAtual != -1, o novo arquivo/pasta será colocado abaixo dela (pasta atual)
        if (CodigoPastaAtual != -1)
            CodigoPastaDestino = CodigoPastaAtual;
        // Se CodigoPastaSuperior != -1, o novo arquivo/pasta será colocado abaixo dela (pasta atual) 
        else if (CodigoPastaSuperior != -1)
            CodigoPastaDestino = CodigoPastaSuperior;
        // Se não tem pasta atual e nem pasta superior, o novo arquivo/psata, será colocado abaixo da pasta raiz
        else
            CodigoPastaDestino = null;

        // busca o nome da Pasta de destino
        if (CodigoPastaDestino == null)
            lblPastaDestino.Text = Resources.traducao.AnexosProjeto_PopUp_pasta_raiz;
        else
        {
            DataTable dtAnexo = cDados.getInformacoesAnexo(CodigoPastaDestino.Value).Tables[0];
            if (dtAnexo.Rows.Count > 0)
                lblPastaDestino.Text = dtAnexo.Rows[0]["Nome"].ToString();
            else
            {
                CodigoPastaDestino = null;
                lblPastaDestino.Text = Resources.traducao.AnexosProjeto_PopUp_pasta_raiz;
            }
        }
        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(this);
            if (ModoOperacao == "Editar")
            {
                DataTable dtAnexo = cDados.getInformacoesAnexo(CodigoPastaAtual).Tables[0];
                if (cDados.DataTableOk(dtAnexo))
                {
                    string incluidoPor = dtAnexo.Rows[0]["NomeUsuario"].ToString();
                    string dataInclusao = dtAnexo.Rows[0]["DataInclusao"].ToString();
                    lblIncluidoPor.Text = Resources.traducao.inclu_do_por_ + incluidoPor + " " + Resources.traducao.AnexosProjeto_PopUp_em_ + " " + dataInclusao;
                    if (dtAnexo.Rows[0]["IndicaPasta"].ToString() == "S")
                    {
                        txtNomePasta.Text = dtAnexo.Rows[0]["Nome"].ToString();
                        txtDescricaoNovoAnexo.Text = dtAnexo.Rows[0]["DescricaoAnexo"].ToString();
                        txtPalavraChave.Text = dtAnexo.Rows[0]["PalavraChave"].ToString();
                        ckLink.Checked = dtAnexo.Rows[0]["IndicaAnexoPublicoExterno"].ToString() == "S";
                        lblTamanhoMaximo.Visible = fluArquivo.Visible = false;
                        txtNomePasta.Visible = true;
                        lblOpcaoPastaOrigem.Text = Resources.traducao.AnexosProjeto_PopUp_edi__o_da_pasta_;
                        btnCheckIn.Visible = false;
                        btnCheckOutForEdit.Visible = false;
                    }
                }
                else
                {
                    DataTable dtArquivo = cDados.getInformacoesAnexo(CodigoAnexo).Tables[0];
                    txtNomePasta.Enabled = false;
                    if (cDados.DataTableOk(dtArquivo))
                    {
                        DataRow rowArquivoAnexo = dtArquivo.Rows[0];
                        string incluidoPor = rowArquivoAnexo["NomeUsuario"].ToString();
                        string dataInclusao = rowArquivoAnexo["DataInclusao"].ToString();
                        lblIncluidoPor.Text = Resources.traducao.inclu_do_por_ + incluidoPor + " " + Resources.traducao.AnexosProjeto_PopUp_em_ + " " + dataInclusao;

                        txtNomePasta.Text = rowArquivoAnexo["Nome"].ToString();
                        txtPalavraChave.Text = rowArquivoAnexo["PalavraChave"].ToString();
                        txtDescricaoNovoAnexo.Text = rowArquivoAnexo["DescricaoAnexo"].ToString();
                        lblTamanhoMaximo.Visible = fluArquivo.Visible = false;
                        txtNomePasta.Visible = true;
                        lblOpcaoPastaOrigem.Text = Resources.traducao.AnexosProjeto_PopUp_edi__o_do_anexo_;
                        lblPastaDestino.Text = rowArquivoAnexo["Nome"].ToString();
                        ckLink.Checked = rowArquivoAnexo["IndicaLink"].ToString() == "S";

                        // APENAS ARQUIVOS POSSUEM CONTROLE DE VERSÃO - 18/11/2011 - ACG
                        // Se a dataCheckOut é nula (ninguém esta com o arquivo) ou a dataCheckIn esta preenchida (já devolveram o arquivo) - O arquivo esta disponível para ser pego
                        if (rowArquivoAnexo["dataCheckOut"] == DBNull.Value || rowArquivoAnexo["dataCheckIn"] != DBNull.Value)
                            btnCheckOutForEdit.Visible = true;
                        // o arquivo esta com alguém e só pode ser devolvido por quem pegou
                        else
                        {
                            // mostra as informações de quem está com o arquivo
                            lblCheckoutPor.Text = Resources.traducao.AnexosProjeto_PopUp_arquivo_bloqueado_por + " " + rowArquivoAnexo["nomeUsuarioCheckout"].ToString() + " " + Resources.traducao.AnexosProjeto_PopUp_em_ + " " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", (DateTime)rowArquivoAnexo["dataCheckOut"]);
                            lblCheckoutPor.Visible = true;
                            // se o usuário logado é a mesma pessoa que pegou o arquivo
                            if ((int)rowArquivoAnexo["codigoUsuarioCheckOut"] == codigoUsuarioResponsavel)
                            {
                                btnCheckIn.Visible = true;
                                fluArquivo.Visible = true;
                                btnSalvarNovoAnexo.Visible = false;
                            }
                        }
                        if (dtArquivo.Rows[0]["IndicaPasta"].ToString() == "S")
                        {
                            btnCheckIn.Visible = false;
                            btnCheckOutForEdit.Visible = false;
                            btnSalvarNovoAnexo.Visible = true;
                        }
                    }
                }
            }
        }
        // Ajusta a tela de acordo com o tipo de origem
        if (ModoOperacao != "Editar")
        {
            lblTamanhoMaximo.Visible = fluArquivo.Visible = (Origem == 'A');
            txtNomePasta.Visible = (Origem == 'P');
            lblOpcaoPastaOrigem.Text = (Origem == 'P') ? "Destino da Pasta:" : "Destino do Anexo:";
        }
        cDados.setaTamanhoMaximoMemo(txtDescricaoNovoAnexo, 250, lblContadorMemoNovoAnexo, true);
        cDados.setInfoSistema("UnidadeSelecionadaCombo", codigoEntidadeUsuarioResponsavel);

        ckLink.ClientVisible = (Origem == 'A' && IniciaisTipoAssociacao == "EN");
        ckPastaPublica.ClientVisible = (Origem == 'P');
        if (verificaArquivoLink())
        {
            txtNomePasta.ClientEnabled = false;
            txtPalavraChave.ClientEnabled = false;
            txtDescricaoNovoAnexo.ClientEnabled = false;
            btnSalvarNovoAnexo.ClientVisible = false;
            btnCheckIn.ClientVisible = false;
            btnCheckOutForEdit.ClientVisible = false;
            ckPastaPublica.ClientVisible = false;
        }

        verificaAcesso();
        this.TH(this.TS("AnexosProjeto_PopUp"));

        if (Session["fileLoad"] != null)
        {
            fluArquivo = (ASPxUploadControl)Session["fileLoad"];
            fluArquivo.DataBind();
        }
    }

    private void verificaAcesso()
    {
        if (Origem == 'P')
        {
            string comandoSQL = string.Format(@"
            select * from dbo.f_pax_GetPermissoesAnexoUsuario({0}, {1}, '{2}', {3}, {4}, {5})
           ", codigoEntidadeUsuarioResponsavel, IDObjetoAssociado, IniciaisTipoAssociacao, IDObjetoPai, CodigoAnexo, codigoUsuarioResponsavel);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string alterar = ds.Tables[0].Rows[0]["ValorAcessoAlterar"].ToString();

                if (alterar == "2" || alterar == "6")
                {
                    txtNomePasta.ClientEnabled = false;
                    txtPalavraChave.ClientEnabled = false;
                    txtDescricaoNovoAnexo.ClientEnabled = false;
                    btnSalvarNovoAnexo.ClientVisible = false;
                    btnCheckIn.ClientVisible = false;
                    btnCheckOutForEdit.ClientVisible = false;
                    ckPastaPublica.ClientVisible = false;
                }
            }
        }
    }

    protected void btnSalvarNovoAnexo_Click(object sender, EventArgs e)
    {
        string mensagem = "";
        if (ModoOperacao == "Incluir")
        {
            if (Origem == 'A')
                mensagem = incluiNovoAnexo(false, fluArquivo.UploadedFiles[0]);
            else
                mensagem = incluiNovaPasta();
        }
        if (ModoOperacao == "Editar")
        {
            if (Origem == 'P')
                mensagem = editarPasta();
            else
                mensagem = editarArquivo();
        }

        if (mensagem != "")
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "arquivo",
                "window.top.mostraMensagem('" + mensagem.Replace("'", "").Replace(Environment.NewLine, " ") + "', 'erro', true, false, null);",
                true);
        else
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "arquivo",
                "  window.top.fechaModal3();",
                true);


    }

    private string incluiNovaPasta()
    {
        try
        {
            string nomeNovaPasta = txtNomePasta.Text.Replace("'", "");
            string descricaoNovaPasta = txtDescricaoNovoAnexo.Text.Replace("'", "");
            string palavraChave = txtPalavraChave.Text.Replace("'", "''");
            string indicaPublica = ckPastaPublica.Checked ? "S" : "N";

            cDados.incluirAnexo(descricaoNovaPasta, codigoUsuarioResponsavel.ToString(), nomeNovaPasta, codigoEntidadeUsuarioResponsavel.ToString(), CodigoPastaDestino, 'S', 'N', codigoTipoAssociacao, "NULL", palavraChave, IDObjetoAssociado.ToString(), null, indicaPublica);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private string editarPasta()
    {
        try
        {
            string nomeNovaPasta = txtNomePasta.Text.Replace("'", "");
            string descricaoNovaPasta = txtDescricaoNovoAnexo.Text.Replace("'", "");
            string palavraChave = txtPalavraChave.Text.Replace("'", "''");
            string indicaPublica = ckPastaPublica.Checked ? "S" : "N";

            cDados.atualizaAnexoSistema('S', CodigoAnexo, nomeNovaPasta, descricaoNovaPasta, "NULL", palavraChave, indicaPublica);

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private string editarArquivo()
    {
        try
        {
            //txtNomePasta.Enabled = false;
            string nomeNovoArquivo = txtNomePasta.Text.Replace("'", "");
            string descricaoNovaPasta = txtDescricaoNovoAnexo.Text.Replace("'", "");
            int codigoAnexo1 = (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" ? int.Parse(Request.QueryString["CA"].ToString()) : -1);
            string indicaLink = ckLink.Checked ? "'S'" : "NULL";
            string palavraChave = txtPalavraChave.Text.Replace("'", "''");

            cDados.atualizaAnexoSistema('N', codigoAnexo1, txtNomePasta.Text, descricaoNovaPasta, indicaLink, palavraChave, "N");

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private string incluiNovoAnexo(bool indicaOperacaoCheckin, UploadedFile uploadedFile)
    {
        return incluiNovoAnexo(indicaOperacaoCheckin, uploadedFile.FileName, (int)uploadedFile.ContentLength, uploadedFile.FileBytes);
    }

    private string incluiNovoAnexo(bool indicaOperacaoCheckin, string fileName, int contentLength, byte[] imagemBinario)
    {
        try
        {
            string mensagem = "";
            // VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
            if (imagemBinario != null && contentLength > 0)
            {
                string nomeNovoAnexo = Path.GetFileName(fileName);
                string descricaoNovoAnexo = txtDescricaoNovoAnexo.Text.Replace("'", "");
                string palavraChave = txtPalavraChave.Text.Replace("'", "''");

                if (nomeNovoAnexo.Length > 255)
                {
                    return "O nome do arquivo não pode ter mais que 255 caracteres! Renomeie o arquivo e tente novamente.";
                }

                // VERIFICA A EXTENSÃO DO ARQUIVO.
                string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
                if (extensao == ".exe" || extensao == ".com" || extensao == ".dll")
                    return "O tipo do arquivo não pode ser anexado.";

                // BLOQUEIA A TRANSFERÊNCIA DE ARQUIVOS MAIOR QUE O LIMITE PERMITIDO - VER PARAMETRO
                if (contentLength > 1000000)
                    return string.Format("Limite máximo para arquivo é de 10 MB");

                string indicaLink = ckLink.Checked ? "'S'" : "NULL";

                if (cDados == null)
                {
                    System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

                    listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
                    listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
                    cDados = CdadosUtil.GetCdados(listaParametrosDados);

                    CodigoAnexo = (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" ? int.Parse(Request.QueryString["CA"].ToString()) : -1);
                    codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
                }
                // insere o arquivo no banco de dados
                if (!indicaOperacaoCheckin)
                    mensagem = cDados.incluirAnexo(descricaoNovoAnexo, codigoUsuarioResponsavel.ToString(), nomeNovoAnexo, codigoEntidadeUsuarioResponsavel.ToString(), CodigoPastaDestino, 'N', 'N', codigoTipoAssociacao, indicaLink, palavraChave, IDObjetoAssociado.ToString(), imagemBinario, "N");
                else
                {
                    if (!cDados.registraCheckinArquivoAnexo(CodigoAnexo, codigoUsuarioResponsavel, nomeNovoAnexo, imagemBinario))
                        mensagem = "Erro ao salvar a nova versão";
                }
                if (mensagem != "")
                    return mensagem;
            }
            else
                return "O arquivo não foi informado";

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }




    private void fechaJanelaPopUp()
    {
        string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.fechaModal3();                                 
                                 </script>";

        ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
    }

    #region Métodos DUPLICADOS da tela espacoTrabalho_frameEspacoTrabalho_Biblioteca



    #endregion

    private bool verificaArquivoLink()
    {
        string comandoSQL = string.Format(@"SELECT 1 FROM {0}.{1}.AnexoAssociacao WHERE CodigoAnexo = {2} AND CodigoObjetoAssociado = {3} AND CodigoTipoAssociacao = {4} AND IndicaLinkCompartilhado = 'S'"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , CodigoAnexo
            , IDObjetoAssociado
            , codigoTipoAssociacao);

        DataSet ds = cDados.getDataSet(comandoSQL);

        return cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]);
    }

    protected void fluArquivo_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        e.CallbackData = e.UploadedFile.FileName;
        Session["fileSave"] = e.UploadedFile;
        Session["fileLoad"] = sender;
        Session["fileBytes"] = e.UploadedFile.FileBytes;

        string mensagem = "";
        mensagem = cDados.registraCheckinIncondicionalAnexo(CodigoAnexo, codigoUsuarioResponsavel);
        if (mensagem != "OK")
            ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                "arquivo",
                "window.top.mostraMensagem('" + mensagem.Replace("'", "").Replace(Environment.NewLine, " ") + "', 'erro', true, false, null);",
                true);
        else
        {
            // inclui uma nova versão para o arquivo e libera para edição
            mensagem = incluiNovoAnexo(true, e.UploadedFile);
        }


    }

    protected void callbackCheckoutArquivo_Callback(object source, CallbackEventArgs e)
    {
        int? codigoSequencialAnexo = null;

        cDados.registraCheckoutArquivoAnexo(CodigoAnexo, codigoUsuarioResponsavel);
        string comandoSQL = "";

        comandoSQL = string.Format(@"
            BEGIN
                DECLARE @codigoSequencialAnexo AS bigint;
                DECLARE @numeroUltimaVersao int
                SET @numeroUltimaVersao = (SELECT max(numeroVersao) FROM {0}.{1}.AnexoVersao WHERE codigoAnexo = {2} )
                SELECT IndicaDestinoGravacaoAnexo FROM {0}.{1}.AnexoVersao WHERE CodigoAnexo = {2} AND numeroVersao = @numeroUltimaVersao
            END", cDados.getDbName()
            , cDados.getDbOwner()
            , CodigoAnexo);

        DataSet ds = cDados.getDataSet(comandoSQL);
        string IndicaDestinoGravacaoAnexo = "BD";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            IndicaDestinoGravacaoAnexo = ds.Tables[0].Rows[0]["IndicaDestinoGravacaoAnexo"].ToString();
        }

        string NomeArquivo = "";
        byte[] imagem = cDados.getConteudoAnexo(CodigoAnexo, codigoSequencialAnexo, ref NomeArquivo, IndicaDestinoGravacaoAnexo);
        string tipo = "";
        NomeArquivo = NomeArquivo.Replace("/", "").Replace("\\", "").Replace("*", "").Replace("?", "").Replace(":", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");

        string[] extensaoarray = NomeArquivo.Split('.');
        tipo = extensaoarray[extensaoarray.GetUpperBound(0)];

        ((ASPxCallback)source).JSProperties["cpTipo"] = tipo;
        ((ASPxCallback)source).JSProperties["cpFileName"] = NomeArquivo;
        if (NomeArquivo != "")
        {
            string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + NomeArquivo;
            string arquivo2 = "~/ArquivosTemporarios/" + NomeArquivo;
            FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
            fs.Write(imagem, 0, imagem.Length);
            fs.Close();
            MemoryStream stream = new MemoryStream();
            stream.Write(imagem, 0, imagem.Length);
            Session["exportStream"] = stream;

        }
    }
}
