<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TarefasToDoList.aspx.cs"
    Inherits="_Projetos_DadosProjeto_TarefasToDoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>To Do List</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        // ]]>
    </script>
    <style type="text/css">
        .style1 {
            width: 135px;
        }

        .style2 {
            width: 10px;
        }

        .style3 {
            width: 10px;
        }

        .style4 {
            width: 135px;
        }

        .style5 {
            width: 170px;
        }

        .style7 {
            width: 90px;
        }

        .style9 {
            width: 10px;
        }

        .style10 {
            height: 5px;
        }

        @media (max-height: 768px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 330px;
            }
        }

        @media (min-height: 769px) and (max-height: 800px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 380px;
            }
        }

        @media (min-height: 801px) and (max-height: 960px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 450px;
            }
        }

        @media (min-height: 961px) {
            .rolagem-tab {
                overflow-y: auto;
                height: 600px;
            }
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 5px; height: 10px"></td>
                    <td style="height: 10px"></td>
                    <td style="width: 10px; height: 10px;"></td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <!-- PANELCALLBACK: pnCallback -->
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback"
                            OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- ASPxGRIDVIEW: gvDados -->
                                     <div id="divGrid" style="visibility:hidden">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoTarefa"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvToDoList_CustomButtonInitialize"
                                        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" ClientIDMode="Static">
                                        <ClientSideEvents CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
     LimpaCamposFormulario();
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Get(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		pcDados.Show();
     }
	
}" Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"
 ></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px" Caption="A&#231;&#227;o"
                                                VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                    ClientInstanceName="menu" ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                    OnInit="menu_Init">
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
                                                                                <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                    <Image Url="~/imagens/menuExportacao/iconoCSV.png">
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
                                                                            <border borderstyle="None" />
                                                                        </HoverStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </ItemStyle>
                                                                    <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                        <SelectedStyle>
                                                                            <border borderstyle="None" />
                                                                        </SelectedStyle>
                                                                    </SubMenuItemStyle>
                                                                    <Border BorderStyle="None" />
                                                                </dxm:ASPxMenu>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Name="M" Width="100px" VisibleIndex="1">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoOrigem" Name="Origem"
                                                Caption="Origem" VisibleIndex="10" Width="400px">
                                                <Settings AllowAutoFilter="True"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTarefa" Name="Tarefa" Caption="Tarefa"
                                                VisibleIndex="2" Width="300px">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                                <FilterCellStyle>
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoStatusTarefa" Name="Status" Width="185px"
                                                Caption="Status" VisibleIndex="3">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                                <FilterCellStyle>
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Estagio" Name="Estagio" Width="120px" Caption="Est&#225;gio"
                                                VisibleIndex="4">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="InicioPrevisto" Name="InicioPrevisto" Width="110px"
                                                Caption="In&#237;cio Previsto" VisibleIndex="5">
                                                <PropertiesDateEdit UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto"
                                                Width="110px" Caption="T&#233;rmino Previsto" VisibleIndex="6">
                                                <PropertiesDateEdit UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="InicioReal" Name="InicioReal" Width="75px"
                                                Caption="In&#237;cio Real" Visible="False" VisibleIndex="7">
                                                <PropertiesDateEdit UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="TerminoReal" Name="TerminoReal" Width="110px"
                                                Caption="T&#233;rmino Real" Visible="False" VisibleIndex="8">
                                                <PropertiesDateEdit UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Name="Responsavel"
                                                Caption="Respons&#225;vel" VisibleIndex="9" Width="300px">
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                                <FilterCellStyle>
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel"
                                                Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="PercentualConcluido" Name="PercentualConcluido"
                                                Caption="PercentualConcluido" Visible="False" VisibleIndex="13">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Anotacoes" Name="Anotacoes" Caption="Anotações"
                                                Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoStatusTarefa" Name="CodigoStatusTarefa"
                                                Caption="CodigoStatusTarefa" Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="EsforcoPrevisto" Name="EsforcoPrevisto"
                                                Caption="EsforcoPrevisto" Visible="False" VisibleIndex="16">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="EsforcoReal" Name="EsforcoReal" Caption="EsforcoReal"
                                                Visible="False" VisibleIndex="19">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CustoPrevisto" Name="CustoPrevisto" Caption="CustoPrevisto"
                                                Visible="False" VisibleIndex="17">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CustoReal" Name="CustoReal" Caption="CustoReal"
                                                Visible="False" VisibleIndex="18">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Prioridade" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                                            ShowFooter="True" HorizontalScrollBarMode="Auto"></Settings>
                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Templates>
                                            <FooterRow>
                                                <table class="grid-legendas" cellspacing="0" cellpadding="0" width="100px">
                                                    <tbody>
                                                        <tr>
                                                            <td class="grid-legendas-cor grid-legendas-cor-concluido"><span></span></td>
                                                            <td class="grid-legendas-label grid-legendas-label-concluido">
                                                                <dxe:ASPxLabel ID="lblDescricaoConcluido" runat="server" ClientInstanceName="lblDescricaoConcluido"
                                                                    Text="<%# Resources.traducao.TarefasToDoList_tarefa_conclu_da %>">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td class="grid-legendas-cor grid-legendas-cor-atrasado"><span></span></td>
                                                            <td class="grid-legendas-label grid-legendas-label-atrasado">
                                                                <dxe:ASPxLabel ID="lblDescricaoAtrasada" runat="server" ClientInstanceName="lblDescricaoAtrasada"
                                                                    Text="<%# Resources.traducao.TarefasToDoList_tarefa_atrasada %>">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td class="grid-legendas-icone">
                                                                <img style="border: 0px" src="../../imagens/anotacao.png" /></td>
                                                            <td class="grid-legendas-label grid-legendas-label-icone">
                                                                <dxe:ASPxLabel ID="lblDescricaoAnotacoes" runat="server" ClientInstanceName="lblDescricaoAnotacoes"
                                                                    Text="<%# Resources.traducao.TarefasToDoList_possui_anota__es %>">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                         </div>
                                    <!-- PANEL CONTROL : pcDados -->
                                    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupVerticalOffset="-10" ShowCloseButton="False" Width="1000px" Height="145px"
                                        ID="pcDados">
                                        <ClientSideEvents Shown="function(s, e) {
	desabilitaHabilitaComponentes();
}"
                                            CloseUp="function(s, e) {
	
	ddlResponsavel.SetValue(null);
	ddlResponsavel.SetText(&quot;&quot;);	
	ddlResponsavel.PerformCallback();
}"></ClientSideEvents>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <div class="rolagem-tab">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o Tarefa:" ClientInstanceName="lblDescricaoTarefa"
                                                                                        ID="lblDescricaoTarefa">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoTarefaBanco"
                                                                                        ID="txtDescricaoTarefaBanco"
                                                                                        MaxLength="250">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            <border bordercolor="Silver"></border>
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="lblCodigoUsuarioResponsavel" runat="server" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                                                        Text="Usuário Responsável:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td class="style7">
                                                                                                    <dxe:ASPxLabel ID="lblPrioridade" runat="server" ClientInstanceName="lblPrioridade"
                                                                                                        Text="Prioridade:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td class="style3">&nbsp;
                                                                                                </td>
                                                                                                <td class="style5">
                                                                                                    <dxe:ASPxLabel ID="lblStatusTarefa" runat="server" ClientInstanceName="lblStatusTarefa"
                                                                                                        Text="Status:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel"
                                                                                                        IncrementalFilteringMode="Contains" ValueType="System.String"
                                                                                                        Width="350px" DropDownStyle="DropDown" EnableCallbackMode="True" OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                                                                                        OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                                                                                        DropDownHeight="170px">
                                                                                                        <Columns>
                                                                                                            <dxe:ListBoxColumn Caption="Nome" Width="300px" FieldName="NomeUsuario" />
                                                                                                            <dxe:ListBoxColumn Caption="Email" Width="200px" FieldName="EMail" />
                                                                                                        </Columns>
                                                                                                        <ValidationSettings ErrorDisplayMode="None" ErrorText="*">
                                                                                                            <RequiredField IsRequired="True" />
                                                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                                                        </ValidationSettings>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td></td>
                                                                                                <td class="style7">
                                                                                                    <dxe:ASPxComboBox ID="ddlPrioridade" runat="server" ClientInstanceName="ddlPrioridade"
                                                                                                        SelectedIndex="2" ValueType="System.String"
                                                                                                        Width="100%">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="Alta" Value="A" />
                                                                                                            <dxe:ListEditItem Selected="True" Text="Média" Value="M" />
                                                                                                            <dxe:ListEditItem Selected="True" Text="Baixa" Value="B" />
                                                                                                        </Items>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="style3">&nbsp;
                                                                                                </td>
                                                                                                <td class="style5">
                                                                                                    <dxe:ASPxComboBox ID="ddlStatusTarefa" runat="server" ClientInstanceName="ddlStatusTarefa"
                                                                                                        ValueType="System.Int32" Width="100%">
                                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
						debugger;
                        var dataAtual = new Date();
						codigoStatus = s.GetValue();
                        if(codigoStatus != '2') //Em execução, cancelada e não executada.
                        {
                            ddlTerminoReal.SetValue(null);
                            mmAnotacoesBanco.SetEnabled(true);
                            txtCustoRealBanco.SetEnabled(true);
                            txtEsforcoReal.SetEnabled(true);
                            ddlTerminoReal.SetEnabled(true);
                            ddlInicioReal.SetEnabled(true);
                            lblCustoPrevisto.SetEnabled(true);
                            lblEsforcoPrevisto.SetEnabled(true);
                            ddlTerminoPrevisto.SetEnabled(true);
                            ddlInicioPrevisto.SetEnabled(true);
                        }
                        if(codigoStatus == '1' || codigoStatus == '4')// Não iniciada(4) ou Em execução(1)
                        {
                            if(ddlInicioPrevisto.GetValue() == null){
                                ddlInicioPrevisto.SetValue(dataAtual);
                            }
                            if(ddlTerminoPrevisto.GetValue() == null){
                                ddlTerminoPrevisto.SetValue(dataAtual);
                            }
                            if(codigoStatus == '4'){
                                ddlTerminoReal.SetValue(null);
                                ddlInicioReal.SetValue(null);
                                txtEsforcoReal.SetValue(null);
                                txtCustoRealBanco.SetValue(null);
                            }
                            if(codigoStatus == '1'){
                                if(ddlInicioReal.GetValue() == null){
                                    ddlInicioReal.SetValue(dataAtual);
                                }
                                if(ddlTerminoReal.GetValue() != null){
                                    ddlTerminoReal.SetValue(null);
                                }
                            }
                        }
                        if(codigoStatus == '2') // Concluída
                        {                             
                             if(lblEsforcoPrevisto.GetValue() != null && txtEsforcoReal.GetValue() == null){
                                txtEsforcoReal.SetValue(lblEsforcoPrevisto.GetValue());
                             }

                             if(lblCustoPrevisto.GetValue() != null && txtCustoRealBanco.GetValue() == null){
                                txtCustoRealBanco.SetValue(lblCustoPrevisto.GetValue());
                             }

                            if(ddlInicioReal.GetValue() == null){
                                ddlInicioReal.SetValue(dataAtual);
                            }
                            if(ddlTerminoReal.GetValue() == null){
                                ddlTerminoReal.SetValue(dataAtual);
                            }
                        }
}" />
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxLabel ID="lblOrigemTarefa" runat="server"
                                                                        ClientInstanceName="lblOrigemTarefa"
                                                                        Text="Origem Tarefa:">
                                                                    </dxtv:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxtv:ASPxMemo ID="txtOrigemTarefaBanco" runat="server" ClientEnabled="False"
                                                                        ClientInstanceName="txtOrigemTarefaBanco"
                                                                        Rows="4" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            <border bordercolor="Silver" />
                                                                        </DisabledStyle>
                                                                    </dxtv:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxLabel ID="lblInicioPrevisto" runat="server" ClientInstanceName="lblInicioPrevisto"
                                                                                        Text="Início Previsto:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px; height: 17px"></td>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxLabel ID="lblTerminoPrevisto" runat="server" ClientInstanceName="lblTerminoPrevisto"
                                                                                        Text="Término Previsto:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Esforço Previsto(h):" ClientInstanceName="lblEsforcoPrevisto"
                                                                                        ID="lblEsforcoPrevisto">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="lblCustoPrevisto" runat="server" ClientInstanceName="lblCustoPrevisto"
                                                                                        Text="Custo Previsto(R$):">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" ID="ddlInicioPrevisto" runat="server" ClientInstanceName="ddlInicioPrevisto"
                                                                                        Width="100%">
                                                                                        <CalendarProperties>


                                                                                            <DayHeaderStyle />
                                                                                            <WeekNumberStyle>
                                                                                            </WeekNumberStyle>
                                                                                            <DayStyle />
                                                                                            <DayWeekendStyle>
                                                                                            </DayWeekendStyle>
                                                                                            <TodayStyle>
                                                                                            </TodayStyle>
                                                                                            <ButtonStyle>
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle />
                                                                                            <FooterStyle />
                                                                                            <Style>
                                                                                    </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ValidationGroup="MKE">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td></td>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" ID="ddlTerminoPrevisto" runat="server" ClientInstanceName="ddlTerminoPrevisto"
                                                                                        Width="100%">
                                                                                        <CalendarProperties>


                                                                                            <DayHeaderStyle />
                                                                                            <WeekNumberStyle>
                                                                                            </WeekNumberStyle>
                                                                                            <DayStyle />
                                                                                            <DaySelectedStyle>
                                                                                            </DaySelectedStyle>
                                                                                            <DayOtherMonthStyle>
                                                                                            </DayOtherMonthStyle>
                                                                                            <DayWeekendStyle>
                                                                                            </DayWeekendStyle>
                                                                                            <DayOutOfRangeStyle>
                                                                                            </DayOutOfRangeStyle>
                                                                                            <TodayStyle>
                                                                                            </TodayStyle>
                                                                                            <ButtonStyle>
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle />
                                                                                            <FooterStyle />
                                                                                            <Style>
                                                                                    </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txtEsforcoPrevistoBanco" runat="server" ClientInstanceName="txtEsforcoPrevistoBanco"
                                                                                        DisplayFormatString="{0:n0}" Width="100%">
                                                                                        <MaskSettings Mask="&lt;0..9999&gt;" />
                                                                                        <MaskSettings Mask="&lt;0..9999&gt;"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            <border bordercolor="Silver"></border>
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txtCustoPrevistoBanco" runat="server" ClientInstanceName="txtCustoPrevistoBanco"
                                                                                        DisplayFormatString="{0:n2}" Width="100%">
                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;0..99&gt;" />
                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;0..99&gt;"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            <border bordercolor="Silver"></border>
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="style4">
                                                                                    <dxe:ASPxLabel ID="lblInicioReal" runat="server" ClientInstanceName="lblInicioReal"
                                                                                        Text="Início Real:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td class="style2">&nbsp;
                                                                                </td>
                                                                                <td class="style4">
                                                                                    <dxe:ASPxLabel ID="lblTerminoReal" runat="server" ClientInstanceName="lblTerminoReal"
                                                                                        Text="Término Real:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="lblEsforcoReal" runat="server" ClientInstanceName="lblEsforcoReal"
                                                                                        Text="Esforço Real(h):">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px;"></td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="lblCustoReal" runat="server" ClientInstanceName="lblCustoReal"
                                                                                        Text="Custo Real(R$):">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" ID="ddlInicioReal" runat="server" ClientInstanceName="ddlInicioReal"
                                                                                        Width="100%">
                                                                                        <CalendarProperties>


                                                                                            <DayHeaderStyle />
                                                                                            <DayStyle />
                                                                                            <DayOtherMonthStyle>
                                                                                            </DayOtherMonthStyle>
                                                                                            <DayWeekendStyle>
                                                                                            </DayWeekendStyle>
                                                                                            <TodayStyle>
                                                                                            </TodayStyle>
                                                                                            <ButtonStyle>
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle />
                                                                                            <FooterStyle />
                                                                                            <InvalidStyle>
                                                                                            </InvalidStyle>
                                                                                            <Style>
                                                                                    </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ValidationGroup="MKE">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="style3">&nbsp;
                                                                                </td>
                                                                                <td class="style1">
                                                                                    <dxe:ASPxDateEdit PopupVerticalAlign="TopSides" ID="ddlTerminoReal" runat="server" ClientInstanceName="ddlTerminoReal"
                                                                                        Width="100%">
                                                                                        <CalendarProperties>


                                                                                            <DayHeaderStyle />
                                                                                            <WeekNumberStyle>
                                                                                            </WeekNumberStyle>
                                                                                            <DayStyle />
                                                                                            <DaySelectedStyle>
                                                                                            </DaySelectedStyle>
                                                                                            <DayOtherMonthStyle>
                                                                                            </DayOtherMonthStyle>
                                                                                            <DayWeekendStyle>
                                                                                            </DayWeekendStyle>
                                                                                            <DayOutOfRangeStyle>
                                                                                            </DayOutOfRangeStyle>
                                                                                            <TodayStyle>
                                                                                            </TodayStyle>
                                                                                            <ButtonStyle>
                                                                                            </ButtonStyle>
                                                                                            <HeaderStyle />
                                                                                            <FooterStyle />
                                                                                            <Style>
                                                                                    </Style>
                                                                                        </CalendarProperties>
                                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetValue() != null && ddlInicioReal.GetValue() != null)
		ddlStatusTarefa.SetValue('2');
}" />
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE"
                                                                                            Display="Dynamic">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="style3">&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txtEsforcoReal" runat="server" ClientInstanceName="txtEsforcoReal"
                                                                                        DisplayFormatString="{0:n0}" Width="100%">
                                                                                        <MaskSettings Mask="&lt;0..9999&gt;" />
                                                                                        <MaskSettings Mask="&lt;0..9999&gt;"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txtCustoRealBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtCustoRealBanco"
                                                                                        DisplayFormatString="{0:n2}" Width="100%">
                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;0..99&gt;" />
                                                                                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;0..99&gt;"></MaskSettings>
                                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            <border bordercolor="Silver"></border>
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxp:ASPxPanel runat="server" ClientInstanceName="paEditar" Width="100%" ID="paEditar">
                                                                        <PanelCollection>
                                                                            <dxp:PanelContent runat="server">
                                                                                <table id="tdEditar" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table id="TABLE" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es:" ClientInstanceName="lblAnotacoes"
                                                                                                                    ID="lblAnotacoes">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="mmAnotacoesBanco"
                                                                                                                    ID="mmAnotacoesBanco">
                                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                    </DisabledStyle>
                                                                                                                </dxe:ASPxMemo>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </dxp:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxp:ASPxPanel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="right">
                                                                <table align="right" class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="formulario-botao">
                                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                                                                    Width="100px" ID="btnSalvar">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
	}
	else
		return false;
    
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td class="formulario-botao">
                                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px"
                                                                                    ID="btnFechar">
                                                                                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_Sucesso != '')
       {
                   window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                   onClick_btnCancelar();
       }
       else if(s.cp_Erro != '')
      {
                   window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
       }
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="" SelectCommand=""></asp:SqlDataSource>
        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
            LeftMargin="50" RightMargin="50"
            Landscape="True" ID="ASPxGridViewExporter1"
            ExportEmptyDetailGrid="True"
            PreserveGroupRowStates="False"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <Footer>
                </Footer>
                <GroupFooter>
                </GroupFooter>
                <GroupRow>
                </GroupRow>
                <Title></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
