<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskboard.aspx.cs" Inherits="_Projetos_Agil_taskboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <link href="../../estilos/taskBoard.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        // --------------------------------------------------------------------------------------------------------
        //  Referência para o drag and drop: http://www.w3schools.com/html/tryit.asp?filename=tryhtml5_draganddrop
        // --------------------------------------------------------------------------------------------------------
        function allowDrop(ev) {
            ev.preventDefault();
        }
        
        function drag(ev) {
            // Registra o "ItemBackLog" que está sendo arrastado
            ev.dataTransfer.setData("Text", ev.target.id);
        }

        function drop(ev, codigoProjeto) {
            ev.preventDefault();
            // obtem o item que está sendo arrastado
            var ItemBackLog = ev.dataTransfer.getData("Text");

            // Obtem o código do status (raia) de origem
            var StatusOrigem = document.getElementById(ItemBackLog).parentNode;
            var CodigoStatusOrigem = StatusOrigem.id.replace("Status", "");

            // Obtem o código do status (raia) de destino
            var StatusDestino = getObjetoStatus(ev.target);
            var CodigoStatusDestino = StatusDestino.id.replace('Status', '');

            // se mudou o item de raia...
            if (CodigoStatusDestino != CodigoStatusOrigem) {
                if (ev.ctrlKey) {
                    var readOnly = 'S';
                    var CodigoItemBacklog = ItemBackLog.replace("ItemBacklog_", "");
                    window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/DetalhesItemSprint.aspx?CI=' + CodigoItemBacklog + '&RO=' + readOnly + '&CS=' + CodigoStatusOrigem + '&CP=' + codigoProjeto, 'Detalhes do Item', 850, 520, '', null);
                }
                mudaItemBackLogRaia(ItemBackLog, CodigoStatusOrigem, CodigoStatusDestino);
            }
            else
                // se manteve na mesma raia, apenas reposiciona na última posição
                StatusDestino.appendChild(document.getElementById(ItemBackLog));
        }

        function getObjetoStatus(objeto) {
            // O objetivo é localizar o "TD" correspondente a Raia onde o itemBackLog foi solto.
            // Se soltou em cima de outro "itemBackLog" precisa "subir" até o objeto correspondente ao "TD" da raia
            if (objeto.id.indexOf('Status') < 0) {
                do {
                    // sobe um nível na hierarquia de "paternidade"
                    objeto = objeto.parentNode;
                    // se encontrou o nível correto, para de procurar.
                    if (objeto.id.indexOf('Status') >= 0)
                        break;
                } while (objeto)
            }
            return objeto;
        }

        // função responsável por fazer a mudança do item de raia
        // A mudança ocorrerá primeiro no servidor
        function mudaItemBackLogRaia(ItemBackLog, CodigoStatusOrigem, CodigoStatusDestino) {
            var CodigoItemBacklog = ItemBackLog.replace("ItemBacklog_", "");
            // faz um "callback" para o servidor registrar a mudança
            cb.PerformCallback("ajusta;" + CodigoItemBacklog + ";" + CodigoStatusOrigem + ";" + CodigoStatusDestino);
            // a mudança no cliente ocorrerá na função "ExecutaMudancaRaiaCliente"
        }

        function ExecutaMudancaRaiaCliente(s, e) {
            var retorno = s.cp_retorno;
            if (retorno == "OK") {
                var parametros = s.cp_param.split(';');
                var ItemBacklog = "ItemBacklog_" + parametros[0];
                var StatusDestino = document.getElementById("Status" + parametros[2]);

                // executa a mudança de raia no cliente
                var divItemBackLog = document.getElementById(ItemBacklog);
                StatusDestino.appendChild(divItemBackLog);
                // atualiza o conteúdo do itemBackLog
                divItemBackLog.outerHTML = s.cp_Div;

                // ajusta a quantidade de itens nas raias
                ajustaQuantidades(parametros[1], parametros[2]);
            }
            else if (retorno == "ERRO") {
                // mostra a mensagem de erro
                window.top.mostraMensagem(s.cp_param, 'erro', true, false, null);
                FechaFullScreen();
            }
        }

        function ajustaQuantidades(origem, destino) {
            hOrigem = document.getElementById('headerStatus' + origem);
            hdestino = document.getElementById('headerStatus' + destino);
            valorOrigem = parseInt(hf.Get('hfStatus' + origem));
            valorDestino = parseInt(hf.Get('hfStatus' + destino));
            // diminui 1 da fase de origem
            hOrigem.textContent = hOrigem.textContent.replace(valorOrigem, valorOrigem - 1);
            hf.Set("hfStatus" + origem, valorOrigem - 1);
            // aumenta 1 na fase de destino
            hdestino.textContent = hdestino.textContent.replace(valorDestino, valorDestino + 1);
            hf.Set("hfStatus" + destino, valorDestino + 1);
        }

        function detalheItemBacklog(id, codigoStatus, codigoProjeto) {
            var readOnly = 'S';
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/DetalhesItemSprint.aspx?CI=' + id + '&RO=' + readOnly + '&CS=' + codigoStatus + '&CP=' + codigoProjeto, 'Detalhes do Item', 850, 520, '', null);
            FechaFullScreen();
        }

        function abreFullscreen(url, altura) {
            //debugger
            if (!window.top.pcModal.GetMaximized()) {
                window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/' + url, '', screen.availWidth - 25, altura, '', null);
                window.top.pcModal.SetMaximized(true);
                //alert(imgFullscreen.GetImageUrl());
            }
            else {
                window.top.pcModal.SetMaximized(false);
                window.top.pcModal.UpdatePosition();                  
                window.top.fechaModal();
                //alert(imgFullscreen.GetImageUrl());
            }
        }

        function abreGrafico(codigoProjeto) {
            window.top.fechaModal();
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/grafico_001.aspx?CP=' + codigoProjeto, 'Evolução', null/*screen.availWidth - 50*/, null/*450*/, '', null);
            window.top.pcModal.SetMaximized(false);
            window.top.pcModal.UpdatePosition();
        }
        function abreReuniaoDiaria(codigoProjeto) {
            window.top.fechaModal();
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Agil/CalendarioReuniaoDiariaSprint.aspx?CP=' + codigoProjeto, 'Reunião', (screen.availWidth / 4) - 10, 250, '', null);
            FechaFullScreen();
        }

        function abreReuniaoDiaria(codigoProjeto, dia, mes, ano) {
            //debugger
            pcCalendario.Hide();
            window.top.fechaModal();
            window.top.pcModal.SetMaximized(false);

            var altura = (screen.availHeight - 290);
            var largura = (screen.availWidth - 250);

            var url = window.top.pcModal.cp_Path + '_Projetos/Agil/ReuniaoDiariaSprint.aspx?CP=' + codigoProjeto;
            url += '&ano=' + ano;
            url += '&mes=' + mes;
            url += '&dia=' + dia;
            url += '&alt=' + altura;
            url += '&larg=' + largura;

            window.top.showModal(url, 'Reunião', largura, altura, '', null);
                      
        }

        function FechaFullScreen() {
            document.getElementById("spBodyBoard").style.height = '';
            if (document.cancelFullScreen) {
                document.cancelFullScreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitCancelFullScreen) {
                document.webkitCancelFullScreen();
            }

            window.top.document.getElementById("spBodyBoard").style.height = screen.availHeight - 100 + 'px';
            window.top.document.getElementById("formMaster").style.height = screen.availHeight - 50 + 'px';
            window.top.document.getElementById("sp_Tela_1_CC").style.height = screen.availHeight - 50 + 'px';
            window.top.document.getElementById("sp_Tela").style.height = screen.availHeight - 50 + 'px';
            window.top.document.getElementById("sp_Tela_1").style.height = screen.availHeight - 50 + 'px';
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin: 0px; overflow: auto;">

    <%--<input type="button" value="clique para alternar 007" onclick="toggleFullScreen()">--%>

    <form id="form1" runat="server" enableviewstate="False">
        <div id="dvFiltro">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td align="left" width="30">
                        <dxcp:ASPxImage ID="imgFullscreen" runat="server"
                            ClientInstanceName="imgFullscreen" ImageUrl="~/imagens/fullscreen.png"
                            ShowLoadingImage="true" Cursor="pointer" ToolTip="Modo tela cheia">
                        </dxcp:ASPxImage>
                    </td>
                    <td align="left" width="30">
                        <dxcp:ASPxImage ID="imgBurndown" runat="server"
                            ClientInstanceName="imgBurndown" ImageUrl="~/imagens/burndown.png"
                            ShowLoadingImage="True" Cursor="pointer"
                            ToolTip="Visualizar gráfico de evolução">
                        </dxcp:ASPxImage>
                    </td>
                    <td align="left" width="30">
                        <dxcp:ASPxImage ID="imgReuniaoDiariaSprint" runat="server"
                            ClientInstanceName="imgReuniaoDiariaSprint" ImageUrl="~/imagens/reuniaoAgil.png"
                            ShowLoadingImage="True" Cursor="pointer"
                            ToolTip="Reunião">
                        </dxcp:ASPxImage>
                    </td>
                    <td align="right">
                        <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server"
                            ClientInstanceName="txtPesquisa"
                            NullText="Pesquisar por palavra chave..." Width="350px" Height="25px" CssPostfix="MaterialCompact" CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css">
                            <ClientSideEvents ButtonClick="function(s, e) {
	callbackFiltro.PerformCallback();
}" />
                            <Buttons>
                                <dxe:EditButton>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Search"
                                            HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                    </Image>
                                </dxe:EditButton>
                            </Buttons>
                            <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                        </dxe:ASPxButtonEdit>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvBoard">
            <div id="spHeaderBoard" runat="server"></div>
            <div id="spBodyBoard" style="height: 77%" runat="server"></div>
            <div id="dvFooter" runat="server"></div>
        </div>

        <dx:ASPxCallback ID="cb" runat="server" OnCallback="cb_Callback">
            <ClientSideEvents EndCallback="function(s, e) {ExecutaMudancaRaiaCliente(s,e); document.getElementById('dvFooter').innerHTML = s.cp_Footer;	}" />
        </dx:ASPxCallback>
        <dxcp:ASPxCallback ID="callbackFiltro" runat="server"
            ClientInstanceName="callbackFiltro" OnCallback="callbackFiltro_Callback1">
            <ClientSideEvents EndCallback="function(s, e) {
 document.getElementById('spHeaderBoard').innerHTML = s.cp_spHeaderBoard;
 document.getElementById('spBodyBoard').innerHTML = s.cp_spBodyBoard;
}" />
        </dxcp:ASPxCallback>
        <dx:ASPxHiddenField ID="hf" runat="server"></dx:ASPxHiddenField>
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
                                    OnDayCellPrepared="ASPxCalendar1_DayCellPrepared" ShowClearButton="False"
                                    ShowTodayButton="False" ShowWeekNumbers="False" Width="100%">
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
                                                <asp:Label ID="Label6" runat="server" EnableViewState="False"><asp:Literal runat="server" Text="<%$ Resources:traducao, taskboard_reuni_es_realizadas %>" /></asp:Label>&nbsp; </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
