<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegracaoOrcamentoERP.aspx.cs" Inherits="_Projetos_DadosProjeto_IntegracaoOrcamentoERP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orçamento ERP</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <style type="text/css">
        .style2 {
            width: 100%;
        }

        .style3 {
            height: 5px;
        }

        .style4 {
            width: 10px;
            height: 10px;
        }

        .style5 {
            height: 10px;
        }

        .style6 {
            width: 3px;
            height: 10px;
        }
        .textoComIniciaisMaiuscula{
            text-transform:capitalize
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="style4"></td>
                    <td class="style5"></td>
                    <td class="style6"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvDados" KeyFieldName="CodigoCR"
                            Width="100%" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                            <SettingsText></SettingsText>

                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}"
                                CustomButtonClick="function(s, e) {
	onClick_CustomButtomGvDados(s, e);
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="A&#231;&#227;o" Width="50px"
                                    Caption="A&#231;&#227;o" VisibleIndex="0">
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>

                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                    <HeaderTemplate>
                                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""novaIntegracao();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                                    </HeaderTemplate>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="NomeCR" Name="NomeCR" Caption="CR"
                                    VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoReservado" Name="CodigoReservado"
                                    Width="200px" Caption="Codigo" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Ano" Name="Ano" Width="80px" Caption="Ano"
                                    VisibleIndex="4">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoMovimentoOrcamento"
                                    Name="DescricaoMovimentoOrcamento" Caption="Movimento Orcament&#225;rio"
                                    VisibleIndex="5" Width="280px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Sigla Entidade"
                                    FieldName="SiglaUnidadeNegocio" VisibleIndex="2" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                            <Settings VerticalScrollBarMode="Visible"></Settings>
                            <SettingsLoadingPanel Mode="Disabled" />
                        </dxwgv:ASPxGridView>
                        <dxcp:ASPxCallbackPanel ID="pnCallbackPopup" runat="server" ClientInstanceName="pnCallbackPopup"
                            OnCallback="pnCallbackPopup_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxpc:ASPxPopupControl runat="server"
                                        AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None"
                                        HeaderText="Detalhe" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="680px"
                                        ID="pcDados">
                                        <ClientSideEvents PopUp="function(s, e) {
	        //desabilitaHabilitaComponentes();
        }"></ClientSideEvents>

                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>

                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table id="tbEntidade" runat="server" cellpadding="0" cellspacing="0" class="style2">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="lblEntidade" runat="server" ClientInstanceName="lblEntidade"
                                                                                Text="Entidade:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="ddlEntidade" runat="server"
                                                                                ClientInstanceName="ddlEntidade"
                                                                                ForeColor="Black" IncrementalFilteringMode="Contains" Width="100%"
                                                                                TextFormatString="{0} - {1}">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMovimientoOrcamentario.PerformCallback('A');
}" />
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMovimientoOrcamentario.PerformCallback(&#39;A&#39;);
}"></ClientSideEvents>
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio"
                                                                                        Width="160px" />
                                                                                    <dxe:ListBoxColumn Caption="Entidade" FieldName="NomeUnidadeNegocio"
                                                                                        Name="650" />
                                                                                </Columns>
                                                                                <DisabledStyle BackColor="#EEEEDD">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style3"></td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblInteresado" runat="server"
                                                                    ClientInstanceName="lblInteresado"
                                                                    Text="Movimento Orçamentário:">
                                                                </dxe:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlMovimientoOrcamentario" runat="server"
                                                                    ClientInstanceName="ddlMovimientoOrcamentario"
                                                                    ForeColor="Black" TextField="DescricaoMovimentoOrcamento"
                                                                    ValueField="CodigoMovimentoOrcamento" ValueType="System.Int32"
                                                                    Width="100%" OnCallback="ddlMovimientoOrcamentario_Callback">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
                            {
	                            onChanged_ddlMovimientoOrcamentario(s, e);
                            }" />
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
                            {
	                            onChanged_ddlMovimientoOrcamentario(s, e);
                            }"
                                                                        EndCallback="function(s, e) {
	onChanged_ddlMovimientoOrcamentario(s, e);
}"></ClientSideEvents>

                                                                    <DisabledStyle BackColor="#EEEEDD">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblPapelNoProjeto" runat="server"
                                                                    ClientInstanceName="lblPapelNoProjeto" ClientVisible="False"
                                                                    Text="Unidade Orçamento:">
                                                                </dxe:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlPapelNoProjeto" runat="server"
                                                                    ClientInstanceName="ddlPapelNoProjeto" ClientVisible="False"
                                                                    ForeColor="Black" Width="100%">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                if(ddlPapelNoProjeto.GetEnabled)
	                                {
		                                if (ddlPapelNoProjeto.GetValue() == &quot;1&quot;)
		                                {
				                            window.top.mostraMensagem(&quot;O Papel 'Gerente do Projeto' não pode ser selecionado neste caso!&quot;, 'atencao', true, false, null);
			                                ddlPapelNoProjeto.SetFocus();
			                                return false;
		                                }
	                                }
	                                pnCallbackCheck.PerformCallback();
                                 }" />
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                if(ddlPapelNoProjeto.GetEnabled)
	                                {
		                                if (ddlPapelNoProjeto.GetValue() == &quot;1&quot;)
		                                {
				                            window.top.mostraMensagem(&quot;O Papel &#39;Gerente do Projeto&#39; n&#227;o pode ser selecionado neste caso!&quot;, 'atencao', true, false, null);
			                                ddlPapelNoProjeto.SetFocus();
			                                return false;
		                                }
	                                }
	                                pnCallbackCheck.PerformCallback();
                                 }"></ClientSideEvents>

                                                                    <DisabledStyle BackColor="#EEEEDD">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblSuperiorNoProjeto" runat="server"
                                                                    ClientInstanceName="lblSuperiorNoProjeto"
                                                                    Text="CR:">
                                                                </dxe:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxCallbackPanel ID="pnCallbackCR" runat="server"
                                                                    ClientInstanceName="pnCallbackCR" OnCallback="pnCallbackCR_Callback"
                                                                    Width="100%">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	                                        onEnd_CallbackCR(s, e);
                                        }" />
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	                                        onEnd_CallbackCR(s, e);
                                        }"></ClientSideEvents>
                                                                    <PanelCollection>
                                                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                            <dxe:ASPxComboBox ID="ddlListagemCR" runat="server"
                                                                                ClientInstanceName="ddlListagemCR"
                                                                                IncrementalFilteringMode="Contains" TextField="NomeCR" TextFormatString="{1}"
                                                                                ValueField="CodigoCR" Width="100%">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                            onChanged_ddlListagemCR(s, e);
                                                            }" />
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                            onChanged_ddlListagemCR(s, e);
                                                            }"></ClientSideEvents>
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn Caption="Código Reservado" FieldName="CodigoReservado"
                                                                                        Width="120px" />
                                                                                    <dxe:ListBoxColumn Caption="Nome CR" FieldName="NomeCR" Width="500px" />
                                                                                </Columns>
                                                                            </dxe:ASPxComboBox>
                                                                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                                            </dxhf:ASPxHiddenField>
                                                                        </dxp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxCallbackPanel ID="pnCallbackEstadoCR" runat="server"
                                                                    ClientInstanceName="pnCallbackEstadoCR"
                                                                    OnCallback="pnCallbackEstadoCR_Callback"
                                                                    Width="100%">
                                                                    <PanelCollection>
                                                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="padding-right: 3px; padding-left: 3px" valign="top">
                                                                                            <dxe:ASPxLabel ID="lblAtencao" runat="server" ClientInstanceName="lblAtencao"
                                                                                                Font-Bold="True">
                                                                                            </dxe:ASPxLabel>
                                                                                            <dxe:ASPxLabel ID="lblCorpoAtencao" runat="server"
                                                                                                ClientInstanceName="lblCorpoAtencao" Font-Bold="False">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 100px">
                                                                                            <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                                                                Text="Salvar" Width="100%" CssClass="textoComIniciaisMaiuscula">
                                                                                                <ClientSideEvents Click="function(s, e) {
	                                    e.processOnServer = false;
                                        if (window.onClick_btnSalvar)
	                                        onClick_btnSalvar();
                                    }" />
                                                                                                <ClientSideEvents Click="function(s, e) {
	                                    e.processOnServer = false;
                                        if (window.onClick_btnSalvar)
	                                        onClick_btnSalvar();
                                    }"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td style="width: 10px"></td>
                                                                                        <td style="width: 100px">
                                                                                            <dxe:ASPxButton ID="btnCancelar" runat="server" CommandArgument="btnCancelar"
                                                                                                Text="Fechar" Width="100%" CssClass="textoComIniciaisMaiuscula">
                                                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                                 e.processOnServer = false;
	                                                                                                 pcDados.Hide();
                                    }"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="" align="center"></td>
                                                            <td style="width: 70px" align="center" rowspan="3">
                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
	onEnd_CallbackPopup(s, e);
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td style="width: 3px"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
