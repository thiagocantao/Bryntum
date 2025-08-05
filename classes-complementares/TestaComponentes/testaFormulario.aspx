<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testaFormulario.aspx.cs"
    Inherits="testaFormulario" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;
        var retornoModal = null;
        var retornoModalTexto = null;
        var retornoModalValor = null;
        var cancelaFechamentoPopUp = 'N';
        var componenteLOV = null;

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

        function fechaModal() {
            pcModal.Hide();
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

        function calcula() {
            var formula = '([txt1]+[txt2])/([txt3]-[txt2])+1';
            var formulaComValores = formula;
            while (formula.indexOf('[') >= 0) {
                inicio = formula.indexOf('[')+1;
                termino = formula.indexOf(']');
                elemento = formula.substr(inicio, termino - inicio);
                var objElemento = eval(elemento);
                formulaComValores = replaceAll(formulaComValores, '[' + elemento + ']', objElemento.GetText());
                formula = formula.substr(termino + 1);
            }
            resposta = calculate(formulaComValores);
            alert(formulaComValores + " ==> " + resposta);
        }

        function replaceAll(origem, antigo, novo) {
            return origem.split(antigo).join(novo);
        }

        function calculate(equation) {
            var answer = 'erro';
            try {
                answer = equation != '' ? eval(equation) : '0';
            }
            catch (e) {
            }
            return answer;
        }

        function mostrarLov(s, e, codigoLista) {
            componenteLOV = s;
            var valor = s.GetValue();
            showModal("lovFormulario.aspx?CL=" + codigoLista + "&V=" + valor, "L.O.V.", 580, 200, null);
        }

        function atribuiResultadoLov() {
            componenteLOV.ClearItems();
            componenteLOV.AddItem(retornoModalTexto, retornoModalValor);
            componenteLOV.SetSelectedIndex(0);
        }

       

        function Button2_onclick() {
            alert('sss');
            var forms = document.forms;
            for (var i = 0; i < forms.length; i++) {
                for (var j = 0; j < forms[i].elements.length; j++) {
                    var elemento = forms[i].elements[j];
                    //aqui vc testa o elemento
                    debugger
                }
            }  
        }

    </script>
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ClientInstanceName="txt1" 
        Text="5" Width="170px">
    </dx:ASPxTextBox>
    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" ClientInstanceName="txt2" 
        Text="7" Width="170px">
    </dx:ASPxTextBox>
    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" ClientInstanceName="txt3" 
        Text="10" Width="170px">
    </dx:ASPxTextBox>
    <dx:ASPxButton ID="ASPxButton1" runat="server" EnableTheming="True" OnClick="ASPxButton1_Click" RenderMode="Link">
        <ClientSideEvents Click="function(s, e) {
	calcula();
}" />
        <Image Url="~/imagens/botoes/btnExcel.png">
        </Image>
    </dx:ASPxButton>
    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" ClientInstanceName="txtR" 
        Width="170px">
        <HelpTextSettings DisplayMode="Popup">
        </HelpTextSettings>
    </dx:ASPxTextBox>
    <br />
    <br />
  <dx:ASPxPopupControl ID="pcModal" runat="server" ClientInstanceName="pcModal" Font-Names="Verdana"
            Font-Size="8pt" HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0" style="overflow:auto; padding:0px; margin:0px;"></iframe></dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents 
                PopUp="function(s, e) {
                            window.document.getElementById('frmModal').dialogArguments = myObject;
	                        document.getElementById('frmModal').src = urlModal;
                        }" 

                Closing="function(s, e) {
                            if(retornoModal != null)
                                atribuiResultadoLov();
                             resetaModal();
                        }" />

            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
        <ClientSideEvents CustomButtonClick="function(s, e) {
	Alert('teste');
}" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxButtonEdit ID="txtDotacao" runat="server" 
        ClientInstanceName="txtDotacao">
        <ClientSideEvents ButtonClick="function(s, e) {
	mostrarLov(s, e);
}" />
        <Buttons>
            <dx:EditButton>
            </dx:EditButton>
        </Buttons>
    </dx:ASPxButtonEdit>
    <dx:ASPxComboBox ID="cmbDotacao" runat="server" 
        ClientInstanceName="cmbDotacao" DropDownRows="10">
        <DropDownButton Visible="False">
        </DropDownButton>
    <Buttons>
                                    <dx:EditButton Text="..." >
                                    </dx:EditButton>
                                </Buttons>
        <ClientSideEvents ButtonClick="function(s, e) {
	mostrarLov(s, e, 176);
}"/>
    </dx:ASPxComboBox>
    <dx:ASPxButton ID="ASPxButton2" runat="server" onclick="ASPxButton2_Click" 
        Text="ASPxButton">
    </dx:ASPxButton>
    <dx:ASPxHiddenField ID="hfSessao" runat="server" ClientInstanceName="hfSessao">
    </dx:ASPxHiddenField>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        </dx:ASPxPopupControl>
    <input id="Button2" type="button" value="button" onclick="return Button2_onclick()" /></form>
    <dx:ASPxLoadingPanel ID="lpAguardeMasterPage" runat="server" ClientInstanceName="lpAguardeMasterPage"
        Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Height="80px" HorizontalAlign="Center"
        Modal="True" Text="Aguarde&amp;hellip;" VerticalAlign="Middle" Width="200px">
    </dx:ASPxLoadingPanel>
</body>
</html>
