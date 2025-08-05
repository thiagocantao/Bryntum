using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_DetalhesItemSprint : System.Web.UI.Page
{
    dados cDados;
    private int codigoItem;
    string codigoStatus = "-1";

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        cDados.setaTamanhoMaximoMemo(txtComentarios, 2000, lblContadorMemoComentarios);

        codigoStatus = Request.QueryString["CS"].ToString();

        if (Request.QueryString["CI"] != null)
            codigoItem = int.Parse(Request.QueryString["CI"].ToString());

        if (!IsPostBack)
            carregaCampos();

        cDados.aplicaEstiloVisual(Page);
    }

    private void carregaCampos()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoMovimento INT

            SELECT @CodigoMovimento = MAX(CodigoMovimentoItem)
                FROM Agil_MovimentoItem
                WHERE CodigoItem = {0}
                AND CodigoNovoStatusItem = {1}
        
            SELECT ib.TituloItem,
                   ib.DetalheItem,
                   ib.Importancia,
                   ib.EsforcoPrevisto,
                   tci.DescricaoTipoClassificacaoItem,
                   mi.Comentario,
                   mi.EsforcoReal,
                   ib.CodigoUsuarioResponsavel, 
                   ib.CodigoProjeto
              FROM Agil_ItemBacklog AS ib INNER JOIN
                   Agil_TipoClassificacaoItemBacklog AS tci ON (tci.CodigoTipoClassificacaoItem = ib.CodigoTipoClassificacaoItem) LEFT JOIN
                   Agil_MovimentoItem mi ON mi.CodigoItem = ib.CodigoItem AND CodigoMovimentoItem = @CodigoMovimento                                
             WHERE ib.CodigoItem = {0}
            END", codigoItem, codigoStatus);

        DataSet ds = cDados.getDataSet(comandoSQL);
        int codigoUsuarioResponsavelItem = -1;
        int codigoProjeto = -1;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtTituloItem.Text = dr["TituloItem"].ToString();
            txtDescricaoTipoClassificacaoItem.Text = dr["DescricaoTipoClassificacaoItem"].ToString();
            txtImportancia.Value = dr["Importancia"];
            txtEsforcoPrevisto.Value = dr["EsforcoPrevisto"];
            mmDescricaoItem.Text = dr["DetalheItem"].ToString();
            txtComentarios.Text = dr["Comentario"].ToString();
            txtEsforcoReal.Value = dr["EsforcoReal"];
            codigoUsuarioResponsavelItem = dr["CodigoUsuarioResponsavel"].ToString() == "" ? -1 : int.Parse(dr["CodigoUsuarioResponsavel"].ToString());      
            codigoProjeto = dr["CodigoProjeto"].ToString() == "" ? -1 : int.Parse(dr["CodigoProjeto"].ToString());  
        }

         int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
         bool podeEditar = usuarioEquipe(codigoUsuarioLogado);

        btnSalvar.ClientVisible = podeEditar;
        txtEsforcoReal.ClientEnabled = podeEditar;
        txtComentarios.ClientEnabled = podeEditar;
        tabControl.JSProperties["cp_PodeEditar"] = podeEditar ? "S" : "N";

        tabControl.JSProperties["cp_Anexo"] = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=IB&ID=" + codigoItem + "&RO=N&TO=Editar&ALT=410";
        tabControl.JSProperties["cp_Comentarios"] = "./HistoricoComentarios.aspx?CI=" + codigoItem + "&ALT=254";
    }

    private bool usuarioEquipe(int codigoUsuario)
    {
        bool usuarioDaEquipe = false;

        string comandoSQL = string.Format(@"
        BEGIN
	            DECLARE @CodigoIteracao Int       
	
	            SELECT @CodigoIteracao = CodigoIteracao
		          FROM Agil_Iteracao
	             WHERE CodigoProjetoIteracao = {0}

                SELECT 1
                  FROM Agil_RecursoIteracao ri INNER JOIN
			           vi_RecursoCorporativo rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo 
								               AND rc.CodigoRecurso = {1}
                 WHERE ri.CodigoIteracao = @CodigoIteracao
        END", Request.QueryString["CP"], codigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);

        usuarioDaEquipe = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]));

        return usuarioDaEquipe;
    }

    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        callbackSalvar.JSProperties["cp_Msg"] = "";
        callbackSalvar.JSProperties["cp_Erro"] = "";
        string comandoSQL = string.Format(@"
       BEGIN
        DECLARE @CodigoMovimento INT

        SELECT @CodigoMovimento = MAX(CodigoMovimentoItem)
          FROM Agil_MovimentoItem
         WHERE CodigoItem = {2}
           AND CodigoNovoStatusItem = {3}

        UPDATE Agil_MovimentoItem 
           SET Comentario = '{0}'
              ,EsforcoReal = {1}
         WHERE CodigoMovimentoItem = @CodigoMovimento
       END"
            , txtComentarios.Text.Replace("'", "''")
            , txtEsforcoReal.Text == "" ? "NULL" : txtEsforcoReal.Text.Replace(",", ".")
            , codigoItem
            , codigoStatus);

        int regAf = 0;

        bool retorno = cDados.execSQL(comandoSQL, ref regAf);

        if (retorno)
            callbackSalvar.JSProperties["cp_Msg"] = "Dados salvos com sucesso!";
        else
            callbackSalvar.JSProperties["cp_Erro"] = "Erro ao salvar dados!";
    }
}