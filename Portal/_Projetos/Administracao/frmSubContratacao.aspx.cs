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
using System.Globalization;
using DevExpress.XtraBars.Alerter;

public partial class _Projetos_Administracao_frmSubContratacao : System.Web.UI.Page
{
    dados cDados;

    int codigoContrato = -1;
    public string somenteLeitura = "";
    DateTime dtInicioVigencia = DateTime.Now;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string dbName;
    private string dbOwner;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool gravouSubContratacao = true;
    string IniciaisTipoAssociacao = "CT";
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (Request.QueryString["CC"] != null)
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();


        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
            gvSub.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 150;

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);
        if (somenteLeitura == "S")
        {
            podeIncluir = false;
            podeEditar = false;
            podeExcluir = false;
        }
        else
        {
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncSubCon");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcSubCon");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltSubCon");
            gvSub.Caption = null;
            if (podeIncluir){
                podeIncluir = cDados.verificaSubContratacao(codigoContrato);

                if (false == podeIncluir)
                {
                    gvSub.Caption = "Para incluir registros, antes é necessário salvar o contrato.";
                }
            }


            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }
        if (!IsCallback)
        {
            carregagvSub();
        }
        
        DataSet ds = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtNumeroContrato.Text = ds.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim() == "" ? ((ds.Tables[0].Rows[0]["StatusContrato"].ToString().ToUpper().Trim() == "A") ? "Ativo" : "Inativo") : ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim();
            txtInicioVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataInicio"].ToString()).ToString("dd/MM/yyyy");
            txtTerminoVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataTermino"].ToString()).ToString("dd/MM/yyyy");

        }
    }

    private void carregagvSub()
    {
        DataSet ds = cDados.getSubContratacao(codigoContrato.ToString());

        if (cDados.DataSetOk(ds))
        {
            gvSub.DataSource = ds;
            gvSub.DataBind();

        }
        
    }


    protected void gvSub_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (somenteLeitura == "S")
        {
            podeIncluir = false;
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            {
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/editarRegDes.png";
            }
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
            {
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/excluirRegDes.png";
            }

        }
        else
        {
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
            {
                if (podeIncluir)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            } 
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
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
    }

    protected void gvSub_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string CNPJCPF = e.Keys["NumeroCNPJCPF"].ToString();
        cDados.excluiSubContratada(codigoContrato, CNPJCPF);
        
        carregagvSub();

        e.Cancel = true;
        gvSub.CancelEdit();
    }

    protected void gvSub_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (!gvSub.IsNewRowEditing)
        {
            ((GridViewDataComboBoxColumn)gvSub.Columns["TipoPessoa"]).ReadOnly = true;
            ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).ReadOnly = true;
            ((GridViewDataComboBoxColumn)gvSub.Columns["TipoPessoa"]).PropertiesComboBox.DropDownButton.Enabled = false;
            //((GridViewDataComboBoxColumn)gvSub.Columns["TipoPessoa"]).PropertiesComboBox. .EnableAnimation = false;

            if (gvSub.GetRowValues(gvSub.FocusedRowIndex, "TipoPessoa").ToString() == "F")
            {
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).PropertiesTextEdit.MaskSettings.Mask = "000,000,000-00";
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).EditFormSettings.Caption = "CPF";
                ((GridViewDataTextColumn)gvSub.Columns["RazaoSocial"]).EditFormSettings.Caption = "Nome";
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).PropertiesTextEdit.ValidationSettings.ErrorText = "Informe CPF válido";
            }
            else
            {
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).PropertiesTextEdit.MaskSettings.Mask = "00,000,000/0000-00";
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).EditFormSettings.Caption = "CNPJ";
                ((GridViewDataTextColumn)gvSub.Columns["RazaoSocial"]).EditFormSettings.Caption = "Nome";
                ((GridViewDataTextColumn)gvSub.Columns["NumeroCNPJCPF"]).PropertiesTextEdit.ValidationSettings.ErrorText = "Informe CNPJ válido";
            }

        }

    }

    protected void callbackCnpjCpf_Callback(object sender, CallbackEventArgsBase e)
    {
        txtCpfCnpj.MaskSettings.ShowHints = true;
        if (e.Parameter == "F")
        {
            txtCpfCnpj.MaskSettings.Mask = "000,000,000-00";
            txtCpfCnpj.ValidationSettings.ErrorText = "Informe CPF Válido";
        }
        else
        {
            txtCpfCnpj.MaskSettings.Mask = "00,000,000/0000-00";
            txtCpfCnpj.ValidationSettings.ErrorText = "Informe CNPJ Válido";
        }
    }
    protected void callbackLabelCnpjCpf_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter == "F")
        {
            labelCnpjCpf.Text = "CPF:";
        }
        else
        {
            labelCnpjCpf.Text = "CNPJ:";
        }
    }

    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        string tipoOperacao = hfGeral.Get("TipoOperacao").ToString();

        string CNPJCPF = txtCpfCnpj.Text;
        string Razao = txtRazaoSocial.Text;
        string Classifica = comboClassificacao.Value.ToString();
        string TipoPessoa = comboTipoPessoa.Value.ToString();
        try
        {
            if (tipoOperacao == "Incluir")
            {
                cDados.incluiSubContratada(codigoContrato, CNPJCPF, Razao, Classifica, TipoPessoa);
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registro incluído com sucesso!";
            }
            else
            {
                cDados.atualizaSubContratada(codigoContrato, CNPJCPF, Razao, Classifica, TipoPessoa);
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registro atualizado com sucesso!";
            }
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }

    protected void gvSub_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregagvSub();
    }
}
