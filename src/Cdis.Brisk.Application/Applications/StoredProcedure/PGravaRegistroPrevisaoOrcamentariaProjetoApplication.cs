using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure;
using System.Collections.Generic;

namespace Cdis.Brisk.Application.Applications.StoredProcedure
{
    public class PGravaRegistroPrevisaoOrcamentariaProjetoApplication : IApplication
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
        public PGravaRegistroPrevisaoOrcamentariaProjetoApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Atualizar ValorDeContaOrcamentariaAnual
        /// </summary>
        public void AtualizarValorDeContaOrcamentariaAnual(List<ParamValorOrcamentarioDomain> listParam)
        {
            foreach (var param in listParam)
            {
                UowApplication.UowService.GetUowService<PGravaRegistroPrevisaoOrcamentariaProjetoService>().AtualizarValorPrevisaoOrcamentaria(param);
                //await Task.Factory.StartNew(() => {

                // });
            }
        }

        #endregion
    }
}
