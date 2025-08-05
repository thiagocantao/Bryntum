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
using System.Drawing;

public partial class espacoTrabalho_frameEspacoTrabalho_AprovacoesTarefas : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        gvDados.TotalSummary.Clear();
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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/aprovacaoTSProjetos.js""></script>"));

        ASPxButton1.ClientEnabled = true;
        ASPxButton2.ClientEnabled = true;
        ddlAcao.ClientEnabled = true;

        defineAlturaTela();

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        cDados.aplicaEstiloVisual(Page);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaComboRecursos();

        carregaGrid();

    }

    private void carregaGrid()
    {
        gvDados.Columns.Clear();

        int codigoRecurso = -1;

        if (ddlRecurso.SelectedIndex != -1)
            codigoRecurso = int.Parse(ddlRecurso.Value.ToString());

        DataSet ds = cDados.getTimeSheetProjetoAprovacao(idUsuarioLogado, codigoRecurso, -1, codigoEntidade);
        
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            if(!IsPostBack)
                defineConfiguracoesGrid();            
        }
        else
        {            
            ASPxButton1.ClientEnabled = false;
            ASPxButton2.ClientEnabled = false;
            ddlAcao.ClientEnabled = false;
        }
    }

    private void defineConfiguracoesGrid()
    {
        if (gvDados.Columns.Count > 0)
        {
            GridViewCommandColumn cmd = new GridViewCommandColumn(" ");
            
            cmd.Width = 40;

            cmd.ShowSelectCheckbox = true;
                        
            cmd.VisibleIndex = 0;

            cmd.Caption = @"<input id=""Checkbox1"" onclick=""selecionaTodos(this.checked);"" type=""checkbox"" />";

            cmd.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            gvDados.Columns.Insert(0, cmd);
            gvDados.Columns[0].VisibleIndex = 0;

            gvDados.Columns["CodigoUsuario"].Visible = false;
            gvDados.Columns["CodigoProjeto"].Visible = false;
            gvDados.Columns["CodigoTipoTarefaTimeSheet"].Visible = false;
            gvDados.Columns["EstruturaHierarquica"].Visible = false;

            gvDados.Columns["NomeUsuario"].Width = 200;
            gvDados.Columns["NomeProjeto"].Width = 200;
            gvDados.Columns["Descricao"].Width = 180;

            gvDados.Columns["NomeUsuario"].VisibleIndex = 1;
            gvDados.Columns["NomeProjeto"].VisibleIndex = 2;
            gvDados.Columns["Descricao"].VisibleIndex = 3;

            gvDados.Columns["NomeUsuario"].Caption = "Recurso";
            gvDados.Columns["NomeProjeto"].Caption = "Ação";
            gvDados.Columns["Descricao"].Caption = "Tipo de Atividade";
            
            for (int i = 8; i < gvDados.Columns.Count; i++)
            {
                gvDados.Columns[i].Width = 105;
                gvDados.Columns[i].VisibleIndex = i-4;
                gvDados.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataTextColumn)gvDados.Columns[i]).PropertiesTextEdit.DisplayFormatString = "{0:n1}";
                ASPxSummaryItem smItem = new ASPxSummaryItem(((GridViewDataTextColumn)gvDados.Columns[i]).FieldName, DevExpress.Data.SummaryItemType.Sum);
                smItem.DisplayFormat = "{0:n1}";
                gvDados.TotalSummary.Add(smItem);
            }

        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 165;
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";
        callBack.JSProperties["cp_MSG"] = "";

        bool retorno;
        string msgErro = "";
        int codigoRecurso;
        carregaGrid();

        if (e.Parameter.ToString() == "A")
        {
            string status = ddlAcao.Value.ToString();            

            int[] codigosRecursos = new int[gvDados.Selection.Count];
            int[] codigosTiposTarefas = new int[gvDados.Selection.Count];
            int[] codigosProjetos = new int[gvDados.Selection.Count];

            for (int i = 0; i < gvDados.Selection.Count; i++)
            {
                codigosRecursos[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoUsuario")[i].ToString());
                codigosTiposTarefas[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoTipoTarefaTimeSheet")[i].ToString());
                codigosProjetos[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoProjeto")[i].ToString());
            }

            if (gvDados.Selection.Count > 0)
            {
                retorno = cDados.atualizaLinhasAprovacaoTSProjetos(codigosProjetos, codigosRecursos, codigosTiposTarefas, idUsuarioLogado, status, ref msgErro);
                
                if(retorno)
                    callBack.JSProperties["cp_OK"] = "Status Alterado com Sucesso!";
                else
                    callBack.JSProperties["cp_Erro"] = "Erro ao Alterar Status!";
            }
            else
            {
                callBack.JSProperties["cp_MSG"] = "Nenhuma Linha foi Selecionada!";
            }


        }
        else
        {
            if (e.Parameter.ToString() == "P") // && ddlRecurso.Items.Count > 0)
            {
                for (int i = 0; i < gvDados.Selection.Count; i++)
                {
                    codigoRecurso = int.Parse(gvDados.GetSelectedFieldValues("CodigoUsuario")[i].ToString());

                    retorno = cDados.publicaAprovacaoTSProjetos(codigoRecurso, idUsuarioLogado, codigoEntidade, ref msgErro);

                    if (retorno)
                        callBack.JSProperties["cp_OK"] = "Tarefas Publicadas com Sucesso!";
                    else
                        callBack.JSProperties["cp_OK"] = "Erro ao Publicar Tarefas!";
                }
            }
        }
    }      

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {        
        gvDados.Selection.UnselectAll();
        gvDados.Columns.Clear();
        gvDados.AutoGenerateColumns = true;
        carregaGrid();
        defineConfiguracoesGrid();
        gvDados.Selection.UnselectAll();

        
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {       

        if (e.DataColumn.VisibleIndex > 3 && e.CellValue.ToString() != "")
        {
            e.Cell.Text = numeroEmHoras(e.CellValue.ToString());

            int codigoTipoTarefa = int.Parse(e.KeyValue.ToString().Split('.')[2]);
            int codigoProjeto = int.Parse(e.KeyValue.ToString().Split('.')[1]);
            int codigoRecurso = int.Parse(e.KeyValue.ToString().Split('.')[0]);

            DataSet dsStatusTarefa = cDados.getStatusHoraRecursoProjeto(codigoProjeto, codigoTipoTarefa, codigoRecurso, codigoEntidade, e.DataColumn.FieldName.ToString(), "");

            string statusTarefa = "", comentarios = "", comentariosRecurso = "";

            if (cDados.DataSetOk(dsStatusTarefa) && cDados.DataTableOk(dsStatusTarefa.Tables[0]))
            {
                statusTarefa = dsStatusTarefa.Tables[0].Rows[0]["StatusTimesheet"].ToString();
                comentarios = dsStatusTarefa.Tables[0].Rows[0]["ComentariosAprovador"].ToString();
                comentariosRecurso = dsStatusTarefa.Tables[0].Rows[0]["ComentariosRecurso"].ToString();
            }

            e.Cell.Style.Add("background-image", "url(../imagens/botoes/tarefas" + statusTarefa + "_pequeno.png);");
            e.Cell.Style.Add("background-repeat", "no-repeat;");
            //e.Cell.Style.Add("background-attachment", "fixed;");
            e.Cell.Style.Add("background-position", "center;");

            comentarios = comentarios.Replace("'", "\\'").Replace(Environment.NewLine, "¥");
            comentariosRecurso = comentariosRecurso.Replace("'", "\\'").Replace(Environment.NewLine, "¥");


            /*
            * Este trecho de código foi modificado em virtude de haver resolvido o problema de quando um comentário fosse feito
            * o mesmo não era mostrado, este problema foi resolvido com sucesso. Este problema foi resolvido juntamente com
            * Amauri.
            */
            comentarios = comentarios.Replace("'", "\\'").Replace(Environment.NewLine, "¥").Replace("\n", "¥");
            comentariosRecurso = comentariosRecurso.Replace("'", "\\'").Replace(Environment.NewLine, "¥").Replace("\n", "¥");

            
            e.Cell.Attributes.Add("onclick", "abreTelaMudancaStatus('" + e.KeyValue + "." + e.DataColumn.FieldName + "','" + statusTarefa + "', '" + comentarios + "','" + comentariosRecurso + "');");
            e.Cell.Style.Add("cursor", "pointer");
        }  
    }

    private string numeroEmHoras(string numero)
    {
        string horaFormatada = "";

        if (numero != "")
        {
            double dbNumero = double.Parse(numero);
            int strHora = ((int)dbNumero);
            double strMinutos = System.Math.Round((dbNumero - strHora) * 60);

            if (strMinutos == 60)
            {
                strMinutos = 0;
                strHora = strHora + 1;
            }

            horaFormatada = string.Format("{0:D2}:{1:D2}", strHora, (int)strMinutos);
        }

        return horaFormatada;
    }

    protected void callbackStatus_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackStatus.JSProperties["cp_OK"] = "";
        callbackStatus.JSProperties["cp_Erro"] = "";

        string[] chaves = e.Parameter.ToString().Split('.');

        int codigoRecurso = int.Parse(chaves[0]);
        int codigoProjeto = int.Parse(chaves[1]);
        int codigoTipoTarefa = int.Parse(chaves[2]);
        string data = chaves[3];
        string statusTS = chaves[4];
        string msg = "";
        string comentarios = txtComentarios.Text;

        bool retorno = cDados.atualizaAprovacaoDataProjeto(codigoProjeto, codigoTipoTarefa, codigoRecurso, codigoEntidade, data, statusTS, idUsuarioLogado, comentarios, ref msg);

        if (retorno)
            callbackStatus.JSProperties["cp_OK"] = "Status Alterado com Sucesso!";
        else
            callbackStatus.JSProperties["cp_Erro"] = "Erro ao Alterar Status!";
    }

    private void carregaComboRecursos()
    {
        DataSet dsRecursos = cDados.getRecursosTSProjetoAprovacao(idUsuarioLogado, codigoEntidade);

        if (cDados.DataSetOk(dsRecursos))
        {
            ddlRecurso.DataSource = dsRecursos;
            ddlRecurso.TextField = "NomeUsuario";
            ddlRecurso.ValueField = "CodigoUsuario";
            ddlRecurso.DataBind();

            if (!IsPostBack && ddlRecurso.Items.Count > 0)
                ddlRecurso.SelectedIndex = 0;
        }
    }

    protected void gvDados_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.Value != null)
            e.Text = numeroEmHoras(e.Value.ToString());
    }
}

public class SelectAllTemplate : ITemplate
{

    public void InstantiateIn(Control container)
    {
        LinkButton button = new LinkButton();
        button.Text = "Select All";
        button.CommandName = "SelectAll";
        container.Controls.Add(button);
    }
}