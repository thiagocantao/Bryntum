<%@ WebHandler Language="C#" Class="GanttPlanoAcaoHandler" %>

using System.Web;
using System.Text;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Application;
using Cdis.Brisk.Application.Applications.Cronograma;


public class GanttPlanoAcaoHandler : IHttpHandler
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
        string iniciaisObjeto = context.Request.Headers["iniciaisObjeto"] == null ? "" : context.Request.Headers["iniciaisObjeto"].ToString();
        int idObjeto = context.Request.Headers["idObjeto"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idObjeto"].ToString());
        int idEntidade = context.Request.Headers["idEntidade"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idEntidade"].ToString());
        int idPlanoAcao = context.Request.Headers["idPlanoAcao"] == null ? 0 : System.Convert.ToInt32(context.Request.Headers["idPlanoAcao"].ToString());
        bool isIniciativas = context.Request.Headers["isIniciativas"] == null ? false : System.Convert.ToBoolean(context.Request.Headers["isIniciativas"].ToString());

        GetJsonProject(context,iniciaisObjeto, idObjeto, idEntidade, idPlanoAcao, isIniciativas);
    }


    public void GetJsonProject(HttpContext context,string iniciaisObjeto, int idObjeto, int idEntidade,int idPlanoAcao, bool isIniciativas)
    {
        var ganttDataset = idPlanoAcao == 0
                ? UowApplication.GetUowApplication<CronogramaGanttPlanoAcaoApplication>().GetGanttDatasetDataTransfer(iniciaisObjeto, idObjeto, idEntidade, isIniciativas)
                : UowApplication.GetUowApplication<CronogramaGanttPlanoAcaoApplication>().GetGanttPlanoAcaoDatasetDataTransfer(idPlanoAcao, iniciaisObjeto, idObjeto, idEntidade, isIniciativas);

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