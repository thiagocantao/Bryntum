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

public partial class administracao_execCalendarios : System.Web.UI.Page
{
    dados cDados;
    int codigoCalendario = 0;
    int codigoExcecao = 0;
    private int codigoEntidade;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CC"] != null && Request.QueryString["CC"].ToString() != "")
            codigoCalendario = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["EX"] != null && Request.QueryString["EX"].ToString() != "")
            codigoExcecao = int.Parse(Request.QueryString["EX"].ToString());

        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            txtDe.Date = DateTime.Now.Date;
            txtAte.Date = DateTime.Now.Date.AddDays(7);

            //txtAte.MinDate = DateTime.Now.Date;

            callback.JSProperties["cp_Inicio"] = txtDe.Text;
            callback.JSProperties["cp_Termino"] = txtAte.Text;

            carregaInformacoesCalendario();
        }

        verificaPeriodo();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/execCalendarios.js""></script>"));
        this.TH(this.TS("execCalendarios"));
    }

    private void verificaPeriodo()
    {
        TimeSpan diff = txtAte.Date - txtDe.Date;

        int diferencaDias = diff.Days + 1;

        int diaSemana = 0;

        switch (txtDe.Date.DayOfWeek.ToString().Substring(0, 3).ToUpper())
        {
            case "SUN": diaSemana = 1;
                break;
            case "MON": diaSemana = 2;
                break;
            case "TUE": diaSemana = 3;
                break;
            case "WED": diaSemana = 4;
                break;
            case "THU": diaSemana = 5;
                break;
            case "FRI": diaSemana = 6;
                break;
            case "SAT": diaSemana = 7;
                break;
        }
        if (diferencaDias >= 7)
        {
            habilitaDomingo(true);
            habilitaSegunda(true);
            habilitaTerca(true);
            habilitaQuarta(true);
            habilitaQuinta(true);
            habilitaSexta(true);
            habilitaSabado(true);
        }
        else
        {
            bool seg = false, ter = false, qua = false, qui = false, sex = false, sab = false, dom = false;

            for (int i = diaSemana; i < diaSemana + diferencaDias; i++)
            {
                switch (i)
                {
                    case 1: dom = true;
                        break;
                    case 2: seg = true;
                        break;
                    case 3: ter = true;
                        break;
                    case 4: qua = true;
                        break;
                    case 5: qui = true;
                        break;
                    case 6: sex = true;
                        break;
                    case 7: sab = true;
                        break;
                    case 8: dom = true;
                        break;
                }
            }

            habilitaDomingo(dom);
            habilitaSegunda(seg);
            habilitaTerca(ter);
            habilitaQuarta(qua);
            habilitaQuinta(qui);
            habilitaSexta(sex);
            habilitaSabado(sab);
        }
    }

    private void habilitaDomingo(bool habilita)
    {
        txt_Dom_Ini_1.ClientEnabled = habilita;
        txt_Dom_Ini_2.ClientEnabled = habilita;
        txt_Dom_Ini_3.ClientEnabled = habilita;
        txt_Dom_Ini_4.ClientEnabled = habilita;
        txt_Dom_Term_1.ClientEnabled = habilita;
        txt_Dom_Term_2.ClientEnabled = habilita;
        txt_Dom_Term_3.ClientEnabled = habilita;
        txt_Dom_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Dom_Ini_1.Text = "";
            txt_Dom_Ini_2.Text = "";
            txt_Dom_Ini_3.Text = "";
            txt_Dom_Ini_4.Text = "";
            txt_Dom_Term_1.Text = "";
            txt_Dom_Term_2.Text = "";
            txt_Dom_Term_3.Text = "";
            txt_Dom_Term_4.Text = "";
        }
    }

    private void habilitaSegunda(bool habilita)
    {
        txt_Seg_Ini_1.ClientEnabled = habilita;
        txt_Seg_Ini_2.ClientEnabled = habilita;
        txt_Seg_Ini_3.ClientEnabled = habilita;
        txt_Seg_Ini_4.ClientEnabled = habilita;
        txt_Seg_Term_1.ClientEnabled = habilita;
        txt_Seg_Term_2.ClientEnabled = habilita;
        txt_Seg_Term_3.ClientEnabled = habilita;
        txt_Seg_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Seg_Ini_1.Text = "";
            txt_Seg_Ini_2.Text = "";
            txt_Seg_Ini_3.Text = "";
            txt_Seg_Ini_4.Text = "";
            txt_Seg_Term_1.Text = "";
            txt_Seg_Term_2.Text = "";
            txt_Seg_Term_3.Text = "";
            txt_Seg_Term_4.Text = "";
        }
    }

    private void habilitaTerca(bool habilita)
    {
        txt_Ter_Ini_1.ClientEnabled = habilita;
        txt_Ter_Ini_2.ClientEnabled = habilita;
        txt_Ter_Ini_3.ClientEnabled = habilita;
        txt_Ter_Ini_4.ClientEnabled = habilita;
        txt_Ter_Term_1.ClientEnabled = habilita;
        txt_Ter_Term_2.ClientEnabled = habilita;
        txt_Ter_Term_3.ClientEnabled = habilita;
        txt_Ter_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Ter_Ini_1.Text = "";
            txt_Ter_Ini_2.Text = "";
            txt_Ter_Ini_3.Text = "";
            txt_Ter_Ini_4.Text = "";
            txt_Ter_Term_1.Text = "";
            txt_Ter_Term_2.Text = "";
            txt_Ter_Term_3.Text = "";
            txt_Ter_Term_4.Text = "";
        }
    }

    private void habilitaQuarta(bool habilita)
    {
        txt_Qua_Ini_1.ClientEnabled = habilita;
        txt_Qua_Ini_2.ClientEnabled = habilita;
        txt_Qua_Ini_3.ClientEnabled = habilita;
        txt_Qua_Ini_4.ClientEnabled = habilita;
        txt_Qua_Term_1.ClientEnabled = habilita;
        txt_Qua_Term_2.ClientEnabled = habilita;
        txt_Qua_Term_3.ClientEnabled = habilita;
        txt_Qua_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Qua_Ini_1.Text = "";
            txt_Qua_Ini_2.Text = "";
            txt_Qua_Ini_3.Text = "";
            txt_Qua_Ini_4.Text = "";
            txt_Qua_Term_1.Text = "";
            txt_Qua_Term_2.Text = "";
            txt_Qua_Term_3.Text = "";
            txt_Qua_Term_4.Text = "";
        }
    }

    private void habilitaQuinta(bool habilita)
    {
        txt_Qui_Ini_1.ClientEnabled = habilita;
        txt_Qui_Ini_2.ClientEnabled = habilita;
        txt_Qui_Ini_3.ClientEnabled = habilita;
        txt_Qui_Ini_4.ClientEnabled = habilita;
        txt_Qui_Term_1.ClientEnabled = habilita;
        txt_Qui_Term_2.ClientEnabled = habilita;
        txt_Qui_Term_3.ClientEnabled = habilita;
        txt_Qui_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Qui_Ini_1.Text = "";
            txt_Qui_Ini_2.Text = "";
            txt_Qui_Ini_3.Text = "";
            txt_Qui_Ini_4.Text = "";
            txt_Qui_Term_1.Text = "";
            txt_Qui_Term_2.Text = "";
            txt_Qui_Term_3.Text = "";
            txt_Qui_Term_4.Text = "";
        }
    }

    private void habilitaSexta(bool habilita)
    {
        txt_Sex_Ini_1.ClientEnabled = habilita;
        txt_Sex_Ini_2.ClientEnabled = habilita;
        txt_Sex_Ini_3.ClientEnabled = habilita;
        txt_Sex_Ini_4.ClientEnabled = habilita;
        txt_Sex_Term_1.ClientEnabled = habilita;
        txt_Sex_Term_2.ClientEnabled = habilita;
        txt_Sex_Term_3.ClientEnabled = habilita;
        txt_Sex_Term_4.ClientEnabled = habilita;

        if (!habilita)
        {
            txt_Sex_Ini_1.Text = "";
            txt_Sex_Ini_2.Text = "";
            txt_Sex_Ini_3.Text = "";
            txt_Sex_Ini_4.Text = "";
            txt_Sex_Term_1.Text = "";
            txt_Sex_Term_2.Text = "";
            txt_Sex_Term_3.Text = "";
            txt_Sex_Term_4.Text = "";
        }
    }

    private void habilitaSabado(bool habilita)
    {
        txt_Sab_Ini_1.ClientEnabled = habilita;
        txt_Sab_Ini_2.ClientEnabled = habilita;
        txt_Sab_Ini_3.ClientEnabled = habilita;
        txt_Sab_Ini_4.ClientEnabled = habilita;
        txt_Sab_Term_1.ClientEnabled = habilita;
        txt_Sab_Term_2.ClientEnabled = habilita;
        txt_Sab_Term_3.ClientEnabled = habilita;
        txt_Sab_Term_4.ClientEnabled = habilita;
        if (!habilita)
        {
            txt_Sab_Ini_1.Text = "";
            txt_Sab_Ini_2.Text = "";
            txt_Sab_Ini_3.Text = "";
            txt_Sab_Ini_4.Text = "";
            txt_Sab_Term_1.Text = "";
            txt_Sab_Term_2.Text = "";
            txt_Sab_Term_3.Text = "";
            txt_Sab_Term_4.Text = "";
        }
    }

    private void carregaInformacoesCalendario()
    {
        string where = "AND c.CodigoCalendario = " + codigoExcecao;
        
        DataSet ds = cDados.getExcecoesCalendario(codigoCalendario, where);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtExcecao.Text = ds.Tables[0].Rows[0]["Excecao"].ToString();
            txtDe.Date = DateTime.Parse(ds.Tables[0].Rows[0]["Inicio"].ToString());
            txtAte.Date = DateTime.Parse(ds.Tables[0].Rows[0]["Termino"].ToString());

            txtAte.MinDate = txtDe.Date;

        }

        ds = cDados.getHorariosPadroesCalendario(codigoExcecao, "AND IndicaHorarioPadrao = 'N'");

        if (cDados.DataSetOk(ds))
        {
            carregaHPSegunda(ds.Tables[0]);
            carregaHPTerca(ds.Tables[0]);
            carregaHPQuarta(ds.Tables[0]);
            carregaHPQuinta(ds.Tables[0]);
            carregaHPSexta(ds.Tables[0]);
            carregaHPSabado(ds.Tables[0]);
            carregaHPDomingo(ds.Tables[0]);
        }
    }

    #region Horarios Padrões
    
    private void carregaHPSegunda(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 2");

        if (dr.Length > 0)
        {
            txt_Seg_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Seg_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Seg_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Seg_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Seg_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Seg_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Seg_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Seg_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPTerca(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 3");

        if (dr.Length > 0)
        {
            txt_Ter_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Ter_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Ter_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Ter_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Ter_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Ter_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Ter_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Ter_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPQuarta(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 4");

        if (dr.Length > 0)
        {
            txt_Qua_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Qua_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Qua_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Qua_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Qua_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Qua_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Qua_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Qua_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPQuinta(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 5");

        if (dr.Length > 0)
        {
            txt_Qui_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Qui_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Qui_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Qui_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Qui_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Qui_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Qui_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Qui_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPSexta(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 6");

        if (dr.Length > 0)
        {
            txt_Sex_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Sex_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Sex_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Sex_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Sex_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Sex_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Sex_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Sex_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPSabado(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 7");

        if (dr.Length > 0)
        {
            txt_Sab_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Sab_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Sab_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Sab_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Sab_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Sab_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Sab_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Sab_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }

    private void carregaHPDomingo(DataTable dtHP)
    {
        DataRow[] dr = dtHP.Select("DiaSemana = 1");

        if (dr.Length > 0)
        {
            txt_Dom_Ini_1.Text = (dr[0]["HoraInicioTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno1"] + "")) : "";
            txt_Dom_Ini_2.Text = (dr[0]["HoraInicioTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno2"] + "")) : "";
            txt_Dom_Ini_3.Text = (dr[0]["HoraInicioTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno3"] + "")) : "";
            txt_Dom_Ini_4.Text = (dr[0]["HoraInicioTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraInicioTurno4"] + "")) : "";
            txt_Dom_Term_1.Text = (dr[0]["HoraTerminoTurno1"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno1"] + "")) : "";
            txt_Dom_Term_2.Text = (dr[0]["HoraTerminoTurno2"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno2"] + "")) : "";
            txt_Dom_Term_3.Text = (dr[0]["HoraTerminoTurno3"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno3"] + "")) : "";
            txt_Dom_Term_4.Text = (dr[0]["HoraTerminoTurno4"] + "" != "") ? string.Format("{0:HH:mm}", DateTime.Parse(dr[0]["HoraTerminoTurno4"] + "")) : "";
        }
    }


    #endregion

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_MSG"] = "";
        callback.JSProperties["cp_Erro"] = "";

        string[,] segunda = new string[,] { { txt_Seg_Ini_1.Text, txt_Seg_Term_1.Text }, { txt_Seg_Ini_2.Text, txt_Seg_Term_2.Text }, { txt_Seg_Ini_3.Text, txt_Seg_Term_3.Text },  { txt_Seg_Ini_4.Text, txt_Seg_Term_4.Text }};
        string[,] terca = new string[,] { { txt_Ter_Ini_1.Text, txt_Ter_Term_1.Text }, { txt_Ter_Ini_2.Text, txt_Ter_Term_2.Text }, { txt_Ter_Ini_3.Text, txt_Ter_Term_3.Text }, { txt_Ter_Ini_4.Text, txt_Ter_Term_4.Text } };
        string[,] quarta = new string[,] { { txt_Qua_Ini_1.Text, txt_Qua_Term_1.Text }, { txt_Qua_Ini_2.Text, txt_Qua_Term_2.Text }, { txt_Qua_Ini_3.Text, txt_Qua_Term_3.Text }, { txt_Qua_Ini_4.Text, txt_Qua_Term_4.Text } };
        string[,] quinta = new string[,] { { txt_Qui_Ini_1.Text, txt_Qui_Term_1.Text }, { txt_Qui_Ini_2.Text, txt_Qui_Term_2.Text }, { txt_Qui_Ini_3.Text, txt_Qui_Term_3.Text }, { txt_Qui_Ini_4.Text, txt_Qui_Term_4.Text } };
        string[,] sexta = new string[,] { { txt_Sex_Ini_1.Text, txt_Sex_Term_1.Text }, { txt_Sex_Ini_2.Text, txt_Sex_Term_2.Text }, { txt_Sex_Ini_3.Text, txt_Sex_Term_3.Text }, { txt_Sex_Ini_4.Text, txt_Sex_Term_4.Text } };
        string[,] sabado = new string[,] { { txt_Sab_Ini_1.Text, txt_Sab_Term_1.Text }, { txt_Sab_Ini_2.Text, txt_Sab_Term_2.Text }, { txt_Sab_Ini_3.Text, txt_Sab_Term_3.Text }, { txt_Sab_Ini_4.Text, txt_Sab_Term_4.Text } };
        string[,] domingo = new string[,] { { txt_Dom_Ini_1.Text, txt_Dom_Term_1.Text }, { txt_Dom_Ini_2.Text, txt_Dom_Term_2.Text }, { txt_Dom_Ini_3.Text, txt_Dom_Term_3.Text }, { txt_Dom_Ini_4.Text, txt_Dom_Term_4.Text } };

        bool retorno;

        if (codigoExcecao > 0)
        {
            retorno = cDados.atualizaExcecao(codigoExcecao, txtExcecao.Text, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString())
                , segunda, terca, quarta, quinta, sexta, sabado, domingo, txtDe.Date, txtAte.Date);
        }
        else
        {
            retorno = cDados.incluiExcecao(codigoEntidade, codigoCalendario, txtExcecao.Text, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString())
                , segunda, terca, quarta, quinta, sexta, sabado, domingo, txtDe.Date, txtAte.Date);
        }
        
        if(retorno)
            callback.JSProperties["cp_MSG"] = Resources.traducao.execCalendarios_exce__o_salva_com_sucesso_;
        else
            callback.JSProperties["cp_Erro"] = Resources.traducao.execCalendarios_erro_ao_salvar_a_exce__o_;
    }
}
