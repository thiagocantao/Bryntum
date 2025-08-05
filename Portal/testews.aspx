<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testews.aspx.cs" Inherits="testews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var ajax;
        var url = "<%=url%>";

        function mostra()
        {
            alert(url);
        }

        /**
         * Criar o objeto ajax que vai fazer a requisição
         */
        function CreateAjaxObject() {
            if (window.XMLHttpRequest) {// Mozilla, Safari, Novos browsers...
                ajax = new XMLHttpRequest();
            } else if (window.ActiveXObject) {// IE antigo
                ajax = new ActiveXObject("Msxml2.XMLHTTP");
                if (!ajax) {
                    ajax = new ActiveXObject("Microsoft.XMLHTTP");
                }
            }

            if (!ajax)// iniciou com sucesso
                alert("Seu navegador não possui suporte para esta aplicação!");

            ajax.onreadystatechange = function trataResposta() {
                if (ajax.readyState == 4) {
                    AjaxResponseFunction(ajax.status, ajax.responseText);
                }
            };
        }

        /*
         * Envia os dados para a URL informada
         *
         * @param url Arquivo que irá receber os dados
         * @param dados dados a serem enviados no formato querystring nome=valor&nome1=valor2
         * @param AjaxResponseFunction  variável to tipo function(string) para receber a resposta do ajax
         */
        function SendData(url_par, dados, AjaxResponseFunction) {
            CreateAjaxObject();
            if (ajax) {
                //ajax.onreadystatechange = function trataResposta() {
                //    if (ajax.readyState == 4) {
                //        AjaxResponseFunction(ajax.status, ajax.responseText);
                //    }
                //};
                //definir o tipo de método
                ajax.open("POST", url + "/testeComunicacaoLista", true);

                //definir o encode do conteúdo
                //ajax.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                ajax.setRequestHeader("Content-Type", "text/xml; charset=UTF-8");

                //tamanho dos dados enviados
                ajax.setRequestHeader('Content-length', dados.length);

                //enviar os dados
                ajax.send();
            }
        }

        /**
         * Chama o webservice
         */
        function CallWs() {
            var dados = '';
            SendData("", dados, AjaxResponseFunction);

     //       var ws = new webService("");

            return false;
        }

        /**
         * tratar a resposta do servidor
         * @param status status da resposta
         * @response resposta do servidor
         */
        function AjaxResponseFunction(status, response) {

            var lblWsTasquesWS = document.getElementById('lblWsTasquesWS');

            if (ajax.status != 200)
                lblWsTasquesWS.innerHTML = "Erro: " + ajax.status;
            else
                lblWsTasquesWS.innerHTML = "acerto"

            //escrever na div de resposta
            var div = document.getElementById('Panel1');
            div.innerHTML = response;            
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnWSTasques" runat="server" Text="Testar WS Tasques (1)" OnClick="btnWSTasques_Click" /></td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblWsTasques1" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
             <table>
                <tr>
                    <td>
                        <asp:Button ID="btnWsTasques_JS" runat="server" Text="Testar WS Tasques (2)" OnClientClick="return CallWs()" /></td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblWsTasquesWS" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <p>
            <asp:GridView ID="gvTeste" runat="server">
            </asp:GridView>
        </p>
        <asp:Label ID="lblRetorno" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <dxcp:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton">
        </dxcp:ASPxButton>
    </form>
</body>
</html>
