 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCadastroLicaoAprendida.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_FormCadastroLicaoAprendida" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tabela {
            width: 100%;
        }

        .style5 {
            width: 176px;
        }

        .style6 {
            height: 10px;
        }



        .capitalize {
            text-transform: capitalize !important;
        }
    </style>
    
    <script type="text/javascript" language="javascript">
        var existeConteudoCampoAlterado = false;
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function gravaInstanciaWf() {
            try {
                window.parent.executaCallbackWF();
            } catch (e) { }
        }

        function eventoPosSalvar(codigoInstancia) {
            try {
                var hfGeralWf = window.parent.parent.hfGeralWorkflow;
                hfGeralWf.Set('CodigoInstanciaWf', codigoInstancia);

                if (callbackSalvar.cp_gravouNovaIni == '1') {
                    if (hfGeralWf.Contains('CodigoInstanciaWf') && hfGeralWf.Contains('CodigoEtapaWf')) {
                        var paramsRenderiza = '&CI=' + hfGeralWf.Get('CodigoInstanciaWf') + '&CE=' + hfGeralWf.Get('CodigoEtapaWf') + '&CS=1' + '&CP=' + callbackSalvar.cpCodigoProjeto;
                        window.top.lpAguardeMasterPage.Show();
                        lpLoading.Hide();
                        window.parent.callbackReload.PerformCallback(paramsRenderiza);
                    }
                }
            } catch (e) {
            }
            lpLoading.Hide();
        }

        function VerificaCamposObrigatoriosPreenchidos() {
            var bRet = true
            if ((Trim(txtNomeProjeto.GetText()) == "") || (Trim(mmDescricao.GetText()) == "") || (null == rbFalhaBoaPratica.GetValue())
                || (null == rbMudancaAprendizagem.GetValue()) || (Trim(txtConhecimento.GetText()) == "")) {
                bRet = false;
            }
                            
            return bRet;
        }

        function ValidaCampos() {
            var msg = ""
            var numAux = 0;

            if (Trim(txtNomeProjeto.GetText()) == "") {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Identificação' deve ser informado.\n";
            }
            if (Trim(mmDescricao.GetText()) == "") {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Descrição' deve ser informado.\n";
            }
            if ( null == rbFalhaBoaPratica.GetValue() ) {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Falha ou Boa prática?' deve ser informado.\n";
            }
            if (null == rbMudancaAprendizagem.GetValue()) {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Gera mudança ou aprendizagem?' deve ser informado.\n";
            }
            if (Trim(txtConhecimento.GetText()) == "") {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Conhecimento' deve ser informado.\n";
            }
            return msg;
        }

        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
            if (hfGeral.Get("PodePassarFluxo") == "N") {
                window.top.mostraMensagem("Somente o usuário que inicializou o processo poderá passar para a próxima etapa do fluxo.", 'atencao', true, false, null);
                return false;
            }

            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem("As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.", 'atencao', true, false, null);
                return false;
            }
            var camposPreenchidos = VerificaCamposObrigatoriosPreenchidos();
            if (!camposPreenchidos) {
                var camposNaoPreenchidos = new Array();
                var cont = 0;

                if (Trim(txtNomeProjeto.GetText()) == "") {
                    camposNaoPreenchidos[cont] = "Identificação";
                    cont++;
                }
                if (Trim(mmDescricao.GetText()) == "") {
                    camposNaoPreenchidos[cont] = "Descrição";
                    cont++;
                }
                if (null == rbFalhaBoaPratica.GetValue()) {
                    camposNaoPreenchidos[cont] = "Falha ou Boa prática?";
                    cont++;
                }
                if (null == rbMudancaAprendizagem.GetValue()) {
                    camposNaoPreenchidos[cont] = "Gera mudança ou aprendizagem?";
                    cont++;
                }
                if (Trim(txtConhecimento.GetText()) == "") {
                    camposNaoPreenchidos[cont] = "Conhecimento";
                    cont++;
                }

                var quantidade = camposNaoPreenchidos.length;
                var nomesCampos = "";
                for (var i = 0; i < quantidade; i++) {
                    nomesCampos += "\n" + camposNaoPreenchidos[i];

                    if (i == (quantidade - 1))       //Se for o último concatena um '.' (ponto final).
                        nomesCampos += ".";
                    else if (i == (quantidade - 2))  //Se for o penúltimo contatena ' e'.
                        nomesCampos += " e";
                    else                            //Caso contrário concatena ',' (vírgula).
                        nomesCampos += ",";
                }

                window.top.mostraMensagem("Para prosseguir com o fluxo, é necessário informar os seguintes campos: " + nomesCampos, 'atencao', true, false, null);
                return false;
            }
            return true;
        }

        function recebeConhecimentoEscolhido(codigoConhecimento) {
            if ((codigoConhecimento != null) && (codigoConhecimento != "")) {
                callbackConhecimento.PerformCallback(codigoConhecimento);
            }
                
        }

        function ProcessaResultadoCallback(s, e) {
            var result = e.result;
            var strAuxiliar = result;
            var mensagemErro = "";
            var resultadoSplit = strAuxiliar.split("¥");
            if (resultadoSplit[1] != "") {
                mensagemErro = resultadoSplit[1];
            }

            if (mensagemErro != "") {
                window.top.mostraMensagem(mensagemErro, 'erro', true, false, null);
            }
            else {
                existeConteudoCampoAlterado = false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" sroll="yes">
        <div style="display: flex; flex-direction: column">
            <div id="dv01" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Identificação:"></dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px" ></td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" ClientInstanceName="txtNomeProjeto"
                                            Width="100%" MaxLength="255">
                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>*</td>
                                    <td style="padding-left: 10px">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer"
                                            Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Informar o título da lição aprendida">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Descrição:"></dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px" ></td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxcp:ASPxMemo ID="mmDescricao" runat="server" ClientInstanceName="mmDescricao" Height="71px" Width="100%" MaxLength="4000">
                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxcp:ASPxMemo>
                                    </td>
                                    <td>*</td>
                                    <td style="padding-left: 10px">
                                        <dxe:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer"
                                            Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Descrever sucintamente a lição aprendida">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Falha ou Boa prática?:"></dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px" ></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxcp:ASPxRadioButtonList ID="rbFalhaBoaPratica" runat="server" ClientInstanceName="rbFalhaBoaPratica" RepeatColumns="2" Width="300px">
                                            <Items>
                                                <dxtv:ListEditItem Text="Falha" Value="Falha" />
                                                <dxtv:ListEditItem Text="Boa prática" Value="Boa prática" />
                                            </Items>
                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                        </dxcp:ASPxRadioButtonList>
                                    </td>
                                    <td>*</td>
                                    <td style="padding-left: 10px">
                                        <dxe:ASPxImage ID="ASPxImage3" runat="server" Cursor="pointer"
                                            Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Indicar se a lição foi aprendida de uma falha ou de uma boa prática">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Gera Mudança ou Aprendizagem?:"></dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px" ></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxcp:ASPxRadioButtonList ID="rbMudancaAprendizagem" runat="server" ClientInstanceName="rbMudancaAprendizagem" RepeatColumns="3" Width="800px">
                                            <Items>
                                                <dxtv:ListEditItem Text="Gera somente mudança" Value="Gera somente mudança" />
                                                <dxtv:ListEditItem Text="Gera somente aprendizagem" Value="Gera somente aprendizagem" />
                                                <dxtv:ListEditItem Text="Gera mudança e aprendizagem" Value="Gera mudança e aprendizagem" />
                                            </Items>
                                            <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                        </dxcp:ASPxRadioButtonList>
                                    </td>
                                    <td>*</td>
                                    <td style="padding-left: 10px">
                                        <dxe:ASPxImage ID="ASPxImage4" runat="server" Cursor="pointer"
                                            Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Indicar se a lição aprendida gera mudança e/ou aprendizagem">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                Text="Conhecimento:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="display:flex;flex-direction:row;width:100%; align-items: center; justify-content: center;">
                                <div style="width:3%;cursor:pointer;margin-left:12px" title="Clique para escolher um conhecimento da árvore">
                                    <dxcp:ASPxImage ID="imgLinkParaConhecimento" ClientInstanceName="imgLinkParaConhecimento" runat="server" ShowLoadingImage="True" ImageUrl="~/imagens/anexo/pastaAberta.gif">
                                        <ClientSideEvents Click="function(s, e) { 
    var codigoAtual =  hfGeral.Get('CodigoItemConhecimento');
    if ( (null == codigoAtual) || ('' == codigoAtual)){
        codigoAtual = '-1';
    }
    var sUrl = '/../../_Projetos/Administracao/LicoesAprendidas/popupArvoresConhecimento.aspx?CS=' +  codigoAtual;
    window.top.showModal(sUrl, 'Conhecimento', null, null, recebeConhecimentoEscolhido, null);
}" />
                                    </dxcp:ASPxImage>
                                </div>
                                <div style="width:97%">
                                    <dxcp:ASPxCallbackPanel ID="callbackConhecimento" runat="server" ClientInstanceName="callbackConhecimento" OnCallback="callbackConhecimento_Callback" Width="100%">
                                        <ClientSideEvents BeginCallback="function(s, e) {
	lpLoading.Show();
}" EndCallback="function(s, e) {
	lpLoading.Hide();
}" />
                                        <PanelCollection>
