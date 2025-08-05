/// <summary>
/// Rotinas para inserção de textos dentro de um controle do tipo TEXTAREA.
/// </summary>
/// <remarks>
/// As rotinas presentes neste arquivo permitem incluir um texto dentro de um TEXTAREA 
/// na posição em que se encontrar o cursor.
///
/// Script obtido por Alexandre Fuentes e traduzido para o inglês por Géter Miranda
// usados na tela de edição de workflow
/// </remarks>


/// <summary>
/// variáveis globais usadas nas rotinas
/// </summary>
var __ta_navegador = navigator.userAgent;
var __ta_isIE = /msi/i.test(__ta_navegador);
var __ta_isOpera = /opera/i.test(__ta_navegador);
var __ta_isMozilla = /gecko/i.test(__ta_navegador);
var __ta_isOtherBrowser = !(__ta_isIE || __ta_isMozilla);

var __ta_insertText, __ta_control, __ta_auxText, __ta_currentText = "";

function __ta_onTextChange_ie()
{
	__ta_auxText = document.selection.createRange().text;
	if (__ta_control.createTextRange)
		__ta_control.posi = document.selection.createRange().duplicate();
	return true;
}

function __ta_getText_ie()
{
	return __ta_auxText;
}

function __ta_getText_mozilla()
{
	with (__ta_control) return value.substring(selectionStart, selectionEnd);
}

function __ta_getText_otherBrowser()
{
	return "";
}

function __ta_insertText_mozilla(f, x)
{

	var _ini = f.selectionStart;
	var _fin = f.selectionEnd;

	if (f.value != "" && _ini == 0) {
		_ini = f.value.length;
		_fin = _ini;
    }
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

function __ta_insertText_otherBrowse(f, x) // opera u otros navegadores desconocidos
{
    
	f.value += x;
	f.focus();
}

function __ta_insertText_ie(f, x)
{
    try {
	    f.focus();
	    if (f.createTextRange) // && f.posi)	{
	    {
		    if (!f.posi)
		        __ta_onTextChange_ie();
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
        // qualquer erro, ignora o procedimento
    }
}

function __ta_initialize(control)
{
	__ta_control = control;
	
	if (__ta_isOpera || __ta_isMozilla)
	{
		__ta_insertText = function(f, x) {__ta_insertText_mozilla(f, x);};
		__ta_currentText = __ta_getText_mozilla;
	}
    else if(__ta_isOtherBrowser)
    {
		__ta_insertText = function(f, x) {__ta_insertText_otherBrowse(f, x);};
		__ta_currentText = __ta_getText_otherBrowser;
	}
    else if(__ta_isIE)
    {
		__ta_control.onchange = __ta_onTextChange_ie;
		__ta_control.onclick  = __ta_onTextChange_ie;
		__ta_insertText = function(f, x) {__ta_insertText_ie(f, x);};
		__ta_currentText = __ta_getText_ie;
	}
	return control;
}
//-------------------------------------------------- fim Session TEXTAREA
