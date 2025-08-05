<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_003.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_vc_003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function abreCategoria(valor)
        {
            parent.abreCategoriaLink(valor);
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Desempenho Categorias"
            Height="415px" Width="220px"  ImageFolder="~/App_Themes/PlasticBlue/{0}/" HorizontalAlign="Left">
            <ContentPaddings Padding="1px" />
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/699579670/HeaderRightEdge.png" Repeat="NoRepeat"
                    VerticalPosition="bottom" HorizontalPosition="right" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
                <Paddings PaddingBottom="7px" PaddingTop="3px" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/699579670/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                         Width="100%" ClientInstanceName="gvDados">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="CorCategoria" VisibleIndex="0"
                                Width="30px">
                                <PropertiesTextEdit DisplayFormatString="&lt;img src=&quot;../../imagens/{0}.gif&quot; /&gt;">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="DescricaoCategoria"
                                VisibleIndex="1">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                                <DataItemTemplate>
                                     <%# (Eval("CorCategoria").ToString() == "Branco" || permissaoLink == false ? Eval("DescricaoCategoria").ToString() + " (" + Eval("SiglaCategoria").ToString() + ")" : "<a onclick='abreCategoria(" + Eval("CodigoCategoria").ToString() + ");' href='#'>" + Eval("DescricaoCategoria").ToString() + " (" + Eval("SiglaCategoria").ToString() + ")</a>")%>                                    
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <Settings ShowColumnHeaders="False" VerticalScrollBarMode="Visible" />
                        <Border BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" />
                        <SettingsText  />
                    </dxwgv:ASPxGridView>
                </dxp:PanelContent>
            </PanelCollection>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
