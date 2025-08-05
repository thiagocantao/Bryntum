<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesTSPopupHistoricoApontamentoOC.aspx.cs" Inherits="espacoTrabalho_DetalhesTSPopupHistoricoApontamentoOC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Detalhes</title>
    <script type="text/javascript" language="javascript">
        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter2();
        }
        window.top.SetBotaoSalvarVisivel2(false);
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <table cellpadding="0" cellspacing="0" class="dx-justification">
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Recurso:">
                    </dxcp:ASPxLabel>
                </td>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Unidade:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <dxcp:ASPxTextBox ID="txtRecurso" runat="server" ClientInstanceName="txtRecurso" Width="100%" ReadOnly="True">
                        <ReadOnlyStyle BackColor="#EBEBEB">
                        </ReadOnlyStyle>
                    </dxcp:ASPxTextBox>
                </td>
                <td style="padding: 5px 5px 5px 0px">
                    <dxcp:ASPxTextBox ID="txtUnidade" runat="server" ClientInstanceName="txtUnidade" Width="100%" ReadOnly="True">
                        <ReadOnlyStyle BackColor="#EBEBEB">
                        </ReadOnlyStyle>
                    </dxcp:ASPxTextBox>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAtribuicao" AutoGenerateColumns="False" Width="100%" ID="gvDados" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomSummaryCalculate="gvDados_CustomSummaryCalculate" OnSummaryDisplayText="gvDados_SummaryDisplayText" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">

            <Columns>
                <dxtv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="2" FieldName="NomeUsuarioApontamento">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="6" FieldName="SiglaStatusAnalise" Visible="False">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataDateColumn Caption="Data" FieldName="DataApontamento" ShowInCustomizationForm="False" VisibleIndex="1" Width="28%">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                    </PropertiesDateEdit>
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataTextColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoRealInformado" ShowInCustomizationForm="True" VisibleIndex="3">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="CustoUnitarioRealInformado" ShowInCustomizationForm="True" VisibleIndex="4">
                    <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Realizado" FieldName="CustoRealInformado" ShowInCustomizationForm="True" VisibleIndex="5">
                    <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="Cancelar" Name="colunaCancelar" VisibleIndex="0">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnCancelar">
                            <Image Url="~/imagens/botoes/aprovarReprovar.png">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxtv:GridViewCommandColumn>
                <dxtv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusAnalise" VisibleIndex="7">
                </dxtv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowHeaderFilter="False" AllowSort="False" AllowAutoFilter="False" AllowGroup="False"></SettingsBehavior>
            <ClientSideEvents Init="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight) - 70;
s.SetHeight(sHeight);
}" CustomButtonClick="function(s, e) {
	if(e.buttonID == 'btnCancelar')
               {
                     
var funcObj = { funcaoClickOK: function () { alert('aqui será executado o metodo de cancelamento da aprovação'); } }
            window.top.mostraConfirmacao('Deseja cancelar este lançamento que foi aprovado por você anteriormente? Esta operação não poderá ser desfeita!', function () { funcObj['funcaoClickOK']() }, null);

                }
}" />
            <SettingsPager PageSize="100" Mode="ShowAllRecords">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" ShowHeaderFilterListBoxSearchUI="False" ShowFooter="True"></Settings>

            <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>

            <TotalSummary>
                <dxtv:ASPxSummaryItem DisplayFormat="c2" FieldName="CustoRealInformado" ShowInColumn="CustoRealInformado" ShowInGroupFooterColumn="CustoRealInformado" SummaryType="Custom" ValueDisplayFormat="c2" />
            </TotalSummary>

            <Styles>
                <AlternatingRow Enabled="False">
                </AlternatingRow>
                <FocusedRow ForeColor="Black">
                </FocusedRow>
                <Cell>
                    <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </Cell>
            </Styles>
            <BorderLeft BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
            <BorderTop BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
            <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
