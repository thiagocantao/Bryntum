<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinanceiroGraficos2.aspx.cs" Inherits="_Projetos_DadosProjeto_RecursosHumanos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
        var vchartJS;
    </script>
    <style type="text/css">
        .style1
        {
            width: 89px;
        }
        .style3
        {
            width: 90px;
        }
        .style4
        {
            width: 75px;
        }
        .style5
        {
            width: 100px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function verificaCheckBox() {
            if (ddlDiretoria.GetValue() == -1 && ddlArea.GetValue() == -1 && ddlCentro.GetValue() == -1) {
                ckTotal.SetEnabled(true);
            }
            else {
                ckTotal.SetChecked(false);
                ckTotal.SetEnabled(false);
            }
        }
    </script>
</head>
<body style="margin:0">
<div><form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="center" class="style1" valign="bottom">
                                <table cellpadding="0" cellspacing="0" style="border-right: #e0e0e0 1px solid; border-top: #e0e0e0 1px solid;
                                    border-left: #e0e0e0 1px solid; border-bottom: #e0e0e0 1px solid; height: 22px">
                                    <tr>
                                        <td style="width: 30px">
                    <dxe:ASPxImage ID="imgGraficos" runat="server" ImageUrl="~/imagens/olap.PNG" ToolTip="Tabela de Orçamento do Projeto">
                    </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 45px">
                                            <dxe:ASPxLabel ID="lblGrafico" runat="server" 
                                                Text="Tabela">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                             <td align="left" >
                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                    Text="Previsão:" Width="100px">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" class="style4" >
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="De:">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" class="style4" >
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="Até:">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" class="style5" >
                                            &nbsp;</td>
                                        <td align="left" >
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Diretoria:" Width="100px">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" >
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Área:" Width="100px">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" >
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                    Text="Centro de Custo:" Width="100px">
                                </dxe:ASPxLabel>
                                        </td>                                       
                                        <td align="left" class="style3" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                            <td>
                    <dxe:ASPxComboBox ID="ddlPrevisao" runat="server" 
                        ClientInstanceName="ddlPrevisao"  
                        Width="100%">
                    </dxe:ASPxComboBox>
                                        </td>
                                        <td class="style4" >
                                            <dxe:ASPxTextBox ID="txtDe" runat="server" ClientInstanceName="txtDe" 
                                                 Width="100%">
                                                <MaskSettings Mask="MM/yyyy" />
                                                <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                    <RequiredField ErrorText="Obrigatório" IsRequired="True" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>





                                        </td>
                                        <td class="style4" >
                                            <dxe:ASPxTextBox ID="txtAte" runat="server" ClientInstanceName="txtAte" 
                                                 Width="100%">
                                                <MaskSettings Mask="MM/yyyy" />
                                                <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                    <RequiredField ErrorText="Obrigatório" IsRequired="True" />
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>





                                        </td>
                                        <td class="style5"  align="left">
                                            <dxe:ASPxCheckBox ID="ckTotal" runat="server" ClientInstanceName="ckTotal" 
                                                 Text="Mostrar Total" TextSpacing="0px" 
                                                Width="100%">
                                            </dxe:ASPxCheckBox>





                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox runat="server" Width="100%" 
                                                ClientInstanceName="ddlDiretoria"  
                                                ID="ddlDiretoria">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	verificaCheckBox();
	ddlArea.PerformCallback();
}" Init="function(s, e) {
	verificaCheckBox();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox runat="server" Width="100%" 
                                                ClientInstanceName="ddlArea"  
                                                ID="ddlArea">
<ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(-1);
	ddlCentro.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	verificaCheckBox();
	ddlCentro.PerformCallback();
}" Init="function(s, e) {
	verificaCheckBox();
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox runat="server" Width="100%" 
                                                ClientInstanceName="ddlCentro"  
                                                ID="ddlCentro" TextFormatString="{0} - {1}">
<ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(-1);
}" Init="function(s, e) {
	verificaCheckBox();
}" SelectedIndexChanged="function(s, e) {
	verificaCheckBox();
}"></ClientSideEvents>

                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="CR" FieldName="CodigoReservadoGrupoConta" 
                                                        Width="140px" />
                                                    <dxe:ListBoxColumn Caption="Descrição" FieldName="DescricaoConta" 
                                                        Width="350px" />
                                                </Columns>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                                        </td>
                                        
                                        <td class="style3" style="padding-right: 7px">
                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" 
                                                Text="Selecionar" Width="90px" AutoPostBack="False" 
                                                ValidationGroup="MKE">
                                                <ClientSideEvents Click="function(s, e) {
	verificaCheckBox();
	callback.PerformCallback();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <dxrp:ASPxRoundPanel ID="pRH" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                        CssPostfix="PlasticBlue"  HeaderText="Desempenho Físico do Projeto"
                        ImageFolder="~/App_Themes/PlasticBlue/{0}/" Width="420px">
                        <ContentPaddings Padding="1px" PaddingTop="2px" />
                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                            ForeColor="#404040">
                            <Paddings Padding="1px" PaddingLeft="3px" PaddingTop="0px" />
                            <BorderLeft BorderStyle="None" />
                            <BorderRight BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </HeaderStyle>
                        <HeaderContent>
                            <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1031923202/HeaderContent.png"
                                Repeat="RepeatX" VerticalPosition="bottom" />
                        </HeaderContent>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="chartdiv1" align="center">
                                            </div>

                                             <script type="text/javascript">                                                 
                                                 vchartJS = getGrafico('<%=grafico1_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico1_xml %>', 'chartdiv1');
                                            </script>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <HeaderTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                Text="Orçamento">
                                            </dxe:ASPxLabel>
                                            &nbsp;</td>
                                        <td align="right" style="width: 20px">
                                            <img align="right" alt="Visualizar gráfico em modo ampliado" onclick="javascript:f_zoomGrafico('../../zoom.aspx', '', 'Flashs/MSColumn2D.swf', '<%=grafico1_xmlzoom %>', '0')"
                                                src="../../imagens/zoom.PNG" style="cursor: pointer" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                    </dxrp:ASPxRoundPanel>                               
                </td>
            </tr>
        </table>
</form></div>    
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback">
        <ClientSideEvents EndCallback="function(s, e) {
	vchartJS.setDataURL(s.cp_grafico);
}" />
    </dxcb:ASPxCallback>
</body>
</html>

