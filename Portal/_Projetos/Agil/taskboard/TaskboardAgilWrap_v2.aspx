<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap/Bootstrap.master" AutoEventWireup="true" CodeFile="TaskboardAgilWrap_v2.aspx.cs" Inherits="taskboard_TaskboardAgilWrap_v2" %>

<%@ MasterType VirtualPath="~/Bootstrap/Bootstrap.master" %>

<asp:Content ID="cabecalhoPaginaScript" ContentPlaceHolderID="cabecalhoPaginaScript" runat="Server">
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
     document.addEventListener('fullscreenchange', (event) => {
        var iconeExpand = document.querySelector('#icoFullscreen');
    if (document.fullscreenElement) {
            iconeExpand.classList.remove("fa-expand");
            iconeExpand.classList.add("fa-compress");
            setTimeout(function () {
                var alturaKanban = Math.max(0, document.documentElement.clientHeight) - 60;
                kanban.style.height = alturaKanban + 'px';
                
                var alturaDivsRolagem = Math.max(0, document.documentElement.clientHeight) - 160;

                 var divsComRolagem = document.querySelectorAll('div[data-codigoiteracao] > div:not(:first-child)');
                for(var i = 0; i < divsComRolagem.length; i++) 
                {
                    divsComRolagem[i].style.height = alturaDivsRolagem + 'px';
                    divsComRolagem[i].parentElement.style.height = (alturaDivsRolagem + 65) + 'px';
                  //console.log(divsComRolagem[i].parentElement);
                }
                
           }, 400);
           
        }
        else {
            setTimeout(function () {
                var alturaKanban = Math.max(0, document.documentElement.clientHeight) - 50;
                kanban.style.height = alturaKanban + 'px';
                
                var alturaDivsRolagem = Math.max(0, document.documentElement.clientHeight) - 135;

                 var divsComRolagem = document.querySelectorAll('div[data-codigoiteracao] > div:not(:first-child)');
                for(var i = 0; i < divsComRolagem.length; i++) 
                {
                    divsComRolagem[i].style.height = alturaDivsRolagem + 'px';
                    divsComRolagem[i].parentElement.style.height = (alturaDivsRolagem + 65) + 'px';
                }
           }, 400);
            iconeExpand.classList.add("fa-expand");
        }
    });
</asp:Content>
<asp:Content ID="conteudoPaginaForm" ContentPlaceHolderID="conteudoPaginaForm" runat="Server">
    <div class="hidden" id="alturaTela"><%=alturaTela%></div>
    <div class="hidden" id="codigoProjeto"><%=codigoProjeto%></div>
    <div class="hidden" id="idioma"><%=idioma%></div>
    <div class="hidden" id="baseUrl"><%=baseUrl%></div>
    <div class="hidden" id="dashboardID"><%=dashboardId%></div>
    <div class="hidden" id="permiteIncluirNovaSprint"><%=permiteIncluirNovaSprint%></div>
    <div class="hidden" id="permiteAlterarSprint"><%=permiteAlterarSprint%></div>
    <div class="hidden" id="permiteExcluirSprint"><%=permiteExcluirSprint%></div>
    <div class="hidden" id="permiteManterEquipe"><%=permiteManterEquipe%></div>
     <div class="hidden" id="permiteManterItensBacklog"><%=permiteManterItensBacklog%></div>
    <div class="hidden" id="permiteExcluirItensBacklog"><%=permiteExcluirItensBacklog%></div>

    <div id="conteudoPrincipal" runat="server">
        <div id="kanbanMaisCabecalho">
            <div class="kanban-agil-cabecalho" id="kanban-agil-cabecalho-id">
                <input type="hidden" runat="server" name="iteracoesCode" id="iteracoesCode"/>
                <form class="kanban-agil-pesquisa form-inline">
                    <div class="form-group mb-0">
                        <input class="form-control" id="pesquisaTexto" placeholder="<%=this.T("kanban_Pesquisar")%>" style="border-top-left-radius: 0; border-bottom-left-radius: 0;" type="text" />
                    </div>
                </form>                
                <div id="divRadioMaisExpand" style="display: flex; flex-direction: row; align-items: center">
                    <i id="icoFullscreen" class="fas fa-expand" title="Modo de tela cheia" style="color: #656469; cursor: pointer; font-size: 18px; margin: 0 0.25rem;"></i>
                    <dxcp:ASPxRadioButtonList ID="rbOrdemSprints" EnableClientSideAPI="true" Caption="Mostrar sprints" ClientInstanceName="rbOrdemSprints" runat="server" ValueType="System.String" RepeatDirection="Horizontal">
                       <Paddings PaddingLeft="5" PaddingBottom="0" PaddingTop="0" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                    ordenacao = s.GetValue();

                               kanbanAgilCarregaSprintsDoProjeto();
                            } " />
                        <CaptionSettings HorizontalAlign="Left" VerticalAlign="Middle" />
                        <Items>
                            <dx:ListEditItem Value="A" Text="Mais antigas primeiro" />
                            <dx:ListEditItem Value="R" Text="Mais novas primeiro" />
                        </Items>
                    </dxcp:ASPxRadioButtonList>
                    <dxcp:ASPxCheckBox ID="checkMostrarSprintsEncerradas" Checked="false" ClientInstanceName="checkMostrarSprintsEncerradas" Text="Encerradas" runat="server" EnableClientSideAPI="true">
                        <ClientSideEvents CheckedChanged="function(s, e) {
                               mostrarSprintsEncerradas = s.GetChecked();

                               kanbanAgilCarregaSprintsDoProjeto();
                            } " />
                    </dxcp:ASPxCheckBox>
                </div>
            </div>

            <div id="kanban">
            </div>
        </div>
    </div>
    <div id="divBotaoAddSprint" class="ui-widget-content"></div>
    <div id="divBotaoAjuda" class="ui-widget-content"></div>
    <dx:ASPxLoadingPanel ID="lpLoading" ClientInstanceName="lpLoading" runat="server"></dx:ASPxLoadingPanel>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
        kanban.style.height = sHeight + 'px';
    </script>
</asp:Content>
