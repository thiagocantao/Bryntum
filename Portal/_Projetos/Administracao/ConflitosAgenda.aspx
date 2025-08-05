<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConflitosAgenda.aspx.cs"
    Inherits="_Projetos_Administracao_ConflitosAgenda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        var recarregar = false;

        function OnAllCheckedChanged(s, e) {
            if (s.GetChecked())
                gvDados.SelectRows();
            else
                gvDados.UnselectRows();
        }

        function OnGridSelectionChanged(s, e) {
            cbAll.SetChecked(s.GetSelectedRowCount() == s.cpVisibleRowCount);
        }

        function fecharPopup() {
            window.top.fechaModal();
        }

        function MostraAgenda(codigoAcao) {
            var codigoEntidade = hfGeral.Get("CodigoEntidade");
            var codigoProjeto = hfGeral.Get("CodigoProjeto");
            var altura = screen.height - 265;
            var url = 'ConflitosAgenda_VisualizaAgenda.aspx?CP=' + codigoProjeto + '&CE=' + codigoEntidade + '&AL=' + altura + '&CA=' + codigoAcao;
            var frmModal = document.getElementById('frmModal');
            frmModal.style.height = altura + "px";
            frmModal.src = url;
            pcModal.Show();
        }

        function AtualizaConflitos() {
            gvDados.Refresh();
        }

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
                         ForeColor="Red" 
                        Text="ATENÇÃO: Foram detectados os seguintes conflitos de agenda das Atividades do Projeto com a Agenda Institucional">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                        DataSourceID="sdsDadosConflitosAgenda" KeyFieldName="CodigoAcao" Width="100%"
                         
                        OnCustomJSProperties="gvDados_CustomJSProperties" 
                        ondatabound="gvDados_DataBound">
                        <ClientSideEvents SelectionChanged="OnGridSelectionChanged" CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnVisualizaAgenda&quot;)
	{
		MostraAgenda(gvDados.GetRowKey(e.visibleIndex));
	}
}" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowSelectCheckbox="True" VisibleIndex="0"
                                Width="41px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <dxe:ASPxCheckBox ID="cbAll" runat="server"  ClientInstanceName="cbAll" ToolTip="Selecionar tudo"
                                        OnInit="cbAll_Init" TextAlign="Left">
                                        <ClientSideEvents CheckedChanged="OnAllCheckedChanged" />
                                    </dxe:ASPxCheckBox>
                                </HeaderTemplate>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAcao" Visible="False" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="Inicio" VisibleIndex="3"
                                Width="125px">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" 
                                    EditFormat="DateTime">
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesDateEdit>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="termino" VisibleIndex="4"
                                Width="125px">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" 
                                    EditFormat="DateTime">
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesDateEdit>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ação" FieldName="NomeAcao" VisibleIndex="5"
                                ReadOnly="True">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle BackColor="LightGray">
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="Responsavel" VisibleIndex="6"
                                ReadOnly="True">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle BackColor="LightGray">
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="1" Width="50px" 
                                Caption=" " ShowEditButton="true" >
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnVisualizaAgenda" Text="Visualizar Agenda">
                                        <Image Url="~/imagens/botoes/ConflitosAgenda_Visualizar.PNG" AlternateText="Visualizar Agenda">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollBarMode="Visible" />
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 20px">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnIgnorar" runat="server" Text="Ignorar Conflitos Selecionados"
                                    AutoPostBack="False"  OnClick="btnIgnorar_Click">
                                    <ClientSideEvents Click="function(s, e) {
	var possuiSelecionado = gvDados.GetSelectedRowCount() &gt; 0;
	if(!possuiSelecionado)
		window.top.mostraMensagem('Nenhuma ação foi selecionada.', 'atencao', true, false, null);
	e.processOnServer = possuiSelecionado;
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="padding-left: 30px">
                                <dxe:ASPxButton ID="btnFechar" runat="server" Text="Fechar" AutoPostBack="False"
                                     Width="130px">
                                    <ClientSideEvents Click="function(s, e) {
    var possuiSelecionado = gvDados.GetSelectedRowCount() &gt; 0;
	if(possuiSelecionado){
          window.top.mostraMensagem('Existem registros marcados!\n\nDeseja sair sem salvar?', 'confirmacao', true, true, fecharPopup);
	        }
    else
       window.top.fechaModal();

 
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="sdsDadosConflitosAgenda" runat="server" SelectCommandType="StoredProcedure"
            SelectCommand="[p_GetConflitosAgenda]" UpdateCommand="UPDATE [tai02_AcoesIniciativa]
   SET [Inicio] = @Inicio
      ,[Termino] = @Termino
 WHERE [CodigoAcao] = @CodigoAcao ">
            <SelectParameters>
                <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="CP" Type="Int32" />
                <asp:QueryStringParameter Name="CodigoEntidade" QueryStringField="CE" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="CodigoAcao" />
                <asp:Parameter Name="Inicio" />
                <asp:Parameter Name="Termino" />
            </UpdateParameters>
        </asp:SqlDataSource>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    </div>
        <dxpc:ASPxPopupControl ID="pcModal" runat="server" 
        ClientInstanceName="pcModal"
            HeaderText="Agenda" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" 
        AllowResize="True" CloseAction="CloseButton" Width="970px" 
        ShowHeader="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0" 
                        style="overflow:auto; padding:0px; margin:0px;" width="100%"></iframe></dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents CloseUp="function(s, e) {
	if(recarregar)
	{
		AtualizaConflitos();
	}
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
