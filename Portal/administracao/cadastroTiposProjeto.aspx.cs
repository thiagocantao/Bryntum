using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.Web;
using System.Drawing;

public partial class administracao_cadastroTiposProjetos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool exportaOLAPTodosFormatos = false;

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

        //select * from  where 
        string comandoSQL = string.Format(@"
       SELECT codigoentidade  CodigoTipoProjeto
          FROM {0}.{1}.unidadenegocio un
         WHERE codigoentidade = codigounidadenegocio and IndicaUnidadeNegocioAtiva = 'S'
          
        ", cDados.getDbName(), cDados.getDbOwner());

        DataSet ds = cDados.getDataSet(comandoSQL);

        /*
        if ((cDados.DataSetOk(ds) && (ds.Tables[0].Rows.Count > 1)))
        {
            try
            {
                Response.Redirect("~/erros/SemAcesso.aspx");
            }
            catch
            {
                Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcesso.aspx?Mensagem=" + Server.UrlEncode("Não é possivel utilizar a opção de cadastro de tipos de projeto, com mais de uma entidade cadastrada no sistema!");
                Response.End();
            }

        }
        */

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ManterTpPrj");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            hfGeral.Set("tipoArquivo", "XLS");

            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();

        }
        carregaGvDados("");
        
        populaCombo();
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            
        }
        cDados.aplicaEstiloVisual(Page);
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/cadastroTiposProjetos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "cadastroTiposProjetos"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 395;
        //gvDados.Width = new Unit((largura - 10) + "px");
    }
    #endregion

    #region GRID

    private DataSet carregaGvDados(string where)
    {

        string comandoSQL = string.Format(@"
       SELECT   CodigoTipoProjeto
               ,TipoProjeto
               ,case when [IndicaControladoSistema] = 'S' then 'Sim' else 'Não' end as [IndicaControladoSistema]
               ,IndicaTipoProjeto
               ,tp.CodigoTipoAssociacao
               ,ta.DescricaoTipoAssociacao
               ,CodigoCalendarioPadrao
               ,ca.DescricaoCalendario
               ,ToleranciaInicioLBProjeto
          FROM {0}.{1}.TipoProjeto tp inner join
               {0}.{1}.tipoassociacao ta on ta.codigotipoassociacao = tp.codigotipoassociacao left join
               {0}.{1}.calendario ca on tp.CodigoCalendarioPadrao = ca.CodigoCalendario
         WHERE 1 = 1
           {2}
         ", cDados.getDbName(), cDados.getDbOwner(), where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        
        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
        return ds;

    }

    private void populaCombo()
    {
        
        string where = "";
        DataSet ds = cDados.getCalendariosEntidade(codigoEntidadeUsuarioResponsavel, where);
        if (cDados.DataSetOk(ds))
        {
            ddlCalendarioBase.DataSource = ds;
            ddlCalendarioBase.TextField = "DescricaoCalendario";
            ddlCalendarioBase.ValueField = "CodigoCalendario";
            ddlCalendarioBase.DataBind();

            ListEditItem lei = new ListEditItem(Resources.traducao.selecione, "-1");
            ddlCalendarioBase.Items.Insert(0, lei);

            if (!IsPostBack && ddlCalendarioBase.Items.Count > 0)
                ddlCalendarioBase.SelectedIndex = 0;
        }
    }


    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados("");
        if (e.CallbackName != "COLLAPSEROW" && e.CallbackName != "EXPANDROW")
            gvDados.ExpandAll();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string controladoSistema = "Não";
        try
        {
            if (e.VisibleIndex > 0)
            {
                controladoSistema = gvDados.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
            }
                
        }
        catch
        {

        }


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
            if (podeExcluir && controladoSistema == "Não")
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
        carregaGvDados("");
        gvDados.ExpandAll();
    }

    #endregion


    #region banco de dados

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }


    private bool incluiTipoDeProjetos(string tipoDeProjetos, string calendario, string dias, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = string.Format(
        @"
          declare @cod int
          select @cod = case when max(CodigoTipoProjeto) is null then 1 else max(CodigoTipoProjeto)+1 end from TipoProjeto--(xlock) 


            INSERT INTO {0}.{1}.TipoProjeto
           ([CodigoTipoProjeto]
           ,[TipoProjeto]
           ,[IndicaTipoProjeto]
           ,[CodigoTipoAssociacao]
           ,[CodigoCalendarioPadrao]
           ,[ToleranciaInicioLBProjeto]
           ,[IndicaControladoSistema])
          VALUES
           (@cod,'{2}','PRJ',4,{3},{4},'N')",
          cDados.getDbName(), cDados.getDbOwner(),
          tipoDeProjetos, calendario == "-1" ? "null" : calendario, dias == "" ? "null" : dias);

        int registrosAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
            retorno = true;

        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;

    }


    private string persisteInclusaoRegistro()
    {
        string tipoDeProjetos = txtTipoDeProjeto.Text;
        string calendario = ddlCalendarioBase.SelectedItem.Value.ToString();
        string dias = txtAtraso.Text;

        string where = string.Format(" AND TipoProjeto = '{0}' ", tipoDeProjetos);
        string mensagemErro = "";
        DataSet ds = carregaGvDados(where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return Resources.traducao.cadastroTiposProjeto_este_tipo_de_projeto_j__est__cadastrado_no_sistema_;

        bool result = incluiTipoDeProjetos(tipoDeProjetos, calendario, dias,  ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            return "";
        }
    }

    private bool alteraTipoProjetos(int codigotipoprojeto, string tipoDeProjetos, string calendario, string dias, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = string.Format(@"
       UPDATE {0}.{1}.TipoProjeto
         SET TipoProjeto = '{2}',
             CodigoCalendarioPadrao = {3},
             ToleranciaInicioLBProjeto = {4}
      WHERE CodigoTipoProjeto = {5}", cDados.getDbName(), cDados.getDbOwner(), tipoDeProjetos, calendario == "-1" ? "null" : calendario, dias=="" ? "null" : dias, codigotipoprojeto);

        int registrosAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }

        return retorno;

    }

    private string persisteEdicaoRegistro()
    {
        int codigoTipoDeProjetos = int.Parse(getChavePrimaria());
        string calendario = ddlCalendarioBase.SelectedItem.Value.ToString();
        string dias = txtAtraso.Text;
        string tipoDeProjetos = txtTipoDeProjeto.Text;

        string mensagemErro = "";
        DataSet ds = carregaGvDados(string.Format(" AND TipoProjeto = '{0}' AND CodigoTipoProjeto <> {1}", tipoDeProjetos, codigoTipoDeProjetos));
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return Resources.traducao.cadastroTiposProjeto_tipo_de_projeto_j__existe_;

        bool result = alteraTipoProjetos(codigoTipoDeProjetos, tipoDeProjetos, calendario == "-1" ? "null" : calendario, dias == "" ? "null" : dias, ref mensagemErro);

        if (result == false)
            return Resources.traducao.cadastroTiposProjeto_erro_ao_alterar_o_registro__n + mensagemErro;
        else
        {
            return "";
        }
    }

    private string persisteExclusaoRegistro()
    {
        int codigoTipoProjeto = int.Parse(getChavePrimaria());
        string mensagemErro = "";

        string comandoSQL = string.Format(@"
       SELECT   CodigoTipoProjeto
          FROM {0}.{1}.Projeto tp
         WHERE codigotipoprojeto = {2}
          
        ", cDados.getDbName(), cDados.getDbOwner(), codigoTipoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds) && (ds.Tables[0].Rows.Count > 0)))
        {
            return Resources.traducao.cadastroTiposProjeto_n_o___poss_vel_excluir_o_registro_pois_existem_projetos_relacionados__n_ + mensagemErro;
        }

        bool result = excluiTipoDeProjetos(codigoTipoProjeto, ref mensagemErro);

        if (result == false)
            return Resources.traducao.cadastroTiposProjeto_erro_ao_excluir_o_registro__n_ + mensagemErro;
        else
        {
            carregaGvDados("");
            return "";
        }

    }

    private bool excluiTipoDeProjetos(int codigoTipoProjeto, ref string mensagemErro)
    {
        bool retorno = false;

        string comandoSQL = string.Format(
        @"
          DELETE 
          FROM {0}.{1}.TipoProjeto
          WHERE CodigoTipoProjeto = {2}

          DELETE FROM {0}.{1}.[Carteira] 
				WHERE [CodigoEntidade]	= {3}
					AND [IniciaisCarteiraControladaSistema]	= 'TP'
					AND [CodigoObjeto] = {2}

         ", cDados.getDbName(), cDados.getDbOwner(), codigoTipoProjeto, codigoEntidadeUsuarioResponsavel);

        int registrosAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }

        return retorno;
    }

    #endregion
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                gvDados.Columns[0].Visible = false;
                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "TiposDeProjeto_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxGridViewExporter1.WritePdfToResponse(p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "TiposDeProjeto_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Text;
                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    app = "application/ms-excel";
                }
                gvDados.Columns[0].Visible = true;
            }
            catch (Exception ex)
            {
                gvDados.Columns[0].Visible = true;
                erro = ex.Message;// "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo = "\"" + nomeArquivo + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
            else
            {
                /*string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";*/
                string script = String.Format(@"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Não foi possível exportar\n{0}', 'erro', true, false, null);</script>", erro);

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
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

        if (e.RowType == GridViewRowType.Data)
        {
            string eControlado = gvDados.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
            if (eControlado == "Sim")
            {
                //e.BrickStyle.BackColor = Color.FromName("#DDFFCC");
                //e.BrickStyle.BackColor = Color.FromArgb(221, 255, 204);
                e.BrickStyle.ForeColor = Color.FromName("#619340");
            }

        }
    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liXLS = new ListEditItem("XLS", "XLS");
        liXLS.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";

        ddlExporta.Items.Add(liXLS);
        ddlExporta.ClientEnabled = false;

        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);
        }

        ddlExporta.SelectedIndex = 0;
    }

    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        setImagenExportacao(parametro);
    }

    private void setImagenExportacao(string opcao)
    {
        string nomeArquivo = "";

        if (opcao == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (opcao == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (opcao == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (opcao == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (opcao == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string tipoProjetosControladaPeloSistema = e.GetValue("IndicaControladoSistema").ToString();

            if (tipoProjetosControladaPeloSistema == "Sim")
            {
                //e.Row.BackColor = Color.FromName("#DDFFCC");
                //e.Row.ForeColor = Color.Black;
                e.Row.ForeColor = Color.FromName("#619340");
            }
        }
    }


    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadModFlx");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadModFlx", lblTituloTela.Text, this);
    }
}