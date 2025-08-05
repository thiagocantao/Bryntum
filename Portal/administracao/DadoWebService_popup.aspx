<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DadoWebService_popup.aspx.cs" Inherits="administracao_DadoWebService_popup" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Ítens</title>
    <script type="text/javascript">
        function redimensiona() {
            var sHeight = Math.max(0, document.documentElement.clientHeight) - 40;
            document.getElementById('divContainer').style.height = sHeight;
        }
    </script>
</head>
<body style="margin: 0; height: 2000px" onload="redimensiona()">
    <form id="form1" runat="server">
        <dxcp:ASPxCallback ID="callbackDadosSugestao" runat="server" ClientInstanceName="callbackDadosSugestao" OnCallback="callbackDadosSugestao_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
         memoParametrosWebServices.SetText(s.cp_parametrosWebService);
         memoProjecaoDados.SetText(s.cp_comandoSelectDadosWs);

document.getElementById('div_lblParametrosWebServices').style.display = 'block';
document.getElementById('div_memoParametrosWebServices').style.display = 'block';
document.getElementById('div_lblInstrucaoProjecaoDados').style.display = 'block';
document.getElementById('div_memoProjecaoDados').style.display = 'block';

document.getElementById('divBotoes').setAttribute('style', 'display: flex; flex-direction: row-reverse');
                btnCancelar_Aplicar.SetEnabled(false);
                btnAplicar.SetEnabled(false);
                btnAlterar.SetVisible(true);
                btnAlterar.SetEnabled(true);
var msg = 'Antes de salvar, o conteúdo dos campos &quot;Parâmetros web service&quot; e \n';
msg += '&quot;Instrução de projeção dos dados&quot; devem ser alterados para&nbsp; garantir que as\n';
msg += 'informações do web service estejam disponíveis para a criação de relatórios, dashboards, etc';

ddlConjuntoDadosAssociados.SetEnabled(false);
window.top.mostraMensagem(msg, 'atencao', true, false, null, null);
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxCallback ID="callbackGravaConfiguracaosDadoWebService" runat="server" ClientInstanceName="callbackGravaConfiguracaosDadoWebService" OnCallback="callbackGravaConfiguracaosDadoWebService_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
        if(s.cpErro !== '')
   {
      window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
   } 
