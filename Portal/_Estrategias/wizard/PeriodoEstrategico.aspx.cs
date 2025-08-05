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

public partial class _Estrategias_wizard_PeriodoEstrategico : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    private string resolucaoCliente = "";

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "PS_Cad");
        }

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PS_Cad")) podeIncluir = true; 
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PS_Cad")) podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PS_Cad")) podeExcluir = true;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        cDados.aplicaEstiloVisual(Page);
        carregaGvDados();

        if (!IsPostBack)
        { 
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getGridPeriodoEstrategico(codigoEntidadeUsuarioResponsavel.ToString());

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            //----------------------------------------------------- getAnos
            //Procuro os anos que ja tenho cadastrado, pra evitar repetição.
            string anos = "";
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    anos += dr["Ano"] + ";";
                }
                hfGeral.Set("hfAnos", anos);
                //*** fim getAnos
            }
            else
            {
                hfGeral.Set("hfAnos", anos);
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
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
            if (!podeExcluir)
            {
                e.Text = "Excluir";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
            else if(e.CellType == GridViewTableCommandCellType.Data)
            {
                int ano = Convert.ToInt32(gvDados.GetRowValues(e.VisibleIndex, "Ano"));
                if (VerificaPeriodoEstrategicoPossuiVinculos(ano))
                {
                    e.Text = "Excluir";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Image.ToolTip = Resources.traducao.PeriodoEstrategico_existem_metas_e_ou_resultados_informados_para_o_per_odo_;
                }
            }
        }
    }

    private bool VerificaPeriodoEstrategicoPossuiVinculos(int ano)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @Ano INT
    SET @Ano = {0}

IF EXISTS(SELECT 1 FROM MetaIndicadorUnidade WHERE Ano = @Ano)
    SELECT 'S' Resultado
ELSE IF EXISTS(SELECT 1 FROM ResultadoDadoUnidade WHERE Ano = @Ano)
    SELECT 'S' Resultado
ELSE
    SELECT 'N' Resultado", ano);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        string resultado = ds.Tables[0].Rows[0]["Resultado"] as string;

        return resultado == "S";
    }

    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PeriodoEstrategico.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "PeriodoEstrategico"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }


    
    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
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
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

        carregaGvDados();
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msg = "";

        string codigoUnidade = codigoEntidadeUsuarioResponsavel.ToString();
        string ano = txtAno.Text;
        string anoAtivo = ddlAnoAtivo.SelectedItem.Value.ToString();
        string metaEditavel = ddlMetaEditavel.SelectedItem.Value.ToString();
        string resultadoEditavel = ddlResultadoEditavel.SelectedItem.Value.ToString();
        string tipoVisualizacao = ddlTipoVisualizacao.Value.ToString();

        try
        {
            bool retorno = cDados.incluiPeriodoEstrategico(codigoUnidade, ano, anoAtivo, metaEditavel, resultadoEditavel, tipoVisualizacao);
            if (retorno)
            {
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUnidade);
                gvDados.ClientVisible = false;
            }
            msg = "";
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteExclusaoRegistro()
    {
        string valorAno = gvDados.GetRowValues(gvDados.FocusedRowIndex,"Ano").ToString();
        string where = string.Format(@"CodigoUnidadeNegocio = {0} and Ano = {1}", getChavePrimaria(), valorAno);
        cDados.delete("PeriodoEstrategia", where);
        return "";
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        // busca a chave primaria
        string codigoUnidade = getChavePrimaria();
        string ano = txtAno.Text;
        string anoAtivo = ddlAnoAtivo.SelectedItem.Value.ToString();
        string metaEditavel = ddlMetaEditavel.SelectedItem.Value.ToString();
        string resultadoEditavel = ddlResultadoEditavel.SelectedItem.Value.ToString();
        string tipoVisualizacao = ddlTipoVisualizacao.Value.ToString();
        
        string msgErro = "";
        cDados.atualizaPeriodoEstrategico(codigoUnidade, ano, anoAtivo, metaEditavel, resultadoEditavel, tipoVisualizacao, ref msgErro);

        return msgErro;
    }

    // Método responsável pela Exclusão do registro
    // {...}

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PeriodoEst");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PeriodoEst", "Período Estratégico", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }
}
