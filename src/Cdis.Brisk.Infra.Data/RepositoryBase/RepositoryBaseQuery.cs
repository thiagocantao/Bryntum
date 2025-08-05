using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cdis.Brisk.Infra.Data.RepositoryBase
{
    /// <summary>
    /// Classe de reposiório base com métodos para consulta de dados
    /// </summary>    
    public class RepositoryBaseQuery<TEntity> : RepositoryBase, IDisposable where TEntity : class
    {
        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>            
        public RepositoryBaseQuery(DbContext contexto, bool proxyEnable, bool lazyLoadingEnable, bool autoDetectChangesEnabled)
            : base(contexto, proxyEnable, lazyLoadingEnable, autoDetectChangesEnabled)
        {

        }

        
        
        /// <summary>
        /// Contructor
        /// </summary>
        public RepositoryBaseQuery(DbContext contexto) : base(contexto)
        {
        }
        #endregion

        #region Methods

        #region All

        /// <summary>
        /// Verifica se todos os registros da entidade atentem a condição passada como parâmetro
        /// </summary>    
        public virtual async Task<bool> AllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Set<TEntity>().AllAsync(@where);
        }

        /// <summary>
        /// Verifica se todos os registros da entidade atentem a condição passada como parâmetro
        /// </summary>    
        public virtual bool All(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().All(@where);
        }

        #endregion

        #region Any

        /// <summary>
        /// Verifica se retorna algum registro na codição informada.
        /// </summary>        
        public virtual bool Any(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().Any(@where);
        }

        /// <summary>
        /// (Async) Verifica se retorna algum registro na codição informada.
        /// </summary>        
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Set<TEntity>().AnyAsync(@where);
        }

        #endregion        

        #region GetSingleOrDefault
        /// <summary>
        /// Retorna um único objeto do tipo da entidade, caso contrário, retorna NULL
        /// </summary>                
        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().SingleOrDefault(where);
        }

        /// <summary>
        /// Retorna um único objeto do tipo da entidade sem depedências relacionadas a entidade.        
        /// </summary>                
        public TEntity GetSingleOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().AsNoTracking().SingleOrDefault(where);
        }

        /// <summary>
        /// (Async) Retorna um único objeto do tipo da entidade sem depedências relacionadas a entidade.              
        /// </summary>                
        public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(where);
        }

        /// <summary>
        /// (Async and AsNoTracking) Retorna um único objeto do tipo da entidade sem depedências relacionadas a entidade.          
        /// </summary>                
        public async Task<TEntity> GetSingleOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(where);
        }
        #endregion

        #region GetSingleById
        /// <summary>
        /// Localiza uma entidade pela sua chave primária (id). 
        /// Se a entidade já estiver atachada ao contexto, retorna-a sem fazer uma nova requisição ao banco. 
        /// Se não estiver atachada, busca no banco. Caso encontre, atacha e retorna a entidade. Do contrário retorna null.
        /// </summary>        
        public TEntity GetSingleById(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// (Async) Localiza uma entidade pela sua chave primária (id). 
        /// Se a entidade já estiver atachada ao contexto, retorna-a sem fazer uma nova requisição ao banco. 
        /// Se não estiver atachada, busca no banco. Caso encontre, atacha e retorna a entidade. Do contrário retorna null.
        /// </summary>        
        public virtual async Task<TEntity> GetSingleByIdAsync(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        #endregion

        #region GetFirstOrDefault

        /// <summary>
        /// Retorna o primeiro elemento de uma sequência que satisfaça a condição passada ou retorna null caso nenhum elemento satisfaça.
        /// </summary>       
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().FirstOrDefault(where);
        }

        /// <summary>
        /// Retorna o primeiro elemento de uma sequência que satisfaça a condição passada ou retorna null caso nenhum elemento satisfaça.
        /// </summary>  
        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> @where)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(@where);
        }

        /// <summary>
        /// (Async and AsNoTracking)
        /// Retorna o primeiro elemento de uma sequência que satisfaça a condição passada ou retorna null caso nenhum elemento satisfaça.
        /// </summary>  
        public virtual async Task<TEntity> GetFirstOrDefaultAsyncAsNoTracking(Expression<Func<TEntity, bool>> @where)
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(@where);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Retorna todos os registros de uma determinada entidade. Como opção, pode-se passar propriedades de navegação para serem carregadas na mesma query através de inner join (eager loading).       
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="includeReferences">Os campos que se deseja carregar (NULL para carregar todos).</param>
        /// <returns>A lista de registros da entidade.</returns>
        protected IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        /// Retorna todos os registros de uma determinada entidade identificada no método. 
        /// Como opção, pode-se passar propriedades de navegação para serem carregadas na mesma query através de inner join (eager loading).       
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="includeReferences">Os campos que se deseja carregar (NULL para carregar todos).</param>
        /// <returns>A lista de registros da entidade.</returns>
        private IQueryable<TOtherEntity> GetAll<TOtherEntity>() where TOtherEntity : class
        {
            return Context.Set<TOtherEntity>();
        }

        /// <summary>
        /// Retorna todos os registros de uma determinada entidade desabilitando o tracking do entity. Dessa forma qualquer alterção feita não será identificada. 
        /// Recomenda-se para selects em geral por ser mais performático. Como opção, pode-se passar propriedades de navegação para serem carregadas na mesma 
        /// query através de inner join (eager loading).       
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>        
        /// <returns>A lista de registros da entidade.</returns>
        protected virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return GetAll().AsNoTracking();
        }

        #endregion

        #region GetWhere

        /// <summary> 
        /// Retorna os registros conforme a condição especificada. Se não for especificada a condição, 
        /// retorna NULL. 
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="where">Cláusula WHERE.</param>
        /// <returns>Lista de registros da entidade.</returns>
        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> where)
        {
#pragma warning disable S1168 // Empty arrays and collections should be returned instead of null
            if (where == null) { return null; }
#pragma warning restore S1168 // Empty arrays and collections should be returned instead of null

            return Context.Set<TEntity>().Where(where);
        }

        /// <summary> 
        /// Retorna todos os registros de uma determinada entidade conforme a condição passada desabilitando o tracking do entity. 
        /// Recomenda-se para selects em geral por ser mais performático.      
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="where">Cláusula WHERE.</param> 
        /// <returns>Lista de registros da entidade.</returns>
        public virtual IQueryable<TEntity> GetWhereAsNoTracking(Expression<Func<TEntity, bool>> where)
        {
            // Se não for passado um filtro, retorna todos 
            if (where == null) { return null; }

            return Context.Set<TEntity>().AsNoTracking<TEntity>().Where(where);
        }

        #endregion

        /// <summary>
        /// Executa um comando SQL (query) e retorna conforme o tipo da entidade identificado no método.
        /// </summary>
        /// <typeparam name="TEntity">O Tipo da entidade.</typeparam>
        /// <param name="sqlCommand">O comando SQL.</param>
        /// <returns>A lista de registros da entidade.</returns> 
        protected IQueryable<TEntity> GetUsingSqlQuery(string sqlCommand = "", params object[] sqlParams)
        {
            if (String.IsNullOrEmpty(sqlCommand)) { return null; }
            return Context.Database.SqlQuery<TEntity>(sqlCommand, sqlParams).AsQueryable();
        }

        /// <summary>
        /// Executa um comando SQL (query) e retorna conforme o tipo da entidade identificado no método.
        /// </summary>
        /// <typeparam name="TEntity">O Tipo da entidade.</typeparam>
        /// <param name="sqlCommand">O comando SQL.</param>
        /// <returns>A lista de registros da entidade.</returns> 
        protected DbDataReader GetDbDataReader(string sqlCommand = "", params object[] sqlParams)
        {

            // If using Code First we need to make sure the model is built before we open the connection
            // This isn't required for models created with the EF Designer
            Context.Database.Initialize(force: false);

            // Create a SQL command to execute the sproc
            var cmd = Context.Database.Connection.CreateCommand();
            cmd.CommandTimeout = this.TimeOutSqlCommand;
            cmd.CommandText = sqlCommand;
            //cmd.Parameters.AddRange(sqlParams);

            try
            {
                Context.Database.Connection.Open();
                // Run the sproc
                return cmd.ExecuteReader();
            }
            finally
            {
                //Context.Database.Connection.Close();
            }

        }
        #endregion

        #region Disposible Ref: https://msdn.microsoft.com/pt-br/library/system.idisposable(v=vs.110).aspx

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public new void Dispose()
        {
            try
            {
                //Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception exc)
            {
                throw exc;
            }
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

        ///// <summary>
        ///// Descructor
        ///// </summary>
        //~RepositoryBaseQuery()
        //{
        //    Dispose(true);
        //}
        #endregion
    }
}
