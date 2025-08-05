<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpCronograma.aspx.cs" Inherits="_Projetos_DadosProjeto_graficos_PopUpCronograma" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cronograma</title>
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Agil/taskboard/Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript">

        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter();
        }

        $(document).ready(function () {
            window.top.SetBotaoSalvarVisivel(false);
            //alert('Ready Assincron');
        });
    </script>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblTarefa" runat="server"
                            Text="Tarefa:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txtTarefa" runat="server" ClientEnabled="False" Width="100%">
                            <DisabledStyle ForeColor="#404040">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxtc:ASPxPageControl ID="pcCronograma" runat="server" ActiveTabIndex="2" Width="100%">
                            <TabPages>
                                <dxtc:TabPage Name="tabDetalhes" Text="Detalhes">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <div id="divDetalhes">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 25%">
                                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                            Text="In&#237;cio Previsto:    ">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                            Text="In&#237;cio Reprogramado:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                            Text="In&#237;cio Real:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                            Text="Varia&#231;&#227;o:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox ID="txtInicioPrevisto" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox ID="txtInicioReprogramado" runat="server" ClientEnabled="False"
                                                                            Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px">
                                                                        <dxe:ASPxTextBox ID="txtInicioReal" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtInicioVar" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 10px">
                                                                <tr>
                                                                    <td style="width: 25%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Término Previsto:    ">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Término Reprogramado:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Text="Término Real:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" Text="Variação:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%; padding-right: 5px;">
                                                                        <dxtv:ASPxTextBox ID="txtTerminoPrevisto" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 25%; padding-right: 5px;">
                                                                        <dxtv:ASPxTextBox ID="txtTerminoReprogramado" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 25%; padding-right: 5px;">
                                                                        <dxtv:ASPxTextBox ID="txtTerminoReal" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxTextBox ID="txtTerminoVar" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" style="width: 25%; padding-top: 10px;">
                                                                <tr>
                                                                    <td style="width: 25%">
                                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                            Text="Percentual Conclu&#237;do:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dxe:ASPxTextBox ID="txtPercentualReal" runat="server" ClientEnabled="False" Width="100%">
                                                                            <DisabledStyle ForeColor="#404040">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>


                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabRH" Text="RH">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <dxwgv:ASPxGridView ID="gvRH" runat="server" AutoGenerateColumns="False" Width="100%">
                                                <TotalSummary>
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="Trabalho"
                                                        ShowInColumn="Trabalho" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="TrabalhoPrevisto"
                                                        ShowInColumn="TrabalhoPrevisto" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="TrabalhoReal"
                                                        ShowInColumn="TrabalhoReal" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="VariacaoTrabalho"
                                                        ShowInColumn="Variacao" SummaryType="Sum" />
                                                </TotalSummary>

                                                <SettingsPopup>
                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Recurso" Name="Recurso" VisibleIndex="0" FieldName="NomeRecurso" Width="45%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Trabalho (h)" Name="Trabalho" VisibleIndex="1" FieldName="Trabalho">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Trabalho Prev. (h)" Name="TrabalhoPrevisto"
                                                        VisibleIndex="2" FieldName="TrabalhoPrevisto">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Trabalho Real (h)" Name="TrabalhoReal" VisibleIndex="3" FieldName="TrabalhoReal">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Varia&#231;&#227;o (h)" Name="Variacao" VisibleIndex="4" FieldName="VariacaoTrabalho">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n0}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 115;
       s.SetHeight(sHeight);
}" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="243"
                                                    ShowFooter="True" />
                                                <SettingsText />
                                                <Styles>
                                                    <Footer>
                                                        <Paddings Padding="5px" />
                                                    </Footer>
                                                </Styles>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabCustos" Text="Custos">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <dxwgv:ASPxGridView ID="gvCustos" runat="server" AutoGenerateColumns="False" Width="100%">
                                                <TotalSummary>
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="Custo"
                                                        ShowInColumn="Custo" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="CustoPrevisto"
                                                        ShowInColumn="CustoPrevisto" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="CustoReal"
                                                        ShowInColumn="CustoReal" SummaryType="Sum" />
                                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="VariacaoCusto"
                                                        ShowInColumn="Variacao" SummaryType="Sum" />
                                                </TotalSummary>

                                                <SettingsPopup>
                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Recurso" Name="Recurso" VisibleIndex="0" FieldName="NomeRecurso" Width="45%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Despesa (R$)" Name="Custo" VisibleIndex="1" FieldName="Custo">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Despesa Prev.(R$)" Name="CustoPrevisto"
                                                        VisibleIndex="2" FieldName="CustoPrevisto">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Despesa Real (R$)" Name="CustoReal" VisibleIndex="3" FieldName="CustoReal">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Varia&#231;&#227;o (R$)" Name="Variacao" VisibleIndex="4" FieldName="VariacaoCusto">
                                                        <Settings AllowAutoFilter="False" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 115;
       s.SetHeight(sHeight);
}" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="243"
                                                    ShowFooter="True" />
                                                <SettingsText />
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabAnotacoes" Text="Anota&#231;&#245;es">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <dxhe:ASPxHtmlEditor ID="txtAnotacao" runat="server" ActiveView="Preview" Height="276px"
                                                            Width="100%">
                                                            <ClientSideEvents Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 115;
       s.SetHeight(sHeight);
}" />
                                                            <Settings AllowDesignView="False" AllowHtmlView="False" />

                                                            <SettingsDialogs>
                                                                <InsertImageDialog>
                                                                    <SettingsImageSelector>
                                                                        <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                                    </SettingsImageSelector>
                                                                </InsertImageDialog>
                                                                <InsertLinkDialog>
                                                                    <SettingsDocumentSelector>
                                                                        <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                                    </SettingsDocumentSelector>
                                                                </InsertLinkDialog>
                                                            </SettingsDialogs>

                                                            <SettingsHtmlEditing>
                                                                <PasteFiltering Attributes="class"></PasteFiltering>
                                                            </SettingsHtmlEditing>

                                                            <Border BorderStyle="None" />
                                                            <BorderLeft BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" />
                                                            <BorderTop BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" />
                                                            <BorderRight BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" />
                                                            <BorderBottom BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" />
                                                        </dxhe:ASPxHtmlEditor>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabAnexos" Text="Anexos" ClientVisible="False">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <iframe id="frameAnexos" frameborder="0" scrolling="no"
                                                style="width: 100%; height: 290px"></iframe>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <ClientSideEvents ActiveTabChanged="function(s, e) {
	var tab = s.GetActiveTab();
	 if(e.tab.name == &quot;tabAnexos&quot; &amp;&amp; document.getElementById('frameAnexos').src == &quot;&quot;)
     {
		document.getElementById('frameAnexos').src = s.cp_URLAnexos;
     }  
}"></ClientSideEvents>
                        </dxtc:ASPxPageControl>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 115;
        document.getElementById('divDetalhes').style.height = sHeight + "px";
    </script>
</body>
</html>
