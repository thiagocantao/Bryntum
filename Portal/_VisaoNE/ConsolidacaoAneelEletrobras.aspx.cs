using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Web.ASPxHtmlEditor;
using System.Data;
using System.Text;
using DevExpress.Web;

public partial class _VisaoNE_ConsolidacaoAneelEletrobras : System.Web.UI.Page
{
    #region Private Fields

    private dados cDados;

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

            GerarRelatorio(DestinatarioRelatorio.ANEEL);
            GerarRelatorio(DestinatarioRelatorio.ELETROBRAS);
        }
    }

    protected void btnGerar_Click(object sender, EventArgs e)
    {
        String destinatario;
        ASPxHtmlEditor htmlEditor;
        ASPxButton button = (ASPxButton)sender;
        if (sender == btnGerarAneel)
        {
            destinatario = "ANEEL";
            htmlEditor = htmlAneel;
        }
        else
        {
            destinatario = "ELETROBRAS";
            htmlEditor = htmlEletrobras;
        }
        using (MemoryStream ms = new MemoryStream())
        {
            String msg = String.Empty;
            try
            {
                htmlEditor.Export(HtmlEditorExportFormat.Rtf, ms);
                Download(ms, destinatario);
                msg = "Operação realizada com sucesso.";
            }
            catch
            {
                msg = "Falha ao realizar a operação.";
            }
            finally
            {
                ms.Close();
                hfGeral.Set("ResultadoOpercao", msg);
            }
        }
    }

    #endregion

    #region Methods

    private void GerarRelatorio(DestinatarioRelatorio destinatario)
    {
        ASPxHtmlEditor htmlEditor = destinatario == DestinatarioRelatorio.ANEEL ? htmlAneel : htmlEletrobras;
        MemoryStream ms = new MemoryStream();
        htmlEditor.Export(HtmlEditorExportFormat.Rtf, ms);
        ms.Position = 0;
        using (StreamReader sr = new StreamReader(ms))
        {
            String relatorio = sr.ReadToEnd();
            AtualizarRelatorioFiscalizacao(destinatario, relatorio);
            sr.Close();
        }
    }

    private void LoadData()
    {
        string cwf = Request.QueryString["CWF"];
        string ciwf = Request.QueryString["CIWF"];

        #region Comando SQL
        String sqlCommand = String.Format(@"
DECLARE @MesAno CHAR(7)

 SELECT @MesAno = MesAno
   FROM MesAnoFluxo
  WHERE CodigoWorkflow = {2}
    AND CodigoInstanciaWf = {3}

 SELECT @MesAno AS MesAno
                                               
 SELECT ccrf.Conteudo,
        crf.NomeFormulario,
        crf.NomeCampo
   FROM {0}.{1}.ConteudoCampoRelatorioFiscalizacao ccrf INNER JOIN
        {0}.{1}.CamposRelatoriosFiscalizacao crf ON ccrf.CodigoCampo = crf.CodigoCampo INNER JOIN
        {0}.{1}.CampoDestinatario cd ON cd.CodigoCampo = crf.CodigoCampo INNER JOIN
        {0}.{1}.DestinatariosRelatoriosFiscalizacao drf ON drf.CodigoDestinatario = cd.CodigoDestinatario
  WHERE MesAno = @MesAno
    AND drf.NomeDestinatario = 'ANEEL'
  ORDER BY
        cd.OrdemPosicao
        
 SELECT ccrf.Conteudo,
        crf.NomeFormulario,
        CASE WHEN ISNULL(cd.NomeAlternativoCampo, '') = ''
            THEN crf.NomeCampo
            ELSE cd.NomeAlternativoCampo
        END AS NomeCampo
   FROM {0}.{1}.ConteudoCampoRelatorioFiscalizacao ccrf INNER JOIN
        {0}.{1}.CamposRelatoriosFiscalizacao crf ON ccrf.CodigoCampo = crf.CodigoCampo INNER JOIN
        {0}.{1}.CampoDestinatario cd ON cd.CodigoCampo = crf.CodigoCampo INNER JOIN
        {0}.{1}.DestinatariosRelatoriosFiscalizacao drf ON drf.CodigoDestinatario = cd.CodigoDestinatario
  WHERE MesAno = @MesAno
    AND drf.NomeDestinatario = 'ELETROBRAS'
  ORDER BY
        cd.OrdemPosicao"
            , cDados.getDbName(), cDados.getDbOwner(), cwf, ciwf);
        #endregion

        DataSet ds = cDados.getDataSet(sqlCommand);
        String mesAno = ds.Tables[0].Rows[0]["MesAno"] as String;
        DataTable dadosAneel = ds.Tables[1];
        DataTable dadosEletrobras = ds.Tables[2];
        hfGeral.Set("MesAno", mesAno);
        htmlAneel.Html = ObtemConteudoHtml(dadosAneel);
        htmlEletrobras.Html = ObtemConteudoHtml(dadosEletrobras);
    }

    private String ObtemConteudoHtml(DataTable dados)
    {
        StringBuilder conteudoHtml = new StringBuilder();

        foreach (DataRow row in dados.Rows)
        {
            Object nomeFormulario = row["NomeFormulario"];
            Object nomeCampo = row["NomeCampo"];
            Object conteudo = row["Conteudo"];
            conteudoHtml.AppendFormat("<p><h3>{0} - {1}</h3></p>{2}<br />"
                , nomeFormulario, nomeCampo, conteudo);
        }

        return conteudoHtml.ToString();
    }

    private void AtualizarRelatorioFiscalizacao(DestinatarioRelatorio destinatario, String relatorio)
    {
        String mesAno = hfGeral.Get("MesAno") as String;
        String sqlCommand;

        relatorio = relatorio.Replace("'", "''");

        #region Comando SQL
        sqlCommand = String.Format(@"
DECLARE @MesAno CHAR(7)
    SET @MesAno = '{2}'
    
DECLARE @CodigoDestinatario INT
 SELECT @CodigoDestinatario = CodigoDestinatario
   FROM DestinatariosRelatoriosFiscalizacao
  WHERE NomeDestinatario = '{3}'
  
 UPDATE RelatorioFiscalizacao
    SET Relatorio = '{4}',
        DataHoraAtualizacao = GETDATE()
  WHERE CodigoDestinatario = @CodigoDestinatario
    AND MesAno = @MesAno"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , mesAno
            , destinatario
            , relatorio);
        #endregion

        Int32 registrosAfetados = 0;
        cDados.execSQL(sqlCommand, ref registrosAfetados);
    }

    private void Download(MemoryStream stream, String destinatario)
    {
        String fileName = String.Format(
            "{0}_{1:dd-MM-yyyy_HH-mm-ss}.rtf", destinatario, DateTime.Now);
        Response.ContentType = "application/rtf";
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition",
            String.Format("Attachment; filename={0}", fileName));
        Response.AddHeader("Content-Length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.End();
    }

    #endregion

    public enum DestinatarioRelatorio
    {
        ANEEL,
        ELETROBRAS
    }
}