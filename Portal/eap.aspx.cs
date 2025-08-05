using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;

public partial class eap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cbEAP_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string ComandoSQL = @" 
                                BEGIN 
                                        DECLARE @DataParam DateTime,
                                                @CodigoProjeto int,
                                                @CodigoRecurso int
    	
                                        SET @DataParam = CONVERT(DateTime, getdate(), 103)
                                        SET @CodigoProjeto = 2217 -- 2207, 806, 2217   
                                        SET @CodigoRecurso = -1
    	
                                        SELECT    CodigoProjeto,  CAST(SUBSTRING(CodigoTarefa,9,6) as Int) AS CodigoTarefa,  CAST(SUBSTRING(TarefaSuperior,9,6) as Int) AS TarefaSuperior,
                                                   NomeTarefa,   TerminoPrevisto,   Termino,   TerminoReal,   PercentualPrevisto,   PercentualReal,   CustoPrevisto,   CustoReal,   Nivel,
                                                   InicioPrevisto,   Inicio,   InicioReal,   Marco,   TarefaResumo,   Custo,   CustoHoraExtra,   Trabalho,   TrabalhoHE,   TrabalhoReal,
                                                   TrabalhoRealHoraExtra,   Duracao,   DuracaoReal,   EAC,   TrabalhoPrevisto,   DuracaoPrevista,   VarCusto,   CustoRestante,   VarDuracao,
                                                   DuracaoRestante,   VarTrabalho,   TrabalhoRestante,   VarInicio,   VarTermino,   IndicaCritica,   DataParametro,   PossuiAnotacoes,
                                                   SubProjeto,   CodigoNumeroTarefa,   EstruturaHierarquica,   Predecessoras,   SequenciaTarefaCronograma,   StringAlocacaoRecursoTarefa,
                                                   UnidadeDuracao,   StatusTarefa,   DesvioPrazo,   IniciaisTipoTarefa
                                        FROM dbo.f_GetCronogramaProjeto(@CodigoProjeto, @DataParam, @CodigoRecurso)    
                                        WHERE 1 = 1 
                                --        AND CodigoTarefa = 'P001611.000000'
                                        ORDER BY 1
                                END
                            ";
        dados rs = new dados(null);
        DataSet ds = rs.getDataSet(ComandoSQL);

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        var resultado = ds.Tables[0].AsEnumerable().Select(r => new
        {
            key = r["CodigoTarefa"],
            tarefaPai = r["TarefaSuperior"],
            NomeTarefa = r["NomeTarefa"],
            dtInicio = String.Format("{0:d}", r["InicioReal"]),
            dtFim = String.Format("{0:d}", r["TerminoReal"])
        }).ToArray();

        e.Result = string.Format("({0})", jsSerializer.Serialize(resultado));
    }
  
}