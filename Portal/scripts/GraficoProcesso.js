// JScript File

//---[ VARIAVEIS GLOBAIS
var __wf_chartObj; // do tipo FusionChart para conter uma referência ao chart sendo apresentado na tela

/// <summary>
/// Busca o novo XML no hidden field hfXML e o atribui ao componente flash. 
/// Assume que haverá uma referência para o gráfico na  variável __wf_chartObj;
/// </summary>
/// <returns type="boolean">Se deu certo a atualização do hidden field, retorna true. Caso contrário, false.</returns>
function setXmlComponente()
{
    var strXml = hfXML.Get("xmlWorkflow");
    
    if ( (null != strXml) && (strXml.length > 0) )
    {
        if (null != __wf_chartObj)
        {
            __wf_chartObj.setDataXML(strXml);
            return true;
        }
    }
}

/// <summary>
/// Devolve o texto de um controle. Antes de devolver, verifica em qual propriedade está o texto, uma vez
/// que cada browser trabalha com um tipo de propriedade diferente.
/// </summary>
/// <param name="control" type="DOM Object">Objeto DOM cuja propriedade 'texto' será alterada.</param>
/// <returns type="string">string com o texto do controle. Em caso de erro, retorna null.</returns>
function getTextFromControl(control)
{
    
    var text = null;
    if (null != control) 
    {
        if (isIE) 
            text = control.value;
        else 
            text = control.textContent;
    }          

    return text;    
}         

///
///
///
function renderizaFlash()
{
    var height = Math.max(0, document.documentElement.clientHeight) - 70;
    //alert(height);
    document.getElementById("divFlash").style.height = height + "px";
}

function linkar(linkEtapaAtual)
{
   window.location.href = linkEtapaAtual;
}
