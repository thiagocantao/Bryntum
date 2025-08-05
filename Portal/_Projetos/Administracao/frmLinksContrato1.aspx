<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLinksContrato1.aspx.cs" Inherits="_Projetos_Administracao_frmLinksContrato1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Agil/taskboard/Scripts/marvinj-1.0.js"></script>
    <script type="text/javascript">
        const urlParams = new URLSearchParams(window.location.search);
        const somenteLeitura = (urlParams.get('RO') === 'S');
        //alert(somenteLeitura);
        function atualizaIconesDaTela() {
            txtNumeroProcesso.SetReadOnly(somenteLeitura);

            //canvasBtnVincular
            var canvasBtnVincular = document.getElementById("canvasBtnVincular");
            var original_canvasBtnVincular = new MarvinImage();
            var image_canvasBtnVincular;

            original_canvasBtnVincular.load("../../imagens/check_ok.png", function () {
                image_canvasBtnVincular = this.clone();
                if (somenteLeitura == true) {
                    Marvin.grayScale(this, image_canvasBtnVincular);
                }
                image_canvasBtnVincular.draw(canvasBtnVincular);
            });

            if (somenteLeitura == false) {
                canvasBtnVincular.addEventListener('click',
                    function (e) {
                        pnCallback.PerformCallback();
                        hfProcessoSenar.PerformCallback();
                    });
            }


            //canvasBtnDesvincular  <img src="../../imagens/delete.png" />
            var canvasBtnDesVincular = document.getElementById("canvasBtnDesVincular");
            var original_canvasBtnDesVincular = new MarvinImage();
            var image_canvasBtnDesVincular;

            original_canvasBtnDesVincular.load("../../imagens/delete.png", function () {
                image_canvasBtnDesVincular = this.clone();
                if (somenteLeitura == true) {
                    Marvin.grayScale(this, image_canvasBtnDesVincular);
                }
                image_canvasBtnDesVincular.draw(canvasBtnDesVincular);
            });

            if (somenteLeitura == false) {
                canvasBtnDesVincular.addEventListener('click',
                    function (e) {
                        var funcObj = {
                            funcaoClickOK: function () {
                                pnCallback.PerformCallback('DESVINCULAR');
                                hfProcessoSenar.PerformCallback('DESVINVULADO');
                            }
                        }
                        window.top.mostraConfirmacao('Ao fazer a desvinculação do processo, todas as informações obtidas do Senar Docs para este contrato serão perdidas! Confirma a desvinculação?', function () { funcObj['funcaoClickOK']() }, null);
                        e.processOnServer = false;
                    });
            }


            //canvasBtnSincronizar
            var canvasBtnSincronizar = document.getElementById("canvasBtnSincronizar");
            var original_canvasBtnSincronizar = new MarvinImage();
            var image_canvasBtnSincronizar;

            original_canvasBtnSincronizar.load("../../imagens/refresh.png", function () {
                image_canvasBtnSincronizar = this.clone();
                if (somenteLeitura == true) {
                    Marvin.grayScale(this, image_canvasBtnSincronizar);
                }
                image_canvasBtnSincronizar.draw(canvasBtnSincronizar);
                if (somenteLeitura == false) {
                    canvasBtnSincronizar.addEventListener('click', function (e) { pnCallback.PerformCallback(); });
                }

            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <dxwgv:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                Width="100%" OnCallback="hfCarregaDocumentos_CustomCallback">

                <ClientSideEvents EndCallback="function(s, e) {
	atualizaIconesDaTela();
}" />

                <Paddings Padding="0px" />

                <PanelCollection>
                    <dxwgv:PanelContent runat="server">
                            <dxwgv:ASPxHiddenField runat="server" ClientInstanceName="hfProcessoSenar" ID="hfProcessoSenar" OnCustomCallback="hfProcessoSenar_CustomCallback">
                                <ClientSideEvents
                                    EndCallback="function(s, e) {
   if (hfProcessoSenar.Get('possuiProcessoSenar') == 'N')
    {
                        window.top.mostraMensagem('Este número de processo não foi encontrado no Senar Docs!', 'erro', true, false, null);
                        }
                        if (hfProcessoSenar.Get('possuiOutroProcessoSenar') == 'S')
    {
                        window.top.mostraMensagem('Vinculação não permitida, pois este processo já está vinculado a outro contrato!', 'erro', true, false, null);
                        } 
                       if (hfProcessoSenar.Get('processoDesvinculado') == 'S' && hfProcessoSenar.Get('possuiProcessoSenar') == 'S')
    {
                        window.top.mostraMensagem('Desvinculação efetuada com sucesso!', 'sucesso', true, false, null);
                        } 
}" />
                            </dxwgv:ASPxHiddenField>
                            <dxwgv:ASPxGridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDocumentos" Width="100%">
                                <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                                    AllowSort="False" ConfirmDelete="True" />
                                <StylesPopup>
                                    <EditForm>
                                        <Header Font-Bold="True">
                                        </Header>
                                        <MainArea Font-Bold="False"></MainArea>
                                    </EditForm>
                                </StylesPopup>
                                <Styles>
                                    <Header Font-Bold="False" Wrap="True">
                                    </Header>
                                    <HeaderPanel Font-Bold="False">
                                    </HeaderPanel>
                                    <TitlePanel Font-Bold="True">
                                    </TitlePanel>
                                </Styles>
                                <Templates>
                                    <TitlePanel>
                                        Documentos vinculados ao processo no Senar Docs
                                    </TitlePanel>
                                </Templates>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>

                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3" />
                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                </SettingsCommandButton>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        VerticalOffset="-40" Width="600px" AllowResize="True" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />

                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                </SettingsPopup>
                                <SettingsText ConfirmDelete="Retirar a Parcela para este contrato?" PopupEditFormCaption="Parcela do Contrato" />
                                <ClientSideEvents CustomButtonClick="function(s, e) {
	//onClick_CustomButtomGridParcelas(s, e);
    if(e.buttonID == 'btnEditar')
    {
         if(s.cp_utilizaConvenio == undefined || s.cp_utilizaConvenio == &quot;N&quot;)
         {
           s.GetRowValues(s.GetFocusedRowIndex(), 'NumeroAditivoContrato;NumeroParcela;CodigoProjetoParcela;CodigoContrato;', mostraPopupParcelasContrato);     
         }
         else
         {
           s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro;CodigoContrato', mostraPopupLancamentoFinanceiro);
         }
    }
}" Init="function(s, e) {                         
	  var sHeight = Math.max(0, document.documentElement.clientHeight) - 25;
                  s.SetHeight(sHeight);
}" />
                                <Settings ShowFooter="False" VerticalScrollBarMode="Visible" />
                            </dxwgv:ASPxGridView>
                    </dxwgv:PanelContent>
                </PanelCollection>
            </dxwgv:ASPxCallbackPanel>
        </div>
        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportedRowType="All" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
            <Styles>
                <Header Font-Bold="True"></Header>

                <GroupFooter Font-Bold="True"></GroupFooter>

                <GroupRow Font-Bold="True"></GroupRow>

                <Title Font-Bold="True"></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>

</form>

</body>
</html>
