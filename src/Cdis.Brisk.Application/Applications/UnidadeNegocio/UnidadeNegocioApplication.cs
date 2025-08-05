using Cdis.Brisk.DataTransfer.UnidadeNegocio;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.UnidadeNegocio;
using System.Collections.Generic;

namespace Cdis.Brisk.Application.Applications.UnidadeNegocio
{
    /// <summary>
    /// Classe de aplicação UnidadeNegocioApplication
    /// </summary>
    public class UnidadeNegocioApplication : IApplication
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de aplicação UnidadeNegocioApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação UnidadeNegocioApplication
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
        /// Construtor da classe de aplicação UnidadeNegocioApplication
        /// </summary>
        public UnidadeNegocioApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods


        /// <summary>
        /// GetListUnidadeNegocioDataTranfer
        /// </summary>        
        public List<UnidadeNegocioDataTransfer> GetListUnidadeNegocioItemTodosDataTransfer(int codUsuario, int codEntidade)
        {
            return GetListUnidadeNegocioDataTransfer(codUsuario, codEntidade, true);
        }
        /// <summary>
        /// GetListUnidadeNegocioDataTranfer
        /// </summary>        
        public List<UnidadeNegocioDataTransfer> GetListUnidadeNegocioDataTransfer(int codUsuario, int codEntidade, bool isIncluirItemTodos)
        {
            var listUnidadeNegocio = UowApplication.UowService.GetUowService<UnidadeNegocioService>().GetListUnidadeNegocioUsuario(codUsuario, codEntidade);

            List<UnidadeNegocioDataTransfer> listUnidadeNegocioDataTransfer = new List<UnidadeNegocioDataTransfer>();

            if (isIncluirItemTodos)
            {
                listUnidadeNegocioDataTransfer.Add(new UnidadeNegocioDataTransfer
                {
                    CodigoUnidadeNegocio = 0,
                    NomeUnidadeNegocio = "Todos"
                });
            }

            foreach (var unidade in listUnidadeNegocio)
            {
                UnidadeNegocioDataTransfer unidadeNegocioDataTranfer = new UnidadeNegocioDataTransfer();
                unidade.CopyProperties(ref unidadeNegocioDataTranfer);
                listUnidadeNegocioDataTransfer.Add(unidadeNegocioDataTranfer);
            }
            return listUnidadeNegocioDataTransfer;
        }

        #endregion
    }
}
