using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using System.Drawing;
using DevExpress.Web;

public partial class administracao_auditoria : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    int idAuditoria;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        idAuditoria = int.Parse(Request.QueryString["ID"].ToString());

        carregaDadosAuditoria();

        cDados.aplicaEstiloVisual(this);

        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void carregaDadosAuditoria()
    {
        DataSet ds = cDados.getInformacoesCampoAlteradoAuditoria(idAuditoria, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow drLinhas = ds.Tables[0].Rows[0];

            string xml = string.Format(@"<tabelas>
                        {0}
                        {1}
                      </tabelas>", drLinhas["NEW_DATA"], drLinhas["OLD_DATA"]);

            StringReader xmlSR = new StringReader(xml);

            DataSet dataSet = new DataSet();

            dataSet.ReadXml(xmlSR);


            DataTable dt = new DataTable();

            dt.Columns.Add("NomeCampo");
            dt.Columns.Add("NovoValor");
            dt.Columns.Add("AntigoValor");
            dt.Columns.Add("IndicaAlterado");

            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                DataRow dr = dt.NewRow();

                if (dataSet.Tables[0].Rows[0][i].ToString() != dataSet.Tables[1].Rows[0][i].ToString())
                {
                    dr["NovoValor"] = dataSet.Tables[0].Rows[0][i].ToString();
                    dr["IndicaAlterado"] = "S";
                }
                else
                {
                    dr["IndicaAlterado"] = "N"; 
                }

                dr["NomeCampo"] = dataSet.Tables[0].Columns[i].ColumnName;
                dr["AntigoValor"] = dataSet.Tables[1].Rows[0][i].ToString();
                dt.Rows.Add(dr);
            }

            gvDados.DataSource = dt;
            gvDados.DataBind();

            txtTabela.Text = drLinhas["TABELA"].ToString();
            txtTipoOperacao.Text = drLinhas["TipoOperacao"].ToString();
            txtData.Text = ((DateTime)drLinhas["Data"]).ToString("dd/MM/yyyy HH:mm:ss");
            txtUsuario.Text = drLinhas["USUARIO"].ToString();
            txtMaquina.Text = drLinhas["MAQUINA"].ToString();
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            string indicaAlterado = gvDados.GetRowValues(e.VisibleIndex, "IndicaAlterado").ToString();

            if (indicaAlterado == "S")
            {
                e.Row.ForeColor = Color.Red;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AuditAtl");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AuditAtl", "Auditoria Atualização", this);
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