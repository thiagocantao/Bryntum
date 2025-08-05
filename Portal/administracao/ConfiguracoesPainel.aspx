<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfiguracoesPainel.aspx.cs" Inherits="administracao_ConfiguracoesPainel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .dxeBase
{
	font: 12px Tahoma, Geneva, sans-serif;
}
.dxeTrackBar, 
.dxeIRadioButton, 
.dxeButtonEdit, 
.dxeTextBox, 
.dxeRadioButtonList, 
.dxeCheckBoxList, 
.dxeMemo, 
.dxeListBox, 
.dxeCalendar, 
.dxeColorTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeTextBox,
.dxeButtonEdit,
.dxeIRadioButton,
.dxeRadioButtonList,
.dxeCheckBoxList
{
    cursor: default;
}

.dxeButtonEdit
{
	background-color: white;
	border: 1px solid #9F9F9F;
}

.dxeButtonEditSys 
{
    width: 170px;
}

.dxeButtonEdit .dxeEditArea
{
	background-color: white;
}

.dxeEditArea
{
	font: 12px Tahoma, Geneva, sans-serif;
	border: 1px solid #A0A0A0;
}
.dxeEditAreaSys 
{
    height: 14px;
    line-height: 14px;
    border: 0px!important;
	padding: 0px 1px 0px 0px; /* B146658 */
    background-position: 0 0; /* iOS Safari */
}
.dxeButtonEditButton,
.dxeSpinIncButton,
.dxeSpinDecButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton
{
	padding: 0px 2px 0px 3px;
}
.dxeButtonEditButton,
.dxeCalendarButton,
.dxeSpinIncButton,
.dxeSpinDecButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton
{
	vertical-align: middle;
	border: 1px solid #7f7f7f;
	cursor: pointer;
} 

.dxEditors_edtDropDown {
    background-position: -95px 0px;
    width: 10px;
    height: 14px;
}

.dxEditors_edtError,
.dxEditors_edtCalendarPrevYear,
.dxEditors_edtCalendarPrevYearDisabled,
.dxEditors_edtCalendarPrevMonth,
.dxEditors_edtCalendarPrevMonthDisabled,
.dxEditors_edtCalendarNextMonth,
.dxEditors_edtCalendarNextMonthDisabled,
.dxEditors_edtCalendarNextYear,
.dxEditors_edtCalendarNextYearDisabled,
.dxEditors_edtCalendarFNPrevYear,
.dxEditors_edtCalendarFNNextYear,
.dxEditors_edtEllipsis,
.dxEditors_edtEllipsisDisabled,
.dxEditors_edtDropDown,
.dxEditors_edtDropDownDisabled,
.dxEditors_edtSpinEditIncrementImage,
.dxEditors_edtSpinEditIncrementImageDisabled,
.dxEditors_edtSpinEditDecrementImage,
.dxEditors_edtSpinEditDecrementImageDisabled,
.dxEditors_edtSpinEditLargeIncImage,
.dxEditors_edtSpinEditLargeIncImageDisabled,
.dxEditors_edtSpinEditLargeDecImage,
.dxEditors_edtSpinEditLargeDecImageDisabled
{
	display:block;
	margin:auto;
}

