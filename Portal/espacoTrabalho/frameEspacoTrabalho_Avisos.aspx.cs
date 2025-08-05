//Revisado
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class espacoTrabalho_frameEspacoTrabalho_Avisos : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioLogado;
    int codigoEntidadeUsuarioLogado;
    public string alturaTela = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        dsAvisos.ConnectionString = cDados.classeDados.getStringConexao();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // se não tem a informação abaixo... deve reiniciar o sistema.
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        // lê o código do usuário logado
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioLogado = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioLogado, codigoEntidadeUsuarioLogado, codigoEntidadeUsuarioLogado, "null", "EN", 0, "null", "EN_AcsNtc");
        }

        // aplica o efeito visual
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        // carrega a grid com todos os avisos do usuário logado
        carregaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void carregaGrid()
    {
        ncAvisos.DataSourceID = "";
        ncAvisos.DataSource = getAvisosUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioLogado, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), "");
        ncAvisos.DataBind();
    }

    public DataSet getAvisosUsuario(int codigoUsuario, int codigoEntidade, int codigoCarteira, string where)
    {
       string comandoSQL = string.Format(
            @"BEGIN
	            DECLARE @CodigoUsuario int
	            DECLARE @CodigoEntidade int
                DECLARE @CodigoCarteira int
	            SET @CodigoUsuario = {2}
	            SET @CodigoEntidade = {3}
                SET @CodigoCarteira = {4} 

                SELECT codigoAviso, Assunto, Aviso, DataInicio, DataTermino, AvisoLido, DataInclusao,
                       case when AvisoLido = 'S' then '../imagens/geral/ok.png'
                            else  '../imagens/geral/erro.png' end as ImagemAvisoLido 
                  FROM (SELECT a.CodigoAviso, a.Assunto, convert(varchar(150), aviso) as Aviso,
			                   CONVERT(Char(10), a.DataInicio, 103) AS DataInicio, 
			                   CONVERT(Char(10), a.DataTermino, 103) AS DataTermino,
			                   CASE WHEN al.DataLeitura IS NULL THEN 'N' ELSE 'S' END AS AvisoLido,
			                   a.DataInclusao
		                  FROM {0}.{1}.Aviso a INNER JOIN 
			                   {0}.{1}.AvisoDestinatario ad ON ad.CodigoAviso = a.CodigoAviso INNER JOIN
			                   {0}.{1}.usuario psu ON psu.CodigoUsuario = ad.CodigoDestinatario LEFT JOIN
			                   {0}.{1}.AvisoLido al ON al.CodigoAviso = a.CodigoAviso AND al.CodigoUsuario = @CodigoUsuario
		                 WHERE ad.tipoDestinatario = 'US'
		                   AND psu.CodigoUsuario = @CodigoUsuario
		                   AND a.CodigoEntidade = @CodigoEntidade
		                   AND GetDate() Between a.DataInicio AND a.DataTermino
		                 UNION
		                SELECT DISTINCT  a.CodigoAviso, a.Assunto, convert(varchar(150), aviso) as Aviso,
				                CONVERT(Char(10), a.DataInicio, 103) AS DataInicio, 
				                CONVERT(Char(10), a.DataTermino, 103) AS DataTermino,
				                CASE WHEN al.DataLeitura IS NULL THEN 'N' ELSE 'S' END AS AvisoLido,
				                a.DataInclusao
		                  FROM {0}.{1}.Aviso a INNER JOIN 
			                   {0}.{1}.AvisoDestinatario ad ON ad.CodigoAviso = a.CodigoAviso INNER JOIN
			                   {0}.{1}.f_GetProjetosUsuario({2}, {3}, {4}) gpr ON gpr.CodigoProjeto = ad.CodigoDestinatario LEFT JOIN
			                   {0}.{1}.AvisoLido al ON al.CodigoAviso = a.CodigoAviso  AND al.CodigoUsuario = @CodigoUsuario
		                 WHERE ad.tipoDestinatario = 'PR'
		                   AND GetDate() Between a.DataInicio AND a.DataTermino
		                   AND a.CodigoEntidade = @CodigoEntidade
		                 UNION
		                SELECT DISTINCT a.CodigoAviso, a.Assunto, convert(varchar(150), aviso) as Aviso,
				                CONVERT(Char(10), a.DataInicio, 103) AS DataInicio, 
				                CONVERT(Char(10), a.DataTermino, 103) AS DataTermino,
				                CASE WHEN al.DataLeitura IS NULL THEN 'N' ELSE 'S' END AS AvisoLido,
				                a.DataInclusao
		                  FROM {0}.{1}.Aviso a INNER JOIN 
			                   {0}.{1}.AvisoDestinatario ad ON ad.CodigoAviso = a.CodigoAviso INNER JOIN
			                   {0}.{1}.f_GetUnidadesUsuario({2},{3}) guu ON guu.CodigoUnidadeNegocio = ad.CodigoDestinatario LEFT JOIN
			                   {0}.{1}.AvisoLido al ON al.CodigoAviso = a.CodigoAviso  AND al.CodigoUsuario = @CodigoUsuario
		                 WHERE ad.tipoDestinatario = 'UN'
		                   AND GetDate() Between a.DataInicio AND a.DataTermino
		                   AND a.CodigoEntidade = @CodigoEntidade
		                UNION
		                SELECT DISTINCT  a.CodigoAviso, a.Assunto, convert(varchar(150), aviso) as Aviso,
				                CONVERT(Char(10), a.DataInicio, 103) AS DataInicio, 
				                CONVERT(Char(10), a.DataTermino, 103) AS DataTermino,
				                CASE WHEN al.DataLeitura IS NULL THEN 'N' ELSE 'S' END AS AvisoLido,
				                a.DataInclusao
		                  FROM {0}.{1}.Aviso a INNER JOIN 
			                   {0}.{1}.AvisoDestinatario ad ON ad.CodigoAviso = a.CodigoAviso LEFT JOIN
			                   {0}.{1}.AvisoLido al ON al.CodigoAviso = a.CodigoAviso  AND al.CodigoUsuario = @CodigoUsuario
		                 WHERE ad.tipoDestinatario = 'TD'
		                   --AND GetDate() Between a.DataInicio AND a.DataTermino
		                   --AND a.CodigoEntidade = @CodigoEntidade
	                ) as Avisos
                WHERE 1=1 {5}
                ORDER BY DataInclusao Desc
            END", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, where);
        return cDados.getDataSet(comandoSQL);
    }

    private void defineAlturaTela()
    {   // Calcula a altura da tela

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente") != null ? cDados.getInfoSistema("ResolucaoCliente").ToString() : "";
        if (resolucaoCliente != "")
        {
            int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
            alturaTela = (alturaPrincipal - 220) + "px";
        }
    }

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int idAviso = int.Parse(e.Parameter);
        string assunto = "";
        bool avisoLido = false;
        getAssuntoAviso(idAviso,codigoUsuarioLogado, ref avisoLido, ref assunto);
        e.Result = assunto;
        
        // se o aviso ainda não foi lido...registra que o usuário leu o aviso.
        if (!avisoLido)
        {
            atualizaAvisoLido(idAviso, codigoUsuarioLogado);
            carregaGrid();
        }
    }
    public void atualizaAvisoLido(int codigoAviso, int codigoUsuario)
    {
        int regAfetados = 0;
        string comandoSQL = string.Format(
               @"INSERT INTO {0}.{1}.avisoLido (CodigoAviso, CodigoUsuario, DataLeitura)
                        VALUES ({2}, {3}, getdate() )", cDados.getDbName(), cDados.getDbOwner(), codigoAviso, codigoUsuario);
       cDados.execSQL(comandoSQL, ref regAfetados);
    }

    public void getAssuntoAviso(int codigoAviso, int codigoUsuario, ref bool avisoLido, ref string Assunto)
    {
       string comandoSQL = string.Format(
            @"SELECT a.[Aviso], al.[DataLeitura] 
                FROM 
                    {0}.{1}.[Aviso]                     AS [a]
                        LEFT JOIN {0}.{1}.[AvisoLido]   AS [al] ON 
                            ( [a].[CodigoAviso] = [al].[CodigoAviso] 
                                AND al.[CodigoUsuario] = {3} )
                WHERE a.[CodigoAviso] = {2}  ", cDados.getDbName(), cDados.getDbOwner(), codigoAviso, codigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            avisoLido = (ds.Tables[0].Rows[0]["DataLeitura"] != null && ds.Tables[0].Rows[0]["DataLeitura"].ToString() != "");
            Assunto = ds.Tables[0].Rows[0]["Aviso"].ToString();
        }
        else
        {
            avisoLido = false;
        }
    }
}
