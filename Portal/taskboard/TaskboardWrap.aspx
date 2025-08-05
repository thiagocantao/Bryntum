<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="TaskboardWrap.aspx.cs" Inherits="taskboard_TaskboardWrap" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div class="hidden" id="idioma"><%=Master.idioma%></div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <!-- <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Tarefas Kanban">

                </dxe:ASPxLabel>
            </td>
        </tr> -->
    </table>

    <!-- Inicio Barra Logo Kanban, Botões de Exibição e data, Pesquisa -->
     <div class="container-fluid bar-top">
        <div class="row">
            <div class="col-sm-12 col-md-1 logo-kanban" style="margin-top: .6rem;">
                <!--
                <picture>
                    <img src="images/btn-kanban.png" class="img-fluid img-thumbnail" alt="Botão Kanban" height="40" width="40" />
                </picture>
                -->

                <span class="kanban-titulo-pagina"><%=tituloPagina %></span>
            </div>
            <div class="col-sm-12 col-md-8 btn-responsive box-header" align="center" style="margin-top: .6rem;">
                <div class="btn-group box-height" role="group" aria-label="Basic example">
                    <button type="button" class="btn btn-light btn-simple-card" id="botaoPesquisaModoExibicaoCardsSimples">
                        <i class="fas fa-list-ul pr-2"></i>
                        <span class="d-none d-sm-inline"><%=this.T("kanban_CartaoSimples")%></span>

                    </button>
                    <button type="button" class="btn btn-light btn-full-card" id="botaoPesquisaModoExibicaoCardsCompleto">
                        <i class="fas fa-th-large pr-2"></i>
                        <span class="d-none d-sm-inline"><%=this.T("kanban_CartaoCompleto")%></span>

                    </button>
                    <button type="button" class="btn btn-light hide hidden" id="botaoPesquisaData"><i class="far fa-calendar-alt pr-2"></i>
                        <input class="hidden" id="pesquisaData" name="pesquisaData" placeholder="Data início e fim" readonly="readonly" type="text" value="" class="dataStyle" />
                    </button>



                    <div class="input-daterange input-group input-responsive" id="pesquisaDatas">
                        <input class="form-control date-start" id="pesquisaDataInicio" name="pesquisaDataInicio" type="text" value="" />
                        <span class="input-group-addon"></span>
                        <input class="form-control date-end" id="pesquisaDataFim" name="pesquisaDataFim" type="text" value="" />
                    </div>
                    <button class="btn btn-green btn-ok" id="botaoPesquisaDatas" name="botaoPesquisaDatas" type="button">Ok</button>
                    <button class="btn btn-grey btn-limpar" id="botaoLimpaDatas" name="botaoLimpaDatas" type="button">Limpar</button>


                    <button type="button" id="popKanban" class="btn btn-light btn-last hidden" data-container="body" data-toggle="popover" data-placement="right" data-content="" data-html="true">
                        <span><i class="fas fa-info-circle" style="color: #7f903c;"></i></span>
                    </button>
                                               
                </div>
            </div>
            <div class="col-sm-12 col-md-3 form-responsive form-search search-mobile" style="margin-top: .6rem;">
                <i class="fas fa-search"></i>
                <i class="fas fa-times hidden"></i>
                <input aria-label="Searc" class="form-control mb-2 mr-sm-2" id="pesquisaTexto" maxlength="100" placeholder="<%=this.T("kanban_Pesquisar")%>" type="search" />
            </div>
        </div>
    </div>
    <div class="container-fluid" style="display:none;">
        <div class="d-flex justify-content-center">
            <div class="d-inline-flex p-1">
                <div class="loader loader--style1 hide" title="0">
                    <svg version="1.1" id="carregando" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
                        width="40px" height="40px" viewBox="0 0 40 40" enable-background="new 0 0 40 40" xml:space="preserve">
                        <path opacity="0.2" fill="#000" d="M20.201,5.169c-8.254,0-14.946,6.692-14.946,14.946c0,8.255,6.692,14.946,14.946,14.946
      s14.946-6.691,14.946-14.946C35.146,11.861,28.455,5.169,20.201,5.169z M20.201,31.749c-6.425,0-11.634-5.208-11.634-11.634
      c0-6.425,5.209-11.634,11.634-11.634c6.425,0,11.633,5.209,11.633,11.634C31.834,26.541,26.626,31.749,20.201,31.749z" />
                        <path fill="#000" d="M26.013,10.047l1.654-2.866c-2.198-1.272-4.743-2.012-7.466-2.012h0v3.312h0
      C22.32,8.481,24.301,9.057,26.013,10.047z">
                            <animateTransform attributeType="xml"
                                attributeName="transform"
                                type="rotate"
                                from="0 20 20"
                                to="360 20 20"
                                dur="0.5s"
                                repeatCount="indefinite" />
                        </path>
                    </svg>
                </div>
            </div>
            <div class="d-inline-flex p-1">
                <p class="kanban-mensagem hide">
                </p>
                <p class="kanban-aviso hide" style="display:none">
                </p>
            </div>

        </div>

    </div>
    <!-- / Fim Inicio Barra Logo Kanban, Botões de Exibição e data, Pesquisa -->

    <!-- Inicio Colunas  -->
    <div id="conteudoPrincipal" runat="server">
        <div id="kanban_carregando"><i class="fas fa-spin fa-spinner"></i></div>
        <div class="row row-eq-height" id="kanban">
            <div class="col-sm-4 kanban-coluna">
                <div class="kanban-coluna-cabecalho  kanban-cor-fundo-verde">
                    <span class="kanban-coluna-cabecalho-titulo"><%=this.T("kanban_Todo")%></span>
                    <span class="kanban-coluna-cabecalho-contador"></span>
                    <span class="kanban-coluna-cabecalho-carregando hidden"><i class="fa fa-spinner fa-spin"></i></span>
                    <span class="kanban-coluna-cabecalho-ordenacao">
                        <label class="form-control-custom-checkbox">
                            <%=this.T("kanban_atrasados")%>
                            <input id="atrasadasTodo" name="atrasadasTodo" type="checkbox" />
                            <span class="custom-checkbox"></span>
                        </label>
                        <select id="ordenacaoTodo" class="kanban-cor-fundo-verde-claro">
                            <option value="prioridade"><%=this.T("kanban_OrdenacaoPrioridade")%></option>
                            <option value="titulo"><%=this.T("kanban_OrdenacaoTitulo")%></option>
                            <option value="dataInicio"><%=this.T("kanban_OrdenacaoDataInicio")%></option>
                            <option value="dataTermino"><%=this.T("kanban_OrdenacaoDataTermino")%></option>
                        </select>
                    </span>
                </div>
                <ul class="kanban-lista ui-sortable" data-pagina="0" data-total="0" id="todo" style="height: <%=alturaKanban%>;">
                </ul>
            </div>
            <div class="col-sm-4 kanban-coluna">
                <div class="kanban-coluna-cabecalho kanban-cor-fundo-laranja">
                    <span class="kanban-coluna-cabecalho-titulo"><%=this.T("kanban_Doing")%></span>
                    <span class="kanban-coluna-cabecalho-contador"></span>
                    <span class="kanban-coluna-cabecalho-carregando hidden"><i class="fa fa-spinner fa-spin"></i></span>
                    <span class="kanban-coluna-cabecalho-ordenacao">
                        <label class="form-control-custom-checkbox">
                            <%=this.T("kanban_atrasados")%>
                            <input type="checkbox" id="atrasadasDoing" name="atrasadasDoing" value="1" />
                            <span class="custom-checkbox"></span>
                        </label>
                        <select id="ordenacaoDoing" class="kanban-cor-fundo-laranja-claro">
                            <option value="prioridade"><%=this.T("kanban_OrdenacaoPrioridade")%></option>
                            <option value="titulo"><%=this.T("kanban_OrdenacaoTitulo")%></option>
                            <option value="dataInicio"><%=this.T("kanban_OrdenacaoDataInicio")%></option>
                            <option value="dataTermino"><%=this.T("kanban_OrdenacaoDataTermino")%></option>
                        </select>
                    </span>
                </div>
                <ul class="kanban-lista ui-sortable" data-pagina="0" data-total="0" id="doing" style="height: <%=alturaKanban%>;">
                </ul>
            </div>
            <div class="col-sm-4 kanban-coluna">
                <div class="kanban-coluna-cabecalho kanban-cor-fundo-azul">
                    <span class="kanban-coluna-cabecalho-titulo"><%=this.T("kanban_Done")%></span>
                    <span class="kanban-coluna-cabecalho-contador"></span>
                    <span class="kanban-coluna-cabecalho-carregando hidden"><i class="fa fa-spinner fa-spin"></i></span>
                    <span class="kanban-coluna-cabecalho-ordenacao">
                        <select id="ordenacaoDone" class="kanban-cor-fundo-azul-claro">
                            <option value="prioridade"><%=this.T("kanban_OrdenacaoPrioridade")%></option>
                            <option value="titulo"><%=this.T("kanban_OrdenacaoTitulo")%></option>
                            <option value="dataInicio"><%=this.T("kanban_OrdenacaoDataInicio")%></option>
                            <option value="dataTermino"><%=this.T("kanban_OrdenacaoDataTermino")%></option>
                        </select>
                    </span>
                </div>
                <ul class="kanban-lista ui-sortable" data-pagina="1" data-total="0" id="done" style="height: <%=alturaKanban%>;">
                </ul>
            </div>
        </div>
    </div>

    <!-- / Fim Colunas Loading -->
    <div class="modal fade" id="kanbanModal" tabindex="-1" role="dialog" aria-labelledby="kanbanModalTitulo" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="kanbanModalTitulo"><%=this.T("aten__o")%></h5>
                </div>
                <div class="modal-body">
                    <p id="kanbanModalMensagem"></p>
                </div>
                <div class="modal-footer text-center">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" data-id="" data-status="done" id="kanbanModalArquivar" tabindex="-1" role="dialog" aria-labelledby="kanbanModalArquivarTitulo" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="kanbanModalArquivarTitulo"><%= this.T("kanban_ArquivarTarefa") %></h5>
                </div>
                <div class="modal-body">
                    <p class="text-center"><%= this.T("kanban_AvisoArquivarTarefa") %></p>
                    <p class="text-center kanban-cor-azul" style="font-weight: 500;"><%= this.T("kanban_ConfirmacaoArquivamentoTarefa") %></p>
                    <p>&nbsp;</p>
                    <p class="text-center alert alert-info hidden" id="kanbanModalArquivarCarregando"><i class="fas fa-spin fa-spinner"></i>&nbsp;<%= this.T("kanban_AguardeArquivamentoTarefa") %></p>
                    <p class="text-center alert alert-success hidden" id="kanbanModalArquivamentoSucesso"><%= this.T("kanban_ArquivamentoSucesso") %></p>
                    <p class="text-center alert alert-danger hidden" id="kanbanModalArquivarErro"><%= this.T("kanban_ErroArquivarTarefa") %></p>
                </div>
                <div class="modal-footer text-center">
                    <button id="kanbanModalArquivarSim" type="button" class="btn btn-primary"><%= this.T("sim") %></button>
                    <button id="kanbanModalArquivarNao" type="button" class="btn btn-secondary" data-dismiss="modal"><%= this.T("n_o") %></button>
                </div>
            </div>
        </div>
    </div>

    <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
</dxcp:ASPxHiddenField>
</asp:Content>
