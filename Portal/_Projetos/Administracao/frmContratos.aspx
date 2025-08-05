<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContratos.aspx.cs" Inherits="_Projetos_DadosProjeto_frmContratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Contratos</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-top: 5px; padding-right: 10px; padding-left: 10px;">
                    <dxwgv:ASPxGridViewExporter ID="gvExporter" runat="server" GridViewID="gvDados" OnRenderBrick="gvExporter_RenderBrick">
                    </dxwgv:ASPxGridViewExporter>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        OnCallback="pnCallback_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato"
                                    AutoGenerateColumns="False" Width="100%" 
                                    ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                                    OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e){
     //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == 'btnEditarCustom')        {__isEdicao = false; onClickBarraNavegacao('Editar', gvDados, pcDados);}
     else if(e.buttonID == 'btnExcluirCustom')  {onClickBarraNavegacao('Excluir', gvDados, pcDados);}
     else if(e.buttonID == 'btnDetalhesCustom')
     {	
        TipoOperacao = 'Consultar';
        hfGeral.Set('TipoOperacao', TipoOperacao);
        hfGeral.Set('TipoOperacaoParcelas', TipoOperacao);
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		pcDados.Show();
     }
	else if(e.buttonID == 'btnPermissoesCustom')	{OnGridFocusedRowChangedPopup(gvDados);}
}
"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="135px" Caption="N&#186; Parcela"
                                            ToolTip="N&#186; Parcela" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                    <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnPermissoesCustom" Text="Visualizar Interessados">
                                                    <Image Url="~/imagens/Perfis/Perfil_Permissoes.png" ToolTip="Visualizar Interessados">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo Contrato"" onclick=""onClick_NovoContrato();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo Contrato"" style=""cursor: default;""/>")%>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton1" OnClick="ImageButton1_Click" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                                                    ToolTip="Exportar para excel"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroContrato" Name="NumeroContrato" Width="110px"
                                            Caption="N&#186; Contrato" VisibleIndex="1">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Fornecedor" Name="Fornecedor" Width="200px"
                                            Caption="Fornecedor" VisibleIndex="2">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUnidadeNegocio" Name="NomeUnidadeNegocio"
                                            Width="150px" Caption="Unidade Gestora" VisibleIndex="3">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Width="150px"
                                            Caption="Respons&#225;vel" VisibleIndex="4">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoAquisicao" Name="DescricaoTipoAquisicao"
                                            Width="150px" Caption="Modalidade Aquisi&#231;&#227;o" VisibleIndex="5">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="DataInicio" Name="DataInicio" Width="100px"
                                            Caption="Inicio Vig&#234;ncia" VisibleIndex="6">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                EditFormat="Custom">
                                            </PropertiesDateEdit>
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="GreaterOrEqual"></Settings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="DataTermino" Name="DataTermino" Width="110px"
                                            Caption="T&#233;rmino Vig&#234;ncia" VisibleIndex="7">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                EditFormat="Custom">
                                            </PropertiesDateEdit>
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="LessOrEqual"></Settings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoObjetoContrato" Name="DescricaoObjetoContrato"
                                            Width="225px" Caption="Objeto" VisibleIndex="8">
                                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="StatusContrato" Name="StatusContrato" Caption="Situa&#231;&#227;o"
                                            Visible="False" VisibleIndex="3">
                                            <DataItemTemplate>
                                                <%# (Eval("StatusContrato").ToString() == "A") ? "Ativo" : "Inativo"%>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Name="CodigoContrato" Caption="CodigoContrato"
                                            Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel"
                                            Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="8">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAquisicao" Name="CodigoTipoAquisicao"
                                            Caption="CodigoTipoAquisicao" Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Name="CodigoUnidadeNegocio"
                                            Caption="CodigoUnidadeNegocio" Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Observacao" Name="Observacao" Caption="Observacao"
                                            Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroContratoSAP" Name="NumeroContratoSAP"
                                            Caption="NumeroContratoSAP" Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" Caption="CodigoProjeto"
                                            Visible="False" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ValorContrato" Name="ValorContrato" Caption="ValorContrato"
                                            Visible="False" VisibleIndex="9">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Permissoes" Name="Permissoes" Visible="False"
                                            VisibleIndex="10">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoFonteRecursosFinanceiros" Visible="False"
                                            VisibleIndex="11">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoContrato" Visible="False" VisibleIndex="12">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                                        HorizontalScrollBarMode="Visible" ShowGroupedColumns="True"></Settings>
                                    <SettingsText GroupPanel="Arraste o cabe&#231;alho da coluna que deseja agrupar">
                                    </SettingsText>
                                    <Templates>
                                        <FooterRow>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="border-right: darkolivegreen 2px solid; border-top: darkolivegreen 2px solid;
                                                            border-left: darkolivegreen 2px solid; width: 10px; border-bottom: darkolivegreen 2px solid;
                                                            background-color: #ddffcc">
                                                            &nbsp;
                                                        </td>
                                                        <td style="padding-left: 10px">
                                                            <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                                Font-Size="7pt" Text="Contratos Inativos" Font-Bold="False">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="CloseButton" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="790px"
                                    ID="pcDados" Target="_top">
                                    <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('tabP'));
	LimpaCamposFormulario();
}" PopUp="function(s, e) {
	desabilitaHabilitaComponentes();
}" Shown="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('tabP'));
}"></ClientSideEvents>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl"
                                                Width="100%"  ID="tabControl">
                                                <TabPages>
                                                    <dxtc:TabPage Name="tabP" Text="Principal">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel runat="server" Text="N&#250;mero do Documento:" ClientInstanceName="lblNumeroContrato"
                                                                                                     ID="lblNumeroContrato">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 150px">
                                                                                                <dxe:ASPxLabel runat="server" Text="Tipo de Documento:" ClientInstanceName="lblTipoContrato"
                                                                                                     ID="lblTipoContrato">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 150px">
                                                                                                <dxe:ASPxLabel runat="server" Text="Situa&#231;&#227;o:" 
                                                                                                    ID="ASPxLabel2">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 160px">
                                                                                                <dxe:ASPxLabel runat="server" Text="Modalidade de Aquisi&#231;&#227;o:"
                                                                                                    ID="ASPxLabel1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-right: 10px">
                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtNumeroContrato"
                                                                                                     TabIndex="1" ID="txtNumeroContrato">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                            <td style="padding-right: 10px">
                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="DescricaoTipoContrato"
                                                                                                    ValueField="CodigoTipoContrato" Width="100%" ClientInstanceName="ddlTipoContrato"
                                                                                                     TabIndex="2" ID="ddlTipoContrato">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                    <Items>
                                                                                                        <dxe:ListEditItem Text="Ativo" Value="0"></dxe:ListEditItem>
                                                                                                        <dxe:ListEditItem Text="N&#227;o Ativo" Value="0"></dxe:ListEditItem>
                                                                                                    </Items>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxComboBox>
                                                                                            </td>
                                                                                            <td style="padding-right: 10px">
                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlSituacao"
                                                                                                     TabIndex="3" ID="ddlSituacao">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                    <Items>
                                                                                                        <dxe:ListEditItem Text="Ativo" Value="A"></dxe:ListEditItem>
                                                                                                        <dxe:ListEditItem Text="N&#227;o Ativo" Value="I"></dxe:ListEditItem>
                                                                                                    </Items>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxComboBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" TextField="DescricaoTipoAquisicao"
                                                                                                    ValueField="CodigoTipoAquisicao" Width="100%" ClientInstanceName="ddlModalidadAquisicao"
                                                                                                     TabIndex="4" ID="ddlModalidadAquisicao">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxComboBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel runat="server" Text="Fornecedor:" 
                                                                                                    ID="ASPxLabel3">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtFornecedor"
                                                                                                     TabIndex="5" ID="txtFornecedor">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                <dxe:ASPxLabel runat="server" Text="Objeto:" ClientInstanceName="lblNumeroContrato"
                                                                                     ID="ASPxLabel4">
                                                                                </dxe:ASPxLabel>
                                                                                <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater"
                                                                                    ForeColor="Silver" ID="lblCantCarater">
                                                                                </dxe:ASPxLabel>
                                                                                <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe250"
                                                                                    ForeColor="Silver" ID="lblDe250">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxMemo runat="server" Rows="3" Width="100%" ClientInstanceName="mmObjeto"
                                                                                     TabIndex="6" ID="mmObjeto">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}" Init="function(s, e) {
	onInit_mmObjeto(s, e);
}"></ClientSideEvents>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxMemo>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td >
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="width: 245px">
                                                                                                                <dxe:ASPxLabel runat="server" Text="Projeto:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel12">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td style="width: 240px">
                                                                                                                <dxe:ASPxLabel runat="server" Text="Unidade Gestora:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel5">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Fonte Pagadora:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel9">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="width: 245px;">
                                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" TextField="NomeProjeto"
                                                                                                                    ValueField="CodigoProjeto" Width="235px" ClientInstanceName="ddlProjetos"
                                                                                                                    TabIndex="7" ID="ddlProjetos">
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxComboBox>
                                                                                                            </td>
                                                                                                            <td style="width: 240px;">
                                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" TextField="NomeUnidadeNegocio"
                                                                                                                    ValueField="CodigoUnidadeNegocio" Width="230px" ClientInstanceName="ddlUnidadeGestora"
                                                                                                                     TabIndex="8" ID="ddlUnidadeGestora">
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxComboBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" TextField="NomeFonte"
                                                                                                                    ValueField="CodigoFonteRecursosFinanceiros" Width="250px" ClientInstanceName="ddlFontePagadora"
                                                                                                                     TabIndex="9" ID="ddlFontePagadora">
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxComboBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="width: 150px">
                                                                                                                &nbsp;<dxe:ASPxLabel runat="server" Text="In&#237;cio de Vig&#234;ncia:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel7">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td style="width: 150px">
                                                                                                                &nbsp;<dxe:ASPxLabel runat="server" Text="T&#233;rmino de Vig&#234;ncia:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel8">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Respons&#225;vel pela Gest&#227;o:" ClientInstanceName="lblNumeroContrato"
                                                                                                                     ID="ASPxLabel6">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="padding-right: 10px">
                                                                                                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                                    EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicioDeVigencia"
                                                                                                                     TabIndex="10" ID="ddlInicioDeVigencia">
                                                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                                    </CalendarProperties>
                                                                                                                    <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}" ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                                    </ValidationSettings>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxDateEdit>
                                                                                                            </td>
                                                                                                            <td style="padding-right: 10px">
                                                                                                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                                    EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTerminoDeVigencia"
                                                                                                                     TabIndex="11" ID="ddlTerminoDeVigencia">
                                                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                                                    </CalendarProperties>
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                                    </ValidationSettings>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxDateEdit>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxButtonEdit runat="server" Width="100%" ClientInstanceName="txtResponsavel"
                                                                                                                     TabIndex="12" ID="txtResponsavel">
                                                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
	e.processOnServer = false;
	mostraLovResponsavel(s.cp_Where);

	//e.processOnServer = false;
	//buscaNomeBD(s);	
}" TextChanged="function(s, e) {
	e.processOnServer = false;
	callbackResponsavel.PerformCallback(s.GetText());
}" ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <Buttons>
                                                                                                                        <dxe:EditButton>
                                                                                                                        </dxe:EditButton>
                                                                                                                    </Buttons>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxButtonEdit>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="width: 150px">
                                                                                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblNomeCIA"
                                                                                                                    ID="lblNomeCIA">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Valor do Contrato:" ClientInstanceName="lblValorDoContrato"
                                                                                                                     ID="lblValorDoContrato">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAux" Width="100%"
                                                                                                                    ID="ASPxLabel11" Visible="False">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="padding-right: 10px">
                                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtSAP"
                                                                                                                     TabIndex="13" ID="txtSAP">
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxTextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <dxe:ASPxTextBox runat="server" Width="170px" MaxLength="14" HorizontalAlign="Right"
                                                                                                                    DisplayFormatString="{0:n2}" ClientInstanceName="txtValorDoContrato"
                                                                                                                    TabIndex="14" ID="txtValorDoContrato">
                                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}"></ClientSideEvents>
                                                                                                                    <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>
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
                                                                                            <td >
                                                                                                <dxe:ASPxLabel runat="server" Text="Observa&#231;&#245;es:" ClientInstanceName="lblObservacoes"
                                                                                                     ID="lblObservacoes">
                                                                                                </dxe:ASPxLabel>
                                                                                                <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterOb"
                                                                                                    ForeColor="Silver" ID="lblCantCaraterOb">
                                                                                                </dxe:ASPxLabel>
                                                                                                <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe250Ob"
                                                                                                    ForeColor="Silver" ID="lblDe250Ob">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxMemo runat="server" Rows="3" Width="100%" ClientInstanceName="mmObservacoes"
                                                                                                     TabIndex="15" ID="mmObservacoes">
                                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	__isEdicao = true;
}" Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxMemo>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackResponsavel" ID="callbackResponsavel"
                                                                                    OnCallback="callbackResponsavel_Callback">
                                                                                    <ClientSideEvents EndCallback="function(s, e) 
{
	//if(s.cp_lovCodigoResponsavel != &quot;-1&quot;)
	//{
    //    hfGeral.Set(&quot;lovMostrarPopPup&quot;, s.cp_lovMostrarPopPup);
	//	hfGeral.Set(&quot;lovCodigoResponsavel&quot;, s.cp_lovCodigoResponsavel);
	//}

	onEnd_CallbackResponsavel(s, e);
}"></ClientSideEvents>
                                                                                </dxcb:ASPxCallback>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabF" Text="Financeiro">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel runat="server" Text="Parcelas:" ClientInstanceName="lblNumeroContrato"
                                                                                     ID="ASPxLabel10">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvParcelas" KeyFieldName="CodigoContrato;NumeroAditivoContrato;NumeroParcela"
                                                                                    AutoGenerateColumns="False" Width="100%" 
                                                                                    ID="gvParcelas" OnHtmlDataCellPrepared="gvParcelas_HtmlDataCellPrepared" OnRowInserting="gvParcelas_RowInserting"
                                                                                    OnRowDeleting="gvParcelas_RowDeleting" OnCustomCallback="gvParcelas_CustomCallback"
                                                                                    OnRowUpdating="gvParcelas_RowUpdating" OnCellEditorInitialize="gvParcelas_CellEditorInitialize"
                                                                                    OnCommandButtonInitialize="gvParcelas_CommandButtonInitialize">
                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	onClick_CustomButtomGridParcelas(s, e);
}" EndCallback="function(s, e) {
	onEnd_CallbackGvParcelas(s, e);
}"></ClientSideEvents>
                                                                                    <TotalSummary>
                                                                                        <dxwgv:ASPxSummaryItem SummaryType="Sum" FieldName="ValorPrevisto" DisplayFormat="{0:n2}"
                                                                                            ShowInColumn="ValorPrevisto" ShowInGroupFooterColumn="ValorPrevisto" Tag="Tot.:">
                                                                                        </dxwgv:ASPxSummaryItem>
                                                                                        <dxwgv:ASPxSummaryItem SummaryType="Sum" FieldName="ValorPago" DisplayFormat="{0:n2}"
                                                                                            ShowInColumn="ValorPago" ShowInGroupFooterColumn="ValorPago" Tag="Tat.:"></dxwgv:ASPxSummaryItem>
                                                                                    </TotalSummary>
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="110px" Caption="N&#186; Parcela"
                                                                                            VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true">
                                                                                            <CustomButtons>
                                                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                                                                                    <Image Url="~/imagens/botoes/pFormulario.png">
                                                                                                    </Image>
                                                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                                            </CustomButtons>
                                                                                            <HeaderTemplate>
                                                                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (hfGeral.Get("TipoOperacaoParcelas").ToString() != "Consultar") ? @"<img style=""cursor: pointer"" onclick=""gvParcelas.AddNewRow();"" src=""../../imagens/botoes/incluirReg02.png"" alt=""Adicionar Nova Parcela.""/>" : "")%>
                                                                                            </HeaderTemplate>
                                                                                            <FooterTemplate>
                                                                                                <dxe:ASPxLabel ID="lblTotales" runat="server" Text="TOTAL:"
                                                                                                    ClientInstanceName="lblTotales">
                                                                                                </dxe:ASPxLabel>
                                                                                            </FooterTemplate>
                                                                                        </dxwgv:GridViewCommandColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroAditivoContrato" Name="NumeroAditivo"
                                                                                            Width="50px" Caption="N&#186; Aditivo" VisibleIndex="1">
                                                                                            <PropertiesTextEdit MaxLength="3" ClientInstanceName="txtNumeroAditivo">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//onValidation_NumeroAditivo(s, e);
}"></ClientSideEvents>
                                                                                                <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                                                                            </PropertiesTextEdit>
                                                                                            <EditFormSettings VisibleIndex="1" CaptionLocation="Top" Caption="N&#186; Aditivo:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NumeroParcela" Name="NumeroParcela" Width="50px"
                                                                                            Caption="N&#186; Parcela" VisibleIndex="1">
                                                                                            <PropertiesTextEdit MaxLength="3" ClientInstanceName="txtNumeroParcela">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	onValidation_NumeroParcela(s, e);
}"></ClientSideEvents>
                                                                                                <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                                                                            </PropertiesTextEdit>
                                                                                            <EditFormSettings VisibleIndex="2" CaptionLocation="Top" Caption="N&#186; Parcela:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ValorPrevisto" Name="ValorPrevisto" Caption="Valor  Previsto"
                                                                                            VisibleIndex="2">
                                                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	onValidation_ValorPrevisto(s, e);
}"></ClientSideEvents>
                                                                                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                            </PropertiesTextEdit>
                                                                                            <EditFormSettings VisibleIndex="3" CaptionLocation="Top" Caption="Valor  Previsto:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataDateColumn FieldName="DataVencimento" Name="DataVencimento" Caption="Data de Vencimento"
                                                                                            VisibleIndex="3">
                                                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                                                <ValidationSettings>
                                                                                                    <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio!"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                            </PropertiesDateEdit>
                                                                                            <EditFormSettings VisibleIndex="4" CaptionLocation="Top" Caption="Data de Vencimento:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataDateColumn>
                                                                                        <dxwgv:GridViewDataDateColumn FieldName="DataPagamento" Name="DataPagamento" Caption="Data de Pagamento"
                                                                                            VisibleIndex="4">
                                                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlDataPagamento">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	onValidation_DataPagamentoGvParcela(s, e);
}"></ClientSideEvents>
                                                                                                <ValidationSettings ValidationGroup="VGG">
                                                                                                </ValidationSettings>
                                                                                            </PropertiesDateEdit>
                                                                                            <EditFormSettings VisibleIndex="6" CaptionLocation="Top" Caption="Data de Pagamento:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataDateColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ValorPago" Name="ValorPago" Caption="Valor Pago"
                                                                                            VisibleIndex="5">
                                                                                            <PropertiesTextEdit DisplayFormatString="{0:n2}" ClientInstanceName="txtValorPagoGvParcela">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	onValidation_ValorPagoGvParcela(s, e);
}"></ClientSideEvents>
                                                                                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                                                <ValidationSettings ValidationGroup="VGG">
                                                                                                </ValidationSettings>
                                                                                            </PropertiesTextEdit>
                                                                                            <EditFormSettings VisibleIndex="5" CaptionLocation="Top" Caption="Valor Pago:"></EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataMemoColumn FieldName="HistoricoParcela" Name="HistoricoParcela"
                                                                                            Caption="Hist&#243;rico" VisibleIndex="6">
                                                                                            <PropertiesMemoEdit Rows="4">
                                                                                                <ClientSideEvents KeyPress="function(s, e) {
 var texto = s.GetText();
 if(texto.length &gt; 500)
 {
  s.SetText(texto.substring(0,500));
 }
}"></ClientSideEvents>
                                                                                            </PropertiesMemoEdit>
                                                                                            <EditFormSettings ColumnSpan="2" VisibleIndex="7" CaptionLocation="Top" Caption="Hist&#243;rico:">
                                                                                            </EditFormSettings>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataMemoColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Name="CodigoContrato" Caption="CondigoContrato"
                                                                                            Visible="False" VisibleIndex="7">
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="TipoRegistro" Name="TipoRegistro" Caption="TipoRegistro"
                                                                                            Visible="False" VisibleIndex="7">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"
                                                                                        ConfirmDelete="True"></SettingsBehavior>
                                                                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <SettingsPopup>
                                                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                            AllowResize="True" Width="350px" VerticalOffset="-40" />
                                                                                            <CustomizationWindow HorizontalAlign="WindowCenter"  VerticalAlign="WindowCenter"/>
                                                                                    </SettingsPopup>
                                                                                    <Settings ShowTitlePanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                                                                                        VerticalScrollableHeight="215"></Settings>
                                                                                    <SettingsText Title="Parcelas associadas" ConfirmDelete="Retirar a Parcela para este contrato?"
                                                                                        PopupEditFormCaption="Parcela do Contrato"></SettingsText>
                                                                                    <StylesPopup>
                                                                                        <EditForm>
                                                                                            <Header Font-Bold="True">
                                                                                            </Header>
                                                                                            <MainArea Font-Bold="False"></MainArea>
                                                                                        </EditForm>
                                                                                    </StylesPopup>
                                                                                    <Styles>
                                                                                        <Header Font-Bold="False">
                                                                                        </Header>
                                                                                        <HeaderPanel Font-Bold="False">
                                                                                        </HeaderPanel>
                                                                                        <TitlePanel Font-Bold="True">
                                                                                        </TitlePanel>
                                                                                    </Styles>
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tabA" Text="Anexos">
                                                        <ContentCollection>
                                                            <dxw:ContentControl runat="server">
                                                                <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallback1" Width="100%"
                                                                    ID="pnCallback1" OnCallback="pnCallback1_Callback">
                                                                    <PanelCollection>
                                                                        <dxp:PanelContent runat="server">
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 10px; height: 25px">
                                                                                        </td>
                                                                                        <td style="height: 25px">
                                                                                            <span runat="server" id="spnControlesAnexo" enableviewstate="False">
                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="width: 30px">
                                                                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/pastaRaiz.PNG" ToolTip="Incluir Pasta na Raiz"
                                                                                                                    ClientInstanceName="imgPastaRaiz" Cursor="pointer" ID="imgPastaRaiz" EnableViewState="False">
                                                                                                                    <ClientSideEvents Click="function(s, e) 
{
	abrePopUp('Pasta','IncluirRaiz');
//pnCallback1.PerformCallback(&quot;Listar&quot;);
//window.location.reload();
}"></ClientSideEvents>
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td style="width: 30px">
                                                                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/novoAnexo.png" ToolTip="Incluir Pasta"
                                                                                                                    ClientInstanceName="imgIncluirPastaRaiz" Cursor="pointer" ID="imgIncluirPastaRaiz"
                                                                                                                    EnableViewState="False">
                                                                                                                    <ClientSideEvents Click="function(s, e) {
	abrePopUp('Pasta','Incluir');
