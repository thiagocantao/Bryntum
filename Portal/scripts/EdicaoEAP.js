// JScript File


//    window.attachEvent("onload", trataEntradaTela);
//    window.attachEvent("onbeforeunload", trataSaidaTela); 
    
    function trataEntradaTela()
    {
        if (window.hfGeral)
        {
            //Vejo que nao tenha bloqueo por uma outra tela ou usario.
            if (hfGeral.Contains("MensajeBloqueio"))
            {
                if( "" != hfGeral.Get("MensajeBloqueio").toString())
                {
                    window.top.mostraMensagem(hfGeral.Get("MensajeBloqueio").toString(), 'erro', true, false, null);
                } // if( "" != hfGeral.Get("MensajeBloqueio").toString())
            } // if (hfGeral.Contains("MensajeBloqueio"))
        } // if (window.hfGeral)
    }

    function trataSaidaTela()
    {
        if (window.hfGeral)
        {
            callbackGeral.PerformCallback("");
        } // if (window.hfGeral)
    }
    
   function openNewWindow(URLtoOpen) 
   {
       URLtoOpen = URLtoOpen + hfGeral.Get("Parametros");
       var newURL = '';
       var str = URLtoOpen.split('?');
       newURL = str[0] + '?auxVar=1';
       var params = str[1].split('&');

       for (i = 0; i < params.length; i++) {
           var parametro = params[i].split('=');
           newURL += '&' + parametro[0] + '=' + escape(decodeURIComponent(parametro[1]));
       }

       //escape(decodeURIComponent(URLtoOpen))
       window.top.showModal2(newURL, 'Detalhes', 800, 320, '');
   }
   
   function setStatusMessage(message)
   {
     window.status = message;
   }
