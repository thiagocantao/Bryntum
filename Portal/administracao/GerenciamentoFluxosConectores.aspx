<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GerenciamentoFluxosConectores.aspx.cs" Inherits="administracao_GerenciamentoFluxosEtapas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var elEditor;	// declaración necesaria para el funcionamiento de la librería editor.js
        var fazerPost = true;

        function tratarSimbolos(s, e) {
            if (e.htmlEvent.keyCode == 39 || e.htmlEvent.keyCode == 192) {
                e.htmlEvent.keyCode = 180;
                return e.htmlEvent.keyCode;
            }
        }
       
        var visibleIndex = 0;
        var indexNovo = 0;

        function onStartEdit(s, e) {
            visibleIndex = e.visibleIndex;
            e.cancel = true;
            pcDados.Show();
        }
        function onShown(s, e) {
            //CodigoPerfilWf
            var codigoPerfil = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, "idGrupoNotificado");
            var where = "";
            ddlPerfil.SetEnabled(codigoPerfil == null);

            if (codigoPerfil != null)
                where = " AND CodigoPerfilWf = " + codigoPerfil;
            else
            {
                var i = 0;
                indexNovo = visibleIndex;
                where = " AND CodigoPerfilWf NOT IN(-1"
                var indices = gv_PessoasAcessos_etp.batchEditHelper.GetDataItemVisibleIndices();

                for (i = 0; i < indices.length; i++) {
                    var rowKey = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoNotificado");
                    if (indices[i] < 0 || !gv_PessoasAcessos_etp.batchEditHelper.IsDeletedItem(rowKey)) {
                        if (gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoNotificado") != null)
                            where += ("," + gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoNotificado"));
                    }
                }

                where += ")";
            }

            ddlPerfil.PerformCallback(where);

        }
        function onAcceptClick(s, e) {
            gv_PessoasAcessos_etp.batchEditApi.SetCellValue(visibleIndex, "idGrupoNotificado", ddlPerfil.GetValue());
            gv_PessoasAcessos_etp.batchEditApi.SetCellValue(visibleIndex, "tipoNotificacao", ddlTipo.GetValue());
            pcDados.Hide();
        }
        function onCloseButtonClick(s, e) {
            if (visibleIndex <= -1 && gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, "idGrupoNotificado") == null)
                gv_PessoasAcessos_etp.batchEditApi.ResetChanges(visibleIndex);

            pcDados.Hide();
        }

        function novaLinha() {
            gv_PessoasAcessos_etp.AddNewRow();
        }

        var possuiGrupoAcao = false;
        var possuiGrupoAcompanhamento = false;

        function verificaGrupos()
        {
            var indices = gv_PessoasAcessos_etp.batchEditHelper.GetDataItemVisibleIndices();

            for (i = 0; i < indices.length; i++) {
                var rowKey = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoNotificado");
                if (indices[i] < 0 || !gv_PessoasAcessos_etp.batchEditHelper.IsDeletedItem(rowKey)) {
                    var registro = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "tipoNotificacao");
                    if (registro != null) {
                        if (registro == "Ação" || registro == 'E')
                            possuiGrupoAcao = true;
                        else if (registro == "Acompanhamento" || registro == 'S')
                            possuiGrupoAcompanhamento = true;
                    }
                }
            }
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";
            possuiGrupoAcao = false;
            possuiGrupoAcompanhamento = false;

            verificaGrupos();

            if (txtAssunto1_cnt.GetText() == '' && possuiGrupoAcao) {
                numAux++;
                mensagem += "\n" + numAux + ") O assunto da mensagem de 'Ação' é obrigatório quando existe um perfil a ser notificado.";
            }

            if (mmTexto1_cnt.GetText() == '' && possuiGrupoAcao) {
                numAux++;
                mensagem += "\n" + numAux + ") O texto da mensagem de 'Ação' é obrigatório quando existe um perfil a ser notificado.";
            }

            if ((txtAssunto1_cnt.GetText() != '' || mmTexto1_cnt.GetText() != '') && !possuiGrupoAcao) {
                numAux++;
                mensagem += "\n" + numAux + ") A escolha de um perfil a ser notificado é obrigatório quando o assunto e o texto da mensagem de 'Ação' estiverem preenchidos.";
            }

            if (txtAssunto2_cnt.GetText() == '' && possuiGrupoAcompanhamento) {
                numAux++;
                mensagem += "\n" + numAux + ") O assunto da mensagem de 'Acompanhamento' é obrigatório quando existe um perfil a ser notificado.";
            }

            if (mmTexto2_cnt.GetText() == '' && possuiGrupoAcompanhamento) {
                numAux++;
                mensagem += "\n" + numAux + ") O texto da mensagem de 'Acompanhamento' é obrigatório quando existe um perfil a ser notificado.";
            }

            if ((txtAssunto2_cnt.GetText() != '' || mmTexto2_cnt.GetText() != '') && !possuiGrupoAcompanhamento) {
                numAux++;
                mensagem += "\n" + numAux + ") A escolha de um perfil a ser notificado é obrigatório quando o assunto e o texto da mensagem de 'Acompanhamento' estiverem preenchidos.";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
                window.top.mostraMensagem(mensagem, 'atencao', true, false, null);
            }


            return mensagemErro_ValidaCamposFormulario;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 139px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td id="tdOpcoes" valign="top">
                                                            <table style="width:100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxLabel runat="server" Text="Op&#231;&#227;o:" ClientInstanceName="lblOpcao_cnt" EnableClientSideAPI="True"  ID="lblOpcao_cnt"></dxcp:ASPxLabel>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="edtAcao_cnt"  TabIndex="2" ToolTip="Informe a op&#231;&#227;o que levar&#225; &#224; etapa de destino" ID="edtAcao_cnt">
<ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                    <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                </ValidationSettings>
</dxcp:ASPxTextBox>


                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td valign="top" style="width:115px">
                                                            <table style="width:100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>                                                                        
                                                                        <td>
                                                                            <dxcp:ASPxLabel runat="server" Text="Cor do Bot&#227;o:" ClientInstanceName="lblCor_cnt" EnableClientSideAPI="True"  ID="ASPxLabel8"></dxcp:ASPxLabel>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>                                                                        
                                                                        <td><dxcp:ASPxColorEdit runat="server" Width="100%" ClientInstanceName="ceCorBotao_cnt"  ID="ceCorBotao_cnt"></dxcp:ASPxColorEdit>


                                                                            </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width:115px" valign="top">
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxcp:ASPxLabel runat="server" Text="&#205;cone do Bot&#227;o:" EnableClientSideAPI="True"  ID="ASPxLabel29"></dxcp:ASPxLabel>


                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxcp:ASPxComboBox runat="server" EncodeHtml="False" Width="100%" ClientInstanceName="cmbIconeBotao_cnt"  ID="cmbIconeBotao_cnt"></dxcp:ASPxComboBox>


                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gv_PessoasAcessos_etp" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv_PessoasAcessos_etp"  KeyFieldName="idGrupoNotificado" Width="100%" OnBatchUpdate="gv_PessoasAcessos_etp_BatchUpdate">
                                                            <ClientSideEvents BatchEditStartEditing="onStartEdit" EndCallback="function(s, e) {
	if(s.cp_msg != '')
	{
		if(s.cp_status == 'ok')
        {
            			window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
                        window.top.fechaModal();
        }
        		else
            			window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
	}
}
" />
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Batch">
                                                                <BatchEditSettings StartEditAction="DblClick" ShowConfirmOnLosingChanges="False" />
                                                            </SettingsEditing>
                                                            <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="160" VerticalScrollBarMode="Visible" />
                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                                                            <SettingsCommandButton>
                                                                <UpdateButton>
                                                                    <Image Url="~/imagens/botoes/salvar.png">
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image Url="~/imagens/botoes/cancelar.png">
                                                                    </Image>
                                                                </CancelButton>
                                                                <EditButton>
                                                                    <Image Url="~/imagens/botoes/editarReg02.png">
                                                                    </Image>
                                                                </EditButton>
                                                                <DeleteButton>
                                                                    <Image Url="~/imagens/botoes/excluirReg02.png">
                                                                    </Image>
                                                                </DeleteButton>
                                                            </SettingsCommandButton>
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="780px" />
                                                                <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </SettingsPopup>
                                                            <SettingsText EmptyDataRow="Nenhum perfil cadastrado" PopupEditFormCaption="Perfis Notificados" Title="Perfis Notificados" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" Ação" ShowCancelButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="Perfil" FieldName="idGrupoNotificado" Name="idGrupoNotificado" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <PropertiesComboBox Width="100%">
                                                                        <DropDownButton ClientVisible="False" Enabled="False">
                                                                        </DropDownButton>
                                                                        <ValidationSettings Display="Dynamic">
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings Caption="Perfil:" CaptionLocation="Top" Visible="True" />
                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="Tipo de Mensagem" FieldName="tipoNotificacao" Name="tipoNotificacao" ShowInCustomizationForm="True" VisibleIndex="3" Width="130px">
                                                                    <PropertiesComboBox Width="100%">                                                                        
                                                                        <Items>
                                                                            <dxtv:ListEditItem Text="Ação" Value="E"></dxtv:ListEditItem>
                                                                            <dxtv:ListEditItem Text="Acompanhamento" Value="S"></dxtv:ListEditItem>
                                                                        </Items>
                                                                        <DropDownButton ClientVisible="False" Enabled="False">
                                                                        </DropDownButton>
                                                                        <ValidationSettings CausesValidation="True" Display="Dynamic">
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings Caption="Tipo de Mensagem:" CaptionLocation="Top" Visible="True"  />
                                                                    <EditCellStyle HorizontalAlign="Left">
                                                                    </EditCellStyle>
                                                                </dxtv:GridViewDataComboBoxColumn>
                                                            </Columns>                                                            
                                                        </dxtv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">

 <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="620px"  ID="pcDados" Modal="True">
     <ClientSideEvents Shown="onShown" />
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxcp:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td>
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Perfil:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style3">
                            <dxtv:ASPxLabel ID="ASPxLabel30" runat="server"  Text="Tipo de Mensagem:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px">
                            <dxtv:ASPxComboBox ID="ddlPerfil" runat="server" ClientInstanceName="ddlPerfil"  Width="100%" OnCallback="ddlPerfil_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	        ddlPerfil.SetValue(gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, &quot;idGrupoNotificado&quot;));
            ddlTipo.SetValue(gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, &quot;tipoNotificacao&quot;));
}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                    <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxtv:ASPxComboBox>
                        </td>
                        <td class="auto-style3">
                            <dxtv:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo"  Width="100%">
                                <Items>
                                                                            <dxtv:ListEditItem Text="Ação" Value="E"></dxtv:ListEditItem>
                                                                            <dxtv:ListEditItem Text="Acompanhamento" Value="S"></dxtv:ListEditItem>
                                                                        </Items>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                    <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxtv:ASPxComboBox>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar" ValidationGroup="MKE2">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
