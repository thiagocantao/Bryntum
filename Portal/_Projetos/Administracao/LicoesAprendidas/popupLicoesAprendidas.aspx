<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupLicoesAprendidas.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_popupLicoesAprendidas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #divCabecalho {
            display: flex;
            flex-direction: row;
            font-family: Verdana;
            font-weight: 600;
            font-size: 14px
        }

        .iniciaisMaiusculas {
            text-transform: capitalize !important
        }

        .divDoBotaoSelecionar {
            position: absolute;
            bottom: 0px;
            right: 0px;
            display: flex;
            flex-direction: row
        }

        .colunasDoCabecalho {
            width: 12.25%;
            border: solid;
            border-color: gainsboro;
            border-width: 1px
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="divDoBotaoSelecionar">
                <div>
                    <dxe:ASPxButton ID="btnSelecionar" runat="server" ClientInstanceName="btnSelecionar" CssClass="iniciaisMaiusculas"
                        Text="Selecionar" AutoPostBack="False"
                        Width="100px">
                        <ClientSideEvents Click="function(s, e) {
                           window.top.retornoModal = popup_lca_CodigoLicaoAprendida.toString();
                            window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </div>
            </div>
            <dxwgv:ASPxGridView ID="gvLicoesAprendidas" runat="server" AutoGenerateColumns="False"
                ClientInstanceName="gvLicoesAprendidas" KeyFieldName="CodigoLicaoAprendida" Width="100%">
                <Columns>
                    <dxtv:GridViewDataComboBoxColumn Caption="Lição Aprendida" FieldName="NomeLicaoAprendida" VisibleIndex="2" Width="350px" FixedStyle="Left">
                        <PropertiesComboBox MaxLength="100" TextField="DescricaoContexto" ValueField="CodigoContexto">
                            <ValidationSettings>
                                <RequiredField ErrorText="Informe um valor válido para o campo." IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                        <EditFormSettings CaptionLocation="Top" />
                    </dxtv:GridViewDataComboBoxColumn>
                    <dxtv:GridViewDataTextColumn FieldName="DescricaoLicaoAprendida" VisibleIndex="3" Caption="Descrição" Width="350px">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="Status" VisibleIndex="4" Caption="Status" Width="250px">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="FalhaouBoaPratica" VisibleIndex="5" Caption="Falha/Boa Pratica" Width="250px">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="Conhecimento" VisibleIndex="6" Caption="Conhecimento" Width="500px">
                        <PropertiesTextEdit EncodeHtml="False">
                        </PropertiesTextEdit>
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="MudancaouApendizagem" VisibleIndex="7" Caption="Mudança"  Width="300px">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="Relevancia" VisibleIndex="8" Caption="Relevância">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="ProjetoRelacionado" VisibleIndex="9" Caption="Projeto Relacionado" Width="350px">
                        <PropertiesTextEdit EncodeHtml="False">
                        </PropertiesTextEdit>
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption=" " FieldName="CodigoLicaoAprendida" VisibleIndex="1" Width="50px"  FixedStyle="Left">
                        <DataItemTemplate>
                            <input type="radio" id="radioSelecionaLicaoAprendida" name="radioSelecionaLicaoAprendida" value="<%# Eval("CodigoLicaoAprendida").ToString() %>" onclick="selecionar('<%# Eval("CodigoLicaoAprendida") %>', this.checked)" style="width:20px;height:20px"/>
                        </DataItemTemplate>
                    </dxtv:GridViewDataTextColumn>
                </Columns>
                <Settings VerticalScrollableHeight="170" VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                <ClientSideEvents Init="function(s, e) {
   var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
   s.SetHeight(sHeight);
}" />
                <SettingsPager Mode="ShowAllRecords" Visible="False">
                </SettingsPager>
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                <SettingsPopup>
                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                        AllowResize="True" Width="600px" />

                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
            </dxwgv:ASPxGridView>
        </div>
    </form>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 80;
        var sWidth = Math.max(0, document.documentElement.clientWidth) - 10;
        var sWidthCabecalho = Math.max(0, document.documentElement.clientWidth) - 27;
        var popup_lca_CodigoLicaoAprendida = -1;

        function selecionar(idSelecionado, estaChecado) {
            popup_lca_CodigoLicaoAprendida = idSelecionado;
        }

    </script>
</body>
</html>
