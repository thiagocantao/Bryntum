<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjetivosTema.aspx.cs" Inherits="_Estrategias_mapa_Macrometa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <title></title>
    <style type="text/css">

.dxnbControl 
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: black;
	background-color: white;
}
.dxnbGroupHeader,
.dxnbGroupHeaderCollapsed
{
	text-align: left;
}
.dxnbGroupHeader
{
	font-weight: bold;
	background-color: #E0E0E0;
	border: 1px solid #A8A8A8;
	padding: 4px 10px;
}
.dxnbImgCellRight
{
	padding-left: 4px;
}

.dxWeb_nbCollapse {
    background-position: -129px 0px;
    width: 13px;
    height: 15px;
}

.dxWeb_rpHeaderTopLeftCorner,
.dxWeb_rpHeaderTopRightCorner,
.dxWeb_rpBottomLeftCorner,
.dxWeb_rpBottomRightCorner,
.dxWeb_rpTopLeftCorner,
.dxWeb_rpTopRightCorner,
.dxWeb_rpGroupBoxBottomLeftCorner,
.dxWeb_rpGroupBoxBottomRightCorner,
.dxWeb_rpGroupBoxTopLeftCorner,
.dxWeb_rpGroupBoxTopRightCorner,
.dxWeb_mHorizontalPopOut,
.dxWeb_mVerticalPopOut,
.dxWeb_mVerticalPopOutRtl,
.dxWeb_mSubMenuItem,
.dxWeb_mSubMenuItemChecked,
.dxWeb_mScrollUp,
.dxWeb_mScrollDown,
.dxWeb_tcScrollLeft,
.dxWeb_tcScrollRight,
.dxWeb_tcScrollLeftHover,
.dxWeb_tcScrollRightHover,
.dxWeb_tcScrollLeftPressed,
.dxWeb_tcScrollRightPressed,
.dxWeb_tcScrollLeftDisabled,
.dxWeb_tcScrollRightDisabled,
.dxWeb_nbCollapse,
.dxWeb_nbExpand,
.dxWeb_splVSeparator,
.dxWeb_splVSeparatorHover,
.dxWeb_splHSeparator,
.dxWeb_splHSeparatorHover,
.dxWeb_splVCollapseBackwardButton,
.dxWeb_splVCollapseBackwardButtonHover,
.dxWeb_splHCollapseBackwardButton,
.dxWeb_splHCollapseBackwardButtonHover,
.dxWeb_splVCollapseForwardButton,
.dxWeb_splVCollapseForwardButtonHover,
.dxWeb_splHCollapseForwardButton,
.dxWeb_splHCollapseForwardButtonHover,
.dxWeb_pcCloseButton,
.dxWeb_pcPinButton,
.dxWeb_pcRefreshButton,
.dxWeb_pcCollapseButton,
.dxWeb_pcMaximizeButton,
.dxWeb_pcSizeGrip,
.dxWeb_pcSizeGripRtl,
.dxWeb_pPopOut,
.dxWeb_pPopOutDisabled,
.dxWeb_pAll,
.dxWeb_pAllDisabled,
.dxWeb_pPrev,
.dxWeb_pPrevDisabled,
.dxWeb_pNext,
.dxWeb_pNextDisabled,
.dxWeb_pLast,
.dxWeb_pLastDisabled,
.dxWeb_pFirst,
.dxWeb_pFirstDisabled,
.dxWeb_tvColBtn,
.dxWeb_tvColBtnRtl,
.dxWeb_tvExpBtn,
.dxWeb_tvExpBtnRtl,
.dxWeb_fmFolder,
.dxWeb_fmFolderLocked,
.dxWeb_fmCreateButton,
.dxWeb_fmMoveButton,
.dxWeb_fmRenameButton,
.dxWeb_fmDeleteButton,
.dxWeb_fmRefreshButton,
.dxWeb_fmDwnlButton,
.dxWeb_fmCreateButtonDisabled,
.dxWeb_fmMoveButtonDisabled,
.dxWeb_fmRenameButtonDisabled,
.dxWeb_fmDeleteButtonDisabled,
.dxWeb_fmRefreshButtonDisabled,
.dxWeb_fmDwnlButtonDisabled,
.dxWeb_fmThumbnailCheck,
.dxWeb_ucClearButton,
.dxWeb_isPrevBtnHor,
.dxWeb_isNextBtnHor,
.dxWeb_isPrevBtnVert,
.dxWeb_isNextBtnVert,
.dxWeb_isPrevPageBtnHor,
.dxWeb_isNextPageBtnHor,
.dxWeb_isPrevPageBtnVert,
.dxWeb_isNextPageBtnVert,
.dxWeb_isPrevBtnHorDisabled,
.dxWeb_isNextBtnHorDisabled,
.dxWeb_isPrevBtnVertDisabled,
.dxWeb_isNextBtnVertDisabled,
.dxWeb_isPrevPageBtnHorDisabled,
.dxWeb_isNextPageBtnHorDisabled,
.dxWeb_isPrevPageBtnVertDisabled,
.dxWeb_isNextPageBtnVertDisabled,
.dxWeb_isDot,
.dxWeb_isDotDisabled,
.dxWeb_isDotSelected
 {
    background-repeat: no-repeat;
    background-color: transparent;
    display:block;
}

.dxnbGroupContent
{
	color: #1E3695;
	border: 1px solid #A8A8A8;
	padding: 5px;
}
.dxnbItem,
.dxnbItemHover,
.dxnbItemSelected,
.dxnbBulletItem,
.dxnbBulletItemHover,
.dxnbBulletItemSelected
{
	text-align: left;
}
.dxnbItem
{
	padding: 4px 5px 5px;
}
.dxnbItem,
.dxnbLargeItem,
.dxnbBulletItem
{
	color: black;
}
.dxnbGroupSpacing,
.dxnbItemSpacing
{
	width: 100%;
	height: 1px;
}
        .style1
        {
            padding: 11px;
        }
        .style2
        {
            
            
            font-weight: normal;
            width: 100%;
            cursor: pointer;
            white-space: normal;
            padding-left: 10px;
            padding-right: 2px;
            padding-top: 2px;
            padding-bottom: 2px;
        }
        .style3
        {
            border-width: 0px;
        }
    </style>
    </head>
<body style="overflow-x:hidden; margin:0px">
    <form id="form1" runat="server">    
        <table cellpadding="0" cellspacing="0" width="100%">                      
            <tr>
                <td align="center">
                                <dxnb:ASPxNavBar ID="nb" runat="server" EncodeHtml="False" 
                        GroupSpacing="4px" Width="100%" Font-Bold="False">
                                    <GroupContentStyle>
                                        <Paddings PaddingBottom="1px" PaddingTop="1px" />
                                    </GroupContentStyle>
                                    <ItemStyle BackColor="White" />
                                    <GroupHeaderStyle Wrap="True">
                                        <Paddings Padding="2px" PaddingLeft="10px" />
                                    </GroupHeaderStyle>
                                    <ClientSideEvents ExpandedChanged="function(s, e) {
	
}" />
                                    <DisabledStyle ForeColor="Black">
                                    </DisabledStyle>
                                </dxnb:ASPxNavBar>
                    </td>
            </tr>
        </table>
    </form>
</body>
</html>
