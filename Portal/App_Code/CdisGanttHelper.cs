using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web.Hosting;

namespace Cdis.gantt
{
    /// <summary>
    /// Summary description for CdisGrantHelper
    /// </summary>
    public class CdisGanttHelper
    {

        private dados cDados;
        private DataSet dsCrono;
        private string tarefasAdicionadas = ";";
        private int index = 0;
        private string dataInicio = "";
        private string dataTermino = "";
        private string pathToBryntumFile = "";
        private string pathToDependencyFile = "";
        private bool modoCalculoAtrasoTotal = false;

        public string DataInicio
        {
            get
            {
                return dataInicio;
            }

            set
            {
                dataInicio = value;
            }
        }

        public string DataTermino
        {
            get
            {
                return dataTermino;
            }

            set
            {
                dataTermino = value;
            }
        }

        public string PathToBryntumFile
        {
            get
            {
                return pathToBryntumFile;
            }

            set
            {
                pathToBryntumFile = value;
            }
        }

        public string PathToDependencyFile
        {
            get
            {
                return pathToDependencyFile;
            }

            set
            {
                pathToDependencyFile = value;
            }
        }

        public CdisGanttHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public CdisGanttHelper(dados cDados)
        {
            this.cDados = cDados;
        }

        public CdisGanttHelper(OrderedDictionary listaParametros)
        {
        }


        public string buildGrantData(int codigoProjeto, string codigoRecurso, int versaoLB, bool fazInner, string where)
        {
            return "";
        }

        public string geraGraficoJsonTaskData(int codigoProjeto, string codigoRecurso, int versaoLB, bool fazInner, bool somenteAtrasadas, bool somenteMarcos, int? percentualConcluido, DateTime? dataFiltro)
        {
            modoCalculoAtrasoTotal = cDados.getModoCalculoAtrasoTotal();
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
            pathToBryntumFile = @"/ArquivosTemporarios/GanttBryNTum_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora + ".json";
            List<BryntumTask> taskStore = new List<BryntumTask>();
            dsCrono = cDados.getCronogramaGantt(codigoProjeto, codigoRecurso, versaoLB, fazInner, somenteAtrasadas, somenteMarcos, percentualConcluido, dataFiltro);

            foreach (DataRow dr in dsCrono.Tables[0].Rows)
            {
                if (!tarefasAdicionadas.Contains(";" + dr["CodigoRealTarefa"] + ";") || index == 0)
                {
                    if (index == 0)
                    {
                        DataInicio = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, -2)", dr["Inicio"]);
                        DataTermino = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, 2)", dr["Termino"]);
                    }
                    taskStore.Add(geraBrytumTaskData(generateBuilderFromRow(dr)));
                }
            }

