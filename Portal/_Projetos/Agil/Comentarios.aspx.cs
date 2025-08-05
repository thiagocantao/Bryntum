using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Agil_Comentarios : System.Web.UI.Page
{
    private dados cDados;
    public int codigoEntidade;
    private int codigoProjeto;
    public int idUsuarioLogado;
    private string concatenaInfo = "";
    private string tituloItem = "";
    private string codigoItem = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        if (Session["CodigoProjeto"] != null && Session["CodigoProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Session["CodigoProjeto"].ToString());
        }
        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));


        Session["co_del"] = null;
        Session["ce"] = codigoEntidade;
        Session["cu"] = idUsuarioLogado;
        sdsComentario.ConnectionString = cDados.ConnectionString;
        
        if (!IsPostBack)
        {
            ListEditSelectionMode selectionMode = (ListEditSelectionMode)Enum.Parse(typeof(ListEditSelectionMode), "CheckColumn");
            var listaEquipe = (ASPxListBox)ASPxDropDownEdit.FindControl("checkListBox");
            listaEquipe.FilteringSettings.CustomEditorID = "";
            listaEquipe.SelectAllText = "Toda Equipe";
            listaEquipe.SelectionMode = selectionMode;
            listaEquipe.EnableSelectAll = true;
            listaEquipe.EnableCallbackMode = false;
            carregaListaEquipe();
            configuraImpedimentoNamensagem();

        }
    }

    private void configuraImpedimentoNamensagem()
    {
        btnSalvarComentario.ClientVisible = false;
        mostraBotaoFechar.Style.Add("display", "none");
        callbackPanel.JSProperties["cpFecharAoSalvar"] = "";
        if (Request.QueryString["bloqueio"] + "" != "")
        {
            if (Request.QueryString["co"] + "" != "")
            {
                codigoItem = Request.QueryString["co"] + "";
                callbackPanel.JSProperties["cpFecharAoSalvar"] = "S";
                callbackPanel.JSProperties["cpCodigo"] = codigoItem;
                mostraBotaoFechar.Style.Add("display", "block");
                btnSalvarComentario.ClientVisible = true;
                btnSalvarComentario.Text = ((Request.QueryString["bloqueio"] == "S") ?  "Registrar" : "Remover") + " impedimento e associar comentário";
            }
        }
    }

    protected void listaEquipe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoUsuario = e.Parameter.ToString();
    }

    private void carregaListaEquipe()
    {

        DataSet ds = cDados.getUsuariosRecursoProjetoAgil(codigoProjeto);
        var listaEquipe = (ASPxListBox)ASPxDropDownEdit.FindControl("checkListBox");
        listaEquipe.DataSource = ds.Tables[0].Select("", "NomeRecurso ASC").CopyToDataTable();
        listaEquipe.DataBind();

        if (listaEquipe.Items.Count > 0)
        {
            listaEquipe.SelectedIndex = -1;
        }
    }

    protected void hfItensSelecionados_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        var listaEquipe = (ASPxListBox)ASPxDropDownEdit.FindControl("checkListBox");
        FilterItens(listaEquipe.Items);
    }
    protected void FilterItens(ListEditItemCollection items)
    {
        StringBuilder sbNomesItensSelecionados = new StringBuilder("");
        StringBuilder sbEmailsItensSelecionados = new StringBuilder("");
        hfItensSelecionados.Set("emailsItensSelecionados", "");
        hfItensSelecionados.Set("nomesItensSelecionados", "");
        hfItensSelecionados.Set("todaEquipe", false);
        bool existeItemNaoSelecionado = false;
        foreach (ListEditItem item in items)
        {
            if (item.Selected)
            {
                sbNomesItensSelecionados.Append(item.Text).Append(", ");
                sbEmailsItensSelecionados.Append(item.Value).Append(", ");
            }
            else
                existeItemNaoSelecionado = true;
        }
        hfItensSelecionados.Set("todaEquipe", !existeItemNaoSelecionado);
        var emailsItensSelecionados = sbEmailsItensSelecionados.ToString();
        if (!emailsItensSelecionados.Equals(""))
        {
            hfItensSelecionados.Set("emailsItensSelecionados", emailsItensSelecionados.Substring(0, emailsItensSelecionados.Length - 2));
            hfItensSelecionados.Set("nomesItensSelecionados", sbNomesItensSelecionados.ToString().Substring(0, sbNomesItensSelecionados.ToString().Length - 2));
        }
    }

    protected void callbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "";

        string parameter = e.Parameter;
        int result;
        int codigoComentarioExclusao = -1;

        if (parameter == "ins")
        {
            
            var destinatarios = "";

            //Se veio do link Registrar impedimento, não deve utilizar as variáveis de sessão
            if (Request.QueryString["bloqueio"] + "" != "")
            {
                preencheInformacoesItem();
            }
            else
            {
                codigoItem = Session["CodigoItem"] != null && Session["CodigoItem"].ToString() != "" ? Session["CodigoItem"].ToString() : "";
                tituloItem = Session["TituloItem"] != null && Session["TituloItem"].ToString() != "" ? Session["TituloItem"].ToString() : "";
            }
            
            var emails = hfItensSelecionados.Get("emailsItensSelecionados").ToString().Split(',').Where(x => x.Trim() != "");
            var linkSistema = "";

            DataSet ds = cDados.getLinkSistemaAcessoExterno(codigoEntidade);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Valor"] != null)
                linkSistema = ds.Tables[0].Rows[0]["Valor"].ToString();

            string nomeRemetente = "";
            if (Session["NomeUsuario"] == null || Session["NomeUsuario"].ToString() == "")
            {
                DataSet dsUsuario = cDados.getUsuarioDaEntidadeAtiva(codigoEntidade.ToString(), " AND u.CodigoUsuario = " + idUsuarioLogado);
                if (cDados.DataSetOk(dsUsuario))
                {
                    foreach (DataRow dr in dsUsuario.Tables[0].Rows)
                    {
                        nomeRemetente = dr["NomeUsuario"].ToString();
                    }
                }
            }
            else
                nomeRemetente = Session["NomeUsuario"].ToString();

            foreach (var email in emails)
                enviarEmail(email,nomeRemetente, codigoItem, tituloItem, htmlComentario.Html,string.Format("<a href='{0}' target='_blank'>{0}</a>", linkSistema));

            var emailsTodaEquipe = bool.Parse(hfItensSelecionados.Get("todaEquipe").ToString());
            if (emails != null && emails.Count() > 0) {
                destinatarios = emailsTodaEquipe ? " enviado para toda a equipe." : string.Format(" enviado para: {0}", hfItensSelecionados.Get("nomesItensSelecionados").ToString());
            }            
            sdsComentario.InsertParameters["DestinatariosComentario"].DefaultValue = destinatarios;

            sdsComentario.InsertParameters["DetalheComentario"].DefaultValue = (Request.QueryString["bloqueio"] + "" != "") ? (concatenaInfo + htmlComentario.Html) : htmlComentario.Html;

            try
            {
                result = sdsComentario.Insert();
            }
            catch(Exception ex)
            {
                ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = ex.Message;
            }
            
        }
        else if(int.TryParse(parameter, out codigoComentarioExclusao))
        {
            Session["co_del"] = codigoComentarioExclusao;
            result = sdsComentario.Delete();
        }
        repeater.DataBind();
    }

    private void preencheInformacoesItem()
    {

        string comandoInfoItem = string.Format(@"SELECT indicabloqueioitem, tituloitem
                                                  FROM agil_itembacklog
                                                 WHERE CodigoItem = {0}", Request.QueryString["co"] + "");
        DataSet ds = cDados.getDataSet(comandoInfoItem);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0][0].ToString().Trim().ToUpper() == "N")
            {
                concatenaInfo = "Comentário relacionado a um impedimento <p>";
            }
            else
            {
                concatenaInfo = "Comentário relacionado a remoção de um impedimento <p>";
            }

            tituloItem = ds.Tables[0].Rows[0]["TituloItem"].ToString();
            codigoItem = Request.QueryString["co"] + "";
        }
    }

    public string setlabelDataInclusaoAlteracao(object indicaComentarioEditado, object dataInclusao, object dataUltimaAlteracao )
    {
        string retorno = dataInclusao.ToString();
        if (indicaComentarioEditado.ToString() == "Sim")
        {
            retorno = dataUltimaAlteracao +  " (editado)";
        }
        return retorno;
    }

    private bool enviarEmail(string emailDestinatario,string nomeRemetente, string codigoItem, string tituloItem, string comentario, string linkSistema)
    {       
        string assunto = string.Format("{0} - {1}", codigoItem, tituloItem);
        string corpoEmail = string.Format("{0} enviou a você o comentário a seguir referente ao item {1} - {2}:<br><br> {3} <br><br> Por favor, acesse o BRISKPPM pelo endereço {4} para dar os encaminhamentos necessários.", nomeRemetente, codigoItem, tituloItem, comentario, linkSistema);

        int retornoStatus = 0;
        string emailEnviado = cDados.enviarEmail(assunto, emailDestinatario, "", corpoEmail, "", "", ref retornoStatus);

        return retornoStatus != 0;
    }

    public string constroiLixeira(string codigoComentario, string CodigoUsuarioMensagem)
    {
        string retorno = "";
        if(idUsuarioLogado == int.Parse(CodigoUsuarioMensagem))
        {
            retorno = string.Format(@"
                 <div>
                      <span id = ""spandelete"" style=""cursor:pointer"" title=""Excluir comentário"" onclick=""excluirComentario({0},  {1} , {2})"">
                            <img src = ""../../imagens/foruns/excluirComentario.png"" />
                      </span>
                </div>", codigoComentario, codigoEntidade, idUsuarioLogado);

        }
        return retorno;
    }
    public string constroiIconeEdicao(string codigoComentario, string CodigoUsuarioMensagem)
    {
        string retorno = "";
        retorno = string.Format(@"
            <div style=""padding-right:5px"">
                      <span id = ""spanedicao"" style=""cursor:pointer"" title=""Excluir comentário"" onclick=""editarComentario({0},  {1} , {2})"">
                            <img src = ""../../imagens/botoes/editarReg02.PNG"" />
                      </span>
           </div>", codigoComentario, codigoEntidade, idUsuarioLogado);
        return retorno;
    }
}