using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;
using System.Drawing;

public partial class administracao_SubstituicaoRecursos : System.Web.UI.Page
{
    dados cDados;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource2.ConnectionString = cDados.classeDados.getStringConexao();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        cDados.aplicaEstiloVisual(this);//Ok

        //cDados.aplicaEstiloVisual(gvDados);//Ok

        gvDados.JSProperties["cpMsg"] = "";

        populaCombos();
        carregaGrid();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/SubstituicaoRecursos.js""></script>"));
        this.TH(this.TS("SubstituicaoRecursos"));

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_SbtUsr");
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void carregaGrid()
    {
        int codigoUsuarioOrigem = ddlUsuarioOrigem.SelectedIndex == -1 ? -1 : int.Parse(ddlUsuarioOrigem.Value.ToString());

        DataSet ds = cDados.getObjetosResponsabilidadeUsuario(codigoUsuarioOrigem, codigoEntidadeUsuarioResponsavel, "");

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel
                                                           , e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    private void populaCombos()
    {
        DataSet ds = new DataSet();

        ddlUsuarioOrigem.TextField = Resources.traducao.SubstituicaoRecursos_nomeusuario;
        ddlUsuarioOrigem.ValueField = "CodigoUsuario";
        ddlUsuarioOrigem.Columns[0].FieldName = "NomeUsuario";
        ddlUsuarioOrigem.Columns[1].FieldName = "EMail";
        ddlUsuarioOrigem.TextFormatString = "{0}";


        ddlUsuarioDestino.TextField = "NomeUsuario";
        ddlUsuarioDestino.ValueField = "CodigoUsuario";
        ddlUsuarioDestino.Columns[0].FieldName = "NomeUsuario";
        ddlUsuarioDestino.Columns[1].FieldName = "EMail";
        ddlUsuarioDestino.TextFormatString = "{0}";
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            ASPxCheckBox checkBox = gvDados.FindGroupRowTemplateControl(e.VisibleIndex, "checkBox") as ASPxCheckBox;

            if (gvDados.GetRowValues(e.VisibleIndex, "Tabela") + "" == "RecursoCronogramaProjeto")
            {
                checkBox.ClientEnabled = false;
                e.Row.ForeColor = Color.Gray;
            }

            if (checkBox != null)
            {
                checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ gvDados.PerformCallback('{0};' + s.GetChecked()); }}", e.VisibleIndex);
                checkBox.Checked = GetChecked(e.VisibleIndex);
            }
        }
        else if(e.RowType == GridViewRowType.Data)
        {
            if (gvDados.GetRowValues(e.VisibleIndex, "Tabela") + "" == "RecursoCronogramaProjeto")
            {               
                e.Row.ForeColor = Color.Gray;
            }
        }


    }

    protected bool GetChecked(int visibleIndex)
    {
        for (int i = 0; i < gvDados.GetChildRowCount(visibleIndex); i++)
        {
            bool isRowSelected = gvDados.Selection.IsRowSelectedByKey(gvDados.GetChildDataRow(visibleIndex, i)["CodigoUnico"]);
            if (!isRowSelected)
                return false;
        }
        return true;
    }

    protected string GetCaptionText(GridViewGroupRowTemplateContainer container)
    {
        string captionText = !string.IsNullOrEmpty(container.Column.Caption) ? container.Column.Caption : container.Column.FieldName;
        return string.Format("{1} {2}", captionText, container.GroupText, container.SummaryText);
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_Sucesso"] = "";
        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_Erro"] = "";
        

        if (e.Parameters == "SBS")
        {
            if (ddlUsuarioDestino.Value == null)
            {
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.SubstituicaoRecursos_informe_qual_usu_rio_receber__as_atribui__es_de_ + ddlUsuarioOrigem.Text;
            }
            else
            {
                string descricaoObjeto;
                string Tabela, Chave1, Chave2, Chave3, Chave4;
                string comandoSQL = "";
                DateTime dataHora = DateTime.Now;
                int nReg, nQtdReg;

                nQtdReg = 0;

                for (int i = 0; i < gvDados.Selection.Count; i++)
                {
                    descricaoObjeto = gvDados.GetSelectedFieldValues("DescricaoObjeto")[i].ToString();
                    Tabela = gvDados.GetSelectedFieldValues("Tabela")[i].ToString();
                    Chave1 = gvDados.GetSelectedFieldValues("Chave1")[i].ToString();
                    Chave2 = gvDados.GetSelectedFieldValues("Chave2")[i].ToString();
                    Chave2 = Chave2.Length == 0 ? "null" : Chave2;
                    Chave3 = gvDados.GetSelectedFieldValues("Chave3")[i].ToString();
                    Chave3 = Chave3.Length == 0 ? "null" : Chave3;
                    Chave4 = gvDados.GetSelectedFieldValues("Chave4")[i].ToString();
                    Chave4 = Chave4.Length == 0 ? "null" : Chave4;
                    comandoSQL += string.Format(@"INSERT INTO {0}.{1}.SubstituicaoRecurso
                                              ( [DataInclusao],	[CodigoUsuarioInclusao],    [CodigoUsuarioSubstituido],
                                                [CodigoUsuarioNovo],	[Tabela],	[Chave1],	[Chave2],	[Chave3],	[Chave4] )
                                              VALUES
                                               ( '{2}',{3},{4},{5},'{6}',{7},{8},{9},{10});{11}",
                                                    cDados.getDbName(), cDados.getDbOwner(), dataHora, codigoUsuarioResponsavel, ddlUsuarioOrigem.Value, ddlUsuarioDestino.Value,
                                                    Tabela, Chave1, Chave2, Chave3, Chave4, Environment.NewLine);

                    nQtdReg++;
                }

                nReg = 0;

                if (comandoSQL.Length > 0)
                    cDados.execSQL(comandoSQL, ref nReg);

                nQtdReg = nQtdReg + nReg;

                if (nQtdReg > 0)
                {
                    try
                    {
                        comandoSQL = string.Format(@" EXEC {0}.{1}.p_SubstituicaoRecurso @DataHora='{2}', @CodigoUsuario={3} ",
                                                           cDados.getDbName(), cDados.getDbOwner(), dataHora, codigoUsuarioResponsavel);

                        cDados.execSQL(comandoSQL, ref nReg);

                        gvDados.Selection.UnselectAll();

                        carregaGrid();

                        gvDados.JSProperties["cp_Sucesso"] = string.Format(Resources.traducao.SubstituicaoRecursos_substitui__o_conclu_da_com_sucesso_);

                    }
                    catch (Exception er)
                    {
                        gvDados.JSProperties["cp_Erro"] = string.Format(Resources.traducao.SubstituicaoRecursos__0__erro_na_execu__o_da_p_substituicaorecurso_, er);
                        gvDados.Selection.UnselectAll();
                        carregaGrid();
                    }
                }                
            }
        }else if (e.Parameters != "")
        {
            string[] parameters = e.Parameters.Split(';');
            int index = int.Parse(parameters[0]);
            bool isGroupRowSelected = bool.Parse(parameters[1]);
            for (int i = 0; i < gvDados.GetChildRowCount(index); i++)
            {
                DataRow row = gvDados.GetChildDataRow(index, i);
                gvDados.Selection.SetSelectionByKey(row["CodigoUnico"], isGroupRowSelected);
            }
        }
    }

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Tabela") + "" == "RecursoCronogramaProjeto")
        {
            e.Enabled = false;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "SubstRec");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "SubstRec", "Substituição de Usuários", this);
    }

    #endregion

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }

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

    protected void pnUsuarioDestino_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoUsuarioOrigem = e.Parameter;

        string comandoSQL =
  string.Format(@"
                SELECT us.CodigoUsuario
                      ,us.NomeUsuario
                      ,us.EMail
                      ,ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') AS IndicaUsuarioAtivoUnidadeNegocio
                      ,CASE WHEN ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') = 'S' THEN '' ELSE '(INATIVO)' END AS StatusUsuario
                  FROM Usuario  us INNER JOIN 
                       UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario
                 WHERE uun.CodigoUnidadeNegocio = {0}
                   AND (us.CodigoUsuario <> {1}) 
                 ORDER BY ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') DESC, us.NomeUsuario
                  ", codigoEntidadeUsuarioResponsavel, codigoUsuarioOrigem);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlUsuarioDestino.DataSource = ds.Tables[0];
            ddlUsuarioDestino.DataBind();
        }
    }
    
    protected void ddlUsuarioDestino_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        if(ddlUsuarioOrigem.Value != null)
        {
            string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel
                                                   , e.Filter, string.Format(@" and us.CodigoUsuario <> {0}", ddlUsuarioOrigem.Value.ToString()));

            cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
        }
    }

    protected void ddlUsuarioDestino_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null && ddlUsuarioOrigem.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand =
         string.Format(@"
                SELECT us.CodigoUsuario
                      ,us.NomeUsuario
                      ,us.EMail
                      ,ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') AS IndicaUsuarioAtivoUnidadeNegocio
                      ,CASE WHEN ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') = 'S' THEN '' ELSE '(INATIVO)' END AS StatusUsuario
                  FROM Usuario  us INNER JOIN 
                       UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario
                 WHERE uun.CodigoUnidadeNegocio = {0}
                   AND (us.CodigoUsuario <> {1}) 
                 ORDER BY ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') DESC, us.NomeUsuario
                  ", codigoEntidadeUsuarioResponsavel, ddlUsuarioOrigem.Value.ToString());

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
    }

}