.dxEditors_edtError,
.dxEditors_edtCalendarPrevYear,
.dxEditors_edtCalendarPrevYearDisabled,
.dxEditors_edtCalendarPrevMonth,
.dxEditors_edtCalendarPrevMonthDisabled,
.dxEditors_edtCalendarNextMonth,
.dxEditors_edtCalendarNextMonthDisabled,
.dxEditors_edtCalendarNextYear,
.dxEditors_edtCalendarNextYearDisabled,
.dxEditors_edtCalendarFNPrevYear,
.dxEditors_edtCalendarFNNextYear,
.dxEditors_edtRadioButtonChecked,
.dxEditors_edtRadioButtonUnchecked,
.dxEditors_edtRadioButtonCheckedDisabled,
.dxEditors_edtRadioButtonUncheckedDisabled,
.dxEditors_edtEllipsis,
.dxEditors_edtEllipsisDisabled,
.dxEditors_edtDropDown,
.dxEditors_edtDropDownDisabled,
.dxEditors_edtDETSClockFace,
.dxEditors_edtDETSHourHand,
.dxEditors_edtDETSMinuteHand,
.dxEditors_edtDETSSecondHand,
.dxEditors_edtSpinEditIncrementImage,
.dxEditors_edtSpinEditIncrementImageDisabled,
.dxEditors_edtSpinEditDecrementImage,
.dxEditors_edtSpinEditDecrementImageDisabled,
.dxEditors_edtSpinEditLargeIncImage,
.dxEditors_edtSpinEditLargeIncImageDisabled,
.dxEditors_edtSpinEditLargeDecImage,
.dxEditors_edtSpinEditLargeDecImageDisabled,
.dxEditors_fcadd,
.dxEditors_fcaddhot,
.dxEditors_fcremove,
.dxEditors_fcremovehot,
.dxEditors_fcgroupaddcondition,
.dxEditors_fcgroupaddgroup,
.dxEditors_fcgroupremove,
.dxEditors_fcopany,
.dxEditors_fcopbegin,
.dxEditors_fcopbetween,
.dxEditors_fcopcontain,
.dxEditors_fcopnotcontain,
.dxEditors_fcopnotequal,
.dxEditors_fcopend,
.dxEditors_fcopequal,
.dxEditors_fcopgreater,
.dxEditors_fcopgreaterorequal,
.dxEditors_fcopnotblank,
.dxEditors_fcopblank,
.dxEditors_fcopless,
.dxEditors_fcoplessorequal,
.dxEditors_fcoplike,
.dxEditors_fcopnotany,
.dxEditors_fcopnotbetween,
.dxEditors_fcopnotlike,
.dxEditors_fcgroupand,
.dxEditors_fcgroupor,
.dxEditors_fcgroupnotand,
.dxEditors_fcgroupnotor,
.dxEditors_caRefresh,
.dxEditors_edtTBDecBtn,
.dxEditors_edtTBIncBtn,
.dxEditors_edtTBMainDH,
.dxEditors_edtTBSecondaryDH,
.dxEditors_edtTBIncBtnDisabled,
.dxEditors_edtTBDecBtnDisabled,
.dxEditors_edtTBMainDHDisabled,
.dxEditors_edtTBSecondaryDHDisabled
{   
    background-repeat: no-repeat;
    background-color: transparent;
}

        .style6
        {
            height: 6px;
        }
        .style7
        {
            width: 68px;
        }
        .style8
        {
            width: 43px;
        }
        .style9
        {
            width: 59px;
        }
        .style10
        {
            width: 53px;
        }
        .style11
        {
            width: 58px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Posição 01:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos1" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos1">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas1" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas1">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas1" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas1">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Posição 02:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos2" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos2">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel17" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas2" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas2">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel28" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas2" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas2">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Posição 03:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos3" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos3">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel18" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas3" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas3">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel29" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas3" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas3">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                    Text="Posição 04:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos4" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos4">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas4" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas4">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel30" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas4" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas4">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                    Text="Posição 05:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos5" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos5">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel20" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas5" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas5">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel31" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas5" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas5">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style7">
                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                    Text="Posição 06:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlPos6" runat="server" 
                                    Width="100%" ClientInstanceName="ddlPos6">
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style8">
                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server" 
                                    Text="Linhas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-bottom: 5px; padding-right: 10px;" class="style9">
                                <dxe:ASPxComboBox ID="ddlLinhas6" runat="server" 
                                    Width="100%" ClientInstanceName="ddlLinhas6">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                        <td  class="style10">
                                <dxe:ASPxLabel ID="ASPxLabel32" runat="server" 
                                    Text="Colunas:" Wrap="False">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td  class="style11">
                                <dxe:ASPxComboBox ID="ddlColunas6" runat="server" 
                                    Width="100%" ClientInstanceName="ddlColunas6">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    </td>
            </tr>
            <tr>
                <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    callbackSalvar.PerformCallback();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>



 </td></tr></TBODY></table>
                </td>
            </tr>
        </table>
    
        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
            ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    window.top.fechaModal();
	window.top.atualizaTela();
}" />
        </dxcb:ASPxCallback>
    
    </div>
    </form>
</body>
</html>
