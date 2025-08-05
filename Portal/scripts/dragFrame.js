/******************************

Draggable Window: JS+CSS

JS designed by
Rogério Alencar Lino Filho
email: rogeriolino@gmail.com
http://rogeriolino.wordpress.com/

******************************/
var x, y, offsetX, offsetY;
var id = "frm001";
var drag = false;
var mini = false;
var offset = false;
//
//addStyle
var style = document.createElement("link");
style.setAttribute("rel", "stylesheet");
style.setAttribute("type", "text/css");
style.setAttribute("href", "script/style.css");
document.getElementsByTagName("head").item(0).appendChild(style);
//
function selecao(target, act){
	if (!act) {
		if (typeof target.onselectstart != "undefined") //IE route
			target.onselectstart = function() { return false; }
		else if (typeof target.style.MozUserSelect != "undefined") //Firefox route
			target.style.MozUserSelect = "none";
		else //All other route (ie: Opera)
			target.onmousedown = function() { return false; }
	} else {
		if (typeof target.onselectstart != "undefined") //IE route
			target.onselectstart = function() { return true; }
		else if (typeof target.style.MozUserSelect != "undefined") //Firefox route
			target.style.MozUserSelect = "none";
		else //All other route (ie: Opera)
			target.onmousedown = function() { return true; }
	}
}
//
function findPos(obj) {
	var left = 0;
	var top = 0;
	if (obj.offsetParent) {
		left = obj.offsetLeft;
		top = obj.offsetTop;
		while (obj = obj.offsetParent) {
			left += obj.offsetLeft;
			top += obj.offsetTop;
		}
	}
	offsetX = left;
	offsetY = top;
}
//
function pos(event) {
    var vlrInicial;
    if (document.all) {
        x = window.event.clientX;
        y = window.event.clientY;
    } else {
        x = event.pageX;
        y = event.pageY;
    }

    vlrInicial = x;
    findPos(document.getElementById(id));
	if (!offset) {
		
		offsetX = x - offsetX;
		offsetY = y - offsetY;
	}
	if (id) { var alvo = document.getElementById(id); }
//	document.getElementById("pos").innerHTML = "x: "+x+"<br />y: "+y+"<br />offsetX: "+offsetX+"<br />offsetY: "+offsetY;
	var body = document.getElementsByTagName("body").item(0);
	if (drag) {
	    alvo.style.top = (y - offsetY) + "px";
	    alvo.style.left = (x - offsetX) + "px";
	    alvo.style.cursor = "move";
	    alvo.style.opacity = 0.7;
	    alvo.style.filter = "alpha(opacity=70)";
	    selecao(body, false);
	} else {
	    alvo.style.cursor = "default";
	    alvo.style.opacity = 1;
	    alvo.style.filter = "alpha(opacity=100)";
	    selecao(body, true);
	}

	alert((x - offsetX) + '<--' + vlrInicial);
}
//
function startDrag(frm) { drag = true; offset = true; id = frm.id; }
function stopDrag(frm) { drag = false; offset = false; id = frm.id; }


//
document.onmousemove = function(event) { pos(event); }
//