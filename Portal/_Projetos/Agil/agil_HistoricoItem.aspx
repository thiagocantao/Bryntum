<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agil_HistoricoItem.aspx.cs" Inherits="_Projetos_DadosProjeto_agil_HistoricoItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        /*14px 'Roboto Medium', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif*/

        html, body, ul, li {
            margin: 0;
            padding: 0;
            font-family: 'Roboto Medium', sans-serif;
            color: #696969;
            background-color: #E8E8E8;
        }

        .container {
            padding: 20px 0;
        }

            .container h1 {
                text-align: center;
            }

        .timeline {
            position: relative;
            margin: 0 auto;
            width: 90%;
        }

            .timeline ul li {
                margin-bottom: 50px;
                list-style-type: none;
                display: flex;
                flex-direction: row;
                align-items: center;
            }

        .point {
            min-width: 15px;
            height: 15px;
            background-color: #228b22;
            border-radius: 100%;
            z-index: 2;
            border: 2px #a9a042 solid;
            position: relative;
            left: 1px;
        }

        @media (max-width: 800px) {
            .point {
                min-width: 15px;
                height: 15px;
            }

            html, body {
                font-size: 15px;
            }
        }

        @media (max-width: 650px) {
            html, body {
                font-size: 14px;
            }

            .point {
                min-width: 12px;
                height: 12px;
            }
        }

        @media (max-width: 450px) {
            html, body {
                font-size: 10px;
            }

            p {
                padding: 10px !important;
            }
        }

        .timeline ul li .content {
            width: 50%;
            padding: 0 20px;
        }

        .timeline ul li:nth-child(odd) .content {
            padding-left: 0;
        }

        .timeline ul li:nth-child(odd) .date {
            padding-right: 0;
        }

        .timeline ul li:nth-child(even) .content {
            padding-right: 0;
        }

        .timeline ul li:nth-child(even) .date {
            padding-left: 0;
        }

        .timeline ul li .date {
            width: 50%;
            padding: 0 20px;
            font-weight: normal;
        }

            .timeline ul li .date h4 {
                background-color: #D6C41C;
                width: 100px;
                text-align: center;
                padding: 5px 10px;
                border-radius: 10px;
            }

        .timeline ul li .content h3 {
            padding: 10px 20px;
            background-color: #daa520;
            margin-bottom: 0;
            text-align: center;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }

        .timeline ul li .content p {
            padding: 10px 20px;
            background-color: #e1d9ec;
            margin-top: 0;
            text-align: center;
            border-bottom-left-radius: 10px;
            border-bottom-right-radius: 10px;
        }

        .timeline ul li:nth-child(even) {
            flex-direction: row-reverse;
        }

            .timeline ul li:nth-child(even) .date h4 {
                float: right
            }

        .timeline::before {
            content: "";
            position: absolute;
            height: 100%;
            width: 3px;
            left: 50%;
            background-color: #a9a042;
        }
        .fonte-cor-cinza{
            color:gray;
        }
    </style>   
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function decodeHTMLEntities(text) {
            var textArea = document.createElement('textarea');
            textArea.innerHTML = text;
            return textArea.value;
        }

        function carregaItens() {
            $.ajax({
                data: 'codigoItem=' + hfGeral.Get('CodigoItem') + '&iniciaisTipoObjeto=IB',
                url: './taskboard/agil-taskboard-service.asmx/obter-historico-item',
                type: 'POST'
            }).done(function (data, textStatus, jqXHR) {
                //console.log(data);

                var divContainer = document.createElement('div');

                document.querySelector('#divHistoricoItem').appendChild(divContainer);

                var divTimeLine = document.createElement('div');
                divTimeLine.classList.add('timeline');

                var ulList = document.createElement('ul');
                divTimeLine.appendChild(ulList);

                var itens = JSON.parse(data.firstElementChild.innerHTML);

                for (var i = 0; i < itens.length; i++) {
                    var item = itens[i];

                    var liList = document.createElement('li');

                    var divConteudo = document.createElement('div');
                    divConteudo.classList.add('content');

                    var h3TituloConteudo = document.createElement('h3');
                    h3TituloConteudo.innerHTML = item.TipoRegistro;

                    var pConteudo = document.createElement('div');
                    pConteudo.innerHTML = decodeHTMLEntities(item.DetalhesRegistro);

                    divConteudo.appendChild(h3TituloConteudo);
                    divConteudo.appendChild(pConteudo);

                    var divPonto = document.createElement('div');
                    divPonto.classList.add('point');

                    var divData = document.createElement('div');
                    divData.classList.add('date');

                    var h4Data = document.createElement('h4');
                    h4Data.innerHTML = item.DataRegistro;
                    divData.appendChild(h4Data);

                    liList.appendChild(divConteudo);
                    liList.appendChild(divPonto);
                    liList.appendChild(divData);

                    ulList.appendChild(liList);
                    //console.log(item);
                }
                divTimeLine.appendChild(ulList);
                document.querySelector('#divHistoricoItem').appendChild(divTimeLine);
            });
        }
    </script>
</head>
<body onload="carregaItens()">
    <form id="form1" runat="server">
        <div class="container" id="divHistoricoItem"></div>
        <%--<div class="container">
            <h1>Timeline with HTML and CSS</h1>
            <div class="timeline">
                <ul>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019
                            </h4>
                        </div>
                    </li>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019</h4>
                        </div>
                    </li>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019</h4>
                        </div>
                    </li>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019</h4>
                        </div>
                    </li>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019</h4>
                        </div>
                    </li>
                    <li>
                        <div class="content">
                            <h3>Lorem Ipsum is simply</h3>
                            <p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                        </div>
                        <div class="point"></div>
                        <div class="date">
                            <h4>January 2019</h4>
                        </div>
                    </li>
                </ul>
            </div>
        </div>--%>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
    </form>
</body>
</html>
