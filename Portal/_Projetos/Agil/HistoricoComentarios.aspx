<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoComentarios.aspx.cs" Inherits="administracao_frmAditivosContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
       

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Comentario;TituloStatusItem;DataMovimento;NomeUsuario;EsforcoReal', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {
            var comentario = (values[0] != null ? values[0] : "");
            var fase = (values[1] != null ? values[1] : "");
            var data = values[2];
            var responsavel = values[3];
            var EsforcoReal = (values[4] != null ? values[4] : "");

            txtFase.SetText(fase);
            txtComentario.SetText(comentario);
            txtData.SetValue(data);
            txtResponsavel.SetText(responsavel);
        }

        function fechaTelaEdicao() {
            pcUsuarioIncluido.Hide();
            txtFase.SetText('');
            txtComentario.SetText('');
            txtData.SetText('');
            txtResponsavel.SetText('');
        }   
       
    </script>
    <style type="text/css">


        .style5
        {
            width: 5px;
        }
        .style1
        {
            height: 10px;
        }
        .style6
        {
            height: 5px;
        }
        .style7
        {
            width: 10px;
            height: 5px;
        }
        .style8
        {
            width: 100%;
        }
        .style13
        {
            width: 129px;
        }
        .style14
        {
            width: 245px;
        }
        .style15
        {
            width: 280px;
        }
        .textoComIniciaisMaiuscula {
            text-transform: capitalize
        }
        </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td class="style6">
            </td>
            <td class="style6">
            </td>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
               
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoMovimentoItem" AutoGenerateColumns="False" 
         ID="gvDados"         
        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" Width="100%">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
    	
		OnGridFocusedRowChanged(gvDados, true);
		pcDados.Show();
  	
}
"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="50px" VisibleIndex="0" 
        Caption=" ">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="Comentario" Name="Comentario" 
        Caption="Comentário" VisibleIndex="4">
    <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" />
</dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Fase" FieldName="TituloStatusItem" 
        ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuario" 
        VisibleIndex="2" Width="200px">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Data" FieldName="DataMovimento" 
        VisibleIndex="1" Width="130px">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">
        </PropertiesDateEdit>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataTextColumn Caption="Esforço Real" FieldName="EsforcoReal" VisibleIndex="5" Width="100px">
    </dxtv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" 
         ShowHeaderFilterBlankItems="False" ShowFooter="True"></Settings>

<SettingsText ></SettingsText>
     <TotalSummary>
         <dxtv:ASPxSummaryItem DisplayFormat="{0}" FieldName="EsforcoReal" ShowInColumn="Esforço Real" ShowInGroupFooterColumn="Esforço Real" SummaryType="Sum" ValueDisplayFormat="{0}" />
     </TotalSummary>
</dxwgv:ASPxGridView>
<script language="javascript" type="text/javascript">
    if (window.innerHeight > 30) {
        gvDados.SetHeight(window.innerHeight - 30);
    }
        </script>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" 
        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="700px" 
         ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td >
                                    <table cellpadding="0" cellspacing="0" class="formulario-colunas">
                                        <tr>
                                            <td class="style13">
                                                <dxtv:ASPxLabel ID="lblObservacoes1" runat="server" EncodeHtml="False" 
                                                     Text="Data:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td class="style15">
                                                <dxtv:ASPxLabel ID="lblObservacoes2" runat="server" EncodeHtml="False" 
                                                     Text="Responsável:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxtv:ASPxLabel ID="lblObservacoes0" runat="server" EncodeHtml="False" 
                                                     Text="Fase:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style13" >
                                                <dxtv:ASPxTextBox ID="txtData" runat="server" ClientEnabled="False" 
                                                    ClientInstanceName="txtData" DisplayFormatString="dd/MM/yyyy HH:mm" 
                                                     Width="100%">
                                                </dxtv:ASPxTextBox>
                                            </td>
                                            <td class="style15" >
                                                <dxtv:ASPxTextBox ID="txtResponsavel" runat="server" ClientEnabled="False" 
                                                    ClientInstanceName="txtResponsavel"  
                                                    Width="100%">
                                                </dxtv:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxtv:ASPxTextBox ID="txtFase" runat="server" ClientEnabled="False" 
                                                    ClientInstanceName="txtFase"  Width="100%">
                                                </dxtv:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" >
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <dxtv:ASPxLabel ID="lblObservacoes" runat="server" 
                                        ClientInstanceName="lblObservacoes" EncodeHtml="False" 
                                        Text="Comentário: &amp;nbsp;">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="txtComentario" runat="server" 
                                        ClientInstanceName="txtComentario"  
                                        Rows="15" Width="100%" ClientEnabled="False">                                       
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" 
                                        ClientInstanceName="btnFechar"  
                                        Text="Fechar" Width="90px" CssClass="textoComIniciaisMaiuscula">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDados.Hide();
}" />
                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                            PaddingRight="0px" PaddingTop="0px" />
                                    </dxtv:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
        </tbody>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

            </td>
            <td>
            </td>
        </tr>

    </table>
</div>
    </form>
</body>
</html>

