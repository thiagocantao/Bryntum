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

public partial class administracao_adm_CheckinFormularios : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void populaGrid()
    {
        DataSet ds = getFormulariosComCheckoutComPermissao(CodigoEntidade,idUsuarioLogado, "");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private DataSet getFormulariosComCheckoutComPermissao(int codigoEntidade, int idUsuarioLogado, string v)
    {
        
        string comandoSQL = string.Format(@"SELECT
       f.CodigoFormulario,
       p.NomeProjeto,
       f.DescricaoFormulario,
       u.NomeUsuario,
       f.DataCheckOut
  FROM Formulario AS f INNER JOIN
       ModeloFormulario AS mf ON (mf.CodigoModeloFormulario = f.CodigoModeloFormulario
                              AND mf.CodigoEntidade = {0}) LEFT JOIN
       FormularioProjeto AS fp ON (fp.CodigoFormulario = f.CodigoFormulario
                               AND f.DataExclusao IS NULL) LEFT JOIN
       Projeto AS p ON (p.CodigoProjeto = fp.CodigoProject) LEFT JOIN
       Usuario AS u ON (u.CodigoUsuario = f.CodigoUsuarioCheckOut) 
 WHERE f.DataCheckOut   IS NOT NULL", codigoEntidade);
        DataSet dsRetorno = cDados.getDataSet(comandoSQL);
        return dsRetorno;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 150;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_Erro"] = "";
        gvDados.JSProperties["cp_Sucesso"] = "";

        string mensagemErro = "";
        string CodigoFormulario = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoFormulario").ToString();
        // ACG: 04/10/2015 - O segundo parametro é para desbloquear cronogramas de replanejamento
        atualizaFormularioCheckin(CodigoFormulario, ref mensagemErro);
        if(mensagemErro == "")
        {
            gvDados.JSProperties["cp_Sucesso"] = "Formulario desbloqueado com sucesso!";
        }
        else
        {
            gvDados.JSProperties["cp_Erro"] = mensagemErro;
        }
        populaGrid();
    }

    public bool atualizaFormularioCheckin(string codigoFormulario, ref string mensagemErro)
    {
        string comandoSQL = string.Format(@"
        UPDATE Formulario
           SET DataCheckout = null
              ,CodigoUsuarioCheckout = null
              ,DataCheckin = GETDATE()
           where  CodigoFormulario = {0} ", codigoFormulario);
        bool retorno = false;
        int regAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch(Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DesblForm");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "DesblForm", lblTituloTela.Text, this);
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
        if (e.Column.Name == "DataCheckout")
        {
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        }
    }
    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool podeDesbloquearFormulario = true;

        if (e.ButtonID == "btnCheckin")
        {
            if (podeDesbloquearFormulario)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/btnDesbloquearDes.png";
            }
        }
    }
}
