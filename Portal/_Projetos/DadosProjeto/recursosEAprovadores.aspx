<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recursosEAprovadores.aspx.cs"
    Inherits="_Projetos_DadosProjeto_recursosEAprovadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 98%;
        }
        .dxgvControl, .dxgvDisabled
        {
            border: 1px Solid #9F9F9F;
            font: 12px Tahoma;
            background-color: #F2F2F2;
            color: Black;
            cursor: default;
        }
        .dxgvTable
        {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .dxgvTable
        {
            background-color: White;
            border-width: 0;
            border-collapse: separate !important;
            overflow: hidden;
            color: Black;
        }
        
        .dxgvHeader
        {
            cursor: pointer;
            white-space: nowrap;
            padding: 4px 6px 5px;
            border: 1px Solid #9F9F9F;
            background-color: #DCDCDC;
            overflow: hidden;
            font-weight: normal;
            text-align: left;
        }
        .dxgvFilterRow
        {
            background-color: #E7E7E7;
        }
        .dxgvCommandColumn
        {
            padding: 2px;
        }
        .dxeTrackBar, .dxeIRadioButton, .dxeButtonEdit, .dxeTextBox, .dxeRadioButtonList, .dxeCheckBoxList, .dxeMemo, .dxeListBox, .dxeCalendar, .dxeColorTable
        {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .dxeTextBox, .dxeButtonEdit, .dxeIRadioButton, .dxeRadioButtonList, .dxeCheckBoxList
        {
            cursor: default;
        }
        
        .dxeTextBox, .dxeMemo
        {
            background-color: white;
            border: 1px solid #9f9f9f;
        }
        .dxeTextBoxSys, .dxeMemoSys
        {
            border-collapse: separate !important;
        }
        
        .dxeTextBox .dxeEditArea
        {
            background-color: white;
        }
        
        .dxeEditArea
        {
            font: 12px Tahoma;
            border: 1px solid #A0A0A0;
        }
        .dxeEditAreaSys
        {
            width: 100%;
            background-position: 0 0; /*iOS Safari*/
        }
        
        .dxeEditAreaSys, .dxeEditAreaNotStrechSys
        {
            border: 0px !important;
            padding: 0px;
        }
        
        .dxGridView_gvFilterRowButton
        {
            background-position: -100px 0px;
            width: 13px;
            height: 13px;
        }
        
        .dxGridView_gvCollapsedButton, .dxGridView_gvCollapsedButtonRtl, .dxGridView_gvExpandedButton, .dxGridView_gvExpandedButtonRtl, .dxGridView_gvDetailCollapsedButton, .dxGridView_gvDetailCollapsedButtonRtl, .dxGridView_gvDetailExpandedButton, .dxGridView_gvDetailExpandedButtonRtl, .dxGridView_gvFilterRowButton, .dxGridView_gvHeaderFilter, .dxGridView_gvHeaderFilterActive, .dxGridView_gvHeaderSortDown, .dxGridView_gvHeaderSortUp, .dxGridView_gvDragAndDropArrowDown, .dxGridView_gvDragAndDropArrowUp, .dxGridView_gvDragAndDropHideColumn, .dxGridView_gvParentGroupRows, .dxGridView_WindowResizer, .dxGridView_WindowResizerRtl
        {
            background-repeat: no-repeat;
            background-color: transparent;
        }
        
        .dxgvFocusedRow
        {
            background-color: #8D8D8D;
            color: White;
        }
        .dxgvFooter
        {
            background-color: #D7D7D7;
            white-space: nowrap;
        }
        .dxeBase
        {
            font: 12px Tahoma;
        }
        
        .dxgvPagerTopPanel, .dxgvPagerBottomPanel
        {
            padding-top: 4px;
            padding-bottom: 4px;
        }
        
        .dxpControl
        {
            font: 12px Tahoma;
            color: black;
        }
        .dxpSummary
        {
            white-space: nowrap;
            text-align: center;
            vertical-align: middle;
            padding: 1px 4px 0px;
        }
        .dxpDisabled
        {
            color: #acacac;
            border-color: #808080;
            cursor: default;
        }
        .dxpDisabledButton
        {
            text-decoration: none;
        }
        .dxpButton
        {
            color: #394EA2;
            text-decoration: underline;
            white-space: nowrap;
            text-align: center;
            vertical-align: middle;
        }
        
        .dxWeb_pPrevDisabled
        {
            background-position: -105px -25px;
            width: 16px;
            height: 17px;
        }
        
        .dxWeb_pPopOut, .dxWeb_pPopOutDisabled, .dxWeb_pAll, .dxWeb_pAllDisabled, .dxWeb_pPrev, .dxWeb_pPrevDisabled, .dxWeb_pNext, .dxWeb_pNextDisabled, .dxWeb_pLast, .dxWeb_pLastDisabled, .dxWeb_pFirst, .dxWeb_pFirstDisabled
        {
            display: inline;
        }
        
        .dxWeb_rpHeaderTopLeftCorner, .dxWeb_rpHeaderTopRightCorner, .dxWeb_rpBottomLeftCorner, .dxWeb_rpBottomRightCorner, .dxWeb_rpTopLeftCorner, .dxWeb_rpTopRightCorner, .dxWeb_rpGroupBoxBottomLeftCorner, .dxWeb_rpGroupBoxBottomRightCorner, .dxWeb_rpGroupBoxTopLeftCorner, .dxWeb_rpGroupBoxTopRightCorner, .dxWeb_mHorizontalPopOut, .dxWeb_mVerticalPopOut, .dxWeb_mVerticalPopOutRtl, .dxWeb_mSubMenuItem, .dxWeb_mSubMenuItemChecked, .dxWeb_mScrollUp, .dxWeb_mScrollDown, .dxWeb_tcScrollLeft, .dxWeb_tcScrollRight, .dxWeb_tcScrollLeftHover, .dxWeb_tcScrollRightHover, .dxWeb_tcScrollLeftPressed, .dxWeb_tcScrollRightPressed, .dxWeb_tcScrollLeftDisabled, .dxWeb_tcScrollRightDisabled, .dxWeb_nbCollapse, .dxWeb_nbExpand, .dxWeb_splVSeparator, .dxWeb_splVSeparatorHover, .dxWeb_splHSeparator, .dxWeb_splHSeparatorHover, .dxWeb_splVCollapseBackwardButton, .dxWeb_splVCollapseBackwardButtonHover, .dxWeb_splHCollapseBackwardButton, .dxWeb_splHCollapseBackwardButtonHover, .dxWeb_splVCollapseForwardButton, .dxWeb_splVCollapseForwardButtonHover, .dxWeb_splHCollapseForwardButton, .dxWeb_splHCollapseForwardButtonHover, .dxWeb_pcCloseButton, .dxWeb_pcSizeGrip, .dxWeb_pcSizeGripRtl, .dxWeb_pPopOut, .dxWeb_pPopOutDisabled, .dxWeb_pAll, .dxWeb_pAllDisabled, .dxWeb_pPrev, .dxWeb_pPrevDisabled, .dxWeb_pNext, .dxWeb_pNextDisabled, .dxWeb_pLast, .dxWeb_pLastDisabled, .dxWeb_pFirst, .dxWeb_pFirstDisabled, .dxWeb_tvColBtn, .dxWeb_tvColBtnRtl, .dxWeb_tvExpBtn, .dxWeb_tvExpBtnRtl, .dxWeb_fmFolder, .dxWeb_fmFolderLocked, .dxWeb_fmCreateButton, .dxWeb_fmMoveButton, .dxWeb_fmRenameButton, .dxWeb_fmDeleteButton, .dxWeb_fmRefreshButton, .dxWeb_fmDwnlButton, .dxWeb_fmCreateButtonDisabled, .dxWeb_fmMoveButtonDisabled, .dxWeb_fmRenameButtonDisabled, .dxWeb_fmDeleteButtonDisabled, .dxWeb_fmRefreshButtonDisabled, .dxWeb_fmDwnlButtonDisabled, .dxWeb_ucClearButton
        {
            background-repeat: no-repeat;
            background-color: transparent;
            display: block;
        }
        
        .dxpCurrentPageNumber
        {
            font-weight: bold;
            text-decoration: none;
            padding: 1px 3px 0px;
            white-space: nowrap;
        }
        .dxpPageNumber
        {
            color: #394EA2;
            text-decoration: underline;
            text-align: center;
            vertical-align: middle;
            padding: 1px 5px 0px;
        }
        
        .dxWeb_pNextDisabled
        {
            background-position: -81px -25px;
            width: 16px;
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td align="right" style="padding-top: 3px">
                                                                            <table cellspacing="0" 
                        cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 116px">
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxComboBox runat="server" Width="70px" ClientInstanceName="ddlExporta" 
                                                                                                                 ID="ddlExporta">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>

                                                                                                        </td>
                                                                                                        <td style="padding-right: 3px; padding-left: 3px">
                                                                                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                                                                                 ClientInstanceName="pnImage" Width="23px" 
                                                                                                                Height="22px" ID="pnImage" OnCallback="pnImage_Callback"><PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                                        <dxe:ASPxImage runat="server" 
                                                                                                                            ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px" Height="20px" 
                                                                                                                            ClientInstanceName="imgExportacao" ID="imgExportacao"></dxe:ASPxImage>

                                                                                                                    </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" Text="Exportar" 
                                                                                                ID="Aspxbutton1" OnClick="btnExcel_Click">
<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 3px">
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
                        GridViewID="gvDados" PaperKind="A4" 
                        onrenderbrick="ASPxGridViewExporter1_RenderBrick">
                        <Styles>
                            <Header Font-Size="10px">
                            </Header>
                            <Cell >
                            </Cell>
                        </Styles>
                    </dxwgv:ASPxGridViewExporter>
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="NomeRecurso"
                        AutoGenerateColumns="False" EnableRowsCache="False" Width="100%"
                        ID="gvDados" EnableViewState="False" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                        OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(window.pcDados &amp;&amp; pcDados.GetVisible())
		OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
	OnClick_CustomButtons(s, e);
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="130px"
                                VisibleIndex="0" Visible="False">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Mostrar Detalhes">
                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeRecurso" ShowInCustomizationForm="True"
                                Caption="Recurso" VisibleIndex="1" ExportWidth="300">
                                <Settings ShowFilterRowMenu="False" AllowAutoFilter="False" AllowGroup="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Aprovador" VisibleIndex="2" 
                                FieldName="NomeAprovadorTarefasRecurso" ExportWidth="300">
                                <Settings ShowFilterRowMenu="False" AllowAutoFilter="False"></Settings>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager PageSize="100" Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
