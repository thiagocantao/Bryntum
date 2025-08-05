/*
 OBSERVAÇÕES
 * 
 * MUDANÇA
 * 04/02/2011 - Alejandro: 
                a.- "Não se pode alterar o tipo de um grupo se o grupo tiver 'filhos'"
                    protected void ddlTipoRecurso_Callback(object sender, CallbackEventArgsBase e), função
                    chamada desde function MontaCamposFormulario(values), no adm_GrupoRecurso.js
                b.- 
 
 
 
 */
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

public partial class administracao_adm_GrupoRecurso : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    
    private string resolucaoCliente = "";

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
            cDados.aplicaEstiloVisual(Page);

            carregaGvDados();

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        pcSolucionar.HeaderText = "Solução de problemas  Vínculo do Recurso MS-Project ao " + cDados.getNomeSistema();
        lblMsgVinculo.Text = "Não foi possível estabelecer um vínculo do recurso MS-Project acima identificado com um recurso do " + cDados.getNomeSistema();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/sincronismoMSProject.js""></script>"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 200;
        gvDados.Width = new Unit((largura - 10) + "px");
    }
    
    #endregion
      
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.sincronizaRecursosMSProject('S');

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }    

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string sincronizado = gvDados.GetRowValues(e.VisibleIndex, "Sincronizado").ToString();

        if ((sincronizado == "S" && e.ButtonID == "btnEditar") || (sincronizado == "N" && e.ButtonID == "btnDetalhesCustom"))
        {
            e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
    }

    #endregion
            
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        return "";
    }
        
    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        return "";
    }
   
    #endregion

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Sincronizado")
        {
            e.Cell.Text = gvDados.GetRowValues(e.VisibleIndex, "Sincronizado").ToString() == "S" ? "Sim" : "Não";
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_Status"] = "0";

        if (e.Parameters.ToString() == "S")
        {

            string msgErro = "", codigoRecurso;

            codigoRecurso = getChavePrimaria();

            bool retorno = cDados.incluiNovoEmailRecurso(codigoRecurso, txtNovoEmail.Text, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, ref msgErro);

            if (retorno)
            {
                //carregaGvDados();
                gvDados.JSProperties["cp_msg"] = "Email incluído com sucesso!";
            }
            else
            {
                gvDados.JSProperties["cp_Status"] = "1";
                gvDados.JSProperties["cp_msg"] = msgErro;
            }
        }
    }
}
