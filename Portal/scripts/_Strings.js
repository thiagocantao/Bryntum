// JScript File

// =============================================================================
// Retirar espaços direita/Esquerda
// =============================================================================
function trim(s)
{
	return rtrim(ltrim(s));
}

function ltrim(s)
{
	var l=0;
	while(l < s.length && s.charAt(l) == ' ')
	{
		l++; 
	}
	return s.substring(l, s.length);
}

function rtrim(s)
{
	var r=s.length -1;
	while(r > 0 && s.charAt(r) == ' ')
	{
		r-=1;	
    }
	return s.substring(0, r+1);
}


// =============================================================================
// Limitar a quantidade de caracteres de um ASPxMemo - DEV EXPRESS
//         como usar: Inclua o evento onkeyup="limitaASPxMemo(this, <limite>) 
//         no textArea que deve ser limitado
// =============================================================================
function limitaASPxMemo(objectCampoMemo, limite)
{
    if (objectCampoMemo.GetText().length > limite)
        objectCampoMemo.SetText( objectCampoMemo.GetText().substring(0, limite) );
}

// =============================================================================
// Limitar a quantidade de caracteres de um textArea - HTML
//         como usar: Inclua o evento onkeyup="limitaCampoMemoDev(this, <limite>) 
//         no textArea que deve ser limitado
// =============================================================================
function limitaCampoMemoDev(objectTextArea, limite)
{
    if (objectTextArea.value.length > limite)
        objectTextArea.value = objectTextArea.value.substring(0, limite);
}
