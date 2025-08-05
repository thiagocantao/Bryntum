/*
 OBSERVAÇÕES
 
 06/01/2011 - MUDANÇA by ALEJANDRO.
 Control do menú:   Tempo Escopo / Editar EAP
                    Control de acceso, segundo si tem o não permiso para editar o cronograma.
                    Alteração no método-> private void carregaMenuTempoEscopo(DataSet ds){...}
 */
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

public partial class _Projetos_DadosProjeto_indexResumoProjeto : System.Web.UI.Page
{
    public dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
        {
            string nomeProjeto = Request.QueryString["NomeProjeto"].ToString();
            lblTituloTela.Text = nomeProjeto + " - Resumo Projeto";            
        }


        if (Request.QueryString["Mensagem"] != null && Request.QueryString["Mensagem"].ToString() != "")
        {

            lblMensagem.Text = Server.UrlDecode(Request.QueryString["Mensagem"].ToString());           
        }

        cDados.aplicaEstiloVisual(this);
        
    }   
}

