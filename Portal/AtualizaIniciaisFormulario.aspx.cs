using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class UltimoComandoSQL : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        string comandoSQL = "SELECT CodigoModeloFormulario, NomeFormulario FROM ModeloFormulario WHERE DataExclusao IS NULL ORDER BY NomeFormulario";

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlModelo.DataSource = ds;
        ddlModelo.TextField = "NomeFormulario";
        ddlModelo.ValueField = "CodigoModeloFormulario";
        ddlModelo.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtSenha.Text == "rsenha" && ddlModelo.SelectedIndex != -1)
        {
            string comandoSQL = "SELECT IniciaisFormularioControladoSistema FROM ModeloFormulario WHERE CodigoModeloFormulario = " + ddlModelo.Value;

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtIniciaisForm.Text = ds.Tables[0].Rows[0]["IniciaisFormularioControladoSistema"].ToString();
            }

            comandoSQL = "select CodigoCampo, CodigoModeloFormulario, NomeCampo, IniciaisCampoControladoSistema from CampoModeloFormulario where codigoModeloFormulario = " + ddlModelo.Value;

            ds = cDados.getDataSet(comandoSQL);

            gvCampos.DataSource = ds;
            gvCampos.DataBind();
        }
    }

    protected void gvCampos_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "IniciaisCampoControladoSistema")
        {
            ASPxTextBox txt = gvCampos.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtIniciaisCampo") as ASPxTextBox;
            string codigoCampo = gvCampos.GetRowValues(e.VisibleIndex, "CodigoCampo") + "";
            txt.Text = e.CellValue + "";

            txt.ClientSideEvents.TextChanged = @"function(s, e) { callbackCampo.PerformCallback('" + codigoCampo + ";' + s.GetText()); }";
        }
    }

    protected void callbackCampo_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string codigoCampo = e.Parameter.Split(';')[0];
            string iniciais = e.Parameter.Split(';')[1];

            string comandoSQL = "UPDATE CampoModeloFormulario SET IniciaisCampoControladoSistema = " + (iniciais.Trim() == "" ? "NULL" : ("'" + iniciais + "'")) + " WHERE CodigoModeloFormulario = " + ddlModelo.Value + " AND CodigoCampo = " + codigoCampo;
            int regAf = 0;

            cDados.execSQL(comandoSQL, ref regAf);
        }
    }

    protected void callbackForm_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = "UPDATE ModeloFormulario SET IniciaisFormularioControladoSistema = " + (txtIniciaisForm.Text.Trim() == "" ? "NULL" : ("'" + txtIniciaisForm.Text + "'")) + "WHERE CodigoModeloFormulario = " + ddlModelo.Value;
        int regAf = 0;

        cDados.execSQL(comandoSQL, ref regAf);
    }

    protected void gvCampos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "A")
        {
            string comandoSQL = "select CodigoCampo, CodigoModeloFormulario, NomeCampo, IniciaisCampoControladoSistema from CampoModeloFormulario where codigoModeloFormulario = -1";

            DataSet ds = cDados.getDataSet(comandoSQL);

            gvCampos.DataSource = ds;
            gvCampos.DataBind();
        }
    }
}