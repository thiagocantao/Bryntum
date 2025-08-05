using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;

public partial class _Projetos_Agil_reuniaoDiariaSprint : System.Web.UI.Page
{

    dados cDados;
    public int codigoProjeto = -1;
    public static int codigoIteracao = -1;

    public static int codigoEventoAtual = -1;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;

    public static string dataHoraInicio;
    public string dataHoraFinal;

    string[] NumerosCardinais = new string[] {"Primeira",
"Segunda",
"Terceira",
"Quarta",
"Quinta",
"Sexta",
"Sétima",
"Oitava",
"Nona",
"Décima",
"décima primeira",
"décima segunda",
"décima terceira",
"Décima quarta",
"Décima quinta",
"Décima sexta",
"Décima sétima",
"Décima oitava",
"Décima nona",
"Vigésima",
"Vigésima primeira",
"Vigésima segunda",
"Vigésima terceira",
"Vigésima quarta",
"Vigésima quinta",
"Vigésima sexta",
"Vigésima sétima",
"Vigésima oitava",
"Vigésima nona",
"Trigésima",
"Trigésima primeira",
"Trigésima segunda",
"Trigésima terceira",
"Trigésima quarta",
"Trigésima quinta",
"Trigésima sexta",
"Trigésima sétima",
"Trigésima oitava",
"Trigésima nona",
"Quadragésima",
"Quadragésima primeira",
"Quadragésima segunda",
"Quadragésima terceira",
"Quadragésima quarta",
"Quadragésima quinta",
"Quadragésima sexta",
"Quadragésima sétima",
"Quadragésima oitava",
"Quadragésima nona",
"Quinquagésima",
"Quinquagésima primeira",
"Quinquagésima segunda",
"Quinquagésima terceira",
"Quinquagésima quarta",
"Quinquagésima quinta",
"Quinquagésima sexta",
"Quinquagésima sétima",
"Quinquagésima oitava",
"Quinquagésima nona",
"Sexagésima",
"Sexagésima primeira",
"Sexagésima segunda",
"Sexagésima terceira",
"Sexagésima quarta",
"Sexagésima quinta",
"Sexagésima sexta",
"Sexagésima sétima",
"Sexagésima oitava",
"Sexagésima nona",
"Septuagésima",
"Septuagésima primeira",
"Septuagésima segunda",
"Septuagésima terceira",
"Septuagésima quarta",
"Septuagésima quinta",
"Septuagésima sexta",
"Septuagésima sétima",
"Septuagésima oitava",
"Septuagésima nona",
"Octogésima",
"Octogésima primeira",
"Octogésima segunda",
"Octogésima terceira",
"Octogésima quarta",
"Octogésima quinta",
"Octogésima sexta",
"Octogésima sétima",
"Octogésima oitava",
"Octogésima nona",
"Nonagésima",
"Nonagésima primeira",
"Nonagésima segunda",
"Nonagésima terceira",
"Nonagésima quarta",
"Nonagésima quinta",
"Nonagésima sexta",
"Nonagésima sétima",
"Nonagésima oitava",
"Nonagésima nona" };

    protected void Page_Init(object sender, EventArgs e)
    {

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CP"] != null && (Request.QueryString["CP"].ToString() + "") != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        defineAlturaTela();
        HeaderOnTela();

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string comandosql = string.Format(@"SELECT ai.[CodigoIteracao]
                                                      ,ai.[InicioPrevisto]
                                                      ,ai.[TerminoPrevisto]
                                                      ,p.[NomeProjeto]
                                                      ,ai.[IndicaItensNaoPlanejados]
                                                      ,ai.[LocalReuniaoDiaria]
                                                      ,ai.[TipoGraficoTaskBoard]
                                                      ,ai.[HorarioInicioReuniaoDiaria]
                                                      ,ai.[HorarioTerminoReuniaoDiaria]
                                                  FROM {0}.{1}.[Agil_Iteracao] ai
                                            INNER JOIN {0}.{1}.Projeto p on (p.CodigoProjeto = ai.CodigoProjetoIteracao)
                                                 WHERE [CodigoProjetoIteracao] = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

            DataSet ds = cDados.getDataSet(comandosql);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoIteracao = int.Parse(ds.Tables[0].Rows[0]["CodigoIteracao"].ToString());
            }

            cDados.aplicaEstiloVisual(Page);

            dataHoraInicio = cDados.classeDados.getDateDB();
            codigoEventoAtual = carregaDadosEventoAtual(codigoProjeto);
        }

