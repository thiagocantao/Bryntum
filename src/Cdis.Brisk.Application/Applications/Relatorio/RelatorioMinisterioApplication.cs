using Cdis.Brisk.DataTransfer.Relatorio.Ministerio;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Cdis.Brisk.Service.Services.UnidadeNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.Application.Applications.Relatorio
{
    public class RelatorioMinisterioApplication : IApplication
    {
        #region Properties

        private const string _styleTable = "font-size:6px;margin-top:0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;width:50%;text-align: center;border: 1px solid #dddddd;margin-left: 20px;";
        private const string _styleCol = "padding: 2px;border: 1px solid #dddddd;";
        private const string _styleColGreen = "background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;";



        /// <summary>
        /// Propriedade unit of work da classe de aplicação RelatorioReceitaDespesaApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação RelatorioReceitaDespesaApplication
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
        /// Construtor da classe de aplicação  RelatorioReceitaDespesaApplication
        /// </summary>
        /// <param name="unitOfWorkApplication"></param>
        public RelatorioMinisterioApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Buscar o array de byte do pdf do relatório 
        /// </summary>        
        public byte[] GetByteArrayPdfStreamRelatorioMinisterio(int idEntidade, int ano1, int ano2, int ano3)
        {
            string titulo = "SERVIÇO NACIONAL DE APRENDIZAGEM DO COOPERATIVISMO - (" + ano1.ToString() + "," + ano2.ToString() + "," + ano3.ToString() + ")";
            DateTime dtAtual = DateTime.Now;
            string strDataHora = dtAtual.ToString("dd/MM/yyyy HH:mm:ss");

            UnidadeNegocioDomain unidadeColigada = UowApplication.UowService.GetUowService<UnidadeNegocioService>().GetUnidadeNegocioColigada(idEntidade);
            string codColigada = unidadeColigada == null ? "0" : unidadeColigada.CodigoReservado;

            List<RelatorioMinisterioReceitaDespesaDataTransfer> listReceita = GetListReceita(codColigada, ano1, ano2, ano3);
            List<RelatorioMinisterioReceitaDespesaDataTransfer> listDespesa = GetListDespesa(codColigada, ano1, ano2, ano3);
            List<string> listHtmlPage = new List<string>();
            listHtmlPage.Add(GetPageOrcamentoReceita(titulo, strDataHora, ano1, ano2, ano3, listReceita));
            listHtmlPage.Add(GetPageOrcamentoDespesa(titulo, strDataHora, ano1, ano2, ano3, listDespesa));
            listHtmlPage.Add(GetPageOrcamentoNatureza(titulo, strDataHora, ano1, ano2, ano3, listReceita, listDespesa));

            listHtmlPage.Add(GetPageOrcamentoFinalidade(titulo, strDataHora, codColigada, ano1, ano2, ano3));

            return Infra.Core.Pdf.PdfCore.GetStreamPageFromListHtmlPageSizeA4PortraitMargin5(listHtmlPage).ToByteArray();
        }

        /// <summary>
        /// Buscar as receitas
        /// </summary>
        /// <param name="idEntidade"></param>
        /// <param name="ano1"></param>
        /// <param name="ano2"></param>
        /// <param name="ano3"></param>
        /// <returns></returns>
        private List<RelatorioMinisterioReceitaDespesaDataTransfer> GetListReceita(string codColigada, int ano1, int ano2, int ano3)
        {
            var listReceita = new List<RelatorioMinisterioReceitaDespesaDataTransfer>();
            #region Load

            //{
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 10000000, DscEspecificacao = "RECEITAS CORRENTES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 12000000, DscEspecificacao = "RECEITAS DE CONTRIBUIÇÕES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 12100000, DscEspecificacao = "CONTRIBUIÇÕES SOCIAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 12104401, DscEspecificacao = "CONTRIBUIÇÃO SESCOOP" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 12104402, DscEspecificacao = "ADICIONAL À CONTRIBUIÇÃO" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13000000, DscEspecificacao = "RECEITAS PATRIMONIAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13100000, DscEspecificacao = "RECEITAS IMOBILIÁRIAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13110001, DscEspecificacao = "ALUGUEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13200000, DscEspecificacao = "RECEITAS DE VALORES MOBILIARIOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13210001, DscEspecificacao = "JUROS DE TÍTULOS DE RENDA" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13900000, DscEspecificacao = "OUTRAS RECEITAS PATRIMONIAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 13900001, DscEspecificacao = "OUTRAS RECEITAS PATRIMONIAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16000000, DscEspecificacao = "RECEITAS DE SERVIÇOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16001601, DscEspecificacao = "SERVIÇOS EDUCACIONAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16001901, DscEspecificacao = "SERVIÇOS RECREATIVOS E CULTURAIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16002001, DscEspecificacao = "SERVIÇOS DE CONSULTORIA, ASSISTÊNCIA TÉCNICA E ANÁLISE DE PROJETOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16002201, DscEspecificacao = "SERVIÇOS DE ESTUDOS E PESQUISAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 16009901, DscEspecificacao = "OUTRAS RECEITAS DE SERVIÇOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17000000, DscEspecificacao = "TRANSFERÊNCIAS CORRENTES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17300000, DscEspecificacao = "TRANSFERÊNCIAS DE INSTITUIÇÕES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17300001, DscEspecificacao = "TRANSFERÊNCIAS REGULAMENTARES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17300002, DscEspecificacao = "TRANSFERÊNCIAS DE INSTITUIÇÕES PRIVADAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17300003, DscEspecificacao = "OUTRAS TRANSFERÊNCIAS CORRENTES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17600000, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17610001, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS COM A UNIÃO E ENTIDADES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17620001, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS DOS ESTADOS, DF E SUAS ENTIDADES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17630001, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS DOS MUNICÍPIOS E SUAS ENTIDADES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 17640001, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS DE INSTITUIÇÕES PRIVADAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19000000, DscEspecificacao = "OUTRAS RECEITAS CORRENTES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19100000, DscEspecificacao = "MULTAS E JUROS DE MORA" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19190001, DscEspecificacao = "MULTAS E JUROS DE MORA" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19200000, DscEspecificacao = "INDENIZAÇÕES E RESTITUIÇÕES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19219901, DscEspecificacao = "OUTRAS INDENIZAÇÕES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19220001, DscEspecificacao = "OUTRAS RESTITUIÇÕES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19900000, DscEspecificacao = "RECEITAS CORRENTES DIVERSAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 19909901, DscEspecificacao = "OUTRAS RECEITAS CORRENTES" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 20000000, DscEspecificacao = "RECEITAS DE CAPITAL" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22000000, DscEspecificacao = "ALIENAÇÃO DE BENS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22100000, DscEspecificacao = "ALIENAÇÃO DE BENS MÓVEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22190001, DscEspecificacao = "ALIENAÇÃO DE OUTROS BENS MÓVEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22190002, DscEspecificacao = "ALIENAÇÃO DE OUTROS BENS INTANGÍVEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22200000, DscEspecificacao = "ALIENAÇÃO DE BENS IMÓVEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 22290001, DscEspecificacao = "ALIENAÇÃO DE OUTROS BENS IMÓVEIS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 24000000, DscEspecificacao = "TRANSFERÊNCIAS DE CAPITAL" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 24300000, DscEspecificacao = "TRANSFERÊNCIAS DE INSTITUIÇÕES PRIVADAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 24300001, DscEspecificacao = "TRANSFERÊNCIAS DE INSTITUIÇÕES PRIVADAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 24700000, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 24740001, DscEspecificacao = "TRANSFERÊNCIAS DE CONVÊNIOS DE INSTITUIÇÕES PRIVADAS" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 25000000, DscEspecificacao = "OUTRAS RECEITAS DE CAPITAL" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 25900000, DscEspecificacao = "OUTRAS RECEITAS DE CAPITAL" },
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo = 25900001, DscEspecificacao = "OUTRAS RECEITAS DE CAPITAL" }
            //};
            #endregion

            var listRelReceia = UowApplication.UowService.GetUowService<PSescoopRelMinisterioReceitaService>().GetListReceita(codColigada, ano1, ano2, ano3);

            var listSaldoAnosAnteriores = listRelReceia.Where(a => a.NumConta == 19999901 || a.NumConta == 29999901).ToList();

            foreach (var item in listRelReceia)
            {
                decimal vlAno1 = item.Valor_0;
                decimal vlAno2 = item.Valor_1;
                decimal vlAno3 = item.Valor_2;

                if (item.NumConta == 10000000 || item.NumConta == 19000000)
                {
                    PSescoopRelMinisterioReceitaDomain saldo19 = listRelReceia.FirstOrDefault(a => a.NumConta == 19999901);
                    if (saldo19 != null)
                    {
                        vlAno1 = vlAno1 - saldo19.Valor_0;
                        vlAno2 = vlAno2 - saldo19.Valor_1;
                        vlAno3 = vlAno3 - saldo19.Valor_2;
                    }
                }

                if (item.NumConta == 20000000)
                {
                    PSescoopRelMinisterioReceitaDomain saldo29 = listRelReceia.FirstOrDefault(a => a.NumConta == 29999901);
                    if (saldo29 != null)
                    {
                        vlAno1 = vlAno1 - saldo29.Valor_0;
                        vlAno2 = vlAno2 - saldo29.Valor_1;
                        vlAno3 = vlAno3 - saldo29.Valor_2;
                    }
                }

                listReceita.Add(new RelatorioMinisterioReceitaDespesaDataTransfer
                {
                    Codigo = item.NumConta,
                    DscEspecificacao = item.Descricao,
                    VlrAno1 = vlAno1,
                    VlrAno2 = vlAno2,
                    VlrAno3 = vlAno3
                });
            }

            return listReceita;
        }

        /// <summary>
        /// Buscar a lista de despesas
        /// </summary>
        /// <param name="idEntidade"></param>
        /// <param name="ano1"></param>
        /// <param name="ano2"></param>
        /// <param name="ano3"></param>
        /// <returns></returns>
        private List<RelatorioMinisterioReceitaDespesaDataTransfer> GetListDespesa(string codColigada, int ano1, int ano2, int ano3)
        {
            var listDespesa = new List<RelatorioMinisterioReceitaDespesaDataTransfer>();
            #region Load
            //{
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =30000000    , DscEspecificacao = "DESPESAS CORRENTES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31000000    , DscEspecificacao = "PESSOAL E ENCARGOS SOCIAIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31900000    , DscEspecificacao = "APLICAÇÕES DIRETAS - PESSOAL"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31900800    , DscEspecificacao = "OUTROS BENEFÍCIOS ASSISTENCIAIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31901100    , DscEspecificacao = "VENCIMENTOS E VANTAGENS FIXAS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31901300    , DscEspecificacao = "OBRIGAÇÕES PATRONAIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31901600    , DscEspecificacao = "OUTRAS DESPESAS VARIÁVEIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =31909400    , DscEspecificacao = "INDENIZAÇÕES TRABALHISTAS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33000000    , DscEspecificacao = "OUTRAS DESPESAS CORRENTES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33200000    , DscEspecificacao = "TRANSFERÊNCIAS À UNIÃO"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33204100    , DscEspecificacao = "CONTRIBUIÇÕES À UNIÃO"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33300000    , DscEspecificacao = "TRANSFERÊNCIAS AOS ESTADOS E AO DF"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33304100    , DscEspecificacao = "CONTRIBUIÇÕES AOS ESTADOS, DF E ENTIDADES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33400000    , DscEspecificacao = "TRANSFERÊNCIAS A MUNICÍPIOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33404100    , DscEspecificacao = "CONTRIBUIÇÕES AOS MUNICÍPIOS E ENTIDADES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33500000    , DscEspecificacao = "TRANSFERÊNCIAS A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33504100    , DscEspecificacao = "CONTRIBUIÇÕES A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33600000    , DscEspecificacao = "TRANSFERÊNCIAS A INSTITUIÇÕES PRIVADAS COM FINS LUCRATIVOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33604100    , DscEspecificacao = "CONTRIBUIÇÕES A INSTITUIÇÕES PRIVADAS COM FINS LUCRATIVOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33900000    , DscEspecificacao = "APLICAÇÕES DIRETAS - CORRENTES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33900400    , DscEspecificacao = "CONTRATAÇÃO POR TEMPO DETERMINADO"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33901400    , DscEspecificacao = "DIÁRIAS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33901800    , DscEspecificacao = "AUXÍLIO FINANCEIRO A ESTUDANTES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903000    , DscEspecificacao = "MATERIAL DE CONSUMO"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903100    , DscEspecificacao = "PREMIAÇÕES CULTURAIS, ARTÍSTICAS, CIENTÍFICAS, DESPORTIVAS E OUTROS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903200    , DscEspecificacao = "MATERIAL DE DISTRIBUIÇÃO GRATUITA"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903300    , DscEspecificacao = "PASSAGENS E DESPESAS COM LOCOMOÇÃO"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903500    , DscEspecificacao = "SERVIÇOS DE CONSULTORIA"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903600    , DscEspecificacao = "OUTROS SERVIÇOS DE TERCEIROS - PF"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33903900    , DscEspecificacao = "OUTROS SERVIÇOS DE TERCEIROS - PJ"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33904700    , DscEspecificacao = "OBRIGAÇÕES TRIBUTÁRIAS E CONTRIBUTIVAS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =33909300    , DscEspecificacao = "INDENIZAÇÕES E RESTITUIÇÕES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =40000000    , DscEspecificacao = "DESPESAS DE CAPITAL"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44000000    , DscEspecificacao = "INVESTIMENTOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44500000    , DscEspecificacao = "TRANSFERÊNCIAS A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS - INVESTIMENTOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44504100    , DscEspecificacao = "CONTRIBUIÇÕES A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS - INVESTIMENTOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44900000    , DscEspecificacao = "APLICAÇÕES DIRETAS - INVESTIMENTOS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44903900    , DscEspecificacao = "OUTROS SERVIÇOS DE TERCEIROS - PJ"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44905100    , DscEspecificacao = "OBRAS E INSTALAÇÕES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44905200    , DscEspecificacao = "EQUIPAMENTOS E MATERIAL PERMANENTE"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =44906100    , DscEspecificacao = "AQUISIÇÃO DE IMÓVEIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45000000    , DscEspecificacao = "INVERSÕES FINANCEIRAS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45500000    , DscEspecificacao = "TRANSFERÊNCIAS A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS - INVERSÕES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45504100    , DscEspecificacao = "CONTRIBUIÇÕES A INSTITUIÇÕES PRIVADAS SEM FINS LUCRATIVOS - INVERSÕES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45900000    , DscEspecificacao = "APLICAÇÕES DIRETAS - INVERSÕES"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45906100    , DscEspecificacao = "AQUISIÇÃO DE IMÓVEIS"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45906200    , DscEspecificacao = "AQUISIÇÃO DE BENS PARA REVENDA"},
            //        new RelatorioMinisterioReceitaDespesaDataTransfer { Codigo =45906600    , DscEspecificacao = "CONCESSÃO DE EMPRÉSTIMOS E FINACIAMENTOS"}
            //};
            #endregion

            var listRelDespesa = UowApplication.UowService.GetUowService<PSescoopRelMinisterioDespesaService>().GetListDespesa(codColigada, ano1, ano2, ano3);

            foreach (var item in listRelDespesa)
            {
                listDespesa.Add(new RelatorioMinisterioReceitaDespesaDataTransfer
                {
                    Codigo = item.NumConta,
                    DscEspecificacao = item.Descricao,
                    VlrAno1 = item.Valor_0,
                    VlrAno2 = item.Valor_1,
                    VlrAno3 = item.Valor_2
                });
            }
            return listDespesa;
        }

        /// <summary>
        /// Retornar o html da nota
        /// </summary>
        /// <returns></returns>
        private string GetNotaHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='margin-bottom:10px;margin-left:10px;padding-left:10px;font-weight: bold;font-size: 9px;'>" +
                "Nota: <br/>" +
                "Este relatório atende a dispositivos legais, quais sejam: (a) Lei de Diretrizes Orçamentárias – LDO (Lei nº 13.898/2019), art.134, inciso III; (b) Acórdão TCU nº 699/2016, item 9.1.1." +
                "</div>");
            return sb.ToString();
        }

        /// <summary>
        /// Montar a pagina com a tabela de receita e despesas
        /// </summary>       
        private string MountPageReceitaDespesa(string tituloTotais, int ano1, int ano2, int ano3, List<RelatorioMinisterioReceitaDespesaDataTransfer> listReceitaDespesa, bool isResumo, bool isOrcamento)
        {
            StringBuilder sb = new StringBuilder();

            string styleFirstCol = "background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;";
            string styleGreenEspecificacao = isResumo ? styleFirstCol : _styleColGreen;
            sb.Append("<table style='" + _styleTable + "width:96%;' cellspacing='0' border='0'>"
                     + "<tr>"
                     + string.Format(isResumo ? "" : "  <th style='" + styleFirstCol + ";width:10%' rowspan='2' >Código</th>")
                     + "  <th style='" + styleGreenEspecificacao + "width:50%' rowspan='2'>Especificação</th>"
                     + "  <th style='" + _styleColGreen + "'>" + ano1.ToString() + "</th>"
                     + "  <th style='" + _styleColGreen + "'>" + ano2.ToString() + "</th>"
                     + "  <th style='" + _styleColGreen + "'>" + ano3.ToString() + "</th>"
                     + "</tr>"
                     + "<tr >"
                     + "  <td style='" + _styleColGreen + "'>Original ou Reformulado</td>"
                     + "  <td style='" + _styleColGreen + "'>Original ou Reformulado</td>"
                     + "  <td style='" + _styleColGreen + "'>Original ou Reformulado</td>"
                     + "</tr>");

            int firstCod = listReceitaDespesa.Where(a => a.Codigo != 19999901 && a.Codigo != 29999901).FirstOrDefault().Codigo;
            var totalCod19 = listReceitaDespesa.FirstOrDefault(a => a.Codigo == 19999901);
            var totalCod29 = listReceitaDespesa.FirstOrDefault(a => a.Codigo == 29999901);

            RelatorioMinisterioReceitaDespesaDataTransfer totaisAnosAnteriores = null;

            if (isOrcamento)
            {
                totaisAnosAnteriores = new RelatorioMinisterioReceitaDespesaDataTransfer
                {
                    VlrAno1 = totalCod19.VlrAno1 + totalCod29.VlrAno1,
                    VlrAno2 = totalCod19.VlrAno2 + totalCod29.VlrAno2,
                    VlrAno3 = totalCod19.VlrAno3 + totalCod29.VlrAno3
                };
            }


            List<RelatorioMinisterioReceitaDespesaDataTransfer> listReceitaSemTotais = listReceitaDespesa.Where(a => a.Codigo != 19999901 && a.Codigo != 29999901).ToList();
            decimal totalAno1 = listReceitaSemTotais.Where(a => a.Codigo == 10000000 || a.Codigo == 20000000 || a.Codigo == 30000000 || a.Codigo == 40000000).Sum(o => o.VlrAno1);
            decimal totalAno2 = listReceitaSemTotais.Where(a => a.Codigo == 10000000 || a.Codigo == 20000000 || a.Codigo == 30000000 || a.Codigo == 40000000).Sum(o => o.VlrAno2);
            decimal totalAno3 = listReceitaSemTotais.Where(a => a.Codigo == 10000000 || a.Codigo == 20000000 || a.Codigo == 30000000 || a.Codigo == 40000000).Sum(o => o.VlrAno3);

            foreach (var item in listReceitaSemTotais.OrderBy(o => o.Codigo).ToList())
            {
                if (item.Codigo == firstCod)
                {
                    string styleCommon = "background-color:#dddddd;padding: 2px;border-right: 1px solid #dddddd;border-bottom: 1px solid #dddddd;";
                    string styleFirst = "border-left: 1px solid #dddddd;";
                    string styleEspFirst = isResumo ? styleCommon + "padding-left:3px;" : styleCommon + "padding-left:3px;";
                    sb.Append("<tr>"
                            + string.Format(isResumo ? "" : "<th style='" + styleCommon + styleFirst + "'>" + item.Codigo.ToString() + "</th>")
                            + "<th style='" + styleEspFirst + "text-align: left;'>" + item.DscEspecificacao + "</th>"
                            + "<th style='" + styleCommon + "border-right: 1px solid #dddddd;'>" + FormatarValorEmReal(item.VlrAno1) + "</th>"
                            + "<th style='" + styleCommon + "border-right: 1px solid #dddddd;'>" + FormatarValorEmReal(item.VlrAno2) + "</th>"
                            + "<th style='background-color:#dddddd;padding: 2px;border-bottom: 1px solid #dddddd;border-right: 1px solid #dddddd;'>" + FormatarValorEmReal(item.VlrAno3) + "</th>"
                          + "</tr>");

                    continue;
                }

                string tpCol = item.IsAgrupador
                                ? "th"
                                : "td";
                string bkColor = item.IsAgrupador
                                ? "background-color:#dddddd" : "";

                if (isResumo && (item.Codigo != 10000000 && item.Codigo != 20000000 && item.Codigo != 30000000 && item.Codigo != 40000000))
                {
                    tpCol = "td";
                    bkColor = "";
                }

                string styleLeftCol = "border-left: 1px solid #dddddd;";
                string styleEsp = isResumo ? _styleCol + styleLeftCol : _styleCol;
                sb.Append("<tr>"
                            + string.Format(isResumo ? "" : "<" + tpCol + " style='" + _styleCol + styleLeftCol + bkColor + "'>" + item.Codigo.ToString() + "</" + tpCol + ">")
                            + "<" + tpCol + " style='" + styleEsp + "text-align: left;" + bkColor + "'>" + item.DscEspecificacao + "</" + tpCol + ">"
                            + "<" + tpCol + " style='" + _styleCol + bkColor + "'>" + FormatarValorEmReal(item.VlrAno1) + "</" + tpCol + ">"
                            + "<" + tpCol + " style='" + _styleCol + bkColor + "'>" + FormatarValorEmReal(item.VlrAno2) + "</" + tpCol + ">"
                            + "<" + tpCol + " style='" + _styleCol + bkColor + "'>" + FormatarValorEmReal(item.VlrAno3) + "</" + tpCol + ">"
                          + "</tr>");
            }

            string tagColspan = isResumo ? "" : "colspan='2'";
            sb.Append("<tr>"
                    + "  <th style='background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;border-bottom: 1px solid #dddddd;' " + tagColspan + ">" + tituloTotais + "</th>"
                    + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-bottom: 1px solid #dddddd;'>" + FormatarValorEmReal(totalAno1) + "</th>"
                    + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-bottom: 1px solid #dddddd;'>" + FormatarValorEmReal(totalAno2) + "</th>"
                    + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-bottom: 1px solid #dddddd;'>" + FormatarValorEmReal(totalAno3) + "</th>"
                    + "</tr>");

            if (isOrcamento && totaisAnosAnteriores != null)
            {
                decimal orcamentoAno1 = totaisAnosAnteriores.VlrAno1;
                decimal orcamentoAno2 = totaisAnosAnteriores.VlrAno2;
                decimal orcamentoAno3 = totaisAnosAnteriores.VlrAno3;

                decimal totalOrcamentoAno1 = orcamentoAno1 + totalAno1;
                decimal totalOrcamentoAno2 = orcamentoAno2 + totalAno2;
                decimal totalOrcamentoAno3 = orcamentoAno3 + totalAno3;

                sb.Append("<tr>"
                   + "  <th style='background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;border-top: 6px solid #dddddd;' " + tagColspan + ">RECEITAS DE EXERCÍCIOS ANTERIORES INCORPORADAS AO ORÇAMENTO DO ANO</th>"
                   + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(orcamentoAno1) + "</th>"
                   + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(orcamentoAno2) + "</th>"
                   + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(orcamentoAno3) + "</th>"
                   + "</tr>");

                sb.Append("<tr>"
                        + "  <th style='background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;border-top: 6px solid #dddddd;border-bottom: 1px solid #dddddd;' " + tagColspan + ">ORÇAMENTO TOTAL DO ANO</th>"
                        + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(totalOrcamentoAno1) + "</th>"
                        + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(totalOrcamentoAno2) + "</th>"
                        + "  <th style='" + _styleColGreen + "border-right: 1px solid #dddddd;border-top: 6px solid #dddddd;'>" + FormatarValorEmReal(totalOrcamentoAno3) + " </th>"
                        + "</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// Esse método trunca o valor e não faz arredondamento e depois formata o resultado para a moeda Real.
        /// Exemplo 9.999 - resultado: 9.99
        /// </summary>        
        private string FormatarValorEmReal(decimal valor)
        {
            var valorTruncado = Math.Truncate(100 * valor) / 100;

            return valorTruncado.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        /// <summary>
        /// Monta a página de ORÇAMENTO DE RECEITAS
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="strDataHora"></param>
        /// <returns></returns>
        private string GetPageOrcamentoReceita(string titulo, string strDataHora, int ano1, int ano2, int ano3, List<RelatorioMinisterioReceitaDespesaDataTransfer> listReceita)
        {
            string subTitulo = "ORÇAMENTO DE RECEITAS";
            string page = MountPageReceitaDespesa("TOTAIS DE RECEITAS", ano1, ano2, ano3, listReceita, false, true);

            return GetPageHtml(page, "01", titulo, subTitulo, strDataHora);
        }

        /// <summary>
        /// Monta a página de ORÇAMENTO DE DESPESAS
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="strDataHora"></param>
        /// <returns></returns>
        private string GetPageOrcamentoDespesa(string titulo, string strDataHora, int ano1, int ano2, int ano3, List<RelatorioMinisterioReceitaDespesaDataTransfer> listDespesa)
        {
            string subTitulo = "ORÇAMENTO DE DESPESAS";
            string page = MountPageReceitaDespesa("TOTAIS DE DESPESAS", ano1, ano2, ano3, listDespesa, false, false);
            return GetPageHtml(page, "02", titulo, subTitulo, strDataHora);
        }

        /// <summary>
        /// Monta a página de RECEITAS E DESPESAS - TOTAIS POR NATUREZA
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="strDataHora"></param>
        /// <returns></returns>
        private string GetPageOrcamentoNatureza(string titulo, string strDataHora, int ano1, int ano2, int ano3, List<RelatorioMinisterioReceitaDespesaDataTransfer> listReceita, List<RelatorioMinisterioReceitaDespesaDataTransfer> listDespesa)
        {
            string subTitulo = "RECEITAS E DESPESAS - TOTAIS POR NATUREZA";
            var listNaturezaReceita = listReceita.Where(r => r.IsAgrupador || r.Codigo == 19999901 || r.Codigo == 29999901).ToList();
            var listNaturezaDespesa = listDespesa.Where(r => r.IsAgrupador).ToList();

            string pageReceita = MountPageReceitaDespesa("TOTAIS DE RECEITAS", ano1, ano2, ano3, listNaturezaReceita, true, true);
            string pageDespesa = MountPageReceitaDespesa("TOTAIS DE DESPESAS", ano1, ano2, ano3, listNaturezaDespesa, true, false);

            StringBuilder sb = new StringBuilder();
            //sb.Append("<table style='width:100%;' cellspacing='0' border='0'>");
            //sb.Append("<tr style='height:100%'>");
            //sb.Append("<td style='width:50%;'>");
            sb.Append(pageReceita);
            sb.Append("<br />");
            //sb.Append("</td>");
            //sb.Append("<td style='width:50%;'>");
            sb.Append(pageDespesa);
            //sb.Append("</td>");
            //sb.Append("</tr>");
            //sb.Append("</table>");
            return GetPageHtml(sb.ToString(), "03", titulo, subTitulo, strDataHora);
        }

        /// <summary>
        /// Monta a página de DESPESAS - TOTAIS POR FINALIDADE
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="strDataHora"></param>
        /// <returns></returns>
        private string GetPageOrcamentoFinalidade(string titulo, string strDataHora, string codColigada, int ano1, int ano2, int ano3)
        {
            string subTitulo = "DESPESAS - TOTAIS POR FINALIDADE";
            StringBuilder sb = new StringBuilder();
            var list5200 = UowApplication.UowService.GetUowService<PSescoopRelMinisterio5200Service>().GetListPSescoopRelMinisterio5200(codColigada, ano1, ano2, ano3);
            var list5400 = UowApplication.UowService.GetUowService<PSescoopRelMinisterio5400Service>().GetListPSescoopRelMinisterio5400(codColigada, ano1, ano2, ano3);
            List<RelatorioMinisterioFinalidade52005400DataTransfer> listFinalidade5200 = GetFinalidade52005400DataTransfer(list5200);
            List<RelatorioMinisterioFinalidade52005400DataTransfer> listFinalidade5400 = GetFinalidade52005400DataTransfer(list5400);
            RelatorioMinisterioFinalidadeDataTransfer finalidade5200 = GetFinalidadeDataTransfer(ano1, ano2, ano3, listFinalidade5200, listFinalidade5400);
            RelatorioMinisterioFinalidadeDataTransfer finalidade5400 = GetFinalidadeDataTransfer(ano1, ano2, ano3, listFinalidade5400, listFinalidade5200);

            string cabecalho5400 = GetFinalidadeCabecalhoHtml(finalidade5400);
            string table5400 = GetTableHtmlFinalidade(finalidade5400);

            string cabecalho5200 = GetFinalidadeCabecalhoHtml(finalidade5200);
            string table5200 = GetTableHtmlFinalidade(finalidade5200);

            sb.Append(cabecalho5400);
            sb.Append(table5400);
            sb.Append("<br />");
            sb.Append(cabecalho5200);
            sb.Append(table5200);

            return GetPageHtml(sb.ToString(), "04", titulo, subTitulo, strDataHora);
        }

        /// <summary>
        /// Criar a lista unificada da finalidade 5200 e 5400
        /// </summary>
        private List<RelatorioMinisterioFinalidade52005400DataTransfer> GetFinalidade52005400DataTransfer<TEntity>(List<TEntity> listRelatorio)
        {
            List<RelatorioMinisterioFinalidade52005400DataTransfer> listFinalidade = new List<RelatorioMinisterioFinalidade52005400DataTransfer>();

            foreach (var item in listRelatorio)
            {
                RelatorioMinisterioFinalidade52005400DataTransfer itemFinalidade = new RelatorioMinisterioFinalidade52005400DataTransfer();
                item.CopyProperties(ref itemFinalidade);
                listFinalidade.Add(itemFinalidade);
            }

            return listFinalidade;
        }
        /// <summary>
        /// 
        /// </summary>
        private RelatorioMinisterioFinalidadeDataTransfer GetFinalidadeDataTransfer(int ano1, int ano2, int ano3, List<RelatorioMinisterioFinalidade52005400DataTransfer> listPrimeira, List<RelatorioMinisterioFinalidade52005400DataTransfer> listSegunda)
        {
            List<long> listSeq1 = listPrimeira.Select(s => s.Seq).ToList();
            List<long> listSeq2 = listSegunda.Select(s => s.Seq).ToList();

            long seq1 = (listPrimeira.Count == 4) ? (listSeq1.Min() + 1) : listSeq1.Min();
            long seq3 = listSeq1.Max();
            long seq2 = listSeq1.FirstOrDefault(s => s > seq1 && s < seq3);

            long seq1Segunda = (listSegunda.Count == 4) ? (listSeq2.Min() + 1) : listSeq2.Min();
            long seq3Segunda = listSeq2.Max();
            long seq2Segunda = listSeq2.FirstOrDefault(s => s > seq1Segunda && s < seq3Segunda);

            decimal totalAno1Primeira =
                   (listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_0
                   + listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_0
                   + listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_0);

            decimal totalAno2Primeira =
                   (listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_1
                   + listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_1
                   + listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_1);

            decimal totalAno3Primeira =
                    (listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_2
                    + listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_2
                    + listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_2);


            decimal totalAno1Segunda =
                    (listSegunda.FirstOrDefault(v => v.Seq == seq1Segunda).Valor_0
                    + listSegunda.FirstOrDefault(v => v.Seq == seq2Segunda).Valor_0
                    + listSegunda.FirstOrDefault(v => v.Seq == seq3Segunda).Valor_0);

            decimal totalAno2Segunda =
                    (listSegunda.FirstOrDefault(v => v.Seq == seq1Segunda).Valor_1
                    + listSegunda.FirstOrDefault(v => v.Seq == seq2Segunda).Valor_1
                    + listSegunda.FirstOrDefault(v => v.Seq == seq3Segunda).Valor_1);

            decimal totalAno3Segunda =
                    (listSegunda.FirstOrDefault(v => v.Seq == seq1Segunda).Valor_2
                    + listSegunda.FirstOrDefault(v => v.Seq == seq2Segunda).Valor_2
                    + listSegunda.FirstOrDefault(v => v.Seq == seq3Segunda).Valor_2);

            var finalidade = new RelatorioMinisterioFinalidadeDataTransfer();

            finalidade.Ano1 = ano1.ToString();
            finalidade.Ano2 = ano2.ToString();
            finalidade.Ano3 = ano3.ToString();

            finalidade.Programa = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Programa)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Programa)).Programa : "";
            finalidade.Objetivo = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Objetivo)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Objetivo)).Objetivo : "";
            finalidade.Indicador = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Indicador)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Indicador)).Indicador : "";

            finalidade.Funcao = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Funcao)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Funcao)).Funcao : "";
            finalidade.SubFuncao = listPrimeira.Any(v => !string.IsNullOrEmpty(v.SubFuncao)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.SubFuncao)).SubFuncao : "";
            finalidade.CodAcao = listPrimeira.Any(v => !string.IsNullOrEmpty(v.CodAcao)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.CodAcao)).CodAcao : "";
            finalidade.Acao = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Acao)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Acao)).Acao : "";
            finalidade.Unidade = listPrimeira.Any(v => !string.IsNullOrEmpty(v.Unidade)) ? listPrimeira.FirstOrDefault(v => !string.IsNullOrEmpty(v.Unidade)).Unidade : "";

            finalidade.Desc_Despesa1 = listPrimeira.FirstOrDefault(v => v.Seq == seq1).Desc_Despesa ?? "";
            finalidade.ValorDespesa1_Ano1 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_0);
            finalidade.ValorDespesa1_Ano2 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_1);
            finalidade.ValorDespesa1_Ano3 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq1).Valor_2);

            finalidade.Desc_Despesa2 = listPrimeira.FirstOrDefault(v => v.Seq == seq2).Desc_Despesa ?? "";
            finalidade.ValorDespesa2_Ano1 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_0);
            finalidade.ValorDespesa2_Ano2 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_1);
            finalidade.ValorDespesa2_Ano3 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq2).Valor_2);

            finalidade.Desc_Despesa3 = listPrimeira.FirstOrDefault(v => v.Seq == seq3).Desc_Despesa ?? "";
            finalidade.ValorDespesa3_Ano1 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_0);
            finalidade.ValorDespesa3_Ano2 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_1);
            finalidade.ValorDespesa3_Ano3 = FormatarValorEmReal(listPrimeira.FirstOrDefault(v => v.Seq == seq3).Valor_2);

            finalidade.TotalAno1 = FormatarValorEmReal(totalAno1Primeira);
            finalidade.TotalAno2 = FormatarValorEmReal(totalAno2Primeira);
            finalidade.TotalAno3 = FormatarValorEmReal(totalAno3Primeira);

            finalidade.MetaAno1 = totalAno1Primeira == 0 && totalAno1Segunda == 0 ? "0 %" : ((totalAno1Primeira / (totalAno1Primeira + totalAno1Segunda)) * 100).ToString("n2") + "%";
            finalidade.MetaAno2 = totalAno2Primeira == 0 && totalAno2Segunda == 0 ? "0 %" : ((totalAno2Primeira / (totalAno2Primeira + totalAno2Segunda)) * 100).ToString("n2") + "%";
            finalidade.MetaAno3 = totalAno3Primeira == 0 && totalAno3Segunda == 0 ? "0 %" : ((totalAno3Primeira / (totalAno3Primeira + totalAno3Segunda)) * 100).ToString("n2") + "%";



            return finalidade;
        }

        /// <summary>
        /// Retornar o html da nota
        /// </summary>
        /// <returns></returns>
        private string GetFinalidadeCabecalhoHtml(RelatorioMinisterioFinalidadeDataTransfer finalidade)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<br /><div style='padding-left:21px;font-size: 10px;'>");
            sb.Append("<table sytle='border:0;'> "
                        + "  <tr>                                         "
                        + "    <th style='text-align:right;'>Programa:</th>                         "
                        + "    <th style='text-align:left; padding-left: 6px;'>" + finalidade.Programa + "</th>         "
                        + "  </tr>                                        "
                        + "  <tr>                                         "
                        + "    <th style='text-align:right;'>Objetivo:</th>                         "
                        + "    <td style='text-align:left; padding-left: 6px;'>" + finalidade.Objetivo + "</td>         "
                        + "  </tr>                                        "
                        + "  <tr>                                         "
                        + "    <th style='text-align:right;'>Indicador:</th>                        "
                        + "    <td style='text-align:left; padding-left: 6px;'>" + finalidade.Indicador + "</td>        "
                        + "  </tr>                                        "
                        + "</table>");
            sb.Append("</div> <br />");
            return sb.ToString();
        }

        /// <summary>
        /// Montar a tabela da finalidade
        /// </summary>
        /// <returns></returns>
        private string GetTableHtmlFinalidade(RelatorioMinisterioFinalidadeDataTransfer finalidade)
        {
            StringBuilder sb = new StringBuilder();

            string styleTable = "font-size:7px;margin-top:0px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;width:96%;text-align: center;border: 1px solid #dddddd;padding-left: 3px;margin-left:20px;";
            string styleTd = "border: 1px solid #dddddd;";
            string styleCol = _styleColGreen;


            sb.Append("<table style='" + styleTable + "'>"
                    + "  <tr>                                                    "
                    + "    <th style='background-color: #4CAF50;color:white;padding: 2px;border-left: 1px solid #dddddd;' rowspan='2'>Função</th>                                                                           "
                    + "    <th style='" + _styleColGreen + "' rowspan='2'>Subfunção</th>                                                                        "
                    + "    <th style='" + _styleColGreen + "' colspan='2' rowspan='2'>Ação</th>                                                                 "
                    + "    <th style='" + _styleColGreen + "' colspan='4'>Meta de Desempenho</th>                                                               "
                    + "    <th style='" + styleCol + styleTd + "' rowspan='2'>Grupo de Despesas</th>                                                                "
                    + "    <th style='" + styleCol + styleTd + " width:50px;'>" + finalidade.Ano1 + "</th>                                                                                         "
                    + "    <th style='" + styleCol + styleTd + " width:50px;'>" + finalidade.Ano2 + "</th>                                                                                         "
                    + "    <th style='" + styleCol + styleTd + " width:50px;'>" + finalidade.Ano3 + "</th>                                                                                         "
                    + "  </tr>                                                                                                   "
                    + "  <tr>                                                     "
                    + "    <th style=''></th>                                                                          "
                    + "    <th style='" + styleCol + styleTd + "'>Produto (Unidade)</th>                                                                          "
                    + "    <th style='" + styleCol + styleTd + "'>" + finalidade.Ano1 + "</th>                                                                                         "
                    + "    <th style='" + styleCol + styleTd + "'>" + finalidade.Ano2 + "</th>                                                                                         "
                    + "    <th style='" + styleCol + styleTd + "'>" + finalidade.Ano3 + "</th>                                                                                         "
                    + "    <th style='" + styleCol + styleTd + "'>Original ou Reformulado</th>                                                                      "
                    + "    <th style='" + styleCol + styleTd + "'>Original ou Reformulado</th>                                                                      "
                    + "    <th style='" + styleCol + styleTd + "'>Original ou Reformulado</th>                                                                      "
                    + "  </tr>                                                                                                   "
                    + "                                                                                                          "
                    + "  <tr>                                                                                                    "
                    + "    <td style='" + styleTd + "padding: 2px;border-left: 1px solid #dddddd;border-top: 2px solid #dddddd;' rowspan='3'>" + finalidade.Funcao + "</td>                                                                             "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;' rowspan='3'>" + finalidade.SubFuncao + "</td>                                                                              "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd; width:20px;' rowspan='3'>" + finalidade.CodAcao + "</td>                                                                             "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;' rowspan='3'>" + finalidade.Acao + "</td>                                                                   "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;' rowspan='3'>" + finalidade.Unidade + "</td>              "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd; width:50px;' rowspan='3'>" + finalidade.MetaAno1 + "</td>                                                                                "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd; width:50px;' rowspan='3'>" + finalidade.MetaAno2 + "</td>                                                                                "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd; width:50px;' rowspan='3'>" + finalidade.MetaAno3 + "</td>                                                                                "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;'>" + finalidade.Desc_Despesa1 + "</td>                                                                   "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;'>" + finalidade.ValorDespesa1_Ano1 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;'>" + finalidade.ValorDespesa1_Ano2 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "border-top: 2px solid #dddddd;'>" + finalidade.ValorDespesa1_Ano3 + "</td>                                                                                         "
                    + "  </tr>                                                                                                   "
                    + "  <tr>                                                                                                    "
                    + "    <td style='" + styleTd + "'>" + finalidade.Desc_Despesa2 + "</td>                                                                  "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa2_Ano1 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa2_Ano2 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa2_Ano3 + "</td>                                                                                         "
                    + "  </tr>                                                                                                   "
                    + "  <tr>                                                                                                    "
                    + "    <td style='" + styleTd + "'>" + finalidade.Desc_Despesa3 + "</td>                                                                              "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa3_Ano1 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa3_Ano2 + "</td>                                                                                         "
                    + "    <td style='" + styleTd + "'>" + finalidade.ValorDespesa3_Ano3 + "</td>                                                                                         "
                    + "  </tr>                                                                                                   "
                    + "                                                                                                          "
                    + "  <tr>                                                     "
                    + "    <th style='" + _styleColGreen + "' colspan='8'>TOTAL DO PROGRAMA</th>                                                              "
                    + "    <th style='" + _styleColGreen + "'>TOTAL</th>                                                                                        "
                    + "    <th style='" + _styleColGreen + "'>" + finalidade.TotalAno1 + "</th>                                                                                         "
                    + "    <th style='" + _styleColGreen + "'>" + finalidade.TotalAno2 + "</th>                                                                                         "
                    + "    <th style='" + _styleColGreen + "'>" + finalidade.TotalAno3 + "</th>                                                                                         "
                    + "  </tr>"
                    + "</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Montar a página HTML
        /// </summary>
        public string GetPageHtml(string htmlPageData, string strNumPage, string titulo, string subTitulo, string strDataHora)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html><body style='overflow: auto; height: 100 %;'>");
            sb.Append("<div style='height:98vh; overflow-y:scroll;margin-top: 10px;position: fixed; top: 0mm; width: 100%; text-align: right;'>");
            sb.Append("<table style='margin-top:10px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;border-collapse: collapse;width:100%;'>");

            sb.Append("<tr>");
            sb.Append("<td style='text-align: right;' width='40%'>");
            sb.Append(MountInfoGeracaoLogoRelatorioImgBase64());
            sb.Append("</td>");
            sb.Append("<td width='60%'>");
            sb.Append(MountInfoTitle(strNumPage, titulo, subTitulo, strDataHora));
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style='text-align: left;' colspan='2'>");
            sb.Append(GetNotaHtml());
            sb.Append(htmlPageData);
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</body></html>");
            return sb.ToString();
        }

        /// <summary>
        /// Montar o html da logo
        /// </summary>
        /// <returns></returns>
        private string MountInfoGeracaoLogoRelatorioImgBase64()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='padding-right:10px;'>");
            string strImg = @"<img style='margin: 15px;display: block;' width='200px' src='data:image/png;base64,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'/><br />";
            sb.Append(strImg);

            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// Montar as informações da página em html
        /// </summary>
        private string MountInfoTitle(string strNumPage, string titulo, string subTitulo, string strDataHora)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div style='padding-left:25px;text-align:left;font-weight: bold;font-size: 15px;'>");
            sb.Append("<p style='font-family: Arial, sans-serif;color: #524f4f;'>" + titulo + "</p>");
            sb.Append("<p style='font-family: Trebuchet MS, sans-serif;color:#808080;'>" + subTitulo + "</p>");
            sb.Append("</div>");
            sb.Append("<br />");
            sb.Append("<div style='text-align:right;'>");

            sb.Append("<span style='color: #524f4f; font-size: 11px;margin-bottom:0px;font-weight: bold;'> <em> Página: " + strNumPage + " / 04 </em></span>");
            sb.Append("<span style='color:#808080; font-size: 11px;'> - <em>Data e hora:" + strDataHora + "</em></span>");
            sb.Append("</div>");
            return sb.ToString();
        }
        #endregion
    }
}
