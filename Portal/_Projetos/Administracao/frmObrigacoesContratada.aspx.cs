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

public partial class _Projetos_Administracao_frmObrigacoesContratada : System.Web.UI.Page
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
            gvObrigacoes.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 85;

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
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncObgCon");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcObgCon");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltObgCon");

            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }

        carregagvObrigacoes();

        gvObrigacoes.Settings.ShowFilterRow = false;
        gvObrigacoes.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void carregagvObrigacoes()
    {
        DataSet ds = cDados.getObrigacoesContratada(codigoContrato.ToString());

        if (cDados.DataSetOk(ds))
        {
            gvObrigacoes.DataSource = ds;
            gvObrigacoes.DataBind();

        }

    }


    protected void gvObrigacoes_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
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

    protected void gvObrigacoes_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string CodigoObrigacoesContratada = e.NewValues["CodigoObrigacoesContratada"].ToString();
        string Ocorrencia = e.NewValues["Ocorrencia"].ToString();
        cDados.incluiObrigacoesContratada(codigoContrato, int.Parse(CodigoObrigacoesContratada), Ocorrencia);

        carregagvObrigacoes();
        e.Cancel = true;
        gvObrigacoes.CancelEdit();
    }

    protected void gvObrigacoes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string CodigoObrigacoesContratada = e.NewValues["CodigoObrigacoesContratada"].ToString();
        string Ocorrencia = e.NewValues["Ocorrencia"].ToString();
        cDados.atualizaObrigacoesContratada(codigoContrato, int.Parse(CodigoObrigacoesContratada), Ocorrencia);
        carregagvObrigacoes();
        e.Cancel = true;
        gvObrigacoes.CancelEdit();
    }

    protected void gvObrigacoes_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string CodigoObrigacoesContratada = e.Keys["CodigoObrigacoesContratada"].ToString();
        cDados.excluiObrigacoesContratada(codigoContrato, int.Parse(CodigoObrigacoesContratada));
        
        carregagvObrigacoes();

        e.Cancel = true;
        gvObrigacoes.CancelEdit();
    }


    protected void gvObrigacoes_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        e.Editor.ClientEnabled = true;
        e.Editor.DisabledStyle.ForeColor = Color.Black;
        e.Editor.DisabledStyle.BackColor = Color.White;

        if (e.Column.FieldName == "CodigoObrigacoesContratada")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            int vCodigoObrigacoesContratada = e.Value != null ? int.Parse(e.Value.ToString()) : -1;

            string where = string.Format(@"AND CodigoTipoObrigacoesContratada not in 
                                                         (SELECT CodigoObrigacoesContratada 
                                                          FROM dbo.[ObrigacoesContratada] AS [ob]
                                                           where ob.codigoContrato = {0} 
                                                             and ob.CodigoObrigacoesContratada <> {1}) ", codigoContrato,vCodigoObrigacoesContratada );

            DataSet ds = cDados.getListaTiposObrigacaoContratada(where);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                combo.DataSource = ds;
                combo.TextField = "DescricaoTipoObrigacoesContratada";
                combo.ValueField = "CodigoTipoObrigacoesContratada";
                combo.DataBind();
            } /// if (cDados.DataSetOk(ds) && ...
        } 
    }


}
