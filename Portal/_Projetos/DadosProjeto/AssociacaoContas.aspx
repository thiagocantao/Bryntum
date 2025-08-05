<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssociacaoContas.aspx.cs" Inherits="_Projetos_DadosProjeto_AssociacaoContas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>

    <title></title>
    <script lang="javascript" type="text/javascript">
        function incluiConta(codigoConta) {
            tlDados.PerformCallback("INC;" + codigoConta)
        }

        function ativaConta(codigoConta) {
            tlDados.PerformCallback("ATV;" + codigoConta)
        }

        function desativaConta(codigoConta) {
            tlDados.PerformCallback("DST;" + codigoConta)
        }

        function excluiConta(codigoConta) {
            var funcObj = { funcaoClickOK: function (codigoContaParam) { tlDados.PerformCallback("EXC;" + codigoContaParam); } }
            window.top.mostraConfirmacao('Confirma a exclusão da conta para este projeto?', function () { funcObj['funcaoClickOK'](codigoConta) }, null);
        }
        function novaDescricao() {
            timedCount(0);
        }

        function timedCount(tempo) {
            if (tempo == 3) {
                tempo = 0;
                tlDados.PerformCallback("SCH;-1");
            }
            else {
                tempo++;
                t = setTimeout("timedCount(" + tempo + ")", 1000);
            }
        }
    </script>

</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div style="display: flex; flex-direction: column">
            <div id="painelDeBusca" style="float: left;">

                <dxe:ASPxTextBox ID="txtBusca" runat="server"  ClientInstanceName="txtBusca" NullText="Informe parte do nome do Plano de contas para a pesquisa." NullTextDisplayMode="UnfocusedAndFocused"
                    Width="100%" ToolTip="Informe parte do nome do Plano de contas para a pesquisa." EnableViewState="False">
                    <ClientSideEvents KeyDown="function(s, e) {
                                                if(e.htmlEvent.keyCode == 13) {
                                                    ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                                                }
                                                else{
                                                    novaDescricao();	
                                                }
                                            }" />
                </dxe:ASPxTextBox>

            </div>
            <div>
                <dxwtl:ASPxTreeList ID="tlDados" runat="server" ClientInstanceName="tlDados" Width="100%" AutoGenerateColumns="False" KeyFieldName="CodigoConta" ParentFieldName="CodigoContaSuperior" OnCustomCallback="tlDados_CustomCallback">
                    <Columns>
                        <dxwtle:TreeListTextColumn Caption=" Descrição" FieldName="CodigoConta" VisibleIndex="0">
                            <DataCellTemplate>
                                <%# getBotoes() %>
                            </DataCellTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn Caption="Tipo Conta" FieldName="TipoConta" VisibleIndex="2" Width="180px">
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn Caption="Código Reservado" FieldName="CodigoReservadoGrupoConta" VisibleIndex="3" Width="180px">
                        </dxwtle:TreeListTextColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Auto" />
                    <SettingsBehavior AutoExpandAllNodes="True" />

                    <SettingsPopup>
                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>

                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_MSG != '')
	{
		var acao = s.cp_MSG;
		if (acao.toUpperCase().indexOf('SUCESSO')!=-1)
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
              else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
	}
}"
                        Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 90;
       s.SetHeight(sHeight);
}" />
                </dxwtl:ASPxTreeList>
            </div>
            <div>
                <div style="float: right;">
                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar" CssClass="btn_inicialMaiuscula" ClientVisible="False">
                        <ClientSideEvents Click="function(s, e) {
                        window.top.fechaModal();
                    }"></ClientSideEvents>

                        <Paddings Padding="0px"></Paddings>
                    </dxcp:ASPxButton>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
