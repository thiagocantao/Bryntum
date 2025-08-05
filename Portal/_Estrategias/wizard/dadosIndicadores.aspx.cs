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
using System.Drawing;

public partial class _Estrategias_wizard_dadosIndicadores : System.Web.UI.Page
{
    DataSet dset;
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeIncluir = false;

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());  //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "IN_CadDadInd");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "IN_CadDadInd"))
            podeIncluir = true;

        carregaGrid();
        
        carregaComboUnidadeMedida();
        carregaComboAgrupamentos();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);             
            //MenuUsuarioLogado();
        }
                
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region Grid's

    private void carregaGrid()
    {
        //string where = " AND du.CodigoUnidadeNegocio = '" + CodigoEntidade + "'";
        //where += " AND dun.CodigoUnidadeNegocio = " + CodigoEntidade;

        string where = string.Format(@" AND dun.IndicaUnidadeCriadoraDado = 'S'
                                        AND dun.CodigoUnidadeNegocio IN(SELECT CodigoUnidadeNegocio 
                                                                         FROM UnidadeNegocio 
                                                                        WHERE CodigoEntidade = {0}) ", CodigoEntidade);

        dset = cDados.getDados(where);

        if (cDados.DataSetOk(dset))
        {
            gvDados.DataSource = dset;
            gvDados.DataBind();

            string nomesDados = "";
            string codigosReservadosDados = "";

            foreach (DataRow dr in dset.Tables[0].Rows)
            {
                nomesDados += dr["DescricaoDado"].ToString() + "¥";
                codigosReservadosDados += dr["CodigoReservado"].ToString() + "¥";
            }

            hfGeral.Set("NomeDados", nomesDados);
            hfGeral.Set("CRsDados", codigosReservadosDados);
        }
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "IN_CadDadInd"))
        {
            string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
            captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
            captionGrid += string.Format(@"</tr></table>");
            gvDados.SettingsText.Title = captionGrid;
        }
    }

    #endregion

    #region COMBOBOX

    private void carregaComboUnidadeMedida()
    {
        dset = cDados.getUnidadeMedida();

        cmbUnidadeDeMedida.DataSource = dset;
        cmbUnidadeDeMedida.TextField = "SiglaUnidadeMedida";
        cmbUnidadeDeMedida.ValueField = "CodigoUnidadeMedida";
        cmbUnidadeDeMedida.DataBind();

        if (!IsPostBack && cDados.DataSetOk(dset) && cDados.DataTableOk(dset.Tables[0]))
            cmbUnidadeDeMedida.SelectedIndex = 0;
    }

    private void carregaComboAgrupamentos()
    {
        dset = cDados.getAgrupamentoFuncao();

        cmbAgrupamentoDoDado.DataSource = dset;
        cmbAgrupamentoDoDado.TextField = "NomeFuncao";
        cmbAgrupamentoDoDado.ValueField = "CodigoFuncao";
        cmbAgrupamentoDoDado.DataBind();

        if (!IsPostBack && cDados.DataSetOk(dset) && cDados.DataTableOk(dset.Tables[0]))
            cmbAgrupamentoDoDado.SelectedIndex = 0;
    }

    #endregion

    #region VARIOS

    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/dadosIndicadores.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "dadosIndicadores"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.dadosIndicadores_dado_inclu_do_com_sucesso_;
            ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.dadosIndicadores_dado_atualizado_com_sucesso_;
            ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxCallbackPanel)sender).JSProperties["cp_sucesso"] = Resources.traducao.dadosIndicadores_dado_exclu_do_com_sucesso_;
            ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoUsuario   = 0;
        string msg = "";

        try
        {
            DataSet ds = cDados.incluiDadoIndicadores(txtDado.Text.Replace("'", "''"),
                                                      heGlossario.Html,
                                                      int.Parse(cmbUnidadeDeMedida.Value.ToString()),
                                                      int.Parse(cmbCasasDecimais.Value.ToString()),
                                                      txtValorMinimo.Text == "" ? "NULL" : txtValorMinimo.Text,
                                                      txtValorMaximo.Text == "" ? "NULL" : txtValorMaximo.Text,
                                                      int.Parse(cmbAgrupamentoDoDado.Value.ToString()),
                                                      idUsuarioLogado, CodigoEntidade, txtCodigoReservado.Text);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUsuario = int.Parse(ds.Tables[0].Rows[0]["codigoDado"].ToString());
                carregaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
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

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        // busca a chave primaria
        int codigoDado = int.Parse(getChavePrimaria());

        cDados.atualizaDadoIndicadores(txtDado.Text.Replace("'", "''"),
                                       heGlossario.Html.Replace("'", "''"),
                                       int.Parse(cmbUnidadeDeMedida.Value.ToString()),
                                       int.Parse(cmbCasasDecimais.Value.ToString()),
                                       txtValorMinimo.Text == "" ? "NULL" : txtValorMinimo.Text,
                                       txtValorMaximo.Text == "" ? "NULL" : txtValorMaximo.Text,
                                       int.Parse(cmbAgrupamentoDoDado.Value.ToString()), codigoDado, txtCodigoReservado.Text, CodigoEntidade);
     
        carregaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoDado);
        gvDados.ClientVisible = false;

        return "";
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        // busca a chave primaria
        string chave = getChavePrimaria();
        string msg = "";

        bool retorno = cDados.excluiDadoIndicadores(int.Parse(chave),idUsuarioLogado, ref msg);

        if (retorno)
            carregaGrid();
        else
            return msg;

        return "";
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string unidadeCriadora = gvDados.GetRowValues(e.VisibleIndex, "IndicaUnidadeCriadoraDado").ToString();
            string codigoDado = gvDados.GetRowValues(e.VisibleIndex, "CodigoDado").ToString();

            bool existeAssociacaoDeIndicador = getIndicaIndicadoresAssociados(codigoDado);
            if (e.ButtonID == "btnEditar")
            {
                if ("S" == unidadeCriadora && podeIncluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";                   
                }
            }
            else if (e.ButtonID == "btnExcluir")
            {
                if ("S" == unidadeCriadora && podeIncluir)
                {
                    if(existeAssociacaoDeIndicador == true)
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Image.ToolTip = "Este dado de indicador está associado a fórmulas de um ou mais indicadores ativos";
                    }
                    else
                    {
                        e.Enabled = true;
                        e.Image.Url = "~/imagens/botoes/ExcluirReg02.png";
                    }                  
                }                   
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else if (e.ButtonID == "btnDadoCompartilhadoCustom")
            {
                if ("S" != unidadeCriadora)
                    e.Enabled = true;
                else
                {
                    //e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/codigoReservadoDes.png";
                }
            }
        }

    }

    private bool getIndicaIndicadoresAssociados(string codigoDado)
    {
        bool retorno = false;

        string comandoSQL = string.Format(@"SELECT CodigoIndicador FROM dadoindicador 
				                             WHERE codigodado = {0} 
                                               AND CodigoIndicador in (SELECT CodigoIndicador 
				                                                         FROM indicador
                                                                         WHERE DataExclusao IS NULL)", codigoDado);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = true;            
        }
        return retorno;
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        

        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeAtivo = e.GetValue("IndicaUnidadeCriadoraDado").ToString();

            if (unidadeAtivo != "S")
            {
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
                
            }
        }
    }

    protected void pnCallbackResponsavel_Callback(object sender, CallbackEventArgsBase e)
    {
        string opcao = e.Parameter;

        if (opcao == "salvar")
        {
            //string codigoIndicador = hfGeral.Get("hfCodigoIndicador").ToString();

            string comandoSQL = string.Format(@"
            UPDATE {0}.{1}.DadoUnidade
            SET
            CodigoReservado = '{2}'
            WHERE
            CodigoDado = {3}
            AND
            CodigoUnidadeNegocio = {4}
            ", cDados.getDbName(), cDados.getDbOwner()
             , txtCodigoReservadoDadoComp.Text
             , txtCodigoDadoCompartilhado.Text
             , CodigoEntidade);

            int regAfectados = 0;
            cDados.execSQL(comandoSQL, ref regAfectados);

            if (regAfectados > 0)
                pnCallbackResponsavel.JSProperties["cp_OperacaoOk"] = "SIM";
            else
                pnCallbackResponsavel.JSProperties["cp_OperacaoOk"] = "NAO";
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DadosInd");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "TipoOperacao = 'Incluir';habilitaHtmlEditor();btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "DadosInd", "Dados de Indicadores", this);
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
