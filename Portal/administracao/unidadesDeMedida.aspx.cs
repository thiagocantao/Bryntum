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

public partial class administracao_unidadesDeMedida : System.Web.UI.Page
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

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ManterUnMed");
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
        }

        carregaGvDados("");

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }


        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/unidadesDeMedida.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "unidadesDeMedida", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 320;
        gvDados.Width = new Unit((largura - 10) + "px");
    }
    #endregion

    #region GRID

    private DataSet carregaGvDados(string where)
    {

        string comandoSQL = string.Format(@"
        SELECT CodigoUnidadeMedida
              ,DescricaoUnidadeMedida_PT
              ,DescricaoUnidadeMedida_EN
              ,DescricaoUnidadeMedida_ES
              ,SiglaUnidadeMedida
              ,FatorMultiplicador
              ,IndicaControladoSistema
         FROM {0}.{1}.TipoUnidadeMedida WHERE (1 = 1)
          {2}
          order by DescricaoUnidadeMedida_PT asc", cDados.getDbName(), cDados.getDbOwner(), where);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
        return ds;
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados("");
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string controladoSistema = "N";
        try
        {
            if(e.VisibleIndex > 0)
            {
                controladoSistema = gvDados.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
            }            
        }
        catch
        {
        }


        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar && controladoSistema == "N")
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
            if (podeExcluir && controladoSistema == "N")
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


    private bool incluiUnidadeMedida(string unidadeMedida, string sigla, string fatorMultiplicador, ref string mensagemErro)
    {
        string fatorMultiplicadorAux = fatorMultiplicador.Replace(",", ".");
        bool retorno = false;
        string comandoSQL = string.Format(
        @"INSERT INTO {0}.{1}.TipoUnidadeMedida
                      (DescricaoUnidadeMedida_PT
                       ,DescricaoUnidadeMedida_EN
                       ,DescricaoUnidadeMedida_ES
                       ,SiglaUnidadeMedida
                       ,FatorMultiplicador
                       ,IndicaControladoSistema)
                 VALUES(/*DescricaoUnidadeMedida_PT*/'{2}',
                        /*DescricaoUnidadeMedida_EN*/'{3}',
                        /*DescricaoUnidadeMedida_ES*/'{4}',
                        /*SiglaUnidadeMedida*/'{5}',
                        /*FatorMultiplicador*/{6},
                        /*IndicaControladoSistema*/'N')",
          cDados.getDbName(), cDados.getDbOwner(),
            /*'{2}'*/unidadeMedida,/*'{3}'*/unidadeMedida,/*'{4}'*/unidadeMedida, /*'{5}'*/sigla,/*{6}*/fatorMultiplicadorAux);

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
        string unidadeMedida = txtUnidade.Text;
        string sigla = txtSigla.Text;
        string fatorMultiplicador = spnFatorMultiplicador.Text;
        string where = string.Format(" AND DescricaoUnidadeMedida_PT LIKE '{0}' ", unidadeMedida);
        string mensagemErro = "";
        DataSet ds = carregaGvDados(where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return Resources.traducao.unidadesDeMedida_esta_unidade_de_medida_j__est__cadastrada_no_sistema_;

        bool result = incluiUnidadeMedida(unidadeMedida, sigla, fatorMultiplicador, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            return "";
        }
    }

    private bool alteraUnidadeMedida(string descricaoUnidadeMedida_PT, string siglaUnidadeMedida, int codigoUnidadeMedida, string fatorMultiplicador, ref string mensagemErro)
    {
        string fatorMultiplicadorAux = fatorMultiplicador.Replace(",", ".");
        bool retorno = false;
        string comandoSQL = string.Format(@"
        UPDATE {0}.{1}.TipoUnidadeMedida
           SET DescricaoUnidadeMedida_PT = '{2}'
               ,DescricaoUnidadeMedida_EN = '{2}'
               ,DescricaoUnidadeMedida_ES = '{2}'
               ,SiglaUnidadeMedida = '{3}'
               ,FatorMultiplicador = {4}
         WHERE CodigoUnidadeMedida = {5}", cDados.getDbName(), cDados.getDbOwner(), descricaoUnidadeMedida_PT, siglaUnidadeMedida, fatorMultiplicadorAux, codigoUnidadeMedida);

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
        int codigoUnidadeMedida = int.Parse(getChavePrimaria());

        string unidadeMedida = txtUnidade.Text;
        string sigla = txtSigla.Text;
        string fatorMultiplicador = spnFatorMultiplicador.Text;
        string mensagemErro = "";
        DataSet ds = carregaGvDados(string.Format(" AND DescricaoUnidadeMedida_PT = '{0}' AND CodigoUnidadeMedida <> {1}", unidadeMedida, codigoUnidadeMedida));
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return Resources.traducao.unidadesDeMedida_nome_de_unidade_de_medida_j__existe_;

        bool result = alteraUnidadeMedida(unidadeMedida, sigla, codigoUnidadeMedida, fatorMultiplicador, ref mensagemErro);

        if (result == false)
            return Resources.traducao.unidadesDeMedida_erro_ao_salvar_o_registro_ + mensagemErro;
        else
        {
            return "";
        }
    }

    private string persisteExclusaoRegistro()
    {
        int codigoUnidadeMedida = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        bool result = excluiUnidadeMedida(codigoUnidadeMedida, ref mensagemErro);

        if (result == false)
            return Resources.traducao.unidadesDeMedida_erro_ao_salvar_o_registro__ + mensagemErro;
        else
        {
            carregaGvDados("");
            return "";
        }

    }

    private bool excluiUnidadeMedida(int codigoUnidadeMedida, ref string mensagemErro)
    {
        bool retorno = false;

        string comandoSQL = string.Format(@"DELETE FROM {0}.{1}.TipoUnidadeMedida
      WHERE CodigoUnidadeMedida = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoUnidadeMedida);

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
            if (eControlado == "S")
            {
                //e.BrickStyle.BackColor = Color.FromName("#DDFFCC");
                e.BrickStyle.ForeColor = Color.FromArgb(97, 147, 64, 1);
            }

        }
    }        

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeMedidaControladoSistema = e.GetValue("IndicaControladoSistema").ToString();

            if (unidadeMedidaControladoSistema == "S")
            {
                e.Row.ForeColor = Color.FromName("#619340");
                //e.Row.ForeColor = Color.Black;

            }
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUnidMed");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadUnidMed", lblTituloTela.Text, this);
    }

    #endregion
}