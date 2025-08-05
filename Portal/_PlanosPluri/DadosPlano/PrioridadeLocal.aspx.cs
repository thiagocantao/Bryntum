using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_DadosPlano_PrioridadeLocal : System.Web.UI.Page
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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "gvDados.AddNewRow();", true, true, false, "PriLoc", "Prioridade Local", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PriLoc");
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

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        string CodigoPrioridadeLocal = e.Keys["CodigoPrioridadeLocal"].ToString();
        sdsGrid.DeleteParameters["CodigoPrioridadeLocal"].DefaultValue = CodigoPrioridadeLocal;

        sdsGrid.Delete();

        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        string NomePrioridade = e.NewValues["NomePrioridade"].ToString();
        string JustificativaAtuacao = e.NewValues["JustificativaAtuacao"].ToString();

        sdsGrid.InsertParameters["NomePrioridade"].DefaultValue = NomePrioridade;
        sdsGrid.InsertParameters["JustificativaAtuacao"].DefaultValue = JustificativaAtuacao;

        sdsGrid.Insert();

        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();

    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        string NomePrioridade = e.NewValues["NomePrioridade"].ToString();
        string JustificativaAtuacao = e.NewValues["JustificativaAtuacao"].ToString();
        string CodigoPrioridadeLocal = e.NewValues["CodigoPrioridadeLocal"].ToString();

        sdsGrid.UpdateParameters["NomePrioridade"].DefaultValue = NomePrioridade;
        sdsGrid.UpdateParameters["JustificativaAtuacao"].DefaultValue = JustificativaAtuacao;
        sdsGrid.UpdateParameters["CodigoPrioridadeLocal"].DefaultValue = CodigoPrioridadeLocal;

        sdsGrid.Update();

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
}
