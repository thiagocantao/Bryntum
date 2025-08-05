<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SENAR_ItemABC.aspx.cs" Inherits="_Projetos_DadosProjeto_SENAR_ItemABC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 134px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoItem" ClientInstanceName="gvDados" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                <ClientSideEvents CustomButtonClick="function(s, e) {
     //debugger
    btnSalvar.SetVisible(true);
    if(e.buttonID == &quot;btnEditarCustom&quot;)
    {
           hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
           onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
           desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
             onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {
             btnSalvar.SetVisible(false);
              OnGridFocusedRowChanged(gvDados, true);
             TipoOperacao = 'Consultar';
             hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
             desabilitaHabilitaComponentes();
             pcDados.Show();
      }
}"
                    BeginCallback="function(s, e) {
	comando = e.command;
}"
                    EndCallback="function(s, e) {
     //alert(comando);
     if(comando == &quot;CUSTOMCALLBACK&quot;)
     {
                var erro = s.cp_Erro;
                var sucesso = s.cp_Sucesso;
                if(erro + '' != '')
                {
                          window.top.mostraMensagem(erro, 'Atencao', true, false, null);
                } 
                else
                {
                        if(sucesso + '' != '')
                        {
                                window.top.mostraMensagem(sucesso, 'sucesso', false, false, null);
                                if (window.onClick_btnCancelar)
                                       onClick_btnCancelar();
                        }
                }
     }
}"></ClientSideEvents>

                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                <Settings ShowFooter="True" GroupFormat="{1} {2}" ShowHeaderFilterBlankItems="False" VerticalScrollBarMode="Visible"></Settings>

                <SettingsBehavior AutoExpandAllGroups="True" AllowFocusedRow="True" AllowGroup="False"></SettingsBehavior>

                <SettingsCommandButton>
                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                </SettingsCommandButton>

                <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                <Columns>
                    <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Name="acao" Width="105px" Caption="A&#231;&#227;o" VisibleIndex="0">
                        <CustomButtons>
                            <dxcp:GridViewCommandColumnCustomButton Visibility="Invisible" ID="btnIncluirCustom" Text="Incluir">
                                <Image Url="~/imagens/botoes/incluirReg02.png"></Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                            <dxcp:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                            </dxcp:GridViewCommandColumnCustomButton>
                        </CustomButtons>

                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                        <CellStyle HorizontalAlign="Center"></CellStyle>
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td align="center">
                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                    </dxcp:GridViewCommandColumn>
                    <dxcp:GridViewDataTextColumn FieldName="CodigoItem" Caption="Código" VisibleIndex="1" Visible="False">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="DescricaoItem" Caption="Descrição" VisibleIndex="2" Width="100%" ExportWidth="300">
                    </dxcp:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="IniciaisItem" Caption="Iniciais" Visible="False" VisibleIndex="7">
                    </dxcp:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="ValorUnitarioItem" VisibleIndex="3" Width="150px" ExportWidth="150">
                        <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                            <SpinButtons ClientVisible="False">
                            </SpinButtons>
                        </PropertiesSpinEdit>
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Ativo" FieldName="IndicaItemAtivo" VisibleIndex="6" Width="70px">
                        <PropertiesComboBox>
                            <Items>
                                <dxtv:ListEditItem Text="Sim" Value="S" />
                                <dxtv:ListEditItem Text="Não" Value="N" />
                            </Items>
                        </PropertiesComboBox>
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataComboBoxColumn Caption="Tipo" FieldName="TipoItem" VisibleIndex="5" Width="80px">
                        <PropertiesComboBox>
                            <Items>
                                <dxtv:ListEditItem Text="Ação" Value="A" />
                                <dxtv:ListEditItem Text="Outros" Value="O" />
                            </Items>
                        </PropertiesComboBox>
                    </dxtv:GridViewDataComboBoxColumn>
                </Columns>
                <TotalSummary>
                    <dxcp:ASPxSummaryItem ShowInColumn="DescricaoRiscoQuestao" SummaryType="Count" FieldName="DescricaoRiscoQuestao" Visible="False" ValueDisplayFormat="{0} Item(ns)"></dxcp:ASPxSummaryItem>
                </TotalSummary>
                <Templates>
                    <FooterRow>
                        <table cellpadding="0" cellspacing="0" style="width: 300px;">
                            <tr>
                                <td style="padding-left: 3px; padding-right: 10px; text-align: left">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td style="border: 1px solid #808080; width: 10px; background-color: #ddffcc">&nbsp; </td>
                                                <td style="padding-left: 5px">Controlados pelo sistema </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </FooterRow>
                </Templates>
            </dxcp:ASPxGridView>

            <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="450px" ID="pcDados">

                <ContentStyle>
                    <Paddings Padding="5px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
                </ContentStyle>

                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ContentCollection>
                    <dxcp:PopupControlContentControl runat="server">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="Descrição:">
                                        </dxtv:ASPxLabel>

                                    </td>
                                </tr>
                                <tr style="">
                                    <td align="left">
                                        <dxtv:ASPxTextBox ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao" MaxLength="150" Width="100%">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="">
                                    <td align="left" style="height: 5px; padding-top: 5px;">
                                        <table cellpadding="0" cellspacing="0" class="auto-style1">
                                            <tr>
                                                <td class="auto-style2">
                                                    <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Tipo:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Valor:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    <dxtv:ASPxRadioButtonList ID="rbTipo" runat="server" ItemSpacing="50px" RepeatDirection="Horizontal" Width="100%" ClientInstanceName="rbTipo">
                                                        <Paddings Padding="0px" />
                                                        <Items>
                                                            <dxtv:ListEditItem Text="Ação" Value="A" />
                                                            <dxtv:ListEditItem Text="Outros" Value="O" />
                                                        </Items>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxRadioButtonList>
                                                </td>
                                                <td style="width: 100%; padding-right: 5px">
                                                    <dxtv:ASPxSpinEdit ID="spnValor" runat="server" ClientInstanceName="spnValor" Number="0" Width="100%" DecimalPlaces="2" DisplayFormatString="{0:c2}">
                                                        <SpinButtons ClientVisible="False">
                                                        </SpinButtons>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxSpinEdit>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxCheckBox ID="ckbAtivo" runat="server" CheckState="Unchecked" ClientInstanceName="ckbAtivo" Text="Ativo?" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="">
                                    <td align="left">&nbsp;</td>
                                </tr>
                                <tr style="">
                                    <td align="right">
                                        <table id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr style="height: 35px">
                                                    <td align="right">
                                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar" CausesValidation="False" Text="Salvar" Width="100px" ID="btnSalvar">
                                                            <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
   onClick_btnSalvar();
}"></ClientSideEvents>

                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxcp:ASPxButton>

                                                    </td>
                                                    <td align="right">
                                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px" ID="btnFechar">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxcp:ASPxButton>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxcp:PopupControlContentControl>
                </ContentCollection>
            </dxcp:ASPxPopupControl>

            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default>
                    </Default>
                    <Header>
                    </Header>
                    <Cell>
                    </Cell>
                    <GroupFooter Font-Bold="True">
                    </GroupFooter>
                    <Title Font-Bold="True"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>

            <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

        </div>

    </form>
</body>
</html>
