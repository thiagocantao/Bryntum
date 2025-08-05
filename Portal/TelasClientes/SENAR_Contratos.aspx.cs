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

public partial class administracao_CadastroRamosAtividades : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    private int codigoProjeto = 0;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    int codigoPessoaParam = -1;

    string siglaUF = "";

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["idProjeto"] != null && Request.QueryString["idProjeto"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_IncCntrt");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltCntrt");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_ExcCntrt");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {            
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            
        }

        setSiglaUF();

        podeEditar = podeEditar && siglaUF != "";

        podeIncluir = podeIncluir && siglaUF != "";
        podeExcluir = podeExcluir && siglaUF != "";

       carregaGvDados(); 

        carregaGvContratos();

        cDados.aplicaEstiloVisual(Page);
        cDados.setaTamanhoMaximoMemo(txtInformacoesContato, 250, lblContadorInformacoesContato);
        gvContratos.Settings.ShowFilterRow = false;
        gvContratos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvContratos.JSProperties["cp_msg"] = "";
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/SENAR_Contratos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int largura1 = 0;
        int altura1 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);


        alturaPrincipal = altura1;
        int altura = (alturaPrincipal - 135);

        if (altura1 > 0)
            gvDados.Settings.VerticalScrollableHeight = altura1 - 280;
        

        pcDados.Width = (largura1 - 215);
    }
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT * FROM dbo.f_SENAR_GetFornecedoresRegional({0}, {1})", codigoUsuarioResponsavel, codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }        
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {        
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

    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion
    
    #region BANCO DE DADOS

    private void setSiglaUF()
    {
        string comandoSQL = string.Format(@"SELECT dbo.f_SENAR_GetUFUsuarioRegional({0}) AS UF", codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            siglaUF = ds.Tables[0].Rows[0]["UF"].ToString();
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoPessoa = -1;

        string nomePessoa = txtNomePessoa.Text;
        string nomeFantasia = "";
        string tipoPessoa = rbTipoPessoa.Value.ToString();
        string numeroIdentificacao = tipoPessoa == "F" ? txtCPF.Text : txtCNPJ.Text;
        string codigoRamoAtividade = "NULL";
        string email = txtEmail.Text == "" ? "NULL" : "'" + txtEmail.Text.Replace("'", "''") + "'";
        string comentarios = txtInformacoesContato.Text;
        bool result = cDados.incluiFornecedor(codigoEntidadeUsuarioResponsavel, nomePessoa, nomeFantasia, tipoPessoa, numeroIdentificacao, codigoRamoAtividade, "", txtTelefone.Text
            , txtNomeContato.Text, email, "", comentarios, -1,  "N", "S", "N", ref codigoPessoa);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            string comandoSQL = string.Format(@"
            INSERT INTO dbo.[SENAR_FornecedorABC]
                       ([CodigoPessoaFornecedor]
                       ,[CodigoUsuarioInclusao]
                       ,[DataInclusao]
                       ,[NumeroProcesso]
                       ,[SiglaUF]
                       ,[CodigoProjeto])
                 VALUES
                       ({0}
                       ,{1}
                       ,GetDate()
                       ,'{2}'
                       ,'{3}'
                       ,{4})"
            ,codigoPessoa
            ,codigoUsuarioResponsavel
            ,txtNumeroProcesso.Text.Replace("'", "''")
            ,siglaUF
            ,codigoProjeto);

            int regAf = 0;

            result = cDados.execSQL(comandoSQL, ref regAf);

            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoPessoa);

            carregaGvDados();
            codigoPessoaParam = codigoPessoa;
            return "";
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoPessoa = int.Parse(getChavePrimaria());

        string nomePessoa = txtNomePessoa.Text;
        string nomeFantasia = "";
        string tipoPessoa = rbTipoPessoa.Value.ToString();
        string numeroIdentificacao = tipoPessoa == "F" ? txtCPF.Text : txtCNPJ.Text;
        string codigoRamoAtividade = "NULL";
        string email = txtEmail.Text == "" ? "NULL" : "'" + txtEmail.Text.Replace("'", "''") + "'";
        string comentarios = txtInformacoesContato.Text;

        bool result = cDados.atualizaFornecedor(codigoPessoa, nomePessoa, nomeFantasia, tipoPessoa, numeroIdentificacao, codigoRamoAtividade, "", txtTelefone.Value.ToString()
            , txtNomeContato.Text, email, "", comentarios, -1, "N", "S", "N");

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            string comandoSQL = string.Format(@"
                UPDATE dbo.[SENAR_FornecedorABC] SET [NumeroProcesso] = '{2}'
                 WHERE [CodigoPessoaFornecedor] = {0}
                   AND [CodigoProjeto] = {1}"
            , codigoPessoa
            , codigoProjeto
            , txtNumeroProcesso.Text.Replace("'", "''"));

            int regAf = 0;

            result = cDados.execSQL(comandoSQL, ref regAf);
            carregaGvDados();
            codigoPessoaParam = codigoPessoa;
            return "";
        }

    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoPessoa = int.Parse(getChavePrimaria());
               
        bool result = cDados.excluiFornecedorABC(codigoUsuarioResponsavel, codigoPessoa, codigoProjeto);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            codigoPessoaParam = codigoPessoa;
            return "";
        }

    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    #region Contratos

    private void carregaGvContratos()
    {
        int codigoFornecedor = -1;

        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") + "" != "Incluir")
            codigoFornecedor = int.Parse(getChavePrimaria());

            string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoFornecedor INT

            SELECT @CodigoFornecedor = CodigoFornecedorABC
              FROM SENAR_FornecedorABC
             WHERE CodigoPessoaFornecedor = {0}
               AND CodigoProjeto = {1}
               AND DataExclusao IS NULL

            SELECT c.CodigoContrato, c.NumeroContrato, c.DataInicioVigencia, c.DataTerminoVigencia, c.ValorGlobalContrato
              FROM SENAR_ContratoRegionalABC c
             WHERE c.CodigoFornecedorABC = @CodigoFornecedor
               AND c.DataExclusao IS NULL
        END", codigoFornecedor, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvContratos.DataSource = ds;
            gvContratos.DataBind();
        }

        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar" && podeIncluir)
        {
            gvContratos.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""novaLinha();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Novo Contrato""/>                                               
                                            </td><td align=""center"">Contratos</td><td style=""width: 50px""></td></tr></table>";
        }else
        {
            gvContratos.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img src=""../imagens/botoes/incluirRegDes.png"" />                                               
                                            </td><td align=""center"">Contratos</td><td style=""width: 50px""></td></tr></table>";
        }
    }

    #endregion

    protected void gvContratos_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        bool resultInsert = true, resultEdit = true, resultDelete = true;
        string msg = "";

        int regAf = 0;

        string mensagemErro_Persistencia = "";
        string tipoOperacao = hfGeral.Get("TipoOperacao") + "";

        if (tipoOperacao == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (tipoOperacao == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (tipoOperacao == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "")
        {
            for (int i = 0; i < e.DeleteValues.Count; i++)
            {
                string comandoSQL = string.Format(@"
            UPDATE SENAR_ContratoRegionalABC 
               SET CodigoUsuarioExclusao = {1}
                  ,DataExclusao = GetDate()
             WHERE CodigoContrato = {0}", e.DeleteValues[i].Keys[0], codigoUsuarioResponsavel);

                resultDelete = cDados.execSQL(comandoSQL, ref regAf);
            }

            for (int i = 0; i < e.InsertValues.Count; i++)
            {
                string comandoSQL = string.Format(@"
         BEGIN
            DECLARE @CodigoFornecedor INT

            SELECT @CodigoFornecedor = CodigoFornecedorABC
              FROM SENAR_FornecedorABC
             WHERE CodigoPessoaFornecedor = {4}
               AND CodigoProjeto = {6}
               AND DataExclusao IS NULL

           INSERT INTO SENAR_ContratoRegionalABC(NumeroContrato, DataInicioVigencia, DataTerminoVigencia, ValorGlobalContrato, CodigoFornecedorABC, CodigoUsuarioInclusao, DataInclusao)
                                          VALUES('{0}', CONVERT(DateTime, '{1}', 103), CONVERT(DateTime, '{2}', 103), {3}, @CodigoFornecedor, {5}, GetDate())
        END
            ", e.InsertValues[i].NewValues["NumeroContrato"]
                , e.InsertValues[i].NewValues["DataInicioVigencia"]
                , e.InsertValues[i].NewValues["DataTerminoVigencia"]
                , e.InsertValues[i].NewValues["ValorGlobalContrato"].ToString().Replace(",", ".")
                , codigoPessoaParam
                , codigoUsuarioResponsavel
                , codigoProjeto);

                resultInsert = cDados.execSQL(comandoSQL, ref regAf);
            }

            for (int i = 0; i < e.UpdateValues.Count; i++)
            {
                string comandoSQL = string.Format(@"
            UPDATE SENAR_ContratoRegionalABC 
               SET NumeroContrato = '{0}'
                 , DataInicioVigencia = CONVERT(DateTime, '{1}', 103)
                 , DataTerminoVigencia = CONVERT(DateTime, '{2}', 103)
                 , ValorGlobalContrato = {3}
             WHERE CodigoContrato = {4}"
                , e.UpdateValues[i].NewValues["NumeroContrato"]
                , e.UpdateValues[i].NewValues["DataInicioVigencia"]
                , e.UpdateValues[i].NewValues["DataTerminoVigencia"]
                , e.UpdateValues[i].NewValues["ValorGlobalContrato"].ToString().Replace(",", ".")
                , e.UpdateValues[i].Keys[0]);

                resultEdit = cDados.execSQL(comandoSQL, ref regAf);
            }


            if (!resultInsert)
                msg += "Erro ao incluir contrato<br>";
            if (!resultEdit)
                msg += "Erro ao editar contrato<br>";
            if (!resultDelete)
                msg += "Erro ao excluir contrato<br>";
        }
        else
        {
            msg = mensagemErro_Persistencia;
        }

        if (msg == "")
        {
            gvContratos.JSProperties["cp_status"] = "ok";
            gvContratos.JSProperties["cp_msg"] = "Contrato alterado com sucesso!";
            gvContratos.JSProperties["cp_OperacaoOk"] = tipoOperacao;
            carregaGvContratos();
        }
        else
        {
            gvContratos.JSProperties["cp_status"] = "erro";
            gvContratos.JSProperties["cp_msg"] = msg;
        }

        e.Handled = true;
    }

    protected void gvContratos_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete && hfGeral.Contains("TipoOperacao"))
        {
            if (podeExcluir == true && hfGeral.Get("TipoOperacao") + "" != "Consultar")
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/ExcluirReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";

            }
        }

        if (e.ButtonType == ColumnCommandButtonType.Edit && hfGeral.Contains("TipoOperacao"))
        {
            if (podeEditar == true && hfGeral.Get("TipoOperacao") + "" != "Consultar")
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/editarReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
    }
}
