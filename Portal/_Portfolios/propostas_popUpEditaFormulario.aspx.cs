﻿using System;
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
using System.Collections.Generic;
using System.Drawing;
using CDIS;

public partial class _Portfolios_propostas_popUpEditaFormulario : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    string CssFilePath = "";
    string CssPostfix = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref CssFilePath, ref CssPostfix);

        int codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());

        int? codigoFormulario = null;
        if (Request.QueryString["CF"]!=null)
            codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());

        int? codigoProjeto = null;
        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        ASPxHiddenField hf = new ASPxHiddenField();
        Hashtable parametros = new Hashtable();
        Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoModeloFormulario, new Unit("100%"), new Unit("600px"), false, this.Page, parametros, ref hf,false);
        
        // se for o formulario de propostas
        if (codigoModeloFormulario == getCodigoModeloFormulario("PROP"))
        {
            myForm.AntesSalvar += new Formulario.AntesSalvarEventHandler(myForm_AntesSalvarFormularioProposta);
            myForm.AposSalvar += new Formulario.AposSalvarEventHandler(myForm_AposSalvarFormularioProposta);
        }

        Control controlesForm = myForm.constroiInterfaceFormulario(true, IsPostBack, codigoFormulario, codigoProjeto,  CssFilePath, CssPostfix);
            
        form1.Controls.Add(controlesForm);
    }

    void myForm_AntesSalvarFormularioProposta(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        // o pré-evento Salvar só pode ser executado para a inclusão da proposta
        // =====================================================================
        if (e.operacaoInclusaoEdicao == 'I')
        {
            string nomeProjeto = "";
            if (e.camposControladoSistema != null)
            {
                for (int i = 0; i < e.camposControladoSistema.Count; i++)
                {
                    object[] Controles = e.camposControladoSistema[i];
                    if (Controles[0].ToString() == "DESC")
                    {
                        nomeProjeto = (Controles[2] as ASPxTextBox).Text;
                        // chama a procedure para incluir o projeto;
                        string comandoSQL = string.Format(
                            @"BEGIN
                            DECLARE @CodigoProjeto int

                            EXEC @CodigoProjeto = [dbo].[p_InsereProposta] '{0}', {1}, {2} 

                            SELECT @CodigoProjeto

                          END", nomeProjeto, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel);
                        DataSet ds = cDados.getDataSet(comandoSQL);
                        e.codigoProjeto = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
            }
        }
    }

    void myForm_AposSalvarFormularioProposta(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        // o pós-evento Salvar só pode ser executado para a inclusão da proposta
        // =====================================================================
        if (e.operacaoInclusaoEdicao == 'I')
        {

        }
    }

    private int getCodigoModeloFormulario(string iniciaisModeloFormulario)
    {
        int codigoModeloFormulario = 0;
        // busca o modelo do formulário de propostas
        string comandoSQL = string.Format("Select codigoModeloFormulario from modeloFormulario where IniciaisFormularioControladoSistema = '{0}'", iniciaisModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            DataTable dt = ds.Tables[0];
            if (cDados.DataTableOk(dt))
            {
                codigoModeloFormulario = int.Parse(dt.Rows[0]["codigoModeloFormulario"].ToString());
            }
        }
        return codigoModeloFormulario;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["_ClosePopUp_"] != null)
        {
            this.ClientScript.RegisterClientScriptBlock(GetType(), "close",
                        @"<script type='text/javascript'>
                            window.top.retornoModal = 'OK';
                            window.top.fechaModal();
                        
                         </script>");

            Session.Remove("_CodigoFormularioMaster_");
            Session.Remove("_CodigoToDoList_");
            Session.Remove("_ClosePopUp_");
        }
    }
}
