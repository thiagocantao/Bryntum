<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agil_FrameChecklist.aspx.cs" Inherits="_Projetos_DadosProjeto_agil_FrameChecklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    </style>
    <script type="text/javascript">
        var comando;

        function excluirTarefa(valor) {
            callbackTela.PerformCallback(valor);
        }
    </script>
    <script src="../../scripts/_Strings.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <dxcp:ASPxTextBox ID="txtIncluiCheckList" runat="server" ClientInstanceName="txtIncluiCheckList" Width="100%" NullText="Informe aqui o nome da tarefa de checklist e pressione ENTER para incluí-la na lista" AutoCompleteType="Disabled">
                        <ClientSideEvents KeyDown="function(s, e) {
       if ((e.htmlEvent.keyCode == 13) &amp;&amp; 
             (trim(s.GetText()).length &gt; 0)) 
      { 
                            callbackTela.PerformCallback(-1);
                            txtIncluiCheckList.SetText(null);                            
                            txtIncluiCheckList.Focus();
                            
      }
}" />
                        <border bordercolor="Black"></border>
                    </dxcp:ASPxTextBox>
                </td>
            </tr>
        </table>

        
        <dxcp:ASPxCallbackPanel ID="callbackTela" ClientInstanceName="callbackTela" runat="server" OnCallback="ASPxCallbackPanel1_Callback" Width="100%">
            <ClientSideEvents EndCallback="function(s, e) {
          if(s.cpAcaoExecutada ==&quot;Incluir&quot; || s.cpAcaoExecutada == &quot;Excluir&quot; )
        {
                txtIncluiCheckList.Focus();
                gvDados.Refresh();                 
        }
}" />
            <PanelCollection>
<dxcp:PanelContent runat="server">
    <dxtv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" Font-Names="Verdana" Font-Size="8pt" KeyFieldName="CodigoTarefaChecklist" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnHtmlEditFormCreated="gvDados_HtmlEditFormCreated" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnRowUpdating="gvDados_RowUpdating" Width="100%">
        <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnExcluir&quot;)
     {  
            gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoTarefaChecklist', excluirTarefa);
     }	
}
" EndCallback="function(s, e) {
        if(comando == 'REFRESH')
         {                
                txtIncluiCheckList.Focus();
         }
}" Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 75);
}" SelectionChanged="function(s, e) {
var key = s.GetRowKey(e.visibleIndex);
    if (e.isSelected) {
       key  += '|SELECT';
    }
     else
    {
       key  += '|UNSELECT';
   }
   callbackTela1.PerformCallback(key);
 
}" />
        <SettingsPager Mode="ShowAllRecords" Visible="False">
        </SettingsPager>
        <SettingsEditing Mode="Inline">
        </SettingsEditing>
        <Settings HorizontalScrollBarMode="Visible" ShowColumnHeaders="False" ShowGroupButtons="False" VerticalScrollBarMode="Visible" />
        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" />
        <SettingsCommandButton>
            <CancelButton>
                <Image Url="~/imagens/botoes/cancelar.PNG">
                </Image>
            </CancelButton>
        </SettingsCommandButton>
        <SettingsPopup>
            <HeaderFilter MinHeight="140px">
            </HeaderFilter>
        </SettingsPopup>
        <Columns>
            <dxtv:GridViewCommandColumn AllowDragDrop="False" ButtonRenderMode="Image" ButtonType="Image" ShowCancelButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="3" Width="10%">
                <CustomButtons>
                    <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Editar">
                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                        </Image>
                    </dxtv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxtv:GridViewCommandColumn>
            <dxtv:GridViewDataTextColumn Caption="Tarefa" FieldName="DetalheChecklist" ShowInCustomizationForm="True" ToolTip="qualquer alteração no checklist é salva automaticamente, não sendo necessário acionar botão 'Salvar'" VisibleIndex="2" Width="80%">
                <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" ShowInFilterControl="False" />
                <EditFormSettings Caption="Tarefa" CaptionLocation="Top" Visible="True" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0">
            </dxtv:GridViewCommandColumn>
        </Columns>
        <Border BorderColor="#009900" BorderWidth="1px" />
    </dxtv:ASPxGridView>
                </dxcp:PanelContent>
</PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <dxcp:ASPxCallback ID="callbackTela1" runat="server" ClientInstanceName="callbackTela1" OnCallback="callbackTela1_Callback">
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
