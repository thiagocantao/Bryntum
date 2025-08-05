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
using System.Drawing;
using DevExpress.Web;

public partial class _Portfolios_Administracao_CalculoPesoFatoresCategoria : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoCategoria = -1;

    DataTable dtObjetos = new DataTable();
    DataTable dtValoresObjetos = new DataTable();

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CC"] != null && Request.QueryString["CC"].ToString() != "")
            codigoCategoria = int.Parse(Request.QueryString["CC"].ToString());
                
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();

        int altura = int.Parse(Request.QueryString["alt"].ToString());
        int largura = int.Parse(Request.QueryString["larg"].ToString());

        gridMatriz.Settings.VerticalScrollableHeight = (altura - 210);
        gridMatriz.Settings.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Visible;

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        carregaObjetos();
        carregaGrid();
        montaCampos();

        gridMatriz.Settings.ShowFilterRow = false;
        gridMatriz.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void montaCampos()
    {
        DataSet ds = cDados.getCategoria("AND CodigoCategoria = " + codigoCategoria);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            txtCategoria.Text = ds.Tables[0].Rows[0]["DescricaoCategoria"].ToString();        
        
    }

    private void carregaObjetos()
    {
        DataSet ds = cDados.getFatoresEntidade(codigoEntidade, "");

        if (cDados.DataSetOk(ds))
            dtObjetos = ds.Tables[0];

        DataSet dsValores = cDados.getValoresFatoresCategoria(codigoCategoria, codigoEntidade, "");

        if (cDados.DataSetOk(dsValores))
            dtValoresObjetos = dsValores.Tables[0];
    }

    private void carregaGrid()
    {
        gridMatriz.Columns.Clear();
        gridMatriz.AutoGenerateColumns = true;

        DataTable dtGrid = new DataTable();

        dtGrid.Columns.Add("Fator");

        foreach (DataRow dr in dtObjetos.Rows)
        {
            dtGrid.Columns.Add(dr["NomeFatorPortfolio"].ToString(), Type.GetType("System.Decimal"));
        }
        
        int count = 0;

        foreach (DataRow dr in dtObjetos.Rows)
        {
            DataRow drLinha = dtGrid.NewRow();

            drLinha["Fator"] = dr["NomeFatorPortfolio"].ToString();

            if (cDados.DataTableOk(dtValoresObjetos) && !IsPostBack)
                
                for (int i = 0; i < dtObjetos.Rows.Count; i++)
                {
                    int codigoLinha = int.Parse(dr["CodigoFatorPortfolio"].ToString());
                    int codigoColuna = int.Parse(dtObjetos.Rows[i]["CodigoFatorPortfolio"].ToString());

                    DataRow[] drValor = dtValoresObjetos.Select("CodigoObjetoCriterioDe = " + codigoLinha + " AND CodigoObjetoCriterioPara = " + codigoColuna);

                    if(drValor.Length > 0)
                        drLinha[i + 1] = drValor[0]["ValorRelacaoObjetoDePara"].ToString();
                }

            dtGrid.Rows.InsertAt(drLinha, count);
            count++;
        }

        dtGrid.Columns.Add("Peso");

        gridMatriz.DataSource = dtGrid;
        gridMatriz.DataBind();
    }

    protected void gridMatriz_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.Style.Add("height", "28px");
        e.Cell.Attributes.Add("id", "cell_" + e.VisibleIndex + "_" + e.DataColumn.Index);
        if (e.DataColumn.FieldName == "Fator")
        {
            e.DataColumn.Caption = " ";
            e.Cell.HorizontalAlign = HorizontalAlign.Left;
            e.Cell.BackColor = Color.FromName("#EBEBEB");
        }
        else if (e.DataColumn.FieldName == "Peso")
        {
            e.Cell.BackColor = Color.FromName("#EBEBEB");
            ASPxTextBox txt = new ASPxTextBox();
            txt.ID = "peso_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.ClientInstanceName = "peso_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.Width = new Unit("100%");
            txt.Height = 28;
            txt.HorizontalAlign = HorizontalAlign.Center;
            txt.Border.BorderStyle = BorderStyle.None;
            txt.ReadOnly = true;
            txt.Font.Name = "Verdana";
            txt.Font.Size = new FontUnit("8pt");
            txt.DisabledStyle.ForeColor = Color.Black;
            txt.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            txt.DisplayFormatString = "{0:p2}";
            txt.Value = null;
            e.Cell.Controls.Add(txt);
        }
        else
        {
            

            ASPxTextBox txt = new ASPxTextBox();
            txt.ID = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Index;
            txt.Width = new Unit("100%");
            txt.Height = 28;
            txt.HorizontalAlign = HorizontalAlign.Center;
            txt.Border.BorderStyle = BorderStyle.None;
            txt.ClientSideEvents.LostFocus = "function(s, e) {alteraValor(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ");}";
            txt.ClientSideEvents.KeyUp = "function(s, e) {if(e.htmlEvent.keyCode == 13){alteraValor(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ")};}";
            txt.ClientSideEvents.Init = "function(s, e) {alteraValor(s, e, " + e.VisibleIndex + ", " + e.DataColumn.Index + ");}";
            txt.Font.Name = "Verdana";
            txt.Font.Size = new FontUnit("8pt");            
            txt.DisabledStyle.ForeColor = Color.Black;
            //txt.MaskSettings.Mask = "<0..9>/<0..9>";
            txt.MaxLength = 3;
            txt.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            
            if(e.CellValue.ToString() != "")
                txt.Text = trataValor(float.Parse(e.CellValue.ToString()));
            e.Cell.Text = "";
            
            if (e.DataColumn.VisibleIndex == e.VisibleIndex + 1)
            {
                e.Cell.BackColor = Color.FromName("#EBEBEB");                
                txt.Text = "1";
                txt.ClientEnabled = false;
            }   

            e.Cell.Controls.Add(txt);
        }
    }

    private string trataValor(float valor)
    {
        if (valor >= 1)
            return string.Format("{0:n0}", valor);
        else
        {
            float novoValor = 1 / valor;

            return string.Format("1/{0:n0}", novoValor);
        }
    }

    private void salvaRegistro()
    {
        string msg = "";
        bool retorno = false;
        int regAfetados = 0;

        int[] codigosCriterios = new int[dtObjetos.Rows.Count]; //new int[lbSelecionados.Items.Count];
        float[] valoresCriterios = new float[(int)Math.Pow((dtObjetos.Rows.Count), 2)];

        int count = 0;

        for (int i = 0; i < dtObjetos.Rows.Count; i++)
        {
            codigosCriterios[i] = int.Parse(dtObjetos.Rows[i]["CodigoFatorPortfolio"].ToString());

            DataRow dr = gridMatriz.GetDataRow(i);

            for (int j = 0; j < dtObjetos.Rows.Count; j++)
            {
                float valorHf = -1;

                string valorObjeto = hfValores.Get("Criterio_" + i + "_" + (j + 1)).ToString();

                if (valorObjeto != "")
                {
                    if (valorObjeto.IndexOf('/') == -1)
                        valorHf = float.Parse(valorObjeto);
                    else
                    {
                        float valorCima = float.Parse(valorObjeto.Substring(0, 1));
                        float valorBaixo = float.Parse(valorObjeto.Substring(valorObjeto.IndexOf('/') + 1));

                        valorHf = valorCima / valorBaixo;
                    }
                }

                valoresCriterios[count] = valorHf;
                count++;
            }
        }

        retorno = cDados.incluiValoresObjetosCategoria(codigoCategoria, codigoCategoria, codigoEntidade, "CT", codigosCriterios, valoresCriterios, ref msg, ref regAfetados);

        if (!retorno)
        {
            callbackSalvar.JSProperties["cp_msg"] = "Erro ao salvar os dados!";
            callbackSalvar.JSProperties["cp_status"] = "0";
        }
        else
        {
            callbackSalvar.JSProperties["cp_msg"] = "Dados salvos com sucesso!";
            callbackSalvar.JSProperties["cp_status"] = "1";
        }

        carregaGrid();
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        salvaRegistro();
    }
}
