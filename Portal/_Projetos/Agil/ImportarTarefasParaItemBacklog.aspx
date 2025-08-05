<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportarTarefasParaItemBacklog.aspx.cs" Inherits="_Projetos_Agil_ImportarTarefasParaItemBacklog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .dxbButton_MaterialCompact.dxbTSys {
    width: 96px;
    text-transform:capitalize !important;
    letter-spacing: 0.01em;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoTarefa" ClientInstanceName="gvTarefas" Width="100%" ID="gvTarefas">
                <ClientSideEvents Init="function(s, e) {
	    var height = Math.max(0, document.documentElement.clientHeight) - 75;
                      s.SetHeight(height);
}" SelectionChanged="function(s, e) {
	//alert(s.GetSelectedRowCount());
            if(s.GetSelectedRowCount() &gt; 0 )
            {
                    btnSelecionar.SetEnabled(true);
            }
            else
           {
                   btnSelecionar.SetEnabled(false);
           }
}" />
                <Templates>
                    <GroupRowContent>
                        <%# Container.GroupText == "0" ? Resources.traducao.programasDoProjetos_selecionados : Resources.traducao.programasDoProjetos_dispon_veis %>
                    </GroupRowContent>
                </Templates>

                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto"></Settings>

                <SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>

                <SettingsPopup>
                    <HeaderFilter MinHeight="275px" MinWidth="600px"></HeaderFilter>
                </SettingsPopup>
                <Columns>
                    <dxcp:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" Width="40px" Caption=" " VisibleIndex="0"></dxcp:GridViewCommandColumn>
                    <dxcp:GridViewDataTextColumn FieldName="CodigoTarefa" ShowInCustomizationForm="True" Caption="CodigoTarefa" VisibleIndex="1" Visible="False">
                        <PropertiesTextEdit EncodeHtml="False"></PropertiesTextEdit>

                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="SequenciaTarefa" ShowInCustomizationForm="True" VisibleIndex="2" Caption="#" Width="80px"></dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="NomeTarefa" ShowInCustomizationForm="True" Caption="Tarefa" VisibleIndex="3"></dxcp:GridViewDataTextColumn>
                    <dxtv:GridViewDataDateColumn Caption="Término Previsto" FieldName="Termino" ShowInCustomizationForm="True" VisibleIndex="4" Width="175px">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                        </PropertiesDateEdit>
                        <Settings AllowHeaderFilter="True" />
                    </dxtv:GridViewDataDateColumn>
                </Columns>
            </dxcp:ASPxGridView>


        </div>
        <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
        if(s.cpErro !== '')
       {
                  window.top.mostraMensagem(s.cpErro, 'erro', true, false, null, null);
       }
        else
        {
                  if(s.cpSucesso !== '')
                 {
                             window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000);
                             window.top.fechaModal();
                 }
         }
}" />
        </dxcp:ASPxCallback>
        <div style="display: flex; flex-direction: row-reverse">
            <div style="padding-top: 10px">
                <dxcp:ASPxButton ID="btnFechar" runat="server" Style="text-transform: capitalize" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar">
                    <ClientSideEvents Click="function(s, e) {
   window.top.fechaModal(); 
}" />
                </dxcp:ASPxButton>
            </div>
            <div style="padding-right:5px;padding-top:10px">
                <dxcp:ASPxButton ID="btnSelecionar" runat="server" Style="text-transform: capitalize" AutoPostBack="False" ClientInstanceName="btnSelecionar" Text="Selecionar" ClientEnabled="False">
                    <ClientSideEvents Click="function(s, e) {
   callbackTela.PerformCallback(); 
}" />
                </dxcp:ASPxButton>
            </div>
        </div>
    </form>
</body>
</html>