            escreveJSON(JsonConvert.SerializeObject(taskStore, Newtonsoft.Json.Formatting.Indented), pathToBryntumFile);
            return JsonConvert.SerializeObject(taskStore, Newtonsoft.Json.Formatting.Indented);
        }

        public class TaskBuilder
        {
            public string Id;
            public string Name;
            public string BaselineStartDate;
            public string BaselineEndDate;
            public string PercentualPrevisto;
            public int PercentDone;
            public string ValorPesoTarefaLB; //Peso LB
            public string PercentualPesoTarefa; //% Peso
            public string Duration;
            public string Trabalho;
            public string StartDate;
            public string EndDate;
            public string TerminoReal;
            public bool Leaf;
            public bool Expanded;
            public string CodigoRealTarefa;
            public string taskStatusClass;
            public string PDone;
            public string Duracao;
        }

        private TaskBuilder generateBuilderFromRow(DataRow dr)
        {
            TaskBuilder childTask = new TaskBuilder();
            childTask.Id = dr["CodigoRealTarefa"].ToString();
            childTask.Name = dr["NomeTarefa"].ToString().Replace("\"", "'").Replace("<", "&lt;").Replace(">", "&gt;").Replace("–", "-");
            childTask.BaselineStartDate = dr["InicioLB"] + "" == "" ? "-" : string.Format("{0:yyyy-MM-dd}", dr["InicioLB"]) + "T00:00:00";
            childTask.BaselineEndDate = dr["TerminoLB"] + "" == "" ? "-" : string.Format("{0:yyyy-MM-dd}", dr["TerminoLB"]) + "T00:00:00";
            childTask.PercentualPrevisto = dr["PercentualPrevisto"] + "" == "" ? "-" : string.Format("{0:n0}%", dr["PercentualPrevisto"]);
            //var rawPercent = int.Parse((dr["Concluido"] + "" == "" ? "0" : dr["Concluido"].ToString()).Replace("%", "").Split(',')[0]);//.Replace(',', '.');
            //var numericPercent = rawPercent;
            //childTask.PercentDone = rawPercent;
            childTask.PDone = dr["Concluido"] + "" == "" ? "-" : string.Format("{0:n0}%", dr["Concluido"]);
            childTask.ValorPesoTarefaLB = dr["ValorPesoTarefaLB"] + "" == "" ? "-" : string.Format("{0:n2}", decimal.Parse(dr["ValorPesoTarefaLB"].ToString()));
            childTask.PercentualPesoTarefa = dr["PercentualPesoTarefa"] + "" == "" ? "-" : string.Format("{0:n2}%", decimal.Parse(dr["PercentualPesoTarefa"].ToString()));

            string dur = dr["Duracao"] + "" == "" ? "0.0" : string.Format("{0:n2}", dr["Duracao"]);
            string[] dur1 = dur.Split(',');
            childTask.Duration = dur1[0];
            childTask.Trabalho = dr["Trabalho"] + "" == "" ? "0" : dr["Trabalho"].ToString();
            childTask.StartDate = dr["Inicio"] + "" == "" ? "" : string.Format("{0:yyyy-MM-dd}", dr["Inicio"]) + "T00:00:00";
            childTask.EndDate = dr["Termino"] + "" == "" ? "" : string.Format("{0:yyyy-MM-dd}", dr["Termino"]) + "T00:00:00";
            childTask.CodigoRealTarefa = dr["CodigoRealTarefa"].ToString();
            childTask.TerminoReal = dr["TerminoReal"] + "" == "" ? "-" : string.Format("{0:yyyy-MM-dd}", dr["TerminoReal"]) + "T00:00:00";
            childTask.Duracao = dur1[0];
            string sumariaIn = dr["IndicaTarefaSumario"].ToString();
            childTask.Leaf = sumariaIn != "1";
            childTask.Expanded = sumariaIn == "1";
            childTask.taskStatusClass = cDados.getClaseTarefa(dr, modoCalculoAtrasoTotal);
            return childTask;
        }

        private string getFormatDate()
        {
            return "";//string.Format("{0:yyyy-MM-dd}", dr["Termino"]) + "T00:00:00";
        }

        /// Gera os dados das tarefas 
        public BryntumTask geraBrytumTaskData(TaskBuilder builder)
        {

            tarefasAdicionadas += builder.CodigoRealTarefa + ";";
            index++;

            BryntumTask mTask = new BryntumTask();
            mTask.Id = builder.Id;
            mTask.Name = builder.Name;
            mTask.BaselineStartDate = builder.BaselineStartDate;
            mTask.BaselineEndDate = builder.BaselineEndDate;
            mTask.PercentualPrevisto = builder.PercentualPrevisto;
            mTask.PercentDone = builder.PercentDone;
            mTask.ValorPesoTarefaLB = builder.ValorPesoTarefaLB;
            mTask.PercentualPesoTarefa = builder.PercentualPesoTarefa;
            mTask.Duration = builder.Duration;
            mTask.Trabalho = builder.Trabalho;
            mTask.StartDate = builder.StartDate;
            mTask.EndDate = builder.EndDate;
            mTask.expanded = builder.Expanded;
            mTask.leaf = builder.Leaf;
            mTask.TaskStatusClass = builder.taskStatusClass;
            mTask.TerminoReal = builder.TerminoReal;
            mTask.PDone = builder.PDone;
            mTask.Duracao = builder.Duracao;
            mTask.CodigoRealTarefa = builder.CodigoRealTarefa;

            if (mTask.expanded)
            {
                foreach (DataRow dr in dsCrono.Tables[0].Select("TarefaSuperior = '" + builder.CodigoRealTarefa + "'"))
                {
                    TaskBuilder childTask = generateBuilderFromRow(dr);
                    mTask.addChilTask(geraBrytumTaskData(childTask));
                }
            }
            return mTask;
        }

        public string retornaJSONDependencias(int codigoProjeto)
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
            pathToDependencyFile = @"/ArquivosTemporarios/GanttBryNTumDependencias_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora + ".json";
            List<BryntumDependency> dependencies = new List<BryntumDependency>();
            string tipoConector = "";
            DataSet ds = cDados.getDataSet(string.Format(@"
                SELECT tcpp.codigoTarefa AS TarefaTo, tcpp.codigoTarefaPredecessora AS TarefaFrom, tipoLatencia
                  FROM {0}.{1}.[TarefaCronogramaProjetoPredecessoras] tcpp INNER JOIN
			           {0}.{1}.CronogramaProjeto cp ON (cp.CodigoCronogramaProjeto = tcpp.CodigoCronogramaProjeto 
													AND cp.CodigoProjeto = {2})", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto));

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tipoConector = dr["tipoLatencia"].ToString();
                    if (tipoConector == "" || tipoConector == "TI")
                        tipoConector = "2";
                    else if (tipoConector == "II")
                        tipoConector = "0";
                    else if (tipoConector == "TT")
                        tipoConector = "3";
                    else if (tipoConector == "IT")
                        tipoConector = "1";
                    BryntumDependency dependency = new BryntumDependency(dr["TarefaFrom"].ToString(), dr["TarefaTo"].ToString(), tipoConector);
                    dependencies.Add(dependency);
                }
            }
            escreveJSON(JsonConvert.SerializeObject(dependencies, Newtonsoft.Json.Formatting.Indented), pathToDependencyFile);
            return JsonConvert.SerializeObject(dependencies, Newtonsoft.Json.Formatting.Indented);
        }


        private string escreveJSON(string jsonString, string nome)
        {
            StreamWriter strWriter;

            //cria um novo arquivo json e abre para escrita
            strWriter = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + nome, false, System.Text.Encoding.UTF8);

            //escreve o corpo do JSON no arquivo json criado
            strWriter.Write(jsonString);
            //fecha o arquivo criado
            strWriter.Close();

            return nome;
        }

    }

    /*
        Classe que descreve as propriedades de uma tarefa definida en Brytum      
     */
    public class BryntumTask
    {
        private string id;
        private string name;
        private string baselineStartDate;
        private string baselineEndDate;
        private string percentualPrevisto;
        private int percentDone;
        private string valorPesoTarefaLB; //Peso LB
        private string percentualPesoTarefa; //% Peso
        private string duration;
        private string trabalho;
        private string startDate;
        private string endDate;
        private string terminoReal;
        private bool Leaf;
        private bool Expanded;
        private List<BryntumTask> Children;
        private string taskStatusClass;
        private string pDone;
        private string duracao;

        private string actualCost;
        private string actualEffort;
        private string baselineCost;
        private string baselinePersentDone;
        private string calendarId;
        private string cls;
        private string constraintDate;
        private string constraintType;
        private string cost;
        private string costVariance;
        private string deadlineDate;
        private string durationUnit;
        private string note;
        private string codigoRealTarefa;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string BaselineEndDate
        {
            get
            {
                return baselineEndDate;
            }

            set
            {
                baselineEndDate = value;
            }
        }

        public string BaselineStartDate
        {
            get
            {
                return baselineStartDate;
            }

            set
            {
                baselineStartDate = value;
            }
        }

        public string DeadlineDate
        {
            get
            {
                return deadlineDate;
            }

            set
            {
                deadlineDate = value;
            }
        }

        public string Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public string DurationUnit
        {
            get
            {
                return durationUnit;
            }

            set
            {
                durationUnit = value;
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public int PercentDone
        {
            get
            {
                return percentDone;
            }

            set
            {
                percentDone = value;
            }
        }

        public string StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public List<BryntumTask> children
        {
            get
            {
                return Children;
            }

            set
            {
                Children = value;
            }
        }

        public bool leaf
        {
            get
            {
                return Leaf;
            }

            set
            {
                Leaf = value;
            }
        }

        public bool expanded
        {
            get
            {
                return Expanded;
            }

            set
            {
                Expanded = value;
            }
        }

        public string PercentualPrevisto
        {
            get
            {
                return percentualPrevisto;
            }

            set
            {
                percentualPrevisto = value;
            }
        }

        public string ValorPesoTarefaLB
        {
            get
            {
                return valorPesoTarefaLB;
            }

            set
            {
                valorPesoTarefaLB = value;
            }
        }

        public string PercentualPesoTarefa
        {
            get
            {
                return percentualPesoTarefa;
            }

            set
            {
                percentualPesoTarefa = value;
            }
        }

        public string Trabalho
        {
            get
            {
                return trabalho;
            }

            set
            {
                trabalho = value;
            }
        }

        public string TerminoReal
        {
            get
            {
                return terminoReal;
            }

            set
            {
                terminoReal = value;
            }
        }

        public string TaskStatusClass
        {
            get
            {
                return taskStatusClass;
            }

            set
            {
                taskStatusClass = value;
            }
        }

        public string PDone
        {
            get
            {
                return pDone;
            }

            set
            {
                pDone = value;
            }
        }

        public string Duracao
        {
            get
            {
                return duracao;
            }

            set
            {
                duracao = value;
            }
        }

        public string CodigoRealTarefa
        {
            get
            {
                return codigoRealTarefa;
            }

            set
            {
                codigoRealTarefa = value;
            }
        }

        public void addChilTask(BryntumTask bt)
        {
            if (this.Children == null)
            {
                this.Children = new List<BryntumTask>();
            }
            this.Children.Add(bt);
        }
    }

    public class BryntumDependency
    {
        private string from;
        private string to;
        private string type;

        public BryntumDependency() { }
        public BryntumDependency(string from, string to, string type)
        {
            this.from = from;
            this.to = to;
            this.type = type;
        }

        public string From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        public string To
        {
            get
            {
                return to;
            }

            set
            {
                to = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }

    public class BryntumGantData
    {
        private List<BryntumTask> tasks;
        private List<BryntumDependency> dependencies;

        public BryntumGantData()
        {
        }

        public BryntumGantData(List<BryntumTask> tasks, List<BryntumDependency> dependencies)
        {
            this.Tasks = tasks;
            this.Dependencies = dependencies;
        }

        public List<BryntumTask> Tasks
        {
            get
            {
                return tasks;
            }

            set
            {
                tasks = value;
            }
        }

        public List<BryntumDependency> Dependencies
        {
            get
            {
                return dependencies;
            }

            set
            {
                dependencies = value;
            }
        }



    }

}
