using Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTabalho
{
    /// <summary>
    /// Classe de repositório RelatorioPlanoTrabalhoRepository
    /// </summary>
    public class RelatorioPlanoTrabalhoRepository : RepositoryBaseQuery<RelatorioPlanoTrabalhoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório RelatorioPlanoTrabalhoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório RelatorioPlanoTrabalhoRepository
        /// </summary>
        private UnitOfWorkRepository<DomainContext> UowRepository
        {
            get
            {
                return _unitOfWorkRepository;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de repositório RelatorioPlanoTrabalhoRepository
        /// </summary>
        public RelatorioPlanoTrabalhoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Listar as informações do plano de trabalho.
        /// </summary>        
        public List<RelatorioPlanoTrabalhoDomain> GetListRelatorioPlanoTrabalhoDomain(int codEntidade, int codUsuario, int? codUnidadeNegocio, int? codCarteira)
        {
            // If using Code First we need to make sure the model is built before we open the connection
            // This isn't required for models created with the EF Designer

            try
            {
                string query = string.Format("EXEC [{0}].[{1}].[p_SESCOOP_RelatoriosProjetos] {2} ,{3} ,{4} ,{5}",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codEntidade, 
                codUsuario, 
                codUnidadeNegocio.HasValue ? codUnidadeNegocio.Value.ToString() : "NULL", 
                codCarteira.HasValue ? codCarteira.Value.ToString() : "NULL");

                // Create a SQL command to execute the sproc
                var cmd = Context.Database.Connection.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandTimeout = this.TimeOutSqlCommand;

                Context.Database.Connection.Open();
                // Run the sproc
                var reader = cmd.ExecuteReader();

                //Info - Plano de trabalho
                var listProjetoGeralPlanoTrabalhoResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<ProjetoGeralPlanoTrabalhoResultSet>(reader)
                            .ToList();
                reader.NextResult();

                var listProjetoClassificacaoResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<ProjetoClassificacaoResultSet>(reader)
                            .ToList();
                reader.NextResult();


                var listProjetoPublicoAlvoResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<ProjetoPublicoAlvoResultSet>(reader)
                            .ToList();
                reader.NextResult();


                //<-------Info - Plano de trabalho

                //Lista ResultadoEsperado
                var listResultadoEsperadoResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<ResultadoEsperadoResultSet>(reader)
                            .ToList();

                //<-----------Lista ResultadoEsperado                
                reader.NextResult();

                //Lista de Produto
                var listProdutoResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<ProdutoResultSet>(reader)
                            .ToList();
                reader.NextResult();
                //<---------------Lista de Produto

                //Lista de entrega
                var listEntregaResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<EntregaResultSet>(reader)
                            .ToList();
                reader.NextResult();
                //<---------------Lista de entrega

                //Lista de entrega da atividade
                var listEntregaAtividadeResultSet = ((IObjectContextAdapter)this.Context)
                            .ObjectContext
                            .Translate<EntregaAtividadeResultSet>(reader)
                            .ToList();
                reader.NextResult();
                //<---------------Lista de entrega da atividade

                return MountListRelatorioPlanoTrabalhoDomain(listProjetoGeralPlanoTrabalhoResultSet, 
                    listProjetoPublicoAlvoResultSet, 
                    listProjetoClassificacaoResultSet, 
                    listResultadoEsperadoResultSet, 
                    listProdutoResultSet, 
                    listEntregaResultSet,
                    listEntregaAtividadeResultSet);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                Context.Database.Connection.Close();
            }

        }

        /// <summary>
        /// MountListRelatorioPlanoTrabalhoDomain
        /// </summary>        
        private List<RelatorioPlanoTrabalhoDomain> MountListRelatorioPlanoTrabalhoDomain(
            List<ProjetoGeralPlanoTrabalhoResultSet> listProjetoGeralPlanoTrabalhoResultSet,
            List<ProjetoPublicoAlvoResultSet> listProjetoPublicoAlvoResultSet,
            List<ProjetoClassificacaoResultSet> listProjetoClassificacaoResultSet,
            List<ResultadoEsperadoResultSet> listResultadoEsperadoResultSet,
            List<ProdutoResultSet> listProdutoResultSet,
            List<EntregaResultSet> listEntregaResultSet,
            List<EntregaAtividadeResultSet> listEntregaAtividadeResultSet)
        {
            List<RelatorioPlanoTrabalhoDomain> listPlanoTrabalhoDomain = new List<RelatorioPlanoTrabalhoDomain>();

            foreach (var infoProjeto in listProjetoGeralPlanoTrabalhoResultSet)
            {
                var projeto = MountRelatorioPlanoTrabalhoDomain(
                                    infoProjeto,
                                    listProjetoPublicoAlvoResultSet.FirstOrDefault(p => p.CodigoProjeto == infoProjeto.CodigoProjeto),
                                    listProjetoClassificacaoResultSet.FirstOrDefault(p => p.CodigoProjeto == infoProjeto.CodigoProjeto),
                                    listResultadoEsperadoResultSet.Where(p => p.CodigoProjeto == infoProjeto.CodigoProjeto).ToList(),
                                    listProdutoResultSet.Where(p => p.CodigoProjeto == infoProjeto.CodigoProjeto).ToList(),
                                    listEntregaResultSet.Where(p => p.CodigoProjeto == infoProjeto.CodigoProjeto).ToList(),
                                    listEntregaAtividadeResultSet.Where(p => p.CodigoProjeto == infoProjeto.CodigoProjeto).ToList()
                               );

                listPlanoTrabalhoDomain.Add(projeto);
            }

            return listPlanoTrabalhoDomain;
        }


        /// <summary>
        /// MountRelatorioPlanoTrabalhoDomain
        /// </summary>        
        private RelatorioPlanoTrabalhoDomain MountRelatorioPlanoTrabalhoDomain(
                ProjetoGeralPlanoTrabalhoResultSet projetoGeral,
                ProjetoPublicoAlvoResultSet projetoPublicoAlvo,
                ProjetoClassificacaoResultSet projetoClassificacao,
                List<ResultadoEsperadoResultSet> listResultadoEsperadoResultSet,
                List<ProdutoResultSet> listProdutoResultSet,
                List<EntregaResultSet> listEntregaResultSet, 
                List<EntregaAtividadeResultSet> listEntregaAtividadeResultSet)
        {
            RelatorioPlanoTrabalhoDomain planoTrabalho = new RelatorioPlanoTrabalhoDomain();
            projetoGeral.CopyProperties(ref planoTrabalho);
            projetoPublicoAlvo.CopyProperties(ref planoTrabalho);
            projetoClassificacao.CopyProperties(ref planoTrabalho);


            planoTrabalho.ListPlanoTrabalhoEntrega = MountListPlanoTrabalhoEntrega(listEntregaResultSet);
            planoTrabalho.ListPlanoTrabalhoAtividadeEntrega = MountListPlanoTrabalhoEntrega(listEntregaAtividadeResultSet);
            planoTrabalho.ListPlanoTrabalhoProduto = MountListPlanoTrabalhoProduto(listProdutoResultSet);
            planoTrabalho.ListPlanoTrabalhoResultadoEsperado = MountListPlanoTrabalhoResultadoEsperado(listResultadoEsperadoResultSet);

            return planoTrabalho;
        }

        /// <summary>
        /// MountListPlanoTrabalhoResultadoEsperado
        /// </summary>        
        private List<PlanoTrabalhoProjetoResultadoEsperadoDomain> MountListPlanoTrabalhoResultadoEsperado(List<ResultadoEsperadoResultSet> listResultadoEsperadoResultSet)
        {
            List<PlanoTrabalhoProjetoResultadoEsperadoDomain> listPlanoTrabalhoProjetoResultadoEsperado = new List<PlanoTrabalhoProjetoResultadoEsperadoDomain>();
            foreach (var resultadoEsperadoResult in listResultadoEsperadoResultSet.Where(e => !string.IsNullOrEmpty(e.Indicador) && !string.IsNullOrEmpty(e.Meta) && !string.IsNullOrEmpty(e.Transformacao)).ToList())
            {
                PlanoTrabalhoProjetoResultadoEsperadoDomain resultado = new PlanoTrabalhoProjetoResultadoEsperadoDomain();
                resultadoEsperadoResult.CopyProperties(ref resultado);
                listPlanoTrabalhoProjetoResultadoEsperado.Add(resultado);
            }

            return listPlanoTrabalhoProjetoResultadoEsperado;
        }

        /// <summary>
        /// MountListPlanoTrabalhoProduto
        /// </summary>        
        private List<PlanoTrabalhoProjetoProdutoDomain> MountListPlanoTrabalhoProduto(List<ProdutoResultSet> listProdutoResultSet)
        {
            List<PlanoTrabalhoProjetoProdutoDomain> listPlanoTrabalhoProjetoProdutoDomain = new List<PlanoTrabalhoProjetoProdutoDomain>();

            foreach (var produtoResult in listProdutoResultSet.Where(e => !string.IsNullOrEmpty(e.Descricao)).ToList())
            {
                PlanoTrabalhoProjetoProdutoDomain produto = new PlanoTrabalhoProjetoProdutoDomain();
                produtoResult.CopyProperties(ref produto);
                listPlanoTrabalhoProjetoProdutoDomain.Add(produto);
            }

            return listPlanoTrabalhoProjetoProdutoDomain;
        }


        /// <summary>
        /// MountListPlanoTrabalhoEntrega
        /// </summary>        
        private List<PlanoTrabalhoAtividadeEntregaDomain> MountListPlanoTrabalhoEntrega(List<EntregaAtividadeResultSet> listEntregaAtividadeResultSet)
        {
            List<PlanoTrabalhoAtividadeEntregaDomain> listPlanoTrabalhoAtividadeEntregaDomain = new List<PlanoTrabalhoAtividadeEntregaDomain>();

            foreach (var entregaResult in listEntregaAtividadeResultSet.ToList())
            {
                PlanoTrabalhoAtividadeEntregaDomain entrega = new PlanoTrabalhoAtividadeEntregaDomain();
                entregaResult.CopyProperties(ref entrega);
                listPlanoTrabalhoAtividadeEntregaDomain.Add(entrega);
            }

            return listPlanoTrabalhoAtividadeEntregaDomain;
        }

        /// <summary>
        /// MountListPlanoTrabalhoEntrega
        /// </summary>        
        private List<PlanoTrabalhoProjetoEntregaDomain> MountListPlanoTrabalhoEntrega(List<EntregaResultSet> listEntregaResultSet)
        {
            List<PlanoTrabalhoProjetoEntregaDomain> listPlanoTrabalhoProjetoEntregaDomain = new List<PlanoTrabalhoProjetoEntregaDomain>();

            foreach (var entregaResult in listEntregaResultSet.ToList())
            {
                PlanoTrabalhoProjetoEntregaDomain entrega = new PlanoTrabalhoProjetoEntregaDomain();
                entregaResult.CopyProperties(ref entrega);
                listPlanoTrabalhoProjetoEntregaDomain.Add(entrega);
            }

            return listPlanoTrabalhoProjetoEntregaDomain;
        }
        #endregion
    }
}
