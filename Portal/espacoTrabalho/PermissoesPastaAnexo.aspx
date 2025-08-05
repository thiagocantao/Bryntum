<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissoesPastaAnexo.aspx.cs" Inherits="espacoTrabalho_PermissoesPastaAnexo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style3 {
            width: 95px;
        }

        .textoComIniciaisMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>

            <table cellspacing="0" class="auto-style1">
                <tr>
                    <td style="padding-bottom: 10px">
                        <table cellspacing="0">
                            <tr>
                                <td style="padding-right: 3px">
                                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Pasta:">
                                    </dxcp:ASPxLabel>
                                </td>
                                <td>
                                    <dxcp:ASPxLabel ID="lblNomePasta" runat="server">
                                    </dxcp:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Grupos:">
                        </dxcp:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px">
                        <dxcp:ASPxListBox ID="lbGrupos" runat="server" ClientInstanceName="lbGrupos" Rows="10" Width="100%">
                            <ItemImage Url="~/imagens/anexo/group.png">
                            </ItemImage>
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnCallback.PerformCallback();
}" />
                        </dxcp:ASPxListBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px">
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%" OnCallback="pnCallback_Callback">
                            <SettingsLoadingPanel Enabled="False" />
                            <PanelCollection>
                                <dxcp:PanelContent runat="server">
                                    <table cellspacing="0" class="auto-style1">
                                        <tr>
                                            <td style="border-bottom: 1px solid #C0C0C0">
                                                <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Permissões">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="border-bottom: 1px solid #C0C0C0">
                                                <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Permitir">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="border-bottom: 1px solid #C0C0C0">
                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Negar">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0;">
                                                <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Consultar">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px">
                                                <dxtv:ASPxCheckBox ID="ckPermitirC" runat="server" CheckState="Unchecked" ClientInstanceName="ckPermitirC">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckNegarC.SetChecked(false);
	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px; border-right-style: solid; border-right-width: 1px; border-right-color: #C0C0C0;">
                                                <dxtv:ASPxCheckBox ID="ckNegarC" runat="server" CheckState="Unchecked" ClientInstanceName="ckNegarC">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirC.SetChecked(false);
		ckPermitirI.SetChecked(false);
		ckPermitirM.SetChecked(false);
		ckPermitirE.SetChecked(false);
		ckNegarI.SetChecked(false);
		ckNegarM.SetChecked(false);
		ckNegarE.SetChecked(false);

	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0;">
                                                <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Text="Incluir">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px">
                                                <dxtv:ASPxCheckBox ID="ckPermitirI" runat="server" CheckState="Unchecked" ClientInstanceName="ckPermitirI">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirC.SetChecked(true);
		ckNegarC.SetChecked(false);
		ckNegarI.SetChecked(false);
	}
    callback.PerformCallback();
}" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px; border-right-style: solid; border-right-width: 1px; border-right-color: #C0C0C0;">
                                                <dxtv:ASPxCheckBox ID="ckNegarI" runat="server" CheckState="Unchecked" ClientInstanceName="ckNegarI">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirI.SetChecked(false);
	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0;">
                                                <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" Text="Editar">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px">
                                                <dxtv:ASPxCheckBox ID="ckPermitirM" runat="server" CheckState="Unchecked" ClientInstanceName="ckPermitirM">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirC.SetChecked(true);
		ckNegarC.SetChecked(false);
		ckNegarM.SetChecked(false);

	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                            <td align="center" class="auto-style3" style="padding: 5px; border-right-style: solid; border-right-width: 1px; border-right-color: #C0C0C0;">
                                                <dxtv:ASPxCheckBox ID="ckNegarM" runat="server" CheckState="Unchecked" ClientInstanceName="ckNegarM">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirM.SetChecked(false);
	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #C0C0C0; border-left-style: solid; border-left-width: 1px; border-left-color: #C0C0C0;">
                                                <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" Text="Modificar Conteúdo">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td align="center" class="auto-style3" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #C0C0C0">
                                                <dxtv:ASPxCheckBox ID="ckPermitirE" runat="server" CheckState="Unchecked" ClientInstanceName="ckPermitirE">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirC.SetChecked(true);
		ckNegarC.SetChecked(false);
		ckNegarE.SetChecked(false);

	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                            <td align="center" class="auto-style3" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #C0C0C0; border-right-style: solid; border-right-width: 1px; border-right-color: #C0C0C0;">
                                                <dxtv:ASPxCheckBox ID="ckNegarE" runat="server" CheckState="Unchecked" ClientInstanceName="ckNegarE">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(s.GetChecked())
	{
		ckPermitirE.SetChecked(false);
	}
    callback.PerformCallback();
}
" />
                                                </dxtv:ASPxCheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px">
                        <dxcp:ASPxCheckBox ID="ckSubPastas" runat="server" ClientInstanceName="ckSubPastas" Text="Aplicar permissões em todas as subpastas" ClientVisible="False">
                        </dxcp:ASPxCheckBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                            Text="Fechar" Width="90px" CssClass="textoComIniciaisMaiuscula">
                            <ClientSideEvents Click="function(s, e) {
	        window.top.fechaModal3();
}" />
                            <Paddings Padding="0px" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>

        </div>

        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback"
            OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
}" />
        </dxcb:ASPxCallback>

    </form>
</body>
</html>
