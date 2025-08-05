using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page {

    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;
    private int tipoAssociacaoParametro;
    int codigoObjetoAssociado = -1;
    string iniciaisAssociacao = "EN";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object s, EventArgs e) {

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/EnvioMensagens.js""></script>"));
        this.TH(this.TS("EnvioMensagens"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));

        tipoAssociacaoParametro = cDados.getCodigoTipoAssociacao("MG");
                
        if (Request.QueryString["CO"] != null && Request.QueryString["CO"].ToString() != "")
        {
            codigoObjetoAssociado = int.Parse(Request.QueryString["CO"].ToString());
        }

        if (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "")
            iniciaisAssociacao = Request.QueryString["TA"].ToString();

        if (Request.QueryString["CR"] != null && Request.QueryString["CR"].ToString() != "")
            incluiDestinatarioFixo(Request.QueryString["CR"].ToString());
        
        carregaComboCategorias();
        
    }

    private void incluiDestinatarioFixo(string codigoResponsavel)
    {
        btnPara.ClientVisible = false;
        string codigosParam = "-1";

        if (codigoResponsavel.Contains(';'))
        {
            foreach (string codigo in codigoResponsavel.Split(';'))
            {
                if(codigo != "")
                    codigosParam += "," + codigo;
            }
        }
        else
        {
            codigosParam += "," + codigoResponsavel;
        }

        DataSet dsUsuarios = cDados.getUsuarios(" AND u.[CodigoUsuario] IN (" + codigosParam + ")");

        if (cDados.DataSetOk(dsUsuarios))
        {
            foreach(DataRow dr in dsUsuarios.Tables[0].Rows)
                txtDestinatarios.Text += "'" + dr["NomeUsuario"].ToString() + "'< " + dr["EMail"].ToString() + " >;";
        }
    }
      
    private void carregaComboCategorias()
    {
        DataSet dsCategorias = cDados.getCategoriasMensagem(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsCategorias))
        {
            ddlCategoria.DataSource = dsCategorias;
            ddlCategoria.TextField = "DescricaoCategoria";
            ddlCategoria.ValueField = "CodigoCategoria";
            ddlCategoria.DataBind();
        }

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlCategoria.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlCategoria.SelectedIndex = 0;
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string prioridade = "NULL";

        if (btnAltaPrioridade.Checked)
            prioridade = "'A'";
        //else if (btnBaixaPrioridade.Checked)
        //    prioridade = "'B'";

        string assunto = txtAssunto.Text;

        bool retorno = false;
        string msg = "";

        if (txtDestinatarios.Text.Trim() != "")
        {
            string emails = "";

            foreach (Match item in Regex.Matches(txtDestinatarios.Text, @"< [^>]* >"))
            {
                emails += "'" + item.Value + "',";
            }

            DataSet dsUsuarios = cDados.getUsuarios(" AND EMail IN(" + emails.Replace("< ", "").Replace(" >", "") + "'NI')");

            if (cDados.DataSetOk(dsUsuarios) && cDados.DataTableOk(dsUsuarios.Tables[0]))
            {
                int[] arrayUsuarios = new int[dsUsuarios.Tables[0].Rows.Count];

                for (int i = 0; i < arrayUsuarios.Length; i++)
                    arrayUsuarios[i] = int.Parse(dsUsuarios.Tables[0].Rows[i]["CodigoUsuario"].ToString());                

                retorno = cDados.incluiMensagem(codigoEntidadeUsuarioResponsavel, codigoObjetoAssociado == -1 ? codigoEntidadeUsuarioResponsavel : codigoObjetoAssociado, codigoUsuarioResponsavel, assunto, MailEditor.Html, DateTime.MinValue, false, prioridade, int.Parse(ddlCategoria.Value.ToString()), arrayUsuarios, iniciaisAssociacao, ref msg);
            }
        }

        if (retorno)
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Mensagem enviada com sucesso!";
            callbackSalvar.JSProperties["cp_Status"] = "OK";
        }
        else
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Erro ao enviar a mensagem!" + msg;
            callbackSalvar.JSProperties["cp_Status"] = "ERR";
        }
    }
}
