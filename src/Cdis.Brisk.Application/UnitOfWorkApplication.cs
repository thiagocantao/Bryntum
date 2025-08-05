using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Core.UnitOfWork;
using Cdis.Brisk.Service;
using System;

namespace Cdis.Brisk.Application
{
    public class UnitOfWorkApplication : UnitOfWorkMapping<IApplication>, IDisposable
    {
        #region Propriedades       

        /// <summary>
        /// UnitOfWorkService
        /// </summary>
        private UnitOfWorkService _uowService { get; set; }

        /// <summary>
        /// UnitOfWorkService
        /// </summary>
        public UnitOfWorkService UowService
        {
            get
            {
                _uowService = _uowService ?? new UnitOfWorkService(_strCon);
                return _uowService;
            }
        }

        private string _strCon;
        #endregion

        #region Constructor
        public UnitOfWorkApplication(string strCon)
        {
            _strCon = strCon;
        }
        #endregion

        /// <summary>
        /// GetUowApplication
        /// </summary>
        /// <typeparam name="TEntityApplication"></typeparam>
        /// <returns></returns>
        public TEntityApplication GetUowApplication<TEntityApplication>() where TEntityApplication : IApplication
        {
            // return GetUow
            return GetUow<TEntityApplication>(this);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~UnitOfWorkApplication()
        {
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
