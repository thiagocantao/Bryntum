<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelecaoTarefasEquipe.aspx.cs"
    Inherits="_PDA_SelecaoProdutosConsultor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <div id="divConteudo">
            <table>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" Width="100%" AutoGenerateColumns="False"
                            ClientInstanceName="gvDados"
                            KeyFieldName="CodigoAtribuicao" 
                            oncustomcallback="gvDados_CustomCallback">
                           
                            <Columns>
                                <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                                    Width="30px" Caption=" ">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="NomeTarefa" VisibleIndex="1" 
                                    Caption="Tarefa">
                                    <EditFormSettings Visible="False" />
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Equipe" FieldName="NomeEquipe" 
                                    VisibleIndex="4" Width="220px">
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxtv:GridViewDataDateColumn Caption="Início" FieldName="Inicio" 
                                    VisibleIndex="2" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataDateColumn Caption="Término" FieldName="Termino" 
                                    VisibleIndex="3" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoEquipe" Visible="False" 
                                    VisibleIndex="5">
                                </dxtv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings VerticalScrollableHeight="350" VerticalScrollBarMode="Auto" 
                                ShowFilterRow="True" />

                            <Styles>
                                <TitlePanel Font-Bold="True" Font-Size="10pt">
                                </TitlePanel>
                            </Styles>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 10px">
                        <table>
                            <tr>
                                <td style="padding-right: 10px">
                        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" 
                             Text="Salvar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
	if(confirm('Confirma a obtenção da(s) tarefa(s)?'))
	gvDados.PerformCallback();
}" />
                            <Paddings Padding="0px" />


<Paddings Padding="0px"></Paddings>
                        </dxe:ASPxButton>
                                </td>
                                <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                             Text="Fechar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
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
    </div>
    </form>
</body>
</html>
