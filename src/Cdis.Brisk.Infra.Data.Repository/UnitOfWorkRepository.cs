using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Core.UnitOfWork;
using System;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository
{
    public class UnitOfWorkRepository<TContext> : UnitOfWorkMapping<IRepository>, IDisposable where TContext : DomainContext
    {
        #region Propriedades                     
        protected readonly DbContext _context;
        public DbContext Context
        {
            get { return _context; }
        }
        private readonly string _stringConnection;
        #endregion

        #region Constructor
        public UnitOfWorkRepository(string strCon)
        {
            this._context = (TContext)Activator.CreateInstance(typeof(TContext), strCon);
            this._stringConnection = strCon;
        }
        #endregion

        /// <summary>
        /// Buscar a string de conexão
        /// </summary>        
        internal string GetStringConnection()
        {
            return _stringConnection;
        }

        /// <summary>
        /// Listar as propriedades e métodos da classe de repositório
        /// </summary>        
        public TEntityRepository GetUowRepository<TEntityRepository>() where TEntityRepository : IRepository
        {
            return GetUow<TEntityRepository>(_context, this);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~UnitOfWorkRepository()
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
