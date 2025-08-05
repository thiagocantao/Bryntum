/*
 * OBSEVAÇÕES
 * 
 * MUDANÇAS
 * 
 * 16/03/2011 :: Alejandro : Adaptação para verificação do acesso ao sistema.
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

public partial class administracao_Calendarios : System.Web.UI.Page
{
    dados cDados;    

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;
    public string alturaFrame = "";

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
        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
               
        this.Title = cDados.getNomeSistema();
     }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeadarOnTela();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "EN_CadCal");
            cDados.aplicaEstiloVisual(Page);
        }       

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        populaGrid();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        gvDados.JSProperties["cp_MSG"] = "";
    }

    private void HeadarOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/calendarios.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisCalendar.css"" rel=""stylesheet"" type=""text/css"" />"));
        this.TH(this.TS("calendarios"));

    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        string where = "";
        DataSet ds = cDados.getCalendariosEntidade(CodigoEntidade, where);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            ddlCalendarioBase.DataSource = ds;
            ddlCalendarioBase.TextField = "DescricaoCalendario";
            ddlCalendarioBase.ValueField = "CodigoCalendario";
            ddlCalendarioBase.DataBind();

            if (!IsPostBack && ddlCalendarioBase.Items.Count > 0)
                ddlCalendarioBase.SelectedIndex = 0;
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int altura = 0;
        int largura = 0;
        
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 131;

        alturaFrame = (alturaPrincipal - 285) + "px";
    }

    public string getBotoes(string codigoCalendario, string indicaPadrao)
    {
        string botoes = "";
        string botaoExcluir = "excluirReg02", alt = Resources.traducao.Calendarios_excluir, cursor = "pointer", eventoExcluir = "if(confirm('" + Resources.traducao.Calendarios_confirma_a_exclus_o_do_calend_rio_ + "'))gvDados.PerformCallback('X_" + codigoCalendario + "');";
        string eventoEdicao = string.Format(@"abreEdicao({0})", codigoCalendario);

        if (indicaPadrao == "S")
        {
            botaoExcluir = "excluirRegDes";
            eventoExcluir = "return false;";
            cursor = "default";
            alt = Resources.traducao.Calendarios_o_calend_rio_padr_o_n_o_pode_ser_exclu_do;
        }

        botoes = string.Format(@"<table cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center"" style=""width: 25px"">
                                            <img onclick=""{2}"" style=""cursor: pointer;"" title=""" + Resources.traducao.editar + @""" src=""../imagens/botoes/editarReg02.PNG"" /></td>
                                        <td align=""center"" style=""width: 25px"">
                                            <img onclick=""{1}"" style=""cursor: {3};"" title=""{4}"" src=""../imagens/botoes/{0}.PNG"" /></td>
                                    </tr>
                                </table>", botaoExcluir, eventoExcluir, eventoEdicao, cursor, alt);

        return botoes;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {      
        string parametro = e.Parameters.ToString();
        string codigoCalendario = "";

        if (parametro != "")
        {
            codigoCalendario = parametro.Substring(2);

            if (parametro.Substring(0, 1) == "X")
            {
                deletaCalendario(int.Parse(codigoCalendario));
                populaGrid();
            }
        }        
    }

    private void deletaCalendario(int codigoCalendario)
    {
        try
        {
            if(!cDados.excluiCalendario(codigoCalendario))
                throw new Exception(Resources.traducao.Calendarios_o_calend_rio_n_o_pode_ser_exclu_do__j__que_possui_depend_ncias_com_outros_objetos_);
        }
        catch (Exception ex)
        {
            gvDados.JSProperties["cp_MSG"] = ex.Message.ToString();
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int codigoNovoUsuario = 0;
        int? calendarioBase = null;

        if (ddlCalendarioBase.SelectedIndex != -1)
            calendarioBase = int.Parse(ddlCalendarioBase.Value.ToString());

        cDados.incluiCopiaCalendario(calendarioBase, CodigoEntidade, idUsuarioLogado, "EN", CodigoEntidade, "", ref codigoNovoUsuario);

        callback.JSProperties["cp_NovoCodigo"] = codigoNovoUsuario;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CalendCorp");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "abreInsercao();", true, true, false, "CalendCorp", lblTituloTela.Text, this);
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
