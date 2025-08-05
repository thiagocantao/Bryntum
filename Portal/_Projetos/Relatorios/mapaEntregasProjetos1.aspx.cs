//Revisado
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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPivotGrid;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Projetos_Relatorios_mapaEntregasProjetos1 : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public string alturaTabela;
    public string larguraTabela = "";
    public bool exportaOLAPTodosFormatos = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_PrjRelMapEnt");
        }

        this.Title = cDados.getNomeSistema();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            txtInicio.Value = DateTime.Now.AddDays(-(double)DateTime.Now.Day + 1);
            int diasNoMes = 30;
            
            if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) == 30)
            {
                diasNoMes = 30;
            }
            if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) == 31)
            {
                diasNoMes = 31;
            }
            if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) == 29)
            {
                diasNoMes = 29;
            }
            if (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) == 28)
            {
                diasNoMes = 28;
            }

            txtTermino.Value = DateTime.Now.AddDays(-(double)DateTime.Now.Day + diasNoMes);            

            cDados.aplicaEstiloVisual(Page);
        }
        
        populaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 100) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 25) + "px";

       /*
         style="overflow: auto; width: "
        */
        Div1.Style.Add("overflow", "auto");
        //Div1.Style.Add("width", larguraTabela);
        //Div1.Style.Add("height", (alturaPrincipal - 250 - 10).ToString());
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 300;
    }  

    private void populaGrid()
    {

        DataSet ds1 = new DataSet();
        string comandoSQL = string.Format(
        @"BEGIN
            DECLARE  @DataInicioParametro AS DATETIME
            DECLARE  @DataTerminoParametro as DATETIME
            SET @DataInicioParametro = CONVERT(DateTime, '{3}', 103)
            SET @DataTerminoParametro = CONVERT(DateTime, '{4}', 103)
            SELECT p.CodigoProjeto,
                  (select NomeProjeto from projeto where codigoprojeto = lp.codigoprojetopai) as NomePrograma,
                   p.NomeProjeto, 
                   tc.NomeTarefa, 
                   tc.TerminoLB, 
                   tc.TerminoReal,
				   CASE WHEN tc.TerminoLB is null THEN ''
					    WHEN tc.TerminoReal is not null THEN 'Concluída'
					    WHEN tc.TerminoReal is null  and tc.TerminoLB < GETDATE() THEN 'Atrasada'
					    WHEN tc.TerminoReal is null  and tc.TerminoLB >= GETDATE() THEN 'Planejada'
				    END AS Situacao, 
                    u.NomeUnidadeNegocio    
			FROM {0}.{1}.Projeto p INNER JOIN
                 {0}.{1}.UnidadeNegocio u ON u.CodigoUnidadeNegocio = p.CodigoUnidadeNegocio INNER JOIN
				 {0}.{1}.CronogramaProjeto cp ON ( cp.CodigoProjeto = p.CodigoProjeto ) INNER JOIN
				 {0}.{1}.TarefaCronogramaProjeto tc ON ( tc.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto ) INNER JOIN
				 {0}.{1}.TipoTarefaCronograma ttc ON ( ttc.CodigoTipoTarefaCronograma = tc.CodigoTipoTarefaCronograma )INNER JOIN
                 {0}.{1}.Status st ON ( st.CodigoStatus = p.CodigoStatusProjeto ) INNER JOIN
                 {0}.{1}.[linkProjeto] lp on (lp.CodigoProjetoFilho = p.CodigoProjeto )
           WHERE st.IniciaisStatus = 'EM_EXECUCAO'
             AND tc.DataExclusao IS NULL              
             AND ttc.codigoEntidade = p.CodigoEntidade
             AND p.codigoEntidade = {2}
             AND ttc.IniciaisTipoControladoSistema = 'ENTREGA'
             AND (tc.TerminoLB BETWEEN @DataInicioParametro and @DataTerminoParametro OR
                  tc.TerminoReal BETWEEN @DataInicioParametro and @DataTerminoParametro)
        END
        ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, txtInicio.Text, txtTermino.Text);
        ds1 = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds1.Tables[0];
        gvDados.DataBind();
    }

    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        populaGrid();
    }
    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {

    }
    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {

    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "MapEntPrj1");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "MapEntPrj1", lblTituloTela.Text, this);
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
