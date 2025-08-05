
function f_MontaWhere(valor,qtdWhere)  {
    var sWhere = MemoWhere.GetText();
    var sProcura = "@Role_" + qtdWhere
    var sCompara = txtFiltroAlteracao.GetText();
    var sClausula = " AND upper(OLD_DATA.value ('(/DADO_ANTIGO/" + ddlCampos.GetText() + ")[1]', 'varchar(255)'))  = upper("+sProcura+" )"
    if (valor != null && valor != '') {
        if (valor == 'E') {
            sWhere += sClausula; 
            MemoWhere.SetText( sWhere );
        }
    }
   return true
}