using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Agil_atualizaComentario : System.Web.UI.Page
{
    public int codigoComentario;
    public int codigoEntidade;
    public int codigoUsuario;
    private dados cDados;
    protected void Page_Load(object sender, EventArgs e)
    {
        //cc=' + codigocomentario + '&ce=' + codigoentidade + '&u=' + codigousuario
        cDados = CdadosUtil.GetCdados(null);
        bool retorno = int.TryParse(Request.QueryString["cc"] + "", out codigoComentario);
        retorno = int.TryParse(Request.QueryString["ce"] + "", out codigoEntidade);
        retorno = int.TryParse(Request.QueryString["u"] + "", out codigoUsuario);

        cDados.aplicaEstiloVisual(this.Page);

        if (!Page.IsPostBack)
        {
            carregaDadosHtmlEditor();
        }      

    }

    private void carregaDadosHtmlEditor()
    {
        string sqlComentario = string.Format(@"

declare @l_CodigoTipoAssociacaoObjeto as int
declare @l_CodigoTipoAssociacaoComentario as int
declare @l_CodigoTipoLink as int


SET @l_CodigoTipoAssociacaoObjeto			= [dbo].[f_GetCodigoTipoAssociacao]('CN');
            SET @l_CodigoTipoAssociacaoComentario		= [dbo].[f_GetCodigoTipoAssociacao]('CN')
            SET @l_CodigoTipoLink						= [dbo].[f_GetCodigoTipoLinkObjeto]('AS');
        
            SELECT co.DetalheComentario  AS Comentario,  
                   co.CodigoComentario                   
             FROM ComentarioObjeto AS co 
             INNER JOIN LinkObjeto AS lo ON (co.CodigoComentario = lo.CodigoObjetoLink  
                                             AND lo.CodigoTipoObjetoLink = @l_CodigoTipoAssociacaoComentario  
                                             /*AND lo.CodigoTipoObjeto = @l_CodigoTipoAssociacaoObjeto  
                                             AND lo.CodigoObjeto = {0}*/) 
             INNER JOIN Usuario AS u ON (u.CodigoUsuario = co.CodigoUsuario)
            WHERE co.CodigoComentario = {0} ", codigoComentario);
        DataSet ds = cDados.getDataSet(sqlComentario);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            htmlEditaComentario.Html = ds.Tables[0].Rows[0]["Comentario"].ToString();
        }
    }

    protected void callbackTela_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        int regAfetados = 0;
        bool retorno = false;

        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @CodigoComentario int
        DECLARE @DetalheComentario varchar(max)

        SET @CodigoComentario = {0}
        SET @DetalheComentario = '{1}'

        EXECUTE @RC = [dbo].[p_AlteraComentarioObjeto] 
           @CodigoComentario
          ,@DetalheComentario", codigoComentario, htmlEditaComentario.Html);

        
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if(retorno == true)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Comentário atualizado com sucesso!";
            }
            
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }

    }
}