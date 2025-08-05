
var connSignalRnewBrisk = null;
var OnConnectedSignalRnewBrisk;

async function realizaConexaoSignalR(token) {

    if (connSignalRnewBrisk) {
        return;
    }
    const isLoginValue = sessionStorage.getItem('isLogin');

    let queryParams = '';

    if (isLoginValue === 'S') {
        queryParams = `?login=${false}`;
    } else {
        queryParams = `?login=${true}`;
    }

    sessionStorage.setItem('isLogin', 'S');

    connSignalRnewBrisk = new signalR.HubConnectionBuilder()
        .withUrl(`${urlWSnewBriskBase}/hubs/notification/${queryParams}`, 
            {
                accessTokenFactory: async () => token,
                transport: signalR.HttpTransportType.WebSockets,
                skipNegotiation: true
            })
        .configureLogging(signalR.LogLevel.Information)
        .withAutomaticReconnect()
        .build();
    async function start() {
        try {
            await connSignalRnewBrisk.start();
            if (OnConnectedSignalRnewBrisk) {
                OnConnectedSignalRnewBrisk();
            }
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connSignalRnewBrisk.on("newDutyNotification", function (message) {

        var jsonData = JSON.parse('[' + message + ']');

        function filtrarPorEntidade(pCodigoEntidade) {
            return jsonData.filter((jsonInfo) => jsonInfo.codigoEntidade == pCodigoEntidade);
        }
        var codigoEntidade = hfSignalR.Get("CodigoEntidadeUsuarioLogado");
        jsonData = filtrarPorEntidade(codigoEntidade);

        if (jsonData[0] != null) {
            atualizaNotificacoesTela(jsonData[0]);
            sessionStorage.setItem('listaNotificacoes', JSON.stringify(jsonData[0]));
            callbackNotificacoes.PerformCallback(JSON.stringify(jsonData[0]));
        }
    });

    connSignalRnewBrisk.on("licenseIssue_sce", function (userNameEconnectionId) {
        connSignalRnewBrisk.stop();
        connSignalRnewBrisk = null;
        window.location.href = hfSignalR.Get("baseUrl") + "/avisoDesconexao.aspx?T=li";
    });

    connSignalRnewBrisk.on("disconnectRequested", function (msg) {
        connSignalRnewBrisk.stop();
        connSignalRnewBrisk = null;
        window.location.href = hfSignalR.Get("baseUrl") + "/avisoDesconexao.aspx?T=dr";
    });

    connSignalRnewBrisk.on("disconnectByDoubleConnection", function (userNameEconnectionId) {
        connSignalRnewBrisk.stop();
        connSignalRnewBrisk = null;
        window.location.href = hfSignalR.Get("baseUrl") + "/avisoDesconexao.aspx?T=ddc";
    });

    connSignalRnewBrisk.on("expiredSession", function (userNameEconnectionId) {
        connSignalRnewBrisk.stop();
        connSignalRnewBrisk = null;
        window.location.href = hfSignalR.Get("baseUrl") + "/avisoDesconexao.aspx?T=es";
    });

    connSignalRnewBrisk.on("SuccessConnected", function (msg) {
        console.log('Usuário conectado com sucesso masterpage.');
    });

    connSignalRnewBrisk.onreconnecting(error => {
        console.log(`Connection lost due to error "${error}". Reconnecting.`);
        console.assert(connSignalRnewBrisk.state === signalR.HubConnectionState.Reconnecting);
    });
    start();
}

async function conectaSignalR(tokenAcessoNewBrisk) {
   
    var token = getTokenNBRFromMemory(tokenAcessoNewBrisk);
    if (token != null) {
        realizaConexaoSignalR(token);
    }
}

function DesconectaUsuarioSignalR() {
    try {
        sessionStorage.removeItem('listaNotificacoes');
        sessionStorage.removeItem("taNBR");
        connSignalRnewBrisk.stop();
        connSignalRnewBrisk = null;
    } catch (err) {
    }
}

function RemoveTodasConexoesUsuario(pUserName) {
    connSignalRnewBrisk.invoke("removeConexoesUsuario", pUserName).then(function () {
        console.log("Solicitada a remoção de todas as conexões do usuario: ", userName);
    }).catch(function (err) {
        console.error(err.toString());
    });
}

function getTokenNBRFromMemory(tokenAcessoNewBrisk) {

    var token = null;
    if (tokenAcessoNewBrisk == "null" || tokenAcessoNewBrisk == null || tokenAcessoNewBrisk == undefined) {
        token = sessionStorage.getItem('taNBR');
    }
    else {
        token = tokenAcessoNewBrisk;
        sessionStorage.setItem('taNBR', token);
    }
    return token;
}
