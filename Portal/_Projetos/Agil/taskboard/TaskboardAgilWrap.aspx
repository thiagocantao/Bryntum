<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap/Bootstrap.master" AutoEventWireup="true" CodeFile="TaskboardAgilWrap.aspx.cs" Inherits="taskboard_TaskboardAgilWrap" %>

<%@ MasterType VirtualPath="~/Bootstrap/Bootstrap.master" %>

<asp:Content ID="cabecalhoPaginaScript" ContentPlaceHolderID="cabecalhoPaginaScript" runat="Server">
    function eventoOkParaEncerrarSprint() {

            //alert('foi no evento: eventoOkParaEncerrarSprint');
           var codigoProjeto = document.querySelector('#codigoProjeto').innerHTML;
            $.ajax({
                data: 'codigoProjetoIteracao=' + encodeURI(codigoProjeto),
                url: './agil-taskboard-service.asmx/encerrar-sprint',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {
                 window.location.reload();
            });
        }

        function eventoCancelarParaEncerrarSprint() {
            window.location.reload();
        }
    
        document.addEventListener('fullscreenchange', (event) => {
            var iconeExpand = document.querySelector('#icoFullscreen');
            if (document.fullscreenElement) {
                setTimeout(function () {
                                iconeExpand.classList.remove("fa-expand");
                                iconeExpand.classList.add("fa-compress");
                                var sHeight = Math.max(0, document.documentElement.clientHeight) - 65;
                                kanban.style.height = sHeight + 'px';
                                divContainerCards.style.height = (sHeight - 80) + 'px';
                }, 500);
            }
            else {
                    setTimeout(function () {
                                    var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
                                    kanban.style.height = sHeight + 'px';
                                    divContainerCards.style.height = (sHeight - 80) + 'px';
                                    iconeExpand.classList.add("fa-expand");
                }, 500);

            }
        });

        document.addEventListener('click', function (e) {
            if ((e.target) && (e.target.id == 'icoFullscreen')) {
                var elem = document.getElementById("kanbanMaisCabecalho");
                if (!document.fullscreen) {
                    if (elem.requestFullscreen) {
                        elem.requestFullscreen();
                    }
                    else if (elem.mozRequestFullScreen) { /* Firefox */
                        elem.mozRequestFullScreen();
                    }
                    else if (elem.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
                        elem.webkitRequestFullscreen();
                    }
                    else if (elem.msRequestFullscreen) { /* IE/Edge */
                        elem.msRequestFullscreen();
                    }
                }
                else {
                    document.exitFullscreen();
                }
            }
            e.preventDefault();

        });



        document.addEventListener('click', function (e) {
            if ((e.target) && (e.target.id == 'icoReuniaoDiariaSprint')) {
                var el = document.getElementById("imgReuniaoDiariaSprint");
                el.click();
                e.preventDefault();
            }
        });



        function abreFullscreen(url, altura) {
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/' + url, 'Kanban Ágil', screen.availWidth - 25, screen.availHeight - 80, '', null);
        }

        function abreGrafico(codigoProjeto, dashboardId) {
            if (document.fullscreen)
                document.exitFullscreen();
            setTimeout(function () {
                window.top.showModal(window.top.pcModal.cp_Path + '_Dashboard/VisualizadorDashboard.aspx?id=' + dashboardId + '&CodProjeto=' + codigoProjeto, 'Evolução', null, null, '', null);
            }, 1000);
        }

        function abreReuniaoDiaria(codigoProjeto) {
            if (document.fullscreen)
                document.exitFullscreen();
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/CalendarioReuniaoDiariaSprint.aspx?CP=' + codigoProjeto, 'Reunião', (screen.availWidth / 4) - 10, 250, '', null);
        }

        function abreReuniaoDiaria(codigoProjeto) {
            //debugger
            if (document.fullscreen)
                document.exitFullscreen();
            window.top.fechaModal();

            var altura = 500;//(screen.availHeight - 190);
            var largura = 1100;//(screen.availWidth - 150);

            var url = window.top.pcModal.cp_Path + '_Projetos/Agil/ReuniaoDiariaSprint.aspx?CP=' + codigoProjeto;
            //url += '&ano=' + ano;
            //url += '&mes=' + mes;
            //url += '&dia=' + dia;
            url += '&alt=' + altura;
            url += '&larg=' + largura;
            window.top.showModal(url, 'Reunião', null, null, '', null);
        }
    

         
   
</asp:Content>
<asp:Content ID="conteudoPaginaForm" ContentPlaceHolderID="conteudoPaginaForm" runat="Server">
    <div class="hidden" id="alturaTela"><%=alturaTela%></div>
    <div class="hidden" id="codigoProjeto"><%=codigoProjeto%></div>
    <div class="hidden" id="idioma"><%=idioma%></div>
    <div class="hidden" id="podeExcluirTarefa"><%=podeExcluirTarefa%></div>
    <div class="hidden" id="ehSprint"><%=ehSprint%></div>

    <div id="conteudoPrincipal" runat="server">
        <div id="kanbanMaisCabecalho">
            <div class="kanban-agil-cabecalho" id="kanban-agil-cabecalho-id">
                <div class="kanban-agil-icones">
                    
                    <i class="fas fa-bars" id="menuBarras" data-toggle="dropdown"></i>
                    <i class="fas fa-expand" id="icoFullscreen" runat="server" title="Modo de tela cheia"></i>
                    <i class="fas fa-chart-area" id="icoBurndown" runat="server" title="Visualizar gráfico de evolução"></i>
                    <i class="fas fa-arrow-down" id="dropdownAssociarItemBacklog" title="Associar Item" runat="server"></i>
                    <i class="fas fa-sync-alt" id="icoRefreshKanban" title="Atualizar Kanban"></i>
                    
                    <div aria-labelledby="menuBarras" class="dropdown-menu">
                        <a class="dropdown-item" href="javascript:void(0);" title="Manter reuniões diárias" id="icoReuniaoDiariaSprint" runat="server"><i class="fas fa-users" style="margin-right:5px" runat="server"></i>Manter reuniões diárias</a>
                        <a class="dropdown-item" href="javascript:void(0);" title="Configurar raias" id="icoConfigurarRaias"><i class="fas fa-th" style="margin-right:8px"></i>Configurar raias</a>
                        <a class="dropdown-item" href="javascript:void(0);" title="Editar equipe" id="icoEditarEquipe" runat="server"><i class="fas fa-users-cog" runat="server" style="margin-right:5px"></i>Editar equipe</a>
                        <a class="dropdown-item" href="javascript:void(0);" title="Encerrar Sprint" id="icoEncerrarSprint" runat="server"><i class="fas fa-power-off" runat="server" style="margin-right:9px"></i>Encerrar Sprint</a>
                    </div>
                    <span style="display: none;">
                        <dxcp:ASPxImage ID="imgFullscreen" runat="server" ClientVisible="True"
                            ClientInstanceName="imgFullscreen" ImageUrl="~/imagens/fullscreen.png"
                            ShowLoadingImage="false" Cursor="pointer" ToolTip="Modo tela cheia" CssClass="espaco-direito">
                        </dxcp:ASPxImage>
                        <dxcp:ASPxImage ID="imgBurndown" runat="server" ClientVisible="True"
                            ClientInstanceName="imgBurndown" ImageUrl="~/imagens/burndown.png"
                            ShowLoadingImage="false" Cursor="pointer"
                            ToolTip="Visualizar gráfico de evolução" CssClass="espaco-direito">
                        </dxcp:ASPxImage>
                        <dxcp:ASPxImage ID="imgReuniaoDiariaSprint" runat="server" ClientVisible="True"
                            ClientInstanceName="imgReuniaoDiariaSprint" ImageUrl="~/imagens/reuniaoAgil.png"
                            ShowLoadingImage="false" Cursor="pointer"
                            ToolTip="Reunião" CssClass="espaco-direito">
                        </dxcp:ASPxImage>
                    </span>
                </div>
                <div class="kanban-agil-data-sprint" id="divKanbanAgilDataSprint" runat="server">
                    <span></span> <span id="kanban-agil-inicio-termino-sprint">Carregando...</span> <span></span>
                </div>
                
                <form class="form-inline">
                    <div class="form-inline">
                        <label for="comboFiltrarStatus" style="margin-right: 5px;font-size:80%">Status</label>
                        <select name="comboFiltrarStatus" id="comboFiltrarStatus" class="form-control" style="margin-right: 20px;border: 1px solid #ced4da;border-radius:.25rem;">
                            <option value="-1" selected>Todos</option>
                        </select>
                        <div style="padding-right: 15px;padding-top:9px">
                            <canvas id="canvasFiltraKanbanPorBugs" width="16" height="21" title="Filtrar Problemas" style="cursor: pointer" />
                        </div>
                        <div style="padding-right: 15px;padding-top:9px">
                            <canvas id="canvasFiltraKanbanPorImpedimentos" width="16" height="21" title="Filtrar Impedimentos" style="cursor: pointer" />
                        </div>
                        <div style="padding-right: 15px;padding-top:9px">
                            <canvas id="canvasFiltraKanbanPorUsuarioLogado" width="16" height="21" title="Meus itens" style="cursor: pointer" />
                        </div>
                        <input class="form-control" id="pesquisaTexto" placeholder="<%=this.T("kanban_Pesquisar")%>" style="border-top-left-radius: 0; border-bottom-left-radius: 0;margin-right:0px" type="text" />

                    </div>
                </form>

            </div>
            <div id="kanban">
            </div>
        </div>
    </div>

    <dxcp:ASPxPopupControl ID="pcCalendario" runat="server"
        ClientInstanceName="pcCalendario" CloseAction="CloseButton"
        HeaderText="Reuniões Diárias"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        Width="400px" Height="300px" PopupHorizontalOffset="-370"
        PopupVerticalOffset="-130">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable" width="100%">
                    <tr>
                        <td>
                            <dxtv:ASPxCalendar ID="calendario" runat="server"
                                ClientInstanceName="calendario"
                                OnDayCellPrepared="ASPxCalendar1_DayCellPrepared" ShowClearButton="True"
                                ShowTodayButton="True" ShowWeekNumbers="False" Width="100%">
                            </dxtv:ASPxCalendar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="grid-legendas" cellpadding="0" cellspacing="0" enableviewstate="false">
                                <tbody>
                                    <tr>
                                        <td id="legVerde" runat="server" class="grid-legendas-cor grid-legendas-cor-atrasado">
                                            <span></span>
                                        </td>
                                        <td class="grid-legendas-label grid-legendas-label-atrasado">

                                            <asp:Label ID="Label6" runat="server" EnableViewState="False"><asp:Literal runat="server" Text="Reuniões Realizadas" /></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
        kanban.style.height = sHeight + 'px';
    </script>
</asp:Content>
