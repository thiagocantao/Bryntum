<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmObrigacoesContratada.aspx.cs"
    Inherits="_Projetos_Administracao_frmObrigacoesContratada" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 5px; margin-top: 7px">
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="gvObrigacoes" runat="server" AutoGenerateColumns="False"
            ClientInstanceName="gvObrigacoes"  KeyFieldName="CodigoContrato;CodigoObrigacoesContratada"
            OnCellEditorInitialize="gvObrigacoes_CellEditorInitialize" OnCommandButtonInitialize="gvObrigacoes_CommandButtonInitialize"
            OnRowDeleting="gvObrigacoes_RowDeleting" OnRowInserting="gvObrigacoes_RowInserting"
            OnRowUpdating="gvObrigacoes_RowUpdating" Width="100%">
            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                AllowSort="False" ConfirmDelete="True" />
            <StylesPopup>
                <EditForm>
                <Content Font-Bold="False">
                </Content>
                    <Header Font-Bold="True">
                    </Header>
                    <MainArea Font-Bold="False" ></MainArea>
                </EditForm>
            </StylesPopup>
            <Styles>
                <Header Font-Bold="False">
                </Header>
                <HeaderPanel Font-Bold="False">
                </HeaderPanel>
                <TitlePanel Font-Bold="True">
                </TitlePanel>
                <Cell >
                </Cell>
                <EditForm >
                </EditForm>
                <EditFormCell >
                </EditFormCell>
            </Styles>
            <SettingsPager Mode="ShowAllRecords" Visible="False">
            </SettingsPager>
            <SettingsEditing Mode="PopupEditForm" />
            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                    AllowResize="True" VerticalOffset="-40" Width="450px" />
            </SettingsPopup>
            <SettingsText ConfirmDelete="Confirma a exclusão da obrigação?" PopupEditFormCaption="Obrigações Contratada"
                Title="Obrigações Contratada" />
            <ClientSideEvents CustomButtonClick="function(s, e) {
	}" EndCallback="function(s, e) {
}" />
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="80px"
                    ShowEditButton="true" ShowDeleteButton="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderTemplate>
                        <%# string.Format(@"<table style=""width:100%""><tr><td>{0}</td></tr></table>", (podeIncluir) ? @"<img style=""cursor: pointer"" onclick=""gvObrigacoes.AddNewRow();"" src=""../../imagens/botoes/incluirReg02.png"" alt=""Adicionar Obrigação.""/>" : "")%>
                    </HeaderTemplate>
                    <FooterTemplate>
                        <dxe:ASPxLabel ID="lblTotales" runat="server" ClientInstanceName="lblTotales"
                            Text="TOTAL:">
                        </dxe:ASPxLabel>
                    </FooterTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Ocorrência" FieldName="Ocorrencia" Name="Ocorrencia"
                    VisibleIndex="3">
                    <PropertiesComboBox ClientInstanceName="txtOcorrencia" MaxLength="50" Width="300px">
                        <ClientSideEvents Validation="function(s, e) {
	//onValidation_NumeroAditivo(s, e);
}" />
                        <Items>
                            <dxe:ListEditItem Text="No ato" Value="No ato" />
                            <dxe:ListEditItem Text="Em cada pagamento" Value="Em cada pagamento" />
                        </Items>
                        <ValidationSettings>
                            <RequiredField ErrorText="Informe a ocorrência" IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings Caption="Ocorrência:" CaptionLocation="Top" VisibleIndex="2" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Código Obrigação da Contratada" FieldName="CodigoObrigacoesContratada"
                    Name="CodigoObrigacoesContratada" VisibleIndex="4" Visible="False">
                    <PropertiesComboBox ClientInstanceName="txtCodigoObrigacao" MaxLength="3" TextField="DescricaoTipoObrigacoesContratada"
                        ValueField="CodigoObrigacoesContratada" Width="300px">
                        <ClientSideEvents Validation="function(s, e) {

}" />
                        <ValidationSettings>
                            <RequiredField ErrorText="Informe a obrigação!" IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings Caption="Obrigação:" CaptionLocation="Top" VisibleIndex="1" Visible="True" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Codigo Contrato" FieldName="CodigoContrato"
                    Name="CodigoContrato" Visible="False" VisibleIndex="2" ShowInCustomizationForm="False">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Obrigação da Contratada" FieldName="DescricaoTipoObrigacoesContratada"
                    ShowInCustomizationForm="False" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="135" />
            <StylesEditors>
                <Style ></Style>
            </StylesEditors>
            <Border BorderStyle="Solid" />
        </dxwgv:ASPxGridView>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </div>
    </form>
</body>
</html>
