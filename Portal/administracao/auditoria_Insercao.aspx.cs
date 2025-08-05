using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
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

            string nomeColuna = drLinhas["OPERACAO"].ToString() == "I" ? "NEW_DATA" : "OLD_DATA";

            string xml = string.Format(@"<tabela>
                        {0}
                      </tabela>", drLinhas[nomeColuna]);

            StringReader xmlSR = new StringReader(xml);

            DataSet dataSet = new DataSet();

            dataSet.ReadXml(xmlSR);


            DataTable dt = new DataTable();

            dt.Columns.Add("NomeCampo");
            dt.Columns.Add("Valor");

            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                
                    DataRow dr = dt.NewRow();

                    dr["NomeCampo"] = dataSet.Tables[0].Columns[i].ColumnName;
                    dr["Valor"] = dataSet.Tables[0].Rows[0][i].ToString();

                    dt.Rows.Add(dr);
            }

            gvDados.DataSource = dt;
            gvDados.DataBind();

            txtTabela.Text = drLinhas["TABELA"].ToString();
            txtTipoOperacao.Text = drLinhas["TipoOperacao"].ToString();
            txtData.Text = drLinhas["Data"].ToString();
            txtUsuario.Text = drLinhas["USUARIO"].ToString();
            txtMaquina.Text = drLinhas["MAQUINA"].ToString();
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AuditInc");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AuditInc", "Auditoria Inserção", this);
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