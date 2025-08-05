<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanilhaOrcamentoAprovadoCA.aspx.cs" Inherits="_VisaoMaster_PlanilhaOrcamentoAprovadoCA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script language="javascript" type="text/javascript">

        window.moveTo(0, 0);
        if (document.all) {
            top.window.resizeTo(screen.availWidth, screen.availHeight);



        }
        else {
            if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {


                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }

                try {
                    top.window.resizeTo(screen.availWidth, screen.availHeight);
                } catch (e) {
                }
            }
        }
    
    </script>
</head>
<body style="padding:5px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1" 
                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:ASPxGridViewExporter>

                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
                        ClientInstanceName="gvDados"  
                        onhtmldatacellprepared="gvDados_HtmlDataCellPrepared" Width="100%" 
                        KeyFieldName="CodigoConta">
                        <Columns>
                            <dxwgv:GridViewBandColumn Caption="ORÇAMENTO - ACOMPANHAMENTO" 
                                FixedStyle="Left" Name="descricao" VisibleIndex="0">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="TOTAL NORTE ENERGIA&lt;br&gt;(Valores em R$ mil)" 
                                        ExportWidth="468" FieldName="DescricaoConta" Name="DescricaoConta" 
                                        VisibleIndex="0" Width="370px">
                                        <HeaderStyle BackColor="#CEE7FF" Font-Bold="True" Font-Size="10pt" 
                                            ForeColor="#9A1D20" HorizontalAlign="Right" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="#E8F2FF" Font-Bold="True" 
                                    Font-Size="11pt" ForeColor="#053854" HorizontalAlign="Right" 
                                    VerticalAlign="Bottom">
                                <Paddings PaddingTop="10px" />
                                </HeaderStyle>
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Jan" Name="mes1" VisibleIndex="1">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoJan" Name="ValorOrcadoJan" VisibleIndex="0" Width="95px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaJan" Name="ValorTendenciaJan" VisibleIndex="1" 
                                        Width="95px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoJan" Name="ValorRealizadoJan" VisibleIndex="2" 
                                        Width="95px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Fev" Name="mes2" VisibleIndex="2">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoFev" Name="ValorOrcadoFev" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaFev" Name="ValorTendenciaFev" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoFev" Name="ValorRealizadoFev" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Mar" Name="mes3" VisibleIndex="3">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoMar" Name="ValorOrcadoMar" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaMar" Name="ValorTendenciaMar" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoMar" Name="ValorRealizadoMar" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Abr" Name="mes4" VisibleIndex="4">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoAbr" Name="ValorOrcadoAbr" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaAbr" Name="ValorTendenciaAbr" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoAbr" Name="ValorRealizadoAbr" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Mai" Name="mes5" VisibleIndex="5">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoMai" Name="ValorOrcadoMai" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaMai" Name="ValorTendenciaMai" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoMai" Name="ValorRealizadoMai" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Jun" Name="mes6" VisibleIndex="6">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoJun" Name="ValorOrcadoJun" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaJun" Name="ValorTendenciaJun" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoJun" Name="ValorRealizadoJun" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Jul" Name="mes7" VisibleIndex="7">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoJul" Name="ValorOrcadoJul" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaJul" Name="ValorTendenciaJul" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoJul" Name="ValorRealizadoJul" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Ago" Name="mes8" VisibleIndex="8">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoAgo" Name="ValorOrcadoAgo" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaAgo" Name="ValorTendenciaAgo" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoAgo" Name="ValorRealizadoAgo" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Set" Name="mes9" VisibleIndex="9">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoSet" Name="ValorOrcadoSet" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaSet" Name="ValorTendenciaSet" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoSet" Name="ValorRealizadoSet" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Out" Name="mes10" VisibleIndex="10">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoOut" Name="ValorOrcadoOut" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaOut" Name="ValorTendenciaOut" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoOut" Name="ValorRealizadoOut" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Nov" Name="mes11" VisibleIndex="11">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoNov" Name="ValorOrcadoNov" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaNov" Name="ValorTendenciaNov" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoNov" Name="ValorRealizadoNov" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Dez" Name="mes12" VisibleIndex="12">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Orçado CA" ExportWidth="95" 
                                        FieldName="ValorOrcadoDez" Name="ValorOrcadoDez" VisibleIndex="0" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" ForeColor="#9A1D20" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Melhor Estimativa" ExportWidth="95" 
                                        FieldName="ValorTendenciaDez" Name="ValorTendenciaDez" VisibleIndex="1" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Real" ExportWidth="95" 
                                        FieldName="ValorRealizadoDez" Name="ValorRealizadoDez" VisibleIndex="2" 
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <HeaderStyle BackColor="#CEE7FF" HorizontalAlign="Center" />
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Size="11pt" 
                                    HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Nivel" Visible="False" 
                                VisibleIndex="14">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoConta" Visible="False" 
                                VisibleIndex="13">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                        <SettingsPager PageSize="100" Visible="False" Mode="EndlessPaging">
                        </SettingsPager>
                        <Settings HorizontalScrollBarMode="Auto" ShowTitlePanel="True" 
                            VerticalScrollBarMode="Auto" />
                        <Styles>
                            <Header Font-Bold="True" Wrap="True">
                            </Header>
                            <TitlePanel BackColor="White">
                                <Paddings Padding="1px" />
                            </TitlePanel>
                        </Styles>
                        <Templates>
                            <TitlePanel>
                                <table>
                                    <tr>
                                        <td style="cursor: pointer" title="Exportar para excel">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/imagens/botoes/btnExcel.png" onclick="ImageButton1_Click" 
                                                ToolTip="Exportar para excel"  />
                                        </td>
                                        <td style="cursor:pointer; padding-left: 10px;" title="Importar informações">
                                            <dxe:ASPxImage ID="imgImportar" runat="server" Cursor="pointer" Height="23px" 
                                                ImageUrl="~/imagens/importar.PNG" ToolTip="Importar informações" 
                                                ClientVisible="False">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="cursor:pointer; padding-left: 10px;" title="Atualizar informações">
                                            <dxe:ASPxImage ID="imgAtualizar" runat="server" Cursor="pointer" 
                                                ImageUrl="~/imagens/atualizar.PNG" ToolTip="Atualizar informações" 
                                                ClientVisible="False">
                                                <ClientSideEvents Click="function(s, e) {
	gvDados.PerformCallback();
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 10px">
                    <dxe:ASPxButton ID="btnFechar" runat="server" 
                        Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	window.close();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
