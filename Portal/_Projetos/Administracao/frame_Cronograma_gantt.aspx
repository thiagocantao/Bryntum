<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame_Cronograma_gantt.aspx.cs" Inherits="frame_Cronograma_gantt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../scripts/AnyChart.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">    
     function abreDetalhes(idTarefa, idProjeto, data)
     {
            var tarefaParam = 'CT=' + idTarefa;
            var dataParam = '&Data=' + data;
            var idProjetoParam = '&IDProjeto=' + idProjeto;
            
            window.top.showModal("PopUpCronograma.aspx?" + tarefaParam + idProjetoParam + dataParam, "Detalhes da Tarefa", 750, 400, "", null);           
        }
        function selecionaTarefa()
        {
            if(chartSample.getSelectedTaskInfo() != null)
            {
                var idTarefa = chartSample.getSelectedTaskInfo().id;
                var codigoProjeto = hfCodigoProjeto.Get("CodigoProjeto");
                
                if(idTarefa != null && idTarefa != "")
                {
                    abreDetalhes(idTarefa, codigoProjeto, "");
                }
            }
            else
            {
                window.top.mostraMensagem("Selecione uma Tarefa para Visualizar os Detalhes!", 'atencao', true, false, null);
            }
        }

        function funcaoPosModal(retorno) {
            cbkGeral.PerformCallback(retorno);
        }

        function zoomGantt(s) {
            var myArguments = new Object();
            myArguments.param1 = s.cp_ParamXML;
            myArguments.param2 = s.cp_ParamALT;

            Thiswidth = screen.width - 80;
            var Thisheight = screen.height - 240;
            window.top.showModal('../../ZoomGantt.aspx', 'Zoom', Thiswidth, Thisheight, "", myArguments);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 76px;
        }
        .style2
        {
            width: 20px;
        }
        .style3
        {
            width: 135px;
        }
        .style4
        {
            width: 100%;
        }
        .style7
        {
            height: 5px;
        }
        .style8
        {
            width: 140px;
        }
        .style9
        {
            width: 502px;
        }
        .style10
        {
            height: 10px;
        }
        .style11
        {
            width: 10px;
        }
        .style12
        {
            width: 62px;
        }
        .style13
        {
            width: 45px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" style="background-image: url(imagens/titulo/back_Titulo_Desktop.gif);
                            width: 100%">
                            <tr>
                                <td align="center">
                                    <span id="spanBotoes" runat="server"></span>
                                </td>
                                <td align="center" style="width: auto">
                                    &nbsp;</td>
                                 <td align="right" style="width: 90px">
                                    <asp:Label ID="Label6" runat="server" EnableViewState="False" Font-Bold="False"
                                        Font-Overline="False" Font-Strikeout="False" 
                                         Text="Linha de Base:"></asp:Label></td>
                                <td style="width: 110px">
                                    <dxe:ASPxComboBox ID="ddlLinhaBase" runat="server" AutoPostBack="True"
                                        ValueType="System.String" Width="105px" 
                                        ClientInstanceName="ddlLinhaBase" TextFormatString="{0}">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Versão" FieldName="Descricao" Width="100px" />
                                            <dxe:ListBoxColumn Caption="Status" FieldName="StatusLB" Width="150px" />
                                        </Columns>
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="width: 25px" align="right">
                                    <dxe:ASPxImage ID="imgLB" runat="server" ClientInstanceName="imgLB" 
                                        Cursor="Pointer" ImageUrl="~/imagens/ajuda.png" 
                                        ToolTip="Informações da Linha de Base Selecionada">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>    
                <tr>
                    <td align="left">
                    <div id="divChart">                    
		            </div>
		            <script type="text/javascript" language="JavaScript">
		                var chartSample = new AnyChart('./../../flashs/AnyGantt.swf');
		                chartSample.width = <%=larguraGrafico %>;
			            chartSample.height = <%=alturaGrafico %>;
			            chartSample.setXMLFile('<%=grafico_xml %>');
			            chartSample.write('divChart');
		            </script>
                        <%=nenhumGrafico %>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                     <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
                 <td align="right" style="padding-right: 10px">
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                        HeaderText="Legenda" Width="460px" HorizontalAlign="Left">
                        <ContentPaddings Padding="1px" />
                        <Border BorderColor="#8B8B8B" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle  HorizontalAlign="Left" />
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <table cellspacing="0" cellpadding="0" enableviewstate="false" align="left"><tbody><tr><td style="WIDTH: 4px"></td><td><span><SPAN><SPAN></SPAN><asp:Label runat="server"  ID="lblNum" EnableViewState="False">Atrasadas:</asp:Label>
 <SPAN></SPAN></SPAN></SPAN></td><td style="WIDTH: 12px" bgColor=#ff0000></td><td style="WIDTH: 10px"></td>
    <td class="style12">
        <asp:Label ID="Label4" runat="server" EnableViewState="False"
            Font-Size="7pt">Adiantadas:</asp:Label>
    </td>
    <td style="width: 12px; background-color: blue">
    </td>
    <td>
    </td>
    <td class="style13">
        <asp:Label ID="Label3" runat="server" EnableViewState="False"
            Font-Size="7pt">Críticas:</asp:Label>
    </td>
    <td style="width: 12px; background-color: #7342d7">
    </td>
    <td>
    </td>
    
    <td class="style1">
        <asp:Label ID="Label5" runat="server" EnableViewState="False" 
            >Linha de Base:</asp:Label>
                                    </td>
                                    <td style="width: 12px; background-color: #BFBFBF;">
                                       </td>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td style="TEXT-ALIGN: left">
                                        <span>
                                            <asp:Label ID="Label1" runat="server" Font-Italic="True" 
                                                Font-Size="7pt" Text="Marcos em Itálico"></asp:Label>
                                        </span>
                                    </td>
                                    </tr></tbody></table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
        </table>
                    </td>
                </tr>
            </table>
          
       </div>
        <dxhf:ASPxHiddenField ID="hfCodigoProjeto" runat="server" ClientInstanceName="hfCodigoProjeto">
        </dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl ID="pcLB" runat="server" ClientInstanceName="pcLB" 
         
        HeaderText="Informações da Linha de Base Selecionada" PopupElementID="imgLB" 
        Width="500px">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="1" width="100%">
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td class="style8">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Versão:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Status:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtVersao" runat="server" ClientEnabled="False" 
                                 Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtStatus" runat="server" ClientEnabled="False" 
                                 Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td class="style8">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                Text="Data Solicitação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Solicitante:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtDataSolicitacao" runat="server" ClientEnabled="False" 
                                 Width="100%" 
                                DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtSolicitante" runat="server" ClientEnabled="False" 
                                 Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td class="style8">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                Text="Data Aprovação:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                Text="Aprovador:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8" style="padding-right: 10px">
                            <dxe:ASPxTextBox ID="txtDataAprovacao" runat="server" ClientEnabled="False" 
                                 Width="100%" 
                                DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtAprovador" runat="server" ClientEnabled="False" 
                                 Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                    Text="Descrição da Solicitação:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxMemo ID="txtAnotacao" runat="server" ClientEnabled="False" 
                     Rows="8" Width="100%">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxMemo>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcInformacao" runat="server" ClientInstanceName="pcInformacao" 
         
        HeaderText="Informação" 
        Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="1" class="style4">
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblInformacao" runat="server" 
                    ClientInstanceName="lblInformacao" >
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td align="right">
                            <dxe:ASPxButton ID="btnAbrirCronoBloqueado" runat="server" 
                                Text="Sim" Width="80px" 
                                OnClick="btnEditarCronograma_Click">
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td class="style11">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton3" runat="server" 
                                Text="Não" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	pcInformacao.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcDownload" runat="server" ClientInstanceName="pcDownload" 
         
        HeaderText="Informação" 
        Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Height="112px">
        <ClientSideEvents Shown="function(s, e) {
	setTimeout(&quot;pcDownload.Hide()&quot;,10000);
}" />
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dxe:ASPxLabel ID="lblDownload" runat="server" ClientInstanceName="lblDownload" 
        EncodeHtml="False"  
        Text="Download &lt;a href='#'&gt;Aqui...&lt;/a&gt;">
    </dxe:ASPxLabel>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxcb:aspxcallback id="cbkGeral" runat="server" clientinstancename="cbkGeral" oncallback="cbkGeral_Callback">
    </dxcb:aspxcallback>
    <dxpc:ASPxPopupControl ID="pcDesbloqueio" runat="server" ClientInstanceName="pcDesbloqueio" 
         
        HeaderText="Informação" 
        Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="1" class="style4">
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblDesbloqueio" runat="server" 
                    ClientInstanceName="lblDesbloqueio" >
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" class="style4">
                    <tr>
                        <td align="right">
                            <dxe:ASPxButton ID="btnDesbloquearCrono" runat="server" 
                                Text="Sim" Width="80px" 
                                OnClick="btnDesbloquearCrono_Click">
                                <ClientSideEvents Click="function(s, e) {
	pcDesbloqueio.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td class="style11">
                            &nbsp;</td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton4" runat="server" 
                                Text="Não" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	pcDesbloqueio.Hide();
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>