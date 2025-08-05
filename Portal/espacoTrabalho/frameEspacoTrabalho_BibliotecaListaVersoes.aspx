<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_BibliotecaListaVersoes.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_BibliotecaListaVersoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

    .mao
    {
        cursor:pointer;
        }
            .btn{
            text-transform: capitalize !important;
        }
        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>

        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable" 
            width="100%">
            <tr>
                <td>

 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
            KeyFieldName="codigoSequencialAnexo" AutoGenerateColumns="False" Width="100%" 
             ID="gvDados" 
            OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
     <Columns>
         <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CodigoAnexo" 
             VisibleIndex="0" Width="5%">
             <Settings AllowAutoFilter="False" />
             <DataItemTemplate>
                 <asp:ImageButton ID="imgDownload" runat="server"
                     ImageUrl="~/imagens/anexo/download.png" onclick="ImageButton1_Click" 
                     ToolTip="<%# Resources.traducao.fazer_o_download_do_arquivo %>" CssClass="mao" />
             </DataItemTemplate>
             <CellStyle HorizontalAlign="Center">
             </CellStyle>
         </dxwgv:GridViewDataTextColumn>
         <dxwgv:GridViewDataTextColumn Caption="Versão" FieldName="numeroVersao" 
             VisibleIndex="1" Width="5%">
             <PropertiesTextEdit DisplayFormatString="{0:n0}">
             </PropertiesTextEdit>
             <Settings AllowAutoFilter="False" />
             <HeaderStyle HorizontalAlign="Right" />
             <CellStyle HorizontalAlign="Right">
             </CellStyle>
         </dxwgv:GridViewDataTextColumn>
         <dxwgv:GridViewDataTextColumn Caption="Nome do Arquivo" FieldName="nomeArquivo" 
             VisibleIndex="2" Width="50%">
             <Settings AutoFilterCondition="Contains" />
         </dxwgv:GridViewDataTextColumn>
         <dxwgv:GridViewDataTextColumn Caption="Incluído Por" FieldName="NomeUsuario" 
             VisibleIndex="3" Width="25%">
             <Settings AutoFilterCondition="Contains" />
         </dxwgv:GridViewDataTextColumn>
         <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="dataVersao" 
             VisibleIndex="4" Width="15%">
             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
             </PropertiesDateEdit>
             <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True" />
             <HeaderStyle HorizontalAlign="Center" />
             <CellStyle HorizontalAlign="Center">
             </CellStyle>
         </dxwgv:GridViewDataDateColumn>
     </Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

     <ClientSideEvents Init="function(s, e) 
{
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 45;
       s.SetHeight(sHeight);
}" />

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" 
         VerticalScrollableHeight="230"></Settings>

</dxwgv:ASPxGridView>

                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 10px">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                         Text="Fechar" Width="90px" CssClass="btn">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal3();
}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
