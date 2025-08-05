<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioMinisterio.aspx.cs" Inherits="_Projetos_Relatorios_RelatorioMinisterio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .rTable {
            display: table;
            width: 100%
        }

        .rTableRow {
            display: table-row;
        }

        .rTableHeading {
            display: table-header-group;
        }

        .rTableBody {
            display: table-row-group;
        }

        .rTableFoot {
            display: table-footer-group;
        }

        .rTableCell, .rTableHead {
            display: table-cell;
            text-align:left;
            vertical-align: top;
            padding-left: 5px
        }
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="rTable">
                <div class="rTableRow">
                    <div class="rTableCell">
                        <dxcp:ASPxLabel ID="lblAno1" runat="server" ClientInstanceName="lblAno1" Text="Ano 1:">
                        </dxcp:ASPxLabel>
                    </div>
                    <div class="rTableCell">
                        <dxcp:ASPxLabel ID="lblAno2" runat="server" ClientInstanceName="lblAno2" Text="Ano 2:">
                        </dxcp:ASPxLabel>
                    </div>
                    <div class="rTableCell">
                        <dxcp:ASPxLabel ID="lblAno3" runat="server" ClientInstanceName="lblAno3" Text="Ano 3:">
                        </dxcp:ASPxLabel>
                    </div>
                    <div class="rTableCell"></div>
                </div>
                <div class="rTableRow">
                    <div class="rTableCell">
                        <dxcp:ASPxSpinEdit ID="txtAno1" runat="server" ClientInstanceName="txtAno1" ValueType="System.Int" Width="100%">
                        </dxcp:ASPxSpinEdit>
                    </div>

                    <div class="rTableCell">
                        <dxcp:ASPxSpinEdit ID="txtAno2" runat="server" ClientInstanceName="txtAno2" ValueType="System.Int" Width="100%">
                        </dxcp:ASPxSpinEdit>
                    </div>

                    <div class="rTableCell">
                        <dxcp:ASPxSpinEdit ID="txtAno3" runat="server" ClientInstanceName="txtAno2" ValueType="System.Int" Width="100%">
                        </dxcp:ASPxSpinEdit>
                    </div>

                    <div class="rTableCell">
                        <dxcp:ASPxButton ID="btnExportarPdf" runat="server" Text="Exportar" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
lpLoading.Show();                
callbackLoading.PerformCallback();	
}" />
                            <Image Url="~/imagens/botoes/btnPDF.png">
                            </Image>
                        </dxcp:ASPxButton>
    <dxcp:ASPxLoadingPanel runat="server" ClientInstanceName="lpLoading" ID="lpLoading"></dxcp:ASPxLoadingPanel>

                    </div>
                    <div class="rTableCell">

        <dxtv:ASPxPopupControl ID="pcApresentacaoAcao" runat="server" ClientInstanceName="pcApresentacaoAcao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" MaxWidth="650px" MinWidth="260px" Modal="True" CloseAction="None">
            <ClientSideEvents Closing="function(s, e) {
	lblMensagemApresentacaoAcao.SetText('');
}" />
            <ContentCollection>
               
                <dxtv:PopupControlContentControl runat="server">
                    <table cellspacing="0" class="auto-style1">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="center" style="width: 70px" valign="middle">
                                                <dxtv:ASPxImage ID="imgApresentacaoAcao" runat="server" ClientInstanceName="imgApresentacaoAcao" ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png" Height="40px">
                                                </dxtv:ASPxImage>
                                            </td>
                                            <td align="left" style="padding: 10px" valign="middle">
                                                <dxtv:ASPxLabel ID="lblMensagemApresentacaoAcao" runat="server" ClientInstanceName="lblMensagemApresentacaoAcao" EncodeHtml="False" Wrap="False">
                                                </dxtv:ASPxLabel>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 2px">
                                <table cellspacing="0">
                                    <tr>
                                        <td style="padding-right: 3px">
                                            <dxtv:ASPxButton ID="btnOkApresentacaoAcao" runat="server" AutoPostBack="False" ClientInstanceName="btnOkApresentacaoAcao" Text="OK" Width="70px">
                                                <ClientSideEvents Click="function(s, e) {
	pcApresentacaoAcao.Hide();
}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td style="padding-left: 3px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxtv:PopupControlContentControl>
            </ContentCollection>
        </dxtv:ASPxPopupControl>

                        <dxcp:ASPxCallback ID="callbackLoading" ClientInstanceName="callbackLoading" OnCallback="painelCallbackLoading_Callback" runat="server">
                            <ClientSideEvents EndCallback="function(s, e) 
{
            lpLoading.Hide();  
           if(s.cp_erro == &quot;&quot;)
          {
                   window.location = '../../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=false';             
          }
          else
         {
                   lblMensagemApresentacaoAcao.SetText(s.cp_erro);
                    pcApresentacaoAcao.Show();
         }
}" />
                        </dxcp:ASPxCallback>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
