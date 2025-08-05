<%@ WebHandler Language="C#" Class="GanttProjetoMetaHandler" %>

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


public class GanttProjetoMetaHandler : IHttpHandler
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
        int idEntidade = context.Request.Headers["idEntidade"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idEntidade"].ToString());
        int idUsuario = context.Request.Headers["idUsuario"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idUsuario"].ToString());
        int idCarteira = context.Request.Headers["idCarteira"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idCarteira"].ToString());

        GetJsonProject(context, idEntidade, idUsuario, idCarteira);
    }


    public void GetJsonProject(HttpContext context,int codEntidade,int idUsuario,int idCarteira)
    {
        var ganttDataset = UowApplication.GetUowApplication<CronogramaGanttProjetoMetaApplication>().GetGanttProjetoMetaDatasetDataTransfer(codEntidade, idUsuario, idCarteira, typeof(Resources.traducao));
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