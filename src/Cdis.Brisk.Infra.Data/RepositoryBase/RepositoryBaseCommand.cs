using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cdis.Brisk.Infra.Data.RepositoryBase
{
    /// <summary>
    /// Classe de reposiório base com métodos para de manipulações de dados
    /// </summary>    
    public class RepositoryBaseCommand<TEntity> : RepositoryBaseQuery<TEntity> where TEntity : class
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>            
        public RepositoryBaseCommand(DbContext contexto) : base(contexto)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>            
        public RepositoryBaseCommand(DbContext contexto, bool proxyEnable, bool lazyLoadingEnable, bool autoDetectChangesEnabled)
        : base(contexto, proxyEnable, lazyLoadingEnable, autoDetectChangesEnabled)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lista as chaves identificadas como KeyAttribute 
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="entidade"></param>
        /// <returns></returns>
        private object[] GetListPrimaryKey<TDomain>(TDomain entidade)
        {
            PropertyInfo[] props = typeof(TDomain).GetProperties();

            List<object> listObject = new List<object>();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo prop = props[i];
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    KeyAttribute authAttr = attr as KeyAttribute;
                    if (authAttr != null)
                    {
                        var field = entidade.GetType().GetProperty(prop.Name).GetValue(entidade, null);
                        listObject.Add(field);
                    }
                }
            }

            return listObject.ToArray();
        }

        #region Save

        /// <summary>
        /// Insere ou altera a entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="isAsync"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        private async Task<TEntity> SaveBase(TEntity entidade, bool isAsync, bool saveChanges = true)
        {
            try
            {
                if (entidade == null) return null;

                //TODO - > Efetuar correção para as tabelas que não possuem identity definida, como as tabelas de Log.
                //A propriedade referente a chave primária da entidade deve possuir o nome de Id
                object[] listKeys = GetListPrimaryKey(entidade);

                if (listKeys.Any())
                {
                    TEntity entityToUpdate = Context.Set<TEntity>().Find(listKeys);

                    if (entityToUpdate == null)
                    {
                        Context.Set<TEntity>().Add(entidade);
                    }
                    else
                    {
                        Context.Entry(entityToUpdate).CurrentValues.SetValues(entidade);
                        Context.Entry(entityToUpdate).State = EntityState.Modified;
                    }
                }
                else
                {
                    Context.Set<TEntity>().Add(entidade);
                }

                if (saveChanges)
                {
                    if (isAsync)
                    {
                        await Context.SaveChangesAsync();
                    }
                    else
                    {
                        Context.SaveChanges();
                    }
                }

                return entidade;

            }

            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string erro = string.Empty;

                foreach (var eve in e.EntityValidationErrors)
                {
                    erro = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        erro += " " + string.Format("- Property: \"{0}\", Error: \"{1}\"",
                              ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw new Exception(erro);
            }
        }

        /// <summary>
        /// Inclui uma nova entidade ou salva as alterações caso a mesma tenha sido pesquisada antes , ou seja, esteja atachada ao contexto.         
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="entidade">A entidade para salvar/atualizar.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        /// <returns>A própria entidade inserida/atualizada. No caso de insert a entidade retorna com o id recém atribuído.</returns>
        public TEntity Save(TEntity entidade, bool saveChanges = true)
        {
            Task.Factory
                    .StartNew(() => SaveBase(entidade, false, saveChanges))
                    .Unwrap<TEntity>()
                    .GetAwaiter()
                    .GetResult();

            return entidade;
        }

        /// <summary>
        /// (Async)Inclui uma nova entidade ou salva as alterações caso a mesma tenha sido pesquisada antes , ou seja, esteja atachada ao contexto.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="entidade">A entidade para salvar/atualizar.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        /// <returns>A própria entidade inserida/atualizada. No caso de insert a entidade retorna com o id recém atribuído.</returns>
        public async Task<TEntity> SaveAsync(TEntity entidade, bool saveChanges = true)
        {
            return await SaveBase(entidade, true, saveChanges);
        }
        #endregion

        #region Delete

        /// <summary>
        /// Deleta a entidade do banco de dados
        /// </summary>         
        private async Task DeleteBase(TEntity entidade, bool saveChanges, bool isAsync)
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;

                if (entidade == null) return;

                if (Context.Entry(entidade).State == EntityState.Detached)
                {
                    Context.Set<TEntity>().Attach(entidade);
                }

                Context.Set<TEntity>().Remove(entidade);

                if (saveChanges)
                {
                    if (isAsync)
                    {
                        await Context.SaveChangesAsync();
                    }
                    else
                    {
                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// Deleta um ou mais registros conforme a condição passada. 
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="where">A condição de exclusão.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        public void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, bool saveChanges = true)
        {
            var itemsParaDelecao = GetWhere(where);

            foreach (TEntity entidade in itemsParaDelecao)
            {
                Task.Factory
                   .StartNew(async () => await DeleteBase(entidade, saveChanges, false))
                   .GetAwaiter()
                   .GetResult();
            }

            if (saveChanges) Context.SaveChanges();
        }

        /// <summary>
        /// Remove uma entidade do banco que já esteja previamente carregada no contexto.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="entidade">A entidade a ser removida.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        public void Delete(TEntity entidade, bool saveChanges = true)
        {
            Task.Factory
                   .StartNew(async () => await DeleteBase(entidade, saveChanges, false))
                   .GetAwaiter()
                   .GetResult();
        }

        /// <summary>
        /// Remove uma entidade através do seu id. Internamente o método irá buscar a entidade, atachar ao contexto e então irá removê-la.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="id">O id da entidade a ser removida.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>
        public void DeleteById(object id, bool saveChanges = true)
        {
            Task.Factory
                   .StartNew(async () => await DeleteBase(GetSingleById(id), saveChanges, false))
                   .GetAwaiter()
                   .GetResult();
        }

        /// <summary>
        /// (Async) Deleta um ou mais registros conforme a condição passada. 
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="where">A condição de exclusão.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        public async Task DeleteAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, bool saveChanges = true)
        {
            var itemsParaDelecao = GetWhere(where);

            foreach (TEntity entidade in itemsParaDelecao)
            {
                await DeleteBase(entidade, saveChanges, false);
            }

            if (saveChanges) await SaveChangesAsync();
        }

        /// <summary>
        /// (Async) Remove uma entidade do banco que já esteja previamente carregada no contexto.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="entidade">A entidade a ser removida.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>        
        public async Task DeleteAsync(TEntity entidade, bool saveChanges = true)
        {
            await DeleteBase(entidade, saveChanges, false);
        }

        /// <summary>
        /// (Async) Remove uma entidade através do seu id. Internamente o método irá buscar a entidade, atachar ao contexto e então irá removê-la.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
        /// <param name="id">O id da entidade a ser removida.</param>
        /// <param name="saveChanges">Salvar as alterações.</param>
        public async Task DeleteByIdAsync(object id, bool saveChanges = true)
        {
            await DeleteBase(GetSingleById(id), saveChanges, false);
        }

        #endregion

        #region ExecuteSqlCommand
        /// <summary>
        /// Executa um comando sql (query/procedure) no banco e retorna o número de registros afetados. 
        /// </summary>      
        public void ExecuteSqlCommand(string sqlCommand, params object[] sqlParams)
        {
            Context.Database.ExecuteSqlCommand(sqlCommand, sqlParams);
        }
        #endregion

        #region SaveChanges

        /// <summary>
        /// Salva as operações que estão no contexto no banco de dados
        /// </summary>
        public void SaveChanges()
        {
            base.Context.SaveChanges();
        }

        /// <summary>
        /// (Async) Salva as operações que estão no contexto no banco de dados
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await base.Context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Destructor

        /// <summary>
        /// Descructor
        /// </summary>
        ~RepositoryBaseCommand()
        {
            base.Dispose(true);
        }
        #endregion
    }
}
