using Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Core.Util;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTabalho;
using System;
using System.Collections.Generic;

namespace Cdis.Brisk.Service.Services.Relatorio.PlanoTrabalho
{
    /// <summary>
    /// Classe de serviço RelatorioPlanoTrabalhoService
    /// </summary>
    public class RelatorioPlanoTrabalhoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço RelatorioPlanoTrabalhoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço RelatorioPlanoTrabalhoService
        /// </summary>
        private UnitOfWorkService UowService
        {
            get
            {
                return _unitOfWorkService;
            }
        }

        #endregion

        #region Constructor        
        /// <summary>
        /// Construtor da classe de serviço RelatorioPlanoTrabalhoService
        /// </summary>
        public RelatorioPlanoTrabalhoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar os itens do plano de trabalho da entidade
        /// </summary>        
        public List<RelatorioPlanoTrabalhoDomain> GetListRelatorioPlanoTrabalhoEntidade(int codEntidade, int codUsuario, int? codCarteira)
        {
            return UowService.UowRepository.GetUowRepository<RelatorioPlanoTrabalhoRepository>().GetListRelatorioPlanoTrabalhoDomain(codEntidade, codUsuario, null, codCarteira);
        }


        /// <summary>
        /// Listar os itens do plano de trabalho da unidade
        /// </summary>        
        public List<RelatorioPlanoTrabalhoDomain> GetListRelatorioPlanoTrabalhoUnidade(int codEntidade, int codUsuario, int codUnidade, int? codCarteira)
        {
            return UowService.UowRepository.GetUowRepository<RelatorioPlanoTrabalhoRepository>().GetListRelatorioPlanoTrabalhoDomain(codEntidade, codUsuario, codUnidade, codCarteira);
        }


        /// <summary>
        /// FAKE - Listar os itens do plano de trabalho da unidade
        /// </summary>        
        public List<RelatorioPlanoTrabalhoDomain> GetListRelatorioPlanoTrabalhoUnidadeFake(int countItem)
        {
            List<RelatorioPlanoTrabalhoDomain> list = new List<RelatorioPlanoTrabalhoDomain>();

            for (int codProjeto = 1; codProjeto <= countItem; codProjeto++)
            {
                list.Add(new RelatorioPlanoTrabalhoDomain
                {
                    CodigoProjeto = codProjeto,
                    NomeProjeto = StringUtil.GetValueFake(250),
                    TipoProjeto = StringUtil.GetValueFake(55),
                    UnidadeNegocio = StringUtil.GetValueFake(95),
                    Objetivos = StringUtil.GetValueFake(4000),
                    ResponsavelTecnico = StringUtil.GetValueFake(50),
                    CarteiraPrincipal = StringUtil.GetValueFake(145),

                    PublicoAlvo = StringUtil.GetValueFake(35),
                    Observacao = StringUtil.GetValueFake(2000),

                    Classificacao = StringUtil.GetValueFake(35),
                    ObjetivoEstrategico = StringUtil.GetValueFake(160),
                    LinhaAcao = StringUtil.GetValueFake(60),
                    AreaAtuacao = StringUtil.GetValueFake(15),
                    Natureza = StringUtil.GetValueFake(13),
                    Funcao = StringUtil.GetValueFake(14),
                    Subfuncao = StringUtil.GetValueFake(15),
                    Programa = StringUtil.GetValueFake(13),
                    ListPlanoTrabalhoEntrega = GetListPlanoTrabalhoEntregaFake(new Random().Next(100, 200)),
                    ListPlanoTrabalhoProduto = GetListPlanoTrabalhoProdutoFake(new Random().Next(100, 200)),
                    ListPlanoTrabalhoResultadoEsperado = GetListPlanoTrabalhoResultadoEsperadoFake(new Random().Next(100, 200))
                });
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<PlanoTrabalhoProjetoResultadoEsperadoDomain> GetListPlanoTrabalhoResultadoEsperadoFake(int countItem)
        {
            List<PlanoTrabalhoProjetoResultadoEsperadoDomain> list = new List<PlanoTrabalhoProjetoResultadoEsperadoDomain>();

            for (int i = 1; i <= countItem; i++)
            {
                list.Add(new PlanoTrabalhoProjetoResultadoEsperadoDomain
                {
                    Indicador = StringUtil.GetValueFake(50),
                    Meta = StringUtil.GetValueFake(50),
                    Prazo = DateTime.Now,
                    Transformacao = StringUtil.GetValueFake(2000)
                });
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<PlanoTrabalhoProjetoProdutoDomain> GetListPlanoTrabalhoProdutoFake(int countItem)
        {
            List<PlanoTrabalhoProjetoProdutoDomain> list = new List<PlanoTrabalhoProjetoProdutoDomain>();

            for (int i = 1; i <= countItem; i++)
            {
                list.Add(new PlanoTrabalhoProjetoProdutoDomain
                {
                    Descricao = StringUtil.GetValueFake(68)
                });
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<PlanoTrabalhoProjetoEntregaDomain> GetListPlanoTrabalhoEntregaFake(int countItem)
        {
            List<PlanoTrabalhoProjetoEntregaDomain> list = new List<PlanoTrabalhoProjetoEntregaDomain>();

            for (int i = 1; i <= countItem; i++)
            {
                list.Add(new PlanoTrabalhoProjetoEntregaDomain
                {
                    Acao = StringUtil.GetValueFake(96),
                    EntregaPrevista = StringUtil.GetValueFake(96),
                    EntregaRealizada = StringUtil.GetValueFake(20),
                    StatusEntrega = StringUtil.GetValueFake(10)
                });
            }

            return list;
        }
        #endregion
    }
}
