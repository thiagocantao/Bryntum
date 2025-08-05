<%@ WebHandler Language="C#" Class="GanttSaveHandler" %>

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


public class GanttSaveHandler : IHttpHandler
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

    public void ProcessRequest(HttpContext context)
    {
        var jsonString = string.Empty;
        using (var inputStream = new StreamReader(context.Request.InputStream))
        {
            jsonString = inputStream.ReadToEnd();
        }

        if(!string.IsNullOrEmpty(jsonString))
        {
            GanttSaveDataTransfer ganttSave = jsonString.JsonToEntity<GanttSaveDataTransfer>();
            UowApplication.GetUowApplication<CronogramaGanttApplication>().SaveCronograma(ganttSave);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}