/* 
 Data Creação: 1 de Fevereiro 2011
 *              
 * JavaScript vinculado: Portfolio/scripts/ParcelasContrato.js
 *

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
using System.Drawing;
using DevExpress.Web;

public partial class _Projetos_Administracao_ParcelasContrato : System.Web.UI.Page
{
    private dados cDados;

    private int idUsuarioLogado;
    private int idEntidadeLogada;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    //private string parcelasAtrasadas = "";
    //private string filtroNome = "";

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
        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        //Filtro
        //if (Request.QueryString["Atrasados"] != null)
        //    parcelasAtrasadas = Request.QueryString["Atrasados"].ToString();
        //if (Request.QueryString["Filtro"] != null)
        //    filtroNome = Request.QueryString["Filtro"].ToString();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        onHearderTela();

        if (!IsPostBack)
        {
            hfGeral.Set("KeyGrid", "");
        }
        DataSet ds = cDados.getParametrosSistema("utilizaConvenio");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.JSProperties["cp_utilizaConvenio"] = ds.Tables[0].Rows[0][0].ToString();
        }
        populaGrid();
        filtraRegistros();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PCONTR", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    #region VARIOS

    private void onHearderTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ParcelasContrato.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ParcelasContrato"));

        cDados.aplicaEstiloVisual(Page);
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 250;
    }

    #endregion

    #region BUTTON

    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        gveExportar.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    #endregion

    #region DVGRID

    private void populaGrid()
    {
        // para o parâmetro 'orderBy' considerar o seguinte:
        // tabela ParcelaContrato AS pcont
        // tabela Contrato        AS cont
        // tabela UnidadeNegocio  AS unid
        // tabela Usuario         AS usu
        DataSet ds = cDados.getParcelasDoContrato(idEntidadeLogada, idUsuarioLogado, "", "ORDER BY pcont.DataVencimento");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void filtraRegistros()
    {
        if (!IsPostBack && !IsCallback)
        {
            string nomeUsuario = "";
            //NomeUsuario - usuario logado.
            if (Request.QueryString["Filtro"] != null && Request.QueryString["Filtro"].ToString() == "NomeUsuario")
            {
                DataSet dsFiltro = cDados.getUsuarios(" AND u.CodigoUsuario = " + idUsuarioLogado);

                if (cDados.DataSetOk(dsFiltro) && cDados.DataTableOk(dsFiltro.Tables[0]))
                {
                    nomeUsuario = dsFiltro.Tables[0].Rows[0]["NomeUsuario"].ToString();
                    gvDados.FilterExpression = "[NomeUsuario] = '" + nomeUsuario + "'";
                }
            }
            //SituacaoParcela - Atrasada
            if (Request.QueryString["Atrasados"] != null && Request.QueryString["Atrasados"].ToString() == "S")
            {
                if (gvDados.FilterExpression == "")
                    gvDados.FilterExpression = string.Format("[SituacaoParcela] = 'Atrasada'");
                else
                    gvDados.FilterExpression += string.Format(" AND [SituacaoParcela] = 'Atrasada'");
            }
            //Vencendo
            if (Request.QueryString["Vencendo"] != null && Request.QueryString["Vencendo"].ToString() == "S")
            {
                if (gvDados.FilterExpression == "")
                    gvDados.FilterExpression = string.Format("([SituacaoParcela] = 'Vencendo' OR [SituacaoParcela] = 'Atrasada')");
                else
                    gvDados.FilterExpression += string.Format(" AND ([SituacaoParcela] = 'Vencendo' OR [SituacaoParcela] = 'Atrasada')");
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();

        if (e.RowType == GridViewRowType.Data)
        {
            string situacaoReal = e.GetValue("SituacaoParcela").ToString();

            if (situacaoReal == "Atrasada")
            {
                int ri = Int32.Parse("E4", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("E1", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.ForeColor = Color.Red;
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "Objeto")
        {
            if (e.CellValue.ToString().Length > 45)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 45) + "...";
            }
        }
    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    #endregion

    #region BANCO DE DADOS

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {   // busca a chave primaria
        string[] chave = hfGeral.Get("KeyGrid").ToString().Split(';');

        string msg = "";
        string valorPago = txtValorPago.Text;
        string dataPagamento = deDataPagamento.Text;
        string historico = mmHistorico.Text;

        try
        {
            if (cDados.atualizaParcelasDoContrato(dataPagamento, valorPago, historico, idUsuarioLogado, int.Parse(chave[0]), int.Parse(chave[1]), int.Parse(chave[2])))
            {
                populaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                gvDados.ClientVisible = false;
            }
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = ex.Message;
        }
        return msg;
    }

    #endregion
    protected void gveExportar_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
