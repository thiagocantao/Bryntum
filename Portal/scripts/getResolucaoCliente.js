        top.window.moveTo(0, 0);
        if (document.all) {
            top.window.resizeTo(screen.availWidth, screen.availHeight);
        }
        else if (document.layers || document.getElementById) {
            if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                window.resizeTo(top.screen.availWidth, top.screen.availHeight);
            }
        }


    var w = ASPxClientUtils.GetDocumentClientWidth() - 12;
    var h = ASPxClientUtils.GetDocumentClientHeight() - 10 + 54;

//        if (navigator.appName.indexOf("Microsoft")!=-1)
//        {
//            w = document.body.clientWidth;
//            h = document.documentElement.clientHeight - 10;
//        }
//        else
//        {
//            w = window.innerWidth - 12;            
//            h = window.innerHeight - 15;
//        }

    var urlMobile = document.getElementById("urlMobile").value;
    if ((urlMobile == "") || ((urlMobile != "") && w >= 992)) {
        res = "&res=" + w + "x" + h + "&d=" + screen.colorDepth;
        top.location.href = "getResolucaoCliente.aspx?action=set" + res + '&' + hfRes.Get('params')
    }
    else {
        top.location.href = urlMobile;
    }
