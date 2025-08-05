<%@ Page Language="C#" AutoEventWireup="true" CodeFile="telaDashboard.aspx.cs" Inherits="_Estrategias_telaDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>   
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center; cursor:<%=tipoCursor %>;" onclick="<%=funcaoClick %>">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            HeaderText="Estratégia"
            Height="415px" Width="220px"  ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" PaddingTop="5px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" HorizontalAlign="Left">
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
                         Width="206px" ClientInstanceName="gvDados" OnHtmlRowCreated="gvDados_HtmlRowCreated">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Perspectiva" FieldName="Perspectiva"
                                VisibleIndex="0" Width="40%">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tema" FieldName="Tema" VisibleIndex="1" Width="60%">
                                <DataItemTemplate>
                                    <%# "<table><tr><td style='width: 20;' align='center'><img src='../imagens/" + Eval("CorTema") + ".gif' /></td><td>" + Eval("Tema") + "</td></tr></table>"%>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <Border BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" />
                        <Settings VerticalScrollBarMode="Visible" />
                        <SettingsText  />
                    </dxwgv:ASPxGridView>
                </dxp:PanelContent>
            </PanelCollection>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
