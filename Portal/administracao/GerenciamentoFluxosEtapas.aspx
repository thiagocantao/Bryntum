<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GerenciamentoFluxosEtapas.aspx.cs" Inherits="administracao_GerenciamentoFluxosEtapas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
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
            var codigoPerfil = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, "idGrupoAcesso");
            var where = "";
            ddlPerfil.SetEnabled(codigoPerfil == null);

            if (codigoPerfil != null)
                where = " AND CodigoPerfilWf = " + codigoPerfil;
            else {
                var i = 0;
                indexNovo = visibleIndex;
                where = " AND CodigoPerfilWf NOT IN(-1"
                var indices = gv_PessoasAcessos_etp.batchEditHelper.GetDataItemVisibleIndices();

                for (i = 0; i < indices.length; i++) {
                    var rowKey = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoAcesso");
                    if (indices[i] < 0 || !gv_PessoasAcessos_etp.batchEditHelper.IsDeletedItem(rowKey)) {
                        if (gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoAcesso") != null)
                            where += ("," + gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoAcesso"));
                    }
                }

                where += ")";
            }

            ddlPerfil.PerformCallback(where);

        }
        function onAcceptClick(s, e) {
            gv_PessoasAcessos_etp.batchEditApi.SetCellValue(visibleIndex, "idGrupoAcesso", ddlPerfil.GetValue());
            gv_PessoasAcessos_etp.batchEditApi.SetCellValue(visibleIndex, "accessType", ddlTipo.GetValue());
            pcDados.Hide();
        }
        function onCloseButtonClick(s, e) {
            if (visibleIndex <= -1 && gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, "idGrupoAcesso") == null)
                gv_PessoasAcessos_etp.batchEditApi.ResetChanges(visibleIndex);

            pcDados.Hide();
        }

        function novaLinha() {
            gv_PessoasAcessos_etp.AddNewRow();
        }

        var possuiGrupoAcao = false;

        function verificaGrupos() {
            var indices = gv_PessoasAcessos_etp.batchEditHelper.GetDataItemVisibleIndices();

            for (i = 0; i < indices.length; i++) {
                var rowKey = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "idGrupoAcesso");
                if (indices[i] < 0 || !gv_PessoasAcessos_etp.batchEditHelper.IsDeletedItem(rowKey)) {
                    var registro = gv_PessoasAcessos_etp.batchEditApi.GetCellValue(indices[i], "accessType");
                    if (registro != null) {
                        if (registro == "Ação" || registro == 'A')
                            possuiGrupoAcao = true;
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

            verificaGrupos();

            if (!possuiGrupoAcao) {
                numAux++;
                mensagem += "\n" + numAux + ") Pelo menos 1 grupo deve ter acesso de ação na etapa!";
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
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 46px">
                                                                        <dxe:ASPxLabel runat="server" Text="ID:"  ID="ASPxLabel2">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 310px">
                                                                        <dxe:ASPxLabel runat="server" Text="Nome Abreviado:" 
                                                                            ID="ASPxLabel3">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 298px">
                                                                        <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o Resumida:"
                                                                            ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="edtIdEtapa_etp"
                                                                             ID="edtIdEtapa_etp">
                                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="40" ClientInstanceName="edtNomeAbreviado_etp"
                                                                             TabIndex="1" ToolTip="Informe o nome abreviado da etapa"
                                                                            ID="edtNomeAbreviado_etp">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="edtDescricaoResumida_etp"
                                                                             TabIndex="2" ToolTip="Esta informa&#231;&#227;o ser&#225; mostrada como 'tooltip' ao posicionar o mouse sobre o item no gr&#225;fico"
                                                                            ID="edtDescricaoResumida_etp" MaxLength="60">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
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
                                                        <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                            <tr>
                                                                <td>
                                                        <dxcp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server"  HeaderText="Prazo Previsto" Width="100%">
                                                            <ContentPaddings Padding="3px" />
                                                            <HeaderStyle Font-Bold="False">
                                                            <Paddings Padding="3px" />
                                                            </HeaderStyle>
                                                            <PanelCollection>
<dxcp:PanelContent runat="server">
    <table>
        <tr>
            <td style="width: 45px">
                <dxtv:ASPxTextBox ID="txtQtdTempo" runat="server" ClientInstanceName="txtQtdTempo"  HorizontalAlign="Right" ToolTip="Informe a quantidade de tempo previsto para a conclusão da etapa" Width="100%">
                    <MaskSettings IncludeLiterals="None" Mask="&lt;0..9999g&gt;" />
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="MKE">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </dxtv:ASPxTextBox>
            </td>
            <td style="width: 105px; padding-left: 10px;">
                <dxtv:ASPxComboBox ID="ddlUnidadeTempo" runat="server" ClientInstanceName="ddlUnidadeTempo"  ToolTip="Unidade de tempo do prazo previsto para a etapa" Width="100%">
                    <Items>
                        <dxtv:ListEditItem Text="horas" Value="horas" />
                        <dxtv:ListEditItem Text="dias" Value="dias" />
                        <dxtv:ListEditItem Text="dias úteis" Value="diasuteis" />
                        <dxtv:ListEditItem Text="semanas" Value="semanas" />
                        <dxtv:ListEditItem Text="meses" Value="meses" />
                    </Items>
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </dxtv:ASPxComboBox>
            </td>
            <td style="width: 180px; padding-left: 10px;">
                <dxtv:ASPxComboBox ID="ddlReferenciaTempo" runat="server" ClientInstanceName="ddlReferenciaTempo" ToolTip="Referência para o prazo previsto da etapa" Width="100%">
                    <Items>
                        <dxtv:ListEditItem Text="a partir do início da etapa" Value="IETP" />
                        <dxtv:ListEditItem Text="a partir do início do fluxo" Value="IFLX" />
                    </Items>
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </dxtv:ASPxComboBox>
            </td>
        </tr>
    </table>
                                                                </dxcp:PanelContent>
</PanelCollection>
                                                        </dxcp:ASPxRoundPanel>
                                                                </td>
                                                                <td style="padding-left: 5px">
                                                        <dxcp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server"  HeaderText="Etapa Controlada Associada" Width="100%">
                                                            <ContentPaddings Padding="3px" />
                                                            <HeaderStyle Font-Bold="False">
                                                            <Paddings Padding="3px" />
                                                            </HeaderStyle>
                                                            <PanelCollection>
<dxcp:PanelContent runat="server">
    <table>
        <tr>
            <td style="width: 315px">
                <dxtv:ASPxComboBox ID="ddlEtapaControladaAssociada" runat="server" ClientInstanceName="ddlEtapaControladaAssociada" ToolTip="Referência para o prazo previsto da etapa" Width="100%"></dxtv:ASPxComboBox>
            </td>
            <td style="width: 5px; padding-left: 10px;">
                &nbsp;</td>
            <td style="width: 10px; padding-left: 10px;">
                &nbsp;</td>
        </tr>
    </table>
                                                                </dxcp:PanelContent>
</PanelCollection>
                                                        </dxcp:ASPxRoundPanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxGridView ID="gv_PessoasAcessos_etp" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv_PessoasAcessos_etp"  KeyFieldName="idGrupoAcesso" Width="100%" OnBatchUpdate="gv_PessoasAcessos_etp_BatchUpdate">
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
                                                            <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="250" VerticalScrollBarMode="Visible" />
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
                                                            <SettingsText EmptyDataRow="Nenhum perfil cadastrado" PopupEditFormCaption="Pessoas com Acesso à Etapa" Title="Pessoas com Acesso à Etapa" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" Ação" ShowCancelButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="Perfil" FieldName="idGrupoAcesso" Name="idGrupoAcesso" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <PropertiesComboBox Width="100%">
                                                                        <DropDownButton ClientVisible="False" Enabled="False">
                                                                        </DropDownButton>
                                                                        <ValidationSettings Display="Dynamic">
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                    <EditFormSettings Caption="Perfil:" CaptionLocation="Top" Visible="True" />
                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                <dxtv:GridViewDataComboBoxColumn Caption="Acesso" FieldName="accessType" Name="accessType" ShowInCustomizationForm="True" VisibleIndex="3" Width="130px">
                                                                    <PropertiesComboBox Width="100%">                                                                        
                                                                        <Items>
                                                                            <dxtv:ListEditItem Text="Consulta" Value="C" />
                                                                            <dxtv:ListEditItem Selected="True" Text="Ação" Value="A" />
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
	        ddlPerfil.SetValue(gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, &quot;idGrupoAcesso&quot;));
            ddlTipo.SetValue(gv_PessoasAcessos_etp.batchEditApi.GetCellValue(visibleIndex, &quot;accessType&quot;));
}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE2">
                                    <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                </ValidationSettings>
                            </dxtv:ASPxComboBox>
                        </td>
                        <td class="auto-style3">
                            <dxtv:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo"  Width="100%">
                                <Items>
                                                                            <dxtv:ListEditItem Text="Consulta" Value="C" />
                                                                            <dxtv:ListEditItem Selected="True" Text="Ação" Value="A" />
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
                                                    <td align="right">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" style="width:100%">
                                                                        
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
                        </td>
                    </tr>
                </table>
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PortfolioConnectionString %>" SelectCommand="SELECT [CodigoEtapaWfControladaSistema], [NomeIndicadorEtapaWf]
FROM [dbo].[EtapasWfControladasSistema]
ORDER BY [NomeIndicadorEtapaWf]"></asp:SqlDataSource>
    </form>
</body>
</html>
