using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _CertificadoDigital_DownloadFormularioAssinado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int codigoFormulario = int.Parse(Request.QueryString["cf"]);
        RealizaDownloadFormularioAssinado(codigoFormulario);
    }

    public void RealizaDownloadFormularioAssinado(int codigoFormulario)
    {
        dados cDados = CdadosUtil.GetCdados(null);
        string connectionString = cDados.classeDados.getStringConexao();
        using (var conn = new SqlConnection(connectionString))
        {
            string comandoSql;

            #region Comando SQL

            comandoSql = string.Format(@"
            BEGIN
            DECLARE @CodigoFormularioAssinado  bigint
            SELECT @CodigoFormularioAssinado = MAX(sub.CodigoFormulario)
            FROM 
            (
                SELECT f.CodigoFormulario 
                  FROM {0}.{1}.vi_Formulario AS f 
                  INNER JOIN {0}.{1}.FormularioAssinatura AS fa ON (fa.CodigoFormulario = f.CodigoFormulario)
                  WHERE f.CodigoFormularioOriginal IN (SELECT vi.CodigoFormularioOriginal 
                                                         FROM {0}.{1}.vi_Formulario AS vi 
                                                        WHERE vi.CodigoFormulario = {2}
                                                          AND vi.CodigoFormulario >= f.CodigoFormulario
                                                          AND vi.VersaoFormulario = f.VersaoFormulario)
                UNION                                                      
                SELECT lf.CodigoFormulario
                  FROM {0}.{1}.LinkFormulario AS lf 
                  INNER JOIN {0}.{1}.FormularioAssinatura AS fa ON (fa.CodigoFormulario = lf.CodigoFormulario
                                                                   AND lf.CodigoSubFormulario = {2}) 
                  INNER JOIN  {0}.{1}.Formulario AS f ON (f.CodigoFormulario = lf.CodigoFormulario 
                                                          AND f.DataExclusao IS NULL)
            ) AS sub;

            SELECT TOP 1
            fa.ImagemFormulario
            FROM {0}.{1}.FormularioAssinatura AS fa
            WHERE fa.CodigoFormulario = @CodigoFormularioAssinado
            ORDER BY fa.DataAssinatura DESC;
            END", cDados.getDbName(), cDados.getDbOwner(), codigoFormulario);

            #endregion

            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(comandoSql, conn);
                comando.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
                comando.Parameters.Add(new SqlParameter("@CodigoFormulario", codigoFormulario));
                byte[] arquivo = comando.ExecuteScalar() as byte[];
                if (arquivo != null)
                {
                    string name = string.Format("{0:yyyyMMddHHmm}.pdf", DateTime.Now);
                    Response.AppendHeader("content-disposition", "attachment; filename=" + name);
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(arquivo);
                    Response.Flush();
                    Response.End();
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}