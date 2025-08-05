<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reunioes1.aspx.cs" Inherits="Reunioes_reunioes1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        var pastaImagens = "../imagens";
    </script>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/reunioes_ASPxListbox.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/reunioes.js"></script>
    <title>Reuniões</title>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="width: 10px;">
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Início Previsto:" ID="lblAssunto0"
                                   ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" Width="130px"
                                    ClientInstanceName="ddlFiltroInicioPrevisto" 
                                    ID="ddlFiltroInicioPrevisto" oncallback="ddlFiltroInicioPrevisto_Callback">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//debugger;
    var ano = s.GetValue();
    var mes = s.GetText();
    //Março - 2013
    var trataMes = mes.substring(0, mes.indexOf('-')-1);
    var parametro = ano + ';' + trataMes;
   gvDados.PerformCallback(parametro);
}" EndCallback="function(s, e) {
	s.SetSelectedIndex(s.cp_IndiceSelecionado);
}"></ClientSideEvents>
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                        <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxcb:ASPxCallback ID="callbackExcluir" runat="server" 
                                    ClientInstanceName="callbackExcluir" oncallback="callbackExcluir_Callback">
                                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_StatusExclusao == &quot;&quot;)
    {
		window.top.mostraMensagem('Evento excluído com sucesso!', 'sucesso', false, false, null);
        gvDados.PerformCallback();
    }
    else
    {
        window.top.mostraMensagem(s.cp_StatusExclusao, 'erro', true, false, null);
    }
}" />
                                </dxcb:ASPxCallback>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                </td>
                <td id="tdLista" align="left">
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoEvento"
                        AutoGenerateColumns="False" Width="100%" 
                        ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize1"
                        OnCustomCallback="gvDados_CustomCallback" 
                        onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize" 
                        onhtmldatacellprepared="gvDados_HtmlDataCellPrepared">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
         hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
         somenteLeitura = 'N'; 
         //s.GetRowValues(e.visibleIndex,'CodigoEvento;DescricaoResumida;CodigoResponsavelEvento;InicioPrevisto;inicioPrevistoData;inicioPrevistoHora;TerminoPrevisto;TerminoPrevistoData;TerminoPrevistoHora;InicioReal;InicioRealData;InicioRealHora;TerminoReal;TerminoRealData;TerminoRealHora;CodigoTipoAssociacao;CodigoObjetoAssociado;LocalEvento;Pauta;ResumoEvento;CodigoTipoEvento;CodigoObjetoAssociado;',abreExecucao);
         s.GetRowValues(e.visibleIndex,'CodigoEvento;CodigoObjetoAssociado;',abreExecucao);
          
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
         //debugger 
         if(confirm('Deseja realmente excluir o registro?') == true)
         {
             s.GetRowValues(e.visibleIndex,'CodigoEvento',excluiEvento);
         }         
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
         hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
         somenteLeitura = 'S'; 
         //s.GetRowValues(e.visibleIndex,'CodigoEvento;DescricaoResumida;CodigoResponsavelEvento;InicioPrevisto;inicioPrevistoData;inicioPrevistoHora;TerminoPrevisto;TerminoPrevistoData;TerminoPrevistoHora;InicioReal;InicioRealData;InicioRealHora;TerminoReal;TerminoRealData;TerminoRealHora;CodigoTipoAssociacao;CodigoObjetoAssociado;LocalEvento;Pauta;ResumoEvento;CodigoTipoEvento;CodigoObjetoAssociado;',abreExecucao);
         s.GetRowValues(e.visibleIndex,'CodigoEvento;CodigoObjetoAssociado;',abreExecucao);     
}	
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="A&#231;&#227;o" Width="100px"
                                Caption="A&#231;&#227;o" VisibleIndex="0">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Visibility="Invisible"
                                        Text="Incluir">
                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                                <HeaderTemplate>
                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeAdministrar) ? @"<img src=""../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""abreExecucaoNovaReuniao();/*novaReuniao();*/"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>")%>
                                </HeaderTemplate>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoResumida" Name="DescricaoResumida"
                                Caption="Assunto" VisibleIndex="1">
                                <Settings AllowAutoFilter="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="InicioPrevisto" Name="Local" Width="140px"
                                Caption="In&#237;cio Previsto" VisibleIndex="2">
                                <PropertiesComboBox DisplayFormatString="dd/MM/yyyy HH:mm">
                                    <ButtonStyle >
                                    </ButtonStyle>
                                </PropertiesComboBox>
                                <Settings AllowAutoFilter="False"></Settings>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Width="120px" Caption="T&#233;rmino Previsto"
                                VisibleIndex="3">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False"></Settings>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="InicioReal" Name="InicioReal" Width="120px"
                                Caption="In&#237;cio Real" VisibleIndex="3">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False"></Settings>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoReal" Name="TerminoReal" Width="120px"
                                Caption="T&#233;rmino Real" VisibleIndex="4">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False"></Settings>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Quando" Name="Quando" Width="120px" Caption="Quando"
                                VisibleIndex="6">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="True"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelEvento" Name="CodigoResponsavelEvento"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoData" Name="inicioPrevistoData"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="inicioPrevistoHora" Name="inicioPrevistoHora"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoData" Name="TerminoPrevistoData"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevistoHora" Name="TerminoPrevistoHora"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="InicioRealData" Name="InicioRealData" Visible="False"
                                VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="InicioRealHora" Name="InicioRealHora" Visible="False"
                                VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoRealData" Name="TerminoRealData"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TerminoRealHora" Name="TerminoRealHora"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoAssociacao" Name="CodigoTipoAssociacao"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="LocalEvento" Name="LocalEvento" Visible="False"
                                VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Pauta" Name="Pauta" Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ResumoEvento" Name="ResumoEvento" Visible="False"
                                VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoEvento" Name="CodigoTipoEvento"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoObjetoAssociado" Name="CodigoObjetoAssociado"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="IndicaAtrasada" Visible="False" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" 
                            VerticalScrollBarMode="Visible"></Settings>
                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                        <Templates>
                            <FooterRow>
                                <table>
                                    <tr>
                                        <td style="width: 10px; background-color: red">
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                Text="Reuniões passadas não finalizadas.">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </FooterRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
                </td>
                <td align="left">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
