<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanoDeTrabalho.aspx.cs" Inherits="_Projetos_Administracao_PlanoDeTrabalho" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            cursor: pointer;
            border-style: none;
            border-color: inherit;
            border-width: 0px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
            KeyFieldName="Codigo" AutoGenerateColumns="False" Width="100%" 
             ID="gvDados" 
            onhtmldatacellprepared="gvDados_HtmlDataCellPrepared">
<Columns>
<dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FixedStyle="Left">
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Nº" VisibleIndex="1" Width="45px" 
        FieldName="Numero" FixedStyle="Left" Name="Numero">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Ações / Atividades" VisibleIndex="2" 
        FieldName="Descricao" FixedStyle="Left" Name="Descricao" Width="320px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Início" VisibleIndex="3" Width="90px" 
        FieldName="Inicio" Name="Inicio">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Término" VisibleIndex="4" Width="90px" 
        FieldName="Termino" Name="Termino">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Institucional" VisibleIndex="5" 
        Width="110px" FieldName="Institucional" Name="Institucional">
        <Settings AllowAutoFilter="False" AllowHeaderFilter="True" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="6" 
        Width="200px" FieldName="Responsavel" Name="Responsavel">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Metas da Ação" VisibleIndex="7" 
        Width="250px" FieldName="Codigo" Name="Metas">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption=" - " VisibleIndex="8" Width="400px" 
        FieldName="Codigo" Name="Parcerias">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Marcos" VisibleIndex="9" Width="280px" 
        FieldName="Codigo" Name="Marcos">
        <Settings AllowAutoFilter="False" />
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" 
         HorizontalScrollBarMode="Visible"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>

    </div>
    </form>
</body>
</html>
