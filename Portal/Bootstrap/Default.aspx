<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap/Bootstrap.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Bootstrap_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cabecalho" Runat="Server">
    <style type="text/css">

        #tituloPagina {
            color: #0094ff;
        }

        #conteudoGridTelaCheia {
            height: calc(100vh - 174px);
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" Runat="Server">

    <h1 id="tituloPagina" class="text-center">Grid</h1>
    <div id="conteudoGridTelaCheia">
    <dxcp:ASPxGridView ID="gvDados" ClientInstanceName="gvDados" runat="server" Width="100%">
        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" />
        <SettingsPager PageSize="10" />
        <ClientSideEvents
            Init="function(s, e) {
                gridTelaCheia();
                ASPxClientUtils.AttachEventToElement(window, 'resize', function(e) {
                    gridTelaCheia();
                });
            }"
            EndCallback="function(s, e) {
                gridTelaCheia();
                ASPxClientUtils.AttachEventToElement(window, 'resize', function(e) {
                    gridTelaCheia();
                });
            }" />
    </dxcp:ASPxGridView>
    </div>

    <script>

        function gridTelaCheia() {
            var p = document.getElementById("conteudoGridTelaCheia");
            //var p = document.getElementById("formulario");
            //var h = p.clientHeight - 60;
            var h = p.clientHeight;
            //alert(h);
            gvDados.SetHeight(h);
        }

    </script>

</asp:Content>