<dxcp:PanelContent runat="server">
    <dxtv:ASPxTextBox ID="txtConhecimento" runat="server" ClientInstanceName="txtConhecimento" ReadOnly="True" Width="100%">
        <ReadOnlyStyle BackColor="#EBEBEB">
        </ReadOnlyStyle>
    </dxtv:ASPxTextBox>
                                            <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxtv:ASPxHiddenField>
                                            </dxcp:PanelContent>
</PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </div>
                                <div style="width:30px" >
                                </div>
                                <div style="width:30px" >
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Status:"></dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px" ></td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxTextBox ID="txtStatusLicao" runat="server" ClientInstanceName="txtStatusLicao"
                                            Width="100%" MaxLength="255" ReadOnly="true">
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="Tabela">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Projetos:"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxhe:ASPxHtmlEditor ID="htmlProjetosLicao" runat="server" ClientInstanceName="htmlProjetosLicao" Width="100%" Enabled="false" ClientEnabled="False" Height="130px" ToolbarMode="None" Settings-AllowContextMenu="False" Settings-AllowDesignView="False" Settings-AllowHtmlView="False" Settings-AllowPreview="False">
                                            <Settings AllowContextMenu="False">
                                            </Settings>
<SettingsHtmlEditing>
<PasteFiltering Attributes="class"></PasteFiltering>
</SettingsHtmlEditing>
                                        </dxhe:ASPxHtmlEditor>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="right">
                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                    Text="Salvar" AutoPostBack="False"
                    Width="100px" CssClass="capitalize">
                    <ClientSideEvents Click="function(s, e) {
    var msg = ValidaCampos();
    if (msg == '') {
        lpLoading.Show();
        callbackSalvar.PerformCallback('');
    }
    else {
        window.top.mostraMensagem(msg, 'atencao', true, false, null);
    }
}" />
                </dxe:ASPxButton>
            </div>


        </div>

        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
    if ( s.cp_gravouNovaIni == '1' ){
	    gravaInstanciaWf();
    }
    else{
        lpLoading.Hide();
    }
    ProcessaResultadoCallback(s, e);
}" />
        </dxcb:ASPxCallback>
        <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
        </dxcp:ASPxLoadingPanel>
    </form>
</body>
</html>
