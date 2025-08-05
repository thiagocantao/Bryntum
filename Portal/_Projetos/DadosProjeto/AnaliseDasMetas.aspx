<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnaliseDasMetas.aspx.cs"
    Inherits="_Projetos_DadosProjeto_AnaliseDasMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <!--correção de bug--> 
        <table>
            <tr>
                <td style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px; padding-right: 5px;">

                    <dxwgv:ASPxGridView ID="gvMeta" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMeta"
                        KeyFieldName="CodigoMetaOperacional" Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" AllowAutoFilter="false" AutoExpandAllGroups="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	e.processOnServer = false;
	gvMetas_FocusedRowChanged(s,e);
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="35px" Caption=" A&#231;&#227;o"
                                Visible="False" VisibleIndex="1">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="imgEditar" Text="Atualizar Resultados de Desempenho">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Meta" Name="Meta" Caption="Meta" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeIndicador" Width="40%" Caption="Indicador"
                                VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Desempenho" Name="Desempenho" Width="12%"
                                Caption="Status" VisibleIndex="3">
                                <Settings AllowAutoFilter="False" />
                                <DataItemTemplate>
                                    <img alt='' src="../../imagens/<%# Eval("Desempenho") %>.gif" />
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CasasDecimais" Visible="False" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeMedida" Visible="False" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo de Indicador" FieldName="DescTipoIndicador"
                                ShowInCustomizationForm="False" VisibleIndex="0" Width="150px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="130" ShowFilterRow="False" ShowFilterBar="Hidden"
                            ShowGroupPanel="False" ShowHeaderFilterBlankItems="False"></Settings>
                        <Templates>
                            <FooterRow>
                                <table cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <img id="IMG1" onclick="return IMG1_onclick()" src="../../imagens/verdeMenor.gif"
                                                    alt="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVerde" runat="server" EnableViewState="False"
                                                    Text="Satisfatório"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <img src="../../imagens/amareloMenor.gif" alt="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAmarelo" runat="server" EnableViewState="False"
                                                    Text="Atenção"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <img src="../../imagens/AzulMenor.gif" alt="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" EnableViewState="False"
                                                    Text="Acima da Meta"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td style="width: 25px">
                                                <img src="../../imagens/vermelhoMenor.gif" alt="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVermelho" runat="server" EnableViewState="False"
                                                    Text="Crítico"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>
                    </dxwgv:ASPxGridView>

                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; padding-right: 5px; padding-left: 5px">

                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPeriodo" ClientVisible="False"
                        KeyFieldName="CodigoIndicador" AutoGenerateColumns="False" Width="100%"
                        ID="gvPeriodo" OnCustomButtonInitialize="gvPeriodo_CustomButtonInitialize"
                        OnCustomCallback="gvPeriodo_CustomCallback">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	e.processOnServer = false;
	gvPeriodo_FocusedRowChanged(s,e);
}"
                            CustomButtonClick="function(s, e) {
	gvPeriodo_FocusedRowChanged(s,e);
	OnClick_CustomGvPeriodo(s, e);
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="20%"
                                Caption="A&#231;&#227;o" VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomNovo" Text="Nova An&#225;lise no Per&#237;odo">
                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomEdit" Text="Editar An&#225;lise no Per&#237;odo">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomExcluir" Text="Excluir An&#225;lise no Per&#237;odo">
                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomDetalhe" Text="Visualizar An&#225;lise no Per&#237;odo">
                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Periodo" ShowInCustomizationForm="True"
                                Name="Periodo" Width="20%" Caption="Per&#237;odo" VisibleIndex="1">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="MetaMes" ShowInCustomizationForm="True"
                                Name="MetaMes" Width="10%" Caption="Meta" VisibleIndex="2">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ResultadoMes" ShowInCustomizationForm="True"
                                Name="ResultadoMes" Width="10%" Caption="Resultado" VisibleIndex="3">
                                <PropertiesTextEdit NullText="-" DisplayFormatString="{0:n2}" NullDisplayText="-"
                                    EncodeHtml="False">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Desempenho" ShowInCustomizationForm="True"
                                Name="Desempenho" Width="10%" Caption="Status" VisibleIndex="4">
                                <DataItemTemplate>
                                    <img alt='' src="../../imagens/<%# Eval("Desempenho") %>.gif" />
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TipoEdicao" ShowInCustomizationForm="True"
                                Visible="False" VisibleIndex="5">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="MetaAcumuladaAno" ShowInCustomizationForm="True"
                                Name="MetaAcumuladaAno" Width="10%" Caption="Meta Acumulada" VisibleIndex="5">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ResultadoAcumuladoAno" ShowInCustomizationForm="True"
                                Name="ResultadoAcumuladoAno" Width="10%" Caption="Resultado Acumulado" VisibleIndex="6">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DesempenhoAculumado" ShowInCustomizationForm="True"
                                Name="DesempenhoAculumado" Width="10%" Caption="Status Acumulado" VisibleIndex="7">
                                <DataItemTemplate>
                                    <img alt="" src='../../imagens/<%# Eval("DesempenhoAculumado") %>.gif' />
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    &nbsp;
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowTitlePanel="True" ShowFooter="True" VerticalScrollBarMode="Visible"
                            VerticalScrollableHeight="145"></Settings>
                        <SettingsText Title="An&#225;lises da Meta Selecionada"></SettingsText>
                        <Styles>
                            <Header SortingImageSpacing="5px" ImageSpacing="5px">
                            </Header>
                        </Styles>
                        <StylesEditors>
                            <ProgressBar Height="25px">
                            </ProgressBar>
                        </StylesEditors>
                        <Templates>
                            <FooterRow>
                                <table cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <img id="IMG2" onclick="return IMG1_onclick()" src="../../imagens/verdeMenor.gif"
                                                    alt="" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 3px">
                                                <asp:Label ID="lblVerde0" runat="server" EnableViewState="False"
                                                    Text="Satisfatório"></asp:Label>
                                            </td>
                                            <td>
                                                <img src="../../imagens/amareloMenor.gif" alt="" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 3px">
                                                <asp:Label ID="lblAmarelo0" runat="server" EnableViewState="False"
                                                    Text="Atenção"></asp:Label>
                                            </td>
                                            <td>
                                                <img src="../../imagens/AzulMenor.gif" alt="" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 3px">
                                                <asp:Label ID="Label2" runat="server" EnableViewState="False"
                                                    Text="Acima da meta"></asp:Label>
                                            </td>
                                            <td>
                                                <img src="../../imagens/vermelhoMenor.gif" alt="" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 3px">
                                                <asp:Label ID="lblVermelho0" runat="server" EnableViewState="False"
                                                    Text="Crítico"></asp:Label>
                                            </td>
                                            <td>
                                                <img src="../../imagens/brancoMenor.gif" alt="" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 3px">
                                                <asp:Label ID="lblVermelho1" runat="server" EnableViewState="False"
                                                    Text="Sem informação"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>
                    </dxwgv:ASPxGridView>

                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="None"
            HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="720px">
            <ClientSideEvents Closing="function(s, e) {
		gvPeriodo.PerformCallback();
}"></ClientSideEvents>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe" Width="720px"
                        ID="pnCallbackDetalhe" OnCallback="pnCallbackDetalhe_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	//Ao finalizar la pesqui&#231;a de los dados en el pnCallback, cargo el pcDados
	//ja con los dados prenchidos.
	End_pnCallbackDados();
}"></ClientSideEvents>
                        <Styles>
                            <LoadingPanel HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                            </LoadingPanel>
                        </Styles>
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxLabel runat="server" Text="An&#225;lises:"
                                                    ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="mmAnalise"
                                                    ID="mmAnalise">
                                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE" Display="Dynamic">
                                                        <RequiredField ErrorText="Campo Obrigat&#243;rio!" IsRequired="True"></RequiredField>
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo"
                                                    Font-Bold="True" ForeColor="#999999">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <dxe:ASPxLabel runat="server" Text="Recomenda&#231;&#245;es:"
                                                    ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="mmRecomendacoes"
                                                    ID="mmRecomendacoes">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                    <ValidationSettings ErrorDisplayMode="None">
                                                    </ValidationSettings>
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="lblContadorMemo0" runat="server" ClientInstanceName="lblContadorMemo0"
                                                    Font-Bold="True" ForeColor="#999999">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left">
                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="padding-right: 2px; height: 16px">
                                                                                <dxe:ASPxLabel runat="server" Text="Inclus&#227;o:" ClientInstanceName="lblInclusao"
                                                                                    Font-Bold="True" ForeColor="DimGray" ID="lblCaptionInclusao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="height: 16px">
                                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblDataInclusao"
                                                                                    ForeColor="DimGray" ID="lblDataInclusao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 10px; height: 16px"></td>
                                                                            <td style="padding-right: 2px">
                                                                                <dxe:ASPxLabel runat="server" Text="Incluido por:" ClientInstanceName="lblIncluidoPor"
                                                                                    Font-Bold="True" ForeColor="DimGray" ID="lblCaptionIncluidoPor">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="height: 16px">
                                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblUsuarioInclusao"
                                                                                    ForeColor="DimGray" ID="lblUsuarioInclusao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 2px"></td>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td style="padding-right: 2px"></td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 2px">
                                                                                <dxe:ASPxLabel runat="server" Text="Ultima Altera&#231;&#227;o:" ClientInstanceName="lblUltimaAlteracao"
                                                                                    Font-Bold="True" ForeColor="DimGray" ID="lblCaptionUltimaAlteracao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblDataAlteracao"
                                                                                    ForeColor="DimGray" ID="lblDataAlteracao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td></td>
                                                                            <td style="padding-right: 2px">
                                                                                <dxe:ASPxLabel runat="server" Text="Alterado por:" ClientInstanceName="lblAlteradoPor"
                                                                                    Font-Bold="True" ForeColor="DimGray" ID="lblCaptionAlteradoPor">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblUsuarioAlteracao"
                                                                                    ForeColor="DimGray" ID="lblUsuarioAlteracao">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td style="width: 100px" valign="top">
                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                                                    Width="100%" ID="btnSalvar">
                                                                    <ClientSideEvents Click="function(s, e) {
	On_ClickSalvar(s, e);
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td style="width: 10px;"></td>
                                                            <td style="width: 100px" valign="top">
                                                                <dxe:ASPxButton runat="server" CommandArgument="btnCancelar" Text="Fechar" Width="100%"
                                                                    ID="btnCancelar">
                                                                    <ClientSideEvents Click="function(s, e) {
	hfAcao.Set('acao', 'Cancelar');
	On_ClickCancelar(s, e);
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfAcao" ID="hfAcao">
                                </dxhf:ASPxHiddenField>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle HorizontalAlign="Left" Font-Bold="True"></HeaderStyle>
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
