<%@ Page Language="C#" AutoEventWireup="true" CodeFile="provisorio.aspx.cs" Inherits="_CertificadoDigital_provisorio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function onClick(s, e) {
            var codigos = '';
            for (var i = 0; i < grid.cp_RowCount; i++) {
                var cod = grid.GetRowKey(i);
                if (cod == null || cod == undefined)
                    cod = -1;
                codigos += cod.toString();
                if (i + 1 < grid.cp_RowCount)
                    codigos += ',';
            }
            //window.top.showModal('./assinaturaMultiplosFluxos.aspx?codigos=' + codigos, 'Assinar', 1000, 460, null, null);

            pc.SetWidth(600);
            pc.SetHeight(350);
            pc.SetContentUrl('./assinaturaMultiplosFluxos.aspx?codigos=' + codigos);
            //meout ('alteraUrlModal();', 0);            
            pc.SetHeaderText('Assinar');
            pc.Show()
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dxcp:ASPxGridView ID="ASPxGridView1" runat="server" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" 
            ClientInstanceName="grid" KeyFieldName="CodigoFormularioAssinar" 
            oncustomjsproperties="ASPxGridView1_CustomJSProperties">
            <Columns>
                <dxtv:GridViewDataTextColumn FieldName="NumeroDemanda" VisibleIndex="0">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="TituloDemanda" VisibleIndex="1">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="SiglaOrgao" VisibleIndex="2">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="Decisao" VisibleIndex="3">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="Deliberacao" VisibleIndex="4">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="ValorSolicitado" VisibleIndex="5">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="ValorAprovado" VisibleIndex="6">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="ValorSolicitadoExercicio" 
                    VisibleIndex="7">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="TipoDeliberacao" VisibleIndex="8">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataDateColumn FieldName="DataDeliberacao" VisibleIndex="9">
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataTextColumn FieldName="ValorAprovadoExercicio" 
                    VisibleIndex="10">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="NumeroOficio" VisibleIndex="11">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataDateColumn FieldName="DataOficio" VisibleIndex="12">
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoWorkflow" Visible="False" 
                    VisibleIndex="13">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoInstanciaWf" Visible="False" 
                    VisibleIndex="14">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoFluxo" Visible="False" 
                    VisibleIndex="15">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoEtapaInicial" Visible="False" 
                    VisibleIndex="16">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoEtapaAtual" Visible="False" 
                    VisibleIndex="17">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="OcorrenciaAtual" Visible="False" 
                    VisibleIndex="18">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" Visible="False" 
                    VisibleIndex="19">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoFormularioAssinar" 
                    Visible="False" VisibleIndex="20">
                </dxtv:GridViewDataTextColumn>
            </Columns>
        </dxcp:ASPxGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="Data Source=sql2008r2;Initial Catalog=dbCdisPortalEstrategia_PBH_HM;User ID=PEstrategia;Password=12345678" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="select * from f_pbh_GetOficiosAssinar('CCG')">
        </asp:SqlDataSource>
    
    </div>
    <dxcp:ASPxButton ID="ASPxButton1" runat="server" Text="Assinar" 
        AutoPostBack="False">
        <ClientSideEvents Click="function(s, e) {
	onClick(s,e);
}" />
    </dxcp:ASPxButton>
    <dxcp:ASPxPopupControl ID="ASPxPopupControl1" runat="server" Height="317px" 
        Width="565px" ClientInstanceName="pc" CloseAction="CloseButton" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
