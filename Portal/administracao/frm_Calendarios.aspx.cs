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
using System.Drawing;
using DevExpress.Web;

public partial class administracao_CalendarioRecurso : System.Web.UI.Page
{
    dados cDados;
    public int codigoCalendario = 0;
    private int codigoEntidade;
    DataSet dsDiasCalendario = new DataSet();
    private int alturaPrincipal = 0;
    public string alturaFrame = "";
    string[] diaUtil = new string[7];
    private int codigoRecursoCorporativo = -1;
    public string idioma;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CC"] != null && Request.QueryString["CC"].ToString() != "")
            codigoCalendario = int.Parse(Request.QueryString["CC"].ToString());

        DataSet ds = cDados.getHorariosPadroesCalendario(codigoCalendario, "AND IndicaHorarioPadrao = 'S'");

        if (cDados.DataSetOk(ds))
        {
            carregaHP(ds.Tables[0], "Dom", 0, !IsPostBack);
            carregaHP(ds.Tables[0], "Seg", 1, !IsPostBack);
            carregaHP(ds.Tables[0], "Ter", 2, !IsPostBack);
            carregaHP(ds.Tables[0], "Qua", 3, !IsPostBack);
            carregaHP(ds.Tables[0], "Qui", 4, !IsPostBack);
            carregaHP(ds.Tables[0], "Sex", 5, !IsPostBack);
            carregaHP(ds.Tables[0], "Sab", 6, !IsPostBack);
        }
        gvExcecao.JSProperties["cp_MSG"] = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["CR"] != null && Request.QueryString["CR"].ToString() != "")
            codigoRecursoCorporativo = int.Parse(Request.QueryString["CR"].ToString());

        if (Request.QueryString["Editar"] != null && Request.QueryString["Editar"].ToString() != "" && Request.QueryString["Editar"].ToString() == "N")
        {
            txtDescricao.ClientEnabled = false;
        }

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
        }

        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        codigoEntidade = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());

        defineAlturaTela(hfDadosSessao.Get("Resolucao").ToString());

        if (!IsPostBack)
        {
            calendario.SelectedDate = DateTime.Now;
        }

        cDados.aplicaEstiloVisual(this);
        carregaHorariosDiaSelecao();

        int ano, mes;

        if (!IsPostBack)
        {
            ano = DateTime.Now.Year;
            mes = DateTime.Now.Month;
        }
        else
        {
            ano = calendario.VisibleDate.Year;
            mes = calendario.VisibleDate.Month;
        }

        dsDiasCalendario = cDados.getDiasMesCalendario(codigoCalendario, ano, mes, "");

        carregaInformacoesCalendario(!IsPostBack);
        carregaExcecoesCalendario();
        lblDataSelecionada.Text = string.Format(Resources.traducao.frm_Calendarios_data__ + "{0:" + Resources.traducao.geral_formato_data_csharp + "}", calendario.SelectedDate);

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());

        gvHorariosTrabalho.Settings.ShowFilterRow = false;
        gvHorariosTrabalho.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvExcecao.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        idioma = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        this.TH(this.TS("frm_Calendarios"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);

        alturaFrame = (alturaPrincipal - 340) + "px";
    }

    private void carregaInformacoesCalendario(bool atualizaCampos)
    {
        DataSet ds;

        if (atualizaCampos)
        {
            string where = "";
            ds = cDados.getCalendario(codigoCalendario, where);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtDescricao.Text = ds.Tables[0].Rows[0]["DescricaoCalendario"].ToString();
                txtHorasDia.Text = ds.Tables[0].Rows[0]["HorasDia"].ToString();
                txtHorasSemana.Text = ds.Tables[0].Rows[0]["HorasSemana"].ToString();
                txtDiasMes.Text = ds.Tables[0].Rows[0]["DiasMes"].ToString();

                if (ds.Tables[0].Rows[0]["IndicaCalendarioPadrao"].ToString() == "S")
                    txtDescricao.ClientEnabled = false;
            }
        }        
    }

    #region Horarios Padrões

    private void carregaHP(DataTable dtHP, string diaSemana, int numeroSemana, bool atualizarCampos)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = " + (numeroSemana + 1));

        if (dr.Length > 0)
        {
            //diaUtil[0] = "N";
            for (int i = 1; i < 5; i++)
            {
                if (atualizarCampos)
                {
                    ((ASPxTextBox)pageControl.FindControl("txt_" + diaSemana + "_Ini_" + i)).Text = (dr[0]["HoraInicioTurno" + i] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno" + i] + "")) : "";
                    ((ASPxTextBox)pageControl.FindControl("txt_" + diaSemana + "_Term_" + i)).Text = (dr[0]["HoraTerminoTurno" + i] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno" + i] + "")) : "";
                }
                if (diaUtil[numeroSemana] != "S" && dr[0]["HoraInicioTurno" + i] + "" != "")
                    diaUtil[numeroSemana] = "S";
            }

        }
        else
        {
            diaUtil[numeroSemana] = "N";
        }
    }

    #endregion

    private void carregaHorariosDiaSelecao()
    {
        DataSet ds = cDados.getHorariosDataSelecionada(codigoCalendario, string.Format("{0:dd/MM/yyyy}", calendario.SelectedDate), "");

        if (cDados.DataSetOk(ds))
        {
            gvHorariosTrabalho.DataSource = ds;
            gvHorariosTrabalho.DataBind();
        }
    }

    private void carregaExcecoesCalendario()
    {
        DataSet dsExcecoes = cDados.getExcecoesCalendario(codigoCalendario, "");

        if (cDados.DataSetOk(dsExcecoes))
        {
            gvExcecao.DataSource = dsExcecoes;
            gvExcecao.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

    }

    protected void gvHorariosTrabalho_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaHorariosDiaSelecao();
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_MSG"] = "";
        callback.JSProperties["cp_Erro"] = "";
        callback.JSProperties["cp_Sucesso"] = "";

        bool noExisteEntidadAtual = cDados.getExisteNomeCalendarioEntidadAtual(codigoEntidade, txtDescricao.Text.Trim(), codigoCalendario);

        if (noExisteEntidadAtual)
        {

            string[,] segunda = new string[,] { { txt_Seg_Ini_1.Text, txt_Seg_Term_1.Text }, { txt_Seg_Ini_2.Text, txt_Seg_Term_2.Text }, { txt_Seg_Ini_3.Text, txt_Seg_Term_3.Text }, { txt_Seg_Ini_4.Text, txt_Seg_Term_4.Text } };
            string[,] terca = new string[,] { { txt_Ter_Ini_1.Text, txt_Ter_Term_1.Text }, { txt_Ter_Ini_2.Text, txt_Ter_Term_2.Text }, { txt_Ter_Ini_3.Text, txt_Ter_Term_3.Text }, { txt_Ter_Ini_4.Text, txt_Ter_Term_4.Text } };
            string[,] quarta = new string[,] { { txt_Qua_Ini_1.Text, txt_Qua_Term_1.Text }, { txt_Qua_Ini_2.Text, txt_Qua_Term_2.Text }, { txt_Qua_Ini_3.Text, txt_Qua_Term_3.Text }, { txt_Qua_Ini_4.Text, txt_Qua_Term_4.Text } };
            string[,] quinta = new string[,] { { txt_Qui_Ini_1.Text, txt_Qui_Term_1.Text }, { txt_Qui_Ini_2.Text, txt_Qui_Term_2.Text }, { txt_Qui_Ini_3.Text, txt_Qui_Term_3.Text }, { txt_Qui_Ini_4.Text, txt_Qui_Term_4.Text } };
            string[,] sexta = new string[,] { { txt_Sex_Ini_1.Text, txt_Sex_Term_1.Text }, { txt_Sex_Ini_2.Text, txt_Sex_Term_2.Text }, { txt_Sex_Ini_3.Text, txt_Sex_Term_3.Text }, { txt_Sex_Ini_4.Text, txt_Sex_Term_4.Text } };
            string[,] sabado = new string[,] { { txt_Sab_Ini_1.Text, txt_Sab_Term_1.Text }, { txt_Sab_Ini_2.Text, txt_Sab_Term_2.Text }, { txt_Sab_Ini_3.Text, txt_Sab_Term_3.Text }, { txt_Sab_Ini_4.Text, txt_Sab_Term_4.Text } };
            string[,] domingo = new string[,] { { txt_Dom_Ini_1.Text, txt_Dom_Term_1.Text }, { txt_Dom_Ini_2.Text, txt_Dom_Term_2.Text }, { txt_Dom_Ini_3.Text, txt_Dom_Term_3.Text }, { txt_Dom_Ini_4.Text, txt_Dom_Term_4.Text } };

            int horasDia = int.Parse(txtHorasDia.Text);
            int horasSemana = int.Parse(txtHorasSemana.Text);
            int diasMes = int.Parse(txtDiasMes.Text);

            bool retorno = cDados.atualizaCalendario(codigoCalendario, txtDescricao.Text, horasDia, horasSemana, diasMes, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString())
                , segunda, terca, quarta, quinta, sexta, sabado, domingo, codigoRecursoCorporativo);

            if (retorno)
                callback.JSProperties["cp_Sucesso"] = Resources.traducao.frm_Calendarios_calend_rio_salvo_com_sucesso_;
            else
                callback.JSProperties["cp_Erro"] = Resources.traducao.frm_Calendarios_erro_ao_salvar_o_calend_rio_;
        }
        else
            callback.JSProperties["cp_MSG"] = Resources.traducao.frm_Calendarios_erro_ao_salvar_o_calend_rio__n_o_pode_existir_descri__o_duplicadas_de_calendarios_;
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        carregaInformacoesCalendario(true);
    }

    public string getBotoes(string codigoExcecao)
    {
        string botoes = "";
        string botaoExcluir = "excluirReg02", alt = Resources.traducao.frm_Calendarios_excluir, cursor = "pointer", eventoExcluir = "if(confirm('" + Resources.traducao.frm_Calendarios_deseja_excluir_a_exce__o_ + "'))gvExcecao.PerformCallback('X_" + codigoExcecao + "');";
        string eventoEdicao = string.Format(@"abreEdicao({0}, {1})", codigoExcecao, codigoCalendario);

        botoes = string.Format(@"<table cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center"" style=""width: 25px"">
                                            <img onclick=""{2}"" style=""cursor: pointer;"" alt=""{5}"" src=""../imagens/botoes/editarReg02.PNG"" /></td>
                                        <td align=""center"" style=""width: 25px"">
                                            <img onclick=""{1}"" style=""cursor: {3};"" alt=""{4}"" src=""../imagens/botoes/{0}.PNG"" /></td>
                                    </tr>
                                </table>", botaoExcluir, eventoExcluir, eventoEdicao, cursor, alt, Resources.traducao.Calendarios_excluir);

        return botoes;
    }

    protected void gvExcecao_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvExcecao.JSProperties["cp_MSG"] = "";

        string parametro = e.Parameters.ToString();
        string codigoExcecao = "";

        if (parametro != "")
        {
            codigoExcecao = parametro.Substring(2);

            if (parametro.Substring(0, 1) == "X")
            {
                deletaExcecao(int.Parse(codigoExcecao));
                carregaExcecoesCalendario();
            }
        }
    }

    private void deletaExcecao(int codigoCalendario)
    {
        try
        {
            if (!cDados.excluiCalendario(codigoCalendario))
                throw new Exception(Resources.traducao.frm_Calendarios_n_o_se_pode_apagar_o_calend_rio__j__que_possui_depend_ncias_com_outros_objetos_);

        }
        catch (Exception ex)
        {
            gvExcecao.JSProperties["cp_MSG"] = ex.Message.ToString();
        }
    }

    protected void pnCalendario_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        dsDiasCalendario = cDados.getDiasMesCalendario(codigoCalendario, calendario.VisibleDate.Year, calendario.VisibleDate.Month, "");

        DateTime mes = calendario.SelectedDate;

        calendario.SelectedDate = mes.AddDays(1);
        calendario.SelectedDate = mes;
    }
    
    protected void callbackDisp_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string retorno = "";
        cDados.AtualizaCapacidadeDiariaRecurso(codigoRecursoCorporativo, ref retorno);
        callbackDisp.JSProperties["cp_Erro"] = retorno;
    }

    /// <summary>
    /// Callback chamado no fechamento da tela de edição do calendário. 
    /// Chama o método que irá verificar a necessidade de fazer nova projeção do calendário.
    /// O fechamento da tela no cliente se dá no processamento do endCallback desta chamada.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void callbackProjCalend_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        cDados.verificaNecessidadeProjecaoCalendario(codigoCalendario);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        gvExcecao.Columns["CodigoExcecao"].Visible = false;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "EdtCalCorp");

        gvExcecao.Columns["CodigoExcecao"].Visible = true;
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "abreInsercao(" + codigoCalendario + ");", true, true, false, "EdtCalCorp", "Calendário", this);
    }

    #endregion
    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if ((e.Column.Name == "Inicio" || e.Column.Name == "Termino") && (e.Value != null))
        {
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        }
    }
    protected void calendario_DayCellPrepared(object sender, CalendarDayCellPreparedEventArgs e)
    {
        if (e.IsOtherMonthDay)
        {
            e.Cell.Text = "";
            return;
        }

        int diaSemana = 0;

        switch (e.Date.DayOfWeek.ToString().Substring(0, 3).ToUpper())
        {
            case "SUN": diaSemana = 0;
                break;
            case "MON": diaSemana = 1;
                break;
            case "TUE": diaSemana = 2;
                break;
            case "WED": diaSemana = 3;
                break;
            case "THU": diaSemana = 4;
                break;
            case "FRI": diaSemana = 5;
                break;
            case "SAT": diaSemana = 6;
                break;
        }

        if (diaUtil[diaSemana] == "S")
            e.Cell.ForeColor = Color.Black;
        else
            e.Cell.ForeColor = Color.Red;

        if (cDados.DataSetOk(dsDiasCalendario) && cDados.DataTableOk(dsDiasCalendario.Tables[0]))
        {
            string whereExc = "Dia = " + e.Date.Day;

            DataRow[] dr = dsDiasCalendario.Tables[0].Select(whereExc);

            if (dr.Length > 0)
            {


                if (diaUtil[diaSemana] == "S")
                {
                    e.Cell.BackColor = Color.FromName("#ccd9c1");
                    e.Cell.ForeColor = Color.Black;
                }
                else
                {
                    if (dr.Length == 1)
                    {
                        bool indicaDiaUtil = dr[0]["IndicaDiaUtil"].ToString() == "S";

                        if (indicaDiaUtil)
                        {
                            e.Cell.BackColor = Color.FromName("#ccd9c1");
                            e.Cell.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (dsDiasCalendario.Tables[0].Select(whereExc + " AND IndicaDiaUtil = 'S'").Length > 0)
                        {
                            e.Cell.BackColor = Color.FromName("#ccd9c1");
                            e.Cell.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }
    }
}
