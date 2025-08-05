<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configuracoes.aspx.cs" Inherits="_Projetos_Boletim_configuracoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 10px;
        }
        .style4
        {
            width: 10px;
            height: 10px;
        }
        .style5
        {
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellspacing="1" class="style2">
            <tr>
                <td class="style4">
                </td>
                <td class="style5">
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>

 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoUsuario" 
                        AutoGenerateColumns="False" Width="100%"  
                        ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback">
     <Columns>
         <dxwgv:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" 
             VisibleIndex="0" Width="35px">
             <HeaderStyle HorizontalAlign="Center" />
             <CellStyle HorizontalAlign="Center">
             </CellStyle>
             <HeaderTemplate>
                 <input onclick="gvDados.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                    type="checkbox" />
             </HeaderTemplate>
         </dxwgv:GridViewCommandColumn>
         <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomeUsuario" 
             VisibleIndex="1" Width="200px">
         </dxwgv:GridViewDataTextColumn>
         <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" 
             VisibleIndex="2">
         </dxwgv:GridViewDataTextColumn>
     </Columns>

<SettingsBehavior AllowSort="False" AllowSelectByRowClick="true"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowTitlePanel="True"></Settings>

<SettingsText  
         Title="Selecione os usuários com acesso aos boletins da unidade"></SettingsText>
     <Styles>
         <TitlePanel Font-Bold="True">
         </TitlePanel>
     </Styles>
</dxwgv:ASPxGridView>

                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                </td>
                <td class="style5">
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    callbackSalvar.PerformCallback();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



                </td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