//pnCallback1.PerformCallback(&quot;Listar&quot;);
//window.location.reload();
}"></ClientSideEvents>
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td style="width: 30px">
                                                                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/anexar.png" ToolTip="Incluir Arquivo"
                                                                                                                    ClientInstanceName="imgIncluirPastaRaiz" Cursor="pointer" ID="ASPxImage2" EnableViewState="False">
                                                                                                                    <ClientSideEvents Click="function(s, e) {
	abrePopUp('Arquivo','Incluir');
  //pnCallback1.PerformCallback(&quot;Listar&quot;);
//window.location.reload();




}"></ClientSideEvents>
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td style="width: 30px">
                                                                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/editarReg02.PNG" ToolTip="Editar"
                                                                                                                    ClientInstanceName="imgIncluirPastaRaiz" Cursor="pointer" ID="ASPxImage3" EnableViewState="False">
                                                                                                                    <ClientSideEvents Click="function(s, e) {
	var tipoAnexo = hfAnexos.Contains(&quot;IndicaPasta&quot;) ? hfAnexos.Get(&quot;IndicaPasta&quot;) : &quot;&quot;;
	if(tipoAnexo != &quot;&quot;)
	{
		if(tipoAnexo == &quot;S&quot;)
		{
			abrePopUp('Pasta','Editar'); 
		} 	
		else
		{
			abrePopUp('Arquivo','Editar'); 
		}
	}
	else
	{
		window.top.mostraMensagem(&quot;Selecione uma pasta ou arquivo para editar.&quot;, 'atencao', true, false, null);
	}
	pnCallback1.PerformCallback(&quot;Listar&quot;);
