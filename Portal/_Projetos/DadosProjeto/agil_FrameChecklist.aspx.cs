using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_agil_FrameChecklist : System.Web.UI.Page
{
    dados cDados;

    private int _codigoItem;
    /// <summary>
    /// Código do item que abriu este frame
    /// </summary>
    public int CodigoItem
    {
        get { return _codigoItem; }
        set
        {
            _codigoItem = value;
        }
    }

    private int _codigoUsuarioLogado;
    /// <summary>
    /// Código do usuário que está atualmente logado
    /// </summary>
    public int CodigoUsuarioLogado
    {
        get { return _codigoUsuarioLogado; }
        set
        {
            _codigoUsuarioLogado = value;
        }
    }

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
        CodigoItem = int.Parse(Request.QueryString["CI"] + "");
        CodigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
        carregaGrid();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        var tamanhoCampo = 100;//int.Parse(cDados.getMetadadosTabelaBanco("Agil_TarefaChecklist", "DetalheChecklist").Tables[0].Rows[0]["Tamanho"].ToString());
        txtIncluiCheckList.MaxLength = tamanhoCampo;
        ((GridViewDataTextColumn)gvDados.Columns["DetalheChecklist"]).PropertiesTextEdit.MaxLength = tamanhoCampo;
        gvDados.Border.BorderWidth = new Unit("5px");
        Color cor = new Color();
        cor = Color.FromName("#619340");
        gvDados.Border.BorderColor = cor;
        if (Page.IsPostBack)
            txtIncluiCheckList.Focus();
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoTarefaChecklist]
              ,[CodigoItem]
              ,[IndicaConcluido]
              ,[DetalheChecklist]
              ,[CodigoUsuarioInclusao]
          FROM [dbo].[Agil_TarefaChecklist]
        WHERE [CodigoItem] = {0}", CodigoItem);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

        gvDados.Selection.BeginSelection();
        gvDados.Selection.UnselectAll();
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            object indicaObjetoSelecionado =  gvDados.GetRowValues(i, "IndicaConcluido");
            if ((bool)indicaObjetoSelecionado == true)
                gvDados.Selection.SelectRow(i);
        }
        gvDados.Selection.EndSelection();
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        var detalhechecklist = e.NewValues[0];
        string mensagemErro = "";
        var codigotarefa = ((ASPxGridView)sender).GetRowValues(((ASPxGridView)sender).FocusedRowIndex, "CodigoTarefaChecklist");
        string comandoSQL = string.Format(@"
        UPDATE [dbo].[Agil_TarefaChecklist]
           SET [DetalheChecklist] = '{2}'
          WHERE [CodigoItem] = {0} and CodigoTarefaChecklist = {1}", CodigoItem, codigotarefa, detalhechecklist);
        int registrosAfetados = 0;
        try
        {
            cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {       
        if (e.GetValue("IndicaConcluido") != null && (e.GetValue("IndicaConcluido").ToString() == "True"))
        {
            e.Row.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
        }
    }


    protected void gvDados_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        e.EditForm.ForeColor = Color.FromArgb(12, 79, 23);
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "DetalheChecklist")
        {
            e.Cell.ToolTip = "Qualquer alteração no checklist é salva automaticamente, não sendo necessário acionar botão 'Salvar'.";
        }
    }

    protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpAcaoExecutada"] = "";

        string comandoSQL = "";
        int parametroInt = -1;
        if (int.TryParse(e.Parameter, out parametroInt) == true)
        {
            if (int.Parse(e.Parameter) > -1)
            {
                comandoSQL = string.Format(@"
        DELETE FROM [dbo].[Agil_TarefaChecklist]
        WHERE CodigoTarefaChecklist = {0}", e.Parameter);
                ((ASPxCallbackPanel)sender).JSProperties["cpAcaoExecutada"] = "Excluir";
            }
            else
            {
                comandoSQL = string.Format(@"
            DECLARE @DetalheChecklist as varchar(2000)
            DECLARE @CodigoUsuarioInclusao as int
            DECLARE @CodigoItem as int

            SET @DetalheChecklist = '{0}'
            SET @CodigoUsuarioInclusao = {1}
            SET @CodigoItem = {2}

            IF NOT EXISTS(SELECT 1 FROM [Agil_TarefaChecklist] 
                           WHERE [DetalheChecklist] = @DetalheChecklist 
                             AND [CodigoUsuarioInclusao] = @CodigoUsuarioInclusao 
                             AND [CodigoItem] = @CodigoItem)
            BEGIN
                INSERT INTO [dbo].[Agil_TarefaChecklist]
                   ([DetalheChecklist]
                   ,[CodigoUsuarioInclusao]
                   ,[CodigoItem])
                VALUES
                   (@DetalheChecklist,
                   @CodigoUsuarioInclusao,
                   @CodigoItem)
            END", txtIncluiCheckList.Text.Replace("'", "'+char(39)+'"), CodigoUsuarioLogado, CodigoItem);
                ((ASPxCallbackPanel)sender).JSProperties["cpAcaoExecutada"] = "Incluir";
            }
        }
        int registrosAfetados = 0;
        try
        {
            cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }
    }

    protected void callbackTela1_Callback(object source, CallbackEventArgs e)
    {
        var codigoTarefa = e.Parameter.Split('|')[0];
        var acao = e.Parameter.Split('|')[1];

        string comandoSQL = string.Format(@"
            UPDATE [dbo].[Agil_TarefaChecklist]
            SET [IndicaConcluido] = {2}
            WHERE [CodigoItem] = {0} and CodigoTarefaChecklist = {1}", CodigoItem, codigoTarefa, (acao == "SELECT") ? 1 : 0);
        ((ASPxCallback)source).JSProperties["cpAcaoExecutada"] = "Selecionar";
        try
        {
            int registrosAfetados = 0;
            cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            string mensagemErro = ex.Message;
        }
    }
}