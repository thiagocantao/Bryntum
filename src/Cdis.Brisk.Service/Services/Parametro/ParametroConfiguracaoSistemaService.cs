using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Parametro
{
    public class ParametroConfiguracaoSistemaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaDependenciaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaDependenciaService
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
        /// Construtor da classe de serviço CronogramaDependenciaService
        /// </summary>
        public ParametroConfiguracaoSistemaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods       

        /// <summary>
        /// Listar os parâmetros da entidade
        /// </summary>        
        public List<ParametroConfiguracaoSistemaDomain> GetListParametroEntidade(int codEntidade)
        {
            return UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>().GetIQueryableParametro(codEntidade).ToList();
        }


        /// <summary>
        /// Listar os parâmetros da entidade relacionados as informações de SMTP
        /// </summary>        
        public List<ParametroConfiguracaoSistemaDomain> GetListParametroEntidadeInfoSmtp(int codEntidade)
        {
            return UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>().GetWhere(p => p.CodigoEntidade == codEntidade && p.Parametro.Contains("smtp")).ToList();
        }


        /// <summary>
        /// Listar os parâmetros da entidade relacionados as informações de SMTP
        /// </summary>        
        public int GetParametroSistemaTempoMinutosValidadeTokenRs(int codEntidade)
        {
            int qtdMintutos = 1440;
            var parametro = UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>().GetFirstOrDefault(p => p.CodigoEntidade == codEntidade && p.Parametro.Contains("TempoValidadeTokenRS"));
            if (parametro != null)
            {
                qtdMintutos = Convert.ToInt32(parametro.Valor);
            }

            return qtdMintutos;
        }

        public T GetValorParametroSistema<T>(int codEntidade, string nameParameter)
        {
            var parametro = UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>().GetFirstOrDefault(p => p.CodigoEntidade == codEntidade && p.Parametro.Contains(nameParameter));
            return (T)Convert.ChangeType(parametro.Valor, typeof(T));
        }


        /// <summary>
        /// Listar os parâmetros da entidade filtrando pelos parâmetros da variável/parâmetro listParam
        /// </summary>        
        public List<ParametroConfiguracaoSistemaDomain> GetListParametroEntidade(int codigoEntidade, params string[] listParam)
        {
            return UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>()
                  .GetIQueryableParametro(codigoEntidade)
                  .Where(p => listParam.Contains(p.Parametro))
                  .ToList();
        }

        /// <summary>
        /// Listar os parâmetros da entidade filtrando pelos parâmetros da variável/parâmetro listParametro
        /// </summary>        
        public List<ParametroConfiguracaoSistemaDomain> GetListParametroEntidade(int codigoEntidade, List<string> listParametro)
        {
            return GetListParametroEntidade(codigoEntidade, string.Join(",", listParametro.ToArray()).Split(','));
        }


        /// <summary>
        /// Buscar o parametro ClickOnceChrome da entidade
        /// </summary>        
        public ParametroConfiguracaoSistemaDomain GetParametroEntidadeClickOnceChrome(int codEntidade)
        {
            return UowService.UowRepository.GetUowRepository<ParametroConfiguracaoSistemaRepository>().GetIQueryableParametro(codEntidade).Where(p => p.Parametro == "ClickOnceChrome").FirstOrDefault();
        }
        #endregion
    }
}
