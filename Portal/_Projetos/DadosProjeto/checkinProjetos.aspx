<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkinProjetos.aspx.cs" Inherits="_Projetos_DadosProjeto_checkinProjetos" Title="Portal da Estratégia" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>To Do List</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    
    
    
<script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
</script>
</head>
<body style="margin:0">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="padding-left: 5px; padding-top: 5px">
    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
         KeyFieldName="CodigoCronogramaProjeto"
        Width="99%" OnCustomCallback="gvDados_CustomCallback">
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <ClientSideEvents CustomButtonClick="function(s, e) 
{
     if(e.buttonID == &quot;btnCheckin&quot;)
     {
         e.processOnServer = false;
             var sentenca = &quot;Ao fazer o desbloqueio as atualiza&#231;&#245;es pendentes ser&#227;o perdidas. Deseja realmente desbloquear o projeto?&quot;
             window.top.mostraMensagem(sentenca , 'confirmacao', true, true, desbloquearProjeto);
         
     }	
}
" />
        <Columns>
            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="40px">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCheckin" Text="Desbloquear o Projeto Selecionado">
                        <Image Url="~/imagens/anexo/download.png" />
                    </dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Por" FieldName="NomeUsuarioCheckoutCronograma"
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Bloqueio Feito Em" FieldName="DataCheckoutCronograma"
                VisibleIndex="3">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Codigo do Projeto" FieldName="CodigoProjeto"
                Visible="False" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Codigo Cronograma Projeto" FieldName="CodigoCronogramaProjeto"
                Visible="False" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <Settings VerticalScrollBarMode="Visible" />
    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


