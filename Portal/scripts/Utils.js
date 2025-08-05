// JScript File

//variaveis globais
var _navegador = navigator.userAgent;
var ie = /msi/i.test(_navegador);
var op = /opera/i.test(_navegador);
var mo = /gecko/i.test(_navegador);
var otro = !(ie || mo);


//------------------------------------------------------------------------
//Session de manejos de TEXTAREA
//------- -- ------- -- --------

//Admite inserir textos na posição do cursor no TEXTAREA.

var _insertor, _insertar, _formulario, _texto, _lector = "";


function datos_ie()
{
	_texto = document.selection.createRange().text;
	if (_formulario.createTextRange)
		_formulario.posi = document.selection.createRange().duplicate();
	return true;
}

function captura_ie()
{
	return _texto;
}

function captura_mo()
{
	with (_formulario) return value.substring(selectionStart, selectionEnd);
}

function captura_otro()
{
	return "";
}

function poner_mo(f, x)
{
	var _ini = f.selectionStart;
	var _fin = f.selectionEnd;
	var inicio = f.value.substr(0, _ini);
	var fin = f.value.substr(_fin, f.value.length);

	f.value = inicio + x + fin;
	if (_ini == _fin)	{
		f.selectionStart = inicio.length + x.length;
		f.selectionEnd = f.selectionStart;
	}
	else	{
		f.selectionStart = inicio.length;
		f.selectionEnd = inicio.length + x.length;
	}
	f.focus();
}

function poner_otro(f, x) // opera u otros navegadores desconocidos
{
	f.value += x;
	f.focus();
}

function poner_ie(f, x)
{
    try {
	    f.focus();
	    if (f.createTextRange) // && f.posi)	{
	    {
		    if (!f.posi)
		        datos_ie();
		    with(f)
		    {
			    var actuar = (posi.text == "");
			    posi.text = x;
			    if (!actuar)
				    posi.moveStart("character", -x.length);
			    posi.select();
		    }
	    }
	}
    catch (e) {
        
    }
}

function ini_editor(formu)
{
	_formulario = formu;
	
	if (op || mo)
	{
		_insertar = function(f, x) {poner_mo(f, x);};
		_lector = captura_mo;
	}
    else if(otro)
    {
		_insertar = function(f, x) {poner_otro(f, x);};
		_lector = captura_otro;
	}
    else if(ie)
    {
		_formulario.onchange = datos_ie;
		_formulario.onclick  = datos_ie;
		_insertar = function(f, x) {poner_ie(f, x);};
		_lector = captura_ie;
	}
	return formu;
}

//-------------------------------------------------- fim Session TEXTAREA

//------------------------------------------------------------------------
// Div's modales
// ----- -------
var fondo = false;
var mensaje = false;

function creaVentanaDisenhando(){
	fondo = document.createElement("div");
	mensaje = document.createElement("div");
	fondo.setAttribute("id","fondo");
	mensaje.setAttribute("id","msg");
	document.getElementsByTagName("body")[0].appendChild(fondo);
	document.getElementsByTagName("body")[0].appendChild(mensaje);
	mensaje.innerHTML="Hola";
	//"<div id=""DivDisenhando"" style=""left: 49%; top: 331px; width: 200px; position: absolute; height: 42px; display: none;"" class=""DivDisenhando""><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td align=""center"" valign=""middle""><dxe:aspxlabel id=""lblDivDisenhando"" runat=""server"" text=""Disenhando Workflow"" clientinstancename=""lblDivDisenhando"" font-names=""Verdana"" font-size=""8pt""></dxe:aspxlabel></td><td align=""center"" valign=""middle""><img src=""../imagens/Workflow/loading2.gif"" />&nbsp;</td></tr></table></div>";
}

function cerrarVentanaDisenhando(){
	document.getElementsByTagName("body")[0].removeChild(fondo);
	document.getElementsByTagName("body")[0].removeChild(mensaje);	
	fondo=false;
	mensaje=false;
}		

//---------------------------------------------- fim Session Div's modales