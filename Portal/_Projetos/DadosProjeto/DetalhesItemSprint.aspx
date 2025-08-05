<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesItemSprint.aspx.cs" Inherits="_Projetos_DadosProjeto_DetalhesItemSprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script language="javascript" type="text/javascript">
    var atualizarURLAnexos = 'S';
</script>
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 90px;
        }
        .style3
        {
            width: 393px;
            height: 10px;
        }
        .style5
        {
            width: 100%;
        }
.dxtcLite > .dxtc-stripContainer .dxtc-link,
.dxtcLite > .dxtc-stripContainer .dxtc-leftIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-rightIndent
{
	text-decoration: none;
	white-space: nowrap;
}
.dxtcLite > .dxtc-stripContainer .dxtc-leftIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-rightIndent
{
	width: 5px;
}
.dxtcLite > .dxtc-stripContainer .dxtc-leftIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-spacer,
.dxtcLite > .dxtc-stripContainer .dxtc-rightIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-sbWrapper,
.dxtcLite > .dxtc-stripContainer .dxtc-sbIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-sbSpacer
{
	float: left;
	border-right-width: 0;
	border-left-width: 0;
	border-top: 1px solid transparent;
	border-bottom: 1px solid #A8A8A8;
	overflow: hidden;
}
.dxtcLite > .dxtc-stripContainer .dxtc-tab,
.dxtcLite > .dxtc-stripContainer .dxtc-activeTab,
.dxtcLite > .dxtc-stripContainer .dxtc-leftIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-spacer,
.dxtcLite > .dxtc-stripContainer .dxtc-rightIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-sbWrapper,
.dxtcLite > .dxtc-stripContainer .dxtc-sbIndent,
.dxtcLite > .dxtc-stripContainer .dxtc-sbSpacer
{
	display: block;
	margin: 0;
}
.dxtcLite > .dxtc-stripContainer .dxtc-spacer
{
	width: 1px;
}
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="style5">
        <tr>
            <td>
    <dxcp:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl" 
                    Width="100%"  ID="tabControl">
        <TabPages>
<dxcp:TabPage Name="tabP" Text="Principal"><ContentCollection>
<dxcp:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" 
                                        Text="Item:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxTextBox ID="txtTituloItem" runat="server" ClientEnabled="False" 
                                        ClientInstanceName="txtTituloItem"  
                                        MaxLength="500" Width="100%">
                                    </dxtv:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style7" width="100%">
                                        <tr>
                                            <td class="style2">
                                                <dxtv:ASPxLabel ID="ASPxLabel13" runat="server" 
                                                    Text="Importância:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td class="style2">
                                                <dxtv:ASPxLabel ID="ASPxLabel20" runat="server" 
                                                    Text="Estimativa:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxtv:ASPxLabel ID="ASPxLabel21" runat="server" 
                                                    Text="Classificação:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2" style="padding-right: 10px">
                                                <dxtv:ASPxTextBox ID="txtImportancia" runat="server" ClientEnabled="False" 
                                                    ClientInstanceName="txtImportancia"  
                                                    MaxLength="500" Width="100%">
                                                </dxtv:ASPxTextBox>
                                            </td>
                                            <td class="style2" style="padding-right: 10px">
                                                <dxtv:ASPxSpinEdit ID="txtEsforcoPrevisto" runat="server" 
                                                    AllowMouseWheel="False" ClientEnabled="False" 
                                                    ClientInstanceName="txtEsforcoPrevisto" DecimalPlaces="2" 
                                                    DisplayFormatString="{0:n2}"  Number="0" 
                                                    Width="100%">
                                                    <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                    </SpinButtons>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxtv:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxtv:ASPxTextBox ID="txtDescricaoTipoClassificacaoItem" runat="server" 
                                                    ClientEnabled="False" ClientInstanceName="txtDescricaoTipoClassificacaoItem" 
                                                     MaxLength="500" Width="100%">
                                                </dxtv:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxLabel ID="ASPxLabel22" runat="server" 
                                        Text="Descrição:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtv:ASPxMemo ID="mmDescricaoItem" runat="server" ClientEnabled="False" 
                                        ClientInstanceName="mmDescricaoItem"  
                                        Rows="14" Width="100%">
                                        <ValidationSettings CausesValidation="True" Display="Dynamic" 
                                            ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido" 
                                            ValidationGroup="MKE">
                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxMemo>
                                </td>
                            </tr>
                        </table>
                    </dxcp:ContentControl>
</ContentCollection>
</dxcp:TabPage>
<dxcp:TabPage Name="tabA" Text="Anexos"><ContentCollection>
<dxcp:ContentControl runat="server">
                        <iframe ID="frmAnexos" frameborder="0" height="288" scrolling="no" src="" 
                            width="800px"></iframe>
                    </dxcp:ContentControl>
</ContentCollection>
</dxcp:TabPage>
</TabPages>

<ClientSideEvents ActiveTabChanged="function(s, e) {
	if(e.tab.index == 1)
	{
		if(atualizarURLAnexos != &#39;N&#39;)
	    {
	        		atualizarURLAnexos = &#39;N&#39;;		    
			        document.getElementById(&#39;frmAnexos&#39;).src = s.cp_Anexo;
		}
	}
}" ></ClientSideEvents>

<ContentStyle>
<Paddings Padding="3px"></Paddings>
</ContentStyle>
</dxcp:ASPxPageControl>


            </td>
        </tr>
        <tr>
            <td align="right" >
                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnFecharItem" Text="Fechar" Width="90px" 
                                             ID="btnFecharItem">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxcp:ASPxButton>

                                    </td>
        </tr>
    </table>
    </form>
</body>
</html>
