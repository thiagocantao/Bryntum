<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogCargaSMD.aspx.cs" Inherits="administracao_DesfazerCargaSMD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
.dxbButton
{
	color: #000000;
	font: normal 12px Tahoma, Geneva, sans-serif;
	vertical-align: middle;
	border: 1px solid #7F7F7F;
	padding: 1px;
	cursor: pointer;
}
        .style2
        {
            border-width: 0px;
            padding-left: 8px;
            padding-right: 8px;
            padding-top: 3px;
            padding-bottom: 4px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td style="padding-bottom: 10px">
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
                         KeyFieldName="CodigoCarga" 
                        Width="100%">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Unidade de Negócio" FieldName="UnidadeNegocio" 
                                VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Indicador" 
                                FieldName="Indicador" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ocorrências" 
                                FieldName="QuantidadeOcorrencia" VisibleIndex="2" Width="80px">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="300" />
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                        <dxe:ASPxButton ID="btnFechar" runat="server" 
                            Text="Fechar" Width="90px" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                            <Paddings Padding="0px" />
                        </dxe:ASPxButton>
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
