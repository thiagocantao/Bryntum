using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma
{
    /// <summary>
    /// Classe de repositório CronogramaProjetoMetaRepository
    /// </summary>
    public class CronogramaProjetoMetaRepository : RepositoryBaseQuery<CronogramaProjetoMetaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório CronogramaProjetoMetaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório CronogramaProjetoMetaRepository
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
        /// Construtor da classe de repositório CronogramaProjetoMetaRepository
        /// </summary>
        public CronogramaProjetoMetaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        public IQueryable<CronogramaProjetoMetaDomain> GetQueryListCronogramaProjetoMeta(int codEntidade, int codUsuario, int codCarteira)
        {
            string query = string.Format(
                    @"BEGIN
							DECLARE @codEntidade INT = {2}
							DECLARE @codUsuario INT = {3}
							DECLARE @codCarteira INT = {4}											

                      DECLARE @tblRetorno TABLE
				            (Codigo Int,
				            Descricao Varchar(255),
				            Inicio DateTime,
				            Termino DateTime,
				            Cor Varchar(20),
				            Concluido Decimal(5,2),
				            CodigoPai Int,
				            CodigoUnidade Int,
				            Sumaria Char(1),
				            Nivel int,
				            StatusProjeto Varchar(255),
				            GerenteProjeto Varchar(255))
                        
		            /* Insere os projetos */
		            INSERT INTO @tblRetorno
						            (Codigo,
						            Descricao,
						            Inicio,
						            Termino,
						            Cor,
						            Concluido,
						            CodigoPai,
						            CodigoUnidade,
						            Sumaria, 
						            Nivel,
						            StatusProjeto, 
						            GerenteProjeto)
					            SELECT p.CodigoProjeto AS Codigo, 
							            prj.NomeProjeto AS Descricao, 
							            IsNull(p.InicioReprogramado,Convert(DateTime,dbo.f_GetConteudoCampoProjeto(p.CodigoProjeto, 'DATA_INI'),103)) AS Inicio,
				                        IsNull(p.TerminoReprogramado,Convert(DateTime,dbo.f_GetConteudoCampoProjeto(p.CodigoProjeto, 'DATA_FIM'),103)) AS Termino, 
							            {0}.{1}.f_GetCorFisico(p.CodigoProjeto) AS Cor, 
							            p.PercentualRealizacao * 100 AS Concluido,
							            prj.CodigoUnidadeNegocio * -1 AS CodigoPai,
							            prj.CodigoUnidadeNegocio, '0', 3,
							            s.DescricaoStatus as StatusProjeto,
							            u.NomeUsuario as GerenteProjeto  
			            FROM {0}.{1}.ResumoProjeto p 
				            INNER JOIN {0}.{1}.Projeto prj ON (prj.CodigoProjeto = p.CodigoProjeto)  
				            INNER JOIN {0}.{1}.Status AS s ON (s.CodigoStatus = prj.CodigoStatusProjeto AND s.IndicaAcompanhamentoExecucao = 'S')   
				            LEFT JOIN {0}.{1}.Usuario u ON prj.CodigoGerenteProjeto = u.CodigoUsuario 
			            WHERE prj.DataExclusao IS NULL
			            AND prj.CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ')
			            AND prj.CodigoStatusProjeto <> 4			
			            AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario(@codUsuario, @codEntidade, @codCarteira)) 
                    	 
			            /* Insere as unidades */
			            INSERT INTO @tblRetorno
			            (Codigo,
			            Descricao,
			            Inicio,
			            Termino,
			            Cor,
			            CodigoPai,
			            Sumaria, 
			            Nivel)
			            SELECT u.CodigoUnidadeNegocio * -1,
					            u.SiglaUnidadeNegocio,
					            MIN(t.Inicio),
					            MAX(t.Termino),
					            'Cinza',
					            u.CodigoUnidadeNegocioSuperior * -1, '1', 2
			            FROM {0}.{1}.UnidadeNegocio AS u INNER JOIN
					            @tblRetorno AS t ON (t.CodigoUnidade = u.CodigoUnidadeNegocio)
			            WHERE CodigoEntidade = @codEntidade                                   
			            GROUP BY u.CodigoUnidadeNegocio * -1,
					            u.SiglaUnidadeNegocio, u.CodigoUnidadeNegocioSuperior    
                         
		            DECLARE @CodigoUnidade Int,
			            @CodigoUnidadeSuperior Int,
			            @CodigoUnidadeAux Int
            		                                               	
	            /* Cursor para percorrer as unidades de negócio e trazer seus respectivos pais */                                       	                                  
	            DECLARE cCursor CURSOR LOCAL FOR
	            SELECT DISTINCT Codigo * -1
		            FROM @tblRetorno
		            WHERE Codigo < 0
		            
	            OPEN cCursor
		            
	            FETCH NEXT FROM cCursor INTO @CodigoUnidade
		            
	            WHILE @@FETCH_STATUS = 0
		            BEGIN	

		            SET @CodigoUnidadeAux = @CodigoUnidade
		            SET @CodigoUnidadeSuperior = null
		            		            	            
		            SELECT @CodigoUnidadeSuperior = CodigoUnidadeNegocioSuperior
			            FROM {0}.{1}.UnidadeNegocio
			            WHERE CodigoUnidadeNegocio = @CodigoUnidadeAux
			            AND CodigoEntidade = @codEntidade
	            	                                
		            
			            WHILE @CodigoUnidadeSuperior IS NOT NULL
				            BEGIN	  
		                    	                             
				            INSERT INTO @tblRetorno (Codigo,
											            Descricao,
											            Inicio,
											            Termino,
											            Cor,
											            CodigoPai,
											            Sumaria, 
											            Nivel)
						            SELECT  un.CodigoUnidadeNegocio * -1,
								            un.SiglaUnidadeNegocio,
								            (SELECT MIN(t.Inicio) FROM @tblRetorno t WHERE (un.CodigoUnidadeNegocio * -1) = t.CodigoPai),
								            (SELECT MAX(t.Termino) FROM @tblRetorno t WHERE (un.CodigoUnidadeNegocio * -1) = t.CodigoPai),
								            'Cinza',
								            un.CodigoUnidadeNegocioSuperior * -1, '1', 1
						            FROM {0}.{1}.UnidadeNegocio AS un  LEFT JOIN
							            {0}.{1}.Usuario AS g ON (g.CodigoUsuario = un.CodigoUsuarioGerente) 
						            WHERE un.CodigoUnidadeNegocio = @CodigoUnidadeSuperior
						            AND (un.CodigoUnidadeNegocio * -1) NOT IN (SELECT Codigo FROM @tblRetorno)
						            AND un.CodigoEntidade = @codEntidade --> Parâmetro!!!!
								   
						            SET @CodigoUnidadeAux = @CodigoUnidadeSuperior
								 
						            SET @CodigoUnidadeSuperior = null
								   
						            SELECT @CodigoUnidadeSuperior = CodigoUnidadeNegocioSuperior
						            FROM {0}.{1}.UnidadeNegocio
						            WHERE CodigoUnidadeNegocio = @CodigoUnidadeAux
						            AND CodigoEntidade = @codEntidade -- Parâmetro CodigoEntidade!!!!  
		                            
		                             
					            END  
		                 
			            FETCH NEXT FROM cCursor INTO @CodigoUnidade
		            END
		            
	            CLOSE cCursor
	            DEALLOCATE cCursor   
                                
	            UPDATE @tblRetorno
		            SET CodigoPai = Null,
			            Nivel = 0
		            WHERE Not EXISTS (SELECT 1
							            FROM {0}.{1}.UnidadeNegocio
						            WHERE CodigoUnidadeNegocio = (CodigoPai * -1)
							            AND CodigoEntidade = @codEntidade)
                                                        
		            SELECT t.Codigo,
				            t.CodigoPai AS CodigoSuperior,
				            t.Descricao,
				            t.Inicio,
				            t.Termino,
				            t.Cor,                               
				            t.Concluido,
				            CASE WHEN t.Inicio IS NULL OR t.Termino IS NULL THEN 'S' ELSE 'N' END AS SemCronograma, 
				            Sumaria,
				            StatusProjeto, 
				            GerenteProjeto 
			            FROM @tblRetorno AS t 
			            ORDER BY t.Nivel, t.CodigoPai, t.Inicio, t.Termino           
            END",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codEntidade,
                codUsuario,
                codCarteira);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
