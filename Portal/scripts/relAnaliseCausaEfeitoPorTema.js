// JScript File

function End_CallbackTree(s, e)
{
    file = document.getElementById('tdExport');

    var podeExportar = hfGeral.Get('podeExportarTree');
    if(podeExportar == 'S')
    {
        if(s.rowCount == 0)
        {
   	        s.SetVisible(false);
            file.style.display = 'none';
            pLegenda.SetVisible(false);
        }
        else
        {
            s.SetVisible(true);
            pLegenda.SetVisible(true);
	        file.style.display = '';
        }
    }
}


