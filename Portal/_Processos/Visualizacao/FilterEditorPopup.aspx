<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilterEditorPopup.aspx.cs" Inherits="_Processos_Visualizacao_FilterEditorPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <!-- Style Bootstrap -->
    <link href="../../Bootstrap/vendor/bootstrap/v4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Bootstrap/vendor/fontawesome/v5.0.12/css/fontawesome-all.min.css" rel="stylesheet" />
    <title></title>
     <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script>
        function funcaoCallbackSalvar() {
                       
           callBack.PerformCallback('', '');
        }
        function funcaoCallbackSalvarPosConfirm() {
            window.parent.mostraConfirmacao('Há personalizações para este filtro. O acionamento do botão Salvar irá retirar estas personalizações. Confirma a gravação?', function () {  CallbackSalvarPosConfirm.PerformCallback('', ''); }, null);
         
        }
        function funcaoCallbackFechar() {
            window.parent.fechaModalComFooter();
        }
    </script>
    <style type="text/css"> 
        .container.custom-container { 
             margin-left:unset;
             border-style:groove;
             max-width:100%;
             margin-top:20px;
             padding-bottom:10px;
             } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input id="retorno_popup" name="retorno_popup" runat="server" type="hidden" value="" />
        <div class="custom-container container"  >
          <div class="row" >
            <div class="col-6" >
              <dxtv:ASPxCheckBox runat="server" ID="cbIndicaPreFiltroPersonalizado" ClientInstanceName  ="cbIndicaPreFiltroPersonalizado" Text="<%$ Resources:traducao, FilterEditorPopup_os_usu_rios_poder_o_personalizar_este_filtro %>" TextAlign="Left">
                    <BorderBottom BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px" /> 
                  

                    
                  
            </dxtv:ASPxCheckBox> 
            
            </div>
            <div class="col-6"></div>
          </div>
          <div class="row" style="margin-top:7px">
            <div class="col">
                  <dxtv:ASPxFilterControl ID="filter" runat="server" Width="100%"  ClientInstanceName="filter" >
                </dxtv:ASPxFilterControl>

            </div>
           </div>
        </div>

            <dxcb:ASPxCallback ID="CallbackSalvarPosConfirm" runat="server" ClientInstanceName="CallbackSalvarPosConfirm" OnCallback="SalvarPosConfirm_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
                
              if(s.cp_OK != '')
             {
                console.log('ok' + s.cp_OK);
                       document.getElementById('retorno_popup').value =  s.cp_clu;
                       window.parent.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                       window.parent.retornoModal = 'S';
                       window.parent.fechaModalComFooter();
             }
              else
             {
              
                      if(s.cp_Erro1 != '')               
                            window.parent.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
              }
                }"></ClientSideEvents>
            </dxcb:ASPxCallback>

            <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
              if(s.cp_OK != '')
             {
                       document.getElementById('retorno_popup').value =  s.cp_clu; 
                       window.parent.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                       window.parent.retornoModal = 'S';
                       window.parent.fechaModalComFooter();
             }
              else
             {
              
                      if(s.cp_Erro != '')               
                            window.parent.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
              }
            if(s.cp_callConfirm == 's')
                funcaoCallbackSalvarPosConfirm()
}"></ClientSideEvents>
        </dxcb:ASPxCallback>
        <asp:SqlDataSource ID="dataSource" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
