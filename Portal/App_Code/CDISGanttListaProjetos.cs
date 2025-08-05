using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web.Hosting;

/// <summary>
/// Summary description for CDISGanttListaProjetos
/// </summary>
public class CDISGanttListaProjetos
{
    private string codigoUsuario = "";
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

    public CDISGanttListaProjetos()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public CDISGanttListaProjetos(string codigoUsuario)
    {
        this.codigoUsuario = codigoUsuario;
    }

    public CDISGanttListaProjetos(OrderedDictionary listaParametros)
    {
    }


    public string buildGrantData(int codigoProjeto, string codigoRecurso, int versaoLB, bool fazInner, string where)
    {
        return "";
    }

    public string geraGraficoJsonTaskData(DataTable dsCrono, int codigoUsuario)
    {
        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
        pathToBryntumFile = @"GanttBryNTum_" + codigoUsuario + "_" + dataHora + ".json";
        List<BryntumTask> taskStore = new List<BryntumTask>();

        foreach (DataRow dr in dsCrono.Rows)
        {
            if (!tarefasAdicionadas.Contains(";" + dr["Codigo"] + ";") || index == 0)
            {
                if (index == 0)
                {
                    DataInicio = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, -2)", getMinDate(dsCrono));
                    DataTermino = string.Format("Sch.util.Date.add(new Date({0:yyyy, M, d}), Sch.util.Date.MONTH, 2)", getMaxDate(dsCrono));
                }

                taskStore.Add(geraBrytumTaskData(generateBuilderFromRow(dr), dsCrono));
            }
        }

        escreveJSON(JsonConvert.SerializeObject(taskStore, Newtonsoft.Json.Formatting.Indented), @"/ArquivosTemporarios/" + pathToBryntumFile);
        return JsonConvert.SerializeObject(taskStore, Newtonsoft.Json.Formatting.Indented);
    }

    public class TaskBuilder
    {
        public string Id;
        public string Name;
        public int PercentDone;
        public string StartDate;
        public string EndDate;
        public bool Leaf;
        public bool Expanded;
        public string taskStatusClass;
        public string PDone;
    }

    private TaskBuilder generateBuilderFromRow(DataRow dr)
    {
        TaskBuilder childTask = new TaskBuilder();
        childTask.Id = dr["Codigo"].ToString();
        childTask.Name = dr["Descricao"].ToString().Replace("\"", "'").Replace("<", "&lt;").Replace(">", "&gt;").Replace("–", "-");
        var rawPercent = dr.IsNull("Concluido") ? 0 : (int)dr.Field<decimal>("Concluido"); //int.Parse((dr["Concluido"] + "" == "" ? "0" : dr["Concluido"].ToString()).Replace("%", "").Split(',')[0]);//.Replace(',', '.');
                                                                                           //var numericPercent = double.Parse(rawPercent);
        childTask.PercentDone = rawPercent;
        childTask.PDone = dr["Concluido"] + "" == "" ? "0" : dr["Concluido"].ToString();
        childTask.StartDate = dr["Inicio"] + "" == "" ? "" : string.Format("{0:yyyy-MM-dd}", dr["Inicio"]) + "T00:00:00";
        childTask.EndDate = dr["Termino"] + "" == "" ? "" : string.Format("{0:yyyy-MM-dd}", dr["Termino"]) + "T00:00:00";
        string sumariaIn = dr["Sumaria"].ToString();
        childTask.Leaf = sumariaIn != "1";
        childTask.Expanded = sumariaIn == "1";
        childTask.taskStatusClass = dr["Status"].ToString().ToLower().Trim();
        return childTask;
    }

    private DateTime getMinDate(DataTable dt)
    {
        //DataColumn col = dt.Columns["Inicio"]; // Call this the one you have
        //int maxValue = (int)dt.Compute("MAX([Inicio])", "");
        //DataRow[] tbl = dt.Select("Inicio IS NOT NULL");

        //var first = tbl.AsEnumerable()
        //               .Select(cols => cols.Field<DateTime>(col.ColumnName))
        //               .OrderBy(p => p.Ticks)
        //               .FirstOrDefault();
        try
        {
            return ((DateTime)dt.Compute("MIN([Inicio])", ""));
        }
        catch
        {
            return new DateTime();
        }
    }

    private DateTime getMaxDate(DataTable dt)
    {
        //DataColumn col = dt.Columns["Termino"]; // Call this the one you have

        //DataRow[] tbl = dt.Select("Inicio IS NOT NULL");

        //var last = tbl.AsEnumerable()
        //              .Select(cols => cols.Field<DateTime>(col.ColumnName))
        //              .OrderByDescending(p => p.Ticks)
        //              .FirstOrDefault();

        //return ((DateTime)last);
        try
        {
            return ((DateTime)dt.Compute("MAX([Termino])", ""));
        }
        catch
        {
            return new DateTime();
        }
    }

    private string getFormatDate()
    {
        return "";//string.Format("{0:yyyy-MM-dd}", dr["Termino"]) + "T00:00:00";
    }

    /// Gera os dados das tarefas 
    public BryntumTask geraBrytumTaskData(TaskBuilder builder, DataTable dsCrono)
    {

        tarefasAdicionadas += builder.Id + ";";
        index++;

        BryntumTask mTask = new BryntumTask();
        mTask.Id = builder.Id;
        mTask.Name = builder.Name;
        mTask.PercentDone = builder.PercentDone;
        mTask.StartDate = builder.StartDate;
        mTask.EndDate = builder.EndDate;
        mTask.expanded = builder.Expanded;
        mTask.leaf = builder.Leaf;
        mTask.TaskStatusClass = builder.taskStatusClass;
        mTask.PDone = builder.PDone;

        if (mTask.expanded)
        {
            int numero = 0;
            bool result = Int32.TryParse(builder.Id, out numero);
            string where = "";

            if (result)
                where = "CodigoSuperior = " + numero;
            else
                where = "CodigoSuperior = '" + builder.Id + "'";

            foreach (DataRow dr in dsCrono.Select(where))
            {
                TaskBuilder childTask = generateBuilderFromRow(dr);
                mTask.addChilTask(geraBrytumTaskData(childTask, dsCrono));
            }
        }
        return mTask;
    }

    public string retornaJSONDependencias(DataSet ds)
    {
        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
        pathToDependencyFile = @"/ArquivosTemporarios/GanttBryNTumDependencias_" + codigoUsuario + "_" + dataHora + ".json";
        List<BryntumDependency> dependencies = new List<BryntumDependency>();
        string tipoConector = "";

        if (ds != null && ds.Tables[0].Rows.Count > 0)
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
                BryntumDependency dependency = new BryntumDependency(dr["From"].ToString(), dr["To"].ToString(), tipoConector);
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
