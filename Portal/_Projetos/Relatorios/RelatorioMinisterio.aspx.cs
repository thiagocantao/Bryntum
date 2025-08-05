using Cdis.Brisk.Application.Applications.Relatorio;
using Cdis.Brisk.Domain.Generic;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Relatorios_RelatorioMinisterio : BasePageBrisk
{
    public static string mensagemErro = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int maxLength = 4;
            var numberType = SpinEditNumberType.Integer;
            int minValue = 1900;
            int maxValue = 3000;

            txtAno1.MaxLength = maxLength;
            txtAno1.NumberType = numberType;
            txtAno1.MinValue = minValue;
            txtAno1.MaxValue = maxValue;

            txtAno2.MaxLength = maxLength;
            txtAno2.NumberType = numberType;
            txtAno2.MinValue = minValue;
            txtAno2.MaxValue = maxValue;

            txtAno3.MaxLength = maxLength;
            txtAno3.NumberType = numberType;
            txtAno3.MinValue = minValue;
            txtAno3.MaxValue = maxValue;

            
            CDados.aplicaEstiloVisual(this.Page);
        }
    }

    protected void painelCallbackLoading_Callback(object sender, CallbackEventArgsBase e)
    {
        Session["exportStream"] = null;
        ((ASPxCallback)sender).JSProperties["cp_erro"] = "";
        try
        {

            int codigoEntidade = UsuarioLogado.CodigoEntidade;
            int ano1 = Convert.ToInt32(txtAno1.Text);
            int ano2 = Convert.ToInt32(txtAno2.Text);
            int ano3 = Convert.ToInt32(txtAno3.Text);


            byte[] byteArray = UowApplication.GetUowApplication<RelatorioMinisterioApplication>().GetByteArrayPdfStreamRelatorioMinisterio(codigoEntidade, ano1, ano2, ano3);
            MemoryStream ms = new MemoryStream(byteArray);
            Stream s = ms;
            if (byteArray == null)
            {
                throw new Exception("Não ha dados para serem exibidos no relatório com os filtros informados.");
            }
            Session["exportStream"] = s;
        }
        catch (Exception exc)
        {
            ResultRequestDomain result = new ResultRequestDomain(exc);
            ((ASPxCallback)sender).JSProperties["cp_erro"] = result.Message;

        }
    }
}