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

public partial class _Portfolios_frameProposta_RH : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjetoSelecionado, codigoEntidadeUsuarioResponsavel;

    public string alturaTela = "";

    DataTable dtPrincipal = new DataTable();

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

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
        }

        if (Request.QueryString["PopUp"] != null && Request.QueryString["PopUp"].ToString() == "S")
            btnFechar.ClientVisible = true;

        codigoProjetoSelecionado = cDados.getInfoSistema("CodigoProjeto") == null ? -1 : int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaGrid();

        if (grid.Columns.Count > 0 && (grid.Columns[0] is GridViewCommandColumn))
        {
            GridViewCommandColumn SelectCol = grid.Columns[0] as GridViewCommandColumn;
            /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";*/
            grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
        }

        //defineTamanhoGrafico();
    }

    private void carregaGrid()
    {
        grid.Columns.Clear();

        grid.AutoGenerateColumns = true;

        dtPrincipal = getRH();

        grid.DataSource = dtPrincipal;

        grid.DataBind();

        ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
        ((GridViewDataTextColumn)grid.Columns[0]).Caption = Resources.traducao.frameProposta_RH_descri__o_do_recurso;
        ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
        ((GridViewDataTextColumn)grid.Columns[0]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;



        grid.Columns[0].Width = 220;

        grid.Columns[1].Visible = false; // Coluna CodigoRecurso
        grid.Columns[2].Visible = false; // Coluna CodigoPeriodicidade

        for (int i = 3; i < grid.Columns.Count; i++)
        {
            grid.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataTextColumn)grid.Columns[i]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
            ((GridViewDataTextColumn)grid.Columns[i]).PropertiesTextEdit.EncodeHtml = false;
            ((GridViewDataTextColumn)grid.Columns[i]).PropertiesTextEdit.MaskSettings.Mask = "<0..10000000000>";
            ((GridViewDataTextColumn)grid.Columns[i]).PropertiesTextEdit.MaskSettings.PromptChar = ' ';
            ((GridViewDataTextColumn)grid.Columns[i]).PropertiesTextEdit.Width = new Unit("80px");
            ((GridViewDataTextColumn)grid.Columns[i]).Width = 100;
            ((GridViewDataTextColumn)grid.Columns[i]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        }

        grid.DataBind();

        if (!(grid.Columns[0] is GridViewCommandColumn))
        {
            GridViewCommandColumn SelectCol = new GridViewCommandColumn();
            SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;

            SelectCol.ShowEditButton = true;
            /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            SelectCol.EditButton.Image.AlternateText = "Editar";
            SelectCol.EditButton.Text = "Editar";*/

            grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            grid.SettingsCommandButton.EditButton.Image.AlternateText = "Editar";
            grid.SettingsCommandButton.EditButton.Text = Resources.traducao.frameProposta_RH_editar;

            SelectCol.ShowUpdateButton = true;
            /*SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            SelectCol.UpdateButton.Text = "Salvar";*/
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            grid.SettingsCommandButton.UpdateButton.Text = Resources.traducao.frameProposta_RH_salvar;

            SelectCol.ShowCancelButton = true;
            /*SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
            SelectCol.CancelButton.Text = "Cancelar";*/

            grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
            grid.SettingsCommandButton.CancelButton.Text = Resources.traducao.frameProposta_RH_cancelar;

            if (((cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S")) ||
                   ((Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S")))
            {
                SelectCol.VisibleIndex = -1;
                SelectCol.Visible = false;
            }
            else
            {
                SelectCol.VisibleIndex = 0;
                SelectCol.Visible = true;
            }

            SelectCol.Width = 75;
            SelectCol.Caption = " ";

            grid.Columns.Insert(0, SelectCol);
        }


    }

    private DataTable getRH()
    {
        DataTable dtRH = cDados.getRHProposta(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado, "").Tables[0];
        DataTable dtNovoRH = new DataTable();
        DataRow drLinha, drLinhaTotal;

        int ano = 0, mes = 0;
        int codigoRecurso, codigoRecursoTemp, i;

        dtNovoRH.Columns.Add("DescricaoRecurso");
        dtNovoRH.Columns.Add("CodigoRecurso");
        dtNovoRH.Columns.Add("CodigoPeriodicidade");

        foreach (DataRow dr in dtRH.Select("", "_Ano, _Mes"))
        {
            if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
            {
                dtNovoRH.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                ano = int.Parse(dr["_Ano"].ToString());
                mes = int.Parse(dr["_Mes"].ToString());
            }
        }

        codigoRecurso = 0;
        i = 0;
        drLinha = null;
        drLinhaTotal = dtNovoRH.NewRow();
        drLinhaTotal[0] = "Total";
        drLinhaTotal[1] = "-1";

        for (int j = 3; j < dtNovoRH.Columns.Count; j++)
        {
            drLinhaTotal[j] = 0.00;
        }

        foreach (DataRow dr in dtRH.Select("", "_CodigoRecurso, _Ano, _Mes"))
        {
            codigoRecursoTemp = (int)dr["_CodigoRecurso"];

            if (codigoRecursoTemp != codigoRecurso)
            {
                drLinha = dtNovoRH.NewRow();
                dtNovoRH.Rows.Add(drLinha);
                drLinha[0] = dr["DescricaoRecurso"].ToString();
                drLinha[1] = dr["_CodigoRecurso"].ToString();
                drLinha[2] = dr["CodigoPeriodicidade"].ToString();
                i = 3;
                codigoRecurso = codigoRecursoTemp;
            }

            drLinha[i] = dr["Valor"].ToString();
            drLinhaTotal[i] = (double)drLinhaTotal[i] + (double)drLinha[i];
            i++;
        };

        dtNovoRH.Rows.Add(drLinhaTotal);
        return dtNovoRH;
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string comandoSQL;
        int i = 0;
        int regAf = 0;
        bool retorno = true;

        DataTable dt = cDados.getRHProposta(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado, "").Tables[0];

        foreach (DataRow dr in dt.Select(string.Format("_CodigoRecurso = {0}", e.Keys[0].ToString()), "_Ano, _Mes"))
        {
            comandoSQL = string.Format(@" EXEC {0}.{1}.p_gravaRegistroPrevisaoGrupoRH {2}, {3}, {4}, {5}, {6}, {7};"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , codigoProjetoSelecionado
            , dr["_CodigoRecurso"].ToString()
            , dr["CodigoPeriodicidade"].ToString()
            , dr["_Ano"].ToString()
            , dr["_Mes"].ToString()
            , e.NewValues[i] == null ? "NULL" : e.NewValues[i].ToString().Replace(",", "."));

            i++;
            regAf = 0;
            retorno = cDados.execSQL(comandoSQL, ref regAf);
        }

        // se gravou algum registro, chama a proc que 'normaliza' a previsão de rh de um projeto
        if (i > 0)
        {
            comandoSQL = string.Format(@" EXEC {0}.{1}.p_AtualizaSelecaoRecurso {2}, {3};"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , codigoEntidadeUsuarioResponsavel
            , codigoProjetoSelecionado);

            retorno = cDados.execSQL(comandoSQL, ref regAf);
        }

        carregaGrid();
        e.Cancel = true;
        grid.CancelEdit();
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex == grid.VisibleRowCount - 1)
        {
            e.Row.Cells[0].Text = "&nbsp;";
            e.Row.Style.Add("background-color", "#EBEBEB");
            e.Row.Style.Add("font-weight", "bold");
        }
        else if (e.VisibleIndex != -1)
        {
            int indexColuna = 0;

            if (grid.Columns[0].Visible == true && e.Row.Cells.Count > 1)
                indexColuna = 1;

            if (dtPrincipal.Rows.Count > 0)
                e.Row.Cells[indexColuna].Text = string.Format(@"<a href='#' title='Ver Disponibilidade dos Recursos' onclick='abreGrafico({1});'>{0}</a>", dtPrincipal.Rows[e.VisibleIndex]["DescricaoRecurso"].ToString()
                    , dtPrincipal.Rows[e.VisibleIndex]["CodigoRecurso"].ToString());
        }

        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[1].Style.Add("background-color", "#EBEBEB");
        }

        e.Row.Cells[0].Style.Add("background-color", "#FFFFFF");
    }

    private void defineTamanhoGrafico()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        alturaTela = (170).ToString() + "px";
    }

    protected void grid_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGrid();
    }
}
