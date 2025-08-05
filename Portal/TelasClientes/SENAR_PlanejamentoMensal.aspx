<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SENAR_PlanejamentoMensal.aspx.cs" Inherits="TelasClientes_SENAR_PlanejamentoMensal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function atualizaValor()
        {
            pnCallback.PerformCallback();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div>
    
        <table cellspacing="0" class="auto-style1">
            <tr>
                <td><iframe frameborder="0" id="frameWF" style="border: none;" scrolling="auto" src="frmPlanejamentoMensal.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela1 %>"></iframe></td>
            </tr>
            <tr>
                <td><iframe frameborder="0" id="frameWF" style="border: none;" scrolling="auto" src="frmOutrasDespesas.aspx<%=parametrosURL %>"
        width="100%" height="<%=alturaTela2 %>"></iframe></td>
            </tr>
            <tr>
                <td style="padding-left: 10px">
                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Valor Total do Planejamento:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px">
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback" Width="100%">
                                <SettingsLoadingPanel Enabled="False" />
                                <PanelCollection>
<dxcp:PanelContent runat="server">
    <dxtv:ASPxSpinEdit ID="txtValor" runat="server" ClientEnabled="False" ClientInstanceName="txtValor" DecimalPlaces="2" DisplayFormatString="{0:c2}"  Number="0" Width="180px">
        <SpinButtons ClientVisible="False">
        </SpinButtons>
        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
        </DisabledStyle>
    </dxtv:ASPxSpinEdit>
                                    </dxcp:PanelContent>
</PanelCollection>
                            </dxcp:ASPxCallbackPanel>


                        </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
