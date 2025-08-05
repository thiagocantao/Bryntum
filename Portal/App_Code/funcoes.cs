﻿using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;

namespace naObraV2.Classes
{
    class Util
    {
        public static DataTable padronizarReajuste(DataTable dt, Boolean remover = false, Boolean primeiro = false)
        {
            DataTable retorno = dt.Copy();
            retorno.CaseSensitive = false;
            DataRow[] rows = retorno.Select("DescricaoAcessorio = 'Reajuste'");
            if (rows.Count() == 1)
            {
                DataRow row = rows[0];
                if (remover)
                    row.Delete();
                else
                {
                    row["Tipo"] = DBNull.Value;
                    //row["ValorAcumulado"] = DBNull.Value;
                    row["IncideSobreQualValor"] = DBNull.Value;
                    if (primeiro)
                    {
                        DataTable temp = retorno.Copy();
                        retorno = new DataTable();
                        DataRow[] comR = temp.Select("DescricaoAcessorio = 'Reajuste'");
                        if (comR.Count() > 0)
                            retorno.Merge(comR.CopyToDataTable());
                        DataRow[] semR = temp.Select("DescricaoAcessorio <> 'Reajuste'");
                        if (semR.Count() > 0)
                            retorno.Merge(semR.CopyToDataTable());
                    }
                    else
                    {
                        row["Valor"] = DBNull.Value;
                    }
                }
            }
            return retorno;
        }
    }

    #region Número por extenso
    public class NumeroPorExtenso
    {
        private ArrayList numeroLista;
        private Int32 num;
        public Boolean numeroValido;
        Boolean capitalizar;

        //array de 2 linhas e 14 colunas[2][14]
        private static readonly String[,] qualificadores = new String[,] {
//                {"milésimo de real","milésimos de real"},//[0][0] e [0][1]
                {"centavo", "centavos"},//[1][0] e [1][1]
                {"", ""},//[2][0],[2][1]
                {"mil", "mil"},
                {"milhão", "milhões"},
                {"bilhão", "bilhões"},
                {"trilhão", "trilhões"},
                {"quatrilhão", "quatrilhões"},
                {"quintilhão", "quintilhões"},
                {"sextilhão", "sextilhões"},
                {"setilhão", "setilhões"},
                {"octilhão","octilhões"},
                {"nonilhão","nonilhões"},
                {"decilhão","decilhões"}
        };

        private static readonly String[,] numeros = new String[,] {
                {"zero", "um", "dois", "três", "quatro",
                 "cinco", "seis", "sete", "oito", "nove",
                 "dez","onze", "doze", "treze", "quatorze",
                 "quinze", "dezesseis", "dezessete", "dezoito", "dezenove"},
                {"vinte", "trinta", "quarenta", "cinquenta", "sessenta",
                 "setenta", "oitenta", "noventa",null,null,null,null,null,null,null,null,null,null,null,null},
                {"cem", "cento",
                 "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos",
                 "setecentos", "oitocentos", "novecentos",null,null,null,null,null,null,null,null,null,null}
                };

        public NumeroPorExtenso()
        {
            numeroLista = new ArrayList();
        }

        public NumeroPorExtenso(Decimal dec, Boolean Capitalizar)
        {
            capitalizar = Capitalizar;
            numeroLista = new ArrayList();
            SetNumero(dec);
        }

        public void SetNumero(Decimal dec)
        {
            dec = Decimal.Round(dec, 2);
            dec = dec * 100;
            try
            {
                num = Convert.ToInt32(dec);
                numeroValido = num != 0;
            }
            catch
            {
                numeroValido = false;
                return;
            }

            numeroLista.Clear();

            if (num == 0)
            {
                numeroLista.Add(0);
                numeroLista.Add(0);
            }
            else
            {
                AddRemainder(100);

                while (num != 0)
                {
                    AddRemainder(1000);
                }
            }
        }

        private void AddRemainder(Int32 divisor)
        {
            Int32 div = num / divisor;
            Int32 mod = num % divisor;
            Int32[] newNum = new Int32[] { div, mod };

            numeroLista.Add(mod);
            num = div;
        }

