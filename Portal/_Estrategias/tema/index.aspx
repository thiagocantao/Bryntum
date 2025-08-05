<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="index.aspx.cs"
    Inherits="_Estrategias_wizard_index" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var telaInicialOE = "";
// ]]>
    </script>
    <div id="pai" class="pai" style="display: flex; overflow-y: visible !important; height: inherit; position: sticky;">
        <div id="filho-esquerda" class="filho-esquerda" style="border-right : 2px solid #f1f1f1; border-bottom : 2px solid #f1f1f1; border-radius: 0px 0px 15px 0px; overflow-y : visible;">
            <%-- Conteúdo Menú --%>
            <iframe id="oe_menu" src="opcoes.aspx?CM=<%=codigoMapa %>&CT=<%=codigoObjetivoEstrategico %>&UN=<%=codigoUnidadeSelecionada %>&UNM=<%=codigoUnidadeMapa %>" width="100%" height="<%=alturaTabela %>" scrolling="no" frameborder="0" name="oe_menu"></iframe>
            <%-- Fim Conteúdo Menú --%>
        </div>
        <div class="filho-direita" style="flex: 1;">
            <%-- Conteúdo Pagina --%>
            <iframe id="oe_desktop" width="100%"
                height="<%=alturaTabela %>" scrolling="no" frameborder="0" name="oe_desktop"></iframe>
            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%;">
                <tr style="height: 26px">
                    <td valign="middle">&nbsp; &nbsp;<dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo"
                        Font-Bold="True" Text="Detalhes">
                    </dxe:ASPxLabel>
                    </td>
                    <td align="right" style="width: 150px" valign="middle">
                        <dxe:ASPxLabel ID="lblEntidade" runat="server"
                            Text="Entidade:" ClientInstanceName="lblEntidade">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" style="width: 120px">
                        <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                            Width="100%" ValueType="System.Int32" TextFormatString="{0}" IncrementalFilteringMode="Contains">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	document.getElementById('oe_menu').src='opcoes.aspx?CT=' + s.cp_COE + '&amp;UN=' + s.GetValue() + '&amp;UNM=' + s.cp_UNM;
	
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*Voc&#234; est&#225; Visualizando as Informa&#231;&#245;es da Entidade: ' + s.GetText());
}"
                                Init="function(s, e) {
	if(s.GetValue() == s.cp_UNL)
		lblEntidadeDiferente.SetText('');
	else
		lblEntidadeDiferente.SetText('*Voc&#234; est&#225; Visualizando as Informa&#231;&#245;es da Entidade: ' + s.GetText());
}"></ClientSideEvents>
                            <Columns>
                                <dxe:ListBoxColumn Name="siglaUnidade" Width="90px" Caption="Sigla Unidade"></dxe:ListBoxColumn>
                                <dxe:ListBoxColumn Name="nomeUnidade" Width="350px" Caption="Nome Unidade"></dxe:ListBoxColumn>
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <%-- Fim Conteúdo Pagina --%>
        </div>
    </div>
    <%--Seta a tela novaCdis.master como scroll para qualquer conteúdo feito na página.--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            AjustaTamanhoFrameNovaCDIS();
        });

        function AjustaTamanhoFrameNovaCDIS() {
            window.top.document.getElementById("telaPrincipal").style.height = 'inherit';
            window.top.document.getElementById("telaPrincipal").style.overflowY = 'visible';

            //Pega tamanho da body pai (tamanho total);
            //alert(window.top.document.getElementById("telaPrincipal").offsetHeight);

            //Pega a localização em altura da pai em relação da body Mestre;
            var p = $("#pai");
            var offset = p.offset();
            //alert(offset.top);

            //Seta tamanho da body menos(-) seu topo fazendo o cálculo total do restante para o frame das páginas dinâmicas o tamanho é setado apenas para o menú, caso a página cresça é setado Scroll automaticamente.
            var tamanhoFrame = 'height:' + ($("body").innerHeight() - offset.top - 5) + 'px; border-right : 2px solid #f1f1f1; border-bottom : 2px solid #f1f1f1; border-radius: 0px 0px 15px 0px;';
            $('#filho-esquerda').attr({ 'style': tamanhoFrame });

           window.onresize = function (event) {
                AjustaTamanhoFrameNovaCDIS();
            };
        }


        $('#telaPrincipal').on('scroll', function (e) {
            //aparece segundo menu
            if($(this).scrollTop() >= $("#filho-esquerda").innerHeight() - 70){
                //alert('adicioanaMenú');
                $("#filho-esquerda").hide(150)
            }
            else if($(this).scrollTop() < $("#filho-esquerda").innerHeight() - 70){
                //alert('voltarMenú');
                $("#filho-esquerda").show();
                
            }
        });

    </script>



</asp:Content>







