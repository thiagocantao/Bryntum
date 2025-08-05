<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auditoria_Insercao.aspx.cs" Inherits="administracao_auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            width: 10px;
            height: 10px;
        }
        .style4
        {
            height: 10px;
        }
        .style5
        {
            width: 115px;
        }
        .style6
        {
            width: 185px;
        }
        .style7
        {
            width: 250px;
        }
        .style8
        {
            width: 95px;
        }
        .iniciaisMaiusculas{
            text-transform:capitalize !important
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <table cellspacing="0" class="style1" cellpadding="0">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                <table cellspacing="0" class="style1" cellpadding="0">
                    <tr>
                        <td class="style7">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Nome da Tabela:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style5">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Tipo de Operação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style8">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                Text="Data:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Usuário Responsável:">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="style6">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="IP da Máquina:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtTabela" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="style5" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtTipoOperacao" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="style8" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtData" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtUsuario" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td class="style6">
                            <dxe:ASPxTextBox ID="txtMaquina" runat="server" ClientEnabled="False" 
                                 ForeColor="Black" Width="100%">
                                <DisabledStyle BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    </table>
            </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style4">
                </td>
            <td class="style3">
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
    
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
             Width="100%">
            <Columns>
                  <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="50px">
                    <HeaderTemplate>
                        <table>
    <tr>
        <td align="center">
            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" 
                                                            ClientInstanceName="menu" 
                ItemSpacing="5px" onitemclick="menu_ItemClick" 
                                                            oninit="menu_Init">
                <Paddings Padding="0px" />
                <Items>
                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                        <Image Url="~/imagens/botoes/incluirReg02.png">
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                        <Items>
                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                <Image Url="~/imagens/menuExportacao/xls.png">
                                </Image>
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                </Image>
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                </Image>
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" 
                                                                            ClientVisible="False">
                                <Image Url="~/imagens/menuExportacao/html.png">
                                </Image>
                            </dxm:MenuItem>
                        </Items>
                        <Image Url="~/imagens/botoes/btnDownload.png">
                        </Image>
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                        <Items>
                            <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                 <Image IconID="save_save_16x16">
                                </Image>
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                <Image IconID="actions_reset_16x16">
                                </Image>
                            </dxm:MenuItem>
                        </Items>
                        <Image Url="~/imagens/botoes/layout.png">
                        </Image>
                    </dxm:MenuItem>
                </Items>
                <ItemStyle Cursor="pointer">
                <HoverStyle>
                    <Border BorderStyle="None" />
                </HoverStyle>
                <Paddings Padding="0px" />
                </ItemStyle>
                <SubMenuItemStyle BackColor="White" Cursor="pointer">
                    <SelectedStyle>
                        <Border BorderStyle="None" />
                    </SelectedStyle>
                </SubMenuItemStyle>
                <Border BorderStyle="None" />
            </dxm:ASPxMenu>
        </td>
    </tr>
</table>

                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NomeCampo" VisibleIndex="1" 
                    Width="220px" Caption="Nome do Campo">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Valor" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents Init="function(s, e) {
     var altura = Math.max(0, document.documentElement.clientHeight) - 115;
     s.SetHeight(altura);
}" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" />
        </dxwgv:ASPxGridView>
    
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1" 
                    onrenderbrick="ASPxGridViewExporter1_RenderBrick">
<Styles>
<Default ></Default>

<Header ></Header>

<Cell ></Cell>

<GroupFooter Font-Bold="True" ></GroupFooter>

<Title Font-Bold="True" ></Title>
</Styles>
</dxwgv:ASPxGridViewExporter>

            </td>
            <td class="style2">
                </td>
        </tr>
        <tr>
            <td class="style2">
               </td>
            <td align="right" style="padding-top: 5px">
                                <dxe:ASPxButton ID="btnCancelar" runat="server"  CssClass="iniciaisMaiusculas"
                                    ClientInstanceName="btnCancelar"  
                                    Text="Fechar" Width="100px" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                </dxe:ASPxButton>
 
                </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