if(ASPxClientEdit.ValidateGroup('MKE2', true))
    		onAcceptClick();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxcp:ASPxButton>


 </td><td style="WIDTH: 10px"></td><td><dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
   onCloseButtonClick();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxcp:ASPxButton>


 </td></tr></tbody></table>
            </td>
        </tr>
    </table>
</dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                                        <dxcp:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcNotificacoes" Width="100%"  ID="pcNotificacoes"><TabPages>
<dxcp:TabPage Name="tcMsgAcao_Cnt" Text="Mensagem A&#231;&#227;o"><ContentCollection>
<dxcp:ContentControl runat="server">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            <dxcp:ASPxLabel runat="server" Text="Assunto:" ClientInstanceName="lblAssunto1_cnt"  ID="lblAssunto1_cnt"></dxcp:ASPxLabel>



                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">
                                                                                                                            <dxtv:ASPxTextBox ID="txtAssunto1_cnt" runat="server" ClientInstanceName="txtAssunto1_cnt"  Width="100%">
                                                                                                                                <ClientSideEvents GotFocus="function(s, e) {
	elEditor = __ta_initialize(s.GetInputElement());
}" />
                                                                                                                            </dxtv:ASPxTextBox>



                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td align="right" style="height: 3px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 60px" valign="top">
                                                                                                            <dxcp:ASPxLabel runat="server" Text="Texto:" ClientInstanceName="lblTexto1_cnt"  ID="lblTexto1_cnt"></dxcp:ASPxLabel>



                                                                                                        </td>
                                                                                                        <td align="left" valign="top">
                                                                                                            <dxtv:ASPxMemo ID="mmTexto1_cnt" runat="server" ClientInstanceName="mmTexto1_cnt"  Rows="8" Width="100%">
                                                                                                                <ClientSideEvents GotFocus="function(s, e) {
	elEditor = __ta_initialize(s.GetInputElement());
}
" />
                                                                                                            </dxtv:ASPxMemo>



                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </dxcp:ContentControl>
