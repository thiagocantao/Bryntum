<%@ WebHandler Language="C#" Class="GanttBalanceamentoHandler" %>

using System.Web;
using System.Text;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Application;
using Cdis.Brisk.Application.Applications.Cronograma;
using Cdis.Brisk.Service.Services.Cronograma;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.DataTransfer.Gantt;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class GanttBalanceamentoHandler : IHttpHandler
{
    /// <summary>
    /// construtor
    /// </summary>
    private UnitOfWorkApplication _uowApplication;

    /// <summary>
    /// UowApplication
    /// </summary>
    public UnitOfWorkApplication UowApplication
    {
        get
        {

            if (_uowApplication == null)
            {
                string strCon = Cdis.Brisk.Infra.Core.Secutiry.ConnectString.GetStringConexao();
                _uowApplication = new UnitOfWorkApplication(strCon);
            }
            return _uowApplication;
        }
    }

    public void ProcessRequest (HttpContext context)
    {        
        int codEntidade = context.Request.Headers["idEntidade"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idEntidade"].ToString());
        int codPortfolio = context.Request.Headers["idPortfolio"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idPortfolio"].ToString());
        int numCenario = context.Request.Headers["numCenario"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["numCenario"].ToString());

        GetJsonProject(context, codEntidade, codPortfolio, numCenario);
    }


    public void GetJsonProject(HttpContext context,int codEntidade,int codPortfolio,int numCenario)
    {
        var ganttDataset = UowApplication.GetUowApplication<CronogramaGanttBalanceamentoApplication>().GetGanttDatasetDataTransfer(codEntidade, codPortfolio, numCenario, typeof(Resources.traducao));
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.Write(ganttDataset.ToJson());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }



}