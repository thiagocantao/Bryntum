<%@ Page Language="C#" AutoEventWireup="true" CodeFile="treeListEscopo.aspx.cs" Inherits="_VisaoMaster_Graficos_treeListEscopo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 10px;
        }
        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/imagens/botoes/btnExcel.png" OnClick="ImageButton1_Click" 
                                        ToolTip="Exportar para excel"  />
                
                <dxwtl:aspxtreelist id="gvDados" runat="server" autogeneratecolumns="False"
                clientinstancename="gvDados"
                keyfieldname="CodigoItem" parentfieldname="CodigoItemSuperior" 
                        width="100%" onhtmldatacellprepared="gvDados_HtmlDataCellPrepared" 
                        onhtmlrowprepared="gvDados_HtmlRowPrepared">
                    <Settings GridLines="Horizontal" VerticalScrollBarMode="Visible" />

                    <Styles>
                        <Header Wrap="True">
                        </Header>
                    </Styles>

<Columns>
<dxwtl:TreeListTextColumn FieldName="Item" Name="Item" 
        Caption="Acompanhamento do Escopo" VisibleIndex="0" ExportWidth="800">

    <HeaderStyle Wrap="True" />
    <CellStyle Wrap="True">
    </CellStyle>

</dxwtl:TreeListTextColumn>

<dxwtl:TreeListTextColumn FieldName="PercRelacaoTotalPrevisto" 
        Name="PercRelacaoTotalPrevisto" Width="135px" 
        Caption="% em Relação ao Total Previsto" VisibleIndex="1" 
        ExportWidth="190">
    <PropertiesTextEdit DisplayFormatString="{0:p4}">
    </PropertiesTextEdit>
   

<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Right"  Wrap="True"></CellStyle>
</dxwtl:TreeListTextColumn>
<dxwtl:TreeListTextColumn FieldName="PercRelacaoTotalContratado" 
        Name="PercRelacaoTotalContratado" 
        Caption="% Contratado em Relação ao Total Contratado" VisibleIndex="2" 
        Width="135px" ExportWidth="190">
    <PropertiesTextEdit DisplayFormatString="{0:p4}">
    </PropertiesTextEdit>
    <HeaderStyle HorizontalAlign="Right" Wrap="True" />
    <CellStyle HorizontalAlign="Right" Wrap="True">
    </CellStyle>
    </dxwtl:TreeListTextColumn>
    <dxwtl:TreeListTextColumn Caption="% Contratado em Relação ao Previsto do Item" 
        FieldName="PercContratado" Name="PercContratado" VisibleIndex="2" 
        Width="135px" ExportWidth="190">
        <PropertiesTextEdit DisplayFormatString="{0:p4}">
        </PropertiesTextEdit>
        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
        <CellStyle HorizontalAlign="Right" Wrap="True">
        </CellStyle>
    </dxwtl:TreeListTextColumn>
    <dxwtl:TreeListTextColumn Caption="Contratado?" FieldName="Status" 
        Name="Status" VisibleIndex="3" Width="95px" ExportWidth="160">
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <CellStyle HorizontalAlign="Center" Wrap="True">
        </CellStyle>
    </dxwtl:TreeListTextColumn>
    <dxwtl:TreeListTextColumn Caption="Prazo Máximo de Contratação" 
        FieldName="DataPrevista" Name="DataPrevista" VisibleIndex="4" 
        Width="125px" ExportWidth="160">
        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
        </PropertiesTextEdit>
        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
        <CellStyle HorizontalAlign="Center" Wrap="True">
        </CellStyle>
    </dxwtl:TreeListTextColumn>
</Columns>
</dxwtl:aspxtreelist>
                    <dxwtle:ASPxTreeListExporter ID="ASPxGridViewExporter1" runat="server" 
                        onrenderbrick="ASPxGridViewExporter1_RenderBrick1" treelistid="gvDados">
                        <Settings AutoWidth="True" ExpandAllNodes="True" ExportAllPages="True">
                            <PageSettings Landscape="True">
                                <Margins Bottom="50" Left="20" Right="20" Top="50" />
                            </PageSettings>
                        </Settings>
                        <Styles>
                            <Cell Wrap="False">
                            </Cell>
                        </Styles>
                    </dxwtle:ASPxTreeListExporter>
                
                </td>
            </tr>            
            <tr>
                <td align="right" class="style2">
                </td>
            </tr>
            <tr>
                <td align="right">
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
        </div>
    </form>
</body>
</html>