else if(s.cpSucesso !== '')
{
    window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3500);
   window.top.fechaModal();
}
s.cpSucesso = '';
s.Erro = '';
}" />
        </dxcp:ASPxCallback>
        <div style="display: flex; flex-direction: column;width:100%">
            <div style="display: flex; flex-direction: column; overflow: auto" id="divContainer">
                <div>
                    <dxe:ASPxLabel runat="server" Text="Título" ID="lblTitulo" ClientInstanceName="lblTitulo">
                    </dxe:ASPxLabel>
                </div>
                <div style="width: 80%">
                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtTitulo" ID="txtTitulo">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </div>
                <div>
                    <dxe:ASPxLabel runat="server" ID="lblDescricao" ClientInstanceName="lblDescricao">
                    </dxe:ASPxLabel>
                </div>
                <div style="width: 80%">
                    <dxcp:ASPxMemo ID="memoDescricao" ClientInstanceName="memoDescricao" runat="server" Height="100px" Width="100%" MaxLength="500">
                    </dxcp:ASPxMemo>
                </div>
                <div>
                    <div style="display: flex; flex-direction: row">
                        <table style="width:80%">
                            <tr>
                                <td style="width:120px">
                                    <div style="display: flex; flex-direction: column">
                                        <div style="padding: 3px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Conjunto de dados associado:" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxcp:ASPxImage ID="imgAjuda" ClientInstanceName="imgAjuda" Cursor="pointer" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/ajuda.png" ToolTip="O webservice será acionado para cada item do conjunto de dados escolhidos.">
                                                        </dxcp:ASPxImage>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="padding: 3px">
                                            <dxcp:ASPxComboBox ID="ddlConjuntoDadosAssociados" ClientInstanceName="ddlConjuntoDadosAssociados" runat="server" Width="200px">
                                            </dxcp:ASPxComboBox>
                                        </div>
                                    </div>
                                </td>
                                <td style="width:120px">
                                    <div style="margin-top: 23px">
                                        <dxcp:ASPxButton runat="server" ClientInstanceName="btnAlterar" Text="Alterar" ValidationGroup="MKE" Width="100px" ID="btnAlterar" EnableClientSideAPI="True">
                                            <ClientSideEvents Click="function(s, e) {
       e.processOnServer = false;
   var funcObj = {
            funcaoClickOK: function () {
                        //alert('clicou em ok');
                        //2.1.1. Habilitar o combobox, e desabilitar o botão;
                       ddlConjuntoDadosAssociados.SetEnabled(true);
               
                        //2..1.2. Ocultar a seção da tela onde estão o campo de select e os botões Salvar e Confirmar;
                       document.getElementById('div_lblParametrosWebServices').style.display = 'none';
                       document.getElementById('div_memoParametrosWebServices').style.display = 'none';            
                       document.getElementById('div_lblInstrucaoProjecaoDados').style.display= 'none';
                       document.getElementById('div_memoProjecaoDados').style.display= 'none';

                       //2.1.3. Habilitar o botões &quot;Aplicar&quot; e o botão &quot;Cancelar&quot; ao seu lado.&nbsp; &nbsp;
                       document.getElementById('view_divBotoesAplicarECancelar').style.display='block';
                       btnCancelar_Aplicar.SetEnabled(true);
                       btnAplicar.SetEnabled(true);
              }
        }
   var funcObj1 = {
            funcaoClickCancelar: function () {
                //alert('clicou em cancelar');
            }
        }
        window.top.mostraConfirmacao(traducao.DadoWebService_popup_alterando_o_conjunto_de_dados_da_integra__o__o_comando_select_ser__reiniciado__alterar_assim_mesmo_, function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });
}"></ClientSideEvents>
                                        </dxcp:ASPxButton>
                                    </div>
                                </td>
                                <td style="width:fit-content;float:right">
                                    <div style="display: flex; flex-direction: column;">
                                        <div style="padding: 3px">
                                            <table id="tabelaSiglaIdentificacao">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" ID="lblSiglaIdentificacao">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxcp:ASPxImage ID="imgAjuda2" ClientInstanceName="imgAjuda2" Cursor="pointer" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/ajuda.png" ToolTip="A sigla determinará o nome da view a ser criada para os dados dessa integração">
                                                        </dxcp:ASPxImage>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="padding: 3px">
                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="44" ClientInstanceName="txtSiglaIdentificacao" ID="txtSiglaIdentificacao">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="view_divBotoesAplicarECancelar" style="display: block" runat="server">
                    <div id="divBotoesAplicarECancelar" style="display: flex; flex-direction: row-reverse">
                        <div style="padding: 5px">
                            <dxcp:ASPxButton runat="server" ClientInstanceName="btnCancelar_Aplicar" Text="Cancelar" ValidationGroup="MKE" Width="100px" ID="btnCancelar_Aplicar" EnableClientSideAPI="True" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
   e.processOnServer = false;
   var funcObj = {
            funcaoClickOK: function () {
                                         window.top.fechaModal();
              }
        }
   var funcObj1 = {
            funcaoClickCancelar: function () {
                //alert('clicou em cancelar');
            }
        }
        window.top.mostraConfirmacao('Confirma o fechamento da tela? (as alterações, se houver, serão perdidas)', function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });
}"></ClientSideEvents>
                            </dxcp:ASPxButton>
                        </div>
                        <div style="padding: 5px">
                            <dxcp:ASPxButton runat="server" ClientInstanceName="btnAplicar" Text="Aplicar" ValidationGroup="MKE" Width="100px" ID="btnAplicar" EnableClientSideAPI="True">
                                <ClientSideEvents Click="function(s, e) {
     e.processOnServer = false;
     var  mensagemErro_ValidaCamposFormulario = &quot;&quot;;
     var countMsg = 1;
   
     if (&quot;&quot; == txtTitulo.GetText()) 
    {
           mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Título deve ser informado' + &quot;\n&quot;;
    }
    if (&quot;&quot; == memoDescricao.GetText()) 
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Descrição deve ser informada' + &quot;\n&quot;;
    }
    if ( ddlConjuntoDadosAssociados.GetValue() == null) 
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Conjunto de dados deve ser informado!' + &quot;\n&quot;;
    }
    if ( txtSiglaIdentificacao.GetText() == '') 
   {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Sigla deve ser informada!' + &quot;\n&quot;;
    }
    if( mensagemErro_ValidaCamposFormulario.length  == 0)
    {
            callbackDadosSugestao.PerformCallback();
    }
    else
    {  
             window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null, null);
    }
}"></ClientSideEvents>
                            </dxcp:ASPxButton>
                        </div>
                    </div>
                </div>
                <div id="div_lblParametrosWebServices" runat="server">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" ID="lblParametrosWebServices" ClientInstanceName="lblParametrosWebServices">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxcp:ASPxImage ID="imgAjuda3" ClientInstanceName="imgAjuda3" Cursor="pointer" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/ajuda.png" ToolTip="O webservice será acionado com base nas informações deste campo.">
                                </dxcp:ASPxImage>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div_memoParametrosWebServices" runat="server">
                    <dxcp:ASPxMemo ID="memoParametrosWebServices" ClientInstanceName="memoParametrosWebServices" runat="server" Height="200px" Width="100%">
                    </dxcp:ASPxMemo>
                </div>
                <div id="div_lblInstrucaoProjecaoDados" runat="server">


                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Instrução de projeção de dados" ID="lblInstrucaoProjecaoDados">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxcp:ASPxImage ID="imgAjuda4" ClientInstanceName="imgAjuda4" Cursor="pointer" runat="server" ShowLoadingImage="true" ImageUrl="~/imagens/ajuda.png" ToolTip="Instrução SELECT da VIEW que projetará os dados desse webservice.">
                                </dxcp:ASPxImage>
                            </td>
                        </tr>
                    </table>







                </div>
                <div id="div_memoProjecaoDados" runat="server">
                    <dxcp:ASPxMemo ID="memoProjecaoDados" ClientInstanceName="memoProjecaoDados" runat="server" Height="200px" Width="100%">
                    </dxcp:ASPxMemo>
                </div>
            </div>
            <div id="divBotoes" style="display: flex; flex-direction: row-reverse" runat="server">
                <div style="padding: 5px">
                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnCancelar_Salvar" Text="Cancelar" ValidationGroup="MKE" Width="100px" ID="btnCancelar_Salvar" EnableClientSideAPI="True" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) {
                               e.processOnServer = false;
   var funcObj = {
            funcaoClickOK: function () {
                                         window.top.fechaModal();
              }
        }
   var funcObj1 = {
            funcaoClickCancelar: function () {
                //alert('clicou em cancelar');
            }
        }
        window.top.mostraConfirmacao('Confirma o fechamento da tela? (as alterações, se houver, serão perdidas)', function () { funcObj['funcaoClickOK']() }, function () { funcObj1['funcaoClickCancelar']() });

}"></ClientSideEvents>
                    </dxcp:ASPxButton>
                </div>
                <div style="padding: 5px">
                    <dxcp:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="100px" ID="btnSalvar" EnableClientSideAPI="True">
                        <ClientSideEvents Click="function(s, e) {
                            e.processOnServer = false;
     var  mensagemErro_ValidaCamposFormulario = &quot;&quot;;
     var countMsg = 1;
   
     if (&quot;&quot; == txtTitulo.GetText()) 
    {
           mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Título deve ser informado' + &quot;\n&quot;;
    }
    if (&quot;&quot; == memoDescricao.GetText()) 
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Descrição deve ser informada' + &quot;\n&quot;;
    }
    if ( ddlConjuntoDadosAssociados.GetValue() == null) 
    {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Conjunto de dados deve ser informado!' + &quot;\n&quot;;
    }
    if ( txtSiglaIdentificacao.GetText() == '') 
   {
        mensagemErro_ValidaCamposFormulario += countMsg++ + &quot;) &quot; + 'Sigla deve ser informada!' + &quot;\n&quot;;
    }
    if( mensagemErro_ValidaCamposFormulario.length  == 0)
    {
            callbackGravaConfiguracaosDadoWebService.PerformCallback();
    }
    else
    {  
             window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null, null);
    }
                            }"></ClientSideEvents>
                    </dxcp:ASPxButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
