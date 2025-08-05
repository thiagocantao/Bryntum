using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_CadastroRDO : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = 0;
    int codigoUsuario = 0;
    int codigoEntidade = 0;
    DataSet dsRdo = new DataSet();
    bool podeIncluir = false;
    bool podeEditar = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        if (Request.QueryString["Data"] != null && Request.QueryString["Data"].ToString() != "")
        {
            definePoliticasDeAcessoComPrazo(Request.QueryString["Data"].ToString());
        }
        

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            string tipoAssociacao = cDados.getIniciaisTipoAssociacaoProjeto(codigoProjeto);
            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", tipoAssociacao, 0, "null", "PR_RelRDO");
        }

        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_LimpaCampos"] = "";

        carregaComboAgrupamento();
        carregaGvDados();

        defineTamanhoObjetos();
    }

    private void definePoliticasDeAcessoComPrazo(string dataMarcada)
    {
        int diasPrazoInclusaoEdicaoRDO;

        DataSet ds = cDados.getParametrosSistema("PrazoInclusaoEdicaoRDO");
        if (cDados.DataSetOk(ds))
        {
            /*se de hoje até o dte tiverem se passado mais que ->diasPrazoInclusaoEdicaoRDO<- dias então*/
            diasPrazoInclusaoEdicaoRDO = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            DateTime dataAux = DateTime.Now.AddDays(-diasPrazoInclusaoEdicaoRDO);

            if ((DateTime.Parse(dataMarcada) <= DateTime.Now) &&
                (DateTime.Parse(dataMarcada) >= dataAux))
            {
                //pode incluir sem precisar de permissao especifica
                podeIncluir = true;
                podeEditar = true;
            }
            else
            {
                //PRECISA DE permissao especifica
                podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");
                podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");
            }

        }

        btnSalvar.ClientEnabled = podeEditar;
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        
        gvDados.Settings.VerticalScrollableHeight = (altura - 400);

    }

    protected void ddlTipo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "A" && ddlTipo.Items.Count > 0)
            ddlTipo.SelectedIndex = 0;
    }

    private void carregaComboAgrupamento()
    {
        DataSet ds = cDados.getAgrupamentoItensRDO(" AND CategoriaItem = '" + rbGrupo.Value + "'");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlTipo.DataSource = ds;
            ddlTipo.ValueField = "CodigoAgrupamento";
            ddlTipo.TextField = "TipoItem";
            ddlTipo.DataBind();

            if (!IsPostBack && ddlTipo.Items.Count > 0)
                ddlTipo.SelectedIndex = 0;
        }
    }

    private void carregaGvDados()
    {
        string where = ddlTipo.SelectedIndex == -1 ? "" : " AND CodigoAgrupamento = " + ddlTipo.Value;

        dsRdo = cDados.getItensRDO(codigoProjeto, where);

        if (cDados.DataSetOk(dsRdo))
        {
            gvDados.DataSource = dsRdo;
            gvDados.DataBind();

            txtOrdem.MaskSettings.Mask = "<1.." + (dsRdo.Tables[0].Rows.Count + 1) + ">";
            if (rbGrupo.Value.ToString() == "MOB")
            {
                gvDados.Columns["DescricaoItem"].Caption = "Cargo/Função";
                ((GridViewDataTextColumn)gvDados.Columns["DescricaoItem"]).EditFormSettings.Caption = "Cargo/Função:";
            }
            else
            {
                gvDados.Columns["DescricaoItem"].Caption = "Descrição";
                ((GridViewDataTextColumn)gvDados.Columns["DescricaoItem"]).EditFormSettings.Caption = "Descrição:";
            }

            if(!IsPostBack)
                txtOrdem.Text = (dsRdo.Tables[0].Rows.Count + 1).ToString();
        }
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string descricaoItem = e.NewValues["DescricaoItem"].ToString();
        int numeroOrdem = int.Parse(e.NewValues["NumeroOrdem"].ToString());
        int codigoItem = int.Parse(e.Keys[0].ToString());

        bool retorno = cDados.atualizaItemRDO(codigoItem, descricaoItem, numeroOrdem, int.Parse(ddlTipo.Value.ToString()), codigoProjeto);

        if (retorno)
        {
            carregaGvDados();
            gvDados.JSProperties["cp_LimpaCampos"] = "S";
        }
        else
        {
            gvDados.JSProperties["cp_Msg"] = "Erro ao cadastrar o item!";
        }   

        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int codigoItem = int.Parse(e.Keys[0].ToString());

        bool retorno = cDados.excluiItemRDO(codigoItem, int.Parse(ddlTipo.Value.ToString()), codigoProjeto);

        if (retorno)
        {
            carregaGvDados();
            gvDados.JSProperties["cp_LimpaCampos"] = "S";
        }
        else
        {
            gvDados.JSProperties["cp_Msg"] = "Erro ao cadastrar o item!";
        }   

        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "NumeroOrdem")
        {
            string where = ddlTipo.SelectedIndex == -1 ? "" : " AND CodigoAgrupamento = " + ddlTipo.Value;

            dsRdo = cDados.getItensRDO(codigoProjeto, where);

            ((ASPxTextBox)e.Editor).MaskSettings.Mask = "<1.." + (dsRdo.Tables[0].Rows.Count) + ">";
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "I")
        {
            int novoCodigo = 0;
            bool retorno = cDados.incluiItemRDO(codigoProjeto, int.Parse(ddlTipo.Value.ToString()), txtItem.Text, int.Parse(txtOrdem.Text), ref novoCodigo);
                        
            if (retorno)
            {
                gvDados.FilterExpression = "";
                carregaGvDados();
                int indexNovaLinha = gvDados.FindVisibleIndexByKeyValue(novoCodigo);
                gvDados.FocusedRowIndex = indexNovaLinha;
                gvDados.ScrollToVisibleIndexOnClient = indexNovaLinha;
                gvDados.JSProperties["cp_LimpaCampos"] = "S";
            }
            else
            {
                gvDados.JSProperties["cp_Msg"] = "Erro ao cadastrar o item!";
            }
        }
    }

    protected void pnOrdem_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "A")
            txtOrdem.Text = (dsRdo.Tables[0].Rows.Count + 1).ToString();
    }
    
    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {

        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            if (podeEditar == true)
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

        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            if (podeEditar == true)
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