using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;

public partial class administracao_auditoria : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string xml = @"<tabelas>
                        <DADO_ANTIGO xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                          <CodigoContrato>53</CodigoContrato>
                          <NumeroContrato>OUT/01/003</NumeroContrato>
                          <CodigoProjeto>592</CodigoProjeto>
                          <CodigoTipoAquisicao xsi:nil=""true"" />
                          <Fornecedor xsi:nil=""true"" />
                          <DescricaoObjetoContrato>Fazer mais testes do trigger auditoria 2,5. Alteração Thiago... AGORA COM O IP, e Workstation ID, gravando em coluna própria.</DescricaoObjetoContrato>
                          <CodigoFonteRecursosFinanceiros>1</CodigoFonteRecursosFinanceiros>
                          <CodigoUnidadeNegocio>1</CodigoUnidadeNegocio>
                          <DataInicio>2011-10-03T00:00:00</DataInicio>
                          <DataTermino>2012-01-03T00:00:00</DataTermino>
                          <CodigoUsuarioResponsavel>387</CodigoUsuarioResponsavel>
                          <NumeroContratoSAP xsi:nil=""true"" />
                          <Observacao>vai</Observacao>
                          <DataInclusao>2011-10-20T15:34:36.687</DataInclusao>
                          <CodigoUsuarioInclusao>393</CodigoUsuarioInclusao>
                          <StatusContrato>A</StatusContrato>
                          <DataUltimaAlteracao>2011-12-03T17:47:52.990</DataUltimaAlteracao>
                          <CodigoUsuarioUltimaAlteracao>371</CodigoUsuarioUltimaAlteracao>
                          <CodigoTipoContrato>1</CodigoTipoContrato>
                          <ValorContrato>-5555.0000</ValorContrato>
                          <DataBaseReajuste>2011-10-03T00:00:00</DataBaseReajuste>
                          <CodigoCriterioReajusteContrato>5</CodigoCriterioReajusteContrato>
                          <CodigoCriterioMedicaoContrato>1</CodigoCriterioMedicaoContrato>
                          <TipoContratado>J</TipoContratado>
                          <CodigoPessoaContratada>10</CodigoPessoaContratada>
                          <CodigoEntidade>1</CodigoEntidade>
                          <DataTerminoOriginal>2012-01-03T00:00:00</DataTerminoOriginal>
                          <ValorContratoOriginal>50000.0000</ValorContratoOriginal>
                          <DataAssinatura>2011-09-30T00:00:00</DataAssinatura>
                          <AnoContrato>3</AnoContrato>
                        </DADO_ANTIGO>
                        <DADO_NOVO xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                          <CodigoContrato>53</CodigoContrato>
                          <NumeroContrato>OUT/01/003</NumeroContrato>
                          <CodigoProjeto>592</CodigoProjeto>
                          <CodigoTipoAquisicao xsi:nil=""true"" />
                          <Fornecedor xsi:nil=""true"" />
                          <DescricaoObjetoContrato>Fazer mais testes do trigger auditoria 2,5. Alteração Thiago... AGORA COM O IP, e Workstation ID, gravando em coluna própria.</DescricaoObjetoContrato>
                          <CodigoFonteRecursosFinanceiros>1</CodigoFonteRecursosFinanceiros>
                          <CodigoUnidadeNegocio>1</CodigoUnidadeNegocio>
                          <DataInicio>2011-10-03T00:00:00</DataInicio>
                          <DataTermino>2012-01-03T00:00:00</DataTermino>
                          <CodigoUsuarioResponsavel>387</CodigoUsuarioResponsavel>
                          <NumeroContratoSAP xsi:nil=""true"" />
                          <Observacao>vai</Observacao>
                          <DataInclusao>2011-10-20T15:34:36.687</DataInclusao>
                          <CodigoUsuarioInclusao>393</CodigoUsuarioInclusao>
                          <StatusContrato>A</StatusContrato>
                          <DataUltimaAlteracao>2011-12-03T17:47:52.990</DataUltimaAlteracao>
                          <CodigoUsuarioUltimaAlteracao>371</CodigoUsuarioUltimaAlteracao>
                          <CodigoTipoContrato>1</CodigoTipoContrato>
                          <ValorContrato>51000.0000</ValorContrato>
                          <DataBaseReajuste>2011-10-03T00:00:00</DataBaseReajuste>
                          <CodigoCriterioReajusteContrato>5</CodigoCriterioReajusteContrato>
                          <CodigoCriterioMedicaoContrato>1</CodigoCriterioMedicaoContrato>
                          <TipoContratado>J</TipoContratado>
                          <CodigoPessoaContratada>10</CodigoPessoaContratada>
                          <CodigoEntidade>1</CodigoEntidade>
                          <DataTerminoOriginal>2012-01-03T00:00:00</DataTerminoOriginal>
                          <ValorContratoOriginal>50000.0000</ValorContratoOriginal>
                          <DataAssinatura>2011-09-30T00:00:00</DataAssinatura>
                          <AnoContrato>3</AnoContrato>
                        </DADO_NOVO>
                      </tabelas>";

        StringReader xmlSR = new StringReader(xml);

        DataSet dataSet = new DataSet();

        dataSet.ReadXml(xmlSR);


        DataTable dt = new DataTable();

        dt.Columns.Add("NomeCampo");
        dt.Columns.Add("NovoValor");
        dt.Columns.Add("AntigoValor");

        for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
        {
            if (dataSet.Tables[0].Rows[0][i].ToString() != dataSet.Tables[1].Rows[0][i].ToString())
            {
                DataRow dr = dt.NewRow();

                dr["NomeCampo"] = dataSet.Tables[0].Columns[i].ColumnName;
                dr["NovoValor"] = dataSet.Tables[0].Rows[0][i].ToString();
                dr["AntigoValor"] = dataSet.Tables[1].Rows[0][i].ToString();

                dt.Rows.Add(dr);
            }
        }

        gvDados.DataSource = dt;
        gvDados.DataBind();
    }
}