</ContentCollection>
</dxcp:TabPage>
<dxcp:TabPage Name="tcAcompanhamento_tc" Text="Mensagem Acompanhamento"><ContentCollection>
<dxcp:ContentControl runat="server">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            <dxcp:ASPxLabel runat="server" Text="Assunto:" ClientInstanceName="lblAssunto2_cnt"  ID="lblAssunto2_cnt"></dxcp:ASPxLabel>



                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">
                                                                                                                            <dxtv:ASPxTextBox ID="txtAssunto2_cnt" runat="server" ClientInstanceName="txtAssunto2_cnt"  Width="100%">
                                                                                                                                <ClientSideEvents GotFocus="function(s, e) {
	elEditor = __ta_initialize(s.GetInputElement());
}
" />
                                                                                                                            </dxtv:ASPxTextBox>



                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td align="right" style="height: 3px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 60px" valign="top">
                                                                                                            <dxcp:ASPxLabel runat="server" Text="Texto:" ClientInstanceName="lblTexto2_cnt"  ID="lblTexto2_cnt"></dxcp:ASPxLabel>



                                                                                                        </td>
                                                                                                        <td align="left" valign="top">
                                                                                                            <dxtv:ASPxMemo ID="mmTexto2_cnt" runat="server" ClientInstanceName="mmTexto2_cnt"  Rows="8" Width="100%">
                                                                                                                <ClientSideEvents GotFocus="function(s, e) {
	elEditor = __ta_initialize(s.GetInputElement());
}
" />
                                                                                                            </dxtv:ASPxMemo>



                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </dxcp:ContentControl>
</ContentCollection>
</dxcp:TabPage>
</TabPages>
</dxcp:ASPxPageControl>



                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px" align="left">
                                                        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_msg != '')
	{
		if(s.cp_status == 'ok')
        {
            			window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
                        window.top.fechaModal();
        }
        		else
            			window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
	}
}" />
                                                        </dxcp:ASPxCallback>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" style="width:100%">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="AutoTexto: Nome Do Fluxo" ClientInstanceName="imgNomeDoProcessoConector" Cursor="pointer" ID="imgNomeDoProcessoConector">
<ClientSideEvents Click="function(s, e) {
	                if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [nomeDoFluxo] &quot;);
                }"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png" ToolTip="AutoTexto:  Data Inicio Fluxo" ClientInstanceName="imgDataInicioProcessoConector" Cursor="pointer" ID="imgDataInicioProcessoConector">
<ClientSideEvents Click="function(s, e) {
                    if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [dataInicioFluxo] &quot;);
                }"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png" ToolTip="AutoTexto: Responsavel Fluxo" ClientInstanceName="imgResponsavelProcessoConector" Cursor="pointer" ID="imgResponsavelProcessoConector">
<ClientSideEvents Click="function(s, e) {    
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [responsavelFluxo] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            &nbsp;<dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png" ToolTip="AutoTexto: Prazo Final Resposta" ClientInstanceName="imgPrazoFinalRespostaConector" Cursor="pointer" ID="imgPrazoFinalRespostaConector">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [prazoFinalResposta] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            &nbsp;<dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="AutoTexto: Responsavel A&#231;&#227;o" ClientInstanceName="imgResponsavelAcaoConector" Cursor="pointer" ID="imgResponsavelAcaoConector">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [ResponsavelAcao] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            &nbsp;<dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="AutoTexto: Data Ultima A&#231;&#227;o " ClientInstanceName="imgDataUltimaAcaoConector" Cursor="pointer" ID="imgDataUltimaAcaoConector">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [dataUltimaAcao] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="AutoTexto: Nome do Projeto " ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" ID="imgNomeProjetoConector">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [nomeProjeto] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="AutoTexto: Nome do Sistema" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" ID="ASPxImage10">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [nomeSistema] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="AutoTexto: Link do Sistema" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" ID="ASPxImage11">
<ClientSideEvents Click="function(s, e) {
	if(elEditor != null)
	                __ta_insertText(elEditor, &quot; [linkSistema] &quot;);
}"></ClientSideEvents>
</dxcp:ASPxImage>


                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/Help.png" ToolTip="Clique para ajuda com AutoTextos" ClientInstanceName="imgAjudaAutoTextoCnr" Cursor="pointer" ID="imgAjudaAutoTextoCnr"></dxcp:ASPxImage>


                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                                    </td>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxButton ID="btnOk_etp" runat="server" ClientInstanceName="btnOk_etp" TabIndex="7" Text="Salvar" Width="100px" AutoPostBack="False" ValidationGroup="MKE">
                                                                            <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('MKE', true) &amp;&amp; validaCamposFormulario() == &quot;&quot;)
		if(gv_PessoasAcessos_etp.batchEditApi.HasChanges())
			gv_PessoasAcessos_etp.UpdateEdit();
		else
                		callbackSalvar.PerformCallback();
                }" />
                                                                            <Paddings Padding="0px" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnCancelar_etp" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar_etp"
                                                                            Height="10px" TabIndex="8" Text="Fechar" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	               window.top.fechaModal();
	                e.processOnServer = false;
                }" />
                                                                            <Paddings Padding="0px" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                                    
                                            <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupElementID="imgAjudaAutoTextoCnr" HeaderText="AutoTexto" Width="600px"  ID="ASPxPopupControl2"><ContentCollection>