        private bool TemMaisGrupos(Int32 ps)
        {
            while (ps > 0)
            {

                if ((Int32)numeroLista[ps] != 00 && !TemMaisGrupos(ps - 1))
                    return true;

                ps--;
            }
            return true;
        }

        private bool EhPrimeiroGrupoUm()
        {
            if ((Int32)numeroLista[numeroLista.Count - 1] == 1)
                return true;
            else
                return false;
        }

        private bool EhUltimoGrupo(Int32 ps)
        {
            return ((ps > 0) && ((Int32)numeroLista[ps] != 0) || !TemMaisGrupos(ps - 1));
        }

        private bool EhGrupoZero(Int32 ps)
        {

            if (ps <= 0 || ps >= numeroLista.Count)
                return true;

            return ((Int32)numeroLista[ps] == 0);
        }

        private bool EhUnicoGrupo()
        {
            if (numeroLista.Count <= 3)
                return false;

            if (!EhGrupoZero(1) && !EhGrupoZero(2))
                return false;

            bool hasOne = false;

            for (Int32 i = 3; i < numeroLista.Count; i++)
            {
                if ((Int32)numeroLista[i] != 0)
                {
                    if (hasOne)
                        return false;

                    hasOne = true;
                }
            }
            return true;
        }

        private String NumToString(Int32 numero, Int32 escala)
        {
            Int32 unidade = (numero % 10);
            Int32 dezena = (numero % 100);
            Int32 centena = (numero / 100);
            StringBuilder buf = new StringBuilder();

            if (numero != 0)
            {
                if (centena != 0)
                {
                    if (dezena == 0 && centena == 1)
                    {
                        buf.Append(numeros[2, 0]);
                    }
                    else
                    {
                        buf.Append(numeros[2, centena]);
                    }
                }

                if (buf.Length > 0 && dezena != 0)
                {
                    buf.Append(" e ");
                }

                if (dezena > 19)
                {
                    dezena = dezena / 10;
                    buf.Append(numeros[1, dezena - 2]);

                    if (unidade != 0)
                    {
                        buf.Append(" e ");
                        buf.Append(numeros[0, unidade]);
                    }
                }
                else if (centena == 0 || dezena != 0)
                {
                    buf.Append(numeros[0, dezena]);
                }

                buf.Append(" ");

                if (numero == 1)
                {
                    buf.Append(qualificadores[escala, 0]);
                }
                else
                {
                    buf.Append(qualificadores[escala, 1]);
                }
            }
            return buf.ToString();
        }

        public String ToString1()
        {
            StringBuilder buf = new StringBuilder();
            Int32 numero = (Int32)numeroLista[0];
            Int32 count;

            for (count = numeroLista.Count - 1; count > 0; count--)
            {

                if (buf.Length > 0 && !EhGrupoZero(count))
                {
                    buf.Append(" e ");
                }
                buf.Append(NumToString((Int32)numeroLista[count], count));
            }

            if (buf.Length > 0)
            {

                while (buf.ToString().EndsWith(" "))
                    buf.Length = buf.Length - 1;

                if (EhUnicoGrupo())
                {
                    buf.Append(" de ");
                }

                if (EhPrimeiroGrupoUm())
                {
                    buf.Insert(0, "h");
                }

                if (numeroLista.Count == 2 && ((Int32)numeroLista[1] == 1))
                {
                    buf.Append(" real");
                }
                else
                {
                    buf.Append(" reais");
                }

                if ((Int32)numeroLista[0] != 0)
                {
                    buf.Append(" e ");
                }
            }

            if ((Int32)numeroLista[0] != 0)
            {
                buf.Append(NumToString((Int32)numeroLista[0], 0));
            }

            String numeroConvertido = buf.ToString();
            if (capitalizar)
                numeroConvertido = char.ToUpper(numeroConvertido[0]) + numeroConvertido.Substring(1);
            return numeroConvertido;
        }

    }
}
#endregion