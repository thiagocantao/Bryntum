<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LancamentoRDO.aspx.cs" Inherits="_Projetos_DadosProjeto_LancamentoRDO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
         var valorAtual = 0;

         function convertDate(inputFormat) {
             function pad(s) { return (s < 10) ? '0' + s : s; }
             var d = new Date(inputFormat);
             return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
         }

         function navegaSetas(tipo) {
             var collection = ASPxClientControl.GetControlCollection();
             var indexControle = 0;

             var novoIndex;
             var varAux = 0;

             if (tipo == 'C')
                 varAux = -1;
             else if (tipo == 'B')
                 varAux = 1;
             else if (tipo == 'E')
                 varAux = -1
             else if (tipo == 'D')
                 varAux = 1;

             try {
                 for (var key in collection.elements) {

                     var control = collection.elements[key];
                     if (control.focused == true) {
                         var novoIndex = indexControle + varAux;
                         break;
                     }

                     indexControle++;
                 }

                 indexControle = 0;

                 for (var key in collection.elements) {

                     var control = collection.elements[key];

                     if (novoIndex == indexControle) {

                         if (control.name.indexOf("txt") != -1) {
                             control.Focus();
                             break;
                         }
                     }

                     indexControle++;
                 }

             } catch (e) { }
         }

         function setMaxLength(textAreaElement, length) {
             textAreaElement.maxlength = length;
             ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
             ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
         }

         function onKeyUpOrChange(evt) {
             processTextAreaText(ASPxClientUtils.GetEventSource(evt));
         }

         function processTextAreaText(textAreaElement) {
             var maxLength = textAreaElement.maxlength;
             var text = textAreaElement.value;
             var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
             if (maxLength != 0 && text.length > maxLength)
                 textAreaElement.value = text.substr(0, maxLength);
             
         }

         function createEventHandler(funcName) {
             return new Function("event", funcName + "(event);");
         }

         function onInit_mmObjeto(s, e) {
             try { return setMaxLength(s.GetInputElement(), 4000); }
             catch (e) { }
         }

         function abreOcorrencias() {
            
             window.top.showModal(gvCabecalho.cp_UrlOcorrencias + '&Altura=' + (screen.height - 340), "Ocorrências", screen.width - 250, screen.height - 240, "", null);
         }

         function abrePDF() {
             window.top.showModal(callbackSalvarCabecalho.cp_UrlPDFRDO + '&Altura=' + (screen.height - 340), "RDO", screen.width - 250, screen.height - 240, "", null);
         }

    </script>
    <style type="text/css">
    .group
    {
        padding:0px;
    }
        .style1
        {
            width: 100%;
        }
        .style5
        {
            height: 20px;
        }
        .style6
        {
            height: 20px;
            width: 61px;
        }
        .style13
        {
            height: 89px;
        }
        .style14
        {
            height: 20px;
            width: 40px;
        }
        .style15
        {
            width: 40px;
        }
        .style19
        {
            width: 61px;
        }
        .style20
        {
            height: 20px;
            width: 115px;
        }
        .style21
        {
            width: 115px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table style="width:100%" cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gvCabecalho" runat="server" 
                        AutoGenerateColumns="False"  Width="100%" 
                                    ClientInstanceName="gvCabecalho" 
                        onhtmlrowprepared="gvCabecalho_HtmlRowPrepared" 
                        oncustomcallback="gvCabecalho_CustomCallback" EnableRowsCache="False" 
                        EnableViewState="False" >
                        <ClientSideEvents EndCallback="function(s, e) {
	gvDados.PerformCallback();
}" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowTitlePanel="True" ShowColumnHeaders="False" 
                            VerticalScrollableHeight="150" />
                        <SettingsLoadingPanel Mode="Disabled" ShowImage="False" />
                        <Styles>
                            <Header Wrap="True">
                            </Header>
                            <EmptyDataRow ForeColor="Black">
                            </EmptyDataRow>
                            <Cell>
                                <Paddings PaddingBottom="0px" PaddingTop="0px" />
                            </Cell>
                            <TitlePanel>
                                <Paddings Padding="1px" />
                            </TitlePanel>
                        </Styles>
                        
                        <Templates>
                            <Header>
                                &nbsp;
                            </Header>
                            <EmptyDataRow>
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td align="center" style="border-bottom: 1px solid #808080; font-weight: bold;" 
                                            width="46%" bgcolor="#EBEBEB">
                                            OBRA: USINA HIDRELÉTRICA TELES PIRES</td>
                                        <td align="center" style="border-left: 1px solid #808080; border-bottom: 1px solid #808080;" 
                                            width="27%" bgcolor="#EBEBEB">
                                            <dxe:ASPxTextBox ID="txtCodigo" runat="server" 
                                                ClientInstanceName="txtCodigo"  
                                                Height="20px" HorizontalAlign="Center" Width="100%" Font-Bold="True" 
                                                MaxLength="32">
                                                <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('CodigoConstrutora;' + s.GetValue());
}" />
                                                <Border BorderStyle="None" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td align="center" 
                                            style="border-left: 1px solid #808080; border-bottom: 1px solid #808080; font-weight: bold;" width="27%" 
                                            bgcolor="#EBEBEB">
                                            PARANAÍTA - MT</td>
                                    </tr>
                                    <tr>
                                        <td 
                                            width="46%">
                                            <table cellpadding="0" cellspacing="0" class="style1" style="height: 100%">
                                                <tr>
                                                    <td align="center" style="border-right: 1px solid #808080;" width="60%" 
                                                        valign="top">
                                                        <table cellpadding="0" cellspacing="0" class="style1" 
                                                            style="height: 120px;">
                                                            <tr>
                                                                <td valign="middle" bgcolor="#EBEBEB" 
                                                                    style="height: 20px; border-bottom: 1px solid #808080;">
                                                                    CLIMA</td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table cellpadding="0" cellspacing="0" 
                                                                        style="width: 100%;">
                                                                        <tr>
                                                                            <td class="style6" 
                                                                                style="border-right: 1px solid #808080; border-bottom: 1px solid #808080;" 
                                                                                bgcolor="#EBEBEB">
                                                                                </td>
                                                                            <td bgcolor="#EBEBEB" class="style14" 
                                                                                style="border-right: 1px solid #808080; border-bottom: 1px solid #808080;">
                                                                                Bom</td>
                                                                            <td bgcolor="#EBEBEB" class="style20" 
                                                                                style="border-right: 1px solid #808080; border-bottom: 1px solid #808080;">
                                                                                Precipitação(mm)</td>
                                                                            <td style="border-bottom: 1px solid #808080;" bgcolor="#EBEBEB">
                                                                                Impraticável</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                bgcolor="#EBEBEB" class="style19">
                                                                                Dia/ME</td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style15">
                                                                                <dxe:ASPxCheckBox ID="ckBomDiaME" runat="server" Text=" " 
                                                                                    ClientInstanceName="ckBomDiaME" CheckState="Unchecked" ValueChecked="S" 
                                                                                    ValueType="System.String" ValueUnchecked="N">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txtImpraticavelME.SetEnabled(!ckBomDiaME.GetChecked() &amp;&amp; !ckBomNoiteME.GetChecked());
	if(txtImpraticavelME.GetEnabled() == false)
		txtImpraticavelME.SetText('');

	callbackSalvarCabecalho.PerformCallback('ME_Dia_Bom;' + s.GetValue());
}" Init="function(s, e) {
	txtImpraticavelME.SetEnabled(!ckBomDiaME.GetChecked() &amp;&amp; !ckBomNoiteME.GetChecked());
	if(txtImpraticavelME.GetEnabled() == false)
		txtImpraticavelME.SetText('');
}" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style21">
                                                                                <dxe:ASPxSpinEdit ID="txtPrecipitacaoDiaME" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="100%" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" ClientInstanceName="txtPrecipitacaoDiaME" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('ME_Dia_Precipitacao;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td style="border-bottom: 1px solid #808080;" align="center" bgcolor="White" 
                                                                                rowspan="2">
                                                                                <dxe:ASPxTextBox ID="txtImpraticavelME" runat="server" 
                                                                                    ClientInstanceName="txtImpraticavelME"  
                                                                                    Height="44px" Width="100%" MaxLength="128">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('ME_Impraticavel;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                                </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td bgcolor="#EBEBEB" 
                                                                                style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                class="style19">
                                                                                Noite/ME</td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style15">
                                                                                <dxe:ASPxCheckBox ID="ckBomNoiteME" runat="server" Text=" " 
                                                                                    ClientInstanceName="ckBomNoiteME" CheckState="Unchecked" ValueChecked="S" 
                                                                                    ValueType="System.String" ValueUnchecked="N">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txtImpraticavelME.SetEnabled(!ckBomDiaME.GetChecked() &amp;&amp; !ckBomNoiteME.GetChecked());
	if(txtImpraticavelME.GetEnabled() == false)
		txtImpraticavelME.SetText('');

	callbackSalvarCabecalho.PerformCallback('ME_Noite_Bom;' + s.GetValue());
}" Init="function(s, e) {
	txtImpraticavelME.SetEnabled(!ckBomDiaME.GetChecked() &amp;&amp; !ckBomNoiteME.GetChecked());
	if(txtImpraticavelME.GetEnabled() == false)
		txtImpraticavelME.SetText('');
}" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style21">
                                                                                <dxe:ASPxSpinEdit ID="txtPrecipitacaoNoiteME" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="100%" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" ClientInstanceName="txtPrecipitacaoNoiteME" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('ME_Noite_Precipitacao;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td bgcolor="#EBEBEB" 
                                                                                style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                class="style19">
                                                                                Dia/MD</td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style15">
                                                                                <dxe:ASPxCheckBox ID="ckBomDiaMD" runat="server" Text=" " 
                                                                                    ClientInstanceName="ckBomDiaMD" CheckState="Unchecked" ValueChecked="S" 
                                                                                    ValueType="System.String" ValueUnchecked="N">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txtImpraticavelMD.SetEnabled(!ckBomDiaMD.GetChecked() &amp;&amp; !ckBomNoiteMD.GetChecked());
	if(txtImpraticavelMD.GetEnabled() == false)
		txtImpraticavelMD.SetText('');

	callbackSalvarCabecalho.PerformCallback('MD_Dia_Bom;' + s.GetValue());
}" Init="function(s, e) {
	txtImpraticavelMD.SetEnabled(!ckBomDiaMD.GetChecked() &amp;&amp; !ckBomNoiteMD.GetChecked());
	if(txtImpraticavelMD.GetEnabled() == false)
		txtImpraticavelMD.SetText('');
}" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                            <td style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                align="center" bgcolor="White" class="style21">
                                                                                <dxe:ASPxSpinEdit ID="txtPrecipitacaoDiaMD" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="100%" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" ClientInstanceName="txtPrecipitacaoDiaMD" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('MD_Dia_Precipitacao;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td align="center" bgcolor="White" rowspan="2">
                                                                                <dxe:ASPxTextBox ID="txtImpraticavelMD" runat="server" 
                                                                                    ClientInstanceName="txtImpraticavelMD"  
                                                                                    Height="44px" Width="100%" MaxLength="128">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('MD_Impraticavel;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td bgcolor="#EBEBEB" style="border-right: 1px solid #808080;" class="style19">
                                                                                Noite/MD</td>
                                                                            <td style="border-right: 1px solid #808080;" align="center" bgcolor="White" 
                                                                                class="style15">
                                                                                <dxe:ASPxCheckBox ID="ckBomNoiteMD" runat="server" Text=" " 
                                                                                    ClientInstanceName="ckBomNoiteMD" CheckState="Unchecked" ValueChecked="S" 
                                                                                    ValueType="System.String" ValueUnchecked="N">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txtImpraticavelMD.SetEnabled(!ckBomDiaMD.GetChecked() &amp;&amp; !ckBomNoiteMD.GetChecked());
	if(txtImpraticavelMD.GetEnabled() == false)
		txtImpraticavelMD.SetText('');

	callbackSalvarCabecalho.PerformCallback('MD_Noite_Bom;' + s.GetValue());
}" Init="function(s, e) {
	txtImpraticavelMD.SetEnabled(!ckBomDiaMD.GetChecked() &amp;&amp; !ckBomNoiteMD.GetChecked());
	if(txtImpraticavelMD.GetEnabled() == false)
		txtImpraticavelMD.SetText('');
}" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                            <td style="border-right: 1px solid #808080;" align="center" bgcolor="White" 
                                                                                class="style21">
                                                                                <dxe:ASPxSpinEdit ID="txtPrecipitacaoNoiteMD" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="100%" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" ClientInstanceName="txtPrecipitacaoNoiteMD" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('MD_Noite_Precipitacao;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" valign="top" width="40%">
                                                        <table cellpadding="0" cellspacing="0" class="style1" style="height: 120px">
                                                            <tr>
                                                                <td style="border-bottom: 1px solid #808080; height: 20px;" bgcolor="#EBEBEB">
                                                                    HORAS PARALISADAS</td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table>
                                                                        <tr>
                                                                            <td bgcolor="#EBEBEB" class="style5" 
                                                                                style="border-bottom: 1px solid #808080; border-right: 1px solid #808080;" 
                                                                                width="50%">
                                                                                No Dia</td>
                                                                            <td bgcolor="#EBEBEB" class="style5" style="border-bottom: 1px solid #808080;">
                                                                                Acumul. Obra</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td bgcolor="White" class="style13" style="border-right: 1px solid #808080; " 
                                                                                width="50%">
                                                                                <dxe:ASPxSpinEdit ID="txtHorasParalisadasDia" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="85px" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" ClientInstanceName="txtHorasParalisadasDia" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('HorasParalisadas;' + s.GetValue());
}" />
                                                                                    <Border BorderStyle="None" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td bgcolor="#EBEBEB" class="style13">
                                                                                <dxe:ASPxSpinEdit ID="txtHorasParalisadasAcumulado" runat="server" AllowMouseWheel="False" 
                                                                                    DecimalPlaces="2" Height="85px" HorizontalAlign="Center" MaxValue="99999999" 
                                                                                    NullText=" " Width="100%" 
                                                                                    ClientInstanceName="txtHorasParalisadasAcumulado" ClientEnabled="False" 
                                                                                    >
                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                    </SpinButtons>
                                                                                    <Border BorderStyle="None" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxSpinEdit>
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
                                        <td 
                                            style="border-left: 1px solid #808080;" 
                                            width="27%" valign="top">
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td align="center" bgcolor="#EBEBEB" 
                                                        style="border-bottom: 1px solid #808080; height: 20px;">
                                                        FRENTE DE SERVIÇO</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtFrenteServico" runat="server"  
                                                            Height="110px" Width="100%" ClientInstanceName="txtFrenteServico" 
                                                            Font-Strikeout="False" Font-Underline="False">
                                                            <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('FrenteServico;' + s.GetValue());
}" />
                                                            <Border BorderStyle="None" />
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border-left: 1px solid #808080;" 
                                            width="27%" valign="top">
                                            <table cellpadding="0" cellspacing="0" class="style1" >
                                                <tr>
                                                    <td align="center" bgcolor="#EBEBEB" 
                                                        style="border-bottom: 1px solid #808080; height: 20px;">
                                                        ENCARREGADO GERAL</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtEncarregadoGeral" runat="server"  
                                                            Height="110px" Width="100%" ClientInstanceName="txtEncarregadoGeral" 
                                                            Font-Strikeout="False" Font-Underline="False">
                                                            <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" ValueChanged="function(s, e) {
	callbackSalvarCabecalho.PerformCallback('EncarregadoGeral;' + s.GetValue());
}" />
                                                            <Border BorderStyle="None" />
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataRow>
                            <titlepanel>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" width="80">
                                            <dxe:ASPxImage ID="imgOcorrencias0" runat="server" 
                                                ClientInstanceName="imgOcorrencias" Cursor="pointer" 
                                                ImageUrl="~/imagens/ArquivoPDF.png" ToolTip="Ocorrências">
                                                <ClientSideEvents Click="function(s, e) {
	abrePDF();
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 2px">
                                                        DIÁRIO DE OBRA - Data:</td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="deData" runat="server" AllowMouseWheel="False" 
                                                            AllowNull="False" AllowUserInput="False" ClientInstanceName="deData" 
                                                            DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" 
                                                            EditFormatString="dd/MM/yyyy"  
                                                            oncalendardaycellprepared="deData_CalendarDayCellPrepared" Width="100px" 
                                                            AutoPostBack="True" onvaluechanged="deData_ValueChanged">
                                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                            </CalendarProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
	hfGeral.Set('Data', s.GetText());
	lpCarregando.Show();
}" Init="function(s, e) {
	lpCarregando.Hide();
}" />
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" title="Ocorrências" width="80">
                                            <dxe:ASPxImage ID="imgOcorrencias" runat="server" 
                                                ClientInstanceName="imgOcorrencias" Cursor="pointer" 
                                                ImageUrl="~/imagens/Ocorrencias.png" ToolTip="Ocorrências">
                                                <ClientSideEvents Click="function(s, e) {
	abreOcorrencias();
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </titlepanel>
                        </Templates>
                        <Border BorderWidth="1px" />
                        <BorderBottom BorderStyle="None" />
                    </dxwgv:ASPxGridView></td>
            </tr>
            <tr>
                <td>
        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
            onhtmlrowprepared="gvDados_HtmlRowPrepared" 
            onhtmldatacellprepared="gvDados_HtmlDataCellPrepared" 
            KeyFieldName="CodigoItem"  
            Width="100%" ClientInstanceName="gvDados" EnableRowsCache="False" 
                        EnableViewState="False">
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Categoria" FieldName="Categoria" GroupIndex="0" 
                    SortIndex="0" SortOrder="Ascending" VisibleIndex="0" Width="350px">
                    <GroupRowTemplate>
                        <table style="width:100%; font-weight:bold; height:22px"><tr><td><%# Eval("Categoria")%></td></tr></table> 
                    </GroupRowTemplate>
                    <HeaderStyle Font-Bold="True"  />
                    <CellStyle BackColor="#F8F9C8">
                        <Paddings Padding="0px" />
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NumeroTipo" VisibleIndex="1" 
                    GroupIndex="1" SortIndex="1" SortOrder="Ascending" Width="220px" 
                    Caption="Tipo">
                    <GroupRowTemplate>
                        <%# Eval("TipoItem") %>                     
                    </GroupRowTemplate>
                    <CellStyle BackColor="#FFFF9F">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoItem" 
                    VisibleIndex="2" Width="400px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" 
                    VisibleIndex="3" Name="QTDE" Width="150px">
                    <PropertiesTextEdit DisplayFormatString="N2">
                    </PropertiesTextEdit>
                    <DataItemTemplate>
                        <dxe:ASPxSpinEdit ID="txtQTDE" runat="server" backcolor="#E1EAFF" 
                            decimalplaces="2" displayformatstring="{0:n2}"
                            forecolor="Black" horizontalalign="Right" increment="0" 
                            largeincrement="0" maxvalue="9999999999999" nulltext="0" 
                            style="margin-bottom: 0px" Text='<%# Eval("Quantidade") + "" %>' width="100%">
                            <spinbuttons showincrementbuttons="False">
                            </spinbuttons>
                            <clientsideevents valuechanged="function(s, e) {
	callbackSalvar.PerformCallback('QTDE');
}" />
                            <nulltextstyle forecolor="Black">
                            </nulltextstyle>
                            <validationsettings errordisplaymode="ImageWithTooltip" 
                                errortextposition="Left" validationgroup="MKE">
                            </validationsettings>
                            <border borderstyle="None" />
                            <disabledstyle backcolor="#EBEBEB" forecolor="Black">
                            </disabledstyle>
                        </dxe:ASPxSpinEdit>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle BackColor="#E1EAFF">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="4">
                    <CellStyle BackColor="#EBEBEB">
                        <Border BorderStyle="None" />
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowDragDrop="False" AllowSort="False" 
                AutoExpandAllGroups="True" AllowFocusedRow="True" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
            <Paddings Padding="0px" />
            <Styles GroupButtonWidth="10">
                <GroupRow CssClass="group" BackColor="#EBEBEB">
                </GroupRow>
            </Styles>          
        </dxwgv:ASPxGridView>
    
                </td>
            </tr>
        </table>
        <dxcb:ASPxCallback ID="callbackSalvar" ClientInstanceName="callbackSalvar" 
            runat="server" oncallback="callbackSalvar_Callback">
        </dxcb:ASPxCallback>

        <dxcb:ASPxCallback ID="callbackSalvarCabecalho" ClientInstanceName="callbackSalvarCabecalho" 
            runat="server" oncallback="callbackSalvarCabecalho_Callback">
        </dxcb:ASPxCallback>

        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>

        <dxlp:ASPxLoadingPanel ID="lpCarregando" runat="server" 
            ClientInstanceName="lpCarregando"  
            Height="60px" Modal="True" Text="Carregando&amp;hellip;" Width="150px">
        </dxlp:ASPxLoadingPanel>

    </div>
    </form>
</body>
</html>
