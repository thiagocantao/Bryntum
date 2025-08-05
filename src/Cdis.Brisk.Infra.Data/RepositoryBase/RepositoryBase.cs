using System;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.RepositoryBase
{
    public class RepositoryBase : IDisposable
    {
        #region Properties and Attributes
        private readonly DbContext _context;
        private int _timeOutSqlCommand = 200;
        public int TimeOutSqlCommand
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["TimeOutSqlCommand"] != null)
                {
                    _timeOutSqlCommand = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeOutSqlCommand"].ToString());
                }
                return _timeOutSqlCommand;
            }
            // set => _timeOutSqlCommand = value; 
        }
        protected DbContext Context {
            
            get {
                  _context.Database.CommandTimeout = TimeOutSqlCommand;
                  return _context; 
            } 

            
        
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>            
        public RepositoryBase(DbContext contexto, bool proxyEnable, bool lazyLoadingEnable, bool autoDetectChangesEnabled)
        {
            // SERIALIZE WILL FAIL WITH PROXIED ENTITIES
            _context.Configuration.ProxyCreationEnabled = proxyEnable;

            // ENABLING COULD CAUSE ENDLESS LOOPS AND PERFORMANCE PROBLEMS
            _context.Configuration.LazyLoadingEnabled = lazyLoadingEnable;
            _context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }

        /// <summary>
        /// Contructor
        /// </summary>
        public RepositoryBase(DbContext contexto)
        {
            _context = contexto;
        }
        #endregion

        #region GetUsingSqlQuery

        /// <summary>
        /// Executa um comando SQL (query) e retorna conforme o tipo da entidade identificado no método.
        /// </summary>
        /// <typeparam name="TEntity">O Tipo de outra entidade.</typeparam>
        /// <param name="sqlCommand">O comando SQL.</param>
        /// <returns>A lista de registros da entidade identificada no método.</returns> 
        protected IQueryable<TOtherEntity> GetUsingSqlQuery<TOtherEntity>(string sqlCommand, params object[] sqlParams) where TOtherEntity : class
        {
            if (String.IsNullOrEmpty(sqlCommand)) { return null; }
            return _context.Database.SqlQuery<TOtherEntity>(sqlCommand, sqlParams).AsQueryable();
        }


        /// <summary>
        /// Executar um comando SQL (query) e retorna conforme o procedimento da query.
        /// </summary>        
        /// <param name="sqlCommand">O comando SQL.</param>        
        protected object ExecSqlQuery(string sqlCommand, params object[] sqlParams)
        {
            if (String.IsNullOrEmpty(sqlCommand)) { return null; }
            return _context.Database.ExecuteSqlCommand(sqlCommand, sqlParams);
        }

        #endregion

        #region Disposible Ref: https://msdn.microsoft.com/pt-br/library/system.idisposable(v=vs.110).aspx

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Dispose();
            }
            disposed = true;
        }

        #endregion
    }
}