//window.location.reload();
}"></ClientSideEvents>
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td style="width: 28px">
                                                                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/excluirReg02.PNG" ToolTip="Excluir"
                                                                                                                    Cursor="pointer" ID="ASPxImage4" EnableViewState="False">
                                                                                                                    <ClientSideEvents Click="function(s, e) 
{
	var tipoAnexo = hfAnexos.Contains(&quot;IndicaPasta&quot;) ? hfAnexos.Get(&quot;IndicaPasta&quot;) : &quot;&quot;;
    if(tipoAnexo != &quot;&quot;)
	{
		var confirmaExclusao = confirm(&quot;Deseja realmente excluir o registro?&quot;);
		if(true == confirmaExclusao)
			pnCallback1.PerformCallback(&quot;excluir&quot;);
		else
			e.processOnServer =	false;
	}
	else
		window.top.mostraMensagem(&quot;Selecione uma pasta ou arquivo para excluir.&quot;, 'atencao', true, false, null);
}"></ClientSideEvents>
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxp:ASPxPanel runat="server" ClientInstanceName="pnAnexosContratos" Width="100%"
                                                                                                Height="240px" ID="pnAnexosContratos" Style="overflow: scroll">
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                                <PanelCollection>
                                                                                                    <dxp:PanelContent runat="server">
                                                                                                        <dxwtl:ASPxTreeList runat="server" KeyFieldName="CodigoAnexo" ParentFieldName="CodigoPastaSuperior"
                                                                                                            AutoGenerateColumns="False" ClientInstanceName="tlAnexos" Width="97%"
                                                                                                            ID="tlAnexos">
                                                                                                            <Columns>
                                                                                                                <dxwtl:TreeListTextColumn FieldName="Nome" AllowSort="False" Name="Nome" Width="100%"
                                                                                                                    Caption="Anexo" VisibleIndex="0">
                                                                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                                                    <CellStyle HorizontalAlign="Left">
                                                                                                                    </CellStyle>
                                                                                                                </dxwtl:TreeListTextColumn>
                                                                                                            </Columns>
                                                                                                            <Settings ShowColumnHeaders="False"></Settings>
                                                                                                            <SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True"></SettingsBehavior>
                                                                                                            <Styles>
                                                                                                                <Indent BackColor="Transparent">
                                                                                                                </Indent>
                                                                                                                <IndentWithButton BackColor="Transparent">
                                                                                                                </IndentWithButton>
                                                                                                                <Node BackColor="Transparent">
                                                                                                                </Node>
                                                                                                                <Cell>
                                                                                                                    <Paddings PaddingLeft="1px"></Paddings>
                                                                                                                </Cell>
                                                                                                            </Styles>
                                                                                                            <ClientSideEvents FocusedNodeChanged="function(s, e) {
	OnFocusedNodeChanged(s);
}" EndCallback="function(s, e) {
	//onEnd_pnCallback(s);
}"></ClientSideEvents>
                                                                                                            <Templates>
                                                                                                                <DataCell>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl='<%# GetIconUrl(Container) %>'
                                                                                                                                                Width="16" Height="16"  >
                                                                                                                                            </dxe:ASPxImage>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <dxe:ASPxButton ID="btnDownLoad" runat="server" Height="16px" ImageSpacing="0px"
                                                                                                                                                Width="16px" Wrap="False" AutoPostBack="False" OnClick="btnDownLoad_Click" ToolTip="Visualizar o arquivo">
                                                                                                                                                <Image Url="~/imagens/anexo/download.png" />
                                                                                                                                                <FocusRectPaddings Padding="0px" />
                                                                                                                                                <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = true;	
}" />
                                                                                                                                                <Border BorderWidth="0px" />
                                                                                                                                            </dxe:ASPxButton>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <td style="width: 2px">
                                                                                                                            </td>
                                                                                                                            <td style="padding-bottom: 1px;" title="<%# GetToolTip(Container) %>">
                                                                                                                                <%# Container.Text %>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </DataCell>
                                                                                                            </Templates>
                                                                                                        </dxwtl:ASPxTreeList>
                                                                                                    </dxp:PanelContent>
                                                                                                </PanelCollection>
                                                                                            </dxp:ASPxPanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfAnexos" ID="hfAnexos">
                                                                            </dxhf:ASPxHiddenField>
                                                                        </dxp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                                <ClientSideEvents ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)
}"></ClientSideEvents>
                                                <ContentStyle>
                                                    <Paddings Padding="3px"></Paddings>
                                                </ContentStyle>
                                            </dxtc:ASPxPageControl>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-top: 5px" align="right">
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"
                                                                 TabIndex="16" ID="btnSalvar">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	onClick_Salvar(s, e);
	__isEdicao = false;
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td style="width: 100px; padding-top: 5px" align="right">
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" CommandArgument="btnCancelar"
                                                                Text="Cancelar" Width="100px"  TabIndex="17"
                                                                ID="btnCancelar">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
	{
       onClick_btnCancelar();
		hfGeral.Set(&quot;TipoOperacaoParcelas&quot;, &quot;&quot;);
	}
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackGeral" ID="callbackGeral"
                                                OnCallback="callbackGeral_Callback">
                                            </dxcb:ASPxCallback>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="" align="center">
                                                        </td>
                                                        <td style="width: 70px" align="center" rowspan="3">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                ID="lblAcaoGravacao">
                                                            </dxe:ASPxLabel>
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
	onEnd_pnCallbackLocal(s, e);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
