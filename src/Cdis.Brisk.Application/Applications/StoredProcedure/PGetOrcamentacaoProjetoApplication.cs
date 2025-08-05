using Cdis.Brisk.DataTransfer.Orcamentacao;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cdis.Brisk.Application.Applications.StoredProcedure
{
    public class PGetOrcamentacaoProjetoApplication : IApplication
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CategoriaApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CategoriaApplication
        /// </summary>
        public UnitOfWorkApplication UowApplication
        {
            get
            {
                return _unitOfWorkApplication;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de aplicação PGetOrcamentacaoProjetoApplication
        /// </summary>
        public PGetOrcamentacaoProjetoApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Montar o objeto DataTable de acordo com a lista de orçamentos
        /// </summary>        
        private DataTable MontarDataTableOrcamentacaoProjeto(List<OrcamentoContaProjetoDataTransfer> list)
        {
            DataTable dTableOrcamento = new DataTable();

            dTableOrcamento.Columns.Add("GrupoConta");
            dTableOrcamento.Columns.Add("DescConta");
            foreach (var mes in Enumerable.Range(1, 12))
            {
                dTableOrcamento.Columns.Add(mes.ToString());
            }
            dTableOrcamento.Columns.Add("TotalConta");

            dTableOrcamento.Columns.Add("ValorOrcamentoAnterior");
            dTableOrcamento.Columns.Add("VariacaoOrcamentoAnterior");

            dTableOrcamento.Columns.Add("CodigoMemoriaCalculo");
            dTableOrcamento.Columns.Add("DescricaoMemoriaCalculo");

            dTableOrcamento.Columns.Add("ListValor");
            dTableOrcamento.Columns.Add("CodConta");
            DataRow dtRow = null;
            foreach (var item in list)
            {
                dtRow = dTableOrcamento.NewRow();
                dtRow[0] = item.GrupoConta;
                dtRow[1] = item.DescConta;
                for (int i = 0; i < item.ListValor.Count; i++)
                {
                    dtRow[i + 2] = item.ListValor[i].Valor;
                }
                dtRow[14] = item.TotalPorConta;

                dtRow[15] = item.TotalValorOrcamentoAnterior;
                dtRow[16] = item.TotalVariacaoOrcamentoAnterior;

                dtRow[17] = item.CodMemoriaCalculo;
                dtRow[18] = item.DescMemoriaCalculo;

                dtRow[19] = item.ListValor.ToJson();
                dtRow[20] = item.CodConta;
                dTableOrcamento.Rows.Add(dtRow);
            }

            return dTableOrcamento;
        }

        /// <summary>        
        /// Converter a lista de despesa da SP - dbo.P_GetOrcamentacaoProjeto para OrcamentacaoProjetoAnoDataTransfer
        /// </summary>        
        public OrcamentacaoProjetoAnoDataTransfer GetDataTableDespesaOrcamentacaoProjeto(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var listOrcamentacao = GetListDespesaOrcamentacaoProjetoDataTransfer(param);
            OrcamentacaoProjetoAnoDataTransfer orcamentacao = new OrcamentacaoProjetoAnoDataTransfer();
            if (listOrcamentacao.Any())
            {
                orcamentacao = new OrcamentacaoProjetoAnoDataTransfer
                {
                    NumAno = listOrcamentacao.FirstOrDefault().NumAno,
                    CodigoPrevisao = listOrcamentacao.FirstOrDefault().CodigoPrevisao,
                    SourceDataTableOrcamentacao = MontarDataTableOrcamentacaoProjeto(listOrcamentacao)
                };
            }
            return orcamentacao;
        }

        /// <summary>        
        /// Converter a lista de despesa da SP - dbo.P_GetOrcamentacaoProjeto para OrcamentacaoProjetoAnoDataTransfer
        /// </summary>        
        public OrcamentacaoProjetoAnoDataTransfer GetDataTableReceitaOrcamentacaoProjeto(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var listOrcamentacao = GetListReceitaOrcamentacaoProjetoDataTransfer(param);
            OrcamentacaoProjetoAnoDataTransfer orcamentacao = new OrcamentacaoProjetoAnoDataTransfer();
            if (listOrcamentacao.Any())
            {
                orcamentacao = new OrcamentacaoProjetoAnoDataTransfer
                {
                    NumAno = listOrcamentacao.FirstOrDefault().NumAno,
                    CodigoPrevisao = listOrcamentacao.FirstOrDefault().CodigoPrevisao,
                    SourceDataTableOrcamentacao = MontarDataTableOrcamentacaoProjeto(listOrcamentacao)
                };
            }

            return orcamentacao;
        }


        /// <summary>        
        /// Converter a lista de despesa da SP - dbo.P_GetOrcamentacaoProjeto para DataSet
        /// </summary>        
        public List<OrcamentoContaProjetoDataTransfer> GetListDespesaOrcamentacaoProjetoDataTransfer(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var list = UowApplication.UowService.GetUowService<PGetOrcamentacaoProjetoService>().GetListDespesaPGetOrcamentacaoProjeto(param);

            var listOrcamentoProjetoDataTransfer = MountOrcamentoProjetoDataTransfer(list);
            return listOrcamentoProjetoDataTransfer.OrderBy(l => l.DescConta).ToList();
        }

        /// <summary>        
        /// Converter a lista de despesa da SP - dbo.P_GetOrcamentacaoProjeto para DataSet
        /// </summary>        
        public List<OrcamentoContaProjetoDataTransfer> GetListReceitaOrcamentacaoProjetoDataTransfer(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var list = UowApplication.UowService.GetUowService<PGetOrcamentacaoProjetoService>().GetListReceitaPGetOrcamentacaoProjeto(param);

            var listOrcamentoProjetoDataTransfer = MountOrcamentoProjetoDataTransfer(list);
            return listOrcamentoProjetoDataTransfer.OrderBy(l => l.DescConta).ToList();
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<OrcamentoContaProjetoDataTransfer> MountOrcamentoProjetoDataTransfer(List<PGetOrcamentacaoProjetoDomain> list)
        {
            List<OrcamentoContaProjetoDataTransfer> listOrcamento = new List<OrcamentoContaProjetoDataTransfer>();

            if (list.Any())
            {
                var listAnoConta = list.Select(s => new { s._CodigoConta, s.DescricaoConta, s._Ano, s.GrupoConta, s.CodigoMemoriaCalculo, s.DescricaoMemoriaCalculo }).Distinct().ToList();

                //todo -> identificar se o ano será passando como parametro
                short? ano = listAnoConta.FirstOrDefault()._Ano;
                short codigoPrevisao = list.FirstOrDefault().CodigoPrevisao;
                foreach (var contaAno in listAnoConta.Where(a => a._Ano == ano).OrderBy(a => a._Ano))
                {
                    if (ano != contaAno._Ano) //Caso exista mais de 1 ano na lista
                    {
                        break;
                    }
                    var listAno = list.Where(i => i._CodigoConta == contaAno._CodigoConta && i._Ano == ano);

                    listOrcamento.Add(new OrcamentoContaProjetoDataTransfer
                    {
                        GrupoConta = contaAno.GrupoConta,
                        CodConta = contaAno._CodigoConta,
                        DescConta = contaAno.DescricaoConta,
                        NumAno = contaAno._Ano,
                        CodigoPrevisao = codigoPrevisao,
                        ListValor = MountListValorOrcamento(listAno),
                        DescMemoriaCalculo = contaAno.DescricaoMemoriaCalculo,
                        CodMemoriaCalculo = contaAno.CodigoMemoriaCalculo
                    });
                }
            }

            return listOrcamento;
        }

        /// <summary>
        /// Montar a lista de valores
        /// </summary>        
        private List<ValorOrcamentacaoDataTransfer> MountListValorOrcamento(IEnumerable<PGetOrcamentacaoProjetoDomain> listAno)
        {
            List<ValorOrcamentacaoDataTransfer> listValor = new List<ValorOrcamentacaoDataTransfer>();
            foreach (var mes in Enumerable.Range(1, 12))
            {
                var itemList = listAno.FirstOrDefault(i => i._Mes == mes);
                listValor.Add(
                    new ValorOrcamentacaoDataTransfer
                    {
                        NumAno = itemList._Ano.HasValue ? itemList._Ano.Value : (short)0,
                        NumMes = mes,
                        Valor = itemList.Valor,
                        IsEditavel = itemList.Editavel == "S",
                        ValorOrcamentoAnterior = decimal.Round(itemList.ValorOrcamentoAnterior, 2, MidpointRounding.AwayFromZero),
                        VariacaoOrcamentoAnterior = decimal.Round(itemList.VariacaoOrcamentoAnterior, 2, MidpointRounding.AwayFromZero),
                    });
            }

            return listValor;
        }

        #endregion
    }
}
