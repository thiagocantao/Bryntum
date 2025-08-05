<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/novaCdis.master" CodeFile="UsuariosConectados.aspx.cs" Inherits="UsuariosLogados_UsuariosConectados" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <script src="scripts/jquery-3.1.1.min.js"></script>
    <script src="scripts/json2.js"></script>
    <script src="scripts/jquery.signalR.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script type="text/javascript">

        var matrizParametros = [];
        var corDaLinhaMouseOver;
        var carregouListaUsuariosConectados = false;
        window.onload = function () {
            OnConnectedSignalRnewBrisk = function () {
                console.log("função OnConnectedSignalRnewBrisk preenchida. Hora de buscar os usuários");


                connSignalRnewBrisk.invoke("GetConnectedUsers").then((users) => {
                    console.log("Recebidos usuários conectados (usuariosConectados.aspx)");
                    if (carregouListaUsuariosConectados == false) {
                        CarregaListaUsuarios(users);
                        carregouListaUsuariosConectados = true;
                    }

                    connSignalRnewBrisk.on("NewUserConnected", function (userName) {
                        console.log("Incluir na grid novo usuário conectado (usuariosConectados.aspx)", userName);
                        AdicionarUsuarioLista(userName);
                    });

                    connSignalRnewBrisk.on("UserDisconnected", function (userName) {
                        console.log("Retirar da grid o usuário desconectado: (usuariosConectados.aspx)" + userName);
                        RemoverUsuarioLista(userName);
                    });

                }).catch((err) => {
                    console.log("Erro ao receber usuários conectados." + err.toString());
                });
            };
        }

        function CarregaListaUsuarios(usuarios) {
            for (i = 0; i < usuarios.length; i++) {
                AdicionaLinhaTabelaUsuariosLogados(usuarios[i])
            }
        }

        function RemoverUsuarioLista(userName) {
            RemoveLinhaTabelaUsuariosLogados(userName);
        }

        function AdicionarUsuarioLista(userName) {

            AdicionaLinhaTabelaUsuariosLogados(userName);
        }

        function ForcaDesconexaoUsuario(userName) {

            RemoveTodasConexoesUsuario(userName);

            RemoveLinhaTabelaUsuariosLogados(userName);
        }

        function AdicionaLinhaTabelaUsuariosLogados(pNomeUsuario) {

            var tabela = document.getElementById("tbUsuariosConectados");
            var qtdLinhas = tabela.rows.length;

            if (qtdLinhas > 0) {
                for (var i = 0; i < qtdLinhas; i++) {
                    var nomeUsuario = tabela.rows[i].cells[1].innerText;
                    if (pNomeUsuario == nomeUsuario) {
                        return;
                    }
                }
            }

            var data = new Date();
            var dia = String(data.getDate()).padStart(2, '0');
            var mes = String(data.getMonth() + 1).padStart(2, '0');
            var ano = data.getFullYear();
            var horas = data.getHours();
            var minutos = data.getMinutes();
            var segundos = data.getSeconds();

            var horaAtual = [horas, minutos, segundos].join(':');

            var dataAcesso = dia + '/' + mes + '/' + ano + " : " + horaAtual;

            var funcaoDesconecta = "ForcaDesconexaoUsuario('" + pNomeUsuario + "')";

            var userName = localStorage.getItem('BRISK-USER');

            if (userName == pNomeUsuario) funcaoDesconecta = "#";

            var html = "<tr onmouseover='trataMouseOver(this)' onmouseout='trataMouseOut(this)' onclick='trataMouseClick(this)'>"
            html = html + "<td class='tdLinha'>"
            html = html + "<a href = '#' onclick = " + funcaoDesconecta + " > <img src='imagens/botoes/excluirReg(16x16).PNG' /> </a >";
            html = html + "</td>"
            html = html + "<td class='tdLinha'>" + pNomeUsuario + "</td>";
            html = html + "<td class='tdLinha'>" + dataAcesso + "</td>";
            html = html + "</tr>";

            $("#tbUsuariosConectados>tbody").append(html);
        }

        function trataMouseClick(linha) {
            var tabela = document.getElementById("tbUsuariosConectados");
            for (var i = 1; i < tabela.rows.length; i++) {
                tabela.rows[i].classList.remove("selected");
            }
            linha.classList.add("selected");
        }

        function RemoveLinhaTabelaUsuariosLogados(pNomeUsuario) {
            var tabela = document.getElementById("tbUsuariosConectados");
            var qtdLinhas = tabela.rows.length;

            if (qtdLinhas > 0) {
                for (var i = 0; i < qtdLinhas; i++) {
                    var nomeUsuario = tabela.rows[i].cells[1].innerText;
                    if (pNomeUsuario == nomeUsuario) {
                        tabela.deleteRow(i);
                    }
                }
            }
        }
    </script>
    <div style="width: 99%; padding-left: 15px; padding-top: 10px">
        <table cellpadding="0" cellspacing="0" id="tbUsuariosConectados" style="width: 100%; box-shadow: 1px 1px 1px 1px #969696;overflow-y:visible;">
            <thead>
                <tr>
                    <th class="tdCabecalho" style="width: 15%">Excluir Conexão</th>
                    <th class="tdCabecalho" style="width: 55%">Usuário</th>
                    <th class="tdCabecalho" style="width: 10%;">Hora/Acesso</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .tdCabecalho {
            height: 40px;
            border: 1px solid #DFDFDF;
            font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif !important;
            font-size: 14px;
            padding-left: 10px;
            text-align: left;
            font-weight: 500;
        }

        .tdLinha {
            border: 1px solid #DFDFDF;
            padding-left: 10px;
            height: 40px;
            font-size:14px;
        }

        .tr {
            background: #ffffff;
        }

        tr:hover {
            background-color: #EEEEEE !important;
        }

        tr.selected {
            background: #A4D08A !important;
            color: white !important;
        }

        tbody > tr:nth-child(even) {
            background: #ffffff;
        }

        tbody > tr:nth-child(odd) {
            background: #F5F5F5;
        }
    </style>
</asp:Content>

