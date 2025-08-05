<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormulario_Edicao.aspx.cs" Inherits="wfRenderizaFormulario_Edicao" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Formulario</title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>

    <script type="text/javascript">
        window.name="modal";
        var existeConteudoCampoAlterado = false;
    </script>

     <script type="text/javascript" language="javascript">
         var urlModal = "";
         var posExecutar = null;
        var popUp = 'S';
        var retornoModal = null;
        var retornoModalTexto = null;
        var retornoModalValor = null;
        var componenteLOV = null;
        var nomeGridSubFormulario = "";
        
        function onClickBotaoSalvar()
        {
            return false;
        }
        
        function showSubModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal)
        {      
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

        function resetaModal() {
            objFrmModal = document.getElementById('frmModal');
            posExecutar = null;
            objFrmModal.src = "";
            pcModal.SetHeaderText("");
            retornoModal = null;
            retornoModalTexto = null;
            retornoModalValor = null;
        }
        
        function fechaModal()
        {
            pcModal.Hide();
        }


        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, objParam) {
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;

            myObject = objParam;
            objFrmModal = document.getElementById('frmModal');

            pcModal.SetWidth(sWidth);
            objFrmModal.style.width = "100%";
            objFrmModal.style.height = sHeight + "px";
            urlModal = sUrl;
            pcModal.SetHeaderText(sHeaderTitulo);
            pcModal.Show();
        }

        function mostrarLov(s, e, codigoLista) {
            componenteLOV = s;
            var valor = s.GetValue();
            showModal("lovFormulario.aspx?CL=" + codigoLista + "&V=" + valor, "Lista de valores", 580, 200, null);
        }

        function atribuiResultadoLov() {
            componenteLOV.ClearItems();
            componenteLOV.AddItem(retornoModalTexto, retornoModalValor);
            componenteLOV.SetSelectedIndex(0);
        }
    </script>

</head>
<body style="margin: 0px">
    <form id="form1" runat="server" target="modal">
    <div>
        <dx:ASPxPopupControl ID="pcModal" runat="server" AllowDragging="True" AllowResize="True"
            ClientInstanceName="pcModal" CloseAction="CloseButton" Font-Names="Verdana" Font-Size="8pt"
            HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <iframe id="frmModal" frameborder="0" name="frmModal" style="overflow: hidden; padding: 0px;
                        margin: 0px;"></iframe>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents CloseUp="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
                atribuiResultadoLov();
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
                
            resetaModal();
}" PopUp="function(s, e) {
	document.getElementById('frmModal').src = urlModal;
}" />
        </dx:ASPxPopupControl>
    
    </div>
    <dx:ASPxHiddenField ID="hfSessao" runat="server" ClientInstanceName="hfSessao">
    </dx:ASPxHiddenField>
        <dx:ASPxPopupControl ID="popupCampo" runat="server" ClientInstanceName="popupCampo" CloseAction="CloseButton" Height="300px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="1000px" AllowDragging="True" HeaderText="Caracterização">
            <ClientSideEvents Closing="function(s, e) {
       var grid = eval(nomeGridSubFormulario);                
       grid.PerformCallback();
}" />
            <ContentCollection>
<dx:PopupControlContentControl runat="server"></dx:PopupControlContentControl>
</ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
