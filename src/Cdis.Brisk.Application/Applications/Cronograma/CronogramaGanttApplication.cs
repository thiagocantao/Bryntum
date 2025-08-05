using Cdis.Brisk.DataTransfer.Gantt;
using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Domain.Domains.DataBaseObject.Function.Param;
using Cdis.Brisk.Domain.Domains.Projeto;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using Cdis.Brisk.Service.Services.Projeto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdis.Brisk.Application.Applications.Cronograma
{
    /// <summary>
    /// Classe de aplicação CronogramaApplication
    /// </summary>
    public class CronogramaGanttApplication : IApplication
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CronogramaApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        private readonly string _strDateFormatPtBr = "dd/MM/yyyy";
        private readonly string _strDateFormatEn = "MM/dd/yyyy";
        private readonly string _strDateFormatProject = "yyyy'-'MM'-'dd";
        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CronogramaApplication
        /// </summary>
        public UnitOfWorkApplication UowApplication
        {
            get
            {
                return _unitOfWorkApplication;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de aplicação CronogramaApplication
        /// </summary>
        public CronogramaGanttApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        #region Public
        /// <summary>
        /// Buscar as informações para alimentar o gráfico de gantt do Bryntum
        /// </summary>        
        public GanttDatasetDataTransfer GetGanttDatasetDataTransfer(int codigoProjeto, short numLinhaBase, Type typeResourceTraducao, bool isCarregarHtmlCaminhoCritico)
        {
            ParamFGetCronogramaGanttProjetoDomain param = new ParamFGetCronogramaGanttProjetoDomain
            {
                CodigoProjeto = codigoProjeto,
                VersaoLinhaBase = numLinhaBase,
                CodigoRecurso = -1,
                SoAtrasadas = "N",
                SoMarcos = "N",
                PercentualConcluido = null,
                DataFiltro = null
            };
            var listTasks = UowApplication.UowService.GetUowService<FGetCronogramaGanttProjetoService>().GetListPrevisaoFluxoCaixa(param);

            DateTime dtStart = listTasks.Any() ? listTasks.Min(d => d.Inicio) : DateTime.Now;
            DateTime dtEnd = listTasks.Any() ? listTasks.Max(d => d.Inicio) : DateTime.Now;
            GanttDatasetDataTransfer ganttDataset = new GanttDatasetDataTransfer
            {
                success = true,
                project = MontaProjectDataTransfer("general", dtStart, dtEnd),
                tasks = MontarTasksGanttDataTransfer(listTasks, typeResourceTraducao, isCarregarHtmlCaminhoCritico),
                dependencies = ObterDependenciasTarefas(listTasks)
            };
            return ganttDataset;
        }

        private DependenciesGanttDataTransfer ObterDependenciasTarefas(List<FGetCronogramaGanttProjetoDomain> listTasks)
        {
            var dependencias = listTasks
                .Where(t => !string.IsNullOrWhiteSpace(t.Predecessoras))
                .SelectMany(t => t.Predecessoras.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    p =>
                    {
                        int sequenciaTarefa = ObtemSequenciaTarefaPredecessora(p);
                        int codigoTarefa = listTasks.Single(tarefa => tarefa.SequenciaTarefaCronograma == sequenciaTarefa).CodigoTarefa;
                        return new DependencyGanttDataTransfer
                        {
                            toTask = t.CodigoTarefa + 1,
                            fromTask = codigoTarefa + 1
                        };
                    }))
                .ToList();

            return new DependenciesGanttDataTransfer
            {
                rows = dependencias
            };
        }

        private static int ObtemSequenciaTarefaPredecessora(string p)
        {
            int sequenciaTarefa = int.Parse(string.Join(string.Empty, p.TakeWhile(c => Char.IsDigit(c))));
            return sequenciaTarefa;
        }

        /// <summary>
        /// Buscar o array de byte do pdf das tasks 
        /// </summary>        
        public byte[] GetByteArrayPdfStreamTask(int idProjeto, List<TaskGanttDataTransfer> listTask, Type typeResourceTraducao)
        {
            List<string> listHtmlPage = new List<string>();

            int numItensPorPagina = 30;
            int qtdPaginas = Math.Abs(listTask.Count / numItensPorPagina);
            string strTotalPage = (qtdPaginas + 1).ToString().PadLeft(2, '0');
            for (int page = 0; page <= qtdPaginas; page++)
            {
                string strNumPage = (page + 1).ToString().PadLeft(2, '0');
                var queryResultPage = listTask
                  .Skip(numItensPorPagina * page)
                  .Take(numItensPorPagina)
                  .ToList();
                listHtmlPage.Add(MountHtmlTaskGantt(idProjeto, queryResultPage, typeResourceTraducao, true, strNumPage, strTotalPage));
            }

            return Infra.Core.Pdf.PdfCore.GetStreamPageFromListHtmlPageSizeA2LandscapeMargin5(listHtmlPage).ToByteArray();
        }

        /// <summary>
        /// Montar a string que representa o html da lista de task do gráfico gantt
        /// </summary>        
        public string MountHtmlTaskGantt(int idProjeto, List<TaskGanttDataTransfer> listTask, Type typeResourceTraducao)
        {
            try
            {
                return MountHtmlTaskGantt(idProjeto, listTask, typeResourceTraducao, false, null, null);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Montar a string que representa o html da lista de task do gráfico gantt
        /// </summary>        
        public string MountHtmlTaskGantt(int idProjeto, List<TaskGanttDataTransfer> listTask, Type typeResourceTraducao, bool isIncludeNumerPage, string strNumPage, string strTotalPage)
        {
            try
            {
                string htmlPageData = MountDataHtmlTasks(listTask, typeResourceTraducao);
                return MountPageHtml(idProjeto, typeResourceTraducao, htmlPageData, isIncludeNumerPage, strNumPage, strTotalPage);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        #region Save
        public void SaveCronograma(GanttSaveDataTransfer ganttSave)
        {

        }
        #endregion

        #endregion

        #region Private
        /// <summary>
        /// Montar o objeto ProjectDataTransfer
        /// </summary>        
        private ProjectGanttDataTransfer MontaProjectDataTransfer(string nomeCalendar, DateTime dtStartDate, DateTime dtEndDate)
        {
            return new ProjectGanttDataTransfer
            {
                calendar = nomeCalendar,
                startDate = dtStartDate.ToString(_strDateFormatProject),
                endDate = dtEndDate.ToString(_strDateFormatProject)
            };
        }

        /// <summary>
        /// Montar a lista de dependências
        /// </summary>        
        private DependenciesGanttDataTransfer MontarDependenciesGanttDataTransfer(List<CronogramaDependenciaDomain> listDependencia)
        {
            return new DependenciesGanttDataTransfer
            {
                rows = MontarRowsDependenciesDataTransfer(listDependencia)
            };
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<DependencyGanttDataTransfer> MontarRowsDependenciesDataTransfer(List<CronogramaDependenciaDomain> listDependencia)
        {
            List<DependencyGanttDataTransfer> list = new List<DependencyGanttDataTransfer>();

            foreach (var dependenciaDomain in listDependencia)
            {
                DependencyGanttDataTransfer dep = new DependencyGanttDataTransfer
                {
                    fromTask = dependenciaDomain.FromTask,
                    toTask = dependenciaDomain.ToTask
                };
                list.Add(dep);
            }

            return list;
        }

        /// <summary>
        /// Montar a lista de tasks
        /// </summary>        
        private TasksGanttDataTransfer MontarTasksGanttDataTransfer(List<FGetCronogramaGanttProjetoDomain> listTasks, Type typeResourceTraducao, bool isCarregarHtmlCaminhoCritico)
        {
            return new TasksGanttDataTransfer
            {
                rows = MontarRowsTaskDataTransfer(listTasks, typeResourceTraducao, isCarregarHtmlCaminhoCritico)
            };
        }

        /// <summary>
        /// Montar a lista de TaskGanttDataTransfer
        /// </summary>        
        private List<TaskGanttDataTransfer> MontarRowsTaskDataTransfer(List<FGetCronogramaGanttProjetoDomain> listTasks, Type typeResourceTraducao,bool isCarregarHtmlCaminhoCritico)
        {
            List<TaskGanttDataTransfer> listTaskGantt = new List<TaskGanttDataTransfer>();

            foreach (var task in listTasks.Where(g => g.TarefaSuperior == null))
            {
                listTaskGantt.Add(MontarTaskGanttDataTransfer(task, listTasks, typeResourceTraducao, isCarregarHtmlCaminhoCritico));
            }

            return listTaskGantt;
        }

        /// <summary>
        /// Montar TaskGanttDataTransfer
        /// </summary>        
        private TaskGanttDataTransfer MontarTaskGanttDataTransfer(FGetCronogramaGanttProjetoDomain task, List<FGetCronogramaGanttProjetoDomain> listTasks, Type typeResourceTraducao, bool isCarregarHtmlCaminhoCritico)
        {
            var listResourceTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeResourceTraducao, new List<string> { "sim", "nao" });
            string langSim = listResourceTraducao.FirstOrDefault(t => t.Key == "sim").Text.ToUpper();
            string langNao = listResourceTraducao.FirstOrDefault(t => t.Key == "nao").Text.ToUpper();
            string strFormatDate = System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Contains("pt") ? _strDateFormatPtBr : _strDateFormatEn;

            string txCaminhoCritico = @"<span class='isCaminhoCritico'>{0}</span>";

            return new TaskGanttDataTransfer
            {
                id = task.CodigoTarefa + 1,
                edtcode = task.Edt,
                isCaminhoCriticoStr = (task.IndicaCritica == 1) ? langSim : langNao,
                name = task.NomeTarefa,
                nomeTarefa = (isCarregarHtmlCaminhoCritico && task.IndicaCritica == 1) ? string.Format(txCaminhoCritico, task.NomeTarefa) : task.NomeTarefa,
                inicioLb = task.InicioLB.HasValue ? task.InicioLB.Value.ToString(strFormatDate) : "",
                inicio = task.Inicio.ToString(strFormatDate),
                startDate = task.InicioLB.HasValue ? task.InicioLB.Value.ToString(_strDateFormatProject) : "",
                endDate = task.TerminoLB.HasValue ? task.TerminoLB.Value.ToString(_strDateFormatProject) : "",
                termino = task.Termino.ToString(strFormatDate),
                terminoLb = task.TerminoLB.HasValue ? task.TerminoLB.Value.ToString(strFormatDate) : "",
                terminoReal = task.TerminoReal.HasValue ? task.TerminoReal.Value.ToString(strFormatDate) : "",
                percentDone = task.Concluido.HasValue ? Math.Round(task.Concluido.Value) : 0,
                realizado = task.Concluido.HasValue ? Math.Round(task.Concluido.Value).ToString() + " %" : "0 %",
                duracao = (Convert.ToDateTime(task.Termino) - Convert.ToDateTime(task.Inicio)).Days,
                duracaoLb = task.DuracaoLB,
                peso = task.PercentualPesoTarefa.ToString() + "%",
                pesoLb = task.ValorPesoTarefaLB.ToString() + "%",
                previsto = task.PercentualPrevisto.HasValue ? Math.Round((decimal)task.PercentualPrevisto) : 0,
                trabalho = task.Trabalho.ToString(),
                isMarcoStr = task.IndicaMarco == 1 ? langSim : langNao,
                isAtrasoStr = (task.PercentualReal < task.PercentualPrevisto) ? langSim : langNao,
                recurso = task.StringAlocacaoRecursoTarefa,
                codTarefa = task.CodigoRealTarefa,
                custo = task.Custo,
                children = GetListTaskGanttDataTransfer(task, listTasks, typeResourceTraducao, isCarregarHtmlCaminhoCritico)
            };
        }

        /// <summary>
        /// Get ListTaskGanttDataTransfer
        /// </summary>        
        private List<TaskGanttDataTransfer> GetListTaskGanttDataTransfer(FGetCronogramaGanttProjetoDomain task, List<FGetCronogramaGanttProjetoDomain> listTasks, Type typeResourceTraducao, bool isCarregarHtmlCaminhoCritico)
        {
            if (listTasks.Any(t => t.TarefaSuperior == task.CodigoRealTarefa))
            {
                List<TaskGanttDataTransfer> listTaskGantt = new List<TaskGanttDataTransfer>();

                foreach (var taskItem in listTasks.Where(g => g.TarefaSuperior == task.CodigoRealTarefa))
                {
                    listTaskGantt.Add((MontarTaskGanttDataTransfer(taskItem, listTasks, typeResourceTraducao, isCarregarHtmlCaminhoCritico)));
                }

                return listTaskGantt;
            }
            else
            {
                return new List<TaskGanttDataTransfer>();
            }
        }

        /// <summary>
        /// Montar o html que corresponde a tabela dos dados.
        /// </summary>        
        private string MountDataHtmlTasks(List<TaskGanttDataTransfer> listTask, Type typeResourceTraducao)
        {
            StringBuilder sb = new StringBuilder();
            var listResourceTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(
                typeResourceTraducao,
                new List<string> { "tarefa", "inicio_lb", "termino_lb", "previsto", "realizado", "peso_lb", "peso", "duracao_d", "trabalho_h", "inicio", "termino", "termino_real", "marco", "atrasada", "caminho_critico" });

            string styleTh = "border: 1px solid #ddd;padding: 8px;padding-top: 12px;padding-bottom:12px;text-align:left; background-color: #4CAF50;color:white;";
            sb.Append("<table style='margin-top:0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;border-collapse: collapse;width:100%;'>");
            sb.Append("<thead>");
            sb.Append("<tr>");

            string tarefa = listResourceTraducao.FirstOrDefault(t => t.Key == "tarefa").Text.ToUpper();
            string inicioLb = listResourceTraducao.FirstOrDefault(t => t.Key == "inicio_lb").Text.ToUpper();
            string terminoLb = listResourceTraducao.FirstOrDefault(t => t.Key == "termino_lb").Text.ToUpper();
            string previsto = listResourceTraducao.FirstOrDefault(t => t.Key == "previsto").Text.ToUpper();
            string realizado = listResourceTraducao.FirstOrDefault(t => t.Key == "realizado").Text.ToUpper();
            string pesoLb = listResourceTraducao.FirstOrDefault(t => t.Key == "peso_lb").Text.ToUpper();
            string peso = "% " + listResourceTraducao.FirstOrDefault(t => t.Key == "peso").Text.ToUpper();
            string duracao = listResourceTraducao.FirstOrDefault(t => t.Key == "duracao_d").Text.ToUpper();
            string trabalho = listResourceTraducao.FirstOrDefault(t => t.Key == "trabalho_h").Text.ToUpper();
            string inicio = listResourceTraducao.FirstOrDefault(t => t.Key == "inicio").Text.ToUpper();
            string termino = listResourceTraducao.FirstOrDefault(t => t.Key == "termino").Text.ToUpper();
            string terminoReal = listResourceTraducao.FirstOrDefault(t => t.Key == "termino_real").Text.ToUpper();
            string marco = listResourceTraducao.FirstOrDefault(t => t.Key == "marco").Text.ToUpper() + " ?";
            string atrasada = listResourceTraducao.FirstOrDefault(t => t.Key == "atrasada").Text.ToUpper() + " ?";

            sb.Append("<th style='width:30%;" + styleTh + "'>" + tarefa + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + inicioLb + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + terminoLb + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + previsto + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + realizado + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + pesoLb + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + peso + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + duracao + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + trabalho + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + inicio + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + termino + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + terminoReal + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + marco + "</th>");
            sb.Append("<th style='" + styleTh + "'>" + atrasada + "</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            string bkColorRow = "#f2f2f2";

            foreach (var task in listTask)
            {
                bool isChildren = listTask.Any(t => t.edtcode != task.edtcode && t.edtcode.StartsWith(task.edtcode));
                string styleBold = isChildren ? "font-weight: bold;" : "";
                sb.Append("<tr style='" + styleBold + " background-color:" + bkColorRow + ";'>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;color:#494a4c;" + styleBold + "'>" + task.edtcode + " " + task.name.Replace("<h3 style='color:#ca2a2a;'>", "").Replace("</h3>", "") + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.inicioLb + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.terminoLb + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.previstoStr + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.realizado + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.pesoLb + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.peso + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.duracaoStr + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.trabalho + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.inicio + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.termino + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.terminoReal + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.isMarcoStr + "</td>");
                sb.Append("<td style='border: 1px solid #ddd;padding: 8px;'>" + task.isAtrasoStr + "</td>");
                sb.Append("</tr>");
                bkColorRow = bkColorRow.Equals("#f2f2f2") ? "#ddd" : "#f2f2f2";
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Montar a página unitária do relatório
        /// </summary>        
        private string MountPageHtml(int idProjeto, Type typeResourceTraducao, string htmlPageData, bool isIncludeNumerPage, string strNumPage, string strTotalPage)
        {
            StringBuilder sb = new StringBuilder();
            ProjetoUnidadeDomain projetoUnidade = UowApplication.UowService.GetUowService<ProjetoUnidadeService>().GetProjetoUnidade(idProjeto);
            sb.Append("<html><body  style='overflow: auto; height: 100 %; '>");
            sb.Append("<hr /><div style='height:98vh; overflow-y:scroll;margin-top: 10px;position: fixed; top: 0mm; width: 100%; border-bottom: 1px solid #f6f6ee; text-align: right;'>");
            sb.Append("<table style='width: 100%;'>");

            sb.Append("<tr>");
            sb.Append("<td style='text-align: left;' width='40%'>");
            sb.Append(GetLogoBriskImgBase64(projetoUnidade.LogoEntidade));
            sb.Append("</td>");
            sb.Append("<td width='60%'>");
            sb.Append(MountInfoTitleBriskGantt(projetoUnidade.NomeProjeto, projetoUnidade.NomeUnidadeNegocio, typeResourceTraducao, isIncludeNumerPage, strNumPage, strTotalPage));
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style='text-align: left;' colspan='2'>");
            sb.Append(htmlPageData);
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</div><hr />");
            sb.Append("</body></html>");
            return sb.ToString();
        }

        /// <summary>
        /// Montar o html que corresponde as informações do cabeçalho do relatório
        /// </summary>       
        /// <returns></returns>
        private string MountInfoTitleBriskGantt(string titulo, string nomeUnidade, Type typeResourceTraducao, bool isIncludeNumerPage, string strNumPage, string strTotalPage)
        {
            DateTime dtAtual = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            var listResourceTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeResourceTraducao, new List<string> { "projeto", "unidade", "pagina", "data_e_hora_de_geracao" });
            string traducaoProjeto = listResourceTraducao.FirstOrDefault(t => t.Key == "projeto").Text;
            string traducaoUnidade = listResourceTraducao.FirstOrDefault(t => t.Key == "unidade").Text;
            sb.Append("<h2 style='text-align:left;font-family: Arial, sans-serif;color: #524f4f; font-size: 30px'>" + traducaoProjeto + " : " + titulo + "</h2>");
            sb.Append("<h3 style='text-align:left;font-family: Trebuchet MS, sans-serif;color:#808080;'>" + traducaoUnidade + " : " + nomeUnidade + "</h3>");

            string langPage = listResourceTraducao.FirstOrDefault(t => t.Key == "pagina").Text;

            //No IE não é necessário gerar a numeração
            if (isIncludeNumerPage)
            {
                sb.Append("<span style='text-align:left;color: #524f4f; font-size: 15px;margin-bottom:0px;font-weight: bold;'> <em>" + langPage + " " + strNumPage + "  /  " + strTotalPage + "</em></h4>");
            }

            string tituloInfo = listResourceTraducao.FirstOrDefault(t => t.Key == "data_e_hora_de_geracao").Text;
            string infoGeracao = System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Contains("pt")
                        ? tituloInfo + " : " + dtAtual.ToString(this._strDateFormatPtBr + " HH:mm:ss")
                        : tituloInfo + " : " + dtAtual.ToString(this._strDateFormatEn + " hh:mm:ss tt");

            sb.Append("<span style='color:#808080; font-size: 15px;'> - <em>" + infoGeracao + "</em></span>");


            return sb.ToString();
        }

        /// <summary>
        /// Get Logo Brisk Img Base64
        /// </summary>
        /// <returns></returns>
        private string GetLogoBriskImgBase64(byte[] arrayByteImg)
        {
            string imgBase64 = arrayByteImg.GetImgBase64(ArrayByteExtensions.TypeImgArrayByteCore.PNG);
            return @"<img style='margin-top: 10px;position: fixed;display: block;' width='230px' src='" + imgBase64 + "'/></br>";
        }
        #endregion

        #endregion
    }
}
