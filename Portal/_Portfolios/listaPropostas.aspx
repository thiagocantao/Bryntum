<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaPropostas.aspx.cs" Inherits="_Portfolios_listaPropostas" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
<script type="text/javascript">
    function abreNovaProposta()
    {
        window.top.gotoURL('_Portfolios/wf_novaProposta.aspx?cwf=2', '_self');
        return true;       
    }
    
</script>

    <table>
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
            <td style="width: 5px; height: 10px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
        KeyFieldName="CodigoProjeto" OnCustomButtonCallback="gvDados_CustomButtonCallback"  Width="100%" OnCustomCallback="gvDados_CustomCallback">
        <SettingsPager Mode="ShowAllRecords" Visible="False">
        </SettingsPager>
        <Columns>
            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="50px">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluir" Text="Incluir nova proposta">
                        <Image AlternateText="Incluir" Url="~/imagens/botoes/incluirReg02.png" />
                    </dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ReadOnly="True" Visible="False"
                VisibleIndex="1">
                <EditFormSettings Visible="False" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="T&#237;tulo da Proposta" FieldName="NomeProjeto"
                VisibleIndex="1" Width="500px">
                <PropertiesTextEdit DisplayFormatString="{0}teste">
                </PropertiesTextEdit>
                <DataItemTemplate>
						<dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" NavigateUrl='<%# "javascript:gvDados.PerformCallback(\"CP" +Eval("CodigoProjeto") + "\")"%>'
							Text='<%# Eval("NomeProjeto")  %>' Width="100%" >
						</dxe:ASPxHyperLink>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatus" VisibleIndex="3"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="DescricaoCategoria"
                VisibleIndex="2" Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria" Visible="False" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CodigoStatusProjeto" Visible="False" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="codigoUsuarioLogado" Visible="False" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="codigoEntidadeLogada" Visible="False" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="function(s, e) {
        abreNovaProposta()
}" EndCallback="function(s, e) {
	window.top.gotoURL('_Portfolios/listaPropostas.aspx?P=S', '_parent');
}" />
        <Settings VerticalScrollBarMode="Visible" />
    </dxwgv:ASPxGridView>
            </td>
            <td style="width: 5px">
            </td>
        </tr>
    </table>
</asp:Content>

