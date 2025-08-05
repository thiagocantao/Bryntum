 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormLicoesAprendidasNovoProjeto.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_FormLicoesAprendidasNovoProjeto" %>
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
        var comando_gvContexto;
        var comando_gvLicoesAprendidas;
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

                if (callbackSalvar.cp_gravarInstanciaFluxo == '1') {
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
            if (Trim(txtNomeProjeto.GetText()) == "") {
                bRet = false;
            }

            return bRet;
        }

        function ValidaCampos() {
            var msg = ""
            var numAux = 0;

            if (Trim(txtNomeProjeto.GetText()) == "") {
                numAux++;
                msg += "\n" + numAux + ") " + "O campo 'Título da Proposta de iniciativa' deve ser informado.\n";
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
                var possuiNomeProjeto = txtNomeProjeto.GetValue() != null;
                var camposNaoPreenchidos = new Array();
                var cont = 0;

                if (!possuiNomeProjeto) {
                    camposNaoPreenchidos[cont] = "Título da Proposta de iniciativa";
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
        function recebeLCAEscolhida(codigoLicaoAprendida) {
            if ((codigoLicaoAprendida != null) && (codigoLicaoAprendida != ""))
                existeConteudoCampoAlterado = true;
                gvLicoesAprendidas.PerformCallback(codigoLicaoAprendida);
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
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Título da Proposta de iniciativa:">
                                        </dxe:ASPxLabel>
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
                                            Height="18px" ImageUrl="~/imagens/ajuda.png" Width="18px" ToolTip="Nome da iniciativa no no sistema">
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
                                        <dxe:ASPxLabel ID="ASPxLabel39" runat="server" Text="Contexto:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="gvContexto" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvContexto" KeyFieldName="CodigoContexto" Width="100%" OnCellEditorInitialize="gvContexto_CellEditorInitialize" OnRowInserting="gvContexto_RowInserting" OnRowDeleting="gvContexto_RowDeleting" OnCommandButtonInitialize="gvContexto_CommandButtonInitialize">
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                    Width="50px" ShowEditButton="false" ShowDeleteButton="true">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxm:ASPxMenu ID="menu_gvContexto" runat="server" BackColor="Transparent"
                                                                        ClientInstanceName="menu_gvContexto" ItemSpacing="5px" OnInit="menu_gvContexto_Init">
                                                                        <Paddings Padding="0px" />
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                <Items>
                                                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                        ClientVisible="False">
                                                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                        <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                </Items>
                                                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                <Items>
                                                                                    <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                        <Image IconID="save_save_16x16">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                        <Image IconID="actions_reset_16x16">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                </Items>
                                                                                <Image Url="~/imagens/botoes/layout.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                        </Items>
                                                                        <ItemStyle Cursor="pointer">
                                                                            <HoverStyle>
                                                                                <border borderstyle="None" />
                                                                            </HoverStyle>
                                                                            <Paddings Padding="0px" />
                                                                        </ItemStyle>
                                                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                            <SelectedStyle>
                                                                                <border borderstyle="None" />
                                                                            </SelectedStyle>
                                                                        </SubMenuItemStyle>
                                                                        <Border BorderStyle="None" />
                                                                    </dxm:ASPxMenu>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxtv:GridViewDataComboBoxColumn Caption="Descricao" FieldName="DescricaoContexto" VisibleIndex="1">
                                                    <PropertiesComboBox MaxLength="100" TextField="DescricaoContexto" ValueField="CodigoContexto">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Informe um valor válido para o campo." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataComboBoxColumn>
                                            </Columns>
                                            <Settings VerticalScrollableHeight="150" VerticalScrollBarMode="Visible" />
                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                            <ClientSideEvents BeginCallback="function(s, e) {
              comando_gvContexto = e.command;
            }"
                                                EndCallback="function(s, e) {
                //alert(comando_gvContexto);
                if(comando_gvContexto == 'UPDATEEDIT') {
	                if (s.cpRecarregarTela == '1') {
                        var queryStr = &quot;prjTemp=S&amp;&quot; + s.cpQueryString;
                        window.location = &quot;./FormLicoesAprendidasNovoProjeto.aspx?&quot; + queryStr;
                    }
                }
                if(comando_gvContexto == 'DELETEROW' &amp;&amp;  s.cpTextoMsg != '') {
                             window.top.mostraMensagem(s.cpTextoMsg, s.cpNomeImagem, (s.cpMostraBtnOK == 'true'), (s.cpMostraBtnCancelar== 'true'), null, s.cpTimeout);
                }
            }" />
                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                    AllowResize="True" Width="600px" />

                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                        </dxwgv:ASPxGridView>
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
                                        <dxe:ASPxLabel ID="ASPxLabel35" runat="server"
                                            Text="Lições Aprendidas">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width:10px"></td>
                                    <td style="width:30px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="gvLicoesAprendidas" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvLicoesAprendidas" KeyFieldName="CodigoLicaoAprendida" Width="100%" OnCommandButtonInitialize="gvLicoesAprendidas_CommandButtonInitialize" OnRowDeleting="gvLicoesAprendidas_RowDeleting" OnCustomCallback="gvLicoesAprendidas_CustomCallback">
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                    Width="50px" ShowDeleteButton="true">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxm:ASPxMenu ID="menu_gvLicoesAprendidas" runat="server" BackColor="Transparent"
                                                                        ClientInstanceName="menu_gvLicoesAprendidas" ItemSpacing="5px"
                                                                        OnInit="menu_gvLicoesAprendidas_Init">
                                                                        <Paddings Padding="0px" />
                                                                        <Items>
                                                                            <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                <Items>
                                                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                        ClientVisible="False">
                                                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                        <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                </Items>
                                                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                            <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                <Items>
                                                                                    <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                        <Image IconID="save_save_16x16">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                    <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                        <Image IconID="actions_reset_16x16">
                                                                                        </Image>
                                                                                    </dxm:MenuItem>
                                                                                </Items>
                                                                                <Image Url="~/imagens/botoes/layout.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                        </Items>
                                                                        <ItemStyle Cursor="pointer">
                                                                            <HoverStyle>
                                                                                <border borderstyle="None" />
                                                                            </HoverStyle>
                                                                            <Paddings Padding="0px" />
                                                                        </ItemStyle>
                                                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                            <SelectedStyle>
                                                                                <border borderstyle="None" />
                                                                            </SelectedStyle>
                                                                        </SubMenuItemStyle>
                                                                        <Border BorderStyle="None" />
                                                                    </dxm:ASPxMenu>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>

                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Descrição" ShowInCustomizationForm="True"
                                                    VisibleIndex="1" FieldName="NomeLicaoAprendida">
                                                    <PropertiesTextEdit MaxLength="100">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Informe um valor válido para o campo." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                    <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption=" " FieldName="IndicaProjetoOrigem" VisibleIndex="2" Width="240px">
                                                    <PropertiesTextEdit EnableFocusedStyle="False">
                                                    </PropertiesTextEdit>
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings VerticalScrollableHeight="150" VerticalScrollBarMode="Visible" />
                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                            <ClientSideEvents BeginCallback="function(s, e) {
	            comando_gvLicoesAprendidas = e.command;
            }"
                                                EndCallback="function(s, e) {
                //alert(comando_gvLicoesAprendidas);
                if(comando_gvLicoesAprendidas == 'UPDATEEDIT' || comando_gvLicoesAprendidas == 'CUSTOMCALLBACK') {
	                if (s.cpRecarregarTela == '1'){
                        var queryStr = &quot;prjTemp=S&amp;&quot; + s.cpQueryString;
                        window.location = &quot;./FormLicoesAprendidasNovoProjeto.aspx?&quot; + queryStr;
                    }
                }
        
                if(comando_gvLicoesAprendidas == 'DELETEROW' &amp;&amp;  s.cpTextoMsg != '') {
                    window.top.mostraMensagem(s.cpTextoMsg, s.cpNomeImagem, (s.cpMostraBtnOK == 'true'), (s.cpMostraBtnCancelar== 'true'), null, s.cpTimeout);
                }
            }" />
                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                    AllowResize="True" Width="600px" />

                                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral"></dxtv:ASPxHiddenField>
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
    if ( s.cp_gravarInstanciaFluxo == '1' ){
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
