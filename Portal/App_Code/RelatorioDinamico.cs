using DevExpress.Data.Filtering;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RelatorioDinamico
/// </summary>
public static class RelatorioDinamico
{
	//public static RelatorioDinamico()
	//{
	//	//
	//	// TODO: Add constructor logic here
	//	//
	//}


    public static bool validaFilterExpression(string filterExpression, List<String> col)
    {
        

        var creteriaOperator = CriteriaOperator.Parse(filterExpression);
        var keys = CriteriaColumnAffinityResolver.SplitByColumnNames(creteriaOperator);
        bool filtroOk = true;

        if (keys.Item1 is GroupOperator)
        {
            filtroOk = validaGrupoOperadores(((GroupOperator)keys.Item1), col);

        }
        if (filtroOk)
        {
            if (keys.Item2 is System.Collections.Generic.Dictionary<string, DevExpress.Data.Filtering.CriteriaOperator>)
            {
                filtroOk = validaDicionarioCriterio(keys.Item2, col);
            }
        }

        return filtroOk;
    }

    private static bool validaGrupoOperadores(GroupOperator groupOperator, List<String> col)
    {
        CriteriaOperator operando;

        foreach (var key in groupOperator.Operands)
        {
            if (key is UnaryOperator)
            {
                operando = ((UnaryOperator)key).Operand;
            }
            else
            {
                operando = key;
            }
            if (operando is GroupOperator)
            {
                if (!validaGrupoOperadores((GroupOperator)operando, col))
                {
                    return false;
                }

            }
            else
            {
                if (!validaOperadorCriterio(operando, col))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static bool validaOperadorCriterio(CriteriaOperator operando, List<String> col)
    {
        if (operando is BinaryOperator)
        {

            //if (this.pgDados.Fields[((BinaryOperator)operando).LeftOperand.ToString().Replace("[", "").Replace("]", "")] == null)
            //    return false;
            String nomeColuna = ((BinaryOperator)operando).LeftOperand.ToString().Replace("[", "").Replace("]", "");
            if (!col.Exists(x => x.Equals(nomeColuna)))
                return false;
        }
        else if (operando is FunctionOperator)
        {
            foreach (var operandoAux in ((FunctionOperator)operando).Operands)
            {
                if (operandoAux is DevExpress.Data.Filtering.OperandProperty)
                {
                    var nomeColuna = ((DevExpress.Data.Filtering.OperandProperty)operandoAux).PropertyName;
                    //if (this.pgDados.Fields[nomeColuna] == null)
                    //    return false;
                    if (!col.Exists(x => x.Equals(nomeColuna)))
                        return false;
                }
            }

        }
        else if (operando is InOperator)
        {
            //if (this.pgDados.Fields[((DevExpress.Data.Filtering.OperandProperty)((DevExpress.Data.Filtering.InOperator)operando).LeftOperand).PropertyName] == null)
            //    return false;
            String nomeColuna = ((DevExpress.Data.Filtering.OperandProperty)((DevExpress.Data.Filtering.InOperator)operando).LeftOperand).PropertyName;
            if (!col.Exists(x => x.Equals(nomeColuna)))
                return false;

        }
        else if (operando is UnaryOperator)
        {
            if (operando is DevExpress.Data.Filtering.OperandProperty)
            {
                var nomeColuna = ((DevExpress.Data.Filtering.OperandProperty)operando).PropertyName;
                //if (this.pgDados.Fields[nomeColuna] == null)
                //    return false;
                if (!col.Exists(x => x.Equals(nomeColuna)))
                    return false;
            }
            else
            {
                if (!validaOperadorCriterio(((UnaryOperator)operando).Operand, col))
                {
                    return false;
                }
            }
        }
        else if (operando is DevExpress.Data.Filtering.OperandProperty)
        {
            var nomeColuna = ((DevExpress.Data.Filtering.OperandProperty)operando).PropertyName;
            //if (this.pgDados.Fields[nomeColuna] == null)
            //    return false;
            if (!col.Exists(x => x.Equals(nomeColuna)))
                return false;
        }
        else
        {
            return true;//autorizando a aplicação do filtro em situações não tratadas(deixa pra ver se vai dar erro Intrução by Géter)
        }

        return true;
    }

    private static bool validaDicionarioCriterio(IDictionary<string, CriteriaOperator> dictioonary, List<String> col)
    {
       

        foreach (var item in dictioonary)
        {
            //if (this.pgDados.Fields[item.Key.ToString().Replace("[", "").Replace("]", "")] == null)
            //    return false;
            String nomeColuna = item.Key.ToString().Replace("[", "").Replace("]", "");
            if (!col.Exists(x=> x.Equals(nomeColuna)))
                return false;
        }
        return true;
    }
}