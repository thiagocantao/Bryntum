using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_DadosPlano_ManutencaoPlanoPeriodo : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    public bool podeEditar = false;


    protected void Page_Init(object sender, EventArgs e)
    {
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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        sdsGrid.ConnectionString = cDados.classeDados.getStringConexao();

        int codigoPlano = int.Parse(Request.QueryString["CP"]);
        podeEditar = false;
        if (cDados.podeEditarPPA(codigoPlano, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel))            
        {
            podeEditar = true;
        }


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "gvDados.AddNewRow();", true, true, false, "PlanPer", "Períodos de vigência", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PlanPer");
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {


        string Ano = e.NewValues["Ano"].ToString();
        string IndicaCenarioEditavel = e.NewValues["IndicaCenarioEditavel"].ToString();
        string IndicaOrcamentoEditavel = e.NewValues["IndicaOrcamentoEditavel"].ToString();
        string TipoDetalheCenario = e.NewValues["TipoDetalheCenario"].ToString();
        string TipoDetalheOrcamento = e.NewValues["TipoDetalheOrcamento"].ToString();
        


        sdsGrid.InsertParameters["Ano"].DefaultValue = Ano;
        sdsGrid.InsertParameters["IndicaCenarioEditavel"].DefaultValue = IndicaCenarioEditavel;
        sdsGrid.InsertParameters["IndicaOrcamentoEditavel"].DefaultValue = IndicaOrcamentoEditavel;
        sdsGrid.InsertParameters["TipoDetalheCenario"].DefaultValue = TipoDetalheCenario;
        sdsGrid.InsertParameters["TipoDetalheOrcamento"].DefaultValue = TipoDetalheOrcamento;
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        string Ano = e.NewValues["Ano"].ToString();
        string IndicaCenarioEditavel = e.NewValues["IndicaCenarioEditavel"].ToString();
        string TipoDetalheCenario = e.NewValues["TipoDetalheCenario"].ToString();
        string TipoDetalheOrcamento = e.NewValues["TipoDetalheOrcamento"].ToString();
        string IndicaOrcamentoEditavel = e.NewValues["IndicaOrcamentoEditavel"].ToString();


        sdsGrid.UpdateParameters["Ano"].DefaultValue = Ano;
        sdsGrid.UpdateParameters["IndicaCenarioEditavel"].DefaultValue = IndicaCenarioEditavel;
        sdsGrid.UpdateParameters["TipoDetalheCenario"].DefaultValue = TipoDetalheCenario;
        sdsGrid.UpdateParameters["TipoDetalheOrcamento"].DefaultValue = TipoDetalheOrcamento;
        sdsGrid.UpdateParameters["IndicaOrcamentoEditavel"].DefaultValue = IndicaOrcamentoEditavel;


        sdsGrid.Update();
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int Ano = int.Parse(e.Keys["Ano"].ToString());
        int CodigoPlano = int.Parse(e.Keys["CodigoPlano"].ToString());

        sdsGrid.DeleteParameters["Ano"].DefaultValue = Ano.ToString();
        sdsGrid.DeleteParameters["CodigoPlano"].DefaultValue = CodigoPlano.ToString();

        sdsGrid.Delete();
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            if (podeEditar == true)
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/ExcluirReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";

            }
        }
        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            if (podeEditar == true)
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/EditarReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/EditarRegDes.png";
            }
        }
    }
}