        //carregaGVParticipantes();
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/reuniaoDiariaSprint.js""></script>"));
        this.TH(this.TS("reuniaoDiariaSprint"));
    }

    private void defineAlturaTela()
    {
        int largura = 0;
        int altura = 0;

        if (Request.QueryString["alt"] != null && (Request.QueryString["alt"].ToString() + "") != "")
        {
            altura = int.Parse(Request.QueryString["alt"].ToString());
        }
        if (Request.QueryString["larg"] != null && (Request.QueryString["larg"].ToString() + "") != "")
        {
            largura = int.Parse(Request.QueryString["larg"].ToString());
        }
    }

    private int carregaDadosEventoAtual(int codigoProjeto)
    {
        int retorno = -1;
        string comandoSQL = string.Format(@"
        SELECT TOP 1 [CodigoEvento]
              ,[DescricaoResumida]
              ,[CodigoResponsavelEvento]
              ,[InicioReal]
              ,[TerminoReal]
              ,[LocalEvento]
              ,[ResumoEvento]
              ,[CodigoTipoEvento]
          FROM [Evento]
          WHERE [CodigoEntidade] = {0} AND
                [CodigoTipoAssociacao] = (SELECT CodigoTipoAssociacao 
                                            FROM TipoAssociacao 
                                           WHERE IniciaisTipoAssociacao = 'PR') AND
                [CodigoObjetoAssociado] = {1} order by [TerminoReal] asc", codigoEntidadeUsuarioResponsavel, codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            memoAta.Html = ds.Tables[0].Rows[0]["ResumoEvento"].ToString();
            retorno = int.Parse(ds.Tables[0].Rows[0]["CodigoEvento"].ToString());

            IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);

            txtHoraInicioReal.Text = ds.Tables[0].Rows[0]["InicioReal"].ToString() != "" ? string.Format("{0:HH:mm}", DateTime.Parse(ds.Tables[0].Rows[0]["InicioReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)) : "";
            txtHoraTerminoReal.Text = ds.Tables[0].Rows[0]["TerminoReal"].ToString() != "" ? string.Format("{0:HH:mm}", DateTime.Parse(ds.Tables[0].Rows[0]["TerminoReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)) : "";

            memoLocal.Text = ds.Tables[0].Rows[0]["LocalEvento"].ToString();
            dateEditDataReuniao.Value = ds.Tables[0].Rows[0]["TerminoReal"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[0]["TerminoReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal): DateTime.Now;

        }
        else
        {
            dateEditDataReuniao.Value = DateTime.Now;
        }
        return retorno;
    }

 



    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string idEventoNovo = "";
        string mesgError = "";
        string operacao = "";
        string mensagemSucesso = "";
        bool result = false;


        string inicioRealFormatado = dateEditDataReuniao.Date.Year + "-" + dateEditDataReuniao.Date.Month + "-" + dateEditDataReuniao.Date.Day+ "T" + txtHoraInicioReal.Text;
        string terminoRealFormatado = dateEditDataReuniao.Date.Year + "-" + dateEditDataReuniao.Date.Month + "-" + dateEditDataReuniao.Date.Day + "T" + txtHoraTerminoReal.Text;

        string[] arrayParticipantesSelecionados = new string[gvParticipantesReuniao.Selection.Count];
        int i = 0;
        foreach (object codigo in gvParticipantesReuniao.GetSelectedFieldValues(gvParticipantesReuniao.KeyFieldName))
        {
            arrayParticipantesSelecionados[i++] =  codigo.ToString();
        }

        result = incluiAtualizaEvento(inicioRealFormatado,
            terminoRealFormatado,
            memoLocal.Text,
            memoAta.Html, arrayParticipantesSelecionados,
            ref mesgError, ref operacao, ref idEventoNovo);

        mensagemSucesso = operacao == "Incluir" ? "Reunião de Sprint Incluída com sucesso!" : "Reunião de Sprint alterada com sucesso!";

        ((ASPxCallback)source).JSProperties["cp_mensagemErro"] = mesgError;
        ((ASPxCallback)source).JSProperties["cp_mensagemSucesso"] = mensagemSucesso;

    }

    private string incluiParticipantesSelecionados(string[] arrayParticipantesSelecionados, string idEvento)
    {
        string comandoSQL = "";
        int registrosAfetados = 0;
        string retorno = "";
        try
        {
            comandoSQL = string.Format(@" DELETE FROM {0}.{1}.ParticipanteEvento 
                         WHERE CodigoEvento = {2} ", cDados.getDbName(), cDados.getDbOwner(), idEvento);

            foreach (string codigoParticipante in arrayParticipantesSelecionados)
            {
                if (codigoParticipante != "")
                {
                    comandoSQL += string.Format(@" INSERT INTO {0}.{1}.ParticipanteEvento(CodigoEvento, CodigoParticipante) values({2}, {3}) ", cDados.getDbName(), cDados.getDbOwner(), idEvento, codigoParticipante);
                }
            }
            cDados.execSQL(comandoSQL, ref registrosAfetados);
            retorno = "";
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
    }



    protected void gvParticipantesReuniao_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string parametro = e.Parameters;

        //((ASPxGridView)sender).JSProperties["cp_checado"] = (parametro == "true") ? true : false;

        //if ((parametro == "true"))
        //{
        //    ((ASPxGridView)sender).Selection.SelectAll();
        //}
        //else
        //{
        //    ((ASPxGridView)sender).Selection.UnselectAll();
        //}

        string comandoSQL = string.Format(@"
               SELECT us.CodigoUsuario
                ,   us.NomeUsuario
                ,   us.EMail
                ,  (SELECT ari.CodigoRecursoCorporativo
                     FROM {0}.{1}.Agil_RecursoIteracao ari
               INNER JOIN {0}.{1}.RecursoCorporativo AS rc on (rc.CodigoUsuario = us.CodigoUsuario)
               INNER JOIN {0}.{1}.ParticipanteEvento AS pe on (pe.CodigoParticipante = rc.CodigoUsuario) 
                    WHERE ari.CodigoIteracao = {3} 
                      AND ari.CodigoRecursoCorporativo = rc.CodigoRecursoCorporativo
                      and pe.CodigoEvento = {4}) AS selecionado                
                 FROM {0}.{1}.Usuario AS us 
           INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun ON (uun.CodigoUsuario = us.CodigoUsuario )
           INNER JOIN {0}.{1}.RecursoCorporativo AS rc on (rc.CodigoUsuario = us.CodigoUsuario)
           INNER JOIN {0}.{1}.Agil_RecursoIteracao ari1 on (ari1.CodigoRecursoCorporativo = rc.CodigoRecursoCorporativo ) 
				WHERE Uun.CodigoUnidadeNegocio = {2}  
                 AND rc.IndicaRecursoAtivo = 'S'
                 AND rc.CodigoEntidade = {5}
                 AND rc.CodigoTipoRecurso = 1
                 AND ari1.CodigoIteracao = {3}              
                ORDER BY us.NomeUsuario
                ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoIteracao, (parametro == "") ? codigoEventoAtual.ToString() : parametro, codigoEntidadeUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(comandoSQL);

        DataTable dtParticipantes = ds.Tables[0];

        gvParticipantesReuniao.DataSource = dtParticipantes;
        gvParticipantesReuniao.DataBind();

        //if (!Page.IsPostBack)
        //{
        if (codigoEventoAtual == -1)
        {
            gvParticipantesReuniao.Selection.SelectAll();
        }
        else
        {
            gvParticipantesReuniao.Selection.UnselectAll();
            int i = 0;
            foreach (DataRow dr in dtParticipantes.Rows)
            {
                if (dr["selecionado"].ToString() != string.Empty)
                {
                    gvParticipantesReuniao.Selection.SelectRow(i);
                }
                i++;
            }
        }
        //}
    }
    protected void ASPxDateEdit1_CalendarDayCellPrepared(object sender, CalendarDayCellPreparedEventArgs e)
    {

        if (/*e.Date <= DateTime.Now &&*/ e.Date.DayOfWeek != DayOfWeek.Sunday)
        {
            string comandoSQL = string.Format(@"
            SELECT [CodigoEvento]
            FROM Evento 
            WHERE CodigoEntidade = {0} AND CodigoObjetoAssociado = {1}
            AND CodigoTipoAssociacao = [dbo].[f_GetCodigoTipoAssociacao]('PR')
            AND  (YEAR(TerminoReal) = {2} 
                  AND MONTH(TerminoReal) = {3}
                  AND DAY(TerminoReal) = {4}) ", codigoEntidadeUsuarioResponsavel, codigoProjeto, e.Date.Year, e.Date.Month, e.Date.Day);

            DataSet dsReuniao = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
            {
                e.Cell.BackColor = Color.LightGreen;
                e.Cell.Font.Bold = true;
            }
        }
        //}
    }

    protected void callbackAtualizaTela_Callback(object source, CallbackEventArgs e)
    {
        IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);
        DateTime objData = DateTime.Parse((e.Parameter == "" ? DateTime.Now.ToString("dd/MM/yyyy"): e.Parameter), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        ((ASPxCallback)source).JSProperties["cp_memoLocal"] = "";
        ((ASPxCallback)source).JSProperties["cp_txtHoraInicioReal"] = "";
        ((ASPxCallback)source).JSProperties["cp_txtHoraTerminoReal"] = "";
        ((ASPxCallback)source).JSProperties["cp_memoAta"] = "";
        ((ASPxCallback)source).JSProperties["cp_CodigoEvento"] = "-1";

        string comandoSQL = string.Format(@"
        
        DECLARE @dataParametro AS DATETIME        
        SET @dataParametro = CONVERT(DATETIME, '{0}', 103)

            SELECT [CodigoEvento]
                  ,[InicioReal]
                  ,[TerminoReal]
                  ,[LocalEvento]
                  ,[ResumoEvento]
            FROM Evento 
            WHERE CodigoEntidade = {1} AND CodigoObjetoAssociado = {2}
            AND CodigoTipoAssociacao = [dbo].[f_GetCodigoTipoAssociacao]('PR')
            AND (YEAR(TerminoReal) = YEAR(@dataParametro) 
                AND MONTH(TerminoReal) = MONTH(@dataParametro) 
                AND DAY(TerminoReal) = DAY(@dataParametro))", objData.ToString(), codigoEntidadeUsuarioResponsavel, codigoProjeto);
        
        DataSet ds = cDados.getDataSet(comandoSQL);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtHoraInicioReal.Text = ds.Tables[0].Rows[0]["InicioReal"].ToString() != "" ? string.Format("{0:HH:mm}", DateTime.Parse(ds.Tables[0].Rows[0]["InicioReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)) : "";
            ((ASPxCallback)source).JSProperties["cp_memoLocal"] = ds.Tables[0].Rows[0]["LocalEvento"].ToString();

            ((ASPxCallback)source).JSProperties["cp_txtHoraInicioReal"] = ds.Tables[0].Rows[0]["InicioReal"].ToString() != "" ? string.Format("{0:HH:mm}", DateTime.Parse(ds.Tables[0].Rows[0]["InicioReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)) : "";
            ((ASPxCallback)source).JSProperties["cp_txtHoraTerminoReal"] = ds.Tables[0].Rows[0]["TerminoReal"].ToString() != "" ? string.Format("{0:HH:mm}", DateTime.Parse(ds.Tables[0].Rows[0]["TerminoReal"].ToString(), iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)) : "";

            ((ASPxCallback)source).JSProperties["cp_memoAta"] = ds.Tables[0].Rows[0]["ResumoEvento"].ToString();
            ((ASPxCallback)source).JSProperties["cp_CodigoEvento"] = ds.Tables[0].Rows[0]["CodigoEvento"].ToString();
            tabControl1.JSProperties["cp_CodigoEvento"] = ds.Tables[0].Rows[0]["CodigoEvento"].ToString();
        }

    }

    public bool incluiAtualizaEvento(string InicioReal, string TerminoReal, string LocalEvento,
                         string ResumoEvento, string[] arrayParticipantesSelecionados, ref string msgError, ref string operacaoFeita, ref string idEventoNovo)
    { 
        bool retorno = false;
        IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);
        DateTime objDataInicioReal = DateTime.Parse(InicioReal, iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        DateTime objDataTerminoReal = DateTime.Parse(TerminoReal, iFormatProvider, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

        string comandoSQL = "";
        try
        {

            comandoSQL = string.Format(@"                       
            DECLARE @NovoCodigoEvento as int
            DECLARE @DescricaoResumida as varchar(100)
		    DECLARE @CodigoResponsavelEvento as int
            DECLARE @InicioReal as datetimeoffset(7)
            DECLARE @TerminoReal as datetimeoffset(7)
            DECLARE @CodigoTipoAssociacao as smallint
            DECLARE @CodigoObjetoAssociado as bigint
            DECLARE @LocalEvento as varchar(50)
            DECLARE @ResumoEvento as varchar(max)
            DECLARE @CodigoEntidade as int
		    DECLARE @CodigoTipoEvento as int
		    DECLARE @DataInclusaoAlteracao as Datetime
            DECLARE @OperacaoFeita as varchar(10);           
            DECLARE @CodigoEventoAtual as int

		    SET @CodigoObjetoAssociado = {0}
		    SET @DescricaoResumida =  'Reunião diária - ' + (SELECT TOP 1 NomeProjeto FROM Projeto WHERE CodigoProjeto = @CodigoObjetoAssociado)
		    SET @CodigoResponsavelEvento = {1}
            SET @InicioReal = {2}
            SET @TerminoReal = {3}
            SET @CodigoTipoAssociacao = dbo.[f_GetCodigoTipoAssociacao] ('PR')
           
            SET @LocalEvento = '{4}'
            SET @ResumoEvento  = '{5}'
            SET @CodigoEntidade = {6}
            SET @CodigoEventoAtual = {7}
		 
		    SELECT TOP 1 @CodigoTipoEvento = CodigoTipoEvento 
              FROM TipoEvento 
		      WHERE IniciaisTipoReuniaoControladaSistema = 'ITERACAO' 
				AND CodigoEntidade = @CodigoEntidade 
                AND CodigoModuloSistema = 'PRJ'
            
            SET @DataInclusaoAlteracao = GETDATE()

		    IF NOT EXISTS( SELECT 1 FROM Evento 
		                    WHERE (YEAR(TerminoReal) = YEAR(@TerminoReal) AND MONTH(TerminoReal) = MONTH(@TerminoReal) AND DAY(TerminoReal) = DAY(@TerminoReal))
						      AND CodigoTipoAssociacao = dbo.[f_GetCodigoTipoAssociacao] ('PR') 
						      AND CodigoObjetoAssociado = @CodigoObjetoAssociado) OR (@CodigoEventoAtual = -1) 
		   BEGIN
		        INSERT INTO [dbo].[Evento]([DescricaoResumida], [CodigoResponsavelEvento], [InicioReal], [TerminoReal]
                                ,[InicioPrevisto], [TerminoPrevisto]
                                ,[CodigoTipoAssociacao], [CodigoObjetoAssociado], [LocalEvento], [ResumoEvento]
                                ,[CodigoTipoEvento], [DataInclusao], [CodigoUsuarioInclusao], [CodigoEntidade])

                        VALUES(@DescricaoResumida, @CodigoResponsavelEvento, @InicioReal, @TerminoReal
                                ,@InicioReal, @TerminoReal
                                ,@CodigoTipoAssociacao, @CodigoObjetoAssociado, @LocalEvento, @ResumoEvento
                                , @CodigoTipoEvento, @DataInclusaoAlteracao, @CodigoResponsavelEvento, @CodigoEntidade)
               SET @OperacaoFeita = 'Incluir'
               SET @NovoCodigoEvento = scope_identity();
		   END
		   ELSE
		   BEGIN
                UPDATE [dbo].[Evento]
                   SET 
                      [InicioReal] = @InicioReal
                      ,[TerminoReal] = @TerminoReal
                      ,[InicioPrevisto] = @InicioReal
                      ,[TerminoPrevisto] = @TerminoReal
                      ,[LocalEvento] = @LocalEvento
                      ,[ResumoEvento] = @ResumoEvento
                      ,[DataUltimaAlteracao] = @DataInclusaoAlteracao
                      ,[CodigoUsuarioUltimaAlteracao] = @CodigoResponsavelEvento
                 WHERE CodigoEvento = @CodigoEventoAtual 
                 SET @OperacaoFeita = 'Atualizar'
		   END
           IF(@OperacaoFeita = 'Incluir')
           BEGIN
                SELECT @NovoCodigoEvento, @OperacaoFeita
           END
           ELSE
           BEGIN
                SELECT @CodigoEventoAtual , @OperacaoFeita
           END

", codigoProjeto, codigoUsuarioResponsavel,
                          (InicioReal == "NULL" ? "NULL" : ("CONVERT(datetimeoffset, '" + objDataInicioReal.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + "', 127)")),
                         (TerminoReal == "NULL" ? "NULL" : ("CONVERT(datetimeoffset, '" + objDataTerminoReal.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + "', 127)")),
                        LocalEvento, ResumoEvento, codigoEntidadeUsuarioResponsavel,  hfGeral.Get("CodigoEvento").ToString());

            DataSet ds = cDados.getDataSet(comandoSQL);
            operacaoFeita = ds.Tables[0].Rows[0][1].ToString();
            idEventoNovo = ds.Tables[0].Rows[0][0].ToString();

            msgError += incluiParticipantesSelecionados(arrayParticipantesSelecionados, idEventoNovo);

            retorno = true;
        }
        catch (Exception ex)
        {
            msgError = ex.Message;
            retorno = false;
        }

        return retorno;
    }
}