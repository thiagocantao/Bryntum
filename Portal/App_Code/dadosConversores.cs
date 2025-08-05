using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

/// <summary>
/// Metodos conversores de dados e/ou valores
/// </summary>
public static class dadosConversores
{
    static private string[] arrClassesMonetariasSingular = { "", "mil", "milhão", "bilhão", "trilhão" };
    static private string[] arrClassesMonetariasPlural = { "", "mil", "milhões", "bilhões", "trilhões" };
    static private string[] arrNomesUnidades = { "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove" };
    static private string[] arrNomesDezenas = { "dez", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };
    static private string[] arrNomesCentenas = { "cem", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos" };
    static private string[] arrNomesSegundaDezena = { "dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove" };

    /// <summary>
    /// Transforma as linhas da tabela em um dicitonary
    /// </summary>
    /// <param name="tabela">tabela a ser convertida</param>
    /// <returns></returns>
	private static List<Dictionary<string, object>> linhasTabelaParaDicionario(DataTable tabela)
    {
        List<Dictionary<string, object>> objs = new List<Dictionary<string, object>>();
        foreach (DataRow dr in tabela.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i < tabela.Columns.Count; i++)
            {
                drow.Add(tabela.Columns[i].ColumnName, dr[i]);
            }
            objs.Add(drow);
        }

        return objs;
    }
    /// <summary>
    /// Tranforma um dataset em dicionary
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Dictionary<string, object> datasetParaDectionary(DataSet data)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        foreach (DataTable table in data.Tables)
        {
            d.Add(table.TableName, linhasTabelaParaDicionario(table));
        }
        return d;
    }
    /// <summary>
    /// Transforma uma tabela em dicionary
    /// </summary>
    /// <param name="tabela">tabela a ser convertida</param>
    /// <returns></returns>
    public static Dictionary<string, object> tabelaParaDictionary(DataTable tabela)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add(tabela.TableName, linhasTabelaParaDicionario(tabela));
        return d;
    }
    /// <summary>
    /// Retorna o nome por extenso do mes informado
    /// </summary>
    /// <param name="mes">numero do mês</param>
    /// <returns></returns>
    public static string mesPorExtenso(string mes)
    {
        string retorno = "Mês inválido";
        if (mes.Length < 2)
            mes = "0" + mes;
        switch (mes)
        {
            case "01":
                retorno = "Janeiro";
                break;
            case "02":
                retorno = "Fevereiro";
                break;
            case "03":
                retorno = "Março";
                break;
            case "04":
                retorno = "Abril";
                break;
            case "05":
                retorno = "Maio";
                break;
            case "06":
                retorno = "Junho";
                break;
            case "07":
                retorno = "Julho";
                break;
            case "08":
                retorno = "Agosto";
                break;
            case "09":
                retorno = "Setembro";
                break;
            case "10":
                retorno = "Outubro";
                break;
            case "11":
                retorno = "Novembro";
                break;
            case "12":
                retorno = "Dezembro";
                break;
        }
        return retorno;
    }
    static public string GetValorPorExtenso(double valor)
    {
        if (valor <= 0)
        {
            return string.Empty;
        }
        string[] arrAuxClassesMontearias;
        StringBuilder sbExtenso = new StringBuilder();
        double proximoValorPrc, log10;
        int intLog10, qtdClasseMonet, numeroIndicUDC, qtdUDC, valor0a100, qtdCentavos = 0, qtdClassesComValor;
        bool bUsarPluralClasseMonet = false, bCentavos = false, bMudouMilhar;
        bool bContinua = true;


        proximoValorPrc = Math.Round(valor, 2);
        if (proximoValorPrc < 1)
        {
            proximoValorPrc *= 100;
            bCentavos = true;
            qtdCentavos = (int)proximoValorPrc;
        }

        while (bContinua)
        {
            log10 = Math.Log10(proximoValorPrc);
            intLog10 = (int)log10;
            // se o número for acima de trilhões
            if (intLog10 > 14)
            {
                break;
            }
            qtdClasseMonet = (int)(log10 / 3);

            // verifica se unidade, dezena ou centena
            Math.DivRem(intLog10, 3, out numeroIndicUDC);
            numeroIndicUDC++;

            qtdUDC = (int)(proximoValorPrc / Math.Pow(10, intLog10));
            bMudouMilhar = false;

            if ((qtdClasseMonet > 1) && ((numeroIndicUDC > 1) || (qtdUDC > 1)))
            {
                bUsarPluralClasseMonet = true;
            }

            // se estamos falando de 1 dezena, usamos 
            if ((numeroIndicUDC == 2) && (qtdUDC == 1))
            {
                valor0a100 = (int)(proximoValorPrc / Math.Pow(10, intLog10 - 1));
                sbExtenso.Append(arrNomesSegundaDezena[valor0a100 - 10]);

                // como os números da segunda dezena são escrito juntos, já diminuímos o próximo número evitando que seja escrito
                // A exceção a essa regra é quando estamos diante do número 10, pois não há próximo número a ser escrito nesta dezena
                if (valor0a100 != 10)
                {
                    proximoValorPrc = Math.Round(proximoValorPrc - Math.Pow(10, intLog10) * qtdUDC, 2);
                    log10 = Math.Log10(proximoValorPrc);
                    intLog10 = (int)log10;
                    Math.DivRem(intLog10, 3, out numeroIndicUDC);
                    numeroIndicUDC++;
                    qtdUDC = (int)(proximoValorPrc / Math.Pow(10, intLog10));
                }
            } // if( (numeroIndicUDC == 2 ) && (qtdUDC == 1) )
            else
            {
                switch (numeroIndicUDC)
                {
                    case 1:
                        sbExtenso.Append(arrNomesUnidades[qtdUDC - 1]);
                        break;
                    case 2:
                        sbExtenso.Append(arrNomesDezenas[qtdUDC - 1]);
                        break;
                    default:

                        if (qtdUDC == 1)
                        {
                            valor0a100 = (int)(proximoValorPrc / Math.Pow(10, intLog10 - 2)) - 100;
                            if (valor0a100 > 0)
                                sbExtenso.Append("cento");
                            else
                                sbExtenso.Append(arrNomesCentenas[0]);
                        }
                        else
                            sbExtenso.Append(arrNomesCentenas[qtdUDC - 1]);
                        break;

                }
            }// else ( (numeroIndicUDC == 2 ) && (qtdUDC == 1) )

            proximoValorPrc = Math.Round(proximoValorPrc - Math.Pow(10, intLog10) * qtdUDC, 2);

            // havia grupo de milhar e agora o número zerou ou mudou o milhar
            if ((qtdClasseMonet > 0) && ((proximoValorPrc == 0) || ((int)(Math.Log10(proximoValorPrc) / 3) != qtdClasseMonet)))
            {
                bMudouMilhar = true;
                if (qtdClasseMonet > 1)
                {
                    if (bUsarPluralClasseMonet == true)
                        arrAuxClassesMontearias = arrClassesMonetariasPlural;
                    else
                        arrAuxClassesMontearias = arrClassesMonetariasSingular;
                }
                else
                {
                    arrAuxClassesMontearias = arrClassesMonetariasSingular;
                }

                sbExtenso.Append(" ");
                sbExtenso.Append(arrAuxClassesMontearias[qtdClasseMonet]);
                bUsarPluralClasseMonet = false; // reset uso do plural para o próximo grupo
            } // if ((int)(Math.Log10(proximoValorPrc) / 3) != qtdClasseMonet)

            if (proximoValorPrc < 1)
            {
                // se já estamos nos centavos
                if (bCentavos == true)
                {
                    sbExtenso.Append(" centavo");
                    if (qtdCentavos > 1)
                        sbExtenso.Append("s");

                    // se o parâmetro original era só de centavos
                    if (valor < 1)
                    {
                        sbExtenso.Append(" de real");
                    }
                    bContinua = false; // terminamos aqui
                }
                else
                {
                    // se o parâmetro original era plural
                    if (valor >= 2)
                    {
                        // se o número anterior era da casa de milhões ou acima
                        if (qtdClasseMonet > 1)
                            sbExtenso.Append(" de");

                        sbExtenso.Append(" reais");
                    }
                    else
                        sbExtenso.Append(" real");

                    if (proximoValorPrc == 0)
                    {
                        bContinua = false; // terminamos aqui
                    }
                    else
                    {
                        sbExtenso.Append(" e ");
                        proximoValorPrc *= 100;
                        bCentavos = true;
                        qtdCentavos = (int)proximoValorPrc;
                    }
                }
            } // if (proximoValorPrc < 1)
            else
            {
                if (bMudouMilhar)
                {
                    qtdClassesComValor = GetQtdGruposComValor(proximoValorPrc);
                    if (qtdClassesComValor > 1)
                        sbExtenso.Append(", ");
                    else
                        sbExtenso.Append(" e ");
                }
                else
                {

                    if ((numeroIndicUDC > 2) || ((numeroIndicUDC == 2) && (qtdUDC > 1)))
                    {
                        sbExtenso.Append(" e ");
                    }
                }
            }
        }
        return sbExtenso.ToString();
    }
    static private int GetQtdGruposComValor(double valorPmt)
    {
        if (valorPmt <= 0)
        {
            return 0;
        }
        else if (valorPmt < 1)
        {
            return 1;
        }
        int qtdGrupoComValor = 1;
        int numeroClasseMonet, novoNumeroClasseMonet;
        double auxValor, log10;
        int intLog10, qtdUDC;
        bool bContinua = true;

        auxValor = Math.Round(valorPmt, 2);
        log10 = Math.Log10(auxValor);
        intLog10 = (int)log10;
        numeroClasseMonet = (int)(log10 / 3);
        qtdUDC = (int)(auxValor / Math.Pow(10, intLog10));

        while (bContinua)
        {
            auxValor = Math.Round(auxValor - Math.Pow(10, intLog10) * qtdUDC, 2);
            log10 = Math.Log10(auxValor);
            intLog10 = (int)log10;
            novoNumeroClasseMonet = (int)(log10 / 3);

            if (auxValor <= 0)
            {
                // se chegou no zero, sai do loop
                bContinua = false;
            }
            else if (auxValor < 1)
            {
                // se chegou nos centavos, sai do loop somando mais um grupo
                qtdGrupoComValor++;
                bContinua = false;
            }

            if (bContinua)
            {

                // se chegarmos no próximo grupo de milhar
                if (novoNumeroClasseMonet != numeroClasseMonet)
                {
                    qtdGrupoComValor++;
                }

                numeroClasseMonet = novoNumeroClasseMonet;
                qtdUDC = (int)(auxValor / Math.Pow(10, intLog10));
            }
        } // while (bContinua)
        return qtdGrupoComValor;
    }

}