using Cdis.Brisk.Infra.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class PlanoTrabalhoDataTransfer
    {
        public PlanoTrabalhoDataTransfer()
        {
            ListItemRelatorio = new List<InfoListPageDataTransfer>();
        }
       
        public string DtInicio { get; set; }
        public string DtTermino { get; set; }

        public int CodigoProjeto { get; set; }
        public string NomeProjeto { get; set; }
        public string NomeTipoProjeto { get; set; }
        public string DescObjetivo { get; set; }
        public string NomeUnidadeNegocio { get; set; }
        public string NomeResponsavelTecnico { get; set; }
        public string NomeCarteiraPrincipal { get; set; }
        public string DescPublicoAlvo { get; set; }
        public string Observacao { get; set; }
        public string DescClassificacao { get; set; }
        public string DescObjetivoEstrategico { get; set; }
        public string DescLinhaAcao { get; set; }
        public string DescAreaAtuacao { get; set; }
        public string DescNatureza { get; set; }
        public string DescFuncao { get; set; }
        public string DescSubFuncao { get; set; }
        public string DescPrograma { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCaracteres
        {
            get
            {
                return this.GetTotalCharacters();
            }
        }

        private readonly int totalCaracteresPrimeiraPagina = 4000;
        private readonly int totalCaracteresDemaisPaginas = 2665;

        private InfoGrupoItemPageDataTransfer _infoGrupoItemPage;
        public InfoGrupoItemPageDataTransfer InfoGrupoItemPage
        {
            get
            {
                if (_infoGrupoItemPage != null)
                {
                    return _infoGrupoItemPage;
                }

                List<InfoItemPageDataTranfer> listItem = new List<InfoItemPageDataTranfer>();
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 1, DescTitulo = "Nome do projeto", NomeItem = "NomeProjeto", Valor = string.IsNullOrEmpty(NomeProjeto) ? "" : NomeProjeto });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 1, DescTitulo = "Tipo de Projeto", NomeItem = "TipoProjeto", Valor = string.IsNullOrEmpty(NomeTipoProjeto) ? "" : NomeTipoProjeto });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 2, DescTitulo = "Objetivos", NomeItem = "Objetivos", Valor = string.IsNullOrEmpty(DescObjetivo) ? "" : DescObjetivo });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 3, DescTitulo = "Unidade", NomeItem = "NomeUnidade", Valor = string.IsNullOrEmpty(NomeUnidadeNegocio) ? "" : NomeUnidadeNegocio });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 3, DescTitulo = "Responsável Técnico", NomeItem = "NomeResponsavel", Valor = string.IsNullOrEmpty(NomeResponsavelTecnico) ? "" : NomeResponsavelTecnico });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 3, DescTitulo = "Carteira Principal", NomeItem = "Carteira", Valor = string.IsNullOrEmpty(NomeCarteiraPrincipal) ? "" : NomeCarteiraPrincipal });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Público Alvo", NomeItem = "PublicoAlvo", Valor = string.IsNullOrEmpty(DescPublicoAlvo) ? "" : DescPublicoAlvo });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Objetivo Geral", NomeItem = "ObjetivoGeral", Valor = string.IsNullOrEmpty(Observacao) ? "" : Observacao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Classificação", NomeItem = "Classificacao", Valor = string.IsNullOrEmpty(DescClassificacao) ? "" : DescClassificacao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Objetivo Estratégico", NomeItem = "ObjetivoEstrategico", Valor = string.IsNullOrEmpty(DescObjetivoEstrategico) ? "" : DescObjetivoEstrategico });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Linha de ação", NomeItem = "LinhaAcao", Valor = string.IsNullOrEmpty(DescLinhaAcao) ? "" : DescLinhaAcao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Área de atuação", NomeItem = "AreaAtuacao", Valor = string.IsNullOrEmpty(DescAreaAtuacao) ? "" : DescAreaAtuacao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Natureza", NomeItem = "Natureza", Valor = string.IsNullOrEmpty(DescNatureza) ? "" : DescNatureza });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Função", NomeItem = "Funcao", Valor = string.IsNullOrEmpty(DescFuncao) ? "" : DescFuncao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Subfunção", NomeItem = "Subfuncao", Valor = string.IsNullOrEmpty(DescSubFuncao) ? "" : DescSubFuncao });
                listItem.Add(new InfoItemPageDataTranfer { NumGrupo = 4, DescTitulo = "Programa", NomeItem = "Programa", Valor = string.IsNullOrEmpty(DescPrograma) ? "" : DescPrograma });

                int numPage = 1;
                int numOrdem = 1;
                int totalCaracteres = 0;
                List<InfoItemPageDataTranfer> listItemCampoDividio = new List<InfoItemPageDataTranfer>();
                foreach (var item in listItem)
                {
                    //Itens da mesma página
                    int tempQtdCaracteres = totalCaracteres + item.Valor.Length;
                    if (((tempQtdCaracteres <= totalCaracteresPrimeiraPagina) && numPage == 1) || ((tempQtdCaracteres <= totalCaracteresDemaisPaginas) && numPage > 1))
                    {
                        totalCaracteres = tempQtdCaracteres;
                        item.NumPage = numPage;
                        item.NumOrdem = numOrdem;
                        numOrdem++;
                    }
                    else
                    {
                        //Quando a quantidade de caracteres é ultrapassada
                        int qtdRestantePagina = numPage == 1 ? (totalCaracteresPrimeiraPagina - totalCaracteres) : (totalCaracteresDemaisPaginas - totalCaracteres);
                        if (qtdRestantePagina == 0)
                        {
                            numPage++;
                            totalCaracteres = item.Valor.Length;
                            item.NumOrdem = numOrdem;
                            item.NumPage = numPage;
                            numOrdem++;
                        }
                        else
                        {
                            string textoCompleto = item.Valor;
                            string textoAtual = textoCompleto.Substring(0, qtdRestantePagina);//caracteres da página atual
                            string textoProximaPagina = textoCompleto.Substring((textoAtual.Length - 1), (textoCompleto.Length - textoAtual.Length));
                            item.Valor = textoAtual;//caracteres da página atual
                            item.NumOrdem = numOrdem;
                            item.NumPage = numPage;
                            totalCaracteres = textoProximaPagina.Length;
                            numPage++;
                            numOrdem++;
                            listItemCampoDividio.Add(new InfoItemPageDataTranfer { NumPage = numPage, NumGrupo = item.NumGrupo, DescTitulo = item.DescTitulo, NumOrdem = numOrdem, NomeItem = item.NomeItem, Valor = textoProximaPagina });
                            numOrdem++;
                        }
                    }
                }
                listItem.AddRange(listItemCampoDividio);
                _infoGrupoItemPage = new InfoGrupoItemPageDataTransfer(listItem.OrderBy(i => i.NumOrdem).ToList());

                return _infoGrupoItemPage;
            }
        }

        private int itemListPagina = 30;
        public List<PlanoTrabalhoResultadoEsperadoDataTransfer> ListResultadoEsperado { get; set; }
        public List<PlanoTrabalhoProdutoDataTransfer> ListProduto { get; set; }
        public List<PlanoTrabalhoEntregaDataTransfer> ListEntrega { get; set; }
        public List<PlanoTrabalhoAtividadeEntregaDataTransfer> ListAtividadeEntrega { get; set; }
        public List<InfoListPageDataTransfer> ListItemRelatorio { get; set; }

    }
}
