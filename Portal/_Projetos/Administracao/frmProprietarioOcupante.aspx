<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProprietarioOcupante.aspx.cs"
    Inherits="_Projetos_Administracao_frmProprietarioOcupante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .dxgvControl, .dxgvDisabled
        {
            border: 1px Solid #9F9F9F;
            font: 12px Tahoma;
            background-color: #F2F2F2;
            color: Black;
            cursor: default;
        }
        .dxgvControl
        {
            border: Solid 1px #9F9F9F;
            font: 11px Tahoma;
            background-color: #F2F2F2;
            color: Black;
            cursor: default;
        }
        
        .dxgvTable
        {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .dxgvTable
        {
            background-color: White;
            border-width: 0;
            border-collapse: separate !important;
            overflow: hidden;
            color: Black;
        }
        
        .dxgvTable
        {
            background-color: White;
            border: 0;
            border-collapse: separate !important;
            overflow: hidden;
            font: 9pt Tahoma;
            color: Black;
        }
        
        .dxgvHeader
        {
            cursor: pointer;
            white-space: nowrap;
            padding: 4px 6px 5px;
            border: 1px Solid #9F9F9F;
            background-color: #DCDCDC;
            overflow: hidden;
            font-weight: normal;
            text-align: left;
        }
        
        .dxgvHeader
        {
            cursor: pointer;
            white-space: nowrap;
            padding: 4px 6px 5px 6px;
            border: Solid 1px #9F9F9F;
            background-color: #DCDCDC;
            overflow: hidden;
            font-weight: normal;
            text-align: left;
        }
        
        .dxgvFocusedRow
        {
            background-color: #8D8D8D;
            color: White;
        }
        .dxgvFocusedRow
        {
            background-color: #8D8D8D;
            color: White;
        }
        .dxgvCommandColumn
        {
            padding: 2px;
        }
        .dxgvCommandColumn
        {
            padding: 2px 2px 2px 2px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ProcessaResultadoCallback1(s, e) {
            var result = e.result;
            if (result && result.length && result.length > 0) {
                if (result.substring(0, 1) == "I") {
                    //var activeTabIndex = pageControl.GetActiveTabIndex();

                    //e.Result = tipoOperacao + codigoProjeto.ToString() + "p" + codigoPessoaImovel;
                    var str2 = result.substring(result.indexOf('p') + 1);

                    var str1 = "&CPI=" + str2;

                    window.location = "./frmProprietarioOcupante.aspx?CP=" + result.substring(1, result.indexOf('p')) + str1;

                }
                window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div style="height: <%=alturaDivRolagem %>; overflow: auto; width: 100%;" id="divRolagem">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1" border="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="False"
                                                    Text="Nome:*">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                    Text="Nascimento:*">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                    Text="Nacionalidade:*">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 3px">
                                                <dxe:ASPxTextBox ID="txtNome" runat="server" Width="100%" ClientInstanceName="txtNome"
                                                     MaxLength="150">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 100px; padding-right: 3px;" align="left">
                                                <dxe:ASPxDateEdit ID="dtNascimento" runat="server" Width="100px" ClientInstanceName="dtNascimento"
                                                     DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                    EditFormatString="dd/MM/yyyy">
                                                    <Paddings Padding="0px" />
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td align="left" style="width: 100px;">
                                                <dxe:ASPxComboBox ID="ddlIndicaNacionalidade" runat="server" ClientInstanceName="ddlIndicaNacionalidade"
                                                     Width="100px" SelectedIndex="0">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	var nac = ddlIndicaNacionalidade.GetValue();
	if(nac == 'B')
	{
		txtPais.SetEnabled(false);
		ddlUFPessoa.SetEnabled(true);
		ddlMunicipioPessoa.SetEnabled(true);
	}
	if(nac == 'E')
	{
		txtPais.SetEnabled(true);
		ddlUFPessoa.SetEnabled(false);
		ddlMunicipioPessoa.SetEnabled(false);
	}
}" />
                                                    <Items>
                                                        <dxe:ListEditItem Selected="True" Text="Brasileira" Value="B" />
                                                        <dxe:ListEditItem Text="Estrangeira" Value="E" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1" border="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                    Text="País:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                    Text="UF:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                    Text="Município:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 300px; padding-right: 3px">
                                                <dxe:ASPxTextBox ID="txtPais" runat="server" Width="100%" ClientInstanceName="txtPais"
                                                     ClientEnabled="False" MaxLength="150">
                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 95px; padding-right: 3px;">
                                                <dxe:ASPxComboBox ID="ddlUFPessoa" runat="server" ClientInstanceName="ddlUFPessoa"
                                                    Width="100%"  DataSourceID="sdsUFPessoa"
                                                    TextField="SiglaUF" ValueField="SiglaUF">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMunicipioPessoa.PerformCallback();
}" />
                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox ID="ddlMunicipioPessoa" runat="server" ClientInstanceName="ddlMunicipioPessoa"
                                                    Width="100%"  TextField="NomeMunicipio" ValueField="CodigoMunicipio"
                                                    OnCallback="ddlMunicipioPessoa_Callback">
                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                    Text="Profissão:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                    Text="CPF:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Tipo de documento:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="Número:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 3px; width: 400px;">
                                                <dxe:ASPxTextBox ID="txtProfissao" runat="server" Width="400px" ClientInstanceName="txtProfissao"
                                                     MaxLength="250">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 120px; padding-right: 3px;">
                                                <dxe:ASPxTextBox ID="txtCPF" runat="server" Width="120px" ClientInstanceName="txtCPF"
                                                     MaxLength="15">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td align="left" style="width: 110px; padding-right: 3px;">
                                                <dxe:ASPxCheckBox ID="ckbSabeAssinar" runat="server" 
                                                    Text="Sabe Assinar?" ClientInstanceName="ckbSabeAssinar" Width="100%">
                                                    <Border BorderWidth="1px" />
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td align="left" style="width: 130px; padding-right: 3px;">
                                                <dxe:ASPxComboBox ID="ddlTipoDocumento" runat="server" ClientInstanceName="ddlTipoDocumento"
                                                     Width="130px">
                                                    <Items>
                                                        <dxe:ListEditItem Text="Carteira de Identidade" Value="Carteira de Identidade" />
                                                        <dxe:ListEditItem Text="CTPS" Value="CTPS" />
                                                        <dxe:ListEditItem Text="Carteira Profissional" Value="Carteira Profissional" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td align="left">
                                                <dxe:ASPxTextBox ID="txtNumeroDocumento" runat="server" Width="100%" ClientInstanceName="txtNumeroDocumento"
                                                     MaxLength="50">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="Órgão Expeditor:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="Nome do Pai:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="Nome da Mãe:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 135px; padding-right: 3px;">
                                                <dxe:ASPxTextBox ID="txtOrgaoExpeditor" runat="server" Width="135px" ClientInstanceName="txtOrgaoExpeditor"
                                                     MaxLength="50">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 350px; padding-right: 3px;">
                                                <dxe:ASPxTextBox ID="txtNomePai" runat="server" Width="350px" ClientInstanceName="txtNomePai"
                                                     MaxLength="150">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtNomeMae" runat="server" Width="100%" ClientInstanceName="txtNomeMae"
                                                     MaxLength="150">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1" id="tbEstadoCivil" bgcolor="#F3F3F3">
                                        <tr>
                                            <td style="width: 135px; padding-right: 3px;">
                                                <dxe:ASPxRadioButtonList runat="server" ItemSpacing="10px" ClientInstanceName="rblIndicaEstadoCivilPessoa"
                                                    Width="135px" Height="100%"  ID="rblIndicaEstadoCivilPessoa">
                                                    <Paddings Padding="0px"></Paddings>
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
{
	txtSoltCasCertidao.SetEnabled(false);
	txtSoltCasFolhas.SetEnabled(false);
	txtSoltCasLivro.SetEnabled(false);
	dtSoltCasEmissao.SetEnabled(false);
	txtSoltCasCartorio.SetEnabled(false);	

	txtSepDivSentencaAutosN.SetEnabled(false);
	dtSepDivData.SetEnabled(false);
	txtSepDivJuizo.SetEnabled(false);

	dtUniaoEstavelDesde.SetEnabled(false);

	txtViuvoNumeroCertObito.SetEnabled(false);
	txtViuvoFolhas.SetEnabled(false);
	txtViuvoLivro.SetEnabled(false);
	dtViuvoEmissao.SetEnabled(false);
	txtViuvoCartorio.SetEnabled(false);
    //habilitaPainelConjuge(false);

	ddlIndicaEscRegPactoAnteNupcialPessoa.SetEnabled(false);
    txtNumeroIndEscRegPacAntNupPes.SetEnabled(false);
    dtEmissaoIndEscRegPacAntNupPes.SetEnabled(false);
    txtLivroIndEscRegPacAntNupPes.SetEnabled(false);
    txtFolhasIndEscRegPacAntNupPes.SetEnabled(false);
    txtCartorioIndEscRegPacAntNupPes.SetEnabled(false);

    var st = rblIndicaEstadoCivilPessoa.GetValue();
	
	if(st == &#39;S&#39; || st == &#39;C&#39;)
	{
		txtSoltCasCertidao.SetEnabled(true);
		txtSoltCasFolhas.SetEnabled(true);
		txtSoltCasLivro.SetEnabled(true);
		dtSoltCasEmissao.SetEnabled(true);
		txtSoltCasCartorio.SetEnabled(true);
       	if(st == &#39;S&#39;)
       	{
			habilitaPainelConjuge(false);
           	rblRegimeSeparacaoBens.SetEnabled(false);
            txtNumeroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
            dtEmissaoIndEscRegPacAntNupPes.SetValue(null);
            txtLivroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
            txtFolhasIndEscRegPacAntNupPes.SetText(&#39;&#39;);
            txtCartorioIndEscRegPacAntNupPes.SetText(&#39;&#39;);
       	}
		else
		{
			habilitaPainelConjuge(true);
           	rblRegimeSeparacaoBens.SetEnabled(true);
			ddlIndicaEscRegPactoAnteNupcialPessoa.SetEnabled(true);
       		txtNumeroIndEscRegPacAntNupPes.SetEnabled(true);
       		dtEmissaoIndEscRegPacAntNupPes.SetEnabled(true);
       		txtLivroIndEscRegPacAntNupPes.SetEnabled(true);
       		txtFolhasIndEscRegPacAntNupPes.SetEnabled(true);
       		txtCartorioIndEscRegPacAntNupPes.SetEnabled(true);
		}
        //limpar textos
        txtSepDivSentencaAutosN.SetText(&#39;&#39;);
        dtSepDivData.SetValue(null);
        txtSepDivJuizo.SetText(&#39;&#39;);
        dtUniaoEstavelDesde.SetValue(null);
		txtViuvoNumeroCertObito.SetText(&#39;&#39;);
		dtViuvoEmissao.SetValue(null);
		txtViuvoLivro.SetText(&#39;&#39;);
		txtViuvoFolhas.SetText(&#39;&#39;);
		txtViuvoCartorio.SetText(&#39;&#39;);
   	}
	if(st == &#39;SJ&#39; || st == &#39;D&#39;)
	{
		txtSepDivSentencaAutosN.SetEnabled(true);
		dtSepDivData.SetEnabled(true);
		txtSepDivJuizo.SetEnabled(true);
       	habilitaPainelConjuge(false);
       	rblRegimeSeparacaoBens.SetEnabled(false);
        
        txtNumeroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        dtEmissaoIndEscRegPacAntNupPes.SetValue(null);
        txtLivroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        txtFolhasIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        txtCartorioIndEscRegPacAntNupPes.SetText(&#39;&#39;);

		txtSoltCasCertidao.SetText(&#39;&#39;);
		dtSoltCasEmissao.SetValue(null);
		txtSoltCasLivro.SetText(&#39;&#39;);
		txtSoltCasFolhas.SetText(&#39;&#39;);
		txtSoltCasCartorio.SetText(&#39;&#39;);
		dtUniaoEstavelDesde.SetValue(null);
		txtViuvoNumeroCertObito.SetText(&#39;&#39;);
		dtViuvoEmissao.SetValue(null);
		txtViuvoLivro.SetText(&#39;&#39;);
		txtViuvoFolhas.SetText(&#39;&#39;);
		txtViuvoCartorio.SetText(&#39;&#39;);
	}
	if(st == &#39;UE&#39;)
	{
		dtUniaoEstavelDesde.SetEnabled(true);
       	rblRegimeSeparacaoBens.SetEnabled(true);
       	habilitaPainelConjuge(true);
		ddlIndicaEscRegPactoAnteNupcialPessoa.SetEnabled(true);
       	txtNumeroIndEscRegPacAntNupPes.SetEnabled(true);
       	dtEmissaoIndEscRegPacAntNupPes.SetEnabled(true);
       	txtLivroIndEscRegPacAntNupPes.SetEnabled(true);
       	txtFolhasIndEscRegPacAntNupPes.SetEnabled(true);
       	txtCartorioIndEscRegPacAntNupPes.SetEnabled(true);

		txtSoltCasCertidao.SetText(&#39;&#39;);
		dtSoltCasEmissao.SetValue(null);
		txtSoltCasLivro.SetText(&#39;&#39;);
		txtSoltCasFolhas.SetText(&#39;&#39;);
		txtSoltCasCartorio.SetText(&#39;&#39;);
	    txtSepDivSentencaAutosN.SetText(&#39;&#39;);
		dtSepDivData.SetValue(null);
		txtSepDivJuizo.SetText(&#39;&#39;);
		txtViuvoNumeroCertObito.SetText(&#39;&#39;);
		dtViuvoEmissao.SetValue(null);
		txtViuvoLivro.SetText(&#39;&#39;);
		txtViuvoFolhas.SetText(&#39;&#39;);
		txtViuvoCartorio.SetText(&#39;&#39;);
	}
	if(st == &#39;V&#39;)
	{
		txtViuvoNumeroCertObito.SetEnabled(true);
		txtViuvoFolhas.SetEnabled(true);
		txtViuvoLivro.SetEnabled(true);
		dtViuvoEmissao.SetEnabled(true);
		txtViuvoCartorio.SetEnabled(true);
       	habilitaPainelConjuge(false);
       	rblRegimeSeparacaoBens.SetEnabled(false);
        
        txtNumeroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        dtEmissaoIndEscRegPacAntNupPes.SetValue(null);
        txtLivroIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        txtFolhasIndEscRegPacAntNupPes.SetText(&#39;&#39;);
        txtCartorioIndEscRegPacAntNupPes.SetText(&#39;&#39;);

		txtSoltCasCertidao.SetText(&#39;&#39;);
		dtSoltCasEmissao.SetValue(null);
		txtSoltCasLivro.SetText(&#39;&#39;);
		txtSoltCasFolhas.SetText(&#39;&#39;);
		txtSoltCasCartorio.SetText(&#39;&#39;);
		txtSepDivSentencaAutosN.SetText(&#39;&#39;);
		dtSepDivData.SetValue(null);
		txtSepDivJuizo.SetText(&#39;&#39;);
		dtUniaoEstavelDesde.SetValue(null);
	}
}"></ClientSideEvents>
                                                    <Items>
                                                        <dxe:ListEditItem Text="Solteiro" Value="S"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Casado" Value="C"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Separado Judicialmente" Value="SJ"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Divorciado" Value="D"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Uni&#227;o Est&#225;vel" Value="UE"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Vi&#250;vo" Value="V"></dxe:ListEditItem>
                                                    </Items>
                                                </dxe:ASPxRadioButtonList>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;<dxe:ASPxLabel runat="server" Text="Certid&#227;o N&#186;:"
                                                                            ID="ASPxLabel15">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Emiss&#227;o:" 
                                                                            ID="ASPxLabel18">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Livro:" 
                                                                            ID="ASPxLabel17">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Folhas:" 
                                                                            ID="ASPxLabel16">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Cart&#243;rio:" 
                                                                            ID="ASPxLabel19">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px; padding-right: 3px">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtSoltCasCertidao"
                                                                            ClientEnabled="False"  ID="txtSoltCasCertidao">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 95px; padding-right: 3px;">
                                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            Width="100%" ClientInstanceName="dtSoltCasEmissao" ClientEnabled="False"
                                                                            ID="dtSoltCasEmissao">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtSoltCasLivro"
                                                                            ClientEnabled="False"  ID="txtSoltCasLivro">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtSoltCasFolhas"
                                                                            ClientEnabled="False"  ID="txtSoltCasFolhas">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtSoltCasCartorio"
                                                                            ClientEnabled="False"  ID="txtSoltCasCartorio">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Senten&#231;a - Autos N&#186;:"
                                                                            ID="ASPxLabel20">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Data:"  ID="ASPxLabel22">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="padding-left: 3px">
                                                                        <dxe:ASPxLabel runat="server" Text="Ju&#237;zo:" 
                                                                            ID="ASPxLabel21">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtSepDivSentencaAutosN"
                                                                            ClientEnabled="False"  ID="txtSepDivSentencaAutosN">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 95px; padding-right: 3px;">
                                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            Width="100%" ClientInstanceName="dtSepDivData" ClientEnabled="False"
                                                                            ID="dtSepDivData">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtSepDivJuizo"
                                                                            ClientEnabled="False"  ID="txtSepDivJuizo">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Desde:" 
                                                                                        ID="ASPxLabel23">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                        Width="95px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="dtUniaoEstavelDesde"
                                                                                        ClientEnabled="False"  ID="dtUniaoEstavelDesde">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Certid&#227;o N&#186;:"
                                                                            ID="ASPxLabel61">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Emiss&#227;o:" 
                                                                            ID="ASPxLabel64">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Livro:" 
                                                                            ID="ASPxLabel63">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Folhas:" 
                                                                            ID="ASPxLabel62">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Cart&#243;rio:" 
                                                                            ID="ASPxLabel65">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtViuvoNumeroCertObito"
                                                                            ClientEnabled="False"  ID="txtViuvoNumeroCertObito">
                                                                            <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 95px; padding-right: 3px;">
                                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            Width="100%" ClientInstanceName="dtViuvoEmissao" ClientEnabled="False"
                                                                            ID="dtViuvoEmissao">
                                                                            <Paddings Padding="0px"></Paddings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtViuvoLivro"
                                                                            ClientEnabled="False"  ID="txtViuvoLivro">
                                                                            <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 150px; padding-right: 3px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtViuvoFolhas"
                                                                            ClientEnabled="False"  ID="txtViuvoFolhas">
                                                                            <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtViuvoCartorio"
                                                                            ClientEnabled="False"  ID="txtViuvoCartorio">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Regime de Separação de bens:" ID="ASPxLabel67"
                                        >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <dxe:ASPxRadioButtonList ID="rblRegimeSeparacaoBens" runat="server" ClientInstanceName="rblRegimeSeparacaoBens"
                                         ItemSpacing="50px" RepeatDirection="Horizontal"
                                        SelectedIndex="1" Width="100%" ClientEnabled="False">
                                        <Paddings Padding="0px" />
                                        <Items>
                                            <dxe:ListEditItem Text="Comunhão Parcial" Value="CP" />
                                            <dxe:ListEditItem Selected="True" Text="Comunhão Universal" Value="CU" />
                                            <dxe:ListEditItem Text="Separação de Bens" Value="SEB" />
                                            <dxe:ListEditItem Text="Participação Final nos Aquestros" Value="PFA" />
                                        </Items>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxRadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1" border="0">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel29" runat="server" 
                                                                Text="Pacto Ant.:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel31" runat="server" 
                                                                Text="Número:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel33" runat="server" 
                                                                Text="Emissão:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel34" runat="server" 
                                                                Text="Livro:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel32" runat="server" 
                                                                Text="Folhas:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel30" runat="server" 
                                                                Text="Cartório:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px; padding-right: 3px">
                                                            <dxe:ASPxComboBox ID="ddlIndicaEscRegPactoAnteNupcialPessoa" runat="server" ClientInstanceName="ddlIndicaEscRegPactoAnteNupcialPessoa"
                                                                Width="100%"  SelectedIndex="0" ClientEnabled="False">
                                                                <Items>
                                                                    <dxe:ListEditItem Selected="True" Text="Escritura" Value="E" />
                                                                    <dxe:ListEditItem Text="Registro" Value="R" />
                                                                </Items>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 150px; padding-right: 3px">
                                                            <dxe:ASPxTextBox ID="txtNumeroIndEscRegPacAntNupPes" runat="server" Width="100%"
                                                                 ClientInstanceName="txtNumeroIndEscRegPacAntNupPes"
                                                                MaxLength="50" ClientEnabled="False">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 95px; padding-right: 3px">
                                                            <dxe:ASPxDateEdit runat="server" Width="100%" 
                                                                ID="dtEmissaoIndEscRegPacAntNupPes" ClientInstanceName="dtEmissaoIndEscRegPacAntNupPes"
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" ClientEnabled="False">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td style="width: 150px; padding-right: 3px">
                                                            <dxe:ASPxTextBox ID="txtLivroIndEscRegPacAntNupPes" runat="server" Width="100%"
                                                                ClientInstanceName="txtLivroIndEscRegPacAntNupPes" MaxLength="50"
                                                                ClientEnabled="False">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 150px; padding-right: 3px">
                                                            <dxe:ASPxTextBox ID="txtFolhasIndEscRegPacAntNupPes" runat="server" Width="100%"
                                                                 ClientInstanceName="txtFolhasIndEscRegPacAntNupPes"
                                                                MaxLength="50" ClientEnabled="False">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtCartorioIndEscRegPacAntNupPes" runat="server" Width="100%"
                                                                 ClientInstanceName="txtCartorioIndEscRegPacAntNupPes"
                                                                MaxLength="250" ClientEnabled="False">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1" bgcolor="#E3E3E3" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" class="style1">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                    <tr>
                                                                                                        <td bgcolor="White" >
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td bgcolor="White">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td bgcolor="White">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td bgcolor="White">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Informa&#231;&#245;es do c&#244;njuge" Font-Bold="True"
                                                                                                                 ID="ASPxLabel70">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Nome do c&#244;njuge:" 
                                                                                                                ID="ASPxLabel35">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Nascimento:" 
                                                                                                                ID="ASPxLabel36">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <dxe:ASPxLabel runat="server" Text="Nacionalidade:" 
                                                                                                                ID="ASPxLabel38">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <dxe:ASPxLabel runat="server" Text="Pa&#237;s do c&#244;njuge:"
                                                                                                                ID="ASPxLabel37">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 470px; padding-right: 3px">
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtNomeConjuge"
                                                                                                                 ID="txtNomeConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                        <td style="padding-right: 3px; width: 95px;">
                                                                                                            <dxe:ASPxDateEdit runat="server" Width="100%" ClientInstanceName="dtNascimentoConjuge"
                                                                                                                 ID="dtNascimentoConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxDateEdit>
                                                                                                        </td>
                                                                                                        <td align="left" style="width: 95px; padding-right: 3px;">
                                                                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" Width="100%" ClientInstanceName="ddlNacionalidadeConjuge"
                                                                                                                 ID="ddlNacionalidadeConjuge" ClientEnabled="False">
                                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	var nac = ddlNacionalidadeConjuge.GetValue();
	if(nac == &#39;B&#39;)
	{
		txtPaisConjuge.SetEnabled(false);
		ddlUFConjuge.SetEnabled(true);
		ddlMunicipioConjuge.SetEnabled(true);
	}
	if(nac == &#39;E&#39;)
	{
		txtPaisConjuge.SetEnabled(true);
		ddlUFConjuge.SetEnabled(false);
		ddlMunicipioConjuge.SetEnabled(false);
	}
}"></ClientSideEvents>
                                                                                                                <Items>
                                                                                                                    <dxe:ListEditItem Selected="True" Text="Brasileira" Value="B"></dxe:ListEditItem>
                                                                                                                    <dxe:ListEditItem Text="Estrangeira" Value="E"></dxe:ListEditItem>
                                                                                                                </Items>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtPaisConjuge"
                                                                                                                ClientEnabled="False"  ID="txtPaisConjuge">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="UF:"  ID="ASPxLabel40">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Munic&#237;pio:" 
                                                                                                                ID="ASPxLabel39">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Profiss&#227;o:" 
                                                                                                                ID="ASPxLabel41">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100px; padding-right: 3px">
                                                                                                            <dxe:ASPxComboBox runat="server" DataSourceID="sdsUFConjuge" TextField="SiglaUF"
                                                                                                                ValueField="SiglaUF" Width="100%" ClientInstanceName="ddlUFConjuge"
                                                                                                                ID="ddlUFConjuge" ClientEnabled="False">
                                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMunicipioConjuge.PerformCallback();
}"></ClientSideEvents>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td style="width: 365px; padding-right: 3px">
                                                                                                            <dxe:ASPxComboBox runat="server" TextField="NomeMunicipio" ValueField="CodigoMunicipio"
                                                                                                                Width="100%" ClientInstanceName="ddlMunicipioConjuge" 
                                                                                                                ID="ddlMunicipioConjuge" OnCallback="ddlMunicipioConjuge_Callback" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtProfissaoConjuge"
                                                                                                                 ID="txtProfissaoConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="CPF:"  ID="ASPxLabel43">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Tipo de Documento:" 
                                                                                                                ID="ASPxLabel44">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="N&#250;mero:" 
                                                                                                                ID="ASPxLabel46">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="&#211;rg&#227;o Expeditor:"
                                                                                                                ID="ASPxLabel45">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 100px; padding-right: 3px;">
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="15" ClientInstanceName="txtCPFConjuge"
                                                                                                                 ID="txtCPFConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 105px; padding-right: 3px">
                                                                                                            <dxe:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Sabe assinar?" ClientInstanceName="ckbSabeAssinarConjuge"
                                                                                                                Width="100%"  ID="ckbSabeAssinarConjuge">
                                                                                                                <Border BorderColor="Black" BorderWidth="1px"></Border>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxCheckBox>
                                                                                                        </td>
                                                                                                        <td style="width: 255px; padding-right: 3px">
                                                                                                            <dxe:ASPxComboBox runat="server" SelectedIndex="0" Width="100%" ClientInstanceName="ddlTipoDocumentoConjuge"
                                                                                                                 ID="ddlTipoDocumentoConjuge" ClientEnabled="False">
                                                                                                                <Items>
                                                                                                                    <dxe:ListEditItem Selected="True" Text="Carteira de identidade" Value="Carteira de identidade">
                                                                                                                    </dxe:ListEditItem>
                                                                                                                    <dxe:ListEditItem Text="CTPS" Value="CTPS"></dxe:ListEditItem>
                                                                                                                    <dxe:ListEditItem Text="Carteira Profissional" Value="Carteira Profissional"></dxe:ListEditItem>
                                                                                                                </Items>
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td style="width: 325px; padding-right: 3px">
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtNumeroDocumentoConjuge"
                                                                                                                 ID="txtNumeroDocumentoConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtOrgaoExpeditorConjuge"
                                                                                                                 ID="txtOrgaoExpeditorConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Nome do Pai:" 
                                                                                                                ID="ASPxLabel47">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxLabel runat="server" Text="Nome da M&#227;e:" 
                                                                                                                ID="ASPxLabel48">
                                                                                                            </dxe:ASPxLabel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 465px; padding-right: 3px;">
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtNomePaiConjuge"
                                                                                                                 ID="txtNomePaiConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" ClientInstanceName="txtNomeMaeConjuge"
                                                                                                                 ID="txtNomeMaeConjuge" ClientEnabled="False">
                                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                </DisabledStyle>
                                                                                                            </dxe:ASPxTextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="padding-bottom: 3px">
                                                                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Estado Civil" 
                                                                                                                            ID="ASPxLabel49">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Certid&#227;o:" 
                                                                                                                            ID="ASPxLabel51">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Emiss&#227;o:" 
                                                                                                                            ID="ASPxLabel52">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Livro:" 
                                                                                                                            ID="ASPxLabel54">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Folhas:" 
                                                                                                                            ID="ASPxLabel53">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxLabel runat="server" Text="Cart&#243;rio:" 
                                                                                                                            ID="ASPxLabel50">
                                                                                                                        </dxe:ASPxLabel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width: 135px; padding-right: 3px">
                                                                                                                        <dxe:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlEstadoCivilConjuge"
                                                                                                                             ID="ddlEstadoCivilConjuge" ClientEnabled="False">
                                                                                                                            <Items>
                                                                                                                                <dxe:ListEditItem Text="Solteiro" Value="So"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Casado" Value="Ca"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Separado Judicialmente" Value="Se"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Divorciado" Value="Di"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Uni&#227;o Est&#225;vel" Value="Un"></dxe:ListEditItem>
                                                                                                                                <dxe:ListEditItem Text="Vi&#250;vo" Value="Vi"></dxe:ListEditItem>
                                                                                                                            </Items>
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxComboBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 150px; padding-right: 3px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="160px" MaxLength="50" ClientInstanceName="txtCertidaoConjuge"
                                                                                                                             ID="txtCertidaoConjuge" ClientEnabled="False">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 95px; padding-right: 3px">
                                                                                                                        <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                                            Width="100%" ClientInstanceName="dtEmissaoConjuge" 
                                                                                                                            ID="dtEmissaoConjuge" ClientEnabled="False">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxDateEdit>
                                                                                                                    </td>
                                                                                                                    <td style="width: 150px; padding-right: 3px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtLivroConjuge"
                                                                                                                             ID="txtLivroConjuge" ClientEnabled="False">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td style="width: 150px; padding-right: 3px">
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtFolhasConjuge"
                                                                                                                             ID="txtFolhasConjuge" ClientEnabled="False">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtCartorioConjuge"
                                                                                                                             ID="txtCartorioConjuge" ClientEnabled="False">
                                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dxe:ASPxTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel55" runat="server" 
                                                    Text="Endereço Residencial:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel56" runat="server" 
                                                    Text="Número:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel68" runat="server" Text="Telefone:"
                                                   >
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 3px; width: 475px;">
                                                <dxe:ASPxTextBox ID="txtEnderecoResidencial" runat="server"
                                                    Width="100%" ClientInstanceName="txtEnderecoResidencial" MaxLength="250">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="padding-right: 3px; width: 190px;">
                                                <dxe:ASPxTextBox ID="txtNumero" runat="server" 
                                                    Width="100%" ClientInstanceName="txtNumero" MaxLength="50">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtFone" runat="server" 
                                                    Width="100%" ClientInstanceName="txtFone" MaxLength="50">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel71" runat="server" 
                                                    Text="UF:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel72" runat="server" 
                                                    Text="Município:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 95px; padding-right: 3px">
                                                <dxe:ASPxComboBox ID="ddlUFResidenciaPessoa" runat="server" ClientInstanceName="ddlUFResidenciaPessoa"
                                                    Width="100%"  DataSourceID="sdsUFResidenciaPessoa"
                                                    TextField="SiglaUF" ValueField="SiglaUF">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMunicipioResidenciaPessoa.PerformCallback();
}" />
                                                    <Paddings Padding="0px" />
                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td style="width: 400px; padding-right: 3px">
                                                <dxe:ASPxComboBox ID="ddlMunicipioResidenciaPessoa" runat="server" ClientInstanceName="ddlMunicipioResidenciaPessoa"
                                                    Width="100%"  TextField="NomeMunicipio" ValueField="CodigoMunicipio"
                                                    OnCallback="ddlMunicipioResidenciaPessoa_Callback">
                                                    <ClientSideEvents Init="function(s, e) {
	s.SetEnabled(true);
}" />
                                                    <Paddings Padding="0px" />
                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1" border="0">
                                                    <tr>
                                                        <td style="width: 95px; padding-right: 3px">
                                                            <dxe:ASPxCheckBox ID="ckbIndicaProprietario" runat="server" Text="Proprietário?"
                                                                 ClientInstanceName="ckbIndicaProprietario"
                                                                Width="100%">
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                        <td style="width: 80px; padding-right: 3px">
                                                            <dxe:ASPxCheckBox ID="ckbIndicaOcupante" runat="server" Text="Ocupante?"
                                                                ClientInstanceName="ckbIndicaOcupante" Width="100%">
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                        <td style="width: 165px; padding-right: 3px">
                                                            <dxe:ASPxLabel ID="ASPxLabel69" runat="server" 
                                                                Text="Tempo de Ocupação (anos):" Width="100%">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="speTempoOcupacaoAnos" runat="server" ClientInstanceName="speTempoOcupacaoAnos"
                                                                 NullText="0" Number="0" Width="100%" MaxValue="90"
                                                                DecimalPlaces="2" DisplayFormatString="n2">
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <Paddings Padding="0px" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel66" runat="server" Text="Documentos do ocupante:"
                                       >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDocsOcupanteImovel" KeyFieldName="CodigoDocumentoOcupanteImovel"
                                        AutoGenerateColumns="False" DataSourceID="sdsDocumentoOcupanteImovel" Width="100%"
                                         ID="gvDocsOcupanteImovel">
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem DisplayFormat="Total:{0:n4}" FieldName="Area" SummaryType="Sum"
                                                ValueDisplayFormat="Total:{0:n4}" ShowInColumn="Área (ha)" ShowInGroupFooterColumn="Área (ha)" />
                                        </TotalSummary>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="50px" VisibleIndex="0" ShowEditButton="true"
                                                ShowDeleteButton="true">
                                                <HeaderTemplate>
                                                    <% =ObtemBotaoInclusaoRegistro("gvDocsOcupanteImovel", "Documentos do Ocupante/Proprietário")%>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoDocumentoOcupanteImovel" Caption="CodigoDocumentoOcupanteImovel"
                                                VisibleIndex="7" Visible="False">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoPessoaImovel" Caption="CodigoPessoaImovel"
                                                VisibleIndex="6" Visible="False">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="TipoDocumento" Caption="TipoDocumento" VisibleIndex="1">
                                                <PropertiesTextEdit>
                                                    <Style >
                                                        
                                                    </Style>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Tipo:" CaptionLocation="Top" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Transmitente" FieldName="NomeTransmitente"
                                                VisibleIndex="2">
                                                <PropertiesTextEdit>
                                                    <Style >
                                                        
                                                    </Style>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Transmitente" CaptionLocation="Top" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Testemunhas" FieldName="Testemunhas" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <Style >
                                                        
                                                    </Style>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Testemunhas:" CaptionLocation="Top" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn Caption="Data Registro" FieldName="DataRegistro" VisibleIndex="4">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                    <Style >
                                                        
                                                    </Style>
                                                </PropertiesDateEdit>
                                                <EditFormSettings Caption="Data Registro:" CaptionLocation="Top" />
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Área (ha)" FieldName="Area" VisibleIndex="5">
                                                <PropertiesSpinEdit DisplayFormatString="n4" DecimalPlaces="4" NumberFormat="Custom">
                                                    <SpinButtons ShowIncrementButtons="False">
                                                    </SpinButtons>
                                                    <Style >
                                                        
                                                    </Style>
                                                </PropertiesSpinEdit>
                                                <EditFormSettings Caption="Área (ha):" CaptionLocation="Top" />
                                            </dxwgv:GridViewDataSpinEditColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm">
                                        </SettingsEditing>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                AllowResize="True" Width="600px" />
                                        </SettingsPopup>
                                        <Settings ShowFooter="True" />
                                        <SettingsText ConfirmDelete="Deseja excluir o registro?"></SettingsText>
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                        <ClientSideEvents CallbackComplete="function(s, e) {
   ProcessaResultadoCallback1(s, e);
}" />
                                    </dxcb:ASPxCallback>
                                    <asp:SqlDataSource ID="sdsUFPessoa" runat="server" SelectCommand="SELECT SiglaUF
                         ,NomeUF                         
                     FROM UF"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsUFConjuge" runat="server" SelectCommand="SELECT SiglaUF
                         ,NomeUF                         
                     FROM UF"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsUFResidenciaPessoa" runat="server" SelectCommand="SELECT SiglaUF
                         ,NomeUF                         
                     FROM UF"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsMunicipioPessoa" runat="server" SelectCommand="SELECT CodigoMunicipio
                         ,NomeMunicipio
                         ,SiglaUF
                         FROM Municipio" OnFiltering="sdsMunicipio_Filtering" FilterExpression="SiglaUF = '{0}'">
                                        <FilterParameters>
                                            <asp:ControlParameter ControlID="ddlUFPessoa" DefaultValue="DF" Name="SiglaUF" PropertyName="Value" />
                                        </FilterParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsMunicipioResidenciaPessoa" runat="server" SelectCommand="SELECT CodigoMunicipio
                         ,NomeMunicipio
                         ,SiglaUF
                         FROM Municipio" OnFiltering="sdsMunicipio_Filtering" FilterExpression="SiglaUF = '{0}'">
                                        <FilterParameters>
                                            <asp:ControlParameter ControlID="ddlUFResidenciaPessoa" DefaultValue="DF" Name="SiglaUF"
                                                PropertyName="Value" />
                                        </FilterParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsMunicipioConjuge" runat="server" SelectCommand="SELECT CodigoMunicipio
                         ,NomeMunicipio
                         ,SiglaUF
                         FROM Municipio" OnFiltering="sdsMunicipio_Filtering">
                                        <FilterParameters>
                                            <%--<asp:ControlParameter ControlID="ddlUFConjuge" DefaultValue="DF" Name="SiglaUF" 
                PropertyName="Value" />--%>
                                        </FilterParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsDadosFormulario" runat="server" SelectCommand="SELECT CodigoPessoaImovel
      ,CodigoProjeto
      ,NomePessoa
      ,DataNascimentoPessoa
      ,IndicaNacionalidadePessoa
      ,NomePaisPessoa
      ,CodigoMunicipioNaturalidadePessoa
      ,ProfissaoPessoa
      ,NumeroCPFCNPJPessoa
      ,IndicaPessoaSabeAssinar
      ,TipoDocumentoPessoa
      ,NumeroDocumentoPessoa
      ,OrgaoExpedidorDocumentoPessoa
      ,NomePaiPessoa
      ,NomeMaePessoa
      ,IndicaEstadoCivilPessoa
      ,RegimeSeparacaoBensPessoa
      ,CertidaoEstadoCivilPessoa
      ,LivroCertidaoEstadoCivilPessoa
      ,FolhaCertidaoEstadoCivilPessoa
      ,EmissaoCertidaoEstadoCivilPessoa
      ,NomeCartorioCertidaoEstadoCivilPessoa
      ,AutosSeparacaoPessoa
      ,DataSeparacaoPessoa
      ,JuizoSeparacaoPessoa
      ,DataUniaoEstavel
      ,CertidaoViuvoPessoa
      ,FolhaCertidaoViuvoPessoa
      ,LivroCertidaoViuvoPessoa
      ,IndicaEscrituraRegistroPactoAnteNupcialPessoa
      ,NumeroPactoAnteNupcialPessoa
      ,FolhaRegistroPactoAnteNupcialPessoa
      ,LivroRegistroPactoAnteNupcialPessoa
      ,NomeCartorioRegistroPactoAnteNupcialPessoa
      ,EnderecoResidencialPessoa
      ,NumeroEnderecoResidencialPessoa
      ,CodigoMunicipioResidenciaPessoa
      ,TelefonePessoa
      ,TempoOcupacaoPessoa
      ,NomeConjuge
      ,DataNascimentoConjuge
      ,IndicaNacionalidadeConjuge
      ,NomePaisConjuge
      ,CodigoMunicipioNaturalidadeConjuge
      ,ProfissaoConjuge
      ,NumeroCPFCNPJConjuge
      ,IndicaConjugeSabeAssinar
      ,TipoDocumentoConjuge
      ,NumeroDocumentoConjuge
      ,OrgaoExpedidorDocumentoConjuge
      ,NomePaiConjuge
      ,NomeMaeConjuge
      ,IndicaEstadoCivilConjuge
      ,CertidaoEstadoCivilConjuge
      ,LivroCertidaoEstadoCivilConjuge
      ,FolhaCertidaoEstadoCivilConjuge
      ,EmissaoCertidaoEstadoCivilConjuge
      ,NomeCartorioCertidaoEstadoCivilConjuge
      ,IndicaProprietario
      ,IndicaOcupante
      ,NomeCartorioCertidaoViuvoPessoa
      ,EmissaoCertidaoViuvoPessoa
      ,EmissaoPactoAnteNupcialPessoal
  FROM Prop_PessoaImovel where CodigoPessoaImovel = @CodigoPessoaImovel">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="codigoPessoaImovel" DefaultValue="-1" SessionField="codigoPessoaImovel" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsDocumentoOcupanteImovel" runat="server" SelectCommand="
SELECT CodigoDocumentoOcupanteImovel
      ,CodigoPessoaImovel
      ,TipoDocumento
      ,NomeTransmitente
      ,Testemunhas
      ,DataRegistro
      ,Area
  FROM Prop_DocumentoOcupanteImovel
where CodigoPessoaImovel = @codigoPessoaImovel" DeleteCommand="DELETE FROM Prop_DocumentoOcupanteImovel
      WHERE CodigoDocumentoOcupanteImovel = @codigodocumentoOcupanteImovel" InsertCommand="INSERT INTO Prop_DocumentoOcupanteImovel
           (CodigoPessoaImovel,TipoDocumento,NomeTransmitente
           ,Testemunhas ,DataRegistro,Area)
     VALUES(@CodigoPessoaImovel
           ,@TipoDocumento
           ,@NomeTransmitente
           ,@Testemunhas
           ,@DataRegistro
           ,@Area)" UpdateCommand="UPDATE Prop_DocumentoOcupanteImovel
                       SET TipoDocumento = @TipoDocumento
                       ,NomeTransmitente = @NomeTransmitente
                       ,Testemunhas = @Testemunhas
                       ,DataRegistro = @DataRegistro
                       ,Area = @Area
                       WHERE CodigoDocumentoOcupanteImovel = @CodigoDocumentoOcupanteImovel">
                                        <DeleteParameters>
                                            <asp:Parameter Name="codigodocumentoOcupanteImovel" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:SessionParameter Name="CodigoPessoaImovel" SessionField="codigoPessoaImovel"
                                                DefaultValue="1" />
                                            <asp:Parameter Name="TipoDocumento" Type="String" />
                                            <asp:Parameter Name="NomeTransmitente" Type="String" />
                                            <asp:Parameter Name="Testemunhas" Type="String" />
                                            <asp:Parameter Name="DataRegistro" Type="DateTime" />
                                            <asp:Parameter Name="Area" Type="Decimal" />
                                        </InsertParameters>
                                        <SelectParameters>
                                            <asp:SessionParameter Name="codigoPessoaImovel" SessionField="codigoPessoaImovel"
                                                DefaultValue="1" />
                                        </SelectParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="TipoDocumento" Type="String" />
                                            <asp:Parameter Name="NomeTransmitente" Type="String" />
                                            <asp:Parameter Name="Testemunhas" Type="String" />
                                            <asp:Parameter Name="DataRegistro" Type="DateTime" />
                                            <asp:Parameter Name="Area" Type="Decimal" />
                                            <asp:Parameter Name="CodigoDocumentoOcupanteImovel" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="btnSalvarPropOcupante" runat="server" ClientInstanceName="btnSalvarPropOcupante"
                         Text="Salvar" AutoPostBack="False" Width="110px">
                        <ClientSideEvents Click="function(s, e) {
	btnSalvarPropOcupante_Click(s, e);
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
