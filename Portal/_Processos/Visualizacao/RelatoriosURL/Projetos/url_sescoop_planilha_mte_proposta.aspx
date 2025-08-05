<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_sescoop_planilha_mte_proposta.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Projetos_url_sescoop_planilha_mte_proposta"
    Title="Planilha de Proposta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 100px;
        }
    </style>

</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
           
            <%-- commit para marcelo --%>
            
            <table cellpadding="0" cellspacing="0" style="padding-bottom:5px;padding-right:3px">
                <tr>
                    <asp:HiddenField runat="server" ID="hPlanilha" />
                    <td align="right" style="width:100%;padding-right:5px;">
                        <dxe:ASPxLabel ID="lblDescricao" runat="server"  Font-Bold="True" Font-Size="11pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="padding-right:5px">
                        <%--<dxe:ASPxComboBox ID="cmbPlanilha" runat="server" ClientInstanceName="cmbPlanilha" Width="400px"></dxe:ASPxComboBox>--%>
                         <dxe:ASPxComboBox ID="cmbPeriodo" runat="server" Visible="false" ClientInstanceName="cmbPeriodo" Width="400px">
                             <Items>
                                 <dxe:ListEditItem  Text="1º Trimestre" Value="1"/>
                                  <dxe:ListEditItem  Text="2º Trimestre" Value="2"/>
                                  <dxe:ListEditItem  Text="3º Trimestre" Value="3"/>
                                  <dxe:ListEditItem  Text="4º Trimestre" Value="4"/>
                             </Items>
                         </dxe:ASPxComboBox>
                         <dxe:ASPxComboBox ID="cmbEtapas" runat="server" Visible="false" ClientInstanceName="cmbEtapas" Width="400px"></dxe:ASPxComboBox>
                    </td>
                    <td align="right" style="width:100%;padding-right:5px;">
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Ano:" Font-Bold="True" Font-Size="11pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="padding-right:5px">
                        <dxe:ASPxComboBox ID="cmbAnos" runat="server" ClientInstanceName="cmbAnos" Width="120px"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnCarregarPlanilha" runat="server"
                            ImageSpacing="2px" Text="Carregar" AutoPostBack="False"
                            Width="100%" CausesValidation="False" EnableClientSideAPI="True" ClientInstanceName=" btnCarregarPlanilha">
                             <ClientSideEvents Click="function(s, e) 
                                {   
	                               //debugger  
                                    e.processOnServer = false; 
                                    
                                    if(hPlanilha.value == 'ORC')
                                    {
                                        if(cmbEtapas.GetSelectedItem() != null && cmbAnos.GetSelectedItem() != null)
                                        {
                                             cbSalvar.PerformCallback();
                                        
                                        }
                                        else
                                        {
                                            window.top.mostraMensagem('Selecione uma Etapa e um Ano para carrregar a planilha!', 'atencao', true, false, null);
                                            e.processOnServer = false;
                                        }
                                        
                                    }else if(hPlanilha.value == 'EXE')
                                    {
                                        if(cmbPeriodo.GetSelectedItem() != null && cmbAnos.GetSelectedItem() != null)
                                        {
                                             cbSalvar.PerformCallback();
                                        }
                                        else
                                        {
                                            window.top.mostraMensagem('Selecione um Período e um Ano para carregar a planilha!', 'atencao', true, false, null);
                                            e.processOnServer = false;
                                        }
                                        
                                    }

                                   
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <dxcp:ASPxCallbackPanel ID="cbSalvar" ClientInstanceName="cbSalvar" runat="server"  OnCallback="cbSalvar_Callback1" EnableClientSideAPI="True">
                <PanelCollection>
                    <dxcp:PanelContent runat="server">
                        <dx:ASPxSpreadsheet ID="AspSpreadsheet" runat="server" WorkDirectory="~/App_Data/WorkDirectory"
                            ClientInstanceName="Spreadsheet" ActiveTabIndex="0" Width="100%" Height="550px" ShowConfirmOnLosingChanges="False" Visible="false"
                            RibbonMode="OneLineRibbon" >
                            <Settings>
                                <Behavior SwitchViewModes="Hidden">
                                </Behavior>
                            </Settings>
                            <SettingsView Mode="Reading" />
                            <RibbonTabs>
                                <dx:SRReadingViewTab>
                                    <Groups>
                                        <dx:SRReadingViewGroup>
                                            <Items>
                                                <dx:SRViewToggleEditingViewCommand Visible="False">
                                                </dx:SRViewToggleEditingViewCommand>
                                                <dx:SRFilePrintCommand>
                                                </dx:SRFilePrintCommand>
                                                <dx:SRDownloadCommand>
                                                </dx:SRDownloadCommand>
                                                <dx:SREditingFindAndSelectCommand Visible="False">
                                                </dx:SREditingFindAndSelectCommand>
                                            </Items>
                                        </dx:SRReadingViewGroup>
                                    </Groups>
                                </dx:SRReadingViewTab>
                            </RibbonTabs>
                        </dx:ASPxSpreadsheet>
                    </dxcp:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="function(s, e)
                        {
                            var erro  = s.cp_ErroSalvar;
                            if (erro.length &gt; 0 )
                            {
                                window.top.mostraMensagem('Atenção : ' + erro, 'erro', true, false, null);
                            }
                        }" />
            </dxcp:ASPxCallbackPanel>
        </div>
    </form>
</body>
</html>
