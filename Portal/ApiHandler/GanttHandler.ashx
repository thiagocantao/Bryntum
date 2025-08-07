<%@ WebHandler Language="C#" Class="GanttHandler" %>

using System.Web;
using System.Text;
using CDIS;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Application;
using Cdis.Brisk.Application.Applications.Cronograma;
using Cdis.Brisk.Service.Services.Cronograma;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.DataTransfer.Gantt;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;


public class GanttHandler : IHttpHandler
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
        string typeResult = context.Request.Headers["typeResult"];
        int idProjeto = context.Request.Headers["idprojeto"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idprojeto"].ToString());
        bool isCarregarHtmlCaminhoCritico = context.Request.Headers["isCarregarHtmlCaminhoCritico"] == null ? true : System.Convert.ToBoolean((context.Request.Headers["isCarregarHtmlCaminhoCritico"].ToString().ToLower()));

        switch (typeResult)
        {
            case "jsonProject":
                short numLinhaBase = (context.Request.Headers["numLinhaBase"] == null || context.Request.Headers["numLinhaBase"] == "undefined")
                                    ? (short)-1
                                    : System.Convert.ToInt16(context.Request.Headers["numLinhaBase"].ToString());

                GetJsonProject(context, idProjeto, numLinhaBase, isCarregarHtmlCaminhoCritico);
                break;
            case "exportToPdf":
                GetExportToPdf(context, idProjeto);
                break;
            case "getHtmlGantt":
                GetHtmlGantt(context, idProjeto);
                break;
            case "desbloquearCronograma":
                int idUsuario = context.Request.Headers["iduser"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["iduser"].ToString());
                DesbloquearCronograma(context, idProjeto, idUsuario);
                break;
            case "bloquearCronograma":
                BloquearCronograma(context, idProjeto);
                break;
        }
    }

    public void DesbloquearCronograma(HttpContext context, int idProjeto, int idUsuario)
    {
        ResultRequestDomain result = UowApplication.UowService.GetUowService<CronogramaProjetoService>().DesbloquearCronograma(idProjeto, idUsuario);
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.Write(result.ToJson());
    }

    public void BloquearCronograma(HttpContext context, int idProjeto)
    {
        int identity = context.Request.Headers["identity"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["identity"].ToString());
        ResultRequestDomain result = UowApplication.UowService.GetUowService<CronogramaProjetoService>().BloquearCronograma(idProjeto, identity);
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.Write(result.ToJson());
    }

    public void GetHtmlGantt(HttpContext context, int idProjeto)
    {
        var jsonString = string.Empty;
        using (var inputStream = new StreamReader(context.Request.InputStream))
        {
            jsonString = inputStream.ReadToEnd();
        }

        List<TaskGanttDataTransfer> listTask = jsonString.JsonToEntity<List<TaskGanttDataTransfer>>();
        if (listTask.Any())
        {
            string htmlDaCoisa = UowApplication.GetUowApplication<CronogramaGanttApplication>().MountHtmlTaskGantt(idProjeto, listTask, typeof(Resources.traducao));
            context.Response.Clear();
            context.Response.ContentType = "application/text";
            context.Response.Buffer = true;
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Write(htmlDaCoisa);
            context.Response.End();
            context.Response.Close();
        }
    }

    public void GetExportToPdf(HttpContext context, int idProjeto)
    {
        var jsonString = string.Empty;
        using (var inputStream = new StreamReader(context.Request.InputStream))
        {
            jsonString = inputStream.ReadToEnd();
        }
        List<TaskGanttDataTransfer> listTask = jsonString.JsonToEntity<List<TaskGanttDataTransfer>>();

        if (listTask.Any())
        {
            byte[] byteArray = UowApplication.GetUowApplication<CronogramaGanttApplication>().GetByteArrayPdfStreamTask(idProjeto, listTask, typeof(Resources.traducao));
            context.Response.Clear();
            string pdfName = "gantt";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");
            context.Response.ContentType = "application/pdf";
            context.Response.Buffer = true;
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.BinaryWrite(byteArray);
            context.Response.End();
            context.Response.Close();
        }
    }

    public void GetJsonProject(HttpContext context, int idProjeto, short numLinhaBase, bool isCarregarHtmlCaminhoCritico)
    {
        var ganttDataset = UowApplication.GetUowApplication<CronogramaGanttApplication>().GetGanttDatasetDataTransfer(idProjeto, numLinhaBase, typeof(Resources.traducao), isCarregarHtmlCaminhoCritico);

        var recursos = new List<ResourceGanttDataTransfer>();

        dados cDados = CdadosUtil.GetCdados(null);
        object objCodigoEntidade = cDados.getInfoSistema("CodigoEntidade");
        if (objCodigoEntidade != null)
        {
            int codigoEntidade = int.Parse(objCodigoEntidade.ToString());
            DataSet dsRecursos = cDados.getRecursosCorporativosProjeto(idProjeto.ToString(), codigoEntidade);
            if (dsRecursos != null && dsRecursos.Tables.Count > 0)
            {
                recursos = dsRecursos.Tables[0].Rows.Cast<DataRow>()
                    .Select(r => new ResourceGanttDataTransfer
                    {
                        id = int.Parse(r["CodigoRecursoCorporativo"].ToString()),
                        name = r["NomeRecursoCorporativo"].ToString()
                    })
                    .ToList();
            }
        }
        ganttDataset.resources = new ResourcesGanttDataTransfer { rows = recursos };
        ganttDataset.assignments = new AssignmentsGanttDataTransfer { rows = new List<AssignmentGanttDataTransfer>() };

        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.Write(ganttDataset.ToJson());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}