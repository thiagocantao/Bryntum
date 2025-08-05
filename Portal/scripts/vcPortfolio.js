function verificaVisao()
{
    var tipoVisao = ddlOpcaoVisao.GetValue();
	var tipoFiltro = rbTipoVisao.GetValue();
	var urlGraficos = "";
	var urlGantt = "";   

	if(tipoFiltro == "G")
	{
        urlGantt = '../_Public/Gantt/paginas/projetometa/Default.aspx';
            //'./VisaoCorporativa/vc_gantt.aspx';
		urlGraficos = './VisaoCorporativa/visaoCorporativa_01.aspx';
		lblFiltro.SetText("Portfólio:");
		ddlGeral.SetVisible(true);
		ddlUnidade.SetVisible(false);
		ddlCategoria.SetVisible(false);
	}
	else
	{
		if(tipoFiltro == "U")
		{
			urlGantt = './VisaoCategoria/vu_gantt.aspx';
			urlGraficos = './VisaoCategoria/visaoUnidade_01.aspx';
			lblFiltro.SetText("Unidade:");
			ddlGeral.SetVisible(false);
			ddlUnidade.SetVisible(true);
			ddlCategoria.SetVisible(false);
		}
		else
		{
			urlGantt = './VisaoCategoria/vcu_gantt.aspx';
			urlGraficos = './VisaoCategoria/visaoCategoria_01.aspx';
			lblFiltro.SetText("Categoria:");
			ddlGeral.SetVisible(false);
			ddlUnidade.SetVisible(false);
			ddlCategoria.SetVisible(true);
		}
	}

	if(tipoVisao == "0")
	{
		document.getElementById('frmVC').src = urlGraficos;	
        rbTipoVisao.SetVisible(true); 
	}
	else
	{
		if(tipoVisao == "1")
		{
			document.getElementById('frmVC').src = urlGantt;
            rbTipoVisao.SetVisible(true); 
		}
		else
		{
		    document.getElementById('frmVC').src = './VisaoMetas/visaoMetas_01.aspx';		    
            rbTipoVisao.SetVisible(false); 
            rbTipoVisao.SetValue("G");
            ddlGeral.SetVisible(true);
		    ddlUnidade.SetVisible(false);
		    ddlCategoria.SetVisible(false);
		    lblFiltro.SetText("Portfólio:");
		}
	}	
}

function clickLinkCategoria(valor)
{
    rbTipoVisao.SetValue("C");
    ddlCategoria.SetValue(valor);
    callBackVC.PerformCallback('AtualizarVC');
}