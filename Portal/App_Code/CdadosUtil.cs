using System.Collections.Specialized;

/// <summary>
/// Summary description for CdadosUtil
/// </summary>
public static class CdadosUtil
{
    /// <summary>
    /// Buscar a classe dados
    /// </summary>
    public static dados GetCdados(OrderedDictionary parametrosDados)
    {
        return GetCdados(-1, parametrosDados);
    }

    /// <summary>
    /// Buscar a classe dados
    /// </summary>
    public static dados GetCdados(int codEntidade, OrderedDictionary parametrosDados)
    {
        return new dados(codEntidade, parametrosDados);
    }
}