using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Data;

public partial class _Projetos_Agil_CalendarioReuniaoDiariaSprint : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = -1;
    public int codigoIteracao = -1;
    public static string dataHoraAtual = "";
    public int codigoUsuarioLogado = -1;
    public int codigoEntidadeUsuarioResponsavel = 0;

   public static DateTime dtInicioPrevisto;
   public static DateTime dtTerminoPrevisto;

    protected void Page_Init(object sender, EventArgs e)
    {

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CP"] != null && (Request.QueryString["CP"].ToString() + "") != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string comandosql = string.Format(@"SELECT [CodigoIteracao]
                                                  ,[InicioPrevisto]
                                                  ,[TerminoPrevisto]
                                              FROM {0}.{1}.[Agil_Iteracao] where [CodigoProjetoIteracao] = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
             dtInicioPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["InicioPrevisto"].ToString());
             dtTerminoPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoPrevisto"].ToString());
             codigoIteracao = int.Parse(ds.Tables[0].Rows[0]["CodigoIteracao"].ToString());
        }
        
        if (!Page.IsPostBack)
        {
            dataHoraAtual = cDados.classeDados.getDateDB();
            calendario.SelectedDate = (DateTime.Compare(dtInicioPrevisto, DateTime.MinValue) == 0) ? DateTime.Now : dtInicioPrevisto;
        }
    }

    protected void ASPxCalendar1_DayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
    {
        DateTime dataAtual = DateTime.Parse(dataHoraAtual);

        string funcao_javascript = "abreReuniaoDiaria(" + codigoProjeto + "," + codigoIteracao + "," + e.Date.Day + "," + e.Date.Month + "," + e.Date.Year + ")";
        
        if (dtInicioPrevisto <= e.Date && e.Date <= dtTerminoPrevisto)
        {
            e.Cell.Font.Bold = true;
            if (e.Date <= dataAtual && e.Date.DayOfWeek != DayOfWeek.Sunday)
            {
                e.Cell.Text = @"<a onclick= " + funcao_javascript + " href='#'>" + e.Date.Day.ToString() + "</a>";

                string whereAux = string.Format(@" AND ev.CodigoTipoAssociacao = (SELECT {0}.{1}.[f_GetCodigoTipoAssociacao]('PR'))   
                                               AND ev.CodigoTipoEvento = (SELECT top 1 CodigoTipoEvento 
                                                                            FROM TipoEvento 
                                                                           WHERE DescricaoTipoEvento like '%Sprint%')
                                               AND  (YEAR(ev.TerminoReal) = {2} 
                                               AND MONTH(ev.TerminoReal) = {3}
                                               AND   DAY(ev.TerminoReal) = {4})
                                               AND ev.CodigoObjetoAssociado = {5}", cDados.getDbName(), cDados.getDbOwner(), e.Date.Year, e.Date.Month, e.Date.Day, codigoProjeto);
                DataSet dsReuniao = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), whereAux, codigoUsuarioLogado.ToString(), "PR_AdmReu");
                if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
                {
                    e.Cell.BackColor = Color.LightGreen;
                }
                else
                {
                    e.Cell.BackColor = Color.Red;
                }
            }
        }

        

    }
}