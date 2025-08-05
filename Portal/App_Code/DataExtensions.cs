using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DataExtensions
/// </summary>
public static class DataExtensions
{
    public static void SetSessionInfo(Dictionary<string, object> data)
    {
        foreach (var item in data)
            HttpContext.Current.Session[item.Key] = item.Value;
    }

    public static void SetSelectParameters(this SqlDataSource sqlDataSource)
    {
        foreach (var item in GetSqlCommandParameters(sqlDataSource.SelectCommand))
        {
            Parameter parameter = new Parameter(item.Key);
            parameter.ConvertEmptyStringToNull = true;
            parameter.DefaultValue = item.Value;
            sqlDataSource.SelectParameters.Add(parameter);
        }
    }

    public static Dictionary<string, string> GetSqlCommandParameters(string cmdText)
    {
        var parameterDictionary = new Dictionary<string, string>();
        var pattern = @"@(?<param_name>pa_\w+)";
        var parametersNames = Regex.Matches(cmdText, pattern, RegexOptions.Multiline)
            .OfType<Match>().Select(m => m.Groups["param_name"].Value).Distinct();
        foreach (var pName in parametersNames)
            parameterDictionary.Add(pName, GetValueFromContext(pName));

        return parameterDictionary;
    }

    public static string GetValueFromContext(string nomeParametro)
    {
        HttpContext currentContext = HttpContext.Current;
        return currentContext.Request.QueryString[nomeParametro] ?? (currentContext.Session[nomeParametro] ?? string.Empty).ToString();
    }

    public static IEnumerable<string> GetColumnsNamesFromSelectCommand(string selectCommandText, string connectionString)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            using (var dataAdapter = new SqlDataAdapter(selectCommandText, connection))
            {
                var dataTable = new DataTable();
                foreach (var item in GetSqlCommandParameters(selectCommandText))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(item.Key, item.Value);
                dataAdapter.FillSchema(dataTable, SchemaType.Source);
                return dataTable.Columns.OfType<DataColumn>().Select(c => c.ColumnName);
            }
        }
    }
}