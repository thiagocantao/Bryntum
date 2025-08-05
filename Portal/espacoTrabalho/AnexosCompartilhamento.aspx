<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnexosCompartilhamento.aspx.cs" Inherits="espacoTrabalho_AnexosCompartilhamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
	color: Black;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px 5px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxgvCommandColumn
{
	padding: 2px;
}
.dxICheckBox 
{
    cursor: default;
	margin: auto;
	display: inline-block;
	vertical-align: middle;
}
.dxWeb_edtCheckBoxUnchecked {
	background-position: -41px -99px;
}

.dxWeb_edtCheckBoxChecked,
.dxWeb_edtCheckBoxUnchecked,
.dxWeb_edtCheckBoxGrayed,
.dxWeb_edtCheckBoxCheckedDisabled,
.dxWeb_edtCheckBoxUncheckedDisabled,
.dxWeb_edtCheckBoxGrayedDisabled {
    background-repeat: no-repeat;
    background-color: transparent;
    width: 15px;
    height: 15px;
}        .btn{
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" 
                        oncallback="callback_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_Msg != &quot;&quot;)
        {
                            window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
                            window.top.fechaModal3();
        }
        else
        {
                            window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
        }
}" />
                    </dxcb:ASPxCallback>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvAnexos" 
                        KeyFieldName="CodigoAnexo" AutoGenerateColumns="False" Width="100%" 
                         ID="gvAnexos" 
                       ><Columns>
<dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" ShowInCustomizationForm="True" Width="35px" 
                                Caption=" " VisibleIndex="0">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<HeaderTemplate>
                                                <input onclick="gvAnexos.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                    type="checkbox" />
                                            
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="Nome" ShowInCustomizationForm="True" Caption="Arquivo" 
                                VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Incluído Por" FieldName="NomeUsuario" 
                                VisibleIndex="2" Width="180px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoAnexo" 
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Visible"></Settings>

<SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>

<Styles>
<SelectedRow BackColor="Transparent" ForeColor="Black"></SelectedRow>
</Styles>
</dxwgv:ASPxGridView>

                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 10px">
                    <table>
                        <tr>
                            <td style="padding-right: 10px">
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                                     Text="Salvar" Width="90px" CssClass="btn">
                                    <ClientSideEvents Click="function(s, e) {
                                    if(gvAnexos.GetSelectedRowCount() > 0)
	                                    callback.PerformCallback();
                                    else
                                        window.top.mostraMensagem('Nenhum anexo foi selecionado!', 'Atencao', true, false, null);
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" 
                                     Text="Fechar" Width="90px" CssClass="btn">
                                    <ClientSideEvents Click="function(s, e) {
	        window.top.fechaModal3();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
