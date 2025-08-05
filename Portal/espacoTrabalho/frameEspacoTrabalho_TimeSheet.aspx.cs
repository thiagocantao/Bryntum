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
using System.Drawing;
using System.IO;
using DevExpress.XtraPrinting;
using System.Globalization;
using DevExpress.XtraScheduler.iCalendar;
using DevExpress.XtraScheduler;

public partial class espacoTrabalho_frameEspacoTrabalho_TimeSheet : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
    public int linha = 0;

    string definicaoToDoList = "To Do List";
    bool utilizaMSProject = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();
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

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList", "VersaoMSProject", "podeExportarCalendarioAtividadesCronograma");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
                definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

            utilizaMSProject = dsParametros.Tables[0].Rows[0]["VersaoMSProject"] + "" != "";

            gvDados.JSProperties["cp_UtilizaMSProject"] = utilizaMSProject ? "S" : "N";

            if ((dsParametros.Tables[0].Rows[0]["podeExportarCalendarioAtividadesCronograma"] as string) == "S")
                btnAtualizarAgenda.ClientVisible = true;
            else
                btnAtualizarAgenda.ClientVisible = false;
        }


        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/atualizacaoTarefas.js""></script>"));
        this.TH(this.TS("atualizacaoTarefas"));
        defineAlturaTela();

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        cDados.aplicaEstiloVisual(Page);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack && !IsCallback)
        {
            txtDe.Date = DateTime.Now.Date.AddDays(-6);
            txtFim.Date = DateTime.Now.Date;

            txtFim.MaxDate = DateTime.Now.Date;
            txtDe.MaxDate = DateTime.Now.Date;
            txtFim.MinDate = DateTime.Now.Date.AddDays(-6);

            if (Request.QueryString["DiasData"] != null && Request.QueryString["DiasData"].ToString() != "")
                txtTerminoPrevistoAte.Date = DateTime.Now.Date.AddDays(int.Parse(Request.QueryString["DiasData"].ToString()));
            //Atrasadas=S
            if (Request.QueryString["Atrasadas"] != null && Request.QueryString["Atrasadas"].ToString() == "S")
                checkAtrasadas.Checked = true;
        }

        gvDados.JSProperties["cp_Inicio"] = txtDe.Text;
        gvDados.JSProperties["cp_Termino"] = txtFim.Text;
        gvDados.JSProperties["cp_Previsto"] = txtTerminoPrevistoAte.Text;


        carregaComboProjetos();

        carregaGrid();

        gvDados.JSProperties["cp_Atualizar"] = "N";

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "ATLTAR", "ENT", -1, Resources.traducao.frameEspacoTrabalho_TimeSheet_adicionar_aos_favoritos);
        }
    }

    private void carregaGrid()
    {
        string inicioApontamento = string.Format("{0:dd/MM/yyyy}", txtDe.Date);
        string terminoApontamento = string.Format("{0:dd/MM/yyyy}", txtFim.Date);
        string terminoTarefas = txtTerminoPrevistoAte.Text != "" ? string.Format("{0:dd/MM/yyyy}", txtTerminoPrevistoAte.Date) : "";
        string terminoToDo = terminoTarefas;
        //if (Request.QueryString["DiasToDo"] != null && Request.QueryString["DiasToDo"].ToString() != "")
        //    terminoToDo = string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date.AddDays(int.Parse(Request.QueryString["DiasToDo"].ToString())));
        string somenteAtrasadas = checkAtrasadas.Checked ? "S" : "N";

        DataSet ds = cDados.getTimeSheetRecurso(idUsuarioLogado, int.Parse(ddlProjeto.Value.ToString()), ddlTipoAtualizacao.Value.ToString(), inicioApontamento, terminoApontamento, terminoTarefas, char.Parse(checkNaoConcluidas.Value.ToString()), codigoEntidade, terminoToDo, somenteAtrasadas);

        if (cDados.DataSetOk(ds))
        {
            carregaColunasDatas(ds.Tables[0].Rows.Count);
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        if (gvDados.VisibleRowCount == 0)
            ASPxButton1.ClientEnabled = false;
        else
            ASPxButton1.ClientEnabled = true;
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
            ((GridViewDataTextColumn)gvDados.Columns["dia" + (i + 1)]).PropertiesTextEdit.MaskSettings.Mask = "<0.." + horasDia + ">";
          


            gvDados.TotalSummary[i].FieldName = "";
        }

        if (quantidadeReg > 0)
        {
            gvDados.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;

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
        else
        {
            gvDados.Settings.HorizontalScrollBarMode = ScrollBarMode.Hidden;
        }
    }

    private string getDiaExtenso(DateTime data)
    {
        string diaSemana = "";

        switch (data.DayOfWeek.ToString().Substring(0, 3).ToUpper())
        {
            case "SUN":
                diaSemana = "Dom";
                break;
            case "MON":
                diaSemana = "Seg";
                break;
            case "TUE":
                diaSemana = "Ter";
                break;
            case "WED":
                diaSemana = "Qua";
                break;
            case "THU":
                diaSemana = "Qui";
                break;
            case "FRI":
                diaSemana = "Sex";
                break;
            case "SAT":
                diaSemana = "Sab";
                break;
        }

        return diaSemana;
    }

    private void carregaComboProjetos()
    {
        DataSet ds = cDados.getProjetosCronogramaRecurso(codigoEntidade, idUsuarioLogado, "");

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
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 320;
            gvDados.Width = Unit.Percentage(100);
    }

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.IsEditingRow == true)
        {
            e.Visible = false;
            //if (gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString() == "1" || (gvDados.GetRowValues(e.VisibleIndex, "TipoTarefa").ToString() == "P" && utilizaMSProject))
            //    e.Visible = false;
            //else if (gvDados.GetRowValues(e.VisibleIndex, "TipoAtualizacao").ToString() == "PC")
            //{
            //    e.Image.Url = "~/imagens/botoes/editarInLineDes.PNG";
            //    e.Text = "";
            //    e.Enabled = false;
            //}
        }
    }

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        string dataInicioProjeto = gvDados.GetRowValues(e.VisibleIndex, "InicioProjeto").ToString();
        string dataTerminoProjeto = gvDados.GetRowValues(e.VisibleIndex, "TerminoProjeto").ToString();
        e.Editor.DisabledStyle.ForeColor = Color.Black;
        e.Editor.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
        e.Editor.ClientInstanceName = "txt" + e.Column.Name + "_" + e.VisibleIndex;

        DateTime dtCampo = new DateTime();
        DateTime dtInicioProjeto = new DateTime();
        DateTime dtTerminoProjeto = new DateTime();

        if (dataInicioProjeto != "")
        {
            try
            {
                bool converteu = DateTime.TryParseExact(e.Column.FieldName, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

                if (converteu)
                {
                    string dataFormatada = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dataInicioProjeto));

                    DateTime.TryParseExact(dataFormatada, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicioProjeto);

                    if (dtCampo < dtInicioProjeto)
                    {
                        e.Editor.ClientEnabled = false;
                        return;
                    }
                    else
                        e.Editor.ClientEnabled = true;
                }
            }
            catch { }
        }

        if (dataTerminoProjeto != "")
        {
            try
            {
                dtCampo = new DateTime();

                bool converteu = DateTime.TryParseExact(e.Column.FieldName, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

                if (converteu)
                {
                    string dataFormatada = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dataTerminoProjeto));

                    DateTime.TryParseExact(dataFormatada, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTerminoProjeto);

                    if (dtCampo > dtTerminoProjeto.AddDays(1).AddSeconds(-1))
                    {
                        e.Editor.ClientEnabled = false;
                        return;
                    }
                    else
                        e.Editor.ClientEnabled = true;
                }
            }
            catch { }
        }


    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int codigoAtribuicao = int.Parse(e.Keys[0].ToString());
        string msgErro = "";

        string[] datas = new string[e.NewValues.Count - 5];
        double[] valores = new double[e.NewValues.Count - 5];

        double trabalhoReal = 0;

        TimeSpan diff = txtFim.Date - txtDe.Date;

        int diferencaDias = diff.Days + 1;

        for (int i = 0; i < diferencaDias; i++)
        {
            datas[i] = string.Format("{0:dd/MM/yyyy}", txtDe.Date.AddDays(i));
        }

        for (int i = 0; i < e.NewValues.Count - 5; i++)
        {
            valores[i] = double.Parse(e.NewValues[i + 5].ToString());
            trabalhoReal = trabalhoReal + double.Parse(e.NewValues[i + 5].ToString());
        }
        bool retorno = cDados.atualizaTarefa_PC_TS(idUsuarioLogado, codigoEntidade, codigoAtribuicao, e.NewValues[3].ToString(), trabalhoReal.ToString(), "NULL", "NULL", null, false, null, ref msgErro);

        if (retorno)
        {
            cDados.atualizaTarefaDiariaTS(codigoAtribuicao, datas, valores, ref msgErro);
        }

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

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0)
        {
            if (gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString() == "1")
                e.Visible = DevExpress.Utils.DefaultBoolean.False;

            string status = gvDados.GetRowValues(e.VisibleIndex, "StatusAprovacao").ToString();
            string possuiAnexo = gvDados.GetRowValues(e.VisibleIndex, "IndicaAnexo").ToString();

            if (e.ButtonID == "btnStatus")
            {
                e.Enabled = false;

                if (status == "PP")
                {
                    e.Image.Url = "~/imagens/botoes/tarefasPP.PNG";
                    e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_envio_pendente_para_aprova__o;
                }
                else
                {
                    if (status == "PA")
                    {
                        e.Image.Url = "~/imagens/botoes/tarefasPA.PNG";
                        e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_pendente_de_aprova__o;
                    }
                    else
                    {
                        if (status == "AP")
                        {
                            e.Image.Url = "~/imagens/botoes/salvar.gif";
                            e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_aprovado;
                        }
                        else
                        {
                            if (status == "RP")
                            {
                                e.Image.Url = "~/imagens/botoes/tarefaRecusada.PNG";
                                e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_reprovado;
                            }
                            else
                            {
                                if (status == "EA" || status == "ER")
                                {
                                    e.Image.Url = "~/imagens/botoes/tarefasPA.PNG";
                                    e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_em_processo_de_aprova__o_reprova__o__n_o_poder__ser_editado_durante_o_processo_;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (e.ButtonID == "btnDetalhes")
                {
                    if (status == "EA" || status == "ER")
                    {
                        e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
                        e.Enabled = false;
                        e.Text = "";
                    }

                    if (gvDados.GetRowValues(e.VisibleIndex, "TipoTarefa").ToString() == "P" && utilizaMSProject)
                    {
                        e.Image.Url = "~/imagens/botoes/pFormulario.png";
                        e.Text = Resources.traducao.frameEspacoTrabalho_TimeSheet_visualizar_detalhes_da_atribui__o;
                    }
                }
            }
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";

        string msgErro = "";
        bool retorno = cDados.publicaTarefasEntidade(idUsuarioLogado, codigoEntidade, ref msgErro);

        if (retorno)
        {
            callBack.JSProperties["cp_OK"] = Resources.traducao.frameEspacoTrabalho_TimeSheet_tarefas_enviadas_com_sucesso_;
            carregaGrid();
        }
        else
        {
            callBack.JSProperties["cp_Erro"] = Resources.traducao.frameEspacoTrabalho_TimeSheet_erro_ao_enviar_as_tarefas_para_aprova__o__n_nentre_em_contato_com_o_administrador_e_informe_a_seguinte_mensagem__n_n + msgErro;
        }
    }

    public string verificaDescricaoTarefa(string nivel, string descricao, string indicaAtrasada, string indicaCritica, string tipoTarefa)
    {
        string texto = "";
        string padding = "";
        string tabelaDescricaoTarefa = "";
        string estilo = "";
        string icone = "";

        if (nivel == "1")
        {
            texto = "<b>" + descricao + "</b>";
        }
        else
        {
            texto = descricao;
            padding = "padding-left:30px;";
        }

        if (indicaAtrasada == "1" || indicaAtrasada.ToLower() == "true")
            estilo += "color:red;";

        if (nivel != "1")
        {
            icone = ((indicaCritica == "1" || indicaCritica.ToLower() == "true") ? "<td style='width:12px'><img src='../imagens/tarefaCritica.gif' alt='Tarefa Crítica' /></td>" : "");
            icone += (Eval("IndicaAnexo").ToString() == "S") ? "<td style='width:12px'><img src='../imagens/possuiAnexo.png' alt='Possui Anexo' /></td>" : "";
        }
        else
            icone = "<td style='width:21px'>" + ((tipoTarefa == "T") ? "<img src='../imagens/toDoList.png' alt='" + definicaoToDoList + "'/>" : "<img src='../imagens/projeto.PNG' alt='Projeto' />") + "</td>";

        tabelaDescricaoTarefa = string.Format(@"<table style='width:330px;{3}'><tr>{0}<td style='{1}'>{2}</td></tr></table>"
            , icone
            , estilo
            , texto
            , padding);

        return tabelaDescricaoTarefa;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "logAtualizacaoInformacoes_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();
                gvExporter.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                app = "application/ms-excel";

            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {

                nomeArquivo = "\"" + nomeArquivo + "\"";
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();

            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem(Resources.traducao.frameEspacoTrabalho_TimeSheet_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
                                 </script>";
                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    protected void btnAtualizarAgenda_Click(object sender, EventArgs e)
    {
        var schedulerStorage = new SchedulerStorage();
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            var row = (DataRowView)gvDados.GetRow(i);
            int nivel = Convert.ToInt32(row["Nivel"]);
            string tipo = row["TipoTarefa"] as string;
            string descricao = row["Descricao"] as string;
            if (nivel != 2 || tipo != "P")
                continue;

            DateTime? inicioPrevisto;
            DateTime? terminoPrevisto;
            if (row["Inicio"] is DateTime)
                inicioPrevisto = (DateTime)row["Inicio"];
            else
                inicioPrevisto = null;
            if (row["Termino"] is DateTime)
                terminoPrevisto = (DateTime)row["Termino"];
            else
                terminoPrevisto = null;
            var appointment = schedulerStorage.CreateAppointment(AppointmentType.Normal);
            appointment.Description = descricao;
            appointment.Subject = descricao;
            if (inicioPrevisto.HasValue)
                appointment.Start = inicioPrevisto.Value;
            if (terminoPrevisto.HasValue && (!inicioPrevisto.HasValue || terminoPrevisto.Value > inicioPrevisto.Value))
                appointment.End = terminoPrevisto.Value;
            schedulerStorage.Appointments.Add(appointment);
        }
        var exporter = new iCalendarExporter(schedulerStorage);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            exporter.Export(memoryStream);
            Stream responseStream = Response.OutputStream;
            memoryStream.WriteTo(responseStream);
            responseStream.Flush();
        }
        Response.ContentType = "text/calendar";
        Response.AddHeader("Content-Disposition", "attachment; filename=Agenda.ics");
        Response.End();
    }

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {

        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }

        e.BrickStyle.Font = new Font("Verdana", 8);

        if (e.VisibleIndex > -1 && e.Column.Name.ToString() == "Descricao")
        {
            string tipo = gvDados.GetRowValues(e.VisibleIndex, "TipoTarefa").ToString();
            string nivel = gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString();
            string atrasada = gvDados.GetRowValues(e.VisibleIndex, "IndicaAtrasada").ToString();
            string critica = gvDados.GetRowValues(e.VisibleIndex, "IndicaCritica").ToString();

            if (nivel != "1")
            {
                if (atrasada == "1" || atrasada.ToLower() == "true")
                    e.BrickStyle.ForeColor = Color.Red;

                if (critica == "1" || critica.ToLower() == "true")
                    e.Text = "! " + e.Text;

                e.Text = "       " + e.Text;
            }
            else
            {
                e.BrickStyle.Font = new Font("Verdana", 8, FontStyle.Bold);

                if (tipo == "T")
                {
                    e.Text = definicaoToDoList + ": " + e.Text;
                }
                else
                {
                    e.Text = "Projeto: " + e.Text;
                }
            }
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex > -1 && gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString() != "1")
        {
            string trabalhoPrevisto = gvDados.GetRowValues(e.VisibleIndex, "TrabalhoPrevisto").ToString();
            string trabalhoReal = gvDados.GetRowValues(e.VisibleIndex, "TrabalhoRealTotal").ToString();

            if (trabalhoReal != "")
            {
                if (float.Parse(trabalhoPrevisto) < float.Parse(trabalhoReal))
                    e.Row.Font.Underline = true;
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex > -1 && gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString() != "1")
        {
            //if(e.DataColumn.FieldName == "Descricao") 
            //    e.Cell.Style.Add("PADDING-LEFT", "40px");
        }
    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
}
