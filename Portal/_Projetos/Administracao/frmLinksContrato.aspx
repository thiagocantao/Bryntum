<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLinksContrato.aspx.cs" Inherits="_Projetos_Administracao_frmLinksContrato" %>

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

            <div id="divLinha1" style="display: flex; flex-direction: row; padding-bottom: 10px">
                <div id="divCampoNumeroContrato" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="lblNumeroContrato" runat="server" ClientInstanceName="lblNumeroContrato"
                            Text="Número do Contrato:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxTextBox ID="txtNumeroContrato_tabItens" runat="server" ClientInstanceName="txtNumeroContrato_tabItens"
                            MaxLength="50" TabIndex="1" Width="100%" ReadOnly="True">
                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </div>
                </div>
                <div id="divCampoTipoContrato" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblTipoContrato"
                            Text="Tipo de Contrato:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxComboBox ID="ddlTipoContrato_tabItens" runat="server" ClientInstanceName="ddlTipoContrato_tabItens"
                            TabIndex="2" TextField="DescricaoTipoContrato"
                            ValueField="CodigoTipoContrato" ValueType="System.Int32" Width="100%" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </div>
                </div>
                <div id="divCampoStatus" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblTipoContrato"
                            Text="Status">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxtv:ASPxComboBox ID="ddlStatusComplementarContrato_tabItens" runat="server" ClientInstanceName="ddlStatusComplementarContrato_tabItens" TabIndex="3" Width="100%" ReadOnly="True">
                            <Items>
                                <dxe:ListEditItem Text="Ativo" Value="A" />
                                <dxe:ListEditItem Text="Não Ativo" Value="I" />
                            </Items>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxtv:ASPxComboBox>
                    </div>
                </div>
                <div id="divCampoInicioVigencia" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                            Text="Início de Vigência">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxDateEdit ID="ddlInicioDeVigencia_tabItens" PopupVerticalAlign="TopSides" runat="server" ClientInstanceName="ddlInicioDeVigencia_tabItens"
                            DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                            EncodeHtml="False" TabIndex="10" UseMaskBehavior="True"
                            Width="100%" ReadOnly="True">
                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                            </CalendarProperties>
                            <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}"
                                ValueChanged="function(s, e) {
	
}" />
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxDateEdit>
                    </div>
                </div>
                <div id="divCampoTerminoVigencia" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                            Text="Término de Vigência:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>

                        <dxe:ASPxDateEdit ID="ddlTerminoDeVigencia_tabItens" PopupVerticalAlign="TopSides" runat="server" ClientInstanceName="ddlTerminoDeVigencia_tabItens"
                            DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                            EncodeHtml="False" TabIndex="11" UseMaskBehavior="True"
                            Width="100%" ReadOnly="True">
                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                            </CalendarProperties>
                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxDateEdit>

                    </div>
                </div>
            </div>
            <dxwgv:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                Width="100%" OnCallback="hfCarregaDocumentos_CustomCallback">

                <ClientSideEvents EndCallback="function(s, e) {
	atualizaIconesDaTela();
}" />

                <PanelCollection>
                    <dxwgv:PanelContent runat="server">

                        <div id="divTipoProcesso" style="display: flex; flex-direction: row">
                            <div id="divCampoTipoProcesso" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblTipoProcesso"
                            Text="Tipo do Processo:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxComboBox ID="ddlTipoProcesso_tabItens" runat="server" ClientInstanceName="ddlTipoProcesso_tabItens"
                            TabIndex="2" TextField="DescricaoTipoFolder"
                            ValueField="CodigoTipoFolder" ValueType="System.Decimal" Width="100%" >
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </div>
                </div>
                            <div id="divCampoNumeroProcesso" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                                <div>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                        Text="Número do Processo:">
                                    </dxe:ASPxLabel>
                                </div>
                                <div style="display: flex; flex-direction: row">
                                    <div>
                                        <dxcp:ASPxTextBox ID="txtNumeroProcesso" ClientInstanceName="txtNumeroProcesso" runat="server" Width="170px">
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                        </dxcp:ASPxTextBox>
                                    </div>
                                    <div style="margin-top: 10px;">
                                        <%--                                        <dxcp:ASPxImage ID="btnVincular" ClientInstanceName="btnVincular" runat="server" Cursor="pointer" ToolTip="Vincular o contrato" ShowLoadingImage="true" ImageUrl="~/imagens/check_ok.PNG">
                                            <ClientSideEvents Click="function(s, e) {pnCallback.PerformCallback(); hfProcessoSenar.PerformCallback();}" />
                                        </dxcp:ASPxImage>--%>
                                        <canvas id="canvasBtnVincular" width="16" height="21" title="Vincular o contrato" style="cursor: pointer" />
                                    </div>
                                    <div style="text-align: left; margin-left: 15px; margin-top: 10px;">
                                        <%--<dxcp:ASPxImage ID="btnDesvincular" ClientInstanceName="btnDesvincular" runat="server" Cursor="pointer" ToolTip="Desvincular o contrato" ShowLoadingImage="true" ImageUrl="~/imagens/delete.PNG">
                                            <ClientSideEvents Click="function(s, e) {
                                                                         var funcObj = { funcaoClickOK: function(s, e)
                                                                                                        {
                                                                                                            pnCallback.PerformCallback('DESVINCULAR'); 
                                                                                                            hfProcessoSenar.PerformCallback('DESVINVULADO'); 
                                                                                                         } 
                                                                                        }
                                                                           window.top.mostraConfirmacao('Ao fazer a desvinculação do processo, todas as informações obtidas do Senar Docs para este contrato serão perdidas! Confirma a desvinculação?', function(){funcObj['funcaoClickOK'](s, e)}, null);
                                                                           e.processOnServer = false;
                                                                           }" />
                                        </dxcp:ASPxImage>--%>
                                        <canvas id="canvasBtnDesVincular" width="16" height="21" title="Desvincular o contrato" style="cursor: pointer" />
                                    </div>
                                </div>
                            </div>
                            <div id="divCampoUltimaAtualizacao" style="display: flex; flex-direction: column; flex-grow: 1; margin-right: 5px">
                                <div>
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                        Text="Última Atualização:">
                                    </dxe:ASPxLabel>
                                </div>
                                <div style="display: flex; flex-direction: row">
                                    <div>
                                        <dxe:ASPxTextBox ID="txtCampoUltimaAtualizacao" ClientInstanceName="txtCampoUltimaAtualizacao" runat="server">
                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </ReadOnlyStyle>
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </div>
                                    <div style="margin-top: 10px;">
                                        <%--<dxcp:ASPxImage ID="btnSincronizar" ClientInstanceName="btnSincronizar" runat="server" Cursor="pointer" ToolTip="Atualizar dados de Integração" ShowLoadingImage="true" ImageUrl="~/imagens/refresh.PNG">
                                            <ClientSideEvents Click="function(s, e) {pnCallback.PerformCallback();}" />
                                        </dxcp:ASPxImage>--%>
                                        <canvas id="canvasBtnSincronizar" width="16" height="21" title="Atualizar dados de integração" style="cursor: pointer" />
                                    </div>
                                </div>
                            </div>
                            <div id="divAssunto1" style="display: flex; flex-direction: column; flex-grow: 4; margin-right: 5px">
                                <div>
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                        Text="Assunto:">
                                    </dxe:ASPxLabel>
                                </div>
                                <div>
                                    <dxcp:ASPxTextBox ID="txtAssunto" ClientInstanceName="txtAssunto" runat="server" Width="100%" ReadOnly="true">
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxcp:ASPxTextBox>
                                </div>
                            </div>
                            <div id="divFornecedor1" style="display: flex; flex-direction: column; flex-grow: 4; margin-right: 5px">
                                <div>
                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                        Text="Fornecedor:">
                                    </dxe:ASPxLabel>
                                </div>
                                <div>
                                    <dxe:ASPxTextBox ID="txtFornecedor" ClientInstanceName="txtFornecedor" runat="server" Width="100%" ReadOnly="true">
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </div>
                            </div>
                        </div>
                        <div id="divLinha4" style="display: flex; flex-direction: column">
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
                                    <Cell>
                                    </Cell>
                                    <EditForm>
                                    </EditForm>
                                    <EditFormCell>
                                    </EditFormCell>
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
}"
                                    EndCallback="function(s, e) {
    try{
    pnPagoAcumulado.PerformCallback(s.cpCodigoContrato);    
    pnSaldo.PerformCallback(s.cpCodigoContrato);
    pnPrevistoAcumulado.PerformCallback(s.cpCodigoContrato);
   }
    catch(e){
                
   }
}"
                                    Init="function(s, e) {                         
	  var sHeight = Math.max(0, document.documentElement.clientHeight) - 130;
                  s.SetHeight(sHeight);
}" />
                                <Settings ShowFooter="False" VerticalScrollBarMode="Visible"
                                    VerticalScrollableHeight="136" />
                                <StylesEditors>
                                    <Style></Style>
                                </StylesEditors>
                                <Paddings PaddingTop="5px" />
                            </dxwgv:ASPxGridView>
                        </div>
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
        <script type="text/javascript" language="javascript">

            atualizaIconesDaTela();

        </script>
</form>
</body>
</html>
