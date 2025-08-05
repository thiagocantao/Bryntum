using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TelasClientes_SENAR_PlanejamentoMensal : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    public string parametrosURL;
    public string alturaTela1, alturaTela2;
    private int codigoWorkflow = 0;
    private int codigoInstanciaWf = 0;

    private string resolucaoCliente = "";
    private string alturaFrames = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "")
            codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

        parametrosURL = "?" + Request.QueryString.ToString();
        defineAlturaTela();

        bool exibirColunas = false;
        if (!string.IsNullOrEmpty(Request.QueryString["exibirColunas"]))
            exibirColunas = Request.QueryString["exibirColunas"].ToLower() == "s";

        string comandoSQL;
        if (exibirColunas)
        {
            comandoSQL = string.Format(@"
        BEGIN 
          DECLARE @in_CodigoWorkflow   Int,
                  @in_CodigoInstanciaWF  BigInt,
                  @l_ValorAcoes      Decimal(25,4),
                  @l_ValorOutrasDespesas Decimal(25,4),
                  @l_ValorTotal      Decimal(25,4)
  
    
          SET @in_CodigoWorkflow = {0}        
          SET @in_CodigoInstanciaWF = {1}
  
          SELECT @l_ValorAcoes = Sum(IsNull(ValorUnitario,0) * IsNull(Quantidade,0))
            FROM SENAR_AcaoPlanejamentoABC AS ap
           WHERE ap.CodigoWorkflowPC = @in_CodigoWorkflow
             AND ap.CodigoInstanciaPC = @in_CodigoInstanciaWF
     
          SELECT @l_ValorOutrasDespesas = Sum(IsNull(ValorUnitario,0) * IsNull(Quantidade,0))
            FROM SENAR_OutrasDespesasPlanejamentoABC AS o
           WHERE o.CodigoWorkflowPC = @in_CodigoWorkflow
             AND o.CodigoInstanciaPC = @in_CodigoInstanciaWF
  
          IF @l_ValorAcoes IS NULL
             SET @l_ValorAcoes = 0
  
          IF @l_ValorOutrasDespesas IS NULL
             SET @l_ValorOutrasDespesas = 0      
     
          SET @l_ValorTotal = @l_ValorAcoes + @l_ValorOutrasDespesas
  
          SELECT @l_ValorAcoes AS ValorAcoes,
                 @l_ValorOutrasDespesas AS OutrasDespesas,
                 @l_ValorTotal AS Total   
        END", codigoWorkflow, codigoInstanciaWf);
        }
        else
        {
            comandoSQL = string.Format(@"
        BEGIN 
          DECLARE @in_CodigoWorkflow   Int,
                  @in_CodigoInstanciaWF  BigInt,
                  @l_ValorAcoes      Decimal(25,4),
                  @l_ValorOutrasDespesas Decimal(25,4),
                  @l_ValorTotal      Decimal(25,4)
  
    
          SET @in_CodigoWorkflow = {0}        
          SET @in_CodigoInstanciaWF = {1}
  
          SELECT @l_ValorAcoes = Sum(IsNull(ValorUnitario,0) * IsNull(Quantidade,0))
            FROM SENAR_AcaoPlanejamentoABC AS ap
           WHERE ap.CodigoWorkflow = @in_CodigoWorkflow
             AND ap.CodigoInstanciaWF = @in_CodigoInstanciaWF
     
          SELECT @l_ValorOutrasDespesas = Sum(IsNull(ValorUnitario,0) * IsNull(Quantidade,0))
            FROM SENAR_OutrasDespesasPlanejamentoABC AS o
           WHERE o.CodigoWorkflow = @in_CodigoWorkflow
             AND o.CodigoInstanciaWF = @in_CodigoInstanciaWF
  
          IF @l_ValorAcoes IS NULL
             SET @l_ValorAcoes = 0
  
          IF @l_ValorOutrasDespesas IS NULL
             SET @l_ValorOutrasDespesas = 0      
     
          SET @l_ValorTotal = @l_ValorAcoes + @l_ValorOutrasDespesas
  
          SELECT @l_ValorAcoes AS ValorAcoes,
                 @l_ValorOutrasDespesas AS OutrasDespesas,
                 @l_ValorTotal AS Total   
        END", codigoWorkflow, codigoInstanciaWf);
        }

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtValor.Value = ds.Tables[0].Rows[0]["Total"];
        }
    }

    private void defineAlturaTela()
    {
        alturaTela1 = (Math.Max(int.Parse(Request.QueryString["ALT"].ToString())/2 + 25, 300)) + "px";
        alturaTela2 = (int.Parse(Request.QueryString["ALT"].ToString()) / 2 - 25) + "px";
    }
}