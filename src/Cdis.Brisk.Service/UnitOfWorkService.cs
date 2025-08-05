using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Core.UnitOfWork;
using Cdis.Brisk.Infra.Data;
using Cdis.Brisk.Infra.Data.Repository;
using System;

namespace Cdis.Brisk.Service
{
    public class UnitOfWorkService : UnitOfWorkMapping<IService>, IDisposable, IService
    {
        #region Attributes and Properties    

        /// <summary>
        /// _uowRepository
        /// </summary>
        private UnitOfWorkRepository<DomainContext> _uowRepository { get; set; }
        /// <summary>
        /// UowRepository
        /// </summary>
        internal UnitOfWorkRepository<DomainContext> UowRepository
        {
            get
            {
                _uowRepository = _uowRepository ?? new UnitOfWorkRepository<DomainContext>(_strCon);
                return _uowRepository;
            }
        }

        #endregion

        #region Construtor       
        private string _strCon;
        public UnitOfWorkService(string strCon)
        {
            _strCon = strCon;
        }
        #endregion

        /// <summary>
        /// GetUowService
        /// </summary>
        /// <typeparam name="TEntityService"></typeparam>
        /// <returns></returns>
        public TEntityService GetUowService<TEntityService>() where TEntityService : IService
        {
            // GetUow
            return GetUow<TEntityService>(this);
        }

        /// <summary>
        /// UnitOfWorkService
        /// </summary>
        ~UnitOfWorkService()
        {
            // Dispose
            Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
