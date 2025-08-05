// JScript File

function MostrarFilas(Fila) {
   // Almacenamos en "elementos" el objeto "Fila" recibido como parámetro
   var elementos = document.getElementsByName(Fila);

   // Mostramos todos los objetos del mismo identificador
   // Con propiedad display = "" dejamos al navegador que ponga su tipo por defecto
   for (i = 0; i < elementos.length; i++)
      elementos[i].style.display ="";
}

function OcultarFilas(Fila) {
    var opcion = Fila.GetValue();
    var _tdMapaCarregado = document.getElementById('tdMapaCarregado');
    var _tdMapaIluminado = document.getElementById('tdMapaFlashView');
    var _tdEmArvore = document.getElementById('tdEmArvore');
    
    document.getElementById('tdMapa01').style.display = "";
    document.getElementById('tdMapa02').style.display = "";
    document.getElementById('tdAjuda').style.display = "";
        
    if("MI" == opcion)
    {
        _tdMapaIluminado.style.display = "";
        _tdEmArvore.style.display = "none";
        _tdMapaCarregado.style.display = "none";
    }
    else if("EA" == opcion)
    {
        _tdMapaIluminado.style.display = "none";
        _tdEmArvore.style.display = "";
        _tdMapaCarregado.style.display = "none";
    }
    else if("LI" == opcion)
    {
        _tdMapaIluminado.style.display = "none";
        _tdEmArvore.style.display = "none";
        _tdMapaCarregado.style.display = "none";
    }
    else if("FM" == opcion)
    {
        _tdMapaIluminado.style.display = "none";
        _tdEmArvore.style.display = "none";
        _tdMapaCarregado.style.display = "none";
    }
    else if ("MC" == opcion) {
        _tdMapaIluminado.style.display = "none";
        _tdEmArvore.style.display = "none";
        _tdMapaCarregado.style.display = "";
        var height = Math.max(0, document.documentElement.clientHeight - 150);
        document.getElementById("frameMapaCarregado").src = "mapa/NovoMapaEstrategico.aspx?cm=" + ddlMapa.GetValue();
        document.getElementById("frameMapaCarregado").style.height = height + "px";
    }
}