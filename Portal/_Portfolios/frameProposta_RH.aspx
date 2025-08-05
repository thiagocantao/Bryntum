<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_RH.aspx.cs" Inherits="_Portfolios_frameProposta_RH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() 
        {
            existeConteudoCampoAlterado = true;
        }
    
        function abreGrafico(codigoRH)
        {
            window.top.showModal3(window.top.pcModal.cp_Path + '_Portfolios/frameProposta_GraficoRH.aspx?CRH=' + codigoRH, "Disponibilidade dos Recursos", 870, 420, "", null);
        }

        var myObject = null;
        var posExecutar = null;
        var urlModal = "";
        var retornoModal = null;

        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
        

            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;

            myObject = objParam;
            posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            //setTimeout ('alteraUrlModal();', 0);            
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();


        }

        function fechaModal() {
            pcModal.Hide();
        }

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
        }
    </script>
    <style>
        .btn-Upp{
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblLegenda" runat="server" Font-Italic="True"
                        Text="Valores em Horas">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                        KeyFieldName="CodigoRecurso" Width="100%" OnRowUpdating="grid_RowUpdating" OnHtmlRowCreated="grid_HtmlRowCreated">
                        <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
s.SetHeight(sHeight);
}" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" 
                            VerticalScrollableHeight="390" />
                        <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px;">
                </td>
                <td style="padding-top: 5px;" align="right">
                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" 
                        Width="90px"  ID="btnFechar" CssClass="btn-Upp" ClientVisible="False">
<ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>





        <dxpc:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal"
            HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow:auto; padding:0px; margin:0px;"></iframe></dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents CloseUp="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
                
            resetaModal();
}" PopUp="function(s, e) {
    window.document.getElementById('frmModal').dialogArguments = myObject;
	document.getElementById('frmModal').src = urlModal;
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>


                </td>
                <td style="width: 10px; height: 5px;">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