<dxcp:PopupControlContentControl runat="server">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style=" width: 100%;  text-align: justify">
                                                                        O recurso de <em>AutoTexto</em>permite incluir informações dinâmicas numa notificação.
                                                                        Os <em>AutoTextos</em>são substituídos por informações reais do fluxo e da ação
                                                                        causadores da notificação. Para usá-lo, posicione o cursor no campo &#39;Assunto&#39;
                                                                        ou &#39;Texto&#39; , no local que deseja inserir a informação e clique sobre o ícone
                                                                        do AutoTexto correspondente. Segue breve explicação de cada <em>AutoTexto</em>:<br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="Auto-Texto: [nomeDoFluxo]" Cursor="pointer" ID="ASPxImage5"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td>
                                                                                        <strong style=" text-align: justify">Nome do Fluxo:</strong>
                                                                                        <span style=" color: #339900;  text-align: justify">
                                                                                            [nomeDoFluxo]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Nome que o usuário deu ao fluxo no momento de sua criação. Este AutoTexto é uma
                                                                                        importante informação para quem vai receber a notificação.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png" ToolTip="Auto-Texto:  [dataInicioFluxo] " Cursor="pointer" ID="ASPxImage7"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td>
                                                                                        <strong style=" text-align: justify">Reponsável
                                                                                            pelo Fluxo:</strong> <span style=" color: #339900; 
                                                                                                text-align: justify">[responsavelFluxo]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Nome do usuário criador do fluxo.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png" ToolTip="Auto-Texto: [responsavelFluxo] " Cursor="pointer" ID="ASPxImage8"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td>
                                                                                        <strong style=" text-align: justify">Data de Início
                                                                                            do Fluxo: </strong><span style=" color: #339900; 
                                                                                                text-align: justify">[dataInicioFluxo]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Data de criação do fluxo.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png" ToolTip="Auto-Texto: [prazoFinalResposta] " Cursor="pointer" ID="ASPxImage9"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td style=" text-align: justify">
                                                                                        <strong>Prazo Final para Resposta:</strong><span style="color: #339900"> [prazoFinalResposta]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Data limite sugerida para resposta à ação.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="Auto-Texto: [responsavelAcao] " Cursor="pointer" ID="ASPxImage15"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td>
                                                                                        <strong style=" text-align: justify">Responsável
                                                                                            pela Ação:</strong> <span style=" color: #339900; 
                                                                                                text-align: justify">[responsavelAcao]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Nome da pessoa que gerou a ação.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="Auto-Texto: [dataUltimaAcao] " Cursor="pointer" ID="ASPxImage27"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td>
                                                                                        <strong style=" text-align: justify">Data da última
                                                                                            ação:</strong> <span style=" color: #339900; 
                                                                                                text-align: justify">[dataUltimaAcao]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Data/hora de ocorrência da ação.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="Auto-Texto: [nomeProjeto] " Cursor="pointer" ID="ASPxImage37"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td style=" text-align: justify">
                                                                                        <strong>Nome do projeto:</strong><span style="color: #339900">[nomeProjeto]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        &nbsp;Nome do projeto relacionado ao fluxo.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="Auto-Texto: [nomeSistema] " Cursor="pointer" ID="ASPxImage38"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td style=" text-align: justify">
                                                                                        <strong>Nome do sistema:</strong><span style="color: #339900">[nomeSistema]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Nome do sistema.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                                                        <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="Auto-Texto: [linkSistema] " Cursor="pointer" ID="ASPxImage39"></dxcp:ASPxImage>


                                                                                    </td>
                                                                                    <td style=" text-align: justify">
                                                                                        <strong>Link do Sistema:</strong><span style="color: #339900">[linkSistema]</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style=" text-align: justify">
                                                                                        Endereço do sistema na Internet.
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>


                        </td>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>
