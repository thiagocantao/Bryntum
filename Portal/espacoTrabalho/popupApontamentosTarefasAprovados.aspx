<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupApontamentosTarefasAprovados.aspx.cs" Inherits="espacoTrabalho_popupApontamentosTarefasAprovados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Detalhes</title>
    <script type="text/javascript" language="javascript">
        var comando;
        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter2();
            window.top.fechaModalComFooter();

        }
        window.top.SetBotaoSalvarVisivel2(false);
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoApontamentoAtribuicao" AutoGenerateColumns="False" Width="100%" ID="gvDados" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomSummaryCalculate="gvDados_CustomSummaryCalculate" OnSummaryDisplayText="gvDados_SummaryDisplayText" OnCustomCallback="gvDados_CustomCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">

            <Columns>
                <dxtv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="2" FieldName="NomeUsuarioApontamento">
                     <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="10" FieldName="SiglaStatusAnalise" Visible="False">
                 <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataDateColumn Caption="Data" FieldName="DataApontamento" ShowInCustomizationForm="False" VisibleIndex="1">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                    </PropertiesDateEdit>
                     <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="CustoUnitarioRealInformado" ShowInCustomizationForm="True" VisibleIndex="8">
                    <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    </PropertiesSpinEdit>
                     <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Realizado" FieldName="CustoRealInformado" ShowInCustomizationForm="True" VisibleIndex="9">
                    <PropertiesSpinEdit DisplayFormatString="c" NumberFormat="Currency">
                    </PropertiesSpinEdit>
                    <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="Cancelar" Name="colunaCancelar" VisibleIndex="0" Width="60px">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnCancelar">
                            <Image Url="~/imagens/botoes/aprovarReprovar.png">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxtv:GridViewCommandColumn>
                <dxtv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusAnalise" VisibleIndex="11">
                    <Settings AllowAutoFilter="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Projeto" VisibleIndex="4" ShowInCustomizationForm="True" FieldName="NomeProjeto">
                <Settings  AllowAutoFilter="True" ShowFilterRowMenu="True"/>
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Tarefa" VisibleIndex="5" ShowInCustomizationForm="True" FieldName="NomeTarefa">
                    <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Recurso" VisibleIndex="6" ShowInCustomizationForm="True" FieldName="NomeRecurso">
                    <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoRealInformado" VisibleIndex="7">
                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="F2">
                    </PropertiesSpinEdit>
                    <Settings AllowAutoFilter="True" />
                </dxtv:GridViewDataSpinEditColumn>
            </Columns>
            <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowHeaderFilter="False" AllowSort="False" AllowAutoFilter="False" AllowGroup="False"></SettingsBehavior>
            <ClientSideEvents Init="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight);
s.SetHeight(sHeight);
}" CustomButtonClick="function(s, e) {
	if(e.buttonID == 'btnCancelar')
               {
                     
var funcObj = { funcaoClickOK: function () { s.PerformCallback( s.GetRowKey(s.GetFocusedRowIndex())) } }
            window.top.mostraConfirmacao('Deseja cancelar este lançamento que foi aprovado por você anteriormente? Esta operação não poderá ser desfeita!', function () { funcObj['funcaoClickOK']() }, null);

                }
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
        if(comando == 'CUSTOMCALLBACK')
        {
s.Refresh();
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
