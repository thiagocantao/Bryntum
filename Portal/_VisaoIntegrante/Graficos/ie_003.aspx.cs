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
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.Web;

public partial class _VisaoIntegrante_Graficos_ie_003 : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioLogado, codigoEntidade;
    public string tituloToDoList = "To Do List";
    public string larguraGrid = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        //Função que gera o gráfico
        geraTabela();

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
            tituloToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

        cDados.aplicaEstiloVisual(Page);

        gvDados.Columns[1].Caption = "Projeto/" + tituloToDoList;
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        gvDados.Settings.VerticalScrollableHeight = altura - 370;
        gvDados.Width = new Unit("100%");//(largura / 3 * 2);

        larguraGrid = ((largura - (largura) / 3) - 65) + "px";
    }

    private void geraTabela()
    {
        DataSet ds = cDados.getListaTarefasIntegranteEquipe(codigoUsuarioLogado, codigoEntidade, "AND TerminoReal IS NULL");

        gvDados.DataSource = ds;
        gvDados.DataBind();
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

        try
        {
            if (e.Column.Name.ToString() == "TarefaCritica")
            {
                if (e.Value != null && e.Value.ToString().ToLower() == "tarefacritica")
                {
                    e.Text = "!";
                    e.TextValue = "!";
                    e.BrickStyle.ForeColor = Color.Red;
                    e.BrickStyle.Font = new Font("Verdana", 11, FontStyle.Bold);
                    e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                }
                else
                {
                    e.Text = "";
                    e.TextValue = "";
                }
            }
        }
        catch { }
    }
    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Anotacoes" && e.CellValue.ToString().Length > 145)
            e.Cell.Text = e.CellValue.ToString().Substring(0, 145) + "...";
    }

    public string getInfoLegenda()
    {
        return string.Format("Minhas tarefas a serem concluídas até {0:dd/MM/yyyy}", DateTime.Now.AddDays(10));
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "TarUsuPnlIntEqp");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "TarUsuPnlIntEqp", "Tarefas do Usuário", this);
    }

    #endregion
}
