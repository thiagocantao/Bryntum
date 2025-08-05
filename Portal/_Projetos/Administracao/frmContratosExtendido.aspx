<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmContratosExtendido.aspx.cs"
    Inherits="_Projetos_DadosProjeto_frmContratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Contratos</title>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div>
            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
            </dxwgv:ASPxGridViewExporter>
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoContrato"
                AutoGenerateColumns="False" Width="100%"
                ID="gvDados" EnableViewState="False" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCustomCallback="gvDados_CustomCallback"
                EnableRowsCache="False" EnableTheming="True">
                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg!= null &amp;&amp; s.cp_Msg != &quot;&quot;)
		mostraDivSalvoPublicado(s.cp_Msg);
	
	txtComentarioEncerramento.SetText(&quot;&quot;);
	pcEncerramento.Hide();
}"
                    Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 20;
s.SetHeight(sHeight);
}"></ClientSideEvents>
                <Columns>
                    <dxwgv:GridViewDataTextColumn ToolTip="Nº Parcela" VisibleIndex="0" Width="130px"
                        ExportWidth="1">
                        <DataItemTemplate>
                            <%# getBotoes() %>
                        </DataItemTemplate>
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo Contrato"" onclick=""novoRegistro();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo Contrato"" style=""cursor: default;""/>")%>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                            OnClick="ImageButton1_Click" ToolTip="Exportar para excel" />
                                    </td>
                                    <td>
                                        <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/botoes/btnPDF.png"
                                            ToolTip="Imprime relatório" Visible="False">
                                            <ClientSideEvents Click="function(s, e) {
         e.processOnServer = false;
	     onClick_btnSalvar_MSR();
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Width="35px" Caption=" " VisibleIndex="1">
                        <DataItemTemplate>
                            <%# Eval("QuantidadeAditivos") + "" != "" && int.Parse(Eval("QuantidadeAditivos").ToString()) > 0 ? "<img src='../../imagens/botoes/aditado.png' alt='Este contrato possui " + Eval("QuantidadeAditivos").ToString() + " aditivo(s).' />" : "&nbsp;"%>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nº Contrato SAP" VisibleIndex="2" FieldName="NumeroContratoSAP"
                        Name="NumeroContratoSAP" Width="130px">
                        <Settings AutoFilterCondition="Contains" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NumeroContrato" Name="NumeroContrato" Width="130px"
                        Caption="N&#186; Contrato" VisibleIndex="3">
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle>
                        </FilterCellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NomeMunicipio" Width="250px" VisibleIndex="4"
                        Caption="Nome Município">
                        <Settings AutoFilterCondition="Contains"></Settings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NomePessoa" Width="210px" Caption="Raz&#227;o Social"
                        VisibleIndex="5">
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle>
                        </FilterCellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Data Assinatura" FieldName="DataAssinatura"
                        Name="DataAssinatura" VisibleIndex="6" Width="115px">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="GreaterOrEqual" ShowFilterRowMenu="True" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DataInicio" Name="DataInicio" Width="115px"
                        Caption="Data OS Externa" VisibleIndex="7">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom">
                        </PropertiesDateEdit>
                        <Settings ShowFilterRowMenu="True" AutoFilterCondition="GreaterOrEqual"></Settings>
                        <FilterCellStyle>
                        </FilterCellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DataTermino" Name="DataTermino" Width="115px"
                        Caption="Data T&#233;rmino" VisibleIndex="8">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom">
                        </PropertiesDateEdit>
                        <Settings ShowFilterRowMenu="True" AutoFilterCondition="LessOrEqual"></Settings>
                        <FilterCellStyle>
                        </FilterCellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SituacaoContrato" Width="75px" Caption="Situa&#231;&#227;o"
                        VisibleIndex="10">
                        <Settings AutoFilterCondition="Contains"></Settings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoObjetoContrato" Name="DescricaoObjetoContrato"
                        Width="595px" Caption="Objeto" VisibleIndex="9">
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle>
                        </FilterCellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ValorContratoOriginal" Width="180px" Caption="Valor Contrato"
                        VisibleIndex="28">
                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                        </PropertiesTextEdit>
                        <Settings AllowAutoFilter="False"></Settings>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ValorContrato" Width="180px" Caption="Valor com Aditivo"
                        VisibleIndex="29">
                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                        </PropertiesTextEdit>
                        <Settings AllowAutoFilter="False"></Settings>
                        <DataItemTemplate>
                            <%# getValorAditadoGrid(Eval("ValorContrato").ToString(), Eval("ValorContratoOriginal").ToString())%>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Name="SaldoContratual" Width="180px" Caption="Saldo Contratual"
                        VisibleIndex="31">
                        <PropertiesTextEdit DisplayFormatString="{0:c2}">
                        </PropertiesTextEdit>
                        <Settings AllowAutoFilter="False"></Settings>
                        <DataItemTemplate>
                            <%# getSaldoContratual()%>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="AnoTermino" Width="110px" Caption="Ano T&#233;rmino"
                        VisibleIndex="40">
                        <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle HorizontalAlign="Center">
                        </FilterCellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PodeExcluirContrato" Visible="False" VisibleIndex="44">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Visible="False" VisibleIndex="43">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ContratoPossuiVinculo" Visible="False" VisibleIndex="42">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager PageSize="80">
                </SettingsPager>
                <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True"
                    ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupedColumns="True" HorizontalScrollBarMode="Visible"></Settings>
                <SettingsText></SettingsText>

                <SettingsPopup>
                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>

                <StylesPopup>
                    <EditForm>
                        <Header Font-Bold="True">
                        </Header>
                    </EditForm>
                    <FilterBuilder>
                        <MainArea>
                        </MainArea>
                    </FilterBuilder>
                </StylesPopup>
                <Styles>
                    <CommandColumnItem>
                        <Paddings PaddingLeft="2px" PaddingRight="2px"></Paddings>
                    </CommandColumnItem>
                </Styles>
                <StylesFilterControl>
                    <Value>
                    </Value>
                </StylesFilterControl>
                <Templates>
                    <FooterRow>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td width="20">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/botoes/aditado.png">
                                        </dxe:ASPxImage>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel20" runat="server"
                                                Text="Contratos aditados">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </FooterRow>
                </Templates>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False"
                ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                        SupportsDisabledAttribute="True">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="" align="center"></td>
                                    <td style="width: 70px" align="center" rowspan="3">
                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
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
            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                Modal="True" CloseAction="None" ClientInstanceName="pcEncerramento" HeaderText="Encerramento do Contrato"
                ShowCloseButton="False" Width="550px" ID="pcEncerramento">
                <ClientSideEvents Shown="function(s, e) {
	txtComentarioEncerramento.SetFocus();
}"></ClientSideEvents>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                        SupportsDisabledAttribute="True">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Coment&#225;rios:" ClientInstanceName="lblComentarios"
                                        ID="lblComentarios">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ClientInstanceName="txtComentarioEncerramento"
                                        ID="txtComentarioEncerramento">
                                        <ClientSideEvents Init="function(s, e) {
	onInit_mmEncerramento(s, e);
}"></ClientSideEvents>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td height="10px">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right" style="padding-right: 10px; padding-top: 5px">
                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnEncerrar" Text="Encerrar" Width="95px"
                                                        ID="btnEncerrar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	gvDados.PerformCallback(&#39;C&#39;);
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 100px; padding-top: 5px">
                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelarEncerramento" Text="Cancelar"
                                                        Width="95px" ID="btnCancelarEncerramento">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    txtComentarioEncerramento.SetText(&quot;&quot;);
	pcEncerramento.Hide();
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes"
                ShowCloseButton="False" ID="pcDados" Width="1000px"
                Modal="True">
                <ClientSideEvents Shown="function(s, e) {
	if(TipoEdicao == 'I')
		abreNovo();
}"></ClientSideEvents>
                <ContentStyle>
                    <Paddings Padding="3px"></Paddings>
                </ContentStyle>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server"
                        SupportsDisabledAttribute="True">
                        <iframe id="frmDetalhes" frameborder="0" scrolling="no"
                            src="" width="100%"></iframe>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
