<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelecaoItensPlanejamentoSprint.aspx.cs"
    Inherits="_Projetos_DadosProjeto_SelecaoItensPlanejamentoSprint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Alertas</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
    </script>
    <style type="text/css">
        .style2 {
            width: 100%;
        }

        .style3 {
            width: 377px;
        }

        </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div>
        </div>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>
        <dxwtl:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server">
        </dxwtl:ASPxTreeListExporter>
        <table class="style2">
            <tr>
                <td>
            <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados" Width="100%" KeyFieldName="CodigoItem" ParentFieldName="CodigoItemSuperior" OnCustomCallback="tlDados_CustomCallback" OnCommandColumnButtonInitialize="tlDados_CommandColumnButtonInitialize" OnHtmlRowPrepared="tlDados_HtmlRowPrepared" OnHtmlDataCellPrepared="tlDados_HtmlDataCellPrepared" OnFocusedNodeChanged="tlDados_FocusedNodeChanged" OnDataBound="tlDados_DataBound">
                <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" EnableCustomizationWindow="True" ProcessFocusedNodeChangedOnServer="True" />
                <Columns>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="1">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AllowAutoFilter="True" AutoFilterCondition="Contains" Caption="Título do Item" FieldName="TituloItem" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="2" Width="300px" SortIndex="0" SortOrder="Ascending">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoProjeto" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="3">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoItemSuperior" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="4">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="DetalheItem" ShowInCustomizationForm="True" ShowInFilterControl="Default" Visible="False" VisibleIndex="5">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Status" FieldName="DescricaoTipoStatusItem" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="6" Width="180px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Sprint" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="7" Width="250px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoTipoStatusItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="8">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Classificação" FieldName="DescricaoTipoClassificacaoItem" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="9" Width="200px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoTipoClassificacaoItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="10">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoUsuarioInclusao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="11">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="DataInclusao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="12">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoUsuarioUltimaAlteracao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="13">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="DataUltimaAlteracao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="14">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoUsuarioExclusao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="15">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="PercentualConcluido" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="16">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoIteracao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="17">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Importancia" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="18" Caption="Importância" Width="100px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Complexidade" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="19">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Esforço Previsto (h)" FieldName="EsforcoPrevisto" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="20" Width="150px">
                        <HeaderStyle Wrap="True" />
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="Complexidade" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="21">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="IndicaQuestao" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="22">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="IndicaBloqueioItem" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="23">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoWorkflow" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="24">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoInstanciaWf" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="25">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoCronogramaProjetoReferencia" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="26">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoTarefaReferencia" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="27">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoCliente" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="28">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="NomeCliente" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="29">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="CodigoTipoTarefaTimeSheet" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="30">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="DescricaoTipoTarefaTimeSheet" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="31">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" FieldName="ReceitaPrevista" ShowInCustomizationForm="False" ShowInFilterControl="Default" Visible="False" VisibleIndex="32">
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Complexidade" FieldName="DescricaoComplexidade" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="33" Width="150px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwtle:TreeListTextColumn>
                    <dxwtle:TreeListDateTimeColumn AutoFilterCondition="Default" Caption="Data Alvo" FieldName="DataAlvo" ShowInFilterControl="Default" Visible="False" VisibleIndex="34">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                    </dxwtle:TreeListDateTimeColumn>
                    <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Tags" FieldName="TagItem" ShowInCustomizationForm="True" ShowInFilterControl="Default" VisibleIndex="35" Width="200px">
                    </dxwtle:TreeListTextColumn>
                </Columns>
                <Settings SuppressOuterGridLines="True" VerticalScrollBarMode="Visible" ShowFilterRow="True" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowAutoFilter="True"></SettingsBehavior>
                <SettingsPager PageSize="30">
                </SettingsPager>
                <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>
                <SettingsSelection Enabled="True" />
                <SettingsEditing AllowNodeDragDrop="True" />
                <SettingsPopupEditForm VerticalOffset="-1"></SettingsPopupEditForm>
                <SettingsText CommandDelete="Excluir" ConfirmDelete="Deseja realmente excluir o item de backlog?" />
                <SettingsPopup>
                    <EditForm VerticalOffset="-1"></EditForm>
                </SettingsPopup>
                <Styles>
                    <Cell Wrap="True">
                    </Cell>
                </Styles>
                <ClientSideEvents
                    BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
//alert(comando);       
 if(comando == 'CustomCallback')
        {
                 if(s.cpErro == '')
                {
                          if(s.cpSucesso != '')
                         {
                                    window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 5000);
                         }
                }
               else
              {
                         if(s.cpErro != '')
                         {
                                     window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                         }
              }
         }
}
" />
            </dxwtl:ASPxTreeList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table style="width: 250px">
                        <tr>
                            <td>
                    <dxcp:ASPxButton ID="btnSalvar" runat="server" Text="Marcar/Desmarcar" AutoPostBack="False" Width="110px" CssClass="btn">
                        <ClientSideEvents Click="function(s, e) {
tlDados.PerformCallback('');
}" />
                    </dxcp:ASPxButton>
                            </td>
                            <td>
                    <dxcp:ASPxButton ID="btnFechar" runat="server" Text="Fechar" AutoPostBack="False" Width="110px" ClientInstanceName="btnFechar" CssClass="btn">
                        <ClientSideEvents Click="function(s, e) {
window.top.fechaModal();
}" />
                    </dxcp:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>