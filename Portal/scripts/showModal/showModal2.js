var urlModal2 = "";
var cancelaFechamentoPopUp2 = 'N';
var retornoModal2 = null;
var posExecutar2 = null;

function showModal2(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal) {

    if (sWidth == null) {
        sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
    }
    if (sHeight == null) {
        sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
    }

    if (parseInt(sHeight) < 535)
        sHeight = parseInt(sHeight) + 20;
    sWidth = sWidth <= 400 ? 900 : sWidth;
    posExecutar2 = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

    pcModal2.SetWidth(sWidth);
    pcModal2.SetHeight(sHeight);
    pcModal2.SetContentUrl(sUrl);
    //setTimeout ('alteraUrlModal();', 0);            
    pcModal2.SetHeaderText(sHeaderTitulo);
    pcModal2.Show();

}
function fechaModal2() {
    //pcModal2.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal2.Hide();
}
function resetaModal2() {
    posExecutar2 = null;
    pcModal2.SetContentUrl(pcModal.cp_Path + "branco.htm");
    pcModal2.SetHeaderText("");
    retornoModal2 = null;
}