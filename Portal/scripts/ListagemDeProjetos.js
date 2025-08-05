// JScript File
var CodigoProjeto2   = "";
var CodigoMSProject2 = "";
var NomeProjeto2     = "";
var CodigoGerenteProjeto2    = "";
var CodigoUnidadeNegocio2    = "";
var SiglaUnidadeNegocio2     = "";
var NomeGerente2     = "";
        
function selecionaProjeto()
{
    //gvDados.SetFocusedRowIndex(-1);
    OnGridFocusedRowChanged();
    var myArgs = new Array(CodigoProjeto2, CodigoMSProject2, NomeProjeto2, CodigoGerenteProjeto2, CodigoUnidadeNegocio2, SiglaUnidadeNegocio2, NomeGerente2);

    window.top.retornoModal = myArgs;    
}


function OnGridFocusedRowChanged() 
{
    gvDados.SelectRowOnPage(gvDados.GetFocusedRowIndex(), true);
    gvDados.GetSelectedFieldValues('CodigoProjeto;CodigoMSProject;NomeProjeto;CodigoGerenteProjeto;CodigoUnidadeNegocio;SiglaUnidadeNegocio;NomeGerente;', MontaCamposFormulario);
 }
         
function MontaCamposFormulario(valores)
{
        var values = valores[0];
        CodigoProjeto2   = (values[0] != null ? values[0] : "");
        CodigoMSProject2 = (values[1] != null ? values[1] : "");
        NomeProjeto2     = (values[2] != null ? values[2] : "");
        CodigoGerenteProjeto2    = (values[3] != null ? values[3] : "");
        CodigoUnidadeNegocio2    = (values[4] != null ? values[4] : "");
        SiglaUnidadeNegocio2     = (values[5] != null ? values[5] : "");
        NomeGerente2     = (values[6] != null ? values[6] : "");
        
        if(CodigoProjeto2 == "")
            CodigoProjeto2 = "-1";
}