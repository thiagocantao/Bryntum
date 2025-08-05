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

public partial class espacoTrabalho_frameEspacoTrabalho_AtualizacaoProjetos : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
    public int linha = 0;

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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/atualizacaoHorasProjeto.js""></script>"));

        defineAlturaTela();

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack && !IsCallback)
        {
            txtDe.Date = DateTime.Now.Date.AddDays(-6);
            txtFim.Date = DateTime.Now.Date;
        }

        gvDados.JSProperties["cp_Inicio"] = txtDe.Text;
        gvDados.JSProperties["cp_Termino"] = txtFim.Text;


        carregaComboProjetos();

        carregaGrid();

        gvDados.JSProperties["cp_Atualizar"] = "N";

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvDados.Settings.ShowFilterRow = false;
    }

    private void carregaGrid()
    {
        string inicioApontamento = string.Format("{0:dd/MM/yyyy}", txtDe.Date);
        string terminoApontamento = string.Format("{0:dd/MM/yyyy}", txtFim.Date);

        DataSet ds = cDados.getAtualizacaoProjetosRecurso(idUsuarioLogado, int.Parse(ddlProjeto.Value.ToString()), inicioApontamento, terminoApontamento, codigoEntidade);

        if (cDados.DataSetOk(ds))
        {
            carregaColunasDatas(ds.Tables[0].Rows.Count);
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
        else
        {
            gvDados.Columns.Clear();
            gvDados.DataBind();
        }

    }

    private void carregaColunasDatas(int quantidadeReg)
    {
        string horasDia = "24";

        DataSet dsParametros = cDados.getParametrosSistema("horasTrabalhoDia");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["horasTrabalhoDia"] + "" != "")
        {
            horasDia = dsParametros.Tables[0].Rows[0]["horasTrabalhoDia"] + "";
        }


        for (int i = 0; i < 7; i++)
        {
            ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).Visible = false;
            //((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).FieldName = "";
            ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).PropertiesTextEdit.MaskSettings.Mask = "HH:mm";
            gvDados.TotalSummary[i].FieldName = "";
        }

        if (quantidadeReg > 0)
        {
            TimeSpan diff = txtFim.Date - txtDe.Date;

            int diferencaDias = diff.Days + 1;

            if (diferencaDias <= 7)
            {
                for (int i = 0; i < diferencaDias; i++)
                {
                    string dia = string.Format("{0:dd/MM/yyyy}", txtDe.Date.AddDays(i));
                    string tituloDia = string.Format("{0:dd/MM}", txtDe.Date.AddDays(i));
                    string nomeDia = getDiaExtenso(txtDe.Date.AddDays(i));
                    ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).Visible = true;
                    ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).FieldName = dia;
                    gvDados.TotalSummary[i].FieldName = dia;
                    ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).VisibleIndex = i + 7;
                    gvDados.Columns["dia" + (i + 1)].Caption = tituloDia + " (" + nomeDia + ")";

                    if (nomeDia == "Sab" || nomeDia == "Dom")
                    {
                        ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).CellStyle.BackColor = Color.FromName("#E6E6E6");
                        ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).EditCellStyle.BackColor = Color.FromName("#E6E6E6");
                    }
                }
            }
        }
    }

    private string getDiaExtenso(DateTime data)
    {
        string diaSemana = "";

        switch (data.DayOfWeek.ToString().Substring(0, 3).ToUpper())
        {
            case "SUN": diaSemana = "Dom";
                break;
            case "MON": diaSemana = "Seg";
                break;
            case "TUE": diaSemana = "Ter";
                break;
            case "WED": diaSemana = "Qua";
                break;
            case "THU": diaSemana = "Qui";
                break;
            case "FRI": diaSemana = "Sex";
                break;
            case "SAT": diaSemana = "Sab";
                break;
        }

        return diaSemana;
    }

    private void carregaComboProjetos()
    {
        string inicioApontamento = string.Format("{0:dd/MM/yyyy}", txtDe.Date);
        string terminoApontamento = string.Format("{0:dd/MM/yyyy}", txtFim.Date);

        DataSet ds = cDados.getProjetosTimeSheetRecurso(idUsuarioLogado, inicioApontamento, terminoApontamento, codigoEntidade);

        ddlProjeto.DataSource = ds;
        ddlProjeto.TextField = "NomeProjeto";
        ddlProjeto.ValueField = "CodigoProjeto";
        ddlProjeto.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
        ddlProjeto.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlProjeto.SelectedIndex = 0;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 220;
    }

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString() == "1")
        {
            string tipoProj = gvDados.GetRowValues(e.VisibleIndex, "CodigoTipoProjeto").ToString();
            e.Enabled = false;
            e.Image.Url = "../imagens/botoes/LegendaTS" + (tipoProj != "" && int.Parse(tipoProj) < 7 ? tipoProj : "1")   + ".png";

            switch (gvDados.GetRowValues(e.VisibleIndex, "CodigoTipoProjeto").ToString())
            {
                case "1": e.Text = "Projeto";
                    break;
                case "2": e.Text = "Programa";
                    break;
                case "3": e.Text = "Processo";
                    break;
                case "4": e.Text = "Demanda";
                    break;
                case "5": e.Text = "Demanda";
                    break;
                case "6": e.Text = "Horas Não Trabalhadas";
                    break;
                default: e.Text = " ";
                    break;
            }
        }
    }

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {      
        e.Editor.ClientInstanceName = "txt" + e.Column.Name + "_" + e.VisibleIndex;

        if (e.Column.VisibleIndex > 1)
        {
            ((ASPxTextBox)e.Editor).Text = numeroEmHoras(((ASPxTextBox)e.Editor).Text);
        }

    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {        
        int codigoTipoTarefa = int.Parse(e.Keys[0].ToString().Split('.')[1]);
        int codigoProjeto = int.Parse(e.Keys[0].ToString().Split('.')[0]);
        string msgErro = "";

        string[] datas = new string[e.NewValues.Count - 1];
        double[] valores = new double[e.NewValues.Count - 1];
        string[] atualizar = new string[e.NewValues.Count - 1];


        for (int i = 0; i < e.NewValues.Count - 1; i++)
        {
            datas[i] = string.Format("{0:dd/MM/yyyy}", txtDe.Date.AddDays(i));
        }

        for (int i = 0; i < e.NewValues.Count - 1; i++)
        {
            valores[i] = horasEmNumero(e.NewValues[i + 1].ToString());
            double valorAntigo = e.OldValues[i + 1].ToString() == "" ? 0 : double.Parse(e.OldValues[i + 1].ToString());
            if (horasEmNumero(e.NewValues[i + 1].ToString()) == valorAntigo)
                atualizar[i] = "N";
            else
                atualizar[i] = "S";
        }
        bool retorno = cDados.atualizaHorasProjetosRecurso(codigoProjeto, codigoTipoTarefa, idUsuarioLogado, codigoEntidade, datas, valores, atualizar, ref msgErro);
       
        carregaGrid();
        e.Cancel = true;
        gvDados.CancelEdit();

    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        carregaGrid();
        gvDados.CancelEdit();
        gvDados.JSProperties["cp_Atualizar"] = "S";

    }
       
    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgErro = "";

        bool retorno = cDados.publicaHorasProjetoEntidade(idUsuarioLogado, codigoEntidade, ref msgErro);

        if (retorno)
        {
            callBack.JSProperties["cp_OK"] = "Publicação Efetuada com Sucesso!";
            carregaGrid();
        }
        else
        {
            callBack.JSProperties["cp_Erro"] = "Erro ao Efetuar a Publicação!";
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        string nivel = gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString();

        if (e.CellValue != null && e.DataColumn.VisibleIndex > 1 && nivel != "1" && e.CellValue.ToString() != "")
        {
            e.Cell.Text = numeroEmHoras(e.CellValue.ToString());



            int codigoTipoTarefa = int.Parse(e.KeyValue.ToString().Split('.')[1]);
            int codigoProjeto = int.Parse(e.KeyValue.ToString().Split('.')[0]);

            DataSet dsStatusTarefa = cDados.getStatusHoraRecursoProjeto(codigoProjeto, codigoTipoTarefa, idUsuarioLogado, codigoEntidade, e.DataColumn.FieldName.ToString(), "");

            string statusTarefa = "", comentarios = "", data = "", codigoProjeto1 = "", comentariosRecurso = "";

            if (cDados.DataSetOk(dsStatusTarefa) && cDados.DataTableOk(dsStatusTarefa.Tables[0]))
            {
                statusTarefa = dsStatusTarefa.Tables[0].Rows[0]["StatusTimesheet"].ToString();
                comentarios = dsStatusTarefa.Tables[0].Rows[0]["ComentariosAprovador"].ToString();
                data = dsStatusTarefa.Tables[0].Rows[0]["data"].ToString();
                codigoProjeto1 = dsStatusTarefa.Tables[0].Rows[0]["CodigoProjeto"].ToString();
                comentariosRecurso = dsStatusTarefa.Tables[0].Rows[0]["ComentariosRecurso"].ToString();

            }

            if (statusTarefa == "EA" || statusTarefa == "ER")
                statusTarefa = "PA";






            e.Cell.Style.Add("background-image", "url(../imagens/botoes/tarefas" + statusTarefa + "_pequeno.png);");
            e.Cell.Style.Add("background-repeat", "no-repeat;");
            //e.Cell.Style.Add("background-attachment", "fixed;");
            e.Cell.Style.Add("background-position", "center;");

            if (comentarios != "" && (statusTarefa == "AP" || statusTarefa == "RP"))
            {
                e.Cell.Style.Add("background-color", "#ebeb7e;");





            }
            /*
             * Este trecho de código foi modificado em virtude de haver resolvido o problema de quando um comentário fosse feito
             * o mesmo não era mostrado, este problema foi resolvido com sucesso. Este problema foi resolvido juntamente com
             * Amauri.
             */
            comentarios = comentarios.Replace("'", "\\'").Replace(Environment.NewLine, "¥").Replace("\n", "¥");
            comentariosRecurso = comentariosRecurso.Replace("'", "\\'").Replace(Environment.NewLine, "¥").Replace("\n", "¥");
            e.Cell.Attributes.Add("onclick", "abreComentarios('" + comentarios + "','" + data + "'," + codigoProjeto + ",'" + comentariosRecurso + "'," + codigoTipoTarefa + ");");
            e.Cell.Style.Add("cursor", "hand;");
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

    private double horasEmNumero(string hora)
    {
        double numeroFormatado = 0;

        hora = string.Format("{0:D4}", int.Parse(hora));

        hora = hora.Insert(2, ":");

        if (hora != "0")
        {
            double strHora = double.Parse(hora.Split(':')[0]);
            double strMinutos = double.Parse(hora.Split(':')[1]);

            numeroFormatado = strHora + (strMinutos/60);
        }

        return numeroFormatado;
    }

    protected void gvDados_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if(e.Value != null)
            e.Text = numeroEmHoras(e.Value.ToString());
    }

    protected void callBack1_CallBack(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgError = "";
        bool retorno = cDados.atualizaComentarioAprovacaoDataProjeto(int.Parse(hfData.Get("CodigoProjeto").ToString()), hfData.Get("Data").ToString(), idUsuarioLogado, txtComentarioRecurso.Text, int.Parse(hfData.Get("codigoTipoTarefaTimeSheet").ToString()), ref msgError);
        if (retorno)
        {
            callBack1.JSProperties["cp_OK"] = "Comentário feito com sucesso!";

        }
        else
        {
            callBack1.JSProperties["cp_Erro"] = "Erro ao escrever comentário:" + msgError;
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        carregaGrid();
        gvDados.CancelEdit();
        gvDados.JSProperties["cp_Atualizar"] = "S";
    }
}
